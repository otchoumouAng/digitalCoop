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
    public class FournisseurAgenceController : BaseController
    {

        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";

        public FournisseurAgenceController()
        {
            //string cultureName = "en";
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
        }

        // GET: FournisseurAgence
        public ActionResult Index()
        {
            int SupplierGroupID = GetCritriaValue(string.Empty);

            int SupplierTypeID = GetCritriaValue(string.Empty);

            int CertifiedID = GetCritriaValue(string.Empty);

            int Approuvé = GetCritriaValue(string.Empty);

            int StatusID = GetCritriaValue(string.Empty);

            int AgenceID = GetCritriaValue(string.Empty);            
          
            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{dde7f146-b41c-478a-93aa-0e859dc89693}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{3aac2dbc-b2cc-42f1-bb83-95754ecb75ff}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{2bd6f84d-a29f-4117-90ec-df1afe69ee1a}")))
                X.GetCmp<MenuItem>("mnuExportToExcel").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportToExcel").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{f9d03fcf-ff9b-4d81-a61a-41f448c12251}")))
                X.GetCmp<MenuItem>("mnuPrintSupplierList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintSupplierList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{a11fc517-2b34-4ae8-b33b-2783fd8159e4}")))
                X.GetCmp<Hidden>("FrshiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("FrshiddenPermDesactiver").SetValue(false);

            X.GetCmp<Hidden>("FrshiddenPermCreer").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{3aac2dbc-b2cc-42f1-bb83-95754ecb75ff}")));
            X.GetCmp<Hidden>("FrshiddenPermModifier").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{31332498-92bb-4134-9e6a-8576ff725b98}")));
            X.GetCmp<Hidden>("FrshiddenPermActiver").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{0dfa48e6-2a81-4cf6-a125-a0111a2653e4}")));
            X.GetCmp<Hidden>("FrshiddenPermExporterExcel").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{2bd6f84d-a29f-4117-90ec-df1afe69ee1a}")));
            X.GetCmp<Hidden>("FrshiddenPermApprove").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{b996dc13-6683-4652-b71d-645e70ae7961}")));
            X.GetCmp<Hidden>("FrshiddenPermOverview").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{e7df3a8b-6453-4bb3-92b6-b6311e88bd38}")));
            X.GetCmp<Hidden>("FrshiddenPermPrintDashBoard").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{11310ea0-1883-4303-8f33-1773da9d1629}")));

            X.GetCmp<Hidden>("FrshiddenPermModifierSpecial").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{1e1465a6-0683-4b58-8d1c-716fa72f8353}")));
            X.GetCmp<Hidden>("FrshiddenPermPrintFinancialStatus").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{d5c24a74-6070-4544-a577-d2d2ec8e4d49}")));
            X.GetCmp<Hidden>("FrshiddenPermPrintFinancialBalance").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{042f801c-d7c7-4426-915a-2e453606bc5d}")));

            X.GetCmp<Hidden>("FrshiddenPermPrintHistoryOfEngagement").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{31f56a2a-682f-4817-933d-6886bf254c45}")));
            X.GetCmp<Hidden>("FrshiddenPermPrintEngagementExecution").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{5b806f64-2582-49b7-80e8-0c2d9070c002}")));

            X.GetCmp<Hidden>("FrshiddenPermPrintHistoryOfDeliveries").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{83297caa-f4c3-4f15-b869-b04c49c55f85}")));
            X.GetCmp<Hidden>("FrshiddenPermPrintHistoryOfBE").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{99db58e3-5bb7-4c48-a8c2-e76491d4997f}")));
            X.GetCmp<Hidden>("FrshiddenPermPrintHistoryOfPayment").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{acd8db87-8b30-4d54-9720-20864f6b0e09}")));            

            X.GetCmp<Hidden>("FrshiddenPermPrintSupplierList").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{f9d03fcf-ff9b-4d81-a61a-41f448c12251}")));

            #endregion
            return View();
        }
        
        public ActionResult LoadFournisseurBySupplierGroup(int? TypeLivraisonID)
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
                        
            int supplierID = -1 ;
            int certificationID = -1;
            int isDisabled = 0;
            int isApproved = 1;

            myList = new Fournisseur().fnSelect(SupplierGroupID, supplierID, certificationID, isDisabled, isApproved);
            Fournisseur mClass = new Fournisseur();
            //mClass.ID = -1;
            //mClass.Nom = "{Tous}";
            //myList.Insert(0, mClass);

            //if (myList.Count > 0)
            //    mClass = myList[0] as Fournisseur; 
                           

            return this.Store(myList);
        }
        
        public ActionResult LoadAllFournisseurBySupplierGroup(int? TypeLivraisonID)
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

            myList = new Fournisseur().fnSelect(SupplierGroupID, supplierID, certificationID, isDisabled, isApproved);
            Fournisseur mClass = new Fournisseur();
            mClass.ID = -1;
            mClass.Nom = "{Tous}";
            myList.Insert(0, mClass);

            if (myList.Count > 0)
                mClass = myList[0] as Fournisseur;


            return this.Store(myList);
        }

        public ActionResult SelectSiteToAdd(int? ItemFournisseur, int? ItemSite = null)
        {
            List<DataPersist> myList = new List<DataPersist>();

            if (!ItemSite.HasValue)
                return this.Store(myList);

            if (!ItemFournisseur.HasValue)
                return this.Store(myList);

            myList = new Site().fnSelectAvailableForFS((int)ItemSite, (int)ItemFournisseur);
            Site mClass = new Site();

            if (myList.Count > 0)
                mClass = myList[0] as Site;

            return this.Store(myList);
        }

        public ActionResult OnCreate()
        {
            Site mSiteParDefaut = new Site();
            string UserName = (string)Session["userName"];
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            //ViewBag.SiteParDefaut = mSiteParDefaut.ID;
            FournisseurViewModel mclass = new FournisseurViewModel();
            
            mclass._Fournisseur = new Fournisseur();
            mclass._Fournisseur.ID = -1;
            mclass._Fournisseur.Agence = new Site();
            mclass._Fournisseur.Agence.ID = mSiteParDefaut.ID;
            mclass._Fournisseur.EstSectorLead = false;
            ViewData["UrlFournisseurType"] = "LoadFournisseurTypeSite";
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFournisseurAgence", Model = mclass, ViewData = ViewData };
        }

        public ActionResult OnConsult(string ItemSelected)
        {            
            Fournisseur mclass = JSON.Deserialize<Fournisseur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });                  

            FournisseurViewModel viewModel = new FournisseurViewModel();            

            viewModel._Fournisseur = mclass;
            viewModel._Fournisseur.EstSectorLead = true;
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
            ViewData["UrlFournisseurType"] = "LoadFournisseurType";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFournisseurAgence", Model = viewModel, ViewData = ViewData};
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
            Parametres mParam = new Parametres(0);
            viewModel._Fournisseur = mclass;
            viewModel._Fournisseur.EstSectorLead = (mclass.FournisseurType.ID == mParam.ChefAgenceType) ? true : false;
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            ViewData["UrlFournisseurType"] = "LoadFournisseurType";

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFournisseurAgence", Model = viewModel, ViewData = ViewData };
        }

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
        public ActionResult SubmitFormMethod(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                Fournisseur mClass = new Fournisseur();
                FournisseurAutreSites mAutreSites;
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

                bool result = false;
                bool resulSite = true;
                mClass = MapFormToObject(mClass);

                _db = mClass.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = mClass.fnUpdate(mtran);

                if (result)
                {
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

                            //(string)Session["userName"];
                            mAutreSites.UtilisateurCreation = (string)Session["userName"];
                            mAutreSites.UtilisateurModification = (string)Session["userName"];
                            if (mAutreSites.IsNew)
                            {
                                result = mAutreSites.fnUpdate(mtran);
                            }

                            if (!result)
                            {
                                _db.RollBackTransaction(mtran);
                                break;
                            }
                        }
                    }
                    
                    _db.CommitTransaction(mtran);

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
                _db.RollBackTransaction(mtran);
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            
            return this.Direct();
                       
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemGroupeFournisseur, string ItemTypeFournisseur, string ItemCertified, string ItemApproved, string ItemStatus, string ItemAgence, string ItemOtherSite = "")
        {

            int SupplierGroupID = GetCritriaValue(ItemGroupeFournisseur);

            int SupplierTypeID = GetCritriaValue(ItemTypeFournisseur);

            int CertifiedID = GetCritriaValue(ItemCertified);

            int Approuvé = GetCritriaValue(ItemApproved);

            int StatusID = GetCritriaValue(ItemStatus);

            int AgenceID = GetCritriaValue(ItemAgence);
            int OtherSite = (ItemOtherSite == "true" && AgenceID != -1) ? 1 : 0;
            string filterHeaders = this.Request.Params["filterheader"];

            var mListe = (new Fournisseur()).fnSelect(SupplierGroupID, SupplierTypeID, CertifiedID, StatusID,Approuvé, AgenceID, OtherSite);
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
        
        public ActionResult OnRefresh(string ItemGroupeFournisseur, string ItemTypeFournisseur, string ItemApproved, string ItemStatus, string ItemAgence, string ItemOtherSite)
        {
            Store mstore = X.GetCmp<Store>("storeListe");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemGroupeFournisseur"   ,ItemGroupeFournisseur),
                                new Ext.Net.Parameter("ItemTypeFournisseur"     ,ItemTypeFournisseur),
                                new Ext.Net.Parameter("ItemApproved"            ,ItemApproved),       
                                new Ext.Net.Parameter("ItemStatus"              ,ItemStatus),
                                new Ext.Net.Parameter("ItemAgence"              ,ItemAgence),
                                new Ext.Net.Parameter("ItemOtherSite"        ,ItemOtherSite)
                            });
            
            string title = X.GetCmp<FormPanel>("CriteriaPanel").Title;

            title += "Fournisseur groupe=" + X.GetCmp<ComboBox>("cmbGroupeFournisseur").SelectedItem.Text;

            title += "; Type Fournisseur=" + X.GetCmp<ComboBox>("cmbFournisseurType").SelectedItem.Text;

            title += "; Agence=" + X.GetCmp<ComboBox>("cmbAgence").SelectedItem.Text;

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
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmFarmers_List_Criteria" };
        }


        public ActionResult OnPrintSupplierList(short vCertified, string vCertifiedText, string vIsApproved , string vIsApprovedText, string vIsDisabled, string vIsDisabledText, string vSupplierGroupID, string vSupplierGroupName, string vSupplierTypeID, string vSupplierTypeName )
        {
            XtraReport report = null;

            report = new rptSupplierList() as XtraReport;

            report.DataSource = DevExpressReportDs.SetDataSource(report);

            Session["CertifiedID"] = vCertified;
            Session["CertifiedText"] = vCertifiedText;

            Session["IsApproved"] = vIsApproved;
            Session["IsApprovedText"] = vIsApprovedText;

            Session["IsDisabled"] = vIsDisabled;
            Session["IsDisabledText"] = vIsDisabledText;
            

            Session["SupplierGroupID"] = vSupplierGroupID;

            Session["SupplierGroupName"] = vSupplierGroupName;

            Session["SupplierTypeID"] = vSupplierTypeID;

            Session["SupplierTypeName"] = vSupplierTypeName;
            
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
            }
            //HttpUtility.JavaScriptStringEncode(param)
            return JavaScript(String.Format("CloseWindow(), addTab(window.parent.Ext.getCmp('tabCenter'), 'FinancingReport{0}', '{1}/Financement/ViewReport?TypeReport={2}', this, '{3}','')", Guid.NewGuid(), BaseUrl, TypeReport, ReportTitle));
        }

        public ActionResult OnPrintFinancingReport(string ReportType, string id_farmer)
        {
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Mask();
            FinancementViewModel mclass = new FinancementViewModel();

            mclass._Financement = new Financement();
            //mclass._Financement.Campagne = new Campagne();        

            Fournisseur mFClass = GetFournisseurById(id_farmer);

            ViewData["fournisseurID"] = mFClass.ID;


            //var mParam = (new Parametres()).fnSelect();
            Parametres parametre = new Parametres();
            parametre.fnGet();
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
            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), '{0}', '{1}/DashBoard/IndexFournisseur?ItemFournisseur={2}&ItemFournisseurName={3}', this, 'Tableau de Bord','')", 12345678, BaseUrl, ItemFournisseurId, Uri.EscapeDataString(ItemFournisseurName)));

        }


        public ActionResult OnDisplaySupplierDeliveries(string id_farmer, string status = "")
        {
            bool hasConvert = false;
            int mIdFarmer;
            hasConvert = int.TryParse(id_farmer, out mIdFarmer);
            Fournisseur mClass = new Fournisseur();
            bool load = mClass.fnGet(mIdFarmer);
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
            X.GetCmp<Window>("FormFournisseurAgence").Close();       
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
            mClass.Nom = GetFormValue("TxtFournisseurName");

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


            int agenceID;

            if (int.TryParse(GetFormValue("cmbAgenceFrs"), out agenceID))
            {
                mClass.Agence = new Tms.Classes.Shared.Site();
                mClass.Agence.ID = agenceID;
                mClass.Agence.Nom = X.GetCmp<ComboBox>("cmbAgenceFrs").SelectedItem.Text.ToString();
            }
            else
                mClass.Agence = null;
            
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

            string UserName = (string)Session["userName"];
            mClass.UtilisateurCreation = UserName;
            mClass.UtilisateurModification = UserName;
            
            //Site mSiteParDefaut = new Site();
            //mClass.SiteParDefaut = new Site();
            //bool result = mSiteParDefaut.fnGetDefaultSite();
            //if (result) mClass.SiteParDefaut.ID = mSiteParDefaut.ID;

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

            X.GetCmp<Window>("FormFournisseurAgence_Role").Close();
            return this.Direct();
        }

        public ActionResult onAddSite(string ItemFournisseur= "",string ItemSite = "")
        {
            //mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            ViewData["ItemSite"] = ItemSite;
            ViewData["ItemFournisseur"] = ItemFournisseur;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFournisseurAgence_Role", ViewData = ViewData };
        }

        public ActionResult SelectFrsAutreSite(string fournisseurID)
        {
            int valueID;
            try
            {
                bool result = int.TryParse(fournisseurID, out valueID);
                if (result)
                {
                    var listeAgences = new FournisseurAutreSites().fnSelect(valueID);

                    return this.Store(listeAgences);
                }
                else
                    throw new Exception("Fournisseur : Supplier not found.");
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Fournisseur : Data Validation",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Store(null);
            }                      
        }

        public ActionResult RemoveSiteToUser(string ItemSelected)
        {
            try
            {
                List<FournisseurAutreSites> sites = JSON.Deserialize<List<FournisseurAutreSites>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                FournisseurAutreSites mClass;

                if (sites.Count() > 0)
                {
                    Store mstore = X.GetCmp<Store>("storeListeAutreSite");

                    foreach (var item in sites)
                    {
                        if (item.IsNew)
                        {
                            ModelProxy _proxy = mstore.GetById(item.ID);
                            _proxy.Drop();

                            return this.Direct();
                        }
                        else
                        {
                            mClass = new FournisseurAutreSites();
                            mClass.fnGet(item.ID);
                            if (mClass == null || mClass.ID == Guid.Empty)
                                throw new Exception("RemoveSiteToUser : Retirer Sites failed.");

                            mClass.UtilisateurModification = (string)Session["userName"];

                            bool result = mClass.fnRemove();

                            if (result)
                            {

                                ModelProxy mProxy = mstore.GetById(mClass.ID);

                                mProxy.Drop();

                            }
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Fournisseur : Retirer Site",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

    }

}