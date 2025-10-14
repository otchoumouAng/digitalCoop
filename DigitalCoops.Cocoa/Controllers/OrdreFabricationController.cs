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
using Tms.Classes.Shared.Sales;
using Tms.Classes.Business;

namespace Tms2017.MVC.Controllers
{
    public class OrdreFabricationController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: OrdreFabrication
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{5d7e085f-99a4-4e48-a688-25e6e0845b5f}", UserName) == false)
                X.GetCmp<Button>("btnNewOrdreFabrication").Disable();
            else
                X.GetCmp<Button>("btnNewOrdreFabrication").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{9da33bde-06cb-484e-a8a4-b3b374429fed}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListOrdreFabrication").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListOrdreFabrication").Enable();

            X.GetCmp<Hidden>("OrdreFabricationhiddenPermOuvrir").SetValue(HasAccessFunction.fnGetUserAccessStatus("{c16dd72b-d925-4a7e-b5f0-d7d142be879c}", UserName));
            X.GetCmp<Hidden>("OrdreFabricationhiddenPermFermer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{c16dd72b-d925-4a7e-b5f0-d7d142be879c}", UserName));
            X.GetCmp<Hidden>("OrdreFabricationhiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{5d7e085f-99a4-4e48-a688-25e6e0845b5f}", UserName));
            X.GetCmp<Hidden>("OrdreFabricationhiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{c16dd72b-d925-4a7e-b5f0-d7d142be879c}", UserName));


            X.GetCmp<Hidden>("OrdreFabricationhiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{1f4af822-9de1-490d-bc24-1aa742f18b4e}", UserName));
            X.GetCmp<Hidden>("OrdreFabricationhiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{11611511-163f-44ca-8200-7c8bc3e79f74}", UserName));
            X.GetCmp<Hidden>("OrdreFabricationhiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{9da33bde-06cb-484e-a8a4-b3b374429fed}", UserName));
            X.GetCmp<Hidden>("OrdreFabricationhiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{bc7ec95d-ddc3-4441-8f45-2b1d20d94a23}", UserName));

            return View();
        }

        public ActionResult LoadOrdreFabrication()
        {
            List<DataPersist> mList = new OrdreFabrication().fnSelect(0);

            OrdreFabrication mclass = new OrdreFabrication();

            if (mList.Count > 0)
                mclass = mList[0] as OrdreFabrication;

            return this.Store(mList);
        }


        public ActionResult LoadOrdreFabricationAll()
        {
            List<DataPersist> mList = new OrdreFabrication().fnSelect(0);

            OrdreFabrication mclass = new OrdreFabrication();

            mclass.Statut = "-1";
            mclass.NumeroProduction = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as OrdreFabrication;

            return this.Store(mList);
        }

        public ActionResult LoadAllOrdreFabricationByAccess()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAllAccess = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            List<DataPersist> mList = null;
            OrdreFabrication mclass = new OrdreFabrication();

            if (HasAllAccess)
            {
                mList = new OrdreFabrication().fnSelect(0);

                mclass.Statut = "-1";
                mclass.NumeroProduction = "{Tous}";

                mList.Insert(0, mclass);
                mclass = mList[0] as OrdreFabrication;
            }
            else
            {
                bool result = mclass.fnGetByUserName(UserName);
                mList = new List<DataPersist>();

                mList.Insert(0, mclass);
                mclass = mList[0] as OrdreFabrication;
            }
            //= new OrdreFabrication().fnSelect(0);

            return this.Store(mList);

        }

        public ActionResult LoadOrdreFabricationByAccess()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAllAccess = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            List<DataPersist> mList = null;
            OrdreFabrication mclass = new OrdreFabrication();

            if (HasAllAccess)
            {
                mList = new OrdreFabrication().fnSelect(0);
                mclass = mList[0] as OrdreFabrication;
            }
            else
            {
                bool result = mclass.fnGetByUserName(UserName);
                mList = new List<DataPersist>();

                mList.Insert(0, mclass);
                mclass = mList[0] as OrdreFabrication;
            }
            //= new OrdreFabrication().fnSelect(0);

            return this.Store(mList);

        }

        public ActionResult LoadSpecificOrdreFabrication()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool CanConsultOverOrdreFabrication = HasAccessFunction.fnGetUserAccessStatus("{17a9e19c-4667-4c65-b173-3749ab8c263e}", UserName);
            List<DataPersist> mList = null;
            OrdreFabrication mclass = new OrdreFabrication();

            bool result = mclass.fnGetByUserName(UserName);
            mList = new List<DataPersist>();

            mList.Insert(0, mclass);
            mclass = mList[0] as OrdreFabrication;

            if (CanConsultOverOrdreFabrication)
            {
                mclass = new OrdreFabrication();
                result = mclass.fnGetDefaultOrdreFabrication();
                mList.Insert(1, mclass);

                mclass = new OrdreFabrication();
                mclass.Statut = "-1";
                mclass.NumeroProduction = "{Tous}";

                mList.Insert(0, mclass);
                mclass = mList[0] as OrdreFabrication;
            }

            //= new OrdreFabrication().fnSelect(0);

            return this.Store(mList);

        }


        public ActionResult SelectToPrice(string ItemPrice)
        {
            Guid id = Guid.Empty;
            if (!string.IsNullOrEmpty(ItemPrice) && (Guid.Parse(ItemPrice) != Guid.Empty))
            {
                id = Guid.Parse(ItemPrice);
                var listeOrdreFabrication = new OrdreFabrication().fnSelectToPrice(id);
                return this.Store(listeOrdreFabrication);
            }
            else
            {
                var listAllOrdreFabrication = new OrdreFabrication().fnSelect(0);
                return this.Store(listAllOrdreFabrication);
            }
        }

        public ActionResult SelectToPriceGood()
        {
            string UserName = (string)Session["userName"];
            var listeOrdreFabrication = new OrdreFabrication().fnSelectAvailableForPrice(UserName);
            return this.Store(listeOrdreFabrication);

        }
        //public ActionResult SelectToPrice(string ItemOrdreFabrication)
        //{
        //    if (!string.IsNullOrEmpty(ItemOrdreFabrication))
        //    {
        //        int id = Int32.Parse(ItemOrdreFabrication);
        //        var listeOrdreFabrication = new OrdreFabrication().fnSelectToPrice(id);
        //        return this.Store(listeOrdreFabrication);
        //    }
        //    else
        //    {
        //        var listAllOrdreFabrication = new OrdreFabrication().fnSelect(0);
        //        return this.Store(listAllOrdreFabrication);
        //    }
        //}

        public ActionResult OnAdd()
        {


            OrdreFabricationViewModel OrdreFabricationVm = new OrdreFabricationViewModel();

            OrdreFabricationVm._OrdreFabrication = new OrdreFabrication();
            OrdreFabricationVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormOrdreFabrication", Model = OrdreFabricationVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {


            OrdreFabricationViewModel OrdreFabricationVm = new OrdreFabricationViewModel();

            OrdreFabricationVm._OrdreFabrication = JSON.Deserialize<OrdreFabrication>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            OrdreFabricationVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormOrdreFabrication", Model = OrdreFabricationVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {


            OrdreFabricationViewModel OrdreFabricationVm = new OrdreFabricationViewModel();

            OrdreFabricationVm._OrdreFabrication = JSON.Deserialize<OrdreFabrication>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            OrdreFabricationVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormOrdreFabrication", Model = OrdreFabricationVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                OrdreFabrication OrdreFabrication = new OrdreFabrication();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    OrdreFabrication.IsNew = true;
                else
                {
                    OrdreFabrication.IsNew = false;
                    Guid productionID = Guid.Empty;
                    bool IsGuid = Guid.TryParse(GetFormValue("TxtOrdreFabricationID"), out productionID);

                    if (IsGuid)
                        OrdreFabrication.fnGet(productionID);
                    else
                        throw new Exception("SubmitFormMethod : Production Order load failed.");


                    if (OrdreFabrication == null)
                        throw new Exception("SubmitFormMethod : Location, load failed.");
                }

                OrdreFabrication = MapFormToObject(OrdreFabrication);

                bool result = OrdreFabrication.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeOrdreFabrication");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, OrdreFabrication);
                        X.GetCmp<RowSelectionModel>("rowSelectionOrdreFabrication").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(OrdreFabrication.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(OrdreFabrication);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormOrdreFabrication").Close();
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

        public ActionResult Select(StoreRequestParameters parameters, int annee, int semaine, int produitID, int typeProduitID, string articleID, string statut, int actifState)
        {
            Guid? articleGuid = null;
            if (!string.IsNullOrEmpty(articleID) && Guid.TryParse(articleID, out Guid parsedGuid))
            {
                articleGuid = parsedGuid;
            }

            var liste = new OrdreFabrication().fnSelect(annee, semaine, produitID, typeProduitID, articleGuid, statut, actifState);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                OrdreFabrication OrdreFabrication = JSON.Deserialize<OrdreFabrication>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = OrdreFabrication.fnGet(OrdreFabrication.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Location, loading failed.");
                //(string)Session["userName"];
                OrdreFabrication.UtilisateurModification = (string)Session["userName"];

                if (OrdreFabrication.Desactive)
                    result = OrdreFabrication.fnActivate();
                else
                    result = OrdreFabrication.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Location, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeOrdreFabrication");

                    ModelProxy mProxy = mstore.GetById(OrdreFabrication.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(OrdreFabrication);

                    mProxy.Commit();

                    mProxy.EndEdit();
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
            FormPanel mform = X.GetCmp<FormPanel>("OrdreFabricationCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult onCloseProduction(string ItemSelected)
        {
            OrdreFabrication mclass = new OrdreFabrication();
            mclass = JSON.Deserialize<OrdreFabrication>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormCloseOrdreFabrication", Model = mclass, };
        }

        public ActionResult onOpenProduction(string ItemSelected)
        {
            OrdreFabrication mclass = new OrdreFabrication();
            mclass = JSON.Deserialize<OrdreFabrication>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormOpenOrdreFabrication", Model = mclass, };
        }

        public ActionResult CloseProduction()
        {
            try
            {
                string id = GetFormValue("TxtOrdreFabricationID");
                string dateFin = GetFormValue("txtDateFinProduction");
                string comment = GetFormValue("txtComment");

                OrdreFabrication mProduction = new OrdreFabrication();
                mProduction.fnGet(Guid.Parse(id));

                mProduction.DateFinProduction = DateTime.Parse(dateFin);
                mProduction.Commentaire = comment;

                bool result = mProduction.fnClose();

                

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeOrdreFabrication");
                    ModelProxy mProxy = mstore.GetById(mProduction.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mProduction);

                    mProxy.Commit();

                    mProxy.EndEdit();

                    X.GetCmp<Window>("FormCloseOrdreFabrication").Close();
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

        public ActionResult OpenProduction()
        {
            try
            {
                string id = GetFormValue("TxtOrdreProductionID");
                string dateDebut = GetFormValue("txtDateDebutProduction");
                string dateFin = GetFormValue("txtDateFinProduction");
                string comment = GetFormValue("txtComment");

                OrdreFabrication mProduction = new OrdreFabrication();

                mProduction.fnGet(Guid.Parse(id));

                mProduction.DateFinProduction = DateTime.Parse(dateFin);
                mProduction.Commentaire = comment;

                bool result = mProduction.fnOpen();

                

                if (result)
                {
                    Palette Palette = new Palette();
                    Palette.fnAutoCreation(mProduction);
                    
                    Store mstore = X.GetCmp<Store>("storeListeOrdreFabrication");
                    ModelProxy mProxy = mstore.GetById(mProduction.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mProduction);

                    mProxy.Commit();

                    mProxy.EndEdit();

                    X.GetCmp<Window>("FormOpenOrdreProduction").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production Order : Open",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeOrdreFabrication");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("OrdreFabricationCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        //private OrdreFabrication MapFormToObject(OrdreFabrication OrdreFabrication)
        //{
        //    OrdreFabrication.NumeroProduction = X.GetCmp<TextField>("TxtNumeroProduction").Text;

        //    OrdreFabrication.Article = null;
        //    if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbDefautArticle").Text))
        //    {
        //        OrdreFabrication.Article = new Articles();
        //        OrdreFabrication.Article.ID = int.Parse(X.GetCmp<ComboBox>("CmbDefautArticle").SelectedItem.Value);
        //        OrdreFabrication.Article.Designation = X.GetCmp<ComboBox>("CmbDefautArticle").SelectedItem.Text;
        //    }


        //    //(string)Session["userName"]
        //    OrdreFabrication.UtilisateurCreation = (string)Session["userName"];
        //    OrdreFabrication.UtilisateurModification = (string)Session["userName"];

        //    return OrdreFabrication;
        //}


        private OrdreFabrication MapFormToObject(OrdreFabrication obj)
        {
            // Numéro de production (readonly ou auto‑généré)
            obj.NumeroProduction = X.GetCmp<TextField>("txtNumeroProduction").Text;

            // Article
            var cmbArticle = X.GetCmp<ComboBox>("cmbArticle");
            if (!string.IsNullOrEmpty(cmbArticle.Text))
            {
                obj.Article = new Article
                {
                    ID = Guid.Parse(cmbArticle.SelectedItem.Value),
                    Nom = cmbArticle.SelectedItem.Text
                };
            }
            else
            {
                obj.Article = null;
            }

            // Ligne de production
            var cmbLigne = X.GetCmp<ComboBox>("cmbLigneProduction");
            if (!string.IsNullOrEmpty(cmbLigne.Text))
            {
                obj.LigneDeProduction = new LigneProduction
                {
                    ID = int.Parse(cmbLigne.SelectedItem.Value),
                    Designation = cmbLigne.SelectedItem.Text
                };
            }

            // Client
            var cmbClient = X.GetCmp<ComboBox>("cmbClient");
            if (!string.IsNullOrEmpty(cmbClient.Text))
            {
                obj.Client = new Client
                {
                    ID = int.Parse(cmbClient.SelectedItem.Value),
                    Nom = cmbClient.SelectedItem.Text
                };
            }

            // Récolte
            var cmbRecolte = X.GetCmp<ComboBox>("cmbRecolte");
            if (!string.IsNullOrEmpty(cmbRecolte.Text))
            {
                obj.Recolte = new Recolte
                {
                    ID = int.Parse(cmbRecolte.SelectedItem.Value),
                    Designation = cmbRecolte.SelectedItem.Text,

                };
            }

            //obj.RecolteDesignation = cmbRecolte.SelectedItem.Text;

            

            // Année & Semaine
            obj.Annee = int.Parse(X.GetCmp<NumberField>("txtAnnee").Text);
            obj.Semaine = int.Parse(X.GetCmp<NumberField>("txtSemaine").Text);

            // Référence externe
            obj.ReferenceExterne = X.GetCmp<TextField>("txtReferenceExterne").Text;

            // Nombre de palettes à produire
            obj.NbrePaletteAProduire = int.Parse(X.GetCmp<NumberField>("txtNbrePaletteAProduire").Text);

            // Dates
            obj.DateEffective = DateTime.Parse(X.GetCmp<DateField>("txtDateEffective").RawText);
            obj.DateDebutProduction = DateTime.Parse(X.GetCmp<DateField>("txtDateDebutProduction").RawText);
            obj.DateFinProduction = DateTime.Parse(X.GetCmp<DateField>("txtDateFinProduction").RawText);

            // Statut (lecture seule)
            obj.Statut = X.GetCmp<TextField>("txtStatut").Text;

            // Utilisateurs
            obj.UtilisateurCreation = (string)Session["userName"];
            obj.UtilisateurModification = (string)Session["userName"];

            return obj;
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
            X.GetCmp<RowSelectionModel>("rowSelectionOrdreFabrication").DeselectAll();
        }


        #endregion

    }
}