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
    public class MiseEnCompteTypeController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: MiseEnCompteType
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{7A19F0CD-01E2-4DDF-8702-4DCC1026DEEA}", UserName) == false)
                X.GetCmp<Button>("btnNewMiseEnCompteType").Disable();
            else
                X.GetCmp<Button>("btnNewMiseEnCompteType").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{C54E2790-D930-4452-BE91-18BDA89B4FF3}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListSavingType").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListSavingType").Enable();

            X.GetCmp<Hidden>("McthiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{7A19F0CD-01E2-4DDF-8702-4DCC1026DEEA}", UserName));
            X.GetCmp<Hidden>("McthiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{042B4CF3-E7D4-4DDC-83A0-1CCEB8EB749E}", UserName));
            X.GetCmp<Hidden>("McthiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{A1F39A44-592F-4BC6-A894-B0CD8CF56C21}", UserName));
            X.GetCmp<Hidden>("McthiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{8AA3BADB-7D66-4177-B015-F6000B00643D}", UserName));
            X.GetCmp<Hidden>("McthiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{C54E2790-D930-4452-BE91-18BDA89B4FF3}", UserName));
            X.GetCmp<Hidden>("McthiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{D6144548-6FD3-4FFA-B8EF-DF5675978D1A}", UserName));

            return View();
        }

        public ActionResult LoadMiseEnCompteType()
        {
            List<DataPersist> liste = new MiseEnCompteType().fnSelect();
            
            return this.Store(liste);
        }

        public ActionResult LoadMiseEnCompteTypeAll()
        {
            List<DataPersist> liste = new MiseEnCompteType().fnSelect();

            MiseEnCompteType labo = new MiseEnCompteType();
            labo.ID = -1;
            labo.Designation = "{Tous}";

            liste.Insert(0, labo);
            labo = liste[0] as MiseEnCompteType;

            return this.Store(liste);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            MiseEnCompteTypeViewModel MiseEnCompteTypeVm = new MiseEnCompteTypeViewModel();

            MiseEnCompteTypeVm._MiseEnCompteType = new MiseEnCompteType();
            MiseEnCompteTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMiseEnCompteType", Model = MiseEnCompteTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            MiseEnCompteTypeViewModel MiseEnCompteTypeVm = new MiseEnCompteTypeViewModel();

            MiseEnCompteTypeVm._MiseEnCompteType = JSON.Deserialize<MiseEnCompteType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            MiseEnCompteTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMiseEnCompteType", Model = MiseEnCompteTypeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            MiseEnCompteTypeViewModel MiseEnCompteTypeVm = new MiseEnCompteTypeViewModel();

            MiseEnCompteTypeVm._MiseEnCompteType = JSON.Deserialize<MiseEnCompteType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            MiseEnCompteTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMiseEnCompteType", Model = MiseEnCompteTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                MiseEnCompteType miseencomptetype = new MiseEnCompteType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    miseencomptetype.IsNew = true;
                else
                {
                    miseencomptetype.IsNew = false;

                    miseencomptetype.fnGet(int.Parse(GetFormValue("TxtMiseEnCompteTypeID")));

                    if (miseencomptetype == null || miseencomptetype.ID == 0)
                        throw new Exception("SubmitFormMethod : Accounting type load failed.");
                }

                miseencomptetype = MapFormToObject(miseencomptetype);

                bool result = miseencomptetype.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeMiseEnCompteType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, miseencomptetype);
                        X.GetCmp<RowSelectionModel>("rowSelectionMiseEnCompteType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(miseencomptetype.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(miseencomptetype);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormMiseEnCompteType").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Accounting type : Data Validation",
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
            var liste = new MiseEnCompteType().fnSelect(status);
            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                MiseEnCompteType miseencomptetype = JSON.Deserialize<MiseEnCompteType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = miseencomptetype.fnGet(miseencomptetype.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Accounting type loading failed.");
                //(string)Session["userName"];
                miseencomptetype.UtilisateurModification = (string)Session["userName"];

                if (miseencomptetype.Desactive)
                    result = miseencomptetype.fnActivate();
                else
                    result = miseencomptetype.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Accounting type, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeMiseEnCompteType");

                    ModelProxy mProxy = mstore.GetById(miseencomptetype.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(miseencomptetype);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Accounting type : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("MiseEnCompteTypeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeMiseEnCompteType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("MiseEnCompteTypeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private MiseEnCompteType MapFormToObject(MiseEnCompteType miseenComptetype)
        {
            miseenComptetype.Designation = X.GetCmp<TextField>("TxtDesignationMiseEnCompteType").Text;
            //(string)Session["userName"]
            miseenComptetype.UtilisateurCreation = (string)Session["userName"];
            miseenComptetype.UtilisateurModification = (string)Session["userName"];

            return miseenComptetype;
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
            X.GetCmp<RowSelectionModel>("rowSelectionMiseEnCompteType").DeselectAll();
        }


        #endregion

    }
}