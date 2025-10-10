using CrystalDecisions.CrystalReports.Engine;
using DevExpress.XtraReports.UI;
using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Business;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Components.Data;
using Tms.Components.Reports;
using Tms.Components.Settings;
using Tms2017.MVC.Models;
using Tms2017.MVC.Reports;
using Tms2017.Reports;

namespace Tms2017.MVC.Controllers
{
    public class LivraisonSuiviController : BaseController
    {

        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";

        //public LivraisonSuiviController()
        //{
        //    //string cultureName = "fr";
        //    //Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
        //    //Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
        //}

        // GET: Fournisseur
        public ActionResult Index()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            
            if (HasAccess.fnGetUserAccessStatus("{4991D43E-1DA9-4D9B-AD91-0F70FBB383B4}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintAcceptedDeliveriesReport").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintAcceptedDeliveriesReport").Enable();

            if (HasAccess.fnGetUserAccessStatus("{20B3F275-6E96-48B6-8259-79A38CF78F9B}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintRejectedDeliveriesReport").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintRejectedDeliveriesReport").Enable();

            if (HasAccess.fnGetUserAccessStatus("{95D47DD6-59A2-4F5F-91DE-D6A623804D73}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintQualityAverage").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintQualityAverage").Enable();
            //if (HasAccess.fnGetUserAccessStatus("{C043F313-13A0-4E6A-85B9-7BAF69F5681B}", UserName) == false)
            //    X.GetCmp<MenuItem>("btnViewPendingDeliveries").Disable();
            //else
            //    X.GetCmp<MenuItem>("btnViewPendingDeliveries").Enable();
            Site mSiteParDefaut = new Site();

            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;
            Parametres mParam = new Parametres(0);

            X.GetCmp<ComboBox>("cmbCampagne").SetValue(mParam.Campagne);
            X.GetCmp<ComboBox>("cmbExportateur").SetValue(mParam.Exportateur.ID);
            ViewBag.HiddenDefExportateur = mParam.Exportateur.ID;

            X.GetCmp<FormPanel>("CriteriaPanel").SetTitle("Moniteur de Livraison - Overview | " + mSiteParDefaut.Nom + ", Campagne : " + mParam.Campagne );

            return View();
        }

        public ActionResult LoadDeliveriesDetailByNumbers(string ItemNumbers)
        {
            string[] liste = ItemNumbers.Split(',');            
            List<DataPersist> myList = new List<DataPersist>();
            Livraison mClass = new Livraison();
            for (int i = 0; i < liste.Count(); i++)
            {
                mClass.fnSelectByNumber(liste[i]);
                myList.Add(mClass);
            }            
            
            if (myList.Count > 0)
                mClass = myList[0] as Livraison;
            else myList = new List<DataPersist>();
            return this.Store(myList);                    

        }

        public ActionResult LoadDeliveryBySupplierAll(string ItemFournisseur, string ItemMode, string ItemExecMode, string ItemSpotPrice)
        {
            List<DataPersist> myList = new List<DataPersist>();
            if (!string.IsNullOrEmpty(ItemFournisseur) && !string.IsNullOrEmpty(ItemMode) && ItemMode == "2")
            {

                if (ItemExecMode == "Update")
                {
                    myList = new Livraison().fnSelectBySpotPrice(Guid.Parse(ItemSpotPrice), int.Parse(ItemFournisseur));
                }                
                
                Livraison mClass = new Livraison();
                if (myList.Count > 0)
                    mClass = myList[0] as Livraison;
                else myList = new List<DataPersist>();
                
            }
            else
            {
                Store mstore = X.GetCmp<Store>("storeDelaitDeliveriesListe");
                mstore.RemoveAll();
                myList = null;
            }

            
                //GridPanel gridpanel = X.GetCmp<GridPanel>("grpDetailDeliveries");
                //RowSelectionModel smTD = gridpanel.GetSelectionModel() as RowSelectionModel;
                //smTD.SelectedRows.Add(new SelectedRow(clsted.ID.ToString()));
                //smTD.UpdateSelection();
            
            return this.Store(myList);
        }

        public ActionResult GetDeliveryBySupplierAll(string ItemFournisseur,string ItemMode)
        {

            List<DataPersist> myList = new List<DataPersist>();
            if (!string.IsNullOrEmpty(ItemFournisseur) || !string.IsNullOrEmpty(ItemMode))
            {
                if (ItemMode == "2")
                {
                    myList = new Livraison().fnSelect("{all}", -1, ItemFournisseur, DateTime.Now, DateTime.Now,-1,-1);

                    //List<DataPersist> myList = new Livraison().fnSelect("{all}", -1, ItemFournisseur, DateTime.Now);
                    Livraison mClass = new Livraison();
                    if (myList.Count > 0)
                        mClass = myList[0] as Livraison;
                    else myList = new List<DataPersist>();
                    
                }
                return this.Store(myList);
            }
            else
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "Prix Negocié : PRICE",
                    Message = "Mode Application and Supplier must be selected",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Store(null);
            }

        }

        public ActionResult LoadFournisseurAll()
        {
            List<DataPersist> myList = new Fournisseur().fnSelect();
            Fournisseur mClass = new Fournisseur();

            mClass.ID = -1;
            mClass.Nom = "{Tous}";
            myList.Insert(0, mClass);
            mClass = myList[0] as Fournisseur;

            return this.Store(myList);
        }

        public ActionResult LoadFournisseur()
        {
            List<DataPersist> myList = new Fournisseur().fnSelect();
            Fournisseur mClass = new Fournisseur();
            
            mClass = myList[0] as Fournisseur;

            return this.Store(myList);
        }

        public ActionResult OnSelectDelivery()
        {
            LivraisonViewModel mclass = new LivraisonViewModel();

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Livraisons_Disponibles_Finalisation", Model = mclass };
        }

        public ActionResult OnConsult(string ItemSelected)
        {
            SuiviLivraison mClass = new SuiviLivraison();

            mClass = JSON.Deserialize<SuiviLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            return new Ext.Net.MVC.PartialViewResult {ViewName = "frmSuiviLivraison_Detail", Model = mClass };
        }


        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne, string ItemLivraisonType, string ItemFournisseur, string ItemExportateur, string ItemAtGate, string ItemInProcess, string ItemStartDate, string ItemEndDate, string ItemStatus, string ItemSite, bool IsQueryOnLoad = true)
        {
            Parametres para = new Parametres();
            //string defaultCrop = para.Campagne;
            DateTime? startdate = string.IsNullOrEmpty(ItemStartDate) ? (DateTime?)null : DateTime.Parse(ItemStartDate.ToString());
            DateTime? enddate = string.IsNullOrEmpty(ItemEndDate) ? (DateTime?)null : DateTime.Parse(ItemEndDate.ToString());

            string Crop = "{Tous}";
            if (!string.IsNullOrEmpty(ItemCampagne))
                Crop = ItemCampagne;

            int LivraisonTypeID = GetCritriaValue(ItemLivraisonType);

            string SupplierID = "-1";
            if (!string.IsNullOrEmpty(ItemFournisseur))
                SupplierID = ItemFournisseur;

            int ExportateurID = -1;
            if (!string.IsNullOrEmpty(ItemExportateur))
                ExportateurID = int.Parse(ItemExportateur);


            int SiteId = -1;
            if (!string.IsNullOrEmpty(ItemSite))
                SiteId = int.Parse(ItemSite);

            int AtGate = 1;
            int InProcess = 1;
            int OnlyByStatus = 0;            
            string status = "-1";

            if (!IsQueryOnLoad)
            {

                if (ItemStatus != "-1")
                {
                    OnlyByStatus = 1;
                    status = ItemStatus;
                    AtGate = 0;
                    InProcess = 0;
                }

            }


            //else if (ItemStatus == "-1" && AtGate = 0 && InProcess)

            //DateTimeFormatInfo ukDtfi = new CultureInfo("fr-FR", false).DateTimeFormat;

            //DateTime maxDate = DateTime.Parse("31/12/9999", ukDtfi);
            //DateTime minDate = DateTime.Parse("1/1/1753", ukDtfi);

            //DateTime StartDate = DateTime.Now.AddDays(-1);
            //DateTime temp;
            //bool hasConvert = DateTime.TryParse(ItemStartDate, out temp);
            //if (hasConvert && DateTime.Compare(temp, minDate) > 0 && DateTime.Compare(temp, maxDate) < 0)
            //    StartDate = DateTime.Parse(ItemStartDate);

            //DateTime EndDate = DateTime.Now;
            //hasConvert = DateTime.TryParse(ItemEndDate, out temp);
            //if (hasConvert && DateTime.Compare(temp, minDate) > 0 && DateTime.Compare(temp, maxDate) < 0)
            //    EndDate = DateTime.Parse(ItemEndDate);

            var mListe = (new SuiviLivraison()).fnSelect(Crop, LivraisonTypeID, SupplierID, ExportateurID,AtGate,InProcess, OnlyByStatus, status, startdate, enddate, SiteId);

            // Filtering
            mListe = Filtering(parameters, mListe);

            // Paging
            //int start = parameters.Start;

            //int limit = parameters.Limit;

            //if ((start + limit) > mListe.Count)
            //{
            //    limit = mListe.Count - start;
            //}

            //List<SuiviLivraison> rangeListe = (start < 0 || limit < 0) ? mListe : mListe.GetRange(start, limit);

            //return this.Store(new Paging<SuiviLivraison>(rangeListe, mListe.Count));
            return this.Store(mListe);
        }

