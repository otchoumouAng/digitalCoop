using Ext.Net.MVC;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Components.Data;
using System.Collections.Generic;

namespace Tms.Classes.Business
{
    public class RemboursementDirect : DataPersist
    {
        #region "Fields"

        private Guid _ID;
        private string _Campagne;
        private Fournisseur _Fournisseur;
        private RemboursementDirectType _RemboursementDirectType;
        private string _Numero;
        private DateTime _DateRemboursement;
        private decimal _Montant;
        private string _Reference;
        private Banque _Banque;
        private string _Commentaire;
        private string _Statut;
        private bool _Desactive;
        protected string _UtilisateurApprobation;
        protected DateTime? _DateApprobation;
        private Site _Sites;
        #endregion

        #region "Properties"
        [ModelField(IDProperty = true, SortType = Ext.Net.SortTypeMethod.None, SortDir = Ext.Net.SortDirection.ASC)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }


        public string Campagne
        {
            get { return _Campagne; }
            set { _Campagne = value; }
        }

        public Fournisseur Fournisseur
        {
            get { return _Fournisseur; }
            set { _Fournisseur = value; }
        }

        public string FournisseurNameAndCode
        {
            get { return _Fournisseur.Nom + " - " + _Fournisseur.ID; }

        }

        public RemboursementDirectType RemboursementDirectType
        {
            get { return _RemboursementDirectType; }
            set { _RemboursementDirectType = value; }
        }

        public string LibelleRemboursementDirectType
        {
            get { return _RemboursementDirectType != null ? _RemboursementDirectType.Designation : string.Empty; }

        }

        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }


        public DateTime DateRemboursement
        {
            get { return _DateRemboursement; }
            set { _DateRemboursement = value; }
        }

        public string DateRemboursementAsString
        {
            get { return _DateRemboursement != null ? _DateRemboursement.ToShortDateString() : ((DateTime?)null).ToString(); }
        }

        public decimal Montant
        {
            get { return _Montant; }
            set { _Montant = value; }
        }

        public string MontantAsString
        {
            get { return _Montant != 0 ? String.Format("{0:#,#}", _Montant).TrimStart() : string.Empty; }
        }

