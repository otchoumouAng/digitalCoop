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
    public class AnalyseChimiqueEchantillon : DataPersist
    {
        #region fields
        private Guid _ID;
        private Campagne _Campagne;
        private Exportateur _Exportateur;
        private DateTime _DateEchantillon;
        private string _NumeroEchantillon;
        private string _NumeroLots;
        private bool _Desactive;        
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

        public string CampagneAsString
        {
            get
            {
                return _Campagne != null ? _Campagne.Designation : string.Empty;
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

        public DateTime DateEchantillon
        {
            get
            {
                return _DateEchantillon;
            }

            set
            {
                _DateEchantillon = value;
            }
        }

        public string DateEchantillonAsString
        {
            get
            {
                return _DateEchantillon != null ? _DateEchantillon.ToString() : string.Empty;
            }            
        }

        public string NumeroEchantillon
        {
            get
            {
                return _NumeroEchantillon;
            }

            set
            {
                _NumeroEchantillon = value;
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

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desactive)
                    return 0; // BulletCross                                 
                else
                    return 2; //                     
            }
        }

        public string NumeroLots
        {
            get
            {
                return _NumeroLots;
            }

            set
            {
                _NumeroLots = value;
            }
        }        
        #endregion

        #region Constructor
        public AnalyseChimiqueEchantillon()
        {

        }

        public AnalyseChimiqueEchantillon(Guid  myId)
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
            DataCommand mCommande = db().CreateStoredProcCommand("V2_AnalyseChimiqueEchantillon_DeActivate");
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
                }
            }
            catch (Exception ex)
            {
                bolResult = false;
                throw new Exception(ex.Message + "\r\n" + "AnalyseChimiqueEchantillon:fnDeActivate");
            }
            return bolResult;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_AnalyseChimiqueEchantillon_Get", (Guid)Id);
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

        public List<DataPersist> fnGetByCode(string Code)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_AnalyseChimiqueEchantillon_GetByCode");
                db().AddInParameter(mCommande, "@Code", SqlDbType.VarChar, 9, Code);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    AnalyseChimiqueEchantillon mClass = new AnalyseChimiqueEchantillon();
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

        public override List<DataPersist> fnSelect()
        {
            return fnSelect("-1", -1, null, null,0);
        }

        public List<DataPersist> fnSelect(string campagneID, int ExportateurID, DateTime? StartDate, DateTime? EndDate, int statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_AnalyseChimiqueEchantillon_Select");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.Char,9, campagneID);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);                
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@DateFin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@statut", SqlDbType.Int, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    AnalyseChimiqueEchantillon mClass = new AnalyseChimiqueEchantillon();
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

        public bool fnUpdate(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {
                    mCommande = db().CreateStoredProcCommand("V2_AnalyseChimiqueEchantillon_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);                    
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_AnalyseChimiqueEchantillon_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, _Exportateur.ID);
                db().AddInParameter(mCommande, "@DateEchantillon", SqlDbType.DateTime, _DateEchantillon);
                db().AddInParameter(mCommande, "@NumeroEchantillon", SqlDbType.VarChar, _NumeroEchantillon);               

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
                throw new Exception(ex.Message + "\r\n" + "AnalyseChimiqueEchantillon:fnUpdate");

            }
            return Result;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        private static void MapFromDataReader(AnalyseChimiqueEchantillon mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass._Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["ExportateurID"]))
                    {
                        mClass._Exportateur = new Exportateur();
                        mClass._Exportateur.ID = (int)mDataReader["ExportateurID"];
                        mClass._Exportateur.Nom = (string)mDataReader["ExportateurNom"];
                    }                                       

                    if (!DBNull.Value.Equals(mDataReader["DateEchantillon"])) mClass._DateEchantillon = (DateTime)mDataReader["DateEchantillon"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroEchantillon"])) mClass._NumeroEchantillon = (string)mDataReader["NumeroEchantillon"];                    
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroLots"])) mClass._NumeroLots = (string)mDataReader["NumeroLots"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nAnalyseChimiqueEchantillon:MapFromDataReader");
            }
        }

        #endregion
    }

    public partial class AnalyseChimiqueEchantillonViewModel
    {
        public AnalyseChimiqueEchantillon _AnalyseChimiqueEchantillon { get; set; }       
        
        public string _DefaultCampagne { get; set; }
        public int _DefaultExportateur { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
