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

namespace Tms.Classes.Business.stock
{
    public class PeseeProduction : DataPersist
    {
        #region fields
        private Guid _ID;
        private Campagne _Campagne;
        private PeseeProductionType _PeseeProductionType;
        private OrdreProduction _OrdreProduction;
        private BonDeLivraison _BonDeLivraison;
        private FicheLotCertifie _FicheLotCertifie;
        private Empotage _Empotage;
        private Conteneur _Conteneur;
        private Lot _Lot;
        private Lot _LotReusine;
        private LotType _LotType;
        private Certification _Certification;
        private DateTime _DatePeseeProduction;
        private string _ReferenceObjet;
        private int _NumeroPesee;
        private int _NombreSacs;
        private decimal _PoidsBrut;
        private decimal _TareSacs;
        private decimal _TarePalette;
        private decimal _PoidsNet;
        private string _Statut;
        private string _Commentaire;
        private bool _Desactive;
        private bool _IsManual;        
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

        public PeseeProductionType PeseeProductionType
        {
            get
            {
                return _PeseeProductionType;
            }

            set
            {
                _PeseeProductionType = value;
            }
        }

        public OrdreProduction OrdreProduction
        {
            get
            {
                return _OrdreProduction;
            }

            set
            {
                _OrdreProduction = value;
            }
        }
        public string NumeroProduction
        {
            get { return _OrdreProduction != null ? _OrdreProduction.NumeroProduction : string.Empty; }
        }

        public BonDeLivraison BonDeLivraison
        {
            get
            {
                return _BonDeLivraison;
            }

            set
            {
                _BonDeLivraison = value;
            }
        }

        public string LivraisonID
        {
            get { return _BonDeLivraison != null && _BonDeLivraison.Livraison != null ? _BonDeLivraison.Livraison.Numero  : string.Empty; }
        }

        public string Immatriculation
        {
            get { return _BonDeLivraison != null ? _BonDeLivraison.Immatriculation : string.Empty; }
        }

        public Lot Lot
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


        public string NumeroLot
        {
            get { return _Lot != null ? _Lot.NumeroLot : string.Empty; }
        }

        public LotType LotType
        {
            get
            {
                return _LotType;
            }

            set
            {
                _LotType = value;
            }
        }

        public string LotTypeAsString
        {
            get { return _LotType != null ? _LotType.Designation : string.Empty; }
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

        public string CertificationString
        {
            get { return _Certification != null ? _Certification.Designation : string.Empty; }
        }

        public DateTime DatePeseeProduction
        {
            get
            {
                return _DatePeseeProduction;
            }

            set
            {
                _DatePeseeProduction = value;
            }
        }

        public string DatePeseeProductionAsString
        {
            get { return _DatePeseeProduction != null ? _DatePeseeProduction.ToShortDateString() : string.Empty; }
        }

        public string ReferenceObjet
        {
            get
            {
                return _ReferenceObjet;
            }

            set
            {
                _ReferenceObjet = value;
            }
        }

        public int NumeroPesee
        {
            get
            {
                return _NumeroPesee;
            }

            set
            {
                _NumeroPesee = value;
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
                    return 3; //                                 
                else
                    return 2; // 
            }
        }

        public bool IsManual
        {
            get
            {
                return _IsManual;
            }

            set
            {
                _IsManual = value;
            }
        }

        public FicheLotCertifie FicheLotCertifie
        {
            get
            {
                return _FicheLotCertifie;
            }

            set
            {
                _FicheLotCertifie = value;
            }
        }

        public Lot LotReusine
        {
            get
            {
                return _LotReusine;
            }

            set
            {
                _LotReusine = value;
            }
        }

        public Empotage Empotage
        {
            get
            {
                return _Empotage;
            }

            set
            {
                _Empotage = value;
            }
        }

        public Conteneur Conteneur
        {
            get
            {
                return _Conteneur;
            }

            set
            {
                _Conteneur = value;
            }
        }

        public string NumeroOT
        {
            get { return (Empotage != null && Empotage.Embarquement != null) ? Empotage.Embarquement.Numero : string.Empty; }
        }
        #endregion


        #region Constructor
        public PeseeProduction()
        {

        }

        public PeseeProduction(Guid myId)
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

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public partial class PeseeProductionViewModel
    {
        public PeseeProduction _PeseeProduction { get; set; }

        public string _DefaultCampagne { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }

    public partial class PeseeAvantUsinage : PeseeProduction
    {
        public int TypePesee
        {
            get { return new Parametres(0).PeseeAvantUsinage; }
        }
        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "MouvementStock:fnDeActivate");
            }
            return bolResult;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_PeseeProduction_Get", (Guid)Id);
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
            return fnSelect("-1", null, null,"-1");
        }

