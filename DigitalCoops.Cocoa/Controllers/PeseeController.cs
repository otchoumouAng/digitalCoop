using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
//using System.ServiceModel;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Business;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Components.Data;
using Tms.Components.Settings;
using Tms.LocalService;

namespace Tms2017.MVC.Controllers
{
    public class PeseeController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";

        //private int CapturedWeight { get; set; }

        static int _CapturedFirstWeight;
        static int _CapturedSecondWeight;

        /// <summary>
        /// Access routine for global variable.
        /// </summary>
        public static int CapturedFirstWeight
        {
            get
            {
                return _CapturedFirstWeight;
            }
            set
            {
                _CapturedFirstWeight = value;
            }
        }

        public static int CapturedSecondWeight
        {
            get
            {
                return _CapturedSecondWeight;
            }
            set
            {
                _CapturedSecondWeight = value;
            }
        }

        bool BtnReadClicked = false;
        public PeseeController()
        {
            //string cultureName = "en";
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
        }

        // GET: Pesee
        public ActionResult Index()
        {

            bool HasAccessToAllWeighBridges = true;

            PeseeViewModel viewModel = new PeseeViewModel();

            Bascule mClass = new Bascule();

            try
            {
                viewModel._Pesee = new Pesee();

                viewModel.HasAccessToAllWeighBridges = HasAccessToAllWeighBridges;

                viewModel.LoadBasculeMethod = viewModel.HasAccessToAllWeighBridges ? "LoadPontBasculeAll" : "LoadPontBascule";

               
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Pesee : Index", ex.Message).Show();

                mClass = new Bascule();
                mClass.ID = 0;
                mClass.Designation = "";
            }

            //if (HasAccessToAllWeighBridges)
            //{
            //    mClass.ID = -1;
            //    mClass.Designation = "{Tous}";
            //}

            string userName = (string)Session["userName"];
            mClass = GetCurrentScale(userName);

            viewModel._Bascule = mClass;

            viewModel.IsFirstWeighing = true;

            viewModel.IsScaleWeighing = true;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            Parametres mParam = new Parametres(0);
            ViewBag.DefaultCampagne = mParam.Campagne;

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();
            
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            viewModel.CriteriaTitle = String.Format("Site : {0}, Weighbridge = {1}, Campagne : {2}, Type De Livraison = {3}, Fournisseur : {4}, du {5} to {6}, Statut : {7}",mSiteParDefaut.Nom, mClass.Designation, mParam.Campagne, "{Tous}", "{Tous}", DateTime.Now.AddDays(-7).ToShortDateString(), DateTime.Now.ToShortDateString(), "{Tous}");

            #region Set Function's Access
            
            Fonction HasAccess = new Fonction();
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{A44F3177-5F8E-4B43-A1CE-8E7255F4099F}", UserName);
            
            if (HasAccess.fnGetUserAccessStatus("{03B3FF73-95F5-4AFD-91D3-F9F8B06DC2ED}", UserName) == false)
                X.GetCmp<Button>("btnFirstWeight").Disable();
            else
                X.GetCmp<Button>("btnFirstWeight").Enable();
                

            if (HasAccess.fnGetUserAccessStatus("{AED36D23-2A54-43EF-A19B-618DB673D2C1}", UserName) == false)
                X.GetCmp<Button>("btnPendingDeliveries").Disable();
            else
                X.GetCmp<Button>("btnPendingDeliveries").Enable();

            if (HasAccess.fnGetUserAccessStatus("{9EC9F2B2-E111-4655-A1A5-81F64B588035}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExport").Disable();
            else
                X.GetCmp<MenuItem>("mnuExport").Enable();

            if (HasAccess.fnGetUserAccessStatus("{2DAE2406-3BAE-443B-8B78-044476C026A7}", UserName) == false)
                X.GetCmp<MenuItem>("mnuConfigurePrinter").Disable();
            else
                X.GetCmp<MenuItem>("mnuConfigurePrinter").Enable();

            if (HasAccess.fnGetUserAccessStatus("{B6016D44-0FF8-4D87-84C4-457F946D4FA8}", UserName) == false)
                X.GetCmp<MenuItem>("mnuManualWeighing").Disable();
            else
                X.GetCmp<MenuItem>("mnuManualWeighing").Enable();

            if (HasAccess.fnGetUserAccessStatus("{1D6C6320-77C6-4514-9B79-D0DCBE084D40}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintListWeighing").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintListWeighing").Enable();

            if (HasAccess.fnGetUserAccessStatus("{E52F7892-8266-47E5-A50A-239430D03C09}", UserName) == false)
                X.GetCmp<MenuItem>("mnuTestScale").Disable();
            else
                X.GetCmp<MenuItem>("mnuTestScale").Enable();

            X.GetCmp<Hidden>("PehiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{F26C3EDD-02D5-41C9-A444-3BEA04110749}", UserName));
            X.GetCmp<Hidden>("PehiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{9EC9F2B2-E111-4655-A1A5-81F64B588035}", UserName));
            X.GetCmp<Hidden>("PehiddenPermFirstWeight").SetValue(HasAccess.fnGetUserAccessStatus("{03B3FF73-95F5-4AFD-91D3-F9F8B06DC2ED}", UserName));
            X.GetCmp<Hidden>("PehiddenPermSecondWeight").SetValue(HasAccess.fnGetUserAccessStatus("{01B05929-A863-4284-AE18-1837BAE892AC}", UserName));
            X.GetCmp<Hidden>("PehiddenPermChangeDeliveryType").SetValue(HasAccess.fnGetUserAccessStatus("{E7B5A494-4437-4854-BB61-B2FF3D4098E1}", UserName));
            X.GetCmp<Hidden>("PehiddenPermCorrectWeight").SetValue(HasAccess.fnGetUserAccessStatus("{91C1AEF1-2FF0-4549-8719-4B2D92281999}", UserName));
            X.GetCmp<Hidden>("PehiddenPermPrintWeighingTicket").SetValue(HasAccess.fnGetUserAccessStatus("{113F70DF-5C6C-48F3-837A-1D59CCD6FED1}", UserName));
            X.GetCmp<Hidden>("PehiddenPermConsultPendingDeliveries").SetValue(HasAccess.fnGetUserAccessStatus("{AED36D23-2A54-43EF-A19B-618DB673D2C1}", UserName));
            X.GetCmp<Hidden>("PehiddenPermManualWeighing").SetValue(HasAccess.fnGetUserAccessStatus("{B6016D44-0FF8-4D87-84C4-457F946D4FA8}", UserName));
            X.GetCmp<Hidden>("PehiddenPermConfigurePrinter").SetValue(HasAccess.fnGetUserAccessStatus("{2DAE2406-3BAE-443B-8B78-044476C026A7}", UserName));
            X.GetCmp<Hidden>("PehiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{A44F3177-5F8E-4B43-A1CE-8E7255F4099F}", UserName));
            
            #endregion


