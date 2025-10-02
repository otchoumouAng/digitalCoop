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
using Tms.Classes.Business.stock;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;

namespace Tms2017.MVC.Controllers
{
    public class LotSituationStockController : BaseController
    {
        // GET: SituationStockAgence
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);
            string Currentcampagne = mParam.Campagne;
            //int MagasinID = mParam.Magasin.ID;
            //string magasin = mParam.Magasin.Designation;

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();

            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            Magasin mMagasin = new Magasin();
            result = mMagasin.fnGetDefaultBySite(mSiteParDefaut.ID);

            ViewData["LoadMagasin"] = "LoadActiveMagasin";
            ViewBag.MagasinParDefaut = mMagasin.ID;

            //Campagne mCampagne = new Campagne();
            //mCampagne.fnGet(Currentcampagne);

            X.GetCmp<ComboBox>("cmbFiltreCampagne").SetValue(Currentcampagne);
            X.GetCmp<ComboBox>("cmbFiltreMagasin").SetValue(mSiteParDefaut.MagasinID);
            X.GetCmp<ComboBox>("cmbFiltreExportateur").SetValue(mParam.Exportateur.ID);
            X.GetCmp<ComboBox>("cmbFiltreEmplacement").SetValue(mParam.Emplacement.ID);
            //X.GetCmp<ComboBox>("cmbFiltreMagasin").SetValue(MagasinID);
            //ViewBag.CampagneMinDate = mCampagne.DateDebut;

            // string StartDate = mCampagne.DateDebutAsString;
            //string EndDate = mCampagne.DateFinAsString;

            //X.GetCmp<DateField>("dtpFiltreStartDate").RawText = StartDate;
            //X.GetCmp<DateField>("txtFiltreDateFin").RawText = DateTime.Now.ToShortDateString();
            //X.GetCmp<FormPanel>("StatutStockItemCP").SetTitle("Magasin : " + magasin + ", Campagne : " + Currentcampagne);
            X.GetCmp<FormPanel>("StatutStockItemCP").SetTitle("Site : " + mSiteParDefaut.Nom + ", Campagne : " + Currentcampagne + " Au : " + DateTime.Now.ToShortDateString());

