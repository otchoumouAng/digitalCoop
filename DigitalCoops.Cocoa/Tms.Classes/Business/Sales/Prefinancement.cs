using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Components.Data;

namespace Tms.Classes.Business.Sales
{
    public class Prefinancement : DataPersist
    {
        #region "Fields"

        private Guid _ID;
        private string _Campagne;
        private Exportateur _Exportateur;
        private PrefinancementType _PrefinancementType;
        private string _Numero;
        private DateTime _Date;
        private string _ReferenceExterne;
        private decimal _Tonnage;
        private decimal _TonnagePaye;
        private decimal _Prix;
        private decimal _Montant;
        private decimal _MontantEuro;
        private string _Commentaire;


        private string _Statut;
        private bool _Desactive;
        protected string _UtilisateurApprobation;
        protected string _UtilisateurRejet;
        protected DateTime? _DateApprobation;
        protected DateTime? _DateRejet;

        //Used for invoicing
        private decimal _Solde;
        private decimal _MontantPreleve;
       

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

        public PrefinancementType PrefinancementType
        {
            get { return _PrefinancementType; }
            set { _PrefinancementType = value; }
        }

        public string LibellePrefinancementType
        {
            get { return _PrefinancementType != null ? _PrefinancementType.Designation : string.Empty; }

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

        public string ReferenceExterne
        {
            get { return _ReferenceExterne; }
            set { _ReferenceExterne = value; }
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

        public decimal TonnagePaye
        {
            get { return _TonnagePaye; }
            set { _TonnagePaye = value; }
        }

        public string TonnagePayeAsString
        {
            get { return _TonnagePaye != 0 ? String.Format("{0:#,#}", _TonnagePaye).TrimStart() : string.Empty; }
        }



        public decimal Prix
        {
            get { return _Prix; }
            set { _Prix = value; }
        }

        public string PrixAsString
        {
            get { return _Prix != 0 ? String.Format("{0:#,##0.##}", _Prix).TrimStart() : string.Empty; }
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

        public decimal MontantEuro
        {
            get { return _MontantEuro; }
            set { _MontantEuro = value; }
        }

        public string MontantEuroAsString
        {
            get { return _MontantEuro != 0 ? String.Format("{0:#,##0.###}", _MontantEuro).TrimStart() : string.Empty; }
        }

        public decimal Solde
        {
            get { return _Solde; }
            set { _Solde = value; }
        }

        public decimal SoldeCriteria
        {
            get { return decimal.Round(_Solde); }
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

       

        public bool IsRejected
        {
            get { return _Statut == "RE"; }

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
        #endregion

        #region Constructor
        public Prefinancement()
        {

        }

        public Prefinancement(Guid myID)
        {
            this.fnGet(myID);
        }
        #endregion

        #region ""
        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_Prefinancement_Get", (Guid)Id);
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
        public  bool fnGetForInvoice(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_Prefinancement_GetForInvoice", (Guid)Id);
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
                mDataReader = db().ExecuteReader("V2_Prefinancement_GetByNumber", (string)Number);
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

        public List<DataPersist> fnSelect(string Campagne, int ExportateurID, int TypeID, DateTime? StartDate, DateTime? EndDate, string statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Prefinancement_Select");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@TypeID", SqlDbType.Int, TypeID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@status", SqlDbType.VarChar, 2, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Prefinancement mClass = new Prefinancement();
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
        public List<DataPersist> fnSelectForInvoice(string Campagne, int ExportateurID, int TypeID, DateTime? StartDate, DateTime? EndDate, string statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Prefinancement_SelectForInvoice");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@TypeID", SqlDbType.Int, TypeID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@status", SqlDbType.VarChar, 2, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Prefinancement mClass = new Prefinancement();
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

        public List<DataPersist> fnSelectForApproval(string Campagne, int ExportateurID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Prefinancement_SelectForApproval");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Prefinancement mClass = new Prefinancement();
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

                    mCommande = db().CreateStoredProcCommand("V2_Prefinancement_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 8);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_Prefinancement_Modify");
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
                db().AddInParameter(mCommande, "@TypeID", SqlDbType.Int, _PrefinancementType.ID);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, _Exportateur.ID);
                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _Date);
                db().AddInParameter(mCommande, "@ReferenceExterne", SqlDbType.VarChar, _ReferenceExterne);
                
                db().AddInParameter(mCommande, "@Prix", SqlDbType.Money, _Prix);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@MontantEuro", SqlDbType.Money, _MontantEuro);
                db().AddInParameter(mCommande, "@Tonnage", SqlDbType.Decimal, _Tonnage);
                db().AddInParameter(mCommande, "@TonnagePaye", SqlDbType.Decimal, _TonnagePaye);


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
                throw new Exception(ex.Message + "\r\n" + "V2_Prefinancement:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("V2_Prefinancement_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 8);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_Prefinancement_Modify");
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
                db().AddInParameter(mCommande, "@TypeID", SqlDbType.Int, _PrefinancementType.ID);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, _Exportateur.ID);
                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _Date);
                db().AddInParameter(mCommande, "@ReferenceExterne", SqlDbType.VarChar, _ReferenceExterne);

                db().AddInParameter(mCommande, "@Prix", SqlDbType.Money, _Prix);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@MontantEuro", SqlDbType.Money, _MontantEuro);
                db().AddInParameter(mCommande, "@Tonnage", SqlDbType.Decimal, _Tonnage);
                db().AddInParameter(mCommande, "@TonnagePaye", SqlDbType.Decimal, _TonnagePaye);


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
                throw new Exception(ex.Message + "\r\n" + "V2_Prefinancement:fnUpdate");

            }
            return Result;
        }

        public bool fnUpdateAndApprove(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("V2_Prefinancement_ModifyAndApprove");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, _Campagne);
                db().AddInParameter(mCommande, "@TypeID", SqlDbType.Int, _PrefinancementType.ID);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, _Exportateur.ID);
                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _Date);
                db().AddInParameter(mCommande, "@ReferenceExterne", SqlDbType.VarChar, _ReferenceExterne);

                db().AddInParameter(mCommande, "@Prix", SqlDbType.Money, _Prix);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@Tonnage", SqlDbType.Decimal, _Tonnage);
                db().AddInParameter(mCommande, "@TonnagePaye", SqlDbType.Decimal, _TonnagePaye);

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
                throw new Exception(ex.Message + "\r\n" + "V2_Prefinancement:fnUpdate");

            }
            return Result;
        }
        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Prefinancement_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "V2_Prefinancement:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Prefinancement_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "V2_Prefinancement:fnDeActivate");
            }
            return bolResult;
        }

        public bool fnCancel()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Prefinancement_Cancel");
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
                throw new Exception(ex.Message + "\r\n" + "V2_Prefinancement:fnCancel");
            }
            return bolResult;
        }
        public bool fnRemovePaymentAll()
        {
            if (!this._isnew)
            {
                bool bolResult;
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Prefinancement_RemovePaymentAll");
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
                    throw new Exception(ex.Message + "\r\n" + "Prefinancement:fnRemovePaymentAll");
                }
                return bolResult;
            }
            return false;
        }
        public bool fnCancel(DataTransaction mTran)
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Prefinancement_Cancel");
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
                throw new Exception(ex.Message + "\r\n" + "V2_Prefinancement:fnCancel");
            }
            return bolResult;
        }

        public bool fnApprove()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Prefinancement_Approve");
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
                throw new Exception(ex.Message + "\r\n" + "V2_Prefinancement:fnApprove");
            }
            return bolResult;
        }

        public bool fnApprove(DataTransaction mTran)
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Prefinancement_Approve");
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
                throw new Exception(ex.Message + "\r\n" + "V2_Prefinancement:fnApprove");
            }
            return bolResult;
        }

        public bool fnReject()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Prefinancement_Reject");
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
                throw new Exception(ex.Message + "\r\n" + "V2_Prefinancement:fnReject");
            }
            return bolResult;
        }

        public bool fnReject(DataTransaction dt)
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Prefinancement_Reject");
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
                throw new Exception(ex.Message + "\r\n" + "V2_Prefinancement:fnReject");
            }
            return bolResult;
        }

        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _Numero;
        }

        private static void MapFromDataReader(Prefinancement mClass, IDataReader mDataReader)
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

                    if (!DBNull.Value.Equals(mDataReader["TypeID"]))
                    {
                        mClass._PrefinancementType = new PrefinancementType();
                        mClass._PrefinancementType.ID = (int)mDataReader["TypeID"];
                        mClass._PrefinancementType.Designation = (string)mDataReader["TypeDesignation"];
                    }

                    

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DatePF"])) mClass._Date = (DateTime)mDataReader["DatePF"];
                    if (!DBNull.Value.Equals(mDataReader["Tonnage"])) mClass._Tonnage = (decimal)mDataReader["Tonnage"];
                    if (!DBNull.Value.Equals(mDataReader["TonnagePaye"])) mClass._TonnagePaye = (decimal)mDataReader["TonnagePaye"];
                    if (!DBNull.Value.Equals(mDataReader["Prix"])) mClass._Prix = (decimal)mDataReader["Prix"];
                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._Montant = (decimal)mDataReader["Montant"];
                    if (!DBNull.Value.Equals(mDataReader["MontantEuro"])) mClass._MontantEuro = (decimal)mDataReader["MontantEuro"];
                    if (!DBNull.Value.Equals(mDataReader["Solde"])) mClass._Solde = (decimal)mDataReader["Solde"];
                    if (!DBNull.Value.Equals(mDataReader["ReferenceExterne"])) mClass._ReferenceExterne = (string)mDataReader["ReferenceExterne"];

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
                    if (!DBNull.Value.Equals(mDataReader["RejetUtilisateur"])) mClass._UtilisateurRejet = (string)mDataReader["RejetUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RejetDate"])) mClass._DateRejet = (DateTime)mDataReader["RejetDate"];

                   
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nV2_Prefinancement:MapFromDataReader");
            }
        }


        private static void MapFromDataReaderLite(Prefinancement mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    
                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                   
                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._Montant = (decimal)mDataReader["Montant"];
                   
                    

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nV2_Prefinancement:MapFromDataReaderLite");
            }
        }
        #endregion
    }

    public partial class PrefinancementViewModel
    {
        public Prefinancement _Prefinancement { get; set; }
        public string _ReportType { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }

    public partial class PrefinancementReportType
    {
        public const string Balance = "Balance";
        public const string Execution = "Execution";
    }
}
