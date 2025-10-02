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
    public class SpotPriceAgenceController : BaseController
    {

        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";

        // GET: SpotPriceAgence
        public ActionResult Index()
        {
            Parametres mParam = (new Parametres(0));

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            X.GetCmp<ComboBox>("cmbCampagne").SetValue(mParam.Campagne);

            DateTime firstdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            DateTime lastDayOfMonth = firstdayofmonth.AddMonths(1).AddDays(-1);

            X.GetCmp<DateField>("TxtPeriodStart").RawText = firstdayofmonth.ToShortDateString();
            X.GetCmp<DateField>("TxtPeriodEnd").RawText = lastDayOfMonth.ToShortDateString();

            X.GetCmp<FormPanel>("CriteriaPanelSpotPrice").SetTitle("Site : " + mSiteParDefaut.Nom + ", Campagne : " + mParam.Campagne + " | Du : " + firstdayofmonth.ToShortDateString() + " Au : " + lastDayOfMonth.ToShortDateString());

            #region Set Function's Access                        
            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{C2A8EDEE-54FE-45C4-A7D6-FA50C4B66A6B}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{82933E6D-84F2-4134-919F-73EDAF2E948D}")))
                X.GetCmp<Button>("btnNewSpotPrice").Enable();
            else
                X.GetCmp<Button>("btnNewSpotPrice").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{46ef48b7-2afe-4e28-9649-db51f92bc456}")))
                X.GetCmp<MenuItem>("mnuExportSpotPrice").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportSpotPrice").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{5a2cd46c-da7b-4781-8745-3f87773ca089}")))
                X.GetCmp<MenuItem>("mnuPrintSpotPriceList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintSpotPriceList").Disable();

            X.GetCmp<Hidden>("SphiddenPermCreer").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{82933e6d-84f2-4134-919f-73edaf2e948d}")));
            X.GetCmp<Hidden>("SphiddenPermModifier").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{6ac6012f-3e74-4770-b5c0-0adee18e6d61}")));
            X.GetCmp<Hidden>("SphiddenPermDesactiver").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{dcef4a98-21b2-46f2-ab1c-1130048b1936}")));
            //X.GetCmp<Hidden>("SphiddenPermActiver").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{EA25C709-E3FD-41CA-881F-C464193C3819}")));
            X.GetCmp<Hidden>("SphiddenPermExporterExcel").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{46ef48b7-2afe-4e28-9649-db51f92bc456}")));
            //X.GetCmp<Hidden>("SphiddenPermPrintSpotPriceList").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{2ABE7A72-758C-4A1D-B213-A34007BFECB1}"));
            X.GetCmp<Hidden>("SphiddenPermApprove").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("4890d2ef-56c1-4b83-b691-e0920dcf84ca")));
            X.GetCmp<Hidden>("SphiddenPermOverview").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{2380ec7e-8e91-432e-978f-bc8f67a208d2}")));
            //}
            #endregion

            return View();
        }


    }
}