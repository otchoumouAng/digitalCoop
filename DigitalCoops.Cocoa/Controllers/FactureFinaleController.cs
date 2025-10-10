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
using Tms.Classes.Business.Sales;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class FactureFinaleController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        // GET: FactureFinale
        public ActionResult Index()
        {
            var mParam = (new Parametres()).fnSelect();

            Parametres mclass = new Parametres();
            mclass = mParam[0] as Parametres;

            Campagne mCampagne = new Campagne();

            X.GetCmp<ComboBox>("cmbCropYear").SetValue(mclass.Campagne);

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("dtpStartDate").RawText = StartDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Exportateur = "{Tous}";

            X.GetCmp<FormPanel>("FinalInvoiceCriteriaPanel").SetTitle("Campagne : " + mclass.Campagne + " | Exportateur : " + Exportateur  + " | Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            if (HasAccess.fnGetUserAccessStatus("{052807B7-6BF8-4125-9134-20BFEB24D18A}", UserName) == false)
                X.GetCmp<MenuItem>("btnNew").Disable();
            else
                X.GetCmp<MenuItem>("btnNew").Enable();

            if (HasAccess.fnGetUserAccessStatus("{AC417C02-7793-4DD6-A292-FBB5951C6A1F}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExport").Disable();
            else
                X.GetCmp<MenuItem>("mnuExport").Enable();

            if (HasAccess.fnGetUserAccessStatus("{1329ECDE-BC18-467F-8888-8DD28EDF2F56}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintHistory").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintHistory").Enable();

            X.GetCmp<Hidden>("ffhiddenPermApprove").SetValue(HasAccess.fnGetUserAccessStatus("{98F17894-9A1A-474F-B7B4-C8517D6FC21B}", UserName));
            X.GetCmp<Hidden>("ffhiddenPermCancel").SetValue(HasAccess.fnGetUserAccessStatus("{67DC040D-BE58-4A5A-BB74-26A2AAAC452D}", UserName));
            X.GetCmp<Hidden>("ffhiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{AC417C02-7793-4DD6-A292-FBB5951C6A1F}", UserName));
            X.GetCmp<Hidden>("ffhiddenPermPrintInvoice").SetValue(HasAccess.fnGetUserAccessStatus("{B4A7A126-D8E5-4CF8-80D0-382B5DB936BF}", UserName));
            X.GetCmp<Hidden>("ffhiddenPermPrintHistory").SetValue(HasAccess.fnGetUserAccessStatus("{1329ECDE-BC18-467F-8888-8DD28EDF2F56}", UserName));
            X.GetCmp<Hidden>("ffhiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{1375876D-BBD9-4373-90CB-1BE6463E48E6}", UserName));
            X.GetCmp<Hidden>("ffhiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{052807B7-6BF8-4125-9134-20BFEB24D18A}", UserName));
            X.GetCmp<Hidden>("ffhiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{822CE521-F9CF-4720-8698-D788073EC10D}", UserName));
            #endregion
            return View();
        }

        public ActionResult onCreate()
        {
            FactureFinaleViewModel viewModel = new FactureFinaleViewModel();

            viewModel._FactureFinale = new FactureFinale();


            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;


            return new Ext.Net.MVC.PartialViewResult { ViewName = "FactureFinale_Detail" , Model = viewModel };
        }
        public ActionResult onEdit(string ItemSelected)
        {
            FactureFinale mclass = JSON.Deserialize<FactureFinale>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            FactureFinaleViewModel viewModel = new FactureFinaleViewModel();

            viewModel._FactureFinale = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FactureFinale_Detail" , Model = viewModel };
        }

        public ActionResult onConsult(string ItemSelected)
        {

            FactureFinale mclass = JSON.Deserialize<FactureFinale>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            FactureFinaleViewModel viewModel = new FactureFinaleViewModel();

            viewModel._FactureFinale = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;


            return new Ext.Net.MVC.PartialViewResult { ViewName = "FactureFinale_Detail", Model = viewModel };
        }
        public ActionResult OnApprove(string ItemSelected)
        {

            FactureFinale mclass = JSON.Deserialize<FactureFinale>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            FactureFinaleViewModel viewModel = new FactureFinaleViewModel();

            viewModel._FactureFinale = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FactureFinale_Detail", Model = viewModel };

        }

        public ActionResult OnCancel(string ItemSelected)
        {
            try
            {
                FactureFinale mClass = JSON.Deserialize<FactureFinale>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);

                if (!result)
                    throw new Exception("OnCancel : Facture Finale loading failed.");

                bool resultFinCancel = false;
                mClass.UtilisateurModification = (string)Session["userName"];
                resultFinCancel = mClass.fnCancel();

                if (resultFinCancel)
                {
                    Store mstore = X.GetCmp<Store>("storeListeFinalInvoice");

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
                    Title = "Facture Finale : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult LoadOfFinalInvoice(StoreRequestParameters parameters, string ItemCropYear, string ItemExporter, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {

            int ExportateurID = GetCriteriaValue(ItemExporter);
            string Campagne = string.IsNullOrEmpty(ItemCropYear) ? "" : ItemCropYear;

            if (!string.IsNullOrEmpty(ItemCropYear) && ItemCropYear.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string Status = ItemStatus;

            var mListe = (new FactureFinale()).fnSelect(Campagne, ExportateurID, StartDate, EndDate, Status);
            
            return this.Store(mListe);
        }

        public ActionResult SetAmountToLetter(string Amount)
        {
            X.GetCmp<DisplayField>("txtNetAmountInLetter").SetValue("");
            if (Amount != "0")
            {
                string val = MoneySpeller.ToLettres(Int32.Parse(Amount.Replace(" ", "")));
                X.GetCmp<DisplayField>("txtNetAmountInLetter").SetValue(val);
                //string val = MoneySpeller.NumberToWords(Int32.Parse(Amount.Replace(" ", "")));

                //X.GetCmp<TextField>("TxtTotalPayementInLetter").SetValue(val + " F CFA");
            }

            return this.Direct();
        }


        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("FinalInvoiceCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemCropYear, string ItemExporter, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeFinalInvoice");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCropYear"   ,ItemCropYear),
                                    new Ext.Net.Parameter("ItemExporter"   ,ItemExporter),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus)
                                });



                // collapse criterias areas
                X.GetCmp<FormPanel>("FinalInvoiceCriteriaPanel").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture Finale : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitApprovalMethod()
        {

            try
            {
                FactureFinale mClass = new FactureFinale();
                mClass.IsNew = false;

                bool result = mClass.fnGet(Guid.Parse(GetFormValue("hiddenID")));

                if (!result)
                    throw new Exception("OnApprove : Final invoice loading failed.");



                bool resultFinApprobation = false;

                if (mClass.Statut == "NA")
                {
                    mClass.UtilisateurApprobation = (string)Session["userName"];
                    resultFinApprobation = mClass.fnApprove();
                }

                if (resultFinApprobation)
                {

                    Store mstore = X.GetCmp<Store>("storeListeFinalInvoice");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();



                    X.GetCmp<Window>("FactureFinale_Detail").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture Finale : SubmitApprovalMethod",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnSelectShipment()
        {
            EmbarquementViewModel mclass = new EmbarquementViewModel();

            //var StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            //var EndDate = DateTime.Now.ToShortDateString();

            var Exportateur = "{Tous}";
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;


            return new Ext.Net.MVC.PartialViewResult { ViewName = "Select_Embarquement", Model = mclass };


        }

        public ActionResult OnRefreshForAvailableShipments(string ItemExportateur)
        {

            int ExportateurID = GetCriteriaValue(ItemExportateur);


            //DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            //DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            Store mstore = X.GetCmp<Store>("storeListShipment");
            mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemExportateur"   ,ItemExportateur)
                                    //,
                                    //new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    //new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd)
                                });

            return this.Direct();
        }


        public ActionResult LoadListOfAvailableShipments(string ItemExportateur)
        {

            int ExportateurID = GetCriteriaValue(ItemExportateur);


            //DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            //DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            var mListe = (new Embarquement()).fnSelectForFinalInvoice("{Tous}", ExportateurID, -1, null, null, 0);

            //return this.Store(paging);
            return this.Store(mListe);
        }


        public ActionResult SubmitOnSelectShipment(string ItemSelected)
        {
            try
            {
                Embarquement Item = JSON.Deserialize<Embarquement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                Parametres mParam = new Parametres(0);

                if (Item != null)
                {
                    X.GetCmp<TextField>("txtShipmentNumber").Text = Item.Numero;
                    X.GetCmp<Hidden>("txtEmbarquementID").Value = Item.ID;

                    X.GetCmp<TextField>("txtNumberOfBags").Value = Item.NbreSacs;
                    X.GetCmp<TextField>("txtQuantity").Value = Item.QuantiteAsString;
                    decimal PoidsStandard = Item.NbreSacs * mParam.PoidsStdNetUnitaire;
                    X.GetCmp<TextField>("txtStandardWeight").Value = string.Format("{0:#,#}", PoidsStandard).TrimStart();
                    X.GetCmp<TextField>("txtNetWeightReceive").Value = Item.PoidsArriveeAsString;
                    X.GetCmp<TextField>("txtSampleWeight").Value = Item.PoidsEchantillonArriveeAsString;
                    X.GetCmp<TextField>("txtPriceCAF").Value = Item.Contrat.PrixAsString;
                    X.GetCmp<TextField>("txtPriceFob").Value = Item.Contrat.PrixFobAsString;

                    decimal PrixCAF = Item.Contrat.Prix;
                    decimal PrixFob = Item.Contrat.PrixFob;
                    //Calcul
                    decimal TotalSurPlus = decimal.Round(Item.PoidsArrivee + Item.PoidsEchantillonArrivee);
                    decimal PoidsSurPlus = decimal.Round( Item.PoidsArrivee - PoidsStandard);

                    if (PoidsSurPlus != 0)
                        X.GetCmp<TextField>("txtWeightSurplus").Value = string.Format("{0:#,#}", PoidsSurPlus).TrimStart();

                    X.GetCmp<TextField>("txtTotalSurplus").Value = string.Format("{0:#,#}", TotalSurPlus).TrimStart();

                    decimal taux = mParam.FactureFinaleBonusTaux; 
                    decimal MontantBonus = Math.Round( ((TotalSurPlus * taux ) / 100 ) * (Item.Contrat.Prix/1000),3) ;

                    X.GetCmp<TextField>("txtRateBonus").Value = string.Format("{0:#,#}", taux).TrimStart();
                    X.GetCmp<TextField>("txtRateBonus").Value = taux;
                    X.GetCmp<TextField>("txtAmountBonus").Value = string.Format("{0:#,##0.###}", MontantBonus).TrimStart();

                    decimal MontantBrut = Math.Round(((PoidsStandard * (PrixFob/1000)) + (Item.PoidsEchantillonArrivee * (PrixFob/1000)) + (PoidsSurPlus * (PrixCAF/1000)) + (MontantBonus)),3);
                    X.GetCmp<TextField>("txtGrossAmount").Value = string.Format("{0:#,##0.###}", MontantBrut).TrimStart();
                    X.GetCmp<TextField>("txtCommercialInvoice").Value = Item.FactureCommercialeMonantAsString;

                    decimal MontantNet = Math.Round(MontantBrut - Item.FactureCommercialeMonant,3);
                    X.GetCmp<TextField>("txtNetAmount").Value = string.Format("{0:#,##0.###}", MontantNet).TrimStart();

                    X.GetCmp<Button>("BtnOkDetail").Enable();

                    X.GetCmp<Window>("ListOfShipments").Close();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Select Shipment : SubmitOnSelectShipment",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitFormMethod()
        {

            DataSource _db = new DataSource();
            DataTransaction mTran = new DataTransaction();

            try
            {
                FactureFinale mClass = new FactureFinale();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("hiddenID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Facture Finale load failed.");

                    if (mClass.Desactive == true)
                        throw new Exception("SubmitFormMethod : Facture Finale is disabled");
                }

                bool Result = true;

                _db = mClass.db();
                mTran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);


                mClass = MapFormToObject(mClass);
                Result = mClass.fnUpdate(mTran);
                if (!Result)
                {
                    _db.RollBackTransaction(mTran);
                }
                else
                {
                    _db.CommitTransaction(mTran);

                    Store mStore = X.GetCmp<Store>("storeListeFinalInvoice");


                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionFinalInvoice").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();


                    }

                    X.GetCmp<Window>("FactureFinale_Detail").Close();
                }

            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mTran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture Finale : SubmitFormMethod",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }
        public ActionResult OnPrintInvoice(string ItemID)
        {

            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'FactureFinale{0}', '{1}/FactureFinale/ViewReportInvoice?id={0}', this,'Facture Finale ', '')", ItemID, BaseUrl));
        }

        public ActionResult ViewReportInvoice(string id)
        {
            try
            {
                rptFinalInvoice report = new rptFinalInvoice();

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["paramID"].Value = id;

                report.Parameters["paramID"].Visible = false;
                ViewData["Report"] = report;

                return View("ViewReportResult");
            }
            catch (Exception ex)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture Finale : Facture ",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
        }

        public ActionResult OnPrintFinalInvoice(string typeReport)
        {
            ViewData["typeReport"] = typeReport;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintFactureFinaleReport", ViewData = ViewData };

        }

        public ActionResult PrintFinalInvoice(string TypeReport)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;
                string type = "";

                if (TypeReport == "Hi")
                {
                    report = new rptFactureFinaleHistory() as XtraReport;

                    type = "Facture Finale - History";

                }

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["paramCampagne"].Value = X.GetCmp<ComboBox>("EnrcropYearForReport").SelectedItem.Text;

                report.Parameters["paramExportateurID"].Value = X.GetCmp<ComboBox>("EnrexportateurForReport").SelectedItem.Value.ToString();
                report.Parameters["paramExportateur"].Value = X.GetCmp<ComboBox>("EnrexportateurForReport").SelectedItem.Text;

                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("EnrstartDateForReport").RawText.ToString());
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("EnrdueDateForReport").RawText.ToString());

                if (TypeReport == "Hi")
                {
                    report.Parameters["paramStatutID"].Value = X.GetCmp<ComboBox>("EnrStatus").SelectedItem.Value.ToString();
                    report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("EnrStatus").SelectedItem.Text;
                }
                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/FactureFinale/ViewList', this, '{2}',''),App.FormPrintFactureFinaleReport.doClose()", Guid.NewGuid(), BaseUrl, type));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture Finale : Data Validation",
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

            return View("ViewReport");
        }

        #region "Methods"
        private FactureFinale MapFormToObject(FactureFinale mClass)
        {
            try
            {
                mClass.ID = Guid.Parse(X.GetCmp<Hidden>("hiddenID").Text);

                mClass.Numero = X.GetCmp<TextField>("txtNumber").Text;
                mClass.Date = DateTime.Parse(X.GetCmp<DateField>("dtfInvoiceDate").RawText.ToString()).Add(DateTime.Now.TimeOfDay);
                if (X.GetCmp<TextField>("txtStandardWeight").Text != string.Empty) mClass.PoidsNetStandard = decimal.Parse(X.GetCmp<TextField>("txtStandardWeight").Text);
                if (X.GetCmp<TextField>("txtNetWeightReceive").Text != string.Empty) mClass.PoidsRecu = decimal.Parse(X.GetCmp<TextField>("txtNetWeightReceive").Text);
                if (X.GetCmp<TextField>("txtSampleWeight").Text != string.Empty) mClass.PoidsEchantillon = decimal.Parse(X.GetCmp<TextField>("txtSampleWeight").Text);
                if (X.GetCmp<TextField>("txtWeightSurplus").Text != string.Empty) mClass.PoidsSurplus = decimal.Parse(X.GetCmp<TextField>("txtWeightSurplus").Text);
                if (X.GetCmp<TextField>("txtTotalSurplus").Text != string.Empty) mClass.TotalSurplus = decimal.Parse(X.GetCmp<TextField>("txtTotalSurplus").Text);

                if (X.GetCmp<TextField>("txtRateBonus").Text != string.Empty) mClass.TauxBonus = decimal.Parse(X.GetCmp<TextField>("txtRateBonus").Text);
                if (X.GetCmp<TextField>("txtAmountBonus").Text != string.Empty) mClass.MontantBonus = decimal.Parse(X.GetCmp<TextField>("txtAmountBonus").Text);

                if (X.GetCmp<TextField>("txtGrossAmount").Text != string.Empty) mClass.MontantBrut = decimal.Parse(X.GetCmp<TextField>("txtGrossAmount").Text);
                if (X.GetCmp<TextField>("txtCommercialInvoice").Text != string.Empty) mClass.MontantFactureCommerciale = decimal.Parse(X.GetCmp<TextField>("txtCommercialInvoice").Text);
                if (X.GetCmp<TextField>("txtNetAmount").Text != string.Empty) mClass.MontantNet = decimal.Parse(X.GetCmp<TextField>("txtNetAmount").Text);


                Embarquement mEmb = new Embarquement();
                mEmb.ID = Guid.Parse(X.GetCmp<TextField>("txtEmbarquementID").Text);
                mEmb.Numero = X.GetCmp<TextField>("txtShipmentNumber").Text;
                if (X.GetCmp<TextField>("txtNumberOfBags").Text != string.Empty) mEmb.NbreSacs = int.Parse(X.GetCmp<TextField>("txtNumberOfBags").Text);
                if (X.GetCmp<TextField>("txtQuantity").Text != string.Empty) mEmb.Quantite = decimal.Parse(X.GetCmp<TextField>("txtQuantity").Text);
                mEmb.Contrat = new ContratDeVentes();
                if (X.GetCmp<TextField>("txtPriceCAF").Text != string.Empty) mEmb.Contrat.Prix = decimal.Parse(X.GetCmp<TextField>("txtPriceCAF").Text);
                if (X.GetCmp<TextField>("txtPriceFob").Text != string.Empty) mEmb.Contrat.PrixFob = decimal.Parse(X.GetCmp<TextField>("txtPriceFob").Text);

                mClass.Embarquement = mEmb;

                
                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];



            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture Finale : MapFormToObject",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

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
            X.GetCmp<RowSelectionModel>("rowSelectionFinalInvoice").DeselectAll();
        }

        #endregion

    }
}