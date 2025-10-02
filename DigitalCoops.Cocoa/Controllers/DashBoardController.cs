using Ext.Net;
using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Shared;

namespace Tms2017.MVC.Controllers
{
    public class DashBoardController : BaseController
    {
        // GET: DashBoard
        public ActionResult Index()
        {
            //var mParam = (new Parametres()).fnSelect();

            //Parametres mclass = new Parametres();
            //mclass.fnGet();

            //Campagne mCampagne = new Campagne();

            //mCampagne.fnGet(mclass.Campagne);

            //X.GetCmp<Hidden>("VolumeTodayDefaultCampagne").SetValue(mclass.Campagne);            

            //X.GetCmp<ComboBox>("CropYearVolumeCropYearToDateID").SetValue(mclass.Campagne);
            //X.GetCmp<DateField>("TxtPeriodStartVolumeCropYearToDate").RawText = (DateTime.Parse(mCampagne.DateDebut.ToString())).ToShortDateString();
            //X.GetCmp<DateField>("TxtPeriodEndVolumeCropYearToDate").RawText = DateTime.Now.ToShortDateString();
            //X.GetCmp<DateField>("TxtPeriodStartVolumeCropYearToDate").MinDate = (DateTime)mCampagne.DateDebut;
            //X.GetCmp<DateField>("TxtPeriodEndVolumeCropYearToDate").MaxDate = (DateTime)mCampagne.DateFin;

            //X.GetCmp<ComboBox>("CropYearFinancingID").SetValue(mclass.Campagne);
            //X.GetCmp<DateField>("TxtPeriodStartFinancing").RawText = (DateTime.Parse(mCampagne.DateDebut.ToString())).ToShortDateString();
            //X.GetCmp<DateField>("TxtPeriodEndFinancing").RawText = DateTime.Now.ToShortDateString();
            //X.GetCmp<DateField>("TxtPeriodStartFinancing").MinDate = (DateTime)mCampagne.DateDebut;
            //X.GetCmp<DateField>("TxtPeriodEndFinancing").MaxDate = (DateTime)mCampagne.DateFin;

            //X.GetCmp<FormPanel>("VolumeTodayPanel").SetTitle("Volume Today : " + DateTime.Now.ToShortDateString());
            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();

            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            X.GetCmp<FormPanel>("VolumeCropYearToDatePanel").SetTitle("Volume ( Campagne à ce jour )");            
            X.GetCmp<FormPanel>("FinancingCropYearToDatePanel").SetTitle("Financement ( Campagne à ce jour )");

            return View();
        }

        public ActionResult IndexFournisseur(string ItemFournisseur, string ItemFournisseurName)
        {
            int fournisseurID = -1;
            if (!string.IsNullOrEmpty(ItemFournisseur.ToString()))
                fournisseurID = int.Parse(ItemFournisseur);

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();

            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            X.GetCmp<ComboBox>("cmbVolumeFournisseurID").SetValue(fournisseurID);
            //X.GetCmp<DateField>("TxtPeriodStartSupplierVolume").RawText = (DateTime.Parse(mCampagne.DateDebut.ToString())).ToShortDateString();
            //X.GetCmp<DateField>("TxtPeriodEndSupplierVolume").RawText = DateTime.Now.ToShortDateString();
            //X.GetCmp<DateField>("TxtPeriodStartSupplierVolume").MinDate = (DateTime)mCampagne.DateDebut;
            //X.GetCmp<DateField>("TxtPeriodEndSupplierVolume").MaxDate = (DateTime)mCampagne.DateFin;

            X.GetCmp<ComboBox>("cmbVolumeFinancingFournisseurID").SetValue(fournisseurID);
            //X.GetCmp<DateField>("TxtPeriodStartVolumeSupplierFinancing").RawText = (DateTime.Parse(mCampagne.DateDebut.ToString())).ToShortDateString();
            //X.GetCmp<DateField>("TxtPeriodEndVolumeSupplierFinancing").RawText = DateTime.Now.ToShortDateString();
            //X.GetCmp<DateField>("TxtPeriodStartVolumeSupplierFinancing").MinDate = (DateTime)mCampagne.DateDebut;
            //X.GetCmp<DateField>("TxtPeriodEndVolumeSupplierFinancing").MaxDate = (DateTime)mCampagne.DateFin;            

            X.GetCmp<FormPanel>("panelSuppliersVolume").SetTitle("Volume ( Campagne à ce Jour ) | Fournisseur : " + ItemFournisseurName);
            X.GetCmp<FormPanel>("FinancingCropYearToDatePanel").SetTitle("Financement ( Campagne à ce Jour ) | Fournisseur : " + ItemFournisseurName);

            return View();
        }

