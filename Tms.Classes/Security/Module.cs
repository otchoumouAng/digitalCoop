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
    public class Module : DataPersist
    {
        #region Fields
        private Guid _ID;
        private Application _Application;
        private string _Nom;
        private string _Description;
        private string _Url;
        private string _FullUrl;
        private string _IconModule;
        private bool _IsDisable;
        private double _Order;        

        private string _BackGroundColor;  
        private string _DisplayOnTwoCols;
        private bool _DisplayableOnWorkSpace;
        private string _NomComplet;
        #endregion

        #region Properties

        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }

        }

        public Application Application
        {
            get { return _Application; }
            set { _Application = value; }

        }

        public string ApplicationAsString
        {
            get { return _Application != null ? _Application.Nom : string.Empty; }

        }

        public string ApplicationAndModuleAsString
        {
            get { return _Application != null ? _Application.Nom + " > " + _Nom + " > " : string.Empty; }

        }

        public string Nom
        {
            get { return _Nom; }
            set { _Nom = value; }

        }

        public string ModuleAsString
        {
            get { return _Nom; }
            set { _Nom = value; }

        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }

        }

        public string Url
        {
            get { return _Url; }
            set { _Url = value; }

        }

        public string FullUrl
        {
            get { return _FullUrl; }
            set { _FullUrl = value; }

        }

        public string IconModule
        {
            get { return _IconModule; }
            set { _IconModule = value; }

        }

        public bool IsDisable
        {
            get { return _IsDisable; }
            set { _IsDisable = value; }

        }

        public double Order
        {
            get { return _Order; }
            set { _Order = value; }

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

        public string BackGroundColor
        {
            get
            {
                return _BackGroundColor;
            }

            set
            {
                _BackGroundColor = value;
            }
        }

        public string DisplayOnTwoCols
        {
            get
            {
                return _DisplayOnTwoCols;
            }

            set
            {
                _DisplayOnTwoCols = value;
            }
        }

        public bool DisplayableOnWorkSpace
        {
            get
            {
                return _DisplayableOnWorkSpace;
            }

            set
            {
                _DisplayableOnWorkSpace = value;
            }
        }

        public string NomComplet
        {
            get
            {
                return _NomComplet;
            }

            set
            {
                _NomComplet = value;
            }
        }
        #endregion

        #region Constructor
        public Module()
        {

        }

        public Module(Guid myID)
        {
            this.fnGet(myID);
        }
        #endregion

        #region Methods
        public override bool fnActivate()
        {

            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("SecModule_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "SecModule:fnActivate");
            }
            return Result;

        }


        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("SecModule_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "SecModule:fnDeActivate");
            }
            return Result;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("SecModule_Get", (Guid)Id);
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

        public List<DataPersist> fnSelect(int? statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("SecModule_Select");
                
                db().AddInParameter(mCommande, "@statut", SqlDbType.Int, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Module mClass = new Module();

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

        public List<DataPersist> fnSelectToUser(Guid userId)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Utilisateur_SelectModules");

                db().AddInParameter(mCommande, "@userID", SqlDbType.UniqueIdentifier, userId);                
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Module mClass = new Module();

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

        public List<DataPersist> fnSelectAllByUserAndApp(Guid userId, Guid AppID,int DisplayOnWorkspace, int DisplayOnReport)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("SecModule_SelectAllByUser");

                db().AddInParameter(mCommande, "@userID", SqlDbType.UniqueIdentifier, userId);
                db().AddInParameter(mCommande, "@appID", SqlDbType.UniqueIdentifier, AppID);
                db().AddInParameter(mCommande, "@DisplayOnWorkspace", SqlDbType.SmallInt, DisplayOnWorkspace);
                db().AddInParameter(mCommande, "@DisplayOnReport", SqlDbType.SmallInt, DisplayOnReport);
                
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Module mClass = new Module();

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

                    mCommande = db().CreateStoredProcCommand("SecModule_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("SecModule_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@ApplicationID", SqlDbType.UniqueIdentifier, _Application.ID);
                db().AddInParameter(mCommande, "@Nom", SqlDbType.VarChar, _Nom);
                db().AddInParameter(mCommande, "@Description", SqlDbType.VarChar, _Description);
                db().AddInParameter(mCommande, "@Url", SqlDbType.VarChar, _Url);
                db().AddInParameter(mCommande, "@Icon", SqlDbType.VarChar, _IconModule);                

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
                throw new Exception(ex.Message + "\r\n" + "SecModule:fnUpdate");

            }
            return Result;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region "Private Members"       

        private static void MapFromDataReader(Module mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Nom"])) mClass._Nom = (string)mDataReader["Nom"];

                    if (!DBNull.Value.Equals(mDataReader["DescriptionModule"])) mClass._Description = (string)mDataReader["DescriptionModule"];
                    if (!DBNull.Value.Equals(mDataReader["UrlModule"])) mClass._Url = (string)mDataReader["UrlModule"];
                    if (!DBNull.Value.Equals(mDataReader["FullUrlModule"])) mClass._FullUrl = (string)mDataReader["FullUrlModule"];
                    if (!DBNull.Value.Equals(mDataReader["IconModule"])) mClass._IconModule = (string)mDataReader["IconModule"];
                    if (!DBNull.Value.Equals(mDataReader["IsDisabled"])) mClass._IsDisable = (bool)mDataReader["IsDisabled"];
                    if (!DBNull.Value.Equals(mDataReader["Ordre"])) mClass._Order = (double)mDataReader["Ordre"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                    mClass.Application = new Application();
                    if (!DBNull.Value.Equals(mDataReader["ApplicationID"])) mClass._Application.ID = (Guid)mDataReader["ApplicationID"];
                    if (!DBNull.Value.Equals(mDataReader["NomApplication"])) mClass._Application.Nom = (string)mDataReader["NomApplication"];

                    if (!DBNull.Value.Equals(mDataReader["BackGroundColor"])) mClass._BackGroundColor = (string)mDataReader["BackGroundColor"];
                    if (!DBNull.Value.Equals(mDataReader["DisplayOnTwoCols"])) mClass._DisplayOnTwoCols = (string)mDataReader["DisplayOnTwoCols"];
                    if (!DBNull.Value.Equals(mDataReader["DisplayableOnWorkspace"])) mClass._DisplayableOnWorkSpace = (bool)mDataReader["DisplayableOnWorkspace"];

                    if (!DBNull.Value.Equals(mDataReader["NomComplet"])) mClass._NomComplet = (string)mDataReader["NomComplet"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nSecModule:MapFromDataReader");
            }
        }
        #endregion

    }

    public partial class ModuleViewModel
    {
        public Module _Module { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}



