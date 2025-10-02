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
    

    public class DestinationExportController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";
        // GET: DestinationExport
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{7A09C592-D888-431A-A968-674B0727DB1E}", UserName) == false)
                X.GetCmp<Button>("btnNewDestinationExport").Disable();
            else
                X.GetCmp<Button>("btnNewDestinationExport").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{AEDABE01-D088-4DC1-9679-7616DBDC0205}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListDestinationExport").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListDestinationExport").Enable();

            X.GetCmp<Hidden>("DehiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{7A09C592-D888-431A-A968-674B0727DB1E}", UserName));
            X.GetCmp<Hidden>("DehiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{E94F01F4-336A-4552-BA52-A219FF63B65D}", UserName));
            X.GetCmp<Hidden>("DehiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{23BD4A1F-71E8-4AAA-8CB8-B82B5190E5AD}", UserName));
            X.GetCmp<Hidden>("DehiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{51740795-2EA5-4F6D-805C-74AF42666D23}", UserName));
            X.GetCmp<Hidden>("DehiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{AEDABE01-D088-4DC1-9679-7616DBDC0205}", UserName));
            X.GetCmp<Hidden>("DehiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{B2B83D5D-11B5-48DE-8C89-A87927CB9A4C}", UserName));

            return View();
        }

        public ActionResult LoadDestinationActive()
        {
            List<DataPersist> liste = new DestinationExport().fnSelect(0);

            return this.Store(liste);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            DestinationExportViewModel DestinationExportVm = new DestinationExportViewModel();

            DestinationExportVm._DestinationExport = new DestinationExport();
            DestinationExportVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDestinationExport", Model = DestinationExportVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            DestinationExportViewModel DestinationExportVm = new DestinationExportViewModel();

            DestinationExportVm._DestinationExport = JSON.Deserialize<DestinationExport>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            DestinationExportVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDestinationExport", Model = DestinationExportVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            DestinationExportViewModel DestinationExportVm = new DestinationExportViewModel();

            DestinationExportVm._DestinationExport = JSON.Deserialize<DestinationExport>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            DestinationExportVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDestinationExport", Model = DestinationExportVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                DestinationExport compagnie = new DestinationExport();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    compagnie.IsNew = true;
                else
                {
                    compagnie.IsNew = false;

                    compagnie.fnGet(int.Parse(GetFormValue("TxtDestinationExportID")));

                    if (compagnie == null || compagnie.ID == 0)
                        throw new Exception("SubmitFormMethod : Destination Export load failed.");
                }

                compagnie = MapFormToObject(compagnie);

                bool result = compagnie.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeDestinationExport");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, compagnie);
                        X.GetCmp<RowSelectionModel>("rowSelectionDestinationExport").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(compagnie.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(compagnie);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormDestinationExport").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Destination Export : Data Validation",
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
            var liste = new DestinationExport().fnSelect(status);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                DestinationExport compagnie = JSON.Deserialize<DestinationExport>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = compagnie.fnGet(compagnie.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Destination Export loading failed.");

                compagnie.UtilisateurModification = (string)Session["userName"];

                if (compagnie.Desactive)
                    result = compagnie.fnActivate();
                else
                    result = compagnie.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Destination Export, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeDestinationExport");

                    ModelProxy mProxy = mstore.GetById(compagnie.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(compagnie);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Destination Export : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("DestinationExportCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeDestinationExport");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("DestinationExportCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private DestinationExport MapFormToObject(DestinationExport compagnie)
        {
            compagnie.Nom = X.GetCmp<TextField>("TxtDesignationDestinationExport").Text;

            compagnie.UtilisateurCreation = (string)Session["userName"];
            compagnie.UtilisateurModification = (string)Session["userName"];

            return compagnie;
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
            X.GetCmp<RowSelectionModel>("rowSelectionDestinationExport").DeselectAll();
        }

        public void CreateIconsList()
        {
            try
            {
                //Ext.Net.ResourceManager.RegisterGlobalIcon(Icon.PageCancel);

                //string url =  Ext.Net.ResourceManager.GetInstance().GetIconUrl(Icon.Tick);
            }
            catch (Exception ex)
            {

                X.Msg.Alert("frmLBC_ReceptionInDistrict_Overview : CreateIconList", ex.Message).Show();
            }
        }
        #endregion
    }
}