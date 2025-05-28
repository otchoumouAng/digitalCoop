using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared.Sales;
using Tms.Components.Data;

namespace Tms.Classes.Business.Sales
{
    public class RedevanceValeur : DataPersist
    {
        #region "Fields"
        private Guid _ID;
        private Redevance _Redevance;
        private RedevanceType _RedevanceType;

        private decimal _Taux;
        private decimal _Montant;

        private bool _Auto;

        private bool _Desactive;
        #endregion

        #region "Properties"
        [ModelField(IDProperty = true, SortType = Ext.Net.SortTypeMethod.None, SortDir = Ext.Net.SortDirection.ASC)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Redevance Redevance
        {
            get { return _Redevance; }
            set { _Redevance = value; }
        }

        public string RedevanceAsString
        {
            get
            {
                if (_Redevance != null)
                    return _Redevance.Numero;
                else
                    return string.Empty;
            }

        }

        public RedevanceType RedevanceType
        {
            get { return _RedevanceType; }
            set { _RedevanceType = value; }
        }

        public string RedevanceTypeAsString
        {
            get
            {
                if (_RedevanceType != null)
                    return _RedevanceType.Designation;
                else
                    return string.Empty;
            }

        }

        public Decimal Taux
        {
            get { return _Taux; }
            set { _Taux = value; }
        }

        public string TauxAsString
        {
            get { return _Taux != 0 ? String.Format("{0:#,#}", _Taux).TrimStart() : string.Empty; }
        }

        public Decimal Montant
        {
            get { return _Montant; }
            set { _Montant = value; }
        }

        public string MontantAsString
        {
            get { return _Montant != 0 ? String.Format("{0:#,#}", _Montant).TrimStart() : string.Empty; }
        }

        public bool Auto
        {
            get { return _Auto; }
            set { _Auto = value; }
        }
        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
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

        #region "Methods"
        public override bool fnActivate()
        {
            throw new NotImplementedException();
        }

        public override bool fnDeActivate()
        {
            throw new NotImplementedException();
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_RedevanceValeur_Get", (Guid)Id);
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
            Guid redevanceID = Guid.Empty;
            return fnSelect(redevanceID);
        }

        public List<DataPersist> fnSelect(object RedevanceID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_RedevanceValeur_Select");
                db().AddInParameter(mCommande, "@RedevanceID", SqlDbType.UniqueIdentifier,RedevanceID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    RedevanceValeur mClass = new RedevanceValeur();
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

                    mCommande = db().CreateStoredProcCommand("V2_RedevanceValeur_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                    db().AddInParameter(mCommande, "@RedevanceID", SqlDbType.UniqueIdentifier, _Redevance.ID);

                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_RedevanceValeur_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@RedevanceType", SqlDbType.Int, _RedevanceType.ID);

               

                db().AddInParameter(mCommande, "@Taux", SqlDbType.Decimal, _Taux);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@AutoRedevance", SqlDbType.Bit, _Auto);

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
                throw new Exception(ex.Message + "\r\n" + "V2_RedevanceValeur:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("V2_RedevanceValeur_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                    db().AddInParameter(mCommande, "@RedevanceID", SqlDbType.UniqueIdentifier, _Redevance.ID);

                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_RedevanceValeur_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@RedevanceTypeID", SqlDbType.Int, _RedevanceType.ID);



                db().AddInParameter(mCommande, "@Taux", SqlDbType.Decimal, _Taux);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);

                db().AddInParameter(mCommande, "@AutoRedevance", SqlDbType.Bit, _Auto);


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
                throw new Exception(ex.Message + "\r\n" + "V2_RedevanceValeur:fnUpdateTransaction");

            }
            return Result;
        }

        #endregion

        #region "Private Members"
        public override string ToString()
        {
            throw new NotImplementedException();
        }
        private static void MapFromDataReader(RedevanceValeur mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    
                    if (!DBNull.Value.Equals(mDataReader["RedevanceID"]))
                    {
                        mClass._Redevance = new Redevance();
                        mClass._Redevance.ID = (Guid)mDataReader["RedevanceID"];
                        mClass._Redevance.Numero = (string)mDataReader["RedevanceNumero"];
                    }

                    
                    
                    if (!DBNull.Value.Equals(mDataReader["Taux"])) mClass._Taux = (Decimal)mDataReader["Taux"];
                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._Montant = (Decimal)mDataReader["Montant"];
                    
                    if (!DBNull.Value.Equals(mDataReader["RedevanceTypeID"]))
                    {
                        mClass._RedevanceType = new RedevanceType();
                        mClass._RedevanceType.ID = (int)mDataReader["RedevanceTypeID"];
                        mClass._RedevanceType.Designation = (string)mDataReader["RedevanceTypeDesignation"];
                    }
                    if (!DBNull.Value.Equals(mDataReader["IsAuto"])) mClass._Auto = (bool)mDataReader["IsAuto"];

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
                throw new Exception(ex.Message + "\nV2_RedevanceValeur:MapFromDataReader");
            }
        }
        #endregion
    }

    public partial class RedevanceValeurViewModel
    {
        public RedevanceValeur _RedevanceValeur { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
