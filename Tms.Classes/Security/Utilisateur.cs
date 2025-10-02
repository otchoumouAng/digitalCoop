using Ext.Net.MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms.Classes.Security
{
    public class Utilisateur : DataPersist
    {

        private Guid _ID;
        private string  _Name;
        private string _UserName;
        private string _Password;
        private string _OldPassword;
        private Site _BasedInLocation;
        private string _EmployeeNumber;
        private string _FunctionName;
        private string _Email;
        private bool _MustChangePwd;
        private DateTime? _LastChangedPwdDate;
        private bool _IsDisabled;
        private DataEncryption _EncryptionEngine;
        private string prefixProcédureStockee = "Utilisateur";
        private bool _IsAuthenticate;
        private Magasin _Magasin;

        [ModelField(IDProperty = true)]
        public Guid IDUtilisateur
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public bool IsDisabled
        {
            get { return _IsDisabled; }
            set { _IsDisabled = value; }
        }

        public bool MustChangePwd
        {
            get { return _MustChangePwd; }
            set { _MustChangePwd = value; }
        }

        public DateTime? LastChangedPwdDate
        {
            get { return _LastChangedPwdDate; }
            set { _LastChangedPwdDate = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }


        public string FunctionName
        {
            get { return _FunctionName; }
            set { _FunctionName = value; }
        }


        public string EmployeeNumber
        {
            get { return _EmployeeNumber; }
            set { _EmployeeNumber = value; }
        }


        public Site BasedInLocation
        {
            get { return _BasedInLocation; }
            set { _BasedInLocation = value; }
        }


        public string Password
        {
            get { return _Password; }
            set 
            { 
                _Password = value;
                _Password = (string)_EncryptionEngine.CryptageAssymetrique(_Password); 
            }
        }

        public string OldPassword
        {
            get { return _OldPassword; }
            set
            {
                _OldPassword = value;
                _OldPassword = (string)_EncryptionEngine.CryptageAssymetrique(_OldPassword);
            }
        }


        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
            }
        }

        public string  Name
        {
            get { return _Name; }
            set 
            { 
                _Name = value;               
             }
        }        

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_IsDisabled)
                    return 0; // BulletCross                
                else
                    return 2; //                     
            }           
        }

        public Utilisateur()
        {
            _EncryptionEngine = new DataEncryption();
        }


        public bool IsAuthenticate
        {
            get { return _IsAuthenticate; }
            set { _IsAuthenticate = value; }
        }

        public string SiteAsString
        {
            get { return _BasedInLocation != null ? _BasedInLocation.Nom : string.Empty; }
        }

        public Magasin Magasin
        {
            get
            {
                return _Magasin;
            }

            set
            {
                _Magasin = value;
            }
        }

        public override bool fnGet(object Id)
        {
            bool Result = true;
            IDataReader mdataReader = null;

            try
            {
                mdataReader = db().ExecuteReader(prefixProcédureStockee + "_Get", (Guid)Id);

                if (mdataReader.Read())
                {
                    MapFromDataReader( this, mdataReader);
                }

            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + Environment.NewLine + this.GetType().FullName+ ":fnGet");
                
            }
            finally
            {
                if (mdataReader != null)
                    mdataReader.Close();
            }

            return Result;
        }

        public bool fnGetByUserName(string username)
        {
            bool Result = true;
            IDataReader mdataReader = null;

            try
            {
                mdataReader = db().ExecuteReader(prefixProcédureStockee + "_GetByUserName", (string)username);

                if (mdataReader.Read())
                {
                    MapFromDataReader(this, mdataReader);
                }

            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + Environment.NewLine + this.GetType().FullName + ":fnGet");

            }
            finally
            {
                if (mdataReader != null)
                    mdataReader.Close();
            }

            return Result;
        }

        public bool fnAuthenticate()
        {
            bool bolResult = true;
            IDataReader mdataReader = null;
            try
            {
                DataCommand mCommand = db().CreateStoredProcCommand(prefixProcédureStockee + "_Authenticate");

                db().AddInParameter(mCommand, "@UserName", SqlDbType.VarChar,_UserName);
                db().AddInParameter(mCommand, "@Password", SqlDbType.VarChar, _Password);
                db().AddParameter(mCommand, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                db().AddOutParameter(mCommand, "@ErrorMessage", SqlDbType.VarChar, 1000);

                mdataReader = db().ExecuteReader(mCommand);

                if ((db().Parameters(mCommand, "ReturnValue") != null) && ((int)db().Parameters(mCommand, "ReturnValue") == -1))
                {
                    string ErrorMessage = (string)db().Parameters(mCommand, "@ErrorMessage");
                    throw new Exception(ErrorMessage);
                    return false;
                }

                if (mdataReader.Read())
                {
                    MapFromDataReaderLite(this, mdataReader);
                }
                //DataCommand mCommand = db().CreateStoredProcCommand(prefixProcédureStockee + "_Authenticate");

                //db().AddInParameter(mCommand, "@UserName", SqlDbType.VarChar,_UserName);
                //db().AddInParameter(mCommand, "@Password", SqlDbType.VarChar, _Password);

                // return ((int)db().ExecuteScalar(mCommand) == 1);
            }
            catch (Exception ex)
            {
                bolResult = false;
                throw new Exception(ex.Message + Environment.NewLine);
            }
            finally
            {
                if (mdataReader != null) mdataReader.Close();
            }
            return bolResult;
        }

        public bool fnCheckUser()
        {
            bool bolResult = true;
            IDataReader mdataReader = null;
            try
            {
                DataCommand mCommand = db().CreateStoredProcCommand(prefixProcédureStockee + "_CheckUserName");

                db().AddInParameter(mCommand, "@UserName", SqlDbType.VarChar, _UserName);                
                db().AddParameter(mCommand, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                db().AddOutParameter(mCommand, "@ErrorMessage", SqlDbType.VarChar, 1000);

                mdataReader = db().ExecuteReader(mCommand);

                if ((db().Parameters(mCommand, "ReturnValue") != null) && ((int)db().Parameters(mCommand, "ReturnValue") == -1))
                {
                    string ErrorMessage = (string)db().Parameters(mCommand, "@ErrorMessage");
                    throw new Exception(ErrorMessage);
                    return false;
                }

                if (mdataReader.Read())
                {
                    MapFromDataReaderLite(this, mdataReader);
                }
                                
            }
            catch (Exception ex)
            {
                bolResult = false;
                throw new Exception(ex.Message + Environment.NewLine);
            }
            finally
            {
                if (mdataReader != null) mdataReader.Close();
            }
            return bolResult;
        }


        public override List<DataPersist> fnSelect()
        {
            return fnSelect(-1, -1);
        }

        public List<DataPersist> fnSelect(int mLocation, int mStatus)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand(prefixProcédureStockee+"_Select");
                db().AddInParameter(mCommande, "@LocationID", SqlDbType.Int, mLocation);
                db().AddInParameter(mCommande, "@Status", SqlDbType.SmallInt, mStatus);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Utilisateur mClass = new Utilisateur();

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

                    mCommande = db().CreateStoredProcCommand(prefixProcédureStockee+"_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    //db().AddInParameter(mCommande, "@Password", SqlDbType.VarChar, _Password);
                    db().AddInParameter(mCommande, "@MustChangePwd", SqlDbType.Bit, 1);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand(prefixProcédureStockee+"_Modify");
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                }

                db().AddInParameter(mCommande, "@Name", SqlDbType.VarChar, _Name);
                db().AddInParameter(mCommande, "@UserName", SqlDbType.VarChar,_UserName);                

                db().AddInParameter(mCommande, "@BasedInLocationID", SqlDbType.Int, _BasedInLocation.ID);
                db().AddInParameter(mCommande, "@EmployeeNumber", SqlDbType.VarChar, _EmployeeNumber);
                db().AddInParameter(mCommande, "@FunctionName", SqlDbType.VarChar, _FunctionName);
                db().AddInParameter(mCommande, "@Email", SqlDbType.VarChar, _Email);                
                //db().AddInParameter(mCommande, "@LastChangedPassword", SqlDbType.DateTime, _LastChangedPwdDate);
                db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);


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
                        _isnew = false;
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
                throw new Exception(ex.Message + "\r\n" + this.GetType().FullName + ":fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand(prefixProcédureStockee + "_New");
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                    db().AddInParameter(mCommande, "@MustChangePwd", SqlDbType.Bit, 1);
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    //db().AddInParameter(mCommande, "@Password", SqlDbType.VarChar, _Password);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand(prefixProcédureStockee + "_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@Name", SqlDbType.VarChar, _Name);
                db().AddInParameter(mCommande, "@UserName", SqlDbType.VarChar, _UserName);                

                db().AddInParameter(mCommande, "@BasedInLocationID", SqlDbType.Int, _BasedInLocation.ID);
                db().AddInParameter(mCommande, "@MagasinID", SqlDbType.Int, _Magasin.ID);
                db().AddInParameter(mCommande, "@EmployeeNumber", SqlDbType.VarChar, _EmployeeNumber);
                db().AddInParameter(mCommande, "@FunctionName", SqlDbType.VarChar, _FunctionName);
                db().AddInParameter(mCommande, "@Email", SqlDbType.VarChar, _Email);                
                //db().AddInParameter(mCommande, "@LastChangedPassword", SqlDbType.DateTime, _LastChangedPwdDate);
                
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
                        _isnew = false;
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
                throw new Exception(ex.Message + "\r\n" + this.GetType().FullName + ":fnUpdate");

            }
            return Result;
        }

        public override bool fnActivate()
        {
            if (!this._isnew)
            {
                bool Result;
                DataCommand mCommande = db().CreateStoredProcCommand(prefixProcédureStockee + "_Activate");
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
                            _IsDisabled = false;
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
                    throw new Exception(ex.Message + "\r\n" + this.GetType().FullName + ":fnActivate");
                }
                return Result;
            }
            return false;
        }

        public override bool fnDeActivate()
        {
            if (!this._isnew)
            {
                bool bolResult;
                DataCommand mCommande = db().CreateStoredProcCommand(prefixProcédureStockee + "_DeActivate");
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
                            _IsDisabled = true;
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
                    throw new Exception(ex.Message + "\r\n" + this.GetType().FullName +  ":fnDeActivate");
                }
                return bolResult;
            }
            return false;
        }


        public bool fnUpdatePassword()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand(prefixProcédureStockee + "_ModifyPassword");
            db().AddInParameter(mCommande, "@userID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@oldPassword", SqlDbType.VarChar, _OldPassword);
            db().AddInParameter(mCommande, "@newPassword", SqlDbType.VarChar, _Password);
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
                        _MustChangePwd = false;
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
                throw new Exception(ex.Message + "\r\n" + this.GetType().FullName + ":fnModifyPassword");
            }
            return Result;
        }

        public bool fnNewPassword()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand(prefixProcédureStockee + "_NewPassword");
            db().AddInParameter(mCommande, "@userID", SqlDbType.UniqueIdentifier, _ID);            
            db().AddInParameter(mCommande, "@newPassword", SqlDbType.VarChar, _Password);
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
                        _MustChangePwd = false;
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
                throw new Exception(ex.Message + "\r\n" + this.GetType().FullName + ":fnNewPassword");
            }
            return Result;
        }

        public bool fnReinitialiserPassword()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand(prefixProcédureStockee + "_ReinitialiserPassword");
            db().AddInParameter(mCommande, "@userID", SqlDbType.UniqueIdentifier, _ID);            
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
                        _MustChangePwd = true;
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
                throw new Exception(ex.Message + "\r\n" + this.GetType().FullName + ":fnReinitialiserPassword");
            }
            return Result;
        }


        public override string ToString()
        {
            return _UserName;
        }

        public  string AsString()
        {
            return ToString();
        }



        private static void MapFromDataReader(Utilisateur mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Name"])) mClass._Name = (string)mDataReader["Name"];
                    if (!DBNull.Value.Equals(mDataReader["UserName"])) mClass._UserName = (string)mDataReader["UserName"];

                    mClass.BasedInLocation = new Site();
                    if (!DBNull.Value.Equals(mDataReader["LocationID"])) mClass._BasedInLocation.ID = (int)mDataReader["LocationID"];
                    if (!DBNull.Value.Equals(mDataReader["LocationName"])) mClass._BasedInLocation.Nom = (string)mDataReader["LocationName"];

                    mClass.Magasin = new Magasin();
                    if (!DBNull.Value.Equals(mDataReader["MagasinID"])) mClass._Magasin.ID = (int)mDataReader["MagasinID"];
                    if (!DBNull.Value.Equals(mDataReader["MagasinNom"])) mClass._Magasin.Designation = (string)mDataReader["MagasinNom"];

                    if (!DBNull.Value.Equals(mDataReader["IsDisabled"])) mClass._IsDisabled = (bool)mDataReader["IsDisabled"];
                    if (!DBNull.Value.Equals(mDataReader["EmployeeNumber"])) mClass._EmployeeNumber = (string)mDataReader["EmployeeNumber"];
                    if (!DBNull.Value.Equals(mDataReader["FunctionName"])) mClass._FunctionName = (string)mDataReader["FunctionName"];
                    if (!DBNull.Value.Equals(mDataReader["Email"])) mClass._Email = (string)mDataReader["Email"];
                    if (!DBNull.Value.Equals(mDataReader["MustChangePwd"])) mClass._MustChangePwd = (bool)mDataReader["MustChangePwd"];
                    if (!DBNull.Value.Equals(mDataReader["LastChangedPassword"])) mClass._LastChangedPwdDate = (DateTime)mDataReader["LastChangedPassword"];
                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n Utilisateur : MapFromDataReader");
            }
        }

        private static void MapFromDataReaderLite(Utilisateur mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;
                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["IsAuthenticate"])) mClass._IsAuthenticate = (bool)mDataReader["IsAuthenticate"];
                    if (!DBNull.Value.Equals(mDataReader["MustChangePwd"])) mClass._MustChangePwd = (bool)mDataReader["MustChangePwd"];                  
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n Utilisateur : MapFromDataReaderLite");
            }
        }







        //#region "Fields"
        //private Guid mID = Guid.Empty;
        //private string mLibelle;
        //private string mNomReseau;
        //private string mFonction;
        //private System.DateTime mDateDerniereConnexion;

        //private int mStatut;
        //private string mServeurMAJWams;

        //private DateTime mDateModificationMotPasse;
        //private string mAncienMotDePasse;

        //private string mNouveauMotDePasse;
        //private int mSiteParDefautID;

        //private string mSiteParDefautNom;
        //private int mDefaultDistrictID;

        //private string mDefaultDistrictNom;
        //private int mCompagnieID;

        //private string mCompagnieNom;
        ////Private mRoles As List(Of Role)
        ////Private mBasesDeDonnees As List(Of DataPersist)

        //#endregion
        //private DataSource _db = new DataSource();

        //#region "Properties"
        ////[DataDisplayStyle(DefaultWidth = 20, Text = "", Visible = true)]
        ////public string Icon
        ////{
        ////    get { return _Icon; }
        ////    set { _Icon = value; }
        ////}

        //public Guid IDUtilisateur
        //{
        //    get { return mID; }
        //    set { mID = value; }
        //}

        //[DataDisplayStyle(DefaultWidth = 200, Text = "Name", Visible = true)]
        //public string Libelle
        //{
        //    get { return mLibelle; }
        //    set { mLibelle = value; }
        //}

        //[DataDisplayStyle(DefaultWidth = 150, Text = "Network Name", Visible = true)]
        //public string NomReseau
        //{
        //    get { return mNomReseau; }
        //    set { mNomReseau = value; }
        //}

        //[DataDisplayStyle(DefaultWidth = 200, Text = "Function", Visible = true)]
        //public string Fonction
        //{
        //    get { return mFonction; }
        //    set { mFonction = value; }
        //}

        //public int SiteParDefautID
        //{
        //    get { return mSiteParDefautID; }
        //    set { mSiteParDefautID = value; }
        //}

        ////<DataDisplayStyle(DefaultWidth:=120, Text:="Site Par Defaut", Visible:=True)>
        //public string SiteParDefaut_Nom
        //{
        //    get { return mSiteParDefautNom; }
        //    set { mSiteParDefautNom = value; }
        //}


        //public int DefaultDistrictID
        //{
        //    get { return mDefaultDistrictID; }
        //    set { mDefaultDistrictID = value; }
        //}

        //[DataDisplayStyle(DefaultWidth = 120, Text = "Default District", Visible = true)]
        //public string DefaultDistrict_Nom
        //{
        //    get { return mDefaultDistrictNom; }
        //    set { mDefaultDistrictNom = value; }
        //}

        //public int CompagnieID
        //{
        //    get { return mCompagnieID; }
        //    set { mCompagnieID = value; }
        //}

        //[DataDisplayStyle(DefaultWidth = 120, Text = "Compagnie", Visible = true)]
        //public string Compagnie_Nom
        //{
        //    get { return mCompagnieNom; }
        //    set { mCompagnieNom = value; }
        //}

        //[DataDisplayStyle(DefaultWidth = 150, Text = "Last Connexion Date.", Visible = true)]
        //public System.DateTime DateDerniereConnexion
        //{
        //    get { return mDateDerniereConnexion; }
        //    set { mDateDerniereConnexion = value; }
        //}

        //[DataDisplayStyle(DefaultWidth = 100, Text = "Status", Visible = false)]
        //public int Statut
        //{
        //    get { return mStatut; }
        //    set { mStatut = value; }
        //}

        //[DataDisplayStyle(DefaultWidth = 100, Text = "Serveur par défaut", Visible = false)]
        //public string ServeurMAJWams
        //{
        //    get { return mServeurMAJWams; }
        //    set { mServeurMAJWams = value; }
        //}


        //[DataDisplayStyle(DefaultWidth = 150, Text = "PWD Modification Date.", Visible = false)]
        //public System.DateTime DateModificationMotPasse
        //{
        //    get { return mDateModificationMotPasse; }
        //    set { mDateModificationMotPasse = value; }
        //}

        //public string NouveauMotDePasse
        //{
        //    get { return mNouveauMotDePasse; }
        //    set { mNouveauMotDePasse = value; }
        //}

        //public string AncienMotDePasse
        //{
        //    get { return mAncienMotDePasse; }
        //    set { mAncienMotDePasse = value; }
        //}

        ////Public ReadOnly Property AMotDePasseReinitialise() As Boolean
        ////    Get
        ////        Try
        ////            Dim result As Boolean = _db.ExecuteScalar("se_UtilisateurAMotDePasseReinitalise", mID)
        ////            Return result
        ////        Catch ex As Exception
        ////            Throw
        ////        End Try
        ////    End Get
        ////End Property


        //public bool AMotDePasseReinitialise
        //{
        //    get
        //    {
        //        try
        //        {
        //            bool result = false;
        //            return result;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }
        //    }
        //}


        //#endregion

        //#region "Members"

        //public Utilisateur()
        //{
        //}

        //public Utilisateur(Guid newID)
        //{
        //    try
        //    {
        //        fnGet(newID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + Environment.NewLine + "Utilisateur:New");
        //    }
        //}

        //public Utilisateur(string newUsername, bool UserWithRoles = true)
        //{
        //    try
        //    {
        //        fnGet(newUsername, UserWithRoles);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + Environment.NewLine + "Utilisateur:New");
        //    }
        //}

        //public override bool fnGet(object newID)
        //{
        //    bool bolResult = true;
        //    IDataReader mdataReader = null;

        //    try
        //    {
        //        mdataReader = _db.ExecuteReader("se_UtilisateurGet", newID);

        //        if (mdataReader.Read())
        //        {
        //            MapDataReaderToFields(mdataReader, this);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + Environment.NewLine + "Utilisateur:fnGet");
        //        bolResult = false;
        //    }
        //    finally
        //    {
        //        if (mdataReader != null)
        //            mdataReader.Close();
        //    }

        //    return bolResult;


        //    //Dim bolResult As Boolean = True

        //    //Try
        //    //    Dim mdataReader As IDataReader = _db.ExecuteReader("se_UtilisateurGet", newID)
        //    //    'Get Utilisateur main data
        //    //    If mdataReader.Read Then
        //    //        mID = mdataReader!utID
        //    //        mLibelle = mdataReader!utLibelle.ToString
        //    //        mNomReseau = mdataReader!utNomReseau.ToString
        //    //        mFonction = mdataReader!utFonction.ToString
        //    //        mDateDerniereConnexion = mdataReader!utDateDerniereConnexion
        //    //        mStatut = mdataReader!utStatut
        //    //        mCreationUser = mdataReader!utCreationUtilisateur
        //    //        mCreationDate = mdataReader!utCreationDate
        //    //        mModificationUser = mdataReader!utModificationUtilisateur
        //    //        mModificationDate = mdataReader!utModificationDate
        //    //        mRowVersion = mdataReader!utRowVersion
        //    //        If Not IsDBNull(mdataReader!utServeurMAJWams) Then mServeurMAJWams = mdataReader!utServeurMAJWams

        //    //        ''Updated by Mz on Jan 2015
        //    //        'If Not IsDBNull(mdataReader!SiteParDefaut_Code) Then
        //    //        '    mSiteParDefaut = New Site()
        //    //        '    mSiteParDefaut.IDSite = mdataReader!SiteParDefaut_Code
        //    //        '    mSiteParDefaut.Nom = mdataReader!SiteParDefaut_Nom
        //    //        'End If

        //    //        If Not IsDBNull(mdataReader!SiteParDefaut_Code) Then SiteParDefautID = mdataReader!SiteParDefaut_Code
        //    //        If Not IsDBNull(mdataReader!SiteParDefaut_Nom) Then mSiteParDefautNom = mdataReader!SiteParDefaut_Nom

        //    //        If Not IsDBNull(mdataReader!Compagnie_Code) Then mCompagnieID = mdataReader!Compagnie_Code
        //    //        If Not IsDBNull(mdataReader!Compagnie_Nom) Then mCompagnieNom = mdataReader!Compagnie_Nom

        //    //    End If
        //    //    'Empty the Roles collection
        //    //    mRoles = New List(Of Role)
        //    //    If mdataReader.NextResult Then
        //    //        'Rebuild Roles collection
        //    //        While mdataReader.Read
        //    //            'Updated by mz on April 2015 : correct slowness on displaying user detail
        //    //            'Dim mRole As New Role(mdataReader!ruRoleID)
        //    //            Dim mRole As New Role()

        //    //            mRole.IDRole = mdataReader!ruRoleID
        //    //            mRole.Libelle = mdataReader!roLibelle

        //    //            mRoles.Add(mRole)
        //    //        End While
        //    //    End If

        //    //    mdataReader.Close()
        //    //    mdataReader.Dispose()
        //    //    mdataReader = Nothing
        //    //Catch ex As Exception
        //    //    Throw New Exception(ex.Message & vbCrLf & "Utilisateur:fnGet")
        //    //    bolResult = False
        //    //End Try

        //    //Return bolResult
        //}

        //public bool fnGet(string newUserName, bool UserWithRoles = true)
        //{
        //    bool bolResult = true;
        //    IDataReader mdataReader = null;

        //    try
        //    {
        //        mdataReader = _db.ExecuteReader("se_UtilisateurGetByName", newUserName);

        //        if (mdataReader.Read())
        //        {
        //            MapDataReaderToFields(mdataReader, this);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + Environment.NewLine + "Utilisateur:fnGet");
        //        bolResult = false;
        //    }
        //    finally
        //    {
        //        if (mdataReader != null)
        //            mdataReader.Close();
        //    }

        //    return bolResult;



        //    //Dim bolResult As Boolean = True

        //    //Try
        //    //    Dim mdataReader As IDataReader = _db.ExecuteReader("se_UtilisateurGetByName", newUserName)
        //    //    'Get Utilisateur main data
        //    //    If mdataReader.Read() Then
        //    //        mID = mdataReader!utID
        //    //        mLibelle = mdataReader!utLibelle.ToString
        //    //        mNomReseau = mdataReader!utNomReseau.ToString
        //    //        mFonction = mdataReader!utFonction.ToString
        //    //        mDateDerniereConnexion = mdataReader!utDateDerniereConnexion
        //    //        mStatut = mdataReader!utStatut
        //    //        mCreationUser = mdataReader!utCreationUtilisateur
        //    //        mCreationDate = mdataReader!utCreationDate
        //    //        mModificationUser = mdataReader!utModificationUtilisateur
        //    //        mModificationDate = mdataReader!utModificationDate
        //    //        mRowVersion = mdataReader!utRowVersion

        //    //        ''Updated by Mz on Jan 2015
        //    //        'If Not IsDBNull(mdataReader!SiteParDefaut_Code) Then
        //    //        '    mSiteParDefaut = New Site()
        //    //        '    mSiteParDefaut.IDSite = mdataReader!SiteParDefaut_Code
        //    //        '    mSiteParDefaut.Nom = mdataReader!SiteParDefaut_Nom
        //    //        'End If

        //    //        If Not IsDBNull(mdataReader!SiteParDefaut_Code) Then SiteParDefautID = mdataReader!SiteParDefaut_Code
        //    //        If Not IsDBNull(mdataReader!SiteParDefaut_Nom) Then mSiteParDefautNom = mdataReader!SiteParDefaut_Nom

        //    //        If Not IsDBNull(mdataReader!Compagnie_Code) Then mCompagnieID = mdataReader!Compagnie_Code
        //    //        If Not IsDBNull(mdataReader!Compagnie_Nom) Then mCompagnieNom = mdataReader!Compagnie_Nom

        //    //        'If Not DBNull.Value.Equals(mdataReader!utServeurMAJWams) Then
        //    //        '    mServeurMAJWams = mdataReader!utServeurMAJWams
        //    //        'End If
        //    //    End If
        //    //    'Empty the Roles collection
        //    //    mRoles = New List(Of Role)
        //    //    If UserWithRoles Then
        //    //        If mdataReader.NextResult Then
        //    //            'Rebuild Roles collection
        //    //            While mdataReader.Read
        //    //                Dim mRole As New Role(mdataReader!ruRoleID)
        //    //                mRoles.Add(mRole)
        //    //            End While
        //    //        End If
        //    //    End If

        //    //    mdataReader.Close()
        //    //    mdataReader.Dispose()
        //    //    mdataReader = Nothing

        //    //Catch ex As Exception
        //    //    Throw New Exception(ex.Message & vbCrLf & "Utilisateur:fnGet")
        //    //    bolResult = False
        //    //End Try

        //    //Return bolResult
        //}

        //public bool fnGet()
        //{
        //    bool bolResult = true;
        //    IDataReader mdataReader = null;

        //    try
        //    {
        //        mdataReader = _db.ExecuteReader("se_UtilisateurGet", mID);

        //        if (mdataReader.Read())
        //        {
        //            MapDataReaderToFields(mdataReader, this);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + Environment.NewLine + "Utilisateur:fnGet");
        //        bolResult = false;
        //    }
        //    finally
        //    {
        //        if (mdataReader != null)
        //            mdataReader.Close();
        //    }

        //    return bolResult;


        //    //Dim bolResult As Boolean = True

        //    //Try
        //    //    Dim mdataReader As IDataReader = _db.ExecuteReader("se_UtilisateurGet", mID)
        //    //    'Get Utilisateur main data
        //    //    If mdataReader.Read Then
        //    //        mLibelle = mdataReader!utLibelle.ToString
        //    //        mNomReseau = mdataReader!utNomReseau.ToString
        //    //        mFonction = mdataReader!utFonction.ToString
        //    //        mDateDerniereConnexion = mdataReader!utDateDerniereConnexion
        //    //        mStatut = mdataReader!utStatut
        //    //        mCreationUser = mdataReader!utCreationUtilisateur
        //    //        mCreationDate = mdataReader!utCreationDate
        //    //        mModificationUser = mdataReader!utModificationUtilisateur
        //    //        mModificationDate = mdataReader!utModificationDate
        //    //        mRowVersion = mdataReader!utRowVersion
        //    //        'If Not IsDBNull(mdataReader!utServeurMAJWams) Then mServeurMAJWams = mdataReader!utServeurMAJWams

        //    //        ''Updated by Mz on Jan 2015
        //    //        'If Not IsDBNull(mdataReader!SiteParDefaut_Code) Then
        //    //        '    mSiteParDefaut = New Site()
        //    //        '    mSiteParDefaut.IDSite = mdataReader!SiteParDefaut_Code
        //    //        '    mSiteParDefaut.Nom = mdataReader!SiteParDefaut_Nom
        //    //        'End If

        //    //        If Not IsDBNull(mdataReader!SiteParDefaut_Code) Then SiteParDefautID = mdataReader!SiteParDefaut_Code
        //    //        If Not IsDBNull(mdataReader!SiteParDefaut_Nom) Then mSiteParDefautNom = mdataReader!SiteParDefaut_Nom

        //    //        If Not IsDBNull(mdataReader!Compagnie_Code) Then mCompagnieID = mdataReader!Compagnie_Code
        //    //        If Not IsDBNull(mdataReader!Compagnie_Nom) Then mCompagnieNom = mdataReader!Compagnie_Nom

        //    //    End If

        //    //    'Empty the Roles collection
        //    //    mRoles = New List(Of Role)
        //    //    If mdataReader.NextResult Then
        //    //        'Rebuild Roles collection
        //    //        While mdataReader.Read

        //    //            'Updated by mz on April 2015 : correct slowness on displaying user detail
        //    //            'Dim mRole As New Role(mdataReader!ruRoleID)
        //    //            Dim mRole As New Role()

        //    //            mRole.IDRole = mdataReader!ruRoleID
        //    //            mRole.Libelle = mdataReader!roLibelle

        //    //            mRoles.Add(mRole)
        //    //        End While
        //    //    End If

        //    //    mdataReader.Close()
        //    //    mdataReader.Dispose()
        //    //    mdataReader = Nothing
        //    //Catch ex As Exception
        //    //    Throw New Exception(ex.Message & vbCrLf & "Utilisateur:fnGet")
        //    //    bolResult = False
        //    //End Try

        //    //Return bolResult
        //}



        //public override List<DataPersist> fnSelect()
        //{
        //    List<DataPersist> mList = new List<DataPersist>();
        //    IDataReader mdataReader = null;

        //    try
        //    {
        //        mdataReader = _db.ExecuteReader("se_UtilisateurListe", 0, DBNull.Value);

        //        while (mdataReader.Read())
        //        {
        //            Utilisateur mClass = new Utilisateur();
        //            MapDataReaderToFields(mdataReader, mClass);
        //            mList.Add(mClass);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + Environment.NewLine + "Role:fnSelect");
        //    }
        //    finally
        //    {
        //        if (mdataReader != null)
        //            mdataReader.Close();
        //    }

        //    return mList;




        //    //Dim mList As New List(Of DataPersist)

        //    //Try
        //    //    Dim mdataReader As IDataReader = _db.ExecuteReader("se_UtilisateurListe", 0, DBNull.Value)

        //    //    While mdataReader.Read
        //    //        Dim mUtilisateur As New Utilisateur
        //    //        With mUtilisateur
        //    //            .IDUtilisateur = mdataReader!utID
        //    //            .Libelle = mdataReader!utLibelle.ToString
        //    //            .NomReseau = mdataReader!utNomReseau.ToString
        //    //            .Fonction = mdataReader!utFonction.ToString
        //    //            .DateDerniereConnexion = mdataReader!utDateDerniereConnexion.ToString
        //    //            .Statut = mdataReader!utStatut
        //    //            .CreationUser = mdataReader!utCreationUtilisateur
        //    //            .CreationDate = mdataReader!utCreationDate
        //    //            .ModificationUser = mdataReader!utModificationUtilisateur
        //    //            .ModificationDate = mdataReader!utModificationDate
        //    //            .RowVersionUtilisateur = mdataReader!utRowVersion
        //    //            'If Not IsDBNull(mdataReader!utServeurMAJWams) Then .mServeurMAJWams = mdataReader!utServeurMAJWams

        //    //            ''Updated by Mz on Jan 2015
        //    //            'If Not IsDBNull(mdataReader!SiteParDefaut_Code) Then
        //    //            '    .SiteParDefaut = New Site()
        //    //            '    .SiteParDefaut.IDSite = mdataReader!SiteParDefaut_Code
        //    //            '    .SiteParDefaut.Nom = mdataReader!SiteParDefaut_Nom
        //    //            'End If
        //    //            If Not IsDBNull(mdataReader!SiteParDefaut_Code) Then SiteParDefautID = mdataReader!SiteParDefaut_Code
        //    //            If Not IsDBNull(mdataReader!SiteParDefaut_Nom) Then mSiteParDefautNom = mdataReader!SiteParDefaut_Nom

        //    //            If Not IsDBNull(mdataReader!Compagnie_Code) Then mCompagnieID = mdataReader!Compagnie_Code
        //    //            If Not IsDBNull(mdataReader!Compagnie_Nom) Then mCompagnieNom = mdataReader!Compagnie_Nom

        //    //            mList.Add(mUtilisateur)
        //    //        End With
        //    //    End While
        //    //Catch ex As Exception
        //    //    Throw New Exception(ex.Message & vbCrLf & "Utilisateur:fnSelect")
        //    //End Try
        //    //Return mList
        //}



        //public List<DataPersist> fnSelect(Role mRole)
        //{
        //    List<DataPersist> mList = new List<DataPersist>();
        //    IDataReader mdataReader = null;

        //    try
        //    {
        //        mdataReader = _db.ExecuteReader("se_UtilisateurListe", 0, mRole.IDRole);

        //        while (mdataReader.Read())
        //        {
        //            Utilisateur mClass = new Utilisateur();
        //            MapDataReaderToFields(mdataReader, mClass);
        //            mList.Add(mClass);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + Environment.NewLine + "Role:fnSelect");
        //    }
        //    finally
        //    {
        //        if (mdataReader != null)
        //            mdataReader.Close();
        //    }

        //    return mList;






        //    //Dim mList As New List(Of Utilisateur)

        //    //Try
        //    //    Dim mdataReader As IDataReader = _db.ExecuteReader("se_UtilisateurListe", 0, mRole.IDRole)

        //    //    While mdataReader.Read
        //    //        Dim mUtilisateur As New Utilisateur
        //    //        With mUtilisateur
        //    //            .IDUtilisateur = mdataReader!utID
        //    //            .Libelle = mdataReader!utLibelle.ToString
        //    //            .NomReseau = mdataReader!utNomReseau.ToString
        //    //            .Fonction = mdataReader!utFonction.ToString
        //    //            .DateDerniereConnexion = mdataReader!utDateDerniereConnexion.ToString
        //    //            .Statut = mdataReader!utStatut
        //    //            .CreationUser = mdataReader!utCreationUtilisateur
        //    //            .CreationDate = mdataReader!utCreationDate
        //    //            .ModificationUser = mdataReader!utModificationUtilisateur
        //    //            .ModificationDate = mdataReader!utModificationDate
        //    //            .RowVersionUtilisateur = mdataReader!utRowVersion
        //    //            'If Not IsDBNull(mdataReader!utServeurMAJWams) Then .mServeurMAJWams = mdataReader!utServeurMAJWams

        //    //            ''Updated by Mz on Jan 2015
        //    //            'If Not IsDBNull(mdataReader!SiteParDefaut_Code) Then
        //    //            '    .SiteParDefaut = New Site()
        //    //            '    .SiteParDefaut.IDSite = mdataReader!SiteParDefaut_Code
        //    //            '    .SiteParDefaut.Nom = mdataReader!SiteParDefaut_Nom
        //    //            'End If

        //    //            If Not IsDBNull(mdataReader!SiteParDefaut_Code) Then SiteParDefautID = mdataReader!SiteParDefaut_Code
        //    //            If Not IsDBNull(mdataReader!SiteParDefaut_Nom) Then mSiteParDefautNom = mdataReader!SiteParDefaut_Nom

        //    //            If Not IsDBNull(mdataReader!Compagnie_Code) Then mCompagnieID = mdataReader!Compagnie_Code
        //    //            If Not IsDBNull(mdataReader!Compagnie_Nom) Then mCompagnieNom = mdataReader!Compagnie_Nom

        //    //            mList.Add(mUtilisateur)
        //    //        End With
        //    //    End While
        //    //Catch ex As Exception
        //    //    Throw New Exception(ex.Message & vbCrLf & "Utilisateur:fnSelect")
        //    //End Try

        //    //Return mList
        //}

        //public override bool fnUpdate()
        //{

        //    return false;
        //}

        //public bool fnUpdate(bool UserWithRoles = true)
        //{
        //    bool bolResult = false;
        //    DataCommand mCommande = new DataCommand();
        //    DataTransaction mTransaction = new DataTransaction();

        //    if (mID == Guid.Empty)
        //    {
        //        mCommande = _db.CreateStoredProcCommand("se_UtilisateurCreer");
        //        _db.AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
        //    }
        //    else {
        //        mCommande = _db.CreateStoredProcCommand("se_UtilisateurModifier");
        //        _db.AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, mID);
        //    }

        //    _db.AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
        //    _db.AddInParameter(mCommande, "@Libelle", SqlDbType.VarChar, 50, mLibelle.Trim());
        //    _db.AddInParameter(mCommande, "@NomReseau", SqlDbType.VarChar, 50, mNomReseau.Trim());
        //    _db.AddInParameter(mCommande, "@Fonction", SqlDbType.VarChar, 50, mFonction.Trim());
        //    _db.AddInParameter(mCommande, "@DateDerniereConnexion", SqlDbType.DateTime, 0, mDateDerniereConnexion);

        //    if (mID != Guid.Empty)
        //    {
        //        _db.AddInParameter(mCommande, "@ResetRoles", SqlDbType.Bit, 0, UserWithRoles);
        //        _db.AddInParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, _RowVersionKey);
        //    }

        //    //Updated by Mz on Jan 2015
        //    _db.AddInParameter(mCommande, "@SiteParDefaut", SqlDbType.SmallInt, mSiteParDefautID);
        //    _db.AddInParameter(mCommande, "@DistrictParDefaut", SqlDbType.SmallInt, mDefaultDistrictID);
        //    _db.AddInParameter(mCommande, "@Compagnie", SqlDbType.SmallInt, mCompagnieID);

        //    //Start db transaction
        //    mTransaction = _db.BeginTransaction();
        //    try
        //    {
        //        //Edit table Utilisateur
        //        _db.ExecuteNonQuery(ref mCommande, mTransaction);

        //        switch ((int)_db.Parameters(mCommande, "ReturnValue"))
        //        {
        //            case 0:
        //                //Everything OK

        //                //In case of Creer, mID is not initialized
        //                mID = (Guid)_db.Parameters(mCommande, "@ID");

        //                //If UserWithRoles Then
        //                //    'Edit table Utilisateur_Role
        //                //    For Each mRole In mRoles
        //                //        Dim cmdUtilisateurRole As DataCommand = _db.CreateStoredProcCommand("se_UtilisateurRoleCreer")

        //                //        _db.AddParameter(cmdUtilisateurRole, "ReturnValue", SqlDbType.Int, 0, Nothing, ParameterDirection.ReturnValue)
        //                //        _db.AddInParameter(cmdUtilisateurRole, "@UtilisateurID", SqlDbType.UniqueIdentifier, mID)
        //                //        _db.AddInParameter(cmdUtilisateurRole, "@RoleID", SqlDbType.UniqueIdentifier, mRole.IDRole)
        //                //        _db.ExecuteNonQuery(cmdUtilisateurRole, mTransaction)
        //                //        If _db.Parameters(cmdUtilisateurRole, "ReturnValue") <> 0 Then
        //                //            _db.RollBackTransaction(mTransaction)
        //                //            Throw New Exception("Erreur inattendue lors de la mise à jour des Roles de l'Utilisateur." & vbCrLf & "Utilisateur:fnUpdate")
        //                //            Return False
        //                //        End If
        //                //    Next
        //                //End If

        //                //'User with databases ?
        //                //If Not IsNothing(mBasesDeDonnees) Then
        //                //    If mBasesDeDonnees.Count > 0 Then
        //                //        'Edit table Utilisateur_DB
        //                //        For Each mDB In mBasesDeDonnees
        //                //            Dim cmdUtilisateurDB As DataCommand = _db.CreateStoredProcCommand("se_UtilisateurDBCreer")

        //                //            _db.AddParameter(cmdUtilisateurDB, "ReturnValue", SqlDbType.Int, 0, Nothing, ParameterDirection.ReturnValue)
        //                //            _db.AddInParameter(cmdUtilisateurDB, "@UtilisateurID", SqlDbType.UniqueIdentifier, mID)
        //                //            _db.AddInParameter(cmdUtilisateurDB, "@BaseDeDonneesID", SqlDbType.UniqueIdentifier, mDB.IDBaseDeDonnees)
        //                //            _db.ExecuteNonQuery(cmdUtilisateurDB, mTransaction)
        //                //            If _db.Parameters(cmdUtilisateurDB, "ReturnValue") <> 0 Then
        //                //                _db.RollBackTransaction(mTransaction)
        //                //                Throw New Exception("Erreur inattendue lors de la mise à jour des bases de données de l'Utilisateur." & vbCrLf & "Utilisateur:fnUpdate")
        //                //                Return False
        //                //            End If
        //                //        Next
        //                //    End If
        //                //End If


        //                _db.CommitTransaction(mTransaction);
        //                bolResult = true;

        //                break;
        //            case 101:
        //                //Concurrency Error
        //                _db.RollBackTransaction(mTransaction);
        //                throw new Exception("La mise à jour a échouée." + Environment.NewLine + "Les données ayant été modifiées par un autre utilisateur." + Environment.NewLine + "Utilisateur:fnUpdate");
        //                bolResult = false;

        //                break;
        //            case -1:
        //                //Network Name in use
        //                _db.RollBackTransaction(mTransaction);
        //                throw new Exception("La mise à jour a échouée." + Environment.NewLine + "Le nom réseau fourni est utilisé." + Environment.NewLine + "Utilisateur:fnUpdate");
        //                bolResult = false;

        //                break;
        //            default:
        //                _db.RollBackTransaction(mTransaction);
        //                throw new Exception("Erreur inattendue lors de la mise à jour." + Environment.NewLine + "Utilisateur:fnUpdate");
        //                bolResult = false;
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _db.RollBackTransaction(mTransaction);
        //        throw new Exception(ex.Message + Environment.NewLine + "Utilisateur:fnUpdate");
        //        bolResult = false;
        //    }

        //    return bolResult;
        //}

        //public bool fnUpdateMotDePasse()
        //{
        //    bool bolResult = false;
        //    DataCommand mCommande = new DataCommand();

        //    mCommande = _db.CreateStoredProcCommand("se_UtilisateurModifierMotDePasse");
        //    //_db.AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, mID)
        //    _db.AddInParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, _RowVersionKey);
        //    _db.AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
        //    _db.AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
        //    _db.AddInParameter(mCommande, "@OldPassword", SqlDbType.VarChar, 400, mAncienMotDePasse);
        //    _db.AddInParameter(mCommande, "@NewPassword", SqlDbType.VarChar, 400, mNouveauMotDePasse);
        //    _db.AddInParameter(mCommande, "@NomReseau", SqlDbType.VarChar, 400, mNomReseau);
        //    //Start db transaction
        //    try
        //    {
        //        //Edit table Utilisateur
        //        _db.ExecuteNonQuery(ref mCommande);

        //        switch ((int)_db.Parameters(mCommande, "ReturnValue"))
        //        {
        //            case 0:
        //                //Everything OK
        //                _RowVersionKey = _db.Parameters(mCommande, "@RowVersion");
        //                bolResult = true;
        //                break;
        //            default:
        //                throw new Exception((string)_db.Parameters(mCommande, "@ErrorMessage"));
        //                bolResult = false;
        //                break;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + Environment.NewLine + "Utilisateur:fnUpdateMotDePasse");
        //        bolResult = false;
        //    }

        //    return bolResult;
        //}

        //public bool fnNouveauMotDePasse()
        //{
        //    bool bolResult = false;
        //    DataCommand mCommande = default(DataCommand);

        //    mCommande = _db.CreateStoredProcCommand("se_UtilisateurNouveauMotDePasse");
        //    //_db.AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, mID)
        //    _db.AddInParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, _RowVersionKey);
        //    _db.AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
        //    _db.AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
        //    //_db.AddInParameter(mCommande, "@OldPassword", SqlDbType.VarChar, 400, mAncienMotDePasse)
        //    _db.AddInParameter(mCommande, "@NewPassword", SqlDbType.VarChar, 400, mNouveauMotDePasse);
        //    _db.AddInParameter(mCommande, "@NomReseau", SqlDbType.VarChar, 400, mNomReseau);
        //    //Start db transaction
        //    try
        //    {
        //        //Edit table Utilisateur
        //        _db.ExecuteNonQuery(ref mCommande);

        //        switch ((int)_db.Parameters(mCommande, "ReturnValue"))
        //        {
        //            case 0:
        //                //Everything OK
        //                _RowVersionKey = _db.Parameters(mCommande, "@RowVersion");
        //                bolResult = true;
        //                break;
        //            default:
        //                throw new Exception((string)_db.Parameters(mCommande, "@ErrorMessage"));
        //                bolResult = false;
        //                break;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + Environment.NewLine + "Utilisateur:fnUpdateMotDePasse");
        //        bolResult = false;
        //    }

        //    return bolResult;
        //}

        //public override bool fnActivate()
        //{
        //    if (!(mID == Guid.Empty))
        //    {
        //        bool bolResult = false;
        //        DataCommand mCommande = _db.CreateStoredProcCommand("se_UtilisateurActiver");
        //        _db.AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, mID);
        //        _db.AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
        //        _db.AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

        //        try
        //        {
        //            _db.ExecuteNonQuery(ref mCommande);
        //            switch ((int)_db.Parameters(mCommande, "ReturnValue"))
        //            {
        //                case 0:
        //                    //Everything OK
        //                    bolResult = true;
        //                    mStatut = 1;
        //                    break;
        //                default:
        //                    bolResult = false;
        //                    string ErrorMessage = (string)_db.Parameters(mCommande, "@ErrorMessage");
        //                    throw new Exception(ErrorMessage);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            bolResult = false;
        //            throw new Exception(ex.Message + Environment.NewLine + "Utilisateur:fnActiver");
        //        }
        //        return bolResult;
        //    }
        //    return false;
        //}

        //public override bool fnDeActivate()
        //{
        //    if (!(mID == Guid.Empty))
        //    {
        //        bool bolResult = false;
        //        DataCommand mCommande = _db.CreateStoredProcCommand("se_UtilisateurDesactiver");
        //        _db.AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, mID);
        //        _db.AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
        //        _db.AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);


        //        try
        //        {
        //            _db.ExecuteNonQuery(ref mCommande);
        //            switch ((int)_db.Parameters(mCommande, "ReturnValue"))
        //            {
        //                case 0:
        //                    //Everything OK
        //                    bolResult = true;
        //                    mStatut = 0;
        //                    break;
        //                default:
        //                    bolResult = false;
        //                    string ErrorMessage = (string)_db.Parameters(mCommande, "@ErrorMessage");
        //                    throw new Exception(ErrorMessage);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            bolResult = false;
        //            throw new Exception(ex.Message + Environment.NewLine + "Utilisateur:fnDesactiver");
        //        }
        //        return bolResult;
        //    }
        //    return false;
        //}

        //#endregion


        //private void MapDataReaderToFields(IDataReader mDataReader, Utilisateur mObjet)
        //{
        //    try
        //    {

        //        if (!DBNull.Value.Equals(mDataReader["utID"]))
        //            mObjet.IDUtilisateur = (Guid)mDataReader["utID"];
        //        if (!DBNull.Value.Equals(mDataReader["utLibelle"]))
        //            mObjet.Libelle = (string)mDataReader["utLibelle"];
        //        if (!DBNull.Value.Equals(mDataReader["utNomReseau"]))
        //            mObjet.NomReseau = (string)mDataReader["utNomReseau"];
        //        if (!DBNull.Value.Equals(mDataReader["utFonction"]))
        //            mObjet.Fonction = (string)mDataReader["utFonction"];
        //        if (!DBNull.Value.Equals(mDataReader["utDateDerniereConnexion"]))
        //            mObjet.DateDerniereConnexion = (DateTime)mDataReader["utDateDerniereConnexion"];
        //        if (!DBNull.Value.Equals(mDataReader["SiteParDefaut_Code"]))
        //            mObjet.SiteParDefautID = (int)mDataReader["SiteParDefaut_Code"];
        //        if (!DBNull.Value.Equals(mDataReader["SiteParDefaut_Nom"]))
        //            mObjet.mSiteParDefautNom = (string)mDataReader["SiteParDefaut_Nom"];
        //        if (!DBNull.Value.Equals(mDataReader["DistrictParDefaut_Code"]))
        //            mObjet.mDefaultDistrictID = (int)mDataReader["DistrictParDefaut_Code"];
        //        if (!DBNull.Value.Equals(mDataReader["DistrictParDefaut_Nom"]))
        //            mObjet.mDefaultDistrictNom = (string)mDataReader["DistrictParDefaut_Nom"];
        //        if (!DBNull.Value.Equals(mDataReader["Compagnie_Code"]))
        //            mObjet.CompagnieID = (int)mDataReader["Compagnie_Code"];
        //        if (!DBNull.Value.Equals(mDataReader["Compagnie_Nom"]))
        //            mObjet.mCompagnieNom = (string)mDataReader["Compagnie_Nom"];
        //        if (!DBNull.Value.Equals(mDataReader["utStatut"]))
        //            mObjet.Statut = (int)mDataReader["utStatut"];
        //        //If Not IsDBNull(mdataReader!utServeurMAJWams) Then mServeurMAJWams = mdataReader!utServeurMAJWams
        //        if (!DBNull.Value.Equals(mDataReader["utCreationUtilisateur"]))
        //            mObjet.UtilisateurCreation = (string)mDataReader["utCreationUtilisateur"];
        //        if (!DBNull.Value.Equals(mDataReader["utCreationDate"]))
        //            mObjet.DateCreation = (DateTime)mDataReader["utCreationDate"];
        //        if (!DBNull.Value.Equals(mDataReader["utModificationUtilisateur"]))
        //            mObjet.UtilisateurModification = (string)mDataReader["utModificationUtilisateur"];
        //        if (!DBNull.Value.Equals(mDataReader["utModificationDate"]))
        //            mObjet.DateModification = (DateTime)mDataReader["utModificationDate"];
        //        if (!DBNull.Value.Equals(mDataReader["utRowVersion"]))
        //            mObjet.RowVersionKey = (object)mDataReader["utRowVersion"];
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + Environment.NewLine + "Role:MapDataReaderToFields");
        //    }
        //}


        //public override string ToString()
        //{
        //    return mLibelle.ToString();
        //}

        //public int MergedStatus
        //{
        //    get
        //    {
        //        if (mStatut == 0)
        //        {
        //            return 0;
        //        }
        //        else {
        //            return 1;
        //        }
        //    }
        //}


        //#region "Static Member"
        //public static string SortPropertyName = "Libelle";

        //public static string IdPropertyName = "IDUtilisateur";
        //public static string DisplayProperty = "Libelle";

        //public static string ValueProperty = "IDUtilisateur";

        ////Const MAXIMUM_DIGIT_INCREMENT_LENGTH As Integer = 5
        ////Const MAXIMUM_DIGIT_COMMUNITY_LENGTH As Integer = 4

        ////Const MAXIMUM_SUPPLIER_CODE_LENGTH As Integer = 5
        ////Const MINIMUM_SUPPLIER_CODE_VALUE As Integer = 0
        ////Const MAXIMUM_SUPPLIER_CODE_VALUE As Integer = 99999
        //#endregion
    }

    public partial class UtilisateurViewModel
    {
        public Utilisateur _Utilisateur { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
