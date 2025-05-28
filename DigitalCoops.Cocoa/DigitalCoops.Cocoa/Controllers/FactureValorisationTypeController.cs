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
    public class FactureValorisationTypeController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: FactureValorisationType
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{E84C1A39-DCCD-41B5-A805-8B9E31A60D03}", UserName) == false)
                X.GetCmp<Button>("btnNewFactureValorisationType").Disable();
            else
                X.GetCmp<Button>("btnNewFactureValorisationType").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{552BF00A-73F7-4F69-9A57-F2DA37343D2B}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListFactValoType").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListFactValoType").Enable();

            X.GetCmp<Hidden>("FvthiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{E84C1A39-DCCD-41B5-A805-8B9E31A60D03}", UserName));
            X.GetCmp<Hidden>("FvthiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{A783589B-D830-4125-9BB6-19BF6682AD31}", UserName));
            X.GetCmp<Hidden>("FvthiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{A5D58AF9-5DFF-4906-9EFD-C4843A886880}", UserName));
            X.GetCmp<Hidden>("FvthiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{E7C860E5-4118-4918-9A2A-5CB17D79B2F9}", UserName));
            X.GetCmp<Hidden>("FvthiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{552BF00A-73F7-4F69-9A57-F2DA37343D2B}", UserName));
            X.GetCmp<Hidden>("FvthiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{727A3D31-9A4D-4A73-921E-0520A6AFFC9F}", UserName));

            return View();
        }

        public ActionResult LoadFactureValorisationType()
        {
            List<DataPersist> myListe = new FactureValorisationType().fnSelect();
            FactureValorisationType mclass = new FactureValorisationType();

            return this.Store(myListe);

        }

        public ActionResult LoadFactureValorisationTypeAll()
        {
            List<DataPersist> myListe = new FactureValorisationType().fnSelect();
            FactureValorisationType mclass = new FactureValorisationType();

            mclass.Designation = "{Tous}";

            myListe.Insert(0, mclass);
            mclass = myListe[0] as FactureValorisationType;

            return this.Store(myListe);

        }


        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            FactureValorisationTypeViewModel FactureValorisationTypeVm = new FactureValorisationTypeViewModel();

            FactureValorisationTypeVm._FactureValorisationType = new FactureValorisationType();
            FactureValorisationTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFactureValorisationType", Model = FactureValorisationTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            FactureValorisationTypeViewModel FactureValorisationTypeVm = new FactureValorisationTypeViewModel();

            FactureValorisationTypeVm._FactureValorisationType = JSON.Deserialize<FactureValorisationType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            FactureValorisationTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFactureValorisationType", Model = FactureValorisationTypeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            FactureValorisationTypeViewModel FactureValorisationTypeVm = new FactureValorisationTypeViewModel();

            FactureValorisationTypeVm._FactureValorisationType = JSON.Deserialize<FactureValorisationType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            FactureValorisationTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFactureValorisationType", Model = FactureValorisationTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                FactureValorisationType facturevalorisationtype = new FactureValorisationType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    facturevalorisationtype.IsNew = true;
                else
                {
                    facturevalorisationtype.IsNew = false;

                    facturevalorisationtype.fnGet(int.Parse(GetFormValue("TxtFactureValorisationTypeID")));

                    if (facturevalorisationtype == null || facturevalorisationtype.ID == 0)
                        throw new Exception("SubmitFormMethod : Type de Valorisation Facture load failed.");
                }

                facturevalorisationtype = MapFormToObject(facturevalorisationtype);

                bool result = facturevalorisationtype.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeFactureValorisationType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, facturevalorisationtype);
                        X.GetCmp<RowSelectionModel>("rowSelectionFactureValorisationType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(facturevalorisationtype.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(facturevalorisationtype);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormFactureValorisationType").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type de Valorisation Facture : Data Validation",
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
            var liste = new FactureValorisationType().fnSelect(status);
            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                FactureValorisationType facturevalorisationtype = JSON.Deserialize<FactureValorisationType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = facturevalorisationtype.fnGet(facturevalorisationtype.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type de Valorisation Facture loading failed.");
                //(string)Session["userName"];
                facturevalorisationtype.UtilisateurModification = (string)Session["userName"];

                if (facturevalorisationtype.Desactive)
                    result = facturevalorisationtype.fnActivate();
                else
                    result = facturevalorisationtype.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type de Valorisation Facture, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeFactureValorisationType");

                    ModelProxy mProxy = mstore.GetById(facturevalorisationtype.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(facturevalorisationtype);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type de Valorisation Facture : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("FactureValorisationTypeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeFactureValorisationType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("FactureValorisationTypeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private FactureValorisationType MapFormToObject(FactureValorisationType facturevalorisationtype)
        {
            facturevalorisationtype.Designation = X.GetCmp<TextField>("TxtDesignationFactureValorisationType").Text;            
            //(string)Session["userName"]
            facturevalorisationtype.UtilisateurCreation = (string)Session["userName"];
            facturevalorisationtype.UtilisateurModification = (string)Session["userName"];

            return facturevalorisationtype;
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
            X.GetCmp<RowSelectionModel>("rowSelectionFactureValorisationType").DeselectAll();
        }


        #endregion

    }
}