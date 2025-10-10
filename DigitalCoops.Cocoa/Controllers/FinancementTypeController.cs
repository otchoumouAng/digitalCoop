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
    public class FinancementTypeController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: FinancementType
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{A24185A0-DA43-4A67-9539-027312560A57}", UserName) == false)
                X.GetCmp<Button>("btnNewFinancementType").Disable();
            else
                X.GetCmp<Button>("btnNewFinancementType").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{E097F617-4245-4E56-ABAD-5D48490C8252}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListFinancingType").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListFinancingType").Enable();

            X.GetCmp<Hidden>("FthiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{A24185A0-DA43-4A67-9539-027312560A57}", UserName));
            X.GetCmp<Hidden>("FthiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{09B213E9-474A-495F-921D-1F3834FC1779}", UserName));
            X.GetCmp<Hidden>("FthiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{B0BAD663-9556-42BF-8F2D-577258A4A3E1}", UserName));
            X.GetCmp<Hidden>("FthiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{0DD7ACB4-E0C3-488B-8603-C808BD2ABAC3}", UserName));
            X.GetCmp<Hidden>("FthiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{E097F617-4245-4E56-ABAD-5D48490C8252}", UserName));
            X.GetCmp<Hidden>("FthiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{659CF675-CF1A-4759-A20C-9BAB210F87BB}", UserName));

            return View();
        }

        public ActionResult Load()
        {
            List<DataPersist> mList = new FinancementType().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new FinancementType().fnSelect(0);
            FinancementType mclass = new FinancementType();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";
            mList.Insert(0, mclass);           

            return this.Store(mList);

        }

        public ActionResult OnAdd()
        {            
            FinancementTypeViewModel FinancementTypeVm = new FinancementTypeViewModel();

            FinancementTypeVm._FinancementType = new FinancementType();
            FinancementTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFinancementType", Model = FinancementTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {            
            FinancementTypeViewModel FinancementTypeVm = new FinancementTypeViewModel();

            FinancementTypeVm._FinancementType = JSON.Deserialize<FinancementType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            FinancementTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFinancementType", Model = FinancementTypeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {            
            FinancementTypeViewModel FinancementTypeVm = new FinancementTypeViewModel();

            FinancementTypeVm._FinancementType = JSON.Deserialize<FinancementType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            FinancementTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFinancementType", Model = FinancementTypeVm };
        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                FinancementType financementtype = new FinancementType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    financementtype.IsNew = true;
                else
                {
                    financementtype.IsNew = false;

                    financementtype.fnGet(int.Parse(GetFormValue("TxtFinancementTypeID")));

                    if (financementtype == null || financementtype.ID == 0)
                        throw new Exception("SubmitFormMethod : Type De Financement load failed.");
                }

                financementtype = MapFormToObject(financementtype);

                bool result = financementtype.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeFinancementType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, financementtype);
                        X.GetCmp<RowSelectionModel>("rowSelectionFinancementType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(financementtype.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(financementtype);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormFinancementType").Close();                    
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type De Financement : Data Validation",
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
            var liste = new FinancementType().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                FinancementType financementtype = JSON.Deserialize<FinancementType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = financementtype.fnGet(financementtype.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type De Financement loading failed.");
                //(string)Session["userName"];
                financementtype.UtilisateurModification = (string)Session["userName"];

                if (financementtype.Desactive)
                    result = financementtype.fnActivate();
                else
                    result = financementtype.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type De Financement, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeFinancementType");

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
                    Title = "Type De Financement : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("FinancementTypeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeFinancementType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("FinancementTypeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private FinancementType MapFormToObject(FinancementType financementtype)
        {
            financementtype.Designation = X.GetCmp<TextField>("TxtDesignationFinancementType").Text;
            financementtype.OrdreStatus = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtOrdre").RawText) ? decimal.Parse(X.GetCmp<NumberField>("TxtOrdre").RawValue.ToString()) : (decimal?)null;
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
            X.GetCmp<RowSelectionModel>("rowSelectionFinancementType").DeselectAll();
        }


        #endregion


    }
}