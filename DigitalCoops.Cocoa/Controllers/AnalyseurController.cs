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
using Tms.Classes.Shared.Sites;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class AnalyseurController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Analyseur
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{A99C7308-E6F4-4ED8-AA93-0D095DFB1A09}", UserName) == false)
                X.GetCmp<Button>("btnNewAnalyseur").Disable();
            else
                X.GetCmp<Button>("btnNewAnalyseur").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{0E7A9411-AA82-43B0-B33A-52A079B2B653}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportAnalyseur").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportAnalyseur").Enable();

            X.GetCmp<Hidden>("AnhiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{A99C7308-E6F4-4ED8-AA93-0D095DFB1A09}", UserName));
            X.GetCmp<Hidden>("AnhiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{7396EB36-E45D-4BA2-A7ED-0F123C2B8A1D}", UserName));            
            X.GetCmp<Hidden>("AnhiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{A68D068B-9332-4FA7-9BF8-6C9D3D4BD3D4}", UserName));
            X.GetCmp<Hidden>("AnhiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{BA41F9E3-313A-4309-9E1C-F34E1B81FDA3}", UserName));
            X.GetCmp<Hidden>("AnhiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{0E7A9411-AA82-43B0-B33A-52A079B2B653}", UserName));
            X.GetCmp<Hidden>("AnhiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{494DA064-4E17-449E-BC13-9632883555F0}", UserName));
            return View();
        }


        public ActionResult LoadAnalyseur()
        {
            Parametres mParam = new Parametres(0);
            List<DataPersist> liste = new Analyseur().fnSelectBySite(0,-1, mParam.Site);

            return this.Store(liste);
        }

        public ActionResult LoadAnalyseurBySite(int? siteID = 1)
        {
            List<DataPersist> liste = null;

            if (!siteID.HasValue)
                return this.Store(liste);

            liste = new Analyseur().fnSelectBySite(0, -1,(int)siteID);

            return this.Store(liste);
        }

        public ActionResult LoadAnalyseurAll()
        {
            List<DataPersist> liste = new Analyseur().fnSelect(0,-1);

            Analyseur analyseur = new Analyseur();

            analyseur.ID = -1;
            analyseur.Nom = "{Tous}";

            liste.Insert(0, analyseur);
            analyseur = liste[0] as Analyseur;

            return this.Store(liste);
        }

        public ActionResult LoadApprobateur()
        {
            List<DataPersist> liste = new Analyseur().fnSelect(0,1);          

            return this.Store(liste);
        }

        public ActionResult OnAdd()
        {

            AnalyseurViewModel AnalyseurVm = new AnalyseurViewModel();

            AnalyseurVm._Analyseur = new Analyseur();
            AnalyseurVm._Analyseur.ID = -1;
            AnalyseurVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAnalyseur", Model = AnalyseurVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {

            AnalyseurViewModel AnalyseurVm = new AnalyseurViewModel();

            AnalyseurVm._Analyseur = JSON.Deserialize<Analyseur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            AnalyseurVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAnalyseur", Model = AnalyseurVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {

            AnalyseurViewModel AnalyseurVm = new AnalyseurViewModel();

            AnalyseurVm._Analyseur = JSON.Deserialize<Analyseur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            AnalyseurVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAnalyseur", Model = AnalyseurVm };

        }

        [HttpPost]
        public ActionResult SubmitFormMethod(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                Analyseur mClass = new Analyseur();
                AnalyseurSite mAnalyseurSite;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(int.Parse(GetFormValue("TxtAnalyseurID")));

                    if (mClass == null || mClass.ID == 0)
                        throw new Exception("SubmitFormMethod : Analyseur load failed.");
                }

                bool result = false;
                mClass = MapFormToObject(mClass);

                _db = mClass.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = mClass.fnUpdate(mtran);

                if (result)
                {
                    List<AnalyseurSite> ItemSites = JSON.Deserialize<List<AnalyseurSite>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (ItemSites.Count > 0)
                    {
                        for (int i = 0; i < ItemSites.Count(); i++)
                        {
                            mAnalyseurSite = new AnalyseurSite();
                            mAnalyseurSite.Sites = new Site();
                            mAnalyseurSite.Analyseur = new Analyseur();

                            mAnalyseurSite.SetDataSource(_db);

                            mAnalyseurSite.Sites.ID = ItemSites[i].Sites.ID;
                            mAnalyseurSite.Analyseur.ID = mClass.ID;
                            mAnalyseurSite.IsNew = ItemSites[i].IsNew;

                            //(string)Session["userName"];
                            mAnalyseurSite.UtilisateurCreation = (string)Session["userName"];
                            mAnalyseurSite.UtilisateurModification = (string)Session["userName"];
                            if (mAnalyseurSite.IsNew)
                            {
                                result = mAnalyseurSite.fnUpdate(mtran);
                            }

                            if (!result)
                            {
                                _db.RollBackTransaction(mtran);
                                break;
                            }
                        }
                    }

                    _db.CommitTransaction(mtran);

                    Store mStore = X.GetCmp<Store>("storeListeAnalyseur");
                    //GridPanel mGrid = X.GetCmp<GridPanel>("grpListe");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                        //mGrid.Store.Insert(0,mStore);                                                  
                        X.GetCmp<RowSelectionModel>("rowSelectionAnalyseur").Select(0);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();

                        //X.GetCmp<RowSelectionModel>("rowSelectionListe").Select(mProxy);
                        //DeselectGridRows();
                    }

                    X.GetCmp<Window>("FormAnalyseur").Close();
                }
            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mtran);
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();

        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemStatus, string ItemPeutApprouver)
        {
            int status = -1;
            int peutapprouver = -1;
            if (string.IsNullOrEmpty(ItemStatus))
            {
                status = -1;
            }
            else if (ItemStatus == "true")
            {
                status = 0;
            }

            if (string.IsNullOrEmpty(ItemPeutApprouver))
            {
                peutapprouver = -1;
            }
            else if (ItemPeutApprouver == "true")
            {
                peutapprouver = 1;
            }
            var liste = new Analyseur().fnSelect(status, peutapprouver);
            
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Analyseur analyseur = JSON.Deserialize<Analyseur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = analyseur.fnGet(analyseur.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Quality Agent loading failed.");
                //(string)Session["userName"];
                analyseur.UtilisateurModification = (string)Session["userName"];

                if (analyseur.Desactive)
                    result = analyseur.fnActivate();
                else
                    result = analyseur.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Quality Agent, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeAnalyseur");

                    ModelProxy mProxy = mstore.GetById(analyseur.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(analyseur);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Quality Agent : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("AnalyseurCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus, string ItemPeutApprouver)
        {
            Store mstore = X.GetCmp<Store>("storeListeAnalyseur");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus),
                                new Ext.Net.Parameter("ItemPeutApprouver", ItemPeutApprouver)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("AnalyseurCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Analyseur MapFormToObject(Analyseur analyseur)
        {
            analyseur.Nom = X.GetCmp<TextField>("TxtNomAnalyseur").Text;
            analyseur.PeutApprouver = bool.Parse(X.GetCmp<Checkbox>("ChkAnalyseurPeutApprouver").Value.ToString());
            //(string)Session["userName"]
            analyseur.UtilisateurCreation = (string)Session["userName"];
            analyseur.UtilisateurModification = (string)Session["userName"];

            return analyseur;
        }

        public ActionResult SelectSite(string AnalyseurID)
        {
            int valueID = -1;
            try
            {
                bool result = int.TryParse(AnalyseurID, out valueID);
                if (result)
                {
                    var listeSites = new AnalyseurSite().fnSelect(valueID);

                    return this.Store(listeSites);
                }
                else
                    throw new Exception("Fournisseur : Supplier not found.");
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Fournisseur : Data Validation",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Store(null);
            }
        }

        public ActionResult onAddSite(string AnalyseurID = "")
        {
            int mID = -1;
            Analyseur mAnalyseur = new Analyseur();
            //mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            if (string.IsNullOrEmpty(AnalyseurID))
                mAnalyseur.ID = mID;
            else
                mAnalyseur.ID = int.Parse(AnalyseurID);
                        
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAnalyseur_Site", Model = mAnalyseur };
        }

        public ActionResult SelectSiteToAdd(int? ItemAnalyseur)
        {
            List<DataPersist> myList = new List<DataPersist>();
            //int mID = -1
            if (!ItemAnalyseur.HasValue)
                return this.Store(myList);

            string userName = (string)Session["userName"];

            myList = new Site().fnSelectAvailableForAnalyseur((int)ItemAnalyseur);
            Site mClass = new Site();

            if (myList.Count > 0)
                mClass = myList[0] as Site;

            return this.Store(myList);
        }

        public ActionResult RemoveSiteToAnalyseur(string ItemSelected)
        {
            try
            {
                List<AnalyseurSite> sites = JSON.Deserialize<List<AnalyseurSite>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                AnalyseurSite mClass;

                if (sites.Count() > 0)
                {
                    Store mstore = X.GetCmp<Store>("storeListeAutreSite");

                    foreach (var item in sites)
                    {
                        if (item.IsNew)
                        {
                            ModelProxy _proxy = mstore.GetById(item.ID);
                            _proxy.Drop();

                            return this.Direct();
                        }
                        else
                        {
                            mClass = new AnalyseurSite();
                            mClass.fnGet(item.ID);
                            if (mClass == null || mClass.ID == Guid.Empty)
                                throw new Exception("RemoveSiteToQualityAgent : Retirer Sites failed.");

                            mClass.UtilisateurModification = (string)Session["userName"];

                            bool result = mClass.fnRemove();

                            if (result)
                            {

                                ModelProxy mProxy = mstore.GetById(mClass.ID);

                                mProxy.Drop();

                            }
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Quality Agent : Retirer Site",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitListeSites(string ItemSelected)
        {
            List<Site> mSites = JSON.Deserialize<List<Site>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            AnalyseurSite mClass = new AnalyseurSite();
            Store store = X.GetCmp<Store>("storeListeAutreSite");

            foreach (var item in mSites)
            {
                mClass.ID = Guid.NewGuid();
                mClass.Sites = new Site();
                mClass.Sites = item;
                mClass.Sites.IsNew = true;
                item.IsNew = true;
                //item.IsNewInList = true;
                store.Insert(0, mClass);
                X.GetCmp<RowSelectionModel>("rowSiteFS").Select(0);
            }

            X.GetCmp<Window>("FormAnalyseur_Site").Close();
            return this.Direct();
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
            X.GetCmp<RowSelectionModel>("rowSelectionAnalyseur").DeselectAll();
        }


        #endregion

    }
}