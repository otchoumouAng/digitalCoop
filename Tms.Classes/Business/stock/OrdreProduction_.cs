using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Classes.Shared.Sales;
using Tms.Components.Data;


namespace Tms.Classes.Business.stock
{
    public class OrdreProduction : DataPersist
    {
        #region Fields
        private Guid _ID;
        private Articles _Article;
        private int _Annee;
        private int _Semaine;
        private LigneProduction _LigneDeProduction;
        private string _NumeroProduction;
        private string _ReferenceExterne;
        private Recolte _Recolte;
        private Client _Client;
        private int _NbrePaletteAProduire;

        private DateTime? _DateProduction; //Date prévue
        private DateTime? _DateDebutProduction;
        private DateTime? _DateFinProduction;

        private string _Statut;
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

        public Articles Article
        {
            get
            {
                return _Article;
            }

            set
            {
                _Article = value;
            }
        }



        public int Semaine
        {
            get
            {
                return _Semaine;
            }

            set
            {
                _Semaine = value;
            }
        }


        public int Annee
        {
            get
            {
                return _Semaine;
            }

            set
            {
                _Semaine = value;
            }
        }

        public LigneProduction LigneDeProduction
        {
            get
            {
                return _LigneDeProduction;
            }

            set
            {
                _LigneDeProduction = value;
            }
        }


        public string NumeroProduction
        {
            get
            {
                return _NumeroProduction;
            }

            set
            {
                _NumeroProduction = value;
            }
        }

        public string ReferenceExterne
        {
            get
            {
                return _ReferenceExterne;
            }

            set
            {
                _ReferenceExterne = value;
            }
        }

        public Recolte Recolte
        {
            get
            {
                return _Recolte;
            }

            set
            {
                _Recolte = value;
            }
        }

        public Client Client
        {
            get
            {
                return _Client;
            }

            set
            {
                _Client = value;
            }
        }

        public int NbrePaletteAProduire
        {
            get
            {
                return _NbrePaletteAProduire;
            }

            set
            {
                _NbrePaletteAProduire = value;
            }
        }

        public DateTime? DateProduction
        {
            get
            {
                return _DateProduction;
            }

            set
            {
                _DateProduction = value;
            }
        }
        public string DateProductionAstring
        {
            get
            {
                return _DateProduction != null ? _DateProduction.Value.ToShortDateString() : string.Empty;
            }
        }

        public DateTime? DateDebutProduction
        {
            get
            {
                return _DateDebutProduction;
            }

            set
            {
                _DateDebutProduction = value;
            }
        }

        public string DateDebutProductionAsString
        {
            get
            {
                return _DateDebutProduction != null ? _DateDebutProduction.Value.ToShortDateString() : string.Empty;
            }
        }

        public DateTime? DateFinProduction
        {
            get
            {
                return _DateFinProduction;
            }

            set
            {
                _DateFinProduction = value;
            }
        }

        public string DateFinProductionAsString
        {
            get
            {
                return _DateFinProduction != null ? _DateFinProduction.Value.ToShortDateString() : string.Empty;
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
        #endregion


        #region Methods
        public override bool fnActivate()
        {
            throw new NotImplementedException();
        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("pp_OrdreProduction_DeActivate");
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
                }
            }
            catch (Exception ex)
            {
                bolResult = false;
                throw new Exception(ex.Message + "\r\n" + "OrdreProduction:fnDeActivate");
            }
            return bolResult;
        }

        public bool fnGetByNumber(object Numero)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("pp_OrdreProduction_GetByNumber", (string)Numero);
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

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("pp_OrdreProduction_Get", (Guid)Id);
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
            return fnSelect("-1");
        }

