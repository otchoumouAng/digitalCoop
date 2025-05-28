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
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.Sales;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class RedevanceController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        // GET: Redevance
        public ActionResult Index()
        {
            var mParam = (new Parametres()).fnSelect();

            Parametres mclass = new Parametres();
            mclass = mParam[0] as Parametres;

        
            string StartDate = "01/01/" + DateTime.Now.Year.ToString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("dtpStartDate").RawText = StartDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Exportateur = "{Tous}";

            X.GetCmp<FormPanel>("RedevanceCriteriaPanel").SetTitle("Exportateur : " + Exportateur + " | Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            if (HasAccess.fnGetUserAccessStatus("{7086BCC7-246F-4AD6-8A79-C3D8AF20FF50}", UserName) == false)
                X.GetCmp<MenuItem>("btnGenerate").Disable();
            else
                X.GetCmp<MenuItem>("btnGenerate").Enable();

            if (HasAccess.fnGetUserAccessStatus("{7AF64BEB-0D97-470B-A793-70D81CA5AE06}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExport").Disable();
            else
                X.GetCmp<MenuItem>("mnuExport").Enable();

            //if (HasAccess.fnGetUserAccessStatus("{DF7855E3-2E2F-4E42-9ADA-0F7D5E4FE28A}", UserName) == false)
            //    X.GetCmp<MenuItem>("mnuPrintDebitNote").Disable();
            //else
            //    X.GetCmp<MenuItem>("mnuPrintDebitNote").Enable();

            if (HasAccess.fnGetUserAccessStatus("{18C8937D-86E1-4C1B-B82D-D17FA0ED83B1}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintHistory").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintHistory").Enable();

            X.GetCmp<Hidden>("rdhiddenPermDesactive").SetValue(HasAccess.fnGetUserAccessStatus("{DEBA4DA3-5BF6-4F6A-94A7-1FB8A69959A6}", UserName));
            X.GetCmp<Hidden>("rdhiddenPermActive").SetValue(HasAccess.fnGetUserAccessStatus("{5AA48526-AC10-4FAB-A504-96EA76061FF0}", UserName));
            X.GetCmp<Hidden>("rdhiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{7AF64BEB-0D97-470B-A793-70D81CA5AE06}", UserName));
            X.GetCmp<Hidden>("rdhiddenPermPrintDebitNote").SetValue(HasAccess.fnGetUserAccessStatus("{DF7855E3-2E2F-4E42-9ADA-0F7D5E4FE28A}", UserName));
            X.GetCmp<Hidden>("rdhiddenPermPrintHistory").SetValue(HasAccess.fnGetUserAccessStatus("{18C8937D-86E1-4C1B-B82D-D17FA0ED83B1}", UserName));
            X.GetCmp<Hidden>("rdhiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{AE5EE193-57EE-4802-A273-FD366AF98262}", UserName));
            X.GetCmp<Hidden>("rdhiddenPermGenerate").SetValue(HasAccess.fnGetUserAccessStatus("{7086BCC7-246F-4AD6-8A79-C3D8AF20FF50}", UserName));
            X.GetCmp<Hidden>("rdhiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{F3032882-0EFD-46F4-9C05-FE9DB9548E40}", UserName));
            #endregion
            return View();
        }

        public ActionResult LoadListOfShipmentFees(StoreRequestParameters parameters,string ItemExporter, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {

            int ExportateurID = GetCriteriaValue(ItemExporter);
            Parametres mParam = new Parametres(0);
            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            int Status = int.Parse(ItemStatus);

            var mListe = (new Redevance()).fnSelect(ExportateurID, StartDate, EndDate, Status, mParam.RedevanceCacao);

            //return this.Store(paging);
            return this.Store(mListe);
        }

        public ActionResult onCreate()
        {
            RedevanceViewModel viewmodel = new RedevanceViewModel();

            viewmodel._Redevance = new Redevance();

            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            #region "Access"
            string UserName = (string)Session["userName"];

            Fonction HasAccess = new Fonction();

            bool RedevancePermissionRemove = HasAccess.fnGetUserAccessStatus("{520A20C7-EC16-4915-83B0-A8740AF06D4F}", UserName);
            bool RedevancePermissionAdd = HasAccess.fnGetUserAccessStatus("{8AC25A8C-FA84-4DB6-AD9E-DCB9284ED93C}", UserName);
            bool RedevancePermissionEdit = HasAccess.fnGetUserAccessStatus("{2104C7E2-F229-4E81-A2DA-519B04402305}", UserName);
            bool RedevancePermissionRefresh = HasAccess.fnGetUserAccessStatus("{64A8AD81-5F76-4818-87E7-4ACC54538F47}", UserName);


            if (RedevancePermissionRemove == true)
            {
                ViewData["RedevancePermissionRemove"] = true;
            }
            else
            {
                ViewData["RedevancePermissionRemove"] = false;
            }

            ViewData["RedevancePermissionAdd"] = false;
            

            if (RedevancePermissionEdit == true)
            {
                ViewData["RedevancePermissionEdit"] = true;
            }
            else
            {
                ViewData["RedevancePermissionEdit"] = false;
            }

            if (RedevancePermissionRefresh == true)
            {
                ViewData["RedevancePermissionRefresh"] = true;
            }
            else
            {
                ViewData["RedevancePermissionRefresh"] = false;
            }
            #endregion
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Redevance_Detail", Model = viewmodel, ViewData = ViewData };
        }
        public ActionResult onEdit(string ItemSelected)
        {
            Redevance mclass = JSON.Deserialize<Redevance>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            RedevanceViewModel viewModel = new RedevanceViewModel();

            viewModel._Redevance = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            #region "Access"
            string UserName = (string)Session["userName"];

            Fonction HasAccess = new Fonction();

            bool RedevancePermissionRemove = HasAccess.fnGetUserAccessStatus("{520A20C7-EC16-4915-83B0-A8740AF06D4F}", UserName);
            bool RedevancePermissionAdd = HasAccess.fnGetUserAccessStatus("{8AC25A8C-FA84-4DB6-AD9E-DCB9284ED93C}", UserName);
            bool RedevancePermissionEdit = HasAccess.fnGetUserAccessStatus("{2104C7E2-F229-4E81-A2DA-519B04402305}", UserName);
            bool RedevancePermissionRefresh = HasAccess.fnGetUserAccessStatus("{64A8AD81-5F76-4818-87E7-4ACC54538F47}", UserName);


            if (RedevancePermissionRemove == true)
            {
                ViewData["RedevancePermissionRemove"] = true;
            }
            else
            {
                ViewData["RedevancePermissionRemove"] = false;
            }

            if (RedevancePermissionAdd == true)
            {
                ViewData["RedevancePermissionAdd"] = true;
            }
            else
            {
                ViewData["RedevancePermissionAdd"] = false;
            }

            if (RedevancePermissionEdit == true)
            {
                ViewData["RedevancePermissionEdit"] = true;
            }
            else
            {
                ViewData["RedevancePermissionEdit"] = false;
            }

            if (RedevancePermissionRefresh == true)
            {
                ViewData["RedevancePermissionRefresh"] = true;
            }
            else
            {
                ViewData["RedevancePermissionRefresh"] = false;
            }
            #endregion

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Redevance_Detail", Model = viewModel, ViewData = ViewData };
        }

        public ActionResult onConsult(string ItemSelected)
        {

            Redevance mclass = JSON.Deserialize<Redevance>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            RedevanceViewModel viewmodel = new RedevanceViewModel();
            viewmodel._Redevance = mclass;
            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            ViewData["RedevancePermissionRemove"] = false;
            ViewData["RedevancePermissionAdd"] = false;
            ViewData["RedevancePermissionEdit"] = false;
            ViewData["RedevancePermissionRefresh"] = false;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Redevance_Detail", Model = viewmodel, ViewData = ViewData };
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Redevance mclass = JSON.Deserialize<Redevance>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = mclass.fnGet(mclass.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Shipment fees loading failed.");

                mclass.UtilisateurModification = (string)Session["userName"];

                if (mclass.Desactive)
                    result = mclass.fnActivate();
                else
                    result = mclass.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Shipment fees, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeRedevance");

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
                    Title = "Redevance : OnActivateDeactivate",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }
        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("RedevanceCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemExporter,string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeRedevance");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemExporter"   ,ItemExporter),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus)
                                });



                // collapse criterias areas
                X.GetCmp<FormPanel>("RedevanceCriteriaPanel").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Redevance : Data Validation",
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

            //var StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            //var EndDate = DateTime.Now.ToShortDateString();

            var Exportateur = "{Tous}";
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            // X.GetCmp<FormPanel>("CriteriaPanelShi").SetTitle("Exportateur : " + Exportateur + " | Du : " + StartDate + " Au : " + EndDate);

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

            Parametres mParam = new Parametres(0);
            //DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            //DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            var mListe = (new Embarquement()).fnSelectForFees("{Tous}", ExportateurID, -1, -1, -1, null, null, 0, mParam.RedevanceCacao);

            return this.Store(mListe);
        }

        public ActionResult LoadListOfFees(string ItemExecMode, string ItemRedevanceID)
        {

            List<DataPersist> myList = new List<DataPersist>();
            if (!string.IsNullOrEmpty(ItemRedevanceID))
            {

                if (ItemExecMode == "AddNew")
                {


                }
                else
                {
                    myList = new RedevanceValeur().fnSelect(Guid.Parse(ItemRedevanceID));
                }

                RedevanceValeur mClass = new RedevanceValeur();
                if (myList.Count > 0)
                    mClass = myList[0] as RedevanceValeur;
                else myList = new List<DataPersist>();

            }
            else
            {
                Store mstore = X.GetCmp<Store>("storeListShipFees");
                mstore.RemoveAll();
                myList = null;
            }

            return this.Store(myList);
        }

        public ActionResult SubmitOnSelectShipment(string ItemSelected)
        {
            try
            {
                Parametres mParam = new Parametres(0);
                Embarquement Item = JSON.Deserialize<Embarquement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                if(Item != null)
                {
                    X.GetCmp<TextField>("txtShipmentNumber").Text = Item.Numero;
                    X.GetCmp<TextField>("txtEmbarquementID").Value = Item.ID;
                    X.GetCmp<TextField>("txtExportateurID").Value = Item.Exportateur.ID;
                    X.GetCmp<TextField>("txtExportateurNom").Value = Item.ExportateurNameAndCode;
                    X.GetCmp<TextField>("txtQuantity").Value = Item.Quantite;
                    X.GetCmp<TextField>("txtNbrContainer").Value = Item.NbreConteneur;
                    X.GetCmp<TextField>("txtContratNumeroHidden").Value = Item.Contrat.ContratNumero;
                    Store mstore = X.GetCmp<Store>("storeListShipFees");
                    mstore.RemoveAll();
                    var mTypes = (new RedevanceType()).fnSelect(0, 1,mParam.RedevanceCacao);

                    //foreach (FactureDeduction item in mListe)
                    //{
                    //    i += 1;
                    //    item.IsNew = true;
                    //    store.Insert(i, item);
                    //}

                    if (mTypes.Count > 0)
                    {
                        var i = 0;
                        foreach (RedevanceType item in mTypes)
                        {
                            
                            
                            if (item.CalculType == "Kg")
                            {
                                i += 1;
                                RedevanceValeur mValeur = new RedevanceValeur();
                                mValeur.ID = Guid.NewGuid();
                                mValeur.RedevanceType = new RedevanceType();
                                mValeur.RedevanceType = item;
                                mValeur.Montant = (Item.Quantite * item.Taux);
                                mValeur.Taux = item.Taux;
                                mValeur.IsNew = true;
                                mValeur.Auto = true;
                                mstore.Insert(i, mValeur);
                                X.GetCmp<RowSelectionModel>("rowSelectionShipFees").Select(i);


                            }

                           
                            else if (item.CalculType == "Tc")
                            {
                                i += 1;
                                RedevanceValeur mValeur = new RedevanceValeur();
                                mValeur.ID = Guid.NewGuid();
                                mValeur.RedevanceType = new RedevanceType();
                                mValeur.RedevanceType = item;
                                mValeur.Montant = (Item.NbreConteneur * item.Taux);
                                mValeur.Taux = item.Taux;
                                mValeur.IsNew = true;
                                mValeur.Auto = true;
                                mstore.Insert(i, mValeur);
                                X.GetCmp<RowSelectionModel>("rowSelectionShipFees").Select(i);
                            }

                            else if (item.CalculType == "Do")
                            {
                                i += 1;
                                RedevanceValeur mValeur = new RedevanceValeur();
                                mValeur.ID = Guid.NewGuid();
                                mValeur.RedevanceType = new RedevanceType();
                                mValeur.RedevanceType = item;
                                mValeur.Montant = item.Taux;
                                mValeur.Taux = item.Taux;
                                mValeur.IsNew = true;
                                mValeur.Auto = true;
                                mstore.Insert(i, mValeur);
                                X.GetCmp<RowSelectionModel>("rowSelectionShipFees").Select(i);

                            }
                            else
                            {

                            }

                        }

                        #region "Access"
                        string UserName = (string)Session["userName"];

                        Fonction HasAccess = new Fonction();

                        bool RedevancePermissionAdd = HasAccess.fnGetUserAccessStatus("{8AC25A8C-FA84-4DB6-AD9E-DCB9284ED93C}", UserName);

                        if (RedevancePermissionAdd == true)
                        {
                            X.GetCmp<Button>("btnAddFee").Enable();
                        }
                        else
                        {
                            X.GetCmp<Button>("btnAddFee").Disable();
                        }

                        #endregion
                    }

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

        public ActionResult OnAddFee(string ItemQuantity, string ItemNbrContainer)
        {
            RedevanceValeurViewModel mclass = new RedevanceValeurViewModel();
            mclass._RedevanceValeur = new RedevanceValeur();
            try
            {
                if(!string.IsNullOrEmpty(ItemQuantity) && ItemQuantity !="")
                {
                    ViewData["Quantity"] = ItemQuantity;
                }
                else
                {
                    ViewData["Quantity"] = 0;
                }

                if (!string.IsNullOrEmpty(ItemNbrContainer) && ItemNbrContainer != "")
                {
                    ViewData["NbrContainer"] = int.Parse(ItemNbrContainer);
                }
                else
                {
                    ViewData["NbrContainer"] = 0;
                }

               
                mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
              
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Fee_Detail", Model = mclass, ViewData = ViewData };


        }

        public ActionResult OnEditFee(string ItemQuantity, string ItemNbrContainer, string ItemSelected)
        {
            
            RedevanceValeur mClass = JSON.Deserialize<RedevanceValeur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            RedevanceValeurViewModel mviewModel = new RedevanceValeurViewModel();
            try
            {
                if (mClass.ID == Guid.Empty)
                    return HttpNotFound();

                if (!string.IsNullOrEmpty(ItemQuantity) && ItemQuantity != "")
                {
                    ViewData["Quantity"] = ItemQuantity;
                }
                else
                {
                    ViewData["Quantity"] = 0;
                }

                if (!string.IsNullOrEmpty(ItemNbrContainer) && ItemNbrContainer != "")
                {
                    ViewData["NbrContainer"] = int.Parse(ItemNbrContainer);
                }
                else
                {
                    ViewData["NbrContainer"] = 0;
                }

                
                mviewModel._RedevanceValeur = mClass;
                mviewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Fee_Detail", Model = mviewModel, ViewData = ViewData };


        }

        public ActionResult OnRemoveFee(string ItemSelected)
        {

            try
            {

                RedevanceValeur mClass = JSON.Deserialize<RedevanceValeur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Store mstore = X.GetCmp<Store>("storeListShipFees");
                GridPanel mStore = X.GetCmp<GridPanel>("grpDetailShipFees");

                ModelProxy mProxy = mstore.GetById(mClass.ID);

                mProxy.Drop();
                
                DeselectGridRows();


            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Redevance : Retirer Fee",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }
        public ActionResult OnRefreshFee(string storeListFees, string ItemQuantity, string ItemNbrContainer)
        {

            try
            {

                List<RedevanceValeur> mFees = JSON.Deserialize<List<RedevanceValeur>>(storeListFees, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                if (mFees.Count > 0)
                {
                    decimal quantite = decimal.Parse(ItemQuantity);
                    int conteneur = int.Parse(ItemNbrContainer);
                    Store mstore = X.GetCmp<Store>("storeListShipFees");
                    for (int i = 0; i < mFees.Count; i++)
                    {
                        if (mFees.ElementAt(i).Desactive == false  && mFees.ElementAt(i).Auto == true)
                        {

                            RedevanceType type = new RedevanceType();
                            type.fnGet(mFees.ElementAt(i).RedevanceType.ID);
                            if(type.Taux != mFees.ElementAt(i).RedevanceType.Taux)
                            {
                                RedevanceValeur red = new RedevanceValeur();
                                if (type.CalculType == "Kg")
                                {
                                    red = mFees.ElementAt(i);
                                    red.Montant = (quantite * type.Taux);
                                    red.Taux = type.Taux;
                                    red.IsNew = true;
                                    red.Auto = true;

                                    ModelProxy mProxy = mstore.GetById(red.ID);
                                    mProxy.BeginEdit();
                                    mProxy.Set(red);
                                    mProxy.Commit();
                                    mProxy.EndEdit();
                                }
                                else if(type.CalculType == "Tc")
                                {
                                    red = mFees.ElementAt(i);
                                    red.Montant = (conteneur * type.Taux);
                                    red.Taux = type.Taux;
                                    red.IsNew = true;
                                    red.Auto = true;

                                    ModelProxy mProxy = mstore.GetById(red.ID);
                                    mProxy.BeginEdit();
                                    mProxy.Set(red);
                                    mProxy.Commit();
                                    mProxy.EndEdit();
                                }
                                else if (type.CalculType == "Do")
                                {
                                    red = mFees.ElementAt(i);
                                    red.Montant = type.Taux;
                                    red.Taux = type.Taux;
                                    red.IsNew = true;
                                    red.Auto = true;

                                    ModelProxy mProxy = mstore.GetById(red.ID);
                                    mProxy.BeginEdit();
                                    mProxy.Set(red);
                                    mProxy.Commit();
                                    mProxy.EndEdit();
                                }

                            }

                        }
                       
                    }
                }

                }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Redevance : Refresh",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }
        public ActionResult SubmitOnAddFee()
        {
            RedevanceValeur mValeur = new RedevanceValeur();
           

            Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecModeFee").Value);

            if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
            {
                mValeur.ID = Guid.NewGuid();
                mValeur.IsNew = true;
            }
                
            else
            {
                mValeur.IsNew = false;

                mValeur.ID  = Guid.Parse(GetFormValue("txtRedevanceValeurID"));

                if (mValeur == null || mValeur.ID == Guid.Empty)
                    throw new Exception("SubmitOnAddFee : Fee load failed.");

            }
            if (X.GetCmp<TextField>("txtRate").Text != string.Empty) mValeur.Taux = decimal.Parse(X.GetCmp<TextField>("txtRate").Text);
            if (X.GetCmp<TextField>("txtAmountFee").Text != string.Empty) mValeur.Montant = decimal.Parse(X.GetCmp<TextField>("txtAmountFee").Text);

            mValeur.RedevanceType = new RedevanceType();
            mValeur.RedevanceType.ID = int.Parse(X.GetCmp<ComboBox>("_cmbRedevanceType").Text);
            mValeur.RedevanceType.Designation = X.GetCmp<ComboBox>("_cmbRedevanceType").SelectedItem.Text.ToString();
            
            Store mstore = X.GetCmp<Store>("storeListShipFees");
            mstore.Insert(0, mValeur);
            X.GetCmp<RowSelectionModel>("rowSelectionShipFees").Select(0);
            X.GetCmp<Window>("RedevanceValeur_Detail").Close();
            return this.Direct();
        }

        public ActionResult SubmitFormMethod(string storeListFees)
        {
            DataSource _db = new DataSource();
            DataTransaction mTran = new DataTransaction();

            try
            {
                Redevance mClass = new Redevance();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("txtRedevanceID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Shipment load failed.");

                    if (mClass.Desactive)
                        throw new Exception("SubmitFormMethod : Shipment is disabled ! Please Refresh Overview");
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

                    List<RedevanceValeur> mFees = JSON.Deserialize<List<RedevanceValeur>>(storeListFees, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (mFees.Count > 0)
                    {
                        if (formExecMode != Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                        {
                           Result = mClass.fnRemoveFeeAll(mTran);
                            if (!Result)
                            {
                                _db.RollBackTransaction(mTran);
                            }
                        }
                        

                        if(Result)
                        {
                            for (int i = 0; i < mFees.Count; i++)
                            {
                                if (mFees.ElementAt(i).Desactive == false)
                                {
                                    RedevanceValeur red = new RedevanceValeur();

                                    red = mFees.ElementAt(i);
                                    red.IsNew = true;

                                    red.Redevance = new Redevance();
                                    red.Redevance.ID = mClass.ID;

                                    red.UtilisateurCreation = (string)Session["userName"];
                                    red.UtilisateurModification = (string)Session["userName"];

                                    red.SetDataSource(_db);

                                    Result = red.fnUpdate(mTran);
                                }
                                if (!Result)
                                {
                                    break;
                                }
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
                Store mStore = X.GetCmp<Store>("storeListeRedevance");
                Redevance mRed = mClass;
                

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                {
                    mStore.Insert(0, mRed);
                    X.GetCmp<RowSelectionModel>("rowSelectionListeRedevance").Select(0);
                }
                else
                {
                    ModelProxy mProxy = mStore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mRed);

                    mProxy.Commit();

                    mProxy.EndEdit();


                }

                X.GetCmp<Window>("Redevance_Detail").Close();

            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mTran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Redevance : SubmitFormMethod",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnPrintShipmentFees(string typeReport)
        {
            ViewData["typeReport"] = typeReport;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintRedevanceReport", ViewData = ViewData };

        }

        public ActionResult PrintShipmentFees(string TypeReport)
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
                    report = new rptRedevanceHistory() as XtraReport;
                    type = "Fees - History";

                }

                report.DataSource = DevExpressReportDs.SetDataSource(report);
               
                report.Parameters["paramExportateurID"].Value = X.GetCmp<ComboBox>("SFexportateurForReport").SelectedItem.Value.ToString();
                report.Parameters["paramExportateur"].Value = X.GetCmp<ComboBox>("SFexportateurForReport").SelectedItem.Text;

                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("SFstartDateForReport").RawText.ToString());
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("SFdueDateForReport").RawText.ToString());

                if (TypeReport == "Hi")
                {

                    report.Parameters["paramStatutID"].Value = X.GetCmp<ComboBox>("SFStatus").SelectedItem.Value.ToString();
                    report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("SFStatus").SelectedItem.Text;

                }
                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Redevance/ViewList', this, '{2}',''),App.FormPrintRedevanceReport.doClose()", Guid.NewGuid(), BaseUrl, type));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Redevance : Data Validation",
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
        private Redevance MapFormToObject(Redevance mClass)
        {
            try
            {
                mClass.ID = Guid.Parse(X.GetCmp<Hidden>("txtRedevanceID").Text);

                mClass.Numero = X.GetCmp<TextField>("txtNumber").Text;
               
                Embarquement mEmbarquement = new Embarquement();
                mEmbarquement.Contrat = new ContratDeVentes();
                mEmbarquement.Contrat.ContratNumero = X.GetCmp<TextField>("txtContratNumeroHidden").Value.ToString();
                mEmbarquement.ID = Guid.Parse(X.GetCmp<TextField>("txtEmbarquementID").Text);
                mEmbarquement.Numero = X.GetCmp<TextField>("txtShipmentNumber").Text;
                mEmbarquement.Quantite = decimal.Parse(X.GetCmp<TextField>("txtQuantity").Text);
                mEmbarquement.NbreConteneur = int.Parse(X.GetCmp<TextField>("txtNbrContainer").Text);
                mClass.Quantite = decimal.Parse(X.GetCmp<TextField>("txtQuantity").Text);
                mClass.Montant = decimal.Parse(X.GetCmp<TextField>("txtAmountTotal").Text);
                mClass.MontantBrut = decimal.Parse(X.GetCmp<TextField>("txtAmountTotal").Text);
                mClass.MontantTaxe = 0;
                mClass.NbreConteneur = int.Parse(X.GetCmp<TextField>("txtNbrContainer").Text);

                Exportateur mExportateur = new Exportateur();
                mExportateur.ID = int.Parse(X.GetCmp<TextField>("txtExportateurID").Text);
                mExportateur.Nom = X.GetCmp<TextField>("txtExportateurNom").Text;
                mEmbarquement.Exportateur = mExportateur;

                mClass.Embarquement = mEmbarquement;

                mClass.Commentaire = X.GetCmp<TextArea>("txtComment").Text;

                mClass.Date = DateTime.Now;
                
                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Redevance : MapFormToObject",
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
            X.GetCmp<RowSelectionModel>("rowSelectionShipFees").DeselectAll();
        }

        #endregion
    }
}