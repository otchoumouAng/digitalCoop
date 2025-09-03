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
    public class LigneProductionController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: LigneProduction
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{1f4b2a4f-f3da-4e6e-a582-dc0cb518b4f6}", UserName) == false)
                X.GetCmp<Button>("btnNewLigneProduction").Disable();
            else
                X.GetCmp<Button>("btnNewLigneProduction").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{a30d1cf7-886a-41bb-a23f-0d08c0f052fd}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListLigneProduction").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListLigneProduction").Enable();

            X.GetCmp<Hidden>("LigneProductionhiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{1f4b2a4f-f3da-4e6e-a582-dc0cb518b4f6}", UserName));
            X.GetCmp<Hidden>("LigneProductionhiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{b46a7582-1942-4e94-8e6b-d848118a1e77}", UserName));
            X.GetCmp<Hidden>("LigneProductionhiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{461d1032-ebc5-43dd-9818-c4d923fbc5d8}", UserName));
            X.GetCmp<Hidden>("LigneProductionhiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{3361f9c6-8133-4972-848a-8cadb81c1865}", UserName));
            X.GetCmp<Hidden>("LigneProductionhiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{a30d1cf7-886a-41bb-a23f-0d08c0f052fd}", UserName));
            X.GetCmp<Hidden>("LigneProductionhiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{05500321-f248-45d0-a1aa-15b0213ab8dc}", UserName));

            return View();
        }

        public ActionResult LoadLigneProduction()
        {
            List<DataPersist> mList = new LigneProduction().fnSelect(0);

            LigneProduction mclass = new LigneProduction();

            if (mList.Count > 0)
                mclass = mList[0] as LigneProduction;

            return this.Store(mList);
        }


        public ActionResult LoadLigneProductionAll()
        {
            List<DataPersist> mList = new LigneProduction().fnSelect(0);

            LigneProduction mclass = new LigneProduction();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as LigneProduction;

            return this.Store(mList);
        }

        public ActionResult LoadAllLigneProductionByAccess()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAllAccess = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            List<DataPersist> mList = null;
            LigneProduction mclass = new LigneProduction();

            if (HasAllAccess)
            {
                mList = new LigneProduction().fnSelect(0);

                mclass.ID = -1;
                mclass.Designation = "{Tous}";

                mList.Insert(0, mclass);
                mclass = mList[0] as LigneProduction;
            }
            else
            {
                bool result = false;// mclass.fnGetByUserName(UserName);
                mList = new List<DataPersist>();

                mList.Insert(0, mclass);
                mclass = mList[0] as LigneProduction;
            }
            //= new LigneProduction().fnSelect(0);

            return this.Store(mList);

        }

        public ActionResult LoadLigneProductionByAccess()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAllAccess = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            List<DataPersist> mList = null;
            LigneProduction mclass = new LigneProduction();

            if (HasAllAccess)
            {
                mList = new LigneProduction().fnSelect(0);
                mclass = mList[0] as LigneProduction;
            }
            else
            {
                //bool result = mclass.fnGetByUserName(UserName);
                mList = new List<DataPersist>();

                mList.Insert(0, mclass);
                mclass = mList[0] as LigneProduction;
            }
            //= new LigneProduction().fnSelect(0);

            return this.Store(mList);

        }

        public ActionResult LoadSpecificLigneProduction()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool CanConsultOverLigneProduction = HasAccessFunction.fnGetUserAccessStatus("{17a9e19c-4667-4c65-b173-3749ab8c263e}", UserName);
            List<DataPersist> mList = null;
            LigneProduction mclass = new LigneProduction();

            bool result = false;// mclass.fnGetByUserName(UserName);
            mList = new List<DataPersist>();

            mList.Insert(0, mclass);
            mclass = mList[0] as LigneProduction;

            if (CanConsultOverLigneProduction)
            {
                mclass = new LigneProduction();
                result = false;//mclass.fnGetDefaultLigneProduction();
                mList.Insert(1, mclass);

                mclass = new LigneProduction();
                mclass.ID = -1;
                mclass.Designation = "{Tous}";

                mList.Insert(0, mclass);
                mclass = mList[0] as LigneProduction;
            }

            //= new LigneProduction().fnSelect(0);

            return this.Store(mList);

        }



        //public ActionResult SelectToPriceGood()
        //{
        //    string UserName = (string)Session["userName"];
        //    var listeLigneProduction = new LigneProduction().fnSelectAvailableForPrice(UserName);
        //    return this.Store(listeLigneProduction);

        //}
        //public ActionResult SelectToPrice(string ItemLigneProduction)
        //{
        //    if (!string.IsNullOrEmpty(ItemLigneProduction))
        //    {
        //        int id = Int32.Parse(ItemLigneProduction);
        //        var listeLigneProduction = new LigneProduction().fnSelectToPrice(id);
        //        return this.Store(listeLigneProduction);
        //    }
        //    else
        //    {
        //        var listAllLigneProduction = new LigneProduction().fnSelect(0);
        //        return this.Store(listAllLigneProduction);
        //    }
        //}

        public ActionResult OnAdd()
        {


            LigneProductionViewModel LigneProductionVm = new LigneProductionViewModel();

            LigneProductionVm._LigneProduction = new LigneProduction();
            LigneProductionVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLigneProduction", Model = LigneProductionVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {


            LigneProductionViewModel LigneProductionVm = new LigneProductionViewModel();

            LigneProductionVm._LigneProduction = JSON.Deserialize<LigneProduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            LigneProductionVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLigneProduction", Model = LigneProductionVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {


            LigneProductionViewModel LigneProductionVm = new LigneProductionViewModel();

            LigneProductionVm._LigneProduction = JSON.Deserialize<LigneProduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            LigneProductionVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLigneProduction", Model = LigneProductionVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                LigneProduction LigneProduction = new LigneProduction();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    LigneProduction.IsNew = true;
                else
                {
                    LigneProduction.IsNew = false;

                    //var idStr = GetFormValue("TxtLigneProductionID");
                    //System.Diagnostics.Debug.WriteLine($"Valeur reçue pour TxtLigneProductionID : '{idStr}'");

                    LigneProduction.fnGet(int.Parse(GetFormValue("TxtLigneProductionID")));

                    if (LigneProduction == null || LigneProduction.ID == 0)
                        throw new Exception("SubmitFormMethod : Location, load failed.");
                }

                LigneProduction = MapFormToObject(LigneProduction);

                bool result = LigneProduction.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeLigneProduction");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, LigneProduction);
                        X.GetCmp<RowSelectionModel>("rowSelectionLigneProduction").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(LigneProduction.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(LigneProduction);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormLigneProduction").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Location : Data Validation",
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
            var liste = new LigneProduction().fnSelect(status);
            //var pagning = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                LigneProduction LigneProduction = JSON.Deserialize<LigneProduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = LigneProduction.fnGet(LigneProduction.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Location, loading failed.");
                //(string)Session["userName"];
                LigneProduction.UtilisateurModification = (string)Session["userName"];

                if (LigneProduction.Desactive)
                    result = LigneProduction.fnActivate();
                else
                    result = LigneProduction.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Location, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeLigneProduction");

                    ModelProxy mProxy = mstore.GetById(LigneProduction.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(LigneProduction);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Location : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("LigneProductionCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeLigneProduction");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("LigneProductionCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private LigneProduction MapFormToObject(LigneProduction LigneProduction)
        {

            LigneProduction.Designation = X.GetCmp<TextField>("TxtDesignation").Text;
            

            //LigneProduction.Provenance = null;
            //if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbDefautProvenance").Text))
            //{
            //    LigneProduction.Provenance = new Provenance();
            //    LigneProduction.Provenance.ID = int.Parse(X.GetCmp<ComboBox>("CmbDefautProvenance").SelectedItem.Value);
            //    LigneProduction.Provenance.Designation = X.GetCmp<ComboBox>("CmbDefautProvenance").SelectedItem.Text;
            //}

            //LigneProduction.Destination = null;
            //if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbDefautDestination").Text))
            //{
            //    LigneProduction.Destination = new Destination();
            //    LigneProduction.Destination.ID = int.Parse(X.GetCmp<ComboBox>("CmbDefautDestination").SelectedItem.Value);
            //    LigneProduction.Destination.Designation = X.GetCmp<ComboBox>("CmbDefautDestination").SelectedItem.Text;
            //}
            //(string)Session["userName"]
            LigneProduction.UtilisateurCreation = (string)Session["userName"];
            LigneProduction.UtilisateurModification = (string)Session["userName"];

            return LigneProduction;
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
            X.GetCmp<RowSelectionModel>("rowSelectionLigneProduction").DeselectAll();
        }


        #endregion

    }
}