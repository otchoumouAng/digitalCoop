using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Business.Sites;
using Tms.Classes.Shared;
using Tms.Components.Data;

namespace Tms.Classes.Business
{
    //[Proxy(Read = "~/Livraison/Select")]
    //[JsonReader(RootProperty = "data")]
    public class Livraison : DataPersist
    {
        #region "Fields"

        private Guid _ID;
        private Exportateur _Exportateur;
        private Site _Site;
        private Campagne _Campagne;
        private Recolte _Recolte;
        private DateTime _DateLivraison;
        private string _Numero;
        private LivraisonType _LivraisonType;
        private Fournisseur _Fournisseur;
        private string _Immatriculation;
        private string _Tracteur;
        private string _Chauffeur;
        private Transporteur _Transporteur;
        private Provenance _Provenance;
        private Destination _Destination;
        private SacType _SacType;
        private int _SacsDeclares;
        private decimal _PoidsDeclare;
        private string _NumeroExterne;
        private Transitaire _Transitaire;
        private string _NumLot;
        private string _NumConteneur;
        private string _NumPlomb;
        private string _NumOT;
        private Certification _Certification;
        private Site _CentreAchat;
        private bool _Desative;
        private int _NumeroArrivee;
        private int _NumeroDechargement;
        private string _NumeroInterne;

        private string prefixeProcedureStockee;
        private decimal _PoidsLivre;
        private decimal _TareSacs;
        private decimal _TarePalettes;
        private int _SacsAcceptes;
        private decimal _PoidsBrut;
        private string _CodesAnalyse;

        private bool _EstReclassee;
        private Certification _CertificationOrigine;
        private Certification _AncienneCertification;
        private Certification _NouvelleCertification;
        private Provenance _AncienneProvenance;
        private Provenance _NouvelleProvenance;
        private Destination _AncienneDestination;
        private Destination _NouvelleDestination;

        private DateTime? _DateReclassement;
        private string _RaisonReclassement;
        private string _UtilisateurReclassement;
        private TransfertFeves _TransfertFeves;

        private LivraisonType _AncienType;
        private LivraisonType _NouveauType;
        #endregion

        #region "Properties"

        [Column(Text = "Code")]
        [ModelField(IDProperty = true, SortType = Ext.Net.SortTypeMethod.AsInt, SortDir = Ext.Net.SortDirection.ASC)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        [Column(Text = "Exportateur")]
        public Exportateur Exportateur
        {
            get { return _Exportateur; }
            set { _Exportateur = value; }
        }

        [Column(Text = "Site")]
        public Site Site
        {
            get { return _Site; }
            set { _Site = value; }
        }

        [Column(Text = "Campagne")]
        public Campagne Campagne
        {
            get { return _Campagne; }
            set { _Campagne = value; }
        }

        [Column(Text = "Recolte")]
        public Recolte Recolte
        {
            get { return _Recolte; }
            set { _Recolte = value; }
        }

        [Column(Text = "Date Livraison")]
        public DateTime DateLivraison
        {
            get { return _DateLivraison; }
            set { _DateLivraison = value; }
        }

        [Column(Text = "Numero")]
        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        [Column(Text = "Livraison type")]
        public LivraisonType LivraisonType
        {
            get { return _LivraisonType; }
            set { _LivraisonType = value; }
        }

        [Column(Text = "Fournisseur")]
        public Fournisseur Fournisseur
        {
            get { return _Fournisseur; }
            set { _Fournisseur = value; }
        }

        [Column(Text = "Immatriculation")]
        public string Immatriculation
        {
            get { return _Immatriculation; }
            set { _Immatriculation = value; }
        }

        [Column(Text = "Tracteur")]
        public string Tracteur
        {
            get { return _Tracteur; }
            set { _Tracteur = value; }
        }

        [Column(Text = "Chauffeur")]
        public string Chauffeur
        {
            get { return _Chauffeur; }
            set { _Chauffeur = value; }
        }

        [Column(Text = "Transporteur")]
        public Transporteur Transporteur
        {
            get { return _Transporteur; }
            set { _Transporteur = value; }
        }

        [Column(Text = "Provenance")]
        public Provenance Provenance
        {
            get { return _Provenance; }
            set { _Provenance = value; }
        }

        [Column(Text = "Destination")]
        public Destination Destination
        {
            get { return _Destination; }
            set { _Destination = value; }
        }

        [Column(Text = "Sac type")]
        public SacType SacType
        {
            get { return _SacType; }
            set { _SacType = value; }
        }

        [Column(Text = "Sacs déclarés")]
        public int SacsDeclares
        {
            get { return _SacsDeclares; }
            set { _SacsDeclares = value; }
        }

        [Column(Text = "Poids déclaré")]
        public decimal PoidsDeclare
        {
            get { return _PoidsDeclare; }
            set { _PoidsDeclare = value; }
        }

        public string PoidsDeclareAsString
        {
            get { return _PoidsDeclare != 0 ? String.Format("{0:#,#}", _PoidsDeclare).TrimStart() : string.Empty; }
        }

        [Column(Text = "Num. externe")]
        public string NumeroExterne
        {
            get { return _NumeroExterne; }
            set { _NumeroExterne = value; }
        }

        [Column(Text = "Transitaire")]
        public Transitaire Transitaire
        {
            get { return _Transitaire; }
            set { _Transitaire = value; }
        }

        [Column(Text = "Numero Lot")]
        public string NumLot
        {
            get { return _NumLot; }
            set { _NumLot = value; }
        }

        [Column(Text = "Num. Conteneur")]
        public string NumConteneur
        {
            get { return _NumConteneur; }
            set { _NumConteneur = value; }
        }

        [Column(Text = "Num. Plomb")]
        public string NumPlomb
        {
            get { return _NumPlomb; }
            set { _NumPlomb = value; }
        }

        [Column(Text = "Num. OT")]
        public string NumOT
        {
            get { return _NumOT; }
            set { _NumOT = value; }
        }

        [Column(Text = "Certification")]
        public Certification Certification
        {
            get { return _Certification; }
            set { _Certification = value; }
        }

        [Column(Text = "Centre achat")]
        public Site CentreAchat
        {
            get { return _CentreAchat; }
            set { _CentreAchat = value; }
        }

        [Column(Text = "Desactive")]
        public bool Desactive
        {
            get { return _Desative; }
            set { _Desative = value; }
        }
        
        [Column(Text = "AsString")]
        public string AsString
        {
            get { return _Numero; }
        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desative)
                    return 0; // BulletCross
                //else if (_IsApproved)
                //    return 1; // Tick
                else
                    return 2; //                     
            }
        }

        public string LivraisonTypeAsString
        {
            get { return _LivraisonType == null ? string.Empty : _LivraisonType.Designation; }
        }

        public string TransporteurAsString
        {
            get { return _Transporteur == null ? string.Empty : _Transporteur.Nom; }
        }

        public string FournisseurNameAsString
        {
            get { return _Fournisseur == null ? string.Empty : _Fournisseur.Nom; }
        }

        public string DateLivraisonAsString
        {
            get { return _DateLivraison != null ?  _DateLivraison.ToString() : string.Empty; }
        }

        public string DateLivraisonShortAsString
        {
            get { return _DateLivraison != null ? _DateLivraison.ToShortDateString() : string.Empty; }
        }

        public string DateLivraisonLongAsString
        {
            get { return _DateLivraison != null ? _DateLivraison.ToString("g") : string.Empty; }
        }

        public string TruckIDAsString
        {
            get { return _Immatriculation; }
        }

        public int NumeroArrivee
        {
            get
            {
                return _NumeroArrivee;
            }

            set
            {
                _NumeroArrivee = value;
            }
        }

        public int NumeroDechargement
        {
            get
            {
                return _NumeroDechargement;
            }

            set
            {
                _NumeroDechargement = value;
            }
        }

