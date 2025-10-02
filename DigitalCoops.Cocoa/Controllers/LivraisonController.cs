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
    public class LivraisonController : BaseController
    {

        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";

        public LivraisonController()
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

            ViewBag.DefaultExportateur = mParam.Exportateur.ID;
            ViewBag.DefaultCampagne = mParam.Campagne;
            ViewBag.LivraisonAchat = mParam.LivraisonTypeAchat.ID;

            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;
            Fonction HasAccessFunction = new Fonction();
            bool HasAccessAllSite = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            ViewBag.HasAccessAllSite = HasAccessAllSite;

            X.GetCmp<FormPanel>("CriteriaPanel").SetTitle("Site : " + mSiteParDefaut.Nom + ", Jour - Campagne : " + mParam.Campagne + ", Exportateur : " + mParam.Exportateur.Nom + ", Type De Livraison : " + mParam.LivraisonTypeAchat.Designation);
            #region Set Function's Access

            
            Fonction HasAccess = new Fonction();            
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{3AF93DDB-A66C-48B7-B30A-9CCE0E112D25}", UserName);

            if (HasAccess.fnGetUserAccessStatus("{F4020901-8D1E-4D5C-BB43-CB88968258B1}", UserName) == false)
                X.GetCmp<Button>("btnNew").Disable();
            else
                X.GetCmp<Button>("btnNew").Enable();

            if (HasAccess.fnGetUserAccessStatus("{6F6DC5E2-EB03-4771-96DB-998BD114FC1F}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportDeliveries").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportDeliveries").Enable();

            if (HasAccess.fnGetUserAccessStatus("{3D95A38A-8788-40FB-B725-CE28C6291E9D}", UserName) == false)
                X.GetCmp<MenuItem>("mnuLvhiddenPermPrintDeliveriesList").Disable();
            else
                X.GetCmp<MenuItem>("mnuLvhiddenPermPrintDeliveriesList").Enable();

            X.GetCmp<Hidden>("LvhiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{F4020901-8D1E-4D5C-BB43-CB88968258B1}", UserName));
            X.GetCmp<Hidden>("LvhiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{426E2F49-E761-4F0F-8F66-CB01029EE39D}", UserName));
            X.GetCmp<Hidden>("LvhiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{E8B49F6F-CB42-4E0A-850C-D178C12B758A}", UserName));
            X.GetCmp<Hidden>("LvhiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{6F6DC5E2-EB03-4771-96DB-998BD114FC1F}", UserName));
            X.GetCmp<Hidden>("LvhiddenPermPrintDeliveriesList").SetValue(HasAccess.fnGetUserAccessStatus("{3D95A38A-8788-40FB-B725-CE28C6291E9D}", UserName));
            X.GetCmp<Hidden>("LvhiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{3AF93DDB-A66C-48B7-B30A-9CCE0E112D25}", UserName));
            X.GetCmp<Hidden>("LvhiddenPermUpdateSupplier").SetValue(HasAccess.fnGetUserAccessStatus("{4B6B9FF5-E474-484A-A1E2-DDC6C32EC84F}", UserName));
            X.GetCmp<Hidden>("LvhiddenPermUpdateType").SetValue(HasAccess.fnGetUserAccessStatus("{3ea07cec-914a-4827-8636-70b0b08e0633}", UserName));
            X.GetCmp<MenuItem>("mnuChangeType").SetHidden(!HasAccess.fnGetUserAccessStatus("{3ea07cec-914a-4827-8636-70b0b08e0633}", UserName));
            X.GetCmp<Hidden>("LvhiddenPermUpdateSuperieur").SetValue(HasAccess.fnGetUserAccessStatus("{dc357eb3-ce4f-4d14-9a15-423b19b55721}", UserName));
            X.GetCmp<MenuItem>("mnuChangeSmall").SetHidden(!HasAccess.fnGetUserAccessStatus("{dc357eb3-ce4f-4d14-9a15-423b19b55721}", UserName));
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

        public ActionResult OnSelectDelivery()
        {
            LivraisonViewModel mclass = new LivraisonViewModel();

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Livraisons_Disponibles_Finalisation", Model = mclass };
        }

        public ActionResult OnCreate()
        {
            LivraisonViewModel mclass = new LivraisonViewModel();

            try
            {
                //Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                //mViewport.Mask();
               
                mclass._Livraison = new Livraison();
                
                ViewData["StatutLivraison"] = 0;
                ViewData["defaultSite"] = new Parametres(0).Site;
                
                mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

                Parametres mParam = new Parametres(0);
                mclass._Campagne = mParam.Campagne;

                mclass._Recolte = mParam.Recolte;

                mclass._Exportateur = mParam.Exportateur;

                //mclass._Parametres = new Parametres(0);

            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }


            return new Ext.Net.MVC.PartialViewResult { ViewName = "Livraison_Detail", Model = mclass, ViewData = ViewData };
        }

        public ActionResult OnEdit(string ItemSelected , string idLivraison = "")
        {
            LivraisonViewModel viewModel = new LivraisonViewModel();

            try
            {
                //Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                //mViewport.Mask();
                Livraison mclass = new Livraison();
                if (string.IsNullOrEmpty(idLivraison))
                {
                    mclass = JSON.Deserialize<Livraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    mclass.fnGet(mclass.ID);
                    ViewData["FromWeighing"] = "NO";
                }
                else if (!string.IsNullOrEmpty(idLivraison))
                {
                    mclass.fnGet(Guid.Parse(idLivraison));
                    ViewData["FromWeighing"] = "YES";
                }              
                else
                    throw new Exception("An error occured ! please retry");

                if (mclass.ID == Guid.Empty)
                    return HttpNotFound();

                int _StatutLivraison = new Livraison().fnGetWeightStatus(mclass.ID);
                ViewData["StatutLivraison"] = _StatutLivraison;
                viewModel._Livraison = mclass;

                if (_StatutLivraison == (int)Tms.Classes.Business.StatutLivraison.Val.SecondWeight)
                    viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
                else
                    viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

                Parametres mParam = new Parametres(0);
                
                viewModel._Campagne = mParam.Campagne;
                //viewModel._Parametres = new Parametres(0);
                viewModel._Recolte = mParam.Recolte;

                viewModel._Exportateur = mParam.Exportateur;

                return new Ext.Net.MVC.PartialViewResult { ViewName = "Livraison_Detail", Model = viewModel, ViewData = ViewData };
            }
            catch (Exception ex)
            {
               return JavaScript(X.MessageBox.Alert("Error", ex.Message).ToScript());
            }           
        }

        public ActionResult OnUpdateSmall(string ItemSelected, string idLivraison = "")
        {
            LivraisonViewModel viewModel = new LivraisonViewModel();

            try
            {
                //Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                //mViewport.Mask();
                Livraison mclass = new Livraison();
                if (string.IsNullOrEmpty(idLivraison))
                {
                    mclass = JSON.Deserialize<Livraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    mclass.fnGet(mclass.ID);
                    ViewData["FromWeighing"] = "NO";
                }
                else if (!string.IsNullOrEmpty(idLivraison))
                {
                    mclass.fnGet(Guid.Parse(idLivraison));
                    ViewData["FromWeighing"] = "YES";
                }
                else
                    throw new Exception("An error occured ! please retry");

                if (mclass.ID == Guid.Empty)
                    return HttpNotFound();

                int _StatutLivraison = new Livraison().fnGetWeightStatus(mclass.ID);
                ViewData["StatutLivraison"] = _StatutLivraison;
                viewModel._Livraison = mclass;

                if (_StatutLivraison == (int)Tms.Classes.Business.StatutLivraison.Val.SecondWeight)
                    viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
                else
                    viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

                Parametres mParam = new Parametres(0);

                viewModel._Campagne = mParam.Campagne;
                //viewModel._Parametres = new Parametres(0);
                viewModel._Recolte = mParam.Recolte;

                viewModel._Exportateur = mParam.Exportateur;

                return new Ext.Net.MVC.PartialViewResult { ViewName = "Livraison_DetailSmall", Model = viewModel, ViewData = ViewData };
            }
            catch (Exception ex)
            {
                return JavaScript(X.MessageBox.Alert("Error", ex.Message).ToScript());
            }
        }


        public ActionResult OnConsult(string ItemSelected)
        {
            LivraisonViewModel viewModel = new LivraisonViewModel();

            try
            {
                //Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                //mViewport.Mask();

                Livraison mclass = JSON.Deserialize<Livraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                mclass.fnGet(mclass.ID);

                if (mclass.ID == Guid.Empty)
                    return HttpNotFound();

                viewModel._Livraison = mclass;
                int _StatutLivraison = new Livraison().fnGetWeightStatus(mclass.ID);
                ViewData["StatutLivraison"] = _StatutLivraison;
                viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
                //viewModel._Parametres = new Parametres(0);
                return new Ext.Net.MVC.PartialViewResult { ViewName = "Livraison_Detail", Model = viewModel, ViewData = ViewData };
            }
            catch (Exception ex)
            {
                return JavaScript(X.MessageBox.Alert("Error", ex.Message).ToScript());
            }
        }
        
        public ActionResult OnActivateDeactivate(string ItemSelected)
        {

            try
            {
                Livraison mClass = JSON.Deserialize<Livraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Guid mId = mClass.ID;

                bool result = mClass.fnGet(mId);

                bool deActivate = mClass.Desactive;

                if (!result)
                    throw new Exception("OnActivateDeactivate : Delivery loading failed.");

                if (mClass.Desactive)
                    result = mClass.fnActivate();
                else
                    result = mClass.fnCancel();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Delivery Approval failed.");

                Store mstore = X.GetCmp<Store>("storeDeliveriesList");

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
                    mclass = JSON.Deserialize<Livraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
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
                Livraison mClass = new Livraison();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("hiddenID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Delivery load failed.");
                }

                mClass = MapFormToObject(mClass);
                
                bool result = mClass.fnUpdate();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeDeliveriesList");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);

                        X.GetCmp<RowSelectionModel>("rowSelectionListe").Select(0);
                    }
                    else
                    {
                        
                        if (FromWeighing == "NO")
                        {
                            mProxy = mStore.GetById(mClass.ID);
                            mProxy.BeginEdit();

                            mProxy.Set(mClass);

                            mProxy.Commit();

                            mProxy.EndEdit();
                        }
                        else
                        {
                            CloseDetailWindow();
                        }                 
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
        public ActionResult SubmitFormMethodSmall()
        {
            try
            {
                Livraison mClass = new Livraison();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("hiddenID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Delivery load failed.");
                }

                mClass = MapFormToObject(mClass);

                bool result = mClass.fnUpdateSmall();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeDeliveriesList");

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
                    X.GetCmp<Window>("Livraison_DetailSmall").Close();
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
                
                mClass.UtilisateurModification = (string)Session["userName"];
                mClass.UtilisateurCreation = (string)Session["userName"];

                bool result = mClass.fnUpdateSupplier();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeDeliveriesList");

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


        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne, string ItemLivraisonType, string ItemFournisseur, string ItemStartDate, string ItemEndDate, string ItemExportateur, string ItemSite)
        {            
            
            Parametres para = new Parametres();
            string defaultCrop = para.Campagne;

            string Crop = defaultCrop;
            if (!String.IsNullOrEmpty(ItemCampagne))
                Crop = ItemCampagne;

            int LivraisonTypeID = GetCritriaValue(ItemLivraisonType);

            string SupplierID = "-1";
            if (!String.IsNullOrEmpty(ItemFournisseur))
                SupplierID = ItemFournisseur;

            int siteID = GetCritriaValue(ItemSite);          

            DateTimeFormatInfo ukDtfi = new CultureInfo("fr-FR", false).DateTimeFormat;
            DateTime StartDate = DateTime.Now.AddDays(-1);
            if (!String.IsNullOrEmpty(ItemStartDate) && !ItemStartDate.Contains("1/1/0001"))
                StartDate = DateTime.Parse(ItemStartDate);

            DateTime EndDate = DateTime.Now;
            if (!String.IsNullOrEmpty(ItemEndDate) && !ItemEndDate.Contains("1/1/0001"))
                EndDate = DateTime.Parse(ItemEndDate);

            int ExportateurID = -1;
            if (!String.IsNullOrEmpty(ItemExportateur))
                ExportateurID = int.Parse(ItemExportateur);

            var mListe = (new Livraison()).fnSelect(Crop, LivraisonTypeID, SupplierID, StartDate, EndDate,ExportateurID,siteID);

            // Paging
            //int start = parameters.Start;

            //int limit = parameters.Limit;

            //if ((start + limit) > mListe.Count)
            //{
            //    limit = mListe.Count - start;
            //}

            //List<DataPersist> rangeListe = (start < 0 || limit < 0) ? mListe : mListe.GetRange(start, limit);

            //return this.Store(new Paging<DataPersist>(rangeListe, mListe.Count));

            //RedirectToAction("UpdateSummary", "Livraison", mListe);
            //UpdateSummary(mListe);
            //Session["mListeLivraion"] = mListe;
            string filterHeaders = this.Request.Params["filterheader"];
            return this.Store(mListe);
            //return this.Store(GridStorePaging.SetRangePlants(parameters, mListe, filterHeaders));
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

        public ActionResult OnRefresh(string ItemCampagne, string ItemLivraisonType, string ItemFournisseur, string ItemStartDate, string ItemEndDate, string ItemExportateur, string ItemSite)
        {            
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemStartDate, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemEndDate, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeDeliveriesList");

                //mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemCampagne"   ,ItemCampagne),
                                new Ext.Net.Parameter("ItemLivraisonType"     ,ItemLivraisonType),
                                new Ext.Net.Parameter("ItemFournisseur"     ,ItemFournisseur),
                                new Ext.Net.Parameter("ItemStartDate"           ,ItemStartDate),
                                new Ext.Net.Parameter("ItemEndDate"           ,ItemEndDate),
                                new Ext.Net.Parameter("ItemExportateur"           ,ItemExportateur),
                                new Ext.Net.Parameter("ItemSite"           ,ItemSite)
                            });

                // collapse criterias areas
                X.GetCmp<FormPanel>("CriteriaPanel").Collapse(Direction.Top, false);
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

            title += ", Du " + X.GetCmp<DateField>("dtpStartDateFD").RawText.ToString() + " au " + X.GetCmp<DateField>("dtpEndDateFD").RawText.ToString();

            X.GetCmp<FormPanel>("CriteriaPanelFD").Title = title;

            // collapse criterias areas
            X.GetCmp<FormPanel>("CriteriaPanelFD").Collapse(Direction.Top, false);

            return this.Direct();
        }

        public ActionResult LoadListOfAvailableDeliveries(string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd)
        {
            int Fournisseur = ItemFournisseur == "" ? -1 : int.Parse(ItemFournisseur);
            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            var mListe = (new Livraison()).fnSelectForFinalizing(Fournisseur, StartDate, EndDate);
            return this.Store(mListe);
        }


        public ActionResult OnFilter()
        {
            X.GetCmp<FormPanel>("CriteriaPanel").ToggleCollapse();
            return this.Direct();
        }


        public ActionResult OnDisplayAcceptedDeliveries()
        {

            ViewData["Titre"] = "Livraisons acceptées";
            ViewData["actionToDo"] = "OnPrintAcceptedDeliveries";
            Fournisseur mClass = new Fournisseur();
            mClass.ID = -1;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmDeliveryAccepted_Criteria", ViewData = ViewData, Model = mClass };
            
        }

        public ActionResult OnDisplayDeliveriesList()
        {
            string UserName = (string)Session["userName"];

            Site mSiteParDefaut = new Site();
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            Parametres mParam = new Parametres(0);

            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;
            if (mParam.Site == mSiteParDefaut.ID) ViewData["UrlSite"] = "LoadSiteAll";
            else ViewData["UrlSite"] = "LoadSiteByAccess";

            ViewData["Titre"] = "Liste des livraisons";
            ViewData["actionToDo"] = "OnPrintDeliveriesList";
            ViewData["ControllerName"] = "Livraison";
            //Fournisseur mClass = new Fournisseur();
            //mClass.ID = -1;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmDeliveryList", ViewData = ViewData };

        }

        public ActionResult OnPrintAcceptedDeliveries(string cropYear,  string exportateur , string deliveryType, string supplier, string startDate, string endDate, string supplierName, string deliveryTypeDesignation, string exportateurNom)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                Session["paramCropYear"] = cropYear;
                Session["paramDeliveryType"] = Int32.Parse(deliveryType);
                Session["paramDeliveryTypeText"] = deliveryTypeDesignation;

                Session["paramExportateur"] = Int32.Parse(exportateur);
                Session["paramExportateurText"] = exportateurNom;

                Session["paramSupplier"] = Int32.Parse(supplier);
                Session["paramSupplierText"] = supplierName;

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Livraison/ViewAcceptedDeliveriesReport', this, 'Livraisons acceptées',''),App.frmAcceptedDelivery.doClose()", Guid.NewGuid(), BaseUrl));
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


        public ActionResult ViewAcceptedDeliveriesReport()
        {
            XtraReport report = null;

            // TODO :  sur le base du type de rapport : detaillé ou cumulé 
            // initialiser l'objet report avec l'object idoine

            report = new rptPhysicalAnalysisDeliveriesReport() as XtraReport;

            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramCampagne"].Value = Session["paramCropYear"];

            report.Parameters["paramExporterID"].Value = Session["paramExportateur"];

            report.Parameters["paramExporterName"].Value = Session["paramExportateurText"];

            report.Parameters["paramTypeLivraison"].Value = Session["paramDeliveryType"];

            report.Parameters["paramFournisseur"].Value = Session["paramSupplier"];

            report.Parameters["paramTypeLivraisonText"].Value = Session["paramDeliveryTypeText"];

            report.Parameters["paramFournisseurText"].Value = Session["paramSupplierText"];

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];

            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];
        
            ViewData["Report"] = report;

            return View("");
        }


        public ActionResult OnPrintFarmerCard(string id_farmer)
        {
            try
            {
                if (!String.IsNullOrEmpty(id_farmer))
                {
                    //Declarations
                    Reporting mReportingClass = new Reporting();

                    mReportingClass.httpCtx = HttpContext;

                    mReportingClass.Report = new rptFarmerCard();

                    mReportingClass.Parameters.Add("@ID", id_farmer);

                    if (mReportingClass.GeneratePDF())
                    {
                        Session["report_path"] = mReportingClass.FilePath;

                        Session["report_id"] = id_farmer;
                    } 
                }

            }
            catch (Exception ex)
            {
                X.Msg.Alert("Erreur", "FournisseurController : OnPrintFarmerCard : " + ex.Message).Show();
            }


            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'farmerCard{0}', 'http://localhost:5179/ReportViewer/OnDisplayReport', this, 'Farmer card')", id_farmer));
            //return JavaScript("alert('Hello world')");
        }
        
        public  ActionResult OnDownloadFarmerCard(string id_farmer)
        {
            if (!String.IsNullOrEmpty(id_farmer))
            {
                Reporting mReportingClass = new Reporting();

                mReportingClass.httpCtx = HttpContext;

                mReportingClass.Report = new rptFarmerCard();

                mReportingClass.Parameters.Add("@ID", id_farmer);

                Response.Buffer = false;

                Response.ClearContent();

                Response.ClearHeaders();

                try
                {

                    Stream stream = mReportingClass.ExportToPDFStream();

                    stream.Seek(0, SeekOrigin.Begin);

                    return File(stream, "application/pdf", "FarmerCard.pdf");
                }
                catch (Exception ex)
                {
                    throw;
                }





                //ReportDocument rd = new rptFarmerCard();
                //MenuControls oWebMenuControls = new MenuControls();
                //oWebMenuControls.AssignConnection(rd);
                //int farmerID = int.Parse(id_farmer);

                //rd.SetParameterValue("@ID", farmerID);

                //Response.Buffer = false;
                //Response.ClearContent();
                //Response.ClearHeaders();
                //try
                //{

                //    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

                //    stream.Seek(0, SeekOrigin.Begin);

                //    return File(stream, "application/pdf", "FarmerCard.pdf");
                //}
                //catch (Exception ex)
                //{
                //    throw;
                //}
            }

            string scriptJs = X.Msg.Alert("Erreur", "FournisseurController : OnPrintFarmerCard : Please select an element in the list." ).ToScript();

            return JavaScript(scriptJs);
        }
              

        #region "Methods"
        private void CloseDetailWindow()
        {
            X.GetCmp<Window>("frmLivraison").Hide();           
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

        private Livraison MapFormToObject(Livraison mClass)
        {
            int iConverted;
            bool result;

            mClass.Site = new Tms.Classes.Shared.Site();
            mClass.Site.ID = int.Parse(GetFormValue("cmbDetSite"));
            mClass.Site.Nom = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text.ToString();
            
            mClass.Provenance = new Tms.Classes.Shared.Provenance();
            mClass.Provenance.ID = int.Parse(GetFormValue("cmbDetOrigin"));
            mClass.Provenance.Nom = X.GetCmp<ComboBox>("cmbDetOrigin").SelectedItem.Text.ToString();

            mClass.Destination = new Tms.Classes.Shared.Destination();
            mClass.Destination.ID = int.Parse(GetFormValue("cmbDetDestination"));
            mClass.Destination.Nom = X.GetCmp<ComboBox>("cmbDetDestination").SelectedItem.Text.ToString();

            mClass.Exportateur = new Tms.Classes.Shared.Exportateur();
            mClass.Exportateur.ID = int.Parse(GetFormValue("cmbDetExporter"));
            mClass.Exportateur.Nom = X.GetCmp<ComboBox>("cmbDetExporter").SelectedItem.Text.ToString();

            mClass.Campagne = new Tms.Classes.Shared.Campagne();           
            mClass.Campagne.Designation = X.GetCmp<ComboBox>("cmbDetCrop").SelectedItem.Text.ToString();
            
            mClass.SacType = new Tms.Classes.Shared.SacType();
            mClass.SacType.ID = int.Parse(GetFormValue("cmbDetPackaging"));
            mClass.SacType.Designation = X.GetCmp<ComboBox>("cmbDetPackaging").SelectedItem.Text.ToString();

            mClass.Recolte = new Tms.Classes.Shared.Recolte();
            mClass.Recolte.ID = int.Parse(GetFormValue("cmbDetCropQuality"));
            mClass.Recolte.Designation = X.GetCmp<ComboBox>("cmbDetCropQuality").SelectedItem.Text.ToString();

            mClass.SacsDeclares = int.Parse(GetFormValue("txtNbrOfBags"));
            
            mClass.DateLivraison = DateTime.Parse(GetFormValue("dtfDeliveryDate")+" "+ GetFormValue("tmfDelivery"));

            //mClass.PoidsDeclare = decimal.Parse("1500,58");
            mClass.PoidsDeclare = Convert.ToDecimal(GetFormValue("txtEstimatedTonnage"));

            mClass.LivraisonType = new Tms.Classes.Shared.LivraisonType();
            mClass.LivraisonType.ID = int.Parse(GetFormValue("cmbDetDeliveryType"));
            mClass.LivraisonType.Designation = X.GetCmp<ComboBox>("cmbDetDeliveryType").SelectedItem.Text.ToString();
            
            mClass.Transitaire = null;            
            result  = int.TryParse(GetFormValue("cmbDetTransitaire"),out iConverted);
            if (result)
            {
                mClass.Transitaire = new Tms.Classes.Shared.Transitaire();
                mClass.Transitaire.ID = iConverted;
                mClass.Transitaire.Nom = X.GetCmp<ComboBox>("cmbDetTransitaire").SelectedItem.Text.ToString(); 
            }
            
            mClass.Fournisseur = new Tms.Classes.Business.Fournisseur();
            mClass.Fournisseur.ID = int.Parse(GetFormValue("cmbDetFournisseur"));
            mClass.Fournisseur.Nom = X.GetCmp<ComboBox>("cmbDetFournisseur").SelectedItem.Text.ToString();

            mClass.NumConteneur = GetFormValue("txtContainerNumber");
            mClass.Immatriculation = GetFormValue("txtTruckID");
            mClass.NumPlomb = GetFormValue("txtSealNumber");
            mClass.Tracteur = GetFormValue("txtTractorID");
            mClass.NumOT = GetFormValue("txtShipmentNumber");
            mClass.Chauffeur = GetFormValue("txtDriverName");
            mClass.Numero = GetFormValue("txtDeliveryNumber");
            mClass.NumeroExterne = GetFormValue("txtExternalWayBill");
            
            mClass.Certification = null;
            result = int.TryParse(GetFormValue("cmbDetCertification"), out iConverted);
            if (result)
            {
                mClass.Certification = new Tms.Classes.Shared.Certification();
                mClass.Certification.ID = int.Parse(GetFormValue("cmbDetCertification"));
                mClass.Certification.Designation = X.GetCmp<ComboBox>("cmbDetCertification").SelectedItem.Text.ToString(); 
            }

            mClass.Transporteur = null;
            result = int.TryParse(GetFormValue("cmbDetTransporter"), out iConverted);
            if (result)
            {
                mClass.Transporteur = new Tms.Classes.Shared.Transporteur();
                mClass.Transporteur.ID = int.Parse(GetFormValue("cmbDetTransporter"));
                mClass.Transporteur.Nom = X.GetCmp<ComboBox>("cmbDetTransporter").SelectedItem.Text.ToString();
            }

            mClass.CentreAchat = null;
            result = int.TryParse(GetFormValue("cmbDetAgence"), out iConverted);
            if (result)
            {
                mClass.CentreAchat = new Tms.Classes.Shared.Site();
                mClass.CentreAchat.ID = int.Parse(GetFormValue("cmbDetAgence"));
                mClass.CentreAchat.Nom = X.GetCmp<ComboBox>("cmbDetAgence").SelectedItem.Text.ToString();
            }

            mClass.NumLot = GetFormValue("txtLotNumber");

            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }

        private Livraison MapFormToObjectForType(Livraison mClass)
        {
            try
            {
                mClass.UtilisateurModification = (string)Session["userName"];

                LivraisonType mOld = new LivraisonType();
                mOld.ID = int.Parse(X.GetCmp<ComboBox>("OldTypeID").Text);
                mOld.Designation = X.GetCmp<ComboBox>("OldTypeID").SelectedItem.Text.ToString();
                mClass.AncienType = mOld;

                LivraisonType mNew = new LivraisonType();
                mNew.ID = int.Parse(X.GetCmp<ComboBox>("NewTypeID").Text);
                mNew.Designation = X.GetCmp<ComboBox>("NewTypeID").SelectedItem.Text.ToString();
                mClass.NouveauType = mNew;

                mClass.RaisonReclassement = X.GetCmp<TextField>("txtRaison").Text;

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bon De Livraison : MapFormToObject",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

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

        public ActionResult LoadListAvailableTransfert(int ItemFournisseur, int siteID = -1)
        {
            List<DataPersist> myList = new List<DataPersist>();

            myList = (new Livraison()).fnSelectTransferFeveForPurchseDeliveries(ItemFournisseur, siteID);
            
            //List<TransfertFeves> newList = myList.Cast<TransfertFeves>().ToList();
            
            //if (LotsExistant != null)
            //{
            //    newList = newList.Where(l => !LotsExistant.Contains(l.Numero)).ToList();
            //    return this.Store(newList);
            //}
            //else
            return this.Store(myList);
        }

        public ActionResult OnRefreshForAvaibleTransfertFeve(string ItemFournisseur, string siteID)
        {
            try
            {
                //List<TransfertFeves> mLot = JSON.Deserialize<List<TransfertFeves>>(rowsInList, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                
                //if (mLot.Count > 0)
                //{
                //    LotsExistant = new HashSet<string>(mLot.Select(l => l.Numero).ToList());
                //}

                Store mstore = X.GetCmp<Store>("storeListAvailableTransferFeves");

                //mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur", ItemFournisseur),
                                    new Ext.Net.Parameter("siteID", siteID)
                                });



                // collapse criterias areas
                //X.GetCmp<FormPanel>("EmbarquementCriteriaPanel").Collapse(Direction.Top, false);
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

        public ActionResult SubmitSelectTransfertFeves(string ItemSelected)
        {
            try
            {
                if (!string.IsNullOrEmpty(ItemSelected))
                    X.GetCmp<TextField>("txtExternalWayBill").SetValue(ItemSelected);

                X.GetCmp<Window>("Select_Transfert").Close();

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

        public ActionResult OnAddExterWaybill(string ItemCampagne, string ItemFournisseur, string ItemSite)
        {
            ViewData["Campagne"] = ItemCampagne;
            ViewData["Fournisseur"] = ItemFournisseur;
            ViewData["Site"] = ItemSite;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Select_Transfert", ViewData = ViewData };


        }


        #endregion

        public ActionResult OnPrintDeliveriesList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetStartDate").RawText.ToString(), "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetEndDate").RawText.ToString(), "d", CultureInfo.CurrentUICulture);

                Session["paramSite"] = int.Parse(GetFormValue("cmbDetSite"));
                Session["paramSiteText"] = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text;

                Session["paramCropYear"] = GetFormValue("cmbDetCrop");

                Session["paramExportateur"] = int.Parse(GetFormValue("cmbDetExporter"));
                Session["paramExportateurNom"] = X.GetCmp<ComboBox>("cmbDetExporter").SelectedItem.Text;

                Session["paramDeliveryType"] = int.Parse(GetFormValue("cmbDetLivraisonType"));
                Session["paramDeliveryTypeText"] = X.GetCmp<ComboBox>("cmbDetLivraisonType").SelectedItem.Text;


                Session["paramSupplier"] = int.Parse(GetFormValue("cmbDetSupplier"));
                Session["paramSupplierText"] = X.GetCmp<ComboBox>("cmbDetSupplier").SelectedItem.Text;

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

            report.Parameters["paramSite"].Value = Session["paramSite"];
            report.Parameters["paramSiteText"].Value = Session["paramSiteText"];

            report.Parameters["paramFournisseur"].Value = Session["paramSupplier"];
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

        public ActionResult UpdateDeliveryType()
        {
            try
            {
                Livraison mClass = new Livraison();

                mClass.IsNew = false;

                mClass.fnGet(Guid.Parse(GetFormValue("hiddenLivraisonID")));

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("UpdateDeliveryType : Financing load failed.");

                mClass = MapFormToObjectForType(mClass);

                bool result = mClass.fnChangerType();

                if (result)
                {

                    X.GetCmp<Window>("Livraison_ChangerType").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Livraison : UpdateDeliveryType",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnUpdateType(string ItemSelected)
        {
            Livraison mclass = JSON.Deserialize<Livraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            ViewData["LivraisonID"] = mclass.ID;
            LivraisonViewModel mModel = new LivraisonViewModel();
            Livraison mlivraison = new Livraison();

            bool result = mlivraison.fnGet(mclass.ID);
            mModel._Livraison = new Livraison();
            mModel._Livraison = mlivraison;
            mModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Livraison_ChangerType", Model = mModel };
        }

    }
}