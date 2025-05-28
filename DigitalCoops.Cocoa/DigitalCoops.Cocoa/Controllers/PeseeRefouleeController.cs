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

namespace Tms2017.MVC.Controllers
{
    public class PeseeRefouleeController : Controller
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";

        public PeseeRefouleeController()
        {
            //string cultureName = "en";
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
        }

        // GET: PeseeRefoulee
        public ActionResult Index()
        {
            try
            {

                bool HasAccessToAllWeighBridges = true;

                PeseeRefouleeViewModel viewModel = new PeseeRefouleeViewModel();

                viewModel._Pesee = new PeseeRefoulee();

                viewModel.HasAccessToAllWeighBridges = HasAccessToAllWeighBridges;

                viewModel.LoadBasculeMethod = viewModel.HasAccessToAllWeighBridges ? "LoadPontBasculeAll" : "LoadPontBascule";


                string userName = (String)Session["userName"];
                Bascule mClass = PeseeController.GetCurrentScale(userName);

                Site mSiteParDefaut = new Site();

                bool result = mSiteParDefaut.fnGetBySiteByUserName(userName);
                ViewBag.SiteParDefaut = mSiteParDefaut.ID;

                //if (HasAccessToAllWeighBridges)
                //{
                //    mClass.ID = -1;
                //    mClass.Designation = "{Tous}";
                //}

                viewModel._Bascule = mClass;

                viewModel.IsFirstWeighing = true;

                viewModel.IsScaleWeighing = true;

                viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

                viewModel.CriteriaTitle = String.Format("Site : {7}, Weighbridge = {0}  ,Campagne : {1} ,Type De Livraison = {2}  , Fournisseur : {3} , du {4}  to {5}  , Statut : {6}", mClass.Designation, "{Tous}", "{Tous}", "{Tous}", DateTime.Now.AddDays(-7).ToShortDateString(), DateTime.Now.ToShortDateString(), "{Tous}",mSiteParDefaut.Nom);

                #region Set Function's Access

                string UserName = (string)Session["userName"];
                Fonction HasAccess = new Fonction();
                //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{DA6C3602-0ECB-41BC-8727-6179D537AA47}", UserName);
                
                if (HasAccess.fnGetUserAccessStatus("{4872D9B6-FAB5-430F-9D9A-86E56F6A189B}", UserName) == false)
                    X.GetCmp<Button>("btnFirstWeight").Disable();
                else
                    X.GetCmp<Button>("btnFirstWeight").Enable();                    

                if (HasAccess.fnGetUserAccessStatus("{9EC9F2B2-E111-4655-A1A5-81F64B588035}", UserName) == false)
                    X.GetCmp<MenuItem>("mnuExportRefWeighing").Disable();
                else
                    X.GetCmp<MenuItem>("mnuExportRefWeighing").Enable();

                if (HasAccess.fnGetUserAccessStatus("{8413CA42-C9F4-47FB-A2E2-8CF0A26B471B}", UserName) == false)
                    X.GetCmp<MenuItem>("mnuConfigurePrinter").Disable();
                else
                    X.GetCmp<MenuItem>("mnuConfigurePrinter").Enable();

                if (HasAccess.fnGetUserAccessStatus("{E50C08F1-DE98-46D9-B2DA-2F8C15B75E2B}", UserName) == false)
                    X.GetCmp<MenuItem>("mnuManualWeighing").Disable();
                else
                    X.GetCmp<MenuItem>("mnuManualWeighing").Enable();

                if (HasAccess.fnGetUserAccessStatus("{086CC547-2321-4F01-9AD3-632CF00AD1AB}", UserName) == false)
                    X.GetCmp<MenuItem>("mnuPrintListWeighingRejection").Disable();
                else
                    X.GetCmp<MenuItem>("mnuPrintListWeighingRejection").Enable();

                X.GetCmp<Hidden>("RehiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{82325F87-FC24-4C3E-86D8-8F9B8ACAC88F}", UserName));
                X.GetCmp<Hidden>("RehiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{4822F8D3-ECCE-4871-A200-E00757E75600}", UserName));
                X.GetCmp<Hidden>("RehiddenPermFirstWeight").SetValue(HasAccess.fnGetUserAccessStatus("{4872D9B6-FAB5-430F-9D9A-86E56F6A189B}", UserName));
                X.GetCmp<Hidden>("RehiddenPermSecondWeight").SetValue(HasAccess.fnGetUserAccessStatus("{62FE115E-F1BD-483B-9277-0CC14E42C490}", UserName));
                X.GetCmp<Hidden>("RehiddenPermPrintWeighingTicket").SetValue(HasAccess.fnGetUserAccessStatus("{90C64DB3-EA2E-43D0-9FD2-C560CDBE09B6}", UserName));
                X.GetCmp<Hidden>("RehiddenPermManualWeighing").SetValue(HasAccess.fnGetUserAccessStatus("{E50C08F1-DE98-46D9-B2DA-2F8C15B75E2B}", UserName));
                X.GetCmp<Hidden>("RehiddenPermConfigurePrinter").SetValue(HasAccess.fnGetUserAccessStatus("{8413CA42-C9F4-47FB-A2E2-8CF0A26B471B}", UserName));
                X.GetCmp<Hidden>("RehiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{DA6C3602-0ECB-41BC-8727-6179D537AA47}", UserName));
                
                #endregion



                return View(viewModel);
            }
            catch (Exception ex)
            {
                return JavaScript(Ext.Net.X.Msg.Alert("PeseeRefoulee : Index", ex.Message).ToScript());
            }
        }

        public ActionResult OnCaptureFirstWeight()
        {
            try
            {
                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                //string ip = "198.168.8.101";
                // initialize services                
                ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);

                int sWeight = proxy.GetWeight();
                X.GetCmp<TextField>("txtFirstGrossWeight").Text = sWeight.ToString();
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
                string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                //string ip = "198.168.8.101";
                // initialize services                
                ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);

                int sWeight = proxy.GetWeight();
                X.GetCmp<TextField>("txtSecondGrossWeight").Text = sWeight.ToString();
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

            List<Bascule> mList = mClass.fnSelectBySite(new Parametres(0).Site);

            if (mList.Count <= 0)
                throw new Exception("Pesee : fnSelectBySite : No Weighbridges sets up for this site.");

            mClass.ID = mList[0].ID;

            mClass.Designation = mList[0].Designation;

            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;

            string machineName = "BTECH003";

            try
            {
                //ServiceLib.IService proxy = ServicesHelper.CreateClientServiceInstance(ip);

                //machineName = proxy.GetMachineName();

                mClass.fnGetByMachineName(machineName);
            }
            catch (Exception ex)
            {
                machineName = "BTECH003";

                mClass.fnGetByMachineName(machineName);

                //throw ex;
            }



            return mClass;
        }

        public ActionResult OnFirstWeighing()
        {
            PeseeRefouleeViewModel viewModel = new PeseeRefouleeViewModel();

            try
            {
                Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                mViewport.Mask();

                viewModel._Pesee = new PeseeRefoulee();

                viewModel.IsFirstWeighing = true;

                viewModel.IsDevelopmentEnvironment = false;

                viewModel.IsScaleWeighing = true;

                viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

                string userName = (String)Session["userName"];
                Bascule mClass = PeseeController.GetCurrentScale(userName);

                if (mClass.ID == 0)
                    throw new Exception("The scale is not set for this machine.");

                viewModel._Bascule = mClass;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "PeseeRefoulee_FirstWeighingDetail", Model = viewModel };
        }


        public ActionResult OnConsult(string ItemSelected)
        {
            PeseeRefoulee mPesee = JSON.Deserialize<PeseeRefoulee>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            PeseeRefouleeViewModel viewModel = new PeseeRefouleeViewModel();
            try
            {

                mPesee.fnGet(mPesee.ID);

                //mPesee.Livraison = new Livraison();
                //mPesee.Livraison.Numero = mPesee.Livraison.Numero;
                mPesee.Livraison.fnGet(mPesee.Livraison.ID);

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


                Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                mViewport.Mask();

                viewModel._Pesee = mPesee;

                viewModel.IsFirstWeighing = (mPesee.PoidsP1 != 0 && mPesee.PoidsP2 == 0); ;

                viewModel.IsScaleWeighing = true;

                viewModel.IsDevelopmentEnvironment = false;

                viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

                string userName = (String)Session["userName"];
                Bascule mClass = PeseeController.GetCurrentScale(userName);

                if (mClass.ID == 0)
                    throw new Exception("The scale is not set for this machine.");

                viewModel._Bascule = mClass;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
                return this.Direct();
            }


            return new Ext.Net.MVC.PartialViewResult { ViewName = "PeseeRefoulee_SecondWeighingDetail", Model = viewModel };
        }

        public ActionResult OnSecondWeighing(string ItemSelected)
        {
            PeseeRefoulee mPesee = JSON.Deserialize<PeseeRefoulee>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

            PeseeRefouleeViewModel viewModel = new PeseeRefouleeViewModel();
            try
            {

                mPesee.fnGet(mPesee.ID);

                //mPesee.Livraison = new Livraison();
                //mPesee.Livraison.Numero = mPesee.Livraison.Numero;
                mPesee.Livraison.fnGetByWeighingStatus(Pesee.StatutPesee.SecondePesee);

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

           
                Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                mViewport.Mask();

                viewModel._Pesee = mPesee;

                viewModel.IsFirstWeighing = false;

                viewModel.IsScaleWeighing = true;

                viewModel.IsDevelopmentEnvironment = false;

                viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

                string userName = (String)Session["userName"];
                Bascule mClass = PeseeController.GetCurrentScale(userName);

                if (mClass.ID == 0)
                    throw new Exception("The scale is not set for this machine.");

                viewModel._Bascule = mClass;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
                return this.Direct();
            }


            return new Ext.Net.MVC.PartialViewResult { ViewName = "PeseeRefoulee_SecondWeighingDetail", Model = viewModel };

        }


        public ActionResult OnManualWeighing()
        {
            PeseeRefouleeViewModel viewModel = new PeseeRefouleeViewModel();

            try
            {
                Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                mViewport.Mask();

                viewModel._Pesee = new PeseeRefoulee();

                viewModel.IsFirstWeighing = true;

                viewModel.IsScaleWeighing = false;

                viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

                string userName = (String)Session["userName"];
                Bascule mClass = PeseeController.GetCurrentScale(userName);

                if (mClass.ID == 0)
                    throw new Exception("The scale is not set for this machine.");

                viewModel._Bascule = mClass;
            }
            catch (Exception ex)
            {
                X.MessageBox.Alert("Error", ex.Message).Show();
            }


            return new Ext.Net.MVC.PartialViewResult { ViewName = "PeseeRefoulee_ManualDetail", Model = viewModel };
        }

        public ActionResult OnPrintWeighingTicket(string IdPesee, string IsCopy)
        {
            bool ReportIsCopy = false;
            //bool LivraisonIsAchat = bool.Parse(IsAchat);

            if (string.IsNullOrEmpty(IsCopy))
                ReportIsCopy = true;

            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Ticket{0}', '{1}/PeseeRefoulee/ViewWeighingTicket?id={0}&IsCopy={2}', this, 'Ticket De pesée','')", IdPesee, BaseUrl,  ReportIsCopy));
        }

        public ActionResult ViewWeighingTicket(string id,  bool IsCopy)
        {
            XtraReport report = null;

            report = new TicketPeseeRejectionCaisse() as XtraReport;

            //if (IsAchat)
            //    report = new TicketPeseeCaisse() as XtraReport;
            //else
            //    report = new TicketPeseeNormal() as XtraReport;

            report.DataSource = DevExpressReportDs.SetDataSource(report);
            report.Parameters["peseeID"].Value = id;
            report.Parameters["IsCopy"].Value = IsCopy;

            ViewData["Report"] = report;

            return View();
        }


        public ActionResult GetFirstWeighingByDeliveryNumber(string ItemDeliveryNumber)
        {
            try
            {
                Livraison mClass = new Livraison();

                mClass.LivraisonType = new LivraisonType();

                mClass.Fournisseur = new Fournisseur();

                mClass.Numero = ItemDeliveryNumber;
                
                mClass.fnGetByWeighingStatus(Pesee.StatutPesee.PremierePesee);

                if (mClass.ID == Guid.Empty)
                {
                    //CleanPeseeHeader();
                    //X.MessageBox.Show(new MessageBoxConfig
                    //{
                    //    Title = "Weighing ",
                    //    Message = "Please check your delivery !",
                    //    Buttons = MessageBox.Button.OK,
                    //    Icon = MessageBox.Icon.WARNING
                    //});
                    //return this.Direct();
                   throw new Exception("No delivery macthes to this number !");                  
                }

                //mClass.fnSelectUnWeighedByNumber(ItemDeliveryNumber);

                if (string.IsNullOrEmpty(ItemDeliveryNumber))
                {
                    return this.Direct();
                }

                SacType mSacType = new SacType();

                mSacType.fnGet(mClass.SacType.ID);

                Pesee mPesee = new Pesee();
                               
                mPesee.fnGetByDeliveryNumber(ItemDeliveryNumber);

                if (mClass.ID != Guid.Empty)
                {
                    X.GetCmp<TextField>("txtSupplierName").Text = string.IsNullOrEmpty(mClass.Fournisseur.FournisseurNameAndCode) ? string.Empty : mClass.Fournisseur.FournisseurNameAndCode;
                    X.GetCmp<TextField>("txtTypeOfBags").Text = string.IsNullOrEmpty(mSacType.Designation) ? string.Empty : mSacType.Designation;
                    X.GetCmp<TextField>("txtDeliveryType").Text = string.IsNullOrEmpty(mClass.LivraisonType.Designation) ? string.Empty : mClass.LivraisonType.Designation;
                    X.GetCmp<TextField>("txtDriverName").Text = string.IsNullOrEmpty(mClass.Chauffeur) ? string.Empty : mClass.Chauffeur;
                    X.GetCmp<TextField>("txtTruckID").Text = string.IsNullOrEmpty(mClass.Immatriculation) ? string.Empty : mClass.Immatriculation;
                    X.GetCmp<TextField>("txtEstimatedTonnage").Text = string.IsNullOrEmpty(mClass.PoidsDeclare.ToString("#")) ? string.Empty : mClass.PoidsDeclare.ToString("#");

                    // Set previous weighing
                    X.GetCmp<TextField>("txtNbrBagsKept").Text = string.IsNullOrEmpty(mPesee.SacsAcceptes.ToString()) ? string.Empty : mPesee.SacsAcceptes.ToString();
                    X.GetCmp<TextField>("txtPreviousGrossWeight").Text = string.IsNullOrEmpty(mPesee.PoidsBrut.ToString("#")) ? string.Empty : mPesee.PoidsBrut.ToString("#");


                    // Set Hidden values
                    X.GetCmp<TextField>("hiddenLivraisonID").Value = mClass.ID;
                    X.GetCmp<TextField>("hiddenDestinationID").Value = mClass.Site.ID;
                    X.GetCmp<TextField>("hiddenCampagne").Value = mClass.Campagne.Designation;
                    X.GetCmp<TextField>("hiddenLivraisonTypeID").Value = mClass.LivraisonType.ID;



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
                    //CleanPeseeHeader();

                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Weighing ",
                        Message = "No delivery macthes to this number !",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    //return this.Direct();
                }
            }
            catch (Exception ex)
            {
                //CleanPeseeHeader();
               
                X.MessageBox.Show
               (
                   new MessageBoxConfig
                   {
                       Title = "Weighing ",
                       Message = ex.Message,
                       Buttons = MessageBox.Button.OK,
                       Icon = MessageBox.Icon.ERROR
                   }
               );
                return this.Direct();
            }
            
            return this.Direct();
        }


        public ActionResult GetSecondWeighingByDeliveryNumber(string ItemDeliveryNumber)
        {
            try
            {
                Livraison mClass = new Livraison();

                mClass.LivraisonType = new LivraisonType();
                mClass.Fournisseur = new Fournisseur();
                mClass.Numero = ItemDeliveryNumber;
                mClass.fnGetByWeighingStatus(Pesee.StatutPesee.SecondePesee);
                //mClass.fnSelectUnWeighedByNumber(ItemDeliveryNumber);

                SacType mSacType = new SacType();
                mSacType.fnGet(mClass.SacType.ID);

                Pesee mPesee = new Pesee();
                mPesee.fnGetByDeliveryNumber(ItemDeliveryNumber);

                if (mClass.ID != Guid.Empty)
                {
                    X.GetCmp<TextField>("txtSupplierName").Text = string.IsNullOrEmpty(mClass.Fournisseur.FournisseurNameAndCode) ? string.Empty : mClass.Fournisseur.FournisseurNameAndCode;
                    X.GetCmp<TextField>("txtTypeOfBags").Text = string.IsNullOrEmpty(mSacType.Designation) ? string.Empty : mSacType.Designation;
                    X.GetCmp<TextField>("txtDeliveryType").Text = string.IsNullOrEmpty(mClass.LivraisonType.Designation) ? string.Empty : mClass.LivraisonType.Designation;
                    X.GetCmp<TextField>("txtDriverName").Text = string.IsNullOrEmpty(mClass.Chauffeur) ? string.Empty : mClass.Chauffeur;
                    X.GetCmp<TextField>("txtTruckID").Text = string.IsNullOrEmpty(mClass.Immatriculation) ? string.Empty : mClass.Immatriculation;
                    X.GetCmp<TextField>("txtEstimatedTonnage").Text = string.IsNullOrEmpty(mClass.PoidsDeclare.ToString("#")) ? string.Empty : mClass.PoidsDeclare.ToString("#");

                    // Set previous weighing
                    X.GetCmp<TextField>("txtNbrBagsKept").Text = string.IsNullOrEmpty(mPesee.SacsAcceptes.ToString()) ? string.Empty : mPesee.SacsAcceptes.ToString();
                    X.GetCmp<TextField>("txtPreviousGrossWeight").Text = string.IsNullOrEmpty(mPesee.PoidsBrut.ToString("#")) ? string.Empty : mPesee.PoidsBrut.ToString("#");


                    // Set Hidden values
                    X.GetCmp<TextField>("hiddenLivraisonID").Value = mClass.ID;
                    X.GetCmp<TextField>("hiddenDestinationID").Value = mClass.Site.ID;
                    X.GetCmp<TextField>("hiddenCampagne").Value = mClass.Campagne.Designation;
                    X.GetCmp<TextField>("hiddenLivraisonTypeID").Value = mClass.LivraisonType.ID;



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
                        Message = "No delivery macthes to this number !",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                }
            }
            catch (Exception ex)
            {
                CleanPeseeHeader();
                X.MessageBox.Alert("Error", ex.Message).Show();
            }
            //OnRefresh(mClass.ID.ToString());
            return this.Direct();
        }

        private static void CleanPeseeHeader()
        {
            X.GetCmp<TextField>("txtLocation").Text = string.Empty;
            X.GetCmp<TextField>("txtDeliveryType").Text = string.Empty;
            X.GetCmp<TextField>("txtDriverName").Text = string.Empty;
            X.GetCmp<TextField>("txtTruckID").Text = string.Empty;
            X.GetCmp<TextField>("txtEstimatedTonnage").Text = string.Empty;

            X.GetCmp<TextField>("txtNbrBagsKept").Text =  string.Empty ;
            X.GetCmp<TextField>("txtPreviousGrossWeight").Text = string.Empty;

            // Set Hidden values
            X.GetCmp<TextField>("hiddenLivraisonID").Value = string.Empty;
            X.GetCmp<TextField>("hiddenDestinationID").Value = string.Empty;
            X.GetCmp<TextField>("hiddenCampagne").Value = string.Empty;
            X.GetCmp<TextField>("hiddenLivraisonTypeID").Value = string.Empty;
            X.GetCmp<TextField>("hiddenTareSacsLivraison").Value = string.Empty;
        }


        [HttpPost]
        public ActionResult SubmitFormManualMethod()
        {
            try
            {
                PeseeRefoulee mClass = new PeseeRefoulee();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                mClass = MapFormToObject(mClass);

                // compare previous weight vs gross weight
                if (mClass.PreviousGrossWeight != mClass.PoidsBrut)
                {
                    throw new Exception("Previous and actual gross weight must be the same.");
                }

                bool result = mClass.fnManualWeighing();

                mClass.IsManual = true;

                mClass.Rejected = true;

               

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
                X.MessageBox.Show
                (
                    new MessageBoxConfig
                    {
                        Title = "Weighing ",
                        Message = ex.Message,
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.ERROR
                    }
                );

                
            }

            return this.Direct();
        }


        [HttpPost]
        public ActionResult SubmitFormFirstMethod()
        {
            try
            {
                PeseeRefoulee mClass = new PeseeRefoulee();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                mClass = MapFormToObject(mClass);

                bool result = mClass.fnFirstWeighing();

                mClass.Rejected = true;

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
                X.MessageBox.Show
               (
                   new MessageBoxConfig
                   {
                       Title = "Weighing ",
                       Message = ex.Message,
                       Buttons = MessageBox.Button.OK,
                       Icon = MessageBox.Icon.ERROR
                   }
               );
            }

            return this.Direct();
        }



        [HttpPost]
        public ActionResult SubmitFormSecondMethod()
        {
            try
            {
                PeseeRefoulee mClass = new PeseeRefoulee();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                mClass = MapFormToObject(mClass);

                // compare previous weight vs gross weight
                if (mClass.PreviousGrossWeight != mClass.PoidsBrut)
                {
                    throw new Exception("Previous and actual gross weight must be the same.");
                }

                bool result = mClass.fnSecondWeighing();

                mClass.Rejected = true;

               

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
                    X.Js.Call("GenerateTicket", mClass.ID, BaseUrl);                    
                }

                CloseDetailWindow();
            }
            catch (Exception ex)
            {
                X.MessageBox.Show
               (
                   new MessageBoxConfig
                   {
                       Title = "Weighing ",
                       Message = ex.Message,
                       Buttons = MessageBox.Button.OK,
                       Icon = MessageBox.Icon.ERROR
                   }
               );
            }

            return this.Direct();
        }



        public ActionResult Select(StoreRequestParameters parameters, string ItemPontBasculeID, string ItemCropYearID, string ItemLivraisonID, string ItemFournisseurID, string ItemStartDate, string ItemEndDate, string ItemStatus, string ItemSite)
        {
            string userName = (string)Session["userName"];
            Bascule mClass = PeseeController.GetCurrentScale(userName);
            int pontBasculeID = mClass.ID;
           
            if (!string.IsNullOrEmpty(ItemPontBasculeID) && int.TryParse(ItemPontBasculeID, out pontBasculeID))
                pontBasculeID = int.Parse(ItemPontBasculeID);

            string cropYearID = "{Tous}";
            if (!string.IsNullOrEmpty(ItemCropYearID))
                cropYearID = ItemCropYearID;

            string livraisonID = "-1";
            if (!string.IsNullOrEmpty(ItemLivraisonID))
                livraisonID = ItemLivraisonID;

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

            int siteID = -1;
            if (!string.IsNullOrEmpty(ItemSite))
                siteID = int.Parse(ItemSite);

            var mListe = (new PeseeRefoulee()).fnSelect(pontBasculeID, cropYearID, livraisonID, fournisseurID, mStartDate, mEndDate, mStatus,siteID);

            // Paging
            //int start = parameters.Start;

            //int limit = parameters.Limit;

            //if ((start + limit) > mListe.Count)
            //{
            //    limit = mListe.Count - start;
            //}

            //List<DataPersist> rangeListe = (start < 0 || limit < 0) ? mListe : mListe.GetRange(start, limit);

            //return this.Store(new Paging<DataPersist>(rangeListe, mListe.Count));

            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemPontBasculeID, string ItemCropYearID, string ItemLivraisonID, string ItemFournisseurID, string ItemStartDate, string ItemEndDate, string ItemStatus)
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
                                new Ext.Net.Parameter("ItemStatus"           ,ItemStatus)
                            });

                // collapse criterias areas
                X.GetCmp<FormPanel>("CriteriaPanel").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Weighing Rejection : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            X.GetCmp<FormPanel>("CriteriaPanel").ToggleCollapse();
            return this.Direct();
        }


        public ActionResult OnCancel(string ItemSelected)
        {

            try
            {
                PeseeRefoulee mClass = JSON.Deserialize<PeseeRefoulee>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

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


        private void DeselectGridRows()
        {
            //X.Js.Call("App.rowSelectionListe.deselectAll();");
            X.GetCmp<RowSelectionModel>("rowSelectionListe").DeselectAll();
        }

        private PeseeRefoulee MapFormToObject(PeseeRefoulee mClass)
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

            mClass.Livraison.Numero = GetFormValue("txtDeliveryNumber");
           
            mClass.Bascule = new Bascule();
            mClass.Bascule.ID = pontBasculeID;
            mClass.Bascule.Designation = string.Empty;

            mClass.Statut = "DF";



            if (isManual)
            {
                mClass.Livraison.fnGetByWeighingStatus(Pesee.StatutPesee.PremierePesee);
                mClass.DateP1 = DateTime.Parse(GetFormValue("dtfFirstWeighingDate") + " " + GetFormValue("tmfFirstWeighing"));
                mClass.PoidsP1 = Decimal.Parse(GetFormValue("txtFirstGrossWeight"));
                mClass.DateP2 = DateTime.Parse(GetFormValue("dtfSecondWeighingDate") + " " + GetFormValue("tmfSecondWeighing"));
                mClass.PoidsP2 = Decimal.Parse(GetFormValue("txtSecondGrossWeight"));
                mClass.SacsDecharges = int.Parse(GetFormValue("txtLoadedBags"));
                mClass.SacsAcceptes = int.Parse(GetFormValue("txtNbrBagsKept"));
                //mClass.TareEmballages = decimal.Parse(GetFormValue("txtTareOfBags"));
                //mClass.TarePalettes = decimal.Parse(GetFormValue("txtTareOfPallets"));
                mClass.PoidsBrut = Decimal.Parse(GetFormValue("txtDeliveryGrossWeight")); //Math.Abs(mClass.PoidsP1 - mClass.PoidsP2);
                //mClass.PoidsNetLivre = mClass.PoidsBrut - mClass.TareEmballages - mClass.TarePalettes;
                mClass.PreviousGrossWeight = decimal.Parse(GetFormValue("txtPreviousGrossWeight"));

            }
            else
            {
                if (!isFirstWeighing)
                    mClass.Livraison.fnGetByWeighingStatus(Pesee.StatutPesee.SecondePesee);
                else
                    mClass.Livraison.fnGetByWeighingStatus(Pesee.StatutPesee.PremierePesee);

                // Si en première pesée               
                mClass.DateP1 = DateTime.Parse(GetFormValue("dtfFirstWeighingDate") + " " + GetFormValue("tmfFirstWeighing"));
                mClass.PoidsP1 = Decimal.Parse(GetFormValue("txtFirstGrossWeight"));

                mClass.PreviousGrossWeight = decimal.Parse(GetFormValue("txtPreviousGrossWeight"));
                mClass.SacsAcceptes = int.Parse(GetFormValue("txtNbrBagsKept"));

                if (!isFirstWeighing)
                {
                    // Sinon en 2e pesée    
                   
                    mClass.ID = Guid.Parse(GetFormValue("hiddenID"));
                    mClass.fnGet(mClass.ID);
                    mClass.DateP2 = DateTime.Parse(GetFormValue("dtfSecondWeighingDate") + " " + GetFormValue("tmfSecondWeighing"));
                    mClass.PoidsP2 = Decimal.Parse(GetFormValue("txtSecondGrossWeight"));
                    mClass.SacsDecharges = int.Parse(GetFormValue("txtLoadedBags"));
                   // mClass.SacsAcceptes = int.Parse(GetFormValue("txtAcceptedBags"));
                    //mClass.TareEmballages = decimal.Parse(GetFormValue("txtTareOfBags"));
                    //mClass.TarePalettes = decimal.Parse(GetFormValue("txtTareOfPallets"));
                    mClass.PoidsBrut = Decimal.Parse(GetFormValue("txtDeliveryGrossWeight"));//Math.Abs(mClass.PoidsP1 - mClass.PoidsP2);
                    //mClass.PoidsNetLivre = mClass.PoidsBrut - mClass.TareEmballages - mClass.TarePalettes;
                }
            }


            mClass.UtilisateurCreation =  (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }

        private void CloseDetailWindow()
        {
            X.GetCmp<Window>("frmPesee").Hide();
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

        public ActionResult OnDisplayListWeighingRejCriteria()
        {
            bool HasAccessToAllWeighBridges = true;

            PeseeViewModel viewModel = new PeseeViewModel();

            viewModel._Pesee = new PeseeRefoulee();

            viewModel.HasAccessToAllWeighBridges = HasAccessToAllWeighBridges;

            viewModel.LoadBasculeMethod = viewModel.HasAccessToAllWeighBridges ? "LoadPontBasculeAll" : "LoadPontBascule";


            string userName = (String)Session["userName"];
            Bascule mClass = PeseeController.GetCurrentScale(userName);

            //if (HasAccessToAllWeighBridges)
            //{
            //    mClass.ID = -1;
            //    mClass.Designation = "{Tous}";
            //}

            viewModel._Bascule = mClass;

            viewModel.IsFirstWeighing = true;

            viewModel.IsScaleWeighing = true;

            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;


            ViewData["Titre"] = "Print weighings - Rejection";
            ViewData["actionToDo"] = "OnPrintWeighingRejectionList";
            ViewData["ControllerName"] = "PeseeRefoulee";
            //Fournisseur mClass = new Fournisseur();
            //mClass.ID = -1;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmWeighingCriteria", Model = viewModel, ViewData = ViewData };

        }

        public ActionResult OnPrintWeighingRejectionList(string cropYear, string pontbascule, string deliveryType, string supplier, string startDate, string endDate, string supplierName, string deliveryTypeDesignation, string pontbasculeText, string ReportStatus, string ReportStatusText)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                Session["paramCropYear"] = cropYear;
                Session["paramDeliveryType"] = Int32.Parse(deliveryType);
                Session["paramDeliveryTypeText"] = deliveryTypeDesignation;

                Session["paramPontBascule"] = Int32.Parse(pontbascule);
                Session["paramPontBasculeText"] = pontbasculeText;

                Session["paramStatus"] = Int32.Parse(ReportStatus);
                Session["paramStatusText"] = ReportStatusText;

                Session["paramSupplier"] = Int32.Parse(supplier);
                Session["paramSupplierText"] = supplierName;

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/PeseeRefoulee/ViewReportResult', this, 'List Of Weighings - Rejection',''),App.frmWeighingList.doClose()", Guid.NewGuid(), BaseUrl));
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

            report = new rptRejectedWeighingList() as XtraReport;

            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["campagneID"].Value = Session["paramCropYear"];

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

    }
}