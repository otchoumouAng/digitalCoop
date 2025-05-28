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
    public class Payement : DataPersist
    {
        #region "Fields"

        private Guid _ID;
        private Campagne _Campagne;
        private Fournisseur _Fournisseur;
        private PayementMode _PayementMode;
        private PayementType _PayementType;
        private string _Numero;
        private DateTime _DatePayement;
        private decimal _Montant;
        private string _MontantAsString;
        private string _Commentaire;
        private string _Statut;
        private bool _Desactive;
        private Site _Sites;
        private PayementOption _PayementOption;
        
        #endregion

        #region "Properties"

        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Campagne Campagne
        {
            get { return _Campagne; }
            set { _Campagne = value; }
        }

        public Fournisseur Fournisseur
        {
            get { return _Fournisseur; }
            set { _Fournisseur = value; }
        }


        public PayementMode PayementMode
        {
            get { return _PayementMode; }
            set { _PayementMode = value; }
        }

        public PayementType PayementType
        {
            get { return _PayementType; }
            set { _PayementType = value; }
        }

        public string PayementTypeAsString
        {
            get { return _PayementType != null ? _PayementType.AsString : string.Empty; }            
        }

        public string PayementModeAsString
        {
            get { return _PayementMode != null ? _PayementMode.AsString : string.Empty; }
        }

        public string PayementOptionAsString
        {
            get { return _PayementOption != null ? _PayementOption.AsString : string.Empty; }
        }

        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        

        public DateTime DatePayement
        {
            get { return _DatePayement; }
            set { _DatePayement = value; }
        }

        public string DatePayementAsString
        {
            get { return _DatePayement != null ? _DatePayement.ToString() : string.Empty; }            
        }

        public string FournisseurAsString
        {
            get { return _Fournisseur != null ? _Fournisseur.NameAndCode : string.Empty; }
        }

        public decimal Montant
        {
            get { return _Montant; }
            set { _Montant = value; }
        }

        public string MontantAsString
        {
            get { return _Montant != 0 ? String.Format("{0:# ### ### ### ###}", _Montant).TrimStart() : string.Empty; }            
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
                if (_Desactive)
                    return 0; // BulletCross                
                else
                    return 2; //                     
            }
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

        public PayementOption PayementOption
        {
            get
            {
                return _PayementOption;
            }

            set
            {
                _PayementOption = value;
            }
        }

        //public bool IsPartial
        //{
        //    get { return _Sites != null ? _Sites.Nom : string.Empty; }
        //}
        #endregion

        #region "Constructor"

        public Payement()
        {

        }

        public Payement(Guid myId)
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
                mDataReader = db().ExecuteReader("Payement_Get", (Guid)Id);
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
            return fnSelect("{Tous}", -1, null, null, -1, "-1", -1);
        }

        public List<DataPersist> fnSelect(string cropyear, int fournisseurID, DateTime? startdate, DateTime? enddate, int payementtype, string statut, int SiteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Payement_Select");
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.VarChar, cropyear);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, fournisseurID);
                db().AddInParameter(mCommande, "@startdate", SqlDbType.DateTime, startdate);
                db().AddInParameter(mCommande, "@enddate", SqlDbType.DateTime, enddate);
                db().AddInParameter(mCommande, "@payementType", SqlDbType.Int, payementtype);
                db().AddInParameter(mCommande, "@status", SqlDbType.VarChar, statut);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, SiteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Payement mClass = new Payement();

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

        public bool fnSelectByNumber(string numero)
        {            
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("");
                mDataReader = db().ExecuteReader("Payement_SelectByNumber", (string)numero);                                              

                if (mDataReader.Read())
                {                   
                    MapFromDataReader(this, mDataReader);                    
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectByNumber");
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

                    mCommande = db().CreateStoredProcCommand("Payement_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("Payement_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@campagneID", SqlDbType.VarChar, _Campagne.Designation);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, _Fournisseur.ID);
                db().AddInParameter(mCommande, "@modeID", SqlDbType.Int, _PayementMode.ID);
                db().AddInParameter(mCommande, "@typeID", SqlDbType.Int, _PayementType.ID);
                db().AddInParameter(mCommande, "@datePayement", SqlDbType.DateTime, _DatePayement);
                db().AddInParameter(mCommande, "@montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@commentaire", SqlDbType.VarChar, _Commentaire);
                db().AddInParameter(mCommande, "@statut", SqlDbType.Char, _Statut);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, _Sites.ID);

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
                        if (this.IsNew)
                        {
                            _Numero = (string)db().Parameters(mCommande, "@Numero");
                        }

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
                throw new Exception(ex.Message + "\r\n" + "Payement:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("Payement_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("Payement_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@campagneID", SqlDbType.VarChar, _Campagne.Designation);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, _Fournisseur.ID);
                db().AddInParameter(mCommande, "@modeID", SqlDbType.Int, _PayementMode.ID);
                db().AddInParameter(mCommande, "@typeID", SqlDbType.Int, _PayementType.ID);
                db().AddInParameter(mCommande, "@datePayement", SqlDbType.DateTime, _DatePayement);
                db().AddInParameter(mCommande, "@montant", SqlDbType.Money, _Montant);
                db().AddInParameter(mCommande, "@commentaire", SqlDbType.VarChar, _Commentaire);
                db().AddInParameter(mCommande, "@statut", SqlDbType.Char, _Statut);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, _Sites.ID);
                //db().AddInParameter(mCommande, "@optionID", SqlDbType.Int, _PayementOption.ID);

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
                        if (this.IsNew)
                        {
                            _Numero = (string)db().Parameters(mCommande, "@Numero");
                        }

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
                throw new Exception(ex.Message + "\r\n" + "Payement:fnUpdate");

            }
            return Result;
        }

        public override bool fnActivate()
        {

            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("Payement_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "Payement:fnActivate");
            }
            return Result;

        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("Payement_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "Payement:fnDeActivate");
            }
            return bolResult;
        }

        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _Numero;
        }

        private static void MapFromDataReader(Payement mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["Campagne"])) mClass._Campagne.Designation = (string)mDataReader["Campagne"];

                    mClass._Fournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["fournisseurID"])) mClass._Fournisseur.ID = (int)mDataReader["fournisseurID"];
                    if (!DBNull.Value.Equals(mDataReader["fournisseurNom"])) mClass._Fournisseur.Nom = (string)mDataReader["fournisseurNom"];

                    mClass._PayementMode = new PayementMode();
                    if (!DBNull.Value.Equals(mDataReader["ModeID"])) mClass._PayementMode.ID = (int)mDataReader["ModeID"];
                    if (!DBNull.Value.Equals(mDataReader["modeDesignation"])) mClass._PayementMode.Designation = (string)mDataReader["modeDesignation"];

                    mClass._PayementType = new PayementType();
                    if (!DBNull.Value.Equals(mDataReader["TypeID"])) mClass._PayementType.ID = (int)mDataReader["TypeID"];
                    if (!DBNull.Value.Equals(mDataReader["typeDesignation"])) mClass._PayementType.Designation = (string)mDataReader["typeDesignation"];

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DatePayement"])) mClass._DatePayement = (DateTime)mDataReader["DatePayement"];
                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._Montant = (decimal)mDataReader["Montant"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                    mClass._Sites = new Site();
                    if (!DBNull.Value.Equals(mDataReader["SiteID"])) mClass._Sites.ID = (int)mDataReader["SiteID"];
                    if (!DBNull.Value.Equals(mDataReader["SiteNom"])) mClass._Sites.Nom = (string)mDataReader["SiteNom"];

                    //mClass._PayementOption = new PayementOption();
                    //if (!DBNull.Value.Equals(mDataReader["OptionID"])) mClass._PayementOption.ID = (int)mDataReader["OptionID"];
                    //if (!DBNull.Value.Equals(mDataReader["OptionLibelle"])) mClass._PayementOption.Designation = (string)mDataReader["OptionLibelle"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nPayement:MapFromDataReader");
            }
        }
        #endregion

    }

    public partial class PayementViewModel
    {
        public Payement _Payement { get; set; }
        public Parametres _Parametres { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
