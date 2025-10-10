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
    public class CertificatPoidsController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        // GET: CertificatPoidsController
        public ActionResult Index()
        {

            string StartDate = "01/01/" + DateTime.Now.Year.ToString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("dtpStartDate").RawText = StartDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Exportateur = "{Tous}";

            X.GetCmp<FormPanel>("WeighingCertificateCriteriaPanel").SetTitle("Exportateur : " + Exportateur + " | Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            if (HasAccess.fnGetUserAccessStatus("{FCE3D7C3-8182-4132-AD4C-ACC2BB5D0C27}", UserName) == false)
                X.GetCmp<MenuItem>("btnNew").Disable();
            else
                X.GetCmp<MenuItem>("btnNew").Enable();

            if (HasAccess.fnGetUserAccessStatus("{B035BCAD-D5B3-4502-8599-E24B5F16548F}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExport").Disable();
            else
                X.GetCmp<MenuItem>("mnuExport").Enable();


            if (HasAccess.fnGetUserAccessStatus("{C0F8A9C0-A88D-4142-A936-C323CF246887}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintHistory").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintHistory").Enable();

            X.GetCmp<Hidden>("wchiddenPermDesactive").SetValue(HasAccess.fnGetUserAccessStatus("{C0365BEA-486C-4CBB-A95E-7AF3C85DB3B9}", UserName));
            X.GetCmp<Hidden>("wchiddenPermActive").SetValue(HasAccess.fnGetUserAccessStatus("{3A5838C5-52EA-4319-9B2D-90BFC80BEEB0}", UserName));
            X.GetCmp<Hidden>("wchiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{B035BCAD-D5B3-4502-8599-E24B5F16548F}", UserName));
            X.GetCmp<Hidden>("wchiddenPermPrintHistory").SetValue(HasAccess.fnGetUserAccessStatus("{C0F8A9C0-A88D-4142-A936-C323CF246887}", UserName));
            X.GetCmp<Hidden>("wchiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{912ECAE9-1B56-4FBB-B7B3-93BE318DBA5A}", UserName));
            X.GetCmp<Hidden>("wchiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{FCE3D7C3-8182-4132-AD4C-ACC2BB5D0C27}", UserName));
            X.GetCmp<Hidden>("wchiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{AA7C18EE-1996-44D0-B29A-1561B117F261}", UserName));
            #endregion
            return View();
        }

        public ActionResult LoadListOfWeighingCertificate(StoreRequestParameters parameters,string ItemExporter, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {

            int ExportateurID = GetCriteriaValue(ItemExporter);
            //string Campagne = string.IsNullOrEmpty(ItemCropYear) ? "" : ItemCropYear;

            //if (!string.IsNullOrEmpty(ItemCropYear) && ItemCropYear.Contains("null"))
            //{
            //    Campagne = "";
            //}

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            int Status = int.Parse(ItemStatus);

            var mListe = (new CertificatPoids()).fnSelect("{all}", ExportateurID, StartDate, EndDate, Status);

            //return this.Store(paging);
            return this.Store(mListe);
        }
        public ActionResult onCreate()
        {
            CertificatPoidsViewModel viewmodel = new CertificatPoidsViewModel();

            viewmodel._CertificatPoids = new CertificatPoids();

            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "CertificatPoids_Detail", Model = viewmodel };
        }
        public ActionResult onEdit(string ItemSelected)
        {
            CertificatPoids mclass = JSON.Deserialize<CertificatPoids>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            CertificatPoidsViewModel viewModel = new CertificatPoidsViewModel();

            viewModel._CertificatPoids = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;


            return new Ext.Net.MVC.PartialViewResult { ViewName = "CertificatPoids_Detail", Model = viewModel };
        }
        public ActionResult onConsult(string ItemSelected)
        {

            CertificatPoids mclass = JSON.Deserialize<CertificatPoids>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            CertificatPoidsViewModel viewModel = new CertificatPoidsViewModel();

            viewModel._CertificatPoids = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "CertificatPoids_Detail", Model = viewModel };
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                CertificatPoids mclass = JSON.Deserialize<CertificatPoids>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = mclass.fnGet(mclass.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Weighing Certificate loading failed.");

                mclass.UtilisateurModification = (string)Session["userName"];

                if (mclass.Desactive)
                    result = mclass.fnActivate();
                else
                    result = mclass.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Weighing Certificate, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeWeighingCertificate");

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
            FormPanel mform = X.GetCmp<FormPanel>("WeighingCertificateCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh( string ItemExporter, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeWeighingCertificate");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemExporter"   ,ItemExporter),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus)
                                });



                // collapse criterias areas
                X.GetCmp<FormPanel>("WeighingCertificateCriteriaPanel").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing Certificate : Data Validation",
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

            //var StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            //var EndDate = DateTime.Now.ToShortDateString();

            var Exportateur = "{Tous}";
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;


            return new Ext.Net.MVC.PartialViewResult { ViewName = "Select_Embarquement", Model = mclass };


        }

        public ActionResult OnRefreshForAvailableShipments(string ItemExportateur)
        {

            int ExportateurID = GetCriteriaValue(ItemExportateur);


            //DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            //DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            Store mstore = X.GetCmp<Store>("storeListShipment");
            mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemExportateur"   ,ItemExportateur)
                                    //,
                                    //new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    //new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd)
                                });

            return this.Direct();
        }


        public ActionResult LoadListOfAvailableShipments(string ItemExportateur)
        {

            int ExportateurID = GetCriteriaValue(ItemExportateur);


            //DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            //DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            var mListe = (new Embarquement()).fnSelectForWeighingCertificate("{Tous}", ExportateurID, -1, -1, -1, null, null, 0);

            //return this.Store(paging);
            return this.Store(mListe);
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
                    X.GetCmp<TextField>("txtNumeroContratHid").Value = Item.Contrat.ContratNumero;
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
                CertificatPoids mClass = new CertificatPoids();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("txtCertificatPoidsID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Weighing Certificate load failed.");

                    if (mClass.Desactive == true)
                        throw new Exception("SubmitFormMethod : Weighing Certificate is disabled");
                }

                bool Result = true;


                mClass = MapFormToObject(mClass);
                Result = mClass.fnUpdate();
                if (Result)
                {
                    Store mStore = X.GetCmp<Store>("storeListeWeighingCertificate");


                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionWeighingCertificate").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();


                    }

                    X.GetCmp<Window>("CertificatPoids_Detail").Close();
                }







            }
            catch (Exception ex)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing Certificate : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnPrintWeighingCertificate(string typeReport)
        {
            ViewData["typeReport"] = typeReport;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintCertificatPoidsReport", ViewData = ViewData };

        }

        public ActionResult PrintWeighingCertificate(string TypeReport)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;
                string type = "";
               
                if (TypeReport == "Hi")
                {
                    report = new rptCertificatPoidsHistory() as XtraReport;

                    type = "Weighing Certificate - History";

                }

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["paramCampagne"].Value = X.GetCmp<ComboBox>("EnrcropYearForReport").SelectedItem.Text;

                report.Parameters["paramExportateurID"].Value = X.GetCmp<ComboBox>("EnrexportateurForReport").SelectedItem.Value.ToString();
                report.Parameters["paramExportateur"].Value = X.GetCmp<ComboBox>("EnrexportateurForReport").SelectedItem.Text;

                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("EnrstartDateForReport").RawText.ToString());
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("EnrdueDateForReport").RawText.ToString());

                if (TypeReport == "Hi")
                {
                    report.Parameters["paramStatutID"].Value = X.GetCmp<ComboBox>("EnrStatus").SelectedItem.Value.ToString();
                    report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("EnrStatus").SelectedItem.Text;
                }
                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/CertificatPoids/ViewList', this, '{2}',''),App.FormPrintCertificatPoidsReport.doClose()", Guid.NewGuid(), BaseUrl, type));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing Certificate : Data Validation",
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
        private CertificatPoids MapFormToObject(CertificatPoids mClass)
        {
            try
            {
                mClass.ID = Guid.Parse(X.GetCmp<Hidden>("txtCertificatPoidsID").Text);

                //mClass.Numero = X.GetCmp<TextField>("txtNumber").Text;


                Embarquement mEmb = new Embarquement();
                mEmb.Contrat = new ContratDeVentes();
                mEmb.Contrat.ContratNumero = X.GetCmp<TextField>("txtNumeroContratHid").Value.ToString();
                mEmb.ID = Guid.Parse(X.GetCmp<TextField>("txtEmbarquementID").Text);
                mEmb.Numero = X.GetCmp<TextField>("txtShipmentNumber").Text;
                mClass.Embarquement = mEmb;

                OrganismeCertification mOrg = new OrganismeCertification();
                mOrg.ID = int.Parse(X.GetCmp<ComboBox>("_cmbBodyCertification").Text);
                mOrg.Nom = X.GetCmp<ComboBox>("_cmbBodyCertification").SelectedItem.Text.ToString();
                mClass.OrganismeCertification = mOrg;

                if (X.GetCmp<TextField>("txtWeight").Text != string.Empty) mClass.PoidsArrivee = decimal.Parse(X.GetCmp<TextField>("txtWeight").Text);
                if (X.GetCmp<TextField>("txtSampleWeight").Text != string.Empty) mClass.PoidsEchantillonArrivee = decimal.Parse(X.GetCmp<TextField>("txtSampleWeight").Text);

                mClass.Commentaire = X.GetCmp<TextField>("txtComment").Text;
                mClass.Statut = "NL";
                mClass.Date = DateTime.Parse(X.GetCmp<DateField>("txtDate").RawText.ToString());
                mClass.DateArrivee = DateTime.Parse(X.GetCmp<DateField>("txtDateArrived").RawText.ToString());

                mClass.DateDepotage = string.IsNullOrEmpty(X.GetCmp<DateField>("txtDateDepotage").RawText) ? (DateTime?)null : DateTime.Parse(X.GetCmp<DateField>("txtDateDepotage").RawText.ToString());

                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing Certificate : MapFormToObject",
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
            X.GetCmp<RowSelectionModel>("rowSelectionWeighingCertificate").DeselectAll();
        }

        #endregion
    }
}