        public ActionResult IndexStock()
        {           

            //X.GetCmp<FormPanel>("VolumeTodayPanel").SetTitle("Volume Today : " + DateTime.Now.ToShortDateString());
            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();

            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;
            Parametres mParam = new Parametres(0);
            string Currentcampagne = mParam.Campagne;
            int MagasinID = mParam.Magasin.ID;
            string magasin = mParam.Magasin.Designation;
            ViewBag.MagasinExport = mParam.MagasinExport;

            //Campagne mCampagne = new Campagne();
            //mCampagne.fnGet(Currentcampagne);

            X.GetCmp<ComboBox>("cmbFiltreCampagne").SetValue(Currentcampagne);
            X.GetCmp<ComboBox>("cmbFiltreMagasin").SetValue(MagasinID);
            //ViewBag.CampagneMinDate = mCampagne.DateDebut;

            return View();
        }


        #region Volume Today
        public ActionResult GetVolumeTodayDataForChart(string ItemPeriodStart, string ItemPeriodEnd)
        {
            if (string.IsNullOrEmpty(ItemPeriodStart) && string.IsNullOrEmpty(ItemPeriodEnd))
            {
                return this.Store(null);
            }
            else
            {
                DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
                DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

                var mParam = (new Parametres()).fnSelect();

                Parametres mclass = new Parametres();
                mclass.fnGet();

                Campagne mCampagne = new Campagne();

                mCampagne.fnGet(mclass.Campagne);

                var mList = (new VolumToday()).fnSelect(string.Empty,startdate, enddate);

                List<ChartModel> mlistChart = new List<ChartModel>();

                VolumToday volume = new VolumToday();
                volume = mList[2] as VolumToday;

                ChartModel chart;
                chart = new ChartModel();
                chart.Libelle = "Accepted";
                chart.Total = volume.Accepted;
                mlistChart.Add(chart);
                chart = new ChartModel();
                chart.Libelle = "Rejected";
                chart.Total = volume.Rejected;
                mlistChart.Add(chart);

                return this.Store(mlistChart);
            }
        }

        //public static List<ChartModel> GenerateData()
        //{
        //    return new List<ChartModel>()
        //    {
        //        new ChartModel() {  Libelle= "Accepted",Total= 300 },
        //        new ChartModel() {  Libelle= "Rejected",Total= 150 },
        //    };

        //}

        public ActionResult GetVolumeTodayData(string ItemCampagne)
        {
            if (string.IsNullOrEmpty(ItemCampagne))
            {
                return this.Store(null);
            }
            else
            {

                DateTime? startdate = DateTime.Now;
                DateTime? enddate = DateTime.Now;
                
                var mList = (new VolumToday()).fnSelect(ItemCampagne,startdate, enddate);

                return this.Store(mList);
            }
        }

        public ActionResult onRefreshVolumeToday(string ItemCampagne)
        {
            try
            {
                //DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                //DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeDshVolumeToday");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemCampagne"   ,ItemCampagne)
                                //new Ext.Net.Parameter("ItemPeriodStart"     ,ItemPeriodStart),
                                //new Ext.Net.Parameter("ItemPeriodEnd"           ,ItemPeriodEnd)
                            });               
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "DashBoard : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });

            }
           
            //X.GetCmp<FormPanel>("VolumeTodayPanel").SetTitle("Volume Du : " + ItemPeriodStart + " Au : " + ItemPeriodEnd + " | Campagne : " + campagne);
            
