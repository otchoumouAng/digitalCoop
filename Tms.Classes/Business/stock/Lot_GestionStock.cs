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

namespace Tms.Classes.Business.stock
{
    public class Lot_GestionStock : DataPersist
    {
        #region fields
        private Guid _ID;
        private int _IdLot;
        //private int _CodeProduit;
        private Campagne _Campagne;
        private Produit _Produit;
        private LotType _LotType;
        private Magasin _Magasin;
        private Site _Sites;
        private Exportateur _Exportateur;
        private DateTime _DateLot;
        private DateTime? _DateReusinage;
        private string _NumeroLot;
        private string _Statut;
        private int _NombreSacs;
        private int _NombreSacsReusine;
        private int _NombrePalette;
        private decimal _PoidsBrut;
        private decimal _PoidsBrutReusine;
        private decimal _TareSacs;
        private decimal _TarePalette;
        private decimal _TarePaletteReusine;
        private decimal _PoidsNet;
        private decimal _PoidsNetReusine;
        private bool _EstQueue;
        private bool _EstManuel;
        private bool _EstReusine;
        private bool _IsApproved;
        private bool _Desactive;

        private decimal _PoidsBrutN;
        private decimal _PoidsNetN;
        //private bool _EstFictif;
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

        public Campagne Campagne
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

        public string CampagneAsString
        {
            get
            {
                return _Campagne != null ? _Campagne.Designation : string.Empty;
            }
        }

        public Produit Produit
        {
            get
            {
                return _Produit;
            }

            set
            {
                _Produit = value;
            }
        }
        public string ProduitAsString
        {
            get
            {
                return _Produit != null ? _Produit.Designation : string.Empty;
            }
        }

        public LotType LotType
        {
            get
            {
                return _LotType;
            }

            set
            {
                _LotType = value;
            }
        }

        public string LotTypeAsString
        {
            get
            {
                return _LotType != null ? _LotType.Designation : string.Empty;
            }
        }

