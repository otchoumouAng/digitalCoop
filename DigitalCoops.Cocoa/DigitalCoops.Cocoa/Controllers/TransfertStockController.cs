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
using Tms.Classes.Business.stock;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;
using Tms.Components.Reports;
using Tms.Components.Settings;
using Tms2017.MVC.Models;
using Tms2017.MVC.Reports;
using Tms2017.Reports;

namespace Tms2017.MVC.Controllers
{
    public class TransfertStockController : BaseController
    {

        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";

        public TransfertStockController()
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

            //ViewBag.DefaultExportateur = mParam.Exportateur.ID;
            ViewBag.DefaultCampagne = mParam.Campagne;
            ViewBag.TransfertFeve = mParam.IDTransfertFevesAgence;

            Site mSiteParDefaut = new Site();

            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;
            ViewBag.MagasinReception = mSiteParDefaut.MagasinID;


            Fonction HasAccessFunction = new Fonction();
            bool HasAccessAllSite = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            ViewBag.HasAccessAllSite = HasAccessAllSite;

            X.GetCmp<FormPanel>("CriteriaPanel").SetTitle("Site : " + mSiteParDefaut.Nom + ", Aujourd'hui - Campagne : " + mParam.Campagne + ", Exportateur : " + mParam.Exportateur.Nom);
            #region Set Function's Access

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{5757ce19-23a8-4e72-9682-ba5b35a80c38}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{a99b02d1-46e6-420a-980a-943f16d58e86}")))
                X.GetCmp<Button>("btnExpedier").Enable();
            else
                X.GetCmp<Button>("btnExpedier").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{9a51a307-d66f-4fe6-a132-9ad134d0441d}")))
                X.GetCmp<Button>("mnuExportDeliveries").Enable();
            else
                X.GetCmp<Button>("mnuExportDeliveries").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{371b6119-d2cb-4467-a279-2e5968defd16}")))
                X.GetCmp<Button>("mnuPrintTransferSummary").Enable();
            else
                X.GetCmp<Button>("mnuPrintTransferSummary").Disable();

            X.GetCmp<Hidden>("tfhiddenPermModifier").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{5e6efa0a-6c19-48a3-b19a-76ada218edaf}")));
            X.GetCmp<Hidden>("tfhiddenPermDesactiver").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{741F7ACD-8066-4498-8E53-00F6B52E3C39}")));
            //X.GetCmp<Hidden>("tfhiddenPermPrintTransferSheet").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{3bd1749a-242c-4c11-b596-e998216e114f}")));
            X.GetCmp<Hidden>("tfhiddenPermExpedier").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{a99b02d1-46e6-420a-980a-943f16d58e86}")));
            X.GetCmp<Hidden>("tfhiddenPermReceptionLot").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{b1515db2-c14e-46a2-9e14-a40791e6ab89}")));
            //X.GetCmp<Hidden>("tfhiddenPermFinaliser").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{1a38f2d6-d513-4ec6-b090-cc8e141ef3ae}")));
            //X.GetCmp<Hidden>("tfhiddenPermDesactiverApresApprouve").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{F29BE407-EB21-488E-941B-8665060B4C62}")));
            //X.GetCmp<Hidden>("tfhiddenPermPrintSheetTraca").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{bca1825e-f265-405e-96ce-5267c728515d}")));
            #endregion

            //bool HasAccessAllSite = new Fonction().fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            ViewBag.SiteParDefaut = HasAccessAllSite ? -1 : mSiteParDefaut.ID;
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

        public ActionResult GetDeliveryBySupplierAll(string ItemFournisseur, string ItemMode)
        {

            List<DataPersist> myList = new List<DataPersist>();
            if (!string.IsNullOrEmpty(ItemFournisseur) || !string.IsNullOrEmpty(ItemMode))
            {
                if (ItemMode == "2")
                {
                    myList = new Livraison().fnSelect("{Tous}", -1, ItemFournisseur, DateTime.Now, DateTime.Now, -1, -1);

                    //List<DataPersist> myList = new Livraison().fnSelect("{Tous}", -1, ItemFournisseur, DateTime.Now);
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
                    Title = "Negociated Price : PRICE",
                    Message = "Mode of Application and Supplier must be selected",
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

        public ActionResult OnCreate()
        {
            VenteLotViewModel mclass = new VenteLotViewModel();

            try
            {
                string UserName = (string)Session["userName"];
                //Site mSiteParDefaut = new Site();
                //bool result = mSiteParDefaut.fnGetDefaultSite();
                Site mSiteParDefaut = new Site();

                //bool result1 = mSiteParDefaut.fnGetBySiteByUserName(UserName);
                Site msite = new Site();
                bool result = msite.fnGetBySiteByUserName(UserName);

                mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

                Parametres mParam = new Parametres(0);
                mclass._VenteLot = new VenteLot();
                mclass._VenteLot.Campagne = new Campagne();
                mclass._VenteLot.Campagne.Designation = mParam.Campagne;
                mclass._VenteLot.Sites = new Site();
                mclass._VenteLot.Sites.ID = msite.ID;
                mclass._VenteLot.Sites.Nom = msite.Nom;
                mclass._VenteLot.Magasin = new Magasin();
                mclass._VenteLot.Magasin.ID = msite.MagasinID;
                mclass._VenteLot.Destination = new Destination();
                mclass._VenteLot.Destination.ID = mParam.IDDestinationParDefaut;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Erreur", ex.Message).Show();
            }
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormExpedition", Model = mclass };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            VenteLotViewModel mclass = new VenteLotViewModel();

            try
            {
                mclass._VenteLot = new VenteLot();
                mclass._VenteLot = JSON.Deserialize<VenteLot>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                mclass._VenteLot.fnGet(mclass._VenteLot.ID);

                string UserName = (string)Session["userName"];

                //Site mSiteParDefaut = new Site();
                //bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

                mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

                //Parametres mParam = new Parametres(0);
                //mclass._VenteLot = new VenteLot();
                //mclass._VenteLot.Campagne = new Campagne();
                //mclass._VenteLot.Campagne.Designation = mParam.Campagne;
                //mclass._VenteLot.Sites = new Site();
                //mclass._VenteLot.Sites.ID = mSiteParDefaut.ID;
                //mclass._VenteLot.Sites.Nom = mSiteParDefaut.Nom;
                //mclass._VenteLot.Destination = new Destination();
                //mclass._VenteLot.Destination.ID = mSiteParDefaut.Destination.ID;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Erreur", ex.Message).Show();
            }
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormVenteLot", Model = mclass };
        }

        public ActionResult OnPrintSituationLot()
        {
            VenteLotViewModel mclass = new VenteLotViewModel();

            try
            {
                mclass._VenteLot = new VenteLot();
                string UserName = (string)Session["userName"];

                Site mSiteParDefaut = new Site();
                bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

                mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

                Parametres mParam = new Parametres(0);
                mclass._VenteLot = new VenteLot();
                mclass._VenteLot.Campagne = new Campagne();
                mclass._VenteLot.Campagne.Designation = mParam.Campagne;
                mclass._VenteLot.Sites = new Site();
                mclass._VenteLot.Sites.ID = mSiteParDefaut.ID;
                mclass._VenteLot.Sites.Nom = mSiteParDefaut.Nom;
                mclass._VenteLot.Destination = new Destination();
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Erreur", ex.Message).Show();
            }
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormCritSitationLot", Model = mclass };
        }

        public ActionResult OnPrintSituationEcart()
        {
            VenteLotViewModel mclass = new VenteLotViewModel();

            try
            {
                mclass._VenteLot = new VenteLot();
                string UserName = (string)Session["userName"];

                Site mSiteParDefaut = new Site();
                bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

                mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

                Parametres mParam = new Parametres(0);
                mclass._VenteLot = new VenteLot();
                mclass._VenteLot.Campagne = new Campagne();
                mclass._VenteLot.Campagne.Designation = mParam.Campagne;
                mclass._VenteLot.Sites = new Site();
                mclass._VenteLot.Sites.ID = mSiteParDefaut.ID;
                mclass._VenteLot.Sites.Nom = mSiteParDefaut.Nom;
                mclass._VenteLot.Destination = new Destination();
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Erreur", ex.Message).Show();
            }
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormCritSitationEcart", Model = mclass };
        }


        public ActionResult OnRecept(string ItemSelected)
        {
            VenteLotViewModel mclass = new VenteLotViewModel();
            mclass._VenteLot = new VenteLot();
            mclass._VenteLot = JSON.Deserialize<VenteLot>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._VenteLot.fnGet(mclass._VenteLot.ID);
            try
            {
                Site mSiteParDefaut = new Site();
                string UserName = (string)Session["userName"];
                //bool result1 = mSiteParDefaut.fnGetBySiteByUserName(UserName);
                Site msite = new Site();
                bool result = msite.fnGetBySiteByUserName(UserName);

                mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

                Parametres mParam = new Parametres(0);
                //mclass._VenteLot = new VenteLot();
                //mclass._VenteLot.Campagne = new Campagne();
                //mclass._VenteLot.Campagne.Designation = mParam.Campagne;
                //mclass._VenteLot.Sites = new Site();
                //mclass._VenteLot.Sites.ID = msite.ID;
                //mclass._VenteLot.Sites.Nom = msite.Nom;
                //mclass._VenteLot.Magasin = new Magasin();
                //mclass._VenteLot.Magasin.ID = msite.MagasinID;
                mclass._VenteLot.Destination = new Destination();
                mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Erreur", ex.Message).Show();
            }
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormReception", Model = mclass };
        }

        public ActionResult OnConsult(string ItemSelected)
        {
            VenteLotViewModel mclass = new VenteLotViewModel();

            try
            {
                mclass._VenteLot = new VenteLot();
                mclass._VenteLot = JSON.Deserialize<VenteLot>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                mclass._VenteLot.fnGet(mclass._VenteLot.ID);

                string UserName = (string)Session["userName"];

                //Site mSiteParDefaut = new Site();
                //bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

                mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;


            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Erreur", ex.Message).Show();
            }

            if (mclass._VenteLot.Statut == "NA")
                return new Ext.Net.MVC.PartialViewResult { ViewName = "FormExpeditionDetail", Model = mclass };
            else if (mclass._VenteLot.Statut == "AP")
                return new Ext.Net.MVC.PartialViewResult { ViewName = "FormVenteLotApprouve", Model = mclass };
            else if (mclass._VenteLot.Statut == "RE")
                return new Ext.Net.MVC.PartialViewResult { ViewName = "FormReceptionDetail", Model = mclass };
            else if (mclass._VenteLot.Statut == "VE")
                return new Ext.Net.MVC.PartialViewResult { ViewName = "FormVenteLot_ApprouveVente", Model = mclass };
            else
                return new Ext.Net.MVC.PartialViewResult { ViewName = "FormVenteLot", Model = mclass };
        }
        public ActionResult OnActivateDeactivate(string ItemSelected)
        {

            try
            {
                VenteLot mClass = JSON.Deserialize<VenteLot>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Guid mId = mClass.ID;

                bool result = mClass.fnGet(mId);

                //bool deActivate = mClass.Desactive;

                if (!result)
                    throw new Exception("OnActivateDeactivate : Ligne introuvable, veuillez réessayer svp !.");

                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];
                result = mClass.fnDeActivate();

                if (!result)
                    throw new Exception("OnCancel : Echec de l'operation, veuillez réessayer svp.");

                Store mstore = X.GetCmp<Store>("storeVenteList");

                ModelProxy mProxy = mstore.GetById(mClass.ID);
                mProxy.BeginEdit();
                mProxy.Set(mClass);
                mProxy.Commit();
                mProxy.EndEdit();
                DeselectGridRows();
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Erreur", ex.Message).Show();
            }

            return this.Direct();
        }


        [HttpPost]
        public ActionResult SubmitFormMethod(string FromWeighing = "")
        {
            try
            {
                VenteLot mClass = new VenteLot();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);
                int nbSacs = 0;
                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtTransfertID")));
                    nbSacs = mClass.NombreSacs;
                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Ligne introuvable, veuillez réessayer svp !");
                }

                mClass = MapFormToObject(mClass);
                bool result = false;
                if (mClass.IsNew == false && mClass.NombreSacs > 0)
                    result = mClass.fnUpdate();
                //bool 

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeVenteList");

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

                    CloseDetailWindow();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Erreur : SubmitFormMethod", ex.Message).Show();
            }

            return this.Direct();

        }

