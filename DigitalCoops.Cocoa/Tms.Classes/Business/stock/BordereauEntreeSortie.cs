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

namespace Tms.Classes.Business.stock
{
    public class BordereauEntreeSortie : DataPersist
    {
        #region Fields
        private Guid _ID;
        private Campagne _Campagne;
        private Magasin _Magasin;        
        private Exportateur _Exportateur;
        private MouvementStockType _MouvementStockType;
        private SacType _SacType;
        private Guid? _ObjetID;
        private string _ObjetRef;
        private DateTime _DateBordereauES;
        private DateTime _DateBordereau;
        private Int16 _Sens;
        private string _Numero;
        private string _NumeroTransfert;
        private string _Reference;
        private decimal _Quantite;
        private decimal _PoidsBrut;
        private decimal _TareSacs;
        private decimal _TarePalette;
        private decimal _PoidsLivre;
        private decimal _RetentionPoids;
        private decimal _PoidsNetAccepte;
        private string _Commentaire;
        private string _Statut;
        private bool _Desactive;
        private decimal? _Prix;
        private decimal? _Montant;
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

        public Exportateur Exportateur
        {
            get
            {
                return _Exportateur;
            }

            set
            {
                _Exportateur = value;
            }
        }

        public string ExportateurAsString
        {
            get
            {
                return _Exportateur != null ? _Exportateur.Nom : string.Empty;
            }
        }
        public MouvementStockType MouvementStockType
        {
            get
            {
                return _MouvementStockType;
            }

            set
            {
                _MouvementStockType = value;
            }
        }
        public string MouvementStockTypeAsString
        {
            get
            {
                return _MouvementStockType != null ? _MouvementStockType.Designation : string.Empty;
            }
        }
        public SacType SacType
        {
            get
            {
                return _SacType;
            }

            set
            {
                _SacType = value;
            }
        }
        public string SacTypeAsString
        {
            get
            {
                return _SacType != null ? _SacType.Designation : string.Empty;
            }
        }
        public Guid? ObjetID
        {
            get
            {
                return _ObjetID;
            }

            set
            {
                _ObjetID = value;
            }
        }

        public DateTime DateBordereau
        {
            get
            {
                return _DateBordereau;
            }

            set
            {
                _DateBordereau = value;
            }
        }
        public string DateBordereauAsString
        {
            get
            {
                return _DateBordereau != null ? DateBordereau.ToShortDateString() : string.Empty;
            }
        }
        public Int16 Sens
        {
            get
            {
                return _Sens;
            }

            set
            {
                _Sens = value;
            }
        }

        public string Numero
        {
            get
            {
                return _Numero;
            }

            set
            {
                _Numero = value;
            }
        }

        public string Reference
        {
            get
            {
                return _Reference;
            }

            set
            {
                _Reference = value;
            }
        }

        public decimal Quantite
        {
            get
            {
                return _Quantite;
            }

            set
            {
                _Quantite = value;
            }
        }

        public string QuantiteAsString
        {
            get { return _Quantite != 0 ? String.Format("{0:#,#}", _Quantite).TrimStart() : string.Empty; }
        }

        public decimal PoidsBrut
        {
            get
            {
                return _PoidsBrut;
            }

            set
            {
                _PoidsBrut = value;
            }
        }

        public string PoidsBrutAsString
        {
            get { return _PoidsBrut != 0 ? String.Format("{0:#,#}", _PoidsBrut).TrimStart() : string.Empty; }
        }

        public decimal TareSacs
        {
            get
            {
                return _TareSacs;
            }

            set
            {
                _TareSacs = value;
            }
        }

        public string TareSacsAsString
        {
            get { return _TareSacs != 0 ? String.Format("{0:#,#}", _TareSacs).TrimStart() : string.Empty; }
        }

        public decimal TarePalette
        {
            get
            {
                return _TarePalette;
            }

            set
            {
                _TarePalette = value;
            }
        }

        public string TarePaletteAsString
        {
            get { return _TarePalette != 0 ? String.Format("{0:#,#}", _TarePalette).TrimStart() : string.Empty; }
        }

        public decimal PoidsLivre
        {
            get
            {
                return _PoidsLivre;
            }

            set
            {
                _PoidsLivre = value;
            }
        }

        public string PoidsLivreAsString
        {
            get { return _PoidsLivre != 0 ? String.Format("{0:#,#}", _PoidsLivre).TrimStart() : string.Empty; }
        }

        public decimal RetentionPoids
        {
            get
            {
                return _RetentionPoids;
            }

            set
            {
                _RetentionPoids = value;
            }
        }

        public decimal PoidsNetAccepte
        {
            get
            {
                return _PoidsNetAccepte;
            }

            set
            {
                _PoidsNetAccepte = value;
            }
        }

