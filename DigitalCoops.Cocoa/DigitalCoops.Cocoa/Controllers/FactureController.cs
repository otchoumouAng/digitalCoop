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
using Tms.LocalService;
using Tms.Classes;
using System.IO;

namespace Tms2017.MVC.Controllers
{
    public class FactureController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
                
        int mTypeDeductionAvanceID = new Parametres(0).FactureDeductionTypeAvance;
        int mTypeDeductionMecID = new Parametres(0).FactureDeductionTypeMec;
        int mTypeDeductionLegalTaxID = new Parametres(0).FactureDeductionTypeLegalTax;
         
        Guid mID;

        // GET: Facture
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            //bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            bool result = mSiteParDefaut.fnGetDefaultSite();
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            X.GetCmp<ComboBox>("cmbCampagne").SetValue(mParam.Campagne);

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("dtpStartDate").RawText = StartDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Fournisseur = "{Tous}";

            string Type = "{Tous}";

            X.GetCmp<FormPanel>("CriteriaPanelFA").SetTitle("Site : " + mSiteParDefaut.Nom + ", Campagne : " + mParam.Campagne + " | Fournisseur : " + Fournisseur + " | Type De Livraison : " + Type + " | Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{9e33e1a4-60b4-436c-9d8a-c3663eace653}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();

            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{1C06EC8D-B389-4F49-A3DC-A0B6C080040C}", UserName);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{9C2F639D-9566-4CD2-89EA-93023EDF704C}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();
            
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{731CBB8A-788A-48B0-B900-6EFB5F22F24B}")))
                X.GetCmp<MenuItem>("mnuExportInvoices").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportInvoices").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{c043f313-13a0-4e6a-85b9-7baf69f5681b}")))
                X.GetCmp<MenuItem>("btnViewPendingDeliveries").Enable();
            else
                X.GetCmp<MenuItem>("btnViewPendingDeliveries").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{01D86562-4C18-4DD2-A16A-BE6067B9AF04}")))
                X.GetCmp<MenuItem>("mnuPrintInvoiceList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintInvoiceList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{3FCDBF84-CD48-4244-8344-3941818B4C1F}")))
                X.GetCmp<MenuItem>("mnuPrintValuationList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintValuationList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{95710067-B6A4-46EE-886F-4BFAB2E2553C}")))
                X.GetCmp<MenuItem>("mnuPrintAvailableDeliveries").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintAvailableDeliveries").Disable();

            X.GetCmp<Hidden>("FahiddenPermCreer").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{9C2F639D-9566-4CD2-89EA-93023EDF704C}")));
            X.GetCmp<Hidden>("FahiddenPermConsultPendingDeliveries").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{C043F313-13A0-4E6A-85B9-7BAF69F5681B}")));
            X.GetCmp<Hidden>("FahiddenPermDesactiver").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{23998AE1-7FA1-46A0-8776-600244A9FF05}")));
            X.GetCmp<Hidden>("FahiddenPermPrintCopyOfInvoices").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{D457282E-95A5-4B16-BDB5-BB45A6E35571}")));
            X.GetCmp<Hidden>("FahiddenPermExporterExcel").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{731CBB8A-788A-48B0-B900-6EFB5F22F24B}")));
            X.GetCmp<Hidden>("FahiddenPermPrintListOfInvoices").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{01D86562-4C18-4DD2-A16A-BE6067B9AF04}")));
            X.GetCmp<Hidden>("FahiddenPermPrintListOfValuation").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{3FCDBF84-CD48-4244-8344-3941818B4C1F}")));
            X.GetCmp<Hidden>("FahiddenPermOverview").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{1C06EC8D-B389-4F49-A3DC-A0B6C080040C}")));

            #endregion

            return View();
        }
                
        public ActionResult onAdd()
        {                        
            FactureViewModel mclass = new FactureViewModel();

            mclass._Facture = new Facture();
            mclass._Facture.isViewPending = false;

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            //}
            //catch (Exception ex)
            //{
            //    X.MessageBox.Alert("Error", ex.Message).Show();
            //}

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Facture_Detail", Model = mclass, };
        }

        public ActionResult onConsult(string ItemSelected)
        {            
            Facture mclass = JSON.Deserialize<Facture>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore});
            bool result = mclass.fnGet(mclass.ID);
            FactureViewModel viewmodel = new FactureViewModel();
            viewmodel._Facture = mclass;
            viewmodel._Facture.isViewPending = false;
            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Facture_Detail", Model = viewmodel, };
        }


        public ActionResult OnViewPendingDeliveries()
        {
            //try
            //{            
            FactureViewModel mclass = new FactureViewModel();

            mclass._Facture = new Facture();
            mclass._Facture.isViewPending = true;

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            //}
            //catch (Exception ex)
            //{
            //    X.MessageBox.Alert("Error", ex.Message).Show();
            //}

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Facture_Detail", Model = mclass, };
        }

       
        public ActionResult OnCheckViewPending(string ViewPending)
        {

            bool IsPending = false;
            bool isBool = bool.TryParse(ViewPending, out IsPending);
            if (isBool && IsPending)
            {
                BonDeLivraisonViewModel mclass = new BonDeLivraisonViewModel();
                mclass._BonDeLivraison = new BonDeLivraison();
                mclass._BonDeLivraison.Livraison = new Livraison();

                var StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
                var EndDate = DateTime.Now.ToShortDateString();

                string UserName = (string)Session["userName"];
                Site mSiteParDefaut = new Site();
                
                bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

                if (result)
                {
                    mclass._BonDeLivraison.Livraison.Site = new Site();
                    mclass._BonDeLivraison.Livraison.Site.ID = mSiteParDefaut.ID;
                    mclass._BonDeLivraison.Livraison.Site.Nom = mSiteParDefaut.Nom;
                }

                //var Fournisseur = "{Tous}";

                //X.GetCmp<FormPanel>("CriteriaPanelFDel").SetTitle(" | Fournisseur : " + Fournisseur + " | Du : " + StartDate + " Au : " + EndDate);

                return new Ext.Net.MVC.PartialViewResult { ViewName = "Facture_DeliveryNotes", Model = mclass };

            }

            else
                return null;

        }

        public ActionResult OnSelectDeliveryNote()
        {
            BonDeLivraisonViewModel mclass = new BonDeLivraisonViewModel();

            var StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            var EndDate = DateTime.Now.ToShortDateString();

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            if (result)
            {
                mclass._BonDeLivraison = new BonDeLivraison();
                mclass._BonDeLivraison.Livraison = new Livraison();
                mclass._BonDeLivraison.Livraison.Site = new Site();
                mclass._BonDeLivraison.Livraison.Site.ID = mSiteParDefaut.ID;
                mclass._BonDeLivraison.Livraison.Site.Nom = mSiteParDefaut.Nom;
            }

            //var Fournisseur = "{Tous}";

            //X.GetCmp<FormPanel>("CriteriaPanelFDel").SetTitle(" | Fournisseur : " + Fournisseur + " | Du : " + StartDate + " Au : " + EndDate);

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Facture_DeliveryNotes", Model = mclass };

        }
        public ActionResult OnFinancing(string ItemFournisseur = "", string ItemFinancings = "")
        {            
            FinancementViewModel viewModel = new FinancementViewModel();
            Financement mclass = new Financement();
            //List<FactureDeduction> ListOfDeductions = JSON.Deserialize<List<FactureDeduction>>(test, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            int IDFournisseur = ItemFournisseur == "" ? -1 : int.Parse(ItemFournisseur);
            Fournisseur mFournisseur = new Fournisseur();
            mFournisseur.ID = IDFournisseur;
            mFournisseur.Nom = string.Empty;
            mclass.Fournisseur = mFournisseur;

            //Get list of financings to be deducted
            List<FactureDeduction> ListOfDeductions = JSON.Deserialize<List<FactureDeduction>>(ItemFinancings, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            Facture_Prelevement mFinancement = null;
            List<Facture_Prelevement> mListOfFinancement = new List<Facture_Prelevement>();
            foreach (FactureDeduction mDeduction in ListOfDeductions)
            {
                if(mDeduction.DeductionType.ID == mTypeDeductionAvanceID) //Financing
                {
                    mFinancement = new Facture_Prelevement();
                    mFinancement.DeductionID = mDeduction.ID;
                    mFinancement.Numero = mDeduction.ElementRef;
                    mFinancement.MontantPreleve = mDeduction.Montant;
                    mListOfFinancement.Add(mFinancement);
                }
            }

            mclass.ListOfFinancementDeducted = JSON.Serialize(mListOfFinancement);
            mclass.NbDeductions = ListOfDeductions.Count;           

            //Calculation of amount availaible            
            decimal mTotalFinancement = 0;
            decimal mAvailaibleAmount = 0;
            decimal mAmountToPay = Convert.ToDecimal(GetFormValue("txtAmount"));

            foreach (Facture_Prelevement mDeduction in mListOfFinancement)
            {
                mTotalFinancement += mDeduction.MontantPreleve;              
            }

            mAvailaibleAmount = mAmountToPay + mTotalFinancement;

            if (X.GetCmp<TextField>("txtNetWeight").Text != string.Empty) mclass.PoidsNetFacture = Convert.ToDecimal(GetFormValue("txtNetWeight"));
            mclass.MontantDisponible = mAvailaibleAmount;
           
            viewModel._Financement = mclass;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Facture_Financings", Model = viewModel};

        }

        public ActionResult OnSaving(string ItemSavings)
        {
            MiseEnCompteViewModel viewModel = new MiseEnCompteViewModel();
            MiseEnCompte mclass = new MiseEnCompte();

            //Get amount availaible            
            decimal mAmountToPay = Convert.ToDecimal(GetFormValue("txtAmount"));
            decimal mNetWeight = Convert.ToDecimal(GetFormValue("txtNetWeight"));

            mclass.MontantDisponible = mAmountToPay;
            mclass.PoidsNet = mNetWeight;

            //Get list of savings 
            List<FactureDeduction> ListOfDeductions = JSON.Deserialize<List<FactureDeduction>>(ItemSavings, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            Facture_Prelevement mMEC = null;
            List<Facture_Prelevement> mListOfMEC= new List<Facture_Prelevement>();
            foreach (FactureDeduction mDeduction in ListOfDeductions)
            {
                if (mDeduction.DeductionType.ID == mTypeDeductionMecID) //Saving
                {
                    mMEC = new Facture_Prelevement();
                    mMEC.DeductionID = mDeduction.ID;
                    mMEC.MontantPreleve = mDeduction.Montant;
                    mMEC.TypeID = mDeduction.DeductionType.ID;
                    mMEC.Libelle = mDeduction.Libelle;
                    mMEC.ElementTypeID = mDeduction.ElementTypeID;
                    mListOfMEC.Add(mMEC);
                }
            }

            mclass.ListOfMecDeducted = JSON.Serialize(mListOfMEC);

            viewModel._MiseEnCompte = mclass;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Facture_Saving", Model = viewModel };

        }

        public ActionResult OnOtherDeduction(string ItemOthersDeductions)
        {
            MiseEnCompteViewModel viewModel = new MiseEnCompteViewModel();
            MiseEnCompte mclass = new MiseEnCompte();

            //Get amount availaible            
            decimal mAmountToPay = Convert.ToDecimal(GetFormValue("txtAmount"));
            decimal mNetWeight = Convert.ToDecimal(GetFormValue("txtNetWeight"));

            mclass.MontantDisponible = mAmountToPay;
            mclass.PoidsNet = mNetWeight;

            //Get list of savings 
            List<FactureDeduction> ListOfDeductions = JSON.Deserialize<List<FactureDeduction>>(ItemOthersDeductions, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            Facture_Prelevement mOther = null;
            FactureDeductionType mDeductionType = null;
            List<Facture_Prelevement> mListOfOthers = new List<Facture_Prelevement>();
            foreach (FactureDeduction mDeduction in ListOfDeductions)
            {
                mDeductionType = new FactureDeductionType(mDeduction.DeductionType.ID);

                if (mDeduction.DeductionType.IsOtherDeduction) //Other deduction
                {
                    mOther = new Facture_Prelevement();
                    mOther.DeductionID = mDeduction.ID;
                    mOther.MontantPreleve = mDeduction.Montant;
                    mOther.TypeID = mDeduction.DeductionType.ID;
                    mOther.Libelle = mDeduction.Libelle;
                    mListOfOthers.Add(mOther);
                }
            }

            mclass.ListOfMecDeducted = JSON.Serialize(mListOfOthers);

            viewModel._MiseEnCompte = mclass;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Facture_OtherDeduction", Model = viewModel };

        }

        public ActionResult OnIncludeFinancing(string ItemSelected)
        {

            try
            {
                Financement mClass = JSON.Deserialize<Financement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);

                if (!result)
                    throw new Exception("OnIncludeFinancing : Financing loading failed.");

                mClass.IsDeducted = true;

                Store mstore = X.GetCmp<Store>("storeListDeductedFinancing");

                ModelProxy mProxy = mstore.GetById(mClass.ID);

                mProxy.BeginEdit();

                mProxy.Set(mClass);

                mProxy.Commit();

                mProxy.EndEdit();

                X.GetCmp<Button>("btnIncludeFinancing").Disable();
                X.GetCmp<Button>("btnExcludeFinancing").Enable();

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Financement : Include",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnExcludeFinancing(string ItemSelected)
        {
            try
            {
                Financement mClass = JSON.Deserialize<Financement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);

                if (!result)
                    throw new Exception("OnExcludeFinancing : Financing loading failed.");

                mClass.IsDeducted = false;

                Store mstore = X.GetCmp<Store>("storeListDeductedFinancing");

                ModelProxy mProxy = mstore.GetById(mClass.ID);

                mProxy.BeginEdit();

                mProxy.Set(mClass);

                mProxy.Commit();

                mProxy.EndEdit();

                X.GetCmp<Button>("btnIncludeFinancing").Enable();
                X.GetCmp<Button>("btnExcludeFinancing").Disable();
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Financement : Exclude",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnUpdateDeductedFinancing(string ItemSelected)
        {
            Financement mclass = JSON.Deserialize<Financement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore});

            FinancementViewModel viewModel = new FinancementViewModel();

            viewModel._Financement = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Facture_Financing_Update", Model = viewModel };
        }

        public ActionResult OnRefreshForAvailableDeliveryNotes(string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd)
        {
            Store mstore = X.GetCmp<Store>("storeListDeliveryNotes");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd)
                                });

            string title = X.GetCmp<FormPanel>("CriteriaPanelDeliveryNote").Title;

            title += " Fournisseur : " + X.GetCmp<ComboBox>("cmbFournisseurFD").SelectedItem.Text;

            title += ", From " + X.GetCmp<DateField>("dtpStartDateFD").RawText.ToString() + " to " + X.GetCmp<DateField>("dtpEndDateFD").RawText.ToString();

            X.GetCmp<FormPanel>("CriteriaPanelDeliveryNote").Title = title;

            // collapse criterias areas
            X.GetCmp<FormPanel>("CriteriaPanelDeliveryNote").Collapse(Direction.Top, false);

            return this.Direct();
        }

        public ActionResult LoadListOfAvailableDeliveryNotes(string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd, string ItemSite)
        {
            int Fournisseur = ItemFournisseur == "" ? -1 : int.Parse(ItemFournisseur);
            int SiteID = ItemSite == "" ? -1 : int.Parse(ItemSite);
            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            var mListe = (new BonDeLivraison()).fnSelectAvailableForInvoice(Fournisseur, StartDate, EndDate,-1,SiteID);
            return this.Store(mListe);
        }

        public ActionResult LoadListOfAvailableFinancing(string ItemFournisseur, string ItemsFinancing)
        {
            int Fournisseur = ItemFournisseur == "" ? -1 : int.Parse(ItemFournisseur);
            var mListe = (new Financement()).fnSelectForInvoiceDeduction(Fournisseur);

            List<Facture_Prelevement> ListOfFinancingsDeducted = JSON.Deserialize<List<Facture_Prelevement>>(ItemsFinancing, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                        
            foreach (Financement item in mListe)
            {
                foreach (Facture_Prelevement itemDeducted in ListOfFinancingsDeducted)
                {
                    if (item.Numero == itemDeducted.Numero)
                    {
                        item.MontantPreleve = itemDeducted.MontantPreleve;
                        item.IsDeducted = true;
                    }

                }

            }

            return this.Store(mListe);
        }

        List<Facture_Prelevement> ListOfSavings = null;

        public ActionResult LoadListOfAvailableMiseEnCompteType(string ItemsSaving)
        {
            ListOfSavings = JSON.Deserialize<List<Facture_Prelevement>>(ItemsSaving, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            var mListeTypeMec = new MiseEnCompteType().fnSelect();
            var mListeTypeMecToDisplay = new List<MiseEnCompteType>();          

            foreach (MiseEnCompteType mMecType in mListeTypeMec)
            {
                if (!IsMecType(mMecType.ID))
                {
                    mListeTypeMecToDisplay.Add(mMecType);
                }
                
            }

            return this.Store(mListeTypeMecToDisplay);
        }

        public ActionResult LoadListOfAvailableOthersDeductions(string ItemsSaving)
        {
            ListOfSavings = JSON.Deserialize<List<Facture_Prelevement>>(ItemsSaving, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            var mListeTypeMec = new FactureDeductionType().fnSelectForInvoice();
            var mListeTypeMecToDisplay = new List<FactureDeductionType>();

            foreach (FactureDeductionType mMecType in mListeTypeMec)
            {
                if (!IsOtherDeductionType(mMecType.ID))
                {
                    mListeTypeMecToDisplay.Add(mMecType);
                }

            }

            return this.Store(mListeTypeMecToDisplay);
        }


        private bool IsMecType(int mMecTypeID)
        {
            bool mReturn = false;
            foreach (Facture_Prelevement mPrelevement in ListOfSavings)
            {
                if (mPrelevement.ElementTypeID == mMecTypeID)
                {
                    mReturn = true;
                    break;
                }
            }

            return mReturn;
        }

        private bool IsOtherDeductionType(int mMecTypeID)
        {
            bool mReturn = false;
            foreach (Facture_Prelevement mPrelevement in ListOfSavings)
            {
                if (mPrelevement.TypeID == mMecTypeID)
                {
                    mReturn = true;
                    break;
                }
            }

            return mReturn;
        }

        public ActionResult LoadListOfValorisation(string ItemFacture, string ItemExecMode)
        {
            List<DataPersist> myList = new List<DataPersist>();
            if (!string.IsNullOrEmpty(ItemFacture))
            {

                if (ItemExecMode == "AddNew")
                {
                    myList = new FactureValorisation().fnSelect(new Guid());
                }
                else
                {
                    myList = new FactureValorisation().fnSelect(Guid.Parse(ItemFacture));
                }

                FactureValorisation mClass = new FactureValorisation();
                if (myList.Count > 0)
                    mClass = myList[0] as FactureValorisation;
                else myList = new List<DataPersist>();

            }
            else
            {
                Store mstore = X.GetCmp<Store>("storeListValorisation");
                mstore.RemoveAll();
                myList = null;
            }

            return this.Store(myList);
        }

        public ActionResult LoadListOfDeduction(string ItemFacture, string ItemExecMode)
        {
            List<DataPersist> myList = new List<DataPersist>();
            if (!string.IsNullOrEmpty(ItemFacture))
            {

                if (ItemExecMode == "AddNew")
                {
                    myList = new FactureDeduction().fnSelect(new Guid());
                }
                else
                {
                    myList = new FactureDeduction().fnSelect(Guid.Parse(ItemFacture));
                }

                FactureDeduction mClass = new FactureDeduction();
                if (myList.Count > 0)
                    mClass = myList[0] as FactureDeduction;
                else myList = new List<DataPersist>();

            }
            else
            {
                Store mstore = X.GetCmp<Store>("storeListDeduction");
                mstore.RemoveAll();
                myList = null;
            }

            return this.Store(myList);
        }

        public ActionResult SubmitDeliveryNumber(string ItemDeliveryNumber)
        {
            try
            {
                BonDeLivraison mclass = new BonDeLivraison();
                if (string.IsNullOrEmpty(ItemDeliveryNumber))
                {
                    return this.Direct();
                }
                if (mclass.fnGetByDeliveryNumber(ItemDeliveryNumber))
                {
                    if (mclass.ID != Guid.Empty)
                    {
                        MapDeliveryNoteToForm(mclass);
                        MapValorisationAndDeductionToForm(mclass);
                    }
                    else
                    {
                        X.MessageBox.Show(new MessageBoxConfig
                        {
                            Title = "Facture : Bon De Reception",
                            Message = "Bon De Reception not Found, Please Retry !",
                            Buttons = MessageBox.Button.OK,
                            Icon = MessageBox.Icon.WARNING
                        });
                        return this.Direct();
                    }                    
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bon De Reception : Submit Delivery",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();

        }


        public ActionResult SubmitDeliveryNote(string ItemSelected)
        {
            try
            {
                string UserName = (string)Session["userName"];
                Fonction HasAccess = new Fonction();
                bool HavPermissionCreer = HasAccess.fnGetUserAccessStatus("{9C2F639D-9566-4CD2-89EA-93023EDF704C}", UserName);
                bool HavPermissionCreerSurSite = HasAccess.fnGetUserAccessStatus("{962d64a0-199c-4c7f-ba9f-fd512a61b7a0}", UserName);
                if (HavPermissionCreer || HavPermissionCreerSurSite)
                {

                    BonDeLivraison mclass = JSON.Deserialize<BonDeLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                    MapDeliveryNoteToForm(mclass);

                    MapValorisationAndDeductionToForm(mclass);

                    X.GetCmp<Window>("ListOfDeliveryNotes").Close();
                }
                else
                {
                    X.GetCmp<Window>("ListOfDeliveryNotes").Close();
                    X.GetCmp<Window>("Facture_Detail").Close();
                    return this.Direct();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture : SubmitDeliveryNote",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();

        }

        public ActionResult SubmitDeductedFinancings(string ItemSelected, string ItemFinancings, string ItemNbDeductions)
        {
            List<Financement> ListOfFinancingsDeducted = JSON.Deserialize<List<Financement>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            List<Facture_Prelevement> ListOfFinancingsToBeRemoved = JSON.Deserialize<List<Facture_Prelevement>>(ItemFinancings, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            X.GetCmp<Window>("ListOfDeductedFinancing").Close();

            //Gets list of existing financings to be removed and remove            
            Store mstore = X.GetCmp<Store>("storeListDeduction");           
            ModelProxy mProxy = null;
            int NbFinancingsDropped = 0;

            //Remove all existings financings from the list of deductions
            foreach (Facture_Prelevement itemDeducted in ListOfFinancingsToBeRemoved)
            {
                mProxy = mstore.GetById(itemDeducted.DeductionID);
                mProxy.Drop();
                NbFinancingsDropped += 1;
            }

            //Add financings deducted in the list of deductions           
            FactureDeduction mFactureDeduction = null;
            int NbDeductions = int.Parse(ItemNbDeductions);
            int StartPoint = NbDeductions - NbFinancingsDropped;

            foreach (Financement item in ListOfFinancingsDeducted)
            {
                mFactureDeduction = new FactureDeduction();
                mFactureDeduction.ID = Guid.NewGuid();
                mFactureDeduction.DeductionType = new FactureDeductionType(mTypeDeductionAvanceID);
                //mFactureDeduction.DeductionType.ID = 2;
                //mFactureDeduction.DeductionType = new FactureDeductionType(item.FinancementType.ID);
                if (mTypeDeductionAvanceID == 2)
                {
                    mFactureDeduction.Libelle = "Financing ( " + item.FinancementType.Designation + " )";
                }
                else
                    mFactureDeduction.Libelle = mFactureDeduction.DeductionType.Designation;

                mFactureDeduction.ElementID = item.ID;
                mFactureDeduction.ElementRef = item.Numero;
                mFactureDeduction.Taux = item.PrelevementTaux;
                mFactureDeduction.Montant = item.MontantPreleve;
                mstore.Insert(StartPoint, mFactureDeduction);
                StartPoint += 1;
            }

            return this.Direct();
        }

        public ActionResult SubmitDeductedSavings(string ItemSavings, string ItemType, string ItemAmount, string ItemRate)
        {
            List<Facture_Prelevement> ListOfSavings = JSON.Deserialize<List<Facture_Prelevement>>(ItemSavings, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            int mTypeID = int.Parse(ItemType);
            MiseEnCompteType mMecType = new MiseEnCompteType(mTypeID);
            decimal mMontant = decimal.Parse(ItemAmount);
            decimal mTaux = ItemRate != string.Empty ? decimal.Parse(ItemRate) : 0;

            X.GetCmp<Window>("SavingDeduction").Close();

            //Gets list of existing financings to be removed and remove            
            Store mstore = X.GetCmp<Store>("storeListDeduction");          

            //Update saving deducted in the list of deductions           
            FactureDeduction mFactureDeduction = null;
            
            mFactureDeduction = new FactureDeduction();
            mFactureDeduction.ID = Guid.NewGuid();
            mFactureDeduction.DeductionType = new FactureDeductionType(mTypeDeductionMecID);
            string mTypeDesignation = mFactureDeduction.DeductionType.Designation + "(" + mMecType.Designation + ")";

            //mFactureDeduction.DeductionType.ID = 3;
            mFactureDeduction.Libelle = mTypeDesignation;
            mFactureDeduction.ElementID = Guid.Empty;
            mFactureDeduction.ElementRef = string.Empty;
            mFactureDeduction.Taux = mTaux;
            mFactureDeduction.Montant = mMontant;
            mFactureDeduction.ElementTypeID = mTypeID;
            mstore.Add(mFactureDeduction);                         
                        
            return this.Direct();
        }

        public ActionResult SubmitDeductedOthers(string ItemSavings, string ItemType, string ItemAmount, string ItemRate)
        {
            List<Facture_Prelevement> ListOfSavings = JSON.Deserialize<List<Facture_Prelevement>>(ItemSavings, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            int mTypeID = int.Parse(ItemType);
            decimal mtauc = 0;
            double ftauc = 0;
            bool res = decimal.TryParse(ItemRate, out mtauc);
            res = double.TryParse(ItemRate, out ftauc);

            FactureDeductionType mMecType = new FactureDeductionType(mTypeID);
            string mTypeDesignation = mMecType.Designation;
            decimal mMontant = decimal.Parse(ItemAmount);
            double mTauxf = ItemRate != string.Empty ? double.Parse(ItemRate) : 0;
            decimal mTaux = ItemRate != string.Empty ? (decimal)double.Parse(ItemRate) : 0;

            X.GetCmp<Window>("SavingOthers").Close();

            Store mstore = X.GetCmp<Store>("storeListDeduction");

            //Update saving deducted in the list of deductions           
            FactureDeduction mFactureDeduction = null;

            mFactureDeduction = new FactureDeduction();
            mFactureDeduction.ID = Guid.NewGuid();
            mFactureDeduction.DeductionType = new FactureDeductionType();
            mFactureDeduction.DeductionType.ID = mTypeID;
            mFactureDeduction.DeductionType.IsOtherDeduction = true;
            mFactureDeduction.Libelle = mTypeDesignation;
            mFactureDeduction.ElementID = Guid.Empty;
            mFactureDeduction.ElementRef = string.Empty;
            mFactureDeduction.Taux = mTaux;
            mFactureDeduction.Montant = mMontant;
            mFactureDeduction.ElementTypeID = mTypeID;
            mstore.Add(mFactureDeduction);

            return this.Direct();
        }

        //public ActionResult SubmitDeductedOthers(string ItemSavings, string ItemType, string ItemAmount, string ItemRate)
        //{
        //    List<Facture_Prelevement> ListOfSavings = JSON.Deserialize<List<Facture_Prelevement>>(ItemSavings, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

        //    int mTypeID = int.Parse(ItemType);
        //    FactureDeductionType mMecType = new FactureDeductionType(mTypeID);
        //    string mTypeDesignation = mMecType.Designation;
        //    decimal mMontant = decimal.Parse(ItemAmount);
        //    decimal mTaux = ItemRate != string.Empty ? decimal.Parse(ItemRate) : 0;

        //    X.GetCmp<Window>("SavingOthers").Close();

        //    Store mstore = X.GetCmp<Store>("storeListDeduction");

        //    //Update saving deducted in the list of deductions           
        //    FactureDeduction mFactureDeduction = null;

        //    mFactureDeduction = new FactureDeduction();
        //    mFactureDeduction.ID = Guid.NewGuid();
        //    mFactureDeduction.DeductionType = new FactureDeductionType();
        //    mFactureDeduction.DeductionType.ID = mTypeID;
        //    mFactureDeduction.DeductionType.IsOtherDeduction = true;
        //    mFactureDeduction.Libelle = mTypeDesignation;
        //    mFactureDeduction.ElementID = Guid.Empty;
        //    mFactureDeduction.ElementRef = string.Empty;
        //    mFactureDeduction.Taux = mTaux;
        //    mFactureDeduction.Montant = mMontant;
        //    mFactureDeduction.ElementTypeID = mTypeID;
        //    mstore.Add(mFactureDeduction);

        //    return this.Direct();
        //}


        private void MapDeliveryNoteToForm(BonDeLivraison mClass)
        {
            try
            {
                X.GetCmp<TextField>("txtSupplier").Text = string.Empty;
                X.GetCmp<TextField>("txtCertification").Text = string.Empty;
                X.GetCmp<TextField>("txtTruckID").Text = string.Empty;
                X.GetCmp<TextField>("txtDeliveryType").Text = string.Empty;
                X.GetCmp<TextField>("txtAcceptedBags").Text = string.Empty;
                X.GetCmp<TextField>("txtNetWeight").Text = string.Empty;
                X.GetCmp<TextField>("txtTotalValorization").Text = string.Empty;
                X.GetCmp<TextField>("txtTotalDeduction").Text = "0";
                X.GetCmp<TextField>("txtAmount").Text = string.Empty;

                X.GetCmp<Hidden>("hiddenBonDeLivraisonID").Text = mClass.ID.ToString();
                X.GetCmp<Hidden>("hiddenLivraisonID").Text = mClass.Livraison.ID.ToString();
                X.GetCmp<Hidden>("hiddenFournisseurID").Text = mClass.Livraison.Fournisseur.ID.ToString();
                X.GetCmp<TextField>("txtDeliveryID").Text = mClass.LivraisonID;
                X.GetCmp<DateField>("dtfDeliveryDate").SelectedDate = mClass.DateLivraison;
                X.GetCmp<TimeField>("tmfDelivery").SelectedTime = TimeSpan.FromTicks(mClass.DateLivraison.Ticks);
                X.GetCmp<TextField>("txtSupplier").Text = mClass.FournisseurNom;

                if (mClass.Livraison != null && mClass.Livraison.Certification != null)
                    X.GetCmp<TextField>("txtCertification").Text = mClass.Livraison.Certification.Designation;
                else
                    X.GetCmp<TextField>("txtCertification").Text = string.Empty;

                //X.GetCmp<TextField>("txtCertification").Text = mClass.Livraison.Certification.Designation;
                X.GetCmp<TextField>("txtTruckID").Text = mClass.Immatriculation;
                X.GetCmp<TextField>("txtDeliveryType").Text = mClass.LibelleTypeDeLivraison;
                X.GetCmp<TextField>("txtAcceptedBags").Text = mClass.NbreSacs.ToString();
                X.GetCmp<TextField>("txtNetWeight").Text = mClass.PoidsNetAccepte.ToString();

                X.GetCmp<TextField>("txtNomSite").Text = mClass.Livraison.Site.Nom;
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture : MapDeliveryNoteToForm",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
        }

        private void MapValorisationAndDeductionToForm(BonDeLivraison mClass)
        {
            try
            {
                var mListe = (new FactureValorisation()).fnSelectAvailable(mClass.ID);                              

                Store store = X.GetCmp<Store>("storeListValorisation");
                store.RemoveAll();

                Store mstore = X.GetCmp<Store>("storeListDeduction");
                mstore.RemoveAll();

                var i = 0;

                foreach (var item in mListe)
                {
                    i += 1;
                    item.IsNew = true;
                    store.Insert(i, item);
                }

                X.GetCmp<Button>("BtnOkDetail").Disable();

                if (mListe.Count >= 1)
                {
                    X.GetCmp<Button>("BtnOkDetail").Enable();
                    X.GetCmp<Button>("btnSaving").Enable();
                    X.GetCmp<Button>("btnFinancing").Enable();
                    X.GetCmp<Button>("btnOtherDeduction").Enable();

                    //Get sum of valorization (server side)
                    decimal mTotalValorisation = 0;

                    var j = 0;
                    FactureValorisation mFactureValorisation = new FactureValorisation();
                    FactureValorisation mFactureListe = new FactureValorisation();
                    for (j = 0; j < mListe.Count; j++)
                    {
                        mFactureValorisation = mListe[j] as FactureValorisation;
                        mTotalValorisation = mTotalValorisation + mFactureValorisation.Montant;
                    }

                    Parametres mParam = new Parametres(0);
                    mFactureListe = mListe.Cast<FactureValorisation>().Where(x => x.ValorisationType.ID == 3).ToList().FirstOrDefault();

                    FactureDeduction mDed = new FactureDeduction();
                    mDed.ElementID = mClass.ID;
                    bool EstValide = mDed.fnBicValide();

                    if (!EstValide && mFactureListe != null)
                    {                        
                        FactureDeductionType mType = new FactureDeductionType(mParam.DefDeductionTypeBIC);
                        mDed = new FactureDeduction();
                        mDed.ID = Guid.NewGuid();
                        mDed.Facture = new Facture();
                        mDed.Facture.ID = Guid.Empty;
                        mDed.DeductionType = new FactureDeductionType();
                        mDed.DeductionType = mType;
                        mDed.ElementID = mFactureListe.ID;
                        mDed.ElementRef = mFactureListe.TarificationRef;
                        mDed.Taux = (decimal)mType.Taux;
                        mDed.Libelle = mType.Designation;
                        mDed.Montant = Math.Round((mFactureListe.Montant * (decimal)mType.Taux) / 100,0);
                    }

                    MapDeductionToForm(mClass, mTotalValorisation, mDed);                    
                }
                else
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Facture : Create",
                        Message = "No Pricing found. Delivery cannot be invoiced",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture : MapValorisationToForm",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

        }

        private void MapDeductionToForm(BonDeLivraison mClass, decimal mTotalValorisation, FactureDeduction mDed = null )
        {
            try
            {
                //Get the list of deductions
                var mListe = (new FactureDeduction()).fnSelectAvailable(mClass.ID, mTotalValorisation);

                Store store = X.GetCmp<Store>("storeListDeduction");
                store.RemoveAll();
                var i = 0;

                foreach (FactureDeduction item in mListe)
                {
                    i += 1;
                    item.IsNew = true;                    
                    store.Insert(i, item);
                }
                if ((mDed != null) && (mDed.ID != Guid.Empty))
                    store.Add(mDed);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture : MapDeductionToForm",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

        }

        private Facture MapFormToObject(Facture mClass)
        {
            try
            {
                mClass.UtilisateurCreation = (string)Session["userName"];

                BonDeLivraison mBonDeLivraison = new BonDeLivraison();
                mBonDeLivraison.fnGet(Guid.Parse(X.GetCmp<Hidden>("hiddenBonDeLivraisonID").Text));
                //mBonDeLivraison.ID = Guid.Parse(X.GetCmp<Hidden>("hiddenBonDeLivraisonID").Text);
                mClass.BonDeLivraison = mBonDeLivraison;

                mClass.DateFacture = DateTime.Parse(GetFormValue("dtfInvoiceDate") + " " + GetFormValue("tmfInvoice"));
                mClass.Montant = Convert.ToDecimal(GetFormValue("txtAmount"));
                mClass.NbreSacs = int.Parse(GetFormValue("txtAcceptedBags"));
                mClass.PoidsNetAccepte = Convert.ToDecimal(GetFormValue("txtNetWeight"));
                mClass.NumeroSticker = string.Empty;

                mClass.Sites = new Site();
                bool result = mClass.Sites.fnGetBySiteByUserName((string)Session["userName"]);
                //mClass.NumeroSticker = GetFormValue("txtNumSticker");                

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture : MapFormToObject",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return mClass;

        }

        public ActionResult OnCancel(string ItemSelected)
        {

            try
            {
                Facture mClass = JSON.Deserialize<Facture>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);

                if (!result)
                    throw new Exception("OnCancel : Facture loading failed.");

                mClass.UtilisateurModification = (string)Session["userName"];

                if (mClass.fnCancel())
                {
                    Store mstore = X.GetCmp<Store>("storeListeFacture");

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
                    Title = "Facture : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        [HttpPost]
        public ActionResult UpdateFormMethod(string ListeOfValorization, string ListeOfDeduction)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                Facture mFacture = new Facture();
                FactureValorisation mValorisation = null;
                FactureDeduction mDeduction = null;

                mFacture.IsNew = true;               

                bool result = false;
                bool resultValorisation = true;
                bool resultDeduction = true;

                mFacture = MapFormToObject(mFacture);

                _db = mFacture.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = mFacture.fnUpdate(mtran);

                if (result)
                {
                    List<FactureValorisation> ItemValorisation = JSON.Deserialize<List<FactureValorisation>>(ListeOfValorization, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (ItemValorisation.Count > 0)
                    {
                        for (int i = 0; i < ItemValorisation.Count(); i++)
                        {
                            mValorisation = new FactureValorisation();
                            mValorisation.ValorisationType = new FactureValorisationType();
                            mValorisation.Facture = new Facture();

                            mValorisation.SetDataSource(_db);

                            mValorisation.Facture.ID = mFacture.ID;
                            mValorisation.ValorisationType.ID = ItemValorisation[i].ValorisationType.ID;
                            mValorisation.TarificationID = ItemValorisation[i].TarificationID;
                            mValorisation.TarificationRef = ItemValorisation[i].TarificationRef;
                            mValorisation.Tonnage = ItemValorisation[i].Tonnage;
                            mValorisation.Prix = ItemValorisation[i].Prix;
                            mValorisation.Montant = ItemValorisation[i].Montant;
                            mValorisation.UtilisateurCreation = (string)Session["userName"];

                            resultValorisation = mValorisation.fnUpdate(mtran);

                            if (!resultValorisation) break;                            
                         }
                      }

                      List<FactureDeduction> ItemDeduction = JSON.Deserialize<List<FactureDeduction>>(ListeOfDeduction, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                     if (ItemDeduction.Count > 0)
                     {
                        for (int i = 0; i < ItemDeduction.Count(); i++)
                        {
                            mDeduction = new FactureDeduction();
                            mDeduction.DeductionType = new FactureDeductionType();
                            mDeduction.Facture = new Facture();

                            mDeduction.SetDataSource(_db);

                            mDeduction.Facture.ID = mFacture.ID;
                            mDeduction.DeductionType.ID = ItemDeduction[i].DeductionType.ID;
                            mDeduction.ElementID = ItemDeduction[i].ElementID;
                            mDeduction.ElementRef = ItemDeduction[i].ElementRef;
                            mDeduction.Taux = ItemDeduction[i].Taux;
                            mDeduction.Montant = ItemDeduction[i].Montant;
                            mDeduction.UtilisateurCreation = (string)Session["userName"];
                            mDeduction.Libelle = ItemDeduction[i].Libelle;
                            mDeduction.ElementTypeID = ItemDeduction[i].ElementTypeID;

                            resultDeduction = mDeduction.fnUpdate(mtran);

                            if (!resultDeduction) break;                            
                        }
                    }

                    if (!resultValorisation || !resultDeduction)
                    {
                        _db.RollBackTransaction(mtran);
                    }
                    _db.CommitTransaction(mtran);

                    Store mstore = X.GetCmp<Store>("storeListeFacture");
                    mstore.Insert(0, mFacture);
                    X.GetCmp<RowSelectionModel>("rowSelectionListeFacture").Select(0);

                    X.GetCmp<Window>("Facture_Detail").Close();

                }

               
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture : UpdateForm",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        //[HttpPost]
        public ActionResult UpdateFinancingDeducted(string FinancementId, string MntDeduction)
        {
            try
            {
                Financement mClass = new Financement();
                //string id = X.GetCmp<Hidden>("txtFinancementDeductedID").Value.ToString();
                mClass.fnGet(Guid.Parse(FinancementId));

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("UpdateFinancingDeducted : Financing load failed.");

                if (MntDeduction != string.Empty)
                {
                    MntDeduction = MntDeduction.Replace(" ","");
                    mClass.MontantPreleve = decimal.Parse(MntDeduction);
                }

                mClass.IsDeducted = mClass.MontantPreleve > 0 ? true : false;
               
                Store mStore = X.GetCmp<Store>("storeListDeductedFinancing");

                ModelProxy mProxy;
                    
                mProxy = mStore.GetById(mClass.ID);

                mProxy.BeginEdit();

                mProxy.Set(mClass);

                mProxy.Commit();

                mProxy.EndEdit();
                   

                X.GetCmp<Window>("FinancingDeduction_Update").Close();
                //Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                //mViewport.Unmask();
                
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Deducted Financement : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite)
        {

            int FournisseurID = GetCriteriaValue(ItemFournisseur);
            int TypeID = GetCriteriaValue(ItemType);
            
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
            int SiteID = GetCriteriaValue(ItemSite);

            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "{Tous}";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string Status = ItemStatus;
            //string Status = ItemStatus == "null" ? "%%" : ItemStatus;

            var mListe = (new Facture()).fnSelect(Campagne, FournisseurID, TypeID, StartDate, EndDate, Status, SiteID);
            string filterHeaders = this.Request.Params["filterheader"];
            //var paging = GridStorePaging.SetRangePlants(parameters, mListe);

            //return this.Store(GridStorePaging.SetRangePlants(parameters, mListe, filterHeaders));
            return this.Store(mListe);
        }        

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelFA");

            mform.ToggleCollapse();
            //mform.Collapsed = false;
            return this.Direct();
        }        

        public ActionResult OnRefresh(string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite, string ItemOption)
        {
            try
            {
                Store mstore = X.GetCmp<Store>("storeListeFacture");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemCampagne"      ,ItemCampagne),
                                    new Ext.Net.Parameter("ItemType"          ,ItemType),
                                    new Ext.Net.Parameter("ItemSite"          ,ItemSite),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus),
                                    new Ext.Net.Parameter("ItemOption"        ,ItemOption)
                                });

                string title = X.GetCmp<FormPanel>("CriteriaPanelFA").Title;
                title += "Site = " + X.GetCmp<ComboBox>("cmbSite").SelectedItem.Text;

                title += ", Campagne : " + X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                title += ", Fournisseur : " + X.GetCmp<ComboBox>("cmbFournisseur").SelectedItem.Text;

                title += ", Type De Livraison = " + X.GetCmp<ComboBox>("cmbType").SelectedItem.Text;

                title += ", From " + X.GetCmp<DateField>("dtpStartDate").RawText.ToString() + "to " + X.GetCmp<DateField>("dtpEndDate").RawText.ToString();

                X.GetCmp<FormPanel>("CriteriaPanelFA").Title = title;

                // collapse criterias areas
                X.GetCmp<FormPanel>("CriteriaPanelFA").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Delivery Note : OnRefresh",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnRemoveDeduction(string ItemSelected)
        {

            try
            {
                FactureDeduction mClass = JSON.Deserialize<FactureDeduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                decimal mTotalDeduction = Convert.ToDecimal(X.GetCmp<Hidden>("txtTotalDeduction").Text);
                decimal mTotalValorisation = Convert.ToDecimal(X.GetCmp<Hidden>("txtTotalValorization").Text);

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);

                if (!result)
                    throw new Exception("OnRemoveDeduction : Facture loading failed.");

                //else if(mClass.DeductionType.ID == mTypeDeductionLegalTaxID)
                //{
                //    throw new Exception("Impossible to remove this type of deduction");                    
                //}

                else
                {
                    Store mstore = X.GetCmp<Store>("storeListDeduction");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.Drop();                   

                    DeselectGridRows();

                    //mTotalDeduction = mTotalDeduction - mClass.Montant;
                    //X.GetCmp<TextField>("txtTotalDeduction").Text = Math.Round(mTotalDeduction).ToString();
                    //X.GetCmp<TextField>("txtAmount").Text = Math.Round(mTotalValorisation - mTotalDeduction).ToString();

                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture : Retirer deduction",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        private string GetFormValue(string id_Component)
        {
            string data = Request.Form[id_Component];
            return string.IsNullOrEmpty(data) ? string.Empty : data;
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
            X.GetCmp<RowSelectionModel>("rowSelectionDeduction").DeselectAll();
        }

        public ActionResult OnPrintInvoice(string IdFacture, string IsCopy)
        {
            bool ReportIscopy = false;
            if (string.IsNullOrEmpty(IsCopy))
                ReportIscopy = true;
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Facture{0}', '{1}/Facture/ViewReport?id={0}&IsCopy={2}', this, 'Facture','')", IdFacture, BaseUrl, ReportIscopy));
        }

        public ActionResult ViewReport(string id, bool IsCopy)
        {
            try
            {
                rptInvoice report = new rptInvoice();

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["ID"].Value = id;
                report.Parameters["IsCopy"].Value = IsCopy;

                ViewData["Report"] = report;
                //PrintMethod.Print(report);
                return View();
            }
            catch (Exception ex)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture : Printing",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
            
        }

        public ActionResult OnPrintHystoryOfInvoice(string cropYear, string deliveryType, string supplier, string startDate, string endDate, string supplierName, string deliveryTypeDesignation)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                Session["paramCampagne"] = cropYear;
                if (cropYear.Contains("{Tous}"))
                    Session["campagneID"] =  "";
                else
                    Session["campagneID"] = cropYear;

                Session["DeliveryTypeId"] = Int32.Parse(deliveryType);
                Session["DeliveryTypeNom"] = deliveryTypeDesignation;

                Session["fournisseurID"] = Int32.Parse(supplier);                

                Session["DateDebut"] = dateDebut;
                Session["DateFin"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Fournisseur/ViewReportResult', this, 'Historique des factures',''),App.frmSupplierPrintCriteria.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }


        }

        public ActionResult OnDisplayInvoiceList(string ItemPeriodStart = "", string ItemPeriodEnd = "")
        {
            string UserName = (string)Session["userName"];

            if (string.IsNullOrEmpty(ItemPeriodStart)) ItemPeriodStart = DateTime.Now.ToShortDateString();
            if (string.IsNullOrEmpty(ItemPeriodEnd)) ItemPeriodEnd = DateTime.Now.ToShortDateString();
            DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
            DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);
            ViewData["datedebut"] = datedebut;
            ViewData["datefin"] = datedfin;

            Site mSiteParDefaut = new Site();
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            Parametres mParam = new Parametres(0);

            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;
            if (mParam.Site == mSiteParDefaut.ID) ViewData["UrlSite"] = "LoadSiteAll";
            else ViewData["UrlSite"] = "LoadSiteByAccess";

            ViewData["Titre"] = "Liste des factures";
            ViewData["actionToDo"] = "OnPrintInvoiceList";
            ViewData["ControllerName"] = "Facture";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmCriteriaForBonDeLivraison", ViewData = ViewData };
        }

        public ActionResult OnDisplayValuationList(string ItemPeriodStart = "", string ItemPeriodEnd = "")
        {
            string UserName = (string)Session["userName"];

            if (string.IsNullOrEmpty(ItemPeriodStart)) ItemPeriodStart = DateTime.Now.ToShortDateString();
            if (string.IsNullOrEmpty(ItemPeriodEnd)) ItemPeriodEnd = DateTime.Now.ToShortDateString();
            DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
            DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);
            ViewData["datedebut"] = datedebut;
            ViewData["datefin"] = datedfin;

            Site mSiteParDefaut = new Site();
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            Parametres mParam = new Parametres(0);

            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;
            if (mParam.Site == mSiteParDefaut.ID) ViewData["UrlSite"] = "LoadSiteAll";
            else ViewData["UrlSite"] = "LoadSiteByAccess";

            ViewData["Titre"] = "Liste Des Valorisations";
            ViewData["actionToDo"] = "OnPrintValuationList";
            ViewData["ControllerName"] = "Facture";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmPeriodWithSupplierForReport", ViewData = ViewData };
        }

        public ActionResult OnDisplayAvailableDeliveries(string ItemPeriodStart = "", string ItemPeriodEnd = "")
        {
            string UserName = (string)Session["userName"];

            if (string.IsNullOrEmpty(ItemPeriodStart)) ItemPeriodStart = DateTime.Now.ToShortDateString();
            if (string.IsNullOrEmpty(ItemPeriodEnd)) ItemPeriodEnd = DateTime.Now.ToShortDateString();
            DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
            DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);
            ViewData["datedebut"] = datedebut;
            ViewData["datefin"] = datedfin;

            Site mSiteParDefaut = new Site();
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            Parametres mParam = new Parametres(0);

            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;
            if (mParam.Site == mSiteParDefaut.ID) ViewData["UrlSite"] = "LoadSiteAll";
            else ViewData["UrlSite"] = "LoadSiteByAccess";
            ViewData["Titre"] = "Liste des Livraisons disponibles";
            ViewData["actionToDo"] = "OnPrintAvailableDeliveries";
            ViewData["ControllerName"] = "Facture";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmPeriodWithSupplierForReport", ViewData = ViewData };
        }

        public ActionResult OnPrintInvoiceList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetStartDate").RawText, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetEndDate").RawText, "d", CultureInfo.CurrentUICulture);

                //Session["paramCropYear"] = cropYear;   
                Session["paramSite"] = GetFormValue("cmbDetSite");
                Session["paramSiteText"] = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text;

                Session["paramCampagne"] = GetFormValue("cmbDetCropYear");
                Session["paramFournisseur"] = GetFormValue("cmbDetFournisseur");
                Session["paramFournisseurText"] = X.GetCmp<ComboBox>("cmbDetFournisseur").SelectedItem.Text;
                Session["paramTypeOfDelivery"] = GetFormValue("cmbDetTypeDelivery");
                Session["paramTypeOfDeliveryText"] = X.GetCmp<ComboBox>("cmbDetTypeDelivery").SelectedItem.Text; ;
                Session["paramStatut"] = GetFormValue("cmbDetStatus");
                Session["paramStatutText"] = X.GetCmp<ComboBox>("cmbDetStatus").SelectedItem.Text; ;

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Facture/ViewReportListResult', this, 'Liste des factures',''),App.frmCriteriaForBonDeLivraison.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }


        }

        public ActionResult OnPrintValuationList(string fournisseur, string fournisseurText, string startDate, string endDate)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                Session["paramSite"] = GetFormValue("cmbDetSite");
                Session["paramSiteText"] = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text;

                //Session["paramCropYear"] = cropYear;                
                Session["paramCampagne"] = "{Tous}";
                Session["paramFournisseur"] = fournisseur;
                Session["paramFournisseurText"] = fournisseurText;               
                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Facture/ViewReportListValuationResult', this, 'List Of Valuation',''),App.frmPeriodWithSupplierForReport.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }


        }
        

        public ActionResult OnPrintAvailableDeliveries(string fournisseur, string fournisseurText, string startDate, string endDate)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                //Session["paramCropYear"] = cropYear; 
                Session["paramSite"] = GetFormValue("cmbDetSite");
                Session["paramSiteText"] = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text;
                Session["paramCampagne"] = "{Tous}";
                Session["paramFournisseur"] = fournisseur;
                Session["paramFournisseurText"] = fournisseurText;
                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Facture/ViewReportListAvailableDeliveriesResult', this, 'List Of Livraisons disponibles',''),App.frmPeriodWithSupplierForReport.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }


        }


        public ActionResult ViewReportListValuationResult()
        {
            //XtraReport report = null;

            rptInvoiceValuationList report = new rptInvoiceValuationList();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            //report.Parameters["paramCampagneText"].Value = Session["paramCampagne"];
            //if (!string.IsNullOrEmpty(Session["paramCampagne"].ToString()) && Session["paramCampagne"].ToString() == "{Tous}")
            //    report.Parameters["paramCampagne"].Value = string.Empty;
            //else
            //    report.Parameters["paramCampagne"].Value = Session["paramCampagne"];

            report.Parameters["paramSite"].Value = Session["paramSite"];
            report.Parameters["paramSiteText"].Value = Session["paramSiteText"];

            report.Parameters["paramFournisseur"].Value = Session["paramFournisseur"];
            report.Parameters["paramFournisseurText"].Value = Session["paramFournisseurText"];

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];
            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

        public ActionResult ViewReportListAvailableDeliveriesResult()
        {
            //XtraReport report = null;

            rptInvoicePendingDeliveries report = new rptInvoicePendingDeliveries();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            //report.Parameters["paramCampagneText"].Value = Session["paramCampagne"];
            //if (!string.IsNullOrEmpty(Session["paramCampagne"].ToString()) && Session["paramCampagne"].ToString() == "{Tous}")
            //    report.Parameters["paramCampagne"].Value = string.Empty;
            //else
            //    report.Parameters["paramCampagne"].Value = Session["paramCampagne"];

            report.Parameters["paramSite"].Value = Session["paramSite"];
            report.Parameters["paramSiteText"].Value = Session["paramSiteText"];

            report.Parameters["paramFournisseur"].Value = Session["paramFournisseur"];
            report.Parameters["paramFournisseurText"].Value = Session["paramFournisseurText"];

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];
            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }


        public ActionResult ViewReportListResult()
        {
            //XtraReport report = null;
            rptInvoiceList report = new rptInvoiceList();
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

            report.Parameters["paramTypeLivraison"].Value = Session["paramTypeOfDelivery"];
            report.Parameters["paramTypeLivraisonText"].Value = Session["paramTypeOfDeliveryText"];

            report.Parameters["paramStatut"].Value = Session["paramStatut"];
            report.Parameters["paramStatutText"].Value = Session["paramStatutText"];

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];
            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

    }
}
