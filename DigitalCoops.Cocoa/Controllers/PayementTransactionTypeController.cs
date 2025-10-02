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
    public class PayementTransactionTypeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: PayementTransactionType
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{BD088C9D-4CA8-4226-8C7B-BB4BECFD8D94}", UserName) == false)
                X.GetCmp<Button>("btnNewPayementTransactionType").Disable();
            else
                X.GetCmp<Button>("btnNewPayementTransactionType").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{5F84E2EA-A5EE-4A65-A919-68EFED03F281}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListTransactType").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListTransactType").Enable();

            X.GetCmp<Hidden>("PttphiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{BD088C9D-4CA8-4226-8C7B-BB4BECFD8D94}", UserName));
            X.GetCmp<Hidden>("PttphiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{A3299A19-244E-4235-A96E-51945A5A0E81}", UserName));
            X.GetCmp<Hidden>("PttphiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{62DE4C56-FF7F-49BC-AAAC-87E111211FA2}", UserName));
            X.GetCmp<Hidden>("PttphiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{BEAFB2F8-9E68-4CCA-8D24-3F9FCC743A4B}", UserName));
            X.GetCmp<Hidden>("PttphiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{5F84E2EA-A5EE-4A65-A919-68EFED03F281}", UserName));
            X.GetCmp<Hidden>("PttphiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{62CC41FB-530E-4456-B79F-3AFD48C91B4A}", UserName));

            return View();
        }

        public ActionResult Load()
        {
            List<DataPersist> mList = new PayementTransactionType().fnSelect();
            return this.Store(mList);
        }

        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new PayementTransactionType().fnSelect();
            PayementTransactionType mclass = new PayementTransactionType();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";
            mList.Insert(0, mclass);           

            return this.Store(mList);

        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PayementTransactionTypeViewModel PayementTransactionTypeVm = new PayementTransactionTypeViewModel();

            PayementTransactionTypeVm._PayementTransactionType = new PayementTransactionType();
            PayementTransactionTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPayementTransactionType", Model = PayementTransactionTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PayementTransactionTypeViewModel PayementTransactionTypeVm = new PayementTransactionTypeViewModel();

            PayementTransactionTypeVm._PayementTransactionType = JSON.Deserialize<PayementTransactionType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PayementTransactionTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPayementTransactionType", Model = PayementTransactionTypeVm };

        }


        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PayementTransactionTypeViewModel PayementTransactionTypeVm = new PayementTransactionTypeViewModel();

            PayementTransactionTypeVm._PayementTransactionType = JSON.Deserialize<PayementTransactionType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PayementTransactionTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPayementTransactionType", Model = PayementTransactionTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                PayementTransactionType payementtransactiontype = new PayementTransactionType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    payementtransactiontype.IsNew = true;
                else
                {
                    payementtransactiontype.IsNew = false;

                    payementtransactiontype.fnGet(int.Parse(GetFormValue("TxtPayementTransactionTypeID")));

                    if (payementtransactiontype == null || payementtransactiontype.ID == 0)
                        throw new Exception("SubmitFormMethod : Transaction type load failed.");
                }

                payementtransactiontype = MapFormToObject(payementtransactiontype);

                bool result = payementtransactiontype.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePayementTransactionType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, payementtransactiontype);
                        X.GetCmp<RowSelectionModel>("rowSelectionPayementTransactionType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(payementtransactiontype.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(payementtransactiontype);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormPayementTransactionType").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Transaction type : Data Validation",
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
            var liste = new PayementTransactionType().fnSelect(status);            

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                PayementTransactionType payementtransactiontype = JSON.Deserialize<PayementTransactionType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = payementtransactiontype.fnGet(payementtransactiontype.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Transaction type loading failed.");
                //(string)Session["userName"];
                payementtransactiontype.UtilisateurModification = (string)Session["userName"];

                if (payementtransactiontype.Desactive)
                    result = payementtransactiontype.fnActivate();
                else
                    result = payementtransactiontype.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Transaction type, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePayementTransactionType");

                    ModelProxy mProxy = mstore.GetById(payementtransactiontype.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(payementtransactiontype);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Transaction type : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("PayementTransactionTypeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListePayementTransactionType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("PayementTransactionTypeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private PayementTransactionType MapFormToObject(PayementTransactionType PayementTransactionType)
        {
            PayementTransactionType.Designation = X.GetCmp<TextField>("TxtDesignationPayementTransactionType").Text;
            //(string)Session["userName"]
            PayementTransactionType.UtilisateurCreation = (string)Session["userName"];
            PayementTransactionType.UtilisateurModification = (string)Session["userName"];

            return PayementTransactionType;
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
            X.GetCmp<RowSelectionModel>("rowSelectionPayementTransactionType").DeselectAll();
        }


        #endregion


    }
}