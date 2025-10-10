using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Components.Data;

namespace Tms.Classes.Shared.Sales
{
    public class ConteneurShip : DataPersist
    {
        #region "Fields"

        private int _ID;
        private string _Libelle;
        private string _Statut;
        private ConteneurType _ConteneurType;
        private bool _Desactive;
        private int _idConteneurType;
        private Campagne _Campagne;
        private CompagnieMaritime _CompagnieMaritime;
        #endregion

        #region "Properties"
        [ModelField(IDProperty = true)]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Libelle
        {
            get { return _Libelle; }
            set { _Libelle = value; }
        }

        public ConteneurType ConteneurType
        {
            get { return _ConteneurType; }
            set { _ConteneurType = value; }
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

        public string ConteneurTypeAsString
        {
            get { return _ConteneurType != null ? _ConteneurType.Designation : string.Empty; }            
        }

        public string CompagnieAsString
        {
            get { return _CompagnieMaritime != null ? _CompagnieMaritime.Nom : string.Empty; }
        }

        public string CampagneAsString
        {
            get { return _Campagne != null ? _Campagne.Designation : string.Empty; }
        }

        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
        }

        [Column(Text = "")]
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

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V4_ConteneurShip_Get", (int)Id);
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

        public bool fnGetDefaultSite()
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V4_ConteneurShip_DefaultSite_Get");
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

        public bool fnGetBySite(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V4_ConteneurShip_GetBySite", (int)Id);
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
            return fnSelect(-1,-1,"{Tous}",-1);
        }

        public List<DataPersist> fnSelect(int mStatus, int mConteneurType, string mCampagne = "{Tous}", int mCompagnie = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;
            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V4_ConteneurShip_Select");
                db().AddInParameter(mCommande, "@Status", SqlDbType.SmallInt, mStatus);
                db().AddInParameter(mCommande, "@ConteneurTypeID", SqlDbType.Int, mConteneurType);
                db().AddInParameter(mCommande, "@compagnie", SqlDbType.Int, mCompagnie);
                db().AddInParameter(mCommande, "@campagne", SqlDbType.Char,9, mCampagne);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    ConteneurShip mClass = new ConteneurShip();
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

                    mCommande = db().CreateStoredProcCommand("V4_ConteneurShip_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.Int, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V4_ConteneurShip_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@Libelle", SqlDbType.VarChar, _Libelle);
                db().AddInParameter(mCommande, "@typeid", SqlDbType.Int, _ConteneurType.ID);
                db().AddInParameter(mCommande, "@campagne", SqlDbType.Char,9, _Campagne.Designation);            
                db().AddInParameter(mCommande, "@compagnie", SqlDbType.Int, _CompagnieMaritime.ID);
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
                        _ID = (int)db().Parameters(mCommande, "@ID");

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
                throw new Exception(ex.Message + "\r\n" + "ConteneurShip:fnUpdate");

            }
            return Result;
        }


        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V4_ConteneurShip_Activate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
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
                throw new Exception(ex.Message + "\r\n" + "ConteneurShip:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V4_ConteneurShip_DeActivate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
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
                throw new Exception(ex.Message + "\r\n" + "ConteneurShip:fnDeActivate");
            }
            return Result;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        #endregion

        private static void MapFromDataReader(ConteneurShip mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (int)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Libelle"])) mClass._Libelle = (string)mDataReader["Libelle"];
                    mClass.ConteneurType = new ConteneurType();

                    if (!DBNull.Value.Equals(mDataReader["ConteneurTypeID"])) mClass.ConteneurType.ID = (int)mDataReader["ConteneurTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["ConteneurTypeID"])) mClass._idConteneurType = (int)mDataReader["ConteneurTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["ConteneurTypeNom"])) mClass.ConteneurType.Designation = (string)mDataReader["ConteneurTypeNom"];

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                    mClass.Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["Campagne"])) mClass._Campagne.Designation = (string)mDataReader["Campagne"];

                    mClass.CompagnieMaritime = new CompagnieMaritime();
                    if (!DBNull.Value.Equals(mDataReader["Compagnie"])) mClass._CompagnieMaritime.ID = (int)mDataReader["Compagnie"];
                    if (!DBNull.Value.Equals(mDataReader["NomCompagnie"])) mClass._CompagnieMaritime.Nom = (string)mDataReader["NomCompagnie"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n ConteneurShip:MapFromDataReader");
            }
        }
               
        public string AsString
        {
            get { return _Libelle; }
        }

        public int IdConteneurType
        {
            get
            {
                return _idConteneurType;
            }

            set
            {
                _idConteneurType = value;
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

        public CompagnieMaritime CompagnieMaritime
        {
            get
            {
                return _CompagnieMaritime;
            }

            set
            {
                _CompagnieMaritime = value;
            }
        }
    }

    public partial class ConteneurShipViewModel
    {
        public ConteneurShip _ConteneurShip { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
