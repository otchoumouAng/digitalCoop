using DevExpress.XtraReports.UI;
using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Business;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Components.Data;
using Tms.Components.Settings;

namespace Tms2017.MVC.Controllers
{
    public class PayementSiteController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        public PayementSiteController()
        {
            //string cultureName = "en";
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
        }
        // GET: Payement
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;            

            //mCampagne.fnGet(mclass.Campagne);
            X.GetCmp<ComboBox>("PayementCampagneID").SetValue(mParam.Campagne);            
            
            X.GetCmp<DateField>("TxtPayementPeriodStart").RawText = DateTime.Now.ToShortDateString();
            X.GetCmp<DateField>("TxtPayementPeriodEnd").RawText = DateTime.Now.ToShortDateString();

            X.GetCmp<FormPanel>("CriteriaPanelPayement").SetTitle("Site : " + mSiteParDefaut.Nom + ", Today : " + DateTime.Now.ToShortDateString());

            //MenuAccess menuAcces = new MenuAccess();
            //menuAcces = InitFunctionAccess();

            #region Set Function's Access

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{6af91f24-e67d-498b-b84b-5cb919eaf11a}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();

            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{A52FA7FC-33C5-42D8-9A9E-28227355DF8D}", UserName);
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{4fc699cd-4e8e-4455-a353-a47a7329e4b4}")))
                X.GetCmp<Button>("btnNewPayement").Enable();
            else
                X.GetCmp<Button>("btnNewPayement").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{fcab7969-6d8b-4911-9228-2fa743c5bbc1}")))
                X.GetCmp<MenuItem>("mnuExportPayments").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportPayments").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{836eb20c-a1e0-4579-a41c-db8bb13dae0c}")))
                X.GetCmp<MenuItem>("btnViewPendingPayement").Enable();
            else
                X.GetCmp<MenuItem>("btnViewPendingPayement").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{407c348b-53ef-462b-ae5c-f2732aaa82fa}")))
                X.GetCmp<MenuItem>("mnuPrintPayementList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintPayementList").Disable();

            X.GetCmp<Hidden>("PahiddenPermCreer").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{4fc699cd-4e8e-4455-a353-a47a7329e4b4}")));
            X.GetCmp<Hidden>("PahiddenPermConsultPendingPayments").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{836eb20c-a1e0-4579-a41c-db8bb13dae0c}")));
            X.GetCmp<Hidden>("PahiddenPermDesactiver").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{dfe0761e-17bd-4aa7-99e3-b997f1a8ca10}")));
            X.GetCmp<Hidden>("PahiddenPermPrintCopyOfPayment").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{fe4be994-590b-4367-af3f-d51a958da210}")));
            X.GetCmp<Hidden>("PahiddenPermExporterExcel").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{fcab7969-6d8b-4911-9228-2fa743c5bbc1}")));
            X.GetCmp<Hidden>("PahiddenPermPrintListOfPayment").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{407c348b-53ef-462b-ae5c-f2732aaa82fa}")));
            X.GetCmp<Hidden>("PahiddenPermOverview").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{1ae403bd-a4dd-423f-b32f-7e1a2753a519}")));
            X.GetCmp<Hidden>("PahiddenPermPrintCopyOfPaymentDet").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{d099d5e6-862d-413a-a840-40604a183c17}")));
            #endregion


            return View();
        }


        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelPayement");            
            mform.ToggleCollapse();            
            return this.Direct();
        }

        public ActionResult onRefresh(string ItemCampagne, string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd, string ItemType, string ItemStatut, string ItemSite)
        {
            try
            {                

                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);              

                Store mstore = X.GetCmp<Store>("storeListePayement");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemCampagne"   ,ItemCampagne),
                                new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                new Ext.Net.Parameter("ItemSite"   ,ItemSite),
                                new Ext.Net.Parameter("ItemPeriodStart"     ,ItemPeriodStart),
                                new Ext.Net.Parameter("ItemPeriodEnd"           ,ItemPeriodEnd),
                                new Ext.Net.Parameter("ItemType"           ,ItemType),
                                new Ext.Net.Parameter("ItemStatut"           ,ItemStatut)
                            });

                FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelPayement");                
                mform.Collapsed = true;
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Payement : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });                
            }
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne, string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd, string ItemType, string ItemStatut, string ItemSite)
        {
            string CropYearID = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                CropYearID = "{Tous}";
            }

            int fournisseurID = string.IsNullOrEmpty(ItemFournisseur) ? -1 : GetCritriaValue(ItemFournisseur);
            int PayementTypeID = string.IsNullOrEmpty(ItemType) ? -1 : GetCritriaValue(ItemType);
            int SiteID = string.IsNullOrEmpty(ItemSite) ? 1 : GetCritriaValue(ItemSite);

            DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? DateTime.Now : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? DateTime.Now : DateTime.Parse(ItemPeriodEnd.ToString());

            string status = string.IsNullOrEmpty(ItemStatut) ? "-1" : ItemStatut;
            if (!string.IsNullOrEmpty(ItemStatut) && ItemStatut.Contains("null"))
            {
                status = "-1";
            }
            var mListe = (new Payement()).fnSelect(CropYearID, fournisseurID, startdate, enddate, PayementTypeID, status, SiteID);
              
            return this.Store(mListe);
        }

        
        public ActionResult OnPrintVoucher(string ItemSelected, int payementType, string IsCopy)
        {
            bool ReportIscopy = false;
            if (string.IsNullOrEmpty(IsCopy))
                ReportIscopy = true;

            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            //string param = JSON.Serialize(contratperiode);
            Session["ParamReport"] = ItemSelected;

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Cash Payment Voucher{0}', '{1}/PayementSite/ViewVoucher?payementtype={2}&IsCopy={3}', this, 'Voucher','')", Guid.NewGuid(), BaseUrl, payementType,ReportIscopy));
        }

        public ActionResult OnPrintVoucherDet(string ItemSelected, int payementType, string IsCopy)
        {
            if (payementType != 1)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Payement : Data Validation",
                    Message = "Can't show Detail of this type of Payment",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }

            bool ReportIscopy = false;
            if (string.IsNullOrEmpty(IsCopy))
                ReportIscopy = true;

            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            //string param = JSON.Serialize(contratperiode);
            Session["ParamReport"] = ItemSelected;

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Cash Payment Voucher{0}', '{1}/PayementSite/ViewVoucherDet?payementtype={2}&IsCopy={3}', this, 'Voucher','')", Guid.NewGuid(), BaseUrl, payementType, ReportIscopy));
        }

        public ActionResult ViewVoucher(int payementtype, bool IsCopy)
        {
            XtraReport report = null;
            //Payement payement = new Payement();
            //payement.fnGet(id);
            Payement payement = JSON.Deserialize<Payement>((string)Session["ParamReport"], new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            if (payementtype == 1)
            {
                report = new rptSiteVoucherDelivery() as XtraReport;
                report.DataSource = DevExpressReportDs.SetDataSource(report);
            }

            if (payementtype == 2)
            {
                report = new RptSiteVoucherFinancing() as XtraReport;
                report.DataSource = DevExpressReportDs.SetDataSource(report);
            }

            if (payementtype == 3)
            {
                report = new rptSiteVoucherSaving() as XtraReport;
                report.DataSource = DevExpressReportDs.SetDataSource(report);
            }
            if (payementtype == 4)
            {
                report = new RptVoucherBonus() as XtraReport;
                report.DataSource = DevExpressReportDs.SetDataSource(report);
            }
            report.Parameters["Number"].Value = payement.ID;
            report.Parameters["AmountInLetter"].Value = MoneySpeller.ToLettres(Int32.Parse(payement.Montant.ToString()));
            report.Parameters["IsCopy"].Value = IsCopy;

            ViewData["Report"] = report;

            return View();
        }

        public ActionResult ViewVoucherDet(int payementtype, bool IsCopy)
        {
            XtraReport report = null;
            //Payement payement = new Payement();
            //payement.fnGet(id);
            Payement payement = JSON.Deserialize<Payement>((string)Session["ParamReport"], new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            if (payementtype == 1)
            {
                report = new rptVoucherDeliveryDetail() as XtraReport;
                report.DataSource = DevExpressReportDs.SetDataSource(report);
            }            

            report.Parameters["Number"].Value = payement.ID;
            report.Parameters["ID"].Value = payement.ID;
            report.Parameters["AmountInLetter"].Value = MoneySpeller.ToLettres(Int32.Parse(payement.Montant.ToString()));
            report.Parameters["IsCopy"].Value = IsCopy;

            ViewData["Report"] = report;

            return View("ViewVoucher");
        }

        #region Method
        private string GetFormValue(string id_Component)
        {
            return Request.Form[id_Component];
        }

        private Tms.Components.Settings.EnumsDefinition.eExecMode GetFormExecMode(object hidAction)
        {
            try
            {
                if (hidAction != null)
                {
                    if (hidAction.ToString().Equals(ADD_NEW))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
                    else if (hidAction.ToString().Equals(APPROVE))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;
                    else if (hidAction.ToString().Equals(CONSULT))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
                    else if (hidAction.ToString().Equals(DEFAULT))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
                    else if (hidAction.ToString().Equals(UPDATE))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                    else if (hidAction.ToString().Equals(EXTEND))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Extend;
                }
                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
            }
            catch (Exception ex)
            {
                Ext.Net.X.Msg.Alert("frm_Detail : GetFormExecMode", ex.Message).Show();
                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
            }
        }

        private int GetCritriaValue(string strComponent)
        {
            int value;

            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
                return value;
            else
                return -1;
        }

        private int GetCritriasValue(string strComponent)
        {
            if (X.GetCmp<ComboBox>(strComponent) != null && X.GetCmp<ComboBox>(strComponent).SelectedItem != null && X.GetCmp<ComboBox>(strComponent).SelectedItem.Value != null)
                return int.Parse(X.GetCmp<ComboBox>(strComponent).SelectedItem.Value.ToString());
            else
                return -1;
        }

        private void DeselectGridRows()
        {
            //X.Js.Call("App.rowSelectionListe.deselectAll();");
            X.GetCmp<RowSelectionModel>("rowSelectionPayement").DeselectAll();
        }

        public void CreateIconsList()
        {
            try
            {
                //Ext.Net.ResourceManager.RegisterGlobalIcon(Icon.PageCancel);

                //string url =  Ext.Net.ResourceManager.GetInstance().GetIconUrl(Icon.Tick);
            }
            catch (Exception ex)
            {

                X.Msg.Alert("frmLBC_ReceptionInDistrict_Overview : CreateIconList", ex.Message).Show();
            }
        }
        #endregion

        private MenuAccess InitFunctionAccess()
        {
            MenuAccess menuAccess = new MenuAccess();
            menuAccess.CanAdd = (new Fonction()).fnGetUserAccessStatus("80b6196c-f8b1-4c47-9974-c63cf0b6542a", (string)Session["username"]);
            return menuAccess;
        }

        public ActionResult OnPrintHystoryOfPayment(string cropYear, string payementType, string supplier, string startDate, string endDate, string supplierName, string payementTypeDesignation)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                Session["paramCampagne"] = cropYear;
                if (cropYear.Contains("{Tous}"))
                    Session["campagneID"] = "";
                else
                    Session["campagneID"] = cropYear;

                Session["PayementTypeId"] = Int32.Parse(payementType);
                Session["PayementTypeNom"] = payementTypeDesignation;

                Session["fournisseurID"] = Int32.Parse(supplier);

                Session["DateDebut"] = dateDebut;
                Session["DateFin"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Payement/ViewReportResult', this, 'Hystory Of Payment',''),App.frmSupplierPrintHystoryOfPayment.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }


        }


        public ActionResult ViewReportResult()
        {
            XtraReport report = null;

            // TODO :  sur le base du type de rapport : detaillé ou cumulé 
            // initialiser l'objet report avec l'object idoine

            report = new rptSupplierHystoryOfPayment() as XtraReport;

            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramCampagne"].Value = Session["paramCampagne"];

            report.Parameters["campagne"].Value = Session["campagneID"];

            report.Parameters["PayementTypeId"].Value = Session["PayementTypeId"];

            report.Parameters["PayementTypeNom"].Value = Session["PayementTypeNom"];

            report.Parameters["fournisseurID"].Value = Session["fournisseurID"];

            report.Parameters["DateDebut"].Value = Session["DateDebut"];

            report.Parameters["DateFin"].Value = Session["DateFin"];

            ViewData["Report"] = report;

            return View();
        }

        public ActionResult OnDisplayPayementList()
        {
            ViewData["Titre"] = "Print List of Payments";
            ViewData["actionToDo"] = "OnPrintPaymentList";
            ViewData["ControllerName"] = "Payement";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmCriteriaForPayment", ViewData = ViewData };
        }

        public ActionResult OnPrintPaymentList(string cropyear, string fournisseur, string fournisseurText, string typepayement, string typepayementText, string startDate, string endDate, string statut, string statutText)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                //Session["paramCropYear"] = cropYear;                
                Session["paramCampagne"] = cropyear;
                Session["paramFournisseur"] = fournisseur;
                Session["paramFournisseurText"] = fournisseurText;
                Session["paramTypeOfPayment"] = typepayement;
                Session["paramTypeOfPaymentText"] = typepayementText;
                Session["paramStatut"] = statut;
                Session["paramStatutText"] = statutText;

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Payement/ViewReportListResult', this, 'Liste Des Paiements',''),App.frmCriteriaForPayment.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Payment : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
        }

        public ActionResult ViewReportListResult()
        {
            //XtraReport report = null;

            rptPayementList report = new rptPayementList();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramCampagneText"].Value = Session["paramCampagne"];
            if (!string.IsNullOrEmpty(Session["paramCampagne"].ToString()) && Session["paramCampagne"].ToString() == "{Tous}")
                report.Parameters["paramCampagne"].Value = string.Empty;
            else
                report.Parameters["paramCampagne"].Value = Session["paramCampagne"];

            report.Parameters["paramFournisseur"].Value = Session["paramFournisseur"];
            report.Parameters["paramFournisseurText"].Value = Session["paramFournisseurText"];

            report.Parameters["paramTypeOfPayment"].Value = Session["paramTypeOfPayment"];
            report.Parameters["paramTypeOfPaymentText"].Value = Session["paramTypeOfPaymentText"];

            report.Parameters["paramStatut"].Value = Session["paramStatut"];
            report.Parameters["paramStatutText"].Value = Session["paramStatutText"];

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];
            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

    }
}