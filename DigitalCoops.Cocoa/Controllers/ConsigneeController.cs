using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Security;
using Tms.Classes.Shared.Sales;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class ConsigneeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Consignee
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{93F807B7-D092-4476-B2CC-0F197BBD4411}", UserName) == false)
                X.GetCmp<Button>("btnNewConsignee").Disable();
            else
                X.GetCmp<Button>("btnNewConsignee").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{12C84C6F-B2C6-4835-8B98-EF39D66D4C49}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListConsignee").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListConsignee").Enable();

            X.GetCmp<Hidden>("CohiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{93F807B7-D092-4476-B2CC-0F197BBD4411}", UserName));
            X.GetCmp<Hidden>("CohiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{310A1D96-8EB8-4392-A5B2-27038D2CCFAF}", UserName));
            X.GetCmp<Hidden>("CohiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{7E1E440A-80D7-4ECF-A901-1F1C393E7436}", UserName));
            X.GetCmp<Hidden>("CohiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{5656F5EF-8DF6-4580-B4BF-FD25019C92BE}", UserName));
            X.GetCmp<Hidden>("CohiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{12C84C6F-B2C6-4835-8B98-EF39D66D4C49}", UserName));
            X.GetCmp<Hidden>("CohiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{2DD2631B-C185-40C3-9CD2-72F8B4584291}", UserName));

            return View();
        }

        public ActionResult LoadActive()
        {
            List<DataPersist> mList = new Consignee().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ConsigneeViewModel ConsigneeVm = new ConsigneeViewModel();

            ConsigneeVm._Consignee = new Consignee();
            ConsigneeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConsignee", Model = ConsigneeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ConsigneeViewModel ConsigneeVm = new ConsigneeViewModel();

            ConsigneeVm._Consignee = JSON.Deserialize<Consignee>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ConsigneeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConsignee", Model = ConsigneeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ConsigneeViewModel ConsigneeVm = new ConsigneeViewModel();

            ConsigneeVm._Consignee = JSON.Deserialize<Consignee>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ConsigneeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConsignee", Model = ConsigneeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Consignee consignee = new Consignee();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    consignee.IsNew = true;
                else
                {
                    consignee.IsNew = false;

                    consignee.fnGet(int.Parse(GetFormValue("TxtConsigneeID")));

                    if (consignee == null || consignee.ID == 0)
                        throw new Exception("SubmitFormMethod : Consignee load failed.");
                }

                consignee = MapFormToObject(consignee);

                bool result = consignee.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeConsignee");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, consignee);
                        X.GetCmp<RowSelectionModel>("rowSelectionConsignee").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(consignee.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(consignee);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormConsignee").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Consignee : Data Validation",
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
            var liste = new Consignee().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Consignee consignee = JSON.Deserialize<Consignee>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = consignee.fnGet(consignee.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Consignee loading failed.");
                //(string)Session["userName"];
                consignee.UtilisateurModification = (string)Session["userName"];

                if (consignee.Desactive)
                    result = consignee.fnActivate();
                else
                    result = consignee.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Consignee, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeConsignee");

                    ModelProxy mProxy = mstore.GetById(consignee.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(consignee);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Consignee : OnActivateDeactivate",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ConsigneeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeConsignee");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ConsigneeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Consignee MapFormToObject(Consignee consignee)
        {
            consignee.Designation = X.GetCmp<TextField>("TxtDesignationConsignee").Text;
            //(string)Session["userName"]
            consignee.UtilisateurCreation = (string)Session["userName"];
            consignee.UtilisateurModification = (string)Session["userName"];

            return consignee;
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
            X.GetCmp<RowSelectionModel>("rowSelectionConsignee").DeselectAll();
        }


        #endregion

    }
}