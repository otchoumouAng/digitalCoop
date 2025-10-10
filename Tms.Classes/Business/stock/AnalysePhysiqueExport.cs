using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Components.Data;

namespace Tms.Classes.Business.stock
{
    public class AnalysePhysiqueExport : DataPersist
    {
        #region Fields
        private Guid _ID;
        private Laboratoire _Laboratoire;
        private Campagne _Campagne;
        private string _FicheNumero;
        private Analyseur _Analyseur;
        private Analyseur _Verificateur;
        private Lot _Lot;
        private DateTime _DateAnalyse;        
        private int _PoidsFeves;
        private int _NombreFeves;
        private int _Grainage;
        private int _MoisieNbre;
        private int _PlateNbre;
        private int _WeevilNbre;
        private int _GermeeNbre;
        private int _ArdoiseeNbre;
        private int _VioletteNbre;
        private double _NombreFevesPc;
        private double _Moisie;
        private double _Plate;
        private double _Weevil;
        private double _Germee;
        private double _Ardoisee;
        private double _Violette;
        private double _Defectueuse;
        private double _Fermentation;
        private double _Tamis;
        private double _Fragment;
        private double _Brisure;
        private double _Humidite;
        private double _MatiereEtrangere;
        private double? _Ffa;
        private double? _BeansCluster;
        private double? _LightCrop;
        private double? _CRM;
        private ClassificationFeves _ClassificationFeves;
        private string _Statut;
        private bool _Desactive;
        private string _Commentaire;
        private string _Approbateur;
        private bool _Confirmation;
        private DateTime _DateApprobation;
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

        public string FicheNumero
        {
            get
            {
                return _FicheNumero;
            }

            set
            {
                _FicheNumero = value;
            }
        }

        public Analyseur Analyseur
        {
            get
            {
                return _Analyseur;
            }

            set
            {
                _Analyseur = value;
            }
        }

