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
    public class ConditionnementController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Conditionnement
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{DD2ADDF1-5C34-4E81-A6BA-C55A630AAAF4}", UserName) == false)
                X.GetCmp<Button>("btnNewConditionnement").Disable();
            else
                X.GetCmp<Button>("btnNewConditionnement").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{05015259-CFDD-4CDB-9CF4-380063D3DDD1}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListConditionnement").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListConditionnement").Enable();

            X.GetCmp<Hidden>("CohiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{DD2ADDF1-5C34-4E81-A6BA-C55A630AAAF4}", UserName));
            X.GetCmp<Hidden>("CohiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{5E817109-CC57-4653-BAB5-831792397000}", UserName));
            X.GetCmp<Hidden>("CohiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{146AB76D-7CAB-4202-937E-3EC3614F4B09}", UserName));
            X.GetCmp<Hidden>("CohiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{515375D3-8FB0-4657-B5F1-3B243EFEAEFA}", UserName));
            X.GetCmp<Hidden>("CohiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{05015259-CFDD-4CDB-9CF4-380063D3DDD1}", UserName));
            X.GetCmp<Hidden>("CohiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{FB43A50A-FE37-4E5F-BC87-A80967393933}", UserName));

            return View();
        }

        public ActionResult Load()
        {
            List<DataPersist> mList = new Conditionnement().fnSelect();
            return this.Store(mList);
        }

        public ActionResult LoadActive()
        {
            List<DataPersist> mList = new Conditionnement().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new Conditionnement().fnSelect();
            Conditionnement mclass = new Conditionnement();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";
            mList.Insert(0, mclass);

            return this.Store(mList);

        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ConditionnementViewModel ConditionnementVm = new ConditionnementViewModel();

            ConditionnementVm._Conditionnement = new Conditionnement();
            ConditionnementVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConditionnement", Model = ConditionnementVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ConditionnementViewModel ConditionnementVm = new ConditionnementViewModel();

            ConditionnementVm._Conditionnement = JSON.Deserialize<Conditionnement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ConditionnementVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConditionnement", Model = ConditionnementVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ConditionnementViewModel ConditionnementVm = new ConditionnementViewModel();

            ConditionnementVm._Conditionnement = JSON.Deserialize<Conditionnement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ConditionnementVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConditionnement", Model = ConditionnementVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Conditionnement package = new Conditionnement();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    package.IsNew = true;
                else
                {
                    package.IsNew = false;

                    package.fnGet(int.Parse(GetFormValue("TxtConditionnementID")));

                    if (package == null || package.ID == 0)
                        throw new Exception("SubmitFormMethod : Packaging load failed.");
                }

                package = MapFormToObject(package);

                bool result = package.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeConditionnement");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, package);
                        X.GetCmp<RowSelectionModel>("rowSelectionConditionnement").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(package.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(package);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormConditionnement").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Packaging : Data Validation",
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
            var liste = new Conditionnement().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Conditionnement package = JSON.Deserialize<Conditionnement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = package.fnGet(package.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Packaging loading failed.");
                //(string)Session["userName"];
                package.UtilisateurModification = (string)Session["userName"];

                if (package.Desactive)
                    result = package.fnActivate();
                else
                    result = package.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Packaging, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeConditionnement");

                    ModelProxy mProxy = mstore.GetById(package.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(package);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Packaging : OnActivateDeactivate",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ConditionnementCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeConditionnement");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ConditionnementCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Conditionnement MapFormToObject(Conditionnement package)
        {
            package.Designation = X.GetCmp<TextField>("TxtDesignationConditionnement").Text;
            //(string)Session["userName"]
            package.UtilisateurCreation = (string)Session["userName"];
            package.UtilisateurModification = (string)Session["userName"];

            return package;
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
            X.GetCmp<RowSelectionModel>("rowSelectionConditionnement").DeselectAll();
        }


        #endregion
    }
}