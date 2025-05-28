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
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Business;
using Tms.Classes.Business.Sites;
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
    public class TransfertFevesController : BaseController
    {

        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";

        public TransfertFevesController()
        {
            //string cultureName = "en";
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
        }

        // GET: Fournisseur
        public ActionResult Index()
        {
            string UserName = (string)Session["userName"];
            Parametres mParam = new Parametres(0);                        

            //ViewBag.DefaultExportateur = mParam.Exportateur.ID;
            ViewBag.DefaultCampagne = mParam.Campagne;
            ViewBag.TransfertFeve = mParam.IDTransfertFevesAgence;

            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            Fonction HasAccessFunction = new Fonction();
            bool HasAccessAllSite = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            ViewBag.HasAccessAllSite = HasAccessAllSite;

            X.GetCmp<FormPanel>("CriteriaPanel").SetTitle("Site : " + mSiteParDefaut.Nom + ", TODAY - Campagne : " + mParam.Campagne + ", Exportateur : " + mParam.Exportateur.Nom);
            #region Set Function's Access

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{d417f49c-85a8-4208-9930-aeea2152e9a0}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{ab2c9b7d-b84d-4f8a-9c4d-cdf97b82e7b9}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{cf86eb84-e97c-45db-a5b7-948ab81691a2}")))
                X.GetCmp<Button>("mnuExportDeliveries").Enable();
            else
                X.GetCmp<Button>("mnuExportDeliveries").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{371b6119-d2cb-4467-a279-2e5968defd16}")))
                X.GetCmp<Button>("mnuPrintTransferSummary").Enable();
            else
                X.GetCmp<Button>("mnuPrintTransferSummary").Disable();

            X.GetCmp<Hidden>("tfhiddenPermModifier").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{5e6efa0a-6c19-48a3-b19a-76ada218edaf}")));
            X.GetCmp<Hidden>("tfhiddenPermDesactiver").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{705c6d91-f88f-4b38-b86a-f47bc92a7ff1}")));
            X.GetCmp<Hidden>("tfhiddenPermPrintTransferSheet").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{3bd1749a-242c-4c11-b596-e998216e114f}")));
            X.GetCmp<Hidden>("tfhiddenPermApprove").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{e0c60bfd-a8e1-4677-8e47-4ad99ae9a577}")));
            X.GetCmp<Hidden>("tfhiddenPermDesactiverApresApprouve").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{4131fb85-7b58-477f-9068-8f0a43878fd0}")));

            #endregion
            return View();
        }

        public ActionResult LoadDeliveriesDetailByNumbers(string ItemNumbers)
        {
            string[] liste = ItemNumbers.Split(',');            
            List<DataPersist> myList = new List<DataPersist>();
            Livraison mClass = new Livraison();
            for (int i = 0; i < liste.Count(); i++)
            {
                mClass.fnSelectByNumber(liste[i]);
                myList.Add(mClass);
            }            
            
            if (myList.Count > 0)
                mClass = myList[0] as Livraison;
            else myList = new List<DataPersist>();
            return this.Store(myList);                    

        }

        public ActionResult LoadDeliveryBySupplierAll(string ItemFournisseur, string ItemMode, string ItemExecMode, string ItemSpotPrice)
        {
            List<DataPersist> myList = new List<DataPersist>();
            if (!string.IsNullOrEmpty(ItemFournisseur) && !string.IsNullOrEmpty(ItemMode) && ItemMode == "2")
            {

                if (ItemExecMode == Tms.Components.Settings.EnumsDefinition.UPDATE || ItemExecMode == Tms.Components.Settings.EnumsDefinition.CONSULT)
                {
                    myList = new Livraison().fnSelectBySpotPrice(Guid.Parse(ItemSpotPrice), int.Parse(ItemFournisseur));
                }                
                
                Livraison mClass = new Livraison();
                if (myList.Count > 0)
                    mClass = myList[0] as Livraison;
                else myList = new List<DataPersist>();
                
            }
            else
            {
                Store mstore = X.GetCmp<Store>("storeDelaitDeliveriesListe");
                mstore.RemoveAll();
                myList = null;
            }

            
                //GridPanel gridpanel = X.GetCmp<GridPanel>("grpDetailDeliveries");
                //RowSelectionModel smTD = gridpanel.GetSelectionModel() as RowSelectionModel;
                //smTD.SelectedRows.Add(new SelectedRow(clsted.ID.ToString()));
                //smTD.UpdateSelection();
            
            return this.Store(myList);
        }

        public ActionResult GetDeliveryBySupplierAll(string ItemFournisseur,string ItemMode)
        {

            List<DataPersist> myList = new List<DataPersist>();
            if (!string.IsNullOrEmpty(ItemFournisseur) || !string.IsNullOrEmpty(ItemMode))
            {
                if (ItemMode == "2")
                {
                    myList = new Livraison().fnSelect("{all}", -1, ItemFournisseur, DateTime.Now, DateTime.Now,-1,-1);

                    //List<DataPersist> myList = new Livraison().fnSelect("{all}", -1, ItemFournisseur, DateTime.Now);
                    Livraison mClass = new Livraison();
                    if (myList.Count > 0)
                        mClass = myList[0] as Livraison;
                    else myList = new List<DataPersist>();
                    
                }
                return this.Store(myList);
            }
            else
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Title = "Prix Negocié : PRICE",
                    Message = "Mode Application and Supplier must be selected",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Store(null);
            }

        }

        public ActionResult LoadFournisseurAll()
        {
            List<DataPersist> myList = new Fournisseur().fnSelect();
            Fournisseur mClass = new Fournisseur();

            mClass.ID = -1;
            mClass.Nom = "{Tous}";
            myList.Insert(0, mClass);
            mClass = myList[0] as Fournisseur;

            return this.Store(myList);
        }

        public ActionResult LoadFournisseur()
        {
            List<DataPersist> myList = new Fournisseur().fnSelect();
            Fournisseur mClass = new Fournisseur();
            
            mClass = myList[0] as Fournisseur;

            return this.Store(myList);
        }        

        public ActionResult OnCreate()
        {
            TransfertFevesViewModel mclass = new TransfertFevesViewModel();

            try
            {
                string UserName = (string)Session["userName"];                
                //Site mSiteParDefaut = new Site();
                //bool result = mSiteParDefaut.fnGetDefaultSite();

                Site msite = new Site();
                bool result = msite.fnGetBySiteByUserName(UserName);

                mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

                Parametres mParam = new Parametres(0);
                mclass._TransfertFeves = new TransfertFeves();
                mclass._TransfertFeves.Campagne = new Campagne();
                mclass._TransfertFeves.Campagne.Designation = mParam.Campagne;
                mclass._TransfertFeves.Sites = new Site();
                mclass._TransfertFeves.Sites.ID = msite.ID;
                mclass._TransfertFeves.Sites.Nom = msite.Nom;
                mclass._TransfertFeves.Destination = new Destination();
                mclass._TransfertFeves.Destination.ID = mParam.IDDestinationParDefaut;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormTransfertFeves", Model = mclass };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            TransfertFevesViewModel mclass = new TransfertFevesViewModel();

            try
            {
                mclass._TransfertFeves = new TransfertFeves();
                mclass._TransfertFeves = JSON.Deserialize<TransfertFeves>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                mclass._TransfertFeves.fnGet(mclass._TransfertFeves.ID);

                string UserName = (string)Session["userName"];

                //Site mSiteParDefaut = new Site();
                //bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

                mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

                //Parametres mParam = new Parametres(0);
                //mclass._TransfertFeves = new TransfertFeves();
                //mclass._TransfertFeves.Campagne = new Campagne();
                //mclass._TransfertFeves.Campagne.Designation = mParam.Campagne;
                //mclass._TransfertFeves.Sites = new Site();
                //mclass._TransfertFeves.Sites.ID = mSiteParDefaut.ID;
                //mclass._TransfertFeves.Sites.Nom = mSiteParDefaut.Nom;
                //mclass._TransfertFeves.Destination = new Destination();
                //mclass._TransfertFeves.Destination.ID = mSiteParDefaut.Destination.ID;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormTransfertFeves", Model = mclass };
        }

        public ActionResult OnApprove(string ItemSelected)
        {
            TransfertFevesViewModel mclass = new TransfertFevesViewModel();

            try
            {
                mclass._TransfertFeves = new TransfertFeves();
                mclass._TransfertFeves = JSON.Deserialize<TransfertFeves>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                mclass._TransfertFeves.fnGet(mclass._TransfertFeves.ID);

                string UserName = (string)Session["userName"];

                //Site mSiteParDefaut = new Site();
                //bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

                mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;

              
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormApproveTransfert", Model = mclass };
        }


        public ActionResult OnConsult(string ItemSelected)
        {
            TransfertFevesViewModel mclass = new TransfertFevesViewModel();

            try
            {
                mclass._TransfertFeves = new TransfertFeves();
                mclass._TransfertFeves = JSON.Deserialize<TransfertFeves>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                mclass._TransfertFeves.fnGet(mclass._TransfertFeves.ID);

                string UserName = (string)Session["userName"];

                //Site mSiteParDefaut = new Site();
                //bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

                mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;


            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormApproveTransfert", Model = mclass };
        }
        public ActionResult OnActivateDeactivate(string ItemSelected)
        {

            try
            {
                TransfertFeves mClass = JSON.Deserialize<TransfertFeves>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                Guid mId = mClass.ID;

                bool result = mClass.fnGet(mId);

                //bool deActivate = mClass.Desactive;

                if (!result)
                    throw new Exception("OnActivateDeactivate : Delivery loading failed.");

                result = mClass.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Operation failed.");

                Store mstore = X.GetCmp<Store>("storeTransferList");

                ModelProxy mProxy = mstore.GetById(mClass.ID);

                mProxy.BeginEdit();

                mProxy.Set(mClass);

                mProxy.Commit();

                mProxy.EndEdit();

                DeselectGridRows();
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }

        public ActionResult OnUpdateSupplier(string ItemSelected, string idLivraison = "")
        {
            LivraisonViewModel viewModel = new LivraisonViewModel();
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            try
            {                
                Livraison mclass = new Livraison();
                
                if (!string.IsNullOrEmpty(ItemSelected))
                {
                    mclass = JSON.Deserialize<Livraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
                    mclass.fnGet(mclass.ID);
                    
                }                
                else
                    throw new Exception("An error occured ! please retry");

                if (mclass.ID == Guid.Empty)
                    return HttpNotFound();
                
                viewModel._Livraison = mclass;
                Parametres mParam = new Parametres(0);
                viewModel._Parametres = mParam;
                return new Ext.Net.MVC.PartialViewResult { ViewName = "Livraison_UpdateFournisseur", Model = viewModel};
            }
            catch (Exception ex)
            {
                return JavaScript(X.MessageBox.Alert("Error", ex.Message).ToScript());
            }
        }


        [HttpPost]
        public ActionResult SubmitFormMethod(string FromWeighing = "")
        {
            try
            {
                TransfertFeves mClass = new TransfertFeves();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);
                int nbSacs = 0;
                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtTransfertID")));
                    nbSacs = mClass.NombreSacs;
                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Error on loading.");
                }

                mClass = MapFormToObject(mClass);
                bool result = false;
                if (mClass.IsNew == false && mClass.NombreSacs > 0)
                    result = mClass.fnUpdate();
                //bool 

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeTransferList");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);

                        X.GetCmp<RowSelectionModel>("rowSelectionListe").Select(0);
                    }
                    else
                    {

                        mProxy = mStore.GetById(mClass.ID);
                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    CloseDetailWindow();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error : SubmitFormMethod", ex.Message).Show();
            }
            
            return this.Direct();
                       
        }

        [HttpPost]
        public ActionResult Finalize(string FromWeighing = "")
        {
            TransfertFeves mClass = new TransfertFeves();
            try
            {
                
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                mClass.IsNew = false;

                mClass.fnGet(Guid.Parse(GetFormValue("TxtTransfertID")));

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("SubmitFormMethod : Error on loading.");


                //mClass = MapFormToObject(mClass);
                mClass.Statut = "AP";
                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];

                bool result = mClass.fnApprove();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeTransferList");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);

                        X.GetCmp<RowSelectionModel>("rowSelectionListe").Select(0);
                    }
                    else
                    {

                        mProxy = mStore.GetById(mClass.ID);
                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormApproveTransfert").Close();

                    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                    X.Js.Call("GenerateTicket", mClass.ID, BaseUrl);
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error : SubmitFormMethod", ex.Message).Show();
            }            

            return this.Direct();

        }


        [HttpPost]
        public ActionResult SubmitSupplierChangeMethod()
        {
            try
            {
                Livraison mClass = new Livraison();

                mClass.IsNew = false;

                mClass.fnGet(Guid.Parse(GetFormValue("hiddenLivraisonID")));

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("SubmitFormMethod : Delivery load failed.");

                mClass.Fournisseur = new Tms.Classes.Business.Fournisseur();
                mClass.Fournisseur.ID = int.Parse(GetFormValue("cmbDetFournisseur"));
                mClass.Fournisseur.Nom = X.GetCmp<ComboBox>("cmbDetFournisseur").SelectedItem.Text.ToString();                

                bool result = mClass.fnUpdateSupplier();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeTransferList");

                    ModelProxy mProxy;

                    mProxy = mStore.GetById(mClass.ID);
                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();

                    X.GetCmp<Window>("frmUpdateSupplierDelivery").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error : SubmitSupplierChangeMethod", ex.Message).Show();
            }

            return this.Direct();

        }


        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne, string ItemStartDate, string ItemEndDate, string ItemStatus, string ItemSite)
        {            
            
            Parametres para = new Parametres(0);
            string defaultCrop = para.Campagne;

            string Crop = defaultCrop;
            if (!String.IsNullOrEmpty(ItemCampagne))
                Crop = ItemCampagne;

            string status = string.IsNullOrEmpty(ItemStatus) ? "-1" : ItemStatus;
            if (!string.IsNullOrEmpty(ItemStatus) && ItemStatus.Contains("null"))
            {
                status = "-1";
            }

            int siteID = GetCritriaValue(ItemSite);          

            DateTimeFormatInfo ukDtfi = new CultureInfo("fr-FR", false).DateTimeFormat;
            DateTime StartDate = DateTime.Now.AddDays(-1);
            if (!String.IsNullOrEmpty(ItemStartDate) && !ItemStartDate.Contains("1/1/0001"))
                StartDate = DateTime.Parse(ItemStartDate);

            DateTime EndDate = DateTime.Now;
            if (!String.IsNullOrEmpty(ItemEndDate) && !ItemEndDate.Contains("1/1/0001"))
                EndDate = DateTime.Parse(ItemEndDate);

            var mListe = (new TransfertFeves()).fnSelect(Crop, StartDate, EndDate,status,siteID);
           
            string filterHeaders = this.Request.Params["filterheader"];
            return this.Store(mListe);
        }

        public ActionResult UpdateSummary()
        {
            var mList = (List<DataPersist>)Session["mListeLivraion"];
            try
            {
                int NbrsOfRows = mList.Count();
                
                int SumSacsDeclares = 0;
                decimal SumPoidsDeclare = 0;
                if (mList.Count > 0)
                {
                    //Livraison mLivraison = mList[0] as Livraison;
                    foreach (Livraison item in mList)
                    {
                        SumSacsDeclares += item.SacsDeclares;
                        SumPoidsDeclare += item.PoidsDeclare;
                    }
                }
                
                var mChaine = string.Format("Livraison(s) : {0} <br/> Total Sacs Estimés : {1} <br/> Total Tonnage estimé : {2} (Kg)", NbrsOfRows.ToString("#,#"), SumSacsDeclares.ToString("#,#"), SumPoidsDeclare.ToString("#,#"));                
                X.GetCmp<Label>("lblResume").Html = mChaine;                                
                              
            }
            catch (Exception)
            {

                throw;
            }
            Session["mListeLivraion"] = "";
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemStatus, string ItemStartDate, string ItemEndDate, string ItemExportateur, string ItemSite)
        {            
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemStartDate, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemEndDate, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeTransferList");

                //mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemCampagne"   ,ItemCampagne),
                                new Ext.Net.Parameter("ItemStartDate"           ,ItemStartDate),
                                new Ext.Net.Parameter("ItemEndDate"           ,ItemEndDate),
                                new Ext.Net.Parameter("ItemStatus"           ,ItemStatus),
                                new Ext.Net.Parameter("ItemSite"           ,ItemSite)
                            });

                // collapse criterias areas
                X.GetCmp<FormPanel>("CriteriaPanel").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Beans Transfer : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });

            }            
            return this.Direct();
        }
               

        public ActionResult OnRefreshForAvailableDeliveries(string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd)
        {
            Store mstore = X.GetCmp<Store>("storeListDeliveries");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd)
                                });

            string title = X.GetCmp<FormPanel>("CriteriaPanelFD").Title;

            title += " Fournisseur : " + X.GetCmp<ComboBox>("cmbFournisseurFD").SelectedItem.Text;

            title += ", From " + X.GetCmp<DateField>("dtpStartDateFD").RawText.ToString() + " to " + X.GetCmp<DateField>("dtpEndDateFD").RawText.ToString();

            X.GetCmp<FormPanel>("CriteriaPanelFD").Title = title;

            // collapse criterias areas
            X.GetCmp<FormPanel>("CriteriaPanelFD").Collapse(Direction.Top, false);

            return this.Direct();
        }

        public ActionResult LoadListOfAvailableDeliveries(string ItemSite, string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd)
        {
            int? siteID = ItemSite == "" ? (int?)null : int.Parse(ItemSite);
            int? fournisseurID = ItemFournisseur == "" ? -1 : int.Parse(ItemFournisseur);
            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            var mListe = (new BonDeLivraison()).fnSelectForTransfertFeves((int)siteID, (int)fournisseurID, StartDate, EndDate);
            return this.Store(mListe);
        }


        public ActionResult OnFilter()
        {
            X.GetCmp<FormPanel>("CriteriaPanel").ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnDisplayTransfertList(string ItemStartDate = "", string ItemEndDate = "")
        {
            string UserName = (string)Session["userName"];

            if (string.IsNullOrEmpty(ItemStartDate)) ItemStartDate = DateTime.Now.ToShortDateString();
            if (string.IsNullOrEmpty(ItemEndDate)) ItemEndDate = DateTime.Now.ToShortDateString();
            DateTime datedebut = DateTime.ParseExact(ItemStartDate, "d", CultureInfo.CurrentUICulture);
            DateTime datedfin = DateTime.ParseExact(ItemEndDate, "d", CultureInfo.CurrentUICulture);
            ViewData["datedebut"] = datedebut;
            ViewData["datefin"] = datedfin;

            Site mSiteParDefaut = new Site();
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            Parametres mParam = new Parametres(0);

            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;
            if (mParam.Site == mSiteParDefaut.ID) ViewData["UrlSite"] = "LoadSiteAll";
            else ViewData["UrlSite"] = "LoadSiteByAccess";

            ViewData["Titre"] = "Print List of Transfer";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormListTransfertCritere", ViewData = ViewData };
        }

        public ActionResult PrintList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;

                report = new rptTransfertFevesList() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                DateTime dateDebut = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetStartDate").RawText, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetEndDate").RawText, "d", CultureInfo.CurrentUICulture);

                report.Parameters["CampagneID"].Value = GetFormValue("cmbDetCropYear");                

                report.Parameters["paramSite"].Value = GetFormValue("cmbDetSite");
                report.Parameters["paramSiteText"].Value = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text;

                //report.Parameters["paramFournisseur"].Value = GetFormValue("cmbDetFournisseur");
                //report.Parameters["paramFournisseurText"].Value = X.GetCmp<ComboBox>("cmbDetFournisseur").SelectedItem.Text;

                report.Parameters["paramStatut"].Value = GetFormValue("cmbDetStatus");
                report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("cmbDetStatus").SelectedItem.Text;

                report.Parameters["paramDateDebut"].Value = dateDebut;
                report.Parameters["paramDateFin"].Value = dateFin;

                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/TransfertFeves/ViewList', this, 'Beans Transfer',''),App.FormListTransfertCritere.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Beans Transfer : Data Validation",
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

        #region "Methods"
        private void CloseDetailWindow()
        {
            X.GetCmp<Window>("FormTransfertFeves").Close();           
        }

        private string GetFormValue(string id_Component)
        {
            string data = Request.Form[id_Component];
            return string.IsNullOrEmpty(data) ?string.Empty: data;
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

        private TransfertFeves MapFormToObject(TransfertFeves mClass)
        {            
            mClass.Sites = new Site();
            mClass.Sites.ID = int.Parse(GetFormValue("txtSiteID"));
            mClass.Sites.Nom = X.GetCmp<TextField>("txtNomSite").Text;

            mClass.Destination = new Tms.Classes.Shared.Destination();
            mClass.Destination.ID = int.Parse(GetFormValue("cmbDetDestination"));
            mClass.Destination.Nom = X.GetCmp<ComboBox>("cmbDetDestination").SelectedItem.Text.ToString();

            mClass.Campagne = new Tms.Classes.Shared.Campagne();           
            mClass.Campagne.Designation = X.GetCmp<ComboBox>("cmbDetCrop").SelectedItem.Text.ToString();
            mClass.DateTransfert = DateTime.Parse(X.GetCmp<DateField>("txtDateTransfert").RawText.ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second).AddMilliseconds(DateTime.Now.Millisecond);
            
            if (X.GetCmp<TextField>("txtNombreSacs").Text != string.Empty) mClass.NombreSacs = int.Parse(X.GetCmp<TextField>("txtNombreSacs").Text);
            if (X.GetCmp<TextField>("txtPoidsBrut").Text != string.Empty) mClass.PoidsBrut = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrut").Text);
            if (X.GetCmp<TextField>("txtTareSacs").Text != string.Empty) mClass.TareSacs = decimal.Parse(X.GetCmp<TextField>("txtTareSacs").Text);

            if (X.GetCmp<TextField>("txtTarePalettes").Text != string.Empty) mClass.TarePalette = decimal.Parse(X.GetCmp<TextField>("txtTarePalettes").Text);            

            if (X.GetCmp<TextField>("txtTarePalettes").Text != string.Empty) mClass.TarePalette = decimal.Parse(X.GetCmp<TextField>("txtTarePalettes").Text);
            if (X.GetCmp<TextField>("txtNetWeight").Text != string.Empty) mClass.PoidsNet = decimal.Parse(X.GetCmp<TextField>("txtNetWeight").Text);
            if (X.GetCmp<TextField>("txtPrixMoyen").Text != string.Empty) mClass.PrixMoyen = decimal.Parse(X.GetCmp<TextField>("txtPrixMoyen").RawText);

            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }
                
        private int GetCritriaValue(string strComponent)
        {
            int value;
        
            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
                return value;
            else
                return -1;
        }
        
        private void DeselectGridRows()
        {
            //X.Js.Call("App.rowSelectionListe.deselectAll();");
            X.GetCmp<RowSelectionModel>("rowSelectionListe").DeselectAll();
        }


        #endregion

        public ActionResult OnPrintDeliveriesList(string exportateur,string cropYear, string deliveryType, string supplier, string startDate, string endDate, string supplierName, string deliveryTypeDesignation, string exportateurNom = "")
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                Session["paramCropYear"] = cropYear;
                Session["paramExportateur"] = exportateur;
                Session["paramDeliveryType"] = Int32.Parse(deliveryType);
                Session["paramDeliveryTypeText"] = deliveryTypeDesignation;
                Session["paramExportateurNom"] = exportateurNom;

                Session["paramSupplier"] = Int32.Parse(supplier);
                Session["paramSupplierText"] = supplierName;

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Livraison/ViewDeliveriesList', this, 'Liste des livraisons',''),App.frmDeliveryList.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Livraison : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }


        }

        public ActionResult ViewDeliveriesList()
        {
            XtraReport report = null;           

            report = new rptDeliveriesList() as XtraReport;

            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["campagneID"].Value = Session["paramCropYear"];
            report.Parameters["paramCampagneText"].Value = Session["paramCropYear"];
            report.Parameters["paramExportateur"].Value = Session["paramExportateur"];            

            report.Parameters["paramTypeLivraison"].Value = Session["paramDeliveryType"];

            report.Parameters["paramFournisseur"].Value = Session["paramSupplier"];

            report.Parameters["paramTypeLivraisonText"].Value = Session["paramDeliveryTypeText"];

            report.Parameters["paramFournisseurText"].Value = Session["paramSupplierText"];

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];

            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];
            report.Parameters["paramExportateurNom"].Value = Session["paramExportateurNom"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

        //public ActionResult LoadListOfAvailableDeliveries(string ItemSite, string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd, string ItemListeAdded = "", string ItemPeseeID = null)
        //{
        //    int? siteID = ItemSite == "" ? (int?)null : int.Parse(ItemSite);
        //    int? fournisseurID = ItemFournisseur == "" ? -1 : int.Parse(ItemFournisseur);
        //    DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
        //    DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
                     
        //    var mListe = (new BonDeLivraison()).fnSelectForTransfertFeves((int)siteID, (int)fournisseurID, StartDate, EndDate);
        //    return this.Store(mListe);
        //}

        public ActionResult SubmitDeliveries(string ItemSelected, string transfertID = "")
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();
            try
            {
                Guid IdTransfert;
                bool parseResult = false;                
                if (string.IsNullOrEmpty(transfertID))
                    throw new Exception("Error");
                else
                    parseResult = Guid.TryParse(transfertID, out IdTransfert);
                
                List<BonDeLivraison> mLivraison = new List<BonDeLivraison>();
                mLivraison = JSON.Deserialize<List<BonDeLivraison>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
                List<TransfertFevesDetail> mDetail = new List<TransfertFevesDetail>();
                TransfertFevesDetail mTransfert = new TransfertFevesDetail();
                mLivraison = CalculateValuesByNbrOfBags(mLivraison);
                Store store = X.GetCmp<Store>("storeListeBL");
                bool result = false;

                decimal totalPrix = 0;
                if (mLivraison.Count > 0)
                {
                    _db = mTransfert.db();
                    mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                    foreach (BonDeLivraison item in mLivraison.Where(x => x.IsNew == true))
                    {
                        mTransfert = new TransfertFevesDetail();
                        mTransfert.BonDeLivraison = new BonDeLivraison();
                        mTransfert.TransfertFeves = new TransfertFeves();
                        mTransfert.SetDataSource(_db);
                        mTransfert.TransfertFeves.ID = IdTransfert;
                        mTransfert.NombreSacs = item.NbreSacs;
                        mTransfert.DateTransfertDetail = DateTime.Now;
                        mTransfert.PoidsBrut = item.PoidsBrut;
                        mTransfert.PoidsNet = item.PoidsNetAccepte;
                        mTransfert.TarePalette = item.TarePalettes;
                        mTransfert.TareSacs = item.TareSacs;
                        mTransfert.PrixMoyen = item.PrixMoyen;
                        mTransfert.Montant = (item.PrixMoyen * item.PoidsBrut);
                        totalPrix += item.PrixMoyen;
                        mTransfert.ID = Guid.NewGuid();
                        mTransfert.BonDeLivraison.ID = item.ID;
                        mTransfert.BonDeLivraison.Numero = item.Numero;
                        mTransfert.BonDeLivraison.Livraison = new Livraison();
                        mTransfert.BonDeLivraison.Livraison.Numero = item.Livraison.Numero;
                        mTransfert.UtilisateurCreation = (string)Session["userName"];
                        mTransfert.UtilisateurModification = (string)Session["userName"];

                        result = mTransfert.fnUpdate(mtran);
                        if (!result)
                        {
                            _db.RollBackTransaction(mtran);
                            break;
                        }                            
                        mDetail.Add(mTransfert);
                    }

                    if (result) _db.CommitTransaction(mtran);
                    store.Add(mDetail);
                }
                X.GetCmp<Window>("FormTransfert_SelectDeliveries").Close();

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Beans Transfer - List Of Livraisons disponibles : OnAdd",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR
                });
            }
            return this.Direct();
        }

        private List<BonDeLivraison> CalculateValuesByNbrOfBags(List<BonDeLivraison> cLivraison)
        {
            foreach (var item in cLivraison)
            {
                if (item.NbreSacs != item.NbreSacsAccepteInitial && item.IsNew)
                {
                    item.PoidsBrut = Math.Round((item.NbreSacs * item.PoidsBrut) / item.NbreSacsAccepteInitial);
                    item.TareSacs = Math.Round((item.NbreSacs * item.TareSacs) / item.NbreSacsAccepteInitial);
                    item.TarePalettes = Math.Round((item.NbreSacs * item.TarePalettesAjustee) / item.NbreSacsAccepteInitial);
                    item.PoidsLivre = item.PoidsBrut - item.TareSacs - item.TarePalettes;
                    //item.TotalRetention = Math.Round((item.NbreSacs * item.TotalRetention) / item.NbreSacsAccepteInitial);
                    item.PoidsNetAccepte = Math.Round((item.NbreSacs * item.PoidsNetAccepte) / item.NbreSacsAccepteInitial);
                }
            }
            return cLivraison;
        }

        public ActionResult OnSelectDelivery(int? siteID, string TransfertID = "")
        {
            try
            {
                Guid IdTransfert;
                bool parseResult = false;
                if (!siteID.HasValue)
                    throw new Exception("");
                if (string.IsNullOrEmpty(TransfertID))
                    throw new Exception("Error");
                else
                {
                    parseResult = Guid.TryParse(TransfertID, out IdTransfert);
                }

                TransfertFevesDetail mModel = new TransfertFevesDetail();
                mModel.TransfertFeves = new TransfertFeves();
                mModel.TransfertFeves.ID = IdTransfert;
                mModel.TransfertFeves.Sites = new Site();
                mModel.TransfertFeves.Sites.ID = (int)siteID;
                return new Ext.Net.MVC.PartialViewResult { ViewName = "FormTransfert_SelectDeliveries", Model = mModel };

            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Beans Transfer : Delivery",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }

        }

        public ActionResult SubmitFormBeforeDeliveries()
        {
            try
            {
                Guid transfertID = Guid.Empty;
                DateTime dateTransfert = new DateTime();
                TransfertFeves mTransfert = new TransfertFeves();
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mTransfert.IsNew = true;
                else
                {
                    formExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                    mTransfert.IsNew = false;
                    bool isGuid = Guid.TryParse(GetFormValue("TxtTransfertID"), out transfertID);
                    if (isGuid)
                        mTransfert.fnGet(transfertID);
                    //else
                    //    mPesee.fnGet(WeighingID);

                    if (mTransfert == null || mTransfert.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Transfer load failed.");

                    dateTransfert = mTransfert.DateTransfert;
                }

                bool result = false;

                mTransfert = MapFormToObject(mTransfert);
                if (mTransfert.IsNew == false && (dateTransfert.Date == mTransfert.DateTransfert.Date))
                    mTransfert.DateTransfert = dateTransfert;

                result = mTransfert.fnUpdate();

                //if (WeighingIsCreated)
                //    WeighingID = mPesee.ID;

                if (result)
                {
                    //WeighingIsCreated = true;
                    //WeighingID = mPesee.ID;

                    Store mstore = X.GetCmp<Store>("storeTransferList");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mTransfert);
                        X.GetCmp<RowSelectionModel>("rowSelectionListe").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mTransfert.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mTransfert);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }
                    X.GetCmp<Container>("zonePalettes").Enable();
                    X.GetCmp<Container>("btnAddNewLivraison").Enable();
                    X.GetCmp<Hidden>("TxtTransfertID").SetValue(mTransfert.ID);
                    X.GetCmp<Hidden>("hiddenExecMode").SetValue("Update");
                    return this.Direct();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Beans Transfer : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        public ActionResult SelectDeliveries(string TransfertID)
        {
            try
            {
                Guid IDTransfert;
                bool result = Guid.TryParse(TransfertID, out IDTransfert);
                TransfertFevesDetail mDetails = new TransfertFevesDetail();
                var Liste = mDetails.fnSelect(IDTransfert);
                return this.Store(Liste);
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Beans Transfer : Data Validation",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Store(null);
            }

        }

        public ActionResult OnRemoveDelivery(string ItemSelected)
        {
            try
            {
                TransfertFevesDetail mDetail = JSON.Deserialize<TransfertFevesDetail>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                if (mDetail.ID != Guid.Empty)
                {
                    Store mstore = X.GetCmp<Store>("storeListeBL");

                    if (mDetail.IsNew)
                    {
                        ModelProxy _proxy = mstore.GetById(mDetail.ID);
                        _proxy.Drop();
                        return this.Direct();
                    }
                    else
                    {
                        mDetail.fnGet(mDetail.ID);
                        if (mDetail == null || mDetail.ID == Guid.Empty)
                            throw new Exception("RemoveDelivery : Retirer Delivery failed.");

                        mDetail.UtilisateurModification = (string)Session["userName"];

                        bool result = mDetail.fnRemove();

                        if (result)
                        {
                            ModelProxy mProxy = mstore.GetById(mDetail.ID);
                            mProxy.Drop();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Beans Transfer - : Retirer Pallets Weight",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnPrintTransfert(string IdTransfert, string IsCopy = "")
        {
            //string BaseUrl = "";
            bool ReportIsCopy = false;

            if (string.IsNullOrEmpty(IsCopy))
                ReportIsCopy = true;

            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));            
            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'TransferSheet{0}', '{1}/TransfertFeves/ViewReport?id={0}&IsCopy={2}', this, 'Transfer Sheet Report','')", IdTransfert, BaseUrl, ReportIsCopy));
        }

        public ActionResult ViewReport(string id, bool IsCopy)
        {
            rptFicheTransfertCacao report = new rptFicheTransfertCacao();

            report.DataSource = DevExpressReportDs.SetDataSource(report);
            report.Parameters["paramID"].Value = id;
            //report.Parameters["IsCopy"].Value = IsCopy;

            ViewData["Report"] = report;

            return View();
        }

    }
}