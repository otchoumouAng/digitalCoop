using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ext.Net.MVC;
using Ext.Net;
using Tms.Classes.Business;
using Newtonsoft.Json;
using Tms.Classes.Shared;
using Tms.Components.Settings;
using Tms.Components.Data;
using Tms.Classes.Security;
using System.Globalization;

namespace Tms2017.MVC.Controllers
{
    public class RemboursementDirectController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";

        Guid mID;

        // GET: RemboursementDirect
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();

            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            X.GetCmp<ComboBox>("cmbCampagne").SetValue(mParam.Campagne);

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("dtpStartDate").RawText = StartDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Fournisseur = "{Tous}";

            string Type = "{Tous}";

            X.GetCmp<FormPanel>("CriteriaPanelRD").SetTitle("Site : " + mSiteParDefaut.Nom + ", Campagne : " + mParam.Campagne + ", Fournisseur : " + Fournisseur + ", Type : " + Type + ", Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access
            
            Fonction HasAccess = new Fonction();
            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{a3231ab8-fa71-440c-9756-7ed9b59b6ffe}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{E29E0D11-D962-4464-9F96-A2DC793BA4ED}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{5558C752-2BEA-4565-88C3-7C91DAA8AA49}")))
                X.GetCmp<MenuItem>("mnuExportReimbList").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportReimbList").Disable();

            X.GetCmp<Hidden>("RdhiddenPermCreer").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{E29E0D11-D962-4464-9F96-A2DC793BA4ED}")));
            X.GetCmp<Hidden>("RdhiddenPermModifier").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{3EDBBF9A-76FA-4ECC-80F6-4DE2FB4BD2A9}")));
            X.GetCmp<Hidden>("RdhiddenPermApprove").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{A1B67A2C-A5D1-432F-9CDD-3B1D22620285}")));
            X.GetCmp<Hidden>("RdhiddenPermDesactiver").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{0C2DC2B1-6C5C-43F7-B7C5-B9BA542BE8F5}")));
            //X.GetCmp<Hidden>("RdhiddenPermPrintReimbList").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{989FA360-ADD9-412D-B1B6-2FF988B403A9}")));
            X.GetCmp<Hidden>("RdhiddenPermExporterExcel").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{5558C752-2BEA-4565-88C3-7C91DAA8AA49}")));
            X.GetCmp<Hidden>("RdhiddenPermOverview").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{4BE54964-5881-4720-99C6-326758694690}")));
            X.GetCmp<Hidden>("RdhiddenPermDesactiverSup").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{2c37a416-a3a9-4f69-a55d-3b3251eda68d}")));
            #endregion

            return View();
        }
        
