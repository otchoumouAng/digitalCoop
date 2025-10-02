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
    public class RemboursementModeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: RemboursementMode

        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{103A9239-4473-4045-A38A-6C6B504CD04B}", UserName) == false)
                X.GetCmp<Button>("btnNewRemboursementMode").Disable();
            else
                X.GetCmp<Button>("btnNewRemboursementMode").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{83198D17-1E3C-4C86-9957-0379B6F0AA1D}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportRefundModeList").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportRefundModeList").Enable();

            X.GetCmp<Hidden>("RfmhiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{103A9239-4473-4045-A38A-6C6B504CD04B}", UserName));
            X.GetCmp<Hidden>("RfmhiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{7C54BB66-DCB8-4EEC-85A2-3A02E28030B4}", UserName));
            X.GetCmp<Hidden>("RfmhiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{599D8C34-7967-4E74-9389-345464996949}", UserName));
            X.GetCmp<Hidden>("RfmhiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{C7D48BE9-EB64-4BE4-A6F9-1A7D19CEF569}", UserName));
            X.GetCmp<Hidden>("RfmhiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{83198D17-1E3C-4C86-9957-0379B6F0AA1D}", UserName));
            X.GetCmp<Hidden>("RfmhiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{AC935EEF-D1D9-4936-B36C-9BC1C3BBA448}", UserName));

            return View();
        }

        public ActionResult LoadRemboursementMode()
        {
            List<DataPersist> mList = new RemboursementMode().fnSelect(0);

            RemboursementMode mclass = new RemboursementMode();

            if (mList.Count > 0)
                mclass = mList[0] as RemboursementMode;

            return this.Store(mList);
        }


        public ActionResult LoadRemboursementModeAll()
        {
            List<DataPersist> mList = new RemboursementMode().fnSelect(0);

            RemboursementMode mclass = new RemboursementMode();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as RemboursementMode;

            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            RemboursementModeViewModel RemboursementModeVm = new RemboursementModeViewModel();

            RemboursementModeVm._RemboursementMode = new RemboursementMode();
            RemboursementModeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRemboursementMode", Model = RemboursementModeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            RemboursementModeViewModel RemboursementModeVm = new RemboursementModeViewModel();

            RemboursementModeVm._RemboursementMode = JSON.Deserialize<RemboursementMode>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            RemboursementModeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRemboursementMode", Model = RemboursementModeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            RemboursementModeViewModel RemboursementModeVm = new RemboursementModeViewModel();

            RemboursementModeVm._RemboursementMode = JSON.Deserialize<RemboursementMode>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            RemboursementModeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRemboursementMode", Model = RemboursementModeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                RemboursementMode remboursementmode = new RemboursementMode();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    remboursementmode.IsNew = true;
                else
                {
                    remboursementmode.IsNew = false;

                    remboursementmode.fnGet(int.Parse(GetFormValue("TxtRemboursementModeID")));

                    if (remboursementmode == null || remboursementmode.ID == 0)
                        throw new Exception("SubmitFormMethod : Refund Mode load failed.");
                }

                remboursementmode = MapFormToObject(remboursementmode);

                bool result = remboursementmode.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeRemboursementMode");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, remboursementmode);
                        X.GetCmp<RowSelectionModel>("rowSelectionRemboursementMode").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(remboursementmode.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(remboursementmode);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormRemboursementMode").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Refund Mode : Data Validation",
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
            var liste = new RemboursementMode().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                RemboursementMode remboursementmode = JSON.Deserialize<RemboursementMode>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = remboursementmode.fnGet(remboursementmode.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Refund Mode loading failed.");
                //(string)Session["userName"];
                remboursementmode.UtilisateurModification = (string)Session["userName"];

                if (remboursementmode.Desactive)
                    result = remboursementmode.fnActivate();
                else
                    result = remboursementmode.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Refund Mode, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeRemboursementMode");

                    ModelProxy mProxy = mstore.GetById(remboursementmode.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(remboursementmode);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Refund Mode : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("RemboursementModeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeRemboursementMode");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("RemboursementModeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private RemboursementMode MapFormToObject(RemboursementMode remboursementmode)
        {
            remboursementmode.Designation = X.GetCmp<TextField>("TxtDesignationRemboursementMode").Text;
            //(string)Session["userName"]
            remboursementmode.UtilisateurCreation = (string)Session["userName"];
            remboursementmode.UtilisateurModification = (string)Session["userName"];

            return remboursementmode;
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
            X.GetCmp<RowSelectionModel>("rowSelectionRemboursementMode").DeselectAll();
        }


        #endregion

    }
}