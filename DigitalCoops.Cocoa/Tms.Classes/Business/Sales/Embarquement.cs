using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Business.stock;
using Tms.Classes.Shared;
using Tms.Classes.Shared.Sales;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms.Classes.Business.Sales
{
    public  class Embarquement : DataPersist
    {

        #region "Fields"
        private Guid _ID;
        private string _Campagne;
        private Exportateur _Exportateur;
        private Enregistrement _Enregistrement;

        private Client _Client;
        private ContratDeVentes _Contrat;

        private string _Numero;
        private string _Reference;
        private DateTime _Date;
        private DateTime _PeriodeEmbarquement;
        private DateTime _ETA;
        private DateTime _PeriodeBL;

        private decimal _Quantite;
        private int _NbreConteneur;
        private int _NbreSacs;
        private string _MentionOT;

        private Banque _Domiciliation;
        private DestinationExport _DestinationExport;
        private Navire _Navire;
        private Conditionnement _Conditionnement;
        private CompagnieMaritime _CompagnieMaritime;
        private Consignee _Consignee;
        private ConteneurType _ConteneurType;
        private Certification _Certification;
        private ModeTraitement _ModeTraitement;
        private Transitaire _Transitaire;
        private bool _Desactive;
        private string _Statut;
        //Weight Lot
        private decimal _PoidsBrut;
        private decimal _PoidsNet;

        //Facture Finale
        private decimal _PoidsArrivee;
        private decimal _PoidsEchantillonArrivee;
        private decimal _FactureCommercialeMontant;

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

        public Exportateur Exportateur
        {
            get { return _Exportateur; }
            set { _Exportateur = value; }
        }

        public string ExportateurNameAndCode
        {
            get
            {
                if (_Exportateur != null)
                    return _Exportateur.Nom.Contains(_Exportateur.ID.ToString()) ? _Exportateur.Nom : _Exportateur.Nom + " - " + _Exportateur.ID;
                else
                    return string.Empty;
            }

        }

        public Enregistrement Enregistrement
        {
            get { return _Enregistrement; }
            set { _Enregistrement = value; }
        }

        public string EnregistrementAsString
        {
            get
            {
                if (_Enregistrement != null)
                    return _Enregistrement.Numero;
                else
                    return string.Empty;
            }

        }

        public string EnregistrementCodeAsString
        {
            get
            {
                if (_Enregistrement != null)
                    return _Enregistrement.NumeroCode;
                else
                    return string.Empty;
            }

        }

        public string NumeroONCC
        {
            get
            {
                if (_Enregistrement != null)
                    return _Enregistrement.NumeroONCC;
                else
                    return string.Empty;
            }

        }

        public Client Client
        {
            get { return _Client; }
            set { _Client = value; }
        }

        public string ClientNameAndCode
        {
            get
            {
                if (_Client != null)
                    return _Client.Nom.Contains(_Client.ID.ToString()) ? _Client.Nom : _Client.Nom + " - " + _Client.ID;
                else
                    return string.Empty;
            }

        }

        public ContratDeVentes Contrat
        {
            get { return _Contrat; }
            set { _Contrat = value; }
        }

        public string ContratAsString
        {
            get
            {
                if (_Contrat != null)
                    return _Contrat.ContratNumero;
                else
                    return string.Empty;
            }

        }

        public string ContratCodeAsString
        {
            get
            {
                if (_Contrat != null)
                    return _Contrat.NumeroCode;
                else
                    return string.Empty;
            }

        }

        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public DateTime PeriodeEmbarquement
        {
            get { return _PeriodeEmbarquement; }
            set { _PeriodeEmbarquement = value; }
        }
        public DateTime ETA
        {
            get { return _ETA; }
            set { _ETA = value; }
        }

        public DateTime PeriodeBL
        {
            get { return _PeriodeBL; }
            set { _PeriodeBL = value; }
        }

        public decimal Quantite
        {
            get { return _Quantite; }
            set { _Quantite = value; }
        }

        public string QuantiteAsString
        {
            get { return _Quantite != 0 ? String.Format("{0:#,##0.###}", _Quantite).TrimStart() : string.Empty; }
        }

        public int NbreConteneur
        {
            get { return _NbreConteneur; }
            set { _NbreConteneur = value; }
        }

        public string NbreConteneurAsString
        {
            get { return _NbreConteneur != 0 ? String.Format("{0:#,#}", _NbreConteneur).TrimStart() : string.Empty; }
        }


        public int NbreSacs
        {
            get { return _NbreSacs; }
            set { _NbreSacs = value; }
        }

        public string NbreSacsAsString
        {
            get { return _NbreSacs != 0 ? String.Format("{0:#,#}", _NbreSacs).TrimStart() : string.Empty; }
        }


        public string MentionOT
        {
            get { return _MentionOT; }
            set { _MentionOT = value; }
        }

        public decimal PoidsBrut
        {
            get { return _PoidsBrut; }
            set { _PoidsBrut = value; }
        }

        public string PoidsBrutAsString
        {
            get { return _PoidsBrut != 0 ? String.Format("{0:#,#}", _PoidsBrut).TrimStart() : string.Empty; }
        }

        public decimal PoidsNet
        {
            get { return _PoidsNet; }
            set { _PoidsNet = value; }
        }

        public string PoidsNetAsString
        {
            get { return _PoidsNet != 0 ? String.Format("{0:#,#}", _PoidsNet).TrimStart() : string.Empty; }
        }

        public decimal PoidsArrivee
        {
            get { return _PoidsArrivee; }
            set { _PoidsArrivee = value; }
        }

        public string PoidsArriveeAsString
        {
            get { return _PoidsArrivee != 0 ? String.Format("{0:#,#}", _PoidsArrivee).TrimStart() : string.Empty; }
        }

        public decimal PoidsEchantillonArrivee
        {
            get { return _PoidsEchantillonArrivee; }
            set { _PoidsEchantillonArrivee = value; }
        }

        public string PoidsEchantillonArriveeAsString
        {
            get { return _PoidsEchantillonArrivee != 0 ? String.Format("{0:#,#}", _PoidsEchantillonArrivee).TrimStart() : string.Empty; }
        }

        public decimal FactureCommercialeMonant
        {
            get { return _FactureCommercialeMontant; }
            set { _FactureCommercialeMontant = value; }
        }

        public string FactureCommercialeMonantAsString
        {
            get { return _FactureCommercialeMontant != 0 ? String.Format("{0:#,##0.###}", _FactureCommercialeMontant).TrimStart() : string.Empty; }
        }

        public Banque Domiciliation
        {
            get { return _Domiciliation; }
            set { _Domiciliation = value; }
        }

        public string DomiciliationAsString
        {
            get { return _Domiciliation != null ? _Domiciliation.Nom : string.Empty; }

        }

        public DestinationExport DestinationExport
        {
            get { return _DestinationExport; }
            set { _DestinationExport = value; }
        }

        public string DestinationExportAsString
        {
            get { return _DestinationExport != null ? _DestinationExport.Nom : string.Empty; }

        }

        public Navire Navire
        {
            get { return _Navire; }
            set { _Navire = value; }
        }

        public string NavireAsString
        {
            get { return _Navire != null ? _Navire.Nom : string.Empty; }

        }

        public Conditionnement Conditionnement
        {
            get { return _Conditionnement; }
            set { _Conditionnement = value; }
        }

        public string ConditionnementAsString
        {
            get { return _Conditionnement != null ? Conditionnement.Designation : string.Empty; }

        }

        public CompagnieMaritime CompagnieMaritime
        {
            get { return _CompagnieMaritime; }
            set { _CompagnieMaritime = value; }
        }

        public string CompagnieMaritimeAsString
        {
            get { return _CompagnieMaritime != null ? _CompagnieMaritime.Nom : string.Empty; }

        }

        public Consignee Consignee
        {
            get { return _Consignee; }
            set { _Consignee = value; }
        }

        public string ConsigneeAsString
        {
            get { return _Consignee != null ? _Consignee.Designation : string.Empty; }

        }

        public ConteneurType ConteneurType
        {
            get { return _ConteneurType; }
            set { _ConteneurType = value; }
        }

        public string ConteneurTypeAsString
        {
            get { return _ConteneurType != null ? _ConteneurType.Designation : string.Empty; }

        }

        public Certification Certification
        {
            get { return _Certification; }
            set { _Certification = value; }
        }

        public string CertificationAsString
        {
            get { return _Certification != null ? _Certification.Designation : string.Empty; }

        }

        public ModeTraitement ModeTraitement
        {
            get { return _ModeTraitement; }
            set { _ModeTraitement = value; }
        }

        public string ModeTraitementAsString
        {
            get { return _ModeTraitement != null ? _ModeTraitement.Designation : string.Empty; }

        }

        public Transitaire Transitaire
        {
            get { return _Transitaire; }
            set { _Transitaire = value; }
        }

        public string TransitaireAsString
        {
            get { return _Transitaire != null ? _Transitaire.Nom : string.Empty; }

        }
        public string Statut
        {
            get { return _Statut; }
            set { _Statut = value; }
        }

        public bool IsPlan
        {
            get { return _Statut == "PL"; }

        }

        public bool IsInTransit
        {
            get { return _Statut == "IT"; }

        }
        public bool IsReceives
        {
            get { return _Statut == "RE"; }

        }

        public string StatutAsSTring
        {
            get
            {
                if (_Statut == "IT")
                    return "En Transit";

                else if (_Statut == "LO")
                    return "Planiifé";

                else if (_Statut == "RE")
                    return "Receptionné";
                else
                    return "Planifié";
            }
        }


        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
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

        public int mIconStatut
        {
            get
            {
                if (_Statut == "PL")
                    return 2; // BulletCross
                else if (_Statut == "RE")
                    return 5; // Tick
                else
                    return 4; // Tick
                 

            }
        }

        public string Reference
        {
            get
            {
                return _Reference;
            }

            set
            {
                _Reference = value;
            }
        }
        #endregion

        #region Constructor
        public Embarquement()
        {

        }

        public Embarquement(Guid MyId)
        {
            this.fnGet(MyId);
        }
        #endregion

        #region "Methods"
        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Embarquement_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "V2_Embarquement:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Embarquement_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "V2_Embarquement:fnDeActivate");
            }
            return Result;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_Embarquement_Get", (Guid)Id);
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
            return fnSelect("{Tous}", -1, -1,-1,-1, null, null, -1);
        }

        public List<DataPersist> fnSelect(string Campagne, int ExportateurID, int CertificationID,int ConteneurTypeID,int ClientID, DateTime? StartDate, DateTime? EndDate, int IsDisabled)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Embarquement_Select");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, CertificationID);
                db().AddInParameter(mCommande, "@ConteneurTypeID", SqlDbType.Int, ConteneurTypeID);
                db().AddInParameter(mCommande, "@ClientID", SqlDbType.Int, ClientID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@IsDisabled", SqlDbType.Int, IsDisabled);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Embarquement mClass = new Embarquement();
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

        public List<DataPersist> fnSelectForFees(string Campagne, int ExportateurID, int CertificationID, int ConteneurTypeID, int ClientID, DateTime? StartDate, DateTime? EndDate, int IsDisabled, int nature)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Embarquement_SelectForFees");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, CertificationID);
                db().AddInParameter(mCommande, "@ConteneurTypeID", SqlDbType.Int, ConteneurTypeID);
                db().AddInParameter(mCommande, "@ClientID", SqlDbType.Int, ClientID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@IsDisabled", SqlDbType.Int, IsDisabled);
                db().AddInParameter(mCommande, "@nature", SqlDbType.Int, nature);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Embarquement mClass = new Embarquement();
                    MapFromDataReaderLite1(mClass, mDataReader);
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


        public List<DataPersist> fnSelectForStuffing(string Campagne, int ExportateurID, int CertificationID, int ConteneurTypeID, int ClientID, DateTime? StartDate, DateTime? EndDate, int IsDisabled)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Embarquement_SelectForStuffing");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, CertificationID);
                db().AddInParameter(mCommande, "@ConteneurTypeID", SqlDbType.Int, ConteneurTypeID);
                db().AddInParameter(mCommande, "@ClientID", SqlDbType.Int, ClientID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@IsDisabled", SqlDbType.Int, IsDisabled);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Embarquement mClass = new Embarquement();
                    MapFromDataReaderLite4(mClass, mDataReader);
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

        public List<DataPersist> fnSelectForBL(string Campagne, int ExportateurID, int CertificationID, int ConteneurTypeID, int ClientID, DateTime? StartDate, DateTime? EndDate, int IsDisabled)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Embarquement_SelectForBL");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, CertificationID);
                db().AddInParameter(mCommande, "@ConteneurTypeID", SqlDbType.Int, ConteneurTypeID);
                db().AddInParameter(mCommande, "@ClientID", SqlDbType.Int, ClientID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@IsDisabled", SqlDbType.Int, IsDisabled);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Embarquement mClass = new Embarquement();
                    MapFromDataReaderLite4(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectForBL");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectForCommercialInvoice(string Campagne, int ExportateurID, int CertificationID, int ConteneurTypeID, int ClientID, DateTime? StartDate, DateTime? EndDate, int IsDisabled)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Embarquement_SelectForCommercialInvoice");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, CertificationID);
                db().AddInParameter(mCommande, "@ConteneurTypeID", SqlDbType.Int, ConteneurTypeID);
                db().AddInParameter(mCommande, "@ClientID", SqlDbType.Int, ClientID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@IsDisabled", SqlDbType.Int, IsDisabled);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Embarquement mClass = new Embarquement();
                    MapFromDataReaderLite2(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectForCommercialInvoice");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectForWeighingCertificate(string Campagne, int ExportateurID, int CertificationID, int ConteneurTypeID, int ClientID, DateTime? StartDate, DateTime? EndDate, int IsDisabled)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Embarquement_SelectForWeighingCertificate");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, CertificationID);
                db().AddInParameter(mCommande, "@ConteneurTypeID", SqlDbType.Int, ConteneurTypeID);
                db().AddInParameter(mCommande, "@ClientID", SqlDbType.Int, ClientID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@IsDisabled", SqlDbType.Int, IsDisabled);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Embarquement mClass = new Embarquement();
                    MapFromDataReaderLite1(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectForWeighingCertificate");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectForFinalInvoice(string Campagne, int ExportateurID, int CertificationID, DateTime? StartDate, DateTime? EndDate, int IsDisabled)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Embarquement_SelectForFinalInvoice");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, CertificationID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@IsDisabled", SqlDbType.Int, IsDisabled);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Embarquement mClass = new Embarquement();
                    MapFromDataReaderLite3(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectForFinalInvoice");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }


        public List<DataPersist> fnSelectLot(object ID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Embarquement_SelectLot");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Lot mClass = new Lot();
                    MapFromDataReaderLite(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectLot");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public bool fnRemoveLotAll()
        {
            if (!this._isnew)
            {
                bool bolResult;
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Embarquement_RemoveLotAll");
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
                    throw new Exception(ex.Message + "\r\n" + "Embarquement:fnRemoveLotAll");
                }
                return bolResult;
            }
            return false;
        }

        

        public override bool fnUpdate()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {

                    mCommande = db().CreateStoredProcCommand("V2_Embarquement_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 7);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_Embarquement_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, _Campagne);
                db().AddInParameter(mCommande, "@Reference", SqlDbType.VarChar, _Reference);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, _Exportateur.ID);

                db().AddInParameter(mCommande, "@EnregistrementID", SqlDbType.UniqueIdentifier, _Enregistrement.ID);
                db().AddInParameter(mCommande, "@ClientID", SqlDbType.Int, _Client.ID);
                db().AddInParameter(mCommande, "@ContratID", SqlDbType.UniqueIdentifier, _Contrat.ID);

                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _Date);
                db().AddInParameter(mCommande, "@PeriodeEmbarquement", SqlDbType.DateTime, _PeriodeEmbarquement);
                db().AddInParameter(mCommande, "@ETA", SqlDbType.DateTime, _ETA);
                db().AddInParameter(mCommande, "@PeriodeBL", SqlDbType.DateTime, _PeriodeBL);

               
                db().AddInParameter(mCommande, "@Quantite", SqlDbType.Decimal, (_Quantite*1000));
                db().AddInParameter(mCommande, "@NbreConteneur", SqlDbType.Int,_NbreConteneur);
                db().AddInParameter(mCommande, "@MentionOT", SqlDbType.VarChar, _MentionOT);

                db().AddInParameter(mCommande, "@Poidsbrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNet);

                db().AddInParameter(mCommande, "@DomiciliationID", SqlDbType.Int, _Domiciliation.ID);
                db().AddInParameter(mCommande, "@DestinationExportID", SqlDbType.Int, _DestinationExport.ID);
                db().AddInParameter(mCommande, "@NavireID", SqlDbType.Int, _Navire.ID);
                db().AddInParameter(mCommande, "@ConditionnementID", SqlDbType.Int, _Conditionnement.ID);
                db().AddInParameter(mCommande, "@CompagnieMaritimeID", SqlDbType.Int, _CompagnieMaritime.ID);
                db().AddInParameter(mCommande, "@ConsigneeID", SqlDbType.Int, _Consignee.ID);
                db().AddInParameter(mCommande, "@ConteneurTypeID", SqlDbType.Int, _ConteneurType.ID);
                if (_Certification != null)
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, _Certification.ID);
                else
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, DBNull.Value);

                if (_ModeTraitement != null)
                    db().AddInParameter(mCommande, "@ModeTraitementID", SqlDbType.Int, _ModeTraitement.ID);
                else
                    db().AddInParameter(mCommande, "@ModeTraitementID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@TransitaireID", SqlDbType.Int, _Transitaire.ID);

                if (_Enregistrement.EnregistrementDetailID != null || _Enregistrement.EnregistrementDetailID != Guid.Empty)
                    db().AddInParameter(mCommande, "@EnregistrementDetailID", SqlDbType.UniqueIdentifier, _Enregistrement.EnregistrementDetailID);
                else
                    db().AddInParameter(mCommande, "@EnregistrementDetailID", SqlDbType.Int, DBNull.Value);

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
                            _Statut = "PL";
                            _Numero = (string)db().Parameters(mCommande, "@Numero");
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
                throw new Exception(ex.Message + "\r\n" + "V2_Embarquement:fnUpdate");

            }
            return Result;
        }

        public  bool fnUpdate(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {

                    mCommande = db().CreateStoredProcCommand("V2_Embarquement_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 7);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_Embarquement_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, _Campagne);
                db().AddInParameter(mCommande, "@Reference", SqlDbType.VarChar, _Reference);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, _Exportateur.ID);

                db().AddInParameter(mCommande, "@EnregistrementID", SqlDbType.UniqueIdentifier, _Enregistrement.ID);
                db().AddInParameter(mCommande, "@ClientID", SqlDbType.Int, _Client.ID);
                db().AddInParameter(mCommande, "@ContratID", SqlDbType.UniqueIdentifier, _Contrat.ID);

                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _Date);
                db().AddInParameter(mCommande, "@PeriodeEmbarquement", SqlDbType.DateTime, _PeriodeEmbarquement);
                db().AddInParameter(mCommande, "@ETA", SqlDbType.DateTime, _ETA);
                db().AddInParameter(mCommande, "@PeriodeBL", SqlDbType.DateTime, _PeriodeBL);


                db().AddInParameter(mCommande, "@Quantite", SqlDbType.Decimal, (_Quantite*1000));
                db().AddInParameter(mCommande, "@NbreConteneur", SqlDbType.Int, _NbreConteneur);
                db().AddInParameter(mCommande, "@MentionOT", SqlDbType.VarChar, _MentionOT);

                db().AddInParameter(mCommande, "@Poidsbrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNet);


                db().AddInParameter(mCommande, "@DomiciliationID", SqlDbType.Int, _Domiciliation.ID);
                db().AddInParameter(mCommande, "@DestinationExportID", SqlDbType.Int, _DestinationExport.ID);
                db().AddInParameter(mCommande, "@NavireID", SqlDbType.Int, _Navire.ID);
                db().AddInParameter(mCommande, "@ConditionnementID", SqlDbType.Int, _Conditionnement.ID);
                db().AddInParameter(mCommande, "@CompagnieMaritimeID", SqlDbType.Int, _CompagnieMaritime.ID);
                db().AddInParameter(mCommande, "@ConsigneeID", SqlDbType.Int, _Consignee.ID);
                db().AddInParameter(mCommande, "@ConteneurTypeID", SqlDbType.Int, _ConteneurType.ID);
                if(_Certification !=null)
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, _Certification.ID);
                else
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, DBNull.Value);

                if (_ModeTraitement != null)
                    db().AddInParameter(mCommande, "@ModeTraitementID", SqlDbType.Int, _ModeTraitement.ID);
                else
                    db().AddInParameter(mCommande, "@ModeTraitementID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@TransitaireID", SqlDbType.Int, _Transitaire.ID);

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
                            _Statut = "PL";
                            _Numero = (string)db().Parameters(mCommande, "@Numero");
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
                throw new Exception(ex.Message + "\r\n" + "V2_Embarquement:fnUpdateTransaction");

            }
            return Result;
        }

        #endregion

        #region "Private Methods"
        public override string ToString()
        {
            throw new NotImplementedException();
        }

        private static void MapFromDataReader(Embarquement mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Campagne"])) mClass._Campagne = (string)mDataReader["Campagne"];
                    if (!DBNull.Value.Equals(mDataReader["ExportateurID"]))
                    {
                        mClass._Exportateur = new Exportateur();
                        mClass._Exportateur.ID = (int)mDataReader["ExportateurID"];
                        mClass._Exportateur.Nom = (string)mDataReader["ExportateurNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["EnregistrementID"]))
                    {
                        mClass._Enregistrement = new Enregistrement();
                        mClass._Enregistrement.ID = (Guid)mDataReader["EnregistrementID"];
                        mClass._Enregistrement.Numero = (string)mDataReader["EnregistrementNumero"];
                        mClass._Enregistrement.NumeroONCC = (string)mDataReader["EnregistrementONCC"];
                        mClass._Enregistrement.ContratExport = (string)mDataReader["EnregistrementContratExport"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ClientID"]))
                    {
                        mClass._Client = new Client();
                        mClass._Client.ID = (int)mDataReader["ClientID"];
                        mClass._Client.Nom = (string)mDataReader["ClientNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ContratID"]))
                    {
                        mClass._Contrat = new ContratDeVentes();
                        mClass._Contrat.ID = (Guid)mDataReader["ContratID"];
                        mClass._Contrat.Numero = (string)mDataReader["NumeroTMS"];
                        mClass._Contrat.ContratNumero = (string)mDataReader["ContratNumero"];
                        mClass._Contrat.ReferenceClient = (string)mDataReader["ContratRefClient"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["Reference"])) mClass._Reference = (string)mDataReader["Reference"];
                    if (!DBNull.Value.Equals(mDataReader["DateEm"])) mClass._Date = (DateTime)mDataReader["DateEm"];
                    if (!DBNull.Value.Equals(mDataReader["PeriodeEmbarquement"])) mClass._PeriodeEmbarquement = (DateTime)mDataReader["PeriodeEmbarquement"];
                    if (!DBNull.Value.Equals(mDataReader["ETA"])) mClass._ETA = (DateTime)mDataReader["ETA"];
                    if (!DBNull.Value.Equals(mDataReader["PeriodeBL"])) mClass._PeriodeBL = (DateTime)mDataReader["PeriodeBL"];

                    if (!DBNull.Value.Equals(mDataReader["Quantite"])) mClass._Quantite = ((decimal)mDataReader["Quantite"]/1000);
                    if (!DBNull.Value.Equals(mDataReader["NbreConteneur"])) mClass._NbreConteneur = (int)mDataReader["NbreConteneur"];

                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNet = (decimal)mDataReader["PoidsNet"];


                    if (!DBNull.Value.Equals(mDataReader["MentionOT"])) mClass._MentionOT = (string)mDataReader["MentionOT"];

                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];

                    if (!DBNull.Value.Equals(mDataReader["BanqueID"]))
                    {
                        mClass._Domiciliation = new Banque();
                        mClass._Domiciliation.ID = (int)mDataReader["BanqueID"];
                        mClass._Domiciliation.Nom = (string)mDataReader["BanqueNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["DestinationExportID"]))
                    {
                        mClass._DestinationExport = new DestinationExport();
                        mClass._DestinationExport.ID = (int)mDataReader["DestinationExportID"];
                        mClass._DestinationExport.Nom = (string)mDataReader["DestinationExportNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["NavireID"]))
                    {
                        mClass._Navire = new Navire();
                        mClass._Navire.ID = (int)mDataReader["NavireID"];
                        mClass._Navire.Nom = (string)mDataReader["NavireNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ConditionnementID"]))
                    {
                        mClass._Conditionnement = new Conditionnement();
                        mClass._Conditionnement.ID = (int)mDataReader["ConditionnementID"];
                        mClass._Conditionnement.Designation = (string)mDataReader["ConditionnementDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["CompagnieMaritimeID"]))
                    {
                        mClass._CompagnieMaritime = new CompagnieMaritime();
                        mClass._CompagnieMaritime.ID = (int)mDataReader["CompagnieMaritimeID"];
                        mClass._CompagnieMaritime.Nom = (string)mDataReader["CompagnieMaritimeNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ConsigneeID"]))
                    {
                        mClass._Consignee = new Consignee();
                        mClass._Consignee.ID = (int)mDataReader["ConsigneeID"];
                        mClass._Consignee.Designation = (string)mDataReader["ConsigneeDesignation"];
                    }


                    if (!DBNull.Value.Equals(mDataReader["ConteneurTypeID"]))
                    {
                        mClass._ConteneurType = new ConteneurType();
                        mClass._ConteneurType.ID = (int)mDataReader["ConteneurTypeID"];
                        mClass._ConteneurType.Designation = (string)mDataReader["ConteneurTypeDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["CertificationID"]))
                    {
                        mClass._Certification = new Certification();
                        mClass._Certification.ID = (int)mDataReader["CertificationID"];
                        mClass._Certification.Designation = (string)mDataReader["CertificationDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ModeTraitementID"]))
                    {
                        mClass._ModeTraitement = new ModeTraitement();
                        mClass._ModeTraitement.ID = (int)mDataReader["ModeTraitementID"];
                        mClass._ModeTraitement.Designation = (string)mDataReader["ModeTraitementDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["TransitaireID"]))
                    {
                        mClass._Transitaire = new Transitaire();
                        mClass._Transitaire.ID = (int)mDataReader["TransitaireID"];
                        mClass._Transitaire.Nom = (string)mDataReader["TransitaireNom"];
                    }

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
                throw new Exception(ex.Message + "\nV2_Embarquement:MapFromDataReader");
            }
        }
        private static void MapFromDataReaderLite4(Embarquement mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Campagne"])) mClass._Campagne = (string)mDataReader["Campagne"];
                    if (!DBNull.Value.Equals(mDataReader["ExportateurID"]))
                    {
                        mClass._Exportateur = new Exportateur();
                        mClass._Exportateur.ID = (int)mDataReader["ExportateurID"];
                        mClass._Exportateur.Nom = (string)mDataReader["ExportateurNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["EnregistrementID"]))
                    {
                        mClass._Enregistrement = new Enregistrement();
                        mClass._Enregistrement.ID = (Guid)mDataReader["EnregistrementID"];
                        mClass._Enregistrement.Numero = (string)mDataReader["EnregistrementNumero"];
                        mClass._Enregistrement.NumeroONCC = (string)mDataReader["EnregistrementONCC"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ClientID"]))
                    {
                        mClass._Client = new Client();
                        mClass._Client.ID = (int)mDataReader["ClientID"];
                        mClass._Client.Nom = (string)mDataReader["ClientNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ContratID"]))
                    {
                        mClass._Contrat = new ContratDeVentes();
                        mClass._Contrat.ID = (Guid)mDataReader["ContratID"];
                        mClass._Contrat.Numero = (string)mDataReader["NumeroTMS"];
                        mClass._Contrat.ContratNumero = (string)mDataReader["ContratNumero"];
                        mClass._Contrat.ReferenceClient = (string)mDataReader["ContratRefClient"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["Reference"])) mClass._Reference = (string)mDataReader["Reference"];
                    if (!DBNull.Value.Equals(mDataReader["DateEm"])) mClass._Date = (DateTime)mDataReader["DateEm"];
                    if (!DBNull.Value.Equals(mDataReader["PeriodeEmbarquement"])) mClass._PeriodeEmbarquement = (DateTime)mDataReader["PeriodeEmbarquement"];
                    if (!DBNull.Value.Equals(mDataReader["ETA"])) mClass._ETA = (DateTime)mDataReader["ETA"];
                    if (!DBNull.Value.Equals(mDataReader["PeriodeBL"])) mClass._PeriodeBL = (DateTime)mDataReader["PeriodeBL"];

                    if (!DBNull.Value.Equals(mDataReader["Quantite"])) mClass._Quantite = ((decimal)mDataReader["Quantite"] / 1000);
                    if (!DBNull.Value.Equals(mDataReader["NbreConteneur"])) mClass._NbreConteneur = (int)mDataReader["NbreConteneur"];

                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNet = (decimal)mDataReader["PoidsNet"];


                    if (!DBNull.Value.Equals(mDataReader["MentionOT"])) mClass._MentionOT = (string)mDataReader["MentionOT"];

                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];

                    if (!DBNull.Value.Equals(mDataReader["BanqueID"]))
                    {
                        mClass._Domiciliation = new Banque();
                        mClass._Domiciliation.ID = (int)mDataReader["BanqueID"];
                        mClass._Domiciliation.Nom = (string)mDataReader["BanqueNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["DestinationExportID"]))
                    {
                        mClass._DestinationExport = new DestinationExport();
                        mClass._DestinationExport.ID = (int)mDataReader["DestinationExportID"];
                        mClass._DestinationExport.Nom = (string)mDataReader["DestinationExportNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["NavireID"]))
                    {
                        mClass._Navire = new Navire();
                        mClass._Navire.ID = (int)mDataReader["NavireID"];
                        mClass._Navire.Nom = (string)mDataReader["NavireNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ConditionnementID"]))
                    {
                        mClass._Conditionnement = new Conditionnement();
                        mClass._Conditionnement.ID = (int)mDataReader["ConditionnementID"];
                        mClass._Conditionnement.Designation = (string)mDataReader["ConditionnementDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["CompagnieMaritimeID"]))
                    {
                        mClass._CompagnieMaritime = new CompagnieMaritime();
                        mClass._CompagnieMaritime.ID = (int)mDataReader["CompagnieMaritimeID"];
                        mClass._CompagnieMaritime.Nom = (string)mDataReader["CompagnieMaritimeNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ConsigneeID"]))
                    {
                        mClass._Consignee = new Consignee();
                        mClass._Consignee.ID = (int)mDataReader["ConsigneeID"];
                        mClass._Consignee.Designation = (string)mDataReader["ConsigneeDesignation"];
                    }


                    if (!DBNull.Value.Equals(mDataReader["ConteneurTypeID"]))
                    {
                        mClass._ConteneurType = new ConteneurType();
                        mClass._ConteneurType.ID = (int)mDataReader["ConteneurTypeID"];
                        mClass._ConteneurType.Designation = (string)mDataReader["ConteneurTypeDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["CertificationID"]))
                    {
                        mClass._Certification = new Certification();
                        mClass._Certification.ID = (int)mDataReader["CertificationID"];
                        mClass._Certification.Designation = (string)mDataReader["CertificationDesignation"];
                    }

                    

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
                throw new Exception(ex.Message + "\nV2_Embarquement:MapFromDataReader");
            }
        }
        private static void MapFromDataReaderLite1(Embarquement mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Campagne"])) mClass._Campagne = (string)mDataReader["Campagne"];
                    if (!DBNull.Value.Equals(mDataReader["ExportateurID"]))
                    {
                        mClass._Exportateur = new Exportateur();
                        mClass._Exportateur.ID = (int)mDataReader["ExportateurID"];
                        mClass._Exportateur.Nom = (string)mDataReader["ExportateurNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["EnregistrementID"]))
                    {
                        mClass._Enregistrement = new Enregistrement();
                        mClass._Enregistrement.ID = (Guid)mDataReader["EnregistrementID"];
                        mClass._Enregistrement.Numero = (string)mDataReader["EnregistrementNumero"];
                        mClass._Enregistrement.NumeroONCC = (string)mDataReader["EnregistrementONCC"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ClientID"]))
                    {
                        mClass._Client = new Client();
                        mClass._Client.ID = (int)mDataReader["ClientID"];
                        mClass._Client.Nom = (string)mDataReader["ClientNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ContratID"]))
                    {
                        mClass._Contrat = new ContratDeVentes();
                        mClass._Contrat.ID = (Guid)mDataReader["ContratID"];
                        mClass._Contrat.Numero = (string)mDataReader["NumeroTMS"];
                        mClass._Contrat.ContratNumero = (string)mDataReader["ContratNumero"];
                        mClass._Contrat.ReferenceClient = (string)mDataReader["ContratRefClient"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["Reference"])) mClass._Reference = (string)mDataReader["Reference"];
                    if (!DBNull.Value.Equals(mDataReader["DateEm"])) mClass._Date = (DateTime)mDataReader["DateEm"];
                    if (!DBNull.Value.Equals(mDataReader["PeriodeEmbarquement"])) mClass._PeriodeEmbarquement = (DateTime)mDataReader["PeriodeEmbarquement"];
                    if (!DBNull.Value.Equals(mDataReader["ETA"])) mClass._ETA = (DateTime)mDataReader["ETA"];
                    if (!DBNull.Value.Equals(mDataReader["PeriodeBL"])) mClass._PeriodeBL = (DateTime)mDataReader["PeriodeBL"];

                    if (!DBNull.Value.Equals(mDataReader["Quantite"])) mClass._Quantite = (decimal)mDataReader["Quantite"];
                    if (!DBNull.Value.Equals(mDataReader["NbreConteneur"])) mClass._NbreConteneur = (int)mDataReader["NbreConteneur"];

                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNet = (decimal)mDataReader["PoidsNet"];


                    if (!DBNull.Value.Equals(mDataReader["MentionOT"])) mClass._MentionOT = (string)mDataReader["MentionOT"];

                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];

                    if (!DBNull.Value.Equals(mDataReader["BanqueID"]))
                    {
                        mClass._Domiciliation = new Banque();
                        mClass._Domiciliation.ID = (int)mDataReader["BanqueID"];
                        mClass._Domiciliation.Nom = (string)mDataReader["BanqueNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["DestinationExportID"]))
                    {
                        mClass._DestinationExport = new DestinationExport();
                        mClass._DestinationExport.ID = (int)mDataReader["DestinationExportID"];
                        mClass._DestinationExport.Nom = (string)mDataReader["DestinationExportNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["NavireID"]))
                    {
                        mClass._Navire = new Navire();
                        mClass._Navire.ID = (int)mDataReader["NavireID"];
                        mClass._Navire.Nom = (string)mDataReader["NavireNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ConditionnementID"]))
                    {
                        mClass._Conditionnement = new Conditionnement();
                        mClass._Conditionnement.ID = (int)mDataReader["ConditionnementID"];
                        mClass._Conditionnement.Designation = (string)mDataReader["ConditionnementDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["CompagnieMaritimeID"]))
                    {
                        mClass._CompagnieMaritime = new CompagnieMaritime();
                        mClass._CompagnieMaritime.ID = (int)mDataReader["CompagnieMaritimeID"];
                        mClass._CompagnieMaritime.Nom = (string)mDataReader["CompagnieMaritimeNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ConsigneeID"]))
                    {
                        mClass._Consignee = new Consignee();
                        mClass._Consignee.ID = (int)mDataReader["ConsigneeID"];
                        mClass._Consignee.Designation = (string)mDataReader["ConsigneeDesignation"];
                    }


                    if (!DBNull.Value.Equals(mDataReader["ConteneurTypeID"]))
                    {
                        mClass._ConteneurType = new ConteneurType();
                        mClass._ConteneurType.ID = (int)mDataReader["ConteneurTypeID"];
                        mClass._ConteneurType.Designation = (string)mDataReader["ConteneurTypeDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["CertificationID"]))
                    {
                        mClass._Certification = new Certification();
                        mClass._Certification.ID = (int)mDataReader["CertificationID"];
                        mClass._Certification.Designation = (string)mDataReader["CertificationDesignation"];
                    }

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
                throw new Exception(ex.Message + "\nV2_Embarquement:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderLite2(Embarquement mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Campagne"])) mClass._Campagne = (string)mDataReader["Campagne"];
                    if (!DBNull.Value.Equals(mDataReader["ExportateurID"]))
                    {
                        mClass._Exportateur = new Exportateur();
                        mClass._Exportateur.ID = (int)mDataReader["ExportateurID"];
                        mClass._Exportateur.Nom = (string)mDataReader["ExportateurNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["EnregistrementID"]))
                    {
                        mClass._Enregistrement = new Enregistrement();
                        mClass._Enregistrement.ID = (Guid)mDataReader["EnregistrementID"];
                        mClass._Enregistrement.Numero = (string)mDataReader["EnregistrementNumero"];
                        mClass._Enregistrement.NumeroONCC = (string)mDataReader["EnregistrementONCC"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ClientID"]))
                    {
                        mClass._Client = new Client();
                        mClass._Client.ID = (int)mDataReader["ClientID"];
                        mClass._Client.Nom = (string)mDataReader["ClientNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ContratID"]))
                    {
                        mClass._Contrat = new ContratDeVentes();
                        mClass._Contrat.ID = (Guid)mDataReader["ContratID"];
                        mClass._Contrat.Numero = (string)mDataReader["NumeroTMS"];
                        mClass._Contrat.ContratNumero = (string)mDataReader["ContratNumero"];
                        mClass._Contrat.ReferenceClient = (string)mDataReader["ContratRefClient"];
                        mClass._Contrat.Prix = (decimal)mDataReader["ContratPrix"];
                        mClass._Contrat.PrixFob = (decimal)mDataReader["ContratPrixFob"];
                        mClass._Contrat.RemiseFob = (decimal)mDataReader["ContratRemiseFob"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["Reference"])) mClass._Reference = (string)mDataReader["Reference"];
                    if (!DBNull.Value.Equals(mDataReader["DateEm"])) mClass._Date = (DateTime)mDataReader["DateEm"];
                    if (!DBNull.Value.Equals(mDataReader["PeriodeEmbarquement"])) mClass._PeriodeEmbarquement = (DateTime)mDataReader["PeriodeEmbarquement"];
                    if (!DBNull.Value.Equals(mDataReader["ETA"])) mClass._ETA = (DateTime)mDataReader["ETA"];
                    if (!DBNull.Value.Equals(mDataReader["PeriodeBL"])) mClass._PeriodeBL = (DateTime)mDataReader["PeriodeBL"];

                    if (!DBNull.Value.Equals(mDataReader["Quantite"])) mClass._Quantite = ((decimal)mDataReader["Quantite"]/1000);
                    if (!DBNull.Value.Equals(mDataReader["NbreConteneur"])) mClass._NbreConteneur = (int)mDataReader["NbreConteneur"];
                    if (!DBNull.Value.Equals(mDataReader["NbreSacs"])) mClass._NbreSacs = (int)mDataReader["NbreSacs"];

                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNet = (decimal)mDataReader["PoidsNet"];


                    if (!DBNull.Value.Equals(mDataReader["MentionOT"])) mClass._MentionOT = (string)mDataReader["MentionOT"];

                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];

                    if (!DBNull.Value.Equals(mDataReader["BanqueID"]))
                    {
                        mClass._Domiciliation = new Banque();
                        mClass._Domiciliation.ID = (int)mDataReader["BanqueID"];
                        mClass._Domiciliation.Nom = (string)mDataReader["BanqueNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["DestinationExportID"]))
                    {
                        mClass._DestinationExport = new DestinationExport();
                        mClass._DestinationExport.ID = (int)mDataReader["DestinationExportID"];
                        mClass._DestinationExport.Nom = (string)mDataReader["DestinationExportNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["NavireID"]))
                    {
                        mClass._Navire = new Navire();
                        mClass._Navire.ID = (int)mDataReader["NavireID"];
                        mClass._Navire.Nom = (string)mDataReader["NavireNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ConditionnementID"]))
                    {
                        mClass._Conditionnement = new Conditionnement();
                        mClass._Conditionnement.ID = (int)mDataReader["ConditionnementID"];
                        mClass._Conditionnement.Designation = (string)mDataReader["ConditionnementDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["CompagnieMaritimeID"]))
                    {
                        mClass._CompagnieMaritime = new CompagnieMaritime();
                        mClass._CompagnieMaritime.ID = (int)mDataReader["CompagnieMaritimeID"];
                        mClass._CompagnieMaritime.Nom = (string)mDataReader["CompagnieMaritimeNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ConsigneeID"]))
                    {
                        mClass._Consignee = new Consignee();
                        mClass._Consignee.ID = (int)mDataReader["ConsigneeID"];
                        mClass._Consignee.Designation = (string)mDataReader["ConsigneeDesignation"];
                    }


                    if (!DBNull.Value.Equals(mDataReader["ConteneurTypeID"]))
                    {
                        mClass._ConteneurType = new ConteneurType();
                        mClass._ConteneurType.ID = (int)mDataReader["ConteneurTypeID"];
                        mClass._ConteneurType.Designation = (string)mDataReader["ConteneurTypeDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["CertificationID"]))
                    {
                        mClass._Certification = new Certification();
                        mClass._Certification.ID = (int)mDataReader["CertificationID"];
                        mClass._Certification.Designation = (string)mDataReader["CertificationDesignation"];
                    }

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
                throw new Exception(ex.Message + "\nV2_Embarquement:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderLite3(Embarquement mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Campagne"])) mClass._Campagne = (string)mDataReader["Campagne"];
                    if (!DBNull.Value.Equals(mDataReader["ExportateurID"]))
                    {
                        mClass._Exportateur = new Exportateur();
                        mClass._Exportateur.ID = (int)mDataReader["ExportateurID"];
                        mClass._Exportateur.Nom = (string)mDataReader["ExportateurNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["EnregistrementID"]))
                    {
                        mClass._Enregistrement = new Enregistrement();
                        mClass._Enregistrement.ID = (Guid)mDataReader["EnregistrementID"];
                        mClass._Enregistrement.Numero = (string)mDataReader["EnregistrementNumero"];
                        mClass._Enregistrement.NumeroONCC = (string)mDataReader["EnregistrementONCC"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ClientID"]))
                    {
                        mClass._Client = new Client();
                        mClass._Client.ID = (int)mDataReader["ClientID"];
                        mClass._Client.Nom = (string)mDataReader["ClientNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ContratID"]))
                    {
                        mClass._Contrat = new ContratDeVentes();
                        mClass._Contrat.ID = (Guid)mDataReader["ContratID"];
                        mClass._Contrat.Numero = (string)mDataReader["NumeroTMS"];
                        mClass._Contrat.ContratNumero = (string)mDataReader["ContratNumero"];
                        mClass._Contrat.ReferenceClient = (string)mDataReader["ContratRefClient"];
                        mClass._Contrat.Prix = (decimal)mDataReader["ContratPrix"];
                        mClass._Contrat.PrixFob = (decimal)mDataReader["ContratPrixFob"];
                        mClass._Contrat.RemiseFob = (decimal)mDataReader["ContratRemiseFob"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["Reference"])) mClass._Reference = (string)mDataReader["Reference"];
                    if (!DBNull.Value.Equals(mDataReader["DateEm"])) mClass._Date = (DateTime)mDataReader["DateEm"];
                    if (!DBNull.Value.Equals(mDataReader["PeriodeEmbarquement"])) mClass._PeriodeEmbarquement = (DateTime)mDataReader["PeriodeEmbarquement"];
                    if (!DBNull.Value.Equals(mDataReader["ETA"])) mClass._ETA = (DateTime)mDataReader["ETA"];
                    if (!DBNull.Value.Equals(mDataReader["PeriodeBL"])) mClass._PeriodeBL = (DateTime)mDataReader["PeriodeBL"];

                    if (!DBNull.Value.Equals(mDataReader["Quantite"])) mClass._Quantite = ((decimal)mDataReader["Quantite"]/1000);
                    if (!DBNull.Value.Equals(mDataReader["NbreConteneur"])) mClass._NbreConteneur = (int)mDataReader["NbreConteneur"];
                    if (!DBNull.Value.Equals(mDataReader["NbreSacs"])) mClass._NbreSacs = (int)mDataReader["NbreSacs"];

                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNet = (decimal)mDataReader["PoidsNet"];

                    if (!DBNull.Value.Equals(mDataReader["PoidsArrivee"])) mClass._PoidsArrivee = (decimal)mDataReader["PoidsArrivee"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsEchantillonArrivee"])) mClass._PoidsEchantillonArrivee = (decimal)mDataReader["PoidsEchantillonArrivee"];
                    if (!DBNull.Value.Equals(mDataReader["FactureCommercialeMontant"])) mClass._FactureCommercialeMontant = (decimal)mDataReader["FactureCommercialeMontant"];


                    if (!DBNull.Value.Equals(mDataReader["MentionOT"])) mClass._MentionOT = (string)mDataReader["MentionOT"];

                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];

                    if (!DBNull.Value.Equals(mDataReader["BanqueID"]))
                    {
                        mClass._Domiciliation = new Banque();
                        mClass._Domiciliation.ID = (int)mDataReader["BanqueID"];
                        mClass._Domiciliation.Nom = (string)mDataReader["BanqueNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["DestinationExportID"]))
                    {
                        mClass._DestinationExport = new DestinationExport();
                        mClass._DestinationExport.ID = (int)mDataReader["DestinationExportID"];
                        mClass._DestinationExport.Nom = (string)mDataReader["DestinationExportNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["NavireID"]))
                    {
                        mClass._Navire = new Navire();
                        mClass._Navire.ID = (int)mDataReader["NavireID"];
                        mClass._Navire.Nom = (string)mDataReader["NavireNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ConditionnementID"]))
                    {
                        mClass._Conditionnement = new Conditionnement();
                        mClass._Conditionnement.ID = (int)mDataReader["ConditionnementID"];
                        mClass._Conditionnement.Designation = (string)mDataReader["ConditionnementDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["CompagnieMaritimeID"]))
                    {
                        mClass._CompagnieMaritime = new CompagnieMaritime();
                        mClass._CompagnieMaritime.ID = (int)mDataReader["CompagnieMaritimeID"];
                        mClass._CompagnieMaritime.Nom = (string)mDataReader["CompagnieMaritimeNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ConsigneeID"]))
                    {
                        mClass._Consignee = new Consignee();
                        mClass._Consignee.ID = (int)mDataReader["ConsigneeID"];
                        mClass._Consignee.Designation = (string)mDataReader["ConsigneeDesignation"];
                    }


                    if (!DBNull.Value.Equals(mDataReader["ConteneurTypeID"]))
                    {
                        mClass._ConteneurType = new ConteneurType();
                        mClass._ConteneurType.ID = (int)mDataReader["ConteneurTypeID"];
                        mClass._ConteneurType.Designation = (string)mDataReader["ConteneurTypeDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["CertificationID"]))
                    {
                        mClass._Certification = new Certification();
                        mClass._Certification.ID = (int)mDataReader["CertificationID"];
                        mClass._Certification.Designation = (string)mDataReader["CertificationDesignation"];
                    }

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
                throw new Exception(ex.Message + "\nV2_Embarquement:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderLite(Lot mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass.ID = (Guid)mDataReader["ID"];
                    mClass.Campagne = new Shared.Campagne();
                    if (!DBNull.Value.Equals(mDataReader["Campagne"])) mClass.Campagne.Designation = (string)mDataReader["Campagne"];

                    if (!DBNull.Value.Equals(mDataReader["ExportateurID"]))
                    {
                        mClass.Exportateur = new Exportateur();
                        mClass.Exportateur.ID = (int)mDataReader["ExportateurID"];
                        mClass.Exportateur.Nom = (string)mDataReader["ExportateurNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass.NumeroLot = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass.NombreSacs = (int)mDataReader["NombreSacs"];

                    //if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass.PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    //if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass.PoidsNet = (decimal)mDataReader["PoidsNet"];

                    if (!DBNull.Value.Equals(mDataReader["StandardPoidsBrut"])) mClass.PoidsBrut = (decimal)mDataReader["StandardPoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["StandardPoidsNet"])) mClass.PoidsNet = (decimal)mDataReader["StandardPoidsNet"];

                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass.PoidsBrutN = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass.PoidsNetN = (decimal)mDataReader["PoidsNet"];

                    if (!DBNull.Value.Equals(mDataReader["TypeLotID"]))
                    {
                        mClass.LotType = new LotType();
                        mClass.LotType.ID = (int)mDataReader["TypeLotID"];
                        mClass.LotType.Designation = (string)mDataReader["TypeLotDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass.Desactive = (bool)mDataReader["Desactive"];


                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nV2_Lot:MapFromDataReaderLite");
            }
        }


        #endregion
    }

    public partial class EmbarquementViewModel
    {
        public Embarquement _Embarquement { get; set; }

        public int? _DefaultExportateur { get; set; }
        public int? _DefaultClient { get; set; }
        public int? _DefaultConditionnement { get; set; }
        public int? _DefaultConteneurType { get; set; }
        public int? _DefaultForwarder { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
