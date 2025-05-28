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
using DevExpress.XtraReports.UI;
using Tms.Classes.Security;
using System.Globalization;

namespace Tms2017.MVC.Controllers
{
    public class FinancementSiteController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";


        const string Balance = "Balance";
        const string Execution = "Execution";
        const string Exposure = "Exposure";

        Guid mID;

        // GET: Financement
        public ActionResult Index()
        {
            string UserName = (string)Session["userName"];
            Parametres mclass = (new Parametres(0));

            //Parametres mclass = new Parametres();
            //mclass = mParam[0] as Parametres;

            //Campagne mCampagne = new Campagne();

            X.GetCmp<ComboBox>("cmbCampagne").SetValue(mclass.Campagne);

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("dtpStartDate").RawText = StartDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Fournisseur = "{Tous}";

            string Type = "{Tous}";            

            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;
            Fonction HasAccessFunction = new Fonction();
            bool HasAccessAllSite = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            ViewBag.HasAccessAllSite = HasAccessAllSite;

            X.GetCmp<FormPanel>("CriteriaPanelFI").SetTitle("Site : "+ mSiteParDefaut.Nom + ", Campagne : " + mclass.Campagne + " | Fournisseur : " + Fournisseur + " | Type : " + Type + " | Du : " + StartDate + " Au : " + EndDate);
            #region Set Function's Access

            //Fonction HasAccess = new Fonction();
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{B8EC41D0-173A-47A6-BB9A-140942CE66B8}", UserName);

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{f8086753-3900-4b1e-9ce9-c50f06842b37}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{893e360d-28c1-43f2-82e4-b5a82672cd5f}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{0db0c35a-9498-43ae-ad14-eb83de1c7ec4}")))
                X.GetCmp<MenuItem>("mnuExport").Enable();
            else
                X.GetCmp<MenuItem>("mnuExport").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{1e5c8755-c4cc-4cd8-9814-7eb36b61ff50}")))
                X.GetCmp<MenuItem>("mnuPrintExecution").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintExecution").Disable();

            //if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{f9d03fcf-ff9b-4d81-a61a-41f448c12251}")))
            //    X.GetCmp<MenuItem>("mnuPrintExposure").Enable();
            //else
            //    X.GetCmp<MenuItem>("mnuPrintExposure").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{4c8bc617-8d2e-4b2a-922d-81a30755cdbf}")))
                X.GetCmp<MenuItem>("mnuPrintFinancementList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintFinancementList").Disable();

            X.GetCmp<Hidden>("fihiddenPermCreer").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{893e360d-28c1-43f2-82e4-b5a82672cd5f}")));
            X.GetCmp<Hidden>("fihiddenPermModifier").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{e51651fb-735f-477c-a922-31eb5bff29b7}")));
            X.GetCmp<Hidden>("fihiddenPermDesactiver").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{b936283c-3029-401b-aa14-a59a41771c2d}")));
            X.GetCmp<Hidden>("fihiddenPermExporterExcel").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{0db0c35a-9498-43ae-ad14-eb83de1c7ec4}")));
            X.GetCmp<Hidden>("fihiddenPermSetToLoss").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{031313e1-d480-452f-9f70-19e1ba637ca6}")));
            X.GetCmp<Hidden>("fihiddenPermPrintBalance").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{478bf568-9dee-4ede-ba1f-cae4aa414eff}")));
            X.GetCmp<Hidden>("fihiddenPermPrintExecution").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{1e5c8755-c4cc-4cd8-9814-7eb36b61ff50}")));
            //X.GetCmp<Hidden>("fihiddenPermPrintExposure").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{1e1465a6-0683-4b58-8d1c-716fa72f8353}")));
            //X.GetCmp<Hidden>("fihiddenPermPrintAgrement").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{d5c24a74-6070-4544-a577-d2d2ec8e4d49}")));
            X.GetCmp<Hidden>("fihiddenPermOverview").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{fe8a8a2f-b148-484b-91fc-00e543d86b7c}")));
            X.GetCmp<Hidden>("fihiddenPermAddManualRefund").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{9dddf74e-fe8d-4ae7-8150-e022fe580e53}")));
            X.GetCmp<Hidden>("fihiddenPermRemoveManualRefund").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{ad4b12e9-47cf-45f3-b811-ed0f1634e5e8}")));
            X.GetCmp<Hidden>("fihiddenPermApproveManualRefund").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{38f6dca8-2925-4149-96eb-72f8ec720049}")));
            #endregion

            return View();
        }

