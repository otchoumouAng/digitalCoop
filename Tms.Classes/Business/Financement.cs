using Ext.Net.MVC;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Components.Data;
using System.Collections.Generic;
using Tms.Classes.Business.Sites;

namespace Tms.Classes.Business
{
    //[Proxy(Read = "~/Financement/Select")]
    //[JsonReader(RootProperty = "data")]
    public class Financement : DataPersist
    {
        #region "Fields"
        private Guid _ID;
        private string _Campagne;
        private Fournisseur _Fournisseur;
        private FinancementType _FinancementType;
        private string _Numero;
        private DateTime _DateFinancement;
        private decimal _Tonnage;
        private decimal _Prix;
        private decimal _Montant;
        private DateTime _DateEcheance;
        private PrelevementMode _PrelevementMode;
        private decimal _PrelevementTaux;
        private string _Commentaire;
        private string _Statut;
        private bool _Desactive;
        protected string _UtilisateurApprobation;
        protected string _UtilisateurRejet;
        protected DateTime? _DateApprobation;
        protected DateTime? _DateRejet;
        private string _PaymentRef;
        private decimal _PaymentTaux;
        private decimal _MontantBrut;
        private Site _Sites;
        //Used for invoicing
        private decimal _Solde;
        private decimal _MontantPreleve;       
        private decimal _PoidsNetFacture;
        private decimal _MontantDisponible;
        private bool _IsDeducted;
        private string _ListOfFinancementDeducted;
        private int _NbDeductions;
        private Engagement _Engagement;        

        private decimal _TauxFinancement;
        private decimal _BalanceTauxFinancement;
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
            get
            {
                if (_Fournisseur != null)
                    return _Fournisseur.Nom.Contains(_Fournisseur.ID.ToString()) ? _Fournisseur.Nom : _Fournisseur.Nom + " - " + _Fournisseur.ID;
                else
                    return string.Empty;
            }

        }

        public FinancementType  FinancementType
        {
            get { return _FinancementType; }
            set { _FinancementType = value; }
        }

        public string LibelleFinancementType
        {
            get { return _FinancementType != null ? _FinancementType.Designation : string.Empty; }

        }

        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        public DateTime DateFinancement
        {
            get { return _DateFinancement; }
            set { _DateFinancement = value; }
        }
        
        public decimal Tonnage
        {
            get { return _Tonnage; }
            set { _Tonnage = value; }
        }

        public string TonnageAsString
        {
            get { return _Tonnage != 0 ? String.Format("{0:#,#}", _Tonnage).TrimStart() : string.Empty; }
        }

        public string PrelevementTauxAsString
        {
            get { return _PrelevementTaux != 0 ? String.Format("{0:#,#}", _PrelevementTaux).TrimStart() : string.Empty; }
        }

        public decimal Prix
        {
            get { return _Prix; }
            set { _Prix = value; }
        }