        public Analyseur Verificateur
        {
            get
            {
                return _Verificateur;
            }

            set
            {
                _Verificateur = value;
            }
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

        public DateTime DateAnalyse
        {
            get
            {
                return _DateAnalyse;
            }

            set
            {
                _DateAnalyse = value;
            }
        }

        public int PoidsFeves
        {
            get
            {
                return _PoidsFeves;
            }

            set
            {
                _PoidsFeves = value;
            }
        }

        public int NombreFeves
        {
            get
            {
                return _NombreFeves;
            }

            set
            {
                _NombreFeves = value;
            }
        }

        public int Grainage
        {
            get
            {
                return _Grainage;
            }

            set
            {
                _Grainage = value;
            }
        }

        public int MoisieNbre
        {
            get
            {
                return _MoisieNbre;
            }

            set
            {
                _MoisieNbre = value;
            }
        }

        public int PlateNbre
        {
            get
            {
                return _PlateNbre;
            }

            set
            {
                _PlateNbre = value;
            }
        }

        public int WeevilNbre
        {
            get
            {
                return _WeevilNbre;
            }

            set
            {
                _WeevilNbre = value;
            }
        }

        public int GermeeNbre
        {
            get
            {
                return _GermeeNbre;
            }

            set
            {
                _GermeeNbre = value;
            }
        }

        public int ArdoiseeNbre
        {
            get
            {
                return _ArdoiseeNbre;
            }

            set
            {
                _ArdoiseeNbre = value;
            }
        }

        public int VioletteNbre
        {
            get
            {
                return _VioletteNbre;
            }

            set
            {
                _VioletteNbre = value;
            }
        }

        public double NombreFevesPc
        {
            get
            {
                return _NombreFevesPc;
            }

            set
            {
                _NombreFevesPc = value;
            }
        }

        public double Moisie
        {
            get
            {
                return _Moisie;
            }

            set
            {
                _Moisie = value;
            }
        }

        public double Plate
        {
            get
            {
                return _Plate;
            }

            set
            {
                _Plate = value;
            }
        }

        public double Weevil
        {
            get
            {
                return _Weevil;
            }

            set
            {
                _Weevil = value;
            }
        }

        public double Germee
        {
            get
            {
                return _Germee;
            }

            set
            {
                _Germee = value;
            }
        }

        public double Ardoisee
        {
            get
            {
                return _Ardoisee;
            }

            set
            {
                _Ardoisee = value;
            }
        }

        public double Violette
        {
            get
            {
                return _Violette;
            }

            set
            {
                _Violette = value;
            }
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

        public double Fermentation
        {
            get
            {
                return _Fermentation;
            }

            set
            {
                _Fermentation = value;
            }
        }

        public double Tamis
        {
            get
            {
                return _Tamis;
            }

            set
            {
                _Tamis = value;
            }
        }

        public double Fragment
        {
            get
            {
                return _Fragment;
            }

            set
            {
                _Fragment = value;
            }
        }

        public double Brisure
        {
            get
            {
                return _Brisure;
            }

            set
            {
                _Brisure = value;
            }
        }

        public double Humidite
        {
            get
            {
                return _Humidite;
            }

            set
            {
                _Humidite = value;
            }
        }

        public double MatiereEtrangere
        {
            get
            {
                return _MatiereEtrangere;
            }

            set
            {
                _MatiereEtrangere = value;
            }
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

        public double? BeansCluster
        {
            get
            {
                return _BeansCluster;
            }

            set
            {
                _BeansCluster = value;
            }
        }

        public double? LightCrop
        {
            get
            {
                return _LightCrop;
            }

            set
            {
                _LightCrop = value;
            }
        }

        public double? CRM
        {
            get
            {
                return _CRM;
            }

            set
            {
                _CRM = value;
            }
        }

        public ClassificationFeves ClassificationFeves
        {
            get
            {
                return _ClassificationFeves;
            }

            set
            {
                _ClassificationFeves = value;
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

        public string Approbateur
        {
            get
            {
                return _Approbateur;
            }

            set
            {
                _Approbateur = value;
            }
        }

        public bool Confirmation
        {
            get
            {
                return _Confirmation;
            }

            set
            {
                _Confirmation = value;
            }
        }

        public string DateAnalyseAsString
        {
            get { return _DateAnalyse.ToString(); }
        }

        public string NumeroLot
        {
            get { return _Lot != null ? _Lot.NumeroLot : string.Empty ; }
        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desactive)
                    return 0; // BulletCross
                else if (_Confirmation && _Statut == "AC")
                    return 1;
                else if ((_Confirmation) && (_Statut == "RE"))
                    return 4;
                else if (_Statut == "RE")
                    return 3;
                else
                    return 2; // Tick                
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
        #endregion

        #region Constructor
        public AnalysePhysiqueExport()
        {

        }

        public AnalysePhysiqueExport(Guid  myId)
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
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_AnalysePhysiqueExport_DeActivate");
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
                        Desactive = true;
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
                throw new Exception(ex.Message + "\r\n" + "AnalysePhysiqueExport:fnDeActivate");
            }
            return Result;
        }

        public bool fnDeActivate(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_AnalysePhysiqueExport_DeActivate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
            db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
            db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
            db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
            try
            {
                db().ExecuteNonQuery(ref mCommande, mTran);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        Result = true;
                        Desactive = true;
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
                throw new Exception(ex.Message + "\r\n" + "AnalysePhysiqueExport:fnDeActivate");
            }
            return Result;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_AnalysePhysiqueExport_Get", (Guid)Id);
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

        public bool fnGetByLot(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_AnalysePhysiqueExport_GetByLot", (Guid)Id);
                if (mDataReader.Read())
                {
                    MapFromDataReader(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetByLot");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public override List<DataPersist> fnSelect()
        {
            return fnSelect(null, null, -1);
        }

        public List<DataPersist> fnSelect(DateTime? startdate, DateTime? enddate, int statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_AnalysePhysiqueExport_Select");
                db().AddInParameter(mCommande, "@begindate", SqlDbType.DateTime, startdate);
                db().AddInParameter(mCommande, "@enddate", SqlDbType.DateTime, enddate);
                db().AddInParameter(mCommande, "@status", SqlDbType.SmallInt, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    AnalysePhysiqueExport mClass = new AnalysePhysiqueExport();

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

                    mCommande = db().CreateStoredProcCommand("V2_AnalysePhysiqueExport_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_AnalysePhysiqueExport_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@LaboratoireId", SqlDbType.Int, _Laboratoire.ID);
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char,9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@FicheNumero", SqlDbType.VarChar, _FicheNumero);
                if (_Verificateur != null)
                    db().AddInParameter(mCommande, "@AnalyseurID", SqlDbType.Int, _Analyseur.ID);
                else
                    db().AddInParameter(mCommande, "@AnalyseurID", SqlDbType.Int, DBNull.Value);

                if (_Verificateur != null)
                    db().AddInParameter(mCommande, "@VerificateurID", SqlDbType.Int, _Verificateur.ID);
                else
                    db().AddInParameter(mCommande, "@VerificateurID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, _Lot.ID);
                
                db().AddInParameter(mCommande, "@DateAnalyse", SqlDbType.DateTime, _DateAnalyse);
                db().AddInParameter(mCommande, "@NombreFeves", SqlDbType.Int, _NombreFeves);
                db().AddInParameter(mCommande, "@BeanCount", SqlDbType.Int, _Grainage);
                db().AddInParameter(mCommande, "@PoidsFeves", SqlDbType.Int, _PoidsFeves);
                db().AddInParameter(mCommande, "@Moisie", SqlDbType.Int, _MoisieNbre);
                db().AddInParameter(mCommande, "@MouldyPc", SqlDbType.Float, _Moisie);
                db().AddInParameter(mCommande, "@Flat", SqlDbType.Int, _PlateNbre);
                db().AddInParameter(mCommande, "@FlatPc", SqlDbType.Float, _Plate);
                db().AddInParameter(mCommande, "@Weevil", SqlDbType.Int, _WeevilNbre);
                db().AddInParameter(mCommande, "@WeevilPc", SqlDbType.Float, _Weevil);
                db().AddInParameter(mCommande, "@Germinated", SqlDbType.Int, _GermeeNbre);
                db().AddInParameter(mCommande, "@GerminatedPc", SqlDbType.Float, _Germee);
                db().AddInParameter(mCommande, "@DefectivesPc", SqlDbType.Float, _Defectueuse);
                db().AddInParameter(mCommande, "@Slaty", SqlDbType.Int, _ArdoiseeNbre);
                db().AddInParameter(mCommande, "@SlatyPc", SqlDbType.Float, _Ardoisee);
                db().AddInParameter(mCommande, "@NonFermentedPc", SqlDbType.Float, _Fermentation);
                db().AddInParameter(mCommande, "@Violet", SqlDbType.Int, _VioletteNbre);
                db().AddInParameter(mCommande, "@VioletPc", SqlDbType.Float, _Violette);
                db().AddInParameter(mCommande, "@SievingPc", SqlDbType.Float, _Tamis);
                db().AddInParameter(mCommande, "@FragmentPc", SqlDbType.Float, _Fragment);
                db().AddInParameter(mCommande, "@BrokenBeansPc", SqlDbType.Float, _Brisure);
                db().AddInParameter(mCommande, "@MoisturePc", SqlDbType.Float, _Humidite);
                db().AddInParameter(mCommande, "@ForeignMatter", SqlDbType.Float, _MatiereEtrangere);
                db().AddInParameter(mCommande, "@FFA", SqlDbType.Float, _Ffa);
                db().AddInParameter(mCommande, "@BeansCluster", SqlDbType.Float, _BeansCluster);
                db().AddInParameter(mCommande, "@LightCropPc", SqlDbType.Float, _LightCrop);
                db().AddInParameter(mCommande, "@CRM", SqlDbType.Float, _CRM);                
                db().AddInParameter(mCommande, "@ClassificationFevesID", SqlDbType.Int, _ClassificationFeves.ID);


                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);
                db().AddInParameter(mCommande, "@ResultatAnalyse", SqlDbType.Char, _Statut);

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
                throw new Exception(ex.Message + "\r\n" + "AnalysePhysiqueExport:fnUpdate");

            }
            return Result;
        }

        public bool fnApprove()
        {

            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_AnalysePhysiqueExport_Approve");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@statut", SqlDbType.VarChar, _Statut);            
            db().AddInParameter(mCommande, "@ApproveUser", SqlDbType.VarChar, _Approbateur);
            db().AddInParameter(mCommande, "@ApproveDate", SqlDbType.DateTime, DateApprobation);
            db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);
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
                throw new Exception(ex.Message + "\r\n" + "AnalysePhysiqueExport:fnApprove");
            }
            return Result;

        }


        public override string ToString()
        {
            return _FicheNumero;
        }

        private static void MapFromDataReader(AnalysePhysiqueExport mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["NumeroFiche"])) mClass._FicheNumero = (string)mDataReader["NumeroFiche"];
                    if (!DBNull.Value.Equals(mDataReader["DateAnalyseExport"])) mClass._DateAnalyse = (DateTime)mDataReader["DateAnalyseExport"];
                    if (!DBNull.Value.Equals(mDataReader["NombreFeves"])) mClass._NombreFeves = (int)mDataReader["NombreFeves"];
                    if (!DBNull.Value.Equals(mDataReader["BeanCount"])) mClass._Grainage = (int)mDataReader["BeanCount"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsFeves"])) mClass._PoidsFeves = (int)mDataReader["PoidsFeves"];
                    if (!DBNull.Value.Equals(mDataReader["Moisie"])) mClass._MoisieNbre = (int)mDataReader["Moisie"];
                    if (!DBNull.Value.Equals(mDataReader["MouldyPc"])) mClass._Moisie = (double)mDataReader["MouldyPc"];
                    if (!DBNull.Value.Equals(mDataReader["Flat"])) mClass._PlateNbre = (int)mDataReader["Flat"];
                    if (!DBNull.Value.Equals(mDataReader["FlatPc"])) mClass._Plate = (double)mDataReader["FlatPc"];
                    if (!DBNull.Value.Equals(mDataReader["Weevil"])) mClass._WeevilNbre = (int)mDataReader["Weevil"];
                    if (!DBNull.Value.Equals(mDataReader["WeevilPc"])) mClass._Weevil = (double)mDataReader["WeevilPc"];
                    if (!DBNull.Value.Equals(mDataReader["Germinated"])) mClass._GermeeNbre = (int)mDataReader["Germinated"];
                    if (!DBNull.Value.Equals(mDataReader["GerminatedPc"])) mClass._Germee = (double)mDataReader["GerminatedPc"];
                    if (!DBNull.Value.Equals(mDataReader["Slaty"])) mClass._ArdoiseeNbre = (int)mDataReader["Slaty"];
                    if (!DBNull.Value.Equals(mDataReader["SlatyPc"])) mClass._Ardoisee = (double)mDataReader["SlatyPc"];
                    if (!DBNull.Value.Equals(mDataReader["DefectivesPc"])) mClass._Defectueuse = (double)mDataReader["DefectivesPc"];
                    if (!DBNull.Value.Equals(mDataReader["NonFermentedPc"])) mClass._Fermentation = (double)mDataReader["NonFermentedPc"];
                    if (!DBNull.Value.Equals(mDataReader["Violet"])) mClass._VioletteNbre = (int)mDataReader["Violet"];
                    if (!DBNull.Value.Equals(mDataReader["VioletPc"])) mClass._Violette = (double)mDataReader["VioletPc"];
                    if (!DBNull.Value.Equals(mDataReader["SievingPc"])) mClass._Tamis = (double)mDataReader["SievingPc"];
                    if (!DBNull.Value.Equals(mDataReader["FragmentPc"])) mClass._Fragment = (double)mDataReader["FragmentPc"];
                    if (!DBNull.Value.Equals(mDataReader["BrokenBeansPc"])) mClass._Brisure = (double)mDataReader["BrokenBeansPc"];
                    if (!DBNull.Value.Equals(mDataReader["MoisturePc"])) mClass._Humidite = (double)mDataReader["MoisturePc"];
                    if (!DBNull.Value.Equals(mDataReader["ForeignMatter"])) mClass._MatiereEtrangere = (double)mDataReader["ForeignMatter"];
                    if (!DBNull.Value.Equals(mDataReader["FFA"])) mClass._Ffa = (double)mDataReader["FFA"];
                    if (!DBNull.Value.Equals(mDataReader["BeansCluster"])) mClass._BeansCluster = (double)mDataReader["BeansCluster"];
                    if (!DBNull.Value.Equals(mDataReader["LightCropPc"])) mClass._LightCrop = (double)mDataReader["LightCropPc"];
                    if (!DBNull.Value.Equals(mDataReader["CRM"])) mClass._CRM = (double)mDataReader["CRM"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["Confirmation"])) mClass._Confirmation = (bool)mDataReader["Confirmation"];

                    mClass._Lot = new Lot();
                    if (!DBNull.Value.Equals(mDataReader["LotID"])) mClass._Lot.ID = (Guid)mDataReader["LotID"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroLot"])) mClass._Lot.NumeroLot = (string)mDataReader["NumeroLot"];                    

                    mClass._Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass._Campagne.Designation = (string)mDataReader["CampagneID"];


                    mClass._Laboratoire = new Laboratoire();
                    if (!DBNull.Value.Equals(mDataReader["LaboratoireID"])) mClass._Laboratoire.ID = (int)mDataReader["LaboratoireID"];
                    if (!DBNull.Value.Equals(mDataReader["LaboratoireNom"])) mClass._Laboratoire.Designation = (string)mDataReader["LaboratoireNom"];

                    mClass._ClassificationFeves = new ClassificationFeves();
                    if (!DBNull.Value.Equals(mDataReader["ClassificationFevesID"])) mClass._ClassificationFeves.ID = (int)mDataReader["ClassificationFevesID"];
                    if (!DBNull.Value.Equals(mDataReader["ClassificationDesignation"])) mClass._ClassificationFeves.Designation = (string)mDataReader["ClassificationDesignation"];

                    mClass._Analyseur = new Analyseur();
                    if (!DBNull.Value.Equals(mDataReader["AnalyseurID"])) mClass._Analyseur.ID = (int)mDataReader["AnalyseurID"];
                    if (!DBNull.Value.Equals(mDataReader["AnalyseurNom"])) mClass._Analyseur.Nom = (string)mDataReader["AnalyseurNom"];
                    //if (!DBNull.Value.Equals(mDataReader["AnalyseurPeutApprouver"])) mClass._Analyseur.PeutApprouver = (bool)mDataReader["AnalyseurPeutApprouver"];

                    if (!DBNull.Value.Equals(mDataReader["Approbateur"])) mClass._Approbateur = (string)mDataReader["Approbateur"];
                    if (!DBNull.Value.Equals(mDataReader["DateApprobation"])) mClass.DateApprobation = (DateTime)mDataReader["DateApprobation"];                    

                    mClass._Verificateur = new Analyseur();
                    if (!DBNull.Value.Equals(mDataReader["VerificateurID"])) mClass._Verificateur.ID = (int)mDataReader["VerificateurID"];
                    if (!DBNull.Value.Equals(mDataReader["VerificateurNom"])) mClass._Verificateur.Nom = (string)mDataReader["VerificateurNom"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nAnalysePhysiqueExport:MapFromDataReader");
            }
        }

        #endregion
    }

    public partial class AnalysePhysiqueExportViewModel
    {
        public AnalysePhysiqueExport _AnalysePhysiqueExport { get; set; }

        public string _DefaultCampagne { get; set; }
        public int _DefaultLaboratoire { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