            return this.Direct();
        }

        #endregion

        #region Volume CropYear To Date
        public ActionResult GetVolumeCropYearToDateChart(string ItemPeriodStart, string ItemPeriodEnd)
        {
            if (string.IsNullOrEmpty(ItemPeriodStart) && string.IsNullOrEmpty(ItemPeriodEnd))
            {
                return this.Store(null);
            }
            else
            {
                DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
                DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

                var mParam = (new Parametres()).fnSelect();

                Parametres mclass = new Parametres();
                mclass.fnGet();

                Campagne mCampagne = new Campagne();

                mCampagne.fnGet(mclass.Campagne);

                var mList = (new VolumToday()).fnSelect(string.Empty, startdate, enddate);

                List<ChartModel> mlistChart = new List<ChartModel>();

                VolumToday volume = new VolumToday();
                volume = mList[2] as VolumToday;

                ChartModel chart;
                chart = new ChartModel();
                chart.Libelle = "Accepted";
                chart.Total = volume.Accepted;
                mlistChart.Add(chart);
                chart = new ChartModel();
                chart.Libelle = "Rejected";
                chart.Total = volume.Rejected;
                mlistChart.Add(chart);

                return this.Store(mlistChart);
            }
        }

        //public static List<ChartModel> GenerateData()
        //{
        //    return new List<ChartModel>()
        //    {
        //        new ChartModel() {  Libelle= "Accepted",Total= 300 },
        //        new ChartModel() {  Libelle= "Rejected",Total= 150 },
        //    };

        //}

        public ActionResult GetVolumeCropYearToDate(string ItemExportateur,string ItemFournisseur,string ItemSite = "1")
        {
            int fournisseurID = -1;
            int exporterID = -1;
            int siteID = -1;
            if (!string.IsNullOrEmpty(ItemFournisseur))
                fournisseurID = int.Parse(ItemFournisseur);

            if (!string.IsNullOrEmpty(ItemExportateur))
                exporterID = int.Parse(ItemExportateur);

            if (!string.IsNullOrEmpty(ItemSite))
                siteID = int.Parse(ItemSite);

            //DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            //DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            var mList = (new VolumeCropYearToDate()).fnSelect(exporterID,fournisseurID,siteID);
            //var mList = (new VolumeCropYearToDate()).fnSelect(ItemCampagne, startdate, enddate, fournisseurID);
            VolumeCropYearToDate mVolume = new VolumeCropYearToDate();
            if (mList.Count > 0)
            {
                mVolume = mList[2] as VolumeCropYearToDate;
                decimal TotalRecu = mVolume.InProcess + mVolume.Accepted + mVolume.Rejected;                
                if(TotalRecu > 0)
                    mVolume.RejectedPr = Math.Round((mVolume.Rejected / TotalRecu) * 100,3).ToString();
                else mVolume.Rejected = 0;
                mList[2] = mVolume;
            }
            return this.Store(mList);
        }

        public ActionResult onRefreshVolumeCropYearToDate(string ItemExportateur,string storeID, string ItemSite)
        {
            try
            {
                //DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                //DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>(storeID);

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemExportateur"   ,ItemExportateur),
                                new Ext.Net.Parameter("ItemSite"   ,ItemSite)
                            });
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "DashBoard : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });

            }
            
            //X.GetCmp<FormPanel>("VolumeCropYearToDatePanel").SetTitle("Volume Du : " + ItemPeriodStart + " Au : " + ItemPeriodEnd );
            return this.Direct();
        }

        public ActionResult GetVolumeCropYearToDateProgress(string ItemExportateur,string ItemFournisseur, int ItemSite = -1)
        {
            int fournisseurID = -1;
            int exportateurID = -1;
            if (!string.IsNullOrEmpty(ItemFournisseur))
                fournisseurID = int.Parse(ItemFournisseur);

            if (!string.IsNullOrEmpty(ItemExportateur))
                exportateurID = int.Parse(ItemExportateur);

            //DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            //DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            //var mList = (new VolumeCropYearToDate()).fnSelectProgress(ItemCampagne, startdate, enddate, fournisseurID);
            var mList = (new VolumeCropYearToDate()).fnSelectProgress(exportateurID,fournisseurID,ItemSite);
            List<LineChartModel> mListLine = new List<LineChartModel>();
            LineChartModel mLine;

            foreach (VolumeCropYearToDate item in mList)
            {
                mLine = new LineChartModel();
                mLine.Designation = item.Designation;
                mLine.Accepted = item.Accepted;
                mLine.Rejected = item.Rejected;
                mListLine.Add(mLine);
            }

            return this.Store(mListLine);
        }

        public ActionResult GetVolumeCropYearToDatePerformers(int ItemSite = -1)
        {
            //DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            //DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            var mList = (new VolumeCropYearToDate()).fnSelectPerDirectSupplier(ItemSite);
            List<ChartModel> mListChart = new List<ChartModel>();
            ChartModel mChart;

            foreach (VolumeCropYearToDate item in mList)
            {
                mChart = new ChartModel();
                mChart.Libelle = item.Designation;
                mChart.Total = item.Accepted;
                mListChart.Add(mChart);
            }

            return this.Store(mListChart);
        }

        public ActionResult GetVolumeCropYearToDatePerAgencies()
        {
            //DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            //DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            var mList = (new VolumeCropYearToDate()).fnSelectPerAgencies();
            //var mList = (new VolumeCropYearToDate()).fnSelectPerAgencies(ItemCampagne, startdate, enddate);
            List<ChartModel> mListChart = new List<ChartModel>();
            ChartModel mChart;

            foreach (VolumeCropYearToDate item in mList)
            {
                mChart = new ChartModel();
                mChart.Libelle = item.Designation;
                mChart.Total = item.Accepted;
                mListChart.Add(mChart);
            }

            return this.Store(mListChart);
        }


        #endregion

        #region Financing
        public ActionResult GetFinancingStatus(string ItemFournisseur, int ItemSite = 1)
        {
            int fournisseurID = -1;
            
            if (!string.IsNullOrEmpty(ItemFournisseur))
                fournisseurID = int.Parse(ItemFournisseur);            

            //DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            //DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            var mList = (new FinancingCropYearToDate()).fnSelect(fournisseurID, ItemSite);

            FinancingCropYearToDate mClass = new FinancingCropYearToDate();
            if (mList.Count > 0)
            {
                for (int i = 1; i < mList.Count; i++)
                {
                    mClass = mList[i] as FinancingCropYearToDate;
                    mClass.Saving = null;
                    mClass.Exposure = null;
                    mList[i] = mClass;
                }
            }

            return this.Store(mList);
        }

        public ActionResult onRefreshFinancingStatus(string ItemFournisseur, string storeID)
        {
            try
            {
                //DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                //DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>(storeID);

                mstore.Reload();

                //mstore.Reload(new Ext.Net.ParameterCollection()
                //            {
                //                new Ext.Net.Parameter("ItemCampagne"   , ItemCampagne),
                //                new Ext.Net.Parameter("ItemPeriodStart"     ,ItemPeriodStart),
                //                new Ext.Net.Parameter("ItemPeriodEnd"           ,ItemPeriodEnd),
                //                new Ext.Net.Parameter("ItemFournisseur"           ,ItemFournisseur),                                
                //            });
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "DashBoard : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });

            }
            
            return this.Direct();
        }

        public ActionResult GetFinancingEvolution(int ItemSite = -1)
        {
            //DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            //DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            var mList = (new FinancingCropYearToDate()).fnSelectEvolution(ItemSite);
            List<LineChartModel> mListLine = new List<LineChartModel>();
            LineChartModel mLine;

            foreach (FinancingCropYearToDate item in mList)
            {
                mLine = new LineChartModel();
                mLine.Designation = item.Designation;
                mLine.Accepted = Int64.Parse(item.Allocated.ToString());
                mListLine.Add(mLine);
            }

            return this.Store(mListLine);
        }

        public ActionResult GetTopBadDebit(int ItemSite = -1)
        {
            //DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            //DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            var mList = (new FinancingCropYearToDate()).fnSelectTopBadDebit(ItemSite);
            List<ChartModel> mListLine = new List<ChartModel>();
            ChartModel mLine;

            foreach (FinancingCropYearToDate item in mList)
            {
                mLine = new ChartModel();
                mLine.Libelle = item.Designation;
                mLine.Total = Int64.Parse(item.Balance.ToString());
                mListLine.Add(mLine);
            }

            return this.Store(mListLine);
        }


        #endregion
        public ActionResult GetLineData()
        {

            return this.Store(GenerateLineData());
        }

        public class ChartModel
        {
            public string Libelle { get; set; }
            public decimal Total { get; set; }
           

        }

        public class LineChartModel
        {
            public string Designation { get; set; }
            public decimal Accepted { get; set; }
            public decimal Rejected { get; set; }            


        }

        public partial class VolumeToday
        {
            public string Libelle { get; set; }
            public Int64 Announced { get; set; }
            public Int64 InProcess { get; set; }
            public Int64 Accepted { get; set; }
            public Int64 Rejected { get; set; }


        }

        public static List<VolumeToday> GenerateVolumeToday()
        {
            return new List<VolumeToday>()
            {
                //new Module() {  ID=1,Title="Suppliers" },
                new VolumeToday() { Libelle = "Nbr Of Trucks", Announced = 410, InProcess = 400, Accepted = 400, Rejected = 0},
                new VolumeToday() { Libelle = "Nbr de sacs", Announced = 253, InProcess = 450, Accepted = 362, Rejected = 10},
                new VolumeToday() { Libelle = "Quantity (T)", Announced = 120, InProcess = 325, Accepted = 750, Rejected = 8}                
            };

        }

        

        public static List<LineChartModel> GenerateLineData()
        {
            return new List<LineChartModel>()
            {
                //new Module() {  ID=1,Title="Suppliers" },
                //new LineChartModel() {  Name= "2016",Data1= 230, Data2= 321, Data3= 20 },
                new LineChartModel() {  Designation= "2017" }                                
            };

        }

        public ActionResult SetPeriod(string ItemCampagne, string IdStart, string IdEnd)
        {            

            Campagne mCampagne = new Campagne();
            if (!string.IsNullOrEmpty(ItemCampagne))
                mCampagne.fnGet(ItemCampagne);
            else return this.Direct();
            
            X.GetCmp<DateField>(IdStart).RawText = DateTime.Parse(mCampagne.DateDebut.ToString()).ToShortDateString();
            X.GetCmp<DateField>(IdEnd).RawText = DateTime.Parse(mCampagne.DateFin.ToString()).ToShortDateString();
            X.GetCmp<DateField>(IdStart).MinDate = (DateTime)mCampagne.DateDebut;
            X.GetCmp<DateField>(IdEnd).MaxDate = (DateTime)mCampagne.DateFin;

            return this.Direct();
        }
    }
}