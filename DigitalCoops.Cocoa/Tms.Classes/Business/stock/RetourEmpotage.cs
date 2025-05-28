using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Business.Sales;
using Tms.Classes.Business.stock;
using Tms.Classes.Shared;
using Tms.Classes.Shared.Sales;
using Tms.Components.Data;

namespace Tms.Classes.Business.stock
{
    public class RetourEmpotage : DataPersist
    {
        #region "Field"
        private Guid _ID;
        private Campagne _Campagne;
        private Empotage _Empotage;
        private Lot _Lot;
        private string _ReferenceLot;
        private int _Sens;
        private decimal _Quantite;
        private decimal _TareSacs;
        private decimal _TarePalette;
        private decimal _PoidsBrut;
        private decimal _PoidsNet;
        private DateTime _DateRetour;
        private string _Statut;
        private string _Commentaire;

        #endregion

        #region "Properties"
        [ModelField(IDProperty = true, SortType = Ext.Net.SortTypeMethod.None, SortDir = Ext.Net.SortDirection.ASC)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        

        public string EmpotageAsString
        {
            get
            {
                if (_Empotage != null && _Empotage.Embarquement != null)
                    return _Empotage.Embarquement.Numero;
                else
                    return string.Empty;
            }

        }

        public string CampagneAsString
        {
            get { return _Campagne != null ? _Campagne.Designation : string.Empty; }

        }

        public string LotAsString
        {
            get { return _Lot != null ? _Lot.NumeroLot : string.Empty;  }

        }
        public int mIcon
        {
            get
            {
                if (Statut == "CA")
                    return 0; // BulletCross              
                else
                    return 2; //                     
            }
        }

        public bool Desactive
        {
            get
            {
                if (Statut == "CA")
                    return true; // BulletCross              
                else
                    return false; //                     
            }
        }

        public Empotage Empotage
        {
            get
            {
                return _Empotage;
            }

            set
            {
                _Empotage = value;
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

        public string ReferenceLot
        {
            get
            {
                return _ReferenceLot;
            }

            set
            {
                _ReferenceLot = value;
            }
        }

        public int Sens
        {
            get
            {
                return _Sens;
            }

            set
            {
                _Sens = value;
            }
        }

        public decimal Quantite
        {
            get
            {
                return _Quantite;
            }

            set
            {
                _Quantite = value;
            }
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

        public DateTime DateRetour
        {
            get
            {
                return _DateRetour;
            }

            set
            {
                _DateRetour = value;
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

        public string Commentaire
        {
            get
            {
                return _Commentaire;
            }

            set
            {
                _Commentaire = value;
            }
        }
        public string DateRetourAsString
        {
            get { return _DateRetour != null ? _DateRetour.ToShortDateString() : string.Empty; }
        }

        public string QuantiteAsString
        {
            get { return _Quantite != 0 ? String.Format("{0:#,#}", _Quantite).TrimStart() : string.Empty; }
        }

        public string PoidsBrutAsString
        {
            get { return _PoidsBrut != 0 ? String.Format("{0:#,#}", _PoidsBrut).TrimStart() : string.Empty; }
        }

        public string TareSacsAsString
        {
            get { return _TareSacs != 0 ? String.Format("{0:#,#}", _TareSacs).TrimStart() : string.Empty; }
        }

        public string TarePaletteAsString
        {
            get { return _TarePalette != 0 ? String.Format("{0:#,#}", _TarePalette).TrimStart() : string.Empty; }
        }

        public string PoidsNetAsString
        {
            get { return _PoidsNet != 0 ? String.Format("{0:#,#}", _PoidsNet).TrimStart() : string.Empty; }
        }
                
        #endregion

        #region Constructor
        public RetourEmpotage()
        {

        }

        public RetourEmpotage(Guid myId)
        {
            this.fnGet(myId);
        }

        #endregion
        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_RetourEmpotage_Activate");
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
                        Statut = "NA";
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
                throw new Exception(ex.Message + "\r\n" + "V2_RetourEmpotage:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_RetourEmpotage_DeActivate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, _Lot.ID);
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
                        Statut = "CA";
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
                throw new Exception(ex.Message + "\r\n" + "V2_RetourEmpotage:fnDeActivate");
            }
            return Result;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_RetourEmpotage_Get", (Guid)Id);
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
            return fnSelect("{Tous}", null, null, "-1");
        }

        public List<DataPersist> fnSelect(string Campagne, DateTime? StartDate, DateTime? EndDate, string Statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_RetourEmpotage_Select");
                db().AddInParameter(mCommande, "@Statut", SqlDbType.VarChar, 2, Statut);
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, EndDate);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    RetourEmpotage mClass = new RetourEmpotage();
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

                    mCommande = db().CreateStoredProcCommand("V2_RetourEmpotage_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_RetourEmpotage_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.VarChar,9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@EmpotageID", SqlDbType.UniqueIdentifier, _Empotage.ID);
                db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, _Lot.ID);
                db().AddInParameter(mCommande, "@Sens", SqlDbType.Int, _Sens);
                db().AddInParameter(mCommande, "@ReferenceLot", SqlDbType.VarChar, _ReferenceLot);

                db().AddInParameter(mCommande, "@Quantite", SqlDbType.Decimal, _Quantite);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, _TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, _TarePalette);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNet);

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
                throw new Exception(ex.Message + "\r\n" + "V2_RetourEmpotage:fnUpdate");

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

                    mCommande = db().CreateStoredProcCommand("V2_RetourEmpotage_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_RetourEmpotage_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.VarChar, 9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);
                db().AddInParameter(mCommande, "@EmpotageID", SqlDbType.UniqueIdentifier, _Empotage.ID);
                db().AddInParameter(mCommande, "@LotID", SqlDbType.UniqueIdentifier, _Lot.ID);
                db().AddInParameter(mCommande, "@Sens", SqlDbType.Int, _Sens);
                db().AddInParameter(mCommande, "@ReferenceLot", SqlDbType.VarChar, _ReferenceLot);
                db().AddInParameter(mCommande, "@DateRetour", SqlDbType.DateTime, _DateRetour);

                db().AddInParameter(mCommande, "@Quantite", SqlDbType.Decimal, _Quantite);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, _TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, _TarePalette);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, _PoidsNet);

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
                throw new Exception(ex.Message + "\r\n" + "V2_RetourEmpotage:fnUpdate");

            }
            return Result;
        }

