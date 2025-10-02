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
    public class ProtocoleController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Protocole
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{AD4F35A1-38A4-4E37-85DA-1729C69865DF}", UserName) == false)
                X.GetCmp<Button>("btnNewProtocole").Disable();
            else
                X.GetCmp<Button>("btnNewProtocole").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{A9709BDF-77DA-4711-A148-5CBAFEC500D1}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportProtocolList").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportProtocolList").Enable();

            X.GetCmp<Hidden>("PtchiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{AD4F35A1-38A4-4E37-85DA-1729C69865DF}", UserName));
            X.GetCmp<Hidden>("PtchiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{3C1B9114-53B4-4228-8D0A-4ABDDA2B7C81}", UserName));
            X.GetCmp<Hidden>("PtchiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{AC0811AD-D53A-4028-B08C-A6B8D6472A9A}", UserName));
            X.GetCmp<Hidden>("PtchiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{FCE95ACF-DB6C-45E3-B7C5-29A5395FFD36}", UserName));
            X.GetCmp<Hidden>("PtchiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{A9709BDF-77DA-4711-A148-5CBAFEC500D1}", UserName));
            X.GetCmp<Hidden>("PtchiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{A3CB1778-C74C-43F0-9D32-BADA734773E2}", UserName));

            return View();
        }

        public ActionResult LoadProtocole()
        {
            List<DataPersist> myListe = new Protocole().fnSelect();
            Protocole mclass = new Protocole();
           
            mclass = myListe[0] as Protocole;

            return this.Store(myListe);

        }

        public ActionResult LoadProtocoleAll()
        {
            List<DataPersist> myListe = new Protocole().fnSelect();
            Protocole mclass = new Protocole();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            myListe.Insert(0, mclass);
            mclass = myListe[0] as Protocole;

            return this.Store(myListe);

        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ProtocoleViewModel ProtocoleVm = new ProtocoleViewModel();

            ProtocoleVm._Protocole = new Protocole();
            ProtocoleVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProtocole", Model = ProtocoleVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ProtocoleViewModel ProtocoleVm = new ProtocoleViewModel();

            ProtocoleVm._Protocole = JSON.Deserialize<Protocole>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ProtocoleVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProtocole", Model = ProtocoleVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ProtocoleViewModel ProtocoleVm = new ProtocoleViewModel();

            ProtocoleVm._Protocole = JSON.Deserialize<Protocole>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ProtocoleVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProtocole", Model = ProtocoleVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Protocole protocole = new Protocole();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    protocole.IsNew = true;
                else
                {
                    protocole.IsNew = false;

                    protocole.fnGet(int.Parse(GetFormValue("TxtProtocoleID")));

                    if (protocole == null || protocole.ID == 0)
                        throw new Exception("SubmitFormMethod : Protocol load failed.");
                }

                protocole = MapFormToObject(protocole);

                bool result = protocole.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeProtocole");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, protocole);
                        X.GetCmp<RowSelectionModel>("rowSelectionProtocole").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(protocole.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(protocole);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormProtocole").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Protocol : Data Validation",
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
            var liste = new Protocole().fnSelect(status);
            //var paging = GridStorePaging.SetRangePlants(parameters, liste);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Protocole Protocole = JSON.Deserialize<Protocole>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = Protocole.fnGet(Protocole.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Protocol loading failed.");
                //(string)Session["userName"];
                Protocole.UtilisateurModification = (string)Session["userName"];

                if (Protocole.Desactive)
                    result = Protocole.fnActivate();
                else
                    result = Protocole.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Protocol, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeProtocole");

                    ModelProxy mProxy = mstore.GetById(Protocole.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(Protocole);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Protocol : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ProtocoleCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeProtocole");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ProtocoleCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Protocole MapFormToObject(Protocole protocole)
        {
            protocole.Designation = X.GetCmp<TextField>("TxtDesignationProtocole").Text;
            //(string)Session["userName"]
            protocole.UtilisateurCreation = (string)Session["userName"];
            protocole.UtilisateurModification = (string)Session["userName"];

            return protocole;
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
            X.GetCmp<RowSelectionModel>("rowSelectionProtocole").DeselectAll();
        }


        #endregion

    }
}