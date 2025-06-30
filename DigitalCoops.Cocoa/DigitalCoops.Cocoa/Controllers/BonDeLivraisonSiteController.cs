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
using Tms.Classes.Security;
using System.Globalization;
using DevExpress.XtraReports.UI;

namespace Tms2017.MVC.Controllers
{
    public class BonDeLivraisonSiteController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";

        Guid mID;

        // GET: BonDeLivraisonSite
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            X.GetCmp<ComboBox>("cmbCampagne").SetValue(mParam.Campagne);

            //string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string StartDate = DateTime.Now.ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("dtpStartDate").RawText = EndDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Fournisseur = "{Tous}";
            string Type = "{Tous}";

            X.GetCmp<FormPanel>("CriteriaPanelBL").SetTitle("Site : "+ mSiteParDefaut.Nom + ", Campagne : " + mParam.Campagne + " | Fournisseur : " + Fournisseur + " | Type De Livraison : " + Type + " | Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{82662823-2bac-4d5f-bd37-bce065a60751}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{ebbda5c6-3b53-418e-8d8e-e98b77c3c619}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{9087280e-9c78-433f-8594-1fa10b1cce52}")))
                X.GetCmp<Button>("mnuExportDeliveryNote").Enable();
            else
                X.GetCmp<Button>("mnuExportDeliveryNote").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{33958b19-faff-4f0c-a9c3-2758bf2cb51e}")))
                X.GetCmp<Button>("btnViewPendingDeliveries").Enable();
            else
                X.GetCmp<Button>("btnViewPendingDeliveries").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{4da1d26e-b7ca-4207-b3ba-a531cf795de4}")))
                X.GetCmp<Button>("mnuPrintDeliveryNoteList").Enable();
            else
                X.GetCmp<Button>("mnuPrintDeliveryNoteList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{17a9e19c-4667-4c65-b173-3749ab8c263e}")))
                X.GetCmp<MenuItem>("btnOtherBL").Enable();
            else
                X.GetCmp<MenuItem>("btnOtherBL").Disable();

            X.GetCmp<Hidden>("BlhiddenPermCreer").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{ebbda5c6-3b53-418e-8d8e-e98b77c3c619}")));
            X.GetCmp<Hidden>("BlhiddenPermConsultPendingDeliveries").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{33958b19-faff-4f0c-a9c3-2758bf2cb51e}")));
            X.GetCmp<Hidden>("BlhiddenPermDesactiver").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{a1cce599-bc43-4105-a489-11e8fe95c991}")));
            X.GetCmp<Hidden>("BlhiddenPermPrintCopyOfDeliveryNote").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{58cb0ea0-ac1f-4b9d-bd6e-9087ea8fceb8}")));
            X.GetCmp<Hidden>("BlhiddenPermExporterExcel").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{9087280e-9c78-433f-8594-1fa10b1cce52}")));
            X.GetCmp<Hidden>("BlhiddenPermOverview").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{8b371c28-6b8b-469b-9046-9af90a9da15b}")));
                       
            #endregion

            return View();
        }

        public ActionResult Index_Reclassification()
        {
            var mParam = (new Parametres()).fnSelect();

            Parametres mclass = new Parametres();
            mclass = mParam[0] as Parametres;

            Campagne mCampagne = new Campagne();

            X.GetCmp<ComboBox>("cmbCampagne").SetValue(mclass.Campagne);

            //string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string StartDate = DateTime.Now.ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("dtpStartDate").RawText = EndDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Fournisseur = "{Tous}";

            string Type = "{Tous}";

            X.GetCmp<FormPanel>("CriteriaPanelBL").SetTitle("Campagne : " + mclass.Campagne + " | Fournisseur : " + Fournisseur + " | Type De Livraison : " + Type + " | Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{F40FD8FA-A761-4752-8514-77B523811751}", UserName);

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{54B5D223-C84B-4D0A-A0BC-F046F1604664}"), UserName);

            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();            

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{297E9919-1B40-47CA-8AF4-38473AA19899}")))
                X.GetCmp<Hidden>("BlhiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("BlhiddenPermModifier").SetValue(false);

            #endregion

            return View();
        }                         

        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus , string ItemSite)
        {
            int FournisseurID = GetCriteriaValue(ItemFournisseur);
            int TypeID = GetCriteriaValue(ItemType);
            int siteID = GetCriteriaValue(ItemSite);

            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;

            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "{Tous}";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string Status = ItemStatus;

            int siteFournisseur = -1;

            string UserName = (string)Session["userName"];
            Site mclass = new Site();
            bool result = mclass.fnGetByUserName(UserName);

            Parametres mParam = new Parametres(0);
            if (siteID != mclass.ID)
            {
                if (siteID == mParam.Site)
                {
                    siteID = -2;
                    siteFournisseur = mclass.ID;
                }
                else
                {
                    siteID = mclass.ID;
                    siteFournisseur = mclass.ID;
                }
            }
            
            var mListe = (new BonDeLivraison()).fnSelectExtend(Campagne, FournisseurID, TypeID, StartDate, EndDate, Status, siteID,siteFournisseur);         
            return this.Store(mListe);
        }        

        public ActionResult OnConsultBL(string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite)
        {
            BonDeLivraison mBon = new BonDeLivraison();
            mBon.Livraison = new Livraison();
            mBon.Livraison.Fournisseur = new Fournisseur();
            mBon.Livraison.LivraisonType = new LivraisonType();
            mBon.Sites = new Site();
            mBon.Livraison.Fournisseur.ID = GetCriteriaValue(ItemFournisseur);
            mBon.Livraison.LivraisonType.ID = GetCriteriaValue(ItemType);            
             
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;

            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "{Tous}";
            }
            mBon.Campagne = Campagne;

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            ViewData["StartDate"] = StartDate;
            ViewData["EndDate"] = EndDate;
            mBon.Statut = ItemStatus;

            //int siteFournisseur = -1;
            string UserName = (string)Session["userName"];
            Site mclass = new Site();
            bool result = mclass.fnGetByUserName(UserName);            

            Parametres mParam = new Parametres(0);
            mBon.Sites.ID = mParam.Site;
            ViewData["siteFournisseur"] = mclass.ID;           
                        
            return new Ext.Net.MVC.PartialViewResult { ViewName = "BonDeLivraison_Others", Model = mBon, ViewData = ViewData };           
        }

        public ActionResult SelectOtherBL(StoreRequestParameters parameters, string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite, string ItemFournisseurSite)
        {
            int FournisseurID = GetCriteriaValue(ItemFournisseur);
            int TypeID = GetCriteriaValue(ItemType);
            int siteID = GetCriteriaValue(ItemSite);

            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;

            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "{Tous}";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string Status = ItemStatus;

            int siteFournisseur = GetCriteriaValue(ItemFournisseurSite);

            string UserName = (string)Session["userName"];
            Site mclass = new Site();
            bool result = mclass.fnGetByUserName(UserName);

            Parametres mParam = new Parametres(0);
            //if (siteID != mclass.ID)
            //{
            //    if (siteID == mParam.Site)
            //    {
            //        siteID = -2;
            //        siteFournisseur = mclass.ID;
            //    }
            //    else
            //    {
            //        siteID = mclass.ID;
            //        siteFournisseur = mclass.ID;
            //    }
            //}

            var mListe = (new BonDeLivraison()).fnSelectExtend(Campagne, FournisseurID, TypeID, StartDate, EndDate, Status, siteID, siteFournisseur);
            return this.Store(mListe);
        }


        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelBL");
            mform.ToggleCollapse();
            //mform.Collapsed = false;
            return this.Direct();
        }

        public ActionResult OnCancel(string ItemSelected)
        {

            try
            {
                BonDeLivraison mClass = JSON.Deserialize<BonDeLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);

                if (!result)
                    throw new Exception("OnCancel : Bon De Reception loading failed.");

                string UserName = (string)Session["userName"];
                Site mSiteParDefaut = new Site();

                result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

                if (mClass.Sites.ID != mSiteParDefaut.ID)
                    throw new Exception("OnCancel : You're not able to cancel this Item !");

                mClass.UtilisateurModification = (string)Session["userName"];

                if (mClass.fnCancel())
                {
                    Store mstore = X.GetCmp<Store>("storeListe");

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
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bon De Reception : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult OnRefresh(string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListe");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemCampagne"      ,ItemCampagne),
                                    new Ext.Net.Parameter("ItemType"          ,ItemType),
                                    new Ext.Net.Parameter("ItemSite", ItemSite),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus)
                                });

                string title = X.GetCmp<FormPanel>("CriteriaPanelBL").Title;
                title += "Site = " + X.GetCmp<ComboBox>("cmbSite").SelectedItem.Text;

                title += ", Campagne : " + X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                title += ", Fournisseur : " + X.GetCmp<ComboBox>("cmbFournisseur").SelectedItem.Text;

                title += ", Type De Livraison = " + X.GetCmp<ComboBox>("cmbType").SelectedItem.Text;

                title += ", From " + X.GetCmp<DateField>("dtpStartDate").RawText.ToString() + "to " + X.GetCmp<DateField>("dtpEndDate").RawText.ToString();

                X.GetCmp<FormPanel>("CriteriaPanelBL").Title = title;

                // collapse criterias areas
                X.GetCmp<FormPanel>("CriteriaPanelBL").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bon De Reception : OnRefresh",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnRefreshOther(string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite, string ItemFournisseurSite)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeOtherBL");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"    ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemCampagne"       ,ItemCampagne),
                                    new Ext.Net.Parameter("ItemType"           ,ItemType),
                                    new Ext.Net.Parameter("ItemSite"           ,ItemSite),
                                    new Ext.Net.Parameter("ItemPeriodStart"    ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"      ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"         ,ItemStatus),
                                    new Ext.Net.Parameter("ItemFournisseurSite",ItemFournisseurSite)
                                });

                //string title = X.GetCmp<FormPanel>("CriteriaPanelBL").Title;
                //title += "Site = " + X.GetCmp<ComboBox>("cmbSite").SelectedItem.Text;

                //title += ", Campagne : " + X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                //title += ", Fournisseur : " + X.GetCmp<ComboBox>("cmbFournisseur").SelectedItem.Text;

                //title += ", Type De Livraison = " + X.GetCmp<ComboBox>("cmbType").SelectedItem.Text;

                //title += ", From " + X.GetCmp<DateField>("dtpStartDate").RawText.ToString() + "to " + X.GetCmp<DateField>("dtpEndDate").RawText.ToString();

                //X.GetCmp<FormPanel>("CriteriaPanelBL").Title = title;

                //// collapse criterias areas
                //X.GetCmp<FormPanel>("CriteriaPanelBL").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bon De Reception : OnRefresh",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult OnRefreshForClassification(string ItemCampagne, string ItemFournisseur, string ItemType, string ItemPeriodStart, string ItemPeriodEnd)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListe");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemCampagne"      ,ItemCampagne),
                                    new Ext.Net.Parameter("ItemType"          ,ItemType),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd)
                                });

                string title = X.GetCmp<FormPanel>("CriteriaPanelBL").Title;

                title += "Campagne : " + X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                title += ", Fournisseur : " + X.GetCmp<ComboBox>("cmbFournisseur").SelectedItem.Text;

                title += ", Type De Livraison = " + X.GetCmp<ComboBox>("cmbType").SelectedItem.Text;

                title += ", From " + X.GetCmp<DateField>("dtpStartDate").RawText.ToString() + "to " + X.GetCmp<DateField>("dtpEndDate").RawText.ToString();

                X.GetCmp<FormPanel>("CriteriaPanelBL").Title = title;

                // collapse criterias areas
                X.GetCmp<FormPanel>("CriteriaPanelBL").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bon De Reception : OnRefresh",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
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

        private void DeselectGridRows()
        {
            //X.Js.Call("App.rowSelectionListe.deselectAll();");
            X.GetCmp<RowSelectionModel>("rowSelectionListe").DeselectAll();
        }

        public ActionResult OnPrintDeliveryNote(string IdBon, string IsCopy, string AfficheResultatAnalyse = "0")
        {
            bool ReportIscopy = false;
            bool isBool = false;
            bool afficheAnalyse = false;
            if (string.IsNullOrEmpty(IsCopy))
                ReportIscopy = true;

            isBool = bool.TryParse(AfficheResultatAnalyse, out afficheAnalyse);

            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'DeliveryNote{0}', '{1}/BonDeLivraisonSite/ViewReport?id={0}&IsCopy={2}&AfficheResultatAnalyse={3}', this, 'Bon De Reception Report','')", IdBon, BaseUrl, ReportIscopy, afficheAnalyse));
        }

        public ActionResult ViewReport(string id, bool IsCopy, bool AfficheResultatAnalyse)
        {
            XtraReport report = new XtraReport(); ;

            if (AfficheResultatAnalyse)
                report = new rptBonDeLivraisonSite() as XtraReport;
            else
                report = new rptBonDeLivraisonSiteDivers() as XtraReport;

            report.DataSource = DevExpressReportDs.SetDataSource(report);
            report.Parameters["ID"].Value = id;
            report.Parameters["IsCopy"].Value = IsCopy;

            ViewData["Report"] = report;

            return View();
        }

    }
}
