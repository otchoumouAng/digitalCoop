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
    public class NavireController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Navire
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{FBC05237-02C2-48CE-A3A4-71887C8CB6DD}", UserName) == false)
                X.GetCmp<Button>("btnNewNavire").Disable();
            else
                X.GetCmp<Button>("btnNewNavire").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{5F4E81F2-E380-4279-879A-B9622414FCDC}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListNavire").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListNavire").Enable();

            X.GetCmp<Hidden>("ShiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{FBC05237-02C2-48CE-A3A4-71887C8CB6DD}", UserName));
            X.GetCmp<Hidden>("ShiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{684D683E-954E-4EBF-AE96-E6798806152C}", UserName));
            X.GetCmp<Hidden>("ShiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{D0A4459B-4A06-4964-A0D0-7D1839CA3CF0}", UserName));
            X.GetCmp<Hidden>("ShiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{E18892F8-4DB5-4C9C-8B8A-C6439B244BD6}", UserName));
            X.GetCmp<Hidden>("ShiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{5F4E81F2-E380-4279-879A-B9622414FCDC}", UserName));
            X.GetCmp<Hidden>("ShiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{469516DF-2860-4E02-85E6-0D0E80383F0B}", UserName));

            return View();
        }

        public ActionResult LoadNavireActive()
        {
            List<DataPersist> liste = new Navire().fnSelect(0);

            return this.Store(liste);
        }

        public ActionResult OnAdd()
        {            
            NavireViewModel NavireVm = new NavireViewModel();

            NavireVm._Navire = new Navire();
            NavireVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            ViewData["StatutForm"] = "0";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormNavire", Model = NavireVm, ViewData = ViewData };
        }

        public ActionResult OnAddVeselShipment()
        {            
            NavireViewModel NavireVm = new NavireViewModel();

            NavireVm._Navire = new Navire();
            NavireVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            ViewData["StatutForm"] = "1";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormNavire", Model = NavireVm, ViewData = ViewData };
        }

        public ActionResult OnEdit(string ItemSelected)
        {           
            NavireViewModel NavireVm = new NavireViewModel();

            NavireVm._Navire = JSON.Deserialize<Navire>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            NavireVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            ViewData["StatutForm"] = "0";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormNavire", Model = NavireVm, ViewData = ViewData };

        }

        public ActionResult OnConsult(string ItemSelected)
        {            
            NavireViewModel NavireVm = new NavireViewModel();

            NavireVm._Navire = JSON.Deserialize<Navire>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            NavireVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
            ViewData["StatutForm"] = "0";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormNavire", Model = NavireVm, ViewData = ViewData };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Navire navire = new Navire();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenNavExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    navire.IsNew = true;
                else
                {
                    navire.IsNew = false;

                    navire.fnGet(int.Parse(GetFormValue("TxtNavireID")));

                    if (navire == null || navire.ID == 0)
                        throw new Exception("SubmitFormMethod : Ship load failed.");
                }

                navire = MapFormToObject(navire);

                bool result = navire.fnUpdate();

                if (result)
                {
                    
                    string StautForm = X.GetCmp<Hidden>("hiddenSatutForm").Value.ToString();
                   
                    if (StautForm == "0")
                    {
                        Store mstore = X.GetCmp<Store>("storeListeNavire");
                        if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                        {
                            mstore.Insert(0, navire);
                            X.GetCmp<RowSelectionModel>("rowSelectionNavire").Select(0);
                        }
                        else
                        {
                            ModelProxy mProxy = mstore.GetById(navire.ID);

                            mProxy.BeginEdit();
                            
                            mProxy.Set(navire);

                            mProxy.Commit();

                            mProxy.EndEdit();
                        }
                        X.GetCmp<Window>("FormNavire").Close();
                        Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                        mViewport.Unmask();

                    }
                    else {
                        X.GetCmp<Window>("FormNavire").Close();
                        X.GetCmp<ComboBox>("_cmbShip").InsertItem(0,navire.Nom,navire.ID);
                        X.GetCmp<ComboBox>("_cmbShip").Value =navire.ID;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Ship : Data Validation",
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
            var liste = new Navire().fnSelect(status);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Navire navire = JSON.Deserialize<Navire>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = navire.fnGet(navire.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Ship loading failed.");

                navire.UtilisateurModification = (string)Session["userName"];

                if (navire.Desactive)
                    result = navire.fnActivate();
                else
                    result = navire.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Ship, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeNavire");

                    ModelProxy mProxy = mstore.GetById(navire.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(navire);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Ship : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("NavireCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeNavire");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("NavireCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Navire MapFormToObject(Navire navire)
        {
            navire.Nom = X.GetCmp<TextField>("TxtDesignationNavire").Text;

            navire.UtilisateurCreation = (string)Session["userName"];
            navire.UtilisateurModification = (string)Session["userName"];

            return navire;
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
            X.GetCmp<RowSelectionModel>("rowSelectionNavire").DeselectAll();
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