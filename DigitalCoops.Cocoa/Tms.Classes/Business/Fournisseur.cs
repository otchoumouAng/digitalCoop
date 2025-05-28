using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Components.Data;
using Ext.Net.MVC;
using System.Data;

namespace Tms.Classes.Business
{
    //[Proxy(Read ="~/Fournisseur/Select", Create = "POST")]      
    //[JsonReader(RootProperty ="data")]       
    public class Fournisseur: DataPersist
    {
        #region "Fields"

        private int _ID;
        private string _Nom;
        private FournisseurGroupe _FournisseurGroupe;
        private FournisseurType _FournisseurType;
        private Provenance _Provenance;
        private string _PieceNumero;
        private PieceType _PieceType;
        private Site _Agence;
        private string _Adresse;
        private string _TelephoneFixe;
        private string _TelephoneMobile;
        private string _Fax;
        private string _eMail;
        private string _CompteNumero;
        private Certification _Certification;
        private string _NumAgrement;
        private string _SiegeSocial;
        private string _NumeroCC;
        private string _NumeroRegistre;
        private decimal? _Capital;
        private string _FloId;
        private bool _Desative;

        private bool _IsApproved;
        private Site _SiteParDefaut;
        private bool _EstSectorLead;
        #endregion

        #region "Properties"
        [Column(Text = "Code")]
        [ModelField(IDProperty = true,SortType = Ext.Net.SortTypeMethod.AsInt, SortDir = Ext.Net.SortDirection.ASC)]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int Code
        {
            get { return _ID != 0 ? _ID : 0; }            
        }

        [Column( Text = "Name" )]        
        public string Nom
        {
            get { return _Nom; }
            set { _Nom = value; }
        }

        [Column(Text = "Groupe",Hidden =true)]
        public FournisseurGroupe FournisseurGroupe
        {
            get { return _FournisseurGroupe; }
            set { _FournisseurGroupe = value; }
        }


        [Column(Text = "Type", Hidden = true)]
        public FournisseurType FournisseurType
        {
            get { return _FournisseurType; }
            set { _FournisseurType = value; }
        }

        public int? FournisseurTypeID
        {
            get { return FournisseurType != null ? _FournisseurType.ID : (int?)null; }            
        }

        public int? DefaultAgenceFournisseurID
        {
            get { return _Agence != null ? _Agence.ID : (int?)null; }
        }


        [Column(Text = "Provenance", Hidden = true)]
        public string ProvenanceAsString
        {
            get { return _Provenance != null ?  _Provenance.Nom : string.Empty; }
            //set { _Provenance = value; }
        }

        public Provenance Provenance
        {
            get { return _Provenance; }
            set { _Provenance = value; }
        }


        [Column(Text = "Numero Pièce", Hidden = true)]
        public string PieceNumero
        {
            get { return _PieceNumero; }
            set { _PieceNumero = value; }
        }

        [Column(Text = "Type Pièce", Hidden = true)]
        public PieceType PieceType
        {
            get { return _PieceType; }
            set { _PieceType = value; }
        }


        [Column(Text = "Adresse")]
        public string Adresse
        {
            get { return _Adresse; }
            set { _Adresse = value; }
        }

        [Column(Text = "Telephone Fixe", Hidden = true)]
        public string TelephoneFixe
        {
            get { return _TelephoneFixe; }
            set { _TelephoneFixe = value; }
        }

        [Column(Text = "Telephone Mobile")]
        public string TelephoneMobile
        {
            get { return _TelephoneMobile; }
            set { _TelephoneMobile = value; }
        }


        [Column(Text = "Fax", Hidden = true)]
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }

        [Column(Text = "Email", Hidden = true)]
        public string eMail
        {
            get { return _eMail; }
            set { _eMail = value; }
        }

        [Column(Text = "Numéro Compte", Hidden = true)]
        public string CompteNumero
        {
            get { return _CompteNumero; }
            set { _CompteNumero = value; }
        }

