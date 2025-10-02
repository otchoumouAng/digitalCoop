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
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class SampleAnalyseChimiqueController : BaseController
    {
        // GET: SampleAnalyseChimique

        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);

            X.GetCmp<ComboBox>("cmbFiltreCampagne").SetValue(mParam.Campagne);
            X.GetCmp<ComboBox>("cmbFiltreExportateur").SetValue(mParam.Exportateur.ID);

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("TxtPeriodStart").RawText = StartDate;
            X.GetCmp<DateField>("TxtPeriodEnd").RawText = EndDate;

            X.GetCmp<FormPanel>("SampleAnalyseChimiqueCP").SetTitle("Campagne :" + mParam.Campagne + ", Exportateur : "+ mParam.Exportateur.Nom + ", Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{D3D9EF90-E0B3-4843-BA71-1E8A9AF73083}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{74554DAC-E618-4D3A-9600-7DEF06C69F2E}")))
                X.GetCmp<Button>("btnNewSample").Enable();
            else
                X.GetCmp<Button>("btnNewSample").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{7A0D5F12-DD5D-49F6-BCA1-B1410455B8A3}")))
                X.GetCmp<MenuItem>("mnuPrintSampleList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintSampleList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{E59EA685-1D3F-4F0E-BD5B-8F165C4037B5}")))
                X.GetCmp<MenuItem>("mnuExportPhysicalAnalysis").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportPhysicalAnalysis").Disable();           

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{A4D9806D-FD76-423F-A184-2CCEFB2C15A9}")))
                X.GetCmp<Hidden>("SmhiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("SmhiddenPermModifier").SetValue(false);            

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{C4766C42-41FB-40A3-B65B-8E364566A6EE}")))
                X.GetCmp<Hidden>("SmhiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("SmhiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{41B08E98-8F07-47F3-A497-936CBA28B772}")))
                X.GetCmp<Hidden>("SmhiddenPermPrintSampleSheet").SetValue(true);
            else
                X.GetCmp<Hidden>("SmhiddenPermPrintSampleSheet").SetValue(false);

            //X.GetCmp<Hidden>("mvhiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{94E1648B-BB69-4470-8B5D-FA8D19598161}", UserName));
            //X.GetCmp<Hidden>("mvhiddenPermGenerate").SetValue(HasAccess.fnGetUserAccessStatus("{C1785226-126D-4648-95C6-92894707BC1E}", UserName));
            //X.GetCmp<Hidden>("mvhiddenPermPrint").SetValue(HasAccess.fnGetUserAccessStatus("{DF2C998A-9232-48ED-B403-6DB46289BE0A}", UserName));
            //X.GetCmp<Hidden>("mvhiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{3CA6BD01-DFC1-4B9F-9E0E-134391DF8B4F}", UserName));
            #endregion

            return View();
        }

        [HttpPost]
        public ActionResult LoadSamplesCode(string query)
        {
            try
            {
                AnalyseChimiqueEchantillon analyse = new AnalyseChimiqueEchantillon();
                List<DataPersist> mList = analyse.fnGetByCode(query);
                if (mList.Count > 0)
                    analyse = mList[0] as AnalyseChimiqueEchantillon;

                return this.Store(mList);
            }
            catch (Exception Ex)
            {
                throw;
            }
            
        }
        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("SampleAnalyseChimiqueCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne,string ItemExportateur, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatut)
        {
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "-1" : ItemCampagne;            
            int ExportateurID = GetCriteriaValue(ItemExportateur);
            
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            int statut = string.IsNullOrEmpty(ItemStatut) ? 0 : int.Parse(ItemStatut);

            var mListe = (new AnalyseChimiqueEchantillon()).fnSelect(ItemCampagne, ExportateurID, StartDate, EndDate, statut);

            return this.Store(mListe);
        }

        public ActionResult GetSampleLots(StoreRequestParameters parameters, string ItemEchantillon)
        {
            Guid EchantillonID = !string.IsNullOrEmpty(ItemEchantillon) ? Guid.Parse(ItemEchantillon) : Guid.Empty;

            var mListe = (new AnalyseChimiqueEchantillonLot()).fnSelect(EchantillonID);

            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemExportateur, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatut)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeEchantillon");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCampagne",ItemCampagne),                                    
                                    new Ext.Net.Parameter("ItemExportateur",ItemExportateur),                                                                        
                                    new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatut",ItemStatut)
                                });

                X.GetCmp<FormPanel>("SampleAnalyseChimiqueCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sample Chemical Analysis : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        private string GenerateCode()
        {
            AnalyseCodeViewModel analyseCodeVm = new AnalyseCodeViewModel();
            analyseCodeVm._AnalyseCode = new AnalyseCode();
            string mNewCode = string.Empty;
            Guid mRndCode = Guid.NewGuid();
            string mResultCode = mRndCode.ToString();

            //code is generated from guid "7fa9bdbc-5a60-48ca-9cd5-9f58054f404a"
            //generate 5 digit code

            if (mResultCode != " ")
            {
                //Dim mCode1 As String = Microsoft.VisualBasic.Left(mResultCode, 8)
                //mCode1 = Microsoft.VisualBasic.Mid(mCode1, 4, 1)

                //Dim mCode2 As String = Microsoft.VisualBasic.Mid(mResultCode, 10, 4)
                //mCode2 = Microsoft.VisualBasic.Mid(mCode2, 2, 1)

                string code1 = mResultCode.PadLeft(8);
                code1 = code1.Substring(4, 1).ToUpper();

                string code2 = mResultCode.Substring(10, 4);
                code2 = code2.Substring(2, 1).ToUpper();

                string code3 = mResultCode.Substring(15, 4);
                code3 = code3.Substring(2, 1).ToUpper();

                string code4 = mResultCode.Substring(20, 4);
                code4 = code4.Substring(2, 1).ToUpper();

                string code5 = mResultCode.Substring(25, 8);
                code5 = code5.Substring(2, 1).ToUpper();

                mNewCode = "S" + code1 + code2 + code3 + code4 + code5;                
            }
            return mNewCode;
        }


        public ActionResult onAdd()
        {            
            AnalyseChimiqueEchantillonViewModel mclass = new AnalyseChimiqueEchantillonViewModel();

            mclass._AnalyseChimiqueEchantillon = new AnalyseChimiqueEchantillon();
            Parametres mParam = new Parametres(0);
            mclass._DefaultCampagne = mParam.Campagne;
            mclass._DefaultExportateur = mParam.Exportateur.ID;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormSampleAnalysePhysique", Model = mclass, };
        }

        public ActionResult onEdit(string ItemSelected)
        {
            AnalyseChimiqueEchantillonViewModel mclass = new AnalyseChimiqueEchantillonViewModel();            

            mclass._AnalyseChimiqueEchantillon = JSON.Deserialize<AnalyseChimiqueEchantillon>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            Parametres mParam = new Parametres(0);
            mclass._DefaultCampagne = mParam.Campagne;
            mclass._DefaultExportateur = mParam.Exportateur.ID;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormSampleAnalysePhysique", Model = mclass, };
        }

        public ActionResult onConsult(string ItemSelected)
        {
            AnalyseChimiqueEchantillonViewModel mclass = new AnalyseChimiqueEchantillonViewModel();

            mclass._AnalyseChimiqueEchantillon = JSON.Deserialize<AnalyseChimiqueEchantillon>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            Parametres mParam = new Parametres(0);
            mclass._DefaultCampagne = mParam.Campagne;
            mclass._DefaultExportateur = mParam.Exportateur.ID;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormSampleAnalysePhysique", Model = mclass, };
        }

        public ActionResult SubmitFormMethod(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                AnalyseChimiqueEchantillon mEchantillon = new AnalyseChimiqueEchantillon();
                AnalyseChimiqueEchantillonLot mLot;
                string NumeroLots = "";
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mEchantillon.IsNew = true;
                else
                {
                    mEchantillon.IsNew = false;

                    mEchantillon.fnGet(Guid.Parse(GetFormValue("TxtEchantillonID")));

                    if (mEchantillon == null || mEchantillon.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Sample load failed.");
                }

                bool result = false;
                bool resultLot = true;
                mEchantillon = MapFormToObject(mEchantillon);

                _db = mEchantillon.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = mEchantillon.fnUpdate(mtran);

                if (result)
                {
                    List<AnalyseChimiqueEchantillonLot> item = JSON.Deserialize<List<AnalyseChimiqueEchantillonLot>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (item.Count > 0)
                    {

                        for (int i = 0; i < item.Count(); i++)
                        {
                            mLot = new AnalyseChimiqueEchantillonLot();
                            mLot.Echantillon = new AnalyseChimiqueEchantillon();
                            mLot.Lot = new Lot();
                            mLot.SetDataSource(_db);

                            mLot.Echantillon.ID = mEchantillon.ID;
                            mLot.Lot.ID = item[i].Lot.ID;                            
                            mLot.IsNew = item[i].IsNew;
                            NumeroLots += (item[i].Lot.NumeroLot + ", ");
                            mLot.UtilisateurCreation = (string)Session["userName"];
                            mLot.UtilisateurModification = (string)Session["userName"];
                            if (mLot.IsNew)
                            {
                                resultLot = mLot.fnUpdate(mtran);
                            }

                            if (!resultLot)
                            {
                                break;
                            }
                        }
                    }
                    if (!resultLot)
                    {
                        _db.RollBackTransaction(mtran);
                    }
                    _db.CommitTransaction(mtran);
                }


                if (result)
                {
                    mEchantillon.NumeroLots = NumeroLots;
                    Store mstore = X.GetCmp<Store>("storeListeEchantillon");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {                        
                        mstore.Insert(0, mEchantillon);
                        X.GetCmp<RowSelectionModel>("rowEchantillon").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mEchantillon.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mEchantillon);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormSampleAnalysePhysique").Close();

                    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                    X.Js.Call("GenerateSample", mEchantillon.ID, BaseUrl);

                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sample Chemical Analysis : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        private AnalyseChimiqueEchantillon MapFormToObject(AnalyseChimiqueEchantillon mClass)
        {            
            Campagne campagne = new Campagne();
            campagne.Designation = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text.ToString();
            mClass.Campagne = campagne;

            Exportateur exportateur = new Exportateur();
            exportateur.ID = int.Parse(GetFormValue("cmbExportateur"));
            exportateur.Nom = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text.ToString();
            mClass.Exportateur = exportateur;

            mClass.DateEchantillon = DateTime.Parse(X.GetCmp<DateField>("txtDateEchantillon").RawText.ToString()).Add(DateTime.Now.TimeOfDay);
            mClass.NumeroEchantillon = GenerateCode();
           
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }

        public ActionResult OnRefreshLot(string ItemCampagne, string ItemExportateur, string ItemType, string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd, string ItemLots = "")
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
                                    new Ext.Net.Parameter("ItemCertification",ItemCertification),
                                    new Ext.Net.Parameter("ItemLots",ItemLots)
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


        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                AnalyseChimiqueEchantillon analyse = JSON.Deserialize<AnalyseChimiqueEchantillon>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = analyse.fnGet(analyse.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Sample Chemical Analysis loading failed.");

                analyse.UtilisateurModification = (string)Session["userName"];

                if (analyse.Desactive)
                    result = analyse.fnActivate();
                else
                    result = analyse.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Sample Chemical Analysis, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeEchantillon");

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
                    Title = "Sample Chemical Analysis : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult OnSelectLot(string LotsAdded = "")
        {
            List<AnalyseChimiqueEchantillonLot> ListeLot = new List<AnalyseChimiqueEchantillonLot>();

            ListeLot = JSON.Deserialize<List<AnalyseChimiqueEchantillonLot>>(LotsAdded, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            string delimiter = ",";
            //LotsAdded = "";
            if (!string.IsNullOrEmpty(LotsAdded) && LotsAdded != "[]")
                LotsAdded = ListeLot.Select(i => i.NumeroLot).Aggregate((i, j) => i + delimiter + j);

            ViewData["LotsAdded"] = LotsAdded;

            Parametres mParam = new Parametres(0);

            string campagne = mParam.Campagne;
            int exportateur = mParam.Exportateur.ID;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Sample_SelectLot", Model = mParam, ViewData = ViewData };
        }

        public ActionResult SubmitLot(string ItemSelected, string ListeLots = "")
        {
            Parametres mParam = new Parametres(0);
            List<Lot> ListeLot = new List<Lot>();
            List<AnalyseChimiqueEchantillonLot> LotsAdded = new List<AnalyseChimiqueEchantillonLot>();
            ListeLot = JSON.Deserialize<List<Lot>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            LotsAdded = JSON.Deserialize<List<AnalyseChimiqueEchantillonLot>>(ListeLots, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            int NbreLotsInList = 0;
            if (LotsAdded != null) NbreLotsInList = LotsAdded.Count;            

            if ((NbreLotsInList + ListeLot.Count) > mParam.NbrOfLotsForChemicalAnalysis)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sample Sheet : Chemical Analysis",
                    Message = "Number Of Lots Is higher than Authorized Number of Lots For Analysis !",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
            else
            {

                AnalyseChimiqueEchantillonLot mClass;
                List<AnalyseChimiqueEchantillonLot> List = new List<AnalyseChimiqueEchantillonLot>();
                foreach (Lot item in ListeLot)
                {
                    mClass = new AnalyseChimiqueEchantillonLot();
                    mClass.ID = Guid.NewGuid();
                    mClass.Lot = new Lot();
                    mClass.Lot.ID = item.ID;
                    mClass.Lot.NombreSacs = item.NombreSacs;
                    mClass.Lot.NumeroLot = item.NumeroLot;
                    mClass.IsNew = true;
                    mClass.IsNewInList = true;
                    List.Add(mClass);
                }
                Store mstore = X.GetCmp<Store>("storeListeSelectedLots");

                mstore.Add(List);
                X.GetCmp<Window>("Sample_SelectLot").Close();
            }
            

            return this.Direct();
        }

        public ActionResult RemoveLot(string ItemSelected)
        {
            try
            {
                AnalyseChimiqueEchantillonLot mLot = JSON.Deserialize<AnalyseChimiqueEchantillonLot>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                if (mLot.ID != Guid.Empty)
                {
                    Store mstore = X.GetCmp<Store>("storeListeSelectedLots");

                    if (mLot.IsNew)
                    {
                        ModelProxy _proxy = mstore.GetById(mLot.ID);
                        _proxy.Drop();
                        return this.Direct();
                    }
                    else
                    {
                        //mPalette = new PeseeProductionPalette();
                        mLot.fnGet(mLot.ID);
                        if (mLot == null || mLot.ID == Guid.Empty)
                            throw new Exception("RemoveLot : Retirer Lot failed.");

                        mLot.UtilisateurModification = (string)Session["userName"];

                        bool result = mLot.fnRemove();

                        if (result)
                        {
                            ModelProxy mProxy = mstore.GetById(mLot.ID);
                            mProxy.Drop();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sample Sheet : Chemical Analysis",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult OnPrintSample(string Sample)
        {
            string BaseUrl = "";
            Guid EchantillonID = Guid.Empty;
            try
            {
                
                bool IsGuid = Guid.TryParse(Sample, out EchantillonID);
                if (!IsGuid)
                    return this.Direct();

                BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            }
            catch (Exception)
            {

                throw;
            }

            if (string.IsNullOrEmpty(Sample))
                throw new Exception("Fiche N° : Operation failed.");
             
            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Sample{0}', '{1}/SampleAnalyseChimique/ViewSampleCode?Sample={2}', this, 'Sample Fiche N°','')", EchantillonID, BaseUrl, EchantillonID));
        }

        public ActionResult ViewSampleCode(Guid Sample)
        {
            try
            {
                rptFicheAnalyseChimique rpt = new rptFicheAnalyseChimique();

                rpt.Parameters["echantillonID"].Value = Sample;
                ViewData["Report"] = rpt;                
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sample Sheet : Chemical Analysis",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return View("ViewReportResult", ViewData = ViewData);
        }

        public ActionResult OnPrintSampleList()
        {            
            Parametres mParam = new Parametres(0);

            ViewData["Campagne"] = mParam.Campagne;
            ViewData["Exportateur"] = mParam.Exportateur.ID;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "SampleAnalyseChimique_Print", ViewData = ViewData };
        }

        public ActionResult PrintListOfSample()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {               
                XtraReport report = null;
                report = new rptEchantillonLotsList() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["CampagneID"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                report.Parameters["paramExportateur"].Value = int.Parse(X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Value);
                report.Parameters["paramExportateurNom"].Value = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text;

                report.Parameters["paramStatut"].Value = int.Parse(X.GetCmp<ComboBox>("CmbStatut").SelectedItem.Value);
                report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("CmbStatut").SelectedItem.Text;

                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("TxtPeriodStart").RawText);
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("TxtPeriodEnd").RawText);

                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/SampleAnalyseChimique/ViewList', this, 'Sample Chemical Analysis',''),App.SampleAnalyseChimique_Print.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Sample Chemical Analysis : Data Validation",
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

            return View("ViewReportResult");
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

        private void CloseDetailWindow()
        {
            X.GetCmp<Window>("FormMouvementStock").Close();
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

    }
}