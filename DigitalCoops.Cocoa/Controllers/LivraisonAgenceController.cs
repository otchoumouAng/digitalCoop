using CrystalDecisions.CrystalReports.Engine;
using DevExpress.XtraReports.UI;
using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Business;
using Tms.Classes.Business.Sites;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Components.Data;
using Tms.Components.Reports;
using Tms.Components.Settings;
using Tms2017.MVC.Models;
using Tms2017.MVC.Reports;
using Tms2017.Reports;

namespace Tms2017.MVC.Controllers
{
    public class LivraisonAgenceController : BaseController
    {

        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";

        public LivraisonAgenceController()
        {
            //string cultureName = "en";
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
        }

        // GET: LivraisonAgence
        public ActionResult Index()
        {
            string UserName = (string)Session["userName"];
            Parametres mParam = new Parametres(0);                        

            ViewBag.DefaultExportateur = mParam.Exportateur.ID;
            ViewBag.DefaultCampagne = mParam.Campagne;
            ViewBag.LivraisonAchat = mParam.LivraisonTypeAchat.ID;

            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;
            Fonction HasAccessFunction = new Fonction();
            bool HasAccessAllSite = HasAccessFunction.fnGetUserAccessStatus("{efb6414e-10b1-4815-91db-51804f7642ff}", UserName);
            ViewBag.HasAccessAllSite = HasAccessAllSite;

            X.GetCmp<FormPanel>("CriteriaPanel").SetTitle("Site : " + mSiteParDefaut.Nom + ", TODAY - Campagne : " + mParam.Campagne + ", Exportateur : " + mParam.Exportateur.Nom + ", Type De Livraison : " + mParam.LivraisonTypeAchat.Designation);
            #region Set Function's Access

            
            Fonction HasAccess = new Fonction();            
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{3AF93DDB-A66C-48B7-B30A-9CCE0E112D25}", UserName);

            if (HasAccess.fnGetUserAccessStatus("{f3de1d30-4629-4f2b-9849-01c9c3f85a9b}", UserName) == false)
                X.GetCmp<Button>("btnNew").Disable();
            else
                X.GetCmp<Button>("btnNew").Enable();

            if (HasAccess.fnGetUserAccessStatus("{d360c1bb-91ef-45b0-9d06-a3b6ce90463a}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportDeliveries").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportDeliveries").Enable();

            if (HasAccess.fnGetUserAccessStatus("{4b222e43-892e-4ee1-82c7-a7df7362f852}", UserName) == false)
                X.GetCmp<MenuItem>("mnuLvhiddenPermPrintDeliveriesList").Disable();
            else
                X.GetCmp<MenuItem>("mnuLvhiddenPermPrintDeliveriesList").Enable();

            X.GetCmp<Hidden>("LvhiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{f3de1d30-4629-4f2b-9849-01c9c3f85a9b}", UserName));
            X.GetCmp<Hidden>("LvhiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{f021cb27-1fd7-40ce-a70b-94faf03c47f7}", UserName));
            X.GetCmp<Hidden>("LvhiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{df2500ad-81da-4d7e-aa89-2f46e8dc137f}", UserName));
            X.GetCmp<Hidden>("LvhiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{d360c1bb-91ef-45b0-9d06-a3b6ce90463a}", UserName));
            X.GetCmp<Hidden>("LvhiddenPermPrintDeliveriesList").SetValue(HasAccess.fnGetUserAccessStatus("{4b222e43-892e-4ee1-82c7-a7df7362f852}", UserName));
            X.GetCmp<Hidden>("LvhiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{f71ee4e5-6d77-42df-b098-9798ce9b92de}", UserName));
            X.GetCmp<Hidden>("LvhiddenPermUpdateSupplier").SetValue(HasAccess.fnGetUserAccessStatus("{4B6B9FF5-E474-484A-A1E2-DDC6C32EC84F}", UserName));

            #endregion


            return View();
        }

        public ActionResult OnCreate()
        {
            LivraisonViewModel mclass = new LivraisonViewModel();

            try
            {
                Site mSiteParDefaut = new Site();
                string UserName = (string)Session["userName"];
                bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

                mclass._Livraison = new Livraison();
                mclass._Livraison.Site = new Site();
                mclass._Livraison.Site.ID = mSiteParDefaut.ID;
                mclass._Livraison.Site.Nom = mSiteParDefaut.Nom;
                mclass._Livraison.Site.Destination = new Destination();
                mclass._Livraison.Site.Provenance = new Provenance();
                mclass._Livraison.Site.Destination.ID = mSiteParDefaut.Destination.ID;
                mclass._Livraison.Site.Provenance.ID = mSiteParDefaut.Provenance.ID;

                mclass._Livraison.Destination = new Destination();
                mclass._Livraison.Destination.ID = mSiteParDefaut.Destination.ID;
                mclass._Livraison.Provenance = new Provenance();
                mclass._Livraison.Provenance.ID = mSiteParDefaut.Provenance.ID;

                Parametres mParam = new Parametres(0);
                mclass._Livraison.Exportateur = new Exportateur();
                mclass._Livraison.Exportateur = mParam.Exportateur;
                mclass._Livraison.Campagne = new Campagne();
                mclass._Livraison.Campagne.Designation = mParam.Campagne;
                mclass._Livraison.Recolte = new Recolte();
                mclass._Livraison.Recolte = mParam.Recolte;
                ViewData["StatutLivraison"] = 0;
                
                mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;                                            
                
                mclass._Livraison.LivraisonType = new LivraisonType();
                mclass._Livraison.LivraisonType.ID = mParam.LivraisonTypeAchat.ID;
                mclass._Parametres = mParam;

            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }


            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLivraison", Model = mclass, ViewData = ViewData };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            LivraisonViewModel mclass = new LivraisonViewModel();
            Livraison mLivraison = new Livraison();
            mLivraison = JSON.Deserialize<Livraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            try
            {
                Site mSiteParDefaut = new Site();
                string UserName = (string)Session["userName"];
                bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
                mclass._Livraison = mLivraison;
                mclass._Livraison.Site = new Site();
                mclass._Livraison.Site.ID = mSiteParDefaut.ID;
                mclass._Livraison.Site.Nom = mSiteParDefaut.Nom;

                Parametres mParam = new Parametres(0);
                mclass._Campagne = mParam.Campagne;
                mclass._Livraison.Site.Provenance = new Provenance();                
                mclass._Livraison.Site.Provenance.ID = mSiteParDefaut.Provenance.ID;
                mclass._Livraison.Site.Destination = new Destination();                
                mclass._Livraison.Site.Destination.ID = mSiteParDefaut.Destination.ID;

                ViewData["StatutLivraison"] = 0;
                mclass._Parametres = mParam;

                mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;                
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }


            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLivraison", Model = mclass, ViewData = ViewData };
        }

        public ActionResult OnConsult(string ItemSelected)
        {
            LivraisonViewModel mclass = new LivraisonViewModel();
            Livraison mLivraison = new Livraison();
            mLivraison = JSON.Deserialize<Livraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            try
            {
                Site mSiteParDefaut = new Site();
                string UserName = (string)Session["userName"];
                bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
                mclass._Livraison = mLivraison;
                mclass._Livraison.Site = new Site();
                mclass._Livraison.Site.ID = mSiteParDefaut.ID;
                mclass._Livraison.Site.Nom = mSiteParDefaut.Nom;

                Parametres mParam = new Parametres(0);
                mclass._Campagne = mParam.Campagne;
                mclass._Livraison.Site.Provenance = new Provenance();
                mclass._Livraison.Site.Provenance.ID = mSiteParDefaut.Provenance.ID;
                mclass._Livraison.Site.Destination = new Destination();
                mclass._Livraison.Site.Destination.ID = mSiteParDefaut.Destination.ID;

                ViewData["StatutLivraison"] = 0;
                mclass._Parametres = mParam;

                mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }


            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLivraison", Model = mclass, ViewData = ViewData };
        }

        private Livraison MapFormToObject(Livraison mClass)
        {
            int iConverted;
            bool result;

            mClass.Site = new Tms.Classes.Shared.Site();
            mClass.Site.ID = int.Parse(GetFormValue("cmbDetSite"));
            mClass.Site.Nom = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text.ToString();
            Site mSite = new Site();
            result = mSite.fnGet(mClass.Site.ID);
            mClass.Provenance = new Tms.Classes.Shared.Provenance();
            //mClass.Provenance.ID = int.Parse(GetFormValue("cmbDetOrigin"));
            //mClass.Provenance.Nom = X.GetCmp<ComboBox>("cmbDetOrigin").SelectedItem.Text.ToString();
            mClass.Provenance.ID = mSite.Provenance.ID;
            mClass.Provenance.Nom = mSite.Provenance.Nom;

            mClass.Destination = new Tms.Classes.Shared.Destination();
            mClass.Destination.ID = int.Parse(GetFormValue("cmbDetDestination"));
            mClass.Destination.Nom = X.GetCmp<ComboBox>("cmbDetDestination").SelectedItem.Text.ToString();

            mClass.Exportateur = new Tms.Classes.Shared.Exportateur();
            mClass.Exportateur.ID = int.Parse(GetFormValue("cmbDetExporter"));
            mClass.Exportateur.Nom = X.GetCmp<ComboBox>("cmbDetExporter").SelectedItem.Text.ToString();

            mClass.Campagne = new Tms.Classes.Shared.Campagne();
            mClass.Campagne.Designation = X.GetCmp<ComboBox>("cmbDetCrop").SelectedItem.Text.ToString();

            mClass.SacType = new Tms.Classes.Shared.SacType();
            mClass.SacType.ID = int.Parse(GetFormValue("cmbDetPackaging"));
            mClass.SacType.Designation = X.GetCmp<ComboBox>("cmbDetPackaging").SelectedItem.Text.ToString();

            mClass.Recolte = new Tms.Classes.Shared.Recolte();
            mClass.Recolte.ID = int.Parse(GetFormValue("cmbDetCropQuality"));
            mClass.Recolte.Designation = X.GetCmp<ComboBox>("cmbDetCropQuality").SelectedItem.Text.ToString();

            mClass.SacsDeclares = int.Parse(GetFormValue("txtNbrOfBags"));

            mClass.DateLivraison = DateTime.Parse(GetFormValue("dtfDeliveryDate") + " " + GetFormValue("tmfDelivery"));

            //mClass.PoidsDeclare = decimal.Parse("1500,58");
            mClass.PoidsDeclare = Convert.ToDecimal(GetFormValue("txtEstimatedTonnage"));

            mClass.LivraisonType = new Tms.Classes.Shared.LivraisonType();
            mClass.LivraisonType.ID = int.Parse(GetFormValue("cmbDetDeliveryType"));
            mClass.LivraisonType.Designation = X.GetCmp<ComboBox>("cmbDetDeliveryType").SelectedItem.Text.ToString();

            mClass.Transitaire = null;
            result = int.TryParse(GetFormValue("cmbDetTransitaire"), out iConverted);
            if (result)
            {
                mClass.Transitaire = new Tms.Classes.Shared.Transitaire();
                mClass.Transitaire.ID = iConverted;
                mClass.Transitaire.Nom = X.GetCmp<ComboBox>("cmbDetTransitaire").SelectedItem.Text.ToString();
            }

            mClass.Fournisseur = new Tms.Classes.Business.Fournisseur();
            mClass.Fournisseur.ID = int.Parse(GetFormValue("cmbDetFournisseur"));
            mClass.Fournisseur.Nom = X.GetCmp<ComboBox>("cmbDetFournisseur").SelectedItem.Text.ToString();

            mClass.NumConteneur = GetFormValue("txtContainerNumber");
            mClass.Immatriculation = GetFormValue("txtTruckID");
            mClass.NumPlomb = GetFormValue("txtSealNumber");
            mClass.Tracteur = GetFormValue("txtTractorID");
            mClass.NumOT = GetFormValue("txtShipmentNumber");
            mClass.Chauffeur = GetFormValue("txtDriverName");
            mClass.Numero = GetFormValue("txtDeliveryNumber");
            mClass.NumeroExterne = GetFormValue("txtExternalWayBill");

            mClass.Certification = null;
            result = int.TryParse(GetFormValue("cmbDetCertification"), out iConverted);
            if (result)
            {
                mClass.Certification = new Tms.Classes.Shared.Certification();
                mClass.Certification.ID = int.Parse(GetFormValue("cmbDetCertification"));
                mClass.Certification.Designation = X.GetCmp<ComboBox>("cmbDetCertification").SelectedItem.Text.ToString();
            }

            mClass.Transporteur = null;
            result = int.TryParse(GetFormValue("cmbDetTransporter"), out iConverted);
            if (result)
            {
                mClass.Transporteur = new Tms.Classes.Shared.Transporteur();
                mClass.Transporteur.ID = int.Parse(GetFormValue("cmbDetTransporter"));
                mClass.Transporteur.Nom = X.GetCmp<ComboBox>("cmbDetTransporter").SelectedItem.Text.ToString();
            }

            mClass.CentreAchat = null;
            result = int.TryParse(GetFormValue("cmbDetSite"), out iConverted);
            if (result)
            {
                mClass.CentreAchat = new Tms.Classes.Shared.Site();
                mClass.CentreAchat.ID = int.Parse(GetFormValue("cmbDetSite"));
                mClass.CentreAchat.Nom = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text.ToString();
            }

            mClass.NumLot = GetFormValue("txtLotNumber");

            mClass.TransfertFeves = null;
            Guid idTransfert;
            Parametres mParam = new Parametres(0);
            if (!string.IsNullOrEmpty(GetFormValue("txtTransferNumero")) && mClass.LivraisonType.ID == mParam.IDTransfertFevesAgence)
            {
                result = Guid.TryParse(GetFormValue("hiddenTransferID"), out idTransfert);
                mClass.TransfertFeves = new TransfertFeves();
                mClass.TransfertFeves.ID = idTransfert;
                mClass.TransfertFeves.Numero = GetFormValue("txtTransferNumero");
            }

            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }

        private string GetFormValue(string id_Component)
        {
            string data = Request.Form[id_Component];
            return string.IsNullOrEmpty(data) ? string.Empty : data;
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

        [HttpPost]
        public ActionResult SubmitFormMethod(string FromWeighing = "")
        {
            try
            {
                Livraison mClass = new Livraison();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("hiddenID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Delivery load failed.");
                }

                mClass = MapFormToObject(mClass);

                bool result = mClass.fnUpdateSite();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeDeliveriesList");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);

                        X.GetCmp<RowSelectionModel>("rowSelectionListe").Select(0);
                    }
                    else
                    {

                        if (FromWeighing == "NO")
                        {
                            mProxy = mStore.GetById(mClass.ID);
                            mProxy.BeginEdit();

                            mProxy.Set(mClass);

                            mProxy.Commit();

                            mProxy.EndEdit();
                        }
                        else
                        {
                            CloseDetailWindow();
                        }
                    }

                    CloseDetailWindow();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error : SubmitFormMethod", ex.Message).Show();
            }

            return this.Direct();

        }

        private void CloseDetailWindow()
        {
            X.GetCmp<Window>("frmLivraison").Hide();
        }

        public ActionResult LoadListOfAvailableTransfert(string ItemSite, string ItemCampagne, string ItemPeriodStart, string ItemPeriodEnd)
        {
            int? siteID = ItemSite == "" ? (int?)null : int.Parse(ItemSite);
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "-1" : ItemCampagne;
            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            var mListe = (new TransfertFeves()).fnSelectForLivraison((int)siteID, Campagne, StartDate, EndDate);
            return this.Store(mListe);
        }

        public ActionResult OnSelectTransfert(int? siteID)
        {
            try
            {                
                if (!siteID.HasValue)
                    throw new Exception("Error");

                Parametres mParam = new Parametres(0);
                TransfertFeves mModel = new TransfertFeves();
                mModel.Campagne = new Campagne();
                mModel.Campagne.Designation = mParam.Campagne;
                mModel.Sites = new Site();
                mModel.Sites.ID = (int)siteID;
                return new Ext.Net.MVC.PartialViewResult { ViewName = "Livraison_SelectTransfert", Model = mModel };

            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Livraison : Select Beans Transfer",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }

        }

        public ActionResult SubmitTransfert(string ItemSelected)
        {            
            TransfertFeves mTransfert = new TransfertFeves();
            mTransfert = JSON.Deserialize<TransfertFeves>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            X.GetCmp<Hidden>("hiddenTransferID").SetValue(mTransfert.ID);
            X.GetCmp<TextField>("txtTransferNumero").SetValue(mTransfert.Numero);

            X.GetCmp<ComboBox>("cmbDetDestination").SetValue(mTransfert.Destination.ID);
            X.GetCmp<NumberField>("txtNbrOfBags").SetValue(mTransfert.NombreSacs);
            X.GetCmp<NumberField>("txtEstimatedTonnage").SetValue(mTransfert.PoidsBrut);
            X.GetCmp<Window>("Livraison_SelectTransfert").Close();

            return this.Direct();
        }
    }
}