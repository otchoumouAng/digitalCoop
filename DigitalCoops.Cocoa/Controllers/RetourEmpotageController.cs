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
    [System.Runtime.InteropServices.Guid("20F29891-3FCF-45E6-B1CF-D05841226C27")]
    public class RetourEmpotageController : BaseController
    {
        // GET: RetourEmpotage

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

            X.GetCmp<FormPanel>("RetourEmpotageCP").SetTitle("Campagne : " + campagne + ", Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{504A5055-A288-4D9D-8211-C24D08EE9306}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{81C79819-492C-46E8-8CD4-A58F3BE5C41C}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{DF108A23-EF16-4304-976C-68E8347B6027}")))
                X.GetCmp<MenuItem>("mnuPrintList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{60D54018-5701-4F43-A914-6F7F41271FCD}")))
                X.GetCmp<MenuItem>("mnuExport").Enable();
            else
                X.GetCmp<MenuItem>("mnuExport").Disable();            

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{A1C16156-0756-4527-B661-9BFCDCB826B4}")))
                X.GetCmp<Hidden>("ashiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("ashiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{3D946B7B-BC5B-4642-BAA9-202D5802B554}")))
                X.GetCmp<Hidden>("ashiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("ashiddenPermDesactiver").SetValue(false);
            
            #endregion

            return View();
        }

        public ActionResult onAdd()
        {            
            RetourEmpotageViewModel mclass = new RetourEmpotageViewModel();
            string UserName = (string)Session["userName"];            

            mclass._RetourEmpotage = new RetourEmpotage();
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRetourEmpotage", Model = mclass };
        }

        public ActionResult onEdit(string ItemSelected)
        {            
            RetourEmpotageViewModel mclass = new RetourEmpotageViewModel();
            string UserName = (string)Session["userName"];            

            mclass._RetourEmpotage = new RetourEmpotage();
            mclass._RetourEmpotage = JSON.Deserialize<RetourEmpotage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            //mclass._RetourEmpotage.IsManual = false;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRetourEmpotage", Model = mclass};
        }

        public ActionResult onConsult(string ItemSelected)
        {            
            RetourEmpotageViewModel mclass = new RetourEmpotageViewModel();
            mclass._RetourEmpotage = new RetourEmpotage();
            
            mclass._RetourEmpotage = JSON.Deserialize<RetourEmpotage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
            //mclass._RetourEmpotage.IsManual = false;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRetourEmpotage", Model = mclass };
        }

        public ActionResult OnSelectStuffing()
        {
            Parametres mParam = new Parametres(0);
            ViewData["Campagne"] = mParam.Campagne;
            ViewData["Exportateur"] = mParam.Exportateur.ID;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "RetourEmpotage_SelectEmpotage", ViewData = ViewData };
        }

        public ActionResult SubmitEmpotage(string ItemSelected)
        {
            Empotage mclass = new Empotage();
            mclass = JSON.Deserialize<Empotage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            X.GetCmp<Hidden>("TxtEmpotageID").SetValue(mclass.ID);
            X.GetCmp<TextField>("TxtNumeroShipment").SetValue(mclass.EmbarquementAsString);
            X.GetCmp<Window>("RetourEmpotage_SelectEmpotage").Close();

            return this.Direct();
        }
        //public ActionResult onAddWeight(string IsManual = "")
        //{
        //    RetourEmpotage pesee = new RetourEmpotage();
        //    if (!string.IsNullOrEmpty(IsManual))
        //        pesee.IsManual = bool.Parse(IsManual);
        //    else
        //        pesee.IsManual = false;

        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAddWeight", Model = pesee };
        //}

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
            var mListe = (new RetourEmpotage()).fnSelect(ItemCampagne, StartDate, EndDate,statut);

            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeRetourEmpotage");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCampagne",ItemCampagne),
                                    new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus",ItemStatus)
                                });

                X.GetCmp<FormPanel>("RetourEmpotageCP").Collapse(Direction.Top, false);
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

        public ActionResult SubmitFormMethod(string ItemSelected = "")
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                RetourEmpotage mEmpotage = new RetourEmpotage();

                DateTime dateRetour = new DateTime();
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mEmpotage.IsNew = true;
                else
                {
                    mEmpotage.IsNew = false;

                    mEmpotage.fnGet(Guid.Parse(GetFormValue("TxtRetourEmpotageID")));

                    if (mEmpotage == null || mEmpotage.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Stuffing load failed.");

                    dateRetour = mEmpotage.DateRetour;
                }                                                           
                
                bool result = false;
                bool resultMouvemement = true;

                
                mEmpotage = MapFormToObject(mEmpotage);

                if (mEmpotage.IsNew == false && (dateRetour.Date == mEmpotage.DateRetour.Date))
                    mEmpotage.DateRetour = dateRetour;

                _db = mEmpotage.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = mEmpotage.fnUpdate(mtran);                

                if (result)
                {
                    MouvementStock mouvement = new MouvementStock();                    
                    mouvement.mCampagne = new Campagne();
                    mouvement.Exportateur = new Exportateur();
                    mouvement.SacType = new SacType();
                    mouvement.TypeElementStock = new TypeElementStock();
                    mouvement.Certification = new Certification();
                    mouvement.Magasin = new Magasin();
                    mouvement.Emplacement = new Emplacement();
                    mouvement.MouvementStockType = new MouvementStockType();
                    InventaireElementStock Items = new InventaireElementStock();
                    Parametres mParam = new Parametres(0);
                    mouvement.SetDataSource(_db);
                    mouvement.mCampagne.Designation = mEmpotage.Campagne.Designation;
                    mouvement.Exportateur.ID = mParam.Exportateur.ID;
                    mouvement.DateMouvement = DateTime.Now;
                    mouvement.SacType.ID = mParam.SacExportType;
                    mouvement.ObjetEnStock = mEmpotage.Lot.ID;
                    mouvement.ObjetEnStockType = mParam.AjustementStockType;
                    //mouvement.TypeElementStock.ID = mParam.AjustementStockType;
                    mouvement.MouvementStockType.ID = mParam.IDRetourEnStockLotMvtType;
                    mouvement.Certification = null;
                    mouvement.Sens = (Int16)mEmpotage.Sens;
                    mouvement.Quantite = mEmpotage.Quantite;
                    mouvement.PoidsBrut = mEmpotage.PoidsBrut;
                    mouvement.TarePalettes = mEmpotage.TarePalette;
                    mouvement.TareSacs = mEmpotage.TareSacs;
                    mouvement.PoidsNetLivre = mEmpotage.PoidsBrut - mEmpotage.TareSacs - mEmpotage.TarePalette;
                    mouvement.PoidsNetAccepte = mEmpotage.PoidsBrut - mEmpotage.TareSacs - mEmpotage.TarePalette;
                    mouvement.Retention = 0;
                    mouvement.Magasin.ID = mParam.MagasinExport;
                    mouvement.Emplacement.ID = mParam.EmplacementParDefaut;
                    //mouvement.DateMouvement = inventaire.DateInventaire;
                    mouvement.Reference1 = mEmpotage.ReferenceLot;
                    //mEmpotage.Empotage = new Empotage();                    
                    mouvement.Reference2 = mEmpotage.Empotage.Embarquement.Numero;
                    mouvement.Commentaire = "Retour Empotage";
                    mouvement.Statut = "NA";
                    mouvement.UtilisateurCreation = (string)Session["userName"];
                    mouvement.UtilisateurModification = (string)Session["userName"];

                    resultMouvemement = mouvement.fnUpdate(mtran);
                    if (!resultMouvemement)
                        _db.RollBackTransaction(mtran);
                    else
                    {
                        _db.CommitTransaction(mtran);

                        Store mstore = X.GetCmp<Store>("storeListeRetourEmpotage");
                        if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                        {
                            mstore.Insert(0, mEmpotage);
                            X.GetCmp<RowSelectionModel>("rowRetourEmpotage").Select(0);
                        }
                        else
                        {
                            ModelProxy mProxy = mstore.GetById(mEmpotage.ID);

                            mProxy.BeginEdit();

                            mProxy.Set(mEmpotage);

                            mProxy.Commit();

                            mProxy.EndEdit();
                        }

                        X.GetCmp<Window>("FormRetourEmpotage").Close();
                    }
                }
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

        public ActionResult onCancel(string ItemSelected)
        {
            try
            {
                RetourEmpotage mClass = JSON.Deserialize<RetourEmpotage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = mClass.fnGet(mClass.ID);

                if (!result)
                    throw new Exception("onCancel : Stuffing, loading failed.");

                mClass.UtilisateurModification = (string)Session["userName"];


                result = mClass.fnDeActivate();

                if (!result)
                    throw new Exception("onCancel : Stuffing, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeRetourEmpotage");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stuffing : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
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

            var mListe = (new Empotage()).fnSelectForCorrectStuffing(Campagne, ExportateurID, StartDate, EndDate, Status);
            return this.Store(mListe);
        }

        private RetourEmpotage MapFormToObject(RetourEmpotage mClass)
        {            
            Campagne campagne = new Campagne();
            campagne.Designation = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text.ToString();
            mClass.Campagne = campagne;

            mClass.Empotage = new Empotage();            
            Empotage mEmpotage = new Empotage();
            mEmpotage.ID = Guid.Parse(GetFormValue("TxtEmpotageID"));
            mEmpotage.Embarquement = new Embarquement();
            mEmpotage.Embarquement.Numero = X.GetCmp<TextField>("TxtNumeroShipment").Text;            
            mClass.Empotage = mEmpotage;            

            mClass.Lot = new Lot();
            mClass.Lot.ID = Guid.Parse(GetFormValue("cmbLot"));
            mClass.Lot.NumeroLot = X.GetCmp<ComboBox>("cmbLot").SelectedItem.Text.ToString();

            mClass.Sens = int.Parse(X.GetCmp<ComboBox>("cmbSens").SelectedItem.Value.ToString());
            mClass.DateRetour = DateTime.Parse(X.GetCmp<DateField>("txtdateRetour").RawText.ToString()).Add(DateTime.Now.TimeOfDay);
            mClass.Statut = "NA";                        
            mClass.ReferenceLot = X.GetCmp<ComboBox>("cmbLot").SelectedItem.Text.ToString();

            if (X.GetCmp<TextField>("txtQuantite").Text != string.Empty) mClass.Quantite = int.Parse(X.GetCmp<TextField>("txtQuantite").Text);
            if (X.GetCmp<TextField>("txtPoidsBrut").Text != string.Empty) mClass.PoidsBrut = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrut").Text);
            if (X.GetCmp<TextField>("txtTareSacs").Text != string.Empty) mClass.TareSacs = decimal.Parse(X.GetCmp<TextField>("txtTareSacs").Text);
            if (X.GetCmp<TextField>("txtTarePalette").Text != string.Empty) mClass.TarePalette = decimal.Parse(X.GetCmp<TextField>("txtTarePalette").Text);           
            if (X.GetCmp<TextField>("txtNetWeight").Text != string.Empty) mClass.PoidsNet = decimal.Parse(X.GetCmp<TextField>("txtNetWeight").Text);
            
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
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
                //int sWeight = 2000;
                X.GetCmp<TextField>("txtTarePalettes").Text = sWeight.ToString("n0");
                CapturedTare = sWeight;

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
                BtnReadClicked = true;
                CapturedWeight = 0;
                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                //string ip = "198.168.8.101";
                // initialize services                      
                ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);

                int sWeight = proxy.GetWeight();
                //int sWeight = 15000;
                X.GetCmp<NumberField>("txtTare").Text = sWeight.ToString();
                CapturedWeight = sWeight;

            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
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

                RetourEmpotage mPesee = new RetourEmpotage();
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

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Ticket{0}', '{1}/RetourEmpotage/ViewWeighingTicket?id={0}&IsCopy={2}', this, 'Ticket De pesée','')", IdPesee, BaseUrl, ReportIsCopy));
        }

        public ActionResult OnPrintWeighingList()
        {
            Parametres mParam = new Parametres(0);

            ViewData["Campagne"] = mParam.Campagne;
            ViewData["Titre"] = "General Weighing";
            ViewData["actionToDo"] = "PrintList";
            ViewData["ControllerName"] = "RetourEmpotage";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "ProductionWeighing_Print", ViewData = ViewData };
        }

        public ActionResult PrintList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;

                //report = new rptRetourEmpotageList() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["CampagneID"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Value.ToString();
                report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Text;

                report.Parameters["paramTypePesee"].Value = (new Parametres(0)).PeseeCertifiee;

                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateDebut").RawText);
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateFin").RawText);

                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/RetourEmpotage/ViewList', this, 'General Weighing',''),App.ProductionWeighing_Print.doClose()", Guid.NewGuid(), BaseUrl));
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
            FormPanel mform = X.GetCmp<FormPanel>("RetourEmpotageCP");
            mform.ToggleCollapse();
            //mform.Collapsed = false;
            return this.Direct();
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