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
    public class Palette : DataPersist
    {
        #region fields
        private Guid _ID;
        private OrdreFabrication _OrdreFabrication;
        private String _Numero;
        private int _NbreUnite;
        private Conditionnement _Conditionnement;
        private ConditionnementReference _ConditionnementReference;
        private DemandeEtiquette _DemandeEtiquette;
        private ProduitType _ProduitType;
        private int _NbreUniteParPalette;
        private UniteDePoids _UniteDePoids;
        private QAStatus _QAStatus;
        private decimal _PoidsBrutUnitaire;
        private decimal _TareUnitaireEmballage;
        private decimal _PoidsBrutPalette;
        private int _TareEmballagePalette;
        private decimal _PoidsNetPalette;
        private int _NbreEtiquetteA4Demande;
        private int _NbreEtiquetteA4Imprime;
        private int _NbreEtiquetteA5Demande;
        private int _NbreEtiquetteA5Imprime;
        private DateTime _DateFabrication;
        private int _QAStatut;
        private string _CodeSSCC;
        private DateTime _CreationDate;
        private bool _Declaree = false;
        private DateTime _DateDeclaration;
        private int _NbreExemplaire;
        private string _TypeEtiquette;
        private string _StockMagasin;
        private string _StockEmplacement;
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

        public DemandeEtiquette DemandeEtiquette
        {
            get { return _DemandeEtiquette; }
            set { _DemandeEtiquette = value; }
        }

    

        public string DemandeEtiquetteBestBeforeDate
        {
            get { return DemandeEtiquette != null ? DemandeEtiquette.BestBeforeDate.ToString("dd MMMM yyyy", System.Globalization.CultureInfo.GetCultureInfo("fr-FR")) : ""; }
        }



        public ProduitType ProduitType
        {
            get { return _ProduitType; }
            set { _ProduitType = value; }
        }

        public string ProduitTypeDesignation
        {
            get { return ProduitType != null ? ProduitType.Designation.ToString() : ""; }
        }


        public OrdreFabrication OrdreFabrication
        {
            get { return _OrdreFabrication; }
            set { _OrdreFabrication = value; }
        }

        public string OrdreFabricationNumeroAsString
        {
            get { return OrdreFabrication != null ? OrdreFabrication.NumeroProduction : ""; }
        }


        public String Numero 
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        public int NbreUnite
        {
            get { return _NbreUnite; }
            set { _NbreUnite = value; }
        }

        public Conditionnement Conditionnement
        {
            get { return _Conditionnement; }
            set { _Conditionnement = value; }
        }

        public ConditionnementReference ConditionnementReference
        {
            get { return _ConditionnementReference; }
            set { _ConditionnementReference = value; }
        }

        public int NbreUniteParPalette
        {
            get { return _NbreUniteParPalette; }
            set { _NbreUniteParPalette = value; }
        }

        public UniteDePoids UniteDePoids
        {
            get { return _UniteDePoids; }
            set { _UniteDePoids = value; }
        }

        public QAStatus QAStatus
        {
            get { return _QAStatus; }
            set { _QAStatus = value; }
        }

        public string QAStatusCodeAsString
        {
            get { return QAStatus != null ? QAStatus.Code.ToString() : ""; }
        }

        public string UniteDePoidsDesignation
        {
            get { return UniteDePoids != null ? UniteDePoids.Designation.ToString() : ""; }
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

        public decimal PoidsNetPalette
        {
            get { return _PoidsNetPalette; }
            set { _PoidsNetPalette = value; }
        }

        public int NbreEtiquetteA4Demande
        {
            get { return _NbreEtiquetteA4Demande; }
            set { _NbreEtiquetteA4Demande = value; }
        }

        public int NbreEtiquetteA4Imprime
        {
            get { return _NbreEtiquetteA4Imprime; }
            set { _NbreEtiquetteA4Imprime = value; }
        }

        public int NbreEtiquetteA5Demande
        {
            get { return _NbreEtiquetteA5Demande; }
            set { _NbreEtiquetteA5Demande = value; }
        }

        public int NbreEtiquetteA5Imprime
        {
            get { return _NbreEtiquetteA5Imprime; }
            set { _NbreEtiquetteA5Imprime = value; }
        }

        public DateTime DateFabrication
        {
            get { return _DateFabrication; }
            set { _DateFabrication = value; }
        }

        public string DateFabricationAsString
        {
            get { return DateFabrication != null ? DateFabrication.ToString("dd MMMM yyyy", System.Globalization.CultureInfo.GetCultureInfo("fr-FR")) : ""; }
        }

        public int QAStatut
        {
            get { return _QAStatut; }
            set { _QAStatut = value; }
        }

        public string CodeSSCC
        {
            get { return _CodeSSCC; }
            set { _CodeSSCC = value; }
        }

        public DateTime CreationDate
        {
            get { return _CreationDate; }
            set { _CreationDate = value; }
        }

        public bool Declaree
        {
            get { return _Declaree; }
            set { _Declaree = value; }
        }

        public DateTime DateDeclaration
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

        public int NbreExemplaire
        {
            get { return _NbreExemplaire; }
            set { _NbreExemplaire = value; }
        }

        public string TypeEtiquette
        {
            get { return _TypeEtiquette; }
            set { _TypeEtiquette = value; }
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
        public Palette()
        {

        }

        public Palette(Guid myId)
        {
            this.fnGet(myId);
        }
        #endregion

        #region Methods
        public override bool fnActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("PP_Palette_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "Palette:fnDeActivate");
            }
            return bolResult;
        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("PP_Palette_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "Palette:fnDeActivate");
            }
            return bolResult;
        }

        public bool fnApprove(DataTransaction mTran)
        {
            if (!this._IsApproved)
            {
                bool Result;
                DataCommand mCommande = db().CreateStoredProcCommand("PP_Palette_Approve");
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
                    throw new Exception(ex.Message + "\r\n" + "Palette:fnApprove");
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
                mDataReader = db().ExecuteReader("PP_Palette_Get", (Guid)Id);
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
                DataCommand mCommande = db().CreateStoredProcCommand("PP_Palette_Select");
                db().AddInParameter(mCommande, "@ProduitID", SqlDbType.Int, mProduitID);
                db().AddInParameter(mCommande, "@TypeProduitID", SqlDbType.Int, mProduitTypeID);
                db().AddInParameter(mCommande, "@MarqueProduitID", SqlDbType.Int, mMarqueProduitID);
                db().AddInParameter(mCommande, "@Actif", SqlDbType.Int, mActif);
                db().AddInParameter(mCommande, "@statut", SqlDbType.Char, 2, mStatut);
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


        public List<DataPersist> ListPalettesForPrint(Guid mOrdreDeProductionID, string mTypeEtiquette, string RePrint, string OverviewPrint)

        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("pp_Palette_Select_ByOrdreDeProduction ");
                db().AddInParameter(mCommande, "@OrdreDeProductionID ", SqlDbType.UniqueIdentifier, mOrdreDeProductionID);
                db().AddInParameter(mCommande, "@TypeEtiquette", SqlDbType.VarChar, mTypeEtiquette);
                db().AddInParameter(mCommande, "@RePrint", SqlDbType.Bit, (RePrint == "true") ? true : false);
                db().AddInParameter(mCommande, "@OverviewPrint", SqlDbType.Bit, (OverviewPrint == "true") ? true : false);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Palette mClass = new Palette();
                    MapFromDataReaderByOrdreDeFabrication(mClass, mDataReader);

                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnListPalettesForPrint");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }




         public List<DataPersist> fnSelectByID(Guid mID, string mTypeEtiquette)

        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("pp_Palette_Select_ByID ");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, mID); 
                db().AddInParameter(mCommande, "@TypeEtiquette", SqlDbType.VarChar, mTypeEtiquette);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Palette mClass = new Palette();
                    MapFromDataReaderByOrdreDeFabrication(mClass, mDataReader);

                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectByID");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }


        



        public  bool fnRemove(Guid Id)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("PP_Palette_Delete");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, Id);
                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                db().ExecuteNonQuery(ref mCommande);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        Result = true;
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
                throw new Exception(ex.Message + "\r\n" + "Palette:fnRemove");
            }
            return Result;
        }




