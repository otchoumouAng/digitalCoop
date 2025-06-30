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
using System.Globalization;
using Tms.Classes.Security;

namespace Tms2017.MVC.Controllers
{
    public class SpotPriceController : BaseController
    {

        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";

        // GET: SpotPrice
        public ActionResult Index()
        {
            Parametres mParam = (new Parametres(0));            

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            X.GetCmp<ComboBox>("cmbCampagne").SetValue(mParam.Campagne);

            DateTime firstdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            DateTime lastDayOfMonth = firstdayofmonth.AddMonths(1).AddDays(-1);

            X.GetCmp<DateField>("TxtPeriodStart").RawText = firstdayofmonth.ToShortDateString();
            X.GetCmp<DateField>("TxtPeriodEnd").RawText = lastDayOfMonth.ToShortDateString();

            X.GetCmp<FormPanel>("CriteriaPanelSpotPrice").SetTitle("Site : " + mSiteParDefaut.Nom + ", Campagne : " + mParam.Campagne + " | Du : " + firstdayofmonth.ToShortDateString() + " Au : " + lastDayOfMonth.ToShortDateString());

            #region Set Function's Access                        
            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{66e0cc5d-4658-4fb8-85c3-b329b8eb081a}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{EF1D4D4A-455A-4C56-9460-89131809F79B}")))
                X.GetCmp<Button>("btnNewSpotPrice").Enable();
            else
                X.GetCmp<Button>("btnNewSpotPrice").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{5B7C8424-BCEA-4DA0-B7B9-93D3F2964039}")))
                X.GetCmp<MenuItem>("mnuExportSpotPrice").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportSpotPrice").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{2abe7a72-758c-4a1d-b213-a34007bfecb1}")))
                X.GetCmp<MenuItem>("mnuPrintSpotPriceList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintSpotPriceList").Disable();            

            X.GetCmp<Hidden>("SphiddenPermCreer").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{EF1D4D4A-455A-4C56-9460-89131809F79B}")));
            X.GetCmp<Hidden>("SphiddenPermModifier").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{98C9DA02-FC40-4B39-8F0C-E0D70A5E8C2A}")));
            X.GetCmp<Hidden>("SphiddenPermDesactiver").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{EA25C709-E3FD-41CA-881F-C464193C3819}")));
            X.GetCmp<Hidden>("SphiddenPermActiver").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{EA25C709-E3FD-41CA-881F-C464193C3819}")));
            X.GetCmp<Hidden>("SphiddenPermExporterExcel").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{5B7C8424-BCEA-4DA0-B7B9-93D3F2964039}")));
            //X.GetCmp<Hidden>("SphiddenPermPrintSpotPriceList").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{2ABE7A72-758C-4A1D-B213-A34007BFECB1}"));
            X.GetCmp<Hidden>("SphiddenPermApprove").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("B8F8CFBB-38EA-422F-A882-3AA4705E1B4D")));
            X.GetCmp<Hidden>("SphiddenPermOverview").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{B8EC41D0-173A-47A6-BB9A-140942CE66B8}")));
            //}
            #endregion

            return View();
        }

        #region SpotPrice        

        public ActionResult onAdd()
        {            
            PrixNegocieViewModel mclass = new PrixNegocieViewModel();

            mclass._PrixNegocie = new PrixNegocie();
            mclass._PrixNegocie.Site = new Site();
            mclass._PrixNegocie.ModeApplication = new PrixNegocieModeApplication();
            mclass._PrixNegocie.Campagne = new Campagne();
            mclass._Parametres = new Parametres();
            mclass._PrixNegocieParam = new PrixNegocieParamViewModel();
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            Site mSiteParDefaut = new Site();
            string UserName = (string)Session["userName"];
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            if (result)
                mclass._PrixNegocie.Site.ID = mSiteParDefaut.ID;

            //var mParam = (new Parametres()).fnSelect();

            mclass._Parametres.fnGet();
            mclass._PrixNegocie.Campagne.Designation = mclass._Parametres.Campagne;
            mclass._PrixNegocie.ModeApplication.ID = mclass._Parametres.IDPrixNegociePourLivraisons;

            PrixJournalier mPrix = new PrixJournalier();
            mPrix.fnSelectByDate(mSiteParDefaut.ID, DateTime.Now);
            decimal prix = string.IsNullOrEmpty(mPrix.Prix.ToString()) ? 0 : mPrix.Prix;
            mclass._PrixNegocieParam.PrixJournalier = prix;
            mclass._PrixNegocieParam.TolerancePrixNegocie = mclass._Parametres.PrixNegocie.ToString();

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormSpotPrice", Model = mclass, };
        }
        public ActionResult onEdit(string ItemSelected)
        {            
            PrixNegocieLivraison PNClass = JSON.Deserialize<PrixNegocieLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            PrixNegocie mclass = new PrixNegocie();

            PrixNegocieViewModel viewModel = new PrixNegocieViewModel();

            mclass.fnGet(PNClass.ID);
            viewModel._PrixNegocieLivraison = new PrixNegocieLivraison();
            viewModel._PrixNegocieLivraison = PNClass;
            viewModel._PrixNegocieParam = new PrixNegocieParamViewModel();
            viewModel._Parametres = new Parametres();
            viewModel._PrixNegocie = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            //var mParam = (new Parametres()).fnSelect();

            //viewModel._Parametres = mParam[0] as Parametres;
            Site mSiteParDefaut = new Site();
            string UserName = (string)Session["userName"];
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            if (result)
                viewModel._PrixNegocie.Site.ID = mSiteParDefaut.ID;

            viewModel._Parametres.fnGet();

            PrixJournalier mPrix = new PrixJournalier();
            mPrix.fnSelectByDate(mSiteParDefaut.ID, DateTime.Now);
            decimal prix = string.IsNullOrEmpty(mPrix.Prix.ToString()) ? 0 : mPrix.Prix;

            viewModel._PrixNegocieParam.PrixJournalier = prix;
            viewModel._PrixNegocieParam.TolerancePrixNegocie = viewModel._Parametres.PrixNegocie.ToString();

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormSpotPrice", Model = viewModel };
        }

        public ActionResult onConsult(string ItemSelected)
        {            
            PrixNegocieLivraison PNClass = JSON.Deserialize<PrixNegocieLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            PrixNegocie mclass = new PrixNegocie();

            PrixNegocieViewModel viewModel = new PrixNegocieViewModel();

            mclass.fnGet(PNClass.ID);
            viewModel._PrixNegocieLivraison = new PrixNegocieLivraison();
            viewModel._PrixNegocieLivraison = PNClass;
            viewModel._PrixNegocieParam = new PrixNegocieParamViewModel();
            viewModel._Parametres = new Parametres();
            viewModel._PrixNegocie = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            //var mParam = (new Parametres()).fnSelect();

            //viewModel._Parametres.fnGet();
            //PrixJournalier mClass = new PrixJournalier();
            //mClass.fnSelectByDate(mclass.DatePrix);

            viewModel._PrixNegocieParam.PrixJournalier = 0;
            viewModel._PrixNegocieParam.TolerancePrixNegocie = "0";

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormSpotPrice", Model = viewModel };
        }

        public ActionResult SubmitFormMethod(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                PrixNegocie mClass = new PrixNegocie();
                PrixNegocieLivraison prixlivraison;
                Guid prixnegocielivraison = Guid.Parse(GetFormValue("TxtSpotPriceDeliverieID"));
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtSpotPriceID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Prix Negocié load failed.");
                }
                bool result = true;
                bool resultLivraison = true;

                mClass = MapFormToObject(mClass);

                if (mClass.ModeApplication.ID == 2)
                {
                    List<Livraison> ItemLivraison = JSON.Deserialize<List<Livraison>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (ItemLivraison.Count() > 0)
                    {
                        _db = mClass.db();
                        mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                        result = mClass.fnUpdate(mtran);

                        if (result)
                        {

                            for (int i = 0; i < ItemLivraison.Count(); i++)
                            {
                                prixlivraison = new PrixNegocieLivraison();
                                prixlivraison.Livraison = new Livraison();
                                prixlivraison.PrixNegocie = new PrixNegocie();

                                prixlivraison.SetDataSource(_db);

                                prixlivraison.Livraison.ID = ItemLivraison[i].ID;

                                prixlivraison.IsNew = ItemLivraison[i].IsNew;
                                prixlivraison.PrixNegocie.ID = mClass.ID;
                                prixlivraison.UtilisateurCreation = (string)Session["userName"];
                                prixlivraison.UtilisateurModification = (string)Session["userName"];
                                if (prixlivraison.IsNew)
                                {
                                    resultLivraison = prixlivraison.fnUpdate(mtran);
                                }

                                if (!resultLivraison)
                                {
                                    break;
                                }
                            }

                            if (!resultLivraison)
                            {
                                _db.RollBackTransaction(mtran);
                            }
                            _db.CommitTransaction(mtran);

                        }
                    }
                    else
                    {
                        _db.RollBackTransaction(mtran);
                        X.MessageBox.Show(new MessageBoxConfig
                        {
                            Title = "Prix Negocié : Data Validation",
                            Message = "Please select specific(s) Livraison(s)",
                            Buttons = MessageBox.Button.OK,
                            Icon = MessageBox.Icon.WARNING
                        });
                        return this.Direct();
                    }
                }
                else
                {
                    result = mClass.fnUpdate();
                }

                if (result)
                {

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.Update && mClass.ModeApplication.ID == 1)
                    {
                        //Livraison ListeLivraisonToUpdate = new Livraison();
                        PrixNegocieLivraison pnclass = new PrixNegocieLivraison();
                        List<DataPersist> myList = new Livraison().fnSelectBySpotPrice(mClass.ID, -1);
                        if (myList.Count > 0)
                        {
                            //ListeLivraisonToUpdate = myList[0] as Livraison;
                            foreach (Livraison item in myList)
                            {
                                pnclass.RowVersionKey = item.RowVersionKey;
                                pnclass.fnRemoveDelivery(item.ID);
                            }
                        }
                    }

                    prixlivraison = new PrixNegocieLivraison();


                    Store mstore = X.GetCmp<Store>("storeListeSpotPrice");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        prixlivraison.fnGet(mClass.ID);
                        mstore.Insert(0, prixlivraison);
                        X.GetCmp<RowSelectionModel>("rowSelectionListeSpotPrice").Select(0);
                    }
                    else
                    {
                        prixlivraison.fnGet(prixnegocielivraison);
                        ModelProxy mProxy = mstore.GetById(prixnegocielivraison);

                        mProxy.BeginEdit();

                        mProxy.Set(prixlivraison);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormSpotPrice").Close();
                }
            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mtran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prix Negocié : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });

            }

            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd, string ItemCampagne, string ItemSite, string ItemStatus)
        {
            int FournisseurID = GetCritriaValue(ItemFournisseur);
            int SiteID = GetCritriaValue(ItemSite);
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "{Tous}";
            }

            DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            string Status = ItemStatus;            
            var mListe = (new PrixNegocieLivraison()).fnSelect(Campagne, SiteID, FournisseurID, startdate, enddate, Status);            
            return this.Store(mListe);
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelSpotPrice");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeSpotPrice");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                new Ext.Net.Parameter("ItemCampagne"      ,ItemCampagne),
                                new Ext.Net.Parameter("ItemPeriodStart"     ,ItemPeriodStart),
                                new Ext.Net.Parameter("ItemPeriodEnd"           ,ItemPeriodEnd),
                                new Ext.Net.Parameter("ItemStatus"        ,ItemStatus),
                                new Ext.Net.Parameter("ItemSite"        ,ItemSite)
                            });

                FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelSpotPrice");
                mform.Collapsed = true;
            }
            catch (Exception ex)
            {
                if (ex is FormatException)
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Prix Negocié : Data Validation",
                        Message = "La periode saisie n'est pas valide !",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                }
                else
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Prix Negocié : Data Validation",
                        Message = ex.Message,
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                }
                
            }

            return this.Direct();
        }

        [DirectMethod]
        public ActionResult OnSelectEntryDate(string ItemDatePrix, int ItemSite = 1)
        {
            //DateTime datedebut = DateTime.ParseExact(ItemDatePrix, "d", CultureInfo.CurrentUICulture);

            PrixJournalier mClass = new PrixJournalier();
            DateTime ValidDatePrix;

            bool IsvalidDate = DateTime.TryParseExact(ItemDatePrix, "d", CultureInfo.CurrentUICulture, DateTimeStyles.None, out ValidDatePrix);

            if (IsvalidDate)
                mClass.fnSelectByDate(ItemSite, DateTime.Parse(ItemDatePrix));

            X.GetCmp<TextField>("LabelPrixJournalier").Text = mClass.PrixAsString;
            X.GetCmp<TextField>("TxtPrice").Text = mClass.PrixAsString;
            X.GetCmp<TextField>("LabelPrixJournalier").Hidden = false;
            return this.Direct();
        }

        public ActionResult OnApprove(string ItemSelected)
        {
            try
            {
                PrixNegocieLivraison mClass = JSON.Deserialize<PrixNegocieLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                PrixNegocie PxNegocie = new PrixNegocie();

                bool result = PxNegocie.fnGet(mClass.ID);

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("Approve : Prix Negocié Approve failed.");

                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];
                PxNegocie.UtilisateurCreation = (string)Session["userName"];
                PxNegocie.UtilisateurModification = (string)Session["userName"];
                result = PxNegocie.fnApprove();

                if (result)
                {
                    PxNegocie.Statut = "AP";
                    mClass.PrixNegocie = new PrixNegocie();
                    mClass.PrixNegocie = PxNegocie;
                    Store mstore = X.GetCmp<Store>("storeListeSpotPrice");

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
                    Title = "Prix Negocié : Approuver",
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
                PrixNegocieLivraison mClass = JSON.Deserialize<PrixNegocieLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                PrixNegocie PxNegocie = new PrixNegocie();
                bool result = PxNegocie.fnGet(mClass.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Prix Negocié loading failed.");

                mClass.UtilisateurModification = (string)Session["userName"];

                PxNegocie.UtilisateurModification = (string)Session["userName"];

                if (PxNegocie.Desactive)
                    result = PxNegocie.fnActivate();
                else
                    result = PxNegocie.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Prix Negocié Operation failed.");


                if (result)
                {
                    mClass.PrixNegocie = new PrixNegocie();
                    mClass.PrixNegocie = PxNegocie;
                    Store mstore = X.GetCmp<Store>("storeListeSpotPrice");

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
                    Title = "Prix Negocié : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnAddDelivery(string ItemFournisseur, string ItemExecMode)
        {
            PrixNegocieViewModel mclass = new PrixNegocieViewModel();

            if (!string.IsNullOrEmpty(ItemFournisseur))
            {
                mclass._PrixNegocie = new PrixNegocie();

                mclass._PrixNegocie.Fournisseur = new Fournisseur();                
                mclass._PrixNegocie.Fournisseur.ID = int.Parse(ItemFournisseur);               
                mclass._ExecMode = ItemExecMode == "Update" ? Tms.Components.Settings.EnumsDefinition.eExecMode.Update : Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            }

            return new Ext.Net.MVC.PartialViewResult { ViewName = "ListDeliveriesDetails", Model = mclass };
        }

        public ActionResult OnRemoveDelivery(string ItemDelivery)
        {

            try
            {
                Livraison mClass = JSON.Deserialize<Livraison>(ItemDelivery, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Store mstore = X.GetCmp<Store>("storeDetailDeliveriesListe");
                Guid prixnegocie = Guid.Parse(X.GetCmp<TextField>("TxtSpotPriceID").Text);
                Guid prixnegocielivraison = Guid.Parse(X.GetCmp<TextField>("TxtSpotPriceDeliverieID").Text);

                mClass.fnGetBySpotPrice(prixnegocie, mClass.ID);

                if (mClass.IsNew)
                {
                    ModelProxy _proxy = mstore.GetById(mClass.ID);
                    _proxy.Drop();

                    return this.Direct();
                }

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("onApproveDailyPrice :Prix Journalier Approve failed.");

                PrixNegocieLivraison itemPN = new PrixNegocieLivraison();
                itemPN.RowVersionKey = mClass.RowVersionKey;

                bool result = itemPN.fnRemoveDelivery(mClass.ID);

                if (result)
                {
                    itemPN = new PrixNegocieLivraison();
                    itemPN.fnGet(prixnegocielivraison);


                    Store storespotprice = X.GetCmp<Store>("storeListeSpotPrice");

                    ModelProxy ProxySpotPrice = storespotprice.GetById(itemPN.ID);

                    ProxySpotPrice.BeginEdit();
                    ProxySpotPrice.Set(itemPN);
                    ProxySpotPrice.Commit();
                    ProxySpotPrice.EndEdit();

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.Drop();

                    mProxy.Set(mClass);

                    mProxy.Commit();


                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prix Negocié : Retirer",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult LoadListDeliveryBySpotPrice(string ItemFournisseur)
        {
            var mListe = (new Livraison()).fnSelectForSpotPrice(int.Parse(ItemFournisseur));
            return this.Store(mListe);
        }

        public ActionResult LoadListPendngDelivery()
        {
            var mListe = (new Livraison()).fnSelectPendingDeliveries();
            return this.Store(mListe);
        }

        public ActionResult OnShowPendingDeliveries()
        {                        
            PrixNegocieViewModel mclass = new PrixNegocieViewModel();
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
            mclass._PrixNegocie = new PrixNegocie();

            mclass._PrixNegocie.Fournisseur = new Fournisseur();
            //mclass._PrixNegocie.Fournisseur.ID = int.Parse(ItemFournisseur);

            return new Ext.Net.MVC.PartialViewResult { ViewName = "ListPendingDeliveries", Model = mclass };
        }

        public ActionResult SubmitListDeliveries(string ItemSelected, string ItemExecMode)
        {
            if (ItemExecMode != Tms.Components.Settings.EnumsDefinition.CONSULT)
            {

                List<Livraison> livraisons = JSON.Deserialize<List<Livraison>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Store store = X.GetCmp<Store>("storeDetailDeliveriesListe");

                foreach (var item in livraisons)
                {
                    item.IsNew = true;
                    store.Insert(0, item);
                }

                X.GetCmp<Window>("ListDeliveriesDetails").Close();
            }
            else
            {
                X.GetCmp<Window>("ListDeliveriesDetails").Close();
            }
            return this.Direct();
        }

        private PrixNegocie MapFormToObject(PrixNegocie mClass)
        {
            //string testdate = X.GetCmp<DateField>("TxtPriceDate").Text;
            //DateTime testdateSelected = X.GetCmp<DateField>("TxtPriceDate").SelectedDate;
            //string testdatevalueraw = X.GetCmp<DateField>("TxtPriceDate").RawText.ToString();
            mClass.DatePrix = DateTime.Parse(X.GetCmp<DateField>("TxtPriceDate").RawText.ToString());
            mClass.Prix = decimal.Parse(X.GetCmp<TextField>("TxtPrice").Text);
            mClass.Commission = decimal.Parse(X.GetCmp<TextField>("TxtCommission").Text);
            mClass.PrixJour = decimal.Parse(X.GetCmp<TextField>("LabelPrixJournalier").Text);
            mClass.FactureAuPrixJour = bool.Parse(X.GetCmp<Checkbox>("ChkAFacturer").Value.ToString());

            mClass.Fournisseur = new Fournisseur();
            mClass.Fournisseur.ID = int.Parse(GetFormValue("FournisseurID"));
            mClass.Fournisseur.Nom = X.GetCmp<ComboBox>("FournisseurID").SelectedItem.Text.ToString();

            mClass.ModeApplication = new PrixNegocieModeApplication();

            int mode = int.Parse(GetFormValue("ModeApplicationID"));
            mClass.ModeApplication.ID = int.Parse(GetFormValue("ModeApplicationID"));
            mClass.Numero = GetFormValue("hiddenNumeroPrice");

            mClass.DateDebut = mode == 1 ? DateTime.Parse(X.GetCmp<DateField>("TxtStartDate").RawText.ToString()) : (DateTime?)null;
            mClass.DateEcheance = mode == 1 ? DateTime.Parse(X.GetCmp<DateField>("TxtDueDate").RawText.ToString()).AddHours(23).AddMinutes(59) : (DateTime?)null;

            mClass.Site = new Site();            
            mClass.Site.ID = int.Parse(GetFormValue("_cmbSite"));
            mClass.Site.Nom = X.GetCmp<ComboBox>("_cmbSite").SelectedItem.Text.ToString();

            mClass.Commentaire = X.GetCmp<TextArea>("TxtCommentaireSpotPrice").Text;
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            mClass.Campagne = new Campagne();
            mClass.Campagne.Designation = GetFormValue("CropYearID");
            //mClass.DateDebut = DateTime.Parse(GetFormValue("hiddenNumeroPrice"));
            return mClass;
        }

        private void MapObjectToForm(PrixNegocie mClass)
        {
            X.GetCmp<ComboBox>("FournisseurID").SetValue(mClass.Fournisseur.ID.ToString());
            X.GetCmp<TextField>("TxtPriceDate").Text = mClass.DatePrix.ToString();
            X.GetCmp<TextField>("TxtPrice").Text = mClass.Prix.ToString();
            X.GetCmp<DateField>("TxtStartDate").Text = mClass.DateDebut.ToString();
            X.GetCmp<DateField>("TxtDueDate").Text = mClass.DateEcheance.ToString();
            X.GetCmp<ComboBox>("ModeApplicationID").SetValue(mClass.ModeApplication.ID.ToString());

            X.GetCmp<TextField>("hiddenNumeroPrice").Text = mClass.Numero.ToString();

        }

        public ActionResult OnSelectModeApplication(string ItemFournisseur, string ItemMode)
        {
            Store mstore = X.GetCmp<Store>("storeDelivery");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur)
                            });

            return this.Direct();
        }
        #endregion

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
            //X.Js.Call("App.rowSelectionListeSpotPrice.deselectAll();");
            X.GetCmp<RowSelectionModel>("rowSelectionListeSpotPrice").DeselectAll();
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


        public ActionResult OnDisplaySpotPriceList()
        {
            string UserName = (string)Session["userName"];

            Site mSiteParDefaut = new Site();
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            Parametres mParam = new Parametres(0);

            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;
            if (mParam.Site == mSiteParDefaut.ID) ViewData["UrlSite"] = "LoadSiteAll";
            else ViewData["UrlSite"] = "LoadSiteByAccess";

            ViewData["Titre"] = "Liste des Prix Negociés";
            ViewData["actionToDo"] = "mnuPrintSpotPriceList";
            ViewData["ControllerName"] = "SpotPrice";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmPeriodWithSupplierForReport", ViewData = ViewData };
        }

        public ActionResult mnuPrintSpotPriceList(string fournisseur, string fournisseurText,string startDate, string endDate)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                //Session["paramCropYear"] = cropYear;    
                Session["paramSite"] = int.Parse(GetFormValue("cmbDetSite"));
                Session["paramSiteText"] = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text;

                Session["paramFournisseur"] = fournisseur;
                Session["paramFournisseurText"] = fournisseurText;
                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/SpotPrice/ViewReportResult', this, 'List Of Prix Negociés',''),App.frmPeriodWithSupplierForReport.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prix Negocié : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }


        }

        public ActionResult ViewReportResult()
        {          

            rptSpotPriceList report = new rptSpotPriceList();
            report.DataSource = DevExpressReportDs.SetDataSource(report);
            report.Parameters["paramSite"].Value = Session["paramSite"];
            report.Parameters["paramSiteText"].Value = Session["paramSiteText"];

            report.Parameters["paramFournisseur"].Value = Session["paramFournisseur"];
            report.Parameters["paramFournisseurText"].Value = Session["paramFournisseurText"];
            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];
            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

    }
}