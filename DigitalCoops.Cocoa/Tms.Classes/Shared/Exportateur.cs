using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Components.Data;

namespace Tms.Classes.Shared
{
    public class Exportateur :DataPersist
    {
        #region "Fields"

        private int _ID;
        private string _Nom;
        private string _Adresse;
        private string _TelephoneFixe;
        private string _TelephoneMobile;
        private string _Fax;
        private string _PrefixeFacture;
        private string _Prefixe;
        private bool _Desative;

        #endregion

        #region "Properties"
        [ModelField(IDProperty = true)]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Nom
        {
            get { return _Nom; }
            set { _Nom = value; }
        }

        
        public string Adresse
        {
            get { return _Adresse; }
            set { _Adresse = value; }
        }

        public string TelephoneFixe
        {
            get { return _TelephoneFixe; }
            set { _TelephoneFixe = value; }
        }

        public string TelephoneMobile
        {
            get { return _TelephoneMobile; }
            set { _TelephoneMobile = value; }
        }

        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }

        public bool Desactive
        {
            get { return _Desative; }
            set { _Desative = value; }
        }

        public string AsString
        {
            get { return _Nom; }
        }

        public int mIcon
        {
            get
            {
                if (_Desative)
                    return 0; // BulletCross              
                else
                    return 2; //                     
            }
        }

        public string PrefixeFacture
        {
            get
            {
                return _PrefixeFacture;
            }

            set
            {
                _PrefixeFacture = value;
            }
        }

        public string Prefixe
        {
            get
            {
                return _Prefixe;
            }

            set
            {
                _Prefixe = value;
            }
        }
        #endregion

        #region Constructor
        public Exportateur()
        {

        }

        public Exportateur(int MyId)
        {
            this.fnGet(MyId);
        }
        #endregion

        #region Methods
        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("Exportateur_Get", (int)Id);
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

        public List<DataPersist> fnSelect(int mStatus)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Exportateur_Select");
                db().AddInParameter(mCommande, "@Status", SqlDbType.SmallInt, mStatus);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Exportateur mClass = new Exportateur();

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

                    mCommande = db().CreateStoredProcCommand("Exportateur_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.Int, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("Exportateur_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@nom", SqlDbType.VarChar, _Nom);
                db().AddInParameter(mCommande, "@adresse", SqlDbType.VarChar, _Adresse);
                db().AddInParameter(mCommande, "@telephonefixe", SqlDbType.VarChar, _TelephoneFixe);
                db().AddInParameter(mCommande, "@telephonemobile", SqlDbType.VarChar, _TelephoneMobile);
                db().AddInParameter(mCommande, "@fax", SqlDbType.VarChar, _Fax);
                db().AddInParameter(mCommande, "@prefixe", SqlDbType.Char,1, _Prefixe);
                db().AddInParameter(mCommande, "@prefixeFacture", SqlDbType.Char, 3, _PrefixeFacture);

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
                        _ID = (int)db().Parameters(mCommande, "@ID");

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
                throw new Exception(ex.Message + "\r\n" + "Exportateur:fnUpdate");

            }
            return Result;
        }

        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("Exportateur_Activate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
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
                throw new Exception(ex.Message + "\r\n" + "Exportateur:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("Exportateur_DeActivate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
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
                        Desactive = true;
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
                throw new Exception(ex.Message + "\r\n" + "Exportateur:fnDeActivate");
            }
            return Result;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region "Private Members"
        private static void MapFromDataReader(Exportateur mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (int)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Nom"])) mClass._Nom = (string)mDataReader["Nom"];
                    
                    if (!DBNull.Value.Equals(mDataReader["Adresse"])) mClass._Adresse = (string)mDataReader["Adresse"];
                    if (!DBNull.Value.Equals(mDataReader["TelephoneFixe"])) mClass._TelephoneFixe = (string)mDataReader["TelephoneFixe"];

                    if (!DBNull.Value.Equals(mDataReader["TelephoneMobile"])) mClass._TelephoneMobile = (string)mDataReader["TelephoneMobile"];
                    if (!DBNull.Value.Equals(mDataReader["Fax"])) mClass._Fax = (string)mDataReader["Fax"];
                    if (!DBNull.Value.Equals(mDataReader["Prefixe"])) mClass._Prefixe = (string)mDataReader["Prefixe"];
                    if (!DBNull.Value.Equals(mDataReader["PrefixeFacture"])) mClass._PrefixeFacture = (string)mDataReader["PrefixeFacture"];

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desative = (bool)mDataReader["Desactive"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n Transporteur:MapFromDataReader");
            }
        }
        #endregion        
               

    }

    public partial class ExportateurViewModel
    {
        public Exportateur _Exportateur { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