        public List<SuiviLivraison> Filtering(StoreRequestParameters parameters , List<SuiviLivraison> data )
        {
            FilterConditions fc = parameters.GridFilters;

            //-- start filtering ------------------------------------------------------------
            if (fc != null)
            {
                foreach (FilterCondition condition in fc.Conditions)
                {
                    Comparison comparison = condition.Comparison;
                    string field = condition.Field;
                    FilterType type = condition.Type;

                    object value;
                    switch (condition.Type)
                    {
                        case FilterType.Boolean:
                            value = condition.Value<bool>();
                            break;
                        case FilterType.Date:
                            value = condition.Value<DateTime>();
                            break;
                        case FilterType.List:
                            value = condition.List;
                            break;
                        case FilterType.Number:
                            if (data.Count > 0 && data[0].GetType().GetProperty(field).PropertyType == typeof(int))
                            {
                                value = condition.Value<int>();
                            }
                            else
                            {
                                value = condition.Value<double>();
                            }

                            break;
                        case FilterType.String:
                            value = condition.Value<string>();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    data.RemoveAll(
                        item =>
                        {
                            object oValue = item.GetType().GetProperty(field).GetValue(item, null);
                            IComparable cItem = oValue as IComparable;

                            switch (comparison)
                            {
                                case Comparison.Eq:
                                case Comparison.Like:
                                case Comparison.In:

                                    switch (type)
                                    {
                                        case FilterType.List:
                                            return !(value as List<string>).Contains(oValue.ToString());
                                        case FilterType.String:
                                            return !oValue.ToString().StartsWith(value.ToString());
                                        default:
                                            return !cItem.Equals(value);
                                    }

                                case Comparison.Gt:
                                    return cItem.CompareTo(value) < 1;
                                case Comparison.Lt:
                                    return cItem.CompareTo(value) > -1;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    );
                }
            }
            //-- end filtering ------------------------------------------------------------
            return data;
        }

        public List<DataPersist> Sorting(StoreRequestParameters parameters, List<DataPersist> data)
        {
            //-- start sorting ------------------------------------------------------------
            //if (parameters.Sort.Length > 0)
            //{
            //    DataSorter sorter = parameters.Sort[0];
            //    data.Sort(delegate (object x, object y)
            //    //{
            //    //    object a;
            //    //    object b;

            //    //    int direction = sorter.Direction == Ext.Net.SortDirection.DESC ? -1 : 1;

            //    //    a = x.GetType().GetProperty(sorter.Property).GetValue(x, null);
            //    //    b = y.GetType().GetProperty(sorter.Property).GetValue(y, null);
            //    //    return CaseInsensitiveComparer.Default.Compare(a, b) * direction;
            //    //});
            //}
            //-- end sorting ------------------------------------------------------------
            return data;
        }

        public ActionResult OnDisplayDeliveriesReport(string status = "")
        {
            ViewData["Titre"] = "Etat Livraison";
            ViewData["actionToDo"] = "OnPrintDeliveriesReport";
            ViewData["status"] = status;
            Fournisseur mClass = new Fournisseur();
            mClass.ID = -1;
            mClass.Agence = new Site();            

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();

            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            mClass.Agence.ID = mSiteParDefaut.ID;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmDeliveryMonitoring_Criteria", ViewData = ViewData, Model= mClass };
        }        

        //public ActionResult OnPrintAcceptedDeliveries()
        //{
        //    ViewData["Titre"] = "Print accepted deliveries";
        //    ViewData["actionToDo"] = "OnPrintAcceptedDeliveries";
        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "frmDeliveryMonitoring_Criteria", ViewData = ViewData };
        //}

        //public ActionResult OnPrintRejectedDeliveries()
        //{
        //    ViewData["Titre"] = "Print rejected deliveries";
        //    ViewData["actionToDo"] = "OnPrintRejectedDeliveries";
        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "frmDeliveryMonitoring_Criteria", ViewData = ViewData };
        //}

        public ActionResult OnPrintDeliveriesReport()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {                               
                DateTime dateDebut = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetStartDate").RawText, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetEndDate").RawText, "d", CultureInfo.CurrentUICulture);


