using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Security;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class MelangeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";
        // GET: Melange
        public ActionResult Index()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{71E6B76B-03C0-4451-B5C9-2DA4924DFA36}"), UserName);
            HashSet<Guid> mlisteFonctions = new HashSet<Guid>(mListe.Cast<Fonction>().Select(f => f.ID).ToList());

            if (mlisteFonctions.Contains(Guid.Parse("{13BC35C7-FC08-4534-B07C-1E03537F349B}")))
                X.GetCmp<Button>("btnNewMelange").Enable();
            else
                X.GetCmp<Button>("btnNewMelange").Disable();

            //if (mlisteFonctions.Contains(Guid.Parse("{ABE46146-DA90-4703-974E-B7CEC070690F}")))
            //    X.GetCmp<MenuItem>("mnuPrintMovement").Enable();
            //else
            //    X.GetCmp<MenuItem>("mnuPrintMovement").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{8873E737-54E5-4BE1-BD1A-42806D366A31}")))
                X.GetCmp<MenuItem>("mnuExportMelange").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportMelange").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{154582CA-7EB9-46C2-B882-C2A93F96A7C6}")))
                X.GetCmp<Hidden>("MlhiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("MlhiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{81B9EF1D-830C-44F1-A7FE-AEB49393B40E}")))
                X.GetCmp<Hidden>("MlhiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("MlhiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{D159DBC3-BA1A-4723-8E9D-DE472C8FE79C}")))
                X.GetCmp<Hidden>("MlhiddenPermActiver").SetValue(true);
            else
                X.GetCmp<Hidden>("MlhiddenPermActiver").SetValue(false);
            return View();
        }

        public ActionResult LoadActiveMelange(string typeID = "-1")
        {
            List<DataPersist> mliste = new Melange().fnSelect(0);

            return this.Store(mliste);
        }

        public ActionResult LoadActiveMelangeAll(string typeID = "-1")
        {
            List<DataPersist> mliste = new Melange().fnSelect(0);

            Melange Melange = new Melange();

            Melange.ID = -1;
            Melange.Designation = "{Tous}";

            mliste.Insert(0, Melange);
            Melange = mliste[0] as Melange;

            return this.Store(mliste);
        }

        public ActionResult OnAdd()
        {
            MelangeViewModel MelangeVm = new MelangeViewModel();

            MelangeVm._Melange = new Melange();
            MelangeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMelange", Model = MelangeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            MelangeViewModel MelangeVm = new MelangeViewModel();

            MelangeVm._Melange = JSON.Deserialize<Melange>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            MelangeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMelange", Model = MelangeVm };
        }

        public ActionResult OnConsult(string ItemSelected)
        {
            MelangeViewModel MelangeVm = new MelangeViewModel();

            MelangeVm._Melange = JSON.Deserialize<Melange>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            MelangeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMelange", Model = MelangeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Melange Melange = new Melange();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    Melange.IsNew = true;
                else
                {
                    Melange.IsNew = false;

                    Melange.fnGet(int.Parse(GetFormValue("TxtMelangeID")));

                    if (Melange == null || Melange.ID == 0)
                        throw new Exception("SubmitFormMethod : Blending load failed.");
                }

                Melange = MapFormToObject(Melange);

                bool result = Melange.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeMelange");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, Melange);
                        X.GetCmp<RowSelectionModel>("rowMelange").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(Melange.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(Melange);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormMelange").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Blending : Data Validation",
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

            var liste = new Melange().fnSelect(status);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Melange Melange = JSON.Deserialize<Melange>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = Melange.fnGet(Melange.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Blending loading failed.");
                //(string)Session["userName"];
                Melange.UtilisateurModification = (string)Session["userName"];

                if (Melange.Desactive)
                    result = Melange.fnActivate();
                else
                    result = Melange.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Blending, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeMelange");

                    ModelProxy mProxy = mstore.GetById(Melange.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(Melange);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Blending : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("MelangeCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeMelange");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("MelangeCP");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Melange MapFormToObject(Melange Melange)
        {
            Melange.Designation = X.GetCmp<TextField>("TxtDesignation").Text;

            Melange.UtilisateurCreation = (string)Session["userName"];
            Melange.UtilisateurModification = (string)Session["userName"];

            return Melange;
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
            X.GetCmp<RowSelectionModel>("rowMelange").DeselectAll();
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