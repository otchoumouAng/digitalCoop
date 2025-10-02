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
    public class CampagneController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";
        // GET: FournisseurType
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{472FB43C-5155-4126-95A1-E0D5D19D134F}", UserName) == false)
                X.GetCmp<Button>("btnNewCampagne").Disable();
            else
                X.GetCmp<Button>("btnNewCampagne").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{6CD86DD2-48D2-40DC-AA34-95D50515BD99}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListCropYear").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListCropYear").Enable();

            X.GetCmp<Hidden>("CphiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{472FB43C-5155-4126-95A1-E0D5D19D134F}", UserName));
            X.GetCmp<Hidden>("CphiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{2B82992F-8A07-430C-B295-1496AC7198D5}", UserName));
            X.GetCmp<Hidden>("CphiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{5475903B-6299-4792-8A4B-12114CF4D92C}", UserName));
            X.GetCmp<Hidden>("CphiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{99263BDB-AA17-46AA-9FBC-1C84B3C50176}", UserName));
            X.GetCmp<Hidden>("CphiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{07D82749-0A09-4D2B-BCE0-EA0A015DA5E0}", UserName));
            X.GetCmp<Hidden>("CphiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{EE769BF6-9B19-4A2C-B7FB-CB3380A4513E}", UserName));

            return View();
        }

        public ActionResult LoadCampagne()
        {
            List<DataPersist> myListe = new Campagne().fnSelect(0);
            Campagne mclass = new Campagne();                 

            return this.Store(myListe);
        }

        public ActionResult LoadCampagneAll()
        {
            List<DataPersist> myListe = new Campagne().fnSelect(0);
            Campagne mclass = new Campagne();
                        
            mclass.Designation = "{Toute}";

            myListe.Insert(0, mclass);
            mclass = myListe[0] as Campagne;

            return this.Store(myListe);

        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            CampagneViewModel campagneVm = new CampagneViewModel();

            campagneVm._Campagne = new Campagne();
            campagneVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormCampagne", Model = campagneVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            CampagneViewModel campagneVm = new CampagneViewModel();

            campagneVm._Campagne = JSON.Deserialize<Campagne>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            campagneVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormCampagne", Model = campagneVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            CampagneViewModel campagneVm = new CampagneViewModel();

            campagneVm._Campagne = JSON.Deserialize<Campagne>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            campagneVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormCampagne", Model = campagneVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Campagne campagne = new Campagne();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    campagne.IsNew = true;
                else
                {
                    campagne.IsNew = false;

                    campagne.fnGet(GetFormValue("TxtCampagneID"));

                    if (campagne == null || campagne.Designation == string.Empty)
                        throw new Exception("SubmitFormMethod : Campagne load failed.");
                }

                campagne = MapFormToObject(campagne);

                bool result = campagne.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeCampagne");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, campagne);
                        X.GetCmp<RowSelectionModel>("rowSelectionCampagne").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(campagne.Designation);

                        mProxy.BeginEdit();

                        mProxy.Set(campagne);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormCampagne").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Campagne : Data Validation",
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
            var listeCampagne = new Campagne().fnSelect(status);           

            return this.Store(listeCampagne);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Campagne campagne = JSON.Deserialize<Campagne>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = campagne.fnGet(campagne.Designation);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Campagne loading failed.");

                campagne.UtilisateurModification = (string)Session["userName"];                

                if (campagne.Desactive)
                    result = campagne.fnActivate();
                else
                    result = campagne.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Campagne,  Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeCampagne");

                    ModelProxy mProxy = mstore.GetById(campagne.Designation);

                    mProxy.BeginEdit();

                    mProxy.Set(campagne);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Campagne : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CampagneCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeCampagne");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("CampagneCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Campagne MapFormToObject(Campagne campagne)
        {
            campagne.Designation = X.GetCmp<TextField>("TxtDesignationCampagne").Text;
            campagne.DateDebut = DateTime.Parse(X.GetCmp<DateField>("TxtDateDebutCampagne").RawText);
            campagne.DateFin = DateTime.Parse(X.GetCmp<DateField>("TxtDateFinCampagne").RawText);

            campagne.UtilisateurCreation = (string)Session["userName"];
            campagne.UtilisateurModification = (string)Session["userName"];           

            return campagne;
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
            X.GetCmp<RowSelectionModel>("rowSelectionCampagne").DeselectAll();
        }

        public void CreateIconsList()
        {
            try
            {
                //Ext.Net.ResourceManager.RegisterGlobalIcon(Icon.PageCancel);

                //string url =  Ext.Net.ResourceManager.GetInstance().GetIconUrl(Icon.Tick);
            }
            catch (Exception ex)
            {

                X.Msg.Alert("frmLBC_ReceptionInDistrict_Overview : CreateIconList", ex.Message).Show();
            }
        }
        #endregion


    }
}