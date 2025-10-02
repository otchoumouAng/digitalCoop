using Ext.Net;
using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Business.stock;
using Tms.Classes.Security;
using Tms.Classes.Shared;

namespace Tms2017.MVC.Controllers
{
    public class SituationStockQtyController : BaseController
    {
        // GET: SituationStockQty
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);            
            string Currentcampagne = mParam.Campagne;
            int MagasinID = mParam.Magasin.ID;
            string magasin = mParam.Magasin.Designation;

            Campagne mCampagne = new Campagne();
            mCampagne.fnGet(Currentcampagne);

            X.GetCmp<ComboBox>("cmbFiltreCampagne").SetValue(Currentcampagne);
            X.GetCmp<ComboBox>("cmbFiltreMagasin").SetValue(MagasinID);
            ViewBag.CampagneMinDate = mCampagne.DateDebut;

            //string StartDate = mCampagne.DateDebutAsString;
            //string EndDate = mCampagne.DateFinAsString;

            //X.GetCmp<DateField>("dtpFiltreStartDate").RawText = StartDate;
            //X.GetCmp<DateField>("dtpFiltreEndDate").RawText = EndDate;            

            //X.GetCmp<FormPanel>("StatutStockCP").SetTitle("Magasin : " + magasin + ", Campagne : " + Currentcampagne + ", Du : " + StartDate + " Au : " + EndDate);
            X.GetCmp<FormPanel>("StatutStockCP").SetTitle("Magasin : " + magasin + ", Campagne : " + Currentcampagne);

            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            if (HasAccess.fnGetUserAccessStatus("{ABB71737-BCB6-4CFE-AAB9-57F63184D05A}", UserName) == false)
                X.GetCmp<Button>("mnuPrintStockQuantiteList").Disable();
            else
                X.GetCmp<Button>("mnuPrintStockQuantiteList").Enable();

            if (HasAccess.fnGetUserAccessStatus("{4A036387-EEC4-4A0F-8630-63707DC2B66B}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportStockQuantite").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportStockQuantite").Enable();
            #endregion
            return View();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("StatutStockCP");
            mform.ToggleCollapse();
            //mform.Collapsed = false;
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne, string ItemMagasin, string ItemPeriodEnd)
        {
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "-1" : ItemCampagne;
            int MagasinID = GetCriteriaValue(ItemMagasin);
           // int ExportateurID = GetCriteriaValue(ItemExportateur);                        
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "";
            }
            //DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());            

            var mListe = (new MouvementStock()).fnSelectStockQuantite(ItemCampagne, MagasinID, null, EndDate);

            return this.Store(mListe);
        }

        public ActionResult SelectForDashStock(StoreRequestParameters parameters, string ItemCampagne, string ItemMagasin, string ItemPeriodEnd)
        {
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "-1" : ItemCampagne;
            int MagasinID = GetCriteriaValue(ItemMagasin);
            // int ExportateurID = GetCriteriaValue(ItemExportateur);                        
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "";
            }
            //DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            var mListe = (new MouvementStock()).fnSelectStockQuantite_Dash(ItemCampagne, MagasinID, null, EndDate);

            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemMagasin, string ItemPeriodEnd)
        {
            try
            {
                //DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeStatutStockQuantite");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCampagne",ItemCampagne),
                                    new Ext.Net.Parameter("ItemMagasin",ItemMagasin),
                                    //new Ext.Net.Parameter("ItemExportateur",ItemExportateur),
                                    //new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd)                                    
                                });
                
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Stock Status Quantity : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnChangeCampagne(string ItemCampagne)
        {
            Campagne mCampagne = new Campagne();
            mCampagne.fnGet(ItemCampagne);

            X.GetCmp<DateField>("dtpFiltreStartDate").RawText = mCampagne.DateDebutAsString;
            X.GetCmp<DateField>("dtpFiltreEndDate").RawText = mCampagne.DateFinAsString;

            return this.Direct();
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