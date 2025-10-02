using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Components.Data;

namespace Tms.Classes.Business.Sales
{
    public class FactureFinale : DataPersist
    {
        #region "Fields"
        private Guid _ID;
        private Embarquement _Embarquement;
        private string _Numero;
        private DateTime _Date;
        private decimal _PoidsNetStandard;
        private decimal _PoidsRecu;
        private decimal _PoidsEchantillon;
        private decimal _PoidsSurplus;
        private decimal _TotalSurplus;
        private decimal _TauxBonus;
        private decimal _MontantBonus;
        private decimal _MontantBrut;
        private decimal _MontantFactureCommerciale;
        private decimal _MontantNet;

        private string _Statut;
        private bool _Desactive;
        protected string _UtilisateurApprobation;
        protected DateTime? _DateApprobation;
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

        public decimal PoidsNetStandard
        {
            get { return _PoidsNetStandard; }
            set { _PoidsNetStandard = value; }
        }

        public string PoidsNetStandardAsString
        {
            get { return _PoidsNetStandard != 0 ? String.Format("{0:#,#}", _PoidsNetStandard).TrimStart() : string.Empty; }
        }

        public decimal PoidsRecu
        {
            get { return _PoidsRecu; }
            set { _PoidsRecu = value; }
        }

        public string PoidsRecuAsString
        {
            get { return _PoidsRecu != 0 ? String.Format("{0:#,#}", _PoidsRecu).TrimStart() : string.Empty; }
        }

        public decimal PoidsEchantillon
        {
            get { return _PoidsEchantillon; }
            set { _PoidsEchantillon = value; }
        }

        public string PoidsEchantillonAsString
        {
            get { return _PoidsEchantillon != 0 ? String.Format("{0:#,#}", _PoidsEchantillon).TrimStart() : string.Empty; }
        }

        public decimal PoidsSurplus
        {
            get { return (_PoidsRecu - _PoidsNetStandard); }
            set { _PoidsSurplus = value; }
        }

        public string PoidsSurplusAsString
        {
            get { return _PoidsSurplus != 0 ? String.Format("{0:#,#}", _PoidsSurplus).TrimStart() : string.Empty; }
        }
       
        public decimal TotalSurplus
        {
            get { return _TotalSurplus; }
            set { _TotalSurplus = value; }
        }
        public string TotalSurplusAsString
        {
            get { return _TotalSurplus != 0 ? String.Format("{0:#,#}", _TotalSurplus).TrimStart() : string.Empty; }
        }
        public decimal TauxBonus
        {
            get { return _TauxBonus; }
            set { _TauxBonus = value; }
        }

        public string TauxBonusAsString
        {
            get { return _TauxBonus != 0 ? String.Format("{0:0.00}", _TauxBonus).TrimStart().Replace(".",",") : string.Empty; }
        }

        public decimal MontantBonus
        {
            get { return _MontantBonus; }
            set { _MontantBonus = value; }
        }

        public string MontantBonusAsString
        {
            get { return _MontantBonus != 0 ? String.Format("{0:#,##0.###}", _MontantBonus).TrimStart() : string.Empty; }
        }
        public decimal MontantBrut
        {
            get { return _MontantBrut; }
            set { _MontantBrut = value; }
        }

        public string MontantBrutAsString
        {
            get { return _MontantBrut != 0 ? String.Format("{0:#,##0.###}", _MontantBrut).TrimStart() : string.Empty; }
        }

        public decimal MontantFactureCommerciale
        {
            get { return _MontantFactureCommerciale; }
            set { _MontantFactureCommerciale = value; }
        }

        public string MontantFactureCommercialeAsString
        {
            get { return _MontantFactureCommerciale != 0 ? String.Format("{0:#,##0.###}", _MontantFactureCommerciale).TrimStart() : string.Empty; }
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

        
        public DateTime? DateApprobation
        {
            get { return _DateApprobation; }
            set { _DateApprobation = value; }
        }

        
        #endregion

        #region Constructor
        public FactureFinale()
        {

        }

        public FactureFinale(Guid MyId)
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
                mDataReader = db().ExecuteReader("V2_FactureFinale_Get", (Guid)Id);
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
                mDataReader = db().ExecuteReader("V2_FactureFinale_GetByNumber", (string)Number);
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
            return fnSelect("", -1, null, null, "%%");
        }