        public ActionResult onAdd()
        {
            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            RemboursementDirectViewModel mclass = new RemboursementDirectViewModel();

            mclass._RemboursementDirect = new RemboursementDirect();
            mclass._RemboursementDirect.Sites = new Site();
            mclass._RemboursementDirect.Campagne = new Parametres(0).Campagne;

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            if (result)
            {
                mclass._RemboursementDirect.Sites = new Site();
                mclass._RemboursementDirect.Sites.ID = mSiteParDefaut.ID;
                mclass._RemboursementDirect.Sites.Nom = mSiteParDefaut.Nom;
            }

            return new Ext.Net.MVC.PartialViewResult { ViewName = "RemboursementDirect_Detail", Model = mclass, };
        }
        public ActionResult onEdit(string ItemSelected)
        {
            RemboursementDirect mclass = JSON.Deserialize<RemboursementDirect>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            RemboursementDirectViewModel viewModel = new RemboursementDirectViewModel();

            viewModel._RemboursementDirect = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "RemboursementDirect_Detail", Model = viewModel };
        }

        public ActionResult onConsult(string ItemSelected)
        {           
            RemboursementDirect mclass = JSON.Deserialize<RemboursementDirect>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            RemboursementDirectViewModel viewmodel = new RemboursementDirectViewModel();
            viewmodel._RemboursementDirect = mclass;
            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "RemboursementDirect_Detail", Model = viewmodel, };
        }


        public ActionResult OnCancel(string ItemSelected)
        {

            try
            {
                RemboursementDirect mClass = JSON.Deserialize<RemboursementDirect>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);

                if (!result)
                    throw new Exception("OnCancel : Direct repayment loading failed.");

                mClass.UtilisateurModification = (string)Session["userName"];

                if (mClass.fnCancel())
                {
                    Store mstore = X.GetCmp<Store>("storeListeRemboursementDirect");

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
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Direct Repayment : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }        

        public ActionResult OnApprove(string ItemSelected)
        {

            try
            {
                RemboursementDirect mClass = JSON.Deserialize<RemboursementDirect>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);

                if (!result)
                    throw new Exception("OnApprove : Direct repayment loading failed.");

                mClass.UtilisateurApprobation = (string)Session["userName"];

                if (mClass.fnApprove())
                {
                    Store mstore = X.GetCmp<Store>("storeListeRemboursementDirect");

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
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Direct repayment : Approuver",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }       

        [HttpPost]
        public ActionResult UpdateFormMethod(string ListeOfFinancing, string ListeOfMec)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                RemboursementDirect mRemboursementDirect = new RemboursementDirect();
                RemboursementDirectFinancement mRemboursementDirectFinancement = null;
                RemboursementDirectMEC mRemboursementDirectMec = null;

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);
                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mRemboursementDirect.IsNew = true;
                else
                {
                    mRemboursementDirect.IsNew = false;

                    mRemboursementDirect.fnGet(Guid.Parse(X.GetCmp<Hidden>("hiddenID").Text));

                    if (mRemboursementDirect == null || mRemboursementDirect.ID == Guid.Empty)
                        throw new Exception("UpdateFormMethod : Direct Repayment load failed.");
                }

                bool result = false;
                bool resultFinancement = true;
                bool resultMec = true;

                mRemboursementDirect = MapFormToObject(mRemboursementDirect);

                _db = mRemboursementDirect.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = mRemboursementDirect.fnUpdate(mtran);

                if (result)
                {
                    List<RemboursementDirectFinancement> ItemFinancement = JSON.Deserialize<List<RemboursementDirectFinancement>>(ListeOfFinancing, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (ItemFinancement.Count > 0)
                    {
                        for (int i = 0; i < ItemFinancement.Count(); i++)
                        {
                            mRemboursementDirectFinancement = new RemboursementDirectFinancement();
                            mRemboursementDirectFinancement.ReboursementDirect = new RemboursementDirect();
                            mRemboursementDirectFinancement.SetDataSource(_db);

                            mRemboursementDirectFinancement.ReboursementDirect.ID = mRemboursementDirect.ID;
                            mRemboursementDirectFinancement.Financement = new Financement();
                            mRemboursementDirectFinancement.Financement.ID = ItemFinancement[i].Financement.ID;
                            mRemboursementDirectFinancement.SoldeEnCours = ItemFinancement[i].SoldeEnCours;
                            mRemboursementDirectFinancement.Montant = ItemFinancement[i].Montant;
                            mRemboursementDirectFinancement.UtilisateurCreation = (string)Session["userName"];

                            resultFinancement = mRemboursementDirectFinancement.fnUpdate(mtran);

                            if (!resultFinancement) break;
                        }
                    }

                    List<RemboursementDirectMEC> ItemMec = JSON.Deserialize<List<RemboursementDirectMEC>>(ListeOfMec, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (ItemMec.Count > 0)
                    {
                        for (int i = 0; i < ItemMec.Count(); i++)
                        {
                            mRemboursementDirectMec = new RemboursementDirectMEC();
                            mRemboursementDirectMec.ReboursementDirect = new RemboursementDirect();
                            mRemboursementDirectMec.SetDataSource(_db);

                            mRemboursementDirectMec.ReboursementDirect.ID = mRemboursementDirect.ID;
                            mRemboursementDirectMec.MiseEnCompte = new MiseEnCompte();
                            mRemboursementDirectMec.MiseEnCompte.ID = ItemMec[i].MiseEnCompte.ID;
                            mRemboursementDirectMec.SoldeEnCours = ItemMec[i].SoldeEnCours;
                            mRemboursementDirectMec.Montant = ItemMec[i].Montant;
                            mRemboursementDirectMec.UtilisateurCreation = (string)Session["userName"];

                            resultMec = mRemboursementDirectMec.fnUpdate(mtran);

                            if (!resultMec) break;
                        }
                    }

                    if (!resultFinancement || !resultMec)
                    {
                        _db.RollBackTransaction(mtran);
                    }
                    _db.CommitTransaction(mtran);

                    //Store mstore = X.GetCmp<Store>("storeListeRemboursementDirect");
                    //mstore.Insert(0, mRemboursementDirect);
                    //X.GetCmp<RowSelectionModel>("rowSelectionListeRD").Select(0);

                    Store mStore = X.GetCmp<Store>("storeListeRemboursementDirect");

                    ModelProxy mProxy;
                    
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mRemboursementDirect);
                        X.GetCmp<RowSelectionModel>("rowSelectionListeRD").Select(0);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mRemboursementDirect.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mRemboursementDirect);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("RemboursementDirect_Detail").Close();

                }


            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Direct Repayment : UpdateForm",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();

        }

        public ActionResult OnPrintRemboursementDirect(string IdRemboursementDirect, string IsCopy)
        {
            bool ReportIscopy = false;
            if (string.IsNullOrEmpty(IsCopy))
                ReportIscopy = true;
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'RemboursementDirect{0}', '{1}/RemboursementDirect/ViewReport?id={0}&IsCopy={2}', this, 'RemboursementDirect','')", IdRemboursementDirect, BaseUrl, ReportIscopy));
        }

        public ActionResult ViewReport(string id, bool IsCopy)
        {
            try
            {
                rptRemboursementDirect report = new rptRemboursementDirect();

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["ID"].Value = id;
                report.Parameters["IsCopy"].Value = IsCopy;

                ViewData["Report"] = report;
                //PrintMethod.Print(report);
                return View("ViewReportResult");
            }
            catch (Exception ex)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Remboursement Direct : Printing",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }

        }

        public ActionResult LoadListOfRepayment(StoreRequestParameters parameters, string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite)
        {

            int FournisseurID = GetCriteriaValue(ItemFournisseur);
            int TypeID = GetCriteriaValue(ItemType);
            int siteID = GetCriteriaValue(ItemSite);
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "" : ItemCampagne;

            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string Status = ItemStatus;

            var mListe = (new RemboursementDirect()).fnSelect(Campagne, FournisseurID, TypeID, StartDate, EndDate, Status, siteID);
            //var paging = GridStorePaging.SetRangePlants(parameters, mListe);

            return this.Store(mListe);
        }

        public ActionResult LoadListOfFinancing(string ItemRemboursementDirect, string ItemExecMode)
        {
            List<DataPersist> myList = new List<DataPersist>();
            if (!string.IsNullOrEmpty(ItemRemboursementDirect))
            {

                if (ItemExecMode == "AddNew")
                {
                    myList = new RemboursementDirectFinancement().fnSelect(new Guid());
                }
                else
                {
                    myList = new RemboursementDirectFinancement().fnSelect(Guid.Parse(ItemRemboursementDirect));
                }

                RemboursementDirectFinancement mClass = new RemboursementDirectFinancement();
                if (myList.Count > 0)
                    mClass = myList[0] as RemboursementDirectFinancement;
                else myList = new List<DataPersist>();

            }
            else
            {
                Store mstore = X.GetCmp<Store>("storeListFinancing");
                mstore.RemoveAll();
                myList = null;
            }

            return this.Store(myList);
        }

        public ActionResult OnSelectSupplier(string ItemSupplier)
        {
            try
            {
                int FournisseurID = GetCriteriaValue(ItemSupplier);

                var mListe = (new RemboursementDirectFinancement()).fnSelectAvailable(FournisseurID);

                Store store = X.GetCmp<Store>("storeListFinancing");
                store.RemoveAll();
                var i = 0;

                foreach (var item in mListe)
                {
                    i += 1;
                    item.IsNew = true;
                    store.Insert(i, item);
                }

                //Get eventual list of savings
                string mType = X.GetCmp<ComboBox>("_cmbType").SelectedItem.Value;
                if ((mType != null) && (ItemSupplier != string.Empty))
                    LoadMecs(ItemSupplier, mType);

                //Clear Financings and savings
                string mAmount = X.GetCmp<TextField>("txtAmount").Text;

                //if ((ItemSupplier != string.Empty) && (mAmount != null) && (mType != null))
                //    ClearFinancingAndMec(ItemSupplier, mAmount, mType);

                //else if ((ItemSupplier != string.Empty) && (mAmount != null) && (mType == null))
                //    ClearFinancing(ItemSupplier, mAmount);


            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Direct Repayment : OnSelectSupplier",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();

        }
        public ActionResult LoadFinancings(string ItemSupplier)
        {
            try
            {
                int FournisseurID = GetCriteriaValue(ItemSupplier);

                var mListe = (new RemboursementDirectFinancement()).fnSelectAvailable(FournisseurID);

                Store store = X.GetCmp<Store>("storeListFinancing");
                store.RemoveAll();
                var i = 0;

                foreach (var item in mListe)
                {
                    i += 1;
                    item.IsNew = true;
                    store.Insert(i, item);
                }               

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Direct Repayment : LoadFinancings",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();

        }

        public ActionResult OnSelectType(string ItemSupplier, string ItemType)
        {
            try
            {
                int FournisseurID = GetCriteriaValue(ItemSupplier);
                int TypeID = GetCriteriaValue(ItemType);

                Store store = X.GetCmp<Store>("storeListMec");
                store.RemoveAll();                

                if (FournisseurID != -1 && TypeID == 3)
                {
                    Store mstoreFin = X.GetCmp<Store>("storeListFinancing");
                    mstoreFin.RemoveAll();
                    var mListe = (new RemboursementDirectMEC()).fnSelectAvailable(FournisseurID);                   
                    var i = 0;

                    foreach (var item in mListe)
                    {
                        i += 1;
                        item.IsNew = true;
                        store.Insert(i, item);
                    }
                }
                string mFournisseur = X.GetCmp<ComboBox>("_cmbFournisseur").SelectedItem.Value;
                //Get list of financings  
                if (FournisseurID != -1 && TypeID != 3)
                {                    
                    if (mFournisseur != null) LoadFinancings(mFournisseur);
                }

                //Clear Financings and savings
                string mAmount = X.GetCmp<TextField>("txtAmount").Text;

                //if ((mFournisseur != null) && (mAmount != string.Empty) && (ItemType != string.Empty))
                //    ClearFinancingAndMec(mFournisseur, mAmount, ItemType);


            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Direct Repayment : OnSelectType",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();

        }
        public ActionResult LoadMecs(string ItemSupplier, string ItemType)
        {
            try
            {
                int FournisseurID = GetCriteriaValue(ItemSupplier);
                int TypeID = GetCriteriaValue(ItemType);

                Store store = X.GetCmp<Store>("storeListMec");
                store.RemoveAll();

                if (FournisseurID != -1 && TypeID == 3)
                {
                    var mListe = (new RemboursementDirectMEC()).fnSelectAvailable(FournisseurID);                   
                    var i = 0;

                    foreach (var item in mListe)
                    {
                        i += 1;
                        item.IsNew = true;
                        store.Insert(i, item);
                    }
                }                              

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Direct Repayment : LoadMecs",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();

        }

        public ActionResult ClearFinancingAndMec(string ItemSupplier, string ItemAmount, string ItemType)
        {
            try
            {
                int FournisseurID = GetCriteriaValue(ItemSupplier);
                int TypeID = GetCriteriaValue(ItemType);
                decimal Montant = ItemAmount != string.Empty ? decimal.Parse(ItemAmount) : 0;

                if(FournisseurID != -1 && Montant != 0)
                {
                    var mListe = (new RemboursementDirectFinancement()).fnClear(FournisseurID, Montant);

                    Store store = X.GetCmp<Store>("storeListFinancing");
                    store.RemoveAll();
                    var i = 0;

                    foreach (var item in mListe)
                    {
                        i += 1;
                        item.IsNew = true;
                        store.Insert(i, item);
                    }

                    if(TypeID == 3)
                    {
                        var mListeMec = (new RemboursementDirectMEC()).fnClear(FournisseurID, Montant);

                        Store storeMec = X.GetCmp<Store>("storeListMec");
                        storeMec.RemoveAll();
                        var j = 0;

                        foreach (var item in mListeMec)
                        {
                            j += 1;
                            item.IsNew = true;
                            storeMec.Insert(j, item);
                        }
                    }
                }                          

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Direct Repayment : ClearFinancingAndMec",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();

        }
        public ActionResult ClearFinancing(string ItemSupplier, string ItemAmount)
        {
            try
            {
                int FournisseurID = GetCriteriaValue(ItemSupplier);
                decimal Montant = ItemAmount != string.Empty ? decimal.Parse(ItemAmount) : 0;

                if(FournisseurID != -1 && Montant != 0)
                {
                    var mListe = (new RemboursementDirectFinancement()).fnClear(FournisseurID, Montant);

                    Store store = X.GetCmp<Store>("storeListFinancing");
                    store.RemoveAll();
                    var i = 0;

                    foreach (var item in mListe)
                    {
                        i += 1;
                        item.IsNew = true;
                        store.Insert(i, item);
                    }                    
                }                          

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Direct Repayment : ClearFinancings",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();

        }


        public ActionResult LoadListOfMec(string ItemRemboursementDirect, string ItemExecMode)
        {
            List<DataPersist> myList = new List<DataPersist>();
            if (!string.IsNullOrEmpty(ItemRemboursementDirect))
            {

                if (ItemExecMode == "AddNew")
                {
                    myList = new RemboursementDirectMEC().fnSelect(new Guid());
                }
                else
                {
                    myList = new RemboursementDirectMEC().fnSelect(Guid.Parse(ItemRemboursementDirect));
                }

                RemboursementDirectMEC mClass = new RemboursementDirectMEC();
                if (myList.Count > 0)
                    mClass = myList[0] as RemboursementDirectMEC;
                else myList = new List<DataPersist>();

            }
            else
            {
                Store mstore = X.GetCmp<Store>("storeListMec");
                mstore.RemoveAll();
                myList = null;
            }

            return this.Store(myList);
        }


        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelRD");

            mform.ToggleCollapse();
            //mform.Collapsed = false;
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite)
        {
            Store mstore = X.GetCmp<Store>("storeListeRemboursementDirect");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemCampagne"      ,ItemCampagne),
                                    new Ext.Net.Parameter("ItemType"          ,ItemType),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus),
                                    new Ext.Net.Parameter("ItemSite"        ,ItemSite)
                                });

            string title = X.GetCmp<FormPanel>("CriteriaPanelRD").Title;
            title += "Site : " + X.GetCmp<ComboBox>("cmbSite").SelectedItem.Text;
            title += ", Campagne : " + X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

            title += ", Fournisseur : " + X.GetCmp<ComboBox>("cmbFournisseur").SelectedItem.Text;

            title += ", Type : " + X.GetCmp<ComboBox>("cmbType").SelectedItem.Text;

            title += ", From " + X.GetCmp<DateField>("dtpStartDate").RawText.ToString() + "to " + X.GetCmp<DateField>("dtpEndDate").RawText.ToString();

            X.GetCmp<FormPanel>("CriteriaPanelRD").Title = title;

            // collapse criterias areas
            X.GetCmp<FormPanel>("CriteriaPanelRD").Collapse(Direction.Top, false);

            return this.Direct();
        }


        private RemboursementDirect MapFormToObject(RemboursementDirect mClass)
        {
            Site mSite = new Site();
            mSite.ID = int.Parse(GetFormValue("hiddenSiteID"));
            mSite.Nom = X.GetCmp<TextField>("TxtSiteNom").Text;
            mClass.Sites = mSite;

            mClass.Campagne = X.GetCmp<ComboBox>("_cmbCampagne").Text;

            mClass.Numero = X.GetCmp<Hidden>("hiddenNumero").Text;
            RemboursementDirectType mType = new RemboursementDirectType();
            mType.ID = int.Parse(GetFormValue("_cmbType"));
            mType.Designation = X.GetCmp<ComboBox>("_cmbType").SelectedItem.Text.ToString();
            mClass.RemboursementDirectType = mType;

            Fournisseur mSupplier = new Fournisseur();
            mSupplier.ID = int.Parse(GetFormValue("_cmbFournisseur"));
            mSupplier.Nom = X.GetCmp<ComboBox>("_cmbFournisseur").SelectedItem.Text.ToString();
            mClass.Fournisseur = mSupplier;

            int BanqueID;

            if (int.TryParse(GetFormValue("cmbBanque"), out BanqueID))
            {
                mClass.Banque = new Tms.Classes.Shared.Banque();
                mClass.Banque.ID = BanqueID;
                mClass.Banque.Designation = X.GetCmp<ComboBox>("cmbBanque").SelectedItem.Text.ToString();
            }
            else
                mClass.Banque = null;          

            mClass.DateRemboursement = DateTime.Parse(X.GetCmp<DateField>("txtDate").RawText.ToString());
            if (X.GetCmp<TextField>("txtReference").Text != string.Empty) mClass.Reference = X.GetCmp<TextField>("txtReference").Text;
            if (X.GetCmp<TextField>("txtAmount").Text != string.Empty) mClass.Montant = decimal.Parse(X.GetCmp<TextField>("txtAmount").Text);
            mClass.Commentaire = X.GetCmp<TextField>("txtCommentaire").Text;
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

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
            X.GetCmp<RowSelectionModel>("rowSelectionListeRD").DeselectAll();
        }

        public ActionResult OnDisplayDirectRepaymentList(int ItemSite = -1)
        {
            ViewData["Titre"] = "Liste des Remboursements directes";
            ViewData["actionToDo"] = "OnPrintDirectRepaymentList";
            ViewData["ControllerName"] = "RemboursementDirect";
            ViewData["SiteParDefaut"] = ItemSite;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmCriteriaForDirectRepayment", ViewData = ViewData };
        }

        public ActionResult OnPrintDirectRepaymentList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetStartDate").RawText, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetEndDate").RawText, "d", CultureInfo.CurrentUICulture);

                //Session["paramCropYear"] = cropYear;   
                Session["paramSite"] = int.Parse(GetFormValue("cmbDetSite"));
                Session["paramSiteText"] = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text;
                Session["paramCampagne"] = X.GetCmp<ComboBox>("cmbDetCropYear").SelectedItem.Text;
                Session["paramFournisseur"] = int.Parse(GetFormValue("cmbDetFournisseur"));
                Session["paramFournisseurText"] = X.GetCmp<ComboBox>("cmbDetFournisseur").SelectedItem.Text;
                Session["paramRemboursement"] = int.Parse(GetFormValue("cmbDetRemboursementType"));
                Session["paramRemboursementText"] = X.GetCmp<ComboBox>("cmbDetRemboursementType").SelectedItem.Text;
                Session["paramStatut"] = GetFormValue("cmbDetStatus");
                Session["paramStatutText"] = X.GetCmp<ComboBox>("cmbDetStatus").SelectedItem.Text;

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/RemboursementDirect/ViewReportListResult', this, 'Direct Repayments',''),App.frmCriteriaForDirectRepayment.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Direct Repayment : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
        }

        public ActionResult ViewReportListResult()
        {
            //XtraReport report = null;

            rptDirectRepaymentList report = new rptDirectRepaymentList();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramCampagneText"].Value = Session["paramCampagne"];
            if (!string.IsNullOrEmpty(Session["paramCampagne"].ToString()) && Session["paramCampagne"].ToString() == "{Tous}")
                report.Parameters["paramCampagne"].Value = string.Empty;
            else
                report.Parameters["paramCampagne"].Value = Session["paramCampagne"];

            report.Parameters["paramSite"].Value = Session["paramSite"];
            report.Parameters["paramSiteText"].Value = Session["paramSiteText"];

            report.Parameters["paramFournisseur"].Value = Session["paramFournisseur"];
            report.Parameters["paramFournisseurText"].Value = Session["paramFournisseurText"];

            report.Parameters["paramRemboursementType"].Value = Session["paramRemboursement"];
            report.Parameters["paramRemboursementTypeText"].Value = Session["paramRemboursementText"];

            report.Parameters["paramStatut"].Value = Session["paramStatut"];
            report.Parameters["paramStatutText"].Value = Session["paramStatutText"];

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];
            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

    }
}
