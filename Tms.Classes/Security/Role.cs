using Ext.Net.MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Tms.Components.Data;

namespace Tms.Classes.Security
{
    public class Role : DataPersist
    {

        #region "Fields"
        private Guid _ID;                
        private string _Nom;
        private string _Description;        
        private bool _IsDisable;
 
        #endregion        

        #region "Properties"        

        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        
        public string Nom
        {
            get { return _Nom; }
            set { _Nom = value; }
        }

        public string RoleAsString
        {
            get { return _Nom; }            
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }                                
        
        public bool IsDisable
        {
            get { return _IsDisable; }
            set { _IsDisable = value; }
        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_IsDisable)
                    return 0; // BulletCross                
                else
                    return 2; //                     
            }
        }

        #endregion

        #region "Members"
        public Role()
        {
            //Nothing to do

        }

        //Create an instance of the class already initialized
        public Role(Guid myID)
        {
            this.fnGet(myID);
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("SecRole_Get", (Guid)Id);
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

        public List<DataPersist> fnSelect(int statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("SecRole_Select");

                db().AddInParameter(mCommande, "@statut", SqlDbType.Int, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Role mClass = new Role();

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

        //Retreive Roles of specified Utilisateur
        public List<Role> fnSelect(Utilisateur newUtilisateur)
        {
            List<Role> mList = new List<Role>();
            IDataReader mdataReader = null;

            try
            {
                mdataReader = db().ExecuteReader("se_RoleListe_Utilisateur", newUtilisateur.IDUtilisateur);

                while (mdataReader.Read())
                {
                    Role mClass = new Role();
                    MapFromDataReader(mClass, mdataReader);
                    mList.Add(mClass);
                }

                //While mdataReader.Read
                //    Dim mObjet As New Role
                //    With mObjet
                //        .IDRole = mdataReader!roID
                //        .BaseDeDonneesID = mdataReader!roBaseDeDonneesID
                //        .BaseDeDonneesLibelle = mdataReader!bdLibelle
                //        .CompagnieID = mdataReader!roCompagnieID
                //        .CompagnieLibelle = mdataReader!coLibelle
                //        .Libelle = mdataReader!roLibelle.ToString
                //        .Description = mdataReader!roDescription.ToString
                //        .Statut = mdataReader!roStatut
                //        .CreationUtilisateur = mdataReader!roCreationUtilisateur
                //        .CreationDate = mdataReader!roCreationDate
                //        .ModificationUtilisateur = mdataReader!roModificationUtilisateur
                //        .ModificationDate = mdataReader!roModificationDate
                //        .RowVersionRole = mdataReader!roRowVersion
                //        mList.Add(mObjet)
                //    End With
                //End While
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "Role:fnSelect");
            }
            finally
            {
                if (mdataReader != null)
                    mdataReader.Close();
            }

            return mList;
        }

        public override bool fnUpdate()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {

                    mCommande = db().CreateStoredProcCommand("SecRole_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("SecRole_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@Nom", SqlDbType.VarChar, _Nom);
                db().AddInParameter(mCommande, "@Description", SqlDbType.VarChar, _Description);                
                db().AddInParameter(mCommande, "@desactive", SqlDbType.Bit, _IsDisable);

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
                throw new Exception(ex.Message + "\r\n" + "PrixJournalier:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("SecRole_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("SecRole_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@Nom", SqlDbType.VarChar, _Nom);
                db().AddInParameter(mCommande, "@Description", SqlDbType.VarChar, _Description);                

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
                throw new Exception(ex.Message + "\r\n" + "PrixJournalier:fnUpdate");

            }
            return Result;
        }


        public override bool fnActivate()
        {

            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("SecRole_Activate");
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
                        IsDisable = false;
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
                throw new Exception(ex.Message + "\r\n" + "SecRole:fnActivate");
            }
            return Result;

        }

        public override bool fnDeActivate()
        {

            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("SecRole_DeActivate");
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
                        IsDisable = true;
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
                throw new Exception(ex.Message + "\r\n" + "SecRole:fnDeActivate");
            }
            return Result;

        }

        public List<DataPersist> fnSelectToUser(Guid userID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Utilisateur_SelectRoles");

                db().AddInParameter(mCommande, "@userID", SqlDbType.UniqueIdentifier, userID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Role mClass = new Role();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectToUser");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        #endregion

        private void MapFromDataReader(Role mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Nom"])) mClass._Nom = (string)mDataReader["Nom"];

                    if (!DBNull.Value.Equals(mDataReader["DescriptionRole"])) mClass._Description = (string)mDataReader["DescriptionRole"];
                    if (!DBNull.Value.Equals(mDataReader["IsDisabled"])) mClass._IsDisable = (bool)mDataReader["IsDisabled"];                    

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nSecModule:MapFromDataReader");
            }
        }                


        #region "Static Member"
        public static string SortPropertyName = "Libelle";

        public static string IdPropertyName = "IDRole";
        public static string DisplayProperty = "Libelle";
        #endregion
        public static string ValueProperty = "IDRole";

    }

    public partial class RoleViewModel
    {
        public Role _Role { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
