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
    public class DestinationController : BaseController
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

            if (HasAccessFunction.fnGetUserAccessStatus("{7B0E442E-37DF-4273-A1DE-EF4AC4007F2F}", UserName) == false)
                X.GetCmp<Button>("btnNewDestination").Disable();
            else
                X.GetCmp<Button>("btnNewDestination").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{4FBC3A10-6C04-44FC-A3C3-1AABBFE1D014}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListDestination").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListDestination").Enable();

            X.GetCmp<Hidden>("DsthiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{7B0E442E-37DF-4273-A1DE-EF4AC4007F2F}", UserName));
            X.GetCmp<Hidden>("DsthiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{92EDD7D2-7190-4A4D-9FEB-4F7DC763BF51}", UserName));
            X.GetCmp<Hidden>("DsthiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{06A95C39-5763-473E-B23E-CB8D1BD3CBE9}", UserName));
            X.GetCmp<Hidden>("DsthiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{875D5CEC-390E-4B32-B16F-09E0E8E8D37B}", UserName));
            X.GetCmp<Hidden>("DsthiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{4FBC3A10-6C04-44FC-A3C3-1AABBFE1D014}", UserName));
            X.GetCmp<Hidden>("DsthiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{621AE741-044A-4BA0-A5B7-495DB1882A0E}", UserName));

            return View();
        }

        public ActionResult LoadDestination()
        {
            List<DataPersist> mList = new Destination().fnSelect();

            Destination mclass = new Destination();

            if (mList.Count > 0)
                mclass = mList[0] as Destination;

            return this.Store(mList);
        }
        
        public ActionResult LoadDestinationByLivraisonType(int? TypeLivraison, int? siteID = 1)
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

                string UserName = (string)Session["userName"];
                Parametres mParam = new Parametres(0);
                
                // Si sens du type de livraison est sortie alors 
                // Provenance est site par defaut sinon 
                if (mTypeLivraison.Sens.Equals(LivraisonType.ENTREE))
                {
                    // selectionner le site par defaut comme Provenance
                    Destination mClass = new Destination();
                    if (siteID == mParam.Site)
                        result = mClass.fnGetDefaultSite();
                    else
                        result = mClass.fnGetBySite(siteID);

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

        public ActionResult LoadDestinationBySite(int? siteID = 1)
        {
            LivraisonType mTypeLivraison = new LivraisonType();

            List<DataPersist> mList;
            mList = new List<DataPersist>();
            try
            {
                if (!siteID.HasValue)
                    return this.Store(mList);                

                string UserName = (string)Session["userName"];
                Parametres mParam = new Parametres(0);
                bool result = false;
                 
                Destination mClass = new Destination();
                if (siteID == mParam.Site)
                    result = mClass.fnGetDefaultSite();
                else
                    result = mClass.fnGetBySite(siteID);

                if (!result || (mClass.ID == 0))
                    throw new Exception(this.GetType().FullName + " : LoadDestinationByLivraisonType : failed load default site.");

                mList.Add(mClass);                

                if (mList.Count > 0)
                    mClass = mList[0] as Destination;
            }                           
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Store(mList);
        }

        public ActionResult LoadDestinationAll()
        {
            List<DataPersist> mList = new Destination().fnSelect();

            Destination mclass = new Destination();

            mclass.ID = -1;
            mclass.Nom = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as Destination;

            return this.Store(mList);
        }        

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            DestinationViewModel DestinationVm = new DestinationViewModel();

            DestinationVm._Destination = new Destination();
            DestinationVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDestination", Model = DestinationVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            DestinationViewModel DestinationVm = new DestinationViewModel();

            DestinationVm._Destination = JSON.Deserialize<Destination>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            DestinationVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDestination", Model = DestinationVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            DestinationViewModel DestinationVm = new DestinationViewModel();

            DestinationVm._Destination = JSON.Deserialize<Destination>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            DestinationVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDestination", Model = DestinationVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Destination destination = new Destination();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    destination.IsNew = true;
                else
                {
                    destination.IsNew = false;

                    destination.fnGet(int.Parse(GetFormValue("TxtDestinationID")));

                    if (destination == null || destination.ID == 0)
                        throw new Exception("SubmitFormMethod : Destination load failed.");
                }

                destination = MapFormToObject(destination);

                bool result = destination.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeDestination");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, destination);
                        X.GetCmp<RowSelectionModel>("rowSelectionDestination").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(destination.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(destination);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormDestination").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Destination : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemStatus, string ItemDestinationType)
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

            int typeID = GetCritriaValue(ItemDestinationType);
            var liste = new Destination().fnSelect(status, typeID);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Destination destination = JSON.Deserialize<Destination>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = destination.fnGet(destination.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Destination loading failed.");

                destination.UtilisateurModification = (string)Session["userName"];

                if (destination.Desactive)
                    result = destination.fnActivate();
                else
                    result = destination.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Destination, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeDestination");

                    ModelProxy mProxy = mstore.GetById(destination.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(destination);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Destination : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("DestinationCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus, string ItemDestinationType)
        {
            Store mstore = X.GetCmp<Store>("storeListeDestination");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus),
                                new Ext.Net.Parameter("ItemDestinationType", ItemDestinationType),
                            });
            FormPanel mform = X.GetCmp<FormPanel>("DestinationCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Destination MapFormToObject(Destination destination)
        {
            destination.Nom = X.GetCmp<TextField>("TxtDesignationDestination").Text;
            destination.DestinationType = new DestinationType();
            destination.DestinationType.ID = int.Parse(X.GetCmp<ComboBox>("CmbDestinationType").SelectedItem.Value);
            destination.DestinationType.Designation = X.GetCmp<ComboBox>("CmbDestinationType").SelectedItem.Text;

            destination.UtilisateurCreation = (string)Session["userName"];
            destination.UtilisateurModification = (string)Session["userName"];

            return destination;
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
            X.GetCmp<RowSelectionModel>("rowSelectionDestination").DeselectAll();
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