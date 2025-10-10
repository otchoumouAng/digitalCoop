using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tms.Classes.Business;
using Tms.Classes.Security;
using Tms.Classes.Shared;

namespace Tms2017.MVC.Models
{
    public class Module
    {
        public string Title { get; set; }

        public double ID { get; set; }
        public IEnumerable<ModuleItem> Items { get; set; }

        //public static List<Module> GetAll()
        //{
        //    List<Module> moduleListe = new List<Module>();            
        //    Module module ;
        //    List<ModuleItem> mModuleItems ;


        //    string base_url = (new Parametres()).Base_url;
        //    string base_icon = "menu_04.png";
        //    string base_icon_settings = "settings.jfif";
        //    //string base_icon = "action_new_04.png";


        //    //module = new Module();
        //    //mModuleItems = new List<ModuleItem>();
        //    //module.Title = "Suppliers";
        //    ////mModuleItems.Add(new ModuleItem() { ID=1, Title="Suppliers", Icon= "../Images/icons/"+ base_icon, Url = base_url+ "/Fournisseur/" });
        //    ////mModuleItems.Add(new ModuleItem() { ID=2, Title="Reporting", Icon= "../Images/icons/" + base_icon, Url = base_url+ "/Fournisseur/" });

        //    //module.Items = mModuleItems;
        //    //moduleListe.Add(module);


        //    module = new Module();
        //    mModuleItems = new List<ModuleItem>();
        //    module.Title = "Purchases";
        //    mModuleItems.Add(new ModuleItem() { ID = 1, Title = "Suppliers", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Fournisseur/" });
        //    //mModuleItems.Add(new ModuleItem() { ID = 2, Title = "Reporting", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Fournisseur/" });
        //    mModuleItems.Add(new ModuleItem() { ID = 3, Title = "Prix Journalier", Icon = "../Images/icons/" + base_icon, Url = base_url + "/DailyPrice" });
        //    mModuleItems.Add(new ModuleItem() { ID = 4, Title = "Prix Negocié", Icon = "../Images/icons/" + base_icon, Url = base_url + "/SpotPrice" });

        //    mModuleItems.Add(new ModuleItem() { ID = 5, Title = "Forward contracts", Icon = "../Images/icons/" + base_icon, Url = base_url + "/ContratPeriode" });
        //    mModuleItems.Add(new ModuleItem() { ID = 6, Title = "Financing", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Financement/" });
        //    mModuleItems.Add(new ModuleItem() { ID = 7, Title = "Financing - Approval", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Financement/Index_Approval" });


            //mModuleItems.Add(new ModuleItem() { ID = 8, Title = "Deliveries", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Livraison/" });
            //mModuleItems.Add(new ModuleItem() { ID = 9, Title = "Weighings", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Pesee/" });
            //mModuleItems.Add(new ModuleItem() { ID = 9.5, Title = "Weighings-Rejection", Icon = "../Images/icons/" + base_icon, Url = base_url + "/PeseeRefoulee/" });
            //mModuleItems.Add(new ModuleItem() { ID = 10, Title = "Codings", Icon = "../Images/icons/" + base_icon, Url = base_url + "/DeliveryCoding/" });
            //mModuleItems.Add(new ModuleItem() { ID = 10.1, Title = "Analyse Physique", Icon = "../Images/icons/" + base_icon, Url = base_url + "/AnalysePhysique/" });

            //mModuleItems.Add(new ModuleItem() { ID = 11, Title="Delivery Note", Icon = "../Images/icons/" + base_icon, Url = base_url+  "/BonDeLivraison/" });
            //mModuleItems.Add(new ModuleItem() { ID = 12, Title="Invoices", Icon = "../Images/icons/" + base_icon, Url = base_url+  "/Facture/" });
            //mModuleItems.Add(new ModuleItem() { ID = 13, Title="Payments", Icon = "../Images/icons/" + base_icon, Url = base_url+  "/Fournisseur/" });
           
        //    module.Items = mModuleItems;
        //    moduleListe.Add(module);


        //    module = new Module();
        //    mModuleItems = new List<ModuleItem>();
        //    module.Title = "Interface";
        //    mModuleItems.Add(new ModuleItem() { ID = 14, Title = "Invoices", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Fournisseur/" });
        //    mModuleItems.Add(new ModuleItem() { ID = 15, Title = "Payments", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Fournisseur/" });
        //    module.Items = mModuleItems;
        //    moduleListe.Add(module);


