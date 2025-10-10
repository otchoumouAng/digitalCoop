using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Components.Data;

namespace Tms.Classes.Business.Sales
{
    public class FactureCommercialePrefinancement : DataPersist
    {
        #region "Fields"
        private Guid _ID;
        private FactureCommerciale _FactureCommerciale;
        private Prefinancement _Prefinancement;
        private decimal _Taux;
        private decimal _Montant;
        private decimal _MontantCFA;
        private string _TypeString;
        
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

        public Prefinancement Prefinancement
        {
            get { return _Prefinancement; }
            set { _Prefinancement = value; }
        }

        public string PrefinancementAsString
        {
            get
            {
                if (_Prefinancement != null)
                    return _Prefinancement.Numero;
                else
                    return string.Empty;
            }

        }

        public string TypeAsString
        {
            get { return _TypeString; }
            set { _TypeString = value; }
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
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_FactureCommercialePrefinancement_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "V2_FactureCommercialePrefinancement:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_FactureCommercialePrefinancement_DeActivate");
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
                        break;
                }
            }
            catch (Exception ex)
            {
                bolResult = false;
                throw new Exception(ex.Message + "\r\n" + "V2_FactureCommercialePrefinancement:fnDeActivate");
            }
            return bolResult;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_FactureCommercialePrefinancement_Get", (Guid)Id);
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
                DataCommand mCommande = db().CreateStoredProcCommand("V2_FactureCommercialePrefinancement_Select");
                db().AddInParameter(mCommande, "@FactureID", SqlDbType.UniqueIdentifier, factureID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FactureCommercialePrefinancement mClass = new FactureCommercialePrefinancement();
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

                    mCommande = db().CreateStoredProcCommand("V2_FactureCommercialePrefinancement_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                    db().AddInParameter(mCommande, "@FactureCommercialeID", SqlDbType.UniqueIdentifier, _FactureCommerciale.ID);

                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_FactureCommercialePrefinancement_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@PrefinancementID", SqlDbType.UniqueIdentifier, _Prefinancement.ID);



                db().AddInParameter(mCommande, "@Taux", SqlDbType.Decimal, _Taux);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@MontantCFA", SqlDbType.Money, _MontantCFA);


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
                throw new Exception(ex.Message + "\r\n" + "V2_FactureCommercialePrefinancement:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("V2_FactureCommercialePrefinancement_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                    db().AddInParameter(mCommande, "@FactureCommercialeID", SqlDbType.UniqueIdentifier, _FactureCommerciale.ID);

                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_FactureCommercialePrefinancement_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@PrefinancementID", SqlDbType.UniqueIdentifier, _Prefinancement.ID);



                db().AddInParameter(mCommande, "@Taux", SqlDbType.Decimal, _Taux);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@MontantCFA", SqlDbType.Money, _MontantCFA);

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
                throw new Exception(ex.Message + "\r\n" + "V2_FactureCommercialePrefinancement:fnUpdateTransaction");

            }
            return Result;
        }
        public bool fnRemove()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_FactureCommercialePrefinancement_Remove");
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
                throw new Exception(ex.Message + "\r\n" + "FactureCommercialePrefinancement:fnRemove");
            }
            return Result;
        }
        #endregion
        #region "Private Members"
        public override string ToString()
        {
            throw new NotImplementedException();
        }
        private static void MapFromDataReader(FactureCommercialePrefinancement mClass, IDataReader mDataReader)
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

                    if (!DBNull.Value.Equals(mDataReader["PrefinancementID"]))
                    {
                        mClass._Prefinancement = new Prefinancement();
                        mClass._Prefinancement.ID = (Guid)mDataReader["PrefinancementID"];
                        mClass._Prefinancement.Numero = (string)mDataReader["PrefinancementNumero"];
                        mClass._Prefinancement.PrefinancementType = new Shared.PrefinancementType();
                        mClass._Prefinancement.PrefinancementType.ID  = (int)mDataReader["TypeID"];
                        mClass._Prefinancement.PrefinancementType.Designation  = (string)mDataReader["TypeDesignation"];
                        mClass._TypeString = (string)mDataReader["TypeDesignation"];
                    }

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
                throw new Exception(ex.Message + "\nV2_FactureCommercialePrefinancement:MapFromDataReader");
            }
        }
        #endregion
    }

    public partial class FactureCommercialePrefinancementViewModel
    {
        public FactureCommercialePrefinancement _FactureCommercialePrefinancement { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