        public List<DataPersist> fnSelect(string statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("pp_OrdreProduction_Select");

                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char,2, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    OrdreProduction mClass = new OrdreProduction();
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

        public List<DataPersist> fnSelectOpenned()
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("pp_OrdreProduction_SelectOpenned");
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    OrdreProduction mClass = new OrdreProduction();
                    MapFromDataReaderLite(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectForLot");
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
                    mCommande = db().CreateStoredProcCommand("pp_OrdreProduction_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                    db().AddInParameter(mCommande, "@Desactive", SqlDbType.VarChar, _Desactive);

                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("pp_OrdreProduction_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                    
                }

                db().AddInParameter(mCommande, "@Statut", SqlDbType.VarChar, _Statut);
                db().AddInParameter(mCommande, "@Article", SqlDbType.UniqueIdentifier,_Article.ID);
                db().AddInParameter(mCommande, "@Annee", SqlDbType.SmallInt,_Annee);
                db().AddInParameter(mCommande, "@Semaine", SqlDbType.SmallInt, _Semaine);
                db().AddInParameter(mCommande, "@LigneDeProduction", SqlDbType.Int, _LigneDeProduction.ID);

                db().AddInParameter(mCommande, "@NumProduction", SqlDbType.Int, _NumeroProduction);
                db().AddInParameter(mCommande, "@ReferenceExterne", SqlDbType.VarChar, _ReferenceExterne);
                db().AddInParameter(mCommande, "@CodeRecolte", SqlDbType.Int, _Recolte.ID);
                db().AddInParameter(mCommande, "@CodeClient", SqlDbType.Int, _Client.ID);
                db().AddInParameter(mCommande, "@NbrePaletteAProduire", SqlDbType.Int, _NbrePaletteAProduire);
                db().AddInParameter(mCommande, "@DatePrevue", SqlDbType.DateTime, _DateProduction);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, _DateDebutProduction);
                db().AddInParameter(mCommande, "@DateFin", SqlDbType.DateTime, _DateFinProduction);
                

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
                        _Statut = "NA";
                        if (_isnew)
                        {
                            _NumeroProduction = (string)db().Parameters(mCommande, "@Numero");
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
                throw new Exception(ex.Message + "\r\n" + "OrdreProduction:fnUpdate");

            }
            return Result;
        }

        public bool fnClose()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("pp_OrdreProduction_Close");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);                
                db().AddInParameter(mCommande, "@ClotureUtilisateur", SqlDbType.VarChar, _ClotureUtilisateur);
                db().AddInParameter(mCommande, "@DateFinProduction", SqlDbType.DateTime, _DateFinProduction);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);
                
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
                        _Statut = "CL";                        
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
                throw new Exception(ex.Message + "\r\n" + "OrdreProduction:fnClose");

            }
            return Result;
        }

        public override string ToString()
        {
            return _NumeroProduction;
        }

        private static void MapFromDataReader(OrdreProduction mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["ArticleCode"]))
                    {
                        mClass._Article = new Articles();
                        mClass._Article.ID = (int)mDataReader["ArticleCode"];
                        mClass._Article.Nom = (string)mDataReader["ArticleDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["LigneProductionCode"]))
                    {
                        mClass._LigneDeProduction = new LigneProduction();
                        mClass._LigneDeProduction.ID = (int)mDataReader["LigneProductionCode"];
                        mClass._LigneDeProduction.Designation = (string)mDataReader["LigneProductionDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["RecolteCode"]))
                    {
                        mClass._Recolte = new Recolte();
                        mClass._Recolte.ID = (int)mDataReader["RecolteCode"];
                        mClass._Recolte.Designation = (string)mDataReader["RecolteDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ClientCode"]))
                    {
                        mClass._Client = new Client();
                        mClass._Client.ID = (int)mDataReader["ClientCode"];
                        mClass._Client.Nom = (string)mDataReader["ClientNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Annee"])) mClass._Annee = (int)mDataReader["Annee"];
                    if (!DBNull.Value.Equals(mDataReader["Semaine"])) mClass._Semaine = (int)mDataReader["Semaine"];
                    if (!DBNull.Value.Equals(mDataReader["NombrePaletteAProduire"])) mClass._NbrePaletteAProduire = (int)mDataReader["NombrePaletteAProduire"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroProduction"])) mClass._NumeroProduction = (string)mDataReader["NumeroProduction"];
                    if (!DBNull.Value.Equals(mDataReader["ReferenceExterne"])) mClass._ReferenceExterne = (string)mDataReader["ReferenceExterne"];
                     

                    if (!DBNull.Value.Equals(mDataReader["DatePrevue"])) mClass._DateProduction = (DateTime)mDataReader["DatePrevue"];
                    if (!DBNull.Value.Equals(mDataReader["DatePrevue"])) mClass._DateDebutProduction = (DateTime)mDataReader["DateDebut"];
                    if (!DBNull.Value.Equals(mDataReader["DateFin"])) mClass._DateFinProduction = (DateTime)mDataReader["DateFin"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroProduction"])) mClass._NumeroProduction = (string)mDataReader["NumeroProduction"];
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
                throw new Exception(ex.Message + "\nOrdreProduction:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderLite(OrdreProduction mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroProduction"])) mClass._NumeroProduction = (string)mDataReader["NumeroProduction"];

                    mClass._Article = new Articles();
                    if (!DBNull.Value.Equals(mDataReader["ArticleCode"])) mClass._Article.ID = (int)mDataReader["ArticleCode"];
                    if (!DBNull.Value.Equals(mDataReader["ArticleDesignation"])) mClass._Article.Designation = (string)mDataReader["ArticleDesignation"];


                    //if (!DBNull.Value.Equals(mDataReader["QuartID"]))
                    //{
                    //    mClass._Quart = new AgentProduction();
                    //    mClass._Quart.ID = (int)mDataReader["QuartID"];
                    //    mClass._Quart.Nom = (string)mDataReader["QuartNom"];
                    //}

                    //if (!DBNull.Value.Equals(mDataReader["ChefQuartID"]))
                    //{
                    //    mClass._ChefQuart = new AgentProduction();
                    //    mClass._ChefQuart.ID = (int)mDataReader["ChefQuartID"];
                    //    mClass._ChefQuart.Nom = (string)mDataReader["ChefQuartNom"];
                    //}

                    //if (!DBNull.Value.Equals(mDataReader["MelangeurID"]))
                    //{
                    //    mClass._Melangeur = new AgentProduction();
                    //    mClass._Melangeur.ID = (int)mDataReader["MelangeurID"];
                    //    mClass._Melangeur.Nom = (string)mDataReader["MelangeurNom"];
                    //}


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nOrdreProduction:MapFromDataReaderLite");
            }
        }
        #endregion
    }
 
        public partial class OrdreProductionViewModel
        {
            public OrdreProduction _OrdreProduction { get; set; }

            public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
        }
    


}
