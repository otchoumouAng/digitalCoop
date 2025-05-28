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
    public class SacTypeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Site
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{F9848A91-CA76-490F-BDFA-E04C41D8C783}", UserName) == false)
                X.GetCmp<Button>("btnNewSacType").Disable();
            else
                X.GetCmp<Button>("btnNewSacType").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{685ACDE7-AFC9-4294-BA11-B276737B781D}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportTypeOfBags").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportTypeOfBags").Enable();

            X.GetCmp<Hidden>("TbghiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{F9848A91-CA76-490F-BDFA-E04C41D8C783}", UserName));
            X.GetCmp<Hidden>("TbghiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{651D1070-A937-46FB-8A05-243D9AB25383}", UserName));
            X.GetCmp<Hidden>("TbghiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{CECCF8A8-0941-4928-8B01-E727D3D43E4C}", UserName));
            X.GetCmp<Hidden>("TbghiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{5C376455-6E89-48E8-AE8C-7B4B68C6C82A}", UserName));
            X.GetCmp<Hidden>("TbghiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{685ACDE7-AFC9-4294-BA11-B276737B781D}", UserName));
            X.GetCmp<Hidden>("TbghiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{AC935EEF-D1D9-4936-B36C-9BC1C3BBA448}", UserName));

            return View();
        }

        public ActionResult LoadSacType()
        {
            List<DataPersist> mList = new SacType().fnSelect();

            SacType mclass = new SacType();

            if (mList.Count > 0)
                mclass = mList[0] as SacType;

            return this.Store(mList);
        }

        public ActionResult LoadSacTypeActive()
        {
            List<DataPersist> mList = new SacType().fnSelect(0);

            SacType mclass = new SacType();

            if (mList.Count > 0)
                mclass = mList[0] as SacType;

            return this.Store(mList);
        }


        public ActionResult LoadSacTypeAll()
        {
            List<DataPersist> mList = new SacType().fnSelect();

            SacType mclass = new SacType();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as SacType;

            return this.Store(mList);
        }

        public ActionResult LoadSacTypeActiveAll()
        {
            List<DataPersist> mList = new SacType().fnSelect(0);

            SacType mclass = new SacType();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as SacType;

            return this.Store(mList);
        }

        public ActionResult LoadSacTypeWithBlank()
        {
            List<DataPersist> mList = new SacType().fnSelect(0);

            SacType mclass = new SacType();

            mclass.ID = -1;
            mclass.Designation = "";

            mList.Insert(0, mclass);
            mclass = mList[0] as SacType;

            return this.Store(mList);
        }


        public ActionResult LoadSacTypeActivePeseeSite()
        {
            List<DataPersist> mList = new SacType().fnSelect(0,1);

            SacType mclass = new SacType();

            if (mList.Count > 0)
                mclass = mList[0] as SacType;

            return this.Store(mList);
        }
        public ActionResult OnAdd()
        {
            

            SacTypeViewModel SacTypeVm = new SacTypeViewModel();

            SacTypeVm._SacType = new SacType();
            SacTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormSacType", Model = SacTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            

            SacTypeViewModel SacTypeVm = new SacTypeViewModel();

            SacTypeVm._SacType = JSON.Deserialize<SacType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            SacTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormSacType", Model = SacTypeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            

            SacTypeViewModel SacTypeVm = new SacTypeViewModel();

            SacTypeVm._SacType = JSON.Deserialize<SacType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            SacTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormSacType", Model = SacTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                SacType sactype = new SacType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    sactype.IsNew = true;
                else
                {
                    sactype.IsNew = false;

                    sactype.fnGet(int.Parse(GetFormValue("TxtSacTypeID")));

                    if (sactype == null || sactype.ID == 0)
                        throw new Exception("SubmitFormMethod : Type de Sacs, load failed.");
                }

                sactype = MapFormToObject(sactype);

                bool result = sactype.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeSacType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, sactype);
                        X.GetCmp<RowSelectionModel>("rowSelectionSacType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(sactype.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(sactype);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormSacType").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type de Sacs : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult Select(string ItemStatus)
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
            var liste = new SacType().fnSelect(status);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                SacType sactype = JSON.Deserialize<SacType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = sactype.fnGet(sactype.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type de Sacs, loading failed.");
                //(string)Session["userName"];
                sactype.UtilisateurModification = (string)Session["userName"];

                if (sactype.Desactive)
                    result = sactype.fnActivate();
                else
                    result = sactype.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type de Sacs, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeSacType");

                    ModelProxy mProxy = mstore.GetById(sactype.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(sactype);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type de Sacs : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("SacTypeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeSacType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("SacTypeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private SacType MapFormToObject(SacType sactype)
        {
            sactype.Designation = X.GetCmp<TextField>("TxtDesignationSacType").Text;
            sactype.Tare = decimal.Parse(X.GetCmp<NumberField>("TxtTareSacType").RawValue.ToString());
            sactype.PoidsMin = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtPoidsMinSacType").RawText) ? decimal.Parse(X.GetCmp<NumberField>("TxtPoidsMinSacType").RawValue.ToString()) : (decimal?)null;
            sactype.PoidsMax = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtPoidsMaxSacType").RawText) ? decimal.Parse(X.GetCmp<NumberField>("TxtPoidsMaxSacType").RawValue.ToString()) : (decimal?)null;
            sactype.GenMouvement = bool.Parse(X.GetCmp<Checkbox>("ChkGenereMvtSacType").Value.ToString());
            sactype.EstVisiblePourPeseeSite = bool.Parse(X.GetCmp<Checkbox>("TxtVisibilityOnWeighing").Value.ToString());
            //(string)Session["userName"]
            sactype.UtilisateurCreation = (string)Session["userName"];
            sactype.UtilisateurModification = (string)Session["userName"];

            return sactype;
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
            X.GetCmp<RowSelectionModel>("rowSelectionSacType").DeselectAll();
        }


        #endregion

    }
}