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
    public class PeseeDiverseController : BaseController
    {
        // GET: PeseeDiverse

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

        public ActionResult Index()
        {
            string campagne = new Parametres(0).Campagne;

            X.GetCmp<ComboBox>("cmbFiltreCampagne").SetValue(campagne);

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("txtFiltreDateDebut").RawText = StartDate;
            X.GetCmp<DateField>("txtFiltreDateFin").RawText = EndDate;

            X.GetCmp<FormPanel>("PeseeDiverseCP").SetTitle("Campagne : " + campagne + ", Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{753FEB9E-1264-42A4-94E8-62711029F301}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{3C1627EB-9F90-47FA-AF58-74E4FE5C0320}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{980FF542-514F-420C-9B29-04CA32C2109D}")))
                X.GetCmp<MenuItem>("mnuPrintWeihingList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintWeihingList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{C3B8924D-2B29-4EB6-913D-A76923440B34}")))
                X.GetCmp<MenuItem>("mnuExport").Enable();
            else
                X.GetCmp<MenuItem>("mnuExport").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{DE6C1676-5007-42AA-9512-D88EFABBBEAC}")))
                X.GetCmp<MenuItem>("bntAddManual").Enable();
            else
                X.GetCmp<MenuItem>("bntAddManual").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{7B19484B-DCF9-41DD-9696-261333A49E6E}")))
                X.GetCmp<Hidden>("phiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{32ED6299-889D-4E87-8E0B-F339F26A4867}")))
                X.GetCmp<Hidden>("phiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{C2952C7C-3FD7-4A8F-BEB8-56C1DB384D34}")))
                X.GetCmp<Hidden>("phiddenPermPrintSheet").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermPrintSheet").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{42B9F67D-5428-485C-8C31-271F7A19203F}")))
                X.GetCmp<Hidden>("phiddenPermFinalize").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermFinalize").SetValue(false);
            #endregion

            return View();
        }

        public ActionResult onAdd()
        {            
            PeseeDiverseViewModel mclass = new PeseeDiverseViewModel();
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{42B9F67D-5428-485C-8C31-271F7A19203F}"), UserName);

            mclass._PeseeDiverse = new PeseeDiverse();
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._PeseeDiverse.IsManual = false;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeDiverse", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onEdit(string ItemSelected)
        {            
            PeseeDiverseViewModel mclass = new PeseeDiverseViewModel();
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{42B9F67D-5428-485C-8C31-271F7A19203F}"), UserName);

            mclass._PeseeDiverse = new PeseeDiverse();
            mclass._PeseeDiverse = JSON.Deserialize<PeseeDiverse>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            CapturedTare = int.Parse(mclass._PeseeDiverse.TarePalette.ToString());
            BtnReadClicked = true;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            //mclass._PeseeDiverse.IsManual = false;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeDiverse", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onConsult(string ItemSelected)
        {            
            PeseeDiverseViewModel mclass = new PeseeDiverseViewModel();
            mclass._PeseeDiverse = new PeseeDiverse();
            ViewData["CanFinalize"] = false;
            mclass._PeseeDiverse = JSON.Deserialize<PeseeDiverse>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            CapturedTare = int.Parse(mclass._PeseeDiverse.TarePalette.ToString());
            BtnReadClicked = true;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
            //mclass._PeseeDiverse.IsManual = false;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeDiverse", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onManualWeight(string ItemSelected = "")
        {            
            PeseeDiverseViewModel mclass = new PeseeDiverseViewModel();
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{42B9F67D-5428-485C-8C31-271F7A19203F}"), UserName);

            mclass._PeseeDiverse = new PeseeDiverse();
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._PeseeDiverse.IsManual = true;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeDiverse", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onAddWeight(string IsManual = "")
        {
            PeseeDiverse pesee = new PeseeDiverse();
            if (!string.IsNullOrEmpty(IsManual))
                pesee.IsManual = bool.Parse(IsManual);
            else
                pesee.IsManual = false;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAddWeight", Model = pesee };
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
            var mListe = (new PeseeDiverse()).fnSelect(ItemCampagne, StartDate, EndDate,statut);

            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListePeseeDiverse");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCampagne",ItemCampagne),
                                    new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus",ItemStatus)
                                });

                X.GetCmp<FormPanel>("PeseeDiverseCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "General Weighing : Data Validation",
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
                PeseeDiverse mPesee = new PeseeDiverse();
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
                            mPalette.PeseeProduction = new PeseeDiverse();

                            mPalette.SetDataSource(_db);
                            mPalette.ID = item.ID;
                            mPalette.PeseeProduction.ID = mPesee.ID;
                            mPalette.NombreSacs = item.NombreSacs;
                            mPalette.PoidsBrut = item.PoidsBrut;
                            mPalette.NumeroPalette = item.NumeroPalette;
                            mPalette.NumeroLivraison = item.NumeroLivraison;
                            mPalette.LivraisonImmatriculation = item.LivraisonImmatriculation;
                            mPalette.DatePesee = item.DatePesee;
                            mPalette.IsNew = item.IsNew;

                            mPalette.UtilisateurCreation = (string)Session["userName"];
                            mPalette.UtilisateurModification = (string)Session["userName"];
                            Store mstorePalette = X.GetCmp<Store>("storeListePoidsDivers");
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

                        Store mstore = X.GetCmp<Store>("storeListePeseeDiverse");
                        ModelProxy mProxy = mstore.GetById(mPesee.ID);
                        mProxy.BeginEdit();
                        mProxy.Set(mPesee);
                        mProxy.Commit();
                        mProxy.EndEdit();
                        //X.GetCmp<Window>("FormAddWeight").Close();
                    }
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "General Weighing : Data Validation",
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
                PeseeDiverse mPesee = new PeseeDiverse();
                DateTime datePesee = new DateTime();
                Guid peseeID = Guid.Empty;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mPesee.IsNew = true;
                else
                {
                    formExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                    mPesee.IsNew = false;
                    bool isGuid = Guid.TryParse(GetFormValue("TxtPeseeDiverseID"), out peseeID);
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
                    Store mstore = X.GetCmp<Store>("storeListePeseeDiverse");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mPesee);
                        X.GetCmp<RowSelectionModel>("rowPeseeDiverse").Select(0);
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
                    X.GetCmp<Hidden>("TxtPeseeDiverseID").SetValue(mPesee.ID);
                    X.GetCmp<Hidden>("hiddenExecMode").SetValue("Update");
                    //X.GetCmp<Window>("FormPeseeAvantUsinage").Close();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "General Weighing : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }


        public ActionResult Finalize(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                PeseeDiverse mPesee = new PeseeDiverse();
                Guid peseeID = Guid.Empty;
                PeseeProductionPalette palette;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                formExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                mPesee.IsNew = false;
                bool IsGuid = Guid.TryParse(GetFormValue("TxtPeseeDiverseID"), out peseeID);

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

                List<PeseeProductionPalette> item = JSON.Deserialize<List<PeseeProductionPalette>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                if (mPesee.TarePalette <= 0 || item.Count <= 0)
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Weighing Before Cleaning : Data Validation",
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
                    
                    if (item.Count > 0)
                    {
                        for (int i = 0; i < item.Count(); i++)
                        {
                            palette = new PeseeProductionPalette();
                            palette.PeseeProduction = new PeseeDiverse();

                            palette.SetDataSource(_db);

                            palette.PeseeProduction.ID = mPesee.ID;
                            palette.NombreSacs = item[i].NombreSacs;
                            palette.PoidsBrut = item[i].PoidsBrut;
                            palette.NumeroPalette = item[i].NumeroPalette;
                            palette.NumeroLivraison = item[i].NumeroLivraison;
                            palette.LivraisonImmatriculation = item[i].LivraisonImmatriculation;
                            palette.DatePesee = item[i].DatePesee;
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
                    if (!resultPalette)
                    {
                        _db.RollBackTransaction(mtran);
                    }
                    _db.CommitTransaction(mtran);
                }


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePeseeDiverse");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mPesee);
                        X.GetCmp<RowSelectionModel>("rowPeseeDiverse").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mPesee.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mPesee);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormPeseeDiverse").Close();

                    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                    X.Js.Call("GenerateTicket", mPesee.ID, BaseUrl);
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "General Weighing : Data Validation",
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
                PeseeDiverse pesee = JSON.Deserialize<PeseeDiverse>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = pesee.fnGet(pesee.ID);

                if (!result)
                    throw new Exception("onCancel : General Weighing, loading failed.");

                pesee.UtilisateurModification = (string)Session["userName"];


                result = pesee.fnDeActivate();

                if (!result)
                    throw new Exception("onCancel : General Weighing, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePeseeDiverse");

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
                    Title = "General Weighing : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        private PeseeDiverse MapFormToObject(PeseeDiverse mClass)
        {
            Parametres mParam = new Parametres(0);
            Campagne campagne = new Campagne();
            campagne.Designation = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text.ToString();
            mClass.Campagne = campagne;

            //OrdreProduction production = new OrdreProduction();
            
            mClass.OrdreProduction = null;
            if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("cmbProduction").SelectedItem.Text))
            {
                mClass.OrdreProduction = new OrdreProduction();
                mClass.OrdreProduction.ID = Guid.Parse(GetFormValue("cmbProduction"));
                mClass.OrdreProduction.NumeroProduction = X.GetCmp<ComboBox>("cmbProduction").SelectedItem.Text.ToString();
            }
            else
                mClass.OrdreProduction = null;

            

            //Lot mLot = new Lot();
            //mLot.ID = Guid.Parse(GetFormValue("txtLotID"));
            //mLot.NumeroLot = X.GetCmp<TextField>("txtNumeroLot").Text;
            mClass.Lot = null;

            //LotType lType = new LotType();
            //lType.ID = int.Parse(GetFormValue("cmbTypeLot"));
            //lType.Designation = X.GetCmp<ComboBox>("cmbTypeLot").SelectedItem.Text.ToString();
            mClass.LotType = null;
            
            mClass.Certification = null;

            mClass.DatePeseeProduction = DateTime.Parse(X.GetCmp<DateField>("txtDatePesee").RawText.ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second).AddMilliseconds(DateTime.Now.Millisecond);
            mClass.Statut = "NA";

            PeseeProductionType typePesee = new PeseeProductionType();
            //typePesee.ID = int.Parse(GetFormValue("cmbTypePesee"));
            //typePesee.Designation = X.GetCmp<ComboBox>("cmbTypePesee").SelectedItem.Text.ToString();

            typePesee.ID = mParam.PeseeDiverse;
            //typePesee.Designation = X.GetCmp<ComboBox>("cmbTypePesee").SelectedItem.Text.ToString();
            mClass.PeseeProductionType = typePesee;

            mClass.IsManual = bool.Parse(X.GetCmp<Hidden>("txtEstManuel").Text);
            mClass.ReferenceObjet = X.GetCmp<TextField>("txtReference").Text;

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
                    Title = "Weighing Before Cleaning : Data Validation",
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
                    sWeight = 2000;
                    X.GetCmp<TextField>("txtTarePalettes").Text = sWeight.ToString("n0");
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            return this.Direct();
        }

        public ActionResult OnCaptureWeight()
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
                    X.GetCmp<NumberField>("txtTare").Text = sWeight.ToString();
                }
                else
                {
                    sWeight = 15000;
                    X.GetCmp<NumberField>("txtTare").Text = sWeight.ToString();
                }
                CapturedWeight = sWeight;                

            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            return this.Direct();
        }

        public ActionResult SubmitWeight(string ItemSelected = "", string NombreTotalSac = "", string CurrentNombreSacs = "")
        {
            int mIndex = 0;
            PeseeProductionPalette mPesee = new PeseeProductionPalette();
            mPesee.ID = Guid.NewGuid();
            int OlDNombreSacs = 0;
            int NombreSacsAutorises = 0;

            List<PeseeProductionPalette> ListePalettesPesees = JSON.Deserialize<List<PeseeProductionPalette>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            if (ListePalettesPesees != null) mIndex = ListePalettesPesees.Count;

            //if (!string.IsNullOrEmpty(NombreTotalSac) && NombreTotalSac != "-1")
            //    NombreSacsAutorises = int.Parse(NombreTotalSac);

            //if (!string.IsNullOrEmpty(CurrentNombreSacs) && CurrentNombreSacs != "-1")
            //    OlDNombreSacs = int.Parse(CurrentNombreSacs);
            mPesee.DatePesee = DateTime.Now;
            if (X.GetCmp<TextField>("txtNombreSacsPesees").Text != string.Empty) mPesee.NombreSacs = int.Parse(X.GetCmp<TextField>("txtNombreSacsPesees").Text);
            if (X.GetCmp<TextField>("txtTare").Text != string.Empty) mPesee.PoidsBrut = decimal.Parse(X.GetCmp<TextField>("txtTare").Text);
            mPesee.IsNew = true;

            //if ((OlDNombreSacs + mPesee.NombreSacs) > NombreSacsAutorises)
            //{
            //    X.MessageBox.Show(new MessageBoxConfig
            //    {
            //        Title = "General Weighing : Data Validation",
            //        Message = "Nbr de sacs added is higher than Authorized Nbr de sacs",
            //        Buttons = MessageBox.Button.OK,
            //        Icon = MessageBox.Icon.WARNING
            //    });
            //}

            if (mPesee.NombreSacs > 0 && mPesee.PoidsBrut > 0)
            {
                Store mstore = X.GetCmp<Store>("storeListePoidsDivers");
                mstore.Insert(mIndex, mPesee);
                X.GetCmp<RowSelectionModel>("rowPoidsDivers").Select(mIndex);

                X.GetCmp<Window>("FormAddWeight").Close();
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
                    Title = "General Weighing - : Retirer Pallets Weight",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnSelectProduction()
        {           
            return new Ext.Net.MVC.PartialViewResult { ViewName = "PeseeDiverse_Production" };
        }

        public ActionResult SubmitOrdreProduction(string ItemSelected = "", string ItemNumero = "")
        {
            try
            {
                OrdreProduction mclass = new OrdreProduction();

                if (!string.IsNullOrEmpty(ItemNumero))
                {
                    if (mclass.fnGetByNumber(ItemNumero))
                    {
                        if (mclass.ID != Guid.Empty)
                        {
                            X.GetCmp<Hidden>("txtOrdreProductionID").SetValue(mclass.ID);
                            X.GetCmp<Hidden>("txtOrdreProduction").SetValue(mclass.NumeroProduction);
                            X.GetCmp<Window>("PeseeDiverse_Production").Close();
                        }
                        else
                        {
                            X.GetCmp<Hidden>("txtOrdreProduction").SetValue("");
                            X.GetCmp<Hidden>("txtOrdreProductionID").SetValue(Guid.Empty);
                            X.MessageBox.Show(new MessageBoxConfig
                            {
                                Title = "Production : Production Order",
                                Message = "Production Order Not Found, Please Retry !",
                                Buttons = MessageBox.Button.OK,
                                Icon = MessageBox.Icon.WARNING
                            });
                            return this.Direct();
                        }
                    }
                }
                else
                {                    
                    mclass = JSON.Deserialize<OrdreProduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (mclass.ID != Guid.Empty)
                    {
                        X.GetCmp<Hidden>("txtOrdreProductionID").SetValue(mclass.ID);
                        X.GetCmp<Hidden>("txtOrdreProduction").SetValue(mclass.NumeroProduction);
                        X.GetCmp<Window>("PeseeDiverse_Production").Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production Order",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            
            return this.Direct();
        }

        //public ActionResult Select(StoreRequestParameters parameters, string ItemQuart, string ItemMelangeur, string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatut)
        //{
        //    int quartID = GetCriteriaValue(ItemQuart);
        //    int MelangeurID = GetCriteriaValue(ItemMelangeur);
        //    int CertificationID = GetCriteriaValue(ItemCertification);
        //    string Statut = string.IsNullOrEmpty(ItemStatut) ? "-1" : ItemStatut;

        //    DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
        //    DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

        //    var mListe = (new OrdreProduction()).fnSelect(quartID, MelangeurID, CertificationID, StartDate, EndDate, Statut);

        //    return this.Store(mListe);
        //}

        public ActionResult OnRefreshForProduction(string ItemQuart, string ItemMelangeur, string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatut)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeSelectOrdreProduction");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemQuart",ItemQuart),
                                    new Ext.Net.Parameter("ItemMelangeur",ItemMelangeur),
                                    new Ext.Net.Parameter("ItemCertification",ItemCertification),
                                    new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatut",ItemStatut)
                                });


                //X.GetCmp<FormPanel>("OrdreProductionCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production Order : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

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

                PeseeDiverse mPesee = new PeseeDiverse();
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

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Ticket{0}', '{1}/PeseeDiverse/ViewWeighingTicket?id={0}&IsCopy={2}', this, 'Ticket De pesée','')", IdPesee, BaseUrl, ReportIsCopy));
        }

        public ActionResult ViewWeighingTicket(string id, bool IsCopy)
        {
            try
            {
                XtraReport report = new TicketPeseeDiverse();

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
            ViewData["Titre"] = "General Weighing";
            ViewData["actionToDo"] = "PrintList";
            ViewData["ControllerName"] = "PeseeDiverse";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "ProductionWeighing_Print", ViewData = ViewData };
        }

        public ActionResult PrintList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;

                report = new rptPeseeDiverseList() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["CampagneID"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Value.ToString();
                report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Text;

                report.Parameters["paramTypePesee"].Value = (new Parametres(0)).PeseeCertifiee;

                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateDebut").RawText);
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateFin").RawText);

                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/PeseeDiverse/ViewList', this, 'General Weighing',''),App.ProductionWeighing_Print.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "General Weighing : Data Validation",
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

        private int GetCriteriasValue(string strComponent)
        {
            if (X.GetCmp<ComboBox>(strComponent) != null && X.GetCmp<ComboBox>(strComponent).SelectedItem != null && X.GetCmp<ComboBox>(strComponent).SelectedItem.Value != null)
                return int.Parse(X.GetCmp<ComboBox>(strComponent).SelectedItem.Value.ToString());
            else
                return -1;
        }


        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("PeseeDiverseCP");
            mform.ToggleCollapse();
            //mform.Collapsed = false;
            return this.Direct();
        }
    }
}