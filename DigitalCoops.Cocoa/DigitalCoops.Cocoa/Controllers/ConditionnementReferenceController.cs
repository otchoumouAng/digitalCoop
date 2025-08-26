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
    public class ConditionnementReferenceController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: ConditionnementReference
        public ActionResult Index()
        {
        
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{DD2ADDF1-5C34-4E81-A6BA-C55A630AAAF4}", UserName) == false)
                X.GetCmp<Button>("btnNewConditionnementReference").Disable();
            else
                X.GetCmp<Button>("btnNewConditionnementReference").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{05015259-CFDD-4CDB-9CF4-380063D3DDD1}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListConditionnementReference").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListConditionnementReference").Enable();

            X.GetCmp<Hidden>("CoRhiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{DD2ADDF1-5C34-4E81-A6BA-C55A630AAAF4}", UserName));
            X.GetCmp<Hidden>("CoRhiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{5E817109-CC57-4653-BAB5-831792397000}", UserName));
            X.GetCmp<Hidden>("CoRhiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{146AB76D-7CAB-4202-937E-3EC3614F4B09}", UserName));
            X.GetCmp<Hidden>("CoRhiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{515375D3-8FB0-4657-B5F1-3B243EFEAEFA}", UserName));
            X.GetCmp<Hidden>("CoRhiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{05015259-CFDD-4CDB-9CF4-380063D3DDD1}", UserName));
            X.GetCmp<Hidden>("CoRhiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{FB43A50A-FE37-4E5F-BC87-A80967393933}", UserName));

            return View();
        }

        public ActionResult Load()
        {
            List<DataPersist> mList = new ConditionnementReference().fnSelect();
            return this.Store(mList);
        }

        public ActionResult LoadActive()
        {
            List<DataPersist> mList = new ConditionnementReference().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new ConditionnementReference().fnSelect();
            ConditionnementReference mclass = new ConditionnementReference();

            mclass.ID = -1;
            mclass.Reference = "{Tous}";
            mList.Insert(0, mclass);

            return this.Store(mList);

        }

        public ActionResult LoadConditionnementReferenceByConditionnementProduit(string ItemConditionnementProduitID)
        {
            List<DataPersist> mList = null;

            if (!string.IsNullOrEmpty(ItemConditionnementProduitID))
            {
                int itemConditionnementProduitID = int.Parse(ItemConditionnementProduitID);
               mList = new ConditionnementReference().fnSelectByConditionnementProduit(itemConditionnementProduitID);



                return this.Store(mList);
            }

            return this.Store(mList);

        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ConditionnementReferenceViewModel ConditionnementReferenceVm = new ConditionnementReferenceViewModel();

            ConditionnementReferenceVm._ConditionnementReference = new ConditionnementReference();
            ConditionnementReferenceVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConditionnementReference", Model = ConditionnementReferenceVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ConditionnementReferenceViewModel ConditionnementReferenceVm = new ConditionnementReferenceViewModel();

            ConditionnementReferenceVm._ConditionnementReference = JSON.Deserialize<ConditionnementReference>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ConditionnementReferenceVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConditionnementReference", Model = ConditionnementReferenceVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ConditionnementReferenceViewModel ConditionnementReferenceVm = new ConditionnementReferenceViewModel();

            ConditionnementReferenceVm._ConditionnementReference = JSON.Deserialize<ConditionnementReference>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ConditionnementReferenceVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConditionnementReference", Model = ConditionnementReferenceVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                ConditionnementReference reference = new ConditionnementReference();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    reference.IsNew = true;
                else
                {
                    reference.IsNew = false;

                    reference.fnGet(int.Parse(GetFormValue("TxtConditionnementReferenceID")));

                    if (reference == null || reference.ID == 0)
                        throw new Exception("SubmitFormMethod : Conditionnement Reference load failed.");
                }

                reference = MapFormToObject(reference);

                bool result = reference.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeConditionnementReference");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, reference);
                        X.GetCmp<RowSelectionModel>("rowSelectionConditionnementReference").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(reference.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(reference);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormConditionnementReference").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Conditionnement Reference : Data Validation",
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
            var liste = new ConditionnementReference().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                ConditionnementReference reference = JSON.Deserialize<ConditionnementReference>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = reference.fnGet(reference.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Conditionnement Reference loading failed.");
                //(string)Session["userName"];
                reference.UtilisateurModification = (string)Session["userName"];

                if (reference.Desactive)
                    result = reference.fnActivate();
                else
                    result = reference.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Conditionnement Reference, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeConditionnementReference");

                    ModelProxy mProxy = mstore.GetById(reference.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(reference);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Conditionnement Reference : OnActivateDeactivate",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ConditionnementReferenceCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeConditionnementReference");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ConditionnementReferenceCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private ConditionnementReference MapFormToObject(ConditionnementReference reference)
        {
            reference.Reference = X.GetCmp<TextField>("TxtReferenceConditionnementReference").Text;

            ConditionnementProduit conditionnementProduit = new ConditionnementProduit();
            conditionnementProduit.ID = int.Parse(X.GetCmp<ComboBox>("CmbConditionnementProduit").SelectedItem.Value.ToString());
            conditionnementProduit.Designation = X.GetCmp<ComboBox>("CmbConditionnementProduit").SelectedItem.Text;
            reference.ConditionnementProduit = conditionnementProduit;

            reference.Description = X.GetCmp<TextField>("TxtDescriptionConditionnementReference").Text;
            //(string)Session["userName"]
            reference.UtilisateurCreation = (string)Session["userName"];
            reference.UtilisateurModification = (string)Session["userName"];

            return reference;
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
            X.GetCmp<RowSelectionModel>("rowSelectionConditionnementReference").DeselectAll();
        }


        #endregion
    }
}