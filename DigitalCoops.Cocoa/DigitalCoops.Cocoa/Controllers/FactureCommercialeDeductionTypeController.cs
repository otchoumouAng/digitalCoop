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
    public class FactureCommercialeDeductionTypeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: FactureCommercialeDeductionType
        public ActionResult Index()
        {
            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{B4838D6B-E310-48F1-A307-A4538362F414}"), UserName);
            HashSet<Guid> mlisteFonctions = new HashSet<Guid>(mListe.Cast<Fonction>().Select(f => f.ID).ToList());
            if (mlisteFonctions.Contains(Guid.Parse("{F9E47319-37C5-40B9-B6C2-A390BDD75816}")))
                X.GetCmp<Button>("btnNewFactureCommercialeDeductionType").Enable();
            else
                X.GetCmp<Button>("btnNewFactureCommercialeDeductionType").Disable();            

            if (mlisteFonctions.Contains(Guid.Parse("{B723DBFB-5679-4160-8B66-AFB592641750}")))
                X.GetCmp<MenuItem>("mnuExportFactureCommercialeDeductionType").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportFactureCommercialeDeductionType").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{963A85AC-777D-4CD4-AE25-FCE0D2A01A2E}")))
                X.GetCmp<Hidden>("fchiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("fchiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{F33BEEA0-2BD7-4C35-88C7-9EEF66A091B0}")))
                X.GetCmp<Hidden>("fchiddenPermActiver").SetValue(true);
            else
                X.GetCmp<Hidden>("fchiddenPermActiver").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{BDD06086-0D51-4EDC-8B35-37739ED9EE09}")))
                X.GetCmp<Hidden>("fchiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("fchiddenPermModifier").SetValue(false);                                    
            #endregion

            return View();
        }

        public ActionResult LoadDeductionTypeActiveNotAuto()
        {
            List<DataPersist> mList = new FactureCommercialeDeductionType().fnSelect(0, 0);
            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {            

            FactureCommercialeDeductionTypeViewModel FactureCommercialeDeductionTypeVm = new FactureCommercialeDeductionTypeViewModel();

            FactureCommercialeDeductionTypeVm._FactureCommercialeDeductionType = new FactureCommercialeDeductionType();
            FactureCommercialeDeductionTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFactureCommercialeDeductionType", Model = FactureCommercialeDeductionTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {            
            FactureCommercialeDeductionTypeViewModel FactureCommercialeDeductionTypeVm = new FactureCommercialeDeductionTypeViewModel();

            FactureCommercialeDeductionTypeVm._FactureCommercialeDeductionType = JSON.Deserialize<FactureCommercialeDeductionType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            FactureCommercialeDeductionTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFactureCommercialeDeductionType", Model = FactureCommercialeDeductionTypeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {            
            FactureCommercialeDeductionTypeViewModel FactureCommercialeDeductionTypeVm = new FactureCommercialeDeductionTypeViewModel();

            FactureCommercialeDeductionTypeVm._FactureCommercialeDeductionType = JSON.Deserialize<FactureCommercialeDeductionType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            FactureCommercialeDeductionTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFactureCommercialeDeductionType", Model = FactureCommercialeDeductionTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                FactureCommercialeDeductionType facturedeductiontype = new FactureCommercialeDeductionType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    facturedeductiontype.IsNew = true;
                else
                {
                    facturedeductiontype.IsNew = false;

                    facturedeductiontype.fnGet(int.Parse(GetFormValue("TxtFactureCommercialeDeductionTypeID")));

                    if (facturedeductiontype == null || facturedeductiontype.ID == 0)
                        throw new Exception("SubmitFormMethod : Type De Deduction Facture load failed.");
                }

                facturedeductiontype = MapFormToObject(facturedeductiontype);

                bool result = facturedeductiontype.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeFactureCommercialeDeductionType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, facturedeductiontype);
                        X.GetCmp<RowSelectionModel>("rowSelectionFactureCommercialeDeductionType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(facturedeductiontype.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(facturedeductiontype);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormFactureCommercialeDeductionType").Close();
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
            var liste = new FactureCommercialeDeductionType().fnSelect(status, deduction);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                FactureCommercialeDeductionType facturedeductiontype = JSON.Deserialize<FactureCommercialeDeductionType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

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
                    Store mstore = X.GetCmp<Store>("storeListeFactureCommercialeDeductionType");

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
            FormPanel mform = X.GetCmp<FormPanel>("FactureCommercialeDeductionTypeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus, string ItemDeduction)
        {
            Store mstore = X.GetCmp<Store>("storeListeFactureCommercialeDeductionType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus),
                                new Ext.Net.Parameter("ItemDeduction", ItemDeduction)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("FactureCommercialeDeductionTypeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private FactureCommercialeDeductionType MapFormToObject(FactureCommercialeDeductionType facturedeductiontype)
        {
            facturedeductiontype.Designation = X.GetCmp<TextField>("TxtDesignation").Text;
            facturedeductiontype.Auto = bool.Parse(X.GetCmp<Checkbox>("ChkIsAuto").Value.ToString());
            facturedeductiontype.Taux = string.IsNullOrEmpty(X.GetCmp<TextField>("TxtTauxDeduction").RawValue.ToString()) ? 0 : decimal.Parse(X.GetCmp<TextField>("TxtTauxDeduction").RawValue.ToString());
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
            X.GetCmp<RowSelectionModel>("rowSelectionFactureCommercialeDeductionType").DeselectAll();
        }


        #endregion

    }
}