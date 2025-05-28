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
using Tms2017.MVC.Models;

namespace Tms2017.MVC.Controllers
{
    public class ProvenanceTypeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: ProvenanceType
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{6A905CF7-7CD3-4013-A27D-BEE335FBFD6A}", UserName) == false)
                X.GetCmp<Button>("btnNewProvenanceType").Disable();
            else
                X.GetCmp<Button>("btnNewProvenanceType").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{5FBCFE54-5D43-408F-BD59-78D96EC4EB21}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListOrgType").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListOrgType").Enable();

            X.GetCmp<Hidden>("OrgThiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{6A905CF7-7CD3-4013-A27D-BEE335FBFD6A}", UserName));
            X.GetCmp<Hidden>("OrgThiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{95DC86A3-88D9-42C7-9A68-03B97920BC78}", UserName));
            X.GetCmp<Hidden>("OrgThiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{3BDA8972-2B7D-456B-B877-6DDB61EA0F61}", UserName));
            X.GetCmp<Hidden>("OrgThiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{D4ED5563-DDC6-4D9B-A679-6EC3391667A8}", UserName));
            X.GetCmp<Hidden>("OrgThiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{5FBCFE54-5D43-408F-BD59-78D96EC4EB21}", UserName));
            X.GetCmp<Hidden>("OrgThiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{15C0CC42-8CA5-4047-AAEE-BF1C608E6EF4}", UserName));

            return View();
        }

        public ActionResult LoadProvenanceType()
        {
            List<DataPersist> mList = new ProvenanceType().fnSelect();

            ProvenanceType mclass = new ProvenanceType();

            if(mList.Count > 0)
               mclass = mList[0] as ProvenanceType;

            return this.Store(mList);
        }

        public ActionResult LoadProvenanceTypeAll()
        {
            List<DataPersist> mList = new ProvenanceType().fnSelect();

            ProvenanceType mclass = new ProvenanceType();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as ProvenanceType;

            return this.Store(mList);
        }

        public ActionResult LoadProvenanceTypeWithBlank()
        {
            List<DataPersist> mList = new ProvenanceType().fnSelect(0);

            ProvenanceType mclass = new ProvenanceType();

            mclass.ID = -1;
            mclass.Designation = "";

            mList.Insert(0, mclass);
            mclass = mList[0] as ProvenanceType;

            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ProvenanceTypeViewModel ProvenanceTypeVm = new ProvenanceTypeViewModel();

            ProvenanceTypeVm._ProvenanceType = new ProvenanceType();
            ProvenanceTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProvenanceType", Model = ProvenanceTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ProvenanceTypeViewModel ProvenanceTypeVm = new ProvenanceTypeViewModel();

            ProvenanceTypeVm._ProvenanceType = JSON.Deserialize<ProvenanceType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ProvenanceTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProvenanceType", Model = ProvenanceTypeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ProvenanceTypeViewModel ProvenanceTypeVm = new ProvenanceTypeViewModel();

            ProvenanceTypeVm._ProvenanceType = JSON.Deserialize<ProvenanceType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ProvenanceTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProvenanceType", Model = ProvenanceTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                ProvenanceType provenancetype = new ProvenanceType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    provenancetype.IsNew = true;
                else
                {
                    provenancetype.IsNew = false;

                    provenancetype.fnGet(int.Parse(GetFormValue("TxtProvenanceTypeID")));

                    if (provenancetype == null || provenancetype.ID == 0)
                        throw new Exception("SubmitFormMethod : Origin Type load failed.");
                }

                provenancetype = MapFormToObject(provenancetype);

                bool result = provenancetype.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeProvenanceType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, provenancetype);
                        X.GetCmp<RowSelectionModel>("rowSelectionProvenanceType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(provenancetype.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(provenancetype);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormProvenanceType").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Origin Type : Data Validation",
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
            var liste = new ProvenanceType().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                ProvenanceType provenancetype = JSON.Deserialize<ProvenanceType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = provenancetype.fnGet(provenancetype.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Origin type loading failed.");
                //(string)Session["userName"];
                provenancetype.UtilisateurModification = (string)Session["userName"];

                if (provenancetype.Desactive)
                    result = provenancetype.fnActivate();
                else
                    result = provenancetype.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Origin type, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeProvenanceType");

                    ModelProxy mProxy = mstore.GetById(provenancetype.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(provenancetype);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Origin Type : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ProvenanceTypeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeProvenanceType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ProvenanceTypeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private ProvenanceType MapFormToObject(ProvenanceType provenancetype)
        {
            provenancetype.Designation = X.GetCmp<TextField>("TxtDesignationProvenanceType").Text;
            //(string)Session["userName"]
            provenancetype.UtilisateurCreation = (string)Session["userName"];
            provenancetype.UtilisateurModification = (string)Session["userName"];

            return provenancetype;
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
            X.GetCmp<RowSelectionModel>("rowSelectionProvenanceType").DeselectAll();
        }


        #endregion


    }
}