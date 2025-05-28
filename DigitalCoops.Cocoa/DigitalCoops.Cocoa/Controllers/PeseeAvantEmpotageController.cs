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
using Tms.Classes.Business.Sales;
using Tms.Classes.Business.stock;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class PeseeAvantEmpotageController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";


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

        // GET: PeseeAvantEmpotage
        public ActionResult Index()
        {
            string campagne = new Parametres(0).Campagne;

            X.GetCmp<ComboBox>("cmbFiltreCampagne").SetValue(campagne);

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("txtFiltreDateDebut").RawText = StartDate;
            X.GetCmp<DateField>("txtFiltreDateFin").RawText = EndDate;

            X.GetCmp<FormPanel>("PeseeAvantEmpotageCP").SetTitle("Campagne : " + campagne + ", Du : " + StartDate + " Au : " + EndDate);
            #region Set Function's Access

            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{28B27280-C454-4CAD-850C-17552F0BC6F5}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{269EA67D-4883-4429-8AF4-4846DC569CD9}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{FC499980-6357-4FFB-84F5-77B1171B1F02}")))
                X.GetCmp<MenuItem>("mnuPrintWeihingList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintWeihingList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{78327414-BF48-474B-8317-3668898C4C1B}")))
                X.GetCmp<MenuItem>("mnuExport").Enable();
            else
                X.GetCmp<MenuItem>("mnuExport").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{D42272F8-C28A-4F6D-B9B8-7FC9ECE0B3E0}")))
                X.GetCmp<MenuItem>("bntAddManual").Enable();
            else
                X.GetCmp<MenuItem>("bntAddManual").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{90276AF1-06EF-4C3A-A649-3D83CD59EA7C}")))
                X.GetCmp<Hidden>("phiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{E4A05C34-966C-47E3-A3AF-3284E2BB632E}")))
                X.GetCmp<Hidden>("phiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{C9CAE90B-18E4-4FD2-9C0D-B65B584F2FE7}")))
                X.GetCmp<Hidden>("phiddenPermPrintSheet").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermPrintSheet").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{AF740771-D5E8-4696-B580-3EC3DFBA90A9}")))
                X.GetCmp<Hidden>("phiddenPermFinalize").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermFinalize").SetValue(false);
            #endregion

            return View();
        }

        public ActionResult onAdd()
        {            
            PeseeAvantEmpotageViewModel mclass = new PeseeAvantEmpotageViewModel();
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{AF740771-D5E8-4696-B580-3EC3DFBA90A9}"), UserName);

            mclass._PeseeAvantEmpotage = new PeseeAvantEmpotage();
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._PeseeAvantEmpotage.IsManual = false;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeAvantEmpotage", Model = mclass, ViewData = ViewData };

        }

        public ActionResult onEdit(string ItemSelected)
        {            
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{AF740771-D5E8-4696-B580-3EC3DFBA90A9}"), UserName);

            PeseeAvantEmpotageViewModel mclass = new PeseeAvantEmpotageViewModel();
            mclass._PeseeAvantEmpotage = new PeseeAvantEmpotage();
            mclass._PeseeAvantEmpotage = JSON.Deserialize<PeseeAvantEmpotage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            //CapturedTare = int.Parse(mclass._PeseeAvantEmpotage.TarePalette.ToString());
            //BtnReadClicked = true;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            //mclass._PeseeAvantEmpotage.IsManual = false;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeAvantEmpotage", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onConsult(string ItemSelected)
        {
            string UserName = (string)Session["userName"];
            PeseeAvantEmpotageViewModel mclass = new PeseeAvantEmpotageViewModel();
            mclass._PeseeAvantEmpotage = new PeseeAvantEmpotage();

            ViewData["CanFinalize"] = false;

            mclass._PeseeAvantEmpotage = JSON.Deserialize<PeseeAvantEmpotage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            //CapturedTare = int.Parse(mclass._PeseeAvantEmpotage.TarePalette.ToString());
            //BtnReadClicked = true;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
            //mclass._PeseeAvantEmpotage.IsManual = false;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeAvantEmpotage", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onManualWeight(string ItemSelected = "")
        {            
            PeseeAvantEmpotageViewModel mclass = new PeseeAvantEmpotageViewModel();
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{AF740771-D5E8-4696-B580-3EC3DFBA90A9}"), UserName);

            mclass._PeseeAvantEmpotage = new PeseeAvantEmpotage();
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._PeseeAvantEmpotage.IsManual = true;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeAvantEmpotage", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onAddWeight(string IsManual = "")
        {
            PeseeAvantEmpotage pesee = new PeseeAvantEmpotage();
            if (!string.IsNullOrEmpty(IsManual))
                pesee.IsManual = bool.Parse(IsManual);
            else
                pesee.IsManual = false;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAddWeight", Model = pesee };
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("PeseeAvantEmpotageCP");
            mform.ToggleCollapse();            
            return this.Direct();
        }

        public ActionResult OnSelectStuffing()
        {
            Parametres mParam = new Parametres(0);
            ViewData["Campagne"] = mParam.Campagne;
            ViewData["Exportateur"] = mParam.Exportateur.ID;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "PeseeAvantEmpotage_SelectEmpotage", ViewData = ViewData };
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

            var mListe = (new PeseeAvantEmpotage()).fnSelect(ItemCampagne, StartDate, EndDate, statut);

            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListePeseeAvantEmpotage");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCampagne",ItemCampagne),
                                    new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus",ItemStatus)
                                });

                X.GetCmp<FormPanel>("PeseeAvantEmpotageCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing Before Stuffing : Data Validation",
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
                PeseeAvantEmpotage pesee = JSON.Deserialize<PeseeAvantEmpotage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = pesee.fnGet(pesee.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Weighing Before Stuffing, loading failed.");

                pesee.UtilisateurModification = (string)Session["userName"];


                result = pesee.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Weighing Before Stuffing, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePeseeAvantEmpotage");

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
                    Title = "Weighing Before Stuffing : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult Finalize(string ItemSelected, string NbrSacsConteneur = "")
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                PeseeAvantEmpotage mPesee = new PeseeAvantEmpotage();
                PeseeProductionPalette palette;
                Guid peseeID = Guid.Empty;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                formExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                mPesee.IsNew = false;
                bool IsGuid = Guid.TryParse(GetFormValue("TxtPeseeAvantEmpotageID"), out peseeID);

                if (IsGuid)
                    mPesee.fnGet(peseeID);

                if (mPesee == null || mPesee.ID == Guid.Empty)
                    throw new Exception("SubmitFormMethod : Weighing load failed.");
                           
                bool result = false;
                bool resultPalette = true;

                DateTime datePesee = new DateTime();
                datePesee = mPesee.DatePeseeProduction;

                mPesee = MapFormToObject(mPesee);
                if (mPesee.IsNew == false && (datePesee.Date == mPesee.DatePeseeProduction.Date))
                    mPesee.DatePeseeProduction = datePesee;
                
                int NombreSacsConteneur = 0;
                bool IsInt = int.TryParse(NbrSacsConteneur, out NombreSacsConteneur);

                if (mPesee.TarePalette <= 0)
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Weighing Before Stuffing : Data Validation",
                        Message = "Set Tare Des Palettes Before Finalize",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }
                
                mPesee.Statut = "AP";

                _db = mPesee.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = mPesee.fnUpdate(mtran);

                if (result)
                {
                    List<PeseeProductionPalette> item = JSON.Deserialize<List<PeseeProductionPalette>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (item.Count > 0)
                    {

                        for (int i = 0; i < item.Count(); i++)
                        {
                            palette = new PeseeProductionPalette();
                            palette.PeseeProduction = new PeseeAvantEmpotage();

                            palette.SetDataSource(_db);

                            palette.PeseeProduction.ID = mPesee.ID;
                            palette.NombreSacs = item[i].NombreSacs;
                            palette.PoidsBrut = item[i].PoidsBrut;
                            palette.IsNew = item[i].IsNew;
                            //palette.NumeroLivraison = item[i].NumeroLivraison;
                            //palette.LivraisonImmatriculation = item[i].LivraisonImmatriculation;
                            palette.DatePesee = item[i].DatePesee;

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
                    if (!resultPalette)
                    {
                        _db.RollBackTransaction(mtran);
                    }
                    else
                        _db.CommitTransaction(mtran);
                }                
                
                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePeseeAvantEmpotage");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mPesee);
                        X.GetCmp<RowSelectionModel>("rowPeseeAvantEmpotage").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mPesee.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mPesee);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormPeseeAvantEmpotage").Close();

                    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                    X.Js.Call("GenerateTicket", mPesee.ID, BaseUrl);
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing Before Stuffing : Data Validation",
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
                PeseeAvantEmpotage mPesee = new PeseeAvantEmpotage();
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mPesee.IsNew = true;
                else
                {
                    formExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                    mPesee.IsNew = false;
                    bool isGuid = Guid.TryParse(GetFormValue("TxtPeseeAvantEmpotageID"), out peseeID);
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
                    Store mstore = X.GetCmp<Store>("storeListePeseeAvantEmpotage");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mPesee);
                        X.GetCmp<RowSelectionModel>("rowPeseeAvantEmpotage").Select(0);
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
                    X.GetCmp<Hidden>("TxtPeseeAvantEmpotageID").SetValue(mPesee.ID);
                    X.GetCmp<Hidden>("hiddenExecMode").SetValue("Update");
                    //X.GetCmp<Panel>("PanelBtnSave").Hide();
                    //X.GetCmp<Window>("FormPeseeAvantEmpotage").Close();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing Before Sttuffing : Data Validation",
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
                PeseeAvantEmpotage mPesee = new PeseeAvantEmpotage();
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

                //LotType mLotType = new LotType();
                //mLotType.fnGet(mPesee.LotType.ID);                

                //if (mPesee.NombreSacs == mLotType.NombreSacs)
                //    mPesee.Statut = "AP";

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
                            mPalette.PeseeProduction = new PeseeAvantEmpotage();                            

                            mPalette.SetDataSource(_db);
                            mPalette.SetDataSource(_db);
                            mPalette.ID = item.ID;
                            mPalette.PeseeProduction.ID = mPesee.ID;
                            mPalette.NombreSacs = item.NombreSacs;
                            mPalette.PoidsBrut = item.PoidsBrut;
                            mPalette.DatePesee = item.DatePesee;
                            mPalette.IsNew = item.IsNew;
                            mPalette.UtilisateurCreation = (string)Session["userName"];
                            mPalette.UtilisateurModification = (string)Session["userName"];
                            Store mstorePalette = X.GetCmp<Store>("storeListeWeightPAU");
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

                        Store mstore = X.GetCmp<Store>("storeListePeseeAvantEmpotage");
                        ModelProxy mProxy = mstore.GetById(mPesee.ID);
                        mProxy.BeginEdit();
                        mProxy.Set(mPesee);
                        mProxy.Commit();
                        mProxy.EndEdit();
                        //X.GetCmp<Window>("FormAddWeight").Close();

                        if (mPesee.Statut == "AP")
                        {
                            X.GetCmp<Window>("FormPeseeApresEmpotage").Close();
                            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                            X.Js.Call("GenerateTicket", mPesee.ID, BaseUrl);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mtran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing Before Stuffing : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }


        private PeseeAvantEmpotage MapFormToObject(PeseeAvantEmpotage mClass)
        {
            Campagne campagne = new Campagne();
            campagne.Designation = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text.ToString();
            mClass.Campagne = campagne;

            Empotage mEmpotage = new Empotage();
            mEmpotage.ID = Guid.Parse(GetFormValue("TxtEmpotageID"));
            mEmpotage.Embarquement = new Embarquement();
            mEmpotage.Embarquement.Numero = X.GetCmp<TextField>("TxtNumeroShipment").Text;
            //mEmpotage.NumeroProduction = X.GetCmp<ComboBox>("cmbProduction").SelectedItem.Text.ToString();
            mClass.Empotage = mEmpotage;

            Conteneur mConteneur = new Conteneur();
            mConteneur.ID = Guid.Parse(GetFormValue("cmbConteneur"));
            mConteneur.Numero = X.GetCmp<ComboBox>("cmbConteneur").SelectedItem.Text.ToString();
            mClass.Conteneur = mConteneur;

            LotType lType = new LotType();
            lType.ID = int.Parse(GetFormValue("cmbTypeLot"));
            lType.Designation = X.GetCmp<ComboBox>("cmbTypeLot").SelectedItem.Text.ToString();            
            mClass.LotType = lType;

            mClass.DatePeseeProduction = DateTime.Parse(X.GetCmp<DateField>("txtDatePesee").RawText.ToString()).Add(DateTime.Now.TimeOfDay);
            mClass.Statut = "NA";

            mClass.PeseeProductionType = new PeseeProductionType();
            mClass.PeseeProductionType.ID = (new Parametres(0)).PeseeAvantEmpotage;
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

        public ActionResult RemoveWeighing(string ItemSelected)
        {
            try
            {
                PeseeProductionPalette mPalette = JSON.Deserialize<PeseeProductionPalette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                if (mPalette.ID != Guid.Empty)
                {
                    Store mstore = X.GetCmp<Store>("storeListeWeightPAU");

                    if (mPalette.IsNew)
                    {
                        ModelProxy _proxy = mstore.GetById(mPalette.ID);
                        _proxy.Drop();
                        return this.Direct();
                    }
                    else
                    {
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
                    Title = "Weighing Before Stuffing - : Retirer Pallets Weight",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
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
                    Title = "Weighing Before Stuffing : Data Validation",
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
                Parametres mParam = new Parametres(0);
                BtnReadClicked = true;
                CapturedTare = 0;
                int sWeight = 0;
                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                if (mParam.IsInDevelopment == false)
                {
                    //string ip = "198.168.8.101";
                    // initialize services                      
                    ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);
                    sWeight = proxy.GetWeight();
                    X.GetCmp<TextField>("txtTarePalettes").Text = sWeight.ToString("n0");
                }
                else
                {
                    sWeight = 200;
                    X.GetCmp<TextField>("txtTarePalettes").Text = sWeight.ToString("n0");
                }
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
                Parametres mParam = new Parametres(0);
                BtnReadClicked = true;
                CapturedWeight = 0;
                int sWeight = 0;
                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                //string ip = "198.168.8.101";
                // initialize services     
                if (mParam.IsInDevelopment == false)
                {
                    ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);

                    sWeight = proxy.GetWeight();
                    //int nbrSac = !string.IsNullOrEmpty(sacs) ? int.Parse(sacs) : 0;
                    //int sWeight = nbrSac * 67;
                    X.GetCmp<NumberField>("txtPoidsBrutPAU").Text = sWeight.ToString();
                }
                else
                {
                    int nbrSac = !string.IsNullOrEmpty(sacs) ? int.Parse(sacs) : 0;
                    sWeight = nbrSac * 67;
                    X.GetCmp<NumberField>("txtPoidsBrutPAU").Text = sWeight.ToString();
                }
                CapturedWeight = sWeight;               

            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            return this.Direct();
        }       

        public ActionResult SetCertification(string certification = "")
        {
            Certification mCertification = new Certification();
            if (!string.IsNullOrEmpty(certification))
            {
                mCertification = JSON.Deserialize<Certification>(certification, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                X.GetCmp<ComboBox>("cmbCertification").SetValue(mCertification.ID);
            }else
                X.GetCmp<ComboBox>("cmbCertification").SetValue((int?)null);

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

                PeseeAvantEmpotage mPesee = new PeseeAvantEmpotage();
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

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Ticket{0}', '{1}/PeseeAvantEmpotage/ViewWeighingTicket?id={0}&IsCopy={2}', this, 'Ticket De pesée','')", IdPesee, BaseUrl, ReportIsCopy));
        }

        public ActionResult ViewWeighingTicket(string id, bool IsCopy)
        {
            try
            {
                XtraReport report = new TicketPeseeAvantEmpotage();

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

        public ActionResult SubmitWeightPAU(string ItemSelected = "", string ContainerNbrOfBags = "")
        {
            int mIndex = 0;
            Parametres mParam = new Parametres(0);
            List<PeseeProductionPalette> ListePalettesPesees = JSON.Deserialize<List<PeseeProductionPalette>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            if (ListePalettesPesees != null) mIndex = ListePalettesPesees.Count;

            int TotalConteneurNbrSacs = 0;
            if (!string.IsNullOrEmpty(ContainerNbrOfBags))
                TotalConteneurNbrSacs = int.Parse(ContainerNbrOfBags);

            PeseeProductionPalette mPesee = new PeseeProductionPalette();
            
            mPesee.ID = Guid.NewGuid();
            if (X.GetCmp<TextField>("txtNombreSacsPAU").Text != string.Empty) mPesee.NombreSacs = int.Parse(X.GetCmp<TextField>("txtNombreSacsPAU").Text);
            if (X.GetCmp<TextField>("txtPoidsBrutPAU").Text != string.Empty) mPesee.PoidsBrut = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrutPAU").Text);

            decimal tareSacs = 0;

            if (X.GetCmp<Hidden>("txtTareUnitaire").Text != string.Empty) tareSacs = decimal.Parse(X.GetCmp<Hidden>("txtTareUnitaire").Text);
            //mPesee.TareSacs = mPesee.NombreSacs * tareSacs;
            mPesee.IsNew = true;
            mPesee.DatePesee = DateTime.Now;

            if (mPesee.NombreSacs > 0 && mPesee.PoidsBrut > 0)
            {
                //if (mPesee.NombreSacs > TotalConteneurNbrSacs)
                //{
                //    X.MessageBox.Show(new MessageBoxConfig
                //    {
                //        Title = "Weighing Before Stuffing : Data Validation",
                //        Message = "Verify Number Of Bags",
                //        Buttons = MessageBox.Button.OK,
                //        Icon = MessageBox.Icon.WARNING
                //    });
                //    return this.Direct();
                //}

                //if (mPesee.NombreSacs > mParam.SacsLivraisonsAutoriseParPalette)
                //{
                //    X.MessageBox.Show(new MessageBoxConfig
                //    {
                //        Title = "Weighing Before Stuffing : Data Validation",
                //        Message = "Verify Number Of Bags Per Pallet",
                //        Buttons = MessageBox.Button.OK,
                //        Icon = MessageBox.Icon.WARNING
                //    });
                //    return this.Direct();
                //}

                if (ListePalettesPesees != null && (ListePalettesPesees.Count > mParam.NombrePalettesAutoriseAuPesePalette))
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Weighing Before Stuffing : Data Validation",
                        Message = "Can't Weight New Pallet",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }

                //if (ListePalettesPesees != null && ((ListePalettesPesees.Count > 0) && ((ListePalettesPesees.Sum(x => x.NombreSacs) + mPesee.NombreSacs) > TotalConteneurNbrSacs)))
                //{
                //    X.MessageBox.Show(new MessageBoxConfig
                //    {
                //        Title = "Weighing Before Stuffing : Data Validation",
                //        Message = "Total Nbr Of Bag is higher than authorized Number Of bags",
                //        Buttons = MessageBox.Button.OK,
                //        Icon = MessageBox.Icon.WARNING
                //    });
                //    return this.Direct();
                //}

                Store mstore = X.GetCmp<Store>("storeListeWeightPAU");
                mstore.Insert(mIndex, mPesee);
                X.GetCmp<RowSelectionModel>("rowWeightPAU").Select(mIndex);

                X.GetCmp<Window>("FormAddWeight").Close();
            }
            return this.Direct();
        }

        public ActionResult OnPrintWeighingList()
        {
            Parametres mParam = new Parametres(0);

            ViewData["Campagne"] = mParam.Campagne;
            ViewData["Titre"] = "Weighing Before Stuffing";
            ViewData["actionToDo"] = "PrintList";
            ViewData["ControllerName"] = "PeseeAvantEmpotage";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "ProductionWeighing_Print", ViewData = ViewData };
        }

        public ActionResult PrintList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;

                report = new rptPeseeAvantEmpotageList() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["CampagneID"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Value.ToString();
                report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Text;

                report.Parameters["paramTypePesee"].Value = (new Parametres(0)).PeseeAvantEmpotage;

                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateDebut").RawText);
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateFin").RawText);

                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/PeseeAvantEmpotage/ViewList', this, 'Weighing Before Stuffing',''),App.ProductionWeighing_Print.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing Before Stuffing : Data Validation",
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

        public ActionResult LoadListOfStuffing(StoreRequestParameters parameters, string ItemCampagne, string ItemExportateur, string ItemPeriodStart, string ItemPeriodEnd)
        {

            int ExportateurID = GetCriteriaValue(ItemExportateur);
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "" : ItemCampagne;

            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            int Status = 0;

            var mListe = (new Empotage()).fnSelectForWeighing(Campagne, ExportateurID, StartDate, EndDate, Status);            
            return this.Store(mListe);
        }

        public ActionResult OnRefreshStuffing(string ItemCampagne, string ItemExportateur, string ItemPeriodStart, string ItemPeriodEnd)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeSelectEmpotage");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemExportateur"   ,ItemExportateur),
                                    new Ext.Net.Parameter("ItemCampagne"      ,ItemCampagne),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd)                                    
                                });



                // collapse criterias areas
                X.GetCmp<FormPanel>("SelectStuffingCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stuffing : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitEmpotage(string ItemSelected)
        {
            Empotage mclass = new Empotage();
            mclass = JSON.Deserialize<Empotage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            X.GetCmp<Hidden>("TxtEmpotageID").SetValue(mclass.ID);            
            X.GetCmp<TextField>("TxtNumeroShipment").SetValue(mclass.EmbarquementAsString);            
            X.GetCmp<Window>("PeseeAvantEmpotage_SelectEmpotage").Close();

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

        private int GetCriteriaValue(string strComponent)
        {
            int value;

            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
                return value;
            else
                return -1;
        }
    }
}