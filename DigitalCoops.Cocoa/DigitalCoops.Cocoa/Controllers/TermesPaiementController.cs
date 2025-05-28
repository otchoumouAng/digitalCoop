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
    public class TermesPaiementController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: TermesPaiement
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{F47F0AE6-1521-4BEF-9F73-5E019FBA6D38}", UserName) == false)
                X.GetCmp<Button>("btnNewTermesPaiement").Disable();
            else
                X.GetCmp<Button>("btnNewTermesPaiement").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{E3A53B74-AE33-4E27-9911-1A61E9EFFDCB}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListTermesPaiement").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListTermesPaiement").Enable();

            X.GetCmp<Hidden>("PthiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{F47F0AE6-1521-4BEF-9F73-5E019FBA6D38}", UserName));
            X.GetCmp<Hidden>("PthiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{5C64EFBC-8FEE-41D2-881F-1A7F1BA08B6C}", UserName));
            X.GetCmp<Hidden>("PthiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{2F6AB822-8416-48E4-96D6-3B92D06A04A1}", UserName));
            X.GetCmp<Hidden>("PthiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{FE30E60F-96E9-4D92-80CF-658C22153E1C}", UserName));
            X.GetCmp<Hidden>("PthiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{E3A53B74-AE33-4E27-9911-1A61E9EFFDCB}", UserName));
            X.GetCmp<Hidden>("PthiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{AD5E1DCE-EC78-4610-B093-94A09C7369AB}", UserName));

            return View();
        }

        public ActionResult Load()
        {
            List<DataPersist> mList = new TermesPaiement().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new TermesPaiement().fnSelect(0);
            TermesPaiement mclass = new TermesPaiement();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";
            mList.Insert(0, mclass);

            return this.Store(mList);

        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            TermesPaiementViewModel TermesPaiementVm = new TermesPaiementViewModel();

            TermesPaiementVm._TermesPaiement = new TermesPaiement();
            TermesPaiementVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormTermesPaiement", Model = TermesPaiementVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            TermesPaiementViewModel TermesPaiementVm = new TermesPaiementViewModel();

            TermesPaiementVm._TermesPaiement = JSON.Deserialize<TermesPaiement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            TermesPaiementVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormTermesPaiement", Model = TermesPaiementVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            TermesPaiementViewModel TermesPaiementVm = new TermesPaiementViewModel();

            TermesPaiementVm._TermesPaiement = JSON.Deserialize<TermesPaiement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            TermesPaiementVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormTermesPaiement", Model = TermesPaiementVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                TermesPaiement terme = new TermesPaiement();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    terme.IsNew = true;
                else
                {
                    terme.IsNew = false;

                    terme.fnGet(int.Parse(GetFormValue("TxtTermesPaiementID")));

                    if (terme == null || terme.ID == 0)
                        throw new Exception("SubmitFormMethod : Type of prefinancing load failed.");
                }

                terme = MapFormToObject(terme);

                bool result = terme.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeTermesPaiement");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, terme);
                        X.GetCmp<RowSelectionModel>("rowSelectionTermesPaiement").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(terme.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(terme);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormTermesPaiement").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type of prefinancing : Data Validation",
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
            var liste = new TermesPaiement().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                TermesPaiement terme = JSON.Deserialize<TermesPaiement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = terme.fnGet(terme.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type of prefinancing loading failed.");
                //(string)Session["userName"];
                terme.UtilisateurModification = (string)Session["userName"];

                if (terme.Desactive)
                    result = terme.fnActivate();
                else
                    result = terme.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type of prefinancing, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeTermesPaiement");

                    ModelProxy mProxy = mstore.GetById(terme.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(terme);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type of prefinancing : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("TermesPaiementCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeTermesPaiement");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("TermesPaiementCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private TermesPaiement MapFormToObject(TermesPaiement terme)
        {
            terme.Designation = X.GetCmp<TextField>("TxtDesignationTermesPaiement").Text;
            //(string)Session["userName"]
            terme.UtilisateurCreation = (string)Session["userName"];
            terme.UtilisateurModification = (string)Session["userName"];

            return terme;
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
            X.GetCmp<RowSelectionModel>("rowSelectionTermesPaiement").DeselectAll();
        }


        #endregion
    }
}