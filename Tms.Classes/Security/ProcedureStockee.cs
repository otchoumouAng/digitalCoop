using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Components.Data;

namespace Tms.Classes.Security
{
    public class ProcedureStockee : DataPersist
    {

        public event evBindingProgressEventHandler evBindingProgress;
        public delegate void evBindingProgressEventHandler(int mProgress);

        #region "Fields"

        private Guid _ID;
        private string _Libelle = string.Empty;
        private string _Description = string.Empty;
        private DataSource _db = new DataSource();

        private int _Statut = 1;
        #endregion

        #region "Properties"

        public Guid IDProcedureStockee
        {
            get { return _ID; }
            set { _ID = value; }
        }

        [DataDisplayStyle(DefaultWidth = 200, Text = "Libelle", Visible = true)]
        public string Libelle
        {
            get { return _Libelle; }
            set { _Libelle = value; }
        }

        [DataDisplayStyle(DefaultWidth = 150, Text = "Description", Visible = true)]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public int Statut
        {
            get { return _Statut; }
            set { _Statut = value; }
        }

        [DataDisplayStyle(DefaultWidth = 80, Text = "Statut", Visible = false)]
        public string Statut_Description
        {
            get
            {
                string mReturn = string.Empty;
                switch (_Statut)
                {
                    case 1:
                        mReturn = "Actif";
                        break;
                    case 0:
                        mReturn = "Inactif";
                        break;
                    default:
                        mReturn = "Inconnu";
                        break;
                }
                return mReturn;
            }
        }
        #endregion

        #region "Members"

        public ProcedureStockee()
        {
            //
        }

