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
using Tms.Classes.Business.Sales;
using Tms.Classes.Business.stock;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.Sales;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class EmpotageController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        // GET: Empotage
        public ActionResult Index()
        {
            var mParam = (new Parametres()).fnSelect();

            Parametres mclass = new Parametres();
            mclass = mParam[0] as Parametres;

            Campagne mCampagne = new Campagne();

            X.GetCmp<ComboBox>("cmbCropYear").SetValue(mclass.Campagne);

            string StartDate = "01/01/" + DateTime.Now.Year.ToString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("dtpStartDate").RawText = StartDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Exportateur = "{Tous}";

            X.GetCmp<FormPanel>("EmpotageCriteriaPanel").SetTitle("Campagne : " + mclass.Campagne + " | Exportateur : " + Exportateur + " | Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            if (HasAccess.fnGetUserAccessStatus("{7D04003F-8C8B-402F-8B75-0C280AE08421}", UserName) == false)
                X.GetCmp<MenuItem>("btnNew").Disable();
            else
                X.GetCmp<MenuItem>("btnNew").Enable();

            if (HasAccess.fnGetUserAccessStatus("{15E34622-A1EB-41D3-BE1A-3CA9DEAA76F4}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExport").Disable();
            else
                X.GetCmp<MenuItem>("mnuExport").Enable();

            //if (HasAccess.fnGetUserAccessStatus("{D3F61EE5-2670-42EC-9911-E16B34C4C6A0}", UserName) == false)
            //    X.GetCmp<MenuItem>("mnuPrintStuffingSheet").Disable();
            //else
            //    X.GetCmp<MenuItem>("mnuPrintStuffingSheet").Enable();

            if (HasAccess.fnGetUserAccessStatus("{CA8D3636-B2E3-4A9D-9AA3-1F7F244B706E}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintVGM").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintVGM").Enable();

            if (HasAccess.fnGetUserAccessStatus("{414CFAAD-563A-4C57-9FFA-1302071269EF}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintHistory").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintHistory").Enable();

            X.GetCmp<Hidden>("emphiddenPermDesactive").SetValue(HasAccess.fnGetUserAccessStatus("{CCDA1598-C709-45DB-B06A-C09DC308F0D6}", UserName));
            X.GetCmp<Hidden>("emphiddenPermActive").SetValue(HasAccess.fnGetUserAccessStatus("{43A98339-7518-4C98-9B32-F7CD9EF9EBB9}", UserName));
            X.GetCmp<Hidden>("emphiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{15E34622-A1EB-41D3-BE1A-3CA9DEAA76F4}", UserName));
            X.GetCmp<Hidden>("emphiddenPermPrinStuffingSheet").SetValue(HasAccess.fnGetUserAccessStatus("{D3F61EE5-2670-42EC-9911-E16B34C4C6A0}", UserName));
            X.GetCmp<Hidden>("emphiddenPermPrintVGM").SetValue(HasAccess.fnGetUserAccessStatus("{CA8D3636-B2E3-4A9D-9AA3-1F7F244B706E}", UserName));
            X.GetCmp<Hidden>("emphiddenPermPrintHistory").SetValue(HasAccess.fnGetUserAccessStatus("{414CFAAD-563A-4C57-9FFA-1302071269EF}", UserName));
            X.GetCmp<Hidden>("emphiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{79B9933A-AED4-4E97-AD90-7EBD7F001C7B}", UserName));
            X.GetCmp<Hidden>("emphiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{7D04003F-8C8B-402F-8B75-0C280AE08421}", UserName));
            X.GetCmp<Hidden>("emphiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{51222DE4-ACDE-40EE-ABE4-4C04953056A0}", UserName));
            X.GetCmp<Hidden>("emphiddenPermAjuster").SetValue(HasAccess.fnGetUserAccessStatus("{5FB83410-D889-4ADE-8BAF-B7110BA92B1A}", UserName));
            #endregion
            return View();
        }

        public ActionResult LoadListOfStuffing(StoreRequestParameters parameters, string ItemCropYear, string ItemExporter, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {

            int ExportateurID = GetCriteriaValue(ItemExporter);
            string Campagne = string.IsNullOrEmpty(ItemCropYear) ? "" : ItemCropYear;

            if (!string.IsNullOrEmpty(ItemCropYear) && ItemCropYear.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            int Status = int.Parse(ItemStatus);

            var mListe = (new Empotage()).fnSelect(Campagne, ExportateurID, StartDate, EndDate, Status);

            //return this.Store(paging);
            return this.Store(mListe);
        }

        public ActionResult LoadListOfContainer(string ItemExecMode, string ItemEmpotageID)
        {

            List<DataPersist> myList = new List<DataPersist>();
            if (!string.IsNullOrEmpty(ItemEmpotageID))
            {

                if (ItemExecMode == "AddNew")
                {


                }
                else
                {
                    myList = new Conteneur().fnSelect(Guid.Parse(ItemEmpotageID), -1,-1);
                }

                Conteneur mClass = new Conteneur();
                if (myList.Count > 0)
                    mClass = myList[0] as Conteneur;
                else myList = new List<DataPersist>();

            }
            else
            {
                Store mstore = X.GetCmp<Store>("storeListeConteneur");
                mstore.RemoveAll();
                myList = null;
            }

            return this.Store(myList);
        }
        public ActionResult onCreate()
        {
            EmpotageViewModel viewmodel = new EmpotageViewModel();

            viewmodel._Empotage = new Empotage();

            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            #region "Access"
            string UserName = (string)Session["userName"];

            Fonction HasAccess = new Fonction();

            bool ConteneurPermissionRemove = HasAccess.fnGetUserAccessStatus("{7CC44793-F113-4B29-97C0-57576D7955A3}", UserName);
            bool ConteneurPermissionAdd = HasAccess.fnGetUserAccessStatus("{18F294AF-603F-468E-9773-8811D06D7F25}", UserName);
            bool ConteneurPermissionEdit = HasAccess.fnGetUserAccessStatus("{13EB053B-EDC7-4DC7-8E53-DF606BD94FEA}", UserName);


            if (ConteneurPermissionRemove == true)
            {
                ViewData["ConteneurPermissionRemove"] = true;
            }
            else
            {
                ViewData["ConteneurPermissionRemove"] = false;
            }

            if (ConteneurPermissionAdd == true)
            {
                ViewData["ConteneurPermissionAdd"] = true;
            }
            else
            {
                ViewData["ConteneurPermissionAdd"] = false;
            }

            if (ConteneurPermissionEdit == true)
            {
                ViewData["ConteneurPermissionEdit"] = true;
            }
            else
            {
                ViewData["ConteneurPermissionEdit"] = false;
            }
            #endregion
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Empotage_Detail" , Model = viewmodel, ViewData = ViewData};
        }
        public ActionResult onEdit(string ItemSelected)
        {
            Empotage mclass = JSON.Deserialize<Empotage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            EmpotageViewModel viewModel = new EmpotageViewModel();

            viewModel._Empotage = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            #region "Access"
            string UserName = (string)Session["userName"];

            Fonction HasAccess = new Fonction();

            bool ConteneurPermissionRemove = HasAccess.fnGetUserAccessStatus("{7CC44793-F113-4B29-97C0-57576D7955A3}", UserName);
            bool ConteneurPermissionAdd = HasAccess.fnGetUserAccessStatus("{18F294AF-603F-468E-9773-8811D06D7F25}", UserName);
            bool ConteneurPermissionEdit = HasAccess.fnGetUserAccessStatus("{13EB053B-EDC7-4DC7-8E53-DF606BD94FEA}", UserName);


            if (ConteneurPermissionRemove == true)
            {
                ViewData["ConteneurPermissionRemove"] = true;
            }
            else
            {
                ViewData["ConteneurPermissionRemove"] = false;
            }

            if (ConteneurPermissionAdd == true)
            {
                ViewData["ConteneurPermissionAdd"] = true;
            }
            else
            {
                ViewData["ConteneurPermissionAdd"] = false;
            }

            if (ConteneurPermissionEdit == true)
            {
                ViewData["ConteneurPermissionEdit"] = true;
            }
            else
            {
                ViewData["ConteneurPermissionEdit"] = false;
            }
            #endregion

            return new Ext.Net.MVC.PartialViewResult { ViewName = "EmpotageConteneur_Detail" ,Model = viewModel, ViewData = ViewData };
        }

        public ActionResult onConsult(string ItemSelected)
        {

            Empotage mclass = JSON.Deserialize<Empotage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            EmpotageViewModel viewmodel = new EmpotageViewModel();
            viewmodel._Empotage = mclass;
            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            ViewData["ConteneurPermissionRemove"] = false;
            ViewData["ConteneurPermissionAdd"] = false;
            ViewData["ConteneurPermissionEdit"] = false;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "EmpotageConteneur_Detail", Model = viewmodel, ViewData = ViewData };
        }

        public ActionResult onAdjust(string ItemSelected)
        {
            Empotage mclass = JSON.Deserialize<Empotage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            Embarquement mEmbarquement = new Embarquement();
            bool result = mEmbarquement.fnGet(mclass.Embarquement.ID);
            if (!result)
                throw new Exception("OnActivateDeactivate : Stuffing loading failed.");

            RetourEmpotageViewModel viewmodel = new RetourEmpotageViewModel();
            viewmodel._RetourEmpotage = new RetourEmpotage();
            viewmodel._RetourEmpotage.Empotage = new Empotage();
            viewmodel._RetourEmpotage.Empotage = mclass;
            viewmodel._RetourEmpotage.Empotage.Embarquement = new Embarquement();
            viewmodel._RetourEmpotage.Empotage.Embarquement.Numero = mEmbarquement.Numero;

            Parametres mParam = new Parametres(0);
            viewmodel._DefaultCampagne = mParam.Campagne;            

            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "EmpotageConteneur_Ajustement", Model = viewmodel, ViewData = ViewData };
        }
        

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Empotage mclass = JSON.Deserialize<Empotage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = mclass.fnGet(mclass.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Stuffing loading failed.");

                mclass.UtilisateurModification = (string)Session["userName"];

                if (mclass.Desactive)
                    result = mclass.fnActivate();
                else
                    result = mclass.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Stuffing, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeEmpotage");

                    ModelProxy mProxy = mstore.GetById(mclass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mclass);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stuffing : OnActivateDeactivate",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("EmpotageCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemCropYear, string ItemExporter, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeEmpotage");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemExporter"   ,ItemExporter),
                                    new Ext.Net.Parameter("ItemCropYear"      ,ItemCropYear),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus)
                                });



                // collapse criterias areas
                X.GetCmp<FormPanel>("EmpotageCriteriaPanel").Collapse(Direction.Top, false);
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

        public ActionResult OnSelectShipment()
        {
            EmbarquementViewModel mclass = new EmbarquementViewModel();

            var StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            var EndDate = DateTime.Now.ToShortDateString();

            var Exportateur = "{Tous}";
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;


            return new Ext.Net.MVC.PartialViewResult { ViewName = "Select_Embarquement", Model = mclass };


        }

        public ActionResult OnRefreshForAvailableShipments(string ItemExportateur)
        {

            int ExportateurID = GetCriteriaValue(ItemExportateur);


            //DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            //DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            Store mstore = X.GetCmp<Store>("storeListShipment");
            mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemExportateur"   ,ItemExportateur)
                                    //,
                                    //new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    //new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd)
                                });

            return this.Direct();
        }


        public ActionResult LoadListOfAvailableShipments(string ItemExportateur)
        {

            int ExportateurID = GetCriteriaValue(ItemExportateur);


            //DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            //DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            var mListe = (new Embarquement()).fnSelectForStuffing("{Tous}", ExportateurID, -1, -1, -1, null, null, 0);

            //return this.Store(paging);
            return this.Store(mListe);
        }

       
        public ActionResult SubmitOnSelectShipment(string ItemSelected)
        {
            try
            {
                Embarquement Item = JSON.Deserialize<Embarquement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                if (Item != null)
                {
                    X.GetCmp<TextField>("txtShipmentNumber").Text = Item.Numero;
                    X.GetCmp<TextField>("txtEmbarquementID").Value = Item.ID;
                    
                    X.GetCmp<Window>("ListOfShipments").Close();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Select Shipment : SubmitArticleForm",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }



        public ActionResult SubmitFormNew2Method()
        {
            DataSource _db = new DataSource();
            DataTransaction mTran = new DataTransaction();

            try
            {
                Empotage mClass = new Empotage();

                mClass.IsNew = false;

                mClass.fnGet(Guid.Parse(GetFormValue("hiddenEmpotageID")));

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("SubmitFormNew2Method : Stuffing load failed.");

                if (mClass.Desactive)
                    throw new Exception("SubmitFormNew2Method : Stuffing is disabled ! Please Refresh Overview");
                

                bool Result = true;

                _db = mClass.db();
                mTran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                mClass = MapFormToObject2(mClass);
                Result = mClass.fnUpdate(mTran);


                if (!Result)
                {
                    _db.RollBackTransaction(mTran);
                }
                else
                {

                    _db.CommitTransaction(mTran);


                }
                Store mStore = X.GetCmp<Store>("storeListeEmpotage");
                Empotage mEmp = mClass;

                ModelProxy mProxy = mStore.GetById(mClass.ID);

                mProxy.BeginEdit();

                mProxy.Set(mEmp);

                mProxy.Commit();

                mProxy.EndEdit();

                X.GetCmp<Window>("EmpotageConteneur_Detail").Close();
               
            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mTran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stuffing : SubmitFormNew2Method",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitFormNewMethod(string storeListFees)
        {
            DataSource _db = new DataSource();
            DataTransaction mTran = new DataTransaction();

            try
            {
                Empotage mClass = new Empotage();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("txtEmpotageID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Stuffing load failed.");

                    if (mClass.Desactive)
                        throw new Exception("SubmitFormMethod : Stuffing is disabled ! Please Refresh Overview");
                }

                bool Result = true;

                _db = mClass.db();
                mTran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                mClass = MapFormToObject(mClass);
                Result = mClass.fnUpdate(mTran);


                if (!Result)
                {
                    _db.RollBackTransaction(mTran);
                }
                else
                {

                    _db.CommitTransaction(mTran);


                }
                Store mStore = X.GetCmp<Store>("storeListeEmpotage");
                Empotage mEmp = mClass;


                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                {
                    mStore.Insert(0, mEmp);
                    X.GetCmp<RowSelectionModel>("rowSelectionListeEmpotage").Select(0);
                }


                X.GetCmp<Window>("Empotage_Detail").Close();
                EmpotageViewModel mViewModel = new EmpotageViewModel();
                mViewModel._Empotage = mClass;
                mViewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                #region "Access"
                string UserName = (string)Session["userName"];

                Fonction HasAccess = new Fonction();

                bool ConteneurPermissionRemove = HasAccess.fnGetUserAccessStatus("{7CC44793-F113-4B29-97C0-57576D7955A3}", UserName);
                bool ConteneurPermissionAdd = HasAccess.fnGetUserAccessStatus("{18F294AF-603F-468E-9773-8811D06D7F25}", UserName);
                bool ConteneurPermissionEdit = HasAccess.fnGetUserAccessStatus("{13EB053B-EDC7-4DC7-8E53-DF606BD94FEA}", UserName);


                if (ConteneurPermissionRemove == true)
                {
                    ViewData["ConteneurPermissionRemove"] = true;
                }
                else
                {
                    ViewData["ConteneurPermissionRemove"] = false;
                }

                if (ConteneurPermissionAdd == true)
                {
                    ViewData["ConteneurPermissionAdd"] = true;
                }
                else
                {
                    ViewData["ConteneurPermissionAdd"] = false;
                }

                if (ConteneurPermissionEdit == true)
                {
                    ViewData["ConteneurPermissionEdit"] = true;
                }
                else
                {
                    ViewData["ConteneurPermissionEdit"] = false;
                }
                #endregion
                return new Ext.Net.MVC.PartialViewResult { ViewName = "EmpotageConteneur_Detail", Model = mViewModel, ViewData = ViewData };
            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mTran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stuffing : SubmitFormNewMethod",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult AjustFormMethod(string ItemSelected = "")
        {            
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

                if (mEmpotage.IsNew == false && (dateRetour.Date == mEmpotage.DateRetour.Date))
                    mEmpotage.DateRetour = dateRetour;

                mEmpotage = MapFormAjustementToObject(mEmpotage);

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
                mouvement.Commentaire = "Ajustement Empotage";
                mouvement.Statut = "NA";
                mouvement.UtilisateurCreation = (string)Session["userName"];
                mouvement.UtilisateurModification = (string)Session["userName"];

                result = mouvement.fnUpdate();
                
                if (result)
                {
                    //Store mstore = X.GetCmp<Store>("storeListeRetourEmpotage");
                    //if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    //{
                    //    mstore.Insert(0, mEmpotage);
                    //    X.GetCmp<RowSelectionModel>("rowRetourEmpotage").Select(0);
                    //}
                    //else
                    //{
                    //    ModelProxy mProxy = mstore.GetById(mEmpotage.ID);

                    //    mProxy.BeginEdit();

                    //    mProxy.Set(mEmpotage);

                    //    mProxy.Commit();

                    //    mProxy.EndEdit();
                    //}

                    X.GetCmp<Window>("EmpotageConteneur_Ajustement").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stuffing - Adjustment : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        private RetourEmpotage MapFormAjustementToObject(RetourEmpotage mClass)
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


        public ActionResult OnPrintStuffing(string typeReport)
        {
            ViewData["typeReport"] = typeReport;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintEmpotageReport", ViewData = ViewData };

        }

        public ActionResult PrintStuffing(string TypeReport)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;
                string type = "";
                //if (TypeReport == "Ba")
                //{
                //    report = new rptContratDeVentesBalance() as XtraReport;
                //    type = "Contracts - Balance";
                //}
                //if (TypeReport == "Ex")
                //{
                //    report = new rptContratDeVentesExecution() as XtraReport;
                //    type = "Contracts - Execution";
                //}
                if (TypeReport == "Hi")
                {
                    report = new rptEmpotageHistory() as XtraReport;
                    type = "Stuffing - History";

                }

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["paramCampagne"].Value = X.GetCmp<ComboBox>("SScropYearForReport").SelectedItem.Text;

                report.Parameters["paramExportateurID"].Value = X.GetCmp<ComboBox>("SSexportateurForReport").SelectedItem.Value.ToString();
                report.Parameters["paramExportateur"].Value = X.GetCmp<ComboBox>("SSexportateurForReport").SelectedItem.Text;


                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("SSstartDateForReport").RawText.ToString());
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("SSdueDateForReport").RawText.ToString());

                if (TypeReport == "Hi")
                {

                    report.Parameters["paramStatutID"].Value = X.GetCmp<ComboBox>("SSStatus").SelectedItem.Value.ToString();
                    report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("SSStatus").SelectedItem.Text;

                }
                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Empotage/ViewList', this, '{2}',''),App.FormPrintEmpotageReport.doClose()", Guid.NewGuid(), BaseUrl, type));
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
                return this.Direct();
            }
        }
        public ActionResult OnPrintStuffingReport(string EmpotageID)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;
                report = new rptEmpotageRecap() as XtraReport;
                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["paramEmpotageID"].Value = EmpotageID;
                report.Parameters["paramTypeConteneurID"].Value = -1;
                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Empotage/ViewList', this, 'Packing List','')", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stuffing : Packing List",
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

            return View("ViewReport");
        }
        #region "Methods"
        private Empotage MapFormToObject(Empotage mClass)
        {
            try
            {
                mClass.ID = Guid.Parse(X.GetCmp<Hidden>("txtEmpotageID").Text);

                Embarquement mEmbarquement = new Embarquement();
                mEmbarquement.ID = Guid.Parse(X.GetCmp<TextField>("txtEmbarquementID").Text);
                mEmbarquement.Numero = X.GetCmp<TextField>("txtShipmentNumber").Text;
                mClass.Embarquement = mEmbarquement;

                StationEmpotage mStation = new StationEmpotage();
                mStation.ID = int.Parse(X.GetCmp<ComboBox>("_cmbStation").Text);
                mStation.Nom = X.GetCmp<ComboBox>("_cmbStation").SelectedItem.Text.ToString();
                mClass.StationEmpotage = mStation;

                mClass.NbreConteneurs = 0;
                mClass.NbreLots = 0;
               
                mClass.Commnetaire = X.GetCmp<TextArea>("txtComment").Text;

                mClass.Date = DateTime.Parse(X.GetCmp<DateField>("txtDate").RawText.ToString());

                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];



            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stuffing : MapFormToObject",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return mClass;
        }
        private Empotage MapFormToObject2(Empotage mClass)
        {
            try
            {
               
                Embarquement mEmbarquement = new Embarquement();
                mEmbarquement.ID = Guid.Parse(X.GetCmp<TextField>("txtEmbarquementID").Text);
                mEmbarquement.Numero = X.GetCmp<TextField>("txtShipmentNumber").Text;
                mClass.Embarquement = mEmbarquement;

                StationEmpotage mStation = new StationEmpotage();
                mStation.ID = int.Parse(X.GetCmp<ComboBox>("_cmbStation").Text);
                mStation.Nom = X.GetCmp<ComboBox>("_cmbStation").SelectedItem.Text.ToString();
                mClass.StationEmpotage = mStation;

                mClass.Commnetaire = X.GetCmp<TextArea>("txtComment").Text;

                mClass.Date = DateTime.Parse(X.GetCmp<DateField>("txtDate").RawText.ToString());

                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];



            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stuffing : MapFormToObject2",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return mClass;
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


        private int GetCriteriasValue(string strComponent)
        {
            if (X.GetCmp<ComboBox>(strComponent) != null && X.GetCmp<ComboBox>(strComponent).SelectedItem != null && X.GetCmp<ComboBox>(strComponent).SelectedItem.Value != null)
                return int.Parse(X.GetCmp<ComboBox>(strComponent).SelectedItem.Value.ToString());
            else
                return -1;
        }

        private void DeselectGridRows()
        {
            X.GetCmp<RowSelectionModel>("rowSelectionListeEmpotage").DeselectAll();
        }

        #endregion
    }
}