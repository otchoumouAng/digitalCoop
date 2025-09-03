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
    //[Proxy(Read = "~/Facture/SelectDeduction")]
    //[JsonReader(RootProperty = "data")]
    public class FactureDeduction : DataPersist
    {
        #region "Fields"
        private Guid _ID;
        private Facture _Facture;
        private FactureDeductionType _DeductionType;
        private Guid _ElementID;
        private string _ElementRef;
        private decimal _Taux;
        private decimal _Montant; 
        private string _Libelle;
        private int _ElementTypeID;

        //For Transport
        private decimal _Tonnage;
        private FactureTransport _FactureTransport;

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

        public FactureDeductionType DeductionType
        {
            get { return _DeductionType; }
            set { _DeductionType = value; }
        }

        public string Libelle
        {
            get { return _Libelle; }
            set { _Libelle = value; }
        }

        public Guid ElementID
        {
            get { return _ElementID; }
            set { _ElementID = value; }
        }

        public string ElementRef
        {
            get { return _ElementRef; }
            set { _ElementRef = value; }
        }

        public decimal Taux
        {
            get { return _Taux; }
            set { _Taux = value; }
        }

        public string TauxAsString
        {
            get { return _Taux != 0 ? String.Format("{0:#,#}", _Taux).TrimStart() : string.Empty; }
        }

        public string DeductionTransportAsString
        {
            get { return _FactureTransport != null && _FactureTransport.Deduction != 0 ? String.Format("{0:#,#}", _FactureTransport.Deduction).TrimStart() : "0"; }
        }

        public decimal Montant
        {
            get { return _Montant; }
            set { _Montant = value; }
        }

        public int ElementTypeID
        {
            get { return _ElementTypeID; }
            set { _ElementTypeID = value; }

        }

        [Column(Ignore = true)]
        public string AsString
        {
            get { return _ElementRef; }
        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                return 3; //                     
            }
        }

        public string MontantAsString
        {
            get { return _Montant != 0 ? String.Format("{0:# ### ### ### ###}", _Montant).TrimStart() : string.Empty; }
        }

        //For Transport Deduction
        public decimal Tonnage
        {
            get { return _Tonnage; }
            set { _Tonnage = value; }
        }

        public string NumeroLivraison
        {
            get { return (_Facture != null && _Facture.BonDeLivraison != null && _Facture.BonDeLivraison.Livraison != null) ? _Facture.BonDeLivraison.Livraison.Numero : string.Empty ; }            
        }

        public string NomFournisseur
        {
            get { return (_Facture !=null &&  _Facture.BonDeLivraison != null && _Facture.BonDeLivraison.Livraison.Fournisseur != null) ? _Facture.BonDeLivraison.Livraison.Fournisseur.Nom : string.Empty; }
        }

        public string DateFacture
        {
            get { return _Facture != null ? _Facture.DateFacture.ToString() : string.Empty; }
        }

        public FactureTransport FactureTransport
        {
            get
            {
                return _FactureTransport;
            }

            set
            {
                _FactureTransport = value;
            }
        }

        #endregion

        #region Constructor
        public FactureDeduction()
        {

        }

        public FactureDeduction(Guid myID)
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
                mDataReader = db().ExecuteReader("Facture_Deduction_Get", (Guid)Id);
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

        public bool fnBicValide()
        {
            bool bolResult = false;
            IDataReader mdataReader = null;
            try
            {
                DataCommand mCommand = db().CreateStoredProcCommand("Facture_Deduction_BicValide");

                db().AddInParameter(mCommand, "@DeliveryNoteID", SqlDbType.UniqueIdentifier, _ElementID);                                
                db().AddOutParameter(mCommand, "@EstValide", SqlDbType.Bit, 0);
                db().AddParameter(mCommand, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                //mdataReader = db().ExecuteReader(mCommand);
                db().ExecuteNonQuery(ref mCommand);
                switch ((int)db().Parameters(mCommand, "ReturnValue"))
                {
                    case 0:                                                                                                
                        bolResult = (bool)db().Parameters(mCommand, "@EstValide");

                        break;
                    default:
                        //Unkown error
                        bolResult = false;
                        break;
                }

                //if (mdataReader.Read())
                //{
                //    bolResult = (bool)db().Parameters(mCommand, "@EstValide");
                //}                
            }
            catch (Exception ex)
            {
                bolResult = false;
                throw new Exception(ex.Message + Environment.NewLine);
            }
            finally
            {
                if (mdataReader != null) mdataReader.Close();
            }
            return bolResult;
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
                DataCommand mCommande = db().CreateStoredProcCommand("Facture_Deduction_Select");
                db().AddInParameter(mCommande, "@FactureID", SqlDbType.UniqueIdentifier, FactureID);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FactureDeduction mClass = new FactureDeduction();
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

        public List<DataPersist> fnSelectAvailable(Guid BonDeLivraisonID, decimal mTotalValorisation)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Facture_Deduction_SelectAvailable");
                db().AddInParameter(mCommande, "@DeliveryNoteID", SqlDbType.UniqueIdentifier, BonDeLivraisonID);
                db().AddInParameter(mCommande, "@TotalValorisation", SqlDbType.Decimal, mTotalValorisation);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FactureDeduction mClass = new FactureDeduction();
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

        public List<DataPersist> fnSelectByTransporteur(int transporteurID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("FactureTransport_SelectDeductionByTransporteur");
                db().AddInParameter(mCommande, "@transpoteurID", SqlDbType.Int, transporteurID);                

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FactureDeduction mClass = new FactureDeduction();
                    MapFromDataReaderForTransport(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectByTransporteur");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectDetailForFactureTransport(Guid factureID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("FactureTransportTransaction_SelectDeductionDetail");
                db().AddInParameter(mCommande, "@facturetransportID", SqlDbType.UniqueIdentifier, factureID);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FactureDeduction mClass = new FactureDeduction();
                    MapFromDataReaderForTransport(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectByTransporteur");
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

                    mCommande = db().CreateStoredProcCommand("Facture_Deduction_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@FactureID", SqlDbType.UniqueIdentifier, _Facture.ID);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                    db().AddInParameter(mCommande, "@DeductionTypeID", SqlDbType.Int, _DeductionType.ID);
                    db().AddInParameter(mCommande, "@ObjetID", SqlDbType.UniqueIdentifier, _ElementID);
                    db().AddInParameter(mCommande, "@ObjetRef", SqlDbType.VarChar, 100, _ElementRef);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("Facture_Deduction_Modify");
                }
                    
                db().AddInParameter(mCommande, "@Taux", SqlDbType.Decimal, _Taux);
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
                throw new Exception(ex.Message + "\r\n" + "FactureDeduction:fnUpdate");

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
                    mCommande = db().CreateStoredProcCommand("Facture_Deduction_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@FactureID", SqlDbType.UniqueIdentifier, _Facture.ID);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                    db().AddInParameter(mCommande, "@DeductionTypeID", SqlDbType.Int, _DeductionType.ID);
                    db().AddInParameter(mCommande, "@ObjetID", SqlDbType.UniqueIdentifier, _ElementID);
                    db().AddInParameter(mCommande, "@ObjetRef", SqlDbType.VarChar, 100, _ElementRef);
                    db().AddInParameter(mCommande, "@Libelle", SqlDbType.VarChar, 200, _Libelle);
                    db().AddInParameter(mCommande, "@ObjetTypeID", SqlDbType.Int, _ElementTypeID);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("Facture_Deduction_Modify");
                }

                db().AddInParameter(mCommande, "@Taux", SqlDbType.Decimal, _Taux);
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
                throw new Exception(ex.Message + "\r\n" + "FactureDeduction:fnUpdate");

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

        public bool fnRemove()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("Facture_Deduction_Remove");
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
                throw new Exception(ex.Message + "\r\n" + "FactureDeduction:fnRemove");
            }
            return bolResult;
        }

        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _ElementRef;
        }

        private static void MapFromDataReader(FactureDeduction mClass, IDataReader mDataReader)
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

                    if (!DBNull.Value.Equals(mDataReader["DeductionTypeID"]))
                    {
                        FactureDeductionType mDeductionType = new FactureDeductionType();
                        mDeductionType.ID = (int)mDataReader["DeductionTypeID"];
                        mDeductionType.Designation = (string)mDataReader["DeductionTypeNom"];
                        mClass._DeductionType = mDeductionType;
                    }

                    if (!DBNull.Value.Equals(mDataReader["ObjetID"])) mClass._ElementID = (Guid)mDataReader["ObjetID"];
                    if (!DBNull.Value.Equals(mDataReader["ObjetRef"])) mClass._ElementRef = (string)mDataReader["ObjetRef"];
                    if (!DBNull.Value.Equals(mDataReader["Libelle"])) mClass._Libelle = (string)mDataReader["Libelle"];
                    if (!DBNull.Value.Equals(mDataReader["ObjetTypeID"])) mClass._ElementTypeID = (int)mDataReader["ObjetTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["Taux"])) mClass._Taux = (decimal)mDataReader["Taux"];
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
                throw new Exception(ex.Message + "\nFactureDeduction:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderForTransport(FactureDeduction mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["DeductionID"])) mClass._ID = (Guid)mDataReader["DeductionID"];

                    mClass._Facture = new Facture();

                    if (!DBNull.Value.Equals(mDataReader["DateInvoice"])) mClass._Facture.DateFacture = (DateTime)mDataReader["DateInvoice"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroInvoice"])) mClass._Facture.Numero = (string)mDataReader["NumeroInvoice"];
                    mClass._Facture.BonDeLivraison = new BonDeLivraison();
                    mClass._Facture.BonDeLivraison.Livraison = new Livraison();
                    mClass._Facture.BonDeLivraison.Livraison.Fournisseur = new Fournisseur();
                    //mClass._Facture.BonDeLivraison.Livraison.Transporteur = new Transporteur();
                    if (!DBNull.Value.Equals(mDataReader["NumeroLivraison"])) mClass._Facture.BonDeLivraison.Livraison.Numero = (string)mDataReader["NumeroLivraison"];
                    if (!DBNull.Value.Equals(mDataReader["FournisseurNom"])) mClass._Facture.BonDeLivraison.Livraison.Fournisseur.Nom = (string)mDataReader["FournisseurNom"];
                    //if (!DBNull.Value.Equals(mDataReader["TransporteurID"])) mClass._Facture.BonDeLivraison.Livraison.Transporteur.ID = (int)mDataReader["TransporteurID"];
                    //if (!DBNull.Value.Equals(mDataReader["TransporteurNom"])) mClass._Facture.BonDeLivraison.Livraison.Transporteur.Nom = (string)mDataReader["TransporteurNom"];

                    if (!DBNull.Value.Equals(mDataReader["Tonnage"])) mClass._Tonnage = (decimal)mDataReader["Tonnage"];
                    if (!DBNull.Value.Equals(mDataReader["ObjetID"])) mClass._ElementID = (Guid)mDataReader["ObjetID"];
                    if (!DBNull.Value.Equals(mDataReader["ObjetRef"])) mClass._ElementRef = (string)mDataReader["ObjetRef"];
                    if (!DBNull.Value.Equals(mDataReader["Libelle"])) mClass._Libelle = (string)mDataReader["Libelle"];                    
                    if (!DBNull.Value.Equals(mDataReader["Taux"])) mClass._Taux = (decimal)mDataReader["Taux"];
                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._Montant = (decimal)mDataReader["Montant"];

                    mClass._FactureTransport = new FactureTransport();
                    if (!DBNull.Value.Equals(mDataReader["MontantDeductionTransport"])) mClass._FactureTransport.Deduction = (decimal)mDataReader["MontantDeductionTransport"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nFactureDeduction:MapFromDataReader");
            }
        }

        #endregion

    }

    public partial class FactureDeductionViewModel
    {
        public FactureDeduction _FactureDeduction { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
