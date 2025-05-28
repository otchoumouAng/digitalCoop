using Ext.Net;
using Ext.Net.MVC;
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
    public class PeseeHistoryController : Controller
    {
        // GET: PeseeHistory
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{2703C226-3D3E-4E20-91FB-FB8FC21DF3EB}", UserName) == false)
                X.GetCmp<Button>("mnuPrintHistoryOfWeighing").Disable();
            else
                X.GetCmp<Button>("mnuPrintHistoryOfWeighing").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{F1C510AD-C97F-4C17-9FCD-2D51607AF98E}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportHistoryWeighing").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportHistoryWeighing").Enable();
            ViewBag.CriteriaTitle = String.Format("Weighing History : From {0} to {1} ", DateTime.Now.AddDays(-7).ToShortDateString(), DateTime.Now.ToShortDateString());

            return View();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemStartDate, string ItemEndDate)
        {            

            DateTime mStartDate = DateTime.Now.AddDays(-30);
            if (!string.IsNullOrEmpty(ItemStartDate) && DateTime.TryParse(ItemStartDate, out mStartDate))
                mStartDate = DateTime.Parse(ItemStartDate);

            DateTime mEndDate = DateTime.Now;
            if (!string.IsNullOrEmpty(ItemEndDate) && DateTime.TryParse(ItemEndDate, out mEndDate))
                mEndDate = DateTime.Parse(ItemEndDate);
            
            var mListe = (new PeseeHistory()).fnSelect(mStartDate, mEndDate);

            // Paging
            //int start = parameters.Start;

            //int limit = parameters.Limit;

            //if ((start + limit) > mListe.Count)
            //{
            //    limit = mListe.Count - start;
            //}

            //List<DataPersist> rangePlants = (start < 0 || limit < 0) ? mListe : mListe.GetRange(start, limit);

            //return this.Store(new Paging<DataPersist>(rangePlants, mListe.Count));

            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemStartDate, string ItemEndDate)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemStartDate, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemEndDate, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListe");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStartDate"              ,ItemStartDate),
                                new Ext.Net.Parameter("ItemEndDate"     ,ItemEndDate)
                            });



                // collapse criterias areas
                X.GetCmp<FormPanel>("CriteriaPanel").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing History : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });                
            }
            

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            X.GetCmp<FormPanel>("CriteriaPanel").ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnDisplayHistoryOfWeighing()
        {

            ViewData["Titre"] = "Print History of Weighings";
            ViewData["actionToDo"] = "OnPrintHistoryOfWeighings";
            ViewData["ControllerName"] = "PeseeHistory";
            ViewData["SiteParDefaut"] = 1;
            ViewData["UrlSite"] = "LoadSiteByAccess";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmPeriodForReport", ViewData = ViewData };

        }

        public ActionResult OnPrintHistoryOfWeighings(string startDate, string endDate)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                //Session["paramCropYear"] = cropYear;                

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/PeseeHistory/ViewReportResult', this, 'History Of Weigihngs',''),App.frmPeriodForReport.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "History Of Weighings : Data Validation",
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

            rptWeighingHistoryList report = new rptWeighingHistoryList();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];

            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

    }
}