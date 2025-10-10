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

namespace Tms.Classes.Business.Sites
{
    public class LotCoop : DataPersist
    {
        #region fields
        private Guid _ID;
        private Campagne _Campagne;
        private Livraison _Livraison;
        private Site _Sites;
        private DateTime _DateComposition;
        private DateTime _DateExpedition;
        private Magasin _Magasin;
        private string _ReferenceCoop;
        private string _Numero;
        private string _NumeroTransfert;
        private int _NombreSacs;
        private int _NombrePalettes;
        private decimal _PoidsBrut;
        private decimal _TareSacs;
        private decimal _TarePalette;
        private decimal _PoidsNet;
        private string _Statut;
        private string _Commentaire;
        private bool _Desactive;
        private int _NbreSacsAccepteInitial;
        private int _EstCertifie;
        private Certification _Certification;
        private ElementEnStock _ElementEnStock;
        private TypeChargement _TypeChargement;
        private Guid _IDTransfert;
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

        public Livraison Livraison
        {
            get
            {
                return _Livraison;
            }

            set
            {
                _Livraison = value;
            }
        }

        public Site Sites
        {
            get
            {
                return _Sites;
            }

            set
            {
                _Sites = value;
            }
        }

        public string LivraisonID
        {
            get { return _Livraison != null && _Livraison.Numero != null ? _Livraison.Numero  : string.Empty; }
        }

        public string EstCertifieAsString
        {
            get { return _Certification != null ? _Certification.Designation : string.Empty; }
        }

        public string Immatriculation
        {
            get { return _Livraison != null ? _Livraison.Immatriculation : string.Empty; }
        }

        public string TypeEnStock
        {
            get { return _ElementEnStock != null ? _ElementEnStock.Designation : string.Empty; }
        }

        public DateTime DateComposition
        {
            get
            {
                return _DateComposition;
            }

            set
            {
                _DateComposition = value;
            }
        }

