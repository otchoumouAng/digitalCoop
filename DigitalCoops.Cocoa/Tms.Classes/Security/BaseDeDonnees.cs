using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Tms.Components.Data;

namespace Tms.Classes.Security
{
    public class BaseDeDonnees : DataPersist
    {

        private Guid mID;
        private string mLibelle;
        private string mServeur;
        private string mBaseDeDonnees;
        private int mStatut;
        private string mCreationUser;
        private System.DateTime mCreationDate;
        private string mModificationUser;
        private System.DateTime mModificationDate;

        private object mRowVersion;

        private DataSource _db = new DataSource();
        #region "Properties"
        public Guid IDBaseDeDonnees
        {
            get { return mID; }
            set { mID = value; }
        }

        [DataDisplayStyle(DefaultWidth = 100, Text = "Name", Visible = true, EnglishText = "Caption")]
        public string Libelle
        {
            get { return mLibelle; }
            set { mLibelle = value; }
        }

        [DataDisplayStyle(DefaultWidth = 100, Text = "Server Name", Visible = true, EnglishText = "Server")]
        public string Serveur
        {
            get { return mServeur; }
            set { mServeur = value; }
        }

        [DataDisplayStyle(DefaultWidth = 150, Text = "Database Name", Visible = true, EnglishText = "Database")]
        public string BaseDeDonneesLibelle
        {
            get { return mBaseDeDonnees; }
            set { mBaseDeDonnees = value; }
        }

        [DataDisplayStyle(DefaultWidth = 100, Text = "Status", Visible = false, EnglishText = "Status")]
        public int Statut
        {
            get { return mStatut; }
            set { mStatut = value; }
        }

        [DataDisplayStyle(DefaultWidth = 100, Text = "Creation User", Visible = false, EnglishText = "Creation User")]
        public string CreationUser
        {
            get { return mCreationUser; }
            set { mCreationUser = value; }
        }

        [DataDisplayStyle(DefaultWidth = 100, Text = "Creation Date", Visible = false, EnglishText = "Creation Date")]
        public System.DateTime CreationDate
        {
            get { return mCreationDate; }
            set { mCreationDate = value; }
        }

        [DataDisplayStyle(DefaultWidth = 100, Text = "Modification User", Visible = false, EnglishText = "Modification User")]
        public string ModificationUser
        {
            get { return mModificationUser; }
            set { mModificationUser = value; }
        }

        [DataDisplayStyle(DefaultWidth = 100, Text = "Modification Date", Visible = false, EnglishText = "Modification Date")]
        public System.DateTime ModificationDate
        {
            get { return mModificationDate; }
            set { mModificationDate = value; }
        }

        public object RowVersionBaseDeDonnees
        {
            get { return mRowVersion; }
            set { mRowVersion = value; }
        }
        #endregion

        #region "Members"

        public BaseDeDonnees()
        {
            //Nothing to do
        }

        //Create an instance of the class already initialized
        public BaseDeDonnees(Guid newID)
        {
            try
            {
                fnGet(newID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "BaseDeDonnees:New");
            }
        }

        public override bool fnGet(object newID)
        {

            bool bolResult = true;
            IDataReader mdataReader = null;

            try
            {
                mdataReader = _db.ExecuteReader("se_BaseDeDonneesGet", newID);

                if (mdataReader.Read())
                {
                    MapDataReaderToFields(mdataReader, this);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "BaseDeDonnees:fnGet");
                bolResult = false;
            }
            finally
            {
                if (mdataReader != null)
                    mdataReader.Close();
            }

            return bolResult;
        }

        public override List<DataPersist> fnSelect()
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mdataReader = null;

            try
            {
                mdataReader = _db.ExecuteReader("se_BaseDeDonneesListe", 0);

                while (mdataReader.Read())
                {
                    BaseDeDonnees mClass = new BaseDeDonnees();
                    MapDataReaderToFields(mdataReader, mClass);
                    mList.Add(mClass);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "BaseDeDonnees:fnSelect");
            }
            finally
            {
                if (mdataReader != null)
                    mdataReader.Close();
            }

            return mList;

        }

