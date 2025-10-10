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
    public class BanqueController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Banque
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{BC8A53C0-CB87-4D6C-9F75-6C81BB2255A7}", UserName) == false)
                X.GetCmp<Button>("btnNewBanque").Disable();
            else
                X.GetCmp<Button>("btnNewBanque").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{6CD86DD2-48D2-40DC-AA34-95D50515BD99}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListBank").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListBank").Enable();

            X.GetCmp<Hidden>("BqhiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{BC8A53C0-CB87-4D6C-9F75-6C81BB2255A7}", UserName));
            X.GetCmp<Hidden>("BqhiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{3763AF0A-9098-424C-9674-C88C4D093EE7}", UserName));
            X.GetCmp<Hidden>("BqhiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{3535F755-9DF5-4D7A-8669-9E417D700062}", UserName));
            X.GetCmp<Hidden>("BqhiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{98470F7A-9D26-4E32-B3D7-1A62BB18A127}", UserName));
            X.GetCmp<Hidden>("BqhiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{6CD86DD2-48D2-40DC-AA34-95D50515BD99}", UserName));
            X.GetCmp<Hidden>("BqhiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{730185F1-82D0-44B3-8C4E-3FBE446C828C}", UserName));

            return View();
        }


        public ActionResult LoadBanque()
        {
            List<DataPersist> listeLabo = new Banque().fnSelect();

            return this.Store(listeLabo);
        }


        public ActionResult LoadBanqueActive()
        {
            List<DataPersist> listeLabo = new Banque().fnSelect(0);

            return this.Store(listeLabo);
        }

        public ActionResult LoadBanqueAll()
        {
            List<DataPersist> liste = new Banque().fnSelect();

            Banque banque = new Banque();

            banque.ID = -1;
            banque.Nom = "{Tous}";

            liste.Insert(0, banque);
            banque = liste[0] as Banque;

            return this.Store(liste);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            BanqueViewModel BanqueVm = new BanqueViewModel();

            BanqueVm._Banque = new Banque();
            BanqueVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormBanque", Model = BanqueVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            BanqueViewModel BanqueVm = new BanqueViewModel();

            BanqueVm._Banque = JSON.Deserialize<Banque>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            BanqueVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormBanque", Model = BanqueVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            BanqueViewModel BanqueVm = new BanqueViewModel();

            BanqueVm._Banque = JSON.Deserialize<Banque>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            BanqueVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormBanque", Model = BanqueVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Banque banque = new Banque();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    banque.IsNew = true;
                else
                {
                    banque.IsNew = false;

                    banque.fnGet(int.Parse(GetFormValue("TxtBanqueID")));

                    if (banque == null || banque.ID == 0)
                        throw new Exception("SubmitFormMethod : Bank load failed.");
                }

                banque = MapFormToObject(banque);

                bool result = banque.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeBanque");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, banque);                        
                        X.GetCmp<RowSelectionModel>("rowSelectionBanque").Select(0);
                        
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(banque.ID);
                                                
                        mProxy.BeginEdit();

                        mProxy.Set(banque);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormBanque").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bank : Data Validation",
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
            
            var liste = new Banque().fnSelect(status);
            
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Banque banque = JSON.Deserialize<Banque>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = banque.fnGet(banque.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Bank loading failed.");
                //(string)Session["userName"];
                banque.UtilisateurModification = (string)Session["userName"];

                if (banque.Desactive)
                    result = banque.fnActivate();
                else
                    result = banque.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Bank, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeBanque");

                    ModelProxy mProxy = mstore.GetById(banque.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(banque);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bank : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("BanqueCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeBanque");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)                                
                            });
            FormPanel mform = X.GetCmp<FormPanel>("BanqueCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Banque MapFormToObject(Banque banque)
        {            
            banque.Nom = X.GetCmp<TextField>("TxtNomBanque").Text;
            banque.Designation = X.GetCmp<TextField>("TxtDesignationBanque").Text;
            //(string)Session["userName"]
            banque.UtilisateurCreation = (string)Session["userName"];
            banque.UtilisateurModification = (string)Session["userName"];

            return banque;
        }

        public ActionResult OnPrintBankList(string id_farmer)
        {
            //return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'farmerCard{0}', 'http://13.81.114.255/tms/Report/Index', this, 'Bank List')", id_farmer));
            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'farmerCard{0}', 'Report/Index', this, 'Bank List')", id_farmer));
        }

        public ActionResult OnPrintBankListWithParam(string id_farmer)
        {
            //return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'farmerCard{0}', 'http://13.81.114.255/tms/Report/ReportWithParam', this, 'Bank List With Param')", 102));
            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'farmerCard{0}', 'Report/ReportWithParam', this, 'Bank List With Param')", 102));
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
            X.GetCmp<RowSelectionModel>("rowSelectionBanque").DeselectAll();
        }


        #endregion

    }
}