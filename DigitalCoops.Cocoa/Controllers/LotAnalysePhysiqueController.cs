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
    public class LotAnalysePhysiqueController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: LotAnalysePhysique
        public ActionResult Index()
        {
            X.GetCmp<DateField>("TxtPeriodStart").RawText = DateTime.Now.ToShortDateString();
            X.GetCmp<DateField>("TxtPeriodEnd").RawText = DateTime.Now.ToShortDateString();

            X.GetCmp<FormPanel>("LotAnalysePhysiqueExportCP").SetTitle("Today");

            #region Set Function's Access

            string UserName = (string)Session["userName"];                       

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{44A37AF3-B662-4554-BD92-F7DAFA4024F1}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{65903206-8631-4854-90C7-CAFFCBFAB620}")))
                X.GetCmp<Button>("btnNewAnalysePhysique").Enable();
            else
                X.GetCmp<Button>("btnNewAnalysePhysique").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{9EB6A6F2-7973-452B-BED9-3E2E2C89741E}")))
                X.GetCmp<MenuItem>("mnuPrintAnalysePhysiqueList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintAnalysePhysiqueList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{4A463348-9DAF-4395-B3C6-99CD7DA3F4F8}")))
                X.GetCmp<MenuItem>("mnuPrintAnalysePhysiqueProduction").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintAnalysePhysiqueProduction").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{9A1E6C31-8920-4898-B499-51BF3C624AED}")))
                X.GetCmp<MenuItem>("mnuPrintAnalysePhysiqueShipment").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintAnalysePhysiqueShipment").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{82A89CE1-2872-4FF1-83CD-83874AC17766}")))
                X.GetCmp<MenuItem>("mnuExportPhysicalAnalysis").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportPhysicalAnalysis").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{65876966-595E-40A1-801D-1AE7AACB9548}")))
                X.GetCmp<Hidden>("AphiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("AphiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{69355870-FA3B-42FC-8561-58CECBBB3553}")))
                X.GetCmp<Hidden>("AphiddenPermApprove").SetValue(true);
            else
                X.GetCmp<Hidden>("AphiddenPermApprove").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{C2B0291E-28B7-4EBA-A42C-A952C945AE41}")))
                X.GetCmp<Hidden>("AphiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("AphiddenPermDesactiver").SetValue(false);

            //X.GetCmp<Hidden>("mvhiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{94E1648B-BB69-4470-8B5D-FA8D19598161}", UserName));
            //X.GetCmp<Hidden>("mvhiddenPermGenerate").SetValue(HasAccess.fnGetUserAccessStatus("{C1785226-126D-4648-95C6-92894707BC1E}", UserName));
            //X.GetCmp<Hidden>("mvhiddenPermPrint").SetValue(HasAccess.fnGetUserAccessStatus("{DF2C998A-9232-48ED-B403-6DB46289BE0A}", UserName));
            //X.GetCmp<Hidden>("mvhiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{3CA6BD01-DFC1-4B9F-9E0E-134391DF8B4F}", UserName));
            #endregion

            return View();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("LotAnalysePhysiqueExportCP");
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

                var mListe = (new AnalysePhysiqueExport()).fnSelect(startdate, enddate, status);
                
                return this.Store(mListe);
            }
        }

        public ActionResult OnRefresh(string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store store = X.GetCmp<Store>("storeListeAnalysePhysiqueExport");
                store.Reload();
                store.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemPeriodStart"     ,ItemPeriodStart),
                                new Ext.Net.Parameter("ItemPeriodEnd"           ,ItemPeriodEnd),
                                new Ext.Net.Parameter("ItemStatus"           ,ItemStatus)
                            });

                X.GetCmp<FormPanel>("LotAnalysePhysiqueExportCP").Collapsed = true;

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Physical Analysys Export : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult onApproveAnalysis()
        {
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Mask();
            AnalysePhysiqueViewModel mclass = new AnalysePhysiqueViewModel();               

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormApproveLotAnalysePhysique", Model = mclass, };

        }

        public ActionResult onAdd()
        {          
            AnalysePhysiqueExportViewModel mclass = new AnalysePhysiqueExportViewModel();
            Parametres mParam = new Parametres(0);
            mclass._AnalysePhysiqueExport = new AnalysePhysiqueExport();
            mclass._AnalysePhysiqueExport.Lot = new Lot();
            mclass._DefaultCampagne = mParam.Campagne;
            mclass._DefaultLaboratoire = mParam.LaboExport;   
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLotAnalysePhysique", Model = mclass };
        }

        public ActionResult onEdit(string ItemSelected)
        {
            Parametres mParam = new Parametres(0);
            AnalysePhysiqueExportViewModel mclass = new AnalysePhysiqueExportViewModel();            
            mclass._AnalysePhysiqueExport = JSON.Deserialize<AnalysePhysiqueExport>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._DefaultCampagne = mParam.Campagne;
            mclass._DefaultLaboratoire = mParam.LaboExport;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLotAnalysePhysique", Model = mclass };
        }

        public ActionResult onConsult(string ItemSelected)
        {
            Parametres mParam = new Parametres(0);
            AnalysePhysiqueExportViewModel mclass = new AnalysePhysiqueExportViewModel();
            mclass._AnalysePhysiqueExport = JSON.Deserialize<AnalysePhysiqueExport>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._DefaultCampagne = mParam.Campagne;
            mclass._DefaultLaboratoire = mParam.LaboExport;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLotAnalysePhysique", Model = mclass };
        }

        public ActionResult onApprove(string ItemSelected)
        {
            Parametres mParam = new Parametres(0);
            AnalysePhysiqueExportViewModel mclass = new AnalysePhysiqueExportViewModel();
            mclass._AnalysePhysiqueExport = JSON.Deserialize<AnalysePhysiqueExport>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._DefaultCampagne = mParam.Campagne;
            mclass._DefaultLaboratoire = mParam.LaboExport;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLotAnalysePhysique", Model = mclass, };
        }

        public ActionResult OnSelectLot()
        {
            Parametres mParam = new Parametres(0);
            string campagne = mParam.Campagne;
            int exportateur = mParam.Exportateur.ID;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "AnalysePhysiqueExport_SelectLot", Model = mParam };
        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                AnalysePhysiqueExport mClass = new AnalysePhysiqueExport();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtAnalysePhysiqueExportID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Physical Analysys Export load failed.");
                }

                mClass = MapFormToObject(mClass);

                bool result = mClass.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeAnalysePhysiqueExport");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowAnalysePhysiqueExport").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormLotAnalysePhysique").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Physical analysis Export : Data Validation",
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
                AnalysePhysiqueExport analyse = new AnalysePhysiqueExport();

                analyse.ID = Guid.Parse(X.GetCmp<TextField>("TxtAnalysePhysiqueExportID").Text.ToString());                

                bool result = analyse.fnGet(analyse.ID);

                if (analyse == null || analyse.ID == Guid.Empty)
                    throw new Exception("onApprovePhysicalAnalysis : Analyse Physique Export Approve failed.");

                analyse.Approbateur = (string)Session["userName"];
                analyse.DateApprobation = DateTime.Now;
                analyse.Statut = X.GetCmp<ComboBox>("CmbResultatAnalyse").RawValue.ToString();
                analyse.Confirmation = true;
                analyse.Commentaire = X.GetCmp<TextField>("TxtCommentaireAnalysePhysique").Text;
                analyse.UtilisateurModification = (string)Session["userName"];

                result = analyse.fnApprove();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeAnalysePhysiqueExport");

                    ModelProxy mProxy = mstore.GetById(analyse.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(analyse);

                    mProxy.Commit();

                    mProxy.EndEdit();
                    X.GetCmp<Window>("FormLotAnalysePhysique").Close();                    
                    X.Js.Call("Overview.resetButtons");
                }
                else
                {
                    X.GetCmp<Window>("FormLotAnalysePhysique").Close();                    
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Analyse Physique Export : Approuver",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }


        private AnalysePhysiqueExport MapFormToObject(AnalysePhysiqueExport analyse)
        {
            analyse.Lot = new Lot();
            analyse.Lot.fnGetByNumero(X.GetCmp<TextField>("txtLot").Text);
            if (analyse.Lot.ID == Guid.Empty || analyse.Lot == null)
            {
                throw new Exception("SubmitFormMethod : Lot N° not Found !.");
            }
            //analyse.Lot.ID = Guid.Parse(X.GetCmp<ComboBox>("CmbCodeAnalyse").RawValue.ToString());
            //analyse.Lot.NumeroLot = X.GetCmp<ComboBox>("CmbCodeAnalyse").SelectedItem.Text.ToString();
            analyse.Campagne = new Campagne();
            analyse.Campagne.Designation = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

            analyse.Laboratoire = new Laboratoire();
            analyse.Laboratoire.ID = int.Parse(X.GetCmp<ComboBox>("CmbLaboratoire").RawValue.ToString());

            analyse.DateAnalyse = DateTime.Parse(X.GetCmp<DateField>("TxtDateAnalysePhysique").RawText.ToString()).Add(DateTime.Now.TimeOfDay);
            analyse.FicheNumero = X.GetCmp<TextField>("TxtSheetNumber").Text;

            analyse.NombreFeves = int.Parse(X.GetCmp<NumberField>("TxtNbrOfBeans").Text);
            analyse.Grainage = int.Parse(X.GetCmp<NumberField>("TxtBeansCount").Text);
            analyse.PoidsFeves = int.Parse(X.GetCmp<NumberField>("TxtBeansCountPc").Text);
            analyse.MoisieNbre = int.Parse(X.GetCmp<NumberField>("TxtMouldy").Text);
            analyse.PlateNbre = int.Parse(X.GetCmp<NumberField>("TxtFlat").Text);
            analyse.WeevilNbre = int.Parse(X.GetCmp<NumberField>("TxtWeevil").Text);
            analyse.GermeeNbre = int.Parse(X.GetCmp<NumberField>("TxtGerminated").Text);
            analyse.ArdoiseeNbre = int.Parse(X.GetCmp<NumberField>("TxtSlaty").Text);
            analyse.VioletteNbre = int.Parse(X.GetCmp<NumberField>("TxtViolet").Text);

            analyse.NombreFevesPc = double.Parse(X.GetCmp<NumberField>("TxtBeansCountPc").RawText);
            analyse.Moisie = double.Parse(X.GetCmp<NumberField>("TxtMouldyPr").RawText);
            analyse.Plate = double.Parse(X.GetCmp<NumberField>("TxtFlatPr").RawText);
            analyse.Weevil = double.Parse(X.GetCmp<NumberField>("TxtWeevilPr").RawText);
            analyse.Germee = double.Parse(X.GetCmp<NumberField>("TxtGerminatedPr").RawText);
            analyse.Ardoisee = double.Parse(X.GetCmp<NumberField>("TxtSlatyPr").RawText);
            analyse.Violette = double.Parse(X.GetCmp<NumberField>("TxtVioletPr").RawText);
            analyse.Defectueuse = double.Parse(X.GetCmp<NumberField>("TxtDefectives").RawText);
            analyse.Fermentation = double.Parse(X.GetCmp<NumberField>("TxtUnFermented").RawText);
            analyse.Tamis = double.Parse(X.GetCmp<NumberField>("TxtSieving").RawText);
            analyse.Fragment = double.Parse(X.GetCmp<NumberField>("TxtFragment").RawText);
            analyse.Brisure = double.Parse(X.GetCmp<NumberField>("TxtBrokenBean").RawText);
            analyse.Humidite = double.Parse(X.GetCmp<NumberField>("TxtMoisture").RawText);
            analyse.LightCrop = double.Parse(X.GetCmp<NumberField>("txtLightCrop").RawText);
            analyse.MatiereEtrangere = double.Parse(X.GetCmp<NumberField>("TxtForeignMatter").RawText);
            
            analyse.BeansCluster = double.Parse(X.GetCmp<NumberField>("txtBeanCluster").RawText);
            analyse.CRM = double.Parse(X.GetCmp<NumberField>("txtCrm").RawText);
            analyse.MatiereEtrangere = double.Parse(X.GetCmp<NumberField>("TxtForeignMatter").RawText);
            analyse.MatiereEtrangere = double.Parse(X.GetCmp<NumberField>("TxtForeignMatter").RawText);
            if (string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtFfa").RawText))
                analyse.Ffa = (double?)null;
            else
                analyse.Ffa = double.Parse(X.GetCmp<NumberField>("TxtFfa").RawText);

            analyse.Commentaire = X.GetCmp<TextField>("TxtCommentaireAnalysePhysique").Text;
            analyse.Confirmation = false;
            analyse.Statut = X.GetCmp<ComboBox>("CmbResultatAnalyse").RawValue.ToString();
            analyse.ClassificationFeves = new ClassificationFeves();
            analyse.ClassificationFeves.ID = int.Parse(X.GetCmp<ComboBox>("CmbClassification").RawValue.ToString());
            analyse.ClassificationFeves.Designation = X.GetCmp<ComboBox>("CmbClassification").RawText;

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

        public ActionResult OnRefreshLot(string ItemCampagne, string ItemExportateur, string ItemType, string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeSelectLot");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCampagne",ItemCampagne),
                                    new Ext.Net.Parameter("ItemExportateur",ItemExportateur),
                                    new Ext.Net.Parameter("ItemType",ItemType),
                                    new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemCertification",ItemCertification)
                                });
                //X.GetCmp<FormPanel>("SelectLotCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - Lot : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitLot(string ItemSelected)
        {
            Lot mclass = new Lot();
            mclass = JSON.Deserialize<Lot>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            X.GetCmp<Hidden>("txtLotID").SetValue(mclass.ID);
            X.GetCmp<Hidden>("txtLot").SetValue(mclass.NumeroLot);;
            X.GetCmp<Window>("AnalysePhysiqueExport_SelectLot").Close();

            return this.Direct();
        }

        public ActionResult OnDisplayAnalysePhysiqueCriteria()
        {

            ViewData["Titre"] = "Print Analyse Physique - Lot";
            ViewData["actionToDo"] = "OnPrintAnalysePhysiqueLotList";
            ViewData["ControllerName"] = "LotAnalysePhysique";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmCriteriaForPhysicalAnalysis", ViewData = ViewData };
        }

        public ActionResult OnPrintAnalysePhysiqueLotList(string startDate, string endDate, string statut, string statutText)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                rptAnalysePhysiqueLot report = new rptAnalysePhysiqueLot();
                report.DataSource = DevExpressReportDs.SetDataSource(report);

                report.Parameters["paramDateDebut"].Value = dateDebut;
                report.Parameters["paramDateFin"].Value = dateFin;
                report.Parameters["paramStatut"].Value = int.Parse(statut);
                report.Parameters["paramStatutText"].Value = statutText;

                Session["report"] = report;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/LotAnalysePhysique/ViewReportListResult', this, 'List Of Analyse Physique - Lot',''),App.frmCriteriaForPhysicalAnalysis.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Analyse Physique - Lot : Data Validation",
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

        private void DeselectGridRows()
        {
            //X.Js.Call("App.rowSelectionListe.deselectAll();");
            X.GetCmp<RowSelectionModel>("rowSelectionDailyPrice").DeselectAll();
        }

        public void CreateIconsList()
        {
            try
            {
                //Ext.Net.ResourceManager.RegisterGlobalIcon(Icon.PageCancel);

                //string url =  Ext.Net.ResourceManager.GetInstance().GetIconUrl(Icon.Tick);
            }
            catch (Exception ex)
            {

                X.Msg.Alert("frmLBC_ReceptionInDistrict_Overview : CreateIconList", ex.Message).Show();
            }
        }
        #endregion


    }
}