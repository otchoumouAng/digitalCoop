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

namespace Tms2017.MVC.Controllers
{
    public class MiseEnComptePlanifieeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";

        Guid mID;

        // GET: MiseEnComptePlanifiee
        public ActionResult Index()
        {
            var mParam = (new Parametres()).fnSelect();

            Parametres mclass = new Parametres();
            mclass = mParam[0] as Parametres;

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("dtpStartDate").RawText = StartDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Fournisseur = "{Tous}";           

            X.GetCmp<FormPanel>("CriteriaPanelMP").SetTitle("Fournisseur : " + Fournisseur  + " | Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{A76C8D3F-77B3-42B2-8C66-6E1246499626}", UserName);

            if (HasAccess.fnGetUserAccessStatus("{6328821A-1866-4F27-934D-25893BD5848D}", UserName) == false)
                X.GetCmp<Button>("btnNew").Disable();
            else
                X.GetCmp<Button>("btnNew").Enable();

            if (HasAccess.fnGetUserAccessStatus("{CA238FB3-79C2-4B36-A061-6F9A66129250}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportPlanSaving").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportPlanSaving").Enable();

            if (HasAccess.fnGetUserAccessStatus("{F8437177-921A-4998-B40E-04AC79CD6AA8}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintProgSavingList").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintProgSavingList").Enable();

            X.GetCmp<Hidden>("MphiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{6328821A-1866-4F27-934D-25893BD5848D}", UserName));
            X.GetCmp<Hidden>("MphiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{1669889D-E271-45D8-AFB9-9D167FDA8697}", UserName));
            X.GetCmp<Hidden>("MphiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{1CAADF30-D335-44F6-8B24-015828C16F5A}", UserName));
            X.GetCmp<Hidden>("MphiddenPermPrintSavingList").SetValue(HasAccess.fnGetUserAccessStatus("{F8437177-921A-4998-B40E-04AC79CD6AA8}", UserName));
            X.GetCmp<Hidden>("MphiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{CA238FB3-79C2-4B36-A061-6F9A66129250}", UserName));
            X.GetCmp<Hidden>("MphiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{A76C8D3F-77B3-42B2-8C66-6E1246499626}", UserName));

            #endregion

            return View();
        }
               
        public ActionResult onAdd()
        {
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Mask();
            MiseEnComptePlanifieeViewModel mclass = new MiseEnComptePlanifieeViewModel();

            mclass._MiseEnComptePlanifiee = new MiseEnComptePlanifiee();

            mclass._MiseEnComptePlanifiee.Campagne = new Parametres(0).Campagne;

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
              

            return new Ext.Net.MVC.PartialViewResult { ViewName = "MecPlanifiee_Detail", Model = mclass, };
        }
        public ActionResult onEdit(string ItemSelected)
        {
            MiseEnComptePlanifiee mclass = JSON.Deserialize<MiseEnComptePlanifiee>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            MiseEnComptePlanifieeViewModel viewModel = new MiseEnComptePlanifieeViewModel();

            viewModel._MiseEnComptePlanifiee = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "MecPlanifiee_Detail", Model = viewModel };
        }

        public ActionResult onConsult(string ItemSelected)
        {
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Mask();

            MiseEnComptePlanifiee mclass = JSON.Deserialize<MiseEnComptePlanifiee>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            MiseEnComptePlanifieeViewModel viewmodel = new MiseEnComptePlanifieeViewModel();
            viewmodel._MiseEnComptePlanifiee = mclass;
            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "MecPlanifiee_Detail", Model = viewmodel, };
        }

        public ActionResult OnCancel(string ItemSelected)
        {

            try
            {
                MiseEnComptePlanifiee mClass = JSON.Deserialize<MiseEnComptePlanifiee>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);

                if (!result)
                    throw new Exception("OnCancel : Programmed saving loading failed.");

                mClass.UtilisateurModification = (string)Session["userName"];

                if (mClass.fnCancel())
                {
                    Store mstore = X.GetCmp<Store>("storeListeMiseEnComptePlanifiee");

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
                    Title = "Programmed saving : Cancel",
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
                MiseEnComptePlanifiee mClass = new MiseEnComptePlanifiee();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("txtMecID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("UpdateFormMethod : Programmed saving load failed.");
                }

                mClass = MapFormToObject(mClass);

                bool result = mClass.fnUpdate();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListeMiseEnComptePlanifiee");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionListeMECP").Select(0);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("MecPlanifiee_Detail").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Programmed saving : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult LoadListOfMec(StoreRequestParameters parameters, string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {

            int FournisseurID = GetCriteriaValue(ItemFournisseur);        

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string Status = ItemStatus;

            var mListe = (new MiseEnComptePlanifiee()).fnSelect(FournisseurID, StartDate, EndDate, Status);
            //var paging = GridStorePaging.SetRangePlants(parameters, mListe);
            return this.Store(mListe);            
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelMP");

            mform.ToggleCollapse();
            //mform.Collapsed = false;
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeMiseEnComptePlanifiee");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus)
                                });

                string title = X.GetCmp<FormPanel>("CriteriaPanelMP").Title;

                title += ", Fournisseur : " + X.GetCmp<ComboBox>("cmbFournisseur").SelectedItem.Text;

                title += ", From " + X.GetCmp<DateField>("dtpStartDate").RawText.ToString() + "to " + X.GetCmp<DateField>("dtpEndDate").RawText.ToString();

                X.GetCmp<FormPanel>("CriteriaPanelMP").Title = title;

                // collapse criterias areas
                X.GetCmp<FormPanel>("CriteriaPanelMP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Mise en Compte planifiée : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }            

            return this.Direct();
        }

        private MiseEnComptePlanifiee MapFormToObject(MiseEnComptePlanifiee mClass)
        {
            mClass.Campagne = X.GetCmp<ComboBox>("_cmbCampagne").Text;

            mClass.Numero = X.GetCmp<Hidden>("hiddenNumero").Text;
            MiseEnCompteType mType = new MiseEnCompteType();
            mType.ID = int.Parse(GetFormValue("_cmbType"));
            mType.Designation = X.GetCmp<ComboBox>("_cmbType").SelectedItem.Text.ToString();
            mClass.MiseEnCompteType = mType;

            Fournisseur mSupplier = new Fournisseur();
            mSupplier.ID = int.Parse(GetFormValue("_cmbFournisseur"));
            mSupplier.Nom = X.GetCmp<ComboBox>("_cmbFournisseur").SelectedItem.Text.ToString();
            mClass.Fournisseur = mSupplier;

            MiseEnComptePlanifieeMode mPrev = new MiseEnComptePlanifieeMode();
            mPrev.ID = int.Parse(GetFormValue("cmbPrelevementType"));
            mPrev.Designation = X.GetCmp<ComboBox>("cmbPrelevementType").SelectedItem.Text.ToString();
            mClass.PrelevementMode = mPrev;

            mClass.DateMiseEnCompte = DateTime.Parse(X.GetCmp<DateField>("txtDate").RawText.ToString());
            mClass.DateEcheance = DateTime.Parse(X.GetCmp<DateField>("txtDueDate").RawText.ToString());
            if (X.GetCmp<TextField>("txtTauxRemboursement").Text != string.Empty) mClass.PrelevementTaux = decimal.Parse(X.GetCmp<TextField>("txtTauxRemboursement").Text);
            mClass.Commentaire = X.GetCmp<TextField>("txtComment").Text;
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

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
            X.GetCmp<RowSelectionModel>("rowSelectionListeMECP").DeselectAll();
        }

        public ActionResult OnDisplayProgSavingList()
        {
            ViewData["Titre"] = "Liste des Mise en Compte planifiées";
            ViewData["actionToDo"] = "OnPrintProgSavingList";
            ViewData["ControllerName"] = "MiseEnComptePlanifiee";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmCriteriaForProgSaving", ViewData = ViewData };
        }

        public ActionResult OnPrintProgSavingList(string fournisseur, string fournisseurText, string startDate, string endDate, string statut, string statutText)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                //Session["paramCropYear"] = cropYear;                
                //Session["paramCampagne"] = cropyear;
                Session["paramFournisseur"] = fournisseur;
                Session["paramFournisseurText"] = fournisseurText;
                Session["paramStatut"] = statut;
                Session["paramStatutText"] = statutText;

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/MiseEnComptePlanifiee/ViewReportListResult', this, 'List Of Mise en Compte planifiées',''),App.frmCriteriaForProgSaving.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Mise en Compte planifiées : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }

        }

        public ActionResult ViewReportListResult()
        {
            //XtraReport report = null;

            rptProgSavingList report = new rptProgSavingList();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            //report.Parameters["paramCampagneText"].Value = Session["paramCampagne"];
            //if (!string.IsNullOrEmpty(Session["paramCampagne"].ToString()) && Session["paramCampagne"].ToString() == "{Tous}")
            //    report.Parameters["paramCampagne"].Value = string.Empty;
            //else
            //    report.Parameters["paramCampagne"].Value = Session["paramCampagne"];

            report.Parameters["paramFournisseur"].Value = Session["paramFournisseur"];
            report.Parameters["paramFournisseurText"].Value = Session["paramFournisseurText"];

            report.Parameters["paramStatut"].Value = Session["paramStatut"];
            report.Parameters["paramStatutText"].Value = Session["paramStatutText"];

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];
            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

    }
}
