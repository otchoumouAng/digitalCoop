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
    public class RedevanceNatureController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: RedevanceNature
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{74D01653-CC9E-4386-8A7A-2EAC768015D8}", UserName) == false)
                X.GetCmp<Button>("btnNewRedevanceNature").Disable();
            else
                X.GetCmp<Button>("btnNewRedevanceNature").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{E9749DA9-3388-4F5A-8B35-CE88786DA22B}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListRedevanceNature").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListRedevanceNature").Enable();

            X.GetCmp<Hidden>("RthiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{74D01653-CC9E-4386-8A7A-2EAC768015D8}", UserName));
            X.GetCmp<Hidden>("RthiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{B87EDA53-F44A-4CED-8481-0A21B7B24D74}", UserName));
            X.GetCmp<Hidden>("RthiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{4C83F41A-A38D-48B0-8FDF-5FED5B7B737A}", UserName));
            X.GetCmp<Hidden>("RthiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{F4D060D8-961E-4AA6-B987-0417C9C6CFED}", UserName));
            X.GetCmp<Hidden>("RthiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{E9749DA9-3388-4F5A-8B35-CE88786DA22B}", UserName));
            X.GetCmp<Hidden>("RthiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{AA64F85D-D7A0-4E0B-A28C-037D45B5D699}", UserName));

            return View();
        }

        public ActionResult LoadActive()
        {
            List<DataPersist> mList = new RedevanceNature().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            RedevanceNatureViewModel RedevanceNatureVm = new RedevanceNatureViewModel();

            RedevanceNatureVm._RedevanceNature = new RedevanceNature();
            RedevanceNatureVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRedevanceType", Model = RedevanceNatureVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            RedevanceNatureViewModel RedevanceNatureVm = new RedevanceNatureViewModel();

            RedevanceNatureVm._RedevanceNature = JSON.Deserialize<RedevanceNature>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            RedevanceNatureVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRedevanceType", Model = RedevanceNatureVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            RedevanceNatureViewModel RedevanceNatureVm = new RedevanceNatureViewModel();

            RedevanceNatureVm._RedevanceNature = JSON.Deserialize<RedevanceNature>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            RedevanceNatureVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRedevanceType", Model = RedevanceNatureVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                RedevanceNature type = new RedevanceNature();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    type.IsNew = true;
                else
                {
                    type.IsNew = false;

                    type.fnGet(int.Parse(GetFormValue("TxtRedevanceTypeID")));

                    if (type == null || type.ID == 0)
                        throw new Exception("SubmitFormMethod : Type Of Fee load failed.");
                }

                type = MapFormToObject(type);

                bool result = type.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeRedevanceType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, type);
                        X.GetCmp<RowSelectionModel>("rowSelectionRedevanceType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(type.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(type);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormRedevanceType").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type Of Fee : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        //public ActionResult Select(StoreRequestParameters parameters, string ItemStatus)
        //{
        //    int status = -1;
        //    if (string.IsNullOrEmpty(ItemStatus))
        //    {
        //        status = -1;
        //    }
        //    else if (ItemStatus == "true")
        //    {
        //        status = 0;
        //    }
        //    var liste = new RedevanceNature().fnSelect(status,-1);

        //    //var paging = GridStorePaging.SetRangePlants(parameters, liste);
        //    return this.Store(liste);
        //}

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
            
            var liste = new RedevanceNature().fnSelect(status);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                RedevanceNature type = JSON.Deserialize<RedevanceNature>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = type.fnGet(type.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type Of Fee loading failed.");
                //(string)Session["userName"];
                type.UtilisateurModification = (string)Session["userName"];

                if (type.Desactive)
                    result = type.fnActivate();
                else
                    result = type.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type Of Fee, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeRedevanceType");

                    ModelProxy mProxy = mstore.GetById(type.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(type);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type Of Fee : OnActivateDeactivate",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("RedevanceTypeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeRedevanceType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("RedevanceTypeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private RedevanceNature MapFormToObject(RedevanceNature nature)
        {
            nature.Designation = X.GetCmp<TextField>("TxtDesignationRedevanceType").Text;

            nature.UtilisateurCreation = (string)Session["userName"];
            nature.UtilisateurModification = (string)Session["userName"];

            return nature;
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
            X.GetCmp<RowSelectionModel>("rowSelectionRedevanceType").DeselectAll();
        }


        #endregion
    }
}