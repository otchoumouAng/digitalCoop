using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Business.Sales;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;
using Tms.Classes.Shared.Sales;

namespace Tms.Classes.Business
{
    public class DemandeEtiquette : DataPersist
    {
        #region fields
        private Guid _ID;

        // ORDRE DE PRODUCTION
        private OrdreFabrication _OrdreFabrication;
        private Article _Article;
        private ProduitFini _ProduitFini;

        private ProduitType _ProduitType;
        private ConditionnementProduit _ConditionnementProduit;
        private DateTime _BestBeforeDate;

        private DateTime _DateEffectiveProduction;
        private int _NbreExemplaireA4;
        private string _Description;
        private int _NbreExemplaireA5;
        private string _Statut;
        private bool _IsApproved;
        private string _Approbateur;
        #endregion

        #region Properties
        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Article Article
        {
            get { return _Article; }
            set { _Article = value; }
        }


        // ORDRE DE PRODUCTION
        public OrdreFabrication OrdreFabrication
        {
            get { return _OrdreFabrication; }
            set { _OrdreFabrication = value; }
        }

        public ProduitType ProduitType
        {
            get { return _ProduitType; }
            set { _ProduitType = value; }
        }

        public DateTime BestBeforeDate
{
            get
            {
                return _BestBeforeDate; // valeur par défaut si Article ou Date non définis
            }
            set
            {
                _BestBeforeDate = value;
            }
        }

        public int? OrdreFabricationAnnee
        {
            get { return _OrdreFabrication?.Annee; }
        }

        public string OrdreFabricationAnneeAsString
        {
            get { return OrdreFabricationAnnee.ToString(); }
        }

        public int? OrdreFabricationSemaine
        {
            get { return _OrdreFabrication?.Semaine; }
        }

        public string OrdreFabricationSemaineAsString
        {
            get { return OrdreFabricationSemaine.ToString(); }

        }

        public string OrdreFabricationNumero
        {
            get { return OrdreFabrication?.NumeroProduction; }
        }


        public string OrdreFabricationNbrePaletteAsString
        {
            get { return OrdreFabrication?.NbrePaletteAProduire.ToString(); }
        }

        // ARTICLE
        public string ArticleCodeInterne
        {
            get { return Article?.Code; }

            set { Article.Code = value; }
        }

        public string ArticleNom
        {
            get { return Article?.Nom; }
            set { Article.Nom = value; }
        }


        // PRODUIT
        public ProduitFini ProduitFini
        {
            get { return _ProduitFini; }
            set { _ProduitFini = value; }
        }
        public string ProduitDesignation
        {
            get { return ProduitFini?.Designation; }

            set { ProduitFini.Designation = value; }
        }

        public string TypeProduitDesignation
        {
            get { return ProduitType?.Designation; }
            set { ProduitType.Designation = value; }
        }

        // ConditionnementProduit
        public ConditionnementProduit ConditionnementProduit
        {
            get { return _ConditionnementProduit; }
            set { _ConditionnementProduit = value; }
        }
        public string ConditionnementProduitDesignation
        {
            get { return ConditionnementProduit?.Designation; }
            set { ConditionnementProduit.Designation = value; }
        }



        public DateTime DateEffectiveProduction
        {
            get { return _DateEffectiveProduction; }
            set { _DateEffectiveProduction = value; }
        }