        //    module = new Module();
        //    mModuleItems = new List<ModuleItem>();
        //    module.Title = "Administration";
        //    mModuleItems.Add(new ModuleItem() { ID = 16, Title = "Modules", Icon = "../Images/icons/" + base_icon, Url = base_url+  "/Module/" });
        //    mModuleItems.Add(new ModuleItem() { ID = 17, Title = "Functions", Icon = "../Images/icons/" + base_icon, Url = base_url+ "/Fonction/" });
        //    mModuleItems.Add(new ModuleItem() { ID = 18, Title = "Users", Icon = "../Images/icons/" + base_icon, Url = base_url+ "/Utilisateur/" });
        //    mModuleItems.Add(new ModuleItem() { ID = 19, Title = "Roles", Icon = "../Images/icons/" + base_icon, Url = base_url+ "/Role/" });
        //    module.Items = mModuleItems;
        //    moduleListe.Add(module);

        //    module = new Module();
        //    mModuleItems = new List<ModuleItem>();
        //    module.Title = "Account";
        //    mModuleItems.Add(new ModuleItem() { ID = 20, Title = "Change PassWord", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Utilisateur/OnChangePassWord" });           
        //    module.Items = mModuleItems;
        //    moduleListe.Add(module);




        //    return moduleListe;
        //}

        public static List<Module> GetItemsForUser(Guid userId, int DisplayOnWorkspace, int DisplayOnReport)
        {
            var mApp = (new Application()).fnSelectByUser(userId, DisplayOnReport);

            List<Application> mList = new List<Application>();
            Application mClass = new Application();
            Tms.Classes.Security.Module myModule = new Tms.Classes.Security.Module();
            List<Module> moduleListe = new List<Module>();
            List<ModuleItem> mModuleItems;

            Module module;

            ModuleItem moduleitem;

            for (int i = 0; i < mApp.Count; i++)
            {
            module = new Module();
                mClass = mApp[i] as Application;
                module.Title = mClass.Nom;                

                var mModule = new Tms.Classes.Security.Module().fnSelectAllByUserAndApp(userId, mClass.ID, DisplayOnWorkspace, DisplayOnReport);

                List<Tms.Classes.Security.Module> listModules = new List<Tms.Classes.Security.Module>();
            mModuleItems = new List<ModuleItem>();

                if (mModule.Count > 0)
                {

                    for (int m = 0; m < mModule.Count; m++)
                    {                        
                        myModule = mModule[m] as Tms.Classes.Security.Module;
                        moduleitem = new ModuleItem();
                        moduleitem.ID = myModule.ID;
                        moduleitem.Title = myModule.Nom;
                        moduleitem.Icon = myModule.IconModule;
                        moduleitem.Url = myModule.FullUrl;
                        mModuleItems.Add(moduleitem);
                    }

                }
                module.Items = mModuleItems;
                moduleListe.Add(module);
            }

            return moduleListe;
        }


        public static List<Module> GetAllModules()
        {
            return new List<Module>()
            {
                //new Module() {  ID=1,Title="Suppliers" },
                new Module() {  ID=2,Title="Purchases" },
                new Module() {  ID=3,Title="Interface" },
                new Module() {  ID=4,Title="Administration" },
                new Module() {  ID=5,Title="References" }
            };

        }


        //public static List<Module> GetByModuleID(double moduleID)
        //{

        //    List<ModuleItem> mItems = new List<ModuleItem>();

        //    string base_url = (new Parametres()).Base_url;
        //    string base_icon = "menu_04.png";
        //    string base_icon_settings = "settings.jfif";
        //    //string base_icon = "action_new_04.png";

        //    switch (moduleID.ToString())
        //   {
        //        //case "1":

        //        //    mItems.Add(new ModuleItem() { ID = 1, Title = "Suppliers", Icon = "../Images/icons/" + base_icon, Url= base_url + "/Fournisseur/" });
                    
        //        //    mItems.Add(new ModuleItem() { ID = 2, Title = "Reporting", Icon = "../Images/icons/" + base_icon, Url= base_url + "/Fournisseur/" });

