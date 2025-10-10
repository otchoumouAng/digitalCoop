using Ext.Net;
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
    //[Proxy(Read = "~/DailyPrice/Select")]
    //[JsonReader(RootProperty = "data")]
    public class PrixJournalier : DataPersist
    {
        #region "Fields"

        private Guid _ID;
        private Site _Site;
        private string _Numero;
        private DateTime _DatePrix;
        private DateTime _DateDebut;
        private DateTime _DateEcheance;
        private Campagne _Campagne;
        private decimal _Prix;
        private string _Commentaire;
        private string _Statut;
        private bool _Desactive;
        private bool _IsApproved;
        
        #endregion

        #region "Properties"

        [ModelField(IDProperty = true, SortType = Ext.Net.SortTypeMethod.None, SortDir = Ext.Net.SortDirection.ASC)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Site Site
        {
            get { return _Site; }
            set { _Site = value; }
        }

        [ModelField(Name = "Numero", Type = Ext.Net.ModelFieldType.String)]
        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }        

        public DateTime DatePrix
        {
            get { return _DatePrix; }
            set { _DatePrix = value; }
        }

        public string DatePrixAsString
        {
            get { return _DatePrix.ToShortDateString(); }           
        }

        public DateTime DateDebut
        {
            get { return _DateDebut; }
            set { _DateDebut = value; }
        }

        public string DateDebutAsString
        {
            get { return _DateDebut.ToShortDateString(); }
        }

        public DateTime DateEcheance
        {
            get { return _DateEcheance; }
            set { _DateEcheance = value; }
        }

        public string DateEcheanceAsString
        {
            get { return _DateEcheance.ToShortDateString(); }            
        }
        
        public decimal Prix
        {
            get { return _Prix ; }
            set { _Prix = value; }
        }

        public string PrixAsString
        {
            get { return String.Format("{0:#,#}", _Prix).TrimStart(); }            
        }
        
        public string Commentaire
        {
            get { return _Commentaire; }
            set { _Commentaire = value; }
        }

        public string Statut
        {
            get { return _Statut; }
            set { _Statut = value; }
        }

        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
        }

        [Column(Text = "IsApproved")]
        public bool IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desactive)
                    return 0; // BulletCross                
                else 
                    return 2; 
                                 
            }
        }

        public string SiteAsString
        {
            get { return _Site != null ? _Site.Nom : string.Empty; }

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
        
        #endregion

        #region "Constructor"

        public PrixJournalier()
        {

        }

        public PrixJournalier(Guid myId)
        {
            this.fnGet(myId);
        }

        public PrixJournalier(int siteID, DateTime? DatePrix)
        {
            this.fnSelectByDate(siteID, DatePrix);
        }

        #endregion

        #region Methods
        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("PrixJournalier_Get", (Guid)Id);
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
            return fnSelect(null, null, -1,-1);
        }

        public List<DataPersist> fnSelect(DateTime? startdate, DateTime? enddate, int desactive, int siteID = -1)
        {            
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("PrixJournalier_Select");
                //db().AddInParameter(mCommande, "@locationID", SqlDbType.Int, LocationID);
                db().AddInParameter(mCommande, "@startdate", SqlDbType.DateTime, startdate);
                db().AddInParameter(mCommande, "@enddate", SqlDbType.DateTime, enddate);
                db().AddInParameter(mCommande, "@status", SqlDbType.Int, desactive);
                db().AddInParameter(mCommande, "@siteid", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PrixJournalier mClass = new PrixJournalier();

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

                    mCommande = db().CreateStoredProcCommand("PrixJournalier_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 8);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("PrixJournalier_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, _Campagne.Designation);
                db().AddInParameter(mCommande, "@siteid", SqlDbType.Int, _Site.ID);
                db().AddInParameter(mCommande, "@dateentry", SqlDbType.DateTime, _DatePrix);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, _DateDebut);
                db().AddInParameter(mCommande, "@dateecheance", SqlDbType.DateTime, _DateEcheance);
                db().AddInParameter(mCommande, "@prix", SqlDbType.Money, _Prix);
                db().AddInParameter(mCommande, "@commentaire", SqlDbType.VarChar, _Commentaire);
                //db().AddInParameter(mCommande, "@statut", SqlDbType.Char, _Statut);                            

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
                        if (this.IsNew)
                        {                           
                            _Numero = (string)db().Parameters(mCommande, "@Numero");
                        }
                        
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
                throw new Exception(ex.Message + "\r\n" + "PrixJournalier:fnUpdate");

            }
            return Result;
        }

        public bool fnUpdate(DataTransaction mtran)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {

                    mCommande = db().CreateStoredProcCommand("PrixJournalier_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 8);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("PrixJournalier_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, _Campagne.Designation);
                db().AddInParameter(mCommande, "@siteid", SqlDbType.Int, _Site.ID);
                db().AddInParameter(mCommande, "@dateentry", SqlDbType.DateTime, _DatePrix);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, _DateDebut);
                db().AddInParameter(mCommande, "@dateecheance", SqlDbType.DateTime, _DateEcheance);
                db().AddInParameter(mCommande, "@prix", SqlDbType.Money, _Prix);
                db().AddInParameter(mCommande, "@commentaire", SqlDbType.VarChar, _Commentaire);
                //db().AddInParameter(mCommande, "@statut", SqlDbType.Char, _Statut);                            

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
                        if (this.IsNew)
                        {
                            _Numero = (string)db().Parameters(mCommande, "@Numero");
                        }

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
                throw new Exception(ex.Message + "\r\n" + "PrixJournalierAgence:fnUpdate");

            }
            return Result;
        }


        public bool fnExtend()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("PrixJournalier_Extend");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                                
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, _DateDebut);
                db().AddInParameter(mCommande, "@dateecheance", SqlDbType.DateTime, _DateEcheance);                                                               

                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);

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
                throw new Exception(ex.Message + "\r\n" + "PrixJournalier:fnExtend");

            }
            return Result;
        }

        public bool fnShrink()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("PrixJournalier_Extend");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);

                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, _DateDebut);
                db().AddInParameter(mCommande, "@dateecheance", SqlDbType.DateTime, _DateEcheance);

                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);

                db().AddParameter(mCommande, "@RowVersion", SqlDbType.Timestamp, 0, _RowVersionKey, ParameterDirection.InputOutput);

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
                throw new Exception(ex.Message + "\r\n" + "PrixJournalier:fnExtend");

            }
            return Result;
        }

        public override bool fnActivate()
        {
            
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("PrixJournalier_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "PrixJournalier:fnActivate");
            }
            return Result;
            
        }        

        public override bool fnDeActivate()
        {            
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("PrixJournalier_DeActivate");
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
                        break;
                }
            }
            catch (Exception ex)
            {
                bolResult = false;
                throw new Exception(ex.Message + "\r\n" + "PrixJournalier:fnDeActivate");
            }
            return bolResult;            
        }

        public bool fnApprove()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("PrixJournalier_Approve");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
            db().AddOutParameter(mCommande, "@statut", SqlDbType.Char, 2);
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
                        //Desactive = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        _Statut = (string)db().Parameters(mCommande, "@statut");
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
                throw new Exception(ex.Message + "\r\n" + "PrixJournalier:fnApprove");
            }
            return Result;
        }

        //public bool fnSelectByDate(int siteID, string datePrix = "")
        //{
        //    IDataReader mDataReader = null;
        //    try
        //    {
        //        mDataReader = db().ExecuteReader("PrixJournalierByDate_Select", siteID,datePrix);
        //        if (mDataReader.Read())
        //        {
        //            MapFromDataReader(this, mDataReader);
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnSelectByDate");
        //    }
        //    finally
        //    {
        //        if (mDataReader != null) mDataReader.Close();
        //    }
        //}

        public bool fnSelectByDate(int siteID, DateTime? datePrix)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;
            //datePrix = string.IsNullOrEmpty(datePrix) ? DateTime.Now.ToString() : datePrix;
            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("PrixJournalierByDate_Select");
                //db().AddInParameter(mCommande, "@locationID", SqlDbType.Int, LocationID);
                db().AddInParameter(mCommande, "@DatePrix", SqlDbType.DateTime, datePrix);                
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, siteID);
                mDataReader = db().ExecuteReader(mCommande);

                if (mDataReader.Read())
                {
                    MapFromDataReader(this, mDataReader);
                }
                return true;               
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

        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _Numero;
        }

        private static void MapFromDataReader(PrixJournalier mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    mClass._Site = new Site();
                    if (!DBNull.Value.Equals(mDataReader["siteID"])) mClass._Site.ID = (int)mDataReader["siteID"];                    

                    if (!DBNull.Value.Equals(mDataReader["EntryDate"])) mClass._DatePrix = (DateTime)mDataReader["EntryDate"];
                    if (!DBNull.Value.Equals(mDataReader["StartDate"])) mClass._DateDebut = (DateTime)mDataReader["StartDate"];
                    if (!DBNull.Value.Equals(mDataReader["DueDate"])) mClass._DateEcheance = (DateTime)mDataReader["DueDate"];
                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["Prix"])) mClass._Prix = (decimal)mDataReader["Prix"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    
                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                   
                    if (!DBNull.Value.Equals(mDataReader["SiteNom"])) mClass._Site.Nom = (string)mDataReader["SiteNom"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nPrixJournalier:MapFromDataReader");
            }
        }
        #endregion


    }

    public partial class PrixJournalierViewModel
    {
        public PrixJournalier _PrixJournalier { get; set; }
        public Parametres _Parametres { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }

        public int _SiteParDefaut { get; set; }
        public string _SiteParDefautNom { get; set; }

        public SelectedRowCollection InitiallySelectedRows
        {
            get
            {
                return new SelectedRowCollection()
                {
                    new SelectedRow(_SiteParDefaut)
                };
            }
        }
    }
}
