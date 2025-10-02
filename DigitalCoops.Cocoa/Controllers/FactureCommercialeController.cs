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
using Tms.Classes.Business.Sales;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.Sales;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class FactureCommercialeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        // GET: FactureCommerciale
        public ActionResult Index()
        {
            var mParam = (new Parametres()).fnSelect();

            Parametres mclass = new Parametres();
            mclass = mParam[0] as Parametres;

            Campagne mCampagne = new Campagne();

            X.GetCmp<ComboBox>("cmbCropYear").SetValue(mclass.Campagne);

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("dtpStartDate").RawText = StartDate;
            X.GetCmp<DateField>("dtpEndDate").RawText = EndDate;

            string Exportateur = "{Tous}";

            string Certification = "{Tous}";

            X.GetCmp<FormPanel>("CommercialInvoiceCriteriaPanel").SetTitle("Campagne : " + mclass.Campagne + " | Exportateur : " + Exportateur + " | Certification : " + Certification + " | Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();

            if (HasAccess.fnGetUserAccessStatus("{4AF5A690-2701-4E35-BB53-41219F7E3338}", UserName) == false)
                X.GetCmp<MenuItem>("btnNew").Disable();
            else
                X.GetCmp<MenuItem>("btnNew").Enable();

            if (HasAccess.fnGetUserAccessStatus("{7192C195-B7F0-4399-9940-FA80429C4D8D}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExport").Disable();
            else
                X.GetCmp<MenuItem>("mnuExport").Enable();

            if (HasAccess.fnGetUserAccessStatus("{1C1CED1F-7519-4EEF-94A8-A8E4246C36B0}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintHistory").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintHistory").Enable();

            X.GetCmp<Hidden>("fchiddenPermApprove").SetValue(HasAccess.fnGetUserAccessStatus("{549B3D69-4D02-41BD-B5FB-31C660CF0BF6}", UserName));
            X.GetCmp<Hidden>("fchiddenPermCancel").SetValue(HasAccess.fnGetUserAccessStatus("{BB0C3907-1FC5-4100-9CA1-990B087FDBE0}", UserName));
            X.GetCmp<Hidden>("fchiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{7192C195-B7F0-4399-9940-FA80429C4D8D}", UserName));
            X.GetCmp<Hidden>("fchiddenPermPrintInvoice").SetValue(HasAccess.fnGetUserAccessStatus("{34505362-CA71-4198-8384-5E47699B080F}", UserName));
            X.GetCmp<Hidden>("fchiddenPermPrintHistory").SetValue(HasAccess.fnGetUserAccessStatus("{1C1CED1F-7519-4EEF-94A8-A8E4246C36B0}", UserName));
            X.GetCmp<Hidden>("fchiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{C3A4CE6F-AF4F-4E0E-9E7C-287C2E1D57BC}", UserName));
            X.GetCmp<Hidden>("fchiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{4AF5A690-2701-4E35-BB53-41219F7E3338}", UserName));
            X.GetCmp<Hidden>("fchiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{29E0B51F-A3FB-4FA2-AD09-125E357336AF}", UserName));
            #endregion
            return View();
        }

        public ActionResult onCreate()
        {
            FactureCommercialeViewModel mclass = new FactureCommercialeViewModel();

            mclass._FactureCommerciale = new FactureCommerciale();


            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;


            return new Ext.Net.MVC.PartialViewResult { ViewName = "FactureCommerciale_Detail", Model = mclass };
        }
        public ActionResult onEdit(string ItemSelected)
        {
            FactureCommerciale mclass = JSON.Deserialize<FactureCommerciale>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            FactureCommercialeViewModel viewModel = new FactureCommercialeViewModel();

            viewModel._FactureCommerciale = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;


            return new Ext.Net.MVC.PartialViewResult { ViewName = "FactureCommerciale_Detail", Model = viewModel };
        }
        public ActionResult onConsult(string ItemSelected)
        {

            FactureCommerciale mclass = JSON.Deserialize<FactureCommerciale>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            FactureCommercialeViewModel viewmodel = new FactureCommercialeViewModel();
            viewmodel._FactureCommerciale = mclass;
            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FactureCommerciale_Detail", Model = viewmodel };
        }
        public ActionResult OnApprove(string ItemSelected)
        {

            FactureCommerciale mclass = JSON.Deserialize<FactureCommerciale>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            FactureCommercialeViewModel viewModel = new FactureCommercialeViewModel();

            viewModel._FactureCommerciale = mclass;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FactureCommerciale_Detail", Model = viewModel };

        }

        public ActionResult OnCancel(string ItemSelected)
        {
            try
            {
                FactureCommerciale mClass = JSON.Deserialize<FactureCommerciale>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,NullValueHandling = NullValueHandling.Ignore });

                Guid mID = mClass.ID;
                bool result = mClass.fnGet(mID);

                if (!result)
                    throw new Exception("OnCancel : Facture Commerciale loading failed.");

                bool resultFinCancel = false;
                mClass.UtilisateurModification = (string)Session["userName"];
                resultFinCancel = mClass.fnCancel();

                if (resultFinCancel)
                {
                    Store mstore = X.GetCmp<Store>("storeListeCommercialInvoice");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();

                    //DeselectGridRows();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture Commerciale : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult LoadOfCommercialInvoice(StoreRequestParameters parameters, string ItemCropYear, string ItemExporter, string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {

            int ExportateurID = GetCriteriaValue(ItemExporter);
            int CertificationID = GetCriteriaValue(ItemCertification);
            string Campagne = string.IsNullOrEmpty(ItemCropYear) ? "" : ItemCropYear;

            if (!string.IsNullOrEmpty(ItemCropYear) && ItemCropYear.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string Status = ItemStatus;

            var mListe = (new FactureCommerciale()).fnSelect(Campagne, ExportateurID, CertificationID, StartDate, EndDate, Status);
            //var paging = GridStorePaging.SetRangePlants(parameters, mListe);

            //return this.Store(paging);
            return this.Store(mListe);
        }
        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CommercialInvoiceCriteriaPanel");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult OnRefresh(string ItemCropYear,  string ItemExporter, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeCommercialInvoice");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCropYear"   ,ItemCropYear),
                                    new Ext.Net.Parameter("ItemExporter"   ,ItemExporter),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus"        ,ItemStatus)
                                });

                // collapse criterias areas
                X.GetCmp<FormPanel>("CommercialInvoiceCriteriaPanel").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture Commerciale : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitApprovalMethod()
        {

            try
            {
                FactureCommerciale mClass = new FactureCommerciale();
                mClass.IsNew = false;

                bool result = mClass.fnGet(Guid.Parse(GetFormValue("hiddenID")));

                if (!result)
                    throw new Exception("OnApprove : Commercial invoice loading failed.");



                bool resultFinApprobation = false;

                if (mClass.Statut == "NA")
                {
                    mClass.UtilisateurApprobation = (string)Session["userName"];
                    resultFinApprobation = mClass.fnApprove();
                }

                if (resultFinApprobation)
                {

                    Store mstore = X.GetCmp<Store>("storeListeCommercialInvoice");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();



                    X.GetCmp<Window>("FactureCommerciale_Detail").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture Commerciale : SubmitApprovalMethod",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult LoadListOfValorisation(string ItemFacture, string ItemExecMode)
        {
            List<DataPersist> myList = new List<DataPersist>();
            if (!string.IsNullOrEmpty(ItemFacture))
            {

                if (ItemExecMode == "AddNew")
                {
                    myList = new FactureCommercialePrefinancement().fnSelect(new Guid());
                }
                else
                {
                    myList = new FactureCommercialePrefinancement().fnSelect(Guid.Parse(ItemFacture));
                }

                FactureCommercialePrefinancement mClass = new FactureCommercialePrefinancement();
                if (myList.Count > 0)
                    mClass = myList[0] as FactureCommercialePrefinancement;
                else myList = new List<DataPersist>();

            }
            else
            {
                Store mstore = X.GetCmp<Store>("storeListValorisation");
                mstore.RemoveAll();
                myList = null;
            }

            return this.Store(myList);
        }

        public ActionResult LoadListOfDeduction(string ItemFacture, string ItemExecMode)
        {
            List<DataPersist> myList = new List<DataPersist>();
            if (!string.IsNullOrEmpty(ItemFacture))
            {

                if (ItemExecMode == "AddNew")
                {
                    myList = new FactureCommercialeDeduction().fnSelect(new Guid());
                }
                else
                {
                    myList = new FactureCommercialeDeduction().fnSelect(Guid.Parse(ItemFacture));
                }

                FactureCommercialeDeduction mClass = new FactureCommercialeDeduction();
                if (myList.Count > 0)
                    mClass = myList[0] as FactureCommercialeDeduction;
                else myList = new List<DataPersist>();

            }
            else
            {
                Store mstore = X.GetCmp<Store>("storeListDeduction");
                mstore.RemoveAll();
                myList = null;
            }

            return this.Store(myList);
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

            var mListe = (new Embarquement()).fnSelectForCommercialInvoice("{Tous}", ExportateurID, -1, -1, -1, null, null, 0);

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
                    X.GetCmp<Hidden>("txtEmbarquementID").Value = Item.ID;
                    X.GetCmp<Hidden>("txtExportateurID").Value = Item.Exportateur.ID;
                    X.GetCmp<Hidden>("txtDomiciliationID").Value = Item.Domiciliation.ID;
                    X.GetCmp<TextField>("txtDomiciliation").Value = Item.Domiciliation.Nom;

                    X.GetCmp<TextField>("txtNumberOfBags").Value = Item.NbreSacs;
                    X.GetCmp<TextField>("txtQuantity").Value = Item.QuantiteAsString;
                    X.GetCmp<TextField>("txtGrossWeight").Value = Item.PoidsBrutAsString;
                    X.GetCmp<TextField>("txtNetWeight").Value = Item.PoidsNetAsString;
                    X.GetCmp<TextField>("txtPriceCAF").Value = Item.Contrat.Prix;
                    X.GetCmp<TextField>("txtPriceFob").Value = Item.Contrat.PrixFob;
                    decimal MontantBrut = Math.Round((Item.Contrat.PrixFob * (Item.PoidsNet / 1000)),3);
                    decimal MontantAT =  Math.Round((MontantBrut * 99) / 100,3);
                    X.GetCmp<TextField>("txtGrossAmount").Value = string.Format("{0:#,##0.###}", MontantBrut).TrimStart();
                    X.GetCmp<TextField>("txtAmountAT").Value = string.Format("{0:#,##0.###}", MontantAT).TrimStart();
                    
                    X.GetCmp<Button>("btnAddPrefinancing").Enable();
                    X.GetCmp<Button>("btnAddOtherDeduction").Enable();
                    X.GetCmp<Button>("BtnOkDetail").Enable();

                    Store mstore = X.GetCmp<Store>("storeListDeduction");
                    mstore.RemoveAll();
                    var mTypes = (new FactureCommercialeDeductionType()).fnSelect(0, 1);
                    if (mTypes.Count > 0)
                    {
                        var i = 0;
                        foreach (FactureCommercialeDeductionType item in mTypes)
                        {
                            i += 1;
                            FactureCommercialeDeduction mValeur = new FactureCommercialeDeduction();
                            mValeur.ID = Guid.NewGuid();
                            mValeur.FactureCommercialeDeductionType = new FactureCommercialeDeductionType();
                            mValeur.FactureCommercialeDeductionType = item;
                            mValeur.Montant = item.Taux;
                            mValeur.Taux = item.Taux;
                            mValeur.IsNew = true;
                            mValeur.IsAuto = true;
                            mstore.Insert(i, mValeur);

                        }

                    }

                        X.GetCmp<Window>("ListOfShipments").Close();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Select Shipment : SubmitArticleForm",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult OnAddPreFinancing(string ItemExportateur)
        {
            FactureCommercialePrefinancementViewModel viewModel = new FactureCommercialePrefinancementViewModel();
            viewModel._FactureCommercialePrefinancement = new FactureCommercialePrefinancement();
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            ViewData["exportateurID"] = ItemExportateur;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Prefinancement_Detail", Model = viewModel, ViewData = ViewData};

        }

        public ActionResult OnEditPreFinancing(string ItemExportateur, string ItemSelected, string ItemExecMode)
        {
            FactureCommercialePrefinancement mclass = JSON.Deserialize<FactureCommercialePrefinancement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            FactureCommercialePrefinancementViewModel viewModel = new FactureCommercialePrefinancementViewModel();
            decimal montantAlloué = 0;
            montantAlloué = mclass.MontantCFA;

            Prefinancement mPrefinancement = new Prefinancement();
            mPrefinancement.fnGetForInvoice(mclass.Prefinancement.ID);

            if (mPrefinancement.Montant != (mclass.Prefinancement.Solde + montantAlloué) )
            {
                mclass.Prefinancement.Solde = mPrefinancement.Montant - montantAlloué;
            }
            else
            {
                mclass.Prefinancement.Solde = mPrefinancement.Montant;
            }

            

            if (mclass.Prefinancement.ID != mclass.ID)
            {
                
                //mclass.Prefinancement.Solde = mP.Solde + mclass.MontantCFA;

            }

            //if(ItemExecMode == "Update")
            //{
            //    if(mclass.Prefinancement.ID != mclass.ID)
            //    {
            //        Prefinancement mP = new Prefinancement();
            //        mP.fnGetForInvoice(mclass.Prefinancement.ID);
            //        mclass.Prefinancement.Solde = mP.Solde + mclass.MontantCFA;

            //    }
            //}

            viewModel._FactureCommercialePrefinancement = mclass;
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Prefinancement_Detail", Model = viewModel };

        }

        public ActionResult OnRemovePrefinancing(string ItemSelected, string ItemExecMode)
        {

            try
            {

                FactureCommercialePrefinancement mClass = JSON.Deserialize<FactureCommercialePrefinancement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                bool result = false;
                if (ItemExecMode == "Update")
                {
                    if (mClass.Prefinancement.ID != mClass.ID)
                    {
                        mClass.RowVersionKey = Convert.FromBase64String(mClass.RowVersionKey.ToString());
                        result = mClass.fnDeActivate();


                    }
                }
                else
                {
                    result = true;
                }

                if(result == true)
                {
                    Store mstore = X.GetCmp<Store>("storeListValorisation");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    if (mClass.Prefinancement.ID == mClass.ID)
                    {
                        mProxy.Drop();
                    }
                    else
                    {
                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }
                   

                    X.GetCmp<RowSelectionModel>("rowSelectionValorisation").DeselectAll();
                }
                



            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prefinancing : Retirer",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult LoadAvailablePrefinancing(string ItemExecMode ,string ItemExportateurID)
        {
            int exp = 0;

            if (!string.IsNullOrEmpty(ItemExportateurID) )
                exp = int.Parse(ItemExportateurID);

            List<DataPersist> mList = new Prefinancement().fnSelectForInvoice("{all}", exp, -1, null, null, "AP");
            return this.Store(mList);
        }

        public ActionResult OnSelectPrefinancing(string ItemExportateur)
        {
            PrefinancementViewModel mclass = new PrefinancementViewModel();
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            ViewData["Exportateur"] = ItemExportateur;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Select_Prefinancement", Model = mclass , ViewData = ViewData};


        }

        public ActionResult OnRefreshForAvailablePrefinancings(string ItemExportateur, string ItemType)
        {
            Store mstore = X.GetCmp<Store>("storeListPrefinancing");
            mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemExportateur"   ,ItemExportateur),
                                    new Ext.Net.Parameter("ItemType"   ,ItemType)
                                   
                                });

            return this.Direct();
        }


        public ActionResult LoadListOfAvailablePrefinancings(string ItemExportateur, string ItemType)
        {

            int ExportateurID = GetCriteriaValue(ItemExportateur);
            int TypeID = GetCriteriaValue(ItemType);
             
            var mListe = (new Prefinancement()).fnSelectForInvoice("{all}",ExportateurID,TypeID,null,null,"AP");

            return this.Store(mListe);
        }


        public ActionResult SubmitOnSelectPrefinancing(string ItemSelected)
        {
            try
            {
                Prefinancement Item = JSON.Deserialize<Prefinancement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                if (Item != null)
                {
                    X.GetCmp<TextField>("txtPrefinancingNumber").Text = Item.Numero;
                    X.GetCmp<Hidden>("txtPrefiancementID").Value = Item.ID;
                    X.GetCmp<Hidden>("txtTypeID").Value = Item.PrefinancementType.ID;
                    X.GetCmp<Hidden>("txtTypeDesignation").Value = Item.PrefinancementType.Designation;
                    X.GetCmp<TextField>("txtBalance").Value = Item.SoldeAsString;
                    //X.GetCmp<TextField>("txtAmount").Value = Item.MontantAsString;

                    X.GetCmp<Window>("ListOfPrefinancings").Close();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Select Prefinancing : SubmitOnSelectPrefinancing",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult SubmitOnAddPrefinancing()
        {
            try
            {

                FactureCommercialePrefinancement factPre = new FactureCommercialePrefinancement();
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecModeSPD").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                  {
                    factPre.IsNew = true;
                    factPre.ID = Guid.Parse(X.GetCmp<TextField>("txtPrefiancementID").Text);
                    factPre.Prefinancement = new Prefinancement();
                    factPre.Prefinancement.ID = Guid.Parse(X.GetCmp<TextField>("txtPrefiancementID").Text);
                    factPre.Prefinancement.Numero = X.GetCmp<TextField>("txtPrefinancingNumber").Text;
                    if (X.GetCmp<TextField>("txtAmountPreleve").Text != string.Empty) factPre.MontantCFA = decimal.Parse(X.GetCmp<TextField>("txtAmountPreleve").Text);
                    if (X.GetCmp<TextField>("txtAmountEuroPreleve").Text != string.Empty) factPre.Montant = decimal.Parse(X.GetCmp<TextField>("txtAmountEuroPreleve").Text);
                    factPre.Prefinancement.PrefinancementType = new PrefinancementType();
                    factPre.Prefinancement.PrefinancementType.ID = int.Parse(X.GetCmp<TextField>("txtTypeID").Text);
                    factPre.Prefinancement.PrefinancementType.Designation = X.GetCmp<TextField>("txtTypeDesignation").Text;
                    factPre.TypeAsString = X.GetCmp<TextField>("txtTypeDesignation").Text;
                    factPre.Prefinancement.Montant = decimal.Parse(X.GetCmp<TextField>("txtBalance").Text);
                    factPre.Prefinancement.Solde = decimal.Parse(X.GetCmp<TextField>("txtNewBalance").Text);

                    Store mStore = X.GetCmp<Store>("storeListValorisation");
                    mStore.Insert(0, factPre);


                }
                else
                {
                    factPre.IsNew = true;
                    factPre.ID = Guid.Parse(X.GetCmp<Hidden>("hiddenFactureCommercialePrefinancementID").Text);
                    factPre.Prefinancement = new Prefinancement();
                    factPre.Prefinancement.ID = Guid.Parse(X.GetCmp<TextField>("txtPrefiancementID").Text);
                    factPre.Prefinancement.Numero = X.GetCmp<TextField>("txtPrefinancingNumber").Text;
                    if (X.GetCmp<TextField>("txtAmountPreleve").Text != string.Empty) factPre.MontantCFA = decimal.Parse(X.GetCmp<TextField>("txtAmountPreleve").Text);
                    if (X.GetCmp<TextField>("txtAmountEuroPreleve").Text != string.Empty) factPre.Montant = decimal.Parse(X.GetCmp<TextField>("txtAmountEuroPreleve").Text);
                    factPre.Prefinancement.PrefinancementType = new PrefinancementType();
                    factPre.Prefinancement.PrefinancementType.ID = int.Parse(X.GetCmp<TextField>("txtTypeID").Text);
                    factPre.Prefinancement.PrefinancementType.Designation = X.GetCmp<TextField>("txtTypeDesignation").Text;
                    factPre.TypeAsString = X.GetCmp<TextField>("txtTypeDesignation").Text;
                    factPre.Prefinancement.Montant = decimal.Parse(X.GetCmp<TextField>("txtBalance").Text);
                    factPre.Prefinancement.Solde = decimal.Parse(X.GetCmp<TextField>("txtNewBalance").Text);

                    Store mStore = X.GetCmp<Store>("storeListValorisation");
                    ModelProxy mProxy = mStore.GetById(factPre.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(factPre);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

               
                X.GetCmp<Window>("Prefinancement_Detail").Close();

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Prefinancing : SubmitOnAddPrefinancing",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }
        public ActionResult OnAddOtherDeduction()
        {
            FactureCommercialeDeductionViewModel viewModel = new FactureCommercialeDeductionViewModel();

            viewModel._FactureCommercialeDeduction = new FactureCommercialeDeduction();
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
           
            return new Ext.Net.MVC.PartialViewResult { ViewName = "OtherDeduction_Detail", Model = viewModel };

        }

        public ActionResult OnEditOtherDeduction(string ItemSelected, string ItemExecMode)
        {
            FactureCommercialeDeduction mclass = JSON.Deserialize<FactureCommercialeDeduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            FactureCommercialeDeductionViewModel viewModel = new FactureCommercialeDeductionViewModel();
            viewModel._FactureCommercialeDeduction = mclass;
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "OtherDeduction_Detail", Model = viewModel };

        }

        public ActionResult OnRemoveOtherDeduction(string ItemSelected)
        {

            try
            {

                FactureCommercialeDeduction mClass = JSON.Deserialize<FactureCommercialeDeduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                Store mstore = X.GetCmp<Store>("storeListDeduction");

                ModelProxy mProxy = mstore.GetById(mClass.ID);

                mProxy.Drop();

                X.GetCmp<RowSelectionModel>("rowSelectionDeduction").DeselectAll();



            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Other Deduction : Retirer",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitOnAddOtherDeduction()
        {
            try
            {

                FactureCommercialeDeduction factPre = new FactureCommercialeDeduction();
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecModeOD").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                {
                    factPre.IsNew = true;
                    factPre.ID = Guid.NewGuid();
                    factPre.FactureCommercialeDeductionType = new FactureCommercialeDeductionType();
                    factPre.FactureCommercialeDeductionType.ID = int.Parse(X.GetCmp<ComboBox>("_cmbDeductionType").Text);
                    factPre.FactureCommercialeDeductionType.Designation = X.GetCmp<ComboBox>("_cmbDeductionType").SelectedItem.Text.ToString();
                    if (X.GetCmp<TextField>("txtRate").Text != string.Empty) factPre.Taux = decimal.Parse(X.GetCmp<TextField>("txtRate").Text);
                    if (X.GetCmp<TextField>("txtAmountOD").Text != string.Empty) factPre.MontantCFA = decimal.Parse(X.GetCmp<TextField>("txtAmountOD").Text);
                    if (X.GetCmp<TextField>("txtAmountEuroOD").Text != string.Empty) factPre.Montant = decimal.Parse(X.GetCmp<TextField>("txtAmountEuroOD").Text);

                    Store mStore = X.GetCmp<Store>("storeListDeduction");
                    mStore.Insert(0, factPre);


                }
                else
                {
                    factPre.IsNew = true;
                    factPre.ID = Guid.Parse(X.GetCmp<Hidden>("txtFactureCommercialeDeductionID").Text);
                    factPre.FactureCommercialeDeductionType = new FactureCommercialeDeductionType();
                    factPre.FactureCommercialeDeductionType.ID = int.Parse(X.GetCmp<ComboBox>("_cmbDeductionType").Text);
                    factPre.FactureCommercialeDeductionType.Designation = X.GetCmp<ComboBox>("_cmbDeductionType").SelectedItem.Text.ToString();
                    if (X.GetCmp<TextField>("txtRate").Text != string.Empty) factPre.Taux = decimal.Parse(X.GetCmp<TextField>("txtRate").Text);
                    if (X.GetCmp<TextField>("txtAmountOD").Text != string.Empty) factPre.MontantCFA = decimal.Parse(X.GetCmp<TextField>("txtAmountOD").Text);
                    if (X.GetCmp<TextField>("txtAmountEuroOD").Text != string.Empty) factPre.Montant = decimal.Parse(X.GetCmp<TextField>("txtAmountEuroOD").Text);

                    Store mStore = X.GetCmp<Store>("storeListDeduction");
                    ModelProxy mProxy = mStore.GetById(factPre.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(factPre);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }


                X.GetCmp<Window>("OtherDeduction_Detail").Close();

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Select Prefinancing : SubmitOnAddPrefinancing",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult UpdateFormMethod(string ListeOfValorization, string ListeOfDeduction)
        {
            DataSource _db = new DataSource();
            DataTransaction mTran = new DataTransaction();
            try
            {
                FactureCommerciale mClass = new FactureCommerciale();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("hiddenID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("UpdateFormMethod : Commercial invoice load failed.");

                    if (mClass.Statut == "CA")
                        throw new Exception("UpdateFormMethod : Commercial invoice is canceled ! Please Refresh Overview");
                }

                bool Result = true;

                _db = mClass.db();
                mTran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                mClass = MapFormToObject(mClass);
                Result = mClass.fnUpdate(mTran);
                if (!Result)
                {
                    _db.RollBackTransaction(mTran);
                }
                else
                {
                    List<FactureCommercialePrefinancement> mFPre = JSON.Deserialize<List<FactureCommercialePrefinancement>>(ListeOfValorization, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (mFPre.Count > 0)
                    {
                        if (formExecMode != Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                        {
                            mClass.fnRemovePrefinancingAll();
                        }

                        for (int i = 0; i < mFPre.Count; i++)
                        {
                            if (mFPre.ElementAt(i).Desactive == false)
                            {
                                FactureCommercialePrefinancement fpre = new FactureCommercialePrefinancement();

                                fpre = mFPre.ElementAt(i);
                                fpre.FactureCommerciale = new FactureCommerciale();
                                fpre.SetDataSource(_db);
                                fpre.FactureCommerciale.ID = mClass.ID;
                                fpre.IsNew = true;
                                fpre.UtilisateurCreation = (string)Session["userName"];
                                fpre.UtilisateurModification = (string)Session["userName"];
                                Result = fpre.fnUpdate(mTran);
                            }
                            if (!Result)
                            {
                                break;
                            }
                        }

                        if (!Result)
                        {
                            _db.RollBackTransaction(mTran);
                        }

                    }

                    List<FactureCommercialeDeduction> mFDe = JSON.Deserialize<List<FactureCommercialeDeduction>>(ListeOfDeduction, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (mFDe.Count > 0)
                    {
                        if (formExecMode != Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                        {
                            mClass.fnRemoveDeductionAll();
                        }

                        for (int i = 0; i < mFDe.Count; i++)
                        {
                            if (mFDe.ElementAt(i).Desactive == false)
                            {
                                FactureCommercialeDeduction fde = new FactureCommercialeDeduction();

                                fde = mFDe.ElementAt(i);
                                fde.FactureCommerciale = new FactureCommerciale();
                                fde.SetDataSource(_db);
                                fde.FactureCommerciale.ID = mClass.ID;
                                fde.IsNew = true;
                                fde.UtilisateurCreation = (string)Session["userName"];
                                fde.UtilisateurModification = (string)Session["userName"];
                                Result = fde.fnUpdate(mTran);
                            }
                            if (!Result)
                            {
                                break;
                            }
                        }
                    }

                    if (!Result)
                    {
                        _db.RollBackTransaction(mTran);
                    }
                    else
                    {
                        _db.CommitTransaction(mTran);
                    }


                }

                Store mStore = X.GetCmp<Store>("storeListeCommercialInvoice");
                FactureCommerciale mFac = mClass;
                //Embarquement mEmb = new Embarquement();
                //mEmb.fnGet(mClass.ID);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                {
                    mStore.Insert(0, mFac);
                    X.GetCmp<RowSelectionModel>("rowSelectionCommercialInvoice").Select(0);
                }
                else
                {
                    ModelProxy mProxy = mStore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mFac);

                    mProxy.Commit();

                    mProxy.EndEdit();


                }

                X.GetCmp<Window>("FactureCommerciale_Detail").Close();
            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mTran);

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture Commerciale : UpdateFormMethod",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnPrintInvoice(string ItemID)
        {

            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
           
            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'FactureCommerciale{0}', '{1}/FactureCommerciale/ViewReportInvoice?id={0}', this,'Facture Commerciale ', '')", ItemID, BaseUrl));
        }

        public ActionResult ViewReportInvoice(string id)
        {
            try
            {
                rptCommercialInvoice report = new rptCommercialInvoice();

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["paramID"].Value = id;

                report.Parameters["paramID"].Visible = false;
                ViewData["Report"] = report;

                return View("ViewReportResult");
            }
            catch (Exception ex)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture Commerciale : Facture",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
        }

        public ActionResult OnPrintCommercialInvoice(string typeReport)
        {
            ViewData["typeReport"] = typeReport;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintFactureCommercialeReport", ViewData = ViewData };

        }

        public ActionResult PrintCommercialInvoice(string TypeReport)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                XtraReport report = null;
                string type = "";
                
                if (TypeReport == "Hi")
                {
                    report = new rptFactureCommercialeHistory() as XtraReport;
                    type = "Facture Commerciale - History";

                }

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["paramCampagne"].Value = X.GetCmp<ComboBox>("SLcropYearForReport").SelectedItem.Text;

                report.Parameters["paramExportateurID"].Value = X.GetCmp<ComboBox>("SLexportateurForReport").SelectedItem.Value.ToString();
                report.Parameters["paramExportateur"].Value = X.GetCmp<ComboBox>("SLexportateurForReport").SelectedItem.Text;

                report.Parameters["paramCertificationID"].Value = X.GetCmp<ComboBox>("SLCertificationForReport").SelectedItem.Value.ToString();
                report.Parameters["paramCertification"].Value = X.GetCmp<ComboBox>("SLCertificationForReport").SelectedItem.Text;


                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("SLstartDateForReport").RawText.ToString());
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("SLdueDateForReport").RawText.ToString());

                if (TypeReport == "Hi")
                {

                    report.Parameters["paramStatutID"].Value = X.GetCmp<ComboBox>("SLStatus").SelectedItem.Value.ToString();
                    report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("SLStatus").SelectedItem.Text;

                }
                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/FactureCommerciale/ViewList', this, '{2}',''),App.FormPrintFactureCommercialeReport.doClose()", Guid.NewGuid(), BaseUrl, type));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture Commerciale : Data Validation",
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
        private FactureCommerciale MapFormToObject(FactureCommerciale mClass)
        {
            try
            {
                mClass.ID = Guid.Parse(X.GetCmp<Hidden>("hiddenID").Text);

                mClass.Numero = X.GetCmp<TextField>("txtNumber").Text;

                Embarquement mEmb = new Embarquement();
                mEmb.ID = Guid.Parse(X.GetCmp<Hidden>("txtEmbarquementID").Text);
                mEmb.Numero = X.GetCmp<TextField>("txtShipmentNumber").Text;

                mEmb.Domiciliation = new Banque();
                mEmb.Domiciliation.ID = int.Parse(X.GetCmp<Hidden>("txtDomiciliationID").Text);
                mEmb.Domiciliation.Nom = X.GetCmp<TextField>("txtDomiciliation").Text;

                mEmb.Exportateur = new Exportateur();
                mEmb.Exportateur.ID = int.Parse(X.GetCmp<Hidden>("txtExportateurID").Text);
                mEmb.Exportateur.Nom = " ";
                mEmb.Quantite = decimal.Parse(X.GetCmp<TextField>("txtQuantity").Text);
                mClass.Embarquement = mEmb;

                CompteBancaire mComp = new CompteBancaire();
                mComp.ID = int.Parse(X.GetCmp<ComboBox>("_cmbAccountNumber").Text);
                mComp.Numero = X.GetCmp<ComboBox>("_cmbAccountNumber").SelectedItem.Text.ToString();
                mClass.CompteBancaire = mComp;

                mClass.NbreSacs = int.Parse(X.GetCmp<TextField>("txtNumberOfBags").Text.ToString());
                mClass.PoidsBrutTheorique = decimal.Parse(X.GetCmp<TextField>("txtGrossWeight").Text);
                mClass.PoidsNetTheorique = decimal.Parse(X.GetCmp<TextField>("txtNetWeight").Text);

                mClass.PrixCAF = decimal.Parse(X.GetCmp<TextField>("txtPriceCAF").Text);
                mClass.PrixFob = decimal.Parse(X.GetCmp<TextField>("txtPriceFob").Text);

                mClass.MontantBrut = decimal.Parse(X.GetCmp<TextField>("txtGrossAmount").Text);

                mClass.DeductionFinancementAPC = decimal.Parse(!string.IsNullOrEmpty(X.GetCmp<Hidden>("hiddenMontantPrefinancmentAPC").Text) ? X.GetCmp<Hidden>("hiddenMontantPrefinancmentAPC").Text : "0" );
                mClass.DeductionFinancementBlank = decimal.Parse(!string.IsNullOrEmpty(X.GetCmp<Hidden>("hiddenMontantPrefinancmentBlank").Text) ? X.GetCmp<Hidden>("hiddenMontantPrefinancmentBlank").Text : "0");
                mClass.AutresDeduction = decimal.Parse(X.GetCmp<TextField>("txtTotalDeduction").Text);
                mClass.MontantNet = decimal.Parse(X.GetCmp<TextField>("txtAmount").Text);
                mClass.Date = DateTime.Parse(X.GetCmp<DateField>("txtDateFactureCommerciale").RawText.ToString()).Add(DateTime.Now.TimeOfDay);
               
                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];


            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture Commerciale : MapFormToObject",
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
            X.GetCmp<RowSelectionModel>("rowSelectionCommercialInvoice").DeselectAll();
        }

        #endregion
    }
}