        public string PoidsNetAccepteAsString
        {
            get { return _PoidsNetAccepte != 0 ? String.Format("{0:#,#}", _PoidsNetAccepte).TrimStart() : string.Empty; }
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

        public Magasin Magasin
        {
            get
            {
                return _Magasin;
            }

            set
            {
                _Magasin = value;
            }
        }
        public string MagasinAsString
        {
            get
            {
                return _Magasin != null ? _Magasin.Designation : string.Empty;
            }
        }        

        

        public Campagne Campagne
        {
            get
            {
                return _Campagne;
            }

            set
            {
                _Campagne = value;
            }
        }
        public string beCampagne
        {
            get
            {
                return _Campagne != null ? _Campagne.Designation : string.Empty;
            }
        }
        public string ObjetRef
        {
            get
            {
                return _ObjetRef;
            }

            set
            {
                _ObjetRef = value;
            }
        }

        public string NumeroTransfert
        {
            get
            {
                return _NumeroTransfert;
            }

            set
            {
                _NumeroTransfert = value;
            }
        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desactive || _Statut == "CA")
                    return 0; // BulletCross
                else if (_Sens == 1)
                    return 1; // Tick
                else if (_Sens == -1)
                    return -1;
                else
                    return 1;
            }
        }

        public DateTime DateBordereauES
        {
            get
            {
                return _DateBordereauES;
            }

            set
            {
                _DateBordereauES = value;
            }
        }

        public decimal? Prix
        {
            get
            {
                return _Prix;
            }

            set
            {
                _Prix = value;
            }
        }

        public string PrixAsString
        {
            get { return _Prix != 0 ? String.Format("{0:#,#}", _Prix).TrimStart() : string.Empty; }
        }

        public decimal? Montant
        {
            get
            {
                return _Montant;
            }

            set
            {
                _Montant = value;
            }
        }

        public string MontantAsString
        {
            get { return _Montant != 0 ? String.Format("{0:#,#}", _Montant).TrimStart() : string.Empty; }
        }
        #endregion

        #region Constructor
        public BordereauEntreeSortie()
        {

        }

