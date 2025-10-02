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
    public class ConnaissementMaritime : DataPersist
    {
        #region "Fields"
        private Guid _ID;
        //private string _Numero;
        private Embarquement _Embarquement;
        private Navire _Navire;
        private DateTime _Date;
        private string _NumeroBL;
        private string _NumeroDossier;
        private string _NumeroD6;
        private string _NumeroAffaire;
        private string _NumeroBesc;
        private string _NumeroDeclaration;
        private bool _Desactive;
        private string _Statut;
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

        //public string Numero
        //{
        //    get { return _Numero; }
        //    set { _Numero = value; }
        //}

        public string NumeroBL
        {
            get { return _NumeroBL; }
            set { _NumeroBL = value; }
        }

        public string NumeroDossier
        {
            get { return _NumeroDossier; }
            set { _NumeroDossier = value; }
        }

        public string NumeroD6
        {
            get { return _NumeroD6; }
            set { _NumeroD6 = value; }
        }


        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
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

        public Navire Navire
        {
            get
            {
                return _Navire;
            }

            set
            {
                _Navire = value;
            }
        }

        public string NumeroAffaire
        {
            get
            {
                return _NumeroAffaire;
            }

            set
            {
                _NumeroAffaire = value;
            }
        }

        public string NumeroBesc
        {
            get
            {
                return _NumeroBesc;
            }

            set
            {
                _NumeroBesc = value;
            }
        }

        public string NumeroDeclaration
        {
            get
            {
                return _NumeroDeclaration;
            }

            set
            {
                _NumeroDeclaration = value;
            }
        }
        #endregion

        #region Constructor
        public ConnaissementMaritime()
        {

        }

        public ConnaissementMaritime(Guid myId)
        {
            this.fnGet(myId);
        }

        #endregion

        #region "Properties"
        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_BL_Activate");
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
                throw new Exception(ex.Message + "\r\n" + "V2_BL:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_BL_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "V2_BL:fnDeActivate");
            }
            return Result;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_BL_Get", (Guid)Id);
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
                DataCommand mCommande = db().CreateStoredProcCommand("V2_BL_Select");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, ExportateurID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@IsDisabled", SqlDbType.Int, IsDisabled);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    ConnaissementMaritime mClass = new ConnaissementMaritime();
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

                    mCommande = db().CreateStoredProcCommand("V2_BL_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    //db().AddOutParameter(mCommande, "@Numero", SqlDbType.VarChar, 9);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_BL_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@EmbarquementID", SqlDbType.UniqueIdentifier, _Embarquement.ID);
                db().AddInParameter(mCommande, "@NavireID", SqlDbType.Int, _Navire.ID);

                db().AddInParameter(mCommande, "@Date", SqlDbType.DateTime, _Date);
             
                db().AddInParameter(mCommande, "@NumeroBL", SqlDbType.VarChar, _NumeroBL);
                db().AddInParameter(mCommande, "@NumeroDossier", SqlDbType.VarChar, _NumeroDossier);
                db().AddInParameter(mCommande, "@NumeroD6", SqlDbType.VarChar, _NumeroD6);
                db().AddInParameter(mCommande, "@NumeroDeclaration", SqlDbType.VarChar, _NumeroDeclaration);
                db().AddInParameter(mCommande, "@NumeroAffaire", SqlDbType.VarChar, _NumeroAffaire);
                db().AddInParameter(mCommande, "@NumeroBesc", SqlDbType.VarChar, _NumeroBesc);

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
                        //if (this._isnew)
                        //{
                        //    _Numero = (string)db().Parameters(mCommande, "@Numero");
                        //}

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
                throw new Exception(ex.Message + "\r\n" + "V2_BL:fnUpdate");

            }
            return Result;
        }
        #endregion
        #region "Private Methods"
        public override string ToString()
        {
            throw new NotImplementedException();
        }

        private static void MapFromDataReader(ConnaissementMaritime mClass, IDataReader mDataReader)
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
                    }

                    if (!DBNull.Value.Equals(mDataReader["NavireID"]))
                    {
                        mClass._Navire = new Navire();
                        mClass._Navire.ID = (int)mDataReader["NavireID"];
                        mClass._Navire.Nom = (string)mDataReader["NomNavire"];
                    }

                    //if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DateBL"])) mClass._Date = (DateTime)mDataReader["DateBL"];

                    if (!DBNull.Value.Equals(mDataReader["NumeroBL"])) mClass._NumeroBL = (string)mDataReader["NumeroBL"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroDossier"])) mClass._NumeroDossier = (string)mDataReader["NumeroDossier"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroD6"])) mClass._NumeroD6 = (string)mDataReader["NumeroD6"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroAffaire"])) mClass._NumeroAffaire = (string)mDataReader["NumeroAffaire"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroBesc"])) mClass._NumeroBesc = (string)mDataReader["NumeroBesc"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroDeclaration"])) mClass._NumeroDeclaration = (string)mDataReader["NumeroDeclaration"];

                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];


                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nV2_BL:MapFromDataReader");
            }
        }
        #endregion
    }
    public partial class ConnaissementMaritimeViewModel
    {
        public ConnaissementMaritime _ConnaissementMaritime { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
