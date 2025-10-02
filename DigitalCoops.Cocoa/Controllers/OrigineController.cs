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
    public class OrigineController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Origine
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{C41757BB-4BAA-4425-B59A-6B20CCD98E42}", UserName) == false)
                X.GetCmp<Button>("btnNewOrigine").Disable();
            else
                X.GetCmp<Button>("btnNewOrigine").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{32A97D97-2F16-4D6D-BB72-5B5661F55C66}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListOrigine").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListOrigine").Enable();

            X.GetCmp<Hidden>("OrhiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{C41757BB-4BAA-4425-B59A-6B20CCD98E42}", UserName));
            X.GetCmp<Hidden>("OrhiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{ECDD5AB2-0BD7-4A6F-98E8-AD41250C1A8A}", UserName));
            X.GetCmp<Hidden>("OrhiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{E3C5D415-0C1D-4366-86E9-5E340B5829DA}", UserName));
            X.GetCmp<Hidden>("OrhiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{E932D9C3-B15C-4B0C-8774-1D745C0A4299}", UserName));
            X.GetCmp<Hidden>("OrhiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{32A97D97-2F16-4D6D-BB72-5B5661F55C66}", UserName));
            X.GetCmp<Hidden>("OrhiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{24145782-F4A0-42ED-AEC1-BF46B5D7DC1B}", UserName));

            return View();
        }

        public ActionResult Load()
        {
            List<DataPersist> mList = new Origine().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new Origine().fnSelect(0);
            Origine mclass = new Origine();

            mclass.ID = -1;
            mclass.Nom = "{Tous}";
            mList.Insert(0, mclass);

            return this.Store(mList);

        }

        public ActionResult OnAdd()
        {            

            OrigineViewModel OrigineVm = new OrigineViewModel();

            OrigineVm._Origine = new Origine();
            OrigineVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormOrigine", Model = OrigineVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {            

            OrigineViewModel OrigineVm = new OrigineViewModel();

            OrigineVm._Origine = JSON.Deserialize<Origine>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            OrigineVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormOrigine", Model = OrigineVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {            

            OrigineViewModel OrigineVm = new OrigineViewModel();

            OrigineVm._Origine = JSON.Deserialize<Origine>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            OrigineVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormOrigine", Model = OrigineVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Origine origine = new Origine();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    origine.IsNew = true;
                else
                {
                    origine.IsNew = false;

                    origine.fnGet(int.Parse(GetFormValue("TxtOrigineID")));

                    if (origine == null || origine.ID == 0)
                        throw new Exception("SubmitFormMethod : Origin load failed.");
                }

                origine = MapFormToObject(origine);

                bool result = origine.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeOrigine");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, origine);
                        X.GetCmp<RowSelectionModel>("rowSelectionOrigine").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(origine.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(origine);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormOrigine").Close();                                        
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Origin : Data Validation",
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
            var liste = new Origine().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Origine origine = JSON.Deserialize<Origine>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = origine.fnGet(origine.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Origin loading failed.");
                //(string)Session["userName"];
                origine.UtilisateurModification = (string)Session["userName"];

                if (origine.Desactive)
                    result = origine.fnActivate();
                else
                    result = origine.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Origin, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeOrigine");

                    ModelProxy mProxy = mstore.GetById(origine.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(origine);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Origin : OnActivateDeactivate",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("OrigineCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeOrigine");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("OrigineCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Origine MapFormToObject(Origine origine)
        {
            origine.Nom = X.GetCmp<TextField>("TxtNomOrigine").Text;
            //(string)Session["userName"]
            origine.UtilisateurCreation = (string)Session["userName"];
            origine.UtilisateurModification = (string)Session["userName"];

            return origine;
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
            X.GetCmp<RowSelectionModel>("rowSelectionOrigine").DeselectAll();
        }


        #endregion
    }
}