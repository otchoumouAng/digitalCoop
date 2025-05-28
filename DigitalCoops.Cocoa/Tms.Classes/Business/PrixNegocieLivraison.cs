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
    //[Proxy(Read = "~/SpotPrice/Select")]
    //[JsonReader(RootProperty = "data")]
    public class PrixNegocieLivraison : DataPersist
    {
        #region "Fields"
        private Guid _ID;
        private bool _Desactive;
        private PrixNegocie _PrixNegocie;
        private Livraison _Livraison;

        #endregion

        #region "Properties"
        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
        }

        public PrixNegocie PrixNegocie
        {
            get { return _PrixNegocie; }
            set { _PrixNegocie = value; }
        }

        public Livraison Livraison
        {
            get { return _Livraison; }
            set { _Livraison = value; }
        }

        public string DateDebutPN
        {
            get { return _PrixNegocie == null ? string.Empty : (_PrixNegocie.DateDebut == null? string.Empty : ((DateTime)_PrixNegocie.DateDebut).ToShortDateString()); }
        }

        public string DateEcheancePN
        {
            get { return _PrixNegocie == null ? string.Empty : (_PrixNegocie.DateEcheance == null ? string.Empty : ((DateTime)_PrixNegocie.DateEcheance).ToShortDateString()); }
        }

        public string DatePrixPN
        {
            get { return _PrixNegocie == null ? string.Empty : ((DateTime)_PrixNegocie.DatePrix).ToShortDateString(); }
        }

        public string DesacticePN
        {
            get { return _PrixNegocie == null ? string.Empty : _PrixNegocie.Desactive.ToString(); }
        }

        public string statutPN
        {
            get { return _PrixNegocie == null ? string.Empty : _PrixNegocie.Statut; }
        }

        public string PrixPN
        {
            get { return _PrixNegocie == null ? string.Empty : String.Format("{0:# ### ### ###}", _PrixNegocie.Prix).TrimStart(); }
        }

        public string FournisseurNomPN
        {
            get { return _PrixNegocie == null ? string.Empty : _PrixNegocie.Fournisseur.Nom + " - " + _PrixNegocie.Fournisseur.ID; }
        }

        public string CommentairePN
        {
            get { return _PrixNegocie == null ? string.Empty : _PrixNegocie.Commentaire; }
        }

        public string NumeroPN
        {
            get { return _PrixNegocie == null ? string.Empty : _PrixNegocie.Numero; }
        }

        public string DeliveriesNumbersPN
        {
            get { return _Livraison == null ? string.Empty : _Livraison.Numero; }
        }


        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_PrixNegocie.Desactive)
                    return 0; // BulletCross  
                else if (_PrixNegocie.Statut == "AP")
                    return 1; // Tick              
                else
                    return 2; //                     
            }
        }

        public string SiteAsString
        {
            get { return _PrixNegocie != null && _PrixNegocie.Site != null ? _PrixNegocie.Site.Nom : string.Empty; }
        }
        #endregion

        #region "Constructor"

        public PrixNegocieLivraison()
        {

        }

        public PrixNegocieLivraison(Guid myId)
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
                //mDataReader = db().ExecuteReader("PrixNegocieLivraison_Get", (Guid)Id);
                mDataReader = db().ExecuteReader("PrixNegocie_Get", (Guid)Id);
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
            return fnSelect();
        }

        public List<DataPersist> fnSelect(int statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                //DataCommand mCommande = db().CreateStoredProcCommand("PrixNegocieLivraison_Select");
                DataCommand mCommande = db().CreateStoredProcCommand("PrixNegocie_Select");

                db().AddInParameter(mCommande, "@status", SqlDbType.Int, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PrixNegocieLivraison mClass = new PrixNegocieLivraison();

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

        public List<DataPersist> fnSelect(string Crop, int siteID, int fournisseurID, DateTime? startdate, DateTime? enddate, string statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("PrixNegocie_Select");
                db().AddInParameter(mCommande, "@cropyear", SqlDbType.Char, Crop);
                db().AddInParameter(mCommande, "@siteid", SqlDbType.Int, siteID);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, fournisseurID);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, startdate);
                db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, enddate);
                db().AddInParameter(mCommande, "@status", SqlDbType.VarChar,2, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PrixNegocieLivraison mClass = new PrixNegocieLivraison();

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

                    mCommande = db().CreateStoredProcCommand("PrixNegocieLivraison_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("PrixNegocieLivraison_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                
                db().AddInParameter(mCommande, "@prixnegocieID", SqlDbType.UniqueIdentifier, _PrixNegocie.ID);
                db().AddInParameter(mCommande, "@livraisonID", SqlDbType.UniqueIdentifier, _Livraison.ID);
                db().AddInParameter(mCommande, "@desactive", SqlDbType.Bit, _Desactive);

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
                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        _ID = (Guid)db().Parameters(mCommande, "@ID");
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
                throw new Exception(ex.Message + "\r\n" + "PrixNegocieLivraison:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("PrixNegocieLivraison_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("PrixNegocieLivraison_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@prixnegocieID", SqlDbType.UniqueIdentifier, _PrixNegocie.ID);
                db().AddInParameter(mCommande, "@livraisonID", SqlDbType.UniqueIdentifier, _Livraison.ID);
                db().AddInParameter(mCommande, "@desactive", SqlDbType.Bit, _Desactive);

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
                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        _ID = (Guid)db().Parameters(mCommande, "@ID");
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
                throw new Exception(ex.Message + "\r\n" + "PrixNegocieLivraison:fnUpdate");

            }
            return Result;
        }


        public override bool fnActivate()
        {
            if (!this._isnew)
            {
                bool Result;
                DataCommand mCommande = db().CreateStoredProcCommand("PrixNegocieLivraison_Activate");
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
                    throw new Exception(ex.Message + "\r\n" + "PrixNegocieLivraison:fnActivate");
                }
                return Result;
            }
            return false;
        }

        public override bool fnDeActivate()
        {
            if (!this._isnew)
            {
                bool bolResult;
                DataCommand mCommande = db().CreateStoredProcCommand("PrixNegocieLivraison_DeActivate");
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
                    throw new Exception(ex.Message + "\r\n" + "PrixNegocieLivraison:fnDeActivate");
                }
                return bolResult;
            }
            return false;
        }

        public bool fnRemoveDelivery(Guid livraison)
        {            
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("PrixNegocieLivraison_Remove");
            db().AddInParameter(mCommande, "@Livraison", SqlDbType.UniqueIdentifier, livraison);
            //db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
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
                throw new Exception(ex.Message + "\r\n" + "PrixNegocieLivraison:fnDeActivate");
            }
            return bolResult;
          
        }



        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return string.Empty;
        }

        private static void MapFromDataReader(PrixNegocieLivraison mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._PrixNegocie = new PrixNegocie();
                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._PrixNegocie.ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["datePrix"])) mClass._PrixNegocie.DatePrix = (DateTime)mDataReader["datePrix"];
                    if (!DBNull.Value.Equals(mDataReader["prix"])) mClass._PrixNegocie.Prix = (decimal)mDataReader["prix"];
                    if (!DBNull.Value.Equals(mDataReader["datedebut"])) mClass._PrixNegocie.DateDebut = (DateTime)mDataReader["datedebut"];
                    if (!DBNull.Value.Equals(mDataReader["dateecheance"])) mClass._PrixNegocie.DateEcheance = (DateTime)mDataReader["dateecheance"];
                    if (!DBNull.Value.Equals(mDataReader["desactive"])) mClass._PrixNegocie.Desactive = (bool)mDataReader["desactive"];
                    if (!DBNull.Value.Equals(mDataReader["statut"])) mClass._PrixNegocie.Statut = (string)mDataReader["statut"];
                    if (!DBNull.Value.Equals(mDataReader["commentaire"])) mClass._PrixNegocie.Commentaire = (string)mDataReader["commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._PrixNegocie.Numero = (string)mDataReader["Numero"];

                    mClass._PrixNegocie.Fournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["fournisseurID"])) mClass._PrixNegocie.Fournisseur.ID = (int)mDataReader["fournisseurID"];
                    if (!DBNull.Value.Equals(mDataReader["fournisseurNom"])) mClass._PrixNegocie.Fournisseur.Nom = (string)mDataReader["fournisseurNom"];
                    if (!DBNull.Value.Equals(mDataReader["fournisseurCode"])) mClass._PrixNegocie.Fournisseur.CompteNumero = (string)mDataReader["fournisseurCode"];

                    mClass._PrixNegocie.Site = new Shared.Site();
                    if (!DBNull.Value.Equals(mDataReader["siteID"])) mClass._PrixNegocie.Site.ID = (int)mDataReader["siteID"];
                    if (!DBNull.Value.Equals(mDataReader["siteNom"])) mClass._PrixNegocie.Site.Nom = (string)mDataReader["siteNom"];

                    mClass._PrixNegocie.ModeApplication = new PrixNegocieModeApplication();
                    if (!DBNull.Value.Equals(mDataReader["modeID"])) mClass._PrixNegocie.ModeApplication.ID = (int)mDataReader["modeID"];

                    mClass._Livraison = new Livraison();
                    //if (!DBNull.Value.Equals(mDataReader["livraisonID"])) mClass._Livraison.ID = (Guid)mDataReader["livraisonID"];                    
                    //if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["DeliveriesNumbers"])) mClass._Livraison.Numero = (string)mDataReader["DeliveriesNumbers"];                    

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nPrixNegocieLivraison:MapFromDataReader");
            }
        }
        #endregion



    }
}
