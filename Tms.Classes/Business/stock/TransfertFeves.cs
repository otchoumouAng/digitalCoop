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
    public class TransfertFeves : DataPersist
    {
        #region Fields
        private Guid _ID;
        private Campagne _Campagne;
        private Magasin _MagasinSource;
        private Magasin _MagasinDestination;
        private Exportateur _Exportateur;        
        private SacType _SacType;        
        private DateTime? _DateEntree;
        private DateTime? _DateSortie;
        private string _Sens;
        private string _Numero;
        private string _NumeroTransfert;        
        private decimal? _QuantiteEntree;
        private decimal? _QuantiteSortie;
        private decimal? _PoidsBrutEntree;
        private decimal? _PoidsBrutSortie;
        private decimal? _TareSacsEntree;
        private decimal?_TareSacsSortie;
        private decimal? _TarePaletteEntree;
        private decimal? _TarePaletteSortie;
        private decimal? _PoidsLivreEntree;
        private decimal? _PoidsLivreSortie;
        private decimal? _RetentionPoidsEntree;
        private decimal? _RetentionPoidsSortie;
        private decimal? _PoidsNetAccepteEntree;
        private decimal? _PoidsNetAccepteSortie;        
        private string _Statut;
        private string _DeviationJour;
        private int? _DeviationQuantite;
        private decimal? _DeviationPoids;
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

        public string CampagneDesignation
        {
            get { return _Campagne != null ? Campagne.Designation : string.Empty; }
        }

        public Magasin MagasinSource
        {
            get
            {
                return _MagasinSource;
            }

            set
            {
                _MagasinSource = value;
            }
        }

        public string MagasinSourceAsString
        {
            get { return _MagasinSource != null ? _MagasinSource.Designation : string.Empty; }
        }

        public Magasin MagasinDestination
        {
            get
            {
                return _MagasinDestination;
            }

            set
            {
                _MagasinDestination = value;
            }
        }

        public string MagasinDestinationAsString
        {
            get { return _MagasinDestination != null ? _MagasinDestination.Designation : string.Empty; }
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
            get { return _Exportateur != null ? _Exportateur.Nom : string.Empty; }
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
            get { return _SacType != null ? _SacType.Designation : string.Empty; }
        }
        public DateTime? DateEntree
        {
            get
            {
                return _DateEntree;
            }

            set
            {
                _DateEntree = value;
            }
        }

        public string DateEntreeAsString
        {
            get { return _DateEntree != null ? _DateEntree.Value.ToShortDateString() : string.Empty; }
        }
        public DateTime? DateSortie
        {
            get
            {
                return _DateSortie;
            }

            set
            {
                _DateSortie = value;
            }
        }

        public string DateSortieAsString
        {
            get { return _DateSortie != null ? _DateSortie.Value.ToShortDateString() : string.Empty; }
        }
        public string Sens
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

        public decimal? QuantiteEntree
        {
            get
            {
                return _QuantiteEntree;
            }

            set
            {
                _QuantiteEntree = value;
            }
        }

        public string QuantiteEntreeAsString
        {
            get { return _QuantiteEntree != 0 ? String.Format("{0:#,#}", _QuantiteEntree).TrimStart() : string.Empty; }
        }

        public decimal? QuantiteSortie
        {
            get
            {
                return _QuantiteSortie;
            }

            set
            {
                _QuantiteSortie = value;
            }
        }

        public string QuantiteSortieAsString
        {
            get { return _QuantiteSortie != 0 ? String.Format("{0:#,#}", _QuantiteSortie).TrimStart() : string.Empty; }
        }

        public decimal? PoidsBrutEntree
        {
            get
            {
                return _PoidsBrutEntree;
            }

            set
            {
                _PoidsBrutEntree = value;
            }
        }

        public string PoidsBrutEntreeAsString
        {
            get { return _PoidsBrutEntree != 0 ? String.Format("{0:#,#}", _PoidsBrutEntree).TrimStart() : string.Empty; }
        }

        public decimal? PoidsBrutSortie
        {
            get
            {
                return _PoidsBrutSortie;
            }

            set
            {
                _PoidsBrutSortie = value;
            }
        }

        public string PoidsBrutSortieAsString
        {
            get { return _PoidsBrutSortie != 0 ? String.Format("{0:#,#}", _PoidsBrutSortie).TrimStart() : string.Empty; }
        }

        public decimal? TareSacsEntree
        {
            get
            {
                return _TareSacsEntree;
            }

            set
            {
                _TareSacsEntree = value;
            }
        }

        public string TareSacsEntreeAsString
        {
            get { return _TareSacsEntree != 0 ? String.Format("{0:#,#}", _TareSacsEntree).TrimStart() : string.Empty; }
        }

        public decimal? TareSacsSortie
        {
            get
            {
                return _TareSacsSortie;
            }

            set
            {
                _TareSacsSortie = value;
            }
        }
        public string TareSacsSortieAsString
        {
            get { return _TareSacsSortie != 0 ? String.Format("{0:#,#}", _TareSacsSortie).TrimStart() : string.Empty; }
        }
        public decimal? TarePaletteEntree
        {
            get
            {
                return _TarePaletteEntree;
            }

            set
            {
                _TarePaletteEntree = value;
            }
        }
        public string TarePaletteEntreeAsString
        {
            get { return _TarePaletteEntree != 0 ? String.Format("{0:#,#}", _TarePaletteEntree).TrimStart() : string.Empty; }
        }
        public decimal? TarePaletteSortie
        {
            get
            {
                return _TarePaletteSortie;
            }

            set
            {
                _TarePaletteSortie = value;
            }
        }

        public string TarePaletteSortieAsString
        {
            get { return _TarePaletteSortie != 0 ? String.Format("{0:#,#}", _TarePaletteSortie).TrimStart() : string.Empty; }
        }

        public decimal? PoidsLivreEntree
        {
            get
            {
                return _PoidsLivreEntree;
            }

            set
            {
                _PoidsLivreEntree = value;
            }
        }

        public string PoidsLivreEntreeAsString
        {
            get { return _PoidsLivreEntree != 0 ? String.Format("{0:#,#}", _PoidsLivreEntree).TrimStart() : string.Empty; }
        }

        public decimal? PoidsLivreSortie
        {
            get
            {
                return _PoidsLivreSortie;
            }

            set
            {
                _PoidsLivreSortie = value;
            }
        }

        public string PoidsLivreSortieAsString
        {
            get { return _PoidsLivreSortie != 0 ? String.Format("{0:#,#}", _PoidsLivreSortie).TrimStart() : string.Empty; }
        }

        public decimal? RetentionPoidsEntree
        {
            get
            {
                return _RetentionPoidsEntree;
            }

            set
            {
                _RetentionPoidsEntree = value;
            }
        }

        public string RetentionPoidsEntreeAsString
        {
            get { return _RetentionPoidsEntree != 0 ? String.Format("{0:#,#}", _RetentionPoidsEntree).TrimStart() : string.Empty; }
        }

        public decimal? RetentionPoidsSortie
        {
            get
            {
                return _RetentionPoidsSortie;
            }

            set
            {
                _RetentionPoidsSortie = value;
            }
        }

        public string RetentionPoidsSortieAsString
        {
            get { return _RetentionPoidsSortie != 0 ? String.Format("{0:#,#}", _RetentionPoidsSortie).TrimStart() : string.Empty; }
        }

        public decimal? PoidsNetAccepteEntree
        {
            get
            {
                return _PoidsNetAccepteEntree;
            }

            set
            {
                _PoidsNetAccepteEntree = value;
            }
        }

        public string PoidsNetAccepteEntreeAsString
        {
            get { return _PoidsNetAccepteEntree != 0 ? String.Format("{0:#,#}", _PoidsNetAccepteEntree).TrimStart() : string.Empty; }
        }

        public decimal? PoidsNetAccepteSortie
        {
            get
            {
                return _PoidsNetAccepteSortie;
            }

            set
            {
                _PoidsNetAccepteSortie = value;
            }
        }

        public string PoidsNetAccepteSortieAsString
        {
            get { return _PoidsNetAccepteSortie != 0 ? String.Format("{0:#,#}", _PoidsNetAccepteSortie).TrimStart() : string.Empty; }
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

        public string DeviationJour
        {
            get
            {
                return _DeviationJour;
            }

            set
            {
                _DeviationJour = value;
            }
        }

        public int? DeviationQuantite
        {
            get
            {
                return _DeviationQuantite;
            }

            set
            {
                _DeviationQuantite = value;
            }
        }

        public string DeviationQuantiteAsString
        {
            get { return _DeviationQuantite != 0 ? String.Format("{0:#,#}", _DeviationQuantite).TrimStart() : string.Empty; }
        }

        public decimal? DeviationPoids
        {
            get
            {
                return _DeviationPoids;
            }

            set
            {
                _DeviationPoids = value;
            }
        }

        public string DeviationPoidsAsString
        {
            get { return _DeviationPoids != 0 ? String.Format("{0:#,#}", _DeviationPoids).TrimStart() : string.Empty; }
        }

        public bool ContientEcart
        {
            get { return _Statut == "LV" && _DeviationPoids > 0; }
        }
        [Column(Text = "")]
        public string mIcon
        {
            get
            {           
                if (_Statut == "NA")
                    return "2";
                else if (_Statut == "LV")
                    return "1"; // Tick                
                else
                    return "2";
            }
        }
        #endregion

        #region Constructor
        public TransfertFeves()
        {

        }

        public TransfertFeves(Guid myID)
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
            throw new NotImplementedException();
        }


        public override bool fnGet(object Id)
        {
            throw new NotImplementedException();
        }

        public override List<DataPersist> fnSelect()
        {
            return fnSelect( -1, -1, null, null, "-1");
        }

        public List<DataPersist> fnSelect(int MagasinSourceID, int MagasinDestinationID, DateTime? StartDate, DateTime? EndDate, string statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_TransfertFeves_Select");
                db().AddInParameter(mCommande, "@magasinsource", SqlDbType.Int, MagasinSourceID);
                db().AddInParameter(mCommande, "@magasindestination", SqlDbType.Int, MagasinDestinationID);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@statut", SqlDbType.Char,2, statut);
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

        public override bool fnUpdate()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Numero;
        }

        private static void MapFromDataReader(TransfertFeves mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    mClass.ID = Guid.NewGuid();

                    mClass.Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass.Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["magasinsourceID"]))
                    {
                        mClass._MagasinSource = new Magasin();
                        mClass._MagasinSource.ID = (int)mDataReader["magasinsourceID"];
                        mClass._MagasinSource.Designation = (string)mDataReader["magasinsourceDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["magasindestinationID"]))
                    {
                        mClass._MagasinDestination = new Magasin();
                        mClass._MagasinDestination.ID = (int)mDataReader["magasindestinationID"];
                        mClass._MagasinDestination.Designation = (string)mDataReader["magasindestinationDesignation"];
                    }

                    
                    if (!DBNull.Value.Equals(mDataReader["ExportateurID"]))
                    {
                        mClass.Exportateur = new Exportateur();
                        mClass.Exportateur.ID = (int)mDataReader["ExportateurID"];
                        mClass.Exportateur.Nom = (string)mDataReader["ExportateurNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["SacTypeID"]))
                    {
                        mClass.SacType = new SacType();
                        mClass.SacType.ID = (int)mDataReader["SacTypeID"];
                        mClass.SacType.Designation = (string)mDataReader["SactypeDesignation"];
                    }      

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass.Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroTransfert"])) mClass.NumeroTransfert = (string)mDataReader["NumeroTransfert"];

                    if (!DBNull.Value.Equals(mDataReader["DateEntree"])) mClass._DateEntree = (DateTime)mDataReader["DateEntree"];
                    if (!DBNull.Value.Equals(mDataReader["DateSortie"])) mClass._DateSortie = (DateTime)mDataReader["DateSortie"];
                    //if (!DBNull.Value.Equals(mDataReader["Sens"])) mClass.Sens = (string)mDataReader["Sens"];
                    if (!DBNull.Value.Equals(mDataReader["QuantiteEntree"])) mClass._QuantiteEntree = (int)mDataReader["QuantiteEntree"];
                    if (!DBNull.Value.Equals(mDataReader["QuantiteSortie"])) mClass._QuantiteSortie = (int)mDataReader["QuantiteSortie"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrutEntre"])) mClass._PoidsBrutEntree = (decimal)mDataReader["PoidsBrutEntre"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrutSortie"])) mClass._PoidsBrutSortie = (decimal)mDataReader["PoidsBrutSortie"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacsEntree"])) mClass._TareSacsEntree = (decimal)mDataReader["TareSacsEntree"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacsSortie"])) mClass._TareSacsSortie = (decimal)mDataReader["TareSacsSortie"];
                    if (!DBNull.Value.Equals(mDataReader["TarePaletteEntree"])) mClass._TarePaletteEntree = (decimal)mDataReader["TarePaletteEntree"];
                    if (!DBNull.Value.Equals(mDataReader["TarePaletteSortie"])) mClass._TarePaletteSortie = (decimal)mDataReader["TarePaletteSortie"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsLivreEntree"])) mClass._PoidsLivreEntree = (decimal)mDataReader["PoidsLivreEntree"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsLivreSortie"])) mClass._PoidsLivreSortie = (decimal)mDataReader["PoidsLivreSortie"];
                    if (!DBNull.Value.Equals(mDataReader["RefactionEntree"])) mClass._RetentionPoidsEntree = (decimal)mDataReader["RefactionEntree"];
                    if (!DBNull.Value.Equals(mDataReader["RefactionSortie"])) mClass._RetentionPoidsSortie = (decimal)mDataReader["RefactionSortie"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsAccepteEntree"])) mClass._PoidsNetAccepteEntree = (decimal)mDataReader["PoidsAccepteEntree"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsAccepteSortie"])) mClass._PoidsNetAccepteSortie = (decimal)mDataReader["PoidsAccepteSortie"];
                    if (!DBNull.Value.Equals(mDataReader["statut"])) mClass.Statut = (string)mDataReader["statut"];

                    if (!DBNull.Value.Equals(mDataReader["DeviationJours"])) mClass._DeviationJour = (string)mDataReader["DeviationJours"];
                    if (!DBNull.Value.Equals(mDataReader["DeviationQuantite"])) mClass._DeviationQuantite = (int)mDataReader["DeviationQuantite"];
                    if (!DBNull.Value.Equals(mDataReader["DeviationPoids"])) mClass._DeviationPoids = (decimal)mDataReader["DeviationPoids"];

                    //if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    //if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    //if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    //if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];                                                          
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nTransfertFeves:MapFromDataReader");
            }
        }

        #endregion
    }

    public partial class TransfertFevesViewModel
    {
        public TransfertFeves _TransfertFeves { get; set; }
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