        public string DateCompositionAsString
        {
            get { return _DateComposition != null ? _DateComposition.ToShortDateString() : string.Empty; }
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

        public int NombreSacs
        {
            get
            {
                return _NombreSacs;
            }

            set
            {
                _NombreSacs = value;
            }
        }

        public string NombreSacsAsString
        {
            get
            {
                return _NombreSacs != 0 ? string.Format("{0:#,#}", _NombreSacs).TrimStart() : "0";
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

        public string PoidsBrutAsString
        {
            get
            {
                return _PoidsBrut != 0 ? string.Format("{0:#,#}", _PoidsBrut).TrimStart() : "0";
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

        public string TareSacsAsString
        {
            get
            {
                return _TareSacs != 0 ? string.Format("{0:#,#}", _TareSacs).TrimStart() : "0";
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

        public string TarePaletteAsString
        {
            get
            {
                return _TarePalette != 0 ? string.Format("{0:#,#}", _TarePalette).TrimStart() : "0";
            }
        }

        public string PoidsNetEvalAsString
        {
            get
            {
                return _PoidsNet != 0 ? string.Format("{0:#,#}", Math.Round((_PoidsNet * 8) / 100), 0).TrimStart() : "0";
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

        public string PoidsNetAsString
        {
            get
            {
                return _PoidsNet != 0 ? string.Format("{0:#,#}", _PoidsNet).TrimStart() : "0";
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

        public bool EstFinalise
        {
            get
            {
                return( _Statut == "AP");
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

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desactive)
                    return 0; // Annulée
                else if (_Statut == "AP")
                    return 1; // Tick
                else if (_Statut == "NA")
                    return 2; // 
                else if (_Statut == "CA")
                    return 0; //                                 
                else
                    return 2; // 
            }
        }                
               

        public string SiteAsString
        {
            get
            {
                return _Sites != null ? _Sites.Nom : string.Empty;
            }
        }

        public string ReferenceCoop
        {
            get
            {
                return _ReferenceCoop;
            }

            set
            {
                _ReferenceCoop = value;
            }
        }

        public int NbreSacsAccepteInitial
        {
            get
            {
                return _NbreSacsAccepteInitial;
            }

            set
            {
                _NbreSacsAccepteInitial = value;
            }
        }

        public int EstCertifie
        {
            get
            {
                return _EstCertifie;
            }

            set
            {
                _EstCertifie = value;
            }
        }

        public Certification Certification
        {
            get
            {
                return _Certification;
            }

            set
            {
                _Certification = value;
            }
        }

        public ElementEnStock ElementEnStock
        {
            get
            {
                return _ElementEnStock;
            }

            set
            {
                _ElementEnStock = value;
            }
        }

        public TypeChargement TypeChargement
        {
            get
            {
                return _TypeChargement;
            }

            set
            {
                _TypeChargement = value;
            }
        }

        public string TypeChargementAsString
        {
            get { return _TypeChargement != null ? _TypeChargement.Designation : string.Empty; }
        }

        public int NombrePalettes
        {
            get
            {
                return _NombrePalettes;
            }

            set
            {
                _NombrePalettes = value;
            }
        }
        public string NombrePalettesAsString
        {
            get
            {
                return _NombrePalettes != 0 ? string.Format("{0:#,#}", _NombrePalettes).TrimStart() : "0";
            }
        }

        public DateTime DateExpedition
        {
            get
            {
                return _DateExpedition;
            }

            set
            {
                _DateExpedition = value;
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

        public Guid IDTransfert
        {
            get
            {
                return _IDTransfert;
            }

            set
            {
                _IDTransfert = value;
            }
        }
        #endregion


        #region Constructor
        public LotCoop()
        {

        }

        public LotCoop(Guid myId)
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
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V4_Lot_DeActivate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
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
                        Statut = "CA";
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
                throw new Exception(ex.Message + "\r\n" + "LotCoop:fnDeActivate");
            }
            return bolResult;
        }

        public bool fnApprove()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V4_Lot_Approve");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
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
                        Statut = "AP";
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
                throw new Exception(ex.Message + "\r\n" + "LotCoop:fnApprove");
            }
            return bolResult;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V4_Lot_Get", (Guid)Id);
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

        public  bool fnGet_ByID(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_Lot_GestionStock_Get", (Guid)Id);
                if (mDataReader.Read())
                {
                    MapFromDataReaderLite2(this, mDataReader);
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

        public bool fnGet_ByLotReception(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V4_Lot_GetForReception", (Guid)Id);
                if (mDataReader.Read())
                {
                    MapFromDataReaderLite3(this, mDataReader);
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
            return fnSelect("{Tous}", null, null, "-1", -1, -1);
        }

        public virtual List<DataPersist> fnSelect(string CampagneID, DateTime? StartDate, DateTime? EndDate, string status, int SiteID, int certif)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V4_Lot_Select");

                db().AddInParameter(mCommande, "@CropYear", SqlDbType.Char, 9, CampagneID);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, SiteID);
                db().AddInParameter(mCommande, "@certification", SqlDbType.Int, certif);
                db().AddInParameter(mCommande, "@StartDate", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@EndDate", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, status);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    LotCoop mClass = new LotCoop();

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

        public virtual List<DataPersist> fnSelectForVente(string CampagneID, DateTime? StartDate, DateTime? EndDate, int SiteID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V4_Lot_SelectForVente");

                db().AddInParameter(mCommande, "@CropYear", SqlDbType.Char, 9, CampagneID);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, SiteID);
                db().AddInParameter(mCommande, "@StartDate", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@EndDate", SqlDbType.DateTime, EndDate);                
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    LotCoop mClass = new LotCoop();

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
        public virtual List<DataPersist> fnSelectForExpedition(string CampagneID, DateTime? StartDate, DateTime? EndDate, int SiteID, int MagasinID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V4_Lot_SelectLot");

                db().AddInParameter(mCommande, "@CropYear", SqlDbType.Char, 9, CampagneID);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, SiteID);
                db().AddInParameter(mCommande, "@MagasinID", SqlDbType.Int, MagasinID);
                db().AddInParameter(mCommande, "@StartDate", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@EndDate", SqlDbType.DateTime, EndDate);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    LotCoop mClass = new LotCoop();

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

        public virtual List<DataPersist> fnSelectForReception(string CampagneID, int SiteID, int MagasinID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V4_Lot_SelectLotReception");

                db().AddInParameter(mCommande, "@CropYear", SqlDbType.Char, 9, CampagneID);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, SiteID);
                //db().AddInParameter(mCommande, "@MagasinID", SqlDbType.Int, MagasinID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    LotCoop mClass = new LotCoop();

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


        public virtual List<DataPersist> fnSelectForLivraison(int SiteID, string CampagneID, DateTime? StartDate, DateTime? EndDate)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V4_Lot_SelectForLivraison");

                db().AddInParameter(mCommande, "@CropYear", SqlDbType.Char, 9, CampagneID);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, SiteID);
                db().AddInParameter(mCommande, "@StartDate", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@EndDate", SqlDbType.DateTime, EndDate);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    LotCoop mClass = new LotCoop();

                    MapFromDataReaderLite(mClass, mDataReader);
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
                    mCommande = db().CreateStoredProcCommand("V4_Lot_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V4_Lot_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, _Sites.ID);                
                db().AddInParameter(mCommande, "@DateComposition", SqlDbType.DateTime, _DateComposition);
                db().AddInParameter(mCommande, "@description", SqlDbType.VarChar, _Commentaire);
                db().AddInParameter(mCommande, "@referenceCoop", SqlDbType.VarChar, ReferenceCoop);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, _TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNet);
                db().AddInParameter(mCommande, "@estCertifie", SqlDbType.Int, _EstCertifie);
                db().AddInParameter(mCommande, "@typeElementEnStock", SqlDbType.Int, _ElementEnStock.ID);
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
                        ID = (Guid)db().Parameters(mCommande, "@ID");
                        if (_isnew)
                        {
                            Numero = (string)db().Parameters(mCommande, "@Numero");
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
                throw new Exception(ex.Message + "\r\n" + "LotCoop:fnUpdate");

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
                    mCommande = db().CreateStoredProcCommand("V4_Lot_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V4_Lot_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, _Sites.ID);
                db().AddInParameter(mCommande, "@referenceCoop", SqlDbType.VarChar, ReferenceCoop);
                db().AddInParameter(mCommande, "@DateComposition", SqlDbType.DateTime, _DateComposition);
                db().AddInParameter(mCommande, "@description", SqlDbType.VarChar, _Commentaire);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, _TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNet);
                db().AddInParameter(mCommande, "@estCertifie", SqlDbType.Int, _EstCertifie);
                db().AddInParameter(mCommande, "@typeElementEnStock", SqlDbType.Int, _ElementEnStock.ID);
                db().AddInParameter(mCommande, "@TypeChargementID", SqlDbType.Int, _TypeChargement.ID);
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
                        ID = (Guid)db().Parameters(mCommande, "@ID");
                        if (_isnew)
                        {
                            Numero = (string)db().Parameters(mCommande, "@Numero");
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
                throw new Exception(ex.Message + "\r\n" + "LotCoop:fnUpdate");

            }
            return Result;
        }


        private static void MapFromDataReader(LotCoop mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass.ID = (Guid)mDataReader["ID"];

                    mClass.Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass.Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["SiteID"]))
                    {
                        mClass.Sites = new Site();
                        mClass.Sites.ID = (int)mDataReader["SiteID"];
                        mClass.Sites.Nom = (string)mDataReader["SiteNom"];
                    }
                                                           
                    if (!DBNull.Value.Equals(mDataReader["NumeroLot"])) mClass._Numero = (string)mDataReader["NumeroLot"];
                    if (!DBNull.Value.Equals(mDataReader["DateComposition"])) mClass._DateComposition = (DateTime)mDataReader["DateComposition"];
                    if (!DBNull.Value.Equals(mDataReader["DescriptionLot"])) mClass._Commentaire = (string)mDataReader["DescriptionLot"];
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass._NombreSacs = (int)mDataReader["NombreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass._NbreSacsAccepteInitial = (int)mDataReader["NombreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalette = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNet = (decimal)mDataReader["PoidsNet"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["ReferenceCoop"])) mClass._ReferenceCoop = (string)mDataReader["ReferenceCoop"];
                    if (!DBNull.Value.Equals(mDataReader["EstCertifie"])) mClass._EstCertifie = (int)mDataReader["EstCertifie"];
                    if (!DBNull.Value.Equals(mDataReader["EstCertifie"]))
                    {
                        mClass._Certification = new Certification();
                        mClass._Certification.ID = (int)mDataReader["EstCertifie"];
                        mClass._Certification.Designation = (string)mDataReader["CertificationNom"];
                    }
                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                    if (!DBNull.Value.Equals(mDataReader["ElementEnStockID"]))
                    {
                        mClass._ElementEnStock = new ElementEnStock();
                        mClass._ElementEnStock.ID = (int)mDataReader["ElementEnStockID"];
                        mClass._ElementEnStock.Designation = (string)mDataReader["ElementEnStock"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["TypeChargementID"]))
                    {
                        mClass._TypeChargement = new TypeChargement();
                        mClass._TypeChargement.ID = (int)mDataReader["TypeChargementID"];
                        mClass._TypeChargement.Designation = (string)mDataReader["TypeChargementLibelle"];
                        mClass._TypeChargement.DetailLivraison = (bool)mDataReader["DetailLivraison"];
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n LotCoop:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderLite(LotCoop mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass.Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass._Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["SiteID"]))
                    {
                        mClass._Sites = new Site();
                        mClass._Sites.ID = (int)mDataReader["SiteID"];
                        mClass._Sites.Nom = (string)mDataReader["SiteNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["NumeroLot"])) mClass._Numero = (string)mDataReader["NumeroLot"];
                    if (!DBNull.Value.Equals(mDataReader["ReferenceCoop"])) mClass.ReferenceCoop = (string)mDataReader["ReferenceCoop"];
                    if (!DBNull.Value.Equals(mDataReader["DateComposition"])) mClass._DateComposition = (DateTime)mDataReader["DateComposition"];                    
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass._NombreSacs = (int)mDataReader["NombreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalette = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNet = (decimal)mDataReader["PoidsNet"];                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n LotCoop:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderLite2(LotCoop mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass.Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass._Campagne.Designation = (string)mDataReader["CampagneID"];

                    //if (!DBNull.Value.Equals(mDataReader["SiteID"]))
                    //{
                    //    mClass._Sites = new Site();
                    //    mClass._Sites.ID = (int)mDataReader["SiteID"];
                    //    mClass._Sites.Nom = (string)mDataReader["SiteNom"];
                    //}

                    if (!DBNull.Value.Equals(mDataReader["NumeroLot"])) mClass._Numero = (string)mDataReader["NumeroLot"];
                    //if (!DBNull.Value.Equals(mDataReader["ReferenceCoop"])) mClass.ReferenceCoop = (string)mDataReader["ReferenceCoop"];

                    if (!DBNull.Value.Equals(mDataReader["DateLot"])) mClass._DateComposition = (DateTime)mDataReader["DateLot"];
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass._NombreSacs = (int)mDataReader["NombreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["NombrePalette"])) mClass._NombrePalettes = (int)mDataReader["NombrePalette"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalette = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNet = (decimal)mDataReader["PoidsNet"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n LotCoop:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderLite3(LotCoop mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;
                    
                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["IdTransfert"])) mClass._IDTransfert = (Guid)mDataReader["IdTransfert"];

                    mClass.Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass._Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["NumeroLot"])) mClass._Numero = (string)mDataReader["NumeroLot"];

                    mClass.Magasin = new Magasin();
                    if (!DBNull.Value.Equals(mDataReader["MagasinID"])) mClass._Magasin.ID = (int)mDataReader["MagasinID"];
                    if (!DBNull.Value.Equals(mDataReader["MagasinNom"])) mClass._Magasin.Designation = (string)mDataReader["MagasinNom"];

                    if (!DBNull.Value.Equals(mDataReader["NumeroTransfert"])) mClass._NumeroTransfert = (string)mDataReader["NumeroTransfert"];
                    if (!DBNull.Value.Equals(mDataReader["DateExpedition"])) mClass._DateExpedition = (DateTime)mDataReader["DateExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass._NombreSacs = (int)mDataReader["NombreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["NombrePalette"])) mClass._NombrePalettes = (int)mDataReader["NombrePalette"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalette = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNet = (decimal)mDataReader["PoidsNet"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n LotCoop:MapFromDataReader");
            }
        }


        public override string ToString()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public partial class LotCoopViewModel
    {
        public LotCoop _LotCoop { get; set; }

        public string _DefaultCampagne { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }


}
