using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Components.Data;

namespace Tms.Classes.Shared.stock
{
    public class Magasin : DataPersist
    {
        #region Fields
        private int _ID;
        private string _Designation;
        private string _Localisation;
        private bool _EstExterne;
        private bool _EstTransit;
        private bool _Desactive;
        private bool _APontBascule;
        private StockType _StockType;

        private Site _Sites;
        #endregion

        #region Constructor
        public Magasin()
        {

        }

        public Magasin(int myId)
        {
            this.fnGet(myId);
        }

        #endregion

        #region Properties
        [ModelField(IDProperty = true)]
        public int ID
        {
            get
            {
                return _ID;
            }

            set
            {
                _ID = value;
            }
        }

        public string Designation
        {
            get
            {
                return _Designation;
            }

            set
            {
                _Designation = value;
            }
        }

        public string Localisation
        {
            get
            {
                return _Localisation;
            }

            set
            {
                _Localisation = value;
            }
        }

        public bool EstExterne
        {
            get
            {
                return _EstExterne;
            }

            set
            {
                _EstExterne = value;
            }
        }

        public bool EstTransit
        {
            get
            {
                return _EstTransit;
            }

            set
            {
                _EstTransit = value;
            }
        }

        public bool Desactive
        {
            get
            {
                return _Desactive;
            }

            set
            {
                _Desactive = value;
            }
        }

        public StockType StockType
        {
            get
            {
                return _StockType;
            }

            set
            {
                _StockType = value;
            }
        }

        public string StockTypeAsString
        {
            get
            {
                return _StockType != null ? _StockType.Designation : string.Empty;
            }
        }

        public int mIcon
        {
            get
            {
                if (_Desactive)
                    return 0;
                else return 2;
            }
        }
       
        public Site Sites
        {
            get
            {
                return _Sites;
            }

            set
            {
                _Sites = value;
            }
        }

        public bool APontBascule
        {
            get
            {
                return _APontBascule;
            }

            set
            {
                _APontBascule = value;
            }
        }

        public string SiteAsString
        {
            get
            {
                return _Sites != null ? _Sites.Nom : string.Empty;
            }
        }
        #endregion

        #region Methods        
        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Magasin_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "Magasin:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Magasin_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "Magasin:fnDeActivate");
            }
            return Result;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_Magasin_Get", (int)Id);
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

        public bool fnGetDefaultBySite(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_Magasin_GetDefaultBySite", (int)Id);
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
            return fnSelect(-1,1);
        }

        public List<DataPersist> fnSelect(int mStatus, int siteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Magasin_Select");
                db().AddInParameter(mCommande, "@Status", SqlDbType.SmallInt, mStatus);              
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.SmallInt, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Magasin mClass = new Magasin();

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

        public List<DataPersist> fnSelectForStock(int siteID = 1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Magasin_SelectForStock");
                //db().AddInParameter(mCommande, "@Status", SqlDbType.SmallInt, mStatus);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.SmallInt, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Magasin mClass = new Magasin();

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

                    mCommande = db().CreateStoredProcCommand("V2_Magasin_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.Int, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_Magasin_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@designation", SqlDbType.VarChar, _Designation);
                db().AddInParameter(mCommande, "@stockTypeID", SqlDbType.Int, _StockType.ID);
                db().AddInParameter(mCommande, "@localisation", SqlDbType.VarChar, _Localisation);
                db().AddInParameter(mCommande, "@estExterne", SqlDbType.Bit, _EstExterne);
                db().AddInParameter(mCommande, "@estTransit", SqlDbType.Bit, _EstTransit);
                db().AddInParameter(mCommande, "@PontBascule", SqlDbType.Bit, _APontBascule);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, _Sites.ID);

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
                throw new Exception(ex.Message + "\r\n" + "Magasin:fnUpdate");

            }
            return Result;
        }


        public override string ToString()
        {
            return _Designation;
        }

        private static void MapFromDataReader(Magasin mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (int)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Designation"])) mClass._Designation = (string)mDataReader["Designation"];
                    mClass._StockType = new StockType();
                    if (!DBNull.Value.Equals(mDataReader["StockTypeID"])) mClass._StockType.ID = (int)mDataReader["StockTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["StockTypeDesignation"])) mClass._StockType.Designation = (string)mDataReader["StockTypeDesignation"];
                    if (!DBNull.Value.Equals(mDataReader["MagasinLocalisation"])) mClass._Localisation = (string)mDataReader["MagasinLocalisation"];
                    if (!DBNull.Value.Equals(mDataReader["EstExterne"])) mClass._EstExterne = (bool)mDataReader["EstExterne"];
                    if (!DBNull.Value.Equals(mDataReader["EstTransit"])) mClass._EstTransit = (bool)mDataReader["EstTransit"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["APontBascule"])) mClass._APontBascule = (bool)mDataReader["APontBascule"];
                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                    mClass._Sites = new Site();
                    if (!DBNull.Value.Equals(mDataReader["SiteID"])) mClass._Sites.ID = (int)mDataReader["SiteID"];
                    if (!DBNull.Value.Equals(mDataReader["SiteNom"])) mClass._Sites.Nom = (string)mDataReader["SiteNom"];                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n Magasin:MapFromDataReader");
            }
        }
        #endregion
    }

    public partial class MagasinViewModel
    {
        public Magasin _Magasin { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
