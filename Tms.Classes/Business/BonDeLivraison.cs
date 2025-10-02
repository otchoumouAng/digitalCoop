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
    //[Proxy(Read = "~/BonDeLivraison/Select")]
    //[JsonReader(RootProperty = "data")]
    public class BonDeLivraison : DataPersist
    {
        #region "Fields"

        private Guid _ID;
        private string _Campagne;
        private Livraison _Livraison;
        private AnalyseCode _AnalyseCode;
        private string _Numero;
        private DateTime _DateBonDeLivraison;
        private decimal _PoidsBrut;
        private decimal _Tare;
        private double _Humidite;
        private double _MatieresEtrangeres;
        private double _Brisures;
        private double _StdHumidite;
        private double _StdMatieresEtrangeres;
        private double _StdBrisures;
        private decimal _RefactionHumidite;
        private decimal _RefactionMatieresEtg;
        private decimal _RefactionBrisures;
        private decimal _PoidsNetAccepte;
        private string _Statut;
        private int _NbreSacs;
        private int _BeanCount;
        private decimal _PoidsLivre;
        private decimal _TareSacs;
        private decimal _TarePalettes;
        private string _Commentaire;
        private int? _CertificationID;
        private decimal _TareSacsAjustee;
        private decimal _TarePalettesAjustee;
        private bool _isViewPending;
        private double _Sievings;
        private string _LibelleProvenance;

        private double _Defectueuse;
        private double _Mouldy;
        private double _Slaty;

        private double? _Ffa;
        private double? _WeevilPc;

        private string _NomTransporteur;
        private string _LivraisonPoidsDeclare;

        // ** Pour Usinage
        private int? _Melange;
        private bool _IsNewInList;

        private Site _Sites;

        private int _NbreSacsAccepteInitial;
        private decimal _PrixMoyen;

        private int _MagasinDefID;
        #endregion

        #region "Properties"
        [ModelField(IDProperty = true, SortType = Ext.Net.SortTypeMethod.None, SortDir = Ext.Net.SortDirection.ASC)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Campagne
        {
            get { return _Campagne; }
            set { _Campagne = value; }
        }

        public int? CertificationID
        {
            get { return _CertificationID; }
            set { _CertificationID = value; }
        }

        public Livraison Livraison
        {
            get { return _Livraison; }
            set { _Livraison = value; }
        }

        public AnalyseCode AnalyseCode
        {
            get { return _AnalyseCode; }
            set { _AnalyseCode = value; }
        }

        public string Code
        {
            get { return _AnalyseCode != null ? _AnalyseCode.Code : string.Empty; }
        }

        public DateTime DateLivraison
        {
            get { return _Livraison != null ? _Livraison.DateLivraison : new DateTime(); }

        }

        public string DateLivraisonLongAstring
        {
            get { return (_Livraison != null && _Livraison.DateLivraison != null) ? _Livraison.DateLivraison.ToString("d") : string.Empty; }

        }

        public string DateLivraisonAstring
        {
            get { return (_Livraison != null && _Livraison.DateLivraison != null) ? _Livraison.DateLivraison.ToShortDateString() : string.Empty; }

        }

        public string LibelleTypeDeLivraison
        {
            get { return (_Livraison != null && _Livraison.LivraisonType != null)  ? _Livraison.LivraisonType.Designation :  string.Empty; }
            
        }
        public string LivraisonID
        {
            get { return (_Livraison != null) ? _Livraison.Numero : string.Empty; }

        }
        public string LibelleTypeDeSac
        {
            get { return (_Livraison != null && _Livraison.SacType != null) ? _Livraison.SacType.Designation : string.Empty; }

        }

        public string LibelleCertification
        {
            get { return (_Livraison != null && _Livraison.Certification != null ) ? _Livraison.Certification.Designation : string.Empty; }

        }
        public string Immatriculation
        {
            get { return _Livraison != null ? _Livraison.Immatriculation : string.Empty; }

        }

        public string FournisseurNom
        {
            get { return (_Livraison != null  && _Livraison.Fournisseur != null ) ? _Livraison.Fournisseur.Nom + " - " + _Livraison.Fournisseur.ID : string.Empty; }

        }

        public string NumeroTransfertFeves
        {
            get { return (_Livraison != null && _Livraison.TransfertFeves != null) ? _Livraison.TransfertFeves.Numero : string.Empty; }

        }

        public string NomTransporteur
        {
            get { return (_Livraison != null && _Livraison.Transporteur != null) ? _Livraison.Transporteur.Nom : string.Empty; }

        }

        public string LivraisonNumeroExterne
        {
            get { return (_Livraison != null ) ? _Livraison.NumeroExterne : string.Empty; }

        }

        public string LibelleProvenance
        {
            get { return _LibelleProvenance; }
            set { _LibelleProvenance = value; }

        }

        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
                
        public DateTime DateBonDeLivraison
        {
            get { return _DateBonDeLivraison; }
            set { _DateBonDeLivraison = value; }
        }

        public string DateBonDeLivraisonLongAsSrtring
        {
            get { return _DateBonDeLivraison != null ? _DateBonDeLivraison.ToString("d") : string.Empty; }            
        }


        public decimal PoidsBrut
        {
            get { return _PoidsBrut; }
            set { _PoidsBrut = value; }
        }

        public string PoidsBrutAsString
        {
            get { return _PoidsBrut != 0 ? String.Format("{0:#,#}", _PoidsBrut).TrimStart() : string.Empty; }
        }

        public int NbreSacs
        {
            get { return _NbreSacs; }
            set { _NbreSacs = value;}
        }

        public string NbreSacsAsString
        {
            get { return _NbreSacs != 0 ? String.Format("{0:#,#}", _NbreSacs).TrimStart() : string.Empty; }
        }

        public int BeanCount
        {
            get { return _BeanCount; }
            set { _BeanCount = value; }
        }

        public decimal Tare
        {
            get { return _Tare; }
            set { _Tare = value; }
        }

        public string TareAsString
        {
            get { return _Tare != 0 ? String.Format("{0:#,#}", _Tare).TrimStart() : string.Empty; }
        }
        

        public double Humidite
        {
            get { return _Humidite; }
            set { _Humidite = value; }
        }

        public double MatieresEtrangeres
        {
            get { return _MatieresEtrangeres; }
            set { _MatieresEtrangeres = value; }
        }

        public double Brisures
        {
            get { return _Brisures; }
            set { _Brisures = value; }
        }
        
        public double StdHumidite
        {
            get { return _StdHumidite; }
            set { _StdHumidite = value; }
        }

        public double StdMatieresEtrangeres
        {
            get { return _StdMatieresEtrangeres; }
            set { _StdMatieresEtrangeres = value; }
        }

        public double StdBrisures
        {
            get { return _StdBrisures; }
            set { _StdBrisures = value; }
        }

        public decimal RefactionHumidite
        {
            get { return _RefactionHumidite; }
            set { _RefactionHumidite = value; }
        }

        public decimal RefactionMatieresEtg
        {
            get { return _RefactionMatieresEtg; }
            set { _RefactionMatieresEtg = value; }
        }

        public decimal RefactionBrisures
        {
            get { return _RefactionBrisures; }
            set { _RefactionBrisures = value; }
        }

        public decimal TotalRetention
        {
            get { return _RefactionBrisures + _RefactionHumidite + _RefactionMatieresEtg; }
        }

        public string TotalRetentionAsString
        {
            get { return TotalRetention != 0 ? String.Format("{0:#,#}", TotalRetention).TrimStart() : string.Empty; }
        }

        public decimal PoidsNetAccepte
        {
            get { return _PoidsNetAccepte; }
            set { _PoidsNetAccepte = value; }
        }

        public string PoidsNetAccepteAsString
        {
            get { return _PoidsNetAccepte != 0 ? String.Format("{0:#,#}", _PoidsNetAccepte).TrimStart() : string.Empty; }
        }

        public decimal PoidsLivre
        {
            get { return _PoidsLivre; }
            set { _PoidsLivre = value; }
        }

        public string PoidsLivreAsString
        {
            get { return _PoidsLivre != 0 ? String.Format("{0:#,#}", _PoidsLivre).TrimStart() : string.Empty; }
        }

        public decimal TareSacs
        {
            get { return _TareSacs; }
            set { _TareSacs = value; }
        }

        public decimal TarePalettes
        {
            get { return _TarePalettes; }
            set { _TarePalettes = value; }
        }
        
        public string Statut
        {
            get { return _Statut; }
            set { _Statut = value; }
        }

        public bool IsActive
        {
            get { return _Statut == "AP"; }

        }

        [Column(Ignore = true)]
        public string AsString
        {
            get { return _Numero; }
        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Statut == "CA")
                    return 0; // BulletCross
                else if (_Statut == "AP")
                    return 1; // Tick
                else
                    return 2; //                     
            }
        }

        public string Commentaire
        {
            get { return _Commentaire; }
            set { _Commentaire = value; }
        }


        public decimal TareSacsAjustee
        {
            get { return _TareSacsAjustee; }
            set { _TareSacsAjustee = value; }
        }

        public string TareSacsAsString
        {
            get { return _TareSacsAjustee != 0 ? String.Format("{0:#,#}", _TareSacsAjustee).TrimStart() : string.Empty; }
        }

        public decimal TarePalettesAjustee
        {
            get { return _TarePalettesAjustee; }
            set { _TarePalettesAjustee = value; }
        }

        public string TarePalettesAsString
        {
            get { return _TarePalettesAjustee != 0 ? String.Format("{0:#,#}", _TarePalettesAjustee).TrimStart() : string.Empty; }
        }

        public bool isViewPending
        {
            get { return _isViewPending; }
            set { _isViewPending = value; }
        }

        public double Sievings
        {
            get { return _Sievings; }
            set { _Sievings = value; }
        }

        public double Defectueuse
        {
            get
            {
                return _Defectueuse;
            }

            set
            {
                _Defectueuse = value;
            }
        }

        public double Moisie
        {
            get
            {
                return _Mouldy;
            }

            set
            {
                _Mouldy = value;
            }
        }

        public double Slaty
        {
            get
            {
                return _Slaty;
            }

            set
            {
                _Slaty = value;
            }
        }

        public string LivraisonPoidsDeclare
        {
            get { return _Livraison != null ? String.Format("{0:#,#}", _Livraison.PoidsDeclare).TrimStart() : string.Empty; }
        }

        public double? Ffa
        {
            get
            {
                return _Ffa;
            }

            set
            {
                _Ffa = value;
            }
        }

        public double? WeevilPc
        {
            get
            {
                return _WeevilPc;
            }

            set
            {
                _WeevilPc = value;
            }
        }

        public int? Melange
        {
            get
            {
                return _Melange;
            }

            set
            {
                _Melange = value;
            }
        }

        public bool IsNewInList
        {
            get
            {
                return _IsNewInList;
            }

            set
            {
                _IsNewInList = value;
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

        public string SiteAsString
        {
            get { return _Sites != null ? _Sites.Nom : string.Empty; }

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

        public int MagasinDefID
        {
            get
            {
                return _MagasinDefID;
            }

            set
            {
                _MagasinDefID = value;
            }
        }
        #endregion

        #region Constructor
        public BonDeLivraison()
        {

        }

        public BonDeLivraison(Guid myID)
        {
            this.fnGet(myID);
        }
        #endregion

        #region Methods
        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("BonDeLivraison_Get", (Guid)Id);
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
        public bool fnGetByDeliveryNumber(object Id)
        {
            IDataReader mDataReader = null;
            bool mReturn = true;
            try
            {
                mDataReader = db().ExecuteReader("BonDeLivraison_GetByDeliveryNumber", (string)Id);
                if (mDataReader.Read())
                {
                    MapFromDataReader(this, mDataReader);
                    mReturn = true;
                }
                return mReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetByDeliveryNumber");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public override List<DataPersist> fnSelect()
        {
            return fnSelect("{Tous}", -1, -1, null, null, "-1", -1);
        }

        public List<DataPersist> fnSelect(string Campagne, int FournisseurID, int TypeID, DateTime? StartDate, DateTime? EndDate, string statut, int siteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("BonDeLivraison_Select");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@DeliveryTypeID", SqlDbType.Int, TypeID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@status", SqlDbType.VarChar, 2, statut);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    BonDeLivraison mClass = new BonDeLivraison();
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

        public List<DataPersist> fnSelectValide(string Campagne, int FournisseurID, int TypeID, DateTime? StartDate, DateTime? EndDate, string statut, int siteID = -1, int EstValide = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("BonDeLivraison_SelectValid");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@DeliveryTypeID", SqlDbType.Int, TypeID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@status", SqlDbType.VarChar, 2, statut);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                db().AddInParameter(mCommande, "@Valide", SqlDbType.Int, EstValide);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    BonDeLivraison mClass = new BonDeLivraison();
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


        public List<DataPersist> fnSelectExtend(string Campagne, int FournisseurID, int TypeID, DateTime? StartDate, DateTime? EndDate, string statut, int siteID = -1, int siteFournisseur = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("BonDeLivraison_Site_Select");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@DeliveryTypeID", SqlDbType.Int, TypeID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@status", SqlDbType.VarChar, 2, statut);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                db().AddInParameter(mCommande, "@SiteFournisseur", SqlDbType.Int, siteFournisseur);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    BonDeLivraison mClass = new BonDeLivraison();
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

        public List<DataPersist> fnSelectForClassification(string Campagne, int FournisseurID, int TypeID, DateTime? StartDate, DateTime? EndDate, int siteID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("BonDeLivraison_SelectForClassify");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@DeliveryTypeID", SqlDbType.Int, TypeID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    BonDeLivraison mClass = new BonDeLivraison();
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


        public List<DataPersist> fnSelectAvailableForInvoice(int FournisseurID, DateTime? StartDate, DateTime? EndDate, int certificationID = -1, int SiteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("BonDeLivraison_SelectAvailableForInvoice");
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@certificationID", SqlDbType.Int, certificationID);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, SiteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    BonDeLivraison mClass = new BonDeLivraison();
                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectAvailableForInvoice");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectAvailableForProduction(int CertificationID, int LivraisonTypeID, DateTime? StartDate, DateTime? EndDate)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("BonDeLivraion_SelectForProduction");
                db().AddInParameter(mCommande, "@LivraisonTypeID", SqlDbType.Int, LivraisonTypeID);
                db().AddInParameter(mCommande, "@CertificationId", SqlDbType.Int, CertificationID);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    BonDeLivraison mClass = new BonDeLivraison();
                    MapFromDataReaderForProduction(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectAvailableForProduction");
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
                    mCommande = db().CreateStoredProcCommand("BonDeLivraison_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);

                    if (_AnalyseCode != null)
                        db().AddInParameter(mCommande, "@CodificationId", SqlDbType.UniqueIdentifier, _AnalyseCode.ID);
                    else db().AddInParameter(mCommande, "@CodificationId", SqlDbType.UniqueIdentifier, DBNull.Value);

                    db().AddInParameter(mCommande, "@DeliveryID", SqlDbType.UniqueIdentifier, _Livraison.ID);
                    db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                    db().AddInParameter(mCommande, "@Humidite", SqlDbType.Float, _Humidite);
                    db().AddInParameter(mCommande, "@ME", SqlDbType.Float, _MatieresEtrangeres);
                    db().AddInParameter(mCommande, "@Brisure", SqlDbType.Float, _Brisures);
                    db().AddInParameter(mCommande, "@StdHumidite", SqlDbType.Float, _StdHumidite);
                    db().AddInParameter(mCommande, "@StdME", SqlDbType.Float, _StdMatieresEtrangeres);
                    db().AddInParameter(mCommande, "@StdBrisure", SqlDbType.Float, _StdBrisures);
                    db().AddInParameter(mCommande, "@RetHumidite", SqlDbType.Float, _RefactionHumidite);
                    db().AddInParameter(mCommande, "@RetME", SqlDbType.Float, _RefactionMatieresEtg);
                    db().AddInParameter(mCommande, "@RetBrisure", SqlDbType.Float, _RefactionBrisures);
                    db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNetAccepte);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("BonDeLivraison_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }                        
              
                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _DateBonDeLivraison);              
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, _TareSacsAjustee);
                db().AddInParameter(mCommande, "@TarePalettes", SqlDbType.Decimal, _TarePalettesAjustee);            
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);
                if (_CertificationID != null)
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, _CertificationID);
                else
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, DBNull.Value);

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
                        if (this._isnew)
                        {
                            _Numero = (string)db().Parameters(mCommande, "@Numero");
                            _Statut = "AP";
                        }

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
                throw new Exception(ex.Message + "\r\n" + "BonDeLivraison:fnUpdate");

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
                    mCommande = db().CreateStoredProcCommand("BonDeLivraison_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);

                    if (_AnalyseCode != null)
                        db().AddInParameter(mCommande, "@CodificationId", SqlDbType.UniqueIdentifier, _AnalyseCode.ID);
                    else db().AddInParameter(mCommande, "@CodificationId", SqlDbType.UniqueIdentifier, DBNull.Value);

                    db().AddInParameter(mCommande, "@DeliveryID", SqlDbType.UniqueIdentifier, _Livraison.ID);
                    db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                    db().AddInParameter(mCommande, "@Humidite", SqlDbType.Float, _Humidite);
                    db().AddInParameter(mCommande, "@ME", SqlDbType.Float, _MatieresEtrangeres);
                    db().AddInParameter(mCommande, "@Brisure", SqlDbType.Float, _Brisures);
                    db().AddInParameter(mCommande, "@StdHumidite", SqlDbType.Float, _StdHumidite);
                    db().AddInParameter(mCommande, "@StdME", SqlDbType.Float, _StdMatieresEtrangeres);
                    db().AddInParameter(mCommande, "@StdBrisure", SqlDbType.Float, _StdBrisures);
                    db().AddInParameter(mCommande, "@RetHumidite", SqlDbType.Float, _RefactionHumidite);
                    db().AddInParameter(mCommande, "@RetME", SqlDbType.Float, _RefactionMatieresEtg);
                    db().AddInParameter(mCommande, "@RetBrisure", SqlDbType.Float, _RefactionBrisures);
                    db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNetAccepte);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("BonDeLivraison_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _DateBonDeLivraison);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, _TareSacsAjustee);
                db().AddInParameter(mCommande, "@TarePalettes", SqlDbType.Decimal, _TarePalettesAjustee);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);
                if (_CertificationID != null)
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, _CertificationID);
                else
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, DBNull.Value);

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
                            _Statut = "AP";
                        }

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
                throw new Exception(ex.Message + "\r\n" + "BonDeLivraison:fnUpdate");

            }
            return Result;


        }

        public override bool fnActivate()
        {
            return false;
        }

        public override bool fnDeActivate()
        {
            return false;
        }

        public List<DataPersist> fnSelectForTransfertFeves(int siteID, int fournisseurID, DateTime? StartDate, DateTime? EndDate)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_BonDeLivraison_SelectForTransfertFeves");
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, fournisseurID);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    BonDeLivraison mClass = new BonDeLivraison();
                    MapFromDataReaderForTransfert(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectForTransfertFeves");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public bool fnValidate(DataTransaction mTran, int statut)
        {            
            bool Result;
            DataCommand mCommande;

            try
            {
                mCommande = db().CreateStoredProcCommand("BonDeLivraison_Valider");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@statut", SqlDbType.SmallInt, statut);
               // db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                db().ExecuteNonQuery(ref mCommande, mTran);

                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        base.UpdateAuditFields();
                        Result = true;

                        //_RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        //_ID = (Guid)db().Parameters(mCommande, "@ID");

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
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnValidate");
            }
            return Result;
        }

        public bool fnValidate(int statut)
        {
            bool Result;
            DataCommand mCommande;

            try
            {
                mCommande = db().CreateStoredProcCommand("BonDeLivraison_Valider");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@statut", SqlDbType.SmallInt, statut);
                // db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                db().ExecuteNonQuery(ref mCommande);

                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        base.UpdateAuditFields();
                        Result = true;

                        //_RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        //_ID = (Guid)db().Parameters(mCommande, "@ID");

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
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnValidate");
            }
            return Result;
        }


        public bool fnCancel()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("BonDeLivraison_Cancel");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
            db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
            db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
            db().AddInParameter(mCommande, "@CancelUser", SqlDbType.VarChar, _UtilisateurModification);
            try
            {
                db().ExecuteNonQuery(ref mCommande);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        bolResult = true;
                        _Statut = "CA";
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
                throw new Exception(ex.Message + "\r\n" + "BonDeLivraison:fnCancel");
            }
            return bolResult;
        }

        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _Numero;
        }

        private static void MapFromDataReader(BonDeLivraison mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonGuid"]))
                    {
                        Livraison mLivraison = new Livraison();

                        mLivraison.ID = (Guid)mDataReader["LivraisonGuid"];
                        if (!DBNull.Value.Equals(mDataReader["LivraisonID"]))  mLivraison.Numero = (string)mDataReader["LivraisonID"];
                        if (!DBNull.Value.Equals(mDataReader["DateLivraison"])) mLivraison.DateLivraison = (DateTime)mDataReader["DateLivraison"];
                        if (!DBNull.Value.Equals(mDataReader["Immatriculation"])) mLivraison.Immatriculation = (string)mDataReader["Immatriculation"];
                        if (!DBNull.Value.Equals(mDataReader["LivraisonNumeroExterne"])) mLivraison.NumeroExterne = (string)mDataReader["LivraisonNumeroExterne"];
                        if (!DBNull.Value.Equals(mDataReader["LivraisonPoidsDeclare"])) mLivraison.PoidsDeclare = (decimal)mDataReader["LivraisonPoidsDeclare"];

                        mLivraison.Transporteur = new Transporteur();
                        if (!DBNull.Value.Equals(mDataReader["IdTransporteur"])) mLivraison.Transporteur.ID = (int)mDataReader["IdTransporteur"];
                        if (!DBNull.Value.Equals(mDataReader["NomTransporteur"])) mLivraison.Transporteur.Nom = (string)mDataReader["NomTransporteur"];
                        

                        Campagne mCampagne = new Campagne();
                        if (!DBNull.Value.Equals(mDataReader["Campagne"])) mCampagne.Designation = (string)mDataReader["Campagne"];
                        mLivraison.Campagne = mCampagne;

                        LivraisonType mLivraisonType = new LivraisonType();
                        if (!DBNull.Value.Equals(mDataReader["TypeLivraisonID"]))
                        {
                            mLivraisonType.ID = (int)mDataReader["TypeLivraisonID"];
                            mLivraisonType.Designation = (string)mDataReader["TypeLivraisonNom"];
                            mLivraisonType.EstAchat = (bool)mDataReader["LivraisonEstAchat"];
                            mLivraisonType.AfficheResultatAnalyse = (bool)mDataReader["AfficheResultatAnalyse"];
                            mLivraison.LivraisonType = mLivraisonType;
                        }

                        Fournisseur mFournisseur = new Fournisseur();
                        if (!DBNull.Value.Equals(mDataReader["FournisseurID"]))
                        {
                            mFournisseur.ID = (int)mDataReader["FournisseurID"];
                            mFournisseur.Nom = (string)mDataReader["FournisseurNom"];
                            mLivraison.Fournisseur = mFournisseur;
                        }

                        SacType mSacType = new SacType();
                        if (!DBNull.Value.Equals(mDataReader["SacTypeID"]))
                        {
                            mSacType.ID = (int)mDataReader["SacTypeID"];
                            mSacType.Designation = (string)mDataReader["SacTypeNom"];
                            mLivraison.SacType = mSacType;
                        }

                        Certification mCertification = new Certification();
                        if (!DBNull.Value.Equals(mDataReader["CertificationID"]))
                        {
                            mCertification.ID = (int)mDataReader["CertificationID"];
                            mCertification.Designation = (string)mDataReader["CertificationNom"];
                            mLivraison.Certification = mCertification;
                        }

                        Site mSite = new Site();
                        if (!DBNull.Value.Equals(mDataReader["SiteID"]))
                        {
                            mSite.ID = (int)mDataReader["SiteID"];
                            mSite.Nom = (string)mDataReader["SiteNom"];
                            mLivraison.Site = mSite;
                            mClass.Sites = new Site();
                            mClass.Sites = mSite;
                        }
                        mLivraison.TransfertFeves = new Business.Sites.TransfertFeves();
                        if (!DBNull.Value.Equals(mDataReader["TransfertFevesID"]))
                        {
                            mLivraison.TransfertFeves.ID = (Guid)mDataReader["TransfertFevesID"];
                            mLivraison.TransfertFeves.Numero = (string)mDataReader["TransfertFevesNumero"];                            
                        }
                        mClass.Livraison = mLivraison;
                    }

                    if (!DBNull.Value.Equals(mDataReader["CodingID"]))
                    {
                        AnalyseCode mAnalyseCode = new AnalyseCode();
                        mAnalyseCode.ID = (Guid)mDataReader["CodingID"];
                        mAnalyseCode.Code = (string)mDataReader["CodingCode"];
                        mAnalyseCode.DateCode = (DateTime)mDataReader["CodingDate"];
                        mClass._AnalyseCode = mAnalyseCode; 
                    }

                    if (!DBNull.Value.Equals(mDataReader["DateFinalisation"])) mClass._DateBonDeLivraison = (DateTime)mDataReader["DateFinalisation"];
                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["NbreSacs"])) mClass._NbreSacs = (int)mDataReader["NbreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["BeanCount"])) mClass._BeanCount = (int)mDataReader["BeanCount"];
                                 
                    if (!DBNull.Value.Equals(mDataReader["Humidite"])) mClass._Humidite = (double)mDataReader["Humidite"];
                    if (!DBNull.Value.Equals(mDataReader["MatiereEtg"])) mClass._MatieresEtrangeres = (double)mDataReader["MatiereEtg"];
                    if (!DBNull.Value.Equals(mDataReader["Brisure"])) mClass._Brisures = (double)mDataReader["Brisure"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["Tare"])) mClass._Tare = (decimal)mDataReader["Tare"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsLivre"])) mClass._PoidsLivre = (decimal)mDataReader["PoidsLivre"];
                    if (!DBNull.Value.Equals(mDataReader["RefactionHumidite"])) mClass._RefactionHumidite = (decimal)mDataReader["RefactionHumidite"];
                    if (!DBNull.Value.Equals(mDataReader["RefactionMatiereEtg"])) mClass._RefactionMatieresEtg = (decimal)mDataReader["RefactionMatiereEtg"];
                    if (!DBNull.Value.Equals(mDataReader["RefactionBrisure"])) mClass._RefactionBrisures = (decimal)mDataReader["RefactionBrisure"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNetAccepte = (decimal)mDataReader["PoidsNet"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalettes = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["StdHumidite"])) mClass._StdHumidite = (double)mDataReader["StdHumidite"];
                    if (!DBNull.Value.Equals(mDataReader["StdMatiereEtg"])) mClass._StdMatieresEtrangeres = (double)mDataReader["StdMatiereEtg"];
                    if (!DBNull.Value.Equals(mDataReader["StdBrisure"])) mClass._StdBrisures = (double)mDataReader["StdBrisure"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacsAjustee"])) mClass._TareSacsAjustee = (decimal)mDataReader["TareSacsAjustee"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettesAjustee"])) mClass._TarePalettesAjustee = (decimal)mDataReader["TarePalettesAjustee"]; 
                    if (!DBNull.Value.Equals(mDataReader["LibelleProvenance"])) mClass.LibelleProvenance = (string)mDataReader["LibelleProvenance"];

                    if (!DBNull.Value.Equals(mDataReader["Defectueuse"])) mClass.Defectueuse = (double)mDataReader["Defectueuse"];
                    if (!DBNull.Value.Equals(mDataReader["Mouldy"])) mClass.Moisie = (double)mDataReader["Mouldy"];
                    if (!DBNull.Value.Equals(mDataReader["Slaty"])) mClass.Slaty = (double)mDataReader["Slaty"];                    

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nlbc_BonDeLivraison:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderForProduction(BonDeLivraison mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonGuid"]))
                    {
                        Livraison mLivraison = new Livraison();

                        mLivraison.ID = (Guid)mDataReader["LivraisonGuid"];
                        if (!DBNull.Value.Equals(mDataReader["LivraisonID"])) mLivraison.Numero = (string)mDataReader["LivraisonID"];
                        if (!DBNull.Value.Equals(mDataReader["DateLivraison"])) mLivraison.DateLivraison = (DateTime)mDataReader["DateLivraison"];
                        if (!DBNull.Value.Equals(mDataReader["Immatriculation"])) mLivraison.Immatriculation = (string)mDataReader["Immatriculation"];

                        Campagne mCampagne = new Campagne();
                        if (!DBNull.Value.Equals(mDataReader["Campagne"])) mCampagne.Designation = (string)mDataReader["Campagne"];
                        mLivraison.Campagne = mCampagne;

                        LivraisonType mLivraisonType = new LivraisonType();
                        if (!DBNull.Value.Equals(mDataReader["TypeLivraisonID"]))
                        {
                            mLivraisonType.ID = (int)mDataReader["TypeLivraisonID"];
                            mLivraisonType.Designation = (string)mDataReader["TypeLivraisonNom"];
                            mLivraison.LivraisonType = mLivraisonType;
                        }                        

                        //SacType mSacType = new SacType();
                        //if (!DBNull.Value.Equals(mDataReader["SacTypeID"]))
                        //{
                        //    mSacType.ID = (int)mDataReader["SacTypeID"];
                        //    mSacType.Designation = (string)mDataReader["SacTypeNom"];
                        //    mLivraison.SacType = mSacType;
                        //}

                        Certification mCertification = new Certification();
                        if (!DBNull.Value.Equals(mDataReader["CertificationID"]))
                        {
                            mCertification.ID = (int)mDataReader["CertificationID"];
                            mCertification.Designation = (string)mDataReader["CertificationNom"];
                            mLivraison.Certification = mCertification;
                        }

                        mClass.Livraison = mLivraison;
                    }                    
                                       
                    if (!DBNull.Value.Equals(mDataReader["NbreSacs"])) mClass._NbreSacs = (int)mDataReader["NbreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["Grainage"])) mClass._BeanCount = (int)mDataReader["Grainage"];

                    if (!DBNull.Value.Equals(mDataReader["Humidite"])) mClass._Humidite = (double)mDataReader["Humidite"];
                    if (!DBNull.Value.Equals(mDataReader["MatiereEtg"])) mClass._MatieresEtrangeres = (double)mDataReader["MatiereEtg"];
                    if (!DBNull.Value.Equals(mDataReader["Mitee"])) mClass._WeevilPc = (double)mDataReader["Mitee"];
                    if (!DBNull.Value.Equals(mDataReader["Mouldy"])) mClass._Mouldy = (double)mDataReader["Mouldy"];
                    if (!DBNull.Value.Equals(mDataReader["Ardoisee"])) mClass._Slaty = (double)mDataReader["Ardoisee"];
                    if (!DBNull.Value.Equals(mDataReader["Dechets"])) mClass._Sievings = (double)mDataReader["Dechets"];
                    if (!DBNull.Value.Equals(mDataReader["FFA"])) mClass._Ffa = (double)mDataReader["FFA"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nlbc_BonDeLivraison:MapFromDataReaderForProduction");
            }
        }

        private static void MapFromDataReaderForTransfert(BonDeLivraison mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    //Specific for Transfert
                    mClass.IsNew = true;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonGuid"]))
                    {
                        Livraison mLivraison = new Livraison();

                        mLivraison.ID = (Guid)mDataReader["LivraisonGuid"];
                        if (!DBNull.Value.Equals(mDataReader["LivraisonID"])) mLivraison.Numero = (string)mDataReader["LivraisonID"];
                        if (!DBNull.Value.Equals(mDataReader["DateLivraison"])) mLivraison.DateLivraison = (DateTime)mDataReader["DateLivraison"];
                        if (!DBNull.Value.Equals(mDataReader["Immatriculation"])) mLivraison.Immatriculation = (string)mDataReader["Immatriculation"];
                        mLivraison.Fournisseur = new Fournisseur();
                        if (!DBNull.Value.Equals(mDataReader["FournisseurID"])) mLivraison.Fournisseur.ID = (int)mDataReader["FournisseurID"];
                        if (!DBNull.Value.Equals(mDataReader["FournisseurNom"])) mLivraison.Fournisseur.Nom = (string)mDataReader["FournisseurNom"];

                        Campagne mCampagne = new Campagne();
                        if (!DBNull.Value.Equals(mDataReader["Campagne"])) mCampagne.Designation = (string)mDataReader["Campagne"];
                        mLivraison.Campagne = mCampagne;

                        LivraisonType mLivraisonType = new LivraisonType();
                        if (!DBNull.Value.Equals(mDataReader["TypeLivraisonID"]))
                        {
                            mLivraisonType.ID = (int)mDataReader["TypeLivraisonID"];
                            mLivraisonType.Designation = (string)mDataReader["TypeLivraisonNom"];
                            mLivraison.LivraisonType = mLivraisonType;
                        }
                        
                        Certification mCertification = new Certification();
                        if (!DBNull.Value.Equals(mDataReader["CertificationID"]))
                        {
                            mCertification.ID = (int)mDataReader["CertificationID"];
                            mCertification.Designation = (string)mDataReader["CertificationNom"];
                            mLivraison.Certification = mCertification;
                        }

                        mClass.Livraison = mLivraison;
                    }

                    if (!DBNull.Value.Equals(mDataReader["NbreSacs"])) mClass._NbreSacs = (int)mDataReader["NbreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["NbreSacs"])) mClass._NbreSacsAccepteInitial = (int)mDataReader["NbreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["Grainage"])) mClass._BeanCount = (int)mDataReader["Grainage"];

                    if (!DBNull.Value.Equals(mDataReader["Humidite"])) mClass._Humidite = (double)mDataReader["Humidite"];
                    if (!DBNull.Value.Equals(mDataReader["MatiereEtg"])) mClass._MatieresEtrangeres = (double)mDataReader["MatiereEtg"];
                    if (!DBNull.Value.Equals(mDataReader["Mitee"])) mClass._WeevilPc = (double)mDataReader["Mitee"];
                    if (!DBNull.Value.Equals(mDataReader["Mouldy"])) mClass._Mouldy = (double)mDataReader["Mouldy"];
                    if (!DBNull.Value.Equals(mDataReader["Ardoisee"])) mClass._Slaty = (double)mDataReader["Ardoisee"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNetAccepte = (decimal)mDataReader["PoidsNet"];
                    if (!DBNull.Value.Equals(mDataReader["PrixMoyen"])) mClass._PrixMoyen = (decimal)mDataReader["PrixMoyen"];

                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalettes = (decimal)mDataReader["TarePalettes"];                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nlbc_BonDeLivraison:MapFromDataReaderForTransfert");
            }
        }

        #endregion
    }

    public partial class BonDeLivraisonViewModel
    {
        public BonDeLivraison _BonDeLivraison { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }

        public string CertificationID
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._BonDeLivraison != null && this._BonDeLivraison.Livraison != null && this._BonDeLivraison.Livraison.Certification != null) ? _BonDeLivraison.Livraison.Certification.ID.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }
    }
}
