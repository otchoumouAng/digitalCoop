using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Security;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class LotTypeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";
        // GET: LotType
        public ActionResult Index()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{E76FC163-4FA1-4A70-9E5B-F755FF1B6BD7}"), UserName);
            HashSet<Guid> mlisteFonctions = new HashSet<Guid>(mListe.Cast<Fonction>().Select(f => f.ID).ToList());

            if (mlisteFonctions.Contains(Guid.Parse("{C28ADC27-43B2-4825-B178-F57BF3B5A273}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            //if (mlisteFonctions.Contains(Guid.Parse("{86A1B5C4-910B-4EDF-9B88-CAAE62E5553B}")))
            //    X.GetCmp<MenuItem>("mnuPrintListLotType").Enable();
            //else
            //    X.GetCmp<MenuItem>("mnuPrintListLotType").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{E6F31EF1-94C6-4D04-B1D4-F6C5E2451C46}")))
                X.GetCmp<MenuItem>("mnuExportLotType").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportLotType").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{44E2AF7F-BFE9-4B8D-905D-328B70F8F21B}")))
                X.GetCmp<Hidden>("LthiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("LthiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{712E88F5-EB0D-44FA-B450-547A7B0A2C33}")))
                X.GetCmp<Hidden>("LthiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("LthiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{9B0B2326-6029-487B-8A1E-71BAFC5FF55E}")))
                X.GetCmp<Hidden>("LthiddenPermActiver").SetValue(true);
            else
                X.GetCmp<Hidden>("LthiddenPermActiver").SetValue(false);
            return View();
        }

        public ActionResult LoadActiveLotType()
        {            
            List<DataPersist> mliste = new LotType().fnSelect(0);

            return this.Store(mliste);
        }

        public ActionResult LoadActiveLotTypeAll()
        {            
            List<DataPersist> mliste = new LotType().fnSelect(0);

            LotType lType = new LotType();

            lType.ID = -1;
            lType.Designation = "{Tous}";

            mliste.Insert(0, lType);
            lType = mliste[0] as LotType;

            return this.Store(mliste);
        }

        public ActionResult OnAdd()
        {
            LotTypeViewModel LotTypeVm = new LotTypeViewModel();

            LotTypeVm._LotType = new LotType();
            LotTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLotType", Model = LotTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            LotTypeViewModel LotTypeVm = new LotTypeViewModel();

            LotTypeVm._LotType = JSON.Deserialize<LotType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            LotTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLotType", Model = LotTypeVm };
        }

        public ActionResult OnConsult(string ItemSelected)
        {
            LotTypeViewModel LotTypeVm = new LotTypeViewModel();

            LotTypeVm._LotType = JSON.Deserialize<LotType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            LotTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLotType", Model = LotTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {
            try
            {
                LotType LotType = new LotType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    LotType.IsNew = true;
                else
                {
                    LotType.IsNew = false;

                    LotType.fnGet(int.Parse(GetFormValue("TxtLotTypeID")));

                    if (LotType == null || LotType.ID == 0)
                        throw new Exception("SubmitFormMethod : Type Of Lot load failed.");
                }

                LotType = MapFormToObject(LotType);

                bool result = LotType.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeLotType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, LotType);
                        X.GetCmp<RowSelectionModel>("rowLotType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(LotType.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(LotType);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormLotType").Close();                                        
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type Of Lot : Data Validation",
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
            var liste = new LotType().fnSelect(statut);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                LotType LotType = JSON.Deserialize<LotType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = LotType.fnGet(LotType.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type Of Lot loading failed.");
                //(string)Session["userName"];
                LotType.UtilisateurModification = (string)Session["userName"];

                if (LotType.Desactive)
                    result = LotType.fnActivate();
                else
                    result = LotType.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type Of Lot, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeLotType");

                    ModelProxy mProxy = mstore.GetById(LotType.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(LotType);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type Of Lot : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("LotTypeCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatut)
        {
            Store mstore = X.GetCmp<Store>("storeListeLotType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatut", ItemStatut)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("LotTypeCP");

            mform.Collapsed = true;

            return this.Direct();
        }

        private LotType MapFormToObject(LotType mlotType)
        {
            mlotType.Designation = X.GetCmp<TextField>("TxtDesignation").Text;

            if (X.GetCmp<TextField>("TxtNombreSacs").Text != string.Empty) mlotType.NombreSacs = int.Parse(X.GetCmp<TextField>("TxtNombreSacs").Text);
            if (X.GetCmp<TextField>("TxtNombrePalettes").Text != string.Empty) mlotType.NombrePalette = int.Parse(X.GetCmp<TextField>("TxtNombrePalettes").Text);
            if (X.GetCmp<TextField>("TxtNombreSacsParPalettes").Text != string.Empty) mlotType.NombreSacsParPalettes = int.Parse(X.GetCmp<TextField>("TxtNombreSacsParPalettes").Text);
            if (X.GetCmp<TextField>("TxtPoidsStandard").Text != string.Empty) mlotType.PoidsStandard = decimal.Parse(X.GetCmp<TextField>("TxtPoidsStandard").Text);
            if (X.GetCmp<TextField>("TxtPoidsMin").Text != string.Empty) mlotType.PoidsMin = decimal.Parse(X.GetCmp<TextField>("TxtPoidsMin").Text);
            if (X.GetCmp<TextField>("TxtPoidsMax").Text != string.Empty) mlotType.PoidsMax = decimal.Parse(X.GetCmp<TextField>("TxtPoidsMax").Text);
            if (X.GetCmp<NumberField>("TxtTareSacsUnitaire").RawText != string.Empty) mlotType.TareSacsUnitaire = decimal.Parse(X.GetCmp<NumberField>("TxtTareSacsUnitaire").RawText);

            mlotType.Prefixe = X.GetCmp<TextField>("TxtPrefixe").Text;

            mlotType.UtilisateurCreation = (string)Session["userName"];
            mlotType.UtilisateurModification = (string)Session["userName"];

            return mlotType;
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
            X.GetCmp<RowSelectionModel>("rowLotType").DeselectAll();
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