                Session["paramSite"] = int.Parse(GetFormValue("cmbDetSite"));
                Session["paramSiteText"] = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text;

                Session["paramCropYear"] = GetFormValue("cmbDetCrop"); ;
                Session["paramDeliveryType"] = int.Parse(GetFormValue("cmbDetLivraisonType"));
                Session["paramDeliveryTypeText"] = X.GetCmp<ComboBox>("cmbDetLivraisonType").SelectedItem.Text;
                Session["paramStatus"] = GetFormValue("DeliveryStatus");

                Session["paramSupplier"] = int.Parse(GetFormValue("cmbDetSupplier"));
                Session["paramSupplierText"] = X.GetCmp<ComboBox>("cmbDetSupplier").SelectedItem.Text;

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/LivraisonSuivi/ViewDeliveriesReport', this, 'Deliveries report',''),App.frmDeliveryMonitoring.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Livraison : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
                                           
            
      }
      
        public ActionResult ViewDeliveriesReport()
        {
            XtraReport report = null;

            // TODO :  sur le base du type de rapport : detaillé ou cumulé 
            // initialiser l'objet report avec l'object idoine

            report = new rptDeliveriesReport() as XtraReport;

            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramSite"].Value = Session["paramSite"];
            report.Parameters["paramSiteText"].Value = Session["paramSiteText"];

            report.Parameters["paramCampagne"].Value           = Session["paramCropYear"];

            //report.Parameters["siteID"].Value               = Session["paramSite"];

            report.Parameters["paramTypeLivraison"].Value       = Session["paramDeliveryType"];

            report.Parameters["paramFournisseur"].Value        = Session["paramSupplier"];

            report.Parameters["paramTypeLivraisonText"].Value     = Session["paramDeliveryTypeText"];
            
            report.Parameters["paramFournisseurText"].Value         = Session["paramSupplierText"];

            report.Parameters["paramDateDebut"].Value            = Session["paramStartDate"];

            report.Parameters["paramDateFin"].Value              = Session["paramEndDate"];
            report.Parameters["paramStatus"].Value = Session["paramStatus"];



            ViewData["Report"] = report;

            return View();
        }
        
