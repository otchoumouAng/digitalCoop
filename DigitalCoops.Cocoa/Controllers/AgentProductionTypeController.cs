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
    public class AgentProductionTypeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: AgentProductionType
        public ActionResult Index()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{D028AEEA-1294-46C0-A3F3-741ADDF082C0}"), UserName);
            HashSet<Guid> mlisteFonctions = new HashSet<Guid>(mListe.Cast<Fonction>().Select(f => f.ID).ToList());

            if (mlisteFonctions.Contains(Guid.Parse("{132F5CA6-2B99-4A17-B4E0-D9B1D15963BC}")))
                X.GetCmp<Button>("btnNewAgentProductionType").Enable();
            else
                X.GetCmp<Button>("btnNewAgentProductionType").Disable();

            //if (mlisteFonctions.Contains(Guid.Parse("{530EF9E7-2048-4EB6-8010-358EAF8A661C}")))
            //    X.GetCmp<MenuItem>("mnuPrintMovement").Enable();
            //else
            //    X.GetCmp<MenuItem>("mnuPrintMovement").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{B0884197-B3B2-4132-8D9A-431029383299}")))
                X.GetCmp<MenuItem>("mnuExportAgentProductionType").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportAgentProductionType").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{8F66A603-67F7-4238-9769-B2E174BFE820}")))
                X.GetCmp<Hidden>("AthiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("AthiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{558BB4A6-111C-4D49-9EFE-E0A4DEC24133}")))
                X.GetCmp<Hidden>("AthiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("AthiddenPermModifier").SetValue(false);            

            if (mlisteFonctions.Contains(Guid.Parse("{B7073AF8-2618-449C-BED8-666313798553}")))
                X.GetCmp<Hidden>("AthiddenPermActiver").SetValue(true);
            else
                X.GetCmp<Hidden>("AthiddenPermActiver").SetValue(false);
            return View();
        }

        public ActionResult LoadActiveAgentProductionType(string typeID = "-1")
        {
            List<DataPersist> mliste = new AgentProductionType().fnSelect(0);

            return this.Store(mliste);
        }

        public ActionResult LoadActiveAgentProductionTypeAll(string typeID = "-1")
        {
            List<DataPersist> mliste = new AgentProductionType().fnSelect(0);

            AgentProductionType AgentProductionType = new AgentProductionType();

            AgentProductionType.ID = -1;
            AgentProductionType.Designation = "{Tous}";

            mliste.Insert(0, AgentProductionType);
            AgentProductionType = mliste[0] as AgentProductionType;

            return this.Store(mliste);
        }

        public ActionResult OnAdd()
        {            
            AgentProductionTypeViewModel AgentProductionTypeVm = new AgentProductionTypeViewModel();

            AgentProductionTypeVm._AgentProductionType = new AgentProductionType();
            AgentProductionTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAgentProductionType", Model = AgentProductionTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {            
            AgentProductionTypeViewModel AgentProductionTypeVm = new AgentProductionTypeViewModel();

            AgentProductionTypeVm._AgentProductionType = JSON.Deserialize<AgentProductionType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            AgentProductionTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAgentProductionType", Model = AgentProductionTypeVm };
        }

        public ActionResult OnConsult(string ItemSelected)
        {          
            AgentProductionTypeViewModel AgentProductionTypeVm = new AgentProductionTypeViewModel();

            AgentProductionTypeVm._AgentProductionType = JSON.Deserialize<AgentProductionType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            AgentProductionTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAgentProductionType", Model = AgentProductionTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                AgentProductionType AgentProductionType = new AgentProductionType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    AgentProductionType.IsNew = true;
                else
                {
                    AgentProductionType.IsNew = false;

                    AgentProductionType.fnGet(int.Parse(GetFormValue("TxtAgentProductionTypeID")));

                    if (AgentProductionType == null || AgentProductionType.ID == 0)
                        throw new Exception("SubmitFormMethod : Agent Production load failed.");
                }

                AgentProductionType = MapFormToObject(AgentProductionType);

                bool result = AgentProductionType.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeAgentProductionType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, AgentProductionType);
                        X.GetCmp<RowSelectionModel>("rowAgentProductionType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(AgentProductionType.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(AgentProductionType);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormAgentProductionType").Close();                    
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Agent Production : Data Validation",
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
            
            var liste = new AgentProductionType().fnSelect(status);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                AgentProductionType AgentProductionType = JSON.Deserialize<AgentProductionType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = AgentProductionType.fnGet(AgentProductionType.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Agent Production loading failed.");
                //(string)Session["userName"];
                AgentProductionType.UtilisateurModification = (string)Session["userName"];

                if (AgentProductionType.Desactive)
                    result = AgentProductionType.fnActivate();
                else
                    result = AgentProductionType.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Agent Production, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeAgentProductionType");

                    ModelProxy mProxy = mstore.GetById(AgentProductionType.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(AgentProductionType);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Agent Production : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("AgentProductionTypeCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus, string ItemPeutApprouver)
        {
            Store mstore = X.GetCmp<Store>("storeListeAgentProductionType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus),
                                new Ext.Net.Parameter("ItemPeutApprouver", ItemPeutApprouver)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("AgentProductionTypeCP");

            mform.Collapsed = true;

            return this.Direct();
        }

        private AgentProductionType MapFormToObject(AgentProductionType AgentProductionType)
        {
            AgentProductionType.Designation = X.GetCmp<TextField>("TxtDesignation").Text;                               
            AgentProductionType.UtilisateurCreation = (string)Session["userName"];
            AgentProductionType.UtilisateurModification = (string)Session["userName"];

            return AgentProductionType;
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
            X.GetCmp<RowSelectionModel>("rowAgentProductionType").DeselectAll();
        }


        #endregion

    }
}