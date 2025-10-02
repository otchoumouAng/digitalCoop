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
using DevExpress.XtraReports.UI;

namespace Tms2017.MVC.Controllers
{
    public class MiseEnCompteController : BaseController
    {
       
        Guid mID;

        // GET: MiseEnComptePlanifiee
        public ActionResult Index()
        {
            //var mParam = (new Parametres()).fnSelect();

            Parametres mclass = new Parametres(0);           
            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            string UserName = (string)Session["userName"];
            Tms.Classes.Shared.Site mSiteParDefaut = new Tms.Classes.Shared.Site();

            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            X.GetCmp<DateField>("dtpStartDate").RawText = StartDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Fournisseur = "{Tous}";

            string Status = "{Unpaid}";

            X.GetCmp<FormPanel>("CriteriaPanelMEC").SetTitle("Fournisseur : " + Fournisseur + " | Du : " + StartDate + " Au : " + EndDate + " | Statut : " + Status);

            #region Set Function's Access

            //string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{EF2D0616-DCD9-4D96-9FCE-E3DB136C2C32}", UserName);
            if (HasAccess.fnGetUserAccessStatus("{516B9F43-961E-4E4A-9C06-B96B91F62364}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportSaving").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportSaving").Enable();

            if (HasAccess.fnGetUserAccessStatus("{E10FF729-E9BB-4C79-8425-803F1FABE410}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintSavingList").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintSavingList").Enable();

            //X.GetCmp<Hidden>("FohiddenPermPrintFunctionList").SetValue(HasAccess.fnGetUserAccessStatus("{989FA360-ADD9-412D-B1B6-2FF988B403A9}", UserName));
            //X.GetCmp<Hidden>("FohiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{516B9F43-961E-4E4A-9C06-B96B91F62364}", UserName));
            //X.GetCmp<Hidden>("FohiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{EF2D0616-DCD9-4D96-9FCE-E3DB136C2C32}", UserName));

            #endregion


            return View();
        }         

        public ActionResult LoadListOfMec(StoreRequestParameters parameters, string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite)
        {

            int FournisseurID = GetCriteriaValue(ItemFournisseur);
            int siteID = GetCriteriaValue(ItemSite);
            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string Status = ItemStatus;

            var mListe = (new MiseEnCompte()).fnSelect(FournisseurID, StartDate, EndDate, Status,siteID);
            //var paging = GridStorePaging.SetRangePlants(parameters, mListe);
            //return this.Store(paging);
            return this.Store(mListe);

        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelMEC");
            mform.ToggleCollapse();
            //mform.Collapsed = false;
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeMec");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus),
                                    new Ext.Net.Parameter("ItemSite"        ,ItemSite)
                                });

                string title = X.GetCmp<FormPanel>("CriteriaPanelMEC").Title;
                title += "Site : " + X.GetCmp<ComboBox>("_cmbSite").SelectedItem.Text;
                title += ", Fournisseur : " + X.GetCmp<ComboBox>("cmbFournisseur").SelectedItem.Text;

                title += ", From " + X.GetCmp<DateField>("dtpStartDate").RawText.ToString() + "to " + X.GetCmp<DateField>("dtpEndDate").RawText.ToString();

                X.GetCmp<FormPanel>("CriteriaPanelMEC").Title = title;

                // collapse criterias areas
                X.GetCmp<FormPanel>("CriteriaPanelMEC").Collapse(Direction.Top, false);

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Savings : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            

            return this.Direct();
        }
        private string GetFormValue(string id_Component)
        {
            return Request.Form[id_Component];
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
            X.GetCmp<RowSelectionModel>("rowSelectionListeMEC").DeselectAll();
        }

        public ActionResult OnDisplaySavingList()
        {
            string UserName = (string)Session["userName"];

            ViewData["Titre"] = "Print List Of Savings";
            ViewData["actionToDo"] = "OnPrintSavingList";
            ViewData["ControllerName"] = "MiseEnCompte";

            Site mSiteParDefaut = new Site();
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;
            Parametres mParam = new Parametres(0);
            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;
            if (mParam.Site == mSiteParDefaut.ID) ViewData["UrlSite"] = "LoadSiteAll";
            else ViewData["UrlSite"] = "LoadSiteByAccess";

            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmCriteriaForSaving", ViewData = ViewData };
        }

        public ActionResult OnPrintSavingList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetStartDate").RawText, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetEndDate").RawText, "d", CultureInfo.CurrentUICulture);
                
                //Session["paramFournisseur"] = fournisseur;
                //Session["paramFournisseurText"] = fournisseurText;
                //Session["paramStatut"] = statut;
                //Session["paramStatutText"] = statutText;

                //Session["paramStartDate"] = dateDebut;
                //Session["paramEndDate"] = dateFin;

                XtraReport report = null;
                report = new rptSavingList() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);

                report.Parameters["paramFournisseur"].Value = int.Parse(X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Value);
                report.Parameters["paramFournisseurText"].Value = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text;

                report.Parameters["paramStatut"].Value = int.Parse(X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Value);
                report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Text;

                report.Parameters["paramSite"].Value = int.Parse(X.GetCmp<ComboBox>("cmbRepSite").SelectedItem.Value);
                report.Parameters["paramSiteText"].Value = X.GetCmp<ComboBox>("cmbRepSite").SelectedItem.Text;

                report.Parameters["paramStartDate"].Value = dateDebut;
                report.Parameters["paramEndDate"].Value = dateFin;

                Session["report"] = report;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/MiseEnCompte/ViewReportListResult', this, 'List Of Savings',''),App.frmCriteriaForSaving.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Savings : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
        }

        public ActionResult ViewReportListResult()
        {
            XtraReport report = null;
            report = Session["report"] as XtraReport;                 
            ViewData["Report"] = report;
            return View("ViewReportResult");
        }

    }
}
