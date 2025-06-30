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
    public class OrdreProductionController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        // GET: OrdreProduction
        public ActionResult Index()
        {
            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("txtStartDate").RawText = StartDate;
            X.GetCmp<DateField>("txtEndDate").RawText = EndDate;

            X.GetCmp<FormPanel>("OrdreProductionCP").SetTitle(" Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{F03E06E0-2516-4833-9C41-20B895E21481}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{E94E4684-723D-4CE7-B8DC-8A8845714083}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{5318B179-E2A2-4A9A-A621-3B1FB3C9030D}")))
                X.GetCmp<MenuItem>("mnuPrintProductionList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintProductionList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{E3BF502C-D23E-422D-91EE-D8E1C5CAA9E3}")))
                X.GetCmp<MenuItem>("mnuPrintProductionShrink").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintProductionShrink").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{DEDFF234-51A2-41EC-AA0E-3D4153B008F1}")))
                X.GetCmp<MenuItem>("mnuExportProduction").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportProduction").Disable();
           

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{C21C4554-D3DC-4095-A69E-7C954E042963}")))
                X.GetCmp<Hidden>("OphiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("OphiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{3B02D2B4-593E-4A53-B011-C626BC3BDE3E}")))
                X.GetCmp<Hidden>("OphiddenPermClose").SetValue(true);
            else
                X.GetCmp<Hidden>("OphiddenPermClose").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{F9D4AF04-BA10-457E-AEB9-21D1B587D0AF}")))
                X.GetCmp<Hidden>("OphiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("OphiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{1974380D-25C7-4FD8-9920-688CDFAFCA54}")))
                X.GetCmp<Hidden>("OphiddenPermPrintSheet").SetValue(true);
            else
                X.GetCmp<Hidden>("OphiddenPermPrintSheet").SetValue(false);

            //X.GetCmp<Hidden>("mvhiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{94E1648B-BB69-4470-8B5D-FA8D19598161}", UserName));
            //X.GetCmp<Hidden>("mvhiddenPermGenerate").SetValue(HasAccess.fnGetUserAccessStatus("{C1785226-126D-4648-95C6-92894707BC1E}", UserName));
            //X.GetCmp<Hidden>("mvhiddenPermPrint").SetValue(HasAccess.fnGetUserAccessStatus("{DF2C998A-9232-48ED-B403-6DB46289BE0A}", UserName));
            //X.GetCmp<Hidden>("mvhiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{3CA6BD01-DFC1-4B9F-9E0E-134391DF8B4F}", UserName));
            #endregion

            return View();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("OrdreProductionCP");
            mform.ToggleCollapse();            
            return this.Direct();
        }

        public ActionResult OnCancel(string ItemSelected)
        {
            try
            {
                OrdreProduction mOrdre = JSON.Deserialize<OrdreProduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = mOrdre.fnGet(mOrdre.ID);

                if (!result)
                    throw new Exception("OnCancel : Ordre De Production loading failed.");

                mOrdre.UtilisateurModification = (string)Session["userName"];

                if (mOrdre.Desactive)
                    result = mOrdre.fnActivate();
                else
                    result = mOrdre.fnDeActivate();

                if (!result)
                    throw new Exception("OnCancel : Ordre De Production, Operation failed.");

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeOrdreProduction");

                    ModelProxy mProxy = mstore.GetById(mOrdre.ID);

                    mProxy.BeginEdit();
                    mProxy.Set(mOrdre);
                    mProxy.Commit();
                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Ordre De Production : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult LoadProduction()
        {
            //Load Current Ordre De Production
            List<DataPersist> mList = new OrdreProduction().fnSelectOpenned();
            return this.Store(mList);
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemQuart, string ItemMelangeur,string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatut)
        {
            int quartID = GetCriteriaValue(ItemQuart);
            int MelangeurID = GetCriteriaValue(ItemMelangeur);
            int CertificationID = GetCriteriaValue(ItemCertification);
            string Statut = string.IsNullOrEmpty(ItemStatut) ? "-1" : ItemStatut;

            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
            int desactive = Statut == "CA" ? 1 : 0;
            var mListe = (new OrdreProduction()).fnSelect(quartID, MelangeurID, CertificationID, StartDate, EndDate, Statut, desactive);

            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemQuart, string ItemMelangeur, string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatut)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeOrdreProduction");

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

                X.GetCmp<FormPanel>("OrdreProductionCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Ordre De Production : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult onCloseProduction(string ItemSelected)
        {           
            OrdreProduction mclass = new OrdreProduction();
            mclass = JSON.Deserialize<OrdreProduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });                               

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormCloseOrdreProduction", Model = mclass, };
        }

        public ActionResult CloseProduction()
        {
            try
            {
                OrdreProduction mProduction = new OrdreProduction();

                bool result = true;

                mProduction.IsNew = false;

                mProduction.fnGet(Guid.Parse(GetFormValue("TxtOrdreProductionID")));

                if (mProduction == null || mProduction.ID == Guid.Empty)
                    throw new Exception("ApproveComposition : Blending load failed.");

                DateTime DateFinProduction = DateTime.ParseExact(X.GetCmp<DateField>("txtDateFinProduction").RawText.ToString(), "d", CultureInfo.CurrentUICulture);
                mProduction.DateFinProduction = DateFinProduction;
                mProduction.ClotureUtilisateur = (string)Session["userName"];
                mProduction.Commentaire = X.GetCmp<TextArea>("txtComment").Text;
                result = mProduction.fnClose();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeOrdreProduction");
                    ModelProxy mProxy = mstore.GetById(mProduction.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mProduction);

                    mProxy.Commit();

                    mProxy.EndEdit();

                    X.GetCmp<Window>("FormCloseOrdreProduction").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Ordre De Production : Close",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        public ActionResult onBlending(string ItemSelected)
        {
            CompositionUsinageViewModel mclass = new CompositionUsinageViewModel();
            OrdreProduction ordreproduction = JSON.Deserialize<OrdreProduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            CompositionUsinage composition = new CompositionUsinage();

            bool result = composition.fnGetByProduction(ordreproduction.ID);
            if (result && composition.ID != Guid.Empty)
            {
                if (!composition.IsApproved)
                {
                    mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                    ViewData["Title"] = "Production - Melange : Modifier";
                }
                else
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Ordre De Production - Blending : Modifier",
                        Message = "Production blending already approved !",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }                
            }
            else
            {
                mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
                composition = new CompositionUsinage();
                ViewData["Title"] = "Production - Melange : Nouveau";
            }

            mclass._CompositionUsinage = composition;
            mclass._CompositionUsinage.OrdreProduction = ordreproduction;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMelangeProduction", Model = mclass, ViewData = ViewData };

        }

        public ActionResult onApproveBlending(string ItemSelected)
        {
            CompositionUsinageViewModel mclass = new CompositionUsinageViewModel();
            OrdreProduction ordreproduction = JSON.Deserialize<OrdreProduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            CompositionUsinage composition = new CompositionUsinage();

            bool result = composition.fnGetByProduction(ordreproduction.ID);
            if (result && composition.ID != Guid.Empty)
            {
                if (!composition.IsApproved)
                {
                    mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;
                    ViewData["Title"] = "Production - Melange : Approuver";
                }
                else
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Ordre De Production - Blending : Approuver",
                        Message = "Production blending already approved !",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }
            }
            else
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Ordre De Production - Blending : Approuver",
                    Message = "Production blending not available !",
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            mclass._CompositionUsinage = composition;
            mclass._CompositionUsinage.OrdreProduction = ordreproduction;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMelangeProduction", Model = mclass, ViewData = ViewData };

        }

        public ActionResult ApproveComposition(string storerows)
        {
            //DataSource _db = new DataSource();
            //DataTransaction mtran = new DataTransaction();

            try
            {
                CompositionUsinage composition = new CompositionUsinage();
                //MouvementStock mouvement = new MouvementStock();

                bool result = true;
                //bool resultmouvement = true;

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.Approve)
                {
                    composition.IsNew = false;

                    composition.fnGet(Guid.Parse(GetFormValue("txtCompositionID")));

                    if (composition == null || composition.ID == Guid.Empty)
                        throw new Exception("ApproveComposition : Blending load failed.");
                }

                //_db = composition.db();
                //mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);

                composition.Approbateur = (string)Session["userName"];
                composition.Statut = "AP";
                
                result = composition.fnApprove();

                if (result)
                {
                    #region Mouvemement
                    //List<CompositionUsinageLivraison> Items = JSON.Deserialize<List<CompositionUsinageLivraison>>(storerows, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    //if (Items.Count > 0)
                    //{
                    //    foreach (var item in Items)
                    //    {                            
                    //        mouvement = new MouvementStock();
                    //        mouvement.mCampagne = new Campagne();
                    //        mouvement.Exportateur = new Exportateur();
                    //        mouvement.SacType = new SacType();
                    //        mouvement.TypeElementStock = new TypeElementStock();
                    //        mouvement.Certification = new Certification();
                    //        mouvement.Magasin = new Magasin();
                    //        mouvement.MouvementStockType = new MouvementStockType();                                
                    //        Parametres mParam = new Parametres(0);
                    //        mouvement.SetDataSource(_db);
                    //        mouvement.mCampagne.Designation = item.BonDeLivraison.Livraison.Campagne.Designation;
                    //        mouvement.Exportateur.ID = item.BonDeLivraison.Livraison.Exportateur.ID;

                    //        mouvement.SacType.ID = item.BonDeLivraison.Livraison.SacType.ID;
                    //        mouvement.ObjetEnStock = item.BonDeLivraison.Livraison.ID;
                    //        mouvement.ObjetEnStockType = mParam.LivraisonTypeElementStock;

                    //        mouvement.MouvementStockType.ID = mParam.FevesUsinageStockType;
                    //        mouvement.Certification.ID = item.BonDeLivraison.Livraison.Certification.ID;
                    //        mouvement.Sens = "S";
                    //        mouvement.Quantite = item.NombreSacs;
                    //        mouvement.PoidsBrut = item.BonDeLivraison.PoidsBrut;
                    //        mouvement.TarePalettes = item.BonDeLivraison.TarePalettesAjustee;
                    //        mouvement.TareSacs = item.BonDeLivraison.TareSacsAjustee;
                    //        mouvement.PoidsNetLivre = item.BonDeLivraison.PoidsLivre;
                    //        mouvement.PoidsNetAccepte = item.BonDeLivraison.PoidsNetAccepte;
                    //        mouvement.Retention = item.BonDeLivraison.TotalRetention;
                    //        mouvement.Magasin.ID = mParam.Magasin.ID;
                    //        mouvement.DateMouvement = DateTime.Now;
                    //        mouvement.Reference1 = item.BonDeLivraison.Livraison.Numero;
                    //        mouvement.Reference2 = composition.OrdreProduction.NumeroProduction;
                    //        mouvement.Commentaire = "Prelevement Feves Usinage";
                    //        mouvement.Statut = "NA";
                    //        mouvement.UtilisateurCreation = (string)Session["userName"];
                    //        mouvement.UtilisateurModification = (string)Session["userName"];                                

                    //        resultmouvement = mouvement.fnUpdate(mtran);
                    //        if (!resultmouvement) break;                            
                    //    }
                    //    if (!resultmouvement) _db.RollBackTransaction(mtran);
                    //    _db.CommitTransaction(mtran);
                    //}
                    //else
                    //{
                    //    _db.RollBackTransaction(mtran);
                    //    X.MessageBox.Show(new MessageBoxConfig
                    //    {
                    //        Title = "Blending : Data Validation",
                    //        Message = "No items Selected",
                    //        Buttons = MessageBox.Button.OK,
                    //        Icon = MessageBox.Icon.WARNING
                    //    });
                    //}
                    #endregion
                    X.GetCmp<Window>("FormMelangeProduction").Close();
                }
                
            }
            catch (Exception ex)
            {
                //_db.RollBackTransaction(mtran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Blending : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        public ActionResult onAddDelivery(string rowsDeliveriesInListe = "")
        {
            CompositionUsinageLivraisonViewModel mclass = new CompositionUsinageLivraisonViewModel();

            //OrdreProduction ordreproduction = JSON.Deserialize<OrdreProduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._CompositionUsinageLivraison = new CompositionUsinageLivraison();

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMelangeLivraison", Model = mclass, };

        }

        public ActionResult GetDeliveries(StoreRequestParameters parameters, string ItemComposition)
        {
            //Store mstore = X.GetCmp<Store>("storeListeLivraisons");

            //mstore.Reload();

            //mstore.Reload(new Ext.Net.ParameterCollection()
            //                    {
            //                        new Ext.Net.Parameter("ItemComposition",ItemComposition)
            //                    });
            Guid CompositionID = Guid.Parse(ItemComposition);
            CompositionUsinageLivraison mModel = new CompositionUsinageLivraison();
            var mList = mModel.fnSelect(CompositionID);
            return this.Store(mList);
        }
        public ActionResult onAdd()
        {            
            OrdreProductionViewModel mclass = new OrdreProductionViewModel();

            mclass._OrdreProduction = new OrdreProduction();            

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormOrdreProduction", Model = mclass, };

        }

        public ActionResult onEdit(string ItemSelected)
        {
            OrdreProductionViewModel mclass = new OrdreProductionViewModel();
            OrdreProduction ordreProdiction = JSON.Deserialize<OrdreProduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._OrdreProduction = ordreProdiction;

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormOrdreProduction", Model = mclass, };

        }

        public ActionResult onConsult(string ItemSelected)
        {
            OrdreProductionViewModel mclass = new OrdreProductionViewModel();
            mclass._OrdreProduction = JSON.Deserialize<OrdreProduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormOrdreProduction", Model = mclass, };

        }

        private string GetFormValue(string id_Component)
        {
            return Request.Form[id_Component];
        }

        private int GetCriteriaValue(string strComponent)
        {
            int value;

            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
                return value;
            else
                return -1;
        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                OrdreProduction ordreproduction = new OrdreProduction();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    ordreproduction.IsNew = true;
                else
                {
                    ordreproduction.IsNew = false;
                    Guid productionID = Guid.Empty;
                    bool IsGuid = Guid.TryParse(GetFormValue("TxtOrdreProductionID"), out productionID);

                    if (IsGuid)
                        ordreproduction.fnGet(productionID);
                    else
                        throw new Exception("SubmitFormMethod : Ordre De Production load failed.");

                    if (ordreproduction == null || ordreproduction.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Ordre De Production load failed.");
                }

                ordreproduction = MapFormToObject(ordreproduction);

                bool result = ordreproduction.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeOrdreProduction");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, ordreproduction);
                        X.GetCmp<RowSelectionModel>("rowOrdreProduction").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(ordreproduction.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(ordreproduction);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormOrdreProduction").Close();                    
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Ordre De Production : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitComposition(string storerows)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                CompositionUsinage composition = new CompositionUsinage();
                CompositionUsinageLivraison cLivraison = new CompositionUsinageLivraison();

                bool result = true;
                bool resulttransact = true;

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    composition.IsNew = true;
                else
                {
                    composition.IsNew = false;

                    composition.fnGet(Guid.Parse(GetFormValue("txtCompositionID")));

                    if (composition == null || composition.ID == Guid.Empty)
                        throw new Exception("SubmitComposition : Blending load failed.");
                }

                _db = composition.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                composition = MapFormToObjectBlending(composition);
                result = composition.fnUpdate(mtran);

                if (result)
                {
                    List<CompositionUsinageLivraison> Items = JSON.Deserialize<List<CompositionUsinageLivraison>>(storerows, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (Items.Count > 0)
                    {
                        foreach (var item in Items)
                        {
                            if (item.IsNew)
                            {
                                cLivraison = new CompositionUsinageLivraison();
                                cLivraison.Melange = new Melange();
                                cLivraison.BonDeLivraison = new BonDeLivraison();
                                cLivraison.CompositionUsinage = new CompositionUsinage();
                                cLivraison.SetDataSource(_db);

                                cLivraison.Melange.ID = (int)item.MelangeID;
                                cLivraison.BonDeLivraison.ID = item.BonDeLivraison.ID;

                                cLivraison.CompositionUsinage.ID = composition.ID;

                                cLivraison.Humidite = item.Humidite;
                                cLivraison.Moisi = item.Moisi;
                                cLivraison.Mite = item.Mite;
                                cLivraison.NombreSacs = item.NombreSacs;
                                cLivraison.Grainage = item.Grainage;
                                cLivraison.Ardoisee = item.Ardoisee;
                                cLivraison.Ffa = item.Ffa;
                                cLivraison.MatiereEtrangere = item.MatiereEtrangere;
                                cLivraison.Sievings = item.Sievings;

                                cLivraison.PoidsBrut = item.PoidsBrut;
                                cLivraison.TareSacs = item.TareSacs;
                                cLivraison.TarePalettes = item.TarePalettes;
                                cLivraison.PoidsLivre = item.PoidsLivre;
                                cLivraison.Retention = item.Retention;
                                cLivraison.PoidsNet = item.PoidsNet;

                                cLivraison.UtilisateurCreation = (string)Session["userName"];
                                cLivraison.UtilisateurModification = (string)Session["userName"];

                                resulttransact = cLivraison.fnUpdate(mtran);
                                if (!resulttransact) break;
                            }
                        }
                        if (!resulttransact) _db.RollBackTransaction(mtran);
                        _db.CommitTransaction(mtran);
                    }
                    else
                    {
                        _db.RollBackTransaction(mtran);
                        X.MessageBox.Show(new MessageBoxConfig
                        {
                            Title = "Blending : Data Validation",
                            Message = "No items Selected",
                            Buttons = MessageBox.Button.OK,
                            Icon = MessageBox.Icon.WARNING
                        });
                    }
                }
                if (resulttransact)
                {
                    //Store mstore = X.GetCmp<Store>("storeListeInventaire");
                    //mstore.Insert(0, composition);

                    X.GetCmp<Window>("FormMelangeProduction").Close();
                }
            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mtran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Blending : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }


        public ActionResult SubmitDeliveries(string ItemSelected, string rowsDeliveriesInListe = "")
        {
            try
            {
                List<CompositionUsinageLivraison> mLivraison = JSON.Deserialize<List<CompositionUsinageLivraison>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                List<CompositionUsinageLivraison> OldListDeliveries;
                mLivraison = CalculateValuesByNbrOfBags(mLivraison);
                Store store = X.GetCmp<Store>("storeListeLivraisons");

                if (!string.IsNullOrEmpty(rowsDeliveriesInListe) && rowsDeliveriesInListe != "[]")
                {
                    OldListDeliveries = new List<CompositionUsinageLivraison>();
                    OldListDeliveries = JSON.Deserialize<List<CompositionUsinageLivraison>>(rowsDeliveriesInListe, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                    if (OldListDeliveries.Count > 0)
                    {
                        //HashSet<Guid> LivraisonID = new HashSet<Guid>(OldListDeliveries.Select(s => s.BonDeLivraison.Livraison.ID));                        
                        //HashSet<int> Melanges = new HashSet<int>(OldListDeliveries.Select(s => s.Melange.ID));
                        HashSet<string> Concat = new HashSet<string>(OldListDeliveries.Select(s => s.Melange.ID.ToString() + s.BonDeLivraison.Livraison.ID.ToString()));
                                                
                        mLivraison = mLivraison.Where(l => !Concat.Contains(l.Melange.ID.ToString() + l.BonDeLivraison.Livraison.ID.ToString())).ToList();
                        //mLivraison = mLivraison.Where(l => !LivraisonID.Contains(l.BonDeLivraison.Livraison.ID) && !Melanges.Contains(l.Melange.ID)).ToList();
                        //mLivraison.Except(rsult);
                    }
                        //mLivraison.Where(l => l.BonDeLivraison.ID != OldListDeliveries.BonDeLivraison.ID && l.Melange.ID != OldListDeliveries.Melange.ID);

                    store.Add(mLivraison);
                }                                                        
                else
                    store.Add(mLivraison);
                
                X.GetCmp<Window>("FormMelangeLivraison").Close();
                
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Blending - List Of Livraisons disponibles : OnAdd",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR
                });
            }
            return this.Direct();
        }

        private List<CompositionUsinageLivraison> CalculateValuesByNbrOfBags(List<CompositionUsinageLivraison> cLivraison)
        {
            foreach (var item in cLivraison)
            {
                if (item.NombreSacs != item.NbreSacsTotalLivraison && item.IsNew)
                {
                    item.PoidsBrut = Math.Round((item.NombreSacs * item.BonDeLivraison.PoidsBrut) / item.NbreSacsTotalLivraison);                    
                    item.TareSacs = Math.Round( (item.NombreSacs * item.BonDeLivraison.TareSacs) / item.NbreSacsTotalLivraison);
                    item.TarePalettes = Math.Round((item.NombreSacs * item.BonDeLivraison.TarePalettesAjustee) / item.NbreSacsTotalLivraison);
                    item.PoidsLivre = item.PoidsBrut - item.TareSacs - item.TarePalettes;
                    item.Retention = Math.Round((item.NombreSacs * item.BonDeLivraison.TotalRetention) / item.NbreSacsTotalLivraison);
                    item.PoidsNet = Math.Round((item.NombreSacs * item.BonDeLivraison.PoidsNetAccepte) / item.NbreSacsTotalLivraison);
                }
            }
            return cLivraison;
        }

        public ActionResult RemoveDeliveryToBlending(string ItemSelected)
        {
            try
            {
                CompositionUsinageLivraison mcomposition = JSON.Deserialize<CompositionUsinageLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                CompositionUsinageLivraison mClass;

                if (mcomposition.ID != Guid.Empty)
                {
                    Store mstore = X.GetCmp<Store>("storeListeLivraisons");

                    if (mcomposition.IsNew)
                    {
                        ModelProxy _proxy = mstore.GetById(mcomposition.ID);
                        _proxy.Drop();
                        return this.Direct();
                    }
                    else
                    {
                        mClass = new CompositionUsinageLivraison();
                        mClass.fnGet(mcomposition.ID);
                        if (mClass == null || mClass.ID == Guid.Empty)
                            throw new Exception("RemoveDeliveryToBlending : Retirer Delivery failed.");

                        mClass.UtilisateurModification = (string)Session["userName"];

                        bool result = mClass.fnRemove();

                        if (result)
                        {
                            ModelProxy mProxy = mstore.GetById(mClass.ID);
                            mProxy.Drop();
                        }
                    }
                }                

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Ordre De Production - Blending : Retirer Delivery",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult LoadCumulMelanges(string rowsLivraisons = "")
        {
            List<CompositionUsinageLivraison> mLivraison = JSON.Deserialize<List<CompositionUsinageLivraison>>(rowsLivraisons, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            CompositionUsinageLivraison composition = new CompositionUsinageLivraison();
            List<CompositionUsinageLivraison> listeComposition = new List<CompositionUsinageLivraison>();

            Store mStoreMelange = X.GetCmp<Store>("storeListeMelange");
            mStoreMelange.RemoveAll();            

            var mList = new Melange().fnSelect();

            foreach (Melange item in mList)
            {
                int NombreLivraisons = 0;
                bool ContientMelange = false;
                int NbreSac = 0;
                int NbreFeves = 0;
                double Humidite = 0;
                double Ffa = 0;
                double Moisi = 0;
                double MatieresEtrangeres = 0;                
                double weevil = 0;
                double slaty = 0;
                double sieving = 0;
                double poidsnetAccepte = 0;

                foreach (CompositionUsinageLivraison comp in mLivraison.Where(ml => ml.Melange.ID == item.ID))
                {
                    composition = new CompositionUsinageLivraison();

                    NbreSac += comp.NombreSacs;
                    NbreFeves += comp.Grainage;
                    Ffa += comp.Ffa * (double)comp.BonDeLivraison.PoidsNetAccepte;
                    Moisi += comp.Moisi * (double)comp.BonDeLivraison.PoidsNetAccepte;
                    MatieresEtrangeres += comp.MatiereEtrangere * (double)comp.BonDeLivraison.PoidsNetAccepte;
                    Humidite += comp.Humidite * (double)comp.BonDeLivraison.PoidsNetAccepte;
                    weevil += comp.Mite * (double)comp.BonDeLivraison.PoidsNetAccepte;
                    slaty += comp.Ardoisee * (double)comp.BonDeLivraison.PoidsNetAccepte;
                    sieving += comp.Sievings * (double)comp.BonDeLivraison.PoidsNetAccepte;
                    NombreLivraisons += 1;
                    poidsnetAccepte += (double)comp.BonDeLivraison.PoidsNetAccepte;
                    ContientMelange = true;
                }
                if (ContientMelange)
                {
                    composition.ID = Guid.NewGuid();
                    composition.Melange = new Melange();
                    composition.Melange.ID = item.ID;
                    composition.Melange.Designation = item.Designation;
                    composition.NombreSacs = NbreSac;
                    composition.Grainage = NbreFeves;
                    composition.Moisi = Math.Round(Moisi / poidsnetAccepte, 2);
                    composition.Ffa = Math.Round(Ffa / poidsnetAccepte, 2);
                    composition.MatiereEtrangere = Math.Round(MatieresEtrangeres / poidsnetAccepte, 2);
                    composition.Mite = Math.Round(weevil / poidsnetAccepte, 2);
                    composition.Ardoisee = Math.Round(slaty / poidsnetAccepte, 2);
                    composition.Sievings = Math.Round(sieving / poidsnetAccepte, 2);
                    composition.Humidite = Math.Round(Humidite / poidsnetAccepte, 2);
                    listeComposition.Add(composition);
                }
            }
            mStoreMelange.Add(listeComposition);
            return this.Direct();
            //return this.Store(listeComposition.OrderBy(ord => ord.Melange.Designation));
        }

        private OrdreProduction MapFormToObject(OrdreProduction mClass)
        {
            Campagne campagne = new Campagne();
            campagne.Designation = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text.ToString();
            mClass.Campagne = campagne;

            Certification certification = null;
                       
            if (!string.IsNullOrEmpty(GetFormValue("cmbCertification")))
            {
                certification = new Certification();
                certification.ID = int.Parse(GetFormValue("cmbCertification"));
                certification.Designation = X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Text.ToString();
                
            }
            mClass.Certification = certification;

            Exportateur exportateur = new Exportateur();
            exportateur.ID = int.Parse(GetFormValue("cmbExportateur"));
            exportateur.Nom = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text.ToString();
            mClass.Exportateur = exportateur;                                    

            AgentProduction quart = null;
            if (!string.IsNullOrEmpty(GetFormValue("cmbQuart")))
            {
                quart = new AgentProduction();
                quart.ID = int.Parse(GetFormValue("cmbQuart"));
                quart.Nom = X.GetCmp<ComboBox>("cmbQuart").SelectedItem.Text.ToString();
            }
            mClass.Quart = quart;

            AgentProduction melangeur = new AgentProduction();
            melangeur.ID = int.Parse(GetFormValue("cmbMelangeur"));
            melangeur.Nom = X.GetCmp<ComboBox>("cmbMelangeur").SelectedItem.Text.ToString();
            mClass.Melangeur = melangeur;

            AgentProduction ChefQuart = new AgentProduction();
            ChefQuart.ID = int.Parse(GetFormValue("cmbChiefQuart"));
            ChefQuart.Nom = X.GetCmp<ComboBox>("cmbChiefQuart").SelectedItem.Text.ToString();
            mClass.ChefQuart = ChefQuart;

            PoleProvenance pole = null;
            if (!string.IsNullOrEmpty(GetFormValue("cmbPoleProvenance")))
            {
                pole = new PoleProvenance();
                pole.ID = int.Parse(GetFormValue("cmbPoleProvenance"));
                pole.Designation = X.GetCmp<ComboBox>("cmbPoleProvenance").SelectedItem.Text.ToString();
            }
            mClass.PoleProvenance = pole;                        

            mClass.DateProduction = DateTime.Parse(X.GetCmp<DateField>("txtDateProduction").RawText.ToString()).Add(DateTime.Now.TimeOfDay);
            //mClass.DateDebutProduction = DateTime.Parse(X.GetCmp<DateField>("txtDateDebutProduction").RawText.ToString());
            //mClass.DateFinProduction = DateTime.Parse(X.GetCmp<DateField>("txtDateFinProduction").RawText.ToString());
            
            mClass.EstReusine = bool.Parse(X.GetCmp<Checkbox>("ChkEstReusine").Value.ToString());           

            mClass.Commentaire = X.GetCmp<TextField>("txtComment").Text;
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;

        }

        private CompositionUsinage MapFormToObjectBlending(CompositionUsinage mClass)
        {
            OrdreProduction production = new OrdreProduction();
            production.ID = Guid.Parse(X.GetCmp<Hidden>("txtOrdreProductionID").Text.ToString());
            mClass.OrdreProduction = production;

            //AgentProduction melangeur = new AgentProduction();
            //melangeur.ID = int.Parse(GetFormValue("cmbMelangeur"));
            //melangeur.Nom = X.GetCmp<ComboBox>("cmbMelangeur").SelectedItem.Text.ToString();
            //mClass.Melangeur = melangeur;            

            //AgentProduction chefquart = new AgentProduction();
            //chefquart.ID = int.Parse(GetFormValue("cmbChefQuart"));
            //chefquart.Nom = X.GetCmp<ComboBox>("cmbChefQuart").SelectedItem.Text.ToString();
            //mClass.ChefQuart = chefquart;

            //AgentProduction quart = new AgentProduction();
            //quart.ID = int.Parse(GetFormValue("cmbQuart"));
            //quart.Nom = X.GetCmp<ComboBox>("cmbQuart").SelectedItem.Text.ToString();
            //mClass.Quart = quart;

            Laboratoire laboratoire = new Laboratoire();
            laboratoire.ID = int.Parse(GetFormValue("cmbLaboratoire"));
            laboratoire.Designation = X.GetCmp<ComboBox>("cmbLaboratoire").SelectedItem.Text.ToString();
            mClass.Laboratoire = laboratoire;

            mClass.DateComposition = DateTime.Parse(X.GetCmp<DateField>("txtDateComposition").RawText.ToString());
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;

        }


        public ActionResult SelectForProduction(StoreRequestParameters parameters, string ItemCertification, string ItemLivraisonType, string ItemPeriodStart, string ItemPeriodEnd, string ItemListeLivraisons, string ItemCampagne = "{Tous}")
        {

            int CertificationID = GetCriteriaValue(ItemCertification);
            int TypeLivraisonID = GetCriteriaValue(ItemLivraisonType);
            string mCampagne = string.IsNullOrEmpty(ItemCampagne) ? "{Tous}" : ItemCampagne;
            DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            var mListe = (new CompositionUsinageLivraison()).fnSelectAvailableForProduction(CertificationID, TypeLivraisonID, StartDate, EndDate, mCampagne);
            

            if (!string.IsNullOrEmpty(ItemListeLivraisons) && ItemListeLivraisons != "[]")
            {
                List<CompositionUsinageLivraison> mLivraison = JSON.Deserialize<List<CompositionUsinageLivraison>>(ItemListeLivraisons, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                HashSet<Guid> LivraisonId = new HashSet<Guid>(mLivraison.Select(l => l.BonDeLivraison.Livraison.ID));

                foreach (CompositionUsinageLivraison itemNew in mListe)
                {
                    foreach (CompositionUsinageLivraison itemOld in mLivraison)
                    {
                        if ((itemNew.BonDeLivraison.Livraison.ID == itemOld.BonDeLivraison.Livraison.ID) && (itemNew.NombreSacs != itemOld.NombreSacs))
                        {
                            itemNew.NombreSacs = itemNew.NombreSacs - itemOld.NombreSacs;
                        }
                    }                    
                }
            }
            
            return this.Store(mListe);
        }

        public ActionResult SetAvailableNbrSacs(string ItemListeAdded, string ItemListeToAdd)
        {
            if (!string.IsNullOrEmpty(ItemListeAdded) && ItemListeAdded != "[]" && !string.IsNullOrEmpty(ItemListeToAdd) && ItemListeToAdd != "[]")
            {
                List<CompositionUsinageLivraison> oldListe = JSON.Deserialize<List<CompositionUsinageLivraison>>(ItemListeAdded, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                List<CompositionUsinageLivraison> newListe = JSON.Deserialize<List<CompositionUsinageLivraison>>(ItemListeToAdd, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
                //HashSet<Guid> LivraisonId = new HashSet<Guid>(mLivraison.Select(l => l.BonDeLivraison.Livraison.ID));
                int nbr = 0;
                foreach (CompositionUsinageLivraison itemNew in newListe)
                {
                    nbr = 0;
                    foreach (CompositionUsinageLivraison itemOld in oldListe.Where(x => x.BonDeLivraison.Livraison.ID == itemNew.BonDeLivraison.Livraison.ID))
                    {
                        nbr += itemOld.NombreSacs;
                        //if ((itemNew.BonDeLivraison.Livraison.ID == itemOld.BonDeLivraison.Livraison.ID))
                        //{
                            
                        //    itemNew.NombreSacs = itemNew.NombreSacs - itemOld.NombreSacs;
                        //}
                    }
                    if (itemNew.NbreSacsTotalLivraison != (nbr + itemNew.NombreSacs))
                    {
                        itemNew.NombreSacs = itemNew.NbreSacsTotalLivraison - nbr;
                    }                    
                        
                }

                Store mStoreMelange = X.GetCmp<Store>("storeListDeliveriesProd");
                mStoreMelange.RemoveAll();
                mStoreMelange.Add(newListe.Where(n => n.NombreSacs > 0));
            }
           
            return this.Direct();
        }
        public ActionResult OnRefreshForProduction(string ItemCertification, string ItemLivraisonType, string ItemPeriodStart, string ItemPeriodEnd)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListDeliveriesProd");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCertification"   ,ItemCertification),
                                    new Ext.Net.Parameter("ItemLivraisonType"   ,ItemLivraisonType),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd)                                    
                                });
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - Blending : Livraisons disponibles : OnRefresh",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        public ActionResult OnPrintProductionSheet(string ordreProductionID)
        {
            string BaseUrl = "";            
            try
            {
                if (string.IsNullOrEmpty(ordreProductionID))
                    return this.Direct();

                CompositionUsinage mComposition = new CompositionUsinage();
                bool result = mComposition.fnGetByProduction(Guid.Parse(ordreProductionID));
                if (result && mComposition.Statut != "AP")
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Delivery Blending - Ordre De Production",
                        Message = "Delivery Blending not Approuvé",
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
            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'ProductionSheet{0}', '{1}/OrdreProduction/ViewProductionSheet?id={0}', this, 'Production Sheet','')", ordreProductionID, BaseUrl));
        }

        public ActionResult ViewProductionSheet(string id)
        {
            try
            {
                rptFicheDeProduction report = new rptFicheDeProduction();

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["paramID"].Value = id;                
                
                ViewData["Report"] = report;
                return View("ViewReportResult", ViewData = ViewData);                
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Ordre De Production : Print Production Sheet",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }                    
            return View();
        }

        public ActionResult OnPrintProductionList()
        {           
            return new Ext.Net.MVC.PartialViewResult { ViewName = "OrdreProduction_Print", ViewData = ViewData };
        }

        public ActionResult PrintList()
        {
            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            try
            {                
                XtraReport report = null;

                report = new rptOrdreProductionList() as XtraReport;

                report.DataSource = DevExpressReportDs.SetDataSource(report);                

                report.Parameters["paramBlendingAgent"].Value = int.Parse(X.GetCmp<ComboBox>("cmbMelangeur").SelectedItem.Value);
                report.Parameters["paramBlendingAgentText"].Value = X.GetCmp<ComboBox>("cmbMelangeur").SelectedItem.Text;

                report.Parameters["paramCertification"].Value = int.Parse(X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Value);
                report.Parameters["paramCertificationText"].Value = X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Text;

                report.Parameters["paramShift"].Value = int.Parse(X.GetCmp<ComboBox>("cmbQuart").SelectedItem.Value);
                report.Parameters["paramShiftText"].Value = X.GetCmp<ComboBox>("cmbQuart").SelectedItem.Text;

                report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateDebut").RawText);
                report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateFin").RawText);

                report.Parameters["paramStatut"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Value.ToString();
                report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Text;
                report.Parameters["paramOtherStatut"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Value == "CA" ? 1 : 0;

                Session["report"] = report;

                return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/OrdreProduction/ViewList', this, 'Ordre De Production List',''),App.OrdreProduction_Print.doClose()", Guid.NewGuid(), BaseUrl));
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Ordre De Production : Data Validation",
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