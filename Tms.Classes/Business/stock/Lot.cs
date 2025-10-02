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
    public class Lot : DataPersist
    {
        #region fields
        private Guid _ID;
        private Campagne _Campagne;
        private Exportateur _Exportateur;
        private OrdreProduction _Production;
        private LotType _LotType;
        private Certification _Certification;
        private Embarquement _Embarquement;
        private DateTime _DateLot;
        private string _NumeroLot;
        private string _Statut;
        private int _NombreSacs;
        private decimal _PoidsBrut;
        private decimal _TareSacs;
        private decimal _TarePalette;
        private decimal _PoidsNet;
        private bool _EstQueue;
        private bool _EstManuel;
        private bool _EstReusine;
        private bool _Desactive;

        private decimal _PoidsBrutN;
        private decimal _PoidsNetN;
        private bool _EstFictif;
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

        public OrdreProduction Production
        {
            get
            {
                return _Production;
            }

            set
            {
                _Production = value;
            }
        }

        public string NumeroProduction
        {
            get
            {
                return _Production != null ? _Production.NumeroProduction : string.Empty;
            }            
        }

        public Embarquement Embarquement
        {
            get
            {
                return _Embarquement;
            }

            set
            {
                _Embarquement = value;
            }
        }
        public string EmbarquementAsString
        {
            get { return _Embarquement != null ? _Embarquement.Numero : string.Empty; }

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
            get
            {
                return _LotType != null ? _LotType.Designation : string.Empty;
            }
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

        public string CertificationAsString
        {
            get
            {
                return _Certification != null ? _Certification.Designation : string.Empty;
            }
        }

        public DateTime DateLot
        {
            get
            {
                return _DateLot;
            }

            set
            {
                _DateLot = value;
            }
        }

        public string DateLotAsString
        {
            get
            {
                return _DateLot != null ? _DateLot.ToShortDateString() : string.Empty;
            }
        }

        public string NumeroLot
        {
            get
            {
                return _NumeroLot;
            }

            set
            {
                _NumeroLot = value;
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
            get { return _NombreSacs != 0 ? String.Format("{0:#,#}", _NombreSacs).TrimStart() : string.Empty; }
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
            get { return _PoidsBrut != 0 ? String.Format("{0:#,#}", _PoidsBrut).TrimStart() : string.Empty; }
        }

        public decimal PoidsBrutN
        {
            get
            {
                return _PoidsBrutN;
            }

            set
            {
                _PoidsBrutN = value;
            }
        }

        public string PoidsBrutNAsString
        {
            get { return _PoidsBrutN != 0 ? String.Format("{0:#,#}", _PoidsBrutN).TrimStart() : string.Empty; }
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
            get { return _TareSacs != 0 ? String.Format("{0:#,#}", _TareSacs).TrimStart() : string.Empty; }
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
            get { return _TarePalette != 0 ? String.Format("{0:#,#}", _TarePalette).TrimStart() : string.Empty; }
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
            get { return _PoidsNet != 0 ? String.Format("{0:#,#}", _PoidsNet).TrimStart() : string.Empty; }
        }

        public decimal PoidsNetN
        {
            get
            {
                return _PoidsNetN;
            }

            set
            {
                _PoidsNetN = value;
            }
        }

        public string PoidsNetNAsString
        {
            get { return _PoidsNetN != 0 ? String.Format("{0:#,#}", _PoidsNetN).TrimStart() : string.Empty; }
        }

        public bool EstQueue
        {
            get
            {
                return _EstQueue;
            }

            set
            {
                _EstQueue = value;
            }
        }

        public bool EstManuel
        {
            get
            {
                return _EstManuel;
            }

            set
            {
                _EstManuel = value;
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

        public int mIcon
        {
            get
            {
                if (_Desactive || _Statut == "CA")
                    return 0; // BulletCross                
                else
                    return 2; //                     
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

        public bool EstReusine
        {
            get
            {
                return _EstReusine;
            }

            set
            {
                _EstReusine = value;
            }
        }

        public bool EstFictif
        {
            get
            {
                return _EstFictif;
            }

            set
            {
                _EstFictif = value;
            }
        }
        #endregion

        #region Constructor
        public Lot()
        {

        }

        public Lot(Guid myId)
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
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_DeActivate");
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
                        _Desactive = true;
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
                throw new Exception(ex.Message + "\r\n" + "Lot:fnDeActivate");
            }
            return bolResult;
        }

        public bool fnCancelForCleaning(DataTransaction mTran)
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_CancelForCleaning");
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
                        bolResult = true;
                        _Desactive = true;
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
                throw new Exception(ex.Message + "\r\n" + "Lot:fnCancelForCleaning");
            }
            return bolResult;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_Lot_Get", (Guid)Id);
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

        public bool fnGetByNumero(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_Lot_GetByNumero", (string)Id);
                if (mDataReader.Read())
                {
                    MapFromDataReader(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetByNumero");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public bool fnGetReusinageByNumero(object Numero)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_Lot_GetLotReusineByNumero", (string)Numero);
                if (mDataReader.Read())
                {
                    MapFromDataReaderReuisinage(this, mDataReader);
                    return true;
                }else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetReusinageByNumero");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }


        public bool fnGetLite(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_Lot_GetLite", (Guid)Id);
                if (mDataReader.Read())
                {
                    MapFromDataReaderLite2(this, mDataReader);
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
            return fnSelect("-1", -1, -1, -1, null, null,"-1");
        }

        public List<DataPersist> fnSelect(string mCampagne, int ExportateurID, int CertificationID, int TypeLotID, DateTime? StartDate, DateTime? EndDate, string statut = "-1")
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_Select");
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, mCampagne);
                db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, Certification);
                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int, TypeLotID);                
                db().AddInParameter(mCommande, "@dateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@dateFin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@statut", SqlDbType.Char,2, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Lot mClass = new Lot();
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

        public List<DataPersist> fnSelectForAnalysis(string mCampagne, int ExportateurID, int CertificationID, int TypeLotID, DateTime? StartDate, DateTime? EndDate)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_SelectForAnalysis");
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, mCampagne);
                db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, Certification);
                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int, TypeLotID);
                db().AddInParameter(mCommande, "@dateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@dateFin", SqlDbType.DateTime, EndDate);
                //db().AddInParameter(mCommande, "@statut", SqlDbType.Int, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Lot mClass = new Lot();
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

        public List<DataPersist> fnSelectForChemicalAnalysis(string mCampagne, int ExportateurID, int CertificationID, int TypeLotID, DateTime? StartDate, DateTime? EndDate)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_SelectForChemicalAnalysis");
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, mCampagne);
                db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, Certification);
                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int, TypeLotID);
                db().AddInParameter(mCommande, "@dateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@dateFin", SqlDbType.DateTime, EndDate);
                //db().AddInParameter(mCommande, "@statut", SqlDbType.Int, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Lot mClass = new Lot();
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


        public List<DataPersist> fnSelectLotForShipment(string Campagne, int ExportateurID, int CertificationID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_SelectLotForShipment");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, CertificationID);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Lot mClass = new Lot();
                    MapFromDataReaderLite(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectLotForShipment");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectLotForShipmentAll(string Campagne, int ExportateurID, object EmbarquementID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_SelectLotForShipmentAll");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@EmbarquementID", SqlDbType.UniqueIdentifier, EmbarquementID);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Lot mClass = new Lot();
                    MapFromDataReaderLite(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectLotForShipmentAll");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectByProduction(Guid productionID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_SelectByProduction");
                db().AddInParameter(mCommande, "@productionID", SqlDbType.UniqueIdentifier, productionID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Lot mClass = new Lot();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectByProduction");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectByEmpotage(Guid empotageID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_SelectByEmpotage");
                db().AddInParameter(mCommande, "@empotageID", SqlDbType.UniqueIdentifier, empotageID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Lot mClass = new Lot();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectByEmpotage");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }



        public List<DataPersist> fnSelectByNumero(string Numero)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_SelectByNumero");
                db().AddInParameter(mCommande, "@Numero", SqlDbType.Char,8, Numero);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Lot mClass = new Lot();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectByNumero");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectForReCleaning(string Numero)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_SelectForReCleaning");
                db().AddInParameter(mCommande, "@Numero", SqlDbType.Char, 8, Numero);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Lot mClass = new Lot();

                    MapFromDataReaderReuisinage(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectForReCleaning");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }


        public List<DataPersist> fnSelectForStuffingAjustement(string Numero)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_SelectForStuffingAjustement");
                db().AddInParameter(mCommande, "@Numero", SqlDbType.Char, 8, Numero);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Lot mClass = new Lot();

                    MapFromDataReaderAjustement(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectForReCleaning");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectReCleaningForWeighing(string mCampagne, int ExportateurID, int CertificationID, int TypeLotID, DateTime? StartDate, DateTime? EndDate)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_SelectReCleaningForWeighing");
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char, 9, mCampagne);
                db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, Certification);
                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int, TypeLotID);
                db().AddInParameter(mCommande, "@dateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@dateFin", SqlDbType.DateTime, EndDate);
                //db().AddInParameter(mCommande, "@statut", SqlDbType.Int, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Lot mClass = new Lot();
                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectReCleaningForWeighing");
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
                    mCommande = db().CreateStoredProcCommand("V2_Lot_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddParameter(mCommande, "@NumeroLot", SqlDbType.Char, 9, _NumeroLot, ParameterDirection.InputOutput);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_Lot_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@NumeroLot", SqlDbType.Char, 8, _NumeroLot);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char,9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, _Exportateur.ID);

                if (_Production != null)
                    db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, _Production.ID);
                else
                    db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, DBNull.Value);

                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int, _LotType.ID);

                if (_Certification != null)
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, _Certification.ID);
                else
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, DBNull.Value);
                
                db().AddInParameter(mCommande, "@DateLot", SqlDbType.DateTime, _DateLot);
                
                //db().AddInParameter(mCommande, "@DateProduction", SqlDbType.DateTime, _Production.DateProduction);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, _TareSacs);
                db().AddInParameter(mCommande, "@TarePalettes", SqlDbType.Decimal, _TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNet);
                db().AddInParameter(mCommande, "@EstQueue", SqlDbType.Bit, _EstQueue);
                db().AddInParameter(mCommande, "@EstManuel", SqlDbType.Bit, _EstManuel);
                db().AddInParameter(mCommande, "@EstReusine", SqlDbType.Bit, _EstReusine);

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
                        if (_isnew)
                            _NumeroLot = (string)db().Parameters(mCommande, "@NumeroLot");
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
                throw new Exception(ex.Message + "\r\n" + "Lot:fnUpdate");

            }
            return Result;
        }

        public bool fnUpdateFictif()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {
                    mCommande = db().CreateStoredProcCommand("V4_LotFictif_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddParameter(mCommande, "@NumeroLot", SqlDbType.Char, 9, _NumeroLot, ParameterDirection.InputOutput);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V4_LotFictif_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@NumeroLot", SqlDbType.Char, 8, _NumeroLot);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, _Exportateur.ID);

                if (_Production != null)
                    db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, _Production.ID);
                else
                    db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, DBNull.Value);

                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int, _LotType.ID);

                if (_Certification != null)
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, _Certification.ID);
                else
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@DateLot", SqlDbType.DateTime, _DateLot);

                //db().AddInParameter(mCommande, "@DateProduction", SqlDbType.DateTime, _Production.DateProduction);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, _TareSacs);
                db().AddInParameter(mCommande, "@TarePalettes", SqlDbType.Decimal, _TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNet);
                db().AddInParameter(mCommande, "@EstQueue", SqlDbType.Bit, _EstQueue);
                db().AddInParameter(mCommande, "@EstManuel", SqlDbType.Bit, _EstManuel);
                db().AddInParameter(mCommande, "@EstReusine", SqlDbType.Bit, _EstReusine);

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
                        if (_isnew)
                            _NumeroLot = (string)db().Parameters(mCommande, "@NumeroLot");
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
                throw new Exception(ex.Message + "\r\n" + "Lot:fnUpdate");

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
                    mCommande = db().CreateStoredProcCommand("V2_Lot_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    //db().AddOutParameter(mCommande, "@NumeroLot", SqlDbType.Char, 8);
                    db().AddParameter(mCommande, "@NumeroLot", SqlDbType.Char, 9, _NumeroLot, ParameterDirection.InputOutput);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_Lot_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@NumeroLot", SqlDbType.Char,9, _NumeroLot);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, _Exportateur.ID);
                if (_Production != null)
                    db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, _Production.ID);
                else
                    db().AddInParameter(mCommande, "@ProductionID", SqlDbType.UniqueIdentifier, DBNull.Value);
                db().AddInParameter(mCommande, "@TypeLotID", SqlDbType.Int, _LotType.ID);
                if (_Certification != null)
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, _Certification.ID);
                else
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@DateLot", SqlDbType.DateTime, _DateLot);

                //db().AddInParameter(mCommande, "@DateProduction", SqlDbType.DateTime, _Production.DateProduction);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, _TareSacs);
                db().AddInParameter(mCommande, "@TarePalettes", SqlDbType.Decimal, _TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNet);
                db().AddInParameter(mCommande, "@EstQueue", SqlDbType.Bit, _EstQueue);
                db().AddInParameter(mCommande, "@EstManuel", SqlDbType.Bit, _EstManuel);
                db().AddInParameter(mCommande, "@EstReusine", SqlDbType.Bit, _EstReusine);

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
                        if (_isnew)
                            _NumeroLot = (string)db().Parameters(mCommande, "@NumeroLot");
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
                throw new Exception(ex.Message + "\r\n" + "Lot:fnUpdate");

            }
            return Result;
        }
        public List<DataPersist> fnSelectLotForContainer(Guid EmbarquementID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_SelectForContainer");
                db().AddInParameter(mCommande, "@EmbarquementID", SqlDbType.UniqueIdentifier, EmbarquementID);

                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Lot mClass = new Lot();
                    MapFromDataReaderLite(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectLotForShipment");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }
        public bool fnUpdateShipment(DataTransaction mTran)
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Lot_ModifyShipment");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@EmbarquementID", SqlDbType.UniqueIdentifier, _Embarquement.ID);
            db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
            db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
            db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
            db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
            try
            {
                db().ExecuteNonQuery(ref mCommande, mTran);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        bolResult = true;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        break;
                    default:
                        bolResult = false;
                        string ErrorMessage = (string)db().Parameters(mCommande, "@ErrorMessage");
                        throw new Exception(ErrorMessage);
                        break;
                }
            }
            catch (Exception ex)
            {
                bolResult = false;
                throw new Exception(ex.Message + "\r\n" + "V2_Lot:fnUpdateShipment");
            }
            return bolResult;
        }

        public override string ToString()
        {
            return _NumeroLot;
        }


        private static void MapFromDataReader(Lot mClass, IDataReader mDataReader)
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

                    if (!DBNull.Value.Equals(mDataReader["ProductionID"]))
                    {
                        mClass._Production = new OrdreProduction();
                        mClass._Production.ID = (Guid)mDataReader["ProductionID"];
                        mClass._Production.NumeroProduction = (string)mDataReader["NumeroProduction"];
                        //mClass._Production.DateProduction = (DateTime)mDataReader["DateProduction"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["TypeLotID"]))
                    {
                        mClass._LotType = new LotType();
                        mClass._LotType.ID = (int)mDataReader["TypeLotID"];
                        mClass._LotType.Designation = (string)mDataReader["TypeLotDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["CertificationID"]))
                    {
                        mClass._Certification = new Certification();
                        mClass._Certification.ID = (int)mDataReader["CertificationID"];
                        mClass._Certification.Designation = (string)mDataReader["CertificationDesignation"];
                    }                    

                    if (!DBNull.Value.Equals(mDataReader["DateLot"])) mClass._DateLot = (DateTime)mDataReader["DateLot"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroLot"])) mClass._NumeroLot = (string)mDataReader["NumeroLot"];

                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass._NombreSacs = (int)mDataReader["NombreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass._TarePalette = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNet = (decimal)mDataReader["PoidsNet"];
                    if (!DBNull.Value.Equals(mDataReader["EstQueue"])) mClass._EstQueue = (bool)mDataReader["EstQueue"];
                    if (!DBNull.Value.Equals(mDataReader["EstManuel"])) mClass._EstManuel = (bool)mDataReader["EstManuel"];
                    if (!DBNull.Value.Equals(mDataReader["EstReusine"])) mClass._EstReusine = (bool)mDataReader["EstReusine"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];
                    if (!DBNull.Value.Equals(mDataReader["EstFictif"])) mClass._EstFictif = (bool)mDataReader["EstFictif"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nLot:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderReuisinage(Lot mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroLot"])) mClass._NumeroLot = (string)mDataReader["NumeroLot"];
                    if (!DBNull.Value.Equals(mDataReader["EstReusine"])) mClass._EstReusine = (bool)mDataReader["EstReusine"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nV2_Lot:MapFromDataReaderReuisinage");
            }
        }

        private static void MapFromDataReaderAjustement(Lot mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroLot"])) mClass._NumeroLot = (string)mDataReader["NumeroLot"];                    
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nV2_Lot:MapFromDataReaderReuisinage");
            }
        }
        #endregion
        #region "Private Members"
        private static void MapFromDataReaderLite(Lot mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;
                    mClass.Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Campagne"])) mClass._Campagne.Designation = (string)mDataReader["Campagne"];

                    if (!DBNull.Value.Equals(mDataReader["ExportateurID"]))
                    {
                        mClass._Exportateur = new Exportateur();
                        mClass._Exportateur.ID = (int)mDataReader["ExportateurID"];
                        mClass._Exportateur.Nom = (string)mDataReader["ExportateurNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._NumeroLot = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass._NombreSacs = (int)mDataReader["NombreSacs"];
                    
                    if (!DBNull.Value.Equals(mDataReader["StandardPoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["StandardPoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["StandardPoidsNet"])) mClass._PoidsNet = (decimal)mDataReader["StandardPoidsNet"];

                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrutN = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNetN = (decimal)mDataReader["PoidsNet"];

                    if (!DBNull.Value.Equals(mDataReader["TypeLotID"]))
                    {
                        mClass._LotType = new LotType();
                        mClass._LotType.ID = (int)mDataReader["TypeLotID"];
                        mClass._LotType.Designation = (string)mDataReader["TypeLotDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];


                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];
                    if (!DBNull.Value.Equals(mDataReader["EstFictif"])) mClass._EstFictif = (bool)mDataReader["EstFictif"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nV2_Lot:MapFromDataReaderLite");
            }
        }

        private static void MapFromDataReaderLite2(Lot mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nV2_Lot:MapFromDataReaderLite2");
            }
        }
        #endregion
    }

    public partial class LotViewModel
    {
        public Lot _Lot { get; set; }

        public string _DefaultCampagne { get; set; }
        public int _DefaultExportateur { get; set; }

        public decimal _PoidsStdBrutUnitaire { get; set; }
        public decimal _PoidsStdNetUnitaire { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
