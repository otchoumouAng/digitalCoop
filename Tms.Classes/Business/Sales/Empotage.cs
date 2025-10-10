using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared.Sales;
using Tms.Components.Data;

namespace Tms.Classes.Business.Sales
{
    public class Empotage : DataPersist
    {
        #region "Field"
        private Guid _ID;
        private Embarquement _Embarquement;
        private StationEmpotage _StationEmpotage;
        private DateTime _Date;
        private string _Commentaire;
        private bool _Desactive;
        private string _Statut;


        //calcule
        private int _NbreConteneurs;
        private int _NbreLots;

        private string _NumeroContrat;
        #endregion

        #region "Properties"
        [ModelField(IDProperty = true, SortType = Ext.Net.SortTypeMethod.None, SortDir = Ext.Net.SortDirection.ASC)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Embarquement Embarquement
        {
            get { return _Embarquement; }
            set { _Embarquement = value; }
        }

        public string EmbarquementAsString
        {
            get
            {
                if (_Embarquement != null)
                    return _Embarquement.Numero;
                else
                    return string.Empty;
            }

        }

        public StationEmpotage StationEmpotage
        {
            get { return _StationEmpotage; }
            set { _StationEmpotage = value; }
        }

        public string StationEmpotageAsString
        {
            get
            {
                if (_StationEmpotage != null)
                    return _StationEmpotage.Nom;
                else
                    return string.Empty;
            }

        }

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public string Commnetaire
        {
            get { return _Commentaire; }
            set { _Commentaire = value; }
        }

        public int NbreConteneurs
        {
            get { return _NbreConteneurs; }
            set { _NbreConteneurs = value; }
        }

        public string NbreConteneursAsString
        {
            get { return _NbreConteneurs != 0 ? String.Format("{0:#,#}", _NbreConteneurs).TrimStart() : string.Empty; }
        }

        public int NbreLots
        {
            get { return _NbreLots; }
            set { _NbreLots = value; }
        }

        public string NbreLotsAsString
        {
            get { return _NbreLots != 0 ? String.Format("{0:#,#}", _NbreLots).TrimStart() : string.Empty; }
        }

       
       
        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
        }

        public string Statut
        {
            get { return _Statut; }
            set { _Statut = value; }
        }

        public bool IsStatut
        {
            get { return _Statut == "NL"; }
        }

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

        public string NumeroContrat
        {
            get
            {
                return _NumeroContrat;
            }

            set
            {
                _NumeroContrat = value;
            }
        }
        #endregion

        #region Constructor
        public Empotage()
        {

        }

        public Empotage(Guid myId)
        {
            this.fnGet(myId);
        }

        #endregion
        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Empotage_Activate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@EmbarquementID", SqlDbType.UniqueIdentifier, _Embarquement.ID);
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
                throw new Exception(ex.Message + "\r\n" + "V2_Empotage:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Empotage_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "V2_Empotage:fnDeActivate");
            }
            return Result;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_Empotage_Get", (Guid)Id);
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
            return fnSelect("{Tous}", -1, null, null, -1);
        }

        public List<DataPersist> fnSelect(string Campagne, int ExportateurID, DateTime? StartDate, DateTime? EndDate, int IsDisabled)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Empotage_Select");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@IsDisabled", SqlDbType.Int, IsDisabled);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Empotage mClass = new Empotage();
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

        public List<DataPersist> fnSelectForWeighing(string Campagne, int ExportateurID, DateTime? StartDate, DateTime? EndDate, int IsDisabled = 0)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Empotage_Select");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@IsDisabled", SqlDbType.Int, IsDisabled);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Empotage mClass = new Empotage();
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

        public List<DataPersist> fnSelectForCorrectStuffing(string Campagne, int ExportateurID, DateTime? StartDate, DateTime? EndDate, int IsDisabled = 0)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Empotage_SelectForStuffingBack");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@IsDisabled", SqlDbType.Int, IsDisabled);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Empotage mClass = new Empotage();
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

                    mCommande = db().CreateStoredProcCommand("V2_Empotage_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_Empotage_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@EmbarquementID", SqlDbType.UniqueIdentifier, _Embarquement.ID);
                db().AddInParameter(mCommande, "@StationEmpotageID", SqlDbType.Int, _StationEmpotage.ID);

                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _Date);

                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);



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
                throw new Exception(ex.Message + "\r\n" + "V2_Empotage:fnUpdate");

            }
            return Result;
        }

        public  bool fnUpdate(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {

                    mCommande = db().CreateStoredProcCommand("V2_Empotage_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_Empotage_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@EmbarquementID", SqlDbType.UniqueIdentifier, _Embarquement.ID);
                db().AddInParameter(mCommande, "@StationEmpotageID", SqlDbType.Int, _StationEmpotage.ID);

                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _Date);

                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);

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
                        break;
                }
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + "\r\n" + "V2_Empotage:fnUpdate");

            }
            return Result;
        }

        #region "Private Members"
        public override string ToString()
        {
            throw new NotImplementedException();
        }
        private static void MapFromDataReader(Empotage mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["EmbarquementID"]))
                    {
                        mClass._Embarquement = new Embarquement();
                        mClass._Embarquement.ID = (Guid)mDataReader["EmbarquementID"];
                        mClass._Embarquement.Numero = (string)mDataReader["EmbarquementNumero"];
                        mClass._Embarquement.Quantite = (decimal)mDataReader["EmbarquementQuantite"];
                        mClass._Embarquement.NbreConteneur = (int)mDataReader["EmbarquementNbreConteneur"];
                       
                    }

                    if (!DBNull.Value.Equals(mDataReader["StationEmpotageID"]))
                    {
                        mClass._StationEmpotage = new StationEmpotage();
                        mClass._StationEmpotage.ID = (int)mDataReader["StationEmpotageID"];
                        mClass._StationEmpotage.Nom = (string)mDataReader["StationEmpotageNom"];
                        
                    }




                    if (!DBNull.Value.Equals(mDataReader["DateEm"])) mClass._Date = (DateTime)mDataReader["DateEm"];


                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];
                    
                    
                    if (!DBNull.Value.Equals(mDataReader["NbreConteneurs"])) mClass._NbreConteneurs = (int)mDataReader["NbreConteneurs"];
                    if (!DBNull.Value.Equals(mDataReader["NbreLots"])) mClass._NbreLots = (int)mDataReader["NbreLots"];
                   
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];

                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];


                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];

                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroContrat"])) mClass._NumeroContrat = (string)mDataReader["NumeroContrat"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nV2_Empotage:MapFromDataReader");
            }
        }
        #endregion
    }

    public partial class EmpotageViewModel
    {
        public Empotage _Empotage { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
