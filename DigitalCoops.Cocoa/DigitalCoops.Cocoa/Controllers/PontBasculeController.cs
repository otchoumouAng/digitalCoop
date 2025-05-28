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
    public class PontBasculeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";
        public ActionResult Index()
        {
            Fonction HasAccessFunction = new Fonction();
            string UserName = (string)Session["userName"];

            if (HasAccessFunction.fnGetUserAccessStatus("{2FC41DCC-AC74-48B7-8BAA-493416071D02}", UserName) == false)
                X.GetCmp<Button>("btnNewBascule").Disable();
            else
                X.GetCmp<Button>("btnNewBascule").Enable();

            if (HasAccessFunction.fnGetUserAccessStatus("{03E34293-14FF-4594-9C16-B19D2689D2D9}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListBascule").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListBascule").Enable();

            X.GetCmp<Hidden>("BschiddenPermCreer").SetValue(HasAccessFunction.fnGetUserAccessStatus("{2FC41DCC-AC74-48B7-8BAA-493416071D02}", UserName));
            X.GetCmp<Hidden>("BschiddenPermModifier").SetValue(HasAccessFunction.fnGetUserAccessStatus("{3EE6CDA1-8818-489F-A0A7-946F14F01590}", UserName));
            X.GetCmp<Hidden>("BschiddenPermDesactiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{35A724F7-E7DE-4E55-BA86-132D8D2D53FC}", UserName));
            X.GetCmp<Hidden>("BschiddenPermActiver").SetValue(HasAccessFunction.fnGetUserAccessStatus("{4A1E4DF3-2CE5-4995-BDE8-89C6F456C2E6}", UserName));
            X.GetCmp<Hidden>("BschiddenPermExporterExcel").SetValue(HasAccessFunction.fnGetUserAccessStatus("{03E34293-14FF-4594-9C16-B19D2689D2D9}", UserName));
            X.GetCmp<Hidden>("BschiddenPermOverview").SetValue(HasAccessFunction.fnGetUserAccessStatus("{4C4E0600-65E2-4042-BB15-26457BC415AF}", UserName));

            return View();
        }

        public ActionResult LoadPontBascule()
        {
            List<DataPersist> myListe = new Bascule().fnSelect(0);

            Bascule mclass = new Bascule();    

            return this.Store(myListe);

        }

        public ActionResult LoadPontBasculeAll()
        {
            List<DataPersist> myListe = new Bascule().fnSelect(0);
            Bascule mclass = new Bascule();

            mclass.ID = -1;
            mclass.Designation = "{Tous}";

            myListe.Insert(0, mclass);
            mclass = myListe[0] as Bascule;

            return this.Store(myListe);

        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            BasculeViewModel BasculeVm = new BasculeViewModel();

            BasculeVm._Bascule = new Bascule();
            BasculeVm._Parametres = new Parametres();
            BasculeVm._Parametres.fnGet();
            BasculeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPontBascule", Model = BasculeVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            BasculeViewModel BasculeVm = new BasculeViewModel();
            BasculeVm._Parametres = new Parametres();
            BasculeVm._Parametres.fnGet();
            BasculeVm._Bascule = JSON.Deserialize<Bascule>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            BasculeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPontBascule", Model = BasculeVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            BasculeViewModel BasculeVm = new BasculeViewModel();

            BasculeVm._Bascule = JSON.Deserialize<Bascule>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            BasculeVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPontBascule", Model = BasculeVm };

        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Bascule bascule = new Bascule();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    bascule.IsNew = true;
                else
                {
                    bascule.IsNew = false;

                    bascule.fnGet(int.Parse(GetFormValue("TxtBasculeID")));

                    if (bascule == null || bascule.ID == 0)
                        throw new Exception("SubmitFormMethod : Bridge load failed.");
                }

                bascule = MapFormToObject(bascule);

                bool result = bascule.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePontBascule");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, bascule);
                        X.GetCmp<RowSelectionModel>("rowSelectionPontBascule").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(bascule.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(bascule);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormPontBascule").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bridge : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemStatus)
        {
            Int16 status = -1;
            if (string.IsNullOrEmpty(ItemStatus))
            {
                status = -1;
            }
            else if (ItemStatus == "true")
            {
                status = 0;
            }
            var listeBascule = new Bascule().fnSelect(status);

            //var paging = GridStorePaging.SetRangePlants(parameters, listeBascule);

            return this.Store(listeBascule);
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Bascule bascule = JSON.Deserialize<Bascule>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = bascule.fnGet(bascule.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Bridge loading failed.");

                bascule.UtilisateurModification = (string)Session["userName"];

                if (bascule.Desactive)
                    result = bascule.fnActivate();
                else
                    result = bascule.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Bridge, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePontBascule");

                    ModelProxy mProxy = mstore.GetById(bascule.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(bascule);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bridge : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("BasculeCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListePontBascule");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemStatus", ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("BasculeCriteriaPanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        private Bascule MapFormToObject(Bascule bascule)
        {
            bascule.Site = new Site();
            bascule.Site.ID = int.Parse(X.GetCmp<TextField>("TxtDefaultSiteID").Text);

            bascule.Protocole = new Protocole();
            bascule.Protocole.ID = int.Parse(X.GetCmp<ComboBox>("CmbProtocoleID").SelectedItem.Value);
            bascule.Protocole.Designation = X.GetCmp<ComboBox>("CmbProtocoleID").SelectedItem.Text;

            bascule.Ordinateur = X.GetCmp<TextField>("TxtOrdinateurBascule").Text;
            bascule.Designation = X.GetCmp<TextField>("TxtDesignationBascule").Text;
            bascule.ComVitesse = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtComVitesseBascule").Text) ? int.Parse(X.GetCmp<NumberField>("TxtComVitesseBascule").Text) : 0;
            bascule.ComParite = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtComPariteBascule").Text) ? Int16.Parse(X.GetCmp<NumberField>("TxtComPariteBascule").Text) : (Int16)0;
            bascule.ComBitsDonnees = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtComBitsDonneesBascule").Text) ? Int16.Parse(X.GetCmp<NumberField>("TxtComBitsDonneesBascule").Text) : (Int16)0;
            bascule.ComBitsStop = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtComBitsStopBascule").Text) ? Int16.Parse(X.GetCmp<NumberField>("TxtComBitsStopBascule").Text) : (Int16)0;
            bascule.ComPort = X.GetCmp<TextField>("TxtComPortBascule").Text;
            bascule.ComTimeOut = !string.IsNullOrEmpty(X.GetCmp<NumberField>("TxtComTimeOutBascule").Text) ? int.Parse(X.GetCmp<NumberField>("TxtComTimeOutBascule").Text) : 0;

            bascule.UtilisateurCreation = (string)Session["userName"];
            bascule.UtilisateurModification = (string)Session["userName"];

            return bascule;
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
            X.GetCmp<RowSelectionModel>("rowSelectionPontBascule").DeselectAll();
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