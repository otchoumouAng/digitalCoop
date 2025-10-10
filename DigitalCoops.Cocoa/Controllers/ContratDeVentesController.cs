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
    public class ContratDeVentesController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        const string Balance = "Balance";
        const string Execution = "Execution";

        // GET: ContratDeVentes
        public ActionResult Index()
        {            
            Parametres mParam = new Parametres(0);            

            Campagne mCampagne = new Campagne();
            bool result = mCampagne.fnGet(mParam.Campagne);
            X.GetCmp<ComboBox>("cmbCropYear").SetValue(mParam.Campagne);
            string StartDate = string.Empty;
            string EndDate = string.Empty;
            if (result)
            {
                StartDate = mCampagne.DateDebut.Value.ToShortDateString();
                EndDate = mCampagne.DateFin.Value.ToShortDateString();
            }
            else
            {
                StartDate = DateTime.Now.AddDays(-120).ToShortDateString();
                EndDate = DateTime.Now.ToShortDateString();
            }           

            X.GetCmp<DateField>("dtpStartDate").RawText = StartDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Exportateur = "{Tous}";
            string Certification = "{Tous}";

            X.GetCmp<FormPanel>("ContratDeVentesCriteriaPanel").SetTitle("Campagne : " + mParam.Campagne + " | Exportateur : " + Exportateur + " | Certification : " + Certification + " | Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            if (HasAccess.fnGetUserAccessStatus("{F6179E68-3B70-4AFB-AFD5-2DB779CB0A4C}", UserName) == false)
                X.GetCmp<MenuItem>("btnNew").Disable();
            else
                X.GetCmp<MenuItem>("btnNew").Enable();

            if (HasAccess.fnGetUserAccessStatus("{2F74D43F-DC27-426F-972F-AAEB21C7DAA0}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExport").Disable();
            else
                X.GetCmp<MenuItem>("mnuExport").Enable();

            if (HasAccess.fnGetUserAccessStatus("{FE01FD4C-CD45-40E2-8F6C-4469B87B0BF1}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintBalance").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintBalance").Enable();

            if (HasAccess.fnGetUserAccessStatus("{CB48D363-347E-41B8-A1A5-B84278D8E0AC}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintExecution").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintExecution").Enable();



            if (HasAccess.fnGetUserAccessStatus("{907994AC-9A65-47AB-8576-9CE6CCF00AB7}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintHistory").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintHistory").Enable();            

            X.GetCmp<Hidden>("cvhiddenPermApprove").SetValue(HasAccess.fnGetUserAccessStatus("{DCA7163B-CC66-4E49-86AA-5DF82EF0B89A}", UserName));
            X.GetCmp<Hidden>("cvhiddenPermCancel").SetValue(HasAccess.fnGetUserAccessStatus("{4B385D6D-1D1C-4967-B6F0-C97B7F44244E}", UserName));
            X.GetCmp<Hidden>("cvhiddenPermClose").SetValue(HasAccess.fnGetUserAccessStatus("{A4F8E112-5723-4CA7-BF76-BCAE964B416F}", UserName));
            X.GetCmp<Hidden>("cvhiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{2F74D43F-DC27-426F-972F-AAEB21C7DAA0}", UserName));
            X.GetCmp<Hidden>("cvhiddenPermPrintBalance").SetValue(HasAccess.fnGetUserAccessStatus("{FE01FD4C-CD45-40E2-8F6C-4469B87B0BF1}", UserName));
            X.GetCmp<Hidden>("cvhiddenPermPrintExecution").SetValue(HasAccess.fnGetUserAccessStatus("{CB48D363-347E-41B8-A1A5-B84278D8E0AC}", UserName));
            X.GetCmp<Hidden>("cvhiddenPermPrintAgreement").SetValue(HasAccess.fnGetUserAccessStatus("{D6AE2C00-A66C-43C4-BFB6-3DF053F1920F}", UserName));
            X.GetCmp<Hidden>("cvhiddenPermPrintHistory").SetValue(HasAccess.fnGetUserAccessStatus("{907994AC-9A65-47AB-8576-9CE6CCF00AB7}", UserName));
            X.GetCmp<Hidden>("cvhiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{EBE73913-4ADE-4FBA-B285-B871F017E22D}", UserName));
            X.GetCmp<Hidden>("cvhiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{F6179E68-3B70-4AFB-AFD5-2DB779CB0A4C}", UserName));
            X.GetCmp<Hidden>("cvhiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{AA6DBE28-6FA5-4D51-A466-7D1FA1BCC038}", UserName));
            X.GetCmp<Hidden>("cvhiddenHighPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{41A47473-93AE-4A4B-9DB6-22A7511B45C9}", UserName));
            X.GetCmp<Hidden>("cvhiddenPermModifierCertif").SetValue(HasAccess.fnGetUserAccessStatus("{6b5a88b2-caba-42c1-beb9-177ebc561030}", UserName));

            #endregion
            return View();
        }

        public ActionResult onCreate()
        {
            ContratDeVentesViewModel mclass = new ContratDeVentesViewModel();
            Parametres mParam = new Parametres(0);

            mclass._PrefixeContratNum = mParam.PrefixeContratNum;
            mclass._DefaultExportateur = mParam.Exportateur.ID;
            mclass._DefaultClient = mParam.ClientDefID;
            mclass._DefaultConditionnement = mParam.ConditionnementDefID;
            mclass._DefaultOrigine = mParam.OrigineContratExportDefID;
            mclass._DefaultTermePaiement = mParam.ContratPaiementTermeDefault;
            mclass._WeightOptionText = mParam.ContratOptionPoidsDefault;
            mclass._DeliveryCondition = mParam.ContratDeliveryCondition;
            mclass._DefaultFobDiscountPrice = mParam.FobDiscountPriceDefault;
            mclass._DefBonusCertification = mParam.ContratVenteBonusCertificationValue;
            mclass._DefaultProduitExport = mParam.IdProduitExport;
            mclass._ContratDeVentes = new ContratDeVentes();

            mclass._ContratDeVentes.Campagne = new Parametres(0).Campagne;

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            ViewData["ValeurConversionEuroFrancs"] = mParam.ValeurTauxEuro;
            ViewData["BonusCertification"] = mParam.ContratVenteBonusCertificationValue;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "ContratDeVentes_Detail", Model= mclass, ViewData = ViewData };
        }
        public ActionResult onEdit(string ItemSelected)
        {
            ContratDeVentes mclass = JSON.Deserialize<ContratDeVentes>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate , NullValueHandling = NullValueHandling.Ignore});

            ContratDeVentesViewModel viewModel = new ContratDeVentesViewModel();
            Parametres mParam = new Parametres(0);
            viewModel._PrefixeContratNum = mclass.ContratNumero;
            viewModel._DefaultExportateur = (int?)null;
            viewModel._DefaultClient = (int?)null;
            viewModel._DefaultConditionnement = (int?)null;
            viewModel._DefaultOrigine = (int?)null;
            viewModel._DefaultTermePaiement = (int?)null;
            viewModel._DeliveryCondition = (int?)null;
            viewModel._DefaultProduitExport = (int?)null;
            viewModel._DefaultFobDiscountPrice = (decimal?)null;
            viewModel._WeightOptionText = "";
            viewModel._ContratDeVentes = mclass;
            viewModel._DefBonusCertification = mParam.ContratVenteBonusCertificationValue;
            ViewData["ValeurConversionEuroFrancs"] = mParam.ValeurTauxEuro;
            ViewData["BonusCertification"] = mParam.ContratVenteBonusCertificationValue;
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "ContratDeVentes_Detail", Model = viewModel, ViewData = ViewData };
        }

        public ActionResult OnAddWeight()
        {
            ContratDeVentePoidsStandard mclass = new ContratDeVentePoidsStandard();            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "ContratDeVentes_Poids", Model = mclass };
        }

        public ActionResult OnChangeCertif(string ItemSelected)
        {
            ContratDeVentes mclass = JSON.Deserialize<ContratDeVentes>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            ContratDeVentesViewModel viewModel = new ContratDeVentesViewModel();
            Parametres mParam = new Parametres(0);
            viewModel._PrefixeContratNum = mclass.ContratNumero;
            viewModel._DefaultExportateur = (int?)null;
            viewModel._DefaultClient = (int?)null;
            viewModel._DefaultConditionnement = (int?)null;
            viewModel._DefaultOrigine = (int?)null;
            viewModel._DefaultTermePaiement = (int?)null;
            viewModel._DeliveryCondition = (int?)null;
            viewModel._DefaultFobDiscountPrice = (decimal?)null;
            viewModel._DefaultProduitExport = (int?)null;
            viewModel._WeightOptionText = "";
            viewModel._ContratDeVentes = mclass;
            ViewData["ValeurConversionEuroFrancs"] = mParam.ValeurTauxEuro;
            ViewData["BonusCertification"] = mParam.ContratVenteBonusCertificationValue;
            viewModel._DefBonusCertification = mParam.ContratVenteBonusCertificationValue;
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "ContratDeVentes_Certification", Model = viewModel, ViewData = ViewData };
        }

        public ActionResult onConsult(string ItemSelected)
        {

            ContratDeVentes mclass = JSON.Deserialize<ContratDeVentes>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            Parametres mParam = new Parametres(0);
            ContratDeVentesViewModel viewmodel = new ContratDeVentesViewModel();
            viewmodel._ContratDeVentes = mclass;
            viewmodel._DefaultExportateur = (int?)null;
            viewmodel._DefaultClient = (int?)null;
            viewmodel._DefaultConditionnement = (int?)null;
            viewmodel._DefaultOrigine = (int?)null;
            viewmodel._DefaultTermePaiement = (int?)null;
            viewmodel._DeliveryCondition = (int?)null;
            viewmodel._DefaultFobDiscountPrice = (decimal?)null;
            viewmodel._DefaultProduitExport = (int?)null;
            viewmodel._WeightOptionText = "";
            viewmodel._PrefixeContratNum = mclass.ContratNumero;
            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
            viewmodel._DefBonusCertification = mParam.ContratVenteBonusCertificationValue;
            ViewData["ValeurConversionEuroFrancs"] = mParam.ValeurTauxEuro;
            ViewData["BonusCertification"] = mParam.ContratVenteBonusCertificationValue;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "ContratDeVentes_Detail", Model = viewmodel, ViewData = ViewData };
        }

        public ActionResult OnApprove(string ItemSelected)
        {

            ContratDeVentes mclass = JSON.Deserialize<ContratDeVentes>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            Parametres mParam = new Parametres(0);
            ContratDeVentesViewModel viewModel = new ContratDeVentesViewModel();
            viewModel._PrefixeContratNum = mclass.ContratNumero;
            viewModel._ContratDeVentes = mclass;
            viewModel._DefaultExportateur = (int?)null;
            viewModel._DefaultClient = (int?)null;
            viewModel._DefaultConditionnement = (int?)null;
            viewModel._DefaultOrigine = (int?)null;
            viewModel._DefaultTermePaiement = (int?)null;
            viewModel._DeliveryCondition = (int?)null;
            viewModel._DefaultFobDiscountPrice = (decimal?)null;
            viewModel._DefaultProduitExport = (int?)null;
            viewModel._WeightOptionText = "";
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;
            viewModel._DefBonusCertification = mParam.ContratVenteBonusCertificationValue;
            ViewData["ValeurConversionEuroFrancs"] = mParam.ValeurTauxEuro;
            ViewData["BonusCertification"] = mParam.ContratVenteBonusCertificationValue;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "ContratDeVentes_Detail", Model = viewModel, ViewData = ViewData};
        }

        public ActionResult OnCancel(string ItemSelected)
        {
            try
            {
                ContratDeVentes mClass = JSON.Deserialize<ContratDeVentes>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);

                if (!result)
                    throw new Exception("OnCancel : Sales Contract loading failed.");

                bool resultFinCancel = false;
                mClass.UtilisateurModification = (string)Session["userName"];
                resultFinCancel = mClass.fnCancel();

                if (resultFinCancel)
                {
                    Store mstore = X.GetCmp<Store>("storeListeContratDeVentes");

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
                    Title = "Sales Contract : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnClose(string ItemSelected)
        {
            try
            {
                ContratDeVentes mClass = JSON.Deserialize<ContratDeVentes>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);

                if (!result)
                    throw new Exception("OnClose : Sales Contract loading failed.");
                if (mClass.Statut != "AP")
                    throw new Exception("OnClose : Sales Contract is not aprpoved.");

                bool resultFinCancel = false;
                mClass.UtilisateurModification = (string)Session["userName"];
                resultFinCancel = mClass.fnClose();

                if (resultFinCancel)
                {
                    Store mstore = X.GetCmp<Store>("storeListeContratDeVentes");

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
                    Title = "Sales Contract : Close",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult LoadOfSalesContract(StoreRequestParameters parameters, string ItemCropYear, string ItemExporter, string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {

            int ExportateurID = GetCriteriaValue(ItemExporter);
            int CertificationID = GetCriteriaValue(ItemCertification);
            string Campagne = string.IsNullOrEmpty(ItemCropYear) ? "" : ItemCropYear;

            if (!string.IsNullOrEmpty(ItemCropYear) && ItemCropYear.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string Status = ItemStatus;

            var mListe = (new ContratDeVentes()).fnSelect(Campagne, ExportateurID, CertificationID, StartDate, EndDate, Status);
            //var paging = GridStorePaging.SetRangePlants(parameters, mListe);

            //return this.Store(paging);
            return this.Store(mListe);
        }

        public ActionResult LoadListOfWeight()
        {
            var mListe = (new ContratDeVentePoidsStandard()).fnSelect(0);
            return this.Store(mListe);
        }

        public ActionResult LoadContratCampagneActive(string ItemCampagne, string ItemExportateur)
        {
            List<DataPersist> liste = new List<DataPersist>();
           if (string.IsNullOrEmpty(ItemExportateur))
           {
                liste = new ContratDeVentes().fnSelect(ItemCampagne, -1, -1, null, null, "AP");

           }
           else
           {
                int exporter = int.Parse(ItemExportateur);
                liste = new ContratDeVentes().fnSelect(ItemCampagne, exporter, -1, null, null, "AP");
            }
            return this.Store(liste);
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ContratDeVentesCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemCropYear, string ItemExporter, string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeContratDeVentes");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemExporter"   ,ItemExporter),
                                    new Ext.Net.Parameter("ItemCropYear"      ,ItemCropYear),
                                    new Ext.Net.Parameter("ItemCertification"      ,ItemCertification),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus)
                                });



                // collapse criterias areas
                X.GetCmp<FormPanel>("ContratDeVentesCriteriaPanel").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sales Contract : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitFormMethod()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            bool HasHighPermission = true;
            HasHighPermission = HasAccess.fnGetUserAccessStatus("{41A47473-93AE-4A4B-9DB6-22A7511B45C9}", UserName);

            try
            {
                ContratDeVentes mClass = new ContratDeVentes();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("txtContratDeVentesID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Sales Contract load failed.");

                    if ((!string.IsNullOrEmpty(mClass.UtilisateurApprobation) || mClass.DateApprobation != null) && HasHighPermission == false)
                        throw new Exception("SubmitFormMethod : Sales Contract Already approved ! Please Refresh Overview");

                }

                bool Result = true;

                mClass = MapFormToObject(mClass);
                Result = mClass.fnUpdate();
                //Result = true;
                if (Result)
                {
                    Store mStore = X.GetCmp<Store>("storeListeContratDeVentes");

                    ContratDeVentes mContrat = new ContratDeVentes();
                    mContrat.fnGet(mClass.ID);

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mContrat);
                        X.GetCmp<RowSelectionModel>("rowSelectionListeContratDeVentes").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mStore.GetById(mContrat.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mContrat);

                        mProxy.Commit();

                        mProxy.EndEdit();

                    }

                    X.GetCmp<Window>("ContratDeVentes_Detail").Close();
                }







            }
            catch (Exception ex)
            {

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

        public ActionResult UpdateCertification()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            bool HasHighPermission = true;
            HasHighPermission = HasAccess.fnGetUserAccessStatus("{6b5a88b2-caba-42c1-beb9-177ebc561030}", UserName);

            try
            {
                ContratDeVentes mClass = new ContratDeVentes();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);                
                mClass.IsNew = false;
                mClass.fnGet(Guid.Parse(GetFormValue("txtContratDeVentesID")));

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("SubmitFormMethod : Sales Contract load failed.");

                if (HasHighPermission == false)
                    throw new Exception("SubmitFormMethod : Sales Contract Already approved ! Please Refresh Overview");

                bool Result = true;
                //mClass = MapFormToObject(mClass);
                string cert = X.GetCmp<ComboBox>("_cmbCertification").Text;
                if (!string.IsNullOrEmpty(cert))
                {
                    Certification mCert = new Certification();
                    mCert.ID = int.Parse(X.GetCmp<ComboBox>("_cmbCertification").Text);
                    mCert.Designation = X.GetCmp<ComboBox>("_cmbCertification").SelectedItem.Text.ToString();
                    mClass.Certification = mCert;
                }
                else
                {
                    mClass.Certification = null;
                }

                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];
                Result = mClass.fnUpdateCertif();
                //Result = true;
                if (Result)
                {
                    Store mStore = X.GetCmp<Store>("storeListeContratDeVentes");
                    ContratDeVentes mContrat = new ContratDeVentes();
                    mContrat.fnGet(mClass.ID);

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mContrat);
                        X.GetCmp<RowSelectionModel>("rowSelectionListeContratDeVentes").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mStore.GetById(mContrat.ID);
                        mProxy.BeginEdit();
                        mProxy.Set(mContrat);
                        mProxy.Commit();
                        mProxy.EndEdit();
                    }
                    X.GetCmp<Window>("ContratDeVentes_Certification").Close();
                }

            }
            catch (Exception ex)
            {

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


        public ActionResult SubmitApproval()
        {

            try
            {
                ContratDeVentes mClass = new ContratDeVentes();
                mClass.IsNew = false;

                bool result = mClass.fnGet(Guid.Parse(GetFormValue("txtContratDeVentesID")));

                if (!result)
                    throw new Exception("OnApprove : Sales Contract loading failed.");

                

                bool resultFinApprobation = false;

                if (mClass.Statut == "NA")
                {
                    mClass.UtilisateurApprobation = (string)Session["userName"];
                    resultFinApprobation = mClass.fnApprove();
                }

                if (resultFinApprobation)
                {

                    Store mstore = X.GetCmp<Store>("storeListeContratDeVentes");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();



                    X.GetCmp<Window>("ContratDeVentes_Detail").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sales Contract : Approuver",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnPrintSalesContract(string typeReport)
        {
            ViewData["typeReport"] = typeReport;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintContratDeVentesReport", ViewData = ViewData };

        }

        public ActionResult PrintSalesContract(string TypeReport)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;
                string type = "";
                if (TypeReport == "Ba")
                {
                    report = new rptContratDeVentesBalance() as XtraReport;
                    type = "Contracts - Balance";
                }
                if (TypeReport == "Ex")
                {
                    report = new rptContratDeVentesExecution() as XtraReport;
                    type = "Contracts - Execution";
                }
                if (TypeReport == "Hi")
                {
                    report = new rptContratDeVentesHistory() as XtraReport;
                    type = "Contracts - History";

                }

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["paramCampagne"].Value = X.GetCmp<ComboBox>("SLcropYearForReport").SelectedItem.Text;

                report.Parameters["paramExportateurID"].Value = X.GetCmp<ComboBox>("SLexportateurForReport").SelectedItem.Value.ToString();
                report.Parameters["paramExportateur"].Value = X.GetCmp<ComboBox>("SLexportateurForReport").SelectedItem.Text;

                report.Parameters["paramCertificationID"].Value = X.GetCmp<ComboBox>("SLCertificationForReport").SelectedItem.Value.ToString();
                report.Parameters["paramCertification"].Value = X.GetCmp<ComboBox>("SLCertificationForReport").SelectedItem.Text;

                if (TypeReport == "Hi" || TypeReport == "Ex")
                {

                    report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("SLstartDateForReport").RawText.ToString());
                    report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("SLdueDateForReport").RawText.ToString());

                }

                if (TypeReport == "Hi")
                {

                    report.Parameters["paramStatutID"].Value = X.GetCmp<ComboBox>("SLStatus").SelectedItem.Value.ToString();
                    report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("SLStatus").SelectedItem.Text;

                }
                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/ContratDeVentes/ViewList', this, '{2}',''),App.FormPrintContratDeVentesReport.doClose()", Guid.NewGuid(), BaseUrl, type));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sales Contracts : Data Validation",
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

        public ActionResult OnSubmitWeight(string ItemSelected)
        {
            ContratDeVentePoidsStandard mclass = JSON.Deserialize<ContratDeVentePoidsStandard>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            X.GetCmp<TextField>("txtQuantity").SetText(mclass.PoidsConvention.ToString());
            X.GetCmp<Window>("ContratDeVentes_Poids").Close();
            return this.Direct();
        }

        #region "Methods"
        private ContratDeVentes MapFormToObject(ContratDeVentes mClass)
        {
            try
            {
                mClass.ID = Guid.Parse(X.GetCmp<Hidden>("txtContratDeVentesID").Text);

                mClass.Numero = X.GetCmp<TextField>("txtNumber").Text;
                mClass.ContratNumero = X.GetCmp<TextField>("txtContratNumero").Text;

                mClass.Campagne = X.GetCmp<ComboBox>("_cmbCampagne").Text;

                Exportateur mExportateur = new Exportateur();
                mExportateur.ID = int.Parse(X.GetCmp<ComboBox>("_cmbExportateur").Text);
                mExportateur.Nom = X.GetCmp<ComboBox>("_cmbExportateur").SelectedItem.Text.ToString();
                mClass.Exportateur = mExportateur;

                Client mClient = new Client();
                mClient.ID = int.Parse(X.GetCmp<ComboBox>("_cmbCustomer").Text);
                mClient.Nom = X.GetCmp<ComboBox>("_cmbCustomer").SelectedItem.Text.ToString();
                mClass.Client = mClient;


                mClass.Banque = null;
                if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("_cmbDomiciliation").SelectedItem.Text))
                {
                    mClass.Banque = new Banque();
                    mClass.Banque.ID = int.Parse(X.GetCmp<ComboBox>("_cmbDomiciliation").Text);
                    mClass.Banque.Nom = X.GetCmp<ComboBox>("_cmbDomiciliation").SelectedItem.Text.ToString();
                }
                else
                    mClass.Banque = null;                

                ProduitExport mProduit = new ProduitExport();
                mProduit.ID = int.Parse(X.GetCmp<ComboBox>("_cmbProduitExport").Text);
                mProduit.Designation = X.GetCmp<ComboBox>("_cmbProduitExport").SelectedItem.Text.ToString();
                mClass.ProduitExport = mProduit;

                mClass.ReferenceClient = X.GetCmp<TextField>("txtCustomRef").Text;

                mClass.Date = DateTime.Parse(X.GetCmp<DateField>("txtDate").RawText.ToString());

                mClass.DebutExpedition = DateTime.Parse(X.GetCmp<DateField>("txtStartShipping").RawText.ToString());
                mClass.FinExpedition = DateTime.Parse(X.GetCmp<DateField>("txtEndShiping").RawText.ToString());

                decimal mQuantite = 0;
                if (X.GetCmp<TextField>("txtQuantity").Text != string.Empty) mQuantite = decimal.Parse(X.GetCmp<TextField>("txtQuantity").Text);
                bool IsConv = false;
                IsConv = bool.Parse(X.GetCmp<Checkbox>("ChkPoidsEstConventionnel").Value.ToString());

                if (IsConv)
                {
                    ContratDeVentePoidsStandard mpoids = new ContratDeVentePoidsStandard();
                    bool result = false;
                    result = mpoids.fnGetByWeight(mQuantite);

                    if (result)
                        mClass.Quantite = mpoids.PoidsConvention;
                    else
                        mClass.Quantite = mQuantite;

                }else
                    mClass.Quantite = mQuantite;

                if (X.GetCmp<TextField>("txtPrice").Text != string.Empty) mClass.Prix = decimal.Parse(X.GetCmp<TextField>("txtPrice").Text);
                if (X.GetCmp<TextField>("txtFobDiscount").Text != string.Empty) mClass.RemiseFob = decimal.Parse(X.GetCmp<TextField>("txtFobDiscount").Text);
                if (X.GetCmp<TextField>("txtFobPrice").Text != string.Empty) mClass.PrixFob = decimal.Parse(X.GetCmp<TextField>("txtFobPrice").Text);

                if (X.GetCmp<TextField>("txtPriceCFA").Text != string.Empty) mClass.PrixCfa = decimal.Parse(X.GetCmp<TextField>("txtPriceCFA").Text);
                if (X.GetCmp<TextField>("txtFobPriceCfa").Text != string.Empty) mClass.PrixFobCfa = decimal.Parse(X.GetCmp<TextField>("txtFobPriceCfa").Text);
                if (X.GetCmp<TextField>("txtPrixCertifie").Text != string.Empty) mClass.PrixEuroCertifie = decimal.Parse(X.GetCmp<TextField>("txtPrixCertifie").Text);
                if (X.GetCmp<TextField>("txtRefBonusCertif").Text != string.Empty) mClass.BonusCertification = decimal.Parse(X.GetCmp<TextField>("txtRefBonusCertif").Text);
                //mClass.ReferenceProduit = X.GetCmp<TextField>("txtProductRef").Text;               
                mClass.PoidsOptions = X.GetCmp<TextField>("txtWeight").Text; 
                mClass.Options = X.GetCmp<TextArea>("txtOptions").Text;
                mClass.Remarques = X.GetCmp<TextArea>("txtRemarks").Text;
                string cert = X.GetCmp<ComboBox>("_cmbCertification").Text;
                if (!string.IsNullOrEmpty(cert))
                {
                    Certification mCert = new Certification();
                    mCert.ID = int.Parse(X.GetCmp<ComboBox>("_cmbCertification").Text);
                    mCert.Designation = X.GetCmp<ComboBox>("_cmbCertification").SelectedItem.Text.ToString();
                    mClass.Certification = mCert;
                }
                else
                {
                    mClass.Certification = null;
                }
                


                Qualite mQualite= new Qualite();
                mQualite.ID = int.Parse(X.GetCmp<ComboBox>("_cmbQuality").Text);
                mQualite.Designation = X.GetCmp<ComboBox>("_cmbQuality").SelectedItem.Text.ToString();
                mClass.Qualite = mQualite;

                ConditionLivraison mCondLivr = new ConditionLivraison();
                mCondLivr.ID = int.Parse(X.GetCmp<ComboBox>("_cmbDeliveryConditions").Text);
                mCondLivr.Designation = X.GetCmp<ComboBox>("_cmbDeliveryConditions").SelectedItem.Text.ToString();
                mClass.ConditionLivraison = mCondLivr;

                TermesPaiement mTerme = new TermesPaiement();
                mTerme.ID = int.Parse(X.GetCmp<ComboBox>("_cmbPaymentTerms").Text);
                mTerme.Designation = X.GetCmp<ComboBox>("_cmbPaymentTerms").SelectedItem.Text.ToString();
                mClass.TermesPaiement = mTerme;

                Conditionnement mPackaging = new Conditionnement();
                mPackaging.ID = int.Parse(X.GetCmp<ComboBox>("_cmbPackaging").Text);
                mPackaging.Designation = X.GetCmp<ComboBox>("_cmbPackaging").SelectedItem.Text.ToString();
                mClass.Conditionnement = mPackaging;

                Origine mOrigine = new Origine();
                mOrigine.ID = int.Parse(X.GetCmp<ComboBox>("_cmbOrigin").Text);
                mOrigine.Nom = X.GetCmp<ComboBox>("_cmbOrigin").SelectedItem.Text.ToString();
                mClass.Origine = mOrigine;

                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];

                mClass.Solde = mClass.IsActive ? mClass.Quantite : mClass.Solde;

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sales Contract : MapFormToObject",
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
            X.GetCmp<RowSelectionModel>("rowSelectionListeContratDeVentes").DeselectAll();
        }

        #endregion
    }
}