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
    public class ProduitGammeController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: ProduitGamme
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{95e9fa83-5f05-456a-831b-59da958a978c}", UserName) == false)
                X.GetCmp<Button>("btnNewProduitGamme").Disable();
            else
                X.GetCmp<Button>("btnNewProduitGamme").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{76e75cef-05e0-40f4-84c9-57144e3e14eb}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListProduitGamme").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListProduitGamme").Enable();

            X.GetCmp<Hidden>("ProduitGammehiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{95e9fa83-5f05-456a-831b-59da958a978c}", UserName));
            X.GetCmp<Hidden>("ProduitGammehiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{76e75cef-05e0-40f4-84c9-57144e3e14eb}", UserName));
            X.GetCmp<Hidden>("ProduitGammehiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{7ac92df4-3f26-44df-bc73-6f946ad93142}", UserName));
            X.GetCmp<Hidden>("ProduitGammehiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{e03fdfef-d5b8-4eb9-ae7b-f906f8f47388}", UserName));
            X.GetCmp<Hidden>("ProduitGammehiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{966a27e7-46d2-4e0c-8f80-72c133cd57a6}", UserName));
            X.GetCmp<Hidden>("ProduitGammehiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{9fb7cff5-dd36-46f6-9b79-ff4e88a14da0}", UserName));

            return View();
        }

        public ActionResult LoadProduitGamme()
        {
            List<DataPersist> mList = new ProduitGamme().fnSelect(0);

            ProduitGamme mclass = new ProduitGamme();

            if (mList.Count > 0)
                mclass = mList[0] as ProduitGamme;

            return this.Store(mList);
        }


        public ActionResult LoadProduitGammeAll()
        {
            List<DataPersist> mList = new ProduitGamme().fnSelect(0);

            ProduitGamme mclass = new ProduitGamme();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as ProduitGamme;

            return this.Store(mList);
        }

        public ActionResult LoadAllProduitGammeByAccess()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAllAccess = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            List<DataPersist> mList = null;
            ProduitGamme mclass = new ProduitGamme();

            if (HasAllAccess)
            {
                mList = new ProduitGamme().fnSelect(0);

                mclass.ID = -1;
                mclass.Designation = "{Tous}";

                mList.Insert(0, mclass);
                mclass = mList[0] as ProduitGamme;
            }
            else
            {
                bool result = false;// mclass.fnGetByUserName(UserName);
                mList = new List<DataPersist>();

                mList.Insert(0, mclass);
                mclass = mList[0] as ProduitGamme;
            }
            //= new ProduitGamme().fnSelect(0);

            return this.Store(mList);

        }

        public ActionResult LoadProduitGammeByAccess()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAllAccess = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            List<DataPersist> mList = null;
            ProduitGamme mclass = new ProduitGamme();

            if (HasAllAccess)
            {
                mList = new ProduitGamme().fnSelect(0);
                mclass = mList[0] as ProduitGamme;
            }
            else
            {
                //bool result = mclass.fnGetByUserName(UserName);
                mList = new List<DataPersist>();

                mList.Insert(0, mclass);
                mclass = mList[0] as ProduitGamme;
            }
            //= new ProduitGamme().fnSelect(0);

            return this.Store(mList);

        }

        public ActionResult LoadSpecificProduitGamme()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool CanConsultOverProduitGamme = HasAccessFunction.fnGetUserAccessStatus("{17a9e19c-4667-4c65-b173-3749ab8c263e}", UserName);
            List<DataPersist> mList = null;
            ProduitGamme mclass = new ProduitGamme();

            bool result = false;// mclass.fnGetByUserName(UserName);
            mList = new List<DataPersist>();

            mList.Insert(0, mclass);
            mclass = mList[0] as ProduitGamme;

            if (CanConsultOverProduitGamme)
            {
                mclass = new ProduitGamme();
                result = false;//mclass.fnGetDefaultProduitGamme();
                mList.Insert(1, mclass);

                mclass = new ProduitGamme();
                mclass.ID = -1;
                mclass.Designation = "{Tous}";

                mList.Insert(0, mclass);
                mclass = mList[0] as ProduitGamme;
            }

            //= new ProduitGamme().fnSelect(0);

            return this.Store(mList);

        }



        //public ActionResult SelectToPriceGood()
        //{
        //    string UserName = (string)Session["userName"];
        //    var listeProduitGamme = new ProduitGamme().fnSelectAvailableForPrice(UserName);
        //    return this.Store(listeProduitGamme);

        //}
        //public ActionResult SelectToPrice(string ItemProduitGamme)
        //{
        //    if (!string.IsNullOrEmpty(ItemProduitGamme))
        //    {
        //        int id = Int32.Parse(ItemProduitGamme);
        //        var listeProduitGamme = new ProduitGamme().fnSelectToPrice(id);
        //        return this.Store(listeProduitGamme);
        //    }
        //    else
        //    {
        //        var listAllProduitGamme = new ProduitGamme().fnSelect(0);
        //        return this.Store(listAllProduitGamme);
        //    }
        //}

        public ActionResult OnAdd()
        {


            ProduitGammeViewModel ProduitGammeVm = new ProduitGammeViewModel();

            ProduitGammeVm._ProduitGamme = new ProduitGamme();
            ProduitGammeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProduitGamme", Model = ProduitGammeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {


            ProduitGammeViewModel ProduitGammeVm = new ProduitGammeViewModel();

            ProduitGammeVm._ProduitGamme = JSON.Deserialize<ProduitGamme>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ProduitGammeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProduitGamme", Model = ProduitGammeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {


            ProduitGammeViewModel ProduitGammeVm = new ProduitGammeViewModel();

            ProduitGammeVm._ProduitGamme = JSON.Deserialize<ProduitGamme>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ProduitGammeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProduitGamme", Model = ProduitGammeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                ProduitGamme ProduitGamme = new ProduitGamme();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    ProduitGamme.IsNew = true;
                else
                {
                    ProduitGamme.IsNew = false;

                    //var idStr = GetFormValue("TxtProduitGammeID");
                    //System.Diagnostics.Debug.WriteLine($"Valeur reçue pour TxtProduitGammeID : '{idStr}'");

                    ProduitGamme.fnGet(int.Parse(GetFormValue("TxtProduitGammeID")));

                    if (ProduitGamme == null || ProduitGamme.ID == 0)
                        throw new Exception("SubmitFormMethod : Location, load failed.");
                }

                ProduitGamme = MapFormToObject(ProduitGamme);

                bool result = ProduitGamme.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeProduitGamme");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, ProduitGamme);
                        X.GetCmp<RowSelectionModel>("rowSelectionProduitGamme").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(ProduitGamme.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(ProduitGamme);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormProduitGamme").Close();
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
            var liste = new ProduitGamme().fnSelect(status);
            //var pagning = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                ProduitGamme ProduitGamme = JSON.Deserialize<ProduitGamme>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = ProduitGamme.fnGet(ProduitGamme.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Location, loading failed.");
                //(string)Session["userName"];
                ProduitGamme.UtilisateurModification = (string)Session["userName"];

                if (ProduitGamme.Desactive)
                    result = ProduitGamme.fnActivate();
                else
                    result = ProduitGamme.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Location, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeProduitGamme");

                    ModelProxy mProxy = mstore.GetById(ProduitGamme.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(ProduitGamme);

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
            FormPanel mform = X.GetCmp<FormPanel>("ProduitGammeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeProduitGamme");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ProduitGammeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private ProduitGamme MapFormToObject(ProduitGamme ProduitGamme)
        {

            ProduitGamme.Designation = X.GetCmp<TextField>("TxtDesignation").Text;
            ProduitGamme.ProduitDeBase = X.GetCmp<TextField>("TxtProduitDeBase").Text;
            ProduitGamme.ProduitDerive = X.GetCmp<Checkbox>("ChkProduitDerive").Checked;

            //ProduitGamme.Provenance = null;
            //if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbDefautProvenance").Text))
            //{
            //    ProduitGamme.Provenance = new Provenance();
            //    ProduitGamme.Provenance.ID = int.Parse(X.GetCmp<ComboBox>("CmbDefautProvenance").SelectedItem.Value);
            //    ProduitGamme.Provenance.Designation = X.GetCmp<ComboBox>("CmbDefautProvenance").SelectedItem.Text;
            //}

            //ProduitGamme.Destination = null;
            //if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbDefautDestination").Text))
            //{
            //    ProduitGamme.Destination = new Destination();
            //    ProduitGamme.Destination.ID = int.Parse(X.GetCmp<ComboBox>("CmbDefautDestination").SelectedItem.Value);
            //    ProduitGamme.Destination.Designation = X.GetCmp<ComboBox>("CmbDefautDestination").SelectedItem.Text;
            //}
            //(string)Session["userName"]
            ProduitGamme.UtilisateurCreation = (string)Session["userName"];
            ProduitGamme.UtilisateurModification = (string)Session["userName"];

            return ProduitGamme;
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
            X.GetCmp<RowSelectionModel>("rowSelectionProduitGamme").DeselectAll();
        }


        #endregion

    }
}