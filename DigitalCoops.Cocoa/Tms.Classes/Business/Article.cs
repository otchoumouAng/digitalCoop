using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Business.Sales;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;
using Tms.Classes.Shared.Sales;

namespace Tms.Classes.Business
{
    public class Article : DataPersist
    {
        #region fields
        private Guid _ID;



        private ProduitFini _ProduitFini;
        private ProduitType _ProduitType;
        private MarqueProduit _MarqueProduit;
        private LigneProduction _LigneProduction;
        private ProduitGamme _ProduitGamme;

        private ConditionnementReference _ConditionnementReference;
        private Conditionnement _Conditionnement;


        private UniteDePoids _UniteDePoids;
        private string _Code;
        private string _CodeEtendu;
        private string _Nom;
        private string _Description;

        private decimal _PoidsBrutTotal;
        private decimal _PoidsNetTotalPalette;
        private decimal _TareTotaleEmballage;

        private decimal _TareUnitaireEmballage;
        private decimal _TarePaletteVide;
        private int _TareEmballagePalette;
        private int _NbreUniteParPalette;

        private decimal _PoidsBrutPalette;
        private int _PoidsNetPalette;
        private decimal _PoidsBrutUnitaire;

        private int _BestBeforeDate;
        private DateTime? _BBDate;


        private int _NbreTiquetteParDefaut;
        private int _NbreTiquetteParDefautA4;
        private int _NbreTiquetteParDefautA5;
        private string _CodeGTIN;
        private string _Statut;
        private bool _Desactive;
        private bool _IsApproved;
        #endregion

        #region Properties
        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public ProduitFini ProduitFini
        {
            get { return _ProduitFini; }
            set { _ProduitFini = value; }
        }

        public ProduitType ProduitType
        {
            get { return _ProduitType; }
            set { _ProduitType = value; }
        }

        public MarqueProduit MarqueProduit
        {
            get { return _MarqueProduit; }
            set { _MarqueProduit = value; }
        }


        public int NbreTiquetteParDefautA4
        {
            get { return _NbreTiquetteParDefautA4; }
            set { _NbreTiquetteParDefautA4 = value; }
        }

        public int NbreTiquetteParDefautA5
        {
            get { return _NbreTiquetteParDefautA5; }
            set { _NbreTiquetteParDefautA5 = value; }
        }

        public string ProduitTypeAsString
        {
            get
            {
                return _ProduitType != null ? _ProduitType.Designation : string.Empty;
            }
        }

        public ConditionnementReference ConditionnementReference
        {
            get { return _ConditionnementReference; }
            set { _ConditionnementReference = value; }
        }

        public Conditionnement Conditionnement
        {
            get { return _Conditionnement; }
            set { _Conditionnement = value; }
        }

        public string ConditionnementAsString
        {
            get
            {
                return _Conditionnement != null ? _Conditionnement.Designation : string.Empty;
            }
        }


        public decimal PoidsBrutTotal
        {
            get { return _PoidsBrutTotal; }
            set { _PoidsBrutTotal = value; }
        }

        public string PoidsBrutTotalAsString
        {
            get { return _PoidsBrutTotal != 0 ? String.Format("{0:#,#}", _PoidsBrutTotal).TrimStart() : string.Empty; }
        }

        public decimal PoidsNetTotalPalette
        {
            get { return _PoidsNetTotalPalette; }
            set { _PoidsNetTotalPalette = value; }
        }

        public decimal TareTotaleEmballage
        {
            get { return _TareTotaleEmballage; }
            set { _TareTotaleEmballage = value; }
        }


        public string PoidsBruiteTotalAsString
        {
            get { return _PoidsBrutTotal.ToString(); }
        }

        public string PoidsNetTotalPaletteAsString
        {
            get { return _PoidsNetTotalPalette.ToString(); }
        }

        public string TareTotaleEmballageAsString
        {
            get { return _TareTotaleEmballage.ToString(); }
        }

        public string MarqueProduitAsString
        {
            get { return _MarqueProduit != null ? _MarqueProduit.Designation : string.Empty; }
        }

