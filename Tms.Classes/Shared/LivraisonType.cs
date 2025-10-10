using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Components.Data;

namespace Tms.Classes.Shared
{
    public class LivraisonType : DataPersist
    {
        #region "Constructor"

        public LivraisonType()
        {

        }

        public LivraisonType(int myId)
        {
            this.fnGet(myId);
        }
        #endregion


        #region "Fields"

        private int _ID;
        private string _Designation;
        private Produit _Produit;
        private string _Sens;
        private FournisseurGroupe _FournisseurGroupe;
        private SacType _SacType;
        private ProvenanceType _ProvenanceType;
        private DestinationType _DestinationType;
        private bool _EstAchat;
        private bool _EstEmpotage;
        private bool _AutoriseCoupage;
        private bool _AutoriseAnalyse;
        private bool _AutoriseRefaction;
        private bool _SaisieCertification;
        private bool _SaisieNumLot;
        private bool _SaisieNumPlomb;
        private bool _SaisieNumConteneur;
        private bool _SaisieNumOT;
        private bool _SaisieNumBS;
        private bool _SaisieTransitaire;
        private bool _SaisieNumConnaisExterne;
        private bool _SaisieNumBL;
        private bool _SaisieCentreAchat;
        private bool _AfficheEcartPoids;
        private bool _AffichePoidsMoyenSacs;
        private bool _AfficheResultatAnalyse;
        private bool _GenereMvtSacherie;
        private bool _CalculeVGM;
        private bool _Desactive;
        private bool _VisibleEnAgence;
        private bool _SaisieNumTransfert;

        #endregion

        #region "Properties"
        [ModelField(IDProperty = true)]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }

        public Produit Produit
        {
            get { return _Produit; }
            set { _Produit = value; }
        }

        public string ProduitAsString
        {
            get { return _Produit != null ? _Produit.Designation : string.Empty; }            
        }

        public SacType SacType
        {
            get { return _SacType; }
            set { _SacType = value; }
        }


        public string Sens
        {
            get { return _Sens; }
            set { _Sens = value; }
        }


        public FournisseurGroupe FournisseurGroupe
        {
            get { return _FournisseurGroupe; }
            set { _FournisseurGroupe = value; }
        }

        public string FournisseurGroupeAsString
        {
            get { return _FournisseurGroupe != null ? _FournisseurGroupe.Designation : string.Empty; }            
        }

        public ProvenanceType ProvenanceType
        {
            get { return _ProvenanceType; }
            set { _ProvenanceType = value; }
        }


        public DestinationType DestinationType
        {
            get { return _DestinationType; }
            set { _DestinationType = value; }
        }

       

        public bool EstAchat
        {
            get { return _EstAchat; }
            set { _EstAchat = value; }
        }

        

        public bool EstEmpotage
        {
            get { return _EstEmpotage; }
            set { _EstEmpotage = value; }
        }


        public bool AutoriseCoupage
        {
            get { return _AutoriseCoupage; }
            set { _AutoriseCoupage = value; }
        }

        public bool AutoriseAnalyse
        {
            get { return _AutoriseAnalyse; }
            set { _AutoriseAnalyse = value; }
        }


        public bool AutoriseRefaction
        {
            get { return _AutoriseRefaction; }
            set { _AutoriseRefaction = value; }
        }

        
        public bool SaisieCertification
        {
            get { return _SaisieCertification; }
            set { _SaisieCertification = value; }
        }

        

        public bool SaisieNumLot
        {
            get { return _SaisieNumLot; }
            set { _SaisieNumLot = value; }
        }

        
        public bool SaisieNumPlomb
        {
            get { return _SaisieNumPlomb; }
            set { _SaisieNumPlomb = value; }
        }

        
        public bool SaisieNumConteneur
        {
            get { return _SaisieNumConteneur; }
            set { _SaisieNumConteneur = value; }
        }

        
        public bool SaisieNumOT
        {
            get { return _SaisieNumOT; }
            set { _SaisieNumOT = value; }
        }

        
        public bool SaisieNumBS
        {
            get { return _SaisieNumBS; }
            set { _SaisieNumBS = value; }
        }

        
        public bool SaisieTransitaire
        {
            get { return _SaisieTransitaire; }
            set { _SaisieTransitaire = value; }
        }

        
        public bool SaisieNumExterne
        {
            get { return _SaisieNumConnaisExterne; }
            set { _SaisieNumConnaisExterne = value; }
        }


