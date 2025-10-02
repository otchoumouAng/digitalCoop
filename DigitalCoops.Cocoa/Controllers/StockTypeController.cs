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
    public class StockTypeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: StockType
        public ActionResult Index()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{7DB356FB-518B-431A-BDF2-2F7951DBE87E}"), UserName);
            HashSet<Guid> mlisteFonctions = new HashSet<Guid>(mListe.Cast<Fonction>().Select(f => f.ID).ToList());

            if (mlisteFonctions.Contains(Guid.Parse("{EB67A73C-1528-4362-91CB-CDC98C0DBFB3}")))
                X.GetCmp<Button>("btnNewStockType").Enable();
            else
                X.GetCmp<Button>("btnNewStockType").Disable();

            //if (mlisteFonctions.Contains(Guid.Parse("{8FF93261-7468-4583-8CC3-56CE8A09F7C1}")))
            //    X.GetCmp<MenuItem>("mnuPrintMovement").Enable();
            //else
            //    X.GetCmp<MenuItem>("mnuPrintMovement").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{68557587-0FD3-4FBF-87A5-CC15A2AEC741}")))
                X.GetCmp<MenuItem>("mnuExportStockType").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportStockType").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{FF242B7B-9E59-448F-B980-067E55CB8F3F}")))
                X.GetCmp<Hidden>("SthiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("SthiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{E5DF4A15-BC3A-4461-86B1-B159B8FDBD82}")))
                X.GetCmp<Hidden>("SthiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("SthiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{5C4F51EA-0726-48DE-B45F-278D53FD8682}")))
                X.GetCmp<Hidden>("SthiddenPermActiver").SetValue(true);
            else
                X.GetCmp<Hidden>("SthiddenPermActiver").SetValue(false);
            return View();
        }

        public ActionResult LoadActiveStockType()
        {           
            List<DataPersist> mliste = new StockType().fnSelect(0);
            return this.Store(mliste);
        }

        public ActionResult LoadAllActiveStockType()
        {
            List<DataPersist> mliste = new StockType().fnSelect(0);

            StockType StockType = new StockType();

            StockType.ID = -1;
            StockType.Designation = "{Tous}";

            mliste.Insert(0, StockType);
            StockType = mliste[0] as StockType;

            return this.Store(mliste);
        }

        public ActionResult OnAdd()
        {
            StockTypeViewModel StockTypeVm = new StockTypeViewModel();

            StockTypeVm._StockType = new StockType();
            StockTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormStockType", Model = StockTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            StockTypeViewModel StockTypeVm = new StockTypeViewModel();

            StockTypeVm._StockType = JSON.Deserialize<StockType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            StockTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormStockType", Model = StockTypeVm };
        }

        public ActionResult OnConsult(string ItemSelected)
        {
            StockTypeViewModel StockTypeVm = new StockTypeViewModel();

            StockTypeVm._StockType = JSON.Deserialize<StockType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            StockTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormStockType", Model = StockTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                StockType StockType = new StockType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    StockType.IsNew = true;
                else
                {
                    StockType.IsNew = false;

                    StockType.fnGet(int.Parse(GetFormValue("TxtStockTypeID")));

                    if (StockType == null || StockType.ID == 0)
                        throw new Exception("SubmitFormMethod : Stock Type load failed.");
                }

                StockType = MapFormToObject(StockType);

                bool result = StockType.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeStockType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, StockType);
                        X.GetCmp<RowSelectionModel>("rowStockType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(StockType.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(StockType);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormStockType").Close();                    
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stock Type : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemStatus)
        {
            int status = -1;                        

            if (string.IsNullOrEmpty(ItemStatus))
            {
                status = -1;
            }
            else if (ItemStatus == "true")
            {
                status = 0;
            }
            
            var liste = new StockType().fnSelect(status);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                StockType StockType = JSON.Deserialize<StockType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = StockType.fnGet(StockType.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Stock Type loading failed.");
                //(string)Session["userName"];
                StockType.UtilisateurModification = (string)Session["userName"];

                if (StockType.Desactive)
                    result = StockType.fnActivate();
                else
                    result = StockType.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Stock Type, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeStockType");

                    ModelProxy mProxy = mstore.GetById(StockType.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(StockType);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stock Type : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("StockTypeCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeStockType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)                             
                            });
            FormPanel mform = X.GetCmp<FormPanel>("StockTypeCP");

            mform.Collapsed = true;

            return this.Direct();
        }

        private StockType MapFormToObject(StockType StockType)
        {
            StockType.Designation = X.GetCmp<TextField>("TxtDesignation").Text;                        

            StockType.UtilisateurCreation = (string)Session["userName"];
            StockType.UtilisateurModification = (string)Session["userName"];

            return StockType;
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
            X.GetCmp<RowSelectionModel>("rowStockType").DeselectAll();
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