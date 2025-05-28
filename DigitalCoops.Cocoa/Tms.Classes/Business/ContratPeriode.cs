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
    //[Proxy(Read ="~/ContratPeriode/Select")]
    //[JsonReader(RootProperty = "data")]
    public class ContratPeriode : DataPersist
    {
        #region "Fields"

        private Guid _ID;
        private Campagne _Campagne;
        private Fournisseur _Fournisseur;
        private ContratPeriodeType _ContratPeriodeType;
        private Financement _Financement;
        private string _Numero;
        private DateTime _DateContrat;
        private DateTime _DateDebut;
        private DateTime _DateEcheance;
        private decimal _Tonnage;
        private decimal _Prix;
        private string _Commentaire;
        private string _Statut;
        private bool _Desactive;
        private decimal _Balance;
        private decimal _Coverage;
        private Guid? _financementID;
        private bool _EstRelance;
        private Site _Sites;
        private decimal _Montant;
        private decimal _PrLivre;
        private decimal _MontantPrime;
        private decimal _PrixBrut;
        private string _StatutText;
        private string _UtilisateurApprobation;
        private DateTime _ApprobationDate;
        private string _UtilisateurRegeneration;
        private DateTime _RegenerationDate;
        #endregion

        #region "Properties"

        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        [Column(Text = "Campagne", Hidden = true)]
        public Campagne Campagne
        {
            get { return _Campagne; }
            set { _Campagne = value; }
        }


        [Column(Text = "Fournisseur", Hidden = true)]
        public Fournisseur Fournisseur
        {
            get { return _Fournisseur; }
            set { _Fournisseur = value; }
        }

        public string FournisseurNameAndCode
        {
            get {
                if (_Fournisseur != null)                
                    return _Fournisseur.Nom.Contains(_Fournisseur.ID.ToString()) ? _Fournisseur.Nom : _Fournisseur.Nom + " - " + _Fournisseur.ID;
                else
                    return string.Empty;

            }
        }

        [Column(Text = "ContratPeriodeType", Hidden = true)]
        public ContratPeriodeType ContratPeriodeType
        {
            get { return _ContratPeriodeType; }
            set { _ContratPeriodeType = value; }
        } 

        public string ContratPeriodeTypeAsString
        {
            get { return _ContratPeriodeType != null ? _ContratPeriodeType.AsString : string.Empty; }
            
        }


        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        public DateTime DateContrat
        {
            get { return _DateContrat; }
            set { _DateContrat = value; }
        }

        public string DateContratAsString
        {
            get { return _DateContrat.ToShortDateString(); }            
        }


        public DateTime DateDebut
        {
            get { return _DateDebut; }
            set { _DateDebut = value; }
        }

        public string DateDebutAsString
        {
            get { return _DateDebut.ToShortDateString(); }            
        }

        public DateTime DateEcheance
        {
            get { return _DateEcheance; }
            set { _DateEcheance = value; }
        }

        public string DateEcheanceAsString
        {
            get { return _DateEcheance.ToShortDateString(); }            
        }

        public decimal Tonnage
        {
            get { return _Tonnage; }
            set { _Tonnage = value; }
        }

        public string TonnageAsString
        {
            get { return _Tonnage != 0 ? String.Format("{0:#,#}", _Tonnage).TrimStart() : "0"; }
           
        }

        public string BalanceAsString
        {
            get { return _Balance != 0 ? String.Format("{0:#,#}", _Balance).TrimStart() : "0"; }

        }

        public string CoverageAsString
        {
            get { return _Coverage != 0 ? String.Format("{0:0.00}", _Coverage).TrimStart() : "0.0"; }

        }

        public string PrLivreAsString
        {
            get {
                if (_PrLivre != 0 && _PrLivre >= (decimal)100)
                    return String.Format("{0:0.00}", (decimal)100).TrimStart();
                else if (_PrLivre != 0 && _PrLivre < (decimal)100)
                    return String.Format("{0:0.00}", _PrLivre).TrimStart();
                else
                    return "0.0";

            }

        }

        public string TonnageKgToTone
        {
            get { return (_Tonnage / 1000).ToString(); }            
        }


        public decimal Prix
        {
            get { return _Prix; }
            set { _Prix = value; }
        }

        public string PrixAsString
        {
            get { return _Prix !=  0 ? String.Format("{0:#,#}", _Prix).TrimStart() : "0" ; }            
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

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desactive)
                    return 0; // BulletCross
                else if (_Statut == "AP" && _EstRelance == false)
                    return 1; // Tick
                else if (_Statut == "AP" && _EstRelance == true)
                    return 6; // Tick
                else if (_Statut == "NA" && _EstRelance == true)
                    return 5; // Tick
                else if (_Statut == "CL")
                    return 3;
                else if (_Statut == "RG")
                    return 7;
                else
                    return 2;
            }
        }

        public decimal Balance
        {
            get
            {
                return _Balance;
            }

            set
            {
                _Balance = value;
            }
        }

        public decimal Coverage
        {
            get
            {
                return _Coverage;
            }

            set
            {
                _Coverage = value;
            }
        }

        public Financement Financement
        {
            get
            {
                return _Financement;
            }

            set
            {
                _Financement = value;
            }
        }

        public Guid? FinancementID
        {
            get
            {
                return _financementID;
            }

            set
            {
                _financementID = value;
            }
        }

        public bool EstRelance
        {
            get
            {
                return _EstRelance;
            }

            set
            {
                _EstRelance = value;
            }
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

        public decimal Montant
        {
            get
            {
                return _Montant;
            }

            set
            {
                _Montant = value;
            }
        }

        public string MontantAsString
        {
            get { return _Montant != 0 ? String.Format("{0:#,#}", _Montant).TrimStart() : "0"; }

        }

        public string SiteAsString
        {
            get { return _Sites != null ? _Sites.Nom : string.Empty; }

        }

        public decimal PrLivre
        {
            get
            {
                return _PrLivre;
            }

            set
            {
                _PrLivre = value;
            }
        }

        public decimal MontantPrime
        {
            get { return _MontantPrime; }
            set { _MontantPrime = value; }
        }

        public string MontantPrimeAsString
        {
            get { return _MontantPrime != 0 ? String.Format("{0:#,#}", _MontantPrime).TrimStart() : "0"; }

        }

        public decimal PrixBrut
        {
            get { return _PrixBrut; }
            set { _PrixBrut = value; }
        }

        public string PrixBrutAsString
        {
            get { return _PrixBrut != 0 ? String.Format("{0:#,#}", _PrixBrut).TrimStart() : "0"; }

        }

        public string StatutText
        {
            get { return _StatutText; }
            set { _StatutText = value; }
        }

        public string UtilisateurApprobation
        {
            get
            {
                return _UtilisateurApprobation;
            }

            set
            {
                _UtilisateurApprobation = value;
            }
        }

        public DateTime ApprobationDate
        {
            get
            {
                return _ApprobationDate;
            }

            set
            {
                _ApprobationDate = value;
            }
        }

        public string UtilisateurRegeneration
        {
            get
            {
                return _UtilisateurRegeneration;
            }

            set
            {
                _UtilisateurRegeneration = value;
            }
        }

        public DateTime RegenerationDate
        {
            get
            {
                return _RegenerationDate;
            }

            set
            {
                _RegenerationDate = value;
            }
        }
        #endregion

        #region "Constructor"

        public ContratPeriode()
        {

        }

        public ContratPeriode(Guid myId)
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
                mDataReader = db().ExecuteReader("ContratPeriode_Get", (Guid)Id);
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

        public bool fnGetByFinancing(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("ContratPeriode_GetByFinancing", (Guid)Id);
                if (mDataReader.Read())
                {
                    MapFromDataReader(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetByFinancing");
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

        public List<DataPersist> fnSelect(string cropyear, int fournisseur, int typecontrat, DateTime? startdate, DateTime? enddate, string statut, int siteID = -1, string optionID = "NO")
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("ContratPeriode_Select");
                db().AddInParameter(mCommande, "@cropyear", SqlDbType.Char, cropyear);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, fournisseur);
                db().AddInParameter(mCommande, "@typecontrat", SqlDbType.Int, typecontrat);
                db().AddInParameter(mCommande, "@startdate", SqlDbType.DateTime, startdate);
                db().AddInParameter(mCommande, "@enddate", SqlDbType.DateTime, enddate);
                db().AddInParameter(mCommande, "@status", SqlDbType.Char, statut);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                db().AddInParameter(mCommande, "@option", SqlDbType.VarChar, optionID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    ContratPeriode mClass = new ContratPeriode();

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

                    mCommande = db().CreateStoredProcCommand("ContratPeriode_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("ContratPeriode_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                    if (!string.IsNullOrEmpty(_Statut))
                        db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, _Statut);
                    else
                        db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, DBNull.Value);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, _Campagne.Designation);
                db().AddInParameter(mCommande, "@FournisseurID", SqlDbType.Int, _Fournisseur.ID);
                db().AddInParameter(mCommande, "@TypeID", SqlDbType.Int, _ContratPeriodeType.ID);
                db().AddInParameter(mCommande, "@DateContrat", SqlDbType.DateTime, _DateContrat);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, _DateDebut);
                db().AddInParameter(mCommande, "@DateEcheance", SqlDbType.DateTime, _DateEcheance);
                db().AddInParameter(mCommande, "@Tonnage", SqlDbType.Decimal, _Tonnage);
                db().AddInParameter(mCommande, "@prix", SqlDbType.Money, _Prix);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Decimal, _Montant);
                db().AddInParameter(mCommande, "@commentaire", SqlDbType.VarChar, _Commentaire);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, _Sites.ID);

                db().AddInParameter(mCommande, "@mPrime", SqlDbType.Money, _MontantPrime);
                db().AddInParameter(mCommande, "@prixBrut", SqlDbType.Money, _PrixBrut);

                if (_financementID != null)
                    db().AddInParameter(mCommande, "@FinancementID", SqlDbType.UniqueIdentifier, _financementID);
                else
                    db().AddInParameter(mCommande, "@FinancementID", SqlDbType.UniqueIdentifier, DBNull.Value);
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
                            _Statut = "NA";
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
                throw new Exception(ex.Message + "\r\n" + "ContratPeriode:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("ContratPeriode_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("ContratPeriode_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                    if (!string.IsNullOrEmpty(_Statut))
                        db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, _Statut);
                    else
                        db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, DBNull.Value);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, _Campagne.Designation);
                db().AddInParameter(mCommande, "@FournisseurID", SqlDbType.Int, _Fournisseur.ID);
                db().AddInParameter(mCommande, "@TypeID", SqlDbType.Int, _ContratPeriodeType.ID);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, _Sites.ID);
                db().AddInParameter(mCommande, "@DateContrat", SqlDbType.DateTime, _DateContrat);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, _DateDebut);
                db().AddInParameter(mCommande, "@DateEcheance", SqlDbType.DateTime, _DateEcheance);
                db().AddInParameter(mCommande, "@Tonnage", SqlDbType.Decimal, _Tonnage);
                db().AddInParameter(mCommande, "@prix", SqlDbType.Money, _Prix);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Decimal, _Montant);
                db().AddInParameter(mCommande, "@commentaire", SqlDbType.VarChar, _Commentaire);

                db().AddInParameter(mCommande, "@mPrime", SqlDbType.Money, _MontantPrime);
                db().AddInParameter(mCommande, "@prixBrut", SqlDbType.Money, _PrixBrut);

                if (_financementID != null)
                db().AddInParameter(mCommande, "@FinancementID", SqlDbType.UniqueIdentifier, _financementID);
                else
                    db().AddInParameter(mCommande, "@FinancementID", SqlDbType.UniqueIdentifier, DBNull.Value);
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
                            _Statut = "NA";
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
                throw new Exception(ex.Message + "\r\n" + "ContratPeriode:fnUpdate");

            }
            return Result;
        }


        public bool fnExtend()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("ContratPeriode_Extend");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                
                db().AddInParameter(mCommande, "@FournisseurID", SqlDbType.Int, _Fournisseur.ID);                                
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, _DateDebut);
                db().AddInParameter(mCommande, "@DateEcheance", SqlDbType.DateTime, _DateEcheance);                                                                

                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);

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
                        //_Statut = "NA";

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
                throw new Exception(ex.Message + "\r\n" + "ContratPeriode:fnUpdate");

            }
            return Result;
        }

        public bool fnRegenerate()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("ContratPeriode_ReGenerate");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

                db().AddInParameter(mCommande, "@FournisseurID", SqlDbType.Int, _Fournisseur.ID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, _DateDebut);
                db().AddInParameter(mCommande, "@DateEcheance", SqlDbType.DateTime, _DateEcheance);

                db().AddInParameter(mCommande, "@Tonnage", SqlDbType.Decimal, _Tonnage);
                db().AddInParameter(mCommande, "@prix", SqlDbType.Money, _Prix);
                db().AddInParameter(mCommande, "@commentaire", SqlDbType.VarChar, _Commentaire);

                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);

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
                        _EstRelance = true;
                        _isnew = false;
                        _Statut = "RG";
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
                throw new Exception(ex.Message + "\r\n" + "ContratPeriode:ContratPeriode_ReGenerate");

            }
            return Result;
        }


        public override bool fnActivate()
        {

            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("ContratPeriode_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "ContratPeriode:fnActivate");
            }
            return Result;

        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("ContratPeriode_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "ContratPeriode:fnDeActivate");
            }
            return bolResult;
        }

        public bool fnClose()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("ContratPeriode_Close");
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
                        Desactive = false;
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
                throw new Exception(ex.Message + "\r\n" + "ContratPeriode:fnClose");
            }
            return bolResult;
        }


        public bool fnDeActivate(DataTransaction mTran)
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("ContratPeriode_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "ContratPeriode:fnDeActivate");
            }
            return bolResult;
        }

        public bool fnApprove()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("ContratPeriode_Approve");
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
                        Desactive = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        _Statut = "AP";
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
                throw new Exception(ex.Message + "\r\n" + "PrixNegocie:fnActivate");
            }
            return Result;
        }

        public bool fnApprove(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("ContratPeriode_Approve");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
            db().AddOutParameter(mCommande, "@statut", SqlDbType.Char, 2);
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
                        Result = true;
                        Desactive = false;
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
                throw new Exception(ex.Message + "\r\n" + "PrixNegocie:fnActivate");
            }
            return Result;
        }

        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _Numero;
        }

        private static void MapFromDataReader(ContratPeriode mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    mClass._Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass._Campagne.Designation = (string)mDataReader["CampagneID"];

                    mClass._Fournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["FournisseurID"])) mClass._Fournisseur.ID = (int)mDataReader["FournisseurID"];

                    mClass._ContratPeriodeType = new ContratPeriodeType();
                    if (!DBNull.Value.Equals(mDataReader["TypeID"])) mClass._ContratPeriodeType.ID = (int)mDataReader["TypeID"];
                    
                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DateContrat"])) mClass._DateContrat = (DateTime)mDataReader["DateContrat"];
                    if (!DBNull.Value.Equals(mDataReader["DateDebut"])) mClass._DateDebut = (DateTime)mDataReader["DateDebut"];
                    if (!DBNull.Value.Equals(mDataReader["DateEcheance"])) mClass._DateEcheance = (DateTime)mDataReader["DateEcheance"];
                    if (!DBNull.Value.Equals(mDataReader["Tonnage"])) mClass._Tonnage = (decimal)mDataReader["Tonnage"];
                    if (!DBNull.Value.Equals(mDataReader["Prix"])) mClass._Prix = (decimal)mDataReader["Prix"];
                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._Montant = (decimal)mDataReader["Montant"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Balance"])) mClass._Balance = (decimal)mDataReader["Balance"];
                    if (!DBNull.Value.Equals(mDataReader["Coverage"])) mClass._Coverage = (decimal)mDataReader["Coverage"];
                    if (!DBNull.Value.Equals(mDataReader["FinancementID"])) mClass._financementID = (Guid?)mDataReader["FinancementID"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                    if (!DBNull.Value.Equals(mDataReader["NomFournisseur"])) mClass._Fournisseur.Nom = (string)mDataReader["NomFournisseur"];
                    if (!DBNull.Value.Equals(mDataReader["TypeDesignation"])) mClass._ContratPeriodeType.Designation = (string)mDataReader["TypeDesignation"];
                    if (!DBNull.Value.Equals(mDataReader["EstRelance"])) mClass._EstRelance = (bool)mDataReader["EstRelance"];

                    mClass._Sites = new Site();
                    if (!DBNull.Value.Equals(mDataReader["SiteID"])) mClass._Sites.ID = (int)mDataReader["SiteID"];
                    if (!DBNull.Value.Equals(mDataReader["SiteNom"])) mClass._Sites.Nom = (string)mDataReader["SiteNom"];

                    if (!DBNull.Value.Equals(mDataReader["PrLivre"])) mClass._PrLivre = (decimal)mDataReader["PrLivre"];
                    if (!DBNull.Value.Equals(mDataReader["MontantPrime"])) mClass._MontantPrime = (decimal)mDataReader["MontantPrime"];
                    if (!DBNull.Value.Equals(mDataReader["PrixBrut"])) mClass._PrixBrut = (decimal)mDataReader["PrixBrut"];

                    if (!DBNull.Value.Equals(mDataReader["DateApprobation"])) mClass._ApprobationDate = (DateTime)mDataReader["DateApprobation"];
                    if (!DBNull.Value.Equals(mDataReader["UtilisateurApprobation"])) mClass._UtilisateurApprobation = (string)mDataReader["UtilisateurApprobation"];

                    if (!DBNull.Value.Equals(mDataReader["DateRegeneration"])) mClass._RegenerationDate = (DateTime)mDataReader["DateRegeneration"];
                    if (!DBNull.Value.Equals(mDataReader["UtilisateurRegeneration"])) mClass._UtilisateurRegeneration = (string)mDataReader["UtilisateurRegeneration"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nContratPeriode:MapFromDataReader");
            }
        }
        #endregion

    }

    public partial class ContratPeriodeViewModel
    {
        public ContratPeriode _ContratPeriode { get; set; }
        public TypeContratPeriodeReport _TypeContratPeriodeReport { get; set; }
        public Parametres _Parametres { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }

    public partial class TypeContratPeriodeReport
    {
        public string Designation { get; set; }
    }
}
