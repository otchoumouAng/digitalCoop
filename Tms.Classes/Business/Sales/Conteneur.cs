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
    public class Conteneur : DataPersist
    {
        #region "Fields"
        private Guid _ID;
        private Empotage _Empotage;
        private ConteneurType _ConteneurType;
        private string _Numero;
        private string _NumPlombCompMaritime;
        private string _NumPlombTelcar;
        private string _LivraisonNumero;
        
        private bool _Desactive;
        private string _LotsConteneur;
        private int _NbreSacs;
        #endregion

        #region "Properties"
        [ModelField(IDProperty = true, SortType = Ext.Net.SortTypeMethod.None, SortDir = Ext.Net.SortDirection.ASC)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Empotage Empotage
        {
            get { return _Empotage; }
            set { _Empotage = value; }
        }

        public ConteneurType ConteneurType
        {
            get { return _ConteneurType; }
            set { _ConteneurType = value; }
        }

        public string ConteneurTypeAsString
        {
            get
            {
                if (_ConteneurType != null)
                    return _ConteneurType.Designation;
                else
                    return string.Empty;
            }

        }

       public string Numero
       {
            get { return _Numero ; }
            set { _Numero = value; }
       }

        public string NumPlombCompMaritime
        {
            get { return _NumPlombCompMaritime; }
            set { _NumPlombCompMaritime = value; }
        }

        public string NumPlombTelcar
        {
            get { return _NumPlombTelcar; }
            set { _NumPlombTelcar = value; }
        }

        public string LivraisonNumero
        {
            get { return _LivraisonNumero; }
            set { _LivraisonNumero = value; }
        }

        

        public string LotsConteneur
        {
            get { return _LotsConteneur; }
            set { _LotsConteneur = value; }
        }

        public int NbreSacs
        {
            get { return _NbreSacs; }
            set { _NbreSacs = value; }
        }

        public string NbreSacsAsString
        {
            get { return _NbreSacs != 0 ? String.Format("{0:#,#}", _NbreSacs).TrimStart() : string.Empty; }
        }


        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
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
        #endregion
        #region Constructor
        public Conteneur()
        {

        }

        public Conteneur(Guid myId)
        {
            this.fnGet(myId);
        }

        #endregion
        #region "Methods"
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
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_EmpotageDetails_Get", (Guid)Id);
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
            Guid EmpotageID = Guid.Empty;
            return fnSelect(EmpotageID, -1, -1);
        }

        public List<DataPersist> fnSelect(Guid EmpotageID, int ConteneurTypeID, int IsDisabled)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_EmpotageDetails_Select");
                db().AddInParameter(mCommande, "@EmpotageID", SqlDbType.UniqueIdentifier, EmpotageID);
                db().AddInParameter(mCommande, "@ConteneurTypeID", SqlDbType.Int, ConteneurTypeID);
                db().AddInParameter(mCommande, "@IsDisabled", SqlDbType.Int, IsDisabled);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Conteneur mClass = new Conteneur();

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

        public List<DataPersist> fnSelectByEmpotage(Guid EmpotageID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_EmpotageDetails_SelectByEmpotage");
                db().AddInParameter(mCommande, "@EmpotageID", SqlDbType.UniqueIdentifier, EmpotageID);                                
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Conteneur mClass = new Conteneur();

                    MapFromDataReaderLite(mClass, mDataReader);

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


        public bool fnRemoveLotAll()
        {
            if (!this._isnew)
            {
                bool bolResult;
                DataCommand mCommande = db().CreateStoredProcCommand("V2_EmpotageDetails_RemoveLotAll");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
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
                    throw new Exception(ex.Message + "\r\n" + "Conteneur:fnRemoveLotAll");
                }
                return bolResult;
            }
            return false;
        }
        public override bool fnUpdate()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {
                    mCommande = db().CreateStoredProcCommand("V2_EmpotageDetails_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_EmpotageDetails_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@EmpotageID", SqlDbType.UniqueIdentifier, _Empotage.ID);
                
                db().AddInParameter(mCommande, "@ConteneurTypeID", SqlDbType.Int, _ConteneurType.ID);
                db().AddInParameter(mCommande, "@NumConteneur", SqlDbType.VarChar, _Numero);
                db().AddInParameter(mCommande, "@NumPlombCompMartime", SqlDbType.VarChar, _NumPlombCompMaritime);
                db().AddInParameter(mCommande, "@NumPlombTelcar", SqlDbType.VarChar, _NumPlombTelcar);

                db().AddInParameter(mCommande, "@LivraisonNumero", SqlDbType.VarChar, _LivraisonNumero);



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
                throw new Exception(ex.Message + "\r\n" + "EmpotageDetails:fnUpdate");

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
                    mCommande = db().CreateStoredProcCommand("V2_EmpotageDetails_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_EmpotageDetails_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@EmpotageID", SqlDbType.UniqueIdentifier, _Empotage.ID);

                db().AddInParameter(mCommande, "@ConteneurTypeID", SqlDbType.Int, _ConteneurType.ID);
                db().AddInParameter(mCommande, "@NumConteneur", SqlDbType.VarChar, _Numero);
                db().AddInParameter(mCommande, "@NumPlombCompMartime", SqlDbType.VarChar, _NumPlombCompMaritime);
                db().AddInParameter(mCommande, "@NumPlombTelcar", SqlDbType.VarChar, _NumPlombTelcar);


                db().AddInParameter(mCommande, "@LivraisonNumero", SqlDbType.VarChar, _LivraisonNumero);



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
                throw new Exception(ex.Message + "\r\n" + "V2_EmpotageDetails:fnUpdate");

            }
            return Result;
        }

        public bool fnRemove()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_EmpotageDetails_Remove");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
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
                throw new Exception(ex.Message + "\r\n" + "Conteneur:fnRemove");
            }
            return Result;
        }
        #endregion

        #region "Private Members"
        public override string ToString()
        {
            throw new NotImplementedException();
        }
        private static void MapFromDataReader(Conteneur mClass, IDataReader mDataReader)
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
                        
                    }

                    if (!DBNull.Value.Equals(mDataReader["ConteneurTypeID"]))
                    {
                        mClass._ConteneurType = new ConteneurType();
                        mClass._ConteneurType.ID = (int)mDataReader["ConteneurTypeID"];
                        mClass._ConteneurType.Designation = (string)mDataReader["ConteneurTypeDesignation"];
                    }



                    if (!DBNull.Value.Equals(mDataReader["NumConteneur"])) mClass._Numero = (string)mDataReader["NumConteneur"];
                    if (!DBNull.Value.Equals(mDataReader["NumPlombCompMaritime"])) mClass._NumPlombCompMaritime = (string)mDataReader["NumPlombCompMaritime"];
                    if (!DBNull.Value.Equals(mDataReader["NumPlombTelcar"])) mClass._NumPlombTelcar = (string)mDataReader["NumPlombTelcar"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonNumero"])) mClass._LivraisonNumero = (string)mDataReader["LivraisonNumero"];


                    if (!DBNull.Value.Equals(mDataReader["LotsConteneur"])) mClass._LotsConteneur = (string)mDataReader["LotsConteneur"];
                    if (!DBNull.Value.Equals(mDataReader["NbreSacs"])) mClass._NbreSacs = (int)mDataReader["NbreSacs"];


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
                throw new Exception(ex.Message + "\nConteneur:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderLite(Conteneur mClass, IDataReader mDataReader)
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
                    }

                    if (!DBNull.Value.Equals(mDataReader["ConteneurTypeID"]))
                    {
                        mClass._ConteneurType = new ConteneurType();
                        mClass._ConteneurType.ID = (int)mDataReader["ConteneurTypeID"];
                        mClass._ConteneurType.Designation = (string)mDataReader["ConteneurTypeDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["NumConteneur"])) mClass._Numero = (string)mDataReader["NumConteneur"];
                    if (!DBNull.Value.Equals(mDataReader["NbreSacs"])) mClass._NbreSacs = (int)mDataReader["NbreSacs"];
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nConteneur:MapFromDataReaderLite");
            }
        }

        #endregion
    }

    public partial class ConteneurViewModel
    {
        public Conteneur _Conteneur { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
