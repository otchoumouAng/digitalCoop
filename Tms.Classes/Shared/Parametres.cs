using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;


namespace Tms.Classes.Shared
{   
    public class Parametres : DataPersist
    {
        #region "Fields"
   
        private Int16 _Site;
        private string _Campagne;
        private Double _PrixNegocie;
        private Double _PrixNegocieAgence;

        Recolte _Recolte;
        Exportateur _Exportateur;
        Laboratoire _Laboratoire;

        //private decimal _StandardHumidite;
        //private decimal _StandardMatiereEtrangere;
        //private decimal _StandardBrisure;
        string _Base_url;
        private int _MaxCodeAnalyseGenere;
        ContratPeriodeType _ContratPeriodeType;
        private string _NomSociete;
        private string _AdresseSociete;
        private string _TelSociete;
        private string _Tel2Societe;
        PayementMode _PayementMode;
        PrixNegocieModeApplication _PrixNegocieModeApplication;
        FactureDeductionType _FactureDeductionType;
        private int _FactureDeductionTypeAvance;
        private int _FactureDeductionTypeMec;
        private int _FactureDeductionTypeLegalTax;
        private int _FinancementModeRemboursementLibre;
        private int _FinancementModeRemboursementIntegral;
        private int _FinancementModeRemboursementAuKilo;
        private int _FinancementModeRemboursementFixedAmount;
        private int _MecPlanifieeModeRemboursementAuKilo;
        private int _FinancementTypeAvance;
        private int _ContratPeriodeTypeFinancement;
        private int _ChefAgenceType;
        private int _SacNylonTypeID;
        private int _AjustementStockType;
        private int _RegularisationStockType;
        private Magasin _Magasin;
        private TypeElementStock _TypeElementStock;
        private MouvementStockType _MouvementStockType;
        private Emplacement _Emplacement;

        private string _MagasinTvDesignation;
        private string _MagasinExportDesignation;        

        private int _FevesUsinageStockType;
        private int _LivraisonTypeElementStock;

        private int _PeseeAvantUsinage;
        private int _PeseeApresUsinage;
        private int _PeseeApresReUsinage;
        private int _PeseeCertifiee;
        private int _PeseeDiverse;
        private int _PeseeAvantEmpotage;
        private int _MagasinTV;
        private int _MagasinExport;

        private int _LotOrdinaire;
        private int _LotCertifie;
        private int _SacsLivraisonsAutoriseParPalette;
        private int _NombrePalettesAutoriseAuPesePalette;

        private int _PrefinancementTypeApc;
        private int _PrefinancementTypeBlank;
        private double _ValeurTauxEuro;
        private int _EmplacementParDefaut;

        private int _PoidsStdBrutUnitaire;
        private int _PoidsStdNetUnitaire;
        private int _ReusinageAnnulationLotMvtType;
        private int _LotTypeElementEnStock;
        private int _SacExportType;
        private int _ConstitutionLotCertifie;
        private int _LaboExport;
        private int _NbrOfLotsForChemicalAnalysis;
        private string _PrefixeContratNum;
        private int _ClientDefID;

        private int _ConditionnementDefID;
        private int _OrigineContratExportDefID;
        private int _DefaultModeEnregistrementVente;
        private int _LaboTv;

        private LivraisonType _LivraisonTypeAchat;
        private decimal _FactureFinaleBonusTaux;
        private int _ContratPaiementTermeDefault;
        private string _ContratOptionPoidsDefault;
        private int _ContratDeliveryCondition;
        private string _PrefixeContratExportEnregistrement;
        private int _DefaultTransitaireShipment;
        private int _DefaultConteneurType;
        private decimal _ContratVenteBonusCertificationValue;
        private decimal _FobDiscountPriceDefault;
        private int _IDRetourEnStockLotMvtType;
        private int _IDEngagementType;
        private int _IDEngagementFinancementType;
        private int _IDPrixNegociePourLivraisons;
        private decimal _FinancementTauxPaiement;
        private int _IDTransfertFevesAgence;
        private int _IDDestinationParDefaut;
        private int _NbrSacAutorisePeseeSurSite;
        private int _AchatBrousseMvtType;
        private int _SacBrousseType;
        private int _MvtTypeVenteLot;
        private int _CertificationOrdinaire;
        private string _NumeroCCC;
        private int _GroupeFrsCacaoID;
        private int _DefTransfertChargementLibre;

        private Produit _Produit;
        private bool _IsInDevelopment;
        private int _RedevanceCacao;
        private int _RedevanceRtc;
        private int _idTaxeRtc;
        private int _IdProduitExport;
        private int _PaiementOptionId;
        #endregion

        #region Properties
        public Int16 Site
        {
            get { return _Site; }
            set { _Site = value; }
        }

        public Produit Produit
        {
            get { return _Produit; }
            set { _Produit = value; }
        }
        public string Campagne
        {
            get { return _Campagne; }
            set { _Campagne = value; }
        }

        public Double PrixNegocie
        {
            get { return _PrixNegocie; }
            set { _PrixNegocie = value; }
        }

        public Double PrixNegocieAgence
        {
            get { return _PrixNegocieAgence; }
            set { _PrixNegocieAgence = value; }
        }

        //public decimal StandardHumidite
        //{
        //    get { return _StandardHumidite; }
        //    set { _StandardHumidite = value; }
        //}

        //public decimal StandardBrisure
        //{
        //    get { return _StandardBrisure; }
        //    set { _StandardBrisure = value; }
        //}

