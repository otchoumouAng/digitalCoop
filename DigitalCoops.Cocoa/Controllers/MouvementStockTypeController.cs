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
    public class MouvementStockTypeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: MouvementStockType
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);
            ViewBag.BaseUrl = mParam.Base_url;
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{689A24F9-A964-4A89-B0E9-A76EEDA68EB1}"), UserName);
            HashSet<Guid> mlisteFonctions = new HashSet<Guid>(mListe.Cast<Fonction>().Select(f => f.ID).ToList());

            if (mlisteFonctions.Contains(Guid.Parse("{C6F8E469-0B26-4A81-AC5C-B7FBBB7148CD}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            //if (mlisteFonctions.Contains(Guid.Parse("{9154A19B-C037-4E02-B820-28F33D69B3E2}")))
            //    X.GetCmp<MenuItem>("mnuPrintListMouvementStockType").Enable();
            //else
            //    X.GetCmp<MenuItem>("mnuPrintListMouvementStockType").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{781A4C8E-B2BF-4CAE-B9BE-8D57EC9B8D89}")))
                X.GetCmp<MenuItem>("mnuExportMouvementStockType").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportMouvementStockType").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{1CA36541-C343-4F85-80F4-789AEB58B542}")))
                X.GetCmp<Hidden>("MvthiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("MvthiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{33752706-CEFE-4228-B8A1-FD458DC6C7FD}")))
                X.GetCmp<Hidden>("MvthiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("MvthiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{AF15013B-C7CC-4CF2-9F2C-F376917EBF3C}")))
                X.GetCmp<Hidden>("MvthiddenPermActiver").SetValue(true);
            else
                X.GetCmp<Hidden>("MvthiddenPermActiver").SetValue(false);
            return View();
        }

        public ActionResult LoadActiveMouvementStockType(string SensMouvement = "")
        {
            int sens = string.IsNullOrEmpty(SensMouvement) ? -2 :int.Parse(SensMouvement);
            List<DataPersist> mliste = new MouvementStockType().fnSelect(0, sens, null);

            return this.Store(mliste);
        }

        public ActionResult LoadActiveMouvementStockTypeManual(string SensMouvement = "")
        {
            int sens = string.IsNullOrEmpty(SensMouvement) ? -2 : int.Parse(SensMouvement);
            List<DataPersist> mliste = new MouvementStockType().fnSelect(0, sens, true);

            return this.Store(mliste);
        }

        public ActionResult LoadActiveMouvementStockTypeManualAll(string SensMouvement = "")
        {
            int sens = string.IsNullOrEmpty(SensMouvement) ? -2 : int.Parse(SensMouvement);
            List<DataPersist> mliste = new MouvementStockType().fnSelect(0, sens, true);
            MouvementStockType mvt = new MouvementStockType();
            if (sens == -2)
            {
                mvt.ID = -2;
                mvt.Designation = "{Tous}";

                mliste.Insert(0, mvt);
                mvt = mliste[0] as MouvementStockType;
            }            
            return this.Store(mliste);
        }

        public ActionResult LoadActiveMouvementStockTypeAll(string SensMouvement = "", string ItemMouvementStockType = "", string ItemMagasin = "", string ItemVisibleSurSite = "")
        {
            int sens = string.IsNullOrEmpty(SensMouvement) ? -2 : int.Parse(SensMouvement);
            int EstVisibleSurSite = string.IsNullOrEmpty(ItemVisibleSurSite) ? -1 : int.Parse(ItemVisibleSurSite);
            int MouvementStockTypeID = string.IsNullOrEmpty(ItemMouvementStockType) ? -1 : int.Parse(ItemMouvementStockType);
            int MagasinID = string.IsNullOrEmpty(ItemMagasin) ? -1 : int.Parse(ItemMagasin);
            List<DataPersist> mliste = new MouvementStockType().fnSelect(0, sens, null, MouvementStockTypeID,MagasinID, EstVisibleSurSite);

            MouvementStockType mvt = new MouvementStockType();

            mvt.ID = -2;
            mvt.Designation = "{Tous}";

            mliste.Insert(0, mvt);
            mvt = mliste[0] as MouvementStockType;

            return this.Store(mliste);
        }

        public ActionResult OnAdd()
        {
            MouvementStockTypeViewModel MouvementStockTypeVm = new MouvementStockTypeViewModel();

            MouvementStockTypeVm._MouvementStockType = new MouvementStockType();
            MouvementStockTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMouvementStockType", Model = MouvementStockTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            MouvementStockTypeViewModel MouvementStockTypeVm = new MouvementStockTypeViewModel();

            MouvementStockTypeVm._MouvementStockType = JSON.Deserialize<MouvementStockType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            MouvementStockTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMouvementStockType", Model = MouvementStockTypeVm };
        }

        public ActionResult OnConsult(string ItemSelected)
        {
            MouvementStockTypeViewModel MouvementStockTypeVm = new MouvementStockTypeViewModel();

            MouvementStockTypeVm._MouvementStockType = JSON.Deserialize<MouvementStockType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            MouvementStockTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMouvementStockType", Model = MouvementStockTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                MouvementStockType MouvementStockType = new MouvementStockType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    MouvementStockType.IsNew = true;
                else
                {
                    MouvementStockType.IsNew = false;

                    MouvementStockType.fnGet(int.Parse(GetFormValue("TxtMouvementStockTypeID")));

                    if (MouvementStockType == null || MouvementStockType.ID == 0)
                        throw new Exception("SubmitFormMethod : Type Of Stock Transaction load failed.");
                }

                MouvementStockType = MapFormToObject(MouvementStockType);

                bool result = MouvementStockType.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeMouvementStockType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, MouvementStockType);
                        X.GetCmp<RowSelectionModel>("rowMouvementStockType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(MouvementStockType.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(MouvementStockType);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormMouvementStockType").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type Of Stock Transaction : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemStatut)
        {
            int statut = string.IsNullOrEmpty(ItemStatut) ? -1 : int.Parse(ItemStatut);
            var liste = new MouvementStockType().fnSelect(statut);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                MouvementStockType MouvementStockType = JSON.Deserialize<MouvementStockType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = MouvementStockType.fnGet(MouvementStockType.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type Of Stock Transaction loading failed.");
                //(string)Session["userName"];
                MouvementStockType.UtilisateurModification = (string)Session["userName"];

                if (MouvementStockType.Desactive)
                    result = MouvementStockType.fnActivate();
                else
                    result = MouvementStockType.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type Of Stock Transaction, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeMouvementStockType");

                    ModelProxy mProxy = mstore.GetById(MouvementStockType.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(MouvementStockType);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type Of Stock Transaction : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("MouvementStockTypeCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatut)
        {
            Store mstore = X.GetCmp<Store>("storeListeMouvementStockType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatut", ItemStatut)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("MouvementStockTypeCP");

            mform.Collapsed = true;

            return this.Direct();
        }

        private MouvementStockType MapFormToObject(MouvementStockType mMouvementStockType)
        {
            mMouvementStockType.Designation = X.GetCmp<TextField>("TxtDesignation").Text;
            
            mMouvementStockType.StockType = new StockType();
            mMouvementStockType.StockType.ID = int.Parse(X.GetCmp<ComboBox>("cmbTypeStock").SelectedItem.Value.ToString());
            mMouvementStockType.StockType.Designation = X.GetCmp<ComboBox>("cmbTypeStock").SelectedItem.Text;

            mMouvementStockType.Sens = int.Parse(X.GetCmp<ComboBox>("cmbSens").SelectedItem.Value.ToString());
            mMouvementStockType.EstAuto = bool.Parse(X.GetCmp<Checkbox>("ChkEstAuto").Value.ToString());

            mMouvementStockType.VisibleEnAgence = bool.Parse(X.GetCmp<Checkbox>("TxtVisibilityOnSite").Value.ToString());

            mMouvementStockType.UtilisateurCreation = (string)Session["userName"];
            mMouvementStockType.UtilisateurModification = (string)Session["userName"];

            return mMouvementStockType;
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
            X.GetCmp<RowSelectionModel>("rowMouvementStockType").DeselectAll();
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

    }
}