        public UniteDePoids UniteDePoids
        {
            get { return _UniteDePoids; }
            set { _UniteDePoids = value; }
        }

        public LigneProduction LigneProduction
        {
            get { return _LigneProduction; }
            set { _LigneProduction = value; }
        }

        public decimal TarePaletteVide
        {
            get { return _TarePaletteVide; }
            set { _TarePaletteVide = value; }
        }

        public string TarePaletteVideAsString
        {
            get { return _TarePaletteVide.ToString(); }
        }



        public string TareEmballagePaletteAsString
        {
            get { return _TareEmballagePalette.ToString(); }
        }


        public string LigneProductionAsString
        {
            get { return _LigneProduction != null ? _LigneProduction.Designation : string.Empty; }
        }

        public ProduitGamme ProduitGamme
        {
            get { return _ProduitGamme; }
            set { _ProduitGamme = value; }
        }

        public string ProduitGammeAsString
        {
            get { return _ProduitGamme != null ? _ProduitGamme.Designation : string.Empty; }
        }

        public string UniteDePoidsAsString
        {
            get { return _UniteDePoids != null ? _UniteDePoids.Designation : string.Empty; }
        }

        public string ConditionnementDesignationAsString
        {
            get { return _Conditionnement != null ? _Conditionnement.Designation : string.Empty; }
        }

        public string NbreUniteParPaletteAsString
        {
            get { return _NbreUniteParPalette.ToString(); }
        }


        public DateTime? BBDate
        {
            get { return _BBDate; }
            set { _BBDate = value; }
        }

        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public string CodeEtendu
        {
            get { return _CodeEtendu; }
            set { _CodeEtendu = value; }
        }

