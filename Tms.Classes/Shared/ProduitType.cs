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
    public class ProduitType : DataPersist
    {
        #region "Fields"

        private int _ID;
        private string _Designation;
        private ProduitFini _ProduitFini;
        private bool _Desactive;
        private object _RowVersionKey;
        private int _ProduitFiniID;
        private string _ProduitFiniDesignation;

        #endregion

        #region "Properties"
        [ModelField(IDProperty = true)]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }

        public ProduitFini ProduitFini
        {
            get { return _ProduitFini; }
            set { _ProduitFini = value; }
        }

        public string ProduitFiniDesignation
        {
            get { return _ProduitFiniDesignation; }
            set { _ProduitFiniDesignation = value; }
        }

        public int ProduitFiniID
        {
            get { return _ProduitFiniID; }
            set { _ProduitFiniID = value; }
        }

        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
        }

        public object RowVersionKey
        {
            get { return _RowVersionKey; }
            set { _RowVersionKey = value; }
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
                mDataReader = db().ExecuteReader("pp_ProduitType_Get", (int)Id);
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

        public bool fnGetDefaultProduitType()
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("ProduitType_GetDefault");
                if (mDataReader.Read())
                {
                    MapFromDataReader(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetDefaultProduitType");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public bool fnGetByProduitTypeByUserName(object userName)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("ProduitType_GetByUserName", (string)userName);
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
                DataCommand mCommande = db().CreateStoredProcCommand("pp_ProduitType_Select");
                db().AddInParameter(mCommande, "@Statut", SqlDbType.SmallInt, mStatus);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    ProduitType mClass = new ProduitType();

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

                    mCommande = db().CreateStoredProcCommand("pp_ProduitType_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.Int, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("pp_ProduitType_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@Designation", SqlDbType.VarChar, _Designation);

                //if (_PrefixeProduitType != null) db().AddInParameter(mCommande, "@Prefixe", SqlDbType.Int, _PrefixeProduitType);
                //else db().AddInParameter(mCommande, "@Prefixe", SqlDbType.Int, DBNull.Value);

                if (_ProduitFini != null) db().AddInParameter(mCommande, "@CodeProduitFini", SqlDbType.Int, _ProduitFini.ID);
                else db().AddInParameter(mCommande, "@CodeProduitFini", SqlDbType.Int, DBNull.Value);

                //if (_Destination != null) db().AddInParameter(mCommande, "@DestinationID", SqlDbType.Int, _Destination.ID);
                //else db().AddInParameter(mCommande, "@DestinationID", SqlDbType.Int, DBNull.Value);

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
                throw new Exception(ex.Message + "\r\n" + "ProduitType:fnUpdate");

            }
            return Result;
        }

        public List<DataPersist> fnSelectToPrice(Guid priceID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_PrixJournalier_SelectProduitTypes");

                db().AddInParameter(mCommande, "@priceID", SqlDbType.UniqueIdentifier, priceID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    ProduitType mClass = new ProduitType();

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
            DataCommand mCommande = db().CreateStoredProcCommand("pp_ProduitType_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "ProduitType:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("pp_ProduitType_DeActivate"); 
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
                throw new Exception(ex.Message + "\r\n" + "ProduitType:fnDeActivate");
            }
            return Result;
        }

        public bool fnGetByUserName(object userName)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("ProduitType_GetByUserName", (string)userName);
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

        public List<DataPersist> fnSelectAvailableForFS(int userProduitType, int FournisseurID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_ProduitType_SelectAvailableForSupplier");
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@userProduitType", SqlDbType.Int, userProduitType);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    ProduitType mClass = new ProduitType();

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
                DataCommand mCommande = db().CreateStoredProcCommand("V3_AnalyseurProduitType_ProduitTypesAvailables");
                //db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, fournisseurID);
                db().AddInParameter(mCommande, "@AnalyseurID", SqlDbType.Int, analyseurID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    ProduitType mClass = new ProduitType();

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
                DataCommand mCommande = db().CreateStoredProcCommand("V3_ProduitType_SelectAvailableForPrice");

                db().AddInParameter(mCommande, "@userName", SqlDbType.VarChar, 100, userName);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    ProduitType mClass = new ProduitType();

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
            return _Designation;
        }

        //private static void MapFromDataReader(ProduitType mClass, IDataReader mDataReader)
        //{
        //    try
        //    {
        //        if (mDataReader != null)
        //        {
        //            mClass.IsNew = false;

        //            if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (int)mDataReader["ID"];
        //            if (!DBNull.Value.Equals(mDataReader["Designation"])) mClass._Designation = (string)mDataReader["Designation"];



        //            mClass._ProduitFini = new ProduitFini();

        //            if (!DBNull.Value.Equals(mDataReader["CodeProduitFini"])) mClass._ProduitFiniID = (int)mDataReader["CodeProduitFini"];
        //            if (!DBNull.Value.Equals(mDataReader["ProduitFiniDesignation"])) mClass._ProduitFiniDesignation =  (string)mDataReader["ProduitFiniDesignation"];

        //            if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];

        //            if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
        //            if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
        //            if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
        //            if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
        //            if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "\nProduitType:MapFromDataReader");
        //    }
        //}

        private static void MapFromDataReader(ProduitType mClass, IDataReader rdr)
        {
            try
            {
                if (rdr == null || rdr.IsClosed)
                    return;

                mClass.IsNew = false;

                // ID et Designation du type
                if (!DBNull.Value.Equals(rdr["ID"]))
                    mClass._ID = (int)rdr["ID"];
                if (!DBNull.Value.Equals(rdr["Designation"]))
                    mClass._Designation = (string)rdr["Designation"];

                // Code et libellé du Produit Fini
                if (!DBNull.Value.Equals(rdr["CodeProduitFini"]))
                    mClass._ProduitFiniID = (int)rdr["CodeProduitFini"];
                if (!DBNull.Value.Equals(rdr["ProduitFiniDesignation"]))
                    mClass._ProduitFiniDesignation = (string)rdr["ProduitFiniDesignation"];

                // Injection dans l'objet ProduitFini
                mClass._ProduitFini = new ProduitFini
                {
                    ID = mClass._ProduitFiniID,
                    Designation = mClass._ProduitFiniDesignation
                };

                // Désactivation
                if (!DBNull.Value.Equals(rdr["Desactive"]))
                    mClass._Desactive = (bool)rdr["Desactive"];

                // Audit
                if (!DBNull.Value.Equals(rdr["CreationUtilisateur"]))
                    mClass.UtilisateurCreation = (string)rdr["CreationUtilisateur"];
                if (!DBNull.Value.Equals(rdr["CreationDate"]))
                    mClass.DateCreation = (DateTime)rdr["CreationDate"];
                if (!DBNull.Value.Equals(rdr["ModificationUtilisateur"]))
                    mClass.UtilisateurModification = (string)rdr["ModificationUtilisateur"];
                if (!DBNull.Value.Equals(rdr["ModificationDate"]))
                    mClass.DateModification = (DateTime)rdr["ModificationDate"];
                if (!DBNull.Value.Equals(rdr["RowVersionKey"]))
                    mClass.RowVersionKey = rdr["RowVersionKey"];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nProduitType:MapFromDataReader");
            }
        }


        private static void MapFromDataReade2(ProduitType mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (int)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Designation"])) mClass._Designation = (string)mDataReader["Designation"];

                    //mClass._DefautProduitType = new ProduitType();
                    //if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._DefautProduitType.ID = (int)mDataReader["ID"];
                    //if (!DBNull.Value.Equals(mDataReader["Nom"])) mClass._DefautProduitType.Nom = (string)mDataReader["Nom"];

                    mClass._ProduitFini = new ProduitFini();
                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ProduitFini.ID = (int)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Designation"])) mClass._ProduitFini.Designation = (string)mDataReader["Designation"];

                    //mClass._Provenance = new Provenance();
                    //if (!DBNull.Value.Equals(mDataReader["ProvenanceID"])) mClass._Provenance.ID = (int)mDataReader["ProvenanceID"];
                    //if (!DBNull.Value.Equals(mDataReader["ProvenanceNom"])) mClass._Provenance.Nom = (string)mDataReader["ProvenanceNom"];

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];

                    //if (!DBNull.Value.Equals(mDataReader["MagasinID"])) mClass._MagasinID = (int)mDataReader["MagasinID"];
                    //if (!DBNull.Value.Equals(mDataReader["MagasinNom"])) mClass._MagasinNom = (string)mDataReader["MagasinNom"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nProduitType:MapFromDataReader");
            }
        }
        #endregion


        public string AsString
        {
            get { return _Designation; }
        }

        //public int MagasinID
        //{
        //    get
        //    {
        //        return _MagasinID;
        //    }

        //    set
        //    {
        //        _MagasinID = value;
        //    }
        //}

        //public string MagasinNom
        //{
        //    get
        //    {
        //        return _MagasinNom;
        //    }

        //    set
        //    {
        //        _MagasinNom = value;
        //    }
        //}
    }

    public partial class ProduitTypeViewModel
    {
        public ProduitType _ProduitType { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
