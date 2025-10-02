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
    public class ConditionLivraisonController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: ConditionLivraison
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{399A34AC-0022-46DF-8E7C-E13552DEE040}", UserName) == false)
                X.GetCmp<Button>("btnNewConditionLivraison").Disable();
            else
                X.GetCmp<Button>("btnNewConditionLivraison").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{59B9D44F-6950-4B14-8211-8B25E901FEF6}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListConditionLivraison").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListConditionLivraison").Enable();

            X.GetCmp<Hidden>("DchiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{399A34AC-0022-46DF-8E7C-E13552DEE040}", UserName));
            X.GetCmp<Hidden>("DchiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{EA0EFE41-D551-41EF-A1E0-13D4FFCCE5EC}", UserName));
            X.GetCmp<Hidden>("DchiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{CB856881-0AC2-4690-A6EA-8A9CB1ECBBF1}", UserName));
            X.GetCmp<Hidden>("DchiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{5F0925B0-E5F0-4C47-822D-836A7133994F}", UserName));
            X.GetCmp<Hidden>("DchiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{59B9D44F-6950-4B14-8211-8B25E901FEF6}", UserName));
            X.GetCmp<Hidden>("DchiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{B3FBF129-8ECC-4AA4-B10D-CF84EE6EC64B}", UserName));

            return View();
        }

        public ActionResult Load()
        {
            List<DataPersist> mList = new ConditionLivraison().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new ConditionLivraison().fnSelect(0);
            ConditionLivraison mclass = new ConditionLivraison();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";
            mList.Insert(0, mclass);

            return this.Store(mList);

        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ConditionLivraisonViewModel ConditionLivraisonVm = new ConditionLivraisonViewModel();

            ConditionLivraisonVm._ConditionLivraison = new ConditionLivraison();
            ConditionLivraisonVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConditionLivraison", Model = ConditionLivraisonVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ConditionLivraisonViewModel ConditionLivraisonVm = new ConditionLivraisonViewModel();

            ConditionLivraisonVm._ConditionLivraison = JSON.Deserialize<ConditionLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ConditionLivraisonVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConditionLivraison", Model = ConditionLivraisonVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ConditionLivraisonViewModel ConditionLivraisonVm = new ConditionLivraisonViewModel();

            ConditionLivraisonVm._ConditionLivraison = JSON.Deserialize<ConditionLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ConditionLivraisonVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConditionLivraison", Model = ConditionLivraisonVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                ConditionLivraison condLivr = new ConditionLivraison();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    condLivr.IsNew = true;
                else
                {
                    condLivr.IsNew = false;

                    condLivr.fnGet(int.Parse(GetFormValue("TxtConditionLivraisonID")));

                    if (condLivr == null || condLivr.ID == 0)
                        throw new Exception("SubmitFormMethod : Delivery Condition load failed.");
                }

                condLivr = MapFormToObject(condLivr);

                bool result = condLivr.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeConditionLivraison");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, condLivr);
                        X.GetCmp<RowSelectionModel>("rowSelectionConditionLivraison").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(condLivr.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(condLivr);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormConditionLivraison").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Delivery Condition : Data Validation",
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
            var liste = new ConditionLivraison().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                ConditionLivraison condLivr = JSON.Deserialize<ConditionLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = condLivr.fnGet(condLivr.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Delivery Condition loading failed.");
                //(string)Session["userName"];
                condLivr.UtilisateurModification = (string)Session["userName"];

                if (condLivr.Desactive)
                    result = condLivr.fnActivate();
                else
                    result = condLivr.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Delivery Condition, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeConditionLivraison");

                    ModelProxy mProxy = mstore.GetById(condLivr.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(condLivr);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Delivery Condition : OnActivateDeactivate",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ConditionLivraisonCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeConditionLivraison");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ConditionLivraisonCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private ConditionLivraison MapFormToObject(ConditionLivraison condLivr)
        {
            condLivr.Designation = X.GetCmp<TextField>("TxtDesignationConditionLivraison").Text;
            //(string)Session["userName"]
            condLivr.UtilisateurCreation = (string)Session["userName"];
            condLivr.UtilisateurModification = (string)Session["userName"];

            return condLivr;
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
            X.GetCmp<RowSelectionModel>("rowSelectionConditionLivraison").DeselectAll();
        }


        #endregion
    }
}