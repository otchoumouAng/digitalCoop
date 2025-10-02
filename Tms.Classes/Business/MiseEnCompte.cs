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
    //[Proxy(Read = "~/MiseEnCompte/Select")]
    //[JsonReader(RootProperty = "data")]
    public class MiseEnCompte : DataPersist
    {
        #region "Fields"
        private Guid _ID;
        private Facture _Facture;
        private string _Numero;
        private DateTime _DateMec;
        private Fournisseur _Fournisseur;
        private MiseEnCompteType _MiseEnCompteType;
        private decimal _Montant;        
        private bool _Desactive;
        private decimal _Taux;
        private decimal _Balance;           

        //For invoice purpose
        private decimal _MontantDisponible;
        private decimal _PoidsNet;
        private string _ListOfMecDeducted;
        private Site _Site;
        #endregion

        #region "Properties"

        [ModelField(IDProperty = true, SortType = Ext.Net.SortTypeMethod.None, SortDir = Ext.Net.SortDirection.ASC)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Facture Facture
        {
            get { return _Facture; }
            set { _Facture = value; }
        }

        public string FactureNumero
        {
            get { return _Numero; }
            set { _Numero = value; }

        }

        public DateTime DateMec
        {
            get { return _DateMec; }
            set { _DateMec = value; }
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

        public Fournisseur Fournisseur
        {
            get { return _Fournisseur; }
            set { _Fournisseur = value; }
        }

        public string FournisseurNameAndCode
        {
            get { return _Fournisseur.Nom + " - " + _Fournisseur.ID; }

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
        public decimal Taux
        {
            get { return _Taux; }
            set { _Taux = value; }
        }

        public decimal Balance
        {
            get { return _Balance; }
            set { _Balance = value; }
        }

        public string BalanceAsString
        {
            get { return _Balance != 0 ? String.Format("{0:#,#}", _Balance).TrimStart() : string.Empty; }
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
                return 2;                      
            }
        }

        public decimal MontantDisponible
        {
            get { return _MontantDisponible; }
            set { _MontantDisponible = value; }
        }

        public string MontantDisponibleAsString
        {
            get { return _MontantDisponible != 0 ? String.Format("{0:#,#}", _MontantDisponible).TrimStart() : "0"; }
        }

        public decimal PoidsNet
        {
            get { return _PoidsNet; }
            set { _PoidsNet = value; }
        }

        public string PoidsNetAsString
        {
            get { return _PoidsNet != 0 ? String.Format("{0:#,#}", _PoidsNet).TrimStart() : string.Empty; }
        }

        public string ListOfMecDeducted
        {
            get { return _ListOfMecDeducted; }
            set { _ListOfMecDeducted = value; }
        }

        public Site Site
        {
            get
            {
                return _Site;
            }

            set
            {
                _Site = value;
            }
        }

        public string SiteAsString
        {
            get { return _Site != null ? _Site.Nom : string.Empty;  }

        }
        #endregion

        #region Constructor
        public MiseEnCompte()
        {

        }

        public MiseEnCompte(Guid myID)
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
                mDataReader = db().ExecuteReader("MiseEnCompte_Get", (Guid)Id);
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
            return fnSelect(-1, null, null, "-1",1);
        }

        public List<DataPersist> fnSelect(int FournisseurID, DateTime? StartDate, DateTime? EndDate, string statut, int siteID = 1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("MiseEnCompte_Select");
                db().AddInParameter(mCommande, "@FournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@Status", SqlDbType.VarChar, 2, statut);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    MiseEnCompte mClass = new MiseEnCompte();
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

        public List<DataPersist> fnSelectForInvoiceDeduction(int FournisseurID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Facture_Deduction_SelectAvailableFinancing");
                db().AddInParameter(mCommande, "@SupplierID", SqlDbType.Int, FournisseurID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    MiseEnCompte mClass = new MiseEnCompte();
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

                    mCommande = db().CreateStoredProcCommand("MiseEnCompte_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("MiseEnCompte_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@FactureID", SqlDbType.UniqueIdentifier,_Facture.ID);
                db().AddInParameter(mCommande, "@TypeId", SqlDbType.Int, _MiseEnCompteType.ID);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);              

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
                throw new Exception(ex.Message + "\r\n" + "MiseEnCompte:fnUpdate");

            }
            return Result;
        }

        public override bool fnActivate()
        {
           return false;
        }

        public override bool fnDeActivate()
        {
            return false;
        }

        public bool fnCancel()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("MiseEnCompte_Cancel");
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
                throw new Exception(ex.Message + "\r\n" + "MiseEnCompte:fnCancel");
            }
            return bolResult;
        }       
      
        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _Facture.Numero;
        }

        private static void MapFromDataReader(MiseEnCompte mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["FactureNumero"])) mClass._Numero = (string)mDataReader["FactureNumero"];
                    Fournisseur mFournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["FournisseurID"]))
                    {
                        mFournisseur.ID = (int)mDataReader["FournisseurID"];
                        mFournisseur.Nom = (string)mDataReader["FournisseurNom"];
                        mClass._Fournisseur = mFournisseur;
                    }
                    if (!DBNull.Value.Equals(mDataReader["FactureID"]))
                    {
                        Facture mFacture = new Facture();
                        mFacture.ID = (Guid)mDataReader["FactureID"];
                        BonDeLivraison mBL = new BonDeLivraison();
                        mBL.ID = new Guid();
                        Livraison mLivraison = new Livraison();
                        mLivraison.ID = new Guid();
                        mLivraison.Numero = string.Empty;
                        mLivraison.DateLivraison = DateTime.Now;
                        mLivraison.Immatriculation = string.Empty;

                        LivraisonType mLivraisonType = new LivraisonType();
                        mLivraisonType.ID = 0;
                        mLivraisonType.Designation = string.Empty;
                        mLivraison.LivraisonType = mLivraisonType;

                        SacType mSacType = new SacType();
                        mSacType.ID = 0;
                        mSacType.Designation = string.Empty;
                        mLivraison.SacType = mSacType;
                        mLivraison.Fournisseur = mFournisseur;

                        mBL.Livraison = mLivraison;

                        mFacture.BonDeLivraison = mBL;

                        mClass._Facture = mFacture;
                    }

                    if (!DBNull.Value.Equals(mDataReader["TypeID"]))
                    {
                        mClass._MiseEnCompteType = new MiseEnCompteType();
                        mClass._MiseEnCompteType.ID = (int)mDataReader["TypeID"];
                        mClass._MiseEnCompteType.Designation = (string)mDataReader["TypeNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["DateFacture"])) mClass._DateMec = (DateTime)mDataReader["DateFacture"];
                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._Montant = (decimal)mDataReader["Montant"];
                    if (!DBNull.Value.Equals(mDataReader["Taux"])) mClass._Taux = (decimal)mDataReader["Taux"];
                    if (!DBNull.Value.Equals(mDataReader["Solde"])) mClass._Balance = (decimal)mDataReader["Solde"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    
                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];
                    
                    mClass._Site = new Site();
                    if (!DBNull.Value.Equals(mDataReader["siteID"]))
                    {
                        mClass._Site.ID = (int)mDataReader["siteID"];
                        mClass._Site.Nom = (string)mDataReader["NomSite"];                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nMiseEnCompte:MapFromDataReader");
            }
        }
      

        #endregion

    }

    public partial class MiseEnCompteViewModel
    {
        public MiseEnCompte _MiseEnCompte { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }


}
