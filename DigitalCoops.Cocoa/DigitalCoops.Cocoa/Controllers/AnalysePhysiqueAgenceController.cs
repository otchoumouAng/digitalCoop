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
    public class AnalysePhysiqueAgenceController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: AnalysePhysiqueAgence
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);                     

            X.GetCmp<ComboBox>("CmbCropYear").SetValue(mParam.Campagne);

            Site mSiteParDefaut = new Site();
            string UserName = (string)Session["userName"];
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            DateTime firstdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            DateTime lastDayOfMonth = firstdayofmonth.AddMonths(1).AddDays(-1);

            //X.GetCmp<DateField>("TxtPeriodStart").RawText = firstdayofmonth.ToShortDateString();
            //X.GetCmp<DateField>("TxtPeriodEnd").RawText = lastDayOfMonth.ToShortDateString();
            X.GetCmp<DateField>("TxtPeriodStart").RawText = DateTime.Now.ToShortDateString();
            X.GetCmp<DateField>("TxtPeriodEnd").RawText = DateTime.Now.ToShortDateString();

            X.GetCmp<FormPanel>("AnalysePhysiqueAgenceCriteriaPanel").SetTitle("Campagne : " + mParam.Campagne + " | Today : " + DateTime.Now.ToShortDateString());


            #region Set Function's Access           

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{07F2DA53-B0A2-4EAD-B3A4-E2C6C0F6D9C5}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{F08A2229-EF17-49C0-BA07-060408E55258}")))
                X.GetCmp<Button>("btnNewAnalysePhysiqueAgence").Enable();
            else
                X.GetCmp<Button>("btnNewAnalysePhysiqueAgence").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{ED0BFDA4-862B-4533-9554-1DBFACF982C3}")))
                X.GetCmp<MenuItem>("mnuExportAnalysePhysiqueAgence").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportAnalysePhysiqueAgence").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{F08A2229-EF17-49C0-BA07-060408E55258}")))
                X.GetCmp<Hidden>("AphiddenPermCreer").SetValue(true);
            else
                X.GetCmp<Hidden>("AphiddenPermCreer").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{C0C6CAE9-0415-484E-8818-E70502AFD873}")))
                X.GetCmp<Hidden>("AphiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("AphiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{BCC54E1C-9F86-47B9-B9DD-486B026461CF}")))
                X.GetCmp<Hidden>("AphiddenPermApprove").SetValue(true);
            else
                X.GetCmp<Hidden>("AphiddenPermApprove").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{BCC54E1C-9F86-47B9-B9DD-486B026461CF}")))
                X.GetCmp<Hidden>("AphiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("AphiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{ECED9A0A-2D54-4376-9675-B05271AE897B}")))
                X.GetCmp<Hidden>("AphiddenPermPrintQualityAverage").SetValue(true);
            else
                X.GetCmp<Hidden>("AphiddenPermPrintQualityAverage").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{ED0BFDA4-862B-4533-9554-1DBFACF982C3}")))
                X.GetCmp<Hidden>("AphiddenPermExporterExcel").SetValue(true);
            else
                X.GetCmp<Hidden>("AphiddenPermExporterExcel").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{ED0BFDA4-862B-4533-9554-1DBFACF982C3}")))
                X.GetCmp<Hidden>("AphiddenPermOverview").SetValue(true);
            else
                X.GetCmp<Hidden>("AphiddenPermOverview").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{F3655E97-95AC-4565-BE06-BDFBB58B2EB7}")))
                X.GetCmp<Hidden>("AphiddenPermSuperDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("AphiddenPermSuperDesactiver").SetValue(false);           

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{21227326-CCF6-42E2-892D-977173D9BCAA}")))
                X.GetCmp<Button>("btnPendingDeliveries").Enable();
            else
                X.GetCmp<Button>("btnPendingDeliveries").Disable();
            #endregion

            return View();
        }

        public ActionResult OnAdd()
        {

            Parametres mParam = new Parametres(0);
            
            AnalysePhysiqueViewModel AnalysePhysiqueAgenceVM = new AnalysePhysiqueViewModel();

            AnalysePhysiqueAgenceVM._AnalysePhysique = new AnalysePhysique();
            AnalysePhysiqueAgenceVM._AnalysePhysique.Laboratoire = new Laboratoire();
            AnalysePhysiqueAgenceVM._AnalysePhysique.Laboratoire.ID = mParam.Laboratoire.ID;
            AnalysePhysiqueAgenceVM._AnalysePhysique.Sites = new Site();

            Site mSiteParDefaut = new Site();
            string UserName = (string)Session["userName"];
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            if (result)
            {
                AnalysePhysiqueAgenceVM._AnalysePhysique.Sites.ID = mSiteParDefaut.ID;
                AnalysePhysiqueAgenceVM._AnalysePhysique.Sites.Nom = mSiteParDefaut.Nom;
            }

            AnalysePhysiqueAgenceVM._Parametres = new Parametres();
            

            AnalysePhysiqueAgenceVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAnalysePhysiqueAgence", Model = AnalysePhysiqueAgenceVM, };

        }

        public ActionResult OnEdit(string ItemSelected)
        {            

            AnalysePhysique AnalysePhysiqueAgence = JSON.Deserialize<AnalysePhysique>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            AnalysePhysiqueViewModel AnalysePhysiqueAgenceVM = new AnalysePhysiqueViewModel();

            AnalysePhysiqueAgenceVM._AnalysePhysique = AnalysePhysiqueAgence;

            AnalysePhysiqueAgenceVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAnalysePhysiqueAgence", Model = AnalysePhysiqueAgenceVM, };

        }

        public ActionResult OnConsult(string ItemSelected)
        {            

            AnalysePhysique AnalysePhysiqueAgence = JSON.Deserialize<AnalysePhysique>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            AnalysePhysiqueViewModel AnalysePhysiqueAgenceVM = new AnalysePhysiqueViewModel();

            AnalysePhysiqueAgenceVM._AnalysePhysique = AnalysePhysiqueAgence;

            AnalysePhysiqueAgenceVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAnalysePhysiqueAgence", Model = AnalysePhysiqueAgenceVM, };

        }

        public ActionResult onApprove(string ItemSelected)
        {            

            AnalysePhysique mAnalyse = JSON.Deserialize<AnalysePhysique>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            AnalysePhysiqueViewModel mAnalyseVM = new AnalysePhysiqueViewModel();

            mAnalyseVM._AnalysePhysique = mAnalyse;

            mAnalyseVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAnalysePhysiqueAgence", Model = mAnalyseVM, };
        }

        public ActionResult OnViewPendingDeliveries()
        {

            Parametres mParam = new Parametres(0);

            AnalysePhysiqueViewModel AnalysePhysiqueAgenceVM = new AnalysePhysiqueViewModel();

            AnalysePhysiqueAgenceVM._AnalysePhysique = new AnalysePhysique();
            AnalysePhysiqueAgenceVM._AnalysePhysique.Laboratoire = new Laboratoire();
            AnalysePhysiqueAgenceVM._AnalysePhysique.Laboratoire.ID = mParam.Laboratoire.ID;
            AnalysePhysiqueAgenceVM._AnalysePhysique.Sites = new Site();
            AnalysePhysiqueAgenceVM._AnalysePhysique.IsPending = true;

            Site mSiteParDefaut = new Site();
            string UserName = (string)Session["userName"];
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            if (result)
            {
                AnalysePhysiqueAgenceVM._AnalysePhysique.Sites.ID = mSiteParDefaut.ID;
                AnalysePhysiqueAgenceVM._AnalysePhysique.Sites.Nom = mSiteParDefaut.Nom;
            }

            AnalysePhysiqueAgenceVM._Parametres = new Parametres();


            AnalysePhysiqueAgenceVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAnalysePhysiqueAgence", Model = AnalysePhysiqueAgenceVM };

        }

        public ActionResult SubmitAnalyeApprove()
        {
            try
            {
                AnalysePhysiqueAgence analyse = new AnalysePhysiqueAgence();

                analyse.ID = Guid.Parse(X.GetCmp<TextField>("TxtAnalysePhysiqueID").Text.ToString());
                //analyse.AnalyseCode = new AnalyseCode();
                //analyse.AnalyseCode.ID = Guid.Parse(X.GetCmp<Hidden>("HiddenCodeAnalyseID").Text.ToString());

                bool result = analyse.fnGet(analyse.ID);

                if (analyse == null || analyse.ID == Guid.Empty)
                    throw new Exception("onApproveAnalysePhysiqueAgence : Analyse Physique Approve failed.");

                analyse.Approbateur = (string)Session["userName"];
                analyse.DateApprobation = DateTime.Now;
                analyse.Statut = X.GetCmp<ComboBox>("CmbResultatAnalyse").RawValue.ToString();
                analyse.Confirmation = true;
                analyse.Commentaire = X.GetCmp<TextField>("TxtCommentaireAnalysePhysiqueAgence").Text;
                analyse.UtilisateurModification = (string)Session["userName"];


                result = analyse.fnApprove();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeAnalysePhysiqueAgence");

                    ModelProxy mProxy = mstore.GetById(analyse.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(analyse);

                    mProxy.Commit();

                    mProxy.EndEdit();
                    X.GetCmp<Window>("FormAnalysePhysiqueAgence").Close();
                    X.GetCmp<Viewport>("TmsViewPort").Unmask();
                    X.Js.Call("Overview.resetButtons");
                }
                else
                {
                    X.GetCmp<Window>("FormAnalysePhysiqueAgence").Close();
                    X.GetCmp<Viewport>("TmsViewPort").Unmask();
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
                AnalysePhysiqueAgence AnalysePhysiqueAgence = JSON.Deserialize<AnalysePhysiqueAgence>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = AnalysePhysiqueAgence.fnGet(AnalysePhysiqueAgence.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Analyse Physique loading failed.");

                AnalysePhysiqueAgence.UtilisateurModification = (string)Session["userName"];

                if (AnalysePhysiqueAgence.Desactive)
                    result = AnalysePhysiqueAgence.fnActivate();
                else
                    result = AnalysePhysiqueAgence.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Analyse Physique, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeAnalysePhysiqueAgence");

                    ModelProxy mProxy = mstore.GetById(AnalysePhysiqueAgence.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(AnalysePhysiqueAgence);

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
            X.GetCmp<FormPanel>("AnalysePhysiqueAgenceCriteriaPanel").ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemCropYear, string ItemSite, string ItemLivraisonType, string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store store = X.GetCmp<Store>("storeListeAnalysePhysiqueAgence");
                store.Reload();
                store.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemCropYear", ItemCropYear),
                                new Ext.Net.Parameter("ItemFournisseur", ItemFournisseur),
                                new Ext.Net.Parameter("ItemLivraisonType", ItemLivraisonType),
                                new Ext.Net.Parameter("ItemSite", ItemSite),
                                new Ext.Net.Parameter("ItemPeriodStart"     ,ItemPeriodStart),
                                new Ext.Net.Parameter("ItemPeriodEnd"           ,ItemPeriodEnd),
                                new Ext.Net.Parameter("ItemStatus"           ,ItemStatus)
                            });

                X.GetCmp<FormPanel>("AnalysePhysiqueAgenceCriteriaPanel").Collapsed = true;

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

        public ActionResult Select(StoreRequestParameters parameters, string ItemCropYear, string ItemSite, string ItemLivraisonType, string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            
                string CropYearID = string.IsNullOrEmpty(ItemCropYear) ? "{Tous}" : ItemCropYear;
                if (!string.IsNullOrEmpty(ItemCropYear) && ItemCropYear.Contains("null"))
                {
                    CropYearID = "{Tous}";
                }

                int fournisseurID = GetCritriaValue(ItemFournisseur);
                int siteID = GetCritriaValue(ItemSite);
                int livraisonTypeID = GetCritriaValue(ItemLivraisonType);

                DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
                DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

                //string status = string.Empty;
                int status = -1;
                if (!string.IsNullOrEmpty(ItemStatus))
                {
                    status = int.Parse(ItemStatus);
                }

                var mListe = (new AnalysePhysique()).fnSelectForSite(CropYearID, siteID, livraisonTypeID, fournisseurID, startdate, enddate, status);

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

        public ActionResult LoadListOfAnalysisForFinalizing(string ItemLivraison, string ItemExecMode)
        {
            List<DataPersist> myList = new List<DataPersist>();
            if (!string.IsNullOrEmpty(ItemLivraison))
            {

                if (ItemExecMode == "AddNew")
                {
                    myList = new AnalysePhysiqueAgence().fnSelectByDelivery(new Guid());
                }
                else
                {
                    myList = new AnalysePhysiqueAgence().fnSelectByDelivery(Guid.Parse(ItemLivraison));
                }

                AnalysePhysiqueAgence mClass = new AnalysePhysiqueAgence();
                if (myList.Count > 0)
                    mClass = myList[0] as AnalysePhysiqueAgence;
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
                        throw new Exception("SubmitFormMethod : Analyse Physique load failed.");
                }

                mClass = MapFormToObject(mClass);

                bool result = mClass.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeAnalysePhysiqueAgence");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionAnalysePhysiqueAgence").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormAnalysePhysiqueAgence").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
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

        private AnalysePhysique MapFormToObject(AnalysePhysique mAnalyse)
        {
            mAnalyse.AnalyseCode = null;
            if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbCodeAnalyse").Text))
            {
                mAnalyse.AnalyseCode = new AnalyseCode();
                mAnalyse.AnalyseCode.ID = Guid.Parse(X.GetCmp<ComboBox>("CmbCodeAnalyse").RawValue.ToString());
                mAnalyse.AnalyseCode.Code = X.GetCmp<ComboBox>("CmbCodeAnalyse").SelectedItem.Text.ToString();
            }

            //AnalysePhysiqueAgence.AnalyseCode.fnGetByCode(X.GetCmp<ComboBox>("CmbCodeAnalyse").SelectedItem.Text.ToString());
            //AnalysePhysiqueAgence.AnalyseCode.ID = Guid.Parse(X.GetCmp<ComboBox>("CmbCodeAnalyse").RawValue.ToString());
            //AnalysePhysiqueAgence.AnalyseCode.Code = X.GetCmp<ComboBox>("CmbCodeAnalyse").SelectedItem.Text.ToString();

            //AnalysePhysiqueAgence.Livraison.Campagne = new Campagne();
            //AnalysePhysiqueAgence.Livraison.Campagne.Designation = GetFormValue("CropYearID");
            mAnalyse.Livraison = new Livraison();
            mAnalyse.Livraison.ID = Guid.Parse(X.GetCmp<ComboBox>("txtLivraisonID").RawValue.ToString());

            mAnalyse.Livraison.Numero = X.GetCmp<TextField>("txtDeliveryNumero").Text;

            mAnalyse.Livraison.Fournisseur = new Fournisseur();
            mAnalyse.Livraison.Fournisseur.ID = int.Parse(GetFormValue("txtLivraisonFournisseurID"));
            mAnalyse.Livraison.Fournisseur.Nom = X.GetCmp<Hidden>("txtLivraisonFournisseurNom").Text;

            mAnalyse.Sites = new Site();
            mAnalyse.Sites.ID = int.Parse(GetFormValue("txtSiteID"));
            mAnalyse.Sites.Nom = X.GetCmp<ComboBox>("txtNomSite").Text;

            //mAnalyse.Livraison.LivraisonType = new LivraisonType();
            //mAnalyse.Livraison.LivraisonType.ID = int.Parse(GetFormValue("CmbTypeLivraisonID"));
            //mAnalyse.Livraison.LivraisonType.Designation = X.GetCmp<ComboBox>("CmbTypeLivraisonID").SelectedItem.Text.ToString();

            mAnalyse.Laboratoire = new Laboratoire();
            mAnalyse.Laboratoire.ID = int.Parse(X.GetCmp<ComboBox>("CmbLaboratoire").RawValue.ToString());

            mAnalyse.DateAnalyse = DateTime.Parse(X.GetCmp<DateField>("TxtDateAnalysePhysique").RawText.ToString()).Add(DateTime.Now.TimeOfDay);
            mAnalyse.FicheNumero = X.GetCmp<TextField>("TxtSheetNumber").Text;

            mAnalyse.NombreFeves = int.Parse(X.GetCmp<NumberField>("TxtNbrOfBeans").Text);
            mAnalyse.Grainage = int.Parse(X.GetCmp<NumberField>("TxtBeansCount").Text);
            mAnalyse.MoisieNbre = int.Parse(X.GetCmp<NumberField>("TxtMouldy").Text);
            mAnalyse.PlateNbre = int.Parse(X.GetCmp<NumberField>("TxtFlat").Text);
            mAnalyse.WeevilNbre = int.Parse(X.GetCmp<NumberField>("TxtWeevil").Text);
            mAnalyse.GermeeNbre = int.Parse(X.GetCmp<NumberField>("TxtGerminated").Text);
            mAnalyse.ArdoiseeNbre = int.Parse(X.GetCmp<NumberField>("TxtSlaty").Text);
            mAnalyse.VioletteNbre = int.Parse(X.GetCmp<NumberField>("TxtViolet").Text);
            mAnalyse.NombreFevesPc = double.Parse(X.GetCmp<NumberField>("TxtBeansCountPc").RawText);
            mAnalyse.Moisie = double.Parse(X.GetCmp<NumberField>("TxtMouldyPr").RawText);
            mAnalyse.Plate = double.Parse(X.GetCmp<NumberField>("TxtFlatPr").RawText);
            mAnalyse.Weevil = double.Parse(X.GetCmp<NumberField>("TxtWeevilPr").RawText);
            mAnalyse.Germee = double.Parse(X.GetCmp<NumberField>("TxtGerminatedPr").RawText);
            mAnalyse.Ardoisee = double.Parse(X.GetCmp<NumberField>("TxtSlatyPr").RawText);
            mAnalyse.Violette = double.Parse(X.GetCmp<NumberField>("TxtVioletPr").RawText);
            mAnalyse.Defectueuse = double.Parse(X.GetCmp<NumberField>("TxtDefectives").RawText);
            mAnalyse.Fermentation = double.Parse(X.GetCmp<NumberField>("TxtUnFermented").RawText);
            mAnalyse.Tamis = double.Parse(X.GetCmp<NumberField>("TxtSieving").RawText);
            mAnalyse.Fragment = double.Parse(X.GetCmp<NumberField>("TxtFragment").RawText);
            mAnalyse.Brisure = double.Parse(X.GetCmp<NumberField>("TxtBrokenBean").RawText);
            mAnalyse.Humidite = double.Parse(X.GetCmp<NumberField>("TxtMoisture").RawText);
            mAnalyse.MatiereEtrangere = double.Parse(X.GetCmp<NumberField>("TxtForeignMatter").RawText);
            if (string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtFfa").RawText))
                mAnalyse.Ffa = (double?)null;
            else
                mAnalyse.Ffa = double.Parse(X.GetCmp<NumberField>("TxtFfa").RawText);

            if (string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtFfa").RawText))
                mAnalyse.Ffa = (double?)null;
            else
                mAnalyse.Ffa = double.Parse(X.GetCmp<NumberField>("TxtFfa").RawText);

            mAnalyse.Commentaire = X.GetCmp<TextField>("TxtCommentaireAnalysePhysique").Text;
            mAnalyse.Confirmation = false;
            mAnalyse.Statut = X.GetCmp<ComboBox>("CmbResultatAnalyse").RawValue.ToString();
            mAnalyse.ClassificationFeves = new ClassificationFeves();
            mAnalyse.ClassificationFeves.ID = int.Parse(X.GetCmp<ComboBox>("CmbClassification").RawValue.ToString());
            mAnalyse.ClassificationFeves.Designation = X.GetCmp<ComboBox>("CmbClassification").RawText;

            mAnalyse.Analyseur = null;
            if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbAnalyseur").Text))
            {
                mAnalyse.Analyseur = new Analyseur();
                mAnalyse.Analyseur.ID = int.Parse(X.GetCmp<ComboBox>("CmbAnalyseur").RawValue.ToString());
                mAnalyse.Analyseur.Nom = X.GetCmp<ComboBox>("CmbAnalyseur").RawText;
            }

            mAnalyse.Verificateur = null;
            if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbSuperviseur").Text))
            {
                mAnalyse.Verificateur = new Analyseur();
                mAnalyse.Verificateur.ID = int.Parse(X.GetCmp<ComboBox>("CmbSuperviseur").RawValue.ToString());
                mAnalyse.Verificateur.Nom = X.GetCmp<ComboBox>("CmbSuperviseur").RawText;
            }

            mAnalyse.UtilisateurCreation = (string)Session["userName"];
            mAnalyse.UtilisateurModification = (string)Session["userName"];

            return mAnalyse;
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
            X.GetCmp<RowSelectionModel>("rowSelectionAnalysePhysiqueAgence").DeselectAll();
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
            ViewData["Titre"] = "Print Moyenne Analyse";
            ViewData["actionToDo"] = "OnPrintQualityAverage";
            ViewData["ControllerName"] = "AnalysePhysiqueAgence";
            //Fournisseur mClass = new Fournisseur();
            //mClass.ID = -1;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmDeliveryList", ViewData = ViewData };

        }

        public ActionResult OnPrintQualityAverage(string cropYear, string exportateur, string deliveryType, string supplier, string startDate, string endDate, string supplierName, string deliveryTypeDesignation, string exportateurNom)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                Session["paramCropYear"] = cropYear;
                Session["paramDeliveryType"] = Int32.Parse(deliveryType);
                Session["paramDeliveryTypeText"] = deliveryTypeDesignation;
                Session["paramExportateur"] = Int32.Parse(exportateur);
                Session["paramExportateurText"] = exportateurNom;

                Session["paramSupplier"] = Int32.Parse(supplier);
                Session["paramSupplierText"] = supplierName;

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/AnalysePhysiqueAgence/ViewReportResult', this, 'Moyenne Analyse report',''),App.frmDeliveryList.doClose()", Guid.NewGuid(), BaseUrl));
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

        public ActionResult OnDisplayAnalysePhysiqueAgenceCriteria()
        {

            ViewData["Titre"] = "Print Analyse Physique";
            ViewData["actionToDo"] = "OnPrintAnalysePhysiqueAgenceList";
            ViewData["ControllerName"] = "AnalysePhysiqueAgence";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmCriteriaForAnalysePhysiqueAgence", ViewData = ViewData };

        }

        public ActionResult OnPrintAnalysePhysiqueAgenceList(string startDate, string endDate, string statut, string statutText)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                //Session["paramCropYear"] = cropYear;                

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                Session["paramStatut"] = statut;
                Session["paramStatutText"] = statutText;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/AnalysePhysiqueAgence/ViewReportListResult', this, 'List Of Analyse Physique',''),App.frmCriteriaForAnalysePhysiqueAgence.doClose()", Guid.NewGuid(), BaseUrl));
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

            //rptAnalysePhysiqueAgenceList report = new rptAnalysePhysiqueAgenceList();
            //report.DataSource = DevExpressReportDs.SetDataSource(report);

            //report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];

            //report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            //report.Parameters["paramStatut"].Value = Session["paramStatut"];

            //report.Parameters["paramStatutText"].Value = Session["paramStatutText"];

            //ViewData["Report"] = report;

            return View("ViewReportResult");
        }

        public ActionResult OnSelectDelivery(int? siteID, string ViewPending = "")
        {
            try
            {
                if ((!string.IsNullOrEmpty(ViewPending) && ViewPending == "true") || string.IsNullOrEmpty(ViewPending))
                {

                    if (!siteID.HasValue)
                        throw new Exception("");

                    AnalysePhysique mModel = new AnalysePhysique();
                    mModel.Sites = new Site();
                    mModel.Sites.ID = (int)siteID;
                    return new Ext.Net.MVC.PartialViewResult { ViewName = "AnalysePhysique_Deliveries", Model = mModel, ViewData = ViewData };
                }
                else
                    return this.Direct();

            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing : Delivery",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }

        }

        public ActionResult LoadListOfAvailableDeliveries(string ItemSite, string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd, string ItemListeAdded = "", string ItemPeseeID = null)
        {
            int? siteID = ItemSite == "" ? (int?)null : int.Parse(ItemSite);
            int? fournisseurID = ItemFournisseur == "" ? -1 : int.Parse(ItemFournisseur);
            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
                     
            var mListe = (new Livraison()).fnSelectForAnalyseInSite((int)siteID, (int)fournisseurID, StartDate, EndDate);
            return this.Store(mListe);
        }

        public ActionResult SubmitDelivery(string ItemSelected, string FromPending = "")
        {
            Livraison mLivraison = new Livraison();
            mLivraison = JSON.Deserialize<Livraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            X.GetCmp<TextField>("txtDeliveryNumero").SetValue(mLivraison.Numero);                        
            X.GetCmp<Hidden>("txtImmatriculation").SetValue(mLivraison.Immatriculation);
            X.GetCmp<Hidden>("txtLivraisonFournisseurID").SetValue(mLivraison.Fournisseur.ID);
            X.GetCmp<Hidden>("txtLivraisonFournisseurNom").SetValue(mLivraison.Fournisseur.Nom);
            X.GetCmp<Hidden>("txtLivraisonID").SetValue(mLivraison.ID);
            X.GetCmp<Window>("AnalysePhysique_Deliveries").Close();

            return this.Direct();
        }

    }
}


