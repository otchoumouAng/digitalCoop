using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared.Sales;
using Tms.Components.Data;

namespace Tms.Classes.Business.Sales
{
    public class CertificatPoids : DataPersist
    {
        #region "Fields"
        private Guid _ID;
        //private string _Numero;
        private Embarquement _Embarquement;
        private OrganismeCertification _OrganismeCertification;
        private DateTime _Date;
        private DateTime _DateArrivee;
        private DateTime? _DateDepotage;
        private decimal _PoidsArrivee;
        private decimal _PoidsEchantillonArrivee;
        private string _Commentaire;
        private bool _Desactive;
        private string _Statut;
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

        //public string Numero
        //{
        //    get { return _Numero; }
        //    set { _Numero = value; }
        //}

        public OrganismeCertification OrganismeCertification
        {
            get { return _OrganismeCertification; }
            set { _OrganismeCertification = value; }
        }

        public string OrganismeCertificationAsString
        {
            get
            {
                if (_OrganismeCertification != null)
                    return _OrganismeCertification.Nom;
                else
                    return string.Empty;
            }

        }

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public DateTime DateArrivee
        {
            get { return _DateArrivee; }
            set { _DateArrivee = value; }
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

        public string Commentaire
        {
            get { return _Commentaire; }
            set { _Commentaire = value; }
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

        public DateTime? DateDepotage
        {
            get
            {
                return _DateDepotage;
            }

            set
            {
                _DateDepotage = value;
            }
        }
        #endregion

        #region Constructor
        public CertificatPoids()
        {

        }

        public CertificatPoids(Guid myId)
        {
            this.fnGet(myId);
        }

        #endregion

        #region "Properties"
        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_CertificatPoids_Activate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@EmbarquementID", SqlDbType.UniqueIdentifier, _Embarquement.ID);
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
                throw new Exception(ex.Message + "\r\n" + "V2_CertificatPoids:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_CertificatPoids_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "V2_CertificatPoids:fnDeActivate");
            }
            return Result;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_CertificatPoids_Get", (Guid)Id);
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
            return fnSelect("{Tous}", -1, null, null, -1);
        }

        public List<DataPersist> fnSelect(string Campagne, int ExportateurID, DateTime? StartDate, DateTime? EndDate, int IsDisabled)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_CertificatPoids_Select");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@IsDisabled", SqlDbType.Int, IsDisabled);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    CertificatPoids mClass = new CertificatPoids();
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

                    mCommande = db().CreateStoredProcCommand("V2_CertificatPoids_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    //db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 9);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_CertificatPoids_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@EmbarquementID", SqlDbType.UniqueIdentifier, _Embarquement.ID);
                db().AddInParameter(mCommande, "@OrganismeCertificationID", SqlDbType.Int, _OrganismeCertification.ID);

                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _Date);
                db().AddInParameter(mCommande, "@DateArrivee", SqlDbType.DateTime, _DateArrivee);
                db().AddInParameter(mCommande, "@DateDepotage", SqlDbType.DateTime, _DateDepotage);

                db().AddInParameter(mCommande, "@PoidsArrivee", SqlDbType.Decimal, _PoidsArrivee);
                db().AddInParameter(mCommande, "@PoidsEchantillonArrivee", SqlDbType.Decimal, _PoidsEchantillonArrivee);

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
                        //if (this._isnew)
                        //{
                        //    _Numero = (string)db().Parameters(mCommande, "@Numero");
                        //}

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
                throw new Exception(ex.Message + "\r\n" + "V2_CertificatPoids:fnUpdate");

            }
            return Result;
        }
        #endregion
        #region "Private Methods"
        public override string ToString()
        {
            throw new NotImplementedException();
        }

        private static void MapFromDataReader(CertificatPoids mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["EmbarquementID"]))
                    {
                        mClass._Embarquement = new Embarquement();
                        mClass._Embarquement.ID = (Guid)mDataReader["EmbarquementID"];
                        mClass._Embarquement.Numero = (string)mDataReader["EmbarquementNumero"];
                        mClass._Embarquement.Contrat = new ContratDeVentes();
                        mClass._Embarquement.Contrat.ContratNumero = (string)mDataReader["ContratNumero"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["OrganismeCertificationID"]))
                    {
                        mClass._OrganismeCertification = new OrganismeCertification();
                        mClass._OrganismeCertification.ID = (int)mDataReader["OrganismeCertificationID"];
                        mClass._OrganismeCertification.Nom = (string)mDataReader["OrganismeCertificationNom"];
                    }

                    //if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DateCP"])) mClass._Date = (DateTime)mDataReader["DateCP"];

                    if (!DBNull.Value.Equals(mDataReader["DateArrivee"])) mClass._DateArrivee = (DateTime)mDataReader["DateArrivee"];
                    if (!DBNull.Value.Equals(mDataReader["DateDepotage"])) mClass._DateDepotage = (DateTime)mDataReader["DateDepotage"];

                    if (!DBNull.Value.Equals(mDataReader["PoidsArrivee"])) mClass._PoidsArrivee = (decimal)mDataReader["PoidsArrivee"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsEchantillonArrivee"])) mClass._PoidsEchantillonArrivee = (decimal)mDataReader["PoidsEchantillonArrivee"];

                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];

                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];

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
                throw new Exception(ex.Message + "\nV2_CertificatPoids:MapFromDataReader");
            }
        }
        #endregion
    }
    public partial class CertificatPoidsViewModel
    {
        public CertificatPoids _CertificatPoids { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
