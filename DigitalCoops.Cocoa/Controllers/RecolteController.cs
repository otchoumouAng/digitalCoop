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
    public class RecolteController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: Site
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{8B790829-40CB-418A-98B7-4A96DDF72C4F}", UserName) == false)
                X.GetCmp<Button>("btnNewRecolte").Disable();
            else
                X.GetCmp<Button>("btnNewRecolte").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{42E56F1B-8C2C-4670-87A3-D8942A5216D3}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListRecolte").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListRecolte").Enable();

            X.GetCmp<Hidden>("RchiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{8B790829-40CB-418A-98B7-4A96DDF72C4F}", UserName));
            X.GetCmp<Hidden>("RchiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{5AD2B4B6-EB6B-474F-9734-23F273EBA6C1}", UserName));
            X.GetCmp<Hidden>("RchiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{304CA114-8492-4E4E-8294-1B6241F9F862}", UserName));
            X.GetCmp<Hidden>("RchiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{499593DD-18DD-4354-8B53-9F4ACA9F58E0}", UserName));
            X.GetCmp<Hidden>("RchiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{42E56F1B-8C2C-4670-87A3-D8942A5216D3}", UserName));
            X.GetCmp<Hidden>("RchiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{7D0B1E8B-5117-43E6-A3E4-9B70AB9EA772}", UserName));

            return View();
        }

        public ActionResult LoadRecolte()
        {
            List<DataPersist> mList = new Recolte().fnSelect(0);

            Recolte mclass = new Recolte();

            if (mList.Count > 0)
                mclass = mList[0] as Recolte;

            return this.Store(mList);
        }


        public ActionResult LoadRecolteAll()
        {
            List<DataPersist> mList = new Recolte().fnSelect(0);

            Recolte mclass = new Recolte();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as Recolte;

            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();
            
            RecolteViewModel recolteVm = new RecolteViewModel();

            recolteVm._Recolte = new Recolte();
            recolteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRecolte", Model = recolteVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            RecolteViewModel recolteVm = new RecolteViewModel();

            recolteVm._Recolte = JSON.Deserialize<Recolte>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            recolteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRecolte", Model = recolteVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            RecolteViewModel recolteVm = new RecolteViewModel();

            recolteVm._Recolte = JSON.Deserialize<Recolte>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            recolteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormRecolte", Model = recolteVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Recolte Recolte = new Recolte();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    Recolte.IsNew = true;
                else
                {
                    Recolte.IsNew = false;

                    Recolte.fnGet(int.Parse(GetFormValue("TxtRecolteID")));

                    if (Recolte == null || Recolte.ID == 0)
                        throw new Exception("SubmitFormMethod : Crop Quality load failed.");
                }

                Recolte = MapFormToObject(Recolte);

                bool result = Recolte.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeRecolte");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, Recolte);
                        X.GetCmp<RowSelectionModel>("rowSelectionRecolte").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(Recolte.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(Recolte);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormRecolte").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Crop Quality : Data Validation",
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
            var listeRecolte = new Recolte().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, listeRecolte);

            return this.Store(listeRecolte);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Recolte Recolte = JSON.Deserialize<Recolte>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = Recolte.fnGet(Recolte.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Crop Quality loading failed.");

                Recolte.UtilisateurModification = (string)Session["userName"];

                if (Recolte.Desactive)
                    result = Recolte.fnActivate();
                else
                    result = Recolte.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Crop Quality, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeRecolte");

                    ModelProxy mProxy = mstore.GetById(Recolte.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(Recolte);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Crop Quality : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("RecolteCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeRecolte");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("RecolteCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Recolte MapFormToObject(Recolte Recolte)
        {
            Recolte.Designation = X.GetCmp<TextField>("TxtDesignationRecolte").Text;
            Recolte.Abreviation = X.GetCmp<TextField>("TxtAbreviationRecolte").Text;

            Recolte.UtilisateurCreation = (string)Session["userName"];
            Recolte.UtilisateurModification = (string)Session["userName"];

            return Recolte;
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
            X.GetCmp<RowSelectionModel>("rowSelectionRecolte").DeselectAll();
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