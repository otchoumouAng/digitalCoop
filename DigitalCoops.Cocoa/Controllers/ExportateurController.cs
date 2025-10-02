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
    public class ExportateurController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Exportateur
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{2D2D0AB2-9F5D-4C70-AE69-B31F1BF4235A}", UserName) == false)
                X.GetCmp<Button>("btnNewExportateur").Disable();
            else
                X.GetCmp<Button>("btnNewExportateur").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{2D2D0AB2-9F5D-4C70-AE69-B31F1BF4235A}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListExportateur").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListExportateur").Enable();

            X.GetCmp<Hidden>("ExphiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{2D2D0AB2-9F5D-4C70-AE69-B31F1BF4235A}", UserName));
            X.GetCmp<Hidden>("ExphiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{57FD3545-2BA5-42C8-BD04-BA79F3314E0B}", UserName));
            X.GetCmp<Hidden>("ExphiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{25D5F0AB-48EF-49E0-898B-AF89B0FA5091}", UserName));
            X.GetCmp<Hidden>("ExphiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{321EC0AD-D928-41FB-8D23-D22AACD6F2B0}", UserName));
            X.GetCmp<Hidden>("ExphiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{2D2D0AB2-9F5D-4C70-AE69-B31F1BF4235A}", UserName));
            X.GetCmp<Hidden>("ExphiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{66855F2B-98CF-47C3-8064-B0EC9BA29081}", UserName));

            return View();
        }

        public ActionResult LoadExportateur()
        {
            List<DataPersist> mList = new Exportateur().fnSelect();

            Exportateur mclass = new Exportateur();

            if (mList.Count > 0)
                mclass = mList[0] as Exportateur;

            return this.Store(mList);
        }

        public ActionResult LoadExportateurActive()
        {
            List<DataPersist> mList = new Exportateur().fnSelect(0);

            Exportateur mclass = new Exportateur();

            if (mList.Count > 0)
                mclass = mList[0] as Exportateur;

            return this.Store(mList);
        }


        public ActionResult LoadExportateurAll()
        {
            List<DataPersist> mList = new Exportateur().fnSelect();

            Exportateur mclass = new Exportateur();

            mclass.ID = -1;
            mclass.Nom = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as Exportateur;

            return this.Store(mList);
        }

        public ActionResult LoadExportateurAllActive()
        {
            List<DataPersist> mList = new Exportateur().fnSelect(0);

            Exportateur mclass = new Exportateur();

            mclass.ID = -1;
            mclass.Nom = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as Exportateur;

            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {           
            ExportateurViewModel ExportateurVm = new ExportateurViewModel();

            ExportateurVm._Exportateur = new Exportateur();
            ExportateurVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormExportateur", Model = ExportateurVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {            
            ExportateurViewModel ExportateurVm = new ExportateurViewModel();

            ExportateurVm._Exportateur = JSON.Deserialize<Exportateur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ExportateurVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormExportateur", Model = ExportateurVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {            
            ExportateurViewModel ExportateurVm = new ExportateurViewModel();

            ExportateurVm._Exportateur = JSON.Deserialize<Exportateur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ExportateurVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormExportateur", Model = ExportateurVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Exportateur exportateur = new Exportateur();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    exportateur.IsNew = true;
                else
                {
                    exportateur.IsNew = false;

                    exportateur.fnGet(int.Parse(GetFormValue("TxtExportateurID")));

                    if (exportateur == null || exportateur.ID == 0)
                        throw new Exception("SubmitFormMethod : Exportateur, load failed.");
                }

                exportateur = MapFormToObject(exportateur);

                bool result = exportateur.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeExportateur");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, exportateur);
                        X.GetCmp<RowSelectionModel>("rowSelectionExportateur").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(exportateur.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(exportateur);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormExportateur").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Exportateur : Data Validation",
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
            var liste = new Exportateur().fnSelect(status);            
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Exportateur exportateur = JSON.Deserialize<Exportateur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = exportateur.fnGet(exportateur.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Exportateur loading failed.");
                //(string)Session["userName"];
                exportateur.UtilisateurModification = (string)Session["userName"];

                if (exportateur.Desactive)
                    result = exportateur.fnActivate();
                else
                    result = exportateur.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Exportateur, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeExportateur");

                    ModelProxy mProxy = mstore.GetById(exportateur.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(exportateur);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Exportateur : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ExportateurCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeExportateur");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ExportateurCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Exportateur MapFormToObject(Exportateur exportateur)
        {
            exportateur.Nom = X.GetCmp<TextField>("TxtNomExportateur").Text;
            exportateur.Adresse = X.GetCmp<TextField>("TxtAdresseExportateur").Text;
            exportateur.TelephoneFixe = X.GetCmp<TextField>("TxtTelephoneFixeExportateur").Text;
            exportateur.TelephoneMobile = X.GetCmp<TextField>("TxtTelephoneMobileExportateur").Text;
            exportateur.Fax = X.GetCmp<TextField>("TxtFaxExportateur").Text;
            exportateur.Prefixe = X.GetCmp<TextField>("TxtPrefixe").Text;
            exportateur.PrefixeFacture = X.GetCmp<TextField>("TxtPrefixeFacture").Text;
            exportateur.UtilisateurCreation = (string)Session["userName"];
            exportateur.UtilisateurModification = (string)Session["userName"];

            return exportateur;
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
            X.GetCmp<RowSelectionModel>("rowSelectionExportateur").DeselectAll();
        }


        #endregion

    }
}