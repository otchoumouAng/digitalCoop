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
    public class ModeTraitementController : Controller
    {
        // GET: ModeTraitement
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{8C72DA1F-C9DB-4EC2-8F9A-73423075CBA0}", UserName) == false)
                X.GetCmp<Button>("btnNewModeTraitement").Disable();
            else
                X.GetCmp<Button>("btnNewModeTraitement").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{3EB074F7-AAF9-4E2B-923D-6D1D1B2DEDAE}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListModeTraitement").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListModeTraitement").Enable();

            X.GetCmp<Hidden>("MthiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{8C72DA1F-C9DB-4EC2-8F9A-73423075CBA0}", UserName));
            X.GetCmp<Hidden>("MthiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{8F9FC956-D57D-4BDC-B7AE-D8569946C3DB}", UserName));
            X.GetCmp<Hidden>("MthiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{D8E75DD2-2325-44A2-823B-4AC13787B738}", UserName));
            X.GetCmp<Hidden>("MthiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{E7CB78E4-D27E-4704-BE2A-922BF49642E7}", UserName));
            X.GetCmp<Hidden>("MthiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{3EB074F7-AAF9-4E2B-923D-6D1D1B2DEDAE}", UserName));
            X.GetCmp<Hidden>("MthiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{934E6369-7C10-4B28-B038-1DD591FBB960}", UserName));

            return View();
        }

        public ActionResult LoadActive()
        {
            List<DataPersist> mList = new ModeTraitement().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ModeTraitementViewModel ModeTraitementVm = new ModeTraitementViewModel();

            ModeTraitementVm._ModeTraitement = new ModeTraitement();
            ModeTraitementVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormModeTraitement", Model = ModeTraitementVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ModeTraitementViewModel ModeTraitementVm = new ModeTraitementViewModel();

            ModeTraitementVm._ModeTraitement = JSON.Deserialize<ModeTraitement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ModeTraitementVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormModeTraitement", Model = ModeTraitementVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ModeTraitementViewModel ModeTraitementVm = new ModeTraitementViewModel();

            ModeTraitementVm._ModeTraitement = JSON.Deserialize<ModeTraitement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ModeTraitementVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormModeTraitement", Model = ModeTraitementVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                ModeTraitement mode = new ModeTraitement();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mode.IsNew = true;
                else
                {
                    mode.IsNew = false;

                    mode.fnGet(int.Parse(GetFormValue("TxtModeTraitementID")));

                    if (mode == null || mode.ID == 0)
                        throw new Exception("SubmitFormMethod : Processing Mode load failed.");
                }

                mode = MapFormToObject(mode);

                bool result = mode.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeModeTraitement");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mode);
                        X.GetCmp<RowSelectionModel>("rowSelectionModeTraitement").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mode.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mode);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormModeTraitement").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Processing Mode : Data Validation",
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
            var liste = new ModeTraitement().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                ModeTraitement mode = JSON.Deserialize<ModeTraitement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = mode.fnGet(mode.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Processing Mode loading failed.");
                //(string)Session["userName"];
                mode.UtilisateurModification = (string)Session["userName"];

                if (mode.Desactive)
                    result = mode.fnActivate();
                else
                    result = mode.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Processing Mode, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeModeTraitement");

                    ModelProxy mProxy = mstore.GetById(mode.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mode);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Processing Mode : OnActivateDeactivate",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ModeTraitementCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeModeTraitement");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ModeTraitementCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private ModeTraitement MapFormToObject(ModeTraitement mode)
        {
            mode.Designation = X.GetCmp<TextField>("TxtDesignationModeTraitement").Text;
            //(string)Session["userName"]
            mode.UtilisateurCreation = (string)Session["userName"];
            mode.UtilisateurModification = (string)Session["userName"];

            return mode;
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
            X.GetCmp<RowSelectionModel>("rowSelectionModeTraitement").DeselectAll();
        }


        #endregion

    }
}