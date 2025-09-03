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
    //[Proxy(Read = "~/Facture/Select")]
    //[JsonReader(RootProperty = "data")]
    public class Facture : DataPersist
    {
        #region "Fields"

        private Guid _ID;
        private string _Campagne;
        private BonDeLivraison _BonDeLivraison;
        private string _Numero;
        private DateTime _DateFacture;
        private decimal _PoidsNetAccepte;
        private string _Statut;
        private int _NbreSacs;
        private string _NumeroSticker;
        private decimal _Montant;
        private bool _isViewPending;
        private decimal _MontantValorisation;
        private decimal _MontantDeduction;

        private Site _Sites;
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

        public BonDeLivraison BonDeLivraison
        {
            get { return _BonDeLivraison; }
            set { _BonDeLivraison = value; }
        }

        public DateTime DateLivraison
        {
            get { return (_BonDeLivraison != null && _BonDeLivraison.Livraison != null) ? _BonDeLivraison.Livraison.DateLivraison : new DateTime(); }

        }

        public string DateLivraisonAsString
        {
            get { return (_BonDeLivraison != null && _BonDeLivraison.Livraison != null) ? _BonDeLivraison.Livraison.DateLivraison.ToShortDateString() : string.Empty; }

        }
        public string LibelleTypeDeLivraison
        {
            get { return _BonDeLivraison != null && _BonDeLivraison.Livraison != null && _BonDeLivraison.Livraison.LivraisonType != null ? _BonDeLivraison.Livraison.LivraisonType.Designation : string.Empty; }

        }
        public string LivraisonID
        {
            get { return _BonDeLivraison != null && _BonDeLivraison.Livraison != null ? _BonDeLivraison.Livraison.Numero : string.Empty; }
        }
       
        public string Immatriculation
        {
            get { return _BonDeLivraison != null && _BonDeLivraison.Livraison != null ? _BonDeLivraison.Livraison.Immatriculation : string.Empty; }

        }

        public string FournisseurNom
        {
            get { return _BonDeLivraison != null && _BonDeLivraison.Livraison != null ? _BonDeLivraison.Livraison.Fournisseur.Nom + " - " + _BonDeLivraison.Livraison.Fournisseur.ID : string.Empty; }
        }

        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        public DateTime DateFacture
        {
            get { return _DateFacture; }
            set { _DateFacture = value; }
        }

        public string DateFactureAsString
        {
            get { return _DateFacture != null ? _DateFacture.ToShortDateString() : string.Empty; }           
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

        public decimal PoidsNetAccepte
        {
            get { return _PoidsNetAccepte; }
            set { _PoidsNetAccepte = value; }
        }

        public string PoidsNetAccepteAsString
        {
            get { return _PoidsNetAccepte != 0 ? String.Format("{0:#,#}", _PoidsNetAccepte).TrimStart() : string.Empty; }
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

        public string MontantValorisationAsString
        {
            get { return _MontantValorisation != 0 ? String.Format("{0:#,#}", _MontantValorisation).TrimStart() : string.Empty; }
        }

        public string MontantDeductionAsString
        {
            get { return _MontantDeduction != 0 ? String.Format("{0:#,#}", _MontantDeduction).TrimStart() : string.Empty; }
        }

        public string Statut
        {
            get { return _Statut; }
            set { _Statut = value; }
        }

        public string NumeroSticker
        {
            get { return _NumeroSticker; }
            set { _NumeroSticker = value; }
        }

        public bool IsActive
        {
            get { return _Statut == "AP"; }

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

        public bool isViewPending
        {
            get { return _isViewPending; }
            set { _isViewPending = value; }
        }

        public decimal MontantValorisation
        {
            get { return _MontantValorisation; }
            set { _MontantValorisation = value; }
        }

        public decimal MontantDeduction
        {
            get { return _MontantDeduction; }
            set { _MontantDeduction = value; }
        }

        public Site Sites
        {
            get { return _Sites; }
            set { _Sites = value; }
        }

        public string SiteAsString
        {
            get { return _Sites != null ? _Sites.Nom : string.Empty; }
        }
        #endregion

        #region Constructor
        public Facture()
        {

        }

        public Facture(Guid myID)
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
                mDataReader = db().ExecuteReader("Facture_Get", (Guid)Id);
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

        public bool fnGetInvoiceByDeliveryNumber(string deliveryID)
        {
            bool Result = false;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("PeseeRefoulee_GetByDeliveryNumber");
                IDataReader mDataReader = null;
                db().AddInParameter(mCommande, "@Numero", SqlDbType.VarChar, this.Numero);                
                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                mDataReader = db().ExecuteReader(mCommande);

                string ErrorMessage = (string)db().Parameters(mCommande, "@ErrorMessage");
                if (String.IsNullOrEmpty(ErrorMessage))
                {
                    return false;
                }
                else
                {
                    throw new Exception(ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + "\r\n" + "Facture:fnGetInvoiceByDeliveryNumber");

            }
            return Result;
        }

        public override List<DataPersist> fnSelect()
        {
            return fnSelect("{Tous}", -1, -1, null, null, "-1",-1);
        }

        public List<DataPersist> fnSelect(string Campagne, int FournisseurID, int TypeID, DateTime? StartDate, DateTime? EndDate, string statut, int SiteID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Facture_Select");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@DeliveryTypeID", SqlDbType.Int, TypeID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@status", SqlDbType.VarChar, 2, statut);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, SiteID);                

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Facture mClass = new Facture();
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

        public List<DataPersist> fnSelectExtend(string Campagne, int FournisseurID, int TypeID, DateTime? StartDate, DateTime? EndDate, string statut, int SiteID, int siteFournisseur = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Facture_Site_Select");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@DeliveryTypeID", SqlDbType.Int, TypeID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@status", SqlDbType.VarChar, 2, statut);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, SiteID);
                db().AddInParameter(mCommande, "@SiteFournisseur", SqlDbType.Int, siteFournisseur);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Facture mClass = new Facture();
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
                mCommande = db().CreateStoredProcCommand("Facture_New");
                db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 10);
                db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                db().AddInParameter(mCommande, "@DeliveryNoteID", SqlDbType.UniqueIdentifier, _BonDeLivraison.ID);
                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _DateFacture);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@Sticker", SqlDbType.VarChar, _NumeroSticker);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, _Sites.ID);

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
                            _Statut = "AP";
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
                throw new Exception(ex.Message + "\r\n" + "Facture:fnUpdate");

            }
            return Result;

        }

        public bool fnUpdate(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("Facture_New");
                db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 10);
                db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                db().AddInParameter(mCommande, "@DeliveryNoteID", SqlDbType.UniqueIdentifier, _BonDeLivraison.ID);
                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _DateFacture);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@Sticker", SqlDbType.VarChar, _NumeroSticker);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, _Sites.ID);

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
                            _Statut = "AP";
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
                throw new Exception(ex.Message + "\r\n" + "Facture:fnUpdate");

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
            DataCommand mCommande = db().CreateStoredProcCommand("Facture_Cancel");
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
                throw new Exception(ex.Message + "\r\n" + "Facture:fnCancel");
            }
            return bolResult;
        }

        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _Numero;
        }

        private static void MapFromDataReader(Facture mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Campagne"])) mClass.Campagne = (string)mDataReader["Campagne"];

                    if (!DBNull.Value.Equals(mDataReader["BonDeLivraisonID"]))
                    {
                        BonDeLivraison mBL = new BonDeLivraison();
                        mBL.ID = (Guid)mDataReader["BonDeLivraisonID"];
                        mBL.DateBonDeLivraison = (DateTime)mDataReader["BonDeLivraisonDate"];

                        Livraison mLivraison = new Livraison();

                        mLivraison.ID = (Guid)mDataReader["LivraisonGuid"];
                        if (!DBNull.Value.Equals(mDataReader["LivraisonID"])) mLivraison.Numero = (string)mDataReader["LivraisonID"];
                        if (!DBNull.Value.Equals(mDataReader["DateLivraison"])) mLivraison.DateLivraison = (DateTime)mDataReader["DateLivraison"];
                        if (!DBNull.Value.Equals(mDataReader["Immatriculation"])) mLivraison.Immatriculation = (string)mDataReader["Immatriculation"];

                        Campagne mCampagne = new Campagne();
                        if (!DBNull.Value.Equals(mDataReader["Campagne"])) mCampagne.Designation = (string)mDataReader["Campagne"];
                        mLivraison.Campagne = mCampagne;

                        LivraisonType mLivraisonType = new LivraisonType();
                        if (!DBNull.Value.Equals(mDataReader["TypeLivraisonID"]))
                        {
                            mLivraisonType.ID = (int)mDataReader["TypeLivraisonID"];
                            mLivraisonType.Designation = (string)mDataReader["TypeLivraisonNom"];
                            mLivraison.LivraisonType = mLivraisonType;
                        }

                        Fournisseur mFournisseur = new Fournisseur();
                        if (!DBNull.Value.Equals(mDataReader["FournisseurID"]))
                        {
                            mFournisseur.ID = (int)mDataReader["FournisseurID"];
                            mFournisseur.Nom = (string)mDataReader["FournisseurNom"];
                            mLivraison.Fournisseur = mFournisseur;
                        }

                        SacType mSacType = new SacType();
                        if (!DBNull.Value.Equals(mDataReader["SacTypeID"]))
                        {
                            mSacType.ID = (int)mDataReader["SacTypeID"];
                            mSacType.Designation = (string)mDataReader["SacTypeNom"];
                            mLivraison.SacType = mSacType;
                        }

                        Certification mCertification = new Certification();
                        if (!DBNull.Value.Equals(mDataReader["CertificationID"]))
                        {
                            mCertification.ID = (int)mDataReader["CertificationID"];
                            mCertification.Designation = (string)mDataReader["CertificationNom"];
                            mLivraison.Certification = mCertification;
                        }

                        mBL.Livraison = mLivraison;

                        mClass.BonDeLivraison = mBL;
                    }

                    if (!DBNull.Value.Equals(mDataReader["DateFacture"])) mClass._DateFacture = (DateTime)mDataReader["DateFacture"];
                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["NumSticker"])) mClass._NumeroSticker = (string)mDataReader["NumSticker"];
                    if (!DBNull.Value.Equals(mDataReader["NbreSacs"])) mClass._NbreSacs = (int)mDataReader["NbreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._Montant = (decimal)mDataReader["Montant"];
                    if (!DBNull.Value.Equals(mDataReader["MontantValorisation"])) mClass._MontantValorisation = (decimal)mDataReader["MontantValorisation"];
                    if (!DBNull.Value.Equals(mDataReader["MontantDeduction"])) mClass._MontantDeduction = (decimal)mDataReader["MontantDeduction"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNetAccepte = (decimal)mDataReader["PoidsNet"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];                   

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];

                    mClass.Sites = new Site();
                    if (!DBNull.Value.Equals(mDataReader["SiteID"]))
                    {
                        mClass.Sites.ID = (int)mDataReader["SiteID"];
                        mClass.Sites.Nom = (string)mDataReader["SiteNom"];
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nFacture:MapFromDataReader");
            }
        }
        #endregion
    }

    public partial class FactureViewModel
    {
        public Facture _Facture { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
