using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Components.Data;

namespace Tms.Classes.Shared
{
    public class DashBoard : DataPersist
    {
        #region Fields
        private string _Designation;
        private decimal _Annonced;
        private decimal _InProcess;
        private decimal _Accepted;
        private decimal _Rejected;
        #endregion

        #region Properties
        public string Designation
        {
            get { return _Designation;}
            set { _Designation = value;}
        }

        public decimal Annonced
        {
            get { return _Annonced; }
            set { _Annonced = value; }
        }

        public decimal InProcess
        {
            get { return _InProcess; }
            set { _InProcess = value; }
        }
        public string InProcessAsString
        {
            get
            {
                if (Designation == "Quantity (T)")
                    return _InProcess > 0 ? string.Format("{0:#,#.000}", _InProcess).TrimStart() : string.Empty;
                else
                    return _InProcess > 0 ? string.Format("{0:#,#}", _InProcess).TrimStart() : string.Empty;
            }
        }
        public decimal Accepted
        {
            get { return _Accepted; }
            set { _Accepted = value; }
        }

        public string AcceptedAsString
        {
            get
            {
                if (Designation == "Quantity (T)")
                    return _Accepted > 0 ? string.Format("{0:#,#.000}", _Accepted).TrimStart() : string.Empty;
                else
                    return _Accepted > 0 ? string.Format("{0:#,#}", _Accepted).TrimStart() : string.Empty;
            }
        }

        public decimal Rejected
        {
            get { return _Rejected;  }
            set { _Rejected = value; }
        }

        public string RejectedAsString
        {
            get
            {
                if (Designation == "Quantity (T)")
                    return _Rejected > 0 ? string.Format("{0:#,#.000}", _Rejected).TrimStart() : string.Empty;
                else
                    return _Rejected > 0 ? string.Format("{0:#,#}", _Rejected).TrimStart() : string.Empty;
            }
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
            return fnSelect(string.Empty,DateTime.Now, DateTime.Now);
        }