        public ProcedureStockee(Guid mIDProcedureStockee)
        {
            try
            {
                fnGet(mIDProcedureStockee);
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message + Environment.NewLine + "ProcedureStockee:New"));
            }
        }

        //Retreive collection of object from db
        public override System.Collections.Generic.List<DataPersist> fnSelect()
        {
            //Send criteria for retreiving all db rows
            return fnSelect(-1);
        }

        //Retreive collection of object from db
        public System.Collections.Generic.List<DataPersist> fnSelect(int mCodeStatus)
        {
            List<DataPersist> mList = new List<DataPersist>();
            int mProgress = 0;
            int mRowCount = 0;
            try
            {
                //Dim mDataReader As IDataReader = _db.ExecuteReader("ac_Fournisseur_Lister", mCodeCategorie, mCodeStatus)

                IDataReader mDataReader;
                DataCommand mCommande = _db.CreateStoredProcCommand("se_ProcedureStockee_Lister");
                _db.AddInParameter(mCommande, "@Desactive", SqlDbType.Int, mCodeStatus);

                mDataReader = _db.ExecuteReader(mCommande);

                if (mDataReader.Read())
                {
                    mRowCount = (int)mDataReader["mRowCount"];
                    mDataReader.NextResult();
                }

                while (mDataReader.Read())
                {
                    ProcedureStockee mProcedureStockee = new ProcedureStockee();
                    MapDataReaderToFields(mDataReader, mProcedureStockee);
                    mList.Add(mProcedureStockee);
                    mProgress += 1;
                    if (evBindingProgress != null)
                    {
                        decimal value = (decimal)(mProgress / mRowCount) * 100;

                        evBindingProgress((int)Math.Round(value));
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "ProcedureStockee:fnSelect");
            }

            return mList;
        }

        //Retreive collection of object from db
        public System.Collections.Generic.List<DataPersist> fnSelect(Guid mFonctionID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            int mProgress = 0;
            int mRowCount = 0;
            try
            {
                //Dim mDataReader As IDataReader = _db.ExecuteReader("ac_Fournisseur_Lister", mCodeCategorie, mCodeStatus)

                IDataReader mDataReader = default(IDataReader);
                DataCommand mCommande = _db.CreateStoredProcCommand("se_ProcedureStockee_Lister_ParFonction");
                _db.AddInParameter(mCommande, "@FonctionID", SqlDbType.UniqueIdentifier, mFonctionID);

                mDataReader = _db.ExecuteReader(mCommande);

                if (mDataReader.Read())
                {
                    mRowCount = (int)mDataReader["mRowCount"];
                    mDataReader.NextResult();
                }

                while (mDataReader.Read())
                {
                    ProcedureStockee mProcedureStockee = new ProcedureStockee();
                    MapDataReaderToFields(mDataReader, mProcedureStockee);
                    mList.Add(mProcedureStockee);
                    mProgress += 1;
                    if (evBindingProgress != null)
                    {
                        evBindingProgress((int)Math.Round((decimal)(mProgress / mRowCount) * 100));
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "ProcedureStockee:fnSelect");
            }

            return mList;
        }

        //Send changes to db
        public override bool fnUpdate()
        {
            bool bolResult = false;
            DataCommand mCommande = default(DataCommand);
            try
            {
                //Define parameters
                if (this.IsNew)
                {
                    mCommande = _db.CreateStoredProcCommand("se_ProcedureStockee_Creer");
                    _db.AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    _db.AddOutParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0);
                }
                else {
                    mCommande = _db.CreateStoredProcCommand("se_ProcedureStockee_Modifier");
                    _db.AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0, _ID);
                    _db.AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
                }
                _db.AddInParameter(mCommande, "@Libelle", SqlDbType.VarChar, 50, _Libelle.Trim());
                _db.AddInParameter(mCommande, "@Description", SqlDbType.VarChar, 1000, _Description.Trim());
                _db.AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                _db.AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                //Send query to DB
                _db.ExecuteNonQuery(ref mCommande);

                //Check execution result
                switch ((int)_db.Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        if (this.IsNew)
                            _ID = (Guid)_db.Parameters(mCommande, "@ID");
                        _RowVersionKey = _db.Parameters(mCommande, "@RowVersion");
                        bolResult = true;
                        break;
                    default:
                        throw new Exception((string)_db.Parameters(mCommande, "@ErrorMessage"));
                        bolResult = false;
                        break;
                }
                this.IsNew = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "ProcedureStockee:fnUpdate");
                bolResult = false;
            }
            return bolResult;
        }

        public override bool fnActivate()
        {
            bool bolResult = false;
            DataCommand mCommande = default(DataCommand);

            try
            {
                mCommande = _db.CreateStoredProcCommand("se_ProcedureStockee_Activer");
                _db.AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                _db.AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                _db.AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
                _db.AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);

                _db.ExecuteNonQuery(ref mCommande);

                //Check execution result
                switch ((int)_db.Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        _Statut = 1;
                        _RowVersionKey = _db.Parameters(mCommande, "@RowVersion");
                        bolResult = true;
                        break;
                    default:
                        throw new Exception((string)_db.Parameters(mCommande, "@ErrorMessage"));
                        bolResult = false;
                        break;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "ProcedureStockee:fnActivate");
                bolResult = false;
            }
            return bolResult;
        }

        public override bool fnDeActivate()
        {
            bool bolResult = false;
            DataCommand mCommande = default(DataCommand);

            try
            {
                mCommande = _db.CreateStoredProcCommand("se_ProcedureStockee_Desactiver");
                _db.AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                _db.AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                _db.AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
                _db.AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);

                _db.ExecuteNonQuery(ref mCommande);

                //Check execution result
                switch ((int)_db.Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        _Statut = 0;
                        _RowVersionKey = _db.Parameters(mCommande, "@RowVersion");
                        bolResult = true;
                        break;
                    default:
                        throw new Exception((string)_db.Parameters(mCommande, "@ErrorMessage"));
                        bolResult = false;
                        break;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "ProcedureStockee:fnDeActivate");
                bolResult = false;
            }
            return bolResult;
        }

        public bool fnGet()
        {
            return fnGet(_ID);
        }

        public override bool fnGet(object mId)
        {
            bool bolResult = true;
            try
            {
                IDataReader mdataReader = _db.ExecuteReader("se_ProcedureStockee_Consulter", mId);

                if (mdataReader.Read())
                {
                    MapDataReaderToFields(mdataReader, this);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "ProcedureStockee:fnGet");
                bolResult = false;
            }
            return bolResult;
        }

        public override string ToString()
        {
            return _Libelle;
        }

        private void MapDataReaderToFields(IDataReader mDataReader, ProcedureStockee mObjet)
        {
            try
            {
                
                mObjet.IsNew = false;
                if (!DBNull.Value.Equals(mDataReader["prID"]))
                    mObjet.IDProcedureStockee = (Guid) mDataReader["prID"];
                if (!DBNull.Value.Equals(mDataReader["prLibelle"]))
                    mObjet.Libelle = (string)mDataReader["prLibelle"];
                if (!DBNull.Value.Equals(mDataReader["prDescription"]))
                    mObjet.Description = (string)mDataReader["prDescription"];
                if (!DBNull.Value.Equals(mDataReader["prStatut"]))
                    mObjet.Statut = (int)mDataReader["prStatut"];
                if (!DBNull.Value.Equals(mDataReader["prCreationUtilisateur"]))
                    mObjet.UtilisateurCreation =(string) mDataReader["prCreationUtilisateur"];
                if (!DBNull.Value.Equals(mDataReader["prCreationDate"]))
                    mObjet.DateCreation = (DateTime)mDataReader["prCreationDate"];
                if (!DBNull.Value.Equals(mDataReader["prModificationUtilisateur"]))
                    mObjet.UtilisateurModification = (string)mDataReader["prModificationUtilisateur"];
                if (!DBNull.Value.Equals(mDataReader["prModificationdate"]))
                    mObjet.DateModification = (DateTime)mDataReader["prModificationdate"];
                if (!DBNull.Value.Equals(mDataReader["prRowVersion"]))
                    mObjet.RowVersionKey = (object)mDataReader["prRowVersion"];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "ProcedureStockee:MapDataReaderToFields");
            }
        }

        #endregion



    }
}