        public string Nom
        {
            get { return _Nom; }
            set { _Nom = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }



        public int NbreUniteParPalette
        {
            get { return _NbreUniteParPalette; }
            set { _NbreUniteParPalette = value; }
        }


        public decimal PoidsBrutUnitaire
        {
            get { return _PoidsBrutUnitaire; }
            set { _PoidsBrutUnitaire = value; }
        }

        public decimal TareUnitaireEmballage
        {
            get { return _TareUnitaireEmballage; }
            set { _TareUnitaireEmballage = value; }
        }

        public decimal PoidsBrutPalette
        {
            get { return _PoidsBrutPalette; }
            set { _PoidsBrutPalette = value; }
        }

        public int TareEmballagePalette
        {
            get { return _TareEmballagePalette; }
            set { _TareEmballagePalette = value; }
        }

        public int PoidsNetPalette
        {
            get { return _PoidsNetPalette; }
            set { _PoidsNetPalette = value; }
        }



        public int BestBeforeDate
        {
            get { return _BestBeforeDate; }
            set { _BestBeforeDate = value; }
        }



        public int NbreTiquetteParDefaut
        {
            get { return _NbreTiquetteParDefaut; }
            set { _NbreTiquetteParDefaut = value; }
        }


        public string CodeGTIN
        {
            get { return _CodeGTIN; }
            set { _CodeGTIN = value; }
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

        [Column(Text = "IsApproved")]
        public bool IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }

        public int mIcon
        {
            get
            {
                if (_Desactive || _Statut == "CA")
                    return 0; // BulletCross
                else if (_IsApproved || _Statut == "AP")
                    return 1; // Tick                
                else
                    return 2; //                     
            }
        }

        #endregion

        #region Constructor
        public Article()
        {

        }

        public Article(Guid myId)
        {
            this.fnGet(myId);
        }
        #endregion

        #region Methods
        public override bool fnActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("PP_Article_Activate");
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
                        _Desactive = false;
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
                throw new Exception(ex.Message + "\r\n" + "Article:fnDeActivate");
            }
            return bolResult;
        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("PP_Article_DeActivate");
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
                        _Desactive = true;
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
                throw new Exception(ex.Message + "\r\n" + "Article:fnDeActivate");
            }
            return bolResult;
        }

        public bool fnApprove(DataTransaction mTran)
        {
            if (!this._IsApproved)
            {
                bool Result;
                DataCommand mCommande = db().CreateStoredProcCommand("PP_Article_Approve");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                try
                {
                    db().ExecuteNonQuery(ref mCommande, mTran);
                    switch ((int)db().Parameters(mCommande, "ReturnValue"))
                    {
                        case 0:
                            //Everything OK
                            Result = true;
                            IsApproved = true;
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
                    throw new Exception(ex.Message + "\r\n" + "Article:fnApprove");
                }
                return Result;
            }
            return false;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("PP_Article_Get", (Guid)Id);
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


        public override List<DataPersist> fnSelect()
        {
            return fnSelect(-1, -1, -1, "-1", -1);
        }

        public List<DataPersist> fnSelect(int mProduitID, int mProduitTypeID, int mMarqueProduitID, string mStatut, int mActif)

        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("PP_Article_Select");
                db().AddInParameter(mCommande, "@ProduitFiniID", SqlDbType.Int, mProduitID);
                db().AddInParameter(mCommande, "@TypeProduitID", SqlDbType.Int, mProduitTypeID);
                db().AddInParameter(mCommande, "@MarqueProduitID", SqlDbType.Int, mMarqueProduitID);
                db().AddInParameter(mCommande, "@Actif", SqlDbType.Int, mActif);
                db().AddInParameter(mCommande, "@statut", SqlDbType.Char, 2, mStatut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Article mClass = new Article();
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


        public List<DataPersist> fnSelectByBBDate(Guid mArticleID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("PP_Article_byBBDate");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, mArticleID);
                mDataReader = db().ExecuteReader(mCommande);
                if (mDataReader.Read()) // ✅ Vérifie s’il y a des données
                {
                    Article mClass = new Article();
                    MapFromDataReaderByBBDate(mClass, mDataReader);
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
                    mCommande = db().CreateStoredProcCommand("PP_Article_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUtilisateur", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("PP_Article_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUtilisateur", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@ProduitID", SqlDbType.Int, _ProduitFini.ID);
                db().AddInParameter(mCommande, "@TypeProduitID", SqlDbType.Int, _ProduitType.ID);
                db().AddInParameter(mCommande, "@MarqueProduitID", SqlDbType.Int, _MarqueProduit.ID);
                db().AddInParameter(mCommande, "@ProduitGammeID", SqlDbType.Int, _ProduitGamme.ID);
                db().AddInParameter(mCommande, "@ConditionnementID", SqlDbType.Int, _Conditionnement.ID);
                db().AddInParameter(mCommande, "@ConditionnementReferenceID", SqlDbType.Int, _ConditionnementReference.ID);
                db().AddInParameter(mCommande, "@LigneProductionID", SqlDbType.Int, _LigneProduction.ID);
                db().AddInParameter(mCommande, "@UniteDePoidsID", SqlDbType.VarChar, _UniteDePoids.ID);

                db().AddInParameter(mCommande, "@TarePaletteVide", SqlDbType.Decimal, _TarePaletteVide);
                db().AddInParameter(mCommande, "@PoidsBrutTotal", SqlDbType.Decimal, _PoidsBrutTotal);
                db().AddInParameter(mCommande, "@PoidsNetTotalPalette", SqlDbType.Decimal, _PoidsNetTotalPalette);
                db().AddInParameter(mCommande, "@TareTotaleEmballage", SqlDbType.Decimal, _TareTotaleEmballage);

                db().AddInParameter(mCommande, "@Code", SqlDbType.VarChar, _Code);
                db().AddInParameter(mCommande, "@CodeEtendu", SqlDbType.VarChar, _CodeEtendu);
                db().AddInParameter(mCommande, "@Nom", SqlDbType.VarChar, _Nom);
                db().AddInParameter(mCommande, "@Description", SqlDbType.VarChar, _Description);
                db().AddInParameter(mCommande, "@NbreUniteParPalette", SqlDbType.Int, _NbreUniteParPalette);
                db().AddInParameter(mCommande, "@PoidsBrutUnitaire", SqlDbType.Decimal, _PoidsBrutUnitaire);
                db().AddInParameter(mCommande, "@TareUnitaireEmballage", SqlDbType.Decimal, _TareUnitaireEmballage);
                db().AddInParameter(mCommande, "@PoidsBrutPalette", SqlDbType.Decimal, _PoidsBrutPalette);
                db().AddInParameter(mCommande, "@TareEmballagePalette", SqlDbType.Decimal, _TareEmballagePalette);
                db().AddInParameter(mCommande, "@PoidsNetPalette", SqlDbType.Decimal, _PoidsNetPalette);
                db().AddInParameter(mCommande, "@BestBeforeDate", SqlDbType.Int, _BestBeforeDate);
                db().AddInParameter(mCommande, "@NbreTiquetteParDefaut", SqlDbType.Int, _NbreTiquetteParDefaut);



                db().AddInParameter(mCommande, "@CodeGTIN", SqlDbType.VarChar, _CodeGTIN);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.VarChar, _Statut);
                db().AddInParameter(mCommande, "@Desactive", SqlDbType.Bit, _Desactive);
                db().AddInParameter(mCommande, "@IsApproved", SqlDbType.Bit, _IsApproved);


                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                if (!this._isnew)
                {
                    db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
                }
                else
                {
                    db().AddOutParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0);
                }

                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                db().ExecuteNonQuery(ref mCommande);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        base.UpdateAuditFields();
                        Result = true;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        _ID = (Guid)db().Parameters(mCommande, "@ID");
                        break;
                    default:
                        //Unkown error
                        Result = false;
                        string ErrorMessage = (string)db().Parameters(mCommande, "@ErrorMessage");
                        throw new Exception(ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + "\r\n" + "Article:fnUpdate");

            }
            return Result;
        }


        public override string ToString()
        {
            return _Code;
        }

        private static void MapFromDataReaderByBBDate(Article mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["BBDate"])) mClass._BBDate = (DateTime)mDataReader["BBDate"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nArticle:MapFromDataReaderByBBDate");
            }
        }


        private static void MapFromDataReader(Article mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._MarqueProduit = new MarqueProduit();
                    if (!DBNull.Value.Equals(mDataReader["MarqueProduitID"])) mClass._MarqueProduit.ID = (int)mDataReader["MarqueProduitID"];
                    if (!DBNull.Value.Equals(mDataReader["MarqueProduitDesignation"])) mClass._MarqueProduit.Designation = (string)mDataReader["MarqueProduitDesignation"];

                    mClass._Conditionnement = new Conditionnement();
                    if (!DBNull.Value.Equals(mDataReader["ConditionnementID"])) mClass._Conditionnement.ID = (int)mDataReader["ConditionnementID"];
                    if (!DBNull.Value.Equals(mDataReader["ConditionnementDesignation"])) mClass._Conditionnement.Designation = (string)mDataReader["ConditionnementDesignation"];

                    mClass._ConditionnementReference = new ConditionnementReference();
                    if (!DBNull.Value.Equals(mDataReader["ConditionnementReferenceID"])) mClass._ConditionnementReference.ID = (int)mDataReader["ConditionnementReferenceID"];
                    if (!DBNull.Value.Equals(mDataReader["ConditionnementReferenceReference"])) mClass._ConditionnementReference.Reference = (string)mDataReader["ConditionnementReferenceReference"];

                    mClass._ProduitType = new ProduitType();
                    if (!DBNull.Value.Equals(mDataReader["TypeProduitID"])) mClass._ProduitType.ID = (int)mDataReader["TypeProduitID"];
                    if (!DBNull.Value.Equals(mDataReader["TypeProduitDesignation"])) mClass._ProduitType.Designation = (string)mDataReader["TypeProduitDesignation"];

                    mClass._UniteDePoids = new UniteDePoids();
                    if (!DBNull.Value.Equals(mDataReader["UniteDePoidsID"])) mClass._UniteDePoids.ID = (int)mDataReader["UniteDePoidsID"];
                    if (!DBNull.Value.Equals(mDataReader["UniteDePoidsDesignation"])) mClass._UniteDePoids.Designation = (string)mDataReader["UniteDePoidsDesignation"];

                    mClass._ProduitFini = new ProduitFini();
                    if (!DBNull.Value.Equals(mDataReader["ProduitFiniID"])) mClass._ProduitFini.ID = (int)mDataReader["ProduitFiniID"];
                    if (!DBNull.Value.Equals(mDataReader["ProduitFiniDesignation"])) mClass._ProduitFini.Designation = (string)mDataReader["ProduitFiniDesignation"];

                    mClass._ProduitGamme = new ProduitGamme();
                    if (!DBNull.Value.Equals(mDataReader["ProduitGammeID"])) mClass._ProduitGamme.ID = (int)mDataReader["ProduitGammeID"];
                    if (!DBNull.Value.Equals(mDataReader["ProduitGammeDesignation"])) mClass._ProduitGamme.Designation = (string)mDataReader["ProduitGammeDesignation"];

                    mClass._LigneProduction = new LigneProduction();
                    if (!DBNull.Value.Equals(mDataReader["LigneProductionID"])) mClass._LigneProduction.ID = (int)mDataReader["LigneProductionID"];
                    if (!DBNull.Value.Equals(mDataReader["LigneProductionDesignation"])) mClass._LigneProduction.Designation = (string)mDataReader["LigneProductionDesignation"];

                    if (!DBNull.Value.Equals(mDataReader["Code"])) mClass._Code = (string)mDataReader["Code"];
                    if (!DBNull.Value.Equals(mDataReader["CodeEtendu"])) mClass._CodeEtendu = (string)mDataReader["CodeEtendu"];
                    if (!DBNull.Value.Equals(mDataReader["Nom"])) mClass._Nom = (string)mDataReader["Nom"];
                    if (!DBNull.Value.Equals(mDataReader["Description"])) mClass._Description = (string)mDataReader["Description"];
                    if (!DBNull.Value.Equals(mDataReader["NbreUniteParPalette"])) mClass._NbreUniteParPalette = (int)mDataReader["NbreUniteParPalette"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrutUnitaire"])) mClass._PoidsBrutUnitaire = (decimal)mDataReader["PoidsBrutUnitaire"];
                    if (!DBNull.Value.Equals(mDataReader["TareUnitaireEmballage"])) mClass._TareUnitaireEmballage = (Decimal)mDataReader["TareUnitaireEmballage"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrutPalette"])) mClass._PoidsBrutPalette = (decimal)mDataReader["PoidsBrutPalette"];
                    if (!DBNull.Value.Equals(mDataReader["TareEmballagePalette"])) mClass._TareEmballagePalette = (int)mDataReader["TareEmballagePalette"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetPalette"])) mClass._PoidsNetPalette = (int)mDataReader["PoidsNetPalette"];
                    if (!DBNull.Value.Equals(mDataReader["BestBeforeDate"])) mClass._BestBeforeDate = (int)mDataReader["BestBeforeDate"];
                    if (!DBNull.Value.Equals(mDataReader["NbreTiquetteParDefaut"])) mClass._NbreTiquetteParDefaut = (int)mDataReader["NbreTiquetteParDefaut"];
                    if (!DBNull.Value.Equals(mDataReader["CodeGTIN"])) mClass._CodeGTIN = (string)mDataReader["CodeGTIN"];

                    if (!DBNull.Value.Equals(mDataReader["TarePaletteVide"])) mClass._TarePaletteVide = (decimal)mDataReader["TarePaletteVide"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrutTotal"])) mClass._PoidsBrutTotal = (decimal)mDataReader["PoidsBrutTotal"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetTotalPalette"])) mClass._PoidsNetTotalPalette = (decimal)mDataReader["PoidsNetTotalPalette"];
                    if (!DBNull.Value.Equals(mDataReader["TareTotaleEmballage"])) mClass._TareTotaleEmballage = (decimal)mDataReader["TareTotaleEmballage"];


                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["IsApproved"])) mClass._IsApproved = (bool)mDataReader["IsApproved"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];
                    //if (!DBNull.Value.Equals(mDataReader["EstFictif"])) mClass._EstFictif = (bool)mDataReader["EstFictif"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nArticle:MapFromDataReader");
            }
        }


        #endregion
    }

    public partial class ArticleViewModel
    {
        public Article _Article { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
