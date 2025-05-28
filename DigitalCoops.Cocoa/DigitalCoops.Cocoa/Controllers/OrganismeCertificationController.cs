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
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class OrganismeCertificationController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: OrganismeCertification
        public ActionResult Index()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{8FA515A7-9FE5-4D31-9120-F8C7252161D7}"), UserName);
            HashSet<Guid> mlisteFonctions = new HashSet<Guid>(mListe.Cast<Fonction>().Select(f => f.ID).ToList());

            if (mlisteFonctions.Contains(Guid.Parse("{24AAD2D2-5900-4732-8424-2F7D3E30B88C}")))
                X.GetCmp<Button>("btnNewOrganismeCertification").Enable();
            else
                X.GetCmp<Button>("btnNewOrganismeCertification").Disable();            

            if (mlisteFonctions.Contains(Guid.Parse("{89AB97BF-A09F-44C8-81E3-4A1D5D465C05}")))
                X.GetCmp<MenuItem>("mnuExportOrganismeCertification").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportOrganismeCertification").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{93269094-195B-4471-B4C7-DFFECBF9D888}")))
                X.GetCmp<Hidden>("OghiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("OghiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{CB72F5F9-CA2A-46E6-84BA-B67A96FA7F61}")))
                X.GetCmp<Hidden>("OghiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("OghiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{B99A975E-2B7C-495D-8A20-93495570AF98}")))
                X.GetCmp<Hidden>("OghiddenPermActiver").SetValue(true);
            else
                X.GetCmp<Hidden>("OghiddenPermActiver").SetValue(false);
            return View();
        }

        public ActionResult LoadActive()
        {
            List<DataPersist> mList = new OrganismeCertification().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {
            OrganismeCertificationViewModel OrganismeCertificationVm = new OrganismeCertificationViewModel();

            OrganismeCertificationVm._OrganismeCertification = new OrganismeCertification();
            OrganismeCertificationVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormOrganismeCertification", Model = OrganismeCertificationVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            OrganismeCertificationViewModel OrganismeCertificationVm = new OrganismeCertificationViewModel();

            OrganismeCertificationVm._OrganismeCertification = JSON.Deserialize<OrganismeCertification>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            OrganismeCertificationVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormOrganismeCertification", Model = OrganismeCertificationVm };
        }

        public ActionResult OnConsult(string ItemSelected)
        {
            OrganismeCertificationViewModel OrganismeCertificationVm = new OrganismeCertificationViewModel();

            OrganismeCertificationVm._OrganismeCertification = JSON.Deserialize<OrganismeCertification>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            OrganismeCertificationVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormOrganismeCertification", Model = OrganismeCertificationVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                OrganismeCertification OrganismeCertification = new OrganismeCertification();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    OrganismeCertification.IsNew = true;
                else
                {
                    OrganismeCertification.IsNew = false;

                    OrganismeCertification.fnGet(int.Parse(GetFormValue("TxtOrganismeCertificationID")));

                    if (OrganismeCertification == null || OrganismeCertification.ID == 0)
                        throw new Exception("SubmitFormMethod : Blending load failed.");
                }

                OrganismeCertification = MapFormToObject(OrganismeCertification);

                bool result = OrganismeCertification.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeOrganismeCertification");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, OrganismeCertification);
                        X.GetCmp<RowSelectionModel>("rowOrganismeCertification").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(OrganismeCertification.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(OrganismeCertification);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormOrganismeCertification").Close();
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

            var liste = new OrganismeCertification().fnSelect(status);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                OrganismeCertification OrganismeCertification = JSON.Deserialize<OrganismeCertification>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = OrganismeCertification.fnGet(OrganismeCertification.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Blending loading failed.");
                //(string)Session["userName"];
                OrganismeCertification.UtilisateurModification = (string)Session["userName"];

                if (OrganismeCertification.Desactive)
                    result = OrganismeCertification.fnActivate();
                else
                    result = OrganismeCertification.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Blending, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeOrganismeCertification");

                    ModelProxy mProxy = mstore.GetById(OrganismeCertification.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(OrganismeCertification);

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
            FormPanel mform = X.GetCmp<FormPanel>("OrganismeCertificationCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeOrganismeCertification");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("OrganismeCertificationCP");

            mform.Collapsed = true;

            return this.Direct();
        }

        private OrganismeCertification MapFormToObject(OrganismeCertification OrganismeCertification)
        {
            OrganismeCertification.Nom = X.GetCmp<TextField>("TxtNom").Text;

            OrganismeCertification.UtilisateurCreation = (string)Session["userName"];
            OrganismeCertification.UtilisateurModification = (string)Session["userName"];

            return OrganismeCertification;
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
            X.GetCmp<RowSelectionModel>("rowOrganismeCertification").DeselectAll();
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