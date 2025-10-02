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
    public class FicheLotCertifieController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";               
        
        // GET: FicheLotCertifie
        public ActionResult Index()
        {
            string campagne = new Parametres(0).Campagne;

            X.GetCmp<ComboBox>("cmbFiltreCampagne").SetValue(campagne);

            string StartDate = DateTime.Now.AddDays(-30).ToShortDateString();
            string EndDate = DateTime.Now.ToShortDateString();

            X.GetCmp<DateField>("txtFiltreDateDebut").RawText = StartDate;
            X.GetCmp<DateField>("txtFiltreDateFin").RawText = EndDate;

            X.GetCmp<FormPanel>("FicheLotCertifieCP").SetTitle("Campagne : " + campagne + ", Du : " + StartDate + " Au : " + EndDate);

            #region Set Function's Access

            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{4CCED212-3E56-456A-B748-80E9AB5576D9}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{214185A4-9E18-40A6-A7A5-B12CC090F98A}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{0EB9165A-88BD-4557-B969-57A92238B0C3}")))
                X.GetCmp<MenuItem>("mnuPrintList").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintList").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{7B32C6EE-0D64-4F78-B596-FD950183FA1F}")))
                X.GetCmp<MenuItem>("mnuExport").Enable();
            else
                X.GetCmp<MenuItem>("mnuExport").Disable();           

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{516A8B64-9691-4D7E-8B02-DBD455F55209}")))
                X.GetCmp<Hidden>("FchiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("FchiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{781EAE69-C305-4B95-A66D-47F4006E1A7D}")))
                X.GetCmp<Hidden>("FchiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("FchiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{46909E6B-8741-43E9-A5AF-B303E6166216}")))
                X.GetCmp<Hidden>("FchiddenPermPrintSheet").SetValue(true);
            else
                X.GetCmp<Hidden>("FchiddenPermPrintSheet").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{23879F26-BB05-4194-8A4F-2466A38190C5}")))
                X.GetCmp<Hidden>("FchiddenPermApprove").SetValue(true);
            else
                X.GetCmp<Hidden>("FchiddenPermApprove").SetValue(false);
            #endregion

            return View();
        }

        public ActionResult LoadFicheLotCertifie()
        {
            List<DataPersist> myListe = new FicheLotCertifie().fnSelect("-1",null, null,"AP");            
            return this.Store(myListe);

        }

        public ActionResult onAdd()
        {
            FicheLotCertifieViewModel mclass = new FicheLotCertifieViewModel();

            mclass._FicheLotCertifie = new FicheLotCertifie();
            mclass._DefaultCampagne = new Parametres(0).Campagne;            
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFicheLotCertifie", Model = mclass, };

        }

        public ActionResult onEdit(string ItemSelected)
        {
            FicheLotCertifieViewModel mclass = new FicheLotCertifieViewModel();            
            mclass._FicheLotCertifie = new FicheLotCertifie();
            mclass._FicheLotCertifie = JSON.Deserialize<FicheLotCertifie>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFicheLotCertifie", Model = mclass, };
        }

        public ActionResult onApprove(string ItemSelected)
        {
            FicheLotCertifieViewModel mclass = new FicheLotCertifieViewModel();
            mclass._FicheLotCertifie = new FicheLotCertifie();
            mclass._FicheLotCertifie = JSON.Deserialize<FicheLotCertifie>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFicheLotCertifie", Model = mclass, };
        }

        public ActionResult onConsult(string ItemSelected)
        {
            FicheLotCertifieViewModel mclass = new FicheLotCertifieViewModel();
            mclass._FicheLotCertifie = new FicheLotCertifie();
            mclass._FicheLotCertifie = JSON.Deserialize<FicheLotCertifie>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._DefaultCampagne = new Parametres(0).Campagne;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;            
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormFicheLotCertifie", Model = mclass, };
        }        

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("FicheLotCertifieCP");
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

            var mListe = (new FicheLotCertifie()).fnSelect(ItemCampagne, StartDate, EndDate, statut);

            return this.Store(mListe);
        }

        public ActionResult OnRefresh(string ItemCampagne, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeFicheLotCertifie");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemCampagne",ItemCampagne),
                                    new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatus",ItemStatus)
                                });

                X.GetCmp<FormPanel>("FicheLotCertifieCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Fiche de lot Certifié : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnSelectDelivery(string ItemCertification = "")
        {
            int? CertificationID = !string.IsNullOrEmpty(ItemCertification) ? int.Parse(ItemCertification) : (int?)null;
            ViewData["CertificationID"] = CertificationID;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FicheLotCertifie_SelectDeliveryNotes", ViewData = ViewData };

        }

        public ActionResult onCancel(string ItemSelected)
        {
            try
            {
                FicheLotCertifie pesee = JSON.Deserialize<FicheLotCertifie>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = pesee.fnGet(pesee.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Fiche de lot Certifié, loading failed.");

                pesee.UtilisateurModification = (string)Session["userName"];

                
                result = pesee.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Fiche de lot Certifié, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeFicheLotCertifie");

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
                    Title = "Fiche de lot Certifié : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult SubmitDeliveryNote(string selectedRows)
        {
            List<FicheLotCertifieLivraison> mLotLivraisons;
            
            mLotLivraisons = JSON.Deserialize<List<FicheLotCertifieLivraison>>(selectedRows, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            mLotLivraisons.ToList().ForEach(c => { c.IsNew = true; c.IsNewInList = true; c.mIcon = 2; c.NombreSacsProduction = c.NombreSacsLivraison; }); 

            //foreach (BonDeLivraison item in mListe)
            //{
            //    mClass = new FicheLotCertifieLivraison();
            //    mClass.BonDeLivraison = new BonDeLivraison();
            //    mClass.BonDeLivraison.Livraison = new Livraison();
            //    mClass.BonDeLivraison.Livraison.Fournisseur = new Fournisseur();
            //    mClass.ID = item.ID;
            //    mClass.BonDeLivraison.ID = item.ID;
            //    mClass.BonDeLivraison.Numero = item.Numero;
            //    mClass.BonDeLivraison.Livraison.Numero = item.Livraison.Numero;
            //    mClass.BonDeLivraison.Livraison.DateLivraison = item.Livraison.DateLivraison;
            //    mClass.BonDeLivraison.Livraison.Fournisseur.Nom = item.Livraison.Fournisseur.Nom;
            //    mClass.IsNew = true;
            //    mClass.IsNewInList = true;
            //    mLotLivraisons.Add(mClass);
            //}            

            Store mStore = X.GetCmp<Store>("storeListeSelectedLivraisons");
            mStore.Add(mLotLivraisons);

            X.GetCmp<Window>("FicheLotCertifie_SelectDeliveryNotes").Close();
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

        public ActionResult LoadListOfAvailableDeliveryNotes(string ItemPeriodStart, string ItemPeriodEnd, string ItemFournisseur, string ItemCertification)
        {
            try
            {
                int fournisseurID = ItemFournisseur == "" ? -1 : int.Parse(ItemFournisseur);
                int certificationID = ItemCertification == "" ? -1 : int.Parse(ItemCertification);
                DateTime? StartDate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
                DateTime? EndDate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());
                //var mListe = (new FicheLotCertifieLivraison()).fnSelectAvailableForWeighing(fournisseurID, StartDate, EndDate, certificationID);
                var mListe = (new FicheLotCertifieLivraison()).fnSelectAvailableForCertif(fournisseurID, StartDate, EndDate, certificationID);
                return this.Store(mListe);
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Fiche de lot Certifié : Data Validation",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
            
        }

        public ActionResult GetDeliveries(string ItemFiche)
        {
            try
            {
                Guid ficheID = ItemFiche == "" ? Guid.Empty : Guid.Parse(ItemFiche);
                var mListe = (new FicheLotCertifieLivraison()).fnSelect(ficheID);
                return this.Store(mListe);
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Fiche de lot Certifié : Data Validation",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }            
        }

        public ActionResult GetDeliveriesForWeighing(string ItemFiche)
        {
            try
            {
                Guid ficheID = ItemFiche == "" ? Guid.Empty : Guid.Parse(ItemFiche);
                var mListe = (new FicheLotCertifieLivraison()).fnSelect(ficheID);
                return this.Store(mListe);
            }
            catch (Exception Ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Fiche de lot Certifié : Data Validation",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
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


        public ActionResult OnRefreshForAvailableDeliveryNotes(string ItemFournisseur, string ItemPeriodStart, string ItemPeriodEnd, string ItemCertification)
        {
            Store mstore = X.GetCmp<Store>("storeListDeliveryNotes");

            mstore.Reload();

            mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemFournisseur"   ,ItemFournisseur),
                                    new Ext.Net.Parameter("ItemPeriodStart"   ,ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd"     ,ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemCertification"     ,ItemCertification)
                                });

            string title = X.GetCmp<FormPanel>("CriteriaPanelDeliveryNote").Title;

            title += " Fournisseur : " + X.GetCmp<ComboBox>("cmbFournisseurFD").SelectedItem.Text;

            title += ", From " + X.GetCmp<DateField>("dtpStartDateFD").RawText.ToString() + " to " + X.GetCmp<DateField>("dtpEndDateFD").RawText.ToString();

            X.GetCmp<FormPanel>("CriteriaPanelDeliveryNote").Title = title;

            // collapse criterias areas
            //X.GetCmp<FormPanel>("CriteriaPanelDeliveryNote").Collapse(Direction.Top, false);

            return this.Direct();
        }


        public ActionResult SubmitFormMethod(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                FicheLotCertifie fiche = new FicheLotCertifie();
                FicheLotCertifieLivraison ficheLivraisons;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    fiche.IsNew = true;
                else
                {
                    fiche.IsNew = false;

                    fiche.fnGet(Guid.Parse(GetFormValue("TxtFicheID")));

                    if (fiche == null || fiche.ID == Guid.Empty)
                        throw new Exception("SubmitFormMethod : Sheet load failed.");
                }

                bool result = false;
                bool resultLivraison = true;
                
                fiche = MapFormToObject(fiche);
                string NumeroLots = "";
                List<FicheLotCertifieLivraison> listeLivraisons = JSON.Deserialize<List<FicheLotCertifieLivraison>>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });                
                if (listeLivraisons.Count > 0)
                {
                    _db = fiche.db();
                    mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                    result = fiche.fnUpdate(mtran);
                    if (result)
                    {
                       
                        foreach (FicheLotCertifieLivraison item in listeLivraisons)
                        {
                            if (item.IsNew == true)
                            {
                                ficheLivraisons = new FicheLotCertifieLivraison();
                                ficheLivraisons.FicheLotCertifie = new FicheLotCertifie();
                                ficheLivraisons.BonDeLivraison = new BonDeLivraison();
                                ficheLivraisons.SetDataSource(_db);

                                ficheLivraisons.FicheLotCertifie.ID = fiche.ID;
                                ficheLivraisons.BonDeLivraison.ID = item.BonDeLivraison.ID;
                                ficheLivraisons.DateFiche = DateTime.Now;
                                ficheLivraisons.NombreSacsProduction = (int)item.NombreSacsLivraison;
                                ficheLivraisons.IsNew = item.IsNew;
                                NumeroLots += (item.LivraisonID + ", ");
                                ficheLivraisons.UtilisateurCreation = (string)Session["userName"];
                                ficheLivraisons.UtilisateurModification = (string)Session["userName"];

                                resultLivraison = ficheLivraisons.fnUpdate(mtran);
                                
                            }
                            if (!resultLivraison)
                            {
                                break;
                            }
                        }
                        
                    }
                    else
                        _db.RollBackTransaction(mtran);

                    if (!resultLivraison)
                    {
                        _db.RollBackTransaction(mtran);
                    }
                    
                    if (result && resultLivraison)
                    {                        
                        _db.CommitTransaction(mtran);
                        Store mstore = X.GetCmp<Store>("storeListeFicheLotCertifie");
                        if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                        {
                            fiche.NumeroLivraison = NumeroLots;
                            mstore.Insert(0, fiche);
                            X.GetCmp<RowSelectionModel>("rowFicheLotCertifie").Select(0);
                        }
                        else
                        {
                            ModelProxy mProxy = mstore.GetById(fiche.ID);

                            mProxy.BeginEdit();

                            mProxy.Set(fiche);

                            mProxy.Commit();

                            mProxy.EndEdit();
                        }

                        X.GetCmp<Window>("FormFicheLotCertifie").Close();
                    }
                }
                else
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Fiche de lot Certifié : Data Validation",
                        Message = "Please Select Livraison(s) before",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                }                
            }
            catch (Exception ex)
            {
                _db.RollBackTransaction(mtran);
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Fiche de lot Certifié : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        public ActionResult ApproveLot()
        {
            //DataSource _db = new DataSource();
            //DataTransaction mTran = new DataTransaction();

            try
            {
                FicheLotCertifie fiche = new FicheLotCertifie();
                FicheLotCertifieLivraison fLivraison = new FicheLotCertifieLivraison();

                fiche.IsNew = false;                
                bool resultMouvement = false;
                bool result = fiche.fnGet(Guid.Parse(GetFormValue("TxtFicheID")));                

                if (fiche == null || fiche.ID == Guid.Empty)
                    throw new Exception("onApprove: Fiche de lot Certifié");

                var mListe = fLivraison.fnSelect(fiche.ID);

                fiche.UtilisateurModification = (string)Session["userName"];

                //_db = fiche.db();
                //mTran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = fiche.fnApprove();

                #region Generartion Mouvement
                //if (result)
                //{
                //    MouvementStock mMouvement;
                //    foreach (FicheLotCertifieLivraison item in mListe)
                //    {
                //        mMouvement = new MouvementStock();
                //        mMouvement.mCampagne = new Campagne();
                //        mMouvement.Exportateur = new Exportateur();
                //        mMouvement.SacType = new SacType();
                //        mMouvement.TypeElementStock = new TypeElementStock();
                //        mMouvement.Certification = new Certification();
                //        mMouvement.Magasin = new Magasin();
                //        mMouvement.Emplacement = new Emplacement();
                //        mMouvement.MouvementStockType = new MouvementStockType();
                //        Parametres mParam = new Parametres(0);
                //        mMouvement.SetDataSource(_db);
                //        mMouvement.mCampagne.Designation = item.BonDeLivraison.Livraison.Campagne.Designation;
                //        mMouvement.Exportateur.ID = item.BonDeLivraison.Livraison.Exportateur.ID;
                //        mMouvement.DateMouvement = DateTime.Now;

                //        if (item.BonDeLivraison.Livraison.SacType != null)
                //            mMouvement.SacType.ID = item.BonDeLivraison.Livraison.SacType.ID;
                //        else
                //            mMouvement.SacType = null;

                //        mMouvement.ObjetEnStock = item.BonDeLivraison.ID;
                //        mMouvement.ObjetEnStockType = mParam.LivraisonTypeElementStock;
                //        //mMouvement.TypeElementStock.ID = mParam.AjustementStockType;
                //        mMouvement.MouvementStockType.ID = mParam.ConstitutionLotCertifie;

                //        if (item.BonDeLivraison.Livraison.Certification != null)
                //            mMouvement.Certification.ID = item.BonDeLivraison.Livraison.Certification.ID;
                //        else
                //            mMouvement.Certification = null;

                //        mMouvement.Sens = "S";
                //        mMouvement.Quantite = item.NombreSacsProduction;
                //        mMouvement.PoidsBrut = ((item.NombreSacsProduction * item.BonDeLivraison.PoidsBrut) / item.BonDeLivraison.NbreSacs);
                //        mMouvement.TarePalettes = ((item.NombreSacsProduction * item.BonDeLivraison.TarePalettes) / item.BonDeLivraison.NbreSacs);
                //        mMouvement.TareSacs = ((item.NombreSacsProduction * item.BonDeLivraison.TareSacs) / item.BonDeLivraison.NbreSacs);
                //        mMouvement.PoidsNetLivre = mMouvement.PoidsBrut - mMouvement.TareSacs - mMouvement.TarePalettes;
                //        mMouvement.PoidsNetAccepte = ((item.NombreSacsProduction * item.BonDeLivraison.PoidsNetAccepte) / item.BonDeLivraison.NbreSacs);
                //        mMouvement.Retention = 0;
                //        mMouvement.Magasin.ID = mParam.MagasinTV;
                //        mMouvement.Emplacement.ID = mParam.EmplacementParDefaut;                        
                //        mMouvement.Reference1 = item.LivraisonID;
                //        mMouvement.Reference2 = item.NumeroFicheLot;
                //        mMouvement.Commentaire = "Constitution de Lot Certifie";
                //        mMouvement.Statut = "NA";
                //        mMouvement.UtilisateurCreation = (string)Session["userName"];
                //        mMouvement.UtilisateurModification = (string)Session["userName"];

                //        resultMouvement = mMouvement.fnUpdate(mTran);
                //        if (!resultMouvement)
                //            _db.RollBackTransaction(mTran);
                //    }
                //}
                #endregion
                if (result)
                {
                   // _db.CommitTransaction(mTran);

                    Store mstore = X.GetCmp<Store>("storeListeFicheLotCertifie");

                    ModelProxy mProxy = mstore.GetById(fiche.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(fiche);

                    mProxy.Commit();

                    mProxy.EndEdit();

                    X.GetCmp<Window>("FormFicheLotCertifie").Close();
                }                                
            }
            catch (Exception ex)
            {                
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Fiche de lot Certifié : Approuver",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        private FicheLotCertifie MapFormToObject(FicheLotCertifie mClass)
        {
            Campagne campagne = new Campagne();
            campagne.Designation = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text.ToString();
            mClass.Campagne = campagne;

            Certification certification = new Certification();                       
            certification.ID = int.Parse(GetFormValue("cmbCertification"));
            certification.Designation = X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Text;            
            mClass.Certification = certification;           

            mClass.DateFiche = DateTime.Parse(X.GetCmp<DateField>("txtDateFiche").RawText.ToString()).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);            
            mClass.Statut = "NA";
           
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }
        

        public ActionResult RemoveDelivery(string ItemSelected)
        {
            try
            {
                FicheLotCertifieLivraison mDelivery = JSON.Deserialize<FicheLotCertifieLivraison>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });                

                if (mDelivery.ID != Guid.Empty)
                {
                    Store mstore = X.GetCmp<Store>("storeListeSelectedLivraisons");

                    if (mDelivery.IsNew)
                    {
                        ModelProxy _proxy = mstore.GetById(mDelivery.ID);
                        _proxy.Drop();
                        return this.Direct();
                    }
                    else
                    {                        
                        mDelivery.fnGet(mDelivery.ID);
                        if (mDelivery == null || mDelivery.ID == Guid.Empty)
                            throw new Exception("RemoveDelivery : Retirer Delivery failed.");

                        mDelivery.UtilisateurModification = (string)Session["userName"];

                        bool result = mDelivery.fnRemove();

                        if (result)
                        {
                            ModelProxy mProxy = mstore.GetById(mDelivery.ID);
                            mProxy.Drop();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Fiche de lot Certifié - : Retirer Delivery",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnSelectProduction()
        {
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FicheLotCertifie_Production" };
        }

        public ActionResult OnPrintCertifiedLotSheet(string IdFiche)
        {
            string BaseUrl = "";            
            try
            {
                if (string.IsNullOrEmpty(IdFiche))
                    return this.Direct();

                FicheLotCertifie mPesee = new FicheLotCertifie();
                bool result = mPesee.fnGet(Guid.Parse(IdFiche));
                if (result && mPesee.Statut != "AP")
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Fiche de lot Certifié",
                        Message = "Fiche de lot Certifié Non Approuvé",
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
                    Title = "Fiche de lot Certifié",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'Ticket{0}', '{1}/FicheLotCertifie/ViewResult?id={0}', this, 'Fiche de lot Certifié','')", IdFiche, BaseUrl));
        }


        public ActionResult ViewResult(string id)
        {
            try
            {
                rptFicheLotCertifie report = new rptFicheLotCertifie();

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["paramID"].Value = id;                
                ViewData["Report"] = report;
                return View("ViewReportResult", ViewData = ViewData);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Fiche de lot Certifié : Report",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }                            
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