        public List<DataPersist> fnSelect(string Campagne, int ExportateurID, DateTime? StartDate, DateTime? EndDate, string statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_FactureFinale_Select");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@Status", SqlDbType.VarChar, 2, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FactureFinale mClass = new FactureFinale();
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
            return false;
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {

                    mCommande = db().CreateStoredProcCommand("V2_FactureFinale_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 13);
                    db().AddInParameter(mCommande, "@EmbarquementID", SqlDbType.UniqueIdentifier, _Embarquement.ID);
                    db().AddInParameter(mCommande, "@EmbarquementNumero", SqlDbType.Char, _Embarquement.Numero);

                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_FactureFinale_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

                }

                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _Date);

                db().AddInParameter(mCommande, "@PoidsNetStandard", SqlDbType.Decimal, _PoidsNetStandard);
                db().AddInParameter(mCommande, "@PoidsRecu", SqlDbType.Decimal, _PoidsRecu);
                db().AddInParameter(mCommande, "@PoidsEchantillon", SqlDbType.Decimal, _PoidsEchantillon);
                db().AddInParameter(mCommande, "@TotalSurplus", SqlDbType.Decimal, _TotalSurplus);
                db().AddInParameter(mCommande, "@TauxBonus", SqlDbType.Decimal, _TauxBonus);
                db().AddInParameter(mCommande, "@MontantBonus", SqlDbType.Money, _MontantBonus);
                db().AddInParameter(mCommande, "@MontantBrut", SqlDbType.Money, _MontantBrut);
                db().AddInParameter(mCommande, "@MontantFactureCommerciale", SqlDbType.Money, _MontantFactureCommerciale);
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
                throw new Exception(ex.Message + "\r\n" + "FactureFinale:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("V2_FactureFinale_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 13);
                    db().AddInParameter(mCommande, "@EmbarquementID", SqlDbType.UniqueIdentifier, _Embarquement.ID);
                    db().AddInParameter(mCommande, "@EmbarquementNumero", SqlDbType.Char, _Embarquement.Numero);

                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_FactureFinale_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

                }

                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _Date);

                db().AddInParameter(mCommande, "@PoidsNetStandard", SqlDbType.Decimal, _PoidsNetStandard);
                db().AddInParameter(mCommande, "@PoidsRecu", SqlDbType.Decimal, _PoidsRecu);
                db().AddInParameter(mCommande, "@PoidsEchantillon", SqlDbType.Decimal, _PoidsEchantillon);
                db().AddInParameter(mCommande, "@TotalSurplus", SqlDbType.Decimal, _TotalSurplus);
                db().AddInParameter(mCommande, "@TauxBonus", SqlDbType.Decimal, _TauxBonus);
                db().AddInParameter(mCommande, "@MontantBonus", SqlDbType.Money, _MontantBonus);
                db().AddInParameter(mCommande, "@MontantBrut", SqlDbType.Money, _MontantBrut);
                db().AddInParameter(mCommande, "@MontantFactureCommerciale", SqlDbType.Money, _MontantFactureCommerciale);
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
                throw new Exception(ex.Message + "\r\n" + "FactureFinale:fnUpdate");

            }
            return Result;
        }

        public bool fnApprove()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_FactureFinale_Approve");
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
                throw new Exception(ex.Message + "\r\n" + "V2_FactureFinale:fnApprove");
            }
            return bolResult;
        }

        public bool fnCancel()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_FactureFinale_Cancel");
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
                throw new Exception(ex.Message + "\r\n" + "FactureFinale:fnCancel");
            }
            return bolResult;
        }
        #endregion
        #region "Private Members"

        public override string ToString()
        {
            return _Numero;
        }

        private static void MapFromDataReader(FactureFinale mClass, IDataReader mDataReader)
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
                        mClass.Embarquement.NbreSacs = (int)mDataReader["EmbarquementNbreSacs"];
                        mClass.Embarquement.Quantite = ((decimal)mDataReader["EmbarquementQuantite"]/1000);
                        mClass.Embarquement.Contrat = new ContratDeVentes();
                        mClass.Embarquement.Contrat.Prix = (decimal)mDataReader["ContratPrix"];
                        mClass.Embarquement.Contrat.PrixFob = (decimal)mDataReader["ContratPrixFob"];
                        mClass.Embarquement.Contrat.ContratNumero = (string)mDataReader["ContratNumero"];

                    }

                    if (!DBNull.Value.Equals(mDataReader["DateFacture"])) mClass._Date = (DateTime)mDataReader["DateFacture"];

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];


                    if (!DBNull.Value.Equals(mDataReader["PoidsNetStandard"])) mClass._PoidsNetStandard = (decimal)mDataReader["PoidsNetStandard"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsRecu"])) mClass._PoidsRecu = (decimal)mDataReader["PoidsRecu"];

                    if (!DBNull.Value.Equals(mDataReader["PoidsEchantillon"])) mClass._PoidsEchantillon = (decimal)mDataReader["PoidsEchantillon"];
                    if (!DBNull.Value.Equals(mDataReader["TotalSurplus"])) mClass._TotalSurplus = (decimal)mDataReader["TotalSurplus"];

                    if (!DBNull.Value.Equals(mDataReader["TauxBonus"])) mClass._TauxBonus = (decimal)mDataReader["TauxBonus"];
                    if (!DBNull.Value.Equals(mDataReader["MontantBonus"])) mClass._MontantBonus = (decimal)mDataReader["MontantBonus"];

                    if (!DBNull.Value.Equals(mDataReader["MontantBrut"])) mClass._MontantBrut = (decimal)mDataReader["MontantBrut"];
                    if (!DBNull.Value.Equals(mDataReader["MontantFactureCommerciale"])) mClass._MontantFactureCommerciale = (decimal)mDataReader["MontantFactureCommerciale"];

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
                throw new Exception(ex.Message + "\nFactureFinaleMapFromDataReader");
            }
        }
        #endregion
    }

    public partial class FactureFinaleViewModel
    {
        public FactureFinale _FactureFinale { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
