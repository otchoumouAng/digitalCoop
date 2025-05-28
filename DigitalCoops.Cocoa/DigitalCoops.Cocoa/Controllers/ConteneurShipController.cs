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
using Tms.Classes.Shared.Sales;
using Tms.Components.Data;
using Tms2017.MVC.Models;

namespace Tms2017.MVC.Controllers
{
    public class ConteneurShipController : BaseController
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
            //Parametres mParam = new Parametres(0);
            X.GetCmp<ComboBox>("cmbCropYear").SetValue("{Tous}");

            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{74ec12f7-6c9c-4e24-a100-35b705b95d9d}", UserName) == false)
                X.GetCmp<Button>("btnNewConteneurShip").Disable();
            else
                X.GetCmp<Button>("btnNewConteneurShip").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{5651caf3-f066-46b2-bd0f-0025cc4583e0}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListConteneurShip").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListConteneurShip").Enable();

            //X.GetCmp<Hidden>("DsthiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{7B0E442E-37DF-4273-A1DE-EF4AC4007F2F}", UserName));
            X.GetCmp<Hidden>("DsthiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{3607c6e0-bdd8-47c1-8d70-addd5c3b3588}", UserName));
            X.GetCmp<Hidden>("DsthiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{66bcb27a-4a4f-4362-9522-619b623b5f60}", UserName));
            X.GetCmp<Hidden>("DsthiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{2b989621-dea1-4b62-b9d6-7423e8d29f1e}", UserName));
            //X.GetCmp<Hidden>("DsthiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{4FBC3A10-6C04-44FC-A3C3-1AABBFE1D014}", UserName));
            //X.GetCmp<Hidden>("DsthiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{621AE741-044A-4BA0-A5B7-495DB1882A0E}", UserName));

            return View();
        }

        public ActionResult LoadConteneurShip()
        {
            List<DataPersist> mList = new ConteneurShip().fnSelect(0, -1,"{Tous}",-1);

            ConteneurShip mclass = new ConteneurShip();

            if (mList.Count > 0)
                mclass = mList[0] as ConteneurShip;

            return this.Store(mList);
        }
        
        public ActionResult LoadConteneurShipByType(int? TypeC, int? siteID = 1)
        {
            ConteneurType mType = new ConteneurType();

            List<DataPersist> mList;
            mList = new List<DataPersist>();
            try
            {
                if (!TypeC.HasValue)
                    return this.Store(mList);


                bool result = mType.fnGet(TypeC);

                if (!result || (mType.ID == 0))
                    throw new Exception(this.GetType().FullName + " : LoadConteneurShipByLivraisonType : failed load TypeLivraison.");

                string UserName = (string)Session["userName"];
                Parametres mParam = new Parametres(0);
                mList = new ConteneurShip().fnSelect(0, mType.ID, "{Tous}", -1);


                ConteneurShip mclass = new ConteneurShip();

                if (mList.Count > 0)
                    mclass = mList[0] as ConteneurShip;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Store(mList);
        }


        public ActionResult LoadConteneurShipAll()
        {
            List<DataPersist> mList = new ConteneurShip().fnSelect();

            ConteneurShip mclass = new ConteneurShip();

            mclass.ID = -1;
            mclass.Libelle = "{Tous}";

            mList.Insert(0, mclass);
            mclass = mList[0] as ConteneurShip;

            return this.Store(mList);
        }        

        public ActionResult OnAdd()
        {            

            ConteneurShipViewModel mConteneurShipVm = new ConteneurShipViewModel();
            Parametres mParam = new Parametres(0);
            mConteneurShipVm._ConteneurShip = new ConteneurShip();
            mConteneurShipVm._ConteneurShip.Campagne = new Campagne();
            mConteneurShipVm._ConteneurShip.Campagne.Designation = mParam.Campagne;
            mConteneurShipVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConteneurShip", Model = mConteneurShipVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {            

            ConteneurShipViewModel mConteneurShipVm = new ConteneurShipViewModel();

            mConteneurShipVm._ConteneurShip = JSON.Deserialize<ConteneurShip>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            mConteneurShipVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConteneurShip", Model = mConteneurShipVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {            

            ConteneurShipViewModel ConteneurShipVm = new ConteneurShipViewModel();

            ConteneurShipVm._ConteneurShip = JSON.Deserialize<ConteneurShip>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            ConteneurShipVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormConteneurShip", Model = ConteneurShipVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                ConteneurShip mConteneurShip = new ConteneurShip();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mConteneurShip.IsNew = true;
                else
                {
                    mConteneurShip.IsNew = false;

                    mConteneurShip.fnGet(int.Parse(GetFormValue("TxtConteneurShipID")));

                    if (mConteneurShip == null || mConteneurShip.ID == 0)
                        throw new Exception("SubmitFormMethod : ConteneurShip load failed.");
                }

                mConteneurShip = MapFormToObject(mConteneurShip);

                bool result = mConteneurShip.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeConteneurShip");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mConteneurShip);
                        X.GetCmp<RowSelectionModel>("rowSelectionConteneurShip").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mConteneurShip.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mConteneurShip);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormConteneurShip").Close();                   
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "ConteneurShip : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemStatus, string ItemType, string ItemCampagne, string ItemCompagnie)
        {
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
            int mCompagnie = GetCriteriaValue(ItemCompagnie);
            int status = -1;
            if (string.IsNullOrEmpty(ItemStatus))
            {
                status = -1;
            }
            else if (ItemStatus == "true")
            {
                status = 0;
            }

            int typeID = GetCritriaValue(ItemType);
            var liste = new ConteneurShip().fnSelect(status, typeID, Campagne, mCompagnie);

            //var paging = GridStorePaging.SetRangePlants(parameters, liste);
            return this.Store(liste);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                ConteneurShip mConteneurShip = JSON.Deserialize<ConteneurShip>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = mConteneurShip.fnGet(mConteneurShip.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : ConteneurShip loading failed.");

                mConteneurShip.UtilisateurModification = (string)Session["userName"];

                if (mConteneurShip.Desactive)
                    result = mConteneurShip.fnActivate();
                else
                    result = mConteneurShip.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : ConteneurShip, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeConteneurShip");

                    ModelProxy mProxy = mstore.GetById(mConteneurShip.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mConteneurShip);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "ConteneurShip : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ConteneurShipCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus, string ItemType, string ItemCampagne, string ItemCompagnie)
        {
            Store mstore = X.GetCmp<Store>("storeListeConteneurShip");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus),
                                new Ext.Net.Parameter("ItemType", ItemType),
                                new Ext.Net.Parameter("ItemCampagne", ItemCampagne),
                                new Ext.Net.Parameter("ItemCompagnie", ItemCompagnie),
                            });
            FormPanel mform = X.GetCmp<FormPanel>("ConteneurShipCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private ConteneurShip MapFormToObject(ConteneurShip mConteneurShip)
        {
            mConteneurShip.Libelle = X.GetCmp<TextField>("TxtDesignation").Text;
            mConteneurShip.ConteneurType = new ConteneurType();
            mConteneurShip.ConteneurType.ID = int.Parse(X.GetCmp<ComboBox>("CmbConteneurType").SelectedItem.Value);
            mConteneurShip.ConteneurType.Designation = X.GetCmp<ComboBox>("CmbConteneurType").SelectedItem.Text;
            mConteneurShip.Campagne = new Campagne();
            mConteneurShip.Campagne.Designation = X.GetCmp<ComboBox>("_cmbCampagne").SelectedItem.Text;

            mConteneurShip.CompagnieMaritime = new CompagnieMaritime();
            mConteneurShip.CompagnieMaritime.ID = int.Parse(X.GetCmp<ComboBox>("_cmbShippingLine").SelectedItem.Value);
            mConteneurShip.CompagnieMaritime.Nom = X.GetCmp<ComboBox>("_cmbShippingLine").SelectedItem.Text;

            mConteneurShip.UtilisateurCreation = (string)Session["userName"];
            mConteneurShip.UtilisateurModification = (string)Session["userName"];

            return mConteneurShip;
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
            X.GetCmp<RowSelectionModel>("rowSelectionConteneurShip").DeselectAll();
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

        private int GetCriteriaValue(string strComponent)
        {
            int value;

            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
                return value;
            else
                return -1;
        }
    }
}