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
    public class EnregistrementModeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: EnregistrementMode
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{BD3C12B2-73C8-4A04-AB02-E3A76C9F4C79}", UserName) == false)
                X.GetCmp<Button>("btnNewEnregistrementMode").Disable();
            else
                X.GetCmp<Button>("btnNewEnregistrementMode").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{39CD5BEA-34AF-4C74-902D-B90907D2ABBE}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListEnregistrementMode").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListEnregistrementMode").Enable();

            X.GetCmp<Hidden>("QlhiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{BD3C12B2-73C8-4A04-AB02-E3A76C9F4C79}", UserName));
            X.GetCmp<Hidden>("QlhiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{FD239E6B-12A3-467A-8485-C50704DF1406}", UserName));
            X.GetCmp<Hidden>("QlhiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{492E6C22-DF5C-4F30-9C17-9FFA01EAF71A}", UserName));
            X.GetCmp<Hidden>("QlhiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{148BBA75-9CEC-43B4-97BF-EA6164E6E559}", UserName));
            X.GetCmp<Hidden>("QlhiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{39CD5BEA-34AF-4C74-902D-B90907D2ABBE}", UserName));
            X.GetCmp<Hidden>("QlhiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{0A047AD0-92AA-44C1-A421-5F416E975907}", UserName));

            return View();
        }

        public ActionResult Load()
        {
            List<DataPersist> mList = new EnregistrementMode().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new EnregistrementMode().fnSelect(0);
            EnregistrementMode mclass = new EnregistrementMode();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";
            mList.Insert(0, mclass);

            return this.Store(mList);

        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            EnregistrementModeViewModel EnregistrementModeVm = new EnregistrementModeViewModel();

            EnregistrementModeVm._EnregistrementMode = new EnregistrementMode();
            EnregistrementModeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormEnregistrementMode", Model = EnregistrementModeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            EnregistrementModeViewModel EnregistrementModeVm = new EnregistrementModeViewModel();

            EnregistrementModeVm._EnregistrementMode = JSON.Deserialize<EnregistrementMode>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            EnregistrementModeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormEnregistrementMode", Model = EnregistrementModeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            EnregistrementModeViewModel EnregistrementModeVm = new EnregistrementModeViewModel();

            EnregistrementModeVm._EnregistrementMode = JSON.Deserialize<EnregistrementMode>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            EnregistrementModeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormEnregistrementMode", Model = EnregistrementModeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                EnregistrementMode EnregistrementMode = new EnregistrementMode();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    EnregistrementMode.IsNew = true;
                else
                {
                    EnregistrementMode.IsNew = false;

                    EnregistrementMode.fnGet(int.Parse(GetFormValue("TxtEnregistrementModeID")));

                    if (EnregistrementMode == null || EnregistrementMode.ID == 0)
                        throw new Exception("SubmitFormMethod : Quality load failed.");
                }

                EnregistrementMode = MapFormToObject(EnregistrementMode);

                bool result = EnregistrementMode.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeEnregistrementMode");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, EnregistrementMode);
                        X.GetCmp<RowSelectionModel>("rowSelectionEnregistrementMode").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(EnregistrementMode.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(EnregistrementMode);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormEnregistrementMode").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Quality : Data Validation",
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
            var liste = new EnregistrementMode().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                EnregistrementMode EnregistrementMode = JSON.Deserialize<EnregistrementMode>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = EnregistrementMode.fnGet(EnregistrementMode.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Quality loading failed.");
                //(string)Session["userName"];
                EnregistrementMode.UtilisateurModification = (string)Session["userName"];

                if (EnregistrementMode.Desactive)
                    result = EnregistrementMode.fnActivate();
                else
                    result = EnregistrementMode.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Quality, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeEnregistrementMode");

                    ModelProxy mProxy = mstore.GetById(EnregistrementMode.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(EnregistrementMode);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Quality : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("EnregistrementModeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeEnregistrementMode");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("EnregistrementModeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private EnregistrementMode MapFormToObject(EnregistrementMode EnregistrementMode)
        {
            EnregistrementMode.Designation = X.GetCmp<TextField>("TxtDesignationEnregistrementMode").Text;
            //(string)Session["userName"]
            EnregistrementMode.UtilisateurCreation = (string)Session["userName"];
            EnregistrementMode.UtilisateurModification = (string)Session["userName"];

            return EnregistrementMode;
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
            X.GetCmp<RowSelectionModel>("rowSelectionEnregistrementMode").DeselectAll();
        }


        #endregion
    }
}