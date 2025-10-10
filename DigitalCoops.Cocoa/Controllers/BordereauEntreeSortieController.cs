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
using Tms.Classes.Business.stock;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;

namespace Tms2017.MVC.Controllers
{
    public class BordereauEntreeSortieController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        // GET: BordereauEntreeSortie
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);
            string campagne = mParam.Campagne;

            X.GetCmp<ComboBox>("cmbFiltreCampagne").SetValue(campagne);

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("txtFiltreDateDebut").RawText = StartDate;
            X.GetCmp<DateField>("txtFiltreDateFin").RawText = EndDate;

            X.GetCmp<FormPanel>("BordereauEntreeSortieCP").SetTitle("Crop Year : " + campagne + ", From : " + StartDate + " To : " + EndDate);

            #region Set Function's Access
            Fonction HasAccess = new Fonction();
            string UserName = (string)Session["userName"];
            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{da3895df-c56c-4e2f-82c4-f32096ee8a65}"), UserName);

            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            //if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{7a5a5c96-7edb-4b12-8050-22d1cbcf9c89}")))
            //    X.GetCmp<Button>("btnNew").Enable();
            //else
            //    X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{8B8A3037-6B78-4F09-BD4B-1D166BE8D61A}")))
                X.GetCmp<MenuItem>("mnuPrintListBordereau").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintListBordereau").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{84BDEE3D-757C-4796-83D0-FA9C4619BD17}")))
                X.GetCmp<MenuItem>("mnuExportListBordereau").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportListBordereau").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{92FBC1AD-74B7-4D59-B1D2-7612E2762CC0}")))
                X.GetCmp<Hidden>("beshiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("beshiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{40fa7f22-bba6-4f95-8340-2c29996d15da}")))
                X.GetCmp<Hidden>("beshiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("beshiddenPermModifier").SetValue(false);

            bool HavAccessMagasinTV = false;
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{7EE49D2D-A1B5-4A9C-958E-5115674E4313}")))
                HavAccessMagasinTV = true;

            bool HavAccessMagasinExport = false;
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{4EC0B159-E190-430A-89A7-8ED847076F4D}")))
                HavAccessMagasinExport = true;

            if (HavAccessMagasinTV && HavAccessMagasinExport)
            {
                ViewData["LoadMagasin"] = "LoadActiveMagasin";
                ViewData["magasinID"] = mParam.MagasinTV;
            }
            else if (HavAccessMagasinTV == true)
            {
                ViewData["LoadMagasin"] = "LoadOnlyMagasinTV";
                ViewData["magasinID"] = mParam.MagasinTV;
            }
            else if (HavAccessMagasinExport == true)
            {
                ViewData["LoadMagasin"] = "LoadOnlyMagasinExport";
                ViewData["magasinID"] = mParam.MagasinExport;
            }
            else {
                ViewData["LoadMagasin"] = "";
                ViewData["magasinID"] = "0";
            }

            //X.GetCmp<ComboBox>("cmbFiltreMagasin").SetValue(ListeMagasin[0].ID);
            #endregion
            return View();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("BordereauEntreeSortieCP");
            mform.ToggleCollapse();
            //mform.Collapsed = false;
            return this.Direct();
        }

        public ActionResult onAdd()
        {
            BordereauEntreeSortieViewModel mclass = new BordereauEntreeSortieViewModel();

            mclass._BordereauEntreeSortie = new BordereauEntreeSortie();            
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormBordereauEntreeSortie", Model = mclass, };

        }
        public ActionResult onAddTransfert()
        {
            BordereauEntreeSortieViewModel mclass = new BordereauEntreeSortieViewModel();

            mclass._BordereauEntreeSortie = new BordereauEntreeSortie();
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormTransfert", Model = mclass, };

        }
        public ActionResult onAddReception()
        {
            BordereauEntreeSortieViewModel mclass = new BordereauEntreeSortieViewModel();

            mclass._BordereauEntreeSortie = new BordereauEntreeSortie();
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormReception", Model = mclass, };

        }

        public ActionResult onEdit(string ItemSelected)
        {            
            BordereauEntreeSortieViewModel mclass = new BordereauEntreeSortieViewModel();
            BordereauEntreeSortie mBordereau = JSON.Deserialize<BordereauEntreeSortie>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            mclass._BordereauEntreeSortie = mBordereau;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormBordereauEntreeSortie", Model = mclass, };

        }

        public ActionResult onConsult(string ItemSelected)
        {
            BordereauEntreeSortieViewModel mclass = new BordereauEntreeSortieViewModel();
            BordereauEntreeSortie mBordereau = JSON.Deserialize<BordereauEntreeSortie>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            mclass._BordereauEntreeSortie = mBordereau;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormBordereauEntreeSortie", Model = mclass, };

        }


        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne, string ItemMagasin, string ItemExportateur, string ItemMouvementType, string ItemSens, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatut)
        {
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
            int MagasinID = GetCriteriaValue(ItemMagasin);            
            int ExportateurID = GetCriteriaValue(ItemExportateur);
            int MouvementTypeID = string.IsNullOrEmpty(ItemMouvementType) ? -2 : int.Parse(ItemMouvementType); 
            int Sens = string.IsNullOrEmpty(ItemSens) ? -2 : int.Parse(ItemSens);
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            //int certificationID = GetCriteriaValue(ItemCertification);
            int statut = string.IsNullOrEmpty(ItemStatut) ? -1 : int.Parse( ItemStatut);
            var mListe = (new BordereauEntreeSortie()).fnSelect(ItemCampagne, MagasinID, ExportateurID, StartDate, EndDate, Sens, MouvementTypeID, statut);
            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemMagasin, string ItemExportateur, string ItemMouvementType, string ItemSens, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatut)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeBordereauxES");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCampagne",ItemCampagne),
                                    new Ext.Net.Parameter("ItemMagasin",ItemMagasin),                                    
                                    new Ext.Net.Parameter("ItemExportateur",ItemExportateur),
                                    new Ext.Net.Parameter("ItemMouvementType",ItemMouvementType),
                                    new Ext.Net.Parameter("ItemSens",ItemSens),
                                    new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatut",ItemStatut)
                                });

                //string title = X.GetCmp<FormPanel>("BordereauEntreeSortieCP").Title;

                //title += "Crop Year = " + X.GetCmp<ComboBox>("cmbCampagne").RawValue.ToString();

                //title += ", Source = " + X.GetCmp<ComboBox>("cmbMagasinSource").SelectedItem.Text;
                //title += ", Dest = " + X.GetCmp<ComboBox>("cmbMagasinDestination").SelectedItem.Text;

                //title += ", Exporter = " + X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text;

                //title += ", Type = " + X.GetCmp<ComboBox>("cmbMouvementType").SelectedItem.Text;

                ////title += ", Status = " + X.GetCmp<ComboBox>("cmbFiltreStatut").SelectedItem.Text;

                //title += ", Direction = " + X.GetCmp<ComboBox>("cmbSens").SelectedItem.Text;

                ////title += ", Certification = " + X.GetCmp<ComboBox>("cmbFiltreCertification").SelectedItem.Text;

                //title += ", From " + X.GetCmp<DateField>("txtDateDebut").RawText.ToString() + "To " + X.GetCmp<DateField>("txtDateFin").RawText.ToString();

                //X.GetCmp<FormPanel>("BordereauEntreeSortieCP").Title = title;

                X.GetCmp<FormPanel>("BordereauEntreeSortieCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Movement Sheet : Data Validation",
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
                BordereauEntreeSortie mClass = new BordereauEntreeSortie();
                bool result = true;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("txtBordereauID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("UpdateFormMethod : Movement Sheet - load failed.");

                }

                mClass = MapFormToObject(mClass);

                result = mClass.fnUpdate();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListeBordereauxES");
                    GridPanel mGrid = X.GetCmp<GridPanel>("grpListeBordereauxES");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowBordereauES").Select(0);
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
                    Title = "Movement Sheet : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        private BordereauEntreeSortie MapFormToObject(BordereauEntreeSortie mClass)
        {
            Campagne campagne = new Campagne();
            campagne.Designation = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text.ToString();
            mClass.Campagne = campagne;

            Magasin magasin = new Magasin();
            magasin.ID = int.Parse(GetFormValue("cmbMagasin"));
            magasin.Designation = X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Text.ToString();
            mClass.Magasin = magasin;
           
            Exportateur exportateur = new Exportateur();
            exportateur.ID = int.Parse(GetFormValue("cmbExportateur"));
            exportateur.Nom = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text.ToString();
            mClass.Exportateur = exportateur;
            mClass.DateBordereau = DateTime.Parse(X.GetCmp<DateField>("txtDateBordereau").RawText.ToString());    
            mClass.Sens = Int16.Parse(X.GetCmp<ComboBox>("cmbSens").SelectedItem.Value.ToString());
            mClass.Numero = X.GetCmp<TextField>("txtNumeroBordereau").Text;
            
            mClass.Reference = X.GetCmp<TextField>("txtReference").Text;
                    
            MouvementStockType mouvementType = new MouvementStockType();
            mouvementType.ID = int.Parse(GetFormValue("cmbMouvementType"));
            mouvementType.Designation = X.GetCmp<ComboBox>("cmbMouvementType").SelectedItem.Text.ToString();
            mClass.MouvementStockType = mouvementType;
                        
            mClass.NumeroTransfert = X.GetCmp<TextField>("txtNumeroTransfert").Text;

            SacType sacType = new SacType();
            sacType.ID = int.Parse(GetFormValue("cmbSacType"));
            sacType.Designation = X.GetCmp<ComboBox>("cmbSacType").SelectedItem.Text.ToString();
            mClass.SacType = sacType;

            if (X.GetCmp<TextField>("txtQuantite").Text != string.Empty) mClass.Quantite = decimal.Parse(X.GetCmp<TextField>("txtQuantite").Text);
            if (X.GetCmp<TextField>("txtPoidsBrut").Text != string.Empty) mClass.PoidsBrut = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrut").Text);
            if (X.GetCmp<TextField>("txtTareSacs").Text != string.Empty) mClass.TareSacs = decimal.Parse(X.GetCmp<TextField>("txtTareSacs").Text);
            if (X.GetCmp<TextField>("txtTarePalette").Text != string.Empty) mClass.TarePalette = decimal.Parse(X.GetCmp<TextField>("txtTarePalette").Text);
            if (X.GetCmp<TextField>("txtPoidsNetLivre").Text != string.Empty) mClass.PoidsLivre = decimal.Parse(X.GetCmp<TextField>("txtPoidsNetLivre").Text);
            if (X.GetCmp<TextField>("txtRetention").Text != string.Empty) mClass.RetentionPoids = decimal.Parse(X.GetCmp<TextField>("txtRetention").Text);
            if (X.GetCmp<TextField>("txtPoidsNetAccepte").Text != string.Empty) mClass.PoidsNetAccepte = decimal.Parse(X.GetCmp<TextField>("txtPoidsNetAccepte").Text);

            //mClass.Prix = null;
            //if (X.GetCmp<TextField>("txtPrix").Text != string.Empty && (decimal.Parse(X.GetCmp<TextField>("txtPrix").Text) > 0)) mClass.Prix = decimal.Parse(X.GetCmp<TextField>("txtPrix").Text);
            //mClass.Montant = null;
            //if (X.GetCmp<TextField>("txtMontant").Text != string.Empty && (decimal.Parse(X.GetCmp<TextField>("txtMontant").Text) > 0)) mClass.Montant = decimal.Parse(X.GetCmp<TextField>("txtMontant").Text);


            mClass.Commentaire = X.GetCmp<TextField>("txtComment").Text;
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;

        }

        public ActionResult OnCancel(string ItemSelected)
        {
            try
            {
                BordereauEntreeSortie bordereau = JSON.Deserialize<BordereauEntreeSortie>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = bordereau.fnGet(bordereau.ID);

                if (!result)
                    throw new Exception("OnCancel : Movement Sheet loading failed.");

                bordereau.UtilisateurModification = (string)Session["userName"];

                if (bordereau.Desactive)
                    result = bordereau.fnActivate();
                else
                    result = bordereau.fnDeActivate();

                if (!result)
                    throw new Exception("OnCancel : Movement Sheet, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeBordereauxES");

                    ModelProxy mProxy = mstore.GetById(bordereau.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(bordereau);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Movement Sheet : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnPrintList()
        {
            Parametres mParam = new Parametres(0);

            ViewData["Campagne"] = mParam.Campagne;
            ViewData["Exportateur"] = mParam.Exportateur.ID;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "BordereauEntreeSortie_Print", ViewData = ViewData };
        }

        public ActionResult PrintList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;

                report = new rptBordereauEntreeSortieList() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["campagneID"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                report.Parameters["paramExportateur"].Value = int.Parse(X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Value);
                report.Parameters["paramExportateurNom"].Value = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text;

                report.Parameters["paramMagasin"].Value = int.Parse(X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Value);
                report.Parameters["paramMagasinText"].Value = X.GetCmp<ComboBox>("cmbMagasin").SelectedItem.Text;

                report.Parameters["paramDirection"].Value = int.Parse(X.GetCmp<ComboBox>("cmbDirection").SelectedItem.Value);
                report.Parameters["paramDirectionText"].Value = X.GetCmp<ComboBox>("cmbDirection").SelectedItem.Text;

                report.Parameters["paramMouvementType"].Value = int.Parse(X.GetCmp<ComboBox>("cmbMouvementType").SelectedItem.Value);
                report.Parameters["paramMouvementTypeText"].Value = X.GetCmp<ComboBox>("cmbMouvementType").SelectedItem.Text;

                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("dtpStartDate").RawText);
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("dtpEndDate").RawText);

                report.Parameters["paramStatut"].Value = int.Parse(X.GetCmp<ComboBox>("CmbStatut").SelectedItem.Value);
                report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("CmbStatut").SelectedItem.Text;

                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/BordereauEntreeSortie/ViewList', this, 'List Of Movement Sheet',''),App.BordereauEntreeSortie_Print.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Movement Sheet : Data Validation",
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
            X.GetCmp<Window>("FormBordereauEntreeSortie").Close();
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