        public string PrixAsString
        {
            get { return _Prix != 0 ? String.Format("{0:#,#}", _Prix).TrimStart() : string.Empty; }
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

        public string MontantBrutAsString
        {
            get { return _MontantBrut != 0 ? String.Format("{0:#,#}", _MontantBrut).TrimStart() : string.Empty; }
        }

        public decimal Solde
        {
            get { return _Solde; }
            set { _Solde = value; }
        }

        public string SoldeAsString
        {
            get { return _Solde != 0 ? String.Format("{0:#,#}", _Solde).TrimStart() : string.Empty; }
        }

        public decimal MontantPreleve
        {
            get { return _MontantPreleve; }
            set { _MontantPreleve = value; }
        }

        public string MontantPreleveAsString
        {
            get { return _MontantPreleve != 0 ? String.Format("{0:#,#}", _MontantPreleve).TrimStart() : string.Empty; }
        }

        public decimal Coverage
        {
            get
            {
                if (_Montant != 0 && _Montant != _Solde)
                    return (100 - Math.Round((_Solde) * 100 / _Montant, 2));
                else
                    return 0;
            }
            
        }

        public DateTime DateEcheance
        {
            get { return _DateEcheance; }
            set { _DateEcheance = value; }
        }


        public string DateEcheanceAsString
        {
            get { return _DateEcheance != null ? _DateEcheance.ToShortDateString() : string.Empty ; }
        }

        public PrelevementMode PrelevementMode
        {
            get { return _PrelevementMode; }
            set { _PrelevementMode = value; }
        }

        public string LibellePrelevementMode
        {
            get { return _PrelevementMode != null ? _PrelevementMode.Designation : string.Empty; }

        }

        public decimal PrelevementTaux
        {
            get { return _PrelevementTaux; }
            set { _PrelevementTaux = value; }
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

        public string PaymentRef
        {
            get { return _PaymentRef; }
            set { _PaymentRef = value; }
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

        public bool IsSetToLoss
        {
            get { return _Statut == "LO"; }

        }

        public bool IsRejected
        {
            get { return _Statut == "RE"; }

        }

        public bool IsPaid
        {
            get { return _PaymentRef != null; }

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
                else if (_Statut == "AP" && IsPaid)
                    return 5; // Tick
                else if (_Statut == "AP" && !IsPaid)
                    return 1; // Tick
                else if (_Statut == "LO")
                    return 4; // Tick
                else if (_Statut == "RE")
                    return 3; // Tick
                
                else
                    return 2; //  
                //if (!IsPaid)
                //{
                //    if (_Statut == "CA")
                //        return 0; // BulletCross
                //    else if (_Statut == "AP")
                //        return 1; // Tick
                //    else if (_Statut == "LO")
                //        return 4; // Tick
                //    else if (_Statut == "RE")
                //        return 3; // Tick
                //    else
                //        return 2; //           
                //}
                //else
                //    return 5;
                         
            }
        }

        public string UtilisateurApprobation
        {
            get { return _UtilisateurApprobation; }
            set { _UtilisateurApprobation = value; }
        }

        public string UtilisateurRejet
        {
            get { return _UtilisateurRejet; }
            set { _UtilisateurRejet = value; }
        }

        public DateTime? DateApprobation
        {
            get { return _DateApprobation; }
            set { _DateApprobation = value; }
        }

        public DateTime? DateRejet
        {
            get { return _DateRejet; }
            set { _DateRejet = value; }
        }
               
        public decimal PoidsNetFacture
        {
            get { return _PoidsNetFacture; }
            set { _PoidsNetFacture = value; }
        }

        public string PoidsNetFactureAsString
        {
            get { return _PoidsNetFacture != 0 ? String.Format("{0:#,#}", _PoidsNetFacture).TrimStart() : string.Empty; }
        }

        public decimal MontantDisponible
        {
            get { return _MontantDisponible; }
            set { _MontantDisponible = value; }
        }

        public string MontantDisponibleAsString
        {
            get { return _MontantDisponible != 0 ? String.Format("{0:#,#}", _MontantDisponible).TrimStart() : string.Empty; }
        }

        public bool IsDeducted
        {
            get { return _IsDeducted; }
            set { _IsDeducted = value; }
        }

        public string ListOfFinancementDeducted
        {
            get { return _ListOfFinancementDeducted; }
            set { _ListOfFinancementDeducted = value; }
        }

        public int NbDeductions
        {
            get { return _NbDeductions; }
            set { _NbDeductions = value; }
        }

        public decimal TonnageInitial
        {
            get { return (_Prix != 0 && _MontantBrut != 0) ? Math.Round(_MontantBrut / _Prix) : 0; }
        }

        public string TonnageInitialAsString
        {
            get { return TonnageInitial != 0 ? String.Format("{0:#,#}", TonnageInitial).TrimStart() : string.Empty; }
        }

        //Get fixed parameters values
        Parametres mParametres = new Parametres(0);

        public int ModeRemboursementLibre
        {
            get
            {
                return mParametres.FinancementModeRemboursementAuKilo;
            }
           
        }

        public int ModeRemboursementIntegral
        {
            get
            {
                return mParametres.FinancementModeRemboursementIntegral;
            }            
        }

        public int ModeRemboursementAuKilo
        {
            get
            {
                return mParametres.FinancementModeRemboursementAuKilo;
            }

        }

        public int ModeRemboursementFixedAmount
        {
            get
            {
                return mParametres.FinancementModeRemboursementFixedAmount;
            }

        }

        public int TypeAvance
        {
            get
            {
                return mParametres.FinancementTypeAvance;
            }

        }

        public decimal PaymentTaux
        {
            get
            {
                return _PaymentTaux;
            }

            set
            {
                _PaymentTaux = value;
            }
        }

        public decimal MontantBrut
        {
            get
            {
                return _MontantBrut;
            }

            set
            {
                _MontantBrut = value;
            }
        }


        public Site Sites
        {
            get { return _Sites; }
            set { _Sites = value; }
        }

        public string SiteAsString
        {
            get { return _Sites != null ? _Sites.Nom : string.Empty; }
        }

        public int TypeEngagement
        {
            get
            {
                return mParametres.IDEngagementFinancementType;
            }

        }

        public int SiteParDefaut
        {
            get
            {                                
                return mParametres.Site;
            }

        }

        public Engagement Engagement
        {
            get { return _Engagement; }
            set { _Engagement = value; }
        }

        public decimal TauxFinancement
        {
            get
            {
                return _TauxFinancement;
            }

            set
            {
                _TauxFinancement = value;
            }
        }

        public decimal BalanceTauxFinancement
        {
            get
            {
                return _BalanceTauxFinancement;
            }

            set
            {
                _BalanceTauxFinancement = value;
            }
        }
        #endregion

        #region Constructor
        public Financement()
        {

        }

        public Financement(Guid myID)
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
                mDataReader = db().ExecuteReader("Financement_Get", (Guid)Id);
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
                mDataReader = db().ExecuteReader("Financement_GetByNumber", (int)Number);
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
            return fnSelect("{Tous}", -1, -1,null, null, "-1", -1,"NO");
        }

        public List<DataPersist> fnSelect(string Campagne, int FournisseurID, int TypeID, DateTime? StartDate, DateTime? EndDate, string statut, int siteID = -1, string optionID = "NO")
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Financement_Select");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@TypeID", SqlDbType.Int, TypeID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@status", SqlDbType.VarChar, 2,statut);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                db().AddInParameter(mCommande, "@option", SqlDbType.VarChar, optionID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Financement mClass = new Financement();
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


        public List<DataPersist> fnSelectForApproval(string Campagne, int FournisseurID, int SiteID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Financement_SelectForApproval");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, SiteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Financement mClass = new Financement();
                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectForApproval");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectForInvoiceDeduction(int FournisseurID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Facture_Deduction_SelectAvailableFinancing");
                db().AddInParameter(mCommande, "@SupplierID", SqlDbType.Int, FournisseurID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Financement mClass = new Financement();
                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectForApproval");
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

                    mCommande = db().CreateStoredProcCommand("Financement_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 9);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("Financement_Modify");
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

                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar , 9, _Campagne);
                db().AddInParameter(mCommande, "@TypeId", SqlDbType.Int, _FinancementType.ID);
                db().AddInParameter(mCommande, "@FournisseurID", SqlDbType.Int, _Fournisseur.ID);
                db().AddInParameter(mCommande, "@PrelevementModeID", SqlDbType.Int, _PrelevementMode.ID);
                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _DateFinancement);
                db().AddInParameter(mCommande, "@DateEcheance", SqlDbType.DateTime, _DateEcheance);
                db().AddInParameter(mCommande, "@Prix", SqlDbType.Money, _Prix);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@Tonnage", SqlDbType.Decimal, _Tonnage);
                db().AddInParameter(mCommande, "@PrelevementTaux", SqlDbType.Float, _PrelevementTaux);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);

