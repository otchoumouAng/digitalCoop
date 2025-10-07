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
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Components.Data;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;


namespace Tms2017.MVC.Controllers
{
    //[RoutePrefix("ForwardContract")]
    public class DeplacementPaletteController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: DeplacementPalette
        //[Route]
        public ActionResult Index()
        {
            Parametres mParam = (new Parametres(0));

            X.GetCmp<ComboBox>("cmbCropYear").SetValue(mParam.Campagne);
           // X.GetCmp<Hidden>("defaultFinancingTypeID").SetValue(mParam.DeplacementPaletteTypeFinancement);

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();

            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;
            //X.GetCmp<ComboBox>("cmbTypeContrat").SetValue(mclass.DeplacementPaletteType.ID);

            DateTime firstdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            DateTime lastDayOfMonth = firstdayofmonth.AddMonths(1).AddDays(-1);

            X.GetCmp<DateField>("TxtPeriodStart").RawText = firstdayofmonth.ToShortDateString();
            X.GetCmp<DateField>("TxtPeriodEnd").RawText = lastDayOfMonth.ToShortDateString();

            X.GetCmp<FormPanel>("CriteriaPanelForwardContract").SetTitle("Site : " + mSiteParDefaut.Nom + ", Campagne : " + mParam.Campagne + " | Du : " + firstdayofmonth.ToShortDateString() + " Au : " + lastDayOfMonth.ToShortDateString());

            #region Set Function's Access            
            Fonction HasAccess = new Fonction();
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{B8EC41D0-173A-47A6-BB9A-140942CE66B8}", UserName);

            if (HasAccess.fnGetUserAccessStatus("{9C7B9B85-4E09-4025-AE98-4B409121C195}", UserName) == false)
                X.GetCmp<Button>("btnNewContract").Disable();
            else
                X.GetCmp<Button>("btnNewContract").Enable();

            if (HasAccess.fnGetUserAccessStatus("{9A7624A9-AFAD-4EB0-9799-BFC0C86365D7}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportContractToExcel").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportContractToExcel").Enable();

            if (HasAccess.fnGetUserAccessStatus("{8BC71399-E078-4B7C-8034-DEA60D4A229F}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintBalance").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintBalance").Enable();

            if (HasAccess.fnGetUserAccessStatus("{340FA4F5-849F-4E31-9CAA-017D51D6E39B}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintExecution").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintExecution").Enable();

            if (HasAccess.fnGetUserAccessStatus("{C488A793-43FF-4A89-B48F-F5A1E96CF91E}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintForwardContractList").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintForwardContractList").Enable();

            if (HasAccess.fnGetUserAccessStatus("{a721b431-8bb3-4cc1-ac2c-5d9f09942274}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintPrime").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintPrime").Enable();

            if (HasAccess.fnGetUserAccessStatus("{b1c99d84-4276-4e16-ad84-363d0c047b4b}", UserName) == false)
            {
                X.GetCmp<MenuItem>("mnuPrintEtatLivStatut").Disable();
                X.GetCmp<MenuItem>("mnuPrintEtatLivStatutFin").Disable();
            }
            else
            {
                X.GetCmp<MenuItem>("mnuPrintEtatLivStatut").Enable();
                X.GetCmp<MenuItem>("mnuPrintEtatLivStatutFin").Enable();
            }

            X.GetCmp<Hidden>("CphiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{9C7B9B85-4E09-4025-AE98-4B409121C195}", UserName));
            X.GetCmp<Hidden>("CphiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{676D2692-3211-476C-B51B-154AA7115175}", UserName));
            X.GetCmp<Hidden>("CphiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{16CB8FFF-F721-4A4D-BE09-DE26E29BBFCD}", UserName));
            X.GetCmp<Hidden>("CphiddenPermActiver").SetValue(HasAccess.fnGetUserAccessStatus("{C7177110-080D-41BF-BBBA-D788F2689963}", UserName));
            X.GetCmp<Hidden>("CphiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{9A7624A9-AFAD-4EB0-9799-BFC0C86365D7}", UserName));
            //X.GetCmp<Hidden>("SphiddenPermPrintSpotPriceList").SetValue(HasAccess.fnGetUserAccessStatus("{2ABE7A72-758C-4A1D-B213-A34007BFECB1}", UserName));
            X.GetCmp<Hidden>("CphiddenPermApprove").SetValue(HasAccess.fnGetUserAccessStatus("144249A6-95E0-4340-993A-B0F8567ABE21", UserName));
            X.GetCmp<Hidden>("CphiddenPermExtend").SetValue(HasAccess.fnGetUserAccessStatus("1ADA7DA5-B7EB-4C72-9600-C5A98E74BBFE", UserName));
            X.GetCmp<Hidden>("CphiddenPermPrintBalance").SetValue(HasAccess.fnGetUserAccessStatus("{8BC71399-E078-4B7C-8034-DEA60D4A229F}", UserName));
            X.GetCmp<Hidden>("CphiddenPermPrintExecution").SetValue(HasAccess.fnGetUserAccessStatus("{340FA4F5-849F-4E31-9CAA-017D51D6E39B}", UserName));
            X.GetCmp<Hidden>("CphiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{6B20933B-F5EC-4F03-8A5C-DDE720F5BE98}", UserName));
            X.GetCmp<Hidden>("CphiddenPermClose").SetValue(HasAccess.fnGetUserAccessStatus("{22481B8A-69CA-4366-AEEB-DFA87EBF4BCA}", UserName));
            X.GetCmp<Hidden>("CphiddenPermRegenerate").SetValue(HasAccess.fnGetUserAccessStatus("{c888dc30-f56d-4658-8edd-e1b433d81ae8}", UserName));
            X.GetCmp<Hidden>("CphiddenPermPrintDetailExecution").SetValue(HasAccess.fnGetUserAccessStatus("{2b00235a-9e1f-4d8a-a907-5b2243393caf}", UserName));
            X.GetCmp<Hidden>("CphiddenPermPrintLivStatut").SetValue(HasAccess.fnGetUserAccessStatus("{b1c99d84-4276-4e16-ad84-363d0c047b4b}", UserName));

            #endregion

            return View();
        }

        public ActionResult OnAdd()
        {
            DeplacementPaletteViewModel mclass = new DeplacementPaletteViewModel();

            mclass._DeplacementPalette = new DeplacementPalette();
            mclass._DeplacementPalette.Palette = new Palette();
            mclass._DeplacementPalette.OrdreFabrication = new OrdreFabrication();
            mclass._DeplacementPalette.MagasinSource = new Magasin();
            mclass._DeplacementPalette.MagasinDestination = new Magasin();

            string UserName = (string)Session["userName"];

           

            
            //mclass._DeplacementPalette.Campagne.Designation = mParam.Campagne;
            //mclass._DeplacementPalette.DeplacementPaletteType.ID = mParam.DeplacementPaletteType.ID;
            //mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            //ViewData["FinancingTypeID"] = mParam.DeplacementPaletteTypeFinancement;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDeplacementPalette", Model = mclass, ViewData = ViewData };
        }

        public ActionResult OnEdit(string ItemSelected)
        {

            DeplacementPalette mclass = JSON.Deserialize<DeplacementPalette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            DeplacementPaletteViewModel contratVM = new DeplacementPaletteViewModel();
            contratVM._DeplacementPalette = new DeplacementPalette();

            contratVM._DeplacementPalette = mclass;
            contratVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            Parametres parametre = new Parametres(0);
            ViewData["UrlContratType"] = "LoadTypeDeplacementPaletteWithoutFinancingType";
            //ViewData["FinancingTypeID"] = parametre.DeplacementPaletteTypeFinancement;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDeplacementPalette", Model = contratVM, ViewData = ViewData };
        }

        public ActionResult OnConsult(string ItemSelected)
        {
            DeplacementPalette mclass = JSON.Deserialize<DeplacementPalette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            DeplacementPaletteViewModel contratVM = new DeplacementPaletteViewModel();
            contratVM._DeplacementPalette = new DeplacementPalette();

            contratVM._DeplacementPalette = mclass;
            ViewData["UrlContratType"] = "LoadTypeDeplacementPalette";
            contratVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;


            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDeplacementPalette", Model = contratVM, ViewData = ViewData };
        }


        //public ActionResult OnPrintBalanceReport(string id_frs = "", string ItemPeriodStart = "", string ItemPeriodEnd = "")
        //{
        //    DeplacementPaletteViewModel mclass = new DeplacementPaletteViewModel();
        //    try
        //    {
        //        if (string.IsNullOrEmpty(ItemPeriodStart)) ItemPeriodStart = DateTime.Now.ToShortDateString();
        //        if (string.IsNullOrEmpty(ItemPeriodEnd)) ItemPeriodEnd = DateTime.Now.ToShortDateString();
        //        DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
        //        DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

