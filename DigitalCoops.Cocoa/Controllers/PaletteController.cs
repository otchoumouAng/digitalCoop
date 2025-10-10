using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Components.Data;
using Tms.Classes.Shared.stock;
using System.Diagnostics;

namespace Tms2017.MVC.Controllers
{
    public class PaletteController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Palette
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{5d7e085f-99a4-4e48-a688-25e6e0845b5f}", UserName) == false)
                X.GetCmp<Button>("btnNewPalette").Disable();
            else
                X.GetCmp<Button>("btnNewPalette").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{9da33bde-06cb-484e-a8a4-b3b374429fed}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListPalette").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListPalette").Enable();

            X.GetCmp<Hidden>("PalettehiddenPermOuvrir").SetValue(HasAccessFunction.fnGetUserAccessStatus("{c16dd72b-d925-4a7e-b5f0-d7d142be879c}", UserName));
            X.GetCmp<Hidden>("PalettehiddenPermFermer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{c16dd72b-d925-4a7e-b5f0-d7d142be879c}", UserName));
            X.GetCmp<Hidden>("PalettehiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{5d7e085f-99a4-4e48-a688-25e6e0845b5f}", UserName));
            X.GetCmp<Hidden>("PalettehiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{c16dd72b-d925-4a7e-b5f0-d7d142be879c}", UserName));


            X.GetCmp<Hidden>("PalettehiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{1f4af822-9de1-490d-bc24-1aa742f18b4e}", UserName));
            X.GetCmp<Hidden>("PalettehiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{11611511-163f-44ca-8200-7c8bc3e79f74}", UserName));
            X.GetCmp<Hidden>("PalettehiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{9da33bde-06cb-484e-a8a4-b3b374429fed}", UserName));
            X.GetCmp<Hidden>("PalettehiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{2e21a0af-a08e-45dd-a3e0-8de117bb062b}", UserName));

            return View();
        }

        public ActionResult LoadPalette()
        {
            List<DataPersist> mList = new Palette().fnSelect();

            Palette mclass = new Palette();

            if (mList.Count > 0)
                mclass = mList[0] as Palette;

            return this.Store(mList);
        }


        public ActionResult LoadPaletteAll()
        {
            List<DataPersist> mList = new Palette().fnSelect();

            Palette mclass = new Palette();

            mclass.ID = Guid.Empty;
            mList.Insert(0, mclass);
            mclass = mList[0] as Palette;

            return this.Store(mList);
        }

        public ActionResult LoadAllPaletteByAccess()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAllAccess = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            List<DataPersist> mList = null;
            Palette mclass = new Palette();

            if (HasAllAccess)
            {
                mList = new Palette().fnSelect();

                mclass.QAStatut = -1;
                //mclass.NumeroProduction = "{Tous}";

                mList.Insert(0, mclass);
                mclass = mList[0] as Palette;
            }
            else
            {
                bool result = mclass.fnGetByUserName(UserName);
                mList = new List<DataPersist>();

                mList.Insert(0, mclass);
                mclass = mList[0] as Palette;
            }
            //= new Palette().fnSelect(0);

            return this.Store(mList);

        }

        public ActionResult LoadPaletteByAccess()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAllAccess = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            List<DataPersist> mList = null;
            Palette mclass = new Palette();

            if (HasAllAccess)
            {
                mList = new Palette().fnSelect();
                mclass = mList[0] as Palette;
            }
            else
            {
                bool result = mclass.fnGetByUserName(UserName);
                mList = new List<DataPersist>();

                mList.Insert(0, mclass);
                mclass = mList[0] as Palette;
            }
            //= new Palette().fnSelect(0);

            return this.Store(mList);

        }

        public ActionResult LoadSpecificPalette()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool CanConsultOverPalette = HasAccessFunction.fnGetUserAccessStatus("{17a9e19c-4667-4c65-b173-3749ab8c263e}", UserName);
            List<DataPersist> mList = null;
            Palette mclass = new Palette();

            bool result = mclass.fnGetByUserName(UserName);
            mList = new List<DataPersist>();

            mList.Insert(0, mclass);
            mclass = mList[0] as Palette;

            if (CanConsultOverPalette)
            {
                mclass = new Palette();
                result = mclass.fnGetDefaultPalette();
                mList.Insert(1, mclass);

                mclass = new Palette();
                mclass.QAStatut = -1;

                mList.Insert(0, mclass);
                mclass = mList[0] as Palette;
            }

            //= new Palette().fnSelect(0);

            return this.Store(mList);

        }


        public ActionResult SelectToPrice(string ItemPrice)
        {
            Guid id = Guid.Empty;
            if (!string.IsNullOrEmpty(ItemPrice) && (Guid.Parse(ItemPrice) != Guid.Empty))
            {
                id = Guid.Parse(ItemPrice);
                var listePalette = new Palette().fnSelectToPrice(id);
                return this.Store(listePalette);
            }
            else
            {
                var listAllPalette = new Palette().fnSelect();
                return this.Store(listAllPalette);
            }
        }

        public ActionResult SelectToPriceGood()
        {
            string UserName = (string)Session["userName"];
            var listePalette = new Palette().fnSelectAvailableForPrice(UserName);
            return this.Store(listePalette);

        }
        //public ActionResult SelectToPrice(string ItemPalette)
        //{
        //    if (!string.IsNullOrEmpty(ItemPalette))
        //    {
        //        int id = Int32.Parse(ItemPalette);
        //        var listePalette = new Palette().fnSelectToPrice(id);
        //        return this.Store(listePalette);
        //    }
        //    else
        //    {
        //        var listAllPalette = new Palette().fnSelect(0);
        //        return this.Store(listAllPalette);
        //    }
        //}

        public ActionResult OnAdd()
        {
            PaletteViewModel PaletteVm = new PaletteViewModel();

            PaletteVm._Palette = new Palette();
            PaletteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDeclarationPalette", Model = PaletteVm };
        }



        public ActionResult OnEdit(string ItemSelected)
        {


            PaletteViewModel PaletteVm = new PaletteViewModel();

            PaletteVm._Palette = JSON.Deserialize<Palette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PaletteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPalette", Model = PaletteVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {


            PaletteViewModel PaletteVm = new PaletteViewModel();

            PaletteVm._Palette = JSON.Deserialize<Palette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PaletteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPalette", Model = PaletteVm };

        }


        public ActionResult SubmitFormMethod()
        {

            try
            {
                Palette mPalette = new Palette();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mPalette.IsNew = true;
                else
                {
                    mPalette.IsNew = false;
                    Guid PaletteID = Guid.Empty;
                    bool IsGuid = Guid.TryParse(GetFormValue("txtPaletteID"), out PaletteID);                   
                    //c'est ok
                    if (IsGuid)
                        mPalette.fnGet(PaletteID);
                    else
                        throw new Exception("SubmitFormMethod : Production Order load failed.");


                    if (mPalette.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Location, load failed.");
                }

                mPalette = MapFormToObject(mPalette);

                bool result = mPalette.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePalette");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mPalette);
                        X.GetCmp<RowSelectionModel>("rowSelectionPalette").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mPalette.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mPalette);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormPalette").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Location : Data Validation...",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemStatus, string ItemAnnee, string ItemSemaine, string ItemProduit, string ItemProduitType, string ItemProduction)
        {
            string status = "-1";
            if (!string.IsNullOrEmpty(ItemStatus) && ItemStatus == "false")
            {
                status = "-1";
            }
            else if(ItemStatus == "true")
            {
                status = "1";
            }

            int Annee = -1;
            if (!string.IsNullOrEmpty(ItemAnnee))
                Annee = int.Parse(ItemAnnee);

            int Semaine = -1;
            if (!string.IsNullOrEmpty(ItemSemaine))
                Semaine = int.Parse(ItemSemaine);

            int Produit = -1;
            if (!string.IsNullOrEmpty(ItemProduit))
                Produit = int.Parse(ItemProduit);

            int ProduitType = -1;
            if (!string.IsNullOrEmpty(ItemProduitType))
                ProduitType = int.Parse(ItemProduitType);

            // string Production = "{Tous}";
            string Production = null;
            if (!string.IsNullOrEmpty(ItemProduction) && ItemProduction == "{Tous}")
                Production = null;
            else if (!string.IsNullOrEmpty(ItemProduction))
            {
                Production = ItemProduction;
            }

            

            var liste = new Palette().fnSelect(status, Annee,Semaine,Produit,ProduitType,Production);
            //var pagning = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Palette Palette = JSON.Deserialize<Palette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = Palette.fnGet(Palette.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Location, loading failed.");
                //(string)Session["userName"];
                Palette.UtilisateurModification = (string)Session["userName"];

                if (Palette.Desactive)
                    result = Palette.fnActivate();
                else
                    result = Palette.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Location, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePalette");

                    ModelProxy mProxy = mstore.GetById(Palette.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(Palette);
                    mProxy.Commit();

                    mProxy.EndEdit();
                    //mstore.Reload();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Location : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("PaletteCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult onCloseProduction(string ItemSelected)
        {
            Palette mclass = new Palette();
            mclass = JSON.Deserialize<Palette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormClosePalette", Model = mclass, };
        }

        public ActionResult onOpenProduction(string ItemSelected)
        {
            Palette mclass = new Palette();
            mclass = JSON.Deserialize<Palette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormOpenPalette", Model = mclass, };
        }

        public ActionResult CloseProduction()
        {
            try
            {
                string id = GetFormValue("TxtPaletteID");
                string dateFin = GetFormValue("txtDateFinProduction");
                string comment = GetFormValue("txtComment");

                Palette mProduction = new Palette();
                mProduction.fnGet(Guid.Parse(id));

                mProduction.DateDeclaration = DateTime.Parse(dateFin);

                bool result = mProduction.fnUpdate();



                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePalette");
                    ModelProxy mProxy = mstore.GetById(mProduction.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mProduction);

                    mProxy.Commit();

                    mProxy.EndEdit();

                    X.GetCmp<Window>("FormClosePalette").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production Order : Close",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }



        public ActionResult DeclarationPalette(string Annee, string Semaine, string Produit, string ProduitType, string Production, string NumeroPalette, string Magasin)
        {
            try
            {
                Palette mPalette = new Palette();
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                // Récupération de l'année avec vérification
                if (string.IsNullOrEmpty(Annee) ||
                   string.IsNullOrEmpty(Semaine) ||
                   string.IsNullOrEmpty(Produit) ||
                   string.IsNullOrEmpty(ProduitType) ||
                   string.IsNullOrEmpty(Production) ||
                   string.IsNullOrEmpty(NumeroPalette) ||
                   string.IsNullOrEmpty(Magasin))
                        {
                            throw new ArgumentException("Tous les paramètres doivent être renseignés");
                        }

                mPalette.Annee = int.Parse(Annee);
                mPalette.Semaine = int.Parse(Semaine);
                mPalette.ProduitID = int.Parse(Produit);
                mPalette.TypeDeProduitID = int.Parse(ProduitType);

                mPalette.OrdreDeProduction = new OrdreFabrication() ;
                mPalette.OrdreDeProduction.ID = Guid.Parse(Production);

                mPalette.ID = Guid.Parse(NumeroPalette);

                mPalette.Magasin = new Magasin();
                mPalette.Magasin.ID = int.Parse(Magasin);



                mPalette.fnPaletteByOf_Get(mPalette.ID);
                mPalette.ModificationUtilisateur = (string)Session["userName"];
                MouvementStockProduitDto MvtStockProduit = null;
                // Alimentation de l'objet de l'ordre de fabrication
                bool fabrication = mPalette.fnGetFabrication(mPalette.OrdreDeProduction);
                bool article = mPalette.fnGetArticle(mPalette.OrdreDeProduction.Article.ID);

                

                if (fabrication && article)
                {
                    MvtStockProduit = new MouvementStockProduitDto
                    {
                        MpCodeMagasin = mPalette.Magasin.ID,
                        MpDate = DateTime.Now,
                        MpCodePalette = mPalette.ID,
                        MpCodeTypeMouvement = 0,
                        MpSens = 1,
                        MpCodeConditionnement = mPalette.OrdreDeProduction.Conditionnement.ID,
                        MpCodeReferenceConditionnement = mPalette.OrdreDeProduction.ConditionnementReference.ID,
                        MpNbreUniteParPalette = mPalette.OrdreDeProduction.Article.NbreUniteParPalette,
                        MpUniteDePoids = "kg",//mPalette.OrdreDeProduction.Article.UniteDePoids,
                        MpPoidsBrutUnitaire = mPalette.OrdreDeProduction.Article.PoidsBrutUnitaire,
                        MpTareUnitaireEmballage = mPalette.OrdreDeProduction.Article.TareUnitaireEmballage,
                        MpPoidsBrutPalette = mPalette.OrdreDeProduction.Article.PoidsBrutPalette,
                        MpTareEmballagePalette = mPalette.OrdreDeProduction.Article.TareEmballagePalette,
                        MpPoidsNetPalette = mPalette.OrdreDeProduction.Article.PoidsNetPalette

                    };
                }

                

                bool result = mPalette.fnUpdateDeclaration(); //true
                bool result2 = mPalette.fnUpdateMouvement(MvtStockProduit); // true

                // --- DEBUT DE LA CORRECTION ---
                if (result && result2)
                {
                    // On prépare une réponse DirectResult pour le client
                    DirectResult r = new DirectResult();

                    // Commande pour fermer la fenêtre modale
                    X.GetCmp<Window>("FormDeclarationPalette").Close();

                    // Commande pour recharger les données de la grille principale
                    X.GetCmp<Store>("storeListePalette").Reload();

                    // Vous pouvez aussi démasquer le viewport si un masque est affiché
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();

                    return r; // On retourne le DirectResult avec les commandes
                }

                else
                {
                    throw new Exception("La mise à jour de la palette a échoué.");
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Erreur",
                    Message = $"Erreur lors de la déclaration: {ex.Message}",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR
                });

                // Même en cas d'erreur, on retourne Direct() pour terminer la requête
                return this.Direct();
            }
        }


        public ActionResult GetPaletteNum(string idOrdreFabrication)
        {
            try
            {
                if (string.IsNullOrEmpty(idOrdreFabrication))
                {
                    return Json(new { success = false, message = "ID de production invalide" });
                }

                Guid ID = Guid.Parse(idOrdreFabrication);

                Palette palette = new Palette();


                var liste = palette.fnGetPaletteByProduction(ID);

                return this.Store(liste);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



       
        public ActionResult OnRefresh(string ItemAnnee, string ItemSemaine, string ItemProduit, string ItemProduitType, string ItemProduction, string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListePalette");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus),
                                new Ext.Net.Parameter("ItemAnnee", ItemAnnee),
                                new Ext.Net.Parameter("ItemSemaine", ItemSemaine),
                                new Ext.Net.Parameter("ItemProduit", ItemProduit),
                                new Ext.Net.Parameter("ItemProduitType", ItemProduitType),
                                new Ext.Net.Parameter("ItemProduction", ItemProduction)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("PaletteCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }


        private Palette MapFormToObject(Palette palette)
        {
            int parser; //
            DateTime parserDate;
            
            palette.ID = Guid.Parse(X.GetCmp<Hidden>("txtPaletteID").Text);

            // --- Zone A ---
            // Ordre de production (obligatoire)

            //palette.OrdreDeProduction = new OrdreFabrication
            //{
            //    NumeroProduction = X.GetCmp<ComboBox>("cmbProduction").Value?.ToString()
            //};

            var cmbOrdreFabrication = X.GetCmp<ComboBox>("cmbProduction");


            //ID = Guid.Parse(cmbOrdreFabrication.SelectedItem.Value),
            if (!string.IsNullOrEmpty(cmbOrdreFabrication.Text))
            {
                palette.OrdreDeProduction = new OrdreFabrication
                {
                    NumeroProduction = cmbOrdreFabrication.SelectedItem.Text
                };
            }





            // Numéro de palette
            palette.Numero = int.Parse(X.GetCmp<NumberField>("txtNumero").Text);

            // Nombre d'unités
            palette.NbreUnite = int.Parse(X.GetCmp<NumberField>("txtNbreUnite").Text);

            // Conditionnement (obligatoire)
            var cmbConditionnement = X.GetCmp<ComboBox>("cmbDesignation");

            palette.CodeConditionnement = new Tms.Classes.Shared.Sales.Conditionnement();
            palette.CodeConditionnement.ID = int.Parse(cmbConditionnement.SelectedItem.Value);
            palette.CodeConditionnement.Designation = cmbConditionnement.SelectedItem.Text;


            

            // Référence Conditionnement
            var cmbRefConditionnement = X.GetCmp<ComboBox>("cmbReference");
            if (!string.IsNullOrEmpty(cmbRefConditionnement.Text))
            {
                palette.CodeReferenceConditionnement = new Tms.Classes.Shared.Sales.ConditionnementReference
                {
                    ID = int.Parse(cmbRefConditionnement.SelectedItem.Value),
                    Reference = cmbRefConditionnement.SelectedItem.Text
                };
            }

            

            // Nombre d'unités par palette
            palette.NbreUniteParPalette = int.Parse(X.GetCmp<NumberField>("txtNbreUniteParPalette").Text);

            // Unité de poids
            palette.UniteDePoids = int.Parse(X.GetCmp<NumberField>("txtUniteDePoids").Text);

            // Poids brut unitaire
            palette.PoidsBrutUnitaire = float.Parse(X.GetCmp<NumberField>("txtPoidsBrutUnitaire").Text);

            // --- Zone B ---
            // Tare unitaire emballage
            palette.TareUnitaireEmballage = int.Parse(X.GetCmp<NumberField>("txtTareUnitaireEmballage").Text);

            // Poids brut palette
            palette.PoidsBrutPalette = float.Parse(X.GetCmp<NumberField>("txtPoidsBrutPalette").Text);

            // Tare emballage palette
            palette.TareEmballagePalette = int.Parse(X.GetCmp<NumberField>("txtTareEmballagePalette").Text);

            // Poids net palette
            palette.PoidsNetPalette = float.Parse(X.GetCmp<NumberField>("txtPoidsNetPalette").Text);

            // Dates (gestion des nullables)
            palette.BestBeforeDate = X.GetCmp<DateField>("txtBestBeforeDate").SelectedDate;
            //palette.DateFabrication = DateTime.Parse(X.GetCmp<DateField>("txtDateFabrication").RawText);
            //Trace.WriteLine(X.GetCmp<DateField>("txtDateFabrication").Text);
            //palette.DateDeclaration = DateTime.Parse(X.GetCmp<DateField>("txtDateDeclaration").RawText);
            //palette.DateDeclaration = DateTime.Parse(X.GetCmp<DateField>("txtDateDeclaration").RawText.ToString()): (DateTime?)null;

            var DateFabrication = X.GetCmp<DateField>("txtDateFabrication").RawText;
            palette.DateFabrication = DateTime.TryParse(DateFabrication, out parserDate) ? (DateTime?)parserDate : null;

            var DateDeclaration = X.GetCmp<DateField>("txtDateDeclaration").RawText;
            palette.DateDeclaration = DateTime.TryParse(DateDeclaration, out parserDate) ? (DateTime?)parserDate : null;





            // Étiquettes
            //palette.NbreEtiquetteA4Demande = int.Parse(X.GetCmp<NumberField>("txtNbreEtiquetteA4Demande").Text);
            var NbreEtiquetteA4Demande = X.GetCmp<NumberField>("txtNbreEtiquetteA4Demande").Text;
            palette.NbreEtiquetteA4Demande = int.TryParse(NbreEtiquetteA4Demande, out parser)
                ? (int?)parser
                : null;

            //palette.NbreEtiquetteA4Imprime = int.Parse(X.GetCmp<NumberField>("txtNbreEtiquetteA4Imprime").Text);
            var NbreEtiquetteA4Imprime = X.GetCmp<NumberField>("txtNbreEtiquetteA4Imprime").Text;
            palette.NbreEtiquetteA4Imprime = int.TryParse(NbreEtiquetteA4Imprime, out parser)
                ? (int?)parser
                : null;

            //palette.NbreEtiquetteA5Demande = int.Parse(X.GetCmp<NumberField>("txtNbreEtiquetteA5Demande").Text);
            var NbreEtiquetteA5Demande = X.GetCmp<NumberField>("txtNbreEtiquetteA5Demande").Text;
            palette.NbreEtiquetteA5Demande = int.TryParse(NbreEtiquetteA5Demande, out parser)
                ? (int?)parser
                : null;

            //palette.NbreEtiquetteA5Imprime = int.Parse(X.GetCmp<NumberField>("txtNbreEtiquetteA5Imprime").Text);
            var NbreEtiquetteA5Imprime = X.GetCmp<NumberField>("txtNbreEtiquetteA5Imprime").Text;
            palette.NbreEtiquetteA5Imprime = int.TryParse(NbreEtiquetteA5Imprime, out parser)
                ? (int?)parser
                : null;

            // QA Statut
            //palette.QAStatut = int.Parse(X.GetCmp<NumberField>("txtQAStatut").Text);
            var QAStatut = X.GetCmp<NumberField>("txtQAStatut").Text;
            palette.QAStatut = int.TryParse(QAStatut, out parser)
                ? (int?)parser
                : null;

            // Champs texte
            palette.CodeSSCC = X.GetCmp<TextField>("txtCodeSSCC").Text;
            

            palette.StockMagasin = X.GetCmp<TextField>("txtStockMagasin").Text;
            palette.StockEmplacement = X.GetCmp<TextField>("txtStockEmplacement").Text;

            // --- Données de traçabilité ---
            palette.UtilisateurCreation = (string)Session["userName"];
            palette.UtilisateurModification = (string)Session["userName"];

            return palette;
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
            X.GetCmp<RowSelectionModel>("rowSelectionPalette").DeselectAll();
        }


        #endregion

    }
}