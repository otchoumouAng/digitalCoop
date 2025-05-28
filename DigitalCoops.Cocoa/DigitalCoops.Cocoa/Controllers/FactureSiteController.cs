using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ext.Net.MVC;
using Ext.Net;
using Tms.Classes.Business;
using Newtonsoft.Json;
using Tms.Classes.Shared;
using Tms.Components.Settings;
using Tms.Components.Data;
using Tms.Classes.Security;
using System.Globalization;
using Tms.LocalService;

namespace Tms2017.MVC.Controllers
{
    public class FactureSiteController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
                
        int mTypeDeductionAvanceID = new Parametres(0).FactureDeductionTypeAvance;
        int mTypeDeductionMecID = new Parametres(0).FactureDeductionTypeMec;
        int mTypeDeductionLegalTaxID = new Parametres(0).FactureDeductionTypeLegalTax;
         
        Guid mID;

        // GET: Facture
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            X.GetCmp<ComboBox>("cmbCampagne").SetValue(mParam.Campagne);

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("dtpStartDate").RawText = StartDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Fournisseur = "{Tous}";

            string Type = "{Tous}";

            X.GetCmp<FormPanel>("CriteriaPanelFA").SetTitle("Site : " + mSiteParDefaut.Nom + ", Campagne : " + mParam.Campagne + " | Fournisseur : " + Fournisseur + " | Type De Livraison : " + Type + " | Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{d3b9ee1a-8384-40f9-a1db-b41757056001}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();

            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{1C06EC8D-B389-4F49-A3DC-A0B6C080040C}", UserName);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{962d64a0-199c-4c7f-ba9f-fd512a61b7a0}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();
            
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{de02c8c4-a00c-4040-9258-32b8332a5135}")))
                X.GetCmp<MenuItem>("mnuExportInvoices").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportInvoices").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{9853497f-4f0f-4923-8c78-e16e01bfe4b1}")))
                X.GetCmp<MenuItem>("btnViewPendingDeliveries").Enable();
            else
                X.GetCmp<MenuItem>("btnViewPendingDeliveries").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{76abda31-dc71-428f-a0c1-d6b732382a31}")))
                X.GetCmp<MenuItem>("mnuPrintInvoiceList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintInvoiceList").Disable();

            //if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{3FCDBF84-CD48-4244-8344-3941818B4C1F}")))
            //    X.GetCmp<MenuItem>("mnuPrintValuationList").Disable();
            //else
            //    X.GetCmp<MenuItem>("mnuPrintValuationList").Enable();

            //if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{95710067-B6A4-46EE-886F-4BFAB2E2553C}")))
            //    X.GetCmp<MenuItem>("mnuPrintAvailableDeliveries").Disable();
            //else
            //    X.GetCmp<MenuItem>("mnuPrintAvailableDeliveries").Enable();

            X.GetCmp<Hidden>("FahiddenPermCreer").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{962d64a0-199c-4c7f-ba9f-fd512a61b7a0}")));
            X.GetCmp<Hidden>("FahiddenPermConsultPendingDeliveries").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{9853497f-4f0f-4923-8c78-e16e01bfe4b1}")));
            X.GetCmp<Hidden>("FahiddenPermDesactiver").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{f25b9086-d521-46fe-98c2-b64e3f0302bd}")));
            X.GetCmp<Hidden>("FahiddenPermPrintCopyOfInvoices").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{3da6e0ee-7967-473c-8e73-d48672e3ba7b}")));
            X.GetCmp<Hidden>("FahiddenPermExporterExcel").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{de02c8c4-a00c-4040-9258-32b8332a5135}")));
            X.GetCmp<Hidden>("FahiddenPermPrintListOfInvoices").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{76abda31-dc71-428f-a0c1-d6b732382a31}")));
            //X.GetCmp<Hidden>("FahiddenPermPrintListOfValuation").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{3FCDBF84-CD48-4244-8344-3941818B4C1F}")));
            X.GetCmp<Hidden>("FahiddenPermOverview").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{880d18ca-b838-4f13-ab89-8e98f042f5a7}")));

            #endregion

            return View();
        }                        

        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite)
        {

            int FournisseurID = GetCriteriaValue(ItemFournisseur);
            int TypeID = GetCriteriaValue(ItemType);
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
            int SiteID = GetCriteriaValue(ItemSite);

            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "{Tous}";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string Status = ItemStatus;
            //string Status = ItemStatus == "null" ? "%%" : ItemStatus;

            int siteFournisseur = -1;

            string UserName = (string)Session["userName"];
            Site mclass = new Site();
            bool result = mclass.fnGetByUserName(UserName);

            Parametres mParam = new Parametres(0);
            if (SiteID != mclass.ID)
            {
                if (SiteID == mParam.Site)
                {
                    SiteID = -2;
                    siteFournisseur = mclass.ID;
                }
                else
                {
                    SiteID = mclass.ID;
                    siteFournisseur = mclass.ID;
                }
            }


            var mListe = (new Facture()).fnSelectExtend(Campagne, FournisseurID, TypeID, StartDate, EndDate, Status, SiteID,siteFournisseur);
            string filterHeaders = this.Request.Params["filterheader"];
            //var paging = GridStorePaging.SetRangePlants(parameters, mListe);

            //return this.Store(GridStorePaging.SetRangePlants(parameters, mListe, filterHeaders));
            return this.Store(mListe);
        }        

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelFA");

            mform.ToggleCollapse();
            //mform.Collapsed = false;
            return this.Direct();
        }        

        public ActionResult OnRefresh(string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite)
        {
            try
            {
                Store mstore = X.GetCmp<Store>("storeListeFacture");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemCampagne"      ,ItemCampagne),
                                    new Ext.Net.Parameter("ItemType"          ,ItemType),
                                    new Ext.Net.Parameter("ItemSite"          ,ItemSite),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus)
                                });

                string title = X.GetCmp<FormPanel>("CriteriaPanelFA").Title;
                title += "Site = " + X.GetCmp<ComboBox>("cmbSite").SelectedItem.Text;

                title += ", Campagne : " + X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                title += ", Fournisseur : " + X.GetCmp<ComboBox>("cmbFournisseur").SelectedItem.Text;

                title += ", Type De Livraison = " + X.GetCmp<ComboBox>("cmbType").SelectedItem.Text;

                title += ", From " + X.GetCmp<DateField>("dtpStartDate").RawText.ToString() + "to " + X.GetCmp<DateField>("dtpEndDate").RawText.ToString();

                X.GetCmp<FormPanel>("CriteriaPanelFA").Title = title;

                // collapse criterias areas
                X.GetCmp<FormPanel>("CriteriaPanelFA").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Delivery Note : OnRefresh",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
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
                }
                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
            }
            catch (Exception ex)
            {
                Ext.Net.X.Msg.Alert("frm_Detail : GetFormExecMode", ex.Message).Show();
                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
            }
        }

        private int GetCriteriaValue(string strComponent)
        {
            int value;

            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
                return value;
            else
                return -1;
        }


        private int GetCriteriasValue(string strComponent)
        {
            if (X.GetCmp<ComboBox>(strComponent) != null && X.GetCmp<ComboBox>(strComponent).SelectedItem != null && X.GetCmp<ComboBox>(strComponent).SelectedItem.Value != null)
                return int.Parse(X.GetCmp<ComboBox>(strComponent).SelectedItem.Value.ToString());
            else
                return -1;
        }

        private void DeselectGridRows()
        {
            //X.Js.Call("App.rowSelectionListe.deselectAll();");
            X.GetCmp<RowSelectionModel>("rowSelectionDeduction").DeselectAll();
        }

        public ActionResult OnPrintInvoicePPR(string IdFacture, string IsCopy)
        {
            bool ReportIscopy = false;
            if (string.IsNullOrEmpty(IsCopy))
                ReportIscopy = true;
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'PPR{0}', '{1}/FactureSite/ViewReport?id={0}&IsCopy={2}', this, 'Facture','')", IdFacture, BaseUrl, ReportIscopy));
        }

        public ActionResult ViewReport(string id, bool IsCopy)
        {
            try
            {
                rptInvoicePPR report = new rptInvoicePPR();

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["ID"].Value = id;
                report.Parameters["IsCopy"].Value = IsCopy;

                ViewData["Report"] = report;
                //PrintMethod.Print(report);
                return View();
            }
            catch (Exception ex)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "PPR : Printing",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }

        }

        public ActionResult OnCancel(string ItemSelected)
        {

            try
            {
                Facture mClass = JSON.Deserialize<Facture>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);

                if (!result)
                    throw new Exception("OnCancel : Facture loading failed.");

                string UserName = (string)Session["userName"];
                Site mSiteParDefaut = new Site();

                result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

                if (mClass.Sites.ID != mSiteParDefaut.ID)
                    throw new Exception("OnCancel : You're not able to cancel this Item !");

                mClass.UtilisateurModification = (string)Session["userName"];

                if (mClass.fnCancel())
                {
                    Store mstore = X.GetCmp<Store>("storeListeFacture");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();

                    DeselectGridRows();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

    }
}
