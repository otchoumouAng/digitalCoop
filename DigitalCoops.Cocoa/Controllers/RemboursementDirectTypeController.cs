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

namespace Tms2017.MVC.Controllers
{
    public class RemboursementDirectTypeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: RemboursementDirectType
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{8B595A9F-9DB0-4ED5-9163-14C1DCC532D9}", UserName) == false)
                X.GetCmp<Button>("btnNewRemboursementDirectType").Disable();
            else
                X.GetCmp<Button>("btnNewRemboursementDirectType").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{8BDA4626-9AD6-4379-B60D-1087C0206830}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListRefundType").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListRefundType").Enable();

            X.GetCmp<Hidden>("RfthiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{8B595A9F-9DB0-4ED5-9163-14C1DCC532D9}", UserName));
            X.GetCmp<Hidden>("RfthiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{ABC1ABD1-3542-46AB-8173-24DA21E7474D}", UserName));
            X.GetCmp<Hidden>("RfthiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{4079F372-0933-44B4-990B-52D5B471EB59}", UserName));
            X.GetCmp<Hidden>("RfthiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{E54077B3-3E1C-4F15-944F-043C6DBF94E1}", UserName));
            X.GetCmp<Hidden>("RfthiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{8BDA4626-9AD6-4379-B60D-1087C0206830}", UserName));
            X.GetCmp<Hidden>("RfthiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{8AD9DCB3-E43D-42B2-9E39-584D1A905F39}", UserName));


            return View();
        }

        public ActionResult LoadRemboursementDirectType()
        {
            List<DataPersist> mList = new RemboursementDirectType().fnSelect(0);

            RemboursementDirectType mclass = new RemboursementDirectType();

            if (mList.Count > 0)
                mclass = mList[0] as RemboursementDirectType;

            return this.Store(mList);
        }


        public ActionResult LoadRemboursementDirectTypeAll()
        {
            List<DataPersist> mList = new RemboursementDirectType().fnSelect(0);

            RemboursementDirectType mclass = new RemboursementDirectType();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as RemboursementDirectType;

            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            RemboursementDirectTypeViewModel RemboursementDirectTypeVm = new RemboursementDirectTypeViewModel();

            RemboursementDirectTypeVm._RemboursementDirectType = new RemboursementDirectType();
            RemboursementDirectTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRemboursementDirectType", Model = RemboursementDirectTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            RemboursementDirectTypeViewModel RemboursementDirectTypeVm = new RemboursementDirectTypeViewModel();

            RemboursementDirectTypeVm._RemboursementDirectType = JSON.Deserialize<RemboursementDirectType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            RemboursementDirectTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRemboursementDirectType", Model = RemboursementDirectTypeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            RemboursementDirectTypeViewModel RemboursementDirectTypeVm = new RemboursementDirectTypeViewModel();

            RemboursementDirectTypeVm._RemboursementDirectType = JSON.Deserialize<RemboursementDirectType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            RemboursementDirectTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRemboursementDirectType", Model = RemboursementDirectTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                RemboursementDirectType remboursementDirectType = new RemboursementDirectType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    remboursementDirectType.IsNew = true;
                else
                {
                    remboursementDirectType.IsNew = false;

                    remboursementDirectType.fnGet(int.Parse(GetFormValue("TxtRemboursementDirectTypeID")));

                    if (remboursementDirectType == null || remboursementDirectType.ID == 0)
                        throw new Exception("SubmitFormMethod : Direct Refund Type Mode load failed.");
                }

                remboursementDirectType = MapFormToObject(remboursementDirectType);

                bool result = remboursementDirectType.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeRemboursementDirectType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, remboursementDirectType);
                        X.GetCmp<RowSelectionModel>("rowSelectionRemboursementDirectType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(remboursementDirectType.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(remboursementDirectType);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormRemboursementDirectType").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Direct Refund Type : Data Validation",
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
            var liste = new RemboursementDirectType().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                RemboursementDirectType remboursementDirectType = JSON.Deserialize<RemboursementDirectType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = remboursementDirectType.fnGet(remboursementDirectType.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Direct Refund Type loading failed.");
                //(string)Session["userName"];
                remboursementDirectType.UtilisateurModification = (string)Session["userName"];

                if (remboursementDirectType.Desactive)
                    result = remboursementDirectType.fnActivate();
                else
                    result = remboursementDirectType.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Direct Refund Type, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeRemboursementDirectType");

                    ModelProxy mProxy = mstore.GetById(remboursementDirectType.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(remboursementDirectType);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Direct Refund Type : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("RemboursementDirectTypeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeRemboursementDirectType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("RemboursementDirectTypeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private RemboursementDirectType MapFormToObject(RemboursementDirectType remboursementdirecttype)
        {
            remboursementdirecttype.Designation = X.GetCmp<TextField>("TxtDesignationRemboursementDirectType").Text;
            //(string)Session["userName"]
            remboursementdirecttype.UtilisateurCreation = (string)Session["userName"];
            remboursementdirecttype.UtilisateurModification = (string)Session["userName"];

            return remboursementdirecttype;
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
            X.GetCmp<RowSelectionModel>("rowSelectionRemboursementDirectType").DeselectAll();
        }


        #endregion

    }
}