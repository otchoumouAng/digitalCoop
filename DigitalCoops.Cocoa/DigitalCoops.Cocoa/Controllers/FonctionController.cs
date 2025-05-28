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
    public class FonctionController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Fonction
        public ActionResult Index()
        {
            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{0991D34D-C3DD-41BF-907E-518C0525942A}", UserName);
            if (HasAccess.fnGetUserAccessStatus("{FB848D31-8674-4376-AE29-5E769556150E}", UserName) == false)
                X.GetCmp<Button>("btnNewFonction").Disable();
            else
                X.GetCmp<Button>("btnNewFonction").Enable();

            if (HasAccess.fnGetUserAccessStatus("{952387FB-9561-401B-A750-B3C12A42BF3D}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportFunctionList").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportFunctionList").Enable();

            X.GetCmp<Hidden>("FohiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{A0AFA827-C98F-43D0-B883-466A9B2A23A5}", UserName));
            X.GetCmp<Hidden>("FohiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{A1C87551-0E02-457A-BB36-CB8DBA6CB9A5}", UserName));
            X.GetCmp<Hidden>("FohiddenPermActiver").SetValue(HasAccess.fnGetUserAccessStatus("{BA12F645-7D54-42C8-8869-6C27C6725997}", UserName));
            X.GetCmp<Hidden>("FohiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{2AEA1A03-48FF-400C-B604-B22A464DA900}", UserName));
            X.GetCmp<Hidden>("FohiddenPermPrintFunctionList").SetValue(HasAccess.fnGetUserAccessStatus("{989FA360-ADD9-412D-B1B6-2FF988B403A9}", UserName));
            X.GetCmp<Hidden>("FohiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{854BBF82-9E3B-4776-BB47-74027DD61200}", UserName));
            X.GetCmp<Hidden>("FohiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{0991D34D-C3DD-41BF-907E-518C0525942A}", UserName));

            #endregion

            return View();
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            FonctionViewModel fonctionVm = new FonctionViewModel();

            fonctionVm._Fonction = new Fonction();
            fonctionVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFonction", Model = fonctionVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            FonctionViewModel fonctionVm = new FonctionViewModel();

            fonctionVm._Fonction = JSON.Deserialize<Fonction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            fonctionVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFonction", Model = fonctionVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            FonctionViewModel fonctionVm = new FonctionViewModel();

            fonctionVm._Fonction = JSON.Deserialize<Fonction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            fonctionVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFonction", Model = fonctionVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Fonction mClass = new Fonction();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtFonctionID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Function load failed.");
                }

                mClass = MapFormToObject(mClass);

                bool result = mClass.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeFonction");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionFonction").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormFonction").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Function : Data Validation",
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
            var listeFonction = new Fonction().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, listeFonction);
            return this.Store(listeFonction);
        }

        public ActionResult SelectToRole(string ItemRole)
        {
            Guid idRole = !string.IsNullOrEmpty(ItemRole) ? Guid.Parse(ItemRole) : Guid.Empty;
            var listeFonction = new Fonction().fnSelectToRole(idRole);
            return this.Store(listeFonction);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Fonction mClass = JSON.Deserialize<Fonction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = mClass.fnGet(mClass.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Function loading failed.");

                mClass.UtilisateurModification = (string)Session["userName"];

                if (mClass.IsDisable)
                    result = mClass.fnActivate();
                else
                    result = mClass.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Function Approval failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeFonction");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Function : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("FonctionCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeFonction");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("FonctionCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Fonction MapFormToObject(Fonction mClass)
        {
            mClass.Nom = X.GetCmp<TextField>("TxtNomFonction").Text;
            mClass.Description = X.GetCmp<TextField>("TxtDescriptionFonction").Text;

            mClass.Module = new Module();
            mClass.Module.ID = Guid.Parse(X.GetCmp<ComboBox>("CmbModule").SelectedItem.Value);

            mClass.Module.Nom = X.GetCmp<ComboBox>("CmbModule").SelectedItem.Text;
            mClass.Module.NomComplet = X.GetCmp<ComboBox>("CmbModule").SelectedItem.Text;
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
            X.GetCmp<RowSelectionModel>("rowSelectionFonction").DeselectAll();
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