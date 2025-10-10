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
    //[Proxy(Read = "~/MiseEnComptePlanifiee/Select")]
    //[JsonReader(RootProperty = "data")]
    public class MiseEnComptePlanifiee : DataPersist
    {
        #region "Fields"
        private Guid _ID;
        private string _Campagne;
        private Fournisseur _Fournisseur;
        private MiseEnCompteType _MiseEnCompteType;
        private string _Numero;
        private DateTime _DateMiseEnCompte;
        private DateTime _DateEcheance;
        private MiseEnComptePlanifieeMode _PrelevementMode;
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

        public MiseEnCompteType MiseEnCompteType
        {
            get { return _MiseEnCompteType; }
            set { _MiseEnCompteType = value; }
        }

        public string LibelleMiseEnCompteType
        {
            get { return _MiseEnCompteType != null ? _MiseEnCompteType.Designation : string.Empty; }

        }

        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }


        public DateTime DateMiseEnCompte
        {
            get { return _DateMiseEnCompte; }
            set { _DateMiseEnCompte = value; }
        }        
       
        public DateTime DateEcheance
        {
            get { return _DateEcheance; }
            set { _DateEcheance = value; }
        }

        public MiseEnComptePlanifieeMode PrelevementMode
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

        public string PrelevementTauxAsString
        {
            get { return _PrelevementTaux != 0 ? String.Format("{0:#,#}", _PrelevementTaux).TrimStart() : string.Empty; }
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
                else
                    return 2; //                     
            }
        } 

      
        #endregion

        #region Constructor
        public MiseEnComptePlanifiee()
        {

        }

        public MiseEnComptePlanifiee(Guid myID)
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
                mDataReader = db().ExecuteReader("MiseEnComptePlanifiee_Get", (Guid)Id);
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
                mDataReader = db().ExecuteReader("MiseEnComptePlanifiee_GetByNumber", (int)Number);
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
            return fnSelect(-1,  null, null, "-1");
        }

        public List<DataPersist> fnSelect(int FournisseurID, DateTime? StartDate, DateTime? EndDate, string statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("MiseEnComptePlanifiee_Select");
                db().AddInParameter(mCommande, "@FournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@status", SqlDbType.VarChar, 2, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    MiseEnComptePlanifiee mClass = new MiseEnComptePlanifiee();
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

                    mCommande = db().CreateStoredProcCommand("MiseEnComptePlanifiee_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 8);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("MiseEnComptePlanifiee_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, _Campagne);
                db().AddInParameter(mCommande, "@TypeId", SqlDbType.Int, _MiseEnCompteType.ID);
                db().AddInParameter(mCommande, "@FournisseurID", SqlDbType.Int, _Fournisseur.ID);
                db().AddInParameter(mCommande, "@PrelevementModeID", SqlDbType.Int, _PrelevementMode.ID);
                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _DateMiseEnCompte);
                db().AddInParameter(mCommande, "@DateEcheance", SqlDbType.DateTime, _DateEcheance);
                db().AddInParameter(mCommande, "@PrelevementTaux", SqlDbType.Float, _PrelevementTaux);
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
                throw new Exception(ex.Message + "\r\n" + "MiseEnComptePlanifiee:fnUpdate");

            }
            return Result;
        }

        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("MiseEnComptePlanifiee_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "MiseEnComptePlanifiee:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("MiseEnComptePlanifiee_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "MiseEnComptePlanifiee:fnDeActivate");
            }
            return bolResult;
        }

        public bool fnCancel()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("MiseEnComptePlanifiee_Cancel");
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
                throw new Exception(ex.Message + "\r\n" + "MiseEnComptePlanifiee:fnCancel");
            }
            return bolResult;
        }      


        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _Numero;
        }

        private static void MapFromDataReader(MiseEnComptePlanifiee mClass, IDataReader mDataReader)
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
                        mClass._MiseEnCompteType = new MiseEnCompteType();
                        mClass._MiseEnCompteType.ID = (int)mDataReader["TypeID"];
                        mClass._MiseEnCompteType.Designation = (string)mDataReader["TypeNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["PrelevementModeID"]))
                    {
                        mClass._PrelevementMode = new MiseEnComptePlanifieeMode();
                        mClass._PrelevementMode.ID = (int)mDataReader["PrelevementModeID"];
                        mClass._PrelevementMode.Designation = (string)mDataReader["PrelevementModeNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DateMec"])) mClass._DateMiseEnCompte = (DateTime)mDataReader["DateMec"];
                    if (!DBNull.Value.Equals(mDataReader["DateEcheance"])) mClass._DateEcheance = (DateTime)mDataReader["DateEcheance"];
                    if (!DBNull.Value.Equals(mDataReader["RemboursementTaux"])) mClass._PrelevementTaux = (decimal)mDataReader["RemboursementTaux"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];
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
                throw new Exception(ex.Message + "\nMiseEnComptePlanifiee:MapFromDataReader");
            }
        }
        

        #endregion

    }

    public partial class MiseEnComptePlanifieeViewModel
    {
        public MiseEnComptePlanifiee _MiseEnComptePlanifiee { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }


}
