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
    public class FactureCommerciale : DataPersist
    {
        #region "Fields"
        private Guid _ID;
        private Embarquement _Embarquement;
        private CompteBancaire _CompteBancaire;
        private string _Numero;
        private DateTime _Date;
        private int _NbreSacs;
        private decimal _PoidsBrutTheorique;
        private decimal _PoidsNetTheorique;
        private decimal _PrixCAF;
        private decimal _PrixFob;
        private decimal _MontantBrut;
        private decimal _MontantAT;
        private decimal _DeductionFinancementBlank;
        private decimal _DeductionFinancementAPC;
        private decimal _AutresDeduction;
        private decimal _MontantNet;

        private string _Statut;
        private bool _Desactive;
        protected string _UtilisateurApprobation;
        protected string _UtilisateurRejet;
        protected DateTime? _DateApprobation;
        protected DateTime? _DateRejet;
        #endregion

        #region "Properties"
        [ModelField(IDProperty = true, SortType = Ext.Net.SortTypeMethod.None, SortDir = Ext.Net.SortDirection.ASC)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }


        public Embarquement Embarquement
        {
            get { return _Embarquement; }
            set { _Embarquement = value; }
        }

        public string EmbarquementAsString
        {
            get
            {
                if (_Embarquement != null)
                    return _Embarquement.Numero;
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

       
        public int NbreSacs
        {
            get { return _NbreSacs; }
            set { _NbreSacs = value; }
        }

        public string NbreSacsAsString
        {
            get { return _NbreSacs != 0 ? String.Format("{0:#,#}", _NbreSacs).TrimStart() : string.Empty; }
        }

        public decimal PoidsBrutTheorique
        {
            get { return _PoidsBrutTheorique; }
            set { _PoidsBrutTheorique = value; }
        }

        public string PoidsBrutTheoriqueAsString
        {
            get { return _PoidsBrutTheorique != 0 ? String.Format("{0:#,#}", _PoidsBrutTheorique).TrimStart() : string.Empty; }
        }

        public decimal PoidsNetTheorique
        {
            get { return _PoidsNetTheorique; }
            set { _PoidsNetTheorique = value; }
        }

        public string PoidsNetTheoriqueAsString
        {
            get { return _PoidsNetTheorique != 0 ? String.Format("{0:#,#}", _PoidsNetTheorique).TrimStart() : string.Empty; }
        }

        public decimal PrixCAF
        {
            get { return _PrixCAF; }
            set { _PrixCAF = value; }
        }

        public string PrixCAFAsString
        {
            get { return _PrixCAF != 0 ? String.Format("{0:#,##0.##}", _PrixCAF).TrimStart() : string.Empty; }
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
        public decimal MontantBrut
        {
            get { return _MontantBrut; }
            set { _MontantBrut = value; }
        }

        public string MontantBrutAsString
        {
            get { return _MontantBrut != 0 ? String.Format("{0:#,##0.##}", _MontantBrut).TrimStart() : string.Empty; }
        }

        public decimal MontantAT
        {
            get { return Math.Round((_MontantBrut*99)/100,3); }
            set { _MontantAT = value; }
        }

        public string MontantATAsString
        {
            get { return _MontantAT != 0 ? String.Format("{0:#,##0.###}", _MontantAT).TrimStart() : string.Empty; }
        }


        public decimal DeductionFinancementBlank
        {
            get { return _DeductionFinancementBlank; }
            set { _DeductionFinancementBlank = value; }
        }

        public string DeductionFinancementBlankAsString
        {
            get { return _DeductionFinancementBlank != 0 ? String.Format("{0:#,##0.###}", _DeductionFinancementBlank).TrimStart() : string.Empty; }
        }

        public decimal DeductionFinancementAPC
        {
            get { return _DeductionFinancementAPC; }
            set { _DeductionFinancementAPC = value; }
        }

        public string DeductionFinancementAPCAsString
        {
            get { return _DeductionFinancementAPC != 0 ? String.Format("{0:#,##0.###}", _DeductionFinancementAPC).TrimStart() : string.Empty; }
        }

        public decimal AutresDeduction
        {
            get { return _AutresDeduction; }
            set { _AutresDeduction = value; }
        }

        public string AutresDeductionAsString
        {
            get { return _AutresDeduction != 0 ? String.Format("{0:#,##0.###}", _AutresDeduction).TrimStart() : string.Empty; }
        }

        public decimal MontantNet
        {
            get { return _MontantNet; }
            set { _MontantNet = value; }
        }

        public string MontantNetAsString
        {
            get { return _MontantNet != 0 ? String.Format("{0:#,##0.###}", _MontantNet).TrimStart() : string.Empty; }
        }

        public CompteBancaire CompteBancaire
        {
            get { return _CompteBancaire; }
            set { _CompteBancaire = value; }
        }

        public string CompteBancaireAsString
        {
            get { return _CompteBancaire != null ? _CompteBancaire.Numero : string.Empty; }

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

        public bool IsRejected
        {
            get { return _Statut == "RE"; }

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
                if (_Statut == "CA")
                    return 0; // BulletCross
                else if (_Statut == "AP")
                    return 1; // Tick
                else if (_Statut == "LO")
                    return 4; // Tick
                else if (_Statut == "RE")
                    return 3; // Tick

                else
                    return 2; //  

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

        public string NumeroContratAsString
        {
            get
            {
                if (_Embarquement != null && _Embarquement.Contrat != null)
                    return _Embarquement.Contrat.ContratNumero;
                else
                    return string.Empty;
            }

        }
        #endregion

        #region Constructor
        public FactureCommerciale()
        {

        }

        public FactureCommerciale(Guid MyId)
        {
            this.fnGet(MyId);
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
                mDataReader = db().ExecuteReader("V2_FactureCommerciale_Get", (Guid)Id);
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
                mDataReader = db().ExecuteReader("V2_FactureCommerciale_GetByNumber", (string)Number);
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
            return fnSelect("", -1, -1, null, null, "%%");
        }

        public List<DataPersist> fnSelect(string Campagne, int ExportateurID, int CertificationID, DateTime? StartDate, DateTime? EndDate, string statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_FactureCommerciale_Select");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, CertificationID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@Status", SqlDbType.VarChar, 2, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FactureCommerciale mClass = new FactureCommerciale();
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

        public bool fnRemovePrefinancingAll()
        {
            if (!this._isnew)
            {
                bool bolResult;
                DataCommand mCommande = db().CreateStoredProcCommand("V2_FactureCommerciale_RemovePrefinancingAll");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
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
                    throw new Exception(ex.Message + "\r\n" + "FactureCommerciale:fnRemovePrefinancingAll");
                }
                return bolResult;
            }
            return false;
        }

        public bool fnRemoveDeductionAll()
        {
            if (!this._isnew)
            {
                bool bolResult;
                DataCommand mCommande = db().CreateStoredProcCommand("V2_FactureCommerciale_RemoveDeductionAll");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
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
                    throw new Exception(ex.Message + "\r\n" + "FactureCommerciale:fnRemoveDeductionAll");
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

                    mCommande = db().CreateStoredProcCommand("V2_FactureCommerciale_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 13);
                    db().AddInParameter(mCommande, "@EmbarquementID", SqlDbType.UniqueIdentifier, _Embarquement.ID);
                    db().AddInParameter(mCommande, "@EmbarquementNumero", SqlDbType.VarChar, _Embarquement.Numero);
                    db().AddInParameter(mCommande, "@NbreSacs", SqlDbType.Int, _NbreSacs);
                    db().AddInParameter(mCommande, "@PoidsBrutTheorique", SqlDbType.Decimal, _PoidsBrutTheorique);
                    db().AddInParameter(mCommande, "@PoidsNetTheorique", SqlDbType.Decimal, _PoidsNetTheorique);
                    db().AddInParameter(mCommande, "@PrixCAF", SqlDbType.Money, _PrixCAF);
                    db().AddInParameter(mCommande, "@PrixFob", SqlDbType.Money, _PrixFob);
                    db().AddInParameter(mCommande, "@MontantBrut", SqlDbType.Money, _MontantBrut);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_FactureCommerciale_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

                }

                db().AddInParameter(mCommande, "@CompteBancaireID", SqlDbType.Int, _CompteBancaire.ID);

                db().AddInParameter(mCommande, "@DeductionFinancementBlank", SqlDbType.Money, _DeductionFinancementBlank);
                db().AddInParameter(mCommande, "@DeductionFinancementAPC", SqlDbType.Money, _DeductionFinancementAPC);
                db().AddInParameter(mCommande, "@AutresDeduction", SqlDbType.Money, _AutresDeduction);
                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _Date);
                db().AddInParameter(mCommande, "@MontantNet", SqlDbType.Money, _MontantNet);

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
                throw new Exception(ex.Message + "\r\n" + "FactureCommerciale:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("V2_FactureCommerciale_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 13);
                    db().AddInParameter(mCommande, "@EmbarquementID", SqlDbType.UniqueIdentifier, _Embarquement.ID);
                    db().AddInParameter(mCommande, "@EmbarquementNumero", SqlDbType.VarChar, _Embarquement.Numero);
                    db().AddInParameter(mCommande, "@NbreSacs", SqlDbType.Int, _NbreSacs);
                    db().AddInParameter(mCommande, "@PoidsBrutTheorique", SqlDbType.Decimal, _PoidsBrutTheorique);
                    db().AddInParameter(mCommande, "@PoidsNetTheorique", SqlDbType.Decimal, _PoidsNetTheorique);
                    db().AddInParameter(mCommande, "@PrixCAF", SqlDbType.Money, _PrixCAF);
                    db().AddInParameter(mCommande, "@PrixFob", SqlDbType.Money, _PrixFob);
                    db().AddInParameter(mCommande, "@MontantBrut", SqlDbType.Money, _MontantBrut);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_FactureCommerciale_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

                }

                db().AddInParameter(mCommande, "@CompteBancaireID", SqlDbType.Int, _CompteBancaire.ID);

                db().AddInParameter(mCommande, "@DeductionFinancementBlank", SqlDbType.Money, _DeductionFinancementBlank);
                db().AddInParameter(mCommande, "@DeductionFinancementAPC", SqlDbType.Money, _DeductionFinancementAPC);
                db().AddInParameter(mCommande, "@AutresDeduction", SqlDbType.Money, _AutresDeduction);
                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _Date);
                db().AddInParameter(mCommande, "@MontantNet", SqlDbType.Money, _MontantNet);

                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                db().AddOutParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0);
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
                throw new Exception(ex.Message + "\r\n" + "FactureCommerciale:fnUpdate");

            }
            return Result;
        }
        public bool fnApprove()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_FactureCommerciale_Approve");
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
                throw new Exception(ex.Message + "\r\n" + "V2_FactureCommerciale:fnApprove");
            }
            return bolResult;
        }

        public bool fnCancel()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_FactureCommerciale_Cancel");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@EmbarquementID", SqlDbType.UniqueIdentifier, _Embarquement.ID);
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
                throw new Exception(ex.Message + "\r\n" + "FactureCommerciale:fnCancel");
            }
            return bolResult;
        }

        #endregion


        #region "Private Members"

        public override string ToString()
        {
            return _Numero;
        }

        private static void MapFromDataReader(FactureCommerciale mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["EmbarquementID"]))
                    {
                        mClass.Embarquement = new Embarquement();
                        mClass.Embarquement.ID = (Guid)mDataReader["EmbarquementID"];
                        mClass.Embarquement.Numero = (string)mDataReader["EmbarquementNumero"];
                        mClass.Embarquement.Exportateur = new Exportateur();
                        mClass.Embarquement.Exportateur.ID = (int)mDataReader["ExportateurID"];
                        mClass.Embarquement.Exportateur.Nom = (string)mDataReader["ExportateurNom"];
                        mClass.Embarquement.Domiciliation = new Banque();
                        mClass.Embarquement.Domiciliation.ID = (int)mDataReader["DomiciliationID"];
                        mClass.Embarquement.Domiciliation.Nom = (string)mDataReader["DomiciliationNom"];
                        mClass.Embarquement.Quantite = ((decimal)mDataReader["EmbarquementQuantite"]/1000);

                        mClass.Embarquement.Contrat = new ContratDeVentes();
                        mClass.Embarquement.Contrat.ContratNumero = (string)mDataReader["ContratNumero"];

                    }

                    if (!DBNull.Value.Equals(mDataReader["CompteBancaireID"]))
                    {
                        mClass.CompteBancaire = new CompteBancaire();
                        mClass.CompteBancaire.ID = (int)mDataReader["CompteBancaireID"];
                        mClass.CompteBancaire.Numero = (string)mDataReader["CompteBancaireNumero"];

                    }

                    if (!DBNull.Value.Equals(mDataReader["DateFacture"])) mClass._Date = (DateTime)mDataReader["DateFacture"];

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];

                    if (!DBNull.Value.Equals(mDataReader["NbreSacs"])) mClass._NbreSacs = (int)mDataReader["NbreSacs"];

                    if (!DBNull.Value.Equals(mDataReader["PoidsBrutTheorique"])) mClass._PoidsBrutTheorique = (decimal)mDataReader["PoidsBrutTheorique"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetTheorique"])) mClass._PoidsNetTheorique = (decimal)mDataReader["PoidsNetTheorique"];
                    
                    if (!DBNull.Value.Equals(mDataReader["PrixCAF"])) mClass._PrixCAF = (decimal)mDataReader["PrixCAF"];
                    if (!DBNull.Value.Equals(mDataReader["PrixFob"])) mClass._PrixFob = (decimal)mDataReader["PrixFob"];

                    if (!DBNull.Value.Equals(mDataReader["MontantBrut"])) mClass._MontantBrut = (decimal)mDataReader["MontantBrut"];

                    if (!DBNull.Value.Equals(mDataReader["DeductionFinancementBlank"])) mClass._DeductionFinancementBlank = (decimal)mDataReader["DeductionFinancementBlank"];
                    if (!DBNull.Value.Equals(mDataReader["DeductionFinancementAPC"])) mClass._DeductionFinancementAPC = (decimal)mDataReader["DeductionFinancementAPC"];
                    if (!DBNull.Value.Equals(mDataReader["AutresDeduction"])) mClass._AutresDeduction = (decimal)mDataReader["AutresDeduction"];
                    if (!DBNull.Value.Equals(mDataReader["MontantNet"])) mClass._MontantNet = (decimal)mDataReader["MontantNet"];
                    
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
                throw new Exception(ex.Message + "\nFactureCommerciale:MapFromDataReader");
            }
        }
        #endregion
    }

    public partial class FactureCommercialeViewModel
    {
        public FactureCommerciale _FactureCommerciale { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
