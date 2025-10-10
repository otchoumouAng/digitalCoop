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

namespace Tms2017.MVC.Controllers
{
    public class RemboursementDirectAgenceController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";

        Guid mID;

        // GET: RemboursementDirectAgence
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

            X.GetCmp<FormPanel>("CriteriaPanelRD").SetTitle("Site : " + mSiteParDefaut.Nom + ", Campagne : " + mParam.Campagne + ", Fournisseur : " + Fournisseur + ", Type : " + Type + ", Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access
            
            Fonction HasAccess = new Fonction();
            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{cfca7269-4931-4a8c-b40f-5895a2724bd7}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{e4ef1fa8-8e5b-4d6c-9b41-d5775df2fd87}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{c8c54a29-94f0-4351-86cd-524914a9fb73}")))
                X.GetCmp<MenuItem>("mnuExportReimbList").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportReimbList").Disable();

            X.GetCmp<Hidden>("RdhiddenPermCreer").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{e4ef1fa8-8e5b-4d6c-9b41-d5775df2fd87}")));
            X.GetCmp<Hidden>("RdhiddenPermModifier").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{5a0a58f8-9dc7-4012-8d60-43b45926ef72}")));
            X.GetCmp<Hidden>("RdhiddenPermApprove").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{e35e7fcb-68f5-42ac-805e-2fea43cfa66f}")));
            X.GetCmp<Hidden>("RdhiddenPermDesactiver").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{c1c5588e-1f10-40f4-a814-e46d15f55da3}")));
            X.GetCmp<Hidden>("RdhiddenPermDesactiverSup").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{5040f68d-8090-40f9-823f-7788100a2689}")));
            //X.GetCmp<Hidden>("RdhiddenPermPrintReimbList").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{3c2dc8d7-ea7d-42fa-87c4-549543d9feaf}")));
            X.GetCmp<Hidden>("RdhiddenPermExporterExcel").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{c8c54a29-94f0-4351-86cd-524914a9fb73}")));
            X.GetCmp<Hidden>("RdhiddenPermOverview").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{d91e9560-08dd-4019-9bab-3fdd907cc682}")));            
            
            #endregion

            return View();
        }        


        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelRD");

            mform.ToggleCollapse();
            //mform.Collapsed = false;
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite)
        {
            Store mstore = X.GetCmp<Store>("storeListeRemboursementDirect");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemCampagne"      ,ItemCampagne),
                                    new Ext.Net.Parameter("ItemType"          ,ItemType),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus),
                                    new Ext.Net.Parameter("ItemSite"        ,ItemSite)
                                });

            string title = X.GetCmp<FormPanel>("CriteriaPanelRD").Title;
            title += "Site : " + X.GetCmp<ComboBox>("cmbSite").SelectedItem.Text;
            title += ", Campagne : " + X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

            title += ", Fournisseur : " + X.GetCmp<ComboBox>("cmbFournisseur").SelectedItem.Text;

            title += ", Type : " + X.GetCmp<ComboBox>("cmbType").SelectedItem.Text;

            title += ", From " + X.GetCmp<DateField>("dtpStartDate").RawText.ToString() + "to " + X.GetCmp<DateField>("dtpEndDate").RawText.ToString();

            X.GetCmp<FormPanel>("CriteriaPanelRD").Title = title;

            // collapse criterias areas
            X.GetCmp<FormPanel>("CriteriaPanelRD").Collapse(Direction.Top, false);

            return this.Direct();
        }

        public ActionResult OnDisplayDirectRepaymentList(int ItemSite = -1)
        {
            ViewData["Titre"] = "Print Liste des Remboursements directes";
            ViewData["actionToDo"] = "OnPrintDirectRepaymentList";
            ViewData["ControllerName"] = "RemboursementDirect";
            ViewData["SiteParDefaut"] = ItemSite;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "CriteriaForDirectRepayment", ViewData = ViewData };
        }

        public ActionResult OnPrintDirectRepaymentList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetStartDate").RawText, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetEndDate").RawText, "d", CultureInfo.CurrentUICulture);

                //Session["paramCropYear"] = cropYear;   
                Session["paramSite"] = int.Parse(GetFormValue("cmbDetSite"));
                Session["paramSiteText"] = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text;
                Session["paramCampagne"] = X.GetCmp<ComboBox>("cmbDetCropYear").SelectedItem.Text;
                Session["paramFournisseur"] = int.Parse(GetFormValue("cmbDetFournisseur"));
                Session["paramFournisseurText"] = X.GetCmp<ComboBox>("cmbDetFournisseur").SelectedItem.Text;
                Session["paramRemboursement"] = int.Parse(GetFormValue("cmbDetRemboursementType"));
                Session["paramRemboursementText"] = X.GetCmp<ComboBox>("cmbDetRemboursementType").SelectedItem.Text;
                Session["paramStatut"] = GetFormValue("cmbDetStatus");
                Session["paramStatutText"] = X.GetCmp<ComboBox>("cmbDetStatus").SelectedItem.Text;

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/RemboursementDirect/ViewReportListResult', this, 'Direct Repayments',''),App.frmCriteriaForDirectRepayment.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Direct Repayment : Data Validation",
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

            rptDirectRepaymentList report = new rptDirectRepaymentList();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramCampagneText"].Value = Session["paramCampagne"];
            if (!string.IsNullOrEmpty(Session["paramCampagne"].ToString()) && Session["paramCampagne"].ToString() == "{Tous}")
                report.Parameters["paramCampagne"].Value = string.Empty;
            else
                report.Parameters["paramCampagne"].Value = Session["paramCampagne"];

            report.Parameters["paramSite"].Value = Session["paramSite"];
            report.Parameters["paramSiteText"].Value = Session["paramSiteText"];

            report.Parameters["paramFournisseur"].Value = Session["paramFournisseur"];
            report.Parameters["paramFournisseurText"].Value = Session["paramFournisseurText"];

            report.Parameters["paramRemboursementType"].Value = Session["paramRemboursement"];
            report.Parameters["paramRemboursementTypeText"].Value = Session["paramRemboursementText"];

            report.Parameters["paramStatut"].Value = Session["paramStatut"];
            report.Parameters["paramStatutText"].Value = Session["paramStatutText"];

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];
            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

        private string GetFormValue(string id_Component)
        {
            return Request.Form[id_Component];
        }
    }
}