        public ActionResult Index_Approval()
        {
            var mParam = (new Parametres()).fnSelect();

            Parametres mclass = new Parametres();
            mclass = mParam[0] as Parametres;

            Campagne mCampagne = new Campagne();

            X.GetCmp<ComboBox>("cmbCampagne").SetValue(mclass.Campagne);

            string Fournisseur = "{Tous}";

            X.GetCmp<FormPanel>("CriteriaPanelFAP").SetTitle("Campagne : " + mclass.Campagne + " | Fournisseur : " + Fournisseur);

            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            
            if (HasAccess.fnGetUserAccessStatus("{5E42CD14-A4B0-49CF-A4AC-9FF5BBEAB338}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportApprovedFinancing").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportApprovedFinancing").Enable();

            if (HasAccess.fnGetUserAccessStatus("{EB726BAC-EBB5-42C2-83CF-1E22D028869E}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintBalance").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintBalance").Enable();

            if (HasAccess.fnGetUserAccessStatus("{757D2863-9A36-4876-853A-D653F89C9152}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintExecution").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintExecution").Enable();

            if (HasAccess.fnGetUserAccessStatus("{DC3E3822-8094-4B0D-A87F-BDCFE83AAA3F}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintExposure").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintExposure").Enable();

            if (HasAccess.fnGetUserAccessStatus("{EFBE8AA0-A20B-4CDE-B5C7-86EFECC41EA3}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintFinancementApvList").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintFinancementApvList").Enable();

            X.GetCmp<Hidden>("fihiddenPermApprove").SetValue(HasAccess.fnGetUserAccessStatus("{757DC1F0-B08D-428F-96F3-902AF6E6ADF6}", UserName));
            X.GetCmp<Hidden>("fihiddenPermReject").SetValue(HasAccess.fnGetUserAccessStatus("{9A65E592-8B93-4C46-AFBD-E0BF77AF7A8A}", UserName));
            X.GetCmp<Hidden>("fihiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{5E42CD14-A4B0-49CF-A4AC-9FF5BBEAB338}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintBalance").SetValue(HasAccess.fnGetUserAccessStatus("{EB726BAC-EBB5-42C2-83CF-1E22D028869E}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintExecution").SetValue(HasAccess.fnGetUserAccessStatus("{757D2863-9A36-4876-853A-D653F89C9152}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintExposure").SetValue(HasAccess.fnGetUserAccessStatus("{DC3E3822-8094-4B0D-A87F-BDCFE83AAA3F}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintFinancialStatus").SetValue(HasAccess.fnGetUserAccessStatus("{1232A21B-4D18-40AC-AB36-A6F3FFA36906}", UserName));
            X.GetCmp<Hidden>("fihiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{D10DCEFC-0440-43CE-9F3F-B8769CA51D12}", UserName));
            #endregion

            return View();
        }                

        public ActionResult LoadListOfFinancing(StoreRequestParameters parameters, string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite, string ItemOption)
        {

            int FournisseurID = GetCriteriaValue(ItemFournisseur);
            int TypeID = GetCriteriaValue(ItemType);
            int SiteID = GetCriteriaValue(ItemSite);
            string optionID = string.IsNullOrEmpty(ItemOption) ? "NO" : ItemOption;
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
           
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "{Tous}";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string Status = ItemStatus;
            
            var mListe = (new Financement()).fnSelect(Campagne, FournisseurID, TypeID, StartDate, EndDate, Status, SiteID, optionID);
            //var paging = GridStorePaging.SetRangePlants(parameters, mListe);

            //return this.Store(paging);
            return this.Store(mListe);
        }

        //public ActionResult LoadListOfFinancingToApprove(StoreRequestParameters parameters, string ItemCampagne, string ItemFournisseur)
        //{

        //    int FournisseurID = GetCriteriaValue(ItemFournisseur);
            
        //    string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;

        //    if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
        //    {
        //        Campagne = "{Tous}";
        //    }

        //    var mListe = (new Financement()).fnSelectForApproval(Campagne, FournisseurID,);
        //    //var paging = GridStorePaging.SetRangePlants(parameters, mListe);

        //    return this.Store(mListe);
        //}

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelFI");
            mform.ToggleCollapse();            
            return this.Direct();
        }

        public ActionResult OnFilterFAP()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelFAP");

            mform.ToggleCollapse();
            //mform.Collapsed = false;
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite, string ItemOption)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeFinancement");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemCampagne"      ,ItemCampagne),
                                    new Ext.Net.Parameter("ItemType"          ,ItemType),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus),
                                    new Ext.Net.Parameter("ItemSite"        ,ItemSite),
                                    new Ext.Net.Parameter("ItemOption"        ,ItemOption)
                                });

                string title = X.GetCmp<FormPanel>("CriteriaPanelFI").Title;
                title += "Site = " + X.GetCmp<ComboBox>("cmbSite").SelectedItem.Text;
                title += ", Campagne : " + X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                title += ", Fournisseur : " + X.GetCmp<ComboBox>("cmbFournisseur").SelectedItem.Text;

                title += ", Type = " + X.GetCmp<ComboBox>("cmbType").SelectedItem.Text;

                title += ", From " + X.GetCmp<DateField>("dtpStartDate").RawText.ToString() + "to " + X.GetCmp<DateField>("dtpEndDate").RawText.ToString();

                X.GetCmp<FormPanel>("CriteriaPanelFI").Title = title;

                // collapse criterias areas
                X.GetCmp<FormPanel>("CriteriaPanelFI").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Financement : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
                        
            return this.Direct();
        }

