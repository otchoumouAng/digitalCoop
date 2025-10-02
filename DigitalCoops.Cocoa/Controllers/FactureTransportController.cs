using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Business;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class FactureTransportController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: FactureTransport
        public ActionResult Index()
        {            
            Parametres mclass = new Parametres();
            mclass.fnGet();

            Campagne mCampagne = new Campagne();

            mCampagne.fnGet(mclass.Campagne);
            X.GetCmp<ComboBox>("FactureTransportCampagneID").SetValue(mclass.Campagne);            
            
            X.GetCmp<DateField>("TxtFactureTransportPeriodStart").RawText = DateTime.Now.ToShortDateString();
            X.GetCmp<DateField>("TxtFactureTransportPeriodEnd").RawText = DateTime.Now.ToShortDateString();

            X.GetCmp<FormPanel>("CriteriaPanelFactureTransport").SetTitle("Today : " + DateTime.Now.ToShortDateString());


            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{E2867BC9-F369-4670-A797-8C944D58D663}", UserName);

            if (HasAccess.fnGetUserAccessStatus("{379DAB56-070C-4280-A404-99D7EE9ED397}", UserName) == false)
                X.GetCmp<Button>("btnNewFactureTransport").Disable();
            else
                X.GetCmp<Button>("btnNewFactureTransport").Enable();

            if (HasAccess.fnGetUserAccessStatus("{FAD91CA0-7422-47D1-B7C9-82BFBF1622E6}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportTrInvoices").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportTrInvoices").Enable();

            if (HasAccess.fnGetUserAccessStatus("{BFFA1DC6-6EEC-49B1-997C-108BB0C3192F}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintTransportInvoiceList").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintTransportInvoiceList").Enable();

            X.GetCmp<Hidden>("FthiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{379DAB56-070C-4280-A404-99D7EE9ED397}", UserName));
            X.GetCmp<Hidden>("FthiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{05201E33-6361-4DDD-8E51-97D383640223}", UserName));
            X.GetCmp<Hidden>("FthiddenPermPrintCopyOfPayment").SetValue(HasAccess.fnGetUserAccessStatus("{55FF8DA6-6ECD-4D4B-959C-0929DBA581AF}", UserName));
            X.GetCmp<Hidden>("FthiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{FAD91CA0-7422-47D1-B7C9-82BFBF1622E6}", UserName));
            X.GetCmp<Hidden>("FthiddenPermPrintListOfPayment").SetValue(HasAccess.fnGetUserAccessStatus("{BFFA1DC6-6EEC-49B1-997C-108BB0C3192F}", UserName));
            X.GetCmp<Hidden>("FthiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{E2867BC9-F369-4670-A797-8C944D58D663}", UserName));


            #endregion


            return View();
        }

        public ActionResult onAdd()
        {
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Mask();

            FactureTransportViewModel mclass = new FactureTransportViewModel();

            mclass._FactureTransport = new FactureTransport();
            mclass._Parametres = new Parametres();
            mclass._Parametres.fnGet();
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;                     

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFactureTransport", Model = mclass, };
        }

        public ActionResult onEdit(string ItemSelected)
        {
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Mask();

            FactureTransport mclass = JSON.Deserialize<FactureTransport>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            FactureTransportViewModel viewmodel = new FactureTransportViewModel();
            viewmodel._FactureTransport = mclass;            
            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFactureTransport", Model = mclass, };
        }

        public ActionResult onConsult(string ItemSelected)
        {
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Mask();

            FactureTransport mclass = JSON.Deserialize<FactureTransport>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            FactureTransportViewModel viewmodel = new FactureTransportViewModel();
            viewmodel._FactureTransport = mclass;            
            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFactureTransportDetail", Model = viewmodel, };
        }

        public ActionResult onViewPendindFactureTransport()
        {
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Mask();
            

            FactureTransportViewModel viewmodel = new FactureTransportViewModel();                       
            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Default;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPendingFactureTransport", Model = viewmodel, };
        }

        public ActionResult SubmitFormMethod(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                FactureTransport facturetransport = new FactureTransport();
                FactureTransportTransaction FactureTransporttransaction;

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("FactureTransportHiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    facturetransport.IsNew = true;
                else
                {
                    facturetransport.IsNew = false;

                    facturetransport.fnGet(Guid.Parse(GetFormValue("TxtFactureTransportID")));

                    if (facturetransport == null || facturetransport.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Facture Transport load failed.");
                }

                bool result = false;
                bool resulttransact = true;
                facturetransport = MapFormToObject(facturetransport);

                _db = facturetransport.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);                
                
                List<FactureDeduction> ItemFactureDeduction = JSON.Deserialize<List<FactureDeduction>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                if (ItemFactureDeduction.Count > 0)
                {
                    decimal totalamount = 0;

                    foreach (var item in ItemFactureDeduction)
                    {
                        totalamount += item.Montant;
                    }
                    //Enlever les dedcutions sur le total
                    facturetransport.Montant = totalamount - facturetransport.Deduction;

                    result = facturetransport.fnUpdate(mtran);

                    if (result)
                    {
                        for (int i = 0; i < ItemFactureDeduction.Count; i++)
                        {
                            FactureTransporttransaction = new FactureTransportTransaction();
                            FactureTransporttransaction.FactureTransport = new FactureTransport();

                            FactureTransporttransaction.SetDataSource(_db);

                            FactureTransporttransaction.FactureTransport.ID = facturetransport.ID;
                            FactureTransporttransaction.ElementID = ItemFactureDeduction[i].ID;
                            //FactureTransporttransaction.ElementRef = ItemFactureDeduction[i].ElementRef;
                            FactureTransporttransaction.ElementSolde = ItemFactureDeduction[i].Montant;
                            FactureTransporttransaction.ElementTonnage = ItemFactureDeduction[i].Tonnage;

                            FactureTransporttransaction.UtilisateurCreation = (string)Session["userName"];
                            FactureTransporttransaction.UtilisateurModification = (string)Session["userName"];

                            if (FactureTransporttransaction.IsNew)
                            {
                                resulttransact = FactureTransporttransaction.fnUpdate(mtran);
                            }

                            if (!resulttransact)
                            {
                                break;
                            }
                        }

                        if (!resulttransact)
                        {
                            _db.RollBackTransaction(mtran);
                        }
                        _db.CommitTransaction(mtran);
                    }
                }

                if (result && resulttransact)
                {
                    Store mstore = X.GetCmp<Store>("storeListeFactureTransport");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, facturetransport);
                        X.GetCmp<RowSelectionModel>("rowSelectionListeFactureTransport").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(facturetransport.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(facturetransport);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormFactureTransport").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture Transport : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelFactureTransport");            
            mform.ToggleCollapse();            
            return this.Direct();
        }

        public ActionResult onRefresh(string ItemCampagne, string ItemTransporteur, string ItemPeriodStart, string ItemPeriodEnd)
        {
            try
            {
                Store mstore = X.GetCmp<Store>("storeListeFactureTransport");

                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemCampagne"   ,ItemCampagne),
                                new Ext.Net.Parameter("ItemTransporteur"   ,ItemTransporteur),
                                new Ext.Net.Parameter("ItemPeriodStart"     ,ItemPeriodStart),
                                new Ext.Net.Parameter("ItemPeriodEnd"           ,ItemPeriodEnd),
                            });
                FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelFactureTransport");

                mform.Collapsed = true;
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture Transport : Data Validation",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });                
            }            

            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne, string ItemTransporteur, string ItemPeriodStart, string ItemPeriodEnd)
        {
            string CropYearID = string.IsNullOrEmpty(ItemCampagne) ? "" : ItemCampagne;
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                CropYearID = "";
            }

            int fournisseurID = string.IsNullOrEmpty(ItemTransporteur) ? -1 : GetCritriaValue(ItemTransporteur);           

            DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? DateTime.Now : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? DateTime.Now : DateTime.Parse(ItemPeriodEnd.ToString());
            
            var mListe = (new FactureTransport()).fnSelect(CropYearID, fournisseurID, startdate, enddate);

            // Paging
            int start = parameters.Start;

            int limit = parameters.Limit;

            if ((start + limit) > mListe.Count)
            {
                limit = mListe.Count - start;
            }

            List<DataPersist> rangePlants = (start < 0 || limit < 0) ? mListe : mListe.GetRange(start, limit);
            return this.Store(new Paging<DataPersist>(rangePlants, mListe.Count));


        }

        public ActionResult SelectDetailsFactureTransportItems(string factureTransportID)
        {
            Guid ID = Guid.Empty;
            if (!string.IsNullOrEmpty(factureTransportID))
                ID = Guid.Parse(factureTransportID);
            else return this.Direct();

            var mListe = (new FactureDeduction()).fnSelectDetailForFactureTransport(ID);
            return this.Store(mListe);
        }

        public ActionResult SearchFactureTransportByNumber(string ItemNumber)
        {
            if (!string.IsNullOrEmpty(ItemNumber))
            {
                FactureTransport mFactureTransport = new FactureTransport();

                mFactureTransport.fnSelectByNumber(ItemNumber);
                if (mFactureTransport.ID != Guid.Empty)
                {
                    Store mstore = X.GetCmp<Store>("storeListeFactureTransport");
                    mstore.RemoveAll();
                    mstore.Insert(0, mFactureTransport);
                }
                else
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Facture Transport",
                        Message = "Facture Transport not found, Please Verify Number and Retry !",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                }                
                
            }
            
            return this.Direct();

        }

        public ActionResult SelectFactureTransportItems(string ItemTransporteur)
        {
            List<DataPersist> mList = new List<DataPersist>();
            
            int transporteurID = 0;

            if (!string.IsNullOrEmpty(ItemTransporteur))
            {                
                transporteurID = int.Parse(ItemTransporteur);

                mList = new FactureDeduction().fnSelectByTransporteur(transporteurID);

            }

            return this.Store(mList);
        }

        

        public ActionResult OnPrintTransportVoucher(string ItemSelected, string IsCopy)
        {
            bool ReportIscopy = false;
            if (string.IsNullOrEmpty(IsCopy))
                ReportIscopy = true;

            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            //string param = JSON.Serialize(contratperiode);
            Session["ParamReport"] = ItemSelected;

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Voucher{0}', '{1}/FactureTransport/ViewVoucher?IsCopy={2}', this, 'Voucher','')", Guid.NewGuid(), BaseUrl, ReportIscopy));
        }

        public ActionResult ViewVoucher(bool IsCopy)
        {
            //XtraReport report = null;

            Payement payement = JSON.Deserialize<Payement>((string)Session["ParamReport"], new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            rptVoucherTransport report = new rptVoucherTransport();            
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["Number"].Value = payement.Numero;
            report.Parameters["AmountInLetter"].Value = MoneySpeller.ToLettres(Int32.Parse(payement.Montant.ToString()));
            report.Parameters["IsCopy"].Value = IsCopy;

            ViewData["Report"] = report;

            return View();
        }


        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                FactureTransport mClass = JSON.Deserialize<FactureTransport>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = mClass.fnGet(mClass.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Facture Transport loading failed.");

                if (mClass.Desactive)
                    result = mClass.fnActivate();
                else
                    result = mClass.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Facture Transport Approval failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeFactureTransport");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture Transport : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        private FactureTransport MapFormToObject(FactureTransport mClass)
        {
            mClass.Campagne = new Campagne();
            Parametres param = new Parametres();
            param.fnGet();
            //mClass.Campagne.Designation = X.GetCmp<ComboBox>("CmbFactureTransportCampagneID").SelectedItem.Value;
            mClass.Campagne.Designation = param.Campagne;

            mClass.Transporteur = new Transporteur();
            mClass.Transporteur.ID = int.Parse(X.GetCmp<ComboBox>("CmbFactureTransportTransporteurID").SelectedItem.Value);
            mClass.Transporteur.Nom = X.GetCmp<ComboBox>("CmbFactureTransportTransporteurID").SelectedItem.Text;

            mClass.PayementMode = new PayementMode();
            mClass.PayementMode.ID = int.Parse(X.GetCmp<ComboBox>("CmbFactureTransportPayementModeID").SelectedItem.Value);
            mClass.PayementMode.Designation = X.GetCmp<ComboBox>("CmbFactureTransportPayementModeID").SelectedItem.Text;

            mClass.Numero = X.GetCmp<Hidden>("hiddenNumeroFactureTransport").Text;
            mClass.DateFactureTransport = DateTime.Parse(X.GetCmp<DateField>("TxtFactureTransportDate").RawText.ToString() + " " + GetFormValue("TxtFactureTransportDateTime")) ;
            //mClass.Montant = decimal.Parse(X.GetCmp<TextField>("TxtTotalFactureTransport").Text); 
            mClass.Deduction = string.IsNullOrEmpty(X.GetCmp<TextField>("TxtDeductionFactureTransport").Text) ? 0 : decimal.Parse(X.GetCmp<TextField>("TxtDeductionFactureTransport").Text); 
            mClass.Tonnage = decimal.Parse(X.GetCmp<TextField>("hiddenFactureTransportTotalTonnage").Text);
            //mClass.Commentaire = X.GetCmp<TextField>("TxtCommentaireFactureTransport").Text;
            mClass.Statut = "AP";

            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }

        public ActionResult SetAmountToLetter(string Amount)
        {
            X.GetCmp<TextField>("TxtTotalFactureTransportInLetter").SetValue("");
            if (Amount != "0")
            {
                string val = MoneySpeller.ToLettres(Int32.Parse(Amount.Replace(" ", "")));
                X.GetCmp<TextField>("TxtTotalFactureTransportInLetter").SetValue(val);
                //string val = MoneySpeller.NumberToWords(Int32.Parse(Amount.Replace(" ", "")));

                //X.GetCmp<TextField>("TxtTotalFactureTransportInLetter").SetValue(val + " F CFA");
            }            

            return this.Direct();
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
            X.GetCmp<RowSelectionModel>("rowSelectionFactureTransport").DeselectAll();
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

        public ActionResult OnDisplayInvoiceList()
        {

            ViewData["Titre"] = "Print List Of Facture Transports";
            ViewData["actionToDo"] = "OnPrintTransportInvoiceList";
            ViewData["ControllerName"] = "FactureTransport";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmCriteriaForTransportInvoice", ViewData = ViewData };

        }

        public ActionResult OnPrintTransportInvoiceList(string cropyear,string transporteur, string transporteurText, string startDate, string endDate)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                Session["paramCampagne"] = cropyear;
                Session["paramTransporteur"] = transporteur;
                Session["paramTransporteurText"] = transporteurText;                

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/FactureTransport/ViewReportListResult', this, 'List Of Facture Transports',''),App.frmCriteriaForTransportInvoice.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture Transport : Data Validation",
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

            rptTransportInvoiceList report = new rptTransportInvoiceList();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramCampagneText"].Value = Session["paramCampagne"];
            if (!string.IsNullOrEmpty(Session["paramCampagne"].ToString()) && Session["paramCampagne"].ToString() == "{Tous}")
                report.Parameters["paramCampagne"].Value = string.Empty;
            else
                report.Parameters["paramCampagne"].Value = Session["paramCampagne"];

            report.Parameters["paramTransporteur"].Value = Session["paramTransporteur"];
            report.Parameters["paramTransporteurText"].Value = Session["paramTransporteurText"];            

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];
            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

    }
}