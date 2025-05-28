using DevExpress.DataAccess.Sql;
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
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{   
    public class DeliveryCodingController : Controller
    {
        // GET: DeliveryCoding
        public ActionResult Index()
        {
            //X.GetCmp<FormPanel>("CriteriaPanelDeliveryCode").Title = "";
            X.GetCmp<FormPanel>("CriteriaPanelDeliveryCode").SetTitle("Today");

            #region Set Function's Access

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{29513F87-134C-49C2-8D70-B5735C8CBB78}", UserName);
            if (HasAccess.fnGetUserAccessStatus("{5FFEC10E-74CF-48A5-ADD3-599FA9C5AC54}", UserName) == false)
                X.GetCmp<Button>("btnNewCode").Disable();
            else
                X.GetCmp<Button>("btnNewCode").Enable();

            if (HasAccess.fnGetUserAccessStatus("{5FD14C19-87F5-49FF-986F-C0C32CA932BB}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportListCode").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportListCode").Enable();

            if (HasAccess.fnGetUserAccessStatus("{EBD4D6E4-D38E-4247-BC7C-19EED519CDC4}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintDeliveryCodingList").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintDeliveryCodingList").Enable();


            X.GetCmp<Hidden>("CdhiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{5FFEC10E-74CF-48A5-ADD3-599FA9C5AC54}", UserName));
            X.GetCmp<Hidden>("CdhiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{15F62E8A-86D7-4259-A2DE-F36BFC172D5C}", UserName));
            //X.GetCmp<Hidden>("CdhiddenPermRemove").SetValue(HasAccess.fnGetUserAccessStatus("{3C067A90-AF15-4170-A253-2BB39575591E}", UserName));
            X.GetCmp<Hidden>("CdhiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{4881B9B3-9362-4CFD-8328-5124571B25BB}", UserName));
            //X.GetCmp<Hidden>("CdhiddenPermPrintCode").SetValue(HasAccess.fnGetUserAccessStatus("{9A7624A9-AFAD-4EB0-9799-BFC0C86365D7}", UserName));                
            X.GetCmp<Hidden>("CdhiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("5FD14C19-87F5-49FF-986F-C0C32CA932BB", UserName));
            X.GetCmp<Hidden>("CdhiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{29513F87-134C-49C2-8D70-B5735C8CBB78}", UserName));

            #endregion

            return View();
        }

        public ActionResult OnAdd()
        {
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Mask();
            LivraisonViewModel mclass = new LivraisonViewModel();

            mclass._Livraison = new Livraison();
            mclass._Parametres = new Parametres();
            mclass._Parametres.fnGet();
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            bool HavPermissionRemove = HasAccess.fnGetUserAccessStatus("{3C067A90-AF15-4170-A253-2BB39575591E}", UserName);
            bool HavPermissionPrint = HasAccess.fnGetUserAccessStatus("{933ECBC8-7E9F-4126-BD1D-3307A4CB4824}", UserName);

            ViewData["HavPermissionPrint"] = HavPermissionPrint;
            ViewData["HavPermissionRemove"] = HavPermissionRemove;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDeliveryCoding", Model = mclass, ViewData = ViewData };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            string UserName = (string)Session["userName"];
            Fonction HasAccess = new Fonction();
            bool HavPermissionRemove = HasAccess.fnGetUserAccessStatus("{3C067A90-AF15-4170-A253-2BB39575591E}", UserName);
            bool HavPermissionPrint = HasAccess.fnGetUserAccessStatus("{933ECBC8-7E9F-4126-BD1D-3307A4CB4824}", UserName);

            ViewData["HavPermissionPrint"] = HavPermissionPrint;
            ViewData["HavPermissionRemove"] = HavPermissionRemove;

            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Mask();

            Livraison mclass = JSON.Deserialize<Livraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });                   

            LivraisonViewModel viewModel = new LivraisonViewModel();

            viewModel._Livraison = mclass;
            viewModel._Parametres = new Parametres();
            viewModel._Parametres.fnGet();
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDeliveryCoding", Model = viewModel, ViewData = ViewData };
        }

        public ActionResult OnConsult(string ItemSelected)
        {
            Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
            mViewport.Mask();

            Livraison mclass = JSON.Deserialize<Livraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            LivraisonViewModel viewModel = new LivraisonViewModel();

            viewModel._Livraison = mclass;
            viewModel._Parametres = new Parametres();
            //viewModel._Parametres.fnGet();
            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDeliveryCoding", Model = viewModel };
        }

        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mTran = new DataTransaction();
            try
            {
                Livraison mlivraison = JSON.Deserialize<Livraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
                AnalyseCode mCode = new AnalyseCode();

                var mListCode = mCode.fnSelectByDelivery(mlivraison.ID);
                
                _db = mCode.db();
                mTran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                bool result = false;

                foreach (AnalyseCode item in mListCode)
                {
                    item.UtilisateurModification = (string)Session["userName"];                    
                    item.SetDataSource(_db);
                    result = item.fnDeActivate(mTran);

                    if (!result)
                    {
                        _db.RollBackTransaction(mTran);
                        throw new Exception("OnActivateDeactivate : Codification loading failed.");
                    }
                }                                                       

                if (result)
                {
                    _db.CommitTransaction(mTran);
                    Store mstore = X.GetCmp<Store>("storeListeAnalysePhysique");

                    ModelProxy mProxy = mstore.GetById(mlivraison.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mlivraison);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mTran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Codification : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult SelectDeliveryInfo(string ItemDeliveryNumber)
        {
            Store mStore = X.GetCmp<Store>("storeDeliveryCodeListe");
            try
            {
                Livraison mClass = new Livraison();

                mClass.LivraisonType = new LivraisonType();
                mClass.Fournisseur = new Fournisseur();

                mClass.fnSelectByNumberAndType(ItemDeliveryNumber);
                if (mClass.ID != Guid.Empty)
                {
                    X.GetCmp<TextField>("TxtDeliveryCodingID").Text = mClass.ID.ToString();
                    X.GetCmp<TextField>("TxtDeliveryDate").Text = string.IsNullOrEmpty(mClass.DateLivraison.ToString()) ? "" : mClass.DateLivraison.ToString();
                    X.GetCmp<TextField>("TxtDeliveryType").Text = string.IsNullOrEmpty(mClass.LivraisonType.Designation) ? "" : mClass.LivraisonType.Designation;
                    X.GetCmp<TextField>("TxtSupplier").Text = string.IsNullOrEmpty(mClass.Fournisseur.Nom) ? "" : mClass.Fournisseur.Nom;
                    X.GetCmp<TextField>("TxtTruckID").Text = string.IsNullOrEmpty(mClass.Immatriculation) ? "" : mClass.Immatriculation;
                    X.GetCmp<Button>("BtnGenerateCode").Disabled = false;

                    var mListe = (new AnalyseCode()).fnSelectByDelivery(mClass.ID);
                    
                    mStore.RemoveAll();
                    mStore.Add(mListe);                    
                }
                else
                {
                    mStore.RemoveAll();
                    X.GetCmp<Button>("BtnGenerateCode").Disabled = true;
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Codification : Code",
                        Message = "Impossible to generate code for this delivery !",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                                        
                }
            }   
            catch (Exception ex)
            {
                mStore.RemoveAll();
                X.GetCmp<FormPanel>("DeliveryCodingFormPanel").Reset();
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Codification : Code",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            
            //OnRefresh(mClass.ID.ToString());
            return this.Direct();
        }
        public ActionResult GenerateCode(string StoreNbrOfRows)
        {
            int rowcount = int.Parse(StoreNbrOfRows);

            int Limit = int.Parse(X.GetCmp<TextField>("hiddenMaxCodeAnalyseGenerate").Text);

            if (rowcount < Limit)
            {
                AnalyseCodeViewModel analyseCodeVm = new AnalyseCodeViewModel();
                analyseCodeVm._AnalyseCode = new AnalyseCode();
                string mNewCode = string.Empty;
                Guid mRndCode = Guid.NewGuid();
                string mResultCode = mRndCode.ToString();

                //code is generated from guid "7fa9bdbc-5a60-48ca-9cd5-9f58054f404a"
                //generate 5 digit code

                if (mResultCode != " ")
                {
                    //Dim mCode1 As String = Microsoft.VisualBasic.Left(mResultCode, 8)
                    //mCode1 = Microsoft.VisualBasic.Mid(mCode1, 4, 1)

                    //Dim mCode2 As String = Microsoft.VisualBasic.Mid(mResultCode, 10, 4)
                    //mCode2 = Microsoft.VisualBasic.Mid(mCode2, 2, 1)

                    string code1 = mResultCode.PadLeft(8);
                    code1 = code1.Substring(4, 1).ToUpper();

                    string code2 = mResultCode.Substring(10, 4);
                    code2 = code2.Substring(2, 1).ToUpper();

                    string code3 = mResultCode.Substring(15, 4);
                    code3 = code3.Substring(2, 1).ToUpper();

                    string code4 = mResultCode.Substring(20, 4);
                    code4 = code4.Substring(2, 1).ToUpper();

                    string code5 = mResultCode.Substring(25, 8);
                    code5 = code5.Substring(2, 1).ToUpper();

                    mNewCode = "A" + code1 + code2 + code3 + code4 + code5;

                    Store mStore = X.GetCmp<Store>("storeDeliveryCodeListe");
                    AnalyseCode analysecode = new AnalyseCode();

                    analysecode.Code = mNewCode;
                    analysecode.ID = Guid.NewGuid();
                    analysecode.IsNew = true;
                    analysecode.DateCode = DateTime.Now;

                    if (!string.IsNullOrEmpty(mNewCode))
                        mStore.Insert(0, analysecode);

                    X.GetCmp<RowSelectionModel>("rowCode").Select(0);

                }
            }
            else
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Codification : Code",
                    Message = "Codification can be generated Only " + Limit +  " times",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelDeliveryCode");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult SelectCodeForAnalyse(string ItemCodeAnalyseID)
        {
            Guid? id = Guid.Empty;
            if (string.IsNullOrEmpty(ItemCodeAnalyseID)) id = (Guid?)null;
            else id = Guid.Parse(ItemCodeAnalyseID);
            var mListe = (new AnalyseCode()).fnSelectCodeForAnalysePhysique(id);            
            return this.Store(mListe);
        }

        public ActionResult onRefreshDelivery(string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemLivraison)
        {
            try
            {
                if (!string.IsNullOrEmpty(ItemPeriodStart) && !string.IsNullOrEmpty(ItemPeriodEnd))
                {
                    DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                    DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);
                }
                

                Store store = X.GetCmp<Store>("storeCodeListe");

                store.Reload();

                store.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemPeriodStart"     ,ItemPeriodStart),
                                new Ext.Net.Parameter("ItemPeriodEnd"           ,ItemPeriodEnd),
                                new Ext.Net.Parameter("ItemStatus"           ,ItemStatus),
                                new Ext.Net.Parameter("ItemLivraison"           ,ItemLivraison)
                            });
                FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelDeliveryCode");

                mform.Collapsed = true;
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Codification : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }            

            return this.Direct();
            
        }

        public void onRefresh(string livraison)
        {
            Store mstore = X.GetCmp<Store>("storeDeliveryCodeListe");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("livraison"   ,livraison),
                            });            
        }


        public ActionResult SelectCode(string ItemDelivery, string ItemExecMode)
        {
            //if (ItemExecMode == "Update")
            //{
                Guid IdLivraison = Guid.Empty;
                if (!string.IsNullOrEmpty(ItemDelivery))
                {
                    IdLivraison = Guid.Parse(ItemDelivery.ToString());
                }
                var mListe = (new AnalyseCode()).fnSelectByDelivery(IdLivraison);
                return this.Store(mListe);
            //}
            //else
            //{
            //    return this.Store(null);
            //}       
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {            
            if (string.IsNullOrEmpty(ItemPeriodStart) && string.IsNullOrEmpty(ItemPeriodEnd) && string.IsNullOrEmpty(ItemStatus))
            {
                var mListe = (new AnalyseCode()).fnSelect();
                return this.Store(mListe);
            }
            else
            {
                //int locationID = GetCritriaValue(ItemLocation);

                DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
                DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
                int status = -1;
                if (string.IsNullOrEmpty(ItemStatus))
                {
                    status = -1;
                }
                else if (ItemStatus == "true")
                {
                    status = 0;
                }
                var mListe = (new AnalyseCode()).fnSelect(startdate, enddate, -1);

                // Paging
                //int start = parameters.Start;

                //int limit = parameters.Limit;

                //if ((start + limit) > mListe.Count)
                //{
                //    limit = mListe.Count - start;
                //}

                //List<DataPersist> rangePlants = (start < 0 || limit < 0) ? mListe : mListe.GetRange(start, limit);

                //string filterHeaders = this.Request.Params["filterheader"];
                //var paging = GridStorePaging.SetRangePlants(parameters, mListe);
                //return this.Store(GridStorePaging.SetRangePlants(parameters, mListe, filterHeaders));
                return this.Store(mListe);

            }
        }

        public ActionResult SelectLivraisons(string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemLivraison)
        {
            if (!string.IsNullOrEmpty(ItemLivraison))
            {
                Guid id = Guid.Parse(ItemLivraison);
                Livraison mLivraison = new Livraison();
                mLivraison.fnGetForDeliveryCoding(id);          
                return this.Store(mLivraison);
            }

            if (string.IsNullOrEmpty(ItemPeriodStart) && string.IsNullOrEmpty(ItemPeriodEnd) && string.IsNullOrEmpty(ItemStatus))
            {
                var mListe = (new Livraison()).fnSelectForDeliveryCoding(null, null, -1);
                return this.Store(mListe);
            }
            else
            {
                //int locationID = GetCritriaValue(ItemLocation);

                DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
                DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
                int status = -1;
                if (string.IsNullOrEmpty(ItemStatus))
                {
                    status = -1;
                }
                else if (ItemStatus == "true")
                {
                    status = 0;
                }
                var mListe = (new Livraison()).fnSelectForDeliveryCoding(startdate, enddate, -1);

                return this.Store(mListe);
            }
        }

        public void RefreshListeLivraison(string ItemLivraison)
        {
            if (!string.IsNullOrEmpty(ItemLivraison))
            {
                Guid id = Guid.Parse(ItemLivraison);
                Livraison mLivraison = new Livraison();
                mLivraison.fnGetForDeliveryCoding(id);
                this.Store(mLivraison);
            }
        }

        public ActionResult SubmitFormMethod(string storerows, string storeListeDelivery)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();
            try
            {
                
                //AnalyseCode analysecode = new AnalyseCode();

                Store mstore = X.GetCmp<Store>("storeCodeListe");

                Guid livraisonID = Guid.Parse(X.GetCmp<TextField>("TxtDeliveryCodingID").Text);
                

                bool IsNewAnalyse = false;
                bool Result = true; ;

                //if (livraison.CodesAnalyse == null) IsNewAnalyse = true;

                List<AnalyseCode> ListeAnalyse = JSON.Deserialize<List<AnalyseCode>>(storerows, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                if (ListeAnalyse.Count > 0)
                {                                        
                    AnalyseCode analysecode = new AnalyseCode();
                    _db = analysecode.db();
                    mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                    for (int i = 0; i < ListeAnalyse.Count; i++)
                    {
                        if (ListeAnalyse.ElementAt(i).IsNew)
                        {

                            analysecode.Code = ListeAnalyse.ElementAt(i).Code;
                            analysecode.DateCode = DateTime.Now;

                            analysecode.Livraison = new Livraison();
                            analysecode.Livraison.ID = Guid.Parse(X.GetCmp<TextField>("TxtDeliveryCodingID").Text);
                            analysecode.Livraison.DateLivraison = DateTime.Parse(X.GetCmp<TextField>("TxtDeliveryDate").Text);
                            analysecode.Livraison.Numero = X.GetCmp<TextField>("TxtDeliveryNum").Text;
                            analysecode.Livraison.Immatriculation = X.GetCmp<TextField>("TxtTruckID").Text;

                            analysecode.Livraison.Fournisseur = new Fournisseur();
                            analysecode.Livraison.Fournisseur.Nom = X.GetCmp<TextField>("TxtSupplier").Text;

                            analysecode.Livraison.LivraisonType = new LivraisonType();
                            analysecode.Livraison.LivraisonType.Designation = X.GetCmp<TextField>("TxtDeliveryType").Text;
                            analysecode.IsNew = ListeAnalyse.ElementAt(i).IsNew;
                            analysecode.UtilisateurCreation = (string)Session["userName"];
                            analysecode.UtilisateurModification = (string)Session["userName"];

                            Result = analysecode.fnUpdate();
                        }
                        if (!Result)
                        {
                            break;
                        }                        
                        
                    }

                    if (!Result)
                    {
                        _db.RollBackTransaction(mtran);
                    }
                    else
                    {
                        _db.CommitTransaction(mtran);

                        List<Livraison> mLivraison = JSON.Deserialize<List<Livraison>>(storeListeDelivery, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                        if (mLivraison.Count > 0)
                        {
                            Livraison livraison = new Livraison();
                            livraison.fnGetForDeliveryCoding(livraisonID);                        
                            
                            bool IsInList = false;
                            IsInList = mLivraison.FindIndex(l => l.ID == livraisonID) == -1 ? false : true;
                            if (IsInList)
                            {
                                ModelProxy mProxy = mstore.GetById(livraisonID);
                                mProxy.BeginEdit();
                                mProxy.Set(livraison);
                                mProxy.Commit();
                                mProxy.EndEdit();
                            }
                            else
                            {
                                mstore.Insert(0, livraison);
                                X.GetCmp<RowSelectionModel>("rowSelectionCode").Select(0);
                            }
                        }
                        else
                        {
                            onRefreshDelivery(null, null, null, livraisonID.ToString());
                        }
                                                                       

                    }                    

                    X.GetCmp<Window>("FormDeliveryCoding").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();                    
                }              

            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mtran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Codification : Code",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }            
      
            return this.Direct();
        }

        public ActionResult OnRemove(string ItemSelected, string storeListeDelivery)
        {

            try
            {
                AnalyseCode mClass = JSON.Deserialize<AnalyseCode>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate , NullValueHandling = NullValueHandling.Ignore});
                bool result = mClass.fnGet(mClass.ID);

                if (mClass == null || mClass.ID == Guid.Empty)
                    throw new Exception("Remove : Codification remove failed.");

                Store store = X.GetCmp<Store>("storeDeliveryCodeListe");
                Store mstore = X.GetCmp<Store>("storeCodeListe");

                Guid idlivraison = Guid.Parse(X.GetCmp<TextField>("TxtDeliveryCodingID").Text);

                ModelProxy proxy = store.GetById(mClass.ID);
                ModelProxy mProxy = mstore.GetById(idlivraison);

                if (mClass.IsNew)
                {
                    proxy.Drop();

                    //proxy.Set(mClass);

                    //proxy.Commit();

                    return this.Direct();
                }

                result = mClass.fnRemove(mClass.ID);
                Livraison livraison = new Livraison();
                livraison.fnGetForDeliveryCoding(idlivraison);
                if (result)
                {                     
                                        
                    proxy.Drop();

                    List<Livraison> mLivraison = JSON.Deserialize<List<Livraison>>(storeListeDelivery, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (mLivraison.Count > 0 && ( mLivraison.FindIndex(l => l.ID == idlivraison) != -1) )
                    {
                        mProxy.BeginEdit();
                        mProxy.Set(livraison);
                        mProxy.Commit();
                        mProxy.EndEdit();
                    }
                    
                    //DeselectGridRows(); 
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Codification : Retirer Code",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }
       
        public ActionResult OnPrintCode(string ItemSelected)
        {
            if (string.IsNullOrEmpty(ItemSelected))
                throw new Exception("Codification : Operation failed.");
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));                        
            
            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'DeliveryCode{0}', '{1}/DeliveryCoding/ViewDeliveryCode?Code={2}', this, 'Codification','')", ItemSelected, BaseUrl, ItemSelected));                    
        }

        public ActionResult ViewDeliveryCode(string Code)
        {
            RptDeliveryCode rpt = new RptDeliveryCode();            

            rpt.Parameters["CodeAnalyse"].Value = Code.Trim();
           
            ViewData["Report"] = rpt;

            return View();
        }

        private void DeselectGridRows()
        {
            //X.Js.Call("App.rowSelectionListe.deselectAll();");
            X.GetCmp<RowSelectionModel>("rowSelectionCode").DeselectAll();
        }

        public ActionResult OnDisplayDeliveryCodingList()
        {
            ViewData["Titre"] = "Imprimer Codification";
            ViewData["actionToDo"] = "OnPrintDeliveryCodingList";
            ViewData["ControllerName"] = "DeliveryCoding";
            ViewData["SiteParDefaut"] = 1;
            ViewData["UrlSite"] = "LoadSiteByAccess";
            return new Ext.Net.MVC.PartialViewResult { ViewName = "frmPeriodForReport", ViewData = ViewData };
        }

        public ActionResult OnPrintDeliveryCodingList(string startDate, string endDate)
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {
                DateTime dateDebut = DateTime.ParseExact(startDate, "d", CultureInfo.CurrentUICulture);
                DateTime dateFin = DateTime.ParseExact(endDate, "d", CultureInfo.CurrentUICulture);

                //Session["paramCropYear"] = cropYear;                

                Session["paramStartDate"] = dateDebut;
                Session["paramEndDate"] = dateFin;
                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/DeliveryCoding/ViewReportResult', this, 'Delivery Codings',''),App.frmPeriodForReport.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Codification : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }


        }

        public ActionResult ViewReportResult()
        {
            //XtraReport report = null;

            rptCodingList report = new rptCodingList();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];

            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

    }
}