        //[HttpPost]
        //public ActionResult UpdateReception()
        //{
        //    VenteLot mClass = new VenteLot();
        //    try
        //    {

        //        Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

        //        mClass.IsNew = false;

        //        mClass.fnGet(Guid.Parse(GetFormValue("txtVenteID")));

        //        if (mClass == null || mClass.ID == Guid.Empty)
        //            throw new Exception("SubmitFormMethod : Ligne introuvable, Veuillez réessauer svp !");


        //        mClass = MapFormToObjectRecept(mClass);
        //        mClass.Statut = "RE";
        //        mClass.UtilisateurCreation = (string)Session["userName"];
        //        mClass.UtilisateurModification = (string)Session["userName"];

        //        bool result = mClass.fnUpdateReception();

        //        if (result)
        //        {
        //            Store mStore = X.GetCmp<Store>("storeVenteList");

        //            ModelProxy mProxy;

        //            if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
        //            {
        //                mStore.Insert(0, mClass);

        //                X.GetCmp<RowSelectionModel>("rowSelectionListe").Select(0);
        //            }
        //            else
        //            {
        //                mProxy = mStore.GetById(mClass.ID);
        //                mProxy.BeginEdit();
        //                mProxy.Set(mClass);
        //                mProxy.Commit();
        //                mProxy.EndEdit();
        //            }

        //            X.GetCmp<Window>("FormVenteLotRecept").Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        X.MessageBox.Alert("Erreur : SubmitFormMethod", ex.Message).Show();
        //    }

        //    return this.Direct();

        //}

        public ActionResult UpdateFormExpedition(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                VenteLot mVente = new VenteLot();
                VenteLotDetail mDetail;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);
                bool result = false;

                bool resultDetail = true;
                bool resultMouvement = false;
                mVente = MapFormToObjectExpedition(mVente);

                _db = mVente.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = mVente.fnUpdateExpedition(mtran);

