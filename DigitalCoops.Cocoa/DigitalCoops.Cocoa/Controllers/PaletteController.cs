using DevExpress.XtraReports.UI;
using DigitalCoops.Cocoa.Models;
using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tms.Classes.Business;
using Tms.Classes.Business.stock;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Classes.Shared.Sales;
using Tms.Components.Data;
using Tms2017.MVC.Controllers;

namespace Cooperative.Controllers
{
    public class PaletteController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string TEST = "Default";

        // GET: Palette
        public ActionResult Index()
        {
            #region Set Function's Access

            string UserName = (string)Session["userName"];

            var mListe = new Fonction().fnGetUserFunctionsByModule(Guid.Parse("{34caff7a-0f0d-40ee-92b3-9de7f7d50b39}"), UserName);
            List<Fonction> mlisteFonctions = mListe.Cast<Fonction>().ToList();
            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{dd88803b-5f9c-41f2-881c-91ecedc61708}")))
                X.GetCmp<Button>("btnNew").Enable();
            else
                X.GetCmp<Button>("btnNew").Disable();



            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{dd88803b-5f9c-41f2-881c-91ecedc61708}")))
                X.GetCmp<Hidden>("arhiddenPermModify").SetValue(true);
            else
                X.GetCmp<Hidden>("arhiddenPermModify").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{ace59756-d945-4d53-92b1-49e1ee69520c}")))
                X.GetCmp<Hidden>("arhiddenPermDesactiver").SetValue(true);
            else
                X.GetCmp<Hidden>("arhiddenPermDesactiver").SetValue(false);

            if (mlisteFonctions.Select(f => f.ID).Contains(Guid.Parse("{30ee1bab-7d3c-4fcb-88a9-628db185f061}")))
                X.GetCmp<Hidden>("arhiddenPermApprove").SetValue(true);
            else
                X.GetCmp<Hidden>("arhiddenPermApprove").SetValue(false);

            #endregion

            return View();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("PaletteCP");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult Select(StoreRequestParameters parameters, int ItemProduitID, int ItemProduitTypeID, int ItemMarqueProduitID, int ItemActif, string ItemStatut)

        {
            // Get filter criteria values
            int produitId = ItemProduitID;
            int produitTypeId = ItemProduitTypeID;
            int marqueProduitId = ItemMarqueProduitID;
            int actif = ItemActif;
            string statut = string.IsNullOrEmpty(ItemStatut) ? "-1" : ItemStatut;


            // Get filtered list using all criteria
            var mListe = (new Palette()).fnSelect(produitId, produitTypeId, marqueProduitId, statut, actif);

            return this.Store(mListe);
        }

        public ActionResult SelectByOrdreDeProduction(Guid ItemOrdreDeFabricationID, string ItemTypeEtiquette = "-1", string ItemRePrint = "false", string ItemOverviewPrint = "false")

        {
            Store mStore = X.GetCmp<Store>("storePaletteListe");
            try
            {
                // Get filter criteria values
                Guid OrdreDeFabricationId = ItemOrdreDeFabricationID;
                string TypeEtiquette = ItemTypeEtiquette;
                string RePrint = ItemRePrint;
                string OverviewPrint = ItemOverviewPrint;


                // Get filtered list using all criteria
                var mListe = (new Palette()).ListPalettesForPrint(OrdreDeFabricationId, TypeEtiquette, RePrint, OverviewPrint);
                


                return this.Store(mListe);
            }

            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - Palette : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            //OnRefresh(mClass.ID.ToString());
            return this.Direct();

        }


        public ActionResult Select_ByOrdreDeProductionAndQAStatus(Guid ItemOrdreDeFabricationID)
        {
            Store mStore = X.GetCmp<Store>("storePaletteListe");
            try
            {
                // Get filter criteria values
                Guid OrdreDeFabricationId = ItemOrdreDeFabricationID;
                // Get filtered list using all criteria
                var mListe = (new Palette()).fnSelect_ByOrdreDeProductionAndQAStatus(OrdreDeFabricationId);
                return this.Store(mListe);
            }

            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Palette : QA Status",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            //OnRefresh(mClass.ID.ToString());
            return this.Direct();

        }


        public ActionResult OnRefresh(int ItemTypeProduitID, int ItemMarqueProduitID, int ItemActif, string ItemStatut)
        {
            try
            {


                Store mstore = X.GetCmp<Store>("storeListePalette");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                                {
                                    new Ext.Net.Parameter("ItemTypeProduitID",ItemTypeProduitID),
                                    new Ext.Net.Parameter("ItemMarqueProduitID",ItemMarqueProduitID),
                                    new Ext.Net.Parameter("ItemActif",ItemActif),
                                    new Ext.Net.Parameter("ItemStatut",ItemStatut)
                                });

                X.GetCmp<FormPanel>("PaletteCP").Collapse(Direction.Top, false);
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - Palette : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }



        [HttpPost]
        public ActionResult RemovePalette(string selectedIds)
        {
            try
            {
                var ids = JsonConvert.DeserializeObject<string[]>(selectedIds);

                if (ids?.Length > 0)
                {
                    foreach (string id in ids)
                    {
                        if (!string.IsNullOrWhiteSpace(id))
                        {
                            var palette = new Palette();
                            palette.fnRemove(Guid.Parse(id));
                        }
                    }

                    // Refresh the grid after successful deletion
                    Store store = X.GetCmp<Store>("storePaletteListe");
                    store.Reload();
                    X.Call("OverviewForm.resetButtons");
                }

                return this.Direct();
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Error",
                    Message = "An error occurred while deleting palettes: " + ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR
                });

                return this.Direct();
            }
        }


        [HttpPost]
        public ActionResult GeneratePalette(string ItemOrdreDeFabricationID, string ItemNbreEtiquetteA4Demande, string ItemNbreEtiquetteA5Demande)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ItemOrdreDeFabricationID))
                    return this.Direct();

                string userName = (string)Session["userName"];
                Guid ordreFabricationID = Guid.Parse(ItemOrdreDeFabricationID);

                int nbA4 = 0, nbA5 = 0;
                int.TryParse(ItemNbreEtiquetteA4Demande, out nbA4);
                int.TryParse(ItemNbreEtiquetteA5Demande, out nbA5);

                if (nbA4 == 0 && nbA5 == 0)
                {
                    X.MessageBox.Show(new MessageBoxConfig
                    {
                        Title = "Avertissement",
                        Message = "Impossible d'ajouter des palettes lorsque le nombre d'exemplaires (A4, A5) est égal à 0.",
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.WARNING
                    });
                    return this.Direct();
                }

                Store store = X.GetCmp<Store>("storePaletteListe");
                RowSelectionModel rowSelection = X.GetCmp<RowSelectionModel>("rowPalette");

                int insertIndex = 0;

                if (nbA4 > 0)
                {
                    Palette paletteA4 = new Palette();
                    paletteA4.UtilisateurCreation = userName;

                    if (paletteA4.fnGenerate(ordreFabricationID))
                    {
                        var dataA4 = paletteA4.fnSelectByID(paletteA4.ID, "A4").FirstOrDefault();
                        if (dataA4 != null)
                        {
                            store.Insert(insertIndex, dataA4);
                            rowSelection.Select(insertIndex);
                            insertIndex++;
                        }
                    }
                }

                if (nbA5 > 0)
                {
                    Palette paletteA5 = new Palette();
                    paletteA5.UtilisateurCreation = userName;

                    if (paletteA5.fnGenerate(ordreFabricationID))
                    {
                        var dataA5 = paletteA5.fnSelectByID(paletteA5.ID, "A5").FirstOrDefault();
                        if (dataA5 != null)
                        {
                            store.Insert(insertIndex, dataA5);
                            rowSelection.Select(insertIndex);
                            insertIndex++;
                        }
                    }
                }

                return this.Direct();
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Erreur",
                    Message = "Une erreur est survenue lors de la génération des palettes : " + ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR
                });

                return this.Direct();
            }
        }





        public ActionResult Load()
        {
            List<DataPersist> mList = new Palette().fnSelect();
            return this.Store(mList);
        }

        public ActionResult LoadActive()
        {
            List<DataPersist> mList = new Palette().fnSelect();
            return this.Store(mList);
        }

        public ActionResult LoadAll()
        {
            List<DataPersist> mList = new Palette().fnSelect();
            Palette mclass = new Palette();

            mclass.ID = System.Guid.NewGuid();
            mclass.Numero = "-1";
            mList.Insert(0, mclass);

            return this.Store(mList);

        }

        public ActionResult OnAdd()
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PaletteViewModel PaletteVm = new PaletteViewModel();

            PaletteVm._Palette = new Palette();
            PaletteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPalette", Model = PaletteVm };
        }

        public ActionResult OnEdit(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PaletteViewModel PaletteVm = new PaletteViewModel();

            PaletteVm._Palette = JSON.Deserialize<Palette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PaletteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPalette", Model = PaletteVm };

        }

        public ActionResult OnConsult(string ItemSelected)
        {
            X.GetCmp<Viewport>("TmsViewPort").Mask();

            PaletteViewModel PaletteVm = new PaletteViewModel();

            PaletteVm._Palette = JSON.Deserialize<Palette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
            PaletteVm._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormPalette", Model = PaletteVm };

        }

        public ActionResult OnApprove(string ItemSelected)
        {
            DataSource _db = new DataSource();
            DataTransaction mtran = new DataTransaction();
            try
            {
                Palette mClass = JSON.Deserialize<Palette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
                Guid mId = mClass.ID;
                bool result = mClass.fnGet(mId);
                mClass.UtilisateurModification = (string)Session["userName"];

                _db = mClass.db();
                mtran = _db.BeginTransaction(System.Data.IsolationLevel.Serializable);
                result = mClass.fnApprove(mtran);



                if (result)
                {
                    _db.CommitTransaction(mtran);

                    Store mstore = X.GetCmp<Store>("storeListePalette");

                    ModelProxy mProxy = mstore.GetById(mClass.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(mClass);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }
            }
            catch (Exception ex)
            {
                if (_db != null)
                {
                    _db.RollBackTransaction(mtran);
                }
                X.MessageBox.Alert("Error", ex.Message).Show();
            }

            return this.Direct();
        }

        public ActionResult SubmitFormMethod()
        {

            try
            {
                Palette Palette = new Palette();

                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    Palette.IsNew = true;
                else
                {
                    Palette.IsNew = false;

                    Palette.fnGet(int.Parse(GetFormValue("TxtPaletteID")));

                    if (Palette == null)
                        throw new Exception("SubmitFormMethod : Palette load failed.");
                }

                Palette = MapFormToObject(Palette);

                bool result = Palette.fnUpdate();

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePalette");
                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mstore.Insert(0, Palette);
                        X.GetCmp<RowSelectionModel>("rowSelectionPalette").Select(0);
                    }
                    else
                    {
                        ModelProxy mProxy = mstore.GetById(Palette.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(Palette);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormPalette").Close();
                    Viewport mViewport = X.GetCmp<Viewport>("TmsViewPort");
                    mViewport.Unmask();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Palette : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }
        public ActionResult OnPrintList()
        {
            Palette mclass = new Palette();
            Parametres mParam = new Parametres(0);

            ViewData["Campagne"] = mParam.Campagne;
            //ViewData["Produit"] = mParam.Produit.ID;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "Palette_Print", ViewData = ViewData };
        }

        //public ActionResult OnPrintPaletteList()
        //{
        //    string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
        //    try
        //    {
        //        XtraReport report = null;

        //        report = new rptPalettesList() as XtraReport;

        //        report.DataSource = DevExpressReportDs.SetDataSource(report);
        //        report.Parameters["campagneID"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;
        //        report.Parameters["paramCampagneText"].Value = X.GetCmp<ComboBox>("cmbCampagne").SelectedItem.Text;

        //        report.Parameters["paramExportateur"].Value = int.Parse(X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Value);
        //        report.Parameters["paramExportateurNom"].Value = X.GetCmp<ComboBox>("cmbExportateur").SelectedItem.Text;

        //        report.Parameters["paramCertification"].Value = int.Parse(X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Value);
        //        report.Parameters["paramCertificationText"].Value = X.GetCmp<ComboBox>("cmbCertification").SelectedItem.Text;

        //        report.Parameters["paramPaletteType"].Value = int.Parse(X.GetCmp<ComboBox>("cmbTypePalette").SelectedItem.Value);
        //        report.Parameters["paramPaletteTypeText"].Value = X.GetCmp<ComboBox>("cmbTypePalette").SelectedItem.Text;

        //        report.Parameters["paramDateDebut"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateDebut").RawText);
        //        report.Parameters["paramDateFin"].Value = DateTime.Parse(X.GetCmp<DateField>("txtDateFin").RawText);

        //        report.Parameters["paramStatut"].Value = int.Parse(X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Value);
        //        report.Parameters["paramStatutText"].Value = X.GetCmp<ComboBox>("cmbStatut").SelectedItem.Text;

        //        Session["report"] = report;


        //        return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'rdm{0}', '{1}/Palette_GestionStock/ViewList', this, 'List Of Palettes',''),App.Palette_GestionStock_Print.doClose()", Guid.NewGuid(), BaseUrl));
        //    }
        //    catch (Exception ex)
        //    {
        //        X.MessageBox.Show(new MessageBoxConfig
        //        {
        //            Title = "Livraison : Data Validation",
        //            Message = ex.Message,
        //            Buttons = MessageBox.Button.OK,
        //            Icon = MessageBox.Icon.WARNING
        //        });
        //        return this.Direct();
        //    }
        //}


        public ActionResult ViewList()
        {
            XtraReport report = null;
            report = Session["report"] as XtraReport;
            //report = new rptPalettesList() as XtraReport;

            //report.DataSource = DevExpressReportDs.SetDataSource(report);
            //report.Parameters["campagneID"].Value = Session["CampagneID"];
            //report.Parameters["paramCampagneText"].Value = Session["paramCampagneText"];

            //report.Parameters["paramExportateur"].Value = int.Parse(Session["paramExportateur"].ToString());
            //report.Parameters["paramExportateurNom"].Value = Session["paramExportateurNom"];

            //report.Parameters["paramCertification"].Value = int.Parse(Session["paramCertification"].ToString());
            //report.Parameters["paramCertificationText"].Value = Session["paramCertificationText"];

            //report.Parameters["paramPaletteType"].Value = int.Parse(Session["paramPaletteType"].ToString());
            //report.Parameters["paramPaletteTypeText"].Value = Session["paramPaletteTypeText"];

            //report.Parameters["paramDateDebut"].Value = DateTime.Parse(Session["paramDateDebut"].ToString());
            //report.Parameters["paramDateFin"].Value = DateTime.Parse(Session["paramDateFin"].ToString());

            //report.Parameters["paramStatut"].Value = int.Parse(Session["paramStatut"].ToString());
            //report.Parameters["paramStatutText"].Value = Session["paramStatutText"];            

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }


        private Palette MapFormToObject(Palette mClass)
        {

            Conditionnement conditionnement = new Conditionnement();
            conditionnement.ID = int.Parse(X.GetCmp<ComboBox>("cmbConditionnement").SelectedItem.Value.ToString());
            conditionnement.Designation = X.GetCmp<ComboBox>("cmbConditionnement").SelectedItem.Text;
            mClass.Conditionnement = conditionnement;

            ConditionnementReference conditionnementReference = new ConditionnementReference();
            conditionnementReference.ID = int.Parse(X.GetCmp<ComboBox>("cmbConditionnementReference").SelectedItem.Value.ToString());
            conditionnementReference.Reference = X.GetCmp<ComboBox>("cmbConditionnementReference").SelectedItem.Text;
            mClass.ConditionnementReference = conditionnementReference;



            mClass.NbreUniteParPalette = int.Parse(X.GetCmp<TextField>("txtNombreUnitePalette").Text.Replace(" ", ""));

            UniteDePoids uniteDePoids = new UniteDePoids();
            uniteDePoids.ID = int.Parse(X.GetCmp<ComboBox>("cmbUniteDePoids").SelectedItem.Value.ToString());
            uniteDePoids.Designation = X.GetCmp<ComboBox>("cmbUniteDePoids").SelectedItem.Text;
            mClass.UniteDePoids = uniteDePoids;

            mClass.PoidsBrutUnitaire = decimal.Parse(X.GetCmp<TextField>("txtPoidsBrut").Text);



            mClass.TareUnitaireEmballage = decimal.Parse(X.GetCmp<TextField>("txtTareUnitaireEmbalage").Text.Replace(" ", ""));

            mClass.UtilisateurCreation = (string)Session["userName"];
            mClass.UtilisateurModification = (string)Session["userName"];

            return mClass;
        }




        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                Palette Palette = JSON.Deserialize<Palette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });

                bool result = Palette.fnGet(Palette.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Palette loading failed.");
                //(string)Session["userName"];
                Palette.UtilisateurModification = (string)Session["userName"];

                if (Palette.Desactive)
                    result = Palette.fnActivate();
                else
                    result = Palette.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Palette, operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePalette");

                    ModelProxy mProxy = mstore.GetById(Palette.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(Palette);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Palette : OnActivateDeactivate",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }



        public ActionResult OnCancel(string ItemSelected)
        {
            try
            {
                Palette Palette = JSON.Deserialize<Palette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = Palette.fnGet(Palette.ID);

                if (!result)
                    throw new Exception("OnCancel : Movement Sheet loading failed.");

                Palette.UtilisateurModification = (string)Session["userName"];

                if (Palette.Desactive)
                    result = Palette.fnActivate();
                else
                    result = Palette.fnDeActivate();

                if (!result)
                    throw new Exception("OnCancel : Production - Palette, Operation failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListePalette");

                    ModelProxy mProxy = mstore.GetById(Palette.ID);

                    mProxy.BeginEdit();

                    mProxy.Set(Palette);

                    mProxy.Commit();

                    mProxy.EndEdit();
                }

            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - Palette : Cancel",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        [HttpPost]
        public ActionResult UpdateFormMethod()
        {
            try
            {
                Palette mClass = new Palette();
                bool result = true;
                Tms.Components.Settings.EnumsDefinition.eExecMode formExecMode = GetFormExecMode(X.GetCmp<Hidden>("hiddenExecMode").Value);

                if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    mClass.IsNew = true;
                else
                {
                    mClass.IsNew = false;

                    mClass.fnGet(Guid.Parse(GetFormValue("TxtPaletteId")));

                    if (mClass == null)
                        throw new Exception("UpdateFormMethod : Production - Palette - load failed.");

                }

                mClass = MapFormToObject(mClass);
                result = mClass.fnUpdate();

                if (result)
                {
                    Store mStore = X.GetCmp<Store>("storeListePalette");

                    ModelProxy mProxy;

                    if (formExecMode == Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    {
                        mStore.Insert(0, mClass);
                        X.GetCmp<RowSelectionModel>("rowPalette").Select(0);
                    }
                    else
                    {
                        mProxy = mStore.GetById(mClass.ID);

                        mProxy.BeginEdit();

                        mProxy.Set(mClass);

                        mProxy.Commit();

                        mProxy.EndEdit();
                    }

                    X.GetCmp<Window>("FormPalette").Close();
                }
            }
            catch (Exception ex)
            {
                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Production - Palette : Update",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
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

        private void CloseDetailWindow()
        {
            X.GetCmp<Window>("FormPalette").Close();
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








    }
}