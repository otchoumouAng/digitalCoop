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
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class ConteneurTypeController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: ConteneurType
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{1DFC6786-F4E8-4D5C-828D-B443D5CEC775}", UserName) == false)
                X.GetCmp<Button>("btnNewConteneurType").Disable();
            else
                X.GetCmp<Button>("btnNewConteneurType").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{C03FC1AB-181E-44C8-85F1-5216F7BB9086}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListConteneurType").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListConteneurType").Enable();

            X.GetCmp<Hidden>("CthiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{1DFC6786-F4E8-4D5C-828D-B443D5CEC775}", UserName));
            X.GetCmp<Hidden>("CthiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{7EDA666F-53A7-44EC-BEED-BCDCC23EEDE6}", UserName));
            X.GetCmp<Hidden>("CthiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{9550410E-878C-4F22-A9D7-D8918B2A25A7}", UserName));
            X.GetCmp<Hidden>("CthiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{A7E87CAA-F85A-4627-A4E6-3EA55477C9C8}", UserName));
            X.GetCmp<Hidden>("CthiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{C03FC1AB-181E-44C8-85F1-5216F7BB9086}", UserName));
            X.GetCmp<Hidden>("CthiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{C369A516-CD90-49BA-900E-53F904B067CE}", UserName));
            return View();
        }

        public ActionResult LoadActive()
        {
            List<DataPersist> mList = new ConteneurType().fnSelect(0);
            return this.Store(mList);
        }
        public ActionResult LoadConteneurTypeAll()
        {
            List<DataPersist> mList = new ConteneurType().fnSelect(0);

            ConteneurType mclass = new ConteneurType();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as ConteneurType;

            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ConteneurTypeViewModel ConteneurTypeVm = new ConteneurTypeViewModel();

            ConteneurTypeVm._ConteneurType = new ConteneurType();
            ConteneurTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConteneurType", Model = ConteneurTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ConteneurTypeViewModel ConteneurTypeVm = new ConteneurTypeViewModel();

            ConteneurTypeVm._ConteneurType = JSON.Deserialize<ConteneurType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ConteneurTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConteneurType", Model = ConteneurTypeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ConteneurTypeViewModel ConteneurTypeVm = new ConteneurTypeViewModel();

            ConteneurTypeVm._ConteneurType = JSON.Deserialize<ConteneurType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ConteneurTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConteneurType", Model = ConteneurTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                ConteneurType conteneur = new ConteneurType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    conteneur.IsNew = true;
                else
                {
                    conteneur.IsNew = false;

                    conteneur.fnGet(int.Parse(GetFormValue("TxtConteneurTypeID")));

                    if (conteneur == null || conteneur.ID == 0)
                        throw new Exception("SubmitFormMethod : Type Of Container load failed.");
                }

                conteneur = MapFormToObject(conteneur);

                bool result = conteneur.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeConteneurType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, conteneur);
                        X.GetCmp<RowSelectionModel>("rowSelectionConteneurType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(conteneur.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(conteneur);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormConteneurType").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type Of Container : Data Validation",
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
            var liste = new ConteneurType().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                ConteneurType conteneur = JSON.Deserialize<ConteneurType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = conteneur.fnGet(conteneur.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type Of Container loading failed.");
                //(string)Session["userName"];
                conteneur.UtilisateurModification = (string)Session["userName"];

                if (conteneur.Desactive)
                    result = conteneur.fnActivate();
                else
                    result = conteneur.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type Of Container, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeConteneurType");

                    ModelProxy mProxy = mstore.GetById(conteneur.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(conteneur);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type Of Container : OnActivateDeactivate",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ConteneurTypeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeConteneurType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ConteneurTypeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private ConteneurType MapFormToObject(ConteneurType conteneur)
        {
            conteneur.Designation = X.GetCmp<TextField>("TxtDesignationConteneurType").Text;
            //(string)Session["userName"]
            conteneur.UtilisateurCreation = (string)Session["userName"];
            conteneur.UtilisateurModification = (string)Session["userName"];

            return conteneur;
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
            X.GetCmp<RowSelectionModel>("rowSelectionConteneurType").DeselectAll();
        }


        #endregion
    }
}