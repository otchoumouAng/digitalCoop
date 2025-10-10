using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Components.Data;

namespace Tms.Classes.Business
{
    public class RemboursementDirectFinancement : DataPersist
    {
        #region "Fields"

        private Guid _ID;
        private RemboursementDirect _ReboursementDirect;
        private Financement _Financement;
        private decimal _SoldeEnCours;
        private decimal _Montant;

        #endregion

        #region "Properties"

        [ModelField(IDProperty = true, SortType = Ext.Net.SortTypeMethod.None, SortDir = Ext.Net.SortDirection.ASC)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public RemboursementDirect ReboursementDirect
        {
            get { return _ReboursementDirect; }
            set { _ReboursementDirect = value; }
        }
        public Financement Financement
        {
            get { return _Financement; }
            set { _Financement = value; }
        }

        public DateTime FinancingDate
        {
            get { return _Financement.DateFinancement; }
        }

        public string FinancingNumero
        {
            get { return _Financement.Numero; }
        }

        public string FinancingType
        {
            get { return _Financement.FinancementType.Designation; }
        }

        public decimal FinancingAmount
        {
            get { return _Financement.Montant; }
        }

        public string FinancingAmountAsString
        {
            get { return _Financement.Montant != 0 ? String.Format("{0:#,#}", _Financement.Montant).TrimStart() : string.Empty; }
        }

        public decimal SoldeEnCours
        {
            get { return _SoldeEnCours; }
            set { _SoldeEnCours = value; }
        }

        public string SoldeEnCoursAsString
        {
            get { return _SoldeEnCours != 0 ? String.Format("{0:#,#}", _SoldeEnCours).TrimStart() : string.Empty; }
        }

        public decimal Montant
        {
            get { return _Montant; }
            set { _Montant = value; }
        }

        public string MontantAsString
        {
            get { return _Montant != 0 ? String.Format("{0:#,0}", _Montant).TrimStart() : string.Empty; }
        }

        [Column(Ignore = true)]
        public string AsString
        {
            get { return _Financement.Numero; }
        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                return 2; //                     
            }
        }

        #endregion

        #region Constructor
        public RemboursementDirectFinancement()
        {

        }

        public RemboursementDirectFinancement(Guid myID)
        {
            this.fnGet(myID);
        }
        #endregion

        #region Methods
        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("RemboursementDirect_Avance_Get", (Guid)Id);
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
            return fnSelect(new Guid());
        }

        public List<DataPersist> fnSelect(Guid RemboursementDirectID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("RemboursementDirect_Avance_Select");
                db().AddInParameter(mCommande, "@RemboursementDirectID", SqlDbType.UniqueIdentifier, RemboursementDirectID);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    RemboursementDirectFinancement mClass = new RemboursementDirectFinancement();
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
        public List<DataPersist> fnSelectAvailable(int SupplierID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("RemboursementDirect_Avance_SelectAvailable");
                db().AddInParameter(mCommande, "@SupplierID", SqlDbType.Int, SupplierID);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    RemboursementDirectFinancement mClass = new RemboursementDirectFinancement();
                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectAvailable");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnClear(int SupplierID, decimal Montant)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("RemboursementDirect_Avance_Clear");
                db().AddInParameter(mCommande, "@SupplierID", SqlDbType.Int, SupplierID);
                db().AddInParameter(mCommande, "@MontantRembourse", SqlDbType.Decimal, Montant);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    RemboursementDirectFinancement mClass = new RemboursementDirectFinancement();
                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnClear");
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
                mCommande = db().CreateStoredProcCommand("RemboursementDirect_Avance_New");
                db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddInParameter(mCommande, "@RemboursementDirectID", SqlDbType.UniqueIdentifier, _ReboursementDirect.ID);
                db().AddInParameter(mCommande, "@FinancementID", SqlDbType.UniqueIdentifier, _Financement.ID);
                db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                db().AddInParameter(mCommande, "@SoldeEnCours", SqlDbType.Decimal, _SoldeEnCours);
                db().AddInParameter(mCommande, "@Apurement", SqlDbType.Money, _Montant);

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
                throw new Exception(ex.Message + "\r\n" + "RemboursementDirectFinancement:fnUpdate");

            }
            return Result;

        }

        public bool fnUpdate(DataTransaction mTran)
        {

            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("RemboursementDirect_Avance_New");
                db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddInParameter(mCommande, "@RemboursementDirectID", SqlDbType.UniqueIdentifier, _ReboursementDirect.ID);
                db().AddInParameter(mCommande, "@FinancementID", SqlDbType.UniqueIdentifier, _Financement.ID);
                db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                db().AddInParameter(mCommande, "@SoldeEnCours", SqlDbType.Decimal, _SoldeEnCours);
                db().AddInParameter(mCommande, "@Apurement", SqlDbType.Money, _Montant);

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
                throw new Exception(ex.Message + "\r\n" + "RemboursementDirectFinancement:fnUpdate");

            }
            return Result;


        }

        public override bool fnActivate()
        {
            return false;
        }

        public override bool fnDeActivate()
        {
            return false;
        }

        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _ReboursementDirect.Numero;
        }

        private static void MapFromDataReader(RemboursementDirectFinancement mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    Fournisseur mFournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["FournisseurID"]))
                    {
                        mFournisseur.ID = (int)mDataReader["FournisseurID"];
                        mFournisseur.Nom = (string)mDataReader["FournisseurNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["RemboursementDirectID"]))
                    {
                        RemboursementDirect mRemb = new RemboursementDirect();
                        mRemb.ID = (Guid)mDataReader["RemboursementDirectID"];
                        mRemb.Numero = (string)mDataReader["RemboursementDirectNumero"];
                        mRemb.Fournisseur = mFournisseur;
                        mClass._ReboursementDirect = mRemb;
                    }

                    if (!DBNull.Value.Equals(mDataReader["FinancementID"]))
                    {
                        Financement mFinancement = new Financement();
                        mFinancement.ID = (Guid)mDataReader["FinancementID"];
                        mFinancement.Numero = (string)mDataReader["Numero"];
                        mFinancement.DateFinancement = (DateTime)mDataReader["DateFinancement"];
                        mFinancement.Montant = (Decimal)mDataReader["Montant"];
                        FinancementType mType = new FinancementType();
                        mType.ID = (int)mDataReader["TypeID"];
                        mType.Designation = (string)mDataReader["TypeNom"];
                        mFinancement.FinancementType = mType;                        
                        mFinancement.Fournisseur = mFournisseur;
                        mClass._Financement = mFinancement;
                    }
                                       
                    if (!DBNull.Value.Equals(mDataReader["SoldeEnCours"])) mClass._SoldeEnCours = (decimal)mDataReader["SoldeEnCours"];
                    if (!DBNull.Value.Equals(mDataReader["Apurement"])) mClass._Montant = (decimal)mDataReader["Apurement"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nlbc_RemboursementDirectFinancement:MapFromDataReader");
            }
        }
        #endregion

    }

    public partial class RemboursementDirectFinancementViewModel
    {
        public RemboursementDirectFinancement _RemboursementDirectFinancement { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }


}