        //public ActionResult OnPrintAcceptedDeliveries(string cropYear, string site, string deliveryType, string supplier, string deliveryStatus, string reportType, string startDate, string endDate)
        //{
        //    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

        //    Session["paramCropYear"] = cropYear;
        //    Session["paramSite"] = Int32.Parse(site);
        //    Session["paramDeliveryType"] = Int32.Parse(deliveryType);

        //    Session["paramSupplier"] = Int32.Parse(deliveryType); ;
        //    Session["paramDeliveryStatus"] = Int32.Parse(deliveryStatus);
        //    Session["paramReportType"] = Int32.Parse(reportType);

        //    Session["paramStartDate"] = DateTime.Parse(startDate);
        //    Session["paramEndDate"] = DateTime.Parse(endDate);

        //    return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'reportDeliveryMonitoring{0}', '{1}/LivraisonSuivi/ViewAcceptedDeliveries', this, 'Farmer account status')", 10, BaseUrl));
        //}

        //public ActionResult ViewAcceptedDeliveries()
        //{
        //    XtraReport report = null;

        //    // TODO :  sur le base du type de rapport : detaillé ou cumulé 
        //    // initialiser l'objet report avec l'object idoine

        //    report = new rptSupplierAccountStatus() as XtraReport;

        //    report.DataSource = DevExpressReportDs.SetDataSource(report);

