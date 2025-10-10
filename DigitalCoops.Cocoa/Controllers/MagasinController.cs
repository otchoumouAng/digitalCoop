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
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class MagasinController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";
        // GET: Magasin
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);
            ViewBag.BaseUrl = mParam.Base_url;
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{25ABEE2C-7484-441F-820F-15CAC79EABCC}"), UserName);
            HashSet<Guid> mlisteFonctions = new HashSet<Guid>(mListe.Cast<Fonction>().Select(f => f.ID).ToList());

            if (mlisteFonctions.Contains(Guid.Parse("{EA2B77DE-EB38-4854-B477-C7790D59E03F}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            //if (mlisteFonctions.Contains(Guid.Parse("{77AC9A6F-BC42-42B4-9BA8-383888B35C07}")))
            //    X.GetCmp<MenuItem>("mnuPrintListMagasin").Enable();
            //else
            //    X.GetCmp<MenuItem>("mnuPrintListMagasin").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{ADFA4A17-0D14-480E-83F4-65C322CE7457}")))
                X.GetCmp<MenuItem>("mnuExportMagasin").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportMagasin").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{D9672168-001B-4933-9679-0571BF48E357}")))
                X.GetCmp<Hidden>("MghiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("MghiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{C572F154-B506-45FC-B30F-CEE3120ADECE}")))
                X.GetCmp<Hidden>("MghiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("MghiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{6CFCE80E-08AB-4F61-B353-8C0A4B1ADBEE}")))
                X.GetCmp<Hidden>("MghiddenPermActiver").SetValue(true);
            else
                X.GetCmp<Hidden>("MghiddenPermActiver").SetValue(false);
            return View();
        }

        public ActionResult LoadActiveMagasin(int siteID = -1)
        {            
            List<DataPersist> mliste = new Magasin().fnSelect(0,siteID);

            return this.Store(mliste);
        }

        public ActionResult LoadMagasin()
        {
            List<DataPersist> mList = new Magasin().fnSelect(0);

            Magasin mclass = new Magasin();

            return this.Store(mList);
        }

        public ActionResult LoadOnlyMagasinTV()
        {
            Parametres mParam = new Parametres(0);
            Magasin mMagasin = new Magasin();
            mMagasin.fnGet(mParam.MagasinTV);
            DataPersist mliste = mMagasin;
            return this.Store(mliste);
        }

        public ActionResult LoadOnlyMagasinExport()
        {
            Parametres mParam = new Parametres(0);
            Magasin mMagasin = new Magasin();
            mMagasin.fnGet(mParam.MagasinExport);
            DataPersist mliste = mMagasin;
            return this.Store(mliste);
        }

        public ActionResult LoadActiveMagasinAll(int siteID = -1)
        {
            List<DataPersist> mliste = new Magasin().fnSelect(0, siteID);

            Magasin magasin = new Magasin();

            magasin.ID = -1;
            magasin.Designation = "{Tous}";

            mliste.Insert(0, magasin);
            magasin = mliste[0] as Magasin;

            return this.Store(mliste);
        }

        public ActionResult LoadActiveMagasinWithAccess(int siteID = -1)
        {
            var mliste = new Magasin().fnSelect(0,siteID);
            List<Magasin> ListeMagasin = new List<Magasin>();

            Parametres mParam = new Parametres(0);
            Magasin magasin = new Magasin();
            string UserName = (string)Session["userName"];

            Fonction HasAccess = new Fonction();
            bool HavAccessMagasinTV = HasAccess.fnGetUserAccessStatus("{49FD7446-B3C5-4283-83F8-58DEA2AB8C1B}", UserName);
            bool HavAccessMagasinExport = HasAccess.fnGetUserAccessStatus("{ED7CA79C-A2D7-431D-A323-787341ABB8D8}", UserName);

            foreach (Magasin item in mliste)
            {
                magasin = new Magasin();
                magasin = item;
                if ((HavAccessMagasinTV && item.ID == mParam.MagasinTV) || (HavAccessMagasinExport && item.ID == mParam.MagasinExport))
                ListeMagasin.Add(magasin);
            }

            HashSet<int> val = new HashSet<int>();                                  
            return this.Store(ListeMagasin);
        }

        public ActionResult OnAdd()
        {
            MagasinViewModel MagasinVm = new MagasinViewModel();

            MagasinVm._Magasin = new Magasin();
            MagasinVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMagasin", Model = MagasinVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            MagasinViewModel MagasinVm = new MagasinViewModel();

            MagasinVm._Magasin = JSON.Deserialize<Magasin>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            MagasinVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMagasin", Model = MagasinVm };
        }

        public ActionResult OnConsult(string ItemSelected)
        {
            MagasinViewModel MagasinVm = new MagasinViewModel();

            MagasinVm._Magasin = JSON.Deserialize<Magasin>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            MagasinVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMagasin", Model = MagasinVm };
        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Magasin Magasin = new Magasin();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    Magasin.IsNew = true;
                else
                {
                    Magasin.IsNew = false;

                    Magasin.fnGet(int.Parse(GetFormValue("TxtMagasinID")));

                    if (Magasin == null || Magasin.ID == 0)
                        throw new Exception("SubmitFormMethod : Magasin introuvable, veuillez réessayer svp !");
                }

                Magasin = MapFormToObject(Magasin);

                bool result = Magasin.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeMagasin");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, Magasin);
                        X.GetCmp<RowSelectionModel>("rowMagasin").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(Magasin.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(Magasin);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormMagasin").Close();                    
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Magasin : Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemStatut, string ItemSite)
        {
            int statut = string.IsNullOrEmpty(ItemStatut) ? -1 : int.Parse(ItemStatut);
            int siteID = string.IsNullOrEmpty(ItemSite) ? -1 : int.Parse(ItemSite);
            var liste = new Magasin().fnSelect(statut,siteID);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Magasin Magasin = JSON.Deserialize<Magasin>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = Magasin.fnGet(Magasin.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Magasin introuvable, veuillez réessayer svp !");
                //(string)Session["userName"];
                Magasin.UtilisateurModification = (string)Session["userName"];

                if (Magasin.Desactive)
                    result = Magasin.fnActivate();
                else
                    result = Magasin.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Echec de l'operation, veuillez réessayer svp");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeMagasin");

                    ModelProxy mProxy = mstore.GetById(Magasin.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(Magasin);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Magasin : Annuler",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("MagasinCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatut, string ItemSite)
        {
            Store mstore = X.GetCmp<Store>("storeListeMagasin");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatut", ItemStatut),
                                new Ext.Net.Parameter("ItemSite", ItemSite)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("MagasinCP");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Magasin MapFormToObject(Magasin mMagasin)
        {
            mMagasin.Designation = X.GetCmp<TextField>("TxtDesignation").Text;
            mMagasin.Localisation = X.GetCmp<TextField>("TxtLocalisation").Text;
            mMagasin.StockType = new StockType();
            mMagasin.StockType.ID = int.Parse(X.GetCmp<ComboBox>("cmbTypeStock").SelectedItem.Value.ToString());
            mMagasin.StockType.Designation = X.GetCmp<ComboBox>("cmbTypeStock").SelectedItem.Text;

            mMagasin.Sites = new Site();
            mMagasin.Sites.ID = int.Parse(X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Value.ToString());
            mMagasin.Sites.Nom = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text;

            mMagasin.EstExterne = bool.Parse(X.GetCmp<Checkbox>("ChkEstExterne").Value.ToString());
            mMagasin.EstTransit = bool.Parse(X.GetCmp<Checkbox>("ChkEstTransit").Value.ToString());
            mMagasin.APontBascule = bool.Parse(X.GetCmp<Checkbox>("ChkPontBascule").Value.ToString());

            mMagasin.UtilisateurCreation = (string)Session["userName"];
            mMagasin.UtilisateurModification = (string)Session["userName"];

            return mMagasin;
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
            X.GetCmp<RowSelectionModel>("rowMagasin").DeselectAll();
        }

        private int GetCriteriaValue(string strComponent)
        {
            int value;

            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
                return value;
            else
                return -1;
        }
        #endregion

        //public ActionResult LoadActiveMagasinStock(int siteID = 1)
        //{
        //    List<DataPersist> mliste = new Magasin().fnSelectForStock(siteID);

        //    return this.Store(mliste);
        //}
    }
}