using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Classes.Shared.Sales;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms.Classes.Business.stock
{
    public class MouvementStockProduit : DataPersist
    {
        #region Fields
        private Guid _ID;
        private Magasin _Magasin;
        private MouvementStockType _MouvementStockProduitType;
        private DateTime _DateMouvement;
        private Int16 _Sens;
        private string _Statut;
        private bool _Desactive;

        // --- Objets principaux ---
        private Palette _Palette;
        private OrdreFabrication _OrdreDeFabrication;
        private Article _Article;

        // --- Objets de conditionnement ---
        private Conditionnement _CodeConditionnement;
        private ConditionnementReference _CodeReferenceConditionnement;
        #endregion

        #region Properties
        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Magasin Magasin
        {
            get { return _Magasin; }
            set { _Magasin = value; }
        }

        public MouvementStockType MouvementStockProduitType
        {
            get { return _MouvementStockProduitType; }
            set { _MouvementStockProduitType = value; }
        }

        public DateTime DateMouvement
        {
            get { return _DateMouvement; }
            set { _DateMouvement = value; }
        }

        public Int16 Sens
        {
            get { return _Sens; }
            set { _Sens = value; }
        }

        public string Statut
        {
            get { return _Statut; }
            set { _Statut = value; }
        }

        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
        }

        // --- Propriétés des Objets ---
        public Palette Palette
        {
            get { return _Palette; }
            set { _Palette = value; }
        }

        public OrdreFabrication OrdreDeFabrication
        {
            get { return _OrdreDeFabrication; }
            set { _OrdreDeFabrication = value; }
        }

        public Article Article
        {
            get { return _Article; }
            set { _Article = value; }
        }

        public Conditionnement CodeConditionnement
        {
            get { return _CodeConditionnement; }
            set { _CodeConditionnement = value; }
        }

        public ConditionnementReference CodeReferenceConditionnement
        {
            get { return _CodeReferenceConditionnement; }
            set { _CodeReferenceConditionnement = value; }
        }

        // --- Propriétés d'accès rapide (pour affichage) ---
        public string MagasinAsString
        {
            get { return _Magasin != null ? _Magasin.Designation : string.Empty; }
        }

        public string MouvementTypeAsString
        {
            get { return _MouvementStockProduitType != null ? _MouvementStockProduitType.Designation : string.Empty; }
        }

        public string DateMouvementAsString
        {
            get { return _DateMouvement.ToShortDateString(); }
        }

        public int PaletteNumero
        {
            get { return _Palette != null ? _Palette.Numero : 0; }
        }

        public string OrdreProductionNum
        {
            get { return _OrdreDeFabrication != null ? _OrdreDeFabrication.NumeroProduction : string.Empty; }
        }

        public string ArticleNom
        {
            get { return _Article != null ? _Article.Nom : string.Empty; }
        }

        // --- Propriétés calculées (utilisant l'objet Article) ---
        public decimal TotalEntree
        {
            get { return _Sens > 0 && _Article != null ? _Article.PoidsBrutPalette : 0; }
        }

        public decimal TotalSortie
        {
            get { return _Sens < 0 && _Article != null ? _Article.PoidsBrutPalette : 0; }
        }

        public decimal TotalUnitesEntree
        {
            get { return _Sens > 0 && _Article != null ? _Article.NbreUniteParPalette : 0; }
        }

        public decimal TotalUnitesSortie
        {
            get { return _Sens < 0 && _Article != null ? _Article.NbreUniteParPalette : 0; }
        }

        public decimal TotalPoidsNetEntree
        {
            get { return _Sens > 0 && _Article != null ? _Article.PoidsNetPalette : 0; }
        }

        public decimal TotalPoidsNetSortie
        {
            get { return _Sens < 0 && _Article != null ? _Article.PoidsNetPalette : 0; }
        }
        #endregion


        #region Constructor
        public MouvementStockProduit()
        {

        }

        public MouvementStockProduit(Guid myId)
        {
            this.fnGet(myId);
        }
        #endregion
        #region Methods
        public override bool fnActivate()
        {
            throw new NotImplementedException();
        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("ps_MouvementStockProduit_DeActivate");
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
                        bolResult = true;
                        Desactive = true;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        break;
                    default:
                        bolResult = false;
                        string ErrorMessage = (string)db().Parameters(mCommande, "@ErrorMessage");
                        throw new Exception(ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                bolResult = false;
                throw new Exception(ex.Message + "\r\n" + "MouvementStockProduit:fnDeActivate");
            }
            return bolResult;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("ps_MouvementStockProduit_Get", (Guid)Id);
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

        //public List<DataPersist> fnSelectStockQuantite_Dash(string mCampagne, int MagasinID, DateTime? StartDate, DateTime? EndDate)
        //{
        //    List<DataPersist> mList = new List<DataPersist>();
        //    IDataReader mDataReader = null;

        //    try
        //    {
        //        DataCommand mCommande = db().CreateStoredProcCommand("V2_StatutStockQuantite_SelectForStock");
        //        db().AddInParameter(mCommande, "@magasinID", SqlDbType.Int, MagasinID);
        //        db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, mCampagne);
        //        //db().AddInParameter(mCommande, "@exportateurID", SqlDbType.Int, ExportateurID);                            
        //        db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, StartDate);
        //        db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, EndDate);
        //        mDataReader = db().ExecuteReader(mCommande);

        //        while (mDataReader.Read())
        //        {
        //            MouvementStockProduit mClass = new MouvementStockProduit();
        //            MapFromDataReaderStockQte(mClass, mDataReader);
        //            mList.Add(mClass);
        //        }
        //        return mList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelect");
        //    }
        //    finally
        //    {
        //        if (mDataReader != null) mDataReader.Close();
        //    }
        //}

        //public bool fnGetLotForRecleaning(object LotId)
        //{
        //    IDataReader mDataReader = null;
        //    try
        //    {
        //        mDataReader = db().ExecuteReader("V2_MouvementStockProduit_GetLotForRecleaning", (Guid)LotId);
        //        if (mDataReader.Read())
        //        {
        //            MapIdFromDataReader(this, mDataReader);
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetLotForRecleaning");
        //    }
        //    finally
        //    {
        //        if (mDataReader != null) mDataReader.Close();
        //    }
        //}


        public override List<DataPersist> fnSelect()
        {
            return fnSelect(-1, null, null, -1, -2, null, null, null);
        }

        public List<DataPersist> fnSelect(int magasinID, Guid? productionID, Guid? articleID, int typeMouvementID, int sens, DateTime? dateDebut, DateTime? dateFin, string statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                // 1. Appel de la nouvelle procédure stockée
                DataCommand mCommande = db().CreateStoredProcCommand("ps_MouvementStockProduit_Select");

                // 2. Ajout des nouveaux paramètres
                db().AddInParameter(mCommande, "@magasinID", SqlDbType.Int, magasinID);
                db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, productionID);
                db().AddInParameter(mCommande, "@articleID", SqlDbType.UniqueIdentifier, articleID);
                db().AddInParameter(mCommande, "@TypeMouvementID", SqlDbType.Int, typeMouvementID);
                db().AddInParameter(mCommande, "@sens", SqlDbType.Int, sens);
                db().AddInParameter(mCommande, "@dateDebut", SqlDbType.DateTime, dateDebut);
                db().AddInParameter(mCommande, "@dateFin", SqlDbType.DateTime, dateFin);
                db().AddInParameter(mCommande, "@statut", SqlDbType.VarChar, 255, statut);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    MouvementStockProduit mClass = new MouvementStockProduit();
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
                // 1. Appel de la nouvelle procédure stockée pour l'insertion
                mCommande = db().CreateStoredProcCommand("ps_MouvementStockProduit_new");

                // 2. Ajout des paramètres en fonction de la nouvelle procédure et de la structure objet

                // --- Paramètres de base ---
                db().AddInParameter(mCommande, "@CodeMagasin", SqlDbType.Int, _Magasin.ID);
                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _DateMouvement);
                db().AddInParameter(mCommande, "@CodePalette", SqlDbType.UniqueIdentifier, _Palette.ID);
                db().AddInParameter(mCommande, "@ProcessID", SqlDbType.UniqueIdentifier, _OrdreDeFabrication != null ? (object)_OrdreDeFabrication.ID : DBNull.Value);
                db().AddInParameter(mCommande, "@CodeTypeMouvement", SqlDbType.Int, _MouvementStockProduitType.ID);
                db().AddInParameter(mCommande, "@Sens", SqlDbType.Int, _Sens);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.VarChar, _Statut);
                db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, this.UtilisateurCreation);

                // --- Paramètres de Conditionnement ---
                db().AddInParameter(mCommande, "@CodeConditionnement", SqlDbType.Int, _CodeConditionnement.ID);
                db().AddInParameter(mCommande, "@CodeReferenceConditionnement", SqlDbType.VarChar, _CodeReferenceConditionnement.Reference);

                // --- Paramètres de poids (provenant de l'objet Article) ---
                if (_Article != null)
                {
                    db().AddInParameter(mCommande, "@NbreUniteParPalette", SqlDbType.Int, _Article.NbreUniteParPalette);
                    db().AddInParameter(mCommande, "@UniteDePoids", SqlDbType.VarChar, _Article.UniteDePoids.Designation);
                    db().AddInParameter(mCommande, "@PoidsBrutUnitaire", SqlDbType.Float, _Article.PoidsBrutUnitaire);
                    db().AddInParameter(mCommande, "@TareUnitaireEmballage", SqlDbType.Float, _Article.TareUnitaireEmballage);
                    db().AddInParameter(mCommande, "@PoidsBrutPalette", SqlDbType.Float, _Article.PoidsBrutPalette);
                    db().AddInParameter(mCommande, "@TareEmballagePalette", SqlDbType.Float, _Article.TareEmballagePalette);
                    db().AddInParameter(mCommande, "@PoidsNetPalette", SqlDbType.Float, _Article.PoidsNetPalette);
                }

                // --- Paramètres de sortie (OUTPUT) ---
                db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddOutParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0);
                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                // Exécution de la commande
                db().ExecuteNonQuery(ref mCommande);

                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0: // Succès
                        base.UpdateAuditFields();
                        _ID = (Guid)db().Parameters(mCommande, "@ID");
                        this.RowVersionKey = (byte[])db().Parameters(mCommande, "@RowVersion");
                        Result = true;
                        break;
                    default: // Erreur gérée par la procédure
                        string ErrorMessage = (string)db().Parameters(mCommande, "@ErrorMessage");
                        throw new Exception(ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + "\r\n" + "MouvementStockProduit:fnUpdate");
            }
            return Result;
        }

        
        public bool fnUpdate(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                // 1. Appel de la nouvelle procédure stockée pour l'insertion
                mCommande = db().CreateStoredProcCommand("ps_MouvementStockProduit_new");

                // 2. Ajout des paramètres (identique à la méthode sans transaction)
                db().AddInParameter(mCommande, "@CodeMagasin", SqlDbType.Int, _Magasin.ID);
                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _DateMouvement);
                db().AddInParameter(mCommande, "@CodePalette", SqlDbType.UniqueIdentifier, _Palette.ID);
                db().AddInParameter(mCommande, "@ProcessID", SqlDbType.UniqueIdentifier, _OrdreDeFabrication != null ? (object)_OrdreDeFabrication.ID : DBNull.Value);
                db().AddInParameter(mCommande, "@CodeTypeMouvement", SqlDbType.Int, _MouvementStockProduitType.ID);
                db().AddInParameter(mCommande, "@Sens", SqlDbType.Int, _Sens);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.VarChar, _Statut);
                db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, this.UtilisateurCreation);

                db().AddInParameter(mCommande, "@CodeConditionnement", SqlDbType.Int, _CodeConditionnement.ID);
                db().AddInParameter(mCommande, "@CodeReferenceConditionnement", SqlDbType.VarChar, _CodeReferenceConditionnement.Reference);

                if (_Article != null)
                {
                    db().AddInParameter(mCommande, "@NbreUniteParPalette", SqlDbType.Int, _Article.NbreUniteParPalette);
                    db().AddInParameter(mCommande, "@UniteDePoids", SqlDbType.VarChar, _Article.UniteDePoids.Designation);
                    db().AddInParameter(mCommande, "@PoidsBrutUnitaire", SqlDbType.Float, _Article.PoidsBrutUnitaire);
                    db().AddInParameter(mCommande, "@TareUnitaireEmballage", SqlDbType.Float, _Article.TareUnitaireEmballage);
                    db().AddInParameter(mCommande, "@PoidsBrutPalette", SqlDbType.Float, _Article.PoidsBrutPalette);
                    db().AddInParameter(mCommande, "@TareEmballagePalette", SqlDbType.Float, _Article.TareEmballagePalette);
                    db().AddInParameter(mCommande, "@PoidsNetPalette", SqlDbType.Float, _Article.PoidsNetPalette);
                }

                db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddOutParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0);
                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                // Exécution de la commande avec la transaction
                db().ExecuteNonQuery(ref mCommande, mTran);

                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0: // Succès
                        base.UpdateAuditFields();
                        _ID = (Guid)db().Parameters(mCommande, "@ID");
                        this.RowVersionKey = (byte[])db().Parameters(mCommande, "@RowVersion");
                        Result = true;
                        break;
                    default: // Erreur
                        string ErrorMessage = (string)db().Parameters(mCommande, "@ErrorMessage");
                        throw new Exception(ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + "\r\n" + "MouvementStockProduit:fnUpdate(DataTransaction)");
            }
            return Result;
        }


        public override string ToString()
        {
            throw new NotImplementedException();
        }

        private static void MapFromDataReader(MouvementStockProduit mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    // --- Propriétés directes de MouvementStockProduit ---
                    mClass._ID = GetValueOrDefault<Guid>(mDataReader, "MouvementStockID");
                    mClass._DateMouvement = GetValueOrDefault<DateTime>(mDataReader, "DateMouvement");
                    //mClass._Sens = GetValueOrDefault<short>(mDataReader, "Sens");
                    mClass._Statut = GetValueOrDefault<string>(mDataReader, "Statut");
                    mClass._Desactive = GetValueOrDefault<bool>(mDataReader, "Desactive");

                    // --- Hydratation de l'objet Magasin ---
                    // On vérifie que l'ID du magasin existe avant de créer l'objet
                    if (mDataReader["CodeMagasin"] != DBNull.Value)
                    {
                        mClass._Magasin = new Magasin
                        {
                            ID = GetValueOrDefault<int>(mDataReader, "CodeMagasin"),
                            Designation = GetValueOrDefault<string>(mDataReader, "MagasinNom")
                        };
                    }

                    // --- Hydratation de l'objet MouvementStockType ---
                    if (mDataReader["CodeTypeMouvement"] != DBNull.Value)
                    {
                        mClass._MouvementStockProduitType = new MouvementStockType
                        {
                            ID = GetValueOrDefault<int>(mDataReader, "CodeTypeMouvement"),
                            Designation = GetValueOrDefault<string>(mDataReader, "TypeMouvementDesignation")
                        };
                    }

                    // --- Hydratation de l'objet Palette ---
                    if (mDataReader["CodePaletteID"] != DBNull.Value)
                    {
                        mClass._Palette = new Palette
                        {
                            ID = GetValueOrDefault<Guid>(mDataReader, "CodePaletteID"),
                            Numero = GetValueOrDefault<int>(mDataReader, "PaletteNumero"),
                            CodeSSCC = GetValueOrDefault<string>(mDataReader, "PaletteCodeSSCC")
                        };
                    }

                    // --- Hydratation de l'objet OrdreDeFabrication ---
                    if (mDataReader["OrdreDeProductionID"] != DBNull.Value)
                    {
                        mClass._OrdreDeFabrication = new OrdreFabrication
                        {
                            ID = GetValueOrDefault<Guid>(mDataReader, "OrdreDeProductionID"),
                            NumeroProduction = GetValueOrDefault<string>(mDataReader, "OrdreProductionNum")
                        };
                    }

                    // --- Hydratation de l'objet Article (incluant les poids) ---
                    if (mDataReader["ArticleCode"] != DBNull.Value)
                    {
                        mClass._Article = new Article();
                        mClass._Article.Code = GetValueOrDefault<string>(mDataReader, "ArticleCode");
                        mClass._Article.Nom = GetValueOrDefault<string>(mDataReader, "ArticleNom");
                        mClass._Article.NbreUniteParPalette = GetValueOrDefault<int>(mDataReader, "NbreUniteParPalette");
                        mClass._Article.PoidsBrutUnitaire = GetValueOrDefault<decimal>(mDataReader, "PoidsBrutUnitaire");
                        mClass._Article.TareUnitaireEmballage = GetValueOrDefault<decimal>(mDataReader, "TareUnitaireEmballage");
                        //mClass._Article.PoidsBrutPalette = GetValueOrDefault<decimal>(mDataReader, "PoidsBrutPalette");
                        //mClass._Article.TareEmballagePalette = GetValueOrDefault<int>(mDataReader, "TareEmballagePalette");
                        //mClass._Article.PoidsNetPalette = GetValueOrDefault<int>(mDataReader, "PoidsNetPalette");

                        string uniteDePoids = GetValueOrDefault<string>(mDataReader, "UniteDePoids");
                        if (uniteDePoids != null)
                        {
                            mClass._Article.UniteDePoids = new UniteDePoids { Designation = uniteDePoids };
                        }
                    }

                    // --- Hydratation des objets Conditionnement ---
                    string codeConditionnement = GetValueOrDefault<string>(mDataReader, "ConditionnementDesignation");
                    if (codeConditionnement != null)
                    {
                        mClass._CodeConditionnement = new Conditionnement { Designation = codeConditionnement };
                    }

                    string refConditionnement = GetValueOrDefault<string>(mDataReader, "ReferenceConditionnementDesignation");
                    if (refConditionnement != null)
                    {
                        mClass._CodeReferenceConditionnement = new ConditionnementReference { Reference = refConditionnement };
                    }


                    // --- Propriétés de la classe de base DataPersist ---
                    mClass.UtilisateurCreation = GetValueOrDefault<string>(mDataReader, "CreationUser");
                    mClass.DateCreation = GetValueOrDefault<DateTime>(mDataReader, "CreationDate");
                    mClass.UtilisateurModification = GetValueOrDefault<string>(mDataReader, "ModificationUser");
                    mClass.DateModification = GetValueOrDefault<DateTime>(mDataReader, "ModificationDate");
                    mClass.RowVersionKey = GetValueOrDefault<byte[]>(mDataReader, "RowVersion");
                }
            }
            catch (Exception ex)
            {
                // Ajout du nom de la méthode à l'exception pour un meilleur débogage
                throw new Exception(ex.Message + "\nMouvementStockProduit:MapFromDataReader");
            }
        }

        private static T GetValueOrDefault<T>(IDataReader reader, string columnName)
        {
            object value = reader[columnName];
            if (value == DBNull.Value)
            {
                return default(T); // Retourne null pour les types référence, 0 pour les nombres, etc.
            }
            return (T)value;
        }

        #endregion
    }

    public partial class MouvementStockProduitViewModel
    {
        public MouvementStockProduit _MouvementStockProduit { get; set; }
        public Parametres _Parametres { get; set; }

        public string _DefaultCampagne { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
