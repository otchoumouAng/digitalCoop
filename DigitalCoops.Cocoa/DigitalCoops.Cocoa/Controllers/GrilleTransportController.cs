using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Business;
using Tms.Classes.Business.stock;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class GrilleTransportController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        // GET: GrilleTransport
        public ActionResult Index()
        {            
            
            #region Set Function's Access            

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();

            //bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            bool result = mSiteParDefaut.fnGetDefaultSite();
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{11a3f554-1f11-4faa-8acf-4d3c98e3bbff}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{0508d11f-43fd-49c3-bf5d-e6d9a03d7dd0}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{93c7275e-1194-492a-92c0-749f534b4c71}")))
                X.GetCmp<MenuItem>("mnuPrintList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{a8ce6315-fa16-4092-b261-3c454c357ee3}")))
                X.GetCmp<MenuItem>("mnuExport").Enable();
            else
                X.GetCmp<MenuItem>("mnuExport").Disable();            

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{99074f46-6333-4acf-b99d-bfe5c3740cf2}")))
                X.GetCmp<Hidden>("ruhiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("ruhiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{aa866e02-3c29-404c-be6a-142e457ac75a}")))
                X.GetCmp<Hidden>("ruhiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("ruhiddenPermDesactiver").SetValue(false);           
            
            #endregion

            return View();
        }

        public ActionResult onAdd()
        {            
            GrilleTransportViewModel mclass = new GrilleTransportViewModel();                        

            mclass._GrilleTransport = new GrilleTransport();                     
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormGrilleTransport", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onEdit(string ItemSelected)
        {            
            GrilleTransportViewModel mclass = new GrilleTransportViewModel();
            mclass._GrilleTransport = new GrilleTransport();
            mclass._GrilleTransport = JSON.Deserialize<GrilleTransport>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });            
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormGrilleTransport", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onConsult(string ItemSelected)
        {            
            GrilleTransportViewModel mclass = new GrilleTransportViewModel();
            mclass._GrilleTransport = new GrilleTransport();            

            mclass._GrilleTransport = JSON.Deserialize<GrilleTransport>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });            
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormGrilleTransport", Model = mclass, ViewData = ViewData };
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("GrilleTransportCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemProvenance, string ItemSite, string ItemStatut)
        {
            int provenanceID = GetCritriaValue(ItemProvenance);      
            int siteID = GetCritriaValue(ItemSite);
            int statut = GetCritriaValue(ItemStatut);

            var mListe = (new GrilleTransport()).fnSelect(provenanceID, siteID, statut);
            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemProvenance, string ItemSite, string ItemStatut)
        {
            try
            {
             
                Store mstore = X.GetCmp<Store>("storeListe");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemProvenance",ItemProvenance),
                                    new Ext.Net.Parameter("ItemSite",ItemSite),
                                    new Ext.Net.Parameter("ItemStatut",ItemStatut)                                    
                                });

                X.GetCmp<FormPanel>("GrilleTransportCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Re-Cleaning : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult onCancel(string ItemSelected)
        {
            try
            {
                GrilleTransport mGrilleTransport = JSON.Deserialize<GrilleTransport>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = mGrilleTransport.fnGet(mGrilleTransport.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Re-Cleaning, loading failed.");

                mGrilleTransport.UtilisateurModification = (string)Session["userName"];

                result = mGrilleTransport.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Re-Cleaning, Operation failed.");

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListe");

                    ModelProxy mProxy = mstore.GetById(mGrilleTransport.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mGrilleTransport);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Re-Cleaning : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitFormMethod(string ItemSelected)
        {            
            try
            {
                GrilleTransport mGrilleTransport = new GrilleTransport();                
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mGrilleTransport.IsNew = true;
                else
                {                    
                    mGrilleTransport.IsNew = false;
                    
                    mGrilleTransport.fnGet(Guid.Parse(GetFormValue("TxtGrilleTransportID")));                    

                    if (mGrilleTransport == null || mGrilleTransport.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Weighing load failed.");
                }

                bool result = false;                
                mGrilleTransport = MapFormToObject(mGrilleTransport);

                result = mGrilleTransport.fnUpdate();                

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListe");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mGrilleTransport);
                        X.GetCmp<RowSelectionModel>("rowListe").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mGrilleTransport.ID);
                        mProxy.BeginEdit();
                        mProxy.Set(mGrilleTransport);
                        mProxy.Commit();
                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormGrilleTransport").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Re-Cleaning : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        private GrilleTransport MapFormToObject(GrilleTransport mClass)
        {            
            Provenance mProv = new Provenance();
            mProv.ID = int.Parse(GetFormValue("cmbProvenance"));
            mProv.Nom = X.GetCmp<ComboBox>("cmbProvenance").SelectedItem.Text.ToString();
            mClass.Provenance = mProv;

            Site mSite = new Site();
            mSite.ID = int.Parse(GetFormValue("cmbSite"));
            mSite.Nom = X.GetCmp<ComboBox>("cmbSite").SelectedItem.Text.ToString();
            mClass.Sites = mSite;

            if (X.GetCmp<NumberField>("txtDistance").Text != string.Empty) mClass.Distance = decimal.Parse(X.GetCmp<NumberField>("txtDistance").RawText);
            if (X.GetCmp<NumberField>("txtCout").Text != string.Empty) mClass.CoutTransport = decimal.Parse(X.GetCmp<NumberField>("txtCout").RawText);

            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }
        private string GetFormValue(string id_Component)
        {
            return Request.Form[id_Component];
        }

        private int GetCritriaValue(string strComponent)
        {
            int value;

            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
                return value;
            else
                return -1;
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
                }
                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
            }
            catch (Exception ex)
            {
                Ext.Net.X.Msg.Alert("frm_Detail : GetFormExecMode", ex.Message).Show();
                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
            }
        }

        [DirectMethod(Namespace = "CompanyX")]
        public ActionResult DoConfirm()
        {
            // Manually configure Handler...
            //Msg.Confirm("Message", "Confirm?", "if (buttonId == 'yes') { CompanyX.DoYes(); } else { CompanyX.DoNo(); }").Show();

            // Configure individualock Buttons using a ButtonsConfig...
            X.Msg.Confirm("Message", "Confirm?", new MessageBoxButtonsConfig
            {
                Yes = new MessageBoxButtonConfig
                {
                    Handler = "CompanyX.MessageBox_Basic.DoYes()",
                    Text = "Yes Please"
                },
                No = new MessageBoxButtonConfig
                {
                    Handler = "CompanyX.MessageBox_Basic.DoNo()",
                    Text = "No Thanks"
                },                
            }).Show();

            return this.Direct();
        }

    }
}