        public string NumeroInterne
        {
            get
            {
                return _NumeroInterne;
            }

            set
            {
                _NumeroInterne = value;
            }
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

        public decimal PoidsBrut
        {
            get { return _PoidsBrut; }
            set { _PoidsBrut = value; }
        }

        public string PoidsBrutAsString
        {
            get { return _PoidsBrut != 0 ? String.Format("{0:#,#}", _PoidsBrut).TrimStart() : string.Empty; }
        }

        public decimal TareSacs
        {
            get { return _TareSacs; }
            set { _TareSacs = value; }
        }

        public string TareSacsAsString
        {
            get { return _TareSacs != 0 ? String.Format("{0:#,#}", _TareSacs).TrimStart() : string.Empty; }
        }

        public decimal TarePalettes
        {
            get { return _TarePalettes; }
            set { _TarePalettes = value; }
        }

        public string TarePalettesAsString
        {
            get { return _TarePalettes != 0 ? String.Format("{0:#,#}", _TarePalettes).TrimStart() : string.Empty; }
        }

        public int SacsAcceptes
        {
            get { return _SacsAcceptes; }
            set { _SacsAcceptes = value; }
        }

        //[ModelField(Ignore = true)]
        public string CodesAnalyse
        {
            get { return _CodesAnalyse != null ? _CodesAnalyse.TrimStart() : string.Empty; }
            set { _CodesAnalyse = value; }
        }

        public string DateLivraisonAsShortDateString
        {
            get { return _DateLivraison == null ? string.Empty : _DateLivraison.ToShortDateString(); }
        }

        public string FournisseurNameAndCodeAsString
        {
            get { return _Fournisseur == null ? string.Empty : _Fournisseur.Nom + " - " + _Fournisseur.ID; }
        }
        #endregion

        public Livraison()
        {
            prefixeProcedureStockee = "Livraison";
        }

        public string OrigineAsString
        {
            get { return _Provenance == null ? string.Empty : _Provenance.Nom; }
        }

        public string DestinationAsString
        {
            get { return _Destination == null ? string.Empty : _Destination.Nom; }
        }

        public string CertificationAsString
        {
            get { return _Certification == null ? string.Empty : _Certification.Designation; }
        }

        public bool EstReclassee
        {
            get
            {
                return _EstReclassee;
            }

            set
            {
                _EstReclassee = value;
            }
        }

        public Certification CertificationOrigine
        {
            get
            {
                return _CertificationOrigine;
            }

            set
            {
                _CertificationOrigine = value;
            }
        }

        public Certification AncienneCertification
        {
            get
            {
                return _AncienneCertification;
            }

            set
            {
                _AncienneCertification = value;
            }
        }

        public Certification NouvelleCertification
        {
            get
            {
                return _NouvelleCertification;
            }

            set
            {
                _NouvelleCertification = value;
            }
        }

        public DateTime? DateReclassement
        {
            get
            {
                return _DateReclassement;
            }

            set
            {
                _DateReclassement = value;
            }
        }

        public string RaisonReclassement
        {
            get
            {
                return _RaisonReclassement;
            }

            set
            {
                _RaisonReclassement = value;
            }
        }

        public string UtilisateurReclassement
        {
            get
            {
                return _UtilisateurReclassement;
            }

            set
            {
                _UtilisateurReclassement = value;
            }
        }

        public string SiteAsString
        {
            get { return _Site == null ? string.Empty : _Site.Nom; }
        }

        public TransfertFeves TransfertFeves
        {
            get
            {
                return _TransfertFeves;
            }

            set
            {
                _TransfertFeves = value;
            }
        }

        public string NumeroTransfertFeves
        {
            get
            {
                return _TransfertFeves != null ? _TransfertFeves.Numero : string.Empty;
            }            
        }

        public Provenance AncienneProvenance
        {
            get
            {
                return _AncienneProvenance;
            }

            set
            {
                _AncienneProvenance = value;
            }
        }

        public Provenance NouvelleProvenance
        {
            get
            {
                return _NouvelleProvenance;
            }

            set
            {
                _NouvelleProvenance = value;
            }
        }

        public Destination AncienneDestination
        {
            get
            {
                return _AncienneDestination;
            }

            set
            {
                _AncienneDestination = value;
            }
        }

        public Destination NouvelleDestination
        {
            get
            {
                return _NouvelleDestination;
            }

            set
            {
                _NouvelleDestination = value;
            }
        }

        public LivraisonType AncienType
        {
            get
            {
                return _AncienType;
            }

            set
            {
                _AncienType = value;
            }
        }

        public LivraisonType NouveauType
        {
            get
            {
                return _NouveauType;
            }

            set
            {
                _NouveauType = value;
            }
        }
        //test


        // #endregion


        #region Methods
        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader(prefixeProcedureStockee + "_Get", (Guid)Id);
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

        public string CampagneAsString
        {
            get { return _Campagne == null ? string.Empty : _Campagne.Designation; }
        }

        public bool fnGetSpecific(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader(prefixeProcedureStockee + "_GetSpecific", (Guid)Id);
                if (mDataReader.Read())
                {
                    MapFromDataReaderSP(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":GetSpecific");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public bool fnGetForClassification(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader(prefixeProcedureStockee + "_Get", (Guid)Id);
                if (mDataReader.Read())
                {
                    MapFromDataReaderClassification(this, mDataReader);
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


        public bool fnGetBySpotPrice(Guid prixnegocie, Guid livraison)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;
            try
            {                
                DataCommand mCommande = db().CreateStoredProcCommand("LivraisonByPrixNegocie_Get");
                db().AddInParameter(mCommande, "@prixnegocie", SqlDbType.UniqueIdentifier, prixnegocie);
                db().AddInParameter(mCommande, "@livraison", SqlDbType.UniqueIdentifier, livraison);
                mDataReader = db().ExecuteReader(mCommande);
                if (mDataReader.Read())
                {
                    MapFromDataReaderToSpotPrice(this, mDataReader);
                }
                return true;
        }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetBySpotPrice");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        //public List<DataPersist> fnSelectBySpotPrice(Guid prixnegocie)
        //{
        //    List<DataPersist> mList = new List<DataPersist>();
        //    IDataReader mDataReader = null;

        //    try
        //    {
        //        //DataCommand mCommande = db().CreateStoredProcCommand(prefixeProcedureStockee + "_Select");
        //        DataCommand mCommande = db().CreateStoredProcCommand("LivraisonByPrixNegocie_Get");
        //        db().AddInParameter(mCommande, "@prixnegocie", SqlDbType.UniqueIdentifier, prixnegocie);
        //        mDataReader = db().ExecuteReader(mCommande);

        //        while (mDataReader.Read())
        //        {
        //            Livraison mClass = new Livraison();

        //            MapFromDataReader(mClass, mDataReader);
        //            mList.Add(mClass);
        //        }
        //        return mList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelect");
        //    }
        //    finally
        //    {
        //        if (mDataReader != null) mDataReader.Close();
        //    }
        //}

        public bool fnGetForFinalizing(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader(prefixeProcedureStockee + "_GetForFinalizing", (Guid)Id);
                if (mDataReader.Read())
                {
                    MapFromDataReaderToFinalizing(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetForFinalizing");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public bool fnGetAvalaibleForFinalizing(object Id)
        {
            IDataReader mDataReader = null;
            bool mReturn = true;
            try
            {
                mDataReader = db().ExecuteReader(prefixeProcedureStockee + "_GetAvailableForFinalizing", (string)Id);
                if (mDataReader.Read())
                {
                    MapFromDataReaderToFinalizing(this, mDataReader);
                    mReturn = true;
                }
                return mReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetAvalaibleForFinalizing");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public bool fnSelectByNumber(object numero)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("Livraison_SelectByNumber", (string)numero);
                if (mDataReader.Read())
                {
                    //MapFromDataReaderToSpotPrice(this, mDataReader);
                    MapFromDataReader(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnSelectByNumber");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }


        public bool fnGetByWeighingStatus(Pesee.StatutPesee mStatut = Pesee.StatutPesee.PremierePesee)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("PeseeRefoulee_GetByDeliveryNumber");
                IDataReader mDataReader = null;
                db().AddInParameter(mCommande, "@Numero", SqlDbType.VarChar, this.Numero);
                db().AddInParameter(mCommande, "@NumeroPesee", SqlDbType.Int, (int)mStatut);
                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                mDataReader = db().ExecuteReader(mCommande);


                string ErrorMessage = (string)db().Parameters(mCommande, "@ErrorMessage");
                if (String.IsNullOrEmpty(ErrorMessage))
                {
                    try
                    {
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
                else
                {
                    throw new Exception(ErrorMessage);
                }
            


                //switch (reslt)
                //{
                //    case 0:
                //        //Everything OK
                //        //base.UpdateAuditFields();
                //        Result = true;
                //        _isnew = false;

                        


                //        break;
                //    default:
                //        //Unkown error
                //        Result = false;
                       
                //        break;
                //}
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + "\r\n" + "Livraison:fnGetByWeighingStatus");

            }
            return Result;
        }



        public bool fnSelectUnWeighedByNumber(object numero)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("Livraison_UnWeighed_GetByNumber", (string)numero);
                if (mDataReader.Read())
                {
                    //MapFromDataReaderToSpotPrice(this, mDataReader);
                    MapFromDataReader(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnSelectUnWeighedByNumber");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }


        public bool fnSelectRejectedByNumber(object numero)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("Livraison_Rejected_GetByNumber", (string)numero);
                if (mDataReader.Read())
                {
                    //MapFromDataReaderToSpotPrice(this, mDataReader);
                    MapFromDataReader(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnSelectRejectedByNumber");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }


        //Select les details de la livraison , qui autorise Analyse
        public bool fnSelectByNumberAndType(object numero)
        {
            IDataReader mDataReader = null;
            
            try
            {
                //mDataReader = db().ExecuteReader("AnalyseCode_SelectLivraisonByNumber", (string)numero);
                DataCommand mCommand = db().CreateStoredProcCommand("AnalyseCode_SelectLivraisonByNumber");

                db().AddInParameter(mCommand, "@Numero", SqlDbType.VarChar, 50, (string)numero);
                db().AddParameter(mCommand, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                db().AddOutParameter(mCommand, "@ErrorMessage", SqlDbType.VarChar, 1000);
                mDataReader = db().ExecuteReader(mCommand);
                //int val = (int)db().Parameters(mCommand, "ReturnValue");

                if ((db().Parameters(mCommand, "ReturnValue") != null) && ( (int)db().Parameters(mCommand, "ReturnValue") == -1 ) )
                {
                    string ErrorMessage = (string)db().Parameters(mCommand, "@ErrorMessage");
                    throw new Exception(ErrorMessage);
                    return false;
                }

                if (mDataReader.Read())
                {
                    MapFromDataReaderToSpotPrice(this, mDataReader);
                }
                return true;
                
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnSelectByNumberAndType");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }
        


        public override List<DataPersist> fnSelect()
        {
            string defaultCrop = new Parametres(0).Campagne;

            int livraisonTypeID = -1;

            string fournisseurID = "{Tous}";

            DateTime debut = DateTime.Now;

            DateTime fin = DateTime.Now;

            int exportateurId = -1;

            int siteID = -1;

            return fnSelect(defaultCrop, livraisonTypeID, fournisseurID, debut, fin, exportateurId, siteID);
        }

        public List<DataPersist> fnSelect(string cropYear, int livraisonTypeID, string fournisseurID, DateTime mStartDate, DateTime mEndDate, int ExportateurID, int siteID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand(prefixeProcedureStockee + "_Select");
                db().AddInParameter(mCommande, "@CropYear", SqlDbType.VarChar, cropYear);
                db().AddInParameter(mCommande, "@LivraisonTypeID", SqlDbType.Int, livraisonTypeID);
                db().AddInParameter(mCommande, "@Fournisseur", SqlDbType.VarChar, fournisseurID);
                db().AddInParameter(mCommande, "@StartDate", SqlDbType.DateTime, mStartDate);
                db().AddInParameter(mCommande, "@EndDate", SqlDbType.DateTime, mEndDate);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Livraison mClass = new Livraison();

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

        public List<DataPersist> fnSelectForContainer(int ExportateurID, DateTime? mStartDate, DateTime? mEndDate)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand(prefixeProcedureStockee + "_SelectForContainer");
                db().AddInParameter(mCommande, "@StartDate", SqlDbType.DateTime, mStartDate);
                db().AddInParameter(mCommande, "@EndDate", SqlDbType.DateTime, mEndDate);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Livraison mClass = new Livraison();

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

        public async Task<List<DataPersist>> fnSelectt(string cropYear, int livraisonTypeID, string fournisseurID, DateTime mStartDate, DateTime mEndDate, int ExportateurID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand(prefixeProcedureStockee + "_Select");
                db().AddInParameter(mCommande, "@CropYear", SqlDbType.VarChar, cropYear);
                db().AddInParameter(mCommande, "@LivraisonTypeID", SqlDbType.Int, livraisonTypeID);
                db().AddInParameter(mCommande, "@Fournisseur", SqlDbType.VarChar, fournisseurID);
                db().AddInParameter(mCommande, "@StartDate", SqlDbType.DateTime, mStartDate);
                db().AddInParameter(mCommande, "@EndDate", SqlDbType.DateTime, mEndDate);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                mDataReader = db().ExecuteReader(mCommande);
                await System.Threading.Tasks.Task.Delay(3000000);
                while (mDataReader.Read())
                {
                    Livraison mClass = new Livraison();

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

        public List<DataPersist> fnSelectByNumbers(string numbers)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {               
                DataCommand mCommande = db().CreateStoredProcCommand("LivraisonByNumber_Select");
                db().AddInParameter(mCommande, "@Numero", SqlDbType.VarChar, numbers);
                
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Livraison mClass = new Livraison();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectByNumbers");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectBySpotPrice(Guid spotprice, int fournisseur)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("PrixNegocie_SelectLivraisonParPrixNegocie");
                db().AddInParameter(mCommande, "@prixnegocieID", SqlDbType.UniqueIdentifier, spotprice);
                db().AddInParameter(mCommande, "@fournisseur", SqlDbType.Int, fournisseur);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Livraison mClass = new Livraison();

                    MapFromDataReaderToSpotPrice(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectBySpotPrice");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectForSpotPrice(int fournisseur)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("PrixNegocie_SelectLivraisonsParFournisseur");
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, fournisseur);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Livraison mClass = new Livraison();

                    MapFromDataReaderToSpotPrice(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectForSpotPrice");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectForDeliveryCoding(DateTime? startdate, DateTime? enddate, int desactive)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("AnalyseCode_SelectLivraisons");
                db().AddInParameter(mCommande, "@startdate", SqlDbType.DateTime, startdate);
                db().AddInParameter(mCommande, "@enddate", SqlDbType.DateTime, enddate);
                db().AddInParameter(mCommande, "@status", SqlDbType.Int, desactive);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Livraison mClass = new Livraison();

                    MapFromDataReaderToAnalyseCode(mClass, mDataReader);
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

        public bool fnGetForDeliveryCoding(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("AnalyseCode_SelectLivraisonByID", (Guid)Id);
                if (mDataReader.Read())
                {
                    MapFromDataReaderToAnalyseCode(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetForDeliveryCoding");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectPendingDeliveries()
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("PrixNegocie_SelectPendingDeliveries");                

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Livraison mClass = new Livraison();

                    MapFromDataReaderToSpotPrice(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectPendingDeliveries");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }


        public List<DataPersist> fnSelectForFinalizing(int FournisseurID, DateTime? StartDate, DateTime? EndDate, int SiteID = -1, int TypeID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Livraison_SelectAvailableForFinalizing");
                db().AddInParameter(mCommande, "@FournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@DateFin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, SiteID);
                db().AddInParameter(mCommande, "@TypeID", SqlDbType.Int, TypeID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Livraison mClass = new Livraison();

                    MapFromDataReaderToFinalizing(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectForFinalizing");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }


        public List<DataPersist> fnSelectPendingDeliveries(string cropYear, DateTime startDate, DateTime endDate)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Pesee_SelectPendingDeliveries");
                db().AddInParameter(mCommande, "@CropYear", SqlDbType.VarChar, cropYear);
                db().AddInParameter(mCommande, "@StartDate", SqlDbType.DateTime, startDate);
                db().AddInParameter(mCommande, "@EndDate", SqlDbType.DateTime, endDate);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Livraison mClass = new Livraison();
                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectPendingDeliveries");
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

                    mCommande = db().CreateStoredProcCommand(prefixeProcedureStockee + "_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    //db().AddOutParameter(mCommande, "@NumeroOrdreArrivee", SqlDbType.Int, 0);
                    //db().AddOutParameter(mCommande, "@NumeroOrdreDechargement", SqlDbType.Int, 0);
                    //db().AddOutParameter(mCommande, "@NumeroInterne", SqlDbType.VarChar, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand(prefixeProcedureStockee + "_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, _Exportateur.ID);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, _Site.ID);
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.VarChar, _Campagne.Designation);
                db().AddInParameter(mCommande, "@RecolteID", SqlDbType.Int, _Recolte.ID);
                db().AddInParameter(mCommande, "@LivraisonDate", SqlDbType.DateTime, _DateLivraison);
                db().AddInParameter(mCommande, "@LivraisonNumero", SqlDbType.VarChar, _Numero);
                db().AddInParameter(mCommande, "@LivraisonTypeID", SqlDbType.Int, _LivraisonType.ID);
                db().AddInParameter(mCommande, "@FournisseurID", SqlDbType.Int, _Fournisseur.ID);
                db().AddInParameter(mCommande, "@Immatriculation", SqlDbType.VarChar, _Immatriculation);
                db().AddInParameter(mCommande, "@Tracteur", SqlDbType.VarChar, _Tracteur);
                db().AddInParameter(mCommande, "@Chauffeur", SqlDbType.VarChar, _Chauffeur);

                if(_Transporteur != null)
                db().AddInParameter(mCommande, "@TransporteurID", SqlDbType.Int, _Transporteur.ID);
                else
                    db().AddInParameter(mCommande, "@TransporteurID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@ProvenanceID", SqlDbType.Int, _Provenance.ID);
                db().AddInParameter(mCommande, "@DestinationID", SqlDbType.Int, _Destination.ID);
                db().AddInParameter(mCommande, "@SacTypeID", SqlDbType.Int, _SacType.ID);
                db().AddInParameter(mCommande, "@SacsDeclares", SqlDbType.Int, _SacsDeclares);
                db().AddInParameter(mCommande, "@PoidsDeclare", SqlDbType.Decimal, _PoidsDeclare);
                db().AddInParameter(mCommande, "@NumeroExterne", SqlDbType.VarChar, _NumeroExterne);

                if(_Transitaire != null)
                db().AddInParameter(mCommande, "@TransitaireID", SqlDbType.Int, _Transitaire.ID);
                else
                    db().AddInParameter(mCommande, "@TransitaireID", SqlDbType.Int, DBNull.Value);


                db().AddInParameter(mCommande, "@NumLot", SqlDbType.VarChar, _NumLot);
                db().AddInParameter(mCommande, "@NumConteneur", SqlDbType.VarChar, _NumConteneur);
                db().AddInParameter(mCommande, "@NumPlomb", SqlDbType.VarChar, _NumPlomb);
                db().AddInParameter(mCommande, "@NumOT", SqlDbType.VarChar, _NumOT);

                if(_Certification != null)
                db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, _Certification.ID);
                else
                   db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, DBNull.Value);

                if (_CentreAchat != null)
                    db().AddInParameter(mCommande, "@CentreAchatID", SqlDbType.Int, _CentreAchat.ID);
                else
                    db().AddInParameter(mCommande, "@CentreAchatID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@TransfertFeveID", SqlDbType.UniqueIdentifier, DBNull.Value);

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
                        //base.UpdateAuditFields();
                        Result = true;

                        //if(_isnew)
                        //{
                        //   _NumeroArrivee = (int)db().Parameters(mCommande, "@NumeroOrdreArrivee");
                        //   _NumeroDechargement = (int)db().Parameters(mCommande, "@NumeroOrdreDechargement");
                        //   _NumeroInterne = (string)db().Parameters(mCommande, "@NumeroInterne");                            
                        //}

                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        _ID = (Guid)db().Parameters(mCommande, "@ID");
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
                throw new Exception(ex.Message + "\r\n" + "Fournisseur:fnUpdate");

            }
            return Result;
        }

        public bool fnUpdateSmall()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("Livraison_ModifySmall");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

                //db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, _Exportateur.ID);
                //db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, _Site.ID);
                //db().AddInParameter(mCommande, "@CampagneID", SqlDbType.VarChar, _Campagne.Designation);
                //db().AddInParameter(mCommande, "@RecolteID", SqlDbType.Int, _Recolte.ID);
                //db().AddInParameter(mCommande, "@LivraisonDate", SqlDbType.DateTime, _DateLivraison);
                //db().AddInParameter(mCommande, "@LivraisonNumero", SqlDbType.VarChar, _Numero);
                //db().AddInParameter(mCommande, "@LivraisonTypeID", SqlDbType.Int, _LivraisonType.ID);
                //db().AddInParameter(mCommande, "@FournisseurID", SqlDbType.Int, _Fournisseur.ID);
                db().AddInParameter(mCommande, "@Immatriculation", SqlDbType.VarChar, _Immatriculation);
                db().AddInParameter(mCommande, "@Tracteur", SqlDbType.VarChar, _Tracteur);
                db().AddInParameter(mCommande, "@Chauffeur", SqlDbType.VarChar, _Chauffeur);

                if (_Transporteur != null)
                    db().AddInParameter(mCommande, "@TransporteurID", SqlDbType.Int, _Transporteur.ID);
                else
                    db().AddInParameter(mCommande, "@TransporteurID", SqlDbType.Int, DBNull.Value);

                //db().AddInParameter(mCommande, "@ProvenanceID", SqlDbType.Int, _Provenance.ID);
                //db().AddInParameter(mCommande, "@DestinationID", SqlDbType.Int, _Destination.ID);
                //db().AddInParameter(mCommande, "@SacTypeID", SqlDbType.Int, _SacType.ID);
                db().AddInParameter(mCommande, "@SacsDeclares", SqlDbType.Int, _SacsDeclares);
                db().AddInParameter(mCommande, "@PoidsDeclare", SqlDbType.Decimal, _PoidsDeclare);
                db().AddInParameter(mCommande, "@NumeroExterne", SqlDbType.VarChar, _NumeroExterne);

                if (_Transitaire != null)
                    db().AddInParameter(mCommande, "@TransitaireID", SqlDbType.Int, _Transitaire.ID);
                else
                    db().AddInParameter(mCommande, "@TransitaireID", SqlDbType.Int, DBNull.Value);


                db().AddInParameter(mCommande, "@NumLot", SqlDbType.VarChar, _NumLot);
                db().AddInParameter(mCommande, "@NumConteneur", SqlDbType.VarChar, _NumConteneur);
                db().AddInParameter(mCommande, "@NumPlomb", SqlDbType.VarChar, _NumPlomb);
                db().AddInParameter(mCommande, "@NumOT", SqlDbType.VarChar, _NumOT);

                //if (_Certification != null)
                //    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, _Certification.ID);
                //else
                //    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, DBNull.Value);

                //if (_CentreAchat != null)
                //    db().AddInParameter(mCommande, "@CentreAchatID", SqlDbType.Int, _CentreAchat.ID);
                //else
                //    db().AddInParameter(mCommande, "@CentreAchatID", SqlDbType.Int, DBNull.Value);

                //db().AddInParameter(mCommande, "@TransfertFeveID", SqlDbType.UniqueIdentifier, DBNull.Value);

                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);

                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                db().ExecuteNonQuery(ref mCommande);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        //base.UpdateAuditFields();
                        Result = true;

                        //if(_isnew)
                        //{
                        //   _NumeroArrivee = (int)db().Parameters(mCommande, "@NumeroOrdreArrivee");
                        //   _NumeroDechargement = (int)db().Parameters(mCommande, "@NumeroOrdreDechargement");
                        //   _NumeroInterne = (string)db().Parameters(mCommande, "@NumeroInterne");                            
                        //}

                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        _ID = (Guid)db().Parameters(mCommande, "@ID");
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
                throw new Exception(ex.Message + "\r\n" + "Fournisseur:fnUpdate");

            }
            return Result;
        }

        public bool fnUpdateSite()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {

                    mCommande = db().CreateStoredProcCommand("LivraisonSite_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 9);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand(prefixeProcedureStockee + "_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, _Exportateur.ID);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, _Site.ID);
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.VarChar, _Campagne.Designation);
                db().AddInParameter(mCommande, "@RecolteID", SqlDbType.Int, _Recolte.ID);
                db().AddInParameter(mCommande, "@LivraisonDate", SqlDbType.DateTime, _DateLivraison);
                db().AddInParameter(mCommande, "@LivraisonNumero", SqlDbType.VarChar, _Numero);
                db().AddInParameter(mCommande, "@LivraisonTypeID", SqlDbType.Int, _LivraisonType.ID);
                db().AddInParameter(mCommande, "@FournisseurID", SqlDbType.Int, _Fournisseur.ID);
                db().AddInParameter(mCommande, "@Immatriculation", SqlDbType.VarChar, _Immatriculation);
                db().AddInParameter(mCommande, "@Tracteur", SqlDbType.VarChar, _Tracteur);
                db().AddInParameter(mCommande, "@Chauffeur", SqlDbType.VarChar, _Chauffeur);

                if (_Transporteur != null)
                    db().AddInParameter(mCommande, "@TransporteurID", SqlDbType.Int, _Transporteur.ID);
                else
                    db().AddInParameter(mCommande, "@TransporteurID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@ProvenanceID", SqlDbType.Int, _Provenance.ID);
                db().AddInParameter(mCommande, "@DestinationID", SqlDbType.Int, _Destination.ID);
                db().AddInParameter(mCommande, "@SacTypeID", SqlDbType.Int, _SacType.ID);
                db().AddInParameter(mCommande, "@SacsDeclares", SqlDbType.Int, _SacsDeclares);
                db().AddInParameter(mCommande, "@PoidsDeclare", SqlDbType.Decimal, _PoidsDeclare);
                db().AddInParameter(mCommande, "@NumeroExterne", SqlDbType.VarChar, _NumeroExterne);

                if (_Transitaire != null)
                    db().AddInParameter(mCommande, "@TransitaireID", SqlDbType.Int, _Transitaire.ID);
                else
                    db().AddInParameter(mCommande, "@TransitaireID", SqlDbType.Int, DBNull.Value);


                db().AddInParameter(mCommande, "@NumLot", SqlDbType.VarChar, _NumLot);
                db().AddInParameter(mCommande, "@NumConteneur", SqlDbType.VarChar, _NumConteneur);
                db().AddInParameter(mCommande, "@NumPlomb", SqlDbType.VarChar, _NumPlomb);
                db().AddInParameter(mCommande, "@NumOT", SqlDbType.VarChar, _NumOT);

                if (_Certification != null)
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, _Certification.ID);
                else
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, DBNull.Value);

                if (_CentreAchat != null)
                    db().AddInParameter(mCommande, "@CentreAchatID", SqlDbType.Int, _CentreAchat.ID);
                else
                    db().AddInParameter(mCommande, "@CentreAchatID", SqlDbType.Int, DBNull.Value);

                if (_TransfertFeves != null)
                    db().AddInParameter(mCommande, "@TransfertFeveID", SqlDbType.UniqueIdentifier, _TransfertFeves.ID);
                else
                    db().AddInParameter(mCommande, "@TransfertFeveID", SqlDbType.UniqueIdentifier, DBNull.Value);

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
                        //base.UpdateAuditFields();
                        Result = true;

                        if (_isnew)
                            _Numero = (string)db().Parameters(mCommande, "@Numero");
                        //if(_isnew)
                        //{
                        //   _NumeroArrivee = (int)db().Parameters(mCommande, "@NumeroOrdreArrivee");
                        //   _NumeroDechargement = (int)db().Parameters(mCommande, "@NumeroOrdreDechargement");
                        //   _NumeroInterne = (string)db().Parameters(mCommande, "@NumeroInterne");                            
                        //}

                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        _ID = (Guid)db().Parameters(mCommande, "@ID");
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
                throw new Exception(ex.Message + "\r\n" + "Fournisseur:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand(prefixeProcedureStockee + "_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.Int, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand(prefixeProcedureStockee + "_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, _Exportateur.ID);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, _Site.ID);
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.VarChar, _Campagne.Designation);
                db().AddInParameter(mCommande, "@RecolteID", SqlDbType.Int, _Recolte.ID);
                db().AddInParameter(mCommande, "@LivraisonDate", SqlDbType.DateTime, _DateLivraison);
                db().AddInParameter(mCommande, "@LivraisonNumero", SqlDbType.VarChar, _Numero);
                db().AddInParameter(mCommande, "@LivraisonTypeID", SqlDbType.Int, _LivraisonType.ID);
                db().AddInParameter(mCommande, "@FournisseurID", SqlDbType.Int, _Fournisseur.ID);
                db().AddInParameter(mCommande, "@Immatriculation", SqlDbType.VarChar, _Immatriculation);
                db().AddInParameter(mCommande, "@Tracteur", SqlDbType.VarChar, _Tracteur);
                db().AddInParameter(mCommande, "@Chauffeur", SqlDbType.VarChar, _Chauffeur);
                db().AddInParameter(mCommande, "@TransporteurID", SqlDbType.Int, _Transporteur.ID);
                db().AddInParameter(mCommande, "@ProvenanceID", SqlDbType.Int, _Provenance.ID);
                db().AddInParameter(mCommande, "@DestinationID", SqlDbType.Int, _Destination.ID);
                db().AddInParameter(mCommande, "@SacTypeID", SqlDbType.Int, _SacType.ID);
                db().AddInParameter(mCommande, "@SacsDeclares", SqlDbType.Int, _SacsDeclares);
                db().AddInParameter(mCommande, "@PoidsDeclare", SqlDbType.Decimal, _PoidsDeclare);
                db().AddInParameter(mCommande, "@NumeroExterne", SqlDbType.VarChar, _NumeroExterne);
                db().AddInParameter(mCommande, "@TransitaireID", SqlDbType.Int, _Transitaire.ID);
                db().AddInParameter(mCommande, "@NumLot", SqlDbType.VarChar, _NumLot);
                db().AddInParameter(mCommande, "@NumConteneur", SqlDbType.VarChar, _NumConteneur);
                db().AddInParameter(mCommande, "@NumPlomb", SqlDbType.VarChar, _NumPlomb);
                db().AddInParameter(mCommande, "@NumOT", SqlDbType.VarChar, _NumOT);
                db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, _Certification.ID);
                //db().AddInParameter(mCommande, "@CentreAchatID", SqlDbType.Int, _CentreAchat.ID);

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
                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        _ID = (Guid)db().Parameters(mCommande, "@ID");
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
                throw new Exception(ex.Message + "\r\n" + this.GetType().FullName + ":fnUpdate");

            }
            return Result;
        }

        public bool fnUpdateSupplier()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand(prefixeProcedureStockee + "_ModifySupplier");
                db().AddInParameter(mCommande, "@LivraisonID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

                db().AddInParameter(mCommande, "@NewIdFournisseur", SqlDbType.Int, _Fournisseur.ID);                                
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
                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        //_ID = (Guid)db().Parameters(mCommande, "@ID");
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
                throw new Exception(ex.Message + "\r\n" + this.GetType().FullName + ":fnUpdateSupplier");

            }
            return Result;
        }

        public bool fnReclassify()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("Livraison_ReclasserCertification");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);                

                if (_Certification != null)
                    db().AddInParameter(mCommande, "@ancienneCertification", SqlDbType.Int, _AncienneCertification.ID);
                else
                    db().AddInParameter(mCommande, "@ancienneCertification", SqlDbType.Int, DBNull.Value);

                if (_Certification != null)
                    db().AddInParameter(mCommande, "@nouvelleCertification", SqlDbType.Int, _NouvelleCertification.ID);
                else
                    db().AddInParameter(mCommande, "@nouvelleCertification", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@raison", SqlDbType.VarChar, _RaisonReclassement);                
                               

                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);

                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                db().ExecuteNonQuery(ref mCommande);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        //base.UpdateAuditFields();
                        Result = true;                                            
                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        //_ID = (Guid)db().Parameters(mCommande, "@BonDeLivraisonID");
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
                throw new Exception(ex.Message + "\r\n" + "Fournisseur:fnUpdate");

            }
            return Result;
        }

        public bool fnChangerType()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("Livraison_ChangerType");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

                if (_Certification != null)
                    db().AddInParameter(mCommande, "@ancienType", SqlDbType.Int, _AncienType.ID);
                else
                    db().AddInParameter(mCommande, "@ancienType", SqlDbType.Int, DBNull.Value);

                if (_Certification != null)
                    db().AddInParameter(mCommande, "@nouveauType", SqlDbType.Int, _NouveauType.ID);
                else
                    db().AddInParameter(mCommande, "@nouveauType", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@raison", SqlDbType.VarChar, _RaisonReclassement);


                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);

                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                db().ExecuteNonQuery(ref mCommande);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        //base.UpdateAuditFields();
                        Result = true;
                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        //_ID = (Guid)db().Parameters(mCommande, "@BonDeLivraisonID");
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
                throw new Exception(ex.Message + "\r\n" + "Fournisseur:fnUpdate");

            }
            return Result;
        }

        public bool fnUpdateOrgDest()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("Livraison_UpdateOrgDest");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

                if (_NouvelleProvenance != null)
                    db().AddInParameter(mCommande, "@nouvelleProvenance", SqlDbType.Int, _NouvelleProvenance.ID);
                else
                    db().AddInParameter(mCommande, "@nouvelleProvenance", SqlDbType.Int, DBNull.Value);

                if (_NouvelleDestination != null)
                    db().AddInParameter(mCommande, "@nouvelleDestination", SqlDbType.Int, _NouvelleDestination.ID);
                else
                    db().AddInParameter(mCommande, "@nouvelleDestination", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@raison", SqlDbType.VarChar, _RaisonReclassement);


                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);

                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                db().ExecuteNonQuery(ref mCommande);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        //base.UpdateAuditFields();
                        Result = true;
                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        //_ID = (Guid)db().Parameters(mCommande, "@BonDeLivraisonID");
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
                throw new Exception(ex.Message + "\r\n" + "Fournisseur:fnUpdate");

            }
            return Result;
        }


        public override bool fnActivate()
        {
            if (!this._isnew)
            {
                bool Result;
                DataCommand mCommande = db().CreateStoredProcCommand(prefixeProcedureStockee + "_Activate");
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
                    throw new Exception(ex.Message + "\r\n" + "Fournisseur:fnActivate");
                }
                return Result;
            }
            return false;
        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("AnalyseCode_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "AnalyseCode:fnDeActivate");
            }
            return bolResult;
        }


        public  bool fnCancel()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("Livraison_Cancel");
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
                throw new Exception(ex.Message + "\r\n" + this.GetType().FullName + ": fnCancel");
            }
            return bolResult;
        }

        public int fnGetWeightStatus(object id)
        {
            IDataReader mDataReader = null;

            try
            {                
                DataCommand mCommand = db().CreateStoredProcCommand("Livraison_GetWeightStatus");

                db().AddInParameter(mCommand, "@ID", SqlDbType.UniqueIdentifier, 50, (Guid)id);
                db().AddParameter(mCommand, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);                
                mDataReader = db().ExecuteReader(mCommand);                

                if (db().Parameters(mCommand, "ReturnValue") != null)
                    return (int)db().Parameters(mCommand, "ReturnValue");
                else
                    return -1;                                            

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetWeightStatus");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectForWeighingInSite(int siteID, int fournisseurID, DateTime? DateDebut, DateTime? DateFin, int LivraisonTypeID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_Livraison_SelectForWeighing");
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, fournisseurID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, DateDebut);
                db().AddInParameter(mCommande, "@DateFin", SqlDbType.DateTime, DateFin);
                db().AddInParameter(mCommande, "@LivraisonTypeID", SqlDbType.Int, LivraisonTypeID);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Livraison mClass = new Livraison();

                    MapFromDataReaderToWeighingSite(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectForWeighingInSite");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectTransferFeveForPurchseDeliveries(int Fournisseur, int siteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_SelectTransfertFevesForPurchaseDeliveries");
                db().AddInParameter(mCommande, "@Fournisseur", SqlDbType.Int, Fournisseur);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Livraison mClass = new Livraison();
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
        public List<DataPersist> fnSelectForAnalyseInSite(int siteID, int fournisseurID, DateTime? DateDebut, DateTime? DateFin)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_Livraison_SelectForAnalyseInSite");
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, fournisseurID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, DateDebut);
                db().AddInParameter(mCommande, "@DateFin", SqlDbType.DateTime, DateFin);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Livraison mClass = new Livraison();

                    MapFromDataReaderToWeighingSite(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectForWeighingInSite");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }


        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _Numero;
        }

        private static void MapFromDataReaderLiteForPurchaseDeliveries(Livraison mClass, IDataReader mDataReader)
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
                        mClass._Site = new Site();
                        mClass._Site.ID = (int)mDataReader["SiteID"];
                        mClass._Site.Nom = (string)mDataReader["SiteNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["NumeroTransfert"]))
                        mClass._Numero = (string)mDataReader["NumeroTransfert"];

                    if (!DBNull.Value.Equals(mDataReader["DateLivraison"]))
                        mClass._DateLivraison = (DateTime)mDataReader["DateLivraison"];

                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"]))
                        mClass._SacsDeclares = (int)mDataReader["NombreSacs"];

                    if (!DBNull.Value.Equals(mDataReader["NumeroBL"]))
                        mClass._NumeroInterne = (string)mDataReader["NumeroBL"];

                    if (!DBNull.Value.Equals(mDataReader["Immatriculation"]))
                        mClass._Immatriculation = (string)mDataReader["Immatriculation"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n TransfertFeves:MapFromDataReader");
            }
        }
        private static void MapFromDataReader(Livraison mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._LivraisonType = new LivraisonType();
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeID"])) mClass._LivraisonType.ID = (int)mDataReader["LivraisonTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeNom"])) mClass._LivraisonType.Designation = (string)mDataReader["LivraisonTypeNom"];

                    mClass._Provenance = new Provenance();
                    if (!DBNull.Value.Equals(mDataReader["ProvenanceID"])) mClass._Provenance.ID = (int)mDataReader["ProvenanceID"];
                    if (!DBNull.Value.Equals(mDataReader["ProvenanceNom"])) mClass._Provenance.Nom = (string)mDataReader["ProvenanceNom"];

                    mClass._Destination = new Destination();
                    if (!DBNull.Value.Equals(mDataReader["DestinationID"])) mClass._Destination.ID = (int)mDataReader["DestinationID"];
                    if (!DBNull.Value.Equals(mDataReader["DestinationNom"])) mClass._Destination.Nom = (string)mDataReader["DestinationNom"];
                    
                    mClass._Exportateur = new Exportateur();
                    if (!DBNull.Value.Equals(mDataReader["ExportateurID"])) mClass._Exportateur.ID = (int)mDataReader["ExportateurID"];
                    if (!DBNull.Value.Equals(mDataReader["ExportateurNom"])) mClass._Exportateur.Nom = (string)mDataReader["ExportateurNom"];

                    mClass._Site = new Site();
                    if (!DBNull.Value.Equals(mDataReader["SiteID"])) mClass._Site.ID = (int)mDataReader["SiteID"];
                    if (!DBNull.Value.Equals(mDataReader["SiteNom"])) mClass._Site.Nom = (string)mDataReader["SiteNom"];

                    mClass._CentreAchat = new Site();
                    if (!DBNull.Value.Equals(mDataReader["CentreAchat"])) mClass._CentreAchat.ID = (Int16)mDataReader["CentreAchat"];
                    //if (!DBNull.Value.Equals(mDataReader["SiteNom"])) mClass._Site.Nom = (string)mDataReader["SiteNom"];

                    mClass._Recolte = new Recolte();
                    if (!DBNull.Value.Equals(mDataReader["RecolteID"])) mClass._Recolte.ID = (int)mDataReader["RecolteID"];
                    if (!DBNull.Value.Equals(mDataReader["RecolteNom"])) mClass._Recolte.Designation = (string)mDataReader["RecolteNom"];

                    mClass._Transporteur = new Transporteur();
                    if (!DBNull.Value.Equals(mDataReader["TransporteurID"])) mClass._Transporteur.ID = (int)mDataReader["TransporteurID"];
                    if (!DBNull.Value.Equals(mDataReader["TransporteurNom"])) mClass._Transporteur.Nom = (string)mDataReader["TransporteurNom"];

                    mClass._Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["Campagne"])) mClass._Campagne.Designation = (string)mDataReader["Campagne"];
                    if (!DBNull.Value.Equals(mDataReader["CampagneDebut"])) mClass._Campagne.DateDebut = (DateTime)mDataReader["CampagneDebut"];
                    if (!DBNull.Value.Equals(mDataReader["CampagneFin"])) mClass._Campagne.DateFin = (DateTime)mDataReader["CampagneFin"];

                    mClass._Fournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["FournisseurID"])) mClass._Fournisseur.ID = (int)mDataReader["FournisseurID"];
                    if (!DBNull.Value.Equals(mDataReader["FournisseurNom"])) mClass._Fournisseur.Nom = (string)mDataReader["FournisseurNom"];
                    
                    if (!DBNull.Value.Equals(mDataReader["LivraisonDate"])) mClass._DateLivraison = (DateTime)mDataReader["LivraisonDate"];
                    
                    if (!DBNull.Value.Equals(mDataReader["LivraisonNumero"])) mClass._Numero = (string)mDataReader["LivraisonNumero"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonImmatriculation"])) mClass._Immatriculation = (string)mDataReader["LivraisonImmatriculation"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTracteur"])) mClass._Tracteur = (string)mDataReader["LivraisonTracteur"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonChauffeur"])) mClass._Chauffeur = (string)mDataReader["LivraisonChauffeur"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonSacsDeclares"])) mClass._SacsDeclares = (int)mDataReader["LivraisonSacsDeclares"];
                    