            return View(viewModel);
            
        }

        public ActionResult OnCaptureFirstWeight()
        {
            try
            {
                Parametres mParam = new Parametres(0);
                BtnReadClicked = true;
                CapturedFirstWeight = 0;
                int sWeight = 0;
                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                //string ip = "198.168.8.101";
                // initialize services  
                if (mParam.IsInDevelopment == false)
                {
                    ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);

                    sWeight = proxy.GetWeight();
                    //int sWeight = 20000;
                    X.GetCmp<TextField>("txtFirstGrossWeight").Text = sWeight.ToString();
                }
                else
                {
                    sWeight = 20000;
                    X.GetCmp<TextField>("txtFirstGrossWeight").Text = sWeight.ToString();

                }
                CapturedFirstWeight = sWeight;
                
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            return this.Direct();
        }

        public ActionResult OnCaptureSecondWeight()
        {
            try
            {
                Parametres mParam = new Parametres(0);
                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                //string ip = "198.168.8.101";
                // initialize services    
                int sWeight = 0;
                if (mParam.IsInDevelopment == false)
                {
                    ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);
                    //CapturedSecondWeight = 0;
                    sWeight = proxy.GetWeight();
                    //int sWeight = 10300;
                    X.GetCmp<TextField>("txtSecondGrossWeight").Text = sWeight.ToString();
                }
                else
                {
                    sWeight = 10300;
                    X.GetCmp<TextField>("txtSecondGrossWeight").Text = sWeight.ToString();                
                }
                CapturedSecondWeight = sWeight;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }

        private static Bascule GetCurrentScale()
        {
            Bascule mClass = new Bascule();

            //List<Bascule> mList = mClass.fnSelectBySite(new Parametres(0).Site);

            //if (mList.Count <= 0)
            //    throw new Exception("Pesee : fnSelectBySite : No Weighbridges sets up for this site.");

            //mClass.ID = mList[0].ID;

            //mClass.Designation = mList[0].Designation;

            //string ip = System.Web.HttpContext.Current.Request.UserHostAddress;

            string machineName = "BTECH003";            

            try
            {
                //ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);
                             
                //machineName = proxy.GetMachineName();

                mClass.fnGetByMachineName(machineName);
            }
            catch (Exception ex)
            {
                //mClass.ID = mList[0].ID;

                //mClass.Designation = mList[0].Designation;

                //machineName = X.GetCmp<ComboBox>("cmbBascule").SelectedItem.Text;

                mClass.fnGetByMachineName(machineName);

                //throw ex;
            }

           

            return mClass;
        }


        public static Bascule GetCurrentScale(string userName)
        {
            Bascule mClass = new Bascule();                     
            try
            {
                mClass.fnGetByUserName(userName);
            }
            catch (Exception ex)
            {

                mClass.fnGetByMachineName(userName);
            }

            return mClass;
        }


        public ActionResult OnSelectPendingDeliveries()
        {
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Pesee_PendingDeliveriesDetail" };
        }


        public ActionResult OnChangeDeliveryType(string ItemSelected)
        {

            Pesee mPesee = JSON.Deserialize<Pesee>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            PeseeViewModel viewModel = new PeseeViewModel();

            mPesee.fnGet(mPesee.ID);

            mPesee.Livraison.fnSelectByNumber(mPesee.Livraison.Numero);

            SacType mSacType = new SacType();
            mSacType.fnGet(mPesee.Livraison.SacType.ID);

            if (mSacType.ID == 0)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing ",
                    Message = "Please check your delivery !",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            viewModel.SacTypeID = mSacType.Tare.ToString();

            try
            {
                Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                mViewport.Mask();

                viewModel._Pesee = mPesee;

                viewModel.IsFirstWeighing = false;

                viewModel.IsScaleWeighing = true;

                // Added by AA on 21st of September 2017
                LivraisonType mTypeLivraison = new LivraisonType();

                mTypeLivraison.fnGet(mPesee.Livraison.LivraisonType.ID);

                viewModel._Pesee.Livraison.LivraisonType = mTypeLivraison;

                viewModel.WeighingWithVGM = mTypeLivraison.CalculeVGM;

                // Ouvrir les zones de saisies des poids
                viewModel.IsDevelopmentEnvironment = false;

                // entrer le numero de la livraison dans la zone prévue à cet effet
                viewModel.IsCallByPendingWindows = false;

                viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

                string userName = (String)Session["userName"];
                Bascule mClass = GetCurrentScale(userName);

                if (mClass.ID == 0)
                    throw new Exception("The scale is not set for this machine.");

                viewModel._Bascule = mClass;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Pesee_ChangeDeliveryTypeDetail", Model = viewModel };            
        }



        public ActionResult OnCorrectWeightManually(string ItemSelected)
        {

            Pesee mPesee = JSON.Deserialize<Pesee>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            PeseeViewModel viewModel = new PeseeViewModel();

            mPesee.fnGet(mPesee.ID);

            mPesee.Livraison.fnSelectByNumber(mPesee.Livraison.Numero);

            SacType mSacType = new SacType();

            mSacType.fnGet(mPesee.Livraison.SacType.ID);

            if (mSacType.ID == 0)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing",
                    Message = "Please check your delivery !",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });

                return this.Direct();
            }
            viewModel.SacTypeID = mSacType.Tare.ToString();



            try
            {
                Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                mViewport.Mask();

                viewModel._Pesee = mPesee;

                viewModel.IsFirstWeighing = false;

                viewModel.IsScaleWeighing = true;

                // Added by AA on 21st of September 2017
                LivraisonType mTypeLivraison = new LivraisonType();

                mTypeLivraison.fnGet(mPesee.Livraison.LivraisonType.ID);

                viewModel._Pesee.Livraison.LivraisonType = mTypeLivraison;

                viewModel.WeighingWithVGM = mTypeLivraison.CalculeVGM;

                // Ouvrir les zones de saisies des poids
                viewModel.IsDevelopmentEnvironment = true;

                // entrer le numero de la livraison dans la zone prévue à cet effet
                viewModel.IsCallByPendingWindows = false;

                viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

                string userName = (String)Session["userName"];
                Bascule mClass = GetCurrentScale(userName);

                if (mClass.ID == 0)
                    throw new Exception("The scale is not set for this machine.");

                viewModel._Bascule = mClass;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
                return this.Direct();
            }


            if(mPesee.PoidsP1 > 0 && mPesee.PoidsP2 == 0 && mPesee.PoidsBrut == 0) // 1ère pesée
            {
                return new Ext.Net.MVC.PartialViewResult { ViewName = "Pesee_CorrectFirstWeightDetail", Model = viewModel };
            }
            else //if (mPesee.PoidsP1 > 0 &&  mPesee.PoidsP2 > 0 && mPesee.PoidsBrut > 0) // 2è pesée
            {
                return new Ext.Net.MVC.PartialViewResult { ViewName = "Pesee_CorrectSecondWeightDetail", Model = viewModel };
            }

            //return new Ext.Net.MVC.PartialViewResult { ViewName = "Pesee_ChangeDeliveryTypeDetail", Model = viewModel };
        }

        public ActionResult OnConsult(string ItemSelected)
        {
            Pesee mPesee = JSON.Deserialize<Pesee>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            PeseeViewModel viewModel = new PeseeViewModel();

            mPesee.fnGet(mPesee.ID);

            mPesee.Livraison.fnSelectByNumber(mPesee.Livraison.Numero);

            SacType mSacType = new SacType();
            mSacType.fnGet(mPesee.Livraison.SacType.ID);

            if (mSacType.ID == 0)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing ",
                    Message = "Please check your delivery !",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            viewModel.SacTypeID = mSacType.Tare.ToString();

            try
            {
                //Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                //mViewport.Mask();

                viewModel._Pesee = mPesee;

                viewModel.IsFirstWeighing = (mPesee.PoidsP1 != 0 && mPesee.PoidsP2 == 0);

                viewModel.IsScaleWeighing = true;

                // Added by AA on 21st of September 2017
                LivraisonType mTypeLivraison = new LivraisonType();

                mTypeLivraison.fnGet(mPesee.Livraison.LivraisonType.ID);

                viewModel.WeighingWithVGM = mTypeLivraison.CalculeVGM;

                // Ouvrir les zones de saisies des poids
                viewModel.IsDevelopmentEnvironment = false;

                // entrer le numero de la livraison dans la zone prévue à cet effet
                viewModel.IsCallByPendingWindows = false;

                viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

                string userName = (String)Session["userName"];
                Bascule mClass = GetCurrentScale(userName);

                if (mClass.ID == 0)
                    throw new Exception("The scale is not set for this machine.");

                viewModel._Bascule = mClass;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            if(viewModel.IsFirstWeighing)
                return new Ext.Net.MVC.PartialViewResult { ViewName = "Pesee_FirstWeighingDetail", Model = viewModel };
            else
                return new Ext.Net.MVC.PartialViewResult { ViewName = "Pesee_SecondWeighingDetail", Model = viewModel };
        }


        public ActionResult OnFirstWeighing()
        {
            PeseeViewModel viewModel = new PeseeViewModel();

            try
            {
                //Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                //mViewport.Mask();

                viewModel._Pesee = new Pesee();

                viewModel.IsFirstWeighing = true;

                viewModel.IsScaleWeighing = true;                

                viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

                // Ouvrir les zones de saisies des poids
                viewModel.IsDevelopmentEnvironment = false;

                // entrer le numero de la livraison dans la zone prévue à cet effet
                viewModel.IsCallByPendingWindows = false;

                string userName = (String)Session["userName"];
                Bascule mClass = GetCurrentScale(userName);

                if (mClass.ID == 0)
                    throw new Exception("The scale is not set for this machine.");

                viewModel._Bascule = mClass;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }


            return new Ext.Net.MVC.PartialViewResult { ViewName = "Pesee_FirstWeighingDetail", Model = viewModel };
        }
        
        public ActionResult OnSecondWeighing(string ItemSelected)
        {
            Pesee mPesee = JSON.Deserialize<Pesee>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            PeseeViewModel viewModel = new PeseeViewModel();
            Parametres mParam = new Parametres(0);
            mPesee.fnGet(mPesee.ID);

            mPesee.Livraison.fnSelectByNumber(mPesee.Livraison.Numero);

            SacType mSacType = new SacType();
            mSacType.fnGet(mPesee.Livraison.SacType.ID);
            if (mSacType.ID == 0)
            {               
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing ",
                    Message = "Please check your delivery !",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            viewModel.SacTypeID = mSacType.Tare.ToString();
            mSacType = new SacType();
            mSacType.fnGet(mParam.SacNylonTypeID);
            viewModel.SacNylonTare = mSacType.Tare.ToString();
            try
            {
                //Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                //mViewport.Mask();

                viewModel._Pesee = mPesee;

                viewModel.IsFirstWeighing = false;

                viewModel.IsScaleWeighing = true;

                // Added by AA on 21st of September 2017
                LivraisonType mTypeLivraison = new LivraisonType();

                mTypeLivraison.fnGet(mPesee.Livraison.LivraisonType.ID);

                viewModel.WeighingWithVGM = mTypeLivraison.CalculeVGM;

                viewModel.AutorizePurchase = mTypeLivraison.EstAchat;
                
                //  check if the analysis of this weighing  has been rejected
                viewModel.IsRejected = mPesee.fnIsRejected();
                
                // Ouvrir les zones de saisies des poids
                viewModel.IsDevelopmentEnvironment = false;

                // entrer le numero de la livraison dans la zone prévue à cet effet
                viewModel.IsCallByPendingWindows = false;

                viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

                string userName = (String)Session["userName"];
                Bascule mClass = GetCurrentScale(userName);

                if (mClass.ID == 0)
                    throw new Exception("The scale is not set for this machine.");

                viewModel._Bascule = mClass;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }


            return new Ext.Net.MVC.PartialViewResult { ViewName = "Pesee_SecondWeighingDetail", Model = viewModel };

        }


        public ActionResult OnUnloadTruck(string ItemSelected)
        {
            Pesee mPesee = JSON.Deserialize<Pesee>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            PeseeViewModel viewModel = new PeseeViewModel();

            mPesee.fnGet(mPesee.ID);

            mPesee.Livraison.fnSelectByNumber(mPesee.Livraison.Numero);

            SacType mSacType = new SacType();
            mSacType.fnGet(mPesee.Livraison.SacType.ID);

            if (mSacType.ID == 0)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing ",
                    Message = "Please check your delivery !",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            viewModel.SacTypeID = mSacType.Tare.ToString();

            try
            {
                Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                mViewport.Mask();

                viewModel._Pesee = mPesee;

                viewModel.IsFirstWeighing = false;

                viewModel.IsScaleWeighing = true;

                // Added by AA on 21st of September 2017
                LivraisonType mTypeLivraison = new LivraisonType();

                mTypeLivraison.fnGet(mPesee.Livraison.LivraisonType.ID);

                viewModel.WeighingWithVGM = mTypeLivraison.CalculeVGM;

                // Ouvrir les zones de saisies des poids
                viewModel.IsDevelopmentEnvironment = false;

                // entrer le numero de la livraison dans la zone prévue à cet effet
                viewModel.IsCallByPendingWindows = false;

                viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

                string userName = (String)Session["userName"];
                Bascule mClass = GetCurrentScale(userName);

                if (mClass.ID == 0)
                    throw new Exception("The scale is not set for this machine.");

                viewModel._Bascule = mClass;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }


            return new Ext.Net.MVC.PartialViewResult { ViewName = "Pesee_DechargementDetail", Model = viewModel };

        }

        public ActionResult OnCancel(string ItemSelected)
        {

            try
            {
                Pesee mClass = JSON.Deserialize<Pesee>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                Guid mId = mClass.ID;

                bool result = mClass.fnGet(mId);

                if (!result)
                    throw new Exception("OnCancel : Weighing loading failed.");

                if (!mClass.Cancelled)
                {
                    mClass.UtilisateurModification = (string)Session["userName"];

                    result = mClass.fnCancel();

                    mClass.Cancelled = true;

                    Store mstore = X.GetCmp<Store>("storeListe");

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
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }

        public ActionResult OnTestScale()
        {
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Mask();
            Bascule mClass = new Bascule();
            try
            {                

                string userName = (String)Session["userName"];
                mClass = GetCurrentScale(userName);

                if (mClass.ID == 0)
                    throw new Exception("The scale is not set for this machine.");                
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Pesee_TestScale", Model = mClass };
        }


        public ActionResult GetUnWeighingDeliveryByNumber(string ItemDeliveryNumber)
        {
            Livraison mClass = new Livraison();

            mClass.LivraisonType = new LivraisonType();
            
            mClass.Fournisseur = new Fournisseur();

            if(string.IsNullOrEmpty(ItemDeliveryNumber))
            {
                return this.Direct();
            }

            mClass.fnSelectUnWeighedByNumber(ItemDeliveryNumber);

            SacType mSacType = new SacType();
            
            if (mClass.ID != Guid.Empty)
            {
                mSacType.fnGet(mClass.SacType.ID);
                
                X.GetCmp<TextField>("txtSupplierNameFristW").Text = string.IsNullOrEmpty(mClass.Fournisseur.FournisseurNameAndCode) ? string.Empty : mClass.Fournisseur.FournisseurNameAndCode;
                X.GetCmp<TextField>("txtTypeOfBagsFirstW").Text = string.IsNullOrEmpty(mSacType.Designation) ? string.Empty : mSacType.Designation;
                X.GetCmp<TextField>("txtDeliveryTypeFirst").Text = string.IsNullOrEmpty(mClass.LivraisonType.Designation) ? string.Empty : mClass.LivraisonType.Designation;
                X.GetCmp<TextField>("txtDriverNameFirstW").Text = string.IsNullOrEmpty(mClass.Chauffeur) ? string.Empty : mClass.Chauffeur;
                X.GetCmp<TextField>("txtTruckIDFirstW").Text = string.IsNullOrEmpty(mClass.Immatriculation) ? string.Empty : mClass.Immatriculation;
                X.GetCmp<TextField>("txtEstimatedTonnageFirstW").Text = string.IsNullOrEmpty(mClass.PoidsDeclare.ToString("#")) ? string.Empty : mClass.PoidsDeclare.ToString("#");
                X.GetCmp<TextField>("txtNbrOfBagsFirstW").Text = string.IsNullOrEmpty(mClass.SacsDeclares.ToString("#")) ? string.Empty : mClass.SacsDeclares.ToString("#");
                //



                // Set Hidden values
                X.GetCmp<TextField>("hiddenLivraisonID").Value = mClass.ID;
                X.GetCmp<TextField>("hiddenDestinationID").Value = mClass.Destination.ID;
                X.GetCmp<TextField>("hiddenCampagne").Value = mClass.Campagne.Designation;
                X.GetCmp<TextField>("hiddenLivraisonTypeID").Value = mClass.LivraisonType.ID;


                // show / hide Poids VGM according to type de livraison
                LivraisonType mLivraisonType = new LivraisonType();
                mLivraisonType.fnGet(mClass.LivraisonType.ID);


                // Added by AA on 30th Of Sept. 17
                X.GetCmp<TextField>("hiddenCalculateVgm").Value = mLivraisonType.CalculeVGM;  
                                               

                if (mSacType.ID == 0)
                {
                    CleanPeseeHeader();

                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Weighing ",
                        Message = "Please check your delivery !",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                }
                X.GetCmp<TextField>("hiddenTareSacsLivraison").Value = mSacType.Tare;
            }
            else
            {
                CleanPeseeHeader();

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing ",
                    Message = "This delivery is not available for the weighing !",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            //OnRefresh(mClass.ID.ToString());
            return this.Direct(); ;
        }
             
        private static void CleanPeseeHeader()
        {
            X.GetCmp<TextField>("txtSupplierName").Text = string.Empty;
            X.GetCmp<TextField>("txtTypeOfBags").Text = string.Empty;
            X.GetCmp<TextField>("txtDeliveryType").Text = string.Empty;
            X.GetCmp<TextField>("txtDriverName").Text = string.Empty;
            X.GetCmp<TextField>("txtTruckID").Text = string.Empty;
            X.GetCmp<TextField>("txtEstimatedTonnage").Text = string.Empty;
            X.GetCmp<TextField>("txtNbrOfBags").Text = string.Empty;

            // Set Hidden values
            X.GetCmp<TextField>("hiddenLivraisonID").Value = string.Empty;
            X.GetCmp<TextField>("hiddenDestinationID").Value = string.Empty;
            X.GetCmp<TextField>("hiddenCampagne").Value = string.Empty;
            X.GetCmp<TextField>("hiddenLivraisonTypeID").Value = string.Empty;
            X.GetCmp<TextField>("hiddenTareSacsLivraison").Value = string.Empty;
        }

        public ActionResult OnManualWeighing()
        {
            PeseeViewModel viewModel = new PeseeViewModel();

            try
            {
               Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                mViewport.Mask();

                viewModel._Pesee = new Pesee();

                viewModel.IsFirstWeighing = true;

                viewModel.IsScaleWeighing = false;

                viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

                viewModel.Title = "Manual weighing : Nouveau";
                Parametres mParam = new Parametres(0);
                SacType _sacType = new SacType();
                _sacType.fnGet(mParam.SacNylonTypeID);

                if (_sacType.ID == 0)
                    throw new Exception("The Type de Sacs is not found.");
                viewModel.SacNylonTare = _sacType.Tare.ToString();

                viewModel.WeighingWithVGM = true;

                string userName = (String)Session["userName"];
                Bascule mClass = GetCurrentScale(userName);

                if (mClass.ID == 0)
                    throw new Exception("The scale is not set for this machine.");

                viewModel._Bascule = mClass;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }


            return new Ext.Net.MVC.PartialViewResult { ViewName = "Pesee_ManualDetail", Model = viewModel };
        }

        [HttpPost]
        public ActionResult SubmitFormManualMethod()
        {
            try
            {
                Pesee mClass = new Pesee();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                mClass = MapFormToObject(mClass);

                bool result = mClass.fnManualWeighing();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListe");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionListe").Select(0);

                        LivraisonType _livraisonType = new LivraisonType(mClass.Livraison.LivraisonType.ID);
                        
                        string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                        if (_livraisonType != null)
                            X.Js.Call("GenerateTicket", mClass.ID, _livraisonType.EstAchat, BaseUrl);
                        else
                            X.Js.Call("GenerateTicket", mClass.ID, false, BaseUrl);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();

                        DeselectGridRows();
                    }
                    CloseDetailWindow();
                    X.Js.Call("Overview.resetButtons");
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            return this.Direct();
        }


        [HttpPost]
        public ActionResult SubmitFormFirstMethod()
        {
            try
            {
                Pesee mClass = new Pesee();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenPeseeExecMode").Value);

                mClass = MapFormToObject(mClass);

                bool result = mClass.fnFirstWeighing();
                
                if (result)
                {
                    Store mStore = new Store();
                    mStore = X.GetCmp<Store>("storeListe");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();

                        DeselectGridRows();
                    }

                    CloseDetailWindow();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }

        [HttpPost]
        public ActionResult OnSelectPendingDelivery(string id_pendingDelivery)
        {
            try
            {
                
                string UserName = (string)Session["userName"];
                Fonction HasAccess = new Fonction();
                bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{03B3FF73-95F5-4AFD-91D3-F9F8B06DC2ED}", UserName);
                if (HavPermissionOverview)
                {
                    Livraison mDelivery = new Livraison();

                    Guid mDeliveryGuid = Guid.Parse(id_pendingDelivery);

                    mDelivery.fnGet(mDeliveryGuid);

                    if (mDelivery.ID == Guid.Empty)
                    {
                        X.MessageBox.Alert("Error", "OnSelectPendingDelivery : Delivery selected is invalid.").Show();
                        return this.Direct();
                    }

                    PeseeViewModel viewModel = new PeseeViewModel();                   

                    viewModel._Pesee = new Pesee();

                    viewModel._Pesee.Livraison = mDelivery;

                    viewModel.IsFirstWeighing = true;

                    viewModel.IsScaleWeighing = true;

                    viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

                    // entrer le numero de la livraison dans la zone prévue à cet effet
                    viewModel.IsCallByPendingWindows = true;

                    // Ouvrir les zones de saisies des poids
                    viewModel.IsDevelopmentEnvironment = false;

                    string userName = (String)Session["userName"];
                    Bascule mClass = GetCurrentScale(userName);

                    if (mClass.ID == 0)
                        throw new Exception("The scale is not set for this machine.");

                    viewModel._Bascule = mClass;

                    //CloseDetailPendingWindow();
                    X.GetCmp<Window>("frmPendingDeliveries").Close();

                    //Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");

                    //mViewport.Mask();

                    return new Ext.Net.MVC.PartialViewResult { ViewName = "Pesee_FirstWeighingDetail", Model = viewModel };
                }
                else
                {
                    CloseDetailPendingWindow();
                    return this.Direct();
                }             
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }
        
        [HttpPost]
        public ActionResult SubmitFormSecondMethod()
        {
            try
            {
                Pesee mClass = new Pesee();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenPeseeExecMode").Value);

                mClass = MapFormToObject(mClass);

                if (IsDataValid())
                {
                    bool result = mClass.fnSecondWeighing();

                    if (result)
                    {
                        Store mStore = X.GetCmp<Store>("storeListe");

                        ModelProxy mProxy;

                        if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                        {
                            mStore.Insert(0, mClass);
                        }
                        else
                        {
                            mProxy = mStore.GetById(mClass.ID);

                            mProxy.BeginEdit();

                            mProxy.Set(mClass);

                            mProxy.Commit();

                            mProxy.EndEdit();

                            DeselectGridRows();
                        }
                        string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                        X.Js.Call("GenerateTicket", mClass.ID, mClass.Livraison.LivraisonType.EstAchat, BaseUrl);


                    }

                    CloseDetailWindow();
                    X.Js.Call("Overview.resetButtons");
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }


        private bool IsDataValid()
        {
            // if the delivery is rejected check that first and the second weight are the same
            bool isWeighingRejected = GetRejectStatus();

            if (isWeighingRejected)
            {
                decimal PoidsP1 = Decimal.Parse(GetFormValue("txtFirstGrossWeight"));

                decimal PoidsP2 = Decimal.Parse(GetFormValue("txtSecondGrossWeight"));

                bool isValid = PoidsP1 == PoidsP2;

                if (!isValid)
                    throw new Exception("First and second weight must be the same.");

                return isValid;
            }


            return true;

        }

        private bool GetRejectStatus()
        {
            return X.GetCmp<Checkbox>("chkIsRejected").Checked;
        }

        [HttpPost]
        public ActionResult SubmitFormUnloadingMethod()
        {
            try
            {
                Pesee mClass = new Pesee();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                mClass = MapFormToObjectForUnloading(mClass);

                bool result = mClass.fnUnload();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListe");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();

                        DeselectGridRows();
                    }



                    CloseDetailWindow();

                    //X.Js.Call("GenerateTicket", mClass.ID, mClass.Livraison.LivraisonType.EstAchat);
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }

        //SubmitFormChangeTypeMethod

        [HttpPost]
        public ActionResult SubmitFormChangeTypeMethod()
        {
            try
            {
                Pesee mClass = new Pesee();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                //mClass = MapFormToObject(mClass);


                // Data validation
                if(GetFormValue("txtReasonChangeType").Equals(string.Empty) || X.GetCmp<ComboBox>("cmbNewDetDeliveryType").SelectedItem.Text.Equals(string.Empty))
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Weighing ",
                        Message = "Please check your entries !",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }

                Guid mPeseeGuid = Guid.Parse(X.GetCmp<Hidden>("hiddenID").Value.ToString());
                int  mLivraisonTypeID = int.Parse(X.GetCmp<Hidden>("hiddenLivraisonTypeID").Value.ToString());

                mClass.fnGet(mPeseeGuid);

                LivraisonType mClassLivraisonType = new LivraisonType();

                mClassLivraisonType.fnGet(mLivraisonTypeID);

                mClassLivraisonType.ID = int.Parse(GetFormValue("cmbNewDetDeliveryType"));

                mClassLivraisonType.Designation = X.GetCmp<ComboBox>("cmbNewDetDeliveryType").SelectedItem.Text;

                mClass.Livraison.LivraisonType = mClassLivraisonType;

                mClass.RaisonChangementType = GetFormValue("txtReasonChangeType");

                mClass.UtilisateurCreation = (string)Session["userName"];

                mClass.UtilisateurModification = (string)Session["userName"];



                bool result = mClass.fnChangeDeliveryType();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListe");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();

                        DeselectGridRows();
                    }



                    CloseDetailWindow();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }



        [HttpPost]
        public ActionResult SubmitFormCorrectFirstWeightMethod()
        {
            try
            {
                Pesee mClass = new Pesee();
                
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                //mClass = MapFormToObject(mClass);

                if (GetFormValue("txtReason").Equals(string.Empty))
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Weighing ",
                        Message = "Please check your entries !",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }

                Guid mPeseeGuid = Guid.Parse(X.GetCmp<Hidden>("hiddenID").Value.ToString());
                int mLivraisonTypeID = int.Parse(X.GetCmp<Hidden>("hiddenLivraisonTypeID").Value.ToString());

                mClass.fnGet(mPeseeGuid);

                mClass.PoidsP1 = decimal.Parse(GetFormValue("txtFirstWeight"));             

                mClass.RaisonCorrectionPremierePoids = GetFormValue("txtReason");

                mClass.UtilisateurCreation = (string)Session["userName"];

                mClass.UtilisateurModification = (string)Session["userName"];
                

                bool result = mClass.fnCorrectFirstWeight();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListe");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();

                        DeselectGridRows();
                    }



                    CloseDetailWindow();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }



        public ActionResult SubmitFormCorrectSecondWeightMethod()
        {
            try
            {
                Pesee mClass = new Pesee();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                Guid mPeseeGuid = Guid.Parse(X.GetCmp<Hidden>("hiddenID").Value.ToString());

                mClass.fnGet(mPeseeGuid);

                mClass.PoidsP1 = decimal.Parse(GetFormValue("txtFirstWeight"));

                mClass.TareEmballages = int.Parse(GetFormValue("txtTareOfBags"));

                mClass.TarePalettes = int.Parse(GetFormValue("txtTareOfPallets"));

                mClass.PoidsNetLivre = decimal.Parse(GetFormValue("txtDeliveryNetWeight"));

                mClass.PoidsP2  = decimal.Parse(GetFormValue("txtSecondGrossWeight"));

                mClass.PoidsBrut = Math.Abs(mClass.PoidsP1 - mClass.PoidsP2);

                bool IsVGM = bool.Parse(GetFormValue("hiddenCalculateVgm"));
                if(IsVGM)
                {
                    mClass.TareConteneur = decimal.Parse(GetFormValue("txtTareContainer"));

                    mClass.PoidsVGM = decimal.Parse(GetFormValue("txtWeighingVgm"));
                }
                else
                {
                    mClass.TareConteneur = null;

                    mClass.PoidsVGM = null;
                }

                mClass.RaisonCorrectionDeuxiemePoids = GetFormValue("txtReason");

                mClass.UtilisateurCreation = (string)Session["userName"];

                mClass.UtilisateurModification = (string)Session["userName"];


                bool result = mClass.fnCorrectSecondWeight();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListe");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();

                        DeselectGridRows();
                    }



                    CloseDetailWindow();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }







        [HttpPost]
        public ActionResult SubmitFormFirstWeightMethod()
        {
            try
            {
                Pesee mClass = new Pesee();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);              

                Guid mPeseeGuid = Guid.Parse(X.GetCmp<Hidden>("hiddenID").Value.ToString());

                int mLivraisonTypeID = int.Parse(X.GetCmp<Hidden>("hiddenLivraisonTypeID").Value.ToString());

                mClass.fnGet(mPeseeGuid);

                LivraisonType mClassLivraisonType = new LivraisonType();

                mClassLivraisonType.fnGet(mLivraisonTypeID);

                mClassLivraisonType.ID = int.Parse(GetFormValue("cmbNewDetDeliveryType"));

                mClassLivraisonType.Designation = X.GetCmp<Hidden>("cmbNewDetDeliveryType").Text;


                mClass.Livraison.LivraisonType = mClassLivraisonType;

                mClass.RaisonChangementType = GetFormValue("txtReasonChangeType");

                mClass.UtilisateurCreation = (string)Session["userName"];

                mClass.UtilisateurModification = (string)Session["userName"];



                bool result = mClass.fnChangeDeliveryType();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListe");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowSelectionListe").Select(0);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();

                        DeselectGridRows();
                    }



                    CloseDetailWindow();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }


        [HttpPost]
        public ActionResult SubmitFormSecondWeightMethod()
        {
            try
            {
                Pesee mClass = new Pesee();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                //mClass = MapFormToObject(mClass);

                Guid mPeseeGuid = Guid.Parse(X.GetCmp<Hidden>("hiddenID").Value.ToString());
                int mLivraisonTypeID = int.Parse(X.GetCmp<Hidden>("hiddenLivraisonTypeID").Value.ToString());

                mClass.fnGet(mPeseeGuid);

                LivraisonType mClassLivraisonType = new LivraisonType();
                mClassLivraisonType.fnGet(mLivraisonTypeID);

                mClassLivraisonType.ID = int.Parse(GetFormValue("cmbNewDetDeliveryType"));
                mClassLivraisonType.Designation = X.GetCmp<Hidden>("cmbNewDetDeliveryType").Text;


                mClass.Livraison.LivraisonType = mClassLivraisonType;

                mClass.RaisonChangementType = GetFormValue("txtReasonChangeType");

                mClass.UtilisateurCreation = (string)Session["userName"];

                mClass.UtilisateurModification = (string)Session["userName"];



                bool result = mClass.fnChangeDeliveryType();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListe");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();

                        DeselectGridRows();
                    }



                    CloseDetailWindow();
                    
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

                      
            return this.Direct();
        }


        public ActionResult Select(StoreRequestParameters parameters, string ItemPontBasculeID, string ItemCropYearID, string ItemLivraisonID, string ItemFournisseurID, string ItemStartDate, string ItemEndDate, string ItemStatus, string ItemSite)
        {
            string userName = (string)Session["userName"];
            Bascule mClass = GetCurrentScale(userName);
            int pontBasculeID = mClass.ID;
            if (!string.IsNullOrEmpty(ItemPontBasculeID) && int.TryParse(ItemPontBasculeID, out pontBasculeID))
                pontBasculeID = int.Parse(ItemPontBasculeID);
                
            string cropYearID = "{Tous}";
            if (!string.IsNullOrEmpty(ItemCropYearID))
                cropYearID = ItemCropYearID;

            string livraisonID = "-1";
            if (!string.IsNullOrEmpty(ItemLivraisonID))
                livraisonID = ItemLivraisonID;

            int siteID = -1;
            if (!string.IsNullOrEmpty(ItemSite))
                siteID = int.Parse(ItemSite);

            int fournisseurID = -1;
            if (!string.IsNullOrEmpty(ItemFournisseurID) && int.TryParse(ItemFournisseurID, out fournisseurID))
                fournisseurID = int.Parse(ItemFournisseurID);

            DateTime mStartDate = DateTime.Now.AddDays(-30);
            if (!string.IsNullOrEmpty(ItemStartDate) && DateTime.TryParse(ItemStartDate, out mStartDate))
                mStartDate = DateTime.Parse(ItemStartDate);

            DateTime mEndDate = DateTime.Now;
            if (!string.IsNullOrEmpty(ItemEndDate) && DateTime.TryParse(ItemEndDate, out mEndDate))
                mEndDate = DateTime.Parse(ItemEndDate);

            short mStatus = -1;
            if (!string.IsNullOrEmpty(ItemStatus) && short.TryParse(ItemStatus, out mStatus))
                mStatus = short.Parse(ItemStatus);
                

            var mListe = (new Pesee()).fnSelect( pontBasculeID,  cropYearID,  livraisonID,  fournisseurID,  mStartDate,  mEndDate,  mStatus, siteID);            
           
            string filterHeaders = this.Request.Params["filterheader"];
            //return this.Store(GridStorePaging.SetRangePlants(parameters, mListe, filterHeaders));

            return this.Store(mListe);
        }
               
        public ActionResult OnRefresh(string ItemPontBasculeID, string ItemCropYearID, string ItemLivraisonID, string ItemFournisseurID, string ItemStartDate, string ItemEndDate, string ItemStatus, string ItemSite)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemStartDate, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemEndDate, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListe");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemPontBasculeID"   ,ItemPontBasculeID),
                                new Ext.Net.Parameter("ItemCropYearID"     ,ItemCropYearID),
                                new Ext.Net.Parameter("ItemLivraisonID"     ,ItemLivraisonID),
                                new Ext.Net.Parameter("ItemFournisseurID"           ,ItemFournisseurID),
                                new Ext.Net.Parameter("ItemStartDate"              ,ItemStartDate),
                                new Ext.Net.Parameter("ItemEndDate"     ,ItemEndDate),
                                new Ext.Net.Parameter("ItemStatus"           ,ItemStatus),
                                new Ext.Net.Parameter("ItemSite"           ,ItemSite)
                            });



                // collapse criterias areas
                X.GetCmp<FormPanel>("CriteriaPanel").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });                
            }
            

            return this.Direct();
        }


        public ActionResult OnRefreshPendingDeliveries( string ItemCropYearID, string ItemStartDate, string ItemEndDate)
        {
            Store mstore = X.GetCmp<Store>("storeListePendingDeliveries");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemCropYearID" ,ItemCropYearID),                              
                                new Ext.Net.Parameter("ItemStartDate"  ,ItemStartDate),
                                new Ext.Net.Parameter("ItemEndDate"    ,ItemEndDate)                               
                            });



            // collapse criterias areas
            //X.GetCmp<FormPanel>("CriteriaPanel").Collapse(Direction.Top, false);

            return this.Direct();
        }

        public ActionResult SelectPendingDeliveries(StoreRequestParameters parameters, string ItemCropYearID, string ItemStartDate, string ItemEndDate)
        {

            string cropYearID = "{Tous}";
            if (!string.IsNullOrEmpty(ItemCropYearID))
                cropYearID = ItemCropYearID;

            DateTime mStartDate = DateTime.Now.AddDays(-30);
            if (!string.IsNullOrEmpty(ItemStartDate) && DateTime.TryParse(ItemStartDate, out mStartDate))
                mStartDate = DateTime.Parse(ItemStartDate);

            DateTime mEndDate = DateTime.Now;
            if (!string.IsNullOrEmpty(ItemEndDate) && DateTime.TryParse(ItemEndDate, out mEndDate))
                mEndDate = DateTime.Parse(ItemEndDate);

            var mListe = (new Livraison()).fnSelectPendingDeliveries(cropYearID, mStartDate, mEndDate);

            // Paging
            int start = parameters.Start;

            int limit = parameters.Limit;

            if ((start + limit) > mListe.Count)
            {
                limit = mListe.Count - start;
            }

            List<DataPersist> rangePlants = (start < 0 || limit < 0) ? mListe : mListe.GetRange(start, limit);

            return this.Store(new Paging<DataPersist>(rangePlants, mListe.Count));
        }

        public ActionResult OnFilter()
        {
            X.GetCmp<FormPanel>("CriteriaPanel").ToggleCollapse();
            return this.Direct();
        }

        private void DeselectGridRows()
        {
            //X.Js.Call("App.rowSelectionListe.deselectAll();");
            X.GetCmp<RowSelectionModel>("rowSelectionListe").DeselectAll();
        }

        private Pesee MapFormToObject(Pesee mClass)
        {
            try
            {
                Parametres mParam = new Parametres(0);
                bool isManual = false;
                bool.TryParse(GetFormValue("hiddenIsManual"), out isManual);
                bool isFirstWeighing = false;
                bool.TryParse(GetFormValue("hiddenIsFirstWeighing"), out isFirstWeighing);

                Guid deliveryID;
                string campagne;
                int siteID;
                int livraisonTypeID;
                int pontBasculeID;

                Guid.TryParse(GetFormValue("hiddenLivraisonID"), out deliveryID);

                int.TryParse(GetFormValue("hiddenDestinationID"), out siteID);

                int.TryParse(GetFormValue("hiddenBasculeID"), out pontBasculeID);

                campagne = GetFormValue("hiddenCampagne");

                int.TryParse(GetFormValue("hiddenLivraisonTypeID"), out livraisonTypeID);

                mClass.Livraison = new Livraison();
                mClass.Livraison.LivraisonType = new LivraisonType();
                string DeliveryNumber = string.Empty;
                if (!string.IsNullOrEmpty(GetFormValue("txtDeliveryNumber")))
                    DeliveryNumber = GetFormValue("txtDeliveryNumber");
                else
                    DeliveryNumber = GetFormValue("txtDeliveryNumberFirstW");

                mClass.Livraison.fnSelectUnWeighedByNumber(DeliveryNumber);

                mClass.Bascule = new Bascule();

                mClass.Bascule.ID = pontBasculeID;

                mClass.Bascule.Designation = string.Empty;

                mClass.Statut = "DF";

                decimal mValue;

                bool canConvert;

                if (isManual)
                {
                    mClass.DateP1 = DateTime.Parse(GetFormValue("dtfFirstWeighingDate") + " " + GetFormValue("tmfFirstWeighing"));
                    mClass.PoidsP1 = Decimal.Parse(GetFormValue("txtFirstGrossWeight"));
                    mClass.DateP2 = DateTime.Parse(GetFormValue("dtfSecondWeighingDate") + " " + GetFormValue("tmfSecondWeighing"));
                    mClass.PoidsP2 = Decimal.Parse(GetFormValue("txtSecondGrossWeight"));
                    mClass.SacsDecharges = int.Parse(GetFormValue("txtUnloadledBags"));
                    mClass.SacsAcceptes = int.Parse(GetFormValue("txtAcceptedBags"));
                    mClass.TareEmballages = decimal.Parse(GetFormValue("txtTareOfBags"));
                    mClass.TarePalettes = decimal.Parse(GetFormValue("txtTareOfPallets"));
                    mClass.PoidsBrut = Math.Abs(mClass.PoidsP1 - mClass.PoidsP2);
                    mClass.PoidsNetLivre = mClass.PoidsBrut - mClass.TareEmballages - mClass.TarePalettes;

                    // Added new cols
                    mClass.SacsBons = int.Parse(GetFormValue("txtGoodBags"));
                    mClass.SacsMauvais = int.Parse(GetFormValue("txtBadBags"));
                    mClass.SacsNylon = int.Parse(GetFormValue("txtNylonBags"));
                    mClass.SacsOPA = int.Parse(GetFormValue("txtOPABags"));
                    mClass.NbrePalette = int.Parse(GetFormValue("txtNbrOfPalets"));
                    //mClass.PoidsVGM         = decimal.Parse(GetFormValue("txtWeighingVgm")); 

                    canConvert = decimal.TryParse(GetFormValue("txtWeighingVgm"), out mValue);
                    if (canConvert)
                        mClass.PoidsVGM = decimal.Parse(GetFormValue("txtWeighingVgm"));
                    else
                        mClass.PoidsVGM = null;

                    canConvert = decimal.TryParse(GetFormValue("txtTareConteneur"), out mValue);
                    if (canConvert)
                        mClass.TareConteneur = decimal.Parse(GetFormValue("txtTareConteneur"));
                    else
                        mClass.TareConteneur = null;

                    mClass.RaisonPeseeManuelle = GetFormValue("TxtRaisonPeseeManuelle");
                }
                else
                {
                    // Si en première pesée
                    mClass.DateP1 = DateTime.Parse(GetFormValue("dtfFirstWeighingDate") + " " + GetFormValue("tmfFirstWeighing"));

                    mClass.PoidsP1 = Decimal.Parse(GetFormValue("txtFirstGrossWeight"));

                    //Prevenir Changement depuis la console du navigateur
                    //if (isFirstWeighing && !isManual && (mClass.PoidsP1 != CapturedFirstWeight) && mParam.IsInDevelopment == false)
                    //{
                    //    bool toremoveaftertest = false;
                    //    throw new Exception("An error occured, please retry Weighing !");
                    //}

                    if (!isFirstWeighing)
                    {
                        // Sinon en 2e pesée    
                        mClass.ID = Guid.Parse(GetFormValue("hiddenPeseeID"));

                        mClass.fnGet(mClass.ID);

                        mClass.DateP2 = DateTime.Parse(GetFormValue("dtfSecondWeighingDate") + " " + GetFormValue("tmfSecondWeighing"));

                        mClass.PoidsP2 = Decimal.Parse(GetFormValue("txtSecondGrossWeight"));

                        if (!isFirstWeighing && !isManual && (mClass.PoidsP2 != CapturedSecondWeight) && mParam.IsInDevelopment == false)
                        {
                            bool toremoveaftertest = false;
                            throw new Exception("An error occured, please retry Weighing !");
                        }

                        mClass.SacsDecharges = int.Parse(GetFormValue("txtUnloadledBags"));

                        mClass.SacsAcceptes = int.Parse(GetFormValue("txtAcceptedBags"));

                        mClass.TareEmballages = decimal.Parse(GetFormValue("txtTareOfBags"));

                        mClass.TarePalettes = decimal.Parse(GetFormValue("txtTareOfPallets"));

                        mClass.PoidsBrut = Math.Abs(mClass.PoidsP1 - mClass.PoidsP2);

                        mClass.PoidsNetLivre = mClass.PoidsBrut - mClass.TareEmballages - mClass.TarePalettes;

                        // Added new cols
                        mClass.SacsBons = int.Parse(GetFormValue("txtGoodBags"));

                        mClass.SacsMauvais = int.Parse(GetFormValue("txtBadBags"));

                        mClass.SacsNylon = int.Parse(GetFormValue("txtNylonBags"));

                        mClass.SacsOPA = int.Parse(GetFormValue("txtOPABags"));

                        mClass.NbrePalette = int.Parse(GetFormValue("txtNbrOfPalets"));

                        canConvert = decimal.TryParse(GetFormValue("txtWeighingVgm"), out mValue);
                        if (canConvert)
                            mClass.PoidsVGM = decimal.Parse(GetFormValue("txtWeighingVgm"));
                        else
                            mClass.PoidsVGM = null;

                        canConvert = decimal.TryParse(GetFormValue("txtTareConteneur"), out mValue);
                        if (canConvert)
                            mClass.TareConteneur = decimal.Parse(GetFormValue("txtTareConteneur"));
                        else
                            mClass.TareConteneur = null;

                    }
                }

                mClass.UtilisateurCreation = (string)Session["userName"];

                mClass.UtilisateurModification = (string)Session["userName"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            return mClass;
        }



        private Pesee MapFormToObjectForUnloading(Pesee mClass)
        {
            bool isManual = false;
            bool.TryParse(GetFormValue("hiddenIsManual"), out isManual);
            bool isFirstWeighing = false;
            bool.TryParse(GetFormValue("hiddenIsFirstWeighing"), out isFirstWeighing);

            Guid deliveryID;
            string campagne;
            int siteID;
            int livraisonTypeID;
            int pontBasculeID;



            Guid.TryParse(GetFormValue("hiddenLivraisonID"), out deliveryID);

            int.TryParse(GetFormValue("hiddenDestinationID"), out siteID);

            int.TryParse(GetFormValue("hiddenBasculeID"), out pontBasculeID);

            campagne = GetFormValue("hiddenCampagne");

            int.TryParse(GetFormValue("hiddenLivraisonTypeID"), out livraisonTypeID);

            mClass.Livraison = new Livraison();
            mClass.Livraison.LivraisonType = new LivraisonType();

            mClass.Livraison.fnSelectUnWeighedByNumber(GetFormValue("hiddenDeliveryNumber"));

            mClass.Bascule = new Bascule();

            mClass.Bascule.ID = pontBasculeID;

            mClass.Bascule.Designation = string.Empty;

            mClass.Statut = "DF";

            decimal mValue;

            bool canConvert;

           
           
                // Si en première pesée
                //mClass.DateP1 = DateTime.Parse(GetFormValue("dtfFirstWeighingDate") + " " + GetFormValue("tmfFirstWeighing"));

                //mClass.PoidsP1 = Decimal.Parse(GetFormValue("txtFirstGrossWeight"));

               
                // Sinon en 2e pesée    
            mClass.ID = Guid.Parse(GetFormValue("hiddenID"));

            mClass.fnGet(mClass.ID);

            //mClass.DateP2 = DateTime.Parse(GetFormValue("dtfSecondWeighingDate") + " " + GetFormValue("tmfSecondWeighing"));

            //mClass.PoidsP2 = Decimal.Parse(GetFormValue("txtSecondGrossWeight"));

            mClass.SacsDecharges = int.Parse(GetFormValue("txtUnloadledBags"));

            mClass.SacsAcceptes = int.Parse(GetFormValue("txtAcceptedBags"));

            //mClass.TareEmballages = decimal.Parse(GetFormValue("txtTareOfBags"));

            //mClass.TarePalettes = decimal.Parse(GetFormValue("txtTareOfPallets"));

            //mClass.PoidsBrut = Math.Abs(mClass.PoidsP1 - mClass.PoidsP2);

            //mClass.PoidsNetLivre = mClass.PoidsBrut - mClass.TareEmballages - mClass.TarePalettes;

            // Added new cols
            mClass.SacsBons = int.Parse(GetFormValue("txtGoodBags"));

            mClass.SacsMauvais = int.Parse(GetFormValue("txtBadBags"));

            mClass.SacsNylon = int.Parse(GetFormValue("txtNylonBags"));

            mClass.SacsOPA = int.Parse(GetFormValue("txtOPABags"));

            mClass.NbrePalette = int.Parse(GetFormValue("txtNbrOfPalets"));

                //canConvert = decimal.TryParse(GetFormValue("txtWeighingVgm"), out mValue);
                //if (canConvert)
                //    mClass.PoidsVGM = decimal.Parse(GetFormValue("txtWeighingVgm"));
                //else
                //    mClass.PoidsVGM = null;

                //canConvert = decimal.TryParse(GetFormValue("txtTareConteneur"), out mValue);
                //if (canConvert)
                //    mClass.TareConteneur = decimal.Parse(GetFormValue("txtTareConteneur"));
                //else
                //    mClass.TareConteneur = null;


            mClass.UtilisateurCreation = (string)Session["userName"];

            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }


        private void CloseDetailWindow()
        {
            X.GetCmp<Window>("frmPesee").Close();
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Unmask();
        }

        private void CloseDetailPendingWindow()
        {
            X.GetCmp<Window>("frmPendingDeliveries").Close();
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Unmask();
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

        public ActionResult OnPrintWeighingTicket(string IdPesee, string IsAchat, string IsCopy)
        {
            bool ReportIsCopy = false;
            bool LivraisonIsAchat = bool.Parse(IsAchat);

            if (string.IsNullOrEmpty(IsCopy))
                ReportIsCopy = true;

            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));                        

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Ticket{0}', '{1}/Pesee/ViewWeighingTicket?id={0}&IsAchat={2}&IsCopy={3}', this, 'Ticket De pesée','')", IdPesee, BaseUrl, LivraisonIsAchat, ReportIsCopy));
        }

        public ActionResult OnPrintWeighingTicketNormal(string IdPesee)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));                       

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Ticket{0}', '{1}/Pesee/ViewWeighingTicket?TypeTicket=0&id={0}', this, 'Ticket','')", IdPesee, BaseUrl));
        }

        public ActionResult ViewWeighingTicket(string id, bool IsAchat, bool IsCopy)
        {
            try
            {
                XtraReport report = null;
                string ReportToPrint = string.Empty;
                if (IsAchat)
                {
                    report = new TicketPeseeCaisse() as XtraReport;
                    ReportToPrint = EnumReportDefinition.TICKETPESEECAISSE;
                }
                else
                {
                    report = new TicketPeseeNormal() as XtraReport;
                    ReportToPrint = EnumReportDefinition.TICKETPESEENORMAL;
                }

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["peseeID"].Value = id;
                report.Parameters["IsCopy"].Value = IsCopy;
                ReportParam param = new ReportParam();
                List<ReportParam> mListParam = new List<ReportParam>();
                param.Name = "peseeID";
                param.Value = id;
                mListParam.Add(param);
                param = new ReportParam();
                param.Name = "IsCopy";
                param.Value = IsCopy.ToString();
                mListParam.Add(param);

                string mParm = JSON.Serialize(mListParam);
                ViewData["Report"] = report;
                //string reportFilePath = @"C:\Temp\Report1.repx";
                //report.SaveLayoutToXml(reportFilePath);            
                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                //string ip = "192.168.8.104";
                // initialize services
                ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);

                proxy.DirectPrintTicket(mParm, ReportToPrint);
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
        private byte[] GetBuffer(XtraReport report)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                report.SaveLayout(stream);
                return stream.ToArray();
            }
        }
        public ActionResult OnDisplayListWeighingCriteria()
        {
            bool HasAccessToAllWeighBridges = true;

            PeseeViewModel viewModel = new PeseeViewModel();

            Bascule mClass = new Bascule();

            try
            {
                viewModel._Pesee = new Pesee();

                viewModel.HasAccessToAllWeighBridges = HasAccessToAllWeighBridges;

                viewModel.LoadBasculeMethod = viewModel.HasAccessToAllWeighBridges ? "LoadPontBasculeAll" : "LoadPontBascule";


            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Pesee : Index", ex.Message).Show();

                mClass = new Bascule();
                mClass.ID = 0;
                mClass.Designation = "";
            }

            string userName = (string)Session["userName"];
            mClass = GetCurrentScale(userName);

            viewModel._Bascule = mClass;

            viewModel.IsFirstWeighing = true;

            viewModel.IsScaleWeighing = true;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            string UserName = (string)Session["userName"];

            Site mSiteParDefaut = new Site();
            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;

            Parametres mParam = new Parametres(0);

            ViewData["SiteParDefaut"] = mSiteParDefaut.ID;
            if (mParam.Site == mSiteParDefaut.ID) ViewData["UrlSite"] = "LoadSiteAll";
            else ViewData["UrlSite"] = "LoadSiteByAccess";

            ViewData["Titre"] = "Print weighing";
            ViewData["actionToDo"] = "OnPrintWeighingList";
            ViewData["ControllerName"] = "Pesee";
            //Fournisseur mClass = new Fournisseur();
            //mClass.ID = -1;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmWeighingCriteria", Model = viewModel, ViewData = ViewData };            

        }

        public ActionResult OnPrintWeighingList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetStartDate").RawText.ToString(), "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(X.GetCmp<DateField>("dtfDetEndDate").RawText.ToString(), "d", CultureInfo.CurrentUICulture);

                Session["paramCropYear"] = GetFormValue("cmbDetCrop");

                Session["paramSite"] = Int32.Parse(GetFormValue("cmbDetSite"));
                Session["paramSiteText"] = X.GetCmp<ComboBox>("cmbDetSite").SelectedItem.Text.ToString();

                Session["paramDeliveryType"] = Int32.Parse(GetFormValue("cmbDetLivraisonType"));
                Session["paramDeliveryTypeText"] = X.GetCmp<ComboBox>("cmbDetLivraisonType").SelectedItem.Text.ToString();

                Session["paramPontBascule"] = Int32.Parse(GetFormValue("cmbBascule"));
                Session["paramPontBasculeText"] = X.GetCmp<ComboBox>("cmbBascule").SelectedItem.Text.ToString();

                Session["paramStatus"] = Int32.Parse(GetFormValue("cmbStatus"));
                Session["paramStatusText"] = X.GetCmp<ComboBox>("cmbStatus").SelectedItem.Text.ToString();

                Session["paramSupplier"] = Int32.Parse(GetFormValue("cmbDetSupplier"));
                Session["paramSupplierText"] = X.GetCmp<ComboBox>("cmbDetSupplier").SelectedItem.Text.ToString();

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Pesee/ViewReportResult', this, 'Liste des pesées',''),App.frmWeighingList.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing : Data Validation",
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

            report = new rptWeighingList() as XtraReport;

            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["campagneID"].Value = Session["paramCropYear"];

            report.Parameters["paramSite"].Value = Session["paramSite"];
            report.Parameters["paramSiteText"].Value = Session["paramSiteText"];

            report.Parameters["paramPontBascule"].Value = Session["paramPontBascule"];

            report.Parameters["paramPontBasculeText"].Value = Session["paramPontBasculeText"];

            report.Parameters["paramStatus"].Value = Session["paramStatus"];

            report.Parameters["paramStatusText"].Value = Session["paramStatusText"];

            report.Parameters["paramTypeLivraison"].Value = Session["paramDeliveryType"];

            report.Parameters["paramFournisseur"].Value = Session["paramSupplier"];

            report.Parameters["paramTypeLivraisonText"].Value = Session["paramDeliveryTypeText"];

            report.Parameters["paramFournisseurText"].Value = Session["paramSupplierText"];

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];

            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

        public partial class ReportParam
        {
            private string _name;
            private string _value;

            public string Name
            {
                get
                {
                    return _name;
                }

                set
                {
                    _name = value;
                }
            }

            public string Value
            {
                get
                {
                    return _value;
                }

                set
                {
                    _value = value;
                }
            }
            
        }
    }
}