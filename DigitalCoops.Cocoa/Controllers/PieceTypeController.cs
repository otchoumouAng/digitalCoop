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
    public class PieceTypeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: PieceType
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{11AA4213-21E5-4A14-8C95-CCB11ACA1CB0}", UserName) == false)
                X.GetCmp<Button>("btnNewPieceType").Disable();
            else
                X.GetCmp<Button>("btnNewPieceType").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{772A12E5-B565-464F-AAB7-5FB037FD1951}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportIdTypeList").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportIdTypeList").Enable();

            X.GetCmp<Hidden>("IdhiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{11AA4213-21E5-4A14-8C95-CCB11ACA1CB0}", UserName));
            X.GetCmp<Hidden>("IdhiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{70E618A5-68D2-42BA-BC7F-8096262A6515}", UserName));
            X.GetCmp<Hidden>("IdhiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{1924E79D-2A0D-43D6-9D87-289187C4FB5D}", UserName));
            X.GetCmp<Hidden>("IdhiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{42CD8B23-86C0-4ABF-A27D-7A241A969AFD}", UserName));
            X.GetCmp<Hidden>("IdhiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{772A12E5-B565-464F-AAB7-5FB037FD1951}", UserName));
            X.GetCmp<Hidden>("IdhiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{0C70C24B-E4EE-4F1F-92BB-55E98B906A07}", UserName));

            return View();
        }

        public ActionResult LoadPieceType()
        {
            List<DataPersist> myListe = new PieceType().fnSelect(0);
            PieceType mclass = new PieceType();

            //mclass.ID = -1;
            //mclass.Designation = "{Tous}";

            //myListe.Insert(0, mclass);
            mclass = myListe[0] as PieceType;

            return this.Store(myListe);

        }

        public ActionResult LoadPieceTypeAll()
        {
            List<DataPersist> myListe = new PieceType().fnSelect(0);
            PieceType mclass = new PieceType();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            myListe.Insert(0, mclass);
            mclass = myListe[0] as PieceType;

            return this.Store(myListe);

        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PieceTypeViewModel PieceTypeVm = new PieceTypeViewModel();

            PieceTypeVm._PieceType = new PieceType();
            PieceTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPieceType", Model = PieceTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PieceTypeViewModel PieceTypeVm = new PieceTypeViewModel();

            PieceTypeVm._PieceType = JSON.Deserialize<PieceType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PieceTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPieceType", Model = PieceTypeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PieceTypeViewModel PieceTypeVm = new PieceTypeViewModel();

            PieceTypeVm._PieceType = JSON.Deserialize<PieceType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PieceTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPieceType", Model = PieceTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                PieceType piecetype = new PieceType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    piecetype.IsNew = true;
                else
                {
                    piecetype.IsNew = false;

                    piecetype.fnGet(int.Parse(GetFormValue("TxtPieceTypeID")));

                    if (piecetype == null || piecetype.ID == 0)
                        throw new Exception("SubmitFormMethod : Id Type load failed.");
                }

                piecetype = MapFormToObject(piecetype);

                bool result = piecetype.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePieceType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, piecetype);
                        X.GetCmp<RowSelectionModel>("rowSelectionPieceType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(piecetype.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(piecetype);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormPieceType").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type de piece : Data Validation",
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
            var liste = new PieceType().fnSelect(status);
            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                PieceType piecetype = JSON.Deserialize<PieceType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = piecetype.fnGet(piecetype.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type de piece loading failed.");
                //(string)Session["userName"];
                piecetype.UtilisateurModification = (string)Session["userName"];

                if (piecetype.Desactive)
                    result = piecetype.fnActivate();
                else
                    result = piecetype.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type de piece, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePieceType");

                    ModelProxy mProxy = mstore.GetById(piecetype.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(piecetype);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type de piece : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("PieceTypeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListePieceType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("PieceTypeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private PieceType MapFormToObject(PieceType piecetype)
        {
            piecetype.Designation = X.GetCmp<TextField>("TxtDesignationPieceType").Text;
            //(string)Session["userName"]
            piecetype.UtilisateurCreation = (string)Session["userName"];
            piecetype.UtilisateurModification = (string)Session["userName"];

            return piecetype;
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
            X.GetCmp<RowSelectionModel>("rowSelectionPieceType").DeselectAll();
        }


        #endregion

    }
}