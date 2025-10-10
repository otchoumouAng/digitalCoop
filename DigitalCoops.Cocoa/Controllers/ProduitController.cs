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
    public class ProduitController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";
        
        // GET: Produit
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{45B4EEBD-858A-41F1-BB91-1E41FBD0EB5B}", UserName) == false)
                X.GetCmp<Button>("btnNewProduit").Disable();
            else
                X.GetCmp<Button>("btnNewProduit").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{EA139DA1-6D10-409A-BD2C-644A0339AE7D}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportProductList").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportProductList").Enable();

            X.GetCmp<Hidden>("PrdhiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{45B4EEBD-858A-41F1-BB91-1E41FBD0EB5B}", UserName));
            X.GetCmp<Hidden>("PrdhiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{CAE7F257-A629-4A95-A728-3A2095114BF2}", UserName));
            X.GetCmp<Hidden>("PrdhiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{E56BC7C3-FFD5-49C1-AA7E-019BC17768D4}", UserName));
            X.GetCmp<Hidden>("PrdhiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{750B3DB8-37F5-4D58-8E42-79C43579CC1B}", UserName));
            X.GetCmp<Hidden>("PrdhiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{EA139DA1-6D10-409A-BD2C-644A0339AE7D}", UserName));
            X.GetCmp<Hidden>("PrdhiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{A0AB2B33-0D02-448B-A5D2-0C36B07975ED}", UserName));

            return View();
        }

        public ActionResult LoadProduitWithBlank()
        {
            List<DataPersist> mList = new Produit().fnSelect(0);

            Produit mclass = new Produit();

            mclass.ID = -1;
            mclass.Designation = "";

            mList.Insert(0, mclass);
            mclass = mList[0] as Produit;

            return this.Store(mList);
        }
        public ActionResult LoadProduit()
        {
            List<DataPersist> mList = new Produit().fnSelect(0);

            Produit mclass = new Produit();

            return this.Store(mList);
        }

        public ActionResult LoadProduitAll()
        {
            List<DataPersist> mList = new Produit().fnSelect(0);

            Produit mclass = new Produit();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as Produit;

            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ProduitViewModel ProduitVm = new ProduitViewModel();

            ProduitVm._Produit = new Produit();
            ProduitVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProduit", Model = ProduitVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ProduitViewModel ProduitVm = new ProduitViewModel();

            ProduitVm._Produit = JSON.Deserialize<Produit>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ProduitVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProduit", Model = ProduitVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            ProduitViewModel ProduitVm = new ProduitViewModel();

            ProduitVm._Produit = JSON.Deserialize<Produit>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ProduitVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProduit", Model = ProduitVm };

        }


        public ActionResult SubmitFormMethod()
        {

            try
            {
                Produit produit = new Produit();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    produit.IsNew = true;
                else
                {
                    produit.IsNew = false;

                    produit.fnGet(int.Parse(GetFormValue("TxtProduitID")));

                    if (produit == null || produit.ID == 0)
                        throw new Exception("SubmitFormMethod : Produit load failed.");
                }

                produit = MapFormToObject(produit);

                bool result = produit.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeProduit");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, produit);
                        X.GetCmp<RowSelectionModel>("rowSelectionProduit").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(produit.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(produit);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormProduit").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Product : Data Validation",
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
            var liste = new Produit().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);

            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Produit Produit = JSON.Deserialize<Produit>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = Produit.fnGet(Produit.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Product loading failed.");
                //(string)Session["userName"];
                Produit.UtilisateurModification = (string)Session["userName"];

                if (Produit.Desactive)
                    result = Produit.fnActivate();
                else
                    result = Produit.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Product, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeProduit");

                    ModelProxy mProxy = mstore.GetById(Produit.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(Produit);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Product : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ProduitCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeProduit");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ProduitCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Produit MapFormToObject(Produit produit)
        {
            produit.Designation = X.GetCmp<TextField>("TxtDesignationProduit").Text;
            //(string)Session["userName"]
            produit.UtilisateurCreation = (string)Session["userName"] ;
            produit.UtilisateurModification = (string)Session["userName"];

            return produit;
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
            X.GetCmp<RowSelectionModel>("rowSelectionProduit").DeselectAll();
        }

        
        #endregion

    }
}