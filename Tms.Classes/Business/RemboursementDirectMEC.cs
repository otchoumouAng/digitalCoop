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
    public class RemboursementDirectMEC : DataPersist
    {
        #region "Fields"

        private Guid _ID;
        private RemboursementDirect _ReboursementDirect;
        private MiseEnCompte _MiseEnCompte;
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

        public MiseEnCompte MiseEnCompte
        {
            get { return _MiseEnCompte; }
            set { _MiseEnCompte = value; }
        }

        public DateTime SavingDate
        {
            get { return _MiseEnCompte.DateMec; }
        }
       
        public string SavingType
        {
            get { return _MiseEnCompte.MiseEnCompteType.Designation; }
        }

        public decimal SavingAmount
        {
            get { return _MiseEnCompte.Montant; }
        }

        public string SavingAmountAsString
        {
            get { return _MiseEnCompte.Montant != 0 ? String.Format("{0:#,#}", _MiseEnCompte.Montant).TrimStart() : string.Empty; }
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
            get { return string.Empty; }
        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                return 3; //                     
            }
        }

        #endregion

        #region Constructor
        public RemboursementDirectMEC()
        {

        }

        public RemboursementDirectMEC(Guid myID)
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
                mDataReader = db().ExecuteReader("RemboursementDirect_Mec_Get", (Guid)Id);
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
                DataCommand mCommande = db().CreateStoredProcCommand("RemboursementDirect_Mec_Select");
                db().AddInParameter(mCommande, "@RemboursementDirectID", SqlDbType.UniqueIdentifier, RemboursementDirectID);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    RemboursementDirectMEC mClass = new RemboursementDirectMEC();
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
                DataCommand mCommande = db().CreateStoredProcCommand("RemboursementDirect_Mec_SelectAvailable");
                db().AddInParameter(mCommande, "@SupplierID", SqlDbType.Int, SupplierID);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    RemboursementDirectMEC mClass = new RemboursementDirectMEC();
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
                DataCommand mCommande = db().CreateStoredProcCommand("RemboursementDirect_Mec_Clear");
                db().AddInParameter(mCommande, "@SupplierID", SqlDbType.Int, SupplierID);
                db().AddInParameter(mCommande, "@MontantRembourse", SqlDbType.Decimal, Montant);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    RemboursementDirectMEC mClass = new RemboursementDirectMEC();
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
                mCommande = db().CreateStoredProcCommand("RemboursementDirect_Mec_New");
                db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddInParameter(mCommande, "@RemboursementDirectID", SqlDbType.UniqueIdentifier, _ReboursementDirect.ID);
                db().AddInParameter(mCommande, "@MecID", SqlDbType.UniqueIdentifier, _MiseEnCompte.ID);
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
                throw new Exception(ex.Message + "\r\n" + "RemboursementDirectMec:fnUpdate");

            }
            return Result;

        }

        public bool fnUpdate(DataTransaction mTran)
        {

            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("RemboursementDirect_Mec_New");
                db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddInParameter(mCommande, "@RemboursementDirectID", SqlDbType.UniqueIdentifier, _ReboursementDirect.ID);
                db().AddInParameter(mCommande, "@MecID", SqlDbType.UniqueIdentifier, _MiseEnCompte.ID);
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
                throw new Exception(ex.Message + "\r\n" + "RemboursementDirectMec:fnUpdate");

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

        private static void MapFromDataReader(RemboursementDirectMEC mClass, IDataReader mDataReader)
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

                    if (!DBNull.Value.Equals(mDataReader["MiseEnCompteID"]))
                    {
                        MiseEnCompte mMec = new MiseEnCompte();
                        mMec.ID = (Guid)mDataReader["MiseEnCompteID"];
                        mMec.DateMec = (DateTime)mDataReader["DateMec"];
                        mMec.Montant = (Decimal)mDataReader["Montant"];
                        MiseEnCompteType mType = new MiseEnCompteType();
                        mType.ID = (int)mDataReader["TypeID"];
                        mType.Designation = (string)mDataReader["TypeNom"];
                        mMec.MiseEnCompteType = mType;                        
                        mMec.Fournisseur = mFournisseur;
                        mClass._MiseEnCompte = mMec;
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
                throw new Exception(ex.Message + "\nlbc_RemboursementDirectMec:MapFromDataReader");
            }
        }
        #endregion


    }
}
