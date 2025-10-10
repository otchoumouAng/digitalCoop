using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Business;
using Tms.Classes.Business.Sales;
using Tms.Classes.Business.stock;
using Tms.Classes.Security;
using Tms.Classes.Shared.Sales;
using Tms.Components.Data;

namespace Tms2017.MVC.Controllers
{
    public class ConteneurController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";

        // GET: Conteneur
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadListOfLot(string ItemExecMode, string ItemConteneurID)
        {

            List<DataPersist> myList = new List<DataPersist>();
            if (!string.IsNullOrEmpty(ItemConteneurID))
            {

                if (ItemExecMode == "AddNew")
                {


                }
                else
                {
                    myList = new ConteneurLot().fnSelectForContainer(Guid.Parse(ItemConteneurID),-1);
                }

                ConteneurLot mClass = new ConteneurLot();
                if (myList.Count > 0)
                    mClass = myList[0] as ConteneurLot;
                else myList = new List<DataPersist>();

            }
            else
            {
                Store mstore = X.GetCmp<Store>("storeListLot");
                mstore.RemoveAll();
                myList = null;
            }

            return this.Store(myList);
        }

        public ActionResult LoadAvailableLot(string ItemEmbarquementID)
        {
            List<DataPersist> mList = new Lot().fnSelectLotForContainer(Guid.Parse(ItemEmbarquementID));
            return this.Store(mList);
        }

        public ActionResult LoadListOfConteneurByEmpotage(string ItemEmpotage)
        {
            try
            {
                Guid EmpotageID = Guid.Empty;
                bool isGuid = Guid.TryParse(ItemEmpotage, out EmpotageID);
                List<DataPersist> mList = new List<DataPersist>();
                if (isGuid)
                    mList = new Conteneur().fnSelectByEmpotage(EmpotageID);
                else
                    mList = null;
                return this.Store(mList);
            }
            catch (Exception)
            {
                return this.Direct();                
            }
            
        }

        public ActionResult OnAddContainer(string ItemEmpotageID, string ItemEmbarquementID)
        {
            ConteneurViewModel viewmodel = new ConteneurViewModel();

            viewmodel._Conteneur = new Conteneur();

            viewmodel._Conteneur.Empotage = new Empotage();
            viewmodel._Conteneur.Empotage.ID = Guid.Parse(ItemEmpotageID);
            viewmodel._Conteneur.Empotage.Embarquement = new Embarquement();
            viewmodel._Conteneur.Empotage.Embarquement.ID = Guid.Parse(ItemEmbarquementID);

            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            #region "Access"
            string UserName = (string)Session["userName"];

            Fonction HasAccess = new Fonction();

            bool LotPermissionRemove = HasAccess.fnGetUserAccessStatus("{7CEF4AA2-6683-4F24-918D-9F489FA73D21}", UserName);
            bool LotPermissionAdd = HasAccess.fnGetUserAccessStatus("{6F8043C5-3F7F-4D9B-A8D4-0E3FB9595783}", UserName);
            bool LotPermissionEdit = HasAccess.fnGetUserAccessStatus("{D31D5D74-7973-412F-831B-F63CDA71C1D4}", UserName);



            if (LotPermissionRemove == true)
            {
                ViewData["LotPermissionRemove"] = true;
            }
            else
            {
                ViewData["LotPermissionRemove"] = false;
            }

            if (LotPermissionAdd == true)
            {
                ViewData["LotPermissionAdd"] = true;
            }
            else
            {
                ViewData["LotPermissionAdd"] = false;
            }


            if (LotPermissionEdit == true)
            {
                ViewData["LotPermissionEdit"] = true;
            }
            else
            {
                ViewData["LotPermissionEdit"] = false;
            }

            #endregion
            return new Ext.Net.MVC.PartialViewResult { ViewName = "ConteneurLot_Detail" , Model = viewmodel, ViewData = ViewData };
        }

       
        public ActionResult onEditContainer(string ItemSelected, string ItemEmbarquementID)
        {
            Conteneur mclass = JSON.Deserialize<Conteneur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            ConteneurViewModel viewModel = new ConteneurViewModel();

            viewModel._Conteneur = mclass;
            viewModel._Conteneur.Empotage.Embarquement = new Embarquement();
            viewModel._Conteneur.Empotage.Embarquement.ID = Guid.Parse(ItemEmbarquementID);


            viewModel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            #region "Access"
            string UserName = (string)Session["userName"];

            Fonction HasAccess = new Fonction();

            bool LotPermissionRemove = HasAccess.fnGetUserAccessStatus("{7CEF4AA2-6683-4F24-918D-9F489FA73D21}", UserName);
            bool LotPermissionAdd = HasAccess.fnGetUserAccessStatus("{6F8043C5-3F7F-4D9B-A8D4-0E3FB9595783}", UserName);
            bool LotPermissionEdit = HasAccess.fnGetUserAccessStatus("{D31D5D74-7973-412F-831B-F63CDA71C1D4}", UserName);



            if (LotPermissionRemove == true)
            {
                ViewData["LotPermissionRemove"] = true;
            }
            else
            {
                ViewData["LotPermissionRemove"] = false;
            }

            if (LotPermissionAdd == true)
            {
                ViewData["LotPermissionAdd"] = true;
            }
            else
            {
                ViewData["LotPermissionAdd"] = false;
            }


            if (LotPermissionEdit == true)
            {
                ViewData["LotPermissionEdit"] = true;
            }
            else
            {
                ViewData["LotPermissionEdit"] = false;
            }

            #endregion

            return new Ext.Net.MVC.PartialViewResult { ViewName = "ConteneurLot_Detail", Model = viewModel, ViewData = ViewData };
        }

