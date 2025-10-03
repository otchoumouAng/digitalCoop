using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms.Classes.Business
{
    //[Proxy(Read ="~/DeplacementPalette/Select")]
    //[JsonReader(RootProperty = "data")]
    public class DeplacementPalette : DataPersist
    {
        #region "Fields"

        private Guid _ID;
        private Palette _Palette;
        private Magasin _MagasinSource;
        private DateTime _DateDepart;
        private Magasin _MagasinDestination;
        private OrdreFabrication _OrdreFabrication;
        private string _Produit;
        private string _TypeDeProduit;
        private string _EmplacementDestination;
        private DateTime _DateArrivee;
        private string _Description;
        private string _Operateur;
        private string _ModeDeTransfert;
        private string _Statut;
        private bool _Desactive;
        #endregion

        #region "Properties"

        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Palette Palette
        {
            get { return _Palette; }
            set { _Palette = value; }
        }

        public int PaletteAsInt
        {
            get { return _Palette != null ? _Palette.Numero : 0; }

        }

        public Magasin MagasinSource
        {
            get { return _MagasinSource; }
            set { _MagasinSource = value; }
        }

        public string MagasinSourceAsString
        {
            get { return _MagasinSource != null ? _MagasinSource.Designation : string.Empty; }

        }

        public DateTime DateDepart
        {
            get { return _DateDepart; }
            set { _DateDepart = value; }
        }

        public Magasin MagasinDestination
        {
            get { return _MagasinDestination; }
            set { _MagasinDestination = value; }
        }

        public string MagasinDestinationAsString
        {
            get { return _MagasinDestination != null ? _MagasinDestination.Designation : string.Empty; }

        }

        public OrdreFabrication OrdreFabrication
        {
            get
            {
                return _OrdreFabrication;
            }

            set
            {
                _OrdreFabrication = value;
            }
        }

        public string OrdreFabricationAsString
        {
            get { return _OrdreFabrication != null ? _OrdreFabrication.NumeroProduction : string.Empty; }

        }

        public string Produit
        {
            get { return _Produit; }
            set { _Produit = value; }
        }


        public string TypeDeProduit
        {
            get { return _TypeDeProduit; }
            set { _TypeDeProduit = value; }
        }


        public string EmplacementDestination
        {
            get { return _EmplacementDestination; }
            set { _EmplacementDestination = value; }
        }

        public DateTime DateArrivee
        {
            get { return _DateArrivee; }
            set { _DateArrivee = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public string Operateur
        {
            get { return _Operateur; }
            set { _Operateur = value; }
        }

        public string ModeDeTransfert
        {
            get { return _ModeDeTransfert; }
            set { _ModeDeTransfert = value; }
        }

        public string Statut
        {
            get { return _Statut; }
            set { _Statut = value; }
        }

        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
        }

        #endregion

        #region "Constructor"

        public DeplacementPalette()
        {

        }

        public DeplacementPalette(Guid myId)
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
                mDataReader = db().ExecuteReader("DeplacementPalette_Get", (Guid)Id);
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
            //return fnSelect("{Tous}", -1, -1, null, null, "-1", -1, "NO");
            return fnSelect("-1");
        }

        public List<DataPersist> fnSelect(string statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("pp_transfert_select");
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    DeplacementPalette mClass = new DeplacementPalette();

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

        //public override bool fnUpdate()
        //{
        //    bool Result;
        //    DataCommand mCommande;
        //    try
        //    {
        //        if (this._isnew)
        //        {

        //            mCommande = db().CreateStoredProcCommand("DeplacementPalette_New");

        //            db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
        //            db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 10);
        //            db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
        //        }
        //        else
        //        {
        //            mCommande = db().CreateStoredProcCommand("DeplacementPalette_Modify");
        //            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
        //            db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
        //            if (!string.IsNullOrEmpty(_Statut))
        //                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, _Statut);
        //            else
        //                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, DBNull.Value);
        //        }

        //        db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, _Campagne.Designation);
        //        db().AddInParameter(mCommande, "@FournisseurID", SqlDbType.Int, _Fournisseur.ID);
        //        db().AddInParameter(mCommande, "@TypeID", SqlDbType.Int, _DeplacementPaletteType.ID);
        //        db().AddInParameter(mCommande, "@DateContrat", SqlDbType.DateTime, _DateContrat);
        //        db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, _DateDebut);
        //        db().AddInParameter(mCommande, "@DateEcheance", SqlDbType.DateTime, _DateEcheance);
        //        db().AddInParameter(mCommande, "@Tonnage", SqlDbType.Decimal, _Tonnage);
        //        db().AddInParameter(mCommande, "@prix", SqlDbType.Money, _Prix);
        //        db().AddInParameter(mCommande, "@Montant", SqlDbType.Decimal, _Montant);
        //        db().AddInParameter(mCommande, "@commentaire", SqlDbType.VarChar, _Commentaire);
        //        db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, _Sites.ID);

        //        db().AddInParameter(mCommande, "@mPrime", SqlDbType.Money, _MontantPrime);
        //        db().AddInParameter(mCommande, "@prixBrut", SqlDbType.Money, _PrixBrut);

        //        if (_financementID != null)
        //            db().AddInParameter(mCommande, "@FinancementID", SqlDbType.UniqueIdentifier, _financementID);
        //        else
        //            db().AddInParameter(mCommande, "@FinancementID", SqlDbType.UniqueIdentifier, DBNull.Value);
        //        //db().AddInParameter(mCommande, "@statut", SqlDbType.Char, _Statut);                

        //        db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

        //        if (!this._isnew)
        //        {
        //            db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
        //        }
        //        else
        //        {
        //            db().AddOutParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0);
        //        }

        //        db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
        //        db().ExecuteNonQuery(ref mCommande);
        //        switch ((int)db().Parameters(mCommande, "ReturnValue"))
        //        {
        //            case 0:
        //                //Everything OK
        //                base.UpdateAuditFields();
        //                Result = true;

        //                _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
        //                _ID = (Guid)db().Parameters(mCommande, "@ID");
        //                if (this.IsNew)
        //                {
        //                    _Numero = (string)db().Parameters(mCommande, "@Numero");
        //                    _Statut = "NA";
        //                }

        //                _isnew = false;
        //                break;
        //            default:
        //                //Unkown error
        //                Result = false;
        //                string ErrorMessage = (string)db().Parameters(mCommande, "@ErrorMessage");
        //                throw new Exception(ErrorMessage);
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Result = false;
        //        throw new Exception(ex.Message + "\r\n" + "DeplacementPalette:fnUpdate");

        //    }
        //    return Result;
        //}

        // Dans le fichier /Classes/Business/DeplacementPalette.cs

        public override bool fnUpdate()
        {
            bool Result;
            DataCommand mCommande;

            try
            {
                if (this._isnew)
                {
                    // Appel de la procédure de création
                    mCommande = db().CreateStoredProcCommand("pp_transfert_new");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, 255,_UtilisateurCreation);
                }
                else
                {
                    // Appel de la procédure de modification
                    mCommande = db().CreateStoredProcCommand("pp_transfert_modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, 255, _UtilisateurModification);
                    db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
                }

                // Paramètres communs pour la création et la modification
                db().AddInParameter(mCommande, "@PaletteId", SqlDbType.UniqueIdentifier, _Palette.ID);
                db().AddInParameter(mCommande, "@MagasinSourceId", SqlDbType.Int, _MagasinSource.ID);
                db().AddInParameter(mCommande, "@DateDeDepart", SqlDbType.DateTime, _DateDepart);
                db().AddInParameter(mCommande, "@MagasinDestinationId", SqlDbType.Int, _MagasinDestination.ID);
                db().AddInParameter(mCommande, "@EmplacementDestination", SqlDbType.VarChar, 255, _EmplacementDestination);

                if (_DateArrivee > DateTime.MinValue)
                {
                    db().AddInParameter(mCommande, "@DateArrivee", SqlDbType.DateTime, _DateArrivee);
                }
                else
                {
                    db().AddInParameter(mCommande, "@DateArrivee", SqlDbType.DateTime, DBNull.Value);
                }

                if (!string.IsNullOrEmpty(_Description))
                {
                    db().AddInParameter(mCommande, "@Description", SqlDbType.VarChar, 500, _Description);
                }
                else
                {
                    db().AddInParameter(mCommande, "@Description", SqlDbType.VarChar, 500, DBNull.Value);
                }

                db().AddInParameter(mCommande, "@Operateur", SqlDbType.VarChar, 255, _Operateur);
                db().AddInParameter(mCommande, "@ModeDeTransfert", SqlDbType.VarChar, 50, _ModeDeTransfert);

                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);

                if (!this._isnew)
                {
                    db().AddOutParameter(mCommande, "@NewRowVersion", SqlDbType.Timestamp, 0);
                }

                // Exécution
                db().ExecuteNonQuery(ref mCommande);

                // Traitement du code retour
                if ((int)db().Parameters(mCommande, "ReturnValue") != 0)
                {
                    string error = (string)db().Parameters(mCommande, "@ErrorMessage");
                    throw new Exception($"Erreur de la base de données : {error}");
                }

                // En cas de succès, mise à jour des champs
                base.UpdateAuditFields();
                if (this._isnew)
                {
                    _ID = (Guid)db().Parameters(mCommande, "@ID");
                    _Statut = "NA"; // Statut initial par défaut
                    this._isnew = false;
                }
                else
                {
                    _RowVersionKey = db().Parameters(mCommande, "@NewRowVersion");
                }

                Result = true;
            }
            catch (Exception ex)
            {
                throw new Exception($"DeplacementPalette:fnUpdate a échoué.\r\n{ex.Message}");
            }

            return Result;
        }


        public override bool fnActivate()
        {

            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("DeplacementPalette_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "DeplacementPalette:fnActivate");
            }
            return Result;

        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("DeplacementPalette_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "DeplacementPalette:fnDeActivate");
            }
            return bolResult;
        }

        public bool fnClose()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("DeplacementPalette_Close");
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
                        Desactive = false;
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
                throw new Exception(ex.Message + "\r\n" + "DeplacementPalette:fnClose");
            }
            return bolResult;
        }


        public bool fnDeActivate(DataTransaction mTran)
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("DeplacementPalette_DeActivate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
            db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
            db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
            db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
            try
            {
                db().ExecuteNonQuery(ref mCommande, mTran);
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
                throw new Exception(ex.Message + "\r\n" + "DeplacementPalette:fnDeActivate");
            }
            return bolResult;
        }

        public bool fnApprove()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("DeplacementPalette_Approve");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
            db().AddOutParameter(mCommande, "@statut", SqlDbType.Char, 2);
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
                        _Statut = "AP";
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
                throw new Exception(ex.Message + "\r\n" + "PrixNegocie:fnActivate");
            }
            return Result;
        }

        public bool fnApprove(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("DeplacementPalette_Approve");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
            db().AddOutParameter(mCommande, "@statut", SqlDbType.Char, 2);
            db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
            db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
            db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
            try
            {
                db().ExecuteNonQuery(ref mCommande, mTran);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        Result = true;
                        Desactive = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        _Statut = (string)db().Parameters(mCommande, "@statut");
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
                throw new Exception(ex.Message + "\r\n" + "PrixNegocie:fnActivate");
            }
            return Result;
        }

        #endregion

        #region "Private Members"

        //public override string ToString()
        //{
        //    return _Numero;
        //}

        private static void MapFromDataReader(DeplacementPalette mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    mClass._Palette = new Palette()
                    {
                        ID = (Guid)mDataReader["PaletteID"],
                        Numero = (int)mDataReader["NumeroPalette"],
                    };

                    mClass._MagasinSource = new Magasin()
                    {
                        ID = (int)mDataReader["MagasinSourceID"],
                        Designation = (string)mDataReader["MagasinSourceDesignation"],
                    };

                    if (!DBNull.Value.Equals(mDataReader["DateDeDepart"])) mClass._DateDepart = (DateTime)mDataReader["DateDeDepart"];

                    mClass._MagasinDestination = new Magasin()
                    {
                        ID = (int)mDataReader["MagasinDestinationID"],
                        Designation = (string)mDataReader["MagasinDestinationDesignation"],
                    };

                    mClass._OrdreFabrication = new OrdreFabrication()
                    {
                        ID = (Guid)mDataReader["ProductionID"],
                        NumeroProduction = (string)mDataReader["NumeroProduction"],
                    };

                    if (!DBNull.Value.Equals(mDataReader["ProduitDesignation"])) mClass._Produit = (string)mDataReader["ProduitDesignation"];
                    if (!DBNull.Value.Equals(mDataReader["TypeProduitDesignation"])) mClass._TypeDeProduit = (string)mDataReader["TypeProduitDesignation"];
                    if (!DBNull.Value.Equals(mDataReader["EmplacementDestination"])) mClass._EmplacementDestination = (string)mDataReader["EmplacementDestination"];
                    if (!DBNull.Value.Equals(mDataReader["DateArrivee"])) mClass._DateArrivee = (DateTime)mDataReader["DateArrivee"];
                    if (!DBNull.Value.Equals(mDataReader["Description"])) mClass._Description = (string)mDataReader["Description"];
                    if (!DBNull.Value.Equals(mDataReader["Operateur"])) mClass._Operateur = (string)mDataReader["Operateur"];
                    if (!DBNull.Value.Equals(mDataReader["ModeDeTransfert"])) mClass._ModeDeTransfert = (string)mDataReader["ModeDeTransfert"];
                   
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                    

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nDeplacementPalette:MapFromDataReader");
            }
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
        #endregion

    }

    public partial class DeplacementPaletteViewModel
    {
        public DeplacementPalette _DeplacementPalette { get; set; }
        public TypeDeplacementPaletteReport _TypeDeplacementPaletteReport { get; set; }
        public Parametres _Parametres { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }

    public partial class TypeDeplacementPaletteReport
    {
        public string Designation { get; set; }
    }
}
