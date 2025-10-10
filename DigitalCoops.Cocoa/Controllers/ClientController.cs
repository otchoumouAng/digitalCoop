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
    public class ClientController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";
        // GET: Client
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{31E8220A-C4AB-4B28-9470-D320E75572A0}", UserName) == false)
                X.GetCmp<Button>("btnNewClient").Disable();
            else
                X.GetCmp<Button>("btnNewClient").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{0E1DBB5D-3080-4F57-9147-2DC9ED7721F3}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListClient").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListClient").Enable();

            X.GetCmp<Hidden>("CthiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{31E8220A-C4AB-4B28-9470-D320E75572A0}", UserName));
            X.GetCmp<Hidden>("CthiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{86549E40-B668-4D43-83AB-8F9836CB27BF}", UserName));
            X.GetCmp<Hidden>("CthiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{E6048070-E4A2-48E8-AAF1-40802C322EC1}", UserName));
            X.GetCmp<Hidden>("CthiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{9DE49FE4-4FF8-4D65-A9E7-DEBD0EFB6914}", UserName));
            X.GetCmp<Hidden>("CthiddenPermCustomerExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{0E1DBB5D-3080-4F57-9147-2DC9ED7721F3}", UserName));
            X.GetCmp<Hidden>("CthiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{0163CA86-3488-4544-8BA4-600FED5A087A}", UserName));

            return View();
        }

        public ActionResult LoadClient()
        {
            List<DataPersist> mList = new Client().fnSelect();

            Client mclass = new Client();

            if (mList.Count > 0)
                mclass = mList[0] as Client;

            return this.Store(mList);
        }

        public ActionResult LoadClientActive()
        {
            List<DataPersist> mList = new Client().fnSelect(0);

            return this.Store(mList);
        }


        public ActionResult LoadClientAll()
        {
            List<DataPersist> mList = new Client().fnSelect();

            Client mclass = new Client();

            mclass.ID = -1;
            mclass.Nom = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as Client;

            return this.Store(mList);
        }

        public ActionResult LoadClientAllActive()
        {
            List<DataPersist> mList = new Client().fnSelect(0);

            Client mclass = new Client();

            mclass.ID = -1;
            mclass.Nom = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as Client;

            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ClientViewModel ClientVm = new ClientViewModel();

            ClientVm._Client = new Client();
            ClientVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormClient", Model = ClientVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ClientViewModel ClientVm = new ClientViewModel();

            ClientVm._Client = JSON.Deserialize<Client>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ClientVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormClient", Model = ClientVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ClientViewModel ClientVm = new ClientViewModel();

            ClientVm._Client = JSON.Deserialize<Client>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ClientVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormClient", Model = ClientVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Client Client = new Client();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    Client.IsNew = true;
                else
                {
                    Client.IsNew = false;

                    Client.fnGet(int.Parse(GetFormValue("TxtClientID")));

                    if (Client == null || Client.ID == 0)
                        throw new Exception("SubmitFormMethod : Exportateur, load failed.");
                }

                Client = MapFormToObject(Client);

                bool result = Client.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeClient");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, Client);
                        X.GetCmp<RowSelectionModel>("rowSelectionClient").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(Client.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(Client);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormClient").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Exportateur : Data Validation",
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
            var liste = new Client().fnSelect(status);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Client Client = JSON.Deserialize<Client>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = Client.fnGet(Client.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Exportateur loading failed.");
                //(string)Session["userName"];
                Client.UtilisateurModification = (string)Session["userName"];

                if (Client.Desactive)
                    result = Client.fnActivate();
                else
                    result = Client.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Exportateur, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeClient");

                    ModelProxy mProxy = mstore.GetById(Client.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(Client);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Exportateur : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ClientCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeClient");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ClientCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Client MapFormToObject(Client Client)
        {
            Client.Nom = X.GetCmp<TextField>("TxtNomClient").Text;
            Client.Adresse = X.GetCmp<TextField>("TxtAdresseClient").Text;
            Client.TelephoneFixe = X.GetCmp<TextField>("TxtTelephoneFixeClient").Text;
            Client.TelephoneMobile = X.GetCmp<TextField>("TxtTelephoneMobileClient").Text;
            Client.Fax = X.GetCmp<TextField>("TxtFaxClient").Text;
            //(string)Session["userName"]
            Client.UtilisateurCreation = (string)Session["userName"];
            Client.UtilisateurModification = (string)Session["userName"];

            return Client;
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
            X.GetCmp<RowSelectionModel>("rowSelectionClient").DeselectAll();
        }


        #endregion

    }
}