using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Classes.Shared.Sales;
using Tms.Components.Data;

namespace Tms.Classes.Business.Sales
{
    public class ContratDeVentes : DataPersist
    {
        #region "Fields"
        private Guid _ID;
        private string _Campagne;
        private Exportateur _Exportateur;
        private Banque _Banque;
        private Client _Client;
       
        private string _Numero;
        private string _ContratNumero;
        private DateTime _Date;
        private string _ReferenceClient;
        private decimal _Quantite;
        private decimal _Prix;
        private decimal _RemiseFob;
        private decimal _PrixFob;

        private decimal _PrixCfa;
        private decimal _PrixEuroCertifie;
        private decimal _PrixFobCfa;

        private ProduitExport _ProduitExport;
        private DateTime _DebutExpedition;
        private DateTime _FinExpedition;
        private string _PoidsOptions;

        private string _Options;
        private string _Remarques;

        private Certification _Certification;
        private Qualite _Qualite;
        private ConditionLivraison _ConditionLivraison;
        private TermesPaiement _TermesPaiement;
        private Conditionnement _Conditionnement;
        private Origine _Origine;


        private string _Statut;
        private bool _Desactive;
        private decimal? _BonusCertification;
        protected string _UtilisateurApprobation;
        protected DateTime? _DateApprobation;

        //Calcul
        private decimal _Solde;
        private string _NumeroCode;

        private bool _PoidsConventionnel;

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

        public string ClientAsString
        {
            get { return _Client != null ? _Client.Nom : string.Empty; }

        }

        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        public string NumeroCode
        {
            get { return _ContratNumero + " (" + _Numero + ")"; }
            set { _NumeroCode = value; }
        }

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public DateTime DebutExpedition
        {
            get { return _DebutExpedition; }
            set { _DebutExpedition = value; }
        }

        public DateTime FinExpedition
        {
            get { return _FinExpedition; }
            set { _FinExpedition = value; }
        }

        public string PeriodeAsString
        {
            get { return _DebutExpedition.ToShortDateString()+ " - "+ _FinExpedition.ToShortDateString(); }
        }


