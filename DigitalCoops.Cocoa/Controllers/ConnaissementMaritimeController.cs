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
using Tms.Classes.Business.Sales;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.Sales;

namespace Tms2017.MVC.Controllers
{
    
    public class ConnaissementMaritimeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        // GET: ConnaissementMaritime
        public ActionResult Index()
        {
            var mParam = (new Parametres()).fnSelect();

            Parametres mclass = new Parametres(0);
            //mclass = mParam[0] as Parametres;
            Campagne mCampagne = new Campagne();

            X.GetCmp<ComboBox>("cmbCropYear").SetValue(mclass.Campagne);

            string StartDate = "01/01/" + DateTime.Now.Year.ToString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("dtpStartDate").RawText = StartDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Exportateur = "{Tous}";

            X.GetCmp<FormPanel>("BLCriteriaPanel").SetTitle("Campagne : " + mclass.Campagne + " | Exportateur : " + Exportateur + " | Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            if (HasAccess.fnGetUserAccessStatus("{A4FC10DE-F7CB-4A4E-8398-44F0E1FCF54B}", UserName) == false)
                X.GetCmp<MenuItem>("btnNew").Disable();
            else
                X.GetCmp<MenuItem>("btnNew").Enable();

            if (HasAccess.fnGetUserAccessStatus("{40B8E339-9BB5-4F60-8D22-F2878ED1C80F}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExport").Disable();
            else
                X.GetCmp<MenuItem>("mnuExport").Enable();

            
            if (HasAccess.fnGetUserAccessStatus("{A315A098-286D-4C00-B221-ED8F28AEA12E}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintHistory").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintHistory").Enable();

            X.GetCmp<Hidden>("blhiddenPermDesactive").SetValue(HasAccess.fnGetUserAccessStatus("{F04E4A74-F6EE-407A-9191-A5F450EC2620}", UserName));
            X.GetCmp<Hidden>("blhiddenPermActive").SetValue(HasAccess.fnGetUserAccessStatus("{8FC86316-665C-44BE-A9BB-B5C6A42EE087}", UserName));
            X.GetCmp<Hidden>("blhiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{40B8E339-9BB5-4F60-8D22-F2878ED1C80F}", UserName));
            X.GetCmp<Hidden>("blhiddenPermPrintHistory").SetValue(HasAccess.fnGetUserAccessStatus("{A315A098-286D-4C00-B221-ED8F28AEA12E}", UserName));
            X.GetCmp<Hidden>("blhiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{2EDE1215-A6A3-4350-AFAC-D1ADF8C0A9A5}", UserName));
            X.GetCmp<Hidden>("blhiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{A4FC10DE-F7CB-4A4E-8398-44F0E1FCF54B}", UserName));
            X.GetCmp<Hidden>("blhiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{5737F251-4885-4FB1-872C-3B652A3217A5}", UserName));
            #endregion
            return View();
        }
        public ActionResult LoadListOfBL(StoreRequestParameters parameters, string ItemCropYear, string ItemExporter, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {

            int ExportateurID = GetCriteriaValue(ItemExporter);
            string Campagne = string.IsNullOrEmpty(ItemCropYear) ? "" : ItemCropYear;

            if (!string.IsNullOrEmpty(ItemCropYear) && ItemCropYear.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            int Status = int.Parse(ItemStatus);

            var mListe = (new ConnaissementMaritime()).fnSelect(Campagne, ExportateurID, StartDate, EndDate, Status);

            //return this.Store(paging);
            return this.Store(mListe);
        }


        public ActionResult onCreate()
        {
            ConnaissementMaritimeViewModel viewmodel = new ConnaissementMaritimeViewModel();

            viewmodel._ConnaissementMaritime = new ConnaissementMaritime();

            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "ConnaissementMaritime_Detail", Model = viewmodel };
        }
        public ActionResult onEdit(string ItemSelected)
        {
            ConnaissementMaritime mclass = JSON.Deserialize<ConnaissementMaritime>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            ConnaissementMaritimeViewModel viewModel = new ConnaissementMaritimeViewModel();

            viewModel._ConnaissementMaritime = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "ConnaissementMaritime_Detail", Model = viewModel };
        }
        public ActionResult onConsult(string ItemSelected)
        {

            ConnaissementMaritime mclass = JSON.Deserialize<ConnaissementMaritime>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            ConnaissementMaritimeViewModel viewModel = new ConnaissementMaritimeViewModel();

            viewModel._ConnaissementMaritime = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "ConnaissementMaritime_Detail", Model = viewModel };
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                ConnaissementMaritime mclass = JSON.Deserialize<ConnaissementMaritime>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = mclass.fnGet(mclass.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : BL loading failed.");

                mclass.UtilisateurModification = (string)Session["userName"];

                if (mclass.Desactive)
                    result = mclass.fnActivate();
                else
                    result = mclass.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : BL, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeBL");

                    ModelProxy mProxy = mstore.GetById(mclass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mclass);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "BL : OnActivateDeactivate",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }
        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("BLCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemCropYear, string ItemExporter, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeBL");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemExporter"   ,ItemExporter),
                                    new Ext.Net.Parameter("ItemCropYear"      ,ItemCropYear),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus)
                                });



                // collapse criterias areas
                X.GetCmp<FormPanel>("BLCriteriaPanel").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "BL : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnSelectShipment()
        {
            EmbarquementViewModel mclass = new EmbarquementViewModel();
            Parametres mParam = new Parametres(0);            
            Campagne mCampagne = new Campagne();
            mCampagne.Designation = mParam.Campagne;
            mCampagne.DateDebut = DateTime.Now.AddDays(-30); ;
            mCampagne.DateFin = DateTime.Now;            
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Select_Embarquement", Model = mCampagne };
        }

        public ActionResult OnRefreshForAvailableShipments(string ItemExportateur, string ItemPeriodEnd, string ItemCampagne, string ItemPeriodStart)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);
                int ExportateurID = GetCriteriaValue(ItemExportateur);

                string CropYearID = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
                if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
                {
                    CropYearID = "{Tous}";
                }

                Store mstore = X.GetCmp<Store>("storeListShipment");
                mstore.Reload(
                    new Ext.Net.ParameterCollection()
                    {
                        new Ext.Net.Parameter("ItemExportateur"   ,ItemExportateur),
                        new Ext.Net.Parameter("ItemCampagne"   ,ItemCampagne),
                        new Ext.Net.Parameter("ItemPeriodStart"   ,datedebut),
                        new Ext.Net.Parameter("ItemPeriodEnd"     ,datedfin)
                    });
                return this.Direct();
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "BL : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }                       
        }

        public ActionResult LoadListOfAvailableShipments(string ItemExportateur, string ItemPeriodStart, string ItemPeriodEnd, string ItemCampagne)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                string CropYearID = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
                if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
                {
                    CropYearID = "{Tous}";
                }
                int ExportateurID = GetCriteriaValue(ItemExportateur);

                //DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
                //DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

                var mListe = (new Embarquement()).fnSelectForBL(CropYearID, ExportateurID, -1, -1, -1, datedebut, datedfin, 0);
                return this.Store(mListe);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "BL : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }            
            //return this.Store(paging);
            
        }


        public ActionResult SubmitOnSelectShipment(string ItemSelected)
        {
            try
            {
                Embarquement Item = JSON.Deserialize<Embarquement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                if (Item != null)
                {
                    X.GetCmp<TextField>("txtShipmentNumber").Text = Item.Numero;
                    X.GetCmp<TextField>("txtEmbarquementID").Value = Item.ID;

                    X.GetCmp<Window>("ListOfShipments").Close();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Select Shipment : SubmitOnSelectShipment",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitFormMethod()
        {


            try
            {
                ConnaissementMaritime mClass = new ConnaissementMaritime();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("txtConnaissementMaritimeID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : BL load failed.");

                    if (mClass.Desactive == true)
                        throw new Exception("SubmitFormMethod : BL is disabled");
                }

                bool Result = true;


                mClass = MapFormToObject(mClass);
                Result = mClass.fnUpdate();
                if (Result)
                {
                    Store mStore = X.GetCmp<Store>("storeListeBL");

                  
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionListeBL").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();


                    }

                    X.GetCmp<Window>("ConnaissementMaritime_Detail").Close();
                }







            }
            catch (Exception ex)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "BL : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnPrintBL(string typeReport)
        {
            ViewData["typeReport"] = typeReport;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintBLReport", ViewData = ViewData };

        }

        public ActionResult PrintBL(string TypeReport)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;
                string type = "";
               
                if (TypeReport == "Hi")
                {
                    report = new rptBLHistory() as XtraReport;
                    type = "BL - History";

                }

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["paramCampagne"].Value = X.GetCmp<ComboBox>("SScropYearForReport").SelectedItem.Text;

                report.Parameters["paramExportateurID"].Value = X.GetCmp<ComboBox>("SSexportateurForReport").SelectedItem.Value.ToString();
                report.Parameters["paramExportateur"].Value = X.GetCmp<ComboBox>("SSexportateurForReport").SelectedItem.Text;


                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("SSstartDateForReport").RawText.ToString());
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("SSdueDateForReport").RawText.ToString());

                if (TypeReport == "Hi")
                {

                    report.Parameters["paramStatutID"].Value = X.GetCmp<ComboBox>("SSStatus").SelectedItem.Value.ToString();
                    report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("SSStatus").SelectedItem.Text;

                }
                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/ConnaissementMaritime/ViewList', this, '{2}',''),App.FormPrintBLReport.doClose()", Guid.NewGuid(), BaseUrl, type));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "BL : Data Validation",
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

            ViewData["Report"] = report;

            return View("ViewReport");
        }
        #region "Methods"
        private ConnaissementMaritime MapFormToObject(ConnaissementMaritime mClass)
        {
            try
            {
                mClass.ID = Guid.Parse(X.GetCmp<Hidden>("txtConnaissementMaritimeID").Text);

                //mClass.Numero = X.GetCmp<TextField>("txtNumber").Text;

               
                Embarquement mEmb = new Embarquement();
                mEmb.ID = Guid.Parse(X.GetCmp<TextField>("txtEmbarquementID").Text);
                mEmb.Numero = X.GetCmp<TextField>("txtShipmentNumber").Text;
                mClass.Embarquement = mEmb;

                Navire nNavire = null;
                if (!string.IsNullOrEmpty(GetFormValue("cmbNavire")))
                {
                    nNavire = new Navire();
                    nNavire.ID = int.Parse(GetFormValue("cmbNavire"));
                    nNavire.Nom = X.GetCmp<ComboBox>("cmbNavire").SelectedItem.Text.ToString();
                }
                mClass.Navire = nNavire;

                mClass.NumeroBL = X.GetCmp<TextField>("txtNumberBL").Text;
                mClass.NumeroDossier = X.GetCmp<TextField>("txtFolderNumber").Text;
                mClass.NumeroD6 = X.GetCmp<TextField>("txtD6Number").Text;
                mClass.NumeroAffaire = X.GetCmp<TextField>("txtNumeroAffaire").Text;
                mClass.NumeroBesc = X.GetCmp<TextField>("txtNumeroBesc").Text;
                mClass.NumeroDeclaration = X.GetCmp<TextField>("txtNumeroDeclaration").Text;
                mClass.Statut = "NL";
                mClass.Date = DateTime.Parse(X.GetCmp<DateField>("txtDate").RawText.ToString());

                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];



            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "BL : MapFormToObject",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return mClass;
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
            X.GetCmp<RowSelectionModel>("rowSelectionListeBL").DeselectAll();
        }

        #endregion
    }
}