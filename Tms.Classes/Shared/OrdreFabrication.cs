using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Components.Data;
using Tms.Classes.Shared;
using Tms.Classes.Shared.Sales;
using Tms.Classes.Business;


namespace Tms.Classes.Shared
{
    public class OrdreFabrication : DataPersist
    {

        #region Fields
        private Guid _ID;
        private Article _Article;
        private Guid _ArticleID;
        private string _ArticleNom;
        private int _Annee;
        private int _Semaine;
        private LigneProduction _LigneDeProduction;
        private int _LigneDeProductionID;
        private string _LigneDeProductionDesignation;
        private string _NumeroProduction;
        private string _ReferenceExterne;
        private Recolte _Recolte;
        private string _RecolteDesignation;
        private Client _Client;
        private int _NbrePaletteAProduire;

        private DateTime? _DateEffective; 
        private DateTime? _DateDebutProduction;
        private DateTime? _DateFinProduction;

        private Produit _Produit;
        private ProduitType _TypeDeProduit;

        private Conditionnement _Conditionnement;
        private ConditionnementReference _ConditionnementReference;

        private string _Statut;
        private bool _Desactive;
        private string _Commentaire;
        private object _RowVersionKey;


        #endregion

        #region Properties
        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get
            {
                return _ID;
            }

            set
            {
                _ID = value;
            }
        }

        public Article Article
        {
            get
            {
                return _Article;
            }

            set
            {
                _Article = value;
            }
        }

        public string ArticleAsString
        {
            get { return _Article != null ? _Article.Nom : string.Empty; }
        }

        public Guid ArticleID
        {
            get
            {
                return _ArticleID;
            }

            set
            {
                _ArticleID = value;
            }
        }
        public string ArticleNom
        {
            get
            {
                return _ArticleNom;
            }

            set
            {
                _ArticleNom = value;
            }
        }




        public int Semaine
        {
            get
            {
                return _Semaine;
            }

            set
            {
                _Semaine = value;
            }
        }


        public int Annee
        {
            get
            {
                return _Annee;
            }

            set
            {
                _Annee = value;
            }
        }

        public LigneProduction LigneDeProduction
        {
            get
            {
                return _LigneDeProduction;
            }

            set
            {
                _LigneDeProduction = value;
            }
        }

        public string LigneDeProductionDesignation
        {
            get { return _LigneDeProduction != null ? _LigneDeProduction.Designation : string.Empty; }
        }


        public string NumeroProduction
        {
            get
            {
                return _NumeroProduction;
            }

            set
            {
                _NumeroProduction = value;
            }
        }

        

        public string ReferenceExterne
        {
            get
            {
                return _ReferenceExterne;
            }

            set
            {
                _ReferenceExterne = value;
            }
        }

        public Recolte Recolte
        {
            get
            {
                return _Recolte;
            }

            set
            {
                _Recolte = value;
            }
        }

        public string RecolteAsString
        {
            get { return _Recolte != null ? _Recolte.Designation : string.Empty; }
        }


        public Client Client
        {
            get
            {
                return _Client;
            }

            set
            {
                _Client = value;
            }
        }

        public string ClientAsString
        {
            get { return _Client != null ? _Client.Nom : string.Empty; }
        }

        public int NbrePaletteAProduire
        {
            get
            {
                return _NbrePaletteAProduire;
            }

            set
            {
                _NbrePaletteAProduire = value;
            }
        }

        public DateTime? DateEffective
        {
            get
            {
                return _DateEffective;
            }

            set
            {
                _DateEffective = value;
            }
        }
        public string DateEffectiveAstring
        {
            get
            {
                return _DateEffective != null ? _DateEffective.Value.ToShortDateString() : string.Empty;
            }
        }

        public Produit Produit
        {
            get { return _Produit; }
            set { _Produit = value; }
        }

        public string ProduitAsString
        {
            get
            {
                return _Produit != null ? _Produit.Designation : string.Empty;
            }
        }

        public ProduitType TypeDeProduit
        {
            get { return _TypeDeProduit; }
            set { _TypeDeProduit = value; }
        }

        public string TypeDeProduitAsString
        {
            get
            {
                return _TypeDeProduit != null ? _TypeDeProduit.Designation : string.Empty;
            }
        }

        public Conditionnement Conditionnement
        {
            get { return _Conditionnement; }
            set { _Conditionnement = value; }
        }

        public ConditionnementReference ConditionnementReference
        {
            get { return _ConditionnementReference; }
            set { _ConditionnementReference = value; }
        }