        public string ExportateurAsString
        {
            get
            {
                return _Exportateur != null ? _Exportateur.Nom : string.Empty;
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

        public DateTime DateLot
        {
            get
            {
                return _DateLot;
            }

            set
            {
                _DateLot = value;
            }
        }

        public string DateLotAsString
        {
            get
            {
                return _DateLot != null ? _DateLot.ToShortDateString() : string.Empty;
            }
        }

        public string NumeroLot
        {
            get
            {
                return _NumeroLot;
            }

            set
            {
                _NumeroLot = value;
            }
        }

        public int NombreSacs
        {
            get
            {
                return _NombreSacs;
            }

            set
            {
                _NombreSacs = value;
            }
        }
        public string NombreSacsAsString
        {
            get { return _NombreSacs != 0 ? String.Format("{0:#,#}", _NombreSacs).TrimStart() : string.Empty; }
        }
        public int NombrePalette
        {
            get
            {
                return _NombrePalette;
            }

            set
            {
                _NombrePalette = value;
            }
        }
        public string NombrePaletteAsString
        {
            get { return _NombrePalette != 0 ? String.Format("{0:#,#}", _NombrePalette).TrimStart() : string.Empty; }
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

        public decimal PoidsBrutN
        {
            get
            {
                return _PoidsBrutN;
            }

            set
            {
                _PoidsBrutN = value;
            }
        }

        public string PoidsBrutNAsString
        {
            get { return _PoidsBrutN != 0 ? String.Format("{0:#,#}", _PoidsBrutN).TrimStart() : string.Empty; }
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

        public decimal TarePalette
        {
            get
            {
                return _TarePalette;
            }

            set
            {
                _TarePalette = value;
            }
        }

        public string TarePaletteAsString
        {
            get { return _TarePalette != 0 ? String.Format("{0:#,#}", _TarePalette).TrimStart() : string.Empty; }
        }

        public decimal PoidsNet
        {
            get
            {
                return _PoidsNet;
            }

            set
            {
                _PoidsNet = value;
            }
        }

        public string PoidsNetAsString
        {
            get { return _PoidsNet != 0 ? String.Format("{0:#,#}", _PoidsNet).TrimStart() : string.Empty; }
        }

        public decimal PoidsNetN
        {
            get
            {
                return _PoidsNetN;
            }
            set
            {
                _PoidsNetN = value;
            }
        }

        public string PoidsNetNAsString
        {
            get { return _PoidsNetN != 0 ? String.Format("{0:#,#}", _PoidsNetN).TrimStart() : string.Empty; }
        }

        public bool EstQueue
        {
            get
            {
                return _EstQueue;
            }

            set
            {
                _EstQueue = value;
            }
        }

        public bool EstManuel
        {
            get
            {
                return _EstManuel;
            }

            set
            {
                _EstManuel = value;
            }
        }

        [Column(Text = "IsApproved")]
        public bool IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
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

        public bool EstReusine
        {
            get
            {
                return _EstReusine;
            }

            set
            {
                _EstReusine = value;
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

        public int IdLot
        {
            get
            {
                return _IdLot;
            }

            set
            {
                _IdLot = value;
            }
        }

        public DateTime? DateReusinage
        {
            get
            {
                return _DateReusinage;
            }

            set
            {
                _DateReusinage = value;
            }
        }

        public int NombreSacsReusine
        {
            get
            {
                return _NombreSacsReusine;
            }

            set
            {
                _NombreSacsReusine = value;
            }
        }

        public decimal PoidsBrutReusine
        {
            get
            {
                return _PoidsBrutReusine;
            }

            set
            {
                _PoidsBrutReusine = value;
            }
        }

        public decimal PoidsNetReusine
        {
            get
            {
                return _PoidsNetReusine;
            }

            set
            {
                _PoidsNetReusine = value;
            }
        }

        public decimal TarePaletteReusine
        {
            get
            {
                return _TarePaletteReusine;
            }

            set
            {
                _TarePaletteReusine = value;
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

        #endregion

        #region Constructor
        public Lot_GestionStock()
        {

        }

        public Lot_GestionStock(Guid myId)
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
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_GestionStock_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "Lot:fnDeActivate");
            }
            return bolResult;
        }

        public bool fnApprove()
        {
            if (!this._IsApproved)
            {
                bool Result;
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_GestionStock_Approve");
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
                    throw new Exception(ex.Message + "\r\n" + "Lot_GestionStock:fnApprove");
                }
                return Result;
            }
            return false;
        }

        public bool fnApprove(DataTransaction mTran)
        {
            if (!this._IsApproved)
            {
                bool Result;
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_GestionStock_Approve");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                try
                {
                    db().ExecuteNonQuery(ref mCommande,mTran);
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
                    throw new Exception(ex.Message + "\r\n" + "Lot_GestionStock:fnApprove");
                }
                return Result;
            }
            return false;
        }

        public bool fnCancelForCleaning(DataTransaction mTran)
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_GestionStock_CancelForCleaning");
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
                throw new Exception(ex.Message + "\r\n" + "Lot:fnCancelForCleaning");
            }
            return bolResult;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_Lot_GestionStock_Get", (Guid)Id);
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

        public bool fnGetByNumero(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_Lot_GestionStock_GetByNumero", (string)Id);
                if (mDataReader.Read())
                {
                    MapFromDataReader(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetByNumero");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public bool fnGetReusinageByNumero(object Numero)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_Lot_GestionStock_GetLotReusineByNumero", (string)Numero);
                if (mDataReader.Read())
                {
                    MapFromDataReaderReuisinage(this, mDataReader);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetReusinageByNumero");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public override List<DataPersist> fnSelect()
        {
            return fnSelect("-1", -1, -1, -1, null, null, "-1",-1);
        }

        public List<DataPersist> fnSelect(string mCampagne, int ProduitID, int MagasinID, int TypeLotID, DateTime? StartDate, DateTime? EndDate, string statut = "-1", int ExportateurID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_GestionStock_Select");
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, mCampagne);
                db().AddInParameter(mCommande, "@ProduitID", SqlDbType.Int, ProduitID);
                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int, TypeLotID);
                db().AddInParameter(mCommande, "@MagasinID", SqlDbType.Int, MagasinID);
                db().AddInParameter(mCommande, "@dateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@dateFin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@statut", SqlDbType.Char, 2, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Lot_GestionStock mClass = new Lot_GestionStock();
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




        public List<DataPersist> fnSelectByNumero(string Numero)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_GestionStock_SelectByNumero");
                db().AddInParameter(mCommande, "@Numero", SqlDbType.Char, 8, Numero);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Lot_GestionStock mClass = new Lot_GestionStock();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectByNumero");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectForReCleaning(string Numero)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_GestionStock_SelectForReCleaning");
                db().AddInParameter(mCommande, "@Numero", SqlDbType.Char, 8, Numero);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Lot_GestionStock mClass = new Lot_GestionStock();

                    MapFromDataReaderReuisinage(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectForReCleaning");
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
                    mCommande = db().CreateStoredProcCommand("V2_Lot_GestionStock_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@NumeroLot", SqlDbType.VarChar, 20, _NumeroLot);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_Lot_GestionStock_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@NumeroLot", SqlDbType.VarChar, 20, _NumeroLot);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@ProduitID", SqlDbType.Int, _Produit.ID);

                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int, _LotType.ID);

                if (_Magasin != null)
                    db().AddInParameter(mCommande, "@MagasinID", SqlDbType.Int, _Magasin.ID);
                else
                    db().AddInParameter(mCommande, "@MagasinID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@DateLot", SqlDbType.DateTime, _DateLot);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, _Exportateur.ID);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@NombrePalette", SqlDbType.Int, _NombrePalette);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, _TareSacs);
                db().AddInParameter(mCommande, "@TarePalettes", SqlDbType.Decimal, _TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNet);
                db().AddInParameter(mCommande, "@EstQueue", SqlDbType.Bit, _EstQueue);
                db().AddInParameter(mCommande, "@EstManuel", SqlDbType.Bit, _EstManuel);
                db().AddInParameter(mCommande, "@EstReusine", SqlDbType.Bit, _EstReusine);
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
                        if (_isnew)
                            _NumeroLot = (string)db().Parameters(mCommande, "@NumeroLot");
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
                throw new Exception(ex.Message + "\r\n" + "Lot:fnUpdate");

            }
            return Result;
        }

        public bool fnSubmit_ListeLot()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("V5_LotAPI_New");
                db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddInParameter(mCommande, "@id_lot", SqlDbType.Int, _IdLot);
                db().AddInParameter(mCommande, "@numerolot", SqlDbType.VarChar, 20, _NumeroLot);
                db().AddInParameter(mCommande, "@code_exportateur", SqlDbType.Int, _Exportateur.ID);
                db().AddInParameter(mCommande, "@exportateur", SqlDbType.NVarChar, _Exportateur.Nom);
                db().AddInParameter(mCommande, "@code_Magasin", SqlDbType.Int, _Magasin.ID);
                db().AddInParameter(mCommande, "@MagasinLibelle", SqlDbType.NVarChar, _Magasin.Designation);
                db().AddInParameter(mCommande, "@code_site", SqlDbType.Int, _Sites.ID);
                db().AddInParameter(mCommande, "@SiteLibelle", SqlDbType.NVarChar, _Sites.Nom);
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.VarChar, _Campagne.Designation);
                db().AddInParameter(mCommande, "@code_produit", SqlDbType.Int, _Produit.ID);
                db().AddInParameter(mCommande, "@dateusinage", SqlDbType.DateTime, _DateLot);
                db().AddInParameter(mCommande, "@nombresacusine", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@poidsbrut", SqlDbType.Float, _PoidsBrut);
                db().AddInParameter(mCommande, "@poidsusine", SqlDbType.Float, _PoidsNet);
                db().AddInParameter(mCommande, "@taresacs", SqlDbType.Float, _TareSacs);
                db().AddInParameter(mCommande, "@tarepalette", SqlDbType.Float, _TarePalette);
                if (_DateReusinage  == null)
                    db().AddInParameter(mCommande, "@datereusinage", SqlDbType.DateTime, DBNull.Value);
                else
                    db().AddInParameter(mCommande, "@datereusinage", SqlDbType.DateTime, _DateReusinage);

                //db().AddInParameter(mCommande, "@datereusinage", SqlDbType.DateTime, _DateReusinage);
                db().AddInParameter(mCommande, "@nombresacreusine", SqlDbType.Int, _NombreSacsReusine);
                db().AddInParameter(mCommande, "@nombrepalette", SqlDbType.Int, _NombrePalette);
                db().AddInParameter(mCommande, "@poidsbrutreusinage", SqlDbType.Float, _PoidsBrutReusine);
                db().AddInParameter(mCommande, "@poidsreusinage", SqlDbType.Float, PoidsNetReusine);
                db().AddInParameter(mCommande, "@tarepalettereusinage", SqlDbType.Float, TarePaletteReusine);

                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                //if (!this._isnew)
                //{
                //    db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
                //}
                //else
                //{
                //    db().AddOutParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0);
                //}

                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                db().ExecuteNonQuery(ref mCommande);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        base.UpdateAuditFields();
                        Result = true;
                       //_RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        //_ID = (Guid)db().Parameters(mCommande, "@ID");
                        //if (_isnew)
                        //    _NumeroLot = (string)db().Parameters(mCommande, "@NumeroLot");
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
                throw new Exception(ex.Message + "\r\n" + "Lot:fnUpdate");

            }
            return Result;
        }

