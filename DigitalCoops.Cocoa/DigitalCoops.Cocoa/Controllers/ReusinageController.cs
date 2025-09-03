using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Business.stock;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class ReusinageController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        // GET: Reusinage
        public ActionResult Index()
        {
            string campagne = new Parametres(0).Campagne;

            X.GetCmp<ComboBox>("cmbFiltreCampagne").SetValue(campagne);

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("txtFiltreDateDebut").RawText = StartDate;
            X.GetCmp<DateField>("txtFiltreDateFin").RawText = EndDate;

            X.GetCmp<FormPanel>("ReusinageCP").SetTitle("Campagne : " + campagne + ", Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{9292D472-A488-4918-9099-EA60F3B685C8}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{38A0E7E1-AB31-4DD5-B4C5-B3F3C181DF67}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{551E7741-082D-482C-8884-7E10B898197D}")))
                X.GetCmp<MenuItem>("mnuPrintList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{98BD9B3D-07CF-4734-B45C-E3EF5E2A2FBD}")))
                X.GetCmp<MenuItem>("mnuExport").Enable();
            else
                X.GetCmp<MenuItem>("mnuExport").Disable();            

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{9BCC38C9-014C-421D-9918-7FA0EADD7634}")))
                X.GetCmp<Hidden>("ruhiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("ruhiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{501E7E41-52C1-4CD7-B2DD-770E395DE66A}")))
                X.GetCmp<Hidden>("ruhiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("ruhiddenPermDesactiver").SetValue(false);           

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{CCAE74CA-A2D2-46F3-B9DE-E012B4820A97}")))
                X.GetCmp<Hidden>("ruhiddenPermApprove").SetValue(true);
            else
                X.GetCmp<Hidden>("ruhiddenPermApprove").SetValue(false);
            #endregion

            return View();
        }

        public ActionResult onAdd()
        {            
            ReusinageViewModel mclass = new ReusinageViewModel();                        

            mclass._Reusinage = new Reusinage();
            ViewData["DefaultCampagne"] = new Parametres(0).Campagne;            
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormReusinage", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onEdit(string ItemSelected)
        {            
            ReusinageViewModel mclass = new ReusinageViewModel();
            mclass._Reusinage = new Reusinage();
            mclass._Reusinage = JSON.Deserialize<Reusinage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            ViewData["DefaultCampagne"] = new Parametres(0).Campagne;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormReusinage", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onConsult(string ItemSelected)
        {            
            ReusinageViewModel mclass = new ReusinageViewModel();
            mclass._Reusinage = new Reusinage();            

            mclass._Reusinage = JSON.Deserialize<Reusinage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            ViewData["DefaultCampagne"] = new Parametres(0).Campagne;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormReusinage", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onApprove(string ItemSelected)
        {
            Reusinage mclass = new Reusinage();            
            mclass = JSON.Deserialize<Reusinage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });                        
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPreventReusinage", Model = mclass };
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ReusinageCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "-1" : ItemCampagne;
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string statut = string.IsNullOrEmpty(ItemStatus) ? "NA" : ItemStatus;

            var mListe = (new Reusinage()).fnSelect(ItemCampagne, StartDate, EndDate, statut);

            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeLotReusine");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCampagne",ItemCampagne),
                                    new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus",ItemStatus)
                                });

                X.GetCmp<FormPanel>("ReusinageCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Reusinage : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult onCancel(string ItemSelected)
        {
            try
            {
                Reusinage mReusinage = JSON.Deserialize<Reusinage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = mReusinage.fnGet(mReusinage.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Reusinage, loading failed.");

                mReusinage.UtilisateurModification = (string)Session["userName"];

                result = mReusinage.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Reusinage, Operation failed.");

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeLotReusine");

                    ModelProxy mProxy = mstore.GetById(mReusinage.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mReusinage);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Reusinage : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitFormMethod(string ItemSelected)
        {            
            try
            {
                Reusinage mReusinage = new Reusinage();                
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mReusinage.IsNew = true;
                else
                {                    
                    mReusinage.IsNew = false;
                    
                    mReusinage.fnGet(Guid.Parse(GetFormValue("TxtReusinageID")));                    

                    if (mReusinage == null || mReusinage.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Weighing load failed.");
                }

                bool result = false;                
                mReusinage = MapFormToObject(mReusinage);

                result = mReusinage.fnUpdate();                

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeLotReusine");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mReusinage);
                        X.GetCmp<RowSelectionModel>("rowLotReusine").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mReusinage.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mReusinage);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormReusinage").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Reusinage : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        private Reusinage MapFormToObject(Reusinage mClass)
        {
            Campagne campagne = new Campagne();
            campagne.Designation = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text.ToString();
            mClass.Campagne = campagne;            

            Lot mLot = new Lot();
            mLot.ID = Guid.Parse(GetFormValue("cmbLot"));
            mLot.NumeroLot = X.GetCmp<ComboBox>("cmbLot").SelectedItem.Text.ToString();
            mClass.Lot = mLot;
            
            mClass.DateReusinage = DateTime.Parse(X.GetCmp<DateField>("txtDateReusinage").RawText.ToString()).Add(DateTime.Now.TimeOfDay);
            mClass.Statut = "NA";

            mClass.Raison = X.GetCmp<TextField>("TxtRaison").Text;
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }

        public ActionResult ApproveReusinage(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mTran = new DataTransaction();
            Guid reusinageID = Guid.Empty;
            try
            {
                string userName = (string)Session["userName"];
                
                bool resultMouvement = false;
                bool resultAnnulationLot = false;
                bool resultAnnulationAnalysePhysique = false;
                bool resultAnnulationAnalyseChimique = false;
                Reusinage mReusinage = new Reusinage();
                bool IsGuid = Guid.TryParse(ItemSelected, out reusinageID);

                if (!IsGuid)
                    throw new Exception("SubmitFormMethod : Weighing load failed.");
                //mReusinage = JSON.Deserialize<Reusinage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                mReusinage.fnGet(reusinageID);
                if (mReusinage == null || mReusinage.ID == Guid.Empty)
                    throw new Exception("SubmitFormMethod : Weighing load failed.");

                Lot mLot = new Lot();
                bool LotTrouve = mLot.fnGet(mReusinage.Lot.ID);
                if (mLot == null || mLot.ID == Guid.Empty)
                    throw new Exception("SubmitFormMethod : Weighing load failed.");

                //Verifier si Mouvement Stock
                MouvementStock mvt = new MouvementStock();
                bool mvtLot = mvt.fnGetLotForRecleaning(mLot.ID);
                if (mvt == null || mvt.ID == Guid.Empty)
                    mvtLot = false;
                else
                    mvtLot = true;

                //Verifier si Analyse Physique
                AnalysePhysiqueExport mAnalysePhysiqueExport = new AnalysePhysiqueExport();
                bool lotAnalysePhysique = mAnalysePhysiqueExport.fnGetByLot(mLot.ID);
                if (mAnalysePhysiqueExport == null || mAnalysePhysiqueExport.ID == Guid.Empty)
                    lotAnalysePhysique = false;
                else
                    lotAnalysePhysique = true;

                //Verifier si Analyse Chimique
                bool lotAnalyseChimique = true;
                AnalyseChimiqueEchantillonLot mEchantillonLot = new AnalyseChimiqueEchantillonLot();
                lotAnalyseChimique = mEchantillonLot.fnGetByLot(mLot.ID);
                if (mEchantillonLot == null || mEchantillonLot.ID == Guid.Empty)
                    lotAnalyseChimique = false;

                //Creer Mouvement Stock - Annulation     
                if (mvtLot)
                {
                    #region Mouvement Stock                
                    MouvementStock mouvement = new MouvementStock();

                    _db = mouvement.db();
                    mTran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                    mouvement.mCampagne = new Campagne();
                    mouvement.Exportateur = new Exportateur();
                    mouvement.SacType = new SacType();
                    mouvement.TypeElementStock = new TypeElementStock();
                    mouvement.Certification = new Certification();
                    mouvement.Magasin = new Magasin();
                    mouvement.Emplacement = new Emplacement();
                    mouvement.MouvementStockType = new MouvementStockType();
                    mouvement.SacType = new SacType();
                    mouvement.Certification = new Certification();
                    InventaireElementStock Items = new InventaireElementStock();
                    Parametres mParam = new Parametres(0);

                    mouvement.SetDataSource(_db);
                    mouvement.mCampagne.Designation = mLot.Campagne.Designation;
                    mouvement.Exportateur.ID = mLot.Exportateur.ID;
                    mouvement.DateMouvement = mReusinage.DateReusinage;
                    mouvement.SacType.ID = mParam.SacExportType;
                    mouvement.ObjetEnStock = mLot.ID;
                    mouvement.ObjetEnStockType = mParam.LotTypeElementEnStock;
                    //mouvement.TypeElementStock.ID = mParam.AjustementStockType;
                    mouvement.MouvementStockType.ID = mParam.ReusinageAnnulationLotMvtType;
                    if (mLot.Certification != null)
                        mouvement.Certification.ID = mLot.Certification.ID;
                    else
                        mouvement.Certification = null;

                    mouvement.Sens = -1;
                    mouvement.Quantite = mLot.NombreSacs;
                    mouvement.PoidsBrut = mLot.PoidsBrut;
                    mouvement.TarePalettes = mLot.TarePalette;
                    mouvement.TareSacs = mLot.TareSacs;
                    mouvement.PoidsNetLivre = mLot.PoidsBrut - mLot.TareSacs - mLot.TarePalette;
                    mouvement.PoidsNetAccepte = mLot.PoidsNet;
                    mouvement.Retention = 0;
                    mouvement.Magasin.ID = mParam.MagasinExport;
                    mouvement.Emplacement.ID = mParam.EmplacementParDefaut;
                    //mouvement.DateMouvement = inventaire.DateInventaire;
                    mouvement.Reference1 = mLot.NumeroLot;

                    if (mLot.Production != null)
                        mouvement.Reference2 = mLot.Production.NumeroProduction;
                    else
                        mouvement.Reference2 = "";

                    mouvement.Commentaire = "Annulation Du Lot";
                    mouvement.Statut = "AP";
                    mouvement.UtilisateurCreation = userName;
                    mouvement.UtilisateurModification = userName;

                    resultMouvement = mouvement.fnUpdate(mTran);
                    if (!resultMouvement)
                        _db.RollBackTransaction(mTran);
                    #endregion

                    mLot.SetDataSource(_db);                    
                }

                else
                {
                    _db = mLot.db();
                    mTran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                }

                //Annuler le Lot
                mLot.UtilisateurModification = userName;
                resultAnnulationLot = mLot.fnCancelForCleaning(mTran);

                if (!resultAnnulationLot)
                    _db.RollBackTransaction(mTran);

                //Annuler Analyse Physique
                if (lotAnalysePhysique)
                {
                    mAnalysePhysiqueExport.SetDataSource(_db);
                    mAnalysePhysiqueExport.UtilisateurModification = userName;
                    resultAnnulationAnalysePhysique = mAnalysePhysiqueExport.fnDeActivate(mTran);
                    if (!resultAnnulationAnalysePhysique)
                        _db.RollBackTransaction(mTran);
                }

                //Annuler Analyse Chimique
                if (lotAnalyseChimique)
                {
                    mEchantillonLot.SetDataSource(_db);
                    mEchantillonLot.UtilisateurModification = userName;
                    resultAnnulationAnalyseChimique = mEchantillonLot.fnDeActivate(mTran);
                    if (!resultAnnulationAnalysePhysique)
                        _db.RollBackTransaction(mTran);
                }

                // Approver Reusinage
                mReusinage.Statut = "AP";
                mReusinage.Approbateur = userName;
                mReusinage.SetDataSource(_db);               
                bool result = false;
                result = mReusinage.fnApprove(mTran);

                if (!result)
                    _db.RollBackTransaction(mTran);
                else
                    _db.CommitTransaction(mTran);

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeLotReusine");

                    ModelProxy mProxy = mstore.GetById(mReusinage.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mReusinage);

                    mProxy.Commit();

                    mProxy.EndEdit();

                    X.GetCmp<Window>("FormPreventReusinage").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Reusinage : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
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

        [DirectMethod(Namespace = "CompanyX")]
        public ActionResult DoConfirm()
        {
            // Manually configure Handler...
            //Msg.Confirm("Message", "Confirm?", "if (buttonId == 'yes') { CompanyX.DoYes(); } else { CompanyX.DoNo(); }").Show();

            // Configure individualock Buttons using a ButtonsConfig...
            X.Msg.Confirm("Message", "Confirm?", new MessageBoxButtonsConfig
            {
                Yes = new MessageBoxButtonConfig
                {
                    Handler = "CompanyX.MessageBox_Basic.DoYes()",
                    Text = "Yes Please"
                },
                No = new MessageBoxButtonConfig
                {
                    Handler = "CompanyX.MessageBox_Basic.DoNo()",
                    Text = "No Thanks"
                },                
            }).Show();

            return this.Direct();
        }

    }
}