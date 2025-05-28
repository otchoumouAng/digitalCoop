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
using Tms.Classes.Shared.Sales;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class PoleProvenanceController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: PoleProvenance
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{C604DB44-9A5E-4EDA-9EBC-74181F27990F}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{F5B0D90B-123B-4B6F-BCDF-62CF352F9176}")))
                X.GetCmp<Button>("btnNewPoleProvenance").Enable();
            else
                X.GetCmp<Button>("btnNewPoleProvenance").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{2E2D8187-2D36-4330-A174-1457A56B5B31}")))
                X.GetCmp<Button>("mnuExportListPoleProvenance").Enable();
            else
                X.GetCmp<Button>("mnuExportListPoleProvenance").Disable();

            //if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{5AD0D34B-5770-440D-92D2-52B47A94603C}")))
            //    X.GetCmp<Button>("mnuPrintListPoleProvenance").Enable();
            //else
            //    X.GetCmp<Button>("mnuPrintListPoleProvenance").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{E03A4612-9226-422A-A7D7-FA2AFE207FCF}")))
                X.GetCmp<Hidden>("pfhiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("pfhiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{83F3D70F-BF1A-47E7-8008-CC61418A6692}")))
                X.GetCmp<Hidden>("pfhiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("pfhiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{57528A3D-17DD-4E9D-96A4-01EDA52DCEDB}")))
                X.GetCmp<Hidden>("pfhiddenPermActiver").SetValue(true);
            else
                X.GetCmp<Hidden>("pfhiddenPermActiver").SetValue(false);                     

            return View();
        }

        public ActionResult Load()
        {
            List<DataPersist> mList = new PoleProvenance().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new PoleProvenance().fnSelect(0);
            PoleProvenance mclass = new PoleProvenance();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";
            mList.Insert(0, mclass);

            return this.Store(mList);

        }

        public ActionResult OnAdd()
        {            
            PoleProvenanceViewModel PoleProvenanceVm = new PoleProvenanceViewModel();

            PoleProvenanceVm._PoleProvenance = new PoleProvenance();
            PoleProvenanceVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPoleProvenance", Model = PoleProvenanceVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {            
            PoleProvenanceViewModel PoleProvenanceVm = new PoleProvenanceViewModel();

            PoleProvenanceVm._PoleProvenance = JSON.Deserialize<PoleProvenance>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PoleProvenanceVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPoleProvenance", Model = PoleProvenanceVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {            
            PoleProvenanceViewModel PoleProvenanceVm = new PoleProvenanceViewModel();

            PoleProvenanceVm._PoleProvenance = JSON.Deserialize<PoleProvenance>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PoleProvenanceVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPoleProvenance", Model = PoleProvenanceVm };
        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                PoleProvenance PoleProvenance = new PoleProvenance();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    PoleProvenance.IsNew = true;
                else
                {
                    PoleProvenance.IsNew = false;

                    PoleProvenance.fnGet(int.Parse(GetFormValue("TxtPoleProvenanceID")));

                    if (PoleProvenance == null || PoleProvenance.ID == 0)
                        throw new Exception("SubmitFormMethod : Quality load failed.");
                }

                PoleProvenance = MapFormToObject(PoleProvenance);

                bool result = PoleProvenance.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePoleProvenance");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, PoleProvenance);
                        X.GetCmp<RowSelectionModel>("rowSelectionPoleProvenance").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(PoleProvenance.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(PoleProvenance);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormPoleProvenance").Close();                                        
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Quality : Data Validation",
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
            var liste = new PoleProvenance().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                PoleProvenance PoleProvenance = JSON.Deserialize<PoleProvenance>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = PoleProvenance.fnGet(PoleProvenance.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Quality loading failed.");
                //(string)Session["userName"];
                PoleProvenance.UtilisateurModification = (string)Session["userName"];

                if (PoleProvenance.Desactive)
                    result = PoleProvenance.fnActivate();
                else
                    result = PoleProvenance.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Quality, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePoleProvenance");

                    ModelProxy mProxy = mstore.GetById(PoleProvenance.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(PoleProvenance);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Quality : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("PoleProvenanceCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListePoleProvenance");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("PoleProvenanceCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private PoleProvenance MapFormToObject(PoleProvenance PoleProvenance)
        {
            PoleProvenance.Designation = X.GetCmp<TextField>("TxtDesignationPoleProvenance").Text;
            PoleProvenance.Prefixe = X.GetCmp<TextField>("TxtPrefixePoleProvenance").Text;            
            PoleProvenance.UtilisateurCreation = (string)Session["userName"];
            PoleProvenance.UtilisateurModification = (string)Session["userName"];

            return PoleProvenance;
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
            X.GetCmp<RowSelectionModel>("rowSelectionPoleProvenance").DeselectAll();
        }


        #endregion
    }
}