        public virtual List<DataPersist> fnSelect(string CampagneID, DateTime? StartDate, DateTime? EndDate, string status)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_Select");

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char,9, CampagneID);
                db().AddInParameter(mCommande, "@TypeID", SqlDbType.Int, TypePesee);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@DateFin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char,2, status);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PeseeAvantUsinage mClass = new PeseeAvantUsinage();

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
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.Int, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@TypePeseeProductionID", SqlDbType.Int, PeseeProductionType.ID);
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, Campagne.Designation);
                db().AddInParameter(mCommande, "@DatePesee", SqlDbType.DateTime, DatePeseeProduction);
                db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, OrdreProduction.ID);
                db().AddInParameter(mCommande, "@BonDeLivraisonID", SqlDbType.UniqueIdentifier, BonDeLivraison.ID);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, Commentaire);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, PoidsNet);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, Statut);
                db().AddInParameter(mCommande, "@EstManuel", SqlDbType.Bit, IsManual);

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
                            NumeroPesee = (int)db().Parameters(mCommande, "@Numero");
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
                throw new Exception(ex.Message + "\r\n" + "PeseeAvantUsinage:fnUpdate");

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
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.Int,10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@TypePeseeProductionID", SqlDbType.Int, PeseeProductionType.ID);
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char,9, Campagne.Designation);
                db().AddInParameter(mCommande, "@DatePesee", SqlDbType.DateTime, DatePeseeProduction);
                db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, OrdreProduction.ID);                                
                db().AddInParameter(mCommande, "@BonDeLivraisonID", SqlDbType.UniqueIdentifier, BonDeLivraison.ID);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, Commentaire);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, PoidsBrut);                
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, TarePalette);                
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, PoidsNet);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, Statut);
                db().AddInParameter(mCommande, "@EstManuel", SqlDbType.Bit, IsManual);

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
                            NumeroPesee = (int)db().Parameters(mCommande, "@Numero");
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
                throw new Exception(ex.Message + "\r\n" + "PeseeAvantUsinage:fnUpdate");

            }
            return Result;
        }


        private static void MapFromDataReader(PeseeAvantUsinage mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass.ID = (Guid)mDataReader["ID"];

                    mClass.Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass.Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["ProductionID"]))
                    {
                        mClass.OrdreProduction = new OrdreProduction();
                        mClass.OrdreProduction.ID = (Guid)mDataReader["ProductionID"];
                        mClass.OrdreProduction.NumeroProduction = (string)mDataReader["NumeroProduction"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["BonDeLivraisonID"]))
                    {
                        mClass.BonDeLivraison = new BonDeLivraison();
                        mClass.BonDeLivraison.ID = (Guid)mDataReader["BonDeLivraisonID"];
                        mClass.BonDeLivraison.Livraison = new Livraison();
                        mClass.BonDeLivraison.Livraison.ID = (Guid)mDataReader["BonDeLivraisonID"];
                        mClass.BonDeLivraison.Livraison.Numero = (string)mDataReader["LivraisonID"]; 
                        mClass.BonDeLivraison.Livraison.SacsAcceptes = (int)mDataReader["LivraisonSacsAcceptes"];
                        mClass.BonDeLivraison.Livraison.Immatriculation = (string)mDataReader["LivraisonImmatriculation"];
                        mClass.BonDeLivraison.Livraison.TareSacs = (decimal)mDataReader["TareUnitaireSacs"];
                    }
                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass.NumeroPesee = (int)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DatePesee"])) mClass.DatePeseeProduction = (DateTime)mDataReader["DatePesee"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass.Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass.NombreSacs = (int)mDataReader["NombreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass.PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass.TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass.TarePalette = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass.PoidsNet = (decimal)mDataReader["PoidsNet"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass.Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["EstManuel"])) mClass.IsManual = (bool)mDataReader["EstManuel"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass.Statut = (string)mDataReader["Statut"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n PeseeAvantUsinage:MapFromDataReader");
            }
        }


    }

    public partial class PeseeAvantUsinageViewModel
    {
        public PeseeAvantUsinage _PeseeAvantUsinage { get; set; }

        public string _DefaultCampagne { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }

    public partial class PeseeApresUsinage : PeseeProduction
    {
        public int TypePesee
        {
            get { return new Parametres(0).PeseeApresUsinage; }
        }
        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_AnnulerLot");
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
                throw new Exception(ex.Message + "\r\n" + "MouvementStock:fnDeActivate");
            }
            return bolResult;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_PeseeProduction_Get", (Guid)Id);
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
            return fnSelect("-1", null, null, "-1");
        }

        public virtual List<DataPersist> fnSelect(string CampagneID, DateTime? StartDate, DateTime? EndDate, string status)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_Select");

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, CampagneID);
                db().AddInParameter(mCommande, "@TypeID", SqlDbType.Int, TypePesee);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@DateFin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, status);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PeseeApresUsinage mClass = new PeseeApresUsinage();

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
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.Int, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@TypePeseeProductionID", SqlDbType.Int, PeseeProductionType.ID);
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, Campagne.Designation);
                db().AddInParameter(mCommande, "@DatePesee", SqlDbType.DateTime, DatePeseeProduction);
                db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, OrdreProduction.ID);
                db().AddInParameter(mCommande, "@BonDeLivraisonID", SqlDbType.UniqueIdentifier, DBNull.Value);
                if (Lot != null)
                    db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, Lot.ID);
                else
                    db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, DBNull.Value);
                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int, LotType.ID);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, Commentaire);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, PoidsNet);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, Statut);
                db().AddInParameter(mCommande, "@EstManuel", SqlDbType.Bit, IsManual);

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
                            NumeroPesee = (int)db().Parameters(mCommande, "@Numero");
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
                throw new Exception(ex.Message + "\r\n" + "PeseeApresUsinage:fnUpdate");

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
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.Int, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@TypePeseeProductionID", SqlDbType.Int, PeseeProductionType.ID);
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, Campagne.Designation);
                db().AddInParameter(mCommande, "@DatePesee", SqlDbType.DateTime, DatePeseeProduction);
                db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, OrdreProduction.ID);                
                db().AddInParameter(mCommande, "@BonDeLivraisonID", SqlDbType.UniqueIdentifier, DBNull.Value);
                if (Lot != null)
                    db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, Lot.ID);
                else
                    db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, DBNull.Value);
                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int,LotType.ID);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, Commentaire);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, PoidsNet);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, Statut);
                db().AddInParameter(mCommande, "@EstManuel", SqlDbType.Bit, IsManual);

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
                            NumeroPesee = (int)db().Parameters(mCommande, "@Numero");
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
                throw new Exception(ex.Message + "\r\n" + "PeseeApresUsinage:fnUpdate");

            }
            return Result;
        }

        private static void MapFromDataReader(PeseeApresUsinage mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass.ID = (Guid)mDataReader["ID"];

                    mClass.Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass.Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["ProductionID"]))
                    {
                        mClass.OrdreProduction = new OrdreProduction();
                        mClass.OrdreProduction.ID = (Guid)mDataReader["ProductionID"];
                        mClass.OrdreProduction.NumeroProduction = (string)mDataReader["NumeroProduction"];
                    }

                    mClass.Lot = new Lot();
                    if (!DBNull.Value.Equals(mDataReader["LotID"])) mClass.Lot.ID = (Guid)mDataReader["LotID"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroLot"])) mClass.Lot.NumeroLot = (string)mDataReader["NumeroLot"];

                    mClass.LotType = new LotType();
                    if (!DBNull.Value.Equals(mDataReader["TypeLotID"])) mClass.LotType.ID = (int)mDataReader["TypeLotID"];
                    if (!DBNull.Value.Equals(mDataReader["TypeLotDesignation"])) mClass.LotType.Designation = (string)mDataReader["TypeLotDesignation"];                    

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass.NumeroPesee = (int)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DatePesee"])) mClass.DatePeseeProduction = (DateTime)mDataReader["DatePesee"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass.Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass.NombreSacs = (int)mDataReader["NombreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass.PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass.TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass.TarePalette = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass.PoidsNet = (decimal)mDataReader["PoidsNet"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass.Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["EstManuel"])) mClass.IsManual = (bool)mDataReader["EstManuel"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass.Statut = (string)mDataReader["Statut"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n PeseeAvantUsinage:MapFromDataReader");
            }
        }
    }

    public partial class PeseeApresUsinageViewModel
    {
        public PeseeApresUsinage _PeseeApresUsinage { get; set; }

        public string _DefaultCampagne { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }

    public partial class PeseeLotCertifie : PeseeProduction
    {
        public int TypePesee
        {
            get { return new Parametres(0).PeseeCertifiee; }
        }
        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "MouvementStock:fnDeActivate");
            }
            return bolResult;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_PeseeProduction_Get", (Guid)Id);
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
            return fnSelect("-1", null, null, "-1");
        }

        public virtual List<DataPersist> fnSelect(string CampagneID, DateTime? StartDate, DateTime? EndDate, string status)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_Select");

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, CampagneID);
                db().AddInParameter(mCommande, "@TypeID", SqlDbType.Int, TypePesee);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@DateFin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, status);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PeseeLotCertifie mClass = new PeseeLotCertifie();

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
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.Int, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@TypePeseeProductionID", SqlDbType.Int, PeseeProductionType.ID);
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, Campagne.Designation);
                db().AddInParameter(mCommande, "@DatePesee", SqlDbType.DateTime, DatePeseeProduction);
                db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, OrdreProduction.ID);
                db().AddInParameter(mCommande, "@BonDeLivraisonID", SqlDbType.UniqueIdentifier, DBNull.Value);
                db().AddInParameter(mCommande, "@FicheLotCertifieID", SqlDbType.UniqueIdentifier, FicheLotCertifie.ID);

                if (Lot != null)
                    db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, Lot.ID);
                else
                    db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, DBNull.Value);

                if (Certification != null)
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, Certification.ID);
                else
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int, LotType.ID);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, Commentaire);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, PoidsNet);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, Statut);
                db().AddInParameter(mCommande, "@EstManuel", SqlDbType.Bit, IsManual);

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
                            NumeroPesee = (int)db().Parameters(mCommande, "@Numero");
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
                throw new Exception(ex.Message + "\r\n" + "PeseeLotCertifie:fnUpdate");

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
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.Int, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@TypePeseeProductionID", SqlDbType.Int, PeseeProductionType.ID);
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, Campagne.Designation);
                db().AddInParameter(mCommande, "@DatePesee", SqlDbType.DateTime, DatePeseeProduction);
                db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, OrdreProduction.ID);
                db().AddInParameter(mCommande, "@BonDeLivraisonID", SqlDbType.UniqueIdentifier, DBNull.Value);
                db().AddInParameter(mCommande, "@FicheLotCertifieID", SqlDbType.UniqueIdentifier, FicheLotCertifie.ID);

                if (Lot != null)
                    db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, Lot.ID);
                else
                    db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, DBNull.Value);

                if (Certification != null)
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, Certification.ID);
                else
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int, LotType.ID);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, Commentaire);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, PoidsNet);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, Statut);
                db().AddInParameter(mCommande, "@EstManuel", SqlDbType.Bit, IsManual);

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
                            NumeroPesee = (int)db().Parameters(mCommande, "@Numero");
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
                throw new Exception(ex.Message + "\r\n" + "PeseeLotCertifie:fnUpdate");

            }
            return Result;
        }

        private static void MapFromDataReader(PeseeLotCertifie mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass.ID = (Guid)mDataReader["ID"];

                    mClass.Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass.Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["ProductionID"]))
                    {
                        mClass.OrdreProduction = new OrdreProduction();
                        mClass.OrdreProduction.ID = (Guid)mDataReader["ProductionID"];
                        mClass.OrdreProduction.NumeroProduction = (string)mDataReader["NumeroProduction"];
                    }

                    mClass.Lot = new Lot();
                    if (!DBNull.Value.Equals(mDataReader["LotID"])) mClass.Lot.ID = (Guid)mDataReader["LotID"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroLot"])) mClass.Lot.NumeroLot = (string)mDataReader["NumeroLot"];

                    mClass.LotType = new LotType();
                    if (!DBNull.Value.Equals(mDataReader["TypeLotID"])) mClass.LotType.ID = (int)mDataReader["TypeLotID"];
                    if (!DBNull.Value.Equals(mDataReader["TypeLotDesignation"])) mClass.LotType.Designation = (string)mDataReader["TypeLotDesignation"];

                    mClass.Certification = new Certification();
                    if (!DBNull.Value.Equals(mDataReader["CertificationID"])) mClass.Certification.ID = (int)mDataReader["CertificationID"];
                    if (!DBNull.Value.Equals(mDataReader["Certification"])) mClass.Certification.Designation = (string)mDataReader["Certification"];

                    if (!DBNull.Value.Equals(mDataReader["FicheLotCertifieID"]))
                    {
                        mClass.FicheLotCertifie = new FicheLotCertifie();
                        mClass.FicheLotCertifie.ID = (Guid)mDataReader["FicheLotCertifieID"];
                        mClass.FicheLotCertifie.Numero = (string)mDataReader["NumeroFicheLotCertifie"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass.NumeroPesee = (int)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DatePesee"])) mClass.DatePeseeProduction = (DateTime)mDataReader["DatePesee"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass.Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass.NombreSacs = (int)mDataReader["NombreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass.PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass.TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass.TarePalette = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass.PoidsNet = (decimal)mDataReader["PoidsNet"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass.Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["EstManuel"])) mClass.IsManual = (bool)mDataReader["EstManuel"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass.Statut = (string)mDataReader["Statut"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n PeseeLotCertifie:MapFromDataReader");
            }
        }
    }

    public partial class PeseeLotCertifieViewModel
    {
        public PeseeLotCertifie _PeseeLotCertifie { get; set; }

        public string _DefaultCampagne { get; set; }
        public int _DefaultTypeLotCertifie { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }

    public partial class PeseeDiverse : PeseeProduction
    {
        public int TypePesee
        {
            get { return new Parametres(0).PeseeDiverse; }
        }
        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "MouvementStock:fnDeActivate");
            }
            return bolResult;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_PeseeProduction_Get", (Guid)Id);
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
            return fnSelect("-1", null, null, "-1");
        }

        public virtual List<DataPersist> fnSelect(string CampagneID, DateTime? StartDate, DateTime? EndDate, string status)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_Select");

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, CampagneID);
                db().AddInParameter(mCommande, "@TypeID", SqlDbType.Int, TypePesee);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@DateFin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, status);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PeseeDiverse mClass = new PeseeDiverse();

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
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.Int, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@TypePeseeProductionID", SqlDbType.Int, PeseeProductionType.ID);
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, Campagne.Designation);
                db().AddInParameter(mCommande, "@DatePesee", SqlDbType.DateTime, DatePeseeProduction);
                if (OrdreProduction != null)
                    db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, OrdreProduction.ID);
                else
                    db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, DBNull.Value);

                db().AddInParameter(mCommande, "@BonDeLivraisonID", SqlDbType.UniqueIdentifier, DBNull.Value);

                if (Lot != null)
                    db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, Lot.ID);
                else
                    db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, DBNull.Value);

                if (Certification != null)
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, Certification.ID);
                else
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int, DBNull.Value);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, Commentaire);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, PoidsNet);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, Statut);
                db().AddInParameter(mCommande, "@EstManuel", SqlDbType.Bit, IsManual);
                db().AddInParameter(mCommande, "@ReferenceObjet", SqlDbType.VarChar, ReferenceObjet);

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
                            NumeroPesee = (int)db().Parameters(mCommande, "@Numero");
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
                throw new Exception(ex.Message + "\r\n" + "PeseeLotCertifie:fnUpdate");

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
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.Int, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@TypePeseeProductionID", SqlDbType.Int, PeseeProductionType.ID);
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, Campagne.Designation);
                db().AddInParameter(mCommande, "@DatePesee", SqlDbType.DateTime, DatePeseeProduction);
                if (OrdreProduction != null)
                    db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, OrdreProduction.ID);
                else
                    db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, DBNull.Value);
                
                db().AddInParameter(mCommande, "@BonDeLivraisonID", SqlDbType.UniqueIdentifier, DBNull.Value);

                if (Lot != null)
                    db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, Lot.ID);
                else
                    db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, DBNull.Value);

                if (Certification != null)
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, Certification.ID);
                else
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int, DBNull.Value);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, Commentaire);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, PoidsNet);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, Statut);
                db().AddInParameter(mCommande, "@EstManuel", SqlDbType.Bit, IsManual);
                db().AddInParameter(mCommande, "@ReferenceObjet", SqlDbType.VarChar, ReferenceObjet);

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
                            NumeroPesee = (int)db().Parameters(mCommande, "@Numero");
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
                throw new Exception(ex.Message + "\r\n" + "PeseeLotCertifie:fnUpdate");

            }
            return Result;
        }

        private static void MapFromDataReader(PeseeDiverse mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass.ID = (Guid)mDataReader["ID"];

                    mClass.Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass.Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["ProductionID"]))
                    {
                        mClass.OrdreProduction = new OrdreProduction();
                        mClass.OrdreProduction.ID = (Guid)mDataReader["ProductionID"];
                        mClass.OrdreProduction.NumeroProduction = (string)mDataReader["NumeroProduction"];
                    }

                    mClass.Lot = new Lot();
                    if (!DBNull.Value.Equals(mDataReader["LotID"])) mClass.Lot.ID = (Guid)mDataReader["LotID"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroLot"])) mClass.Lot.NumeroLot = (string)mDataReader["NumeroLot"];

                    mClass.LotType = new LotType();
                    if (!DBNull.Value.Equals(mDataReader["TypeLotID"])) mClass.LotType.ID = (int)mDataReader["TypeLotID"];
                    if (!DBNull.Value.Equals(mDataReader["TypeLotDesignation"])) mClass.LotType.Designation = (string)mDataReader["TypeLotDesignation"];

                    mClass.Certification = new Certification();
                    if (!DBNull.Value.Equals(mDataReader["CertificationID"])) mClass.LotType.ID = (int)mDataReader["CertificationID"];
                    if (!DBNull.Value.Equals(mDataReader["Certification"])) mClass.LotType.Designation = (string)mDataReader["Certification"];

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass.NumeroPesee = (int)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DatePesee"])) mClass.DatePeseeProduction = (DateTime)mDataReader["DatePesee"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass.Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass.NombreSacs = (int)mDataReader["NombreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass.PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass.TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass.TarePalette = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass.PoidsNet = (decimal)mDataReader["PoidsNet"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass.Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["EstManuel"])) mClass.IsManual = (bool)mDataReader["EstManuel"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass.Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["ReferenceObjet"])) mClass.ReferenceObjet = (string)mDataReader["ReferenceObjet"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n PeseeDiverse:MapFromDataReader");
            }
        }
    }

    public partial class PeseeDiverseViewModel
    {
        public PeseeDiverse _PeseeDiverse { get; set; }

        public string _DefaultCampagne { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }

    public partial class PeseeAvantEmpotage : PeseeProduction
    {
        public int TypePesee
        {
            get { return new Parametres(0).PeseeAvantEmpotage; }
        }
        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "MouvementStock:fnDeActivate");
            }
            return bolResult;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_PeseeProduction_Get", (Guid)Id);
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
            return fnSelect("-1", null, null, "-1");
        }

        public virtual List<DataPersist> fnSelect(string CampagneID, DateTime? StartDate, DateTime? EndDate, string status)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_Select");

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, CampagneID);
                db().AddInParameter(mCommande, "@TypeID", SqlDbType.Int, TypePesee);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@DateFin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, status);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PeseeAvantEmpotage mClass = new PeseeAvantEmpotage();

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
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.Int, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@TypePeseeProductionID", SqlDbType.Int, PeseeProductionType.ID);
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, Campagne.Designation);
                db().AddInParameter(mCommande, "@DatePesee", SqlDbType.DateTime, DatePeseeProduction);
                db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, DBNull.Value);
                db().AddInParameter(mCommande, "@BonDeLivraisonID", SqlDbType.UniqueIdentifier, DBNull.Value);
                if (Empotage != null)
                    db().AddInParameter(mCommande, "@EmpotageID", SqlDbType.UniqueIdentifier, Empotage.ID);
                else
                    db().AddInParameter(mCommande, "@EmpotageID", SqlDbType.UniqueIdentifier, DBNull.Value);

                if (Conteneur != null)
                    db().AddInParameter(mCommande, "@ConteneurID", SqlDbType.UniqueIdentifier, Conteneur.ID);
                else
                    db().AddInParameter(mCommande, "@ConteneurID", SqlDbType.UniqueIdentifier, DBNull.Value);

                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int, LotType.ID);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, Commentaire);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, PoidsNet);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, Statut);
                db().AddInParameter(mCommande, "@EstManuel", SqlDbType.Bit, IsManual);

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
                            NumeroPesee = (int)db().Parameters(mCommande, "@Numero");
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
                throw new Exception(ex.Message + "\r\n" + "PeseeAvantEmpotage:fnUpdate");

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
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.Int, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@TypePeseeProductionID", SqlDbType.Int, PeseeProductionType.ID);
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, Campagne.Designation);
                db().AddInParameter(mCommande, "@DatePesee", SqlDbType.DateTime, DatePeseeProduction);
                db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, DBNull.Value);
                db().AddInParameter(mCommande, "@BonDeLivraisonID", SqlDbType.UniqueIdentifier, DBNull.Value);
                if (Empotage != null)
                    db().AddInParameter(mCommande, "@EmpotageID", SqlDbType.UniqueIdentifier, Empotage.ID);
                else
                    db().AddInParameter(mCommande, "@EmpotageID", SqlDbType.UniqueIdentifier, DBNull.Value);

                if (Conteneur != null)
                    db().AddInParameter(mCommande, "@ConteneurID", SqlDbType.UniqueIdentifier, Conteneur.ID);
                else
                    db().AddInParameter(mCommande, "@ConteneurID", SqlDbType.UniqueIdentifier, DBNull.Value);

                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int, LotType.ID);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, Commentaire);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, PoidsNet);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, Statut);
                db().AddInParameter(mCommande, "@EstManuel", SqlDbType.Bit, IsManual);

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
                            NumeroPesee = (int)db().Parameters(mCommande, "@Numero");
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
                throw new Exception(ex.Message + "\r\n" + "PeseeAvantEmpotage:fnUpdate");

            }
            return Result;
        }

        private static void MapFromDataReader(PeseeAvantEmpotage mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass.ID = (Guid)mDataReader["ID"];

                    mClass.Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass.Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["ProductionID"]))
                    {
                        mClass.OrdreProduction = new OrdreProduction();
                        mClass.OrdreProduction.ID = (Guid)mDataReader["ProductionID"];
                        mClass.OrdreProduction.NumeroProduction = (string)mDataReader["NumeroProduction"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["empotageID"]))
                    {
                        mClass.Empotage = new Empotage();
                        mClass.Empotage.Embarquement = new Embarquement();
                        mClass.Empotage.ID = (Guid)mDataReader["empotageID"];
                        mClass.Empotage.Date = (DateTime)mDataReader["DateEmpotage"];
                        mClass.Empotage.Embarquement.Numero = (string)mDataReader["NumeroOT"];
                    }

                    mClass.Conteneur = new Conteneur();
                    if (!DBNull.Value.Equals(mDataReader["EmpotageDetailID"])) mClass.Conteneur.ID = (Guid)mDataReader["EmpotageDetailID"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroConteneur"])) mClass.Conteneur.Numero = (string)mDataReader["NumeroConteneur"];

                    mClass.LotType = new LotType();
                    if (!DBNull.Value.Equals(mDataReader["TypeLotID"])) mClass.LotType.ID = (int)mDataReader["TypeLotID"];
                    if (!DBNull.Value.Equals(mDataReader["TypeLotDesignation"])) mClass.LotType.Designation = (string)mDataReader["TypeLotDesignation"];

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass.NumeroPesee = (int)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DatePesee"])) mClass.DatePeseeProduction = (DateTime)mDataReader["DatePesee"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass.Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass.NombreSacs = (int)mDataReader["NombreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass.PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass.TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass.TarePalette = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass.PoidsNet = (decimal)mDataReader["PoidsNet"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass.Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["EstManuel"])) mClass.IsManual = (bool)mDataReader["EstManuel"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass.Statut = (string)mDataReader["Statut"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n PeseeAvantEmpotage:MapFromDataReader");
            }
        }
    }

    public partial class PeseeAvantEmpotageViewModel
    {
        public PeseeAvantEmpotage _PeseeAvantEmpotage { get; set; }

        public string _DefaultCampagne { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }

    public partial class PeseeApresReUsinage : PeseeProduction
    {
        public int TypePesee
        {
            get { return new Parametres(0).PeseeApresReUsinage; }
        }
        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "PeseeApresReUsinage:fnDeActivate");
            }
            return bolResult;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_PeseeProduction_Get", (Guid)Id);
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
            return fnSelect("-1", null, null, "-1");
        }

        public virtual List<DataPersist> fnSelect(string CampagneID, DateTime? StartDate, DateTime? EndDate, string status)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_Select");

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, CampagneID);
                db().AddInParameter(mCommande, "@TypeID", SqlDbType.Int, TypePesee);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@DateFin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, status);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PeseeApresReUsinage mClass = new PeseeApresReUsinage();

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
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.Int, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@TypePeseeProductionID", SqlDbType.Int, PeseeProductionType.ID);
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, Campagne.Designation);
                db().AddInParameter(mCommande, "@DatePesee", SqlDbType.DateTime, DatePeseeProduction);
                db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, DBNull.Value);
                db().AddInParameter(mCommande, "@BonDeLivraisonID", SqlDbType.UniqueIdentifier, DBNull.Value);
                if (Lot != null)
                    db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, Lot.ID);
                else
                    db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, DBNull.Value);

                if (LotReusine != null)
                    db().AddInParameter(mCommande, "@LotReusineID", SqlDbType.UniqueIdentifier, LotReusine.ID);
                else
                    db().AddInParameter(mCommande, "@LotReusineID", SqlDbType.UniqueIdentifier, DBNull.Value);

                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int, LotType.ID);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, Commentaire);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, PoidsNet);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, Statut);
                db().AddInParameter(mCommande, "@EstManuel", SqlDbType.Bit, IsManual);

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
                            NumeroPesee = (int)db().Parameters(mCommande, "@Numero");
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
                throw new Exception(ex.Message + "\r\n" + "PeseeApresReUsinage:fnUpdate");

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
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.Int, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProduction_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@TypePeseeProductionID", SqlDbType.Int, PeseeProductionType.ID);
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, Campagne.Designation);
                db().AddInParameter(mCommande, "@DatePesee", SqlDbType.DateTime, DatePeseeProduction);
                db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, DBNull.Value);
                db().AddInParameter(mCommande, "@BonDeLivraisonID", SqlDbType.UniqueIdentifier, DBNull.Value);
                if (Lot != null)
                    db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, Lot.ID);
                else
                    db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, DBNull.Value);

                if (LotReusine != null)
                    db().AddInParameter(mCommande, "@LotReusineID", SqlDbType.UniqueIdentifier, LotReusine.ID);
                else
                    db().AddInParameter(mCommande, "@LotReusineID", SqlDbType.UniqueIdentifier, DBNull.Value);

                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int, LotType.ID);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, Commentaire);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, PoidsNet);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, Statut);
                db().AddInParameter(mCommande, "@EstManuel", SqlDbType.Bit, IsManual);

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
                            NumeroPesee = (int)db().Parameters(mCommande, "@Numero");
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
                throw new Exception(ex.Message + "\r\n" + "PeseeApresReUsinage:fnUpdate");

            }
            return Result;
        }

        private static void MapFromDataReader(PeseeApresReUsinage mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass.ID = (Guid)mDataReader["ID"];

                    mClass.Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass.Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["ProductionID"]))
                    {
                        mClass.OrdreProduction = new OrdreProduction();
                        mClass.OrdreProduction.ID = (Guid)mDataReader["ProductionID"];
                        mClass.OrdreProduction.NumeroProduction = (string)mDataReader["NumeroProduction"];
                    }

                    mClass.Lot = new Lot();
                    if (!DBNull.Value.Equals(mDataReader["LotID"])) mClass.Lot.ID = (Guid)mDataReader["LotID"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroLot"])) mClass.Lot.NumeroLot = (string)mDataReader["NumeroLot"];

                    if (!DBNull.Value.Equals(mDataReader["LotReusineID"]))
                    {
                        mClass.LotReusine = new Lot();
                        mClass.LotReusine.ID = (Guid)mDataReader["LotReusineID"];
                        mClass.LotReusine.NumeroLot = (string)mDataReader["NumeroLotReusine"];
                    }

                    mClass.LotType = new LotType();
                    if (!DBNull.Value.Equals(mDataReader["TypeLotID"])) mClass.LotType.ID = (int)mDataReader["TypeLotID"];
                    if (!DBNull.Value.Equals(mDataReader["TypeLotDesignation"])) mClass.LotType.Designation = (string)mDataReader["TypeLotDesignation"];

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass.NumeroPesee = (int)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DatePesee"])) mClass.DatePeseeProduction = (DateTime)mDataReader["DatePesee"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass.Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass.NombreSacs = (int)mDataReader["NombreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass.PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass.TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass.TarePalette = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass.PoidsNet = (decimal)mDataReader["PoidsNet"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass.Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["EstManuel"])) mClass.IsManual = (bool)mDataReader["EstManuel"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass.Statut = (string)mDataReader["Statut"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n PeseeApresReUsinage:MapFromDataReader");
            }
        }
    }

    public partial class PeseeApresReUsinageViewModel
    {
        public PeseeApresReUsinage _PeseeApresReUsinage { get; set; }

        public string _DefaultCampagne { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }

}
