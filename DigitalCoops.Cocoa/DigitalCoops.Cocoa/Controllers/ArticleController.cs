using DevExpress.XtraReports.UI;
using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tms.Classes;
using Tms.Classes.Business;
using Tms.Classes.Security;
using Tms.Classes.Shared;
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

        public ActionResult LoadByBBDate(Guid ItemArticleID)
        {
            List<DataPersist> mList = new Article().fnSelectByBBDate(ItemArticleID);
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


        public ActionResult OnDisplayArticleList()
        {
            string UserName = (string)Session["userName"];

            Site mSiteParDefaut = new Site();
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            Parametres mParam = new Parametres(0);

            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;
            if (mParam.Site == mSiteParDefaut.ID) ViewData["UrlSite"] = "LoadSiteAll";
            else ViewData["UrlSite"] = "LoadSiteByAccess";

            ViewData["Titre"] = "Liste Des Articles";
            ViewData["actionToDo"] = "OnPrintArticleList";
            ViewData["ControllerName"] = "Article";
            ViewData["SiteParDefaut"] = 1;
            ViewData["UrlSite"] = "LoadSiteByAccess";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmArticleReport", ViewData = ViewData };

        }

        [HttpPost]
        public ActionResult OnPrintArticleList()
        {
            try
            {
                XtraReport report = null;

                report = new rptArticlesList() as XtraReport;

                Session["paramTypeProduit"] = int.Parse(GetFormValue("RPcmbTypeProduit"));
                Session["paramTypeProduitText"] = X.GetCmp<ComboBox>("RPcmbTypeProduit").SelectedItem.Text;

                Session["paramProduit"] = int.Parse(GetFormValue("RPcmbProduit"));
                Session["paramProduitText"] = X.GetCmp<ComboBox>("RPcmbProduit").SelectedItem.Text;

                Session["paramMarqueProduit"] = int.Parse(GetFormValue("RPcmbMarqueProduit"));
                Session["paramMarqueProduitText"] = X.GetCmp<ComboBox>("RPcmbMarqueProduit").SelectedItem.Text;

                Session["paramStatut"] = int.Parse(GetFormValue("RPcmbStatut"));
                Session["paramStatutText"] = X.GetCmp<ComboBox>("RPcmbStatut").SelectedItem.Text;

                Session["paramActif"] = int.Parse(GetFormValue("RPcmbActif"));
                Session["paramActifText"] = X.GetCmp<ComboBox>("RPcmbActif").SelectedItem.Text;

                ViewData["Report"] = report;

                string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Article/ViewReportResult', this, 'Liste Des Articles',''),App.frmArticleForReport.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prix Journalier : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }


        }

        public ActionResult ViewReportResult()
        {
            // Créez un rapport composite qui contiendra tous les sous-rapports
            XtraReport compositeReport = new XtraReport();

            rptArticlesList report = new rptArticlesList();

            report.Parameters["paramTypeProduit"].Value = Session["paramTypeProduit"];
            report.Parameters["paramTypeProduitText"].Value = Session["paramTypeProduitText"];

            report.Parameters["paramProduit"].Value = Session["paramProduit"];
            report.Parameters["paramProduitText"].Value = Session["paramProduitText"];

            report.Parameters["paramMarqueProduit"].Value = Session["paramMarqueProduit"];
            report.Parameters["paramMarqueProduitText"].Value = Session["paramMarqueProduitText"];

            report.Parameters["paramStatut"].Value = Session["paramStatut"];
            report.Parameters["paramStatutText"].Value = Session["paramStatutText"];

            report.Parameters["paramActif"].Value = Session["paramActif"];
            report.Parameters["paramActifText"].Value = Session["paramActifText"];

            
            ViewData["Report"] = report;

            return View();
        }


        private Article MapFormToObject(Article mClass)
        {
            mClass.Nom = X.GetCmp<TextField>("TxtNom").Text;
            mClass.CodeEtendu = X.GetCmp<TextField>("TxtCodeEtendu").Text;

            ProduitFini produitFini = new ProduitFini();
            produitFini.ID = int.Parse(X.GetCmp<ComboBox>("cmbProduit").SelectedItem.Value.ToString());
            produitFini.Designation = X.GetCmp<ComboBox>("cmbProduit").SelectedItem.Text;
            mClass.ProduitFini = produitFini;

            ProduitType produitType = new ProduitType();
            produitType.ID = int.Parse(X.GetCmp<ComboBox>("cmbProtuitType").SelectedItem.Value.ToString());
            produitType.Designation = X.GetCmp<ComboBox>("cmbProtuitType").SelectedItem.Text;
            mClass.ProduitType = produitType;

            MarqueProduit marqueProduit = new MarqueProduit();
            marqueProduit.ID = int.Parse(X.GetCmp<ComboBox>("cmbMarqueProduit").SelectedItem.Value.ToString());
            marqueProduit.Designation = X.GetCmp<ComboBox>("cmbMarqueProduit").SelectedItem.Text;
            mClass.MarqueProduit = marqueProduit;

            var cmbProduitGamme = X.GetCmp<ComboBox>("cmbProduitGamme");

            // Vérification plus robuste
            if (cmbProduitGamme.SelectedItem != null &&
                cmbProduitGamme.SelectedItem.Value != null &&
                !string.IsNullOrEmpty(cmbProduitGamme.SelectedItem.Value.ToString()))
            {
                ProduitGamme produitGamme = new ProduitGamme();
                produitGamme.ID = int.Parse(cmbProduitGamme.SelectedItem.Value.ToString());
                produitGamme.Designation = cmbProduitGamme.SelectedItem.Text;
                mClass.ProduitGamme = produitGamme;
            }

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

            mClass.BestBeforeDate = int.Parse(GetFormValue("txtBestBeforeDate"));

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