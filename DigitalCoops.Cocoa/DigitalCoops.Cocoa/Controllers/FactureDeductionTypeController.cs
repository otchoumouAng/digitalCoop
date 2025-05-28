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
    public class FactureDeductionTypeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: FactureDeductionType
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{9EEA9DB0-850F-4F9A-BF12-8DCDB2F0CC1A}", UserName) == false)
                X.GetCmp<Button>("btnNewFactureDeductionType").Disable();
            else
                X.GetCmp<Button>("btnNewFactureDeductionType").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{636F9775-81F3-4836-81C0-8676B44BEE1B}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportFactDedType").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportFactDedType").Enable();

            X.GetCmp<Hidden>("FdthiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{9EEA9DB0-850F-4F9A-BF12-8DCDB2F0CC1A}", UserName));
            X.GetCmp<Hidden>("FdthiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{B9681A3F-ACDD-4B55-9871-33778FF6424D}", UserName));
            X.GetCmp<Hidden>("FdthiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{5855E284-45CA-42E1-8B61-396E3678705A}", UserName));
            X.GetCmp<Hidden>("FdthiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{D184F5CD-82FC-421B-82CC-BE4E3B49AA46}", UserName));
            X.GetCmp<Hidden>("FdthiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{636F9775-81F3-4836-81C0-8676B44BEE1B}", UserName));
            X.GetCmp<Hidden>("FdthiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{CE938B90-ADE2-4E7D-8AFA-0A567CAA3899}", UserName));

            return View();
        }

        public ActionResult LoadFactureDeductionType()
        {
            List<DataPersist> myListe = new FactureDeductionType().fnSelect();
            FactureDeductionType mclass = new FactureDeductionType();

            return this.Store(myListe);

        }

        public ActionResult LoadFactureDeductionTypeAll()
        {
            List<DataPersist> myListe = new FactureDeductionType().fnSelect();
            FactureDeductionType mclass = new FactureDeductionType();

            mclass.Designation = "{Tous}";

            myListe.Insert(0, mclass);
            mclass = myListe[0] as FactureDeductionType;

            return this.Store(myListe);

        }

        public ActionResult LoadFactureDeductionTypeForInvoice()
        {
            List<DataPersist> myListe = new FactureDeductionType().fnSelectForInvoice();            
            return this.Store(myListe);

        }


        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            FactureDeductionTypeViewModel FactureDeductionTypeVm = new FactureDeductionTypeViewModel();

            FactureDeductionTypeVm._FactureDeductionType = new FactureDeductionType();
            FactureDeductionTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFactureDeductionType", Model = FactureDeductionTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            FactureDeductionTypeViewModel FactureDeductionTypeVm = new FactureDeductionTypeViewModel();

            FactureDeductionTypeVm._FactureDeductionType = JSON.Deserialize<FactureDeductionType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            FactureDeductionTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFactureDeductionType", Model = FactureDeductionTypeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            FactureDeductionTypeViewModel FactureDeductionTypeVm = new FactureDeductionTypeViewModel();

            FactureDeductionTypeVm._FactureDeductionType = JSON.Deserialize<FactureDeductionType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            FactureDeductionTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFactureDeductionType", Model = FactureDeductionTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                FactureDeductionType facturedeductiontype = new FactureDeductionType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    facturedeductiontype.IsNew = true;
                else
                {
                    facturedeductiontype.IsNew = false;

                    facturedeductiontype.fnGet(int.Parse(GetFormValue("TxtFactureDeductionTypeID")));

                    if (facturedeductiontype == null || facturedeductiontype.ID == 0)
                        throw new Exception("SubmitFormMethod : Type De Deduction Facture load failed.");
                }

                facturedeductiontype = MapFormToObject(facturedeductiontype);

                bool result = facturedeductiontype.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeFactureDeductionType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, facturedeductiontype);
                        X.GetCmp<RowSelectionModel>("rowSelectionFactureDeductionType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(facturedeductiontype.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(facturedeductiontype);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormFactureDeductionType").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type De Deduction Facture : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemStatus, string ItemDeduction)
        {
            int status = -1;
            int deduction = -1;
            if (string.IsNullOrEmpty(ItemStatus))
            {
                status = -1;
            }
            else if (ItemStatus == "true")
            {
                status = 0;
            }

            if (string.IsNullOrEmpty(ItemDeduction))
            {
                deduction = -1;
            }
            else if (ItemDeduction == "true")
            {
                deduction = 1;
            }
            var liste = new FactureDeductionType().fnSelect(status, deduction);
            
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                FactureDeductionType facturedeductiontype = JSON.Deserialize<FactureDeductionType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = facturedeductiontype.fnGet(facturedeductiontype.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type De Deduction Facture loading failed.");
                //(string)Session["userName"];
                facturedeductiontype.UtilisateurModification = (string)Session["userName"];

                if (facturedeductiontype.Desactive)
                    result = facturedeductiontype.fnActivate();
                else
                    result = facturedeductiontype.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Type De Deduction Facture, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeFactureDeductionType");

                    ModelProxy mProxy = mstore.GetById(facturedeductiontype.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(facturedeductiontype);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Type De Deduction Facture : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("FactureDeductionTypeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus, string ItemDeduction)
        {
            Store mstore = X.GetCmp<Store>("storeListeFactureDeductionType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus),
                                new Ext.Net.Parameter("ItemDeduction", ItemDeduction)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("FactureDeductionTypeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private FactureDeductionType MapFormToObject(FactureDeductionType facturedeductiontype)
        {
            facturedeductiontype.Designation = X.GetCmp<TextField>("TxtDesignationFactureDeductionType").Text;
            facturedeductiontype.Auto = bool.Parse( X.GetCmp<Checkbox>("ChkIsAutoFactureDeductionType").Value.ToString());
            facturedeductiontype.Taux = string.IsNullOrEmpty(X.GetCmp<TextField>("TxtTauxDeduction").RawValue.ToString()) ? (decimal?)null : decimal.Parse(X.GetCmp<TextField>("TxtTauxDeduction").RawValue.ToString());
            //(string)Session["userName"]
            facturedeductiontype.UtilisateurCreation = (string)Session["userName"];
            facturedeductiontype.UtilisateurModification = (string)Session["userName"];

            return facturedeductiontype;
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
            X.GetCmp<RowSelectionModel>("rowSelectionFactureDeductionType").DeselectAll();
        }


        #endregion

    }
}