        [Column(Text = "Certification")]
        public string CertificationAsString
        {
            get { return _Certification != null ? _Certification.Designation : string.Empty; }
            //set { _Certification = value; }
        }
        public Certification Certification
        {
            get { return _Certification; }
            set { _Certification = value; }
        }

        [Column(Text= "Desactive")]
        public bool Desactive
        {
            get { return _Desative; }
            set { _Desative = value; }
        }


        [Column(Text = "IsApproved")]
        public bool IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }


        [Column(Text = "AsString")]
        public string AsString
        {
            get { return _Nom; }
        }

        [Column(Text = "")]
        public int mIcon
        {
            get 
            {
                if (_Desative)
                    return 0; // BulletCross
                else if (_IsApproved)
                    return 1; // Tick
                else
                    return 2; //                     
            }
        }

        public string NameAndCode
        {
            get {
                if (_ID != -1)
                    return _Nom != null ? _Nom + " - " + _ID : string.Empty;
                else
                    return _Nom != null ? _Nom : string.Empty;                         
            }            
        }

        public string FournisseurNameAndCode
        {
            get 
            {
                if (_Nom != null)
                {
                    if (_Nom.Contains(_ID.ToString()))
                    {
                        return _Nom;
                    }
                    if (!Nom.Equals(string.Empty) && this.ID != 0)
                        return (this.ID > 0) ? this.Nom + " - " + this.ID : this.Nom;
                    else
                        return string.Empty;
                }
                else
                {
                    return string.Empty;
                }
            }

        }

        public string FournisseurGroupeAsString
        {
            get { return _FournisseurGroupe != null ? _FournisseurGroupe.Designation : string.Empty; }            
        }

        public string FournisseurTypeAsString
        {
            get { return _FournisseurType != null ? _FournisseurType.Designation : string.Empty; }            
        }

        public string NumAgrement
        {
            get
            {
                return _NumAgrement;
            }

            set
            {
                _NumAgrement = value;
            }
        }

        public string SiegeSocial
        {
            get
            {
                return _SiegeSocial;
            }

            set
            {
                _SiegeSocial = value;
            }
        }

        public string NumeroCC
        {
            get
            {
                return _NumeroCC;
            }

            set
            {
                _NumeroCC = value;
            }
        }

        public string NumeroRegistre
        {
            get
            {
                return _NumeroRegistre;
            }

            set
            {
                _NumeroRegistre = value;
            }
        }

        public decimal? Capital
        {
            get
            {
                return _Capital;
            }

            set
            {
                _Capital = value;
            }
        }

        public string CapitalAsString
        {
            get { return _Capital != 0 ? String.Format("{0:#,#}", _Capital).TrimStart() : string.Empty; }
        }

        public string FloId
        {
            get
            {
                return _FloId;
            }

            set
            {
                _FloId = value;
            }
        }

        public Site Agence
        {
            get
            {
                return _Agence;
            }

            set
            {
                _Agence = value;
            }
        }

        public string AgenceAsString
        {
            get{ return (Agence != null) ? Agence.Nom : string.Empty; }
        }

        public Site SiteParDefaut
        {
            get
            {
                return _SiteParDefaut;
            }

            set
            {
                _SiteParDefaut = value;
            }
        }

        public bool EstSectorLead
        {
            get
            {
                return _EstSectorLead;
            }

            set
            {
                _EstSectorLead = value;
            }
        }
        #endregion

        #region "Constructor"

        public Fournisseur()
        {

        }

        public Fournisseur(int myId)
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
                mDataReader = db().ExecuteReader("Fournisseur_Get", (int)Id);
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
            return fnSelect(-1,-1,-1,-1,0);
        }

        public List<DataPersist> fnSelect(int SupplierGroupID, int  SupplierTypeID,int CertifiedID, int IsDisabled, int IsApproved = -1, int AgenceID = -1, int OtherSite = 0)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {                
                DataCommand mCommande = db().CreateStoredProcCommand("Fournisseur_Select");                                
                db().AddInParameter(mCommande, "@SupplierGroupID", SqlDbType.Int, SupplierGroupID);
                db().AddInParameter(mCommande, "@SupplierTypeID", SqlDbType.Int, SupplierTypeID);
                db().AddInParameter(mCommande, "@CertifiedID", SqlDbType.Int, CertifiedID);
                db().AddInParameter(mCommande, "@IsDisabled", SqlDbType.Int, IsDisabled);
                db().AddInParameter(mCommande, "@IsApproved", SqlDbType.Int, IsApproved);
                //db().AddInParameter(mCommande, "@AgenceID", SqlDbType.Int, AgenceID);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, AgenceID);
                db().AddInParameter(mCommande, "@VoirAutreFournisseur", SqlDbType.Int, OtherSite);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Fournisseur mClass = new Fournisseur();

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

        public List<DataPersist> fnSelectForFinancing(int SiteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Fournisseur_SelectForFinancing");
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, SiteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Fournisseur mClass = new Fournisseur();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectForFinancing");
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

                    mCommande = db().CreateStoredProcCommand("Fournisseur_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.Int, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("Fournisseur_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@nom", SqlDbType.VarChar, _Nom);
                db().AddInParameter(mCommande, "@groupeid", SqlDbType.Int, _FournisseurGroupe.ID);
                db().AddInParameter(mCommande, "@typeid", SqlDbType.Int, _FournisseurType.ID);
                db().AddInParameter(mCommande, "@provenance", SqlDbType.Int, _Provenance.ID);
                db().AddInParameter(mCommande, "@piecenumero", SqlDbType.VarChar, _PieceNumero);

                if (_PieceType != null)
                    db().AddInParameter(mCommande, "@piecetype", SqlDbType.Int, _PieceType.ID);
                else
                    db().AddInParameter(mCommande, "@piecetype", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@adresse", SqlDbType.VarChar, _Adresse);
                db().AddInParameter(mCommande, "@telephonefixe", SqlDbType.VarChar, _TelephoneFixe);
                db().AddInParameter(mCommande, "@telephonemobile", SqlDbType.VarChar, _TelephoneMobile);
                db().AddInParameter(mCommande, "@fax", SqlDbType.VarChar, _Fax);
                db().AddInParameter(mCommande, "@email", SqlDbType.VarChar, _eMail);
                if (!string.IsNullOrEmpty(_CompteNumero))
                    db().AddInParameter(mCommande, "@comptenumero", SqlDbType.VarChar, _CompteNumero);
                else
                    db().AddInParameter(mCommande, "@comptenumero", SqlDbType.VarChar, DBNull.Value);

                db().AddInParameter(mCommande, "@numagrement", SqlDbType.VarChar, _NumAgrement);
                db().AddInParameter(mCommande, "@siegesocial", SqlDbType.VarChar, _SiegeSocial);
                db().AddInParameter(mCommande, "@numcc", SqlDbType.VarChar, _NumeroCC);
                db().AddInParameter(mCommande, "@numregistre", SqlDbType.VarChar, _NumeroRegistre);
                db().AddInParameter(mCommande, "@capital", SqlDbType.Money, _Capital);
                db().AddInParameter(mCommande, "@floId", SqlDbType.VarChar, _FloId);

                if (_Certification != null)
                    db().AddInParameter(mCommande, "@certification", SqlDbType.Int, _Certification.ID);
                else
                    db().AddInParameter(mCommande, "@certification", SqlDbType.Int, DBNull.Value);

                if (_Agence != null)
                    //db().AddInParameter(mCommande, "@agenceid", SqlDbType.Int, _Agence.ID);
                    db().AddInParameter(mCommande, "@agenceid", SqlDbType.Int, _Agence.ID);
                else
                    db().AddInParameter(mCommande, "@agenceid", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, _Agence.ID);

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
                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        _ID = (int)db().Parameters(mCommande, "@ID");
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
                throw new Exception(ex.Message + "\r\n" + "Tms_Fournisseur:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("Fournisseur_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.Int, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("Fournisseur_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@nom", SqlDbType.VarChar, _Nom);
                db().AddInParameter(mCommande, "@groupeid", SqlDbType.Int, _FournisseurGroupe.ID);
                db().AddInParameter(mCommande, "@typeid", SqlDbType.Int, _FournisseurType.ID);
                db().AddInParameter(mCommande, "@provenance", SqlDbType.Int, _Provenance.ID);
                db().AddInParameter(mCommande, "@piecenumero", SqlDbType.VarChar, _PieceNumero);

                if (_PieceType != null)
                    db().AddInParameter(mCommande, "@piecetype", SqlDbType.Int, _PieceType.ID);
                else
                    db().AddInParameter(mCommande, "@piecetype", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@adresse", SqlDbType.VarChar, _Adresse);
                db().AddInParameter(mCommande, "@telephonefixe", SqlDbType.VarChar, _TelephoneFixe);
                db().AddInParameter(mCommande, "@telephonemobile", SqlDbType.VarChar, _TelephoneMobile);
                db().AddInParameter(mCommande, "@fax", SqlDbType.VarChar, _Fax);
                db().AddInParameter(mCommande, "@email", SqlDbType.VarChar, _eMail);

                if (!string.IsNullOrEmpty(_CompteNumero))
                    db().AddInParameter(mCommande, "@comptenumero", SqlDbType.VarChar, _CompteNumero);
                else
                    db().AddInParameter(mCommande, "@comptenumero", SqlDbType.VarChar, DBNull.Value);

                db().AddInParameter(mCommande, "@numagrement", SqlDbType.VarChar, _NumAgrement);
                db().AddInParameter(mCommande, "@siegesocial", SqlDbType.VarChar, _SiegeSocial);
                db().AddInParameter(mCommande, "@numcc", SqlDbType.VarChar, _NumeroCC);
                db().AddInParameter(mCommande, "@numregistre", SqlDbType.VarChar, _NumeroRegistre);
                db().AddInParameter(mCommande, "@capital", SqlDbType.Money, _Capital);
                db().AddInParameter(mCommande, "@floId", SqlDbType.VarChar, _FloId);

                if (_Certification != null)
                    db().AddInParameter(mCommande, "@certification", SqlDbType.Int, _Certification.ID);
                else
                    db().AddInParameter(mCommande, "@certification", SqlDbType.Int, DBNull.Value);

                if (_Agence != null)
                    db().AddInParameter(mCommande, "@agenceid", SqlDbType.Int, _Agence.ID);
                //db().AddInParameter(mCommande, "@agenceid", SqlDbType.Int, _Agence.ID);
                else
                    db().AddInParameter(mCommande, "@agenceid", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, _Agence.ID);

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
                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        _ID = (int)db().Parameters(mCommande, "@ID");
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
                throw new Exception(ex.Message + "\r\n" + "Tms_Fournisseur:fnUpdate");

            }
            return Result;
        }


        public override bool fnActivate()
        {
            if (!this._isnew)
            {
                bool Result;
                DataCommand mCommande = db().CreateStoredProcCommand("Fournisseur_Activate");
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
                    throw new Exception(ex.Message + "\r\n" + "Tms_Fournisseur:fnActivate");
                }
                return Result;
            }
            return false;
        }

        public override bool fnDeActivate()
        {
            if (!this._isnew)
            {
                bool bolResult;
                DataCommand mCommande = db().CreateStoredProcCommand("Fournisseur_DeActivate");
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
                            bolResult = true;
                            Desactive  = true;
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
                    throw new Exception(ex.Message + "\r\n" + "Tms_Fournisseur:fnDeActivate");
                }
                return bolResult;
            }
            return false;
        }


        public  bool fnApprove()
        {
            if (!this._IsApproved)
            {
                bool Result;
                DataCommand mCommande = db().CreateStoredProcCommand("Fournisseur_Approve");
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
                            IsApproved = true;
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
                    throw new Exception(ex.Message + "\r\n" + "Fournisseur:fnApprove");
                }
                return Result;
            }
            return false;
        }

        public List<DataPersist> fnSelectFromDelivery(int siteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Payement_SelectFournisseurFromPurchase");
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Fournisseur mClass = new Fournisseur();

                    MapFromDataReaderLite(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectFromDelivery");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectFromFinancing(int siteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Payement_SelectFournisseurFromFinancing");
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Fournisseur mClass = new Fournisseur();

                    MapFromDataReaderLite(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectFromFinancing");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectFromSaving(int siteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Payement_SelectFournisseurFromSaving");
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Fournisseur mClass = new Fournisseur();

                    MapFromDataReaderLite(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectFromSaving");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectFromPrime(int siteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Payement_SelectFournisseurFromPrime");
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Fournisseur mClass = new Fournisseur();

                    MapFromDataReaderLite(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectFromSaving");
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

        private static void MapFromDataReader(Fournisseur mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (int)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Nom"])) mClass._Nom = (string)mDataReader["Nom"];
                    //if (!DBNull.Value.Equals(mDataReader["IsDisabled"])) mClass. = (bool)mDataReader["IsDisabled"];

                    if (!DBNull.Value.Equals(mDataReader["PieceNumero"])) mClass._PieceNumero = (string)mDataReader["PieceNumero"];
                    if (!DBNull.Value.Equals(mDataReader["Adresse"])) mClass._Adresse = (string)mDataReader["Adresse"];
                    if (!DBNull.Value.Equals(mDataReader["TelephoneFixe"])) mClass._TelephoneFixe = (string)mDataReader["TelephoneFixe"];
                    if (!DBNull.Value.Equals(mDataReader["TelephoneMobile"])) mClass._TelephoneMobile = (string)mDataReader["TelephoneMobile"];
                    if (!DBNull.Value.Equals(mDataReader["Fax"])) mClass._Fax = (string)mDataReader["Fax"];
                    if (!DBNull.Value.Equals(mDataReader["Email"])) mClass._eMail = (string)mDataReader["Email"];
                    if (!DBNull.Value.Equals(mDataReader["CompteNumero"])) mClass._CompteNumero = (string)mDataReader["CompteNumero"];
                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                    mClass._FournisseurGroupe = new FournisseurGroupe();
                    if (!DBNull.Value.Equals(mDataReader["GroupeID"])) mClass._FournisseurGroupe.ID = (int)mDataReader["GroupeID"];

                    mClass._FournisseurType = new FournisseurType();
                    if (!DBNull.Value.Equals(mDataReader["TypeID"])) mClass._FournisseurType.ID = (int)mDataReader["TypeID"];

                    mClass._Provenance = new Provenance();
                    if (!DBNull.Value.Equals(mDataReader["ProvenanceID"])) mClass._Provenance.ID = (int)mDataReader["ProvenanceID"];

                    mClass._PieceType = new PieceType();
                    if (!DBNull.Value.Equals(mDataReader["PieceTypeID"])) mClass._PieceType.ID = (int)mDataReader["PieceTypeID"];

                    mClass._Agence = new Site();
                    if (!DBNull.Value.Equals(mDataReader["AgenceID"])) mClass._Agence.ID = (int)mDataReader["AgenceID"];
                    if (!DBNull.Value.Equals(mDataReader["NomAgence"])) mClass._Agence.Nom = (string)mDataReader["NomAgence"];

                    if (!DBNull.Value.Equals(mDataReader["NumeroAgrement"])) mClass._NumAgrement = (string)mDataReader["NumeroAgrement"];
                    if (!DBNull.Value.Equals(mDataReader["SiegeSocial"])) mClass._SiegeSocial = (string)mDataReader["SiegeSocial"];
                    if (!DBNull.Value.Equals(mDataReader["NumCC"])) mClass._NumeroCC = (string)mDataReader["NumCC"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroRegistre"])) mClass._NumeroRegistre = (string)mDataReader["NumeroRegistre"];
                    if (!DBNull.Value.Equals(mDataReader["MontantCapital"])) mClass._Capital = (decimal?)mDataReader["MontantCapital"];
                    if (!DBNull.Value.Equals(mDataReader["FloId"])) mClass._FloId = (string)mDataReader["FloId"];

                    mClass._Certification = new Certification();
                    //mClass._Certification.Designation = string.Empty;
                    if (!DBNull.Value.Equals(mDataReader["CertificationID"])) mClass._Certification.ID = (int)mDataReader["CertificationID"];
                    if (!DBNull.Value.Equals(mDataReader["GroupDesignation"])) mClass._FournisseurGroupe.Designation = (string)mDataReader["GroupDesignation"];
                    if (!DBNull.Value.Equals(mDataReader["TypeDesignation"])) mClass._FournisseurType.Designation = (string)mDataReader["TypeDesignation"];
                    if (!DBNull.Value.Equals(mDataReader["ProvenanceName"])) mClass._Provenance.Nom = (string)mDataReader["ProvenanceName"];
                    if (!DBNull.Value.Equals(mDataReader["CertificationDesignation"])) mClass._Certification.Designation = (string)mDataReader["CertificationDesignation"];
                    if (!DBNull.Value.Equals(mDataReader["PieceTypeDesignation"])) mClass._PieceType.Designation = (string)mDataReader["PieceTypeDesignation"];
                    
                    if (!DBNull.Value.Equals(mDataReader["IsApproved"])) mClass._IsApproved = (bool)mDataReader["IsApproved"];
                    if (!DBNull.Value.Equals(mDataReader["IsDisabled"])) mClass._Desative = (bool)mDataReader["IsDisabled"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nFournisseur:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderLite(Fournisseur mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (int)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Nom"])) mClass._Nom = (string)mDataReader["Nom"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nFournisseur:MapFromDataReader");
            }
        }

        #endregion

        #region "Static Member"
        public static string SortPropertyName = "Nom";
        public static string IdPropertyName = "ID";

        public static string DisplayProperty = "Nom";
        public static string valueProperty = "ID";
        #endregion

        public static List<Fournisseur> GetAll()
        {
            return new List<Fournisseur>
                 {
                    new Fournisseur
                        {
                            ID = 1 ,
                            Adresse="Copacabana",
                            Provenance = new Provenance { ID = 1, Nom = "Copabane" },
                            Nom="Toto",
                            TelephoneFixe = "02020202",
                            PieceNumero = "XXX--YYYYYY-ZZ",
                            PieceType = new PieceType {ID = 1, Designation= "CNI" },
                            FournisseurGroupe = new FournisseurGroupe {ID=1, Designation="Partenaire" },
                            Certification     = new Certification { ID=1, Designation="UTZ"},
                            eMail             = "m_f@vi.ci"
                        },

                     new Fournisseur
                        {
                            ID = 2 ,
                            Adresse="Copacabana",
                            Nom="Tata",
                            CompteNumero = "XXX--YYYYYY-ZZ",
                            FournisseurGroupe = new FournisseurGroupe {ID=1, Designation="Partenaire" },
                            Certification     = new Certification { ID=1, Designation="Rainforest"},
                            eMail             = "m_c@vi.ci"
                        },
                     new Fournisseur
                        {
                            ID = 3 ,
                            Adresse="Copacabana",
                            Nom="Tutu",
                            CompteNumero = "XXX--YYYYYY-ZZ",
                            FournisseurGroupe = new FournisseurGroupe {ID=1, Designation="Partenaire" },
                            Certification     = new Certification { ID=1, Designation="UTZ"},
                            eMail             = "m_f@vi.ci"
                        },
                     new Fournisseur
                        {
                            ID = 4 ,
                            Adresse="Copacabana",
                            Nom="Tito",
                            CompteNumero = "XXX--YYYYYY-ZZ",
                            FournisseurGroupe = new FournisseurGroupe {ID=1, Designation="Partenaire" },
                            Certification     = new Certification { ID=1, Designation="UTZ"},
                            eMail             = "m_f@vi.ci"
                        }
                 };
        }
    }


    public class FournisseurViewModel
    {
        public Fournisseur _Fournisseur { get; set; }

        

        public string Title
        {
            get 
            {
                
                if (_ExecMode == Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return "Fournisseur : Nouveau";                
                else if (_ExecMode == Components.Settings.EnumsDefinition.eExecMode.Update)
                    return "Fournisseur : Update";
                else if (_ExecMode == Components.Settings.EnumsDefinition.eExecMode.Consult)
                    return "Fournisseur : Properties";
                else
                    return "Fournisseur : Nouveau";
            }
            
        }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }

        public object Rowversion
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return this._Fournisseur != null ? _Fournisseur.RowVersionKey : String.Empty;
                else
                    return  String.Empty;
            }
        }


        public string ID
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return this._Fournisseur != null ? _Fournisseur.ID.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }


        public string Name
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return this._Fournisseur != null ? _Fournisseur.Nom : String.Empty;
                else
                    return String.Empty;
            }
        }


        public string GroupFournisseurID
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null && this._Fournisseur.FournisseurGroupe != null) ? _Fournisseur.FournisseurGroupe.ID.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }


        public string GroupFournisseurName
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null && this._Fournisseur.FournisseurGroupe != null) ? _Fournisseur.FournisseurGroupe.Designation.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }


        public string TypeFournisseurID
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null && this._Fournisseur.FournisseurType != null) ? _Fournisseur.FournisseurType.ID.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }
        
        public string OriginFournisseurID
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null && this._Fournisseur.Provenance != null) ? _Fournisseur.Provenance.ID.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }


        public string PieceNumeroFournisseurID
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null ) ? _Fournisseur.PieceNumero.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }

        public string PieceTypeFournisseur
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null && this._Fournisseur.PieceType != null) ? _Fournisseur.PieceType.ID.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }


        public string AddresseFournisseur
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null) ? _Fournisseur.Adresse.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }


        public string FixeFournisseur
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null) ? _Fournisseur.TelephoneFixe.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }


        public string MobileFournisseur
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null) ? _Fournisseur.TelephoneMobile.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }


        public string FaxFournisseur
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null) ? _Fournisseur.Fax.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }


        public string EmailFournisseur
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null) ? _Fournisseur.eMail.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }


        public string AccountingIDFournisseur
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null) ? _Fournisseur.CompteNumero.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }


        public string CertificationFournisseur
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null && this._Fournisseur.Certification != null && this._Fournisseur.Certification.ID != 0) ? _Fournisseur.Certification.ID.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }

        public string AgenceFournisseur
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null && this._Fournisseur.Agence != null && this._Fournisseur.Agence.ID != 0) ? _Fournisseur.Agence.ID.ToString() : String.Empty;
                else
                    return _Fournisseur.Agence.ID.ToString();
            }
        }

        public string NumeroAgrementFournisseur
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null) ? _Fournisseur.NumAgrement.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }

        public string SiegeSocialFournisseur
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null) ? _Fournisseur.SiegeSocial.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }

        public string NumeroCCFournisseur
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null) ? _Fournisseur.NumeroCC.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }

        public string NumeroRegistreFournisseur
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null) ? _Fournisseur.NumeroRegistre.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }

        public decimal? CapitalFournisseur
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null) ? _Fournisseur.Capital : (decimal?)null;
                else
                    return (decimal?)null;
            }
        }

        public string FloIdFournisseur
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._Fournisseur != null) ? _Fournisseur.FloId.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }

        //Get fixed parameters values
        Parametres mParametres = new Parametres(0);

        public int IdChefAgence
        {
            get
            {
                return mParametres.ChefAgenceType;
            }

        }

        public string CapitalFournisseurAsString
        {
            get { return CapitalFournisseur != 0 ? String.Format("{0:#,#}", CapitalFournisseur).TrimStart() : string.Empty; }
        }
    }

}
