using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Components.Data;


namespace Tms.Classes.Shared
{
    public class AnalysePhysiqueNorme : DataPersist
    {
        #region "Fields"
        private double _StandardHumidite;
        private double _StandardMatiereEtrangere;
        private double _StandardBrisure;
       
        #endregion

        #region Properties
        
        public double StandardHumidite
        {
            get { return _StandardHumidite; }
            set { _StandardHumidite = value; }
        }

        public double StandardBrisure
        {
            get { return _StandardBrisure; }
            set { _StandardBrisure = value; }
        }

        public double StandardMatiereEtrangere
        {
            get { return _StandardMatiereEtrangere; }
            set { _StandardMatiereEtrangere = value; }
        }       

        #endregion

        #region Methods
        public AnalysePhysiqueNorme()
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
                mDataReader = db().ExecuteReader("AnalysePhysiqueNorme_Select");
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
                DataCommand mCommande = db().CreateStoredProcCommand("AnalysePhysiqueNorme_Select");
                mDataReader = db().ExecuteReader(mCommande);

                if (mDataReader.Read())
                {
                    AnalysePhysiqueNorme mClass = new AnalysePhysiqueNorme();

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
            throw new NotImplementedException();
        }

        #endregion

        #region "Private Members"       

        private static void MapFromDataReader(AnalysePhysiqueNorme mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;
                  
                    if (!DBNull.Value.Equals(mDataReader["StandardHumidite"])) mClass._StandardHumidite = (double)mDataReader["StandardHumidite"];
                    if (!DBNull.Value.Equals(mDataReader["StandardMatiereEtrangere"])) mClass._StandardMatiereEtrangere = (double)mDataReader["StandardMatiereEtrangere"];
                    if (!DBNull.Value.Equals(mDataReader["StandardBrisure"])) mClass._StandardBrisure = (double)mDataReader["StandardBrisure"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nAnalysePhysiqueNorme:MapFromDataReader");
            }
        }
        #endregion

    }
}
