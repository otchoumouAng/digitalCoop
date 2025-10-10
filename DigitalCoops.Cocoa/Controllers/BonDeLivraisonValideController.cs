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
using DevExpress.XtraReports.UI;

namespace Tms2017.MVC.Controllers
{
    public class BonDeLivraisonValideController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";

        Guid mID;

        // GET: BonDeLivraisonValide
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            //bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            bool result = mSiteParDefaut.fnGetDefaultSite();
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            X.GetCmp<ComboBox>("cmbCampagne").SetValue(mParam.Campagne);

            //string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string StartDate = DateTime.Now.ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("dtpStartDate").RawText = EndDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Fournisseur = "{Tous}";

            string Type = "{Tous}";

            X.GetCmp<FormPanel>("CriteriaPanelBL").SetTitle("Site : " + mSiteParDefaut.Nom + ", Campagne : " + mParam.Campagne + " | Fournisseur : " + Fournisseur + " | Type De Livraison : " + Type + " | Du : " + StartDate + " Au : " + EndDate);


            #region Set Function's Access
            
            Fonction HasAccess = new Fonction();
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{F40FD8FA-A761-4752-8514-77B523811751}", UserName);
            

            //if (HasAccess.fnGetUserAccessStatus("{14720FC0-9A17-431D-A34A-02664A59FF7E}", UserName) == false)
            //    X.GetCmp<MenuItem>("mnuExportDeliveryNote").Disable();
            //else
            //    X.GetCmp<MenuItem>("mnuExportDeliveryNote").Enable();

            //if (HasAccess.fnGetUserAccessStatus("{1BDF90F3-91F7-47CB-8740-DA4E4147C6C7}", UserName) == false)
            //    X.GetCmp<MenuItem>("btnViewPendingDeliveries").Disable();
            //else
            //    X.GetCmp<MenuItem>("btnViewPendingDeliveries").Enable();           
            
            #endregion


