using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Components.Data;

namespace Tms.Classes.Business
{
    public class Pesee : DataPersist
    {
        #region "Fields"

        private Guid _ID;
        private Livraison _Livraison;
        private Bascule _Bascule;
        private int _Numero;
        private DateTime _DateP1;
        private decimal _PoidsP1;
        private int _SacsDecharges;
        private int _SacsAcceptes;
        private DateTime? _DateP2;
        private decimal _PoidsP2;
        private decimal _PoidsBrut;
        private decimal _TareEmballages;
        private decimal _TarePalettes;
        private decimal _PoidsNetLivre;
        private string _Statut;             //DF=defaut, RF=Refoulee, AN=Annulee
        private bool _IsManual;

        // new columns added
        private int _SacsBons;
        private int _SacsMauvais;
        private int _SacsNylon;
        private int _SacsOPA;
        private int _NbrePalette;
        private decimal? _PoidsVGM;
        private decimal? _TareConteneur;

        private string _RaisonChangementType;
        private string _RaisonCorrectionPremierePoids;
        private string _RaisonCorrectionDeuxiemePoids;
        private string _RaisonPeseeManuelle;

        public enum StatutPesee { PremierePesee = 1, SecondePesee = 2 };

        #endregion

        #region "Properties"
        [ModelField(IDProperty = true, SortType = Ext.Net.SortTypeMethod.AsInt, SortDir = Ext.Net.SortDirection.ASC)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Livraison Livraison
        {
            get { return _Livraison; }
            set { _Livraison = value; }
        }

        public DateTime DateLivraison
        {
            get
            {
                return _Livraison.DateLivraison; 
            } 
        }

        public string DateLivraisonLongAsString
        {
            get
            {
                return _Livraison.DateLivraison != null ? _Livraison.DateLivraison.ToString("g") : string.Empty;
            }
        }

        public string TransporteurAsString
        {
            get
            {
                return _Livraison != null && _Livraison.Transporteur != null ? _Livraison.Transporteur.Nom : string.Empty;
            }
        }

        public string DeliveryType
        {
            get
            {
                return _Livraison.LivraisonTypeAsString;
            }
        }
        
        public string DeliveryID
        {
            get
            {
                return _Livraison.Numero;
            }
        }

        public string DeliverySupplier
        {
            get
            {
                return _Livraison.FournisseurNameAsString;
            }
        }

        public string DeliveryTruckID
        {
            get
            {
                return _Livraison.Immatriculation;
            }
        }
        
        public int DeliveryEstimatedBags
        {
            get
            {
                return _Livraison.SacsDeclares;
            }
        }

        public decimal DeliveryEstimatedTonnage
        {
            get
            {
                return _Livraison != null ? _Livraison.PoidsDeclare : 0;
            }
        }

        public string DeliveryEstimatedTonnageAsString
        {
            get
            {
                return _Livraison != null ? string.Format("{0:#,#}",  _Livraison.PoidsDeclare).TrimStart() : "0";
            }
        }
        

        public Bascule Bascule
        {
            get { return _Bascule; }
            set { _Bascule = value; }
        }

        public string BasculeAsString
        {
            get { return _Bascule != null ? _Bascule.Designation : string.Empty; }            
        }
        public int Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
                
        public DateTime DateP1
        {
            get { return _DateP1; }
            set { _DateP1 = value; }
        }
                
        public decimal PoidsP1
        {
            get { return _PoidsP1; }
            set { _PoidsP1 = value; }
        }
                
        public int SacsDecharges
        {
            get { return _SacsDecharges; }
            set { _SacsDecharges = value; }
        }
                
        public int SacsAcceptes
        {
            get { return _SacsAcceptes; }
            set { _SacsAcceptes = value; }
        }
                
        public DateTime? DateP2
        {
            get { return _DateP2; }
            set { _DateP2 = value; }
        }
              
        public decimal PoidsP2
        {
            get { return _PoidsP2; }
            set { _PoidsP2 = value; }
        }

        public string PoidsP2AsString
        {
            get { return _PoidsP2 != 0 ? string.Format("{0:#,#}", _PoidsP2).TrimStart() : "0"; }            
        }

        public string PoidsP1AsString
        {
            get { return _PoidsP1 != 0 ? string.Format("{0:#,#}", _PoidsP1).TrimStart() : "0"; }
        }

        public decimal PoidsBrut
        {
            get { return _PoidsBrut; }
            set { _PoidsBrut = value; }
        }

        public string PoidsBrutAsString
        {
            get { return _PoidsBrut != 0 ? string.Format("{0:#,#}", _PoidsBrut).TrimStart() : "0"; }
        }

        public decimal TareEmballages
        {
            get { return _TareEmballages; }
            set { _TareEmballages = value; }
        }
                
        public decimal TarePalettes
        {
            get { return _TarePalettes; }
            set { _TarePalettes = value; }
        }

        public decimal Tare
        {
            get { return _TarePalettes + _TareEmballages; }           
        }
        
        public decimal PoidsNetLivre
        {
            get { return _PoidsNetLivre; }
            set { _PoidsNetLivre = value; }
        }

        public string PoidsNetLivreAsString
        {
            get { return _PoidsNetLivre != 0 ? string.Format("{0:#,#}", _PoidsNetLivre).TrimStart() : "0"; }
        }

        public string Statut
        {
            get { return _Statut; }
            set { _Statut = value; }
        }

        public string RaisonPeseeManuelle
        {
            get { return _RaisonPeseeManuelle; }
            set { _RaisonPeseeManuelle = value; }
        }

        public string LivraisonNumeroLot
        {
            get
            {
                return _Livraison != null ? _Livraison.NumLot : string.Empty;
            }
        }

        public string LivraisonNumConteneur
        {
            get
            {
                return _Livraison != null ? _Livraison.NumConteneur : string.Empty;
            }
        }

        public string LivraisonNumPlomb
        {
            get
            {
                return _Livraison != null  ? _Livraison.NumPlomb : string.Empty;
            }
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("Pesee_Get", (Guid)Id);
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

        public bool fnGetByDeliveryNumber(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("Pesee_GetByDeliveryNumber", (string)Id);
                if (mDataReader.Read())
                {
                    MapFromDataReader(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetByDeliveryNumber");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public override List<DataPersist> fnSelect()
        {
            return fnSelect(-1, "{Tous}", "{Tous}", -1, DateTime.Now.AddDays(-7), DateTime.Now, -1, -1);
        }
        
        public virtual List<DataPersist> fnSelect(int pontBasculeID, string cropYearID, string livraisonID, int fournisseurID, DateTime mStartDate, DateTime mEndDate, short mStatus, int siteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Pesee_Select");

                db().AddInParameter(mCommande, "@PontBasculeID", SqlDbType.Int, pontBasculeID);
                db().AddInParameter(mCommande, "@CropYearID", SqlDbType.VarChar, cropYearID);
                db().AddInParameter(mCommande, "@DeliveryTypeID", SqlDbType.VarChar, livraisonID);
                db().AddInParameter(mCommande, "@Supplier", SqlDbType.VarChar, fournisseurID);
                db().AddInParameter(mCommande, "@StartDate", SqlDbType.DateTime, mStartDate);
                db().AddInParameter(mCommande, "@EndDate", SqlDbType.DateTime, mEndDate);
                db().AddInParameter(mCommande, "@Status", SqlDbType.SmallInt, mStatus);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Pesee mClass = new Pesee();

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
        
        public virtual bool fnFirstWeighing()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("Pesee_FirstWeighing");

                db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddInParameter(mCommande, "@DeliveryID", SqlDbType.UniqueIdentifier, _Livraison.ID);
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, _Livraison.Campagne.Designation);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, _Livraison.Site.ID);
                db().AddInParameter(mCommande, "@LivraisonTypeID", SqlDbType.Int, _Livraison.LivraisonType.ID);
                db().AddInParameter(mCommande, "@PontBasculeID", SqlDbType.Int, _Bascule.ID);
                db().AddOutParameter(mCommande, "@Numero", SqlDbType.Int, 0);
                db().AddInParameter(mCommande, "@DateP1", SqlDbType.DateTime, _DateP1);
                db().AddInParameter(mCommande, "@PoidsP1", SqlDbType.Decimal, _PoidsP1);
                //db().AddInParameter(mCommande, "@SacsDecharges", SqlDbType.Int, _SacsDecharges);
                //db().AddInParameter(mCommande, "@SacsAcceptes", SqlDbType.Int, _SacsAcceptes);
                //db().AddInParameter(mCommande, "@DateP2", SqlDbType.DateTime, _DateP2);
                //db().AddInParameter(mCommande, "@PoidsP2", SqlDbType.Decimal, _PoidsP2);
                //db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                //db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, _TareEmballages);
                //db().AddInParameter(mCommande, "@TarePalettes", SqlDbType.Decimal, _TarePalettes);
                //db().AddInParameter(mCommande, "@PoidsLivre", SqlDbType.Decimal, _PoidsNetLivre);
                db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);

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
                        //base.UpdateAuditFields();
                        Result = true;

                        if (_isnew)
                        {
                            _ID = (Guid)db().Parameters(mCommande, "@ID");
                            _Numero = (int)db().Parameters(mCommande, "@Numero");
                        }

                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
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
                throw new Exception(ex.Message + "\r\n" + "Pesee:fnFirstWeighing");

            }
            return Result;
        }
        
        public virtual bool fnSecondWeighing()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("Pesee_SecondWeighing");

                //db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                //db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, _Livraison.Campagne.Designation);
                //db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, _Livraison.Destination.ID);

                db().AddInParameter(mCommande, "@SacsBons", SqlDbType.Int, _SacsBons);
                db().AddInParameter(mCommande, "@SacsMauvais", SqlDbType.Int, _SacsMauvais);
                db().AddInParameter(mCommande, "@SacsNylon", SqlDbType.Int, _SacsNylon);
                db().AddInParameter(mCommande, "@SacsOPA", SqlDbType.Int, _SacsOPA);
                db().AddInParameter(mCommande, "@NbrePalettes", SqlDbType.Int, _NbrePalette);


                if (_PoidsVGM.HasValue)
                    db().AddInParameter(mCommande, "@PoidsVGM", SqlDbType.Decimal, _PoidsVGM);
                else
                    db().AddInParameter(mCommande, "@PoidsVGM", SqlDbType.Decimal, DBNull.Value);


                if (_TareConteneur.HasValue)
                    db().AddInParameter(mCommande, "@TareConteneur", SqlDbType.Decimal, _TareConteneur);
                else
                    db().AddInParameter(mCommande, "@TareConteneur", SqlDbType.Decimal, DBNull.Value);

                db().AddInParameter(mCommande, "@SacsDecharges", SqlDbType.Int, _SacsDecharges);
                db().AddInParameter(mCommande, "@SacsAcceptes", SqlDbType.Int, _SacsAcceptes);

                db().AddInParameter(mCommande, "@DateP2", SqlDbType.DateTime, _DateP2);
                db().AddInParameter(mCommande, "@PoidsP2", SqlDbType.Decimal, _PoidsP2);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, _TareEmballages);
                db().AddInParameter(mCommande, "@TarePalettes", SqlDbType.Decimal, _TarePalettes);
                db().AddInParameter(mCommande, "@PoidsLivre", SqlDbType.Decimal, _PoidsNetLivre);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

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
                        //base.UpdateAuditFields();
                        Result = true;

