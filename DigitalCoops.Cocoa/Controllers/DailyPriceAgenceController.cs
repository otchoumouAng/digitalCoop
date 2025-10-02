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
    public class DailyPriceAgenceController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        //public DailyPriceAgenceController()
        //{
        //    string cultureName = "en";
        //    Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
        //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
        //}

        // GET: DailyPriceAgence
        public ActionResult Index()
        {
            var mParam = (new Parametres()).fnSelect();

            Parametres mclass = new Parametres();
            mclass = mParam[0] as Parametres;

            Campagne mCampagne = new Campagne();
           
            X.GetCmp<ComboBox>("cmbCropYear").SetValue(mclass.Campagne);

            Site mSiteParDefaut = new Site();
            string UserName = (string)Session["userName"];
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            DateTime firstdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            X.GetCmp<DateField>("TxtPeriodStart").RawText = firstdayofmonth.ToShortDateString();
            X.GetCmp<DateField>("TxtPeriodEnd").RawText = DateTime.Now.ToShortDateString();

            X.GetCmp<FormPanel>("DailyPriceAgenceCriteriaPanel").SetTitle("Campagne : " + mclass.Campagne + " | Du : " + firstdayofmonth.ToShortDateString() + " Au : " + DateTime.Now.ToShortDateString());

            #region Set Function's Access
            
            Fonction HasAccess = new Fonction();
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{2E59D500-3B5F-4A6B-BFE3-4ED066891B41}", UserName);
            //if (HasAccess.fnGetUserAccessStatus("{ED9269E8-B69D-400F-B2E8-9F8A3C89980A}", UserName) == false)
            //    X.GetCmp<Button>("btnNewDailyPriceAgence").Disable();
            //else
            //    X.GetCmp<Button>("btnNewDailyPriceAgence").Enable();

            //if (HasAccess.fnGetUserAccessStatus("{A6EC5B0D-09B7-4756-A42B-933CC025C397}", UserName) == false)
            //    X.GetCmp<MenuItem>("mnuExportDailyPriceAgence").Disable();
            //else
            //    X.GetCmp<MenuItem>("mnuExportDailyPriceAgence").Enable();

            //if (HasAccess.fnGetUserAccessStatus("{ED16542D-D164-4B0C-8559-5F9007906D91}", UserName) == false)
            //    X.GetCmp<MenuItem>("mnuPrintDailyPriceAgenceList").Disable();
            //else
            //    X.GetCmp<MenuItem>("mnuPrintDailyPriceAgenceList").Enable();

            //X.GetCmp<Hidden>("DPAhiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{ED9269E8-B69D-400F-B2E8-9F8A3C89980A}", UserName));
            //X.GetCmp<Hidden>("DPAhiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{5CCF8ACA-DC71-4595-BF11-3703CDD28D20}", UserName));
            //X.GetCmp<Hidden>("DPAhiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{151E608A-1679-4F8E-8B31-17DFF79857DA}", UserName));
            //X.GetCmp<Hidden>("DPAhiddenPermActiver").SetValue(HasAccess.fnGetUserAccessStatus("{96E74DA0-EDF9-4085-ACAF-BF25C4A24CBB}", UserName));
            X.GetCmp<Hidden>("DPAhiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{A6EC5B0D-09B7-4756-A42B-933CC025C397}", UserName));
            //X.GetCmp<Hidden>("DPAhiddenPermExtend").SetValue(HasAccess.fnGetUserAccessStatus("{A6EC5B0D-09B7-4756-A42B-933CC025C397}", UserName));
            X.GetCmp<Hidden>("DPAhiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{B1DF7602-5176-4FFB-B10C-BD2054BD93A5}", UserName));

            #endregion


            return View();
        }

        public ActionResult onAdd()
        {
            PrixJournalierAgenceViewModel mclass = new PrixJournalierAgenceViewModel();

            mclass._PrixJournalierAgence = new PrixJournalierAgence();
            mclass._PrixJournalierAgence.Campagne = new Campagne();
            mclass._PrixJournalierAgence.Site = new Site();

            string UserName = (string)Session["userName"];

            Fonction HasAccessFunction = new Fonction();
            bool HasAccessAllSite = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            

            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            var mParam = (new Parametres()).fnSelect();
            Parametres parametre = new Parametres();
            parametre = mParam[0] as Parametres;


            mclass._PrixJournalierAgence.Campagne.Designation = parametre.Campagne;

            if (result)
            {
                mclass._PrixJournalierAgence.Site.ID = mSiteParDefaut.ID;
                mclass._PrixJournalierAgence.Site.Nom = mSiteParDefaut.Nom;
            }

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDailyPriceAgence", Model = mclass, ViewData = ViewData };

        }

        public ActionResult onEdit(string ItemSelected)
        {            
            PrixJournalierAgence mclass = JSON.Deserialize<PrixJournalierAgence>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            //MapObjectToForm(mclass);          

            PrixJournalierAgenceViewModel viewModel = new PrixJournalierAgenceViewModel();

            viewModel._PrixJournalierAgence = mclass;
            viewModel._Parametres = new Parametres();
            viewModel._Parametres.fnGet();
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDailyPriceAgence", Model = viewModel };
        }

        public ActionResult onConsult(string ItemSelected)
        {            
            PrixJournalierAgence mclass = JSON.Deserialize<PrixJournalierAgence>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            //MapObjectToForm(mclass);          

            PrixJournalierAgenceViewModel viewModel = new PrixJournalierAgenceViewModel();

            viewModel._PrixJournalierAgence = mclass;
            viewModel._Parametres = new Parametres();
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDailyPriceAgence", Model = viewModel };
        }

        public ActionResult onExtend(string ItemSelected)
        {            
            PrixJournalierAgence mclass = JSON.Deserialize<PrixJournalierAgence>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });   

            PrixJournalierAgenceViewModel viewModel = new PrixJournalierAgenceViewModel();

            viewModel._PrixJournalierAgence = mclass;
            viewModel._Parametres = new Parametres();
            viewModel._Parametres.fnGet();
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Extend;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDailyPriceAgence", Model = viewModel };
        }

        [HttpPost]
        public ActionResult SubmitFormMethodOld(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();
            try
            {
                List<PrixJournalierAgence> mListPrix = new List<PrixJournalierAgence>();
                PrixJournalierAgence mClass = new PrixJournalierAgence();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtDailyPriceAgenceID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Prix Journalier load failed.");
                }

                bool resultsiteprice = true;
                mClass = MapFormToObject(mClass);

                //bool result = mClass.fnUpdate();
                _db = mClass.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                    List<Site> ItemSite = JSON.Deserialize<List<Site>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (ItemSite.Count > 0)
                    {
                        
                        for (int i = 0; i < ItemSite.Count(); i++)
                        {
                            mClass = new PrixJournalierAgence();
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
                            mClass = new PrixJournalierAgence();
                        }
                    }

                    if (!resultsiteprice)
                    {
                        _db.RollBackTransaction(mtran);
                    }
                    _db.CommitTransaction(mtran);

                if (resultsiteprice)
                {
                    Store mstore = X.GetCmp<Store>("storeListeDailyPrice");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        foreach (PrixJournalierAgence item in mListPrix)
                        {
                            mstore.Insert(0, item);
                            X.GetCmp<RowSelectionModel>("rowSelectionDailyPriceAgence").Select(0);
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
                
                X.GetCmp<Window>("FormDailyPriceAgence").Close();               
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
                List<PrixJournalierAgence> mListPrix = new List<PrixJournalierAgence>();
                PrixJournalierAgence mClass = new PrixJournalierAgence();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtDailyPriceAgenceID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Prix Journalier load failed.");
                }

                bool resultsiteprice = false;
                //mClass = MapFormToObject(mClass);

                //bool result = mClass.fnUpdate();
                _db = mClass.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                //resultsiteprice = mClass.fnUpdate();
                //mListPrix.Add(mClass);

                List<Site> ItemSite = JSON.Deserialize<List<Site>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                if (ItemSite.Count > 0)
                {

                    for (int i = 0; i < ItemSite.Count(); i++)
                    {
                        mClass = new PrixJournalierAgence();
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
                        mClass = new PrixJournalierAgence();
                    }
                }
                else
                {
                    throw new Exception("SubmitFormMethod : Please select at least one Site.");
                }

                if (!resultsiteprice)
                {
                    _db.RollBackTransaction(mtran);
                }

                _db.CommitTransaction(mtran);

                if (resultsiteprice)
                {
                    Store mstore = X.GetCmp<Store>("storeListeDailyPrice");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        foreach (PrixJournalierAgence item in mListPrix)
                        {
                            mstore.Insert(0, item);
                            X.GetCmp<RowSelectionModel>("rowSelectionDailyPriceAgence").Select(0);
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

                X.GetCmp<Window>("FormDailyPriceAgence").Close();
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
        public ActionResult UpdateFormMethod()
        {            
            try
            {
               
                PrixJournalierAgence mClass = new PrixJournalierAgence();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                mClass.IsNew = false;

                mClass.fnGet(Guid.Parse(GetFormValue("TxtDailyPriceAgenceID")));

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("SubmitFormMethod : Prix Journalier load failed.");

                bool resultsiteprice = false;
                mClass = MapFormToObject(mClass);

                resultsiteprice = mClass.fnUpdate();                               

                if (resultsiteprice)
                {
                    Store mstore = X.GetCmp<Store>("storeListeDailyPrice");
                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();

                }

                X.GetCmp<Window>("FormDailyPriceAgence").Close();
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
                PrixJournalierAgence mClass = new PrixJournalierAgence();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode != Tms.Components.Settings.EnumsDefinition.eExecMode.Extend)
                    throw new Exception("SubmitFormMethod : Prix Journalier load failed.");
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtDailyPriceAgenceID")));

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
                        X.GetCmp<RowSelectionModel>("rowSelectionDailyPriceAgence").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormDailyPriceAgence").Close();                   
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


        public ActionResult Select(StoreRequestParameters parameters, string ItemCropYear, string ItemSite, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            if (string.IsNullOrEmpty(ItemPeriodStart) && string.IsNullOrEmpty(ItemPeriodEnd) && string.IsNullOrEmpty(ItemStatus))
            {
                return this.Store(null);
            }
            else
            {
                string CropYearID = string.IsNullOrEmpty(ItemCropYear) ? "{Tous}" : ItemCropYear;
                if (!string.IsNullOrEmpty(ItemCropYear) && ItemCropYear.Contains("null"))
                {
                    CropYearID = "{Tous}";
                }

                int siteid = GetCritriaValue(ItemSite);
                int status = -1;

                if (string.IsNullOrEmpty(ItemStatus))
                {
                    status = -1;
                }
                else if (ItemStatus == "true")
                {
                    status = 0;
                }

                DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
                DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
                
                var mListe = (new PrixJournalierAgence()).fnSelect(CropYearID, siteid, startdate, enddate, status);

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
            FormPanel mform = X.GetCmp<FormPanel>("DailyPriceAgenceCriteriaPanel");
            //mform.FieldDefaults.ReadOnly = true;
            mform.ToggleCollapse();
            //mform.Collapsed = false;
            return this.Direct();
        }

        public ActionResult onRefresh(string ItemCropYear, string ItemSite, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeDailyPrice");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemCropYear", ItemCropYear),
                                new Ext.Net.Parameter("ItemSite"   ,ItemSite),
                                new Ext.Net.Parameter("ItemPeriodStart"     ,ItemPeriodStart),
                                new Ext.Net.Parameter("ItemPeriodEnd"           ,ItemPeriodEnd),
                                new Ext.Net.Parameter("ItemStatus"           ,ItemStatus)
                            });
                FormPanel mform = X.GetCmp<FormPanel>("DailyPriceAgenceCriteriaPanel");

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
                PrixJournalierAgence mClass = JSON.Deserialize<PrixJournalierAgence>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = mClass.fnGet(mClass.ID);

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("onApproveDailyPriceAgence :Prix Journalier Approve failed.");

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

        public ActionResult AddSiteToPrice(string ItemPrice)
        {
            Guid priceID = !string.IsNullOrEmpty(ItemPrice) ? Guid.Parse(ItemPrice) : Guid.Empty;

            PrixJournalierAgence price = new PrixJournalierAgence();
            price.ID = priceID;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormListePriceSites", Model = price };
        }

        //public ActionResult AddSiteToPrice(int ItemSite)
        //{

        //    PrixJournalierAgence price = new PrixJournalierAgence();
        //    price.Site.ID = ItemSite;

        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "FormListePriceSites", Model = price };
        //}

        public ActionResult RemoveSiteToPrice(string ItemSelected)
        {
            try
            {

                SitePrice siteprice = JSON.Deserialize<SitePrice>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Store mstore = X.GetCmp<Store>("storeListeSitePrice");



                if (siteprice.IsNew)
                {
                    ModelProxy _proxy = mstore.GetById(siteprice.ID);
                    _proxy.Drop();

                    return this.Direct();
                }

                siteprice.fnGet(siteprice.ID);

                if (siteprice == null || siteprice.ID == Guid.Empty)
                    throw new Exception("RemoveSiteToPrice : Retirer Site failed.");

                bool result = siteprice.fnRemove();

                if (result)
                {

                    ModelProxy mProxy = mstore.GetById(siteprice.ID);

                    mProxy.Drop();

                    mProxy.Set(siteprice);

                    mProxy.Commit();

                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prix Journalier : Retirer Site",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SelectSite(string ItemPrice, string ItemExecMode)
        {
            if (!string.IsNullOrEmpty(ItemExecMode) && (ItemExecMode != Tms.Components.Settings.EnumsDefinition.ADD_NEW))
            {
                Guid id = Guid.Empty;
                if (!string.IsNullOrEmpty(ItemPrice))
                {
                    id = Guid.Parse(ItemPrice);
                }
                var listeSite = new SitePrice().fnSelect(id);
                return this.Store(listeSite);
            }
            else
            {
                return this.Direct();
            }

        }

        public ActionResult SubmitListeSite(string ItemSelected)
        {
            List<Site> sites = JSON.Deserialize<List<Site>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            Store store = X.GetCmp<Store>("storeListeSitePrice");

            foreach (var item in sites)
            {
                item.IsNew = true;
                item.Desactive = true;
                store.Insert(0, item);
                X.GetCmp<RowSelectionModel>("rowSelectionSitePrice").Select(0);
            }

            X.GetCmp<Window>("FormListePriceSites").Close();
            return this.Direct();
        }


        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                PrixJournalierAgence mClass = JSON.Deserialize<PrixJournalierAgence>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

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

        private PrixJournalierAgence MapFormToObject(PrixJournalierAgence mClass)
        {
            mClass.DatePrix = DateTime.Parse(X.GetCmp<DateField>("TxtEntryDate").RawText.ToString());
            mClass.DateDebut = DateTime.Parse(X.GetCmp<DateField>("TxtStartDate").RawText.ToString());
            mClass.DateEcheance = DateTime.Parse(X.GetCmp<DateField>("TxtDueDate").RawText.ToString()).AddHours(23).AddMinutes(59);
            //mClass.Statut = "0";
            string prix = string.Empty;
            //if (X.GetCmp<TextField>("TxtPrice").Text.Contains(" "))
            //    prix = X.GetCmp<TextField>("TxtPrice").Text.Replace(" ", "");

            mClass.Prix = decimal.Parse(X.GetCmp<TextField>("TxtPrice").Text);

            mClass.Numero = X.GetCmp<Hidden>("hiddenNumeroPrice").Text;

            mClass.Campagne = new Campagne();
            mClass.Campagne.Designation = GetFormValue("CropYearID");

            mClass.Site = new Site();
            mClass.Site.ID = int.Parse(GetFormValue("txtSiteID"));
            mClass.Site.Nom = X.GetCmp<TextField>("txtSiteNom").Text;

            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            mClass.Commentaire = X.GetCmp<Hidden>("TxtCommentaire").Text;

            return mClass;
        }

        private void MapObjectToForm(PrixJournalierAgence mClass)
        {
            X.GetCmp<TextField>("TxtDailyPriceAgenceID").Text = mClass.ID.ToString();
            X.GetCmp<TextField>("TxtEntryDate").Text = mClass.DatePrix.ToString();
            X.GetCmp<TextField>("TxtStartDate").Text = mClass.DateDebut.ToString();
            X.GetCmp<TextField>("TxtDueDate").Text = mClass.DateEcheance.ToString();
            X.GetCmp<TextField>("TxtPrice").Text = mClass.Prix.ToString();
            X.GetCmp<TextField>("hiddenNumeroPrice").Text = mClass.Numero.ToString();
            X.GetCmp<ComboBox>("cmbSite").SetValue(mClass.Site.ID.ToString());


        }

        //public ActionResult AddSiteToPrice(string ItemPrice)
        //{
        //    Guid priceID = !string.IsNullOrEmpty(ItemPrice) ? Guid.Parse(ItemPrice) : Guid.Empty;

        //    PrixJournalierAgence price = new PrixJournalierAgence();
        //    price.ID = priceID;

        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "FormListePriceSites", Model = price };
        //}

        //public ActionResult RemoveSiteToPrice(string ItemSelected)
        //{
        //    try
        //    {

        //        List<Site> fonctions = JSON.Deserialize<List<Site>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
        //        Site mClass;

        //        if (fonctions.Count() > 0)
        //        {
        //            Store mstore = X.GetCmp<Store>("storeListePriceSite");

        //            foreach (var item in fonctions)
        //            {
        //                if (item.IsNew)
        //                {
        //                    ModelProxy _proxy = mstore.GetById(item.ID);
        //                    _proxy.Drop();

        //                    return this.Direct();
        //                }
        //                else
        //                {
        //                    mClass = new Site();
        //                    mClass.fnGet(item.ID);
        //                    if (mClass == null) // || mClass.ID == Guid.Empty)
        //                        throw new Exception("RemoveSiteToPrice : Retirer Site failed.");

        //                    mClass.UtilisateurModification = (string)Session["userName"];

        //                    //bool result = mClass.fnRemove();

        //                    //if (result)
        //                    //{

        //                        ModelProxy mProxy = mstore.GetById(mClass.ID);

        //                        mProxy.Drop();

        //                        mProxy.Set(mClass);

        //                        mProxy.Commit();

        //                    //}
        //                }

        //            }
        //        }




        //        //if (rolefonction.IsNew)
        //        //{
        //        //    ModelProxy _proxy = mstore.GetById(rolefonction.ID);
        //        //    _proxy.Drop();

        //        //    return this.Direct();
        //        //}

        //        //fonctions.fnGet(fonctions.ID);



        //    }
        //    catch (Exception ex)
        //    {
        //        X.MessageBox.Show(new MessageBoxConfig
        //        {
        //            Title = "Role : Retirer function",
        //            Message = ex.Message,
        //            Buttons = MessageBox.Button.OK,
        //            Icon = MessageBox.Icon.WARNING
        //        });
        //    }

        //    return this.Direct();
        //}

        //public ActionResult SelectSite(string ItemPrice)
        //{
        //    int id = 1;
        //    if (!string.IsNullOrEmpty(ItemPrice))
        //    {
        //        id = Int32.Parse(ItemPrice);
        //    }
        //    var listePrice = new PrixJournalierAgence().fnSelectBySite(id);
        //    return this.Store(listePrice);
        //}

        //public ActionResult SubmitListeSites(string ItemSelected)
        //{
        //    List<Site> fonctions = JSON.Deserialize<List<Site>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

        //    Store store = X.GetCmp<Store>("storeListePriceSite");

        //    foreach (var item in fonctions)
        //    {
        //        item.IsNew = true;
        //        item.IsNewInList = true;
        //        store.Insert(0, item);
        //        X.GetCmp<RowSelectionModel>("rowSelectionPriceSite").Select(0);
        //    }

        //    X.GetCmp<Window>("FormListePriceSites").Close();
        //    return this.Direct();
        //}

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
            X.GetCmp<RowSelectionModel>("rowSelectionDailyPriceAgence").DeselectAll();
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

        public ActionResult OnDisplayDailyPriceAgenceList()
        {

            ViewData["Titre"] = "Print List of Daily Prices";
            ViewData["actionToDo"] = "OnPrintDailyPriceAgenceList";
            ViewData["ControllerName"] = "DailyPriceAgence";
            ViewData["SiteParDefaut"] = 1;
            ViewData["UrlSite"] = "LoadSiteByAccess";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmPeriodForReport", ViewData = ViewData };

        }

        public ActionResult OnPrintDailyPriceAgenceList(string cropyear, string startDate, string endDate)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                Session["paramCampagne"] = cropyear;
                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/DailyPriceAgence/ViewReportResult', this, 'List Of Daily Prices',''),App.frmPeriodForReport.doClose()", Guid.NewGuid(), BaseUrl));
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

            report.Parameters["paramCampagneText"].Value = Session["paramCampagne"];
            if (!string.IsNullOrEmpty(Session["paramCampagne"].ToString()) && Session["paramCampagne"].ToString() == "{Tous}")
                report.Parameters["paramCampagne"].Value = string.Empty;
            else
                report.Parameters["paramCampagne"].Value = Session["paramCampagne"];

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];

            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }


    }
}