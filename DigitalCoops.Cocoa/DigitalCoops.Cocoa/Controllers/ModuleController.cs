using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Security;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class ModuleController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Module
        public ActionResult Index()
        {
            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{B36E1598-27B1-4432-81B9-3304ECA6754A}", UserName);
            if (HasAccess.fnGetUserAccessStatus("{34A6F5A8-C297-4533-8013-BDB28FECCD9E}", UserName) == false)
                X.GetCmp<Button>("btnNewModule").Disable();
            else
                X.GetCmp<Button>("btnNewModule").Enable();

            if (HasAccess.fnGetUserAccessStatus("{0B9A7703-241E-40D3-925D-2BDCB61EFA8C}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportModuleList").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportModuleList").Enable();

            X.GetCmp<Hidden>("MohiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{34A6F5A8-C297-4533-8013-BDB28FECCD9E}", UserName));
            X.GetCmp<Hidden>("MohiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{1F5E97FA-238F-4A88-8C6B-8C4F746551E9}", UserName));
            X.GetCmp<Hidden>("MohiddenPermActiver").SetValue(HasAccess.fnGetUserAccessStatus("{B962C27F-D093-40CC-B74D-580E1A986894}", UserName));
            X.GetCmp<Hidden>("MohiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{981C204E-6C14-4960-9C19-122B0D9CDE4E}", UserName));
            X.GetCmp<Hidden>("MohiddenPermPrintModuleList").SetValue(HasAccess.fnGetUserAccessStatus("{DEE64198-08B6-4D49-9756-A98335A6C982}", UserName));
            X.GetCmp<Hidden>("MohiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{0B9A7703-241E-40D3-925D-2BDCB61EFA8C}", UserName));
            X.GetCmp<Hidden>("MohiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{B36E1598-27B1-4432-81B9-3304ECA6754A}", UserName));

            #endregion

            return View();
        }

        public ActionResult LoadModule()
        {
            List<DataPersist> listeModule = (new Module()).fnSelect();
            return this.Store(listeModule);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ModuleViewModel ModuleVm = new ModuleViewModel();

            ModuleVm._Module = new Module();
            ModuleVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormModule", Model = ModuleVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ModuleViewModel ModuleVm = new ModuleViewModel();

            ModuleVm._Module = JSON.Deserialize<Module>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ModuleVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormModule", Model = ModuleVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ModuleViewModel ModuleVm = new ModuleViewModel();

            ModuleVm._Module = JSON.Deserialize<Module>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ModuleVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormModule", Model = ModuleVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Module mClass = new Module();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtModuleID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Module load failed.");
                }

                mClass = MapFormToObject(mClass);

                bool result = mClass.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeModule");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionModule").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormModule").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Module : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult OnCancel()
        {
            return View();
        }

        public ActionResult SelectToUser(string ItemUser)
        {
            Guid id = Guid.Empty;
            if (!string.IsNullOrEmpty(ItemUser))
            {
                id = Guid.Parse(ItemUser);
            }
            var liste = new Module().fnSelectToUser(id);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Module Module = JSON.Deserialize<Module>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = Module.fnGet(Module.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Module loading failed.");

                Module.UtilisateurModification = (string)Session["userName"];                

                if (Module.IsDisable)
                    result = Module.fnActivate();
                else
                    result = Module.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Cancel Module failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeModule");

                    ModelProxy mProxy = mstore.GetById(Module.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(Module);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Module : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ModuleCriteriaPanel");
            mform.ToggleCollapse();
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
            var listeModule = new Module().fnSelect(status);

            //// Paging
            //int start = parameters.Start;

            //int limit = parameters.Limit;

            //if ((start + limit) > listeModule.Count)
            //{
            //    limit = listeModule.Count - start;
            //}

            //List<DataPersist> rangePlants = (start < 0 || limit < 0) ? listeModule : listeModule.GetRange(start, limit);
            //var paging = GridStorePaging.SetRangePlants(parameters, listeModule);
            return this.Store(listeModule);
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeModule");

            mstore.Reload();
            
            mstore.Reload(new Ext.Net.ParameterCollection()
                            {                               
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ModuleCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Module MapFormToObject(Module mClass)
        {
            mClass.Nom = X.GetCmp<TextField>("TxtNomModule").Text;

            mClass.Application = new Application();
            mClass.Application.ID = Guid.Parse(X.GetCmp<ComboBox>("ComboApplication").SelectedItem.Value);
            mClass.Application.Nom = X.GetCmp<ComboBox>("ComboApplication").SelectedItem.Text;

            mClass.Description = X.GetCmp<TextField>("TxtDescriptionModule").Text;
            mClass.Url = X.GetCmp<TextField>("TxtUrlModule").Text;
            mClass.IconModule = X.GetCmp<TextField>("TxtIconModule").Text;
            //mClass.IsDisable = false;
            //mClass.Order = double.Parse(X.GetCmp<NumberField>("TxtOrdreModule").RawText);
            //(string)Session["userName"];
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
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
            X.GetCmp<RowSelectionModel>("rowSelectionModule").DeselectAll();
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