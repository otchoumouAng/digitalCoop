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
    public class ConditionnementProduitController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: ConditionnementProduit
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{DD2ADDF1-5C34-4E81-A6BA-C55A630AAAF4}", UserName) == false)
                X.GetCmp<Button>("btnNewConditionnementProduit").Disable();
            else
                X.GetCmp<Button>("btnNewConditionnementProduit").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{05015259-CFDD-4CDB-9CF4-380063D3DDD1}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListConditionnementProduit").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListConditionnementProduit").Enable();

            X.GetCmp<Hidden>("CohiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{DD2ADDF1-5C34-4E81-A6BA-C55A630AAAF4}", UserName));
            X.GetCmp<Hidden>("CohiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{DD2ADDF1-5C34-4E81-A6BA-C55A630AAAF4}", UserName));
            X.GetCmp<Hidden>("CohiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{146AB76D-7CAB-4202-937E-3EC3614F4B09}", UserName));
            X.GetCmp<Hidden>("CohiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{515375D3-8FB0-4657-B5F1-3B243EFEAEFA}", UserName));
            X.GetCmp<Hidden>("CohiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{05015259-CFDD-4CDB-9CF4-380063D3DDD1}", UserName));
            X.GetCmp<Hidden>("CohiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{FB43A50A-FE37-4E5F-BC87-A80967393933}", UserName));

            return View();
        }

        public ActionResult Load()
        {
            List<DataPersist> mList = new ConditionnementProduit().fnSelect();
            return this.Store(mList);
        }

        public ActionResult LoadActive()
        {
            List<DataPersist> mList = new ConditionnementProduit().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new ConditionnementProduit().fnSelect();
            ConditionnementProduit mclass = new ConditionnementProduit();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";
            mList.Insert(0, mclass);

            return this.Store(mList);

        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ConditionnementProduitViewModel ConditionnementProduitVm = new ConditionnementProduitViewModel();

            ConditionnementProduitVm._ConditionnementProduit = new ConditionnementProduit();
            ConditionnementProduitVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConditionnementProduit", Model = ConditionnementProduitVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ConditionnementProduitViewModel ConditionnementProduitVm = new ConditionnementProduitViewModel();

            ConditionnementProduitVm._ConditionnementProduit = JSON.Deserialize<ConditionnementProduit>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ConditionnementProduitVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConditionnementProduit", Model = ConditionnementProduitVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ConditionnementProduitViewModel ConditionnementProduitVm = new ConditionnementProduitViewModel();

            ConditionnementProduitVm._ConditionnementProduit = JSON.Deserialize<ConditionnementProduit>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ConditionnementProduitVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConditionnementProduit", Model = ConditionnementProduitVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                ConditionnementProduit ConditionnementProduit = new ConditionnementProduit();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    ConditionnementProduit.IsNew = true;
                else
                {
                    ConditionnementProduit.IsNew = false;

                    ConditionnementProduit.fnGet(int.Parse(GetFormValue("TxtConditionnementProduitID")));

                    if (ConditionnementProduit == null || ConditionnementProduit.ID == 0)
                        throw new Exception("SubmitFormMethod : Conditionnement Produit load failed.");
                }

                ConditionnementProduit = MapFormToObject(ConditionnementProduit);

                bool result = ConditionnementProduit.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeConditionnementProduit");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, ConditionnementProduit);
                        X.GetCmp<RowSelectionModel>("rowSelectionConditionnementProduit").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(ConditionnementProduit.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(ConditionnementProduit);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormConditionnementProduit").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Conditionnement Produit : Data Validation",
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
            var liste = new ConditionnementProduit().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                ConditionnementProduit ConditionnementProduit = JSON.Deserialize<ConditionnementProduit>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = ConditionnementProduit.fnGet(ConditionnementProduit.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Conditionnement Produit loading failed.");
                //(string)Session["userName"];
                ConditionnementProduit.UtilisateurModification = (string)Session["userName"];

                if (ConditionnementProduit.Desactive)
                    result = ConditionnementProduit.fnActivate();
                else
                    result = ConditionnementProduit.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Conditionnement Produit, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeConditionnement");

                    ModelProxy mProxy = mstore.GetById(ConditionnementProduit.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(ConditionnementProduit);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Conditionnement Produit : OnActivateDeactivate",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ConditionnementProduitCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeConditionnementProduit");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ConditionnementProduitCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private ConditionnementProduit MapFormToObject(ConditionnementProduit ConditionnementProduit)
        {
            ConditionnementProduit.Designation = X.GetCmp<TextField>("TxtDesignationConditionnementProduit").Text;
            //(string)Session["userName"]
            ConditionnementProduit.UtilisateurCreation = (string)Session["userName"];
            ConditionnementProduit.UtilisateurModification = (string)Session["userName"];

            return ConditionnementProduit;
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
            X.GetCmp<RowSelectionModel>("rowSelectionConditionnement").DeselectAll();
        }


        #endregion
    }
}