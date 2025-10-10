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
    public class ProduitFiniController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: ProduitFini
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{0cd5f49e-8c09-465e-8ebf-90d07aab6a3f}", UserName) == false)
                X.GetCmp<Button>("btnNewProduitFini").Disable();
            else
                X.GetCmp<Button>("btnNewProduitFini").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{2d51d557-de29-4de8-8c44-19c44a869292}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListProduitFini").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListProduitFini").Enable();

            X.GetCmp<Hidden>("ProduitFinihiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{0cd5f49e-8c09-465e-8ebf-90d07aab6a3f}", UserName));
            X.GetCmp<Hidden>("ProduitFinihiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{cab21a0d-e3f5-4e17-a924-c5c99530ae02}", UserName));
            X.GetCmp<Hidden>("ProduitFinihiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{29a4e115-165b-44f9-8aae-129e485487b7}", UserName));
            X.GetCmp<Hidden>("ProduitFinihiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{f768f441-9a07-48fa-9264-1714b09c1c34}", UserName));
            X.GetCmp<Hidden>("ProduitFinihiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{2d51d557-de29-4de8-8c44-19c44a869292}", UserName));
            X.GetCmp<Hidden>("ProduitFinihiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{12d5692f-c3c1-489b-9338-3b72538f425a}", UserName));

            return View();
        }

        public ActionResult LoadProduitFini()
        {
            List<DataPersist> mList = new ProduitFini().fnSelect(0);

            ProduitFini mclass = new ProduitFini();

            if (mList.Count > 0)
                mclass = mList[0] as ProduitFini;

            return this.Store(mList);
        }


        public ActionResult LoadProduitFiniAll()
        {
            List<DataPersist> mList = new ProduitFini().fnSelect(0);

            ProduitFini mclass = new ProduitFini();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as ProduitFini;

            return this.Store(mList);
        }

        public ActionResult LoadAllProduitFiniByAccess()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAllAccess = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            List<DataPersist> mList = null;
            ProduitFini mclass = new ProduitFini();

            if (HasAllAccess)
            {
                mList = new ProduitFini().fnSelect(0);

                mclass.ID = -1;
                mclass.Designation = "{Tous}";

                mList.Insert(0, mclass);
                mclass = mList[0] as ProduitFini;
            }
            else
            {
                bool result = false;// mclass.fnGetByUserName(UserName);
                mList = new List<DataPersist>();

                mList.Insert(0, mclass);
                mclass = mList[0] as ProduitFini;
            }
            //= new ProduitFini().fnSelect(0);

            return this.Store(mList);

        }

        public ActionResult LoadProduitFiniByAccess()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAllAccess = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            List<DataPersist> mList = null;
            ProduitFini mclass = new ProduitFini();

            if (HasAllAccess)
            {
                mList = new ProduitFini().fnSelect(0);
                mclass = mList[0] as ProduitFini;
            }
            else
            {
                //bool result = mclass.fnGetByUserName(UserName);
                mList = new List<DataPersist>();

                mList.Insert(0, mclass);
                mclass = mList[0] as ProduitFini;
            }
            //= new ProduitFini().fnSelect(0);

            return this.Store(mList);

        }

        public ActionResult LoadSpecificProduitFini()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool CanConsultOverProduitFini = HasAccessFunction.fnGetUserAccessStatus("{17a9e19c-4667-4c65-b173-3749ab8c263e}", UserName);
            List<DataPersist> mList = null;
            ProduitFini mclass = new ProduitFini();

            bool result = false;// mclass.fnGetByUserName(UserName);
            mList = new List<DataPersist>();

            mList.Insert(0, mclass);
            mclass = mList[0] as ProduitFini;

            if (CanConsultOverProduitFini)
            {
                mclass = new ProduitFini();
                result = false;//mclass.fnGetDefaultProduitFini();
                mList.Insert(1, mclass);

                mclass = new ProduitFini();
                mclass.ID = -1;
                mclass.Designation = "{Tous}";

                mList.Insert(0, mclass);
                mclass = mList[0] as ProduitFini;
            }

            //= new ProduitFini().fnSelect(0);

            return this.Store(mList);

        }



        //public ActionResult SelectToPriceGood()
        //{
        //    string UserName = (string)Session["userName"];
        //    var listeProduitFini = new ProduitFini().fnSelectAvailableForPrice(UserName);
        //    return this.Store(listeProduitFini);

        //}
        //public ActionResult SelectToPrice(string ItemProduitFini)
        //{
        //    if (!string.IsNullOrEmpty(ItemProduitFini))
        //    {
        //        int id = Int32.Parse(ItemProduitFini);
        //        var listeProduitFini = new ProduitFini().fnSelectToPrice(id);
        //        return this.Store(listeProduitFini);
        //    }
        //    else
        //    {
        //        var listAllProduitFini = new ProduitFini().fnSelect(0);
        //        return this.Store(listAllProduitFini);
        //    }
        //}

        public ActionResult OnAdd()
        {


            ProduitFiniViewModel ProduitFiniVm = new ProduitFiniViewModel();

            ProduitFiniVm._ProduitFini = new ProduitFini();
            ProduitFiniVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProduitFini", Model = ProduitFiniVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {


            ProduitFiniViewModel ProduitFiniVm = new ProduitFiniViewModel();

            ProduitFiniVm._ProduitFini = JSON.Deserialize<ProduitFini>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ProduitFiniVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProduitFini", Model = ProduitFiniVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {


            ProduitFiniViewModel ProduitFiniVm = new ProduitFiniViewModel();

            ProduitFiniVm._ProduitFini = JSON.Deserialize<ProduitFini>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ProduitFiniVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProduitFini", Model = ProduitFiniVm };

        }
        
        public ActionResult SubmitFormMethod()
        {

            try
            {
                ProduitFini ProduitFini = new ProduitFini();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    ProduitFini.IsNew = true;
                else
                {
                    ProduitFini.IsNew = false;

                    //var idStr = GetFormValue("TxtProduitFiniID");
                    //System.Diagnostics.Debug.WriteLine($"Valeur reçue pour TxtProduitFiniID : '{idStr}'");

                    ProduitFini.fnGet(int.Parse(GetFormValue("TxtProduitFiniID")));

                    if (ProduitFini == null || ProduitFini.ID == 0)
                        throw new Exception("SubmitFormMethod : Location, load failed.");
                }

                ProduitFini = MapFormToObject(ProduitFini);

                bool result = ProduitFini.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeProduitFini");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, ProduitFini);
                        X.GetCmp<RowSelectionModel>("rowSelectionProduitFini").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(ProduitFini.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(ProduitFini);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormProduitFini").Close();
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
            var liste = new ProduitFini().fnSelect(status);
            //var pagning = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                ProduitFini ProduitFini = JSON.Deserialize<ProduitFini>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = ProduitFini.fnGet(ProduitFini.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Location, loading failed.");
                //(string)Session["userName"];
                ProduitFini.UtilisateurModification = (string)Session["userName"];

                if (ProduitFini.Desactive)
                    result = ProduitFini.fnActivate();
                else
                    result = ProduitFini.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Location, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeProduitFini");

                    ModelProxy mProxy = mstore.GetById(ProduitFini.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(ProduitFini);

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
            FormPanel mform = X.GetCmp<FormPanel>("ProduitFiniCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeProduitFini");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ProduitFiniCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private ProduitFini MapFormToObject(ProduitFini ProduitFini)
        {

            ProduitFini.Designation = X.GetCmp<TextField>("TxtDesignation").Text;
            ProduitFini.ProduitDeBase = X.GetCmp<TextField>("TxtProduitDeBase").Text;
            ProduitFini.ProduitDerive = X.GetCmp<Checkbox>("ChkProduitDerive").Checked;

            //ProduitFini.Provenance = null;
            //if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbDefautProvenance").Text))
            //{
            //    ProduitFini.Provenance = new Provenance();
            //    ProduitFini.Provenance.ID = int.Parse(X.GetCmp<ComboBox>("CmbDefautProvenance").SelectedItem.Value);
            //    ProduitFini.Provenance.Designation = X.GetCmp<ComboBox>("CmbDefautProvenance").SelectedItem.Text;
            //}

            //ProduitFini.Destination = null;
            //if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbDefautDestination").Text))
            //{
            //    ProduitFini.Destination = new Destination();
            //    ProduitFini.Destination.ID = int.Parse(X.GetCmp<ComboBox>("CmbDefautDestination").SelectedItem.Value);
            //    ProduitFini.Destination.Designation = X.GetCmp<ComboBox>("CmbDefautDestination").SelectedItem.Text;
            //}
            //(string)Session["userName"]
            ProduitFini.UtilisateurCreation = (string)Session["userName"];
            ProduitFini.UtilisateurModification = (string)Session["userName"];

            return ProduitFini;
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
            X.GetCmp<RowSelectionModel>("rowSelectionProduitFini").DeselectAll();
        }


        #endregion

    }
}