        public decimal MontantIfIsActive
        {
            get { return IsActive ? _Montant : 0; }
        }

        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }


        public Banque Banque
        {
            get { return _Banque; }
            set { _Banque = value; }
        }

        public string Commentaire
        {
            get { return _Commentaire; }
            set { _Commentaire = value; }
        }

        public string Statut
        {
            get { return _Statut; }
            set { _Statut = value; }
        }

        public bool IsCancelled
        {
            get { return _Statut == "CA"; }

        }

        public bool IsApproved
        {
            get { return _Statut == "AP"; }

        }
        public bool IsActive
        {
            get { return _Statut == "NA"; }

        }

        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
        }

        [Column(Ignore = true)]
        public string AsString
        {
            get { return _Numero; }
        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Statut == "CA")
                    return 0; // BulletCross
                else if (_Statut == "AP")
                   return 1; // Tick               
                else
                    return 2; //                     
            }
        }

        public string UtilisateurApprobation
        {
            get { return _UtilisateurApprobation; }
            set { _UtilisateurApprobation = value; }
        }       

        public DateTime? DateApprobation
        {
            get { return _DateApprobation; }
            set { _DateApprobation = value; }
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

        public string SiteAsString
        {
            get { return _Sites != null ? _Sites.Nom : string.Empty; }

        }

        public string BanqueAsString
        {
            get { return _Banque != null ? _Banque.Designation : string.Empty; }

        }
        #endregion

        #region Constructor
        public RemboursementDirect()
        {

        }

        public RemboursementDirect(Guid myID)
        {
            this.fnGet(myID);
        }
        #endregion

        #region Methods
        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("RemboursementDirect_Get", (Guid)Id);
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

        public bool fnGetByNumber(int Number)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("RemboursementDirect_GetByNumber", (int)Number);
                if (mDataReader.Read())
                {
                    MapFromDataReader(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetByNumber");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public override List<DataPersist> fnSelect()
        {
            return fnSelect("", -1, -1, null, null, "-1",-1);
        }

        public List<DataPersist> fnSelect(string Campagne, int FournisseurID, int TypeID, DateTime? StartDate, DateTime? EndDate, string statut, int siteID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("RemboursementDirect_Select");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@TypeID", SqlDbType.Int, TypeID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@Status", SqlDbType.VarChar, 2, statut);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    RemboursementDirect mClass = new RemboursementDirect();
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

                    mCommande = db().CreateStoredProcCommand("RemboursementDirect_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 9);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("RemboursementDirect_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, _Sites.ID);
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, _Campagne);
                db().AddInParameter(mCommande, "@TypeId", SqlDbType.Int, _RemboursementDirectType.ID);
                db().AddInParameter(mCommande, "@FournisseurID", SqlDbType.Int, _Fournisseur.ID);
                db().AddInParameter(mCommande, "@BanqueID", SqlDbType.Int, _Banque.ID);
                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _DateRemboursement);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@Reference", SqlDbType.VarChar,50, _Reference);
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
                        base.UpdateAuditFields();
                        Result = true;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        _ID = (Guid)db().Parameters(mCommande, "@ID");
                        if (this._isnew)
                        {
                            _Numero = (string)db().Parameters(mCommande, "@Numero");
                            _Statut = "NA";
                        }

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
                throw new Exception(ex.Message + "\r\n" + "RemboursementDirect:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("RemboursementDirect_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 9);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("RemboursementDirect_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, _Sites.ID);
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, _Campagne);
                db().AddInParameter(mCommande, "@TypeId", SqlDbType.Int, _RemboursementDirectType.ID);
                db().AddInParameter(mCommande, "@FournisseurID", SqlDbType.Int, _Fournisseur.ID);
                if (_Banque != null) db().AddInParameter(mCommande, "@BanqueID", SqlDbType.Int, _Banque.ID);
                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _DateRemboursement);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@Reference", SqlDbType.VarChar, 50, _Reference);
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
                        if (this._isnew)
                        {
                            _Numero = (string)db().Parameters(mCommande, "@Numero");
                            _Statut = "NA";
                        }

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
                throw new Exception(ex.Message + "\r\n" + "RemboursementDirect:fnUpdate");

            }
            return Result;
        }

        public override bool fnActivate()
        {
            return false;
        }

        public override bool fnDeActivate()
        {
            return false;
        }

        public bool fnCancel()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("RemboursementDirect_Cancel");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
            db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
            db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
            db().AddInParameter(mCommande, "@CancelUser", SqlDbType.VarChar, _UtilisateurModification);
            try
            {
                db().ExecuteNonQuery(ref mCommande);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        bolResult = true;
                        _Statut = "CA";
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
                throw new Exception(ex.Message + "\r\n" + "RemboursementDirect:fnCancel");
            }
            return bolResult;
        }
       
        public bool fnApprove()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("RemboursementDirect_Approve");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
            db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
            db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
            db().AddInParameter(mCommande, "@ApprovalUser", SqlDbType.VarChar, _UtilisateurApprobation);
            try
            {
                db().ExecuteNonQuery(ref mCommande);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        bolResult = true;
                        _Statut = "AP";
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
                throw new Exception(ex.Message + "\r\n" + "RemboursementDirect:fnApprove");
            }
            return bolResult;
        }

      
        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _Numero;
        }

        private static void MapFromDataReader(RemboursementDirect mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Campagne"])) mClass._Campagne = (string)mDataReader["Campagne"];
                    if (!DBNull.Value.Equals(mDataReader["FournisseurID"]))
                    {
                        mClass._Fournisseur = new Fournisseur();
                        mClass._Fournisseur.ID = (int)mDataReader["FournisseurID"];
                        mClass._Fournisseur.Nom = (string)mDataReader["FournisseurNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["TypeID"]))
                    {
                        mClass._RemboursementDirectType = new RemboursementDirectType();
                        mClass._RemboursementDirectType.ID = (int)mDataReader["TypeID"];
                        mClass._RemboursementDirectType.Designation = (string)mDataReader["TypeNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["BanqueID"]))
                    {
                        mClass._Banque = new Banque();
                        mClass._Banque.ID = (int)mDataReader["BanqueID"];
                        mClass._Banque.Designation = (string)mDataReader["BanqueNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["SiteID"]))
                    {
                        mClass._Sites = new Site();
                        mClass._Sites.ID = (int)mDataReader["SiteID"];
                        mClass._Sites.Nom = (string)mDataReader["SiteNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DateRemboursement"])) mClass._DateRemboursement = (DateTime)mDataReader["DateRemboursement"];
                    if (!DBNull.Value.Equals(mDataReader["Reference"])) mClass._Reference = (string)mDataReader["Reference"];
                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._Montant = (decimal)mDataReader["Montant"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];
                    if (!DBNull.Value.Equals(mDataReader["ApprobationUtilisateur"])) mClass._UtilisateurApprobation = (string)mDataReader["ApprobationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["ApprobationDate"])) mClass._DateApprobation = (DateTime)mDataReader["ApprobationDate"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nRemboursementDirect:MapFromDataReader");
            }
        }


        #endregion

    }
    public partial class RemboursementDirectViewModel
    {
        public RemboursementDirect _RemboursementDirect { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
