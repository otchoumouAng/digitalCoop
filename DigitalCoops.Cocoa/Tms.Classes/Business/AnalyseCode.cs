using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Components.Data;

namespace Tms.Classes.Business
{
    //[Proxy(Read = "~/DeliveryCoding/SelectCode")]
    //[JsonReader(RootProperty = "data")]
    public class AnalyseCode : DataPersist
    {
        #region "Fields"

        private Guid _ID;
        private Livraison _Livraison;
        private DateTime _DateCode;
        private string _Code;
        private bool _Desactive;        
        private bool _EstInterne;

        #endregion

        #region "Properties"

        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string IDasString
        {
            get { return _ID.ToString(); }           
        }

        public Livraison Livraison
        {
            get { return _Livraison; }
            set { _Livraison = value; }
        }        

        public DateTime DateCode
        {
            get { return _DateCode; }
            set { _DateCode = value; }
        }

        public string DateCodeAsString
        {
            get { return _DateCode != null ? _DateCode.ToString() : string.Empty; }            
        }


        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
        }
        
        public string LivraisonType
        {
            get { return _Livraison == null ? string.Empty: _Livraison.LivraisonTypeAsString; }
        }

        public string LivraisonNumero
        {
            get { return _Livraison == null ? string.Empty : _Livraison.Numero; }
        }

        public string FournisseurName
        {
            get { return _Livraison == null ? string.Empty : _Livraison.FournisseurNameAsString; }
        }
        
        public string DateLivraison
        {
            get { return _Livraison == null ? string.Empty : _Livraison.DateLivraisonAsString; }
        }
     
