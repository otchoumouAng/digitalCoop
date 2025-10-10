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
    public class ContratPeriodeTypeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: ContratPeriodeType
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{8571C26E-EB4A-4300-B71E-911495D476B3}", UserName) == false)
                X.GetCmp<Button>("btnNewContratPeriodeType").Disable();
            else
                X.GetCmp<Button>("btnNewContratPeriodeType").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{7854401A-D768-4CBE-98E8-C89A93D765D7}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListFwdContractType").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListFwdContractType").Enable();

            X.GetCmp<Hidden>("CpthiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{8571C26E-EB4A-4300-B71E-911495D476B3}", UserName));
            X.GetCmp<Hidden>("CpthiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{1D958AD4-D6F5-4F74-9C6C-3D1E47E872BD}", UserName));
            X.GetCmp<Hidden>("CpthiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{B994489A-CAAE-40EA-BBA7-70EC6F7F328E}", UserName));
            X.GetCmp<Hidden>("CpthiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{8086F135-083D-45C2-A323-D74A00C4261B}", UserName));
            X.GetCmp<Hidden>("CpthiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{7854401A-D768-4CBE-98E8-C89A93D765D7}", UserName));
            X.GetCmp<Hidden>("CpthiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{04E4A3B6-ECBF-4854-A9F8-8E3CB1631886}", UserName));

            return View();
        }

        public ActionResult LoadTypeContratPeriodeAll()
        {
            List<DataPersist> mList = new ContratPeriodeType().fnSelect(0);

            ContratPeriodeType mclass = new ContratPeriodeType();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as ContratPeriodeType;

            return this.Store(mList);
        }

        public ActionResult LoadTypeContratPeriode()
        {
            List<DataPersist> mList = new ContratPeriodeType().fnSelect(0);

            ContratPeriodeType mclass = new ContratPeriodeType();

            //mclass.ID = -1;
            //mclass.Designation = "{Tous}";

            //mList.Insert(0, mclass);
            //mclass = mList[0] as ContratPeriodeType;

            return this.Store(mList);
        }

        public ActionResult LoadTypeContratPeriodeWithoutFinancingType()
        {
            int FinancementTypeId = (new Parametres(0).ContratPeriodeTypeFinancement);
            List<DataPersist> mList = new ContratPeriodeType().fnSelect(0,1);
            ContratPeriodeType mclass = new ContratPeriodeType();
            
            //mclass = mList[0] as ContratPeriodeType;
            //mclass.ID = FinancementTypeId;
            //mList.Remove(mclass);
            //mclass.ID = -1;
            //mclass.Designation = "{Tous}";

            //mList.Insert(0, mclass);
            //mclass = mList[0] as ContratPeriodeType;

            return this.Store(mList);
        }


        public ActionResult OnAdd()
        {
            

            ContratPeriodeTypeViewModel ContratPeriodeTypeVm = new ContratPeriodeTypeViewModel();

            ContratPeriodeTypeVm._ContratPeriodeType = new ContratPeriodeType();
            ContratPeriodeTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormContratPeriodeType", Model = ContratPeriodeTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            

            ContratPeriodeTypeViewModel ContratPeriodeTypeVm = new ContratPeriodeTypeViewModel();

            ContratPeriodeTypeVm._ContratPeriodeType = JSON.Deserialize<ContratPeriodeType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ContratPeriodeTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormContratPeriodeType", Model = ContratPeriodeTypeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            

            ContratPeriodeTypeViewModel ContratPeriodeTypeVm = new ContratPeriodeTypeViewModel();

            ContratPeriodeTypeVm._ContratPeriodeType = JSON.Deserialize<ContratPeriodeType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ContratPeriodeTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormContratPeriodeType", Model = ContratPeriodeTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                ContratPeriodeType ContratPeriodeType = new ContratPeriodeType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    ContratPeriodeType.IsNew = true;
                else
                {
                    ContratPeriodeType.IsNew = false;

                    ContratPeriodeType.fnGet(int.Parse(GetFormValue("TxtContratPeriodeTypeID")));

                    if (ContratPeriodeType == null || ContratPeriodeType.ID == 0)
                        throw new Exception("SubmitFormMethod : Contrat Periode Type, load failed.");
                }

                ContratPeriodeType = MapFormToObject(ContratPeriodeType);

                bool result = ContratPeriodeType.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeContratPeriodeType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, ContratPeriodeType);
                        X.GetCmp<RowSelectionModel>("rowSelectionContratPeriodeType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(ContratPeriodeType.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(ContratPeriodeType);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormContratPeriodeType").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Contrat Periode Type : Data Validation",
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
            var liste = new ContratPeriodeType().fnSelect(status);
            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                ContratPeriodeType ContratPeriodeType = JSON.Deserialize<ContratPeriodeType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = ContratPeriodeType.fnGet(ContratPeriodeType.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Contrat Periode Type, loading failed.");
                //(string)Session["userName"];
                ContratPeriodeType.UtilisateurModification = (string)Session["userName"];

                if (ContratPeriodeType.Desactive)
                    result = ContratPeriodeType.fnActivate();
                else
                    result = ContratPeriodeType.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Contrat Periode Type, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeContratPeriodeType");

                    ModelProxy mProxy = mstore.GetById(ContratPeriodeType.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(ContratPeriodeType);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Contrat Periode Type : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ContratPeriodeTypeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeContratPeriodeType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ContratPeriodeTypeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private ContratPeriodeType MapFormToObject(ContratPeriodeType ContratPeriodeType)
        {
            ContratPeriodeType.Designation = X.GetCmp<TextField>("TxtDesignationContratPeriodeType").Text;
            ContratPeriodeType.VisibleInContratPeriode = bool.Parse(X.GetCmp<Checkbox>("TxtVisibilityContratPeriodeType").Value.ToString());
            //(string)Session["userName"]
            ContratPeriodeType.UtilisateurCreation = (string)Session["userName"];
            ContratPeriodeType.UtilisateurModification = (string)Session["userName"];

            return ContratPeriodeType;
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
            X.GetCmp<RowSelectionModel>("rowSelectionContratPeriodeType").DeselectAll();
        }


        #endregion

    }
}