#region V1
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using Ext.Net.MVC;
//using Ext.Net;
//using Tms.Classes.Business;
//using Newtonsoft.Json;
//using Tms.Classes.Shared;
//using Tms.Components.Settings;
//using Tms.Components.Data;

//namespace Tms2017.MVC.Controllers
//{
//    public class PrixNegocieAgenceController : BaseController
//    {
//        const string UPDATE = "Update";
//        const string ADD_NEW = "AddNew";
//        const string CONSULT = "Consult";
//        const string APPROVE = "Approve";
//        const string DEFAULT = "Default";

//        // GET: PrixNegocieAgence
//        public ActionResult Index()
//        {
//            return View();
//        }

//        public ActionResult onAdd()
//        {
//            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
//            mViewport.Mask();
//            PrixNegocieAgenceViewModel mclass = new PrixNegocieAgenceViewModel();

//            mclass._PrixNegocieAgence = new PrixNegocieAgence();

//            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

//            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrixNegocieAgence", Model = mclass, };
//        }

//        public ActionResult onEdit(string ItemSelected)
//        {
//            PrixNegocieAgence mclass = JSON.Deserialize<PrixNegocieAgence>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

//            PrixNegocieAgenceViewModel viewModel = new PrixNegocieAgenceViewModel();

//            viewModel._PrixNegocieAgence = mclass;

//            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

//            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrixNegocieAgence", Model = viewModel };
//        }

//        public ActionResult SubmitFormMethod()
//        {
//            try
//            {
//                PrixNegocieAgence mClass = new PrixNegocieAgence();

//                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

//                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
//                    mClass.IsNew = true;
//                else
//                {
//                    mClass.IsNew = false;

//                    mClass.fnGet(Guid.Parse(GetFormValue("TxtPrixNegocieAgenceID")));

//                    if (mClass == null || mClass.ID == Guid.Empty)
//                        throw new Exception("SubmitFormMethod : Prix Negocié load failed.");
//                }

//                mClass = MapFormToObject(mClass);

//                bool result = mClass.fnUpdate();

//                if (result)
//                {
//                    Store mstore = X.GetCmp<Store>("storeListe");
//                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
//                    {
//                        mstore.Insert(0, mClass);
//                        X.GetCmp<RowSelectionModel>("rowSelectionListe").Select(0);
//                    }
//                    else
//                    {
//                        ModelProxy mProxy = mstore.GetById(mClass.ID);

//                        mProxy.BeginEdit();

//                        mProxy.Set(mClass);

//                        mProxy.Commit();

//                        mProxy.EndEdit();
//                    }

//                    X.GetCmp<Window>("FormPrixNegocieAgence").Close();
//                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
//                    mViewport.Unmask();
//                }
//            }
//            catch (Exception ex)
//            {
//                X.MessageBox.Alert("Error", ex.Message).Show();
//            }

//            return this.Direct();
//        }

//        public ActionResult Select(string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd)
//        {
//            int FournisseurID = GetCritriaValue(ItemFournisseur);

//            DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
//            DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

//            //int status = -1;
//            //if (string.IsNullOrEmpty(ItemStatus))
//            //{
//            //    status = -1;
//            //}
//            //else if (ItemStatus == "true")
//            //{
//            //    status = 0;
//            //}

//            var mListe = (new PrixNegocieAgence()).fnSelect(FournisseurID, startdate, enddate, "-1");
//            return this.Store(mListe);
//        }

//        public ActionResult OnFilter()
//        {
//            FormPanel mform = X.GetCmp<FormPanel>("filterpanel");

//            mform.Collapsed = false;
//            return this.Direct();
//        }

//        public ActionResult OnRefresh(string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd)
//        {
//            Store mstore = X.GetCmp<Store>("storeListe");

//            mstore.Reload();

//            mstore.Reload(new Ext.Net.ParameterCollection()
//                            {
//                                new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
//                                new Ext.Net.Parameter("ItemPeriodStart"     ,ItemPeriodStart),
//                                new Ext.Net.Parameter("ItemPeriodEnd"           ,ItemPeriodEnd)
//                            });

//            FormPanel mform = X.GetCmp<FormPanel>("filterpanel");

//            mform.Collapsed = true;
//            return this.Direct();
//        }

//        public ActionResult OnApprove(string ItemSelected)
//        {
//            try
//            {
//                PrixNegocieAgence mClass = JSON.Deserialize<PrixNegocieAgence>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
//                bool result = mClass.fnGet(mClass.ID);