        public virtual List<DataPersist> fnSelect(string Campagne, DateTime? datedebut, DateTime? datefin)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("DashBoard_VolumeToday");
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, Campagne);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, datedebut);
                db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, datefin);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    DashBoard mClass = new DashBoard();

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
        public virtual List<DataPersist> fnSelect(int exportateurId,int FournisseurId, int SiteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("DashBoard_VolumeToday");
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, Campagne);
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, datedebut);
                //db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, datefin);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    DashBoard mClass = new DashBoard();

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
        public virtual List<DataPersist> fnSelect(int FournisseurId)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("DashBoard_VolumeToday");
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, Campagne);
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, datedebut);
                //db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, datefin);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    DashBoard mClass = new DashBoard();

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

        public virtual List<DataPersist> fnSelect(int FournisseurId, int siteID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("DashBoard_VolumeToday");
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, Campagne);
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, datedebut);
                //db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, datefin);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    DashBoard mClass = new DashBoard();

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


        public virtual List<DataPersist> fnSelectEvolution(int siteID = 1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("DashBoard_VolumeToday");
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, datedebut);
                //db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, datefin);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    DashBoard mClass = new DashBoard();

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

        public virtual List<DataPersist> fnSelectTopBadDebit(int siteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("DashBoard_FinancingSelectTopBadDebit");
                //db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, camapgne);
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, datedebut);
                //db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, datefin);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.DateTime, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FinancingCropYearToDate mClass = new FinancingCropYearToDate();

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

        public virtual List<DataPersist> fnSelectProgress(string Campagne,DateTime? datedebut, DateTime? datefin)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("DashBoard_VolumeCropYearToDateProgress");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, Campagne);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, datedebut);
                db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, datefin);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    DashBoard mClass = new DashBoard();

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

        public virtual List<DataPersist> fnSelectProgress(int EXportateurId, int FournisseurId, int siteID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("DashBoard_VolumeCropYearToDateProgress");
                //db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, Campagne);
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, datedebut);
                //db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, datefin);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    DashBoard mClass = new DashBoard();

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


        public virtual List<DataPersist> fnSelectPerAgencies()
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("DashBoard_VolumeCropYearToDatePerformers");
                //db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, campagne);
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, datedebut);
                //db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, datefin);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    DashBoard mClass = new DashBoard();

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

        public virtual List<DataPersist> fnSelectPerDirectSupplier(int SiteID = 1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("DashBoard_VolumeCropYearToDatePerformers");
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, SiteID);
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, datedebut);
                //db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, datefin);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    DashBoard mClass = new DashBoard();

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

        private static void MapFromDataReader(DashBoard mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["Designation"])) mClass._Designation = (string)mDataReader["Designation"];
                    if (!DBNull.Value.Equals(mDataReader["Annonced"])) mClass._Annonced = (decimal)mDataReader["Annonced"];
                    if (!DBNull.Value.Equals(mDataReader["InProcess"])) mClass._InProcess = (decimal)mDataReader["InProcess"];
                    if (!DBNull.Value.Equals(mDataReader["Accepted"])) mClass._Accepted = (decimal)mDataReader["Accepted"];
                    if (!DBNull.Value.Equals(mDataReader["Rejected"])) mClass._Rejected = (decimal)mDataReader["Rejected"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n DashBoard:MapFromDataReaderVolumeToday");
            }
        }
        #endregion

    }

    public class VolumToday : DashBoard
    {
        //public override List<DataPersist> fnSelect()
        //{
        //    return fnSelect(DateTime.Now, DateTime.Now);
        //}

        public override List<DataPersist> fnSelect(string Campagne,DateTime? datedebut, DateTime? datefin)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("DashBoard_VolumeToday");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, Campagne);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, datedebut);
                db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, datefin);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    VolumToday mClass = new VolumToday();

                    MapFromDataReaderVolumeToday(mClass, mDataReader);
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

        private static void MapFromDataReaderVolumeToday(VolumToday mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["Designation"])) mClass.Designation = (string)mDataReader["Designation"];
                    if (!DBNull.Value.Equals(mDataReader["Annonced"])) mClass.Annonced = (int)mDataReader["Annonced"];
                    if (!DBNull.Value.Equals(mDataReader["InProcess"])) mClass.InProcess = (int)mDataReader["InProcess"];
                    if (!DBNull.Value.Equals(mDataReader["Accepted"])) mClass.Accepted = (int)mDataReader["Accepted"];
                    if (!DBNull.Value.Equals(mDataReader["Rejected"])) mClass.Rejected = (int)mDataReader["Rejected"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n DashBoard:MapFromDataReaderVolumeToday");
            }
        }


    }

    public class VolumeCropYearToDate : DashBoard
    {
        decimal _Declared;
        decimal _Conventional;
        decimal _Certified;
        string _RejectedPr;
        decimal _AtGate;
        decimal _TotalStock;

        public decimal Declared
        {
            get
            {
                return _Declared;
            }

            set
            {
                _Declared = value;
            }
        }

        public string DeclaredAsString
        {
            get {
                if (Designation == "Quantity (T)")
                    return _Declared > 0 ? string.Format("{0:#,#.000}", _Declared).TrimStart() : string.Empty;
                else
                    return _Declared > 0 ? string.Format("{0:#,#}", _Declared).TrimStart() : string.Empty;
            }            
        }

        public decimal Conventional
        {
            get
            {
                return _Conventional;
            }

            set
            {
                _Conventional = value;
            }
        }

        public string ConventionalAsString
        {
            get {
                if (Designation == "Quantity (T)")
                    return _Conventional > 0 ? string.Format("{0:#,#.000}", _Conventional).TrimStart() : string.Empty;
                else
                    return _Conventional > 0 ? string.Format("{0:#,#}", _Conventional).TrimStart() : string.Empty;
            }
        }

        public decimal Certified
        {
            get
            {
                return _Certified;
            }

            set
            {
                _Certified = value;
            }
        }

        public string CertifiedAsString
        {
            get {
                if (Designation == "Quantity (T)")
                    return _Certified > 0 ? string.Format("{0:#,#.000}", _Certified).TrimStart() : string.Empty;
                else
                    return _Certified > 0 ? string.Format("{0:#,#}", _Certified).TrimStart() : string.Empty;
            }
        }

        public string RejectedPr
        {
            get
            {
                return _RejectedPr;
            }

            set
            {
                _RejectedPr = value;
            }
        }

        public decimal AtGate
        {
            get
            {
                return _AtGate;
            }

            set
            {
                _AtGate = value;
            }
        }

        public string AtGateAsString
        {
            get {
                if (Designation == "Quantity (T)")
                    return _AtGate > 0 ? string.Format("{0:#,#.000}", _AtGate).TrimStart() : string.Empty;
                else
                    return _AtGate > 0 ? string.Format("{0:#,#}", _AtGate).TrimStart() : string.Empty;
            }
        }

        public decimal TotalStock
        {
            get
            {
                return _TotalStock;
            }

            set
            {
                _TotalStock = value;
            }
        }

        public string TotalStockString
        {
            get {
                if (Designation == "Quantity (T)")
                    return _TotalStock > 0 ? string.Format("{0:#,#.000}", _TotalStock).TrimStart() : string.Empty;
                else
                    return _TotalStock > 0 ? string.Format("{0:#,#}", _TotalStock).TrimStart() : string.Empty;
            }
        }

        //public override List<DataPersist> fnSelect()
        //{
        //    return fnSelect(DateTime.Now, DateTime.Now);
        //}

        public override List<DataPersist> fnSelect(int exportateurId,int fournisseurId, int siteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("DashBoard_Volume");
                //db().AddInParameter(mCommande, "@campagne", SqlDbType.VarChar, campagne);
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, datedebut);
                //db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, datefin);
                db().AddInParameter(mCommande, "@ExportateurId", SqlDbType.Int, exportateurId);
                db().AddInParameter(mCommande, "@FournisseurId", SqlDbType.Int, fournisseurId);
                db().AddInParameter(mCommande, "@SiteId", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    VolumeCropYearToDate mClass = new VolumeCropYearToDate();

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

        public override List<DataPersist> fnSelectProgress(int exportateurId,int fournisseurID, int siteID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("DashBoard_VolumeCropYearToDateProgress");
                //db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, Campagne);
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, datedebut);
                //db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, datefin);
                db().AddInParameter(mCommande, "@ExportateurId", SqlDbType.Int, exportateurId);
                db().AddInParameter(mCommande, "@FournisseurId", SqlDbType.Int, fournisseurID);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    VolumeCropYearToDate mClass = new VolumeCropYearToDate();

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

        public override List<DataPersist> fnSelectPerAgencies()
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("DashBoard_VolumeCropYearToDatePerAgencies");
                //db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, Campagne);
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, datedebut);
                //db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, datefin);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    VolumeCropYearToDate mClass = new VolumeCropYearToDate();

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

        public override List<DataPersist> fnSelectPerDirectSupplier(int SiteID = 1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("DashBoard_VolumeCropYearToDatePerDirectSupplier");
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, SiteID);
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, datedebut);
                //db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, datefin);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    VolumeCropYearToDate mClass = new VolumeCropYearToDate();

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

        private static void MapFromDataReader(VolumeCropYearToDate mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["Designation"])) mClass.Designation = (string)mDataReader["Designation"];
                    if (!DBNull.Value.Equals(mDataReader["AtGate"])) mClass.AtGate = (decimal)mDataReader["AtGate"];
                    if (!DBNull.Value.Equals(mDataReader["InProcess"])) mClass.InProcess = (decimal)mDataReader["InProcess"];
                    if (!DBNull.Value.Equals(mDataReader["TotalStock"])) mClass.TotalStock = (decimal)mDataReader["TotalStock"];                    
                    if (!DBNull.Value.Equals(mDataReader["Accepted"])) mClass.Accepted = (decimal)mDataReader["Accepted"];
                    if (!DBNull.Value.Equals(mDataReader["Conventional"])) mClass.Conventional = (decimal)mDataReader["Conventional"];
                    if (!DBNull.Value.Equals(mDataReader["Certified"])) mClass.Certified = (decimal)mDataReader["Certified"];
                    if (!DBNull.Value.Equals(mDataReader["Rejected"])) mClass.Rejected = (decimal)mDataReader["Rejected"];
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n DashBoard:MapFromDataReader");
            }
        }


    }

    public class FinancingCropYearToDate : DashBoard
    {
        decimal _Allocated;
        decimal _Recovered;
        decimal _Balance;
        decimal? _Saving;
        decimal? _Exposure;

        public decimal Allocated
        {
            get
            {
                return _Allocated;
            }

            set
            {
                _Allocated = value;
            }
        }

        public decimal Recovered
        {
            get
            {
                return _Recovered;
            }

            set
            {
                _Recovered = value;
            }
        }

        public decimal Balance
        {
            get
            {
                return _Balance;
            }

            set
            {
                _Balance = value;
            }
        }

        public decimal? Saving
        {
            get
            {
                return _Saving;
            }

            set
            {
                _Saving = value;
            }
        }

        public string SavingAsString
        {
            get { return String.Format("0:#,#", _Saving).TrimStart(); }            
        }

        public string ExposureAsString
        {
            get { return String.Format("0:#,#", _Exposure).TrimStart(); }
        }
        public decimal? Exposure
        {
            get
            {
                return _Exposure;
            }

            set
            {
                _Exposure = value;
            }
        }

        //public override List<DataPersist> fnSelect()
        //{
        //    return fnSelect(DateTime.Now, DateTime.Now);
        //}

        public override List<DataPersist> fnSelect(int FournisseurId, int siteID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("DashBoard_FinancialStatus");
                //db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, camapgne);
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, datedebut);
                //db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, datefin);
                db().AddInParameter(mCommande, "@FournisseurId", SqlDbType.Int, FournisseurId);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FinancingCropYearToDate mClass = new FinancingCropYearToDate();

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

        public override List<DataPersist> fnSelectEvolution(int siteID = 1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("DashBoard_FinancingEvolution");
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                //db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, camapgne);
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, datedebut);
                //db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, datefin);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FinancingCropYearToDate mClass = new FinancingCropYearToDate();

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

        public override List<DataPersist> fnSelectTopBadDebit(int siteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("DashBoard_FinancingSelectTopBadDebit");
                //db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, camapgne);
                //db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, datedebut);
                //db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, datefin);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FinancingCropYearToDate mClass = new FinancingCropYearToDate();

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



        private static void MapFromDataReader(FinancingCropYearToDate mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["Designation"])) mClass.Designation = (string)mDataReader["Designation"];
                    if (!DBNull.Value.Equals(mDataReader["Allocated"])) mClass.Allocated = (decimal)mDataReader["Allocated"];
                    if (!DBNull.Value.Equals(mDataReader["Recovered"])) mClass.Recovered = (decimal)mDataReader["Recovered"];
                    if (!DBNull.Value.Equals(mDataReader["Balance"])) mClass.Balance = (decimal)mDataReader["Balance"];
                    if (!DBNull.Value.Equals(mDataReader["Saving"])) mClass.Saving = (decimal)mDataReader["Saving"];
                    if (!DBNull.Value.Equals(mDataReader["Exposure"])) mClass.Exposure = (decimal)mDataReader["Exposure"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n DashBoard:MapFromDataReader");
            }
        }


    }


}
