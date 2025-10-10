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
    public class LaboratoireController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Laboratoire
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);
            ViewBag.SiteParDefaut = mParam.Site;
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{5AB1B0A4-D655-43AE-8D53-FDAE563AD571}", UserName) == false)
                X.GetCmp<Button>("btnNewLaboratoire").Disable();
            else
                X.GetCmp<Button>("btnNewLaboratoire").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{DA865668-FF71-4522-86A6-65EFA12F318A}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListLaboratoire").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListLaboratoire").Enable();

            X.GetCmp<Hidden>("LbhiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{5AB1B0A4-D655-43AE-8D53-FDAE563AD571}", UserName));
            X.GetCmp<Hidden>("LbhiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{8061191D-2040-40D9-BEE4-48952C86A623}", UserName));
            X.GetCmp<Hidden>("LbhiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{7DC3CB83-11E4-478A-9C8D-1F14B71AFAFD}", UserName));
            X.GetCmp<Hidden>("LbhiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{BD5081C1-B0AA-4995-9C55-901A70D91524}", UserName));
            X.GetCmp<Hidden>("LbhiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{DA865668-FF71-4522-86A6-65EFA12F318A}", UserName));
            X.GetCmp<Hidden>("LbhiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{CD9371E8-16DE-4DA7-8924-333FD4BD956C}", UserName));

            return View();
        }

        public ActionResult LoadLaboratoire(int? site = null)
        {
            Parametres mParam = new Parametres(0);

            if (string.IsNullOrEmpty(site.ToString()))
                site = mParam.Site;
            
            List<DataPersist> listeLabo = new Laboratoire().fnSelect(0, (int)site);
            
            return this.Store(listeLabo);
        }

        public ActionResult LoadLaboratoireAll(int? site = null)
        {
            
            Parametres mParam = new Parametres(0);

            if (string.IsNullOrEmpty(site.ToString()))
                site = mParam.Site;

            List<DataPersist> listeLabo = new Laboratoire().fnSelect(0, (int)site);

            Laboratoire labo = new Laboratoire();
            labo.ID = -1;
            labo.Designation = "{Tous}";

            listeLabo.Insert(0, labo);
            labo = listeLabo[0] as Laboratoire;

            return this.Store(listeLabo);
        }

        public ActionResult OnAdd()
        {            

            LaboratoireViewModel LaboratoireVm = new LaboratoireViewModel();
            Parametres mParam = new Parametres();

            LaboratoireVm._Laboratoire = new Laboratoire();
            LaboratoireVm._Laboratoire.Sites = new Site();
            LaboratoireVm._Laboratoire.Sites.ID = mParam.Site;
            LaboratoireVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLaboratoire", Model = LaboratoireVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {            

            LaboratoireViewModel LaboratoireVm = new LaboratoireViewModel();

            LaboratoireVm._Laboratoire = JSON.Deserialize<Laboratoire>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            LaboratoireVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLaboratoire", Model = LaboratoireVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {            

            LaboratoireViewModel LaboratoireVm = new LaboratoireViewModel();

            LaboratoireVm._Laboratoire = JSON.Deserialize<Laboratoire>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            LaboratoireVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLaboratoire", Model = LaboratoireVm };

        }

        public ActionResult SubmitFormMethod()
        {
            try
            {
                Laboratoire laboratoire = new Laboratoire();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    laboratoire.IsNew = true;
                else
                {
                    laboratoire.IsNew = false;

                    laboratoire.fnGet(int.Parse(GetFormValue("TxtLaboratoireID")));

                    if (laboratoire == null || laboratoire.ID == 0)
                        throw new Exception("SubmitFormMethod : Laboratory load failed.");
                }

                laboratoire = MapFormToObject(laboratoire);

                bool result = laboratoire.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeLaboratoire");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, laboratoire);
                        X.GetCmp<RowSelectionModel>("rowSelectionLaboratoire").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(laboratoire.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(laboratoire);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormLaboratoire").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Laboratory : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemStatus, string ItemSite)
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
            int SiteID = GetCritriaValue(ItemSite);
            var liste = new Laboratoire().fnSelect(status, SiteID);
            
            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Laboratoire laboratoire = JSON.Deserialize<Laboratoire>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = laboratoire.fnGet(laboratoire.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Laboratory loading failed.");
                //(string)Session["userName"];
                laboratoire.UtilisateurModification = (string)Session["userName"];

                if (laboratoire.Desactive)
                    result = laboratoire.fnActivate();
                else
                    result = laboratoire.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Laboratory, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeLaboratoire");

                    ModelProxy mProxy = mstore.GetById(laboratoire.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(laboratoire);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Laboratory : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("LaboratoireCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus, string ItemSite)
        {
            Store mstore = X.GetCmp<Store>("storeListeLaboratoire");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus),
                                new Ext.Net.Parameter("ItemSite", ItemSite)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("LaboratoireCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Laboratoire MapFormToObject(Laboratoire laboratoire)
        {
            laboratoire.Designation = X.GetCmp<TextField>("TxtDesignationLaboratoire").Text;
            //(string)Session["userName"]
            laboratoire.UtilisateurCreation = (string)Session["userName"];
            laboratoire.UtilisateurModification = (string)Session["userName"];

            laboratoire.Sites = new Site();
            laboratoire.Sites.ID = int.Parse(GetFormValue("cmbSite"));
            laboratoire.Sites.Nom = X.GetCmp<ComboBox>("cmbSite").SelectedItem.Text.ToString();

            return laboratoire;
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
            X.GetCmp<RowSelectionModel>("rowSelectionLaboratoire").DeselectAll();
        }


        #endregion

    }
}