using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Classes.Shared.stock;
using Tms.Components.Data;

namespace Tms.Classes.Business.stock
{
    public class Reusinage : DataPersist
    {
        #region Fields
        private Guid _ID;        
        private Campagne _Campagne;
        private Lot _Lot;
        private DateTime _DateReusinage;
        private string _Raison;        
        private bool _Desactive;
        private string _Statut;
        private string _Approbateur;
        private DateTime _DateApprobation;        
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

        public Lot Lot
        {
            get
            {
                return _Lot;
            }

            set
            {
                _Lot = value;
            }
        }        

        public DateTime DateReusinage
        {
            get
            {
                return _DateReusinage;
            }

            set
            {
                _DateReusinage = value;
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
        
        public bool EstApprove
        {
            get { return _Statut == "AP"; }
        }

        public string Raison
        {
            get
            {
                return _Raison;
            }

            set
            {
                _Raison = value;
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

        public string NumeroLot
        {
            get { return _Lot != null ? _Lot.NumeroLot : string.Empty; }
        }

        
        public string DateReusinageAsString
        {
            get { return _DateReusinage != null ? _DateReusinage.ToShortDateString() : string.Empty; }
        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desactive)
                    return 0; // BulletCross
                else if (_Statut == "AP")
                    return 1; // Tick                
                else
                    return 2;

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
        #endregion

        #region Constructor
        public Reusinage()
        {

        }

        public Reusinage(Guid myId)
        {
            this.fnGet(myId);
        }

        public Reusinage(string Statut)
        {
            this.Statut = _Statut;
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
            DataCommand mCommande = db().CreateStoredProcCommand("V2_Reusinage_DeActivate");
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
                        _Statut = "CA";
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
                throw new Exception(ex.Message + "\r\n" + "Reusinage:fnDeActivate");
            }
            return bolResult;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_Reusinage_Get", (Guid)Id);
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
            return fnSelect("{All]", null, null, "-1");
        }

        public List<DataPersist> fnSelect(string campagneID, DateTime? DateDebut, DateTime? DateFin, string statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_Reusinage_Select");
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char,9, campagneID);
                db().AddInParameter(mCommande, "@dateDebut", SqlDbType.DateTime, DateDebut);
                db().AddInParameter(mCommande, "@dateFin", SqlDbType.DateTime, DateFin);
                db().AddInParameter(mCommande, "@statut", SqlDbType.Char, 2, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Reusinage mClass = new Reusinage();
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
                    mCommande = db().CreateStoredProcCommand("V2_Reusinage_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);                    
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_Reusinage_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }                
                                
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, _Lot.ID);                
                db().AddInParameter(mCommande, "@dateReusinage", SqlDbType.DateTime, _DateReusinage);                
                db().AddInParameter(mCommande, "@Raison", SqlDbType.VarChar, _Raison);                               

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
                        if (this._isnew)
                        {                            
                            _Statut = "NA";
                        }                        

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
                throw new Exception(ex.Message + "\r\n" + "Reusinage:fnUpdate");

            }
            return Result;
        }

        public bool fnApprove(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("V2_Reusinage_Approve");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);                
                db().AddInParameter(mCommande, "@approbateur", SqlDbType.VarChar, _Approbateur);

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
                db().ExecuteNonQuery(ref mCommande,mTran);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        base.UpdateAuditFields();
                        Result = true;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        _ID = (Guid)db().Parameters(mCommande, "@ID");
                        _Statut = "AP";                                            
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
                throw new Exception(ex.Message + "\r\n" + "Reusinage:fnUpdate");

            }
            return Result;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        private static void MapFromDataReader(Reusinage mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["CampagneID"]))
                    {
                        mClass._Campagne = new Campagne();
                        mClass._Campagne.Designation = (string)mDataReader["CampagneID"];                        
                    }


                    if (!DBNull.Value.Equals(mDataReader["LotID"]))
                    {
                        mClass._Lot = new Lot();
                        mClass._Lot.ID = (Guid)mDataReader["LotID"];
                        mClass._Lot.NumeroLot = (string)mDataReader["NumeroLot"];
                    }                    

                    if (!DBNull.Value.Equals(mDataReader["Raison"])) mClass._Raison = (string)mDataReader["Raison"];
                    if (!DBNull.Value.Equals(mDataReader["DateReusinage"])) mClass._DateReusinage = (DateTime)mDataReader["DateReusinage"];   
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];                    
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];

                    if (!DBNull.Value.Equals(mDataReader["Approbateur"])) mClass.Approbateur = (string)mDataReader["Approbateur"];
                    if (!DBNull.Value.Equals(mDataReader["DateApprobation"])) mClass.DateApprobation = (DateTime)mDataReader["DateApprobation"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];                                        
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nReusinage:MapFromDataReader");
            }
        }

        #endregion
    }

    public partial class ReusinageViewModel
    {
        public Reusinage _Reusinage { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
