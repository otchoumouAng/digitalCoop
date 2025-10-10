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
  
    public class FactureValorisation : DataPersist
    {
        #region "Fields"

        private Guid _ID;
        private Facture _Facture;
        private FactureValorisationType _ValorisationType;
        private Guid _TarificationID;
        private string _TarificationRef;
        private decimal _Tonnage;
        private decimal _Prix;
        private decimal _Montant;

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
                
        public FactureValorisationType ValorisationType
        {
            get { return _ValorisationType; }
            set { _ValorisationType = value; }
        }
        
        public Guid TarificationID
        {
            get { return _TarificationID; }
            set { _TarificationID = value; }
        }

        public string TarificationRef
        {
            get { return _TarificationRef; }
            set { _TarificationRef = value; }
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

        public decimal Prix
        {
            get { return _Prix; }
            set { _Prix = value; }
        }

        public string PrixAsString
        {
            get { return _Prix != 0 ? String.Format("{0:#,#}", _Prix).TrimStart() : string.Empty; }
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

        [Column(Ignore = true)]
        public string AsString
        {
            get { return _TarificationRef; }
        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
               return 2; //                     
            }
        }

        #endregion

        #region Constructor
        public FactureValorisation()
        {

        }

        public FactureValorisation(Guid myID)
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
                mDataReader = db().ExecuteReader("Facture_Valorisation_Get", (Guid)Id);
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
            return fnSelect(new Guid());
        }

        public List<DataPersist> fnSelect(Guid FactureID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Facture_Valorisation_Select");
                db().AddInParameter(mCommande, "@FactureID", SqlDbType.UniqueIdentifier, FactureID);
             
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FactureValorisation mClass = new FactureValorisation();
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

        public List<DataPersist> fnSelectAvailable(Guid BonDeLivraisonID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Facture_Valorisation_SelectAvailable");
                db().AddInParameter(mCommande, "@DeliveryNoteID", SqlDbType.UniqueIdentifier, BonDeLivraisonID);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FactureValorisation mClass = new FactureValorisation();
                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectAvailable");
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
                mCommande = db().CreateStoredProcCommand("Facture_Valorisation_New");
                db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddInParameter(mCommande, "@FactureID", SqlDbType.UniqueIdentifier, _Facture.ID);
                db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                db().AddInParameter(mCommande, "@ValorisationTypeID", SqlDbType.Int, _ValorisationType.ID);
                db().AddInParameter(mCommande, "@TarificationID", SqlDbType.UniqueIdentifier, _TarificationID);
                db().AddInParameter(mCommande, "@TarificationRef", SqlDbType.VarChar, 100, _TarificationRef);
                db().AddInParameter(mCommande, "@Tonnage", SqlDbType.Decimal, _Tonnage);
                db().AddInParameter(mCommande, "@Prix", SqlDbType.Money, _Prix);
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
                throw new Exception(ex.Message + "\r\n" + "FactureValorisation:fnUpdate");

            }
            return Result;

        }

        public bool fnUpdate(DataTransaction mTran)
        {

            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("Facture_Valorisation_New");
                db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddInParameter(mCommande, "@FactureID", SqlDbType.UniqueIdentifier, _Facture.ID);
                db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                db().AddInParameter(mCommande, "@ValorisationTypeID", SqlDbType.Int, _ValorisationType.ID);
                db().AddInParameter(mCommande, "@TarificationID", SqlDbType.UniqueIdentifier, _TarificationID);
                db().AddInParameter(mCommande, "@TarificationRef", SqlDbType.VarChar, 100, _TarificationRef);
                db().AddInParameter(mCommande, "@Tonnage", SqlDbType.Decimal, _Tonnage);
                db().AddInParameter(mCommande, "@Prix", SqlDbType.Money, _Prix);
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
                db().ExecuteNonQuery(ref mCommande, mTran);
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
                throw new Exception(ex.Message + "\r\n" + "FactureValorisation:fnUpdate");

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
        
        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _TarificationRef;
        }

        private static void MapFromDataReader(FactureValorisation mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["FactureID"]))
                    {
                        Facture mFacture = new Facture();
                        mFacture.ID = (Guid)mDataReader["FactureID"];
                        mFacture.Numero = (string)mDataReader["FactureNumero"];

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

                        Fournisseur mFournisseur = new Fournisseur();
                        mFournisseur.ID = 1;
                        mFournisseur.Nom = string.Empty;
                        mLivraison.Fournisseur = mFournisseur;

                        SacType mSacType = new SacType();
                        mSacType.ID = 0;
                        mSacType.Designation = string.Empty;
                        mLivraison.SacType = mSacType;                       

                        mBL.Livraison = mLivraison;

                        mFacture.BonDeLivraison = mBL;

                        mClass._Facture = mFacture;
                    }

                    if (!DBNull.Value.Equals(mDataReader["ValorisationTypeID"]))
                    {
                        FactureValorisationType mValoType = new FactureValorisationType();
                        mValoType.ID = (int)mDataReader["ValorisationTypeID"];
                        mValoType.Designation = (string)mDataReader["ValorisationTypeNom"];
                        mClass._ValorisationType = mValoType;
                    }
                    
                    if (!DBNull.Value.Equals(mDataReader["TarificationID"])) mClass._TarificationID = (Guid)mDataReader["TarificationID"];
                    if (!DBNull.Value.Equals(mDataReader["TarificationRef"])) mClass._TarificationRef = (string)mDataReader["TarificationRef"];
                    if (!DBNull.Value.Equals(mDataReader["Tonnage"])) mClass._Tonnage = (decimal)mDataReader["Tonnage"];
                    if (!DBNull.Value.Equals(mDataReader["Prix"])) mClass._Prix = (decimal)mDataReader["Prix"];
                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._Montant = (decimal)mDataReader["Montant"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nFactureValorisation:MapFromDataReader");
            }
        }
        #endregion

    }

    public partial class FactureValorisationViewModel
    {
        public FactureValorisation _FactureValorisation { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
