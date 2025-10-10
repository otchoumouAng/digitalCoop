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
    public class FournisseurGroupeController : BaseController
    {

        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: FournisseurGroupe
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{661679E9-14AF-47B3-A08E-DC39275BB511}", UserName) == false)
                X.GetCmp<Button>("btnNewFournisseurGroupe").Disable();
            else
                X.GetCmp<Button>("btnNewFournisseurGroupe").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{E097F617-4245-4E56-ABAD-5D48490C8252}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListFrsGroupe").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListFrsGroupe").Enable();

            X.GetCmp<Hidden>("FghiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{661679E9-14AF-47B3-A08E-DC39275BB511}", UserName));
            X.GetCmp<Hidden>("FghiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{ED1AA0E7-30A7-47F5-82D6-8EFDCFB3180C}", UserName));
            X.GetCmp<Hidden>("FghiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{0158BFB4-47B0-42CC-96C5-87511B10180D}", UserName));
            X.GetCmp<Hidden>("FghiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{302923C8-B3AA-4C85-9149-068FFF9F6C64}", UserName));
            X.GetCmp<Hidden>("FghiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{47422283-CD12-414C-822F-1F34E61E93BF}", UserName));
            X.GetCmp<Hidden>("FghiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{BD0E5811-E40D-4C00-8735-B6E93CD57F00}", UserName));

            return View();
        }

        public ActionResult LoadGroupFournisseur()
        {
            List<DataPersist> mList = new FournisseurGroupe().fnSelect(0);

            FournisseurGroupe mclass = new FournisseurGroupe();          
            
            return this.Store(mList);
        }

        public ActionResult LoadGroupFournisseurSite()
        {
            List<DataPersist> mList = new FournisseurGroupe().fnSelect(0,1);

            FournisseurGroupe mclass = new FournisseurGroupe();

            return this.Store(mList);
        }

        public ActionResult LoadGroupFournisseurAll()
        {
            List<DataPersist> mList = new FournisseurGroupe().fnSelect(0);

            FournisseurGroupe mclass = new FournisseurGroupe();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as FournisseurGroupe;

            return this.Store(mList);

        }

        public ActionResult LoadGroupeFournisseurWithBlank()
        {
            List<DataPersist> mList = new FournisseurGroupe().fnSelect(0);

            FournisseurGroupe mclass = new FournisseurGroupe();

            mclass.ID = -1;
            mclass.Designation = "";

            mList.Insert(0, mclass);
            mclass = mList[0] as FournisseurGroupe;

            return this.Store(mList);

        }

        public ActionResult LoadGroupFournisseurSiteAll()
        {
            List<DataPersist> mList = new FournisseurGroupe().fnSelect(0,1);

            FournisseurGroupe mclass = new FournisseurGroupe();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as FournisseurGroupe;

            return this.Store(mList);

        }

        public ActionResult OnAdd()
        {
            

            FournisseurGroupeViewModel FournisseurGroupeVm = new FournisseurGroupeViewModel();

            FournisseurGroupeVm._FournisseurGroupe = new FournisseurGroupe();
            FournisseurGroupeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFournisseurGroupe", Model = FournisseurGroupeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            

            FournisseurGroupeViewModel FournisseurGroupeVm = new FournisseurGroupeViewModel();

            FournisseurGroupeVm._FournisseurGroupe = JSON.Deserialize<FournisseurGroupe>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            FournisseurGroupeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFournisseurGroupe", Model = FournisseurGroupeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            

            FournisseurGroupeViewModel FournisseurGroupeVm = new FournisseurGroupeViewModel();

            FournisseurGroupeVm._FournisseurGroupe = JSON.Deserialize<FournisseurGroupe>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            FournisseurGroupeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFournisseurGroupe", Model = FournisseurGroupeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                FournisseurGroupe fournisseurgroupe = new FournisseurGroupe();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    fournisseurgroupe.IsNew = true;
                else
                {
                    fournisseurgroupe.IsNew = false;

                    fournisseurgroupe.fnGet(int.Parse(GetFormValue("TxtFournisseurGroupeID")));

                    if (fournisseurgroupe == null || fournisseurgroupe.ID == 0)
                        throw new Exception("SubmitFormMethod : Groupe de fournisseur load failed.");
                }

                fournisseurgroupe = MapFormToObject(fournisseurgroupe);

                bool result = fournisseurgroupe.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeFournisseurGroupe");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, fournisseurgroupe);
                        X.GetCmp<RowSelectionModel>("rowSelectionFournisseurGroupe").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(fournisseurgroupe.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(fournisseurgroupe);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormFournisseurGroupe").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Groupe de fournisseur : Data Validation",
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
            var liste = new FournisseurGroupe().fnSelect(status);
            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                FournisseurGroupe fournisseurgroupe = JSON.Deserialize<FournisseurGroupe>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = fournisseurgroupe.fnGet(fournisseurgroupe.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Groupe de fournisseur loading failed.");
                //(string)Session["userName"];
                fournisseurgroupe.UtilisateurModification = (string)Session["userName"];

                if (fournisseurgroupe.Desactive)
                    result = fournisseurgroupe.fnActivate();
                else
                    result = fournisseurgroupe.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Groupe de fournisseur operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeFournisseurGroupe");

                    ModelProxy mProxy = mstore.GetById(fournisseurgroupe.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(fournisseurgroupe);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Groupe de fournisseur : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("FournisseurGroupeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeFournisseurGroupe");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("FournisseurGroupeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private FournisseurGroupe MapFormToObject(FournisseurGroupe fournisseurgroupe)
        {
            fournisseurgroupe.Designation = X.GetCmp<TextField>("TxtDesignationFournisseurGroupe").Text;
            fournisseurgroupe.EstVisibleSurSite = bool.Parse(X.GetCmp<Checkbox>("TxtVisibilityOnSite").Value.ToString());
            //(string)Session["userName"]
            fournisseurgroupe.UtilisateurCreation = (string)Session["userName"];
            fournisseurgroupe.UtilisateurModification = (string)Session["userName"];

            return fournisseurgroupe;
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
            X.GetCmp<RowSelectionModel>("rowSelectionFournisseurGroupe").DeselectAll();
        }


        #endregion


    }
}