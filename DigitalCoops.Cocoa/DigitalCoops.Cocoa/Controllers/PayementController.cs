using DevExpress.XtraReports.UI;
using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Business;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Components.Data;
using Tms.Components.Settings;

namespace Tms2017.MVC.Controllers
{
    public class PayementController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        public PayementController()
        {
            //string cultureName = "en";
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
        }
        // GET: Payement
        public ActionResult Index()
        {
            Parametres mParam = new Parametres(0);

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            //bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            bool result = mSiteParDefaut.fnGetDefaultSite();
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;            

            //mCampagne.fnGet(mclass.Campagne);
            X.GetCmp<ComboBox>("PayementCampagneID").SetValue(mParam.Campagne);            
            
            X.GetCmp<DateField>("TxtPayementPeriodStart").RawText = DateTime.Now.ToShortDateString();
            X.GetCmp<DateField>("TxtPayementPeriodEnd").RawText = DateTime.Now.ToShortDateString();

            X.GetCmp<FormPanel>("CriteriaPanelPayement").SetTitle("Site : " + mSiteParDefaut.Nom + ", Today : " + DateTime.Now.ToShortDateString());

            //MenuAccess menuAcces = new MenuAccess();
            //menuAcces = InitFunctionAccess();

            #region Set Function's Access

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{60355486-8660-4072-8ce7-f737be36b176}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();

            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{A52FA7FC-33C5-42D8-9A9E-28227355DF8D}", UserName);
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{6E73E5B5-F004-40B0-9829-B18C10E5AD99}")))
                X.GetCmp<Button>("btnNewPayement").Enable();
            else
                X.GetCmp<Button>("btnNewPayement").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{B9010BA5-0A5E-4CC1-BEF7-52A3CA6F045D}")))
                X.GetCmp<MenuItem>("mnuExportPayments").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportPayments").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{276027BC-DB7E-4851-AD01-EF468D994E7F}")))
                X.GetCmp<MenuItem>("btnViewPendingPayement").Enable();
            else
                X.GetCmp<MenuItem>("btnViewPendingPayement").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{3E57CD64-2D42-4F12-8485-249510BFD973}")))
                X.GetCmp<MenuItem>("mnuPrintPayementList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintPayementList").Disable();

            X.GetCmp<Hidden>("PahiddenPermCreer").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{6E73E5B5-F004-40B0-9829-B18C10E5AD99}")));
            X.GetCmp<Hidden>("PahiddenPermConsultPendingPayments").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{276027BC-DB7E-4851-AD01-EF468D994E7F}")));
            X.GetCmp<Hidden>("PahiddenPermDesactiver").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{F472ADA0-C3D8-4205-A316-5D1998DA36E0}")));
            X.GetCmp<Hidden>("PahiddenPermPrintCopyOfPayment").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{A25209D8-9396-453B-841D-A0E9827E5E3E}")));
            X.GetCmp<Hidden>("PahiddenPermExporterExcel").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{B9010BA5-0A5E-4CC1-BEF7-52A3CA6F045D}")));
            X.GetCmp<Hidden>("PahiddenPermPrintListOfPayment").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{3E57CD64-2D42-4F12-8485-249510BFD973}")));
            X.GetCmp<Hidden>("PahiddenPermOverview").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{A52FA7FC-33C5-42D8-9A9E-28227355DF8D}")));
            X.GetCmp<Hidden>("PahiddenPermPrintCopyOfPaymentDet").SetValue(mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{2871bf2a-dfff-4155-88e5-b3415e205006}")));

            #endregion


            return View();
        }

        public ActionResult onAdd()
        {            
            PayementViewModel mclass = new PayementViewModel();

            mclass._Payement = new Payement();
            mclass._Payement.PayementOption = new PayementOption();
            mclass._Parametres = new Parametres(0);
            //mclass._Payement.PayementOption.ID = 
            //mclass._Parametres.fnGet();
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            if (result)
            {
                mclass._Payement.Sites = new Site();
                mclass._Payement.Sites.ID = mSiteParDefaut.ID;
                mclass._Payement.Sites.Nom = mSiteParDefaut.Nom;
            }

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPayement", Model = mclass, };
        }

        public ActionResult onEdit(string ItemSelected)
        {            
            Payement mclass = JSON.Deserialize<Payement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            PayementViewModel viewmodel = new PayementViewModel();
            viewmodel._Payement = mclass;
            viewmodel._Parametres = new Parametres(0);
            //viewmodel._Parametres.fnGet();
            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPayement", Model = mclass, };
        }

        public ActionResult onConsult(string ItemSelected)
        {            
            Payement mclass = JSON.Deserialize<Payement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            PayementViewModel viewmodel = new PayementViewModel();
            viewmodel._Payement = mclass;
            viewmodel._Parametres = new Parametres(0);
            //viewmodel._Parametres.fnGet();
            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPayementDetail", Model = viewmodel, };
        }

        public ActionResult onViewPendindPayement()
        {            
            PayementViewModel viewmodel = new PayementViewModel();
            viewmodel._Payement = new Payement();
            viewmodel._Parametres = new Parametres(0);
            //viewmodel._Parametres.fnGet();

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);

            if (result)
            {
                viewmodel._Payement.Sites = new Site();
                viewmodel._Payement.Sites.ID = mSiteParDefaut.ID;
                viewmodel._Payement.Sites.Nom = mSiteParDefaut.Nom;
            }

            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPendingPayement", Model = viewmodel, };
        }

        public ActionResult SubmitFormMethod(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                Payement payement = new Payement();
                PayementTransaction payementtransaction;

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("PayementHiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    payement.IsNew = true;
                else
                {
                    payement.IsNew = false;

                    payement.fnGet(Guid.Parse(GetFormValue("TxtPayementID")));

                    if (payement == null || payement.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Payement load failed.");
                }

                bool result = false;
                bool resulttransact = true;
                payement = MapFormToObject(payement);

                _db = payement.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                                
                List<PayementItem> ItemPayement = JSON.Deserialize<List<PayementItem>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                if (ItemPayement.Count > 0)
                {
                    decimal totalamount = 0;

                    foreach (var item in ItemPayement)
                    {
                        totalamount += item.NewMontant;
                        //if(payement.PayementOption.ID == 1) totalamount += item.Montant;
                        //else totalamount += item.NewMontant;
                    }

                    payement.Montant = totalamount;
                    result = payement.fnUpdate(mtran);

                    if (result)
                    {
                        for (int i = 0; i < ItemPayement.Count; i++)
                        {
                            payementtransaction = new PayementTransaction();
                            payementtransaction.Payement = new Payement();
                            payementtransaction.Payement.PayementType = new PayementType();
                            payementtransaction.PayementTransactionType = new PayementTransactionType();

                            payementtransaction.SetDataSource(_db);

                            payementtransaction.Payement.ID = payement.ID;
                            payementtransaction.PayementTransactionType.ID = payement.PayementType.ID;
                            payementtransaction.ElementID = ItemPayement[i].ID;
                            payementtransaction.ElementRef = ItemPayement[i].Reference;
                            //payementtransaction.ElementSolde = ItemPayement[i].Montant;
                            payementtransaction.ElementSolde = ItemPayement[i].NewMontant;
                            payementtransaction.Solde = ItemPayement[i].Solde - ItemPayement[i].NewMontant;
                            payementtransaction.MontantTotal = ItemPayement[i].Montant;
                            //if (payement.PayementOption.ID == 1)
                            //    payementtransaction.ElementSolde = ItemPayement[i].OldMontant;
                            //else payementtransaction.ElementSolde = ItemPayement[i].NewMontant;

                            payementtransaction.UtilisateurCreation = (string)Session["userName"];
                            payementtransaction.UtilisateurModification = (string)Session["userName"];

                            if (payementtransaction.IsNew)
                            {
                                resulttransact = payementtransaction.fnUpdate(mtran);
                            }

                            if (!resulttransact)
                            {
                                break;
                            }
                        }
                        if (!resulttransact)
                        {
                            _db.RollBackTransaction(mtran);
                        }
                        _db.CommitTransaction(mtran);
                    }
                }
                    
                if (result && resulttransact)
                {

                    Store mstore = X.GetCmp<Store>("storeListePayement");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, payement);
                        X.GetCmp<RowSelectionModel>("rowSelectionListePayement").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(payement.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(payement);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormPayement").Close();
                }                
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Payement : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelPayement");            
            mform.ToggleCollapse();            
            return this.Direct();
        }

        public ActionResult onRefresh(string ItemCampagne, string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd, string ItemType, string ItemStatut, string ItemSite)
        {
            try
            {                

                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);              

                Store mstore = X.GetCmp<Store>("storeListePayement");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemCampagne"   ,ItemCampagne),
                                new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                new Ext.Net.Parameter("ItemSite"   ,ItemSite),
                                new Ext.Net.Parameter("ItemPeriodStart"     ,ItemPeriodStart),
                                new Ext.Net.Parameter("ItemPeriodEnd"           ,ItemPeriodEnd),
                                new Ext.Net.Parameter("ItemType"           ,ItemType),
                                new Ext.Net.Parameter("ItemStatut"           ,ItemStatut)
                            });

                FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelPayement");                
                mform.Collapsed = true;
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Payement : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });                
            }
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne, string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd, string ItemType, string ItemStatut, string ItemSite)
        {
            string CropYearID = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                CropYearID = "{Tous}";
            }

            int fournisseurID = string.IsNullOrEmpty(ItemFournisseur) ? -1 : GetCritriaValue(ItemFournisseur);
            int PayementTypeID = string.IsNullOrEmpty(ItemType) ? -1 : GetCritriaValue(ItemType);
            int SiteID = string.IsNullOrEmpty(ItemSite) ? 1 : GetCritriaValue(ItemSite);

            DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? DateTime.Now : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? DateTime.Now : DateTime.Parse(ItemPeriodEnd.ToString());

            string status = string.IsNullOrEmpty(ItemStatut) ? "-1" : ItemStatut;
            if (!string.IsNullOrEmpty(ItemStatut) && ItemStatut.Contains("null"))
            {
                status = "-1";
            }
            var mListe = (new Payement()).fnSelect(CropYearID, fournisseurID, startdate, enddate, PayementTypeID, status, SiteID);
              
            return this.Store(mListe);
        }

        public ActionResult SelectDetailsPayementItems(string payementID)
        {
            Guid ID = Guid.Empty;
            if (!string.IsNullOrEmpty(payementID))
                ID = Guid.Parse(payementID);
            else return this.Direct();

            var mListe = (new PayementTransaction()).fnSelectByPayement(ID);
            return this.Store(mListe);
        }

        public ActionResult SearchPayementByNumber(string ItemNumber)
        {
            if (!string.IsNullOrEmpty(ItemNumber))
            {
                Payement mPayement = new Payement();

                mPayement.fnSelectByNumber(ItemNumber);
                if (mPayement.ID != Guid.Empty)
                {
                    Store mstore = X.GetCmp<Store>("storeListePayement");
                    mstore.RemoveAll();
                    mstore.Insert(0, mPayement);
                }
                else
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Payment",
                        Message = "Payment Item not found, Please Verify Number and Retry !",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                }                                
            }
            
            return this.Direct();

        }

        public ActionResult SelectPayementItems(string ItemFournisseur, string ItemType, string ItemSite)
        {
            List<DataPersist> mList = new List<DataPersist>();

            int TypePayement = 0;
            int fournisseurID = -1;

            if (!string.IsNullOrEmpty(ItemType) && !string.IsNullOrEmpty(ItemSite))
            {
                TypePayement = int.Parse(ItemType);
                fournisseurID = string.IsNullOrEmpty(ItemFournisseur) ? fournisseurID : int.Parse(ItemFournisseur);
                int siteID = string.IsNullOrEmpty(ItemSite) ? 1 : int.Parse(ItemSite);

                switch (TypePayement)
                {
                    case 1:
                        mList = new PayementItem().fnSelectDeliveryToPay(fournisseurID, siteID);
                        break;
                    case 2:
                        mList = new PayementItem().fnSelectFinancingToPay(fournisseurID, siteID);
                        break;
                    case 3:
                        mList = new PayementItem().fnSelectSavingToPay(fournisseurID, siteID);
                        break;
                    case 4:
                        mList = new PayementItem().fnSelectBonusToPay(fournisseurID, siteID);
                        break;
                    case 5:
                        mList = new PayementItem().fnSelectCommissionToPay(fournisseurID, siteID);
                        break;
                    case -1:
                        mList = new PayementItem().fnSelectDeliveryToPay(fournisseurID, siteID);
                        foreach (DataPersist mv in new PayementItem().fnSelectFinancingToPay((int)fournisseurID, siteID))
                        {
                            mList.Add(mv);
                        }

                        foreach (DataPersist mv in new PayementItem().fnSelectSavingToPay((int)fournisseurID, siteID))
                        {
                            mList.Add(mv);
                        }
                        break;
                }

            }

            return this.Store(mList);
        }
        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Payement mClass = JSON.Deserialize<Payement>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = mClass.fnGet(mClass.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Payement loading failed.");

                if (mClass.Desactive)
                    result = mClass.fnActivate();
                else
                    result = mClass.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Payement Approval failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePayement");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Payement : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        private Payement MapFormToObject(Payement mClass)
        {
            mClass.Campagne = new Campagne();
            Parametres param = new Parametres();
            param.fnGet();
            //mClass.Campagne.Designation = X.GetCmp<ComboBox>("PayementCampagneID").SelectedItem.Value;
            mClass.Campagne.Designation = param.Campagne;

            mClass.Fournisseur = new Fournisseur();
            mClass.Fournisseur.ID = int.Parse(X.GetCmp<ComboBox>("CmbPayementFounisseurID").SelectedItem.Value);
            mClass.Fournisseur.Nom = X.GetCmp<ComboBox>("CmbPayementFounisseurID").SelectedItem.Text;

            mClass.PayementMode = new PayementMode();
            mClass.PayementMode.ID = int.Parse(X.GetCmp<ComboBox>("CmbPayementModeID").SelectedItem.Value);
            mClass.PayementMode.Designation = X.GetCmp<ComboBox>("CmbPayementModeID").SelectedItem.Text;

            mClass.PayementType = new PayementType();
            mClass.PayementType.ID = int.Parse(X.GetCmp<ComboBox>("CmbPayementTypeID").SelectedItem.Value);
            mClass.PayementType.Designation = X.GetCmp<ComboBox>("CmbPayementTypeID").SelectedItem.Text;

            mClass.Numero = X.GetCmp<Hidden>("hiddenNumeroPayement").Text;
            mClass.DatePayement = DateTime.Parse(X.GetCmp<DateField>("TxtPayementDate").RawText.ToString() + " " + GetFormValue("TxtPayementDateTime")) ;
            //mClass.Montant = decimal.Parse(X.GetCmp<TextField>("hiddenTotalPayement").Text);
            //mClass.Commentaire = X.GetCmp<TextField>("TxtCommentairePayement").Text;
            mClass.Statut = "AP";

            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            mClass.Sites = new Site();
            mClass.Sites.ID = int.Parse(X.GetCmp<ComboBox>("cmbPayementSite").SelectedItem.Value);
            mClass.Sites.Nom = X.GetCmp<ComboBox>("cmbPayementSite").SelectedItem.Text;

            //mClass.PayementOption = new PayementOption();
            //mClass.PayementOption.ID = int.Parse(X.GetCmp<ComboBox>("CmbPayementOptionID").SelectedItem.Value);
            //mClass.PayementOption.Designation = X.GetCmp<ComboBox>("CmbPayementOptionID").SelectedItem.Text;

            return mClass;
        }

        public ActionResult SetAmountToLetter(string Amount)
        {
            X.GetCmp<DisplayField>("TxtTotalPayementInLetter").SetValue("");
            if (Amount != "0")
            {
                //string val = MoneySpellerEn.Spell(Amount.Replace(" ", ""));
                string val = MoneySpeller.ToLettres(Int32.Parse(Amount.Replace(" ", "")));
                X.GetCmp<DisplayField>("TxtTotalPayementInLetter").SetValue(val);
                //string val = MoneySpeller.NumberToWords(Int32.Parse(Amount.Replace(" ", "")));

                //X.GetCmp<TextField>("TxtTotalPayementInLetter").SetValue(val + " F CFA");
            }            

            return this.Direct();
        }

        public ActionResult OnPrintPayement()
        {                      
            PayementViewModel viewmodel = new PayementViewModel();           
            viewmodel._Parametres = new Parametres();
            viewmodel._Parametres.fnGet();
            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPrintPayment", Model = viewmodel, };
        }
        
        public ActionResult OnPrintVoucher(string ItemSelected, int payementType, string IsCopy)
        {
            bool ReportIscopy = false;
            if (string.IsNullOrEmpty(IsCopy))
                ReportIscopy = true;

            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            
            //string param = JSON.Serialize(contratperiode);
            Session["ParamReport"] = ItemSelected;

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Voucher{0}', '{1}/Payement/ViewVoucher?payementtype={2}&IsCopy={3}', this, 'Voucher','')", Guid.NewGuid(), BaseUrl, payementType,ReportIscopy));
        }

        public ActionResult OnPrintVoucherDet(string ItemSelected, int payementType, string IsCopy)
        {
            if (payementType != 1)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Payement : Data Validation",
                    Message = "Can't show Detail of this type of Payment",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }

            bool ReportIscopy = false;
            if (string.IsNullOrEmpty(IsCopy))
                ReportIscopy = true;

            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            //string param = JSON.Serialize(contratperiode);
            Session["ParamReport"] = ItemSelected;

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Voucher{0}', '{1}/Payement/ViewVoucherDet?payementtype={2}&IsCopy={3}', this, 'Voucher','')", Guid.NewGuid(), BaseUrl, payementType, ReportIscopy));
        }

        public ActionResult ViewVoucher(int payementtype, bool IsCopy)
        {
            XtraReport report = null;
            //Payement payement = new Payement();
            //payement.fnGet(id);
            Payement payement = JSON.Deserialize<Payement>((string)Session["ParamReport"], new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            if (payementtype == 1)
            {
                report = new rptVoucherDelivery() as XtraReport;
                report.DataSource = DevExpressReportDs.SetDataSource(report);
            }

            if (payementtype == 2)
            {
                report = new RptVoucherFinancing() as XtraReport;
                report.DataSource = DevExpressReportDs.SetDataSource(report);
            }

            if (payementtype == 3)
            {
                report = new rptVoucherSaving() as XtraReport;
                report.DataSource = DevExpressReportDs.SetDataSource(report);
            }
            if (payementtype == 4)
            {
                report = new RptVoucherBonus() as XtraReport;
                report.DataSource = DevExpressReportDs.SetDataSource(report);
            }

            report.Parameters["Number"].Value = payement.ID;
            report.Parameters["AmountInLetter"].Value = MoneySpeller.ToLettres(Int32.Parse(payement.Montant.ToString()));
            report.Parameters["IsCopy"].Value = IsCopy;

            ViewData["Report"] = report;

            return View();
        }

        public ActionResult ViewVoucherDet(int payementtype, bool IsCopy)
        {
            XtraReport report = null;
            //Payement payement = new Payement();
            //payement.fnGet(id);
            Payement payement = JSON.Deserialize<Payement>((string)Session["ParamReport"], new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            if (payementtype == 1)
            {
                report = new rptVoucherDeliveryDetail() as XtraReport;
                report.DataSource = DevExpressReportDs.SetDataSource(report);
            }

            //if (payementtype == 2)
            //{
            //    report = new RptVoucherFinancing() as XtraReport;
            //    report.DataSource = DevExpressReportDs.SetDataSource(report);
            //}

            //if (payementtype == 3)
            //{
            //    report = new rptVoucherSaving() as XtraReport;
            //    report.DataSource = DevExpressReportDs.SetDataSource(report);
            //}

            report.Parameters["Number"].Value = payement.ID;
            report.Parameters["ID"].Value = payement.ID;
            report.Parameters["AmountInLetter"].Value = MoneySpeller.ToLettres(Int32.Parse(payement.Montant.ToString()));
            report.Parameters["IsCopy"].Value = IsCopy;

            ViewData["Report"] = report;

            return View("ViewVoucher");
        }

        #region Method
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
                    else if (hidAction.ToString().Equals(EXTEND))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Extend;
                }
                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
            }
            catch (Exception ex)
            {
                Ext.Net.X.Msg.Alert("frm_Detail : GetFormExecMode", ex.Message).Show();
                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
            }
        }

        private int GetCritriaValue(string strComponent)
        {
            int value;

            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
                return value;
            else
                return -1;
        }

        private int GetCritriasValue(string strComponent)
        {
            if (X.GetCmp<ComboBox>(strComponent) != null && X.GetCmp<ComboBox>(strComponent).SelectedItem != null && X.GetCmp<ComboBox>(strComponent).SelectedItem.Value != null)
                return int.Parse(X.GetCmp<ComboBox>(strComponent).SelectedItem.Value.ToString());
            else
                return -1;
        }

        private void DeselectGridRows()
        {
            //X.Js.Call("App.rowSelectionListe.deselectAll();");
            X.GetCmp<RowSelectionModel>("rowSelectionPayement").DeselectAll();
        }

        public void CreateIconsList()
        {
            try
            {
                //Ext.Net.ResourceManager.RegisterGlobalIcon(Icon.PageCancel);

                //string url =  Ext.Net.ResourceManager.GetInstance().GetIconUrl(Icon.Tick);
            }
            catch (Exception ex)
            {

                X.Msg.Alert("frmLBC_ReceptionInDistrict_Overview : CreateIconList", ex.Message).Show();
            }
        }
        #endregion

        private MenuAccess InitFunctionAccess()
        {
            MenuAccess menuAccess = new MenuAccess();
            menuAccess.CanAdd = (new Fonction()).fnGetUserAccessStatus("80b6196c-f8b1-4c47-9974-c63cf0b6542a", (string)Session["username"]);
            return menuAccess;
        }

        public ActionResult OnPrintHystoryOfPayment(string cropYear, string payementType, string supplier, string startDate, string endDate, string supplierName, string payementTypeDesignation)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                Session["paramCampagne"] = cropYear;
                if (cropYear.Contains("{Tous}"))
                    Session["campagneID"] = "";
                else
                    Session["campagneID"] = cropYear;

                Session["PayementTypeId"] = Int32.Parse(payementType);
                Session["PayementTypeNom"] = payementTypeDesignation;

                Session["fournisseurID"] = Int32.Parse(supplier);

                Session["DateDebut"] = dateDebut;
                Session["DateFin"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Payement/ViewReportResult', this, 'Hystory Of Payment',''),App.frmSupplierPrintHystoryOfPayment.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Facture : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }


        }


        public ActionResult ViewReportResult()
        {
            XtraReport report = null;

            // TODO :  sur le base du type de rapport : detaillé ou cumulé 
            // initialiser l'objet report avec l'object idoine

            report = new rptSupplierHystoryOfPayment() as XtraReport;

            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramCampagne"].Value = Session["paramCampagne"];

            report.Parameters["campagne"].Value = Session["campagneID"];

            report.Parameters["PayementTypeId"].Value = Session["PayementTypeId"];

            report.Parameters["PayementTypeNom"].Value = Session["PayementTypeNom"];

            report.Parameters["fournisseurID"].Value = Session["fournisseurID"];

            report.Parameters["DateDebut"].Value = Session["DateDebut"];

            report.Parameters["DateFin"].Value = Session["DateFin"];

            ViewData["Report"] = report;

            return View();
        }

        public ActionResult OnDisplayPayementList(string ItemPeriodStart = "", string ItemPeriodEnd = "")
        {
            string UserName = (string)Session["userName"];

            if (string.IsNullOrEmpty(ItemPeriodStart)) ItemPeriodStart = DateTime.Now.ToShortDateString();
            if (string.IsNullOrEmpty(ItemPeriodEnd)) ItemPeriodEnd = DateTime.Now.ToShortDateString();
            DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
            DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);
            ViewData["datedebut"] = datedebut;
            ViewData["datefin"] = datedfin;

            Site mSiteParDefaut = new Site();
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);            

            Parametres mParam = new Parametres(0);

            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;
            if (mParam.Site == mSiteParDefaut.ID) ViewData["UrlSite"] = "LoadSiteAll";
            else ViewData["UrlSite"] = "LoadSiteByAccess";

            ViewData["Titre"] = "Liste des Paiements";
            ViewData["actionToDo"] = "OnPrintPaymentList";
            ViewData["ControllerName"] = "Payement";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmCriteriaForPayment", ViewData = ViewData };
        }

        public ActionResult OnPrintPaymentList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetStartDate").RawText, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetEndDate").RawText, "d", CultureInfo.CurrentUICulture);

                //Session["paramCropYear"] = cropYear;       
                Session["paramSite"] = GetFormValue("cmbDetSite");
                Session["paramSiteText"] = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text;

                Session["paramCampagne"] = GetFormValue("cmbDetCropYear");
                Session["paramFournisseur"] = GetFormValue("cmbDetFournisseur");
                Session["paramFournisseurText"] = X.GetCmp<ComboBox>("cmbDetFournisseur").SelectedItem.Text;
                Session["paramTypeOfPayment"] = GetFormValue("cmbDetTypePayment");
                Session["paramTypeOfPaymentText"] = X.GetCmp<ComboBox>("cmbDetTypePayment").SelectedItem.Text;
                Session["paramStatut"] = GetFormValue("cmbDetStatus");
                Session["paramStatutText"] = X.GetCmp<ComboBox>("cmbDetStatus").SelectedItem.Text;

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;              
                
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Payement/ViewReportListResult', this, 'Liste Des Paiements',''),App.frmCriteriaForPayment.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Payment : Data Validation",
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

            rptPayementList report = new rptPayementList();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramCampagneText"].Value = Session["paramCampagne"];
            if (!string.IsNullOrEmpty(Session["paramCampagne"].ToString()) && Session["paramCampagne"].ToString() == "{Tous}")
                report.Parameters["paramCampagne"].Value = string.Empty;
            else
                report.Parameters["paramCampagne"].Value = Session["paramCampagne"];

            report.Parameters["paramSite"].Value = Session["paramSite"];
            report.Parameters["paramSiteText"].Value = Session["paramSiteText"];

            report.Parameters["paramFournisseur"].Value = Session["paramFournisseur"];
            report.Parameters["paramFournisseurText"].Value = Session["paramFournisseurText"];

            report.Parameters["paramTypeOfPayment"].Value = Session["paramTypeOfPayment"];
            report.Parameters["paramTypeOfPaymentText"].Value = Session["paramTypeOfPaymentText"];

            report.Parameters["paramStatut"].Value = Session["paramStatut"];
            report.Parameters["paramStatutText"].Value = Session["paramStatutText"];

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];
            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

    }
}