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
    public class Enregistrement : DataPersist
    {
        #region "Fields"
        private Guid _ID;
        private string _Campagne;
        private Exportateur _Exportateur;
        private EnregistrementMode _EnregistrementMode;
        private string _Numero;
        private DateTime _Date;
        private string _NumeroONCC;
        private string _ContratExport;
        private DateTime _Periode;
        private decimal _Tonnage;
        private decimal _Prix;
        private decimal _Montant;
        private string _Terme;
        private string _Description;
        private bool _Desactive;
        private string _Statut;
        private string _NumeroGuichet;
        //Used for 
        private decimal _TonnageRestant;
        private string _NumeroCode;
        private Guid _EnregistrementDetailID;
        #endregion

        #region "Properties"
        [ModelField(IDProperty = true)]
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

        public string NumeroONCC
        {
            get { return _NumeroONCC; }
            set { _NumeroONCC = value; }
        }

        public string ContratExport
        {
            get { return _ContratExport; }
            set { _ContratExport = value; }
        }

        public DateTime Periode
        {
            get { return _Periode; }
            set { _Periode = value; }
        }

        public decimal Tonnage
        {
            get { return _Tonnage; }
            set { _Tonnage = value; }
        }

        public string TonnageAsString
        {
            get { return _Tonnage != 0 ? String.Format("{0:#,##0.##}", _Tonnage).TrimStart() : string.Empty; }
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

        public decimal TonnageRestant
        {
            get { return _TonnageRestant; }
            set { _TonnageRestant = value; }
        }

        public string TonnageRestantAsString
        {
            get { return _TonnageRestant != 0 ? String.Format("{0:#,##0.##}", _TonnageRestant).TrimStart() : string.Empty; }
        }

        public decimal TonnageRestantCr
        {
            get { return _TonnageRestant/100; }
        }

        public string TonnageRestantCrAsString
        {
            get { return _TonnageRestant != 0 ? String.Format("{0:#,##0.##}", _TonnageRestant / 100).TrimStart() : string.Empty; }
        }

        //public string NumeroCode
        //{
        //    get { return _NumeroGuichet + " (N°" + _NumeroONCC + " - "+ _Numero +")"; }
        //    set { _NumeroCode = value; }
        //}

        public string NumeroCode
        {
            get { return _NumeroGuichet + " (N°" + _NumeroONCC + ")"; }
            set { _NumeroCode = value; }
        }

        public string Terme
        {
            get { return _Terme; }
            set { _Terme = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
        }
        public string Statut
        {
            get { return _Statut; }
            set { _Statut = value; }
        }

        public bool IsStatut
        {
            get { return _Statut == "NL"; }
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
                if (_Desactive)
                    return 0; // 
                else
                    return 2; //                     
            }
        }

        public string NumeroGuichet
        {
            get
            {
                return _NumeroGuichet;
            }

            set
            {
                _NumeroGuichet = value;
            }
        }

        public EnregistrementMode EnregistrementMode
        {
            get
            {
                return _EnregistrementMode;
            }

            set
            {
                _EnregistrementMode = value;
            }
        }

        public Guid EnregistrementDetailID
        {
            get
            {
                return _EnregistrementDetailID;
            }

            set
            {
                _EnregistrementDetailID = value;
            }
        }

        #endregion

        #region Constructor
        public Enregistrement()
        {

        }

        public Enregistrement(Guid myID)
        {
            this.fnGet(myID);
        }
        #endregion

        #region "Methods"
        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Enregistrement_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "V2_Enregistrement:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Enregistrement_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "V2_Enregistrement:fnDeActivate");
            }
            return Result;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_Enregistrement_Get", (Guid)Id);
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
                mDataReader = db().ExecuteReader("V2_Enregistrement_GetByNumber", (string)Number);
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
            return fnSelect("{Tous}", -1, null, null, -1, "NA");
        }
        public List<DataPersist> fnSelect(string Campagne, int ExportateurID, DateTime? StartDate, DateTime? EndDate, int IsDisabled, string mStatut = "NA")
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Enregistrement_Select");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@IsDisabled", SqlDbType.Int, IsDisabled);
                db().AddInParameter(mCommande, "@statut", SqlDbType.Char, mStatut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Enregistrement mClass = new Enregistrement();
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


        public List<DataPersist> fnSelectForShipment(string Campagne, int ExportateurID, DateTime? StartDate, DateTime? EndDate, int IsDisabled)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Enregistrement_SelectForShipment");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@IsDisabled", SqlDbType.Int, IsDisabled);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Enregistrement mClass = new Enregistrement();
                    MapFromDataReaderShipment(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectForShipment");
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

                    mCommande = db().CreateStoredProcCommand("V2_Enregistrement_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 8);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_Enregistrement_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, _Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, _Exportateur.ID);

                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _Date);
                db().AddInParameter(mCommande, "@Periode", SqlDbType.DateTime, _Periode);

                db().AddInParameter(mCommande, "@NumeroONCC", SqlDbType.VarChar, _NumeroONCC);
                db().AddInParameter(mCommande, "@ContratExport", SqlDbType.VarChar, _ContratExport);

                db().AddInParameter(mCommande, "@Tonnage", SqlDbType.Decimal,( _Tonnage*100));
                db().AddInParameter(mCommande, "@Prix", SqlDbType.Money, _Prix);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
               

                db().AddInParameter(mCommande, "@Terme", SqlDbType.VarChar, _Terme);
                db().AddInParameter(mCommande, "@Description", SqlDbType.VarChar, _Description);



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
                throw new Exception(ex.Message + "\r\n" + "V2_Enregistrement:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("V2_Enregistrement_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 8);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_Enregistrement_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, _Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, _Exportateur.ID);
                db().AddInParameter(mCommande, "@ModeEnregistrement", SqlDbType.Int, _EnregistrementMode.ID);

                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _Date);
                db().AddInParameter(mCommande, "@Periode", SqlDbType.DateTime, _Periode);

                db().AddInParameter(mCommande, "@NumeroONCC", SqlDbType.VarChar, _NumeroONCC);
                db().AddInParameter(mCommande, "@NumeroGuichet", SqlDbType.VarChar, _NumeroGuichet);
                db().AddInParameter(mCommande, "@ContratExport", SqlDbType.VarChar, _ContratExport);

                db().AddInParameter(mCommande, "@Tonnage", SqlDbType.Decimal, (_Tonnage * 100));
                db().AddInParameter(mCommande, "@Prix", SqlDbType.Money, _Prix);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);

                db().AddInParameter(mCommande, "@Terme", SqlDbType.VarChar, _Terme);
                db().AddInParameter(mCommande, "@Description", SqlDbType.VarChar, _Description);



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
                throw new Exception(ex.Message + "\r\n" + "V2_Enregistrement:fnUpdate");

            }
            return Result;
        }

        #endregion

        #region "Private Methods"
        public override string ToString()
        {
            throw new NotImplementedException();
        }

        private static void MapFromDataReader(Enregistrement mClass, IDataReader mDataReader)
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

                    if (!DBNull.Value.Equals(mDataReader["ModeEnregistrementID"]))
                    {
                        mClass._EnregistrementMode = new EnregistrementMode();
                        mClass._EnregistrementMode.ID = (int)mDataReader["ModeEnregistrementID"];
                        mClass._EnregistrementMode.Designation = (string)mDataReader["ModeEnregistrementLibelle"];
                    }                    

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DateEn"])) mClass._Date = (DateTime)mDataReader["DateEn"];
                    if (!DBNull.Value.Equals(mDataReader["Periode"])) mClass._Periode = (DateTime)mDataReader["Periode"];

                    if (!DBNull.Value.Equals(mDataReader["NumeroONCC"])) mClass._NumeroONCC = (string)mDataReader["NumeroONCC"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroGuichet"])) mClass._NumeroGuichet = (string)mDataReader["NumeroGuichet"];
                    if (!DBNull.Value.Equals(mDataReader["ContratExport"])) mClass._ContratExport = (string)mDataReader["ContratExport"];

                    if (!DBNull.Value.Equals(mDataReader["Tonnage"])) mClass._Tonnage = ((decimal)mDataReader["Tonnage"]/100);
                    if (!DBNull.Value.Equals(mDataReader["Prix"])) mClass._Prix = (decimal)mDataReader["Prix"];
                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._Montant = (decimal)mDataReader["Montant"];
                    if (!DBNull.Value.Equals(mDataReader["TonnageRestant"])) mClass._TonnageRestant = (decimal)mDataReader["TonnageRestant"];

                    if (!DBNull.Value.Equals(mDataReader["Terme"])) mClass._Terme = (string)mDataReader["Terme"];
                    if (!DBNull.Value.Equals(mDataReader["Description"])) mClass._Description = (string)mDataReader["Description"];

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];
                   

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nV2_Enregistrement:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderShipment(Enregistrement mClass, IDataReader mDataReader)
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

                    if (!DBNull.Value.Equals(mDataReader["ModeEnregistrementID"]))
                    {
                        mClass._EnregistrementMode = new EnregistrementMode();
                        mClass._EnregistrementMode.ID = (int)mDataReader["ModeEnregistrementID"];
                        mClass._EnregistrementMode.Designation = (string)mDataReader["ModeEnregistrementLibelle"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["IdEnregistrementDetail"]))
                    {
                        mClass._EnregistrementDetailID = (Guid)mDataReader["IdEnregistrementDetail"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DateEn"])) mClass._Date = (DateTime)mDataReader["DateEn"];
                    if (!DBNull.Value.Equals(mDataReader["Periode"])) mClass._Periode = (DateTime)mDataReader["Periode"];

                    if (!DBNull.Value.Equals(mDataReader["NumeroONCC"])) mClass._NumeroONCC = (string)mDataReader["NumeroONCC"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroGuichet"])) mClass._NumeroGuichet = (string)mDataReader["NumeroGuichet"];
                    if (!DBNull.Value.Equals(mDataReader["ContratExport"])) mClass._ContratExport = (string)mDataReader["ContratExport"];

                    if (!DBNull.Value.Equals(mDataReader["Tonnage"])) mClass._Tonnage = ((decimal)mDataReader["Tonnage"] / 100);
                    if (!DBNull.Value.Equals(mDataReader["Prix"])) mClass._Prix = (decimal)mDataReader["Prix"];
                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._Montant = (decimal)mDataReader["Montant"];
                    if (!DBNull.Value.Equals(mDataReader["TonnageRestant"])) mClass._TonnageRestant = (decimal)mDataReader["TonnageRestant"];

                    if (!DBNull.Value.Equals(mDataReader["Terme"])) mClass._Terme = (string)mDataReader["Terme"];
                    if (!DBNull.Value.Equals(mDataReader["Description"])) mClass._Description = (string)mDataReader["Description"];

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nV2_Enregistrement:MapFromDataReader");
            }
        }

        #endregion
    }
    public partial class EnregistrementViewModel
    {
        public Enregistrement _Enregistrement { get; set; }
        public int _ModeEnregistrement { get; set; }
        public int _DefaultExportateur { get; set; }
        public string _DefaultCampagne { get; set; }
        public string _PrefixeContratExport { get; set; }        
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
