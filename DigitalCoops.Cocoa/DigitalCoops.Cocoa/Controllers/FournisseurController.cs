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
    public class FournisseurController : BaseController
    {

        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";

        public FournisseurController()
        {
            //string cultureName = "en";
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
        }

        // GET: Fournisseur
        public ActionResult Index()
        {
            int SupplierGroupID = GetCritriaValue(string.Empty);

            int SupplierTypeID = GetCritriaValue(string.Empty);

            int CertifiedID = GetCritriaValue(string.Empty);

            int Approuvé = GetCritriaValue(string.Empty);

            int StatusID = GetCritriaValue(string.Empty);

            int AgenceID = GetCritriaValue(string.Empty);

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            ViewBag.SiteParDefaut = mSiteParDefaut.ID;
            //var mListe = (new Fournisseur()).fnSelect(SupplierGroupID, SupplierTypeID, CertifiedID, StatusID, Approuvé, AgenceID);

            // filtering
            //mListe = Filtering(parameters, mListe);

            // Paging

            ////int start = parameters.Start;

            ////int limit = parameters.Limit;

            //if ((start + limit) > mListe.Count)
            //{
            //    limit = mListe.Count - start;
            //}

            //List<DataPersist> rangePlants = (start < 0 || limit < 0) ? mListe : mListe.GetRange(start, limit);

            //return this.Store(new Paging<DataPersist>(rangePlants, mListe.Count));
            #region Set Function's Access
            //string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{A9F1208E-1E1C-4913-AE82-B40ABE55AC36}", UserName);
            if (HasAccess.fnGetUserAccessStatus("{31A12771-136D-4C44-9A36-F2898F0FECEC}", UserName) == false)
                X.GetCmp<Button>("btnNew").Disable();
            else
                X.GetCmp<Button>("btnNew").Enable();

            if (HasAccess.fnGetUserAccessStatus("{87CDDE66-D7B3-4B76-9D3D-1AC97EF858E4}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportToExcel").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportToExcel").Enable();

            if (HasAccess.fnGetUserAccessStatus("{3AFC9E11-04D1-400B-94F1-237BE653DC7A}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintSupplierList").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintSupplierList").Enable();

            X.GetCmp<Hidden>("FrshiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{31A12771-136D-4C44-9A36-F2898F0FECEC}", UserName));
            X.GetCmp<Hidden>("FrshiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{0A254F16-C004-4F83-80AD-6705CA115AC4}", UserName));
            X.GetCmp<Hidden>("FrshiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{995F19E2-56B1-40A4-B736-D71EE638D36A}", UserName));
            X.GetCmp<Hidden>("FrshiddenPermActiver").SetValue(HasAccess.fnGetUserAccessStatus("{7168DC48-60AB-4DCF-A582-FA19E18FEE11}", UserName));
            X.GetCmp<Hidden>("FrshiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{87CDDE66-D7B3-4B76-9D3D-1AC97EF858E4}", UserName));
            X.GetCmp<Hidden>("FrshiddenPermApprove").SetValue(HasAccess.fnGetUserAccessStatus("{2F2A750E-58D9-4251-BE11-235118E4F59F}", UserName));
            X.GetCmp<Hidden>("FrshiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{A9F1208E-1E1C-4913-AE82-B40ABE55AC36}", UserName));
            X.GetCmp<Hidden>("FrshiddenPermPrintDashBoard").SetValue(HasAccess.fnGetUserAccessStatus("{25A65E25-C10A-43D7-A506-25BC06E6DC83}", UserName));
            X.GetCmp<Hidden>("FrshiddenPermPrintPurchaseTransaction").SetValue(HasAccess.fnGetUserAccessStatus("{8EA72863-5F16-406A-AE0F-F45BF5BB8ADC}", UserName));
            X.GetCmp<Hidden>("FrshiddenPermPrintFinancialStatus").SetValue(HasAccess.fnGetUserAccessStatus("{01B99017-F1DA-4A0F-AD5A-0114D6F3706F}", UserName));
            X.GetCmp<Hidden>("FrshiddenPermPrintFinancialBalance").SetValue(HasAccess.fnGetUserAccessStatus("{470EE666-9B5F-4210-90D3-3A7D4FC03220}", UserName));
            X.GetCmp<Hidden>("FrshiddenPermPrintFinancialExecution").SetValue(HasAccess.fnGetUserAccessStatus("{34E61971-7D12-4BAB-BB27-F06DD4C1BE5B}", UserName));
            X.GetCmp<Hidden>("FrshiddenPermPrintFinancialExposure").SetValue(HasAccess.fnGetUserAccessStatus("{F9A66184-F51F-4CB1-BD50-B5908B196E3E}", UserName));
            X.GetCmp<Hidden>("FrshiddenPermPrintForwardContractsBalance").SetValue(HasAccess.fnGetUserAccessStatus("{4500262D-6D4B-4627-A674-B684536F724B}", UserName));
            X.GetCmp<Hidden>("FrshiddenPermPrintForwardContractsExecution").SetValue(HasAccess.fnGetUserAccessStatus("{F8F509E2-6158-447A-BD22-72F41A645490}", UserName));
            X.GetCmp<Hidden>("FrshiddenPermPrintAcceptedDeliveries").SetValue(HasAccess.fnGetUserAccessStatus("{72F19A0F-1769-4446-81C8-07C58F27D914}", UserName));
            X.GetCmp<Hidden>("FrshiddenPermPrintRejectedDeliveries").SetValue(HasAccess.fnGetUserAccessStatus("{8B9B501E-B613-4908-92F8-0BAA6BD1E0B8}", UserName));
            X.GetCmp<Hidden>("FrshiddenPermPrintHystoryOfInvoice").SetValue(HasAccess.fnGetUserAccessStatus("{EF844457-E69B-4CF9-9FEF-E8D2FC89AF9B}", UserName));
            X.GetCmp<Hidden>("FrshiddenPermPrintHystoryOfPayement").SetValue(HasAccess.fnGetUserAccessStatus("{E4C9DE87-C7CE-41EE-A2C5-F2039FECF9C7}", UserName));

            X.GetCmp<Hidden>("FrshiddenPermPrintSupplierList").SetValue(HasAccess.fnGetUserAccessStatus("{3AFC9E11-04D1-400B-94F1-237BE653DC7A}", UserName));
            X.GetCmp<Hidden>("FrshiddenPermAddSite").SetValue(HasAccess.fnGetUserAccessStatus("{20542B05-66FA-41BC-90B4-A15377B333F4}", UserName));
            #endregion
            return View();
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

        public ActionResult LoadFournisseurActiveAll()
        {
            int groupSupplierID = -1;
            int supplierID = -1;
            int certificationID = -1;
            int isDisabled = 0;           

            List<DataPersist> myList = new Fournisseur().fnSelect(groupSupplierID, supplierID, certificationID, isDisabled);
            Fournisseur mClass = new Fournisseur();

            mClass.ID = -1;
            mClass.Nom = "{Tous}";
            myList.Insert(0, mClass);
            mClass = myList[0] as Fournisseur;

            return this.Store(myList);
        }
        
        public ActionResult LoadFournisseursApproved(int SiteID = -1)
        {
            int groupSupplierID = -1;
            int supplierID = -1;
            int certificationID = -1;
            int isDisabled = 0;
            int isApproved = 1;

            List<DataPersist> myList = new Fournisseur().fnSelect(groupSupplierID, supplierID, certificationID, isDisabled, isApproved,SiteID);
            Fournisseur mClass = new Fournisseur();

            mClass.ID = -1;
            mClass.Nom = "{Tous}";
            myList.Insert(0, mClass);
            mClass = myList[0] as Fournisseur;

            return this.Store(myList);
        }

        public ActionResult LoadFournisseursApprovedExtend(int SiteID = -1)
        {
            int groupSupplierID = -1;
            int supplierID = -1;
            int certificationID = -1;
            int isDisabled = 0;
            int isApproved = 1;

            List<DataPersist> myList = new Fournisseur().fnSelect(groupSupplierID, supplierID, certificationID, isDisabled, isApproved, SiteID,1);
            Fournisseur mClass = new Fournisseur();

            mClass.ID = -1;
            mClass.Nom = "{Tous}";
            myList.Insert(0, mClass);
            mClass = myList[0] as Fournisseur;

            return this.Store(myList);
        }

        public ActionResult LoadFournisseur()
        {
            int SupplierGroupID = -1;
            int SupplierTypeID = -1;
            int CertifiedID = -1;
            int IsDisabled = 0;

            List<DataPersist> myList = new Fournisseur().fnSelect( SupplierGroupID,   SupplierTypeID,  CertifiedID,  IsDisabled);
            Fournisseur mClass = new Fournisseur();
            
            if(myList.Count > 0)
              mClass = myList[0] as Fournisseur;

            return this.Store(myList);
        }
        
        public ActionResult LoadFournisseurBySupplierGroup(int? TypeLivraisonID, int SiteID = -1, int? Extend = 0)
        {
            List<DataPersist> myList = new List<DataPersist>();           

            LivraisonType mTypeLivraison = new LivraisonType();

            if (!TypeLivraisonID.HasValue)
                return this.Store(myList);

            int SupplierGroupID;
            int supplierID = -1;
            int ChefAgenceTypeFrs = -1;
            
            Parametres mParam = new Parametres(0);
            ChefAgenceTypeFrs = mParam.ChefAgenceType;
            
            if (TypeLivraisonID.Value != -1)
            {
                bool result = mTypeLivraison.fnGet(TypeLivraisonID);

                if (!result || (mTypeLivraison.ID == 0))
                    throw new Exception(this.GetType().FullName + " : LoadFournisseurBySupplierGroup : failed load TypeLivraison.");

                if (mTypeLivraison.SaisieNumTransfert)
                {
                    SupplierGroupID = -1;
                    supplierID = ChefAgenceTypeFrs;
                }
                else
                    SupplierGroupID = mTypeLivraison.FournisseurGroupe.ID;
            }
            else
            {
                SupplierGroupID = -1;
            }

            int certificationID = -1;
            int isDisabled = 0;
            int isApproved = 1;

            myList = new Fournisseur().fnSelect(SupplierGroupID, supplierID, certificationID, isDisabled, isApproved,SiteID,(int)Extend);
            Fournisseur mClass = new Fournisseur();                           

            return this.Store(myList);
        }

        public ActionResult LoadFournisseurBySupplierGroupExtend(int? TypeLivraisonID, int SiteID = -1)
        {
            List<DataPersist> myList = new List<DataPersist>();

            LivraisonType mTypeLivraison = new LivraisonType();

            if (!TypeLivraisonID.HasValue)
                return this.Store(myList);

            int SupplierGroupID;
            int supplierID = -1;
            int ChefAgenceTypeFrs = -1;

            Parametres mParam = new Parametres(0);
            ChefAgenceTypeFrs = mParam.ChefAgenceType;

            if (TypeLivraisonID.Value != -1)
            {
                bool result = mTypeLivraison.fnGet(TypeLivraisonID);

                if (!result || (mTypeLivraison.ID == 0))
                    throw new Exception(this.GetType().FullName + " : LoadFournisseurBySupplierGroup : failed load TypeLivraison.");

                if (mTypeLivraison.SaisieNumTransfert)
                {
                    SupplierGroupID = -1;
                    supplierID = ChefAgenceTypeFrs;
                }                    
                else
                    SupplierGroupID = mTypeLivraison.FournisseurGroupe.ID;
            }
            else
            {
                SupplierGroupID = -1;
            }

            int certificationID = -1;
            int isDisabled = 0;
            int isApproved = 1;

            myList = new Fournisseur().fnSelect(SupplierGroupID, supplierID, certificationID, isDisabled, isApproved, SiteID,1);
            Fournisseur mClass = new Fournisseur();
            //mClass.ID = -1;
            //mClass.Nom = "{Tous}";
            //myList.Insert(0, mClass);

            //if (myList.Count > 0)
            //    mClass = myList[0] as Fournisseur; 


            return this.Store(myList);
        }


        public ActionResult LoadAllFournisseurBySupplierGroup(int? TypeLivraisonID, int SiteID = -1, int? Extend = 0)
        {
            List<DataPersist> myList = new List<DataPersist>();



            LivraisonType mTypeLivraison = new LivraisonType();

            if (!TypeLivraisonID.HasValue)
                return this.Store(myList);

            int SupplierGroupID;

            if (TypeLivraisonID.Value != -1)
            {
                bool result = mTypeLivraison.fnGet(TypeLivraisonID);

                if (!result || (mTypeLivraison.ID == 0))
                    throw new Exception(this.GetType().FullName + " : LoadFournisseurBySupplierGroup : failed load TypeLivraison.");

                SupplierGroupID = mTypeLivraison.FournisseurGroupe.ID;
            }
            else
            {
                SupplierGroupID = -1;
            }

            int supplierID = -1;
            int certificationID = -1;
            int isDisabled = 0;
            int isApproved = 1;

            myList = new Fournisseur().fnSelect(SupplierGroupID, supplierID, certificationID, isDisabled, isApproved, SiteID, (int)Extend);
            Fournisseur mClass = new Fournisseur();
            mClass.ID = -1;
            mClass.Nom = "{Tous}";
            myList.Insert(0, mClass);

            if (myList.Count > 0)
                mClass = myList[0] as Fournisseur;


            return this.Store(myList);
        }

        public ActionResult LoadCocoaBeansSuppliers()
        {
            int SupplierGroupID = 1;
            int SupplierTypeID = -1;
            int CertifiedID = -1;
            int IsDisabled = 0;
            int IsApproved = 1;

            List<DataPersist> myList = new Fournisseur().fnSelect(SupplierGroupID, SupplierTypeID, CertifiedID, IsDisabled, IsApproved);
            Fournisseur mClass = new Fournisseur();

            if (myList.Count > 0)
                mClass = myList[0] as Fournisseur;

            return this.Store(myList);
        }

        public ActionResult LoadAllCocoaBeansSuppliers(int? siteID, int? Extend = 0)
        {
            List<DataPersist> myList = new List<DataPersist>();
            if (!siteID.HasValue)
                return this.Store(myList);

            int SupplierGroupID = 1;
            int SupplierTypeID = -1;
            int CertifiedID = -1;
            int IsDisabled = 0;
            int IsApproved = 1;

            myList = new Fournisseur().fnSelect(SupplierGroupID, SupplierTypeID, CertifiedID, IsDisabled, IsApproved,(int)siteID, (int)Extend);
            Fournisseur mClass = new Fournisseur();
            mClass.ID = -1;
            mClass.Nom = "{Tous}";
            myList.Insert(0, mClass);

            if (myList.Count > 0)
                mClass = myList[0] as Fournisseur;

            return this.Store(myList);
        }

        public ActionResult LoadAllSupplierBySite(int? siteID, int? Extend = 0)
        {
            List<DataPersist> myList = new List<DataPersist>();

            if (!siteID.HasValue)
                return this.Store(myList);

            int SupplierGroupID = 1;
            int SupplierTypeID = -1;
            int CertifiedID = -1;
            int IsDisabled = 0;
            int IsApproved = 1;

            myList = new Fournisseur().fnSelect(SupplierGroupID, SupplierTypeID, CertifiedID, IsDisabled, IsApproved, (int)siteID, (int)Extend);
            Fournisseur mClass = new Fournisseur();
            mClass.ID = -1;
            mClass.Nom = "{Tous}";
            myList.Insert(0, mClass);

            if (myList.Count > 0)
                mClass = myList[0] as Fournisseur;

            return this.Store(myList);
        }

        public ActionResult LoadSupplierBySite(int? siteID)
        {
            List<DataPersist> myList = new List<DataPersist>();

            if (!siteID.HasValue)
                return this.Store(myList);

            int SupplierGroupID = 1;
            int SupplierTypeID = -1;
            int CertifiedID = -1;
            int IsDisabled = 0;
            int IsApproved = 1;

            myList = new Fournisseur().fnSelect(SupplierGroupID, SupplierTypeID, CertifiedID, IsDisabled, IsApproved, (int)siteID);
            Fournisseur mClass = new Fournisseur();            

            if (myList.Count > 0)
                mClass = myList[0] as Fournisseur;

            return this.Store(myList);
        }


        public ActionResult LoadCocoaBeansSuppliersAndAgency(int? siteID)
        {
            string UserName = (string)Session["userName"];

            if (!siteID.HasValue)
                siteID = 1;

            //Fonction HasAccessFunction = new Fonction();
            //bool HasAccessAllSite = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            //if (!HasAccessAllSite)
            //{
            //    Site mSiteParDefaut = new Site();
            //    bool result = mSiteParDefaut.fnGetDefaultSite();
            //    siteID = mSiteParDefaut.ID;
            //}

            List<DataPersist> myList = new Fournisseur().fnSelectForFinancing((int)siteID);
            Fournisseur mClass = new Fournisseur();

            if (myList.Count > 0)
                mClass = myList[0] as Fournisseur;

            return this.Store(myList);
        }        


        public ActionResult OnCreate()
        {
            Site mSiteParDefaut = new Site();
            string UserName = (string)Session["userName"];
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            FournisseurViewModel mclass = new FournisseurViewModel();

            mclass._Fournisseur = new Fournisseur();
            mclass._Fournisseur.Agence = new Site();
            mclass._Fournisseur.Agence.ID = mSiteParDefaut.ID;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Fournisseur_Detail", Model = mclass };
        }

        public ActionResult OnConsult(string ItemSelected)
        {
            
            Fournisseur mclass = JSON.Deserialize<Fournisseur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            //MapObjectToForm(mclass);          

            FournisseurViewModel viewModel = new FournisseurViewModel();

            viewModel._Fournisseur = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Fournisseur_Detail", Model = viewModel };
        }
                
        public ActionResult OnApprove(string ItemSelected)
        {

            try
            {
                Fournisseur mClass = JSON.Deserialize<Fournisseur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                int mId = mClass.ID;

                bool result = mClass.fnGet(mId);

                if (!result)
                    throw new Exception("OnApprove : Supplier loading failed.");

                if (!mClass.IsApproved )
                {
                    mClass.UtilisateurModification = (string)Session["userName"];
                    result = mClass.fnApprove();

                    Store mstore = X.GetCmp<Store>("storeListe");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();

                    DeselectGridRows();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }
        
        public ActionResult OnEdit(string ItemSelected)
        {           

            Fournisseur mclass = JSON.Deserialize<Fournisseur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            //MapObjectToForm(mclass);          

            FournisseurViewModel viewModel = new FournisseurViewModel();

            viewModel._Fournisseur = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Fournisseur_Detail", Model = viewModel };
        }

        public ActionResult OnAddSite(string ItemSelected)
        {

            Fournisseur mclass = JSON.Deserialize<Fournisseur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            //MapObjectToForm(mclass);          

            FournisseurViewModel viewModel = new FournisseurViewModel();

            viewModel._Fournisseur = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Fournisseur_AddSite", Model = viewModel };
        }

        public ActionResult onSelectSite(string ItemSite = "")
        {
            ViewData["ItemSite"] = ItemSite;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFournisseur_Site", ViewData = ViewData };
        }

        public ActionResult SelectSiteToAdd(int? ItemFournisseur, int? ItemSite = null)
        {
            List<DataPersist> myList = new List<DataPersist>();

            if (!ItemSite.HasValue)
                return this.Store(myList);

            myList = new Site().fnSelectAvailableForFS((int)ItemSite);
            Site mClass = new Site();

            if (myList.Count > 0)
                mClass = myList[0] as Site;

            return this.Store(myList);
        }

        public ActionResult SubmitListeSites(string ItemSelected)
        {
            List<Site> mSites = JSON.Deserialize<List<Site>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            FournisseurAutreSites mClass = new FournisseurAutreSites();
            Store store = X.GetCmp<Store>("storeListeAutreSite");

            foreach (var item in mSites)
            {
                mClass.ID = Guid.NewGuid();
                mClass.Sites = new Site();
                mClass.Sites = item;
                mClass.Sites.IsNew = true;
                item.IsNew = true;
                //item.IsNewInList = true;
                store.Insert(0, mClass);
                X.GetCmp<RowSelectionModel>("rowSiteFS").Select(0);
            }

            X.GetCmp<Window>("FormFournisseur_Site").Close();
            return this.Direct();
        }

        [HttpPost]
        public ActionResult SubmitSite(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                Fournisseur mClass = new Fournisseur();
                FournisseurAutreSites mAutreSites = new FournisseurAutreSites();
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                mClass.IsNew = false;

                mClass.fnGet(int.Parse(GetFormValue("TxtFournisseurID")));

                if (mClass == null || mClass.ID == 0)
                    throw new Exception("SubmitFormMethod : Supplier load failed.");

                bool result = false;                              

                _db = mAutreSites.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                List<FournisseurAutreSites> ItemSites = JSON.Deserialize<List<FournisseurAutreSites>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                if (ItemSites.Count > 0)
                {
                    for (int i = 0; i < ItemSites.Count(); i++)
                    {
                        mAutreSites = new FournisseurAutreSites();
                        mAutreSites.Sites = new Site();
                        mAutreSites.Founrnisseur = new Fournisseur();

                        mAutreSites.SetDataSource(_db);

                        mAutreSites.Sites.ID = ItemSites[i].Sites.ID;
                        mAutreSites.Founrnisseur.ID = mClass.ID;
                        mAutreSites.IsNew = ItemSites[i].IsNew;
                        
                        mAutreSites.UtilisateurCreation = (string)Session["userName"];
                        mAutreSites.UtilisateurModification = (string)Session["userName"];
                        if (mAutreSites.IsNew)                        
                            result = mAutreSites.fnUpdate(mtran);
                        
                        if (!result)
                        {
                            _db.RollBackTransaction(mtran);
                            break;
                        }
                    }
                }

                if (result)          
                    _db.CommitTransaction(mtran);

                X.GetCmp<Window>("Fournisseur_AddSite").Close();
                
            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mtran);
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }


        //public ActionResult OnApprove(string ItemSelected)
        //{

        //    try
        //    {
        //        Fournisseur mClass = JSON.Deserialize<Fournisseur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

        //        int mId = mClass.ID;

        //        bool result = mClass.fnGet(mId);

        //        if (!result)
        //            throw new Exception("OnApprove : Supplier loading failed.");

        //        if (!mClass.IsApproved)
        //        {
        //            mClass.UtilisateurModification = (string)Session["userName"];
        //            result = mClass.fnApprove();

        //            Store mstore = X.GetCmp<Store>("storeListe");

        //            ModelProxy mProxy = mstore.GetById(mClass.ID);

        //            mProxy.BeginEdit();

        //            mProxy.Set(mClass);

        //            mProxy.Commit();

        //            mProxy.EndEdit();

        //            DeselectGridRows();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        X.MessageBox.Alert("Error", ex.Message).Show();
        //    }

        //    return this.Direct();
        //}

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {

            try
            {
                Fournisseur mClass = JSON.Deserialize<Fournisseur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                int mId = mClass.ID;

                bool result = mClass.fnGet(mId);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Supplier loading failed.");

                bool deActivate = mClass.Desactive;

                if (mClass.Desactive)
                    result = mClass.fnActivate();
                else
                    result = mClass.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Supplier Approval failed.");

                Store mstore = X.GetCmp<Store>("storeListe");

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
                     
        [HttpPost]
        public ActionResult SubmitFormMethod()
        {
            try
            {
                Fournisseur mClass = new Fournisseur();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(int.Parse(GetFormValue("TxtFournisseurID")));

                    if (mClass == null || mClass.ID == 0)
                        throw new Exception("SubmitFormMethod : Supplier load failed.");
                }

                mClass = MapFormToObject(mClass);
                
                bool result = mClass.fnUpdate();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListe");
                    GridPanel mGrid = X.GetCmp<GridPanel>("grpListe");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {                        
                        mStore.Insert(0,mClass);
                        //mGrid.Store.Insert(0,mStore);                                                  
                        X.GetCmp<RowSelectionModel>("rowSelectionListe").Select(0);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();

                        //X.GetCmp<RowSelectionModel>("rowSelectionListe").Select(mProxy);
                        //DeselectGridRows();
                    }

                   

                    CloseDetailWindow();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            
            return this.Direct();
                       
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemGroupeFournisseur, string ItemTypeFournisseur, string ItemCertified, string ItemApproved, string ItemStatus, string ItemSite, string ItemOtherSite = "")
        {

            int SupplierGroupID = GetCritriaValue(ItemGroupeFournisseur);

            int SupplierTypeID = GetCritriaValue(ItemTypeFournisseur);

            int CertifiedID = GetCritriaValue(ItemCertified);

            int Approuvé = GetCritriaValue(ItemApproved);

            int StatusID = GetCritriaValue(ItemStatus);

            //int AgenceID = GetCritriaValue(ItemAgence);
            int SiteID = GetCritriaValue(ItemSite);
            int OtherSite = (ItemOtherSite == "true" && SiteID != -1) ? 1 : 0;
            string filterHeaders = this.Request.Params["filterheader"];

            var mListe = (new Fournisseur()).fnSelect(SupplierGroupID, SupplierTypeID, CertifiedID, StatusID,Approuvé, SiteID, OtherSite);
            return this.Store(mListe);
            //return this.Store(GridStorePaging.SetRangePlants(parameters, mListe, filterHeaders));                    
        }        


        public List<DataPersist> Filtering(StoreRequestParameters parameters, List<DataPersist> data)
        {
            FilterConditions fc = parameters.GridFilters;

            //-- start filtering ------------------------------------------------------------
            if (fc != null)
            {
                foreach (FilterCondition condition in fc.Conditions)
                {
                    Comparison comparison = condition.Comparison;
                    string field = condition.Field;
                    FilterType type = condition.Type;

                    object value;
                    switch (condition.Type)
                    {
                        case FilterType.Boolean:
                            value = condition.Value<bool>();
                            break;
                        case FilterType.Date:
                            value = condition.Value<DateTime>();
                            break;
                        case FilterType.List:
                            value = condition.List;
                            break;
                        case FilterType.Number:
                            if (data.Count > 0 && data[0].GetType().GetProperty(field).PropertyType == typeof(int))
                            {
                                value = condition.Value<int>();
                            }
                            else
                            {
                                value = condition.Value<double>();
                            }

                            break;
                        case FilterType.String:
                            value = condition.Value<string>();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    data.RemoveAll(
                        item =>
                        {
                            object oValue = item.GetType().GetProperty(field).GetValue(item, null);
                            IComparable cItem = oValue as IComparable;

                            switch (comparison)
                            {
                                case Comparison.Eq:
                                case Comparison.Like:
                                case Comparison.In:

                                    switch (type)
                                    {
                                        case FilterType.List:
                                            return !(value as List<string>).Contains(oValue.ToString());
                                        case FilterType.String:
                                            return !oValue.ToString().StartsWith(value.ToString());
                                        default:
                                            return !cItem.Equals(value);
                                    }

                                case Comparison.Gt:
                                    return cItem.CompareTo(value) < 1;
                                case Comparison.Lt:
                                    return cItem.CompareTo(value) > -1;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                        }
                    );
                }
            }
            //-- end filtering ------------------------------------------------------------
            return data;
        }
        
        public ActionResult OnRefresh(string ItemGroupeFournisseur, string ItemTypeFournisseur, string ItemCertified, string ItemApproved, string ItemStatus, string ItemSite, string ItemOtherSite)
        {
            Store mstore = X.GetCmp<Store>("storeListe");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemGroupeFournisseur"   ,ItemGroupeFournisseur),
                                new Ext.Net.Parameter("ItemTypeFournisseur"     ,ItemTypeFournisseur),
                                new Ext.Net.Parameter("ItemCertified"           ,ItemCertified),
                                new Ext.Net.Parameter("ItemApproved"            ,ItemApproved),       
                                new Ext.Net.Parameter("ItemStatus"              ,ItemStatus),
                                //new Ext.Net.Parameter("ItemAgence"              ,ItemAgence)
                                new Ext.Net.Parameter("ItemSite"              ,ItemSite),
                                new Ext.Net.Parameter("ItemOtherSite"        ,ItemOtherSite)
                            });
            
            string title = X.GetCmp<FormPanel>("CriteriaPanel").Title;
            title += "Site:" + X.GetCmp<ComboBox>("cmbSite").SelectedItem.Text;
            title += "; Groupe Fournisseur:" + X.GetCmp<ComboBox>("cmbGroupeFournisseur").SelectedItem.Text;

            title += "; Type Fournisseur:" + X.GetCmp<ComboBox>("cmbFournisseurType").SelectedItem.Text;

            title += "; Certification:" + X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Text;

            //title += "; Agence=" + X.GetCmp<ComboBox>("cmbAgence").SelectedItem.Text;

            X.GetCmp<FormPanel>("CriteriaPanel").Title = title;

            // collapse criterias areas
            X.GetCmp<FormPanel>("CriteriaPanel").Collapse(Direction.Top, false);

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            X.GetCmp<FormPanel>("CriteriaPanel").ToggleCollapse();
            
            return this.Direct();
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


            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'farmerCard{0}', 'ReportViewer/OnDisplayReport', this, 'Farmer card')", id_farmer));
            //return JavaScript("alert('Hello world')");
        }

        public ActionResult OnDisplaySupplierList()
        {
            string UserName = (string)Session["userName"];

            Site mSiteParDefaut = new Site();
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;
            Parametres mParam = new Parametres(0);
            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;
            if (mParam.Site == mSiteParDefaut.ID) ViewData["UrlSite"] = "LoadSiteAll";
            else ViewData["UrlSite"] = "LoadSiteByAccess";

            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmFarmers_List_Criteria", ViewData = ViewData };
        }

        [HttpPost]
        public ActionResult OnPrintSupplierList()
        {
            XtraReport report = null;

            report = new rptSupplierList() as XtraReport;

            report.DataSource = DevExpressReportDs.SetDataSource(report);

            Session["SiteID"] = int.Parse(GetFormValue("cmbRepSite"));
            Session["SiteText"] = X.GetCmp<ComboBox>("cmbRepSite").SelectedItem.Text.ToString();

            Session["CertifiedID"] = int.Parse(GetFormValue("cmbRepCertification"));
            Session["CertifiedText"] = X.GetCmp<ComboBox>("cmbRepCertification").SelectedItem.Text.ToString();

            Session["IsApproved"] = int.Parse(GetFormValue("cmbRepApproval"));
            Session["IsApprovedText"] = X.GetCmp<ComboBox>("cmbRepApproval").SelectedItem.Text.ToString();

            Session["IsDisabled"] = int.Parse(GetFormValue("cmbRepStatus"));
            Session["IsDisabledText"] = X.GetCmp<ComboBox>("cmbRepStatus").SelectedItem.Text.ToString();

            Session["SupplierGroupID"] = int.Parse(GetFormValue("cmbRepGroupeFournisseur"));
            Session["SupplierGroupName"] = X.GetCmp<ComboBox>("cmbRepGroupeFournisseur").SelectedItem.Text.ToString();

            Session["SupplierTypeID"] = int.Parse(GetFormValue("cmbRepFournisseurType"));
            Session["SupplierTypeName"] = X.GetCmp<ComboBox>("cmbRepFournisseurType").SelectedItem.Text.ToString();
            
            ViewData["Report"] = report;
            
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            
            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'fas{0}', '{1}/Fournisseur/ViewFarmersList', this, 'Liste Des Fournisseurs',''),App.frmSuppliersList.doClose()", Guid.NewGuid(), BaseUrl));

            //return View();
        }


        public ActionResult ViewFarmersList()
        {
            XtraReport report = null;

            report = new rptSupplierList() as XtraReport;

            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["SiteID"].Value = Session["SiteID"];
            report.Parameters["SiteText"].Value = Session["SiteText"];

            report.Parameters["CertifiedID"].Value = Session["CertifiedID"];
            report.Parameters["CertifiedText"].Value = Session["CertifiedText"];

            report.Parameters["IsApproved"].Value = Session["IsApproved"];
            report.Parameters["IsApprovedText"].Value = Session["IsApprovedText"];

            report.Parameters["IsDisabled"].Value = Session["IsDisabled"];
            report.Parameters["IsDisabledText"].Value = Session["IsDisabledText"];

            report.Parameters["SupplierGroupID"].Value = Session["SupplierGroupID"];

            report.Parameters["SupplierGroupName"].Value = Session["SupplierGroupName"];

            report.Parameters["SupplierTypeID"].Value = Session["SupplierTypeID"];

            report.Parameters["SupplierTypeName"].Value = Session["SupplierTypeName"];

            ViewData["Report"] = report;

            return View();
        }



        public ActionResult OnDisplaySupplierFinancialStatus(string id_farmer)
        {
            bool hasConvert = false;
            int mIdFarmer;
            hasConvert = int.TryParse(id_farmer, out mIdFarmer);
            Fournisseur mClass = new Fournisseur();
            bool load = mClass.fnGet(mIdFarmer);
            if (hasConvert && mClass.ID != 0)
            {
                return new Ext.Net.MVC.PartialViewResult { ViewName = "frmFarmerAccountStatus_Criteria" };
            }

            return this.Direct();
        }
                   
        public ActionResult GenerateReport(string TypeReport)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            Financement ficlass = new Financement();

            //contratperiode.Campagne = new Campagne();
            ficlass.Campagne = GetFormValue("FIPcropYearForReport");

            ficlass.Sites = new Site();
            ficlass.Sites.ID = int.Parse(GetFormValue("FISiteForReport"));
            ficlass.Sites.Nom = X.GetCmp<ComboBox>("FISiteForReport").SelectedItem.Text;

            ficlass.Fournisseur = new Fournisseur();
            ficlass.Fournisseur.ID = int.Parse(GetFormValue("FIfournisseurForReport"));
            ficlass.Fournisseur.Nom = X.GetCmp<ComboBox>("FIfournisseurForReport").SelectedItem.Text;

            ficlass.FinancementType = new FinancementType();
            ficlass.FinancementType.ID = int.Parse(GetFormValue("FITypeFinancementForReport"));
            ficlass.FinancementType.Designation = X.GetCmp<ComboBox>("FITypeFinancementForReport").SelectedItem.Text;

            ficlass.DateFinancement = DateTime.Parse(X.GetCmp<DateField>("FIstartDateForReport").RawText.ToString());
            ficlass.DateEcheance = DateTime.Parse(X.GetCmp<DateField>("FIdueDateForReport").RawText.ToString());
            ficlass.Statut = X.GetCmp<ComboBox>("FIOptionReport").SelectedItem.Value.ToString();

            X.Js.Call("CloseWindow");

            string param = JSON.Serialize(ficlass);
            Session["ParamReport"] = param;

            string ReportTitle = string.Empty;
            switch (TypeReport)
            {
                case FinancementReportType.Balance:
                    ReportTitle = "Situation Des Financements";
                    break;
                case FinancementReportType.Execution:
                    ReportTitle = "Execution Des Financements";
                    break;
                case FinancementReportType.Exposure:
                    ReportTitle = "Risque Sur financement";
                    break;
                case FinancementReportType.FinancialStatus:
                    ReportTitle = "Position Financière";
                    break;
                case FinancementReportType.FinancialStatusCrop:
                    ReportTitle = "Position Financière";
                    break;
            }
            //HttpUtility.JavaScriptStringEncode(param)
            return JavaScript(String.Format("CloseWindow(), addTab(window.parent.Ext.getCmp('tabCenter'), 'FinancingReport{0}', '{1}/Financement/ViewReport?TypeReport={2}', this, '{3}','')", Guid.NewGuid(), BaseUrl, TypeReport, ReportTitle));
        }

        public ActionResult OnPrintFinancingReport(string ReportType, string id_farmer)
        {           
            FinancementViewModel mclass = new FinancementViewModel();

            mclass._Financement = new Financement();
            mclass._Financement.Sites = new Site();
            bool result = false;
            int idFournisseur = -1;
            result = int.TryParse(id_farmer, out idFournisseur);
            string UserName = (string)Session["userName"];

            Site mSiteParDefaut = new Site();
            //bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            //mclass._Financement.Sites.ID = mSiteParDefaut.ID;
            Parametres parametre = new Parametres(0);

            ViewData["UrlSite"] = "LoadSiteAll";
            //else ViewData["UrlSite"] = "LoadSiteByAccess";
            //mclass._Financement.Campagne = new Campagne();
            mclass._Financement.Sites = new Site();
            Fournisseur mFClass = new Fournisseur();

            if (idFournisseur == -1 )
            {
                result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
                mclass._Financement.Sites.ID = mSiteParDefaut.ID;
                ViewData["fournisseurID"] = idFournisseur;
            }
            else
            {
                mFClass = GetFournisseurById(id_farmer);
                mclass._Financement.Sites.ID = mFClass.Agence.ID;
                //mclass._Financement.Sites.Nom = mFClass.Agence.Nom;
                ViewData["fournisseurID"] = mFClass.ID;
            }
                                                                      
            mclass._Financement.Campagne = parametre.Campagne;
            switch (ReportType)
            {
                case FinancementReportType.Balance:
                    mclass._ReportType = FinancementReportType.Balance;
                    ViewData["ReportTitle"] = "Situation Des Financements - Etat";
                    ViewData["CanFilterByPeriod"] = true;
                    break;
                case FinancementReportType.Execution:
                    mclass._ReportType = FinancementReportType.Execution;
                    ViewData["ReportTitle"] = "Execution Des Financements - Etat";
                    ViewData["CanFilterByPeriod"] = true;
                    break;
                case FinancementReportType.Exposure:
                    mclass._ReportType = FinancementReportType.Exposure;
                    ViewData["ReportTitle"] = "Risque Sur financement - Etat";
                    ViewData["CanFilterByPeriod"] = false;
                    break;
                case FinancementReportType.FinancialStatus:
                    mclass._ReportType = FinancementReportType.FinancialStatus;
                    ViewData["ReportTitle"] = "Position Financière - Etat";
                    ViewData["CanFilterByPeriod"] = false;
                    break;
                case FinancementReportType.FinancialStatusCrop:
                    mclass._ReportType = FinancementReportType.FinancialStatusCrop;
                    ViewData["ReportTitle"] = "Position Financière - Etat";
                    ViewData["CanFilterByPeriod"] = false;
                    break;
            }

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintFinancingReport", Model = mclass, ViewData = ViewData };
        }

        private static Fournisseur GetFournisseurById(string id_farmer)
        {
            bool hasConvert = false;
            int mIdFarmer;
            hasConvert = int.TryParse(id_farmer, out mIdFarmer);
            Fournisseur mFClass = new Fournisseur();
            bool load = mFClass.fnGet(mIdFarmer);
            return mFClass;
        }

        public ActionResult OnPrintBalanceReport(string id_farmer)
        {
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Mask();
            ContratPeriodeViewModel mclass = new ContratPeriodeViewModel();

            Fournisseur mFClass = GetFournisseurById(id_farmer);

            ViewData["fournisseurID"] = mFClass.ID;

            mclass._ContratPeriode = new ContratPeriode();
            mclass._ContratPeriode.Sites = new Site();
            mclass._ContratPeriode.Sites.ID = mFClass.Agence.ID;
            mclass._ContratPeriode.Sites.Nom = mFClass.Agence.Nom;
            mclass._ContratPeriode.Campagne = new Campagne();
            mclass._TypeContratPeriodeReport = new TypeContratPeriodeReport();
            mclass._TypeContratPeriodeReport.Designation = "Balance";
           
            //var mParam = (new Parametres()).fnSelect();
            Parametres parametre = new Parametres();
            parametre.fnGet();
            mclass._ContratPeriode.Campagne.Designation = parametre.Campagne;
            mclass._ContratPeriode.DateDebut = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintBalanceCP", Model = mclass, ViewData= ViewData };
        }


        public ActionResult OnPrintExecutionReport(string id_farmer)
        {
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Mask();
            ContratPeriodeViewModel mclass = new ContratPeriodeViewModel();

            mclass._ContratPeriode = new ContratPeriode();
            mclass._ContratPeriode.Campagne = new Campagne();
            mclass._TypeContratPeriodeReport = new TypeContratPeriodeReport();
            mclass._TypeContratPeriodeReport.Designation = "Execution";            

            Fournisseur mFClass = GetFournisseurById(id_farmer);

            ViewData["fournisseurID"] = mFClass.ID;
            mclass._ContratPeriode.Sites = new Site();
            mclass._ContratPeriode.Sites.ID = mFClass.Agence.ID;
            mclass._ContratPeriode.Sites.Nom = mFClass.Agence.Nom;
            //var mParam = (new Parametres()).fnSelect();
            Parametres parametre = new Parametres();
            parametre.fnGet();
            ViewBag.TypeReport = "Execution";
            mclass._ContratPeriode.Campagne.Designation = parametre.Campagne;
            mclass._ContratPeriode.DateDebut = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintBalanceCP", Model = mclass, ViewData = ViewData };
        }

        public ActionResult OnDisplayForwardContractBalance(string id_farmer)
        {
           return OnPrintBalanceReport(id_farmer);
        }
        
        public ActionResult OnDisplaySupplierForwardContractExecution(string id_farmer)
        {
            return OnPrintExecutionReport(id_farmer);
        }
        
        public ActionResult OnDisplaySupplierStatusAccount(string id_farmer)
        {
            bool hasConvert = false;
            int mIdFarmer;
            hasConvert = int.TryParse(id_farmer, out mIdFarmer);
            Fournisseur mClass = new Fournisseur();
            if (mIdFarmer == -1)
            {
                mClass.ID = -1;
                return new Ext.Net.MVC.PartialViewResult { ViewName = "frmFarmerAccountStatus_Criteria", Model = mClass };
            }
            else {
                bool load = mClass.fnGet(mIdFarmer);
                if (hasConvert && mClass.ID != 0)
                {
                    return new Ext.Net.MVC.PartialViewResult { ViewName = "frmFarmerAccountStatus_Criteria", Model = mClass };
                }
            }
            return this.Direct();  
        }
        
        public ActionResult OnPrintSupplierStatusAccount(string id_farmer, string startDate , string endDate)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            //string param = JSON.Serialize(contratperiode);
            Session["paramFarmerID"]    = id_farmer;
            Session["paramStartDate"]   = DateTime.Parse(startDate);
            Session["paramEndDate"]     = DateTime.Parse(endDate);

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'fas{0}', '{1}/Fournisseur/ViewFarmerAccountStatus', this, 'Statut Fournisseur',''),App.frmSupplierStatusAccount.doClose()", Guid.NewGuid(), BaseUrl));
        }
            
        public ActionResult OnOpenSupplierDashBoard(string ItemFournisseurId, string ItemFournisseurName)
        {            
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            //string url = String.Format("{0}/DashBoard/IndexFournisseur?ItemFournisseur={1}&ItemFournisseurName={2}", BaseUrl, ItemFournisseurId, Uri.EscapeDataString(ItemFournisseurName));
            //string url = Uri.EscapeDataString(urll);
            //return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), '{0}', '{1}', this, 'Supplier DashBoard','')", 12345678, url));
            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), '{0}', '{1}/DashBoard/IndexFournisseur?ItemFournisseur={2}&ItemFournisseurName={3}', this, 'Supplier DashBoard','')", 12345678, BaseUrl, ItemFournisseurId, Uri.EscapeDataString(ItemFournisseurName)));

        }


        public ActionResult OnDisplaySupplierDeliveries(string id_farmer, string status = "", string siteID = "")
        {
            bool hasConvert = false;
            int mIdFarmer;
            int mSiteID;
            hasConvert = int.TryParse(id_farmer, out mIdFarmer);
            hasConvert = int.TryParse(siteID, out mSiteID);
            Fournisseur mClass = new Fournisseur();
            bool load = mClass.fnGet(mIdFarmer);
            mClass.Agence = new Site();
            mClass.Agence.ID = mSiteID;
            ViewData["Titre"] = "Etat Livraison";
            ViewData["actionToDo"] = "OnPrintDeliveriesReport";
            ViewData["status"] = status;
            if (mIdFarmer == -1)
            {
                mClass.ID = -1;
                return new Ext.Net.MVC.PartialViewResult { ViewName = "frmDeliveryMonitoring_Criteria", Model = mClass, ViewData = ViewData };
            }
            else
            {
                if (hasConvert && mClass.ID != 0)
                {                    
                    //ViewData["Titre"] = "Etat Livraison";
                    //ViewData["actionToDo"] = "OnPrintDeliveriesReport";
                    return new Ext.Net.MVC.PartialViewResult { ViewName = "frmDeliveryMonitoring_Criteria", Model = mClass, ViewData = ViewData };
                }
                else
                {
                    return this.Direct();
                }
            }

            //return this.Direct();
        }

        public ActionResult ViewFarmerAccountStatus()
        {
            XtraReport report = null;

            report = new rptSupplierAccountStatus() as XtraReport;

            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["fournisseurID"].Value = Session["paramFarmerID"] ;

            report.Parameters["startDate"].Value = Session["paramStartDate"];

            report.Parameters["endDate"].Value = Session["paramEndDate"] ;
           
           

            ViewData["Report"] = report;

            return View();
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
        
        //#endregion

        #region "Methods"
        private void CloseDetailWindow()
        {
            X.GetCmp<Window>("FormFournisseur").Hide();
            //Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            //mViewport.Unmask();           
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

        private Fournisseur MapFormToObject(Fournisseur mClass)
        {
            mClass.Nom = GetFormValue("TxtFournisseurName").ToUpper();

            mClass.FournisseurGroupe = new Tms.Classes.Shared.FournisseurGroupe();
            mClass.FournisseurGroupe.ID = int.Parse(GetFormValue("GroupFournisseurID"));
            mClass.FournisseurGroupe.Designation = X.GetCmp<ComboBox>("GroupFournisseurID").SelectedItem.Text.ToString();

            mClass.FournisseurType = new Tms.Classes.Shared.FournisseurType();
            mClass.FournisseurType.ID = int.Parse(GetFormValue("TypeFournisseurID"));
            mClass.FournisseurType.Designation = X.GetCmp<ComboBox>("TypeFournisseurID").SelectedItem.Text.ToString();

            mClass.Provenance = new Tms.Classes.Shared.Provenance();
            mClass.Provenance.ID = int.Parse(GetFormValue("OriginFournisseurID"));
            mClass.Provenance.Nom = X.GetCmp<ComboBox>("OriginFournisseurID").SelectedItem.Text.ToString();

            mClass.PieceNumero = GetFormValue("TxtPieceNumeroFournisseur");

            int pieceID;

            if (int.TryParse(GetFormValue("PieceTypeIDFournisseur"), out pieceID))
            {
                mClass.PieceType = new Tms.Classes.Shared.PieceType();
                mClass.PieceType.ID = int.Parse(GetFormValue("PieceTypeIDFournisseur"));
                mClass.PieceType.Designation = X.GetCmp<ComboBox>("PieceTypeIDFournisseur").SelectedItem.Text.ToString();
            }
            else
                mClass.PieceType = null;                 
            
            mClass.Adresse = GetFormValue("TxtAdressFournisseur");
            mClass.TelephoneFixe = GetFormValue("TxtTelFixeFournisseur");
            mClass.TelephoneMobile = GetFormValue("TxtTelMobileFournisseur");
            mClass.Fax = GetFormValue("TxtFaxFournisseur");
            mClass.eMail = GetFormValue("TxtEmailFournisseur");
            mClass.CompteNumero = GetFormValue("TxtAccountingIDForunisseur");

            mClass.NumAgrement = GetFormValue("TxtNumAgrementFournisseur");
            mClass.NumeroCC = GetFormValue("TxtNumeroCCFournisseur");
            mClass.NumeroRegistre = GetFormValue("TxtNumeroRegistreFournisseur");
            mClass.SiegeSocial = GetFormValue("TxtSiegeSocialFournisseur");
            mClass.FloId = GetFormValue("TxtFloIdFournisseur");

            if (X.GetCmp<TextField>("TxtCapitalFournisseur").Text != string.Empty) mClass.Capital = decimal.Parse(X.GetCmp<TextField>("TxtCapitalFournisseur").Text);            

            int certificationID;

            if (int.TryParse(GetFormValue("CertificationID"), out certificationID))
            {
                mClass.Certification = new Tms.Classes.Shared.Certification();
                mClass.Certification.ID = certificationID;
                mClass.Certification.Designation = X.GetCmp<ComboBox>("CertificationID").SelectedItem.Text.ToString();
            }
            else
                mClass.Certification = null;

            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            //Site mSiteParDefaut = new Site();
            //mClass.SiteParDefaut = new Site();
            //bool result = mSiteParDefaut.fnGetDefaultSite();
            //if (result) mClass.SiteParDefaut.ID = mSiteParDefaut.ID;

            int agenceID;

            if (int.TryParse(GetFormValue("cmbAgenceFrs"), out agenceID))
            {
                mClass.Agence = new Tms.Classes.Shared.Site();
                mClass.Agence.ID = agenceID;
                mClass.Agence.Nom = X.GetCmp<ComboBox>("cmbAgenceFrs").SelectedItem.Text.ToString();
                //mClass.SiteParDefaut.ID = agenceID;
            }
            else
                mClass.Agence = null;

            return mClass;
        }

        private void MapObjectToForm(Fournisseur mClass)
        {
            X.GetCmp<TextField>("TxtFournisseurID").Text = mClass.ID.ToString();
            X.GetCmp<TextField>("TxtFournisseurName").Text = mClass.Nom;
            X.GetCmp<TextField>("TxtAdressFournisseur").Text = mClass.Adresse;
            X.GetCmp<TextField>("TxtPieceNumeroFournisseur").Text = mClass.PieceNumero;
            X.GetCmp<TextField>("TxtTelFixeFournisseur").Text = mClass.TelephoneFixe;
            X.GetCmp<TextField>("TxtAccountingIDForunisseur").Text = mClass.CompteNumero;
            X.GetCmp<TextField>("TxtEmailFournisseur").Text = mClass.eMail;

            X.GetCmp<ComboBox>("GroupFournisseurID").SetValue(mClass.FournisseurGroupe.ID.ToString());
            X.GetCmp<ComboBox>("TypeFournisseurID").SetValue(mClass.FournisseurType.ID.ToString());
            X.GetCmp<ComboBox>("OriginFournisseurID").SetValue(mClass.Provenance.ID.ToString());
            X.GetCmp<ComboBox>("PieceTypeIDFournisseur").SetValue(mClass.PieceType.ID.ToString());
            X.GetCmp<ComboBox>("CertificationID").SetValue(mClass.Certification.ID.ToString());
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

        public ActionResult LoadFournisseurByPayementType(int? PayementTypeID, int? siteID)
        {
            List<DataPersist> myList = new List<DataPersist>();
            try
            {                
                if (!PayementTypeID.HasValue)
                    return this.Store(myList);

                if (!siteID.HasValue)
                    return this.Store(myList);

                switch (PayementTypeID)
                {
                    case 1:
                        myList = new Fournisseur().fnSelectFromDelivery((int)siteID);
                        break;
                    case 2:
                        myList = new Fournisseur().fnSelectFromFinancing((int)siteID);
                        break;
                    case 3:
                        myList = new Fournisseur().fnSelectFromSaving((int)siteID);
                        break;
                    case 4:
                        myList = new Fournisseur().fnSelectFromPrime((int)siteID);
                        break;
                    case 5:
                        myList = new Fournisseur().fnSelectFromCommission((int)siteID);
                        break;
                }

                return this.Store(myList);
            }
            catch (Exception Ex)
            {
                X.Msg.Alert("Erreur", "FournisseurController : OnSearchSupplier : " + Ex.Message).Show();                
            }
            return this.Store(myList);
        }

        public ActionResult LoadFournisseurByPayementTypeAll(int? PayementTypeID, int? siteID)
        {
            List<DataPersist> myList = new List<DataPersist>();

            if (!PayementTypeID.HasValue)
                return this.Store(myList);

            if (!siteID.HasValue)
                return this.Store(myList);

            switch (PayementTypeID)
            {
                case 1:
                    myList = new Fournisseur().fnSelectFromDelivery((int)siteID);
                    break;
                case 2:
                    myList = new Fournisseur().fnSelectFromFinancing((int)siteID);
                    break;
                case 3:
                    myList = new Fournisseur().fnSelectFromSaving((int)siteID);
                    break;
                case -1:
                    myList = new Fournisseur().fnSelectFromDelivery((int)siteID);

                    foreach (DataPersist mv in new Fournisseur().fnSelectFromFinancing((int)siteID))
                    {
                        myList.Add(mv);
                    }

                    foreach (DataPersist mv in new Fournisseur().fnSelectFromSaving((int)siteID))
                    {
                        myList.Add(mv);
                    }

                    break;

            }
            Fournisseur mFournisseur = new Fournisseur();
            mFournisseur.ID = -1;
            mFournisseur.Nom = "{Tous}";
            
            myList.Insert(0, mFournisseur);
            mFournisseur = myList[0] as Fournisseur;

            return this.Store(myList);
        }

        private void InitSecurityFunctions()
        {
            try
            {
                //string UserName = (string)Session["UserName"];
                //Fonction objSecurity = new Fonction();
                //hiddenPermCreer.Value = objSecurity.fnGetUserAccessStatus("{f4bdee0a-943a-41d0-816d-91fad53aedd0}",UserName);
                //hiddenPermModifier.Value = objSecurity.HasFunctionAccess("{9400c2b1-babb-481a-adfc-ef1f68d86800}");
                //hiddenPermDesactiver.Value = objSecurity.HasFunctionAccess("{27e0ad4f-bd30-4bbc-b365-2d2ad8c58772}");
                //hiddenPermActiver.Value = objSecurity.HasFunctionAccess("{7b0454b8-5869-480b-b72a-43dd26b0186a}");
                //hiddenPermImprimer.Value = objSecurity.HasFunctionAccess("{301ac399-8723-4c8c-8bdc-20880c2f7585}");
                //hiddenPermExporterExcel.Value = objSecurity.HasFunctionAccess("{e10e6daa-6e97-43df-bf10-c4e1fc9ecc3a}");
                ////hiddenPermCurrentDistrict.Value =  true||objSecurity.HasFunctionAccess("");
                //hiddenPermAllDistricts.Value = objSecurity.HasFunctionAccess("{80458668-307c-4ea4-a94d-924347f51d30}");
                //hiddenPermRecordAcceptedBags.Value = objSecurity.HasFunctionAccess("{59dbf92c-9015-4081-b3d3-90763137f9f2}");

            }
            catch (Exception ex)
            {
                X.Msg.Alert(" : InitSecurityFunctions", ex.Message).Show();
            }
        }

        public ActionResult OnDisplaySupplierInvoiceHystory(string id_farmer)
        {
            bool hasConvert = false;
            int mIdFarmer;
            hasConvert = int.TryParse(id_farmer, out mIdFarmer);
            Fournisseur mClass = new Fournisseur();
            bool load = mClass.fnGet(mIdFarmer);
            ViewData["Titre"] = "Historique des factures";
            ViewData["actionToDo"] = "OnPrintHystoryOfInvoice";
            ViewData["ControllerName"] = "Facture";
            if (mIdFarmer == -1)
            {
                mClass.ID = -1;
                return new Ext.Net.MVC.PartialViewResult { ViewName = "frmSupplierPrint_Criteria", Model = mClass, ViewData = ViewData };
            }
            else
            {
                if (hasConvert && mClass.ID != 0)
                {
                    //ViewData["Titre"] = "Etat Livraison";
                    //ViewData["actionToDo"] = "OnPrintDeliveriesReport";
                    return new Ext.Net.MVC.PartialViewResult { ViewName = "frmSupplierPrint_Criteria", Model = mClass, ViewData = ViewData };
                }
                else
                {
                    return this.Direct();
                }
            }

            //return this.Direct();
        }

        public ActionResult OnDisplaySupplierPaymentHystory(string id_farmer)
        {
            bool hasConvert = false;
            int mIdFarmer;
            hasConvert = int.TryParse(id_farmer, out mIdFarmer);
            Fournisseur mClass = new Fournisseur();
            bool load = mClass.fnGet(mIdFarmer);
            ViewData["Titre"] = "Historique Des Paiements";
            ViewData["actionToDo"] = "OnPrintHystoryOfPayment";
            ViewData["ControllerName"] = "Payement";
            if (mIdFarmer == -1)
            {
                mClass.ID = -1;
                return new Ext.Net.MVC.PartialViewResult { ViewName = "frmPrintHistoryOfPayment", Model = mClass, ViewData = ViewData };
            }
            else
            {
                if (hasConvert && mClass.ID != 0)
                {
                    //ViewData["Titre"] = "Etat Livraison";
                    //ViewData["actionToDo"] = "OnPrintDeliveriesReport";
                    return new Ext.Net.MVC.PartialViewResult { ViewName = "frmPrintHistoryOfPayment", Model = mClass, ViewData = ViewData };
                }
                else
                {
                    return this.Direct();
                }
            }

            //return this.Direct();
        }


        public ActionResult ViewReportResult()
        {
            XtraReport report = null;

            // TODO :  sur le base du type de rapport : detaillé ou cumulé 
            // initialiser l'objet report avec l'object idoine
            if ((int)Session["fournisseurID"] == -1)
                report = new rptAllSupplierHystoryOfInvoice() as XtraReport;
            else
                report = new rptSupplierHystoryOfInvoice() as XtraReport;

            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramCampagne"].Value = Session["paramCampagne"];           

            report.Parameters["campagne"].Value = Session["campagneID"];

            report.Parameters["DeliveryTypeId"].Value = Session["DeliveryTypeId"];

            report.Parameters["DeliveryTypeNom"].Value = Session["DeliveryTypeNom"];

            report.Parameters["fournisseurID"].Value = Session["fournisseurID"];

            report.Parameters["DateDebut"].Value = Session["DateDebut"];

            report.Parameters["DateFin"].Value = Session["DateFin"];

            report.Parameters["Status"].Value = "AP";

            ViewData["Report"] = report;

            return View();
        }
    }

}