        public ActionResult OnRefreshForApproval(string ItemCampagne, string ItemFournisseur)
        {
            Store mstore = X.GetCmp<Store>("storeListeFinancingApproval");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemCampagne"      ,ItemCampagne),
                                   
                                });

          
            // collapse criterias areas
            X.GetCmp<FormPanel>("CriteriaPanel").Collapse(Direction.Top, false);

            return this.Direct();
        }

        private Financement MapFormToObject(Financement mClass)
        {
            mClass.Campagne = X.GetCmp<ComboBox>("_cmbCampagne").Text;

            mClass.Numero = X.GetCmp<Hidden>("hiddenNumero").Text;
            FinancementType mType = new FinancementType();
            mType.ID = int.Parse(GetFormValue("_cmbType"));
            mType.Designation = X.GetCmp<ComboBox>("_cmbType").SelectedItem.Text.ToString();
            mClass.FinancementType = mType;

            Fournisseur mSupplier = new Fournisseur();
            mSupplier.ID = int.Parse(GetFormValue("_cmbFournisseur"));
            mSupplier.Nom = X.GetCmp<ComboBox>("_cmbFournisseur").SelectedItem.Text.ToString();
            mClass.Fournisseur = mSupplier;

            PrelevementMode mPrev = new PrelevementMode();
            mPrev.ID = int.Parse(GetFormValue("cmbPrelevementType"));
            mPrev.Designation = X.GetCmp<ComboBox>("cmbPrelevementType").SelectedItem.Text.ToString();
            mClass.PrelevementMode = mPrev;

            mClass.DateFinancement = DateTime.Parse(X.GetCmp<DateField>("txtDate").RawText.ToString());
            mClass.DateEcheance = DateTime.Parse(X.GetCmp<DateField>("txtDueDate").RawText.ToString());
            if (X.GetCmp<TextField>("txtPrice").Text != string.Empty) mClass.Prix = decimal.Parse(X.GetCmp<TextField>("txtPrice").Text);
            if (X.GetCmp<TextField>("txtAmount").Text != string.Empty) mClass.Montant = decimal.Parse(X.GetCmp<TextField>("txtAmount").Text);
            if (X.GetCmp<TextField>("txtTonnage").Text != string.Empty) mClass.Tonnage = decimal.Parse(X.GetCmp<TextField>("txtTonnage").Text);
            if (X.GetCmp<TextField>("txtTauxRemboursement").Text != string.Empty) mClass.PrelevementTaux = decimal.Parse(X.GetCmp<TextField>("txtTauxRemboursement").Text);

            if (X.GetCmp<TextField>("txtTauxPaiement").Text != string.Empty) mClass.PaymentTaux = decimal.Parse(X.GetCmp<TextField>("txtTauxPaiement").Text);
            if (X.GetCmp<TextField>("txtGrossAmount").Text != string.Empty) mClass.MontantBrut = decimal.Parse(X.GetCmp<TextField>("txtGrossAmount").Text);

            mClass.Commentaire = X.GetCmp<TextField>("txtComment").Text;
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];
            
            mClass.Solde = mClass.IsActive ? mClass.Montant : mClass.Solde;

            return mClass;
            
        }       

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
            X.GetCmp<RowSelectionModel>("rowSelectionListeFinancement").DeselectAll();
        }

        public ActionResult OnPrintAgreement(string IdAvance)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Agreement{0}', '{1}/Financement/ViewAgreementReport?id={0}', this, 'Protocol Accord','')", IdAvance, BaseUrl));
        }

        public ActionResult OnPrintFinancingReport(string ReportType, string ItemPeriodStart = "", string ItemPeriodEnd = "")
        {
            FinancementViewModel mclass = new FinancementViewModel();
            try
            {
                if (string.IsNullOrEmpty(ItemPeriodStart)) ItemPeriodStart = DateTime.Now.ToShortDateString();
                if (string.IsNullOrEmpty(ItemPeriodEnd)) ItemPeriodEnd = DateTime.Now.ToShortDateString();

                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);
                mclass._Financement = new Financement();
                mclass._Financement.DateFinancement = datedebut;
                mclass._Financement.DateEcheance = datedfin;
                mclass._Financement.Sites = new Site();
                //mclass._Financement.Campagne = new Campagne();                        

                //var mParam = (new Parametres()).fnSelect();
                Parametres parametre = new Parametres(0);
                string UserName = (string)Session["userName"];
                Site mSiteParDefaut = new Site();
                bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

                if (result)
                    mclass._Financement.Sites.ID = mSiteParDefaut.ID;

                mclass._Financement.Campagne = parametre.Campagne;
                switch (ReportType)
                {
                    case FinancementReportType.Balance:
                        mclass._ReportType = FinancementReportType.Balance;
                        ViewData["ReportTitle"] = "Situation Des Financements - Etat";
                        ViewData["CanFilterByPeriod"] = true;
                        break;
                    case FinancementReportType.Execution:
                        mclass._ReportType = FinancementReportType.Execution;
                        ViewData["ReportTitle"] = "Execution Des Financements - Etat";
                        ViewData["CanFilterByPeriod"] = true;
                        break;
                    case FinancementReportType.Exposure:
                        mclass._ReportType = FinancementReportType.Exposure;
                        ViewData["ReportTitle"] = "Risque Sur financement - Etat";
                        ViewData["CanFilterByPeriod"] = false;
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintFinancingReport", Model = mclass };
        }

        public ActionResult GenerateReport(string TypeReport)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            Financement ficlass = new Financement();

            //contratperiode.Campagne = new Campagne();
            ficlass.Campagne = GetFormValue("FIPcropYearForReport");

            ficlass.Fournisseur = new Fournisseur();
            ficlass.Fournisseur.ID = int.Parse(GetFormValue("FIfournisseurForReport"));
            ficlass.Fournisseur.Nom = X.GetCmp<ComboBox>("FIfournisseurForReport").SelectedItem.Text;

            ficlass.FinancementType = new FinancementType();
            ficlass.FinancementType.ID = int.Parse(GetFormValue("FITypeFinancementForReport"));
            ficlass.FinancementType.Designation = X.GetCmp<ComboBox>("FITypeFinancementForReport").SelectedItem.Text;

            ficlass.DateFinancement = DateTime.Parse(X.GetCmp<DateField>("FIstartDateForReport").RawText.ToString());
            ficlass.DateEcheance = DateTime.Parse(X.GetCmp<DateField>("FIdueDateForReport").RawText.ToString());
            ficlass.Statut = X.GetCmp<ComboBox>("FIOptionReport").SelectedItem.Value.ToString();

            ficlass.Sites = new Site();
            ficlass.Sites.ID = int.Parse(GetFormValue("FISiteForReport"));
            ficlass.Sites.Nom = X.GetCmp<ComboBox>("FISiteForReport").SelectedItem.Text;

            X.Js.Call("CloseWindow");

            string param = JSON.Serialize(ficlass);
            Session["ParamReport"] = param;
            string ReportTitle = string.Empty;
            switch (TypeReport)
            {
                case FinancementReportType.Balance:
                    ReportTitle = "Situation Des Financements";
                    break;
                case FinancementReportType.Execution:
                    ReportTitle = "Execution Des Financements";
                    break;
                case FinancementReportType.Exposure:
                    ReportTitle = "Risque Sur financement";
                    break;

                case FinancementReportType.FinancialStatus:
                    ReportTitle = "Position Financière";
                    break;
            }

            //HttpUtility.JavaScriptStringEncode(param)
            return JavaScript(String.Format("CloseWindow(), addTab(window.parent.Ext.getCmp('tabCenter'), 'FinancingReport{0}', '{1}/FinancementSite/ViewReport?TypeReport={2}', this, '{3}','')", Guid.NewGuid(), BaseUrl, TypeReport, ReportTitle));
        }

        public ActionResult ViewAgreementReport(string id)
        {
            rptFinancingAgreement report = new rptFinancingAgreement();
            Financement mFinancement = new Financement(Guid.Parse(id));

            report.DataSource = DevExpressReportDs.SetDataSource(report);
            report.Parameters["FinancementID"].Value = id;
            report.Parameters["MontantEnLettre"].Value = MoneySpeller.ToLettres(Int32.Parse(mFinancement.Montant.ToString()));

            ViewData["Report"] = report;

            return View();
        }


        public ActionResult ViewReport(string TypeReport)
        {
            XtraReport report = new XtraReport();
            switch (TypeReport)
            {
                case FinancementReportType.Balance:
                    report = new rptFinancingBalance() as XtraReport;
                    break;
                case FinancementReportType.Execution:
                    report = new rptFinancingExecution() as XtraReport;
                    break;
                case FinancementReportType.Exposure:
                    report = new rptFinancingExposure() as XtraReport;
                    break;

                case FinancementReportType.FinancialStatus:
                    report = new rptSupplierFinancialStatus() as XtraReport;
                    break;
            }
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            Financement financement = JSON.Deserialize<Financement>((string)Session["ParamReport"], new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            if (TypeReport == FinancementReportType.Exposure)
            {
                report.Parameters["campagneID"].Value = financement.Campagne;
                report.Parameters["fournisseurID"].Value = financement.Fournisseur.ID;
                report.Parameters["NomFournisseur"].Value = financement.Fournisseur.Nom;
                report.Parameters["siteID"].Value = financement.Sites.ID;
                report.Parameters["siteNom"].Value = financement.Sites.Nom;

                ViewData["Report"] = report;

                return View();
            }

            report.Parameters["campagneID"].Value = financement.Campagne;
            report.Parameters["fournisseurID"].Value = financement.Fournisseur.ID;


            if (TypeReport != FinancementReportType.FinancialStatus)
            {
                report.Parameters["FinancementTypeID"].Value = financement.FinancementType.ID;
                report.Parameters["DateDebut"].Value = financement.DateFinancement;
                report.Parameters["DateFin"].Value = financement.DateEcheance;
                report.Parameters["Status"].Value = financement.Statut;

                //Afficher Les criteres sur l'etat
                report.Parameters["NomFournisseur"].Value = financement.Fournisseur.Nom;
                //report.Parameters["fournisseurNom"].Value = contrat.Statut.Nom;
                report.Parameters["FinancementTypeNom"].Value = financement.FinancementType.Designation;
                report.Parameters["OptionLibelle"].Value = financement.Statut;
            }
            else
            {
                report.Parameters["NomFournisseur"].Value = financement.Fournisseur.Nom;
            }
            report.Parameters["siteID"].Value = financement.Sites.ID;
            report.Parameters["siteNom"].Value = financement.Sites.Nom;
            ViewData["Report"] = report;

            return View();
        }

        public ActionResult OnDisplayFinancingList(string ItemPeriodStart = "", string ItemPeriodEnd = "")
        {
            Financement mFinancement = new Financement();
            try
            {
                if (string.IsNullOrEmpty(ItemPeriodStart)) ItemPeriodStart = DateTime.Now.ToShortDateString();
                if (string.IsNullOrEmpty(ItemPeriodEnd)) ItemPeriodEnd = DateTime.Now.ToShortDateString();

                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                mFinancement.DateFinancement = datedebut;
                mFinancement.DateEcheance = datedfin;

                ViewData["Titre"] = "Liste des Financements";
                ViewData["actionToDo"] = "OnPrintFinancingList";
                ViewData["ControllerName"] = "Financement";

                mFinancement.Sites = new Site();
                Site mSiteParDefaut = new Site();
                string UserName = (string)Session["userName"];
                bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

                if (result)
                    mFinancement.Sites.ID = mSiteParDefaut.ID;
            }
            catch (Exception)
            {

                throw;
            }

            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmCriteriaForFinancing", Model = mFinancement, ViewData = ViewData };
        }



        public ActionResult OnPrintFinancingList(string cropyear, string fournisseur, string fournisseurText, string typefinancement, string typefinancementText, string startDate, string endDate, string statut, string statutText)
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
                Session["paramTypeOfFinancing"] = typefinancement;
                Session["paramTypeOfFinancingText"] = typefinancementText;
                Session["paramStatut"] = statut;
                Session["paramStatutText"] = statutText;

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;

                Session["paramOption"] = X.GetCmp<ComboBox>("cmbDetOption").SelectedItem.Value.ToString();
                Session["paramOptionText"] = X.GetCmp<ComboBox>("cmbDetOption").SelectedItem.Text;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Financement/ViewReportResult', this, 'Liste Des Financements',''),App.frmCriteriaForFinancing.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Financement : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }


        }

        public ActionResult ViewReportResult()
        {
            //XtraReport report = null;

            rptFinancingList report = new rptFinancingList();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramCampagneText"].Value = Session["paramCampagne"];
            report.Parameters["paramCampagne"].Value = Session["paramCampagne"];
            //if (!string.IsNullOrEmpty(Session["paramCampagne"].ToString()) && Session["paramCampagne"].ToString() == "{Tous}")
            //    report.Parameters["paramCampagne"].Value = string.Empty;
            //else
            //    report.Parameters["paramCampagne"].Value = Session["paramCampagne"];

            report.Parameters["paramFournisseur"].Value = Session["paramFournisseur"];
            report.Parameters["paramFournisseurText"].Value = Session["paramFournisseurText"];

            report.Parameters["paramTypeOfFinancing"].Value = Session["paramTypeOfFinancing"];
            report.Parameters["paramTypeOfFinancingText"].Value = Session["paramTypeOfFinancingText"];

            report.Parameters["paramStatut"].Value = Session["paramStatut"];
            report.Parameters["paramStatutText"].Value = Session["paramStatutText"];

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];
            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            report.Parameters["paramOption"].Value = Session["paramOption"];
            report.Parameters["paramOptionText"].Value = Session["paramOptionText"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

        public ActionResult OnDisplayFinancingApvlList()
        {
            ViewData["Titre"] = "Approbation - Financement";
            ViewData["actionToDo"] = "OnPrintFinancingApvlList";
            ViewData["ControllerName"] = "Financement";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmCriteriaForFinApproval", ViewData = ViewData };
        }

        public ActionResult OnPrintFinancingApvlList(string cropyear, string fournisseur, string fournisseurText)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
               
                //Session["paramCropYear"] = cropYear;                
                Session["paramCampagne"] = cropyear;
                Session["paramFournisseur"] = fournisseur;
                Session["paramFournisseurText"] = fournisseurText;
                
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Financement/ViewReportApvlResult', this, 'Approbation - Financement',''),App.frmCriteriaForFinApproval.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Financing - Approval : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
        }

        public ActionResult ViewReportApvlResult()
        {
            //XtraReport report = null;

            rptFinancingApprovalList report = new rptFinancingApprovalList();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramCampagneText"].Value = Session["paramCampagne"];
            if (!string.IsNullOrEmpty(Session["paramCampagne"].ToString()) && Session["paramCampagne"].ToString() == "{Tous}")
                report.Parameters["paramCampagne"].Value = string.Empty;
            else
                report.Parameters["paramCampagne"].Value = Session["paramCampagne"];

            report.Parameters["paramFournisseur"].Value = Session["paramFournisseur"];
            report.Parameters["paramFournisseurText"].Value = Session["paramFournisseurText"];            

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

    }
}
