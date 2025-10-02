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
  
    public class FactureCommercialeDeduction : DataPersist
    {
        #region "Fields"
        private Guid _ID;
        private FactureCommerciale _FactureCommerciale;
        private FactureCommercialeDeductionType _FactureCommercialeDeductionType;
        private decimal _Taux;
        private decimal _Montant;
        private decimal _MontantCFA;
        private bool _IsAuto;
        private bool _Desactive;
        #endregion
        #region "Properties"
        [ModelField(IDProperty = true, SortType = Ext.Net.SortTypeMethod.None, SortDir = Ext.Net.SortDirection.ASC)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public FactureCommerciale FactureCommerciale
        {
            get { return _FactureCommerciale; }
            set { _FactureCommerciale = value; }
        }

        public string FactureCommercialeAsString
        {
            get
            {
                if (_FactureCommerciale != null)
                    return _FactureCommerciale.Numero;
                else
                    return string.Empty;
            }

        }

        public FactureCommercialeDeductionType FactureCommercialeDeductionType
        {
            get { return _FactureCommercialeDeductionType; }
            set { _FactureCommercialeDeductionType = value; }
        }

        public string FactureCommercialeDeductionTypeAsString
        {
            get
            {
                if (_FactureCommercialeDeductionType != null)
                    return _FactureCommercialeDeductionType.Designation;
                else
                    return string.Empty;
            }

        }

        public decimal Taux
        {
            get { return _Taux; }
            set { _Taux = value; }
        }

        public string TauxAsString
        {
            get { return _Taux != 0 ? string.Format("{0:#,#}", _Taux).TrimStart() : string.Empty; }
        }

        public decimal Montant
        {
            get { return _Montant; }
            set { _Montant = value; }
        }

        public string MontantAsString
        {
            get { return _Montant != 0 ? String.Format("{0:#,##0.###}", _Montant).TrimStart() : string.Empty; }
        }


        public decimal MontantCFA
        {
            get { return _MontantCFA; }
            set { _MontantCFA = value; }
        }

        public string MontantCFAAsString
        {
            get { return _MontantCFA != 0 ? String.Format("{0:#,#}", _MontantCFA).TrimStart() : string.Empty; }
        }

        public bool IsAuto
        {
            get { return _IsAuto; }
            set { _IsAuto = value; }
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
                mDataReader = db().ExecuteReader("V2_FactureCommercialeDeduction_Get", (Guid)Id);
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
            Guid factureID = Guid.Empty;
            return fnSelect(factureID);
        }

        public List<DataPersist> fnSelect(object factureID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_FactureCommercialeDeduction_Select");
                db().AddInParameter(mCommande, "@FactureID", SqlDbType.UniqueIdentifier, factureID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FactureCommercialeDeduction mClass = new FactureCommercialeDeduction();
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

                    mCommande = db().CreateStoredProcCommand("V2_FactureCommercialeDeduction_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                    db().AddInParameter(mCommande, "@FactureCommercialeID", SqlDbType.UniqueIdentifier, _FactureCommerciale.ID);

                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_FactureCommercialeDeduction_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@DeductionTypeID", SqlDbType.Int, _FactureCommercialeDeductionType.ID);



                db().AddInParameter(mCommande, "@Taux", SqlDbType.Decimal, _Taux);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@MontantCFA", SqlDbType.Money, _MontantCFA);
                db().AddInParameter(mCommande, "@AutoDeduction", SqlDbType.Bit, _IsAuto);

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
                throw new Exception(ex.Message + "\r\n" + "V2_FactureCommercialeDeduction:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("V2_FactureCommercialeDeduction_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                    db().AddInParameter(mCommande, "@FactureCommercialeID", SqlDbType.UniqueIdentifier, _FactureCommerciale.ID);

                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_FactureCommercialeDeduction_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@DeductionTypeID", SqlDbType.Int, _FactureCommercialeDeductionType.ID);



                db().AddInParameter(mCommande, "@Taux", SqlDbType.Decimal, _Taux);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@MontantCFA", SqlDbType.Money, _MontantCFA);
                db().AddInParameter(mCommande, "@AutoDeduction", SqlDbType.Bit, _IsAuto);
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
                throw new Exception(ex.Message + "\r\n" + "V2_FactureCommercialeDeduction:fnUpdateTransaction");

            }
            return Result;
        }

        #endregion
        #region "Private Members"
        public override string ToString()
        {
            throw new NotImplementedException();
        }
        private static void MapFromDataReader(FactureCommercialeDeduction mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["FactureCommercialeID"]))
                    {
                        mClass._FactureCommerciale = new FactureCommerciale();
                        mClass._FactureCommerciale.ID = (Guid)mDataReader["FactureCommercialeID"];
                        mClass._FactureCommerciale.Numero = (string)mDataReader["FactureCommercialeNumero"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Taux"])) mClass._Taux = (decimal)mDataReader["Taux"];
                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._Montant = (decimal)mDataReader["Montant"];
                    if (!DBNull.Value.Equals(mDataReader["MontantCFA"])) mClass._MontantCFA = (decimal)mDataReader["MontantCFA"];

                    if (!DBNull.Value.Equals(mDataReader["DeductionTypeID"]))
                    {
                        mClass._FactureCommercialeDeductionType = new FactureCommercialeDeductionType();
                        mClass._FactureCommercialeDeductionType.ID = (int)mDataReader["DeductionTypeID"];
                        mClass._FactureCommercialeDeductionType.Designation = (string)mDataReader["DeductionTypeDesignation"];
                    }
                    if (!DBNull.Value.Equals(mDataReader["IsAuto"])) mClass._IsAuto = (bool)mDataReader["IsAuto"];

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
                throw new Exception(ex.Message + "\nV2_FactureCommercialeDeduction:MapFromDataReader");
            }
        }
        #endregion
    }

    public partial class FactureCommercialeDeductionViewModel
    {
        public FactureCommercialeDeduction _FactureCommercialeDeduction { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
