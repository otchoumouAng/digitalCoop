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
    public class Site : DataPersist
    {
        #region "Fields"

        private int _ID;
        private int? _PrefixeSite;
        private string _Nom;
        private Site _DefautSite;
        private Provenance _Provenance;
        private Destination _Destination;
        private bool _Desactive;
        private bool _IsNewInList;
        private int _MagasinID;
        private string _MagasinNom;

        #endregion

        #region "Properties"
        [ModelField(IDProperty = true)]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int? PrefixeSite
        {
            get { return _PrefixeSite; }
            set { _PrefixeSite = value; }
        }

        public string Nom
        {
            get { return _Nom; }
            set { _Nom = value; }
        }

        public Site DefautSite
        {
            get { return _DefautSite; }
            set { _DefautSite = value; }
        }

        public Destination Destination
        {
            get { return _Destination; }
            set { _Destination = value; }
        }

        public Provenance Provenance
        {
            get { return _Provenance; }
            set { _Provenance = value; }
        }

        public string ProvenanceAsString
        {
            get { return _Provenance != null ? _Provenance.AsString : string.Empty; }            
        }

        public int ProvenanceID
        {
            get { return _Provenance != null ? _Provenance.ID : 1; }
        }


        public string DestinationAsString
        {
            get { return _Destination != null ? _Destination.AsString : string.Empty; }
        }

        public int DestinationID
        {
            get { return _Destination != null ? _Destination.ID : 1; }
        }

        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
        }

        public bool IsNewInList
        {
            get { return _IsNewInList; }
            set { _IsNewInList = value; }
        }

        public int mIcon
        {
            get
            {
                if (_Desactive)
                    return 0; // BulletCross              
                else
                    return 2; //                     
            }
        }
        #endregion

        #region Methods
        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("Site_Get", (int) Id);
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

        public bool fnGetDefaultSite()
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("Site_GetDefault");
                if (mDataReader.Read())
                {
                    MapFromDataReader(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetDefaultSite");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public bool fnGetBySiteByUserName(object userName)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("Site_GetByUserName", (string)userName);
                if (mDataReader.Read())
                {
                    MapFromDataReade2(this, mDataReader);
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
                DataCommand mCommande = db().CreateStoredProcCommand("Site_Select");
                db().AddInParameter(mCommande, "@Status", SqlDbType.SmallInt, mStatus);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Site mClass = new Site();

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

                    mCommande = db().CreateStoredProcCommand("Site_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.Int, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("Site_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@Nom", SqlDbType.VarChar, _Nom);

                if (_PrefixeSite != null) db().AddInParameter(mCommande, "@Prefixe", SqlDbType.Int, _PrefixeSite);
                else db().AddInParameter(mCommande, "@Prefixe", SqlDbType.Int, DBNull.Value);

                if (_Provenance != null) db().AddInParameter(mCommande, "@ProvenanceID", SqlDbType.Int, _Provenance.ID);
                else db().AddInParameter(mCommande, "@ProvenanceID", SqlDbType.Int, DBNull.Value);

                if (_Destination != null ) db().AddInParameter(mCommande, "@DestinationID", SqlDbType.Int, _Destination.ID);
                else db().AddInParameter(mCommande, "@DestinationID", SqlDbType.Int, DBNull.Value);

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
                throw new Exception(ex.Message + "\r\n" + "Site:fnUpdate");

            }
            return Result;
        }

        public List<DataPersist> fnSelectToPrice(Guid priceID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_PrixJournalier_SelectSites");

                db().AddInParameter(mCommande, "@priceID", SqlDbType.UniqueIdentifier, priceID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Site mClass = new Site();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectToPrice");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("Site_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "Site:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("Site_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "Site:fnDeActivate");
            }
            return Result;
        }

        public bool fnGetByUserName(object userName)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("Site_GetByUserName", (string)userName);
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

        public List<DataPersist> fnSelectAvailableForFS(int userSite, int FournisseurID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_Site_SelectAvailableForSupplier");
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@userSite", SqlDbType.Int , userSite);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Site mClass = new Site();

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

        public List<DataPersist> fnSelectAvailableForAnalyseur(int analyseurID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_AnalyseurSite_SitesAvailables");
                //db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, fournisseurID);
                db().AddInParameter(mCommande, "@AnalyseurID", SqlDbType.Int, analyseurID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Site mClass = new Site();

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


        public List<DataPersist> fnSelectAvailableForPrice(string userName)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_Site_SelectAvailableForPrice");
                
                db().AddInParameter(mCommande, "@userName", SqlDbType.VarChar, 100, userName);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Site mClass = new Site();

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
        #endregion

        #region "Private Members"


        public override string ToString()
        {
            return _Nom;
        }

        private static void MapFromDataReader(Site mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (int)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["PrefixeSite"])) mClass._PrefixeSite = (int)mDataReader["PrefixeSite"];
                    if (!DBNull.Value.Equals(mDataReader["Nom"])) mClass._Nom = (string)mDataReader["Nom"];
                    mClass._DefautSite = new Site();
                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._DefautSite.ID = (int)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Nom"])) mClass._DefautSite.Nom = (string)mDataReader["Nom"];

                    mClass._Destination = new Destination();
                    if (!DBNull.Value.Equals(mDataReader["DestinationID"])) mClass._Destination.ID = (int)mDataReader["DestinationID"];
                    if (!DBNull.Value.Equals(mDataReader["DestinationNom"])) mClass._Destination.Nom = (string)mDataReader["DestinationNom"];

                    mClass._Provenance = new Provenance();
                    if (!DBNull.Value.Equals(mDataReader["ProvenanceID"])) mClass._Provenance.ID = (int)mDataReader["ProvenanceID"];
                    if (!DBNull.Value.Equals(mDataReader["ProvenanceNom"])) mClass._Provenance.Nom = (string)mDataReader["ProvenanceNom"];

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];                                        

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nSite:MapFromDataReader");
            }
        }

        private static void MapFromDataReade2(Site mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (int)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["PrefixeSite"])) mClass._PrefixeSite = (int)mDataReader["PrefixeSite"];
                    if (!DBNull.Value.Equals(mDataReader["Nom"])) mClass._Nom = (string)mDataReader["Nom"];
                    mClass._DefautSite = new Site();
                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._DefautSite.ID = (int)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Nom"])) mClass._DefautSite.Nom = (string)mDataReader["Nom"];

                    mClass._Destination = new Destination();
                    if (!DBNull.Value.Equals(mDataReader["DestinationID"])) mClass._Destination.ID = (int)mDataReader["DestinationID"];
                    if (!DBNull.Value.Equals(mDataReader["DestinationNom"])) mClass._Destination.Nom = (string)mDataReader["DestinationNom"];

                    mClass._Provenance = new Provenance();
                    if (!DBNull.Value.Equals(mDataReader["ProvenanceID"])) mClass._Provenance.ID = (int)mDataReader["ProvenanceID"];
                    if (!DBNull.Value.Equals(mDataReader["ProvenanceNom"])) mClass._Provenance.Nom = (string)mDataReader["ProvenanceNom"];

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];

                    if (!DBNull.Value.Equals(mDataReader["MagasinID"])) mClass._MagasinID = (int)mDataReader["MagasinID"];
                    if (!DBNull.Value.Equals(mDataReader["MagasinNom"])) mClass._MagasinNom = (string)mDataReader["MagasinNom"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nSite:MapFromDataReader");
            }
        }
        #endregion


        public string AsString
        {
            get { return _Nom; }
        }

        public int MagasinID
        {
            get
            {
                return _MagasinID;
            }

            set
            {
                _MagasinID = value;
            }
        }

        public string MagasinNom
        {
            get
            {
                return _MagasinNom;
            }

            set
            {
                _MagasinNom = value;
            }
        }
    }

    public partial class SiteViewModel
    {
        public Site _Site { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