        public bool fnUpdate(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {
                    mCommande = db().CreateStoredProcCommand("V2_Lot_GestionStock_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    //db().AddOutParameter(mCommande, "@NumeroLot", SqlDbType.Char, 8);
                    db().AddInParameter(mCommande, "@NumeroLot", SqlDbType.Char, 9, _NumeroLot);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_Lot_GestionStock_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@NumeroLot", SqlDbType.Char, 9, _NumeroLot);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@ProduitID", SqlDbType.Int, _Produit.ID);
               
                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int, _LotType.ID);
                if (_Magasin != null)
                    db().AddInParameter(mCommande, "@MagasinID", SqlDbType.Int, _Magasin.ID);
                else
                    db().AddInParameter(mCommande, "@MagasinID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@DateLot", SqlDbType.DateTime, _DateLot);

                //db().AddInParameter(mCommande, "@DateProduction", SqlDbType.DateTime, _Production.DateProduction);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, _Exportateur.ID);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@NombrePalette", SqlDbType.Int, _NombrePalette);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, _TareSacs);
                db().AddInParameter(mCommande, "@TarePalettes", SqlDbType.Decimal, _TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNet);
                db().AddInParameter(mCommande, "@EstQueue", SqlDbType.Bit, _EstQueue);
                db().AddInParameter(mCommande, "@EstManuel", SqlDbType.Bit, _EstManuel);
                db().AddInParameter(mCommande, "@EstReusine", SqlDbType.Bit, _EstReusine);
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
                db().ExecuteNonQuery(ref mCommande, mTran);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        base.UpdateAuditFields();
                        Result = true;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        _ID = (Guid)db().Parameters(mCommande, "@ID");
                        if (_isnew)
                            _NumeroLot = (string)db().Parameters(mCommande, "@NumeroLot");
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
                throw new Exception(ex.Message + "\r\n" + "Lot:fnUpdate");

            }
            return Result;
        }

        public override string ToString()
        {
            return _NumeroLot;
        }


        private static void MapFromDataReader(Lot_GestionStock mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass._Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["ProduitID"]))
                    {
                        mClass._Produit = new Produit();
                        mClass._Produit.ID = (int)mDataReader["ProduitID"];
                        mClass._Produit.Designation = (string)mDataReader["ProduitNom"];
                    }                    

                    if (!DBNull.Value.Equals(mDataReader["TypeLotID"]))
                    {
                        mClass._LotType = new LotType();
                        mClass._LotType.ID = (int)mDataReader["TypeLotID"];
                        mClass._LotType.Designation = (string)mDataReader["TypeLotDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ExportateurID"]))
                    {
                        mClass._Exportateur = new Exportateur();
                        mClass._Exportateur.ID = (int)mDataReader["ExportateurID"];
                        mClass._Exportateur.Nom = (string)mDataReader["ExportateurNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["MagasinID"]))
                    {
                        mClass._Magasin = new Magasin();
                        mClass._Magasin.Sites = new Site();
                        mClass._Magasin.ID = (int)mDataReader["MagasinID"];
                        mClass._Magasin.Designation = (string)mDataReader["MagasinDesignation"];
                        mClass._Magasin.Sites.ID = (int)mDataReader["SiteID"];
                        mClass._Magasin.Sites.Nom = (string)mDataReader["SiteNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["DateLot"])) mClass._DateLot = (DateTime)mDataReader["DateLot"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroLot"])) mClass._NumeroLot = (string)mDataReader["NumeroLot"];

                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass._NombreSacs = (int)mDataReader["NombreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["NombrePalette"])) mClass._NombrePalette = (int)mDataReader["NombrePalette"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalette = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNet = (decimal)mDataReader["PoidsNet"];
                    if (!DBNull.Value.Equals(mDataReader["EstQueue"])) mClass._EstQueue = (bool)mDataReader["EstQueue"];
                    if (!DBNull.Value.Equals(mDataReader["EstManuel"])) mClass._EstManuel = (bool)mDataReader["EstManuel"];
                    if (!DBNull.Value.Equals(mDataReader["EstReusine"])) mClass._EstReusine = (bool)mDataReader["EstReusine"];
                    if (!DBNull.Value.Equals(mDataReader["IsApproved"])) mClass._IsApproved = (bool)mDataReader["IsApproved"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];

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
                throw new Exception(ex.Message + "\nLot:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderReuisinage(Lot_GestionStock mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroLot"])) mClass._NumeroLot = (string)mDataReader["NumeroLot"];
                    if (!DBNull.Value.Equals(mDataReader["EstReusine"])) mClass._EstReusine = (bool)mDataReader["EstReusine"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nV2_Lot:MapFromDataReaderReuisinage");
            }
        }
        #endregion
    }

    public partial class Lot_GestionStockViewModel
    {
        public Lot_GestionStock _Lot_GestionStock { get; set; }

        public string _DefaultCampagne { get; set; }
        //public int _DefaultExportateur { get; set; }

        public decimal _PoidsStdBrutUnitaire { get; set; }
        public decimal _PoidsStdNetUnitaire { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
