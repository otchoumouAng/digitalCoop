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
    public class PeriodeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Periode
        public ActionResult Index()
        {
            #region Set Function's Access
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{F2885F40-2E9F-439B-8291-842CFA5FE60B}"), UserName);
            HashSet<Guid> mlisteFonctions = new HashSet<Guid>(mListe.Cast<Fonction>().Select(f => f.ID).ToList());

            if (mlisteFonctions.Contains(Guid.Parse("{F1650635-3614-421E-AECD-EC0DC0B8FC94}")))
                X.GetCmp<Button>("btnNewPeriode").Enable();
            else
                X.GetCmp<Button>("btnNewPeriode").Disable();

            //if (mlisteFonctions.Contains(Guid.Parse("{9B945574-328F-4B3C-9D30-7D2C64B36868}")))
            //    X.GetCmp<MenuItem>("mnuPrintMovement").Enable();
            //else
            //    X.GetCmp<MenuItem>("mnuPrintMovement").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{CAC57EB7-75FF-4D04-8B58-07E5ABD459A8}")))
                X.GetCmp<MenuItem>("mnuExportPeriode").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportPeriode").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{AA60A02F-647C-4FCA-B1F4-F842D46B980A}")))
                X.GetCmp<Hidden>("PerhiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("PerhiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{D4F7EDD6-563E-478C-95E1-186C4AB7AA08}")))
                X.GetCmp<Hidden>("PerhiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("PerhiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{15D6B397-7F1E-4E37-B17B-41A416B9B8A1}")))
                X.GetCmp<Hidden>("PerhiddenPermActiver").SetValue(true);
            else
                X.GetCmp<Hidden>("PerhiddenPermActiver").SetValue(false);

            #endregion

            return View();
        }

        public ActionResult LoadActivePeriode()
        {
            List<DataPersist> mliste = new Periode().fnSelect(0);

            return this.Store(mliste);
        }

        public ActionResult LoadActivePeriodeAll()
        {
            List<DataPersist> mliste = new Periode().fnSelect();

            Periode Periode = new Periode();

            Periode.ID = -1;
            Periode.Designation = "{Tous}";

            mliste.Insert(0, Periode);
            Periode = mliste[0] as Periode;

            return this.Store(mliste);
        }

        public ActionResult OnAdd()
        {
            PeriodeViewModel PeriodeVm = new PeriodeViewModel();

            PeriodeVm._Periode = new Periode();
            PeriodeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeriode", Model = PeriodeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            PeriodeViewModel PeriodeVm = new PeriodeViewModel();

            PeriodeVm._Periode = JSON.Deserialize<Periode>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PeriodeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeriode", Model = PeriodeVm };
        }

        public ActionResult OnConsult(string ItemSelected)
        {
            PeriodeViewModel PeriodeVm = new PeriodeViewModel();

            PeriodeVm._Periode = JSON.Deserialize<Periode>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PeriodeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeriode", Model = PeriodeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Periode Periode = new Periode();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    Periode.IsNew = true;
                else
                {
                    Periode.IsNew = false;

                    Periode.fnGet(int.Parse(GetFormValue("TxtPeriodeID")));

                    if (Periode == null || Periode.ID == 0)
                        throw new Exception("SubmitFormMethod : Blending load failed.");
                }

                Periode = MapFormToObject(Periode);

                bool result = Periode.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePeriode");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, Periode);
                        X.GetCmp<RowSelectionModel>("rowPeriode").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(Periode.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(Periode);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormPeriode").Close();
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

            var liste = new Periode().fnSelect(status);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Periode Periode = JSON.Deserialize<Periode>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = Periode.fnGet(Periode.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Blending loading failed.");
                //(string)Session["userName"];
                Periode.UtilisateurModification = (string)Session["userName"];

                if (Periode.Desactive)
                    result = Periode.fnActivate();
                else
                    result = Periode.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Blending, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePeriode");

                    ModelProxy mProxy = mstore.GetById(Periode.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(Periode);

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
            FormPanel mform = X.GetCmp<FormPanel>("PeriodeCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListePeriode");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("PeriodeCP");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Periode MapFormToObject(Periode Periode)
        {
            Periode.Designation = X.GetCmp<TextField>("TxtDesignation").Text;

            Periode.UtilisateurCreation = (string)Session["userName"];
            Periode.UtilisateurModification = (string)Session["userName"];

            return Periode;
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
            X.GetCmp<RowSelectionModel>("rowPeriode").DeselectAll();
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