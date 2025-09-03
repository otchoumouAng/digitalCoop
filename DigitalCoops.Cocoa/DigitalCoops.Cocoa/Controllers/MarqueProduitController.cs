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
    public class MarqueProduitController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: MarqueProduit
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{4277b97a-c503-4937-9f90-8c074c32f9c9}", UserName) == false)
                X.GetCmp<Button>("btnNewMarqueProduit").Disable();
            else
                X.GetCmp<Button>("btnNewMarqueProduit").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{b0d14e65-ed49-4fa7-8e44-3fd377e09363}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListMarqueProduit").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListMarqueProduit").Enable();

            X.GetCmp<Hidden>("MarqueProduithiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{4277b97a-c503-4937-9f90-8c074c32f9c9}", UserName));
            X.GetCmp<Hidden>("MarqueProduithiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{b0d14e65-ed49-4fa7-8e44-3fd377e09363}", UserName));
            X.GetCmp<Hidden>("MarqueProduithiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{d98be65d-710d-4d87-9979-9f6f097ec0e1}", UserName));
            X.GetCmp<Hidden>("MarqueProduithiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{51e3127e-94e7-4ee7-ab74-33a153cf38f4}", UserName));
            X.GetCmp<Hidden>("MarqueProduithiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{d0ee7d23-359f-4c43-bb1c-12b697c5dc87}", UserName));
            X.GetCmp<Hidden>("MarqueProduithiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{4b25cda5-1016-4bc0-8f9b-581c376d7b68}", UserName));

            return View();
        }

        public ActionResult LoadMarqueProduit()
        {
            List<DataPersist> mList = new MarqueProduit().fnSelect(0);

            MarqueProduit mclass = new MarqueProduit();

            if (mList.Count > 0)
                mclass = mList[0] as MarqueProduit;

            return this.Store(mList);
        }


        public ActionResult LoadMarqueProduitAll()
        {
            List<DataPersist> mList = new MarqueProduit().fnSelect(0);

            MarqueProduit mclass = new MarqueProduit();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as MarqueProduit;

            return this.Store(mList);
        }

        public ActionResult LoadAllMarqueProduitByAccess()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAllAccess = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            List<DataPersist> mList = null;
            MarqueProduit mclass = new MarqueProduit();

            if (HasAllAccess)
            {
                mList = new MarqueProduit().fnSelect(0);

                mclass.ID = -1;
                mclass.Designation = "{Tous}";

                mList.Insert(0, mclass);
                mclass = mList[0] as MarqueProduit;
            }
            else
            {
                bool result = false;// mclass.fnGetByUserName(UserName);
                mList = new List<DataPersist>();

                mList.Insert(0, mclass);
                mclass = mList[0] as MarqueProduit;
            }
            //= new MarqueProduit().fnSelect(0);

            return this.Store(mList);

        }

        public ActionResult LoadMarqueProduitByAccess()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool HasAllAccess = HasAccessFunction.fnGetUserAccessStatus("{5cc0d4cd-53b5-4910-af30-face52c5f12d}", UserName);
            List<DataPersist> mList = null;
            MarqueProduit mclass = new MarqueProduit();

            if (HasAllAccess)
            {
                mList = new MarqueProduit().fnSelect(0);
                mclass = mList[0] as MarqueProduit;
            }
            else
            {
                //bool result = mclass.fnGetByUserName(UserName);
                mList = new List<DataPersist>();

                mList.Insert(0, mclass);
                mclass = mList[0] as MarqueProduit;
            }
            //= new MarqueProduit().fnSelect(0);

            return this.Store(mList);

        }

        public ActionResult LoadSpecificMarqueProduit()
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccessFunction = new Fonction();
            bool CanConsultOverMarqueProduit = HasAccessFunction.fnGetUserAccessStatus("{17a9e19c-4667-4c65-b173-3749ab8c263e}", UserName);
            List<DataPersist> mList = null;
            MarqueProduit mclass = new MarqueProduit();

            bool result = false;// mclass.fnGetByUserName(UserName);
            mList = new List<DataPersist>();

            mList.Insert(0, mclass);
            mclass = mList[0] as MarqueProduit;

            if (CanConsultOverMarqueProduit)
            {
                mclass = new MarqueProduit();
                result = false;//mclass.fnGetDefaultMarqueProduit();
                mList.Insert(1, mclass);

                mclass = new MarqueProduit();
                mclass.ID = -1;
                mclass.Designation = "{Tous}";

                mList.Insert(0, mclass);
                mclass = mList[0] as MarqueProduit;
            }

            //= new MarqueProduit().fnSelect(0);

            return this.Store(mList);

        }



        //public ActionResult SelectToPriceGood()
        //{
        //    string UserName = (string)Session["userName"];
        //    var listeMarqueProduit = new MarqueProduit().fnSelectAvailableForPrice(UserName);
        //    return this.Store(listeMarqueProduit);

        //}
        //public ActionResult SelectToPrice(string ItemMarqueProduit)
        //{
        //    if (!string.IsNullOrEmpty(ItemMarqueProduit))
        //    {
        //        int id = Int32.Parse(ItemMarqueProduit);
        //        var listeMarqueProduit = new MarqueProduit().fnSelectToPrice(id);
        //        return this.Store(listeMarqueProduit);
        //    }
        //    else
        //    {
        //        var listAllMarqueProduit = new MarqueProduit().fnSelect(0);
        //        return this.Store(listAllMarqueProduit);
        //    }
        //}

        public ActionResult OnAdd()
        {


            MarqueProduitViewModel MarqueProduitVm = new MarqueProduitViewModel();

            MarqueProduitVm._MarqueProduit = new MarqueProduit();
            MarqueProduitVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMarqueProduit", Model = MarqueProduitVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {


            MarqueProduitViewModel MarqueProduitVm = new MarqueProduitViewModel();

            MarqueProduitVm._MarqueProduit = JSON.Deserialize<MarqueProduit>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            MarqueProduitVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMarqueProduit", Model = MarqueProduitVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {


            MarqueProduitViewModel MarqueProduitVm = new MarqueProduitViewModel();

            MarqueProduitVm._MarqueProduit = JSON.Deserialize<MarqueProduit>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            MarqueProduitVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMarqueProduit", Model = MarqueProduitVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                MarqueProduit MarqueProduit = new MarqueProduit();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    MarqueProduit.IsNew = true;
                else
                {
                    MarqueProduit.IsNew = false;

                    //var idStr = GetFormValue("TxtMarqueProduitID");
                    //System.Diagnostics.Debug.WriteLine($"Valeur reçue pour TxtMarqueProduitID : '{idStr}'");

                    MarqueProduit.fnGet(int.Parse(GetFormValue("TxtMarqueProduitID")));

                    if (MarqueProduit == null || MarqueProduit.ID == 0)
                        throw new Exception("SubmitFormMethod : Location, load failed.");
                }

                MarqueProduit = MapFormToObject(MarqueProduit);

                bool result = MarqueProduit.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeMarqueProduit");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, MarqueProduit);
                        X.GetCmp<RowSelectionModel>("rowSelectionMarqueProduit").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(MarqueProduit.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(MarqueProduit);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormMarqueProduit").Close();
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
            var liste = new MarqueProduit().fnSelect(status);
            //var pagning = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                MarqueProduit MarqueProduit = JSON.Deserialize<MarqueProduit>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = MarqueProduit.fnGet(MarqueProduit.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Location, loading failed.");
                //(string)Session["userName"];
                MarqueProduit.UtilisateurModification = (string)Session["userName"];

                if (MarqueProduit.Desactive)
                    result = MarqueProduit.fnActivate();
                else
                    result = MarqueProduit.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Location, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeMarqueProduit");

                    ModelProxy mProxy = mstore.GetById(MarqueProduit.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(MarqueProduit);

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
            FormPanel mform = X.GetCmp<FormPanel>("MarqueProduitCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeMarqueProduit");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("MarqueProduitCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private MarqueProduit MapFormToObject(MarqueProduit MarqueProduit)
        {

            MarqueProduit.Designation = X.GetCmp<TextField>("TxtDesignation").Text;
            MarqueProduit.ProduitDeBase = X.GetCmp<TextField>("TxtProduitDeBase").Text;
            MarqueProduit.ProduitDerive = X.GetCmp<Checkbox>("ChkProduitDerive").Checked;

            //MarqueProduit.Provenance = null;
            //if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbDefautProvenance").Text))
            //{
            //    MarqueProduit.Provenance = new Provenance();
            //    MarqueProduit.Provenance.ID = int.Parse(X.GetCmp<ComboBox>("CmbDefautProvenance").SelectedItem.Value);
            //    MarqueProduit.Provenance.Designation = X.GetCmp<ComboBox>("CmbDefautProvenance").SelectedItem.Text;
            //}

            //MarqueProduit.Destination = null;
            //if (!string.IsNullOrEmpty(X.GetCmp<ComboBox>("CmbDefautDestination").Text))
            //{
            //    MarqueProduit.Destination = new Destination();
            //    MarqueProduit.Destination.ID = int.Parse(X.GetCmp<ComboBox>("CmbDefautDestination").SelectedItem.Value);
            //    MarqueProduit.Destination.Designation = X.GetCmp<ComboBox>("CmbDefautDestination").SelectedItem.Text;
            //}
            //(string)Session["userName"]
            MarqueProduit.UtilisateurCreation = (string)Session["userName"];
            MarqueProduit.UtilisateurModification = (string)Session["userName"];

            return MarqueProduit;
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
            X.GetCmp<RowSelectionModel>("rowSelectionMarqueProduit").DeselectAll();
        }


        #endregion

    }
}