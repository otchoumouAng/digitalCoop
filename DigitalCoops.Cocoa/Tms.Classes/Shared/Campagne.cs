using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Components.Data;
using Ext.Net.MVC;

namespace Tms.Classes.Shared
{

    //[Proxy(Read = "~/Campagne/LoadAll")]
    //[JsonReader(RootProperty = "data")]
    public class Campagne : DataPersist
    {
        #region "Fields"

        private string _Designation;
        private DateTime? _DateDebut;
        private DateTime? _DateFin;
        private bool _Desative;

        #endregion

        #region "Properties"

        [ModelField(IDProperty = true, SortType = Ext.Net.SortTypeMethod.AsText, SortDir = Ext.Net.SortDirection.ASC)]
        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }

        public DateTime? DateDebut
        {
            get { return _DateDebut; }
            set { _DateDebut = value; }
        }

        public string DateDebutAsString
        {
            get { return _DateDebut != null ? DateTime.Parse(_DateDebut.ToString()).ToShortDateString() : string.Empty; }            
        }

        public string DateFinAsString
        {
            get { return _DateFin != null ? DateTime.Parse(_DateFin.ToString()).ToShortDateString() : string.Empty; }
        }

        public DateTime? DateFin
        {
            get { return _DateFin; }
            set { _DateFin = value; }
        }

        public bool Desactive
        {
            get { return _Desative; }
            set { _Desative = value; }
        }

        [Column(Ignore = true)]
        public string AsString
        {
            get { return _Designation; }
        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desative)
                    return 0; // BulletCross                
                else
                    return 2; //                     
            }
        }
        #endregion

        #region constructor
        public Campagne()
        {

        }

        public Campagne(string myId)
        {
            this.fnGet(myId);
        }

        #endregion

        #region methods
        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("Campagne_Get", (string)Id);
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
            return fnSelect(-1);
        }

        public List<DataPersist> fnSelect(int mStatus)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Campagne_Select");
                db().AddInParameter(mCommande, "@Status", SqlDbType.SmallInt, mStatus);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Campagne mClass = new Campagne();

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

        public List<DataPersist> fnSelect(int mStatus, string campagne)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Campagne_Select");
                db().AddInParameter(mCommande, "@Status", SqlDbType.SmallInt, mStatus);
                db().AddInParameter(mCommande, "@camapgne", SqlDbType.VarChar, campagne);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Campagne mClass = new Campagne();

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

                    mCommande = db().CreateStoredProcCommand("Campagne_New");

                    db().AddParameter(mCommande, "@Designation", SqlDbType.VarChar, 0, _Designation, ParameterDirection.InputOutput);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("Campagne_Modify");
                    db().AddInParameter(mCommande, "@Designation", SqlDbType.VarChar, _Designation);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, _DateDebut);
                db().AddInParameter(mCommande, "@DateFin", SqlDbType.DateTime, _DateFin);                                

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
                        _Designation = (string)db().Parameters(mCommande, "@Designation");

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
                throw new Exception(ex.Message + "\r\n" + "Campagne:fnUpdate");

            }
            return Result;
        }


        public override bool fnActivate()
        {

            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("Campagne_Activate");
            db().AddInParameter(mCommande, "@Designation", SqlDbType.VarChar, _Designation);
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
                throw new Exception(ex.Message + "\r\n" + "Campagne:fnActivate");
            }
            return Result;

        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("Campagne_DeActivate");
            db().AddInParameter(mCommande, "@Designation", SqlDbType.VarChar, _Designation);
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
                throw new Exception(ex.Message + "\r\n" + "Campagne:fnDeActivate");
            }
            return Result;
        }

        #endregion

        #region "Static Member"
        public static string SortPropertyName = "Designation";
        public static string IdPropertyName = "ID";

        public static string DisplayProperty = "Designation";
        public static string valueProperty = "ID";
        #endregion
             
        #region "Private Members"

        public override string ToString()
        {
            return _Designation;
        }

        private static void MapFromDataReader(Campagne mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["DateDebut"])) mClass._DateDebut = (DateTime)mDataReader["DateDebut"];
                    if (!DBNull.Value.Equals(mDataReader["DateFin"])) mClass._DateFin = (DateTime)mDataReader["DateFin"];
                    if (!DBNull.Value.Equals(mDataReader["Designation"])) mClass._Designation = (string)mDataReader["Designation"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desative = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nTms_Campagne:MapFromDataReader");
            }
        }
        #endregion

    }

    public partial class CampagneViewModel
    {
        public Campagne _Campagne { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