                        //if (_isnew)
                        //{
                        //    _ID = (Guid)db().Parameters(mCommande, "@ID");
                        //    _Numero = (int)db().Parameters(mCommande, "@Numero");
                        //}

                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
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
                throw new Exception(ex.Message + "\r\n" + "Pesee:fnSecondWeighing");

            }
            return Result;
        }

        public virtual bool fnUnload()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("Pesee_Unload");

                //db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
               

                db().AddInParameter(mCommande, "@SacsBons", SqlDbType.Int, _SacsBons);
                db().AddInParameter(mCommande, "@SacsMauvais", SqlDbType.Int, _SacsMauvais);
                db().AddInParameter(mCommande, "@SacsNylon", SqlDbType.Int, _SacsNylon);
                db().AddInParameter(mCommande, "@SacsOPA", SqlDbType.Int, _SacsOPA);
                db().AddInParameter(mCommande, "@NbrePalettes", SqlDbType.Int, _NbrePalette);

                //if (_PoidsVGM.HasValue)
                //    db().AddInParameter(mCommande, "@PoidsVGM", SqlDbType.Decimal, _PoidsVGM);
                //else
                //    db().AddInParameter(mCommande, "@PoidsVGM", SqlDbType.Decimal, DBNull.Value);


                //if (_TareConteneur.HasValue)
                //    db().AddInParameter(mCommande, "@TareConteneur", SqlDbType.Decimal, _TareConteneur);
                //else
                //    db().AddInParameter(mCommande, "@TareConteneur", SqlDbType.Decimal, DBNull.Value);

                db().AddInParameter(mCommande, "@SacsDecharges", SqlDbType.Int, _SacsDecharges);
                db().AddInParameter(mCommande, "@SacsAcceptes", SqlDbType.Int, _SacsAcceptes);

                //db().AddInParameter(mCommande, "@DateP2", SqlDbType.DateTime, _DateP2);
                //db().AddInParameter(mCommande, "@PoidsP2", SqlDbType.Decimal, _PoidsP2);
                //db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                //db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, _TareEmballages);
                //db().AddInParameter(mCommande, "@TarePalettes", SqlDbType.Decimal, _TarePalettes);
                //db().AddInParameter(mCommande, "@PoidsLivre", SqlDbType.Decimal, _PoidsNetLivre);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

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
                        //base.UpdateAuditFields();
                        Result = true;

                        //if (_isnew)
                        //{
                        //    _ID = (Guid)db().Parameters(mCommande, "@ID");
                        //    _Numero = (int)db().Parameters(mCommande, "@Numero");
                        //}

                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
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
                throw new Exception(ex.Message + "\r\n" + "Pesee:fnUnload");

            }
            return Result;
        }

        public virtual bool fnChangeDeliveryType()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("Pesee_ChangeDeliveryType");

                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@RaisonChangementTypePesee", SqlDbType.VarChar, _RaisonChangementType);
                //db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, _Livraison.Campagne.Designation);
                //db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, _Livraison.Site.ID);
                db().AddInParameter(mCommande, "@LivraisonTypeID", SqlDbType.Int, _Livraison.LivraisonType.ID); 
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

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
                        //base.UpdateAuditFields();
                        Result = true;

                        if (_isnew)
                        {
                            _ID = (Guid)db().Parameters(mCommande, "@ID");
                            _Numero = (int)db().Parameters(mCommande, "@Numero");
                        }

                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
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
                throw new Exception(ex.Message + "\r\n" + "Pesee:fnFirstWeighing");

            }
            return Result;
        }


        public virtual bool fnIsRejected()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("Pesee_IsRejected");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);

                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                db().ExecuteNonQuery(ref mCommande);

                int result = (int)db().Parameters(mCommande, "ReturnValue");
                Result =  result == 1;
                
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + "\r\n" + "Pesee:fnIsRejected");
            }
            return Result;
        }

        public virtual List<DataPersist> fnSelect(DateTime mStartDate, DateTime mEndDate)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Pesee_Select");

                db().AddInParameter(mCommande, "@StartDate", SqlDbType.DateTime, mStartDate);
                db().AddInParameter(mCommande, "@EndDate", SqlDbType.DateTime, mEndDate);                
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Pesee mClass = new Pesee();

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










        public virtual bool fnCorrectFirstWeight()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("Pesee_CorrectFirstWeighing");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@RaisonChangementPremierePesee", SqlDbType.VarChar, _RaisonCorrectionPremierePoids);
                db().AddInParameter(mCommande, "@PoidsPremierePesee", SqlDbType.Decimal, _PoidsP1);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);              


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
                        //base.UpdateAuditFields();
                        Result = true;

                        //if (_isnew)
                        //{
                        //    _ID = (Guid)db().Parameters(mCommande, "@ID");
                        //    _Numero = (int)db().Parameters(mCommande, "@Numero");
                        //}

                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
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
                throw new Exception(ex.Message + "\r\n" + "Pesee:fnFirstWeighing");

            }
            return Result;
        }
        
        public virtual bool fnCorrectSecondWeight()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("Pesee_CorrectSecondWeighing");

                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);

                db().AddInParameter(mCommande, "@RaisonChangementDeuxiemePesee", SqlDbType.VarChar, _RaisonCorrectionDeuxiemePoids);

                db().AddInParameter(mCommande, "@PoidsPremierePesee", SqlDbType.Decimal, _PoidsP1);

                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, _TareEmballages);
                                
                db().AddInParameter(mCommande, "@TarePalettes", SqlDbType.Decimal, _TarePalettes);
                
                if (_TareConteneur.HasValue)
                    db().AddInParameter(mCommande, "@TareConteneur", SqlDbType.Decimal, _TareConteneur.Value);
                else
                    db().AddInParameter(mCommande, "@TareConteneur", SqlDbType.Decimal, DBNull.Value);
                    
                db().AddInParameter(mCommande, "@PoidsDeuxiemePesee", SqlDbType.Decimal, _PoidsP2);

                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNetLivre);

                if (_PoidsVGM.HasValue)
                    db().AddInParameter(mCommande, "@PoidsVGM", SqlDbType.Decimal, _PoidsVGM.Value);
                else
                    db().AddInParameter(mCommande, "@PoidsVGM", SqlDbType.Decimal, DBNull.Value);


                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

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
                        //base.UpdateAuditFields();
                        Result = true;

                        //if (_isnew)
                        //{
                        //    _ID = (Guid)db().Parameters(mCommande, "@ID");
                        //    _Numero = (int)db().Parameters(mCommande, "@Numero");
                        //}

                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
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
                throw new Exception(ex.Message + "\r\n" + "Pesee:fnFirstWeighing");

            }
            return Result;
        }


       


        public virtual bool fnCancel()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("Pesee_Cancel");
                
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);               
              
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

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
                        //base.UpdateAuditFields();
                        Result = true;

                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
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
                throw new Exception(ex.Message + "\r\n" + "Pesee:fnCancel");

            }
            return Result;
        }
        
        public virtual bool fnManualWeighing()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("Pesee_ManualWeighing");

                db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddInParameter(mCommande, "@DeliveryID", SqlDbType.UniqueIdentifier, _Livraison.ID);
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, _Livraison.Campagne.Designation);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, _Livraison.Site.ID);
                db().AddInParameter(mCommande, "@LivraisonTypeID", SqlDbType.Int, _Livraison.LivraisonType.ID);
                db().AddInParameter(mCommande, "@PontBasculeID", SqlDbType.Int, _Bascule.ID);
                db().AddOutParameter(mCommande, "@Numero", SqlDbType.Int, 0);
                db().AddInParameter(mCommande, "@DateP1", SqlDbType.DateTime, _DateP1);
                db().AddInParameter(mCommande, "@PoidsP1", SqlDbType.Decimal, _PoidsP1);
                db().AddInParameter(mCommande, "@SacsDecharges", SqlDbType.Int, _SacsDecharges);
                db().AddInParameter(mCommande, "@SacsAcceptes", SqlDbType.Int, _SacsAcceptes);
                db().AddInParameter(mCommande, "@DateP2", SqlDbType.DateTime, _DateP2);
                db().AddInParameter(mCommande, "@PoidsP2", SqlDbType.Decimal, _PoidsP2);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, _TareEmballages);
                db().AddInParameter(mCommande, "@TarePalettes", SqlDbType.Decimal, _TarePalettes);
                db().AddInParameter(mCommande, "@PoidsLivre", SqlDbType.Decimal, _PoidsNetLivre);


                // New Columns added
                db().AddInParameter(mCommande, "@SacsBons", SqlDbType.Int, _SacsBons);
                db().AddInParameter(mCommande, "@SacsMauvais", SqlDbType.Int, _SacsMauvais);
                db().AddInParameter(mCommande, "@SacsNylon", SqlDbType.Int, _SacsNylon);
                db().AddInParameter(mCommande, "@SacsOPA", SqlDbType.Int, _SacsOPA);
                db().AddInParameter(mCommande, "@NbrePalettes", SqlDbType.Int, _NbrePalette);

                if (_PoidsVGM.HasValue)
                    db().AddInParameter(mCommande, "@PoidsVGM", SqlDbType.Decimal, _PoidsVGM);
                else
                    db().AddInParameter(mCommande, "@PoidsVGM", SqlDbType.Decimal, DBNull.Value);


                if (_TareConteneur.HasValue)
                    db().AddInParameter(mCommande, "@TareConteneur", SqlDbType.Decimal, _TareConteneur);
                else
                    db().AddInParameter(mCommande, "@TareConteneur", SqlDbType.Decimal, DBNull.Value);


                db().AddInParameter(mCommande, "@RaisonPeseeManuelle", SqlDbType.VarChar, _RaisonPeseeManuelle);

                db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);

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
                        //base.UpdateAuditFields();
                        Result = true;

                        if (_isnew)
                        {
                            _ID = (Guid)db().Parameters(mCommande, "@ID");
                            _Numero = (int)db().Parameters(mCommande, "@Numero");                            
                        }

                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");                       
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
                throw new Exception(ex.Message + "\r\n" + "Pesee:fnUpdate");

            }
            return Result;
        }

        public override bool fnActivate()
        {
            throw new NotImplementedException();
        }

        public override bool fnDeActivate()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        #endregion
                
        public string AsString
        {
            get { return _Numero.ToString(); }
        }

        //DF=defaut, RF=Refoulee, AN=Annulee
        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (Cancelled)
                    return 0; // Annulée
                else if (Rejected)
                    return 4; // Refoulée
                else if (DateP2 > DateTime.Parse("1/1/0001 12:00 AM"))
                    return 2; // 2è pesée
                else if (IsManual)
                    return 3; // saisie manuel                     
                else
                    return 1; // 1ère pesée
            }
        }

        public bool Cancelled { get; set; }

        public bool Rejected { get; set; }

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

        public int SacsBons
        {
            get
            {
                return _SacsBons;
            }

            set
            {
                _SacsBons = value;
            }
        }

        public int SacsMauvais
        {
            get
            {
                return _SacsMauvais;
            }

            set
            {
                _SacsMauvais = value;
            }
        }

        public int SacsNylon
        {
            get
            {
                return _SacsNylon;
            }

            set
            {
                _SacsNylon = value;
            }
        }

        public int SacsOPA
        {
            get
            {
                return _SacsOPA;
            }

            set
            {
                _SacsOPA = value;
            }
        }

        public int NbrePalette
        {
            get
            {
                return _NbrePalette;
            }

            set
            {
                _NbrePalette = value;
            }
        }

        public decimal? PoidsVGM
        {
            get
            {
                return _PoidsVGM;
            }

            set
            {
                _PoidsVGM = value;
            }
        }

        public decimal? TareConteneur
        {
            get
            {
                return _TareConteneur;
            }

            set
            {
                _TareConteneur = value;
            }
        }

        public string RaisonChangementType
        {
            get
            {
                return _RaisonChangementType;
            }

            set
            {
                _RaisonChangementType = value;
            }
        }

        public string RaisonCorrectionPremierePoids
        {
            get
            {
                return _RaisonCorrectionPremierePoids;
            }

            set
            {
                _RaisonCorrectionPremierePoids = value;
            }
        }

        public string RaisonCorrectionDeuxiemePoids
        {
            get
            {
                return _RaisonCorrectionDeuxiemePoids;
            }

            set
            {
                _RaisonCorrectionDeuxiemePoids = value;
            }
        }

        private static void MapFromDataReader(Pesee mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._Livraison = new Livraison();
                    mClass._Livraison.LivraisonType = new LivraisonType();
                    mClass._Livraison.Fournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["LivraisonID"])) mClass._Livraison.ID = (Guid)mDataReader["LivraisonID"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonNumber"])) mClass._Livraison.Numero = (string)mDataReader["LivraisonNumber"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonDate"])) mClass._Livraison.DateLivraison = (DateTime)mDataReader["LivraisonDate"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonSupplierID"])) mClass._Livraison.Fournisseur.ID = (int)mDataReader["LivraisonSupplierID"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonSupplierName"])) mClass._Livraison.Fournisseur.Nom = (string)mDataReader["LivraisonSupplierName"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonImmatriculation"])) mClass._Livraison.Immatriculation = (string)mDataReader["LivraisonImmatriculation"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonEstimatedBags"])) mClass._Livraison.SacsDeclares = (int)mDataReader["LivraisonEstimatedBags"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonEstimatedTonnage"])) mClass._Livraison.PoidsDeclare = (decimal)mDataReader["LivraisonEstimatedTonnage"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeID"])) mClass._Livraison.LivraisonType.ID = (int)mDataReader["LivraisonTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeName"])) mClass._Livraison.LivraisonType.Designation = (string)mDataReader["LivraisonTypeName"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonIsAchat"])) mClass.Livraison.LivraisonType.EstAchat = (bool)mDataReader["LivraisonIsAchat"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonIsEmpotage"])) mClass.Livraison.LivraisonType.EstEmpotage = (bool)mDataReader["LivraisonIsEmpotage"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonNumeroLot"])) mClass._Livraison.NumLot = (string)mDataReader["LivraisonNumeroLot"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonNumConteneur"])) mClass.Livraison.NumConteneur = (string)mDataReader["LivraisonNumConteneur"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonNumPlomb"])) mClass.Livraison.NumPlomb = (string)mDataReader["LivraisonNumPlomb"];

                    mClass._Bascule = new Bascule();
                    if (!DBNull.Value.Equals(mDataReader["BasculeID"])) mClass._Bascule.ID = (int)mDataReader["BasculeID"];
                    if (!DBNull.Value.Equals(mDataReader["BasculeName"])) mClass._Bascule.Designation = (string)mDataReader["BasculeName"];
                    
                    if (!DBNull.Value.Equals(mDataReader["PeseeNumero"])) mClass._Numero = (int)mDataReader["PeseeNumero"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeFirstWeightDate"])) mClass._DateP1 = (DateTime)mDataReader["PeseeFirstWeightDate"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeFirstWeight"])) mClass._PoidsP1 = (decimal)mDataReader["PeseeFirstWeight"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeSacsDecharges"])) mClass.SacsDecharges = (int)mDataReader["PeseeSacsDecharges"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeSacsAcceptes"])) mClass._SacsAcceptes = (int)mDataReader["PeseeSacsAcceptes"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeSecondWeightDate"])) mClass._DateP2 = (DateTime)mDataReader["PeseeSecondWeightDate"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeSecondWeight"])) mClass._PoidsP2 = (decimal)mDataReader["PeseeSecondWeight"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeGrossWeight"])) mClass._PoidsBrut = (decimal)mDataReader["PeseeGrossWeight"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeTareOfBags"])) mClass._TareEmballages = (decimal)mDataReader["PeseeTareOfBags"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeTareOfPalets"])) mClass._TarePalettes = (decimal)mDataReader["PeseeTareOfPalets"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeNetWeightDelivered"])) mClass._PoidsNetLivre = (decimal)mDataReader["PeseeNetWeightDelivered"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeStatus"])) mClass._Statut = (string)mDataReader["PeseeStatus"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeIsManual"])) mClass._IsManual = (bool)mDataReader["PeseeIsManual"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeCancelled"])) mClass.Cancelled = (bool)mDataReader["PeseeCancelled"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeRejected"])) mClass.Rejected = (bool)mDataReader["PeseeRejected"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeRaisonManuelle"])) mClass._RaisonPeseeManuelle = (string)mDataReader["PeseeRaisonManuelle"];

                    // New Columns added
                    if (!DBNull.Value.Equals(mDataReader["SacsBons"])) mClass._SacsBons = (int)mDataReader["SacsBons"];
                    if (!DBNull.Value.Equals(mDataReader["SacsMauvais"])) mClass._SacsMauvais = (int)mDataReader["SacsMauvais"];
                    if (!DBNull.Value.Equals(mDataReader["SacsNylon"])) mClass._SacsNylon = (int)mDataReader["SacsNylon"];
                    if (!DBNull.Value.Equals(mDataReader["SacsOPA"])) mClass._SacsOPA = (int)mDataReader["SacsOPA"];
                    if (!DBNull.Value.Equals(mDataReader["NbrePalettes"])) mClass._NbrePalette = (int)mDataReader["NbrePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsVGM"])) mClass._PoidsVGM = (decimal)mDataReader["PoidsVGM"];
                    if (!DBNull.Value.Equals(mDataReader["TareConteneur"])) mClass._TareConteneur = (decimal)mDataReader["TareConteneur"];


                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                                        
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n Pesee:MapFromDataReader");
            }
        }

    }
        

    public class PeseeRefoulee : Pesee
    {
        decimal _PreviousGrossWeight;

        public decimal PreviousGrossWeight
        {
            get
            {
                return _PreviousGrossWeight;
            }

            set
            {
                _PreviousGrossWeight = value;
            }
        }

        public string PreviousGrossWeightAsString
        {
            get
            {
                return _PreviousGrossWeight != 0 ? string.Format("{0:#,#}", _PreviousGrossWeight).TrimStart() : "0";
            }
        }

        public override bool fnFirstWeighing()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("PeseeRefoulee_FirstWeighing");

                db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddInParameter(mCommande, "@DeliveryID", SqlDbType.UniqueIdentifier, this.Livraison.ID);
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, this.Livraison.Campagne.Designation);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, this.Livraison.Site.ID);
                db().AddInParameter(mCommande, "@LivraisonTypeID", SqlDbType.Int, this.Livraison.LivraisonType.ID);
                db().AddInParameter(mCommande, "@PontBasculeID", SqlDbType.Int, this.Bascule.ID);
                db().AddOutParameter(mCommande, "@Numero", SqlDbType.Int, 0);
                db().AddInParameter(mCommande, "@DateP1", SqlDbType.DateTime, this.DateP1);
                db().AddInParameter(mCommande, "@PoidsP1", SqlDbType.Decimal, this.PoidsP1);
                db().AddInParameter(mCommande, "@NbrSacsAcceptes", SqlDbType.Int, this.SacsAcceptes);                
                db().AddInParameter(mCommande, "@PoidsBrutPrecedent", SqlDbType.Decimal, _PreviousGrossWeight);               
                db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);

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
                        //base.UpdateAuditFields();
                        Result = true;

                        if (_isnew)
                        {
                            this.ID = (Guid)db().Parameters(mCommande, "@ID");
                            this.Numero = (int)db().Parameters(mCommande, "@Numero");
                        }

                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
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
                throw new Exception(ex.Message + "\r\n" + "Pesee:fnFirstWeighing");

            }
            return Result;
        }
        
        public override bool fnSecondWeighing()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("PeseeRefoulee_SecondWeighing");
                                
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, this.ID);                
                db().AddInParameter(mCommande, "@SacsDecharges", SqlDbType.Int, SacsDecharges);
                //db().AddInParameter(mCommande, "@SacsAcceptes", SqlDbType.Int, SacsAcceptes);
                db().AddInParameter(mCommande, "@DateP2", SqlDbType.DateTime, DateP2);
                db().AddInParameter(mCommande, "@PoidsP2", SqlDbType.Decimal, PoidsP2);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, PoidsBrut);
                
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, UtilisateurModification);

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
                        //base.UpdateAuditFields();
                        Result = true;                       

                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
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
                throw new Exception(ex.Message + "\r\n" + "Pesee:fnSecondWeighing");

            }
            return Result;
        }

        public override bool fnManualWeighing()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("PeseeRefoulee_ManualWeighing");

                db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                db().AddInParameter(mCommande, "@DeliveryID", SqlDbType.UniqueIdentifier, this.Livraison.ID);
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, this.Livraison.Campagne.Designation);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, this.Livraison.Site.ID);
                db().AddInParameter(mCommande, "@LivraisonTypeID", SqlDbType.Int, this.Livraison.LivraisonType.ID);
                db().AddInParameter(mCommande, "@PontBasculeID", SqlDbType.Int, this.Bascule.ID);
                db().AddOutParameter(mCommande, "@Numero", SqlDbType.Int, 0);
                db().AddInParameter(mCommande, "@DateP1", SqlDbType.DateTime, this.DateP1);
                db().AddInParameter(mCommande, "@PoidsP1", SqlDbType.Decimal, this.PoidsP1);
                db().AddInParameter(mCommande, "@NbrSacsDecharges", SqlDbType.Int, this.SacsDecharges);
                db().AddInParameter(mCommande, "@SacsAcceptes", SqlDbType.Int, this.SacsAcceptes);
                db().AddInParameter(mCommande, "@DateP2", SqlDbType.DateTime, this.DateP2);
                db().AddInParameter(mCommande, "@PoidsP2", SqlDbType.Decimal, this.PoidsP2);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, this.PoidsBrut);
                db().AddInParameter(mCommande, "@PoidsBrutPrecedent", SqlDbType.Decimal, _PreviousGrossWeight);
                //db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, this.TareEmballages);
                //db().AddInParameter(mCommande, "@TarePalettes", SqlDbType.Decimal, this.TarePalettes);
                //db().AddInParameter(mCommande, "@PoidsLivre", SqlDbType.Decimal, this.PoidsNetLivre);
                db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, this.UtilisateurCreation);

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
                        //base.UpdateAuditFields();
                        Result = true;

                        if (_isnew)
                        {
                            this.ID = (Guid)db().Parameters(mCommande, "@ID");
                            this.Numero = (int)db().Parameters(mCommande, "@Numero");
                        }

                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
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
                throw new Exception(ex.Message + "\r\n" + "Pesee:fnUpdate");

            }
            return Result;
        }

        public override bool fnCancel()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("PeseeRefoulee_Cancel");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, this.ID);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);
                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                db().ExecuteNonQuery(ref mCommande);

                //if (!this._isnew)
                //{

                //}
                //else
                //{
                //    db().AddOutParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0);
                //}

                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        //base.UpdateAuditFields();
                        Result = true;

                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
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
                throw new Exception(ex.Message + "\r\n" + "PeseeRefoulee:fnCancel");

            }
            return Result;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("PeseeRefoulee_Get", (Guid)Id);
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

        public override List<DataPersist> fnSelect(int pontBasculeID, string cropYearID, string livraisonID, int fournisseurID, DateTime mStartDate, DateTime mEndDate, short mStatus, int SiteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("PeseeRefoulee_Select");

                db().AddInParameter(mCommande, "@PontBasculeID", SqlDbType.Int, pontBasculeID);
                db().AddInParameter(mCommande, "@CropYearID", SqlDbType.VarChar, cropYearID);
                db().AddInParameter(mCommande, "@DeliveryTypeID", SqlDbType.VarChar, livraisonID);
                db().AddInParameter(mCommande, "@Supplier", SqlDbType.VarChar, fournisseurID);
                db().AddInParameter(mCommande, "@StartDate", SqlDbType.DateTime, mStartDate);
                db().AddInParameter(mCommande, "@EndDate", SqlDbType.DateTime, mEndDate);
                db().AddInParameter(mCommande, "@Status", SqlDbType.SmallInt, mStatus);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, SiteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PeseeRefoulee mClass = new PeseeRefoulee();

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

        private static void MapFromDataReader(PeseeRefoulee mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass.ID = (Guid)mDataReader["ID"];

                    mClass.Livraison = new Livraison();
                    mClass.Livraison.LivraisonType = new LivraisonType();
                    mClass.Livraison.Fournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["LivraisonID"])) mClass.Livraison.ID = (Guid)mDataReader["LivraisonID"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonNumber"])) mClass.Livraison.Numero = (string)mDataReader["LivraisonNumber"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonDate"])) mClass.Livraison.DateLivraison = (DateTime)mDataReader["LivraisonDate"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonSupplierID"])) mClass.Livraison.Fournisseur.ID = (int)mDataReader["LivraisonSupplierID"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonSupplierName"])) mClass.Livraison.Fournisseur.Nom = (string)mDataReader["LivraisonSupplierName"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonImmatriculation"])) mClass.Livraison.Immatriculation = (string)mDataReader["LivraisonImmatriculation"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonEstimatedBags"])) mClass.Livraison.SacsDeclares = (int)mDataReader["LivraisonEstimatedBags"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonEstimatedTonnage"])) mClass.Livraison.PoidsDeclare = (decimal)mDataReader["LivraisonEstimatedTonnage"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeID"])) mClass.Livraison.LivraisonType.ID = (int)mDataReader["LivraisonTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeName"])) mClass.Livraison.LivraisonType.Designation = (string)mDataReader["LivraisonTypeName"];                    

                    mClass.Bascule = new Bascule();
                    if (!DBNull.Value.Equals(mDataReader["BasculeID"])) mClass.Bascule.ID = (int)mDataReader["BasculeID"];
                    if (!DBNull.Value.Equals(mDataReader["BasculeName"])) mClass.Bascule.Designation = (string)mDataReader["BasculeName"];

                    if (!DBNull.Value.Equals(mDataReader["PeseeNumero"])) mClass.Numero = (int)mDataReader["PeseeNumero"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeFirstWeightDate"])) mClass.DateP1 = (DateTime)mDataReader["PeseeFirstWeightDate"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeFirstWeight"])) mClass.PoidsP1 = (decimal)mDataReader["PeseeFirstWeight"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeSacsDecharges"])) mClass.SacsDecharges = (int)mDataReader["PeseeSacsDecharges"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeSacsAcceptes"])) mClass.SacsAcceptes = (int)mDataReader["PeseeSacsAcceptes"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeSecondWeightDate"])) mClass.DateP2 = (DateTime)mDataReader["PeseeSecondWeightDate"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeSecondWeight"])) mClass.PoidsP2 = (decimal)mDataReader["PeseeSecondWeight"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeGrossWeight"])) mClass._PreviousGrossWeight = (decimal)mDataReader["PeseeGrossWeight"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeTareOfBags"])) mClass.TareEmballages = (decimal)mDataReader["PeseeTareOfBags"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeTareOfPalets"])) mClass.TarePalettes = (decimal)mDataReader["PeseeTareOfPalets"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeNetWeightDelivered"])) mClass.PoidsNetLivre = (decimal)mDataReader["PeseeNetWeightDelivered"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeStatus"])) mClass.Statut = (string)mDataReader["PeseeStatus"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeIsManual"])) mClass.IsManual = (bool)mDataReader["PeseeIsManual"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeCancelled"])) mClass.Cancelled = (bool)mDataReader["PeseeCancelled"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeRejected"])) mClass.Rejected = (bool)mDataReader["PeseeRejected"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeRejectedGrossWeight"])) mClass.PoidsBrut = (decimal)mDataReader["PeseeRejectedGrossWeight"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n Pesee:MapFromDataReader");
            }
        }
    }
    
    public class PeseeViewModel
    {
        public Pesee _Pesee { get; set; }

        public bool IsDevelopmentEnvironment { get; set; }

        public bool IsCallByPendingWindows { get; set; }


        public bool IsRejected { get; set; }

        public string DeliveryNumber
        {
            get
            {
                if (_Pesee == null || _Pesee.Livraison == null || !IsCallByPendingWindows)
                    return string.Empty;

                return _Pesee.Livraison.Numero.ToString();
            } 
        }

        public string Title
        {
            get
            {

                if (_ExecMode == Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return "Weighing : Nouveau";
                else if (_ExecMode == Components.Settings.EnumsDefinition.eExecMode.Update)
                    return "Weighing : Update";
                else if (_ExecMode == Components.Settings.EnumsDefinition.eExecMode.Consult)
                    return "Weighing : Properties";
                else
                    return "Weighing : Nouveau";
            }

            set { }

        }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }

        public bool HasAccessToAllWeighBridges { get; set; }
        
        public bool WeighingWithVGM { get; set; }

        public bool AutorizePurchase { get; set; }

        public bool IsFirstWeighing { get; set; }       

        public bool IsScaleWeighing { get; set; }

        public string LoadBasculeMethod { get; set; }
        
        public string CriteriaTitle { get; set; }

        public Bascule _Bascule { get; set; }

        public string SacTypeID { get; set; }
        public string SacNylonTare { get; set; }
    }
    
    public class PeseeRefouleeViewModel
    {
        public PeseeRefoulee _Pesee { get; set; }

        public bool IsDevelopmentEnvironment { get; set; }

        public string Title
        {
            get
            {

                if (_ExecMode == Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return "Weighing - Rejection : Nouveau";
                else if (_ExecMode == Components.Settings.EnumsDefinition.eExecMode.Update)
                    return "Weighing - Rejection : Update";
                else if (_ExecMode == Components.Settings.EnumsDefinition.eExecMode.Consult)
                    return "Weighing - Rejection : Properties";
                else
                    return "Weighing - Rejection : Nouveau";
            }

        }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }

        public bool HasAccessToAllWeighBridges { get; set; }

        public bool IsFirstWeighing { get; set; }

        public bool IsScaleWeighing { get; set; }

        public string LoadBasculeMethod { get; set; }

        public string CriteriaTitle { get; set; }

        public Bascule _Bascule { get; set; }

        public string SacTypeID { get; set; }
    }

    public class PeseeHistory : Pesee
    {
        private Guid _HistoryID;
        private DateTime _DateAction;
        private string _UserName;

        public Guid HistoryID
        {
            get { return _HistoryID; }
            set { _HistoryID = value; }
        }

        public DateTime DateAction
        {
            get { return _DateAction; }
            set { _DateAction = value; }
        }

        public string DateActionAsString
        {
            get { return _DateAction != null ? _DateAction.ToString() : string.Empty; }            
        }

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public override List<DataPersist> fnSelect(DateTime mStartDate, DateTime mEndDate)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Pesee_History_Select");
                
                db().AddInParameter(mCommande, "@StartDate", SqlDbType.DateTime, mStartDate);
                db().AddInParameter(mCommande, "@EndDate", SqlDbType.DateTime, mEndDate);
                
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PeseeHistory mClass = new PeseeHistory();

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

        private static void MapFromDataReader(PeseeHistory mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass.HistoryID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeID"])) mClass.ID = (Guid)mDataReader["PeseeID"];

                    mClass.Livraison = new Livraison();
                    mClass.Livraison.LivraisonType = new LivraisonType();
                    mClass.Livraison.Fournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["LivraisonID"])) mClass.Livraison.ID = (Guid)mDataReader["LivraisonID"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonNumber"])) mClass.Livraison.Numero = (string)mDataReader["LivraisonNumber"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonDate"])) mClass.Livraison.DateLivraison = (DateTime)mDataReader["LivraisonDate"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonSupplierID"])) mClass.Livraison.Fournisseur.ID = (int)mDataReader["LivraisonSupplierID"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonSupplierName"])) mClass.Livraison.Fournisseur.Nom = (string)mDataReader["LivraisonSupplierName"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonImmatriculation"])) mClass.Livraison.Immatriculation = (string)mDataReader["LivraisonImmatriculation"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonEstimatedBags"])) mClass.Livraison.SacsDeclares = (int)mDataReader["LivraisonEstimatedBags"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonEstimatedTonnage"])) mClass.Livraison.PoidsDeclare = (decimal)mDataReader["LivraisonEstimatedTonnage"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeID"])) mClass.Livraison.LivraisonType.ID = (int)mDataReader["LivraisonTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeName"])) mClass.Livraison.LivraisonType.Designation = (string)mDataReader["LivraisonTypeName"];

                    mClass.Bascule = new Bascule();
                    if (!DBNull.Value.Equals(mDataReader["BasculeID"])) mClass.Bascule.ID = (int)mDataReader["BasculeID"];
                    if (!DBNull.Value.Equals(mDataReader["BasculeName"])) mClass.Bascule.Designation = (string)mDataReader["BasculeName"];
                    
                    if (!DBNull.Value.Equals(mDataReader["UserName"])) mClass.UserName = (string)mDataReader["UserName"];
                    if (!DBNull.Value.Equals(mDataReader["DateAction"])) mClass.DateAction = (DateTime)mDataReader["DateAction"];

                    if (!DBNull.Value.Equals(mDataReader["PeseeNumero"])) mClass.Numero = (int)mDataReader["PeseeNumero"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeDateP1"])) mClass.DateP1 = (DateTime)mDataReader["PeseeDateP1"];
                    if (!DBNull.Value.Equals(mDataReader["PeseePoidsP1"])) mClass.PoidsP1 = (decimal)mDataReader["PeseePoidsP1"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeSacsDecharges"])) mClass.SacsDecharges = (int)mDataReader["PeseeSacsDecharges"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeSacsAcceptes"])) mClass.SacsAcceptes = (int)mDataReader["PeseeSacsAcceptes"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeDateP2"])) mClass.DateP2 = (DateTime)mDataReader["PeseeDateP2"];
                    if (!DBNull.Value.Equals(mDataReader["PeseePoidsP2"])) mClass.PoidsP2 = (decimal)mDataReader["PeseePoidsP2"];
                    if (!DBNull.Value.Equals(mDataReader["PeseePoidsBrut"])) mClass.PoidsBrut = (decimal)mDataReader["PeseePoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeTareSacs"])) mClass.TareEmballages = (decimal)mDataReader["PeseeTareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeTarePalettes"])) mClass.TarePalettes = (decimal)mDataReader["PeseeTarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PeseePoidsLivre"])) mClass.PoidsNetLivre = (decimal)mDataReader["PeseePoidsLivre"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeStatus"])) mClass.Statut = (string)mDataReader["PeseeStatus"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeIsManual"])) mClass.IsManual = (bool)mDataReader["PeseeIsManual"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeCancelled"])) mClass.Cancelled = (bool)mDataReader["PeseeCancelled"];
                    if (!DBNull.Value.Equals(mDataReader["PeseeRejected"])) mClass.Rejected = (bool)mDataReader["PeseeRejected"];                    

                    if (!DBNull.Value.Equals(mDataReader["SacsBons"])) mClass.SacsBons = (int)mDataReader["SacsBons"];
                    if (!DBNull.Value.Equals(mDataReader["SacsMauvais"])) mClass.SacsMauvais = (int)mDataReader["SacsMauvais"];
                    if (!DBNull.Value.Equals(mDataReader["SacsNylon"])) mClass.SacsNylon = (int)mDataReader["SacsNylon"];
                    if (!DBNull.Value.Equals(mDataReader["SacsOPA"])) mClass.SacsOPA = (int)mDataReader["SacsOPA"];
                    if (!DBNull.Value.Equals(mDataReader["NbrePalettes"])) mClass.NbrePalette = (int)mDataReader["NbrePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsVGM"])) mClass.PoidsVGM = (decimal)mDataReader["PoidsVGM"];
                    if (!DBNull.Value.Equals(mDataReader["TareConteneur"])) mClass.TareConteneur = (decimal)mDataReader["TareConteneur"];

                    if (!DBNull.Value.Equals(mDataReader["RaisonChangementTypePesee"])) mClass.RaisonChangementType = (string)mDataReader["RaisonChangementTypePesee"];
                    if (!DBNull.Value.Equals(mDataReader["RaisonCorrectionPoidsPremierePesee"])) mClass.RaisonCorrectionPremierePoids = (string)mDataReader["RaisonCorrectionPoidsPremierePesee"];
                    if (!DBNull.Value.Equals(mDataReader["RaisonCorrectionPoidsDeuxiemePesee"])) mClass.RaisonCorrectionDeuxiemePoids = (string)mDataReader["RaisonCorrectionPoidsDeuxiemePesee"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n Pesee:MapFromDataReader");
            }
        }

    }

    public class PeseeAPI : Pesee
    {
        private DateTime _DatePesee;
        private int _Id;
        private decimal _Valeur;

        public DateTime DatePesee
        {
            get
            {
                return _DatePesee;
            }

            set
            {
                _DatePesee = value;
            }
        }

        public int Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }

        public decimal Valeur
        {
            get
            {
                return _Valeur;
            }

            set
            {
                _Valeur = value;
            }
        }
    }
}
