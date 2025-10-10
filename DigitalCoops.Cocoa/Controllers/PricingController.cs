using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ext.Net.MVC;
using Ext.Net;
using Tms.Classes.Business;
using Newtonsoft.Json;
using Tms.Classes.Shared;
using Tms.Components.Settings;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{        
    public class PricingController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
               
        // GET: Pricing
        public ActionResult Index()
        {
            return View();
        }
        #region Prix Journalier
        public ActionResult DailyPrice()
        {
            return View();
        }

        public ActionResult onAddDailyPrice()
        {
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Mask();
            PrixJournalierViewModel mclass = new PrixJournalierViewModel();

            mclass._PrixJournalier = new PrixJournalier();

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            //FormPanel mForm = X.GetCmp<FormPanel>("FormDailyPrice");           

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDailyPrice" , Model = mclass,  };
        }

        public ActionResult onEditDailyPrice(string ItemSelected)
        {
            PrixJournalier mclass = JSON.Deserialize<PrixJournalier>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            //MapObjectToForm(mclass);          

            PrixJournalierViewModel viewModel = new PrixJournalierViewModel();

            viewModel._PrixJournalier = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDailyPrice", Model = viewModel };
        }

        [HttpPost]
        public ActionResult SubmitDailyPriceMethod()
        {

            try
            {
                PrixJournalier mClass = new PrixJournalier();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtDailyPriceID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Prix Journalier load failed.");
                }

                mClass = MapFormDailyPriceToObject(mClass);

                bool result = mClass.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListe");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionListe").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormDailyPrice").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }

        public ActionResult SelectDailyPrice(string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {

            //int locationID = GetCritriaValue(ItemLocation);
            
            //DateTime? se = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(X.GetCmp<DateField>("TxtPeriodStart").Text); 
            DateTime ? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            //string startdate = string.IsNullOrEmpty(ItemPeriodStart) ? null : ItemPeriodStart;
            //string enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? null : ItemPeriodEnd;
            int status = -1;
            if (string.IsNullOrEmpty(ItemStatus))
            {
                status = -1;
            }else if (ItemStatus == "true")
            {
                status = 0;
            }
            

            var mListe = (new PrixJournalier()).fnSelect(null, null, -1);

            return this.Store(mListe);
        }

        public ActionResult OnFilterDailyPrice()
        {
            FormPanel mform = X.GetCmp<FormPanel>("filterpanel");
            
            mform.Collapsed = false;
            return this.Direct();
        }
        
        public ActionResult onRefreshDailyPrice(string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            Store mstore = X.GetCmp<Store>("storeListe");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                //new Ext.Net.Parameter("ItemLocation"   ,ItemLocation),
                                new Ext.Net.Parameter("ItemPeriodStart"     ,ItemPeriodStart),
                                new Ext.Net.Parameter("ItemPeriodEnd"           ,ItemPeriodEnd),
                                new Ext.Net.Parameter("ItemStatus"           ,ItemStatus)
                            });
            FormPanel mform = X.GetCmp<FormPanel>("filterpanel");

            mform.Collapsed = true;

            return this.Direct();
        }

        public ActionResult onApproveDailyPrice(string ItemSelected)
        {            
            try
            {
                PrixJournalier mClass = JSON.Deserialize<PrixJournalier>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
                              
                bool result = mClass.fnGet(mClass.ID);

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("onApproveDailyPrice :Prix Journalier Approve failed.");

                result = mClass.fnApprove();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListe");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }

        public ActionResult OnActivateDeactivate_DailyPrice(string ItemSelected)
        {            
            try
            {
                PrixJournalier mClass = JSON.Deserialize<PrixJournalier>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = mClass.fnGet(mClass.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Prix Journalier loading failed.");

                if (mClass.Desactive)
                    result = mClass.fnActivate();
                else
                    result = mClass.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Prix Journalier Approval failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListe");
                    
                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }
                                              
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }

        private PrixJournalier MapFormDailyPriceToObject(PrixJournalier mClass)
        {            
            mClass.DatePrix = DateTime.Parse( X.GetCmp<DateField>("TxtEntryDate").RawText.ToString());
            mClass.DateDebut = DateTime.Parse(X.GetCmp<DateField>("TxtStartDate").RawText.ToString());
            mClass.DateEcheance = DateTime.Parse(X.GetCmp<DateField>("TxtDueDate").RawText.ToString());
            mClass.Statut = "0";
            mClass.Prix = decimal.Parse(X.GetCmp<TextField>("TxtPrice").Text);
            mClass.Numero = X.GetCmp<Hidden>("hiddenNumeroPrice").Text;
            mClass.Site = new Site();
            //mClass.Site.ID = int.Parse(GetFormValue("LocationID"));
            //mClass.Site.Nom = X.GetCmp<ComboBox>("LocationID").SelectedItem.Text.ToString();

            return mClass;
        }

        private void MapObjectToFormDailyPrice(PrixJournalier mClass)
        {
            X.GetCmp<TextField>("TxtDailyPriceID").Text = mClass.ID.ToString();
            X.GetCmp<TextField>("TxtEntryDate").Text = mClass.DatePrix.ToString();
            X.GetCmp<TextField>("TxtStartDate").Text = mClass.DateDebut.ToString();
            X.GetCmp<TextField>("TxtDueDate").Text = mClass.DateEcheance.ToString();
            X.GetCmp<TextField>("TxtPrice").Text = mClass.Prix.ToString();
            X.GetCmp<TextField>("hiddenNumeroPrice").Text = mClass.Numero.ToString();
            //X.GetCmp<ComboBox>("LocationID").SetValue(mClass.Site.ID.ToString());
            

        }

        #endregion

        #region SpotPrice
        public ActionResult SpotPrice()
        {
            return View();
        }

        public ActionResult onAddSpotPrice()
        {
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Mask();
            PrixNegocieViewModel mclass = new PrixNegocieViewModel();

            mclass._PrixNegocie = new PrixNegocie();

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            //FormPanel mForm = X.GetCmp<FormPanel>("FormDailyPrice");                       
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormSpotPrice", Model = mclass, };
        }
        public ActionResult onEditSpotPrice(string ItemSelected)
        {
            PrixNegocie mclass = JSON.Deserialize<PrixNegocie>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });                     

            PrixNegocieViewModel viewModel = new PrixNegocieViewModel();

            viewModel._PrixNegocie = mclass;                                    

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormSpotPrice", Model = viewModel };
        }

        public ActionResult SubmitSpotPriceMethod()
        {
            try
            {
                PrixNegocie mClass = new PrixNegocie();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtSpotPriceID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Prix Negocié load failed.");
                }

                mClass = MapFormSpotPriceToObject(mClass);

                bool result = mClass.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListe");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionListe").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormSpotPrice").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }

        public ActionResult SelectSpotPrice(string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd)
        {
            int FournisseurID = GetCritriaValue(ItemFournisseur);
            
            DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());            
            
            //int status = -1;
            //if (string.IsNullOrEmpty(ItemStatus))
            //{
            //    status = -1;
            //}
            //else if (ItemStatus == "true")
            //{
            //    status = 0;
            //}

            var mListe = (new PrixNegocie()).fnSelect(FournisseurID, startdate, enddate, "-1");
            return this.Store(mListe);
        }

        public ActionResult OnFilterSpotPrice()
        {
            FormPanel mform = X.GetCmp<FormPanel>("filterpanel");

            mform.Collapsed = false;
            return this.Direct();
        }

        public ActionResult OnRefreshSpotPrice(string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd)
        {
            Store mstore = X.GetCmp<Store>("storeListe");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                new Ext.Net.Parameter("ItemPeriodStart"     ,ItemPeriodStart),
                                new Ext.Net.Parameter("ItemPeriodEnd"           ,ItemPeriodEnd)                                
                            });

            FormPanel mform = X.GetCmp<FormPanel>("filterpanel");

            mform.Collapsed = true;
            return this.Direct();
        }

        public ActionResult OnApproveSpotPrice(string ItemSelected)
        {           
            try
            {
                PrixNegocie mClass = JSON.Deserialize<PrixNegocie>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
                bool result = mClass.fnGet(mClass.ID);

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("Approve : Prix Negocié Approve failed.");                

                result = mClass.fnApprove();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListe");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }

        public ActionResult OnActivateDeactivate_SpotPrice(string ItemSelected)
        {
            try
            {
                PrixNegocie mClass = JSON.Deserialize<PrixNegocie>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = mClass.fnGet(mClass.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Prix Negocié loading failed.");

                if (mClass.Desactive)
                    result = mClass.fnActivate();
                else
                    result = mClass.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Prix Negocié Approval failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListe");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }

        private PrixNegocie MapFormSpotPriceToObject(PrixNegocie mClass)
        {
            //string testdate = X.GetCmp<DateField>("TxtPriceDate").Text;
            //DateTime testdateSelected = X.GetCmp<DateField>("TxtPriceDate").SelectedDate;
            //string testdatevalueraw = X.GetCmp<DateField>("TxtPriceDate").RawText.ToString();
            mClass.DatePrix = DateTime.Parse(X.GetCmp<DateField>("TxtPriceDate").RawText.ToString());
            mClass.Prix = decimal.Parse(X.GetCmp<TextField>("TxtPrice").Text);

            mClass.Fournisseur = new Fournisseur();
            mClass.Fournisseur.ID = int.Parse(GetFormValue("FournisseurID"));
            mClass.Fournisseur.Nom = X.GetCmp<ComboBox>("FournisseurID").SelectedItem.Text.ToString();

            mClass.ModeApplication = new PrixNegocieModeApplication();

            int mode = int.Parse(GetFormValue("ModeApplicationID"));
            mClass.ModeApplication.ID = int.Parse(GetFormValue("ModeApplicationID"));
            mClass.Numero = GetFormValue("hiddenNumeroPrice");
            mClass.DateDebut = mode == 1 ? DateTime.Parse(X.GetCmp<DateField>("TxtStartDate").RawText.ToString()) : DateTime.Now;
            mClass.DateEcheance = mode == 1 ? DateTime.Parse(X.GetCmp<DateField>("TxtDueDate").RawText.ToString()) : DateTime.Now;
            //mClass.DateDebut = DateTime.Parse(GetFormValue("hiddenNumeroPrice"));
            return mClass;
        }

        private void MapObjectToFormSpotPrice(PrixNegocie mClass)
        {
            X.GetCmp<ComboBox>("FournisseurID").SetValue(mClass.Fournisseur.ID.ToString());
            X.GetCmp<TextField>("TxtPriceDate").Text = mClass.DatePrix.ToString();
            X.GetCmp<TextField>("TxtPrice").Text = mClass.Prix.ToString();
            X.GetCmp<DateField>("TxtStartDate").Text = mClass.DateDebut.ToString();
            X.GetCmp<DateField>("TxtDueDate").Text = mClass.DateEcheance.ToString();
            X.GetCmp<ComboBox>("ModeApplicationID").SetValue(mClass.ModeApplication.ID.ToString());            
            
            X.GetCmp<TextField>("hiddenNumeroPrice").Text = mClass.Numero.ToString();          

        }

        #endregion

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
            X.GetCmp<RowSelectionModel>("rowSelectionListe").DeselectAll();
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