        public ActionResult OnRemoveContainer(string ItemSelected)
        {

            try
            {

                Conteneur  mClass = JSON.Deserialize<Conteneur>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                bool result = false;
                mClass.RowVersionKey = Convert.FromBase64String(mClass.RowVersionKey.ToString());
                result = mClass.fnRemove();
                if(result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeConteneur");
                    ModelProxy mProxy = mstore.GetById(mClass.ID);
                    mProxy.Drop();
                    DeselectGridRows();
                }
               


            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Lot : Retirer Lot",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult SubmitOnAdd()
        {
            ConteneurLot mClass = new ConteneurLot();
            Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecModeCL").Value);

            if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                mClass.ID = Guid.NewGuid();
            else
           { 
          
                mClass.ID = Guid.Parse(X.GetCmp<Hidden>("hiddenConteneurLotID").Text);
                //mClass.Conteneur = new Conteneur();
                //mClass.Conteneur.ID = Guid.Parse(X.GetCmp<Hidden>("hiddenConteneurID").Text);

            }


            mClass.Lot = new Lot();
            mClass.Lot.ID = Guid.Parse(X.GetCmp<ComboBox>("_cmbLot").Text);
            mClass.Lot.NumeroLot = X.GetCmp<ComboBox>("_cmbLot").SelectedItem.Text.ToString();
            mClass.NbreSacs = int.Parse(X.GetCmp<TextField>("txtNbrOfBagsPreleve").Text);
            Store mstore = X.GetCmp<Store>("storeListLot");
            mstore.Insert(0,mClass);
            X.GetCmp<Window>("Lot_Detail").Close();

            return this.Direct();
        }
        public ActionResult OnAddLot()
        {
            ConteneurLotViewModel viewmodel = new ConteneurLotViewModel();

            viewmodel._ConteneurLot = new ConteneurLot();

            viewmodel._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Lot_Detail", Model = viewmodel };
        }

        public ActionResult OnRemoveLot(string ItemSelected)
        {

            try
            {

                ConteneurLot mClass = JSON.Deserialize<ConteneurLot>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                Store mstore = X.GetCmp<Store>("storeListLot");
                ModelProxy mProxy = mstore.GetById(mClass.ID);
                mProxy.Drop();
                X.GetCmp<RowSelectionModel>("rowSelectionListLot").DeselectAll();


            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Lot : Retirer Lot",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitFormContainerMethod(string storeListLot)
        {
            DataSource _db = new DataSource();
            DataTransaction mTran = new DataTransaction();

            try
            {
                Conteneur mClass = new Conteneur();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("HiExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("HiConteneurID")));

                    if (mClass == null || mClass.ID == Guid.Empty)
                        throw new Exception("UpdateFormMethod : Contianer load failed.");

                    if (mClass.Desactive)
                        throw new Exception("UpdateFormMethod : Container is disabled ! Please Refresh Overview");
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

                    List<ConteneurLot> mLot = JSON.Deserialize<List<ConteneurLot>>(storeListLot, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (mLot.Count > 0)
                    {
                        if (formExecMode != Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                        {
                            mClass.fnRemoveLotAll();
                        }
                        mClass.LotsConteneur = "";
                        for (int i = 0; i < mLot.Count; i++)
                        {
                            if (mLot.ElementAt(i).Desactive == false)
                            {
                                ConteneurLot Lot = new ConteneurLot();
                                Lot = mLot.ElementAt(i);
                                Lot.IsNew = true;    
                                Lot.Conteneur = new Conteneur();
                                Lot.SetDataSource(_db);
                                Lot.Conteneur.ID = mClass.ID;

                                Lot.UtilisateurCreation = (string)Session["userName"];
                                Lot.UtilisateurModification = (string)Session["userName"];
                                Result = Lot.fnUpdate(mTran);
                                if (Result)
                                {
                                  if(i==0)
                                  {
                                        mClass.LotsConteneur = mLot.ElementAt(i).Lot.NumeroLot;
                                  }
                                  else
                                  {
                                        mClass.LotsConteneur = mClass.LotsConteneur.ToString() +" ,"+ mLot.ElementAt(i).Lot.NumeroLot.ToString();
                                  }   
                                }
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
                        else
                        {
                            _db.CommitTransaction(mTran);
                        }

                    }



                }
                Store mStore = X.GetCmp<Store>("storeListeConteneur");
                Conteneur mEmb = mClass;

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                {
                    mStore.Insert(0, mEmb);
                    X.GetCmp<RowSelectionModel>("rowSelectionListeConteneur").Select(0);
                }
                else
                {
                    ModelProxy mProxy = mStore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mEmb);

                    mProxy.Commit();

                    mProxy.EndEdit();


                }

                X.GetCmp<Window>("ConteneurLot_Detail").Close();

            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mTran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Container : SubmitFormMethod",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        //public ActionResult OnSelectDelivery()
        //{
        //    LivraisonViewModel mclass = new LivraisonViewModel();

        //    var StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
        //    var EndDate = DateTime.Now.ToShortDateString();

        //    var Exportateur = "{Tous}";
        //    mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;


        //    return new Ext.Net.MVC.PartialViewResult { ViewName = "Select_Livraison", Model = mclass };


        //}

        //public ActionResult OnRefreshForAvailableDeliveries(string ItemExportateur, string ItemPeriodStart, string ItemPeriodEnd)
        //{

        //    int ExportateurID = GetCriteriaValue(ItemExportateur);


        //    DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
        //    DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

        //    Store mstore = X.GetCmp<Store>("storeListDelivery");
        //    mstore.Reload(new Ext.Net.ParameterCollection()
        //                        {
        //                            new Ext.Net.Parameter("ItemExportateur"   ,ItemExportateur),
        //                            new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
        //                            new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd)
        //                        });

        //    return this.Direct();
        //}


        //public ActionResult LoadListOfAvailableDeliveries(string ItemExportateur, string ItemPeriodStart, string ItemPeriodEnd)
        //{

        //    int ExportateurID = GetCriteriaValue(ItemExportateur);


        //    DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
        //    DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

        //    var mListe = (new Livraison()).fnSelectForContainer( ExportateurID, StartDate, EndDate);

        //    //return this.Store(paging);
        //    return this.Store(mListe);
        //}


        //public ActionResult SubmitOnSelectDelivery(string ItemSelected)
        //{
        //    try
        //    {
        //        Livraison Item = JSON.Deserialize<Livraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

        //        if (Item != null)
        //        {
        //            X.GetCmp<TextField>("txtDeliveryNumber").Text = Item.Numero;
        //            X.GetCmp<TextField>("txtLivraisonID").Value = Item.ID;

        //            X.GetCmp<Window>("ListOfDeliveries").Close();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        X.MessageBox.Show(new MessageBoxConfig
        //        {
        //            Title = "Select Livraison : SubmitArticleForm",
        //            Message = ex.Message,
        //            Buttons = MessageBox.Button.OK,
        //            Icon = MessageBox.Icon.WARNING
        //        });
        //    }

        //    return this.Direct();
        //}


        #region "Methods"
        private Conteneur MapFormToObject(Conteneur mClass)
        {
            try
            {
                mClass.ID = Guid.Parse(X.GetCmp<Hidden>("HiConteneurID").Text);

                //mClass.Numero = X.GetCmp<TextField>("txtNumContainer").Text;
                mClass.Numero = X.GetCmp<ComboBox>("cmbCont").SelectedItem.Text;
                mClass.Empotage = new Empotage();
                mClass.Empotage.ID = Guid.Parse(X.GetCmp<Hidden>("HiEmpotageID").Text);

                ConteneurType mType = new ConteneurType();
                mType.ID = int.Parse(X.GetCmp<ComboBox>("_cmbType").Text);
                mType.Designation = X.GetCmp<ComboBox>("_cmbType").SelectedItem.Text.ToString();
                mClass.ConteneurType = mType;

                
               



                mClass.NumPlombCompMaritime = X.GetCmp<TextField>("txtNumPlombCompMaritime").Text;
                mClass.NumPlombTelcar = X.GetCmp<TextField>("txtNumPlombTelcar").Text;

                mClass.LivraisonNumero = X.GetCmp<TextField>("txtDeliveryNumber").Text;
                mClass.NbreSacs = int.Parse(X.GetCmp<TextField>("txtNbreSacsTotal").Text);


                mClass.UtilisateurCreation = (string)Session["userName"];
                mClass.UtilisateurModification = (string)Session["userName"];



            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Container : MapFormToObject",
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
            X.GetCmp<RowSelectionModel>("rowSelectionListeConteneur").DeselectAll();
        }

        #endregion
    }
}