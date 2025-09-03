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
    public class PeseeApresUsinageController : BaseController
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

        // GET: PeseeApresUsinage
        public ActionResult Index()
        {
            string campagne = new Parametres(0).Campagne;

            X.GetCmp<ComboBox>("cmbFiltreCampagne").SetValue(campagne);

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("txtFiltreDateDebut").RawText = StartDate;
            X.GetCmp<DateField>("txtFiltreDateFin").RawText = EndDate;

            X.GetCmp<FormPanel>("PeseeApresUsinageCP").SetTitle("Campagne : " + campagne + ", Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{029AA1CF-26EF-4F4F-95DA-F937E72231F8}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{64452555-694B-4E8F-8DDC-D484F225B890}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{F51AF1B6-CF32-4C5B-B073-8ECF15C69448}")))
                X.GetCmp<MenuItem>("mnuPrintWeihingList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintWeihingList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{373D0129-C905-482B-96E0-5C00B9596DCF}")))
                X.GetCmp<MenuItem>("mnuExport").Enable();
            else
                X.GetCmp<MenuItem>("mnuExport").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{9F9507B8-CFA1-434F-ABDB-86CDB47CEDB6}")))
                X.GetCmp<MenuItem>("bntAddManual").Enable();
            else
                X.GetCmp<MenuItem>("bntAddManual").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{85EECA91-106A-4AF3-9911-01E8F50D6133}")))
                X.GetCmp<Hidden>("phiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{AA03777C-7375-4B61-9F69-00E202B7393F}")))
                X.GetCmp<Hidden>("phiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{09BE7249-9EBA-4D2F-92EE-448D93B9DE5E}")))
                X.GetCmp<Hidden>("phiddenPermPrintSheet").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermPrintSheet").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{CB110810-CE1D-4920-B400-1BF6BED68211}")))
                X.GetCmp<Hidden>("phiddenPermFinalize").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermFinalize").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{ab2c611c-f77e-40d4-ad24-ba0165c0720d}")))
                X.GetCmp<Hidden>("phiddenPermSupDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermSupDesactiver").SetValue(false);
            #endregion
            return View();
        }

        public ActionResult onAdd()
        {
            //WeighingIsCreated = false;
            PeseeApresUsinageViewModel mclass = new PeseeApresUsinageViewModel();
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{CB110810-CE1D-4920-B400-1BF6BED68211}"), UserName);

            mclass._PeseeApresUsinage = new PeseeApresUsinage();
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._PeseeApresUsinage.IsManual = false;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeApresUsinage", Model = mclass, ViewData = ViewData };

        }

        public ActionResult onEdit(string ItemSelected)
        {
            //WeighingIsCreated = true;
            PeseeApresUsinageViewModel mclass = new PeseeApresUsinageViewModel();
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{CB110810-CE1D-4920-B400-1BF6BED68211}"), UserName);

            mclass._PeseeApresUsinage = new PeseeApresUsinage();
            mclass._PeseeApresUsinage = JSON.Deserialize<PeseeApresUsinage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            CapturedTare = int.Parse(mclass._PeseeApresUsinage.TarePalette.ToString());
            BtnReadClicked = true;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            //mclass._PeseeApresUsinage.IsManual = false;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeApresUsinage", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onConsult(string ItemSelected)
        {
            //WeighingIsCreated = true;
            PeseeApresUsinageViewModel mclass = new PeseeApresUsinageViewModel();
            ViewData["CanFinalize"] = false;

            mclass._PeseeApresUsinage = new PeseeApresUsinage();
            mclass._PeseeApresUsinage = JSON.Deserialize<PeseeApresUsinage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            CapturedTare = int.Parse(mclass._PeseeApresUsinage.TarePalette.ToString());
            BtnReadClicked = true;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
            //mclass._PeseeApresUsinage.IsManual = false;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeApresUsinage", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onManualWeight(string ItemSelected = "")
        {
            //WeighingIsCreated = false;
            PeseeApresUsinageViewModel mclass = new PeseeApresUsinageViewModel();
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{CB110810-CE1D-4920-B400-1BF6BED68211}"), UserName);

            mclass._PeseeApresUsinage = new PeseeApresUsinage();
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._PeseeApresUsinage.IsManual = true;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeApresUsinage", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onAddWeight(string IsManual = "")
        {
            PeseeApresUsinage pesee = new PeseeApresUsinage();
            if (!string.IsNullOrEmpty(IsManual))
                pesee.IsManual = bool.Parse(IsManual);
            else
                pesee.IsManual = false;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAddPalletsWeight", Model = pesee };

        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("PeseeApresUsinageCP");
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

            var mListe = (new PeseeApresUsinage()).fnSelect(ItemCampagne, StartDate, EndDate, statut);

            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListePeseeApresUsinage");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCampagne",ItemCampagne),
                                    new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus",ItemStatus)
                                });

                X.GetCmp<FormPanel>("PeseeApresUsinageCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Pesée Apres Usinage : Data Validation",
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
                PeseeApresUsinage pesee = JSON.Deserialize<PeseeApresUsinage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = pesee.fnGet(pesee.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Pesée Apres Usinage, loading failed.");

                pesee.UtilisateurModification = (string)Session["userName"];


                result = pesee.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Pesée Apres Usinage, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePeseeApresUsinage");

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
                    Title = "Pesée Apres Usinage : Cancel",
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
                LotType mLotType = new LotType();
                int SelectedLot = int.Parse(GetFormValue("cmbTypeLot"));

                bool resultLotTtpe = mLotType.fnGet(SelectedLot);

                if (mLotType == null || mLotType.ID == 0)
                    throw new Exception("FinalizeWeighing : Weighing load failed.");

                PeseeApresUsinage mPesee = new PeseeApresUsinage();
                PeseeProductionPalette palette;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mPesee.IsNew = true;
                else
                {
                    mPesee.IsNew = false;
                    mPesee.fnGet(Guid.Parse(GetFormValue("TxtPeseeApresUsID")));

                    if (mPesee == null || mPesee.ID == Guid.Empty)
                        throw new Exception("FinalizeWeighing : Weighing load failed.");
                }

                bool result = false;
                bool resultPalette = true;
                bool resultLot = false;
                                
                #region Creation Du Lot
                Lot mLot = new Lot();
                
                mLot.Campagne = new Campagne();
                mLot.Exportateur = new Exportateur();
                mLot.Certification = new Certification();
                mLot.Production = new OrdreProduction();
                mLot.LotType = new LotType();
                mPesee = MapFormToObject(mPesee);
                _db = mLot.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                mLot.Campagne.Designation = mPesee.Campagne.Designation;
                mLot.Exportateur.ID = new Parametres(0).Exportateur.ID;
                mLot.Certification = null;
                mLot.Production.ID = mPesee.OrdreProduction.ID;
                mLot.LotType.ID = mPesee.LotType.ID;
                mLot.DateLot = DateTime.Now;
                mLot.EstManuel = false;
                mLot.EstQueue = mPesee.NombreSacs >= mLotType.NombreSacs ? false : true;
                //mLot.EstQueue = false;
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
                    //mPesee = MapFormToObject(mPesee);
                    
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
                                palette.PeseeProduction = new PeseeApresUsinage();

                                palette.SetDataSource(_db);

                                palette.PeseeProduction.ID = mPesee.ID;
                                palette.NombreSacs = item[i].NombreSacs;
                                palette.PoidsBrut = item[i].PoidsBrut;
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
                        else
                            resultPalette = false;                        
                    }                                        
                }

                if (resultLot && result && resultPalette)
                {
                    _db.CommitTransaction(mtran);
                    Store mstore = X.GetCmp<Store>("storeListePeseeApresUsinage");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mPesee);
                        X.GetCmp<RowSelectionModel>("rowPeseeApresUsinage").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mPesee.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mPesee);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormPeseeApresUsinage").Close();

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
                    Title = "Pesée Apres Usinage : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        private PeseeApresUsinage MapFormToObject(PeseeApresUsinage mClass)
        {
            Campagne campagne = new Campagne();
            campagne.Designation = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text.ToString();
            mClass.Campagne = campagne;

            OrdreProduction production = new OrdreProduction();
            production.ID = Guid.Parse(GetFormValue("cmbProduction"));
            production.NumeroProduction = X.GetCmp<ComboBox>("cmbProduction").SelectedItem.Text.ToString();
            mClass.OrdreProduction = production;

            LotType lType = new LotType();
            lType.ID = int.Parse(GetFormValue("cmbTypeLot"));
            lType.Designation = X.GetCmp<ComboBox>("cmbTypeLot").SelectedItem.Text.ToString();            
            mClass.LotType = lType;

            mClass.Lot = null;

            //mClass.DatePeseeProduction = DateTime.Parse(X.GetCmp<DateField>("txtDatePesee").RawText.ToString());
            mClass.DatePeseeProduction = DateTime.Parse(X.GetCmp<DateField>("txtDatePesee").RawText.ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second).AddMilliseconds(DateTime.Now.Millisecond);
            mClass.Statut = "NA";

            mClass.PeseeProductionType = new PeseeProductionType();
            mClass.PeseeProductionType.ID = (new Parametres(0)).PeseeApresUsinage;
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
                    Title = "Pesée Apres Usinage : Data Validation",
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
                    X.GetCmp<NumberField>("txtTare").Text = sWeight.ToString();
                }
                else
                {
                    int nbrSac = !string.IsNullOrEmpty(sacs) ? int.Parse(sacs) : 0;
                    sWeight = nbrSac * 67;
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

        public ActionResult SubmitWeightPU(string ItemSelected = "",string NombreTotalSac = "", string CurrentNombreSacs = "", string TypeLotID = "")
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

            if (X.GetCmp<TextField>("txtNombreSacsPU").Text != string.Empty) mPesee.NombreSacs = int.Parse(X.GetCmp<TextField>("txtNombreSacsPU").Text);
            if (X.GetCmp<TextField>("txtTare").Text != string.Empty) mPesee.PoidsBrut = decimal.Parse(X.GetCmp<TextField>("txtTare").Text);
            mPesee.IsNew = true;
            mPesee.DatePesee = DateTime.Now;

            if ((OlDNombreSacs + mPesee.NombreSacs) > NombreSacsAutorises)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Pesée Apres Usinage : Data Validation",
                    Message = "Nbre de sacs supérieure au nombre de sacs autorisé",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }

            if (mPesee.NombreSacs > 0 && mPesee.PoidsBrut > 0)
            {

                if (mPesee.NombreSacs > mlotType.NombreSacsParPalettes)
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Pesée Apres Usinage : Data Validation",
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
                        Title = "Pesée Apres Usinage : Data Validation",
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
                        Title = "Pesée Apres Usinage : Data Validation",
                        Message = "Nbre de sacs supérieure au nombre de sacs autorisé",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }
                Store mstore = X.GetCmp<Store>("storeListeWeightPU");
                mstore.Insert(mIndex, mPesee);
                X.GetCmp<RowSelectionModel>("rowWeightPU").Select(mIndex);

                X.GetCmp<Window>("FormAddPalletsWeight").Close();
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
                PeseeApresUsinage mPesee = new PeseeApresUsinage();
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
                mLotType.fnGet(mPesee.LotType.ID);
                Lot mLot = new Lot();

                //if (mPesee.NombreSacs == mLotType.NombreSacs)
                //{
                //    #region Creation Du Lot                    

                //    mLot.Campagne = new Campagne();
                //    mLot.Exportateur = new Exportateur();
                //    mLot.Certification = new Certification();
                //    mLot.Production = new OrdreProduction();
                //    mLot.LotType = new LotType();
                //    mPesee = MapFormToObject(mPesee);
                //    _db = mLot.db();
                //    mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                //    mLot.Campagne.Designation = mPesee.Campagne.Designation;
                //    mLot.Exportateur.ID = new Parametres(0).Exportateur.ID;
                //    mLot.Certification = null;
                //    mLot.Production.ID = mPesee.OrdreProduction.ID;
                //    mLot.LotType.ID = mPesee.LotType.ID;
                //    mLot.DateLot = DateTime.Now;
                //    mLot.EstManuel = false;
                //    //mLot.EstQueue = mPesee.NombreSacs >= mLotType.NombreSacs ? false : true;
                //    mLot.EstQueue = false;
                //    mLot.NombreSacs = mPesee.NombreSacs;
                //    mLot.PoidsBrut = mPesee.PoidsBrut;
                //    mLot.TareSacs = mPesee.TareSacs;
                //    mLot.TarePalette = mPesee.TarePalette;
                //    mLot.PoidsNet = mPesee.PoidsNet;
                //    mLot.UtilisateurCreation = (string)Session["userName"];
                //    mLot.IsNew = true;

                //    resultLot = mLot.fnUpdate(mtran);

                //    if (!resultLot)
                //    {
                //        _db.RollBackTransaction(mtran);
                //        return this.Direct();
                //    }
                //    #endregion
                //}

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
                            mPalette.PeseeProduction = new PeseeApresUsinage();

                            mPalette.SetDataSource(_db);
                            mPalette.ID = item.ID;
                            mPalette.PeseeProduction.ID = mPesee.ID;
                            mPalette.NombreSacs = item.NombreSacs;
                            mPalette.PoidsBrut = item.PoidsBrut;
                            mPalette.DatePesee = item.DatePesee;
                            mPalette.IsNew = item.IsNew;

                            mPalette.UtilisateurCreation = (string)Session["userName"];
                            mPalette.UtilisateurModification = (string)Session["userName"];
                            Store mstorePalette = X.GetCmp<Store>("storeListeWeightPU");
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

                        Store mstore = X.GetCmp<Store>("storeListePeseeApresUsinage");
                        ModelProxy mProxy = mstore.GetById(mPesee.ID);
                        mProxy.BeginEdit();
                        mProxy.Set(mPesee);
                        mProxy.Commit();
                        mProxy.EndEdit();

                        if(resultLot)
                        {
                            X.GetCmp<Window>("FormPeseeApresUsinage").Close();
                            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                            X.Js.Call("GenerateTicket", mPesee.ID, BaseUrl);
                        }                            
                    }
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Post Cleanig Weighing : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        public ActionResult Finalize(string ItemSelected = "", string peseeID = "", string nbrSacs = "", string poidsPalette = "")
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                bool resultLot = false;
                bool resultMouvement = false;
                Parametres mParam = new Parametres(0);
                PeseeApresUsinage mPesee = new PeseeApresUsinage();
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
                mLotType.fnGet(mPesee.LotType.ID);
                Lot mLot = new Lot();
                
                #region Creation Du Lot                    

                mLot.Campagne = new Campagne();
                mLot.Exportateur = new Exportateur();
                mLot.Certification = new Certification();
                mLot.Production = new OrdreProduction();
                mLot.LotType = new LotType();
                mPesee = MapFormToObject(mPesee);
                if (mPesee.NombreSacs < mLotType.NombreSacs)
                {
                    int TotalBags = mLotType.NombreSacs - mPesee.NombreSacs;
                    decimal TareToRemove = TotalBags * mLotType.TareSacsUnitaire;
                    mPesee.TarePalette = mPesee.TarePalette - TareToRemove;
                    mPesee.PoidsNet = mPesee.PoidsBrut - mPesee.TarePalette;
                }
                else
                {
                    int TotalBags = mPesee.NombreSacs - mLotType.NombreSacs;
                    decimal TareToAdd = TotalBags * mLotType.TareSacsUnitaire;
                    mPesee.TarePalette = mPesee.TarePalette + TareToAdd;
                    mPesee.PoidsNet = mPesee.PoidsBrut - mPesee.TarePalette;
                }

                _db = mLot.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

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

                if (!resultLot)
                {
                    _db.RollBackTransaction(mtran);
                    return this.Direct();
                }
                #endregion

                if (mLot.ID != Guid.Empty)
                {
                    mPesee.Lot = new Lot();
                    mPesee.Lot.ID = mLot.ID;
                    mPesee.Lot.NumeroLot = mLot.NumeroLot;
                    mPesee.Statut = "AP";
                    mPesee.SetDataSource(_db);
                }
                else
                {
                    _db = mPesee.db();
                    mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                }
                result = mPesee.fnUpdate(mtran);

                if (result)
                {
                    List<PeseeProductionPalette> ListePalettesPesees = JSON.Deserialize<List<PeseeProductionPalette>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (ListePalettesPesees.Count > 0)
                    {
                        foreach (PeseeProductionPalette item in ListePalettesPesees.Where(p => p.IsNew == true))
                        {
                            mPalette = new PeseeProductionPalette();
                            mPalette.PeseeProduction = new PeseeApresUsinage();

                            mPalette.SetDataSource(_db);
                            mPalette.ID = item.ID;
                            mPalette.PeseeProduction.ID = mPesee.ID;
                            mPalette.NombreSacs = item.NombreSacs;
                            mPalette.PoidsBrut = item.PoidsBrut;
                            mPalette.DatePesee = item.DatePesee;
                            mPalette.IsNew = item.IsNew;

                            mPalette.UtilisateurCreation = (string)Session["userName"];
                            mPalette.UtilisateurModification = (string)Session["userName"];
                            Store mstorePalette = X.GetCmp<Store>("storeListeWeightPU");
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

                    //Generer Mvt
                    #region Mvt Stock
                    MouvementStock mouvement = new MouvementStock();
                    
                    mouvement = new MouvementStock();
                    mouvement.mCampagne = new Campagne();
                    mouvement.Exportateur = new Exportateur();
                    mouvement.SacType = new SacType();
                    mouvement.TypeElementStock = new TypeElementStock();
                    mouvement.Certification = new Certification();
                    mouvement.Magasin = new Magasin();
                    mouvement.Emplacement = new Emplacement();
                    mouvement.MouvementStockType = new MouvementStockType();
                    mouvement.Sites = new Site();
                    InventaireElementStock Items = new InventaireElementStock();

                    mouvement.SetDataSource(_db);
                    mouvement.mCampagne.Designation = mLot.Campagne.Designation;
                    mouvement.Exportateur.ID = mLot.Exportateur.ID;
                    mouvement.DateMouvement = DateTime.Now;
                    mouvement.SacType.ID = mParam.SacExportType;
                    mouvement.ObjetEnStock = mLot.ID;
                    mouvement.ObjetEnStockType = mParam.LotTypeElementEnStock;
                    mouvement.MouvementStockType.ID = mParam.LotUsineTypeMvtID;
                    mouvement.Sites.ID = mParam.Site;
                    mouvement.Sites.Nom = mParam.Site.ToString();
                    mouvement.Certification = null;

                    //if (mLot.Certification != null)
                    //{
                    //    mouvement.Certification = new Certification();
                    //    mouvement.Certification.ID = mLot.Certification.ID;
                    //}
                    mouvement.Sens = 1;
                    mouvement.Quantite = mPesee.NombreSacs;
                    mouvement.PoidsBrut = mPesee.PoidsBrut;
                    mouvement.TarePalettes = mPesee.TarePalette;
                    mouvement.TareSacs = mPesee.TareSacs;
                    mouvement.PoidsNetLivre = mLot.PoidsNet;
                    mouvement.PoidsNetAccepte = mLot.PoidsNet;
                    mouvement.Retention = 0;
                    mouvement.Magasin.ID = mParam.MagasinExport;
                    //mouvement.Magasin.ID = mClass.MagasinDefID;
                    mouvement.Emplacement.ID = mParam.EmplacementParDefaut;
                    mouvement.Reference1 = mLot.NumeroLot;
                    mouvement.Reference2 = mPesee.OrdreProduction.NumeroProduction;
                    mouvement.Commentaire = "generé automatiquement";
                    mouvement.Statut = "AP";
                    mouvement.UtilisateurCreation = (string)Session["userName"];
                    mouvement.UtilisateurModification = (string)Session["userName"];

                    result = mouvement.fnUpdate(mtran);

                    if (!result)
                        _db.RollBackTransaction(mtran);
                    #endregion

                    if (result)
                    {
                        _db.CommitTransaction(mtran);

                        Store mstore = X.GetCmp<Store>("storeListePeseeApresUsinage");
                        ModelProxy mProxy = mstore.GetById(mPesee.ID);
                        mProxy.BeginEdit();
                        mProxy.Set(mPesee);
                        mProxy.Commit();
                        mProxy.EndEdit();

                        if (resultLot)
                        {
                            X.GetCmp<Window>("FormPeseeApresUsinage").Close();
                            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                            X.Js.Call("GenerateTicket", mPesee.ID, BaseUrl);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (_db != null)
                    _db.RollBackTransaction(mtran);

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Post Cleanig Weighing : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
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
                    Store mstore = X.GetCmp<Store>("storeListeWeightPU");

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
                    Title = "Pesée Apres Usinage - : Retirer Pallets Weight",
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
                PeseeApresUsinage mPesee = new PeseeApresUsinage();
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mPesee.IsNew = true;
                else
                {
                    formExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                    mPesee.IsNew = false;
                    bool isGuid = Guid.TryParse(GetFormValue("TxtPeseeApresUsID"), out peseeID);
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
                    Store mstore = X.GetCmp<Store>("storeListePeseeApresUsinage");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mPesee);
                        X.GetCmp<RowSelectionModel>("rowPeseeApresUsinage").Select(0);
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
                    X.GetCmp<Hidden>("TxtPeseeApresUsID").SetValue(mPesee.ID);
                    X.GetCmp<Hidden>("hiddenExecMode").SetValue("Update");
                    //X.GetCmp<Window>("FormPeseeAvantUsinage").Close();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Post Cleaning Weighing : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
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
                            X.GetCmp<Window>("PeseeApresUsinage_Production").Close();
                        }
                        else
                        {
                            X.GetCmp<Hidden>("txtOrdreProduction").SetValue("");
                            X.GetCmp<Hidden>("txtOrdreProductionID").SetValue(Guid.Empty);
                            X.MessageBox.Show(new MessageBoxConfig
                            {
                                Title = "Production : Ordre De Production",
                                Message = "Ordre De Production Not Found, Please Retry !",
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
                        X.GetCmp<Window>("PeseeApresUsinage_Production").Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Ordre De Production - Data Validation",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnSelectProduction()
        {
            return new Ext.Net.MVC.PartialViewResult { ViewName = "PeseeApresUsinage_Production" };
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
                    Title = "Ordre De Production : Data Validation",
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

                PeseeApresUsinage mPesee = new PeseeApresUsinage();
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

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Ticket{0}', '{1}/PeseeApresUsinage/ViewWeighingTicket?id={0}&IsCopy={2}', this, 'Ticket De pesée','')", IdPesee, BaseUrl, ReportIsCopy));
        }

        public ActionResult ViewWeighingTicket(string id, bool IsCopy)
        {
            try
            {
                XtraReport report = new TicketPeseeApresUsinage();

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
            ViewData["Titre"] = "Post Cleaning Weighing";
            ViewData["actionToDo"] = "PrintList";
            ViewData["ControllerName"] = "PeseeApresUsinage";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "ProductionWeighing_Print", ViewData = ViewData };
        }

        public ActionResult PrintList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;

                report = new rptPeseeApresUsinageList() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["CampagneID"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Value.ToString();
                report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Text;

                report.Parameters["paramTypePesee"].Value = (new Parametres(0)).PeseeApresUsinage;

                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateDebut").RawText);
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateFin").RawText);

                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/PeseeApresUsinage/ViewList', this, 'Post Cleaning Weighing',''),App.ProductionWeighing_Print.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Post Cleaning Weighing : Data Validation",
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

    }
}