//                if (mClass == null || mClass.ID == Guid.Empty)
//                    throw new Exception("Approve : Prix Negocié Approve failed.");

//                result = mClass.fnApprove();

//                if (result)
//                {
//                    Store mstore = X.GetCmp<Store>("storeListe");

//                    ModelProxy mProxy = mstore.GetById(mClass.ID);

//                    mProxy.BeginEdit();

//                    mProxy.Set(mClass);

//                    mProxy.Commit();

//                    mProxy.EndEdit();
//                }

//            }
//            catch (Exception ex)
//            {
//                X.MessageBox.Alert("Error", ex.Message).Show();
//            }

//            return this.Direct();
//        }

//        public ActionResult OnActivateDeactivate(string ItemSelected)
//        {
//            try
//            {
//                PrixNegocieAgence mClass = JSON.Deserialize<PrixNegocieAgence>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

//                bool result = mClass.fnGet(mClass.ID);

//                if (!result)
//                    throw new Exception("OnActivateDeactivate : Prix Negocié loading failed.");

//                if (mClass.Desactive)
//                    result = mClass.fnActivate();
//                else
//                    result = mClass.fnDeActivate();

//                if (!result)
//                    throw new Exception("OnActivateDeactivate : Prix Negocié Approval failed.");


//                if (result)
//                {
//                    Store mstore = X.GetCmp<Store>("storeListe");

//                    ModelProxy mProxy = mstore.GetById(mClass.ID);

//                    mProxy.BeginEdit();

//                    mProxy.Set(mClass);

//                    mProxy.Commit();

//                    mProxy.EndEdit();
//                }

//            }
//            catch (Exception ex)
//            {
//                X.MessageBox.Alert("Error", ex.Message).Show();
//            }

//            return this.Direct();
//        }

//        private PrixNegocieAgence MapFormToObject(PrixNegocieAgence mClass)
//        {
//            //string testdate = X.GetCmp<DateField>("TxtPriceDate").Text;
//            //DateTime testdateSelected = X.GetCmp<DateField>("TxtPriceDate").SelectedDate;
//            //string testdatevalueraw = X.GetCmp<DateField>("TxtPriceDate").RawText.ToString();
//            mClass.DatePrix = DateTime.Parse(X.GetCmp<DateField>("TxtPriceDate").RawText.ToString());
//            mClass.Prix = decimal.Parse(X.GetCmp<TextField>("TxtPrice").Text);

//            mClass.Fournisseur = new Fournisseur();
//            mClass.Fournisseur.ID = int.Parse(GetFormValue("FournisseurID"));
//            mClass.Fournisseur.Nom = X.GetCmp<ComboBox>("FournisseurID").SelectedItem.Text.ToString();

//            mClass.ModeApplication = new PrixNegocieModeApplication();

//            int mode = int.Parse(GetFormValue("ModeApplicationID"));
//            mClass.ModeApplication.ID = int.Parse(GetFormValue("ModeApplicationID"));
//            mClass.Numero = GetFormValue("hiddenNumeroPrice");
//            mClass.DateDebut = mode == 1 ? DateTime.Parse(X.GetCmp<DateField>("TxtStartDate").RawText.ToString()) : DateTime.Now;
//            mClass.DateEcheance = mode == 1 ? DateTime.Parse(X.GetCmp<DateField>("TxtDueDate").RawText.ToString()) : DateTime.Now;
//            //mClass.DateDebut = DateTime.Parse(GetFormValue("hiddenNumeroPrice"));
//            return mClass;
//        }

//        private void MapObjectToForm(PrixNegocieAgence mClass)
//        {
//            X.GetCmp<ComboBox>("FournisseurID").SetValue(mClass.Fournisseur.ID.ToString());
//            X.GetCmp<TextField>("TxtPriceDate").Text = mClass.DatePrix.ToString();
//            X.GetCmp<TextField>("TxtPrice").Text = mClass.Prix.ToString();
//            X.GetCmp<DateField>("TxtStartDate").Text = mClass.DateDebut.ToString();
//            X.GetCmp<DateField>("TxtDueDate").Text = mClass.DateEcheance.ToString();
//            X.GetCmp<ComboBox>("ModeApplicationID").SetValue(mClass.ModeApplication.ID.ToString());

