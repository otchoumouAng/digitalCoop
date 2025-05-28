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
    public class StationEmpotageController : BaseController
    {
        // GET: StationEmpotage
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: StationEmpotage
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{DC9042F8-6449-4B2C-824C-FC392BBF6135}", UserName) == false)
                X.GetCmp<Button>("btnNewStationEmpotage").Disable();
            else
                X.GetCmp<Button>("btnNewStationEmpotage").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{15244856-9F92-4C1E-B06E-C09C7E4C0626}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListStationEmpotage").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListStationEmpotage").Enable();

            X.GetCmp<Hidden>("SehiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{DC9042F8-6449-4B2C-824C-FC392BBF6135}", UserName));
            X.GetCmp<Hidden>("SehiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{FA93BF20-706B-47AF-9F4E-D033132A0AB9}", UserName));
            X.GetCmp<Hidden>("SehiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{4DDEBB13-9FD7-4540-9AE4-F24453969C09}", UserName));
            X.GetCmp<Hidden>("SehiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{EF018EE3-F235-4A6D-8E7B-A4F1DDC2CFAF}", UserName));
            X.GetCmp<Hidden>("SehiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{15244856-9F92-4C1E-B06E-C09C7E4C0626}", UserName));
            X.GetCmp<Hidden>("SehiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{751AB9F7-638D-46A4-875A-D2BD79CD9D8E}", UserName));

            return View();
        }

        public ActionResult Load()
        {
            List<DataPersist> mList = new StationEmpotage().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult LoadActive()
        {
            List<DataPersist> mList = new StationEmpotage().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new StationEmpotage().fnSelect(0);
            StationEmpotage mclass = new StationEmpotage();

            mclass.ID = -1;
            mclass.Nom = "{Tous}";
            mList.Insert(0, mclass);

            return this.Store(mList);

        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            StationEmpotageViewModel StationEmpotageVm = new StationEmpotageViewModel();

            StationEmpotageVm._StationEmpotage = new StationEmpotage();
            StationEmpotageVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormStationEmpotage", Model = StationEmpotageVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            StationEmpotageViewModel StationEmpotageVm = new StationEmpotageViewModel();

            StationEmpotageVm._StationEmpotage = JSON.Deserialize<StationEmpotage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            StationEmpotageVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormStationEmpotage", Model = StationEmpotageVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            StationEmpotageViewModel StationEmpotageVm = new StationEmpotageViewModel();

            StationEmpotageVm._StationEmpotage = JSON.Deserialize<StationEmpotage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            StationEmpotageVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormStationEmpotage", Model = StationEmpotageVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                StationEmpotage StationEmpotage = new StationEmpotage();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    StationEmpotage.IsNew = true;
                else
                {
                    StationEmpotage.IsNew = false;

                    StationEmpotage.fnGet(int.Parse(GetFormValue("TxtStationEmpotageID")));

                    if (StationEmpotage == null || StationEmpotage.ID == 0)
                        throw new Exception("SubmitFormMethod : Origin load failed.");
                }

                StationEmpotage = MapFormToObject(StationEmpotage);

                bool result = StationEmpotage.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeStationEmpotage");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, StationEmpotage);
                        X.GetCmp<RowSelectionModel>("rowSelectionStationEmpotage").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(StationEmpotage.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(StationEmpotage);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormStationEmpotage").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stuffing Station : Data Validation",
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
            var liste = new StationEmpotage().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                StationEmpotage StationEmpotage = JSON.Deserialize<StationEmpotage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = StationEmpotage.fnGet(StationEmpotage.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Stuffing Station loading failed.");
                //(string)Session["userName"];
                StationEmpotage.UtilisateurModification = (string)Session["userName"];

                if (StationEmpotage.Desactive)
                    result = StationEmpotage.fnActivate();
                else
                    result = StationEmpotage.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Stuffing Station, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeStationEmpotage");

                    ModelProxy mProxy = mstore.GetById(StationEmpotage.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(StationEmpotage);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Origin : OnActivateDeactivate",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("StationEmpotageCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeStationEmpotage");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("StationEmpotageCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private StationEmpotage MapFormToObject(StationEmpotage StationEmpotage)
        {
            StationEmpotage.Nom = X.GetCmp<TextField>("TxtNomStationEmpotage").Text;
            //(string)Session["userName"]
            StationEmpotage.UtilisateurCreation = (string)Session["userName"];
            StationEmpotage.UtilisateurModification = (string)Session["userName"];

            return StationEmpotage;
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
            X.GetCmp<RowSelectionModel>("rowSelectionStationEmpotage").DeselectAll();
        }


        #endregion
    }
}