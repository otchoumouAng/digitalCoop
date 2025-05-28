using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ext.Net.MVC;
using Tms.Components.Data;
using Tms.Classes.Shared;
using Ext.Net;
using Newtonsoft.Json;
using Tms.Classes.Security;

namespace Tms2017.MVC.Controllers
{
    public class PrixNegocieModeApplicationController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: PrixNegocieModeApplication
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{085AD934-1D05-470D-9B55-25329E2F0F8E}", UserName) == false)
                X.GetCmp<Button>("btnNewPrixNegocieModeApplication").Disable();
            else
                X.GetCmp<Button>("btnNewPrixNegocieModeApplication").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{913181B2-F138-4DCD-80BE-F276B891E723}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportSpModeList").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportSpModeList").Enable();

            X.GetCmp<Hidden>("SpmhiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{085AD934-1D05-470D-9B55-25329E2F0F8E}", UserName));
            X.GetCmp<Hidden>("SpmhiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{49A82BA1-548E-423B-A589-363185D146FC}", UserName));
            X.GetCmp<Hidden>("SpmhiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{179F900C-C9E6-4405-9129-999697620449}", UserName));
            X.GetCmp<Hidden>("SpmhiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{A62B6B90-4C7D-4383-82A8-1434F101588B}", UserName));
            X.GetCmp<Hidden>("SpmhiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{913181B2-F138-4DCD-80BE-F276B891E723}", UserName));
            X.GetCmp<Hidden>("SpmhiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{FC850CFB-9ACA-494D-99A0-DB2B6F8A8039}", UserName));

            return View();
        }

        public ActionResult LoadModeApplication()
        {
            List<DataPersist> mList = new PrixNegocieModeApplication().fnSelect(0);
            PrixNegocieModeApplication mClass = new PrixNegocieModeApplication();

            mClass = mList[0] as PrixNegocieModeApplication;
            return this.Store(mList);
        }

        public ActionResult LoadModeApplicationAll()
        {
            List<DataPersist> mList = new PrixNegocieModeApplication().fnSelect(0);
            PrixNegocieModeApplication mClass = new PrixNegocieModeApplication();

            mClass.ID = -1;
            mClass.Designation = "{Tous}";

            mList.Insert(0, mClass);

            mClass = mList[0] as PrixNegocieModeApplication;
            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PrixNegocieModeApplicationViewModel PrixNegocieModeApplicationVm = new PrixNegocieModeApplicationViewModel();

            PrixNegocieModeApplicationVm._PrixNegocieModeApplication = new PrixNegocieModeApplication();
            PrixNegocieModeApplicationVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrixNegocieModeApplication", Model = PrixNegocieModeApplicationVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PrixNegocieModeApplicationViewModel PrixNegocieModeApplicationVm = new PrixNegocieModeApplicationViewModel();

            PrixNegocieModeApplicationVm._PrixNegocieModeApplication = JSON.Deserialize<PrixNegocieModeApplication>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PrixNegocieModeApplicationVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrixNegocieModeApplication", Model = PrixNegocieModeApplicationVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PrixNegocieModeApplicationViewModel PrixNegocieModeApplicationVm = new PrixNegocieModeApplicationViewModel();

            PrixNegocieModeApplicationVm._PrixNegocieModeApplication = JSON.Deserialize<PrixNegocieModeApplication>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PrixNegocieModeApplicationVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrixNegocieModeApplication", Model = PrixNegocieModeApplicationVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                PrixNegocieModeApplication prixNegocieModeApplication = new PrixNegocieModeApplication();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    prixNegocieModeApplication.IsNew = true;
                else
                {
                    prixNegocieModeApplication.IsNew = false;

                    prixNegocieModeApplication.fnGet(int.Parse(GetFormValue("TxtPrixNegocieModeApplicationID")));

                    if (prixNegocieModeApplication == null || prixNegocieModeApplication.ID == 0)
                        throw new Exception("SubmitFormMethod : Prix Negocié Mode Of Application load failed.");
                }

                prixNegocieModeApplication = MapFormToObject(prixNegocieModeApplication);

                bool result = prixNegocieModeApplication.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePrixNegocieModeApplication");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, prixNegocieModeApplication);
                        X.GetCmp<RowSelectionModel>("rowSelectionPrixNegocieModeApplication").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(prixNegocieModeApplication.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(prixNegocieModeApplication);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormPrixNegocieModeApplication").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prix Negocié Mode Of Application : Data Validation",
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
            var liste = new PrixNegocieModeApplication().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                PrixNegocieModeApplication prixNegocieModeApplication = JSON.Deserialize<PrixNegocieModeApplication>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = prixNegocieModeApplication.fnGet(prixNegocieModeApplication.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Prix Negocié Mode Of Application loading failed.");
                //(string)Session["userName"];
                prixNegocieModeApplication.UtilisateurModification = (string)Session["userName"];

                if (prixNegocieModeApplication.Desactive)
                    result = prixNegocieModeApplication.fnActivate();
                else
                    result = prixNegocieModeApplication.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Prix Negocié Mode Of Application, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePrixNegocieModeApplication");

                    ModelProxy mProxy = mstore.GetById(prixNegocieModeApplication.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(prixNegocieModeApplication);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prix Negocié Mode Of Application : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("PrixNegocieModeApplicationCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListePrixNegocieModeApplication");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("PrixNegocieModeApplicationCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private PrixNegocieModeApplication MapFormToObject(PrixNegocieModeApplication prixNegocieModeApplication)
        {
            prixNegocieModeApplication.Designation = X.GetCmp<TextField>("TxtDesignationPrixNegocieModeApplication").Text;
            //(string)Session["userName"]
            prixNegocieModeApplication.UtilisateurCreation = (string)Session["userName"];
            prixNegocieModeApplication.UtilisateurModification = (string)Session["userName"];

            return prixNegocieModeApplication;
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
            X.GetCmp<RowSelectionModel>("rowSelectionPrixNegocieModeApplication").DeselectAll();
        }


        #endregion


    }
}