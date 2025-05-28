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
using Tms.Classes;
using Tms.Classes.Business;
using Tms.Classes.Business.stock;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class PeseeAvantUsinageController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";


        static int _CapturedTare;
        static int _CapturedWeight;
        //static bool _WeighingIsCreated;
        //static Guid _WeighingID;

        //public static Guid WeighingID
        //{
        //    get
        //    {
        //        return _WeighingID;
        //    }
        //    set
        //    {
        //        _WeighingID = value;
        //    }
        //}

        //public static bool WeighingIsCreated
        //{
        //    get
        //    {
        //        return _WeighingIsCreated;
        //    }
        //    set
        //    {
        //        _WeighingIsCreated = value;
        //    }
        //}

        public static int CapturedTare
        {
            get
            {
                return _CapturedTare;
            }
            set
            {
                _CapturedTare = value;
            }
        }

        public static int CapturedWeight
        {
            get
            {
                return _CapturedWeight;
            }
            set
            {
                _CapturedWeight = value;
            }
        }

        bool BtnReadClicked = false;
        // GET: PeseeAvantUsinage
        public ActionResult Index()
        {
            string campagne = new Parametres(0).Campagne;

            X.GetCmp<ComboBox>("cmbFiltreCampagne").SetValue(campagne);

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("txtFiltreDateDebut").RawText = StartDate;
            X.GetCmp<DateField>("txtFiltreDateFin").RawText = EndDate;

            X.GetCmp<FormPanel>("PeseeAvantUsinageCP").SetTitle("Campagne : " + campagne + ", Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{626A4B2D-A6B4-45CD-9EDD-B9F28C109649}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{849A8B0F-ADBB-4050-ADF0-802C3E801A04}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{3E864BD4-2EDA-427C-850F-24F3739E900E}")))
                X.GetCmp<MenuItem>("mnuPrintWeihingList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintWeihingList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{57DC6254-7D7D-4742-A7A5-7687179B8EA0}")))
                X.GetCmp<MenuItem>("mnuExportPAU").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportPAU").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{C9CFD28B-F42D-4FC8-998F-EC29F310C837}")))
                X.GetCmp<MenuItem>("bntAddManual").Enable();
            else
                X.GetCmp<MenuItem>("bntAddManual").Disable();            

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{EB574A09-E05A-484F-B700-374DDE6E1245}")))
                X.GetCmp<Hidden>("phiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{5E41C481-7348-4FE3-8448-C99CC865FBBA}")))
                X.GetCmp<Hidden>("phiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{fe8f9ed0-7f1f-45ab-915a-dddb39665d2f}")))
                X.GetCmp<Hidden>("phiddenPermDesactiverSup").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermDesactiverSup").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{4EE6C46C-07EF-4C38-8F49-04D9C6DC7A61}")))
                X.GetCmp<Hidden>("phiddenPermPrintSheet").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermPrintSheet").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{764DB266-8CAA-470D-BDBE-24BE37BD015E}")))
                X.GetCmp<Hidden>("phiddenPermFinalize").SetValue(true);
            else
                X.GetCmp<Hidden>("phiddenPermFinalize").SetValue(false);

            //X.GetCmp<Hidden>("mvhiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{94E1648B-BB69-4470-8B5D-FA8D19598161}", UserName));
            //X.GetCmp<Hidden>("mvhiddenPermGenerate").SetValue(HasAccess.fnGetUserAccessStatus("{C1785226-126D-4648-95C6-92894707BC1E}", UserName));
            //X.GetCmp<Hidden>("mvhiddenPermPrint").SetValue(HasAccess.fnGetUserAccessStatus("{DF2C998A-9232-48ED-B403-6DB46289BE0A}", UserName));
            //X.GetCmp<Hidden>("mvhiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{3CA6BD01-DFC1-4B9F-9E0E-134391DF8B4F}", UserName));
            #endregion

            return View();
        }

        public ActionResult OnCaptureTare(string ItemSelected)
        {
            try
            {
                List<PeseeProductionPalette> item = JSON.Deserialize<List<PeseeProductionPalette>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                if (item.Count > 0)
                {

                    Parametres mParam = new Parametres(0);
                    BtnReadClicked = true;
                    CapturedTare = 0;
                    int sWeight = 0;
                    string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                    if (mParam.IsInDevelopment == false)
                    {
                        //string ip = "198.168.8.101";
                        // initialize services                      
                        ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);
                        sWeight = proxy.GetWeight();
                        X.GetCmp<TextField>("txtTarePalettes").Text = sWeight.ToString("n0");
                    }
                    else
                    {
                        sWeight = 20;
                        X.GetCmp<TextField>("txtTarePalettes").Text = sWeight.ToString("n0");
                    }
                }
                else
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Weighing Before Cleaning : Capture Weight",
                        Message = "Full Pallets Weight Is Empty",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            return this.Direct();
        }

        public ActionResult OnCaptureWeight(string sacs = "")
        {
            try
            {
                Parametres mParam = new Parametres(0);
                BtnReadClicked = true;
                CapturedWeight = 0;
                int sWeight = 0;
                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                //string ip = "198.168.8.101";
                // initialize services     
                if (mParam.IsInDevelopment == false)
                {
                    ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);
                    sWeight = proxy.GetWeight();
                    //int nbrSac = !string.IsNullOrEmpty(sacs) ? int.Parse(sacs) : 0;
                    //int sWeight = nbrSac * 67;
                    X.GetCmp<NumberField>("txtPoidsBrutPAU").Text = sWeight.ToString();
                }
                else
                {
                    int nbrSac = !string.IsNullOrEmpty(sacs) ? int.Parse(sacs) : 0;
                    sWeight = nbrSac * 67;
                    X.GetCmp<NumberField>("txtPoidsBrutPAU").Text = sWeight.ToString();
                }
                CapturedWeight = sWeight;              

            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            return this.Direct();
        }

        public ActionResult onAdd()
        {
            //WeighingIsCreated = false;
            PeseeAvantUsinageViewModel mclass = new PeseeAvantUsinageViewModel();
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{764DB266-8CAA-470D-BDBE-24BE37BD015E}"), UserName);

            mclass._PeseeAvantUsinage = new PeseeAvantUsinage();
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._PeseeAvantUsinage.IsManual = false;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeAvantUsinage", Model = mclass, ViewData = ViewData };

        }

        public ActionResult onEdit(string ItemSelected)
        {
            //WeighingIsCreated = true;
            PeseeAvantUsinageViewModel mclass = new PeseeAvantUsinageViewModel();
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{764DB266-8CAA-470D-BDBE-24BE37BD015E}"), UserName);

            mclass._PeseeAvantUsinage = new PeseeAvantUsinage();
            mclass._PeseeAvantUsinage = JSON.Deserialize<PeseeAvantUsinage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._PeseeAvantUsinage.fnGet(mclass._PeseeAvantUsinage.ID);
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            CapturedTare = mclass._PeseeAvantUsinage.TarePalette > 0 ? int.Parse(mclass._PeseeAvantUsinage.TarePalette.ToString("#")) : 0;
            //mclass._PeseeAvantUsinage.IsManual = false;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeAvantUsinage", Model = mclass, ViewData = ViewData};
        }

        public ActionResult onConsult(string ItemSelected)
        {
            //WeighingIsCreated = true;
            PeseeAvantUsinageViewModel mclass = new PeseeAvantUsinageViewModel();            
            ViewData["CanFinalize"] = false;

            mclass._PeseeAvantUsinage = new PeseeAvantUsinage();
            mclass._PeseeAvantUsinage = JSON.Deserialize<PeseeAvantUsinage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._PeseeAvantUsinage.fnGet(mclass._PeseeAvantUsinage.ID);
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
            //mclass._PeseeAvantUsinage.IsManual = false;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeAvantUsinage", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onManualWeight(string ItemSelected = "")
        {
            PeseeAvantUsinageViewModel mclass = new PeseeAvantUsinageViewModel();
            string UserName = (string)Session["userName"];
            ViewData["CanFinalize"] = new Fonction().fnGetUserAccessStatus(Guid.Parse("{764DB266-8CAA-470D-BDBE-24BE37BD015E}"), UserName);

            mclass._PeseeAvantUsinage = new PeseeAvantUsinage();
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._PeseeAvantUsinage.IsManual = true;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPeseeAvantUsinage", Model = mclass, ViewData = ViewData };
        }

        public ActionResult onAddWeight(string IsManual = "")
        {           
            if (X.GetCmp<TextField>("txtDeliveryNumero").Text != string.Empty)
                ViewData["NumeroLivraison"] = X.GetCmp<TextField>("txtDeliveryNumero").Text;
            if (X.GetCmp<Hidden>("txtImmatriculation").Text != string.Empty)
                ViewData["LivraisonImmatriculation"] = X.GetCmp<Hidden>("txtImmatriculation").Text;
            if (X.GetCmp<Hidden>("txtBonDeLivraisonID").Text != string.Empty)
                ViewData["BonDeLivraisonID"] = Guid.Parse(X.GetCmp<Hidden>("txtBonDeLivraisonID").Text);
            if (X.GetCmp<Hidden>("hidNombreSacsLivraison").Text != string.Empty)
                ViewData["NbrSacLivraisons"] = int.Parse(X.GetCmp<Hidden>("hidNombreSacsLivraison").Text);
            if (X.GetCmp<Hidden>("txtTareUnitaire").Text != string.Empty)
                ViewData["tareSacs"] = decimal.Parse(X.GetCmp<Hidden>("txtTareUnitaire").Text);

            PeseeAvantUsinage pesee = new PeseeAvantUsinage();
            
            if (!string.IsNullOrEmpty(IsManual))            
                pesee.IsManual = bool.Parse(IsManual);
            else
                pesee.IsManual = false;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormAddWeight", Model = pesee, ViewData = ViewData };
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("PeseeAvantUsinageCP");
            mform.ToggleCollapse();            
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemCampagne, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            string Campagne = string.IsNullOrEmpty(ItemCampagne) ? "-1" : ItemCampagne;
            if (!string.IsNullOrEmpty(ItemCampagne) && ItemCampagne.Contains("null"))
            {
                Campagne = "";
            }

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            string statut = string.IsNullOrEmpty(ItemStatus) ? "NA" : ItemStatus;

            var mListe = (new PeseeAvantUsinage()).fnSelect(ItemCampagne, StartDate, EndDate, statut);

            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListePeseeAvtUsinage");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCampagne",ItemCampagne),
                                    new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus",ItemStatus)
                                });

                X.GetCmp<FormPanel>("PeseeAvantUsinageCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing Before Cleaning : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnSelectDelivery(string productionID)
        {
            try
            {
                Guid OrdreProduction = Guid.Empty;

                if (!string.IsNullOrEmpty(productionID))
                    OrdreProduction = Guid.Parse(productionID);

                OrdreProduction mModel = new OrdreProduction();
                mModel.ID = OrdreProduction;
                //BonDeLivraisonViewModel mclass = new BonDeLivraisonViewModel();

                //var StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
                //var EndDate = DateTime.Now.ToShortDateString();
                //return new Ext.Net.MVC.PartialViewResult { ViewName = "PeseeProduction_DeliveryNotes", Model = mclass };
                return new Ext.Net.MVC.PartialViewResult { ViewName = "PeseeAvantUsinage_Deliveries", Model = mModel };

            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing Before Cleanig  : Bon De Livraison",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();                
            }

        }

        public ActionResult onCancel(string ItemSelected)
        {
            try
            {
                PeseeAvantUsinage pesee = JSON.Deserialize<PeseeAvantUsinage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = pesee.fnGet(pesee.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Weighing Before Cleaning, loading failed.");

                pesee.UtilisateurModification = (string)Session["userName"];

                
                result = pesee.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Weighing Before Cleaning, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePeseeAvtUsinage");

                    ModelProxy mProxy = mstore.GetById(pesee.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(pesee);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing Before Cleaning : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitDeliveryNote(string ItemSelected)
        {
            //BonDeLivraison mclass = new BonDeLivraison();
            //mclass = JSON.Deserialize<BonDeLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            //X.GetCmp<Hidden>("txtLivraisonID").SetValue(mclass.Livraison.ID);
            //X.GetCmp<Hidden>("txtLivraisonImmatriculation").SetValue(mclass.Livraison.Immatriculation);
            //X.GetCmp<TextField>("txtDeliveryNumero").SetValue(mclass.Livraison.Numero);
            //X.GetCmp<Window>("PeseeProduction_DeliveyNotes").Close();
            CompositionUsinageLivraison mComposition = new CompositionUsinageLivraison();
            mComposition = JSON.Deserialize<CompositionUsinageLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            X.GetCmp<TextField>("txtDeliveryNumero").SetValue(mComposition.BonDeLivraison.Livraison.Numero);
            X.GetCmp<TextField>("txtNombreSacsDelivery").SetValue(mComposition.NombreSacs);
            X.GetCmp<Hidden>("hidNombreSacsLivraison").SetValue(mComposition.NbreSacsTotalLivraison);
            X.GetCmp<TextField>("txtPoidsBrutComposition").SetValue(mComposition.PoidsBrut);
            X.GetCmp<Hidden>("txtImmatriculation").SetValue(mComposition.Immatriculation);
            X.GetCmp<Hidden>("txtTareUnitaire").SetValue(mComposition.TareSacs);
            X.GetCmp<Hidden>("txtBonDeLivraisonID").SetValue(mComposition.BonDeLivraison.ID);
            X.GetCmp<Window>("PeseeAvantUsinage_Deliveries").Close();
            return this.Direct();
        }

        public ActionResult SubmitDeliveryNumber(string ItemDeliveryNumber)
        {
            try
            {
                BonDeLivraison mclass = new BonDeLivraison();
                if (string.IsNullOrEmpty(ItemDeliveryNumber))
                {
                    return this.Direct();
                }
                if (mclass.fnGetByDeliveryNumber(ItemDeliveryNumber))
                {
                    if (mclass.ID != Guid.Empty)
                    {
                        X.GetCmp<Hidden>("txtLivraisonID").SetValue(mclass.Livraison.ID);
                        X.GetCmp<TextField>("txtDeliveryNumero").SetValue(mclass.Livraison.Numero);
                    }
                    else
                    {
                        X.MessageBox.Show(new MessageBoxConfig
                        {
                            Title = "Weighing Before Cleanig  : Bon De Livraison",
                            Message = "Bon De Livraison not Found, Please Retry !",
                            Buttons = MessageBox.Button.OK,
                            Icon = MessageBox.Icon.WARNING
                        });
                        return this.Direct();
                    }
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Bon De Livraison : Submit Delivery",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();

        }

        public ActionResult LoadListOfAvailableDeliveryNotes(string ItemProduction, string ItemListeAdded = "", string ItemPeseeID = null)
        {
            Guid ProductionID = ItemProduction == "" ? Guid.Empty : Guid.Parse(ItemProduction);
            Guid? peseeID = string.IsNullOrEmpty(ItemPeseeID) ? Guid.Empty : Guid.Parse(ItemPeseeID);

            if (peseeID == Guid.Empty)
                peseeID = null;
            //DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            //DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            //var mListe = (new BonDeLivraison()).fnSelectAvailableForInvoice(Fournisseur, StartDate, EndDate);
            var mListe = (new CompositionUsinageLivraison()).fnSelectAvailableDeliveries(ProductionID, peseeID);
            return this.Store(mListe);
        }

        public ActionResult SetAvailableNbrSacs(string ItemListeAdded = "", string ItemListeToAdd = "")
        {
            if (!string.IsNullOrEmpty(ItemListeAdded) && ItemListeAdded != "[]" && !string.IsNullOrEmpty(ItemListeToAdd) && ItemListeToAdd != "[]")
            {
                List<PeseeProductionPalette> oldListe = JSON.Deserialize<List<PeseeProductionPalette>>(ItemListeAdded, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                List<CompositionUsinageLivraison> newListe = JSON.Deserialize<List<CompositionUsinageLivraison>>(ItemListeToAdd, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });                
                int nbr = 0;
                foreach (CompositionUsinageLivraison itemNew in newListe)
                {
                    nbr = 0;
                    foreach (PeseeProductionPalette itemOld in oldListe.Where(x => x.NumeroLivraison == itemNew.LivraisonID))
                    {
                        nbr += itemOld.NombreSacs;                        
                    }
                    if (itemNew.NbreSacsTotalLivraison != (nbr + itemNew.NombreSacs))
                    {
                        itemNew.NombreSacs = itemNew.NbreSacsTotalLivraison - nbr;
                    }

                }

                Store mStoreMelange = X.GetCmp<Store>("storeListDeliveryNotes");
                mStoreMelange.RemoveAll();
                mStoreMelange.Add(newListe.Where(n => n.NombreSacs > 0));
            }

            return this.Direct();
        }


        public ActionResult OnRefreshForProduction(string ItemQuart, string ItemMelangeur, string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatut)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeSelectOrdreProduction");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemQuart",ItemQuart),
                                    new Ext.Net.Parameter("ItemMelangeur",ItemMelangeur),
                                    new Ext.Net.Parameter("ItemCertification",ItemCertification),
                                    new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatut",ItemStatut)
                                });


                //X.GetCmp<FormPanel>("OrdreProductionCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production Order : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult OnRefreshForAvailableDeliveryNotes(string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd)
        {
            Store mstore = X.GetCmp<Store>("storeListDeliveryNotes");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd)
                                });

            string title = X.GetCmp<FormPanel>("CriteriaPanelDeliveryNote").Title;

            title += " Fournisseur : " + X.GetCmp<ComboBox>("cmbFournisseurFD").SelectedItem.Text;

            title += ", From " + X.GetCmp<DateField>("dtpStartDateFD").RawText.ToString() + " to " + X.GetCmp<DateField>("dtpEndDateFD").RawText.ToString();

            X.GetCmp<FormPanel>("CriteriaPanelDeliveryNote").Title = title;

            // collapse criterias areas
            //X.GetCmp<FormPanel>("CriteriaPanelDeliveryNote").Collapse(Direction.Top, false);

            return this.Direct();
        }

        public ActionResult SubmitWeightPAU(string ItemSelected = "")
        {
            int mIndex = 0;
            decimal tareSacs = 0;
            int NbrSacLivraisons = 0;

            Parametres mParam = new Parametres(0);
            List<PeseeProductionPalette> ListePalettesPesees = JSON.Deserialize<List<PeseeProductionPalette>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            if (ListePalettesPesees != null) mIndex = ListePalettesPesees.Count;

            PeseeProductionPalette mPesee = new PeseeProductionPalette();
            mPesee.BonDeLivraison = new BonDeLivraison();
            mPesee.ID = Guid.NewGuid();

            if (X.GetCmp<TextField>("txtNombreSacsPAU").Text != string.Empty) mPesee.NombreSacs = int.Parse(X.GetCmp<TextField>("txtNombreSacsPAU").Text);
            if (X.GetCmp<TextField>("txtPoidsBrutPAU").Text != string.Empty) mPesee.PoidsBrut = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrutPAU").Text);
            if (X.GetCmp<TextField>("hidDeliveryNumero").Text != string.Empty) mPesee.NumeroLivraison = X.GetCmp<TextField>("hidDeliveryNumero").Text;
            if (X.GetCmp<Hidden>("hidImmatriculation").Text != string.Empty) mPesee.LivraisonImmatriculation = X.GetCmp<Hidden>("hidImmatriculation").Text;
            if (X.GetCmp<Hidden>("hidBonDeLivraisonID").Text != string.Empty) mPesee.BonDeLivraison.ID = Guid.Parse(X.GetCmp<Hidden>("hidBonDeLivraisonID").Text);            
            if (X.GetCmp<Hidden>("NombreSacsLivraison").Text != string.Empty) NbrSacLivraisons = int.Parse(X.GetCmp<Hidden>("NombreSacsLivraison").Text);
            if (X.GetCmp<Hidden>("hidTareUnitaire").Text != string.Empty) tareSacs = decimal.Parse(X.GetCmp<Hidden>("hidTareUnitaire").Text);
            mPesee.TareSacs = mPesee.NombreSacs * tareSacs;
            mPesee.IsNew = true;
            mPesee.DatePesee = DateTime.Now;

            if (mPesee.NombreSacs > 0 && mPesee.PoidsBrut > 0)
            {
                //if (mPesee.NombreSacs > NbrSacLivraisons)
                //{
                //    X.MessageBox.Show(new MessageBoxConfig
                //    {
                //        Title = "Weighing Before Cleaning : Data Validation",
                //        Message = "The number Of bags of the delivery is lower than selected bags !",
                //        Buttons = MessageBox.Button.OK,
                //        Icon = MessageBox.Icon.WARNING
                //    });
                //    return this.Direct();
                //}

                if (mPesee.NombreSacs > mParam.SacsLivraisonsAutoriseParPalette)
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Weighing Before Cleaning : Data Validation",
                        Message = "Verify Number Of Bags Per Pallet",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }

                if (ListePalettesPesees != null && (ListePalettesPesees.Count > mParam.NombrePalettesAutoriseAuPesePalette))
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Weighing Before Cleaning : Data Validation",
                        Message = "Can't Weight New Pallet",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }

                //if (ListePalettesPesees != null && ((ListePalettesPesees.Count > 0) && ((ListePalettesPesees.Where(l => l.NumeroLivraison == mPesee.NumeroLivraison).Sum(x => x.NombreSacs) + mPesee.NombreSacs) > NbrSacLivraisons)))
                //{
                //    X.MessageBox.Show(new MessageBoxConfig
                //    {
                //        Title = "Weighing Before Cleaning : Data Validation",
                //        Message = "Total Nbr Of Bag is higher than authorized Number Of bags",
                //        Buttons = MessageBox.Button.OK,
                //        Icon = MessageBox.Icon.WARNING
                //    });
                //    return this.Direct();
                //}



                Store mstore = X.GetCmp<Store>("storeListeWeightPAU");
                mstore.Insert(mIndex,mPesee);
                X.GetCmp<RowSelectionModel>("rowWeightPAU").Select(mIndex);
                
                X.GetCmp<Window>("FormAddWeight").Close();
            }            
            return this.Direct();
        }

        //public ActionResult SubmitFormMethod(string ItemSelected)
        //{
        //    DataSource _db = new DataSource();
        //    DataTransaction mtran = new DataTransaction();

        //    try
        //    {
        //        PeseeAvantUsinage mPesee = new PeseeAvantUsinage();
        //        PeseeProductionPalette palette;
        //        Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

        //        if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew && WeighingIsCreated == false)
        //            mPesee.IsNew = true;
        //        else
        //        {
        //            formExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
        //            mPesee.IsNew = false;
        //            if (WeighingID == Guid.Empty)
        //                mPesee.fnGet(Guid.Parse(GetFormValue("TxtPeseePauID")));
        //            else
        //                mPesee.fnGet(WeighingID);

        //            if (mPesee == null || mPesee.ID == Guid.Empty)
        //                throw new Exception("SubmitFormMethod : Weighing load failed.");
        //        }

        //        bool result = false;
        //        bool resultPalette = true;
        //        mPesee = MapFormToObject(mPesee);

        //        _db = mPesee.db();
        //        mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
        //        result = mPesee.fnUpdate(mtran);

        //        if (WeighingIsCreated)
        //            WeighingID = mPesee.ID;

        //        if (result)
        //        {                    

        //            List<PeseeProductionPalette> item = new List<PeseeProductionPalette>();
        //            if (!string.IsNullOrEmpty(ItemSelected) || ItemSelected != "[]")
        //                item = JSON.Deserialize<List<PeseeProductionPalette>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

        //            if (item.Count > 0)
        //            {

        //                for (int i = 0; i < item.Count(); i++)
        //                {
        //                    palette = new PeseeProductionPalette();
        //                    palette.PeseeProduction = new PeseeAvantUsinage();
        //                    palette.BonDeLivraison = new BonDeLivraison();

        //                    palette.SetDataSource(_db);

        //                    palette.PeseeProduction.ID = WeighingIsCreated ? WeighingID : mPesee.ID;                            
        //                    palette.NombreSacs = item[i].NombreSacs;
        //                    palette.PoidsBrut = item[i].PoidsBrut;
        //                    palette.TareSacs = item[i].TareSacs;
        //                    palette.IsNew = item[i].IsNew;
        //                    palette.BonDeLivraison.ID = item[i].BonDeLivraison.ID;
        //                    palette.NumeroLivraison = item[i].NumeroLivraison;
        //                    palette.LivraisonImmatriculation = item[i].LivraisonImmatriculation;
        //                    palette.DatePesee = item[i].DatePesee;

        //                    palette.UtilisateurCreation = (string)Session["userName"];
        //                    palette.UtilisateurModification = (string)Session["userName"];
        //                    Store mstorePalette = X.GetCmp<Store>("storeListeWeightPAU");
        //                    ModelProxy mProxyPalette = mstorePalette.GetById(item[i].ID);

        //                    if (palette.IsNew)
        //                    {
        //                        resultPalette = palette.fnUpdate(mtran);
        //                    }
        //                    item[i].IsNew = false;
                            
        //                    mProxyPalette.BeginEdit();

        //                    mProxyPalette.Set(item[i]);

        //                    mProxyPalette.Commit();

        //                    mProxyPalette.EndEdit();
        //                    if (!resultPalette)
        //                    {
        //                        break;
        //                    }
        //                }
        //            }

        //            if (!resultPalette)
        //            {
        //                _db.RollBackTransaction(mtran);
        //            }

        //            _db.CommitTransaction(mtran);
        //            WeighingIsCreated = true;
        //            WeighingID = mPesee.ID;
        //        }

        //        if (result)
        //        {
        //            Store mstore = X.GetCmp<Store>("storeListePeseeAvtUsinage");
        //            if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
        //            {
        //                mstore.Insert(0, mPesee);
        //                X.GetCmp<RowSelectionModel>("rowPeseeAvantUsinage").Select(0);
        //            }
        //            else
        //            {
        //                ModelProxy mProxy = mstore.GetById(mPesee.ID);

        //                mProxy.BeginEdit();

        //                mProxy.Set(mPesee);

        //                mProxy.Commit();

        //                mProxy.EndEdit();
        //            }

        //            //X.GetCmp<Window>("FormPeseeAvantUsinage").Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        X.MessageBox.Show(new MessageBoxConfig
        //        {
        //            Title = "Weighing Before Cleaning : Data Validation",
        //            Message = ex.Message,
        //            Buttons = MessageBox.Button.OK,
        //            Icon = MessageBox.Icon.WARNING
        //        });
        //    }
        //    return this.Direct();
        //}

        public ActionResult SubmitFormWeighingBeforePallets()
        {            
            try
            {
                Guid peseeID = Guid.Empty;
                DateTime datePesee = new DateTime();
                PeseeAvantUsinage mPesee = new PeseeAvantUsinage();                
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mPesee.IsNew = true;
                else
                {
                    formExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                    mPesee.IsNew = false;
                    bool isGuid = Guid.TryParse(GetFormValue("TxtPeseePauID"), out peseeID);
                    if (isGuid)
                        mPesee.fnGet(peseeID);
                    //else
                    //    mPesee.fnGet(WeighingID);

                    if (mPesee == null || mPesee.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Weighing load failed.");

                    datePesee = mPesee.DatePeseeProduction;
                }

                bool result = false;
                
                mPesee = MapFormToObject(mPesee);
                if (mPesee.IsNew == false && (datePesee.Date == mPesee.DatePeseeProduction.Date))
                    mPesee.DatePeseeProduction = datePesee;
                                
                result = mPesee.fnUpdate();

                //if (WeighingIsCreated)
                //    WeighingID = mPesee.ID;

                if (result)
                {                    
                    //WeighingIsCreated = true;
                    //WeighingID = mPesee.ID;

                    Store mstore = X.GetCmp<Store>("storeListePeseeAvtUsinage");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mPesee);
                        X.GetCmp<RowSelectionModel>("rowPeseeAvantUsinage").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mPesee.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mPesee);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }
                    X.GetCmp<Container>("zonePalettes").Enable();
                    X.GetCmp<Container>("BtnAddWeight").Enable();
                    X.GetCmp<Hidden>("TxtPeseePauID").SetValue(mPesee.ID);
                    X.GetCmp<Hidden>("hiddenExecMode").SetValue("Update");
                    //X.GetCmp<Panel>("PanelBtnSave").Hide();
                    //X.GetCmp<Window>("FormPeseeAvantUsinage").Close();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing Before Cleaning : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        public ActionResult SubmitWeighingPallet(string ItemSelected= "", string peseeID = "", string nbrSacs = "", string poidsPalette = "" )
        {            
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                PeseeAvantUsinage mPesee = new PeseeAvantUsinage();
                PeseeProductionPalette mPalette;
                DateTime datePesee = new DateTime();
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                formExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                mPesee.IsNew = false;

                Guid IdPesee = Guid.Empty;
                bool isGuid = Guid.TryParse(peseeID, out IdPesee);

                if (isGuid)
                    mPesee.fnGet(IdPesee);                

                if (mPesee == null || mPesee.ID == Guid.Empty)
                    throw new Exception("SubmitFormMethod : Weighing load failed.");

                bool result = false;
                bool resultPalette = true;
                datePesee = mPesee.DatePeseeProduction;
                mPesee = MapFormToObject(mPesee);

                if (mPesee.IsNew == false && (datePesee.Date == mPesee.DatePeseeProduction.Date))
                    mPesee.DatePeseeProduction = datePesee;

                _db = mPesee.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = mPesee.fnUpdate(mtran);

                //if (WeighingIsCreated)
                //    WeighingID = mPesee.ID;

                if (result)
                {
                    List<PeseeProductionPalette> ListePalettesPesees = JSON.Deserialize<List<PeseeProductionPalette>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (ListePalettesPesees.Count > 0)
                    {
                        foreach (PeseeProductionPalette item in ListePalettesPesees.Where(p => p.IsNew == true))
                        {
                            mPalette = new PeseeProductionPalette();
                            mPalette.PeseeProduction = new PeseeAvantUsinage();
                            mPalette.BonDeLivraison = new BonDeLivraison();

                            mPalette.SetDataSource(_db);
                            mPalette.ID = item.ID;
                            mPalette.PeseeProduction.ID = mPesee.ID;
                            mPalette.NombreSacs = item.NombreSacs;
                            mPalette.PoidsBrut = item.PoidsBrut;
                            mPalette.TareSacs = item.TareSacs;
                            mPalette.IsNew = item.IsNew;
                            mPalette.BonDeLivraison.ID = item.BonDeLivraison.ID;
                            mPalette.NumeroLivraison = item.NumeroLivraison;
                            mPalette.LivraisonImmatriculation = item.LivraisonImmatriculation;
                            mPalette.DatePesee = item.DatePesee;
                            mPalette.UtilisateurCreation = (string)Session["userName"];
                            mPalette.UtilisateurModification = (string)Session["userName"];
                            Store mstorePalette = X.GetCmp<Store>("storeListeWeightPAU");
                            if (mPalette.IsNew)
                            {
                                resultPalette = mPalette.fnUpdate(mtran);

                                ModelProxy mProxyPalette = mstorePalette.GetById(item.ID);
                                item.IsNew = false;                                
                                mProxyPalette.BeginEdit();
                                mProxyPalette.Set(item);
                                mProxyPalette.Commit();
                                mProxyPalette.EndEdit();
                            }
                            //mPalette.IsNew = false;

                            if (!resultPalette)
                            {
                                _db.RollBackTransaction(mtran);
                                result = false;
                                break;
                            }
                        }
                    }
                    if (result)
                    {
                        _db.CommitTransaction(mtran);
                        //WeighingIsCreated = true;
                        //WeighingID = mPesee.ID;

                        Store mstore = X.GetCmp<Store>("storeListePeseeAvtUsinage");
                        ModelProxy mProxy = mstore.GetById(mPesee.ID);
                        mProxy.BeginEdit();
                        mProxy.Set(mPesee);
                        mProxy.Commit();
                        mProxy.EndEdit();
                        //X.GetCmp<Window>("FormAddWeight").Close();
                        X.GetCmp<Hidden>("TxtPeseePauID").SetValue(mPesee.ID);
                        X.GetCmp<Hidden>("hiddenExecMode").SetValue("Update");
                    }
                }                
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing Before Cleaning : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }


        public ActionResult Finalize(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                PeseeAvantUsinage mPesee = new PeseeAvantUsinage();
                Guid peseeID = Guid.Empty;
                PeseeProductionPalette palette;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                formExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                mPesee.IsNew = false;
                bool IsGuid = Guid.TryParse(GetFormValue("TxtPeseePauID"), out peseeID);

                if (IsGuid)
                    mPesee.fnGet(peseeID);                

                if (mPesee == null || mPesee.ID == Guid.Empty)
                    throw new Exception("SubmitFormMethod : Weighing load failed.");

                bool result = false;
                bool resultPalette = true;
                DateTime datePesee = new DateTime();

                datePesee = mPesee.DatePeseeProduction;
                mPesee = MapFormToObject(mPesee);
                if (mPesee.IsNew == false && (datePesee.Date == mPesee.DatePeseeProduction.Date))
                    mPesee.DatePeseeProduction = datePesee;

                if (mPesee.TarePalette <= 0)
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Weighing Before Cleaning : Data Validation",
                        Message = "Set Tare Des Palettes Before Finalize",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }
                mPesee.Statut = "AP";
                _db = mPesee.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = mPesee.fnUpdate(mtran);

                if (result)
                {
                    List<PeseeProductionPalette> item = JSON.Deserialize<List<PeseeProductionPalette>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (item.Count > 0)
                    {

                        for (int i = 0; i < item.Count(); i++)
                        {
                            palette = new PeseeProductionPalette();
                            palette.PeseeProduction = new PeseeAvantUsinage();
                            palette.BonDeLivraison = new BonDeLivraison();

                            palette.SetDataSource(_db);

                            palette.PeseeProduction.ID = mPesee.ID;
                            palette.NombreSacs = item[i].NombreSacs;
                            palette.PoidsBrut = item[i].PoidsBrut;
                            palette.TareSacs = item[i].TareSacs;
                            palette.IsNew = item[i].IsNew;
                            palette.BonDeLivraison.ID = item[i].BonDeLivraison.ID;
                            palette.NumeroLivraison = item[i].NumeroLivraison;
                            palette.LivraisonImmatriculation = item[i].LivraisonImmatriculation;
                            palette.DatePesee = item[i].DatePesee;

                            palette.UtilisateurCreation = (string)Session["userName"];
                            palette.UtilisateurModification = (string)Session["userName"];
                            if (palette.IsNew)
                            {
                                resultPalette = palette.fnUpdate(mtran);
                            }

                            if (!resultPalette)
                            {
                                break;
                            }
                        }
                    }
                    if (!resultPalette)
                    {
                        _db.RollBackTransaction(mtran);
                    }
                    _db.CommitTransaction(mtran);
                }


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePeseeAvtUsinage");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, mPesee);
                        X.GetCmp<RowSelectionModel>("rowPeseeAvantUsinage").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(mPesee.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mPesee);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormPeseeAvantUsinage").Close();

                    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                    X.Js.Call("GenerateTicket", mPesee.ID, BaseUrl);
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing Before Cleaning : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        private PeseeAvantUsinage MapFormToObject(PeseeAvantUsinage mClass)
        {
            Campagne campagne = new Campagne();
            campagne.Designation = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text.ToString();
            mClass.Campagne = campagne;

            OrdreProduction production = new OrdreProduction();                       
            production.ID = Guid.Parse(GetFormValue("cmbProduction"));
            production.NumeroProduction = X.GetCmp<ComboBox>("cmbProduction").SelectedItem.Text;            
            mClass.OrdreProduction = production;

            //Livraison livraison = new Livraison();            
            //livraison.ID = Guid.Parse(GetFormValue("txtLivraisonID"));
            //livraison.Numero = X.GetCmp<TextField>("txtDeliveryNumero").Text;
            //livraison.Immatriculation = X.GetCmp<Hidden>("txtLivraisonImmatriculation").Text;
            BonDeLivraison mBonDeLivraison = new BonDeLivraison();
            mBonDeLivraison.ID = Guid.Parse(GetFormValue("txtBonDeLivraisonID"));
            //livraison.ID = Guid.Parse(GetFormValue("txtLivraisonID"));
            //livraison.Numero = X.GetCmp<TextField>("txtDeliveryNumero").Text;

            mClass.BonDeLivraison = mBonDeLivraison;

            mClass.DatePeseeProduction = DateTime.Parse(X.GetCmp<DateField>("txtDatePesee").RawText.ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second).AddMilliseconds(DateTime.Now.Millisecond);            
            mClass.Statut = "NA";

            mClass.PeseeProductionType = new PeseeProductionType();
            mClass.PeseeProductionType.ID = (new Parametres(0)).PeseeAvantUsinage;
            mClass.IsManual = bool.Parse(X.GetCmp<Hidden>("txtEstManuel").Text);

            if (X.GetCmp<TextField>("txtNombreSacs").Text != string.Empty) mClass.NombreSacs = int.Parse(X.GetCmp<TextField>("txtNombreSacs").Text);
            if (X.GetCmp<TextField>("txtPoidsBrut").Text != string.Empty) mClass.PoidsBrut = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrut").Text);
            if (X.GetCmp<TextField>("txtTareSacs").Text != string.Empty) mClass.TareSacs = decimal.Parse(X.GetCmp<TextField>("txtTareSacs").Text);

            if (X.GetCmp<TextField>("txtTarePalettes").Text != string.Empty) mClass.TarePalette = decimal.Parse(X.GetCmp<TextField>("txtTarePalettes").Text);
            //Prevenir Changement depuis la console du navigateur
            //if (!mClass.IsManual && (mClass.TarePalette != CapturedTare))
            //{
            //    throw new Exception("An error occured, please retry Weighing !");
            //}

            if (X.GetCmp<TextField>("txtTarePalettes").Text != string.Empty) mClass.TarePalette = decimal.Parse(X.GetCmp<TextField>("txtTarePalettes").Text);
            if (X.GetCmp<TextField>("txtNetWeight").Text != string.Empty) mClass.PoidsNet = decimal.Parse(X.GetCmp<TextField>("txtNetWeight").Text);

            mClass.Commentaire = X.GetCmp<TextField>("txtComment").Text;
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }

        public ActionResult SelectPalletsWeight(string PeseeId)
        {
            try
            {
                Guid IdPesee = Guid.Parse(PeseeId);
                PeseeProductionPalette mPalettes = new PeseeProductionPalette();
                var Liste = mPalettes.fnSelect(IdPesee);
                return this.Store(Liste);
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing Before Cleaning : Data Validation",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Store(null);
            }
            
        }

        public ActionResult RemoveWeighing(string ItemSelected)
        {
            try
            {
                PeseeProductionPalette mPalette = JSON.Deserialize<PeseeProductionPalette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });                

                if (mPalette.ID != Guid.Empty)
                {
                    Store mstore = X.GetCmp<Store>("storeListeWeightPAU");

                    if (mPalette.IsNew)
                    {
                        ModelProxy _proxy = mstore.GetById(mPalette.ID);
                        _proxy.Drop();
                        return this.Direct();
                    }
                    else
                    {                        
                        mPalette.fnGet(mPalette.ID);
                        if (mPalette == null || mPalette.ID == Guid.Empty)
                            throw new Exception("RemoveWeighing : Retirer Weighing failed.");

                        mPalette.UtilisateurModification = (string)Session["userName"];

                        bool result = mPalette.fnRemove();

                        if (result)
                        {
                            ModelProxy mProxy = mstore.GetById(mPalette.ID);
                            mProxy.Drop();                                                        
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing Before Cleaning - : Retirer Pallets Weight",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
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
                            X.GetCmp<Window>("PeseeAvantUsinage_Production").Close();
                            X.GetCmp<Button>("BtnAddWeight").Enable();
                            return this.Direct();
                        }
                        else
                        {
                            X.GetCmp<Hidden>("txtOrdreProduction").SetValue("");
                            X.GetCmp<Hidden>("txtOrdreProductionID").SetValue(Guid.Empty);
                            X.MessageBox.Show(new MessageBoxConfig
                            {
                                Title = "Production : Production Order",
                                Message = "Production Order Not Found, Please Retry !",
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
                        X.GetCmp<Window>("PeseeAvantUsinage_Production").Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production Order - Data Validation",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnSelectProduction()
        {
            return new Ext.Net.MVC.PartialViewResult { ViewName = "PeseeAvantUsinage_Production" };
        }

        public ActionResult OnPrintWeighingTicket(string IdPesee, string IsCopy)
        {
            string BaseUrl = "";
            bool ReportIsCopy = false;
            try
            {                
                if (string.IsNullOrEmpty(IsCopy))
                    ReportIsCopy = true;

                if (string.IsNullOrEmpty(IdPesee))
                    return this.Direct();

                    PeseeAvantUsinage mPesee = new PeseeAvantUsinage();
                bool result = mPesee.fnGet(Guid.Parse(IdPesee));
                if (result && mPesee.Statut != "AP")
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Ticket de Pesée",
                        Message = "Weighing Not Finalized",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }

                BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Ticket de Pesée",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }            
            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Ticket{0}', '{1}/PeseeAvantUsinage/ViewWeighingTicket?id={0}&IsCopy={2}', this, 'Ticket De pesée','')", IdPesee, BaseUrl, ReportIsCopy));
        }

        public ActionResult ViewWeighingTicket(string id, bool IsCopy)
        {
            try
            {
                XtraReport report = new TicketPeseeAvantUsinage();                

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["peseeID"].Value = id;
                report.Parameters["IsCopy"].Value = IsCopy;

                //ReportParam param = new ReportParam();
                //List<ReportParam> mListParam = new List<ReportParam>();
                //param.Name = "peseeID";
                //param.Value = id;
                //mListParam.Add(param);
                //param = new ReportParam();
                //param.Name = "IsCopy";
                //param.Value = IsCopy.ToString();
                //mListParam.Add(param);

                //string mParm = JSON.Serialize(mListParam);
                ViewData["Report"] = report;
                return View("ViewReportResult", ViewData = ViewData);
                //string reportFilePath = @"C:\Temp\Report1.repx";
                //report.SaveLayoutToXml(reportFilePath);            
                //string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                //string ip = "192.168.8.104";
                // initialize services
                //ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);

                //proxy.DirectPrintTicket(mParm, ReportToPrint);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing : Ticket",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            //PrintMethod.Print(report);          
            return View();
        }

        public ActionResult OnPrintWeighingList()
        {
            Parametres mParam = new Parametres(0);

            ViewData["Campagne"] = mParam.Campagne;
            ViewData["Titre"] = "Weighing Before Cleaning";
            ViewData["actionToDo"] = "PrintList";
            ViewData["ControllerName"] = "PeseeAvantUsinage";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "ProductionWeighing_Print", ViewData = ViewData };
        }

        public ActionResult PrintList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {              
                XtraReport report = null;

                report = new rptPeseeAvantUsinageList() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["CampagneID"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

                report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Value.ToString();
                report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Text;

                report.Parameters["paramTypePesee"].Value = (new Parametres(0)).PeseeAvantUsinage;

                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateDebut").RawText);
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateFin").RawText);

                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/PeseeAvantUsinage/ViewList', this, 'Weighing Before Cleaning',''),App.ProductionWeighing_Print.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing Before Cleaning : Data Validation",
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

    }
}