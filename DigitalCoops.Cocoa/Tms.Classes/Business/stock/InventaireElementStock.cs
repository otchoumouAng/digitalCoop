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
    public class InventaireElementStock : DataPersist
    {
        #region Fields
        private Guid _ID;
        private Inventaire _Inventaire;
        private Guid _ObjetEnStockID;
        private TypeElementStock _TypeElementStock;
        private SacType _SacType;
        private int _Quantite;
        private decimal _PoidsBrut;
        private decimal _TareSacs;
        private decimal _TarePalette;
        private decimal _PoidsNetLivre;
        private decimal _Refaction;
        private decimal _PoidsNetAccepte;
        private string _Reference;

        private int _QuantitePhysique;
        private decimal _PoidsBrutPhysique;
        #endregion

        #region Properties
        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public Inventaire Inventaire
        {
            get
            {
                return _Inventaire;
            }

            set
            {
                _Inventaire = value;
            }
        }

        public TypeElementStock TypeElementStock
        {
            get
            {
                return _TypeElementStock;
            }

            set
            {
                _TypeElementStock = value;
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

        public bool GenereMouvement
        {
            get { return ((_Quantite != 0 && _QuantitePhysique != 0) && (_Quantite != _QuantitePhysique)); }
        }

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

        public string PoidsNetAccepteAsString
        {
            get { return _PoidsNetAccepte != 0 ? String.Format("{0:#,#}", _PoidsNetAccepte).TrimStart() : string.Empty; }
        }
        public string ObjetEnStock
        {
            get { return _TypeElementStock != null? _TypeElementStock.Designation : string.Empty; }
        }

        public string SacTypeAsString
        {
            get { return _SacType != null ? _SacType.Designation : string.Empty; }
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

        public string QuantitePhysiqueAsString
        {
            get { return _QuantitePhysique != 0 ? String.Format("{0:#,#}", _QuantitePhysique).TrimStart() : string.Empty; }
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

        public string PoidsBrutPhysiqueAsString
        {
            get { return _PoidsBrutPhysique != 0 ? String.Format("{0:#,#}", _PoidsBrutPhysique).TrimStart() : string.Empty; }
        }

        public Guid ObjetEnStockID
        {
            get
            {
                return _ObjetEnStockID;
            }

            set
            {
                _ObjetEnStockID = value;
            }
        }
        #endregion

        #region Constructor
        public InventaireElementStock()
        {

        }

        public InventaireElementStock(Guid myId)
        {
            this.fnGet(myId);
        }
        #endregion
        #region Methods
        public override bool fnActivate()
        {
            throw new NotImplementedException();
        }

        public override bool fnDeActivate()
        {
            throw new NotImplementedException();
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_InventaireElementStock_Get", (Guid)Id);
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
            return fnSelect(Guid.Empty);
        }

        public List<DataPersist> fnSelect(Guid inventaireID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Inventaire_Select");
                db().AddInParameter(mCommande, "@inventaireID", SqlDbType.UniqueIdentifier, _Inventaire.ID);
                //db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, mCampagne);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    InventaireElementStock mClass = new InventaireElementStock();
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

        public List<DataPersist> fnSelectItemInStock(string campagneID, int magasinID, DateTime? dateInventaire, int siteID = 1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_StatutStockItem_Select");
                db().AddInParameter(mCommande, "@magasinID", SqlDbType.Int, magasinID);
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, campagneID);
                db().AddInParameter(mCommande, "@exportateurID", SqlDbType.Int, -1);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, null);
                db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, dateInventaire);
                db().AddInParameter(mCommande, "@emplacementID", SqlDbType.Int, -1);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    InventaireElementStock mClass = new InventaireElementStock();
                    MapFromDataReaderLite(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectItemInStock");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectItemInStockByID(Guid InventaireID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_InventaireElementStock_Select");
                db().AddInParameter(mCommande, "@inventaireID", SqlDbType.UniqueIdentifier, InventaireID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    InventaireElementStock mClass = new InventaireElementStock();
                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectItemInStockByID");
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
                    mCommande = db().CreateStoredProcCommand("V2_InventaireElementStock_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);                    
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_InventaireElementStock_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@inventaireID", SqlDbType.UniqueIdentifier, _Inventaire.ID);    
                if (_SacType != null)                           
                    db().AddInParameter(mCommande, "@typeSacID", SqlDbType.Int, _SacType.ID);
                else
                    db().AddInParameter(mCommande, "@typeSacID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@objetEnStockID", SqlDbType.UniqueIdentifier, _ObjetEnStockID);
                db().AddInParameter(mCommande, "@objetEnStockTypeID", SqlDbType.Int, _TypeElementStock.ID);
                db().AddInParameter(mCommande, "@reference", SqlDbType.VarChar,50, _Reference);
                db().AddInParameter(mCommande, "@quantite", SqlDbType.Int, _Quantite);
                db().AddInParameter(mCommande, "@poidsbrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@taresacs", SqlDbType.Decimal, _TareSacs);
                db().AddInParameter(mCommande, "@tarepalettes", SqlDbType.Decimal, _TarePalette);
                db().AddInParameter(mCommande, "@poidsnetlivre", SqlDbType.Decimal, _PoidsNetLivre);
                db().AddInParameter(mCommande, "@refaction", SqlDbType.Decimal, _Refaction);
                db().AddInParameter(mCommande, "@poidsnetaccepte", SqlDbType.Decimal, _PoidsNetAccepte);

                db().AddInParameter(mCommande, "@quantitePhysique", SqlDbType.Int, _Quantite);
                db().AddInParameter(mCommande, "@poidsbrutPhysique", SqlDbType.Decimal, _PoidsBrut);

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
                }
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + "\r\n" + "InventaireElementStock:fnUpdate");

            }
            return Result;
        }

        public bool fnUpdateSite(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {
                    mCommande = db().CreateStoredProcCommand("V2_InventaireElementStock_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_InventaireElementStock_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@inventaireID", SqlDbType.UniqueIdentifier, _Inventaire.ID);
                if (_SacType != null)
                    db().AddInParameter(mCommande, "@typeSacID", SqlDbType.Int, _SacType.ID);
                else
                    db().AddInParameter(mCommande, "@typeSacID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@objetEnStockID", SqlDbType.UniqueIdentifier, _ObjetEnStockID);
                db().AddInParameter(mCommande, "@objetEnStockTypeID", SqlDbType.Int, _TypeElementStock.ID);
                db().AddInParameter(mCommande, "@reference", SqlDbType.VarChar, 50, _Reference);
                db().AddInParameter(mCommande, "@quantite", SqlDbType.Int, _Quantite);
                db().AddInParameter(mCommande, "@poidsbrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@taresacs", SqlDbType.Decimal, _TareSacs);
                db().AddInParameter(mCommande, "@tarepalettes", SqlDbType.Decimal, _TarePalette);
                db().AddInParameter(mCommande, "@poidsnetlivre", SqlDbType.Decimal, _PoidsNetLivre);
                db().AddInParameter(mCommande, "@refaction", SqlDbType.Decimal, _Refaction);
                db().AddInParameter(mCommande, "@poidsnetaccepte", SqlDbType.Decimal, _PoidsNetAccepte);

                db().AddInParameter(mCommande, "@quantitePhysique", SqlDbType.Int, _Quantite);
                db().AddInParameter(mCommande, "@poidsbrutPhysique", SqlDbType.Decimal, _PoidsBrut);

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
                }
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + "\r\n" + "InventaireElementStock:fnUpdate");

            }
            return Result;
        }


        public bool fnUpdateStock()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                
                mCommande = db().CreateStoredProcCommand("V2_InventaireElementStock_Modify");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);                               
                db().AddInParameter(mCommande, "@quantite", SqlDbType.Int, _QuantitePhysique);
                db().AddInParameter(mCommande, "@poidsbrut", SqlDbType.Decimal, _PoidsBrutPhysique);

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
                throw new Exception(ex.Message + "\r\n" + "InventaireElementStock:fnUpdate");

            }
            return Result;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        private static void MapFromDataReader(InventaireElementStock mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._Inventaire = new Inventaire();                    
                    if (!DBNull.Value.Equals(mDataReader["inventaireID"]))
                    {
                        mClass._Inventaire = new Inventaire();
                        mClass._Inventaire.ID = (Guid)mDataReader["inventaireID"];
                        mClass._Inventaire.Numero = (string)mDataReader["NumeroInventaire"];
                    }

                    mClass._TypeElementStock = new TypeElementStock();
                    if (!DBNull.Value.Equals(mDataReader["ObjetEnStockTypeID"]))
                    {
                        mClass._TypeElementStock = new TypeElementStock();
                        mClass._TypeElementStock.ID = (int)mDataReader["ObjetEnStockTypeID"];
                        mClass._TypeElementStock.Designation = (string)mDataReader["ObjetEnStockDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ObjetEnStockID"])) mClass._ObjetEnStockID = (Guid)mDataReader["ObjetEnStockID"];
                    if (!DBNull.Value.Equals(mDataReader["ReferenceObjetEnstock"])) mClass._Reference = (string)mDataReader["ReferenceObjetEnstock"];
                    if (!DBNull.Value.Equals(mDataReader["QuantiteTheorique"])) mClass._Quantite = (int)mDataReader["QuantiteTheorique"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrutTheorique"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrutTheorique"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalette = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetLivre"])) mClass._PoidsNetLivre = (decimal)mDataReader["PoidsNetLivre"];
                    if (!DBNull.Value.Equals(mDataReader["RetentionPoids"])) mClass._Refaction = (decimal)mDataReader["RetentionPoids"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetAccepte"])) mClass._PoidsNetAccepte = (decimal)mDataReader["PoidsNetAccepte"];

                    if (!DBNull.Value.Equals(mDataReader["QuantitePhysique"])) mClass._QuantitePhysique = (int)mDataReader["QuantitePhysique"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrutPhysique"])) mClass._PoidsBrutPhysique = (decimal)mDataReader["PoidsBrutPhysique"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];
                                       
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nInventaireElementStock:MapFromDataReader");
            }
        }


        private static void MapFromDataReaderLite(InventaireElementStock mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    mClass._ID = Guid.NewGuid();                    

                    mClass._TypeElementStock = new TypeElementStock();
                    if (!DBNull.Value.Equals(mDataReader["ElementTypeID"]))
                    {
                        mClass._TypeElementStock = new TypeElementStock();
                        mClass._TypeElementStock.ID = (int)mDataReader["ElementTypeID"];
                        mClass._TypeElementStock.Designation = (string)mDataReader["ElementTypeDesignation"];
                    }

                    mClass._SacType = new SacType();
                    if (!DBNull.Value.Equals(mDataReader["SacTypeID"]))
                    {
                        mClass._SacType = new SacType();
                        mClass._SacType.ID = (int)mDataReader["SacTypeID"];
                        mClass._SacType.Designation = (string)mDataReader["SacTypeDesignation"];
                    }
                    if (!DBNull.Value.Equals(mDataReader["ObjetEnStockID"])) mClass._ObjetEnStockID = (Guid)mDataReader["ObjetEnStockID"];
                    if (!DBNull.Value.Equals(mDataReader["Reference"])) mClass._Reference = (string)mDataReader["Reference"];
                    if (!DBNull.Value.Equals(mDataReader["Quantite"])) mClass._Quantite = (int)mDataReader["Quantite"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalette = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetLivre"])) mClass._PoidsNetLivre = (decimal)mDataReader["PoidsNetLivre"];
                    if (!DBNull.Value.Equals(mDataReader["RetentionPoids"])) mClass._Refaction = (decimal)mDataReader["RetentionPoids"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetAccepte"])) mClass._PoidsNetAccepte = (decimal)mDataReader["PoidsNetAccepte"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nInventaireElementStock:MapFromDataReaderLite");
            }
        }


        #endregion
    }

    public partial class InventaireElementStockViewModel
    {
        public InventaireElementStock _InventaireElementStock { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
