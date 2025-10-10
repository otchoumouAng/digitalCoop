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
using DevExpress.XtraReports.UI;
using Tms.Classes.Security;
using System.Globalization;
using Tms.Classes.Business.Sites;

namespace Tms2017.MVC.Controllers
{
    public class FinancementController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        const string Balance = "Balance";
        const string Execution = "Execution";
        const string Exposure = "Exposure";

        Guid mID;

        // GET: Financement
        public ActionResult Index()
        {
            Parametres mParam = (new Parametres(0));

            X.GetCmp<ComboBox>("cmbCampagne").SetValue(mParam.Campagne);

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            X.GetCmp<DateField>("dtpStartDate").RawText = StartDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Fournisseur = "{Tous}";

            string Type = "{Tous}";

            X.GetCmp<FormPanel>("CriteriaPanelFI").SetTitle("Site : "+ mSiteParDefaut.Nom + ", Campagne : " + mParam.Campagne + " | Fournisseur : " + Fournisseur + " | Type : " + Type + " | Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access
            
            Fonction HasAccess = new Fonction();
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{B8EC41D0-173A-47A6-BB9A-140942CE66B8}", UserName);
            if (HasAccess.fnGetUserAccessStatus("{9879CFEB-EC68-4C9B-AC20-49363BD45D3F}", UserName) == false)
                X.GetCmp<Button>("btnNew").Disable();
            else
                X.GetCmp<Button>("btnNew").Enable();

            if (HasAccess.fnGetUserAccessStatus("{0971B806-B7C7-4ADF-B6DE-A299FE425BCC}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExport").Disable();
            else
                X.GetCmp<MenuItem>("mnuExport").Enable();

            if (HasAccess.fnGetUserAccessStatus("{9F2AFCA0-4A61-4CD4-955A-9008EEC38005}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintBalance").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintBalance").Enable();

            if (HasAccess.fnGetUserAccessStatus("{C2D91CCF-ABCF-4835-850F-5F5ADEA7D2DA}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintExecution").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintExecution").Enable();

            if (HasAccess.fnGetUserAccessStatus("{8E30EBB7-34F9-4F1D-8D01-2FBAE1932540}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintExposure").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintExposure").Enable();

            if (HasAccess.fnGetUserAccessStatus("{FC2BE14C-5357-48CB-B492-66D1BA3F3E28}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintFinancementList").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintFinancementList").Enable();

            X.GetCmp<Hidden>("fihiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{9879CFEB-EC68-4C9B-AC20-49363BD45D3F}", UserName));
            X.GetCmp<Hidden>("fihiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{BF5B1214-F0E8-4C45-BA92-560B0ACA03E5}", UserName));
            X.GetCmp<Hidden>("fihiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{06DC2A45-78B7-4276-A430-611713CEE5E4}", UserName));
            X.GetCmp<Hidden>("fihiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{0971B806-B7C7-4ADF-B6DE-A299FE425BCC}", UserName));
            X.GetCmp<Hidden>("fihiddenPermSetToLoss").SetValue(HasAccess.fnGetUserAccessStatus("{166A77D6-61E0-4DBF-8F0B-9B25A420D743}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintBalance").SetValue(HasAccess.fnGetUserAccessStatus("{9F2AFCA0-4A61-4CD4-955A-9008EEC38005}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintExecution").SetValue(HasAccess.fnGetUserAccessStatus("{C2D91CCF-ABCF-4835-850F-5F5ADEA7D2DA}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintExposure").SetValue(HasAccess.fnGetUserAccessStatus("{8E30EBB7-34F9-4F1D-8D01-2FBAE1932540}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintAgrement").SetValue(HasAccess.fnGetUserAccessStatus("{210C630E-2D9F-466A-AE26-0AA520EE6835}", UserName));
            X.GetCmp<Hidden>("fihiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{D10DCEFC-0440-43CE-9F3F-B8769CA51D12}", UserName));
            X.GetCmp<Hidden>("fihiddenPermDesactiverApresApprouve").SetValue(HasAccess.fnGetUserAccessStatus("{ABAA37A2-23FC-4879-BA98-F45E5B5637CB}", UserName));

            #endregion

            return View();
        }

        public ActionResult Index_Approval()
        {
            Parametres mParam = (new Parametres(0));            
            
            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetDefaultSite();
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            X.GetCmp<ComboBox>("cmbCampagne").SetValue(mParam.Campagne);

            string Fournisseur = "{Tous}";

            X.GetCmp<FormPanel>("CriteriaPanelFAP").SetTitle("Site : {Tous}, Campagne : " + mParam.Campagne + " | Fournisseur : " + Fournisseur);

            #region Set Function's Access
            
            Fonction HasAccess = new Fonction();
            
            if (HasAccess.fnGetUserAccessStatus("{5E42CD14-A4B0-49CF-A4AC-9FF5BBEAB338}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportApprovedFinancing").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportApprovedFinancing").Enable();

            if (HasAccess.fnGetUserAccessStatus("{EB726BAC-EBB5-42C2-83CF-1E22D028869E}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintBalance").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintBalance").Enable();

            if (HasAccess.fnGetUserAccessStatus("{757D2863-9A36-4876-853A-D653F89C9152}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintExecution").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintExecution").Enable();

            if (HasAccess.fnGetUserAccessStatus("{DC3E3822-8094-4B0D-A87F-BDCFE83AAA3F}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintExposure").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintExposure").Enable();

            if (HasAccess.fnGetUserAccessStatus("{EFBE8AA0-A20B-4CDE-B5C7-86EFECC41EA3}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintFinancementApvList").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintFinancementApvList").Enable();

            X.GetCmp<Hidden>("fihiddenPermApprove").SetValue(HasAccess.fnGetUserAccessStatus("{757DC1F0-B08D-428F-96F3-902AF6E6ADF6}", UserName));
            X.GetCmp<Hidden>("fihiddenPermReject").SetValue(HasAccess.fnGetUserAccessStatus("{9A65E592-8B93-4C46-AFBD-E0BF77AF7A8A}", UserName));
            X.GetCmp<Hidden>("fihiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{5E42CD14-A4B0-49CF-A4AC-9FF5BBEAB338}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintBalance").SetValue(HasAccess.fnGetUserAccessStatus("{EB726BAC-EBB5-42C2-83CF-1E22D028869E}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintExecution").SetValue(HasAccess.fnGetUserAccessStatus("{757D2863-9A36-4876-853A-D653F89C9152}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintExposure").SetValue(HasAccess.fnGetUserAccessStatus("{DC3E3822-8094-4B0D-A87F-BDCFE83AAA3F}", UserName));
            X.GetCmp<Hidden>("fihiddenPermPrintFinancialStatus").SetValue(HasAccess.fnGetUserAccessStatus("{1232A21B-4D18-40AC-AB36-A6F3FFA36906}", UserName));
            X.GetCmp<Hidden>("fihiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{D10DCEFC-0440-43CE-9F3F-B8769CA51D12}", UserName));
            #endregion

            return View();
        }

        public ActionResult onAdd()
        {            
            FinancementViewModel mclass = new FinancementViewModel();

            mclass._Financement = new Financement();
            Parametres mParam = new Parametres(0);
            mclass._Financement.Campagne = mParam.Campagne;
            mclass._Financement.PaymentTaux = mParam.FinancementTauxPaiement;
            mclass._Financement.Sites = new Site();

            string UserName = (string)Session["userName"];

            Fonction HasAccessFunction = new Fonction();
            bool HasAccessAllSite = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);            
            if (HasAccessAllSite) ViewData["UrlFournisseur"] = "LoadFournisseursApproved";
            else ViewData["UrlFournisseur"] = "LoadSupplierBySite";

            ViewData["HasAccessAllSite"] = HasAccessAllSite;

            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            if (result)
                mclass._Financement.Sites.ID = mSiteParDefaut.ID;

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;             

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Financement_Detail", Model = mclass, ViewData = ViewData};
        }

        public ActionResult onEdit(string ItemSelected)
        {            
            Financement mclass = JSON.Deserialize<Financement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            FinancementViewModel viewModel = new FinancementViewModel();

            viewModel._Financement = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAccessAllSite = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            if (HasAccessAllSite) ViewData["UrlFournisseur"] = "LoadFournisseursApproved";
            else ViewData["UrlFournisseur"] = "LoadSupplierBySite";

            ViewData["HasAccessAllSite"] = HasAccessAllSite;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Financement_Detail", Model = viewModel, ViewData = ViewData };
        }
              
        public ActionResult OnCancel(string ItemSelected)
        {            
            try
            {
                Financement mClass = JSON.Deserialize<Financement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);
                bool resultFinCancel = false;
                if (!result)
                    throw new Exception("OnCancel : Financing loading failed.");

                ContratPeriode mContrat = new ContratPeriode();
                if (mClass.FinancementType.ID == (new Parametres(0)).FinancementTypeAvance)
                {
                    bool resultGetContract = mContrat.fnGetByFinancing(mClass.ID);
                    if (!resultGetContract)
                        throw new Exception("OnCancel : Contrat Periode Not Found.");

                    DataSource _db = new DataSource();
                    DataTransaction dTran = new DataTransaction();

                    try
                    {
                        _db = mClass.db();
                        dTran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                        mClass.UtilisateurModification = (string)Session["userName"];

                        resultFinCancel = mClass.fnCancel(dTran);

                        if (resultFinCancel)
                        {
                            mContrat.SetDataSource(_db);

                            mContrat.UtilisateurModification = (string)Session["userName"];
                            bool resultCancelContrat = mContrat.fnDeActivate(dTran);

                            if (!resultCancelContrat)
                            {
                                resultFinCancel = false;
                                _db.RollBackTransaction(dTran);
                            }
                            _db.CommitTransaction(dTran);                            

                        }
                        else
                            _db.RollBackTransaction(dTran);

                    }
                    catch (Exception ex)
                    {
                        _db.RollBackTransaction(dTran);
                        X.MessageBox.Show(new MessageBoxConfig
                        {
                            Title = "Financement : Cancel",
                            Message = ex.Message,
                            Buttons = MessageBox.Button.OK,
                            Icon = MessageBox.Icon.WARNING
                        });
                    }
                }
                else
                {
                    resultFinCancel = mClass.fnCancel();
                }
                if (resultFinCancel)
                {
                    Store mstore = X.GetCmp<Store>("storeListeFinancement");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();

                    DeselectGridRows();
                }
                
            }
            catch (Exception ex)
            {                
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Financement : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult onConsult(string ItemSelected)
        {            
            Financement mclass = JSON.Deserialize<Financement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            FinancementViewModel viewmodel = new FinancementViewModel();
            viewmodel._Financement = mclass;
            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAccessAllSite = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            if (HasAccessAllSite) ViewData["UrlFournisseur"] = "LoadFournisseursApproved";
            else ViewData["UrlFournisseur"] = "LoadSupplierBySite";

            ViewData["HasAccessAllSite"] = HasAccessAllSite;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Financement_Detail", Model = viewmodel, ViewData = ViewData};
        }

        public ActionResult OnSetToLoss(string ItemSelected)
        {

            try
            {
                Financement mClass = JSON.Deserialize<Financement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);

                if (!result)
                    throw new Exception("OnSetToLoss : Financing loading failed.");

                mClass.UtilisateurModification = (string)Session["userName"];

                if (mClass.fnSetToLoss())
                {
                    Store mstore = X.GetCmp<Store>("storeListeFinancement");
                    mClass.Statut = "LO";
                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();

                    DeselectGridRows();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Financement : Set To Loss",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnApprove(string ItemSelected)
        {           
            Financement mclass = JSON.Deserialize<Financement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            FinancementViewModel viewModel = new FinancementViewModel();

            viewModel._Financement = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Financement_Approuve", Model = viewModel };

        }
        public ActionResult SubmitApproval(string ItemSelected)
        {

            try
            {
                Financement mClass = JSON.Deserialize<Financement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);
                bool resultFinApprobation = false;
                if (!result)
                    throw new Exception("OnApprove : Financing loading failed.");

                mClass.UtilisateurApprobation = (string)Session["userName"];

                ContratPeriode mContrat = new ContratPeriode();
                if (mClass.FinancementType.ID == (new Parametres(0)).FinancementTypeAvance)
                {
                    bool mGetContrat = mContrat.fnGetByFinancing(mClass.ID);
                    if (!mGetContrat)
                        throw new Exception("OnApprove : Forward contract not found.");

                    DataSource _db = new DataSource();
                    DataTransaction dTran = new DataTransaction();

                    try
                    {
                        _db = mClass.db();
                        dTran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                        resultFinApprobation = mClass.fnApprove(dTran);

                        if (resultFinApprobation)
                        {
                            mContrat.SetDataSource(_db);
                            mContrat.UtilisateurModification = (string)Session["userName"];
                            bool resultFwdContractApprobation = mContrat.fnApprove(dTran);

                            if (!resultFwdContractApprobation)
                            {
                                resultFinApprobation = false;
                                _db.RollBackTransaction(dTran);
                            }
                            _db.CommitTransaction(dTran);
                        }
                    }
                    catch (Exception ex)
                    {
                        _db.RollBackTransaction(dTran);
                        X.MessageBox.Show(new MessageBoxConfig
                        {
                            Title = "Financement : Approuver",
                            Message = ex.Message,
                            Buttons = MessageBox.Button.OK,
                            Icon = MessageBox.Icon.WARNING
                        });
                    }
                }
                else
                {
                    resultFinApprobation = mClass.fnApprove();                    
                }                

                if (resultFinApprobation)
                {
                    //Store mstore = X.GetCmp<Store>("storeListeFinancingApproval");

                    string mCampagne = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;
                    string mFournisseur = X.GetCmp<ComboBox>("cmbFournisseur").SelectedItem.Value;
                    string mSite = X.GetCmp<ComboBox>("cmbSite").SelectedItem.Value;
                    OnRefreshForApproval(mCampagne, mFournisseur, mSite);

                    //ModelProxy mProxy = mstore.GetById(mClass.ID);

                    //mProxy.Drop();

                    //mProxy.Set(mClass);

                    //mProxy.Commit();

                    //mProxy.EndEdit();

                    X.GetCmp<RowSelectionModel>("rowSelectionListeFAP").DeselectAll();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Financement : Approuver",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnReject(string ItemSelected)
        {
            try
            {
                Financement mClass = JSON.Deserialize<Financement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);
                Parametres mParam = new Parametres(0);
                mClass.UtilisateurRejet = (string)Session["userName"];

                if (mClass.FinancementType.ID == mParam.FinancementTypeAvance)
                {
                    DataSource _ds = new DataSource();
                    DataTransaction _dtr = new DataTransaction();

                    ContratPeriode mContrat = new ContratPeriode();
                    mContrat.fnGetByFinancing(mClass.ID);
                    if (mClass.ID == null || mClass.ID == Guid.Empty)
                        throw new Exception("Error");
                    mContrat.UtilisateurModification = (string)Session["userName"];
                    try
                    {
                        _ds = mClass.db();
                        _dtr = _ds.BeginTransaction(System.Data.IsolationLevel.Serializable);

                        bool resultReject = mClass.fnReject(_dtr);

                        if (resultReject)
                        {
                            mContrat.SetDataSource(_ds);
                            mContrat.fnDeActivate();
                        }
                        else
                        {
                            resultReject = false;
                            result = false;
                            _ds.RollBackTransaction(_dtr);
                        }
                        _ds.CommitTransaction(_dtr);
                    }
                    catch (Exception)
                    {
                        result = false;                        
                        _ds.RollBackTransaction(_dtr);
                    }
                    string mCampagne = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;
                    string mFournisseur = X.GetCmp<ComboBox>("cmbFournisseur").SelectedItem.Value;
                    string mSite = X.GetCmp<ComboBox>("cmbSite").SelectedItem.Value;
                    OnRefreshForApproval(mCampagne, mFournisseur, mSite);
                }
                else
                {
                    if (mClass.fnReject())
                    {
                        //Store mstore = X.GetCmp<Store>("storeListeFinancement");

                        //ModelProxy mProxy = mstore.GetById(mClass.ID);

                        //mProxy.Drop();

                        //mProxy.Set(mClass);

                        //mProxy.Commit();

                        ////mProxy.EndEdit();

                        string mCampagne = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;
                        string mFournisseur = X.GetCmp<ComboBox>("cmbFournisseur").SelectedItem.Value;
                        string mSite = X.GetCmp<ComboBox>("cmbSite").SelectedItem.Value;
                        OnRefreshForApproval(mCampagne, mFournisseur, mSite);
                        //DeselectGridRows();
                    }
                }
                if (!result)
                    throw new Exception("OnReject : Financing loading failed.");                
                
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Financement : Reject",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        [HttpPost]
        public ActionResult UpdateFormMethod()
        {
            try
            {
                Financement mClass = new Financement();
                int? OldTypeFinancement = 0;
                int? NewTypeFinancement;
                bool result = true;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("txtFinancementID")));
                    
                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("UpdateFormMethod : Financing load failed.");

                    OldTypeFinancement = mClass.FinancementType.ID;

                    if (!string.IsNullOrEmpty(mClass.UtilisateurApprobation) || mClass.DateApprobation != null)
                        throw new Exception("UpdateFormMethod : Financing Already approved ! Please Refresh Overview");

                    if (!string.IsNullOrEmpty(mClass.Statut) && mClass.IsRejected)
                        throw new Exception("UpdateFormMethod : Financing Already rejected ! Please Refresh Overview");
                }

                mClass = MapFormToObject(mClass);
                NewTypeFinancement = mClass.FinancementType.ID;
                //Quick check

                if (mClass.DateFinancement > mClass.DateEcheance)                
                    throw new Exception("UpdateFormMethod : The financing date can not be higher than due date.");                                                               

                result = mClass.fnUpdate();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListeFinancement");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionListeFinancement").Select(0);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("Financement_Detail").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Financement : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        [HttpPost]
        public ActionResult UpdateFormMethodOld()
        {
            try
            {
                Financement mClass = new Financement();
                int? OldTypeFinancement = 0;
                int? NewTypeFinancement;
                bool result = true;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("txtFinancementID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("UpdateFormMethod : Financing load failed.");

                    OldTypeFinancement = mClass.FinancementType.ID;

                    if (!string.IsNullOrEmpty(mClass.UtilisateurApprobation) || mClass.DateApprobation != null)
                        throw new Exception("UpdateFormMethod : Financing Already approved ! Please Refresh Overview");

                    if (!string.IsNullOrEmpty(mClass.Statut) && mClass.IsRejected)
                        throw new Exception("UpdateFormMethod : Financing Already rejected ! Please Refresh Overview");
                }

                mClass = MapFormToObject(mClass);
                NewTypeFinancement = mClass.FinancementType.ID;
                //Quick check

                if (mClass.DateFinancement > mClass.DateEcheance)
                {
                    throw new Exception("UpdateFormMethod : The financing date can not be higher than due date.");
                    return null;
                }

                //Verification du type d'avance -- Debuter la transaction si le financement est une avance
                Parametres mParm = new Parametres(0);

                if (mClass.FinancementType.ID == mParm.FinancementTypeAvance)
                {
                    #region Transaction
                    DataSource _db = new DataSource();
                    DataTransaction dTran = new DataTransaction();

                    try
                    {

                        _db = mClass.db();
                        dTran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                        //Creer ou update le financement                       
                        result = mClass.fnUpdate(dTran);

                        if (result)
                        {
                            //Generer un Contrat Periode
                            ContratPeriode mContrat = new ContratPeriode();

                            mContrat.SetDataSource(_db);
                            if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                                mContrat.IsNew = true;
                            else
                            {
                                mContrat.IsNew = false;

                                mContrat.fnGetByFinancing(Guid.Parse(GetFormValue("txtFinancementID")));

                                if (mClass == null || mClass.ID == Guid.Empty)
                                    throw new Exception("SubmitFormMethod : Financing not found.");
                            }

                            mContrat.Campagne = new Campagne();
                            mContrat.Campagne.Designation = mClass.Campagne;

                            mContrat.Fournisseur = new Fournisseur();
                            mContrat.Fournisseur.ID = mClass.Fournisseur.ID;

                            mContrat.ContratPeriodeType = new ContratPeriodeType();
                            mContrat.ContratPeriodeType.ID = (new Parametres(0)).ContratPeriodeTypeFinancement;

                            mContrat.DateContrat = DateTime.Now;
                            mContrat.DateDebut = mClass.DateFinancement;
                            mContrat.DateEcheance = mClass.DateEcheance;
                            mContrat.Tonnage = mClass.Tonnage;
                            mContrat.Prix = mClass.Prix;
                            mContrat.FinancementID = mClass.ID;
                            mContrat.Commentaire = "Generated From Financing";
                            mContrat.UtilisateurCreation = (string)Session["userName"];
                            mContrat.UtilisateurModification = (string)Session["userName"];

                            mContrat.fnUpdate(dTran);
                        }
                        else
                        {
                            result = false;
                            _db.RollBackTransaction(dTran);
                        }

                        _db.CommitTransaction(dTran);

                    }
                    catch (Exception ex)
                    {
                        _db.RollBackTransaction(dTran);
                        X.MessageBox.Show(new MessageBoxConfig
                        {
                            Title = "Financing: Data Validation",
                            Message = ex.Message,
                            Buttons = MessageBox.Button.OK,
                            Icon = MessageBox.Icon.WARNING
                        });
                        return this.Direct();

                    }

                    #endregion
                }
                else
                {
                    if ((formExecMode == EnumsDefinition.eExecMode.Update) && (OldTypeFinancement != NewTypeFinancement) && (OldTypeFinancement == mParm.FinancementTypeAvance))
                    {
                        DataSource _ds = new DataSource();
                        DataTransaction _dtr = new DataTransaction();
                        try
                        {
                            ContratPeriode mOldContrat = new ContratPeriode();
                            mOldContrat.fnGetByFinancing(mClass.ID);
                            if (mOldContrat.ID == null || mOldContrat.ID == Guid.Empty)
                                throw new Exception("SubmitFormMethod : Financing not found.");

                            mOldContrat.UtilisateurModification = (string)Session["userName"];

                            //Update Financement                            
                            _ds = mClass.db();
                            _dtr = _ds.BeginTransaction(System.Data.IsolationLevel.Serializable);
                            result = mClass.fnUpdate(_dtr);
                            if (result)
                            {
                                // Annuler le contrat Periode                                
                                mOldContrat.SetDataSource(_ds);
                                mOldContrat.fnDeActivate(_dtr);
                            }
                            else
                            {
                                result = false;
                                _ds.RollBackTransaction(_dtr);
                            }
                            _ds.CommitTransaction(_dtr);
                        }
                        catch (Exception ex)
                        {
                            result = false;
                            _ds.RollBackTransaction(_dtr);
                        }
                    }
                    else
                    {
                        result = mClass.fnUpdate();
                    }
                }


                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListeFinancement");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionListeFinancement").Select(0);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("Financement_Detail").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Financement : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        //[HttpPost]
        public ActionResult SubmitApprovalFromMethod()
        {
            try
            {
                Financement mClass = new Financement();
                bool result = true;                

                mClass.IsNew = false;

                mClass.fnGet(Guid.Parse(GetFormValue("txtFinancementID")));

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("SubmitApprovalFromMethod : Financing load failed.");

                mClass = MapFormToObject(mClass);
                mClass.Statut = "AP";
                mClass.UtilisateurApprobation = (string)Session["userName"];
                //Quick check

                if (mClass.DateFinancement > mClass.DateEcheance)
                {
                    throw new Exception("SubmitApprovalFromMethod : The financing date can not be higher than due date.");
                    return null;
                }

                result = mClass.fnApprove();            

                if (result)
                {                   
                    X.GetCmp<Window>("Financement_Approuve").Close();

                    string mCampagne = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;
                    string mFournisseur = X.GetCmp<ComboBox>("cmbFournisseur").SelectedItem.Value;
                    string mSite = X.GetCmp<ComboBox>("cmbSite").SelectedItem.Value;
                    OnRefreshForApproval(mCampagne, mFournisseur,mSite);
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Financement : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult LoadListOfFinancing(StoreRequestParameters parameters, string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite, string ItemOption)
        {

            int FournisseurID = GetCriteriaValue(ItemFournisseur);
            int TypeID = GetCriteriaValue(ItemType);
            int siteID = GetCriteriaValue(ItemSite);
            string optionID = string.IsNullOrEmpty(ItemOption) ? "NO" : ItemOption;
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
           
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "{Tous}";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string Status = ItemStatus;
            
            var mListe = (new Financement()).fnSelect(Campagne, FournisseurID, TypeID, StartDate, EndDate, Status, siteID, optionID);
            //var paging = GridStorePaging.SetRangePlants(parameters, mListe);

            //return this.Store(paging);
            return this.Store(mListe);
        }

        public ActionResult LoadListOfFinancingToApprove(StoreRequestParameters parameters, string ItemCampagne, string ItemFournisseur, string ItemSite)
        {

            int FournisseurID = GetCriteriaValue(ItemFournisseur);
            int siteID = GetCriteriaValue(ItemSite);
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;

            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "{Tous}";
            }

            var mListe = (new Financement()).fnSelectForApproval(Campagne, FournisseurID, siteID);
            //var paging = GridStorePaging.SetRangePlants(parameters, mListe);

            return this.Store(mListe);
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelFI");
            mform.ToggleCollapse();            
            return this.Direct();
        }

        public ActionResult OnFilterFAP()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelFAP");

            mform.ToggleCollapse();
            //mform.Collapsed = false;
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite, string ItemOption)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeFinancement");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemCampagne"      ,ItemCampagne),
                                    new Ext.Net.Parameter("ItemType"          ,ItemType),
                                    new Ext.Net.Parameter("ItemSite"          ,ItemSite),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus),
                                    new Ext.Net.Parameter("ItemOption"        ,ItemOption)
                                });

                string title = X.GetCmp<FormPanel>("CriteriaPanelFI").Title;
                title += "Site : " + X.GetCmp<ComboBox>("cmbSite").SelectedItem.Text;
                title += ", Campagne : " + X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                title += ", Fournisseur : " + X.GetCmp<ComboBox>("cmbFournisseur").SelectedItem.Text;

                title += ", Type : " + X.GetCmp<ComboBox>("cmbType").SelectedItem.Text;

                title += ", Du " + X.GetCmp<DateField>("dtpStartDate").RawText.ToString() + " au " + X.GetCmp<DateField>("dtpEndDate").RawText.ToString();

                X.GetCmp<FormPanel>("CriteriaPanelFI").Title = title;

                // collapse criterias areas
                X.GetCmp<FormPanel>("CriteriaPanelFI").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Financement : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
                        
            return this.Direct();
        }

        public ActionResult OnRefreshForApproval(string ItemCampagne, string ItemFournisseur, string ItemSite)
        {
            Store mstore = X.GetCmp<Store>("storeListeFinancingApproval");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemCampagne"      ,ItemCampagne),
                                   new Ext.Net.Parameter("ItemSite"      ,ItemSite)
                                });

          
            // collapse criterias areas
            X.GetCmp<FormPanel>("CriteriaPanelFAP").Collapse(Direction.Top, false);

            return this.Direct();
        }

        private Financement MapFormToObject(Financement mClass)
        {
            mClass.Campagne = X.GetCmp<ComboBox>("_cmbCampagne").Text;

            mClass.Numero = X.GetCmp<Hidden>("hiddenNumero").Text;
            FinancementType mType = new FinancementType();
            mType.ID = int.Parse(GetFormValue("_cmbType"));
            mType.Designation = X.GetCmp<ComboBox>("_cmbType").SelectedItem.Text.ToString();
            mClass.FinancementType = mType;

            Fournisseur mSupplier = new Fournisseur();
            mSupplier.ID = int.Parse(GetFormValue("_cmbFournisseur"));
            mSupplier.Nom = X.GetCmp<ComboBox>("_cmbFournisseur").SelectedItem.Text.ToString();
            mClass.Fournisseur = mSupplier;

            PrelevementMode mPrev = new PrelevementMode();
            mPrev.ID = int.Parse(GetFormValue("cmbPrelevementType"));
            mPrev.Designation = X.GetCmp<ComboBox>("cmbPrelevementType").SelectedItem.Text.ToString();
            mClass.PrelevementMode = mPrev;

            mClass.DateFinancement = DateTime.Parse(X.GetCmp<DateField>("txtDate").RawText.ToString());
            mClass.DateEcheance = DateTime.Parse(X.GetCmp<DateField>("txtDueDate").RawText.ToString());
            if (X.GetCmp<TextField>("txtPrice").Text != string.Empty) mClass.Prix = decimal.Parse(X.GetCmp<TextField>("txtPrice").Text);
            if (X.GetCmp<TextField>("txtAmount").Text != string.Empty) mClass.Montant = decimal.Parse(X.GetCmp<TextField>("txtAmount").Text);
            if (X.GetCmp<TextField>("txtTonnage").Text != string.Empty) mClass.Tonnage = decimal.Parse(X.GetCmp<TextField>("txtTonnage").Text);
            if (X.GetCmp<TextField>("txtTauxRemboursement").Text != string.Empty) mClass.PrelevementTaux = decimal.Parse(X.GetCmp<TextField>("txtTauxRemboursement").Text);

            if (X.GetCmp<TextField>("txtTauxPaiement").Text != string.Empty) mClass.PaymentTaux = decimal.Parse(X.GetCmp<TextField>("txtTauxPaiement").Text);
            if (X.GetCmp<TextField>("txtGrossAmount").Text != string.Empty) mClass.MontantBrut = decimal.Parse(X.GetCmp<TextField>("txtGrossAmount").Text);

            mClass.Commentaire = X.GetCmp<TextField>("txtComment").Text;
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            mClass.Solde = mClass.IsActive ? mClass.Montant : mClass.Solde;

            mClass.Sites = new Site();
            mClass.Sites.ID = int.Parse(GetFormValue("_cmbSite"));
            mClass.Sites.Nom = X.GetCmp<ComboBox>("_cmbSite").SelectedItem.Text.ToString();            

            mClass.Engagement = null;
            if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("cmbEngagement").SelectedItem.Text))
            {
                mClass.Engagement = new Engagement();
                mClass.Engagement.ID = Guid.Parse(GetFormValue("cmbEngagement"));
                mClass.Engagement.Numero = X.GetCmp<ComboBox>("cmbEngagement").SelectedItem.Text.ToString();
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
            X.GetCmp<RowSelectionModel>("rowSelectionListeFinancement").DeselectAll();
        }

        public ActionResult OnPrintAgreement(string IdAvance)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Agreement{0}', '{1}/Financement/ViewAgreementReport?id={0}', this, 'Protocol Accord','')", IdAvance, BaseUrl));
        }

        public ActionResult OnPrintFinancingReport(string ReportType, string ItemPeriodStart = "", string ItemPeriodEnd = "")
        {            
            FinancementViewModel mclass = new FinancementViewModel();
            try
            {
                if (string.IsNullOrEmpty(ItemPeriodStart)) ItemPeriodStart = DateTime.Now.ToShortDateString();
                if (string.IsNullOrEmpty(ItemPeriodEnd)) ItemPeriodEnd = DateTime.Now.ToShortDateString();

                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);
                mclass._Financement = new Financement();
                mclass._Financement.DateFinancement = datedebut;
                mclass._Financement.DateEcheance = datedfin;
                mclass._Financement.Sites = new Site();
                //mclass._Financement.Campagne = new Campagne();                        

                //var mParam = (new Parametres()).fnSelect();
                Parametres parametre = new Parametres(0);
                string UserName = (string)Session["userName"];
                Site mSiteParDefaut = new Site();
                bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

                if (result)
                    mclass._Financement.Sites.ID = mSiteParDefaut.ID;

                mclass._Financement.Campagne = parametre.Campagne;
                switch (ReportType)
                {
                    case FinancementReportType.Balance:
                        mclass._ReportType = FinancementReportType.Balance;
                        ViewData["ReportTitle"] = "Situation Des Financements - Etat";
                        ViewData["CanFilterByPeriod"] = true;
                        break;
                    case FinancementReportType.Execution:
                        mclass._ReportType = FinancementReportType.Execution;
                        ViewData["ReportTitle"] = "Execution Des Financements - Etat";
                        ViewData["CanFilterByPeriod"] = true;
                        break;
                    case FinancementReportType.Exposure:
                        mclass._ReportType = FinancementReportType.Exposure;
                        ViewData["ReportTitle"] = "Risque Sur financement - Etat";
                        ViewData["CanFilterByPeriod"] = false;
                        break;

                }
            }
            catch (Exception)
            {

                throw;
            }
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintFinancingReport", Model = mclass };
        }

        public ActionResult GenerateReport(string TypeReport)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            Financement ficlass = new Financement();

            //contratperiode.Campagne = new Campagne();
            ficlass.Campagne = GetFormValue("FIPcropYearForReport");

            ficlass.Fournisseur = new Fournisseur();
            ficlass.Fournisseur.ID = int.Parse(GetFormValue("FIfournisseurForReport"));
            ficlass.Fournisseur.Nom = X.GetCmp<ComboBox>("FIfournisseurForReport").SelectedItem.Text;

            ficlass.FinancementType = new FinancementType();
            ficlass.FinancementType.ID = int.Parse(GetFormValue("FITypeFinancementForReport"));
            ficlass.FinancementType.Designation = X.GetCmp<ComboBox>("FITypeFinancementForReport").SelectedItem.Text;

            ficlass.DateFinancement = DateTime.Parse(X.GetCmp<DateField>("FIstartDateForReport").RawText.ToString());
            ficlass.DateEcheance = DateTime.Parse(X.GetCmp<DateField>("FIdueDateForReport").RawText.ToString());
            ficlass.Statut = X.GetCmp<ComboBox>("FIOptionReport").SelectedItem.Value.ToString();

            ficlass.Sites = new Site();
            ficlass.Sites.ID = int.Parse(GetFormValue("FISiteForReport"));
            ficlass.Sites.Nom = X.GetCmp<ComboBox>("FISiteForReport").SelectedItem.Text;

            X.Js.Call("CloseWindow");

            string param = JSON.Serialize(ficlass);
            Session["ParamReport"] = param;
            string ReportTitle = string.Empty;
            switch (TypeReport)
            {
                case FinancementReportType.Balance:
                    ReportTitle = "Situation Des Financements";
                    break;
                case FinancementReportType.Execution:
                    ReportTitle = "Execution Des Financements";
                    break;
                case FinancementReportType.Exposure:
                    ReportTitle = "Risque Sur financement";
                    break;
                case FinancementReportType.FinancialStatus:
                    ReportTitle = "Position Financière";
                    break;
                case FinancementReportType.FinancialStatusCrop:
                    ReportTitle = "Position Financière";
                    break;
            }

            //HttpUtility.JavaScriptStringEncode(param)
            return JavaScript(String.Format("CloseWindow(), addTab(window.parent.Ext.getCmp('tabCenter'), 'FinancingReport{0}', '{1}/Financement/ViewReport?TypeReport={2}', this, '{3}','')", Guid.NewGuid(), BaseUrl, TypeReport, ReportTitle));            
        }

        public ActionResult ViewAgreementReport(string id)
        {
            rptFinancingAgreement report = new rptFinancingAgreement();
            Financement mFinancement = new Financement(Guid.Parse(id));

            report.DataSource = DevExpressReportDs.SetDataSource(report);
            report.Parameters["FinancementID"].Value = id;
            report.Parameters["MontantEnLettre"].Value = MoneySpeller.ToLettres(Int32.Parse(mFinancement.Montant.ToString()));


            ViewData["Report"] = report;

            return View();
        }


        public ActionResult ViewReport(string TypeReport)
        {
            XtraReport report = new XtraReport();
            switch (TypeReport)
            {
                case FinancementReportType.Balance:
                    report = new rptFinancingBalance() as XtraReport;
                    break;
                case FinancementReportType.Execution:
                    report = new rptFinancingExecution() as XtraReport;
                    break;
                case FinancementReportType.Exposure:
                    report = new rptFinancingExposure() as XtraReport;
                    break;
                case FinancementReportType.FinancialStatus:
                    report = new rptSupplierFinancialStatus() as XtraReport;
                    break;
                case FinancementReportType.FinancialStatusCrop:
                    report = new rptSupplierFinancialStatusCrop() as XtraReport;
                    break;
            }           
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            Financement financement = JSON.Deserialize<Financement>((string)Session["ParamReport"], new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            if (TypeReport == FinancementReportType.Exposure)
            {
                report.Parameters["campagneID"].Value = financement.Campagne;
                report.Parameters["fournisseurID"].Value = financement.Fournisseur.ID;
                report.Parameters["NomFournisseur"].Value = financement.Fournisseur.Nom;
                report.Parameters["siteID"].Value = financement.Sites.ID;
                report.Parameters["siteNom"].Value = financement.Sites.Nom;

                ViewData["Report"] = report;

                return View();
            }

            report.Parameters["campagneID"].Value = financement.Campagne;
            report.Parameters["fournisseurID"].Value = financement.Fournisseur.ID;


            if (TypeReport != FinancementReportType.FinancialStatus && TypeReport != FinancementReportType.FinancialStatusCrop)
            {
                report.Parameters["FinancementTypeID"].Value = financement.FinancementType.ID;
                report.Parameters["DateDebut"].Value = financement.DateFinancement;
                report.Parameters["DateFin"].Value = financement.DateEcheance;
                report.Parameters["Status"].Value = financement.Statut;

                //Afficher Les criteres sur l'etat
                report.Parameters["NomFournisseur"].Value = financement.Fournisseur.Nom;
                //report.Parameters["fournisseurNom"].Value = contrat.Statut.Nom;
                report.Parameters["FinancementTypeNom"].Value = financement.FinancementType.Designation;
                report.Parameters["OptionLibelle"].Value = financement.Statut;
                report.Parameters["siteID"].Value = financement.Sites.ID;
                report.Parameters["siteNom"].Value = financement.Sites.Nom;
                ViewData["Report"] = report;
                return View();
            }
            else
            {
                report.Parameters["NomFournisseur"].Value = financement.Fournisseur.Nom;
                ViewData["Report"] = report;
                return View();
            }

            
            //ViewData["Report"] = report;

            //return View();
        }

        public ActionResult OnDisplayFinancingList(string ItemPeriodStart = "", string ItemPeriodEnd = "")
        {
            Financement mFinancement = new Financement();
            try
            {
                if (string.IsNullOrEmpty(ItemPeriodStart)) ItemPeriodStart = DateTime.Now.ToShortDateString();
                if (string.IsNullOrEmpty(ItemPeriodEnd)) ItemPeriodEnd = DateTime.Now.ToShortDateString();

                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                mFinancement.DateFinancement = datedebut;
                mFinancement.DateEcheance = datedfin;

                ViewData["Titre"] = "Liste des Financements";
                ViewData["actionToDo"] = "OnPrintFinancingList";
                ViewData["ControllerName"] = "Financement";
                
                mFinancement.Sites = new Site();
                Site mSiteParDefaut = new Site();
                string UserName = (string)Session["userName"];
                bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

                if (result)
                    mFinancement.Sites.ID = mSiteParDefaut.ID;
            }
            catch (Exception)
            {

                throw;
            }
            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmCriteriaForFinancing", Model = mFinancement, ViewData = ViewData };
        }

        public ActionResult OnPrintFinancingList(string cropyear, string fournisseur, string fournisseurText, string typefinancement, string typefinancementText, string startDate, string endDate, string statut, string statutText, string siteID, string SiteNom)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                //Session["paramCropYear"] = cropYear;                
                Session["paramCampagne"] = cropyear;
                Session["paramFournisseur"] = fournisseur;
                Session["paramFournisseurText"] = fournisseurText;
                Session["paramTypeOfFinancing"] = typefinancement;
                Session["paramTypeOfFinancingText"] = typefinancementText;
                Session["paramStatut"] = statut;
                Session["paramStatutText"] = statutText;

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                Session["paramSite"] = siteID;
                Session["paramSiteText"] = SiteNom;

                Session["paramOption"] = X.GetCmp<ComboBox>("cmbDetOption").SelectedItem.Value.ToString();
                Session["paramOptionText"] = X.GetCmp<ComboBox>("cmbDetOption").SelectedItem.Text;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Financement/ViewReportResult', this, 'Liste Des Financements',''),App.frmCriteriaForFinancing.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Financement : Data Validation",
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

            rptFinancingList report = new rptFinancingList();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramCampagneText"].Value = Session["paramCampagne"];
            report.Parameters["paramCampagne"].Value = Session["paramCampagne"];
            //if (!string.IsNullOrEmpty(Session["paramCampagne"].ToString()) && Session["paramCampagne"].ToString() == "{Tous}")
            //    report.Parameters["paramCampagne"].Value = string.Empty;
            //else
            //    report.Parameters["paramCampagne"].Value = Session["paramCampagne"];

            report.Parameters["paramFournisseur"].Value = Session["paramFournisseur"];
            report.Parameters["paramFournisseurText"].Value = Session["paramFournisseurText"];

            report.Parameters["paramTypeOfFinancing"].Value = Session["paramTypeOfFinancing"];
            report.Parameters["paramTypeOfFinancingText"].Value = Session["paramTypeOfFinancingText"];

            report.Parameters["paramStatut"].Value = Session["paramStatut"];
            report.Parameters["paramStatutText"].Value = Session["paramStatutText"];

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];
            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            report.Parameters["paramSite"].Value = Session["paramSite"];
            report.Parameters["siteNom"].Value = Session["paramSiteText"];

            report.Parameters["paramOption"].Value = Session["paramOption"];
            report.Parameters["paramOptionText"].Value = Session["paramOptionText"];
            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

        public ActionResult OnDisplayFinancingApvlList()
        {
            ViewData["Titre"] = "Approbation - Financement";
            ViewData["actionToDo"] = "OnPrintFinancingApvlList";
            ViewData["ControllerName"] = "Financement";
            Financement mFinancement = new Financement();
            mFinancement.Sites = new Site();
            Site mSiteParDefaut = new Site();
            string UserName = (string)Session["userName"];
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmCriteriaForFinApproval",Model = mFinancement, ViewData = ViewData };
        }

        public ActionResult OnPrintFinancingApvlList(string cropyear, string fournisseur, string fournisseurText, string siteID, string SiteNom)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
               
                //Session["paramCropYear"] = cropYear;                
                Session["paramCampagne"] = cropyear;
                Session["paramFournisseur"] = fournisseur;
                Session["paramFournisseurText"] = fournisseurText;
                Session["paramSite"] = siteID;
                Session["paramSiteNom"] = SiteNom;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Financement/ViewReportApvlResult', this, 'Approbation - Financement',''),App.frmCriteriaForFinApproval.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Financing - Approval : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
        }

        public ActionResult ViewReportApvlResult()
        {
            //XtraReport report = null;

            rptFinancingApprovalList report = new rptFinancingApprovalList();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramCampagneText"].Value = Session["paramCampagne"];
            if (!string.IsNullOrEmpty(Session["paramCampagne"].ToString()) && Session["paramCampagne"].ToString() == "{Tous}")
                report.Parameters["paramCampagne"].Value = string.Empty;
            else
                report.Parameters["paramCampagne"].Value = Session["paramCampagne"];

            report.Parameters["paramFournisseur"].Value = Session["paramFournisseur"];
            report.Parameters["paramFournisseurText"].Value = Session["paramFournisseurText"];
            report.Parameters["paramSite"].Value = Session["paramSite"];
            report.Parameters["siteNom"].Value = Session["paramSiteNom"];
            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

        public ActionResult LoadListOfEngagement(int? ItemFournisseur, string ItemExecMode)
        {
            List<DataPersist> myList = new List<DataPersist>();
            if (!ItemFournisseur.HasValue)
                return this.Store(myList);

            if (string.IsNullOrEmpty(ItemExecMode))
                return this.Store(myList);

            //int fournisseurID = int.Parse(ItemFournisseur);
            DateTime? DateFin;
            Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(ItemExecMode);

            if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                DateFin = DateTime.Now;
            else
                DateFin = (DateTime?)null;
            
            myList = new Engagement().fnSelectForFinancing((int)ItemFournisseur, DateFin);
            Engagement mClass = new Engagement();

            if (myList.Count > 0)
                mClass = myList[0] as Engagement;

            return this.Store(myList);
        }
    }
}