                if (result)
                {


                    #region Mvt
                    MouvementStockAgence mouvement = new MouvementStockAgence();

                    //mVente.Statut = "VE";
                    mouvement = new MouvementStockAgence();
                    mouvement.mCampagne = new Campagne();
                    mouvement.Exportateur = new Exportateur();
                    mouvement.SacType = new SacType();
                    mouvement.TypeElementStock = new TypeElementStock();
                    mouvement.Certification = new Certification();
                    mouvement.Magasin = new Magasin();
                    mouvement.Emplacement = new Emplacement();
                    mouvement.MouvementStockType = new MouvementStockType();
                    mouvement.Sites = new Site();
                    InventaireElementStock Items = new InventaireElementStock();
                    Parametres mParam = new Parametres(0);
                    mouvement.SetDataSource(_db);
                    mouvement.mCampagne.Designation = mVente.Campagne.Designation;
                    mouvement.Exportateur.ID = mVente.Exportateur.ID;
                    mouvement.DateMouvement = mVente.DateExpedition;
                    mouvement.SacType.ID = mParam.SacExportType;
                    mouvement.ObjetEnStock = mVente.ID;
                    mouvement.ObjetEnStockType = mParam.LotTypeElementEnStock;
                    //mouvement.MouvementStockType.ID = mParam.MvtTypeVenteLot;
                    mouvement.MouvementStockType.ID = 30;
                    mouvement.Sites.ID = mVente.Sites.ID;
                    mouvement.Sites.Nom = mVente.Sites.Nom;
                    mouvement.Certification = null;
                    //if (mClass.Livraison.Certification.ID != 0)
                    //{
                    //    mouvement.Certification = new Certification();
                    //    mouvement.Certification.ID = mClass.Certification.ID;
                    //}
                    mouvement.Sens = -1;
                    mouvement.Quantite = mVente.NombreSacs;
                    mouvement.PoidsBrut = mVente.PoidsBrut;
                    mouvement.TarePalettes = mVente.TarePalette;
                    mouvement.TareSacs = mVente.TareSacs;
                    //mouvement.TareSacs = mClass.TareSacs;
                    mouvement.PoidsNetLivre = mVente.PoidsBrut;
                    mouvement.PoidsNetAccepte = mVente.PoidsNet;
                    //mouvement.Retention = mClass.PoidsBrut - mClass.PoidsNet;
                    mouvement.Retention = 0;
                    mouvement.Magasin.ID = mVente.Magasin.ID;
                    mouvement.Emplacement.ID = mParam.EmplacementParDefaut;
                    mouvement.Reference1 = mVente.Numero;
                    mouvement.Reference2 = mVente.Lot.Numero;
                    mouvement.Commentaire = "generé automatiquement";
                    mouvement.Statut = "AP";
                    mouvement.UtilisateurCreation = (string)Session["userName"];
                    mouvement.UtilisateurModification = (string)Session["userName"];

                    resultMouvement = mouvement.fnUpdate(mtran);
                }
                else
                {
                    _db.RollBackTransaction(mtran);
                }
                if (!resultMouvement)
                    _db.RollBackTransaction(mtran);
                #endregion
                if (result && resultMouvement)
                {
                    _db.CommitTransaction(mtran);

                    Store mstore = X.GetCmp<Store>("storeVenteList");
                    mVente.MagasinReception = null;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mVente);
                        X.GetCmp<RowSelectionModel>("rowSelectionListe").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mVente.ID);
                        mProxy.BeginEdit();
                        mProxy.Set(mVente);
                        mProxy.Commit();
                        mProxy.EndEdit();
                    }
                    X.GetCmp<Window>("FormExpedition").Close();
                    X.MessageBox.Show(
                        new MessageBoxConfig
                        {
                            Title = "Transfert",
                            Message = "Reception saisie avec Succès",
                            Buttons = MessageBox.Button.OK,
                            Icon = MessageBox.Icon.INFO
                        });
                }
                else
                {
                    // Handle the case where ApproveVente fails
                    _db.RollBackTransaction(mtran);
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Transfert : Validation",
                        Message = "Erreur lors de l'approbation de la vente.",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                }
            }
            catch (Exception ex)
            {
                if (_db != null)
                    _db.RollBackTransaction(mtran);

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Transfert : Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            X.GetCmp<FormPanel>("FormExpedition").Reset();
            return this.Direct();
        }

        private VenteLot MapFormToObjectExpedition(VenteLot mClass)
        {
            mClass.Sites = new Site();
            //mClass.Sites.ID = int.Parse(GetFormValue("txtSiteID"));
            //mClass.Sites.Nom = X.GetCmp<TextField>("txtNomSite").Text;

            mClass.Sites.ID = int.Parse(GetFormValue("cmbSites"));
            mClass.Sites.Nom = X.GetCmp<ComboBox>("cmbSites").SelectedItem.Text;

            mClass.Lot = new LotCoop();
            mClass.Lot.ID = Guid.Parse(GetFormValue("cmbLot"));
            mClass.Lot.Numero = X.GetCmp<ComboBox>("cmbLot").SelectedItem.Text.ToString();

            mClass.Statut = "NA";

            mClass.Exportateur = new Exportateur();
            Lot_GestionStock mLot = new Lot_GestionStock();
            bool res = mLot.fnGet(mClass.Lot.ID);
            mClass.Exportateur = new Exportateur();
            mClass.Exportateur.ID = mLot.Exportateur.ID;
            //mClass.Exportateur = new Exportateur();
            //mClass.Exportateur.ID = int.Parse(GetFormValue("cmbDetExportateur"));
            mClass.Exportateur.Nom = mLot.Exportateur.Nom;

            mClass.Magasin = new Magasin();
            mClass.Magasin.ID = int.Parse(GetFormValue("cmbMagasin"));
            mClass.Magasin.Designation = X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Text.ToString();

            mClass.MagasinExpedition = new Magasin();
            mClass.MagasinExpedition.ID = int.Parse(GetFormValue("cmbMagasin"));
            mClass.MagasinExpedition.Designation = X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Text.ToString();

            mClass.MagasinReception = new Magasin();
            mClass.MagasinReception.ID = int.Parse(GetFormValue("cmbMagasinDestination"));
            //mClass.MagasinDestination.ID = int.Parse(GetFormValue("cmbMagasinDestination"));
            //mClass.MagasinDestination.Designation = X.GetCmp<ComboBox>("cmbMagasinDestination").SelectedItem.Text.ToString();

            mClass.ImmTracteurExpedition1 = X.GetCmp<TextField>("txtImmTracteur").Text;
            mClass.ImmRemorqueExpedition1 = X.GetCmp<TextField>("txtImmRemorque").Text;
            mClass.NumBordereauSortie = X.GetCmp<TextField>("txtBordereauSortie").Text;
            mClass.NumeroExpedition = X.GetCmp<TextField>("txtBordereauSortie").Text;

            mClass.Campagne = new Tms.Classes.Shared.Campagne();
            mClass.Campagne.Designation = X.GetCmp<ComboBox>("cmbDetCrop").SelectedItem.Text.ToString();
            mClass.DateVente = DateTime.Parse(X.GetCmp<DateField>("txtDateVente").RawText.ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second).AddMilliseconds(DateTime.Now.Millisecond);
            mClass.DateExpedition = DateTime.Parse(X.GetCmp<DateField>("txtDateExpedition").RawText.ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second).AddMilliseconds(DateTime.Now.Millisecond);
            mClass.NumCCC = X.GetCmp<TextField>("txtNumeroCCC").Text;

            mClass.Commentaire = X.GetCmp<TextField>("TxtAreaCommentaire").Text;

            if (X.GetCmp<TextField>("txtNombreSacs").Text != string.Empty) mClass.NombreSacs = int.Parse(X.GetCmp<TextField>("txtNombreSacs").Text);
            if (X.GetCmp<TextField>("txtPoidsBrut").Text != string.Empty) mClass.PoidsBrut = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrut").Text);
            if (X.GetCmp<TextField>("txtNetWeight").Text != string.Empty) mClass.PoidsNet = decimal.Parse(X.GetCmp<TextField>("txtNetWeight").Text);

            if (X.GetCmp<TextField>("txtNombrePalette").Text != string.Empty) mClass.NombrePalette = int.Parse(X.GetCmp<TextField>("txtNombrePalette").Text);
            if (X.GetCmp<TextField>("txtTarePalette").Text != string.Empty) mClass.TarePalette = decimal.Parse(X.GetCmp<TextField>("txtTarePalette").Text);
            if (X.GetCmp<TextField>("txtTareSac").Text != string.Empty) mClass.TareSacs = decimal.Parse(X.GetCmp<TextField>("txtTareSac").Text);

            if (X.GetCmp<TextField>("txtPrixMoyen").Text != string.Empty) mClass.PrixMoyen = decimal.Parse(X.GetCmp<TextField>("txtPrixMoyen").Text);
            //mClass.Immatriculation = X.GetCmp<TextField>("txtImmatriculation").Text;

            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }

        [HttpPost]
        public ActionResult UpdateReception()
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                VenteLot mClass = new VenteLot();
                bool result = false;
                bool resultMouvement = false;

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                mClass.IsNew = false;

                mClass.fnGetReception(Guid.Parse(GetFormValue("txtTransfertID")));

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("SubmitFormMethod : Ligne introuvable, Veuillez réessauer svp !");

                mClass = MapFormToObjectRecept(mClass);
                mClass.Statut = "RE";
                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];
                _db = mClass.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = mClass.fnUpdateReception(mtran);

                if (result)
                {

                    #region Mvt
                    MouvementStockAgence mouvement = new MouvementStockAgence();

                    //mVente.Statut = "VE";
                    mouvement = new MouvementStockAgence();
                    mouvement.mCampagne = new Campagne();
                    mouvement.Exportateur = new Exportateur();
                    mouvement.SacType = new SacType();
                    mouvement.TypeElementStock = new TypeElementStock();
                    mouvement.Certification = new Certification();
                    mouvement.Magasin = new Magasin();
                    mouvement.Emplacement = new Emplacement();
                    mouvement.MouvementStockType = new MouvementStockType();
                    mouvement.Sites = new Site();
                    InventaireElementStock Items = new InventaireElementStock();
                    Parametres mParam = new Parametres(0);
                    mouvement.SetDataSource(_db);
                    mouvement.mCampagne.Designation = mClass.Campagne.Designation;
                    mouvement.Exportateur.ID = mClass.Exportateur.ID;
                    mouvement.DateMouvement = mClass.DateReception;
                    mouvement.SacType.ID = mParam.SacExportType;
                    mouvement.ObjetEnStock = mClass.ID;
                    mouvement.ObjetEnStockType = mParam.LotTypeElementEnStock;
                    //mouvement.MouvementStockType.ID = mParam.MvtTypeVenteLot;
                    mouvement.MouvementStockType.ID = 31;
                    mouvement.Sites.ID = mClass.Sites.ID;
                    mouvement.Sites.Nom = mClass.Sites.Nom;
                    mouvement.Certification = null;
                    //if (mClass.Livraison.Certification.ID != 0)
                    //{
                    //    mouvement.Certification = new Certification();
                    //    mouvement.Certification.ID = mClass.Certification.ID;
                    //}
                    mouvement.Sens = 1;
                    mouvement.Quantite = mClass.NombreSacs;
                    mouvement.PoidsBrut = mClass.PoidsBrut;
                    mouvement.TarePalettes = mClass.TarePalette;
                    mouvement.TareSacs = mClass.TareSacs;
                    //mouvement.TareSacs = mClass.TareSacs;
                    mouvement.PoidsNetLivre = mClass.PoidsBrut;
                    mouvement.PoidsNetAccepte = mClass.PoidsNet;
                    //mouvement.Retention = mClass.PoidsBrut - mClass.PoidsNet;
                    mouvement.Retention = 0;
                    mouvement.Magasin.ID = mClass.MagasinReception.ID;
                    mouvement.Emplacement.ID = mParam.EmplacementParDefaut;
                    mouvement.Reference1 = mClass.Numero;
                    mouvement.Reference2 = mClass.Lot.Numero;
                    mouvement.Commentaire = "generé automatiquement";
                    mouvement.Statut = "AP";
                    mouvement.UtilisateurCreation = (string)Session["userName"];
                    mouvement.UtilisateurModification = (string)Session["userName"];

                    resultMouvement = mouvement.fnUpdate(mtran);
                }
                else
                {
                    _db.RollBackTransaction(mtran);
                }
                if (!resultMouvement)
                    _db.RollBackTransaction(mtran);
                #endregion
                if (result && resultMouvement)
                {
                    _db.CommitTransaction(mtran);

                    Store mStore = X.GetCmp<Store>("storeVenteList");

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

                    X.GetCmp<Window>("FormReception").Close();

                    //X.MessageBox.Show(
                    //    new MessageBoxConfig
                    //    {
                    //        Title = "Transfert : Validation",
                    //        Message = "OK",
                    //        Buttons = MessageBox.Button.OK,
                    //        Icon = MessageBox.Icon.INFO
                    //    });
                }
                else
                {
                    // Handle the case where ApproveVente fails
                    _db.RollBackTransaction(mtran);
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Transfert : Validation",
                        Message = "Erreur lors de l'approbation de la vente.",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                }

            }
            catch (Exception ex)
            {
                if (_db != null)
                    _db.RollBackTransaction(mtran);
                X.MessageBox.Alert("Erreur : SubmitFormMethod", ex.Message).Show();
            }

            return this.Direct();

        }


        [HttpPost]
        public ActionResult ApproveVente()
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();
            VenteLot mClass = new VenteLot();
            MouvementStockAgence mouvement = new MouvementStockAgence();
            try
            {
                bool result = false;
                bool resultMouvement = false;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                mClass.IsNew = false;

                mClass.fnGet(Guid.Parse(GetFormValue("txtVenteID")));

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("SubmitFormMethod : Ligne introuvable, veuillez réessayer svp !");


                //mClass = MapFormToObjectRecept(mClass);
                mClass.Statut = "VE";
                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];
                mClass.UserApprobation = (string)Session["userName"];
                mClass.DateApprobation = DateTime.Now;

                _db = mClass.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = mClass.fnApproveVente(mtran);

                if (result)
                {
                    mouvement = new MouvementStockAgence();
                    mouvement.mCampagne = new Campagne();
                    mouvement.Exportateur = new Exportateur();
                    mouvement.SacType = new SacType();
                    mouvement.TypeElementStock = new TypeElementStock();
                    mouvement.Certification = new Certification();
                    mouvement.Magasin = new Magasin();
                    mouvement.Emplacement = new Emplacement();
                    mouvement.MouvementStockType = new MouvementStockType();
                    mouvement.Sites = new Site();
                    InventaireElementStock Items = new InventaireElementStock();
                    Parametres mParam = new Parametres(0);
                    mouvement.SetDataSource(_db);
                    mouvement.mCampagne.Designation = mClass.Campagne.Designation;
                    mouvement.Exportateur.ID = mParam.Exportateur.ID;
                    mouvement.DateMouvement = DateTime.Now;
                    mouvement.SacType.ID = mParam.SacBrousseType;
                    mouvement.ObjetEnStock = mClass.ID;
                    mouvement.ObjetEnStockType = mParam.LotTypeElementEnStock;
                    mouvement.MouvementStockType.ID = mParam.MvtTypeVenteLot;
                    mouvement.Sites.ID = mClass.Sites.ID;
                    mouvement.Sites.Nom = mClass.Sites.Nom;
                    mouvement.Certification = null;
                    //if (mClass.Livraison.Certification.ID != 0)
                    //{
                    //    mouvement.Certification = new Certification();
                    //    mouvement.Certification.ID = mClass.Certification.ID;
                    //}
                    mouvement.Sens = -1;
                    mouvement.Quantite = mClass.NombreSacs;
                    mouvement.PoidsBrut = mClass.PoidsBrut;
                    mouvement.TarePalettes = 0;
                    mouvement.TareSacs = 0;
                    //mouvement.TareSacs = mClass.TareSacs;
                    mouvement.PoidsNetLivre = mClass.PoidsBrut;
                    mouvement.PoidsNetAccepte = mClass.PoidsNet;
                    //mouvement.Retention = mClass.PoidsBrut - mClass.PoidsNet;
                    mouvement.Retention = 0;
                    mouvement.Magasin.ID = mParam.MagasinTV;
                    mouvement.Emplacement.ID = mParam.EmplacementParDefaut;
                    mouvement.Reference1 = mClass.Numero;
                    mouvement.Reference2 = mClass.Numero;
                    mouvement.Commentaire = "generé automatiquement";
                    mouvement.Statut = "AP";
                    mouvement.UtilisateurCreation = (string)Session["userName"];
                    mouvement.UtilisateurModification = (string)Session["userName"];

                    resultMouvement = mouvement.fnUpdate(mtran);

                    if (!resultMouvement)
                        _db.RollBackTransaction(mtran);
                }

                if (result && resultMouvement)
                {
                    _db.CommitTransaction(mtran);
                    Store mStore = X.GetCmp<Store>("storeVenteList");

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

                    X.GetCmp<Window>("FormVenteLot_ApprouveVente").Close();
                }
            }
            catch (Exception ex)
            {
                if (_db != null)
                    _db.RollBackTransaction(mtran);
                X.MessageBox.Alert("Erreur : SubmitFormMethod", ex.Message).Show();
            }

            return this.Direct();

        }


        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne, string ItemStartDate, string ItemEndDate, string ItemStatus, string ItemSite, string ItemMagasinExpedition, string ItemMagasinReception, string ItemExportateur, string ItemTransit = "")
        {

            Parametres para = new Parametres(0);
            string defaultCrop = para.Campagne;

            string Crop = defaultCrop;
            if (!String.IsNullOrEmpty(ItemCampagne))
                Crop = ItemCampagne;

            string status = string.IsNullOrEmpty(ItemStatus) ? "-1" : ItemStatus;
            if (!string.IsNullOrEmpty(ItemStatus) && ItemStatus.Contains("null"))
            {
                status = "-1";
            }

            int siteID = GetCritriaValue(ItemSite);
            int ExportateurID = GetCritriaValue(ItemExportateur);
            int MagasinExpeditionID = GetCritriaValue(ItemMagasinExpedition);
            int MagasinReceptionID = GetCritriaValue(ItemMagasinReception);
            bool _EnTransit = false;
            bool res = bool.TryParse(ItemTransit, out _EnTransit);
            int _IndTransit = 0;
            _IndTransit = _EnTransit ? 1 : 0;
            DateTimeFormatInfo ukDtfi = new CultureInfo("fr-FR", false).DateTimeFormat;
            DateTime StartDate = DateTime.Now.AddDays(-1);
            if (!String.IsNullOrEmpty(ItemStartDate) && !ItemStartDate.Contains("1/1/0001"))
                StartDate = DateTime.Parse(ItemStartDate);

            DateTime EndDate = DateTime.Now;
            if (!String.IsNullOrEmpty(ItemEndDate) && !ItemEndDate.Contains("1/1/0001"))
                EndDate = DateTime.Parse(ItemEndDate);

            var mListe = (new VenteLot()).fnSelectListTransfer(Crop, StartDate, EndDate, status, siteID, MagasinExpeditionID, MagasinReceptionID, ExportateurID, _IndTransit);

            string filterHeaders = this.Request.Params["filterheader"];
            return this.Store(mListe);
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

                var mChaine = string.Format("Livraison(s) : {0} <br/> Total Sacs estimé : {1} <br/> Total tonnage estimé : {2} (Kg)", NbrsOfRows.ToString("#,#"), SumSacsDeclares.ToString("#,#"), SumPoidsDeclare.ToString("#,#"));
                X.GetCmp<Label>("lblResume").Html = mChaine;

            }
            catch (Exception)
            {

                throw;
            }
            Session["mListeLivraion"] = "";
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemStatus, string ItemStartDate, string ItemEndDate, string ItemExportateur, string ItemSite, string ItemMagasinExpedition, string ItemMagasinReception, string ItemTransit)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemStartDate, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemEndDate, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeVenteList");

                //mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemCampagne"   ,ItemCampagne),
                                new Ext.Net.Parameter("ItemStartDate"           ,ItemStartDate),
                                new Ext.Net.Parameter("ItemEndDate"           ,ItemEndDate),
                                new Ext.Net.Parameter("ItemStatus"           ,ItemStatus),
                                new Ext.Net.Parameter("ItemSite"           ,ItemSite),
                                new Ext.Net.Parameter("ItemMagasinExpedition"           ,ItemMagasinExpedition),
                                new Ext.Net.Parameter("ItemMagasinReception"           ,ItemMagasinReception),
                                new Ext.Net.Parameter("ItemTransit"           ,ItemTransit)
                            });

                // collapse criterias areas
                //X.GetCmp<FormPanel>("CriteriaPanel").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Vente De Lot : Validation",
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

            title += " Fournisseur = " + X.GetCmp<ComboBox>("cmbFournisseurFD").SelectedItem.Text;

            title += ", Du " + X.GetCmp<DateField>("dtpStartDateFD").RawText.ToString() + " Au " + X.GetCmp<DateField>("dtpEndDateFD").RawText.ToString();

            X.GetCmp<FormPanel>("CriteriaPanelFD").Title = title;

            // collapse criterias areas
            X.GetCmp<FormPanel>("CriteriaPanelFD").Collapse(Direction.Top, false);

            return this.Direct();
        }

        public ActionResult LoadListOfAvailableDeliveries(string ItemSite, string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd)
        {
            int? siteID = ItemSite == "" ? (int?)null : int.Parse(ItemSite);
            int? fournisseurID = ItemFournisseur == "" ? -1 : int.Parse(ItemFournisseur);
            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            var mListe = (new BonDeLivraison()).fnSelectForTransfertFeves((int)siteID, (int)fournisseurID, StartDate, EndDate);
            return this.Store(mListe);
        }

        public ActionResult LoadListOfAvailableLot(string ItemSite, string ItemCampagne, string ItemPeriodStart, string ItemPeriodEnd)
        {
            int? siteID = ItemSite == "" ? (int?)null : int.Parse(ItemSite);
            Parametres para = new Parametres(0);
            string defaultCrop = para.Campagne;

            string Crop = defaultCrop;
            if (!String.IsNullOrEmpty(ItemCampagne))
                Crop = ItemCampagne;

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            var mListe = (new LotCoop()).fnSelectForVente(Crop, StartDate, EndDate, (int)siteID);
            return this.Store(mListe);
        }


        public ActionResult OnFilter()
        {
            X.GetCmp<FormPanel>("CriteriaPanel").ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnDisplayVenteList(string ItemStartDate = "", string ItemEndDate = "")
        {
            string UserName = (string)Session["userName"];

            if (string.IsNullOrEmpty(ItemStartDate)) ItemStartDate = DateTime.Now.ToShortDateString();
            if (string.IsNullOrEmpty(ItemEndDate)) ItemEndDate = DateTime.Now.ToShortDateString();
            DateTime datedebut = DateTime.ParseExact(ItemStartDate, "d", CultureInfo.CurrentUICulture);
            DateTime datedfin = DateTime.ParseExact(ItemEndDate, "d", CultureInfo.CurrentUICulture);
            ViewData["datedebut"] = datedebut;
            ViewData["datefin"] = datedfin;

            Site mSiteParDefaut = new Site();
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            Parametres mParam = new Parametres(0);

            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;
            if (mParam.Site == mSiteParDefaut.ID) ViewData["UrlSite"] = "LoadSiteAll";
            else ViewData["UrlSite"] = "LoadSiteByAccess";

            ViewData["Titre"] = "Imprimer Liste Des Ventes";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormListVenteCritere", ViewData = ViewData };
        }


        public ActionResult PrintList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;

                report = new rptVenteLotList() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                DateTime dateDebut = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetStartDate").RawText, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetEndDate").RawText, "d", CultureInfo.CurrentUICulture);

                report.Parameters["paramSite"].Value = GetFormValue("cmbDetSite");
                report.Parameters["campagne"].Value = GetFormValue("cmbDetCropYear");

                report.Parameters["paramSite"].Value = GetFormValue("cmbDetSite");
                report.Parameters["paramSiteText"].Value = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text;

                //report.Parameters["paramFournisseur"].Value = GetFormValue("cmbDetFournisseur");
                //report.Parameters["paramFournisseurText"].Value = X.GetCmp<ComboBox>("cmbDetFournisseur").SelectedItem.Text;

                report.Parameters["paramStatut"].Value = GetFormValue("cmbDetStatus");
                report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("cmbDetStatus").SelectedItem.Text;

                report.Parameters["paramDateDebut"].Value = dateDebut;
                report.Parameters["paramDateFin"].Value = dateFin;

                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/VenteLot/ViewList', this, 'Vente de Lots',''),App.FormListVenteCritere.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Transfert : Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
        }

        public ActionResult PrintSituationLotTransit()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;

                report = new rptSituationLotTransit() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                DateTime dateDebut = DateTime.ParseExact(X.GetCmp<DateField>("f_dtfDateDebutLivraison").RawText, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(X.GetCmp<DateField>("f_dtfDateFinLivraison").RawText, "d", CultureInfo.CurrentUICulture);

                report.Parameters["paramSite"].Value = GetFormValue("f_cmbSite");
                report.Parameters["CampagneID"].Value = GetFormValue("f_cmbCampagne");

                report.Parameters["paramSite"].Value = GetFormValue("f_cmbSite");
                report.Parameters["paramSiteText"].Value = X.GetCmp<ComboBox>("f_cmbSite").SelectedItem.Text;

                report.Parameters["paramExportateur"].Value = GetFormValue("f_cmbExportateur");
                report.Parameters["paramExportateurText"].Value = X.GetCmp<ComboBox>("f_cmbExportateur").SelectedItem.Text;

                report.Parameters["paramMagExpedition"].Value = GetFormValue("f_cmbMagasinExpedition");
                report.Parameters["paramMagExpeditionText"].Value = X.GetCmp<ComboBox>("f_cmbMagasinExpedition").SelectedItem.Text;

                report.Parameters["paramDateDebut"].Value = dateDebut;
                report.Parameters["paramDateFin"].Value = dateFin;

                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/TransfertStock/ViewList', this, 'Situation Des Lots en Transit',''), App.FormCritSitationLot.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Transfert : Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
        }

        public ActionResult PrintSituationLotEcart()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;

                report = new rptSituationLotEcart() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                DateTime dateDebut = DateTime.ParseExact(X.GetCmp<DateField>("f_dtfDateDebutLivraison").RawText, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(X.GetCmp<DateField>("f_dtfDateFinLivraison").RawText, "d", CultureInfo.CurrentUICulture);

                report.Parameters["paramSite"].Value = GetFormValue("f_cmbSite");
                report.Parameters["CampagneID"].Value = GetFormValue("f_cmbCampagne");

                report.Parameters["paramSite"].Value = GetFormValue("f_cmbSite");
                report.Parameters["paramSiteText"].Value = X.GetCmp<ComboBox>("f_cmbSite").SelectedItem.Text;

                report.Parameters["paramExportateur"].Value = GetFormValue("f_cmbExportateur");
                report.Parameters["paramExportateurText"].Value = X.GetCmp<ComboBox>("f_cmbExportateur").SelectedItem.Text;

                report.Parameters["paramMagExpedition"].Value = GetFormValue("f_cmbMagasinExpedition");
                report.Parameters["paramMagExpeditionText"].Value = X.GetCmp<ComboBox>("f_cmbMagasinExpedition").SelectedItem.Text;

                report.Parameters["paramMagReception"].Value = GetFormValue("f_cmbMagasinReception");
                report.Parameters["paramMagReceptionText"].Value = X.GetCmp<ComboBox>("f_cmbMagasinReception").SelectedItem.Text;

                report.Parameters["paramDateDebut"].Value = dateDebut;
                report.Parameters["paramDateFin"].Value = dateFin;

                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/TransfertStock/ViewList', this, 'Analyse des écarts de transfert',''),App.FormCritSitationEcart.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Transfert : Validation",
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

        #region "Methods"
        private void CloseDetailWindow()
        {
            X.GetCmp<Window>("FormVenteLot").Close();
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

        private VenteLot MapFormToObject(VenteLot mClass)
        {
            mClass.Sites = new Site();
            //mClass.Sites.ID = int.Parse(GetFormValue("txtSiteID"));
            //mClass.Sites.Nom = X.GetCmp<TextField>("txtNomSite").Text;

            mClass.Sites.ID = int.Parse(GetFormValue("cmbSites"));
            mClass.Sites.Nom = X.GetCmp<ComboBox>("cmbSites").SelectedItem.Text;

            mClass.Exportateur = new Exportateur();
            mClass.Exportateur.ID = int.Parse(GetFormValue("cmbDetExportateur"));
            mClass.Exportateur.Nom = X.GetCmp<ComboBox>("cmbDetExportateur").SelectedItem.Text.ToString();

            mClass.Campagne = new Tms.Classes.Shared.Campagne();
            mClass.Campagne.Designation = X.GetCmp<ComboBox>("cmbDetCrop").SelectedItem.Text.ToString();
            mClass.DateVente = DateTime.Parse(X.GetCmp<DateField>("txtDateVente").RawText.ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second).AddMilliseconds(DateTime.Now.Millisecond);
            mClass.DateExpedition = DateTime.Parse(X.GetCmp<DateField>("txtDateExpedition").RawText.ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second).AddMilliseconds(DateTime.Now.Millisecond);
            mClass.NumCCC = X.GetCmp<TextField>("txtNumeroCCC").Text;
            if (X.GetCmp<TextField>("txtNombreSacs").Text != string.Empty) mClass.NombreSacs = int.Parse(X.GetCmp<TextField>("txtNombreSacs").Text);
            if (X.GetCmp<TextField>("txtPoidsBrut").Text != string.Empty) mClass.PoidsBrut = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrut").Text);
            if (X.GetCmp<TextField>("txtNetWeight").Text != string.Empty) mClass.PoidsNet = decimal.Parse(X.GetCmp<TextField>("txtNetWeight").Text);

            if (X.GetCmp<TextField>("txtPrixMoyen").Text != string.Empty) mClass.PrixMoyen = decimal.Parse(X.GetCmp<TextField>("txtPrixMoyen").Text);
            mClass.Immatriculation = X.GetCmp<TextField>("txtImmatriculation").Text;

            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }

        private VenteLot MapFormToObjectRecept(VenteLot mClass)
        {
            //mClass.Destination = new Destination();
            //mClass.Destination.ID = int.Parse(GetFormValue("cmbDetDestination"));
            //mClass.Destination.Nom = X.GetCmp<ComboBox>("cmbDetDestination").SelectedItem.Text.ToString();

            //mClass.Magasin = new Magasin();
            //mClass.Magasin.ID = int.Parse(GetFormValue("cmbMagasin"));
            //mClass.Magasin.Designation = X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Text.ToString();
            //blo
            //mClass.MagasinExpedition = new Magasin();
            //mClass.MagasinExpedition.ID = int.Parse(GetFormValue("cmbMagasin"));
            //mClass.MagasinExpedition.Designation = X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Text.ToString();
            //mClass.Magasin = new Magasin();
            //mClass.Magasin.ID = int.Parse(GetFormValue("cmbMagasin"));
            //mClass.Magasin.Designation = X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Text.ToString();
            mClass.Lot = new LotCoop();
            mClass.Lot.ID = Guid.Parse(GetFormValue("cmbLot"));
            mClass.Lot.Numero = X.GetCmp<ComboBox>("cmbLot").SelectedItem.Text.ToString();

            Lot_GestionStock mLot = new Lot_GestionStock();
            bool res = mLot.fnGet(mClass.Lot.ID);
            mClass.Exportateur = new Exportateur();
            mClass.Exportateur.ID = mLot.Exportateur.ID;
            mClass.Exportateur.Nom = mLot.Exportateur.Nom;

            mClass.MagasinReception = new Magasin();
            mClass.MagasinReception.ID = int.Parse(GetFormValue("cmbMagasin"));
            mClass.MagasinReception.Designation = X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Text;

            //mClass.MagasinExpedition = new Magasin();
            //mClass.MagasinExpedition.ID = mLot.Magasin.ID;
            //mClass.MagasinExpedition.Designation = mLot.Magasin.Designation;

            if (X.GetCmp<TextField>("txtNombrePalette").Text != string.Empty) mClass.NombrePaletteReception = int.Parse(X.GetCmp<TextField>("txtNombrePalette").Text);
            if (X.GetCmp<TextField>("txtNombreSacs").Text != string.Empty) mClass.NombreSacs = int.Parse(X.GetCmp<TextField>("txtNombreSacs").Text);
            if (X.GetCmp<TextField>("txtTareSac").Text != string.Empty) mClass.TareSacsArrive = decimal.Parse(X.GetCmp<TextField>("txtTareSac").Text);
            if (X.GetCmp<TextField>("txtPoidsBrut").Text != string.Empty) mClass.PoidsBrutRecetpion = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrut").Text);
            if (X.GetCmp<TextField>("txtNetWeight").Text != string.Empty) mClass.PoidsNetRecetpion = decimal.Parse(X.GetCmp<TextField>("txtNetWeight").Text);
            if (X.GetCmp<TextField>("txtTarePalette").Text != string.Empty) mClass.TarePaletteArrive = decimal.Parse(X.GetCmp<TextField>("txtTarePalette").Text);

            if (X.GetCmp<TextField>("txtNumBordereauEntree").Text != string.Empty) mClass.NumBordereauReception = X.GetCmp<TextField>("txtNumBordereauEntree").Text;
            mClass.ImmTracteurReception = X.GetCmp<TextField>("txtImmTracteur").Text;
            mClass.ImmRemorqueReception = X.GetCmp<TextField>("txtImmRemorque").Text;

            mClass.DateReception = DateTime.Parse(X.GetCmp<DateField>("txtDateReception").RawText.ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second).AddMilliseconds(DateTime.Now.Millisecond);
            //if (X.GetCmp<TextField>("txtPoidsBrutExportateur").Text != string.Empty) mClass.PoidsBrutExportateur = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrutExportateur").Text);
            ////if (X.GetCmp<TextField>("txtRefraction").Text != string.Empty) mClass.Refaction = decimal.Parse(X.GetCmp<TextField>("txtRefraction").Text);
            //if (X.GetCmp<TextField>("txtNetWeighRecu").Text != string.Empty) mClass.PoidsNetArrive = decimal.Parse(X.GetCmp<TextField>("txtNetWeighRecu").Text);
            ////if (X.GetCmp<TextField>("txtFreinte").Text != string.Empty) mClass.Freinte = decimal.Parse(X.GetCmp<TextField>("txtFreinte").Text);
            //if (X.GetCmp<TextField>("txtTareSacExportateur").Text != string.Empty) mClass.TareSacsArrive = decimal.Parse(X.GetCmp<TextField>("txtTareSacExportateur").Text);
            ////if (X.GetCmp<NumberField>("txtPrixExportateur").Text != string.Empty) mClass.PrixExportateur = decimal.Parse(X.GetCmp<NumberField>("txtPrixExportateur").Text);
            //if (X.GetCmp<TextField>("txtPoidsProduitAccepte").Text != string.Empty) mClass.PoidsProduitAccepte = decimal.Parse(X.GetCmp<TextField>("txtPoidsProduitAccepte").Text);
            //if (X.GetCmp<TextField>("txtTarePaletteExportateur").Text != string.Empty) mClass.TarePaletteArrive = decimal.Parse(X.GetCmp<TextField>("txtTarePaletteExportateur").Text);

            mClass.CommentaireReception = X.GetCmp<TextField>("TxtAreaCommentaire").Text;

            //if (X.GetCmp<TextField>("txtSacExportateur").Text != string.Empty) mClass.SacExportateur = int.Parse(X.GetCmp<TextField>("txtSacExportateur").Text);
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }

        //private VenteLot MapFormToObjectRecept(VenteLot mClass)
        //{
        //    mClass.Destination = new Destination();
        //    mClass.Destination.ID = int.Parse(GetFormValue("cmbDetDestination"));
        //    mClass.Destination.Nom = X.GetCmp<ComboBox>("cmbDetDestination").SelectedItem.Text.ToString();

        //    mClass.DateReception = DateTime.Parse(X.GetCmp<DateField>("txtDateReception").RawText.ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second).AddMilliseconds(DateTime.Now.Millisecond);
        //    if (X.GetCmp<TextField>("txtPoidsBrutExportateur").Text != string.Empty) mClass.PoidsBrutExportateur = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrutExportateur").Text);
        //    if (X.GetCmp<TextField>("txtRefraction").Text != string.Empty) mClass.Refaction = decimal.Parse(X.GetCmp<TextField>("txtRefraction").Text);
        //    if (X.GetCmp<TextField>("txtNetWeighRecu").Text != string.Empty) mClass.PoidsNetArrive = decimal.Parse(X.GetCmp<TextField>("txtNetWeighRecu").Text);
        //    if (X.GetCmp<TextField>("txtFreinte").Text != string.Empty) mClass.Freinte = decimal.Parse(X.GetCmp<TextField>("txtFreinte").Text);
        //    if (X.GetCmp<TextField>("txtTareSacExportateur").Text != string.Empty) mClass.TareSacsArrive = decimal.Parse(X.GetCmp<TextField>("txtTareSacExportateur").Text);
        //    if (X.GetCmp<NumberField>("txtPrixExportateur").Text != string.Empty) mClass.PrixExportateur = decimal.Parse(X.GetCmp<NumberField>("txtPrixExportateur").Text);
        //    if (X.GetCmp<TextField>("txtPoidsProduitAccepte").Text != string.Empty) mClass.PoidsProduitAccepte = decimal.Parse(X.GetCmp<TextField>("txtPoidsProduitAccepte").Text);
        //    if (X.GetCmp<TextField>("txtTarePaletteExportateur").Text != string.Empty) mClass.TarePaletteArrive = decimal.Parse(X.GetCmp<TextField>("txtTarePaletteExportateur").Text);

        //    if (X.GetCmp<TextField>("txtSacExportateur").Text != string.Empty) mClass.SacExportateur = int.Parse(X.GetCmp<TextField>("txtSacExportateur").Text);
        //    mClass.UtilisateurCreation = (string)Session["userName"];
        //    mClass.UtilisateurModification = (string)Session["userName"];

        //    return mClass;
        //}

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

        public ActionResult OnPrintDeliveriesList(string exportateur, string cropYear, string deliveryType, string supplier, string startDate, string endDate, string supplierName, string deliveryTypeDesignation, string exportateurNom = "")
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                Session["paramCropYear"] = cropYear;
                Session["paramExportateur"] = exportateur;
                Session["paramDeliveryType"] = Int32.Parse(deliveryType);
                Session["paramDeliveryTypeText"] = deliveryTypeDesignation;
                Session["paramExportateurNom"] = exportateurNom;

                Session["paramSupplier"] = Int32.Parse(supplier);
                Session["paramSupplierText"] = supplierName;

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Livraison/ViewDeliveriesList', this, 'List des Livraisons',''),App.frmDeliveryList.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Livraison : Validation",
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

        public ActionResult SubmitLot(string ItemSelected, string transfertID = "")
        {
            try
            {

                List<LotCoop> mLot = new List<LotCoop>();
                List<VenteLotDetail> mClass = new List<VenteLotDetail>();
                VenteLotDetail mDetail = new VenteLotDetail();
                mLot = JSON.Deserialize<List<LotCoop>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
                mLot = CalculateValuesByNbrOfBags(mLot);

                foreach (LotCoop item in mLot)
                {
                    mDetail = new VenteLotDetail();
                    mDetail.LotCoop = new LotCoop();
                    mDetail.LotCoop = item;
                    mDetail.ID = item.ID;
                    mDetail.NombreSacs = item.NombreSacs;
                    mDetail.PoidsBrut = item.PoidsBrut;
                    mDetail.PoidsNet = item.PoidsNet;
                    mDetail.NumeroLot = item.Numero;
                    mDetail.IsNew = true;
                    mDetail.IsNewIsList = true;
                    mClass.Add(mDetail);
                }
                //mLivraison = CalculateValuesByNbrOfBags(mLivraison);
                Store store = X.GetCmp<Store>("storeListeLotVt");

                store.Add(mClass);

                X.GetCmp<ComboBox>("cmbSites").ReadOnly = true;
                X.GetCmp<Window>("FormVente_SelectLot").Close();
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Transfert - Erreur",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR
                });
            }
            return this.Direct();
        }

        private List<LotCoop> CalculateValuesByNbrOfBags(List<LotCoop> clLot)
        {
            foreach (var item in clLot)
            {
                if (item.NombreSacs != item.NbreSacsAccepteInitial)
                {
                    item.PoidsBrut = Math.Round((item.NombreSacs * item.PoidsBrut) / item.NbreSacsAccepteInitial);
                    item.TareSacs = Math.Round((item.NombreSacs * item.TareSacs) / item.NbreSacsAccepteInitial);
                    item.TarePalette = Math.Round((item.NombreSacs * item.TarePalette) / item.NbreSacsAccepteInitial);
                    item.PoidsNet = Math.Round((item.NombreSacs * item.PoidsNet) / item.NbreSacsAccepteInitial);
                }
            }
            return clLot;
        }

        public ActionResult OnSelectLot(int? siteID, string venteID = "")
        {
            try
            {
                Guid IdVente;
                bool parseResult = false;
                if (!siteID.HasValue)
                    throw new Exception("");
                if (string.IsNullOrEmpty(venteID))
                    throw new Exception("Erreur");
                else
                {
                    parseResult = Guid.TryParse(venteID, out IdVente);
                }

                VenteLotDetail mModel = new VenteLotDetail();
                mModel.VenteLot = new VenteLot();
                mModel.VenteLot.ID = IdVente;
                mModel.VenteLot.Sites = new Site();
                mModel.VenteLot.Sites.ID = (int)siteID;
                Parametres mParam = new Parametres(0);
                mModel.VenteLot.Campagne = new Campagne();
                mModel.VenteLot.Campagne.Designation = mParam.Campagne;

                return new Ext.Net.MVC.PartialViewResult { ViewName = "FormVente_SelectLot", Model = mModel };

            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Transfert : Erreur",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }

        }

        public ActionResult SubmitFormBeforeDeliveries()
        {
            try
            {
                Guid transfertID = Guid.Empty;
                DateTime dateTransfert = new DateTime();
                VenteLot mTransfert = new VenteLot();
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mTransfert.IsNew = true;
                else
                {
                    formExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                    mTransfert.IsNew = false;
                    bool isGuid = Guid.TryParse(GetFormValue("TxtTransfertID"), out transfertID);
                    if (isGuid)
                        mTransfert.fnGet(transfertID);
                    //else
                    //    mPesee.fnGet(WeighingID);

                    if (mTransfert == null || mTransfert.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Erreur, Veuillez réessayer");

                    dateTransfert = mTransfert.DateVente;
                }

                bool result = false;

                mTransfert = MapFormToObject(mTransfert);
                if (mTransfert.IsNew == false && (dateTransfert.Date == mTransfert.DateVente.Date))
                    mTransfert.DateVente = dateTransfert;

                result = mTransfert.fnUpdate();

                //if (WeighingIsCreated)
                //    WeighingID = mPesee.ID;

                if (result)
                {
                    //WeighingIsCreated = true;
                    //WeighingID = mPesee.ID;

                    Store mstore = X.GetCmp<Store>("storeVenteList");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mTransfert);
                        X.GetCmp<RowSelectionModel>("rowSelectionListe").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mTransfert.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mTransfert);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }
                    X.GetCmp<Container>("zonePalettes").Enable();
                    X.GetCmp<Container>("btnAddNewLivraison").Enable();
                    X.GetCmp<Hidden>("TxtTransfertID").SetValue(mTransfert.ID);
                    X.GetCmp<Hidden>("hiddenExecMode").SetValue("Update");
                    return this.Direct();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Transfert : Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        public ActionResult SelectLots(string venteID)
        {
            try
            {
                Guid IDVente;
                bool result = Guid.TryParse(venteID, out IDVente);
                VenteLotDetail mDetails = new VenteLotDetail();
                var Liste = mDetails.fnSelect(IDVente);
                return this.Store(Liste);
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Transfert : Validation",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Store(null);
            }

        }

        public ActionResult OnRemoveDelivery(string ItemSelected)
        {
            try
            {
                VenteLotDetail mDetail = JSON.Deserialize<VenteLotDetail>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                if (mDetail.ID != Guid.Empty)
                {
                    Store mstore = X.GetCmp<Store>("storeListeBL");

                    if (mDetail.IsNew)
                    {
                        ModelProxy _proxy = mstore.GetById(mDetail.ID);
                        _proxy.Drop();
                        return this.Direct();
                    }
                    else
                    {
                        mDetail.fnGet(mDetail.ID);
                        if (mDetail == null || mDetail.ID == Guid.Empty)
                            throw new Exception("RemoveDelivery : Ligne introuvable, veuillez réessayer svp !");

                        mDetail.UtilisateurModification = (string)Session["userName"];

                        bool result = mDetail.fnRemove();

                        if (result)
                        {
                            ModelProxy mProxy = mstore.GetById(mDetail.ID);
                            mProxy.Drop();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Transfert - Retirer",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnRemoveLot(string ItemSelected)
        {
            try
            {
                VenteLotDetail mDetail = JSON.Deserialize<VenteLotDetail>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                if (mDetail.ID != Guid.Empty)
                {
                    Store mstore = X.GetCmp<Store>("storeListeLotVt");

                    if (mDetail.IsNew)
                    {
                        ModelProxy _proxy = mstore.GetById(mDetail.ID);
                        _proxy.Drop();
                        return this.Direct();
                    }
                    else
                    {
                        mDetail.fnGet(mDetail.ID);
                        if (mDetail == null || mDetail.ID == Guid.Empty)
                            throw new Exception("RemoveDelivery : Ligne introuvable, veuillez réessayer svp !");

                        mDetail.UtilisateurModification = (string)Session["userName"];

                        bool result = mDetail.fnRemove();

                        if (result)
                        {
                            ModelProxy mProxy = mstore.GetById(mDetail.ID);
                            mProxy.Drop();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Transfert - Retirer",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult OnPrintTransfert(string IdVente, string IsCopy = "")
        {
            //string BaseUrl = "";
            bool ReportIsCopy = false;

            if (string.IsNullOrEmpty(IsCopy))
                ReportIsCopy = true;

            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'VenteLot{0}', '{1}/VenteLot/ViewReport?id={0}&IsCopy={2}', this, 'Fiche De Vente','')", IdVente, BaseUrl, ReportIsCopy));
        }

        public ActionResult ViewReport(string id, bool IsCopy)
        {
            rptFicheVenteLot report = new rptFicheVenteLot();

            report.DataSource = DevExpressReportDs.SetDataSource(report);
            report.Parameters["paramID"].Value = id;
            //report.Parameters["IsCopy"].Value = IsCopy;

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

        [HttpPost]
        public ActionResult UpdateFormMethod(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                VenteLot mVente = new VenteLot();
                VenteLotDetail mDetail;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mVente.IsNew = true;
                else
                {
                    mVente.IsNew = false;

                    mVente.fnGet(Guid.Parse(GetFormValue("txtVenteID")));

                    if (mVente == null || mVente.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Ligne introuvable, veuillez réessayer svp !");
                }

                bool result = false;
                bool resultDetail = true;
                mVente = MapFormToObject(mVente);

                _db = mVente.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = mVente.fnUpdate(mtran);

                if (result)
                {
                    resultDetail = true;
                    List<VenteLotDetail> det = JSON.Deserialize<List<VenteLotDetail>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (det.Count > 0)
                    {

                        for (int i = 0; i < det.Count(); i++)
                        {
                            mDetail = new VenteLotDetail();
                            mDetail.VenteLot = new VenteLot();
                            mDetail.LotCoop = new LotCoop();

                            mDetail.SetDataSource(_db);

                            mDetail.VenteLot.ID = mVente.ID;
                            mDetail.LotCoop.ID = det[i].LotCoop.ID;
                            mDetail.LotCoop.Numero = det[i].LotCoop.Numero;
                            mDetail.PoidsBrut = det[i].LotCoop.PoidsBrut;
                            mDetail.PoidsNet = det[i].LotCoop.PoidsNet;
                            mDetail.NombreSacs = det[i].LotCoop.NombreSacs;
                            mDetail.UtilisateurCreation = (string)Session["userName"];
                            mDetail.UtilisateurModification = (string)Session["userName"];
                            mDetail.IsNew = det[i].IsNew;
                            if (mDetail.IsNew)
                                resultDetail = mDetail.fnUpdate(mtran);

                            if (!resultDetail)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        //_db.RollBackTransaction(mtran);
                        X.MessageBox.Show(new MessageBoxConfig
                        {
                            Title = "Transfert : Validation",
                            Message = "Veuillez ajouter au moins un lot à la vente !",
                            Buttons = MessageBox.Button.OK,
                            Icon = MessageBox.Icon.WARNING
                        });
                    }
                    if (!resultDetail)
                    {
                        _db.RollBackTransaction(mtran);
                    }

                }

                if (result && resultDetail)
                {
                    _db.CommitTransaction(mtran);
                    Store mstore = X.GetCmp<Store>("storeVenteList");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mVente);
                        X.GetCmp<RowSelectionModel>("rowSelectionListe").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mVente.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mVente);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormVenteLot").Close();
                }
            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mtran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Transfert : Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SetAvailableNbrSacs(string ItemListeAdded, string ItemListeToAdd)
        {
            if (!string.IsNullOrEmpty(ItemListeAdded) && ItemListeAdded != "[]" && !string.IsNullOrEmpty(ItemListeToAdd) && ItemListeToAdd != "[]")
            {
                List<VenteLotDetail> oldListe = JSON.Deserialize<List<VenteLotDetail>>(ItemListeAdded, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                List<LotCoop> newListe = JSON.Deserialize<List<LotCoop>>(ItemListeToAdd, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                //HashSet<Guid> LivraisonId = new HashSet<Guid>(mLivraison.Select(l => l.BonDeLivraison.Livraison.ID));
                int nbr = 0;
                foreach (LotCoop itemNew in newListe)
                {
                    nbr = 0;
                    foreach (VenteLotDetail itemOld in oldListe.Where(x => x.LotCoop.ID == itemNew.ID))
                    {
                        nbr += itemOld.NombreSacs;
                        //if ((itemNew.BonDeLivraison.Livraison.ID == itemOld.BonDeLivraison.Livraison.ID))
                        //{

                        //    itemNew.NombreSacs = itemNew.NombreSacs - itemOld.NombreSacs;
                        //}
                    }
                    if (itemNew.NbreSacsAccepteInitial != (nbr + itemNew.NombreSacs))
                    {
                        itemNew.NombreSacs = itemNew.NbreSacsAccepteInitial - nbr;
                    }

                }

                Store mStoreMelange = X.GetCmp<Store>("storeListeLot");
                mStoreMelange.RemoveAll();
                mStoreMelange.Add(newListe.Where(n => n.NombreSacs > 0));
            }

            return this.Direct();
        }

        public ActionResult OnPrintVentreTraca(string IdVente)
        {
            //string BaseUrl = "";
            bool ReportIsCopy = false;

            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'VenteLot{0}', '{1}/VenteLot/ViewReportTraca?id={0}', this, 'Fiche De Vente - Traca','')", IdVente, BaseUrl, ReportIsCopy));
        }

        public ActionResult ViewReportTraca(string id)
        {
            rptFicheVenteLotTraca report = new rptFicheVenteLotTraca();

            report.DataSource = DevExpressReportDs.SetDataSource(report);
            report.Parameters["paramID"].Value = id;

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }
    }
}