        //public decimal StandardMatiereEtrangere
        //{
        //    get { return _StandardMatiereEtrangere; }
        //    set { _StandardMatiereEtrangere = value; }
        //}
        public Recolte Recolte
        {
            get
            {
                return _Recolte;
            }

            set
            {
                _Recolte = value;
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

        public Laboratoire Laboratoire
        {
            get
            {
                return _Laboratoire;
            }

            set
            {
                _Laboratoire = value;
            }
        }

        public string Base_url
        {
            get
            {
                return _Base_url;
            }

            set
            {
                _Base_url = value;
            }
        }

        public int MaxCodeAnalyseGenere
        {
            get { return _MaxCodeAnalyseGenere; }
            set { _MaxCodeAnalyseGenere = value; }
        }

        public ContratPeriodeType ContratPeriodeType
        {
            get { return _ContratPeriodeType; }
            set { _ContratPeriodeType = value; }
        }

        public string NomSociete
        {
            get { return _NomSociete; }
            set { _NomSociete = value; }
        }

        public string AdresseSociete
        {
            get { return _AdresseSociete; }
            set { _AdresseSociete = value; }
        }

        public string TelSociete
        {
            get { return _TelSociete; }
            set { _TelSociete = value; }
        }

        public PayementMode PayementMode
        {
            get { return _PayementMode; }
            set { _PayementMode = value; }
        }

        public PrixNegocieModeApplication PrixNegocieModeApplication
        {
            get
            {
                return _PrixNegocieModeApplication;
            }

            set
            {
                _PrixNegocieModeApplication = value;
            }
        }

        public FactureDeductionType FactureDeductionType
        {
            get
            {
                return _FactureDeductionType;
            }

            set
            {
                _FactureDeductionType = value;
            }
        }

        public int FactureDeductionTypeAvance
        {
            get { return _FactureDeductionTypeAvance; }
            set { _FactureDeductionTypeAvance = value; }
        }

        public int FactureDeductionTypeMec
        {
            get { return _FactureDeductionTypeMec; }
            set { _FactureDeductionTypeMec = value; }
        }

        public int FactureDeductionTypeLegalTax
        {
            get { return _FactureDeductionTypeLegalTax; }
            set { _FactureDeductionTypeLegalTax = value; }
        }

        public int FinancementModeRemboursementLibre
        {
            get
            {
                return _FinancementModeRemboursementLibre;
            }

            set
            {
                _FinancementModeRemboursementLibre = value;
            }
        }

        public int FinancementModeRemboursementIntegral
        {
            get
            {
                return _FinancementModeRemboursementIntegral;
            }

            set
            {
                _FinancementModeRemboursementIntegral = value;
            }
        }

        public int FinancementModeRemboursementAuKilo
        {
            get
            {
                return _FinancementModeRemboursementAuKilo;
            }

            set
            {
                _FinancementModeRemboursementAuKilo = value;
            }
        }

        public int FinancementModeRemboursementFixedAmount
        {
            get
            {
                return _FinancementModeRemboursementFixedAmount;
            }

            set
            {
                _FinancementModeRemboursementFixedAmount = value;
            }
        }

        public int MecPlanifieeModeRemboursementAuKilo
        {
            get
            {
                return _MecPlanifieeModeRemboursementAuKilo;
            }

            set
            {
                _MecPlanifieeModeRemboursementAuKilo = value;
            }
        }

        public int FinancementTypeAvance
        {
            get
            {
                return _FinancementTypeAvance;
            }

            set
            {
                _FinancementTypeAvance = value;
            }
        }

        public string Tel2Societe
        {
            get
            {
                return _Tel2Societe;
            }

            set
            {
                _Tel2Societe = value;
            }
        }

        public int ContratPeriodeTypeFinancement
        {
            get
            {
                return _ContratPeriodeTypeFinancement;
            }

            set
            {
                _ContratPeriodeTypeFinancement = value;
            }
        }

        public int ChefAgenceType
        {
            get
            {
                return _ChefAgenceType;
            }

            set
            {
                _ChefAgenceType = value;
            }
        }

        public int SacNylonTypeID
        {
            get { return _SacNylonTypeID; }
            set { _SacNylonTypeID = value;  }
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

        public int AjustementStockType
        {
            get
            {
                return _AjustementStockType;
            }

            set
            {
                _AjustementStockType = value;
            }
        }

        public int RegularisationStockType
        {
            get
            {
                return _RegularisationStockType;
            }

            set
            {
                _RegularisationStockType = value;
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

        public int FevesUsinageStockType
        {
            get
            {
                return _FevesUsinageStockType;
            }

            set
            {
                _FevesUsinageStockType = value;
            }
        }

        public int LivraisonTypeElementStock
        {
            get
            {
                return _LivraisonTypeElementStock;
            }

            set
            {
                _LivraisonTypeElementStock = value;
            }
        }

        public int PeseeAvantUsinage
        {
            get
            {
                return _PeseeAvantUsinage;
            }

            set
            {
                _PeseeAvantUsinage = value;
            }
        }

        public int PeseeApresUsinage
        {
            get
            {
                return _PeseeApresUsinage;
            }

            set
            {
                _PeseeApresUsinage = value;
            }
        }

        public int PeseeCertifiee
        {
            get
            {
                return _PeseeCertifiee;
            }

            set
            {
                _PeseeCertifiee = value;
            }
        }

        public int PeseeDiverse
        {
            get
            {
                return _PeseeDiverse;
            }

            set
            {
                _PeseeDiverse = value;
            }
        }

        public int MagasinTV
        {
            get
            {
                return _MagasinTV;
            }

            set
            {
                _MagasinTV = value;
            }
        }

        public int MagasinExport
        {
            get
            {
                return _MagasinExport;
            }

            set
            {
                _MagasinExport = value;
            }
        }

        public int LotOrdinaire
        {
            get
            {
                return _LotOrdinaire;
            }

            set
            {
                _LotOrdinaire = value;
            }
        }

        public int LotCertifie
        {
            get
            {
                return _LotCertifie;
            }

            set
            {
                _LotCertifie = value;
            }
        }

        public int SacsLivraisonsAutoriseParPalette
        {
            get
            {
                return _SacsLivraisonsAutoriseParPalette;
            }

            set
            {
                _SacsLivraisonsAutoriseParPalette = value;
            }
        }

        public int NombrePalettesAutoriseAuPesePalette
        {
            get
            {
                return _NombrePalettesAutoriseAuPesePalette;
            }

            set
            {
                _NombrePalettesAutoriseAuPesePalette = value;
            }
        }

        public int PrefinancementTypeApc
        {
            get
            {
                return _PrefinancementTypeApc;
            }

            set
            {
                _PrefinancementTypeApc = value;
            }
        }

        public int PrefinancementTypeBlank
        {
            get
            {
                return _PrefinancementTypeBlank;
            }

            set
            {
                _PrefinancementTypeBlank = value;
            }
        }

        public double ValeurTauxEuro
        {
            get
            {
                return _ValeurTauxEuro;
            }

            set
            {
                _ValeurTauxEuro = value;
            }
        }

        public int EmplacementParDefaut
        {
            get
            {
                return _EmplacementParDefaut;
            }

            set
            {
                _EmplacementParDefaut = value;
            }
        }

        public int PeseeAvantEmpotage
        {
            get
            {
                return _PeseeAvantEmpotage;
            }

            set
            {
                _PeseeAvantEmpotage = value;
        }
        }

        public int PoidsStdBrutUnitaire
        {
            get { return _PoidsStdBrutUnitaire; }
            set { _PoidsStdBrutUnitaire = value; }
        }

        public int PoidsStdNetUnitaire
        {
            get { return _PoidsStdNetUnitaire; }
            set { _PoidsStdNetUnitaire = value; }
        }

        public int ReusinageAnnulationLotMvtType
        {
            get
            {
                return _ReusinageAnnulationLotMvtType;
            }

            set
            {
                _ReusinageAnnulationLotMvtType = value;
            }
        }

        public int LotTypeElementEnStock
        {
            get
            {
                return _LotTypeElementEnStock;
            }

            set
            {
                _LotTypeElementEnStock = value;
            }
        }

        public int SacExportType
        {
            get
            {
                return _SacExportType;
            }

            set
            {
                _SacExportType = value;
            }
        }

        public int ConstitutionLotCertifie
        {
            get
            {
                return _ConstitutionLotCertifie;
            }

            set
            {
                _ConstitutionLotCertifie = value;
            }
        }

        public int PeseeApresReUsinage
        {
            get
            {
                return _PeseeApresReUsinage;
            }

            set
            {
                _PeseeApresReUsinage = value;
            }
        }

        public int LaboExport
        {
            get
            {
                return _LaboExport;
            }

            set
            {
                _LaboExport = value;
            }
        }

        public int NbrOfLotsForChemicalAnalysis
        {
            get
            {
                return _NbrOfLotsForChemicalAnalysis;
            }

            set
            {
                _NbrOfLotsForChemicalAnalysis = value;
            }
        }

        public Emplacement Emplacement
        {
            get
            {
                return _Emplacement;
            }

            set
            {
                _Emplacement = value;
            }
        }

        public string MagasinTvDesignation
        {
            get
            {
                return _MagasinTvDesignation;
            }

            set
            {
                _MagasinTvDesignation = value;
            }
        }

        public string MagasinExportDesignation
        {
            get
            {
                return _MagasinExportDesignation;
            }

            set
            {
                _MagasinExportDesignation = value;
            }
        }

        public string PrefixeContratNum
        {
            get
            {
                return _PrefixeContratNum;
            }

            set
            {
                _PrefixeContratNum = value;
            }
        }

        public int ClientDefID
        {
            get
            {
                return _ClientDefID;
            }

            set
            {
                _ClientDefID = value;
            }
        }

        public int ConditionnementDefID
        {
            get
            {
                return _ConditionnementDefID;
            }

            set
            {
                _ConditionnementDefID = value;
            }
        }

        public int OrigineContratExportDefID
        {
            get
            {
                return _OrigineContratExportDefID;
            }

            set
            {
                _OrigineContratExportDefID = value;
            }
        }

        public int DefaultModeEnregistrementVente
        {
            get
            {
                return _DefaultModeEnregistrementVente;
            }

            set
            {
                _DefaultModeEnregistrementVente = value;
            }
        }

        public int LaboTv
        {
            get
            {
                return _LaboTv;
            }

            set
            {
                _LaboTv = value;
            }
        }

        public LivraisonType LivraisonTypeAchat
        {
            get
            {
                return _LivraisonTypeAchat;
            }

            set
            {
                _LivraisonTypeAchat = value;
            }
        }

        public decimal FactureFinaleBonusTaux
        {
            get
            {
                return _FactureFinaleBonusTaux;
            }

            set
            {
                _FactureFinaleBonusTaux = value;
            }
        }

        public int ContratPaiementTermeDefault
        {
            get
            {
                return _ContratPaiementTermeDefault;
            }

            set
            {
                _ContratPaiementTermeDefault = value;
            }
        }

        public string ContratOptionPoidsDefault
        {
            get
            {
                return _ContratOptionPoidsDefault;
            }

            set
            {
                _ContratOptionPoidsDefault = value;
            }
        }

        public int ContratDeliveryCondition
        {
            get
            {
                return _ContratDeliveryCondition;
            }

            set
            {
                _ContratDeliveryCondition = value;
            }
        }

        public string PrefixeContratExportEnregistrement
        {
            get
            {
                return _PrefixeContratExportEnregistrement;
            }

            set
            {
                _PrefixeContratExportEnregistrement = value;
            }
        }

        public int DefaultTransitaireShipment
        {
            get
            {
                return _DefaultTransitaireShipment;
            }

            set
            {
                _DefaultTransitaireShipment = value;
            }
        }

        public int DefaultConteneurType
        {
            get
            {
                return _DefaultConteneurType;
            }

            set
            {
                _DefaultConteneurType = value;
            }
        }

        public decimal ContratVenteBonusCertificationValue
        {
            get
            {
                return _ContratVenteBonusCertificationValue;
            }

            set
            {
                _ContratVenteBonusCertificationValue = value;
            }
        }

        public decimal FobDiscountPriceDefault
        {
            get
            {
                return _FobDiscountPriceDefault;
            }

            set
            {
                _FobDiscountPriceDefault = value;
            }
        }

        public int IDRetourEnStockLotMvtType
        {
            get
            {
                return _IDRetourEnStockLotMvtType;
            }

            set
            {
                _IDRetourEnStockLotMvtType = value;
            }
        }

        public int IDEngagementType
        {
            get
            {
                return _IDEngagementType;
            }

            set
            {
                _IDEngagementType = value;
            }
        }

        public int IDEngagementFinancementType
        {
            get
            {
                return _IDEngagementFinancementType;
            }

            set
            {
                _IDEngagementFinancementType = value;
            }
        }

        public int IDPrixNegociePourLivraisons
        {
            get
            {
                return _IDPrixNegociePourLivraisons;
            }

            set
            {
                _IDPrixNegociePourLivraisons = value;
            }
        }

        public decimal FinancementTauxPaiement
        {
            get
            {
                return _FinancementTauxPaiement;
            }

            set
            {
                _FinancementTauxPaiement = value;
            }
        }

        public int IDTransfertFevesAgence
        {
            get
            {
                return _IDTransfertFevesAgence;
            }

            set
            {
                _IDTransfertFevesAgence = value;
            }
        }

        public int IDDestinationParDefaut
        {
            get
            {
                return _IDDestinationParDefaut;
            }

            set
            {
                _IDDestinationParDefaut = value;
            }
        }

        public int NbrSacAutorisePeseeSurSite
        {
            get
            {
                return _NbrSacAutorisePeseeSurSite;
            }

            set
            {
                _NbrSacAutorisePeseeSurSite = value;
            }
        }

        public int AchatBrousseMvtType
        {
            get
            {
                return _AchatBrousseMvtType;
            }

            set
            {
                _AchatBrousseMvtType = value;
            }
        }

        public int SacBrousseType
        {
            get
            {
                return _SacBrousseType;
            }

            set
            {
                _SacBrousseType = value;
            }
        }

        public int MvtTypeVenteLot
        {
            get
            {
                return _MvtTypeVenteLot;
            }

            set
            {
                _MvtTypeVenteLot = value;
            }
        }

        public int CertificationOrdinaire
        {
            get
            {
                return _CertificationOrdinaire;
            }

            set
            {
                _CertificationOrdinaire = value;
            }
        }

        public string NumeroCCC
        {
            get
            {
                return _NumeroCCC;
            }

            set
            {
                _NumeroCCC = value;
            }
        }

        public int GroupeFrsCacaoID
        {
            get
            {
                return _GroupeFrsCacaoID;
            }

            set
            {
                _GroupeFrsCacaoID = value;
            }
        }

        public int DefTransfertChargementLibre
        {
            get
            {
                return _DefTransfertChargementLibre;
            }

            set
            {
                _DefTransfertChargementLibre = value;
            }
        }

        public int RedevanceCacao
        {
            get
            {
                return _RedevanceCacao;
            }

            set
            {
                _RedevanceCacao = value;
            }
        }

        public int RedevanceRtc
        {
            get
            {
                return _RedevanceRtc;
            }

            set
            {
                _RedevanceRtc = value;
            }
        }

        public int IdTaxeRtc
        {
            get
            {
                return _idTaxeRtc;
            }

            set
            {
                _idTaxeRtc = value;
            }
        }

        public int IdProduitExport
        {
            get
            {
                return _IdProduitExport;
            }

            set
            {
                _IdProduitExport = value;
            }
        }

        public int PaiementOptionId
        {
            get
            {
                return _PaiementOptionId;
            }

            set
            {
                _PaiementOptionId = value;
            }
        }

        public bool IsInDevelopment
        {
            get
            {
                return _IsInDevelopment;
            }

            set
            {
                _IsInDevelopment = value;
            }
        }
        #endregion

        #region Methods
        public Parametres()
        {
            //fnGet(0);
        }

        public Parametres(int id)
        {
            fnGet(0);
        }

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
            return fnGet();
        }



        public bool fnGet()
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("Parametres_Select");
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
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Parametres_Select");                                
                mDataReader = db().ExecuteReader(mCommande);

                if (mDataReader.Read())
                {
                    Parametres mClass = new Parametres();

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

                    mCommande = db().CreateStoredProcCommand("Parametres_Modify");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.Int, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("Parametres_Modify");
                    //db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
                    //db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@DefaultSiteID", SqlDbType.Int, _Site);
                db().AddInParameter(mCommande, "@DefaultCampagneID", SqlDbType.VarChar, _Campagne);
                db().AddInParameter(mCommande, "@DefaultTolerancePrixNegocie", SqlDbType.Float, _PrixNegocie);
                db().AddInParameter(mCommande, "@DefaultExportateurID", SqlDbType.Int, _Exportateur.ID);
                db().AddInParameter(mCommande, "@DefaultRecolteID", SqlDbType.Int, _Recolte.ID);
                db().AddInParameter(mCommande, "@DefaultLaboratoireID", SqlDbType.Int, _Laboratoire.ID);
                //db().AddInParameter(mCommande, "@DefaultBaseUrl", SqlDbType.VarChar, _Base_url);
                db().AddInParameter(mCommande, "@MaxCodeAnalysePourLivraisonGenere", SqlDbType.Int, _MaxCodeAnalyseGenere);
                //db().AddInParameter(mCommande, "@DefaultNomSociete", SqlDbType.VarChar, _NomSociete);
                db().AddInParameter(mCommande, "@DefaultAdresseSociete", SqlDbType.VarChar, _AdresseSociete);
                db().AddInParameter(mCommande, "@DefaultTelSociete", SqlDbType.VarChar, _TelSociete);
                //db().AddInParameter(mCommande, "@DefaultContratPeriodeTypeID", SqlDbType.Int, _ContratPeriodeType.ID);
                //db().AddInParameter(mCommande, "@DefaultPayementModeID", SqlDbType.Int, _PayementMode.ID);
                //db().AddInParameter(mCommande, "@DefaultModeApplicationID", SqlDbType.Int, _PrixNegocieModeApplication.ID);
                //db().AddInParameter(mCommande, "@FactureDeductionTypeTransportID", SqlDbType.Int, _FactureDeductionType.ID);
                //db().AddInParameter(mCommande, "@FactureDeductionTypeAvanceID", SqlDbType.Int, _FactureDeductionTypeAvance);
                //db().AddInParameter(mCommande, "@FactureDeductionTypeLegalTaxID", SqlDbType.Int, _FactureDeductionTypeLegalTax);
                //db().AddInParameter(mCommande, "@FactureDeductionTypeMecID", SqlDbType.Int, _FactureDeductionTypeMec);
                //db().AddInParameter(mCommande, "@FinancementModeRemboursementLibreID", SqlDbType.Int, _FinancementModeRemboursementLibre);
                //db().AddInParameter(mCommande, "@FinancementModeRemboursementAuKiloID", SqlDbType.Int, _FinancementModeRemboursementAuKilo);
                //db().AddInParameter(mCommande, "@FinancementModeRemboursementIntegralID", SqlDbType.Int, _FinancementModeRemboursementIntegral);
                //db().AddInParameter(mCommande, "@FinancementModeRemboursementFixedAmountID", SqlDbType.Int, _FinancementModeRemboursementFixedAmount);
                //db().AddInParameter(mCommande, "@FinancementTypeAvanceID", SqlDbType.Int, _FinancementTypeAvance);
                //db().AddInParameter(mCommande, "@MecPlanifieeModeRemboursementAuKiloID", SqlDbType.Int, _MecPlanifieeModeRemboursementAuKilo);
                db().AddInParameter(mCommande, "@Tel2Societe", SqlDbType.VarChar, _Tel2Societe);
                db().AddInParameter(mCommande, "@NumeroCCC", SqlDbType.VarChar, _NumeroCCC);
                //db().AddInParameter(mCommande, "@Designation", SqlDbType.VarChar, _AdresseSociete);
                //db().AddInParameter(mCommande, "@Designation", SqlDbType.VarChar, _AdresseSociete);
                //db().AddInParameter(mCommande, "@Designation", SqlDbType.VarChar, _AdresseSociete);

                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                if (!this._isnew)
                {
                    //db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
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

                        //_RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        //_ID = (int)db().Parameters(mCommande, "@ID");

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
                throw new Exception(ex.Message + "\r\n" + "Parametres:fnUpdate");

            }
            return Result;
        }


        public override string ToString()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region "Private Members"       

        private static void MapFromDataReader(Parametres mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["Sites"])) mClass._Site = (Int16)mDataReader["Sites"];
                    if (!DBNull.Value.Equals(mDataReader["Campagne"])) mClass._Campagne = (string)mDataReader["Campagne"];
                    if (!DBNull.Value.Equals(mDataReader["PrixNegocie"])) mClass._PrixNegocie = (Double)mDataReader["PrixNegocie"];
                    if (!DBNull.Value.Equals(mDataReader["MaxCodeAnalyseGenere"])) mClass._MaxCodeAnalyseGenere = (int)mDataReader["MaxCodeAnalyseGenere"];
                    //if (!DBNull.Value.Equals(mDataReader["StandardHumidite"])) mClass._StandardHumidite = (decimal)mDataReader["StandardHumidite"];
                    //if (!DBNull.Value.Equals(mDataReader["StandardMatiereEtrangere"])) mClass._StandardMatiereEtrangere = (decimal)mDataReader["StandardMatiereEtrangere"];
                    //if (!DBNull.Value.Equals(mDataReader["StandardBrisure"])) mClass._StandardBrisure = (decimal)mDataReader["StandardBrisure"];

                    mClass._Recolte = new Recolte();
                    if (!DBNull.Value.Equals(mDataReader["Recolte"])) mClass._Recolte.ID = (int)mDataReader["Recolte"];
                    if (!DBNull.Value.Equals(mDataReader["RecolteNom"])) mClass._Recolte.Designation = (string)mDataReader["RecolteNom"];

                    mClass._Exportateur = new Exportateur();
                    if (!DBNull.Value.Equals(mDataReader["Exportateur"])) mClass._Exportateur.ID = (int)mDataReader["Exportateur"];                   
                    if (!DBNull.Value.Equals(mDataReader["ExportateurNom"])) mClass._Exportateur.Nom = (string)mDataReader["ExportateurNom"];

                    mClass._Laboratoire = new Laboratoire();
                    if (!DBNull.Value.Equals(mDataReader["Laboratoire"])) mClass._Laboratoire.ID = (int)mDataReader["Laboratoire"];
                    if (!DBNull.Value.Equals(mDataReader["LaboratoireDesignation"])) mClass._Laboratoire.Designation = (string)mDataReader["LaboratoireDesignation"];

                    if (!DBNull.Value.Equals(mDataReader["Base_Url"])) mClass._Base_url = (string)mDataReader["Base_Url"];

                    mClass._ContratPeriodeType = new ContratPeriodeType();
                    if (!DBNull.Value.Equals(mDataReader["ContratPeriodeType"])) mClass._ContratPeriodeType.ID = (int)mDataReader["ContratPeriodeType"];
                    if (!DBNull.Value.Equals(mDataReader["ContratPeriodeTypeDesignation"])) mClass._ContratPeriodeType.Designation = (string)mDataReader["ContratPeriodeTypeDesignation"];

                    if (!DBNull.Value.Equals(mDataReader["NomSociete"])) mClass._NomSociete = (string)mDataReader["NomSociete"];
                    if (!DBNull.Value.Equals(mDataReader["AdresseSociete"])) mClass._AdresseSociete = (string)mDataReader["AdresseSociete"];
                    if (!DBNull.Value.Equals(mDataReader["TelSociete"])) mClass._TelSociete = (string)mDataReader["TelSociete"];
                    if (!DBNull.Value.Equals(mDataReader["Tel2Societe"])) mClass._Tel2Societe = (string)mDataReader["Tel2Societe"];

                    mClass._PayementMode = new PayementMode();
                    if (!DBNull.Value.Equals(mDataReader["PayementMode"])) mClass._PayementMode.ID = (int)mDataReader["PayementMode"];
                    if (!DBNull.Value.Equals(mDataReader["PayementModeDesignation"])) mClass._PayementMode.Designation = (string)mDataReader["PayementModeDesignation"];

                    mClass._PrixNegocieModeApplication = new PrixNegocieModeApplication();
                    if (!DBNull.Value.Equals(mDataReader["ModeApplication"])) mClass._PrixNegocieModeApplication.ID = (int)mDataReader["ModeApplication"];
                    if (!DBNull.Value.Equals(mDataReader["ModeApplicationDesignation"])) mClass._PrixNegocieModeApplication.Designation = (string)mDataReader["ModeApplicationDesignation"];

                    mClass._FactureDeductionType = new FactureDeductionType();
                    if (!DBNull.Value.Equals(mDataReader["FactureDeductionTypeTransport"])) mClass._FactureDeductionType.ID = (int)mDataReader["FactureDeductionTypeTransport"];
                    if (!DBNull.Value.Equals(mDataReader["FactureDeductionTypeTransportDesignation"])) mClass._FactureDeductionType.Designation = (string)mDataReader["FactureDeductionTypeTransportDesignation"];

                    if (!DBNull.Value.Equals(mDataReader["FactureDeductionTypeAvance"])) mClass._FactureDeductionTypeAvance = (int)mDataReader["FactureDeductionTypeAvance"];
                    if (!DBNull.Value.Equals(mDataReader["FactureDeductionTypeMec"])) mClass._FactureDeductionTypeMec = (int)mDataReader["FactureDeductionTypeMec"];
                    if (!DBNull.Value.Equals(mDataReader["FactureDeductionTypeLegalTax"])) mClass._FactureDeductionTypeLegalTax = (int)mDataReader["FactureDeductionTypeLegalTax"];

                    if (!DBNull.Value.Equals(mDataReader["FinancementModeRemboursementLibre"])) mClass._FinancementModeRemboursementLibre = (int)mDataReader["FinancementModeRemboursementLibre"];
                    if (!DBNull.Value.Equals(mDataReader["FinancementModeRemboursementAuKilo"])) mClass._FinancementModeRemboursementAuKilo = (int)mDataReader["FinancementModeRemboursementAuKilo"];
                    if (!DBNull.Value.Equals(mDataReader["FinancementModeRemboursementIntegral"])) mClass._FinancementModeRemboursementIntegral = (int)mDataReader["FinancementModeRemboursementIntegral"];
                    if (!DBNull.Value.Equals(mDataReader["FinancementModeRemboursementFixedAmount"])) mClass._FinancementModeRemboursementFixedAmount = (int)mDataReader["FinancementModeRemboursementFixedAmount"];
                    if (!DBNull.Value.Equals(mDataReader["MecPlanifieeModeRemboursementAuKilo"])) mClass._MecPlanifieeModeRemboursementAuKilo = (int)mDataReader["MecPlanifieeModeRemboursementAuKilo"];
                    if (!DBNull.Value.Equals(mDataReader["FinancementTypeAvance"])) mClass._FinancementTypeAvance = (int)mDataReader["FinancementTypeAvance"];
                    if (!DBNull.Value.Equals(mDataReader["ContratPeriodeTypeFinancement"])) mClass._ContratPeriodeTypeFinancement = (int)mDataReader["ContratPeriodeTypeFinancement"];
                    if (!DBNull.Value.Equals(mDataReader["ChefAgenceType"])) mClass._ChefAgenceType = (int)mDataReader["ChefAgenceType"];
                    if (!DBNull.Value.Equals(mDataReader["SacNylonTypeID"])) mClass._SacNylonTypeID = (int)mDataReader["SacNylonTypeID"];

                    mClass._Magasin = new Magasin();
                    if (!DBNull.Value.Equals(mDataReader["Magasin"])) mClass._Magasin.ID = (int)mDataReader["Magasin"];
                    if (!DBNull.Value.Equals(mDataReader["MagasinTvDesignation"])) mClass._Magasin.Designation = (string)mDataReader["MagasinTvDesignation"];

                    mClass._TypeElementStock = new TypeElementStock();
                    if (!DBNull.Value.Equals(mDataReader["AjustementStockType"])) mClass._AjustementStockType = (int)mDataReader["AjustementStockType"];
                    if (!DBNull.Value.Equals(mDataReader["AjustementStockType"])) mClass._TypeElementStock.ID = (int)mDataReader["AjustementStockType"];
                    if (!DBNull.Value.Equals(mDataReader["AjustementStockTypeDesignation"])) mClass._TypeElementStock.Designation = (string)mDataReader["AjustementStockTypeDesignation"];

                    mClass._MouvementStockType = new MouvementStockType();
                    if (!DBNull.Value.Equals(mDataReader["RegularisationStockType"])) mClass._RegularisationStockType = (int)mDataReader["RegularisationStockType"];
                    if (!DBNull.Value.Equals(mDataReader["RegularisationStockType"])) mClass._MouvementStockType.ID = (int)mDataReader["RegularisationStockType"];
                    if (!DBNull.Value.Equals(mDataReader["RegularisationStockTypeDesignation"])) mClass._MouvementStockType.Designation = (string)mDataReader["RegularisationStockTypeDesignation"];

                    if (!DBNull.Value.Equals(mDataReader["FevesUsinageStockType"])) mClass._FevesUsinageStockType = (int)mDataReader["FevesUsinageStockType"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeElementStock"])) mClass._LivraisonTypeElementStock = (int)mDataReader["LivraisonTypeElementStock"];

                    if (!DBNull.Value.Equals(mDataReader["PeseeAvantUsinage"])) mClass._PeseeAvantUsinage = (int)mDataReader["PeseeAvantUsinage"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeApresUsinage"])) mClass._PeseeApresUsinage = (int)mDataReader["PeseeApresUsinage"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeApresReUsinage"])) mClass._PeseeApresReUsinage = (int)mDataReader["PeseeApresReUsinage"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeCertifiee"])) mClass._PeseeCertifiee = (int)mDataReader["PeseeCertifiee"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeDiverse"])) mClass._PeseeDiverse = (int)mDataReader["PeseeDiverse"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeAvantEmpotage"])) mClass._PeseeAvantEmpotage = (int)mDataReader["PeseeAvantEmpotage"];

                    if (!DBNull.Value.Equals(mDataReader["MagasinExport"])) mClass._MagasinExport = (int)mDataReader["MagasinExport"];
                    if (!DBNull.Value.Equals(mDataReader["MagasinTV"])) mClass._MagasinTV = (int)mDataReader["MagasinTV"];
                    if (!DBNull.Value.Equals(mDataReader["LotCertifie"])) mClass._LotCertifie = (int)mDataReader["LotCertifie"];
                    if (!DBNull.Value.Equals(mDataReader["LotOrdinaire"])) mClass._LotOrdinaire = (int)mDataReader["LotOrdinaire"];
                    if (!DBNull.Value.Equals(mDataReader["SacsLivraisonsAutoriseParPalette"])) mClass._SacsLivraisonsAutoriseParPalette = (int)mDataReader["SacsLivraisonsAutoriseParPalette"];
                    if (!DBNull.Value.Equals(mDataReader["NombrePalettesAutoriseAuPesePalette"])) mClass._NombrePalettesAutoriseAuPesePalette = (int)mDataReader["NombrePalettesAutoriseAuPesePalette"];

                    if (!DBNull.Value.Equals(mDataReader["PrefinancementTypeApc"])) mClass._PrefinancementTypeApc = (int)mDataReader["PrefinancementTypeApc"];
                    if (!DBNull.Value.Equals(mDataReader["PrefinancementTypeBlank"])) mClass._PrefinancementTypeBlank = (int)mDataReader["PrefinancementTypeBlank"];
                    if (!DBNull.Value.Equals(mDataReader["ValeurTauxEuro"])) mClass._ValeurTauxEuro = (double)mDataReader["ValeurTauxEuro"];
                    if (!DBNull.Value.Equals(mDataReader["EmplacementParDefaut"])) mClass._EmplacementParDefaut = (int)mDataReader["EmplacementParDefaut"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsStdBrutUnitaire"])) mClass._PoidsStdBrutUnitaire = (int)mDataReader["PoidsStdBrutUnitaire"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsStdNetUnitaire"])) mClass._PoidsStdNetUnitaire = (int)mDataReader["PoidsStdNetUnitaire"];
                    if (!DBNull.Value.Equals(mDataReader["ReusinageAnnulationLotMvtType"])) mClass.ReusinageAnnulationLotMvtType = (int)mDataReader["ReusinageAnnulationLotMvtType"];
                    if (!DBNull.Value.Equals(mDataReader["LotTypeElementEnStock"])) mClass.LotTypeElementEnStock = (int)mDataReader["LotTypeElementEnStock"];
                    if (!DBNull.Value.Equals(mDataReader["SacExportType"])) mClass.SacExportType = (int)mDataReader["SacExportType"];
                    if (!DBNull.Value.Equals(mDataReader["ConstitutionLotCertifie"])) mClass._ConstitutionLotCertifie = (int)mDataReader["ConstitutionLotCertifie"];
                    if (!DBNull.Value.Equals(mDataReader["LaboExport"])) mClass._LaboExport = (int)mDataReader["LaboExport"];
                    if (!DBNull.Value.Equals(mDataReader["LaboTV"])) mClass._LaboTv = (int)mDataReader["LaboTV"];
                    if (!DBNull.Value.Equals(mDataReader["NbrOfLotsForChemicalAnalysis"])) mClass._NbrOfLotsForChemicalAnalysis = (int)mDataReader["NbrOfLotsForChemicalAnalysis"];

