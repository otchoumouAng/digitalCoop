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
    [Proxy(Read = "~/SpotPriceAgence/Select")]
    [JsonReader(RootProperty = "data")]
    public class PrixNegocieAgence : DataPersist
    {
        #region "Fields"

        private Guid _ID;
        private Site _Site;
        private Campagne _Campagne;
        private Fournisseur _Fournisseur;
        private string _Numero;
        private DateTime _DatePrix;
        private decimal _Prix;
        private PrixNegocieModeApplication _ModeApplication;
        private DateTime? _DateDebut;
        private DateTime? _DateEcheance;
        private string _Commentaire;
        private string _Statut;
        private bool _Desactive;

        #endregion

        #region "Properties"

        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Site Site
        {
            get { return _Site; }
            set { _Site = value; }
        }

        public string SiteAsString
        {
            get { return _Site != null ? _Site.Nom : string.Empty; }

        }

        [Column(Text = "Campagne")]
        public Campagne Campagne
        {
            get { return _Campagne; }
            set { _Campagne = value; }
        }

        public Fournisseur Fournisseur
        {
            get { return _Fournisseur; }
            set { _Fournisseur = value; }
        }

        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        public DateTime DatePrix
        {
            get { return _DatePrix; }
            set { _DatePrix = value; }
        }

        public decimal Prix
        {
            get { return _Prix; }
            set { _Prix = value; }
        }

        public PrixNegocieModeApplication ModeApplication
        {
            get { return _ModeApplication; }
            set { _ModeApplication = value; }
        }

        public DateTime? DateDebut
        {
            get { return _DateDebut; }
            set { _DateDebut = value; }
        }


        public DateTime? DateEcheance
        {
            get { return _DateEcheance; }
            set { _DateEcheance = value; }
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

        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
        }

        //[Column(Text = "AsString")]
        //public string AsString
        //{
        //    get { return _Fournisseur.Nom; }
        //}

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desactive)
                    return 0; // BulletCross  
                else if (_Statut == "AP")
                    return 1; // Tick              
                else
                    return 2; //                     
            }
        }
        #endregion

        #region Constructor
        public PrixNegocieAgence()
        {

        }

        public PrixNegocieAgence(Guid myID)
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
                mDataReader = db().ExecuteReader("PrixNegocie_Get", (Guid)Id);
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
            return fnSelect("{Tous}", -1, -1, null, null/*, "-1"*/);
        }

        public List<DataPersist> fnSelect(string cropYear, int siteID, int fournisseurID, DateTime? startdate, DateTime? enddate/*, string statut*/)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("PrixNegocie_Select");
                db().AddInParameter(mCommande, "@CropYear", SqlDbType.VarChar, cropYear);
                db().AddInParameter(mCommande, "@siteid", SqlDbType.Int, siteID);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, fournisseurID);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, startdate);
                db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, enddate);
                //db().AddInParameter(mCommande, "@status", SqlDbType.Int, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PrixNegocieAgence mClass = new PrixNegocieAgence();

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

                    mCommande = db().CreateStoredProcCommand("PrixNegocie_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("PrixNegocie_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, _Site.ID);
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.VarChar, _Campagne.Designation);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, _Fournisseur.ID);
                db().AddInParameter(mCommande, "@dateprix", SqlDbType.DateTime, _DatePrix);
                db().AddInParameter(mCommande, "@prix", SqlDbType.Money, _Prix);
                db().AddInParameter(mCommande, "@modeID", SqlDbType.Int, _ModeApplication.ID);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, _DateDebut);
                db().AddInParameter(mCommande, "@dateecheance", SqlDbType.DateTime, _DateEcheance);
                db().AddInParameter(mCommande, "@commentaire", SqlDbType.VarChar, _Commentaire);
                //db().AddInParameter(mCommande, "@statut", SqlDbType.Char, _Statut);                

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
                        if (this.IsNew)
                        {
                            _Numero = (string)db().Parameters(mCommande, "@Numero");
                        }

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
                throw new Exception(ex.Message + "\r\n" + "PrixNegocieAgence:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("PrixNegocie_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("PrixNegocie_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, _Site.ID);
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.VarChar, _Campagne.Designation);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, _Fournisseur.ID);
                db().AddInParameter(mCommande, "@dateprix", SqlDbType.DateTime, _DatePrix);
                db().AddInParameter(mCommande, "@prix", SqlDbType.Money, _Prix);
                db().AddInParameter(mCommande, "@modeID", SqlDbType.Int, _ModeApplication.ID);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, _DateDebut);
                db().AddInParameter(mCommande, "@dateecheance", SqlDbType.DateTime, _DateEcheance);
                db().AddInParameter(mCommande, "@commentaire", SqlDbType.VarChar, _Commentaire);
                //db().AddInParameter(mCommande, "@statut", SqlDbType.Char, _Statut);                

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
                        if (this.IsNew)
                        {
                            _Numero = (string)db().Parameters(mCommande, "@Numero");
                        }

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
                throw new Exception(ex.Message + "\r\n" + "PrixNegocieAgence:fnUpdate");

            }
            return Result;
        }


        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("PrixNegocie_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "PrixNegocieAgence:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("PrixNegocie_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "PrixNegocieAgence:fnDeActivate");
            }
            return bolResult;
        }

        public bool fnApprove()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("PrixNegocie_Approve");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
            db().AddOutParameter(mCommande, "@statut", SqlDbType.Char, 2);
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
                        //Desactive = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        _Statut = (string)db().Parameters(mCommande, "@statut");

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
                throw new Exception(ex.Message + "\r\n" + "PrixNegocie:fnApprove");
            }
            return Result;
        }

        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _Numero;
        }

        private static void MapFromDataReader(PrixNegocieAgence mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._Site = new Site();
                    if (!DBNull.Value.Equals(mDataReader["siteID"])) mClass._Site.ID = (int)mDataReader["siteID"];
                    if (!DBNull.Value.Equals(mDataReader["siteNom"])) mClass._Site.Nom = (string)mDataReader["siteNom"];

                    mClass._Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["Campagne"])) mClass._Campagne.Designation = (string)mDataReader["Campagne"];
                    if (!DBNull.Value.Equals(mDataReader["CampagneDebut"])) mClass._Campagne.DateDebut = (DateTime)mDataReader["CampagneDebut"];
                    if (!DBNull.Value.Equals(mDataReader["CampagneFin"])) mClass._Campagne.DateFin = (DateTime)mDataReader["CampagneFin"];

                    mClass._Fournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["fournisseurID"])) mClass._Fournisseur.ID = (int)mDataReader["fournisseurID"];

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["datePrix"])) mClass._DatePrix = (DateTime)mDataReader["datePrix"];
                    if (!DBNull.Value.Equals(mDataReader["prix"])) mClass._Prix = (decimal)mDataReader["prix"];

                    mClass._ModeApplication = new PrixNegocieModeApplication();
                    if (!DBNull.Value.Equals(mDataReader["modeID"])) mClass._ModeApplication.ID = (int)mDataReader["modeID"];

                    if (!DBNull.Value.Equals(mDataReader["datedebut"])) mClass._DateDebut = (DateTime)mDataReader["datedebut"];
                    if (!DBNull.Value.Equals(mDataReader["dateecheance"])) mClass._DateEcheance = (DateTime)mDataReader["dateecheance"];
                    if (!DBNull.Value.Equals(mDataReader["desactive"])) mClass._Desactive = (bool)mDataReader["desactive"];
                    if (!DBNull.Value.Equals(mDataReader["statut"])) mClass._Statut = (string)mDataReader["statut"];
                    if (!DBNull.Value.Equals(mDataReader["commentaire"])) mClass._Commentaire = (string)mDataReader["commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["fournisseurNom"])) mClass._Fournisseur.Nom = (string)mDataReader["fournisseurNom"];
                    if (!DBNull.Value.Equals(mDataReader["fournisseurCode"])) mClass._Fournisseur.CompteNumero = (string)mDataReader["fournisseurCode"];
                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                    if (!DBNull.Value.Equals(mDataReader["designationModeLivraison"])) mClass._ModeApplication.Designation = (string)mDataReader["designationModeLivraison"];


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nlbc_PrixNegocieAgence:MapFromDataReader");
            }
        }
        #endregion


    }
    public partial class PrixNegocieAgenceViewModel
    {
        public PrixNegocieAgence _PrixNegocieAgence { get; set; }

        public PrixNegocieLivraison _PrixNegocieLivraison { get; set; }

        public Parametres _Parametres { get; set; }

        public int _SiteParDefaut { get; set; }

        public PrixNegocieAgenceParamViewModel _PrixNegocieAgenceParam { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }

    public partial class PrixNegocieAgenceParamViewModel
    {
        public string TolerancePrixNegocie { get; set; }

        public string PrixJournalier { get; set; }

    }
}
