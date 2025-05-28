using DevExpress.XtraReports.UI;
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
    public class MouvementStockAgenceController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        // GET: MouvementStock
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);
            string campagne = mParam.Campagne;
            int ExportateurID = mParam.Exportateur.ID;

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            X.GetCmp<ComboBox>("cmbFiltreCampagne").SetValue(campagne);
            //X.GetCmp<ComboBox>("cmbFiltreExportateur").SetValue(ExportateurID);
            X.GetCmp<ComboBox>("cmbFiltreEmplacement").SetValue(mParam.Emplacement.ID);

            string StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
            //string StartDate = DateTime.Now.AddDays(-60).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("dtpFiltreStartDate").RawText = StartDate;
            X.GetCmp<DateField>("dtpFiltreEndDate").RawText = EndDate;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.BaseUrl = mParam.Base_url;
            ViewBag.CurrentCampagne = mParam.Campagne;
            
            ViewData["Exportateur"] = mParam.Exportateur.ID;

            Magasin mMagasin = new Magasin();
            result = mMagasin.fnGetDefaultBySite(mSiteParDefaut.ID);
            ViewBag.MagasinParDefaut = mMagasin.ID;
            //X.GetCmp<FormPanel>("MouvementStockCP").SetTitle("Campagne : " + campagne + ", Magasin : " + mMagasin.Designation +  ", Dir : {Tous}, Type : {Tous}, Location : " + mParam.Emplacement.Designation);
            X.GetCmp<FormPanel>("MouvementStockCP").SetTitle("Campagne : " + campagne + ", Magasin : {Tous}, Dir : {Tous}, Type : {Tous}");

            #region Set Function's Access

            Fonction HasAccess = new Fonction();

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{9308c18c-26f6-48a5-a18c-1ce3824e2568}"), UserName);
            HashSet<Guid> mlisteFonctions = new HashSet<Guid>(mListe.Cast<Fonction>().Select(f => f.ID).ToList());
            if (mlisteFonctions.Contains(Guid.Parse("{1be0b0c0-51ec-484c-85d1-c49c28adaaf7}")))
                X.GetCmp<Button>("btnGenerateMovement").Enable();
            else
                X.GetCmp<Button>("btnGenerateMovement").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{d784b042-de55-4a4f-ba25-c35096e3a56b}")))
                X.GetCmp<MenuItem>("mnuPrintMovement").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintMovement").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{8605a3be-8046-4956-ab4a-11232baa6c18}")))
                X.GetCmp<MenuItem>("mnuExportMovement").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportMovement").Disable();

            if (mlisteFonctions.Contains(Guid.Parse("{293263e5-fbcc-43ae-9da1-5b2f5ddf4269}")))
                X.GetCmp<Hidden>("mvhiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("mvhiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Contains(Guid.Parse("{63a4f552-beb1-4f1b-a1d5-e5fbc5648794}")))
                X.GetCmp<Hidden>("mvhiddenPermChangePosition").SetValue(true);
            else
                X.GetCmp<Hidden>("mvhiddenPermChangePosition").SetValue(false);


            //if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{C1785226-126D-4648-95C6-92894707BC1E}")))
            //    X.GetCmp<Button>("btnGenerateMovement").Enable();
            //else
            //    X.GetCmp<Button>("btnGenerateMovement").Disable();

            //if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{DF2C998A-9232-48ED-B403-6DB46289BE0A}")))
            //    X.GetCmp<MenuItem>("mnuPrintMovement").Enable();
            //else
            //    X.GetCmp<MenuItem>("mnuPrintMovement").Disable();

            //if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{3CA6BD01-DFC1-4B9F-9E0E-134391DF8B4F}")))
            //    X.GetCmp<MenuItem>("mnuExportMovement").Enable();
            //else
            //    X.GetCmp<MenuItem>("mnuExportMovement").Disable();

            //if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{94E1648B-BB69-4470-8B5D-FA8D19598161}")))
            //    X.GetCmp<Hidden>("mvhiddenPermDesactiver").SetValue(true);
            //else
            //    X.GetCmp<Hidden>("mvhiddenPermDesactiver").SetValue(false);

            //if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{6A744734-6747-44B5-91BE-124B10603D27}")))
            //    X.GetCmp<Hidden>("mvhiddenPermChangePosition").SetValue(true);
            //else
            //    X.GetCmp<Hidden>("mvhiddenPermChangePosition").SetValue(false);
            #endregion
            //bool HavAccessMagasinTV = false;
            //if (mlisteFonctions.Contains(Guid.Parse("{c706bf26-10b4-49da-bd8b-2f23b551fe94}")))
            //    HavAccessMagasinTV = true;

            ViewData["LoadMagasin"] = "LoadActiveMagasin";
            //bool HavAccessMagasinExport = false;
            //if (mlisteFonctions.Contains(Guid.Parse("{6866AFF0-DB9E-4FA7-AF90-C54B871E689F}")))
            //    HavAccessMagasinExport = true;

            //if (HavAccessMagasinTV == true)
            //{
            //    ViewData["LoadMagasin"] = "LoadOnlyMagasinTV";
            //    ViewData["magasinID"] = mParam.MagasinTV;
            //}            
            //else {
            //    ViewData["LoadMagasin"] = "";
            //    ViewData["magasinID"] = "0";
            //}

            //var mliste = new Magasin().fnSelect(0);
            //List<Magasin> ListeMagasin = new List<Magasin>();
            //Magasin magasin = new Magasin();
            //foreach (Magasin item in mliste)
            //{
            //    magasin = new Magasin();
            //    magasin = item;
            //    if ((HavAccessMagasinTV && item.ID == mParam.MagasinTV) || (HavAccessMagasinExport && item.ID == mParam.MagasinExport))
            //        ListeMagasin.Add(magasin);
            //}

            //X.GetCmp<ComboBox>("cmbFiltreMagasin").SetValue(ListeMagasin[0].ID);


            return View();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("MouvementStockCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne, string ItemMagasin, string ItemExportateur, string ItemMouvementTypeID, string ItemSens, string ItemPeriodStart, string ItemPeriodEnd, string ItemCertification, string ItemEmplacement, string ItemStatut, string ItemSite)
        {
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Toute}" : ItemCampagne;
            int MagasinID = GetCriteriaValue(ItemMagasin) == -1 ? -1 : GetCriteriaValue(ItemMagasin);
            int ExportateurID = GetCriteriaValue(ItemExportateur);
            int MouvementTypeID = GetCriteriaValue(ItemMouvementTypeID) == -1 ? -2 : GetCriteriaValue(ItemMouvementTypeID);
            int EmplacementID = GetCriteriaValue(ItemEmplacement);
            int SiteID = GetCriteriaValue(ItemSite);
            if (EmplacementID == -1) EmplacementID = 1;

            int Sens = string.IsNullOrEmpty(ItemSens) ? -2 : int.Parse(ItemSens);
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            int certificationID = GetCriteriaValue(ItemCertification);
            ItemStatut = string.IsNullOrEmpty(ItemStatut) ? "-1" : ItemStatut;
            var mListe = (new MouvementStock()).fnSelect(ItemCampagne, MagasinID, ExportateurID, StartDate, EndDate, Sens, MouvementTypeID, certificationID, EmplacementID, ItemStatut,SiteID);

            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemMagasin, string ItemExportateur, string ItemMouvementTypeID, string ItemSens, string ItemPeriodStart, string ItemPeriodEnd, string ItemCertification, string ItemEmplacement, string ItemStatut, string ItemSite)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeMouvementStock");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCampagne",ItemCampagne),
                                    new Ext.Net.Parameter("ItemSite",ItemSite),
                                    new Ext.Net.Parameter("ItemMagasin",ItemMagasin),
                                    new Ext.Net.Parameter("ItemExportateur",ItemExportateur),
                                    new Ext.Net.Parameter("ItemMouvementTypeID",ItemMouvementTypeID),
                                    new Ext.Net.Parameter("ItemSens",ItemSens),
                                    new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemCertification",ItemCertification),
                                    new Ext.Net.Parameter("ItemEmplacement",ItemEmplacement),
                                    new Ext.Net.Parameter("ItemStatut",ItemStatut)
                                });

                //string title = X.GetCmp<FormPanel>("MouvementStockCP").Title;

                //title += "Campagne : " + X.GetCmp<ComboBox>("cmbFiltreCampagne").SelectedItem.Text;

                //title += ", Magasin = " + X.GetCmp<ComboBox>("cmbFiltreMagasin").SelectedItem.Text;

                //title += ", Exportateur = " + X.GetCmp<ComboBox>("cmbFiltreExportateur").SelectedItem.Text;

                //title += ", Type = " + X.GetCmp<ComboBox>("cmbFiltreMouvementType").SelectedItem.Text;

                //title += ", Statut : " + X.GetCmp<ComboBox>("cmbFiltreStatut").SelectedItem.Text;

                //title += ", Direction = " + X.GetCmp<ComboBox>("cmbFiltreDirection").SelectedItem.Text;

                //title += ", Certification = " + X.GetCmp<ComboBox>("cmbFiltreCertification").SelectedItem.Text;

                //title += ", From " + X.GetCmp<DateField>("dtpFiltreStartDate").RawText.ToString() + "To " + X.GetCmp<DateField>("dtpFiltreEndDate").RawText.ToString();

                //X.GetCmp<FormPanel>("MouvementStockCP").Title = title;

                X.GetCmp<FormPanel>("MouvementStockCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stock Transaction : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SelectPending(StoreRequestParameters parameters, string ItemCampagne, string ItemMagasin, string ItemExportateur, string ItemMouvementTypeID, string ItemSens, string ItemCertification)
        {
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "-1" : ItemCampagne;
            int MagasinID = GetCriteriaValue(ItemMagasin);
            int ExportateurID = GetCriteriaValue(ItemExportateur);
            int MouvementTypeID = GetCriteriaValue(ItemMouvementTypeID);
            int CertificationID = GetCriteriaValue(ItemCertification);
            int Sens = string.IsNullOrEmpty(ItemSens) ? -2 : int.Parse(ItemSens);
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "";
            }

            //DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            //DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());            

            var mListe = (new MouvementStockAgence()).fnSelectPendingForSite(ItemCampagne, MagasinID, ExportateurID, Sens, MouvementTypeID, CertificationID);

            return this.Store(mListe);
        }

        public ActionResult OnRefreshPending(string ItemCampagne, string ItemMagasin, string ItemExportateur, string ItemMouvementTypeID, string ItemSens, string ItemCertification)
        {
            try
            {
                //DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                //DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListPendingMouvement");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCampagne",ItemCampagne),
                                    new Ext.Net.Parameter("ItemMagasin",ItemMagasin),
                                    new Ext.Net.Parameter("ItemExportateur",ItemExportateur),
                                    new Ext.Net.Parameter("ItemMouvementTypeID",ItemMouvementTypeID),
                                    new Ext.Net.Parameter("ItemSens",ItemSens),
                                    //new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    //new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemCertification",ItemCertification)
                                });

                //string title = X.GetCmp<FormPanel>("MouvementStockCP").Title;

                //title += "Campagne : " + X.GetCmp<ComboBox>("cmbFiltreCampagne").SelectedItem.Text;

                //title += ", Magasin = " + X.GetCmp<ComboBox>("cmbFiltreMagasin").SelectedItem.Text;

                //title += ", Exportateur = " + X.GetCmp<ComboBox>("cmbFiltreExportateur").SelectedItem.Text;

                //title += ", Type = " + X.GetCmp<ComboBox>("cmbFiltreMouvementType").SelectedItem.Text;

                //title += ", Statut : " + X.GetCmp<ComboBox>("cmbFiltreStatut").SelectedItem.Text;

                //title += ", Direction = " + X.GetCmp<ComboBox>("cmbFiltreDirection").SelectedItem.Text;

                //title += ", From " + X.GetCmp<DateField>("dtpFiltreStartDate").RawText.ToString() + "To " + X.GetCmp<DateField>("dtpFiltreEndDate").RawText.ToString();

                //X.GetCmp<FormPanel>("MouvementStockCP").Title = title;

                //X.GetCmp<FormPanel>("PendingMouvementCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stock Transaction : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult onAdd()
        {
            MouvementStockViewModel mclass = new MouvementStockViewModel();

            mclass._MouvementStock = new MouvementStock();
            mclass._DefaultCampagne = (new Parametres(0)).Campagne;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMouvementStock", Model = mclass, };

        }

        public ActionResult onEdit(string ItemSelected)
        {
            MouvementStockViewModel mclass = new MouvementStockViewModel();

            mclass._MouvementStock = new MouvementStock();
            mclass._DefaultCampagne = (new Parametres(0)).Campagne;

            mclass._MouvementStock = JSON.Deserialize<MouvementStock>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMouvementStock", Model = mclass };

        }

        public ActionResult onSelectPosition(string magasinID = "", string magasinText = "")
        {
            try
            {
                int idMagasin = 0;
                bool isInt = int.TryParse(magasinID, out idMagasin);

                if (isInt == false)
                    return this.Direct();

                ViewData["MagasinID"] = idMagasin;
                ViewData["magasinText"] = magasinText;
                return new Ext.Net.MVC.PartialViewResult { ViewName = "Mouvement_SelectEmplacement", ViewData = ViewData };
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stock Transaction : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }

        }

        public ActionResult onChangePosition(string magasin = "")
        {
            //ViewData["Magasin"] = magasin;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Mouvement_ChangeEmplacement" };
        }

        public ActionResult onApprove()
        {
            MouvementStockAgence mclass = new MouvementStockAgence();
            
            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            Magasin mMagasin = new Magasin();
            result = mMagasin.fnGetDefaultBySite(mSiteParDefaut.ID);
            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;
            ViewData["MagasinParDefaut"] = mMagasin.ID;

            //Fonction HasAccess = new Fonction();
            //Parametres mParam = new Parametres(0);

            //bool HavAccessMagasinTV = HasAccess.fnGetUserAccessStatus("{c706bf26-10b4-49da-bd8b-2f23b551fe94}", UserName);
            //bool HavAccessMagasinExport = HasAccess.fnGetUserAccessStatus("{6866AFF0-DB9E-4FA7-AF90-C54B871E689F}", UserName);

            ViewData["LoadMagasin"] = "LoadActiveMagasin";
            //if (HavAccessMagasinTV == true)
            //{
            //    ViewData["LoadMagasin"] = "LoadOnlyMagasinTV";
            //    ViewData["magasinID"] = mParam.MagasinTV;
            //}            
            //else {
            //    ViewData["LoadMagasin"] = "";
            //    ViewData["magasinID"] = "0";
            //}
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormApproveMouvement", Model = mclass, ViewData = ViewData };
        }

        [HttpPost]
        public ActionResult UpdateFormMethod()
        {
            try
            {
                MouvementStock mClass = new MouvementStock();
                bool result = true;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("txtMouvementID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("UpdateFormMethod : Stock Transaction - load failed.");


                    if (!string.IsNullOrEmpty(mClass.Approbateur) || mClass.DateApprobation != null)
                        throw new Exception("UpdateFormMethod : Stock Transaction Already approved ! Please Refresh Overview");
                }

                mClass = MapFormToObject(mClass);

                result = mClass.fnUpdate();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListeMouvementStock");
                    GridPanel mGrid = X.GetCmp<GridPanel>("grpListeMouvementStock");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowMouvementStock").Select(0);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    CloseDetailWindow();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stock Transaction : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnCancel(string ItemSelected)
        {
            try
            {
                MouvementStock mouvementStock = JSON.Deserialize<MouvementStock>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = mouvementStock.fnGet(mouvementStock.ID);

                if (!result)
                    throw new Exception("OnCancel : Stock Transaction loading failed.");

                mouvementStock.UtilisateurModification = (string)Session["userName"];

                if (mouvementStock.Desactive)
                    result = mouvementStock.fnActivate();
                else
                    result = mouvementStock.fnDeActivate();

                if (!result)
                    throw new Exception("OnCancel : Stock Transaction, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeMouvementStock");

                    ModelProxy mProxy = mstore.GetById(mouvementStock.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mouvementStock);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stock Transaction : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult ChangePosition(string ItemSelected = "")
        {
            try
            {
                MouvementStock mouvementStock = JSON.Deserialize<MouvementStock>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = mouvementStock.fnGet(mouvementStock.ID);

                if (!result)
                    throw new Exception("OnCancel : Stock Transaction loading failed.");

                mouvementStock.UtilisateurModification = (string)Session["userName"];
                mouvementStock.Commentaire = X.GetCmp<TextArea>("txtCommentaire").Text;
                int EmplacementID = int.Parse(X.GetCmp<ComboBox>("cmbMouvementEmplacement").SelectedItem.Value);

                mouvementStock.Emplacement = new Emplacement();
                mouvementStock.Emplacement.ID = EmplacementID;
                mouvementStock.Emplacement.Designation = X.GetCmp<ComboBox>("cmbMouvementEmplacement").SelectedItem.Text;

                result = mouvementStock.fnChangeLocation();

                if (!result)
                    throw new Exception("OnCancel : Stock Transaction, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeMouvementStock");

                    ModelProxy mProxy = mstore.GetById(mouvementStock.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mouvementStock);

                    mProxy.Commit();

                    mProxy.EndEdit();

                    X.GetCmp<Window>("Mouvement_ChangeEmplacement").Close();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stock Transaction : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult GenerateMouvement(string ItemSelected, string ItemMagasin = "", string ItemEmplacement = "", string ItemEmplacementText = "")
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();
            List<MouvementStock> mListeMouvements = new List<MouvementStock>();
            try
            {
                int emplacementID = 0;
                int magasinID = 0;
                if (!string.IsNullOrEmpty(ItemEmplacement))
                    emplacementID = int.Parse(ItemEmplacement);
                else
                    throw new Exception("Generate Movement : Position not found");

                if (!string.IsNullOrEmpty(ItemMagasin))
                    magasinID = int.Parse(ItemMagasin);
                else
                    throw new Exception("Generate Movement : Magasin not found");

                MouvementStock mouvement = new MouvementStock();

                bool resulttransact = true;

                _db = mouvement.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                List<MouvementStock> ItemMouvement = JSON.Deserialize<List<MouvementStock>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                if (ItemMouvement.Count > 0)
                {
                    foreach (var item in ItemMouvement)
                    {
                        mouvement = new MouvementStock();
                        mouvement.Magasin = new Magasin();
                        mouvement.mCampagne = new Campagne();
                        mouvement.Exportateur = new Exportateur();
                        mouvement.MouvementStockType = new MouvementStockType();
                        mouvement.Certification = new Certification();
                        mouvement.Emplacement = new Emplacement();
                        mouvement.SacType = new SacType();
                        mouvement.SetDataSource(_db);

                        mouvement.Magasin.ID = magasinID;
                        mouvement.Emplacement.ID = emplacementID;
                        mouvement.Emplacement.Designation = ItemEmplacementText;
                        mouvement.Magasin.Designation = item.Magasin.Designation;
                        mouvement.mCampagne.Designation = item.mCampagne.Designation;
                        mouvement.Exportateur.ID = item.Exportateur.ID;
                        mouvement.Exportateur.Nom = item.Exportateur.Nom;
                        mouvement.MouvementStockType.ID = item.MouvementStockType.ID;
                        mouvement.MouvementStockType.Designation = item.MouvementStockType.Designation;
                        mouvement.ObjetEnStock = item.ObjetEnStock;
                        mouvement.ObjetEnStockType = item.ObjetEnStockType;
                        mouvement.DateMouvement = DateTime.Now;
                        mouvement.Reference1 = item.Reference1;
                        mouvement.Reference2 = item.Reference2;
                        mouvement.Reference3 = item.Reference3;
                        mouvement.Sens = item.Sens;
                        if (item.Certification != null)
                        {
                            mouvement.Certification.ID = item.Certification.ID;
                            mouvement.Certification.Designation = item.Certification.Designation;
                        }
                        else
                            mouvement.Certification = null;

                        mouvement.SacType.ID = item.SacType.ID;
                        mouvement.SacType.Designation = item.SacType.Designation;
                        mouvement.Quantite = item.Quantite;
                        mouvement.PoidsBrut = item.PoidsBrut;
                        mouvement.TareSacs = item.TareSacs;
                        mouvement.TarePalettes = item.TarePalettes;
                        mouvement.PoidsNetLivre = item.PoidsNetLivre;
                        mouvement.Retention = item.Retention;
                        mouvement.PoidsNetAccepte = item.PoidsNetAccepte;
                        mouvement.Statut = "AP";
                        mouvement.Commentaire = "Generated";
                        mouvement.UtilisateurCreation = (string)Session["userName"];

                        resulttransact = mouvement.fnUpdate(mtran);
                        if (!resulttransact) break;
                        mListeMouvements.Add(mouvement);
                    }
                    if (!resulttransact) _db.RollBackTransaction(mtran);
                    _db.CommitTransaction(mtran);
                }

                if (resulttransact)
                {

                    Store mstore = X.GetCmp<Store>("storeListeMouvementStock");
                    mstore.Insert(0, mListeMouvements);
                    //X.GetCmp<RowSelectionModel>("rowMouvementStock").Select(0);
                    X.GetCmp<Window>("Mouvement_SelectEmplacement").Close();
                    X.GetCmp<Window>("FormApproveMouvement").Close();
                }
            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mtran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stock Transaction - Generate Movement : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        private MouvementStock MapFormToObject(MouvementStock mClass)
        {
            Magasin magasin = new Magasin();
            magasin.ID = int.Parse(GetFormValue("cmbMagasin"));
            magasin.Designation = X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Text.ToString();
            mClass.Magasin = magasin;

            Campagne campagne = new Campagne();
            campagne.Designation = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text.ToString();
            mClass.mCampagne = campagne;

            Exportateur exportateur = new Exportateur();
            exportateur.ID = int.Parse(GetFormValue("cmbExportateur"));
            exportateur.Nom = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text.ToString();
            mClass.Exportateur = exportateur;

            mClass.DateMouvement = DateTime.Parse(X.GetCmp<DateField>("txtDateMouvement").RawText.ToString());
            mClass.Sens = Int16.Parse(X.GetCmp<ComboBox>("cmbSens").SelectedItem.Value.ToString());

            MouvementStockType mouvementType = new MouvementStockType();
            mouvementType.ID = int.Parse(GetFormValue("cmbMouvementType"));
            mouvementType.Designation = X.GetCmp<ComboBox>("cmbMouvementType").SelectedItem.Text.ToString();
            mClass.MouvementStockType = mouvementType;

            mClass.Reference1 = X.GetCmp<TextField>("txtNumeroBordereau").Text;
            mClass.Reference2 = X.GetCmp<TextField>("txtReferenceMouvement").Text;

            SacType sacType = new SacType();
            sacType.ID = int.Parse(GetFormValue("cmbSacType"));
            sacType.Designation = X.GetCmp<ComboBox>("cmbSacType").SelectedItem.Text.ToString();
            mClass.SacType = sacType;

            if (X.GetCmp<TextField>("txtQuantite").Text != string.Empty) mClass.Quantite = decimal.Parse(X.GetCmp<TextField>("txtQuantite").Text);
            if (X.GetCmp<TextField>("txtPoidsBrut").Text != string.Empty) mClass.PoidsBrut = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrut").Text);
            if (X.GetCmp<TextField>("txtTareSacs").Text != string.Empty) mClass.TareSacs = decimal.Parse(X.GetCmp<TextField>("txtTareSacs").Text);
            if (X.GetCmp<TextField>("txtTarePalette").Text != string.Empty) mClass.TarePalettes = decimal.Parse(X.GetCmp<TextField>("txtTarePalette").Text);
            if (X.GetCmp<TextField>("txtPoidsNetLivre").Text != string.Empty) mClass.PoidsNetLivre = decimal.Parse(X.GetCmp<TextField>("txtPoidsNetLivre").Text);
            if (X.GetCmp<TextField>("txtRetention").Text != string.Empty) mClass.Retention = decimal.Parse(X.GetCmp<TextField>("txtRetention").Text);
            if (X.GetCmp<TextField>("txtPoidsNetAccepte").Text != string.Empty) mClass.PoidsNetAccepte = decimal.Parse(X.GetCmp<TextField>("txtPoidsNetAccepte").Text);

            mClass.Commentaire = X.GetCmp<TextField>("txtComment").Text;
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }

        public ActionResult OnPrintList()
        {
            Parametres mParam = new Parametres(0);
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            Site mSiteParDefaut = new Site();
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{9308c18c-26f6-48a5-a18c-1ce3824e2568}"), UserName);
            HashSet<Guid> mlisteFonctions = new HashSet<Guid>(mListe.Cast<Fonction>().Select(f => f.ID).ToList());

            Magasin mMagasin = new Magasin();
            result = mMagasin.fnGetDefaultBySite(mSiteParDefaut.ID);

            ViewData["LoadMagasin"] = "LoadActiveMagasin";
            ViewData["magasinID"] = mMagasin.ID;
            //bool HavAccessMagasinTV = false;
            //if (mlisteFonctions.Contains(Guid.Parse("{c706bf26-10b4-49da-bd8b-2f23b551fe94}")))
            //    HavAccessMagasinTV = true;

            //bool HavAccessMagasinExport = false;
            //if (mlisteFonctions.Contains(Guid.Parse("{6866AFF0-DB9E-4FA7-AF90-C54B871E689F}")))
            //    HavAccessMagasinExport = true;

            //if (HavAccessMagasinTV == true)
            //{
            //    ViewData["LoadMagasin"] = "LoadOnlyMagasinTV";
            //    ViewData["magasinID"] = mParam.MagasinTV;
            //}            
            //else {
            //    ViewData["LoadMagasin"] = "";
            //    ViewData["magasinID"] = "0";
            //}

            ViewData["Campagne"] = mParam.Campagne;
            ViewData["Exportateur"] = mParam.Exportateur.ID;

            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;
            if (mParam.Site == mSiteParDefaut.ID) ViewData["UrlSite"] = "LoadSiteAll";
            else ViewData["UrlSite"] = "LoadSiteByAccess";

            return new Ext.Net.MVC.PartialViewResult { ViewName = "MouvementStock_Print", ViewData = ViewData };
        }


        public ActionResult PrintList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;

                report = new rptStockTransactionList() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["campagneID"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                report.Parameters["paramExportateur"].Value = int.Parse(X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Value);
                report.Parameters["paramExportateurNom"].Value = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text;

                report.Parameters["paramCertification"].Value = int.Parse(X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Value);
                report.Parameters["paramCertificationText"].Value = X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Text;

                report.Parameters["paramMagasin"].Value = int.Parse(X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Value);
                report.Parameters["paramMagasinText"].Value = X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Text;

                report.Parameters["paramDirection"].Value = int.Parse(X.GetCmp<ComboBox>("cmbDirection").SelectedItem.Value);
                report.Parameters["paramDirectionText"].Value = X.GetCmp<ComboBox>("cmbDirection").SelectedItem.Text;

                report.Parameters["paramMouvementType"].Value = int.Parse(X.GetCmp<ComboBox>("cmbMouvementType").SelectedItem.Value);
                report.Parameters["paramMouvementTypeText"].Value = X.GetCmp<ComboBox>("cmbMouvementType").SelectedItem.Text;

                report.Parameters["paramPosition"].Value = int.Parse(X.GetCmp<ComboBox>("cmbEmplacement").SelectedItem.Value);
                report.Parameters["paramPositionText"].Value = X.GetCmp<ComboBox>("cmbEmplacement").SelectedItem.Text;

                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("dtpStartDate").RawText);
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("dtpEndDate").RawText);

                report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("CmbStatut").SelectedItem.Value.ToString();
                report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("CmbStatut").SelectedItem.Text;

                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/MouvementStock/ViewList', this, 'List Of stock transaction',''),App.MouvementStock_Print.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stock Transaction : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
        }

        public ActionResult ViewList()
        {
            XtraReport report = null;
            report = Session["report"] as XtraReport;
            //report = new rptLotsList() as XtraReport;

            //report.DataSource = DevExpressReportDs.SetDataSource(report);
            //report.Parameters["campagneID"].Value = Session["CampagneID"];
            //report.Parameters["paramCampagneText"].Value = Session["paramCampagneText"];

            //report.Parameters["paramExportateur"].Value = int.Parse(Session["paramExportateur"].ToString());
            //report.Parameters["paramExportateurNom"].Value = Session["paramExportateurNom"];

            //report.Parameters["paramCertification"].Value = int.Parse(Session["paramCertification"].ToString());
            //report.Parameters["paramCertificationText"].Value = Session["paramCertificationText"];

            //report.Parameters["paramLotType"].Value = int.Parse(Session["paramLotType"].ToString());
            //report.Parameters["paramLotTypeText"].Value = Session["paramLotTypeText"];

            //report.Parameters["paramDateDebut"].Value = DateTime.Parse(Session["paramDateDebut"].ToString());
            //report.Parameters["paramDateFin"].Value = DateTime.Parse(Session["paramDateFin"].ToString());

            //report.Parameters["paramStatut"].Value = int.Parse(Session["paramStatut"].ToString());
            //report.Parameters["paramStatutText"].Value = Session["paramStatutText"];            

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }


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

        private void CloseDetailWindow()
        {
            X.GetCmp<Window>("FormMouvementStock").Close();
        }

        private int GetCriteriaValue(string strComponent)
        {
            int value;

            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
                return value;
            else
                return -1;
        }


        private int GetCriteriasValue(string strComponent)
        {
            if (X.GetCmp<ComboBox>(strComponent) != null && X.GetCmp<ComboBox>(strComponent).SelectedItem != null && X.GetCmp<ComboBox>(strComponent).SelectedItem.Value != null)
                return int.Parse(X.GetCmp<ComboBox>(strComponent).SelectedItem.Value.ToString());
            else
                return -1;
        }
    }
}