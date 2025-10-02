using DevExpress.XtraReports.UI;
using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Xsl;
using Tms.Classes.Business;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{    
    public class DailyPriceController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        //public DailyPriceController()
        //{
        //    string cultureName = "en";
        //    Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
        //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
        //}

        // GET: DailyPrice
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);            

            Site mSiteParDefaut = new Site();

            string UserName = (string)Session["userName"];
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            Campagne mCampagne = new Campagne();

            mCampagne.fnGet(mParam.Campagne);
            X.GetCmp<DateField>("TxtPeriodStart").RawText = (DateTime.Parse(mCampagne.DateDebut.ToString())).ToShortDateString();
            X.GetCmp<DateField>("TxtPeriodEnd").RawText = DateTime.Now.ToShortDateString();

            X.GetCmp<FormPanel>("DailyPriceCriteriaPanel").SetTitle("Site : "+ mSiteParDefaut.Nom + ", Du : " + (DateTime.Parse(mCampagne.DateDebut.ToString())).ToShortDateString() + " Au : " + DateTime.Now.ToShortDateString());

            #region Set Function's Access            
            Fonction HasAccess = new Fonction();
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{2E59D500-3B5F-4A6B-BFE3-4ED066891B41}", UserName);
            if (HasAccess.fnGetUserAccessStatus("{A0F400E9-1A2D-4C6E-A8FC-9BDDF9D3A849}", UserName) == false)
                X.GetCmp<Button>("btnNewDailyPrice").Disable();
            else
                X.GetCmp<Button>("btnNewDailyPrice").Enable();

            if (HasAccess.fnGetUserAccessStatus("{443B21CB-EF6F-4F68-915C-B925FEBB03CB}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportDailyPrice").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportDailyPrice").Enable();

            if (HasAccess.fnGetUserAccessStatus("{ED086978-F5A8-48A1-8CDC-2F0690D5BEEF}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintDailyPriceList").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintDailyPriceList").Enable();

            X.GetCmp<Hidden>("DlphiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{A0F400E9-1A2D-4C6E-A8FC-9BDDF9D3A849}", UserName));
            X.GetCmp<Hidden>("DlphiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{E126C43C-4183-4178-A5C5-314D21DC6FBE}", UserName));
            X.GetCmp<Hidden>("DlphiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{2DFC09CB-66AE-45C1-AE2B-7C6857F0E7EC}", UserName));
            X.GetCmp<Hidden>("DlphiddenPermActiver").SetValue(HasAccess.fnGetUserAccessStatus("{2DFC09CB-66AE-45C1-AE2B-7C6857F0E7EC}", UserName));
            X.GetCmp<Hidden>("DlphiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{443B21CB-EF6F-4F68-915C-B925FEBB03CB}", UserName));
            X.GetCmp<Hidden>("DlphiddenPermExtend").SetValue(HasAccess.fnGetUserAccessStatus("{750F76F4-2B36-46DF-9F64-A1666F08324A}", UserName));
            X.GetCmp<Hidden>("DlphiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{2E59D500-3B5F-4A6B-BFE3-4ED066891B41}", UserName));

            #endregion


            return View();
        }
                
        public ActionResult onAdd()
        {           
            PrixJournalierViewModel mclass = new PrixJournalierViewModel();

            

            mclass._PrixJournalier = new PrixJournalier();
            Parametres mParam = new Parametres(0);
            mclass._Parametres = mParam;            
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            mclass._PrixJournalier.Campagne = new Campagne();
            mclass._PrixJournalier.Campagne.Designation = mParam.Campagne;

            Site mSiteParDefaut = new Site();
            string UserName = (string)Session["userName"];
            bool result = mSiteParDefaut.fnGetDefaultSite();
            mclass._PrixJournalier.Site = new Site();
            if (result)
            {
                mclass._PrixJournalier.Site.ID = mSiteParDefaut.ID;
                mclass._PrixJournalier.Site.Nom = mSiteParDefaut.Nom;
            }
            mclass._SiteParDefautNom = mSiteParDefaut.Nom;
            //FormPanel mForm = X.GetCmp<FormPanel>("FormDailyPrice");           

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDailyPrice", Model = mclass, };
        }

        public ActionResult onAddForSite()
        {
            PrixJournalierViewModel mclass = new PrixJournalierViewModel();

            mclass._PrixJournalier = new PrixJournalier();
            Parametres mParam = new Parametres(0);
            mclass._Parametres = mParam;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            mclass._PrixJournalier.Campagne = new Campagne();
            mclass._PrixJournalier.Campagne.Designation = mParam.Campagne;

            Site mSiteParDefaut = new Site();
            string UserName = (string)Session["userName"];
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            mclass._PrixJournalier.Site = new Site();
            if (result)
            {
                mclass._PrixJournalier.Site.ID = mSiteParDefaut.ID;
                mclass._PrixJournalier.Site.Nom = mSiteParDefaut.Nom;
            }
            //FormPanel mForm = X.GetCmp<FormPanel>("FormDailyPrice");           

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDailyPriceDetail", Model = mclass, };
        }


        public ActionResult onEdit(string ItemSelected)
        {            
            PrixJournalier mclass = JSON.Deserialize<PrixJournalier>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            //MapObjectToForm(mclass);          

            PrixJournalierViewModel viewModel = new PrixJournalierViewModel();

            viewModel._PrixJournalier = mclass;
            Parametres mParam = new Parametres(0);
            viewModel._Parametres = mParam;
            viewModel._PrixJournalier.Campagne = new Campagne();
            viewModel._PrixJournalier.Campagne.Designation = mParam.Campagne;
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            //Site mSiteParDefaut = new Site();
            //bool result = mSiteParDefaut.fnGetDefaultSite();
            //viewModel._PrixJournalier.Site = new Site();
            //if (result)
            //{
            //    viewModel._PrixJournalier.Site.ID = mSiteParDefaut.ID;
            //    viewModel._PrixJournalier.Site.Nom = mSiteParDefaut.Nom;
            //}

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDailyPriceDetail", Model = viewModel };
        }

        public ActionResult onConsult(string ItemSelected)
        {            

            PrixJournalier mclass = JSON.Deserialize<PrixJournalier>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            //MapObjectToForm(mclass);          

            PrixJournalierViewModel viewModel = new PrixJournalierViewModel();

            viewModel._PrixJournalier = mclass;
            Parametres mParam = new Parametres(0);
            viewModel._Parametres = mParam;
            viewModel._PrixJournalier.Campagne = new Campagne();
            viewModel._PrixJournalier.Campagne.Designation = mParam.Campagne;
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDailyPriceDetail", Model = viewModel };
        }

        public ActionResult onExtend(string ItemSelected)
        {            

            PrixJournalier mclass = JSON.Deserialize<PrixJournalier>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            //MapObjectToForm(mclass);          

            PrixJournalierViewModel viewModel = new PrixJournalierViewModel();

            viewModel._PrixJournalier = mclass;
            Parametres mParam = new Parametres(0);
            viewModel._Parametres = mParam;
            viewModel._PrixJournalier.Campagne = new Campagne();
            viewModel._PrixJournalier.Campagne.Designation = mParam.Campagne;
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Extend;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDailyPriceDetail", Model = viewModel };
        }

        public ActionResult OnShrink(string ItemSelected)
        {
            PrixJournalier mclass = JSON.Deserialize<PrixJournalier>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            //MapObjectToForm(mclass);          

            PrixJournalierViewModel viewModel = new PrixJournalierViewModel();

            viewModel._PrixJournalier = mclass;
            Parametres mParam = new Parametres(0);
            viewModel._Parametres = mParam;
            viewModel._PrixJournalier.Campagne = new Campagne();
            viewModel._PrixJournalier.Campagne.Designation = mParam.Campagne;
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDailyPriceShrink", Model = viewModel };
        }

        [HttpPost]
        public ActionResult UpdateFormMethod()
        {

            try
            {
                PrixJournalier mClass = new PrixJournalier();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtDailyPriceID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Prix Journalier load failed.");
                }

                mClass = MapFormToObject(mClass);

                bool result = mClass.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeDailyPrice");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionDailyPrice").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormDailyPriceDetail").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prix Journalier : Data Validation",
                    Message = ex.Message,                                        
                    Buttons = MessageBox.Button.OK,                    
                    Icon = MessageBox.Icon.WARNING 
                });
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
                List<Site> ItemSite = JSON.Deserialize<List<Site>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                bool resultsiteprice = false;

                List<PrixJournalier> mListPrix = new List<PrixJournalier>();
                PrixJournalier mClass = new PrixJournalier();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtDailyPriceID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Prix Journalier load failed.");
                }

                if (ItemSite.Count > 0)
                {
                    _db = mClass.db();
                    mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                    for (int i = 0; i < ItemSite.Count(); i++)
                    {
                        mClass = new PrixJournalier();
                        mClass = MapFormToObject(mClass);
                        mClass.Site = new Site();
                        mClass.Site.ID = ItemSite[i].ID;
                        mClass.Site.Nom = ItemSite[i].Nom;
                        resultsiteprice = mClass.fnUpdate(mtran);

                        if (!resultsiteprice)
                        {
                            break;
                        }

                        mListPrix.Add(mClass);
                        mClass = new PrixJournalier();
                    }

                    if (!resultsiteprice)
                        _db.RollBackTransaction(mtran);
                    else
                        _db.CommitTransaction(mtran);
                }
                else
                {
                    mClass = MapFormToObject(mClass);
                    resultsiteprice = mClass.fnUpdate();
                    mListPrix.Add(mClass);
                }                                                

                if (resultsiteprice)
                {
                    Store mstore = X.GetCmp<Store>("storeListeDailyPrice");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        foreach (PrixJournalier item in mListPrix)
                        {
                            mstore.Insert(0, item);
                            X.GetCmp<RowSelectionModel>("rowSelectionDailyPrice").Select(0);
                        }

                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                }

                X.GetCmp<Window>("FormDailyPrice").Close();
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prix Journalier : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitExtendFormMethod()
        {

            try
            {
                PrixJournalier mClass = new PrixJournalier();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode != Tms.Components.Settings.EnumsDefinition.eExecMode.Extend)
                    throw new Exception("SubmitFormMethod : Prix Journalier load failed.");
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtDailyPriceID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Prix Journalier load failed.");
                }

                mClass = MapFormToObject(mClass);

                bool result = mClass.fnExtend();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeDailyPrice");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionDailyPrice").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormDailyPriceDetail").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prix Journalier : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitShrinkFormMethod()
        {

            try
            {
                PrixJournalier mClass = new PrixJournalier();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode != Tms.Components.Settings.EnumsDefinition.eExecMode.Extend)
                    throw new Exception("SubmitFormMethod : Prix Journalier load failed.");
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtDailyPriceID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Prix Journalier load failed.");
                }

                mClass = MapFormToObject(mClass);

                bool result = mClass.fnExtend();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeDailyPrice");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionDailyPrice").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormDailyPriceDetail").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prix Journalier : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult Select(StoreRequestParameters parameters, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite)
        {
            if (string.IsNullOrEmpty(ItemPeriodStart) && string.IsNullOrEmpty(ItemPeriodEnd) && string.IsNullOrEmpty(ItemStatus))
            {
                return this.Store(null);
            }
            else
            {
                int siteID = GetCritriaValue(ItemSite);
                
                DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
                DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());              
                int status = -1;
                if (string.IsNullOrEmpty(ItemStatus))
                {
                    status = -1;
                }
                else if (ItemStatus == "true")
                {
                    status = 0;
                }
                var mListe = (new PrixJournalier()).fnSelect(startdate, enddate, -1, siteID);

                // Paging
                //int start = parameters.Start;

                //int limit = parameters.Limit;

                //if ((start + limit) > mListe.Count)
                //{
                //    limit = mListe.Count - start;
                //}

                //List<DataPersist> rangePlants = (start < 0 || limit < 0) ? mListe : mListe.GetRange(start, limit);
                //return this.Store(new Paging<DataPersist>(rangePlants, mListe.Count));
                return this.Store(mListe);
            }

        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("DailyPriceCriteriaPanel");
            //mform.FieldDefaults.ReadOnly = true;
            mform.ToggleCollapse();            
            //mform.Collapsed = false;
            return this.Direct();
        }

        public ActionResult onRefresh(string ItemPeriodStart, string ItemPeriodEnd, string ItemSite)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeDailyPrice");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemSite"   ,ItemSite),
                                new Ext.Net.Parameter("ItemPeriodStart"     ,ItemPeriodStart),
                                new Ext.Net.Parameter("ItemPeriodEnd"           ,ItemPeriodEnd)
                            });
                FormPanel mform = X.GetCmp<FormPanel>("DailyPriceCriteriaPanel");

                mform.Collapsed = true;
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prix Journalier : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });

            }
            
            return this.Direct();
        }

        public ActionResult onApprove(string ItemSelected)
        {
            try
            {
                PrixJournalier mClass = JSON.Deserialize<PrixJournalier>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = mClass.fnGet(mClass.ID);

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("onApproveDailyPrice :Prix Journalier Approve failed.");

                result = mClass.fnApprove();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeDailyPrice");

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
                    Title = "Prix Journalier : Approuver",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }
                
        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                PrixJournalier mClass = JSON.Deserialize<PrixJournalier>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = mClass.fnGet(mClass.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Prix Journalier loading failed.");

                if (mClass.Desactive)
                    result = mClass.fnActivate();
                else
                    result = mClass.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Prix Journalier Approval failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeDailyPrice");

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
                    Title = "Prix Journalier : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        private PrixJournalier MapFormToObject(PrixJournalier mClass)
        {
            mClass.DatePrix = DateTime.Parse(X.GetCmp<DateField>("TxtEntryDate").RawText.ToString());                        
            mClass.DateDebut = DateTime.Parse(X.GetCmp<DateField>("TxtStartDate").RawText.ToString());
            mClass.DateEcheance = DateTime.Parse(X.GetCmp<DateField>("TxtDueDate").RawText.ToString()).AddHours(23).AddMinutes(59);

            mClass.Campagne = new Campagne();
            mClass.Campagne.Designation = X.GetCmp<TextField>("currentcampagneCP").Text;
            //mClass.Statut = "0";
            string prix = string.Empty;
            //if (X.GetCmp<TextField>("TxtPrice").Text.Contains(" "))
            //    prix = X.GetCmp<TextField>("TxtPrice").Text.Replace(" ", "");
            
            mClass.Prix = decimal.Parse(X.GetCmp<TextField>("TxtPrice").Text);
            
            mClass.Numero = X.GetCmp<Hidden>("hiddenNumeroPrice").Text;

            mClass.Site = new Site();
            mClass.Site.ID = int.Parse(X.GetCmp<Hidden>("txtSiteID").Text);
            mClass.Site.Nom = X.GetCmp<TextField>("txtSiteNom").Text;
            
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }
        
        private void MapObjectToForm(PrixJournalier mClass)
        {
            X.GetCmp<TextField>("TxtDailyPriceID").Text = mClass.ID.ToString();
            X.GetCmp<TextField>("TxtEntryDate").Text = mClass.DatePrix.ToString();
            X.GetCmp<TextField>("TxtStartDate").Text = mClass.DateDebut.ToString();
            X.GetCmp<TextField>("TxtDueDate").Text = mClass.DateEcheance.ToString();
            X.GetCmp<TextField>("TxtPrice").Text = mClass.Prix.ToString();
            X.GetCmp<TextField>("hiddenNumeroPrice").Text = mClass.Numero.ToString();
            //X.GetCmp<ComboBox>("LocationID").SetValue(mClass.Site.ID.ToString());


        }
        

        #region Method
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
                    else if (hidAction.ToString().Equals(EXTEND))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Extend;
                }
                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
            }
            catch (Exception ex)
            {
                Ext.Net.X.Msg.Alert("frm_Detail : GetFormExecMode", ex.Message).Show();
                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
            }
        }

        private int GetCritriaValue(string strComponent)
        {
            int value;

            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
                return value;
            else
                return -1;
        }

        private int GetCritriasValue(string strComponent)
        {
            if (X.GetCmp<ComboBox>(strComponent) != null && X.GetCmp<ComboBox>(strComponent).SelectedItem != null && X.GetCmp<ComboBox>(strComponent).SelectedItem.Value != null)
                return int.Parse(X.GetCmp<ComboBox>(strComponent).SelectedItem.Value.ToString());
            else
                return -1;
        }

        private void DeselectGridRows()
        {
            //X.Js.Call("App.rowSelectionListe.deselectAll();");
            X.GetCmp<RowSelectionModel>("rowSelectionDailyPrice").DeselectAll();
        }

        public void CreateIconsList()
        {
            try
            {
                //Ext.Net.ResourceManager.RegisterGlobalIcon(Icon.PageCancel);

                //string url =  Ext.Net.ResourceManager.GetInstance().GetIconUrl(Icon.Tick);
            }
            catch (Exception ex)
            {

                X.Msg.Alert("frmLBC_ReceptionInDistrict_Overview : CreateIconList", ex.Message).Show();
            }
        }
        #endregion

        public ActionResult ExportExcel(string ItemList)
        {
            //string json = X.GetCmp<Hidden>("hiddenListe").Value.ToString();
            XslCompiledTransform xt = new XslCompiledTransform();
            //var submitData = new Ext.Net.SubmitHandler(data);
            StoreSubmitDataEventArgs submitData = new StoreSubmitDataEventArgs(ItemList, null);
            XmlNode xml = submitData.Xml;

            this.Response.Clear();
            this.Response.ContentType = "application/vnd.ms-excel";
            
            this.Response.AddHeader("Content-Disposition", "attachment; filename=submittedData.xls");            
            xt.Load(Server.MapPath("~/Resources/Excel.xsl"));                        
            xt.Transform(xml, null, this.Response.OutputStream);            
            this.Response.End();
            return this.Direct();
        }

        public ActionResult ToExcel(string data)
        {
            //string json = hiddenListe.Value.ToString();
            StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(data, null);
            XmlNode xml = eSubmit.Xml;            

            this.Response.Clear();
            this.Response.ContentType = "application/vnd.ms-excel";
            this.Response.AddHeader("Content-Disposition", "attachment; filename=submittedData.xls");

            XslCompiledTransform xtExcel = new XslCompiledTransform();

            xtExcel.Load(Server.MapPath("../Resources/Excel.xsl"));
            xtExcel.Transform(xml, null, this.Response.OutputStream);
            this.Response.End();
            return this.Direct();
        }

        public ActionResult OnDisplayDailyPriceList()
        {
            string UserName = (string)Session["userName"];

            Site mSiteParDefaut = new Site();
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            Parametres mParam = new Parametres(0);

            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;
            if (mParam.Site == mSiteParDefaut.ID) ViewData["UrlSite"] = "LoadSiteAll";
            else ViewData["UrlSite"] = "LoadSiteByAccess";

            ViewData["Titre"] = "Print List of Reference Prices";
            ViewData["actionToDo"] = "OnPrintDailyPriceList";
            ViewData["ControllerName"] = "DailyPrice";
            ViewData["SiteParDefaut"] = 1;
            ViewData["UrlSite"] = "LoadSiteByAccess";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmPeriodForReport", ViewData = ViewData };

        }

        [HttpPost]
        public ActionResult OnPrintDailyPriceList(string startDate, string endDate)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);



                Session["paramSite"] = int.Parse(GetFormValue("RPcmbSite"));
                Session["paramSiteText"] = X.GetCmp<ComboBox>("RPcmbSite").SelectedItem.Text;

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/DailyPrice/ViewReportResult', this, 'List Of Reference Prices',''),App.frmPeriodForReport.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prix Journalier : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }


        }

        public ActionResult ViewReportResult()
        {
            //XtraReport report = null;

            rptDailyPriceList report = new rptDailyPriceList();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramSite"].Value = Session["paramSite"];
            report.Parameters["paramSiteText"].Value = Session["paramSiteText"];
            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];

            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }


    }
}