        //        mclass._DeplacementPalette = new DeplacementPalette();
        //        mclass._DeplacementPalette.DateDebut = datedebut;
        //        mclass._DeplacementPalette.DateEcheance = datedfin;
        //        mclass._DeplacementPalette.Campagne = new Campagne();
        //        mclass._DeplacementPalette.Sites = new Site();
        //        mclass._TypeDeplacementPaletteReport = new TypeDeplacementPaletteReport();
        //        mclass._TypeDeplacementPaletteReport.Designation = "Balance";

        //        Site mSiteParDefaut = new Site();
        //        string UserName = (string)Session["userName"];
        //        bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

        //        if (result)
        //            mclass._DeplacementPalette.Sites.ID = mSiteParDefaut.ID;

        //        //var mParam = (new Parametres()).fnSelect();
        //        Parametres parametre = new Parametres(0);
        //        mclass._DeplacementPalette.Campagne.Designation = parametre.Campagne;
        //        //mclass._DeplacementPalette.DateDebut = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        //        if (!string.IsNullOrEmpty(id_frs))
        //            ViewData["IdFrs"] = int.Parse(id_frs);
        //        else
        //            ViewData["IdFrs"] = string.Empty;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintBalanceCP", Model = mclass, ViewData = ViewData };
        //}

        //public ActionResult OnPrintContratStatusLivReport(string id_frs = "", string ItemPeriodStart = "", string ItemPeriodEnd = "")
        //{
        //    DeplacementPaletteViewModel mclass = new DeplacementPaletteViewModel();
        //    try
        //    {
        //        if (string.IsNullOrEmpty(ItemPeriodStart)) ItemPeriodStart = DateTime.Now.ToShortDateString();
        //        if (string.IsNullOrEmpty(ItemPeriodEnd)) ItemPeriodEnd = DateTime.Now.ToShortDateString();
        //        DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
        //        DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

        //        mclass._DeplacementPalette = new DeplacementPalette();
        //        mclass._DeplacementPalette.DateDebut = datedebut;
        //        mclass._DeplacementPalette.DateEcheance = datedfin;
        //        mclass._DeplacementPalette.Campagne = new Campagne();
        //        mclass._DeplacementPalette.Sites = new Site();
        //        mclass._TypeDeplacementPaletteReport = new TypeDeplacementPaletteReport();
        //        mclass._TypeDeplacementPaletteReport.Designation = "StatutLiv";

        //        Site mSiteParDefaut = new Site();
        //        string UserName = (string)Session["userName"];
        //        bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

        //        if (result)
        //            mclass._DeplacementPalette.Sites.ID = mSiteParDefaut.ID;

        //        //var mParam = (new Parametres()).fnSelect();
        //        Parametres parametre = new Parametres(0);
        //        mclass._DeplacementPalette.Campagne.Designation = parametre.Campagne;
        //        //mclass._DeplacementPalette.DateDebut = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        //        if (!string.IsNullOrEmpty(id_frs))
        //            ViewData["IdFrs"] = int.Parse(id_frs);
        //        else
        //            ViewData["IdFrs"] = string.Empty;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintBalanceCP", Model = mclass, ViewData = ViewData };
        //}

        //public ActionResult OnPrintContratStatusLivReportFin(string id_frs = "", string ItemPeriodStart = "", string ItemPeriodEnd = "")
        //{
        //    DeplacementPaletteViewModel mclass = new DeplacementPaletteViewModel();
        //    try
        //    {
        //        if (string.IsNullOrEmpty(ItemPeriodStart)) ItemPeriodStart = DateTime.Now.ToShortDateString();
        //        if (string.IsNullOrEmpty(ItemPeriodEnd)) ItemPeriodEnd = DateTime.Now.ToShortDateString();
        //        DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
        //        DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

        //        mclass._DeplacementPalette = new DeplacementPalette();
        //        mclass._DeplacementPalette.DateDebut = datedebut;
        //        mclass._DeplacementPalette.DateEcheance = datedfin;
        //        mclass._DeplacementPalette.Campagne = new Campagne();
        //        mclass._DeplacementPalette.Sites = new Site();
        //        mclass._TypeDeplacementPaletteReport = new TypeDeplacementPaletteReport();
        //        mclass._TypeDeplacementPaletteReport.Designation = "StatutLivFin";

        //        Site mSiteParDefaut = new Site();
        //        string UserName = (string)Session["userName"];
        //        bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

        //        if (result)
        //            mclass._DeplacementPalette.Sites.ID = mSiteParDefaut.ID;

        //        //var mParam = (new Parametres()).fnSelect();
        //        Parametres parametre = new Parametres(0);
        //        mclass._DeplacementPalette.Campagne.Designation = parametre.Campagne;
        //        //mclass._DeplacementPalette.DateDebut = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        //        if (!string.IsNullOrEmpty(id_frs))
        //            ViewData["IdFrs"] = int.Parse(id_frs);
        //        else
        //            ViewData["IdFrs"] = string.Empty;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintBalanceCP", Model = mclass, ViewData = ViewData };
        //}


        //public ActionResult OnPrintExecutionReport(string id_frs = "", string ItemPeriodStart = "", string ItemPeriodEnd = "")
        //{
        //    DeplacementPaletteViewModel mclass = new DeplacementPaletteViewModel();
        //    try
        //    {
        //        if (string.IsNullOrEmpty(ItemPeriodStart)) ItemPeriodStart = DateTime.Now.ToShortDateString();
        //        if (string.IsNullOrEmpty(ItemPeriodEnd)) ItemPeriodEnd = DateTime.Now.ToShortDateString();
        //        DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
        //        DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

        //        mclass._DeplacementPalette = new DeplacementPalette();
        //        mclass._DeplacementPalette.DateDebut = datedebut;
        //        mclass._DeplacementPalette.DateEcheance = datedfin;
        //        mclass._DeplacementPalette.Campagne = new Campagne();
        //        mclass._DeplacementPalette.Sites = new Site();
        //        mclass._TypeDeplacementPaletteReport = new TypeDeplacementPaletteReport();
        //        mclass._TypeDeplacementPaletteReport.Designation = "Execution";

        //        Site mSiteParDefaut = new Site();
        //        string UserName = (string)Session["userName"];
        //        bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

        //        if (result)
        //            mclass._DeplacementPalette.Sites.ID = mSiteParDefaut.ID;

        //        Parametres parametre = new Parametres(0);

        //        ViewBag.TypeReport = "Execution";
        //        mclass._DeplacementPalette.Campagne.Designation = parametre.Campagne;
        //        //mclass._DeplacementPalette.DateDebut = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        //        if (!string.IsNullOrEmpty(id_frs))
        //            ViewData["IdFrs"] = int.Parse(id_frs);
        //        else
        //            ViewData["IdFrs"] = string.Empty;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintBalanceCP", Model = mclass, ViewData = ViewData };
        //}

        //public ActionResult OnExtend(string ItemSelected)
        //{
        //    DeplacementPalette mclass = JSON.Deserialize<DeplacementPalette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

        //    DeplacementPaletteViewModel contratVM = new DeplacementPaletteViewModel();
        //    contratVM._DeplacementPalette = new DeplacementPalette();

        //    contratVM._DeplacementPalette = mclass;
        //    contratVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Extend;
        //    Parametres parametre = new Parametres(0);
        //    ViewData["UrlContratType"] = "LoadTypeDeplacementPaletteWithoutFinancingType";
        //    ViewData["FinancingTypeID"] = parametre.DeplacementPaletteTypeFinancement;

        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDeplacementPalette", Model = contratVM, ViewData = ViewData };
        //}

        //public ActionResult onRegenerate(string ItemSelected)
        //{
        //    DeplacementPalette mclass = JSON.Deserialize<DeplacementPalette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

        //    DeplacementPaletteViewModel contratVM = new DeplacementPaletteViewModel();
        //    contratVM._DeplacementPalette = new DeplacementPalette();

        //    contratVM._DeplacementPalette = mclass;
        //    contratVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Extend;
        //    Parametres parametre = new Parametres(0);
        //    ViewData["UrlContratType"] = "LoadTypeDeplacementPaletteWithoutFinancingType";
        //    ViewData["FinancingTypeID"] = parametre.DeplacementPaletteTypeFinancement;

        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRegenenerDeplacementPalette", Model = contratVM, ViewData = ViewData };
        //}

