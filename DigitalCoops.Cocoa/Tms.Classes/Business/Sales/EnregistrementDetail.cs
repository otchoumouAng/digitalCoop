using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Components.Data;

namespace Tms.Classes.Business.Sales
{
    public class EnregistrementDetail : DataPersist
    {
        #region "Fields"
        private Guid _ID;
        private Enregistrement _Enregistrement;        
        private string _Numero;
        private DateTime _Date;                
        private decimal _Tonnage;
        private decimal _Prix;
        private decimal _Montant;
        private bool _Desactive;      
        #endregion

        #region "Properties"
        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        
        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }       

        public decimal Tonnage
        {
            get { return _Tonnage; }
            set { _Tonnage = value; }
        }

        public string TonnageAsString
        {
            get { return _Tonnage != 0 ? String.Format("{0:#,#}", _Tonnage).TrimStart() : string.Empty; }
        }

        public decimal Prix
        {
            get { return _Prix; }
            set { _Prix = value; }
        }

        public string PrixAsString
        {
            get { return _Prix != 0 ? String.Format("{0:#,##0.##}", _Prix).TrimStart() : string.Empty; }
        }

        public decimal Montant
        {
            get { return _Montant; }
            set { _Montant = value; }
        }

        public string MontantAsString
        {
            get { return _Montant != 0 ? String.Format("{0:#,#}", _Montant).TrimStart() : string.Empty; }
        }
        

        //public string NumeroCode
        //{
        //    get { return _Numero + " (N°" + _NumeroONCC + " - "+ _ContratExport +")"; }
        //    set { _NumeroCode = value; }
        //}                    

        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
        }        
       

        [Column(Ignore = true)]
        public string AsString
        {
            get { return _Numero; }
        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desactive)
                    return 0; // 
                else
                    return 2; //                     
            }
        }

        public Enregistrement Enregistrement
        {
            get
            {
                return _Enregistrement;
            }

            set
            {
                _Enregistrement = value;
            }
        }

        #endregion

        #region Constructor
        public EnregistrementDetail()
        {

        }

        public EnregistrementDetail(Guid myID)
        {
            this.fnGet(myID);
        }
        #endregion

        #region "Methods"
        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_EnregistrementDetail_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "V2_EnregistrementDetail:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_EnregistrementDetail_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "V2_EnregistrementDetail:fnDeActivate");
            }
            return Result;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_EnregistrementDetail_Get", (Guid)Id);
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
        public bool fnGetByNumber(string Number)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_EnregistrementDetail_GetByNumber", (string)Number);
                if (mDataReader.Read())
                {
                    MapFromDataReader(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetByNumber");
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
        public List<DataPersist> fnSelect(Guid EnregistrementID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_EnregistrementDetail_Select");
                db().AddInParameter(mCommande, "@IDEnregistrement", SqlDbType.UniqueIdentifier, EnregistrementID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    EnregistrementDetail mClass = new EnregistrementDetail();
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


        public List<DataPersist> fnSelectForShipment(string Campagne, int ExportateurID, DateTime? StartDate, DateTime? EndDate, int IsDisabled)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_EnregistrementDetail_SelectForShipment");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@IsDisabled", SqlDbType.Int, IsDisabled);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    EnregistrementDetail mClass = new EnregistrementDetail();
                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectForShipment");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public bool fnUpdate(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {

                    mCommande = db().CreateStoredProcCommand("V2_EnregistrementDetail_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);                    
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_EnregistrementDetail_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@Numero", SqlDbType.VarChar, _Numero);
                db().AddInParameter(mCommande, "@EnregistrementID", SqlDbType.UniqueIdentifier, _Enregistrement.ID);                
                
                db().AddInParameter(mCommande, "@Tonnage", SqlDbType.Decimal,( _Tonnage*100));
                db().AddInParameter(mCommande, "@Prix", SqlDbType.Money, _Prix);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
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
                db().ExecuteNonQuery(ref mCommande,mTran);
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
                        break;
                }
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + "\r\n" + "V2_EnregistrementDetail:fnUpdate");

            }
            return Result;
        }

        public override bool fnUpdate()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {

                    mCommande = db().CreateStoredProcCommand("V2_EnregistrementDetail_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_EnregistrementDetail_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 20);
                db().AddInParameter(mCommande, "@EnregistrementID", SqlDbType.UniqueIdentifier, _Enregistrement.ID);

                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _Date);
                db().AddInParameter(mCommande, "@Tonnage", SqlDbType.Decimal, (_Tonnage * 100));
                db().AddInParameter(mCommande, "@Prix", SqlDbType.Money, _Prix);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
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
                        break;
                }
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + "\r\n" + "V2_EnregistrementDetail:fnUpdate");

            }
            return Result;
        }

        public bool fnRemove()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_EnregistrementDetail_Remove");
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
                throw new Exception(ex.Message + "\r\n" + "V2_EnregistrementDetail:fnRemove");
            }
            return Result;
        }

        #endregion

        #region "Private Methods"
        public override string ToString()
        {
            throw new NotImplementedException();
        }

        private static void MapFromDataReader(EnregistrementDetail mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];                    
                    if (!DBNull.Value.Equals(mDataReader["EnregistrementID"]))
                    {
                        mClass._Enregistrement = new Enregistrement();
                        mClass._Enregistrement.ID = (Guid)mDataReader["EnregistrementID"];
                        mClass._Enregistrement.NumeroGuichet = (string)mDataReader["NumeroGuichet"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DateEnregistrementDetail"])) mClass._Date = (DateTime)mDataReader["DateEnregistrementDetail"];         

                    if (!DBNull.Value.Equals(mDataReader["Tonnage"])) mClass._Tonnage = (decimal)mDataReader["Tonnage"];
                    if (!DBNull.Value.Equals(mDataReader["Prix"])) mClass._Prix = (decimal)mDataReader["Prix"];
                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._Montant = (decimal)mDataReader["Montant"];
                    
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
                throw new Exception(ex.Message + "\nV2_EnregistrementDetail:MapFromDataReader");
            }
        }
        #endregion
    }
    public partial class EnregistrementDetailViewModel
    {
        public EnregistrementDetail _EnregistrementDetail { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
