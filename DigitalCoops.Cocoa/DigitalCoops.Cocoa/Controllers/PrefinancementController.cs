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
    public class PrefinancementController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        const string Balance = "Balance";
        const string Execution = "Execution";

        

        // GET: Prefinancement
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

            string Type = "{Tous}";

            X.GetCmp<FormPanel>("PrefinancementCriteriaPanel").SetTitle("Campagne : " + mclass.Campagne + " | Exportateur : " + Exportateur + " | Type : " + Type + " | Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            if (HasAccess.fnGetUserAccessStatus("{CFE5B8CD-E2AA-4B46-9D11-EDBC66F3ED56}", UserName) == false)
                X.GetCmp<MenuItem>("btnNew").Disable();
            else
                X.GetCmp<MenuItem>("btnNew").Enable();

            if (HasAccess.fnGetUserAccessStatus("{07AC0636-AFFD-41D1-9688-AEB7707F3DA9}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExport").Disable();
            else
                X.GetCmp<MenuItem>("mnuExport").Enable();

            if (HasAccess.fnGetUserAccessStatus("{77C0FBD6-D548-4B9E-8748-0F571A680F27}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintBalance").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintBalance").Enable();

            if (HasAccess.fnGetUserAccessStatus("{CDAA7C41-7B10-4550-9B5E-DBEC3515686C}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintExecution").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintExecution").Enable();

           

            if (HasAccess.fnGetUserAccessStatus("{35A65B10-9C75-4F5D-8D1E-A38B26FA5616}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintHistory").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintHistory").Enable();

            X.GetCmp<Hidden>("fihiddenPermApprove").SetValue(HasAccess.fnGetUserAccessStatus("{D3CF508E-026C-46BC-868E-73079895670C}", UserName));
            X.GetCmp<Hidden>("fihiddenPermCancel").SetValue(HasAccess.fnGetUserAccessStatus("{5289CCDA-7EE3-4372-A787-1ED4F45CDFAC}", UserName));
            X.GetCmp<Hidden>("fihiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{07AC0636-AFFD-41D1-9688-AEB7707F3DA9}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintBalance").SetValue(HasAccess.fnGetUserAccessStatus("{77C0FBD6-D548-4B9E-8748-0F571A680F27}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintExecution").SetValue(HasAccess.fnGetUserAccessStatus("{CDAA7C41-7B10-4550-9B5E-DBEC3515686C}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintAgreement").SetValue(HasAccess.fnGetUserAccessStatus("{9549E7D7-D5BC-4CEB-9723-89581EE49234}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintHistory").SetValue(HasAccess.fnGetUserAccessStatus("{35A65B10-9C75-4F5D-8D1E-A38B26FA5616}", UserName));
            X.GetCmp<Hidden>("fihiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{82EA60E7-461C-463F-ACC4-927B2AA2DBD4}", UserName));
            X.GetCmp<Hidden>("fihiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{CFE5B8CD-E2AA-4B46-9D11-EDBC66F3ED56}", UserName));
            X.GetCmp<Hidden>("fihiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{463AF901-63B2-4B28-87B1-545248778DB3}", UserName));
            #endregion
            return View();
        }

        public ActionResult onCreate()
        {
            PrefinancementViewModel mclass = new PrefinancementViewModel();

            mclass._Prefinancement = new Prefinancement();

            mclass._Prefinancement.Campagne = new Parametres(0).Campagne;

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            #region "Access"
            string UserName = (string)Session["userName"];
           
            Fonction HasAccess = new Fonction();

            bool PaymentPermissionRemove = HasAccess.fnGetUserAccessStatus("{E327B639-6B81-4042-9D23-7363465AAB32}", UserName);
            bool PaymentPermissionAdd = HasAccess.fnGetUserAccessStatus("{3BCD854F-921D-4460-83C5-72B7F11776F7}", UserName);
            bool PaymentPermissionEdit = HasAccess.fnGetUserAccessStatus("{0AE3DFAB-9049-4F0B-A6FB-73454D4E842E}", UserName);


            if (PaymentPermissionRemove == true)
            {
                ViewData["PaymentPermissionRemove"] = true;
            }
            else
            {
                ViewData["PaymentPermissionRemove"] = false;
            }

            if (PaymentPermissionAdd == true)
            {
                ViewData["PaymentPermissionAdd"] = true;
            }
            else
            {
                ViewData["PaymentPermissionAdd"] = false;
            }

            if (PaymentPermissionEdit == true)
            {
                ViewData["PaymentPermissionEdit"] = true;
            }
            else
            {
                ViewData["PaymentPermissionEdit"] = false;
            }
            #endregion

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Prefinancement_Detail", Model = mclass,ViewData = ViewData };
        }
        public ActionResult onEdit(string ItemSelected)
        {
            
            Prefinancement mclass = JSON.Deserialize<Prefinancement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            PrefinancementViewModel viewModel = new PrefinancementViewModel();

            viewModel._Prefinancement = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            int TypeApc = new Tms.Classes.Shared.Parametres(0).PrefinancementTypeApc;
            int TypeBlank = new Tms.Classes.Shared.Parametres(0).PrefinancementTypeBlank;

            if(TypeApc ==  mclass.PrefinancementType.ID)
            {
                ViewData["type"] = "Apc";
            }
            else
            {
                ViewData["type"] = "Blank";
            }


            #region "Access"
            string UserName = (string)Session["userName"];

            Fonction HasAccess = new Fonction();

            bool PaymentPermissionRemove = HasAccess.fnGetUserAccessStatus("{E327B639-6B81-4042-9D23-7363465AAB32}", UserName);
            bool PaymentPermissionAdd = HasAccess.fnGetUserAccessStatus("{3BCD854F-921D-4460-83C5-72B7F11776F7}", UserName);
            bool PaymentPermissionEdit = HasAccess.fnGetUserAccessStatus("{0AE3DFAB-9049-4F0B-A6FB-73454D4E842E}", UserName);


            if (PaymentPermissionRemove == true)
            {
                ViewData["PaymentPermissionRemove"] = true;
            }
            else
            {
                ViewData["PaymentPermissionRemove"] = false;
            }

            if (PaymentPermissionAdd == true)
            {
                ViewData["PaymentPermissionAdd"] = true;
            }
            else
            {
                ViewData["PaymentPermissionAdd"] = false;
            }

            if (PaymentPermissionEdit == true)
            {
                ViewData["PaymentPermissionEdit"] = true;
            }
            else
            {
                ViewData["PaymentPermissionEdit"] = false;
            }
            #endregion

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Prefinancement_Detail", Model = viewModel, ViewData = ViewData };
        }

        public ActionResult onConsult(string ItemSelected)
        {
           
            Prefinancement mclass = JSON.Deserialize<Prefinancement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            PrefinancementViewModel viewmodel = new PrefinancementViewModel();
            viewmodel._Prefinancement = mclass;
            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            ViewData["PaymentPermissionRemove"] = false;
            ViewData["PaymentPermissionAdd"] = false;
            ViewData["PaymentPermissionEdit"] = false;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Prefinancement_Detail", Model = viewmodel, ViewData = ViewData };
        }
        public ActionResult OnApprove(string ItemSelected)
        {
            
            Prefinancement mclass = JSON.Deserialize<Prefinancement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            PrefinancementViewModel viewModel = new PrefinancementViewModel();

            viewModel._Prefinancement = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;

            #region "Access"
            ViewData["PaymentPermissionRemove"] = false;
            ViewData["PaymentPermissionAdd"] = false;
            ViewData["PaymentPermissionEdit"] = false;
            #endregion

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Prefinancement_Detail", Model = viewModel, ViewData = ViewData };

        }

        public ActionResult OnCancel(string ItemSelected)
        {
            try
            {
                Prefinancement mClass = JSON.Deserialize<Prefinancement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);
                bool resultFinCancel = false;
                if (!result)
                    throw new Exception("OnCancel : Prefinancing loading failed.");

                mClass.UtilisateurModification = (string)Session["userName"];
                resultFinCancel = mClass.fnCancel();
                
                if (resultFinCancel)
                {
                    Store mstore = X.GetCmp<Store>("storeListePrefinancement");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();

                    //DeselectGridRows();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prefinancing : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }
        public ActionResult LoadListOfPrefinancing(StoreRequestParameters parameters, string ItemCropYear, string ItemExporter, string ItemTypeFinancing, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {

            int ExportateurID = GetCriteriaValue(ItemExporter);
            int TypeID = GetCriteriaValue(ItemTypeFinancing);
            string Campagne = string.IsNullOrEmpty(ItemCropYear) ? "" : ItemCropYear;

            if (!string.IsNullOrEmpty(ItemCropYear) && ItemCropYear.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string Status = ItemStatus;

            var mListe = (new Prefinancement()).fnSelect(Campagne, ExportateurID, TypeID, StartDate, EndDate, Status);
            //var paging = GridStorePaging.SetRangePlants(parameters, mListe);

            //return this.Store(paging);
            return this.Store(mListe);
        }

        public ActionResult LoadListPayment(string ItemExecMode, string ItemPrefinancementID)
        {

            List<DataPersist> myList = new List<DataPersist>();
            if (!string.IsNullOrEmpty(ItemPrefinancementID))
            {

                if (ItemExecMode == "AddNew")
                {


                }
                else
                {
                    myList = new PaiementCheque().fnSelect(Guid.Parse(ItemPrefinancementID), -1, -1);
                }

                PaiementCheque mClass = new PaiementCheque();
                if (myList.Count > 0)
                    mClass = myList[0] as PaiementCheque;
                else myList = new List<DataPersist>();

            }
            else
            {
                Store mstore = X.GetCmp<Store>("storeListPaiementCheque");
                mstore.RemoveAll();
                myList = null;
            }

            return this.Store(myList);
        }

       

        //public ActionResult LoadListOfFinancingToApprove(StoreRequestParameters parameters, string ItemCampagne, string ItemExportateur)
        //{

        //    int ExportateurID = GetCriteriaValue(ItemExportateur);

        //    string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "" : ItemCampagne;

        //    if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
        //    {
        //        Campagne = "";
        //    }

        //    var mListe = (new Prefinancement()).fnSelectForApproval(Campagne, ExportateurID);
        //    //var paging = GridStorePaging.SetRangePlants(parameters, mListe);

        //    return this.Store(mListe);
        //}
        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("PrefinancementCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemCropYear, string ItemExporter, string ItemTypeFinancing, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListePrefinancement");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemExporter"   ,ItemExporter),
                                    new Ext.Net.Parameter("ItemCropYear"      ,ItemCropYear),
                                    new Ext.Net.Parameter("ItemTypeFinancing"  ,ItemTypeFinancing),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus)
                                });

                

                // collapse criterias areas
                X.GetCmp<FormPanel>("PrefinancementCriteriaPanel").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prefinancement : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }
        public ActionResult UpdateFormMethod(string storeListPaiementCheque)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();
            DataTransaction mTran = new DataTransaction();

            try
            {
                Prefinancement mClass = new Prefinancement();
              
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("pfhiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("txtPrefinancementID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("UpdateFormMethod : SalesPrefinancing load failed.");

                   

                    if (!string.IsNullOrEmpty(mClass.UtilisateurApprobation) || mClass.DateApprobation != null)
                        throw new Exception("UpdateFormMethod : Prefinancing Already approved ! Please Refresh Overview");

                    if (!string.IsNullOrEmpty(mClass.Statut) && mClass.IsRejected)
                        throw new Exception("UpdateFormMethod : Prefinancing Already rejected ! Please Refresh Overview");
                }

                bool Result = true;

                _db = mClass.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                mClass = MapFormToObject(mClass);
                Result = mClass.fnUpdate(mtran);


                if (!Result)
                {
                    _db.RollBackTransaction(mtran);
                }
                else
                {
                    _db.CommitTransaction(mtran);

                    List<PaiementCheque> mPaieList= JSON.Deserialize<List<PaiementCheque>>(storeListPaiementCheque, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (mPaieList.Count > 0)
                    {
                        if (formExecMode != Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                        {
                            mClass.fnRemovePaymentAll();
                        }

                        for (int i = 0; i < mPaieList.Count; i++)
                        {
                            if (mPaieList.ElementAt(i).Desactive == false)
                            {

                                PaiementCheque Paiement = new PaiementCheque();
                                _db = Paiement.db();
                                mTran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                                Paiement = mPaieList.ElementAt(i);
                                Paiement.IsNew = true;
                                Paiement.Prefinancement = new Prefinancement();
                                Paiement.Prefinancement.ID = mClass.ID;


                                if (Paiement.Banque == null)
                                {
                                    Paiement.Banque = null;
                                }
                                else {
                                    if (Paiement.Banque.ID == 0)
                                    {
                                        Paiement.Banque = null;
                                    }
                                }

                                Paiement.UtilisateurCreation = (string)Session["userName"];
                                Paiement.UtilisateurModification = (string)Session["userName"];
                                Result = Paiement.fnUpdate();
                            }



                            if (!Result)
                            {
                                break;
                            }
                        }

                        if (!Result)
                        {
                            _db.RollBackTransaction(mTran);
                        }
                        else
                        {
                            _db.CommitTransaction(mTran);

                        }

                    }



                }
                Store mStore = X.GetCmp<Store>("storeListePrefinancement");

                Prefinancement mPre = new Prefinancement();
                mPre.fnGet(mClass.ID);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                {
                    mStore.Insert(0, mPre);
                    X.GetCmp<RowSelectionModel>("rowSelectionListePrefinancement").Select(0);
                }
                else
                {
                    ModelProxy mProxy = mStore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mPre);

                    mProxy.Commit();

                    mProxy.EndEdit();


                }

                X.GetCmp<Window>("Prefinancement_Detail").Close();

            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mtran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prefinancing : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitApproval()
        {

            try
            {
                Prefinancement mClass = new Prefinancement();
                mClass.IsNew = false;

                bool result = mClass.fnGet(Guid.Parse(GetFormValue("txtPrefinancementID")));

                if (!result)
                    throw new Exception("OnApprove : Prefinancing loading failed.");

                mClass.UtilisateurApprobation = (string)Session["userName"];

                bool resultFinApprobation = false;

                if (mClass.Statut == "NA")
                {
                    resultFinApprobation = mClass.fnApprove();
                }

                if (resultFinApprobation)
                {

                    Store mstore = X.GetCmp<Store>("storeListePrefinancement");
                    
                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();

                   

                    X.GetCmp<Window>("Prefinancement_Detail").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prefinancing : Approuver",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnPrintPrefinancing(string typeReport)
        {
            ViewData["typeReport"] = typeReport;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintPrefinancementReport", ViewData = ViewData };

        }

        public ActionResult PrintPrefinancing(string TypeReport)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;
                string type = "";
                if(TypeReport == "Ba")
                {
                    report = new rptPrefinancementBalance() as XtraReport;
                    type = "Prefinancing - Balance";
                }
                if(TypeReport == "Ex")
                {
                    report = new rptPrefinancementExecution() as XtraReport;
                    type = "Prefinancing - Execution";
                }
                if(TypeReport == "Hi")
                {
                    report = new rptPrefinancementHistory() as XtraReport;
                   
                    type = "Prefinancing - History";

                }

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["paramCampagne"].Value = X.GetCmp<ComboBox>("PFcropYearForReport").SelectedItem.Text;

                report.Parameters["paramExportateurID"].Value = X.GetCmp<ComboBox>("PFexportateurForReport").SelectedItem.Value.ToString();
                report.Parameters["paramExportateur"].Value = X.GetCmp<ComboBox>("PFexportateurForReport").SelectedItem.Text;

                report.Parameters["paramTypeID"].Value = X.GetCmp<ComboBox>("PITypePrefinancementForReport").SelectedItem.Value.ToString();
                report.Parameters["paramType"].Value = X.GetCmp<ComboBox>("PITypePrefinancementForReport").SelectedItem.Text;

                if (TypeReport == "Hi" || TypeReport == "Ex")
                {

                    report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("PFstartDateForReport").RawText.ToString());
                    report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("PFdueDateForReport").RawText.ToString());

                }

                if (TypeReport == "Hi")
                {
                  
                    report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("FPTStatus").SelectedItem.Value.ToString();
                    report.Parameters["paramStatutNom"].Value = X.GetCmp<ComboBox>("FPTStatus").SelectedItem.Text;
                 
                }
                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Prefinancement/ViewList', this, '{2}',''),App.FormPrintPrefinancementReport.doClose()", Guid.NewGuid(), BaseUrl,type));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sales Prefinancing : Data Validation",
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
        private Prefinancement MapFormToObject(Prefinancement mClass)
        {
            try
            {
                mClass.ID = Guid.Parse(X.GetCmp<Hidden>("txtPrefinancementID").Text);

                mClass.Numero = X.GetCmp<TextField>("txtNumber").Text;

                mClass.Campagne = X.GetCmp<ComboBox>("_cmbCampagne").Text;

                Exportateur mExportateur = new Exportateur();
                mExportateur.ID = int.Parse(X.GetCmp<ComboBox>("_cmbExportateur").Text);
                mExportateur.Nom = X.GetCmp<ComboBox>("_cmbExportateur").SelectedItem.Text.ToString();
                mClass.Exportateur = mExportateur;

                PrefinancementType mType = new PrefinancementType();
                mType.ID = int.Parse(X.GetCmp<ComboBox>("_cmbType").Text);
                mType.Designation = X.GetCmp<ComboBox>("_cmbType").SelectedItem.Text.ToString();
                mClass.PrefinancementType = mType;


                mClass.ReferenceExterne = X.GetCmp<TextField>("txtExternalRef").Text;

                mClass.Date = DateTime.Parse(X.GetCmp<DateField>("txtDate").RawText.ToString());
               
                if (X.GetCmp<TextField>("txtPrice").Text != string.Empty) mClass.Prix = decimal.Parse(X.GetCmp<TextField>("txtPrice").Text);
                if (X.GetCmp<TextField>("txtTonnage").Text != string.Empty) mClass.Tonnage = decimal.Parse(X.GetCmp<TextField>("txtTonnage").Text);
                if (X.GetCmp<TextField>("txtPaid").Text != string.Empty) mClass.TonnagePaye = decimal.Parse(X.GetCmp<TextField>("txtPaid").Text);

                if (X.GetCmp<TextField>("txtAmount").Text != string.Empty) mClass.Montant = decimal.Parse(X.GetCmp<TextField>("txtAmount").Text);
                if (X.GetCmp<TextField>("txtAmountEuro").Text != string.Empty) mClass.MontantEuro = decimal.Parse(X.GetCmp<TextField>("txtAmountEuro").Text);


                mClass.Commentaire = X.GetCmp<TextArea>("txtComment").Text;

                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];

                mClass.Solde = mClass.IsActive ? mClass.Montant : mClass.Solde;

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prefinancing : MapFormToObject",
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
            //X.Js.Call("App.rowSelectionListe.deselectAll();");
            X.GetCmp<RowSelectionModel>("rowSelectionListePrefinancement").DeselectAll();
        }

        #endregion
    }
}