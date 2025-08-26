using DevExpress.XtraReports.UI;
using DigitalCoops.Cocoa.Models;
using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Business;
using Tms.Classes.Business.stock;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Classes.Shared.Sales;
using Tms.Components.Data;
using Tms2017.MVC.Controllers;
using System.IO;

namespace Cooperative.Controllers
{
    public class DemandeEtiquetteController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        // GET: DemandeEtiquette
        public ActionResult Index()
        {
            #region Set Function's Access

            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{34caff7a-0f0d-40ee-92b3-9de7f7d50b39}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{dd88803b-5f9c-41f2-881c-91ecedc61708}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();



            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{dd88803b-5f9c-41f2-881c-91ecedc61708}")))
                X.GetCmp<Hidden>("arhiddenPermModify").SetValue(true);
            else
                X.GetCmp<Hidden>("arhiddenPermModify").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{ace59756-d945-4d53-92b1-49e1ee69520c}")))
                X.GetCmp<Hidden>("arhiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("arhiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{30ee1bab-7d3c-4fcb-88a9-628db185f061}")))
                X.GetCmp<Hidden>("arhiddenPermApprove").SetValue(true);
            else
                X.GetCmp<Hidden>("arhiddenPermApprove").SetValue(false);

            #endregion

            return View();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("DemandeEtiquetteCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemAnnee, string ItemSemaine, int ItemProduitID, int ItemProduitTypeID, string ItemStatut)

        {
            // Get filter criteria values
            string Annee = string.IsNullOrEmpty(ItemAnnee) ? "-1" : ItemAnnee;
            string Semaine = string.IsNullOrEmpty(ItemSemaine) ? "-1" : ItemSemaine;
            int produitId = ItemProduitID;
            int produitTypeId = ItemProduitTypeID;
            string statut = string.IsNullOrEmpty(ItemStatut) ? "-1" : ItemStatut;

            // Get filtered list using all criteria
            var mListe = (new DemandeEtiquette()).fnSelect(Annee, Semaine, produitId, produitTypeId, statut);


            return this.Store(mListe);
        }


        public ActionResult OnRefresh(string ItemAnnee, string ItemSemaine, int ItemProduitID, int ItemProduitTypeID, string ItemStatut)
        {
            try
            {


                Store mstore = X.GetCmp<Store>("storeListeDemandeEtiquette");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemAnnee",ItemAnnee),
                                    new Ext.Net.Parameter("ItemSemaine",ItemSemaine),
                                    new Ext.Net.Parameter("ItemProduitID",ItemProduitID),
                                    new Ext.Net.Parameter("ItemProduitTypeID",ItemProduitTypeID),
                                    new Ext.Net.Parameter("ItemStatut",ItemStatut)
                                });

                X.GetCmp<FormPanel>("DemandeEtiquetteCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - DemandeEtiquette : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult Load()
        {
            List<DataPersist> mList = new DemandeEtiquette().fnSelect();
            return this.Store(mList);
        }

        public ActionResult LoadActive()
        {
            List<DataPersist> mList = new DemandeEtiquette().fnSelect();
            return this.Store(mList);
        }

        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new DemandeEtiquette().fnSelect();
            DemandeEtiquette mclass = new DemandeEtiquette();

            mclass.ID = Guid.Empty;
            mList.Insert(0, mclass);

            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PermissionPalette();

            DemandeEtiquetteViewModel DemandeEtiquetteVm = new DemandeEtiquetteViewModel();

            DemandeEtiquetteVm._DemandeEtiquette = new DemandeEtiquette();
            DemandeEtiquetteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDemandeEtiquette", Model = DemandeEtiquetteVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PermissionPalette();

            DemandeEtiquetteViewModel DemandeEtiquetteVm = new DemandeEtiquetteViewModel();

            DemandeEtiquetteVm._DemandeEtiquette = JSON.Deserialize<DemandeEtiquette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            DemandeEtiquetteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDemandeEtiquette", Model = DemandeEtiquetteVm, ViewData = ViewData };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            

            DemandeEtiquetteViewModel DemandeEtiquetteVm = new DemandeEtiquetteViewModel();

            DemandeEtiquetteVm._DemandeEtiquette = JSON.Deserialize<DemandeEtiquette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            DemandeEtiquetteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDemandeEtiquette", Model = DemandeEtiquetteVm };

        }

        public ActionResult OnApproveView(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PermissionPalette();

            DemandeEtiquetteViewModel DemandeEtiquetteVm = new DemandeEtiquetteViewModel();

            DemandeEtiquetteVm._DemandeEtiquette = JSON.Deserialize<DemandeEtiquette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            DemandeEtiquetteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDemandeEtiquette", Model = DemandeEtiquetteVm, ViewData = ViewData };

        }

        public ActionResult OnPrintEtiquette(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

             PermissionPalette();

            DemandeEtiquetteViewModel DemandeEtiquetteVm = new DemandeEtiquetteViewModel();

            DemandeEtiquetteVm._DemandeEtiquette = JSON.Deserialize<DemandeEtiquette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            DemandeEtiquetteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintDemandeEtiquette", Model = DemandeEtiquetteVm, ViewData = ViewData };
        }

        public ActionResult OnApprove()
        {
            try
            {
                DemandeEtiquette demandeEtiquette = new DemandeEtiquette();

                demandeEtiquette.ID = Guid.Parse(X.GetCmp<TextField>("TxtDemandeEtiquetteId").Text.ToString());

                bool result = demandeEtiquette.fnGet(demandeEtiquette.ID);

                if (demandeEtiquette == null || demandeEtiquette.ID == Guid.Empty)
                    throw new Exception("OnApprove : DemandeEtiquette Approve failed.");

                demandeEtiquette.Approbateur = (string)Session["userName"];
                demandeEtiquette.IsApproved = true;
                demandeEtiquette.DateEffectiveProduction = DateTime.Parse(GetFormValue("txtBestBeforeDate"));
                demandeEtiquette.BestBeforeDate = DateTime.Parse(GetFormValue("txtBestBeforeDate"));
                demandeEtiquette.NbreExemplaireA4 = int.Parse(X.GetCmp<TextField>("txtNbreExemplaireA4").Text);
                demandeEtiquette.NbreExemplaireA5 = int.Parse(X.GetCmp<TextField>("txtNbreExemplaireA5").Text);
                demandeEtiquette.UtilisateurModification = (string)Session["userName"];

                result = demandeEtiquette.fnApprove();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeDemandeEtiquette");

                    ModelProxy mProxy = mstore.GetById(demandeEtiquette.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(demandeEtiquette);

                    mProxy.Commit();

                    mProxy.EndEdit();
                    X.GetCmp<Window>("FormDemandeEtiquette").Close();
                    X.Js.Call("OverviewForm.resetButtons");
                }
                else
                {
                    X.GetCmp<Window>("FormDemandeEtiquette").Close();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Demande D'etiquette : Approuver",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }


        public ActionResult SubmitFormMethod()
        {

            try
            {
                DemandeEtiquette DemandeEtiquette = new DemandeEtiquette();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    DemandeEtiquette.IsNew = true;
                else
                {
                    DemandeEtiquette.IsNew = false;

                    DemandeEtiquette.fnGet(int.Parse(GetFormValue("TxtDemandeEtiquetteID")));

                    if (DemandeEtiquette == null)
                        throw new Exception("SubmitFormMethod : DemandeEtiquette load failed.");
                }

                DemandeEtiquette = MapFormToObject(DemandeEtiquette);

                bool result = DemandeEtiquette.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeDemandeEtiquette");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, DemandeEtiquette);
                        X.GetCmp<RowSelectionModel>("rowSelectionDemandeEtiquette").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(DemandeEtiquette.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(DemandeEtiquette);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormDemandeEtiquette").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "DemandeEtiquette : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }



        public ActionResult SelectOrdreFabricationInfo(string ItemOrdreFabricationID, string ItemDateEffectiveProduction)
        {

            try
            {
                OrdreFabrication mClass = new OrdreFabrication();

                string parsed = JsonConvert.DeserializeObject<string>(ItemDateEffectiveProduction);
                DateTime DateEffectiveProduction = DateTime.Parse(parsed);

                mClass.fnSelectByNumber(ItemOrdreFabricationID, DateEffectiveProduction);
                if (mClass.NumeroProduction != String.Empty)
                {
                    X.GetCmp<TextField>("txtOpOrdreFabricationID").Text = string.IsNullOrEmpty(mClass.ID.ToString()) ? "" : mClass.ID.ToString();
                    X.GetCmp<TextField>("txtOpOrdreFabricationNum").Text = string.IsNullOrEmpty(mClass.NumeroProduction) ? "" : mClass.NumeroProduction;

                    X.GetCmp<TextField>("txtOpProduit").Text = string.IsNullOrEmpty(mClass.ProduitFini.Designation) ? "" : mClass.ProduitFini.Designation;
                    X.GetCmp<TextField>("txtOpTypeProduit").Text = string.IsNullOrEmpty(mClass.ProduitType.Designation) ? "" : mClass.ProduitType.Designation;
                    X.GetCmp<TextField>("TxtOpArticleCode").Text = string.IsNullOrEmpty(mClass.Article.Code) ? "" : mClass.Article.Code;
                    X.GetCmp<TextField>("txtOpNomArticle").Text = string.IsNullOrEmpty(mClass.Article.Nom) ? "" : mClass.Article.Nom;
                    X.GetCmp<TextField>("txtOpEmballage").Text = string.IsNullOrEmpty(mClass.ConditionnementProduit.Designation) ? "" : mClass.ConditionnementProduit.Designation;
                    X.GetCmp<TextField>("txtOpNbreUniteParPalette").Text = string.IsNullOrEmpty(mClass.Article.NbreUniteParPalette.ToString()) ? "" : mClass.Article.NbreUniteParPalette.ToString();
                    X.GetCmp<TextField>("txtOpNbrePaletteAProduit").Text = string.IsNullOrEmpty(mClass.NbrePaletteAProduire.ToString()) ? "" : mClass.NbrePaletteAProduire.ToString();

                    X.GetCmp<TextField>("TxtOpArticleID").Text = string.IsNullOrEmpty(mClass.Article.ID.ToString()) ? "" : mClass.Article.ID.ToString();
                    X.GetCmp<TextField>("txtArticleBestBeforeDate").Text = string.IsNullOrEmpty(mClass.Article.BestBeforeDate.ToString()) ? "" : mClass.Article.BestBeforeDate.ToString();

                    X.GetCmp<DateField>("txtBestBeforeDate").SetValue(mClass.Article.BBDate);

                    X.GetCmp<TextField>("txtNbreExemplaireA4").Text = string.IsNullOrEmpty(mClass.Article.NbreTiquetteParDefautA4.ToString()) ? "0" : mClass.Article.NbreTiquetteParDefautA4.ToString();
                    X.GetCmp<TextField>("txtNbreExemplaireA5").Text = string.IsNullOrEmpty(mClass.Article.NbreTiquetteParDefautA5.ToString()) ? "0" : mClass.Article.NbreTiquetteParDefautA5.ToString();
                }
            }
            catch (Exception ex)
            {

                X.GetCmp<FormPanel>("DemandeEtiquetteFormPanel").Reset();
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Demande d'etiquette : Info",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            //OnRefresh(mClass.ID.ToString());
            return this.Direct();
        }


        public ActionResult OnPrintList()
        {
            DemandeEtiquette mclass = new DemandeEtiquette();
            Parametres mParam = new Parametres(0);

            ViewData["Campagne"] = mParam.Campagne;
            //ViewData["Produit"] = mParam.Produit.ID;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "DemandeEtiquette_Print", ViewData = ViewData };
        }

        //public ActionResult OnPrintDemandeEtiquetteList()
        //{
        //    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
        //    try
        //    {
        //        XtraReport report = null;

        //        report = new rptDemandeEtiquettesList() as XtraReport;

        //        report.DataSource = DevExpressReportDs.SetDataSource(report);
        //        report.Parameters["campagneID"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;
        //        report.Parameters["paramCampagneText"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

        //        report.Parameters["paramExportateur"].Value = int.Parse(X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Value);
        //        report.Parameters["paramExportateurNom"].Value = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text;

        //        report.Parameters["paramCertification"].Value = int.Parse(X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Value);
        //        report.Parameters["paramCertificationText"].Value = X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Text;

        //        report.Parameters["paramDemandeEtiquetteType"].Value = int.Parse(X.GetCmp<ComboBox>("cmbTypeDemandeEtiquette").SelectedItem.Value);
        //        report.Parameters["paramDemandeEtiquetteTypeText"].Value = X.GetCmp<ComboBox>("cmbTypeDemandeEtiquette").SelectedItem.Text;

        //        report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateDebut").RawText);
        //        report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateFin").RawText);

        //        report.Parameters["paramStatut"].Value = int.Parse(X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Value);
        //        report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Text;

        //        Session["report"] = report;


        //        return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/DemandeEtiquette_GestionStock/ViewList', this, 'List Of DemandeEtiquettes',''),App.DemandeEtiquette_GestionStock_Print.doClose()", Guid.NewGuid(), BaseUrl));
        //    }
        //    catch (Exception ex)
        //    {
        //        X.MessageBox.Show(new MessageBoxConfig
        //        {
        //            Title = "Livraison : Data Validation",
        //            Message = ex.Message,
        //            Buttons = MessageBox.Button.OK,
        //            Icon = MessageBox.Icon.WARNING
        //        });
        //        return this.Direct();
        //    }
        //}


        public ActionResult ViewList()
        {
            XtraReport report = null;
            report = Session["report"] as XtraReport;
            //report = new rptDemandeEtiquettesList() as XtraReport;

            //report.DataSource = DevExpressReportDs.SetDataSource(report);
            //report.Parameters["campagneID"].Value = Session["CampagneID"];
            //report.Parameters["paramCampagneText"].Value = Session["paramCampagneText"];

            //report.Parameters["paramExportateur"].Value = int.Parse(Session["paramExportateur"].ToString());
            //report.Parameters["paramExportateurNom"].Value = Session["paramExportateurNom"];

            //report.Parameters["paramCertification"].Value = int.Parse(Session["paramCertification"].ToString());
            //report.Parameters["paramCertificationText"].Value = Session["paramCertificationText"];

            //report.Parameters["paramDemandeEtiquetteType"].Value = int.Parse(Session["paramDemandeEtiquetteType"].ToString());
            //report.Parameters["paramDemandeEtiquetteTypeText"].Value = Session["paramDemandeEtiquetteTypeText"];

            //report.Parameters["paramDateDebut"].Value = DateTime.Parse(Session["paramDateDebut"].ToString());
            //report.Parameters["paramDateFin"].Value = DateTime.Parse(Session["paramDateFin"].ToString());

            //report.Parameters["paramStatut"].Value = int.Parse(Session["paramStatut"].ToString());
            //report.Parameters["paramStatutText"].Value = Session["paramStatutText"];            

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }


        public class ItemModel
        {
            public string ID { get; set; }
            public string OrdreFabricationNumeroAsString { get; set; }
            public string Numero { get; set; }
            public string NbreExemplaire { get; set; }
            // Ajoutez d'autres propriétés selon vos besoins
            public string NbreUnite { get; set; }
            public string PoidsBrutUnitaire { get; set; }
            public string UniteDePoidsDesignation { get; set; }
            public string DateFabricationAsString { get; set; }
            public string DemandeEtiquetteBestBeforeDate { get; set; }
            public string TypeEtiquette { get; set; }
            public string ProduitTypeDesignation { get; set; }

        }


        public ActionResult OnPrintDemandeEtiquette(string selectedIds, string typeEtiquette)
        {
            if (string.IsNullOrEmpty(selectedIds))
                throw new Exception("Aucun élément sélectionné.");

            List<ItemModel> items;
            try
            {
                items = JsonConvert.DeserializeObject<List<ItemModel>>(selectedIds);

                if (items == null || !items.Any())
                    throw new Exception("Aucun élément valide trouvé dans les données.");
            }
            catch (Exception ex)
            {
                throw new Exception("Format des données invalide. Le format attendu est un tableau JSON d'objets.", ex);
            }

            // Stockage en session pour ViewDemandeEtiquette
            string sessionKey = Guid.NewGuid().ToString();
            System.Web.HttpContext.Current.Session[sessionKey] = selectedIds;

            string baseUrl = $"{Request.Url.Scheme}://{Request.Url.Authority}{Url.Content("~")}";

            // ✅ Script JS regroupé : reload du store + ouverture de l’onglet
            var script = $@"
                App.storePaletteListe.reload();
                addTab(
                    window.parent.Ext.getCmp('tabCenter'),
                    'DemandeEtiquette{sessionKey}',
                    '{baseUrl}/DemandeEtiquette/ViewDemandeEtiquette?key={sessionKey}&typeEtiquette={typeEtiquette}',
                    this,
                    'Etiquette - Palette',
                    ''
                );
            ";


            X.Js.AddScript(script);
           

            // ✅ Réponse Direct() unique
            return this.Direct();
        }



        public System.Drawing.Image Base64ToImage(string base64String)
        {
            // Convertir la chaîne Base64 en tableau de bytes
            byte[] imageBytes = Convert.FromBase64String(base64String);

            // Créer un flux mémoire à partir des bytes
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                // Créer et retourner l'image à partir du flux mémoire
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                return image;
            }
        }


        private void _XtraPrintEtiquette(XtraReport rpt, ItemModel code, XtraReport compositeReport, Parametres parametres)
        {
            rpt.Parameters["ProduitTypeDesignation"].Value = code.ProduitTypeDesignation;
            rpt.Parameters["Numero"].Value = code.OrdreFabricationNumeroAsString;
            rpt.Parameters["NumeroPalette"].Value = code.Numero;
            rpt.Parameters["NbreUnite"].Value = code.NbreUnite + " X " + code.PoidsBrutUnitaire + " " + code.UniteDePoidsDesignation;
            rpt.Parameters["PoidsBrutUnitaire"].Value = code.PoidsBrutUnitaire;
            rpt.Parameters["UniteDePoidsDesignation"].Value = code.UniteDePoidsDesignation ;
            rpt.Parameters["DateFabrication"].Value = code.DateFabricationAsString;
            rpt.Parameters["DemandeEtiquetteBestBeforeDate"].Value = code.DemandeEtiquetteBestBeforeDate;

            rpt.Parameters["QrCode"].Value = Base64ToImage(new QrCodeGenerator().GenerateQrCodeAsBase64("https://example.com?Numero=" + code.OrdreFabricationNumeroAsString));

            rpt.Parameters["NomSociete"].Value = parametres.NomSociete;
            rpt.Parameters["AdresseSociete"].Value = parametres.AdresseSociete;
            rpt.Parameters["TelSociete"].Value = parametres.TelSociete;

            // Générer le document
            rpt.CreateDocument();
            compositeReport.Pages.AddRange(rpt.Pages);
        }



        public ActionResult ViewDemandeEtiquette(string key, string typeEtiquette)
        {// Récupérez les données depuis la session
            var selectedIds = System.Web.HttpContext.Current.Session[key] as string;

            if (string.IsNullOrEmpty(selectedIds))
                throw new Exception("Données introuvables.");

            // Désérialisation et génération du rapport
            var items = JsonConvert.DeserializeObject<List<ItemModel>>(selectedIds);

            // Créez un rapport composite qui contiendra tous les sous-rapports
            XtraReport compositeReport = new XtraReport();

            var parametres = new Parametres().fnSelect().FirstOrDefault();


            if (typeEtiquette == "A4")
            {

                foreach (var code in items)
                {
                    // Créez un rapport pour chaque exemplaire de chaque code
                    for (int i = 0; i < int.Parse(code.NbreExemplaire); i++)
                    {
                        RptDemandeEtiquetteA4 rpt = new RptDemandeEtiquetteA4();
                        _XtraPrintEtiquette(rpt, code, compositeReport, (Parametres)parametres);
                        new Palette().fnUpdateToPrint(Guid.Parse(code.ID), "A4");
                    }
                }
            }
            else
            {
                foreach (var code in items)
                {
                    // Créez un rapport pour chaque exemplaire de chaque code
                    for (int i = 0; i < int.Parse(code.NbreExemplaire); i++)
                    {


                        RptDemandeEtiquetteA5 rpt = new RptDemandeEtiquetteA5();
                        _XtraPrintEtiquette(rpt, code, compositeReport, (Parametres)parametres);
                        new Palette().fnUpdateToPrint(Guid.Parse(code.ID), "A5");

                    }
                }
            }

            
            

            ViewData["Report"] = compositeReport;
            return View();
        }


        private DemandeEtiquette MapFormToObject(DemandeEtiquette mClass)
        {


            OrdreFabrication OrdreFabrication = new OrdreFabrication();
            OrdreFabrication.ID = Guid.Parse(X.GetCmp<TextField>("txtOpOrdreFabricationID").Text.ToString());
            OrdreFabrication.NumeroProduction = X.GetCmp<TextField>("txtOpOrdreFabricationNum").Text.ToString() != "" ? X.GetCmp<TextField>("txtOpOrdreFabricationNum").Text.ToString() : X.GetCmp<ComboBox>("cmbOrdreFabricationID").SelectedItem.Text.ToString();
            OrdreFabrication.Annee = int.Parse(X.GetCmp<ComboBox>("cmbAnnee").SelectedItem.Text.ToString());
            OrdreFabrication.Semaine = int.Parse(X.GetCmp<ComboBox>("cmbSemaine").SelectedItem.Text.ToString());
            OrdreFabrication.NbrePaletteAProduire = int.Parse(X.GetCmp<TextField>("txtOpNbrePaletteAProduit").Text.ToString());
            mClass.OrdreFabrication = OrdreFabrication;

            Article Article = new Article();
            Article.ID = Guid.Parse(X.GetCmp<TextField>("TxtOpArticleID").Text);
            Article.Code = X.GetCmp<TextField>("TxtOpArticleCode").Text;
            Article.Nom = X.GetCmp<TextField>("txtOpNomArticle").Text;
            Article.BestBeforeDate = int.Parse(X.GetCmp<TextField>("txtArticleBestBeforeDate").Text.ToString());
            Article.NbreUniteParPalette = int.Parse(X.GetCmp<TextField>("txtOpNbreUniteParPalette").Text);
            mClass.Article = Article;

            ProduitFini ProduitFini = new ProduitFini();
            ProduitFini.Designation = X.GetCmp<TextField>("txtOpProduit").Text;
            mClass.ProduitFini = ProduitFini;

            ProduitType ProduitType = new ProduitType();
            ProduitType.Designation = X.GetCmp<TextField>("txtOpTypeProduit").Text;
            mClass.ProduitType = ProduitType;

            ConditionnementProduit ConditionnementProduit = new ConditionnementProduit();
            ConditionnementProduit.Designation = X.GetCmp<TextField>("txtOpEmballage").Text;
            mClass.ConditionnementProduit = ConditionnementProduit;

            mClass.DateEffectiveProduction = DateTime.Parse(GetFormValue("txtDateEffectiveFabrication"));

            mClass.Description = X.GetCmp<TextArea>("txtDescription").Text;

            mClass.BestBeforeDate = DateTime.Parse(GetFormValue("txtBestBeforeDate"));

            mClass.NbreExemplaireA4 = int.Parse(X.GetCmp<TextField>("txtNbreExemplaireA4").Text.ToString());
            mClass.NbreExemplaireA5 = int.Parse(X.GetCmp<TextField>("txtNbreExemplaireA5").Text.ToString());

            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }





        [HttpPost]
        public ActionResult UpdateFormMethod()
        {
            try
            {
                DemandeEtiquette mClass = new DemandeEtiquette();
                bool result = true;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtDemandeEtiquetteId")));

                    if (mClass == null)
                        throw new Exception("UpdateFormMethod :  Demande Etiquette - load failed.");

                }

                mClass = MapFormToObject(mClass);
                result = mClass.fnUpdate();

                if (result)
                {

                    X.GetCmp<Panel>("PnPalette").Disabled = false;
                    X.GetCmp<Button>("btnConfirmer").Disabled = true;
                    X.GetCmp<Button>("BtnGenerateCode").Disabled = false;

                    Store mStore = X.GetCmp<Store>("storeListeDemandeEtiquette");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowDemandeEtiquette").Select(0);
                    }
                    else
                    {
                        Store mstore = X.GetCmp<Store>("storeListeDemandeEtiquette");
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    return this.Direct();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Demande Etiquette : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        private string GetFormValue(string id_Component)
        {
            return Request.Form[id_Component];
        }

        private void PermissionPalette ()
        {
             string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            bool PAhiddenPermAdd = HasAccess.fnGetUserAccessStatus("{3C067A90-AF15-4170-A253-2BB39575591E}", UserName);
            bool PAhiddenPermRemove = HasAccess.fnGetUserAccessStatus("{3C067A90-AF15-4170-A253-2BB39575591E}", UserName);
            bool PAhiddenPermPrintCode = HasAccess.fnGetUserAccessStatus("{933ECBC8-7E9F-4126-BD1D-3307A4CB4824}", UserName);

            ViewData["PAhiddenPermAdd"] = PAhiddenPermAdd;
            ViewData["PAhiddenPermRemove"] = PAhiddenPermRemove;
            ViewData["PAhiddenPermPrintCode"] = PAhiddenPermPrintCode;
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

        private void CloseDetailWindow()
        {
            X.GetCmp<Window>("FormDemandeEtiquette").Close();
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








    }
}