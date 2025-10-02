using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms.Classes.Business.stock
{
    public class MouvementStockAgence : DataPersist
    {

        #region Fields
        private Guid _ID;
        private Magasin _Magasin;
        private Campagne _Campagne;
        private MouvementStockType _MouvementStockType;
        private Certification _Certification;
        private Exportateur _Exportateur;
        private SacType _SacType;
        private Guid? _ObjetEnStock;
        private int? _ObjetEnStockType;
        private string _Reference1;
        private string _Reference2;
        private string _Reference3;
        private DateTime _DateMouvement;
        private Int16 _Sens;
        private decimal _Quantite;
        private decimal _PoidsBrut;
        private decimal _TareSacs;
        private decimal _TarePalettes;
        private decimal _PoidsNetLivre;
        private decimal _Retention;
        private decimal _PoidsNetAccepte;
        private string _Statut;
        private string _Commentaire;
        private bool _Desactive;
        private string _Approbateur;
        private DateTime? _DateApprobation;
        private string _LivraisonID;
        private string _BordereauNumero;
        private string _LotNumero;
        private string _Immatriculation;
        private TypeElementStock _TypeElementStock;
        private Emplacement _Emplacement;
        private Site _Sites;
        private int _NombreLots;
        #endregion

        #region Properties
        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get
            {
                return _ID;
            }

            set
            {
                _ID = value;
            }
        }

        public Magasin Magasin
        {
            get
            {
                return _Magasin;
            }

            set
            {
                _Magasin = value;
            }
        }
        public string MagasinAsString
        {
            get
            {
                return _Magasin != null ? _Magasin.Designation : string.Empty;
            }
        }

        [Column(Text = "Campagne")]
        public Campagne mCampagne
        {
            get
            {
                return _Campagne;
            }

            set
            {
                _Campagne = value;
            }
        }

        public MouvementStockType MouvementStockType
        {
            get
            {
                return _MouvementStockType;
            }

            set
            {
                _MouvementStockType = value;
            }
        }

        public string MouvementTypeAsString
        {
            get
            {
                return _MouvementStockType != null ? _MouvementStockType.Designation : string.Empty;
            }
        }

        public Certification Certification
        {
            get
            {
                return _Certification;
            }

            set
            {
                _Certification = value;
            }
        }

        public string CertificationAsString
        {
            get
            {
                return _Certification != null ? _Certification.Designation : string.Empty;
            }
        }
        public SacType SacType
        {
            get
            {
                return _SacType;
            }

            set
            {
                _SacType = value;
            }
        }

        public string SacTypeAsString
        {
            get
            {
                return _SacType != null ? _SacType.Designation : string.Empty;
            }
        }
        public Guid? ObjetEnStock
        {
            get
            {
                return _ObjetEnStock;
            }

            set
            {
                _ObjetEnStock = value;
            }
        }

        public int? ObjetEnStockType
        {
            get
            {
                return _ObjetEnStockType;
            }

            set
            {
                _ObjetEnStockType = value;
            }
        }

        public string Reference1
        {
            get
            {
                return _Reference1;
            }

            set
            {
                _Reference1 = value;
            }
        }

        public string Reference2
        {
            get
            {
                return _Reference2;
            }

            set
            {
                _Reference2 = value;
            }
        }

        public DateTime DateMouvement
        {
            get
            {
                return _DateMouvement;
            }

            set
            {
                _DateMouvement = value;
            }
        }

        public string DateMouvementAsString
        {
            get { return _DateMouvement.ToShortDateString(); }
        }
        public Int16 Sens
        {
            get
            {
                return _Sens;
            }

            set
            {
                _Sens = value;
            }
        }

        public decimal Quantite
        {
            get
            {
                return _Quantite;
            }

            set
            {
                _Quantite = value;
            }
        }

        public string QuantiteAsString
        {
            get { return _Quantite != 0 ? String.Format("{0:#,#}", _Quantite).TrimStart() : string.Empty; }
        }

        public string NombreLotsAsString
        {
            get { return _NombreLots != 0 ? String.Format("{0:#,#}", _NombreLots).TrimStart() : string.Empty; }
        }

        public decimal PoidsBrut
        {
            get
            {
                return _PoidsBrut;
            }

            set
            {
                _PoidsBrut = value;
            }
        }

        public string PoidsBrutAsString
        {
            get { return _PoidsBrut != 0 ? String.Format("{0:#,#}", _PoidsBrut).TrimStart() : string.Empty; }
        }

        public decimal TareSacs
        {
            get
            {
                return _TareSacs;
            }

            set
            {
                _TareSacs = value;
            }
        }

        public string TareSacsAsString
        {
            get { return _TareSacs != 0 ? String.Format("{0:#,#}", _TareSacs).TrimStart() : string.Empty; }
        }

        public decimal TarePalettes
        {
            get
            {
                return _TarePalettes;
            }

            set
            {
                _TarePalettes = value;
            }
        }

        public string TarePalettesAsString
        {
            get { return _TarePalettes != 0 ? String.Format("{0:#,#}", _TarePalettes).TrimStart() : string.Empty; }
        }

        public decimal PoidsNetLivre
        {
            get
            {
                return _PoidsNetLivre;
            }

            set
            {
                _PoidsNetLivre = value;
            }
        }

        public string PoidsNetLivreAsString
        {
            get { return _PoidsNetLivre != 0 ? String.Format("{0:#,#}", _PoidsNetLivre).TrimStart() : string.Empty; }
        }

        public decimal Retention
        {
            get
            {
                return _Retention;
            }

            set
            {
                _Retention = value;
            }
        }

        public string RetentionAsString
        {
            get { return _Retention != 0 ? String.Format("{0:#,#}", _Retention).TrimStart() : string.Empty; }
        }

        public decimal PoidsNetAccepte
        {
            get
            {
                return _PoidsNetAccepte;
            }

            set
            {
                _PoidsNetAccepte = value;
            }
        }
        public string PoidsNetAccepteAsString
        {
            get { return _PoidsNetAccepte != 0 ? String.Format("{0:#,#}", _PoidsNetAccepte).TrimStart() : string.Empty; }
        }
        public string Statut
        {
            get
            {
                return _Statut;
            }

            set
            {
                _Statut = value;
            }
        }

        public string Commentaire
        {
            get
            {
                return _Commentaire;
            }

            set
            {
                _Commentaire = value;
            }
        }

        public bool Desactive
        {
            get
            {
                return _Desactive;
            }

            set
            {
                _Desactive = value;
            }
        }

        public Exportateur Exportateur
        {
            get
            {
                return _Exportateur;
            }

            set
            {
                _Exportateur = value;
            }
        }

        public string ExportateurAsString
        {
            get
            {
                return _Exportateur != null ? _Exportateur.Nom : string.Empty;
            }
        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desactive || _Statut == "CA")
                    return 0; // BulletCross
                else if (_Sens == 1)
                    return 1; // Tick
                else if (_Sens == -1)
                    return -1;
                else
                    return 1;
            }
        }

        public string Approbateur
        {
            get
            {
                return _Approbateur;
            }

            set
            {
                _Approbateur = value;
            }
        }

        public DateTime? DateApprobation
        {
            get
            {
                return _DateApprobation;
            }

            set
            {
                _DateApprobation = value;
            }
        }

        public string LivraisonID
        {
            get
            {
                return _LivraisonID;
            }

            set
            {
                _LivraisonID = value;
            }
        }

        public string BordereauNumero
        {
            get
            {
                return _BordereauNumero;
            }

            set
            {
                _BordereauNumero = value;
            }
        }

        public string LotNumero
        {
            get
            {
                return _LotNumero;
            }

            set
            {
                _LotNumero = value;
            }
        }

        public string Immatriculation
        {
            get
            {
                return _Immatriculation;
            }

            set
            {
                _Immatriculation = value;
            }
        }

        public TypeElementStock TypeElementStock
        {
            get
            {
                return _TypeElementStock;
            }

            set
            {
                _TypeElementStock = value;
            }
        }

        public string TypeElementStockAsString
        {
            get
            {
                return _TypeElementStock != null ? _TypeElementStock.Designation : string.Empty;
            }
        }

        public string DesignationStock
        {
            get
            {
                return _TypeElementStock != null ? _TypeElementStock.Mouvement : string.Empty;
            }
        }

        public Emplacement Emplacement
        {
            get
            {
                return _Emplacement;
            }

            set
            {
                _Emplacement = value;
            }
        }

        public string EmplacementAsString
        {
            get
            {
                return _Emplacement != null ? _Emplacement.Designation : string.Empty;
            }
        }

        public string SiteAsString
        {
            get
            {
                return _Sites != null ? _Sites.Nom : string.Empty;
            }
        }

        public string Reference3
        {
            get
            {
                return _Reference3;
            }

            set
            {
                _Reference3 = value;
            }
        }

        public Site Sites
        {
            get
            {
                return _Sites;
            }

            set
            {
                _Sites = value;
            }
        }

        public int NombreLots
        {
            get
            {
                return _NombreLots;
            }

            set
            {
                _NombreLots = value;
            }
        }
        #endregion

        #region Constructor
        public MouvementStockAgence()
        {

        }

        public MouvementStockAgence(Guid myId)
        {
            this.fnGet(myId);
        }
        #endregion
        #region Methods
        public override bool fnActivate()
        {
            throw new NotImplementedException();
        }

        public List<DataPersist> fnSelectPendingForSite(string mCampagne, int MagasinID, int ExportateurID, int sens, int mouvementTypeID, int certificationID, int siteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_MouvementStock_SelectPendingMouvementForSite");
                db().AddInParameter(mCommande, "@magasinID", SqlDbType.Int, MagasinID);
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, mCampagne);
                db().AddInParameter(mCommande, "@exportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@mouvementTypeID", SqlDbType.Int, mouvementTypeID);
                db().AddInParameter(mCommande, "@sens", SqlDbType.SmallInt, sens);
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, StartDate);
                //db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@certificationID", SqlDbType.Int, certificationID);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    MouvementStockAgence mClass = new MouvementStockAgence();
                    MapFromDataReaderLite(mClass, mDataReader);
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


        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_MouvementStock_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "MouvementStock:fnDeActivate");
            }
            return bolResult;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_MouvementStock_Get", (Guid)Id);
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

        public bool fnGetLotForRecleaning(object LotId)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_MouvementStock_GetLotForRecleaning", (Guid)LotId);
                if (mDataReader.Read())
                {
                    MapIdFromDataReader(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetLotForRecleaning");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }


        public override List<DataPersist> fnSelect()
        {
            return fnSelect("{Tous}", -1, -1, null, null, -2, -1, -1);
        }

        public List<DataPersist> fnSelect(string mCampagne, int MagasinID, int ExportateurID, DateTime? StartDate, DateTime? EndDate, int sens, int mouvementTypeID, int certificationID, int EmplacementID = 1, string statut = "-1")
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_MouvementStock_Select");
                db().AddInParameter(mCommande, "@magasinID", SqlDbType.Int, MagasinID);
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, mCampagne);
                db().AddInParameter(mCommande, "@exportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@mouvementTypeID", SqlDbType.Int, mouvementTypeID);
                db().AddInParameter(mCommande, "@sens", SqlDbType.SmallInt, sens);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@certificationID", SqlDbType.Int, certificationID);
                db().AddInParameter(mCommande, "@EmplacementID", SqlDbType.Int, EmplacementID);
                db().AddInParameter(mCommande, "@status", SqlDbType.Char, 2, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    MouvementStockAgence mClass = new MouvementStockAgence();
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

        public List<DataPersist> fnSelectPending(string mCampagne, int MagasinID, int ExportateurID, int sens, int mouvementTypeID, int certificationID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_MouvementStock_SelectPendingMouvement");
                db().AddInParameter(mCommande, "@magasinID", SqlDbType.Int, MagasinID);
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, mCampagne);
                db().AddInParameter(mCommande, "@exportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@mouvementTypeID", SqlDbType.Int, mouvementTypeID);
                db().AddInParameter(mCommande, "@sens", SqlDbType.SmallInt, sens);
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, StartDate);
                //db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@certificationID", SqlDbType.Int, certificationID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    MouvementStockAgence mClass = new MouvementStockAgence();
                    MapFromDataReaderLite(mClass, mDataReader);
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

        public List<DataPersist> fnSelectStockQuantite(string mCampagne, int MagasinID, DateTime? StartDate, DateTime? EndDate)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_StatutStockQuantite_Select");
                db().AddInParameter(mCommande, "@magasinID", SqlDbType.Int, MagasinID);
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, mCampagne);
                //db().AddInParameter(mCommande, "@exportateurID", SqlDbType.Int, ExportateurID);                            
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, EndDate);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    MouvementStockAgence mClass = new MouvementStockAgence();
                    MapFromDataReaderStockQte(mClass, mDataReader);
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

        public List<DataPersist> fnSelectStockItem(string mCampagne, int MagasinID, int ExportateurID, DateTime? StartDate, DateTime? EndDate, int emplacementID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_StatutStockItem_Select");
                db().AddInParameter(mCommande, "@magasinID", SqlDbType.Int, MagasinID);
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, mCampagne);
                db().AddInParameter(mCommande, "@exportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, null);
                db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@emplacementID", SqlDbType.Int, emplacementID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    MouvementStockAgence mClass = new MouvementStockAgence();
                    MapFromDataReaderStockItem(mClass, mDataReader);
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

        public List<DataPersist> fnSelectStockItemSite(string mCampagne, int MagasinID, int ExportateurID, DateTime? StartDate, DateTime? EndDate, int emplacementID = -1, int siteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_StatutStockItemForSite_Select");
                db().AddInParameter(mCommande, "@magasinID", SqlDbType.Int, MagasinID);
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, mCampagne);
                db().AddInParameter(mCommande, "@exportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, null);
                db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@emplacementID", SqlDbType.Int, emplacementID);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    MouvementStockAgence mClass = new MouvementStockAgence();
                    MapFromDataReaderStockItemSite(mClass, mDataReader);
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
        public List<DataPersist> fnSelectStockLot(string mCampagne, string MagasinID, int ExportateurID, DateTime? StartDate, DateTime? EndDate, int emplacementID = -1, int siteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V5_StatutStockLot_Select");
                db().AddInParameter(mCommande, "@magasinID", SqlDbType.VarChar, MagasinID);
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, mCampagne);
                db().AddInParameter(mCommande, "@exportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, null);
                db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@emplacementID", SqlDbType.Int, emplacementID);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    MouvementStockAgence mClass = new MouvementStockAgence();
                    MapFromDataReaderStockLot(mClass, mDataReader);
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

        public List<DataPersist> fnSelect_SituationLot(string mCampagne, string MagasinID, int ExportateurID, DateTime? StartDate, DateTime? EndDate, int emplacementID = -1, int siteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V5_SituationStockLot_Select");
                db().AddInParameter(mCommande, "@magasinID", SqlDbType.VarChar, MagasinID);
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, mCampagne);
                db().AddInParameter(mCommande, "@exportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, null);
                db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@emplacementID", SqlDbType.Int, emplacementID);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    MouvementStockAgence mClass = new MouvementStockAgence();
                    MapFromDataReaderSituationLot(mClass, mDataReader);
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
            Parametres mParam = new Parametres();
            if (_Sites == null)
                mParam = new Parametres(0);

            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("V2_MouvementStock_New");

                db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                db().AddInParameter(mCommande, "@magasinId", SqlDbType.Int, _Magasin.ID);
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@exportateurId", SqlDbType.Int, _Exportateur.ID);
                db().AddInParameter(mCommande, "@datemouvement", SqlDbType.DateTime, _DateMouvement);
                db().AddInParameter(mCommande, "@sens", SqlDbType.SmallInt, _Sens);
                db().AddInParameter(mCommande, "@mouvementTypeId", SqlDbType.Int, _MouvementStockType.ID);
                db().AddInParameter(mCommande, "@objectEnStockID", SqlDbType.UniqueIdentifier, _ObjetEnStock);
                db().AddInParameter(mCommande, "@objectEnStockType", SqlDbType.Int, _ObjetEnStockType);
                if (_Certification != null)
                    db().AddInParameter(mCommande, "@certificationID", SqlDbType.Int, _Certification.ID);
                else
                    db().AddInParameter(mCommande, "@certificationID", SqlDbType.Int, DBNull.Value);
                db().AddInParameter(mCommande, "@reference1", SqlDbType.VarChar, _Reference1);
                db().AddInParameter(mCommande, "@reference2", SqlDbType.VarChar, _Reference2);

                if (_SacType != null)
                    db().AddInParameter(mCommande, "@sactypeId", SqlDbType.Int, _SacType.ID);
                else
                    db().AddInParameter(mCommande, "@sactypeId", SqlDbType.Int, DBNull.Value);

                if (_Sites != null)
                    db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, _Sites.ID);
                else
                    db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, mParam.Site);

                db().AddInParameter(mCommande, "@quantite", SqlDbType.Int, _Quantite);
                db().AddInParameter(mCommande, "@poidsbrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@tarebags", SqlDbType.Decimal, _TareSacs);
                db().AddInParameter(mCommande, "@tarepalette", SqlDbType.Decimal, _TarePalettes);
                db().AddInParameter(mCommande, "@poidsnetlivre", SqlDbType.Decimal, _PoidsNetLivre);
                db().AddInParameter(mCommande, "@retention", SqlDbType.Decimal, _Retention);
                db().AddInParameter(mCommande, "@poidsnetaccepte", SqlDbType.Decimal, _PoidsNetAccepte);
                db().AddInParameter(mCommande, "@commentaire", SqlDbType.VarChar, _Commentaire);
                db().AddInParameter(mCommande, "@statut", SqlDbType.Char, 2, _Statut);
                db().AddInParameter(mCommande, "@EmplacementID", SqlDbType.Int, _Emplacement.ID);

                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                db().AddOutParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0);

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
                        //_Statut = (string)db().Parameters(mCommande, "@statut");

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
                throw new Exception(ex.Message + "\r\n" + "MouvementStock:fnUpdate");

            }
            return Result;
        }


        public bool fnUpdate(DataTransaction mTran)
        {
            Parametres mParam = new Parametres();
            if (_Sites == null)
                mParam = new Parametres(0);

            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("V2_MouvementStock_New");

                db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                db().AddInParameter(mCommande, "@magasinId", SqlDbType.Int, _Magasin.ID);
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@exportateurId", SqlDbType.Int, _Exportateur.ID);
                db().AddInParameter(mCommande, "@datemouvement", SqlDbType.DateTime, _DateMouvement);
                db().AddInParameter(mCommande, "@sens", SqlDbType.SmallInt, _Sens);
                db().AddInParameter(mCommande, "@mouvementTypeId", SqlDbType.Int, _MouvementStockType.ID);
                db().AddInParameter(mCommande, "@objectEnStockID", SqlDbType.UniqueIdentifier, _ObjetEnStock);
                db().AddInParameter(mCommande, "@objectEnStockType", SqlDbType.Int, _ObjetEnStockType);
                if (_Certification != null)
                    db().AddInParameter(mCommande, "@certificationID", SqlDbType.Int, _Certification.ID);
                else
                    db().AddInParameter(mCommande, "@certificationID", SqlDbType.Int, DBNull.Value);
                db().AddInParameter(mCommande, "@reference1", SqlDbType.VarChar, _Reference1);
                db().AddInParameter(mCommande, "@reference2", SqlDbType.VarChar, _Reference2);

                if (_SacType != null)
                    db().AddInParameter(mCommande, "@sactypeId", SqlDbType.Int, _SacType.ID);
                else
                    db().AddInParameter(mCommande, "@sactypeId", SqlDbType.Int, DBNull.Value);

                if (_Sites != null)
                    db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, _Sites.ID);
                else
                    db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, mParam.Site);

                db().AddInParameter(mCommande, "@quantite", SqlDbType.Int, _Quantite);
                db().AddInParameter(mCommande, "@poidsbrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@tarebags", SqlDbType.Decimal, _TareSacs);
                db().AddInParameter(mCommande, "@tarepalette", SqlDbType.Decimal, _TarePalettes);
                db().AddInParameter(mCommande, "@poidsnetlivre", SqlDbType.Decimal, _PoidsNetLivre);
                db().AddInParameter(mCommande, "@retention", SqlDbType.Decimal, _Retention);
                db().AddInParameter(mCommande, "@poidsnetaccepte", SqlDbType.Decimal, _PoidsNetAccepte);
                db().AddInParameter(mCommande, "@commentaire", SqlDbType.VarChar, _Commentaire);
                db().AddInParameter(mCommande, "@statut", SqlDbType.Char, 2, _Statut);
                db().AddInParameter(mCommande, "@EmplacementID", SqlDbType.Int, _Emplacement.ID);

                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                db().AddOutParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0);

                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                db().ExecuteNonQuery(ref mCommande, mTran);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        base.UpdateAuditFields();
                        Result = true;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        _ID = (Guid)db().Parameters(mCommande, "@ID");
                        //_Statut = (string)db().Parameters(mCommande, "@statut");

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
                throw new Exception(ex.Message + "\r\n" + "MouvementStock:fnUpdate");

            }
            return Result;
        }

        public bool fnChangeLocation()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("V2_MouvementStock_ChangeLocation");

                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@EmplacementID", SqlDbType.Int, _Emplacement.ID);
                db().AddInParameter(mCommande, "@commentaire", SqlDbType.VarChar, _Commentaire);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                db().AddOutParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0);

                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                db().ExecuteNonQuery(ref mCommande);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        base.UpdateAuditFields();
                        Result = true;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");

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
                throw new Exception(ex.Message + "\r\n" + "MouvementStock:fnChangeLocation");

            }
            return Result;
        }


        public override string ToString()
        {
            throw new NotImplementedException();
        }

        private static void MapFromDataReader(MouvementStockAgence mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass._Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["MagasinID"]))
                    {
                        mClass._Magasin = new Magasin();
                        mClass._Magasin.ID = (int)mDataReader["MagasinID"];
                        mClass._Magasin.Designation = (string)mDataReader["MagasinNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["MouvementTypeID"]))
                    {
                        mClass._MouvementStockType = new MouvementStockType();
                        mClass._MouvementStockType.ID = (int)mDataReader["MouvementTypeID"];
                        mClass._MouvementStockType.Designation = (string)mDataReader["MouvementTypeDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ExportateurID"]))
                    {
                        mClass._Exportateur = new Exportateur();
                        mClass._Exportateur.ID = (int)mDataReader["ExportateurID"];
                        mClass._Exportateur.Nom = (string)mDataReader["ExportateurNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["CertificationID"]))
                    {
                        mClass._Certification = new Certification();
                        mClass._Certification.ID = (int)mDataReader["CertificationID"];
                        mClass._Certification.Designation = (string)mDataReader["CertificationDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["SacTypeID"]))
                    {
                        mClass._SacType = new SacType();
                        mClass._SacType.ID = (int)mDataReader["SacTypeID"];
                        mClass._SacType.Designation = (string)mDataReader["SacTypeDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["EmplacementID"]))
                    {
                        mClass._Emplacement = new Emplacement();
                        mClass._Emplacement.ID = (int)mDataReader["EmplacementID"];
                        mClass._Emplacement.Designation = (string)mDataReader["EmplacementDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ObjetEnStockID"])) mClass._ObjetEnStock = (Guid)mDataReader["ObjetEnStockID"];
                    if (!DBNull.Value.Equals(mDataReader["ObjetEnStockType"])) mClass._ObjetEnStockType = (int)mDataReader["ObjetEnStockType"];
                    if (!DBNull.Value.Equals(mDataReader["Reference1"])) mClass._Reference1 = (string)mDataReader["Reference1"];
                    if (!DBNull.Value.Equals(mDataReader["Reference2"])) mClass._Reference2 = (string)mDataReader["Reference2"];
                    if (!DBNull.Value.Equals(mDataReader["Reference3"])) mClass._Reference3 = (string)mDataReader["Reference3"];
                    if (!DBNull.Value.Equals(mDataReader["DateMouvement"])) mClass._DateMouvement = (DateTime)mDataReader["DateMouvement"];
                    if (!DBNull.Value.Equals(mDataReader["Sens"])) mClass._Sens = (Int16)mDataReader["Sens"];
                    if (!DBNull.Value.Equals(mDataReader["Quantite"])) mClass._Quantite = (int)mDataReader["Quantite"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalettes = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetLivre"])) mClass._PoidsNetLivre = (decimal)mDataReader["PoidsNetLivre"];
                    if (!DBNull.Value.Equals(mDataReader["RetentionPoids"])) mClass._Retention = (decimal)mDataReader["RetentionPoids"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetAccepte"])) mClass._PoidsNetAccepte = (decimal)mDataReader["PoidsNetAccepte"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];
                    if (!DBNull.Value.Equals(mDataReader["ApprobationUtilisateur"])) mClass._Approbateur = (string)mDataReader["ApprobationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["ApprobationDate"])) mClass._DateApprobation = (DateTime)mDataReader["ApprobationDate"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nMouvementStock:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderLite(MouvementStockAgence mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    mClass._ID = Guid.NewGuid();

                    mClass._Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass._Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["MagasinID"]))
                    {
                        mClass._Magasin = new Magasin();
                        mClass._Magasin.ID = (int)mDataReader["MagasinID"];
                        //mClass._Magasin.Designation = (string)mDataReader["MagasinNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["MouvementTypeID"]))
                    {
                        mClass._MouvementStockType = new MouvementStockType();
                        mClass._MouvementStockType.ID = (int)mDataReader["MouvementTypeID"];
                        mClass._MouvementStockType.Designation = (string)mDataReader["MouvementTypeDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ExportateurID"]))
                    {
                        mClass._Exportateur = new Exportateur();
                        mClass._Exportateur.ID = (int)mDataReader["ExportateurID"];
                        mClass._Exportateur.Nom = (string)mDataReader["ExportateurNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["CertificationID"]))
                    {
                        mClass._Certification = new Certification();
                        mClass._Certification.ID = (int)mDataReader["CertificationID"];
                        mClass._Certification.Designation = (string)mDataReader["CertificationDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["SacTypeID"]))
                    {
                        mClass._SacType = new SacType();
                        mClass._SacType.ID = (int)mDataReader["SacTypeID"];
                        mClass._SacType.Designation = (string)mDataReader["SacTypeDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ObjetEnStockID"])) mClass._ObjetEnStock = (Guid)mDataReader["ObjetEnStockID"];
                    if (!DBNull.Value.Equals(mDataReader["ObjetEnStockType"])) mClass._ObjetEnStockType = (int)mDataReader["ObjetEnStockType"];
                    if (!DBNull.Value.Equals(mDataReader["Reference1"])) mClass._Reference1 = (string)mDataReader["Reference1"];
                    if (!DBNull.Value.Equals(mDataReader["Reference2"])) mClass._Reference2 = (string)mDataReader["Reference2"];
                    if (!DBNull.Value.Equals(mDataReader["Reference3"])) mClass._Reference3 = (string)mDataReader["Reference3"];
                    if (!DBNull.Value.Equals(mDataReader["DateMouvement"])) mClass._DateMouvement = (DateTime)mDataReader["DateMouvement"];
                    if (!DBNull.Value.Equals(mDataReader["Sens"])) mClass._Sens = (Int16)mDataReader["Sens"];
                    if (!DBNull.Value.Equals(mDataReader["Quantite"])) mClass._Quantite = (int)mDataReader["Quantite"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalettes = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetLivre"])) mClass._PoidsNetLivre = (decimal)mDataReader["PoidsNetLivre"];
                    if (!DBNull.Value.Equals(mDataReader["RetentionPoids"])) mClass._Retention = (decimal)mDataReader["RetentionPoids"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetAccepte"])) mClass._PoidsNetAccepte = (decimal)mDataReader["PoidsNetAccepte"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nMouvementStock:MapFromDataReaderLite");
            }
        }

        private static void MapFromDataReaderStockQte(MouvementStockAgence mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    mClass._ID = Guid.NewGuid();

                    if (!DBNull.Value.Equals(mDataReader["MagasinID"]))
                    {
                        mClass._Magasin = new Magasin();
                        mClass._Magasin.ID = (int)mDataReader["MagasinID"];
                        mClass._Magasin.Designation = (string)mDataReader["MagasinNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["MouvementTypeID"]))
                    {
                        mClass._MouvementStockType = new MouvementStockType();
                        mClass._MouvementStockType.ID = (int)mDataReader["MouvementTypeID"];
                        mClass._MouvementStockType.Designation = (string)mDataReader["MouvementTypeDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Sens"])) mClass._Sens = (Int16)mDataReader["Sens"];
                    if (!DBNull.Value.Equals(mDataReader["Quantite"])) mClass._Quantite = (int)mDataReader["Quantite"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalettes = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetLivre"])) mClass._PoidsNetLivre = (decimal)mDataReader["PoidsNetLivre"];
                    if (!DBNull.Value.Equals(mDataReader["RetentionPoids"])) mClass._Retention = (decimal)mDataReader["RetentionPoids"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetAccepte"])) mClass._PoidsNetAccepte = (decimal)mDataReader["PoidsNetAccepte"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nMouvementStock:MapFromDataReaderStockQte");
            }
        }

        private static void MapFromDataReaderStockItem(MouvementStockAgence mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    mClass._ID = Guid.NewGuid();

                    //if (!DBNull.Value.Equals(mDataReader["MagasinID"]))
                    //{
                    //    mClass._Magasin = new Magasin();
                    //    mClass._Magasin.ID = (int)mDataReader["MagasinID"];
                    //    mClass._Magasin.Designation = (string)mDataReader["MagasinNom"];
                    //}

                    //if (!DBNull.Value.Equals(mDataReader["MouvementTypeID"]))
                    //{
                    //    mClass._MouvementStockType = new MouvementStockType();
                    //    mClass._MouvementStockType.ID = (int)mDataReader["MouvementTypeID"];
                    //    mClass._MouvementStockType.Designation = (string)mDataReader["MouvementTypeDesignation"];
                    //}

                    if (!DBNull.Value.Equals(mDataReader["ElementTypeID"]))
                    {
                        mClass._TypeElementStock = new TypeElementStock();
                        mClass._TypeElementStock.ID = (int)mDataReader["ElementTypeID"];
                        mClass._TypeElementStock.Designation = (string)mDataReader["ElementTypeDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Reference"])) mClass._Reference1 = (string)mDataReader["Reference"];
                    //if (!DBNull.Value.Equals(mDataReader["BordereauNumero"])) mClass._ob = (string)mDataReader["BordereauNumero"];
                    //if (!DBNull.Value.Equals(mDataReader["BordereauNumero"])) mClass._BordereauNumero = (string)mDataReader["BordereauNumero"];
                    //if (!DBNull.Value.Equals(mDataReader["LotNumero"])) mClass._LotNumero = (string)mDataReader["LotNumero"];
                    if (!DBNull.Value.Equals(mDataReader["Immatriculation"])) mClass._Immatriculation = (string)mDataReader["Immatriculation"];

                    //if (!DBNull.Value.Equals(mDataReader["Sens"])) mClass._Sens = (Int16)mDataReader["Sens"];
                    if (!DBNull.Value.Equals(mDataReader["Quantite"])) mClass._Quantite = (int)mDataReader["Quantite"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalettes = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetLivre"])) mClass._PoidsNetLivre = (decimal)mDataReader["PoidsNetLivre"];
                    if (!DBNull.Value.Equals(mDataReader["RetentionPoids"])) mClass._Retention = (decimal)mDataReader["RetentionPoids"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetAccepte"])) mClass._PoidsNetAccepte = (decimal)mDataReader["PoidsNetAccepte"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nMouvementStock:MapFromDataReaderStockItem");
            }
        }

        private static void MapFromDataReaderStockItemSite(MouvementStockAgence mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    mClass._ID = Guid.NewGuid();

                    //if (!DBNull.Value.Equals(mDataReader["MagasinID"]))
                    //{
                    //    mClass._Magasin = new Magasin();
                    //    mClass._Magasin.ID = (int)mDataReader["MagasinID"];
                    //    mClass._Magasin.Designation = (string)mDataReader["MagasinNom"];
                    //}

                    //if (!DBNull.Value.Equals(mDataReader["MouvementTypeID"]))
                    //{
                    //    mClass._MouvementStockType = new MouvementStockType();
                    //    mClass._MouvementStockType.ID = (int)mDataReader["MouvementTypeID"];
                    //    mClass._MouvementStockType.Designation = (string)mDataReader["MouvementTypeDesignation"];
                    //}

                    if (!DBNull.Value.Equals(mDataReader["ElementTypeID"]))
                    {
                        mClass._TypeElementStock = new TypeElementStock();
                        mClass._TypeElementStock.ID = (int)mDataReader["ElementTypeID"];
                        mClass._TypeElementStock.Designation = (string)mDataReader["ElementTypeDesignation"];
                        mClass._TypeElementStock.Mouvement = (string)mDataReader["mouvement"];
                    }

                    //if (!DBNull.Value.Equals(mDataReader["Reference"])) mClass._Reference1 = (string)mDataReader["Reference"];
                    //if (!DBNull.Value.Equals(mDataReader["BordereauNumero"])) mClass._ob = (string)mDataReader["BordereauNumero"];
                    //if (!DBNull.Value.Equals(mDataReader["BordereauNumero"])) mClass._BordereauNumero = (string)mDataReader["BordereauNumero"];
                    //if (!DBNull.Value.Equals(mDataReader["LotNumero"])) mClass._LotNumero = (string)mDataReader["LotNumero"];
                    //if (!DBNull.Value.Equals(mDataReader["Immatriculation"])) mClass._Immatriculation = (string)mDataReader["Immatriculation"];

                    if (!DBNull.Value.Equals(mDataReader["Sens"])) mClass._Sens = (Int16)mDataReader["Sens"];
                    if (!DBNull.Value.Equals(mDataReader["Quantite"])) mClass._Quantite = (int)mDataReader["Quantite"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalettes = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetLivre"])) mClass._PoidsNetLivre = (decimal)mDataReader["PoidsNetLivre"];
                    if (!DBNull.Value.Equals(mDataReader["RetentionPoids"])) mClass._Retention = (decimal)mDataReader["RetentionPoids"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetAccepte"])) mClass._PoidsNetAccepte = (decimal)mDataReader["PoidsNetAccepte"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nMouvementStock:MapFromDataReaderStockItem");
            }
        }

        private static void MapFromDataReaderStockLot(MouvementStockAgence mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    mClass._ID = Guid.NewGuid();
                   
                    if (!DBNull.Value.Equals(mDataReader["MagasinID"]))
                    {
                        mClass._Magasin = new Magasin();
                        mClass._Magasin.ID = (int)mDataReader["MagasinID"];
                        mClass._Magasin.Designation = (string)mDataReader["MagasinNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ExportateurID"]))
                    {
                        mClass._Exportateur = new Exportateur();
                        mClass._Exportateur.ID = (int)mDataReader["ExportateurID"];
                        mClass._Exportateur.Nom = (string)mDataReader["ExportateurNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Reference"])) mClass._Reference2 = (string)mDataReader["Reference"];
                    if (!DBNull.Value.Equals(mDataReader["Quantite"])) mClass._Quantite = (int)mDataReader["Quantite"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalettes = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetLivre"])) mClass._PoidsNetLivre = (decimal)mDataReader["PoidsNetLivre"];
                    if (!DBNull.Value.Equals(mDataReader["RetentionPoids"])) mClass._Retention = (decimal)mDataReader["RetentionPoids"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetAccepte"])) mClass._PoidsNetAccepte = (decimal)mDataReader["PoidsNetAccepte"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nMouvementStock:MapFromDataReaderStockItem");
            }
        }

        private static void MapFromDataReaderSituationLot(MouvementStockAgence mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    mClass._ID = Guid.NewGuid();

                    if (!DBNull.Value.Equals(mDataReader["MagasinID"]))
                    {
                        mClass._Magasin = new Magasin();
                        mClass._Magasin.ID = (int)mDataReader["MagasinID"];
                        mClass._Magasin.Designation = (string)mDataReader["MagasinNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["SiteID"]))
                    {
                        mClass._Sites = new Site();
                        mClass._Sites.ID = (int)mDataReader["SiteID"];
                        mClass._Sites.Nom = (string)mDataReader["SiteNom"];
                    }
                    
                    if (!DBNull.Value.Equals(mDataReader["Quantite"])) mClass._Quantite = (int)mDataReader["Quantite"];
                    if (!DBNull.Value.Equals(mDataReader["NombreLot"])) mClass._NombreLots = (int)mDataReader["NombreLot"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalettes = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetLivre"])) mClass._PoidsNetLivre = (decimal)mDataReader["PoidsNetLivre"];
                    if (!DBNull.Value.Equals(mDataReader["RetentionPoids"])) mClass._Retention = (decimal)mDataReader["RetentionPoids"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetAccepte"])) mClass._PoidsNetAccepte = (decimal)mDataReader["PoidsNetAccepte"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nMouvementStock:MapFromDataReaderStockItem");
            }
        }


        private static void MapIdFromDataReader(MouvementStockAgence mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    mClass._ID = Guid.NewGuid();

                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nMouvementStock:MapIdFromDataReader");
            }
        }

        #endregion
    }

    public partial class MouvementStockAgenceViewModel
    {
        public MouvementStockAgence _MouvementStockAgence { get; set; }
        public Parametres _Parametres { get; set; }

        public string _DefaultCampagne { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
