using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Components.Data;

namespace Tms.Classes.Business.Sites
{
    public class PaiementChequeAgence : DataPersist
    {
        #region "Fields"
        private Guid _ID;
        private PrefinancementAgence _PrefinancementAgence;
        private DateTime _Date;
        private decimal _Montant;
        private string _Commentaire;
        private bool _Desactive;
        #endregion

        #region "Properties"
        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Text = "Prefinancement")]
        public PrefinancementAgence PrefinancementAgence
        {
            get { return _PrefinancementAgence; }
            set { _PrefinancementAgence = value; }
        }

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        [Column(Text = "Amount")]
        public decimal Montant
        {
            get { return _Montant; }
            set { _Montant = value; }
        }

        public string MontantAsString
        {
            get { return _Montant != 0 ? String.Format("{0:#,#}", _Montant).TrimStart() : string.Empty; }
        }

        [Column(Text = "Commentaire")]
        public string Commentaire
        {
            get { return _Commentaire; }
            set { _Commentaire = value; }
        }

        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desactive)
                    return 0; // 
                else
                    return 2; //                     
            }
        }
        #endregion

        #region Constructor
        public PaiementChequeAgence()
        {

        }

        public PaiementChequeAgence(Guid myID)
        {
            this.fnGet(myID);
        }
        #endregion

        #region "Methods"
        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V3_PrefinancementDecaisser_Activate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
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
                throw new Exception(ex.Message + "\r\n" + "V3_PrefinancementDecaisser:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V3_PrefinancementDecaisser_DeActivate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
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
                throw new Exception(ex.Message + "\r\n" + "V3_PrefinancementDecaisser:fnDeActivate");
            }
            return bolResult;
        }

        public bool fnRemove()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V3_PrefinancementDecaisser_Remove");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
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
                throw new Exception(ex.Message + "\r\n" + "V3_PrefinancementDecaisser:fnRemove");
            }
            return bolResult;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V3_PrefinancementDecaisser_Get", (Guid)Id);
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
            Guid PrefinancementID = Guid.Empty;
            return fnSelect(PrefinancementID,/* -1,*/ -1);
        }

        public List<DataPersist> fnSelect(Guid PrefinancementID, /*int BanqueID,*/ int IsDisabled)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_PrefinancementDecaisser_Select");
                db().AddInParameter(mCommande, "@PrefinancementID", SqlDbType.UniqueIdentifier, PrefinancementID);
                //db().AddInParameter(mCommande, "@BanqueID", SqlDbType.Int, BanqueID);
                db().AddInParameter(mCommande, "@IsDisabled", SqlDbType.Int, IsDisabled);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PaiementChequeAgence mClass = new PaiementChequeAgence();

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
                    mCommande = db().CreateStoredProcCommand("V3_PrefinancementDecaisser_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V3_PrefinancementDecaisser_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@PrefinancementID", SqlDbType.UniqueIdentifier, _PrefinancementAgence.ID);
                //if (_Banque != null)
                //    db().AddInParameter(mCommande, "@BanqueID", SqlDbType.Int, _Banque.ID);
                //else
                //    db().AddInParameter(mCommande, "@BanqueID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _Date);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);



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
                        //base.UpdateAuditFields();
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
                throw new Exception(ex.Message + "\r\n" + "PaiementCheque:fnUpdate");

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
                    mCommande = db().CreateStoredProcCommand("V3_PrefinancementDecaisser_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                    db().AddInParameter(mCommande, "@PrefinancementID", SqlDbType.UniqueIdentifier, _PrefinancementAgence.ID);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V3_PrefinancementDecaisser_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }


                //if (_Banque != null)
                //    db().AddInParameter(mCommande, "@BanqueID", SqlDbType.Int, _Banque.ID);
                //else
                //    db().AddInParameter(mCommande, "@BanqueID", SqlDbType.Int, DBNull.Value);
                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _Date);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);

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
                throw new Exception(ex.Message + "\r\n" + "V3_PrefinancementDecaisser:fnUpdate");

            }
            return Result;
        }
        #endregion

        #region "Private Members"
        public override string ToString()
        {
            throw new NotImplementedException();
        }
        private static void MapFromDataReader(PaiementChequeAgence mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["PrefinancementID"]))
                    {
                        mClass._PrefinancementAgence = new PrefinancementAgence();
                        mClass._PrefinancementAgence.ID = (Guid)mDataReader["PrefinancementID"];
                        mClass._PrefinancementAgence.Numero = (string)mDataReader["PrefinancementNumero"];
                    }

                    //if (!DBNull.Value.Equals(mDataReader["BanqueID"]))
                    //{
                    //    mClass._Banque = new Banque();
                    //    mClass._Banque.ID = (int)mDataReader["BanqueID"];
                    //    mClass._Banque.Designation = (string)mDataReader["BanqueDesignation"];
                    //    mClass._Banque.Nom = (string)mDataReader["BanqueNom"];
                    //}

                    if (!DBNull.Value.Equals(mDataReader["Date"])) mClass._Date = (DateTime)mDataReader["Date"];

                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];

                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._Montant = (decimal)mDataReader["Montant"];

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];


                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nV3_PrefinancementDecaisser:MapFromDataReader");
            }
        }

        #endregion
    }

    public partial class PaiementChequeAgenceViewModel
    {
        public PaiementChequeAgence _PaiementChequeAgence{ get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