        public int NbreExemplaireA4
        {
            get { return _NbreExemplaireA4; }
            set { _NbreExemplaireA4 = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public int NbreExemplaireA5
        {
            get { return _NbreExemplaireA5; }
            set { _NbreExemplaireA5 = value; }
        }


        public string Statut
        {
            get { return _Statut; }
            set { _Statut = value; }
        }

        public string Approbateur
        {
            get { return _Approbateur; }
            set { _Approbateur = value; }
        }


        [Column(Text = "IsApproved")]
        public bool IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }

        public int mIcon
        {
            get
            {
               if (_IsApproved || _Statut == "AP")
                    return 1; // Tick                
                else
                    return 2; //                     
            }
        }

        #endregion

        #region Constructor
        public DemandeEtiquette()
        {

        }

        public DemandeEtiquette(Guid myId)
        {
            this.fnGet(myId);
        }
        #endregion

        #region Methods



        public override bool fnActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("PP_DemandeEtiquette_Activate");
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
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        break;
                    default:
                        bolResult = false;
                        string ErrorMessage = (string)db().Parameters(mCommande, "@ErrorMessage");
                        throw new Exception(ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                bolResult = false;
                throw new Exception(ex.Message + "\r\n" + "DemandeEtiquette:fnDeActivate");
            }
            return bolResult;
        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("PP_DemandeEtiquette_DeActivate");
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
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        break;
                    default:
                        bolResult = false;
                        string ErrorMessage = (string)db().Parameters(mCommande, "@ErrorMessage");
                        throw new Exception(ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                bolResult = false;
                throw new Exception(ex.Message + "\r\n" + "DemandeEtiquette:fnDeActivate");
            }
            return bolResult;
        }

        public bool fnApprove()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("PP_DemandeEtiquette_Approve");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);

