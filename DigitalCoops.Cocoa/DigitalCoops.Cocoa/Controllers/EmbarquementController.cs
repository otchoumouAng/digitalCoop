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
    public class EmbarquementController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
       

        // GET: Shipment
        public ActionResult Index()
        {
            var mParam = (new Parametres()).fnSelect();

            Parametres mclass = new Parametres();
            mclass = mParam[0] as Parametres;

            Campagne mCampagne = new Campagne();

            X.GetCmp<ComboBox>("cmbCropYear").SetValue(mclass.Campagne);

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("dtpStartDate").RawText = StartDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Exportateur = "{Tous}";

            string Certification = "{Tous}";
            string Client = "{Tous}";
            string Type = "{Tous}";

            X.GetCmp<FormPanel>("EmbarquementCriteriaPanel").SetTitle("Campagne : " + mclass.Campagne + " | Exportateur : " + Exportateur + " | Customer : " + Client + " | Type of container : " + Type + " | Certification : " + Certification + " | Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            if (HasAccess.fnGetUserAccessStatus("{24907554-2537-468C-9248-408C615FCE03}", UserName) == false)
                X.GetCmp<MenuItem>("btnNew").Disable();
            else
                X.GetCmp<MenuItem>("btnNew").Enable();

            if (HasAccess.fnGetUserAccessStatus("{1EC6C0A2-68D2-4982-B976-E284901FB997}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExport").Disable();
            else
                X.GetCmp<MenuItem>("mnuExport").Enable();

            if (HasAccess.fnGetUserAccessStatus("{985A02D1-C130-4D7F-9D1C-893636D264BD}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintRecap").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintRecap").Enable();

            if (HasAccess.fnGetUserAccessStatus("{29262432-6753-4009-A751-8B54ED40E319}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintPlan").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintPlan").Enable();



            if (HasAccess.fnGetUserAccessStatus("{5061749D-CC14-4E3C-8523-23E51A0E1228}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintHistory").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintHistory").Enable();

            X.GetCmp<Hidden>("emhiddenPermActiver").SetValue(HasAccess.fnGetUserAccessStatus("{BDCB0545-E9A8-4454-BE3D-AE99A566BB79}", UserName));
            X.GetCmp<Hidden>("emhiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{E0EE3E2F-B598-4657-ABDB-B83FEC0117E3}", UserName));
            X.GetCmp<Hidden>("emhiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{1EC6C0A2-68D2-4982-B976-E284901FB997}", UserName));
            X.GetCmp<Hidden>("emhiddenPermPrintOrderTransit").SetValue(HasAccess.fnGetUserAccessStatus("{C00D7452-0559-41E1-B1D6-B411744AEC11}", UserName));
            X.GetCmp<Hidden>("emhiddenPermPrintRecap").SetValue(HasAccess.fnGetUserAccessStatus("{985A02D1-C130-4D7F-9D1C-893636D264BD}", UserName));
            X.GetCmp<Hidden>("emhiddenPermPrintPlan").SetValue(HasAccess.fnGetUserAccessStatus("{29262432-6753-4009-A751-8B54ED40E319}", UserName));
            X.GetCmp<Hidden>("emhiddenPermPrintHistory").SetValue(HasAccess.fnGetUserAccessStatus("{5061749D-CC14-4E3C-8523-23E51A0E1228}", UserName));
            X.GetCmp<Hidden>("emhiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{39CFC700-6304-47CC-A082-C2C40D95E1ED}", UserName));
            X.GetCmp<Hidden>("emhiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{24907554-2537-468C-9248-408C615FCE03}", UserName));
            X.GetCmp<Hidden>("emhiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{3DB48F6F-E0CE-4DA3-8D48-676F1CD6D0D1}", UserName));
            #endregion
            return View();
        }

        public ActionResult LoadListOfShipment(StoreRequestParameters parameters, string ItemCropYear, string ItemExporter, string ItemCustomer, string ItemTypeContainer, string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {

            int ExportateurID = GetCriteriaValue(ItemExporter);
            int ClientID = GetCriteriaValue(ItemCustomer);
            int TypeID = GetCriteriaValue(ItemTypeContainer);
            int CertificationID = GetCriteriaValue(ItemCertification);

            string Campagne = string.IsNullOrEmpty(ItemCropYear) ? "" : ItemCropYear;

            if (!string.IsNullOrEmpty(ItemCropYear) && ItemCropYear.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            int Status = int.Parse(ItemStatus);

            var mListe = (new Embarquement()).fnSelect(Campagne, ExportateurID,CertificationID,TypeID,ClientID, StartDate, EndDate, Status);

            //return this.Store(paging);
            return this.Store(mListe);
        }

        public ActionResult onCreate()
        {
            Parametres mParam = new Parametres(0);
            EmbarquementViewModel viewModel = new EmbarquementViewModel();

            viewModel._Embarquement = new Embarquement();
            viewModel._DefaultExportateur = mParam.Exportateur.ID;
            viewModel._DefaultConditionnement = mParam.ConditionnementDefID;
            viewModel._DefaultClient = mParam.ClientDefID;
            viewModel._DefaultConteneurType = mParam.DefaultConteneurType;
            viewModel._DefaultForwarder = mParam.DefaultTransitaireShipment;
            viewModel._Embarquement.Campagne = new Parametres(0).Campagne;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Embarquement_Detail", Model = viewModel};
        }
        public ActionResult onEdit(string ItemSelected)
        {
            Embarquement mclass = JSON.Deserialize<Embarquement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            EmbarquementViewModel viewModel = new EmbarquementViewModel();
            viewModel._DefaultExportateur = (int?)null;
            viewModel._DefaultConditionnement = (int?)null;
            viewModel._DefaultClient = (int?)null;
            viewModel._DefaultConteneurType = (int?)null;
            viewModel._DefaultForwarder = (int?)null;
            viewModel._Embarquement = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
           

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Embarquement_Detail" , Model = viewModel};
        }
        public ActionResult onConsult(string ItemSelected)
        {

            Embarquement mclass = JSON.Deserialize<Embarquement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            EmbarquementViewModel viewmodel = new EmbarquementViewModel();
            viewmodel._Embarquement = mclass;
            viewmodel._DefaultExportateur = (int?)null;
            viewmodel._DefaultConditionnement = (int?)null;
            viewmodel._DefaultClient = (int?)null;
            viewmodel._DefaultConteneurType = (int?)null;
            viewmodel._DefaultForwarder = (int?)null;
            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Embarquement_Detail", Model = viewmodel };
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Embarquement mclass = JSON.Deserialize<Embarquement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = mclass.fnGet(mclass.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Shipment loading failed.");

                mclass.UtilisateurModification = (string)Session["userName"];

                if (mclass.Desactive)
                    result = mclass.fnActivate();
                else
                    result = mclass.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Shipment, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeEmbarquement");

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
                    Title = "Shipment : OnActivateDeactivate",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult LoadListOfLot(string ItemExecMode, string ItemEmbarquementID)
        {

            List<DataPersist> myList = new List<DataPersist>();
            if (!string.IsNullOrEmpty(ItemEmbarquementID))
            {

                if (ItemExecMode == "AddNew")
                {


                }
                else
                {
                    myList = new Embarquement().fnSelectLot(Guid.Parse(ItemEmbarquementID));
                }

                Embarquement mClass = new Embarquement();
                if (myList.Count > 0)
                    mClass = myList[0] as Embarquement;
                else myList = new List<DataPersist>();

            }
            else
            {
                Store mstore = X.GetCmp<Store>("storeListLot");
                mstore.RemoveAll();
                myList = null;
            }

            return this.Store(myList);
        }

        public ActionResult LoadListOfLot2(string ItemExecMode, string ItemEmbarquementID)
        {

            List<DataPersist> myList = new List<DataPersist>();
            Guid EmbarquementID = Guid.Empty;
            bool isGuid = Guid.TryParse(ItemEmbarquementID, out EmbarquementID);
            if (isGuid)
            {

                if (ItemExecMode != "AddNew")
                    myList = new Embarquement().fnSelectLot(Guid.Parse(ItemEmbarquementID));
                
                //Embarquement mClass = new Embarquement();
                //if (myList.Count > 0)
                //    mClass = myList[0] as Embarquement;
                //else myList = new List<DataPersist>();

            }
            else
            {
                Store mstore = X.GetCmp<Store>("storeListLot2");
                mstore.RemoveAll();
                myList = null;
            }

            return this.Store(myList);
        }

        public  ActionResult GetOldLots(string rows = "") 
        {
            List<Lot> ItemLots = JSON.Deserialize<List<Lot>>(rows, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            Store store = X.GetCmp<Store>("storeListLot2");
            if (ItemLots.Count > 0)
            {

                //store.RemoveAll();

                foreach (var item in ItemLots)
                {
                    item.IsNew = true;
                    store.Insert(0, item);
                    X.GetCmp<RowSelectionModel>("rowSelectionListLot2").Select(0);
                }

            }

            //Store mstore = X.GetCmp<Store>("storeListLot");
            //var te = mstore.Data;
            //var ts = mstore.GetAll();
            //Store mStore = X.GetCmp<Store>("storeListLot2");
            //mStore.Insert(0,mstore.GetAll());
            //GridPanel Grid = X.GetCmp<GridPanel>("grpDetailLot");
            
            //mStore = mstore;

            //
            return this.Direct();
        }
        public ActionResult OnAddLot(string ItemCampagne, string ItemExportateur, string ItemCertificationID, string ItemEmbarquementID, string ItemExecMode, string storeListLot)
        {
            LotViewModel mclass = new LotViewModel();
            mclass._Lot = new Lot();
            try
            {
                Parametres mParam = new Parametres(0);
                ViewData["PoidsStdBrutUnitaire"] = mParam.PoidsStdBrutUnitaire;
                ViewData["PoidsStdNetUnitaire"] = mParam.PoidsStdNetUnitaire;
                ViewData["HiCampagne"] = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
                ViewData["HiExportateurID"] = string.IsNullOrEmpty(ItemExportateur) ? -1 : int.Parse(ItemExportateur);
                ViewData["HiCertificationID"] = string.IsNullOrEmpty(ItemCertificationID) || ItemCertificationID == "null" ? -1 : int.Parse(ItemCertificationID) ;
                ViewData["HiEmbarquementID"] = string.IsNullOrEmpty(ItemEmbarquementID) ? Guid.Empty : Guid.Parse(ItemEmbarquementID); ;
                if (ItemExecMode == "AddNew")
                {
                    mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
                    #region "Access"
                    string UserName = (string)Session["userName"];

                    Fonction HasAccess = new Fonction();

                    bool LotPermissionRemove = HasAccess.fnGetUserAccessStatus("{E9877BC7-258C-4FEE-861B-66C405957B38}", UserName);
                    bool LotPermissionAdd = HasAccess.fnGetUserAccessStatus("{E3A4744F-56AD-4D73-92A7-3A60FF687F48}", UserName);


                    if (LotPermissionRemove == true)
                    {
                        ViewData["LotPermissionRemove"] = true;
                    }
                    else
                    {
                        ViewData["LotPermissionRemove"] = false;
                    }

                    if (LotPermissionAdd == true)
                    {
                        ViewData["LotPermissionAdd"] = true;
                    }
                    else
                    {
                        ViewData["LotPermissionAdd"] = false;
                    }


                    #endregion

                }
                else if(ItemExecMode == "Update")
                {
                    mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                    #region "Access"
                    string UserName = (string)Session["userName"];

                    Fonction HasAccess = new Fonction();

                    bool LotPermissionRemove = HasAccess.fnGetUserAccessStatus("{E9877BC7-258C-4FEE-861B-66C405957B38}", UserName);
                    bool LotPermissionAdd = HasAccess.fnGetUserAccessStatus("{E3A4744F-56AD-4D73-92A7-3A60FF687F48}", UserName);


                    if (LotPermissionRemove == true)
                    {
                        ViewData["LotPermissionRemove"] = true;
                    }
                    else
                    {
                        ViewData["LotPermissionRemove"] = false;
                    }

                    if (LotPermissionAdd == true)
                    {
                        ViewData["LotPermissionAdd"] = true;
                    }
                    else
                    {
                        ViewData["LotPermissionAdd"] = false;
                    }


                    #endregion

                }
                else
                {
                    mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
                    ViewData["LotPermissionRemove"] = false;
                    ViewData["LotPermissionAdd"] = false;

                }

                //List<Lot> ItemLots = JSON.Deserialize<List<Lot>>(storeListLot, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                //Store store = X.GetCmp<Store>("storeListLot2");
                //if (ItemLots.Count > 0)
                //{

                //    store.RemoveAll();

                //    foreach (var item in ItemLots)
                //    {
                //        item.IsNew = true;
                //        store.Insert(0, item);
                //        X.GetCmp<RowSelectionModel>("rowSelectionListLot2").Select(0);
                //    }

                //}
                //int m = 0;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Lot_Detail", Model = mclass, ViewData = ViewData };


        }

        public ActionResult OnAddAvailableLot(string ItemCampagne,string ItemExportateurID,string ItemCertificationID, string ItemEmbarquementID, string ItemExecMode)
        {
            LotViewModel mclass = new LotViewModel();
            mclass._Lot = new Lot();
            try
            {
                ViewData["Campagne"] = ItemCampagne;
                ViewData["Exportateur"] = ItemExportateurID;
                ViewData["Certification"] = ItemCertificationID;
                ViewData["Embarquement"] = ItemEmbarquementID;
                if(ItemExecMode == "AddNew")
                {
                    mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
                }
                else
                {
                    mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                }

                //List<Lot> ItemLots = JSON.Deserialize<List<Lot>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                //string chaine = "";

                //if (ItemLots.Count > 0)
                //{
                    
                //    int i = 0;
                //    foreach (var item in ItemLots)
                //    {
                //        if(i == 0 )
                //            chaine = string.Concat(item.ID);
                //        else                       
                //            chaine = chaine +","+ item.ID;
                //        i++;
                //    }
                //    X.GetCmp<ComboBox>("_cmbCampagne").ReadOnly = true;
                //    X.GetCmp<ComboBox>("_cmbExportateur").ReadOnly = true;

                //}
                //ViewData["collectID"] = chaine;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Select_Lot", Model = mclass, ViewData = ViewData };


        }
        public ActionResult OnRemove(string ItemSelected, string ItemCount)
        {

            try
            {

                Lot mClass = JSON.Deserialize<Lot>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Store mstore = X.GetCmp<Store>("storeListLot2");
                GridPanel mStore = X.GetCmp<GridPanel>("grpDetailLot2");

                ModelProxy mProxy = mstore.GetById(mClass.ID);

                mProxy.Drop();
                int mCount = int.Parse(ItemCount);
                DeselectGridRows();


            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Lot : Retirer Lot",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        //public ActionResult OnEditAvailableLot(string ItemCampagne, string ItemEmbarquement)
        //{
        //    LotViewModel mclass = new LotViewModel();
        //    mclass._Lot = new Lot();
        //    try
        //    {
        //        ViewData["Campagne"] = ItemCampagne;
        //        ViewData["Embarquement"] = ItemEmbarquement;
        //        mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

        //    }
        //    catch (Exception ex)
        //    {
        //        X.MessageBox.Alert("Error", ex.Message).Show();
        //    }
        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "Select_Lot", Model = mclass, ViewData = ViewData };


        //}

        public ActionResult LoadListAvailableLots(string ItemCampagne, string ItemExportateurID,string ItemCertificationID, string ItemEmbarquementID, string ItemExecMode, HashSet<string> LotsExistant)
        {
            List<DataPersist> myList  = new List<DataPersist>();
            int Exp = -1;
            if (!string.IsNullOrEmpty(ItemExportateurID))
            {
                Exp = int.Parse(ItemExportateurID);
            }
           
         
            int Cert = -1;
            if (!string.IsNullOrEmpty(ItemCertificationID) && ItemCertificationID != "null")
            {
                Cert = int.Parse(ItemCertificationID);
            }

            myList = (new Lot()).fnSelectLotForShipment(ItemCampagne, Exp, Cert);
            List<Lot> newList = myList.Cast<Lot>().ToList();
            if (LotsExistant != null)
            {
                newList = newList.Where(l => !LotsExistant.Contains(l.NumeroLot)).ToList();
                return this.Store(newList);
            }
            else
                return this.Store(myList);
        }
        public ActionResult SubmitSelectLot(string ItemExecMode, string ItemSelected)
        {
          
            try
            {
                

                List<Lot> ItemLots = JSON.Deserialize<List<Lot>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                Store store = X.GetCmp<Store>("storeListLot2");
                if(ItemLots.Count > 0)
                {
                    if(ItemExecMode == "AddNew")
                        store.RemoveAll();

                    foreach (var item in ItemLots)
                    {
                        item.IsNew = true;
                        store.Insert(0, item);
                        X.GetCmp<RowSelectionModel>("rowSelectionListLot2").Select(0);
                    }
                    

                }
                

                X.GetCmp<Window>("ListOfLots").Close();
               
            }
            catch (Exception ex)
            {
               
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Select Available Of Lot: Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }


        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("EmbarquementCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult AddListMethod(string ItemExecMode, string ItemSelected)
        {

            try
            {


                List<Lot> ItemLots = JSON.Deserialize<List<Lot>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                Store store = X.GetCmp<Store>("storeListLot");
                if (ItemLots.Count > 0)
                {
                    
                    store.RemoveAll();

                    foreach (var item in ItemLots)
                    {
                        item.IsNew = true;
                        store.Insert(0, item);
                        //X.GetCmp<RowSelectionModel>("rowSelectionListLot").Select(0);
                    }
                    X.GetCmp<ComboBox>("_cmbCampagne").ReadOnly = true;
                    X.GetCmp<ComboBox>("_cmbExportateur").ReadOnly = true;
                    X.GetCmp<ComboBox>("_cmbCertification").ReadOnly = true;

                }
                else
                {
                    X.GetCmp<ComboBox>("_cmbCampagne").ReadOnly = false;
                    X.GetCmp<ComboBox>("_cmbExportateur").ReadOnly = false;
                    X.GetCmp<ComboBox>("_cmbCertification").ReadOnly = false;
                }

                X.GetCmp<Window>("Lot_Detail").Close();

            }
            catch (Exception ex)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Select Available Of Lot: Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }


       

        public ActionResult OnRefresh(string ItemCropYear, string ItemCustomer, string ItemExporter, string ItemTypeContainer, string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeEmbarquement");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCropYear"   ,ItemCropYear),
                                    new Ext.Net.Parameter("ItemCustomer"   ,ItemCustomer),
                                    new Ext.Net.Parameter("ItemExporter"   ,ItemExporter),
                                    new Ext.Net.Parameter("ItemTypeContainer"      ,ItemTypeContainer),
                                    new Ext.Net.Parameter("ItemCertification"      ,ItemCertification),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus)
                                });



                // collapse criterias areas
                X.GetCmp<FormPanel>("EmbarquementCriteriaPanel").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Shipment : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnRefreshForAvaibleLot(string ItemCampagne, string ItemExportateurID, string ItemCertificationID, string ItemEmbarquementID, string ItemExecMode, string rowsInList)
        {
            try
            {
                List<Lot> mLot = JSON.Deserialize<List<Lot>>(rowsInList, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                HashSet<string> LotsExistant = new HashSet<string>();
                if (mLot.Count > 0)
                {
                    LotsExistant = new HashSet<string>( mLot.Select(l => l.NumeroLot).ToList());
                }
                Store mstore = X.GetCmp<Store>("storeListAvailableLots");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCampagne"   ,ItemCampagne),
                                    new Ext.Net.Parameter("ItemExportateurID"   ,ItemExportateurID),
                                    new Ext.Net.Parameter("ItemCertificationID"   ,ItemCertificationID),
                                    new Ext.Net.Parameter("ItemEmbarquementID"      ,ItemEmbarquementID),
                                    new Ext.Net.Parameter("ItemExecMode"      ,ItemExecMode),
                                    new Ext.Net.Parameter("LotsExistant"   ,LotsExistant)
                                });



                // collapse criterias areas
                X.GetCmp<FormPanel>("EmbarquementCriteriaPanel").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Shipment : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitFormMethod(string storeListLot)
        {
            DataSource _db = new DataSource();
           // DataTransaction mTran = new DataTransaction();
            DataTransaction mTran = new DataTransaction();

            try
            {
                Embarquement mClass = new Embarquement();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("txtEmbarquementID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("UpdateFormMethod : Shipment load failed.");

                    if (mClass.Desactive)
                        throw new Exception("UpdateFormMethod : Shipment is disabled ! Please Refresh Overview");
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
                    
                    List<Lot> mLot = JSON.Deserialize<List<Lot>>(storeListLot, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (mLot.Count > 0)
                    {
                        if (formExecMode != Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                        {
                            mClass.fnRemoveLotAll();
                        }

                        for (int i = 0; i < mLot.Count; i++)
                        {
                            if (mLot.ElementAt(i).Desactive == false)
                            {
                                Lot Lot = new Lot();
                                if (formExecMode != Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                                {
                                    Lot.fnGetLite(mLot.ElementAt(i).ID);
                                }
                                else
                                {
                                    Lot = mLot.ElementAt(i);
                                    Lot.RowVersionKey = Convert.FromBase64String(mLot[i].RowVersionKey.ToString());
                                }
                                Lot.Embarquement = new Embarquement();
                                Lot.SetDataSource(_db);
                                Lot.Embarquement.ID = mClass.ID;

                                Lot.UtilisateurCreation = (string)Session["userName"];
                                Lot.UtilisateurModification = (string)Session["userName"];
                                Result = Lot.fnUpdateShipment(mTran);
                            }
                            if (!Result)
                            {
                                break;
                            }
                        }

                        if (!Result)
                        {
                            _db.RollBackTransaction(mTran);
                        }
                        else
                        {
                            _db.CommitTransaction(mTran);                            
                        }

                    }



                }
                Store mStore = X.GetCmp<Store>("storeListeEmbarquement");
                Embarquement mEmb = mClass;
                //Embarquement mEmb = new Embarquement();
                //mEmb.fnGet(mClass.ID);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                {
                    mStore.Insert(0, mEmb);
                    X.GetCmp<RowSelectionModel>("rowSelectionListeEmbarquement").Select(0);
                }
                else
                {
                    ModelProxy mProxy = mStore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mEmb);

                    mProxy.Commit();

                    mProxy.EndEdit();


                }

                X.GetCmp<Window>("Embarquement_Detail").Close();

            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mTran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Shipment : SubmitFormMethod",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnPrintOrdreTransit(string EmbarquementID)
        {
           
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            //return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Facture{0}', '{1}/Facture/ViewReport?id={0}&IsCopy={2}', this, 'Facture','')", IdFacture, BaseUrl, ReportIscopy));

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Embarquement{0}', '{1}/Embarquement/ViewReportOrdreTransit?id={0}', this,'Ordre de transit', '')", EmbarquementID, BaseUrl));
        }

        public ActionResult ViewReportOrdreTransit(string id)
        {
            try
            {
                rptOrdreDeTransit report = new rptOrdreDeTransit();

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["paramID"].Value = id;

                report.Parameters["paramID"].Visible = false;
                ViewData["Report"] = report;

                return View("ViewReportResult");
            }
            catch (Exception ex)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Shipment : Ordre Transit",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
        }

        public ActionResult OnPrintShipment(string typeReport)
        {
            ViewData["typeReport"] = typeReport;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintEmbarquementReport", ViewData = ViewData };

        }

        public ActionResult OnRecap(string typeReport)
        {
            ViewData["typeReport"] = typeReport;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintShipmentRecapReport", ViewData = ViewData };

        }

        public ActionResult PrintShipment(string TypeReport)
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
                    report = new rptEmbarquementHistory() as XtraReport;
                    type = "Shipment - History";

                }

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["paramCampagne"].Value = X.GetCmp<ComboBox>("SSHcropYearForReport").SelectedItem.Text;

                report.Parameters["paramExportateurID"].Value = X.GetCmp<ComboBox>("SSHexportateurForReport").SelectedItem.Value.ToString();
                report.Parameters["paramExportateur"].Value = X.GetCmp<ComboBox>("SSHexportateurForReport").SelectedItem.Text;

                report.Parameters["paramCertificationID"].Value = X.GetCmp<ComboBox>("SSHCertificationForReport").SelectedItem.Value.ToString();
                report.Parameters["paramCertification"].Value = X.GetCmp<ComboBox>("SSHCertificationForReport").SelectedItem.Text;

                report.Parameters["paramTypeConteneurID"].Value = X.GetCmp<ComboBox>("SSHTypeContForReport").SelectedItem.Value.ToString();
                report.Parameters["paramTypeConteneur"].Value = X.GetCmp<ComboBox>("SSHTypeContForReport").SelectedItem.Text;

                report.Parameters["paramClientID"].Value = X.GetCmp<ComboBox>("SSHClientForReport").SelectedItem.Value.ToString();
                report.Parameters["paramClient"].Value = X.GetCmp<ComboBox>("SSHClientForReport").SelectedItem.Text;

                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("SSHstartDateForReport").RawText.ToString());
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("SSHdueDateForReport").RawText.ToString());

                if (TypeReport == "Hi")
                {

                    report.Parameters["paramStatutID"].Value = X.GetCmp<ComboBox>("SSHStatus").SelectedItem.Value.ToString();
                    report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("SSHStatus").SelectedItem.Text;

                }
                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Embarquement/ViewList', this, '{2}',''),App.FormPrintEmbarquementReport.doClose()", Guid.NewGuid(), BaseUrl, type));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Shipment : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
        }

        public ActionResult PrintShipmentRecap(string TypeReport)
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
                if (TypeReport == "Recap")
                {
                    report = new rptShipmentRecapHistory() as XtraReport;
                    type = "Shipment recap - History";

                }

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["paramCampagne"].Value = X.GetCmp<ComboBox>("SSHcropYearForReport").SelectedItem.Text;

                report.Parameters["paramExportateurID"].Value = X.GetCmp<ComboBox>("SSHexportateurForReport").SelectedItem.Value.ToString();
                report.Parameters["paramExportateur"].Value = X.GetCmp<ComboBox>("SSHexportateurForReport").SelectedItem.Text;

                //report.Parameters["paramCertificationID"].Value = X.GetCmp<ComboBox>("SSHCertificationForReport").SelectedItem.Value.ToString();
                //report.Parameters["paramCertification"].Value = X.GetCmp<ComboBox>("SSHCertificationForReport").SelectedItem.Text;

                //report.Parameters["paramTypeConteneurID"].Value = X.GetCmp<ComboBox>("SSHTypeContForReport").SelectedItem.Value.ToString();
                //report.Parameters["paramTypeConteneur"].Value = X.GetCmp<ComboBox>("SSHTypeContForReport").SelectedItem.Text;

                //report.Parameters["paramClientID"].Value = X.GetCmp<ComboBox>("SSHClientForReport").SelectedItem.Value.ToString();
                //report.Parameters["paramClient"].Value = X.GetCmp<ComboBox>("SSHClientForReport").SelectedItem.Text;

                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("SSHstartDateForReport").RawText.ToString());
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("SSHdueDateForReport").RawText.ToString());

                //if (TypeReport == "Recap")
                //{

                //    report.Parameters["paramStatutID"].Value = X.GetCmp<ComboBox>("SSHStatus").SelectedItem.Value.ToString();
                //    report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("SSHStatus").SelectedItem.Text;

                //}
                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Embarquement/ViewList', this, '{2}',''),App.FormPrintShipmentRecapReport.doClose()", Guid.NewGuid(), BaseUrl, type));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Shipment : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
        }

        public ActionResult PrintListLot(string EmbarquementID)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;
                report = new rptEmbarquementListLot() as XtraReport;
                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["paramID"].Value = EmbarquementID;
                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Embarquement/ViewList', this, 'List of Lot','')", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Shipment : Print List of Lot",
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
        private Embarquement MapFormToObject(Embarquement mClass)
        {
            try
            {
                mClass.ID = Guid.Parse(X.GetCmp<Hidden>("txtEmbarquementID").Text);

                mClass.Numero = X.GetCmp<TextField>("txtNumber").Text;
                mClass.Reference = X.GetCmp<TextField>("txtReference").Text;

                mClass.Campagne = X.GetCmp<ComboBox>("_cmbCampagne").Text;

                Exportateur mExportateur = new Exportateur();
                mExportateur.ID = int.Parse(X.GetCmp<ComboBox>("_cmbExportateur").Text);
                mExportateur.Nom = X.GetCmp<ComboBox>("_cmbExportateur").SelectedItem.Text.ToString();
                mClass.Exportateur = mExportateur;

                Client mClient= new Client();
                mClient.ID = int.Parse(X.GetCmp<ComboBox>("_cmbCustomer").Text);
                mClient.Nom = X.GetCmp<ComboBox>("_cmbCustomer").SelectedItem.Text.ToString();
                mClass.Client = mClient;

                ContratDeVentes mContrat = new ContratDeVentes();
                mContrat.ID = Guid.Parse(X.GetCmp<ComboBox>("_cmbContract").Text);
                mContrat.NumeroCode = X.GetCmp<ComboBox>("_cmbContract").SelectedItem.Text.ToString();
                mClass.Contrat = new ContratDeVentes();
                mClass.Contrat.fnGet(mContrat.ID);

                Enregistrement mEnr = new Enregistrement();
                mEnr.ID = Guid.Parse(X.GetCmp<ComboBox>("_cmbRegistration").Text);
                mEnr.NumeroCode = X.GetCmp<ComboBox>("_cmbRegistration").SelectedItem.Text.ToString();
                mEnr.EnregistrementDetailID = Guid.Parse(X.GetCmp<ComboBox>("HiddenEnregistrementDetail").Value.ToString());                              
                
                mClass.Enregistrement = new Enregistrement();
                mClass.Enregistrement.fnGet(mEnr.ID);

                mClass.Date = DateTime.Parse(X.GetCmp<DateField>("txtDate").RawText.ToString());

                Banque mDomiciliation = new Banque();
                mDomiciliation.ID = int.Parse(X.GetCmp<ComboBox>("_cmbDomiciliation").Text);
                mDomiciliation.Nom = X.GetCmp<ComboBox>("_cmbDomiciliation").SelectedItem.Text.ToString();
                mClass.Domiciliation = mDomiciliation;

                mClass.PeriodeEmbarquement = DateTime.Parse(X.GetCmp<DateField>("txtPeriodeEmbarquement").RawText.ToString());


                DestinationExport mDestination = new DestinationExport();
                mDestination.ID = int.Parse(X.GetCmp<ComboBox>("_cmbDestination").Text);
                mDestination.Nom = X.GetCmp<ComboBox>("_cmbDestination").SelectedItem.Text.ToString();
                mClass.DestinationExport = mDestination;

                mClass.ETA = DateTime.Parse(X.GetCmp<DateField>("txtETA").RawText.ToString());

                Navire mNavire = new Navire();
                mNavire.ID = int.Parse(X.GetCmp<ComboBox>("_cmbShip").Text);
                mNavire.Nom = X.GetCmp<ComboBox>("_cmbShip").SelectedItem.Text.ToString();
                mClass.Navire = mNavire;

                mClass.PeriodeBL = DateTime.Parse(X.GetCmp<DateField>("txtPeriodeBL").RawText.ToString());

                if (X.GetCmp<TextField>("txtQuantity").Text != string.Empty) mClass.Quantite = decimal.Parse(X.GetCmp<TextField>("txtQuantity").Text);

                if (X.GetCmp<TextField>("txtGrossWeight").Text != string.Empty) mClass.PoidsBrut = decimal.Parse(X.GetCmp<TextField>("txtGrossWeight").Text);
                if (X.GetCmp<TextField>("txtNetWeight").Text != string.Empty) mClass.PoidsNet = decimal.Parse(X.GetCmp<TextField>("txtNetWeight").Text);

                Conditionnement mCondi = new Conditionnement();
                mCondi.ID = int.Parse(X.GetCmp<ComboBox>("_cmbPackaging").Text);
                mCondi.Designation = X.GetCmp<ComboBox>("_cmbPackaging").SelectedItem.Text.ToString();
                mClass.Conditionnement = mCondi;

                CompagnieMaritime mComp = new CompagnieMaritime();
                mComp.ID = int.Parse(X.GetCmp<ComboBox>("_cmbShippingLine").Text);
                mComp.Nom = X.GetCmp<ComboBox>("_cmbShippingLine").SelectedItem.Text.ToString();
                mClass.CompagnieMaritime = mComp;

                Consignee mCons = new Consignee();
                mCons.ID = int.Parse(X.GetCmp<ComboBox>("_cmbConsingee").Text);
                mCons.Designation = X.GetCmp<ComboBox>("_cmbConsingee").SelectedItem.Text.ToString();
                mClass.Consignee = mCons;

                ConteneurType mConteneurType = new ConteneurType();
                mConteneurType.ID = int.Parse(X.GetCmp<ComboBox>("_cmbTypeContainer").Text);
                mConteneurType.Designation = X.GetCmp<ComboBox>("_cmbTypeContainer").SelectedItem.Text.ToString();
                mClass.ConteneurType = mConteneurType;

                if (X.GetCmp<NumberField>("txtNbrContainer").Text != string.Empty) mClass.NbreConteneur = int.Parse(X.GetCmp<NumberField>("txtNbrContainer").Text);

                if (X.GetCmp<ComboBox>("_cmbCertification").Text != string.Empty)
                {
                    Certification mCertification = new Certification();
                    mCertification.ID = int.Parse(X.GetCmp<ComboBox>("_cmbCertification").Text);
                    mCertification.Designation = X.GetCmp<ComboBox>("_cmbCertification").SelectedItem.Text.ToString();
                    mClass.Certification = mCertification;
                }
                else
                {
                    mClass.Certification = null;
                }


                if (X.GetCmp<ComboBox>("_cmbModeTraitement").Text != string.Empty)
                {
                    ModeTraitement mMode = new ModeTraitement();
                    mMode.ID = int.Parse(X.GetCmp<ComboBox>("_cmbModeTraitement").Text);
                    mMode.Designation = X.GetCmp<ComboBox>("_cmbModeTraitement").SelectedItem.Text.ToString();
                    mClass.ModeTraitement = mMode;
                }
                else
                {
                    mClass.Certification = null;
                }

                Transitaire mTranstaire = new Transitaire();
                mTranstaire.ID = int.Parse(X.GetCmp<ComboBox>("_cmbTransitaire").Text);
                mTranstaire.Nom = X.GetCmp<ComboBox>("_cmbTransitaire").SelectedItem.Text.ToString();
                mClass.Transitaire = mTranstaire;

                mClass.MentionOT = X.GetCmp<TextArea>("txtMentionOT").Text;
               

                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];



            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Shipment : MapFormToObject",
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
            //X.Js.Call("App.rowSelectionListe.deselectAll();");
            X.GetCmp<RowSelectionModel>("rowSelectionListeEmbarquement").DeselectAll();
        }

        #endregion

        public ActionResult OnAddLotFictif()
        {
            LotViewModel mclass = new LotViewModel();
            Parametres mParam = new Parametres(0);
            mclass._Lot = new Lot();
            mclass._DefaultCampagne = mParam.Campagne;
            mclass._DefaultExportateur = mParam.Exportateur.ID;
            mclass._PoidsStdBrutUnitaire = mParam.PoidsStdBrutUnitaire;
            mclass._PoidsStdNetUnitaire = mParam.PoidsStdNetUnitaire;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLotFictif", Model = mclass, };
        }

        [HttpPost]
        public ActionResult UpdateFormLot()
        {
            try
            {
                Lot mClass = new Lot();
                bool result = true;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecModeLot").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtLotId")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("UpdateFormMethod : Production - Lot - load failed.");

                }

                mClass = MapFormToObjectToLot(mClass);
                result = mClass.fnUpdateFictif();

                if (result)
                {                    
                    X.GetCmp<Window>("FormLotFictif").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - Lot : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        private Lot MapFormToObjectToLot(Lot mClass)
        {
            Campagne campagne = new Campagne();
            campagne.Designation = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text.ToString();
            mClass.Campagne = campagne;

            Exportateur exportateur = new Exportateur();
            exportateur.ID = int.Parse(GetFormValue("cmbExportateur"));
            exportateur.Nom = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text.ToString();
            mClass.Exportateur = exportateur;

            Certification certification = null;

            if (!string.IsNullOrEmpty(GetFormValue("cmbCertificationLot")))
            {
                certification = new Certification();
                certification.ID = int.Parse(GetFormValue("cmbCertificationLot"));
                certification.Designation = X.GetCmp<ComboBox>("cmbCertificationLot").SelectedItem.Text.ToString();
            }
            mClass.Certification = certification;

            OrdreProduction production = null;
            //production.fnGetByNumber(X.GetCmp<TextField>("txtOrdreProduction").Text);
            if (!string.IsNullOrEmpty(GetFormValue("txtOrdreProduction")))
            {
                production = new OrdreProduction();
                production.ID = Guid.Parse(GetFormValue("txtOrdreProductionID"));
                production.NumeroProduction = X.GetCmp<TextField>("txtOrdreProduction").Text;
                mClass.Production = production;
            }

            mClass.NumeroLot = X.GetCmp<TextField>("TxtNumero").Text;
            mClass.DateLot = DateTime.Parse(X.GetCmp<DateField>("TxtDateLot").RawText.ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
            mClass.EstQueue = bool.Parse(X.GetCmp<Checkbox>("ChkEstQueue").Value.ToString());
            mClass.EstReusine = bool.Parse(X.GetCmp<Checkbox>("ChkEstReusine").Value.ToString());
            if (mClass.EstReusine)
            {
                mClass.NumeroLot = X.GetCmp<TextField>("TxtNumero").Text;
                bool result = new Lot().fnGetReusinageByNumero(mClass.NumeroLot);
                if (!result)
                    throw new Exception("Lot : Lot's Number Not Found");
            }

            mClass.EstManuel = true;

            LotType lottype = new LotType();
            lottype.ID = int.Parse(GetFormValue("cmbTypeLotF"));
            lottype.Designation = X.GetCmp<ComboBox>("cmbTypeLotF").SelectedItem.Text.ToString();
            mClass.LotType = lottype;

            mClass.NumeroLot = X.GetCmp<TextField>("TxtNumero").Text;

            if (X.GetCmp<TextField>("txtNombreSacs").Text != string.Empty) mClass.NombreSacs = int.Parse(X.GetCmp<TextField>("txtNombreSacs").Text.Replace(" ", ""));
            if (X.GetCmp<TextField>("txtPoidsBrut").Text != string.Empty) mClass.PoidsBrut = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrut").Text);
            if (X.GetCmp<TextField>("txtTareSacs").Text != string.Empty) mClass.TareSacs = decimal.Parse(X.GetCmp<TextField>("txtTareSacs").Text);
            if (X.GetCmp<TextField>("txtTarePalette").Text != string.Empty) mClass.TarePalette = decimal.Parse(X.GetCmp<TextField>("txtTarePalette").Text);
            if (X.GetCmp<TextField>("txtPoidsNet").Text != string.Empty) mClass.PoidsNet = decimal.Parse(X.GetCmp<TextField>("txtPoidsNet").Text);

            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }

    }
}