                    mClass._SacType = new SacType();
                    if (!DBNull.Value.Equals(mDataReader["SacTypeID"])) mClass._SacType.ID = (int)mDataReader["SacTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["SacTypeNom"])) mClass._SacType.Designation = (string)mDataReader["SacTypeNom"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonPoidsDeclare"])) mClass._PoidsDeclare = (decimal)mDataReader["LivraisonPoidsDeclare"];

                    
                    if (!DBNull.Value.Equals(mDataReader["LivraisonNumeroExterne"])) mClass._NumeroExterne = (string)mDataReader["LivraisonNumeroExterne"];
                    if (!DBNull.Value.Equals(mDataReader["NumLot"])) mClass._NumLot = (string)mDataReader["NumLot"];
                    if (!DBNull.Value.Equals(mDataReader["NumConteneur"])) mClass._NumConteneur = (string)mDataReader["NumConteneur"];
                    if (!DBNull.Value.Equals(mDataReader["NumPlomb"])) mClass._NumPlomb = (string)mDataReader["NumPlomb"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroOT"])) mClass._NumOT = (string)mDataReader["NumeroOT"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroOT"])) mClass._NumOT = (string)mDataReader["NumeroOT"];

                    mClass._Certification = new Certification();
                    if (!DBNull.Value.Equals(mDataReader["CertificationID"])) mClass._Certification.ID = (int)mDataReader["CertificationID"];
                    if (!DBNull.Value.Equals(mDataReader["CertificationNom"])) mClass._Certification.Designation = (string)mDataReader["CertificationNom"];

                    mClass._Transitaire = new Transitaire();
                    if (!DBNull.Value.Equals(mDataReader["TransitaireID"])) mClass._Transitaire.ID = (int)mDataReader["TransitaireID"];
                    if (!DBNull.Value.Equals(mDataReader["TransitaireNom"])) mClass._Transitaire.Nom = (string)mDataReader["TransitaireNom"];

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desative = (bool)mDataReader["Desactive"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];


                    //if (!DBNull.Value.Equals(mDataReader["NumeroOrdreArrivee"])) mClass._NumeroArrivee = (int)mDataReader["NumeroOrdreArrivee"]; 
                    //if (!DBNull.Value.Equals(mDataReader["NumeroOrdreDechargement"])) mClass._NumeroDechargement = (int)mDataReader["NumeroOrdreDechargement"];
                    //if (!DBNull.Value.Equals(mDataReader["NumeroInterne"])) mClass._NumeroInterne = (string)mDataReader["NumeroInterne"];

                    mClass._TransfertFeves = new TransfertFeves();
                    if (!DBNull.Value.Equals(mDataReader["TransfertFevesID"])) mClass._TransfertFeves.ID = (Guid)mDataReader["TransfertFevesID"];
                    if (!DBNull.Value.Equals(mDataReader["TransfertFevesNumero"])) mClass._TransfertFeves.Numero = (string)mDataReader["TransfertFevesNumero"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nLivraison:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderSP(Livraison mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._LivraisonType = new LivraisonType();
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeID"])) mClass._LivraisonType.ID = (int)mDataReader["LivraisonTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeNom"])) mClass._LivraisonType.Designation = (string)mDataReader["LivraisonTypeNom"];                    

                    mClass._Exportateur = new Exportateur();
                    if (!DBNull.Value.Equals(mDataReader["ExportateurID"])) mClass._Exportateur.ID = (int)mDataReader["ExportateurID"];
                    if (!DBNull.Value.Equals(mDataReader["ExportateurNom"])) mClass._Exportateur.Nom = (string)mDataReader["ExportateurNom"];

                    mClass._Site = new Site();
                    if (!DBNull.Value.Equals(mDataReader["SiteID"])) mClass._Site.ID = (int)mDataReader["SiteID"];
                    if (!DBNull.Value.Equals(mDataReader["SiteNom"])) mClass._Site.Nom = (string)mDataReader["SiteNom"];

                    mClass._CentreAchat = new Site();
                    if (!DBNull.Value.Equals(mDataReader["CentreAchat"])) mClass._CentreAchat.ID = (Int16)mDataReader["CentreAchat"];
                    //if (!DBNull.Value.Equals(mDataReader["SiteNom"])) mClass._Site.Nom = (string)mDataReader["SiteNom"];

                    mClass._Recolte = new Recolte();
                    if (!DBNull.Value.Equals(mDataReader["RecolteID"])) mClass._Recolte.ID = (int)mDataReader["RecolteID"];
                    if (!DBNull.Value.Equals(mDataReader["RecolteNom"])) mClass._Recolte.Designation = (string)mDataReader["RecolteNom"];

                    mClass._Transporteur = new Transporteur();
                    if (!DBNull.Value.Equals(mDataReader["TransporteurID"])) mClass._Transporteur.ID = (int)mDataReader["TransporteurID"];
                    if (!DBNull.Value.Equals(mDataReader["TransporteurNom"])) mClass._Transporteur.Nom = (string)mDataReader["TransporteurNom"];

                    mClass._Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["Campagne"])) mClass._Campagne.Designation = (string)mDataReader["Campagne"];
                    if (!DBNull.Value.Equals(mDataReader["CampagneDebut"])) mClass._Campagne.DateDebut = (DateTime)mDataReader["CampagneDebut"];
                    if (!DBNull.Value.Equals(mDataReader["CampagneFin"])) mClass._Campagne.DateFin = (DateTime)mDataReader["CampagneFin"];

                    mClass._Fournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["FournisseurID"])) mClass._Fournisseur.ID = (int)mDataReader["FournisseurID"];
                    if (!DBNull.Value.Equals(mDataReader["FournisseurNom"])) mClass._Fournisseur.Nom = (string)mDataReader["FournisseurNom"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonDate"])) mClass._DateLivraison = (DateTime)mDataReader["LivraisonDate"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonNumero"])) mClass._Numero = (string)mDataReader["LivraisonNumero"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonImmatriculation"])) mClass._Immatriculation = (string)mDataReader["LivraisonImmatriculation"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTracteur"])) mClass._Tracteur = (string)mDataReader["LivraisonTracteur"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonChauffeur"])) mClass._Chauffeur = (string)mDataReader["LivraisonChauffeur"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonSacsDeclares"])) mClass._SacsDeclares = (int)mDataReader["LivraisonSacsDeclares"];

                    mClass._SacType = new SacType();
                    if (!DBNull.Value.Equals(mDataReader["SacTypeID"])) mClass._SacType.ID = (int)mDataReader["SacTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["SacTypeNom"])) mClass._SacType.Designation = (string)mDataReader["SacTypeNom"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonPoidsDeclare"])) mClass._PoidsDeclare = (decimal)mDataReader["LivraisonPoidsDeclare"];


                    if (!DBNull.Value.Equals(mDataReader["LivraisonNumeroExterne"])) mClass._NumeroExterne = (string)mDataReader["LivraisonNumeroExterne"];
                    if (!DBNull.Value.Equals(mDataReader["NumLot"])) mClass._NumLot = (string)mDataReader["NumLot"];
                    if (!DBNull.Value.Equals(mDataReader["NumConteneur"])) mClass._NumConteneur = (string)mDataReader["NumConteneur"];
                    if (!DBNull.Value.Equals(mDataReader["NumPlomb"])) mClass._NumPlomb = (string)mDataReader["NumPlomb"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroOT"])) mClass._NumOT = (string)mDataReader["NumeroOT"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroOT"])) mClass._NumOT = (string)mDataReader["NumeroOT"];

                    mClass._Certification = new Certification();
                    if (!DBNull.Value.Equals(mDataReader["CertificationID"])) mClass._Certification.ID = (int)mDataReader["CertificationID"];
                    if (!DBNull.Value.Equals(mDataReader["CertificationNom"])) mClass._Certification.Designation = (string)mDataReader["CertificationNom"];

                    mClass._Transitaire = new Transitaire();
                    if (!DBNull.Value.Equals(mDataReader["TransitaireID"])) mClass._Transitaire.ID = (int)mDataReader["TransitaireID"];
                    if (!DBNull.Value.Equals(mDataReader["TransitaireNom"])) mClass._Transitaire.Nom = (string)mDataReader["TransitaireNom"];

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desative = (bool)mDataReader["Desactive"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];


                    //if (!DBNull.Value.Equals(mDataReader["NumeroOrdreArrivee"])) mClass._NumeroArrivee = (int)mDataReader["NumeroOrdreArrivee"]; 
                    //if (!DBNull.Value.Equals(mDataReader["NumeroOrdreDechargement"])) mClass._NumeroDechargement = (int)mDataReader["NumeroOrdreDechargement"];
                    //if (!DBNull.Value.Equals(mDataReader["NumeroInterne"])) mClass._NumeroInterne = (string)mDataReader["NumeroInterne"];

                    mClass._TransfertFeves = new TransfertFeves();
                    if (!DBNull.Value.Equals(mDataReader["TransfertFevesID"])) mClass._TransfertFeves.ID = (Guid)mDataReader["TransfertFevesID"];
                    if (!DBNull.Value.Equals(mDataReader["TransfertFevesNumero"])) mClass._TransfertFeves.Numero = (string)mDataReader["TransfertFevesNumero"];

                    mClass._Provenance = new Provenance();
                    if (!DBNull.Value.Equals(mDataReader["ProvenanceID"])) mClass._Provenance.ID = (int)mDataReader["ProvenanceID"];
                    if (!DBNull.Value.Equals(mDataReader["ProvenanceNom"])) mClass._Provenance.Nom = (string)mDataReader["ProvenanceNom"];

                    mClass._Destination = new Destination();
                    if (!DBNull.Value.Equals(mDataReader["DestinationID"])) mClass._Destination.ID = (int)mDataReader["DestinationID"];
                    if (!DBNull.Value.Equals(mDataReader["DestinationNom"])) mClass._Destination.Nom = (string)mDataReader["DestinationNom"];

                    mClass._NouvelleDestination = new Destination();
                    if (!DBNull.Value.Equals(mDataReader["DestinationSecID"])) mClass._NouvelleDestination.ID = (int)mDataReader["DestinationSecID"];
                    mClass._NouvelleProvenance = new Provenance();
                    if (!DBNull.Value.Equals(mDataReader["ProvenanceSecID"])) mClass._NouvelleProvenance.ID = (int)mDataReader["ProvenanceSecID"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nLivraison:MapFromDataReader");
            }
        }


        private static void MapFromDataReaderClassification(Livraison mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._LivraisonType = new LivraisonType();
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeID"])) mClass._LivraisonType.ID = (int)mDataReader["LivraisonTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeNom"])) mClass._LivraisonType.Designation = (string)mDataReader["LivraisonTypeNom"];

                    mClass._Provenance = new Provenance();
                    if (!DBNull.Value.Equals(mDataReader["ProvenanceID"])) mClass._Provenance.ID = (int)mDataReader["ProvenanceID"];
                    if (!DBNull.Value.Equals(mDataReader["ProvenanceNom"])) mClass._Provenance.Nom = (string)mDataReader["ProvenanceNom"];

                    mClass._Destination = new Destination();
                    if (!DBNull.Value.Equals(mDataReader["DestinationID"])) mClass._Destination.ID = (int)mDataReader["DestinationID"];
                    if (!DBNull.Value.Equals(mDataReader["DestinationNom"])) mClass._Destination.Nom = (string)mDataReader["DestinationNom"];

                    mClass._Exportateur = new Exportateur();
                    if (!DBNull.Value.Equals(mDataReader["ExportateurID"])) mClass._Exportateur.ID = (int)mDataReader["ExportateurID"];
                    if (!DBNull.Value.Equals(mDataReader["ExportateurNom"])) mClass._Exportateur.Nom = (string)mDataReader["ExportateurNom"];


                    mClass._Site = new Site();
                    if (!DBNull.Value.Equals(mDataReader["SiteID"])) mClass._Site.ID = (int)mDataReader["SiteID"];
                    if (!DBNull.Value.Equals(mDataReader["SiteNom"])) mClass._Site.Nom = (string)mDataReader["SiteNom"];

                    mClass._CentreAchat = new Site();
                    if (!DBNull.Value.Equals(mDataReader["CentreAchat"])) mClass._CentreAchat.ID = (Int16)mDataReader["CentreAchat"];
                    //if (!DBNull.Value.Equals(mDataReader["SiteNom"])) mClass._Site.Nom = (string)mDataReader["SiteNom"];

                    mClass._Recolte = new Recolte();
                    if (!DBNull.Value.Equals(mDataReader["RecolteID"])) mClass._Recolte.ID = (int)mDataReader["RecolteID"];
                    if (!DBNull.Value.Equals(mDataReader["RecolteNom"])) mClass._Recolte.Designation = (string)mDataReader["RecolteNom"];

                    mClass._Transporteur = new Transporteur();
                    if (!DBNull.Value.Equals(mDataReader["TransporteurID"])) mClass._Transporteur.ID = (int)mDataReader["TransporteurID"];
                    if (!DBNull.Value.Equals(mDataReader["TransporteurNom"])) mClass._Transporteur.Nom = (string)mDataReader["TransporteurNom"];

                    mClass._Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["Campagne"])) mClass._Campagne.Designation = (string)mDataReader["Campagne"];
                    if (!DBNull.Value.Equals(mDataReader["CampagneDebut"])) mClass._Campagne.DateDebut = (DateTime)mDataReader["CampagneDebut"];
                    if (!DBNull.Value.Equals(mDataReader["CampagneFin"])) mClass._Campagne.DateFin = (DateTime)mDataReader["CampagneFin"];

                    mClass._Fournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["FournisseurID"])) mClass._Fournisseur.ID = (int)mDataReader["FournisseurID"];
                    if (!DBNull.Value.Equals(mDataReader["FournisseurNom"])) mClass._Fournisseur.Nom = (string)mDataReader["FournisseurNom"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonDate"])) mClass._DateLivraison = (DateTime)mDataReader["LivraisonDate"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonNumero"])) mClass._Numero = (string)mDataReader["LivraisonNumero"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonImmatriculation"])) mClass._Immatriculation = (string)mDataReader["LivraisonImmatriculation"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTracteur"])) mClass._Tracteur = (string)mDataReader["LivraisonTracteur"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonChauffeur"])) mClass._Chauffeur = (string)mDataReader["LivraisonChauffeur"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonSacsDeclares"])) mClass._SacsDeclares = (int)mDataReader["LivraisonSacsDeclares"];

                    mClass._SacType = new SacType();
                    if (!DBNull.Value.Equals(mDataReader["SacTypeID"])) mClass._SacType.ID = (int)mDataReader["SacTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["SacTypeNom"])) mClass._SacType.Designation = (string)mDataReader["SacTypeNom"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonPoidsDeclare"])) mClass._PoidsDeclare = (decimal)mDataReader["LivraisonPoidsDeclare"];


                    if (!DBNull.Value.Equals(mDataReader["LivraisonNumeroExterne"])) mClass._NumeroExterne = (string)mDataReader["LivraisonNumeroExterne"];
                    if (!DBNull.Value.Equals(mDataReader["NumLot"])) mClass._NumLot = (string)mDataReader["NumLot"];
                    if (!DBNull.Value.Equals(mDataReader["NumConteneur"])) mClass._NumConteneur = (string)mDataReader["NumConteneur"];
                    if (!DBNull.Value.Equals(mDataReader["NumPlomb"])) mClass._NumPlomb = (string)mDataReader["NumPlomb"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroOT"])) mClass._NumOT = (string)mDataReader["NumeroOT"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroOT"])) mClass._NumOT = (string)mDataReader["NumeroOT"];

                    mClass._Certification = new Certification();
                    if (!DBNull.Value.Equals(mDataReader["CertificationID"])) mClass._Certification.ID = (int)mDataReader["CertificationID"];
                    if (!DBNull.Value.Equals(mDataReader["CertificationNom"])) mClass._Certification.Designation = (string)mDataReader["CertificationNom"];

                    mClass._Transitaire = new Transitaire();
                    if (!DBNull.Value.Equals(mDataReader["TransitaireID"])) mClass._Transitaire.ID = (int)mDataReader["TransitaireID"];
                    if (!DBNull.Value.Equals(mDataReader["TransitaireNom"])) mClass._Transitaire.Nom = (string)mDataReader["TransitaireNom"];

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desative = (bool)mDataReader["Desactive"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                    mClass._AncienneCertification = new Certification();
                    if (!DBNull.Value.Equals(mDataReader["AncienneCertificationID"])) mClass._AncienneCertification.ID = (int)mDataReader["AncienneCertificationID"];
                    //if (!DBNull.Value.Equals(mDataReader["CertificationNom"])) mClass._AncienneCertification.Designation = (string)mDataReader["CertificationNom"];

                    mClass._NouvelleCertification = new Certification();
                    if (!DBNull.Value.Equals(mDataReader["NouvelleCertificationID"])) mClass._NouvelleCertification.ID = (int)mDataReader["NouvelleCertificationID"];
                    //if (!DBNull.Value.Equals(mDataReader["CertificationNom"])) mClass._NouvelleCertification.Designation = (string)mDataReader["CertificationNom"];

                    if (!DBNull.Value.Equals(mDataReader["EstReclassee"])) mClass._EstReclassee = (bool)mDataReader["EstReclassee"];
                    if (!DBNull.Value.Equals(mDataReader["DateReclassement"])) mClass._DateReclassement = (DateTime)mDataReader["DateReclassement"];
                    if (!DBNull.Value.Equals(mDataReader["UtilisateurReclassement"])) mClass._UtilisateurReclassement = (string)mDataReader["UtilisateurReclassement"];
                    if (!DBNull.Value.Equals(mDataReader["RaisonReclassement"])) mClass._RaisonReclassement = (string)mDataReader["RaisonReclassement"];

                    //if (!DBNull.Value.Equals(mDataReader["NumeroOrdreArrivee"])) mClass._NumeroArrivee = (int)mDataReader["NumeroOrdreArrivee"]; 
                    //if (!DBNull.Value.Equals(mDataReader["NumeroOrdreDechargement"])) mClass._NumeroDechargement = (int)mDataReader["NumeroOrdreDechargement"];
                    //if (!DBNull.Value.Equals(mDataReader["NumeroInterne"])) mClass._NumeroInterne = (string)mDataReader["NumeroInterne"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nLivraison:MapFromDataReader");
            }
        }


        private static void MapFromDataReaderToSpotPrice(Livraison mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];                                                                           
                    
                    mClass._Fournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["FournisseurID"])) mClass._Fournisseur.ID = (int)mDataReader["FournisseurID"];
                    if (!DBNull.Value.Equals(mDataReader["FournisseurNom"])) mClass._Fournisseur.Nom = (string)mDataReader["FournisseurNom"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonDate"])) mClass._DateLivraison = (DateTime)mDataReader["LivraisonDate"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonNumero"])) mClass._Numero = (string)mDataReader["LivraisonNumero"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonImmatriculation"])) mClass._Immatriculation = (string)mDataReader["LivraisonImmatriculation"];                                                          

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desative = (bool)mDataReader["Desactive"];

                    mClass._LivraisonType = new LivraisonType();
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeID"])) mClass._LivraisonType.ID = (int)mDataReader["LivraisonTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeNom"])) mClass._LivraisonType.Designation = (string)mDataReader["LivraisonTypeNom"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nLivraison:MapFromDataReaderToSpotPrice");
            }
        }

        private static void MapFromDataReaderToFinalizing(Livraison mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._Fournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["FournisseurID"])) mClass._Fournisseur.ID = (int)mDataReader["FournisseurID"];
                    if (!DBNull.Value.Equals(mDataReader["FournisseurNom"])) mClass._Fournisseur.Nom = (string)mDataReader["FournisseurNom"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonDate"])) mClass._DateLivraison = (DateTime)mDataReader["LivraisonDate"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonNumero"])) mClass._Numero = (string)mDataReader["LivraisonNumero"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonImmatriculation"])) mClass._Immatriculation = (string)mDataReader["LivraisonImmatriculation"];
                                       
                    mClass._LivraisonType = new LivraisonType();
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeID"])) mClass._LivraisonType.ID = (int)mDataReader["LivraisonTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeNom"])) mClass._LivraisonType.Designation = (string)mDataReader["LivraisonTypeNom"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonEstAchat"])) mClass._LivraisonType.EstAchat = (bool)mDataReader["LivraisonEstAchat"];

                    if (!DBNull.Value.Equals(mDataReader["PoidsLivre"])) mClass._PoidsLivre = (decimal)mDataReader["PoidsLivre"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalettes = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["SacsAcceptes"])) mClass._SacsAcceptes = (int)mDataReader["SacsAcceptes"];

                    mClass._Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["Campagne"])) mClass._Campagne.Designation = (string)mDataReader["Campagne"];
                    if (!DBNull.Value.Equals(mDataReader["CampagneDebut"])) mClass._Campagne.DateDebut = (DateTime)mDataReader["CampagneDebut"];
                    if (!DBNull.Value.Equals(mDataReader["CampagneFin"])) mClass._Campagne.DateFin = (DateTime)mDataReader["CampagneFin"];

                    mClass._SacType = new SacType();
                    if (!DBNull.Value.Equals(mDataReader["SacTypeID"])) mClass._SacType.ID = (int)mDataReader["SacTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["SacTypeNom"])) mClass._SacType.Designation = (string)mDataReader["SacTypeNom"];

                    mClass._Certification = new Certification();
                    if (!DBNull.Value.Equals(mDataReader["CertificationID"])) mClass._Certification.ID = (int)mDataReader["CertificationID"];
                    if (!DBNull.Value.Equals(mDataReader["CertificationNom"])) mClass._Certification.Designation = (string)mDataReader["CertificationNom"];

                    mClass._Site = new Site();
                    if (!DBNull.Value.Equals(mDataReader["SiteID"])) mClass._Site.ID = (int)mDataReader["SiteID"];
                    if (!DBNull.Value.Equals(mDataReader["SiteNom"])) mClass._Site.Nom = (string)mDataReader["SiteNom"];


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nLivraison:MapFromDataReaderToFinalizing");
            }
        }


        private static void MapFromDataReaderToAnalyseCode(Livraison mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._Fournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["FournisseurID"])) mClass._Fournisseur.ID = (int)mDataReader["FournisseurID"];
                    if (!DBNull.Value.Equals(mDataReader["FournisseurNom"])) mClass._Fournisseur.Nom = (string)mDataReader["FournisseurNom"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonDate"])) mClass._DateLivraison = (DateTime)mDataReader["LivraisonDate"];
                    if (!DBNull.Value.Equals(mDataReader["CodesAnalyse"])) mClass._CodesAnalyse = (string)mDataReader["CodesAnalyse"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonNumero"])) mClass._Numero = (string)mDataReader["LivraisonNumero"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonImmatriculation"])) mClass._Immatriculation = (string)mDataReader["LivraisonImmatriculation"];

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desative = (bool)mDataReader["Desactive"];

                    mClass._LivraisonType = new LivraisonType();
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeID"])) mClass._LivraisonType.ID = (int)mDataReader["LivraisonTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeNom"])) mClass._LivraisonType.Designation = (string)mDataReader["LivraisonTypeNom"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nLivraison:MapFromDataReaderToSpotPrice");
            }
        }

        private static void MapFromDataReaderToWeighingSite(Livraison mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._Fournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["FournisseurID"])) mClass._Fournisseur.ID = (int)mDataReader["FournisseurID"];
                    if (!DBNull.Value.Equals(mDataReader["FournisseurNom"])) mClass._Fournisseur.Nom = (string)mDataReader["FournisseurNom"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonDate"])) mClass._DateLivraison = (DateTime)mDataReader["LivraisonDate"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonNumero"])) mClass._Numero = (string)mDataReader["LivraisonNumero"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonImmatriculation"])) mClass._Immatriculation = (string)mDataReader["LivraisonImmatriculation"];

                    mClass._LivraisonType = new LivraisonType();
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeID"])) mClass._LivraisonType.ID = (int)mDataReader["LivraisonTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeNom"])) mClass._LivraisonType.Designation = (string)mDataReader["LivraisonTypeNom"];

                    if (!DBNull.Value.Equals(mDataReader["SacDeclare"])) mClass._SacsDeclares = (int)mDataReader["SacDeclare"];                    
                    if (!DBNull.Value.Equals(mDataReader["PoidsDeclare"])) mClass._PoidsDeclare = (decimal)mDataReader["PoidsDeclare"];

                    mClass._Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["Campagne"])) mClass._Campagne.Designation = (string)mDataReader["Campagne"];                    

                    mClass._SacType = new SacType();
                    if (!DBNull.Value.Equals(mDataReader["SacTypeID"])) mClass._SacType.ID = (int)mDataReader["SacTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["SacTypeNom"])) mClass._SacType.Designation = (string)mDataReader["SacTypeNom"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacUnitaire"])) mClass._SacType.Tare = (decimal)mDataReader["TareSacUnitaire"];

                    if (!DBNull.Value.Equals(mDataReader["TareSacUnitaire"])) mClass._TareSacs = (decimal)mDataReader["TareSacUnitaire"];

                    mClass._Certification = new Certification();
                    if (!DBNull.Value.Equals(mDataReader["CertificationID"])) mClass._Certification.ID = (int)mDataReader["CertificationID"];
                    if (!DBNull.Value.Equals(mDataReader["CertificationNom"])) mClass._Certification.Designation = (string)mDataReader["CertificationNom"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nLivraison:MapFromDataReaderToFinalizing");
            }
        }

        #endregion

    }


    public class LivraisonViewModel
    {
        public Livraison _Livraison { get; set; }

        public Recolte _Recolte { get; set; }

        public string _Campagne { get; set; }

        public Exportateur _Exportateur { get; set; }

        public Site _SiteUser { get; set; }

        //public StatutLivraison _StatutLivraison { get; set; }

        public Site _DefaultSite
        {
            get 
            {
                Site mSite = new Site();
                mSite.fnGetDefaultSite();
                return mSite;
            } 
        }

        //public StatutLivraison _StatutLivraison { get; }
        public Parametres _Parametres
        {
            get {
                Parametres mParam = new Parametres(0);
                return mParam;
            }        
            set { }    
        }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }

    public partial class StatutLivraison
    {
        public const string Default = "Default";
        public const string NotWeighed = "NotWeighed";
        public const string FirstWeight = "FirstWeight";
        public const string SecondWeight = "SecondWeight";

        public enum Val
        {
            Default = -1,
            NotWeighed = 0,
            FirstWeight = 1,
            SecondWeight = 2
        };
    }
}

    