            db().AddInParameter(mCommande, "@DateEffectiveProduction", SqlDbType.DateTime, _DateEffectiveProduction);
            db().AddInParameter(mCommande, "@BestBeforeDate", SqlDbType.DateTime, _BestBeforeDate); 
            db().AddInParameter(mCommande, "@NbreExemplaireA4", SqlDbType.Int, _NbreExemplaireA4);
            db().AddInParameter(mCommande, "@NbreExemplaireA5", SqlDbType.Int, _NbreExemplaireA5);

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
                    throw new Exception(ex.Message + "\r\n" + "DemandeEtiquette:fnApprove");
                }
                return Result;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("PP_DemandeEtiquette_Get", (Guid)Id);
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
            return fnSelect("-1", "-1", -1, -1, "-1");
        }

        public List<DataPersist> fnSelect(string mAnnee, string mSemaine, int mProduitID, int mProduitTypeID, string mStatut)

        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("PP_DemandeEtiquette_Select");
                db().AddInParameter(mCommande, "@Annee", SqlDbType.VarChar, mAnnee);
                db().AddInParameter(mCommande, "@Semaine", SqlDbType.VarChar, mSemaine);
                db().AddInParameter(mCommande, "@ProduitID", SqlDbType.Int, mProduitID);
                db().AddInParameter(mCommande, "@ProduitTypeID", SqlDbType.Int, mProduitTypeID);

             

                // Maintenant, tu ajoutes le paramètre à ta commande
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, mStatut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    DemandeEtiquette mClass = new DemandeEtiquette();
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
                    mCommande = db().CreateStoredProcCommand("PP_DemandeEtiquette_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUtilisateur", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("PP_DemandeEtiquette_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUtilisateur", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@OrdreFabricationID", SqlDbType.UniqueIdentifier, _OrdreFabrication.ID);
                db().AddInParameter(mCommande, "@DateEffectiveProduction", SqlDbType.DateTime, _DateEffectiveProduction);

                db().AddInParameter(mCommande, "@Description", SqlDbType.VarChar, _Description);
                db().AddInParameter(mCommande, "@BestBeforeDate", SqlDbType.DateTime, _BestBeforeDate);
                db().AddInParameter(mCommande, "@NbreExemplaireA4", SqlDbType.Int, _NbreExemplaireA4);
                db().AddInParameter(mCommande, "@NbreExemplaireA5", SqlDbType.Int, _NbreExemplaireA5);

                //db().AddInParameter(mCommande, "@Statut", SqlDbType.VarChar, _Statut);
                //db().AddInParameter(mCommande, "@Desactive", SqlDbType.Bit, _Desactive);
                //db().AddInParameter(mCommande, "@IsApproved", SqlDbType.Bit, _IsApproved);


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
                }
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + "\r\n" + "DemandeEtiquette:fnUpdate");

            }
            return Result;
        }


        public override string ToString()
        {
            return _NbreExemplaireA5.ToString();
        }


        private static void MapFromDataReader(DemandeEtiquette mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];


                    mClass._OrdreFabrication = new OrdreFabrication();
                    if (!DBNull.Value.Equals(mDataReader["OrdreFabricationID"]))mClass._OrdreFabrication.ID = (Guid)mDataReader["OrdreFabricationID"];
                    if (!DBNull.Value.Equals(mDataReader["OrdreFabricationAnnee"])) mClass._OrdreFabrication.Annee = Convert.ToInt32(mDataReader["OrdreFabricationAnnee"]);
                    if (!DBNull.Value.Equals(mDataReader["OrdreFabricationSemaine"])) mClass._OrdreFabrication.Semaine = Convert.ToInt32(mDataReader["OrdreFabricationSemaine"]);
                    if (!DBNull.Value.Equals(mDataReader["OrdreFabricationNumero"])) mClass._OrdreFabrication.NumeroProduction = (string)mDataReader["OrdreFabricationNumero"];
                    if (!DBNull.Value.Equals(mDataReader["OrdreFabricationNbrePalette"])) mClass._OrdreFabrication.NbrePaletteAProduire = Convert.ToInt32(mDataReader["OrdreFabricationNbrePalette"]);

                    mClass._Article = new Article();
                    if (!DBNull.Value.Equals(mDataReader["BestBeforeDate"])) mClass._Article.BBDate = (DateTime)mDataReader["BestBeforeDate"];
                    if (!DBNull.Value.Equals(mDataReader["ArticleCodeInterne"])) mClass._Article.Code = (string)mDataReader["ArticleCodeInterne"];
                    if (!DBNull.Value.Equals(mDataReader["ArticleNom"])) mClass._Article.Nom = (string)mDataReader["ArticleNom"];
                    if (!DBNull.Value.Equals(mDataReader["ArticleNbreUniteParPalette"])) mClass._Article.NbreUniteParPalette = (int)mDataReader["ArticleNbreUniteParPalette"];

                    mClass._ProduitFini = new ProduitFini();
                    if (!DBNull.Value.Equals(mDataReader["ProduitDesignation"])) mClass._ProduitFini.Designation = (string)mDataReader["ProduitDesignation"];

                    mClass._ProduitType = new ProduitType();
                    if (!DBNull.Value.Equals(mDataReader["TypeProduitDesignation"])) mClass._ProduitType.Designation = (string)mDataReader["TypeProduitDesignation"];

                    mClass._ConditionnementProduit = new ConditionnementProduit();
                    if (!DBNull.Value.Equals(mDataReader["ConditionnementProduitDesignation"])) mClass._ConditionnementProduit.Designation = (string)mDataReader["ConditionnementProduitDesignation"];

                    if (!DBNull.Value.Equals(mDataReader["BestBeforeDate"])) mClass._BestBeforeDate = (DateTime)mDataReader["BestBeforeDate"];

                    if (!DBNull.Value.Equals(mDataReader["DateEffectiveProduction"])) mClass._DateEffectiveProduction = (DateTime)mDataReader["DateEffectiveProduction"];
                    if (!DBNull.Value.Equals(mDataReader["NbreExemplaireA4"])) mClass._NbreExemplaireA4 = (int)mDataReader["NbreExemplaireA4"];
                    if (!DBNull.Value.Equals(mDataReader["Description"])) mClass._Description = (string)mDataReader["Description"];
                    if (!DBNull.Value.Equals(mDataReader["NbreExemplaireA5"])) mClass._NbreExemplaireA5 = (int)mDataReader["NbreExemplaireA5"];

                    //if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["IsApproved"])) mClass._IsApproved = (bool)mDataReader["IsApproved"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];
                    //if (!DBNull.Value.Equals(mDataReader["EstFictif"])) mClass._EstFictif = (bool)mDataReader["EstFictif"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nDemandeEtiquette:MapFromDataReader");
            }
        }


        #endregion
    }

    public partial class DemandeEtiquetteViewModel
    {
        public DemandeEtiquette _DemandeEtiquette { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
