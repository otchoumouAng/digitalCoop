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
    public class PayementModeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: PayementMode
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{92894B5F-5D70-473A-B520-E6C08FB02962}", UserName) == false)
                X.GetCmp<Button>("btnNewPayementMode").Disable();
            else
                X.GetCmp<Button>("btnNewPayementMode").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{8BC51556-F113-4D25-BB82-BC34F3F119CD}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListModePayement").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListModePayement").Enable();

            X.GetCmp<Hidden>("MophiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{92894B5F-5D70-473A-B520-E6C08FB02962}", UserName));
            X.GetCmp<Hidden>("MophiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{85AF0138-59BB-448A-8904-D17A4CB398A0}", UserName));
            X.GetCmp<Hidden>("MophiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{194441C4-2BBB-4546-9B7A-2925A8D06161}", UserName));
            X.GetCmp<Hidden>("MophiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{24356FC9-24FA-41BB-866E-7CED107D460C}", UserName));
            X.GetCmp<Hidden>("MophiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{8BC51556-F113-4D25-BB82-BC34F3F119CD}", UserName));
            X.GetCmp<Hidden>("MophiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{E1155949-4C1F-4DAE-B4FA-5966FDFDC9AD}", UserName));

            return View();
        }

        public ActionResult Load()
        {
            List<DataPersist> mList = new PayementMode().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new PayementMode().fnSelect(0);
            PayementMode mclass = new PayementMode();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";
            mList.Insert(0, mclass);           

            return this.Store(mList);

        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PayementModeViewModel PayementModeVm = new PayementModeViewModel();

            PayementModeVm._PayementMode = new PayementMode();
            PayementModeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPayementMode", Model = PayementModeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PayementModeViewModel PayementModeVm = new PayementModeViewModel();

            PayementModeVm._PayementMode = JSON.Deserialize<PayementMode>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PayementModeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPayementMode", Model = PayementModeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PayementModeViewModel PayementModeVm = new PayementModeViewModel();

            PayementModeVm._PayementMode = JSON.Deserialize<PayementMode>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PayementModeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPayementMode", Model = PayementModeVm };

        }


        public ActionResult SubmitFormMethod()
        {

            try
            {
                PayementMode payementmode = new PayementMode();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    payementmode.IsNew = true;
                else
                {
                    payementmode.IsNew = false;

                    payementmode.fnGet(int.Parse(GetFormValue("TxtPayementModeID")));

                    if (payementmode == null || payementmode.ID == 0)
                        throw new Exception("SubmitFormMethod : Mode Paiement load failed.");
                }

                payementmode = MapFormToObject(payementmode);

                bool result = payementmode.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePayementMode");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, payementmode);
                        X.GetCmp<RowSelectionModel>("rowSelectionPayementMode").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(payementmode.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(payementmode);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormPayementMode").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Mode Paiement : Data Validation",
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
            var liste = new PayementMode().fnSelect(status);
            //var paging = GridStorePaging.SetRangePlants(parameters, liste);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                PayementMode payementmode = JSON.Deserialize<PayementMode>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = payementmode.fnGet(payementmode.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Mode Paiement loading failed.");
                //(string)Session["userName"];
                payementmode.UtilisateurModification = (string)Session["userName"];

                if (payementmode.Desactive)
                    result = payementmode.fnActivate();
                else
                    result = payementmode.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Mode Paiement, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePayementMode");

                    ModelProxy mProxy = mstore.GetById(payementmode.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(payementmode);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Mode Paiement : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("PayementModeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListePayementMode");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("PayementModeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private PayementMode MapFormToObject(PayementMode PayementMode)
        {
            PayementMode.Designation = X.GetCmp<TextField>("TxtDesignationPayementMode").Text;
            //(string)Session["userName"]
            PayementMode.UtilisateurCreation = (string)Session["userName"];
            PayementMode.UtilisateurModification = (string)Session["userName"];

            return PayementMode;
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
            X.GetCmp<RowSelectionModel>("rowSelectionPayementMode").DeselectAll();
        }


        #endregion


    }
}