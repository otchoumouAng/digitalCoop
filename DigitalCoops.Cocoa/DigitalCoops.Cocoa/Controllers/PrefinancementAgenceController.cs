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
using Tms.Classes.Business.Sites;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class PrefinancementAgenceController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        const string Balance = "Balance";
        const string Execution = "Execution";

        // GET: PrefinancementAgence
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);

            X.GetCmp<ComboBox>("cmbCropYear").SetValue(mParam.Campagne);

            Site mSiteParDefaut = new Site();
            string UserName = (string)Session["userName"];
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            DateTime firstdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            X.GetCmp<DateField>("dtpStartDate").RawText = firstdayofmonth.ToShortDateString();
            X.GetCmp<DateField>("dtpEndDate").RawText = DateTime.Now.ToShortDateString();

            //string Exportateur = "{Tous}";

            //string Type = "{Tous}";

            X.GetCmp<FormPanel>("PrefinancementAgenceCriteriaPanel").SetTitle("Site : " + mSiteParDefaut.Nom + ", Campagne : " + mParam.Campagne + " | Du : " + firstdayofmonth.ToShortDateString() + " Au : " + DateTime.Now.ToShortDateString());

            #region Set Function's Access
            
            Fonction HasAccess = new Fonction();

            if (HasAccess.fnGetUserAccessStatus("{F658B53B-D500-4830-8D92-C2D8CE906802}", UserName) == false)
                X.GetCmp<MenuItem>("btnNew").Disable();
            else
                X.GetCmp<MenuItem>("btnNew").Enable();

            if (HasAccess.fnGetUserAccessStatus("{752EEDB6-D59E-4006-8E4B-B0BA688F96EC}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExport").Disable();
            else
                X.GetCmp<MenuItem>("mnuExport").Enable();

            if (HasAccess.fnGetUserAccessStatus("{05EF98DD-BD0A-4A26-844F-99132B225CC2}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintBalance").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintBalance").Enable();

            if (HasAccess.fnGetUserAccessStatus("{BC34206E-B577-4104-8F8D-380960C984BB}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintExecution").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintExecution").Enable();

            if (HasAccess.fnGetUserAccessStatus("{440D04AD-A7B6-4BD1-A128-198518A49233}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintHistory").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintHistory").Enable();

            X.GetCmp<Hidden>("fihiddenPermApprove").SetValue(HasAccess.fnGetUserAccessStatus("{E0EABA31-3453-4D48-B6BB-CE8E483BD2E0}", UserName));
            X.GetCmp<Hidden>("fihiddenPermCancel").SetValue(HasAccess.fnGetUserAccessStatus("{FE9F02DA-048B-4964-829F-FF1E0883882A}", UserName));
            X.GetCmp<Hidden>("fihiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{752EEDB6-D59E-4006-8E4B-B0BA688F96EC}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintBalance").SetValue(HasAccess.fnGetUserAccessStatus("{05EF98DD-BD0A-4A26-844F-99132B225CC2}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintExecution").SetValue(HasAccess.fnGetUserAccessStatus("{BC34206E-B577-4104-8F8D-380960C984BB}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintAgreement").SetValue(HasAccess.fnGetUserAccessStatus("{D4F62BB1-02F7-44F4-8882-2B28DF90AC18}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintHistory").SetValue(HasAccess.fnGetUserAccessStatus("{440D04AD-A7B6-4BD1-A128-198518A49233}", UserName));
            X.GetCmp<Hidden>("fihiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{42B42061-18E5-44F4-AF6F-CF4A13DB1AA3}", UserName));
            X.GetCmp<Hidden>("fihiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{F658B53B-D500-4830-8D92-C2D8CE906802}", UserName));
            X.GetCmp<Hidden>("fihiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{012BA5AC-AD56-4F01-9674-98DE95B951AE}", UserName));
            #endregion
            return View();
        }

        public ActionResult onCreate()
        {
            PrefinancementAgenceViewModel mclass = new PrefinancementAgenceViewModel();
            Parametres mParam = new Parametres(0);
            mclass._PrefinancementAgence = new PrefinancementAgence();

            mclass._PrefinancementAgence.Campagne = mParam.Campagne;
            mclass._PrefinancementAgence.Site = new Site();            

            string UserName = (string)Session["userName"];

            Fonction HasAccessFunction = new Fonction();
            bool HasAccessAllSite = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);

            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            if (result)
                mclass._PrefinancementAgence.Site.ID = mSiteParDefaut.ID;

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            #region "Access"          
           
            Fonction HasAccess = new Fonction();

            //bool PaymentPermissionRemove = HasAccess.fnGetUserAccessStatus("{E327B639-6B81-4042-9D23-7363465AAB32}", UserName);
            //bool PaymentPermissionAdd = HasAccess.fnGetUserAccessStatus("{3BCD854F-921D-4460-83C5-72B7F11776F7}", UserName);
            //bool PaymentPermissionEdit = HasAccess.fnGetUserAccessStatus("{0AE3DFAB-9049-4F0B-A6FB-73454D4E842E}", UserName);


            //if (PaymentPermissionRemove == true)
            //{
            //    ViewData["PaymentPermissionRemove"] = true;
            //}
            //else
            //{
            //    ViewData["PaymentPermissionRemove"] = false;
            //}

            //if (PaymentPermissionAdd == true)
            //{
            //    ViewData["PaymentPermissionAdd"] = true;
            //}
            //else
            //{
            //    ViewData["PaymentPermissionAdd"] = false;
            //}

            //if (PaymentPermissionEdit == true)
            //{
            //    ViewData["PaymentPermissionEdit"] = true;
            //}
            //else
            //{
            //    ViewData["PaymentPermissionEdit"] = false;
            //}
            #endregion
            return new Ext.Net.MVC.PartialViewResult { ViewName = "PrefinancementAgence_Detail", Model = mclass};
        }
        public ActionResult onEdit(string ItemSelected)
        {

            PrefinancementAgence mclass = JSON.Deserialize<PrefinancementAgence>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            PrefinancementAgenceViewModel viewModel = new PrefinancementAgenceViewModel();

            viewModel._PrefinancementAgence = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "PrefinancementAgence_Detail", Model = viewModel, ViewData = ViewData };
        }

        public ActionResult OnAddCashDisbursement(string ItemSelected)
        {
            PrefinancementAgence mclass = JSON.Deserialize<PrefinancementAgence>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            PrefinancementAgenceViewModel viewModel = new PrefinancementAgenceViewModel();

            viewModel._PrefinancementAgence = mclass;                      

            string UserName = (string)Session["userName"];

            Fonction HasAccessFunction = new Fonction();
            bool HasAccessAllSite = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);           

            //viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            #region "Access"
            //string UserName = (string)Session["userName"];

            Fonction HasAccess = new Fonction();

            //bool PaymentPermissionRemove = HasAccess.fnGetUserAccessStatus("{E327B639-6B81-4042-9D23-7363465AAB32}", UserName);
            //bool PaymentPermissionAdd = HasAccess.fnGetUserAccessStatus("{3BCD854F-921D-4460-83C5-72B7F11776F7}", UserName);
            //bool PaymentPermissionEdit = HasAccess.fnGetUserAccessStatus("{0AE3DFAB-9049-4F0B-A6FB-73454D4E842E}", UserName);


            //if (PaymentPermissionRemove == true)
            //{
            //    ViewData["PaymentPermissionRemove"] = true;
            //}
            //else
            //{
            //    ViewData["PaymentPermissionRemove"] = false;
            //}

            //if (PaymentPermissionAdd == true)
            //{
            //    ViewData["PaymentPermissionAdd"] = true;
            //}
            //else
            //{
            //    ViewData["PaymentPermissionAdd"] = false;
            //}

            //if (PaymentPermissionEdit == true)
            //{
            //    ViewData["PaymentPermissionEdit"] = true;
            //}
            //else
            //{
            //    ViewData["PaymentPermissionEdit"] = false;
            //}
            #endregion
            return new Ext.Net.MVC.PartialViewResult { ViewName = "PrefinancementAgence_CashDisbursement", Model = viewModel, ViewData = ViewData };
        }

        public ActionResult onConsult(string ItemSelected)
        {

            PrefinancementAgence mclass = JSON.Deserialize<PrefinancementAgence>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            PrefinancementAgenceViewModel viewmodel = new PrefinancementAgenceViewModel();
            viewmodel._PrefinancementAgence = mclass;
            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            //ViewData["PaymentPermissionRemove"] = false;
            //ViewData["PaymentPermissionAdd"] = false;
            //ViewData["PaymentPermissionEdit"] = false;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "PrefinancementAgence_Detail", Model = viewmodel, ViewData = ViewData };
        }
        public ActionResult OnApprove(string ItemSelected)
        {

            PrefinancementAgence mclass = JSON.Deserialize<PrefinancementAgence>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            PrefinancementAgenceViewModel viewModel = new PrefinancementAgenceViewModel();

            viewModel._PrefinancementAgence = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;

            #region "Access"
            //ViewData["PaymentPermissionRemove"] = false;
            //ViewData["PaymentPermissionAdd"] = false;
            //ViewData["PaymentPermissionEdit"] = false;
            #endregion

            return new Ext.Net.MVC.PartialViewResult { ViewName = "PrefinancementAgence_Detail", Model = viewModel, ViewData = ViewData };

        }

        public ActionResult OnCancel(string ItemSelected)
        {
            try
            {
                PrefinancementAgence mClass = JSON.Deserialize<PrefinancementAgence>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);
                bool resultFinCancel = false;
                if (!result)
                    throw new Exception("OnCancel : Prefinancing loading failed.");

                mClass.UtilisateurModification = (string)Session["userName"];
                resultFinCancel = mClass.fnCancel();

                if (resultFinCancel)
                {
                    Store mstore = X.GetCmp<Store>("storeListePrefinancementAgence");

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
        public ActionResult LoadListOfPrefinancing(StoreRequestParameters parameters, string ItemSite, string ItemCropYear, /*string ItemExporter, string ItemTypeFinancing,*/ string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {

            //int ExportateurID = GetCriteriaValue(ItemExporter);
            //int TypeID = GetCriteriaValue(ItemTypeFinancing);
            int SiteID = GetCriteriaValue(ItemSite);
            string Campagne = string.IsNullOrEmpty(ItemCropYear) ? "" : ItemCropYear;

            if (!string.IsNullOrEmpty(ItemCropYear) && ItemCropYear.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string Status = ItemStatus;

            var mListe = (new PrefinancementAgence()).fnSelect(SiteID, Campagne,/* ExportateurID, TypeID,*/ StartDate, EndDate, Status);
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
                    myList = new PaiementChequeAgence().fnSelect(Guid.Parse(ItemPrefinancementID), /*-1,*/ -1);
                }

                PaiementChequeAgence mClass = new PaiementChequeAgence();
                if (myList.Count > 0)
                    mClass = myList[0] as PaiementChequeAgence;
                else myList = new List<DataPersist>();

            }
            else
            {
                Store mstore = X.GetCmp<Store>("storeListPaiementChequeAgence");
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
            FormPanel mform = X.GetCmp<FormPanel>("PrefinancementAgenceCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemSite, string ItemCropYear,/* string ItemExporter, string ItemTypeFinancing, */string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListePrefinancementAgence");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    //new Ext.Net.Parameter("ItemExporter"   ,ItemExporter),
                                    new Ext.Net.Parameter("ItemSite"   ,ItemSite),
                                    new Ext.Net.Parameter("ItemCropYear"      ,ItemCropYear),
                                    //new Ext.Net.Parameter("ItemTypeFinancing"  ,ItemTypeFinancing),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus)
                                });



                // collapse criterias areas
                X.GetCmp<FormPanel>("PrefinancementAgenceCriteriaPanel").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prefinancing : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitCD(string storeListPaiementChequeAgence)
        {
            DataSource _db = new DataSource();            
            DataTransaction mTran = new DataTransaction();

            try
            {
                PrefinancementAgence mClass = new PrefinancementAgence();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("pfhiddenExecMode").Value);
                mClass.fnGet(Guid.Parse(GetFormValue("txtPrefinancementAgenceID")));
                   
                bool Result = true;

                _db = mClass.db();
                mTran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                mClass = MapFormToObject(mClass);

                decimal montantSolde = 0;
                PaiementChequeAgence Paiement = new PaiementChequeAgence();
                List<PaiementChequeAgence> mPaieList = JSON.Deserialize<List<PaiementChequeAgence>>(storeListPaiementChequeAgence, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                if (mPaieList.Count > 0)
                {
                    //if (formExecMode != Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    //{
                    //    mClass.fnRemovePaymentAll();
                    //}
                    foreach (PaiementChequeAgence item in mPaieList)
                    {
                        montantSolde += item.Montant;
                        if (item.IsNew)
                        {
                            Paiement = new PaiementChequeAgence();
                            Paiement.PrefinancementAgence = new PrefinancementAgence();

                            Paiement.SetDataSource(_db);
                            Paiement.Montant = item.Montant;
                            
                            Paiement.Commentaire = item.Commentaire;
                            Paiement.Date = item.Date;
                            Paiement.PrefinancementAgence.ID = mClass.ID;
                            Paiement.UtilisateurCreation = (string)Session["userName"];
                            Paiement.UtilisateurModification = (string)Session["userName"];
                            Result = Paiement.fnUpdate();
                        }
                        if (!Result)
                        {
                            if(item.Montant >= montantSolde)
                                montantSolde -= item.Montant;
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
                    mClass.Solde = mClass.Montant - montantSolde;
                    mClass.Couverture = 100 - (((mClass.Montant - montantSolde) / mClass.Montant) * 100);
                    Store mStore = X.GetCmp<Store>("storeListePrefinancementAgence");
                    ModelProxy mProxy = mStore.GetById(mClass.ID);
                    mProxy.BeginEdit();
                    mProxy.Set(mClass);
                    mProxy.Commit();
                    mProxy.EndEdit();
                }

                X.GetCmp<Window>("PrefinancementAgence_CashDisbursement").Close();

            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mTran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Cash Disbursement : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult UpdateFormMethod()
        {            
            try
            {
                PrefinancementAgence mClass = new PrefinancementAgence();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("pfhiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("txtPrefinancementAgenceID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("UpdateFormMethod : SalesPrefinancing load failed.");



                    if (!string.IsNullOrEmpty(mClass.UtilisateurApprobation) || mClass.DateApprobation != null)
                        throw new Exception("UpdateFormMethod : Prefinancing Already approved ! Please Refresh Overview");

                    //if (!string.IsNullOrEmpty(mClass.Statut) && mClass.IsRejected)
                      //  throw new Exception("UpdateFormMethod : Prefinancing Already rejected ! Please Refresh Overview");
                }

                bool Result = true;

                mClass = MapFormToObject(mClass);
                Result = mClass.fnUpdate();
                
                Store mStore = X.GetCmp<Store>("storeListePrefinancementAgence");

                //PrefinancementAgence mPre = new PrefinancementAgence();
                //mPre.fnGet(mClass.ID);
                mClass.Couverture = 0;
                mClass.Solde = mClass.Montant;
                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                {
                    
                    
                    mStore.Insert(0, mClass);
                    X.GetCmp<RowSelectionModel>("rowSelectionListePrefinancementAgence").Select(0);
                }
                else
                {
                    ModelProxy mProxy = mStore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();


                }

                X.GetCmp<Window>("PrefinancementAgence_Detail").Close();

            }
            catch (Exception ex)
            {               
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
                PrefinancementAgence mClass = new PrefinancementAgence();
                mClass.IsNew = false;

                bool result = mClass.fnGet(Guid.Parse(GetFormValue("txtPrefinancementAgenceID")));

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
                    mClass.Couverture = 0;
                    mClass.Solde = mClass.Montant;
                    Store mstore = X.GetCmp<Store>("storeListePrefinancementAgence");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();

                    X.GetCmp<Window>("PrefinancementAgence_Detail").Close();
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
                if (TypeReport == "Ba")
                {
                    report = new rptPrefinancementBalance() as XtraReport;
                    type = "Prefinancing - Balance";
                }
                if (TypeReport == "Ex")
                {
                    report = new rptPrefinancementExecution() as XtraReport;
                    type = "Prefinancing - Execution";
                }
                if (TypeReport == "Hi")
                {
                    report = new rptPrefinancementHistory() as XtraReport;

                    type = "Prefinancing - History";

                }

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["paramCampagne"].Value = X.GetCmp<ComboBox>("PFcropYearForReport").SelectedItem.Text;

                //report.Parameters["paramExportateurID"].Value = X.GetCmp<ComboBox>("PFexportateurForReport").SelectedItem.Value.ToString();
                //report.Parameters["paramExportateur"].Value = X.GetCmp<ComboBox>("PFexportateurForReport").SelectedItem.Text;

                //report.Parameters["paramTypeID"].Value = X.GetCmp<ComboBox>("PITypePrefinancementForReport").SelectedItem.Value.ToString();
                //report.Parameters["paramType"].Value = X.GetCmp<ComboBox>("PITypePrefinancementForReport").SelectedItem.Text;

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

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/PrefinancementAgence/ViewList', this, '{2}',''),App.FormPrintPrefinancementReport.doClose()", Guid.NewGuid(), BaseUrl, type));
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
        private PrefinancementAgence MapFormToObject(PrefinancementAgence mClass)
        {
            try
            {
                mClass.ID = Guid.Parse(X.GetCmp<Hidden>("txtPrefinancementAgenceID").Text);

                mClass.Numero = X.GetCmp<TextField>("txtNumber").Text;

                mClass.Campagne = X.GetCmp<ComboBox>("_cmbCampagne").Text;

                mClass.Site = new Site();
                mClass.Site.ID = int.Parse(GetFormValue("_cmbSite"));
                mClass.Site.Nom = X.GetCmp<ComboBox>("_cmbSite").SelectedItem.Text.ToString();                

                mClass.Date = DateTime.Parse(X.GetCmp<DateField>("txtDateCP").RawText.ToString());

                if (X.GetCmp<TextField>("txtAmount").Text != string.Empty) mClass.Montant = decimal.Parse(X.GetCmp<TextField>("txtAmount").Text);
                //if (X.GetCmp<TextField>("txtAmountEuro").Text != string.Empty) mClass.MontantEuro = decimal.Parse(X.GetCmp<TextField>("txtAmountEuro").Text);

                mClass.Description = X.GetCmp<TextArea>("txtComment").Text;

                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];

                //mClass.Solde = mClass.IsActive ? mClass.Montant : mClass.Solde;

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
            X.GetCmp<RowSelectionModel>("rowSelectionListePrefinancementAgence").DeselectAll();
        }

        public ActionResult OnPrintPaiement(string ItemSelected, string IsCopy)
        {
            bool ReportIscopy = false;
            if (string.IsNullOrEmpty(IsCopy))
                ReportIscopy = true;

            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            //string param = JSON.Serialize(contratperiode);
            Session["ParamReport"] = ItemSelected;

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'CashDisbursementVoucher{0}', '{1}/PrefinancementAgence/ViewVoucher?IsCopy={2}', this, 'Cash Disbursement Voucher','')", Guid.NewGuid(), BaseUrl, ReportIscopy));
        }

        public ActionResult ViewVoucher(bool IsCopy)
        {
            XtraReport report = null;
            //Payement payement = new Payement();
            //payement.fnGet(id);
            PrefinancementAgence mClass = JSON.Deserialize<PrefinancementAgence>((string)Session["ParamReport"], new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            report = new rptCashDisbursement() as XtraReport;
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["ID"].Value = mClass.ID;
            report.Parameters["Number"].Value = mClass.Numero;
            report.Parameters["AmountInLetter"].Value = MoneySpeller.ToLettres(Int32.Parse(mClass.Montant.ToString()));
            report.Parameters["IsCopy"].Value = IsCopy;

            ViewData["Report"] = report;

            return View();
        }

        #endregion
    }
}