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
    //[Proxy(Read = "~/Livraison/Select")]
    //[JsonReader(RootProperty = "data")]
    public class SuiviLivraison : DataPersist
    {
        #region "Fields"
        private DateTime _DateLivraison;
        private LivraisonType _LivraisonType;
        private string _NumeroLivraison;
        private Fournisseur _Fournisseur;
        private Transporteur _Transporteur;
        private Site _Sites;
        private string _Immatriculation;
        private string _Status;
        private int _StatusID;
        private int _SacsDeclares;
        private decimal _PoidsDeclare;
        private double _Moisture;
        private double _BeanCount;
        private double _ForeignMatter;
        private int _AcceptedBags;
        private decimal _GrossWeight;
        private decimal _QualityRetention;
        private decimal _Tare;
        private decimal _AcceptedTonnage;
        private string _NumeroExterne;
        private string _OrigineDesignation;

        private string _CertificationAsString;
        private decimal _RefactionBrisures;
        private decimal _RefactionHumidite;
        private decimal _RefactionMatieresEtg;

        public string CertificationAsString
        {
            get { return (_Certification != null )? _Certification.Designation : string.Empty; }
            
        }

        public int CertificationID
        {
            get { return (_Certification != null) ? _Certification.ID : -1; }

        }

        private Certification _Certification;

        public Certification Certification
        {
            get { return _Certification; }
            set { _Certification = value; }
        }

        public DateTime DateLivraison
        {
            get
            {
                return _DateLivraison;
            }

            set
            {
                _DateLivraison = value;
            }
        }

        public string DateLivraisonLongAsString
        {
            get
            {
                return _DateLivraison != null ? _DateLivraison.ToString("g") : string.Empty;
            }
               
        }

        public LivraisonType LivraisonType
        {
            get
            {
                return _LivraisonType;
            }

            set
            {
                _LivraisonType = value;
            }
        }

        public string LivraisonTypeAsString
        {
            get
            {
                return (_LivraisonType != null) ? _LivraisonType.Designation : string.Empty; 
            }
        }

        public string SiteAsString
        {
            get
            {
                return (_Sites != null) ? _Sites.Nom : string.Empty;
            }
        }

        public string FournisseurAsString
        {
            get
            {
                return (_Fournisseur != null) ? _Fournisseur.Nom : string.Empty;
            }
        }

        public string NumeroLivraison
        {
            get
            {
                return _NumeroLivraison;
            }

            set
            {
                _NumeroLivraison = value;
            }
        }

        public Fournisseur Fournisseur
        {
            get
            {
                return _Fournisseur;
            }

            set
            {
                _Fournisseur = value;
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

        public string Status
        {
            get
            {
                return _Status;
            }

            set
            {
                _Status = value;
            }
        }

        public int SacsDeclares
        {
            get
            {
                return _SacsDeclares;
            }

            set
            {
                _SacsDeclares = value;
            }
        }

        public decimal PoidsDeclare
        {
            get
            {
                return _PoidsDeclare;
            }

            set
            {
                _PoidsDeclare = value;
            }
        }

        public double Moisture
        {
            get
            {
                return _Moisture;
            }

            set
            {
                _Moisture = value;
            }
        }

        public double BeanCount
        {
            get
            {
                return _BeanCount;
            }

            set
            {
                _BeanCount = value;
            }
        }

        public double ForeignMatter
        {
            get
            {
                return _ForeignMatter;
            }

            set
            {
                _ForeignMatter = value;
            }
        }

        public int AcceptedBags
        {
            get
            {
                return _AcceptedBags;
            }

            set
            {
                _AcceptedBags = value;
            }
        }

        public decimal GrossWeight
        {
            get
            {
                return _GrossWeight;
            }

            set
            {
                _GrossWeight = value;
            }
        }

        public decimal QualityRetention
        {
            get
            {
                return _QualityRetention;
            }

            set
            {
                _QualityRetention = value;
            }
        }

        public decimal Tare
        {
            get
            {
                return _Tare;
            }

            set
            {
                _Tare = value;
            }
        }

        public decimal AcceptedTonnage
        {
            get
            {
                return _AcceptedTonnage;
            }

            set
            {
                _AcceptedTonnage = value;
            }
        }


        public int mIcon
        {
            get
            {
                return _StatusID;
                //if (_Status.Equals("REJECTED"))
                //    return 4; // BulletCross
                //else if (_Status.Equals("ACCEPTED"))
                //    return 3; // Tick
                //else if (_Status.Equals("In PROCESS"))
                //    return 2; //                
                //else //if (_Status.Equals("AT GATE"))
                //    return 1; //                     
            }
        }

        public int StatusID
        {
            get
            {
                return _StatusID;
            }

            set
            {
                _StatusID = value;
            }
        }

        public string NumeroExterne
        {
            get
            {
                return _NumeroExterne;
            }

            set
            {
                _NumeroExterne = value;
            }
        }

        public string OrigineDesignation
        {
            get
            {
                return _OrigineDesignation;
            }

            set
            {
                _OrigineDesignation = value;
            }
        }

        public decimal RefactionBrisures
        {
            get
            {
                return _RefactionBrisures;
            }

            set
            {
                _RefactionBrisures = value;
            }
        }

        public decimal RefactionHumidite
        {
            get
            {
                return _RefactionHumidite;
            }

            set
            {
                _RefactionHumidite = value;
            }
        }

        public decimal RefactionMatieresEtg
        {
            get
            {
                return _RefactionMatieresEtg;
            }

            set
            {
                _RefactionMatieresEtg = value;
            }
        }

        public Transporteur Transporteur
        {
            get { return _Transporteur; }
            set { _Transporteur = value; }
        }

        public string TransporteurNom
        {
            get
            {
                return _Transporteur != null ? _Transporteur.Nom : string.Empty;
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
        #endregion

        #region "Properties"

        #endregion

        public SuiviLivraison()
        {
           
        }

        public List<SuiviLivraison> fnSelect(string cropYear, int livraisonTypeID, string fournisseurID, int ExportateurID, int AtGate, int InProcess, int OnlyByStatus, string Status,DateTime? StartDate, DateTime? EndDate, int SiteID = -1)
        {
            List<SuiviLivraison> mList = new List<SuiviLivraison>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("LivraisonSuivi_Select");
                db().AddInParameter(mCommande, "@CropYear", SqlDbType.VarChar, cropYear);
                db().AddInParameter(mCommande, "@LivraisonTypeID", SqlDbType.Int, livraisonTypeID);
                db().AddInParameter(mCommande, "@Fournisseur", SqlDbType.VarChar, fournisseurID);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@AtGate", SqlDbType.Int, AtGate);
                db().AddInParameter(mCommande, "@InProcess", SqlDbType.Int, InProcess);
                db().AddInParameter(mCommande, "@OnlyByStatus", SqlDbType.Int, OnlyByStatus);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@Status", SqlDbType.VarChar, Status);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, SiteID);
                //db().AddInParameter(mCommande, "@StartDate", SqlDbType.DateTime, mStartDate);
                //db().AddInParameter(mCommande, "@EndDate", SqlDbType.DateTime, mEndDate);
                mCommande.CommandTimeout = 15000;
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    SuiviLivraison mClass = new SuiviLivraison();

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
        
        #region "Private Members"
        
        public override string ToString()
        {
            return _NumeroLivraison;
        }

        private static void MapFromDataReader(SuiviLivraison mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["dateLivraison"])) mClass._DateLivraison                   = (DateTime)mDataReader["dateLivraison"];
                    if (!DBNull.Value.Equals(mDataReader["numeroLivraison"])) mClass._NumeroLivraison               = (string)mDataReader["numeroLivraison"];
                    if (!DBNull.Value.Equals(mDataReader["immatriculationLivraison"])) mClass._Immatriculation      = (string)mDataReader["immatriculationLivraison"];
                    if (!DBNull.Value.Equals(mDataReader["statutLivraisonID"])) mClass._StatusID                    = (int)mDataReader["statutLivraisonID"];
                    if (!DBNull.Value.Equals(mDataReader["statutLivraison"])) mClass._Status                        = (string)mDataReader["statutLivraison"];
                    if (!DBNull.Value.Equals(mDataReader["NbrSacsDeclares"])) mClass._SacsDeclares            = (int)mDataReader["NbrSacsDeclares"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsDeclares"])) mClass._PoidsDeclare           = (decimal)mDataReader["PoidsDeclares"];
                    if (!DBNull.Value.Equals(mDataReader["MoistureLivraison"])) mClass._Moisture                    = (double)mDataReader["MoistureLivraison"];
                    if (!DBNull.Value.Equals(mDataReader["beansCount"])) mClass._BeanCount                 = (double)mDataReader["beansCount"];
                    if (!DBNull.Value.Equals(mDataReader["foreignMatter"])) mClass._ForeignMatter          = (double)mDataReader["foreignMatter"];
                    if (!DBNull.Value.Equals(mDataReader["sacsAcceptes"])) mClass._AcceptedBags            = (int)mDataReader["sacsAcceptes"];
                    if (!DBNull.Value.Equals(mDataReader["poidsBrutLivre"])) mClass._GrossWeight              = (decimal)mDataReader["poidsBrutLivre"];
                    if (!DBNull.Value.Equals(mDataReader["qualityRetention"])) mClass._QualityRetention    = (decimal)mDataReader["qualityRetention"];
                    if (!DBNull.Value.Equals(mDataReader["tareLivraison"])) mClass._Tare                            = (decimal)mDataReader["tareLivraison"];
                    if (!DBNull.Value.Equals(mDataReader["poidsAccepte"])) mClass._AcceptedTonnage      = (decimal)mDataReader["poidsAccepte"];

                    LivraisonType dLivraisonType = new LivraisonType();
                    if (!DBNull.Value.Equals(mDataReader["typeLivraisonID"]))  dLivraisonType.ID = (int)mDataReader["typeLivraisonID"];
                    if (!DBNull.Value.Equals(mDataReader["typeLivraisonNom"]))  dLivraisonType.Designation = (string)mDataReader["typeLivraisonNom"];
                    mClass._LivraisonType = dLivraisonType;
                                        
                    Fournisseur sFournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["fournisseurID"])) sFournisseur.ID = (int)mDataReader["fournisseurID"];
                    if (!DBNull.Value.Equals(mDataReader["fournisseurNom"])) sFournisseur.Nom = (string)mDataReader["fournisseurNom"];
                    mClass._Fournisseur = sFournisseur;


                    Certification mCertification = new Certification();
                    if (!DBNull.Value.Equals(mDataReader["certificationID"])) mCertification.ID = (int)mDataReader["certificationID"];
                    if (!DBNull.Value.Equals(mDataReader["certificationNom"])) mCertification.Designation = (string)mDataReader["certificationNom"];
                    mClass._Certification = mCertification;

                    if (!DBNull.Value.Equals(mDataReader["numeroexterne"])) mClass._NumeroExterne = (string)mDataReader["numeroexterne"];
                    if (!DBNull.Value.Equals(mDataReader["origine"])) mClass._OrigineDesignation = (string)mDataReader["origine"];

                    if (!DBNull.Value.Equals(mDataReader["RefactionBrisures"])) mClass.RefactionBrisures = (decimal)mDataReader["RefactionBrisures"];
                    if (!DBNull.Value.Equals(mDataReader["RefactionHumidite"])) mClass.RefactionHumidite = (decimal)mDataReader["RefactionHumidite"];
                    if (!DBNull.Value.Equals(mDataReader["RefactionMatieresEtg"])) mClass.RefactionMatieresEtg = (decimal)mDataReader["RefactionMatieresEtg"];

                    Transporteur mTransporteur = new Transporteur();
                    if (!DBNull.Value.Equals(mDataReader["TransporteurNom"])) mTransporteur.Nom = (String)mDataReader["TransporteurNom"];
                    //if (!DBNull.Value.Equals(mDataReader["certificationNom"])) mCertification.Designation = (string)mDataReader["certificationNom"];
                    mClass._Transporteur = mTransporteur;

                    Site mSite = new Site();
                    if (!DBNull.Value.Equals(mDataReader["SiteNom"])) mSite.Nom = (String)mDataReader["SiteNom"];
                    mClass.Sites = mSite;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nSuiviLivraison:MapFromDataReader");
            }
        }

        public override bool fnGet(object Id)
        {
            throw new NotImplementedException();
        }

        public override List<DataPersist> fnSelect()
        {
            throw new NotImplementedException();
        }

        public override bool fnUpdate()
        {
            throw new NotImplementedException();
        }

        public override bool fnActivate()
        {
            throw new NotImplementedException();
        }

        public override bool fnDeActivate()
        {
            throw new NotImplementedException();
        }
        
        #endregion

    }


    public class SuiviLivraisonViewModel
    {
        public Livraison _Livraison { get; set; }

        public Recolte _Recolte { get; set; }

        public string _Campagne { get; set; }

        public Exportateur _Exportateur { get; set; }

        public Site _DefaultSite
        {
            get
            {
                Site mSite = new Site();
                mSite.fnGetDefaultSite();
                return mSite;
            }
        }

        public Parametres _Parametres { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}

    

