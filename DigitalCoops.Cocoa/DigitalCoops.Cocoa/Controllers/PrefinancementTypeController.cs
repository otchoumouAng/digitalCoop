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
    public class PrefinancementTypeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Prefinancement
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{0423C4EC-C8F0-46D0-9937-C40BC131E5B3}", UserName) == false)
                X.GetCmp<Button>("btnNewPrefinancementType").Disable();
            else
                X.GetCmp<Button>("btnNewPrefinancementType").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{089CAA60-1736-46CF-98B2-075C1BCEAA07}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListFinancingType").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListFinancingType").Enable();

            X.GetCmp<Hidden>("FthiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{0423C4EC-C8F0-46D0-9937-C40BC131E5B3}", UserName));
            X.GetCmp<Hidden>("FthiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{82DE55A6-80DC-40C9-9748-CD60719E45DF}", UserName));
            X.GetCmp<Hidden>("FthiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{C00AA786-7AFE-43E4-923F-AA1CF4DFEB81}", UserName));
            X.GetCmp<Hidden>("FthiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{841E6A85-D963-408E-B979-56B0AD6BAC1C}", UserName));
            X.GetCmp<Hidden>("FthiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{089CAA60-1736-46CF-98B2-075C1BCEAA07}", UserName));
            X.GetCmp<Hidden>("FthiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{7FD34F24-4E78-4AE8-85CB-4D344C894FD8}", UserName));

            return View();
        }

        public ActionResult Load()
        {
            List<DataPersist> mList = new PrefinancementType().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new PrefinancementType().fnSelect(0);
            PrefinancementType mclass = new PrefinancementType();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";
            mList.Insert(0, mclass);

            return this.Store(mList);

        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PrefinancementTypeViewModel PrefinancementTypeVm = new PrefinancementTypeViewModel();

            PrefinancementTypeVm._PrefinancementType = new PrefinancementType();
            PrefinancementTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrefinancementType", Model = PrefinancementTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PrefinancementTypeViewModel PrefinancementTypeVm = new PrefinancementTypeViewModel();

            PrefinancementTypeVm._PrefinancementType = JSON.Deserialize<PrefinancementType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PrefinancementTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrefinancementType", Model = PrefinancementTypeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PrefinancementTypeViewModel PrefinancementTypeVm = new PrefinancementTypeViewModel();

            PrefinancementTypeVm._PrefinancementType = JSON.Deserialize<PrefinancementType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PrefinancementTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrefinancementType", Model = PrefinancementTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                PrefinancementType financementtype = new PrefinancementType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    financementtype.IsNew = true;
                else
                {
                    financementtype.IsNew = false;

                    financementtype.fnGet(int.Parse(GetFormValue("TxtPrefinancementTypeID")));

                    if (financementtype == null || financementtype.ID == 0)
                        throw new Exception("SubmitFormMethod : Type of prefinancing load failed.");
                }

                financementtype = MapFormToObject(financementtype);

                bool result = financementtype.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePrefinancementType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, financementtype);
                        X.GetCmp<RowSelectionModel>("rowSelectionPrefinancementType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(financementtype.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(financementtype);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormPrefinancementType").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type of prefinancing : Data Validation",
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
            var liste = new PrefinancementType().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                PrefinancementType financementtype = JSON.Deserialize<PrefinancementType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = financementtype.fnGet(financementtype.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type of prefinancing loading failed.");
                //(string)Session["userName"];
                financementtype.UtilisateurModification = (string)Session["userName"];

                if (financementtype.Desactive)
                    result = financementtype.fnActivate();
                else
                    result = financementtype.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type of prefinancing, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePrefinancementType");

                    ModelProxy mProxy = mstore.GetById(financementtype.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(financementtype);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type of prefinancing : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("PrefinancementTypeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListePrefinancementType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("PrefinancementTypeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private PrefinancementType MapFormToObject(PrefinancementType financementtype)
        {
            financementtype.Designation = X.GetCmp<TextField>("TxtDesignationPrefinancementType").Text;
            //(string)Session["userName"]
            financementtype.UtilisateurCreation = (string)Session["userName"];
            financementtype.UtilisateurModification = (string)Session["userName"];

            return financementtype;
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
            X.GetCmp<RowSelectionModel>("rowSelectionPrefinancementType").DeselectAll();
        }


        #endregion
    }
}