        public string ConditionnementAsString
        {
            get
            {
                return _Conditionnement != null ? _Conditionnement.Designation : string.Empty;
            }
        }


        public DateTime? DateDebutProduction
        {
            get
            {
                return _DateDebutProduction;
            }

            set
            {
                _DateDebutProduction = value;
            }
        }

        public string DateDebutProductionAsString
        {
            get
            {
                return _DateDebutProduction != null ? _DateDebutProduction.Value.ToShortDateString() : string.Empty;
            }
        }

        public DateTime? DateFinProduction
        {
            get
            {
                return _DateFinProduction;
            }

            set
            {
                _DateFinProduction = value;
            }
        }

        public string DateFinProductionAsString
        {
            get
            {
                return _DateFinProduction != null ? _DateFinProduction.Value.ToShortDateString() : string.Empty;
            }
        }

        public string Statut
        {
            get
            {
                return _Statut;
            }

            set
            {
                _Statut = value;
            }
        }

        public bool Desactive
        {
            get
            {
                return _Desactive;
            }

            set
            {
                _Desactive = value;
            }
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

        public string Commentaire
        {
            get
            {
                return _Commentaire;
            }

            set
            {
                _Commentaire = value;
            }
        }

        public new object RowVersionKey
        {
            get { return _RowVersionKey; }
            set { _RowVersionKey = value; }
        }



        #endregion



        #region Methods
        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("pp_OrdreFabrication_Get", (Guid)Id);
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

        public bool fnGetDefaultOrdreFabrication()
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("OrdreFabrication_GetDefault");
                if (mDataReader.Read())
                {
                    MapFromDataReader(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetDefaultOrdreFabrication");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public bool fnGetByOrdreFabricationByUserName(object userName)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("OrdreFabrication_GetByUserName", (string)userName);
                if (mDataReader.Read())
                {
                    //MapFromDataReade2(this, mDataReader);
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
            return fnSelect(-1);
        }

        public List<DataPersist> fnSelect(int mStatus)
        {
            return fnSelect(-1, -1, -1, -1, null, "Tous", mStatus);
        }

        public List<DataPersist> fnSelect(int annee, int semaine, int produitID, int typeProduitID, Guid? articleID, string statut, int actifState)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("pp_OrdreFabrication_Select");
                db().AddInParameter(mCommande, "@Annee", SqlDbType.Int, annee);
                db().AddInParameter(mCommande, "@Semaine", SqlDbType.Int, semaine);
                db().AddInParameter(mCommande, "@ProduitID", SqlDbType.Int, produitID);
                db().AddInParameter(mCommande, "@TypeProduitID", SqlDbType.Int, typeProduitID);
                db().AddInParameter(mCommande, "@ArticleID", SqlDbType.UniqueIdentifier, articleID);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.VarChar, statut);
                db().AddInParameter(mCommande, "@ActifState", SqlDbType.Int, actifState);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    OrdreFabrication mClass = new OrdreFabrication();

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

                    mCommande = db().CreateStoredProcCommand("pp_OrdreFabrication_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@Creation_User", SqlDbType.VarChar, _UtilisateurCreation);
                    db().AddInParameter(mCommande, "@Creation_date", SqlDbType.DateTime, DateTime.Now);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("pp_OrdreFabrication_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@Modification_Date", SqlDbType.DateTime, DateTime.Now);
                    db().AddInParameter(mCommande, "@Modification_User", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@NumProduction", SqlDbType.VarChar, _NumeroProduction);
                db().AddInParameter(mCommande, "@Article", SqlDbType.UniqueIdentifier, _Article.ID);
                db().AddInParameter(mCommande, "@Annee", SqlDbType.Int, _Annee);
                db().AddInParameter(mCommande, "@Semaine", SqlDbType.Int, _Semaine);
                db().AddInParameter(mCommande, "@LigneDeProduction", SqlDbType.Int, _LigneDeProduction.ID);
                db().AddInParameter(mCommande, "@ReferenceExterne", SqlDbType.VarChar, _ReferenceExterne);
                db().AddInParameter(mCommande, "@CodeRecolte", SqlDbType.Int, _Recolte.ID);
                db().AddInParameter(mCommande, "@CodeClient", SqlDbType.Int, _Client.ID);
                db().AddInParameter(mCommande, "@NbrePaletteAProduire", SqlDbType.Int, _NbrePaletteAProduire);
                db().AddInParameter(mCommande, "@DatePrevue", SqlDbType.DateTime, _DateEffective);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, _DateDebutProduction);
                db().AddInParameter(mCommande, "@DateFin", SqlDbType.DateTime, _DateFinProduction);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.VarChar, "EA");


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
                        _NumeroProduction = (string)db().Parameters(mCommande, "@NumProduction");
                        

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
                throw new Exception(ex.Message + "\r\n" + "OrdreFabrication:fnUpdate");

            }
            return Result;
        }

        public List<DataPersist> fnSelectToPrice(Guid priceID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_PrixJournalier_SelectOrdreFabrications");

                db().AddInParameter(mCommande, "@priceID", SqlDbType.UniqueIdentifier, priceID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    OrdreFabrication mClass = new OrdreFabrication();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectToPrice");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("pp_OrdreFabrication_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "OrdreFabrication:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("pp_OrdreFabrication_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "OrdreFabrication:fnDeActivate");
            }
            return Result;
        }

        public bool fnClose()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("pp_OrdreProduction_Close");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@DateFinProduction", SqlDbType.DateTime, _DateFinProduction);
                db().AddInParameter(mCommande, "@ClotureUtilisateur", SqlDbType.VarChar,_UtilisateurCreation);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);

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
                        _Statut = "CL";
                        break;
                    default:
                        //Unkown error
                        Result = false;
                        string ErrorMessage = (string)db().Parameters(mCommande, "@ErrorMessage");
                        throw new Exception(ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + "\r\n" + "OrdreProduction:fnClose");

            }
            return Result;
        }

        public bool fnOpen()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("pp_OrdreProduction_Open");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@OuvertureUtilisateur", SqlDbType.VarChar, _UtilisateurCreation);
                db().AddInParameter(mCommande, "@DateDebutProduction", SqlDbType.DateTime,_DateDebutProduction);
                db().AddInParameter(mCommande, "@DateFinProduction", SqlDbType.DateTime, _DateFinProduction);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);

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
                        _Statut = "OP";
                        break;
                    default:
                        //Unkown error
                        Result = false;
                        string ErrorMessage = (string)db().Parameters(mCommande, "@ErrorMessage");
                        throw new Exception(ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + "\r\n" + "OrdreProduction:fnOpen");

            }
            return Result;
        }


        public bool fnGetByUserName(object userName)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("OrdreFabrication_GetByUserName", (string)userName);
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

        public List<DataPersist> fnSelectAvailableForFS(int userOrdreFabrication, int FournisseurID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_OrdreFabrication_SelectAvailableForSupplier");
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@userOrdreFabrication", SqlDbType.Int, userOrdreFabrication);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    OrdreFabrication mClass = new OrdreFabrication();

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

        public List<DataPersist> fnSelectAvailableForAnalyseur(int analyseurID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_AnalyseurOrdreFabrication_OrdreFabricationsAvailables");
                //db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, fournisseurID);
                db().AddInParameter(mCommande, "@AnalyseurID", SqlDbType.Int, analyseurID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    OrdreFabrication mClass = new OrdreFabrication();

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


        public List<DataPersist> fnSelectAvailableForPrice(string userName)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_OrdreFabrication_SelectAvailableForPrice");

                db().AddInParameter(mCommande, "@userName", SqlDbType.VarChar, 100, userName);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    OrdreFabrication mClass = new OrdreFabrication();

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
        #endregion

        #region "Private Members"


        //public override string ToString()
        //{
        //    return _Designation;
        //}

        //private static void MapFromDataReader(OrdreFabrication mClass, IDataReader mDataReader)
        //{
        //    try
        //    {
        //        if (mDataReader != null)
        //        {
        //            mClass.IsNew = false;

        //            if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (int)mDataReader["ID"];
        //            if (!DBNull.Value.Equals(mDataReader["Designation"])) mClass._Designation = (string)mDataReader["Designation"];



        //            mClass._OrdreFabrication = new OrdreFabrication();

        //            if (!DBNull.Value.Equals(mDataReader["CodeOrdreFabrication"])) mClass._OrdreFabricationID = (int)mDataReader["CodeOrdreFabrication"];
        //            if (!DBNull.Value.Equals(mDataReader["OrdreFabricationDesignation"])) mClass._OrdreFabricationDesignation =  (string)mDataReader["OrdreFabricationDesignation"];

        //            if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];

        //            if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
        //            if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
        //            if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
        //            if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
        //            if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "\nOrdreFabrication:MapFromDataReader");
        //    }
        //}

        //private static void MapFromDataReader(OrdreFabrication mClass, IDataReader rdr)
        //{
        //    try
        //    {
        //        if (rdr == null || rdr.IsClosed)
        //            return;

        //        mClass.IsNew = false;

        //        // ID et Designation du type
        //        if (!DBNull.Value.Equals(rdr["ID"])) mClass._ID = (Guid)rdr["ID"];
        //        if (!DBNull.Value.Equals(rdr["Designation"]))
        //            mClass._Designation = (string)rdr["Designation"];

        //        // Code et libellé du Produit Fini
        //        if (!DBNull.Value.Equals(rdr["CodeOrdreFabrication"]))
        //            mClass._OrdreFabricationID = (int)rdr["CodeOrdreFabrication"];
        //        if (!DBNull.Value.Equals(rdr["OrdreFabricationDesignation"]))
        //            mClass._OrdreFabricationDesignation = (string)rdr["OrdreFabricationDesignation"];

        //        // Injection dans l'objet OrdreFabrication
        //        mClass._OrdreFabrication = new OrdreFabrication
        //        {
        //            ID = mClass._OrdreFabricationID,
        //            Designation = mClass._OrdreFabricationDesignation
        //        };

        //        // Désactivation
        //        if (!DBNull.Value.Equals(rdr["Desactive"]))
        //            mClass._Desactive = (bool)rdr["Desactive"];

        //        // Audit
        //        if (!DBNull.Value.Equals(rdr["CreationUtilisateur"]))
        //            mClass.UtilisateurCreation = (string)rdr["CreationUtilisateur"];
        //        if (!DBNull.Value.Equals(rdr["CreationDate"]))
        //            mClass.DateCreation = (DateTime)rdr["CreationDate"];
        //        if (!DBNull.Value.Equals(rdr["ModificationUtilisateur"]))
        //            mClass.UtilisateurModification = (string)rdr["ModificationUtilisateur"];
        //        if (!DBNull.Value.Equals(rdr["ModificationDate"]))
        //            mClass.DateModification = (DateTime)rdr["ModificationDate"];
        //        if (!DBNull.Value.Equals(rdr["RowVersionKey"]))
        //            mClass.RowVersionKey = rdr["RowVersionKey"];
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "\nOrdreFabrication:MapFromDataReader");
        //    }
        //}

        private static void MapFromDataReader(OrdreFabrication mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["ArticleCode"]))
                    {
                        mClass._Article = new Article();
                        mClass._Article.ID = (Guid)mDataReader["ArticleCode"];
                        if (!DBNull.Value.Equals(mDataReader["ArticleDesignation"])) mClass._Article.Nom = (string)mDataReader["ArticleDesignation"];

                        mClass._ArticleID = (Guid)mDataReader["ArticleCode"];
                        if (!DBNull.Value.Equals(mDataReader["ArticleDesignation"])) mClass._ArticleNom = (string)mDataReader["ArticleDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["LigneProductionCode"]))
                    {
                        mClass._LigneDeProduction = new LigneProduction();
                        mClass._LigneDeProduction.ID = (int)mDataReader["LigneProductionCode"];
                        if (!DBNull.Value.Equals(mDataReader["LigneProductionDesignation"])) mClass._LigneDeProduction.Designation = (string)mDataReader["LigneProductionDesignation"];

                        mClass._LigneDeProductionID = (int)mDataReader["LigneProductionCode"];
                        if (!DBNull.Value.Equals(mDataReader["LigneProductionDesignation"])) mClass._LigneDeProductionDesignation = (string)mDataReader["LigneProductionDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["RecolteCode"]))
                    {
                        mClass._Recolte = new Recolte();
                        mClass._Recolte.ID = (int)mDataReader["RecolteCode"];
                        if (!DBNull.Value.Equals(mDataReader["RecolteDesignation"]))
                        {
                            mClass._Recolte.Designation = (string)mDataReader["RecolteDesignation"];
                            mClass._RecolteDesignation = (string)mDataReader["RecolteDesignation"];
                        }
                    }

                    if (!DBNull.Value.Equals(mDataReader["ClientCode"]))
                    {
                        mClass._Client = new Client();
                        mClass._Client.ID = (int)mDataReader["ClientCode"];
                        if (!DBNull.Value.Equals(mDataReader["ClientNom"])) mClass._Client.Nom = (string)mDataReader["ClientNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ProduitCode"]))
                    {
                        mClass._Produit = new Produit();
                        mClass._Produit.ID = (int)mDataReader["ProduitCode"];
                        if (!DBNull.Value.Equals(mDataReader["ProduitDesignation"])) mClass._Produit.Designation = (string)mDataReader["ProduitDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["TypeProduitCode"]))
                    {
                        mClass._TypeDeProduit = new ProduitType();
                        mClass._TypeDeProduit.ID = (int)mDataReader["TypeProduitCode"];
                        if (!DBNull.Value.Equals(mDataReader["TypeProduitDesignation"])) mClass._TypeDeProduit.Designation = (string)mDataReader["TypeProduitDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ConditionnementCode"]))
                    {
                        mClass._Conditionnement = new Conditionnement();
                        mClass._Conditionnement.ID = (int)mDataReader["ConditionnementCode"];
                        if (!DBNull.Value.Equals(mDataReader["ConditionnementDesignation"])) mClass._Conditionnement.Designation = (string)mDataReader["ConditionnementDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Annee"])) mClass._Annee = (int)mDataReader["Annee"];
                    if (!DBNull.Value.Equals(mDataReader["Semaine"])) mClass._Semaine = (int)mDataReader["Semaine"];
                    if (!DBNull.Value.Equals(mDataReader["NombrePaletteAProduire"])) mClass._NbrePaletteAProduire = (int)mDataReader["NombrePaletteAProduire"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroProduction"])) mClass._NumeroProduction = (string)mDataReader["NumeroProduction"];
                    if (!DBNull.Value.Equals(mDataReader["ReferenceExterne"])) mClass._ReferenceExterne = (string)mDataReader["ReferenceExterne"];

                    if (!DBNull.Value.Equals(mDataReader["DatePrevue"])) mClass._DateEffective = (DateTime)mDataReader["DatePrevue"];
                    if (!DBNull.Value.Equals(mDataReader["DateDebut"])) mClass._DateDebutProduction = (DateTime)mDataReader["DateDebut"];
                    if (!DBNull.Value.Equals(mDataReader["DateFin"])) mClass._DateFinProduction = (DateTime)mDataReader["DateFin"];

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
                throw new Exception(ex.Message + "\nOrdreFabrication:MapFromDataReader");
            }
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }


        //private static void MapFromDataReade2(OrdreFabrication mClass, IDataReader mDataReader)
        //{
        //    try
        //    {
        //        if (mDataReader != null)
        //        {
        //            mClass.IsNew = false;

        //            if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (int)mDataReader["ID"];
        //            if (!DBNull.Value.Equals(mDataReader["Designation"])) mClass._Designation = (string)mDataReader["Designation"];

        //            //mClass._DefautOrdreFabrication = new OrdreFabrication();
        //            //if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._DefautOrdreFabrication.ID = (int)mDataReader["ID"];
        //            //if (!DBNull.Value.Equals(mDataReader["Nom"])) mClass._DefautOrdreFabrication.Nom = (string)mDataReader["Nom"];

        //            mClass._OrdreFabrication = new OrdreFabrication();
        //            if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._OrdreFabrication.ID = (int)mDataReader["ID"];
        //            if (!DBNull.Value.Equals(mDataReader["Designation"])) mClass._OrdreFabrication.Designation = (string)mDataReader["Designation"];

        //            //mClass._Provenance = new Provenance();
        //            //if (!DBNull.Value.Equals(mDataReader["ProvenanceID"])) mClass._Provenance.ID = (int)mDataReader["ProvenanceID"];
        //            //if (!DBNull.Value.Equals(mDataReader["ProvenanceNom"])) mClass._Provenance.Nom = (string)mDataReader["ProvenanceNom"];

        //            if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];

        //            //if (!DBNull.Value.Equals(mDataReader["MagasinID"])) mClass._MagasinID = (int)mDataReader["MagasinID"];
        //            //if (!DBNull.Value.Equals(mDataReader["MagasinNom"])) mClass._MagasinNom = (string)mDataReader["MagasinNom"];

        //            if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
        //            if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
        //            if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
        //            if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
        //            if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "\nOrdreFabrication:MapFromDataReader");
        //    }
        //}
        #endregion


        //public string AsString
        //{
        //    get { return _Designation; }
        //}

        //public int MagasinID
        //{
        //    get
        //    {
        //        return _MagasinID;
        //    }

        //    set
        //    {
        //        _MagasinID = value;
        //    }
        //}

        //public string MagasinNom
        //{
        //    get
        //    {
        //        return _MagasinNom;
        //    }

        //    set
        //    {
        //        _MagasinNom = value;
        //    }
        //}
    }

    public partial class OrdreFabricationViewModel
    {
        public OrdreFabrication _OrdreFabrication { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