        //    report.Parameters["cropYearID"].Value = Session["paramCropYear"];

        //    report.Parameters["siteID"].Value = Session["paramSite"];

        //    report.Parameters["deliveryType"].Value = Session["paramDeliveryType"];

        //    report.Parameters["fournisseurID"].Value = Session["paramFarmerID"];

        //    report.Parameters["deliveryStatusID"].Value = Session["paramDeliveryStatus"];

        //    report.Parameters["reportTypeID"].Value = Session["paramReportType"];

        //    report.Parameters["startDate"].Value = Session["paramStartDate"];

        //    report.Parameters["endDate"].Value = Session["paramEndDate"];

        //    ViewData["Report"] = report;

        //    return View();
        //}

        //public ActionResult PrintRejectedDeliveries(string cropYear, string site, string deliveryType, string supplier, string deliveryStatus, string reportType, string startDate, string endDate)
        //{
        //    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

        //    Session["paramCropYear"] = cropYear;
        //    Session["paramSite"] = Int32.Parse(site);
        //    Session["paramDeliveryType"] = Int32.Parse(deliveryType);

        //    Session["paramSupplier"] = Int32.Parse(deliveryType); ;
        //    Session["paramDeliveryStatus"] = Int32.Parse(deliveryStatus);
        //    Session["paramReportType"] = Int32.Parse(reportType);

        //    Session["paramStartDate"] = DateTime.Parse(startDate);
        //    Session["paramEndDate"] = DateTime.Parse(endDate);

        //    return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'reportDeliveryMonitoring{0}', '{1}/LivraisonSuivi/ViewRejectedDeliveries', this, 'Farmer account status')", 10, BaseUrl));
        //}

        //public ActionResult ViewRejectedDeliveries()
        //{
        //    XtraReport report = null;

        //    // TODO :  sur le base du type de rapport : detaillé ou cumulé 
        //    // initialiser l'objet report avec l'object idoine


        //    report = new rptSupplierAccountStatus() as XtraReport;

        //    report.DataSource = DevExpressReportDs.SetDataSource(report);

        //    report.Parameters["cropYearID"].Value = Session["paramCropYear"];

        //    report.Parameters["siteID"].Value = Session["paramSite"];

