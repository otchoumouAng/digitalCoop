using DevExpress.XtraReports.UI;
using DigitalCoops.Cocoa.Models;
using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Business;
using Tms.Classes.Business.stock;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;
using Tms2017.MVC.Controllers;

namespace Cooperative.Controllers
{
    public class Lot_GestionStockController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        private const string ApiUrl = "https://tnci-api.touton.com:2104/";
        // Hardcoded API token to access the external API.
        private const string ApiToken = "1F52C417-D568-4EE3-BE43-3F76F0E9BB56";

        // GET: Lot_GestionStock
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);
            string campagne = mParam.Campagne;
            //int produit = mParam.Produit.ID;
            X.GetCmp<ComboBox>("cmbFiltreCampagne").SetValue(campagne);
            //X.GetCmp<ComboBox>("cmbFiltreProduit").SetValue(produit);

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("txtFiltreDateDebut").RawText = StartDate;
            X.GetCmp<DateField>("txtFiltreDateFin").RawText = EndDate;

            X.GetCmp<FormPanel>("Lot_GestionStockCP").SetTitle("Campagne : " + campagne + ", Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{34caff7a-0f0d-40ee-92b3-9de7f7d50b39}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{dd88803b-5f9c-41f2-881c-91ecedc61708}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            //if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{F663846F-C832-402D-9B85-F6EB1E6132EF}")))
            //    X.GetCmp<MenuItem>("mnuPrintLot_GestionStock").Enable();
            //else
            //    X.GetCmp<MenuItem>("mnuPrintLot_GestionStock").Disable();

            //if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{EC36A28C-B082-44E9-B3A3-69ED9F719E86}")))
            //    X.GetCmp<MenuItem>("mnuExportLot_GestionStock").Enable();
            //else
            //    X.GetCmp<MenuItem>("mnuExportLot_GestionStock").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{561c911e-d54b-4d52-8cb6-fbc52d1e49e7}")))
                X.GetCmp<Hidden>("lthiddenPermModify").SetValue(true);
            else
                X.GetCmp<Hidden>("lthiddenPermModify").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{ace59756-d945-4d53-92b1-49e1ee69520c}")))
                X.GetCmp<Hidden>("lthiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("lthiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{30ee1bab-7d3c-4fcb-88a9-628db185f061}")))
                X.GetCmp<Hidden>("lthiddenPermApprove").SetValue(true);
            else
                X.GetCmp<Hidden>("lthiddenPermApprove").SetValue(false);

            #endregion

            return View();
        }

        [HttpPost]
        public ActionResult LoadLot_GestionStockByNumero(string query)
        {
            try
            {
                Lot_GestionStock mLot = new Lot_GestionStock();
                List<DataPersist> mliste = new Lot_GestionStock().fnSelectByNumero(query);

                //if (mliste.Count > 0)
                //    mLot_GestionStock = mliste[0] as Lot_GestionStock;

                return this.Store(mliste);
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult LoadLot_GestionStockForReCleaning(string query)
        {
            try
            {
                Lot_GestionStock mLot_ = new Lot_GestionStock();
                List<DataPersist> mliste = new Lot_GestionStock().fnSelectForReCleaning(query);

                //if (mliste.Count > 0)
                //    mLot_GestionStock = mliste[0] as Lot_GestionStock;

                return this.Store(mliste);
            }
            catch (Exception Ex)
            {
                throw;
            }

        }

        private void DeselectGridRows()
        {
            //X.Js.Call("App.rowSelectionListe.deselectAll();");
            X.GetCmp<RowSelectionModel>("rowSelectionListe").DeselectAll();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("Lot_GestionStockCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne, string ItemProduit, string ItemType, string ItemMagasin, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatut, string ItemExportateur)
        {
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "-1" : ItemCampagne;
            int MagasinID = GetCriteriaValue(ItemMagasin);
            int ExportateurID = GetCriteriaValue(ItemExportateur);
            int TypeID = GetCriteriaValue(ItemType);
            int ProduitID = GetCriteriaValue(ItemProduit);
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string Statut = string.IsNullOrEmpty(ItemStatut) ? "-1" : ItemStatut;
            var mListe = (new Lot_GestionStock()).fnSelect(Campagne, ProduitID, MagasinID, TypeID, StartDate, EndDate, Statut, ExportateurID);

            return this.Store(mListe);
        }


        public ActionResult OnRefresh(string ItemCampagne, string ItemProduit, string ItemType, string ItemMagasin, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatut)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeLot_GestionStock");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCampagne",ItemCampagne),
                                    new Ext.Net.Parameter("ItemProduit",ItemProduit),
                                    new Ext.Net.Parameter("ItemType",ItemType),
                                    new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemMagasin",ItemMagasin),
                                    new Ext.Net.Parameter("ItemStatut",ItemStatut)
                                });

                X.GetCmp<FormPanel>("Lot_GestionStockCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - Lot_GestionStock : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult onAdd()
        {
            string UserName = (string)Session["userName"];
            Lot_GestionStockViewModel mclass = new Lot_GestionStockViewModel();
            Parametres mParam = new Parametres(0);
            mclass._Lot_GestionStock = new Lot_GestionStock();
            mclass._DefaultCampagne = mParam.Campagne;
            Site mSiteParDefaut = new Site();
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            mclass._Lot_GestionStock.Magasin = new Magasin();
            mclass._Lot_GestionStock.Magasin.ID = mSiteParDefaut.MagasinID;
            //mclass._DefaultExportateur = mParam.Exportateur.ID;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLot_GestionStock", Model = mclass, };
        }

        public ActionResult onImport()
        {
            //return new Ext.Net.MVC.PartialViewResult { ViewName = "FormImport_Lot" };
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormImport_Lot" };
        }

        public ActionResult onEdit(string ItemSelected)
        {
            Lot_GestionStockViewModel mclass = new Lot_GestionStockViewModel();

            mclass._Lot_GestionStock = new Lot_GestionStock();
            Parametres mParam = new Parametres(0);
            mclass._DefaultCampagne = mParam.Campagne;
            //mclass._DefaultExportateur = mParam.Exportateur.ID;

            mclass._Lot_GestionStock = JSON.Deserialize<Lot_GestionStock>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLot_GestionStock", Model = mclass, };
        }

        public ActionResult onConsult(string ItemSelected)
        {
            Lot_GestionStockViewModel mclass = new Lot_GestionStockViewModel();

            mclass._Lot_GestionStock = new Lot_GestionStock();
            Parametres mParam = new Parametres(0);
            mclass._DefaultCampagne = mParam.Campagne;
            //mclass._DefaultExportateur = mParam.Exportateur.ID;

            mclass._Lot_GestionStock = JSON.Deserialize<Lot_GestionStock>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLot_GestionStock", Model = mclass, };
        }

        public ActionResult OnApprove(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();
            try
            {
                Lot_GestionStock mClass = JSON.Deserialize<Lot_GestionStock>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
                Guid mId = mClass.ID;
                bool result = mClass.fnGet(mId);
                bool resultMouvement = false;
                mClass.UtilisateurModification = (string)Session["userName"];

                _db = mClass.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = mClass.fnApprove(mtran);

                if (result)
                {
                    #region Mvt
                    MouvementStockAgence mouvement = new MouvementStockAgence();
                    mouvement = new MouvementStockAgence();
                    mouvement.mCampagne = new Campagne();
                    mouvement.Exportateur = new Exportateur();
                    mouvement.SacType = new SacType();
                    mouvement.TypeElementStock = new TypeElementStock();
                    mouvement.Certification = new Certification();
                    mouvement.Magasin = new Magasin();
                    mouvement.Emplacement = new Emplacement();
                    mouvement.MouvementStockType = new MouvementStockType();
                    mouvement.Sites = new Site();
                    InventaireElementStock Items = new InventaireElementStock();
                    Parametres mParam = new Parametres(0);
                    mouvement.SetDataSource(_db);
                    mouvement.mCampagne.Designation = mClass.Campagne.Designation;
                    mouvement.Exportateur.ID = mClass.Exportateur.ID;
                    mouvement.DateMouvement = DateTime.Now;
                    mouvement.SacType.ID = mParam.SacExportType;
                    mouvement.ObjetEnStock = mClass.ID;
                    mouvement.ObjetEnStockType = mParam.LotTypeElementEnStock;
                    //mouvement.MouvementStockType.ID = mParam.MvtTypeVenteLot;
                    mouvement.MouvementStockType.ID = 12;
                    mouvement.Sites.ID = mClass.Magasin.Sites.ID;
                    mouvement.Sites.Nom = mClass.Magasin.Sites.Nom;
                    mouvement.Certification = null;
                    //if (mClass.Livraison.Certification.ID != 0)
                    //{
                    //    mouvement.Certification = new Certification();
                    //    mouvement.Certification.ID = mClass.Certification.ID;
                    //}
                    mouvement.Sens = 1;
                    mouvement.Quantite = mClass.NombreSacs;
                    mouvement.PoidsBrut = mClass.PoidsBrut;
                    mouvement.TarePalettes = mClass.TarePalette;
                    mouvement.TareSacs = mClass.TareSacs;
                    //mouvement.TareSacs = mClass.TareSacs;
                    mouvement.PoidsNetLivre = mClass.PoidsBrut;
                    mouvement.PoidsNetAccepte = mClass.PoidsNet;
                    //mouvement.Retention = mClass.PoidsBrut - mClass.PoidsNet;
                    mouvement.Retention = 0;
                    mouvement.Magasin.ID = mClass.Magasin.ID;
                    mouvement.Emplacement.ID = mParam.EmplacementParDefaut;
                    mouvement.Reference1 = mClass.NumeroLot;
                    mouvement.Reference2 = mClass.NumeroLot;
                    mouvement.Commentaire = "generé automatiquement";
                    mouvement.Statut = "AP";
                    mouvement.UtilisateurCreation = (string)Session["userName"];
                    mouvement.UtilisateurModification = (string)Session["userName"];

                    resultMouvement = mouvement.fnUpdate(mtran);
                }
                else
                {
                    _db.RollBackTransaction(mtran);
                }

                if (!resultMouvement)
                    _db.RollBackTransaction(mtran);
                #endregion

                if (result)
                {
                    _db.CommitTransaction(mtran);

                    Store mstore = X.GetCmp<Store>("storeListeLot_GestionStock");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();

                    DeselectGridRows();
                }
            }
            catch (Exception ex)
            {
                if (_db != null)
                {
                    _db.RollBackTransaction(mtran);
                }
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }

        public ActionResult OnPrintList()
        {
            Lot_GestionStock mclass = new Lot_GestionStock();
            Parametres mParam = new Parametres(0);

            ViewData["Campagne"] = mParam.Campagne;
            //ViewData["Produit"] = mParam.Produit.ID;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Lot_GestionStock_Print", ViewData = ViewData };
        }

        //public ActionResult OnPrintLot_GestionStockList()
        //{
        //    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
        //    try
        //    {
        //        XtraReport report = null;

        //        report = new rptLot_GestionStocksList() as XtraReport;

        //        report.DataSource = DevExpressReportDs.SetDataSource(report);
        //        report.Parameters["campagneID"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;
        //        report.Parameters["paramCampagneText"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

        //        report.Parameters["paramExportateur"].Value = int.Parse(X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Value);
        //        report.Parameters["paramExportateurNom"].Value = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text;

        //        report.Parameters["paramCertification"].Value = int.Parse(X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Value);
        //        report.Parameters["paramCertificationText"].Value = X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Text;

        //        report.Parameters["paramLot_GestionStockType"].Value = int.Parse(X.GetCmp<ComboBox>("cmbTypeLot_GestionStock").SelectedItem.Value);
        //        report.Parameters["paramLot_GestionStockTypeText"].Value = X.GetCmp<ComboBox>("cmbTypeLot_GestionStock").SelectedItem.Text;

        //        report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateDebut").RawText);
        //        report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateFin").RawText);

        //        report.Parameters["paramStatut"].Value = int.Parse(X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Value);
        //        report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Text;

        //        Session["report"] = report;


        //        return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Lot_GestionStock_GestionStock/ViewList', this, 'List Of Lot_GestionStocks',''),App.Lot_GestionStock_GestionStock_Print.doClose()", Guid.NewGuid(), BaseUrl));
        //    }
        //    catch (Exception ex)
        //    {
        //        X.MessageBox.Show(new MessageBoxConfig
        //        {
        //            Title = "Livraison : Data Validation",
        //            Message = ex.Message,
        //            Buttons = MessageBox.Button.OK,
        //            Icon = MessageBox.Icon.WARNING
        //        });
        //        return this.Direct();
        //    }
        //}

        public ActionResult SelectInfoTypeLot(string ItemType)
        {
            try
            {
                LotType _typeLot = new LotType();
                int _typeID = -1;
                bool result = int.TryParse(ItemType, out _typeID);

                _typeLot.fnGet(_typeID);
                if (_typeLot.ID != 0)
                {

                    X.GetCmp<TextField>("txtNombreSacs").Text = _typeLot.NombreSacs.ToString();
                    X.GetCmp<TextField>("txtNombrePalette").Text = _typeLot.NombrePalette.ToString();
                    //X.GetCmp<TextField>("txtPoidsBrut").Text = _typeLot.PoidsBrutAsString;
                    X.GetCmp<TextField>("txtTareSacs").Text = _typeLot.TareSacs.ToString();
                }
            }
            catch (Exception ex)
            {
                return this.Direct();
            }
            //OnRefresh(mClass.ID.ToString())
            return this.Direct();
        }


        public ActionResult ViewList()
        {
            XtraReport report = null;
            report = Session["report"] as XtraReport;
            //report = new rptLot_GestionStocksList() as XtraReport;

            //report.DataSource = DevExpressReportDs.SetDataSource(report);
            //report.Parameters["campagneID"].Value = Session["CampagneID"];
            //report.Parameters["paramCampagneText"].Value = Session["paramCampagneText"];

            //report.Parameters["paramExportateur"].Value = int.Parse(Session["paramExportateur"].ToString());
            //report.Parameters["paramExportateurNom"].Value = Session["paramExportateurNom"];

            //report.Parameters["paramCertification"].Value = int.Parse(Session["paramCertification"].ToString());
            //report.Parameters["paramCertificationText"].Value = Session["paramCertificationText"];

            //report.Parameters["paramLot_GestionStockType"].Value = int.Parse(Session["paramLot_GestionStockType"].ToString());
            //report.Parameters["paramLot_GestionStockTypeText"].Value = Session["paramLot_GestionStockTypeText"];

            //report.Parameters["paramDateDebut"].Value = DateTime.Parse(Session["paramDateDebut"].ToString());
            //report.Parameters["paramDateFin"].Value = DateTime.Parse(Session["paramDateFin"].ToString());

            //report.Parameters["paramStatut"].Value = int.Parse(Session["paramStatut"].ToString());
            //report.Parameters["paramStatutText"].Value = Session["paramStatutText"];            

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }


        private Lot_GestionStock MapFormToObject(Lot_GestionStock mClass)
        {
            Campagne campagne = new Campagne();
            campagne.Designation = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text.ToString();
            mClass.Campagne = campagne;

            Produit Produit = new Produit();
            Produit.ID = int.Parse(GetFormValue("cmbProduit"));
            Produit.Designation = X.GetCmp<ComboBox>("cmbProduit").SelectedItem.Text.ToString();
            mClass.Produit = Produit;

            Exportateur mExportateur = new Exportateur();
            mExportateur.ID = int.Parse(GetFormValue("cmbExportateur"));
            mExportateur.Nom = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text.ToString();
            mClass.Exportateur = mExportateur;

            Magasin Magasin = null;
            if (!string.IsNullOrEmpty(GetFormValue("cmbMagasin")))
            {
                Magasin = new Magasin();
                Magasin.ID = int.Parse(GetFormValue("cmbMagasin"));
                Magasin.Designation = X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Text.ToString();
            }
            mClass.Magasin = Magasin;

            mClass.NumeroLot = X.GetCmp<TextField>("TxtNumero").Text;
            mClass.DateLot = DateTime.Parse(X.GetCmp<DateField>("TxtDateLot").RawText.ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
            mClass.EstQueue = bool.Parse(X.GetCmp<Checkbox>("ChkEstQueue").Value.ToString());
            mClass.EstReusine = bool.Parse(X.GetCmp<Checkbox>("ChkEstReusine").Value.ToString());
            if (mClass.EstReusine)
            {
                mClass.NumeroLot = X.GetCmp<TextField>("TxtNumero").Text;
                bool result = new Lot_GestionStock().fnGetReusinageByNumero(mClass.NumeroLot);
                if (!result)
                    throw new Exception("Lot_GestionStock : Lot_GestionStock's Number Not Found");
            }

            mClass.EstManuel = true;

            LotType LotType = new LotType();
            LotType.ID = int.Parse(GetFormValue("cmbTypeLot"));
            LotType.Designation = X.GetCmp<ComboBox>("cmbTypeLot").SelectedItem.Text.ToString();
            mClass.LotType = LotType;

            mClass.NumeroLot = X.GetCmp<TextField>("TxtNumero").Text;

            if (X.GetCmp<TextField>("txtNombreSacs").Text != string.Empty) mClass.NombreSacs = int.Parse(X.GetCmp<TextField>("txtNombreSacs").Text.Replace(" ", ""));
            if (X.GetCmp<TextField>("txtNombrePalette").Text != string.Empty) mClass.NombrePalette = int.Parse(X.GetCmp<TextField>("txtNombrePalette").Text.Replace(" ", ""));
            if (X.GetCmp<TextField>("txtPoidsBrut").Text != string.Empty) mClass.PoidsBrut = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrut").Text);
            if (X.GetCmp<TextField>("txtTareSacs").Text != string.Empty) mClass.TareSacs = decimal.Parse(X.GetCmp<TextField>("txtTareSacs").Text);
            if (X.GetCmp<TextField>("txtTarePalette").Text != string.Empty) mClass.TarePalette = decimal.Parse(X.GetCmp<TextField>("txtTarePalette").Text);
            if (X.GetCmp<TextField>("txtPoidsNet").Text != string.Empty) mClass.PoidsNet = decimal.Parse(X.GetCmp<TextField>("txtPoidsNet").Text);

            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }

        public ActionResult OnCancel(string ItemSelected)
        {
            try
            {
                Lot_GestionStock Lot_GestionStock = JSON.Deserialize<Lot_GestionStock>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = Lot_GestionStock.fnGet(Lot_GestionStock.ID);

                if (!result)
                    throw new Exception("OnCancel : Movement Sheet loading failed.");

                Lot_GestionStock.UtilisateurModification = (string)Session["userName"];

                if (Lot_GestionStock.Desactive)
                    result = Lot_GestionStock.fnActivate();
                else
                    result = Lot_GestionStock.fnDeActivate();

                if (!result)
                    throw new Exception("OnCancel : Production - Lot_GestionStock, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeLot_GestionStock");

                    ModelProxy mProxy = mstore.GetById(Lot_GestionStock.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(Lot_GestionStock);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - Lot_GestionStock : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        [HttpPost]
        public ActionResult UpdateFormMethod()
        {
            try
            {
                Lot_GestionStock mClass = new Lot_GestionStock();
                bool result = true;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtLot_GestionStockId")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("UpdateFormMethod : Production - Lot_GestionStock - load failed.");

                }

                mClass = MapFormToObject(mClass);
                result = mClass.fnUpdate();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListeLot_GestionStock");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowLot_GestionStock").Select(0);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormLot_GestionStock").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - Lot_GestionStock : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnSelectProduction()
        {
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Lot_GestionStock_SelectProduction" };
        }

        public ActionResult SubmitOrdreProduction(string ItemSelected = "", string ItemNumero = "")
        {
            try
            {
                OrdreProduction mclass = new OrdreProduction();

                if (!string.IsNullOrEmpty(ItemNumero))
                {
                    if (mclass.fnGetByNumber(ItemNumero))
                    {
                        if (mclass.ID != Guid.Empty)
                        {
                            X.GetCmp<Hidden>("txtOrdreProductionID").SetValue(mclass.ID);
                            X.GetCmp<Hidden>("txtOrdreProduction").SetValue(mclass.NumeroProduction);
                            X.GetCmp<Window>("Lot_GestionStock_SelectProduction").Close();
                            return this.Direct();
                        }
                        else
                        {
                            X.GetCmp<Hidden>("txtOrdreProduction").SetValue("");
                            X.GetCmp<Hidden>("txtOrdreProductionID").SetValue(Guid.Empty);
                            X.MessageBox.Show(new MessageBoxConfig
                            {
                                Title = "Production : Ordre De Production",
                                Message = "Ordre De Production Not Found, Please Retry !",
                                Buttons = MessageBox.Button.OK,
                                Icon = MessageBox.Icon.WARNING
                            });
                            return this.Direct();
                        }
                    }
                }
                else
                {
                    mclass = JSON.Deserialize<OrdreProduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (mclass.ID != Guid.Empty)
                    {
                        X.GetCmp<Hidden>("txtOrdreProductionID").SetValue(mclass.ID);
                        X.GetCmp<Hidden>("txtOrdreProduction").SetValue(mclass.NumeroProduction);
                        X.GetCmp<Window>("Lot_GestionStock_SelectProduction").Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Ordre De Production - Data Validation",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
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
            X.GetCmp<Window>("FormLot_GestionStock").Close();
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

        [HttpPost]
        public async Task<ActionResult> GetLot_fromPeriod(DateTime debut, DateTime fin)
        {
            //DateTime debut = DateTime.Parse(X.GetCmp<DateField>("TxtDateDebut").RawText.ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
            //DateTime fin = DateTime.Parse(X.GetCmp<DateField>("TxtDateFin").RawText.ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);

            string apiUrl = "https://tnci-api.touton.com:2104/api/DataExtract?debut=" + debut + "&fin=" + fin;
            //apiUrl.h.Authorization = new AuthenticationHeaderValue("Bearer", token.token);
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "1F52C417-D568-4EE3-BE43-3F76F0E9BB56");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);

                }

            }
            return View();

        }

        public DirectResult GetLot_fromPeriod_text()
        {
            return this.Direct();

        }

        //public async Task<ActionResult> GetAll()
        //{
        //    List<employee> EmpInfo = new List<employee>();
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(Baseurl);
        //        client.DefaultRequestHeaders.Clear();
        //        HttpResponseMessage Res = await client.GetAsync("api/Employee/GetAllEmployees");
        //        if (Res.IsSuccessStatusCode)
        //        {
        //            var EmpResponse = Res.Content.ReadAsStringAsync().Result;
        //            EmpInfo = JsonConvert.DeserializeObject<list<employee>>(EmpResponse);
        //        }
        //        return View(EmpInfo);
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult> FetchData(DateTime debut, DateTime fin)
        {
            try
            {
                // Fetch data from HTTPS API
                var apiData = await FetchFromApi(debut, fin);

                // Process data
                var processedData = ProcessData(apiData);
                //var realResult = JsonConvert.DeserializeObject<LotData[]>(apiData);
                // Save to SQL Database
                //SaveToDatabase(processedData);

                return this.Direct();
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    error = $"Error: {ex.Message}"
                });
            }
        }


        [HttpPost]
        private async Task<string> FetchFromApi(DateTime debut, DateTime fin)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://tnci-api.touton.com:2104/");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", ApiToken);

                //var formattedDebut = debut.ToString("yyyy-MM-dd");
                //var formattedFin = fin.ToString("yyyy-MM-dd");

                var formattedDebut = debut.ToString();
                var formattedFin = fin.ToString();
                //var response = await client.GetAsync(
                //    $"{ApiUrl}?debut={Uri.EscapeDataString(formattedDebut)}&fin={Uri.EscapeDataString(formattedFin)}");

                var response = await client.GetAsync(string.Format("api/DataExtract?debut={0}&fin={1}",
                        debut.ToString("yyyy-MM-dd"),
                        fin.ToString("yyyy-MM-dd")));

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        [HttpPost]
        private async Task<ActionResult> FetchFromApi_Result(DateTime debut, DateTime fin)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://tnci-api.touton.com:2104/");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", ApiToken);

                //var formattedDebut = debut.ToString("yyyy-MM-dd");
                //var formattedFin = fin.ToString("yyyy-MM-dd");

                var formattedDebut = debut.ToString();
                var formattedFin = fin.ToString();
                //var response = await client.GetAsync(
                //    $"{ApiUrl}?debut={Uri.EscapeDataString(formattedDebut)}&fin={Uri.EscapeDataString(formattedFin)}");

                var response = await client.GetAsync(string.Format("api/DataExtract?debut={0}&fin={1}",
                        debut.ToString("yyyy-MM-dd"),
                        fin.ToString("yyyy-MM-dd")));

                response.EnsureSuccessStatusCode();
                return this.Direct();
            }
        }

        private LotData ProcessData(string rawJson)
        {
            // Add your custom processing logic here
            return new LotData
            {
                RawJson = rawJson,
                Items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DataItem>>(rawJson),
                ProcessedAt = DateTime.UtcNow
            };
        }        

        //[HttpPost]
        public ActionResult FetchFromApi_New1(DateTime debut, DateTime fin)
        {
            //System.Threading.Tasks.Task x = FetchFromApi(debut, fin);
            try
            {
                // Using HttpClient to call the API.
                using (var client = new HttpClient())
                {
                    // Set your API base URL here.
                    client.BaseAddress = new Uri("https://tnci-api.touton.com:2104/");                                        
                    // Configure the authorization header using the hardcoded token.
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", ApiToken);

                    //string apiUrl = "https://tnci-api.touton.com:2104/api/DataExtract/?debut=" + debut + "&fin=" + fin;
                    // Prepare the API endpoint with query string parameters.
                    // Adjust the endpoint (here: "api/lot") as required.
                    string requestUri = string.Format("api/DataExtract/?debut={0}&fin={1}",
                        debut.ToString("yyyy-MM-dd"),
                        fin.ToString("yyyy-MM-dd"));

                    var response = client.GetAsync(requestUri).Result;
                    //if (true)
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the API response (you might want to deserialize it).
                        var apiResponse = response.Content.ReadAsStringAsync().Result;                        

                        //var realResult = JsonConvert.DeserializeObject<LotData[]>(apiResponse);
                        List<LotData> _mList;
                        Lot_GestionStock _mLot;
                        _mList = JSON.Deserialize<List<LotData>>(apiResponse, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                        foreach (LotData lot in _mList)
                        {
                            _mLot = new Lot_GestionStock();
                            _mLot.IdLot = lot.IdLot;
                            _mLot.NumeroLot = lot.NumeroLot;

                            _mLot.Exportateur = new Exportateur();
                            _mLot.Exportateur.ID = lot.CodeExportateur;
                            _mLot.Exportateur.Nom = lot.Exportateur;

                            _mLot.Campagne = new Campagne();
                            _mLot.Campagne.Designation = lot.Campagne;

                            _mLot.Produit = new Produit();
                            _mLot.Produit.ID = lot.CodeProduit;

                            _mLot.Sites = new Site();
                            _mLot.Sites.ID = lot.CodeSite;
                            _mLot.Sites.Nom = lot.NomSite;

                            _mLot.Magasin = new Magasin();
                            _mLot.Magasin.ID = lot.CodeMagasinInitial;
                            _mLot.Magasin.Designation = lot.MagasinInitial;                            

                            _mLot.DateLot = lot.DateUsinage;
                            _mLot.NombreSacs = lot.NombresAcusine;
                            _mLot.NombrePalette = lot.NombrePalette;
                            _mLot.PoidsBrut = (decimal)lot.PoidsBrut;
                            _mLot.PoidsNet = (decimal)lot.PoidsUsine;
                            _mLot.TareSacs = (decimal)lot.TareSac;
                            _mLot.TarePalette = (decimal)lot.TarePalette;
                            _mLot.DateReusinage = lot.DateReusinage;
                            _mLot.NombreSacsReusine = (int)lot.NombresAcreusine;
                            _mLot.PoidsBrutReusine = (decimal)lot.PoidsBrutReusinage;
                            _mLot.PoidsNetReusine = (decimal)lot.PoidsReusinage;
                            _mLot.TarePaletteReusine = (decimal)lot.TarePaletteReusinage;

                            bool res = _mLot.fnSubmit_ListeLot();

                            if (res)
                            {

                            }
                        }
                        //Save Date


                        X.GetCmp<Window>("FormImport_Lot").Close();


                        X.MessageBox.Show(new MessageBoxConfig
                        {
                            Title = "Importation des lots",
                            Message = "Importation des Lots Réussie!",
                            Buttons = MessageBox.Button.OK,
                            Icon = MessageBox.Icon.INFO
                        });

                        // Ext.Net Direct returns a JSON response with success flag and data.
                        return this.Direct(new
                        {
                            success = true,
                            message = "API call succeeded",
                            data = apiResponse
                        });

                       
                    }
                    else
                    {
                        X.MessageBox.Show(new MessageBoxConfig
                        {
                            Title = "Importation des lots",
                            Message = "API call failed: " + response.ReasonPhrase,
                            Buttons = MessageBox.Button.OK,
                            Icon = MessageBox.Icon.ERROR
                        });

                        return this.Direct(new
                        {
                            success = false,
                            error = "API call failed: " + response.ReasonPhrase
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Return the exception error message in the response.
                return this.Direct(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }

        //[HttpPost]
        public ActionResult FetchFromApi_New_Test(DateTime debut, DateTime fin)
        {
            //System.Threading.Tasks.Task x = FetchFromApi(debut, fin);
            try
            {
                string apiUrl = "https://tnci-api.touton.com:2104/api/DataExtract?debut=" + debut + "&fin=" + fin;
                // Using HttpClient to call the API.
                using (var client = new HttpClient())
                {
                    // Set your API base URL here.
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "1F52C417-D568-4EE3-BE43-3F76F0E9BB56");
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync(apiUrl).Result;
                    //if (true)
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the API response (you might want to deserialize it).
                        var apiResponse = response.Content.ReadAsStringAsync().Result;

                        //var realResult = JsonConvert.DeserializeObject<LotData[]>(apiResponse);
                        List<LotData> _mList;
                        Lot_GestionStock _mLot;
                        _mList = JSON.Deserialize<List<LotData>>(apiResponse, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                       
                        //Save Date


                        // Ext.Net Direct returns a JSON response with success flag and data.
                        return this.Direct(new
                        {
                            success = true,
                            message = "API call succeeded",
                            data = apiResponse
                        });
                    }
                    else
                    {
                        return this.Direct(new
                        {
                            success = false,
                            error = "API call failed: " + response.ReasonPhrase
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Return the exception error message in the response.
                return this.Direct(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }


        //[HttpPost]
        public ActionResult FetchData_API(DateTime debut, DateTime fin)
        {            
            System.Threading.Tasks.Task xt = FetchFromApi(debut, fin);            
            return this.Direct();
        }

    }
}