        public string ReferenceClient
        {
            get { return _ReferenceClient; }
            set { _ReferenceClient = value; }
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

        public decimal Prix
        {
            get { return _Prix; }
            set { _Prix = value; }
        }

        public decimal ValeurContrat
        {
            get { return _Quantite != 0 && _Prix != 0 ? (_Quantite * _Prix) : 0; }
        }

        public string PrixAsString
        {
            get { return _Prix != 0 ? String.Format("{0:#,##0.##}", _Prix).TrimStart() : string.Empty; }
        }

        public string PrixCfaAsString
        {
            get { return _Prix != 0 ? String.Format("{0:#,#}", _PrixCfa).TrimStart() : string.Empty; }
        }

        public string PrixFobCfaAsString
        {
            get { return _Prix != 0 ? String.Format("{0:#,#}", _PrixFobCfa).TrimStart() : string.Empty; }
        }

        public string PrixEuroCertifieAsString
        {
            get { return _Prix != 0 ? String.Format("{0:#,##0.##}", _PrixEuroCertifie).TrimStart() : string.Empty; }
        }

        public decimal RemiseFob
        {
            get { return _RemiseFob; }
            set { _RemiseFob = value; }
        }

        public string RemiseFobAsString
        {
            get { return _RemiseFob != 0 ? String.Format("{0:#,##0.##}", _RemiseFob).TrimStart() : string.Empty; }
        }


        public decimal PrixFob
        {
            get { return _PrixFob; }
            set { _PrixFob = value; }
        }

        public string PrixFobAsString
        {
            get { return _PrixFob != 0 ? String.Format("{0:#,##0.##}", _PrixFob).TrimStart() : string.Empty; }
        }

        public ProduitExport ProduitExport
        {
            get { return _ProduitExport; }
            set { _ProduitExport = value; }
        }

        public string PoidsOptions
        {
            get { return _PoidsOptions; }
            set { _PoidsOptions = value; }
        }

        
        public decimal Solde
        {
            get { return _Solde; }
            set { _Solde = value; }
        }

        
        public string SoldeAsString
        {
            get { return _Solde != 0 ? String.Format("{0:#,##0.##}", _Solde).TrimStart() : string.Empty; }
        }

        
        public string Options
        {
            get { return _Options; }
            set { _Options = value; }
        }

        public string Remarques
        {
            get { return _Remarques; }
            set { _Remarques = value; }
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

        public Qualite Qualite
        {
            get { return _Qualite; }
            set { _Qualite = value; }
        }

        public string QualiteAsString
        {
            get { return _Qualite != null ? _Qualite.Designation : string.Empty; }

        }

        public ConditionLivraison ConditionLivraison
        {
            get { return _ConditionLivraison; }
            set { _ConditionLivraison = value; }
        }

        public string ConditionLivraisonAsString
        {
            get { return _ConditionLivraison != null ? _ConditionLivraison.Designation : string.Empty; }

        }

        public TermesPaiement TermesPaiement
        {
            get { return _TermesPaiement; }
            set { _TermesPaiement = value; }
        }

        public string TermesPaiementAsString
        {
            get { return _TermesPaiement != null ? _TermesPaiement.Designation : string.Empty; }

        }

        public Conditionnement Conditionnement
        {
            get { return _Conditionnement; }
            set { _Conditionnement = value; }
        }

        public string ConditionnementAsString
        {
            get { return _Conditionnement != null ? _Conditionnement.Designation : string.Empty; }

        }

        public Origine Origine
        {
            get { return _Origine; }
            set { _Origine = value; }
        }

        public string OrigineAsString
        {
            get { return _Origine != null ? _Origine.Nom : string.Empty; }

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

        public bool IsClose
        {
            get { return _Statut == "CL"; }

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
                else if (_Statut == "CL")
                    return 3;
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

        public string ContratNumero
        {
            get
            {
                return _ContratNumero;
            }

            set
            {
                _ContratNumero = value;
            }
        }

        public Banque Banque
        {
            get
            {
                return _Banque;
            }

            set
            {
                _Banque = value;
            }
        }

        public Client Client
        {
            get
            {
                return _Client;
            }

            set
            {
                _Client = value;
            }
        }

        public decimal PrixCfa
        {
            get
            {
                return _PrixCfa;
            }

            set
            {
                _PrixCfa = value;
            }
        }

        public decimal PrixEuroCertifie
        {
            get
            {
                return _PrixEuroCertifie;
            }

            set
            {
                _PrixEuroCertifie = value;
            }
        }

        public decimal? BonusCertification
        {
            get
            {
                return _BonusCertification;
            }

            set
            {
                _BonusCertification = value;
            }
        }

        public decimal PrixFobCfa
        {
            get
            {
                return _PrixFobCfa;
            }

            set
            {
                _PrixFobCfa = value;
            }
        }

        public bool PoidsConventionnel
        {
            get
            {
                return _PoidsConventionnel;
            }

            set
            {
                _PoidsConventionnel = value;
            }
        }


        #endregion

        #region "Methods"
        public override bool fnActivate()
        {
            throw new NotImplementedException();
        }

        public override bool fnDeActivate()
        {
            throw new NotImplementedException();
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_ContratDeVentes_Get", (Guid)Id);
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

        public bool fnGetByNumber(string Number)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_ContratDeVentes_GetByNumber", (string)Number);
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
            return fnSelect("{Tous}", -1, -1, null, null, "-1");
        }

        public List<DataPersist> fnSelect(string Campagne, int ExportateurID, int CertificationID, DateTime? StartDate, DateTime? EndDate, string statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_ContratDeVentes_Select");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, CertificationID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@status", SqlDbType.VarChar, 2, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    ContratDeVentes mClass = new ContratDeVentes();
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

        public List<DataPersist> fnSelectWeight()
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_ContratDeVentes_Select");
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    ContratDeVentes mClass = new ContratDeVentes();
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

                    mCommande = db().CreateStoredProcCommand("V2_ContratDeVentes_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@NumeroTms", SqlDbType.VarChar, 8);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_ContratDeVentes_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

                    if (!string.IsNullOrEmpty(_Statut))
                        db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, _Statut);
                    else
                        db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, DBNull.Value);

                    if (!string.IsNullOrEmpty(_UtilisateurApprobation))
                        db().AddInParameter(mCommande, "@ApprovalUser", SqlDbType.VarChar, _UtilisateurApprobation);
                    else
                        db().AddInParameter(mCommande, "@ApprovalUser", SqlDbType.VarChar, DBNull.Value);
                }

                db().AddInParameter(mCommande, "@ContratNumero", SqlDbType.VarChar, 30, _ContratNumero);
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, _Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, _Exportateur.ID);

                if (_Client != null)
                    db().AddInParameter(mCommande, "@ClientID", SqlDbType.Int, _Client.ID);
                else
                    db().AddInParameter(mCommande, "@ClientID", SqlDbType.Int, DBNull.Value);

                if (_Banque != null)
                    db().AddInParameter(mCommande, "@BanqueID", SqlDbType.Int, _Banque.ID);
                else
                    db().AddInParameter(mCommande, "@BanqueID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _Date);
                db().AddInParameter(mCommande, "@DebutExpedition", SqlDbType.DateTime, _DebutExpedition);
                db().AddInParameter(mCommande, "@FinExpedition", SqlDbType.DateTime, _FinExpedition);

                //db().AddInParameter(mCommande, "@ReferenceClient", SqlDbType.VarChar, _ReferenceClient);
                db().AddInParameter(mCommande, "@Quantite", SqlDbType.Decimal, (_Quantite*1000));

                db().AddInParameter(mCommande, "@PrixEuro", SqlDbType.Money, _Prix);
                db().AddInParameter(mCommande, "@RemiseFob", SqlDbType.Money, _RemiseFob);
                db().AddInParameter(mCommande, "@PrixFobEuro", SqlDbType.Money, _PrixFob);

                db().AddInParameter(mCommande, "@PrixCfa", SqlDbType.Money, _PrixCfa);
                
                db().AddInParameter(mCommande, "@PrixFobCfa", SqlDbType.Money, _PrixFobCfa);

                db().AddInParameter(mCommande, "@ProduitExportID", SqlDbType.Int, _ProduitExport.ID);

                db().AddInParameter(mCommande, "@PoidsOptions", SqlDbType.VarChar, _PoidsOptions);
                
                db().AddInParameter(mCommande, "@Options", SqlDbType.VarChar, _Options);
                db().AddInParameter(mCommande, "@Remarques", SqlDbType.VarChar, _Remarques);

                if (_Certification != null)
                {
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, _Certification.ID);
                    db().AddInParameter(mCommande, "@PrixCertifieEuro", SqlDbType.Money, _PrixEuroCertifie);
                    db().AddInParameter(mCommande, "@bonusCertif", SqlDbType.Money, _BonusCertification);
                }
                else {
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, DBNull.Value);
                    db().AddInParameter(mCommande, "@PrixCertifieEuro", SqlDbType.Money, DBNull.Value);
                    db().AddInParameter(mCommande, "@bonusCertif", SqlDbType.Money, DBNull.Value);
                }

                
                db().AddInParameter(mCommande, "@QualiteID", SqlDbType.Int, _Qualite.ID);
                db().AddInParameter(mCommande, "@ConditionLivraisonID", SqlDbType.Int, _ConditionLivraison.ID);
                db().AddInParameter(mCommande, "@TermesPaiementID", SqlDbType.Int, _TermesPaiement.ID);
                db().AddInParameter(mCommande, "@ConditionnementID", SqlDbType.Int, _Conditionnement.ID);
                db().AddInParameter(mCommande, "@OrigineID", SqlDbType.Int, _Origine.ID);

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
                            _Numero = (string)db().Parameters(mCommande, "@NumeroTms");
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
                throw new Exception(ex.Message + "\r\n" + "V2_ContratDeVentes:fnUpdate");

            }
            return Result;
        }

        public bool fnUpdateCertif()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("V2_ContratDeVentes_ModifyCertification");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);                                                                            

                if (_Certification != null)
                {
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, _Certification.ID);
                    //db().AddInParameter(mCommande, "@PrixCertifieEuro", SqlDbType.Money, _PrixEuroCertifie);
                }
                else {
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, DBNull.Value);
                    //db().AddInParameter(mCommande, "@PrixCertifieEuro", SqlDbType.Money, DBNull.Value);
                }
                

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
                            _Numero = (string)db().Parameters(mCommande, "@NumeroTms");
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
                throw new Exception(ex.Message + "\r\n" + "V2_ContratDeVentes:fnUpdate");

            }
            return Result;
        }

        public bool fnApprove()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_ContratDeVentes_Approve");
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
                throw new Exception(ex.Message + "\r\n" + "V2_ContratDeVentes:fnApprove");
            }
            return bolResult;
        }
        public bool fnCancel()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_ContratDeVentes_Cancel");
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
                throw new Exception(ex.Message + "\r\n" + "V2_ContratDeVentes:fnCancel");
            }
            return bolResult;
        }
        public bool fnClose()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_ContratDeVentes_Close");
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
                        _Statut = "CL";
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
                throw new Exception(ex.Message + "\r\n" + "V2_ContratDeVentes:fnCancel");
            }
            return bolResult;
        }
        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _Numero;
        }

        private static void MapFromDataReader(ContratDeVentes mClass, IDataReader mDataReader)
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

                  
                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["ContratNumero"])) mClass._ContratNumero = (string)mDataReader["ContratNumero"];
                    if (!DBNull.Value.Equals(mDataReader["DateCV"])) mClass._Date = (DateTime)mDataReader["DateCV"];

                    if (!DBNull.Value.Equals(mDataReader["DebutExpedition"])) mClass._DebutExpedition = (DateTime)mDataReader["DebutExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["FinExpedition"])) mClass._FinExpedition = (DateTime)mDataReader["FinExpedition"];

                    if (!DBNull.Value.Equals(mDataReader["ReferenceClient"])) mClass._ReferenceClient = (string)mDataReader["ReferenceClient"];

                    if (!DBNull.Value.Equals(mDataReader["Quantite"])) mClass._Quantite = ((decimal)mDataReader["Quantite"]/1000);

                   
                    if (!DBNull.Value.Equals(mDataReader["PrixEuro"])) mClass._Prix = (decimal)mDataReader["PrixEuro"];
                    if (!DBNull.Value.Equals(mDataReader["RemiseFob"])) mClass._RemiseFob = (decimal)mDataReader["RemiseFob"];
                    if (!DBNull.Value.Equals(mDataReader["PrixFobEuro"])) mClass._PrixFob = (decimal)mDataReader["PrixFobEuro"];

                    if (!DBNull.Value.Equals(mDataReader["PrixCfa"])) mClass._PrixCfa = (decimal)mDataReader["PrixCfa"];
                    if (!DBNull.Value.Equals(mDataReader["PrixEuroCertifie"])) mClass._PrixEuroCertifie = (decimal)mDataReader["PrixEuroCertifie"];
                    if (!DBNull.Value.Equals(mDataReader["PrixFobCfa"])) mClass._PrixFobCfa = (decimal)mDataReader["PrixFobCfa"];

                    if (!DBNull.Value.Equals(mDataReader["Solde"])) mClass._Solde = (decimal)mDataReader["Solde"];

                    if (!DBNull.Value.Equals(mDataReader["ProduitExportID"]))
                    {
                        mClass._ProduitExport = new ProduitExport();
                        mClass._ProduitExport.ID = (int)mDataReader["ProduitExportID"];
                        mClass._ProduitExport.Designation = (string)mDataReader["ProduitExportDesignation"];
                    }
                    
                    if (!DBNull.Value.Equals(mDataReader["PoidsOptions"])) mClass._PoidsOptions = (string)mDataReader["PoidsOptions"];

                    if (!DBNull.Value.Equals(mDataReader["Options"])) mClass._Options = (string)mDataReader["Options"];
                    if (!DBNull.Value.Equals(mDataReader["Remarques"])) mClass._Remarques = (string)mDataReader["Remarques"];

                    if (!DBNull.Value.Equals(mDataReader["CertificationID"]))
                    {
                        mClass._Certification = new Certification();
                        mClass._Certification.ID = (int)mDataReader["CertificationID"];
                        mClass._Certification.Designation = (string)mDataReader["CertificationDesignation"];
                    }
                    else {
                        mClass.Certification = null;
                    }

                    if (!DBNull.Value.Equals(mDataReader["QualiteID"]))
                    {
                        mClass._Qualite = new Qualite();
                        mClass._Qualite.ID = (int)mDataReader["QualiteID"];
                        mClass._Qualite.Designation = (string)mDataReader["QualiteDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ConditionLivraisonID"]))
                    {
                        mClass._ConditionLivraison = new ConditionLivraison();
                        mClass._ConditionLivraison.ID = (int)mDataReader["ConditionLivraisonID"];
                        mClass._ConditionLivraison.Designation = (string)mDataReader["ConditionLivraisonDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["TermesPaiementID"]))
                    {
                        mClass._TermesPaiement = new TermesPaiement();
                        mClass._TermesPaiement.ID = (int)mDataReader["TermesPaiementID"];
                        mClass._TermesPaiement.Designation = (string)mDataReader["TermesPaiementDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ConditionnementID"]))
                    {
                        mClass._Conditionnement = new Conditionnement();
                        mClass._Conditionnement.ID = (int)mDataReader["ConditionnementID"];
                        mClass._Conditionnement.Designation = (string)mDataReader["ConditionnementDesignation"];
                    }


                    if (!DBNull.Value.Equals(mDataReader["OrigineID"]))
                    {
                        mClass._Origine = new Origine();
                        mClass._Origine.ID = (int)mDataReader["OrigineID"];
                        mClass._Origine.Nom = (string)mDataReader["OrigineNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ClientID"]))
                    {
                        mClass._Client = new Client();
                        mClass._Client.ID = (int)mDataReader["ClientID"];
                        mClass._Client.Nom = (string)mDataReader["ClientNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["BanqueID"]))
                    {
                        mClass._Banque = new Banque();
                        mClass._Banque.ID = (int)mDataReader["BanqueID"];
                        mClass._Banque.Designation = (string)mDataReader["BanqueDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                   

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];
                    if (!DBNull.Value.Equals(mDataReader["ApprobationUtilisateur"])) mClass._UtilisateurApprobation = (string)mDataReader["ApprobationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["ApprobationDate"])) mClass._DateApprobation = (DateTime)mDataReader["ApprobationDate"];

                    if (!DBNull.Value.Equals(mDataReader["BonusCertification"])) mClass._BonusCertification = (decimal)mDataReader["BonusCertification"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nV2_ContratDeVentes:MapFromDataReader");
            }
        }
        #endregion
    }

    public partial class ContratDeVentesViewModel
    {
        public ContratDeVentes _ContratDeVentes { get; set; }
        public string _ReportType { get; set; }

        public int? _DefaultExportateur { get; set; }
        public int? _DefaultClient { get; set; }
        public int? _DefaultConditionnement { get; set; }
        public int? _DefaultOrigine { get; set; }

        public string _PrefixeContratNum { get; set; }
        public int? _DefaultTermePaiement { get; set; }
        public string _WeightOptionText { get; set; }

        public int? _DeliveryCondition { get; set; }
        public decimal? _DefaultFobDiscountPrice { get; set; }
        public decimal? _DefBonusCertification { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }

        public int? _DefaultProduitExport { get; set; }
    }

    public partial class ContratDeVenstesReportType
    {
        public const string Balance = "Balance";
        public const string Execution = "Execution";
    }
}
