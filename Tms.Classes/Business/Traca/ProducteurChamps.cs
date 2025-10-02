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

namespace Tms.Classes.Business.Traca
{
    public class ProducteurChamps : DataPersist
    {
        #region fields
        private Guid _ID;
        private Producteur _Producteur;
        private Champs _Champs;        
        private string _Libelle;
        private string _Longitude;
        private string _Latitude;
        private string _PointGps;
        private bool _Desactive;
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

        public Producteur Producteur
        {
            get
            {
                return _Producteur;
            }

            set
            {
                _Producteur = value;
            }
        }

        public Champs Champs
        {
            get
            {
                return _Champs;
            }

            set
            {
                _Champs = value;
            }
        }                                                  

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (Desactive)
                    return 0;                                  
                else
                    return 2; // 
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

        public string ProducteurNom
        {
            get
            {
                return _Producteur != null ? _Producteur.CodeNom : string.Empty;
            }
        }

        public string NomPrenomRegistre
        {
            get
            {
                return _Producteur != null ? _Producteur.NomPrenomRegistre : string.Empty;
            }
        }

        public string Libelle
        {
            get
            {
                return _Libelle;
            }

            set
            {
                _Libelle = value;
            }
        }

        public string Longitude
        {
            get
            {
                return _Longitude;
            }

            set
            {
                _Longitude = value;
            }
        }

        public string Latitude
        {
            get
            {
                return _Latitude;
            }

            set
            {
                _Latitude = value;
            }
        }

        public string ChampsNom
        {
            get
            {
                return _Champs != null ? _Champs.Designation : string.Empty;
            }
        }

        public string PointGps
        {
            get
            {
                return _PointGps;
            }

            set
            {
                _PointGps = value;
            }
        }
        #endregion

        #region Constructor
        public ProducteurChamps()
        {

        }

        public ProducteurChamps(Guid myId)
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
            DataCommand mCommande = db().CreateStoredProcCommand("V4_Producteur_Champs_DeActivate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
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
                }
            }
            catch (Exception ex)
            {
                bolResult = false;
                throw new Exception(ex.Message + "\r\n" + "ProducteurChamps:fnDeActivate");
            }
            return bolResult;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V4_Producteur_Champs_Get", (Guid)Id);
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
            return fnSelect(-1,-1);
        }

        public virtual List<DataPersist> fnSelect(Int64 mProducteurID, int mStatut = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V4_Producteur_Champs_Select");

                db().AddInParameter(mCommande, "@producteurCode", SqlDbType.BigInt, mProducteurID);
                db().AddInParameter(mCommande, "@statut", SqlDbType.Int, mStatut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    ProducteurChamps mClass = new ProducteurChamps();

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
                    mCommande = db().CreateStoredProcCommand("V4_Producteur_Champs_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);                                       
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V4_Producteur_Champs_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@producteurID", SqlDbType.UniqueIdentifier, _Producteur.ID);
                //db().AddInParameter(mCommande, "@champsID", SqlDbType.Int, _Champs.ID);
                db().AddInParameter(mCommande, "@libelle", SqlDbType.VarChar, _Libelle);
                db().AddInParameter(mCommande, "@longitude", SqlDbType.VarChar, _Longitude);
                db().AddInParameter(mCommande, "@latitude", SqlDbType.VarChar, _Latitude);
                db().AddInParameter(mCommande, "@pgps", SqlDbType.VarChar, _PointGps);

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
                        _isnew = false;
                        _RowVersionKey = db().Parameters(mCommande, "@RowVersion");
                        ID = (Guid)db().Parameters(mCommande, "@ID");                        
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
                throw new Exception(ex.Message + "\r\n" + "ProducteurChamps:fnUpdate");

            }
            return Result;
        }

        public bool fnRemove()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V4_Producteur_Champs_Remove");
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
                throw new Exception(ex.Message + "\r\n" + "ProducteurChamps:fnRemove");
            }
            return Result;
        }


        private static void MapFromDataReader(ProducteurChamps mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];                    

                    //if (!DBNull.Value.Equals(mDataReader["IdChamps"]))
                    //{
                    //    mClass._Champs = new Champs();
                    //    mClass._Champs.ID = (int)mDataReader["IdChamps"];
                    //    mClass._Champs.Designation = (string)mDataReader["ChampsNumero"];
                    //}

                    if (!DBNull.Value.Equals(mDataReader["IdProducteur"]))
                    {
                        mClass._Producteur = new Producteur();
                        mClass._Producteur.ID = (Guid)mDataReader["IdProducteur"];
                        mClass._Producteur.Code = (Int64)mDataReader["CodeProducteur"];
                        mClass._Producteur.Nom = (string)mDataReader["NomProducteur"];
                        mClass._Producteur.Prenom = (string)mDataReader["PrenomProducteur"];
                        mClass._Producteur.CodeNom = (string)mDataReader["CodeNom"];
                        mClass._Producteur.NomPrenomRegistre = (string)mDataReader["NomPrenomRegistre"];
                    }                                        
                                                           
                    if (!DBNull.Value.Equals(mDataReader["LibelleChamps"])) mClass._Libelle = (string)mDataReader["LibelleChamps"];
                    if (!DBNull.Value.Equals(mDataReader["Longitude"])) mClass._Longitude = (string)mDataReader["Longitude"];
                    if (!DBNull.Value.Equals(mDataReader["Latitude"])) mClass._Latitude = (string)mDataReader["Latitude"];
                    if (!DBNull.Value.Equals(mDataReader["pointGps"])) mClass._PointGps = (string)mDataReader["pointGps"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];

                    //mClass._Montant = mClass._PoidsBrut * mClass._PrixMoyen;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n ProducteurChamps:MapFromDataReader");
            }
        }


        public override string ToString()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public partial class ProducteurChampsViewModel
    {
        public ProducteurChamps _ProducteurChamps { get; set; }

        public string _DefaultCampagne { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }


}
