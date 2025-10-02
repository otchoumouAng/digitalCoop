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
    public class FournisseurTypeController : BaseController
    {

        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: FournisseurType
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{F5A11E2C-BE4C-453F-957B-52C7694F5AA1}", UserName) == false)
                X.GetCmp<Button>("btnNewFournisseurType").Disable();
            else
                X.GetCmp<Button>("btnNewFournisseurType").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{7E92A5B6-0CAE-4D90-8578-561B5EF1CE25}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListFrsType").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListFrsType").Enable();

            X.GetCmp<Hidden>("FrthiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{F5A11E2C-BE4C-453F-957B-52C7694F5AA1}", UserName));
            X.GetCmp<Hidden>("FrthiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{2D7BC9D4-D332-4878-9B69-08EA64ADACD1}", UserName));
            X.GetCmp<Hidden>("FrthiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{D2241C56-4805-4228-8DB4-EAF79B318CE3}", UserName));
            X.GetCmp<Hidden>("FrthiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{A014B0D5-15B6-46F3-82A6-C006016E546B}", UserName));
            X.GetCmp<Hidden>("FrthiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{7E92A5B6-0CAE-4D90-8578-561B5EF1CE25}", UserName));
            X.GetCmp<Hidden>("FrthiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{00E415CD-1A99-43EC-B640-56A347D70856}", UserName));

            return View();
        }

        public ActionResult LoadFournisseurType()
        {
            List<DataPersist> myListe = new FournisseurType().fnSelect(0);
            FournisseurType mclass = new FournisseurType();
            return this.Store(myListe);
        }

        public ActionResult LoadFournisseurTypeSite()
        {
            List<DataPersist> myListe = new FournisseurType().fnSelect(0,1);
            FournisseurType mclass = new FournisseurType();
            return this.Store(myListe);
        }

        public ActionResult LoadFournisseurTypeAll()
        {
            List<DataPersist> myListe = new FournisseurType().fnSelect(0);
            FournisseurType mclass = new FournisseurType();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            myListe.Insert(0, mclass);
            mclass = myListe[0] as FournisseurType;

            return this.Store(myListe);

        }

        public ActionResult LoadFournisseurTypeSiteAll()
        {
            List<DataPersist> myListe = new FournisseurType().fnSelect(0,1);
            FournisseurType mclass = new FournisseurType();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            myListe.Insert(0, mclass);
            mclass = myListe[0] as FournisseurType;

            return this.Store(myListe);

        }

        public ActionResult OnAdd()
        {
           

            FournisseurTypeViewModel FournisseurTypeVm = new FournisseurTypeViewModel();

            FournisseurTypeVm._FournisseurType = new FournisseurType();
            FournisseurTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFournisseurType", Model = FournisseurTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
           

            FournisseurTypeViewModel FournisseurTypeVm = new FournisseurTypeViewModel();

            FournisseurTypeVm._FournisseurType = JSON.Deserialize<FournisseurType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            FournisseurTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFournisseurType", Model = FournisseurTypeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
           

            FournisseurTypeViewModel FournisseurTypeVm = new FournisseurTypeViewModel();

            FournisseurTypeVm._FournisseurType = JSON.Deserialize<FournisseurType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            FournisseurTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFournisseurType", Model = FournisseurTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                FournisseurType fournisseurtype = new FournisseurType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    fournisseurtype.IsNew = true;
                else
                {
                    fournisseurtype.IsNew = false;

                    fournisseurtype.fnGet(int.Parse(GetFormValue("TxtFournisseurTypeID")));

                    if (fournisseurtype == null || fournisseurtype.ID == 0)
                        throw new Exception("SubmitFormMethod : Type de fournisseur load failed.");
                }

                fournisseurtype = MapFormToObject(fournisseurtype);

                bool result = fournisseurtype.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeFournisseurType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, fournisseurtype);
                        X.GetCmp<RowSelectionModel>("rowSelectionFournisseurType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(fournisseurtype.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(fournisseurtype);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormFournisseurType").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type de fournisseur : Data Validation",
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
            var liste = new FournisseurType().fnSelect(status);
            //var paging = GridStorePaging.SetRangePlants(parameters, liste);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                FournisseurType fournisseurtype = JSON.Deserialize<FournisseurType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = fournisseurtype.fnGet(fournisseurtype.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type de fournisseur loading failed.");
                //(string)Session["userName"];
                fournisseurtype.UtilisateurModification = (string)Session["userName"];

                if (fournisseurtype.Desactive)
                    result = fournisseurtype.fnActivate();
                else
                    result = fournisseurtype.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type de fournisseur, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeFournisseurType");

                    ModelProxy mProxy = mstore.GetById(fournisseurtype.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(fournisseurtype);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type de fournisseur : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("FournisseurTypeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeFournisseurType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("FournisseurTypeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private FournisseurType MapFormToObject(FournisseurType fournisseurtype)
        {
            fournisseurtype.Designation = X.GetCmp<TextField>("TxtDesignationFournisseurType").Text;
            fournisseurtype.EstVisibleSurSite = bool.Parse(X.GetCmp<Checkbox>("TxtVisibilityOnSite").Value.ToString());
            //(string)Session["userName"]
            fournisseurtype.UtilisateurCreation = (string)Session["userName"];
            fournisseurtype.UtilisateurModification = (string)Session["userName"];

            return fournisseurtype;
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
            X.GetCmp<RowSelectionModel>("rowSelectionFournisseurType").DeselectAll();
        }


        #endregion

    }
}