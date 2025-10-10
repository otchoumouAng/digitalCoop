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
    public class TransfertFeves : DataPersist
    {
        #region fields
        private Guid _ID;
        private Campagne _Campagne;
        private Livraison _Livraison;
        private Site _Sites;
        private DateTime _DateTransfert;
        private Destination _Destination;
        private string _ReferenceObjet;
        private string _Numero;
        private int _NombreSacs;
        private decimal _PoidsBrut;
        private decimal _TareSacs;
        private decimal _TarePalette;
        private decimal _PoidsNet;
        private string _Statut;
        private string _Commentaire;
        private bool _Desactive;
        private decimal _PrixMoyen;
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

        public string Immatriculation
        {
            get { return _Livraison != null ? _Livraison.Immatriculation : string.Empty; }
        }

        public string CampagneAsString
        {
            get
            {
                return _Campagne != null ? _Campagne.Designation : string.Empty;
            }
        }

        public string SiteAsString
        {
            get
            {
                return _Sites != null ? _Sites.Nom : string.Empty;
            }
        }

        public DateTime DateTransfert
        {
            get
            {
                return _DateTransfert;
            }

            set
            {
                _DateTransfert = value;
            }
        }

        public string DateTransfertAsString
        {
            get { return _DateTransfert != null ? _DateTransfert.ToShortDateString() : string.Empty; }
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

        public Destination Destination
        {
            get
            {
                return _Destination;
            }

            set
            {
                _Destination = value;
            }
        }

        public string DestinationAsString
        {
            get
            {
                return _Destination != null ? _Destination.Nom : string.Empty;
            }            
        }

        public decimal PrixMoyen
        {
            get
            {
                return _PrixMoyen;
            }

            set
            {
                _PrixMoyen = value;
            }
        }

        public string PrixMoyenAsString
        {
            get
            {
                return _PrixMoyen != 0 ? string.Format("{0:#,#}", _PrixMoyen).TrimStart() : "0";
            }
        }
        #endregion


        #region Constructor
        public TransfertFeves()
        {

        }

        public TransfertFeves(Guid myId)
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
            DataCommand mCommande = db().CreateStoredProcCommand("V3_TransfertFeves_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "TransfertFeves:fnDeActivate");
            }
            return bolResult;
        }

        public List<DataPersist> fnSelectTransferFeveForPurchseDeliveries(int Fournisseur)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_SelectTransfertFevesForPurchaseDeliveries");
                db().AddInParameter(mCommande, "@Fournisseur", SqlDbType.Int, Fournisseur);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    TransfertFeves mClass = new TransfertFeves();
                    MapFromDataReaderLiteForPurchaseDeliveries(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectLotForShipment");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public bool fnApprove()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V3_TransfertFeves_Approve");
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
                throw new Exception(ex.Message + "\r\n" + "TransfertFeves:fnApprove");
            }
            return bolResult;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V3_TransfertFeves_Get", (Guid)Id);
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
            return fnSelect("{Tous}", null, null, "-1", -1);
        }

        public virtual List<DataPersist> fnSelect(string CampagneID, DateTime? StartDate, DateTime? EndDate, string status, int SiteID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_TransfertFeves_Select");

                db().AddInParameter(mCommande, "@CropYear", SqlDbType.Char, 9, CampagneID);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, SiteID);
                db().AddInParameter(mCommande, "@StartDate", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@EndDate", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, status);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    TransfertFeves mClass = new TransfertFeves();

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
                DataCommand mCommande = db().CreateStoredProcCommand("V3_TransfertFeves_SelectForLivraison");

                db().AddInParameter(mCommande, "@CropYear", SqlDbType.Char, 9, CampagneID);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, SiteID);
                db().AddInParameter(mCommande, "@StartDate", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@EndDate", SqlDbType.DateTime, EndDate);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    TransfertFeves mClass = new TransfertFeves();

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
                    mCommande = db().CreateStoredProcCommand("V3_TransfertFeves_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V3_TransfertFeves_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, _Sites.ID);
                db().AddInParameter(mCommande, "@DestinationID", SqlDbType.Int, _Destination.ID);
                db().AddInParameter(mCommande, "@DateTransfert", SqlDbType.DateTime, _DateTransfert);
                db().AddInParameter(mCommande, "@description", SqlDbType.VarChar, _Commentaire);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, _TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNet);
                db().AddInParameter(mCommande, "@PrixMoyen", SqlDbType.Decimal, _PrixMoyen);

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
                throw new Exception(ex.Message + "\r\n" + "TransfertFeves:fnUpdate");

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
                    mCommande = db().CreateStoredProcCommand("V3_TransfertFeves_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V3_TransfertFeves_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, _Sites.ID);
                db().AddInParameter(mCommande, "@DestinationID", SqlDbType.Int, _Destination.ID);
                db().AddInParameter(mCommande, "@DateTransfert", SqlDbType.DateTime, _DateTransfert);
                db().AddInParameter(mCommande, "@description", SqlDbType.VarChar, _Commentaire);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, _TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNet);
                db().AddInParameter(mCommande, "@PrixMoyen", SqlDbType.Decimal, _PrixMoyen);

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
                throw new Exception(ex.Message + "\r\n" + "TransfertFeves:fnUpdate");

            }
            return Result;
        }


        private static void MapFromDataReader(TransfertFeves mClass, IDataReader mDataReader)
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

                    if (!DBNull.Value.Equals(mDataReader["DestinationID"]))
                    {
                        mClass.Destination = new Destination();
                        mClass.Destination.ID = (int)mDataReader["DestinationID"];
                        mClass.Destination.Nom = (string)mDataReader["DestinationNom"];
                    }                                        

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DateTransfert"])) mClass._DateTransfert = (DateTime)mDataReader["DateTransfert"];
                    if (!DBNull.Value.Equals(mDataReader["DescriptionTransfert"])) mClass._Commentaire = (string)mDataReader["DescriptionTransfert"];
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass._NombreSacs = (int)mDataReader["NombreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalette = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNet = (decimal)mDataReader["PoidsNet"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                    if (!DBNull.Value.Equals(mDataReader["PrixMoyen"])) mClass._PrixMoyen = (decimal)mDataReader["PrixMoyen"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n TransfertFeves:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderLiteForPurchaseDeliveries(TransfertFeves mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) 
                        mClass._ID = (Guid)mDataReader["ID"];

                    mClass.Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"]))
                        mClass._Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["SiteID"]))
                    {
                        mClass._Sites = new Site();
                        mClass._Sites.Nom = (string)mDataReader["SiteNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["NumeroTransfert"]))
                        mClass._Numero = (string)mDataReader["NumeroTransfert"];

                    if (!DBNull.Value.Equals(mDataReader["DateLivraison"]))
                        mClass._DateTransfert = (DateTime)mDataReader["DateLivraison"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n TransfertFeves:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderLite(TransfertFeves mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass.Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) 
                        mClass._Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["SiteID"]))
                    {
                        mClass._Sites = new Site();
                        mClass._Sites.ID = (int)mDataReader["SiteID"];
                        mClass._Sites.Nom = (string)mDataReader["SiteNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["DestinationID"]))
                    {
                        mClass._Destination = new Destination();
                        mClass._Destination.ID = (int)mDataReader["DestinationID"];
                        mClass._Destination.Nom = (string)mDataReader["DestinationNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) 
                        mClass._Numero = (string)mDataReader["Numero"];

                    if (!DBNull.Value.Equals(mDataReader["DateTransfert"])) 
                        mClass._DateTransfert = (DateTime)mDataReader["DateTransfert"]; 
                    
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) 
                        mClass._NombreSacs = (int)mDataReader["NombreSacs"];

                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) 
                        mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];

                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) 
                        mClass._TareSacs = (decimal)mDataReader["TareSacs"];

                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) 
                        mClass._TarePalette = (decimal)mDataReader["TarePalettes"];

                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) 
                        mClass._PoidsNet = (decimal)mDataReader["PoidsNet"];

                    if (!DBNull.Value.Equals(mDataReader["PrixMoyen"])) 
                        mClass._PrixMoyen = (decimal)mDataReader["PrixMoyen"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n TransfertFeves:MapFromDataReader");
            }
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public partial class TransfertFevesViewModel
    {
        public TransfertFeves _TransfertFeves { get; set; }

        public string _DefaultCampagne { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }


}