            #region Set Function's Access

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{f1c9b8de-e188-4c76-a9a5-f8c63a6aeffe}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{4ea4c1ee-3ebb-4a85-8acd-fe5abb6808db}")))
                X.GetCmp<MenuItem>("mnuPrintStockItemList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintStockItemList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{e7777151-4bc2-4986-970c-b2e5ee759f90}")))
                X.GetCmp<MenuItem>("mnuExportStockItem").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportStockItem").Disable();

            //bool HavAccessMagasinTV = false;
            //if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{edf058fe-9dbf-460b-9fb8-3123d84cd601}")))
            //    HavAccessMagasinTV = true;

            //bool HavAccessMagasinExport = false;
            //if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{D4D77E67-F96C-4104-ABF5-C633C2AFC141}")))
            //    HavAccessMagasinExport = true;

            //if (HavAccessMagasinTV == true)
            //{

            //    ViewData["magasinID"] = mParam.MagasinTV;
            //}            
            //else {
            //    ViewData["LoadMagasin"] = "";
            //    ViewData["magasinID"] = "0";
            //}
            #endregion
            return View();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("StatutStockItemCP");
            mform.ToggleCollapse();
            //mform.Collapsed = false;
            return this.Direct();
        }

        public ActionResult OnConsultDetail(string ItemSelected, string ItemCampagne, string ItemPeriodEnd)
        {
            MouvementStockAgence mclass = new MouvementStockAgence();                        
            mclass = JSON.Deserialize<MouvementStockAgence>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            mclass.mCampagne = new Tms.Classes.Shared.Campagne();

            mclass.mCampagne.Designation = Campagne;
            mclass.DateMouvement = (DateTime)EndDate;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDetailLot", Model = mclass };
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne, string ItemMagasin, string ItemExportateur, string ItemPeriodEnd, string ItemEmplacement, string ItemSite)
        {
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
            //int MagasinID = string.IsNullOrEmpty(ItemMagasin) ? -1 : (ItemMagasin);
            string MagasinID = string.IsNullOrEmpty(ItemMagasin) ? "[-1]" : (ItemMagasin);
            int ExportateurID = GetCriteriaValue(ItemExportateur);
            int EmplacementID = GetCriteriaValue(ItemEmplacement);
            int siteID = GetCriteriaValue(ItemSite);
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "";
            }

            //DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            //var mListe = (new MouvementStockAgence()).fnSelectStockItemSite(ItemCampagne, MagasinID, ExportateurID, null, EndDate, EmplacementID, siteID);
            var mListe = (new MouvementStockAgence()).fnSelect_SituationLot(ItemCampagne, MagasinID, ExportateurID, null, EndDate, EmplacementID, siteID);

            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemMagasin, string ItemExportateur, string ItemPeriodEnd, string ItemEmplacement, string ItemSite)
        {
            try
            {
                //DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeStatutStockItem");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCampagne",ItemCampagne),
                                    new Ext.Net.Parameter("ItemSite",ItemSite),
                                    new Ext.Net.Parameter("ItemMagasin",ItemMagasin),
                                    new Ext.Net.Parameter("ItemExportateur",ItemExportateur),
                                    new Ext.Net.Parameter("ItemEmplacement",ItemEmplacement),
                                    //new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd)
                                });
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stock Statut : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnChangeCampagne(string ItemCampagne)
        {
            Campagne mCampagne = new Campagne();
            mCampagne.fnGet(ItemCampagne);

            X.GetCmp<DateField>("dtpFiltreStartDate").RawText = mCampagne.DateDebutAsString;
            X.GetCmp<DateField>("dtpFiltreEndDate").RawText = mCampagne.DateFinAsString;

            return this.Direct();
        }

        public ActionResult OnPrintStockItems()
        {
            Parametres mParam = new Parametres(0);

            ViewData["Campagne"] = mParam.Campagne;
            ViewData["Exportateur"] = mParam.Exportateur.ID;

            //Campagne mCampagne = new Campagne();
            //mCampagne.fnGet(mParam.Campagne);
            //ViewData["CampagneMinDate"] = mCampagne.DateDebut;

            string UserName = (string)Session["userName"];
            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{f1c9b8de-e188-4c76-a9a5-f8c63a6aeffe}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            bool HavAccessMagasinTV = false;
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{edf058fe-9dbf-460b-9fb8-3123d84cd601}")))
                HavAccessMagasinTV = true;

            if (HavAccessMagasinTV == true)
            {
                ViewData["LoadMagasin"] = "LoadOnlyMagasinTV";
                ViewData["magasinID"] = mParam.MagasinTV;
            }
            else
            {
                ViewData["LoadMagasin"] = "";
                ViewData["magasinID"] = "0";
            }

            return new Ext.Net.MVC.PartialViewResult { ViewName = "SituationStockItem_Print", ViewData = ViewData };
        }

        public ActionResult PrintList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                Parametres mParam = new Parametres(0);
                Campagne mCampagne = new Campagne();
                mCampagne.fnGet(mParam.Campagne);
                XtraReport report = null;

                report = new rptSituationStockItem() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["CampagneID"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                report.Parameters["paramExportateur"].Value = int.Parse(X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Value);
                report.Parameters["paramExportateurNom"].Value = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text;

                report.Parameters["paramMagasin"].Value = int.Parse(X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Value);
                report.Parameters["paramMagasinText"].Value = X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Text;

                report.Parameters["paramPosition"].Value = int.Parse(X.GetCmp<ComboBox>("cmbEmplacement").SelectedItem.Value);
                report.Parameters["paramPositionText"].Value = X.GetCmp<ComboBox>("cmbEmplacement").SelectedItem.Text;

                report.Parameters["paramDateDebut"].Value = DateTime.Now;
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateFin").RawText);

                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/SituationStockItem/ViewList', this, 'Daily Stock Status',''),App.SituationStockItem_Print.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stock Statut : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
        }

        public ActionResult OnPrintDailyStockTV()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;

                report = new rptDailyStock() as XtraReport;
                report.DataSource = DevExpressReportDs.SetDataSource(report);
                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/SituationStockItem/ViewList', this, 'Daily Stock TV',''),App.SituationStockItem_Print.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stock Statut : Data Validation",
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

            ViewData["Report"] = report;

            return View("ViewReportResult");
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


    }
}