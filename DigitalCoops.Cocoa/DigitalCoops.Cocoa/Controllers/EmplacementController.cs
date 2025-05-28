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
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class EmplacementController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";
        // GET: Emplacement
        public ActionResult Index()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{112F83B7-53E6-49CB-81B5-AB081AB1CA7E}"), UserName);
            HashSet<Guid> mlisteFonctions = new HashSet<Guid>(mListe.Cast<Fonction>().Select(f => f.ID).ToList());

            if (mlisteFonctions.Contains(Guid.Parse("{55F846D7-D968-42CA-84B0-6843B17E4227}")))
                X.GetCmp<Button>("btnNewEmplacement").Enable();
            else
                X.GetCmp<Button>("btnNewEmplacement").Disable();

            //if (mlisteFonctions.Contains(Guid.Parse("{EE090697-2AEC-4DB1-ABD9-C8A1BFD25112}")))
            //    X.GetCmp<MenuItem>("mnuPrintMovement").Enable();
            //else
            //    X.GetCmp<MenuItem>("mnuPrintMovement").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{AE8CFB60-0228-421F-8CCA-6D58432CECA2}")))
                X.GetCmp<MenuItem>("mnuExportEmplacement").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportEmplacement").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{DFA301EE-4282-4E1E-A16B-9A40DDEF497E}")))
                X.GetCmp<Hidden>("EmhiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("EmhiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{9E7A6B57-E0EC-4094-85E2-41530DED935D}")))
                X.GetCmp<Hidden>("EmhiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("EmhiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{81FB3813-DB4C-4A0D-93BC-7C0B359EFFD7}")))
                X.GetCmp<Hidden>("EmhiddenPermActiver").SetValue(true);
            else
                X.GetCmp<Hidden>("EmhiddenPermActiver").SetValue(false);
            return View();
        }

        public ActionResult LoadActiveEmplacement(string ItemMagasin = "")
        {
            int MagasinID = -1;
            if (!string.IsNullOrEmpty(ItemMagasin))
                MagasinID = int.Parse(ItemMagasin);
            List<DataPersist> mliste = new Emplacement().fnSelect(0,MagasinID);

            return this.Store(mliste);
        }

        public ActionResult LoadAllActiveEmplacement()
        {
            List<DataPersist> mliste = new Emplacement().fnSelect(0);

            Emplacement Emplacement = new Emplacement();

            Emplacement.ID = -1;
            Emplacement.Designation = "{Tous}";

            mliste.Insert(0, Emplacement);
            Emplacement = mliste[0] as Emplacement;

            return this.Store(mliste);
        }

        public ActionResult OnAdd()
        {
            EmplacementViewModel EmplacementVm = new EmplacementViewModel();

            EmplacementVm._Emplacement = new Emplacement();
            EmplacementVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormEmplacement", Model = EmplacementVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            EmplacementViewModel EmplacementVm = new EmplacementViewModel();

            EmplacementVm._Emplacement = JSON.Deserialize<Emplacement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            EmplacementVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormEmplacement", Model = EmplacementVm };
        }

        public ActionResult OnConsult(string ItemSelected)
        {
            EmplacementViewModel EmplacementVm = new EmplacementViewModel();

            EmplacementVm._Emplacement = JSON.Deserialize<Emplacement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            EmplacementVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormEmplacement", Model = EmplacementVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Emplacement Emplacement = new Emplacement();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    Emplacement.IsNew = true;
                else
                {
                    Emplacement.IsNew = false;

                    Emplacement.fnGet(int.Parse(GetFormValue("TxtEmplacementID")));

                    if (Emplacement == null || Emplacement.ID == 0)
                        throw new Exception("SubmitFormMethod : Location load failed.");
                }

                Emplacement = MapFormToObject(Emplacement);

                bool result = Emplacement.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeEmplacement");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, Emplacement);
                        X.GetCmp<RowSelectionModel>("rowEmplacement").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(Emplacement.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(Emplacement);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormEmplacement").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Location : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemStatus, string ItemMagasin)
        {
            int status = -1;            
            int magasinID = GetCriteriaValue(ItemMagasin);

            if (string.IsNullOrEmpty(ItemStatus))
            {
                status = -1;
            }
            else if (ItemStatus == "true")
            {
                status = 0;
            }
            
            var liste = new Emplacement().fnSelect(status, magasinID);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Emplacement Emplacement = JSON.Deserialize<Emplacement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = Emplacement.fnGet(Emplacement.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Location loading failed.");
                //(string)Session["userName"];
                Emplacement.UtilisateurModification = (string)Session["userName"];

                if (Emplacement.Desactive)
                    result = Emplacement.fnActivate();
                else
                    result = Emplacement.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Location, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeEmplacement");

                    ModelProxy mProxy = mstore.GetById(Emplacement.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(Emplacement);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Location : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("EmplacementCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus, string ItemPeutApprouver, string ItemMagasin)
        {
            Store mstore = X.GetCmp<Store>("storeListeEmplacement");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus),
                                new Ext.Net.Parameter("ItemMagasin", ItemMagasin)                                
                            });
            FormPanel mform = X.GetCmp<FormPanel>("EmplacementCP");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Emplacement MapFormToObject(Emplacement Emplacement)
        {
            Emplacement.Designation = X.GetCmp<TextField>("TxtDesignation").Text;            
            Emplacement.Magasin = new Magasin();

            Emplacement.Magasin.ID = int.Parse(GetFormValue("cmbMagasin"));
            Emplacement.Magasin.Designation = X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Text.ToString();

            Emplacement.UtilisateurCreation = (string)Session["userName"];
            Emplacement.UtilisateurModification = (string)Session["userName"];

            return Emplacement;
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
            X.GetCmp<RowSelectionModel>("rowEmplacement").DeselectAll();
        }

        private int GetCriteriaValue(string strComponent)
        {
            int value;

            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
                return value;
            else
                return -1;
        }
        #endregion

    }
}