            return View();
        }

        public ActionResult Index_Reclassification()
        {
            var mParam = (new Parametres()).fnSelect();

            Parametres mclass = new Parametres();
            mclass = mParam[0] as Parametres;

            Campagne mCampagne = new Campagne();

            X.GetCmp<ComboBox>("cmbCampagne").SetValue(mclass.Campagne);

            //string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string StartDate = DateTime.Now.ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("dtpStartDate").RawText = EndDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Fournisseur = "{Tous}";

            string Type = "{Tous}";

            X.GetCmp<FormPanel>("CriteriaPanelBL").SetTitle("Campagne : " + mclass.Campagne + " | Fournisseur : " + Fournisseur + " | Type De Livraison : " + Type + " | Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{F40FD8FA-A761-4752-8514-77B523811751}", UserName);

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{54B5D223-C84B-4D0A-A0BC-F046F1604664}"), UserName);

            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();            

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{297E9919-1B40-47CA-8AF4-38473AA19899}")))
                X.GetCmp<Hidden>("BlhiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("BlhiddenPermModifier").SetValue(false);

            #endregion

            return View();
        }


        public ActionResult onAdd()
        {
            //try
            //{               
            BonDeLivraisonViewModel mclass = new BonDeLivraisonViewModel();

            mclass._BonDeLivraison = new BonDeLivraison();
            mclass._BonDeLivraison.isViewPending = false;

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            
            //}
            //catch (Exception ex)
            //{
            //    X.MessageBox.Alert("Error", ex.Message).Show();
            //}

            return new Ext.Net.MVC.PartialViewResult { ViewName = "BonDeLivraison_Detail", Model = mclass, };
        }

        public ActionResult OnViewPendingDeliveries()
        {                       
            BonDeLivraisonViewModel mclass = new BonDeLivraisonViewModel();
            Parametres mParam = new Parametres(0);
            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();

            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            //bool result = mSiteParDefaut.fnGetDefaultSite();
            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;
            ViewData["Campagne"] = mParam.Campagne;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "BonDeLivraison_Available", Model = mclass, ViewData = ViewData};
        }

        public ActionResult onEdit(string ItemSelected)
        {
            //try
            //{
                BonDeLivraison mclass = JSON.Deserialize<BonDeLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                BonDeLivraisonViewModel viewModel = new BonDeLivraisonViewModel();

                viewModel._BonDeLivraison = mclass;
                            
                viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;


            //}
            //catch (Exception ex)
            //{
            //    X.MessageBox.Alert("Error", ex.Message).Show();
            //}
            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "BonDeLivraison_Detail", Model = viewModel };
            
        }

        public ActionResult onConsult(string ItemSelected)
        {            
            BonDeLivraison mclass = JSON.Deserialize<BonDeLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            BonDeLivraisonViewModel viewmodel = new BonDeLivraisonViewModel();
            viewmodel._BonDeLivraison = mclass;
            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "BonDeLivraison_Detail", Model = viewmodel, };
        }

        public ActionResult OnCheckViewPending(string ViewPending)
        {
            bool IsPending = false;
            bool isBool = bool.TryParse(ViewPending, out IsPending);
            //bool mView = bool.Parse(X.GetCmp<Hidden>("hiddenPending").Text);
            if (isBool && IsPending)
            {
                LivraisonViewModel mclass = new LivraisonViewModel();
                mclass._Livraison = new Livraison();
                var StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
                var EndDate = DateTime.Now.ToShortDateString();

                string UserName = (string)Session["userName"];
                Site mSiteParDefaut = new Site();
                
                bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

                if (result)
                {
                    mclass._Livraison.Site = new Site();
                    mclass._Livraison.Site.ID = mSiteParDefaut.ID;
                    mclass._Livraison.Site.Nom = mSiteParDefaut.Nom;
                }                

                return new Ext.Net.MVC.PartialViewResult { ViewName = "BonDeLivraison_Deliveries", Model = mclass };

            }

            else
                return null;

        }

        public ActionResult OnSelectDelivery()
        {
            LivraisonViewModel mclass = new LivraisonViewModel();
            mclass._Livraison = new Livraison();

            var StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            var EndDate = DateTime.Now.ToShortDateString();

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            if (result)
            {
                mclass._Livraison.Site = new Site();
                mclass._Livraison.Site.ID = mSiteParDefaut.ID;
                mclass._Livraison.Site.Nom = mSiteParDefaut.Nom;
            }

            //var Fournisseur = "{Tous}";

            //X.GetCmp<FormPanel>("CriteriaPanelFDel").SetTitle(" | Fournisseur : " + Fournisseur + " | Du : " + StartDate + " Au : " + EndDate);

            return new Ext.Net.MVC.PartialViewResult { ViewName = "BonDeLivraison_Deliveries", Model = mclass };

        }       

        public ActionResult OnRefreshForAvailableDeliveries(string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListDeliveries");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemCampagne"      ,ItemCampagne),
                                    new Ext.Net.Parameter("ItemType"          ,ItemType),
                                    new Ext.Net.Parameter("ItemSite", ItemSite),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus)
                                });

                string title = X.GetCmp<FormPanel>("CriteriaPanelFDel").Title;
                title += "Site = " + X.GetCmp<ComboBox>("cmbSite").SelectedItem.Text;

                title += ", Campagne : " + X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                title += ", Fournisseur : " + X.GetCmp<ComboBox>("cmbFournisseur").SelectedItem.Text;

                title += ", Type De Livraison = " + X.GetCmp<ComboBox>("cmbType").SelectedItem.Text;

                title += ", From " + X.GetCmp<DateField>("dtpStartDate").RawText.ToString() + "to " + X.GetCmp<DateField>("dtpEndDate").RawText.ToString();

                X.GetCmp<FormPanel>("CriteriaPanelFDel").Title = title;

                // collapse criterias areas
                X.GetCmp<FormPanel>("CriteriaPanelFDel").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bon De Livraison : OnRefresh",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult LoadListOfAvailableBL(string ItemFournisseur,string ItemCampagne, string ItemPeriodStart, string ItemPeriodEnd, string ItemSite, string ItemType)
        {
            int Fournisseur = ItemFournisseur == "" ? -1 : int.Parse(ItemFournisseur);
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
            int SiteID = ItemSite == "" ? -1 : int.Parse(ItemSite);
            int TypeID = ItemType == "" ? -1 : int.Parse(ItemType);
            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            var mListe = (new BonDeLivraison()).fnSelectValide(Campagne,Fournisseur, TypeID, StartDate, EndDate, "1", SiteID, 0);
            return this.Store(mListe);
        }

        public ActionResult LoadListOfAvailableDeliveryNotes(string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd)
        {
            int fournisseur = ItemFournisseur == "" ? -1 : int.Parse(ItemFournisseur);
            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            var mListe = (new BonDeLivraison()).fnSelectAvailableForInvoice(fournisseur, StartDate, EndDate);            
            return this.Store(mListe);
        }

        public ActionResult SubmitDelivery(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                BonDeLivraison mBon = new BonDeLivraison();
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);
                
                bool result = false;
                 
                _db = mBon.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                List<BonDeLivraison> Items = JSON.Deserialize<List<BonDeLivraison>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                if (Items.Count > 0)
                {

                    for (int i = 0; i < Items.Count(); i++)
                    {
                        mBon = new BonDeLivraison();                                            
                        mBon.ID = Items[i].ID;
                        mBon = Items[i];
                        mBon.SetDataSource(_db);
                        result = mBon.fnValidate(mtran,1);

                        if (!result)
                        {
                            break;
                        }
                    }
                }
                if (!result)
                {
                    _db.RollBackTransaction(mtran);
                }
                _db.CommitTransaction(mtran);


                if (result)
                {
                    foreach (var item in Items)
                    {
                        item.IsNew = true;
                        item.IsNewInList = true;
                        Store mstore = X.GetCmp<Store>("storeListe");
                        mstore.Insert(0, item);
                    }                    

                    X.GetCmp<Window>("BonDeLivraison_Available").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "BL : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult SubmitDeliveryByNumber(string ItemDeliveryNumber)
        {
            try
            {
                Livraison mclass = new Livraison();
                if (string.IsNullOrEmpty(ItemDeliveryNumber))
                {
                    return this.Direct();
                }
                if(mclass.fnGetAvalaibleForFinalizing(ItemDeliveryNumber))
                {
                    MapDeliveryToForm(mclass);

                    MapAnalysisToForm(mclass);

                    MapRetentionToForm(mclass);
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bon De Livraison : Submit Delivery",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();

        }

        private void MapDeliveryToForm(Livraison mClass)
        {
            try
            {
                //X.GetCmp<TextField>("txtDeliveryID").Text = string.Empty;
                X.GetCmp<TextField>("txtSupplier").Text = string.Empty;
                //X.GetCmp<TextField>("txtCertification").Text = string.Empty;
                X.GetCmp<TextField>("txtTruckID").Text = string.Empty;
                X.GetCmp<TextField>("txtDeliveryType").Text = string.Empty;
                X.GetCmp<TextField>("txtAcceptedBags").Text = string.Empty;
                X.GetCmp<TextField>("txtGrossWeight").Text = string.Empty;
                X.GetCmp<TextField>("txtSacType").Text = string.Empty;
                X.GetCmp<TextField>("txtTareSacs").Text = string.Empty;
                X.GetCmp<TextField>("txtTarePalettes").Text = string.Empty;
                X.GetCmp<TextField>("txtDelivered").Text = string.Empty;

                if(mClass.Numero != null)
                {
                    X.GetCmp<Hidden>("hiddenLivraisonID").Text = mClass.ID.ToString();
                    if (mClass.Numero != null) X.GetCmp<TextField>("txtDeliveryID").Text = mClass.Numero;
                    X.GetCmp<DateField>("dtfDeliveryDate").SelectedDate = mClass.DateLivraison;
                    X.GetCmp<TimeField>("tmfDelivery").SelectedTime = TimeSpan.FromTicks(mClass.DateLivraison.Ticks);
                    X.GetCmp<TextField>("txtSupplier").Text = mClass.FournisseurNameAsString;
                    //X.GetCmp<TextField>("txtCertification").Text = mClass.Certification.Designation;
                    X.GetCmp<TextField>("txtTruckID").Text = mClass.Immatriculation;
                    X.GetCmp<TextField>("txtDeliveryType").Text = mClass.LivraisonType.Designation;
                    X.GetCmp<TextField>("txtAcceptedBags").Text = mClass.SacsAcceptes.ToString();
                    X.GetCmp<TextField>("txtGrossWeight").Text = mClass.PoidsBrutAsString.ToString();
                    X.GetCmp<TextField>("txtSacType").Text = mClass.SacType.Designation;
                    X.GetCmp<TextField>("txtTareSacs").Text = mClass.TareSacs.ToString();
                    X.GetCmp<TextField>("txtTarePalettes").Text = mClass.TarePalettes.ToString();
                    X.GetCmp<TextField>("txtDelivered").Text = mClass.PoidsLivreAsString.ToString();

                    //X.GetCmp<TextField>("txtDelivered").Text = mClass.Site.ID.ToString();
                    X.GetCmp<TextField>("txtNomSite").Text = mClass.Site.Nom;

                    if (mClass.LivraisonType != null && mClass.LivraisonType.EstAchat == false)
                    {
                        X.GetCmp<ComboBox>("CertificationID").ReadOnly = true;
                        //X.GetCmp<GridPanel>("grpDetailAnalysis").Disable();
                        X.GetCmp<TextField>("txtTotalRetention").RawText = "0";
                        X.GetCmp<TextField>("txtRetBrisure").AllowBlank = true;
                        X.GetCmp<TextField>("txtRetME").AllowBlank = true;
                        X.GetCmp<TextField>("txtRetHumidite").AllowBlank = true;

                        X.GetCmp<TextField>("txtRetBrisure").IndicatorText = "";
                        X.GetCmp<TextField>("txtRetME").IndicatorText = "";
                        X.GetCmp<TextField>("txtRetHumidite").IndicatorText = "";

                        X.GetCmp<TextField>("txtRetBrisure").IndicatorCls = "";
                        X.GetCmp<TextField>("txtRetME").IndicatorCls = "";
                        X.GetCmp<TextField>("txtRetHumidite").IndicatorCls = "";

                        X.GetCmp<TextField>("txtRetBrisure").ShowIndicator();
                        X.GetCmp<TextField>("txtRetME").ShowIndicator();
                        X.GetCmp<TextField>("txtRetHumidite").ShowIndicator();
                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bon De Livraison : MapDeliveryToForm",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
        }

        private void MapAnalysisToForm(Livraison mClass)
        {
            try
            {
                var mListe = (new AnalysePhysique()).fnSelectByDelivery(mClass.ID);

                Store store = X.GetCmp<Store>("storeListAnalysis");
                store.RemoveAll();

                foreach (var item in mListe)
                {
                    item.IsNew = true;
                    store.Insert(0, item);
                }

                X.GetCmp<Button>("BtnOkDetail").Disable();

                if (mListe.Count == 1)
                {
                    AnalysePhysique mAnalyse = mListe[0] as AnalysePhysique;
                    X.GetCmp<Hidden>("hiddenCodingID").Text = mAnalyse.AnalyseCode.ID.ToString();
                    X.GetCmp<Hidden>("hiddenHumidite").Text = mAnalyse.Humidite.ToString();
                    X.GetCmp<Hidden>("hiddenME").Text = mAnalyse.MatiereEtrangere.ToString();
                    X.GetCmp<Hidden>("hiddenBrisure").Text = mAnalyse.Brisure.ToString();
                    X.GetCmp<Hidden>("hiddenSieving").Text = mAnalyse.Tamis.ToString();
                    X.GetCmp<Button>("BtnOkDetail").Enable();
                    X.GetCmp<RowSelectionModel>("rowSelectionListeAnalyse").Select(0);
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bon De Livraison : MapAnalysisToForm",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

        }

        private void MapRetentionToForm(Livraison mClass)
        {
            try
            {
                X.GetCmp<TextField>("txtStdHumidite").Text = string.Empty;
                X.GetCmp<TextField>("txtStdME").Text = string.Empty;
                X.GetCmp<TextField>("txtStdBrisure").Text = string.Empty;
                X.GetCmp<TextField>("txtRetHumidite").Text = string.Empty;
                X.GetCmp<TextField>("txtRetME").Text = string.Empty;
                X.GetCmp<TextField>("txtRetBrisure").Text = string.Empty;
                X.GetCmp<TextField>("txtTotalRetention").Text = string.Empty;
                X.GetCmp<TextField>("txtPoidsNet").Text = string.Empty;
                //X.GetCmp<TextField>("txtTareSacsAdjust").Text = string.Empty;
                //X.GetCmp<TextField>("txtTarePalettesAdjust").Text = string.Empty;
                X.GetCmp<ComboBox>("CertificationID").SetValue(string.Empty);
                
                double mStdHumidite = new AnalysePhysiqueNorme().StandardHumidite;
                double mStdME = new AnalysePhysiqueNorme().StandardMatiereEtrangere;
                double mStdBrisure = new AnalysePhysiqueNorme().StandardBrisure;

                if(mClass.Numero != null)
                {
                    X.GetCmp<TextField>("txtStdHumidite").Text = Math.Round(mStdHumidite, 2).ToString();
                    X.GetCmp<TextField>("txtStdME").Text = Math.Round(mStdME, 2).ToString();
                    X.GetCmp<TextField>("txtStdBrisure").Text = Math.Round(mStdBrisure, 2).ToString();
                }              

                decimal mPoidsLivre = mClass.PoidsLivre;
                var mListe = (new AnalysePhysique()).fnSelectByDelivery(mClass.ID);
                if (mListe.Count == 1)
                {
                    AnalysePhysique mAnalyse = mListe[0] as AnalysePhysique;

                    decimal mRetHumidite = Math.Round((decimal)mAnalyse.Humidite - (decimal)mStdHumidite > 0 ? ((decimal)mAnalyse.Humidite - (decimal)mStdHumidite) * mPoidsLivre / (100 - (decimal)mStdHumidite) : 0, 0);
                    decimal mRetME = Math.Round((decimal)mAnalyse.MatiereEtrangere - (decimal)mStdME > 0 ? ((decimal)mAnalyse.MatiereEtrangere - (decimal)mStdME) * mPoidsLivre / 100 : 0, 0);
                    //decimal mRetBrisure = Math.Round((decimal)mAnalyse.Brisure - (decimal)mStdBrisure > 0 ? ((decimal)mAnalyse.Brisure - (decimal)mStdBrisure) * mPoidsLivre / 100 : 0, 0);
                    decimal mRetBrisure = Math.Round((decimal)mAnalyse.Tamis - (decimal)mStdBrisure > 0 ? ((decimal)mAnalyse.Tamis - (decimal)mStdBrisure) * mPoidsLivre / 100 : 0, 0);

                    X.GetCmp<TextField>("txtRetHumidite").Text = mRetHumidite.ToString();
                    X.GetCmp<TextField>("txtRetME").Text = mRetME.ToString();
                    X.GetCmp<TextField>("txtRetBrisure").Text = mRetBrisure.ToString();

                    decimal mTotalRetention = mRetBrisure + mRetHumidite + mRetME;
                    decimal mPoidsNet = mPoidsLivre - mTotalRetention;

                    X.GetCmp<TextField>("txtTotalRetention").Text = String.Format("{0:#,#}", mTotalRetention).TrimStart();
                    X.GetCmp<TextField>("txtPoidsNet").Text = String.Format("{0:#,#}", mPoidsNet).TrimStart();

                }

                if(mClass.Numero != null)
                {
                    //X.GetCmp<TextField>("txtTareSacsAdjust").Text = mClass.TareSacs.ToString();
                    //X.GetCmp<TextField>("txtTarePalettesAdjust").Text = mClass.TarePalettes.ToString();
                    X.GetCmp<ComboBox>("CertificationID").SetValue(mClass.Certification.ID.ToString());
                }
                
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bon De Livraison : MapRetentionToForm",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
        }

        private BonDeLivraison MapFormToObject(BonDeLivraison mClass)
        {
            try
            {
                Parametres mParam = new Parametres(0);
                Site mSiteParDefaut = new Site();
                

                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];

                bool result = mSiteParDefaut.fnGetBySiteByUserName((string)Session["userName"]);
                ViewBag.SiteParDefaut = mSiteParDefaut.ID;

                Livraison mLivraison = new Livraison();
                mLivraison.fnGetForFinalizing(Guid.Parse(X.GetCmp<Hidden>("hiddenLivraisonID").Text));
                mClass.Livraison = mLivraison;

                AnalyseCode mAnalyseCode = null;
                Guid codeAnalyseID;
                bool resultParse = Guid.TryParse(X.GetCmp<Hidden>("hiddenCodingID").Text, out codeAnalyseID);

                if (resultParse && codeAnalyseID != Guid.Empty)
                {
                    mAnalyseCode = new AnalyseCode();
                    mAnalyseCode.ID = (Guid.Parse(X.GetCmp<Hidden>("hiddenCodingID").Text));
                    mAnalyseCode.DateCode = DateTime.Parse(GetFormValue("dtfFinalizingDate") + " " + GetFormValue("tmfFinalizing"));
                    mClass.AnalyseCode = mAnalyseCode;
                }

                if (resultParse && codeAnalyseID == Guid.Empty && mSiteParDefaut.ID == mParam.Site)
                {
                    throw new Exception("Analysis : Select one Analysis");
                }

                mClass.DateBonDeLivraison = DateTime.Parse(GetFormValue("dtfFinalizingDate") + " " + GetFormValue("tmfFinalizing"));
                mClass.PoidsBrut = Convert.ToDecimal(GetFormValue("txtGrossWeight"));
                mClass.TareSacsAjustee = Convert.ToDecimal(GetFormValue("txtTareSacs"));
                mClass.TarePalettesAjustee = Convert.ToDecimal(GetFormValue("txtTarePalettes"));

                //mClass.Humidite = (double.Parse(X.GetCmp<Hidden>("hiddenHumidite").Text.Replace('.', ',')));
                //mClass.MatieresEtrangeres = (double.Parse(X.GetCmp<Hidden>("hiddenME").Text.Replace('.', ',')));
                //mClass.Brisures = (double.Parse(X.GetCmp<Hidden>("hiddenBrisure").Text.Replace('.', ',')));

                mClass.Humidite = Convert.ToDouble(X.GetCmp<Hidden>("hiddenHumidite").Text);
                mClass.MatieresEtrangeres = Convert.ToDouble(X.GetCmp<Hidden>("hiddenME").Text);
                mClass.Brisures = Convert.ToDouble(X.GetCmp<Hidden>("hiddenBrisure").Text);

                mClass.StdHumidite = Convert.ToDouble(GetFormValue("txtStdHumidite"));
                mClass.StdMatieresEtrangeres = Convert.ToDouble(GetFormValue("txtStdME"));
                mClass.StdBrisures = Convert.ToDouble(GetFormValue("txtStdBrisure"));

                mClass.NbreSacs = int.Parse(GetFormValue("txtAcceptedBags"));
                mClass.Tare = decimal.Parse(GetFormValue("txtTareSacs")) + decimal.Parse(GetFormValue("txtTarePalettes"));

                if (string.IsNullOrEmpty(GetFormValue("txtRetHumidite")))
                    mClass.RefactionHumidite = 0;
                else
                    mClass.RefactionHumidite = Convert.ToDecimal(GetFormValue("txtRetHumidite"));

                if (string.IsNullOrEmpty(GetFormValue("txtRetME")))
                    mClass.RefactionMatieresEtg = 0;
                else
                    mClass.RefactionMatieresEtg = Convert.ToDecimal(GetFormValue("txtRetME"));

                if (string.IsNullOrEmpty(GetFormValue("txtRetBrisure")))
                    mClass.RefactionBrisures = 0;
                else
                    mClass.RefactionBrisures = Convert.ToDecimal(GetFormValue("txtRetBrisure"));
                
                mClass.PoidsNetAccepte = Convert.ToDecimal(GetFormValue("txtPoidsNet"));
                mClass.Commentaire = X.GetCmp<TextField>("txtCommentaire").Text;

                //mClass.CertificationID = int.Parse(GetFormValue("CertificationID"));

                mClass.CertificationID = null;

                int iConverted;
                //bool result;
                result = int.TryParse(GetFormValue("CertificationID"), out iConverted);
                if (result)
                {
                    mClass.CertificationID = int.Parse(GetFormValue("CertificationID"));
                }

                mClass.Livraison.Site = new Site();
                mClass.Livraison.Site.Nom = X.GetCmp<TextField>("txtNomSite").Text;
                mClass.Sites = new Site();
                mClass.Sites.Nom = X.GetCmp<TextField>("txtNomSite").Text;
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

        private Livraison MapFormToObject(Livraison mClass)
        {
            try
            {                
                mClass.UtilisateurModification = (string)Session["userName"];

                Certification mOld = new Certification();
                mOld.ID = int.Parse(X.GetCmp<ComboBox>("OldCertificationID").Text);
                mOld.Designation = X.GetCmp<ComboBox>("OldCertificationID").SelectedItem.Text.ToString();
                mClass.AncienneCertification = mOld;

                Certification mNew = new Certification();
                mNew.ID = int.Parse(X.GetCmp<ComboBox>("NewCertificationID").Text);
                mNew.Designation = X.GetCmp<ComboBox>("NewCertificationID").SelectedItem.Text.ToString();
                mClass.NouvelleCertification = mNew;                              
                
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


        public ActionResult OnCancel(string ItemSelected)
        {

            try
            {
                BonDeLivraison mClass = JSON.Deserialize<BonDeLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);

                if (!result)
                    throw new Exception("OnCancel : Bon De Livraison loading failed.");

                mClass.UtilisateurModification = (string)Session["userName"];

                if (mClass.fnValidate(0))
                {
                    Store mstore = X.GetCmp<Store>("storeListe");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);
                    mProxy.Drop();

                    //DeselectGridRows();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bon De Livraison : Cancel",
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
                BonDeLivraison mClass = new BonDeLivraison();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("hiddenID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("UpdateFormMethod : Financing load failed.");
                }

                mClass = MapFormToObject(mClass);
                
                bool result = mClass.fnUpdate();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListe");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                        //X.GetCmp<RowSelectionModel>("rowSelectionListeBL").Select(0);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("BonDeLivraison_Detail").Close();                                      
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bon De Livraison : UpdateFormMethod",
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
            int siteID = GetCriteriaValue(ItemSite);
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;

            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "{Tous}";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string Status = ItemStatus;
            //string Status = ItemStatus == "null" ? "%%" : ItemStatus;

            var mListe = (new BonDeLivraison()).fnSelectValide(Campagne, FournisseurID, TypeID, StartDate, EndDate, Status, siteID,1);
            
            return this.Store(mListe);
        }        

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelBL");
            mform.ToggleCollapse();
            //mform.Collapsed = false;
            return this.Direct();
        }       

        public ActionResult OnRefresh(string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListe");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemCampagne"      ,ItemCampagne),
                                    new Ext.Net.Parameter("ItemType"          ,ItemType),
                                    new Ext.Net.Parameter("ItemSite", ItemSite),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus)
                                });

                string title = X.GetCmp<FormPanel>("CriteriaPanelBL").Title;
                title += "Site = " + X.GetCmp<ComboBox>("cmbSite").SelectedItem.Text;

                title += ", Campagne : " + X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                title += ", Fournisseur : " + X.GetCmp<ComboBox>("cmbFournisseur").SelectedItem.Text;

                title += ", Type De Livraison = " + X.GetCmp<ComboBox>("cmbType").SelectedItem.Text;

                title += ", From " + X.GetCmp<DateField>("dtpStartDate").RawText.ToString() + "to " + X.GetCmp<DateField>("dtpEndDate").RawText.ToString();

                X.GetCmp<FormPanel>("CriteriaPanelBL").Title = title;

                // collapse criterias areas
                X.GetCmp<FormPanel>("CriteriaPanelBL").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bon De Livraison : OnRefresh",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnRefreshForClassification(string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListe");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemCampagne"      ,ItemCampagne),
                                    new Ext.Net.Parameter("ItemType"          ,ItemType),
                                    new Ext.Net.Parameter("ItemSite", ItemSite),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus)
                                });

                string title = X.GetCmp<FormPanel>("CriteriaPanelFDel").Title;
                title += "Site = " + X.GetCmp<ComboBox>("cmbSiteFD").SelectedItem.Text;

                title += ", Campagne : " + X.GetCmp<ComboBox>("cmbCampagneFD").SelectedItem.Text;

                title += ", Fournisseur : " + X.GetCmp<ComboBox>("cmbFournisseurFD").SelectedItem.Text;

                title += ", Type De Livraison = " + X.GetCmp<ComboBox>("cmbTypeFD").SelectedItem.Text;

                title += ", From " + X.GetCmp<DateField>("dtpStartDateFD").RawText.ToString() + "to " + X.GetCmp<DateField>("dtpEndDateFD").RawText.ToString();

                X.GetCmp<FormPanel>("CriteriaPanelFDel").Title = title;

                // collapse criterias areas
                X.GetCmp<FormPanel>("CriteriaPanelFDel").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bon De Livraison : OnRefresh",
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
            X.GetCmp<RowSelectionModel>("rowSelectionListe").DeselectAll();
        }

        public ActionResult OnPrintDeliveryNote(string IdBon, string IsCopy, string AfficheResultatAnalyse = "0")
        {
            bool ReportIscopy = false;
            bool isBool = false;
            bool afficheAnalyse = false;
            if (string.IsNullOrEmpty(IsCopy))
                ReportIscopy = true;

            isBool = bool.TryParse(AfficheResultatAnalyse, out afficheAnalyse);

            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));            

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'DeliveryNote{0}', '{1}/BonDeLivraison/ViewReport?id={0}&IsCopy={2}&AfficheResultatAnalyse={3}', this, 'Bon De Livraison Report','')", IdBon, BaseUrl, ReportIscopy, afficheAnalyse));
        }

        public ActionResult ViewReport(string id, bool IsCopy, bool AfficheResultatAnalyse)
        {
            XtraReport report = new XtraReport(); ;

            if (AfficheResultatAnalyse)
             report = new rptBonDeLivraison() as XtraReport;  
            else
                report = new rptBonDeLivraisonDivers() as XtraReport;

            report.DataSource = DevExpressReportDs.SetDataSource(report);
            report.Parameters["ID"].Value = id;
            report.Parameters["IsCopy"].Value = IsCopy;

            ViewData["Report"] = report;

            return View();
        }

        public ActionResult OnDisplayDeliveryNoteList(string ItemPeriodStart = "", string ItemPeriodEnd = "")
        {
            try
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

                ViewData["Titre"] = "Print Liste des bons de livraisons";
                ViewData["actionToDo"] = "OnPrintDeliveryNoteList";
                ViewData["ControllerName"] = "BonDeLivraison";
            }
            catch (Exception)
            {

                throw;
            }            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmCriteriaForBonDeLivraison", ViewData = ViewData };
        }

        public ActionResult OnPrintDeliveryNoteList()
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

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/BonDeLivraison/ViewReportListResult', this, 'Liste des bons de livraisons',''),App.frmCriteriaForBonDeLivraison.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bon De Livraison : Data Validation",
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

            rptBonDeLivraisonList report = new rptBonDeLivraisonList();
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

            report.Parameters["paramTypeOfDelivery"].Value = Session["paramTypeOfDelivery"];
            report.Parameters["paramTypeOfDeliveryText"].Value = Session["paramTypeOfDeliveryText"];

            report.Parameters["paramStatut"].Value = Session["paramStatut"];
            report.Parameters["paramStatutText"].Value = Session["paramStatutText"];

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];
            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

        public ActionResult onClassify(string ItemSelected)
        {            
            BonDeLivraison mclass = JSON.Deserialize<BonDeLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ViewData["BonDeLivraisonID"] = mclass.ID;
            LivraisonViewModel mModel = new LivraisonViewModel();
            Livraison mlivraison = new Livraison();

            bool result = mlivraison.fnGet(mclass.Livraison.ID);
            mModel._Livraison = new Livraison();
            mModel._Livraison = mlivraison;
            mModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "BonDeLivraison_Classify", Model = mModel };
        }

        public ActionResult onConsultClassification(string ItemSelected)
        {
            BonDeLivraison mclass = JSON.Deserialize<BonDeLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ViewData["BonDeLivraisonID"] = mclass.ID;
            LivraisonViewModel mModel = new LivraisonViewModel();
            Livraison mlivraison = new Livraison();

            bool result = mlivraison.fnGetForClassification(mclass.Livraison.ID);
            mModel._Livraison = new Livraison();
            mModel._Livraison.AncienneCertification = new Certification();
            mModel._Livraison.NouvelleCertification = new Certification();
            mModel._Livraison.AncienneCertification.ID = mlivraison.AncienneCertification.ID;
            mModel._Livraison.NouvelleCertification.ID = mlivraison.NouvelleCertification.ID;
                        
            mModel._Livraison = mlivraison;
           
            mModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "BonDeLivraison_Classify", Model = mModel };
        }

        [HttpPost]
        public ActionResult UpdateCertification()
        {
            try
            {
                Livraison mClass = new Livraison();

                mClass.IsNew = false;

                mClass.fnGet(Guid.Parse(GetFormValue("hiddenLivraisonID")));

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("UpdateCertification : Financing load failed.");

                mClass = MapFormToObject(mClass);

                bool result = mClass.fnReclassify();

                if (result)
                {
                    //Store mStore = X.GetCmp<Store>("storeListe");

                    //ModelProxy mProxy;

                    //mProxy = mStore.GetById(mClass.ID);

                    //mProxy.BeginEdit();

                    //mProxy.Set(mClass);

                    //mProxy.Commit();

                    //mProxy.EndEdit();

                    X.GetCmp<Window>("BonDeLivraison_Classify").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bon De Livraison : UpdateCertification",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

    }
}
