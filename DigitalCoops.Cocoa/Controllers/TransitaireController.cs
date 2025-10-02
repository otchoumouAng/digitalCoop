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
    public class TransitaireController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Transitaire
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{9D205761-0773-47AD-9B26-A48228C2A23A}", UserName) == false)
                X.GetCmp<Button>("btnNewTransitaire").Disable();
            else
                X.GetCmp<Button>("btnNewTransitaire").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{043CD188-96B9-4554-9932-A3581C92829D}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListTransitaire").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListTransitaire").Enable();

            X.GetCmp<Hidden>("FwdhiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{9D205761-0773-47AD-9B26-A48228C2A23A}", UserName));
            X.GetCmp<Hidden>("FwdhiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{F9F47E71-A0F9-45B1-853D-C1A645A7BC99}", UserName));
            X.GetCmp<Hidden>("FwdhiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{2FE15872-506D-4305-9D19-F3923E46A8D9}", UserName));
            X.GetCmp<Hidden>("FwdhiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{8E5EC8E6-C9A0-44FB-B5D7-C8BBB5274B24}", UserName));
            X.GetCmp<Hidden>("FwdhiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{043CD188-96B9-4554-9932-A3581C92829D}", UserName));
            X.GetCmp<Hidden>("FwdhiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{8A0B46BF-A296-4721-A06D-0EEF94816C11}", UserName));

            return View();
        }

        public ActionResult LoadTransitaire()
        {
            List<DataPersist> mList = new Transitaire().fnSelect();

            Transitaire mclass = new Transitaire();

            if (mList.Count > 0)
               mclass = mList[0] as Transitaire;

                return this.Store(mList);
        }


        public ActionResult LoadTransitaireAll()
        {
            List<DataPersist> mList = new Transitaire().fnSelect();

            Transitaire mclass = new Transitaire();

            mclass.ID = -1;
            mclass.Nom = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as Transitaire;

            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {           
            TransitaireViewModel TransitaireVm = new TransitaireViewModel();

            TransitaireVm._Transitaire = new Transitaire();
            TransitaireVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormTransitaire", Model = TransitaireVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {            
            TransitaireViewModel TransitaireVm = new TransitaireViewModel();

            TransitaireVm._Transitaire = JSON.Deserialize<Transitaire>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            TransitaireVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormTransitaire", Model = TransitaireVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {            
            TransitaireViewModel TransitaireVm = new TransitaireViewModel();

            TransitaireVm._Transitaire = JSON.Deserialize<Transitaire>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            TransitaireVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormTransitaire", Model = TransitaireVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Transitaire transitaire = new Transitaire();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    transitaire.IsNew = true;
                else
                {
                    transitaire.IsNew = false;

                    transitaire.fnGet(int.Parse(GetFormValue("TxtTransitaireID")));

                    if (transitaire == null || transitaire.ID == 0)
                        throw new Exception("SubmitFormMethod : Forwarder, load failed.");
                }

                transitaire = MapFormToObject(transitaire);

                bool result = transitaire.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeTransitaire");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, transitaire);
                        X.GetCmp<RowSelectionModel>("rowSelectionTransitaire").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(transitaire.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(transitaire);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormTransitaire").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Forwarder : Data Validation",
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
            var liste = new Transitaire().fnSelect(status);
            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Transitaire transitaire = JSON.Deserialize<Transitaire>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = transitaire.fnGet(transitaire.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Forwarder, loading failed.");
                //(string)Session["userName"];
                transitaire.UtilisateurModification = (string)Session["userName"];

                if (transitaire.Desactive)
                    result = transitaire.fnActivate();
                else
                    result = transitaire.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Forwarder, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeTransitaire");

                    ModelProxy mProxy = mstore.GetById(transitaire.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(transitaire);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Forwarder : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("TransitaireCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeTransitaire");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("TransitaireCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Transitaire MapFormToObject(Transitaire transitaire)
        {
            transitaire.Nom = X.GetCmp<TextField>("TxtNomTransitaire").Text;
            transitaire.NomAgent = X.GetCmp<TextField>("TxtNomAgentTransitaire").Text;
            
            transitaire.UtilisateurCreation = (string)Session["userName"];
            transitaire.UtilisateurModification = (string)Session["userName"];

            return transitaire;
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
            X.GetCmp<RowSelectionModel>("rowSelectionTransitaire").DeselectAll();
        }


        #endregion

    }
}