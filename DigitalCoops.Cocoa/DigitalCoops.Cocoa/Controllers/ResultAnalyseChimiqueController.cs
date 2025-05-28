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
using Tms.Classes.Business;
using Tms.Classes.Business.stock;
using Tms.Classes.Security;
using Tms.Classes.Shared;

namespace Tms2017.MVC.Controllers
{
    public class ResultAnalyseChimiqueController : BaseController
    {
        // GET: ResultAnalyseChimique

        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";
        public ActionResult Index()
        {
            X.GetCmp<DateField>("TxtPeriodStart").RawText = DateTime.Now.ToShortDateString();
            X.GetCmp<DateField>("TxtPeriodEnd").RawText = DateTime.Now.ToShortDateString();

            X.GetCmp<FormPanel>("AnalyseChimiqueCP").SetTitle("Today");

            #region Set Function's Access

            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{FF56B18B-CE36-4F1E-902F-B54F3E9DB762}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{4AFD3DEB-1202-4E80-86CC-59318F0019D0}")))
                X.GetCmp<Button>("btnNewAnalyseChimique").Enable();
            else
                X.GetCmp<Button>("btnNewAnalyseChimique").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{B829B9AE-BCB0-4E03-B0AF-17612B183210}")))
                X.GetCmp<MenuItem>("mnuPrintAnalyseChimiqueList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintAnalyseChimiqueList").Disable();            

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{8DFECA14-1233-41E0-BDE4-5FAFF15CBE6D}")))
                X.GetCmp<MenuItem>("mnuExportChemicalAnalysis").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportChemicalAnalysis").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{A943C875-50DA-4445-A2B9-13D4AF32A44E}")))
                X.GetCmp<Hidden>("RahiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("RahiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{238B7DF9-D375-4CE7-A05D-91C1C9CBE402}")))
                X.GetCmp<Hidden>("RahiddenPermApprove").SetValue(true);
            else
                X.GetCmp<Hidden>("RahiddenPermApprove").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{810FD80B-9BAF-4CBC-84EC-2AF5BED1A910}")))
                X.GetCmp<Hidden>("RahiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("RahiddenPermDesactiver").SetValue(false);

            //X.GetCmp<Hidden>("mvhiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{94E1648B-BB69-4470-8B5D-FA8D19598161}", UserName));
            //X.GetCmp<Hidden>("mvhiddenPermGenerate").SetValue(HasAccess.fnGetUserAccessStatus("{C1785226-126D-4648-95C6-92894707BC1E}", UserName));
            //X.GetCmp<Hidden>("mvhiddenPermPrint").SetValue(HasAccess.fnGetUserAccessStatus("{DF2C998A-9232-48ED-B403-6DB46289BE0A}", UserName));
            //X.GetCmp<Hidden>("mvhiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{3CA6BD01-DFC1-4B9F-9E0E-134391DF8B4F}", UserName));
            #endregion

            return View();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("AnalyseChimiqueCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            if (string.IsNullOrEmpty(ItemPeriodStart) && string.IsNullOrEmpty(ItemPeriodEnd) && string.IsNullOrEmpty(ItemStatus))
            {
                return this.Store(null);
            }
            else
            {
                DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
                DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

                int status = -1;
                if (!string.IsNullOrEmpty(ItemStatus))
                {
                    status = int.Parse(ItemStatus);
                }

                var mListe = (new AnalyseChimique()).fnSelect(startdate, enddate, status);

                return this.Store(mListe);
            }
        }

        public ActionResult OnRefresh(string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store store = X.GetCmp<Store>("storeListeAnalyseChimique");
                store.Reload();
                store.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemPeriodStart"     ,ItemPeriodStart),
                                new Ext.Net.Parameter("ItemPeriodEnd"           ,ItemPeriodEnd),
                                new Ext.Net.Parameter("ItemStatus",ItemStatus)
                            });

                X.GetCmp<FormPanel>("AnalyseChimiqueCP").Collapsed = true;

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Chemical Analysis : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult onAdd()
        {           
            AnalyseChimiqueViewModel mclass = new AnalyseChimiqueViewModel();

            mclass._AnalyseChimique = new AnalyseChimique();

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            Parametres mParam = new Parametres(0);
            mclass._DefaultLaboratoire = mParam.LaboExport;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormResultAnalyseChimique", Model = mclass };

        }

        public ActionResult onEdit(string ItemSelected)
        {
            AnalyseChimiqueViewModel mclass = new AnalyseChimiqueViewModel();
            mclass._AnalyseChimique = JSON.Deserialize<AnalyseChimique>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            //mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            Parametres mParam = new Parametres(0);
            mclass._DefaultLaboratoire = mParam.LaboExport;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormResultAnalyseChimique", Model = mclass };
        }

        public ActionResult onConsult(string ItemSelected)
        {
            AnalyseChimiqueViewModel mclass = new AnalyseChimiqueViewModel();
            mclass._AnalyseChimique = JSON.Deserialize<AnalyseChimique>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            //mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
            Parametres mParam = new Parametres(0);
            mclass._DefaultLaboratoire = mParam.LaboExport;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormResultAnalyseChimique", Model = mclass };
        }

        public ActionResult onApprove(string ItemSelected)
        {
            AnalyseChimiqueViewModel mclass = new AnalyseChimiqueViewModel();
            mclass._AnalyseChimique = JSON.Deserialize<AnalyseChimique>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            //mclass._DefaultCampagne = new Parametres(0).Campagne;
            Parametres mParam = new Parametres(0);
            mclass._DefaultLaboratoire = mParam.LaboExport;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormResultAnalyseChimique", Model = mclass };            
        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                AnalyseChimique mClass = new AnalyseChimique();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtAnalyseChimiqueID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Chemical Analysis load failed.");
                }

                mClass = MapFormToObject(mClass);

                bool result = mClass.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeAnalyseChimique");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowAnalyseChimique").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormResultAnalyseChimique").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Chemical Analysis : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitAnalyeApprove()
        {
            try
            {
                AnalyseChimique analyse = new AnalyseChimique();

                analyse.ID = Guid.Parse(X.GetCmp<TextField>("TxtAnalyseChimiqueID").Text.ToString());

                bool result = analyse.fnGet(analyse.ID);

                if (analyse == null || analyse.ID == Guid.Empty)
                    throw new Exception("onApprovePhysicalAnalysis : Chemical Analysis Approve failed.");

                analyse.Approbateur = (string)Session["userName"];
                analyse.DateApprobation = DateTime.Now;
                analyse.Statut = X.GetCmp<ComboBox>("CmbResultatAnalyse").RawValue.ToString();
                analyse.Confirmation = true;
                //analyse.Commentaire = X.GetCmp<TextField>("TxtCommentaireAnalyseChimique").Text;
                analyse.UtilisateurModification = (string)Session["userName"];

                result = analyse.fnApprove();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeAnalyseChimique");

                    ModelProxy mProxy = mstore.GetById(analyse.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(analyse);

                    mProxy.Commit();

                    mProxy.EndEdit();
                    X.GetCmp<Window>("FormResultAnalyseChimique").Close();
                    X.Js.Call("Overview.resetButtons");
                }
                else
                {
                    X.GetCmp<Window>("FormResultAnalyseChimique").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Chemical Analysis : Approuver",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        private AnalyseChimique MapFormToObject(AnalyseChimique analyse)
        {
            analyse.Echantillon = new AnalyseChimiqueEchantillon();
            //analyse.Echantillon.fnGetByCode(X.GetCmp<ComboBox>("cmbEchantillon").SelectedItem.Text);
            if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("cmbEchantillon").Text))
            {
                analyse.Echantillon.ID = Guid.Parse(X.GetCmp<ComboBox>("cmbEchantillon").RawValue.ToString());
                analyse.Echantillon.NumeroEchantillon = X.GetCmp<ComboBox>("cmbEchantillon").SelectedItem.Text.ToString();
            }

            //if (analyse.Echantillon.ID == Guid.Empty || analyse.Echantillon == null)
            //{
            //    throw new Exception("SubmitFormMethod : Lot N° not Found !.");
            //}            
            analyse.Laboratoire = new Laboratoire();
            analyse.Laboratoire.ID = int.Parse(X.GetCmp<ComboBox>("CmbLaboratoire").RawValue.ToString());

            analyse.DateAnalyse = DateTime.Parse(X.GetCmp<DateField>("TxtDateAnalyseChimique").RawText.ToString()).Add(DateTime.Now.TimeOfDay);
            analyse.FicheNumero = X.GetCmp<TextField>("TxtSheetNumber").Text;

            analyse.PoidsFeves = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtPoidsFeves").RawText) ? double.Parse(X.GetCmp<NumberField>("TxtPoidsFeves").RawText) : 0;
            analyse.WeightShell = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtWeightShell").RawText) ? double.Parse(X.GetCmp<NumberField>("TxtWeightShell").RawText) : 0;
            analyse.ShellContentPc = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtShellContentPc").RawText) ? double.Parse(X.GetCmp<NumberField>("TxtShellContentPc").RawText) : 0;
            analyse.WeightGroundBeans = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtWeightGroundBeans").RawText) ? double.Parse(X.GetCmp<NumberField>("TxtWeightGroundBeans").RawText) : 0;
            analyse.WeightGroundBeansAndAluminium = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtWeightGroundBeansAndAluminium").RawText) ? double.Parse(X.GetCmp<NumberField>("TxtWeightGroundBeansAndAluminium").RawText) : 0;
            analyse.WeightGroundBeansAluminumClean = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtWeightGroundBeansAluminumClean").RawText) ? double.Parse(X.GetCmp<NumberField>("TxtWeightGroundBeansAluminumClean").RawText) : 0;

            analyse.Moisture = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtMoisture").RawText) ? double.Parse(X.GetCmp<NumberField>("TxtMoisture").RawText) : 0;
            analyse.PH = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtPH").RawText) ? double.Parse(X.GetCmp<NumberField>("TxtPH").RawText) : 0;
            analyse.WeightDryFlaskPumice = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtWeightDryFlaskPumice").RawText) ? double.Parse(X.GetCmp<NumberField>("TxtWeightDryFlaskPumice").RawText) : 0;
            analyse.WeightGroundBeans2 = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtWeightGroundBeans2").RawText) ? double.Parse(X.GetCmp<NumberField>("TxtWeightGroundBeans2").RawText) : 0;
            analyse.WeightFlaskFatClean = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtWeightFlaskFatClean").RawText) ? double.Parse(X.GetCmp<NumberField>("TxtWeightFlaskFatClean").RawText) : 0;
            analyse.WeightFat = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtWeightFat").RawText) ? double.Parse(X.GetCmp<NumberField>("TxtWeightFat").RawText) : 0;

            analyse.FatContentPc = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtFatContentPc").RawText) ? double.Parse(X.GetCmp<NumberField>("TxtFatContentPc").RawText) : 0;
            analyse.ActualFatContent = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtActualFatContent").RawText) ? double.Parse(X.GetCmp<NumberField>("TxtActualFatContent").RawText) : 0;
            analyse.KOH = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtKOH").RawText) ? double.Parse(X.GetCmp<NumberField>("TxtKOH").RawText) : 0;
            analyse.FFA = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtFfa").RawText) ? double.Parse(X.GetCmp<NumberField>("TxtFFA").RawText) : 0;
            analyse.BlankFromPetroleum = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtBlankFromPetroleum").RawText) ? double.Parse(X.GetCmp<NumberField>("TxtBlankFromPetroleum").RawText) : 0;

            analyse.Confirmation = false;
            analyse.Statut = X.GetCmp<ComboBox>("CmbResultatAnalyse").RawValue.ToString();

            analyse.Analyseur = null;
            if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbAnalyseur").Text))
            {
                analyse.Analyseur = new Analyseur();
                analyse.Analyseur.ID = int.Parse(X.GetCmp<ComboBox>("CmbAnalyseur").RawValue.ToString());
                analyse.Analyseur.Nom = X.GetCmp<ComboBox>("CmbAnalyseur").RawText;
            }

            analyse.Verificateur = null;
            if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbSuperviseur").Text))
            {
                analyse.Verificateur = new Analyseur();
                analyse.Verificateur.ID = int.Parse(X.GetCmp<ComboBox>("CmbSuperviseur").RawValue.ToString());
                analyse.Verificateur.Nom = X.GetCmp<ComboBox>("CmbSuperviseur").RawText;
            }

            analyse.UtilisateurCreation = (string)Session["userName"];
            analyse.UtilisateurModification = (string)Session["userName"];

            return analyse;
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                AnalyseChimique analyse = JSON.Deserialize<AnalyseChimique>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = analyse.fnGet(analyse.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Chemical Analysis loading failed.");

                analyse.UtilisateurModification = (string)Session["userName"];

                if (analyse.Desactive)
                    result = analyse.fnActivate();
                else
                    result = analyse.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Chemical Analysis, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeAnalyseChimique");

                    ModelProxy mProxy = mstore.GetById(analyse.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(analyse);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Chemical Analysis : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnDisplayAnalysePhysiqueCriteria()
        {

            ViewData["Titre"] = "Print Chemical Analysis";
            ViewData["actionToDo"] = "OnPrintAnalyseChimiqueList";
            ViewData["ControllerName"] = "ResultAnalyseChimique";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmCriteriaForPhysicalAnalysis", ViewData = ViewData };
        }

        public ActionResult OnPrintAnalyseChimiqueList(string startDate, string endDate, string statut, string statutText)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                rptAnalyseChimiqueList report = new rptAnalyseChimiqueList();
                report.DataSource = DevExpressReportDs.SetDataSource(report);

                report.Parameters["paramDateDebut"].Value = dateDebut;
                report.Parameters["paramDateFin"].Value = dateFin;
                report.Parameters["paramStatut"].Value = int.Parse(statut);
                report.Parameters["paramStatutText"].Value = statutText;

                Session["report"] = report;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/ResultAnalyseChimique/ViewReportListResult', this, 'List Of Chemical Analysis',''),App.frmCriteriaForPhysicalAnalysis.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Chemical Analysis : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
        }

        public ActionResult ViewReportListResult()
        {
            XtraReport report = null;

            report = Session["report"] as XtraReport;

            ViewData["Report"] = report;
            return View("ViewReportResult");
        }


        #region Method
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
                    else if (hidAction.ToString().Equals(EXTEND))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Extend;
                }
                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
            }
            catch (Exception ex)
            {
                Ext.Net.X.Msg.Alert("frm_Detail : GetFormExecMode", ex.Message).Show();
                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
            }
        }

        private int GetCritriaValue(string strComponent)
        {
            int value;

            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
                return value;
            else
                return -1;
        }

        private int GetCritriasValue(string strComponent)
        {
            if (X.GetCmp<ComboBox>(strComponent) != null && X.GetCmp<ComboBox>(strComponent).SelectedItem != null && X.GetCmp<ComboBox>(strComponent).SelectedItem.Value != null)
                return int.Parse(X.GetCmp<ComboBox>(strComponent).SelectedItem.Value.ToString());
            else
                return -1;
        }

        #endregion

    }
}