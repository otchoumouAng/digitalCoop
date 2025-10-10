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
    public class MelangeProductionController : BaseController
    {
        // GET: MelangeProduction

        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";
        public ActionResult Index()
        {
            #region Set Function's Access

            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{F58843D5-B402-4F1C-8CCF-370C96675D93}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{43C40582-E0B9-4BA0-98FB-6C0C1FB4330C}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{39E4462E-EBD7-4074-9C57-79AD9C22E54D}")))
                X.GetCmp<MenuItem>("mnuPrintListOfBlending").Enable();
            else
                X.GetCmp<MenuItem>("mnuPrintListOfBlending").Disable();

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{46583FBB-9E05-4C2A-A144-2DECFCE7493A}")))
                X.GetCmp<MenuItem>("mnuExportListOfBlending").Enable();
            else
                X.GetCmp<MenuItem>("mnuExportListOfBlending").Disable();            

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{07612D9B-A9E7-4CF6-B59D-1E8DFA1B232A}")))
                X.GetCmp<Hidden>("MlhiddenPermModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("MlhiddenPermModifier").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{486E438A-F682-4951-93DF-19A76FCC0916}")))
                X.GetCmp<Hidden>("MlhiddenPermApprove").SetValue(true);
            else
                X.GetCmp<Hidden>("MlhiddenPermApprove").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{30614BC3-D86D-4A6E-8DBE-6E956027D475}")))
                X.GetCmp<Hidden>("MlhiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("MlhiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{C62824CE-C43E-4FD2-969B-61FA95575730}")))
                X.GetCmp<Hidden>("MlhiddenPermPrintSheet").SetValue(true);
            else
                X.GetCmp<Hidden>("MlhiddenPermPrintSheet").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{6CBDCE7D-6EF5-4D06-B2F3-7549647EBC89}")))
                X.GetCmp<Hidden>("MlhiddenPermSupModifier").SetValue(true);
            else
                X.GetCmp<Hidden>("MlhiddenPermSupModifier").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{e226a82d-3b50-4a9c-bcf4-db72b78f5b89}")))
                X.GetCmp<Hidden>("MlhiddenPermCancelApproval").SetValue(true);
            else
                X.GetCmp<Hidden>("MlhiddenPermCancelApproval").SetValue(false);
            #endregion

            return View();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("MelangeProductionCP");
            mform.ToggleCollapse();            
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            if (string.IsNullOrEmpty(ItemPeriodStart) && string.IsNullOrEmpty(ItemPeriodEnd) && string.IsNullOrEmpty(ItemStatus))
            {
                return this.Store(null);
            }
            else
            {
                DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
                DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

                string status = "-1";
                if (!string.IsNullOrEmpty(ItemStatus))
                {
                    status = ItemStatus;
                }

                var mListe = (new CompositionUsinage()).fnSelect(startdate, enddate, status);

                return this.Store(mListe);
            }
        }

        public ActionResult OnRefresh(string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store store = X.GetCmp<Store>("storeListeCompositionUsinage");
                store.Reload();
                store.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemPeriodStart"     ,ItemPeriodStart),
                                new Ext.Net.Parameter("ItemPeriodEnd"           ,ItemPeriodEnd),
                                new Ext.Net.Parameter("ItemStatus"           ,ItemStatus)
                            });

                X.GetCmp<FormPanel>("MelangeProductionCP").Collapsed = true;

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Delivery Blending : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }


        public ActionResult onAdd()
        {
            Parametres mParam = new Parametres(0);            
            CompositionUsinageViewModel mclass = new CompositionUsinageViewModel();
            mclass._DefaultLaboratoire = mParam.LaboTv;
            mclass._CompositionUsinage = new CompositionUsinage();
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMelangeProduction", Model = mclass };
        }

        public ActionResult onEdit(string ItemSelected)
        {
            CompositionUsinageViewModel mclass = new CompositionUsinageViewModel();            
            CompositionUsinage composition = JSON.Deserialize<CompositionUsinage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._DefaultLaboratoire = (int?)null;
            mclass._CompositionUsinage = composition;            
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMelangeProduction", Model = mclass };
        }

        public ActionResult onUpdateProduction(string ItemSelected)
        {
            CompositionUsinageViewModel mclass = new CompositionUsinageViewModel();
            CompositionUsinage composition = JSON.Deserialize<CompositionUsinage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._DefaultLaboratoire = (int?)null;
            mclass._CompositionUsinage = composition;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDetailMelangeProduction", Model = mclass };
        }

        public ActionResult onConsult(string ItemSelected)
        {
            CompositionUsinageViewModel mclass = new CompositionUsinageViewModel();
            CompositionUsinage composition = JSON.Deserialize<CompositionUsinage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            mclass._CompositionUsinage = composition;
            mclass._DefaultLaboratoire = (int?)null;
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMelangeProduction", Model = mclass };

        }

        public ActionResult onApprove(string ItemSelected)
        {
            CompositionUsinageViewModel mclass = new CompositionUsinageViewModel();
            CompositionUsinage composition = JSON.Deserialize<CompositionUsinage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._DefaultLaboratoire = (int?)null;
            mclass._CompositionUsinage = composition;            
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMelangeProduction", Model = mclass };

        }

        public ActionResult SubmitFormMethod(string storerows)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();

            try
            {
                CompositionUsinage composition = new CompositionUsinage();
                CompositionUsinageLivraison cLivraison = new CompositionUsinageLivraison();
                DateTime dateComposition = new DateTime();

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

                    dateComposition = composition.DateComposition;
                }

                _db = composition.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                composition = MapFormToObjectBlending(composition);

                if (composition.IsNew == false && (dateComposition.Date == composition.DateComposition.Date))
                    composition.DateComposition = dateComposition;

                composition.UtilisateurCreation = (string)Session["userName"];
                composition.UtilisateurModification = (string)Session["userName"];
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

                        Store mstore = X.GetCmp<Store>("storeListeCompositionUsinage");
                        if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                        {
                            mstore.Insert(0, composition);
                            X.GetCmp<RowSelectionModel>("rowCompositionUsinage").Select(0);
                        }
                        else
                        {
                            ModelProxy mProxy = mstore.GetById(composition.ID);

                            mProxy.BeginEdit();

                            mProxy.Set(composition);

                            mProxy.Commit();

                            mProxy.EndEdit();
                        }

                        X.GetCmp<Window>("FormMelangeProduction").Close();
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

        public ActionResult UpdateProduction()
        {
            try
            {
                CompositionUsinage composition = new CompositionUsinage();                
                DateTime dateComposition = new DateTime();

                bool result = true;                                

                composition.IsNew = false;
                composition.fnGet(Guid.Parse(GetFormValue("txtCompositionID")));

                if (composition == null || composition.ID == Guid.Empty)
                    throw new Exception("SubmitComposition : Blending load failed.");

                dateComposition = composition.DateComposition;                                
                composition = MapFormToObjectBlending(composition);

                if (dateComposition.Date == composition.DateComposition.Date)
                    composition.DateComposition = dateComposition;

                result = composition.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeCompositionUsinage");
                    composition.Statut = "AP";
                    ModelProxy mProxy = mstore.GetById(composition.ID);
                    mProxy.BeginEdit();
                    mProxy.Set(composition);
                    mProxy.Commit();
                    mProxy.EndEdit();

                    X.GetCmp<Window>("FormDetailMelangeProduction").Close();
                }
            }
            catch (Exception ex)
            {                
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

        private CompositionUsinage MapFormToObjectBlending(CompositionUsinage mClass)
        {
            OrdreProduction production = new OrdreProduction();
            production.ID = Guid.Parse(X.GetCmp<ComboBox>("cmbProduction").SelectedItem.Value);
            production.NumeroProduction = X.GetCmp<ComboBox>("cmbProduction").SelectedItem.Text;
            mClass.OrdreProduction = production;

            Laboratoire laboratoire = new Laboratoire();
            laboratoire.ID = int.Parse(GetFormValue("cmbLaboratoire"));
            laboratoire.Designation = X.GetCmp<ComboBox>("cmbLaboratoire").SelectedItem.Text.ToString();
            mClass.Laboratoire = laboratoire;

            LotType lType = new LotType();
            lType.ID = int.Parse(GetFormValue("cmbTypeLot"));
            lType.Designation = X.GetCmp<ComboBox>("cmbTypeLot").SelectedItem.Text.ToString();
            mClass.LotType = lType;

            mClass.DateComposition = DateTime.Parse(X.GetCmp<DateField>("txtDateComposition").RawText).Add(DateTime.Now.TimeOfDay);
            mClass.Commentaire = X.GetCmp<TextArea>("txtCommentaire").Text;
            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }

        public ActionResult onAddDelivery(string rowsDeliveriesInListe = "")
        {
            CompositionUsinageLivraisonViewModel mclass = new CompositionUsinageLivraisonViewModel();

            //OrdreProduction ordreproduction = JSON.Deserialize<OrdreProduction>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });
            mclass._CompositionUsinageLivraison = new CompositionUsinageLivraison();

            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            Parametres mParam = new Parametres(0);
            mclass._Campagne = mParam.Campagne;        
            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormMelangeLivraison", Model = mclass, };

        }

        public ActionResult OnSelectProduction()
        {
            return new Ext.Net.MVC.PartialViewResult { ViewName = "Melange_SelectProduction" };
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


        public ActionResult OnRefreshForProduction(string ItemQuart, string ItemMelangeur, string ItemCertification, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatut, string ItemCampagne)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListDeliveriesProd");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemQuart",ItemQuart),
                                    new Ext.Net.Parameter("ItemMelangeur",ItemMelangeur),
                                    new Ext.Net.Parameter("ItemCertification",ItemCertification),
                                    new Ext.Net.Parameter("ItemPeriodStart",ItemPeriodStart),
                                    new Ext.Net.Parameter("ItemPeriodEnd",ItemPeriodEnd),
                                    new Ext.Net.Parameter("ItemStatut",ItemStatut),
                                    new Ext.Net.Parameter("ItemCampagne",ItemCampagne)
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

                            X.GetCmp<TextField>("cmbCampagne").SetValue(mclass.CampagneAsString);
                            X.GetCmp<TextField>("cmbQuart").SetValue(mclass.QuartAsString);
                            X.GetCmp<TextField>("cmbMelangeur").SetValue(mclass.MelangeurAsString);
                            X.GetCmp<TextField>("cmbChefQuart").SetValue(mclass.ChefQuartAsString);
                            X.GetCmp<TextField>("txtDateProduction").SetValue(mclass.DateProductionAstring);

                            X.GetCmp<Window>("Melange_SelectProduction").Close();
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
                        X.GetCmp<TextField>("cmbCampagne").SetValue(mclass.CampagneAsString);
                        X.GetCmp<TextField>("cmbQuart").SetValue(mclass.QuartAsString);
                        X.GetCmp<TextField>("cmbChefQuart").SetValue(mclass.ChefQuartAsString);
                        X.GetCmp<TextField>("cmbMelangeur").SetValue(mclass.MelangeurAsString);
                        X.GetCmp<TextField>("txtDateProduction").SetValue(mclass.DateProductionAstring);

                        X.GetCmp<Window>("Melange_SelectProduction").Close();
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
                    item.TareSacs = Math.Round((item.NombreSacs * item.BonDeLivraison.TareSacs) / item.NbreSacsTotalLivraison);
                    item.TarePalettes = Math.Round((item.NombreSacs * item.BonDeLivraison.TarePalettesAjustee) / item.NbreSacsTotalLivraison);
                    item.PoidsLivre = item.PoidsBrut - item.TareSacs - item.TarePalettes;
                    item.Retention = Math.Round((item.NombreSacs * item.BonDeLivraison.TotalRetention) / item.NbreSacsTotalLivraison);
                    item.PoidsNet = Math.Round((item.NombreSacs * item.BonDeLivraison.PoidsNetAccepte) / item.NbreSacsTotalLivraison);
                }
            }
            return cLivraison;
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
                    Title = "Production Order - Blending : Retirer Delivery",
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
                    Ffa += (comp.Ffa * (double)comp.PoidsNet);
                    Moisi += (comp.Moisi * (double)comp.PoidsNet);
                    //MatieresEtrangeres += (comp.MatiereEtrangere * (double)comp.PoidsNet);
                    MatieresEtrangeres += comp.MatiereEtrangere;
                    Humidite += (comp.Humidite * (double)comp.PoidsNet);
                    weevil += (comp.Mite * (double)comp.PoidsNet);
                    slaty += (comp.Ardoisee * (double)comp.PoidsNet);
                    //sieving += (comp.Sievings * (double)comp.PoidsNet);
                    sieving += comp.Sievings;
                    NombreLivraisons += 1;
                    poidsnetAccepte += (double)comp.PoidsNet;
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
                    composition.MatiereEtrangere = Math.Round(MatieresEtrangeres,2);
                    composition.Mite = Math.Round(weevil / poidsnetAccepte, 2);
                    composition.Ardoisee = Math.Round(slaty / poidsnetAccepte, 2);
                    composition.Sievings = Math.Round(sieving,2);
                    composition.Humidite = Math.Round(Humidite / poidsnetAccepte, 2);
                    listeComposition.Add(composition);
                }
            }
            mStoreMelange.Add(listeComposition);
            return this.Direct();
            //return this.Store(listeComposition.OrderBy(ord => ord.Melange.Designation));
        }

        public ActionResult ApproveComposition(string storerows)
        {
            try
            {
                CompositionUsinage composition = new CompositionUsinage();

                bool result = true;

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.Approve)
                {
                    composition.IsNew = false;

                    composition.fnGet(Guid.Parse(GetFormValue("txtCompositionID")));

                    if (composition == null || composition.ID == Guid.Empty)
                        throw new Exception("ApproveComposition : Blending load failed.");
                }

                composition.Approbateur = (string)Session["userName"];
                composition.Statut = "AP";
                result = composition.fnApprove();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeCompositionUsinage");
                    ModelProxy mProxy = mstore.GetById(composition.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(composition);

                    mProxy.Commit();

                    mProxy.EndEdit();
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

        public ActionResult OnPrintBlendingSheet(string MelangeID)
        {
            string BaseUrl = "";
            try
            {
                if (string.IsNullOrEmpty(MelangeID))
                    return this.Direct();

                CompositionUsinage mComposition = new CompositionUsinage();
                bool result = mComposition.fnGet(Guid.Parse(MelangeID));
                if (result && mComposition.Statut != "AP")
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Delivery Blending - Sheet",
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
                    Title = "Blending Sheet",
                    Message = Ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
                return this.Direct();
            }
            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'BlendingSheet{0}', '{1}/MelangeProduction/ViewBlendingSheet?id={0}', this, 'Blending Sheet','')", MelangeID, BaseUrl));
        }

        public ActionResult ViewBlendingSheet(string id)
        {
            try
            {
                rptFicheDeMelange report = new rptFicheDeMelange();

                report.DataSource = DevExpressReportDs.SetDataSource(report);
                report.Parameters["paramID"].Value = id;

                ViewData["Report"] = report;
                return View("ViewReportResult", ViewData = ViewData);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production Order : Print Production Sheet",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return View();
        }


        public ActionResult OnCancel(string ItemSelected)
        {
            try
            {
                CompositionUsinage composition = JSON.Deserialize<CompositionUsinage>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = composition.fnGet(composition.ID);

                if (!result)
                    throw new Exception("OnCancel : Delivery Blending loading failed.");

                composition.UtilisateurModification = (string)Session["userName"];

                if (composition.Desactive)
                    result = composition.fnActivate();
                else
                    result = composition.fnDeActivate();

                if (!result)
                    throw new Exception("OnCancel : Delivery Blending, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeCompositionUsinage");

                    ModelProxy mProxy = mstore.GetById(composition.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(composition);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Delivery Blending : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
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

        private int GetCritriasValue(string strComponent)
        {
            if (X.GetCmp<ComboBox>(strComponent) != null && X.GetCmp<ComboBox>(strComponent).SelectedItem != null && X.GetCmp<ComboBox>(strComponent).SelectedItem.Value != null)
                return int.Parse(X.GetCmp<ComboBox>(strComponent).SelectedItem.Value.ToString());
            else
                return -1;
        }

        private int GetCriteriaValue(string strComponent)
        {
            int value;

            if (!string.IsNullOrEmpty(strComponent) && int.TryParse(strComponent, out value))
                return value;
            else
                return -1;
        }

        #endregion


    }
}