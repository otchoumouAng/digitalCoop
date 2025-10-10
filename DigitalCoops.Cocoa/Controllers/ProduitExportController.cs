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
    public class ProduitExportController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: ProduitExport
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{B366686D-A055-4FD3-9814-FBED325F759C}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{63F85244-9B27-4E34-8D64-EDEEB0D2EE96}")))
                X.GetCmp<Button>("btnNewProduitExport").Enable();
            else
                X.GetCmp<Button>("btnNewProduitExport").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{15BD00F2-DCC8-4A62-A2C3-C339135F7A73}")))
                X.GetCmp<Button>("mnuExportListProduitExport").Enable();
            else
                X.GetCmp<Button>("mnuExportListProduitExport").Disable();

            //if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{5AD0D34B-5770-440D-92D2-52B47A94603C}")))
            //    X.GetCmp<Button>("mnuPrintListProduitExport").Enable();
            //else
            //    X.GetCmp<Button>("mnuPrintListProduitExport").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{7FACA084-F4D3-467B-82BD-10CBB1E24513}")))
                X.GetCmp<Hidden>("phiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{6689F602-706F-42BE-B888-D48F7054D881}")))
                X.GetCmp<Hidden>("phiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{6D90A4DE-2792-4B37-A610-AE10DCE8173B}")))
                X.GetCmp<Hidden>("phiddenPermActiver").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermActiver").SetValue(false);

            return View();
        }

        public ActionResult Load()
        {
            List<DataPersist> mList = new ProduitExport().fnSelect(0);
            return this.Store(mList);
        }

        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new ProduitExport().fnSelect(0);
            ProduitExport mclass = new ProduitExport();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";
            mList.Insert(0, mclass);

            return this.Store(mList);

        }

        public ActionResult OnAdd()
        {            

            ProduitExportViewModel ProduitExportVm = new ProduitExportViewModel();

            ProduitExportVm._ProduitExport = new ProduitExport();
            ProduitExportVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProduitExport", Model = ProduitExportVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {            

            ProduitExportViewModel ProduitExportVm = new ProduitExportViewModel();

            ProduitExportVm._ProduitExport = JSON.Deserialize<ProduitExport>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ProduitExportVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProduitExport", Model = ProduitExportVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {            

            ProduitExportViewModel ProduitExportVm = new ProduitExportViewModel();

            ProduitExportVm._ProduitExport = JSON.Deserialize<ProduitExport>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ProduitExportVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormProduitExport", Model = ProduitExportVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                ProduitExport ProduitExport = new ProduitExport();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    ProduitExport.IsNew = true;
                else
                {
                    ProduitExport.IsNew = false;

                    ProduitExport.fnGet(int.Parse(GetFormValue("TxtProduitExportID")));

                    if (ProduitExport == null || ProduitExport.ID == 0)
                        throw new Exception("SubmitFormMethod : Quality load failed.");
                }

                ProduitExport = MapFormToObject(ProduitExport);

                bool result = ProduitExport.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeProduitExport");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, ProduitExport);
                        X.GetCmp<RowSelectionModel>("rowSelectionProduitExport").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(ProduitExport.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(ProduitExport);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormProduitExport").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Quality : Data Validation",
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
            var liste = new ProduitExport().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                ProduitExport ProduitExport = JSON.Deserialize<ProduitExport>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = ProduitExport.fnGet(ProduitExport.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Quality loading failed.");
                //(string)Session["userName"];
                ProduitExport.UtilisateurModification = (string)Session["userName"];

                if (ProduitExport.Desactive)
                    result = ProduitExport.fnActivate();
                else
                    result = ProduitExport.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Quality, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeProduitExport");

                    ModelProxy mProxy = mstore.GetById(ProduitExport.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(ProduitExport);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Quality : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ProduitExportCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeProduitExport");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ProduitExportCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private ProduitExport MapFormToObject(ProduitExport ProduitExport)
        {
            ProduitExport.Designation = X.GetCmp<TextField>("TxtDesignationProduitExport").Text;
            //(string)Session["userName"]
            ProduitExport.UtilisateurCreation = (string)Session["userName"];
            ProduitExport.UtilisateurModification = (string)Session["userName"];

            return ProduitExport;
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
            X.GetCmp<RowSelectionModel>("rowSelectionProduitExport").DeselectAll();
        }


        #endregion
    }
}