        public ActionResult OnRefresh(string ItemCropYear, string ItemFournisseur, string ItemTypeContrat, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite, string ItemOption)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeForwardContract");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemCropYear", ItemCropYear),
                                new Ext.Net.Parameter("ItemFournisseur", ItemFournisseur),
                                new Ext.Net.Parameter("ItemTypeContrat", ItemTypeContrat),
                                new Ext.Net.Parameter("ItemPeriodStart", ItemPeriodStart),
                                new Ext.Net.Parameter("ItemPeriodEnd", ItemPeriodEnd),
                                new Ext.Net.Parameter("ItemSite", ItemSite),
                                new Ext.Net.Parameter("ItemStatus", ItemStatus),
                                new Ext.Net.Parameter("ItemOption", ItemOption)
                            });
                FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelForwardContract");

                mform.Collapsed = true;
            }
            catch (Exception ex)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Contrat Periode : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelForwardContract");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult Select()
        {
            //string CropYearID = string.IsNullOrEmpty(ItemCropYear) ? "{Tous}" : ItemCropYear;
            //string optionID = string.IsNullOrEmpty(ItemOption) ? "NO" : ItemOption;
            //if (!string.IsNullOrEmpty(ItemCropYear) && ItemCropYear.Contains("null"))
            //{
            //    CropYearID = "{Tous}";
            //}

            //int fournisseurID = GetCritriaValue(ItemFournisseur);
            //int TypeContratID = GetCritriaValue(ItemTypeContrat);
            //int SiteID = GetCritriaValue(ItemSite);

            //DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            //DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            //string status = string.IsNullOrEmpty(ItemStatus) ? "-1" : ItemStatus;
            //if (!string.IsNullOrEmpty(ItemStatus) && ItemStatus.Contains("null"))
            //{
            //    status = "-1";
            //}


            var mListe = (new DeplacementPalette()).fnSelect("-1");

            return this.Store(mListe);

        }

        public ActionResult SelectByModalFilter(StoreRequestParameters parameters, string ItemStatus, string ItemAnnee, string ItemSemaine, string ItemProduit, string ItemProduitType, string ItemProduction)
        {
            string status = "-1";
            if (!string.IsNullOrEmpty(ItemStatus) && ItemStatus == "false")
            {
                status = "-1";
            }
            else if (ItemStatus == "true")
            {
                status = "1";
            }

            int Annee = -1;
            if (!string.IsNullOrEmpty(ItemAnnee))
                Annee = int.Parse(ItemAnnee);

            int Semaine = -1;
            if (!string.IsNullOrEmpty(ItemSemaine))
                Semaine = int.Parse(ItemSemaine);

            int Produit = -1;
            if (!string.IsNullOrEmpty(ItemProduit))
                Produit = int.Parse(ItemProduit);

            int ProduitType = -1;
            if (!string.IsNullOrEmpty(ItemProduitType))
                ProduitType = int.Parse(ItemProduitType);

            // string Production = "{Tous}";
            string Production = null;
            if (!string.IsNullOrEmpty(ItemProduction) && ItemProduction == "{Tous}")
                Production = null;
            else if (!string.IsNullOrEmpty(ItemProduction))
            {
                Production = ItemProduction;
            }



            var liste = new Palette().fnSelect(status, Annee, Semaine, Produit, ProduitType, Production);
            //var pagning = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }



        //public ActionResult onApprove(string ItemSelected)
        //{
        //    DeplacementPalette mclass = JSON.Deserialize<DeplacementPalette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

        //    DeplacementPaletteViewModel contratVM = new DeplacementPaletteViewModel();
        //    contratVM._DeplacementPalette = new DeplacementPalette();

        //    contratVM._DeplacementPalette = mclass;
        //    contratVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;
        //    Parametres parametre = new Parametres(0);
        //    ViewData["UrlContratType"] = "LoadTypeDeplacementPaletteWithoutFinancingType";
        //    ViewData["FinancingTypeID"] = parametre.DeplacementPaletteTypeFinancement;

        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDeplacementPalette", Model = contratVM, ViewData = ViewData };
        //}




        public ActionResult OnDeplacerManuellement()
        {
            DeplacementPaletteViewModel mclass = new DeplacementPaletteViewModel();

            mclass._DeplacementPalette = new DeplacementPalette();
            mclass._DeplacementPalette.Palette = new Palette();
            mclass._DeplacementPalette.MagasinSource = new Magasin();
            mclass._DeplacementPalette.MagasinDestination = new Magasin();
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDeplacementPalette", Model = mclass };
        }


        public ActionResult GetPalettesByOrdreFabrication(string idOrdreFabrication)
        {
            if (string.IsNullOrEmpty(idOrdreFabrication))
            {
                return this.Store(new List<Palette>());
            }

            Guid ordreId;
            if (!Guid.TryParse(idOrdreFabrication, out ordreId))
            {
                return this.Store(new List<Palette>());
            }

            // Ici, vous devez appeler une méthode qui récupère les palettes pour cet ordre de fabrication
            // Par exemple : 
            var palettes = (new Palette()).fnPaletteByOf_Get(ordreId);

            return this.Store(palettes);
        }


        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                DeplacementPalette mClass = JSON.Deserialize<DeplacementPalette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = mClass.fnGet(mClass.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Contrat Periode loading failed.");

                if (mClass.Desactive)
                    result = mClass.fnActivate();
                else
                    result = mClass.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Contrat Periode Approval failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeForwardContract");

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
                    Title = "Contrat Periode : Approuver",
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
                DeplacementPalette mClass = JSON.Deserialize<DeplacementPalette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = mClass.fnGet(mClass.ID);

                if (!result)
                    throw new Exception("OnClose : Contrat Periode loading failed.");

                mClass.UtilisateurModification = (string)Session["userName"];
                result = mClass.fnClose();

                if (!result)
                    throw new Exception("OnClose : Contrat Periode Close failed.");

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeForwardContract");

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
                    Title = "Contrat Periode : Close",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }



        //[HttpPost]
        //public ActionResult SubmitFormMethod()
        //{

        //    try
        //    {
        //        DeplacementPalette mClass = new DeplacementPalette();

        //        Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

        //        if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
        //            mClass.IsNew = true;
        //        else
        //        {
        //            mClass.IsNew = false;

        //            mClass.fnGet(Guid.Parse(GetFormValue("TxtContratID")));

        //            if (mClass == null || mClass.ID == Guid.Empty)
        //                throw new Exception("SubmitFormMethod : Contrat Periode load failed.");
        //        }

        //        mClass = MapFormToObject(mClass);

        //        bool result = mClass.fnUpdate();

        //        if (result)
        //        {
        //            Store mstore = X.GetCmp<Store>("storeListeForwardContract");
        //            if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
        //            {
        //                mstore.Insert(0, mClass);
        //                X.GetCmp<RowSelectionModel>("rowSelectionListeForwardContract").Select(0);
        //            }
        //            else
        //            {
        //                ModelProxy mProxy = mstore.GetById(mClass.ID);

        //                mProxy.BeginEdit();

        //                mProxy.Set(mClass);

        //                mProxy.Commit();

        //                mProxy.EndEdit();
        //            }

        //            X.GetCmp<Window>("FormForwardContract").Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        X.MessageBox.Show(new MessageBoxConfig
        //        {
        //            Title = "Contrat Periode : Data Validation",
        //            Message = ex.Message,
        //            Buttons = MessageBox.Button.OK,
        //            Icon = MessageBox.Icon.WARNING
        //        });
        //    }

        //    return this.Direct();
        //}


        [HttpPost]
        public ActionResult SubmitFormMethod()
        {
            try
            {
                DeplacementPalette mClass = new DeplacementPalette();
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                {
                    mClass.IsNew = true;
                    mClass.UtilisateurCreation = (string)Session["userName"];
                }
                else
                {
                    mClass.IsNew = false;
                    Guid transfertId = Guid.Parse(GetFormValue("TxtTransfertID"));
                    if (!mClass.fnGet(transfertId))
                    {
                        throw new Exception("Le transfert que vous essayez de modifier n'a pas été trouvé.");
                    }
                    mClass.UtilisateurModification = (string)Session["userName"];
                }

                mClass = MapFormToObject(mClass);
                bool result = mClass.fnUpdate();

                if (result)
                {
                    // Construction de la réponse en cas de succès
                    DirectResult directResult = new DirectResult();

                    // Commande 1 : Fermer la fenêtre modale
                    X.GetCmp<Window>("FormDeplacementPaletteWindow").Close();

                    // Commande 2 : Afficher une notification de succès en haut à droite
                    X.Msg.Notify("Opération réussie", "Le déplacement de la palette a été enregistré avec succès.").Show();

                    // Commande 3 : Recharger le store principal pour voir la nouvelle ligne
                    // Assurez-vous que "storeListeForwardContract" est bien l'ID de votre grille principale
                    var mainStore = X.GetCmp<Store>("storeListeForwardContract");
                    if (mainStore != null)
                    {
                        mainStore.Reload();
                    }

                    return directResult;
                }
                else
                {
                    // Gère le cas où fnUpdate() retourne false sans lever d'exception
                    X.Msg.Alert("Échec", "L'enregistrement du déplacement a échoué. Veuillez contacter un administrateur.").Show();
                    return this.Direct();
                }
            }
            catch (Exception ex)
            {
                // Gère toutes les exceptions (validation, connexion BD, etc.)
                // et les affiche clairement à l'utilisateur.
                X.Msg.Alert("Erreur", "Une erreur est survenue : " + ex.Message).Show();
                return this.Direct();
            }
        }


        //public ActionResult SubmitExtendFormMethod()
        //{
        //    try
        //    {
        //        DeplacementPalette mClass = new DeplacementPalette();

        //        Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

        //        if (formExecMode != Tms.Components.Settings.EnumsDefinition.eExecMode.Extend)
        //            throw new Exception("SubmitFormMethod : Contrat Periode load failed.");
        //        else
        //        {
        //            mClass.IsNew = false;

        //            mClass.fnGet(Guid.Parse(GetFormValue("TxtContratID")));

        //            if (mClass == null || mClass.ID == Guid.Empty)
        //                throw new Exception("SubmitFormMethod : Contrat Periode load failed.");
        //        }

        //        mClass = MapFormToObject(mClass);

        //        bool result = mClass.fnExtend();

        //        if (result)
        //        {
        //            Store mstore = X.GetCmp<Store>("storeListeForwardContract");
        //            if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
        //            {
        //                mstore.Insert(0, mClass);
        //                X.GetCmp<RowSelectionModel>("rowSelectionListeForwardContract").Select(0);
        //            }
        //            else
        //            {
        //                ModelProxy mProxy = mstore.GetById(mClass.ID);

        //                mProxy.BeginEdit();

        //                mProxy.Set(mClass);

        //                mProxy.Commit();

        //                mProxy.EndEdit();
        //            }

        //            X.GetCmp<Window>("FormForwardContract").Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        X.MessageBox.Show(new MessageBoxConfig
        //        {
        //            Title = "Contrat Periode : Data Validation",
        //            Message = ex.Message,
        //            Buttons = MessageBox.Button.OK,
        //            Icon = MessageBox.Icon.WARNING
        //        });
        //    }

        //    return this.Direct();
        //}

        //public ActionResult SubmitRegeneration()
        //{
        //    try
        //    {
        //        DeplacementPalette mClass = new DeplacementPalette();

        //        Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

        //        if (formExecMode != Tms.Components.Settings.EnumsDefinition.eExecMode.Extend)
        //            throw new Exception("SubmitRegeneration : Contrat Periode load failed.");
        //        else
        //        {
        //            mClass.IsNew = false;

        //            mClass.fnGet(Guid.Parse(GetFormValue("TxtContratID")));

        //            if (mClass == null || mClass.ID == Guid.Empty)
        //                throw new Exception("SubmitRegeneration : Contrat Periode load failed.");
        //        }

        //        mClass = MapFormToObject(mClass);

        //        bool result = mClass.fnRegenerate();
        //        mClass.EstRelance = true;
        //        mClass.Statut = "RG";
        //        if (result)
        //        {
        //            Store mstore = X.GetCmp<Store>("storeListeForwardContract");
        //            if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
        //            {
        //                mstore.Insert(0, mClass);
        //                X.GetCmp<RowSelectionModel>("rowSelectionListeForwardContract").Select(0);
        //            }
        //            else
        //            {
        //                ModelProxy mProxy = mstore.GetById(mClass.ID);

        //                mProxy.BeginEdit();

        //                mProxy.Set(mClass);

        //                mProxy.Commit();

        //                mProxy.EndEdit();
        //            }

        //            X.GetCmp<Window>("FormRegenenerDeplacementPalette").Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        X.MessageBox.Show(new MessageBoxConfig
        //        {
        //            Title = "Contrat Periode : Data Validation",
        //            Message = ex.Message,
        //            Buttons = MessageBox.Button.OK,
        //            Icon = MessageBox.Icon.WARNING
        //        });
        //    }

        //    return this.Direct();
        //}

        //public ActionResult ApproveFormMethod()
        //{
        //    try
        //    {
        //        DeplacementPalette mClass = new DeplacementPalette();

        //        bool result = mClass.fnGet(Guid.Parse(GetFormValue("TxtContratID")));

        //        if (mClass == null || mClass.ID == Guid.Empty)
        //            throw new Exception("onApprove : Contrat Periode Approve failed.");

        //        mClass.UtilisateurCreation = (string)Session["userName"];
        //        mClass.UtilisateurModification = (string)Session["userName"];
        //        mClass.UtilisateurApprobation = (string)Session["userName"];
        //        mClass.ApprobationDate = DateTime.Now;

        //        result = mClass.fnApprove();

        //        mClass.Statut = "AP";
        //        if (result)
        //        {
        //            Store mstore = X.GetCmp<Store>("storeListeForwardContract");

        //            ModelProxy mProxy = mstore.GetById(mClass.ID);

        //            mProxy.BeginEdit();

        //            mProxy.Set(mClass);

        //            mProxy.Commit();

        //            mProxy.EndEdit();
        //        }

        //        X.GetCmp<Window>("FormForwardContract").Close();

        //    }
        //    catch (Exception ex)
        //    {
        //        X.MessageBox.Show(new MessageBoxConfig
        //        {
        //            Title = "Contrat Periode : Approuver",
        //            Message = ex.Message,
        //            Buttons = MessageBox.Button.OK,
        //            Icon = MessageBox.Icon.WARNING
        //        });
        //    }

        //    return this.Direct();
        //}


        //public ActionResult GenerateBalanceReport()
        //{
        //    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

        //    DeplacementPalette DeplacementPalette = new DeplacementPalette();

        //    //report.Parameters["cropyear"].Value = GetFormValue("CPcropYearForReport");
        //    //report.Parameters["fournisseurID"].Value = int.Parse(GetFormValue("CPfournisseurForReport"));
        //    //report.Parameters["typecontratID"].Value = int.Parse(GetFormValue("CPtypeContratForReport"));
        //    //report.Parameters["startdate"].Value = DateTime.Parse(X.GetCmp<DateField>("CPstartDateForReport").RawText.ToString());
        //    //report.Parameters["duedate"].Value = DateTime.Parse(X.GetCmp<DateField>("CPdueDateForReport").RawText.ToString());
        //    //report.Parameters["optionID"].Value = X.GetCmp<ComboBox>("CPtypeContratOptionForReport").SelectedItem.Value.ToString();

        //    DeplacementPalette.Campagne = new Campagne();
        //    DeplacementPalette.Campagne.Designation = GetFormValue("CPcropYearForReport");

        //    DeplacementPalette.Fournisseur = new Fournisseur();
        //    DeplacementPalette.Fournisseur.ID = int.Parse(GetFormValue("CPfournisseurForReport"));
        //    DeplacementPalette.Fournisseur.Nom = X.GetCmp<ComboBox>("CPfournisseurForReport").SelectedItem.Text;

        //    DeplacementPalette.DeplacementPaletteType = new DeplacementPaletteType();
        //    DeplacementPalette.DeplacementPaletteType.ID = int.Parse(GetFormValue("CPtypeContratForReport"));

        //    DeplacementPalette.DateDebut = DateTime.Parse(X.GetCmp<DateField>("CPstartDateForReport").RawText.ToString());
        //    DeplacementPalette.DateEcheance = DateTime.Parse(X.GetCmp<DateField>("CPdueDateForReport").RawText.ToString());
        //    DeplacementPalette.Statut = X.GetCmp<ComboBox>("CPtypeContratOptionForReport").SelectedItem.Value.ToString();

        //    DeplacementPalette.Sites = new Site();
        //    DeplacementPalette.Sites.ID = int.Parse(GetFormValue("CpcmbSite"));
        //    DeplacementPalette.Sites.Nom = X.GetCmp<ComboBox>("CpcmbSite").SelectedItem.Text;

        //    string param = JSON.Serialize(DeplacementPalette);
        //    Session["ParamReport"] = param;

        //    X.GetCmp<Window>("FormPrintBalanceCP").Close();
        //    //Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
        //    //mViewport.Unmask();

        //    //HttpUtility.JavaScriptStringEncode(param)
        //    return JavaScript(String.Format("CloseFormPrintBalanceCP(), addTab(window.parent.Ext.getCmp('tabCenter'), 'BalanceReport{0}', '{1}/DeplacementPalette/ViewBalanceReport', this, 'Contrat Periode - Balance','report')", Guid.NewGuid(), BaseUrl));
        //    //return this.Direct();
        //}

        //public ActionResult GenerateStatutLivReport()
        //{
        //    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

        //    DeplacementPalette DeplacementPalette = new DeplacementPalette();

        //    //report.Parameters["cropyear"].Value = GetFormValue("CPcropYearForReport");
        //    //report.Parameters["fournisseurID"].Value = int.Parse(GetFormValue("CPfournisseurForReport"));
        //    //report.Parameters["typecontratID"].Value = int.Parse(GetFormValue("CPtypeContratForReport"));
        //    //report.Parameters["startdate"].Value = DateTime.Parse(X.GetCmp<DateField>("CPstartDateForReport").RawText.ToString());
        //    //report.Parameters["duedate"].Value = DateTime.Parse(X.GetCmp<DateField>("CPdueDateForReport").RawText.ToString());
        //    //report.Parameters["optionID"].Value = X.GetCmp<ComboBox>("CPtypeContratOptionForReport").SelectedItem.Value.ToString();

        //    DeplacementPalette.Campagne = new Campagne();
        //    DeplacementPalette.Campagne.Designation = GetFormValue("CPcropYearForReport");

        //    DeplacementPalette.Fournisseur = new Fournisseur();
        //    DeplacementPalette.Fournisseur.ID = int.Parse(GetFormValue("CPfournisseurForReport"));
        //    DeplacementPalette.Fournisseur.Nom = X.GetCmp<ComboBox>("CPfournisseurForReport").SelectedItem.Text;

        //    DeplacementPalette.DeplacementPaletteType = new DeplacementPaletteType();
        //    DeplacementPalette.DeplacementPaletteType.ID = int.Parse(GetFormValue("CPtypeContratForReport"));

        //    DeplacementPalette.DateDebut = DateTime.Parse(X.GetCmp<DateField>("CPstartDateForReport").RawText.ToString());
        //    DeplacementPalette.DateEcheance = DateTime.Parse(X.GetCmp<DateField>("CPdueDateForReport").RawText.ToString());
        //    DeplacementPalette.Statut = X.GetCmp<ComboBox>("CPtypeContratOptionForReport").SelectedItem.Value.ToString();

        //    DeplacementPalette.Sites = new Site();
        //    DeplacementPalette.Sites.ID = int.Parse(GetFormValue("CpcmbSite"));
        //    DeplacementPalette.Sites.Nom = X.GetCmp<ComboBox>("CpcmbSite").SelectedItem.Text;

        //    string param = JSON.Serialize(DeplacementPalette);
        //    Session["ParamReport"] = param;

        //    X.GetCmp<Window>("FormPrintBalanceCP").Close();
        //    //Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
        //    //mViewport.Unmask();

        //    //HttpUtility.JavaScriptStringEncode(param)
        //    return JavaScript(String.Format("CloseFormPrintBalanceCP(), addTab(window.parent.Ext.getCmp('tabCenter'), 'StatusReport{0}', '{1}/DeplacementPalette/ViewStatusLivReport', this, 'Contrat Periode - Delivery-Balance/Contract','report')", Guid.NewGuid(), BaseUrl));
        //    //return this.Direct();
        //}

        ////public ActionResult GenerateStatutLivFinReport()
        //{
        //    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

        //    DeplacementPalette DeplacementPalette = new DeplacementPalette();

        //    //report.Parameters["cropyear"].Value = GetFormValue("CPcropYearForReport");
        //    //report.Parameters["fournisseurID"].Value = int.Parse(GetFormValue("CPfournisseurForReport"));
        //    //report.Parameters["typecontratID"].Value = int.Parse(GetFormValue("CPtypeContratForReport"));
        //    //report.Parameters["startdate"].Value = DateTime.Parse(X.GetCmp<DateField>("CPstartDateForReport").RawText.ToString());
        //    //report.Parameters["duedate"].Value = DateTime.Parse(X.GetCmp<DateField>("CPdueDateForReport").RawText.ToString());
        //    //report.Parameters["optionID"].Value = X.GetCmp<ComboBox>("CPtypeContratOptionForReport").SelectedItem.Value.ToString();

        //    DeplacementPalette.Campagne = new Campagne();
        //    DeplacementPalette.Campagne.Designation = GetFormValue("CPcropYearForReport");

        //    DeplacementPalette.Fournisseur = new Fournisseur();
        //    DeplacementPalette.Fournisseur.ID = int.Parse(GetFormValue("CPfournisseurForReport"));
        //    DeplacementPalette.Fournisseur.Nom = X.GetCmp<ComboBox>("CPfournisseurForReport").SelectedItem.Text;

        //    DeplacementPalette.DeplacementPaletteType = new DeplacementPaletteType();
        //    DeplacementPalette.DeplacementPaletteType.ID = int.Parse(GetFormValue("CPtypeContratForReport"));

        //    DeplacementPalette.DateDebut = DateTime.Parse(X.GetCmp<DateField>("CPstartDateForReport").RawText.ToString());
        //    DeplacementPalette.DateEcheance = DateTime.Parse(X.GetCmp<DateField>("CPdueDateForReport").RawText.ToString());
        //    DeplacementPalette.Statut = X.GetCmp<ComboBox>("CPtypeContratOptionForReport").SelectedItem.Value.ToString();

        //    DeplacementPalette.Sites = new Site();
        //    DeplacementPalette.Sites.ID = int.Parse(GetFormValue("CpcmbSite"));
        //    DeplacementPalette.Sites.Nom = X.GetCmp<ComboBox>("CpcmbSite").SelectedItem.Text;

        //    string param = JSON.Serialize(DeplacementPalette);
        //    Session["ParamReport"] = param;

        //    X.GetCmp<Window>("FormPrintBalanceCP").Close();
        //    //Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
        //    //mViewport.Unmask();

        //    //HttpUtility.JavaScriptStringEncode(param)
        //    return JavaScript(String.Format("CloseFormPrintBalanceCP(), addTab(window.parent.Ext.getCmp('tabCenter'), 'StatusReport{0}', '{1}/DeplacementPalette/ViewStatusLivFinReport', this, 'Contrat Periode - Delivery-Balance/Contract And Financing','report')", Guid.NewGuid(), BaseUrl));
        //    //return this.Direct();
        //}


        //public ActionResult GenerateExecutionReport()
        //{
        //    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

        //    DeplacementPalette DeplacementPalette = new DeplacementPalette();

        //    DeplacementPalette.Campagne = new Campagne();
        //    DeplacementPalette.Campagne.Designation = GetFormValue("CPcropYearForReport");

        //    DeplacementPalette.Fournisseur = new Fournisseur();
        //    DeplacementPalette.Fournisseur.ID = int.Parse(GetFormValue("CPfournisseurForReport"));
        //    DeplacementPalette.Fournisseur.Nom = X.GetCmp<ComboBox>("CPfournisseurForReport").SelectedItem.Text;

        //    DeplacementPalette.DeplacementPaletteType = new DeplacementPaletteType();
        //    DeplacementPalette.DeplacementPaletteType.ID = int.Parse(GetFormValue("CPtypeContratForReport"));

        //    DeplacementPalette.DateDebut = DateTime.Parse(X.GetCmp<DateField>("CPstartDateForReport").RawText.ToString());
        //    DeplacementPalette.DateEcheance = DateTime.Parse(X.GetCmp<DateField>("CPdueDateForReport").RawText.ToString());
        //    DeplacementPalette.Statut = X.GetCmp<ComboBox>("CPtypeContratOptionForReport").SelectedItem.Value.ToString();

        //    DeplacementPalette.Sites = new Site();
        //    DeplacementPalette.Sites.ID = int.Parse(GetFormValue("CpcmbSite"));
        //    DeplacementPalette.Sites.Nom = X.GetCmp<ComboBox>("CpcmbSite").SelectedItem.Text;

        //    string param = JSON.Serialize(DeplacementPalette);
        //    Session["ParamReport"] = param;

        //    //Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
        //    //mViewport.Unmask();
        //    this.Direct();
        //    //HttpUtility.JavaScriptStringEncode(param)
        //    return JavaScript(String.Format("CloseFormPrintBalanceCP(), addTab(window.parent.Ext.getCmp('tabCenter'), 'BalanceReport{0}', '{1}/DeplacementPalette/ViewExecutionReport', this, 'Contrat Periode - Execution','report')", Guid.NewGuid(), BaseUrl));
        //    //return this.Direct();
        //}


        //public ActionResult ViewBalanceReport()
        //{
        //    DeplacementPaletteBalanceReport report = new DeplacementPaletteBalanceReport();
        //    report.DataSource = DevExpressReportDs.SetDataSource(report);

        //    DeplacementPalette contrat = JSON.Deserialize<DeplacementPalette>((string)Session["ParamReport"], new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
        //    report.Parameters["titre"].Value = "Contrat Periode - Balance";
        //    report.Parameters["cropyear"].Value = contrat.Campagne.Designation;
        //    report.Parameters["fournisseurID"].Value = contrat.Fournisseur.ID;
        //    report.Parameters["typecontratID"].Value = contrat.DeplacementPaletteType.ID;
        //    report.Parameters["startdate"].Value = contrat.DateDebut;
        //    report.Parameters["duedate"].Value = contrat.DateEcheance;
        //    report.Parameters["optionID"].Value = contrat.Statut;
        //    report.Parameters["siteID"].Value = contrat.Sites.ID;

        //    //Afficher Les criteres sur l'etat
        //    report.Parameters["fournisseurNom"].Value = contrat.Fournisseur.Nom;
        //    report.Parameters["optionNom"].Value = contrat.Statut;
        //    report.Parameters["siteNom"].Value = contrat.Sites.Nom;

        //    ViewData["Report"] = report;

        //    return View();
        //}

        //public ActionResult ViewStatusLivReport()
        //{
        //    DeplacementPaletteStatutLivraison report = new DeplacementPaletteStatutLivraison();
        //    report.DataSource = DevExpressReportDs.SetDataSource(report);

        //    DeplacementPalette contrat = JSON.Deserialize<DeplacementPalette>((string)Session["ParamReport"], new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
        //    report.Parameters["titre"].Value = "Contrat Periode - Delivery-Balance/Contract";
        //    report.Parameters["cropyear"].Value = contrat.Campagne.Designation;
        //    report.Parameters["fournisseurID"].Value = contrat.Fournisseur.ID;
        //    report.Parameters["typecontratID"].Value = contrat.DeplacementPaletteType.ID;
        //    report.Parameters["startdate"].Value = contrat.DateDebut;
        //    report.Parameters["duedate"].Value = contrat.DateEcheance;
        //    report.Parameters["optionID"].Value = contrat.Statut;
        //    report.Parameters["siteID"].Value = contrat.Sites.ID;

        //    //Afficher Les criteres sur l'etat
        //    report.Parameters["fournisseurNom"].Value = contrat.Fournisseur.Nom;
        //    report.Parameters["optionNom"].Value = contrat.Statut;
        //    report.Parameters["siteNom"].Value = contrat.Sites.Nom;

        //    ViewData["Report"] = report;

        //    return View();
        //}

        //public ActionResult ViewStatusLivFinReport()
        //{
        //    DeplacementPaletteStatutLivraisonFinancing report = new DeplacementPaletteStatutLivraisonFinancing();
        //    report.DataSource = DevExpressReportDs.SetDataSource(report);

        //    DeplacementPalette contrat = JSON.Deserialize<DeplacementPalette>((string)Session["ParamReport"], new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
        //    report.Parameters["titre"].Value = "Contrat Periode - Delivery-Balance/Contract And Financing";
        //    report.Parameters["cropyear"].Value = contrat.Campagne.Designation;
        //    report.Parameters["fournisseurID"].Value = contrat.Fournisseur.ID;
        //    report.Parameters["typecontratID"].Value = contrat.DeplacementPaletteType.ID;
        //    report.Parameters["startdate"].Value = contrat.DateDebut;
        //    report.Parameters["duedate"].Value = contrat.DateEcheance;
        //    report.Parameters["optionID"].Value = contrat.Statut;
        //    report.Parameters["siteID"].Value = contrat.Sites.ID;

        //    //Afficher Les criteres sur l'etat
        //    report.Parameters["fournisseurNom"].Value = contrat.Fournisseur.Nom;
        //    report.Parameters["optionNom"].Value = contrat.Statut;
        //    report.Parameters["siteNom"].Value = contrat.Sites.Nom;

        //    ViewData["Report"] = report;

        //    return View("ViewStatusLivReport");
        //}


        //public ActionResult ViewExecutionReport()
        //{
        //    DeplacementPaletteExecutionReport report = new DeplacementPaletteExecutionReport();
        //    report.DataSource = DevExpressReportDs.SetDataSource(report);

        //    DeplacementPalette contrat = JSON.Deserialize<DeplacementPalette>((string)Session["ParamReport"], new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
        //    report.Parameters["titre"].Value = "Contrat Periode - Execution";
        //    report.Parameters["cropyear"].Value = contrat.Campagne.Designation;
        //    report.Parameters["fournisseurID"].Value = contrat.Fournisseur.ID;
        //    report.Parameters["typecontratID"].Value = contrat.DeplacementPaletteType.ID;
        //    report.Parameters["startdate"].Value = contrat.DateDebut;
        //    report.Parameters["duedate"].Value = contrat.DateEcheance;
        //    report.Parameters["optionID"].Value = contrat.Statut;
        //    report.Parameters["siteID"].Value = contrat.Sites.ID;

        //    //Afficher Les criteres sur l'etat
        //    report.Parameters["fournisseurNom"].Value = contrat.Fournisseur.Nom;
        //    report.Parameters["optionNom"].Value = contrat.Statut;
        //    report.Parameters["siteNom"].Value = contrat.Sites.Nom;

        //    ViewData["Report"] = report;

        //    return View();
        //}



        //private DeplacementPalette MapFormToObject(DeplacementPalette mClass)
        //{
        //    mClass.ID = Guid.Parse(X.GetCmp<TextField>("TxtContratID").Text.ToString());
        //    mClass.Campagne = new Campagne();
        //    mClass.Campagne.Designation = GetFormValue("CropYearID");

        //    mClass.Fournisseur = new Fournisseur();
        //    mClass.Fournisseur.ID = int.Parse(GetFormValue("FournisseurID"));
        //    mClass.Fournisseur.Nom = X.GetCmp<ComboBox>("FournisseurID").SelectedItem.Text.ToString();

        //    mClass.DeplacementPaletteType = new DeplacementPaletteType();
        //    mClass.DeplacementPaletteType.ID = int.Parse(GetFormValue("TypeContratID"));
        //    mClass.DeplacementPaletteType.Designation = X.GetCmp<ComboBox>("TypeContratID").SelectedItem.Text.ToString();

        //    mClass.DateContrat = DateTime.Parse(X.GetCmp<DateField>("TxtDateContrat").RawText.ToString());
        //    mClass.DateDebut = DateTime.Parse(X.GetCmp<DateField>("TxtStartDate").RawText.ToString());
        //    mClass.DateEcheance = DateTime.Parse(X.GetCmp<DateField>("TxtDueDate").RawText.ToString()).AddHours(23).AddMinutes(59);

        //    mClass.Tonnage = string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtTonnage").RawText) ? 0 : decimal.Parse(X.GetCmp<NumberField>("TxtTonnage").RawText);
        //    mClass.Montant = string.IsNullOrEmpty(X.GetCmp<NumberField>("txtAmount").RawText) ? 0 : decimal.Parse(X.GetCmp<NumberField>("txtAmount").RawText);
        //    //mClass.Balance = decimal.Parse(X.GetCmp<NumberField>("TxtTonnage").RawText);
        //    //mClass.Statut = "NA";
        //    mClass.Prix = string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtPrice").RawText) ? 0 : decimal.Parse(X.GetCmp<NumberField>("TxtPrice").RawText);

        //    mClass.MontantPrime = string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtPrime").RawText) ? 0 : decimal.Parse(X.GetCmp<NumberField>("TxtPrime").RawText);
        //    mClass.PrixBrut = string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtGrossPrice").RawText) ? 0 : decimal.Parse(X.GetCmp<NumberField>("TxtGrossPrice").RawText);

        //    mClass.Numero = X.GetCmp<Hidden>("hiddenNumeroPrice").Text;
        //    mClass.Commentaire = X.GetCmp<Hidden>("TxtCommentaire").Text;

        //    mClass.UtilisateurCreation = (string)Session["userName"];
        //    mClass.UtilisateurModification = (string)Session["userName"];

        //    mClass.Sites = new Site();
        //    string UserName = (string)Session["userName"];
        //    bool result = mClass.Sites.fnGetBySiteByUserName(UserName);

        //    if (!result)
        //        throw new Exception("Error On Default Siet");

        //    return mClass;
        //}

        //private DeplacementPalette MapFormToObject(DeplacementPalette mClass)
        //{
        //    // --- ID
        //    var idHidden = X.GetCmp<Hidden>("TxtTransfertID");
        //    if (idHidden != null && !string.IsNullOrEmpty(idHidden.Text))
        //    {
        //        mClass.ID = Guid.Parse(idHidden.Text);
        //    }
        //    else
        //    {
        //        mClass.ID = Guid.NewGuid();
        //    }

        //    // --- Palette (peut venir d'un ComboBox (value) ou d'un NumberField)
        //    Palette palette = new Palette();
        //    palette.ID = Guid.Parse(X.GetCmp<ComboBox>("cmbPalette").SelectedItem.Value.ToString());
        //    palette.Numero = int.Parse(X.GetCmp<ComboBox>("cmbPalette").SelectedItem.Text);

        //    Magasin magasinSource= new Magasin();
        //    magasinSource.ID = int.Parse(X.GetCmp<ComboBox>("cmbMagasinSource").SelectedItem.Value.ToString());
        //    magasinSource.Designation = X.GetCmp<ComboBox>("cmbMagasinSource").SelectedItem.Text;

        //    // --- Date de départ
        //    try
        //    {
        //        var df = X.GetCmp<DateField>("DateDepart");
        //        if (df != null && !string.IsNullOrEmpty(df.RawText))
        //            mClass.DateDepart = DateTime.Parse(df.RawText);
        //        else if (!string.IsNullOrEmpty(GetFormValue("DateDepart")))
        //            mClass.DateDepart = DateTime.Parse(GetFormValue("DateDepart"));
        //    }
        //    catch { mClass.DateDepart = DateTime.MinValue; }

        //    // --- Magasin de destination
        //    Magasin MagasinDest = new Magasin();
        //    MagasinDest.ID = int.Parse(X.GetCmp<ComboBox>("cmbMagasinDest").SelectedItem.Value.ToString());
        //    MagasinDest.Designation = X.GetCmp<ComboBox>("cmbMagasinDest").SelectedItem.Text;

        //    mClass.EmplacementDestinatination = GetFormValue("TxtEmplacementDest") ?? X.GetCmp<TextField>("TxtEmplacementDest")?.Text ?? string.Empty;
        //    // --- Date d'arrivée
        //    try
        //    {
        //        var da = X.GetCmp<DateField>("DateArrivee");
        //        if (da != null && !string.IsNullOrEmpty(da.RawText))
        //            mClass.DateArrivee = DateTime.Parse(da.RawText);
        //        else if (!string.IsNullOrEmpty(GetFormValue("DateArrivee")))
        //            mClass.DateArrivee = DateTime.Parse(GetFormValue("DateArrivee"));
        //    }
        //    catch { mClass.DateArrivee = DateTime.MinValue; }

        //    // --- Description / Operateur / ModeDeTransfert / Statut
        //    mClass.Description = GetFormValue("Description") ?? X.GetCmp<TextArea>("TxtDescription")?.Text ?? string.Empty;
        //    mClass.Operateur = GetFormValue("Operateur") ?? X.GetCmp<TextField>("TxtOperateur")?.Text ?? string.Empty;
        //    mClass.ModeDeTransfert = GetFormValue("cmbModeTransfert") ?? X.GetCmp<ComboBox>("cmbModeTransfert")?.SelectedItem?.Text ?? string.Empty;
        //    //mClass.Statut = GetFormValue("Statut") ?? X.GetCmp<ComboBox>("CbStatut")?.SelectedItem?.Text ?? string.Empty;


        //    // --- Utilisateurs de création / modification
        //    string userName = (string)Session["userName"];
        //    if (!string.IsNullOrEmpty(userName))
        //    {
        //        // Si nouvel enregistrement, remplir UtilisateurCreation, sinon seulement modification
        //        if (mClass.UtilisateurCreation == null || mClass.UtilisateurCreation.Trim() == string.Empty)
        //            mClass.UtilisateurCreation = userName;

        //        mClass.UtilisateurModification = userName;
        //    }


        //    return mClass;
        //}

        private DeplacementPalette MapFormToObject(DeplacementPalette mClass)
        {
            try
            {
                // --- Palette
                string paletteValue = GetFormValue("cmbPalette");
                if (!string.IsNullOrEmpty(paletteValue))
                {
                    mClass.Palette = new Palette { ID = Guid.Parse(paletteValue) };
                }
                else
                {
                    throw new Exception("Veuillez sélectionner une palette.");
                }

                // --- Magasin Source
                string magasinSourceValue = GetFormValue("cmbMagasinSource");
                if (!string.IsNullOrEmpty(magasinSourceValue))
                {
                    mClass.MagasinSource = new Magasin { ID = int.Parse(magasinSourceValue) };
                }
                else
                {
                    throw new Exception("Veuillez sélectionner le magasin source.");
                }

                // --- Magasin Destination
                string magasinDestValue = GetFormValue("cmbMagasinDest");
                if (!string.IsNullOrEmpty(magasinDestValue))
                {
                    mClass.MagasinDestination = new Magasin { ID = int.Parse(magasinDestValue) };
                }
                else
                {
                    throw new Exception("Veuillez sélectionner le magasin de destination.");
                }

                // --- Date de départ (obligatoire)
                string dateDepartValue = GetFormValue("DateDepart");
                if (!string.IsNullOrEmpty(dateDepartValue))
                {
                    mClass.DateDepart = DateTime.Parse(dateDepartValue);
                }
                else
                {
                    throw new Exception("Veuillez saisir la date de départ.");
                }

                // --- Date d'arrivée (optionnelle)
                string dateArriveeValue = GetFormValue("DateArrivee");
                if (!string.IsNullOrEmpty(dateArriveeValue))
                {
                    mClass.DateArrivee = DateTime.Parse(dateArriveeValue);
                }
                else
                {
                    mClass.DateArrivee = DateTime.MinValue;
                }

                // --- Emplacement destination (obligatoire)
                string emplacementDest = GetFormValue("TxtEmplacementDest");
                if (!string.IsNullOrEmpty(emplacementDest))
                {
                    mClass.EmplacementDestination = emplacementDest;
                }
                else
                {
                    throw new Exception("Veuillez saisir l'emplacement de destination.");
                }

                // --- Champs optionnels
                mClass.ModeDeTransfert = GetFormValue("cmbModeTransfert") ?? "Manuel";
                mClass.Operateur = GetFormValue("TxtOperateur") ?? "";
                mClass.Description = GetFormValue("TxtDescription") ?? "";

                return mClass;
            }
            catch (FormatException ex)
            {
                throw new Exception($"Erreur de format dans les données saisies: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la validation des données: {ex.Message}");
            }
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
            //X.Js.Call("App.rowSelectionListeForwardContract.deselectAll();");
            X.GetCmp<RowSelectionModel>("rowSelectionListeForwardContract").DeselectAll();
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

        //public ActionResult OnDisplayForwardContractList(string ItemPeriodStart = "", string ItemPeriodEnd = "")
        //{
        //    DeplacementPalette mContrat = new DeplacementPalette();
        //    try
        //    {

        //        ViewData["Titre"] = "Liste Des Contrats";
        //        ViewData["actionToDo"] = "OnPrintForwardContractList";
        //        ViewData["ControllerName"] = "DeplacementPalette";
        //        if (string.IsNullOrEmpty(ItemPeriodStart)) ItemPeriodStart = DateTime.Now.ToShortDateString();
        //        if (string.IsNullOrEmpty(ItemPeriodEnd)) ItemPeriodEnd = DateTime.Now.ToShortDateString();

        //        DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
        //        DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);
        //        ViewData["datedebut"] = datedebut;
        //        ViewData["datefin"] = datedfin;
        //        mContrat.DateDebut = datedebut;
        //        mContrat.DateEcheance = datedfin;

        //        mContrat.Sites = new Site();
        //        Site mSiteParDefaut = new Site();
        //        string UserName = (string)Session["userName"];
        //        bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

        //        if (result)
        //            mContrat.Sites.ID = mSiteParDefaut.ID;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "frmCriteriaForForwardContract", Model = mContrat, ViewData = ViewData };
        //}

        //public ActionResult OnDisplayPrimeList(string ItemPeriodStart = "", string ItemPeriodEnd = "")
        //{
        //    DeplacementPalette mContrat = new DeplacementPalette();
        //    try
        //    {

        //        if (string.IsNullOrEmpty(ItemPeriodStart)) ItemPeriodStart = DateTime.Now.ToShortDateString();
        //        if (string.IsNullOrEmpty(ItemPeriodEnd)) ItemPeriodEnd = DateTime.Now.ToShortDateString();
        //        DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
        //        DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);
        //        ViewData["datedebut"] = datedebut;
        //        ViewData["datefin"] = datedfin;
        //        mContrat.DateDebut = datedebut;
        //        mContrat.DateEcheance = datedfin;

        //        mContrat.Sites = new Site();
        //        Site mSiteParDefaut = new Site();
        //        string UserName = (string)Session["userName"];
        //        bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

        //        if (result)
        //            mContrat.Sites.ID = mSiteParDefaut.ID;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "frmCriteriaForPrime", Model = mContrat, ViewData = ViewData };
        //}

        //public ActionResult OnPrintForwardContractList(string cropyear, string fournisseur, string fournisseurText, string typecontract, string typecontractText, string startDate, string endDate, string statut, string statutText, string siteID, string SiteNom)
        //{
        //    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
        //    try
        //    {
        //        DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
        //        DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

        //        //Session["paramCropYear"] = cropYear;                
        //        Session["paramCampagne"] = cropyear;
        //        Session["paramFournisseur"] = fournisseur;
        //        Session["paramFournisseurText"] = fournisseurText;
        //        Session["paramTypeOfContract"] = typecontract;
        //        Session["paramTypeOfContractText"] = typecontractText;
        //        Session["paramStatut"] = statut;
        //        Session["paramStatutText"] = statutText;

        //        Session["paramStartDate"] = dateDebut;
        //        Session["paramEndDate"] = dateFin;

        //        Session["paramSite"] = siteID;
        //        Session["paramSiteText"] = SiteNom;

        //        Session["paramOption"] = X.GetCmp<ComboBox>("cmbDetOption").SelectedItem.Value.ToString();
        //        Session["paramOptionText"] = X.GetCmp<ComboBox>("cmbDetOption").SelectedItem.Text;
        //        return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/DeplacementPalette/ViewReportResult', this, 'Contrats Periode',''),App.frmCriteriaForForwardContract.doClose()", Guid.NewGuid(), BaseUrl));
        //    }
        //    catch (Exception ex)
        //    {
        //        X.MessageBox.Show(new MessageBoxConfig
        //        {
        //            Title = "Contrat Periode : Data Validation",
        //            Message = ex.Message,
        //            Buttons = MessageBox.Button.OK,
        //            Icon = MessageBox.Icon.WARNING
        //        });
        //        return this.Direct();
        //    }


        //}

        public ActionResult ViewReportResult()
        {
            //XtraReport report = null;

            rptForwardContractList report = new rptForwardContractList();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramCampagneText"].Value = Session["paramCampagne"];
            report.Parameters["paramCampagne"].Value = Session["paramCampagne"];
            //if (!string.IsNullOrEmpty(Session["paramCampagne"].ToString()) && Session["paramCampagne"].ToString() == "{Tous}")
            //    report.Parameters["paramCampagne"].Value = string.Empty;
            //else
            //    report.Parameters["paramCampagne"].Value = Session["paramCampagne"];

            report.Parameters["paramFournisseur"].Value = Session["paramFournisseur"];
            report.Parameters["paramFournisseurText"].Value = Session["paramFournisseurText"];

            report.Parameters["paramTypeOfContract"].Value = Session["paramTypeOfContract"];
            report.Parameters["paramTypeOfContractText"].Value = Session["paramTypeOfContractText"];

            report.Parameters["paramStatut"].Value = Session["paramStatut"];
            report.Parameters["paramStatutText"].Value = Session["paramStatutText"];

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];
            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            report.Parameters["siteID"].Value = Session["paramSite"];
            report.Parameters["paramSiteNomText"].Value = Session["paramSiteText"];

            report.Parameters["paramOption"].Value = Session["paramOption"];
            report.Parameters["paramOptionText"].Value = Session["paramOptionText"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

        public ActionResult OnPrintExecutionDetail(string IdContrat)
        {

            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'ContratDetail{0}', '{1}/DeplacementPalette/ViewReport?id={0}', this, 'Execution des contrats','')", IdContrat, BaseUrl));
        }

        //public ActionResult ViewReport(string id)
        //{
        //    DeplacementPaletteDetailExecutionReport report = new DeplacementPaletteDetailExecutionReport();

        //    report.DataSource = DevExpressReportDs.SetDataSource(report);
        //    report.Parameters["contratID"].Value = id;

        //    ViewData["Report"] = report;

        //    return View("ViewReportResult");
        //}

        //public ActionResult PrintBonus()
        //{
        //    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

        //    DeplacementPalette DeplacementPalette = new DeplacementPalette();

        //    //report.Parameters["cropyear"].Value = GetFormValue("CPcropYearForReport");
        //    //report.Parameters["fournisseurID"].Value = int.Parse(GetFormValue("CPfournisseurForReport"));
        //    //report.Parameters["typecontratID"].Value = int.Parse(GetFormValue("CPtypeContratForReport"));
        //    //report.Parameters["startdate"].Value = DateTime.Parse(X.GetCmp<DateField>("CPstartDateForReport").RawText.ToString());
        //    //report.Parameters["duedate"].Value = DateTime.Parse(X.GetCmp<DateField>("CPdueDateForReport").RawText.ToString());
        //    //report.Parameters["optionID"].Value = X.GetCmp<ComboBox>("CPtypeContratOptionForReport").SelectedItem.Value.ToString();

        //    DeplacementPalette.Campagne = new Campagne();
        //    DeplacementPalette.Campagne.Designation = GetFormValue("cmbDetCropYearP");

        //    DeplacementPalette.Sites = new Site();
        //    DeplacementPalette.Sites.ID = int.Parse(GetFormValue("CpDetcmbSiteP"));
        //    DeplacementPalette.Sites.Nom = X.GetCmp<ComboBox>("CpDetcmbSiteP").SelectedItem.Text;

        //    DeplacementPalette.Fournisseur = new Fournisseur();
        //    DeplacementPalette.Fournisseur.ID = int.Parse(GetFormValue("cmbDetFournisseurP"));
        //    DeplacementPalette.Fournisseur.Nom = X.GetCmp<ComboBox>("cmbDetFournisseurP").SelectedItem.Text;

        //    //DeplacementPalette.DeplacementPaletteType = new DeplacementPaletteType();
        //    //DeplacementPalette.DeplacementPaletteType.ID = int.Parse(GetFormValue("cmbDetTypeContrat"));

        //    DeplacementPalette.DateDebut = DateTime.Parse(X.GetCmp<DateField>("dtfDetStartDateP").RawText.ToString());
        //    DeplacementPalette.DateEcheance = DateTime.Parse(X.GetCmp<DateField>("dtfDetEndDateP").RawText.ToString());
        //    DeplacementPalette.Statut = X.GetCmp<ComboBox>("cmbDetStatusP").SelectedItem.Value.ToString();
        //    DeplacementPalette.StatutText = X.GetCmp<ComboBox>("cmbDetStatusP").SelectedItem.Text;

        //    string param = JSON.Serialize(DeplacementPalette);
        //    Session["ParamReport"] = param;

        //    X.GetCmp<Window>("frmCriteriaForPrime").Close();
        //    //Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
        //    //mViewport.Unmask();

        //    //HttpUtility.JavaScriptStringEncode(param)
        //    return JavaScript(String.Format("CloseFormPrimeCP(), addTab(window.parent.Ext.getCmp('tabCenter'), 'ListBonus{0}', '{1}/DeplacementPalette/ViewListBonus', this, 'List Of Bonus','report')", Guid.NewGuid(), BaseUrl));
        //    //return this.Direct();
        //}

        //public ActionResult ViewListBonus()
        //{
        //    rptPrimeList report = new rptPrimeList();
        //    report.DataSource = DevExpressReportDs.SetDataSource(report);

        //    DeplacementPalette contrat = JSON.Deserialize<DeplacementPalette>((string)Session["ParamReport"], new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
        //    //report.Parameters["titre"].Value = "Contrat Periode - List Of Bonus";
        //    report.Parameters["paramCampagneText"].Value = contrat.Campagne.Designation;
        //    report.Parameters["paramCampagne"].Value = contrat.Campagne.Designation;
        //    report.Parameters["paramFournisseur"].Value = contrat.Fournisseur.ID;
        //    report.Parameters["paramFournisseurText"].Value = contrat.Fournisseur.Nom;
        //    //report.Parameters["typecontratID"].Value = contrat.DeplacementPaletteType.ID;
        //    report.Parameters["paramStatut"].Value = contrat.Statut;
        //    report.Parameters["paramStatutText"].Value = contrat.StatutText;

        //    report.Parameters["paramDateDebut"].Value = contrat.DateDebut;
        //    report.Parameters["paramDateFin"].Value = contrat.DateEcheance;
        //    //report.Parameters["optionID"].Value = contrat.Statut;
        //    report.Parameters["siteID"].Value = contrat.Sites.ID;
        //    report.Parameters["paramSiteNomText"].Value = contrat.Sites.Nom;

        //    ViewData["Report"] = report;

        //    return View("ViewStatusLivReport");
        //}


    }
}