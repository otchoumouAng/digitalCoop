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
using Tms.Classes.Business.stock;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Classes.Shared.Sales;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class MouvementStockProduitController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        // GET: MouvementStockProduit
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);
            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            string StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("dtpFiltreStartDate").RawText = StartDate;
            X.GetCmp<DateField>("dtpFiltreEndDate").RawText = EndDate;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.BaseUrl = mParam.Base_url;

            X.GetCmp<FormPanel>("MouvementStockProduitCP").SetTitle("Filtres de recherche des mouvements de stock");

            #region Gestion des permissions d'accès aux magasins (Logique existante préservée)
            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{1AA7BB54-F177-4C44-A884-249A2E372FD0}"), UserName);
            HashSet<Guid> mlisteFonctions = new HashSet<Guid>(mListe.Cast<Fonction>().Select(f => f.ID).ToList());

            bool HavAccessMagasinTV = mlisteFonctions.Contains(Guid.Parse("{11D10A36-53B0-4961-9E5D-C2F67B4AB6AE}"));
            bool HavAccessMagasinExport = mlisteFonctions.Contains(Guid.Parse("{6866AFF0-DB9E-4FA7-AF90-C54B871E689F}"));

            if (HavAccessMagasinTV && HavAccessMagasinExport)
            {
                ViewData["LoadMagasin"] = "LoadActiveMagasinAll";
                ViewData["magasinID"] = mParam.MagasinTV;
            }
            else if (HavAccessMagasinTV)
            {
                ViewData["LoadMagasin"] = "LoadOnlyMagasinTV";
                ViewData["magasinID"] = mParam.MagasinTV;
            }
            else if (HavAccessMagasinExport)
            {
                ViewData["LoadMagasin"] = "LoadOnlyMagasinExport";
                ViewData["magasinID"] = mParam.MagasinExport;
            }
            else
            {
                ViewData["LoadMagasin"] = ""; // Ne charge rien si pas d'accès
                ViewData["magasinID"] = "0";
            }
            #endregion

            return View();
        }


        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("MouvementStockProduitCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemMagasin, string ItemProduction, string ItemArticle, string ItemMouvementTypeID, string ItemSens, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatut)
        {
            int magasinID = GetCriteriaValue(ItemMagasin);
            Guid? productionID = GetCriteriaValueGuid(ItemProduction);
            Guid? articleID = GetCriteriaValueGuid(ItemArticle);
            int mouvementTypeID = GetCriteriaValue(ItemMouvementTypeID);
            int sens = string.IsNullOrEmpty(ItemSens) ? -2 : int.Parse(ItemSens);
            DateTime? startDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart);
            DateTime? endDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd);
            string statut = (ItemStatut == "-1" || string.IsNullOrEmpty(ItemStatut)) ? null : ItemStatut;
            //string statut = string.IsNullOrEmpty(ItemStatut) ? "-1" : ItemStatut;

            // Appel de la nouvelle méthode métier fnSelect
            var mListe = (new MouvementStockProduit()).fnSelect(magasinID, productionID, articleID, mouvementTypeID, sens, startDate, endDate, statut);

            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemMagasin, string ItemProduction, string ItemArticle, string ItemMouvementTypeID, string ItemSens, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatut, string ItemSite)
        {
            try
            {
                Store mstore = X.GetCmp<Store>("storeListeMouvementStockProduit");

                // Rechargement du store avec les nouveaux paramètres
                mstore.Reload(new Ext.Net.ParameterCollection()
                {
                    new Ext.Net.Parameter("ItemMagasin", ItemMagasin),
                    new Ext.Net.Parameter("ItemProduction", ItemProduction),
                    new Ext.Net.Parameter("ItemArticle", ItemArticle),
                    new Ext.Net.Parameter("ItemMouvementTypeID", ItemMouvementTypeID),
                    new Ext.Net.Parameter("ItemSens", ItemSens),
                    new Ext.Net.Parameter("ItemPeriodStart", ItemPeriodStart),
                    new Ext.Net.Parameter("ItemPeriodEnd", ItemPeriodEnd),
                    new Ext.Net.Parameter("ItemStatut", ItemStatut)
                });

                X.GetCmp<FormPanel>("MouvementStockProduitCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Erreur de rafraîchissement",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult onAdd()
        {
            MouvementStockProduitViewModel mclass = new MouvementStockProduitViewModel();

            mclass._MouvementStockProduit = new MouvementStockProduit();
            mclass._DefaultCampagne = (new Parametres(0)).Campagne;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMouvementStockProduit", Model = mclass, };

        }

        public ActionResult onEdit(string ItemSelected)
        {
            MouvementStockProduitViewModel mclass = new MouvementStockProduitViewModel();

            mclass._MouvementStockProduit = new MouvementStockProduit();
            mclass._DefaultCampagne = (new Parametres(0)).Campagne;

            mclass._MouvementStockProduit = JSON.Deserialize<MouvementStockProduit>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMouvementStockProduit", Model = mclass };

        }


        public ActionResult onApprove()
        {
            MouvementStockProduit mclass = new MouvementStockProduit();

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();

            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;

            Fonction HasAccess = new Fonction();
            Parametres mParam = new Parametres(0);

            bool HavAccessMagasinTV = HasAccess.fnGetUserAccessStatus("{11D10A36-53B0-4961-9E5D-C2F67B4AB6AE}", UserName);
            bool HavAccessMagasinExport = HasAccess.fnGetUserAccessStatus("{6866AFF0-DB9E-4FA7-AF90-C54B871E689F}", UserName);

            if (HavAccessMagasinTV && HavAccessMagasinExport)
            {
                ViewData["LoadMagasin"] = "LoadActiveMagasin";
                ViewData["magasinID"] = mParam.MagasinTV;
            }
            else if (HavAccessMagasinTV == true)
            {
                ViewData["LoadMagasin"] = "LoadOnlyMagasinTV";
                ViewData["magasinID"] = mParam.MagasinTV;
            }
            else if (HavAccessMagasinExport == true)
            {
                ViewData["LoadMagasin"] = "LoadOnlyMagasinExport";
                ViewData["magasinID"] = mParam.MagasinExport;
            }
            else
            {
                ViewData["LoadMagasin"] = "";
                ViewData["magasinID"] = "0";
            }
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormApproveMouvement", Model = mclass, ViewData = ViewData };
        }

        [HttpPost]
        public ActionResult UpdateFormMethod()
        {
            try
            {
                MouvementStockProduit mClass = new MouvementStockProduit();
                bool result = true;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("txtMouvementID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("UpdateFormMethod : Stock Transaction - load failed.");


                    //if (!string.IsNullOrEmpty(mClass.Approbateur) || mClass.DateApprobation != null)
                    //    throw new Exception("UpdateFormMethod : Stock Transaction Already approved ! Please Refresh Overview");
                }

                mClass = MapFormToObject(mClass);

                result = mClass.fnUpdate();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListeMouvementStockProduit");
                    GridPanel mGrid = X.GetCmp<GridPanel>("grpListeMouvementStockProduit");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowMouvementStockProduit").Select(0);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    CloseDetailWindow();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stock Transaction : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnCancel(string ItemSelected)
        {
            try
            {
                MouvementStockProduit MouvementStockProduit = JSON.Deserialize<MouvementStockProduit>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = MouvementStockProduit.fnGet(MouvementStockProduit.ID);

                if (!result)
                    throw new Exception("OnCancel : Stock Transaction loading failed.");

                MouvementStockProduit.UtilisateurModification = (string)Session["userName"];

                if (MouvementStockProduit.Desactive)
                    result = MouvementStockProduit.fnActivate();
                else
                    result = MouvementStockProduit.fnDeActivate();

                if (!result)
                    throw new Exception("OnCancel : Stock Transaction, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeMouvementStockProduit");

                    ModelProxy mProxy = mstore.GetById(MouvementStockProduit.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(MouvementStockProduit);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stock Transaction : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        private MouvementStockProduit MapFormToObject(MouvementStockProduit mClass)
        {
            // Hydratation des objets à partir des valeurs du formulaire
            mClass.Magasin = new Magasin { ID = int.Parse(GetFormValue("cmbMagasin")) };
            mClass.MouvementStockProduitType = new MouvementStockType { ID = int.Parse(GetFormValue("cmbMouvementType")) };
            mClass.DateMouvement = DateTime.Parse(GetFormValue("txtDateMouvement"));
            mClass.Sens = Int16.Parse(GetFormValue("cmbSens"));
            mClass.Statut = "AP"; // Statut par défaut à la création
            mClass.UtilisateurCreation = (string)Session["userName"];

            // Hydratation des objets complexes
            mClass.Palette = new Palette { ID = Guid.Parse(GetFormValue("cmbPalette")) };
            mClass.OrdreDeFabrication = new OrdreFabrication { ID = Guid.Parse(GetFormValue("cmbOrdreFabrication")) };

            // L'objet Article contient maintenant toutes les informations de poids
            mClass.Article = new Article();
            mClass.Article.fnGet(Guid.Parse(GetFormValue("cmbArticle"))); // On charge l'article pour récupérer ses infos par défaut

            mClass.CodeConditionnement = new Conditionnement { ID = int.Parse(GetFormValue("cmbConditionnement")) };
            mClass.CodeReferenceConditionnement = new ConditionnementReference { Reference = GetFormValue("txtRefConditionnement") };

            return mClass;
        }

        public ActionResult OnPrintList()
        {
            Parametres mParam = new Parametres(0);
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            Site mSiteParDefaut = new Site();
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{1AA7BB54-F177-4C44-A884-249A2E372FD0}"), UserName);
            HashSet<Guid> mlisteFonctions = new HashSet<Guid>(mListe.Cast<Fonction>().Select(f => f.ID).ToList());

            bool HavAccessMagasinTV = false;
            if (mlisteFonctions.Contains(Guid.Parse("{11D10A36-53B0-4961-9E5D-C2F67B4AB6AE}")))
                HavAccessMagasinTV = true;

            bool HavAccessMagasinExport = false;
            if (mlisteFonctions.Contains(Guid.Parse("{6866AFF0-DB9E-4FA7-AF90-C54B871E689F}")))
                HavAccessMagasinExport = true;

            if (HavAccessMagasinTV && HavAccessMagasinExport)
            {
                ViewData["LoadMagasin"] = "LoadActiveMagasin";
                ViewData["magasinID"] = mParam.MagasinTV;
            }
            else if (HavAccessMagasinTV == true)
            {
                ViewData["LoadMagasin"] = "LoadOnlyMagasinTV";
                ViewData["magasinID"] = mParam.MagasinTV;
            }
            else if (HavAccessMagasinExport == true)
            {
                ViewData["LoadMagasin"] = "LoadOnlyMagasinExport";
                ViewData["magasinID"] = mParam.MagasinExport;
            }
            else
            {
                ViewData["LoadMagasin"] = "";
                ViewData["magasinID"] = "0";
            }

            ViewData["Campagne"] = mParam.Campagne;
            ViewData["Exportateur"] = mParam.Exportateur.ID;

            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;
            if (mParam.Site == mSiteParDefaut.ID) ViewData["UrlSite"] = "LoadSiteAll";
            else ViewData["UrlSite"] = "LoadSiteByAccess";

            return new Ext.Net.MVC.PartialViewResult { ViewName = "MouvementStockProduit_Print", ViewData = ViewData };
        }

        public ActionResult PrintList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;

                report = new rptStockTransactionList() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["campagneID"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                report.Parameters["paramSite"].Value = int.Parse(X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Value);
                report.Parameters["paramSiteText"].Value = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text;

                report.Parameters["paramExportateur"].Value = int.Parse(X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Value);
                report.Parameters["paramExportateurNom"].Value = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text;

                report.Parameters["paramCertification"].Value = int.Parse(X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Value);
                report.Parameters["paramCertificationText"].Value = X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Text;

                report.Parameters["paramMagasin"].Value = int.Parse(X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Value);
                report.Parameters["paramMagasinText"].Value = X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Text;

                report.Parameters["paramDirection"].Value = int.Parse(X.GetCmp<ComboBox>("cmbDirection").SelectedItem.Value);
                report.Parameters["paramDirectionText"].Value = X.GetCmp<ComboBox>("cmbDirection").SelectedItem.Text;

                report.Parameters["paramMouvementType"].Value = int.Parse(X.GetCmp<ComboBox>("cmbMouvementType").SelectedItem.Value);
                report.Parameters["paramMouvementTypeText"].Value = X.GetCmp<ComboBox>("cmbMouvementType").SelectedItem.Text;

                report.Parameters["paramPosition"].Value = int.Parse(X.GetCmp<ComboBox>("cmbEmplacement").SelectedItem.Value);
                report.Parameters["paramPositionText"].Value = X.GetCmp<ComboBox>("cmbEmplacement").SelectedItem.Text;

                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("dtpStartDate").RawText);
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("dtpEndDate").RawText);

                report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("CmbStatut").SelectedItem.Value.ToString();
                report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("CmbStatut").SelectedItem.Text;

                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/MouvementStockProduit/ViewList', this, 'List Of stock transaction',''),App.MouvementStockProduit_Print.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stock Transaction : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
        }

        public ActionResult ViewList()
        {
            XtraReport report = null;
            report = Session["report"] as XtraReport;
            //report = new rptLotsList() as XtraReport;

            //report.DataSource = DevExpressReportDs.SetDataSource(report);
            //report.Parameters["campagneID"].Value = Session["CampagneID"];
            //report.Parameters["paramCampagneText"].Value = Session["paramCampagneText"];

            //report.Parameters["paramExportateur"].Value = int.Parse(Session["paramExportateur"].ToString());
            //report.Parameters["paramExportateurNom"].Value = Session["paramExportateurNom"];

            //report.Parameters["paramCertification"].Value = int.Parse(Session["paramCertification"].ToString());
            //report.Parameters["paramCertificationText"].Value = Session["paramCertificationText"];

            //report.Parameters["paramLotType"].Value = int.Parse(Session["paramLotType"].ToString());
            //report.Parameters["paramLotTypeText"].Value = Session["paramLotTypeText"];

            //report.Parameters["paramDateDebut"].Value = DateTime.Parse(Session["paramDateDebut"].ToString());
            //report.Parameters["paramDateFin"].Value = DateTime.Parse(Session["paramDateFin"].ToString());

            //report.Parameters["paramStatut"].Value = int.Parse(Session["paramStatut"].ToString());
            //report.Parameters["paramStatutText"].Value = Session["paramStatutText"];            

            ViewData["Report"] = report;

            return View("ViewReportResult");
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

        private void CloseDetailWindow()
        {
            X.GetCmp<Window>("FormMouvementStockProduit").Close();
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

        private Guid? GetCriteriaValueGuid(string strComponent)
        {
            Guid value;
            return !string.IsNullOrEmpty(strComponent) && Guid.TryParse(strComponent, out value) ? (Guid?)value : null;
        }
    }
}