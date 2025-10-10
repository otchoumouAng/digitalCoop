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
    public class MouvementStockType : DataPersist
    {
        #region "Fields"

        private int _ID;
        private string _Designation;
        private StockType _StockType;
        private int _Sens;
        private bool _EstAuto;
        private bool _Desactive;
        private bool _VisibleEnAgence;

        #endregion

        #region "Properties"
        [ModelField(IDProperty = true)]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }

        public StockType StockType
        {
            get { return _StockType; }
            set { _StockType = value; }
        }

        public int Sens
        {
            get { return _Sens; }
            set { _Sens = value; }
        }

        public bool EstAuto
        {
            get { return _EstAuto; }
            set { _EstAuto = value; }
        }

        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
        }

        [Column(Text = "AsString")]
        public string AsString
        {
            get { return _Designation; }
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

        public string StockTypeAsString
        {
            get
            {
                return _StockType != null ? _StockType.Designation : string.Empty;
            }
        }

        public string SensAsString
        {
            get
            {
                if (_Sens > 0)
                    return "Input";
                else if (_Sens == 0)
                    return "";
                else
                    return "OutPut";                        
            }
        }

        public bool VisibleEnAgence
        {
            get
            {
                return _VisibleEnAgence;
            }

            set
            {
                _VisibleEnAgence = value;
            }
        }
        #endregion

        #region Constructor
        public MouvementStockType()
        {

        }

        public MouvementStockType(int myId)
        {
            this.fnGet(myId);
        }

        #endregion

        #region Methods
        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_MouvementStockType_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "MouvementStockType:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_MouvementStockType_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "MouvementStockType:fnDeActivate");
            }
            return Result;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_MouvementStockType_Get", (int)Id);
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
            return fnSelect(-1, -2, null, -1);
        }

        public List<DataPersist> fnSelect(int mStatus, int sens = -2, bool? saisiemanuelle = null, int MouvementStockTypeID = -1, int MagasinID = -1, int EstVisibleSurSite = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_MouvementStockType_Select");
                db().AddInParameter(mCommande, "@Status", SqlDbType.SmallInt, mStatus);
                db().AddInParameter(mCommande, "@sens", SqlDbType.SmallInt, sens);
                db().AddInParameter(mCommande, "@saisieManuelle", SqlDbType.Bit, saisiemanuelle);
                db().AddInParameter(mCommande, "@magasinID", SqlDbType.SmallInt, MagasinID);
                db().AddInParameter(mCommande, "@EstVisibleSurSite", SqlDbType.SmallInt, EstVisibleSurSite);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    MouvementStockType mClass = new MouvementStockType();

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

                    mCommande = db().CreateStoredProcCommand("V2_MouvementStockType_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.Int, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_MouvementStockType_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@designation", SqlDbType.VarChar, _Designation);
                db().AddInParameter(mCommande, "@stockTypeID", SqlDbType.Int, _StockType.ID);
                db().AddInParameter(mCommande, "@sens", SqlDbType.Int, _Sens);
                db().AddInParameter(mCommande, "@estAuto", SqlDbType.Bit, _EstAuto);                
                db().AddInParameter(mCommande, "@VisibleEnAgence", SqlDbType.Bit, _VisibleEnAgence);

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
                throw new Exception(ex.Message + "\r\n" + "MouvementStockType:fnUpdate");

            }
            return Result;
        }


        public override string ToString()
        {
            return _Designation;
        }

        private static void MapFromDataReader(MouvementStockType mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (int)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Designation"])) mClass._Designation = (string)mDataReader["Designation"];
                    mClass._StockType = new StockType();
                    if (!DBNull.Value.Equals(mDataReader["StockTypeID"])) mClass._StockType.ID = (int)mDataReader["StockTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["StockTypeDesignation"])) mClass._StockType.Designation = (string)mDataReader["StockTypeDesignation"];
                    if (!DBNull.Value.Equals(mDataReader["Sens"])) mClass._Sens = (Int16)mDataReader["Sens"];
                    if (!DBNull.Value.Equals(mDataReader["EstAuto"])) mClass._EstAuto = (bool)mDataReader["EstAuto"];
                    
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                    if (!DBNull.Value.Equals(mDataReader["VisibleEnAgence"])) mClass._VisibleEnAgence = (bool)mDataReader["VisibleEnAgence"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n MouvementStockType:MapFromDataReader");
            }
        }

        #endregion
    }

    public partial class MouvementStockTypeViewModel
    {
        public MouvementStockType _MouvementStockType { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
