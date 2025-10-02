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
    public class AnalyseChimique : DataPersist
    {
        #region Fields
        private Guid _ID;
        private AnalyseChimiqueEchantillon _Echantillon;
        private Analyseur _Analyseur;
        private Analyseur _Verificateur;
        private string _Approbateur;
        private Laboratoire _Laboratoire;
        private string _FicheNumero;
        private bool _Confirmation;
        private DateTime _DateAnalyse;
        private string _Statut;
        private double _PoidsFeves;
        private double _WeightShell;
        private double _ShellContentPc;
        private double _WeightGroundBeans;
        private double _WeightGroundBeansAndAluminium;
        private double _WeightGroundBeansAluminumClean;
        private double _Moisture;
        private double _PH;
        private double _WeightDryFlaskPumice;
        private double _WeightGroundBeans2;
        private double _WeightFlaskFatClean;
        private double _WeightFat;
        private double _FatContentPc;
        private double _ActualFatContent;
        private double _KOH;
        private double _FFA;
        private bool _Desactive;
        private DateTime _DateApprobation;
        private double _BlankFromPetroleum;

        //private double _PrFc45;
        //private double _PrFc35;
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

        public AnalyseChimiqueEchantillon Echantillon
        {
            get
            {
                return _Echantillon;
            }

            set
            {
                _Echantillon = value;
            }
        }

        public string NumeroEchantillon
        {
            get
            {
                return _Echantillon != null ? _Echantillon.NumeroEchantillon : string.Empty;
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

        public double PoidsFeves
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

        public double WeightShell
        {
            get
            {
                return _WeightShell;
            }

            set
            {
                _WeightShell = value;
            }
        }

        public double ShellContentPc
        {
            get
            {
                return _ShellContentPc;
            }

            set
            {
                _ShellContentPc = value;
            }
        }

        public double WeightGroundBeans
        {
            get
            {
                return _WeightGroundBeans;
            }

            set
            {
                _WeightGroundBeans = value;
            }
        }

        public double WeightGroundBeansAndAluminium
        {
            get
            {
                return _WeightGroundBeansAndAluminium;
            }

            set
            {
                _WeightGroundBeansAndAluminium = value;
            }
        }

        public double WeightGroundBeansAluminumClean
        {
            get
            {
                return _WeightGroundBeansAluminumClean;
            }

            set
            {
                _WeightGroundBeansAluminumClean = value;
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

        public double PH
        {
            get
            {
                return _PH;
            }

            set
            {
                _PH = value;
            }
        }

        public double WeightDryFlaskPumice
        {
            get
            {
                return _WeightDryFlaskPumice;
            }

            set
            {
                _WeightDryFlaskPumice = value;
            }
        }

        public double WeightGroundBeans2
        {
            get
            {
                return _WeightGroundBeans2;
            }

            set
            {
                _WeightGroundBeans2 = value;
            }
        }

        public double WeightFlaskFatClean
        {
            get
            {
                return _WeightFlaskFatClean;
            }

            set
            {
                _WeightFlaskFatClean = value;
            }
        }

        public double WeightFat
        {
            get
            {
                return _WeightFat;
            }

            set
            {
                _WeightFat = value;
            }
        }

        public double FatContentPc
        {
            get
            {
                return _FatContentPc;
            }

            set
            {
                _FatContentPc = value;
            }
        }

        public double ActualFatContent
        {
            get
            {
                return _ActualFatContent;
            }

            set
            {
                _ActualFatContent = value;
            }
        }

        public double KOH
        {
            get
            {
                return _KOH;
            }

            set
            {
                _KOH = value;
            }
        }

        public double FFA
        {
            get
            {
                return _FFA;
            }

            set
            {
                _FFA = value;
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

        public string DateAnalyseAsString
        {
            get { return _DateAnalyse.ToString(); }
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

        public double BlankFromPetroleum
        {
            get
            {
                return _BlankFromPetroleum;
            }

            set
            {
                _BlankFromPetroleum = value;
            }
        }

        public double PrFc45
        {
            get
            {
                return Math.Round( 0 + ((PrFc1 - 0.02) * 0.9646) + 0.09, 2);
            }
        }

        public double PrFc35
        {
            get
            {
                return Math.Round( 0 + ((PrFc1 - 0.02) * 0.9798) + 0.06, 2);
            }
        }

        public double PrFc1
        {
            get
            {
                double resultPrFatContent1 = (((99.1 * _FatContentPc) - (1.98 * _ShellContentPc)) / ((101 - (0.89 * _ShellContentPc) - _Moisture)));
                //PrFc35 = 0 + ((resultPrFatContent1 - 0.02) * 0.9798) + 0.06;
                //PrFc45 = 0 + ((resultPrFatContent1 - 0.02) * 0.9646) + 0.09;
                return Math.Round(resultPrFatContent1, 2);
            }
        }
        #endregion

        #region Constructor
        public AnalyseChimique()
        {

        }

        public AnalyseChimique(Guid myId)
        {
            this.fnGet(myId);
        }
        #endregion

        #region
        public override bool fnActivate()
        {
            throw new NotImplementedException();
        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_AnalyseChimique_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "AnalyseChimique:fnDeActivate");
            }
            return Result;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_AnalyseChimique_Get", (Guid)Id);
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
            return fnSelect(null, null, -1);
        }

        public List<DataPersist> fnSelect(DateTime? startdate, DateTime? enddate, int statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_AnalyseChimique_Select");
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, startdate);
                db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, enddate);
                db().AddInParameter(mCommande, "@status", SqlDbType.SmallInt, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    AnalyseChimique mClass = new AnalyseChimique();

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

                    mCommande = db().CreateStoredProcCommand("V2_AnalyseChimique_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_AnalyseChimique_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@LaboratoireId", SqlDbType.Int, _Laboratoire.ID);                
                db().AddInParameter(mCommande, "@FicheNumero", SqlDbType.VarChar, _FicheNumero);
                if (_Verificateur != null)
                    db().AddInParameter(mCommande, "@AnalyseurID", SqlDbType.Int, _Analyseur.ID);
                else
                    db().AddInParameter(mCommande, "@AnalyseurID", SqlDbType.Int, DBNull.Value);

                if (_Verificateur != null)
                    db().AddInParameter(mCommande, "@VerificateurID", SqlDbType.Int, _Verificateur.ID);
                else
                    db().AddInParameter(mCommande, "@VerificateurID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@EchantillonID", SqlDbType.UniqueIdentifier, _Echantillon.ID);

                db().AddInParameter(mCommande, "@DateAnalyse", SqlDbType.DateTime, _DateAnalyse);
                db().AddInParameter(mCommande, "@PoidsFeves", SqlDbType.Float, _PoidsFeves);
                db().AddInParameter(mCommande, "@WeightShell", SqlDbType.Float, _WeightShell);
                db().AddInParameter(mCommande, "@ShellContentPc", SqlDbType.Float, _ShellContentPc);
                db().AddInParameter(mCommande, "@WeightGroundBeans", SqlDbType.Float, _WeightGroundBeans);
                db().AddInParameter(mCommande, "@WeightGroundBeansAndAluminium", SqlDbType.Float, _WeightGroundBeansAndAluminium);
                db().AddInParameter(mCommande, "@WeightGroundBeansAluminumClean", SqlDbType.Float, _WeightGroundBeansAluminumClean);
                db().AddInParameter(mCommande, "@Moisture", SqlDbType.Float, _Moisture);
                db().AddInParameter(mCommande, "@PH", SqlDbType.Float, _PH);
                db().AddInParameter(mCommande, "@WeightDryFlaskPumice", SqlDbType.Float, _WeightDryFlaskPumice);
                db().AddInParameter(mCommande, "@WeightGroundBeans2", SqlDbType.Float, _WeightGroundBeans2);
                db().AddInParameter(mCommande, "@WeightFlaskFatClean", SqlDbType.Float, _WeightFlaskFatClean);
                db().AddInParameter(mCommande, "@WeightFat", SqlDbType.Float, _WeightFat);
                db().AddInParameter(mCommande, "@FatContentPc", SqlDbType.Float, _FatContentPc);
                db().AddInParameter(mCommande, "@ActualFatContent", SqlDbType.Float, _ActualFatContent);
                db().AddInParameter(mCommande, "@KOH", SqlDbType.Float, _KOH);
                db().AddInParameter(mCommande, "@FFA", SqlDbType.Float, _FFA);
                db().AddInParameter(mCommande, "@BlankFromPetroleum", SqlDbType.Float, _BlankFromPetroleum);

                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, _Statut);

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
                throw new Exception(ex.Message + "\r\n" + "AnalyseChimique:fnUpdate");

            }
            return Result;
        }


        public bool fnApprove()
        {

            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_AnalyseChimique_Approve");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@statut", SqlDbType.VarChar, _Statut);
            db().AddInParameter(mCommande, "@ApproveUser", SqlDbType.VarChar, _Approbateur);
            db().AddInParameter(mCommande, "@ApproveDate", SqlDbType.DateTime, DateApprobation);            
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
                throw new Exception(ex.Message + "\r\n" + "AnalyseChimique:fnApprove");
            }
            return Result;

        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        private static void MapFromDataReader(AnalyseChimique mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["DateApprobation"])) mClass._DateApprobation = (DateTime)mDataReader["DateApprobation"];
                    if (!DBNull.Value.Equals(mDataReader["FicheNumero"])) mClass._FicheNumero = (string)mDataReader["FicheNumero"];
                    if (!DBNull.Value.Equals(mDataReader["Confirmation"])) mClass._Confirmation = (bool)mDataReader["Confirmation"];
                    if (!DBNull.Value.Equals(mDataReader["DateAnalyse"])) mClass._DateAnalyse = (DateTime)mDataReader["DateAnalyse"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsFeves"])) mClass._PoidsFeves = (double)mDataReader["PoidsFeves"];
                    if (!DBNull.Value.Equals(mDataReader["WeightShell"])) mClass._WeightShell = (double)mDataReader["WeightShell"];
                    if (!DBNull.Value.Equals(mDataReader["ShellContentPc"])) mClass._ShellContentPc = (double)mDataReader["ShellContentPc"];
                    if (!DBNull.Value.Equals(mDataReader["WeightGroundBeans"])) mClass._WeightGroundBeans = (double)mDataReader["WeightGroundBeans"];
                    if (!DBNull.Value.Equals(mDataReader["WeightGroundBeansAndAluminium"])) mClass._WeightGroundBeansAndAluminium = (double)mDataReader["WeightGroundBeansAndAluminium"];
                    if (!DBNull.Value.Equals(mDataReader["WeightGroundBeansAluminumClean"])) mClass._WeightGroundBeansAluminumClean = (double)mDataReader["WeightGroundBeansAluminumClean"];
                    if (!DBNull.Value.Equals(mDataReader["Moisture"])) mClass._Moisture = (double)mDataReader["Moisture"];
                    if (!DBNull.Value.Equals(mDataReader["PH"])) mClass._PH = (double)mDataReader["PH"];
                    if (!DBNull.Value.Equals(mDataReader["WeightDryFlaskPumice"])) mClass._WeightDryFlaskPumice = (double)mDataReader["WeightDryFlaskPumice"];
                    if (!DBNull.Value.Equals(mDataReader["WeightGroundBeans2"])) mClass._WeightGroundBeans2 = (double)mDataReader["WeightGroundBeans2"];
                    if (!DBNull.Value.Equals(mDataReader["WeightFlaskFatClean"])) mClass._WeightFlaskFatClean = (double)mDataReader["WeightFlaskFatClean"];
                    if (!DBNull.Value.Equals(mDataReader["WeightFat"])) mClass._WeightFat = (double)mDataReader["WeightFat"];
                    if (!DBNull.Value.Equals(mDataReader["FatContentPc"])) mClass._FatContentPc = (double)mDataReader["FatContentPc"];
                    if (!DBNull.Value.Equals(mDataReader["ActualFatContent"])) mClass._ActualFatContent = (double)mDataReader["ActualFatContent"];
                    if (!DBNull.Value.Equals(mDataReader["KOH"])) mClass._KOH = (double)mDataReader["KOH"];
                    if (!DBNull.Value.Equals(mDataReader["FFA"])) mClass._FFA = (double)mDataReader["FFA"];
                    if (!DBNull.Value.Equals(mDataReader["BlankFromPetroleum"])) mClass._BlankFromPetroleum = (double)mDataReader["BlankFromPetroleum"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];

                    mClass._Echantillon = new AnalyseChimiqueEchantillon();
                    if (!DBNull.Value.Equals(mDataReader["EchantillonID"])) mClass._Echantillon.ID = (Guid)mDataReader["EchantillonID"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroEchantillon"])) mClass._Echantillon.NumeroEchantillon = (string)mDataReader["NumeroEchantillon"];

                    mClass._Laboratoire = new Laboratoire();
                    if (!DBNull.Value.Equals(mDataReader["LaboratoireID"])) mClass._Laboratoire.ID = (int)mDataReader["LaboratoireID"];
                    if (!DBNull.Value.Equals(mDataReader["LaboratoireNom"])) mClass._Laboratoire.Designation = (string)mDataReader["LaboratoireNom"];

                    mClass._Analyseur = new Analyseur();
                    if (!DBNull.Value.Equals(mDataReader["AnalyseurID"])) mClass._Analyseur.ID = (int)mDataReader["AnalyseurID"];
                    if (!DBNull.Value.Equals(mDataReader["AnalyseurNom"])) mClass._Analyseur.Nom = (string)mDataReader["AnalyseurNom"];                    

                    if (!DBNull.Value.Equals(mDataReader["ApprobateurID"])) mClass._Approbateur = (string)mDataReader["ApprobateurID"];                    

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
                throw new Exception(ex.Message + "\nAnalyseChimique:MapFromDataReader");
            }
        }

        #endregion
    }

    public partial class AnalyseChimiqueViewModel
    {
        public AnalyseChimique _AnalyseChimique { get; set; }
        public int _DefaultLaboratoire { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
