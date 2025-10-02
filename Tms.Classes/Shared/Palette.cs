using Ext.Net.MVC;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Components.Data;
using Tms.Classes.Shared.Sales;
using Tms.Classes.Shared.stock;
using Tms.Classes.Business;

namespace Tms.Classes.Shared
{
    public class Palette : DataPersist
    {
       
            #region "Fields"

            private Guid _ID;
            private OrdreFabrication _OrdreDeProduction;
            private int _Numero;
            private int _Annee;
            private int _Semaine;
            private string _CodeInterneArticle;
            private string _NomArticle;
            private string _Produit;
            private int _ProduitID;
            private string _TypeDeProduit;
            private int _TypeDeProduitID;

            private Magasin _Magasin;

        private int _NbreUnite;
            private Conditionnement _CodeConditionnement;
            private ConditionnementReference _CodeReferenceConditionnement;
            private int _NbreUniteParPalette;
            private int _UniteDePoids;
            private float _PoidsBrutUnitaire;
            private float _TareUnitaireEmballage;
            private float _PoidsBrutPalette;
            private int _TareEmballagePalette;
            private float _PoidsNetPalette;
            private DateTime _BestBeforeDate;
            private int? _NbreEtiquetteA4Demande;
            private int? _NbreEtiquetteA4Imprime;
            private int? _NbreEtiquetteA5Demande;
            private int? _NbreEtiquetteA5Imprime;
            private DateTime? _DateFabrication;
            private int? _QAStatut;
            private string _CodeSSCC;
            private DateTime? _DateDeclaration;
            private string _StockMagasin;
            private string _StockEmplacement;
            private string _CreationUtilisateur;
            private DateTime? _ModificationDate;
            private string _ModificationUtilisateur;
            private bool _Desactive;

            #endregion

            #region "Properties"

            [ModelField(IDProperty = true)]
            public Guid ID
            {
                get { return _ID; }
                set { _ID = value; }
            }

            public OrdreFabrication OrdreDeProduction
            {
                get { return _OrdreDeProduction; }
                set { _OrdreDeProduction = value; }
            }

        public string NumeroDeProductionToString
        {
            get { return _OrdreDeProduction != null ? _OrdreDeProduction.NumeroProduction : string.Empty; }
        }

        public int Numero
            {
                get { return _Numero; }
                set { _Numero = value; }
            }

        public Magasin Magasin
        {
            get { return _Magasin; }
            set { _Magasin = value; }
        }

        public int Annee
        {
            get { return _Annee; }
            set { _Annee = value; }
        }

        public int Semaine
        {
            get { return _Semaine; }
            set { _Semaine = value; }
        }

        public string NomArticle         {
            get { return _NomArticle; }
            set { _NomArticle = value; }
        }

        public string CodeInterneArticle
        {
            get { return _CodeInterneArticle; }
            set { _CodeInterneArticle = value; }
        }

        public string TypeDeProduit
        {
            get { return _TypeDeProduit; }
            set { _TypeDeProduit = value; }
        }

        public int TypeDeProduitID
        {
            get { return _TypeDeProduitID; }
            set { _TypeDeProduitID = value; }
        }

        public string Produit
        {
            get { return _Produit; }
            set { _Produit = value; }
        }

        public int ProduitID
        {
            get { return _ProduitID; }
            set { _ProduitID = value; }
        }

        public int NbreUnite
            {
                get { return _NbreUnite; }
                set { _NbreUnite = value; }
            }

        public Conditionnement CodeConditionnement
            {
                get { return _CodeConditionnement; }
                set { _CodeConditionnement = value; }
            }

            public string ConditionnementToString
            {
                get { return _CodeConditionnement != null ? _CodeConditionnement.Designation : string.Empty; }
            }

            public ConditionnementReference CodeReferenceConditionnement
            {
                get { return _CodeReferenceConditionnement; }
                set { _CodeReferenceConditionnement = value; }
            }

            public string CodeReferenceCondToString
            {
                get { return _CodeReferenceConditionnement != null ? _CodeReferenceConditionnement.Reference : string.Empty; }
            }

        public int NbreUniteParPalette
            {
                get { return _NbreUniteParPalette; }
                set { _NbreUniteParPalette = value; }
            }

            public int UniteDePoids
            {
                get { return _UniteDePoids; }
                set { _UniteDePoids = value; }
            }

            public float PoidsBrutUnitaire
            {
                get { return _PoidsBrutUnitaire; }
                set { _PoidsBrutUnitaire = value; }
            }

            public float TareUnitaireEmballage
            {
                get { return _TareUnitaireEmballage; }
                set { _TareUnitaireEmballage = value; }
            }

            public float PoidsBrutPalette
            {
                get { return _PoidsBrutPalette; }
                set { _PoidsBrutPalette = value; }
            }

            public int TareEmballagePalette
            {
                get { return _TareEmballagePalette; }
                set { _TareEmballagePalette = value; }
            }

            public float PoidsNetPalette
            {
                get { return _PoidsNetPalette; }
                set { _PoidsNetPalette = value; }
            }

            public DateTime BestBeforeDate
            {
                get { return _BestBeforeDate; }
                set { _BestBeforeDate = value; }
            }

            public int? NbreEtiquetteA4Demande
            {
                get { return _NbreEtiquetteA4Demande; }
                set { _NbreEtiquetteA4Demande = value; }
            }

            public int? NbreEtiquetteA4Imprime
            {
                get { return _NbreEtiquetteA4Imprime; }
                set { _NbreEtiquetteA4Imprime = value; }
            }

            public int? NbreEtiquetteA5Demande
            {
                get { return _NbreEtiquetteA5Demande; }
                set { _NbreEtiquetteA5Demande = value; }
            }

            public int? NbreEtiquetteA5Imprime
            {
                get { return _NbreEtiquetteA5Imprime; }
                set { _NbreEtiquetteA5Imprime = value; }
            }

            public DateTime? DateFabrication
            {
                get { return _DateFabrication; }
                set { _DateFabrication = value; }
            }

            public int? QAStatut
            {
                get { return _QAStatut; }
                set { _QAStatut = value; }
            }

            public string CodeSSCC
            {
                get { return _CodeSSCC; }
                set { _CodeSSCC = value; }
            }

            public DateTime? DateDeclaration
            {
                get { return _DateDeclaration; }
                set { _DateDeclaration = value; }
            }

