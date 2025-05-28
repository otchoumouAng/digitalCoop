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
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class PeseeLotCertifieController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        // GET: PeseeLotCertifie

        static int _CapturedTare;
        static int _CapturedWeight;                

        public static int CapturedTare
        {
            get
            {
                return _CapturedTare;
            }
            set
            {
                _CapturedTare = value;
            }
        }

        public static int CapturedWeight
        {
            get
            {
                return _CapturedWeight;
            }
            set
            {
                _CapturedWeight = value;
            }
        }

        bool BtnReadClicked = false;

        public ActionResult Index()
        {
            string campagne = new Parametres(0).Campagne;

            X.GetCmp<ComboBox>("cmbFiltreCampagne").SetValue(campagne);

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("txtFiltreDateDebut").RawText = StartDate;
            X.GetCmp<DateField>("txtFiltreDateFin").RawText = EndDate;

            X.GetCmp<FormPanel>("PeseeLotCertifieCP").SetTitle("Campagne : " + campagne + ", Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{9EA639BE-EFFA-4410-819A-2FB85F1ED839}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{B25416FB-979E-4213-9FB6-D5ECC774C78C}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{67252D7B-EF7F-4544-9D9C-7C32F3E1559F}")))
                X.GetCmp<MenuItem>("mnuPrintWeihingList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintWeihingList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{184927D1-3569-483F-B7D1-126F0660F26D}")))
                X.GetCmp<MenuItem>("mnuExport").Enable();
            else
                X.GetCmp<MenuItem>("mnuExport").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{E700832D-1C9F-4357-ACA3-E2B850B9466E}")))
                X.GetCmp<MenuItem>("bntAddManual").Enable();
            else
                X.GetCmp<MenuItem>("bntAddManual").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{F1DFF88E-E126-4265-89A6-9015F69259F4}")))
                X.GetCmp<Hidden>("phiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{88C46BD2-A962-4509-A790-F15C67C162EC}")))
                X.GetCmp<Hidden>("phiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{24168844-2133-423D-8399-F6B8A86AD06B}")))
                X.GetCmp<Hidden>("phiddenPermPrintSheet").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermPrintSheet").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{B13FF6F3-37A3-4AE1-8BE5-FB389EECE1B3}")))
                X.GetCmp<Hidden>("phiddenPermFinalize").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermFinalize").SetValue(false);
            #endregion

            return View();
        }

        public ActionResult onAdd()
        {            
            PeseeLotCertifieViewModel mclass = new PeseeLotCertifieViewModel();
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{B13FF6F3-37A3-4AE1-8BE5-FB389EECE1B3}"), UserName);

            Parametres mParam = new Parametres(0);
            mclass._PeseeLotCertifie = new PeseeLotCertifie();
            mclass._DefaultCampagne = mParam.Campagne;
            mclass._DefaultTypeLotCertifie = mParam.LotCertifie;
            mclass._PeseeLotCertifie.IsManual = false;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeLotCertifie", Model = mclass, ViewData = ViewData };

        }

        public ActionResult onEdit(string ItemSelected)
        {
            //WeighingIsCreated = true;
            PeseeLotCertifieViewModel mclass = new PeseeLotCertifieViewModel();
            // mclass._PeseeLotCertifie = new PeseeLotCertifie();
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{B13FF6F3-37A3-4AE1-8BE5-FB389EECE1B3}"), UserName);

            mclass._PeseeLotCertifie = JSON.Deserialize<PeseeLotCertifie>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            Parametres mParam = new Parametres(0);
            mclass._DefaultCampagne = mParam.Campagne;
            mclass._DefaultTypeLotCertifie = mParam.LotCertifie;
            CapturedTare = int.Parse(mclass._PeseeLotCertifie.TarePalette.ToString());
            BtnReadClicked = true;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            //mclass._PeseeLotCertifie.IsManual = false;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeLotCertifie", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onConsult(string ItemSelected)
        {
            //WeighingIsCreated = true;
            PeseeLotCertifieViewModel mclass = new PeseeLotCertifieViewModel();
            //mclass._PeseeLotCertifie = new PeseeLotCertifie();            
            ViewData["CanFinalize"] = false;

            mclass._PeseeLotCertifie = JSON.Deserialize<PeseeLotCertifie>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            Parametres mParam = new Parametres(0);
            mclass._DefaultCampagne = mParam.Campagne;
            mclass._DefaultTypeLotCertifie = mParam.LotCertifie;
            CapturedTare = int.Parse(mclass._PeseeLotCertifie.TarePalette.ToString());
            BtnReadClicked = true;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
            //mclass._PeseeLotCertifie.IsManual = false;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeLotCertifie", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onFinalize(string ItemSelected)
        {
            //WeighingIsCreated = true;
            PeseeLotCertifieViewModel mclass = new PeseeLotCertifieViewModel();
            //mclass._PeseeLotCertifie = new PeseeLotCertifie();
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{B13FF6F3-37A3-4AE1-8BE5-FB389EECE1B3}"), UserName);

            mclass._PeseeLotCertifie = JSON.Deserialize<PeseeLotCertifie>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            Parametres mParam = new Parametres(0);
            mclass._DefaultCampagne = mParam.Campagne;
            mclass._DefaultTypeLotCertifie = mParam.LotCertifie;
            CapturedTare = int.Parse(mclass._PeseeLotCertifie.TarePalette.ToString());
            BtnReadClicked = true;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;
            //mclass._PeseeLotCertifie.IsManual = false;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeLotCertifie", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onManualWeight(string ItemSelected = "")
        {
            //WeighingIsCreated = false;
            PeseeLotCertifieViewModel mclass = new PeseeLotCertifieViewModel();
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{B13FF6F3-37A3-4AE1-8BE5-FB389EECE1B3}"), UserName);

            mclass._PeseeLotCertifie = new PeseeLotCertifie();
            Parametres mParam = new Parametres(0);
            mclass._DefaultCampagne = mParam.Campagne;
            mclass._DefaultTypeLotCertifie = mParam.LotCertifie;
            mclass._PeseeLotCertifie.IsManual = true;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeLotCertifie", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onAddWeight(string IsManual = "")
        {
            PeseeLotCertifie pesee = new PeseeLotCertifie();
            if (!string.IsNullOrEmpty(IsManual))
                pesee.IsManual = bool.Parse(IsManual);
            else
                pesee.IsManual = false;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAddWeight", Model = pesee };
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("PeseeLotCertifieCP");
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

            var mListe = (new PeseeLotCertifie()).fnSelect(ItemCampagne, StartDate, EndDate, statut);

            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListePeseeLotCertifie");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCampagne",ItemCampagne),
                                    new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus",ItemStatus)
                                });

                X.GetCmp<FormPanel>("PeseeLotCertifieCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Certified Lot Weighing  : Data Validation",
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
                PeseeLotCertifie pesee = JSON.Deserialize<PeseeLotCertifie>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = pesee.fnGet(pesee.ID);

                if (!result)
                    throw new Exception("onCancel : Certified Lot Weighing , loading failed.");

                pesee.UtilisateurModification = (string)Session["userName"];


                result = pesee.fnDeActivate();

                if (!result)
                    throw new Exception("onCancel : Certified Lot Weighing , Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePeseeLotCertifie");

                    ModelProxy mProxy = mstore.GetById(pesee.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(pesee);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Certified Lot Weighing  : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitFormWeighingBeforePallets()
        {
            try
            {
                Guid peseeID = Guid.Empty;
                DateTime datePesee = new DateTime();
                PeseeLotCertifie mPesee = new PeseeLotCertifie();
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mPesee.IsNew = true;
                else
                {
                    formExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                    mPesee.IsNew = false;
                    bool isGuid = Guid.TryParse(GetFormValue("TxtPeseeLotCertifieID"), out peseeID);
                    if (isGuid)
                        mPesee.fnGet(peseeID);                    

                    if (mPesee == null || mPesee.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Weighing load failed.");

                    datePesee = mPesee.DatePeseeProduction;
                }

                bool result = false;

                mPesee = MapFormToObject(mPesee);
                if (mPesee.IsNew == false && (datePesee.Date == mPesee.DatePeseeProduction.Date))
                    mPesee.DatePeseeProduction = datePesee;

                result = mPesee.fnUpdate();
                
                if (result)
                {                    

                    Store mstore = X.GetCmp<Store>("storeListePeseeLotCertifie");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mPesee);
                        X.GetCmp<RowSelectionModel>("rowPeseeLotCertifie").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mPesee.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mPesee);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }
                    X.GetCmp<Container>("zonePalettes").Enable();
                    X.GetCmp<Hidden>("TxtPeseeLotCertifieID").SetValue(mPesee.ID);
                    X.GetCmp<Hidden>("hiddenExecMode").SetValue("Update");
                    //X.GetCmp<Window>("FormPeseeAvantUsinage").Close();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Certified Lot Weighing : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        public ActionResult SubmitWeighingPallet(string ItemSelected = "", string peseeID = "", string nbrSacs = "", string poidsPalette = "")
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                bool resultLot = false;
                PeseeLotCertifie mPesee = new PeseeLotCertifie();
                PeseeProductionPalette mPalette;
                DateTime datePesee = new DateTime();
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                formExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                mPesee.IsNew = false;
                Guid IdPesee = Guid.Empty;
                bool isGuid = Guid.TryParse(peseeID, out IdPesee);

                if (isGuid)
                    mPesee.fnGet(IdPesee);             

                if (mPesee == null || mPesee.ID == Guid.Empty)
                    throw new Exception("SubmitFormMethod : Weighing load failed.");

                bool result = false;
                bool resultPalette = true;
                datePesee = mPesee.DatePeseeProduction;
                mPesee = MapFormToObject(mPesee);

                if (mPesee.IsNew == false && (datePesee.Date == mPesee.DatePeseeProduction.Date))
                    mPesee.DatePeseeProduction = datePesee;

                LotType mLotType = new LotType();
                //mLotType.fnGet(mPesee.LotType.ID);
                Lot mLot = new Lot();

                if (mPesee.NombreSacs == mLotType.NombreSacs)
                {
                    #region Creation Du Lot                    

                    //mLot.Campagne = new Campagne();
                    //mLot.Exportateur = new Exportateur();
                    //mLot.Certification = new Certification();
                    //mLot.Production = new OrdreProduction();
                    //mLot.LotType = new LotType();

                    //_db = mLot.db();
                    //mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                    ////mPesee = MapFormToObject(mPesee);

                    //mLot.Campagne.Designation = mPesee.Campagne.Designation;
                    //mLot.Exportateur.ID = new Parametres(0).Exportateur.ID;
                    //mLot.Certification = null;
                    //mLot.Production = null;
                    //mLot.LotType.ID = mPesee.LotType.ID;
                    //mLot.DateLot = DateTime.Now;
                    //mLot.EstManuel = false;
                    //mLot.EstQueue = mPesee.NombreSacs >= mLotType.NombreSacs ? false : true;
                    //mLot.NombreSacs = mPesee.NombreSacs;
                    //mLot.PoidsBrut = mPesee.PoidsBrut;
                    //mLot.TareSacs = mPesee.TareSacs;
                    //mLot.TarePalette = mPesee.TarePalette;
                    //mLot.PoidsNet = mPesee.PoidsNet;
                    //mLot.UtilisateurCreation = (string)Session["userName"];
                    //mLot.IsNew = true;

                    //resultLot = mLot.fnUpdate(mtran);

                    //if (!resultLot)
                    //{
                    //    _db.RollBackTransaction(mtran);
                    //    return this.Direct();

                    //}
                    #endregion
                }

                //if (mLot.ID != Guid.Empty)
                //{
                //    mPesee.Lot = new Lot();
                //    mPesee.Lot.ID = mLot.ID;
                //    mPesee.Lot.NumeroLot = mLot.NumeroLot;
                //    mPesee.Statut = "AP";
                //    mPesee.SetDataSource(_db);
                //}
                //else
                //{
                //    _db = mPesee.db();
                //    mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                //}

                _db = mPesee.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = mPesee.fnUpdate(mtran);                

                if (result)
                {
                    List<PeseeProductionPalette> ListePalettesPesees = JSON.Deserialize<List<PeseeProductionPalette>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (ListePalettesPesees.Count > 0)
                    {
                        foreach (PeseeProductionPalette item in ListePalettesPesees.Where(p => p.IsNew == true))
                        {
                            mPalette = new PeseeProductionPalette();
                            mPalette.PeseeProduction = new PeseeLotCertifie();
                            mPalette.BonDeLivraison = new BonDeLivraison();

                            mPalette.SetDataSource(_db);
                            mPalette.ID = item.ID;
                            mPalette.PeseeProduction.ID = mPesee.ID;
                            mPalette.NombreSacs = item.NombreSacs;
                            mPalette.PoidsBrut = item.PoidsBrut;
                            mPalette.TareSacs = item.TareSacs;
                            mPalette.NumeroPalette = item.NumeroPalette;
                            mPalette.NumeroLivraison = item.NumeroLivraison;
                            mPalette.LivraisonImmatriculation = item.LivraisonImmatriculation;
                            mPalette.DatePesee = item.DatePesee;
                            mPalette.BonDeLivraison.ID = item.BonDeLivraison.ID;
                            mPalette.IsNew = item.IsNew;

                            mPalette.UtilisateurCreation = (string)Session["userName"];
                            mPalette.UtilisateurModification = (string)Session["userName"];
                            Store mstorePalette = X.GetCmp<Store>("storeListePoidsCertifie");
                            if (mPalette.IsNew)
                            {
                                resultPalette = mPalette.fnUpdate(mtran);

                                ModelProxy mProxyPalette = mstorePalette.GetById(item.ID);
                                item.IsNew = false;
                                mProxyPalette.BeginEdit();
                                mProxyPalette.Set(item);
                                mProxyPalette.Commit();
                                mProxyPalette.EndEdit();
                            }
                            //mPalette.IsNew = false;

                            if (!resultPalette)
                            {
                                _db.RollBackTransaction(mtran);
                                result = false;
                                break;
                            }
                        }
                    }
                    if (result)
                    {
                        _db.CommitTransaction(mtran);

                        Store mstore = X.GetCmp<Store>("storeListePeseeLotCertifie");
                        ModelProxy mProxy = mstore.GetById(mPesee.ID);
                        mProxy.BeginEdit();
                        mProxy.Set(mPesee);
                        mProxy.Commit();
                        mProxy.EndEdit();

                        //if (resultLot)
                        //{
                        //    X.GetCmp<Window>("FormPeseeLotCertifie").Close();
                        //    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                        //    X.Js.Call("GenerateTicket", mPesee.ID, BaseUrl);
                        //}
                            
                    }
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Certified Lot Weighing : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }


        public ActionResult FinalizeWeighing(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                Guid peseeID = Guid.Empty;
                LotType mLotType = new LotType();
                int SelectedLot = int.Parse(GetFormValue("cmbTypeLot"));

                bool resultLotTtpe = mLotType.fnGet(SelectedLot);

                if (mLotType == null || mLotType.ID == 0)
                    throw new Exception("FinalizeWeighing : Weighing load failed.");

                PeseeLotCertifie mPesee = new PeseeLotCertifie();
                PeseeProductionPalette palette;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                formExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                mPesee.IsNew = false;
                bool IsGuid = Guid.TryParse(GetFormValue("TxtPeseeLotCertifieID"), out peseeID);

                if (IsGuid)
                    mPesee.fnGet(peseeID);                

                if (mPesee == null || mPesee.ID == Guid.Empty)
                    throw new Exception("FinalizeWeighing : Weighing load failed.");

                bool result = false;
                bool resultPalette = true;
                bool resultLot = false;

                DateTime datePesee = new DateTime();
                datePesee = mPesee.DatePeseeProduction;

                mPesee = MapFormToObject(mPesee);

                if (mPesee.IsNew == false && (datePesee.Date == mPesee.DatePeseeProduction.Date))
                    mPesee.DatePeseeProduction = datePesee;

                if (mPesee.TarePalette <= 0)
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Certified Lot Weighing  : Data Validation",
                        Message = "Set Tare Of Paletts Before Finalize",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }

                #region Creation Du Lot
                Lot mLot = new Lot();

                mLot.Campagne = new Campagne();
                mLot.Exportateur = new Exportateur();
                mLot.Certification = new Certification();
                mLot.Production = new OrdreProduction();
                mLot.LotType = new LotType();

                _db = mLot.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                //mPesee = MapFormToObject(mPesee);

                mLot.Campagne.Designation = mPesee.Campagne.Designation;
                mLot.Exportateur.ID = new Parametres(0).Exportateur.ID;
                mLot.Certification = null;
                mLot.Production.ID = mPesee.OrdreProduction.ID;
                mLot.LotType.ID = mPesee.LotType.ID;
                mLot.DateLot = DateTime.Now;
                mLot.EstManuel = false;
                //mLot.EstQueue = mPesee.NombreSacs >= mLotType.NombreSacs ? false : true;
                mLot.EstQueue = false;
                mLot.NombreSacs = mPesee.NombreSacs;
                mLot.PoidsBrut = mPesee.PoidsBrut;
                mLot.TareSacs = mPesee.TareSacs;
                mLot.TarePalette = mPesee.TarePalette;
                mLot.PoidsNet = mPesee.PoidsNet;
                mLot.UtilisateurCreation = (string)Session["userName"];
                mLot.IsNew = true;

                resultLot = mLot.fnUpdate(mtran);
                #endregion                            

                if (resultLot)
                {
                    
                    mPesee.Lot = new Lot();
                    mPesee.Lot.ID = mLot.ID;
                    mPesee.Lot.NumeroLot = mLot.NumeroLot;
                    mPesee.Statut = "AP";
                    mPesee.SetDataSource(_db);

                    result = mPesee.fnUpdate(mtran);

                    if (result)
                    {
                        List<PeseeProductionPalette> item = JSON.Deserialize<List<PeseeProductionPalette>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                        if (item.Count > 0)
                        {

                            for (int i = 0; i < item.Count(); i++)
                            {
                                palette = new PeseeProductionPalette();
                                palette.PeseeProduction = new PeseeLotCertifie();

                                palette.SetDataSource(_db);

                                palette.PeseeProduction.ID = mPesee.ID;
                                palette.NombreSacs = item[i].NombreSacs;
                                palette.PoidsBrut = item[i].PoidsBrut;
                                palette.NumeroPalette = item[i].NumeroPalette;
                                palette.NumeroLivraison = item[i].NumeroLivraison;
                                palette.LivraisonImmatriculation = item[i].LivraisonImmatriculation;
                                palette.IsNew = item[i].IsNew;

                                palette.UtilisateurCreation = (string)Session["userName"];
                                palette.UtilisateurModification = (string)Session["userName"];
                                if (palette.IsNew)
                                {
                                    resultPalette = palette.fnUpdate(mtran);
                                }

                                if (!resultPalette)
                                {
                                    break;
                                }
                            }
                        }
                        else
                            resultPalette = false;
                    }
                }

                if (resultLot && result && resultPalette)
                {
                    _db.CommitTransaction(mtran);
                    Store mstore = X.GetCmp<Store>("storeListePeseeLotCertifie");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mPesee);
                        X.GetCmp<RowSelectionModel>("rowPeseeLotCertifie").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mPesee.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mPesee);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormPeseeLotCertifie").Close();

                    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                    X.Js.Call("GenerateTicket", mPesee.ID, BaseUrl);
                }
                else
                    _db.RollBackTransaction(mtran);
            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mtran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Certified Lot Weighing  : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        private PeseeLotCertifie MapFormToObject(PeseeLotCertifie mClass)
        {
            Campagne campagne = new Campagne();
            campagne.Designation = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text.ToString();
            mClass.Campagne = campagne;

            OrdreProduction mProduction = new OrdreProduction();
            mProduction.ID = Guid.Parse(GetFormValue("cmbProduction"));
            mProduction.NumeroProduction = X.GetCmp<ComboBox>("cmbProduction").SelectedItem.Text.ToString();
            mClass.OrdreProduction = mProduction;

            //Lot mLot = new Lot();
            //mLot.ID = Guid.Parse(GetFormValue("txtLotID"));
            //mLot.NumeroLot = X.GetCmp<TextField>("txtNumeroLot").Text;
            mClass.Lot = null;

            LotType lType = new LotType();
            lType.ID = int.Parse(GetFormValue("cmbTypeLot"));
            lType.Designation = X.GetCmp<ComboBox>("cmbTypeLot").SelectedItem.Text.ToString();
            mClass.LotType = lType;

            Certification certification = new Certification();
            certification.ID = int.Parse(GetFormValue("cmbCertification"));
            certification.Designation = X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Text.ToString();
            mClass.Certification = certification;

            FicheLotCertifie fiche = new FicheLotCertifie();
            fiche.ID = Guid.Parse(GetFormValue("cmbFicheLotCertifie"));
            fiche.Numero = X.GetCmp<ComboBox>("cmbFicheLotCertifie").SelectedItem.Text.ToString();
            mClass.FicheLotCertifie = fiche;

            mClass.DatePeseeProduction = DateTime.Parse(X.GetCmp<DateField>("txtDatePesee").RawText.ToString()).Add(DateTime.Now.TimeOfDay);
            mClass.Statut = "NA";

            mClass.PeseeProductionType = new PeseeProductionType();
            mClass.PeseeProductionType.ID = (new Parametres(0)).PeseeCertifiee;

            mClass.IsManual = bool.Parse(X.GetCmp<Hidden>("txtEstManuel").Text);

            if (X.GetCmp<TextField>("txtNombreSacs").Text != string.Empty) mClass.NombreSacs = int.Parse(X.GetCmp<TextField>("txtNombreSacs").Text);
            if (X.GetCmp<TextField>("txtPoidsBrut").Text != string.Empty) mClass.PoidsBrut = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrut").Text);
            if (X.GetCmp<TextField>("txtTareSacs").Text != string.Empty) mClass.TareSacs = decimal.Parse(X.GetCmp<TextField>("txtTareSacs").Text);

            if (X.GetCmp<TextField>("txtTarePalettes").Text != string.Empty) mClass.TarePalette = decimal.Parse(X.GetCmp<TextField>("txtTarePalettes").Text);
            //Prevenir Changement depuis la console du navigateur
            //if (!mClass.IsManual && (mClass.TarePalette != CapturedTare))
            //{
            //    throw new Exception("An error occured, please retry Weighing !");
            //}
            //if (X.GetCmp<TextField>("txtTarePalettes").Text != string.Empty) mClass.TarePalette = decimal.Parse(X.GetCmp<TextField>("txtTarePalettes").Text);
            if (X.GetCmp<TextField>("txtNetWeight").Text != string.Empty) mClass.PoidsNet = decimal.Parse(X.GetCmp<TextField>("txtNetWeight").Text);

            mClass.Commentaire = X.GetCmp<TextField>("txtComment").Text;
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }

        public ActionResult SelectPalletsWeight(string PeseeId)
        {
            try
            {
                Guid IdPesee = Guid.Parse(PeseeId);
                PeseeProductionPalette mPalettes = new PeseeProductionPalette();
                var Liste = mPalettes.fnSelect(IdPesee);
                return this.Store(Liste);
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Certified Lot Weighing : Data Validation",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Store(null);
            }

        }

        public ActionResult OnCaptureTare()
        {
            try
            {
                BtnReadClicked = true;
                CapturedTare = 0;
                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                //string ip = "198.168.8.101";
                // initialize services                      
                ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);

                int sWeight = proxy.GetWeight();
                //int sWeight = 200;
                X.GetCmp<TextField>("txtTarePalettes").Text = sWeight.ToString("n0");
                CapturedTare = sWeight;

            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            return this.Direct();
        }

        public ActionResult OnCaptureWeight(string sacs = "")
        {
            try
            {
                BtnReadClicked = true;
                CapturedWeight = 0;
                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                //string ip = "198.168.8.101";
                // initialize services                      
                ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);

                int sWeight = proxy.GetWeight();
                //int nbrSac = !string.IsNullOrEmpty(sacs) ? int.Parse(sacs) : 0;
                //int sWeight = nbrSac * 67;
                X.GetCmp<NumberField>("txtTare").Text = sWeight.ToString();
                CapturedWeight = sWeight;

            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            return this.Direct();
        }

        public ActionResult GetDeliveriesForWeighing(string ItemFiche)
        {
            try
            {
                Guid ficheID = ItemFiche == "" ? Guid.Empty : Guid.Parse(ItemFiche);
                var mListe = (new FicheLotCertifieLivraison()).fnSelectAvailable(ficheID);
                return this.Store(mListe);
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Fiche de lot Certifié : Data Validation",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
        }

        public ActionResult SubmitWeight(string ItemSelected = "",string NombreTotalSac = "", string CurrentNombreSacs = "",string TypeLotID = "")
        {
            int mIndex = 0;
            LotType mlotType = new LotType();
            bool result = mlotType.fnGet(int.Parse(TypeLotID));
            if (!result)
                return this.Direct();

            List<PeseeProductionPalette> ListePalettesPesees = JSON.Deserialize<List<PeseeProductionPalette>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            if (ListePalettesPesees != null) mIndex = ListePalettesPesees.Count;

            PeseeProductionPalette mPesee = new PeseeProductionPalette();
            mPesee.ID = Guid.NewGuid();
            int OlDNombreSacs = 0;
            int NombreSacsAutorises = 0;
            if (!string.IsNullOrEmpty(NombreTotalSac) && NombreTotalSac != "-1")
                NombreSacsAutorises = int.Parse(NombreTotalSac);

            if (!string.IsNullOrEmpty(CurrentNombreSacs) && CurrentNombreSacs != "-1")
                OlDNombreSacs = int.Parse(CurrentNombreSacs);

            mPesee.BonDeLivraison = new BonDeLivraison();
            if (X.GetCmp<Hidden>("txtBonLivraisonID").Text != string.Empty) mPesee.BonDeLivraison.ID = Guid.Parse(X.GetCmp<Hidden>("txtBonLivraisonID").Text);
            if (X.GetCmp<TextField>("txtNombreSacsPesees").Text != string.Empty) mPesee.NombreSacs = int.Parse(X.GetCmp<TextField>("txtNombreSacsPesees").Text);
            if (X.GetCmp<TextField>("txtTare").Text != string.Empty) mPesee.PoidsBrut = decimal.Parse(X.GetCmp<TextField>("txtTare").Text);
            if (X.GetCmp<TextField>("txtNumeroPalette").Text != string.Empty) mPesee.NumeroPalette = X.GetCmp<TextField>("txtNumeroPalette").Text;
            if (X.GetCmp<TextField>("txtLivraisonNumero").Text != string.Empty) mPesee.NumeroLivraison = X.GetCmp<TextField>("txtLivraisonNumero").Text;
            if (X.GetCmp<TextField>("txtLivraisonImmatriculation").Text != string.Empty) mPesee.LivraisonImmatriculation = X.GetCmp<TextField>("txtLivraisonImmatriculation").Text;
            mPesee.IsNew = true;
            mPesee.DatePesee = DateTime.Now;
            if ((OlDNombreSacs + mPesee.NombreSacs) > NombreSacsAutorises)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Certified Lot Weighing  : Data Validation",
                    Message = "Nbr de sacs added is higher than Authorized Nbr de sacs",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }

            if (mPesee.NombreSacs > 0 && mPesee.PoidsBrut >= 0)
            {

                if (mPesee.NombreSacs > mlotType.NombreSacsParPalettes)
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Certified Lot Weighing : Data Validation",
                        Message = "Verify Number Of Bags Per Pallets",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }

                if (ListePalettesPesees != null && (ListePalettesPesees.Count > mlotType.NombrePalette))
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Certified Lot Weighing : Data Validation",
                        Message = "Can't Weight New Pallets",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }

                if (ListePalettesPesees != null && ((ListePalettesPesees.Count > 0) && ((ListePalettesPesees.Sum(x => x.NombreSacs) + mPesee.NombreSacs) > mlotType.NombreSacs)))
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Certified Lot Weighing : Data Validation",
                        Message = "Total Nbr Of Bag is higher than authorized Number Of bags",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }
                Store mstore = X.GetCmp<Store>("storeListePoidsCertifie");
                mstore.Insert(mIndex, mPesee);
                X.GetCmp<RowSelectionModel>("rowPoidsCertifie").Select(mIndex);

                X.GetCmp<Window>("FormAddWeight").Close();
            }
            return this.Direct();
        }

        public ActionResult SetAvailableNbrSacs(string ItemListeAdded = "", string ItemListeToAdd = "")
        {
            if (!string.IsNullOrEmpty(ItemListeAdded) && ItemListeAdded != "[]" && !string.IsNullOrEmpty(ItemListeToAdd) && ItemListeToAdd != "[]")
            {
                List<PeseeProductionPalette> oldListe = JSON.Deserialize<List<PeseeProductionPalette>>(ItemListeAdded, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                List<FicheLotCertifieLivraison> newListe = JSON.Deserialize<List<FicheLotCertifieLivraison>>(ItemListeToAdd, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                int nbr = 0;
                foreach (FicheLotCertifieLivraison itemNew in newListe)
                {
                    nbr = 0;
                    foreach (PeseeProductionPalette itemOld in oldListe.Where(x => x.BonDeLivraison.ID == itemNew.BonDeLivraison.ID))
                    {
                        nbr += itemOld.NombreSacs;
                    }
                    if (itemNew.NombreSacsProduction != (nbr + itemNew.NombreSacsProduction))
                    {
                        itemNew.NombreSacsProduction = itemNew.NombreSacsLivraison - nbr;
                    }

                }

                Store mStoreMelange = X.GetCmp<Store>("storeListDeliveryNotes");
                mStoreMelange.RemoveAll();
                mStoreMelange.Add(newListe.Where(n => n.NombreSacsProduction > 0));
            }

            return this.Direct();
        }


        public ActionResult RemoveWeighing(string ItemSelected)
        {
            try
            {
                PeseeProductionPalette mPalette = JSON.Deserialize<PeseeProductionPalette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                if (mPalette.ID != Guid.Empty)
                {
                    Store mstore = X.GetCmp<Store>("storeListePoidsCertifie");

                    if (mPalette.IsNew)
                    {
                        ModelProxy _proxy = mstore.GetById(mPalette.ID);
                        _proxy.Drop();
                        return this.Direct();
                    }
                    else
                    {
                        //mPalette = new PeseeProductionPalette();
                        mPalette.fnGet(mPalette.ID);
                        if (mPalette == null || mPalette.ID == Guid.Empty)
                            throw new Exception("RemoveWeighing : Retirer Weighing failed.");

                        mPalette.UtilisateurModification = (string)Session["userName"];

                        bool result = mPalette.fnRemove();

                        if (result)
                        {
                            ModelProxy mProxy = mstore.GetById(mPalette.ID);
                            mProxy.Drop();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Certified Lot Weighing  - : Retirer Pallets Weight",
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

        public ActionResult OnRefreshForAvailableLot(string ItemCampagne, string ItemExportateur, string ItemType, string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeAvlbLot");

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

                X.GetCmp<FormPanel>("SelectLotCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Certified Lot Weighing  - Lot : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult LoadListOfAvailableLots(StoreRequestParameters parameters, string ItemCampagne, string ItemExportateur, string ItemType, string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd)
        {
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "-1" : ItemCampagne;
            int certificationID = GetCriteriaValue(ItemCertification);
            int TypeID = GetCriteriaValue(ItemType);
            int ExportateurID = GetCriteriaValue(ItemExportateur);
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            var mListe = (new Lot()).fnSelect(Campagne, ExportateurID, certificationID, TypeID, StartDate, EndDate);

            return this.Store(mListe);
        }


        public ActionResult SubmitSelectedLot(string ItemSelected)
        {
            Lot mclass = new Lot();
            mclass = JSON.Deserialize<Lot>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            X.GetCmp<Hidden>("txtLotID").SetValue(mclass.ID);
            X.GetCmp<Hidden>("txtLotNumero").SetValue(mclass.NumeroLot);
            X.GetCmp<TextField>("txtNumeroLot").SetValue(mclass.NumeroLot);
            X.GetCmp<Window>("FormPeseeCertifie_SelectLot").Close();
            return this.Direct();
        }

        public ActionResult OnSelectLot()
        {
            Lot mclass = new Lot();

            var StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            var EndDate = DateTime.Now.ToShortDateString();

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeCertifie_SelectLot", Model = mclass };
        }

        public ActionResult OnSelectDelivery(string ficheID)
        {
            Guid IdFiche = !string.IsNullOrEmpty(ficheID) ? Guid.Parse(ficheID) : Guid.Empty;
            //ViewData["ControllerName"] = "PeseeLotCertifie";
            FicheLotCertifie mClass = new FicheLotCertifie();
            mClass.ID = IdFiche;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "PeseeLotCertifie_Deliveries", Model = mClass };
        }

        public ActionResult SubmitDeliveryNote(string ItemSelected)
        {
            FicheLotCertifieLivraison mclass = new FicheLotCertifieLivraison();
            mclass = JSON.Deserialize<FicheLotCertifieLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            X.GetCmp<Hidden>("txtBonLivraisonID").SetValue(mclass.BonDeLivraison.ID);
            X.GetCmp<Hidden>("txtLivraisonImmatriculation").SetValue(mclass.BonDeLivraison.Immatriculation);
            X.GetCmp<TextField>("txtLivraisonNumero").SetValue(mclass.BonDeLivraison.LivraisonID);
            X.GetCmp<NumberField>("txtNombreSacsPesees").SetValue(mclass.NombreSacsProduction);
            X.GetCmp<Window>("PeseeLotCertifie_Deliveries").Close();  
                      
            return this.Direct();
        }

        public ActionResult OnPrintWeighingTicket(string IdPesee, string IsCopy)
        {
            string BaseUrl = "";
            bool ReportIsCopy = false;
            try
            {
                if (string.IsNullOrEmpty(IsCopy))
                    ReportIsCopy = true;

                if (string.IsNullOrEmpty(IdPesee))
                    return this.Direct();

                PeseeLotCertifie mPesee = new PeseeLotCertifie();
                bool result = mPesee.fnGet(Guid.Parse(IdPesee));
                if (result && mPesee.Statut != "AP")
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Ticket de Pesée",
                        Message = "Weighing Not Finalized",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }

                BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Ticket de Pesée",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Ticket{0}', '{1}/PeseeLotCertifie/ViewWeighingTicket?id={0}&IsCopy={2}', this, 'Ticket De pesée','')", IdPesee, BaseUrl, ReportIsCopy));
        }

        public ActionResult ViewWeighingTicket(string id, bool IsCopy)
        {
            try
            {
                XtraReport report = new TicketPeseeLotCertifie();

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["peseeID"].Value = id;
                report.Parameters["IsCopy"].Value = IsCopy;

                //ReportParam param = new ReportParam();
                //List<ReportParam> mListParam = new List<ReportParam>();
                //param.Name = "peseeID";
                //param.Value = id;
                //mListParam.Add(param);
                //param = new ReportParam();
                //param.Name = "IsCopy";
                //param.Value = IsCopy.ToString();
                //mListParam.Add(param);

                //string mParm = JSON.Serialize(mListParam);
                ViewData["Report"] = report;
                return View("ViewReportResult", ViewData = ViewData);
                //string reportFilePath = @"C:\Temp\Report1.repx";
                //report.SaveLayoutToXml(reportFilePath);            
                //string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                //string ip = "192.168.8.104";
                // initialize services
                //ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);

                //proxy.DirectPrintTicket(mParm, ReportToPrint);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing : Ticket",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            //PrintMethod.Print(report);          
            return View();
        }

        public ActionResult OnPrintWeighingList()
        {
            Parametres mParam = new Parametres(0);

            ViewData["Campagne"] = mParam.Campagne;
            ViewData["Titre"] = "Certified Lot Weighing";
            ViewData["actionToDo"] = "PrintList";
            ViewData["ControllerName"] = "PeseeLotCertifie";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "ProductionWeighing_Print", ViewData = ViewData };
        }

        public ActionResult PrintList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;

                report = new rptPeseeLotCertifieList() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["CampagneID"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Value.ToString();
                report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Text;

                report.Parameters["paramTypePesee"].Value = (new Parametres(0)).PeseeCertifiee;

                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateDebut").RawText);
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateFin").RawText);

                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/PeseeLotCertifie/ViewList', this, 'Certified Lot Weighing',''),App.ProductionWeighing_Print.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Certified Lot Weighing : Data Validation",
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