                db().AddInParameter(mCommande, "@PaymentTaux", SqlDbType.Float, _PaymentTaux);
                db().AddInParameter(mCommande, "@MontantBrut", SqlDbType.Money, _MontantBrut);

                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, _Sites.ID);

                if (_Engagement != null)
                    db().AddInParameter(mCommande, "@EngagementID", SqlDbType.UniqueIdentifier, _Engagement.ID);
                else
                    db().AddInParameter(mCommande, "@EngagementID", SqlDbType.UniqueIdentifier, DBNull.Value);

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
                throw new Exception(ex.Message + "\r\n" + "Financement:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("Financement_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 9);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("Financement_Modify");
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

                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, _Campagne);
                db().AddInParameter(mCommande, "@TypeId", SqlDbType.Int, _FinancementType.ID);
                db().AddInParameter(mCommande, "@FournisseurID", SqlDbType.Int, _Fournisseur.ID);
                db().AddInParameter(mCommande, "@PrelevementModeID", SqlDbType.Int, _PrelevementMode.ID);
                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _DateFinancement);
                db().AddInParameter(mCommande, "@DateEcheance", SqlDbType.DateTime, _DateEcheance);
                db().AddInParameter(mCommande, "@Prix", SqlDbType.Money, _Prix);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@Tonnage", SqlDbType.Decimal, _Tonnage);
                db().AddInParameter(mCommande, "@PrelevementTaux", SqlDbType.Float, _PrelevementTaux);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);

                db().AddInParameter(mCommande, "@PaymentTaux", SqlDbType.Float, _PaymentTaux);
                db().AddInParameter(mCommande, "@MontantBrut", SqlDbType.Money, _MontantBrut);

                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, _Sites.ID);

                if (_Engagement != null)
                    db().AddInParameter(mCommande, "@EngagementID", SqlDbType.UniqueIdentifier, _Engagement.ID);
                else
                    db().AddInParameter(mCommande, "@EngagementID", SqlDbType.UniqueIdentifier, DBNull.Value);

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
                throw new Exception(ex.Message + "\r\n" + "Financement:fnUpdate");

            }
            return Result;
        }

        public bool fnUpdateAndApprove(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("Financement_ModifyAndApprove");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, _Campagne);
                db().AddInParameter(mCommande, "@TypeId", SqlDbType.Int, _FinancementType.ID);
                db().AddInParameter(mCommande, "@FournisseurID", SqlDbType.Int, _Fournisseur.ID);
                db().AddInParameter(mCommande, "@PrelevementModeID", SqlDbType.Int, _PrelevementMode.ID);
                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _DateFinancement);
                db().AddInParameter(mCommande, "@DateEcheance", SqlDbType.DateTime, _DateEcheance);
                db().AddInParameter(mCommande, "@Prix", SqlDbType.Money, _Prix);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@Tonnage", SqlDbType.Decimal, _Tonnage);
                db().AddInParameter(mCommande, "@PrelevementTaux", SqlDbType.Float, _PrelevementTaux);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);

                db().AddInParameter(mCommande, "@PaymentTaux", SqlDbType.Float, _PaymentTaux);
                db().AddInParameter(mCommande, "@MontantBrut", SqlDbType.Money, _MontantBrut);

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
                throw new Exception(ex.Message + "\r\n" + "Financement:fnUpdate");

            }
            return Result;
        }


        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("Financement_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "Financement:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("Financement_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "Financement:fnDeActivate");
            }
            return bolResult;
        }

        public bool fnCancel()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("Financement_Cancel");
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
                throw new Exception(ex.Message + "\r\n" + "Financement:fnCancel");
            }
            return bolResult;
        }

        public bool fnCancel(DataTransaction mTran)
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("Financement_Cancel");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
            db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
            db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
            db().AddInParameter(mCommande, "@CancelUser", SqlDbType.VarChar, _UtilisateurModification);
            try
            {
                db().ExecuteNonQuery(ref mCommande, mTran);
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
                throw new Exception(ex.Message + "\r\n" + "Financement:fnCancel");
            }
            return bolResult;
        }


        public bool fnSetToLoss()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("Financement_SetToLoss");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
            db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
            db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
            db().AddInParameter(mCommande, "@SetToLossUser", SqlDbType.VarChar, _UtilisateurModification);
            try
            {
                db().ExecuteNonQuery(ref mCommande);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        bolResult = true;
                        _Statut = "LO";
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
                throw new Exception(ex.Message + "\r\n" + "Financement:fnSetToLoss");
            }
            return bolResult;
        }

        public bool fnApprove()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("Financement_Approve");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
            db().AddInParameter(mCommande, "@PaymentTaux", SqlDbType.Float, _PaymentTaux);
            db().AddInParameter(mCommande, "@PrelevementTaux", SqlDbType.Float, _PrelevementTaux);
            db().AddInParameter(mCommande, "@MontantBrut", SqlDbType.Money, _MontantBrut);
            db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);
            db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
            db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
            db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
            db().AddInParameter(mCommande, "@ApprovalUser", SqlDbType.VarChar, _UtilisateurApprobation);
            db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
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
                throw new Exception(ex.Message + "\r\n" + "Financement:fnApprove");
            }
            return bolResult;
        }

        public bool fnApprove(DataTransaction mTran)
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("Financement_Approve");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
            db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
            db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
            db().AddInParameter(mCommande, "@ApprovalUser", SqlDbType.VarChar, _UtilisateurApprobation);
            try
            {
                db().ExecuteNonQuery(ref mCommande, mTran);
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
                throw new Exception(ex.Message + "\r\n" + "Financement:fnApprove");
            }
            return bolResult;
        }

        public bool fnReject()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("Financement_Reject");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
            db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
            db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
            db().AddInParameter(mCommande, "@RejectUser", SqlDbType.VarChar, _UtilisateurRejet);
            try
            {
                db().ExecuteNonQuery(ref mCommande);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        bolResult = true;
                        _Statut = "RE";
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
                throw new Exception(ex.Message + "\r\n" + "Financement:fnReject");
            }
            return bolResult;
        }

        public bool fnReject(DataTransaction dt)
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("Financement_Reject");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
            db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
            db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
            db().AddInParameter(mCommande, "@RejectUser", SqlDbType.VarChar, _UtilisateurRejet);
            try
            {
                db().ExecuteNonQuery(ref mCommande, dt);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        bolResult = true;
                        _Statut = "RE";
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
                throw new Exception(ex.Message + "\r\n" + "Financement:fnReject");
            }
            return bolResult;
        }

        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _Numero;
        }

        private static void MapFromDataReader(Financement mClass, IDataReader mDataReader)
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
                        mClass._FinancementType = new FinancementType();
                        mClass._FinancementType.ID = (int)mDataReader["TypeID"];
                        mClass._FinancementType.Designation = (string)mDataReader["TypeNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["PrelevementModeID"]))
                    {
                        mClass._PrelevementMode = new PrelevementMode();
                        mClass._PrelevementMode.ID = (int)mDataReader["PrelevementModeID"];
                        mClass._PrelevementMode.Designation = (string)mDataReader["PrelevementModeNom"];
                    }
                                                          
                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DateFinancement"])) mClass._DateFinancement = (DateTime)mDataReader["DateFinancement"];
                    if (!DBNull.Value.Equals(mDataReader["Tonnage"])) mClass._Tonnage = (decimal)mDataReader["Tonnage"];
                    if (!DBNull.Value.Equals(mDataReader["Prix"])) mClass._Prix = (decimal)mDataReader["Prix"];
                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._Montant = (decimal)mDataReader["Montant"];
                    if (!DBNull.Value.Equals(mDataReader["Solde"])) mClass._Solde = (decimal)mDataReader["Solde"];
                    if (!DBNull.Value.Equals(mDataReader["DateEcheance"])) mClass._DateEcheance = (DateTime)mDataReader["DateEcheance"];
                    if (!DBNull.Value.Equals(mDataReader["RemboursementTaux"])) mClass._PrelevementTaux = (decimal)mDataReader["RemboursementTaux"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["PaymentRef"])) mClass._PaymentRef = (string)mDataReader["PaymentRef"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];
                    if (!DBNull.Value.Equals(mDataReader["ApprobationUtilisateur"])) mClass._UtilisateurApprobation = (string)mDataReader["ApprobationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["ApprobationDate"])) mClass._DateApprobation = (DateTime)mDataReader["ApprobationDate"];
                    if (!DBNull.Value.Equals(mDataReader["RejetUtilisateur"])) mClass._UtilisateurRejet = (string)mDataReader["RejetUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RejetDate"])) mClass._DateRejet = (DateTime)mDataReader["RejetDate"];

                    if (!DBNull.Value.Equals(mDataReader["PaymentTaux"])) mClass._PaymentTaux = (decimal)mDataReader["PaymentTaux"];
                    if (!DBNull.Value.Equals(mDataReader["MontantBrut"])) mClass._MontantBrut = (decimal)mDataReader["MontantBrut"];

                    if (!DBNull.Value.Equals(mDataReader["SiteID"]))
                    {
                        mClass._Sites = new Site();
                        mClass._Sites.ID = (int)mDataReader["SiteID"];
                        mClass._Sites.Nom = (string)mDataReader["SiteNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["IDEngagement"]))
                    {
                        mClass._Engagement = new Engagement();
                        mClass._Engagement.ID = (Guid)mDataReader["IDEngagement"];
                        mClass._Engagement.Numero = (string)mDataReader["NumeroEngagement"];
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nFinancement:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderLight(Financement mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["PrelevementModeID"]))
                    {
                        mClass._PrelevementMode = new PrelevementMode();
                        mClass._PrelevementMode.ID = (int)mDataReader["PrelevementModeID"];
                        mClass._PrelevementMode.Designation = (string)mDataReader["PrelevementModeNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["FournisseurID"]))
                    {
                        mClass._Fournisseur = new Fournisseur();
                        mClass._Fournisseur.ID = (int)mDataReader["FournisseurID"];
                        mClass._Fournisseur.Nom = (string)mDataReader["FournisseurNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DateFinancement"])) mClass._DateFinancement = (DateTime)mDataReader["DateFinancement"];
                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._Montant = (decimal)mDataReader["Montant"];
                    if (!DBNull.Value.Equals(mDataReader["Solde"])) mClass._Solde = (decimal)mDataReader["Solde"];
                    if (!DBNull.Value.Equals(mDataReader["MontantPreleve"])) mClass._MontantPreleve = (decimal)mDataReader["MontantPreleve"];
                    if (!DBNull.Value.Equals(mDataReader["RemboursementTaux"])) mClass._PrelevementTaux = (decimal)mDataReader["RemboursementTaux"];
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nFinancement:MapFromDataReaderLight");
            }
        }

        #endregion

    }

    public partial class FinancementViewModel
    {
        public Financement _Financement { get; set; }
        public string _ReportType { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }

    public partial class FinancementReportType
    {
        public const string Balance = "Balance";
        public const string Execution = "Execution";
        public const string Exposure = "Exposure";
        public const string FinancialStatus = "FinancialStatus";
        public const string FinancialStatusCrop = "FinancialStatusCrop";

        //public enum TypeReport
        //{
        //    Balance = 0,
        //    Execution = 1,
        //    Exposure = 2
        //};
    }

    


}