        //        //    return new List<Module>()
        //        //    {
        //        //        new Module() {  ID=1,Title="Suppliers", Items = mItems }
        //        //    };
                    
        //        case "2":

                    //mItems.Add(new ModuleItem() { ID = 1, Title = "Suppliers", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Fournisseur/" });
                    ////mItems.Add(new ModuleItem() { ID = 2, Title = "Reporting", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Fournisseur/" });
                    //mItems.Add(new ModuleItem() { ID = 3, Title = "Prix Journalier", Icon = "../Images/icons/" + base_icon, Url = base_url + "/DailyPrice" });
                    //mItems.Add(new ModuleItem() { ID = 4, Title = "Prix Negocié", Icon = "../Images/icons/" + base_icon, Url = base_url + "/SpotPrice" });
                    //mItems.Add(new ModuleItem() { ID = 5, Title = "Contrats Periode", Icon = "../Images/icons/" + base_icon, Url = base_url + "/ContratPeriode" });
                    //mItems.Add(new ModuleItem() { ID = 6, Title = "Financing", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Financement/" });
                    //mItems.Add(new ModuleItem() { ID = 7, Title = "Financing - Approval", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Financement/Index_Approval" });
                    //mItems.Add(new ModuleItem() { ID = 8, Title = "Deliveries", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Livraison/" });
                    //mItems.Add(new ModuleItem() { ID = 9, Title = "Weighings", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Pesee/" });
                    //mItems.Add(new ModuleItem() { ID = 9.5, Title = "Weighings-Rejection", Icon = "../Images/icons/" + base_icon, Url = base_url + "/PeseeRefoulee/" });
                    //mItems.Add(new ModuleItem() { ID = 10, Title = "Coding", Icon = "../Images/icons/" + base_icon, Url = base_url + "/DeliveryCoding" });
                    //mItems.Add(new ModuleItem() { ID = 10.1, Title = "Analyse Physique", Icon = "../Images/icons/" + base_icon, Url = base_url + "/AnalysePhysique/" });
                    //mItems.Add(new ModuleItem() { ID = 11, Title = "Delivery Note", Icon = "../Images/icons/" + base_icon, Url = base_url + "/BonDeLivraison/" });
                    //mItems.Add(new ModuleItem() { ID = 12, Title = "Facture", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Facture/" });
                    //mItems.Add(new ModuleItem() { ID = 13, Title = "Payments", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Fournisseur/" });
                    

        //            return new List<Module>()
        //            {
        //                new Module() {  ID=2,Title="Purchases", Items = mItems }
        //            };
        //        case "3":
                    
        //            mItems.Add(new ModuleItem() { ID = 14, Title = "Invoices", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Fournisseur/" });
        //            mItems.Add(new ModuleItem() { ID = 15, Title = "Payments", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Fournisseur/" });
        //            mItems.Add(new ModuleItem() { ID = 200, Title = "DashBoard", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Dashboard/" });

        //            return new List<Module>()
        //            {
        //                new Module() {  ID=3,Title="Interfaces", Items = mItems }
        //            };
                    
        //        case "4":

        //            mItems.Add(new ModuleItem() { ID = 16, Title = "Modules", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Module/" });
        //            mItems.Add(new ModuleItem() { ID = 17, Title = "Functions", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Fonction/" });
        //            mItems.Add(new ModuleItem() { ID = 18, Title = "Users", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Utilisateur/" });
        //            mItems.Add(new ModuleItem() { ID = 19, Title = "Roles", Icon = "../Images/icons/" + base_icon, Url = base_url + "/Role/" });                   

        //            return new List<Module>()
        //            {
        //                new Module() {  ID=4,Title="Administration", Items = mItems }
        //            };

        //        case "5":

