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

namespace Tms2017.MVC.Controllers
{
    public class TransfertLotController : Controller
    {
        // GET: TransfertLot
        public ActionResult Index()
        {
            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("txtDateDebut").RawText = StartDate;
            X.GetCmp<DateField>("txtDateFin").RawText = EndDate;

            string UserName = (string)Session["userName"];
            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{5757CE19-23A8-4E72-9682-BA5B35A80C38}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{BFE7D577-CC78-4E18-8018-085BB0944921}")))
                X.GetCmp<Hidden>("TRhiddenPermConsult").SetValue(true);
            else
                X.GetCmp<Hidden>("TRhiddenPermConsult").SetValue(true);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{9A51A307-D66F-4FE6-A132-9AD134D0441D}")))
                X.GetCmp<MenuItem>("mnuExportTransfer").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportTransfer").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{9C2B6E77-C55F-4806-956A-9181F2612A97}")))
                X.GetCmp<MenuItem>("mnuPrintListTransfer").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintListTransfer").Disable();

            return View();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("TransfertLotCP");
            mform.ToggleCollapse();
            //mform.Collapsed = false;
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemMagasinSource, string ItemMagasinDestination, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatut)
        {            
            int MagasinSourceID = GetCriteriaValue(ItemMagasinSource);
            int MagasinDestinationID = GetCriteriaValue(ItemMagasinDestination);
            string Statut = string.IsNullOrEmpty(ItemStatut) ? "-1" : ItemStatut;

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());            

            var mListe = (new TransfertFeves()).fnSelect(MagasinSourceID, MagasinDestinationID, StartDate, EndDate, Statut);

            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemMagasinSource, string ItemMagasinDestination, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatut)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeTransfert");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemMagasinSource",ItemMagasinSource),
                                    new Ext.Net.Parameter("ItemMagasinDestination",ItemMagasinDestination),
                                    new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatut",ItemStatut)
                                });
                

                X.GetCmp<FormPanel>("TransfertLotCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Beans Transfer : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult onConsult(string ItemSelected)
        {            
            TransfertFevesViewModel mclass = new TransfertFevesViewModel();
            TransfertFeves mTransfert = JSON.Deserialize<TransfertFeves>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._TransfertFeves = mTransfert;            

            //mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormTransfertLot", Model = mclass, };

        }

        private string GetFormValue(string id_Component)
        {
            return Request.Form[id_Component];
        }

        private int GetCriteriaValue(string strComponent)
        {
            int value;

            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
                return value;
            else
                return -1;
        }


    }
}