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
    public class PayementTypeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: PayementType
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{376E0066-89CA-4992-9ADD-A42ADE3FCA4A}", UserName) == false)
                X.GetCmp<Button>("btnNewPayementType").Disable();
            else
                X.GetCmp<Button>("btnNewPayementType").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{A154C616-FB78-41F9-8269-1D40C2D2EE5A}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportPayementType").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportPayementType").Enable();

            X.GetCmp<Hidden>("PatphiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{376E0066-89CA-4992-9ADD-A42ADE3FCA4A}", UserName));
            X.GetCmp<Hidden>("PatphiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{F05AD0FE-97D8-4A8D-9E72-53BB1D091B52}", UserName));
            X.GetCmp<Hidden>("PatphiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{C55ED2C2-D52F-4BAC-AA51-478DF253DAD2}", UserName));
            X.GetCmp<Hidden>("PatphiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{95C952D9-5445-4EF7-AA9F-875DD376B0C3}", UserName));
            X.GetCmp<Hidden>("PatphiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{A154C616-FB78-41F9-8269-1D40C2D2EE5A}", UserName));
            X.GetCmp<Hidden>("PatphiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{AED13638-8859-4C4D-BFDC-917B8B97E413}", UserName));

            return View();
        }

        public ActionResult Load()
        {
            List<DataPersist> mList = new PayementType().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new PayementType().fnSelect(0);
            PayementType mclass = new PayementType();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";
            mList.Insert(0, mclass);           

            return this.Store(mList);

        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PayementTypeViewModel PayementTypeVm = new PayementTypeViewModel();

            PayementTypeVm._PayementType = new PayementType();
            PayementTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPayementType", Model = PayementTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PayementTypeViewModel PayementTypeVm = new PayementTypeViewModel();

            PayementTypeVm._PayementType = JSON.Deserialize<PayementType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PayementTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPayementType", Model = PayementTypeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PayementTypeViewModel PayementTypeVm = new PayementTypeViewModel();

            PayementTypeVm._PayementType = JSON.Deserialize<PayementType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PayementTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPayementType", Model = PayementTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                PayementType PayementType = new PayementType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    PayementType.IsNew = true;
                else
                {
                    PayementType.IsNew = false;

                    PayementType.fnGet(int.Parse(GetFormValue("TxtPayementTypeID")));

                    if (PayementType == null || PayementType.ID == 0)
                        throw new Exception("SubmitFormMethod : Type Paiement load failed.");
                }

                PayementType = MapFormToObject(PayementType);

                bool result = PayementType.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePayementType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, PayementType);
                        X.GetCmp<RowSelectionModel>("rowSelectionPayementType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(PayementType.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(PayementType);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormPayementType").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type Paiement : Data Validation",
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
            var liste = new PayementType().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                PayementType PayementType = JSON.Deserialize<PayementType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = PayementType.fnGet(PayementType.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type Paiement loading failed.");
                //(string)Session["userName"];
                PayementType.UtilisateurModification = (string)Session["userName"];

                if (PayementType.Desactive)
                    result = PayementType.fnActivate();
                else
                    result = PayementType.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type Paiement, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePayementType");

                    ModelProxy mProxy = mstore.GetById(PayementType.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(PayementType);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type Paiement : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("PayementTypeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListePayementType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("PayementTypeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private PayementType MapFormToObject(PayementType PayementType)
        {
            PayementType.Designation = X.GetCmp<TextField>("TxtDesignationPayementType").Text;
            //(string)Session["userName"]
            PayementType.UtilisateurCreation = (string)Session["userName"];
            PayementType.UtilisateurModification = (string)Session["userName"];

            return PayementType;
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
            X.GetCmp<RowSelectionModel>("rowSelectionPayementType").DeselectAll();
        }


        #endregion


    }
}