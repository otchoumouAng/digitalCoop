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
using Tms2017.MVC.Models;

namespace Tms2017.MVC.Controllers
{
    public class DestinationTypeController : BaseController
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

            if (HasAccessFunction.fnGetUserAccessStatus("{3D8BD9DF-CE1D-4A5E-B895-7A0E0C4A0E5E}", UserName) == false)
                X.GetCmp<Button>("btnNewDestinationType").Disable();
            else
                X.GetCmp<Button>("btnNewDestinationType").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{88F044E6-956C-4FD1-A79F-A39E040E9D98}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListDsType").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListDsType").Enable();

            X.GetCmp<Hidden>("DstphiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{3D8BD9DF-CE1D-4A5E-B895-7A0E0C4A0E5E}", UserName));
            X.GetCmp<Hidden>("DstphiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{426064C7-0B0C-452D-9731-3A463901130F}", UserName));
            X.GetCmp<Hidden>("DstphiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{F3B9DC22-2477-4CB7-BD4D-5CF4EAA7C226}", UserName));
            X.GetCmp<Hidden>("DstphiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{036A4498-91BE-466D-9EAC-1BEC7E9B4E4A}", UserName));
            X.GetCmp<Hidden>("DstphiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{88F044E6-956C-4FD1-A79F-A39E040E9D98}", UserName));
            X.GetCmp<Hidden>("DstphiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{A8A9B1FB-7E25-40CC-9E40-663C6B2FA250}", UserName));

            return View();
        }

        public ActionResult LoadDestinationType()
        {
            List<DataPersist> mList = new DestinationType().fnSelect();

            DestinationType mclass = new DestinationType();

            if (mList.Count > 0)
                mclass = mList[0] as DestinationType;

            return this.Store(mList);
        }
        
        public ActionResult LoadDestinationByLivraisonType(int? TypeLivraison)
        {
            LivraisonType mTypeLivraison = new LivraisonType();

            List<DataPersist> mList;
            mList = new List<DataPersist>();
            try
            {
                if (!TypeLivraison.HasValue)
                    return this.Store(mList);


                bool result = mTypeLivraison.fnGet(TypeLivraison);

                if (!result || (mTypeLivraison.ID == 0))
                    throw new Exception(this.GetType().FullName + " : LoadDestinationByLivraisonType : failed load TypeLivraison.");

                // Si sens du type de livraison est sortie alors 
                // Provenance est site par defaut sinon 
                if (mTypeLivraison.Sens.Equals(LivraisonType.ENTREE))
                {
                    // selectionner le site par defaut comme Provenance
                    Destination mClass = new Destination();

                    result = mClass.fnGetDefaultSite();

                    if (!result || (mClass.ID == 0))
                        throw new Exception(this.GetType().FullName + " : LoadDestinationByLivraisonType : failed load default site.");

                    mList.Add(mClass);                            

                }
                else
                {
                    mList = new Destination().fnSelect(0, mTypeLivraison.DestinationType.ID);
                }


                Destination mclass = new Destination();

                if (mList.Count > 0)
                    mclass = mList[0] as Destination;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Store(mList);
        }


        public ActionResult LoadDestinationTypeAll()
        {
            List<DataPersist> mList = new DestinationType().fnSelect();

            DestinationType mclass = new DestinationType();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as DestinationType;

            return this.Store(mList);
        }

        public ActionResult LoadDestinationTypeWithBlank()
        {
            List<DataPersist> mList = new DestinationType().fnSelect(0);

            DestinationType mclass = new DestinationType();

            mclass.ID = -1;
            mclass.Designation = "";

            mList.Insert(0, mclass);
            mclass = mList[0] as DestinationType;

            return this.Store(mList);
        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            DestinationTypeViewModel DestinationTypeVm = new DestinationTypeViewModel();

            DestinationTypeVm._DestinationType = new DestinationType();
            DestinationTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDestinationType", Model = DestinationTypeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            DestinationTypeViewModel DestinationTypeVm = new DestinationTypeViewModel();

            DestinationTypeVm._DestinationType = JSON.Deserialize<DestinationType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            DestinationTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDestinationType", Model = DestinationTypeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            DestinationTypeViewModel DestinationTypeVm = new DestinationTypeViewModel();

            DestinationTypeVm._DestinationType = JSON.Deserialize<DestinationType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            DestinationTypeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDestinationType", Model = DestinationTypeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                DestinationType destinationType = new DestinationType();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    destinationType.IsNew = true;
                else
                {
                    destinationType.IsNew = false;

                    destinationType.fnGet(int.Parse(GetFormValue("TxtDestinationTypeID")));

                    if (destinationType == null || destinationType.ID == 0)
                        throw new Exception("SubmitFormMethod : Destination Type, load failed.");
                }

                destinationType = MapFormToObject(destinationType);

                bool result = destinationType.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeDestinationType");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, destinationType);
                        X.GetCmp<RowSelectionModel>("rowSelectionDestinationType").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(destinationType.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(destinationType);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormDestinationType").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Destination Type: Data Validation",
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
            var liste = new DestinationType().fnSelect(status);
            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                DestinationType destinationType = JSON.Deserialize<DestinationType>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = destinationType.fnGet(destinationType.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Destination type, loading failed.");

                destinationType.UtilisateurModification = (string)Session["userName"];

                if (destinationType.Desactive)
                    result = destinationType.fnActivate();
                else
                    result = destinationType.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Destination type, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeDestinationType");

                    ModelProxy mProxy = mstore.GetById(destinationType.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(destinationType);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Destination Type : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("DestinationTypeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListeDestinationType");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("DestinationTypeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private DestinationType MapFormToObject(DestinationType destinationtype)
        {
            destinationtype.Designation = X.GetCmp<TextField>("TxtDesignationType").Text;

            //(string)Session["userName"];
            destinationtype.UtilisateurCreation = (string)Session["userName"];
            destinationtype.UtilisateurModification = (string)Session["userName"];

            return destinationtype;
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
            X.GetCmp<RowSelectionModel>("rowSelectionDestinationType").DeselectAll();
        }

        
        #endregion

    }
}