        public bool SaisieNumBL
        {
            get { return _SaisieNumBL; }
            set { _SaisieNumBL = value; }
        }

        public bool SaisieCentreAchat
        {
            get { return _SaisieCentreAchat; }
            set { _SaisieCentreAchat = value; }
        }

        public bool AfficheEcartPoids
        {
            get { return _AfficheEcartPoids; }
            set { _AfficheEcartPoids = value; }
        }

        public bool AffichePoidsMoyenSacs
        {
            get { return _AffichePoidsMoyenSacs; }
            set { _AffichePoidsMoyenSacs = value; }
        }

        public bool AfficheResultatAnalyse
        {
            get { return _AfficheResultatAnalyse; }
            set { _AfficheResultatAnalyse = value; }
        }

        public bool GenereMvtSacherie
        {
            get { return _GenereMvtSacherie; }
            set { _GenereMvtSacherie = value; }
        }

        public bool CalculeVGM
        {
            get { return _CalculeVGM; }
            set { _CalculeVGM = value; }
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
        #endregion

        #region Methods
        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("LivraisonType_Get", (int) Id);
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
            return fnSelect(0,-1);
        }

        public List<DataPersist> fnSelect(int mStatus, int visibleEnAgence = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("LivraisonType_Select");
                db().AddInParameter(mCommande, "@Status", SqlDbType.SmallInt, mStatus);
                db().AddInParameter(mCommande, "@VisibleEnAgence", SqlDbType.SmallInt, visibleEnAgence);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    LivraisonType mClass = new LivraisonType();

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

                    mCommande = db().CreateStoredProcCommand("LivraisonType_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.Int, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("LivraisonType_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@designation", SqlDbType.VarChar, _Designation);                

                if (_Produit != null) db().AddInParameter(mCommande, "@produitcode", SqlDbType.Int, _Produit.ID);
                else db().AddInParameter(mCommande, "@produitcode", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@senslivraison", SqlDbType.Char, _Sens);

                if (_SacType != null) db().AddInParameter(mCommande, "@sactypecode", SqlDbType.Int, _SacType.ID);
                else db().AddInParameter(mCommande, "@sactypecode", SqlDbType.Int, DBNull.Value);

                if (_FournisseurGroupe != null) db().AddInParameter(mCommande, "@groupefournisseurID", SqlDbType.Int, _FournisseurGroupe.ID);
                else db().AddInParameter(mCommande, "@groupefournisseurID", SqlDbType.Int, DBNull.Value);

                if (_ProvenanceType != null) db().AddInParameter(mCommande, "@provenancetypeID", SqlDbType.Int, _ProvenanceType.ID);
                else db().AddInParameter(mCommande, "@provenancetypeID", SqlDbType.Int, DBNull.Value);

                if (_DestinationType != null) db().AddInParameter(mCommande, "@destinationtypeID", SqlDbType.Int, _DestinationType.ID);
                else db().AddInParameter(mCommande, "@destinationtypeID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@IsAchat", SqlDbType.Bit, _EstAchat);
                db().AddInParameter(mCommande, "@IsEmpotage", SqlDbType.Bit, _EstEmpotage);
                db().AddInParameter(mCommande, "@AutoriseCoupage", SqlDbType.Bit, _AutoriseCoupage);
                db().AddInParameter(mCommande, "@AutoriseAnalyse", SqlDbType.Bit, _AutoriseAnalyse);
                db().AddInParameter(mCommande, "@AutoriseRefaction", SqlDbType.Bit, _AutoriseRefaction);
                db().AddInParameter(mCommande, "@SaisieCertification", SqlDbType.Bit, _SaisieCertification);
                db().AddInParameter(mCommande, "@SaisieNumLot", SqlDbType.Bit, _SaisieNumLot);
                db().AddInParameter(mCommande, "@SaisieNumPlomb", SqlDbType.Bit, _SaisieNumPlomb);
                db().AddInParameter(mCommande, "@SaisieNumContainer", SqlDbType.Bit, _SaisieNumConteneur);
                db().AddInParameter(mCommande, "@SaisieNumOt", SqlDbType.Bit, _SaisieNumOT);
                db().AddInParameter(mCommande, "@SaisieNumBs", SqlDbType.Bit, _SaisieNumBS);
                db().AddInParameter(mCommande, "@SaisieTransitaire", SqlDbType.Bit, _SaisieTransitaire);
                db().AddInParameter(mCommande, "@SaisieNumConnaisExterne", SqlDbType.Bit, _SaisieNumConnaisExterne);
                db().AddInParameter(mCommande, "@SaisieNumBl", SqlDbType.Bit, _SaisieNumBL);
                //db().AddInParameter(mCommande, "@SaisieCentreAchat", SqlDbType.Bit, _SaisieCentreAchat);
                db().AddInParameter(mCommande, "@AfficheEcartDePoids", SqlDbType.Bit, _AfficheEcartPoids);
                db().AddInParameter(mCommande, "@AffichePoidsMoyenSacs", SqlDbType.Bit, _AffichePoidsMoyenSacs);
                db().AddInParameter(mCommande, "@AfficheResultatAnalyse", SqlDbType.Bit, _AfficheResultatAnalyse);
                db().AddInParameter(mCommande, "@VisibleEnAgence", SqlDbType.Bit, _VisibleEnAgence);
                db().AddInParameter(mCommande, "@SaisieNumTransfert", SqlDbType.Bit, _SaisieNumTransfert);
                //db().AddInParameter(mCommande, "@GenereMvtSacherie", SqlDbType.Bit, _GenereMvtSacherie);
                db().AddInParameter(mCommande, "@CalculVGM", SqlDbType.Bit, _CalculeVGM);                                

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
                        _ID = (int)db().Parameters(mCommande, "@ID");

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
                throw new Exception(ex.Message + "\r\n" + "LivraisonType:fnUpdate");

            }
            return Result;
        }

        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("LivraisonType_Activate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
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
                throw new Exception(ex.Message + "\r\n" + "LivraisonType:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("LivraisonType_DeActivate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
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
                throw new Exception(ex.Message + "\r\n" + "LivraisonType:fnDeActivate");
            }
            return Result;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        #endregion
        private static void MapFromDataReader(LivraisonType mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (int)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Designation"])) mClass._Designation = (string)mDataReader["Designation"];

                    mClass._Produit = new Produit();
                    if (!DBNull.Value.Equals(mDataReader["ProduitID"])) mClass._Produit.ID = (int)mDataReader["ProduitID"];
                    if (!DBNull.Value.Equals(mDataReader["ProduitNom"])) mClass._Produit.Designation = (string)mDataReader["ProduitNom"];

                    if (!DBNull.Value.Equals(mDataReader["Sens"])) mClass._Sens = (string)mDataReader["Sens"];

                    mClass._FournisseurGroupe = new FournisseurGroupe();
                    if (!DBNull.Value.Equals(mDataReader["FournisseurGroupeID"])) mClass._FournisseurGroupe.ID = (int)mDataReader["FournisseurGroupeID"];
                    if (!DBNull.Value.Equals(mDataReader["FournisseurGroupeNom"])) mClass._FournisseurGroupe.Designation = (string)mDataReader["FournisseurGroupeNom"];


                    mClass._ProvenanceType = new ProvenanceType();
                    if (!DBNull.Value.Equals(mDataReader["ProvenanceTypeID"])) mClass._ProvenanceType.ID = (int)mDataReader["ProvenanceTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["ProvenanceTypeNom"])) mClass._ProvenanceType.Designation = (string)mDataReader["ProvenanceTypeNom"];

                    mClass._DestinationType = new DestinationType();
                    if (!DBNull.Value.Equals(mDataReader["DestinationTypeID"])) mClass._DestinationType.ID = (int)mDataReader["DestinationTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["DestinationTypeNom"])) mClass._DestinationType.Designation = (string)mDataReader["DestinationTypeNom"];

                    mClass._SacType = new SacType();
                    if (!DBNull.Value.Equals(mDataReader["SacTypeID"])) mClass._SacType.ID = (int)mDataReader["SacTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["SacTypeDesignation"])) mClass._SacType.Designation = (string)mDataReader["SacTypeDesignation"];


                    if (!DBNull.Value.Equals(mDataReader["IsAchat"])) mClass._EstAchat = (bool)mDataReader["IsAchat"];
                    if (!DBNull.Value.Equals(mDataReader["IsEmpotage"])) mClass._EstEmpotage = (bool)mDataReader["IsEmpotage"];
                    if (!DBNull.Value.Equals(mDataReader["AutoriseCoupage"])) mClass._AutoriseCoupage = (bool)mDataReader["AutoriseCoupage"];
                    if (!DBNull.Value.Equals(mDataReader["AutoriseAnalyse"])) mClass._AutoriseAnalyse = (bool)mDataReader["AutoriseAnalyse"];
                    if (!DBNull.Value.Equals(mDataReader["AutoriseRefaction"])) mClass._AutoriseRefaction = (bool)mDataReader["AutoriseRefaction"];

                    if (!DBNull.Value.Equals(mDataReader["SaisieCertification"])) mClass._SaisieCertification = (bool)mDataReader["SaisieCertification"];
                    if (!DBNull.Value.Equals(mDataReader["SaisieNumLot"])) mClass._SaisieNumLot = (bool)mDataReader["SaisieNumLot"];
                    if (!DBNull.Value.Equals(mDataReader["SaisieNumPlomb"])) mClass._SaisieNumPlomb = (bool)mDataReader["SaisieNumPlomb"];
                    if (!DBNull.Value.Equals(mDataReader["SaisieNumContainer"])) mClass._SaisieNumConteneur = (bool)mDataReader["SaisieNumContainer"];
                    if (!DBNull.Value.Equals(mDataReader["SaisieNumOT"])) mClass._SaisieNumOT = (bool)mDataReader["SaisieNumOT"];

                    if (!DBNull.Value.Equals(mDataReader["SaisieNumBS"])) mClass._SaisieNumBS = (bool)mDataReader["SaisieNumBS"];
                    if (!DBNull.Value.Equals(mDataReader["SaisieTransitaire"])) mClass._SaisieTransitaire = (bool)mDataReader["SaisieTransitaire"];
                    if (!DBNull.Value.Equals(mDataReader["SaisieNumConnaisExterne"])) mClass._SaisieNumConnaisExterne = (bool)mDataReader["SaisieNumConnaisExterne"];
                    if (!DBNull.Value.Equals(mDataReader["SaisieNumBL"])) mClass._SaisieNumBL = (bool)mDataReader["SaisieNumBL"];
                    if (!DBNull.Value.Equals(mDataReader["SaisieCentreAchat"])) mClass._SaisieCentreAchat = (bool)mDataReader["SaisieCentreAchat"];

                    if (!DBNull.Value.Equals(mDataReader["AfficheEcartPoids"])) mClass._AfficheEcartPoids = (bool)mDataReader["AfficheEcartPoids"];
                    if (!DBNull.Value.Equals(mDataReader["AffichePoidsMoyenSacs"])) mClass._AffichePoidsMoyenSacs = (bool)mDataReader["AffichePoidsMoyenSacs"];
                    if (!DBNull.Value.Equals(mDataReader["AfficheResultatAnalyse"])) mClass._AfficheResultatAnalyse = (bool)mDataReader["AfficheResultatAnalyse"];
                    if (!DBNull.Value.Equals(mDataReader["GenereMvtSacherie"])) mClass._GenereMvtSacherie = (bool)mDataReader["GenereMvtSacherie"];
                    if (!DBNull.Value.Equals(mDataReader["CalculVGM"])) mClass._CalculeVGM = (bool)mDataReader["CalculVGM"];


                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                    if (!DBNull.Value.Equals(mDataReader["VisibleEnAgence"])) mClass._VisibleEnAgence = (bool)mDataReader["VisibleEnAgence"];
                    if (!DBNull.Value.Equals(mDataReader["SaisieNumTransfert"])) mClass._SaisieNumTransfert = (bool)mDataReader["SaisieNumTransfert"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n LivraisonType:MapFromDataReader");
            }
        }
        
        public static string ENTREE = "E";
        public static string SORTIE = "S";


        public string AsString
        {
            get { return _Designation; }
        }

        public bool VisibleEnAgence
        {
            get
            {
                return _VisibleEnAgence;
            }

            set
            {
                _VisibleEnAgence = value;
            }
        }

        public bool SaisieNumTransfert
        {
            get
            {
                return _SaisieNumTransfert;
            }

            set
            {
                _SaisieNumTransfert = value;
            }
        }
    }

    public partial class LivraisonTypeViewModel
    {
        public LivraisonType _LivraisonType { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
