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
    public class InventaireAgence : DataPersist
    {
        #region Fields
        private Guid _ID;
        private Periode _Periode;
        private Campagne _Campagne;
        private Magasin _Magasin;
        private MouvementStockType _MouvementStockType;
        private DateTime _DateInventaire;
        private string _Numero;
        private bool _EstFinalise;
        private bool _Desactive;
        private string _Statut;
        private int _Quantite;
        //private int _NombreSacs;
        private decimal _PoidsBrut;
        private decimal _TareSacs;
        private decimal _TarePalette;
        private decimal _PoidsNetLivre;
        private decimal _Refaction;
        //private decimal _PoidsNetAccepte;
        private decimal _PoidsNet;
        private int _QuantitePhysique;
        private decimal _PoidsBrutPhysique;
        private string _Approbateur;
        private DateTime _DateApprobation;
        //private byte[] _RowVersionAsByte;
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

        public Periode Periode
        {
            get
            {
                return _Periode;
            }

            set
            {
                _Periode = value;
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

        public DateTime DateInventaire
        {
            get
            {
                return _DateInventaire;
            }

            set
            {
                _DateInventaire = value;
            }
        }

        public bool EstFinalise
        {
            get
            {
                return _EstFinalise;
            }

            set
            {
                _EstFinalise = value;
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

        //public int NombreSacs
        //{
        //    get
        //    {
        //        return _NombreSacs;
        //    }

        //    set
        //    {
        //        _NombreSacs = value;
        //    }
        //}

        public int Quantite
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

        public decimal PoidsNetLivre
        {
            get
            {
                return _PoidsNetLivre;
            }

            set
            {
                _PoidsNetLivre = value;
            }
        }

        public decimal Refaction
        {
            get
            {
                return _Refaction;
            }

            set
            {
                _Refaction = value;
            }
        }

        public decimal PoidsNet
        {
            get
            {
                return _PoidsNet;
            }

            set
            {
                _PoidsNet = value;
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

        public int QuantitePhysique
        {
            get
            {
                return _QuantitePhysique;
            }

            set
            {
                _QuantitePhysique = value;
            }
        }

        public decimal PoidsBrutPhysique
        {
            get
            {
                return _PoidsBrutPhysique;
            }

            set
            {
                _PoidsBrutPhysique = value;
            }
        }
        //public string NombreSacsAsString
        //{
        //    get { return _NombreSacs != 0 ? String.Format("{0:#,#}", _NombreSacs).TrimStart() : string.Empty; }
        //}

        public string QuantiteAsString
        {
            get { return _Quantite != 0 ? String.Format("{0:#,#}", _Quantite).TrimStart() : string.Empty; }
        }

        public string PoidsBrutAsString
        {
            get { return _PoidsBrut != 0 ? String.Format("{0:#,#}", _PoidsBrut).TrimStart() : string.Empty; }
        }

        public string TareSacsAsString
        {
            get { return _TareSacs != 0 ? String.Format("{0:#,#}", _TareSacs).TrimStart() : string.Empty; }
        }

        public string TarePalettesAsString
        {
            get { return _TarePalette != 0 ? String.Format("{0:#,#}", _TarePalette).TrimStart() : string.Empty; }
        }

        public string PoidsNetLivreAsString
        {
            get { return _PoidsNetLivre != 0 ? String.Format("{0:#,#}", _PoidsNetLivre).TrimStart() : string.Empty; }
        }

        public string RetentionAsString
        {
            get { return _Refaction != 0 ? String.Format("{0:#,#}", _Refaction).TrimStart() : string.Empty; }
        }

        public string PoidsNetAsString
        {
            get { return _PoidsNet != 0 ? String.Format("{0:#,#}", _PoidsNet).TrimStart() : string.Empty; }
        }

        public string QuantitePhysiqueAsString
        {
            get { return _QuantitePhysique != 0 ? String.Format("{0:#,#}", _QuantitePhysique).TrimStart() : string.Empty; }
        }

        public string PoidsBrutPhysiqueAsString
        {
            get { return _PoidsBrutPhysique != 0 ? String.Format("{0:#,#}", _PoidsBrutPhysique).TrimStart() : string.Empty; }
        }


        public string MagasinAsString
        {
            get { return _Magasin != null ? _Magasin.Designation : string.Empty; }
        }

        public string MouvementStockTypeAsString
        {
            get { return _MouvementStockType != null ? _MouvementStockType.Designation : string.Empty; }
        }

        public string PeriodeAsString
        {
            get { return _Periode != null ? _Periode.Designation : string.Empty; }
        }
        public string DateInventaireAsString
        {
            get { return _DateInventaire != null ? _DateInventaire.ToShortDateString() : string.Empty; }
        }
        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desactive)
                    return 0; // BulletCross
                else if (_Statut == "AP")
                    return 1; // Tick                
                else
                    return 2;

            }
        }

        public string Approbateur
        {
            get
            {
                return _Approbateur;
            }

            set
            {
                _Approbateur = value;
            }
        }

        public DateTime DateApprobation
        {
            get
            {
                return _DateApprobation;
            }

            set
            {
                _DateApprobation = value;
            }
        }

        //public byte[] RowVersionAsByte
        //{
        //    get
        //    {
        //        return _RowVersionKey != null ? Convert.FromBase64String(RowVersionKey.ToString()) : null ;
        //    }             
        //}

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
        #endregion

        #region Constructor
        public InventaireAgence()
        {

        }

        public InventaireAgence(Guid myId)
        {
            this.fnGet(myId);
        }

        public InventaireAgence(string _Numero)
        {
            this.Numero = _Numero;
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
            DataCommand mCommande = db().CreateStoredProcCommand("V3_StockInventaire_DeActivate");
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
                        _Statut = "CA";
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
                throw new Exception(ex.Message + "\r\n" + "Inventaire:fnDeActivate");
            }
            return bolResult;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V3_StockInventaire_Get", (Guid)Id);
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
            return fnSelect(-1, -1, "{Tous}", "-1");
        }

        public List<DataPersist> fnSelect(int MagasinID, int PeriodeId, string campagneID, string statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_StockInventaire_Select");
                db().AddInParameter(mCommande, "@magasinID", SqlDbType.Int, MagasinID);
                db().AddInParameter(mCommande, "@periodeId", SqlDbType.Int, PeriodeId);
                db().AddInParameter(mCommande, "@campagne", SqlDbType.Char, 9, campagneID);
                db().AddInParameter(mCommande, "@status", SqlDbType.Char, 2, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    InventaireAgence mClass = new InventaireAgence();
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
            throw new NotImplementedException();
        }

        public bool fnUpdate(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {
                    mCommande = db().CreateStoredProcCommand("V3_StockInventaire_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@ID2", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 8);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V3_StockInventaire_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@magasinID", SqlDbType.Int, _Magasin.ID);
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@dateInventaire", SqlDbType.DateTime, _DateInventaire);
                db().AddInParameter(mCommande, "@periodeID", SqlDbType.Int, _Periode.ID);
                db().AddInParameter(mCommande, "@mouvementTypeID", SqlDbType.Int, _MouvementStockType.ID);
                db().AddInParameter(mCommande, "@quantite", SqlDbType.Int, _Quantite);
                db().AddInParameter(mCommande, "@poidsbrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@tarebags", SqlDbType.Decimal, _TareSacs);
                db().AddInParameter(mCommande, "@tarepalette", SqlDbType.Decimal, _TarePalette);
                db().AddInParameter(mCommande, "@poidsnetlivre", SqlDbType.Decimal, _PoidsNetLivre);
                db().AddInParameter(mCommande, "@retention", SqlDbType.Decimal, _Refaction);
                db().AddInParameter(mCommande, "@poidsnet", SqlDbType.Decimal, _PoidsNet);

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
                        if (this._isnew)
                        {
                            _Numero = (string)db().Parameters(mCommande, "@Numero");
                            _Statut = "NA";
                        }

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
                throw new Exception(ex.Message + "\r\n" + "MouvementStock:fnUpdate");

            }
            return Result;
        }

        public bool fnApprove()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("V3_StockInventaire_Approve");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@approbateur", SqlDbType.VarChar, Approbateur);

                db().AddInParameter(mCommande, "@quantitephysique", SqlDbType.Int, _QuantitePhysique);
                db().AddInParameter(mCommande, "@poidsphysique", SqlDbType.Decimal, _PoidsBrutPhysique);

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
                        _EstFinalise = true;
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
                throw new Exception(ex.Message + "\r\n" + "MouvementStock:fnUpdate");

            }
            return Result;
        }

        public bool fnApprove(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("V3_StockInventaire_Approve");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@approbateur", SqlDbType.VarChar, Approbateur);

                db().AddInParameter(mCommande, "@quantitephysique", SqlDbType.Int, _QuantitePhysique);
                db().AddInParameter(mCommande, "@poidsphysique", SqlDbType.Decimal, _PoidsBrutPhysique);

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
                        _Statut = "AP";
                        _EstFinalise = true;
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
                throw new Exception(ex.Message + "\r\n" + "MouvementStock:fnUpdate");

            }
            return Result;
        }


        public override string ToString()
        {
            throw new NotImplementedException();
        }

        private static void MapFromDataReader(InventaireAgence mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["PeriodeID"]))
                    {
                        mClass._Periode = new Periode();
                        mClass._Periode.ID = (int)mDataReader["PeriodeID"];
                        mClass._Periode.Designation = (string)mDataReader["PeriodeDesignation"];
                    }


                    if (!DBNull.Value.Equals(mDataReader["MagasinID"]))
                    {
                        mClass._Magasin = new Magasin();
                        mClass._Magasin.ID = (int)mDataReader["MagasinID"];
                        mClass._Magasin.Designation = (string)mDataReader["MagasinDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["CampagneID"]))
                    {
                        mClass._Campagne = new Campagne();
                        mClass._Campagne.Designation = (string)mDataReader["CampagneID"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["mouvementTypeID"]))
                    {
                        mClass._MouvementStockType = new MouvementStockType();
                        mClass._MouvementStockType.ID = (int)mDataReader["mouvementTypeID"];
                        mClass._MouvementStockType.Designation = (string)mDataReader["mouvementTypeDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass.Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DateInventaire"])) mClass._DateInventaire = (DateTime)mDataReader["DateInventaire"];
                    if (!DBNull.Value.Equals(mDataReader["Quantite"])) mClass._Quantite = (int)mDataReader["Quantite"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalette"])) mClass._TarePalette = (decimal)mDataReader["TarePalette"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetLivre"])) mClass._PoidsNetLivre = (decimal)mDataReader["PoidsNetLivre"];
                    if (!DBNull.Value.Equals(mDataReader["Refaction"])) mClass._Refaction = (decimal)mDataReader["Refaction"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNet = (decimal)mDataReader["PoidsNetAccepte"];
                    if (!DBNull.Value.Equals(mDataReader["QuantitePhysique"])) mClass.QuantitePhysique = (int)mDataReader["QuantitePhysique"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrutPhysique"])) mClass.PoidsBrutPhysique = (decimal)mDataReader["PoidsBrutPhysique"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["EstFinalise"])) mClass._EstFinalise = (bool)mDataReader["EstFinalise"];

                    if (!DBNull.Value.Equals(mDataReader["Approbateur"])) mClass.Approbateur = (string)mDataReader["Approbateur"];
                    if (!DBNull.Value.Equals(mDataReader["DateApprobation"])) mClass.DateApprobation = (DateTime)mDataReader["DateApprobation"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nInventaire:MapFromDataReader");
            }
        }

        #endregion
    }

    public partial class InventaireAgenceViewModel
    {
        public InventaireAgence _InventaireAgence { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