        public BordereauEntreeSortie(Guid myID)
        {
            this.fnGet(myID);
        }
        #endregion
        #region Methods
        public override bool fnActivate()
        {
            throw new NotImplementedException();
        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_BordereauEntreeSortie_DeActivate");
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
                }
            }
            catch (Exception ex)
            {
                bolResult = false;
                throw new Exception(ex.Message + "\r\n" + "BordereauEntreeSortie:fnDeActivate");
            }
            return bolResult;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_BordereauEntreeSortie_Get", (Guid)Id);
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
            return fnSelect("{Tous}", -1,-1, null, null, -2, -2, -1);
        }

        public List<DataPersist> fnSelect(string mCampagne, int MagasinID, int ExportateurID, DateTime? StartDate, DateTime? EndDate, int sens, int mouvementTypeID, int statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_BordereauEntreeSortie_Select");
                db().AddInParameter(mCommande, "@magasinID", SqlDbType.Int, MagasinID);
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, mCampagne);
                db().AddInParameter(mCommande, "@exportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@mouvementTypeID", SqlDbType.Int, mouvementTypeID);
                db().AddInParameter(mCommande, "@sens", SqlDbType.SmallInt, sens);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@statut", SqlDbType.Int, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    BordereauEntreeSortie mClass = new BordereauEntreeSortie();
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
                    mCommande = db().CreateStoredProcCommand("V2_BordereauEntreeSortie_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_BordereauEntreeSortie_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                                
                db().AddInParameter(mCommande, "@magasinId", SqlDbType.Int, _Magasin.ID);                
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@exportateurId", SqlDbType.Int, _Exportateur.ID);
                db().AddInParameter(mCommande, "@dateBordereau", SqlDbType.DateTime, _DateBordereau);                
                db().AddInParameter(mCommande, "@sens", SqlDbType.SmallInt, _Sens);
                db().AddInParameter(mCommande, "@mouvementTypeId", SqlDbType.Int, _MouvementStockType.ID);

                if (!string.IsNullOrEmpty(_ObjetID.ToString()))
                    db().AddInParameter(mCommande, "@objectEnStockID", SqlDbType.UniqueIdentifier, _ObjetID);
                else
                    db().AddInParameter(mCommande, "@objectEnStockID", SqlDbType.UniqueIdentifier, DBNull.Value);

                db().AddInParameter(mCommande, "@objectEnStockRef", SqlDbType.VarChar, _ObjetRef);
                db().AddInParameter(mCommande, "@numeroBordereau", SqlDbType.VarChar, _Numero);
                db().AddInParameter(mCommande, "@numeroTransfert", SqlDbType.VarChar, _NumeroTransfert);
                db().AddInParameter(mCommande, "@reference", SqlDbType.VarChar, _Reference);
                db().AddInParameter(mCommande, "@sactypeId", SqlDbType.Int, _SacType.ID);
                db().AddInParameter(mCommande, "@quantite", SqlDbType.Int, _Quantite);
                db().AddInParameter(mCommande, "@poidsbrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@tarebags", SqlDbType.Decimal, _TareSacs);
                db().AddInParameter(mCommande, "@tarepalette", SqlDbType.Decimal, _TarePalette);
                db().AddInParameter(mCommande, "@poidsnetlivre", SqlDbType.Decimal, _PoidsLivre);
                db().AddInParameter(mCommande, "@retention", SqlDbType.Decimal, _RetentionPoids);
                db().AddInParameter(mCommande, "@poidsnetaccepte", SqlDbType.Decimal, _PoidsNetAccepte);
                db().AddInParameter(mCommande, "@commentaire", SqlDbType.VarChar, _Commentaire);

                db().AddInParameter(mCommande, "@Prix", SqlDbType.Decimal, _Prix);
                db().AddInParameter(mCommande, "@Montant", SqlDbType.Decimal, _Montant);

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
                        _Statut = "AP";

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
                throw new Exception(ex.Message + "\r\n" + "BordereauEntreeSortie:fnUpdate");

            }
            return Result;
        }

        public override string ToString()
        {
            return _Numero;
        }

        private static void MapFromDataReader(BordereauEntreeSortie mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass._Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["MagasinID"]))
                    {
                        mClass._Magasin = new Magasin();
                        mClass._Magasin.ID = (int)mDataReader["MagasinID"];
                        mClass._Magasin.Designation = (string)mDataReader["MagasinDesignation"];
                    }                   

                    if (!DBNull.Value.Equals(mDataReader["MouvementTypeID"]))
                    {
                        mClass._MouvementStockType = new MouvementStockType();
                        mClass._MouvementStockType.ID = (int)mDataReader["MouvementTypeID"];
                        mClass._MouvementStockType.Designation = (string)mDataReader["MouvementTypeDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ExportateurID"]))
                    {
                        mClass._Exportateur = new Exportateur();
                        mClass._Exportateur.ID = (int)mDataReader["ExportateurID"];
                        mClass._Exportateur.Nom = (string)mDataReader["ExportateurNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["SacTypeID"]))
                    {
                        mClass._SacType = new SacType();
                        mClass._SacType.ID = (int)mDataReader["SacTypeID"];
                        mClass._SacType.Designation = (string)mDataReader["SacTypeDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ObjetID"])) mClass._ObjetID = (Guid)mDataReader["ObjetID"];
                    if (!DBNull.Value.Equals(mDataReader["ObjetRef"])) mClass._ObjetRef = (string)mDataReader["ObjetRef"];
                    if (!DBNull.Value.Equals(mDataReader["Reference"])) mClass._Reference = (string)mDataReader["Reference"];

                    if (!DBNull.Value.Equals(mDataReader["NumeroBordereau"])) mClass._Numero = (string)mDataReader["NumeroBordereau"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroTransfert"])) mClass._NumeroTransfert = (string)mDataReader["NumeroTransfert"];

                    if (!DBNull.Value.Equals(mDataReader["DateBordereau"])) mClass._DateBordereau = (DateTime)mDataReader["DateBordereau"];
                    if (!DBNull.Value.Equals(mDataReader["Sens"])) mClass._Sens = (Int16)mDataReader["Sens"];
                    if (!DBNull.Value.Equals(mDataReader["Quantite"])) mClass._Quantite = (int)mDataReader["Quantite"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalette"])) mClass._TarePalette = (decimal)mDataReader["TarePalette"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsLivre"])) mClass._PoidsLivre = (decimal)mDataReader["PoidsLivre"];
                    if (!DBNull.Value.Equals(mDataReader["RetentionPoids"])) mClass._RetentionPoids = (decimal)mDataReader["RetentionPoids"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetAccepte"])) mClass._PoidsNetAccepte = (decimal)mDataReader["PoidsNetAccepte"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];

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
                throw new Exception(ex.Message + "\nBordereauEntreeSortie:MapFromDataReader");
            }
        }

        #endregion
    }

    public partial class BordereauEntreeSortieViewModel
    {
        public BordereauEntreeSortie _BordereauEntreeSortie { get; set; }
        public string _DefaultCampagne {
            get
            {
                Parametres mParam = new Parametres(0);
                return mParam.Campagne;
            }
            set { }
        }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
