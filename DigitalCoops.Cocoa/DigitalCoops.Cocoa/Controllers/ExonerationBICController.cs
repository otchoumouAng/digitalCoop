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
    public class ExonerationBICController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        // GET: ExonerationBIC
        public ActionResult Index()
        {            
            
            #region Set Function's Access            

            string UserName = (string)Session["userName"];
            Parametres mParam = new Parametres(0);

            ViewBag.Campagne = mParam.Campagne;

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{113C015A-E6C0-4837-BDA4-DB57F7D9B79D}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{b7046d5f-e181-4ade-81d5-018f3b0f5abc}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{a111bb67-0eec-4e38-8c58-c8ab6ccbaf45}")))
                X.GetCmp<MenuItem>("mnuPrintList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{2ba35e71-3615-4488-aa63-7a9bea5710d6}")))
                X.GetCmp<MenuItem>("mnuExport").Enable();
            else
                X.GetCmp<MenuItem>("mnuExport").Disable();            

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{a209f0fb-dcfa-424e-9b36-bdf4dda876d0}")))
                X.GetCmp<Hidden>("ruhiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("ruhiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{a21e38c8-85ab-4c6e-9ebc-456eeb674a34}")))
                X.GetCmp<Hidden>("ruhiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("ruhiddenPermDesactiver").SetValue(false);           
            
            #endregion

            return View();
        }

        public ActionResult onAdd()
        {            
            ExonerationBICViewModel mclass = new ExonerationBICViewModel();
            Parametres mParam = new Parametres(0);

            mclass._ExonerationBIC = new ExonerationBIC();
            mclass._ExonerationBIC.Campagne = new Campagne();
            mclass._ExonerationBIC.Campagne.Designation = mParam.Campagne;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormExonerationBIC", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onEdit(string ItemSelected)
        {            
            ExonerationBICViewModel mclass = new ExonerationBICViewModel();
            mclass._ExonerationBIC = new ExonerationBIC();
            mclass._ExonerationBIC = JSON.Deserialize<ExonerationBIC>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });            
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormExonerationBIC", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onConsult(string ItemSelected)
        {            
            ExonerationBICViewModel mclass = new ExonerationBICViewModel();
            mclass._ExonerationBIC = new ExonerationBIC();            

            mclass._ExonerationBIC = JSON.Deserialize<ExonerationBIC>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });            
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormExonerationBIC", Model = mclass, ViewData = ViewData };
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("ExonerationBICCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemFournisseur, string ItemAnnee, string ItemDateDebut, string ItemDateFin, string ItemStatut)
        {
            int frsID = GetCritriaValue(ItemFournisseur);                  
            int statut = GetCritriaValue(ItemStatut);       

            int _annee = -1;
            if (!String.IsNullOrEmpty(ItemAnnee))
            {
                bool isInt = int.TryParse(ItemAnnee, out _annee);
                //if (!isInt)
                //{
                //    X.MessageBox.Show(new MessageBoxConfig
                //    {
                //        Title = "Exoneration : Data Validation",
                //        Message = "Date incorrecte",
                //        Buttons = MessageBox.Button.OK,
                //        Icon = MessageBox.Icon.WARNING
                //    });
                //    return this.Direct();
                //}
            }
                
            DateTimeFormatInfo ukDtfi = new CultureInfo("fr-FR", false).DateTimeFormat;
            DateTime StartDate = DateTime.Now.AddDays(-1);
            if (!String.IsNullOrEmpty(ItemDateDebut) && !ItemDateDebut.Contains("1/1/0001"))
                StartDate = DateTime.Parse(ItemDateDebut);

            DateTime EndDate = DateTime.Now;
            if (!String.IsNullOrEmpty(ItemDateFin) && !ItemDateFin.Contains("1/1/0001"))
                EndDate = DateTime.Parse(ItemDateFin);

            var mListe = (new ExonerationBIC()).fnSelect(_annee, frsID, StartDate, EndDate, statut);
            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemFournisseur, string ItemAnnee, string ItemDateDebut, string ItemDateFin, string ItemStatut)
        {
            try
            {
             
                Store mstore = X.GetCmp<Store>("storeListe");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur",ItemFournisseur),
                                    new Ext.Net.Parameter("ItemAnnee",ItemAnnee),
                                    new Ext.Net.Parameter("ItemDateDebut",ItemDateDebut),
                                    new Ext.Net.Parameter("ItemDateFin",ItemDateFin),
                                    new Ext.Net.Parameter("ItemStatut",ItemStatut)                                    
                                });

                X.GetCmp<FormPanel>("ExonerationBICCP").Collapse(Direction.Top, false);
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
                ExonerationBIC mExonerationBIC = JSON.Deserialize<ExonerationBIC>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = mExonerationBIC.fnGet(mExonerationBIC.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Re-Cleaning, loading failed.");

                mExonerationBIC.UtilisateurModification = (string)Session["userName"];

                result = mExonerationBIC.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Re-Cleaning, Operation failed.");

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListe");

                    ModelProxy mProxy = mstore.GetById(mExonerationBIC.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mExonerationBIC);

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
                ExonerationBIC mExonerationBIC = new ExonerationBIC();                
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mExonerationBIC.IsNew = true;
                else
                {                    
                    mExonerationBIC.IsNew = false;
                    
                    mExonerationBIC.fnGet(Guid.Parse(GetFormValue("TxtExonerationBICID")));                    

                    if (mExonerationBIC == null || mExonerationBIC.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Weighing load failed.");
                }

                bool result = false;                
                mExonerationBIC = MapFormToObject(mExonerationBIC);

                result = mExonerationBIC.fnUpdate();                

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListe");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mExonerationBIC);
                        X.GetCmp<RowSelectionModel>("rowListe").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mExonerationBIC.ID);
                        mProxy.BeginEdit();
                        mProxy.Set(mExonerationBIC);
                        mProxy.Commit();
                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormExonerationBIC").Close();
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

        private ExonerationBIC MapFormToObject(ExonerationBIC mClass)
        {            
            Fournisseur _frs = new Fournisseur();
            _frs.ID = int.Parse(GetFormValue("cmbFournisseur"));
            _frs.Nom = X.GetCmp<ComboBox>("cmbFournisseur").SelectedItem.Text.ToString();
            mClass.Fournisseur = _frs;

            //Campagne mCamp = new Campagne();
            //mCamp.Designation = X.GetCmp<ComboBox>("cmbDetCrop").SelectedItem.Text.ToString();
            //mClass.Campagne = mCamp;
            mClass.Annee = int.Parse(X.GetCmp<NumberField>("txtAnnee").RawText.ToString());
            mClass.DateExoneration = DateTime.Parse(X.GetCmp<DateField>("txtDateBIC").RawText.ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second).AddMilliseconds(DateTime.Now.Millisecond);            
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