//            X.GetCmp<TextField>("hiddenNumeroPrice").Text = mClass.Numero.ToString();

//        }

//        private string GetFormValue(string id_Component)
//        {
//            return Request.Form[id_Component];
//        }

//        private Tms.Components.Settings.EnumsDefinition.eExecMode GetFormExecMode(object hidAction)
//        {
//            try
//            {
//                if (hidAction != null)
//                {
//                    if (hidAction.ToString().Equals(ADD_NEW))
//                        return Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
//                    else if (hidAction.ToString().Equals(APPROVE))
//                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;
//                    else if (hidAction.ToString().Equals(CONSULT))
//                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
//                    else if (hidAction.ToString().Equals(DEFAULT))
//                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
//                    else if (hidAction.ToString().Equals(UPDATE))
//                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
//                }
//                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
//            }
//            catch (Exception ex)
//            {
//                Ext.Net.X.Msg.Alert("frm_Detail : GetFormExecMode", ex.Message).Show();
//                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
//            }
//        }

//        private int GetCritriaValue(string strComponent)
//        {
//            int value;

//            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
//                return value;
//            else
//                return -1;
//        }

//        private int GetCritriasValue(string strComponent)
//        {
//            if (X.GetCmp<ComboBox>(strComponent) != null && X.GetCmp<ComboBox>(strComponent).SelectedItem != null && X.GetCmp<ComboBox>(strComponent).SelectedItem.Value != null)
//                return int.Parse(X.GetCmp<ComboBox>(strComponent).SelectedItem.Value.ToString());
//            else
//                return -1;
//        }

//        private void DeselectGridRows()
//        {
//            //X.Js.Call("App.rowSelectionListe.deselectAll();");
//            X.GetCmp<RowSelectionModel>("rowSelectionListe").DeselectAll();
//        }

//        public void CreateIconsList()
//        {
//            try
//            {
//                //Ext.Net.ResourceManager.RegisterGlobalIcon(Icon.PageCancel);

//                //string url =  Ext.Net.ResourceManager.GetInstance().GetIconUrl(Icon.Tick);
//            }
//            catch (Exception ex)
//            {

//                X.Msg.Alert("frmLBC_ReceptionInDistrict_Overview : CreateIconList", ex.Message).Show();
//            }
//        }

//    }
//}
#endregion

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
using System.Globalization;
using Tms.Classes.Security;

namespace Tms2017.MVC.Controllers
{
    public class PrixNegocieAgenceController : BaseController
    {

        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";

