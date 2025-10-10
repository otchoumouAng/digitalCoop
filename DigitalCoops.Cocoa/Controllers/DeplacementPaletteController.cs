using DevExpress.Web.Mvc;
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
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Components.Data;
using Tms.Classes.Security;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;


namespace Tms2017.MVC.Controllers
{
    //[RoutePrefix("ForwardContract")]
    public class DeplacementPaletteController : BaseController
    {
        const string UPDATE = "Update";
        const string ADD_NEW = "AddNew";
        const string CONSULT = "Consult";
        const string APPROVE = "Approve";
        const string DEFAULT = "Default";
        const string EXTEND = "Extend";

        // GET: DeplacementPalette
        //[Route]
        public ActionResult Index()
        {
            Parametres mParam = (new Parametres(0));

            X.GetCmp<ComboBox>("cmbCropYear").SetValue(mParam.Campagne);
           // X.GetCmp<Hidden>("defaultFinancingTypeID").SetValue(mParam.DeplacementPaletteTypeFinancement);

            string UserName = (string)Session["userName"];
            Site mSiteParDefaut = new Site();

            bool result = mSiteParDefaut.fnGetBySiteByUserName(UserName);
            ViewBag.SiteParDefaut = mSiteParDefaut.ID;
            //X.GetCmp<ComboBox>("cmbTypeContrat").SetValue(mclass.DeplacementPaletteType.ID);

            DateTime firstdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            DateTime lastDayOfMonth = firstdayofmonth.AddMonths(1).AddDays(-1);

            X.GetCmp<DateField>("TxtPeriodStart").RawText = firstdayofmonth.ToShortDateString();
            X.GetCmp<DateField>("TxtPeriodEnd").RawText = lastDayOfMonth.ToShortDateString();

            X.GetCmp<FormPanel>("CriteriaPanelForwardContract").SetTitle("Site : " + mSiteParDefaut.Nom + ", Campagne : " + mParam.Campagne + " | Du : " + firstdayofmonth.ToShortDateString() + " Au : " + lastDayOfMonth.ToShortDateString());

            #region Set Function's Access            
            Fonction HasAccess = new Fonction();
            //bool HavPermissionOverview = HasAccess.fnGetUserAccessStatus("{B8EC41D0-173A-47A6-BB9A-140942CE66B8}", UserName);

            if (HasAccess.fnGetUserAccessStatus("{9C7B9B85-4E09-4025-AE98-4B409121C195}", UserName) == false)
                X.GetCmp<Button>("btnNewContract").Disable();
            else
                X.GetCmp<Button>("btnNewContract").Enable();

            if (HasAccess.fnGetUserAccessStatus("{9A7624A9-AFAD-4EB0-9799-BFC0C86365D7}", UserName) == false)
                X.GetCmp<MenuItem>("mnuExportContractToExcel").Disable();
            else
                X.GetCmp<MenuItem>("mnuExportContractToExcel").Enable();

            if (HasAccess.fnGetUserAccessStatus("{8BC71399-E078-4B7C-8034-DEA60D4A229F}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintBalance").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintBalance").Enable();

            if (HasAccess.fnGetUserAccessStatus("{340FA4F5-849F-4E31-9CAA-017D51D6E39B}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintExecution").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintExecution").Enable();

            if (HasAccess.fnGetUserAccessStatus("{C488A793-43FF-4A89-B48F-F5A1E96CF91E}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintForwardContractList").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintForwardContractList").Enable();

            if (HasAccess.fnGetUserAccessStatus("{a721b431-8bb3-4cc1-ac2c-5d9f09942274}", UserName) == false)
                X.GetCmp<MenuItem>("mnuPrintPrime").Disable();
            else
                X.GetCmp<MenuItem>("mnuPrintPrime").Enable();

            if (HasAccess.fnGetUserAccessStatus("{b1c99d84-4276-4e16-ad84-363d0c047b4b}", UserName) == false)
            {
                X.GetCmp<MenuItem>("mnuPrintEtatLivStatut").Disable();
                X.GetCmp<MenuItem>("mnuPrintEtatLivStatutFin").Disable();
            }
            else
            {
                X.GetCmp<MenuItem>("mnuPrintEtatLivStatut").Enable();
                X.GetCmp<MenuItem>("mnuPrintEtatLivStatutFin").Enable();
            }

            X.GetCmp<Hidden>("CphiddenPermCreer").SetValue(HasAccess.fnGetUserAccessStatus("{9C7B9B85-4E09-4025-AE98-4B409121C195}", UserName));
            X.GetCmp<Hidden>("CphiddenPermModifier").SetValue(HasAccess.fnGetUserAccessStatus("{676D2692-3211-476C-B51B-154AA7115175}", UserName));
            X.GetCmp<Hidden>("CphiddenPermDesactiver").SetValue(HasAccess.fnGetUserAccessStatus("{16CB8FFF-F721-4A4D-BE09-DE26E29BBFCD}", UserName));
            X.GetCmp<Hidden>("CphiddenPermActiver").SetValue(HasAccess.fnGetUserAccessStatus("{C7177110-080D-41BF-BBBA-D788F2689963}", UserName));
            X.GetCmp<Hidden>("CphiddenPermExporterExcel").SetValue(HasAccess.fnGetUserAccessStatus("{9A7624A9-AFAD-4EB0-9799-BFC0C86365D7}", UserName));
            //X.GetCmp<Hidden>("SphiddenPermPrintSpotPriceList").SetValue(HasAccess.fnGetUserAccessStatus("{2ABE7A72-758C-4A1D-B213-A34007BFECB1}", UserName));
            X.GetCmp<Hidden>("CphiddenPermApprove").SetValue(HasAccess.fnGetUserAccessStatus("144249A6-95E0-4340-993A-B0F8567ABE21", UserName));
            X.GetCmp<Hidden>("CphiddenPermExtend").SetValue(HasAccess.fnGetUserAccessStatus("1ADA7DA5-B7EB-4C72-9600-C5A98E74BBFE", UserName));
            X.GetCmp<Hidden>("CphiddenPermPrintBalance").SetValue(HasAccess.fnGetUserAccessStatus("{8BC71399-E078-4B7C-8034-DEA60D4A229F}", UserName));
            X.GetCmp<Hidden>("CphiddenPermPrintExecution").SetValue(HasAccess.fnGetUserAccessStatus("{340FA4F5-849F-4E31-9CAA-017D51D6E39B}", UserName));
            X.GetCmp<Hidden>("CphiddenPermOverview").SetValue(HasAccess.fnGetUserAccessStatus("{6B20933B-F5EC-4F03-8A5C-DDE720F5BE98}", UserName));
            X.GetCmp<Hidden>("CphiddenPermClose").SetValue(HasAccess.fnGetUserAccessStatus("{22481B8A-69CA-4366-AEEB-DFA87EBF4BCA}", UserName));
            X.GetCmp<Hidden>("CphiddenPermRegenerate").SetValue(HasAccess.fnGetUserAccessStatus("{c888dc30-f56d-4658-8edd-e1b433d81ae8}", UserName));
            X.GetCmp<Hidden>("CphiddenPermPrintDetailExecution").SetValue(HasAccess.fnGetUserAccessStatus("{2b00235a-9e1f-4d8a-a907-5b2243393caf}", UserName));
            X.GetCmp<Hidden>("CphiddenPermPrintLivStatut").SetValue(HasAccess.fnGetUserAccessStatus("{b1c99d84-4276-4e16-ad84-363d0c047b4b}", UserName));

            #endregion

            return View();
        }

        public ActionResult OnAdd()
        {
            DeplacementPaletteViewModel mclass = new DeplacementPaletteViewModel();

            mclass._DeplacementPalette = new DeplacementPalette();
            mclass._DeplacementPalette.Palette = new Palette();
            mclass._DeplacementPalette.OrdreFabrication = new OrdreFabrication();
            mclass._DeplacementPalette.MagasinSource = new Magasin();
            mclass._DeplacementPalette.MagasinDestination = new Magasin();

            string UserName = (string)Session["userName"];

           

            
            //mclass._DeplacementPalette.Campagne.Designation = mParam.Campagne;
            //mclass._DeplacementPalette.DeplacementPaletteType.ID = mParam.DeplacementPaletteType.ID;
            //mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
            //ViewData["FinancingTypeID"] = mParam.DeplacementPaletteTypeFinancement;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDeplacementPalette", Model = mclass, ViewData = ViewData };
        }

        public ActionResult OnEdit(string ItemSelected)
        {

            DeplacementPalette mclass = JSON.Deserialize<DeplacementPalette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            DeplacementPaletteViewModel contratVM = new DeplacementPaletteViewModel();
            contratVM._DeplacementPalette = new DeplacementPalette();

            contratVM._DeplacementPalette = mclass;
            contratVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
            Parametres parametre = new Parametres(0);
            ViewData["UrlContratType"] = "LoadTypeDeplacementPaletteWithoutFinancingType";
            //ViewData["FinancingTypeID"] = parametre.DeplacementPaletteTypeFinancement;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDeplacementPalette", Model = contratVM, ViewData = ViewData };
        }

        public ActionResult OnConsult(string ItemSelected)
        {
            DeplacementPalette mclass = JSON.Deserialize<DeplacementPalette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

            DeplacementPaletteViewModel contratVM = new DeplacementPaletteViewModel();
            contratVM._DeplacementPalette = new DeplacementPalette();

            contratVM._DeplacementPalette = mclass;
            ViewData["UrlContratType"] = "LoadTypeDeplacementPalette";
            contratVM._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;


            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDeplacementPalette", Model = contratVM, ViewData = ViewData };
        }

        public ActionResult OnRefresh(string ItemCropYear, string ItemFournisseur, string ItemTypeContrat, string ItemPeriodStart, string ItemPeriodEnd, string ItemStatus, string ItemSite, string ItemOption)
        {
            try
            {
                DateTime datedebut = DateTime.ParseExact(ItemPeriodStart, "d", CultureInfo.CurrentUICulture);
                DateTime datedfin = DateTime.ParseExact(ItemPeriodEnd, "d", CultureInfo.CurrentUICulture);

                Store mstore = X.GetCmp<Store>("storeListeForwardContract");

                mstore.Reload();

                mstore.Reload(new Ext.Net.ParameterCollection()
                            {
                                new Ext.Net.Parameter("ItemCropYear", ItemCropYear),
                                new Ext.Net.Parameter("ItemFournisseur", ItemFournisseur),
                                new Ext.Net.Parameter("ItemTypeContrat", ItemTypeContrat),
                                new Ext.Net.Parameter("ItemPeriodStart", ItemPeriodStart),
                                new Ext.Net.Parameter("ItemPeriodEnd", ItemPeriodEnd),
                                new Ext.Net.Parameter("ItemSite", ItemSite),
                                new Ext.Net.Parameter("ItemStatus", ItemStatus),
                                new Ext.Net.Parameter("ItemOption", ItemOption)
                            });
                FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelForwardContract");

                mform.Collapsed = true;
            }
            catch (Exception ex)
            {

                X.MessageBox.Show(new MessageBoxConfig
                {
                    Title = "Contrat Periode : Data Validation",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnFilter()
        {
            FormPanel mform = X.GetCmp<FormPanel>("CriteriaPanelForwardContract");
            mform.ToggleCollapse();
            return this.Direct();
        }

        public ActionResult Select()
        {
            //string CropYearID = string.IsNullOrEmpty(ItemCropYear) ? "{Tous}" : ItemCropYear;
            //string optionID = string.IsNullOrEmpty(ItemOption) ? "NO" : ItemOption;
            //if (!string.IsNullOrEmpty(ItemCropYear) && ItemCropYear.Contains("null"))
            //{
            //    CropYearID = "{Tous}";
            //}

            //int fournisseurID = GetCritriaValue(ItemFournisseur);
            //int TypeContratID = GetCritriaValue(ItemTypeContrat);
            //int SiteID = GetCritriaValue(ItemSite);

            //DateTime? startdate = string.IsNullOrEmpty(ItemPeriodStart) ? (DateTime?)null : DateTime.Parse(ItemPeriodStart.ToString());
            //DateTime? enddate = string.IsNullOrEmpty(ItemPeriodEnd) ? (DateTime?)null : DateTime.Parse(ItemPeriodEnd.ToString());

            //string status = string.IsNullOrEmpty(ItemStatus) ? "-1" : ItemStatus;
            //if (!string.IsNullOrEmpty(ItemStatus) && ItemStatus.Contains("null"))
            //{
            //    status = "-1";
            //}


            var mListe = (new DeplacementPalette()).fnSelect("-1");

            return this.Store(mListe);

        }

        public ActionResult SelectByModalFilter(string ItemAnnee, string ItemSemaine, string ItemProduit, string ItemProduitType)
        {
            int annee = string.IsNullOrEmpty(ItemAnnee) ? -1 : int.Parse(ItemAnnee);
            int semaine = string.IsNullOrEmpty(ItemSemaine) ? -1 : int.Parse(ItemSemaine);
            int produitId = string.IsNullOrEmpty(ItemProduit) ? -1 : int.Parse(ItemProduit);
            int produitTypeId = string.IsNullOrEmpty(ItemProduitType) ? -1 : int.Parse(ItemProduitType);

            var liste = new Palette().fnSelect("-1", annee, semaine, produitId, produitTypeId, null);
            return this.Store(liste);
        }


        public ActionResult OnPaletteSelected(string SelectedPaletteID)
        {
            try
            {

                Guid paletteId;
                if (string.IsNullOrEmpty(SelectedPaletteID) || !Guid.TryParse(SelectedPaletteID, out paletteId))
                {
                    throw new ArgumentException("ID de palette invalide.");
                }

                Palette palette = new Palette();
                if (!palette.fnGet(paletteId) || palette.IsNew)
                {
                    X.Msg.Alert("Information", "Impossible de charger les détails pour la palette sélectionnée.").Show();
                    return this.Direct();
                }

                // IMPORTANT: L'ID du magasin doit être retourné par la procédure stockée pp_Palette_Get.
                // En attendant une mise à jour, nous mettons une valeur par défaut.
                int magasinDepartID = 0; // À corriger après la mise à jour de la BDD.

                // Utilisation d'un DirectResult pour envoyer plusieurs commandes au client.
                var directResult = new DirectResult();
                var script = new System.Text.StringBuilder();

                script.AppendLine($"App.hiddenMagasinSourceID.setValue('{magasinDepartID}');");
                script.AppendLine($"App.dfMagasinDepart.update('{palette.StockMagasin ?? "N/A"}');");
                script.AppendLine($"App.dfEmplacementDepart.update('{palette.StockEmplacement ?? "N/A"}');");
                script.AppendLine($"App.dfPoidsBrut.update('{palette.PoidsBrutPalette.ToString("N2")}');");
                script.AppendLine($"App.dfPoidsNet.update('{palette.PoidsNetPalette.ToString("N2")}');");
                script.AppendLine("App.PaletteInfoContainer.show();");
                script.AppendLine("App.DestinationFormContainer.show();");
                script.AppendLine("App.FormDeplacementPaletteWindow.setHeight(700);");
                script.AppendLine("App.FormDeplacementPaletteWindow.center();");

                directResult.Script = script.ToString();
                return directResult;
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Erreur", "Une erreur est survenue : " + ex.Message).Show();
                return this.Direct();
            }
        }



        public ActionResult OnDeplacerManuellement()
        {
            DeplacementPaletteViewModel mclass = new DeplacementPaletteViewModel();

            mclass._DeplacementPalette = new DeplacementPalette();
            mclass._DeplacementPalette.Palette = new Palette();
            mclass._DeplacementPalette.MagasinSource = new Magasin();
            mclass._DeplacementPalette.MagasinDestination = new Magasin();
            mclass._ExecMode = Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;

            return new Ext.Net.MVC.PartialViewResult { ViewName = "FormDeplacementPalette", Model = mclass };
        }


        public ActionResult GetPalettesByOrdreFabrication(string idOrdreFabrication)
        {
            if (string.IsNullOrEmpty(idOrdreFabrication))
            {
                return this.Store(new List<Palette>());
            }

            Guid ordreId;
            if (!Guid.TryParse(idOrdreFabrication, out ordreId))
            {
                return this.Store(new List<Palette>());
            }

            // Ici, vous devez appeler une méthode qui récupère les palettes pour cet ordre de fabrication
            // Par exemple : 
            var palettes = (new Palette()).fnPaletteByOf_Get(ordreId);

            return this.Store(palettes);
        }


        public ActionResult OnActivateDeactivate(string ItemSelected)
        {
            try
            {
                DeplacementPalette mClass = JSON.Deserialize<DeplacementPalette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = mClass.fnGet(mClass.ID);

                if (!result)
                    throw new Exception("OnActivateDeactivate : Contrat Periode loading failed.");

                if (mClass.Desactive)
                    result = mClass.fnActivate();
                else
                    result = mClass.fnDeActivate();

                if (!result)
                    throw new Exception("OnActivateDeactivate : Contrat Periode Approval failed.");


                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeForwardContract");

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
                    Title = "Contrat Periode : Approuver",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }

            return this.Direct();
        }

        public ActionResult OnClose(string ItemSelected)
        {
            try
            {
                DeplacementPalette mClass = JSON.Deserialize<DeplacementPalette>(ItemSelected, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Ignore });

                bool result = mClass.fnGet(mClass.ID);

                if (!result)
                    throw new Exception("OnClose : Contrat Periode loading failed.");

                mClass.UtilisateurModification = (string)Session["userName"];
                result = mClass.fnClose();

                if (!result)
                    throw new Exception("OnClose : Contrat Periode Close failed.");

                if (result)
                {
                    Store mstore = X.GetCmp<Store>("storeListeForwardContract");

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
                    Title = "Contrat Periode : Close",
                    Message = ex.Message,
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.WARNING
                });
            }
            return this.Direct();
        }

        [HttpPost]
        public ActionResult SubmitFormMethod()
        {
            try
            {
                DeplacementPalette mClass = new DeplacementPalette();
                mClass.IsNew = true; // Dans ce contexte, c'est toujours un nouvel enregistrement.
                mClass.UtilisateurCreation = (string)Session["userName"];

                mClass = MapFormToObject(mClass);

                if (mClass.fnUpdate())
                {
                    X.GetCmp<Window>("FormDeplacementPaletteWindow").Close();
                    X.Msg.Notify("Opération réussie", "Le déplacement a été enregistré.").Show();
                    X.GetCmp<Store>("storeListeForwardContract").Reload(); // Assurez-vous que l'ID est correct
                }
                else
                {
                    X.Msg.Alert("Échec", "L'enregistrement a échoué.").Show();
                }
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Erreur", "Une erreur est survenue : " + ex.Message).Show();
            }
            return this.Direct();
        }

        private DeplacementPalette MapFormToObject(DeplacementPalette mClass)
        {
            try
            {
                // --- Palette
                string paletteValue = GetFormValue("cmbPalette");
                if (string.IsNullOrEmpty(paletteValue)) throw new Exception("Veuillez sélectionner une palette.");
                mClass.Palette = new Palette { ID = Guid.Parse(paletteValue) };

                // --- Magasin Source (depuis le champ caché)
                string magasinSourceValue = GetFormValue("hiddenMagasinSourceID");
                if (string.IsNullOrEmpty(magasinSourceValue) || magasinSourceValue == "0")
                {
                    throw new Exception("L'ID du magasin source est manquant. Mettez à jour la procédure stockée 'pp_Palette_Get'.");
                }
                mClass.MagasinSource = new Magasin { ID = int.Parse(magasinSourceValue) };

                // --- Magasin Destination
                string magasinDestValue = GetFormValue("cmbMagasinDest");
                if (string.IsNullOrEmpty(magasinDestValue)) throw new Exception("Veuillez sélectionner le magasin de destination.");
                mClass.MagasinDestination = new Magasin { ID = int.Parse(magasinDestValue) };

                // --- Dates
                string dateDepartValue = GetFormValue("DateDepart");
                if (string.IsNullOrEmpty(dateDepartValue)) throw new Exception("Veuillez saisir la date de départ.");
                mClass.DateDepart = DateTime.Parse(dateDepartValue);

                string dateArriveeValue = GetFormValue("DateArrivee");
                mClass.DateArrivee = !string.IsNullOrEmpty(dateArriveeValue) ? DateTime.Parse(dateArriveeValue) : DateTime.MinValue;

                // --- Emplacement destination
                string emplacementDest = GetFormValue("TxtEmplacementDest");
                if (string.IsNullOrEmpty(emplacementDest)) throw new Exception("Veuillez saisir l'emplacement de destination.");
                mClass.EmplacementDestination = emplacementDest;

                // --- Champs par défaut
                mClass.ModeDeTransfert = "Manuel";
                mClass.Operateur = (string)Session["userName"];
                mClass.Description = "Déplacement manuel de palette.";

                return mClass;
            }
            catch (Exception ex)
            {
                // Propage l'exception pour qu'elle soit attrapée par la méthode appelante
                throw new Exception($"Erreur lors de la validation des données du formulaire : {ex.Message}");
            }
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
            //X.Js.Call("App.rowSelectionListeForwardContract.deselectAll();");
            X.GetCmp<RowSelectionModel>("rowSelectionListeForwardContract").DeselectAll();
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

        public ActionResult ViewReportResult()
        {
            //XtraReport report = null;

            rptForwardContractList report = new rptForwardContractList();
            report.DataSource = DevExpressReportDs.SetDataSource(report);

            report.Parameters["paramCampagneText"].Value = Session["paramCampagne"];
            report.Parameters["paramCampagne"].Value = Session["paramCampagne"];
            //if (!string.IsNullOrEmpty(Session["paramCampagne"].ToString()) && Session["paramCampagne"].ToString() == "{Tous}")
            //    report.Parameters["paramCampagne"].Value = string.Empty;
            //else
            //    report.Parameters["paramCampagne"].Value = Session["paramCampagne"];

            report.Parameters["paramFournisseur"].Value = Session["paramFournisseur"];
            report.Parameters["paramFournisseurText"].Value = Session["paramFournisseurText"];

            report.Parameters["paramTypeOfContract"].Value = Session["paramTypeOfContract"];
            report.Parameters["paramTypeOfContractText"].Value = Session["paramTypeOfContractText"];

            report.Parameters["paramStatut"].Value = Session["paramStatut"];
            report.Parameters["paramStatutText"].Value = Session["paramStatutText"];

            report.Parameters["paramDateDebut"].Value = Session["paramStartDate"];
            report.Parameters["paramDateFin"].Value = Session["paramEndDate"];

            report.Parameters["siteID"].Value = Session["paramSite"];
            report.Parameters["paramSiteNomText"].Value = Session["paramSiteText"];

            report.Parameters["paramOption"].Value = Session["paramOption"];
            report.Parameters["paramOptionText"].Value = Session["paramOptionText"];

            ViewData["Report"] = report;

            return View("ViewReportResult");
        }

        public ActionResult OnPrintExecutionDetail(string IdContrat)
        {

            string BaseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            return JavaScript(String.Format("addTab(window.parent.Ext.getCmp('tabCenter'), 'ContratDetail{0}', '{1}/DeplacementPalette/ViewReport?id={0}', this, 'Execution des contrats','')", IdContrat, BaseUrl));
        }

    }
}