        //            mItems.Add(new ModuleItem() { ID = 22, Title = "Type De Livraison", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/LivraisonType/" });
        //            mItems.Add(new ModuleItem() { ID = 23, Title = "Product", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/Produit/" });
        //            mItems.Add(new ModuleItem() { ID = 24, Title = "Contrat Periode Type", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/ContratPeriodeType/" });
        //            mItems.Add(new ModuleItem() { ID = 25, Title = "Campagne", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/Campagne/" });
        //            mItems.Add(new ModuleItem() { ID = 26, Title = "Certification", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/Certification/" });
        //            mItems.Add(new ModuleItem() { ID = 27, Title = "Beans Classification", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/ClassificationFeves/" });
        //            mItems.Add(new ModuleItem() { ID = 28, Title = "Destination", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/Destination/" });
        //            mItems.Add(new ModuleItem() { ID = 29, Title = "Destination Type", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/DestinationType/" });
        //            mItems.Add(new ModuleItem() { ID = 30, Title = "Supplier Group", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/FournisseurGroupe/" });
        //            mItems.Add(new ModuleItem() { ID = 31, Title = "Supplier Type", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/FournisseurType/" });
        //            mItems.Add(new ModuleItem() { ID = 32, Title = "Type Of Origin", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/ProvenanceType/" });
        //            mItems.Add(new ModuleItem() { ID = 33, Title = "Type de Sacs", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/SacType/" });
        //            mItems.Add(new ModuleItem() { ID = 34, Title = "Exportateur", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/Exportateur/" });
        //            mItems.Add(new ModuleItem() { ID = 35, Title = "Bank", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/Banque/" });
        //            mItems.Add(new ModuleItem() { ID = 36, Title = "Type De Deduction Facture", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/FactureDeductionType/" });
        //            mItems.Add(new ModuleItem() { ID = 37, Title = "Type de Valorisation Facture", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/FactureValorisationType/" });
        //            mItems.Add(new ModuleItem() { ID = 38, Title = "Type De Financement", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/FinancementType/" });
        //            mItems.Add(new ModuleItem() { ID = 39, Title = "Laboratory", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/Laboratoire/" });
        //            mItems.Add(new ModuleItem() { ID = 40, Title = "Accounting Type", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/MiseEnCompteType/" });
        //            mItems.Add(new ModuleItem() { ID = 41, Title = "Mode Of Payment", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/PayementMode/" });
        //            mItems.Add(new ModuleItem() { ID = 42, Title = "Type Of Transaction", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/PayementTransactionType/" });
        //            mItems.Add(new ModuleItem() { ID = 43, Title = "Id Type", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/PieceType/" });
        //            mItems.Add(new ModuleItem() { ID = 44, Title = "Prix Negocié Mode Of Application", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/PrixNegocieModeApplication/" });
        //            mItems.Add(new ModuleItem() { ID = 45, Title = "Protocol", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/Protocole/" });
        //            mItems.Add(new ModuleItem() { ID = 46, Title = "Origin", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/Provenance/" });                    
        //            mItems.Add(new ModuleItem() { ID = 47, Title = "Direct Refund Type", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/RemboursementDirectType/" });
        //            mItems.Add(new ModuleItem() { ID = 48, Title = "Refund Mode", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/RemboursementMode/" });
        //            mItems.Add(new ModuleItem() { ID = 49, Title = "Forwarder", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/Transitaire/" });
        //            mItems.Add(new ModuleItem() { ID = 50, Title = "Tranporter", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/Transporteur/" });
        //            mItems.Add(new ModuleItem() { ID = 51, Title = "Location", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/Site/" });
        //            mItems.Add(new ModuleItem() { ID = 51, Title = "Quality Agent", Icon = "../Images/icons/" + base_icon_settings, Url = base_url + "/Analyseur/" });

        //            return new List<Module>()
        //            {
        //                new Module() {  ID=5,Title="References", Items = mItems }
        //            };



        //        default:

        //            return new List<Module>()
        //            {
        //                new Module() {  ID=6,Title="None Functions", Items = mItems }
        //            };

        //    }           

        //}
    }

    public class ModuleItem
    {
        public string Title { get; set; }
        //public double ID { get; set; }
        public Guid ID { get; set; }

        public string Url { get; set; }

        public string Icon { get; set; }      
        
    }



    public class mMain
    {
        public static void SetCtrlReadOnly( string control_id, bool value=false)
        {
            //X.Js.Call("App."+ control_id+ ".setReadOnly", value);
            string jsScript = "setControlInReadOnly";
            X.Js.Call(jsScript, control_id, value);
        }

        public static void SetComboBoxReadOnly(string control_id, bool value = false)
        {
            X.GetCmp<ComboBox>(control_id).ReadOnly = value;
        }
    }
}