        // GET: PrixNegocieAgence
        public ActionResult Index()
        {

            DateTime firstdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            DateTime lastDayOfMonth = firstdayofmonth.AddMonths(1).AddDays(-1);

            X.GetCmp<DateField>("TxtPeriodStart").RawText = firstdayofmonth.ToShortDateString();
            X.GetCmp<DateField>("TxtPeriodEnd").RawText = lastDayOfMonth.ToShortDateString();

            X.GetCmp<FormPanel>("CriteriaPanelPrixNegocieAgence").SetTitle("Du : " + firstdayofmonth.ToShortDateString() + " Au : " + lastDayOfMonth.ToShortDateString());


            #region Set Function's Access
            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{C2A8EDEE-54FE-45C4-A7D6-FA50C4B66A6B}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{C6AC056F-C60D-4B54-8A73-93E0207ECCE5}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{F663846F-C832-402D-9B85-F6EB1E6132EF}")))
                X.GetCmp<MenuItem>("mnuPrintLot").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintLot").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{EC36A28C-B082-44E9-B3A3-69ED9F719E86}")))
                X.GetCmp<MenuItem>("mnuExportLot").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportLot").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{1F600E40-B52A-46B5-9C8C-217569876F00}")))
                X.GetCmp<Hidden>("lthiddenPermModify").SetValue(true);
            else
                X.GetCmp<Hidden>("lthiddenPermModify").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{E3AFFC6C-D617-42F0-8889-DE0B8913C4FC}")))
                X.GetCmp<Hidden>("lthiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("lthiddenPermDesactiver").SetValue(false);
            #endregion

            return View();
        }

        #region PrixNegocieAgence        

        //public ActionResult onAdd()
        //{
        //    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
        //    mViewport.Mask();
        //    PrixNegocieAgenceViewModel mclass = new PrixNegocieAgenceViewModel();

        //    mclass._PrixNegocieAgence = new PrixNegocieAgence();
        //    mclass._Parametres = new Parametres();
        //    mclass._PrixNegocieAgenceParam = new PrixNegocieAgenceParamViewModel();
        //    mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

        //    //var mParam = (new Parametres()).fnSelect();
        //    mclass._Parametres.fnGet();
        //    //Parametres MclassParam = new Parametres();
        //    //mclass._Parametres = mParam[0] as Parametres;
        //    //mclass._Parametres = MclassParam;

        //    PrixJournalierAgence mClass = new PrixJournalierAgence();
        //    mClass.fnSelectByDate(DateTime.Now);

        //    mclass._PrixNegocieAgenceParam.PrixJournalierAgence = mClass.Prix.ToString();
        //    mclass._PrixNegocieAgenceParam.TolerancePrixNegocieAgence = mclass._Parametres.PrixNegocie.ToString();

        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrixNegocieAgence", Model = mclass, };
        //}
        //public ActionResult onEdit(string ItemSelected)
        //{
        //    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
        //    mViewport.Mask();

        //    PrixNegocieLivraison PNClass = JSON.Deserialize<PrixNegocieLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

        //    PrixNegocieAgence mclass = new PrixNegocieAgence();

        //    PrixNegocieAgenceViewModel viewModel = new PrixNegocieAgenceViewModel();

        //    mclass.fnGet(PNClass.ID);
        //    viewModel._PrixNegocieLivraison = new PrixNegocieLivraison();
        //    viewModel._PrixNegocieLivraison = PNClass;
        //    viewModel._PrixNegocieAgenceParam = new PrixNegocieAgenceParamViewModel();
        //    viewModel._Parametres = new Parametres();
        //    viewModel._PrixNegocieAgence = mclass;

        //    viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

        //    //var mParam = (new Parametres()).fnSelect();

        //    //viewModel._Parametres = mParam[0] as Parametres;
        //    viewModel._Parametres.fnGet();
        //    PrixJournalier mClass = new PrixJournalier();
        //    mClass.fnSelectByDate(mclass.DatePrix);

        //    viewModel._PrixNegocieAgenceParam.PrixJournalierAgence = mClass.Prix.ToString();
        //    viewModel._PrixNegocieAgenceParam.TolerancePrixNegocieAgence = viewModel._Parametres.PrixNegocieAgence.ToString();

        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrixNegocieAgence", Model = viewModel };
        //}

        //public ActionResult onConsult(string ItemSelected)
        //{
        //    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
        //    mViewport.Mask();

        //    PrixNegocieLivraison PNClass = JSON.Deserialize<PrixNegocieLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

        //    PrixNegocieAgence mclass = new PrixNegocieAgence();

        //    PrixNegocieAgenceViewModel viewModel = new PrixNegocieAgenceViewModel();

        //    mclass.fnGet(PNClass.ID);
        //    viewModel._PrixNegocieLivraison = new PrixNegocieLivraison();
        //    viewModel._PrixNegocieLivraison = PNClass;
        //    viewModel._PrixNegocieAgenceParam = new PrixNegocieAgenceParamViewModel();
        //    viewModel._Parametres = new Parametres();
        //    viewModel._PrixNegocieAgence = mclass;

        //    viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

        //    //var mParam = (new Parametres()).fnSelect();

        //    //viewModel._Parametres.fnGet();
        //    PrixJournalierAgence mClass = new PrixJournalierAgence();
        //    mClass.fnSelectByDate(mclass.DatePrix);

        //    viewModel._PrixNegocieAgenceParam.PrixJournalierAgence = mClass.Prix.ToString();
        //    viewModel._PrixNegocieAgenceParam.TolerancePrixNegocieAgence = viewModel._Parametres.PrixNegocieAgence.ToString();

        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrixNegocieAgence", Model = viewModel };
        //}

        //public ActionResult SubmitFormMethod(string ItemSelected)
        //{
        //    DataSource _db = new DataSource();
        //    DataTransaction mtran = new DataTransaction();

        //    try
        //    {
        //        PrixNegocieAgence mClass = new PrixNegocieAgence();
        //        PrixNegocieLivraison prixlivraison;
        //        Guid prixnegocielivraison = Guid.Parse(GetFormValue("TxtPrixNegocieAgenceDeliverieID"));
        //        Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

        //        if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
        //            mClass.IsNew = true;
        //        else
        //        {
        //            mClass.IsNew = false;

        //            mClass.fnGet(Guid.Parse(GetFormValue("TxtPrixNegocieAgenceID")));

        //            if (mClass == null || mClass.ID == Guid.Empty)
        //                throw new Exception("SubmitFormMethod : Prix Negocié load failed.");
        //        }
        //        bool result = true;
        //        bool resultLivraison = true;

        //        mClass = MapFormToObject(mClass);

        //        if (mClass.ModeApplication.ID == 2)
        //        {
        //            List<Livraison> ItemLivraison = JSON.Deserialize<List<Livraison>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
        //            if (ItemLivraison.Count() > 0)
        //            {
        //                _db = mClass.db();
        //                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
        //                result = mClass.fnUpdate(mtran);

        //                if (result)
        //                {

        //                    for (int i = 0; i < ItemLivraison.Count(); i++)
        //                    {
        //                        prixlivraison = new PrixNegocieLivraison();
        //                        prixlivraison.Livraison = new Livraison();
        //                        prixlivraison.PrixNegocieAgence = new PrixNegocieAgence();

        //                        prixlivraison.SetDataSource(_db);

        //                        prixlivraison.Livraison.ID = ItemLivraison[i].ID;

        //                        prixlivraison.IsNew = ItemLivraison[i].IsNew;
        //                        prixlivraison.PrixNegocie.ID = mClass.ID;
        //                        prixlivraison.UtilisateurCreation = (string)Session["userName"];
        //                        prixlivraison.UtilisateurModification = (string)Session["userName"];
        //                        if (prixlivraison.IsNew)
        //                        {
        //                            resultLivraison = prixlivraison.fnUpdate(mtran);
        //                        }

        //                        if (!resultLivraison)
        //                        {
        //                            break;
        //                        }
        //                    }

        //                    if (!resultLivraison)
        //                    {
        //                        _db.RollBackTransaction(mtran);
        //                    }
        //                    _db.CommitTransaction(mtran);

        //                }
        //            }
        //            else
        //            {
        //                _db.RollBackTransaction(mtran);
        //                X.MessageBox.Show(new MessageBoxConfig
        //                {
        //                    Title = "Prix Negocié : Data Validation",
        //                    Message = "Please select specific(s) Livraison(s)",
        //                    Buttons = MessageBox.Button.OK,
        //                    Icon = MessageBox.Icon.WARNING
        //                });
        //                return this.Direct();
        //            }
        //        }
        //        else
        //        {
        //            result = mClass.fnUpdate();
        //        }

        //        if (result)
        //        {

        //            if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.Update && mClass.ModeApplication.ID == 1)
        //            {
        //                //Livraison ListeLivraisonToUpdate = new Livraison();
        //                PrixNegocieLivraison pnclass = new PrixNegocieLivraison();
        //                List<DataPersist> myList = new Livraison().fnSelectBySpotPrice(mClass.ID, -1);
        //                if (myList.Count > 0)
        //                {
        //                    //ListeLivraisonToUpdate = myList[0] as Livraison;
        //                    foreach (Livraison item in myList)
        //                    {
        //                        pnclass.RowVersionKey = item.RowVersionKey;
        //                        pnclass.fnRemoveDelivery(item.ID);
        //                    }
        //                }
        //            }

        //            prixlivraison = new PrixNegocieLivraison();


        //            Store mstore = X.GetCmp<Store>("storeListePrixNegocieAgence");
        //            if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
        //            {
        //                prixlivraison.fnGet(mClass.ID);
        //                mstore.Insert(0, prixlivraison);
        //                X.GetCmp<RowSelectionModel>("rowSelectionListePrixNegocieAgence").Select(0);
        //            }
        //            else
        //            {
        //                prixlivraison.fnGet(prixnegocielivraison);
        //                ModelProxy mProxy = mstore.GetById(prixnegocielivraison);

        //                mProxy.BeginEdit();

        //                mProxy.Set(prixlivraison);

        //                mProxy.Commit();

        //                mProxy.EndEdit();
        //            }

        //            X.GetCmp<Window>("FormPrixNegocieAgence").Close();
        //            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
        //            mViewport.Unmask();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _db.RollBackTransaction(mtran);
        //        X.MessageBox.Show(new MessageBoxConfig
        //        {
        //            Title = "Prix Negocié : Data Validation",
        //            Message = ex.Message,
        //            Buttons = MessageBox.Button.OK,
        //            Icon = MessageBox.Icon.WARNING
        //        });

        //    }

        //    return this.Direct();
        //}

        public ActionResult Select(StoreRequestParameters parameters, string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd, string ItemCampagne, string ItemSite, string ItemStatus)
        {
            int FournisseurID = GetCritriaValue(ItemFournisseur);
            int SiteID = GetCritriaValue(ItemSite);
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "{Tous}";
            }

            DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            string Status = ItemStatus;

            var mListe = (new PrixNegocieLivraison()).fnSelect(Campagne, SiteID, FournisseurID, startdate, enddate, Status);

            return this.Store(mListe);

        }
        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelPrixNegocieAgence");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListePrixNegocieAgence");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                new Ext.Net.Parameter("ItemPeriodStart"     ,ItemPeriodStart),
                                new Ext.Net.Parameter("ItemPeriodEnd"           ,ItemPeriodEnd)
                            });

                FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelPrixNegocieAgence");
                mform.Collapsed = true;
            }
            catch (Exception ex)
            {
                if (ex is FormatException)
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Prix Negocié : Data Validation",
                        Message = "La periode saisie n'est pas valide !",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                }
                else
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Prix Negocié : Data Validation",
                        Message = ex.Message,
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                }

            }

            return this.Direct();
        }

        [DirectMethod]
        public ActionResult OnSelectEntryDate(string ItemDatePrix)
        {
            //DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);

            PrixJournalierAgence mClass = new PrixJournalierAgence();
            DateTime ValidDatePrix;

            bool IsvalidDate = DateTime.TryParseExact(ItemDatePrix, "d", CultureInfo.CurrentUICulture, DateTimeStyles.None, out ValidDatePrix);

            if (IsvalidDate)
                mClass.fnSelectByDate(DateTime.Parse(ItemDatePrix));

            X.GetCmp<TextField>("LabelPrixJournalierAgence").Text = mClass.Prix.ToString();
            X.GetCmp<TextField>("LabelPrixJournalierAgence").Hidden = false;
            return this.Direct();
        }

        //public ActionResult OnApprove(string ItemSelected)
        //{
        //    try
        //    {
        //        PrixNegocieLivraison mClass = JSON.Deserialize<PrixNegocieLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
        //        PrixNegocieAgence PxNegocie = new PrixNegocieAgence();

        //        bool result = PxNegocie.fnGet(mClass.ID);

        //        if (mClass == null || mClass.ID == Guid.Empty)
        //            throw new Exception("Approve : Prix Negocié Approve failed.");

        //        mClass.UtilisateurCreation = (string)Session["userName"];
        //        mClass.UtilisateurModification = (string)Session["userName"];
        //        PxNegocie.UtilisateurCreation = (string)Session["userName"];
        //        PxNegocie.UtilisateurModification = (string)Session["userName"];
        //        result = PxNegocie.fnApprove();

        //        if (result)
        //        {
        //            PxNegocie.Statut = "AP";
        //            mClass.PrixNegocieAgence = new PrixNegocieAgence();
        //            mClass.PrixNegocieAgence = PxNegocie;
        //            Store mstore = X.GetCmp<Store>("storeListePrixNegocieAgence");

        //            ModelProxy mProxy = mstore.GetById(mClass.ID);

        //            mProxy.BeginEdit();

        //            mProxy.Set(mClass);

        //            mProxy.Commit();

        //            mProxy.EndEdit();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        X.MessageBox.Show(new MessageBoxConfig
        //        {
        //            Title = "Prix Negocié : Approuver",
        //            Message = ex.Message,
        //            Buttons = MessageBox.Button.OK,
        //            Icon = MessageBox.Icon.WARNING
        //        });
        //    }

        //    return this.Direct();
        //}

        //public ActionResult OnActivateDeactivate(string ItemSelected)
        //{
        //    try
        //    {
        //        PrixNegocieLivraison mClass = JSON.Deserialize<PrixNegocieLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
        //        PrixNegocieAgence PxNegocie = new PrixNegocieAgence();
        //        bool result = PxNegocie.fnGet(mClass.ID);

        //        if (!result)
        //            throw new Exception("OnActivateDeactivate : Prix Negocié loading failed.");

        //        mClass.UtilisateurModification = (string)Session["userName"];

        //        PxNegocie.UtilisateurModification = (string)Session["userName"];

        //        if (PxNegocie.Desactive)
        //            result = PxNegocie.fnActivate();
        //        else
        //            result = PxNegocie.fnDeActivate();

        //        if (!result)
        //            throw new Exception("OnActivateDeactivate : Prix Negocié Operation failed.");


        //        if (result)
        //        {
        //            mClass.PrixNegocieAgence = new PrixNegocieAgence();
        //            mClass.PrixNegocieAgence = PxNegocie;
        //            Store mstore = X.GetCmp<Store>("storeListePrixNegocieAgence");

        //            ModelProxy mProxy = mstore.GetById(mClass.ID);

        //            mProxy.BeginEdit();

        //            mProxy.Set(mClass);

        //            mProxy.Commit();

        //            mProxy.EndEdit();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        X.MessageBox.Show(new MessageBoxConfig
        //        {
        //            Title = "Prix Negocié : Data Validation",
        //            Message = ex.Message,
        //            Buttons = MessageBox.Button.OK,
        //            Icon = MessageBox.Icon.WARNING
        //        });
        //    }

        //    return this.Direct();
        //}

        public ActionResult OnAddDelivery(string ItemFournisseur, string ItemExecMode)
        {
            PrixNegocieAgenceViewModel mclass = new PrixNegocieAgenceViewModel();

            if (!string.IsNullOrEmpty(ItemFournisseur))
            {

                mclass._PrixNegocieAgence = new PrixNegocieAgence();

                mclass._PrixNegocieAgence.Fournisseur = new Fournisseur();
                mclass._PrixNegocieAgence.Fournisseur.ID = int.Parse(ItemFournisseur);

                mclass._ExecMode = ItemExecMode == "Update" ? Tms.Components.Settings.EnumsDefinition.eExecMode.Update : Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            }

            return new Ext.Net.MVC.PartialViewResult { ViewName = "ListDeliveriesDetails", Model = mclass };
        }

        public ActionResult OnRemoveDelivery(string ItemDelivery)
        {

            try
            {

                Livraison mClass = JSON.Deserialize<Livraison>(ItemDelivery, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Store mstore = X.GetCmp<Store>("storeDetailDeliveriesListe");



                Guid prixNegocieAgence = Guid.Parse(X.GetCmp<TextField>("TxtPrixNegocieAgenceID").Text);
                Guid prixnegocielivraison = Guid.Parse(X.GetCmp<TextField>("TxtSpotPriceDeliverieID").Text);

                mClass.fnGetBySpotPrice(prixNegocieAgence, mClass.ID);

                if (mClass.IsNew)
                {
                    ModelProxy _proxy = mstore.GetById(mClass.ID);
                    _proxy.Drop();

                    return this.Direct();
                }

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("onApproveDailyPrice : Prix Journalier Approve failed.");

                PrixNegocieLivraison itemPN = new PrixNegocieLivraison();
                itemPN.RowVersionKey = mClass.RowVersionKey;

                bool result = itemPN.fnRemoveDelivery(mClass.ID);

                if (result)
                {
                    itemPN = new PrixNegocieLivraison();
                    itemPN.fnGet(prixnegocielivraison);


                    Store storePrixNegocieAgence = X.GetCmp<Store>("storeListePrixNegocieAgence");

                    ModelProxy ProxyPrixNegocieAgence = storePrixNegocieAgence.GetById(itemPN.ID);

                    ProxyPrixNegocieAgence.BeginEdit();
                    ProxyPrixNegocieAgence.Set(itemPN);
                    ProxyPrixNegocieAgence.Commit();
                    ProxyPrixNegocieAgence.EndEdit();

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.Drop();

                    mProxy.Set(mClass);

                    mProxy.Commit();


                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prix Negocié : Retirer",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult LoadListDeliveryByPrixNegocieAgence(string ItemFournisseur)
        {
            var mListe = (new Livraison()).fnSelectForSpotPrice(int.Parse(ItemFournisseur));
            return this.Store(mListe);
        }

        public ActionResult LoadListPendngDelivery()
        {
            var mListe = (new Livraison()).fnSelectPendingDeliveries();
            return this.Store(mListe);
        }

        public ActionResult OnShowPendingDeliveries()
        {
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Mask();

            PrixNegocieAgenceViewModel mclass = new PrixNegocieAgenceViewModel();
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
            mclass._PrixNegocieAgence = new PrixNegocieAgence();

            mclass._PrixNegocieAgence.Fournisseur = new Fournisseur();
            //mclass._PrixNegocie.Fournisseur.ID = int.Parse(ItemFournisseur);

            return new Ext.Net.MVC.PartialViewResult { ViewName = "ListPendingDeliveries", Model = mclass };
        }

        public ActionResult SubmitListDeliveries(string ItemSelected, string ItemExecMode)
        {
            if (ItemExecMode != Tms.Components.Settings.EnumsDefinition.CONSULT)
            {

                List<Livraison> livraisons = JSON.Deserialize<List<Livraison>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Store store = X.GetCmp<Store>("storeDetailDeliveriesListe");

                foreach (var item in livraisons)
                {
                    item.IsNew = true;
                    store.Insert(0, item);
                }

                X.GetCmp<Window>("ListDeliveriesDetails").Close();
            }
            else
            {
                X.GetCmp<Window>("ListDeliveriesDetails").Close();
                Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                mViewport.Unmask();
            }
            return this.Direct();
        }

        private PrixNegocieAgence MapFormToObject(PrixNegocieAgence mClass)
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

            mClass.DateDebut = mode == 1 ? DateTime.Parse(X.GetCmp<DateField>("TxtStartDate").RawText.ToString()) : (DateTime?)null;
            mClass.DateEcheance = mode == 1 ? DateTime.Parse(X.GetCmp<DateField>("TxtDueDate").RawText.ToString()).AddHours(23).AddMinutes(59) : (DateTime?)null;

            mClass.Site = new Site();
            mClass.Site.ID = int.Parse(X.GetCmp<Hidden>("hiddenDefaultSite").Text);
            mClass.Commentaire = X.GetCmp<TextArea>("TxtCommentaireSpotPrice").Text;
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            //mClass.DateDebut = DateTime.Parse(GetFormValue("hiddenNumeroPrice"));
            return mClass;
        }

        private void MapObjectToForm(PrixNegocie mClass)
        {
            X.GetCmp<ComboBox>("FournisseurID").SetValue(mClass.Fournisseur.ID.ToString());
            X.GetCmp<TextField>("TxtPriceDate").Text = mClass.DatePrix.ToString();
            X.GetCmp<TextField>("TxtPrice").Text = mClass.Prix.ToString();
            X.GetCmp<DateField>("TxtStartDate").Text = mClass.DateDebut.ToString();
            X.GetCmp<DateField>("TxtDueDate").Text = mClass.DateEcheance.ToString();
            X.GetCmp<ComboBox>("ModeApplicationID").SetValue(mClass.ModeApplication.ID.ToString());

            X.GetCmp<TextField>("hiddenNumeroPrice").Text = mClass.Numero.ToString();

        }

        public ActionResult OnSelectModeApplication(string ItemFournisseur, string ItemMode)
        {
            Store mstore = X.GetCmp<Store>("storeDelivery");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur)
                            });

            return this.Direct();
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
            //X.Js.Call("App.rowSelectionListeSpotPrice.deselectAll();");
            X.GetCmp<RowSelectionModel>("rowSelectionListePrixNegocieAgence").DeselectAll();
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

        public ActionResult OnDisplaySpotPriceList()
        {
            ViewData["Titre"] = "Liste des Prix Negociés";
            ViewData["actionToDo"] = "mnuPrintPrixNegocieAgenceList";
            ViewData["ControllerName"] = "PrixNegocieAgence";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmPeriodWithSupplierForReport", ViewData = ViewData };
        }

        public ActionResult mnuPrintPrixNegocieAgenceList(string fournisseur, string fournisseurText, string startDate, string endDate)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                //Session["paramCropYear"] = cropYear;                
                Session["paramFournisseur"] = fournisseur;
                Session["paramFournisseurText"] = fournisseurText;
                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/SpotPrice/ViewReportResult', this, 'List Of Prix Negociés',''),App.frmPeriodWithSupplierForReport.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prix Negocié : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }


        }

        //public ActionResult ViewReportResult()
        //{

        //    rptSpotPriceList report = new rptSpotPriceList();
        //    report.DataSource = DevExpressReportDs.SetDataSource(report);
        //    report.Parameters["paramFournisseur"].Value = Session["paramFournisseur"];
        //    report.Parameters["paramFournisseurText"].Value = Session["paramFournisseurText"];
        //    report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];
        //    report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

        //    ViewData["Report"] = report;

        //    return View("ViewReportResult");
        //}

    }
}