        public List<DataPersist> fnSelect(Utilisateur newUtilisateur)
        {

            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mdataReader = null;

            try
            {
                mdataReader = _db.ExecuteReader("se_BaseDeDonneesListe_Utilisateur", 0);

                while (mdataReader.Read())
                {
                    BaseDeDonnees mClass = new BaseDeDonnees();
                    MapDataReaderToFields(mdataReader, mClass);
                    mList.Add(mClass);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "BaseDeDonnees:fnSelect");
            }
            finally
            {
                if (mdataReader != null)
                    mdataReader.Close();
            }

            return mList;

            //Dim mList As New List(Of BaseDeDonnees)
            //Try
            //    Dim mdataReader As IDataReader = _db.ExecuteReader("se_BaseDeDonneesListe_Utilisateur", newUtilisateur.IDUtilisateur)

            //    While mdataReader.Read
            //        Dim mBaseDeDonnees As New BaseDeDonnees
            //        With mBaseDeDonnees
            //            .IDBaseDeDonnees = mdataReader!bdID
            //            .Libelle = mdataReader!bdLibelle.ToString
            //            .Serveur = mdataReader!bdServeur.ToString
            //            .BaseDeDonnees = mdataReader!bdBaseDeDonnees.ToString
            //            .Statut = mdataReader!bdStatut
            //            .CreationUser = mdataReader!bdCreationUtilisateur
            //            .CreationDate = mdataReader!bdCreationDate
            //            .ModificationUser = mdataReader!bdModificationUtilisateur
            //            .ModificationDate = mdataReader!bdModificationDate
            //            .RowVersionBaseDeDonnees = mdataReader!bdRowVersion
            //            mList.Add(mBaseDeDonnees)
            //        End With
            //    End While
            //Catch ex As Exception
            //    Throw New Exception(ex.Message & vbCrLf & "BaseDeDonnees:fnSelect")
            //End Try

            //Return mList
        }


        public override bool fnUpdate()
        {
            bool bolResult = false;
            DataCommand mCommande = new DataCommand();
            try
            {
                if (mID == Guid.Empty)
                {
                    mCommande = _db.CreateStoredProcCommand("se_BaseDeDonneesCreer");
                }
                else {
                    mCommande = _db.CreateStoredProcCommand("se_BaseDeDonneesModifier");
                    _db.AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, mID);
                }

                _db.AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                _db.AddInParameter(mCommande, "@Libelle", SqlDbType.VarChar, 50, mLibelle.Trim());
                _db.AddInParameter(mCommande, "@Serveur", SqlDbType.VarChar, 50, mServeur.Trim());
                _db.AddInParameter(mCommande, "@BaseDeDonnees", SqlDbType.VarChar, 50, mBaseDeDonnees.Trim());

                if (mID != Guid.Empty)
                {
                    _db.AddInParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, mRowVersion);
                }

                _db.ExecuteNonQuery(ref mCommande);