            public string StockMagasin
            {
                get { return _StockMagasin; }
                set { _StockMagasin = value; }
            }

            public string StockEmplacement
            {
                get { return _StockEmplacement; }
                set { _StockEmplacement = value; }
            }

            public string CreationUtilisateur
            {
                get { return _CreationUtilisateur; }
                set { _CreationUtilisateur = value; }
            }

            public DateTime? ModificationDate
            {
                get { return _ModificationDate; }
                set { _ModificationDate = value; }
            }

            public string ModificationUtilisateur
            {
                get { return _ModificationUtilisateur; }
                set { _ModificationUtilisateur = value; }
            }

            public bool Desactive
            {
                get { return _Desactive; }
                set { _Desactive = value; }
            }

            public int mIcon
            {
                get
                {
                    if (_Desactive)
                        return 0; // Icône pour désactivé
                    else
                        return 2; // Icône pour actif
                }
            }

        #endregion


        #region Methods
        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("pp_Palette_Get", (Guid)Id);
                if (mDataReader.Read())
                {
                    MapFromDataReader(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGet");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }


        public bool fnPaletteByOf_Get(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("pp_PaletteByOf_Get", (Guid)Id);
                if (mDataReader.Read())
                {
                    MapFromDataReader3(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGet");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public bool fnGetFabrication(OrdreFabrication obj)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("pp_OrdreFabrication_Get", (Guid)obj.ID);
                if (mDataReader.Read())
                {
                    MapFromDataReader4(obj, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGet");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public bool fnGetArticle(Guid ID)
        {
            IDataReader mDataReader = null;
            Article obj = new Article();

            try
            {
                mDataReader = db().ExecuteReader("PP_Article_Get", (Guid)ID);
                if (mDataReader.Read())
                {
                    MapFromDataReader5(obj, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGet");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public bool fnGetDefaultPalette()
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("Palette_GetDefault");
                if (mDataReader.Read())
                {
                    MapFromDataReader(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetDefaultPalette");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public bool fnGetByPaletteByUserName(object userName)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("Palette_GetByUserName", (string)userName);
                if (mDataReader.Read())
                {
                    MapFromDataReade2(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGet");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }





        public List<Palette> fnGetPaletteByProduction(Guid Id)
        {
            IDataReader mDataReader = null;
            var palettes = new List<Palette>();

            try
            {
                mDataReader = db().ExecuteReader("pp_PaletteNextByFabric_Get", Id);

                while (mDataReader.Read())
                {
                    var palette = new Palette();

                    if (!DBNull.Value.Equals(mDataReader["ID"]))
                        palette._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["Numero"]))
                        palette._Numero = Convert.ToInt32(mDataReader["Numero"]);

                    palettes.Add(palette);
                }

                return palettes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetPaletteByProduction", ex);
            }
            finally
            {
                mDataReader?.Close();
            }
        }


     

        public override List<DataPersist> fnSelect()
        {
            return fnSelect("-1", -1, -1,-1, -1, null);
        }

        public List<DataPersist> fnSelect(string Status, int Annee, int Semaine, int Produit, int ProduitType, string Production)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("pp_Palette_Select");
                db().AddInParameter(mCommande, "@Statut", SqlDbType.VarChar, Status);
                db().AddInParameter(mCommande, "@Annee", SqlDbType.Int, Annee);
                db().AddInParameter(mCommande, "@Semaine", SqlDbType.Int, Semaine);
                db().AddInParameter(mCommande, "@Produit", SqlDbType.Int, Produit);
                db().AddInParameter(mCommande, "@TypeDeProduit", SqlDbType.Int, ProduitType);

                if (string.IsNullOrEmpty(Production) || Production == "0" || Production == Guid.Empty.ToString())
                {
                    db().AddInParameter(mCommande, "@Production", SqlDbType.UniqueIdentifier, DBNull.Value);
                }
                else
                {
                    db().AddInParameter(mCommande, "@Production", SqlDbType.UniqueIdentifier, new Guid(Production));
                }
                //db().AddInParameter(mCommande, "@Production", SqlDbType.UniqueIdentifier, Production);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Palette mClass = new Palette();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelect");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public override bool fnUpdate()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {
                    mCommande = db().CreateStoredProcCommand("pp_Palette_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                    db().AddOutParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("pp_Palette_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                    db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 8, _RowVersionKey, ParameterDirection.InputOutput);
                }

                // Ajout des paramètres communs à la création et à la modification
                db().AddInParameter(mCommande, "@NbreUnite", SqlDbType.Int, _NbreUnite);
                db().AddInParameter(mCommande, "@NbreUniteParPalette", SqlDbType.Int, _NbreUniteParPalette);
                db().AddInParameter(mCommande, "@UniteDePoids", SqlDbType.Int, _UniteDePoids);
                db().AddInParameter(mCommande, "@PoidsBrutUnitaire", SqlDbType.Float, _PoidsBrutUnitaire);
                db().AddInParameter(mCommande, "@TareUnitaireEmballage", SqlDbType.Float, _TareUnitaireEmballage);
                db().AddInParameter(mCommande, "@PoidsBrutPalette", SqlDbType.Float, _PoidsBrutPalette);
                db().AddInParameter(mCommande, "@TareEmballagePalette", SqlDbType.Int, _TareEmballagePalette);
                db().AddInParameter(mCommande, "@PoidsNetPalette", SqlDbType.Float, _PoidsNetPalette);
                db().AddInParameter(mCommande, "@BestBeforeDate", SqlDbType.DateTime, _BestBeforeDate);
                db().AddInParameter(mCommande, "@NbreEtiquetteA4Demande", SqlDbType.Int, _NbreEtiquetteA4Demande);
                db().AddInParameter(mCommande, "@NbreEtiquetteA4Imprime", SqlDbType.Int, _NbreEtiquetteA4Imprime);
                db().AddInParameter(mCommande, "@NbreEtiquetteA5Demande", SqlDbType.Int, _NbreEtiquetteA5Demande);
                db().AddInParameter(mCommande, "@NbreEtiquetteA5Imprime", SqlDbType.Int, _NbreEtiquetteA5Imprime);
                db().AddInParameter(mCommande, "@DateFabrication", SqlDbType.DateTime, _DateFabrication);
                db().AddInParameter(mCommande, "@QAStatut", SqlDbType.Int, _QAStatut);
                db().AddInParameter(mCommande, "@CodeSSCC", SqlDbType.VarChar, _CodeSSCC);
                db().AddInParameter(mCommande, "@DateDeclaration", SqlDbType.DateTime, _DateDeclaration);
                db().AddInParameter(mCommande, "@StockMagasin", SqlDbType.VarChar, _StockMagasin);
                db().AddInParameter(mCommande, "@StockEmplacement", SqlDbType.VarChar, _StockEmplacement);

                

                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                //db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);

                db().ExecuteNonQuery(ref mCommande);

                

                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        _ID = (Guid)db().Parameters(mCommande, "@ID");
                        _isnew = false;
                        return true;
                    default:
                        throw new Exception("Échec lors de la modification de la palette.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\r\n" + "Palette:fnUpdate");
            }
        }



        public bool fnUpdateDeclaration()
        {
            bool Result;
            DataCommand mCommande;
            //this._ModificationUtilisateur = modificationUser;
            try
            {

                mCommande = db().CreateStoredProcCommand("pp_Palette_Declare");
                db().AddInParameter(mCommande, "@OrdreDeFabricationID", SqlDbType.UniqueIdentifier, _OrdreDeProduction.ID);
                db().AddInParameter(mCommande, "@PaletteID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@Annee", SqlDbType.Int, _Annee);
                db().AddInParameter(mCommande, "@Semaine", SqlDbType.Int, _Semaine);
                db().AddInParameter(mCommande, "@ProduitID", SqlDbType.Int, _ProduitID);
                db().AddInParameter(mCommande, "@ProduitTypeID", SqlDbType.Int, _TypeDeProduitID);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _ModificationUtilisateur);
                db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 8, _RowVersionKey, ParameterDirection.InputOutput);


                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                db().ExecuteNonQuery(ref mCommande);



                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        _ID = (Guid)db().Parameters(mCommande, "@OrdreDeFabricationID");
                        _isnew = false;
                        return true;
                    default:
                        throw new Exception("Échec lors de la modification de la palette.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\r\n" + "Palette:fnUpdateDeclaration");
            }
        }


        public bool fnUpdateMouvement(MouvementStockProduitDto obj)
        {
            
            DataCommand mCommande;
            try
            {
                if (obj == null)
                    throw new ArgumentNullException(nameof(obj));

                mCommande = db().CreateStoredProcCommand("ps_MouvementStockProduit_new");

                

                // Paramètres d'entrée
                db().AddInParameter(mCommande, "@CodeMagasin", SqlDbType.Int, obj.MpCodeMagasin);
                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, obj.MpDate);
                db().AddInParameter(mCommande, "@CodePalette", SqlDbType.UniqueIdentifier, obj.MpCodePalette);

                // ProcessID non fourni dans DTO -> passer NULL
                db().AddInParameter(mCommande, "@ProcessID", SqlDbType.UniqueIdentifier, DBNull.Value);

                db().AddInParameter(mCommande, "@CodeTypeMouvement", SqlDbType.Int, obj.MpCodeTypeMouvement ?? 0);
                db().AddInParameter(mCommande, "@Sens", SqlDbType.Int, obj.MpSens);
                db().AddInParameter(mCommande, "@CodeConditionnement", SqlDbType.Int, obj.MpCodeConditionnement);
                db().AddInParameter(mCommande, "@CodeReferenceConditionnement", SqlDbType.VarChar, obj.MpCodeReferenceConditionnement);
                db().AddInParameter(mCommande, "@NbreUniteParPalette", SqlDbType.Int, obj.MpNbreUniteParPalette);
                db().AddInParameter(mCommande, "@UniteDePoids", SqlDbType.VarChar, obj.MpUniteDePoids ?? "kg");

                // SQL attend FLOAT -> convertir en double
                db().AddInParameter(mCommande, "@PoidsBrutUnitaire", SqlDbType.Float, Convert.ToDouble(obj.MpPoidsBrutUnitaire));
                db().AddInParameter(mCommande, "@TareUnitaireEmballage", SqlDbType.Float, Convert.ToDouble(obj.MpTareUnitaireEmballage));
                db().AddInParameter(mCommande, "@PoidsBrutPalette", SqlDbType.Float, Convert.ToDouble(obj.MpPoidsBrutPalette));
                db().AddInParameter(mCommande, "@TareEmballagePalette", SqlDbType.Float, Convert.ToDouble(obj.MpTareEmballagePalette));
                db().AddInParameter(mCommande, "@PoidsNetPalette", SqlDbType.Float, Convert.ToDouble(obj.MpPoidsNetPalette));

                //db().AddInParameter(mCommande, "@Statut", SqlDbType.VarChar, DBNull.Value);

                // Création user : on utilise la même variable que dans fnUpdateDeclaration
                db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _CreationUtilisateur);

                // Paramètres OUTPUT
                // @ID uniqueidentifier OUTPUT
                db().AddParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 16, null, ParameterDirection.Output);

                // @RowVersion timestamp OUTPUT (taille 8)
                db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 8, null, ParameterDirection.Output);

                // @ErrorMessage varchar(1000) OUTPUT
                db().AddParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000, null, ParameterDirection.Output);

                // ReturnValue
                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                // Exécution
                db().ExecuteNonQuery(ref mCommande);

                int returnValue = (int)db().Parameters(mCommande, "ReturnValue");

                switch (returnValue)
                {
                    case 0:
                        // Récupère RowVersion et ID renvoyés
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        // Met à jour éventuellement l'ID si tu veux le conserver côté objet
                        _ID = (Guid)db().Parameters(mCommande, "@ID");

                        // Optionnel : si tu veux récupérer l'ID retourné par la procédure pour l'exposer, tu peux l'assigner quelque part
                        return true;

                    default:
                        string err = db().Parameters(mCommande, "@ErrorMessage") != null ?
                                     db().Parameters(mCommande, "@ErrorMessage").ToString() :
                                     "Échec lors de l'insertion du mouvement.";
                        throw new Exception(err);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\r\n" + "Palette:fnUpdateMouvement");
            }
        }




        public bool fnAutoCreation(OrdreFabrication OrdreProd)
        {
            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("pp_Palette_New");

                // Paramètres d'entrée seulement
                db().AddInParameter(mCommande, "@OrdreDeProductionId", SqlDbType.UniqueIdentifier, OrdreProd.ID);
                db().AddInParameter(mCommande, "@ArticleId", SqlDbType.UniqueIdentifier, OrdreProd.ArticleID);
                db().AddInParameter(mCommande, "@NombrePalettes", SqlDbType.Int, OrdreProd.NbrePaletteAProduire);

                // Paramètre de retour
                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                db().ExecuteNonQuery(ref mCommande);

                return (int)db().Parameters(mCommande, "ReturnValue") == 0;
            }
            catch (Exception ex)
            {
                // Gestion des erreurs
                throw new Exception("Erreur création palettes: " + ex.Message);
            }
        }

        public List<DataPersist> fnSelectToPrice(Guid priceID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_PrixJournalier_SelectPalettes");

                db().AddInParameter(mCommande, "@priceID", SqlDbType.UniqueIdentifier, priceID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Palette mClass = new Palette();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectToPrice");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("pp_Palette_Activate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
            db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
            db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
            db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
            try
            {
                db().ExecuteNonQuery(ref mCommande);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        Result = true;
                        Desactive = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        break;
                    default:
                        Result = false;
                        string ErrorMessage = (string)db().Parameters(mCommande, "@ErrorMessage");
                        throw new Exception(ErrorMessage);
                        break;
                }
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + "\r\n" + "Palette:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("pp_Palette_DeActivate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
            db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
            db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
            db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
            try
            {
                db().ExecuteNonQuery(ref mCommande);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        Result = true;
                        //Desactive = true;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        break;
                    default:
                        Result = false;
                        string ErrorMessage = (string)db().Parameters(mCommande, "@ErrorMessage");
                        throw new Exception(ErrorMessage);
                        break;
                }
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + "\r\n" + "Palette:fnDeActivate");
            }
            return Result;
        }

        public bool fnGetByUserName(object userName)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("Palette_GetByUserName", (string)userName);
                if (mDataReader.Read())
                {
                    MapFromDataReader(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGet");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectAvailableForFS(int userPalette, int FournisseurID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_Palette_SelectAvailableForSupplier");
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@userPalette", SqlDbType.Int, userPalette);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Palette mClass = new Palette();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelect");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectAvailableForAnalyseur(int analyseurID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_AnalyseurPalette_PalettesAvailables");
                //db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, fournisseurID);
                db().AddInParameter(mCommande, "@AnalyseurID", SqlDbType.Int, analyseurID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Palette mClass = new Palette();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelect");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }


        public List<DataPersist> fnSelectAvailableForPrice(string userName)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_Palette_SelectAvailableForPrice");

                db().AddInParameter(mCommande, "@userName", SqlDbType.VarChar, 100, userName);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Palette mClass = new Palette();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelect");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }
        #endregion

        #region "Private Members"




       

        private static void MapFromDataReader(Palette mClass, IDataReader rdr)
        {
            try
            {
                if (rdr == null || rdr.IsClosed)
                    return;

                mClass.IsNew = false;

                // ID
                if (!DBNull.Value.Equals(rdr["ID"]))
                    mClass._ID = (Guid)rdr["ID"];

                // Ordre de Production (injection de l'objet complexe)
                if (!DBNull.Value.Equals(rdr["OrdreDeProduction"]))
                {
                    Guid ofId = (Guid)rdr["OrdreDeProduction"];
                    string numeroProduction = (string)rdr["NumeroProduction"];
                    mClass._OrdreDeProduction = new OrdreFabrication { ID = ofId, NumeroProduction = numeroProduction };
                }

                // Numéro et nombre d'unités
                if (!DBNull.Value.Equals(rdr["Numero"]))
                    mClass._Numero = Convert.ToInt32(rdr["Numero"]);
                if (!DBNull.Value.Equals(rdr["NbreUnite"]))
                    mClass._NbreUnite = Convert.ToInt32(rdr["NbreUnite"]);

                // Conditionnement
                if (!DBNull.Value.Equals(rdr["CodeConditionnement"]))
                {
                    int condId = Convert.ToInt32(rdr["CodeConditionnement"]);
                    string CondDesignation = !DBNull.Value.Equals(rdr["CondDesignation"])
                     ? (string)rdr["CondDesignation"]
                     : null;

                    mClass._CodeConditionnement = new Conditionnement { ID = condId, Designation=CondDesignation };
                }
                if (!DBNull.Value.Equals(rdr["CodeReferenceConditionnement"])) 
                {
                    int crefId = Convert.ToInt32(rdr["CodeReferenceConditionnement"]);
                    string CondRefConditionnement = !DBNull.Value.Equals(rdr["CondRefDesignation"])
                     ? (string)rdr["CondDesignation"]
                     : null;
                    mClass._CodeReferenceConditionnement = new ConditionnementReference { ID = crefId, Reference= CondRefConditionnement };
                }

                // Détails poids / nombre d'unités
                if (!DBNull.Value.Equals(rdr["NbreUniteParPalette"]))
                    mClass._NbreUniteParPalette = Convert.ToInt32(rdr["NbreUniteParPalette"]);
                if (!DBNull.Value.Equals(rdr["UniteDePoids"]))
                    mClass._UniteDePoids = Convert.ToInt32(rdr["UniteDePoids"]);
                if (!DBNull.Value.Equals(rdr["PoidsBrutUnitaire"]))
                    mClass._PoidsBrutUnitaire = Convert.ToSingle(rdr["PoidsBrutUnitaire"]);
                if (!DBNull.Value.Equals(rdr["TareUnitaireEmballage"]))
                    mClass._TareUnitaireEmballage = Convert.ToSingle(rdr["TareUnitaireEmballage"]);
                if (!DBNull.Value.Equals(rdr["PoidsBrutPalette"]))
                    mClass._PoidsBrutPalette = Convert.ToSingle(rdr["PoidsBrutPalette"]);
                if (!DBNull.Value.Equals(rdr["TareEmballagePalette"]))
                    mClass._TareEmballagePalette = Convert.ToInt32(rdr["TareEmballagePalette"]);
                if (!DBNull.Value.Equals(rdr["PoidsNetPalette"]))
                    mClass._PoidsNetPalette = Convert.ToSingle(rdr["PoidsNetPalette"]);

                if (!DBNull.Value.Equals(rdr["Annee"]))
                    mClass._Annee = Convert.ToInt32(rdr["Annee"]);
                if (!DBNull.Value.Equals(rdr["Semaine"]))
                    mClass._Semaine = Convert.ToInt32(rdr["Semaine"]);
                if (!DBNull.Value.Equals(rdr["NomArticle"]))
                    mClass._NomArticle = (string)(rdr["NomArticle"]);
                if (!DBNull.Value.Equals(rdr["CodeArticle"]))
                    mClass._CodeInterneArticle = (string)(rdr["CodeArticle"]);
                if (!DBNull.Value.Equals(rdr["ProduitDesignation"]))
                    mClass._Produit = (string)(rdr["ProduitDesignation"]);
                if (!DBNull.Value.Equals(rdr["TypeProduitDesignation"]))
                    mClass._TypeDeProduit = (string)(rdr["TypeProduitDesignation"]);

                // Dates et statuts
                if (!DBNull.Value.Equals(rdr["BestBeforeDate"]))
                    mClass._BestBeforeDate = (DateTime)rdr["BestBeforeDate"];
                if (!DBNull.Value.Equals(rdr["NbreEtiquetteA4Demande"]))
                    mClass._NbreEtiquetteA4Demande = Convert.ToInt32(rdr["NbreEtiquetteA4Demande"]);
                if (!DBNull.Value.Equals(rdr["NbreEtiquetteA4Imprime"]))
                    mClass._NbreEtiquetteA4Imprime = Convert.ToInt32(rdr["NbreEtiquetteA4Imprime"]);
                if (!DBNull.Value.Equals(rdr["NbreEtiquetteA5Demande"]))
                    mClass._NbreEtiquetteA5Demande = Convert.ToInt32(rdr["NbreEtiquetteA5Demande"]);
                if (!DBNull.Value.Equals(rdr["NbreEtiquetteA5Imprime"]))
                    mClass._NbreEtiquetteA5Imprime = Convert.ToInt32(rdr["NbreEtiquetteA5Imprime"]);
                if (!DBNull.Value.Equals(rdr["DateFabrication"]))
                    mClass._DateFabrication = (DateTime)rdr["DateFabrication"];
                if (!DBNull.Value.Equals(rdr["QAStatut"]))
                    mClass._QAStatut = Convert.ToInt32(rdr["QAStatut"]);

                // Références et déclarations
                if (!DBNull.Value.Equals(rdr["CodeSSCC"]))
                    mClass._CodeSSCC = (string)rdr["CodeSSCC"];

                if (!DBNull.Value.Equals(rdr["DateDeclaration"]))
                    mClass._DateDeclaration = (DateTime)rdr["DateDeclaration"];

                // Stocks
                if (!DBNull.Value.Equals(rdr["StockMagasin"]))
                    mClass._StockMagasin = (string)rdr["StockMagasin"];
                if (!DBNull.Value.Equals(rdr["StockEmplacement"]))
                    mClass._StockEmplacement = (string)rdr["StockEmplacement"];

                // Audit
                if (!DBNull.Value.Equals(rdr["CreationUtilisateur"]))
                    mClass._CreationUtilisateur = (string)rdr["CreationUtilisateur"];
                if (!DBNull.Value.Equals(rdr["ModificationDate"]))
                    mClass._ModificationDate = (DateTime)rdr["ModificationDate"];
                if (!DBNull.Value.Equals(rdr["ModificationUtilisateur"]))
                    mClass._ModificationUtilisateur = (string)rdr["ModificationUtilisateur"];

                // RowVersion
                if (!DBNull.Value.Equals(rdr["RowVersionKey"])) mClass._RowVersionKey = (object)rdr["RowVersionKey"];


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nPalette:MapFromDataReader");
            }
        }


        private static void MapFromDataReader3(Palette mClass, IDataReader rdr)
        {
            try
            {
                if (rdr == null || rdr.IsClosed)
                    return;

                mClass.IsNew = false;

                // ID
                if (!DBNull.Value.Equals(rdr["PaletteID"]))
                    mClass._ID = (Guid)rdr["PaletteID"];

                // Ordre de Production (injection de l'objet complexe)
                if (!DBNull.Value.Equals(rdr["OrdreDeFabricationID"]))
                {
                    Guid ofId = (Guid)rdr["OrdreDeFabricationID"];
                    string numeroProduction = (string)rdr["NumeroProduction"];
                    mClass._OrdreDeProduction = new OrdreFabrication { ID = ofId, NumeroProduction = numeroProduction };
                }

                // Numéro et nombre d'unités
                if (!DBNull.Value.Equals(rdr["Numero"]))
                    mClass._Numero = Convert.ToInt32(rdr["Numero"]);
                if (!DBNull.Value.Equals(rdr["NbreUnite"]))
                    mClass._NbreUnite = Convert.ToInt32(rdr["NbreUnite"]);

                // Conditionnement
                if (!DBNull.Value.Equals(rdr["CodeConditionnement"]))
                {
                    int condId = Convert.ToInt32(rdr["CodeConditionnement"]);
                    string CondDesignation = !DBNull.Value.Equals(rdr["CondDesignation"])
                     ? (string)rdr["CondDesignation"]
                     : null;

                    mClass._CodeConditionnement = new Conditionnement { ID = condId, Designation = CondDesignation };
                }
                if (!DBNull.Value.Equals(rdr["CodeReferenceConditionnement"]))
                {
                    int crefId = Convert.ToInt32(rdr["CodeReferenceConditionnement"]);
                    string CondRefConditionnement = !DBNull.Value.Equals(rdr["CondRefDesignation"])
                     ? (string)rdr["CondDesignation"]
                     : null;
                    mClass._CodeReferenceConditionnement = new ConditionnementReference { ID = crefId, Reference = CondRefConditionnement };
                }

                // Détails poids / nombre d'unités
                if (!DBNull.Value.Equals(rdr["NbreUniteParPalette"]))
                    mClass._NbreUniteParPalette = Convert.ToInt32(rdr["NbreUniteParPalette"]);
                if (!DBNull.Value.Equals(rdr["UniteDePoids"]))
                    mClass._UniteDePoids = Convert.ToInt32(rdr["UniteDePoids"]);
                if (!DBNull.Value.Equals(rdr["PoidsBrutUnitaire"]))
                    mClass._PoidsBrutUnitaire = Convert.ToSingle(rdr["PoidsBrutUnitaire"]);
                if (!DBNull.Value.Equals(rdr["TareUnitaireEmballage"]))
                    mClass._TareUnitaireEmballage = Convert.ToSingle(rdr["TareUnitaireEmballage"]);
                if (!DBNull.Value.Equals(rdr["PoidsBrutPalette"]))
                    mClass._PoidsBrutPalette = Convert.ToSingle(rdr["PoidsBrutPalette"]);
                if (!DBNull.Value.Equals(rdr["TareEmballagePalette"]))
                    mClass._TareEmballagePalette = Convert.ToInt32(rdr["TareEmballagePalette"]);
                if (!DBNull.Value.Equals(rdr["PoidsNetPalette"]))
                    mClass._PoidsNetPalette = Convert.ToSingle(rdr["PoidsNetPalette"]);

                // Dates et statuts
                if (!DBNull.Value.Equals(rdr["BestBeforeDate"]))
                    mClass._BestBeforeDate = (DateTime)rdr["BestBeforeDate"];
                if (!DBNull.Value.Equals(rdr["NbreEtiquetteA4Demande"]))
                    mClass._NbreEtiquetteA4Demande = Convert.ToInt32(rdr["NbreEtiquetteA4Demande"]);
                if (!DBNull.Value.Equals(rdr["NbreEtiquetteA4Imprime"]))
                    mClass._NbreEtiquetteA4Imprime = Convert.ToInt32(rdr["NbreEtiquetteA4Imprime"]);
                if (!DBNull.Value.Equals(rdr["NbreEtiquetteA5Demande"]))
                    mClass._NbreEtiquetteA5Demande = Convert.ToInt32(rdr["NbreEtiquetteA5Demande"]);
                if (!DBNull.Value.Equals(rdr["NbreEtiquetteA5Imprime"]))
                    mClass._NbreEtiquetteA5Imprime = Convert.ToInt32(rdr["NbreEtiquetteA5Imprime"]);
                if (!DBNull.Value.Equals(rdr["DateFabrication"]))
                    mClass._DateFabrication = (DateTime)rdr["DateFabrication"];
                if (!DBNull.Value.Equals(rdr["QAStatut"]))
                    mClass._QAStatut = Convert.ToInt32(rdr["QAStatut"]);

                // Références et déclarations
                if (!DBNull.Value.Equals(rdr["CodeSSCC"]))
                    mClass._CodeSSCC = (string)rdr["CodeSSCC"];

                if (!DBNull.Value.Equals(rdr["DateDeclaration"]))
                    mClass._DateDeclaration = (DateTime)rdr["DateDeclaration"];

                // Stocks
                if (!DBNull.Value.Equals(rdr["StockMagasin"]))
                    mClass._StockMagasin = (string)rdr["StockMagasin"];
                if (!DBNull.Value.Equals(rdr["StockEmplacement"]))
                    mClass._StockEmplacement = (string)rdr["StockEmplacement"];

                // Audit
                if (!DBNull.Value.Equals(rdr["CreationUtilisateur"]))
                    mClass._CreationUtilisateur = (string)rdr["CreationUtilisateur"];
                if (!DBNull.Value.Equals(rdr["ModificationDate"]))
                    mClass._ModificationDate = (DateTime)rdr["ModificationDate"];
                if (!DBNull.Value.Equals(rdr["ModificationUtilisateur"]))
                    mClass._ModificationUtilisateur = (string)rdr["ModificationUtilisateur"];

                // RowVersion
                if (!DBNull.Value.Equals(rdr["RowVersionKey"])) mClass._RowVersionKey = (object)rdr["RowVersionKey"];


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nPalette:MapFromDataReader");
            }
        }

        private static void MapFromDataReader4(OrdreFabrication mClass, IDataReader rdr)
        {
            try
            {
                if (rdr == null || rdr.IsClosed)
                    return;

                mClass.IsNew = false;

                // Mappage de l'ID de l'Ordre de Fabrication
                if (!DBNull.Value.Equals(rdr["ID"]))
                    mClass.ID = (Guid)rdr["ID"];

                // Mappage des informations de l'article
                mClass.Article = new Article();

                if (!DBNull.Value.Equals(rdr["ArticleCode"])) mClass.Article.ID = (Guid)rdr["ArticleCode"];
                if (!DBNull.Value.Equals(rdr["ArticleDesignation"])) mClass.Article.Nom = (string)rdr["ArticleDesignation"];                    

                // Mappage des informations de conditionnement et de produit
                if (!DBNull.Value.Equals(rdr["ConditionnementCode"]))
                    mClass.Conditionnement = new Conditionnement
                    {
                        ID = (int)rdr["ConditionnementCode"],
                        Designation = !DBNull.Value.Equals(rdr["ConditionnementDesignation"]) ? (string)rdr["ConditionnementDesignation"] : null
                    };
                if (!DBNull.Value.Equals(rdr["ProduitCode"]))
                    mClass.Produit = new Produit
                    {
                        ID = (int)rdr["ProduitCode"],
                        Designation = !DBNull.Value.Equals(rdr["ProduitDesignation"]) ? (string)rdr["ProduitDesignation"] : null
                    };
                if (!DBNull.Value.Equals(rdr["TypeProduitCode"]))
                    mClass.TypeDeProduit = new ProduitType
                    {
                        ID = (int)rdr["TypeProduitCode"],
                        Designation = !DBNull.Value.Equals(rdr["TypeProduitDesignation"]) ? (string)rdr["TypeProduitDesignation"] : null
                    };
                if (!DBNull.Value.Equals(rdr["ConditionnementRefCode"]))
                    mClass.ConditionnementReference = new ConditionnementReference
                    {
                        ID = (int)rdr["ConditionnementRefCode"]};

                // Mappage des détails techniques de l'article
               
               

                // Mappage des informations de l'ordre de production
                if (!DBNull.Value.Equals(rdr["Annee"]))
                    mClass.Annee = Convert.ToInt32(rdr["Annee"]);
                if (!DBNull.Value.Equals(rdr["Semaine"]))
                    mClass.Semaine = Convert.ToInt32(rdr["Semaine"]);
                if (!DBNull.Value.Equals(rdr["LigneProductionCode"]))
                    mClass.LigneDeProduction = new LigneProduction
                    {
                        ID = (int)rdr["LigneProductionCode"],
                        Designation = !DBNull.Value.Equals(rdr["LigneProductionDesignation"]) ? (string)rdr["LigneProductionDesignation"] : null
                    };
                if (!DBNull.Value.Equals(rdr["NumeroProduction"]))
                    mClass.NumeroProduction = (string)rdr["NumeroProduction"];
                if (!DBNull.Value.Equals(rdr["ReferenceExterne"]))
                    mClass.ReferenceExterne = (string)rdr["ReferenceExterne"];
                if (!DBNull.Value.Equals(rdr["RecolteCode"]))
                    mClass.Recolte = new Recolte
                    {
                        ID = Convert.ToInt32(rdr["RecolteCode"]),
                        Designation = !DBNull.Value.Equals(rdr["RecolteDesignation"]) ? (string)rdr["RecolteDesignation"] : null
                    };
                if (!DBNull.Value.Equals(rdr["ClientCode"]))
                    mClass.Client = new Client
                    {
                        ID = Convert.ToInt32(rdr["ClientCode"]),
                        Nom = !DBNull.Value.Equals(rdr["ClientNom"]) ? (string)rdr["ClientNom"] : null
                    };
                if (!DBNull.Value.Equals(rdr["NombrePaletteAProduire"]))
                    mClass.NbrePaletteAProduire = Convert.ToInt32(rdr["NombrePaletteAProduire"]);

                if (!DBNull.Value.Equals(rdr["DatePrevue"]))
                    mClass.DateEffective = (DateTime)rdr["DatePrevue"];
                if (!DBNull.Value.Equals(rdr["DateDebut"]))
                    mClass.DateDebutProduction = (DateTime)rdr["DateDebut"];
                if (!DBNull.Value.Equals(rdr["DateFin"]))
                    mClass.DateFinProduction = (DateTime)rdr["DateFin"];
                if (!DBNull.Value.Equals(rdr["Statut"]))
                    mClass.Statut = (string)(rdr["Statut"]);
                if (!DBNull.Value.Equals(rdr["Desactive"]))
                    mClass.Desactive = (bool)rdr["Desactive"];

                // Mappage des champs d'audit
                if (!DBNull.Value.Equals(rdr["CreationDate"]))
                    mClass.DateCreation = (DateTime)rdr["CreationDate"];
                if (!DBNull.Value.Equals(rdr["CreationUtilisateur"]))
                    mClass.UtilisateurCreation = (string)rdr["CreationUtilisateur"];
                if (!DBNull.Value.Equals(rdr["ModificationDate"]))
                    mClass.DateModification = (DateTime)rdr["ModificationDate"];
                if (!DBNull.Value.Equals(rdr["ModificationUtilisateur"]))
                    mClass.UtilisateurModification = (string)rdr["ModificationUtilisateur"];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nOrdreFabrication:MapFromDataReader4");
            }
        }

        private static void MapFromDataReader5(Article mClass, IDataReader rdr)
        {
            try
            {
                if (rdr == null || rdr.IsClosed)
                    return;

                mClass.IsNew = false;

                // Champs principaux
                if (!DBNull.Value.Equals(rdr["ID"])) mClass.ID = (Guid)rdr["ID"];
                if (!DBNull.Value.Equals(rdr["Code"])) mClass.Code = (string)rdr["Code"];
                if (!DBNull.Value.Equals(rdr["CodeEtendu"])) mClass.CodeEtendu = (string)rdr["CodeEtendu"];
                if (!DBNull.Value.Equals(rdr["Nom"])) mClass.Nom = (string)rdr["Nom"];
                if (!DBNull.Value.Equals(rdr["Description"])) mClass.Description = (string)rdr["Description"];

                // Détails techniques
                if (!DBNull.Value.Equals(rdr["NbreUniteParPalette"])) mClass.NbreUniteParPalette = Convert.ToInt32(rdr["NbreUniteParPalette"]);
                if (!DBNull.Value.Equals(rdr["PoidsBrutUnitaire"])) mClass.PoidsBrutUnitaire = Convert.ToDecimal(rdr["PoidsBrutUnitaire"]);
                if (!DBNull.Value.Equals(rdr["TareUnitaireEmballage"])) mClass.TareUnitaireEmballage = Convert.ToDecimal(rdr["TareUnitaireEmballage"]);
                if (!DBNull.Value.Equals(rdr["PoidsBrutPalette"])) mClass.PoidsBrutPalette = Convert.ToDecimal(rdr["PoidsBrutPalette"]);
                if (!DBNull.Value.Equals(rdr["TareEmballagePalette"])) mClass.TareEmballagePalette = (int)rdr["TareEmballagePalette"];
                if (!DBNull.Value.Equals(rdr["PoidsNetPalette"])) mClass.PoidsNetPalette = (int)rdr["PoidsNetPalette"];
                if (!DBNull.Value.Equals(rdr["BestBeforeDate"])) mClass.BestBeforeDate = (DateTime)rdr["BestBeforeDate"];
                if (!DBNull.Value.Equals(rdr["NbreTiquetteParDefaut"])) mClass.NbreTiquetteParDefaut = Convert.ToInt32(rdr["NbreTiquetteParDefaut"]);
                if (!DBNull.Value.Equals(rdr["CodeGTIN"])) mClass.CodeGTIN = (string)rdr["CodeGTIN"];

                // Nouveaux champs poids & tare
                if (!DBNull.Value.Equals(rdr["TarePaletteVide"])) mClass.TarePaletteVide = Convert.ToDecimal(rdr["TarePaletteVide"]);
                if (!DBNull.Value.Equals(rdr["TareTotaleEmballage"])) mClass.TareTotaleEmballage = Convert.ToDecimal(rdr["TareTotaleEmballage"]);
                if (!DBNull.Value.Equals(rdr["PoidsBrutTotal"])) mClass.PoidsBrutTotal = Convert.ToDecimal(rdr["PoidsBrutTotal"]);
                if (!DBNull.Value.Equals(rdr["PoidsNetTotalPalette"])) mClass.PoidsNetTotalPalette = Convert.ToDecimal(rdr["PoidsNetTotalPalette"]);

                // Produit
                if (!DBNull.Value.Equals(rdr["ProduitID"]))
                    mClass.Produit = new Produit
                    {
                        ID = (int)rdr["ProduitID"],
                        Designation = !DBNull.Value.Equals(rdr["ProduitDesignation"]) ? (string)rdr["ProduitDesignation"] : null
                    };

                // Produit Gamme
                if (!DBNull.Value.Equals(rdr["ProduitGammeID"]))
                    mClass.ProduitGamme = new ProduitGamme
                    {
                        ID = (int)rdr["ProduitGammeID"],
                        Designation = !DBNull.Value.Equals(rdr["ProduitGammeDesignation"]) ? (string)rdr["ProduitGammeDesignation"] : null
                    };

                // Ligne de production
                if (!DBNull.Value.Equals(rdr["LigneProductionID"]))
                    mClass.LigneProduction = new LigneProduction
                    {
                        ID = (int)rdr["LigneProductionID"],
                        Designation = !DBNull.Value.Equals(rdr["LigneProductionDesignation"]) ? (string)rdr["LigneProductionDesignation"] : null
                    };

                // Type de produit
                if (!DBNull.Value.Equals(rdr["TypeProduitID"]))
                    mClass.ProduitType = new ProduitType
                    {
                        ID = (int)rdr["TypeProduitID"],
                        Designation = !DBNull.Value.Equals(rdr["TypeProduitDesignation"]) ? (string)rdr["TypeProduitDesignation"] : null
                    };

                // Marque de produit
                if (!DBNull.Value.Equals(rdr["MarqueProduitID"]))
                    mClass.MarqueProduit = new MarqueProduit
                    {
                        ID = (int)rdr["MarqueProduitID"],
                        Designation = !DBNull.Value.Equals(rdr["MarqueProduitDesignation"]) ? (string)rdr["MarqueProduitDesignation"] : null
                    };

                // Unité de poids
                if (!DBNull.Value.Equals(rdr["UniteDePoidsID"]))
                    mClass.UniteDePoids = new UniteDePoids
                    {
                        ID = (int)rdr["UniteDePoidsID"],
                        Designation = !DBNull.Value.Equals(rdr["UniteDePoidsDesignation"]) ? (string)rdr["UniteDePoidsDesignation"] : null
                    };

                // Conditionnement Référence
                if (!DBNull.Value.Equals(rdr["ConditionnementReferenceID"]))
                    mClass.ConditionnementReference = new ConditionnementReference
                    {
                        ID = (int)rdr["ConditionnementReferenceID"],
                        Reference = !DBNull.Value.Equals(rdr["ConditionnementReferenceReference"]) ? (string)rdr["ConditionnementReferenceReference"] : null
                    };

                // Conditionnement
                if (!DBNull.Value.Equals(rdr["ConditionnementID"]))
                    mClass.Conditionnement = new Conditionnement
                    {
                        ID = (int)rdr["ConditionnementID"],
                        Designation = !DBNull.Value.Equals(rdr["ConditionnementDesignation"]) ? (string)rdr["ConditionnementDesignation"] : null
                    };

                // Audit
                if (!DBNull.Value.Equals(rdr["CreationDate"])) mClass.DateCreation = (DateTime)rdr["CreationDate"];
                if (!DBNull.Value.Equals(rdr["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)rdr["CreationUtilisateur"];
                if (!DBNull.Value.Equals(rdr["ModificationDate"])) mClass.DateModification = (DateTime)rdr["ModificationDate"];
                if (!DBNull.Value.Equals(rdr["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)rdr["ModificationUtilisateur"];

                // Divers
                if (!DBNull.Value.Equals(rdr["RowVersionKey"])) mClass.RowVersionKey = (byte[])rdr["RowVersionKey"];
                if (!DBNull.Value.Equals(rdr["Statut"])) mClass.Statut = (string)rdr["Statut"];
                if (!DBNull.Value.Equals(rdr["Desactive"])) mClass.Desactive = (bool)rdr["Desactive"];
                if (!DBNull.Value.Equals(rdr["IsApproved"])) mClass.IsApproved = (bool)rdr["IsApproved"];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nArticle:MapFromDataReader5");
            }
        }

        private static void MapFromDataReade2(Palette mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    //if (!DBNull.Value.Equals(mDataReader["Designation"])) mClass._Designation = (string)mDataReader["Designation"];

                    //mClass._DefautPalette = new Palette();
                    //if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._DefautPalette.ID = (int)mDataReader["ID"];
                    //if (!DBNull.Value.Equals(mDataReader["Nom"])) mClass._DefautPalette.Nom = (string)mDataReader["Nom"];

                    //mClass._ProduitFini = new ProduitFini();
                    //if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ProduitFini.ID = (int)mDataReader["ID"];
                    //if (!DBNull.Value.Equals(mDataReader["Designation"])) mClass._ProduitFini.Designation = (string)mDataReader["Designation"];

                    //mClass._Provenance = new Provenance();
                    //if (!DBNull.Value.Equals(mDataReader["ProvenanceID"])) mClass._Provenance.ID = (int)mDataReader["ProvenanceID"];
                    //if (!DBNull.Value.Equals(mDataReader["ProvenanceNom"])) mClass._Provenance.Nom = (string)mDataReader["ProvenanceNom"];

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];

                    //if (!DBNull.Value.Equals(mDataReader["MagasinID"])) mClass._MagasinID = (int)mDataReader["MagasinID"];
                    //if (!DBNull.Value.Equals(mDataReader["MagasinNom"])) mClass._MagasinNom = (string)mDataReader["MagasinNom"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nPalette:MapFromDataReader");
            }
        }
        #endregion


        
    }

    public partial class PaletteViewModel
    {
        public Palette _Palette { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }

    public class MouvementStockProduitDto
    {
        public Guid? MpId { get; set; }

        public int MpCodeMagasin { get; set; }
        public DateTime MpDate { get; set; }
        public Guid MpCodePalette { get; set; }
        public int? MpCodeTypeMouvement { get; set; } // non-nullable en SQL -> int
        public int MpSens { get; set; }
        public int MpCodeConditionnement { get; set; }
        public int MpCodeReferenceConditionnement { get; set; }
        public int MpNbreUniteParPalette { get; set; }
        public string MpUniteDePoids { get; set; } = string.Empty;

        public decimal MpPoidsBrutUnitaire { get; set; }
        public decimal MpTareUnitaireEmballage { get; set; }
        public decimal MpPoidsBrutPalette { get; set; }
        public decimal MpTareEmballagePalette { get; set; }
        public decimal MpPoidsNetPalette { get; set; }

    }

}
