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

namespace Cooperative.Controllers
{
    public class ArticleController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        // GET: Article
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
            FormPanel mform = X.GetCmp<FormPanel>("ArticleCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, int ItemProduitID, int ItemProduitTypeID, int ItemMarqueProduitID, int ItemActif, string ItemStatut)

        {
            // Get filter criteria values
            int produitId = ItemProduitID;
            int produitTypeId = ItemProduitTypeID;
            int marqueProduitId = ItemMarqueProduitID;
            int actif = ItemActif;
            string statut = string.IsNullOrEmpty(ItemStatut) ? "-1" : ItemStatut;


            // Get filtered list using all criteria
            var mListe = (new Article()).fnSelect(produitId, produitTypeId, marqueProduitId, statut, actif);

            return this.Store(mListe);
        }


        public ActionResult OnRefresh(int ItemTypeProduitID, int ItemMarqueProduitID, int ItemActif, string ItemStatut)
        {
            try
            {


                Store mstore = X.GetCmp<Store>("storeListeArticle");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemTypeProduitID",ItemTypeProduitID),
                                    new Ext.Net.Parameter("ItemMarqueProduitID",ItemMarqueProduitID),
                                    new Ext.Net.Parameter("ItemActif",ItemActif),
                                    new Ext.Net.Parameter("ItemStatut",ItemStatut)
                                });

                X.GetCmp<FormPanel>("ArticleCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - Article : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult Load()
        {
            List<DataPersist> mList = new Article().fnSelect();
            return this.Store(mList);
        }

        public ActionResult LoadActive()
        {
            List<DataPersist> mList = new Article().fnSelect();
            return this.Store(mList);
        }

        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new Article().fnSelect();
            Article mclass = new Article();

            mclass.ID = System.Guid.NewGuid();
            mclass.Nom = "{Tous}";
            mList.Insert(0, mclass);

            return this.Store(mList);

        }

        public ActionResult LoadByType(int typeProduitID = -1)
        {
            List<DataPersist> mList = new Article().fnSelect(-1, typeProduitID, -1, "-1", -1);

            Article mclass = new Article();
            mclass.ID = Guid.Empty; // Utiliser Guid.Empty pour "Tous"
            mclass.Nom = "{Tous}";
            mList.Insert(0, mclass);

            return this.Store(mList);
        }



        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ArticleViewModel ArticleVm = new ArticleViewModel();

            ArticleVm._Article = new Article();
            ArticleVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormArticle", Model = ArticleVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ArticleViewModel ArticleVm = new ArticleViewModel();

            ArticleVm._Article = JSON.Deserialize<Article>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ArticleVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormArticle", Model = ArticleVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ArticleViewModel ArticleVm = new ArticleViewModel();

            ArticleVm._Article = JSON.Deserialize<Article>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ArticleVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormArticle", Model = ArticleVm };

        }

