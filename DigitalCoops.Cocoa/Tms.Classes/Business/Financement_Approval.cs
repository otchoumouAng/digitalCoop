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
    [Proxy(Read = "~/Financement/SelectForApproval")]
    [JsonReader(RootProperty = "data")]
    public class Financement_Approval : DataPersist
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
            get { return _Fournisseur.Nom + " - " + _Fournisseur.ID; }

        }

        public FinancementType FinancementType
        {
            get { return _FinancementType; }
            set { _FinancementType = value; }
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


        public decimal Prix
        {
            get { return _Prix; }
            set { _Prix = value; }
        }

        public decimal Montant
        {
            get { return _Montant; }
            set { _Montant = value; }
        }

        public DateTime DateEcheance
        {
            get { return _DateEcheance; }
            set { _DateEcheance = value; }
        }

        public PrelevementMode PrelevementMode
        {
            get { return _PrelevementMode; }
            set { _PrelevementMode = value; }
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

        #endregion

        #region Constructor
        public Financement_Approval()
        {

        }

        public Financement_Approval(Guid myID)
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

        public override List<DataPersist> fnSelect()
        {
            return fnSelect("", -1, -1, null, null, "-1");
        }

        public List<DataPersist> fnSelect(string Campagne, int FournisseurID, int TypeID, DateTime? StartDate, DateTime? EndDate, string statut)
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
                db().AddInParameter(mCommande, "@status", SqlDbType.VarChar, 2, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Financement_Approval mClass = new Financement_Approval();
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


        public List<DataPersist> fnSelectForApproval(string Campagne, int FournisseurID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Financement_SelectForApproval");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, FournisseurID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Financement_Approval mClass = new Financement_Approval();
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
            return false;
        }

        public override bool fnActivate()
        {
            return false;
        }

        public override bool fnDeActivate()
        {
            return false;
        }
              

        public bool fnApprove()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("Financement_Approve");
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
                throw new Exception(ex.Message + "\r\n" + "Financement_Approval:fnSetToLoss");
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
                throw new Exception(ex.Message + "\r\n" + "Financement_Approval:fnReject");
            }
            return bolResult;
        }


        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _Numero;
        }

        private static void MapFromDataReader(Financement_Approval mClass, IDataReader mDataReader)
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
                    if (!DBNull.Value.Equals(mDataReader["DateEcheance"])) mClass._DateEcheance = (DateTime)mDataReader["DateEcheance"];
                    if (!DBNull.Value.Equals(mDataReader["RemboursementTaux"])) mClass._PrelevementTaux = (decimal)mDataReader["RemboursementTaux"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nlbc_Financement:MapFromDataReader");
            }
        }
        #endregion

    }
    

}

