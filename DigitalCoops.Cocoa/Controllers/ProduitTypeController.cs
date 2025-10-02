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
    public class ProduitTypeController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: ProduitType
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{5d7e085f-99a4-4e48-a688-25e6e0845b5f}", UserName) == false)
                X.GetCmp<Button>("btnNewProduitType").Disable();
            else
                X.GetCmp<Button>("btnNewProduitType").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{9da33bde-06cb-484e-a8a4-b3b374429fed}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListProduitType").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListProduitType").Enable();

            X.GetCmp<Hidden>("ProduitTypehiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{5d7e085f-99a4-4e48-a688-25e6e0845b5f}", UserName));
            X.GetCmp<Hidden>("ProduitTypehiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{c16dd72b-d925-4a7e-b5f0-d7d142be879c}", UserName));
            X.GetCmp<Hidden>("ProduitTypehiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{1f4af822-9de1-490d-bc24-1aa742f18b4e}", UserName));
            X.GetCmp<Hidden>("ProduitTypehiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{11611511-163f-44ca-8200-7c8bc3e79f74}", UserName));
            X.GetCmp<Hidden>("ProduitTypehiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{9da33bde-06cb-484e-a8a4-b3b374429fed}", UserName));
            X.GetCmp<Hidden>("ProduitTypehiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{bc7ec95d-ddc3-4441-8f45-2b1d20d94a23}", UserName));

            return View();
        }

        public ActionResult LoadProduitType()
        {
            List<DataPersist> mList = new ProduitType().fnSelect(0);

            ProduitType mclass = new ProduitType();

            if (mList.Count > 0)
                mclass = mList[0] as ProduitType;

            return this.Store(mList);
        }


        public ActionResult LoadProduitTypeAll()
        {
            List<DataPersist> mList = new ProduitType().fnSelect(0);

            ProduitType mclass = new ProduitType();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as ProduitType;

            return this.Store(mList);
        }

        public ActionResult LoadAllProduitTypeByAccess()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAllAccess = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            List<DataPersist> mList = null;
            ProduitType mclass = new ProduitType();

            if (HasAllAccess)
            {
                mList = new ProduitType().fnSelect(0);

                mclass.ID = -1;
                mclass.Designation = "{Tous}";

                mList.Insert(0, mclass);
                mclass = mList[0] as ProduitType;
            }
            else
            {
                bool result = mclass.fnGetByUserName(UserName);
                mList = new List<DataPersist>();

                mList.Insert(0, mclass);
                mclass = mList[0] as ProduitType;
            }
            //= new ProduitType().fnSelect(0);

            return this.Store(mList);

        }

        public ActionResult LoadProduitTypeByAccess()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAllAccess = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            List<DataPersist> mList = null;
            ProduitType mclass = new ProduitType();

            if (HasAllAccess)
            {
                mList = new ProduitType().fnSelect(0);
                mclass = mList[0] as ProduitType;
            }
            else
            {
                bool result = mclass.fnGetByUserName(UserName);
                mList = new List<DataPersist>();

                mList.Insert(0, mclass);
                mclass = mList[0] as ProduitType;
            }
            //= new ProduitType().fnSelect(0);

            return this.Store(mList);

        }

        public ActionResult LoadSpecificProduitType()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool CanConsultOverProduitType = HasAccessFunction.fnGetUserAccessStatus("{17a9e19c-4667-4c65-b173-3749ab8c263e}", UserName);
            List<DataPersist> mList = null;
            ProduitType mclass = new ProduitType();

            bool result = mclass.fnGetByUserName(UserName);
            mList = new List<DataPersist>();

            mList.Insert(0, mclass);
            mclass = mList[0] as ProduitType;

            if (CanConsultOverProduitType)
            {
                mclass = new ProduitType();
                result = mclass.fnGetDefaultProduitType();
                mList.Insert(1, mclass);

                mclass = new ProduitType();
                mclass.ID = -1;
                mclass.Designation = "{Tous}";

                mList.Insert(0, mclass);
                mclass = mList[0] as ProduitType;
            }

            //= new ProduitType().fnSelect(0);

            return this.Store(mList);

        }


        public ActionResult SelectToPrice(string ItemPrice)
        {
            Guid id = Guid.Empty;
            if (!string.IsNullOrEmpty(ItemPrice) && (Guid.Parse(ItemPrice) != Guid.Empty))
            {
                id = Guid.Parse(ItemPrice);
                var listeProduitType = new ProduitType().fnSelectToPrice(id);
                return this.Store(listeProduitType);
            }
            else
            {
                var listAllProduitType = new ProduitType().fnSelect(0);
                return this.Store(listAllProduitType);
            }
        }

        public ActionResult SelectToPriceGood()
        {
            string UserName = (string)Session["userName"];
            var listeProduitType = new ProduitType().fnSelectAvailableForPrice(UserName);
            return this.Store(listeProduitType);

        }
        //public ActionResult SelectToPrice(string ItemProduitType)
        //{
        //    if (!string.IsNullOrEmpty(ItemProduitType))
        //    {
        //        int id = Int32.Parse(ItemProduitType);
        //        var listeProduitType = new ProduitType().fnSelectToPrice(id);
        //        return this.Store(listeProduitType);
        //    }
        //    else
        //    {
        //        var listAllProduitType = new ProduitType().fnSelect(0);
        //        return this.Store(listAllProduitType);
        //    }
        //}

        public ActionResult OnAdd()
        {


            ProduitTypeViewModel ProduitTypeVm = new ProduitTypeViewModel();

            ProduitTypeVm._ProduitType = new ProduitType();
            ProduitTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProduitType", Model = ProduitTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {


            ProduitTypeViewModel ProduitTypeVm = new ProduitTypeViewModel();

            ProduitTypeVm._ProduitType = JSON.Deserialize<ProduitType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ProduitTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProduitType", Model = ProduitTypeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {


            ProduitTypeViewModel ProduitTypeVm = new ProduitTypeViewModel();

            ProduitTypeVm._ProduitType = JSON.Deserialize<ProduitType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ProduitTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProduitType", Model = ProduitTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                ProduitType ProduitType = new ProduitType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    ProduitType.IsNew = true;
                else
                {
                    ProduitType.IsNew = false;

                    ProduitType.fnGet(int.Parse(GetFormValue("TxtProduitTypeID")));

                    if (ProduitType == null || ProduitType.ID == 0)
                        throw new Exception("SubmitFormMethod : Location, load failed.");
                }

                ProduitType = MapFormToObject(ProduitType);

                bool result = ProduitType.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeProduitType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, ProduitType);
                        X.GetCmp<RowSelectionModel>("rowSelectionProduitType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(ProduitType.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(ProduitType);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormProduitType").Close();
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
            var liste = new ProduitType().fnSelect(status);
            //var pagning = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                ProduitType ProduitType = JSON.Deserialize<ProduitType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = ProduitType.fnGet(ProduitType.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Location, loading failed.");
                //(string)Session["userName"];
                ProduitType.UtilisateurModification = (string)Session["userName"];

                if (ProduitType.Desactive)
                    result = ProduitType.fnActivate();
                else
                    result = ProduitType.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Location, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeProduitType");

                    ModelProxy mProxy = mstore.GetById(ProduitType.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(ProduitType);

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
            FormPanel mform = X.GetCmp<FormPanel>("ProduitTypeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeProduitType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ProduitTypeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private ProduitType MapFormToObject(ProduitType ProduitType)
        {
            ProduitType.Designation = X.GetCmp<TextField>("TxtDesignation").Text;

            ProduitType.ProduitFini = null;
            if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbDefautProduitFini").Text))
            {
                ProduitType.ProduitFini = new ProduitFini();
                ProduitType.ProduitFini.ID = int.Parse(X.GetCmp<ComboBox>("CmbDefautProduitFini").SelectedItem.Value);
                ProduitType.ProduitFini.Designation = X.GetCmp<ComboBox>("CmbDefautProduitFini").SelectedItem.Text;
            }

           
            //(string)Session["userName"]
            ProduitType.UtilisateurCreation = (string)Session["userName"];
            ProduitType.UtilisateurModification = (string)Session["userName"];

            return ProduitType;
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
            X.GetCmp<RowSelectionModel>("rowSelectionProduitType").DeselectAll();
        }


        #endregion

    }
}