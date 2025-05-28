using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Components.Data;

namespace Tms.Classes.Shared
{
    public class PrelevementMode : DataPersist
    {
        #region "Fields"

        private int _ID;
        private string _Designation;
        private bool _Desative;


        #endregion

        #region "Properties"
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
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

        #endregion

        #region Constructor
        public PrelevementMode()
        {

        }

        public PrelevementMode(int myId)
        {
            this.fnGet(myId);
        }

        #endregion

        #region Methods
        public override bool fnGet(object Id)
        {
            throw new NotImplementedException();
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
                DataCommand mCommande = db().CreateStoredProcCommand("PrelevementMode_Select");
                db().AddInParameter(mCommande, "@Status", SqlDbType.SmallInt, mStatus);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PrelevementMode mClass = new PrelevementMode();

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

        public override bool fnActivate()
        {
            throw new NotImplementedException();
        }

        public override bool fnDeActivate()
        {
            throw new NotImplementedException();
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

        private static void MapFromDataReader(PrelevementMode mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (int)mDataReader["ID"];
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
                throw new Exception(ex.Message + "\nTms_PrelevementMode:MapFromDataReader");
            }
        }
        #endregion
    }
}