public bool fnGenerate(Guid mOrdreDeProductionID)
{
    bool Result;
    DataCommand mCommande;
    try
    {
        mCommande = db().CreateStoredProcCommand("PP_Palette_Generate");
        db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
        db().AddInParameter(mCommande, "@OrdreDeProductionID", SqlDbType.UniqueIdentifier, mOrdreDeProductionID);
        db().AddInParameter(mCommande, "@CreationUtilisateur", SqlDbType.VarChar, _UtilisateurCreation);
        db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
        db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);

        db().ExecuteNonQuery(ref mCommande);
        switch ((int)db().Parameters(mCommande, "ReturnValue"))
        {
            case 0:
                Result = true;
                _ID = (Guid)db().Parameters(mCommande, "@ID");
                break;
            default:
                Result = false;
                string ErrorMessage = (string)db().Parameters(mCommande, "@ErrorMessage");
                throw new Exception(ErrorMessage);
        }
    }
    catch (Exception ex)
    {
        Result = false;
        throw new Exception(ex.Message + "\r\n" + "Palette:fnGenerate");
    }
    return Result;
}


public List<DataPersist> fnSelect_ByOrdreDeProductionAndQAStatus(Guid mOrdreDeProductionID)
{
    List<DataPersist> mList = new List<DataPersist>();
    IDataReader mDataReader = null;

    try
    {
        DataCommand mCommande = db().CreateStoredProcCommand("pp_Palette_Select_ByOrdreDeProductionAndQAStatus ");
        db().AddInParameter(mCommande, "@OrdreDeProductionID", SqlDbType.UniqueIdentifier, mOrdreDeProductionID); 
        mDataReader = db().ExecuteReader(mCommande);

        while (mDataReader.Read())
        {
            Palette mClass = new Palette();
            MapFromDataReaderOrdreDeProductionAndQAStatus(mClass, mDataReader);

            mList.Add(mClass);
        }
        return mList;
    }
    catch (Exception ex)
    {
        throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelect_ByOrdreDeProductionAndQAStatus");
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
                    mCommande = db().CreateStoredProcCommand("PP_Palette_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUtilisateur", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("PP_Palette_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUtilisateur", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@OrdreFabricationID", SqlDbType.UniqueIdentifier, _OrdreFabrication.ID);
                db().AddInParameter(mCommande, "@Numero", SqlDbType.Int, _Numero);
                db().AddInParameter(mCommande, "@NbreUnite", SqlDbType.Int, _NbreUnite);
                db().AddInParameter(mCommande, "@ConditionnementID", SqlDbType.Int, _Conditionnement.ID);
                db().AddInParameter(mCommande, "@ConditionnementReferenceID", SqlDbType.Int, _ConditionnementReference.ID);
                db().AddInParameter(mCommande, "@NbreUniteParPalette", SqlDbType.Int, _NbreUniteParPalette);
                db().AddInParameter(mCommande, "@UniteDePoidsID", SqlDbType.Int, _UniteDePoids.ID);
                
                db().AddInParameter(mCommande, "@PoidsBrutUnitaire", SqlDbType.Decimal, _PoidsBrutUnitaire);
                db().AddInParameter(mCommande, "@TareUnitaireEmballage", SqlDbType.Decimal, _TareUnitaireEmballage);
                db().AddInParameter(mCommande, "@PoidsBrutPalette", SqlDbType.Decimal, _PoidsBrutPalette);
                db().AddInParameter(mCommande, "@TareEmballagePalette", SqlDbType.Int, _TareEmballagePalette);
                db().AddInParameter(mCommande, "@PoidsNetPalette", SqlDbType.Decimal, _PoidsNetPalette);
                
                db().AddInParameter(mCommande, "@NbreEtiquetteA4Demande", SqlDbType.Int, _NbreEtiquetteA4Demande);
                db().AddInParameter(mCommande, "@NbreEtiquetteA4Imprime", SqlDbType.Int, _NbreEtiquetteA4Imprime);
                db().AddInParameter(mCommande, "@NbreEtiquetteA5Demande", SqlDbType.Int, _NbreEtiquetteA5Demande);
                db().AddInParameter(mCommande, "@NbreEtiquetteA5Imprime", SqlDbType.Int, _NbreEtiquetteA5Imprime);
                
                db().AddInParameter(mCommande, "@DateFabrication", SqlDbType.DateTime, _DateFabrication);
                db().AddInParameter(mCommande, "@QAStatut", SqlDbType.Int, _QAStatut);
                db().AddInParameter(mCommande, "@CodeSSCC", SqlDbType.VarChar, _CodeSSCC);
                db().AddInParameter(mCommande, "@CreationDate", SqlDbType.DateTime, _CreationDate);
                db().AddInParameter(mCommande, "@Declaree", SqlDbType.Bit, _Declaree);
                db().AddInParameter(mCommande, "@DateDeclaration", SqlDbType.DateTime, _DateDeclaration);
                db().AddInParameter(mCommande, "@StockMagasin", SqlDbType.VarChar, _StockMagasin);
                db().AddInParameter(mCommande, "@StockEmplacement", SqlDbType.VarChar, _StockEmplacement);
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
                throw new Exception(ex.Message + "\r\n" + "Palette:fnUpdate");

            }
            return Result;
        }


        public void fnUpdateToPrint(Guid ID, string TypeEtiquette)
        {
            DataCommand mCommande;

            try
            {

                
                if (ID != Guid.Empty)
                {
                    mCommande = db().CreateStoredProcCommand("PP_Palette_UpdateToPrint");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                    db().AddInParameter(mCommande, "@TypeEtiquette", SqlDbType.VarChar, TypeEtiquette);
                    db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                    db().ExecuteNonQuery(ref mCommande);
                    switch ((int)db().Parameters(mCommande, "ReturnValue"))
                    {
                        case 0:
                            //Everything OK
                            base.UpdateAuditFields();
                            break;
                        default:
                            //Unkown error
                            string ErrorMessage = (string)db().Parameters(mCommande, "@ErrorMessage");
                            throw new Exception(ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\r\n" + "Palette:fnUpdateToPrint");
            }
        }




        public override string ToString()
        {
            return _Numero.ToString();
        }


        private static void MapFromDataReaderOrdreDeProductionAndQAStatus(Palette mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    
                    mClass._OrdreFabrication = new OrdreFabrication();
                    if (!DBNull.Value.Equals(mDataReader["NumeroProduction"])) mClass._OrdreFabrication.NumeroProduction = (string)mDataReader["NumeroProduction"];


                    if (!DBNull.Value.Equals(mDataReader["NumeroPalette"])) mClass._Numero = (String)mDataReader["NumeroPalette"];

                    mClass._QAStatus = new QAStatus();
                    if (!DBNull.Value.Equals(mDataReader["QAStatus"])) mClass._QAStatus.Code = (string)mDataReader["QAStatus"];
                   

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + mClass.GetType().FullName + ":MapFromDataReaderOrdreDeProductionAndQAStatus");
            }
        }


        private static void MapFromDataReaderByOrdreDeFabrication(Palette mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._ProduitType = new ProduitType();
                    if (!DBNull.Value.Equals(mDataReader["ProduitTypeDesignation"])) mClass._ProduitType.Designation = (string)mDataReader["ProduitTypeDesignation"];

                    mClass._OrdreFabrication = new OrdreFabrication();
                    if (!DBNull.Value.Equals(mDataReader["NumeroFabrication"])) mClass._OrdreFabrication.NumeroProduction = (string)mDataReader["NumeroFabrication"];

                    if (!DBNull.Value.Equals(mDataReader["NumeroPalette"])) mClass._Numero = (String)mDataReader["NumeroPalette"];
                    if (!DBNull.Value.Equals(mDataReader["NbreUnite"])) mClass._NbreUnite = (int)mDataReader["NbreUnite"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrutUnitaire"])) mClass._PoidsBrutUnitaire = (decimal)mDataReader["PoidsBrutUnitaire"];

                    mClass._UniteDePoids = new UniteDePoids();
                    if (!DBNull.Value.Equals(mDataReader["UniteDePoidsDesignation"])) mClass._UniteDePoids.Designation = (string)mDataReader["UniteDePoidsDesignation"];

                    if (!DBNull.Value.Equals(mDataReader["DateFabrication"])) mClass._DateFabrication = (DateTime)mDataReader["DateFabrication"];

                    mClass._DemandeEtiquette = new DemandeEtiquette();
                    if (!DBNull.Value.Equals(mDataReader["DemandeEtiqueteBestBeforeDate"])) mClass._DemandeEtiquette.BestBeforeDate = (DateTime)mDataReader["DemandeEtiqueteBestBeforeDate"];

                    if (!DBNull.Value.Equals(mDataReader["TypeEtiquette"])) mClass._TypeEtiquette = (string)mDataReader["TypeEtiquette"];
                    if (!DBNull.Value.Equals(mDataReader["NbreExemplaire"])) mClass.NbreExemplaire = (int)mDataReader["NbreExemplaire"];
                }
             }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nPalette:MapFromDataReaderByOrdreDeFabrication");
            }
        }


        private static void MapFromDataReader(Palette mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._Conditionnement = new Conditionnement();
                    if (!DBNull.Value.Equals(mDataReader["ConditionnementID"])) mClass._Conditionnement.ID = (int)mDataReader["ConditionnementID"];
                    if (!DBNull.Value.Equals(mDataReader["ConditionnementDesignation"])) mClass._Conditionnement.Designation = (string)mDataReader["ConditionnementDesignation"];

                    mClass._ConditionnementReference = new ConditionnementReference();
                    if (!DBNull.Value.Equals(mDataReader["ConditionnementReferenceID"])) mClass._ConditionnementReference.ID = (int)mDataReader["ConditionnementReferenceID"];
                    if (!DBNull.Value.Equals(mDataReader["ConditionnementReferenceReference"])) mClass._ConditionnementReference.Reference = (string)mDataReader["ConditionnementReferenceReference"];

                    mClass._UniteDePoids = new UniteDePoids();
                    if (!DBNull.Value.Equals(mDataReader["UniteDePoidsID"])) mClass._UniteDePoids.ID = (int)mDataReader["UniteDePoidsID"];
                    if (!DBNull.Value.Equals(mDataReader["UniteDePoidsDesignation"])) mClass._UniteDePoids.Designation = (string)mDataReader["UniteDePoidsDesignation"];

                    if (!DBNull.Value.Equals(mDataReader["NbreUnite"])) mClass._NbreUnite = (int)mDataReader["NbreUnite"];
                    if (!DBNull.Value.Equals(mDataReader["NbreUniteParPalette"])) mClass._NbreUniteParPalette = (int)mDataReader["NbreUniteParPalette"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrutUnitaire"])) mClass._PoidsBrutUnitaire = (decimal)mDataReader["PoidsBrutUnitaire"];
                    if (!DBNull.Value.Equals(mDataReader["TareUnitaireEmballage"])) mClass._TareUnitaireEmballage = (decimal)mDataReader["TareUnitaireEmballage"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrutPalette"])) mClass._PoidsBrutPalette = (decimal)mDataReader["PoidsBrutPalette"];
                    if (!DBNull.Value.Equals(mDataReader["TareEmballagePalette"])) mClass._TareEmballagePalette = (int)mDataReader["TareEmballagePalette"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetPalette"])) mClass._PoidsNetPalette = (decimal)mDataReader["PoidsNetPalette"];
                    
                    if (!DBNull.Value.Equals(mDataReader["NbreEtiquetteA4Demande"])) mClass._NbreEtiquetteA4Demande = (int)mDataReader["NbreEtiquetteA4Demande"];
                    if (!DBNull.Value.Equals(mDataReader["NbreEtiquetteA4Imprime"])) mClass._NbreEtiquetteA4Imprime = (int)mDataReader["NbreEtiquetteA4Imprime"];
                    if (!DBNull.Value.Equals(mDataReader["NbreEtiquetteA5Demande"])) mClass._NbreEtiquetteA5Demande = (int)mDataReader["NbreEtiquetteA5Demande"];
                    if (!DBNull.Value.Equals(mDataReader["NbreEtiquetteA5Imprime"])) mClass._NbreEtiquetteA5Imprime = (int)mDataReader["NbreEtiquetteA5Imprime"];

                    if (!DBNull.Value.Equals(mDataReader["DateFabrication"])) mClass._DateFabrication = (DateTime)mDataReader["DateFabrication"];
                    if (!DBNull.Value.Equals(mDataReader["QAStatut"])) mClass._QAStatut = (int)mDataReader["QAStatut"];
                    if (!DBNull.Value.Equals(mDataReader["CodeSSCC"])) mClass._CodeSSCC = (string)mDataReader["CodeSSCC"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._CreationDate = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["Declaree"])) mClass._Declaree = (bool)mDataReader["Declaree"];
                    if (!DBNull.Value.Equals(mDataReader["DateDeclaration"])) mClass._DateDeclaration = (DateTime)mDataReader["DateDeclaration"];
                    if (!DBNull.Value.Equals(mDataReader["StockMagasin"])) mClass._StockMagasin = (string)mDataReader["StockMagasin"];
                    if (!DBNull.Value.Equals(mDataReader["StockEmplacement"])) mClass._StockEmplacement = (string)mDataReader["StockEmplacement"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];


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
}
