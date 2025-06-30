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
using Tms.Classes.Business;
using Tms.Classes.Business.stock;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class LotProductionController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        // GET: LotProduction
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);
            string campagne = mParam.Campagne;
            int exportateur = mParam.Exportateur.ID;
            X.GetCmp<ComboBox>("cmbFiltreCampagne").SetValue(campagne);
            X.GetCmp<ComboBox>("cmbFiltreExportateur").SetValue(exportateur);

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("txtFiltreDateDebut").RawText = StartDate;
            X.GetCmp<DateField>("txtFiltreDateFin").RawText = EndDate;            

            X.GetCmp<FormPanel>("LotCP").SetTitle("Campagne : " + campagne + ", Exportateur : "+ mParam.Exportateur.Nom + ", Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{7A26652A-6D17-4378-9F0D-1CE8DF9453AC}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{C6AC056F-C60D-4B54-8A73-93E0207ECCE5}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{F663846F-C832-402D-9B85-F6EB1E6132EF}")))
                X.GetCmp<MenuItem>("mnuPrintLot").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintLot").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{EC36A28C-B082-44E9-B3A3-69ED9F719E86}")))
                X.GetCmp<MenuItem>("mnuExportLot").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportLot").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{1F600E40-B52A-46B5-9C8C-217569876F00}")))
                X.GetCmp<Hidden>("lthiddenPermModify").SetValue(true);
            else
                X.GetCmp<Hidden>("lthiddenPermModify").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{E3AFFC6C-D617-42F0-8889-DE0B8913C4FC}")))
                X.GetCmp<Hidden>("lthiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("lthiddenPermDesactiver").SetValue(false);   

            #endregion

            return View();
        }

        public ActionResult LoadLotsByProduction(string ItemProduction)
        {
            Guid productionID = Guid.Empty;
            if (!string.IsNullOrEmpty(ItemProduction)) productionID = Guid.Parse(ItemProduction);
            List<DataPersist> mliste = new Lot().fnSelectByEmpotage(productionID);

            return this.Store(mliste);
        }

        public ActionResult LoadLotsByStuffing(string ItemEmpotage)
        {
            Guid empotageID = Guid.Empty;
            if (!string.IsNullOrEmpty(ItemEmpotage)) empotageID = Guid.Parse(ItemEmpotage);
            List<DataPersist> mliste = new Lot().fnSelectByEmpotage(empotageID);

            return this.Store(mliste);
        }

        [HttpPost]
        public ActionResult LoadLotsByNumero(string query)
        {
            try
            {
                Lot mLot = new Lot();          
                List<DataPersist> mliste = new Lot().fnSelectByNumero(query);

                //if (mliste.Count > 0)
                //    mLot = mliste[0] as Lot;

                return this.Store(mliste);
            }
            catch (Exception Ex)
            {
                throw;
            }           
        }

        [HttpPost]
        public ActionResult LoadLotsForAjustement(string query, string statut)
        {
            try
            {
                List<Lot> mLot = new List<Lot>();
                List<DataPersist> mliste = new Lot().fnSelectForStuffingAjustement(query);
                if (statut == "-1")
                    mLot = mliste.Cast<Lot>().Where(x => x.Statut == "NA").ToList();                                 
                else
                    mLot = mliste.Cast<Lot>().Where(x => x.Statut == "PE").ToList();

                return this.Store(mLot);
            }
            catch (Exception Ex)
            {
                throw;
            }

        }

        [HttpPost]
        public ActionResult LoadLotsForReCleaning(string query)
        {
            try
            {
                Lot mLot = new Lot();
                List<DataPersist> mliste = new Lot().fnSelectForReCleaning(query);

                //if (mliste.Count > 0)
                //    mLot = mliste[0] as Lot;

                return this.Store(mliste);
            }
            catch (Exception Ex)
            {
                throw;
            }

        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("LotCP");
            mform.ToggleCollapse();            
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne, string ItemExportateur, string ItemType, string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatut)
        {
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "-1" : ItemCampagne;
            int certificationID = GetCriteriaValue(ItemCertification);
            int TypeID = GetCriteriaValue(ItemType);
            int ExportateurID = GetCriteriaValue(ItemExportateur);
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string Statut = string.IsNullOrEmpty(ItemStatut) ? "-1" : ItemStatut;
            var mListe = (new Lot()).fnSelect(Campagne, ExportateurID,certificationID, TypeID, StartDate, EndDate,Statut);

            return this.Store(mListe);
        }

        public ActionResult SelectForAnalysis(StoreRequestParameters parameters, string ItemCampagne, string ItemExportateur, string ItemType, string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd)
        {
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
            int certificationID = GetCriteriaValue(ItemCertification);
            int TypeID = GetCriteriaValue(ItemType);
            int ExportateurID = GetCriteriaValue(ItemExportateur);
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            //int Statut = string.IsNullOrEmpty(ItemStatut) ? -1 : int.Parse(ItemStatut);
            var mListe = (new Lot()).fnSelectForAnalysis(Campagne, ExportateurID, certificationID, TypeID, StartDate, EndDate);

            return this.Store(mListe);
        }

        public ActionResult SelectForChemicalAnalysis(StoreRequestParameters parameters, string ItemCampagne, string ItemExportateur, string ItemType, string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd, string ItemLots = "")
        {
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "-1" : ItemCampagne;
            int certificationID = GetCriteriaValue(ItemCertification);
            int TypeID = GetCriteriaValue(ItemType);
            int ExportateurID = GetCriteriaValue(ItemExportateur);
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            //int Statut = string.IsNullOrEmpty(ItemStatut) ? -1 : int.Parse(ItemStatut);

            var ListeDesLots = (new Lot()).fnSelectForChemicalAnalysis(Campagne, ExportateurID, certificationID, TypeID, StartDate, EndDate);
            
            HashSet<string> LotsAdded = new HashSet<string>(ItemLots.Split(','));            
            //List<Lot> mlist = new List<Lot>();
            List<Lot> mListe = ListeDesLots.Cast<Lot>().ToList();

            mListe = mListe.Where(l => !LotsAdded.Contains(l.NumeroLot)).ToList();
            return this.Store(mListe);
        }


        public ActionResult OnRefresh(string ItemCampagne, string ItemExportateur, string ItemType, string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatut)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeLots");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCampagne",ItemCampagne),
                                    new Ext.Net.Parameter("ItemExportateur",ItemExportateur),
                                    new Ext.Net.Parameter("ItemType",ItemType),
                                    new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemCertification",ItemCertification),
                                    new Ext.Net.Parameter("ItemStatut",ItemStatut)
                                });

                X.GetCmp<FormPanel>("LotCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - Lot : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }        

        public ActionResult onAdd()
        {          
            LotViewModel mclass = new LotViewModel();
            Parametres mParam = new Parametres(0);
            mclass._Lot = new Lot();
            mclass._DefaultCampagne = mParam.Campagne;
            mclass._DefaultExportateur = mParam.Exportateur.ID;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLot", Model = mclass, };
        }

        public ActionResult onEdit(string ItemSelected)
        {
            LotViewModel mclass = new LotViewModel();

            mclass._Lot = new Lot();
            Parametres mParam = new Parametres(0);
            mclass._DefaultCampagne = mParam.Campagne;
            mclass._DefaultExportateur = mParam.Exportateur.ID;

            mclass._Lot = JSON.Deserialize<Lot>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLot", Model = mclass, };
        }

        public ActionResult onConsult(string ItemSelected)
        {
            LotViewModel mclass = new LotViewModel();

            mclass._Lot = new Lot();
            Parametres mParam = new Parametres(0);
            mclass._DefaultCampagne = mParam.Campagne;
            mclass._DefaultExportateur = mParam.Exportateur.ID;

            mclass._Lot = JSON.Deserialize<Lot>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormLot", Model = mclass, };
        }

        public ActionResult OnPrintList()
        {
            Lot mclass = new Lot();            
            Parametres mParam = new Parametres(0);

            ViewData["Campagne"] = mParam.Campagne;
            ViewData["Exportateur"] = mParam.Exportateur.ID;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "LotProduction_Print", ViewData = ViewData};
        }

        public ActionResult OnPrintLotList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {               
                XtraReport report = null;

                report = new rptLotsList() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["campagneID"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;
                report.Parameters["paramCampagneText"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                report.Parameters["paramExportateur"].Value = int.Parse(X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Value);
                report.Parameters["paramExportateurNom"].Value = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text;

                report.Parameters["paramCertification"].Value = int.Parse(X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Value);
                report.Parameters["paramCertificationText"].Value = X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Text;

                report.Parameters["paramLotType"].Value = int.Parse(X.GetCmp<ComboBox>("cmbTypeLot").SelectedItem.Value);
                report.Parameters["paramLotTypeText"].Value = X.GetCmp<ComboBox>("cmbTypeLot").SelectedItem.Text;

                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateDebut").RawText);
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateFin").RawText);

                report.Parameters["paramStatut"].Value = int.Parse(X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Value);
                report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Text;

                Session["report"] = report;


                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/LotProduction/ViewList', this, 'List Of Lots',''),App.LotProduction_Print.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Livraison : Data Validation",
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
            //report = new rptLotsList() as XtraReport;

            //report.DataSource = DevExpressReportDs.SetDataSource(report);
            //report.Parameters["campagneID"].Value = Session["CampagneID"];
            //report.Parameters["paramCampagneText"].Value = Session["paramCampagneText"];

            //report.Parameters["paramExportateur"].Value = int.Parse(Session["paramExportateur"].ToString());
            //report.Parameters["paramExportateurNom"].Value = Session["paramExportateurNom"];

            //report.Parameters["paramCertification"].Value = int.Parse(Session["paramCertification"].ToString());
            //report.Parameters["paramCertificationText"].Value = Session["paramCertificationText"];

            //report.Parameters["paramLotType"].Value = int.Parse(Session["paramLotType"].ToString());
            //report.Parameters["paramLotTypeText"].Value = Session["paramLotTypeText"];

            //report.Parameters["paramDateDebut"].Value = DateTime.Parse(Session["paramDateDebut"].ToString());
            //report.Parameters["paramDateFin"].Value = DateTime.Parse(Session["paramDateFin"].ToString());

            //report.Parameters["paramStatut"].Value = int.Parse(Session["paramStatut"].ToString());
            //report.Parameters["paramStatutText"].Value = Session["paramStatutText"];            

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }


        private Lot MapFormToObject(Lot mClass)
        {
            Campagne campagne = new Campagne();
            campagne.Designation = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text.ToString();
            mClass.Campagne = campagne;

            Exportateur exportateur = new Exportateur();
            exportateur.ID = int.Parse(GetFormValue("cmbExportateur"));
            exportateur.Nom = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text.ToString();
            mClass.Exportateur = exportateur;

            Certification certification = null;

            if (!string.IsNullOrEmpty(GetFormValue("cmbCertification")))
            {
                certification = new Certification();
                certification.ID = int.Parse(GetFormValue("cmbCertification"));
                certification.Designation = X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Text.ToString();
            }
            mClass.Certification = certification;

            OrdreProduction production = null;
            //production.fnGetByNumber(X.GetCmp<TextField>("txtOrdreProduction").Text);
            if (!string.IsNullOrEmpty(GetFormValue("txtOrdreProduction")))
            {
                production = new OrdreProduction();
                production.ID = Guid.Parse(GetFormValue("txtOrdreProductionID"));
                production.NumeroProduction = X.GetCmp<TextField>("txtOrdreProduction").Text;
                mClass.Production = production;
            }           
            
            mClass.NumeroLot = X.GetCmp<TextField>("TxtNumero").Text; 
            mClass.DateLot = DateTime.Parse(X.GetCmp<DateField>("TxtDateLot").RawText.ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
            mClass.EstQueue = bool.Parse(X.GetCmp<Checkbox>("ChkEstQueue").Value.ToString());
            mClass.EstReusine = bool.Parse(X.GetCmp<Checkbox>("ChkEstReusine").Value.ToString());
            if (mClass.EstReusine)
            {
                mClass.NumeroLot = X.GetCmp<TextField>("TxtNumero").Text;
                bool result = new Lot().fnGetReusinageByNumero(mClass.NumeroLot);
                if (!result)
                    throw new Exception("Lot : Lot's Number Not Found");
            }
                
            mClass.EstManuel = true;

            LotType lottype = new LotType();
            lottype.ID = int.Parse(GetFormValue("cmbTypeLot"));
            lottype.Designation = X.GetCmp<ComboBox>("cmbTypeLot").SelectedItem.Text.ToString();
            mClass.LotType = lottype;

            mClass.NumeroLot = X.GetCmp<TextField>("TxtNumero").Text;

            if (X.GetCmp<TextField>("txtNombreSacs").Text != string.Empty) mClass.NombreSacs = int.Parse(X.GetCmp<TextField>("txtNombreSacs").Text.Replace(" ",""));
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
                Lot lot = JSON.Deserialize<Lot>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = lot.fnGet(lot.ID);

                if (!result)
                    throw new Exception("OnCancel : Movement Sheet loading failed.");

                lot.UtilisateurModification = (string)Session["userName"];

                if (lot.Desactive)
                    result = lot.fnActivate();
                else
                    result = lot.fnDeActivate();

                if (!result)
                    throw new Exception("OnCancel : Production - Lot, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeLots");

                    ModelProxy mProxy = mstore.GetById(lot.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(lot);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - Lot : Cancel",
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
                Lot mClass = new Lot();
                bool result = true;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtLotId")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("UpdateFormMethod : Production - Lot - load failed.");

                }

                mClass = MapFormToObject(mClass);
                result = mClass.fnUpdate();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListeLots");                    

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowLot").Select(0);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormLot").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - Lot : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnSelectProduction()
        {
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Lot_SelectProduction" };
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
                            X.GetCmp<Window>("Lot_SelectProduction").Close();                            
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
                        X.GetCmp<Window>("Lot_SelectProduction").Close();
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
            X.GetCmp<Window>("FormLot").Close();
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