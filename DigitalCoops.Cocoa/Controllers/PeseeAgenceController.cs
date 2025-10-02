using DevExpress.XtraReports.UI;
using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes;
using Tms.Classes.Business;
using Tms.Classes.Business.Sites;
using Tms.Classes.Business.stock;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class PeseeAgenceController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";


        static int _CapturedTare;
        static int _CapturedWeight;
        //static bool _WeighingIsCreated;
        //static Guid _WeighingID;

        //public static Guid WeighingID
        //{
        //    get
        //    {
        //        return _WeighingID;
        //    }
        //    set
        //    {
        //        _WeighingID = value;
        //    }
        //}

        //public static bool WeighingIsCreated
        //{
        //    get
        //    {
        //        return _WeighingIsCreated;
        //    }
        //    set
        //    {
        //        _WeighingIsCreated = value;
        //    }
        //}

        public static int CapturedTare
        {
            get
            {
                return _CapturedTare;
            }
            set
            {
                _CapturedTare = value;
            }
        }

        public static int CapturedWeight
        {
            get
            {
                return _CapturedWeight;
            }
            set
            {
                _CapturedWeight = value;
            }
        }

        bool BtnReadClicked = false;
        // GET: PeseeAvantUsinage
        public ActionResult Index()
        {
            //string campagne = new Parametres(0).Campagne;
            Parametres mParam = new Parametres(0);
            X.GetCmp<ComboBox>("cmbFiltreCampagne").SetValue(mParam.Campagne);

            //string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string StartDate = DateTime.Now.ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("txtFiltreDateDebut").RawText = StartDate;
            X.GetCmp<DateField>("txtFiltreDateFin").RawText = EndDate;

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;
            ViewBag.LivraisonAchat = mParam.LivraisonTypeAchat.ID;
            Fonction HasAccessFunction = new Fonction();
            bool HasAccessAllSite = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            ViewBag.HasAccessAllSite = HasAccessAllSite;

            X.GetCmp<FormPanel>("PeseeAgenceCP").SetTitle("Site : "+ mSiteParDefaut.Nom + ", Campagne : " + mParam.Campagne + ", Today : " + StartDate +", Type De Livraison : " + mParam.LivraisonTypeAchat.Designation);

            #region Set Function's Access            

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{465d5d7a-4d8f-4c29-bcf3-c4eb57a6628e}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{6b2099c5-b750-4e88-969d-24bbdc5ea4a7}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{20942a1a-a5a0-4983-8f90-075cc2059f04}")))
                X.GetCmp<MenuItem>("mnuPrintWeihingList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintWeihingList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{8508143b-6d83-4887-ace0-62914f68895c}")))
                X.GetCmp<MenuItem>("mnuExportPAU").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportPAU").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{f155fbbf-7dda-43cb-9c21-6cfa057f70ea}")))
                X.GetCmp<MenuItem>("bntAddManual").Enable();
            else
                X.GetCmp<MenuItem>("bntAddManual").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{4a9d8938-d2e8-4414-aa68-61daf7c8c92e}")))
                X.GetCmp<MenuItem>("btnPendingDeliveries").Enable();
            else
                X.GetCmp<MenuItem>("btnPendingDeliveries").Disable();


            X.GetCmp<Hidden>("phiddenPermModifier").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{7ad270a7-9e0d-40ca-af17-cbef3deba3aa}")));
            X.GetCmp<Hidden>("phiddenPermDesactiver").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{4a828598-bb6e-44af-958c-38eee4a09617}")));
            X.GetCmp<Hidden>("phiddenPermPrintSheet").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{adfffac6-a6bf-400e-b36e-defa383e5caf}")));
            X.GetCmp<Hidden>("phiddenPermFinalize").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{d42483b7-7b0d-4abb-aec6-95687b281958}")));            

            //X.GetCmp<Hidden>("mvhiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{94E1648B-BB69-4470-8B5D-FA8D19598161}", UserName));
            //X.GetCmp<Hidden>("mvhiddenPermGenerate").SetValue(HasAccess.fnGetUserAccessStatus("{C1785226-126D-4648-95C6-92894707BC1E}", UserName));
            //X.GetCmp<Hidden>("mvhiddenPermPrint").SetValue(HasAccess.fnGetUserAccessStatus("{DF2C998A-9232-48ED-B403-6DB46289BE0A}", UserName));
            //X.GetCmp<Hidden>("mvhiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{3CA6BD01-DFC1-4B9F-9E0E-134391DF8B4F}", UserName));
            #endregion

            return View();
        }

        public ActionResult OnCaptureTare(string ItemSelected)
        {
            try
            {
                List<PeseeProductionPalette> item = JSON.Deserialize<List<PeseeProductionPalette>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                if (item.Count > 0)
                {

                    BtnReadClicked = true;
                    CapturedTare = 0;
                    string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                    //string ip = "198.168.8.101";
                    // initialize services                      
                    //ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);

                    //int sWeight = proxy.GetWeight();
                    int sWeight = 20;
                    X.GetCmp<TextField>("txtTarePalettes").Text = sWeight.ToString("n0");
                    CapturedTare = sWeight;
                }
                else
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Weighing : Capture Weight",
                        Message = "Full Pallets Weight Is Empty",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            return this.Direct();
        }

        public ActionResult OnCaptureWeight(string sacs = "")
        {
            try
            {
                BtnReadClicked = true;
                CapturedWeight = 0;
                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                //string ip = "198.168.8.101";
                // initialize services                      
                //ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);

                //int sWeight = proxy.GetWeight();
                int nbrSac = !string.IsNullOrEmpty(sacs) ? int.Parse(sacs) : 0;
                int sWeight = nbrSac * 67;
                X.GetCmp<NumberField>("txtPoidsBrutPAU").Text = sWeight.ToString();
                CapturedWeight = sWeight;

            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            return this.Direct();
        }

        public ActionResult onAdd()
        {
            //WeighingIsCreated = false;
            PeseeSiteViewModel mclass = new PeseeSiteViewModel();
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{d42483b7-7b0d-4abb-aec6-95687b281958}"), UserName);

            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            mclass._PeseeSite = new PeseeSite();
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._PeseeSite.IsManual = false;

            if (result)
            {
                mclass._PeseeSite.Sites = new Site();
                mclass._PeseeSite.Sites.ID = mSiteParDefaut.ID;
                mclass._PeseeSite.Sites.Nom = mSiteParDefaut.Nom;
            }
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeAgence", Model = mclass, ViewData = ViewData };

        }

        public ActionResult OnViewPendingDeliveries()
        {
            //WeighingIsCreated = false;
            PeseeSiteViewModel mclass = new PeseeSiteViewModel();
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{d42483b7-7b0d-4abb-aec6-95687b281958}"), UserName);

            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            mclass._PeseeSite = new PeseeSite();
            mclass._PeseeSite.IsPending = true;
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._PeseeSite.IsManual = false;

            if (result)
            {
                mclass._PeseeSite.Sites = new Site();
                mclass._PeseeSite.Sites.ID = mSiteParDefaut.ID;
                mclass._PeseeSite.Sites.Nom = mSiteParDefaut.Nom;
            }
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeAgence", Model = mclass, ViewData = ViewData };

        }


        public ActionResult onEdit(string ItemSelected)
        {
            //WeighingIsCreated = true;
            PeseeSiteViewModel mclass = new PeseeSiteViewModel();
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{d42483b7-7b0d-4abb-aec6-95687b281958}"), UserName);

            mclass._PeseeSite = new PeseeSite();
            mclass._PeseeSite = JSON.Deserialize<PeseeSite>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._PeseeSite.fnGet(mclass._PeseeSite.ID);
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            CapturedTare = mclass._PeseeSite.TarePalette > 0 ? int.Parse(mclass._PeseeSite.TarePalette.ToString("#")) : 0;
            //mclass._PeseeSite.IsManual = false;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeAgence", Model = mclass, ViewData = ViewData};
        }

        public ActionResult onConsult(string ItemSelected)
        {
            //WeighingIsCreated = true;
            PeseeSiteViewModel mclass = new PeseeSiteViewModel();            
            ViewData["CanFinalize"] = false;

            mclass._PeseeSite = new PeseeSite();
            mclass._PeseeSite = JSON.Deserialize<PeseeSite>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._PeseeSite.fnGet(mclass._PeseeSite.ID);
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
            //mclass._PeseeSite.IsManual = false;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeAgence", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onManualWeight(string ItemSelected = "")
        {
            PeseeSiteViewModel mclass = new PeseeSiteViewModel();
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{d42483b7-7b0d-4abb-aec6-95687b281958}"), UserName);

            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            mclass._PeseeSite = new PeseeSite();
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._PeseeSite.IsManual = true;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            if (result)
            {
                mclass._PeseeSite.Sites = new Site();
                mclass._PeseeSite.Sites.ID = mSiteParDefaut.ID;
                mclass._PeseeSite.Sites.Nom = mSiteParDefaut.Nom;
            }

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeAgence", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onAddWeight(string IsManual = "")
        {           
            if (X.GetCmp<TextField>("txtDeliveryNumero").Text != string.Empty)
                ViewData["NumeroLivraison"] = X.GetCmp<TextField>("txtDeliveryNumero").Text;
            if (X.GetCmp<Hidden>("txtImmatriculation").Text != string.Empty)
                ViewData["LivraisonImmatriculation"] = X.GetCmp<Hidden>("txtImmatriculation").Text;
            if (X.GetCmp<Hidden>("txtLivraisonID").Text != string.Empty)
                ViewData["LivraisonID"] = Guid.Parse(X.GetCmp<Hidden>("txtLivraisonID").Text);
            if (X.GetCmp<Hidden>("hidNombreSacsLivraison").Text != string.Empty)
                ViewData["NbrSacLivraisons"] = int.Parse(X.GetCmp<Hidden>("hidNombreSacsLivraison").Text);
            if (X.GetCmp<Hidden>("txtTareUnitaire").Text != string.Empty)
                ViewData["tareSacs"] = decimal.Parse(X.GetCmp<Hidden>("txtTareUnitaire").Text);

            PeseeSite pesee = new PeseeSite();
            
            if (!string.IsNullOrEmpty(IsManual))            
                pesee.IsManual = bool.Parse(IsManual);
            else
                pesee.IsManual = false;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAddWeight", Model = pesee, ViewData = ViewData };
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("PeseeAgenceCP");
            mform.ToggleCollapse();            
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite, string ItemFournisseur, string ItemLivraisonType)
        {
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "-1" : ItemCampagne;
            int siteID = GetCritriaValue(ItemSite);
            int fournisseurID = GetCritriaValue(ItemFournisseur);
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "";
            }
            int LivraisonTypeID = GetCritriaValue(ItemLivraisonType);

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string statut = string.IsNullOrEmpty(ItemStatus) ? "NA" : ItemStatus;

            var mListe = (new PeseeSite()).fnSelect(ItemCampagne, StartDate, EndDate, statut, siteID, fournisseurID,LivraisonTypeID);

            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite, string ItemFournisseur, string ItemLivraisonType)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListePeseeAgence");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCampagne", ItemCampagne),
                                    new Ext.Net.Parameter("ItemSite", ItemSite),
                                    new Ext.Net.Parameter("ItemLivraisonType"     ,ItemLivraisonType),
                                    new Ext.Net.Parameter("ItemFournisseur", ItemFournisseur),
                                    new Ext.Net.Parameter("ItemPeriodStart", ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd", ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus", ItemStatus)
                                });

                X.GetCmp<FormPanel>("PeseeAgenceCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnSelectDelivery(int? siteID, string ViewPending = "")
        {
            try
            {
                if ((!string.IsNullOrEmpty(ViewPending) && ViewPending == "true") || string.IsNullOrEmpty(ViewPending))
                {

                    if (!siteID.HasValue)
                        throw new Exception("");

                    PeseeSite mModel = new PeseeSite();
                    mModel.Sites = new Site();
                    mModel.Sites.ID = (int)siteID;
                    return new Ext.Net.MVC.PartialViewResult { ViewName = "PeseeAgence_Deliveries", Model = mModel, ViewData = ViewData };
                }
                else
                    return this.Direct();

            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing : Delivery",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();                
            }

        }

        public ActionResult onCancel(string ItemSelected)
        {
            try
            {
                PeseeSite pesee = JSON.Deserialize<PeseeSite>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = pesee.fnGet(pesee.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Weighing, loading failed.");

                pesee.UtilisateurModification = (string)Session["userName"];

                
                result = pesee.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Weighing, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePeseeAgence");

                    ModelProxy mProxy = mstore.GetById(pesee.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(pesee);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitDelivery(string ItemSelected, string FromPending = "")
        {            
            Livraison mLivraison = new Livraison();
            mLivraison = JSON.Deserialize<Livraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            X.GetCmp<TextField>("txtDeliveryNumero").SetValue(mLivraison.Numero);
            X.GetCmp<TextField>("txtNombreSacsDelivery").SetValue(mLivraison.SacsDeclares);            
            //X.GetCmp<TextField>("txtPoidsBrutComposition").SetValue(mLivraison.PoidsBrut);
            X.GetCmp<Hidden>("txtImmatriculation").SetValue(mLivraison.Immatriculation);
            X.GetCmp<Hidden>("txtTareUnitaire").SetValue(mLivraison.TareSacs);
            X.GetCmp<Hidden>("txtLivraisonID").SetValue(mLivraison.ID);
            X.GetCmp<Window>("PeseeAgence_Deliveries").Close();
            
            return this.Direct();
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
                        X.GetCmp<Hidden>("txtLivraisonID").SetValue(mclass.Livraison.ID);
                        X.GetCmp<TextField>("txtDeliveryNumero").SetValue(mclass.Livraison.Numero);
                    }
                    else
                    {
                        X.MessageBox.Show(new MessageBoxConfig
                        {
                            Title = "Weighing Before Cleanig  : Bon De Livraison",
                            Message = "Bon De Livraison not Found, Please Retry !",
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
                    Title = "Bon De Livraison : Submit Delivery",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();

        }

        public ActionResult LoadListOfAvailableDeliveries(string ItemSite,string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd, string ItemListeAdded = "", string ItemPeseeID = null, string ItemLivraisonType = "")
        {
            int? siteID = ItemSite == "" ? (int?)null : int.Parse(ItemSite);
            int? fournisseurID = ItemFournisseur == "" ? -1 : int.Parse(ItemFournisseur);
            int? livraisonTypeID = ItemLivraisonType == "" ? -1 : int.Parse(ItemLivraisonType);
            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            //Guid? peseeID = string.IsNullOrEmpty(ItemPeseeID) ? Guid.Empty : Guid.Parse(ItemPeseeID);

            //if (peseeID == Guid.Empty)
            //    peseeID = null;           
            var mListe = (new Livraison()).fnSelectForWeighingInSite((int)siteID,(int)fournisseurID, StartDate, EndDate, (int)livraisonTypeID);
            return this.Store(mListe);
        }

        public ActionResult SetAvailableNbrSacs(string ItemListeAdded = "", string ItemListeToAdd = "")
        {
            if (!string.IsNullOrEmpty(ItemListeAdded) && ItemListeAdded != "[]" && !string.IsNullOrEmpty(ItemListeToAdd) && ItemListeToAdd != "[]")
            {
                List<PeseeProductionPalette> oldListe = JSON.Deserialize<List<PeseeProductionPalette>>(ItemListeAdded, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                List<CompositionUsinageLivraison> newListe = JSON.Deserialize<List<CompositionUsinageLivraison>>(ItemListeToAdd, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });                
                int nbr = 0;
                foreach (CompositionUsinageLivraison itemNew in newListe)
                {
                    nbr = 0;
                    foreach (PeseeProductionPalette itemOld in oldListe.Where(x => x.NumeroLivraison == itemNew.LivraisonID))
                    {
                        nbr += itemOld.NombreSacs;                        
                    }
                    if (itemNew.NbreSacsTotalLivraison != (nbr + itemNew.NombreSacs))
                    {
                        itemNew.NombreSacs = itemNew.NbreSacsTotalLivraison - nbr;
                    }

                }

                Store mStoreMelange = X.GetCmp<Store>("storeListDeliveryNotes");
                mStoreMelange.RemoveAll();
                mStoreMelange.Add(newListe.Where(n => n.NombreSacs > 0));
            }

            return this.Direct();
        }


        public ActionResult OnRefreshForProduction(string ItemQuart, string ItemMelangeur, string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatut)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeSelectOrdreProduction");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemQuart",ItemQuart),
                                    new Ext.Net.Parameter("ItemMelangeur",ItemMelangeur),
                                    new Ext.Net.Parameter("ItemCertification",ItemCertification),
                                    new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatut",ItemStatut)
                                });


                //X.GetCmp<FormPanel>("OrdreProductionCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production Order : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
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
            //X.GetCmp<FormPanel>("CriteriaPanelDeliveryNote").Collapse(Direction.Top, false);

            return this.Direct();
        }

        public ActionResult SubmitWeightPAU(string ItemSelected = "")
        {
            int mIndex = 0;
            decimal tareSacs = 0;
            int NbrSacLivraisons = 0;

            Parametres mParam = new Parametres(0);
            List<PeseeSitePalette> ListePalettesPesees = JSON.Deserialize<List<PeseeSitePalette>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            if (ListePalettesPesees != null) mIndex = ListePalettesPesees.Count;

            PeseeSitePalette mPesee = new PeseeSitePalette();
            //mPesee.BonDeLivraison = new BonDeLivraison();
            mPesee.ID = Guid.NewGuid();

            if (X.GetCmp<TextField>("txtNombreSacsPAU").Text != string.Empty) mPesee.NombreSacs = int.Parse(X.GetCmp<TextField>("txtNombreSacsPAU").Text);
            if (X.GetCmp<TextField>("txtPoidsBrutPAU").Text != string.Empty) mPesee.PoidsBrut = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrutPAU").Text);
            if (X.GetCmp<Hidden>("NombreSacsLivraison").Text != string.Empty) NbrSacLivraisons = int.Parse(X.GetCmp<Hidden>("NombreSacsLivraison").Text);
            if (X.GetCmp<Hidden>("hidTareUnitaire").Text != string.Empty) tareSacs = decimal.Parse(X.GetCmp<Hidden>("hidTareUnitaire").Text);
            mPesee.TareSacs = mPesee.NombreSacs * tareSacs;
            mPesee.IsNew = true;
            mPesee.DatePesee = DateTime.Now;

            if (mPesee.NombreSacs > 0 && mPesee.PoidsBrut > 0)
            {
                //if (mPesee.NombreSacs > NbrSacLivraisons)
                //{
                //    X.MessageBox.Show(new MessageBoxConfig
                //    {
                //        Title = "Weighing : Data Validation",
                //        Message = "The number Of bags of the delivery is lower than selected bags !",
                //        Buttons = MessageBox.Button.OK,
                //        Icon = MessageBox.Icon.WARNING
                //    });
                //    return this.Direct();
                //}

                if (mPesee.NombreSacs > mParam.NbrSacAutorisePeseeSurSite)
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Weighing : Data Validation",
                        Message = "Verify Number Of Bags Per Pallet",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }

                if (ListePalettesPesees != null && (ListePalettesPesees.Count > mParam.NombrePalettesAutoriseAuPesePalette))
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Weighing : Data Validation",
                        Message = "Can't Weight New Pallet",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }

                //if (ListePalettesPesees != null && ((ListePalettesPesees.Count > 0) && ((ListePalettesPesees.Where(l => l.NumeroLivraison == mPesee.NumeroLivraison).Sum(x => x.NombreSacs) + mPesee.NombreSacs) > NbrSacLivraisons)))
                //{
                //    X.MessageBox.Show(new MessageBoxConfig
                //    {
                //        Title = "Weighing : Data Validation",
                //        Message = "Total Nbr Of Bag is higher than authorized Number Of bags",
                //        Buttons = MessageBox.Button.OK,
                //        Icon = MessageBox.Icon.WARNING
                //    });
                //    return this.Direct();
                //}

                Store mstore = X.GetCmp<Store>("storeListeWeightPAG");
                mstore.Insert(mIndex,mPesee);
                X.GetCmp<RowSelectionModel>("rowWeightPAG").Select(mIndex);
                
                X.GetCmp<Window>("FormAddWeight").Close();
            }            
            return this.Direct();
        }

        //public ActionResult SubmitFormMethod(string ItemSelected)
        //{
        //    DataSource _db = new DataSource();
        //    DataTransaction mtran = new DataTransaction();

        //    try
        //    {
        //        PeseeAvantUsinage mPesee = new PeseeAvantUsinage();
        //        PeseeProductionPalette palette;
        //        Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

        //        if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew && WeighingIsCreated == false)
        //            mPesee.IsNew = true;
        //        else
        //        {
        //            formExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
        //            mPesee.IsNew = false;
        //            if (WeighingID == Guid.Empty)
        //                mPesee.fnGet(Guid.Parse(GetFormValue("TxtPeseeSiteID")));
        //            else
        //                mPesee.fnGet(WeighingID);

        //            if (mPesee == null || mPesee.ID == Guid.Empty)
        //                throw new Exception("SubmitFormMethod : Weighing load failed.");
        //        }

        //        bool result = false;
        //        bool resultPalette = true;
        //        mPesee = MapFormToObject(mPesee);

        //        _db = mPesee.db();
        //        mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
        //        result = mPesee.fnUpdate(mtran);

        //        if (WeighingIsCreated)
        //            WeighingID = mPesee.ID;

        //        if (result)
        //        {                    

        //            List<PeseeProductionPalette> item = new List<PeseeProductionPalette>();
        //            if (!string.IsNullOrEmpty(ItemSelected) || ItemSelected != "[]")
        //                item = JSON.Deserialize<List<PeseeProductionPalette>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

        //            if (item.Count > 0)
        //            {

        //                for (int i = 0; i < item.Count(); i++)
        //                {
        //                    palette = new PeseeProductionPalette();
        //                    palette.PeseeProduction = new PeseeAvantUsinage();
        //                    palette.BonDeLivraison = new BonDeLivraison();

        //                    palette.SetDataSource(_db);

        //                    palette.PeseeProduction.ID = WeighingIsCreated ? WeighingID : mPesee.ID;                            
        //                    palette.NombreSacs = item[i].NombreSacs;
        //                    palette.PoidsBrut = item[i].PoidsBrut;
        //                    palette.TareSacs = item[i].TareSacs;
        //                    palette.IsNew = item[i].IsNew;
        //                    palette.BonDeLivraison.ID = item[i].BonDeLivraison.ID;
        //                    palette.NumeroLivraison = item[i].NumeroLivraison;
        //                    palette.LivraisonImmatriculation = item[i].LivraisonImmatriculation;
        //                    palette.DatePesee = item[i].DatePesee;

        //                    palette.UtilisateurCreation = (string)Session["userName"];
        //                    palette.UtilisateurModification = (string)Session["userName"];
        //                    Store mstorePalette = X.GetCmp<Store>("storeListeWeightPAG");
        //                    ModelProxy mProxyPalette = mstorePalette.GetById(item[i].ID);

        //                    if (palette.IsNew)
        //                    {
        //                        resultPalette = palette.fnUpdate(mtran);
        //                    }
        //                    item[i].IsNew = false;
                            
        //                    mProxyPalette.BeginEdit();

        //                    mProxyPalette.Set(item[i]);

        //                    mProxyPalette.Commit();

        //                    mProxyPalette.EndEdit();
        //                    if (!resultPalette)
        //                    {
        //                        break;
        //                    }
        //                }
        //            }

        //            if (!resultPalette)
        //            {
        //                _db.RollBackTransaction(mtran);
        //            }

        //            _db.CommitTransaction(mtran);
        //            WeighingIsCreated = true;
        //            WeighingID = mPesee.ID;
        //        }

        //        if (result)
        //        {
        //            Store mstore = X.GetCmp<Store>("storeListePeseeAgence");
        //            if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
        //            {
        //                mstore.Insert(0, mPesee);
        //                X.GetCmp<RowSelectionModel>("rowPeseeAgence").Select(0);
        //            }
        //            else
        //            {
        //                ModelProxy mProxy = mstore.GetById(mPesee.ID);

        //                mProxy.BeginEdit();

        //                mProxy.Set(mPesee);

        //                mProxy.Commit();

        //                mProxy.EndEdit();
        //            }

        //            //X.GetCmp<Window>("FormPeseeAgence").Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        X.MessageBox.Show(new MessageBoxConfig
        //        {
        //            Title = "Weighing : Data Validation",
        //            Message = ex.Message,
        //            Buttons = MessageBox.Button.OK,
        //            Icon = MessageBox.Icon.WARNING
        //        });
        //    }
        //    return this.Direct();
        //}

        public ActionResult SubmitFormWeighingBeforePallets()
        {            
            try
            {
                Guid peseeID = Guid.Empty;
                DateTime datePesee = new DateTime();
                PeseeSite mPesee = new PeseeSite();                
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mPesee.IsNew = true;
                else
                {
                    formExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                    mPesee.IsNew = false;
                    bool isGuid = Guid.TryParse(GetFormValue("TxtPeseeSiteID"), out peseeID);
                    if (isGuid)
                        mPesee.fnGet(peseeID);
                    //else
                    //    mPesee.fnGet(WeighingID);

                    if (mPesee == null || mPesee.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Weighing load failed.");

                    datePesee = mPesee.DatePesee;
                }

                bool result = false;
                
                mPesee = MapFormToObject(mPesee);
                if (mPesee.IsNew == false && (datePesee.Date == mPesee.DatePesee.Date))
                    mPesee.DatePesee = datePesee;
                                
                result = mPesee.fnUpdate();

                //if (WeighingIsCreated)
                //    WeighingID = mPesee.ID;

                if (result)
                {                    
                    //WeighingIsCreated = true;
                    //WeighingID = mPesee.ID;

                    Store mstore = X.GetCmp<Store>("storeListePeseeAgence");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mPesee);
                        X.GetCmp<RowSelectionModel>("rowPeseeAgence").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mPesee.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mPesee);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }
                    X.GetCmp<Container>("zonePalettes").Enable();
                    X.GetCmp<Container>("BtnAddWeight").Enable();
                    X.GetCmp<Hidden>("TxtPeseeSiteID").SetValue(mPesee.ID);
                    X.GetCmp<Hidden>("hiddenExecMode").SetValue("Update");
                    //X.GetCmp<Panel>("PanelBtnSave").Hide();
                    //X.GetCmp<Window>("FormPeseeAgence").Close();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        public ActionResult SubmitWeighingPallet(string ItemSelected= "", string peseeID = "", string nbrSacs = "", string poidsPalette = "" )
        {            
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                PeseeSite mPesee = new PeseeSite();
                PeseeSitePalette mPalette;
                DateTime datePesee = new DateTime();
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                formExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                mPesee.IsNew = false;

                Guid IdPesee = Guid.Empty;
                bool isGuid = Guid.TryParse(peseeID, out IdPesee);

                if (isGuid)
                    mPesee.fnGet(IdPesee);                

                if (mPesee == null || mPesee.ID == Guid.Empty)
                    throw new Exception("SubmitFormMethod : Weighing load failed.");

                bool result = false;
                bool resultPalette = true;
                datePesee = mPesee.DatePesee;
                mPesee = MapFormToObject(mPesee);

                if (mPesee.IsNew == false && (datePesee.Date == mPesee.DatePesee.Date))
                    mPesee.DatePesee = datePesee;

                _db = mPesee.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = mPesee.fnUpdate(mtran);

                //if (WeighingIsCreated)
                //    WeighingID = mPesee.ID;

                if (result)
                {
                    List<PeseeSitePalette> ListePalettesPesees = JSON.Deserialize<List<PeseeSitePalette>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (ListePalettesPesees.Count > 0)
                    {
                        foreach (PeseeSitePalette item in ListePalettesPesees.Where(p => p.IsNew == true))
                        {
                            mPalette = new PeseeSitePalette();
                            mPalette.PeseeSite = new PeseeSite();                            

                            mPalette.SetDataSource(_db);
                            mPalette.ID = item.ID;
                            mPalette.PeseeSite.ID = mPesee.ID;
                            mPalette.NombreSacs = item.NombreSacs;
                            mPalette.PoidsBrut = item.PoidsBrut;
                            mPalette.TareSacs = item.TareSacs;
                            mPalette.IsNew = item.IsNew;
                            mPalette.DatePesee = item.DatePesee;
                            mPalette.UtilisateurCreation = (string)Session["userName"];
                            mPalette.UtilisateurModification = (string)Session["userName"];
                            Store mstorePalette = X.GetCmp<Store>("storeListeWeightPAG");
                            if (mPalette.IsNew)
                            {
                                resultPalette = mPalette.fnUpdate(mtran);
                                
                                ModelProxy mProxyPalette = mstorePalette.GetById(item.ID);
                                item.ID = mPalette.ID;
                                item.IsNew = false;                                
                                mProxyPalette.BeginEdit();
                                mProxyPalette.Set(item);
                                mProxyPalette.Commit();
                                mProxyPalette.EndEdit();
                            }
                            //mPalette.IsNew = false;

                            if (!resultPalette)
                            {
                                _db.RollBackTransaction(mtran);
                                result = false;
                                break;
                            }
                        }
                    }
                    if (result)
                    {
                        _db.CommitTransaction(mtran);
                        //WeighingIsCreated = true;
                        //WeighingID = mPesee.ID;

                        Store mstore = X.GetCmp<Store>("storeListePeseeAgence");
                        ModelProxy mProxy = mstore.GetById(mPesee.ID);
                        mProxy.BeginEdit();
                        mProxy.Set(mPesee);
                        mProxy.Commit();
                        mProxy.EndEdit();
                        //X.GetCmp<Window>("FormAddWeight").Close();
                        X.GetCmp<Hidden>("TxtPeseeSiteID").SetValue(mPesee.ID);
                        X.GetCmp<Hidden>("hiddenExecMode").SetValue("Update");
                    }
                }                
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }


        public ActionResult Finalize(string ItemSelected)
        {            
            try
            {
                PeseeSite mPesee = new PeseeSite();
                Guid peseeID = Guid.Empty;
                
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                formExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                mPesee.IsNew = false;
                bool IsGuid = Guid.TryParse(GetFormValue("TxtPeseeSiteID"), out peseeID);

                if (IsGuid)
                    mPesee.fnGet(peseeID);                

                if (mPesee == null || mPesee.ID == Guid.Empty)
                    throw new Exception("SubmitFormMethod : Weighing load failed.");

                bool result = false;
                
                DateTime datePesee = new DateTime();

                datePesee = mPesee.DatePesee;
                mPesee = MapFormToObject(mPesee);
                if (mPesee.IsNew == false && (datePesee.Date == mPesee.DatePesee.Date))
                    mPesee.DatePesee = datePesee;

                //if (mPesee.TarePalette <= 0)
                //{
                //    X.MessageBox.Show(new MessageBoxConfig
                //    {
                //        Title = "Weighing : Data Validation",
                //        Message = "Set Tare Des Palettes Before Finalize",
                //        Buttons = MessageBox.Button.OK,
                //        Icon = MessageBox.Icon.WARNING
                //    });
                //    return this.Direct();
                //}

                mPesee.Statut = "AP";
                //_db = mPesee.db();
                //mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = mPesee.fnUpdate();

                //if (result)
                //{
                //    List<PeseeSitePalette> item = JSON.Deserialize<List<PeseeSitePalette>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                //    if (item.Count > 0)
                //    {

                //        for (int i = 0; i < item.Count(); i++)
                //        {
                //            palette = new PeseeSitePalette();
                //            palette.PeseeSite = new PeseeSite();

                //            palette.SetDataSource(_db);

                //            palette.PeseeSite.ID = mPesee.ID;
                //            palette.NombreSacs = item[i].NombreSacs;
                //            palette.PoidsBrut = item[i].PoidsBrut;
                //            palette.TareSacs = item[i].TareSacs;
                //            palette.IsNew = item[i].IsNew;
                //            palette.DatePesee = item[i].DatePesee;

                //            palette.UtilisateurCreation = (string)Session["userName"];
                //            palette.UtilisateurModification = (string)Session["userName"];
                //            if (palette.IsNew)
                //            {
                //                resultPalette = palette.fnUpdate(mtran);
                //            }

                //            if (!resultPalette)
                //            {
                //                break;
                //            }
                //        }
                //    }
                //    if (!resultPalette)
                //    {
                //        _db.RollBackTransaction(mtran);
                //    }
                //    _db.CommitTransaction(mtran);
                //}


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePeseeAgence");
                    ModelProxy mProxy = mstore.GetById(mPesee.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mPesee);

                    mProxy.Commit();

                    mProxy.EndEdit();
                    X.GetCmp<Window>("FormPeseeAgence").Close();

                    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                    X.Js.Call("GenerateTicket", mPesee.ID, BaseUrl);
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        private PeseeSite MapFormToObject(PeseeSite mClass)
        {            
            mClass.Sites = new Site();
            mClass.Sites.ID = int.Parse(GetFormValue("txtSiteID"));
            mClass.Sites.Nom = X.GetCmp<TextField>("txtNomSite").Text;

            mClass.Livraison = new Livraison();
            mClass.Livraison.ID = Guid.Parse(GetFormValue("txtLivraisonID"));
            mClass.Livraison.Numero = X.GetCmp<TextField>("txtDeliveryNumero").Text;
            mClass.Livraison.Immatriculation = X.GetCmp<Hidden>("txtLivraisonImmatriculation").Text;

            mClass.SacType = new SacType();
            mClass.SacType.ID = int.Parse(GetFormValue("cmbSacType"));
            mClass.SacType.Designation = X.GetCmp<ComboBox>("cmbSacType").SelectedItem.Text;            

            mClass.DatePesee = DateTime.Parse(X.GetCmp<DateField>("txtDatePesee").RawText.ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second).AddMilliseconds(DateTime.Now.Millisecond);            
            mClass.Statut = "NA";
            
            mClass.IsManual = bool.Parse(X.GetCmp<Hidden>("txtEstManuel").Text);

            if (X.GetCmp<TextField>("txtNombreSacs").Text != string.Empty) mClass.NombreSacs = int.Parse(X.GetCmp<TextField>("txtNombreSacs").Text);
            if (X.GetCmp<TextField>("txtPoidsBrut").Text != string.Empty) mClass.PoidsBrut = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrut").Text);
            if (X.GetCmp<TextField>("txtTareSacs").Text != string.Empty) mClass.TareSacs = decimal.Parse(X.GetCmp<TextField>("txtTareSacs").Text);

            if (X.GetCmp<TextField>("txtTarePalettes").Text != string.Empty) mClass.TarePalette = decimal.Parse(X.GetCmp<TextField>("txtTarePalettes").Text);
            //Prevenir Changement depuis la console du navigateur
            //if (!mClass.IsManual && (mClass.TarePalette != CapturedTare))
            //{
            //    throw new Exception("An error occured, please retry Weighing !");
            //}

            if (X.GetCmp<TextField>("txtTarePalettes").Text != string.Empty) mClass.TarePalette = decimal.Parse(X.GetCmp<TextField>("txtTarePalettes").Text);
            if (X.GetCmp<TextField>("txtNetWeight").Text != string.Empty) mClass.PoidsNet = decimal.Parse(X.GetCmp<TextField>("txtNetWeight").Text);

            mClass.Commentaire = X.GetCmp<TextField>("txtComment").Text;
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }

        public ActionResult SelectPalletsWeight(string PeseeId)
        {
            try
            {
                Guid IdPesee = Guid.Parse(PeseeId);
                PeseeSitePalette mPalettes = new PeseeSitePalette();
                var Liste = mPalettes.fnSelect(IdPesee);
                return this.Store(Liste);
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing : Data Validation",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Store(null);
            }
            
        }

        public ActionResult RemoveWeighing(string ItemSelected)
        {
            try
            {
                PeseeSitePalette mPalette = JSON.Deserialize<PeseeSitePalette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });                

                if (mPalette.ID != Guid.Empty)
                {
                    Store mstore = X.GetCmp<Store>("storeListeWeightPAG");

                    if (mPalette.IsNew)
                    {
                        ModelProxy _proxy = mstore.GetById(mPalette.ID);
                        _proxy.Drop();
                        return this.Direct();
                    }
                    else
                    {                        
                        mPalette.fnGet(mPalette.ID);
                        if (mPalette == null || mPalette.ID == Guid.Empty)
                            throw new Exception("RemoveWeighing : Retirer Weighing failed.");

                        mPalette.UtilisateurModification = (string)Session["userName"];

                        bool result = mPalette.fnRemove();

                        if (result)
                        {
                            ModelProxy mProxy = mstore.GetById(mPalette.ID);
                            mProxy.Drop();                                                        
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing - : Retirer Pallets Weight",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitOrdreProduction(string ItemSelected = "", string ItemNumero = "")
        {
            try
            {
                OrdreProduction mclass = new OrdreProduction();

                if (!string.IsNullOrEmpty(ItemNumero))
                {
                    if (mclass.fnGetByNumber(ItemNumero))
                    {
                        if (mclass.ID != Guid.Empty)
                        {
                            X.GetCmp<Hidden>("txtOrdreProductionID").SetValue(mclass.ID);
                            X.GetCmp<Hidden>("txtOrdreProduction").SetValue(mclass.NumeroProduction);                            
                            X.GetCmp<Window>("PeseeAvantUsinage_Production").Close();
                            X.GetCmp<Button>("BtnAddWeight").Enable();
                            return this.Direct();
                        }
                        else
                        {
                            X.GetCmp<Hidden>("txtOrdreProduction").SetValue("");
                            X.GetCmp<Hidden>("txtOrdreProductionID").SetValue(Guid.Empty);
                            X.MessageBox.Show(new MessageBoxConfig
                            {
                                Title = "Production : Production Order",
                                Message = "Production Order Not Found, Please Retry !",
                                Buttons = MessageBox.Button.OK,
                                Icon = MessageBox.Icon.WARNING
                            });
                            return this.Direct();
                        }
                    }
                }
                else
                {
                    mclass = JSON.Deserialize<OrdreProduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (mclass.ID != Guid.Empty)
                    {
                        X.GetCmp<Hidden>("txtOrdreProductionID").SetValue(mclass.ID);
                        X.GetCmp<Hidden>("txtOrdreProduction").SetValue(mclass.NumeroProduction);
                        X.GetCmp<Window>("PeseeAvantUsinage_Production").Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production Order - Data Validation",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnSelectProduction()
        {
            return new Ext.Net.MVC.PartialViewResult { ViewName = "PeseeAvantUsinage_Production" };
        }

        public ActionResult OnPrintWeighingTicket(string IdPesee, string IsCopy)
        {
            string BaseUrl = "";
            bool ReportIsCopy = false;
            try
            {                
                if (string.IsNullOrEmpty(IsCopy))
                    ReportIsCopy = true;

                if (string.IsNullOrEmpty(IdPesee))
                    return this.Direct();

                PeseeSite mPesee = new PeseeSite();
                bool result = mPesee.fnGet(Guid.Parse(IdPesee));
                if (result && mPesee.Statut != "AP")
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Ticket de Pesée",
                        Message = "Weighing Not Finalized",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }

                BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Ticket de Pesée",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }            
            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Ticket{0}', '{1}/PeseeAgence/ViewWeighingTicket?id={0}&IsCopy={2}', this, 'Ticket De pesée','')", IdPesee, BaseUrl, ReportIsCopy));
        }

        public ActionResult ViewWeighingTicket(string id, bool IsCopy)
        {
            try
            {
                XtraReport report = new TicketPeseeSite();                

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["peseeID"].Value = id;
                report.Parameters["IsCopy"].Value = IsCopy;
                
                ViewData["Report"] = report;
                return View("ViewReportResult", ViewData = ViewData);               
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing : Ticket",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }                    
            return View();
        }

        public ActionResult OnPrintWeighingList(string ItemPeriodStart = "", string ItemPeriodEnd = "")
        {
            Parametres mParam = new Parametres(0);
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

            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;

            if (mParam.Site == mSiteParDefaut.ID) ViewData["UrlSite"] = "LoadSiteAll";
            else ViewData["UrlSite"] = "LoadSiteByAccess";
            ViewData["Campagne"] = mParam.Campagne;
            ViewData["Titre"] = "Weighing";
           
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeListCritere", ViewData = ViewData };
        }

        public ActionResult PrintList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {              
                XtraReport report = null;

                report = new rptPeseeAgenceList() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                DateTime dateDebut = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetStartDate").RawText, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetEndDate").RawText, "d", CultureInfo.CurrentUICulture);

                report.Parameters["CampagneID"].Value = GetFormValue("cmbDetCropYear");
                //if (!string.IsNullOrEmpty(Session["paramCampagne"].ToString()) && Session["paramCampagne"].ToString() == "{Tous}")
                //    report.Parameters["paramCampagne"].Value = string.Empty;
                //else
                //    report.Parameters["paramCampagne"].Value = Session["paramCampagne"];

                report.Parameters["paramSite"].Value = GetFormValue("cmbDetSite");
                report.Parameters["paramSiteText"].Value = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text;

                report.Parameters["paramFournisseur"].Value = GetFormValue("cmbDetFournisseur");
                report.Parameters["paramFournisseurText"].Value = X.GetCmp<ComboBox>("cmbDetFournisseur").SelectedItem.Text;

                report.Parameters["paramStatut"].Value = GetFormValue("cmbDetStatus");
                report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("cmbDetStatus").SelectedItem.Text;

                report.Parameters["paramDateDebut"].Value = dateDebut;
                report.Parameters["paramDateFin"].Value = dateFin;

                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/PeseeAgence/ViewList', this, 'Weighing',''),App.FormPeseeListCritere.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing : Data Validation",
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
    }
}