        //    report.Parameters["deliveryType"].Value = Session["paramDeliveryType"];

        //    report.Parameters["fournisseurID"].Value = Session["paramFarmerID"];

        //    report.Parameters["deliveryStatusID"].Value = Session["paramDeliveryStatus"];

        //    report.Parameters["reportTypeID"].Value = Session["paramReportType"];

        //    report.Parameters["startDate"].Value = Session["paramStartDate"];

        //    report.Parameters["endDate"].Value = Session["paramEndDate"];

        //    ViewData["Report"] = report;

        //    return View();
        //}
        
        public ActionResult OnRefresh(string ItemCampagne, string ItemLivraisonType, string ItemFournisseur, string ItemStartDate, string ItemEndDate, string ItemExportateur, string ItemAtGate, string ItemInProcess, string ItemStatus, string ItemSite, String ItemOption)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemStartDate, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemEndDate, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListe");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemCampagne", ItemCampagne),
                                new Ext.Net.Parameter("ItemLivraisonType", ItemLivraisonType),
                                new Ext.Net.Parameter("ItemFournisseur", ItemFournisseur),
                                new Ext.Net.Parameter("ItemStartDate", ItemStartDate),
                                new Ext.Net.Parameter("ItemEndDate", ItemEndDate),
                                new Ext.Net.Parameter("ItemExportateur", ItemExportateur),
                                new Ext.Net.Parameter("ItemAtGate", ItemAtGate),
                                new Ext.Net.Parameter("ItemInProcess", ItemInProcess),
                                new Ext.Net.Parameter("ItemStatus", ItemStatus),
                                new Ext.Net.Parameter("ItemOption", ItemOption),
                                new Ext.Net.Parameter("ItemSite", ItemSite),
                                new Ext.Net.Parameter("IsQueryOnLoad", false)
                            });


                // collapse criterias areas
                X.GetCmp<FormPanel>("CriteriaPanel").Collapse(Direction.Top, false);
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
            X.GetCmp<FormPanel>("CriteriaPanel").ToggleCollapse();
            return this.Direct();
        }

        public ActionResult SetFilter(string Item)
        {
            Column column = X.GetCmp<Column>("ColumnStatutLivraison");
            column.Call("filter.setValue", "item");

            return this.Direct();
        }

        #region "Methods"

        private string GetFormValue(string id_Component)
        {
            string data = Request.Form[id_Component];
            return string.IsNullOrEmpty(data) ?string.Empty: data;
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

        private Livraison MapFormToObject(Livraison mClass)
        {
            int iConverted;
            bool result;

            mClass.Site = new Tms.Classes.Shared.Site();
            mClass.Site.ID = int.Parse(GetFormValue("cmbDetSite"));
            mClass.Site.Nom = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text.ToString();
            
            mClass.Provenance = new Tms.Classes.Shared.Provenance();
            mClass.Provenance.ID = int.Parse(GetFormValue("cmbDetOrigin"));
            mClass.Provenance.Nom = X.GetCmp<ComboBox>("cmbDetOrigin").SelectedItem.Text.ToString();

            mClass.Destination = new Tms.Classes.Shared.Destination();
            mClass.Destination.ID = int.Parse(GetFormValue("cmbDetDestination"));
            mClass.Destination.Nom = X.GetCmp<ComboBox>("cmbDetDestination").SelectedItem.Text.ToString();

            mClass.Exportateur = new Tms.Classes.Shared.Exportateur();
            mClass.Exportateur.ID = int.Parse(GetFormValue("cmbDetExporter"));
            mClass.Exportateur.Nom = X.GetCmp<ComboBox>("cmbDetExporter").SelectedItem.Text.ToString();

            mClass.Campagne = new Tms.Classes.Shared.Campagne();           
            mClass.Campagne.Designation = X.GetCmp<ComboBox>("cmbDetCrop").SelectedItem.Text.ToString();
            
            mClass.SacType = new Tms.Classes.Shared.SacType();
            mClass.SacType.ID = int.Parse(GetFormValue("cmbDetPackaging"));
            mClass.SacType.Designation = X.GetCmp<ComboBox>("cmbDetPackaging").SelectedItem.Text.ToString();

            mClass.Recolte = new Tms.Classes.Shared.Recolte();
            mClass.Recolte.ID = int.Parse(GetFormValue("cmbDetCropQuality"));
            mClass.Recolte.Designation = X.GetCmp<ComboBox>("cmbDetCropQuality").SelectedItem.Text.ToString();

            mClass.SacsDeclares = int.Parse(GetFormValue("txtNbrOfBags"));
            
            mClass.DateLivraison = DateTime.Parse(GetFormValue("dtfDeliveryDate")+" "+ GetFormValue("tmfDelivery"));

            //mClass.PoidsDeclare = decimal.Parse("1500,58");
            mClass.PoidsDeclare = Convert.ToDecimal(GetFormValue("txtEstimatedTonnage"));

            mClass.LivraisonType = new Tms.Classes.Shared.LivraisonType();
            mClass.LivraisonType.ID = int.Parse(GetFormValue("cmbDetDeliveryType"));
            mClass.LivraisonType.Designation = X.GetCmp<ComboBox>("cmbDetDeliveryType").SelectedItem.Text.ToString();
            
            mClass.Transitaire = null;            
            result  = int.TryParse(GetFormValue("cmbDetTransitaire"),out iConverted);
            if (result)
            {
                mClass.Transitaire = new Tms.Classes.Shared.Transitaire();
                mClass.Transitaire.ID = iConverted;
                mClass.Transitaire.Nom = X.GetCmp<ComboBox>("cmbDetTransitaire").SelectedItem.Text.ToString(); 
            }
            
            mClass.Fournisseur = new Tms.Classes.Business.Fournisseur();
            mClass.Fournisseur.ID = int.Parse(GetFormValue("cmbDetFournisseur"));
            mClass.Fournisseur.Nom = X.GetCmp<ComboBox>("cmbDetFournisseur").SelectedItem.Text.ToString();

            mClass.NumConteneur = GetFormValue("txtContainerNumber");
            mClass.Immatriculation = GetFormValue("txtTruckID");
            mClass.NumPlomb = GetFormValue("txtSealNumber");
            mClass.Tracteur = GetFormValue("txtTractorID");
            mClass.NumOT = GetFormValue("txtShipmentNumber");
            mClass.Chauffeur = GetFormValue("txtDriverName");
            mClass.Numero = GetFormValue("txtDeliveryNumber");
            mClass.NumeroExterne = GetFormValue("txtExternalWayBill");
            
            mClass.Certification = null;
            result = int.TryParse(GetFormValue("cmbDetCertification"), out iConverted);
            if (result)
            {
                mClass.Certification = new Tms.Classes.Shared.Certification();
                mClass.Certification.ID = int.Parse(GetFormValue("cmbDetCertification"));
                mClass.Certification.Designation = X.GetCmp<ComboBox>("cmbDetCertification").SelectedItem.Text.ToString(); 
            }

            mClass.Transporteur = null;
            result = int.TryParse(GetFormValue("cmbDetTransporter"), out iConverted);
            if (result)
            {
                mClass.Transporteur = new Tms.Classes.Shared.Transporteur();
                mClass.Transporteur.ID = int.Parse(GetFormValue("cmbDetTransporter"));
                mClass.Transporteur.Nom = X.GetCmp<ComboBox>("cmbDetTransporter").SelectedItem.Text.ToString();
            }
            
            mClass.NumLot = GetFormValue("txtLotNumber");

            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }
                
        private int GetCritriaValue(string strComponent)
        {
            int value;
        
            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
                return value;
            else
                return -1;
        }
        
        
              

        #endregion


    }
}