        public ActionResult OnApprove(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();
            try
            {
                Article mClass = JSON.Deserialize<Article>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
                Guid mId = mClass.ID;
                bool result = mClass.fnGet(mId);
                mClass.UtilisateurModification = (string)Session["userName"];

                _db = mClass.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = mClass.fnApprove(mtran);



                if (result)
                {
                    _db.CommitTransaction(mtran);

                    Store mstore = X.GetCmp<Store>("storeListeArticle");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }
            }
            catch (Exception ex)
            {
                if (_db != null)
                {
                    _db.RollBackTransaction(mtran);
                }
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Article article = new Article();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    article.IsNew = true;
                else
                {
                    article.IsNew = false;

                    article.fnGet(int.Parse(GetFormValue("TxtArticleID")));

                    if (article == null)
                        throw new Exception("SubmitFormMethod : Article load failed.");
                }

                article = MapFormToObject(article);

                bool result = article.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeArticle");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, article);
                        X.GetCmp<RowSelectionModel>("rowSelectionArticle").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(article.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(article);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormArticle").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Article : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }
        public ActionResult OnPrintList()
        {
            Article mclass = new Article();
            Parametres mParam = new Parametres(0);

            ViewData["Campagne"] = mParam.Campagne;
            //ViewData["Produit"] = mParam.Produit.ID;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Article_Print", ViewData = ViewData };
        }

        //public ActionResult OnPrintArticleList()
        //{
        //    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
        //    try
        //    {
        //        XtraReport report = null;

        //        report = new rptArticlesList() as XtraReport;

        //        report.DataSource = DevExpressReportDs.SetDataSource(report);
        //        report.Parameters["campagneID"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;
        //        report.Parameters["paramCampagneText"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

        //        report.Parameters["paramExportateur"].Value = int.Parse(X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Value);
        //        report.Parameters["paramExportateurNom"].Value = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text;

        //        report.Parameters["paramCertification"].Value = int.Parse(X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Value);
        //        report.Parameters["paramCertificationText"].Value = X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Text;

        //        report.Parameters["paramArticleType"].Value = int.Parse(X.GetCmp<ComboBox>("cmbTypeArticle").SelectedItem.Value);
        //        report.Parameters["paramArticleTypeText"].Value = X.GetCmp<ComboBox>("cmbTypeArticle").SelectedItem.Text;

        //        report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateDebut").RawText);
        //        report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateFin").RawText);

        //        report.Parameters["paramStatut"].Value = int.Parse(X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Value);
        //        report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Text;

        //        Session["report"] = report;


        //        return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Article_GestionStock/ViewList', this, 'List Of Articles',''),App.Article_GestionStock_Print.doClose()", Guid.NewGuid(), BaseUrl));
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
            //report = new rptArticlesList() as XtraReport;

            //report.DataSource = DevExpressReportDs.SetDataSource(report);
            //report.Parameters["campagneID"].Value = Session["CampagneID"];
            //report.Parameters["paramCampagneText"].Value = Session["paramCampagneText"];

            //report.Parameters["paramExportateur"].Value = int.Parse(Session["paramExportateur"].ToString());
            //report.Parameters["paramExportateurNom"].Value = Session["paramExportateurNom"];

            //report.Parameters["paramCertification"].Value = int.Parse(Session["paramCertification"].ToString());
            //report.Parameters["paramCertificationText"].Value = Session["paramCertificationText"];

            //report.Parameters["paramArticleType"].Value = int.Parse(Session["paramArticleType"].ToString());
            //report.Parameters["paramArticleTypeText"].Value = Session["paramArticleTypeText"];

            //report.Parameters["paramDateDebut"].Value = DateTime.Parse(Session["paramDateDebut"].ToString());
            //report.Parameters["paramDateFin"].Value = DateTime.Parse(Session["paramDateFin"].ToString());

            //report.Parameters["paramStatut"].Value = int.Parse(Session["paramStatut"].ToString());
            //report.Parameters["paramStatutText"].Value = Session["paramStatutText"];            

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }


        private Article MapFormToObject(Article mClass)
        {
            mClass.Nom = X.GetCmp<TextField>("TxtNom").Text;
            mClass.CodeEtendu = X.GetCmp<TextField>("TxtCodeEtendu").Text;

            Produit produit = new Produit();
            produit.ID = int.Parse(X.GetCmp<ComboBox>("cmbProduit").SelectedItem.Value.ToString());
            produit.Designation = X.GetCmp<ComboBox>("cmbProduit").SelectedItem.Text;
            mClass.Produit = produit;

            ProduitType produitType = new ProduitType();
            produitType.ID = int.Parse(X.GetCmp<ComboBox>("cmbProtuitType").SelectedItem.Value.ToString());
            produitType.Designation = X.GetCmp<ComboBox>("cmbProtuitType").SelectedItem.Text;
            mClass.ProduitType = produitType;

            MarqueProduit marqueProduit = new MarqueProduit();
            marqueProduit.ID = int.Parse(X.GetCmp<ComboBox>("cmbMarqueProduit").SelectedItem.Value.ToString());
            marqueProduit.Designation = X.GetCmp<ComboBox>("cmbMarqueProduit").SelectedItem.Text;
            mClass.MarqueProduit = marqueProduit;

            ProduitGamme produitGamme = new ProduitGamme();
            produitGamme.ID = int.Parse(X.GetCmp<ComboBox>("cmbProduitGamme").SelectedItem.Value.ToString());
            produitGamme.Designation = X.GetCmp<ComboBox>("cmbProduitGamme").SelectedItem.Text;
            mClass.ProduitGamme = produitGamme;

            mClass.Code = X.GetCmp<TextField>("TxtCode").Text;
            mClass.Description = X.GetCmp<TextField>("TxtDescription").Text;

            LigneProduction ligneProduction = new LigneProduction();
            ligneProduction.ID = int.Parse(X.GetCmp<ComboBox>("cmbLigneProduction").SelectedItem.Value.ToString());
            ligneProduction.Designation = X.GetCmp<ComboBox>("cmbLigneProduction").SelectedItem.Text;
            mClass.LigneProduction = ligneProduction;

            Conditionnement conditionnement = new Conditionnement();
            conditionnement.ID = int.Parse(X.GetCmp<ComboBox>("cmbConditionnement").SelectedItem.Value.ToString());
            conditionnement.Designation = X.GetCmp<ComboBox>("cmbConditionnement").SelectedItem.Text;
            mClass.Conditionnement = conditionnement;

            ConditionnementReference conditionnementReference = new ConditionnementReference();
            conditionnementReference.ID = int.Parse(X.GetCmp<ComboBox>("cmbConditionnementReference").SelectedItem.Value.ToString());
            conditionnementReference.Reference = X.GetCmp<ComboBox>("cmbConditionnementReference").SelectedItem.Text;
            mClass.ConditionnementReference = conditionnementReference;


            mClass.CodeGTIN = X.GetCmp<TextField>("TxtCodeGS1").Text;

            mClass.NbreUniteParPalette = int.Parse(X.GetCmp<TextField>("txtNombreUnitePalette").Text.Replace(" ", ""));

            UniteDePoids uniteDePoids = new UniteDePoids();
            uniteDePoids.ID = int.Parse(X.GetCmp<ComboBox>("cmbUniteDePoids").SelectedItem.Value.ToString());
            uniteDePoids.Designation = X.GetCmp<ComboBox>("cmbUniteDePoids").SelectedItem.Text;
            mClass.UniteDePoids = uniteDePoids;

            mClass.PoidsBrutUnitaire = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrut").Text);
            if (X.GetCmp<TextField>("txtPoidsBrutTotal").Text != string.Empty) mClass.PoidsBrutTotal = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrutTotal").Text.Replace(" ", ""));



            mClass.TareUnitaireEmballage = decimal.Parse(X.GetCmp<TextField>("txtTareUnitaireEmbalage").Text.Replace(" ", ""));
            mClass.TareTotaleEmballage = int.Parse(X.GetCmp<TextField>("txtTareTotaleEmballage").Text.Replace(" ", ""));
            mClass.TarePaletteVide = int.Parse(X.GetCmp<TextField>("txtTarePalette").Text.Replace(" ", ""));
            mClass.PoidsNetTotalPalette = int.Parse(X.GetCmp<TextField>("txtPoidsNetTotalPalette").Text.Replace(" ", ""));

            mClass.BestBeforeDate = DateTime.Parse(GetFormValue("txtBestBeforeDate"));

            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }




        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Article article = JSON.Deserialize<Article>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = article.fnGet(article.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Article loading failed.");
                //(string)Session["userName"];
                article.UtilisateurModification = (string)Session["userName"];

                if (article.Desactive)
                    result = article.fnActivate();
                else
                    result = article.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Article, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeArticle");

                    ModelProxy mProxy = mstore.GetById(article.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(article);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Article : OnActivateDeactivate",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }



        public ActionResult OnCancel(string ItemSelected)
        {
            try
            {
                Article Article = JSON.Deserialize<Article>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = Article.fnGet(Article.ID);

                if (!result)
                    throw new Exception("OnCancel : Movement Sheet loading failed.");

                Article.UtilisateurModification = (string)Session["userName"];

                if (Article.Desactive)
                    result = Article.fnActivate();
                else
                    result = Article.fnDeActivate();

                if (!result)
                    throw new Exception("OnCancel : Production - Article, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeArticle");

                    ModelProxy mProxy = mstore.GetById(Article.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(Article);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - Article : Cancel",
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
                Article mClass = new Article();
                bool result = true;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtArticleId")));

                    if (mClass == null)
                        throw new Exception("UpdateFormMethod : Production - Article - load failed.");

                }

                mClass = MapFormToObject(mClass);
                result = mClass.fnUpdate();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListeArticle");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowArticle").Select(0);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormArticle").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - Article : Update",
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
            X.GetCmp<Window>("FormArticle").Close();
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