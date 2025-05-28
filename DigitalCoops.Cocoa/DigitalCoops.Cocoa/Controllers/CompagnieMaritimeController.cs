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
    public class CompagnieMaritimeController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: CompagnieMaritime
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{257FB2E3-B654-48B0-A342-9E3772EB6AFB}", UserName) == false)
                X.GetCmp<Button>("btnNewCompagnieMaritime").Disable();
            else
                X.GetCmp<Button>("btnNewCompagnieMaritime").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{41C91CFC-D750-4E24-9F6F-8D5AE16FB758}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListShipLine").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListShipLine").Enable();

            X.GetCmp<Hidden>("SlhiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{257FB2E3-B654-48B0-A342-9E3772EB6AFB}", UserName));
            X.GetCmp<Hidden>("SlhiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{5AE193F7-9691-459A-A648-BCF7D2AE6E73}", UserName));
            X.GetCmp<Hidden>("SlhiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{DB146889-F951-4B27-BBCE-5ACA9C397591}", UserName));
            X.GetCmp<Hidden>("SlhiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{D2CF2A20-7988-4477-9897-CB08D904B514}", UserName));
            X.GetCmp<Hidden>("SlhiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{41C91CFC-D750-4E24-9F6F-8D5AE16FB758}", UserName));
            X.GetCmp<Hidden>("SlhiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{4956F20C-E00B-4648-8B5D-5C30272E418A}", UserName));

            return View();
        }

        public ActionResult LoadActive()
        {
            List<DataPersist> mList = new CompagnieMaritime().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult LoadActiveAll()
        {
            List<DataPersist> mList = new CompagnieMaritime().fnSelect(0);
            CompagnieMaritime mclass = new CompagnieMaritime();

            mclass.ID = -1;
            mclass.Nom = "{Tous}";

            mList.Insert(0, mclass);           

            mclass = mList[0] as CompagnieMaritime;
            return this.Store(mList);
        }
        public ActionResult OnAdd()
        {            

            CompagnieMaritimeViewModel CompagnieMaritimeVm = new CompagnieMaritimeViewModel();
            ViewData["StatutForm"] = "0";
            CompagnieMaritimeVm._CompagnieMaritime = new CompagnieMaritime();
            CompagnieMaritimeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormCompagnieMaritime", Model = CompagnieMaritimeVm, ViewData = ViewData };
        }

        public ActionResult OnAddShiplineShipment()
        {

            CompagnieMaritimeViewModel CompagnieMaritimeVm = new CompagnieMaritimeViewModel();
            ViewData["StatutForm"] = "1";
            CompagnieMaritimeVm._CompagnieMaritime = new CompagnieMaritime();
            CompagnieMaritimeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormCompagnieMaritime", Model = CompagnieMaritimeVm, ViewData = ViewData };
        }

        public ActionResult OnEdit(string ItemSelected)
        {           

            CompagnieMaritimeViewModel CompagnieMaritimeVm = new CompagnieMaritimeViewModel();

            CompagnieMaritimeVm._CompagnieMaritime = JSON.Deserialize<CompagnieMaritime>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            CompagnieMaritimeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormCompagnieMaritime", Model = CompagnieMaritimeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {            

            CompagnieMaritimeViewModel CompagnieMaritimeVm = new CompagnieMaritimeViewModel();

            CompagnieMaritimeVm._CompagnieMaritime = JSON.Deserialize<CompagnieMaritime>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            CompagnieMaritimeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormCompagnieMaritime", Model = CompagnieMaritimeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                CompagnieMaritime compagnie = new CompagnieMaritime();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecModeCM").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    compagnie.IsNew = true;
                else
                {
                    compagnie.IsNew = false;
                    
                    compagnie.fnGet(int.Parse(GetFormValue("TxtCompagnieMaritimeID")));

                    if (compagnie == null || compagnie.ID == 0)
                        throw new Exception("SubmitFormMethod : Origin load failed.");
                }

                compagnie = MapFormToObject(compagnie);

                bool result = compagnie.fnUpdate();

                if (result)
                {
                    string StatutForm = X.GetCmp<Hidden>("hiddenStatutForm").Value.ToString();
                    if (StatutForm == "0")
                    {



                        Store mstore = X.GetCmp<Store>("storeListeCompagnieMaritime");
                        if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                        {
                            mstore.Insert(0, compagnie);
                            X.GetCmp<RowSelectionModel>("rowSelectionCompagnieMaritime").Select(0);
                        }
                        else
                        {
                            ModelProxy mProxy = mstore.GetById(compagnie.ID);

                            mProxy.BeginEdit();

                            mProxy.Set(compagnie);

                            mProxy.Commit();

                            mProxy.EndEdit();
                        }

                        X.GetCmp<Window>("FormCompagnieMaritime").Close();
                    }
                    else {
                        X.GetCmp<Window>("FormCompagnieMaritime").Close();
                        X.GetCmp<ComboBox>("_cmbShippingLine").InsertItem(0, compagnie.Nom, compagnie.ID);
                        X.GetCmp<ComboBox>("_cmbShippingLine").Value = compagnie.ID;
                    }
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Ship Line : Data Validation",
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
            var liste = new CompagnieMaritime().fnSelect(status);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                CompagnieMaritime compagnie = JSON.Deserialize<CompagnieMaritime>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = compagnie.fnGet(compagnie.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Ship Line loading failed.");

                compagnie.UtilisateurModification = (string)Session["userName"];

                if (compagnie.Desactive)
                    result = compagnie.fnActivate();
                else
                    result = compagnie.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Ship Line, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeCompagnieMaritime");

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
                    Title = "Ship Line : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CompagnieMaritimeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeCompagnieMaritime");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("CompagnieMaritimeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private CompagnieMaritime MapFormToObject(CompagnieMaritime compagnie)
        {
            compagnie.Nom = X.GetCmp<TextField>("TxtDesignationCompagnieMaritime").Text;
           
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
            X.GetCmp<RowSelectionModel>("rowSelectionCompagnieMaritime").DeselectAll();
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