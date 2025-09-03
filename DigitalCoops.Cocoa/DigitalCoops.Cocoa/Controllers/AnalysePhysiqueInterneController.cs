using CrystalDecisions.CrystalReports.Engine;
using DevExpress.XtraReports.UI;
using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Business;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Components.Data;
using Tms.Components.Reports;
using Tms.Components.Settings;
using Tms2017.MVC.Models;
using Tms2017.MVC.Reports;
using Tms2017.Reports;

namespace Tms2017.MVC.Controllers
{
    public class AnalysePhysiqueInterneController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: AnalysePhysique
        public ActionResult Index()
        {
            X.GetCmp<DateField>("TxtPeriodStart").RawText = DateTime.Now.ToShortDateString();
            X.GetCmp<DateField>("TxtPeriodEnd").RawText = DateTime.Now.ToShortDateString();

            X.GetCmp<FormPanel>("AnalysePhysiqueCriteriaPanel").SetTitle("Today");

            Site mSiteParDefaut = new Site();
            string UserName = (string)Session["userName"];
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            #region Set Function's Access
            
            Fonction HasAccess = new Fonction();

            if (HasAccess.fnGetUserAccessStatus("{0669beb5-984b-44b9-90f4-69d04eeabf26}", UserName) == false)
                X.GetCmp<Button>("btnNewAnalysePhysique").Disable();
            else
                X.GetCmp<Button>("btnNewAnalysePhysique").Enable();

            if (HasAccess.fnGetUserAccessStatus("{0c0a91a4-c224-4e07-8c3e-5a871b83510f}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportPhysicalAnalysis").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportPhysicalAnalysis").Enable();
            
            //if (HasAccess.fnGetUserAccessStatus("{95D47DD6-59A2-4F5F-91DE-D6A623804D73}", UserName) == false)
            //    X.GetCmp<MenuItem>("mnuPrintQualityAverage").Disable();
            //else
            //    X.GetCmp<MenuItem>("mnuPrintQualityAverage").Enable();

            if (HasAccess.fnGetUserAccessStatus("{b6aa1cff-b78a-48c5-a238-640678ee0208}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintAnalysePhysiqueList").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintAnalysePhysiqueList").Enable();

            X.GetCmp<Hidden>("AphiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{0669beb5-984b-44b9-90f4-69d04eeabf26}", UserName));
            X.GetCmp<Hidden>("AphiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{221bcdd7-ea9b-45d1-a961-4a3f197f3d33}", UserName));
            X.GetCmp<Hidden>("AphiddenPermApprove").SetValue(HasAccess.fnGetUserAccessStatus("{cea11585-2ca3-40c6-ba99-99859710cdbf}", UserName));
            X.GetCmp<Hidden>("AphiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{01a4a4fb-3bab-44d8-b125-fc883318d539}", UserName));
            X.GetCmp<Hidden>("AphiddenPermPrintQualityAverage").SetValue(HasAccess.fnGetUserAccessStatus("{95D47DD6-59A2-4F5F-91DE-D6A623804D73}", UserName));
            X.GetCmp<Hidden>("AphiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("0c0a91a4-c224-4e07-8c3e-5a871b83510f", UserName));
            X.GetCmp<Hidden>("AphiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{7bbafa50-f39e-4ef3-8ec6-3d8493b748b3}", UserName));
            X.GetCmp<Hidden>("AphiddenPermSuperDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{e2a1bb6d-0e65-496b-a8bc-fbd0e0b4b2a9}", UserName));

            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{33F1DFFE-1E83-45DD-A194-2CE3C759C297}", UserName);

            #endregion

            return View();
        }

        public ActionResult OnAdd()
        {            

            Parametres mclass = new Parametres(0);                          

            AnalysePhysiqueViewModel analysephysiqueVM = new AnalysePhysiqueViewModel();

            analysephysiqueVM._AnalysePhysique = new AnalysePhysique();
            analysephysiqueVM._AnalysePhysique.Laboratoire = new Laboratoire();
            analysephysiqueVM._AnalysePhysique.Laboratoire.ID = mclass.Laboratoire.ID;
            analysephysiqueVM._AnalysePhysique.Sites = new Site();

            analysephysiqueVM._Parametres = mclass;

            Site mSiteParDefaut = new Site();
            string UserName = (string)Session["userName"];
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            if (result)
            {
                analysephysiqueVM._AnalysePhysique.Sites.ID = mSiteParDefaut.ID;
                analysephysiqueVM._AnalysePhysique.Sites.Nom = mSiteParDefaut.Nom;
            }

            analysephysiqueVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;                      

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAnalysePhysique", Model = analysephysiqueVM, };
            
        }

        public ActionResult OnEdit(string ItemSelected)
        {            

            AnalysePhysique analysephysique = JSON.Deserialize<AnalysePhysique>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            AnalysePhysiqueViewModel analysephysiqueVM = new AnalysePhysiqueViewModel();

            analysephysiqueVM._AnalysePhysique = analysephysique;

            analysephysiqueVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAnalysePhysique", Model = analysephysiqueVM, };
            
        }

        public ActionResult OnConsult(string ItemSelected)
        {            

            AnalysePhysique analysephysique = JSON.Deserialize<AnalysePhysique>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            AnalysePhysiqueViewModel analysephysiqueVM = new AnalysePhysiqueViewModel();

            analysephysiqueVM._AnalysePhysique = analysephysique;

            analysephysiqueVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAnalysePhysique", Model = analysephysiqueVM, };

        }

        public ActionResult onApprove(string ItemSelected)
        {            

            AnalysePhysique analysephysique = JSON.Deserialize<AnalysePhysique>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            AnalysePhysiqueViewModel analysephysiqueVM = new AnalysePhysiqueViewModel();

            analysephysiqueVM._AnalysePhysique = analysephysique;

            analysephysiqueVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAnalysePhysique", Model = analysephysiqueVM, };
        }

        public ActionResult SubmitAnalyeApprove()
        {
            try
            {
                AnalysePhysique analyse = new AnalysePhysique();

                analyse.ID = Guid.Parse(X.GetCmp<TextField>("TxtAnalysePhysiqueID").Text.ToString());
                analyse.AnalyseCode = new AnalyseCode();
                analyse.AnalyseCode.ID = Guid.Parse(X.GetCmp<Hidden>("HiddenCodeAnalyseID").Text.ToString());

                bool result = analyse.fnGet(analyse.ID);

                if (analyse == null || analyse.ID == Guid.Empty)
                    throw new Exception("onApprovePhysicalAnalysis : Analyse Physique Approve failed.");

                analyse.Approbateur = (string)Session["userName"];
                analyse.DateApprobation = DateTime.Now;
                analyse.Statut = X.GetCmp<ComboBox>("CmbResultatAnalyse").RawValue.ToString();
                analyse.Confirmation = true;
                analyse.Commentaire = X.GetCmp<TextField>("TxtCommentaireAnalysePhysique").Text;
                analyse.UtilisateurModification = (string)Session["userName"];

                result = analyse.fnApprove();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeAnalysePhysique");

                    ModelProxy mProxy = mstore.GetById(analyse.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(analyse);

                    mProxy.Commit();

                    mProxy.EndEdit();
                    X.GetCmp<Window>("FormAnalysePhysique").Close();                    
                    X.Js.Call("Overview.resetButtons");
                }
                else
                {
                    X.GetCmp<Window>("FormAnalysePhysique").Close();                    
                }                                

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Analyse Physique : Approuver",
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
                AnalysePhysique analysephysique = JSON.Deserialize<AnalysePhysique>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = analysephysique.fnGet(analysephysique.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Analyse Physique loading failed.");

                analysephysique.UtilisateurModification = (string)Session["userName"];

                if (analysephysique.Desactive)
                    result = analysephysique.fnActivate();
                else
                    result = analysephysique.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Analyse Physique, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeAnalysePhysique");

                    ModelProxy mProxy = mstore.GetById(analysephysique.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(analysephysique);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Analyse Physique : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult OnFilter()
        {
            X.GetCmp<FormPanel>("AnalysePhysiqueCriteriaPanel").ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store store = X.GetCmp<Store>("storeListeAnalysePhysique");
                store.Reload();
                store.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemPeriodStart"     ,ItemPeriodStart),
                                new Ext.Net.Parameter("ItemPeriodEnd"           ,ItemPeriodEnd),
                                new Ext.Net.Parameter("ItemStatus"           ,ItemStatus),
                                new Ext.Net.Parameter("ItemSite"           ,ItemSite)
                            });

                X.GetCmp<FormPanel>("AnalysePhysiqueCriteriaPanel").Collapsed = true;

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Physical Analysys : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite)
        {
            if (string.IsNullOrEmpty(ItemPeriodStart) && string.IsNullOrEmpty(ItemPeriodEnd) && string.IsNullOrEmpty(ItemStatus))
            {
                return this.Store(null);
            }
            else
            {                
                DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
                DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
                int siteID = GetCritriaValue(ItemSite);

                //string status = string.Empty;
                int status = -1;
                if (!string.IsNullOrEmpty(ItemStatus))
                {
                    status = int.Parse(ItemStatus);
                }
                
                var mListe = (new AnalysePhysique()).fnSelect_Interne("{Tous}",siteID,-1,-1,startdate, enddate, status);

                //// Paging
                //int start = parameters.Start;

                //int limit = parameters.Limit;

                //if ((start + limit) > mListe.Count)
                //{
                //    limit = mListe.Count - start;
                //}

                //string filterHeaders = this.Request.Params["filterheader"];
                //var paging = GridStorePaging.SetRangePlants(parameters, mListe);
                //return this.Store(GridStorePaging.SetRangePlants(parameters, mListe, filterHeaders));
                return this.Store(mListe);

            }
        }

        public ActionResult LoadListOfAnalysisForFinalizing(string ItemLivraison, string ItemExecMode)
        {
            List<DataPersist> myList = new List<DataPersist>();
            if (!string.IsNullOrEmpty(ItemLivraison))
            {

                if (ItemExecMode == "AddNew")
                {
                    myList = new AnalysePhysique().fnSelectByDelivery(new Guid());
                }
                else
                {
                    myList = new AnalysePhysique().fnSelectByDelivery(Guid.Parse(ItemLivraison));
                }

                AnalysePhysique mClass = new AnalysePhysique();
                if (myList.Count > 0)
                    mClass = myList[0] as AnalysePhysique;
                else myList = new List<DataPersist>();

            }
            else
            {
                Store mstore = X.GetCmp<Store>("storeListAnalysis");
                mstore.RemoveAll();
                myList = null;
            }
            return this.Store(myList);
        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                AnalysePhysique mClass = new AnalysePhysique();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtAnalysePhysiqueID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Physical Analysys load failed.");
                }

                mClass = MapFormToObject(mClass);
                
                bool result = mClass.fnUpdate();
                
                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeAnalysePhysique");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionAnalysePhysique").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormAnalysePhysique").Close();                    
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Physical analysis : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        private AnalysePhysique MapFormToObject(AnalysePhysique analysephysique)
        {
            analysephysique.Sites = new Site();
            analysephysique.Sites.ID = int.Parse(GetFormValue("txtSiteID"));
            analysephysique.Sites.Nom = X.GetCmp<ComboBox>("txtNomSite").Text;

            analysephysique.AnalyseCode = new AnalyseCode();
            analysephysique.AnalyseCode.fnGetByCode(X.GetCmp<ComboBox>("CmbCodeAnalyse").SelectedItem.Text.ToString());
            analysephysique.AnalyseCode.ID = Guid.Parse(X.GetCmp<ComboBox>("CmbCodeAnalyse").RawValue.ToString());
            analysephysique.AnalyseCode.Code = X.GetCmp<ComboBox>("CmbCodeAnalyse").SelectedItem.Text.ToString();
            
            analysephysique.Laboratoire = new Laboratoire();
            analysephysique.Laboratoire.ID = int.Parse(X.GetCmp<ComboBox>("CmbLaboratoire").RawValue.ToString());

            analysephysique.DateAnalyse = DateTime.Parse(X.GetCmp<DateField>("TxtDateAnalysePhysique").RawText.ToString()).Add(DateTime.Now.TimeOfDay);
            analysephysique.FicheNumero = X.GetCmp<TextField>("TxtSheetNumber").Text;

            analysephysique.NombreFeves =  int.Parse(X.GetCmp<NumberField>("TxtNbrOfBeans").Text);
            analysephysique.Grainage = int.Parse(X.GetCmp<NumberField>("TxtBeansCount").Text);
            analysephysique.MoisieNbre = int.Parse(X.GetCmp<NumberField>("TxtMouldy").Text);
            analysephysique.PlateNbre = int.Parse(X.GetCmp<NumberField>("TxtFlat").Text);
            analysephysique.WeevilNbre = int.Parse(X.GetCmp<NumberField>("TxtWeevil").Text);
            analysephysique.GermeeNbre = int.Parse(X.GetCmp<NumberField>("TxtGerminated").Text);
            analysephysique.ArdoiseeNbre = int.Parse(X.GetCmp<NumberField>("TxtSlaty").Text);
            analysephysique.VioletteNbre = int.Parse(X.GetCmp<NumberField>("TxtViolet").Text);
            analysephysique.NombreFevesPc = double.Parse(X.GetCmp<NumberField>("TxtBeansCountPc").RawText);
            analysephysique.Moisie = double.Parse(X.GetCmp<NumberField>("TxtMouldyPr").RawText);
            analysephysique.Plate = double.Parse(X.GetCmp<NumberField>("TxtFlatPr").RawText);
            analysephysique.Weevil = double.Parse(X.GetCmp<NumberField>("TxtWeevilPr").RawText);
            analysephysique.Germee = double.Parse(X.GetCmp<NumberField>("TxtGerminatedPr").RawText);
            analysephysique.Ardoisee = double.Parse(X.GetCmp<NumberField>("TxtSlatyPr").RawText);
            analysephysique.Violette = double.Parse(X.GetCmp<NumberField>("TxtVioletPr").RawText);
            analysephysique.Defectueuse = double.Parse(X.GetCmp<NumberField>("TxtDefectives").RawText);
            analysephysique.Fermentation = double.Parse(X.GetCmp<NumberField>("TxtUnFermented").RawText);
            analysephysique.Tamis = double.Parse(X.GetCmp<NumberField>("TxtSieving").RawText);
            analysephysique.Fragment = double.Parse(X.GetCmp<NumberField>("TxtFragment").RawText);
            analysephysique.Brisure = double.Parse(X.GetCmp<NumberField>("TxtBrokenBean").RawText);
            analysephysique.Humidite = double.Parse(X.GetCmp<NumberField>("TxtMoisture").RawText);
            analysephysique.MatiereEtrangere = double.Parse(X.GetCmp<NumberField>("TxtForeignMatter").RawText);
            
            if (string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtFfa").RawText))
                analysephysique.Ffa = (double?)null;
            else
                analysephysique.Ffa = double.Parse(X.GetCmp<NumberField>("TxtFfa").RawText);

            if (string.IsNullOrEmpty(X.GetCmp<NumberField>("txtBeanCluster").RawText))
                analysephysique.BeansCluster = (double?)null;
            else
                analysephysique.BeansCluster = double.Parse(X.GetCmp<NumberField>("txtBeanCluster").RawText);

            analysephysique.Commentaire = X.GetCmp<TextField>("TxtCommentaireAnalysePhysique").Text;
            analysephysique.Confirmation = false;
            analysephysique.Statut = X.GetCmp<ComboBox>("CmbResultatAnalyse").RawValue.ToString();
            analysephysique.ClassificationFeves = new ClassificationFeves();
            analysephysique.ClassificationFeves.ID = int.Parse(X.GetCmp<ComboBox>("CmbClassification").RawValue.ToString());
            analysephysique.ClassificationFeves.Designation = X.GetCmp<ComboBox>("CmbClassification").RawText;            

            analysephysique.Analyseur = null;
            if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbAnalyseur").Text))
            {
                analysephysique.Analyseur = new Analyseur();
                analysephysique.Analyseur.ID = int.Parse(X.GetCmp<ComboBox>("CmbAnalyseur").RawValue.ToString());
                analysephysique.Analyseur.Nom = X.GetCmp<ComboBox>("CmbAnalyseur").RawText;
            }

            analysephysique.Verificateur = null;
            if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbSuperviseur").Text))
            {
                analysephysique.Verificateur = new Analyseur();
                analysephysique.Verificateur.ID = int.Parse(X.GetCmp<ComboBox>("CmbSuperviseur").RawValue.ToString());
                analysephysique.Verificateur.Nom = X.GetCmp<ComboBox>("CmbSuperviseur").RawText;
            }
                        
            analysephysique.UtilisateurCreation = (string)Session["userName"];
            analysephysique.UtilisateurModification = (string)Session["userName"];

            return analysephysique;
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

        public ActionResult OnDisplayQualityAverageCriteria()
        {
            string UserName = (string)Session["userName"];

            Site mSiteParDefaut = new Site();
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            ViewData["UrlSite"] = "LoadSiteAll";

            ViewData["Titre"] = "Moyenne Analyse";
            ViewData["actionToDo"] = "OnPrintQualityAverage";
            ViewData["ControllerName"] = "AnalysePhysique";
                       
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmDeliveryList", ViewData = ViewData };

        }

        public ActionResult OnPrintQualityAverage(string cropYear, string exportateur, string deliveryType, string supplier, string startDate, string endDate, string supplierName, string deliveryTypeDesignation, string exportateurNom)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {                

                DateTime dateDebut = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetStartDate").RawText.ToString(), "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetEndDate").RawText.ToString(), "d", CultureInfo.CurrentUICulture);

                Session["paramSite"] = int.Parse(GetFormValue("cmbDetSite"));
                Session["paramSiteText"] = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text;

                Session["paramCropYear"] = GetFormValue("cmbDetCrop");

                Session["paramExportateur"] = int.Parse(GetFormValue("cmbDetExporter"));
                Session["paramExportateurText"] = X.GetCmp<ComboBox>("cmbDetExporter").SelectedItem.Text;

                Session["paramDeliveryType"] = int.Parse(GetFormValue("cmbDetLivraisonType"));
                Session["paramDeliveryTypeText"] = X.GetCmp<ComboBox>("cmbDetLivraisonType").SelectedItem.Text;


                Session["paramSupplier"] = int.Parse(GetFormValue("cmbDetSupplier"));
                Session["paramSupplierText"] = X.GetCmp<ComboBox>("cmbDetSupplier").SelectedItem.Text;

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/AnalysePhysique/ViewReportResult', this, 'Moyenne Analyse report',''),App.frmDeliveryList.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Analyse Physique : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }


        }

        public ActionResult ViewReportResult()
        {
            XtraReport report = null;

            // TODO :  sur le base du type de rapport : detaillé ou cumulé 
            // initialiser l'objet report avec l'object idoine

            report = new rptQualityAverage() as XtraReport;

            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["campagneID"].Value = Session["paramCropYear"];

            report.Parameters["paramExportateur"].Value = Session["paramExportateur"];

            report.Parameters["paramExportateurText"].Value = Session["paramExportateurText"];

            report.Parameters["paramTypeLivraison"].Value = Session["paramDeliveryType"];

            report.Parameters["paramFournisseur"].Value = Session["paramSupplier"];

            report.Parameters["paramTypeLivraisonText"].Value = Session["paramDeliveryTypeText"];

            report.Parameters["paramFournisseurText"].Value = Session["paramSupplierText"];

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];

            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

        public ActionResult OnDisplayAnalysePhysiqueCriteria()
        {
            string UserName = (string)Session["userName"];

            Site mSiteParDefaut = new Site();
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            Parametres mParam = new Parametres(0);

            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;
            if (mParam.Site == mSiteParDefaut.ID) ViewData["UrlSite"] = "LoadSiteAll";
            else ViewData["UrlSite"] = "LoadSiteByAccess";

            ViewData["Titre"] = "Analyse Physique Interne";
            ViewData["actionToDo"] = "OnPrintAnalysePhysiqueList";
            ViewData["ControllerName"] = "AnalysePhysiqueInterne";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmCriteriaForPhysicalAnalysis", ViewData = ViewData };

        }

        public ActionResult OnPrintAnalysePhysiqueList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetStartDate").RawText.ToString(), "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetEndDate").RawText.ToString(), "d", CultureInfo.CurrentUICulture);

                //Session["paramCropYear"] = cropYear;                
                Session["paramSite"] = GetFormValue("CpcmbSite");
                Session["paramSiteText"] = X.GetCmp<ComboBox>("CpcmbSite").SelectedItem.Text.ToString();

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                Session["paramStatut"] = GetFormValue("CmbDetStatutAnalyse");
                Session["paramStatutText"] = X.GetCmp<ComboBox>("CmbDetStatutAnalyse").SelectedItem.Text.ToString();

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/AnalysePhysiqueInterne/ViewReportListResult', this, 'Liste des Analyses Physique',''),App.frmCriteriaForPhysicalAnalysis.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Analyse Physique : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }


        }

        public ActionResult ViewReportListResult()
        {
            //XtraReport report = null;

            rptPhysicalAnalysisList_int report = new rptPhysicalAnalysisList_int();
            report.DataSource = DevExpressReportDs.SetDataSource(report);
            report.Parameters["paramSite"].Value = Session["paramSite"];
            report.Parameters["paramSiteText"].Value = Session["paramSiteText"];
            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];

            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            report.Parameters["paramStatut"].Value = Session["paramStatut"];

            report.Parameters["paramStatutText"].Value = Session["paramStatutText"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }


    }
}


