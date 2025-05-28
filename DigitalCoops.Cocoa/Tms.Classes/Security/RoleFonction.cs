using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Components.Data;

namespace Tms.Classes.Security
{
    public class RoleFonction : DataPersist
    {

        #region "Fields"
        private Guid _ID;
        private Role _Role;
        private Fonction _Fonction;
        private Module _Module;
        private int _mIcon;
        //private bool mStatutAChange = false;
        #endregion
        //private DataSource _db = new DataSource();

        #region "Properties"
        //[DataDisplayStyle(DefaultWidth = 20, Text = "", Visible = true)]
        //public string Icon
        //{
        //    get { return _Icon; }
        //    set { _Icon = value; }
        //}

        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Role Role
        {
            get { return _Role; }
            set { _Role = value; }
        }

        public Fonction Fonction
        {
            get { return _Fonction; }
            set { _Fonction = value; }
        }

        public Module Module
        {
            get { return _Module; }
            set { _Module = value; }
        }

        public string ModuleAsString
        {
            get { return _Module != null ? _Module.Nom : string.Empty; }            
        }

        public string FonctionAsString
        {
            get { return _Fonction != null ? _Fonction.Nom : string.Empty; }
        }


        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_isnew)
                    return 0; // BulletCross                
                else
                    return 2; //                     
            }
            set { _mIcon = value; }
        }

        #endregion

        public override List<DataPersist> fnSelect()
        {
            throw new NotImplementedException();
        }

        public List<DataPersist> fnSelect(Guid RoleID)
        {

            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommand = db().CreateStoredProcCommand("SecRoleFonctions_Select");

                db().AddInParameter(mCommand, "@roleID", SqlDbType.UniqueIdentifier, RoleID);
                mDataReader = db().ExecuteReader(mCommand);

                while (mDataReader.Read())
                {
                    RoleFonction mClass = new  RoleFonction();
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


        public bool fnRemove()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("SecRoleFonctions_Remove");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            //db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
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
                throw new Exception(ex.Message + "\r\n" + "SecRoleFonctions:fnRemove");
            }
            return Result;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("SecRoleFonctions_Get", (Guid)Id);
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

        public override bool fnUpdate()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {

                    mCommande = db().CreateStoredProcCommand("SecRoleFonctions_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("SecRoleFonctions_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@RoleID", SqlDbType.UniqueIdentifier, _Role.ID);
                db().AddInParameter(mCommande, "@FonctionID", SqlDbType.UniqueIdentifier, _Fonction.ID);                               

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
                throw new Exception(ex.Message + "\r\n" + "Fonction:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("SecRoleFonctions_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("SecRoleFonctions_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@RoleID", SqlDbType.UniqueIdentifier, _Role.ID);
                db().AddInParameter(mCommande, "@FonctionID", SqlDbType.UniqueIdentifier, _Fonction.ID);

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
                throw new Exception(ex.Message + "\r\n" + "Fonction:fnUpdate");

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


        private void MapFromDataReader(RoleFonction mClass, IDataReader mDataReader)
        {
            try
            {

                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass.Role = new Role();
                    if (!DBNull.Value.Equals(mDataReader["RoleId"])) mClass._Role.ID = (Guid)mDataReader["RoleId"];
                    if (!DBNull.Value.Equals(mDataReader["NomRole"])) mClass._Role.Nom = (string)mDataReader["NomRole"];

                    mClass.Fonction = new Fonction();
                    if (!DBNull.Value.Equals(mDataReader["FonctionId"])) mClass._Fonction.ID = (Guid)mDataReader["FonctionId"];
                    if (!DBNull.Value.Equals(mDataReader["NomFonction"])) mClass._Fonction.Nom = (string)mDataReader["NomFonction"];

                    mClass.Module = new Module();
                    if (!DBNull.Value.Equals(mDataReader["ModuleID"])) mClass._Module.ID = (Guid)mDataReader["ModuleID"];
                    if (!DBNull.Value.Equals(mDataReader["NomModule"])) mClass._Module.Nom = (string)mDataReader["NomModule"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "RoleFonction:MapFromDataReader");
            }
        }

        public override string ToString()
        {
            return Role.Nom;
        }

        #region "Static Member"
        public static string IdPropertyName = "ID";

        public static string SortPropertyName = "FonctionLibelle";
        public static string ValueProperty = "FonctionID";
        #endregion
        public static string DisplayProperty = "FonctionLibelle";
    }
}
