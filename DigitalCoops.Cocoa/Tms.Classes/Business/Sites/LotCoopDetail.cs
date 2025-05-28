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

namespace Tms.Classes.Business.Sites
{
    public class LotCoopDetail : DataPersist
    {
        #region fields
        private Guid _ID;
        private LotCoop _LotCoop;
        private BonDeLivraison _BonDeLivraison;
        private DateTime _DateLotDetail;
        private int _NombreSacs;
        private decimal _PoidsBrut;
        private decimal _TareSacs;
        private decimal _TarePalette;
        private decimal _PoidsNet;
        private bool _Desactive;        

        private decimal _Montant;
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

        public LotCoop LotCoop
        {
            get
            {
                return _LotCoop;
            }

            set
            {
                _LotCoop = value;
            }
        }

        public BonDeLivraison BonDeLivraison
        {
            get
            {
                return _BonDeLivraison;
            }

            set
            {
                _BonDeLivraison = value;
            }
        }                             

        public DateTime DateLotDetail
        {
            get
            {
                return _DateLotDetail;
            }

            set
            {
                _DateLotDetail = value;
            }
        }

        public string DateLotDetailAsString
        {
            get { return _DateLotDetail != null ? _DateLotDetail.ToShortDateString() : string.Empty; }
        }

        public string DateLotDetailLongAsString
        {
            get { return (_DateLotDetail != null) ? _DateLotDetail.ToString("G") : string.Empty; }            
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
            get
            {
                return _NombreSacs != 0 ? string.Format("{0:#,#}", _NombreSacs).TrimStart() : "0";
            }
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
            get
            {
                return _PoidsBrut != 0 ? string.Format("{0:#,#}", _PoidsBrut).TrimStart() : "0";
            }
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
            get
            {
                return _TareSacs != 0 ? string.Format("{0:#,#}", _TareSacs).TrimStart() : "0";
            }
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
            get
            {
                return _TarePalette != 0 ? string.Format("{0:#,#}", _TarePalette).TrimStart() : "0";
            }
        }

        public string PoidsNetEvalAsString
        {
            get
            {
                return _PoidsNet != 0 ? string.Format("{0:#,#}", Math.Round((_PoidsNet * 8) / 100), 0).TrimStart() : "0";
            }
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
            get
            {
                return _PoidsNet != 0 ? string.Format("{0:#,#}", _PoidsNet).TrimStart() : "0";
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

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desactive)
                    return 0;                                  
                else
                    return 2; // 
            }
        }                        

        public string NumeroTransfert
        {
            get
            {
                return _LotCoop != null ? _LotCoop.Numero : string.Empty;
            }            
        }

        public string NumeroBonDeLivraison
        {
            get
            {
                return _BonDeLivraison != null ? _BonDeLivraison.Numero : string.Empty;
            }
        }

        public string NumeroLivraison
        {
            get
            {
                return _BonDeLivraison != null && _BonDeLivraison.Livraison != null ? _BonDeLivraison.Livraison.Numero : string.Empty;
            }
        }        

        public decimal Montant
        {
            get
            {
                return _Montant;
            }

            set
            {
                _Montant = value;
            }
        }
        #endregion

        #region Constructor
        public LotCoopDetail()
        {

        }

        public LotCoopDetail(Guid myId)
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
            DataCommand mCommande = db().CreateStoredProcCommand("V4_LotDetail_DeActivate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
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
                throw new Exception(ex.Message + "\r\n" + "LotCoopDetail:fnDeActivate");
            }
            return bolResult;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V4_LotDetail_GET", (Guid)Id);
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
            return fnSelect(Guid.Empty);
        }

        public virtual List<DataPersist> fnSelect(Guid LotID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V4_LotDetail_Select");

                db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, LotID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    LotCoopDetail mClass = new LotCoopDetail();

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
                    mCommande = db().CreateStoredProcCommand("V4_LotDetail_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V4_LotDetail_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, _LotCoop.ID);
                db().AddInParameter(mCommande, "@bonDeLivraisonID", SqlDbType.UniqueIdentifier, _BonDeLivraison.ID);
                db().AddInParameter(mCommande, "@DateLot", SqlDbType.DateTime, _DateLotDetail);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, _TarePalette);
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
                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        ID = (Guid)db().Parameters(mCommande, "@ID");                        
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
                throw new Exception(ex.Message + "\r\n" + "LotCoopDetail:fnUpdate");

            }
            return Result;
        }

        public bool fnUpdate(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("V4_LotDetail_New");
                db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);

                db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, _LotCoop.ID);
                db().AddInParameter(mCommande, "@bonDeLivraisonID", SqlDbType.UniqueIdentifier, _BonDeLivraison.ID);
                db().AddInParameter(mCommande, "@DateLot", SqlDbType.DateTime, _DateLotDetail);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, _TarePalette);
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
                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        ID = (Guid)db().Parameters(mCommande, "@ID");
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
                throw new Exception(ex.Message + "\r\n" + "LotCoopDetail:fnUpdate");

            }
            return Result;
        }


        public bool fnRemove()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V4_LotDetail_Remove");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
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
                throw new Exception(ex.Message + "\r\n" + "LotCoopDetail:fnRemove");
            }
            return Result;
        }


        private static void MapFromDataReader(LotCoopDetail mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];                    

                    if (!DBNull.Value.Equals(mDataReader["LotID"]))
                    {
                        mClass._LotCoop = new LotCoop();
                        mClass._LotCoop.ID = (Guid)mDataReader["LotID"];
                        mClass._LotCoop.Numero = (string)mDataReader["LotNumero"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["BonDeLivraisonID"]))
                    {
                        mClass._BonDeLivraison = new BonDeLivraison();
                        mClass._BonDeLivraison.ID = (Guid)mDataReader["BonDeLivraisonID"];
                        mClass._BonDeLivraison.Numero = (string)mDataReader["BonDeLivraisonNumero"];
                        mClass._BonDeLivraison.Livraison = new Livraison();
                        mClass._BonDeLivraison.Livraison.Numero = (string)mDataReader["NumeroLivraison"];
                    }                                        
                    
                    if (!DBNull.Value.Equals(mDataReader["DateLot"])) mClass._DateLotDetail = (DateTime)mDataReader["DateLot"];                    
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass._NombreSacs = (int)mDataReader["NombreSacs"];
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

                    //mClass._Montant = mClass._PoidsBrut * mClass._PrixMoyen;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n LotCoopDetail:MapFromDataReader");
            }
        }


        public override string ToString()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public partial class LotCoopDetailViewModel
    {
        public LotCoopDetail _LotCoopDetail { get; set; }

        public string _DefaultCampagne { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }


}
