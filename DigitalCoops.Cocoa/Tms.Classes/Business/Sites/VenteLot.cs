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
    public class VenteLot : DataPersist
    {
        #region fields
        private Guid _ID;
        private Campagne _Campagne;
        private Exportateur _Exportateur;
        private Magasin _Magasin;
        private Magasin _MagasinExpedition;
        private Magasin _MagasinReception;
        private LotCoop _Lot;
        private string _NumeroExpedition;
        private string _CommentaireReception;
        private string _ImmTracteurExpedition;
        private string _ImmRemorqueExpedition;
        private string _ImmTracteurReception;
        private string _ImmRemorqueReception;
        //private Magasin _MagasinDestination;
        private Site _Sites;
        private DateTime _DateVente;
        private DateTime _DateExpedition;
        private DateTime _DateReception;
        private Destination _Destination;
        private string _Numero;
        private string _NumeroLot;
        private string _NumBordereauSortie;
        private string _NumBordereauReception;
        private int _NombreSacs;
        private int _NombreSacsReception;
        private int _NombrePalette;
        private int _NombrePaletteReception;
        private decimal _PoidsBrut;
        private decimal _PoidsBrutReception;
        private decimal _TareSacs;
        private decimal _TareSacsReception;
        private decimal _TareSacsArrive;
        private decimal _TarePalette;
        private decimal _TarePaletteArrive;
        private decimal _PoidsNet;
        private decimal _PoidsNetRecetpion;
        private string _Statut;
        private string _Commentaire;
        private bool _Desactive;
        private decimal _Refaction;
        private decimal _PoidsNetArrive;
        private string _NumCCC;
        private int _SacExportateur;
        private decimal _PoidsBrutExportateur;
        private decimal _PoidsNetExportateur;
        private decimal _Freinte;
        private string _UserApprobation;
        private DateTime _DateApprobation;
        private string _Immatriculation;
        private string _ImmRemorque;
        private decimal _PrixMoyen;
        private decimal _ValuerVente;
        private decimal _PrixExportateur;
        private decimal _PoidsProduitAccepte;
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

        public DateTime DateVente
        {
            get
            {
                return _DateVente;
            }

            set
            {
                _DateVente = value;
            }
        }

        public string DateVenteAsString
        {
            get { return _DateVente != null ? _DateVente.ToShortDateString() : string.Empty; }
        }

        public string NumLot
        {
            get { return _Lot != null ? _Lot.Numero : string.Empty; }
        }

        public string DateExpeditionAsString
        {
            get { return _DateExpedition != null ? _DateExpedition.ToShortDateString() : string.Empty; }
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
        public string NumBordereauSortie
        {
            get
            {
                return _NumBordereauSortie;
            }

            set
            {
                _NumBordereauSortie = value;
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

        public string TareSacsArriveAsString
        {
            get
            {
                return _TareSacsArrive != 0 ? string.Format("{0:#,#}", _TareSacsArrive).TrimStart() : "0";
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

        public string PrixExportateurAsString
        {
            get
            {
                return _PrixExportateur != 0 ? string.Format("{0:#,#}", _PrixExportateur).TrimStart() : "0";
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
                return (_Statut == "AP");
            }
        }

        public bool VenteEstApprouve
        {
            get
            {
                return (_Statut == "VE" || _Destination != null);
            }
        }

        public bool EstReceptionne
        {
            get
            {
                return (_MagasinReception != null);
            }
        }

        public bool PeuxFinaliser
        {
            get
            {
                return (_Destination != null);
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
                else if (_Statut == "VE")
                    return 3; // 
                else if (_Statut == "CA")
                    return 0; //      
                else if (_Statut == "RE")
                    return 4; //                                 
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

        public string SiteAsString
        {
            get
            {
                return _Sites != null ? _Sites.Nom : string.Empty;
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

        public DateTime DateReception
        {
            get
            {
                return _DateReception;
            }

            set
            {
                _DateReception = value;
            }
        }

        public string DateReceptionAsString
        {
            get { return _DateReception != null ? _DateReception.ToShortDateString() : string.Empty; }
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

        public decimal PoidsNetArrive
        {
            get
            {
                return _PoidsNetArrive;
            }

            set
            {
                _PoidsNetArrive = value;
            }
        }
        public string PoidsNetArriveAsString
        {
            get
            {
                return _PoidsNetArrive != 0 ? string.Format("{0:#,#}", PoidsNetArrive).TrimStart() : "0";
            }
        }

        public string NumCCC
        {
            get
            {
                return _NumCCC;
            }

            set
            {
                _NumCCC = value;
            }
        }

        public decimal PoidsBrutExportateur
        {
            get
            {
                return _PoidsBrutExportateur;
            }

            set
            {
                _PoidsBrutExportateur = value;
            }
        }

        public string PoidsBrutExportateurAsString
        {
            get
            {
                return _PoidsBrutExportateur != 0 ? string.Format("{0:#,#}", _PoidsBrutExportateur).TrimStart() : "0";
            }
        }

        public string RefactionAsString
        {
            get
            {
                return _Refaction != 0 ? string.Format("{0:#,#}", _Refaction).TrimStart() : "0";
            }
        }

        public decimal PoidsNetExportateur
        {
            get
            {
                return _PoidsNetExportateur;
            }

            set
            {
                _PoidsNetExportateur = value;
            }
        }

        public decimal Freinte
        {
            get
            {
                return _Freinte;
            }

            set
            {
                _Freinte = value;
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

        public string MagasinExpeditionAsString
        {
            get
            {
                return _MagasinExpedition != null ? _MagasinExpedition.Designation : string.Empty;
            }
        }

        public string MagasinReceptionAsString
        {
            get
            {
                return _MagasinReception != null ? _MagasinReception.Designation : string.Empty;
            }
        }
        //public Magasin MagasinDestination
        //{
        //    get
        //    {
        //        return _MagasinDestination;
        //    }

        //    set
        //    {
        //        _MagasinDestination = value;
        //    }
        //}

        //public string MagasinDestinationAsString
        //{
        //    get
        //    {
        //        return _MagasinDestination != null ? _MagasinDestination.Designation : string.Empty;
        //    }


        //}

        public string UserApprobation
        {
            get
            {
                return _UserApprobation;
            }

            set
            {
                _UserApprobation = value;
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

        public int SacExportateur
        {
            get
            {
                return _SacExportateur;
            }

            set
            {
                _SacExportateur = value;
            }
        }

        public string SacExportateurAsString
        {
            get
            {
                return _SacExportateur != 0 ? string.Format("{0:#,#}", _SacExportateur).TrimStart() : "0";
            }
        }

        public string Immatriculation
        {
            get
            {
                return _Immatriculation;
            }

            set
            {
                _Immatriculation = value;
            }
        }

        public string ImmRemorque
        {
            get
            {
                return _ImmRemorque;
            }

            set
            {
                _ImmRemorque = value;
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

        public decimal ValuerVente
        {
            get
            {
                return _ValuerVente;
            }

            set
            {
                _ValuerVente = value;
            }
        }

        public decimal TareSacsArrive
        {
            get
            {
                return _TareSacsArrive;
            }

            set
            {
                _TareSacsArrive = value;
            }
        }

        //public string TareSacsArriveAsString
        //{
        //    get
        //    {
        //        return _TareSacsArrive != 0 ? string.Format("{0:#,#}", _TareSacsArrive).TrimStart() : "0";
        //    }
        //}

        public decimal PrixExportateur
        {
            get
            {
                return _PrixExportateur;
            }

            set
            {
                _PrixExportateur = value;
            }
        }

        public decimal PoidsProduitAccepte
        {
            get
            {
                return _PoidsProduitAccepte;
            }

            set
            {
                _PoidsProduitAccepte = value;
            }
        }

        public string PoidsProduitAccepteAsString
        {
            get
            {
                return _PoidsProduitAccepte != 0 ? string.Format("{0:#,#}", _PoidsProduitAccepte).TrimStart() : "0";
            }
        }

        public decimal TarePaletteArrive
        {
            get
            {
                return _TarePaletteArrive;
            }

            set
            {
                _TarePaletteArrive = value;
            }
        }
        public string TarePaletteArriveAsString
        {
            get
            {
                return _TarePaletteArrive != 0 ? string.Format("{0:#,#}", _TarePaletteArrive).TrimStart() : "0";
            }
        }

        public string NumeroLot
        {
            get
            {
                return _NumeroLot;
            }

            set
            {
                _NumeroLot = value;
            }
        }

        public int NombrePalette
        {
            get
            {
                return _NombrePalette;
            }

            set
            {
                _NombrePalette = value;
            }
        }

        public LotCoop Lot
        {
            get
            {
                return _Lot;
            }

            set
            {
                _Lot = value;
            }
        }

        //public string ExportateurAsString
        //{
        //    get
        //    {
        //        return _Exportateur != null ? _Exportateur.Nom : string.Empty;
        //    }
        //}

        public string NumeroExpedition
        {
            get
            {
                return _NumeroExpedition;
            }

            set
            {
                _NumeroExpedition = value;
            }
        }

        public int NombreSacsReception
        {
            get
            {
                return _NombreSacsReception;
            }

            set
            {
                _NombreSacsReception = value;
            }
        }

        public int NombrePaletteReception
        {
            get
            {
                return _NombrePaletteReception;
            }

            set
            {
                _NombrePaletteReception = value;
            }
        }

        public decimal PoidsBrutRecetpion
        {
            get
            {
                return PoidsBrutReception;
            }

            set
            {
                PoidsBrutReception = value;
            }
        }

        public decimal PoidsNetRecetpion
        {
            get
            {
                return _PoidsNetRecetpion;
            }

            set
            {
                _PoidsNetRecetpion = value;
            }
        }

        public string CommentaireReception
        {
            get
            {
                return _CommentaireReception;
            }

            set
            {
                _CommentaireReception = value;
            }
        }

        public string ImmTracteurExpedition1
        {
            get
            {
                return _ImmTracteurExpedition;
            }

            set
            {
                _ImmTracteurExpedition = value;
            }
        }

        public string ImmRemorqueExpedition1
        {
            get
            {
                return _ImmRemorqueExpedition;
            }

            set
            {
                _ImmRemorqueExpedition = value;
            }
        }

        public string ImmTracteurReception
        {
            get
            {
                return _ImmTracteurReception;
            }

            set
            {
                _ImmTracteurReception = value;
            }
        }

        public string ImmRemorqueReception
        {
            get
            {
                return _ImmRemorqueReception;
            }

            set
            {
                _ImmRemorqueReception = value;
            }
        }

        public string NumBordereauReception
        {
            get
            {
                return _NumBordereauReception;
            }

            set
            {
                _NumBordereauReception = value;
            }
        }

        public Magasin MagasinExpedition
        {
            get
            {
                return _MagasinExpedition;
            }

            set
            {
                _MagasinExpedition = value;
            }
        }

        public Magasin MagasinReception
        {
            get
            {
                return _MagasinReception;
            }

            set
            {
                _MagasinReception = value;
            }
        }

        public decimal TareSacsReception
        {
            get
            {
                return _TareSacsReception;
            }

            set
            {
                _TareSacsReception = value;
            }
        }

        public decimal PoidsBrutReception
        {
            get
            {
                return _PoidsBrutReception;
            }

            set
            {
                _PoidsBrutReception = value;
            }
        }
        #endregion

        #region Constructor
        public VenteLot()
        {

        }

        public VenteLot(Guid myId)
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
            DataCommand mCommande = db().CreateStoredProcCommand("V4_VenteLot_Cancel");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
            db().AddInParameter(mCommande, "@CancelUser", SqlDbType.VarChar, _UtilisateurModification);
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

        public bool fnApprove()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V4_VenteLot_Approve");
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
                mDataReader = db().ExecuteReader("V5_Transfert_Lot_Get ", (Guid)Id);
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
        public bool fnGetReception(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V5_Transfert_Lot_Get ", (Guid)Id);
                if (mDataReader.Read())
                {
                    MapFromDataReaderReception(this, mDataReader);
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
                DataCommand mCommande = db().CreateStoredProcCommand("V4_VenteLot_Select");

                db().AddInParameter(mCommande, "@CropYear", SqlDbType.Char, 9, CampagneID);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, SiteID);
                db().AddInParameter(mCommande, "@StartDate", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@EndDate", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, status);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    VenteLot mClass = new VenteLot();

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

        public virtual List<DataPersist> fnSelectListTransfer(string CampagneID, DateTime? StartDate, DateTime? EndDate, string status, int SiteID, int MagasinExpeditionID, int MagasinReceptionID, int ExportateurID, int EnTransit = 0)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V5_Transfert_Lot_Select");

                db().AddInParameter(mCommande, "@CropYear", SqlDbType.Char, 9, CampagneID);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, SiteID);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@MagasinReceptionID", SqlDbType.Int, MagasinReceptionID);
                db().AddInParameter(mCommande, "@MagasinExpeditionID", SqlDbType.Int, MagasinExpeditionID);
                db().AddInParameter(mCommande, "@StartDate", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@EndDate", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, status);
                db().AddInParameter(mCommande, "@EnTransit", SqlDbType.Int, EnTransit);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    VenteLot mClass = new VenteLot();

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
                DataCommand mCommande = db().CreateStoredProcCommand("V4_VenteLot_SelectForLivraison");

                db().AddInParameter(mCommande, "@CropYear", SqlDbType.Char, 9, CampagneID);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, SiteID);
                db().AddInParameter(mCommande, "@StartDate", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@EndDate", SqlDbType.DateTime, EndDate);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    VenteLot mClass = new VenteLot();

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
                    mCommande = db().CreateStoredProcCommand("V4_VenteLot_Entete_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 8);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V4_VenteLot_Entete_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, _Sites.ID);
                db().AddInParameter(mCommande, "@exportateur", SqlDbType.Int, _Exportateur.ID);
                db().AddInParameter(mCommande, "@magasin", SqlDbType.Int, _Magasin.ID);
                //db().AddInParameter(mCommande, "@magasinDestination", SqlDbType.Int, _MagasinDestination.ID);
                db().AddInParameter(mCommande, "@dateVente", SqlDbType.DateTime, _DateVente);
                db().AddInParameter(mCommande, "@dateExpedition", SqlDbType.DateTime, _DateExpedition);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);
                db().AddInParameter(mCommande, "@NumeroCCC", SqlDbType.VarChar, _NumCCC);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@poidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNet);
                db().AddInParameter(mCommande, "@PrixMoyen", SqlDbType.Decimal, _PrixMoyen);
                db().AddInParameter(mCommande, "@Immatriculation", SqlDbType.VarChar, 50, _Immatriculation);
                db().AddInParameter(mCommande, "@ImmRemorque", SqlDbType.VarChar, 50, _ImmRemorque);
                db().AddInParameter(mCommande, "@NumBordereauSortie", SqlDbType.VarChar, 50, _NumBordereauSortie);

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

        public bool fnUpdateExpedition()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {
                    mCommande = db().CreateStoredProcCommand("V5_Transfert_Lot_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@numeroExpedition", SqlDbType.VarChar, 8);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V5_Transfert_Lot_New");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, _Campagne.Designation);
                //db().AddInParameter(mCommande, "@numerolot", SqlDbType.Char, 10, _NumeroLot);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, _Sites.ID);
                db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, _Lot.ID);
                //db().AddInParameter(mCommande, "@exportateur", SqlDbType.Int, _Exportateur.ID);
                db().AddInParameter(mCommande, "@magasinExpeditionID", SqlDbType.Int, _Magasin.ID);
                //db().AddInParameter(mCommande, "@magasinDestination", SqlDbType.Int, _MagasinDestination.ID);
                //db().AddInParameter(mCommande, "@dateVente", SqlDbType.DateTime, _DateVente);
                db().AddInParameter(mCommande, "@dateExpedition", SqlDbType.DateTime, _DateExpedition);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);
                //db().AddInParameter(mCommande, "@NumeroCCC", SqlDbType.VarChar, _NumCCC);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, _Statut);
                db().AddInParameter(mCommande, "@nombreSacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@NombrePalette", SqlDbType.Int, _NombrePalette);
                db().AddInParameter(mCommande, "@poidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@poidsNet", SqlDbType.Decimal, _PoidsNet);
                db().AddInParameter(mCommande, "@TareSac", SqlDbType.Decimal, _TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, _TarePalette);
                //db().AddInParameter(mCommande, "@PrixMoyen", SqlDbType.Decimal, _PrixMoyen);
                db().AddInParameter(mCommande, "@ImmTracteur", SqlDbType.VarChar, 50, _Immatriculation);
                db().AddInParameter(mCommande, "@ImmRemorque", SqlDbType.VarChar, 50, _ImmRemorque);
                db().AddInParameter(mCommande, "@NumBordereauExpedition", SqlDbType.VarChar, 50, _NumBordereauSortie);

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
                            Numero = (string)db().Parameters(mCommande, "@numeroExpedition");
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

        public bool fnUpdateExpedition(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {
                    mCommande = db().CreateStoredProcCommand("V5_Transfert_Lot_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@numeroExpedition", SqlDbType.VarChar, 8);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V5_Transfert_Lot_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, _Campagne.Designation);
                //db().AddInParameter(mCommande, "@numerolot", SqlDbType.Char, 10, _NumeroLot);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, _Sites.ID);
                db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, _Lot.ID);
                //db().AddInParameter(mCommande, "@exportateur", SqlDbType.Int, _Exportateur.ID);
                db().AddInParameter(mCommande, "@magasinExpeditionID", SqlDbType.Int, _Magasin.ID);
                db().AddInParameter(mCommande, "@magasinTheoReceptionID", SqlDbType.Int, _MagasinReception.ID);
                //db().AddInParameter(mCommande, "@magasinDestination", SqlDbType.Int, _MagasinDestination.ID);
                //db().AddInParameter(mCommande, "@dateVente", SqlDbType.DateTime, _DateVente);
                db().AddInParameter(mCommande, "@dateExpedition", SqlDbType.DateTime, _DateExpedition);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);
                //db().AddInParameter(mCommande, "@NumeroCCC", SqlDbType.VarChar, _NumCCC);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, _Statut);
                db().AddInParameter(mCommande, "@nombreSacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@NombrePalette", SqlDbType.Int, _NombrePalette);
                db().AddInParameter(mCommande, "@poidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@poidsNet", SqlDbType.Decimal, _PoidsNet);
                db().AddInParameter(mCommande, "@TareSac", SqlDbType.Decimal, _TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, _TarePalette);
                //db().AddInParameter(mCommande, "@PrixMoyen", SqlDbType.Decimal, _PrixMoyen);
                db().AddInParameter(mCommande, "@ImmTracteur", SqlDbType.VarChar, 50, _ImmTracteurExpedition);
                db().AddInParameter(mCommande, "@ImmRemorque", SqlDbType.VarChar, 50, _ImmRemorqueExpedition);
                db().AddInParameter(mCommande, "@NumBordereauExpedition", SqlDbType.VarChar, 50, _NumBordereauSortie);

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
                            Numero = (string)db().Parameters(mCommande, "@numeroExpedition");
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
                    mCommande = db().CreateStoredProcCommand("V4_VenteLot_Entete_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V4_VenteLot_Entete_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, _Sites.ID);
                db().AddInParameter(mCommande, "@exportateur", SqlDbType.Int, _Exportateur.ID);
                db().AddInParameter(mCommande, "@magasin", SqlDbType.Int, _Magasin.ID);
                //db().AddInParameter(mCommande, "@magasinDestination", SqlDbType.Int, _MagasinDestination.ID);
                db().AddInParameter(mCommande, "@dateVente", SqlDbType.DateTime, _DateVente);
                db().AddInParameter(mCommande, "@dateExpedition", SqlDbType.DateTime, _DateExpedition);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);
                db().AddInParameter(mCommande, "@NumeroCCC", SqlDbType.VarChar, _NumCCC);
                db().AddInParameter(mCommande, "@statut", SqlDbType.Char, 2, _Statut);

                db().AddInParameter(mCommande, "@numerolot", SqlDbType.VarChar, NumeroLot);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@poidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNet);
                db().AddInParameter(mCommande, "@PrixMoyen", SqlDbType.Decimal, _PrixMoyen);
                db().AddInParameter(mCommande, "@Immatriculation", SqlDbType.VarChar, 50, _Immatriculation);
                db().AddInParameter(mCommande, "@ImmRemorque", SqlDbType.VarChar, 50, _ImmRemorque);
                db().AddInParameter(mCommande, "@NumBordereauSortie", SqlDbType.VarChar, 50, _NumBordereauSortie);

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

        public bool fnUpdateReception()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("V4_VenteLot_Reception_Modify");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

                db().AddInParameter(mCommande, "@destinationID", SqlDbType.Int, _Destination.ID);
                db().AddInParameter(mCommande, "@dateReception", SqlDbType.DateTime, _DateReception);
                db().AddInParameter(mCommande, "@poidsbrutExportateur", SqlDbType.Decimal, _PoidsBrutExportateur);
                db().AddInParameter(mCommande, "@totalRefaction", SqlDbType.Decimal, _Refaction);
                db().AddInParameter(mCommande, "@poidsNetRecu", SqlDbType.Decimal, _PoidsNetArrive);
                db().AddInParameter(mCommande, "@tareSacRecu", SqlDbType.Decimal, _TareSacsArrive);
                db().AddInParameter(mCommande, "@freinte", SqlDbType.Decimal, _Freinte);
                db().AddInParameter(mCommande, "@PrixExportateur", SqlDbType.Decimal, _PrixExportateur);
                db().AddInParameter(mCommande, "@PoidsProduitAccepte", SqlDbType.Decimal, _PoidsProduitAccepte);
                db().AddInParameter(mCommande, "@sacexportateur", SqlDbType.Int, _SacExportateur);
                db().AddInParameter(mCommande, "@TarePaletteArrive", SqlDbType.Decimal, _TarePaletteArrive);
                db().AddInParameter(mCommande, "@statut", SqlDbType.VarChar, _Statut);

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

        public bool fnUpdateReception(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("V5_TransfertLot_Reception_Modify");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

                db().AddInParameter(mCommande, "@destinationID", SqlDbType.Int, _MagasinReception.ID);
                db().AddInParameter(mCommande, "@dateReception", SqlDbType.DateTime, _DateReception);

                db().AddInParameter(mCommande, "@nombresac", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@nombrepalette", SqlDbType.Int, _NombrePaletteReception);
                db().AddInParameter(mCommande, "@poidsNetRecu", SqlDbType.Decimal, _PoidsNetRecetpion);
                db().AddInParameter(mCommande, "@tareSacRecu", SqlDbType.Decimal, _TareSacsArrive);
                db().AddInParameter(mCommande, "@poidsbrut", SqlDbType.Decimal, PoidsBrutReception);
                db().AddInParameter(mCommande, "@TarePaletteArrive", SqlDbType.Decimal, _TarePaletteArrive);
                db().AddInParameter(mCommande, "@statut", SqlDbType.VarChar, _Statut);

                db().AddInParameter(mCommande, "@ImmTracteurRec", SqlDbType.VarChar, _ImmTracteurReception);
                db().AddInParameter(mCommande, "@ImmRemorqueRec", SqlDbType.VarChar, _ImmRemorqueReception);
                db().AddInParameter(mCommande, "@numBordereauRec", SqlDbType.VarChar, _NumBordereauReception);
                db().AddInParameter(mCommande, "@CommentaireRec", SqlDbType.VarChar, _CommentaireReception);

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

        public bool fnApproveVente()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("V4_VenteLot_Statut_Modify");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);

                db().AddInParameter(mCommande, "@statut", SqlDbType.VarChar, _Statut);
                db().AddInParameter(mCommande, "@ApprobationUser", SqlDbType.VarChar, _UserApprobation);
                db().AddInParameter(mCommande, "@DateApprobation", SqlDbType.DateTime, _DateApprobation);

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

        public bool fnApproveVente(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("V4_VenteLot_Statut_Modify");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);

                db().AddInParameter(mCommande, "@statut", SqlDbType.VarChar, _Statut);
                db().AddInParameter(mCommande, "@ApprobationUser", SqlDbType.VarChar, _UserApprobation);
                db().AddInParameter(mCommande, "@DateApprobation", SqlDbType.DateTime, _DateApprobation);

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

        public bool fnApproveReception()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("V4_VenteLot_Reception_Approve");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                db().AddInParameter(mCommande, "@ApprobationUser", SqlDbType.VarChar, _UserApprobation);
                db().AddInParameter(mCommande, "@DateApprobation", SqlDbType.DateTime, _DateApprobation);

                db().AddInParameter(mCommande, "@statut", SqlDbType.VarChar, _Statut);

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

        private static void MapFromDataReader(VenteLot mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass.ID = (Guid)mDataReader["ID"];

                    mClass._Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass.Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["SiteID"]))
                    {
                        mClass._Sites = new Site();
                        mClass._Sites.ID = (int)mDataReader["SiteID"];
                        mClass._Sites.Nom = (string)mDataReader["SiteNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["LotID"]))
                    {
                        mClass._Lot = new LotCoop();
                        mClass._Lot.ID = (Guid)mDataReader["LotID"];
                        mClass._Lot.Numero = (string)mDataReader["NumeroLot"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ExportateurID"]))
                    {
                        mClass._Exportateur = new Exportateur();
                        mClass._Exportateur.ID = (int)mDataReader["ExportateurID"];
                        mClass._Exportateur.Nom = (string)mDataReader["ExportateurNom"];
                    }

                    //if (!DBNull.Value.Equals(mDataReader["DestinationID"]))
                    //{
                    //    mClass._Destination = new Destination();
                    //    mClass._Destination.ID = (int)mDataReader["DestinationID"];
                    //    mClass._Destination.Nom = (string)mDataReader["DestinationNom"];
                    //}

                    //if (!DBNull.Value.Equals(mDataReader["ExportateurID"]))
                    //{
                    //    mClass._Exportateur = new Exportateur();
                    //    mClass._Exportateur.ID = (int)mDataReader["ExportateurID"];
                    //    mClass._Exportateur.Nom = (string)mDataReader["ExportateurNom"];
                    //}

                    if (!DBNull.Value.Equals(mDataReader["MagasinExpeditionID"]))
                    {
                        mClass._Magasin = new Magasin();
                        mClass._Magasin.ID = (int)mDataReader["MagasinExpeditionID"];
                        mClass._Magasin.Designation = (string)mDataReader["MagasinExpeditionNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["MagasinExpeditionID"]))
                    {
                        mClass._MagasinExpedition = new Magasin();
                        mClass._MagasinExpedition.ID = (int)mDataReader["MagasinExpeditionID"];
                        mClass._MagasinExpedition.Designation = (string)mDataReader["MagasinExpeditionNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["MagasinReceptionID"]))
                    {
                        mClass._MagasinReception = new Magasin();
                        mClass._MagasinReception.ID = (int)mDataReader["MagasinReceptionID"];
                        mClass._MagasinReception.Designation = (string)mDataReader["MagasinReceptionNom"];
                    }
                    //if (!DBNull.Value.Equals(mDataReader["MagasinDestinationID"]))
                    //{
                    //    mClass._MagasinDestination.ID = (int)mDataReader["MagasinDestinationID"];
                    //    mClass._MagasinDestination.Designation = (string)mDataReader["MagasinDestiantionNom"];
                    //}
                    if (!DBNull.Value.Equals(mDataReader["NumeroExpedition"])) mClass._Numero = (string)mDataReader["NumeroExpedition"];

                    //if (!DBNull.Value.Equals(mDataReader["DateVente"])) mClass._DateVente = (DateTime)mDataReader["DateVente"];
                    if (!DBNull.Value.Equals(mDataReader["DateExpedition"])) mClass._DateExpedition = (DateTime)mDataReader["DateExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["DateReception"])) mClass._DateReception = (DateTime)mDataReader["DateReception"];

                    if (!DBNull.Value.Equals(mDataReader["CommentaireExpedition"])) mClass._Commentaire = (string)mDataReader["CommentaireExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["CommentaireReception"])) mClass._CommentaireReception = (string)mDataReader["CommentaireReception"];
                    if (!DBNull.Value.Equals(mDataReader["NumBordereauExpedition"])) mClass._NumeroExpedition = (string)mDataReader["NumBordereauExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["NumBordereauReception"])) mClass._NumBordereauReception = (string)mDataReader["NumBordereauReception"];

                    if (!DBNull.Value.Equals(mDataReader["NombreSacsExpedition"])) mClass._NombreSacs = (int)mDataReader["NombreSacsExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["NombreSacsReception"])) mClass._NombreSacsReception = (int)mDataReader["NombreSacsReception"];

                    if (!DBNull.Value.Equals(mDataReader["NombrePaletteExpedition"])) mClass._NombrePalette = (int)mDataReader["NombrePaletteExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["NombrePaletteReception"])) mClass._NombrePaletteReception = (int)mDataReader["NombrePaletteReception"];

                    if (!DBNull.Value.Equals(mDataReader["TareSacsExpedition"])) mClass._TareSacs = (decimal)mDataReader["TareSacsExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacsReception"])) mClass.TareSacsReception = (decimal)mDataReader["TareSacsReception"];

                    if (!DBNull.Value.Equals(mDataReader["TarePaletteExpedition"])) mClass._TarePalette = (decimal)mDataReader["TarePaletteExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["TarePaletteReception"])) mClass._TarePaletteArrive = (decimal)mDataReader["TarePaletteReception"];

                    if (!DBNull.Value.Equals(mDataReader["PoidsBrutExpedition"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrutExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrutReception"])) mClass.PoidsBrutReception = (decimal)mDataReader["PoidsBrutReception"];

                    //if (!DBNull.Value.Equals(mDataReader["RefactionExportateur"])) mClass._Refaction = (decimal)mDataReader["RefactionExportateur"];

                    if (!DBNull.Value.Equals(mDataReader["PoidsNetExpedition"])) mClass._PoidsNet = (decimal)mDataReader["PoidsNetExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetReception"])) mClass._PoidsNetArrive = (decimal)mDataReader["PoidsNetReception"];

                    //if (!DBNull.Value.Equals(mDataReader["PoidsBrutExportateur"])) mClass._PoidsBrutExportateur = (decimal)mDataReader["PoidsBrutExportateur"];
                    //if (!DBNull.Value.Equals(mDataReader["PoidsNetExportateur"])) mClass._PoidsNetExportateur = (decimal)mDataReader["PoidsNetExportateur"];
                    //if (!DBNull.Value.Equals(mDataReader["Freinte"])) mClass._Freinte = (decimal)mDataReader["Freinte"];

                    //if (!DBNull.Value.Equals(mDataReader["TareSacsArrive"])) mClass._TareSacsArrive = (decimal)mDataReader["TareSacsArrive"];

                    //if (!DBNull.Value.Equals(mDataReader["SacExportateur"])) mClass._SacExportateur = (int)mDataReader["SacExportateur"];

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                    if (!DBNull.Value.Equals(mDataReader["ImmTracteurExpedition"])) mClass._ImmTracteurExpedition = (string)mDataReader["ImmTracteurExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["ImmRemorqueExpedition"])) mClass._ImmRemorqueExpedition = (string)mDataReader["ImmRemorqueExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["ImmTracteurReception"])) mClass._ImmTracteurReception = (string)mDataReader["ImmTracteurReception"];
                    if (!DBNull.Value.Equals(mDataReader["ImmRemorqueReception"])) mClass._ImmRemorqueReception = (string)mDataReader["ImmRemorqueReception"];

                    //if (!DBNull.Value.Equals(mDataReader["BordereauSortie"])) mClass._NumBordereauSortie = (string)mDataReader["BordereauSortie"];
                    //if (!DBNull.Value.Equals(mDataReader["PrixMoyen"])) mClass._PrixMoyen = (decimal)mDataReader["PrixMoyen"];
                    //if (!DBNull.Value.Equals(mDataReader["ValeurTheoriqueVente"])) mClass._ValuerVente = (decimal)mDataReader["ValeurTheoriqueVente"];
                    //if (!DBNull.Value.Equals(mDataReader["PrixExportateur"])) mClass._PrixExportateur = (decimal)mDataReader["PrixExportateur"];
                    //if (!DBNull.Value.Equals(mDataReader["PoidsProduitAccepte"])) mClass._PoidsProduitAccepte = (decimal)mDataReader["PoidsProduitAccepte"];
                    //if (!DBNull.Value.Equals(mDataReader["TarePaletteArrive"])) mClass._TarePaletteArrive = (decimal)mDataReader["TarePaletteArrive"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n TransfertFeves:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderReception(VenteLot mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass.ID = (Guid)mDataReader["ID"];

                    mClass._Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass.Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["SiteID"]))
                    {
                        mClass._Sites = new Site();
                        mClass._Sites.ID = (int)mDataReader["SiteID"];
                        mClass._Sites.Nom = (string)mDataReader["SiteNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["LotID"]))
                    {
                        mClass._Lot = new LotCoop();
                        mClass._Lot.ID = (Guid)mDataReader["LotID"];
                        mClass._Lot.Numero = (string)mDataReader["NumeroLot"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["NumeroExpedition"])) mClass._Numero = (string)mDataReader["NumeroExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["NumBordereauExpedition"])) mClass._NumeroExpedition = (string)mDataReader["NumBordereauExpedition"];

                    if (!DBNull.Value.Equals(mDataReader["MagasinExpeditionID"]))
                    {
                        mClass._Magasin = new Magasin();
                        mClass._Magasin.ID = (int)mDataReader["MagasinExpeditionID"];
                        mClass._Magasin.Designation = (string)mDataReader["MagasinExpeditionNom"];
                    }


                    if (!DBNull.Value.Equals(mDataReader["NombreSacsExpedition"])) mClass._NombreSacs = (int)mDataReader["NombreSacsExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["NombrePaletteExpedition"])) mClass._NombrePalette = (int)mDataReader["NombrePaletteExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacsExpedition"])) mClass._TareSacs = (decimal)mDataReader["TareSacsExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["TarePaletteExpedition"])) mClass._TarePalette = (decimal)mDataReader["TarePaletteExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrutExpedition"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrutExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetExpedition"])) mClass._PoidsNet = (decimal)mDataReader["PoidsNetExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["ImmTracteurExpedition"])) mClass._ImmTracteurExpedition = (string)mDataReader["ImmTracteurExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["ImmRemorqueExpedition"])) mClass._ImmRemorqueExpedition = (string)mDataReader["ImmRemorqueExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["DateExpedition"])) mClass._DateExpedition = (DateTime)mDataReader["DateExpedition"];
                    if (!DBNull.Value.Equals(mDataReader["CommentaireExpedition"])) mClass._Commentaire = (string)mDataReader["CommentaireExpedition"];

                    if (!DBNull.Value.Equals(mDataReader["NumBordereauReception"])) mClass._NumBordereauReception = (string)mDataReader["NumBordereauReception"];
                    if (!DBNull.Value.Equals(mDataReader["MagasinReceptionID"]))
                    {
                        mClass._Magasin = new Magasin();
                        mClass._Magasin.ID = (int)mDataReader["MagasinReceptionID"];
                        mClass._Magasin.Designation = (string)mDataReader["MagasinReceptionNom"];
                    }
                    if (!DBNull.Value.Equals(mDataReader["NombreSacsReception"])) mClass._NombreSacsReception = (int)mDataReader["NombreSacsReception"];
                    if (!DBNull.Value.Equals(mDataReader["NombrePaletteReception"])) mClass._NombrePaletteReception = (int)mDataReader["NombrePaletteReception"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNetReception"])) mClass._PoidsNetArrive = (decimal)mDataReader["PoidsNetReception"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrutReception"])) mClass.PoidsBrutReception = (decimal)mDataReader["PoidsBrutReception"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacsReception"])) mClass.TareSacsReception = (decimal)mDataReader["TareSacsReception"];
                    if (!DBNull.Value.Equals(mDataReader["TarePaletteReception"])) mClass._TarePaletteArrive = (decimal)mDataReader["TarePaletteReception"];
                    if (!DBNull.Value.Equals(mDataReader["ImmTracteurReception"])) mClass._ImmTracteurReception = (string)mDataReader["ImmTracteurReception"];
                    if (!DBNull.Value.Equals(mDataReader["ImmRemorqueReception"])) mClass._ImmRemorqueReception = (string)mDataReader["ImmRemorqueReception"];
                    if (!DBNull.Value.Equals(mDataReader["CommentaireReception"])) mClass._CommentaireReception = (string)mDataReader["CommentaireReception"];
                    if (!DBNull.Value.Equals(mDataReader["DateReception"])) mClass._DateReception = (DateTime)mDataReader["DateReception"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

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

    public partial class VenteLotViewModel
    {
        public VenteLot _VenteLot { get; set; }

        public string _DefaultCampagne { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }


}