        #region "Private Members"
        public override string ToString()
        {
            throw new NotImplementedException();
        }
        private static void MapFromDataReader(RetourEmpotage mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["EmpotageID"]))
                    {
                        mClass._Empotage = new Empotage();
                        mClass._Empotage.ID = (Guid)mDataReader["EmpotageID"];
                        mClass._Empotage.Embarquement = new Embarquement();
                        mClass._Empotage.Embarquement.Numero = (string)mDataReader["EmbarquementNumero"];
                        //mClass._Empotage.Numero = (string)mDataReader["EmbarquementNumero"];

                    }

                    if (!DBNull.Value.Equals(mDataReader["CampagneID"]))
                    {
                        mClass._Campagne = new Campagne();
                        mClass._Campagne.Designation = (string)mDataReader["CampagneID"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["LotID"]))
                    {
                        mClass._Lot = new Lot();
                        mClass._Lot.ID = (Guid)mDataReader["LotID"];
                        mClass._Lot.NumeroLot = (string)mDataReader["ReferenceLot"];
                        
                    }

                    if (!DBNull.Value.Equals(mDataReader["DateRetour"])) mClass._DateRetour = (DateTime)mDataReader["DateRetour"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["Quantite"])) mClass._Quantite = (decimal)mDataReader["Quantite"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNet = (decimal)mDataReader["PoidsNet"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nV2_RetourEmpotage:MapFromDataReader");
            }
        }
        #endregion
    }

    public partial class RetourEmpotageViewModel
    {
        public RetourEmpotage _RetourEmpotage { get; set; }
        public string _DefaultCampagne { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
