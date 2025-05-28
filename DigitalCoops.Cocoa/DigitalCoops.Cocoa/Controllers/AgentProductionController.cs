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
    public class AgentProductionController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: AgentProduction
        public ActionResult Index()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{23FA6EFB-88C4-49EE-AE4D-86672D517F93}"), UserName);
            HashSet<Guid> mlisteFonctions = new HashSet<Guid>(mListe.Cast<Fonction>().Select(f => f.ID).ToList());

            if (mlisteFonctions.Contains(Guid.Parse("{32AC3C28-5850-4C43-89C5-641DAC2E99C9}")))
                X.GetCmp<Button>("btnNewAgentProduction").Enable();
            else
                X.GetCmp<Button>("btnNewAgentProduction").Disable();

            //if (mlisteFonctions.Contains(Guid.Parse("{DF2C998A-9232-48ED-B403-6DB46289BE0A}")))
            //    X.GetCmp<MenuItem>("mnuPrintMovement").Enable();
            //else
            //    X.GetCmp<MenuItem>("mnuPrintMovement").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{60C6A1C1-00A0-4C55-806B-D25ED0DBBEED}")))
                X.GetCmp<MenuItem>("mnuExportAgentProduction").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportAgentProduction").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{D8C0175B-A1EC-48B4-B466-EC482F677974}")))
                X.GetCmp<Hidden>("AghiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("AghiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{12801446-0817-45B9-A08E-393CA30F1724}")))
                X.GetCmp<Hidden>("AghiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("AghiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{C252279B-C4D7-4811-A14D-6837F5F16FA9}")))
                X.GetCmp<Hidden>("AghiddenPermActiver").SetValue(true);
            else
                X.GetCmp<Hidden>("AghiddenPermActiver").SetValue(false);
            return View();
        }

        public ActionResult LoadActiveAgentProduction(string typeID = "-1")
        {
            List<DataPersist> mliste = new AgentProduction().fnSelect(0, int.Parse(typeID), -1);

            return this.Store(mliste);
        }

        public ActionResult LoadActiveAgentProductionAll(string typeID = "-1")
        {
            List<DataPersist> mliste = new AgentProduction().fnSelect(0, int.Parse(typeID), -1);

            AgentProduction AgentProduction = new AgentProduction();

            AgentProduction.ID = -1;
            AgentProduction.Nom = "{Tous}";

            mliste.Insert(0, AgentProduction);
            AgentProduction = mliste[0] as AgentProduction;

            return this.Store(mliste);
        }

        public ActionResult OnAdd()
        {            
            AgentProductionViewModel AgentProductionVm = new AgentProductionViewModel();

            AgentProductionVm._AgentProduction = new AgentProduction();
            AgentProductionVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAgentProduction", Model = AgentProductionVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {            
            AgentProductionViewModel AgentProductionVm = new AgentProductionViewModel();

            AgentProductionVm._AgentProduction = JSON.Deserialize<AgentProduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            AgentProductionVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAgentProduction", Model = AgentProductionVm };
        }

        public ActionResult OnConsult(string ItemSelected)
        {          
            AgentProductionViewModel AgentProductionVm = new AgentProductionViewModel();

            AgentProductionVm._AgentProduction = JSON.Deserialize<AgentProduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            AgentProductionVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAgentProduction", Model = AgentProductionVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                AgentProduction AgentProduction = new AgentProduction();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    AgentProduction.IsNew = true;
                else
                {
                    AgentProduction.IsNew = false;

                    AgentProduction.fnGet(int.Parse(GetFormValue("TxtAgentProductionID")));

                    if (AgentProduction == null || AgentProduction.ID == 0)
                        throw new Exception("SubmitFormMethod : Agent Production load failed.");
                }

                AgentProduction = MapFormToObject(AgentProduction);

                bool result = AgentProduction.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeAgentProduction");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, AgentProduction);
                        X.GetCmp<RowSelectionModel>("rowAgentProduction").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(AgentProduction.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(AgentProduction);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormAgentProduction").Close();                                        
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

        public ActionResult Select(StoreRequestParameters parameters, string ItemStatus, string ItemPeutApprouver, string ItemType)
        {
            int status = -1;
            int peutapprouver = -1;
            int TypeAgent = GetCriteriaValue(ItemType);

            if (string.IsNullOrEmpty(ItemStatus))
            {
                status = -1;
            }
            else if (ItemStatus == "true")
            {
                status = 0;
            }

            if (string.IsNullOrEmpty(ItemPeutApprouver))
            {
                peutapprouver = -1;
            }
            else if (ItemPeutApprouver == "true")
            {
                peutapprouver = 1;
            }
            var liste = new AgentProduction().fnSelect(status, TypeAgent, peutapprouver);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                AgentProduction AgentProduction = JSON.Deserialize<AgentProduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = AgentProduction.fnGet(AgentProduction.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Agent Production loading failed.");
                //(string)Session["userName"];
                AgentProduction.UtilisateurModification = (string)Session["userName"];

                if (AgentProduction.Desactive)
                    result = AgentProduction.fnActivate();
                else
                    result = AgentProduction.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Agent Production, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeAgentProduction");

                    ModelProxy mProxy = mstore.GetById(AgentProduction.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(AgentProduction);

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
            FormPanel mform = X.GetCmp<FormPanel>("AgentProductionCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus, string ItemPeutApprouver, string ItemType)
        {
            Store mstore = X.GetCmp<Store>("storeListeAgentProduction");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus),
                                new Ext.Net.Parameter("ItemType", ItemType),
                                new Ext.Net.Parameter("ItemPeutApprouver", ItemPeutApprouver)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("AgentProductionCP");

            mform.Collapsed = true;

            return this.Direct();
        }

        private AgentProduction MapFormToObject(AgentProduction AgentProduction)
        {
            AgentProduction.Nom = X.GetCmp<TextField>("TxtNomAgent").Text;
            AgentProduction.PeuxApprouver = bool.Parse(X.GetCmp<Checkbox>("ChkAgentPeutApprouver").Value.ToString());
            AgentProduction.AgentProductionType = new AgentProductionType();

            AgentProduction.AgentProductionType.ID = int.Parse(GetFormValue("cmbTypeAgent"));
            AgentProduction.AgentProductionType.Designation = X.GetCmp<ComboBox>("cmbTypeAgent").SelectedItem.Text.ToString();                        
            AgentProduction.UtilisateurCreation = (string)Session["userName"];
            AgentProduction.UtilisateurModification = (string)Session["userName"];

            return AgentProduction;
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
            X.GetCmp<RowSelectionModel>("rowAgentProduction").DeselectAll();
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