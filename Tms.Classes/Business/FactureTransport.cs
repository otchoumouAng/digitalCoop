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
    public class FactureTransport : DataPersist
    {
        #region "Fields"

        private Guid _ID;
        private Campagne _Campagne;
        private Transporteur _Transporteur;
        private PayementMode _PayementMode;
        private string _Numero;
        private DateTime _DateFactureTransport;
        private decimal _Montant;
        private decimal _Deduction;
        private decimal _Tonnage;
        //private string _MontantAsString;
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

        public Campagne Campagne
        {
            get { return _Campagne; }
            set { _Campagne = value; }
        }

        public Transporteur Transporteur
        {
            get { return _Transporteur; }
            set { _Transporteur = value; }
        }

        public string TransporteurAsString
        {
            get { return _Transporteur != null ? _Transporteur.AsString : string.Empty; }            
        }

        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        

        public DateTime DateFactureTransport
        {
            get { return _DateFactureTransport; }
            set { _DateFactureTransport = value; }
        }

        public string DateFactureTransportAsString
        {
            get { return _DateFactureTransport != null ? _DateFactureTransport.ToString() : string.Empty; }            
        }

        public decimal Montant
        {
            get { return _Montant; }
            set { _Montant = value; }
        }

        public decimal Tonnage
        {
            get { return _Tonnage; }
            set { _Tonnage = value; }
        }

        public string TonnageAsString
        {
            get { return _Tonnage != 0 ? String.Format("{0:#,#}", _Tonnage).TrimStart() : string.Empty ; }            
        }

        public string MontantAsString 
        {
            get { return _Montant != 0 ? String.Format("{0:#,#}", _Montant).TrimStart() : string.Empty; }            
        }

        public string DeductionAsString
        {
            get { return _Deduction != 0 ? String.Format("{0:#,#}", _Deduction).TrimStart() : string.Empty; }            
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
                else
                    return 2; //                     
            }
        }

        public PayementMode PayementMode
        {
            get
            {
                return _PayementMode;
            }

            set
            {
                _PayementMode = value;
            }
        }

        public decimal Deduction
        {
            get
            {
                return _Deduction;
            }

            set
            {
                _Deduction = value;
            }
        }
        #endregion

        #region "Constructor"

        public FactureTransport()
        {

        }

        public FactureTransport(Guid myId)
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
                mDataReader = db().ExecuteReader("FactureTransport_Get", (Guid)Id);
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
            return fnSelect("", -1, null, null);
        }

        public List<DataPersist> fnSelect(string cropyear, int TransporteurID, DateTime? startdate, DateTime? enddate)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("FactureTransport_Select");
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.VarChar, cropyear);
                db().AddInParameter(mCommande, "@transporteurID", SqlDbType.Int, TransporteurID);
                db().AddInParameter(mCommande, "@startdate", SqlDbType.DateTime, startdate);
                db().AddInParameter(mCommande, "@enddate", SqlDbType.DateTime, enddate);                                
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FactureTransport mClass = new FactureTransport();

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

        public bool fnSelectByNumber(string numero)
        {            
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("");
                mDataReader = db().ExecuteReader("FactureTransport_SelectByNumber", (string)numero);                                              

                if (mDataReader.Read())
                {                   
                    MapFromDataReader(this, mDataReader);                    
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectByNumber");
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

                    mCommande = db().CreateStoredProcCommand("FactureTransport_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 9);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("FactureTransport_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@campagneID", SqlDbType.VarChar, _Campagne.Designation);
                db().AddInParameter(mCommande, "@transporteurID", SqlDbType.Int, _Transporteur.ID);
                db().AddInParameter(mCommande, "@modeID", SqlDbType.Int, _PayementMode.ID);
                db().AddInParameter(mCommande, "@dateFactureTransport", SqlDbType.DateTime, _DateFactureTransport);
                db().AddInParameter(mCommande, "@montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@deduction", SqlDbType.Money, _Deduction);
                db().AddInParameter(mCommande, "@tonnage", SqlDbType.Decimal, _Tonnage);
                db().AddInParameter(mCommande, "@commentaire", SqlDbType.VarChar, _Commentaire);
                db().AddInParameter(mCommande, "@statut", SqlDbType.Char, _Statut);

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
                throw new Exception(ex.Message + "\r\n" + "FactureTransport:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("FactureTransport_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 9);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("FactureTransport_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@campagneID", SqlDbType.VarChar, _Campagne.Designation);
                db().AddInParameter(mCommande, "@transporteurID", SqlDbType.Int, _Transporteur.ID);
                db().AddInParameter(mCommande, "@modeID", SqlDbType.Int, _PayementMode.ID);
                db().AddInParameter(mCommande, "@dateFactureTransport", SqlDbType.DateTime, _DateFactureTransport);
                db().AddInParameter(mCommande, "@montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@deduction", SqlDbType.Money, _Deduction);
                db().AddInParameter(mCommande, "@tonnage", SqlDbType.Decimal, _Tonnage);
                db().AddInParameter(mCommande, "@commentaire", SqlDbType.VarChar, _Commentaire);
                db().AddInParameter(mCommande, "@statut", SqlDbType.Char, _Statut);                                           

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
                throw new Exception(ex.Message + "\r\n" + "FactureTransport:fnUpdate");

            }
            return Result;
        }

        public override bool fnActivate()
        {

            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("FactureTransport_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "FactureTransport:fnActivate");
            }
            return Result;

        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("FactureTransport_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "FactureTransport:fnDeActivate");
            }
            return bolResult;
        }

        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _Numero;
        }

        private static void MapFromDataReader(FactureTransport mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["Campagne"])) mClass._Campagne.Designation = (string)mDataReader["Campagne"];

                    mClass._Transporteur = new Transporteur();
                    if (!DBNull.Value.Equals(mDataReader["TransporteurID"])) mClass._Transporteur.ID = (int)mDataReader["TransporteurID"];
                    if (!DBNull.Value.Equals(mDataReader["transporteurNom"])) mClass._Transporteur.Nom = (string)mDataReader["transporteurNom"];

                    mClass._PayementMode = new PayementMode();
                    if (!DBNull.Value.Equals(mDataReader["ModePayementID"])) mClass._PayementMode.ID = (int)mDataReader["ModePayementID"];
                    if (!DBNull.Value.Equals(mDataReader["ModePayementDesignation"])) mClass._PayementMode.Designation = (string)mDataReader["ModePayementDesignation"];


                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DateFactureTransport"])) mClass._DateFactureTransport = (DateTime)mDataReader["DateFactureTransport"];
                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._Montant = (decimal)mDataReader["Montant"];
                    if (!DBNull.Value.Equals(mDataReader["Deduction"])) mClass._Deduction = (decimal)mDataReader["Deduction"];
                    if (!DBNull.Value.Equals(mDataReader["Tonnage"])) mClass._Tonnage = (decimal)mDataReader["Tonnage"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];                   
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nFactureTransport:MapFromDataReader");
            }
        }
        #endregion

    }

    public partial class FactureTransportViewModel
    {
        public FactureTransport _FactureTransport { get; set; }
        public Parametres _Parametres { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
