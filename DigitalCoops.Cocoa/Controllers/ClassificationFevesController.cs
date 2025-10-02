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
    public class ClassificationFevesController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";
        // GET: Classification
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{D8754F4C-39EB-4B78-87C3-AA0EE42B4C72}", UserName) == false)
                X.GetCmp<Button>("btnNewClassificationFeves").Disable();
            else
                X.GetCmp<Button>("btnNewClassificationFeves").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{EA88DC57-5E3D-4247-AA5C-9B96CF4A4FBE}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListClassification").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListClassification").Enable();

            X.GetCmp<Hidden>("ClahiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{D8754F4C-39EB-4B78-87C3-AA0EE42B4C72}", UserName));
            X.GetCmp<Hidden>("ClahiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{F9318908-2446-470C-AC7B-E4ABDF3A97D6}", UserName));
            X.GetCmp<Hidden>("ClahiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{1A7AE6E2-8A2F-4976-9736-81C7808082DF}", UserName));
            X.GetCmp<Hidden>("ClahiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{CB238C73-3B8E-448C-9CF0-5392CBB92171}", UserName));
            X.GetCmp<Hidden>("ClahiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{EA88DC57-5E3D-4247-AA5C-9B96CF4A4FBE}", UserName));
            X.GetCmp<Hidden>("ClahiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{AE642379-538C-4E05-980F-7501BCC3258A}", UserName));

            return View();
        }

        public ActionResult LoadClassificationFeves()
        {
            List<DataPersist> liste = new ClassificationFeves().fnSelect(0);

            return this.Store(liste);
        }

        public ActionResult LoadClassificationFevesAll()
        {
            List<DataPersist> liste = new ClassificationFeves().fnSelect(0);

            ClassificationFeves feves = new ClassificationFeves();

            feves.ID = -1;
            feves.Designation = "{Tous}";

            liste.Insert(0, feves);
            feves = liste[0] as ClassificationFeves;

            return this.Store(liste);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ClassificationFevesViewModel ClassificationVm = new ClassificationFevesViewModel();

            ClassificationVm._ClassificationFeves = new ClassificationFeves();
            ClassificationVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormClassificationFeves", Model = ClassificationVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ClassificationFevesViewModel ClassificationVm = new ClassificationFevesViewModel();

            ClassificationVm._ClassificationFeves = JSON.Deserialize<ClassificationFeves>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ClassificationVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormClassificationFeves", Model = ClassificationVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ClassificationFevesViewModel ClassificationVm = new ClassificationFevesViewModel();

            ClassificationVm._ClassificationFeves = JSON.Deserialize<ClassificationFeves>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ClassificationVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormClassificationFeves", Model = ClassificationVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                ClassificationFeves classification = new ClassificationFeves();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    classification.IsNew = true;
                else
                {
                    classification.IsNew = false;

                    classification.fnGet(int.Parse(GetFormValue("TxtClassificationFevesID")));

                    if (classification == null || classification.ID == 0)
                        throw new Exception("SubmitFormMethod : Bean Classification load failed.");
                }

                classification = MapFormToObject(classification);

                bool result = classification.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeClassificationFeves");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, classification);
                        X.GetCmp<RowSelectionModel>("rowSelectionClassificationFeves").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(classification.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(classification);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormClassificationFeves").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bean Classification : Data Validation",
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
            var listeCampagne = new ClassificationFeves().fnSelect(status);
            //var paging = GridStorePaging.SetRangePlants(parameters, listeCampagne);
            return this.Store(listeCampagne);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                ClassificationFeves classification = JSON.Deserialize<ClassificationFeves>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = classification.fnGet(classification.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Bean Classification loading failed.");

                classification.UtilisateurModification = (string)Session["userName"];                

                if (classification.Desactive)
                    result = classification.fnActivate();
                else
                    result = classification.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Bean Classification, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeClassificationFeves");

                    ModelProxy mProxy = mstore.GetById(classification.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(classification);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bean Classification : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ClassificationFevesCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeClassificationFeves");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ClassificationFevesCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private ClassificationFeves MapFormToObject(ClassificationFeves classification)
        {
            classification.Designation = X.GetCmp<TextField>("TxtDesignationClassificationFeves").Text;

            classification.UtilisateurCreation = (string)Session["userName"];
            classification.UtilisateurModification = (string)Session["userName"];

            return classification;
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
            X.GetCmp<RowSelectionModel>("rowSelectionClassificationFeves").DeselectAll();
        }

        public void CreateIconsList()
        {
            try
            {
                //Ext.Net.ResourceManager.RegisterGlobalIcon(Icon.PageCancel);

                //string url =  Ext.Net.ResourceManager.GetInstance().GetIconUrl(Icon.Tick);
            }
            catch (Exception ex)
            {

                X.Msg.Alert("frmLBC_ReceptionInDistrict_Overview : CreateIconList", ex.Message).Show();
            }
        }
        #endregion

    }
}