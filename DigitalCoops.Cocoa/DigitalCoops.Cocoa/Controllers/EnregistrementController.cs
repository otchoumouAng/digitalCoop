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
using Tms.Classes.Shared.Sales;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class EnregistrementController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        // GET: Sales Registration
        public ActionResult Index()
        {
            var mParam = (new Parametres()).fnSelect();

            Parametres mclass = new Parametres();
            mclass = mParam[0] as Parametres;

            Campagne mCampagne = new Campagne();

            X.GetCmp<ComboBox>("cmbCropYear").SetValue(mclass.Campagne);

            string StartDate = "01/01/" + DateTime.Now.Year.ToString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("dtpStartDate").RawText = StartDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Exportateur = "{Tous}";

            X.GetCmp<FormPanel>("EnregistrementCriteriaPanel").SetTitle("Campagne : " + mclass.Campagne + " | Exportateur : " + Exportateur + " | Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            if (HasAccess.fnGetUserAccessStatus("{43C7B828-B1B2-4C31-A03C-8C42ECC8516B}", UserName) == false)
                X.GetCmp<MenuItem>("btnNew").Disable();
            else
                X.GetCmp<MenuItem>("btnNew").Enable();

            if (HasAccess.fnGetUserAccessStatus("{4368DB2E-87F9-4430-94D5-2BC88F830337}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExport").Disable();
            else
                X.GetCmp<MenuItem>("mnuExport").Enable();

            if (HasAccess.fnGetUserAccessStatus("{19E723CF-9AF7-4702-AB9E-9B74E03C7E91}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintBalance").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintBalance").Enable();

            if (HasAccess.fnGetUserAccessStatus("{7604E15E-8D6A-4815-A3D1-9E029239A985}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintExecution").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintExecution").Enable();

            if (HasAccess.fnGetUserAccessStatus("{45D8535D-0796-4E73-B0C2-A5BD4F95CFD4}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintHistory").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintHistory").Enable();

            X.GetCmp<Hidden>("enhiddenPermDesactive").SetValue(HasAccess.fnGetUserAccessStatus("{EB1CA593-550B-47C5-8C5B-88E8722FAEFE}", UserName));
            X.GetCmp<Hidden>("enhiddenPermActive").SetValue(HasAccess.fnGetUserAccessStatus("{2E0F53AB-80AB-47BE-B31B-BB49DE629B1A}", UserName));
            X.GetCmp<Hidden>("enhiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{4368DB2E-87F9-4430-94D5-2BC88F830337}", UserName));
            X.GetCmp<Hidden>("enhiddenPermPrintBalance").SetValue(HasAccess.fnGetUserAccessStatus("{19E723CF-9AF7-4702-AB9E-9B74E03C7E91}", UserName));
            X.GetCmp<Hidden>("enhiddenPermPrintExecution").SetValue(HasAccess.fnGetUserAccessStatus("{7604E15E-8D6A-4815-A3D1-9E029239A985}", UserName));
            //X.GetCmp<Hidden>("enhiddenPermPrintAgreement").SetValue(HasAccess.fnGetUserAccessStatus("{9549E7D7-D5BC-4CEB-9723-89581EE49234}", UserName));
            X.GetCmp<Hidden>("enhiddenPermPrintHistory").SetValue(HasAccess.fnGetUserAccessStatus("{45D8535D-0796-4E73-B0C2-A5BD4F95CFD4}", UserName));
            X.GetCmp<Hidden>("enhiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{311970B4-CD0A-41E4-BEA6-44E620FBF9AA}", UserName));
            X.GetCmp<Hidden>("enhiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{43C7B828-B1B2-4C31-A03C-8C42ECC8516B}", UserName));
            X.GetCmp<Hidden>("enhiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{30501311-CF57-44D8-95D1-4C3E587CD18C}", UserName));
            #endregion
            return View();
        }
        public ActionResult LoadListOfRegistration(StoreRequestParameters parameters, string ItemCropYear, string ItemExporter, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemOption)
        {

            int ExportateurID = GetCriteriaValue(ItemExporter);
            string Campagne = string.IsNullOrEmpty(ItemCropYear) ? "" : ItemCropYear;
            string option = string.IsNullOrEmpty(ItemOption) ? "NA" : ItemOption;

            if (!string.IsNullOrEmpty(ItemCropYear) && ItemCropYear.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            int Status = int.Parse(ItemStatus);

            var mListe = (new Enregistrement()).fnSelect(Campagne, ExportateurID, StartDate, EndDate, Status, option);

            //return this.Store(paging);
            return this.Store(mListe);
        }

        public ActionResult LoadDetailEnregistrement(string ItemEnregistrement)
        {
            Guid EnregistrementID = Guid.Empty;

            bool IsGuid = Guid.TryParse(ItemEnregistrement, out EnregistrementID);
            var mListe = new List<DataPersist>();

            if (IsGuid)
                mListe = (new EnregistrementDetail()).fnSelect(EnregistrementID);
            else
                mListe = null;
                  
            return this.Store(mListe);
        }

        public ActionResult LoadRegistrationCampagneActive(string ItemCampagne, string ItemExportateur)
        {

            List<DataPersist> liste = new List<DataPersist>();
            if (string.IsNullOrEmpty(ItemExportateur))
            {
                liste = new Enregistrement().fnSelectForShipment(ItemCampagne, -1, null, null, 0);
            }
            else
            {
                int exporter = int.Parse(ItemExportateur);
                liste = new Enregistrement().fnSelectForShipment(ItemCampagne, exporter, null, null, 0);

            }

            return this.Store(liste);
        }

        public ActionResult onCreate()
        {
            Parametres mParam = new Parametres(0);
            EnregistrementViewModel viewmodel = new EnregistrementViewModel();

            viewmodel._Enregistrement = new Enregistrement();
            viewmodel._ModeEnregistrement = mParam.DefaultModeEnregistrementVente;
            viewmodel._DefaultCampagne = mParam.Campagne;
            viewmodel._DefaultExportateur = mParam.Exportateur.ID;            
            viewmodel._PrefixeContratExport = mParam.PrefixeContratExportEnregistrement;

            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
           
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Enregistrement_Detail", Model = viewmodel };
        }

        public ActionResult onAddDetail()
        {            
            EnregistrementDetailViewModel viewmodel = new EnregistrementDetailViewModel();

            viewmodel._EnregistrementDetail = new EnregistrementDetail();            
            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDetailEnregistrement", Model = viewmodel };
        }

        public ActionResult onEditDetail(string ItemSelected = "")
        {

            EnregistrementDetailViewModel viewmodel = new EnregistrementDetailViewModel();

            viewmodel._EnregistrementDetail = new EnregistrementDetail();

            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDetailEnregistrement", Model = viewmodel };
        }

        public ActionResult onEdit(string ItemSelected)
        {
            Parametres mParam = new Parametres(0);
            Enregistrement mclass = JSON.Deserialize<Enregistrement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            EnregistrementViewModel viewModel = new EnregistrementViewModel();

            viewModel._ModeEnregistrement = mParam.DefaultModeEnregistrementVente;
            viewModel._Enregistrement = mclass;
            viewModel._DefaultCampagne = mParam.Campagne;
            viewModel._DefaultExportateur = mParam.Exportateur.ID;            
            viewModel._PrefixeContratExport = "";
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Enregistrement_Detail", Model = viewModel };
        }

        public ActionResult onConsult(string ItemSelected)
        {

            Enregistrement mclass = JSON.Deserialize<Enregistrement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            EnregistrementViewModel viewModel = new EnregistrementViewModel();

            viewModel._Enregistrement = mclass;
            viewModel._ModeEnregistrement = 1;            
            viewModel._PrefixeContratExport = "";
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Enregistrement_Detail", Model = viewModel };
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
               Enregistrement mclass = JSON.Deserialize<Enregistrement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = mclass.fnGet(mclass.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Registration loading failed.");
                
                mclass.UtilisateurModification = (string)Session["userName"];

                if (mclass.Desactive)
                    result = mclass.fnActivate();
                else
                    result = mclass.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Sales Registration, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeEnregistrement");

                    ModelProxy mProxy = mstore.GetById(mclass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mclass);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sales registration : OnActivateDeactivate",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult onRemoveDetail(string ItemSelected)
        {
            try
            {
                EnregistrementDetail mDetail = JSON.Deserialize<EnregistrementDetail>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                if (mDetail.ID != Guid.Empty)
                {
                    Store mstore = X.GetCmp<Store>("storeDetailEnregistrement");

                    if (mDetail.IsNew)
                    {
                        ModelProxy _proxy = mstore.GetById(mDetail.ID);
                        _proxy.Drop();
                        return this.Direct();
                    }
                    else
                    {                        
                        mDetail.fnGet(mDetail.ID);
                        if (mDetail == null || mDetail.ID == Guid.Empty)
                            throw new Exception("RemoveDetail : Retirer Detail failed.");

                        mDetail.UtilisateurModification = (string)Session["userName"];

                        bool result = mDetail.fnRemove();

                        if (result)
                        {
                            ModelProxy mProxy = mstore.GetById(mDetail.ID);
                            mProxy.Drop();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "General Weighing - : Retirer Pallets Weight",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("EnregistrementCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemCropYear, string ItemExporter, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemOption)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeEnregistrement");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemExporter"   ,ItemExporter),
                                    new Ext.Net.Parameter("ItemCropYear"      ,ItemCropYear),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus),
                                    new Ext.Net.Parameter("ItemOption"        ,ItemOption)
                                });



                // collapse criterias areas
                X.GetCmp<FormPanel>("EnregistrementCriteriaPanel").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sales Registration : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        #region Old Submit
        //public ActionResult SubmitFormMethod()
        //{           
        //    try
        //    {
        //        Enregistrement mClass = new Enregistrement();

        //        Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

        //        if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
        //            mClass.IsNew = true;
        //        else
        //        {
        //            mClass.IsNew = false;

        //            mClass.fnGet(Guid.Parse(GetFormValue("txtEnregistrementID")));

        //            if (mClass == null || mClass.ID == Guid.Empty)
        //                throw new Exception("SubmitFormMethod : Registration load failed.");

        //            if ( mClass.Desactive == true)
        //                throw new Exception("SubmitFormMethod : Registration is disabled");
        //        }

        //        bool Result = true;

                
        //        mClass = MapFormToObject(mClass);
        //        Result = mClass.fnUpdate();
        //        if(Result)
        //        {
        //            Store mStore = X.GetCmp<Store>("storeListeEnregistrement");

        //            Enregistrement mEnr = new Enregistrement();
        //            mEnr.fnGet(mClass.ID);

        //            if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
        //            {
        //                mStore.Insert(0, mEnr);
        //                X.GetCmp<RowSelectionModel>("rowSelectionListeEnregistrement").Select(0);
        //            }
        //            else
        //            {
        //                ModelProxy mProxy = mStore.GetById(mEnr.ID);

        //                mProxy.BeginEdit();

        //                mProxy.Set(mEnr);

        //                mProxy.Commit();

        //                mProxy.EndEdit();


        //            }

        //            X.GetCmp<Window>("Enregistrement_Detail").Close();
        //        }

               


               
                

        //    }
        //    catch (Exception ex)
        //    {
                
        //        X.MessageBox.Show(new MessageBoxConfig
        //        {
        //            Title = "Sales Registration : Update",
        //            Message = ex.Message,
        //            Buttons = MessageBox.Button.OK,
        //            Icon = MessageBox.Icon.WARNING
        //        });
        //    }

        //    return this.Direct();
        //}
        #endregion

        public ActionResult SubmitFormMethod(string storerows = "")
        {
            DataSource _db = new DataSource();
            DataTransaction mTran = new DataTransaction();
            try
            {
                Enregistrement mClass = new Enregistrement();
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("txtEnregistrementID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Registration load failed.");

                    if (mClass.Desactive == true)
                        throw new Exception("SubmitFormMethod : Registration is disabled");
                }

                bool ResultEnregristrement = true;
                bool ResultDetailEnregristrement = true;

                mClass = MapFormToObject(mClass);

                _db = mClass.db();
                mTran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                ResultEnregristrement = mClass.fnUpdate(mTran);

                if (ResultEnregristrement)
                {
                    EnregistrementDetail mDetail = new EnregistrementDetail();
                    List<EnregistrementDetail> ItemsDetails = JSON.Deserialize<List<EnregistrementDetail>>(storerows, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                    if (ItemsDetails.Count > 0)
                    {
                        foreach (var det in ItemsDetails.Where(d => d.IsNew))
                        {
                            mDetail = new EnregistrementDetail();
                            mDetail.Enregistrement = new Enregistrement();
                            mDetail.SetDataSource(_db);                            
                            mDetail.Enregistrement.ID = mClass.ID;
                            mDetail.Tonnage = det.Tonnage;
                            mDetail.Montant = det.Montant;
                            mDetail.Prix = det.Prix;
                            mDetail.Numero = det.Numero;
                            
                            mDetail.UtilisateurCreation = (string)Session["userName"];
                            mDetail.UtilisateurModification = (string)Session["userName"];

                            ResultDetailEnregristrement = mDetail.fnUpdate(mTran);
                            //mDetail.IsNew = false;
                            if (!ResultDetailEnregristrement)
                            {
                                _db.RollBackTransaction(mTran);
                                ResultEnregristrement = false;
                                break;
                            }
                        }                                                
                    }

                    if (ResultEnregristrement)
                    {
                        _db.CommitTransaction(mTran);

                        Store mStore = X.GetCmp<Store>("storeListeEnregistrement");                        
                        //mEnr.fnGet(mClass.ID);
                        if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                        {
                            mStore.Insert(0, mClass);
                            X.GetCmp<RowSelectionModel>("rowSelectionListeEnregistrement").Select(0);
                        }
                        else
                        {
                            ModelProxy mProxy = mStore.GetById(mClass.ID);
                            mProxy.BeginEdit();
                            mProxy.Set(mClass);
                            mProxy.Commit();
                            mProxy.EndEdit();
                        }
                    }
                    X.GetCmp<Window>("Enregistrement_Detail").Close();
                }
                else
                {
                    _db.RollBackTransaction(mTran);
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Registration : Data Validation",
                        Message = "Error ! Please Retry",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                }      

            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mTran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sales Registration : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult OnPrintRegistration(string typeReport)
        {
            ViewData["typeReport"] = typeReport;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintEnregistrementReport", ViewData = ViewData };

        }

        public ActionResult PrintRegistration(string TypeReport)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;
                string type = "";
                if (TypeReport == "Ba")
                {
                    report = new rptEnregistrementBalance() as XtraReport;
                    type = "Registration - Balance";
                }
                if (TypeReport == "Ex")
                {
                    report = new rptEnregistrementExecution() as XtraReport;
                    type = "Registration - Execution";

                }
                if (TypeReport == "Hi")
                {
                    report = new rptEnregistrementHistrory() as XtraReport;

                    type = "Registration - History";

                }

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["paramCampagne"].Value = X.GetCmp<ComboBox>("EnrcropYearForReport").SelectedItem.Text;

                report.Parameters["paramExportateurID"].Value = X.GetCmp<ComboBox>("EnrexportateurForReport").SelectedItem.Value.ToString();
                report.Parameters["paramExportateur"].Value = X.GetCmp<ComboBox>("EnrexportateurForReport").SelectedItem.Text;
                if (TypeReport == "Hi" || TypeReport == "Ex")
                {
                    report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("EnrstartDateForReport").RawText.ToString());
                    report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("EnrdueDateForReport").RawText.ToString());

                }

                if (TypeReport == "Hi")
                {
                    report.Parameters["paramStatutID"].Value = X.GetCmp<ComboBox>("EnrStatus").SelectedItem.Value.ToString();
                    report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("EnrStatus").SelectedItem.Text;

                    report.Parameters["paramOption"].Value = X.GetCmp<ComboBox>("cmbOptionForReport").SelectedItem.Value.ToString();
                    report.Parameters["paramOptionText"].Value = X.GetCmp<ComboBox>("cmbOptionForReport").SelectedItem.Text;
                }
                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Enregistrement/ViewList', this, '{2}',''),App.FormPrintEnregistrementReport.doClose()", Guid.NewGuid(), BaseUrl, type));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sales Registration : Data Validation",
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

        public ActionResult SubmitDetail(string ItemSelected = "")
        {
            EnregistrementDetail mDetail = new EnregistrementDetail();           
            mDetail.ID = Guid.NewGuid();

            if (X.GetCmp<TextField>("txtDetNumber").Text != string.Empty) mDetail.Numero = X.GetCmp<TextField>("txtDetNumber").Text;
            if (X.GetCmp<TextField>("txtDetTonnage").Text != string.Empty) mDetail.Tonnage = decimal.Parse(X.GetCmp<TextField>("txtDetTonnage").Text);
            if (X.GetCmp<TextField>("txtDetPrice").Text != string.Empty) mDetail.Prix = decimal.Parse(X.GetCmp<TextField>("txtDetPrice").Text);
            if (X.GetCmp<Hidden>("txtDetAmount").Text != string.Empty) mDetail.Montant = decimal.Parse(X.GetCmp<Hidden>("txtDetAmount").Text);
            mDetail.IsNew = true;            

            Store mstore = X.GetCmp<Store>("storeDetailEnregistrement");
            mstore.Add(mDetail);
            X.GetCmp<RowSelectionModel>("rowDetailEnregistrement").Select(0);

            X.GetCmp<Window>("FormDetailEnregistrement").Close();

            return this.Direct();
        }


        #region "Methods"
        private Enregistrement MapFormToObject(Enregistrement mClass)
        {
            try
            {
                mClass.ID = Guid.Parse(X.GetCmp<Hidden>("txtEnregistrementID").Text);
                mClass.Numero = X.GetCmp<TextField>("txtNumber").Text;
                mClass.Campagne = X.GetCmp<ComboBox>("_cmbCampagne").Text;

                Exportateur mExportateur = new Exportateur();
                mExportateur.ID = int.Parse(X.GetCmp<ComboBox>("_cmbExportateur").Text);
                mExportateur.Nom = X.GetCmp<ComboBox>("_cmbExportateur").SelectedItem.Text.ToString();
                mClass.Exportateur = mExportateur;

                EnregistrementMode mMode = new EnregistrementMode();
                mMode.ID = int.Parse(X.GetCmp<ComboBox>("_cmbModeEnregistrement").Text);
                mMode.Designation = X.GetCmp<ComboBox>("_cmbModeEnregistrement").SelectedItem.Text.ToString();
                mClass.EnregistrementMode = mMode;

                mClass.NumeroONCC = X.GetCmp<TextField>("txtONCC").Text;
                mClass.NumeroGuichet = X.GetCmp<TextField>("txtNumeroGuichet").Text;
                mClass.ContratExport = X.GetCmp<TextField>("txtContractExport").Text;

                mClass.Date = DateTime.Parse(X.GetCmp<DateField>("txtDate").RawText.ToString());
                mClass.Periode = DateTime.Parse(X.GetCmp<DateField>("txtPeriod").RawText.ToString());

                if (X.GetCmp<TextField>("txtPrice").Text != string.Empty) mClass.Prix = decimal.Parse(X.GetCmp<TextField>("txtPrice").Text);
                if (X.GetCmp<TextField>("txtTonnage").Text != string.Empty) mClass.Tonnage = decimal.Parse(X.GetCmp<TextField>("txtTonnage").Text);
                if (X.GetCmp<TextField>("txtAmount").Text != string.Empty) mClass.Montant = decimal.Parse(X.GetCmp<TextField>("txtAmount").Text);

                mClass.Terme = X.GetCmp<TextArea>("txtTerms").Text;
                mClass.Description = X.GetCmp<TextArea>("txtDescription").Text;

                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];
               
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sales Registration : MapFormToObject",
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
            X.GetCmp<RowSelectionModel>("rowSelectionListeEnregistrement").DeselectAll();
        }

        #endregion
    }
}