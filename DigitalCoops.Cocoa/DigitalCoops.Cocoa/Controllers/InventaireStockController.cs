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
using Tms.Classes.Business;
using Tms.Classes.Business.stock;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class InventaireStockController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";
        // GET: InventaireStock
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);
            X.GetCmp<ComboBox>("cmbFiltreMagasin").SetValue(mParam.MagasinTV);
            X.GetCmp<ComboBox>("cmbFiltreCampagne").SetValue(mParam.Campagne);

            #region Set Function's Access            
            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            Magasin mMagasin = new Magasin();
            result = mMagasin.fnGetDefaultBySite(mSiteParDefaut.ID);
            ViewBag.MagasinParDefaut = mMagasin.ID;

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{C9A25A05-7EFC-4CC7-BE55-53B623B86D1A}"), UserName);
            List<Fonction> mlisteFonctions = new List<Fonction>();
            mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{B55BC59B-C5CD-4051-A496-EA24EC6D6596}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{48B491AD-DC9E-4172-9362-E6101ACB2FF7}")))
                X.GetCmp<MenuItem>("mnuPrintList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{6B285C33-6108-47D8-BED9-0C693DD4F454}")))
                X.GetCmp<MenuItem>("mnuExport").Enable();
            else
                X.GetCmp<MenuItem>("mnuExport").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{CE8C0FCB-EBF0-400B-AA04-495AACCEB473}")))
                X.GetCmp<MenuItem>("mnuPrintStockStatus").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintStockStatus").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{F9FAA722-020D-4FA9-86F6-2B038F8984F1}")))
                X.GetCmp<Hidden>("ivhiddenPermAjust").SetValue(true);
            else
                X.GetCmp<Hidden>("ivhiddenPermAjust").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{8C13ACFA-AF8E-49EB-80E1-48F11C780728}")))
                X.GetCmp<Hidden>("ivhiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("ivhiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{E574FFAA-B7AC-47A1-BF8C-19A68B7E263B}")))
                X.GetCmp<Hidden>("ivhiddenPermApprove").SetValue(true);
            else
                X.GetCmp<Hidden>("ivhiddenPermApprove").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{A51D6130-557C-4B25-A028-88E5E6A5B985}")))
                X.GetCmp<Hidden>("ivhiddenPermPrintInventory").SetValue(true);
            else
                X.GetCmp<Hidden>("ivhiddenPermPrintInventory").SetValue(false);
          
            #endregion

            return View();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("inventaireCP");
            mform.ToggleCollapse();
            //mform.Collapsed = false;
            return this.Direct();
        }

        public ActionResult SelectItemsInventaire(StoreRequestParameters parameters, string inventaireID = "")
        {
            Guid IdInventaire = Guid.Parse(inventaireID);
            var mListe = (new InventaireElementStock()).fnSelectItemInStockByID(IdInventaire);
            return this.Store(mListe);
        }

        public ActionResult SelectItemsWithShrink(StoreRequestParameters parameters, string inventaireID = "")
        {
            Guid IdInventaire = Guid.Parse(inventaireID);
            var mListe = (new InventaireElementStock()).fnSelectItemInStockByID(IdInventaire);
            List<InventaireElementStock> inventaire = new List<InventaireElementStock>();
            foreach (InventaireElementStock item in mListe)
            {
                inventaire.Add(item);
            }
            return this.Store(inventaire.Where(x => x.Quantite != x.QuantitePhysique));
        }

        public ActionResult SelectMouvementsInStock(StoreRequestParameters parameters, string ItemCampagne = "", string ItemMagasin = "", string ItemDateInventaire = "", string inventaireID = "")
        {
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
            int MagasinID = GetCriteriaValue(ItemMagasin);
            DateTime? DateInventaire = string.IsNullOrEmpty(ItemDateInventaire) ? (DateTime?)null : DateTime.Parse(ItemDateInventaire.ToString());

            var mListe = (new InventaireElementStock()).fnSelectItemInStock(Campagne,MagasinID, DateInventaire);
            return this.Store(mListe);
        }

        public ActionResult OnRefreshMouvementsInStock(string ItemMagasin, string ItemDateInventaire, string ItemCampagne)
        {
            try
            {
                DateTime datedfin = DateTime.ParseExact(ItemDateInventaire, "d", CultureInfo.CurrentUICulture);

                if (!string.IsNullOrEmpty(ItemMagasin) && !string.IsNullOrEmpty(ItemDateInventaire) && !string.IsNullOrEmpty(ItemCampagne))
                {
                    DateTime dateinventaire = DateTime.ParseExact(ItemDateInventaire, "d", CultureInfo.CurrentUICulture);
                    Store mstore = X.GetCmp<Store>("storeListeItemInStock");

                    mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemMagasin"   ,ItemMagasin),
                                new Ext.Net.Parameter("ItemDateInventaire" ,ItemDateInventaire),
                                new Ext.Net.Parameter("ItemCampagne" ,ItemCampagne)
                            });
                }
                
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Inventaire : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });

            }
            return this.Direct();
        }

        public ActionResult onAdd()
        {
            InventaireViewModel mclass = new InventaireViewModel();
            
            Site mSiteParDefaut = new Site();
            string UserName = (string)Session["userName"];
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            mclass._Inventaire = new Inventaire();
            mclass._Inventaire.Sites = new Site();
            mclass._Inventaire.Sites.ID = mSiteParDefaut.ID;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormInventaireStock", Model = mclass};

        }

        public ActionResult onEdit(string ItemSelected)
        {
            InventaireViewModel mclass = new InventaireViewModel();
            Inventaire inventaire = JSON.Deserialize<Inventaire>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            mclass._Inventaire = inventaire;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormInventaireStock", Model = mclass };

        }

        public ActionResult onApprove(string ItemSelected)
        {
            
            InventaireViewModel mclass = new InventaireViewModel();
            Inventaire inventaire = JSON.Deserialize<Inventaire>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._Inventaire = inventaire;            
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAjustementStock", Model = mclass };

        }

        public ActionResult onAjust(string ItemSelected)
        {

            InventaireViewModel mclass = new InventaireViewModel();
            Inventaire inventaire = JSON.Deserialize<Inventaire>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._Inventaire = inventaire;

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAjustementStock", Model = mclass};

        }

        public ActionResult onUpdateStock(string storerow)
        {

            InventaireElementStock mclass = new InventaireElementStock();
            mclass = JSON.Deserialize<InventaireElementStock>(storerow, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });                       

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormNewStock", Model = mclass };

        }

        public ActionResult onConsult(string ItemSelected)
        {

            InventaireViewModel mclass = new InventaireViewModel();
            Inventaire inventaire = JSON.Deserialize<Inventaire>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._Inventaire = inventaire;

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormInventaireStock", Model = mclass, };

        }

        public ActionResult Select(StoreRequestParameters parameters,string ItemMagasin, string ItemPeriode, string ItemCampagne, string ItemStatut, string ItemSite)
        {
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
            int MagasinID = GetCriteriaValue(ItemMagasin);
            int PeriodeID = GetCriteriaValue(ItemPeriode);
            int SiteID = GetCriteriaValue(ItemSite);
            ItemStatut = string.IsNullOrEmpty(ItemStatut) ? "-1" : ItemStatut;
            var mListe = (new Inventaire()).fnSelect(MagasinID, PeriodeID, Campagne, ItemStatut, SiteID);

            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemMagasin, string ItemPeriode, string ItemCampagne, string ItemStatut)
        {
            try
            {                
                Store mstore = X.GetCmp<Store>("storeListeInventaire");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {                                    
                                    new Ext.Net.Parameter("ItemMagasin",ItemMagasin),
                                    new Ext.Net.Parameter("ItemPeriode",ItemPeriode),
                                    new Ext.Net.Parameter("ItemCampagne",ItemCampagne),
                                    new Ext.Net.Parameter("ItemStatut",ItemStatut)
                                });

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stock Status Item : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult SubmitFormMethod(string storerows)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                Inventaire inventaire = new Inventaire();
                InventaireElementStock ItemEnStock = new InventaireElementStock();

                bool result = true;
                bool resulttransact = true;

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    inventaire.IsNew = true;
                else
                {
                    inventaire.IsNew = false;

                    inventaire.fnGet(int.Parse(GetFormValue("TxtinventaireID")));

                    if (inventaire == null || inventaire.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Inventaire load failed.");
                }

                _db = inventaire.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                inventaire = MapFormToObject(inventaire);
                result = inventaire.fnUpdate(mtran);

                if (result)
                {
                    List<InventaireElementStock> ItemsInventaire = JSON.Deserialize<List<InventaireElementStock>>(storerows, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (ItemsInventaire.Count > 0)
                    {
                        foreach (var item in ItemsInventaire)
                        {
                            ItemEnStock = new InventaireElementStock();
                            ItemEnStock.Inventaire = new Inventaire();
                            ItemEnStock.SacType = new SacType();
                            ItemEnStock.TypeElementStock = new TypeElementStock();
                            ItemEnStock.SetDataSource(_db);

                            //ItemEnStock.SacType.ID = item.SacType.ID;
                            //ItemEnStock.SacType.Designation = item.SacType.Designation;
                        
                            ItemEnStock.TypeElementStock.ID = item.TypeElementStock.ID;
                            ItemEnStock.Reference = item.Reference;

                            ItemEnStock.SacType = null;
                            
                            ItemEnStock.Inventaire.ID = inventaire.ID;
                            ItemEnStock.ObjetEnStockID = item.ObjetEnStockID;
                            ItemEnStock.Inventaire.Numero = inventaire.Numero;
                            ItemEnStock.Quantite = item.Quantite;
                            ItemEnStock.QuantitePhysique = item.Quantite;
                            ItemEnStock.PoidsBrut = item.PoidsBrut;
                            ItemEnStock.PoidsBrutPhysique = item.PoidsBrut;
                            ItemEnStock.TareSacs = item.TareSacs;
                            ItemEnStock.TarePalette = item.TarePalette;
                            ItemEnStock.PoidsNetLivre = item.PoidsNetLivre;
                            ItemEnStock.Refaction = item.Refaction;
                            ItemEnStock.PoidsNetAccepte = item.PoidsNetAccepte;
                            ItemEnStock.UtilisateurCreation = (string)Session["userName"];
                            ItemEnStock.UtilisateurModification = (string)Session["userName"];

                            resulttransact = ItemEnStock.fnUpdate(mtran);
                            if (!resulttransact) break;
                        }
                        if (!resulttransact) _db.RollBackTransaction(mtran);
                        _db.CommitTransaction(mtran);
                    }
                    else
                    {
                        _db.RollBackTransaction(mtran);
                        X.MessageBox.Show(new MessageBoxConfig
                        {
                            Title = "Inventaire : Data Validation",
                            Message = "No items Selected",
                            Buttons = MessageBox.Button.OK,
                            Icon = MessageBox.Icon.WARNING
                        });
                    }
                }
                if (resulttransact)
                {
                    Store mstore = X.GetCmp<Store>("storeListeInventaire");
                    mstore.Insert(0, inventaire);                    

                    X.GetCmp<Window>("FormInventaireStock").Close();
                }
            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mtran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Inventaire : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        private Inventaire MapFormToObject(Inventaire mClass)
        {
            Magasin magasin = new Magasin();
            magasin.ID = int.Parse(GetFormValue("cmbMagasinIV"));
            magasin.Designation = X.GetCmp<ComboBox>("cmbMagasinIV").SelectedItem.Text.ToString();
            mClass.Magasin = magasin;

            Periode periode = new Periode();
            periode.ID = int.Parse(GetFormValue("cmbPeriode"));
            periode.Designation = X.GetCmp<ComboBox>("cmbPeriode").SelectedItem.Text.ToString();
            mClass.Periode = periode;

            Site mSite = new Site();
            mSite.ID = int.Parse(GetFormValue("_cmbSite"));
            mSite.Nom = X.GetCmp<ComboBox>("_cmbSite").SelectedItem.Text.ToString();
            mClass.Sites = mSite;

            Campagne campagne = new Campagne();
            campagne.Designation = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text.ToString();
            mClass.Campagne = campagne;

            mClass.DateInventaire = DateTime.Parse(X.GetCmp<DateField>("txtDateInventaire").RawText.ToString());

            if (X.GetCmp<TextField>("txtQuantite").Text != string.Empty) mClass.Quantite = int.Parse(X.GetCmp<TextField>("txtQuantite").Text.Replace(" ",""));
            if (X.GetCmp<TextField>("txtPoidsBrut").Text != string.Empty) mClass.PoidsBrut = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrut").Text);
            if (X.GetCmp<TextField>("txtTareSacs").Text != string.Empty) mClass.TareSacs = decimal.Parse(X.GetCmp<TextField>("txtTareSacs").Text);
            if (X.GetCmp<TextField>("txtTarePalettes").Text != string.Empty) mClass.TarePalette = decimal.Parse(X.GetCmp<TextField>("txtTarePalettes").Text);
            if (X.GetCmp<TextField>("txtPoidsNetLivre").Text != string.Empty) mClass.PoidsNetLivre = decimal.Parse(X.GetCmp<TextField>("txtPoidsNetLivre").Text);
            if (X.GetCmp<TextField>("txtRetention").Text != string.Empty) mClass.Refaction = decimal.Parse(X.GetCmp<TextField>("txtRetention").Text);
            if (X.GetCmp<TextField>("txtPoidsNetAccepte").Text != string.Empty) mClass.PoidsNetAccepte = decimal.Parse(X.GetCmp<TextField>("txtPoidsNetAccepte").Text);

            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }

        public ActionResult OnCancel(string ItemSelected)
        {
            try
            {
                Inventaire inventaire = JSON.Deserialize<Inventaire>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                
                bool result = inventaire.fnGet(inventaire.ID);

                if (!result)
                    throw new Exception("OnCancel : Inventaire loading failed.");
                
                inventaire.UtilisateurModification = (string)Session["userName"];

                result = inventaire.fnDeActivate();

                if (!result)
                    throw new Exception("OnCancel : Inventaire, operation failed.");

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeInventaire");

                    ModelProxy mProxy = mstore.GetById(inventaire.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(inventaire);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type De Financement : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult UpdateStock(string inventaireElementEnStockID = "")
        {
            try
            {
                //Inventaire inventaire = JSON.Deserialize<Inventaire>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                InventaireElementStock inventaire = new InventaireElementStock();

                inventaire.IsNew = false;

                bool result = inventaire.fnGet(Guid.Parse(inventaireElementEnStockID));

                if (inventaire == null || inventaire.ID == Guid.Empty)
                    throw new Exception("UpdateStock : Inventaire not Found.");

                if (!result)
                    throw new Exception("UpdateStock : Inventaire not Found.");

                inventaire.UtilisateurModification = (string)Session["userName"];                
                if (X.GetCmp<TextField>("txtQuantitePhysiqueHid").Text != string.Empty) inventaire.QuantitePhysique = int.Parse(X.GetCmp<TextField>("txtQuantitePhysiqueHid").Text);
                if (X.GetCmp<TextField>("txtPoidsPhysique").Text != string.Empty) inventaire.PoidsBrutPhysique = decimal.Parse(X.GetCmp<TextField>("txtPoidsPhysique").Text);
                
                result = inventaire.fnUpdateStock();

                if (!result)
                    throw new Exception("UpdateStock : Stock Update, operation failed.");

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeItemInStock");

                    ModelProxy mProxy = mstore.GetById(inventaire.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(inventaire);

                    mProxy.Commit();

                    mProxy.EndEdit();

                    X.GetCmp<Window>("FormNewStock").Close();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Ajustement de Stock : UpdateStock",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnConfirmStock(string storerows = "")
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();
            try
            {
                List<InventaireElementStock> lignesInventaire = new List<InventaireElementStock>();

                if (!string.IsNullOrEmpty(storerows) || storerows != "[]")
                    lignesInventaire = JSON.Deserialize<List<InventaireElementStock>>(storerows, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Inventaire inventaire = new Inventaire();
                InventaireElementStock itemsIventaire = new InventaireElementStock();
                
                MouvementStock mouvement = new MouvementStock();
                bool resultInventaire = true;
                bool resultMouvement = true;

                resultInventaire = inventaire.fnGet(Guid.Parse(GetFormValue("TxtinventaireID")));

                if (inventaire == null || inventaire.ID == Guid.Empty)
                    throw new Exception("SubmitFormMethod : Inventaire load failed.");

                var mListeItemsInventaire = itemsIventaire.fnSelectItemInStockByID(inventaire.ID);
                //if (X.GetCmp<TextField>("txtQuantiteAjusteeHid").Text != string.Empty) inventaire.QuantitePhysique = int.Parse(X.GetCmp<TextField>("txtQuantiteAjusteeHid").Text);
                //if (X.GetCmp<TextField>("txtPoidsAjusteHid").Text != string.Empty) inventaire.PoidsBrutPhysique = decimal.Parse(X.GetCmp<TextField>("txtPoidsAjusteHid").Text);
                inventaire.Approbateur = (string)Session["userName"];

                if (lignesInventaire.Count > 0)
                {
                    _db = inventaire.db();
                    mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                    resultInventaire = inventaire.fnApprove(mtran);

                    if (resultInventaire)
                    {
                        foreach (InventaireElementStock item in lignesInventaire.Where(iv => iv.GenereMouvement == true && iv.Quantite != iv.QuantitePhysique ))
                        {
                            mouvement = new MouvementStock();
                            mouvement.mCampagne = new Campagne();
                            mouvement.Exportateur = new Exportateur();
                            mouvement.SacType = new SacType();
                            mouvement.TypeElementStock = new TypeElementStock();
                            mouvement.Certification = new Certification();
                            mouvement.Magasin = new Magasin();
                            mouvement.Emplacement = new Emplacement();
                            mouvement.MouvementStockType = new MouvementStockType();
                            InventaireElementStock Items = new InventaireElementStock();
                            Parametres mParam = new Parametres(0);
                            mouvement.SetDataSource(_db);
                            mouvement.mCampagne.Designation = inventaire.Campagne.Designation;
                            mouvement.Exportateur.ID = mParam.Exportateur.ID;
                            mouvement.DateMouvement = DateTime.Now;
                            mouvement.SacType = null;
                            mouvement.ObjetEnStock = item.ObjetEnStockID;
                            mouvement.ObjetEnStockType = mParam.AjustementStockType;
                            //mouvement.TypeElementStock.ID = mParam.AjustementStockType;
                            mouvement.MouvementStockType.ID = mParam.RegularisationStockType;
                            mouvement.Certification = null;
                            mouvement.Sens = -1;
                            mouvement.Quantite = (item.Quantite - item.QuantitePhysique) ;
                            mouvement.PoidsBrut = (item.PoidsBrut - item.PoidsBrutPhysique);
                            mouvement.TarePalettes = 0;
                            mouvement.TareSacs = (Math.Abs((item.Quantite - item.QuantitePhysique) * item.TareSacs)) / item.Quantite;
                            mouvement.PoidsNetLivre = (item.PoidsBrut - item.PoidsBrutPhysique) - mouvement.TareSacs;
                            mouvement.PoidsNetAccepte = 0;
                            mouvement.Retention = 0;
                            mouvement.Magasin.ID = inventaire.Magasin.ID;
                            mouvement.Emplacement.ID = mParam.EmplacementParDefaut;
                            //mouvement.DateMouvement = inventaire.DateInventaire;
                            mouvement.Reference1 = inventaire.Numero;
                            mouvement.Reference2 = item.Reference;
                            mouvement.Commentaire = "Ajustement Inventaire";
                            mouvement.Statut = "AP";
                            mouvement.UtilisateurCreation = (string)Session["userName"];
                            mouvement.UtilisateurModification = (string)Session["userName"];

                            resultMouvement = mouvement.fnUpdate(mtran);
                            if (!resultMouvement)
                                _db.RollBackTransaction(mtran);
                        }

                        //resultMouvement = mouvement.fnUpdate(mtran);
                        if (!resultMouvement)
                            _db.RollBackTransaction(mtran);
                        else
                            _db.CommitTransaction(mtran);
                    }
                    else
                        _db.RollBackTransaction(mtran);
                }
                else
                {
                    resultInventaire = inventaire.fnApprove();
                }

                if (!resultInventaire)
                    throw new Exception("OnCancel : Inventaire loading failed.");

                Store mstore = X.GetCmp<Store>("storeListeInventaire");
                ModelProxy mProxy = mstore.GetById(inventaire.ID);

                mProxy.BeginEdit();
                mProxy.Set(inventaire);
                mProxy.Commit();
                mProxy.EndEdit();

                X.GetCmp<Window>("FormAjustementStock").Close();

            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mtran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Inventaire : Approuver",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        #region Old Confirm Stock
        //public ActionResult OnConfirmStock(string storerows = "")
        //{
        //    DataSource _db = new DataSource();
        //    DataTransaction mtran = new DataTransaction();
        //    try
        //    {
        //        List<InventaireElementStock> lignesInventaire = new List<InventaireElementStock>();

        //        if (!string.IsNullOrEmpty(storerows) || storerows != "[]")
        //            lignesInventaire = JSON.Deserialize<List<InventaireElementStock>>(storerows, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

        //        Inventaire inventaire = new Inventaire();
        //        MouvementStock mouvement = new MouvementStock();
        //        bool resultInventaire = true;
        //        bool resultMouvement = true;

        //        resultInventaire = inventaire.fnGet(Guid.Parse(GetFormValue("TxtinventaireID")));

        //        if (inventaire == null || inventaire.ID == Guid.Empty)
        //            throw new Exception("SubmitFormMethod : Inventaire load failed.");
                
        //        //if (X.GetCmp<TextField>("txtQuantiteAjusteeHid").Text != string.Empty) inventaire.QuantitePhysique = int.Parse(X.GetCmp<TextField>("txtQuantiteAjusteeHid").Text);
        //        //if (X.GetCmp<TextField>("txtPoidsAjusteHid").Text != string.Empty) inventaire.PoidsBrutPhysique = decimal.Parse(X.GetCmp<TextField>("txtPoidsAjusteHid").Text);
        //        inventaire.Approbateur = (string)Session["userName"];
              
        //        if (lignesInventaire.Count > 0)
        //        {
        //            _db = inventaire.db();
        //            mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
        //            resultInventaire = inventaire.fnApprove(mtran);

        //            if (resultInventaire)
        //            {
        //                foreach (InventaireElementStock item in lignesInventaire.Where(iv => iv.GenereMouvement == true))
        //                {
        //                    mouvement = new MouvementStock();
        //                    mouvement.mCampagne = new Campagne();
        //                    mouvement.Exportateur = new Exportateur();
        //                    mouvement.SacType = new SacType();
        //                    mouvement.TypeElementStock = new TypeElementStock();
        //                    mouvement.Certification = new Certification();
        //                    mouvement.Magasin = new Magasin();
        //                    mouvement.Emplacement = new Emplacement();
        //                    mouvement.MouvementStockType = new MouvementStockType();
        //                    InventaireElementStock Items = new InventaireElementStock();
        //                    Parametres mParam = new Parametres(0);
        //                    mouvement.SetDataSource(_db);
        //                    mouvement.mCampagne.Designation = inventaire.Campagne.Designation;
        //                    mouvement.Exportateur.ID = mParam.Exportateur.ID;
        //                    mouvement.DateMouvement = DateTime.Now;
        //                    mouvement.SacType = null;
        //                    mouvement.ObjetEnStock = inventaire.ID;
        //                    mouvement.ObjetEnStockType = mParam.AjustementStockType;
        //                    //mouvement.TypeElementStock.ID = mParam.AjustementStockType;
        //                    mouvement.MouvementStockType.ID = mParam.RegularisationStockType;
        //                    mouvement.Certification = null;
        //                    mouvement.Sens = "S";
        //                    mouvement.Quantite = (item.Quantite - item.QuantitePhysique) * (-1);
        //                    mouvement.PoidsBrut = (item.PoidsBrut - item.PoidsBrutPhysique) * (-1);
        //                    mouvement.TarePalettes = 0;
        //                    mouvement.TareSacs = (Math.Abs((item.Quantite - item.QuantitePhysique) * item.TareSacs)) / item.Quantite;
        //                    mouvement.PoidsNetLivre = (item.PoidsBrut - item.PoidsBrutPhysique) - mouvement.TareSacs;
        //                    mouvement.PoidsNetAccepte = 0;
        //                    mouvement.Retention = 0;
        //                    mouvement.Magasin.ID = inventaire.Magasin.ID;
        //                    mouvement.Emplacement.ID = mParam.EmplacementParDefaut;
        //                    //mouvement.DateMouvement = inventaire.DateInventaire;
        //                    mouvement.Reference1 = inventaire.Numero;
        //                    mouvement.Reference2 = item.Reference;
        //                    mouvement.Commentaire = "Ajustement Inventaire";
        //                    mouvement.Statut = "AP";
        //                    mouvement.UtilisateurCreation = (string)Session["userName"];
        //                    mouvement.UtilisateurModification = (string)Session["userName"];                                                            

        //                    resultMouvement = mouvement.fnUpdate(mtran);
        //                    if (!resultMouvement)
        //                        _db.RollBackTransaction(mtran);
        //                }
                                                
        //                //resultMouvement = mouvement.fnUpdate(mtran);
        //                if (!resultMouvement)
        //                    _db.RollBackTransaction(mtran);
        //                else
        //                    _db.CommitTransaction(mtran);
        //            }
        //            else
        //                _db.RollBackTransaction(mtran);
        //        }
        //        else
        //        {
        //            resultInventaire = inventaire.fnApprove();
        //        }

        //        if (!resultInventaire)
        //            throw new Exception("OnCancel : Inventaire loading failed.");

        //        Store mstore = X.GetCmp<Store>("storeListeInventaire");
        //            ModelProxy mProxy = mstore.GetById(inventaire.ID);

        //            mProxy.BeginEdit();
        //            mProxy.Set(inventaire);
        //            mProxy.Commit();
        //            mProxy.EndEdit();

        //            X.GetCmp<Window>("FormAjustementStock").Close();                

        //    }
        //    catch (Exception ex)
        //    {
        //        _db.RollBackTransaction(mtran);
        //        X.MessageBox.Show(new MessageBoxConfig
        //        {
        //            Title = "Inventaire : Approuver",
        //            Message = ex.Message,
        //            Buttons = MessageBox.Button.OK,
        //            Icon = MessageBox.Icon.WARNING
        //        });
        //    }

        //    return this.Direct();
        //}

        #endregion
        public ActionResult OnPrintStockInventory(string ItemInventaire)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                Guid InventoryID = Guid.Empty;
                if (!string.IsNullOrEmpty(ItemInventaire))
                    InventoryID = Guid.Parse(ItemInventaire);
               
                XtraReport report = null;

                report = new rptFicheInventaire() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["inventaireID"].Value = InventoryID;

                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/InventaireStock/ViewList', this, 'Inventaire de Stock','')", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Inventaire de Stock : Data Validation",
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

        public ActionResult OnPrintList()
        {
            Parametres mParam = new Parametres(0);

            ViewData["Campagne"] = mParam.Campagne;
            ViewData["Magasin"] = mParam.MagasinTV;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Inventory_Print", ViewData = ViewData };
        }

        public ActionResult PrintList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;

                report = new rptInventaireList() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["campagneID"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                report.Parameters["paramMagasin"].Value = int.Parse(X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Value);
                report.Parameters["paramMagasinText"].Value = X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Text;

                report.Parameters["paramPeriode"].Value = int.Parse(X.GetCmp<ComboBox>("cmbPeriode").SelectedItem.Value);
                report.Parameters["paramPeriodeText"].Value = X.GetCmp<ComboBox>("cmbPeriode").SelectedItem.Text;

                report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("CmbStatut").SelectedItem.Value.ToString();
                report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("CmbStatut").SelectedItem.Text;

                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/InventaireStock/ViewList', this, 'List Of Inventaire de Stock',''),App.Inventory_Print.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Inventaire de Stock : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
        }


        private string GetFormValue(string id_Component)
        {
            return Request.Form[id_Component];
        }

        private int GetCriteriaValue(string strComponent)
        {
            int value;

            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
                return value;
            else
                return -1;
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


    }
}