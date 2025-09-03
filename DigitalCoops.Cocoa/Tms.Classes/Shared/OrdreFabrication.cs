using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Components.Data;
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

        private DateTime? _DateProduction; //Date prévue
        private DateTime? _DateDebutProduction;
        private DateTime? _DateFinProduction;

        private string _Statut;
        private bool _Desactive;
        private string _Commentaire;
        private object _RowVersionKey;

        private ProduitFini _ProduitFini;
        private ProduitType _ProduitType;
        private ConditionnementProduit _ConditionnementProduit;






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



        public ConditionnementProduit ConditionnementProduit
        {
            get { return _ConditionnementProduit; }
            set { _ConditionnementProduit = value; }
        }
        public ProduitFini ProduitFini
        {
            get { return _ProduitFini; }
            set { _ProduitFini = value; }
        }
        public ProduitType ProduitType
        {
            get { return _ProduitType; }
            set { _ProduitType = value; }
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
        public int LigneDeProductionID
        {
            get
            {
                return _LigneDeProductionID;
            }

            set
            {
                _LigneDeProductionID = value;
            }
        }
        public string LigneDeProductionDesignation
        {
            get
            {
                return _LigneDeProductionDesignation;
            }

            set
            {
                _LigneDeProductionDesignation = value;
            }
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

        public string RecolteDesignation
        {
            get
            {
                return _RecolteDesignation;
            }

            set
            {
                _RecolteDesignation = value;
            }
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

        public DateTime? DateProduction
        {
            get
            {
                return _DateProduction;
            }

            set
            {
                _DateProduction = value;
            }
        }
        public string DateProductionAstring
        {
            get
            {
                return _DateProduction != null ? _DateProduction.Value.ToShortDateString() : string.Empty;
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
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("pp_OrdreFabrication_Select");
                db().AddInParameter(mCommande, "@Statut", SqlDbType.SmallInt, mStatus);
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
                db().AddInParameter(mCommande, "@DatePrevue", SqlDbType.DateTime, _DateProduction);
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
                db().AddInParameter(mCommande, "@ModificationUtilisateur", SqlDbType.VarChar, _UtilisateurModification);
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



        public bool fnSelectByNumber(object numero, DateTime DateEffectiveProduction)
        {
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommand = db().CreateStoredProcCommand("pp_OrdreFabrication_GetByNumero");

                db().AddInParameter(mCommand, "@Numero", SqlDbType.VarChar, 50, (string)numero);
                db().AddInParameter(mCommand, "@DateEffectiveProduction", SqlDbType.DateTime, 50, (DateTime)DateEffectiveProduction);
                db().AddParameter(mCommand, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                db().AddOutParameter(mCommand, "@ErrorMessage", SqlDbType.VarChar, 1000);
                mDataReader = db().ExecuteReader(mCommand);

                if ((db().Parameters(mCommand, "ReturnValue") != null) && ((int)db().Parameters(mCommand, "ReturnValue") == -1))
                {
                    string ErrorMessage = (string)db().Parameters(mCommand, "@ErrorMessage");
                    throw new Exception(ErrorMessage);
                }

                if (mDataReader.Read())
                {
                    MapFromDataReaderByNumero(this, mDataReader);
                }
                return true;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnSelectByNumber");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }


         public List<DataPersist> fnSelectByOFOpenAndDemandeEtiquette(int mAnnee, int mSemaine)
        {

            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommand = db().CreateStoredProcCommand("pp_OrdreFabrication_GetByOFOpenAndDemandeEtiquette");

                db().AddInParameter(mCommand, "@Annee", SqlDbType.Int, mAnnee);
                db().AddInParameter(mCommand, "@Semaine", SqlDbType.Int, mSemaine);
                db().AddParameter(mCommand, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                db().AddOutParameter(mCommand, "@ErrorMessage", SqlDbType.VarChar, 1000);
                mDataReader = db().ExecuteReader(mCommand);

                if ((db().Parameters(mCommand, "ReturnValue") != null) && ((int)db().Parameters(mCommand, "ReturnValue") == -1))
                {
                    string ErrorMessage = (string)db().Parameters(mCommand, "@ErrorMessage");
                    throw new Exception(ErrorMessage);
                }

               while (mDataReader.Read())
                {
                    OrdreFabrication mClass = new OrdreFabrication();
                    MapFromDataReadeOFOpenAndDemandeEtiquetter(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":pp_OrdreFabrication_GetByOFOpenAndDemandeEtiquette");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }


        public List<DataPersist> fnSelectOrdreFabrication_ByYearAndWeeks(int mAnnee, int mSemaine)
        {

            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommand = db().CreateStoredProcCommand("pp_OrdreFabrication_ByYearAndWeeks");

                db().AddInParameter(mCommand, "@Annee", SqlDbType.Int, mAnnee);
                db().AddInParameter(mCommand, "@Semaine", SqlDbType.Int, mSemaine);
                db().AddParameter(mCommand, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                db().AddOutParameter(mCommand, "@ErrorMessage", SqlDbType.VarChar, 1000);
                mDataReader = db().ExecuteReader(mCommand);

                if ((db().Parameters(mCommand, "ReturnValue") != null) && ((int)db().Parameters(mCommand, "ReturnValue") == -1))
                {
                    string ErrorMessage = (string)db().Parameters(mCommand, "@ErrorMessage");
                    throw new Exception(ErrorMessage);
                }

               while (mDataReader.Read())
                {
                    OrdreFabrication mClass = new OrdreFabrication();
                    MapFromDataReadeOFOpenAndDemandeEtiquetter(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":pp_OrdreFabrication_GetByOFOpenAndDemandeEtiquette");
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

        //        // Code et libellé du ProduitFini Fini
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


        private static void MapFromDataReadeOFOpenAndDemandeEtiquetter(OrdreFabrication mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroProduction"])) mClass._NumeroProduction = (string)mDataReader["NumeroProduction"];
                }
            }
            catch (System.Exception ex)
            {

                throw new Exception(ex.Message + "\nOrdreFabrication:MapFromDataReader"); ;
            }
        }

        private static void MapFromDataReaderByNumero(OrdreFabrication mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["NbrePaletteAProduire"])) mClass._NbrePaletteAProduire = (int)mDataReader["NbrePaletteAProduire"];

                    mClass._ProduitFini = new ProduitFini();
                    if (!DBNull.Value.Equals(mDataReader["ProduitFiniDesignation"])) mClass._ProduitFini.Designation = (string)mDataReader["ProduitFiniDesignation"];

                    mClass._ProduitType = new ProduitType();
                    if (!DBNull.Value.Equals(mDataReader["ProduitTypeDesignation"])) mClass._ProduitType.Designation = (string)mDataReader["ProduitTypeDesignation"];

                    mClass._Article = new Article();
                    if (!DBNull.Value.Equals(mDataReader["ArticleID"])) mClass._Article.ID = (Guid)mDataReader["ArticleID"];
                    if (!DBNull.Value.Equals(mDataReader["ArticleNbreTiquetteParDefautA4"])) mClass._Article.NbreTiquetteParDefautA4 = (int)mDataReader["ArticleNbreTiquetteParDefautA4"];
                    if (!DBNull.Value.Equals(mDataReader["ArticleNbreTiquetteParDefautA5"])) mClass._Article.NbreTiquetteParDefautA5 = (int)mDataReader["ArticleNbreTiquetteParDefautA5"];
                    if (!DBNull.Value.Equals(mDataReader["ArticleBestBeforeDate"])) mClass._Article.BestBeforeDate = (int)mDataReader["ArticleBestBeforeDate"];
                    if (!DBNull.Value.Equals(mDataReader["BBDate"])) mClass._Article.BBDate = (DateTime)mDataReader["BBDate"];
                    if (!DBNull.Value.Equals(mDataReader["ArticleNom"])) mClass._Article.Nom = (string)mDataReader["ArticleNom"];
                    if (!DBNull.Value.Equals(mDataReader["ArticleCodeInterne"])) mClass._Article.Code = (string)mDataReader["ArticleCodeInterne"];
                    if (!DBNull.Value.Equals(mDataReader["ArticleNbreUniteParPalette"])) mClass._Article.NbreUniteParPalette = (int)mDataReader["ArticleNbreUniteParPalette"];

                    mClass._ConditionnementProduit = new ConditionnementProduit();
                    if (!DBNull.Value.Equals(mDataReader["ConditionnementProduitDesignation"])) mClass._ConditionnementProduit.Designation = (string)mDataReader["ConditionnementProduitDesignation"];

                }
            }
            catch (System.Exception ex)
            {

                throw new Exception(ex.Message + "\nOrdreFabrication:MapFromDataReader"); ;
            }
        }



        private static void MapFromDataReader(OrdreFabrication mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["ArticleID"]))
                    {
                        mClass._Article = new Article();
                        mClass._Article.ID = (Guid)mDataReader["ArticleID"];
                        mClass._Article.Nom = (string)mDataReader["ArticleNom"];

                        mClass._ArticleID = (Guid)mDataReader["ArticleID"];
                        mClass._ArticleNom = (string)mDataReader["ArticleNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["LigneProductionCode"]))
                    {
                        mClass._LigneDeProduction = new LigneProduction();
                        mClass._LigneDeProduction.ID = (int)mDataReader["LigneProductionCode"];
                        mClass._LigneDeProduction.Designation = (string)mDataReader["LigneProductionDesignation"];

                        mClass._LigneDeProductionID = (int)mDataReader["LigneProductionCode"];
                        mClass._LigneDeProductionDesignation = (string)mDataReader["LigneProductionDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["RecolteCode"]))
                    {
                        mClass._Recolte = new Recolte();
                        mClass._Recolte.ID = (int)mDataReader["RecolteCode"];
                        mClass._Recolte.Designation = (string)mDataReader["RecolteDesignation"];
                        mClass._RecolteDesignation = (string)mDataReader["RecolteDesignation"];
                    }

                    // if (!DBNull.Value.Equals(mDataReader["ClientCode"]))
                    // {
                    //     mClass._Client = new Client();
                    //     mClass._Client.ID = (int)mDataReader["ClientCode"];
                    //     mClass._Client.Nom = (string)mDataReader["ClientNom"];
                    // }

                    if (!DBNull.Value.Equals(mDataReader["Annee"])) mClass._Annee = (int)mDataReader["Annee"];
                    if (!DBNull.Value.Equals(mDataReader["Semaine"])) mClass._Semaine = (int)mDataReader["Semaine"];
                    if (!DBNull.Value.Equals(mDataReader["NombrePaletteAProduire"])) mClass._NbrePaletteAProduire = (int)mDataReader["NombrePaletteAProduire"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroProduction"])) mClass._NumeroProduction = (string)mDataReader["NumeroProduction"];
                    if (!DBNull.Value.Equals(mDataReader["ReferenceExterne"])) mClass._ReferenceExterne = (string)mDataReader["ReferenceExterne"];


                    if (!DBNull.Value.Equals(mDataReader["DatePrevue"])) mClass._DateProduction = (DateTime)mDataReader["DatePrevue"];
                    if (!DBNull.Value.Equals(mDataReader["DatePrevue"])) mClass._DateDebutProduction = (DateTime)mDataReader["DateDebut"];
                    if (!DBNull.Value.Equals(mDataReader["DateFin"])) mClass._DateFinProduction = (DateTime)mDataReader["DateFin"];
                    //if (!DBNull.Value.Equals(mDataReader["NumeroProduction"])) mClass._NumeroProduction = (string)mDataReader["NumeroProduction"];
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
