using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Components.Data;

namespace Tms.Classes.Shared.stock
{
    public class LotType : DataPersist
    {
        #region "Fields"

        private int _ID;
        private string _Designation;
        private int _NombreSacs;
        private int _NombrePalette;
        private int _NombreSacsParPalettes;
        private decimal _PoidsStandard;
        private decimal _PoidsMin;
        private decimal _PoidsMax;
        private decimal _TareSacsUnitaire;
        private bool _Desactive;
        private string _Prefixe;
        #endregion

        #region Constructor
        public LotType()
        {

        }

        public LotType(int myId)
        {
            this.fnGet(myId);
        }

        #endregion

        #region Properties
        [ModelField(IDProperty = true)]
        public int ID
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

        public string Designation
        {
            get
            {
                return _Designation;
            }

            set
            {
                _Designation = value;
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

        public decimal PoidsStandard
        {
            get
            {
                return _PoidsStandard;
            }

            set
            {
                _PoidsStandard = value;
            }
        }

        public decimal PoidsBrut
        {
            get
            {
                return _PoidsStandard * _NombreSacs;
            }
        }

        public decimal TareSacs
        {
            get
            {
                return Math.Ceiling( _TareSacsUnitaire * _NombreSacs);
            }
        }

        public string PoidsBrutAsString
        {
            get { return PoidsBrut != 0 ? string.Format("{0:#,#}", PoidsBrut).TrimStart() : string.Empty; }
        }

        public decimal PoidsMin
        {
            get
            {
                return _PoidsMin;
            }

            set
            {
                _PoidsMin = value;
            }
        }

        public decimal PoidsMax
        {
            get
            {
                return _PoidsMax;
            }

            set
            {
                _PoidsMax = value;
            }
        }

        public int NombreSacsParPalettes
        {
            get
            {
                return _NombreSacsParPalettes;
            }

            set
            {
                _NombreSacsParPalettes = value;
            }
        }

        public decimal TareSacsUnitaire
        {
            get
            {
                return _TareSacsUnitaire;
            }

            set
            {
                _TareSacsUnitaire = value;
            }
        }

        public int mIcon
        {
            get
            {
                if (_Desactive)
                    return 0;
                else return 2;
            }
        }

        public string Prefixe
        {
            get
            {
                return _Prefixe;
            }

            set
            {
                _Prefixe = value;
            }
        }
        #endregion

        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_LotType_Activate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
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
                throw new Exception(ex.Message + "\r\n" + "LotType:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_LotType_DeActivate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
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
                        Desactive = true;
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
                throw new Exception(ex.Message + "\r\n" + "LotType:fnDeActivate");
            }
            return Result;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_LotType_Get", (int)Id);
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
            return fnSelect(-1);
        }

        public List<DataPersist> fnSelect(int mStatus)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_LotType_Select");
                db().AddInParameter(mCommande, "@Status", SqlDbType.SmallInt, mStatus);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    LotType mClass = new LotType();

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

                    mCommande = db().CreateStoredProcCommand("V2_LotType_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.Int, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_LotType_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@designation", SqlDbType.VarChar, _Designation);
                db().AddInParameter(mCommande, "@nombresacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@nombrepalette", SqlDbType.Int, _NombrePalette);
                db().AddInParameter(mCommande, "@NombreSacsParPalettes", SqlDbType.Int, _NombreSacsParPalettes);
                db().AddInParameter(mCommande, "@poidsstandard", SqlDbType.Decimal, _PoidsStandard);
                db().AddInParameter(mCommande, "@poidsmin", SqlDbType.Decimal, _PoidsMin);
                db().AddInParameter(mCommande, "@poidsmax", SqlDbType.Decimal, _PoidsMax);
                db().AddInParameter(mCommande, "@TareSacsUnitaire", SqlDbType.Decimal, _TareSacsUnitaire);
                db().AddInParameter(mCommande, "@prefixe", SqlDbType.Char,1, Prefixe);

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
                        _ID = (int)db().Parameters(mCommande, "@ID");

                        _isnew = false;
                        break;
                    default:
                        //Unkown error
                        Result = false;
                        string ErrorMessage = (string)db().Parameters(mCommande, "@ErrorMessage");
                        throw new Exception(ErrorMessage);
                        break;
                }
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + "\r\n" + "LotType:fnUpdate");

            }
            return Result;
        }


        public override string ToString()
        {
            throw new NotImplementedException();
        }

        private static void MapFromDataReader(LotType mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (int)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Designation"])) mClass._Designation = (string)mDataReader["Designation"];                    
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass._NombreSacs = (int)mDataReader["NombreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["NombrePalettes"])) mClass._NombrePalette = (int)mDataReader["NombrePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["NombreSacsParPalettes"])) mClass._NombreSacsParPalettes = (int)mDataReader["NombreSacsParPalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsStandard"])) mClass._PoidsStandard = (decimal)mDataReader["PoidsStandard"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsMin"])) mClass._PoidsMin = (decimal)mDataReader["PoidsMin"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsMax"])) mClass._PoidsMax = (decimal)mDataReader["PoidsMax"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacsUnitaire"])) mClass._TareSacsUnitaire = (decimal)mDataReader["TareSacsUnitaire"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Prefixe"])) mClass._Prefixe = (string)mDataReader["Prefixe"];
                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n LotType:MapFromDataReader");
            }
        }

    }

    public partial class LotTypeViewModel
    {
        public LotType _LotType { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
