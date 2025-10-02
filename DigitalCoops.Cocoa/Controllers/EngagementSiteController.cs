using DevExpress.Web.Mvc;
using DevExpress.XtraReports.UI;
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
using Tms.Classes.Business.Sites;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    //[RoutePrefix("ForwardContract")]
    public class EngagementSiteController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: EngagementSite
        //[Route]
        public ActionResult Index()
        {
            var mParam = (new Parametres()).fnSelect();

            Parametres mclass = new Parametres();
            mclass = mParam[0] as Parametres;
            ViewBag.EngagementType = mclass.IDEngagementType;
            Campagne mCampagne = new Campagne();
           
            X.GetCmp<ComboBox>("cmbCropYear").SetValue(mclass.Campagne);

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            Fonction HasAccessFunction = new Fonction();
            bool HasAccessAllSite = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            if (HasAccessAllSite) ViewBag.UrlFournisseur = "LoadFournisseursApproved";
            else ViewBag.UrlFournisseur = "LoadAllSupplierBySite";

            ViewBag.HasAccessAllSite = HasAccessAllSite;
            //X.GetCmp<Hidden>("defaultFinancingTypeID").SetValue(mclass.EngagementSiteTypeFinancement);
            //X.GetCmp<ComboBox>("cmbTypeContrat").SetValue(mclass.EngagementSiteType.ID);

            DateTime firstdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            
            DateTime lastDayOfMonth = firstdayofmonth.AddMonths(1).AddDays(-1);

            X.GetCmp<DateField>("TxtPeriodStart").RawText = firstdayofmonth.ToShortDateString();
            X.GetCmp<DateField>("TxtPeriodEnd").RawText = lastDayOfMonth.ToShortDateString();            
                
            X.GetCmp<FormPanel>("CriteriaPanelEngagement").SetTitle("Campagne : " + mclass.Campagne + " | Du : " + firstdayofmonth.ToShortDateString() + " Au : " + lastDayOfMonth.ToShortDateString());


            #region Set Function's Access

            //string UserName = (string)Session["userName"];            

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{829753e4-49e0-4437-8894-8d8c165be822}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{dfd31575-043b-420f-ab08-edafff78ca82}")))
                X.GetCmp<Button>("btnNewEngagement").Enable();
            else
                X.GetCmp<Button>("btnNewEngagement").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{e5ce5c40-b5d2-4678-9fda-8e3507bc5dcd}")))
                X.GetCmp<MenuItem>("mnuExportEngagementToExcel").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportEngagementToExcel").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{988f3001-eeab-4677-8f3c-2ee0b2fb50d1}")))
                X.GetCmp<MenuItem>("mnuPrintBalance").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintBalance").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{6be828a3-d164-4550-8d23-761f6a8db526}")))
                X.GetCmp<MenuItem>("mnuPrintExecution").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintExecution").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{ebede45a-6942-405b-9b5e-13cbe9b985ac}")))
                X.GetCmp<MenuItem>("mnuPrintEngagementList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintEngagementList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{499ab361-e8b3-4f39-a3b3-2d64db1831cb}")))
                X.GetCmp<MenuItem>("mnuPrintPrime").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintPrime").Disable();

            X.GetCmp<Hidden>("EghiddenPermCreer").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{dfd31575-043b-420f-ab08-edafff78ca82}")));
            X.GetCmp<Hidden>("EghiddenPermModifier").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{f7c7e4e4-832b-4f80-a4ef-20719e2786c6}")));
            X.GetCmp<Hidden>("EghiddenPermDesactiver").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{b6ddaa39-392a-47ee-b6ff-696fddf27995}")));
            //X.GetCmp<Hidden>("EghiddenPermActiver").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{2bd6f84d-a29f-4117-90ec-df1afe69ee1a}")));
            X.GetCmp<Hidden>("EghiddenPermExporterExcel").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{e5ce5c40-b5d2-4678-9fda-8e3507bc5dcd}")));
            X.GetCmp<Hidden>("EghiddenPermApprove").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{34b3b8cd-2026-4a03-b3ca-6913d19c4c41}")));
            X.GetCmp<Hidden>("EghiddenPermExtend").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{b0e47ba1-fa54-453e-b8c0-1d816832567c}")));
            X.GetCmp<Hidden>("EghiddenPermPrintBalance").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{988f3001-eeab-4677-8f3c-2ee0b2fb50d1}")));
            X.GetCmp<Hidden>("EghiddenPermPrintExecution").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{6be828a3-d164-4550-8d23-761f6a8db526}")));
            //X.GetCmp<Hidden>("EghiddenPermOverview").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{4c9f3b71-1a05-40cb-a4f0-af48c63e008a}")));
            X.GetCmp<Hidden>("EghiddenPermClose").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{8d978b32-532e-487e-a0ca-31d5c343d4aa}")));
            //X.GetCmp<Hidden>("EghiddenPermRegenerate").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{042f801c-d7c7-4426-915a-2e453606bc5d}")));
            X.GetCmp<Hidden>("EghiddenPermPrintContract").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{cd9bc869-96ef-4ff2-bef3-fa167bb9b0a5}")));

            #endregion

            return View();
        }

        public ActionResult OnAdd()
        {            
            EngagementViewModel mclass = new EngagementViewModel();

            mclass._Engagement = new Engagement();
            mclass._Engagement.Campagne = new Campagne();
            mclass._Engagement.Site = new Site();
            mclass._Engagement.EngagementType = new ContratPeriodeType();

            string UserName = (string)Session["userName"];

            Fonction HasAccessFunction = new Fonction();
            bool HasAccessAllSite = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            //LoadFournisseursApproved
            if (HasAccessAllSite) ViewData["UrlFournisseur"] = "LoadFournisseursApproved";
            else ViewData["UrlFournisseur"] = "LoadSupplierBySite";

            ViewData["HasAccessAllSite"] = HasAccessAllSite;

            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            //var mParam = (new Parametres()).fnSelect();
            Parametres parametre = new Parametres(0);            
            ///parametre = mParam[0] as Parametres;

            //ViewData["UrlContratType"] = "LoadTypeEngagementSiteWithoutFinancingType";
            
            mclass._Engagement.Campagne.Designation = parametre.Campagne;
            mclass._Engagement.EngagementType.ID = parametre.IDEngagementType;

            if (result)
                mclass._Engagement.Site.ID = mSiteParDefaut.ID;       

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormEngagementSite", Model = mclass, ViewData = ViewData };
        }

        public ActionResult OnEdit(string ItemSelected)
        {           
            Engagement mclass = JSON.Deserialize<Engagement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            EngagementViewModel engagementVM = new EngagementViewModel();
            engagementVM._Engagement = new Engagement();

            engagementVM._Engagement = mclass;
            engagementVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            Parametres parametre = new Parametres(0);

            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAccessAllSite = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);            
            if (HasAccessAllSite) ViewData["UrlFournisseur"] = "LoadFournisseursApproved";
            else ViewData["UrlFournisseur"] = "LoadSupplierBySite";

            ViewData["HasAccessAllSite"] = HasAccessAllSite;

            //ViewData["UrlContratType"] = "LoadTypeEngagementSiteWithoutFinancingType";
            //ViewData["FinancingTypeID"] = parametre.EngagementSiteTypeFinancement;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormEngagementSite", Model = engagementVM , ViewData = ViewData };
        }

        public ActionResult OnConsult(string ItemSelected)
        {           
            Engagement mclass = JSON.Deserialize<Engagement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            EngagementViewModel engagementVM = new EngagementViewModel();
            engagementVM._Engagement = new Engagement();

            engagementVM._Engagement = mclass;            
            engagementVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAccessAllSite = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            if (HasAccessAllSite) ViewData["UrlFournisseur"] = "LoadFournisseursApproved";
            else ViewData["UrlFournisseur"] = "LoadSupplierBySite";

            ViewData["HasAccessAllSite"] = false;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormEngagementSite", Model = engagementVM , ViewData = ViewData };
        }

        public ActionResult OnApprove(string ItemSelected)
        {
            Engagement mclass = JSON.Deserialize<Engagement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            EngagementViewModel engagementVM = new EngagementViewModel();
            engagementVM._Engagement = new Engagement();

            engagementVM._Engagement = mclass;
            engagementVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;
            Parametres parametre = new Parametres(0);

            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAccessAllSite = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            if (HasAccessAllSite) ViewData["UrlFournisseur"] = "LoadFournisseursApproved";
            else ViewData["UrlFournisseur"] = "LoadSupplierBySite";

            ViewData["HasAccessAllSite"] = HasAccessAllSite;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormEngagementSite", Model = engagementVM, ViewData = ViewData };
        }


        public ActionResult OnPrintBalanceReport(string id_frs = "", string ItemPeriodStart = "", string ItemPeriodEnd = "")
        {
            ContratPeriodeViewModel mclass = new ContratPeriodeViewModel();
            try
            {
                if (string.IsNullOrEmpty(ItemPeriodStart)) ItemPeriodStart = DateTime.Now.ToShortDateString();
                if (string.IsNullOrEmpty(ItemPeriodEnd)) ItemPeriodEnd = DateTime.Now.ToShortDateString();

                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                mclass._ContratPeriode = new ContratPeriode();
                mclass._ContratPeriode.DateDebut = datedebut;
                mclass._ContratPeriode.DateEcheance = datedfin;
                mclass._ContratPeriode.Campagne = new Campagne();
                mclass._ContratPeriode.Sites = new Site();
                mclass._TypeContratPeriodeReport = new TypeContratPeriodeReport();
                mclass._TypeContratPeriodeReport.Designation = "Balance";

                Site mSiteParDefaut = new Site();
                string UserName = (string)Session["userName"];
                bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

                if (result)
                    mclass._ContratPeriode.Sites.ID = mSiteParDefaut.ID;

                //var mParam = (new Parametres()).fnSelect();
                Parametres parametre = new Parametres(0);
                mclass._ContratPeriode.Campagne.Designation = parametre.Campagne;
                //mclass._ContratPeriode.DateDebut = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                if (!string.IsNullOrEmpty(id_frs))
                    ViewData["IdFrs"] = int.Parse(id_frs);
                else
                    ViewData["IdFrs"] = string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintBalanceCP", Model = mclass, ViewData = ViewData };
        }

        public ActionResult OnPrintExecutionReport(string id_frs = "", string ItemPeriodStart = "", string ItemPeriodEnd = "")
        {
            ContratPeriodeViewModel mclass = new ContratPeriodeViewModel();
            try
            {
                if (string.IsNullOrEmpty(ItemPeriodStart)) ItemPeriodStart = DateTime.Now.ToShortDateString();
                if (string.IsNullOrEmpty(ItemPeriodEnd)) ItemPeriodEnd = DateTime.Now.ToShortDateString();

                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                mclass._ContratPeriode = new ContratPeriode();
                mclass._ContratPeriode.DateDebut = datedebut;
                mclass._ContratPeriode.DateEcheance = datedfin;
                mclass._ContratPeriode.Campagne = new Campagne();
                mclass._ContratPeriode.Sites = new Site();
                mclass._TypeContratPeriodeReport = new TypeContratPeriodeReport();
                mclass._TypeContratPeriodeReport.Designation = "Execution";

                Site mSiteParDefaut = new Site();
                string UserName = (string)Session["userName"];
                bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

                if (result)
                    mclass._ContratPeriode.Sites.ID = mSiteParDefaut.ID;

                Parametres parametre = new Parametres(0);

                ViewBag.TypeReport = "Execution";
                mclass._ContratPeriode.Campagne.Designation = parametre.Campagne;
                //mclass._ContratPeriode.DateDebut = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                if (!string.IsNullOrEmpty(id_frs))
                    ViewData["IdFrs"] = int.Parse(id_frs);
                else
                    ViewData["IdFrs"] = string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintBalanceCP", Model = mclass, ViewData = ViewData };
        }

        public ActionResult OnExtend(string ItemSelected)
        {            
            Engagement mclass = JSON.Deserialize<Engagement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            EngagementViewModel engagementVM = new EngagementViewModel();
            engagementVM._Engagement = new Engagement();

            engagementVM._Engagement = mclass;
            engagementVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Extend;
            //Parametres parametre = new Parametres(0);

            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAccessAllSite = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);            
            if (HasAccessAllSite) ViewData["UrlFournisseur"] = "LoadFournisseursApproved";
            else ViewData["UrlFournisseur"] = "LoadSupplierBySite";

            ViewData["HasAccessAllSite"] = false;

            //ViewData["UrlContratType"] = "LoadTypeEngagementSiteWithoutFinancingType";
            //ViewData["FinancingTypeID"] = parametre.EngagementTypeFinancement;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormEngagementSite", Model = engagementVM, ViewData = ViewData };
        }

        //public ActionResult onRegenerate(string ItemSelected)
        //{
        //    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
        //    mViewport.Mask();

        //    EngagementSite mclass = JSON.Deserialize<EngagementSite>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

        //    EngagementViewModel contratVM = new EngagementViewModel();
        //    contratVM._Engagement = new Engagement();

        //    contratVM._Engagement = mclass;
        //    contratVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Extend;
        //    Parametres parametre = new Parametres(0);
        //    ViewData["UrlContratType"] = "LoadTypeEngagementSiteWithoutFinancingType";
        //    ViewData["FinancingTypeID"] = parametre.EngagementSiteTypeFinancement;

        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRegenenerEngagementSite", Model = contratVM, ViewData = ViewData };
        //}

        public ActionResult OnRefresh(string ItemCropYear, string ItemFournisseur, string ItemSite, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemTypeContrat, string ItemOption)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeEngagement");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemCropYear", ItemCropYear),
                                new Ext.Net.Parameter("ItemFournisseur", ItemFournisseur),
                                new Ext.Net.Parameter("ItemSite", ItemSite),
                                new Ext.Net.Parameter("ItemPeriodStart", ItemPeriodStart),
                                new Ext.Net.Parameter("ItemPeriodEnd", ItemPeriodEnd),
                                new Ext.Net.Parameter("ItemStatus", ItemStatus),
                                new Ext.Net.Parameter("ItemTypeContrat", ItemTypeContrat),
                                new Ext.Net.Parameter("ItemOption", ItemOption)
                            });
                FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelEngagement");

                mform.Collapsed = true;
            }
            catch (Exception ex)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Contrat : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }            

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelEngagement");            
            mform.ToggleCollapse();            
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemCropYear, string ItemFournisseur, string ItemSite, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemTypeContrat, string ItemOption)
        {
            string CropYearID = string.IsNullOrEmpty(ItemCropYear) ? "{Tous}" : ItemCropYear;
            string optionID = string.IsNullOrEmpty(ItemOption) ? "NO" : ItemOption;
            if (!string.IsNullOrEmpty(ItemCropYear) && ItemCropYear.Contains("null"))
            {
                CropYearID = "{Tous}";
            }
                           
            int fournisseurID = GetCritriaValue(ItemFournisseur);
            int siteID = GetCritriaValue(ItemSite);
            int typeContratID = GetCritriaValue(ItemTypeContrat);

            DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            string status = string.IsNullOrEmpty(ItemStatus) ? "-1": ItemStatus;
            if (!string.IsNullOrEmpty(ItemStatus) && ItemStatus.Contains("null"))
            {
                status = "-1";
            }
            var mListe = (new Engagement()).fnSelect(CropYearID, fournisseurID, typeContratID, startdate, enddate, status, siteID, optionID);            

            return this.Store(mListe);
        }

        public ActionResult ApproveFormMethod()
        {
            try
            {
                Engagement mClass = new Engagement();
                
                bool result = mClass.fnGet(Guid.Parse(GetFormValue("TxtEngagementID")));

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("onApprove : Engagement Approve failed.");

                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];
                result = mClass.fnApprove();
                mClass.Statut = "AP";
                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeEngagement");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

                X.GetCmp<Window>("FormEngagementSite").Close();

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Contrat : Approuver",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Engagement mClass = JSON.Deserialize<Engagement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = mClass.fnGet(mClass.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Engagement loading failed.");

                if (mClass.Desactive)
                    result = mClass.fnActivate();
                else
                    result = mClass.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Engagement Approval failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeEngagement");

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
                    Title = "Contrat : Approuver",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnClose(string ItemSelected)
        {
            try
            {
                Engagement mClass = JSON.Deserialize<Engagement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = mClass.fnGet(mClass.ID);

                if (!result)
                    throw new Exception("OnClose : Engagement loading failed.");

                mClass.UtilisateurModification = (string)Session["userName"];
                result = mClass.fnClose();                

                if (!result)
                    throw new Exception("OnClose : Engagement Close failed.");

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeEngagement");

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
                    Title = "Contrat : Close",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }



        [HttpPost]
        public ActionResult SubmitFormMethod()
        {

            try
            {
                Engagement mClass = new Engagement();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtEngagementID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Engagement load failed.");
                }

                mClass = MapFormToObject(mClass);

                bool result = mClass.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeEngagement");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionEngagement").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormEngagementSite").Close();                    
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Contrat : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitExtendFormMethod()
        {
            try
            {
                Engagement mClass = new Engagement();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode != Tms.Components.Settings.EnumsDefinition.eExecMode.Extend)
                    throw new Exception("SubmitFormMethod : Engagement load failed.");
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtEngagementID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Engagement load failed.");
                }

                mClass = MapFormToObject(mClass);

                bool result = mClass.fnExtend();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeEngagement");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionEngagement").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormEngagementSite").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Contrat : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitRegeneration()
        {
            try
            {
                Engagement mClass = new Engagement();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode != Tms.Components.Settings.EnumsDefinition.eExecMode.Extend)
                    throw new Exception("SubmitRegeneration : Engagement load failed.");
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtEngagementID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitRegeneration : Engagement load failed.");
                }

                mClass = MapFormToObject(mClass);

                bool result = mClass.fnRegenerate();
                mClass.EstRelance = true;
                mClass.Statut = "AP";
                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeEngagement");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionEngagement").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormREngagementSite").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Contrat : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult GenerateBalanceReport()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            ContratPeriode contratperiode = new ContratPeriode();

            //report.Parameters["cropyear"].Value = GetFormValue("CPcropYearForReport");
            //report.Parameters["fournisseurID"].Value = int.Parse(GetFormValue("CPfournisseurForReport"));
            //report.Parameters["typecontratID"].Value = int.Parse(GetFormValue("CPtypeContratForReport"));
            //report.Parameters["startdate"].Value = DateTime.Parse(X.GetCmp<DateField>("CPstartDateForReport").RawText.ToString());
            //report.Parameters["duedate"].Value = DateTime.Parse(X.GetCmp<DateField>("CPdueDateForReport").RawText.ToString());
            //report.Parameters["optionID"].Value = X.GetCmp<ComboBox>("CPtypeContratOptionForReport").SelectedItem.Value.ToString();

            contratperiode.Campagne = new Campagne();
            contratperiode.Campagne.Designation = GetFormValue("CPcropYearForReport");

            contratperiode.Fournisseur = new Fournisseur();
            contratperiode.Fournisseur.ID = int.Parse(GetFormValue("CPfournisseurForReport"));
            contratperiode.Fournisseur.Nom = X.GetCmp<ComboBox>("CPfournisseurForReport").SelectedItem.Text;

            contratperiode.ContratPeriodeType = new ContratPeriodeType();
            contratperiode.ContratPeriodeType.ID = int.Parse(GetFormValue("CPtypeContratForReport"));

            contratperiode.DateDebut = DateTime.Parse(X.GetCmp<DateField>("CPstartDateForReport").RawText.ToString());
            contratperiode.DateEcheance = DateTime.Parse(X.GetCmp<DateField>("CPdueDateForReport").RawText.ToString());
            contratperiode.Statut = X.GetCmp<ComboBox>("CPtypeContratOptionForReport").SelectedItem.Value.ToString();

            contratperiode.Sites = new Site();
            contratperiode.Sites.ID = int.Parse(GetFormValue("CpcmbSite"));
            contratperiode.Sites.Nom = X.GetCmp<ComboBox>("CpcmbSite").SelectedItem.Text;

            string param = JSON.Serialize(contratperiode);
            Session["ParamReport"] = param;

            X.GetCmp<Window>("FormPrintBalanceCP").Close();
            //Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            //mViewport.Unmask();

            //HttpUtility.JavaScriptStringEncode(param)
            return JavaScript(String.Format("CloseFormPrintBalanceCP(), addTab(window.parent.Ext.getCmp('tabCenter'), 'BalanceReport{0}', '{1}/EngagementSite/ViewBalanceReport', this, 'Engagement - Balance','report')", Guid.NewGuid(), BaseUrl));
            //return this.Direct();
        }

        public ActionResult GenerateExecutionReport()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            ContratPeriode contratperiode = new ContratPeriode();

            contratperiode.Campagne = new Campagne();
            contratperiode.Campagne.Designation = GetFormValue("CPcropYearForReport");

            contratperiode.Fournisseur = new Fournisseur();
            contratperiode.Fournisseur.ID = int.Parse(GetFormValue("CPfournisseurForReport"));
            contratperiode.Fournisseur.Nom = X.GetCmp<ComboBox>("CPfournisseurForReport").SelectedItem.Text;

            contratperiode.ContratPeriodeType = new ContratPeriodeType();
            contratperiode.ContratPeriodeType.ID = int.Parse(GetFormValue("CPtypeContratForReport"));

            contratperiode.DateDebut = DateTime.Parse(X.GetCmp<DateField>("CPstartDateForReport").RawText.ToString());
            contratperiode.DateEcheance = DateTime.Parse(X.GetCmp<DateField>("CPdueDateForReport").RawText.ToString());
            contratperiode.Statut = X.GetCmp<ComboBox>("CPtypeContratOptionForReport").SelectedItem.Value.ToString();

            contratperiode.Sites = new Site();
            contratperiode.Sites.ID = int.Parse(GetFormValue("CpcmbSite"));
            contratperiode.Sites.Nom = X.GetCmp<ComboBox>("CpcmbSite").SelectedItem.Text;

            string param = JSON.Serialize(contratperiode);
            Session["ParamReport"] = param;

            //Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            //mViewport.Unmask();
            this.Direct();
            //HttpUtility.JavaScriptStringEncode(param)
            return JavaScript(String.Format("CloseFormPrintBalanceCP(), addTab(window.parent.Ext.getCmp('tabCenter'), 'BalanceReport{0}', '{1}/EngagementSite/ViewExecutionReport', this, 'Engagement - Execution','report')", Guid.NewGuid(), BaseUrl));
            //return this.Direct();
        }


        public ActionResult ViewBalanceReport()
        {
            ContratPeriodeBalanceReport report = new ContratPeriodeBalanceReport();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            ContratPeriode contrat = JSON.Deserialize<ContratPeriode>((string)Session["ParamReport"], new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            report.Parameters["titre"].Value = "Engagement - Balance";
            report.Parameters["cropyear"].Value = contrat.Campagne.Designation;
            report.Parameters["fournisseurID"].Value = contrat.Fournisseur.ID;
            report.Parameters["typecontratID"].Value = contrat.ContratPeriodeType.ID;
            report.Parameters["startdate"].Value = contrat.DateDebut;
            report.Parameters["duedate"].Value = contrat.DateEcheance;
            report.Parameters["optionID"].Value = contrat.Statut;
            report.Parameters["siteID"].Value = contrat.Sites.ID;

            //Afficher Les criteres sur l'etat
            report.Parameters["fournisseurNom"].Value = contrat.Fournisseur.Nom;
            report.Parameters["optionNom"].Value = contrat.Statut;
            report.Parameters["siteNom"].Value = contrat.Sites.Nom;

            ViewData["Report"] = report;

            return View();
        }

        public ActionResult ViewExecutionReport()
        {
            ContratPeriodeExecutionReport report = new ContratPeriodeExecutionReport();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            ContratPeriode contrat = JSON.Deserialize<ContratPeriode>((string)Session["ParamReport"], new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            report.Parameters["titre"].Value = "Engagement - Execution";
            report.Parameters["cropyear"].Value = contrat.Campagne.Designation;
            report.Parameters["fournisseurID"].Value = contrat.Fournisseur.ID;
            report.Parameters["typecontratID"].Value = contrat.ContratPeriodeType.ID;
            report.Parameters["startdate"].Value = contrat.DateDebut;
            report.Parameters["duedate"].Value = contrat.DateEcheance;
            report.Parameters["optionID"].Value = contrat.Statut;
            report.Parameters["siteID"].Value = contrat.Sites.ID;

            //Afficher Les criteres sur l'etat
            report.Parameters["fournisseurNom"].Value = contrat.Fournisseur.Nom;
            report.Parameters["optionNom"].Value = contrat.Statut;
            report.Parameters["siteNom"].Value = contrat.Sites.Nom;

            ViewData["Report"] = report;

            return View();
        }

        private Engagement MapFormToObject(Engagement mClass)
        {
            mClass.ID = Guid.Parse( X.GetCmp<TextField>("TxtEngagementID").Text.ToString());
            mClass.Campagne = new Campagne();
            mClass.Campagne.Designation = GetFormValue("CropYearID");

            mClass.Fournisseur = new Fournisseur();
            mClass.Fournisseur.ID = int.Parse(GetFormValue("FournisseurID"));
            mClass.Fournisseur.Nom = X.GetCmp<ComboBox>("FournisseurID").SelectedItem.Text.ToString();

            mClass.Site = new Site();
            mClass.Site.ID = int.Parse(GetFormValue("cmbSiteEngagement"));
            mClass.Site.Nom = X.GetCmp<ComboBox>("cmbSiteEngagement").SelectedItem.Text.ToString();

            mClass.EngagementType = new ContratPeriodeType();
            mClass.EngagementType.ID = int.Parse(GetFormValue("TypeContratID"));
            mClass.EngagementType.Designation = X.GetCmp<ComboBox>("TypeContratID").SelectedItem.Text.ToString();

            mClass.DateContrat = DateTime.Parse(X.GetCmp<DateField>("TxtDateContrat").RawText.ToString());
            mClass.DateDebut = DateTime.Parse(X.GetCmp<DateField>("TxtStartDate").RawText.ToString());
            mClass.DateEcheance = DateTime.Parse(X.GetCmp<DateField>("TxtDueDate").RawText.ToString()).AddHours(23).AddMinutes(59);
            
            mClass.Tonnage = string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtTonnage").RawText) ? 0 : decimal.Parse(X.GetCmp<NumberField>("TxtTonnage").RawText);
            mClass.Montant = string.IsNullOrEmpty(X.GetCmp<NumberField>("txtAmount").RawText) ? 0 : decimal.Parse(X.GetCmp<NumberField>("txtAmount").RawText);
            //mClass.Balance = decimal.Parse(X.GetCmp<NumberField>("TxtTonnage").RawText);
            //mClass.Statut = "NA";
            mClass.Prix = string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtPrice").RawText) ? 0 : decimal.Parse(X.GetCmp<NumberField>("TxtPrice").RawText);
            mClass.MontantPrime = string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtPrime").RawText) ? 0 : decimal.Parse(X.GetCmp<NumberField>("TxtPrime").RawText);
            mClass.PrixBrut = string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtGrossPrice").RawText) ? 0 : decimal.Parse(X.GetCmp<NumberField>("TxtGrossPrice").RawText);

            mClass.Numero = X.GetCmp<Hidden>("hiddenNumeroPrice").Text;
            mClass.Commentaire = X.GetCmp<Hidden>("TxtCommentaire").Text;

            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }

        private void MapObjectToForm(PrixJournalier mClass)
        {
            X.GetCmp<TextField>("TxtDailyPriceID").Text = mClass.ID.ToString();
            X.GetCmp<TextField>("TxtEntryDate").Text = mClass.DatePrix.ToString();
            X.GetCmp<TextField>("TxtStartDate").Text = mClass.DateDebut.ToString();
            X.GetCmp<TextField>("TxtDueDate").Text = mClass.DateEcheance.ToString();
            X.GetCmp<TextField>("TxtPrice").Text = mClass.Prix.ToString();
            X.GetCmp<TextField>("hiddenNumeroPrice").Text = mClass.Numero.ToString();
            //X.GetCmp<ComboBox>("LocationID").SetValue(mClass.Site.ID.ToString());


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
            //X.Js.Call("App.rowSelectionEngagement.deselectAll();");
            X.GetCmp<RowSelectionModel>("rowSelectionEngagement").DeselectAll();
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

        public ActionResult OnDisplayEngagementList(string ItemPeriodStart = "", string ItemPeriodEnd = "")
        {
            ContratPeriode mContrat = new ContratPeriode();
            try
            {
                Parametres mParam = new Parametres(0);
                ViewData["Titre"] = "Print List of Engagements";
                ViewData["actionToDo"] = "OnPrintForwardContractList";
                ViewData["ControllerName"] = "ContratPeriode";
                if (string.IsNullOrEmpty(ItemPeriodStart)) ItemPeriodStart = DateTime.Now.ToShortDateString();
                if (string.IsNullOrEmpty(ItemPeriodEnd)) ItemPeriodEnd = DateTime.Now.ToShortDateString();

                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);
                
                mContrat.DateDebut = datedebut;
                mContrat.DateEcheance = datedfin;
                mContrat.ContratPeriodeType = new ContratPeriodeType();
                mContrat.ContratPeriodeType.ID = mParam.IDEngagementType;
                mContrat.Sites = new Site();
                Site mSiteParDefaut = new Site();
                string UserName = (string)Session["userName"];
                bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

                if (result)
                    mContrat.Sites.ID = mSiteParDefaut.ID;
            }
            catch (Exception)
            {

                throw;
            }
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintEngagementList", Model = mContrat, ViewData = ViewData };
        }


        public ActionResult OnPrintForwardContractList(string cropyear, string fournisseur, string fournisseurText, string typecontract, string typecontractText, string startDate, string endDate, string statut, string statutText)
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
                Session["paramTypeOfContract"] = typecontract;
                Session["paramTypeOfContractText"] = typecontractText;
                Session["paramStatut"] = statut;
                Session["paramStatutText"] = statutText;
                
                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;

                Session["paramOption"] = X.GetCmp<ComboBox>("cmbDetOption").SelectedItem.Value.ToString();
                Session["paramOptionText"] = X.GetCmp<ComboBox>("cmbDetOption").SelectedItem.Text;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/EngagementSite/ViewReportResult', this, 'Engagements',''),App.frmCriteriaForForwardContract.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Contrat : Data Validation",
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

            rptForwardContractList report = new rptForwardContractList();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramCampagneText"].Value = Session["paramCampagne"];
            if (!string.IsNullOrEmpty(Session["paramCampagne"].ToString()) && Session["paramCampagne"].ToString() == "{Tous}")
                report.Parameters["paramCampagne"].Value = string.Empty;
            else
                report.Parameters["paramCampagne"].Value = Session["paramCampagne"];

            report.Parameters["paramFournisseur"].Value = Session["paramFournisseur"];
            report.Parameters["paramFournisseurText"].Value = Session["paramFournisseurText"];

            report.Parameters["paramTypeOfContract"].Value = Session["paramTypeOfContract"];
            report.Parameters["paramTypeOfContractText"].Value = Session["paramTypeOfContractText"];

            report.Parameters["paramStatut"].Value = Session["paramStatut"];
            report.Parameters["paramStatutText"].Value = Session["paramStatutText"];

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];
            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            report.Parameters["paramOption"].Value = Session["paramOption"];
            report.Parameters["paramOptionText"].Value = Session["paramOptionText"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

        public ActionResult OnPrintExecutionDetail(string IdContrat)
        {            

            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'ContratDetail{0}', '{1}/EngagementSite/ViewReport?id={0}', this, 'Engagement Execution Detail','')", IdContrat, BaseUrl));
        }

        //public ActionResult ViewReport(string id)
        //{
        //    EngagementSiteDetailExecutionReport report = new EngagementSiteDetailExecutionReport();

        //    report.DataSource = DevExpressReportDs.SetDataSource(report);
        //    report.Parameters["contratID"].Value = id;

        //    ViewData["Report"] = report;

        //    return View("ViewReportResult");
        //}


    }
}