        public string TruckID
        {
            get { return _Livraison == null ? string.Empty : _Livraison.TruckIDAsString; }
        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desactive)
                    return 0; // BulletCross
                else 
                    return 2; // Tick                
            }  
                  
        }

        public bool EstInterne
        {
            get
            {
                return _EstInterne;
            }

            set
            {
                _EstInterne = value;
            }
        }

        public string LibelleTypeCode
        {
            get { return _EstInterne ? "Int." : "Conces."; }
        }
        #endregion

        #region "Constructor"

        public AnalyseCode()
        {

        }

        public AnalyseCode(Guid myId)
        {
            this.fnGet(myId);
        }


        #endregion

        #region Methods
        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("AnalyseCode_Get", (Guid)Id);
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

        public bool fnGetByCode(string Code)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("AnalyseCodeByCode_Get", Code);
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
            return fnSelect(null, null, -1);
        }

        public List<DataPersist> fnSelect(DateTime? startdate, DateTime? enddate, int desactive)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;            
            
            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("AnalyseCode_Select");
                db().AddInParameter(mCommande, "@startdate", SqlDbType.DateTime, startdate);
                db().AddInParameter(mCommande, "@enddate", SqlDbType.DateTime, enddate);
                db().AddInParameter(mCommande, "@status", SqlDbType.Int, desactive);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    AnalyseCode mClass = new AnalyseCode();

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

        public List<DataPersist> fnSelectByDelivery(Guid id)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;


            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("AnalyseCodeByLivraison_Select");
                db().AddInParameter(mCommande, "@livraisonid", SqlDbType.UniqueIdentifier, id);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    AnalyseCode mClass = new AnalyseCode();

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

        public List<DataPersist> fnSelectCodeForAnalysePhysique(Guid? CodeAnalyseId, int mType = 0)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("AnalysePhysique_ListeCodeAnalyse_Select");
                db().AddInParameter(mCommande, "@CodeId", SqlDbType.UniqueIdentifier, CodeAnalyseId);
                db().AddInParameter(mCommande, "@CodeInterne", SqlDbType.Int, mType);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    AnalyseCode mClass = new AnalyseCode();

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


        public override bool fnUpdate()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {

                    mCommande = db().CreateStoredProcCommand("AnalyseCode_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("AnalyseCode_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@LivraisonID", SqlDbType.UniqueIdentifier, _Livraison.ID);
                db().AddInParameter(mCommande, "@DateCode", SqlDbType.DateTime, _DateCode);
                db().AddInParameter(mCommande, "@Code", SqlDbType.VarChar, _Code);                              
                db().AddInParameter(mCommande, "@EstInterne", SqlDbType.Bit, _EstInterne);

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
                throw new Exception(ex.Message + "\r\n" + "AnalyseCode:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("AnalyseCode_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("AnalyseCode_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@LivraisonID", SqlDbType.UniqueIdentifier, _Livraison.ID);
                db().AddInParameter(mCommande, "@DateCode", SqlDbType.DateTime, _DateCode);
                db().AddInParameter(mCommande, "@Code", SqlDbType.VarChar, _Code);
                db().AddInParameter(mCommande, "@EstInterne", SqlDbType.Bit, _EstInterne);

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
                throw new Exception(ex.Message + "\r\n" + "AnalyseCode:fnUpdate");

            }
            return Result;
        }


        public override bool fnActivate()
        {

            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("AnalyseCode_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "AnalyseCode:fnActivate");
            }
            return Result;

        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("AnalyseCode_DeActivate");
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
                        Desactive = true;
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
                throw new Exception(ex.Message + "\r\n" + "AnalyseCode:fnDeActivate");
            }
            return bolResult;
        }

        public bool fnDeActivate(DataTransaction mTran)
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("AnalyseCode_DeActivate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
            db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
            db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
            db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
            try
            {
                db().ExecuteNonQuery(ref mCommande, mTran);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        bolResult = true;
                        Desactive = true;
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
                throw new Exception(ex.Message + "\r\n" + "AnalyseCode:fnDeActivate");
            }
            return bolResult;
        }


        public bool fnRemove(Guid id)
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("AnalyseCode_Remove");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, id);
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
                throw new Exception(ex.Message + "\r\n" + "AnalyseCode:fnRemove");
            }
            return bolResult;
        }


        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _Code;
        }

        private static void MapFromDataReader(AnalyseCode mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    mClass._Livraison = new Livraison();
                    if (!DBNull.Value.Equals(mDataReader["LivraisonID"])) mClass._Livraison.ID = (Guid)mDataReader["LivraisonID"];
                    if (!DBNull.Value.Equals(mDataReader["TruckID"])) mClass._Livraison.Immatriculation = (string)mDataReader["TruckID"];
                    if (!DBNull.Value.Equals(mDataReader["DateLivraison"])) mClass._Livraison.DateLivraison = (DateTime)mDataReader["DateLivraison"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroLivraison"])) mClass._Livraison.Numero = (string)mDataReader["NumeroLivraison"];

                    if (!DBNull.Value.Equals(mDataReader["DateCode"])) mClass._DateCode = (DateTime)mDataReader["DateCode"];
                    if (!DBNull.Value.Equals(mDataReader["Code"])) mClass._Code = (string)mDataReader["Code"];                   
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["EstInterne"])) mClass._EstInterne = (bool)mDataReader["EstInterne"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                    
                    mClass._Livraison.Fournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["FournisseurID"])) mClass._Livraison.Fournisseur.ID = (int)mDataReader["FournisseurID"];
                    if (!DBNull.Value.Equals(mDataReader["NomFournisseur"])) mClass._Livraison.Fournisseur.Nom = (string)mDataReader["NomFournisseur"];

                    mClass._Livraison.LivraisonType = new LivraisonType();
                    if (!DBNull.Value.Equals(mDataReader["TypeLivraison"])) mClass._Livraison.LivraisonType.ID = (int)mDataReader["TypeLivraison"];
                    if (!DBNull.Value.Equals(mDataReader["DesignationTypeLivraison"])) mClass._Livraison.LivraisonType.Designation = (string)mDataReader["DesignationTypeLivraison"];



                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nAnalyseCode:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderLite(AnalyseCode mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];                    
                    
                    if (!DBNull.Value.Equals(mDataReader["Code"])) mClass._Code = (string)mDataReader["Code"];
                    if (!DBNull.Value.Equals(mDataReader["EstInterne"])) mClass._EstInterne = (bool)mDataReader["EstInterne"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nAnalyseCode:MapFromDataReader");
            }
        }

        #endregion

    }

    public partial class AnalyseCodeViewModel
    {
        public AnalyseCode _AnalyseCode { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
