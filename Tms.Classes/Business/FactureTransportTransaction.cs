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
    public class FactureTransportTransaction : DataPersist
    {
        #region "Fields"

        private Guid _ID;
        private FactureTransport _FactureTransport;            
        private Guid _ElementID;
        //private string _ElementRef;
        private decimal _ElementSolde;
        private decimal _ElementTonnage;
        private string _Commentaire;                

        #endregion

        #region "Properties"

        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public FactureTransport FactureTransport
        {
            get { return _FactureTransport; }
            set { _FactureTransport = value; }
        }        

        public Guid ElementID
        {
            get { return _ElementID; }
            set { _ElementID = value; }
        }


        //public string ElementRef
        //{
        //    get { return _ElementRef; }
        //    set { _ElementRef = value; }
        //}


        public decimal ElementSolde
        {
            get { return _ElementSolde; }
            set { _ElementSolde = value; }
        }

        public decimal ElementTonnage
        {
            get { return _ElementTonnage; }
            set { _ElementTonnage = value; }
        }

        public string ElementSoldeToString
        {
            get { return _ID != null ? String.Format("{0:# ### ### ### ###}", _ElementSolde).TrimStart() : string.Empty; }            
        }

        public string Commentaire
        {
            get { return _Commentaire; }
            set { _Commentaire = value; }
        }

        //public decimal Montant
        //{
        //    get { return _Montant; }
        //    set { _Montant = value; }
        //}

        #endregion

        #region "Constructor"

        public FactureTransportTransaction()
        {

        }

        public FactureTransportTransaction(Guid myId)
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
                mDataReader = db().ExecuteReader("FactureTransportTransaction_Get", (Guid)Id);
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
            return fnSelect();
        }

        public List<DataPersist> fnSelect(string cropyear, int fournisseurID, DateTime? startdate, DateTime? enddate, int FactureTransporttype, int statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("FactureTransportTransaction_Select");
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.VarChar, cropyear);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, fournisseurID);
                db().AddInParameter(mCommande, "@startdate", SqlDbType.DateTime, startdate);
                db().AddInParameter(mCommande, "@enddate", SqlDbType.DateTime, enddate);
                db().AddInParameter(mCommande, "@FactureTransportType", SqlDbType.Int, FactureTransporttype);
                db().AddInParameter(mCommande, "@status", SqlDbType.Int, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FactureTransportTransaction mClass = new FactureTransportTransaction();

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

        public List<DataPersist> fnSelectByFactureTransport(Guid FactureTransportID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("FactureTransportTransaction_SelectByFacture");
                db().AddInParameter(mCommande, "@facturetransportID", SqlDbType.UniqueIdentifier, FactureTransportID);
             
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FactureTransportTransaction mClass = new FactureTransportTransaction();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectByFactureTransport");
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

                    mCommande = db().CreateStoredProcCommand("FactureTransportTransaction_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);                    
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("FactureTransportTransaction_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@factureTransportID", SqlDbType.UniqueIdentifier, _FactureTransport.ID);              
                db().AddInParameter(mCommande, "@objetID", SqlDbType.UniqueIdentifier, _ElementID);
                //db().AddInParameter(mCommande, "@objetReference", SqlDbType.VarChar, _ElementRef);
                db().AddInParameter(mCommande, "@montant", SqlDbType.Money, _ElementSolde);
                db().AddInParameter(mCommande, "@tonnage", SqlDbType.Decimal, _ElementTonnage);

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
                throw new Exception(ex.Message + "\r\n" + "FactureTransportTransaction:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("FactureTransportTransaction_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("FactureTransportTransaction_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@factureTransportID", SqlDbType.UniqueIdentifier, _FactureTransport.ID);                
                db().AddInParameter(mCommande, "@objetID", SqlDbType.UniqueIdentifier, _ElementID);
                //db().AddInParameter(mCommande, "@objetReference", SqlDbType.VarChar, _ElementRef);
                db().AddInParameter(mCommande, "@montant", SqlDbType.Money, _ElementSolde);
                db().AddInParameter(mCommande, "@tonnage", SqlDbType.Decimal, _ElementTonnage);

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
                throw new Exception(ex.Message + "\r\n" + "FactureTransportTransaction:fnUpdate");

            }
            return Result;
        }


        public override bool fnActivate()
        {

            throw new NotImplementedException();

        }

        public override bool fnDeActivate()
        {

            throw new NotImplementedException();

        }


        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return string.Empty;
        }

        private static void MapFromDataReader(FactureTransportTransaction mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._FactureTransport = new FactureTransport();
                    if (!DBNull.Value.Equals(mDataReader["FactureTransportID"])) mClass._FactureTransport.ID = (Guid)mDataReader["FactureTransportID"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._FactureTransport.Commentaire = (string)mDataReader["Commentaire"];                    

                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._ElementSolde = (decimal)mDataReader["Montant"];
                    if (!DBNull.Value.Equals(mDataReader["Tonnage"])) mClass._ElementTonnage = (decimal)mDataReader["Tonnage"];
                    //if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._ElementRef = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["objetID"])) mClass._ElementID = (Guid)mDataReader["objetID"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nFactureTransportTransaction:MapFromDataReader");
            }
        }
        #endregion


    }
}