                switch ((int)_db.Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        bolResult = true;
                        break;
                    case 101:
                        //Concurrency Error
                        throw new Exception("La mise à jour a échouée." + Environment.NewLine + "Les données ayant été modifiées par un autre utilisateur." + Environment.NewLine + "BaseDeDonnees:fnUpdate");
                        bolResult = false;
                        break;
                    default:
                        throw new Exception("Erreur inattendue lors de la mise à jour." + Environment.NewLine + "BaseDeDonnees:fnUpdate");
                        bolResult = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "BaseDeDonnees:fnUpdate");
                bolResult = false;
            }
            return bolResult;
        }

        public bool fnUpdate(BaseDeDonnees newBaseDeDonnees)
        {
            bool bolResult = false;
            DataCommand mCommande = default(DataCommand);
            try
            {
                if (mID == Guid.Empty)
                {
                    mCommande = _db.CreateStoredProcCommand("se_BaseDeDonneesCreer");
                }
                else {
                    mCommande = _db.CreateStoredProcCommand("se_BaseDeDonneesModifier");
                    _db.AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, mID);
                }

                _db.AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                _db.AddInParameter(mCommande, "@Libelle", SqlDbType.VarChar, 50, mLibelle.Trim());
                _db.AddInParameter(mCommande, "@Serveur", SqlDbType.VarChar, 50, mServeur.Trim());
                _db.AddInParameter(mCommande, "@BaseDeDonnees", SqlDbType.VarChar, 50, mBaseDeDonnees.Trim());
                _db.AddInParameter(mCommande, "@BaseDeDonneesModele", SqlDbType.UniqueIdentifier, 0, newBaseDeDonnees.IDBaseDeDonnees);

                if (mID != Guid.Empty)
                {
                    _db.AddInParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, mRowVersion);
                }

                _db.ExecuteNonQuery(ref mCommande);

                switch ((int)_db.Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        bolResult = true;
                        break;
                    case 101:
                        //Concurrency Error
                        throw new Exception("La mise à jour a échouée." + Environment.NewLine + "Les données ayant été modifiées par un autre utilisateur." + Environment.NewLine + "BaseDeDonnees:fnUpdate");
                        bolResult = false;
                        break;
                    default:
                        throw new Exception("Erreur inattendue lors de la mise à jour." + Environment.NewLine + "BaseDeDonnees:fnUpdate");
                        bolResult = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "BaseDeDonnees:fnUpdate");
                bolResult = false;
            }
            return bolResult;
        }

        public override bool fnActivate()
        {
            if (!this._isnew)
            {
                bool bolResult = false;
                DataCommand mCommande = _db.CreateStoredProcCommand("se_BaseDeDonneesActivate");
                _db.AddInParameter(mCommande, "@ID", SqlDbType.Int, mID);
                _db.AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
                _db.AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                _db.AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                try
                {
                    db().ExecuteNonQuery(ref mCommande);
                    switch (Convert.ToInt32(db().Parameters(mCommande, "ReturnValue")))
                    {
                        case 0:
                            //Everything OK
                            bolResult = true;
                            //mIsDisabled = False
                            _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                            break; // TODO: might not be correct. Was : Exit Select

                            break;
                        default:
                            bolResult = false;
                            string ErrorMessage = (string)db().Parameters(mCommande, "@ErrorMessage");
                            throw new Exception(ErrorMessage);
                            break; // TODO: might not be correct. Was : Exit Select

                            break;
                    }
                }
                catch (Exception ex)
                {
                    bolResult = false;
                    throw new Exception(ex.Message + Environment.NewLine + "BaseDeDonnees:fnActivate");
                }
                return bolResult;
            }
            return false;
        }

        public override bool fnDeActivate()
        {
            if (!this._isnew)
            {
                bool bolResult = false;
                DataCommand mCommande = _db.CreateStoredProcCommand("se_BaseDeDonneesDeActivate");
                _db.AddInParameter(mCommande, "@ID", SqlDbType.Int, mID);
                _db.AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
                _db.AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                _db.AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                try
                {
                    db().ExecuteNonQuery(ref mCommande);
                    switch (Convert.ToInt32(db().Parameters(mCommande, "ReturnValue")))
                    {
                        case 0:
                            //Everything OK
                            bolResult = true;
                            //mIsDisabled = False
                            _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                            break; // TODO: might not be correct. Was : Exit Select

                            break;
                        default:
                            bolResult = false;
                            string ErrorMessage = (string)db().Parameters(mCommande, "@ErrorMessage");
                            throw new Exception(ErrorMessage);
                            break; // TODO: might not be correct. Was : Exit Select

                            break;
                    }
                }
                catch (Exception ex)
                {
                    bolResult = false;
                    throw new Exception(ex.Message + Environment.NewLine+ "BaseDeDonnees:fnActivate");
                }
                return bolResult;
            }
            return false;
        }
        #endregion


        public override string ToString()
        {
            return mLibelle.ToString();
        }

        public int MergedStatus
        {
            get
            {
                if (mStatut == 0)
                {
                    return 0;
                }
                else {
                    return 1;
                }
            }
        }

        private void MapDataReaderToFields(IDataReader mDataReader, BaseDeDonnees mObjet)
        {
            try
            {
                
                if (!DBNull.Value.Equals(mDataReader["bdID"]))
                    mObjet.mID = (Guid)mDataReader["bdID"];
                if (!DBNull.Value.Equals(mDataReader["bdLibelle"]))
                    mObjet.mLibelle = (string)mDataReader["bdLibelle"];
                if (!DBNull.Value.Equals(mDataReader["bdServeur"]))
                    mObjet.mServeur = (string)mDataReader["bdServeur"];
                if (!DBNull.Value.Equals(mDataReader["bdStatut"]))
                    mObjet.mStatut = (int)mDataReader["bdStatut"];
                if (!DBNull.Value.Equals(mDataReader["bdCreationUtilisateur"]))
                    mObjet.UtilisateurCreation = (string)mDataReader["bdCreationUtilisateur"];
                if (!DBNull.Value.Equals(mDataReader["bdCreationDate"]))
                    mObjet.DateCreation = (DateTime)mDataReader["bdCreationDate"];
                if (!DBNull.Value.Equals(mDataReader["bdModificationUtilisateur"]))
                    mObjet.UtilisateurModification = (string)mDataReader["bdModificationUtilisateur"];
                if (!DBNull.Value.Equals(mDataReader["bdModificationDate"]))
                    mObjet.DateModification = (DateTime)mDataReader["bdModificationDate"];
                if (!DBNull.Value.Equals(mDataReader["bdRowVersion"]))
                    mObjet.RowVersionKey = (object)mDataReader["bdRowVersion"];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "BaseDeDonnees:MapDataReaderToFields");
            }
        }

        #region "Static Member"
        public static string IdPropertyName = "IDBaseDeDonnees";

        public static string SortPropertyName = "Libelle";
        public static string ValueProperty = "IDBaseDeDonnees";
        #endregion
        public static string DisplayProperty = "Libelle";
    }
}
