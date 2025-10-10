using Ext.Net.MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using Tms.Components.Data;

namespace Tms.Classes.Security
{
    public class Fonction : DataPersist
    {

        #region "Fields"
        private Guid _ID;
        private Module _Module;             
        private string _Nom;
        private string _Description;        
        private bool _IsDisable;
        private bool _IsReport;
        private string _MethodeName;
        private string _ControllerName;
        private bool _DisplayOnReportViewer;
        private bool _IsNewInList; 
        #endregion

        #region "Properties"        

        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Module Module
        {
            get { return _Module; }
            set { _Module = value; }
        }

        public string ModuleAsString
        {
            get { return _Module != null ? _Module.Nom : string.Empty ; }            
        }

        public string NomModuleComplet
        {
            get { return _Module != null ? _Module.NomComplet : string.Empty; }
        }

        public string Nom
        {
            get { return _Nom; }
            set { _Nom = value; }
        }

        public string FonctionAsString
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

        public bool IsReport
        {
            get { return _IsReport; }
            set { _IsReport = value; }
        }

        public string NomModule
        {
            get { return _Module != null ? _Module.Nom : string.Empty; }
            
        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_IsDisable)
                    return 0; // BulletCross  
                if (_IsNewInList)
                    return 4; // BulletCross                
                else
                    return 2; //                     
            }            
        }

        public string MethodeName
        {
            get
            {
                return _MethodeName;
            }

            set
            {
                _MethodeName = value;
            }
        }

        public string ControllerName
        {
            get
            {
                return _ControllerName;
            }

            set
            {
                _ControllerName = value;
            }
        }

        public string ShortFunctionName
        {
            get {
                if (!_Nom.Contains("."))
                {

                    string[] PartOfName = _Nom.Split('>');
                    int mpart = (PartOfName.Length - 1);
                    if (PartOfName[mpart].ToLower().TrimStart().Contains("print"))
                        PartOfName[mpart] = PartOfName[mpart].ToLower().Replace("print", "").TrimStart();
                    else
                        return string.Empty;
                    return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(PartOfName[2]);
                }
                else
                    return _Nom;
                //System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(input)
                
            }
        }

        public bool DisplayOnReportViewer
        {
            get
            {
                return _DisplayOnReportViewer;
            }

            set
            {
                _DisplayOnReportViewer = value;
            }
        }

        public bool IsNewInList
        {
            get
            {
                return _IsNewInList;
            }

            set
            {
                _IsNewInList = value;
            }
        }

        #endregion

        #region "Members"
        public Fonction()
        {
            //Nothing to do

        }
        
        public Fonction(Guid myID)
        {
            this.fnGet(myID);
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("SecFonction_Get", (Guid)Id);
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

        public bool fnGetUserAccessStatus(object fonctionID, string username)
        {
            bool returnValue = false;
            try
            {                
                DataCommand mCommande = db().CreateStoredProcCommand("SecFonction_GetUserAccessStatus");

                db().AddInParameter(mCommande, "@fonctionID", SqlDbType.UniqueIdentifier, Guid.Parse(fonctionID.ToString()));
                db().AddInParameter(mCommande, "@username", SqlDbType.VarChar, username);

                returnValue = ((int)db().ExecuteScalar(mCommande) == 1);

                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGet");
            }            
        }        

        public List<DataPersist> fnGetUserFunctionsByModule(Guid moduleID, string username)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("SecFonction_GetUserFunctionsByModule");

                db().AddInParameter(mCommande, "@moduleID", SqlDbType.UniqueIdentifier, moduleID);
                db().AddInParameter(mCommande, "@username", SqlDbType.VarChar, username);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Fonction mClass = new Fonction();

                    MapFromDataReaderLite(mClass, mDataReader);
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
                DataCommand mCommande = db().CreateStoredProcCommand("SecFonction_Select");

                db().AddInParameter(mCommande, "@statut", SqlDbType.Int, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Fonction mClass = new Fonction();

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

        public List<DataPersist> fnSelectReport(Guid UserId, Guid ModuleId)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("SecFonction_SelectReport");

                db().AddInParameter(mCommande, "@UserId", SqlDbType.UniqueIdentifier, UserId);
                db().AddInParameter(mCommande, "@ModuleId", SqlDbType.UniqueIdentifier, ModuleId);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Fonction mClass = new Fonction();

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


        public List<DataPersist> fnSelectToRole(Guid roleid)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("SecRole_SelectFonctions");

                db().AddInParameter(mCommande, "@roleID", SqlDbType.UniqueIdentifier, roleid);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Fonction mClass = new Fonction();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectToRole");
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

                    mCommande = db().CreateStoredProcCommand("SecFonction_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("SecFonction_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@Nom", SqlDbType.VarChar, _Nom);
                db().AddInParameter(mCommande, "@Description", SqlDbType.VarChar, _Description);
                db().AddInParameter(mCommande, "@ModuleID", SqlDbType.UniqueIdentifier, _Module.ID);                

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

        public override bool fnActivate()
        {

            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("SecFonction_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "SecFonction:fnActivate");
            }
            return Result;

        }

        public override bool fnDeActivate()
        {

            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("SecFonction_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "Fonction:fnDeActivate");
            }
            return Result;

        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        #endregion

        private void MapFromDataReader(Fonction mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Nom"])) mClass._Nom = (string)mDataReader["Nom"];

                    if (!DBNull.Value.Equals(mDataReader["DescriptionFonction"])) mClass._Description = (string)mDataReader["DescriptionFonction"];
                    if (!DBNull.Value.Equals(mDataReader["IsDisabled"])) mClass._IsDisable = (bool)mDataReader["IsDisabled"];
                    if (!DBNull.Value.Equals(mDataReader["IsReport"])) mClass._IsReport = (bool)mDataReader["IsReport"];
                    if (!DBNull.Value.Equals(mDataReader["MethodeName"])) mClass._MethodeName = (string)mDataReader["MethodeName"];
                    if (!DBNull.Value.Equals(mDataReader["ControllerName"])) mClass._ControllerName = (string)mDataReader["ControllerName"]; 
                    if (!DBNull.Value.Equals(mDataReader["DisplayOnReportViewer"])) mClass._DisplayOnReportViewer = (bool)mDataReader["DisplayOnReportViewer"];

                    mClass.Module = new Module();
                    if (!DBNull.Value.Equals(mDataReader["ModuleID"])) mClass._Module.ID = (Guid)mDataReader["ModuleID"];
                    if (!DBNull.Value.Equals(mDataReader["NomModule"])) mClass._Module.Nom = (string)mDataReader["NomModule"];
                    if (!DBNull.Value.Equals(mDataReader["NomModuleComplet"])) mClass._Module.NomComplet = (string)mDataReader["NomModuleComplet"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n Fonction:MapFromDataReader");
            }
        }

        private void MapFromDataReaderLite(Fonction mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n Fonction:MapFromDataReaderLite");
            }
        }

        #region "Static Member"
        public static string SortPropertyName = "Libelle";

        public static string IdPropertyName = "IDFonction";
        public static string DisplayProperty = "Libelle";
        #endregion
        public static string ValueProperty = "IDFonction";

    }

    public partial class FonctionViewModel
    {
        public Fonction _Fonction { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