                    if (!DBNull.Value.Equals(mDataReader["MagasinTvDesignation"])) mClass._MagasinTvDesignation = (string)mDataReader["MagasinTvDesignation"];
                    if (!DBNull.Value.Equals(mDataReader["MagasinExportDesignation"])) mClass._MagasinExportDesignation = (string)mDataReader["MagasinExportDesignation"];

                    mClass._Emplacement = new Emplacement();
                    if (!DBNull.Value.Equals(mDataReader["EmplacementDesignation"])) mClass._Emplacement.Designation = (string)mDataReader["EmplacementDesignation"];
                    if (!DBNull.Value.Equals(mDataReader["EmplacementParDefaut"])) mClass._Emplacement.ID = (int)mDataReader["EmplacementParDefaut"];

                    if (!DBNull.Value.Equals(mDataReader["PrefixeContratNum"])) mClass._PrefixeContratNum = (string)mDataReader["PrefixeContratNum"];
                    if (!DBNull.Value.Equals(mDataReader["ClientDefID"])) mClass._ClientDefID = (int)mDataReader["ClientDefID"];

                    if (!DBNull.Value.Equals(mDataReader["ConditionnementDefID"])) mClass._ConditionnementDefID = (int)mDataReader["ConditionnementDefID"];
                    if (!DBNull.Value.Equals(mDataReader["OrigineContratExportDefID"])) mClass._OrigineContratExportDefID = (int)mDataReader["OrigineContratExportDefID"];
                    if (!DBNull.Value.Equals(mDataReader["DefaultModeEnregistrementVente"])) mClass.DefaultModeEnregistrementVente = (int)mDataReader["DefaultModeEnregistrementVente"];

