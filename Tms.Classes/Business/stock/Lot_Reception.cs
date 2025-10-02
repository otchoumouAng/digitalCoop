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
    public class Lot_Reception : DataPersist
    {
        #region fields
        private Guid _ID;
        private Campagne _Campagne;
        private Magasin _Magasin;
        private Produit _Produit;
        private DateTime _DateEnvoi;
        private Lot_GestionStock _NumeroLot;
        private string _Immatriculation;
        private string _ImmTracteur;
        private int _NombreSacs;
        private int _NombrePalette;
        private decimal _PoidsBrut;
        private decimal _TareSacs;
        private decimal _TarePalette;
        private decimal _PoidsNet;
        private bool _Desactive;
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

        public DateTime DateEnvoi
        {
            get
            {
                return _DateEnvoi;
            }

            set
            {
                _DateEnvoi = value;
            }
        }

        public string DateEnvoiAsString
        {
            get
            {
                return _DateEnvoi != null ? _DateEnvoi.ToShortDateString() : string.Empty;
            }
        }

        public Lot_GestionStock NumeroLot
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
        public string NumeroLotAsString
        {
            get
            {
                return _NumeroLot != null ? _NumeroLot.NumeroLot : string.Empty;
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
        public string ImmTracteur
        {
            get
            {
                return _ImmTracteur;
            }

            set
            {
                _ImmTracteur = value;
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
                if (_Desactive)
                    return 0; // BulletCross
                else
                    return 2; //                     
            }
        }


        #endregion

        #region Constructor
        public Lot_Reception()
        {

        }

        public Lot_Reception(Guid myId)
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
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_Reception_DeActivate");
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

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_Lot_Reception_Get", (Guid)Id);
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
            return fnSelect("-1", -1, -1, null, null, "-1");
        }

        public List<DataPersist> fnSelect(string mCampagne, int ProduitID, int MagasinID, DateTime? StartDate, DateTime? EndDate, string statut = "-1")
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_Reception_Select");
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, mCampagne);
                db().AddInParameter(mCommande, "@ProduitID", SqlDbType.Int, ProduitID);
                db().AddInParameter(mCommande, "@MagasinID", SqlDbType.Int, MagasinID);
                db().AddInParameter(mCommande, "@dateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@dateFin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@statut", SqlDbType.Char, 2, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Lot_Reception mClass = new Lot_Reception();
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
                    mCommande = db().CreateStoredProcCommand("V2_Lot_Reception_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@NumeroLot", SqlDbType.Char, 9, _NumeroLot.NumeroLot);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_Lot_Reception_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@NumeroLot", SqlDbType.Char, 9, _NumeroLot.NumeroLot);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@ProduitID", SqlDbType.Int, _Produit.ID);


                if (_Magasin != null)
                    db().AddInParameter(mCommande, "@MagasinID", SqlDbType.Int, _Magasin.ID);
                else
                    db().AddInParameter(mCommande, "@MagasinID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@DateEnvoi", SqlDbType.DateTime, _DateEnvoi);

                db().AddInParameter(mCommande, "@Immatriculation", SqlDbType.VarChar, _Immatriculation);
                db().AddInParameter(mCommande, "@ImmTracteur", SqlDbType.VarChar, _ImmTracteur);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@NombrePalette", SqlDbType.Int, _NombrePalette);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, _TareSacs);
                db().AddInParameter(mCommande, "@TarePalettes", SqlDbType.Decimal, _TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNet);


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
                            _NumeroLot.NumeroLot = (string)db().Parameters(mCommande, "@NumeroLot");
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
                    mCommande = db().CreateStoredProcCommand("V2_Lot_Reception_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@NumeroLot", SqlDbType.Char, 9, _NumeroLot.NumeroLot);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_Lot_Reception_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@NumeroLot", SqlDbType.Char, 9, _NumeroLot.NumeroLot);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@ProduitID", SqlDbType.Int, _Produit.ID);


                if (_Magasin != null)
                    db().AddInParameter(mCommande, "@MagasinID", SqlDbType.Int, _Magasin.ID);
                else
                    db().AddInParameter(mCommande, "@MagasinID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@DateEnvoi", SqlDbType.DateTime, _DateEnvoi);

                db().AddInParameter(mCommande, "@Immatriculation", SqlDbType.VarChar, _Immatriculation);
                db().AddInParameter(mCommande, "@ImmTracteur", SqlDbType.VarChar, _ImmTracteur);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@NombrePalette", SqlDbType.Int, _NombrePalette);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, _TareSacs);
                db().AddInParameter(mCommande, "@TarePalettes", SqlDbType.Decimal, _TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNet);

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
                            _NumeroLot.NumeroLot = (string)db().Parameters(mCommande, "@NumeroLot");
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
            return _Immatriculation;
        }


        private static void MapFromDataReader(Lot_Reception mClass, IDataReader mDataReader)
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

                    if (!DBNull.Value.Equals(mDataReader["MagasinID"]))
                    {
                        mClass._Magasin = new Magasin();
                        mClass._Magasin.ID = (int)mDataReader["MagasinID"];
                        mClass._Magasin.Designation = (string)mDataReader["MagasinDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["DateLot"])) mClass._DateEnvoi = (DateTime)mDataReader["DateEnvoi"];

                    if (!DBNull.Value.Equals(mDataReader["NumeroLot"]))
                    {
                        mClass._NumeroLot = new Lot_GestionStock();
                        mClass._NumeroLot.NumeroLot = (string)mDataReader["NumeroLot"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Immatriculation"])) mClass._Immatriculation = (string)mDataReader["Immatriculation"];
                    if (!DBNull.Value.Equals(mDataReader["Tracteur"])) mClass._ImmTracteur = (string)mDataReader["Tracteur"];
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass._NombreSacs = (int)mDataReader["NombreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["NombrePalette"])) mClass._NombrePalette = (int)mDataReader["NombrePalette"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalette = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNet = (decimal)mDataReader["PoidsNet"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nLot:MapFromDataReader");
            }
        }


        #endregion
    }
    public partial class Lot_ReceptionViewModel
    {
        public Lot_Reception _Lot_Reception { get; set; }

        public string _DefaultCampagne { get; set; }

        public decimal _PoidsStdBrutUnitaire { get; set; }
        public decimal _PoidsStdNetUnitaire { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