                    mClass._LivraisonTypeAchat = new LivraisonType();
                    if (!DBNull.Value.Equals(mDataReader["IDLivraisonTypeAchat"])) mClass._LivraisonTypeAchat.ID = (int)mDataReader["IDLivraisonTypeAchat"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeAchatDesignation"])) mClass._LivraisonTypeAchat.Designation = (string)mDataReader["LivraisonTypeAchatDesignation"];

                    if (!DBNull.Value.Equals(mDataReader["FactureFinaleBonusTaux"])) mClass._FactureFinaleBonusTaux = (decimal)mDataReader["FactureFinaleBonusTaux"];

                    if (!DBNull.Value.Equals(mDataReader["ContratPaiementTermeDefault"])) mClass._ContratPaiementTermeDefault = (int)mDataReader["ContratPaiementTermeDefault"];
                    if (!DBNull.Value.Equals(mDataReader["ContratOptionPoidsDefault"])) mClass._ContratOptionPoidsDefault = (string)mDataReader["ContratOptionPoidsDefault"];

                    if (!DBNull.Value.Equals(mDataReader["ContratDeliveryCondition"])) mClass._ContratDeliveryCondition = (int)mDataReader["ContratDeliveryCondition"];
                    if (!DBNull.Value.Equals(mDataReader["PrefixeContratExportEnregistrement"])) mClass._PrefixeContratExportEnregistrement = (string)mDataReader["PrefixeContratExportEnregistrement"];
                    if (!DBNull.Value.Equals(mDataReader["DefaultTransitaireShipment"])) mClass._DefaultTransitaireShipment = (int)mDataReader["DefaultTransitaireShipment"];
                    if (!DBNull.Value.Equals(mDataReader["DefaultConteneurType"])) mClass._DefaultConteneurType = (int)mDataReader["DefaultConteneurType"];

                    if (!DBNull.Value.Equals(mDataReader["ContratVenteBonusCertificationValue"])) mClass._ContratVenteBonusCertificationValue = (decimal)mDataReader["ContratVenteBonusCertificationValue"];
                    if (!DBNull.Value.Equals(mDataReader["FobDiscountPriceDefault"])) mClass._FobDiscountPriceDefault = (decimal)mDataReader["FobDiscountPriceDefault"];
                    if (!DBNull.Value.Equals(mDataReader["IDRetourEnStockLotMvtType"])) mClass._IDRetourEnStockLotMvtType = (int)mDataReader["IDRetourEnStockLotMvtType"];
                    if (!DBNull.Value.Equals(mDataReader["IDEngagementType"])) mClass._IDEngagementType = (int)mDataReader["IDEngagementType"];
                    if (!DBNull.Value.Equals(mDataReader["IDEngagementFinancementType"])) mClass._IDEngagementFinancementType = (int)mDataReader["IDEngagementFinancementType"];
                    if (!DBNull.Value.Equals(mDataReader["IDPrixNegociePourLivraisons"])) mClass._IDPrixNegociePourLivraisons = (int)mDataReader["IDPrixNegociePourLivraisons"];

                    if (!DBNull.Value.Equals(mDataReader["FinancementTauxPaiement"])) mClass._FinancementTauxPaiement = (decimal)mDataReader["FinancementTauxPaiement"];
                    if (!DBNull.Value.Equals(mDataReader["IDTransfertFevesAgence"])) mClass._IDTransfertFevesAgence = (int)mDataReader["IDTransfertFevesAgence"];
                    if (!DBNull.Value.Equals(mDataReader["IDDestinationParDefaut"])) mClass._IDDestinationParDefaut = (int)mDataReader["IDDestinationParDefaut"];

                    if (!DBNull.Value.Equals(mDataReader["NbrSacAutorisePeseeSurSite"])) mClass._NbrSacAutorisePeseeSurSite = (int)mDataReader["NbrSacAutorisePeseeSurSite"];
                    if (!DBNull.Value.Equals(mDataReader["AchatBrousseMvtType"])) mClass._AchatBrousseMvtType = (int)mDataReader["AchatBrousseMvtType"];
                    if (!DBNull.Value.Equals(mDataReader["SacBrousseType"])) mClass._SacBrousseType = (int)mDataReader["SacBrousseType"];
                    if (!DBNull.Value.Equals(mDataReader["MvtTypeVenteLot"])) mClass._MvtTypeVenteLot = (int)mDataReader["MvtTypeVenteLot"];
                    if (!DBNull.Value.Equals(mDataReader["CertificationOrdinaire"])) mClass._CertificationOrdinaire = (int)mDataReader["CertificationOrdinaire"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroCCC"])) mClass._NumeroCCC = (string)mDataReader["NumeroCCC"];
                    if (!DBNull.Value.Equals(mDataReader["GroupeFrsCacaoID"])) mClass._GroupeFrsCacaoID = (int)mDataReader["GroupeFrsCacaoID"];
                    if (!DBNull.Value.Equals(mDataReader["DefTransfertChargementLibre"])) mClass._DefTransfertChargementLibre = (int)mDataReader["DefTransfertChargementLibre"];
                    if (!DBNull.Value.Equals(mDataReader["isDev"])) mClass.IsInDevelopment = (bool)mDataReader["isDev"];
                    if (!DBNull.Value.Equals(mDataReader["redevanceCacao"])) mClass.RedevanceCacao = (int)mDataReader["redevanceCacao"];
                    if (!DBNull.Value.Equals(mDataReader["redevanceRtc"])) mClass.RedevanceRtc = (int)mDataReader["redevanceRtc"];
                    if (!DBNull.Value.Equals(mDataReader["IdTaxeRtc"])) mClass.IdTaxeRtc = (int)mDataReader["IdTaxeRtc"];
                    if (!DBNull.Value.Equals(mDataReader["ProduitExportId"])) mClass.IdProduitExport = (int)mDataReader["ProduitExportId"];
                    if (!DBNull.Value.Equals(mDataReader["PaiementOptionId"])) mClass.PaiementOptionId = (int)mDataReader["PaiementOptionId"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nParametres:MapFromDataReader");
            }
        }
        #endregion

    }
}
