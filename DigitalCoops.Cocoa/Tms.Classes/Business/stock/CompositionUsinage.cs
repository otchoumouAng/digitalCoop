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
    public class CompositionUsinage : DataPersist
    {
        #region fields
        private Guid _ID;
        private OrdreProduction _OrdreProduction;
        private LotType _LotType;
        private AgentProduction _Melangeur;
        private AgentProduction _Quart;
        private AgentProduction _ChefQuart;
        private Laboratoire _Laboratoire;
        private DateTime _DateComposition;
        private string _Approbateur;
        private DateTime _DateApprobation;
        private bool _Desactive;
        private string _Statut;
        private string _Numero;
        private string _Commentaire;
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

        public OrdreProduction OrdreProduction
        {
            get
            {
                return _OrdreProduction;
            }

            set
            {
                _OrdreProduction = value;
            }
        }

        public string NumeroProduction
        {
            get
            {
                return _OrdreProduction != null ? _OrdreProduction.NumeroProduction : string.Empty;
            }
        }

        public AgentProduction Melangeur
        {
            get
            {
                return _Melangeur;
            }

            set
            {
                _Melangeur = value;
            }
        }

        public string LaboratoireAsString
        {
            get
            {
                return _Laboratoire != null ? _Laboratoire.Designation : string.Empty;
            }
        }

        public Laboratoire Laboratoire
        {
            get
            {
                return _Laboratoire;
            }

            set
            {
                _Laboratoire = value;
            }
        }

        public DateTime DateComposition
        {
            get
            {
                return _DateComposition;
            }

            set
            {
                _DateComposition = value;
            }
        }
        public string DateCompositionAsString
        {
            get
            {
                return _DateComposition != null ? _DateComposition.ToString() : string.Empty;
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

        public AgentProduction ChefQuart
        {
            get
            {
                return _ChefQuart;
            }

            set
            {
                _ChefQuart = value;
            }
        }
        
        public AgentProduction Quart
        {
            get
            {
                return _Quart;
            }

            set
            {
                _Quart = value;
            }
        }

        public bool IsApproved
        {
            get
            {
                return (_Statut == "AP") && (_DateApprobation != null);
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

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desactive || _Statut == "CA")
                    return 0; // BulletCross
                else if (_Statut == "AP")
                    return 1; // Tick                
                else
                    return 2;
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

        public string Numero
        {
            get
            {
                return _Numero;
            }

            set
            {
                _Numero = value;
            }
        }
        #endregion

        #region Constructor
        public CompositionUsinage()
        {

        }

        public CompositionUsinage(Guid myID)
        {
            this.fnGet(myID);
        }
        #endregion
        #region methods
        public override bool fnActivate()
        {
            throw new NotImplementedException();
        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_CompositionUsinage_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "CompositionUsinage:fnDeActivate");
            }
            return bolResult;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_CompositionUsinage_Get", (Guid)Id);
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

        public bool fnGetByProduction(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_CompositionUsinage_GetByProduction", (Guid)Id);
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
            return fnSelect(null, null, "-1");
        }

        public List<DataPersist> fnSelect(DateTime? DateDebut, DateTime? DateFin, string statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_CompositionUsinage_Select");
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, DateDebut);
                db().AddInParameter(mCommande, "@DateFin", SqlDbType.DateTime, DateFin);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char,2, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    CompositionUsinage mClass = new CompositionUsinage();
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
                    mCommande = db().CreateStoredProcCommand("V2_CompositionUsinage_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.Char, 7);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_CompositionUsinage_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.Char, 7);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@OrdreProductionID", SqlDbType.UniqueIdentifier, _OrdreProduction.ID);
                db().AddInParameter(mCommande, "@LotTypeID", SqlDbType.Int, _LotType.ID);
                db().AddInParameter(mCommande, "@LaboratoireID", SqlDbType.Int, _Laboratoire.ID);
                db().AddInParameter(mCommande, "@DateComposition", SqlDbType.DateTime, _DateComposition);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);

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
                            _Numero = (string)db().Parameters(mCommande, "@Numero");

                        _Statut = "NA";

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
                throw new Exception(ex.Message + "\r\n" + "CompositionUsinage:fnUpdate");

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
                    mCommande = db().CreateStoredProcCommand("V2_CompositionUsinage_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.Char,7);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_CompositionUsinage_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.Char, 7);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@OrdreProductionID", SqlDbType.UniqueIdentifier, _OrdreProduction.ID);
                //db().AddInParameter(mCommande, "@MelangeurID", SqlDbType.Int, _Melangeur.ID);
                //db().AddInParameter(mCommande, "@QuartID", SqlDbType.Int, Quart.ID);
                //db().AddInParameter(mCommande, "@ChefQuartID", SqlDbType.Int, _ChefQuart.ID);
                db().AddInParameter(mCommande, "@LaboratoireID", SqlDbType.Int, _Laboratoire.ID);
                db().AddInParameter(mCommande, "@LotTypeID", SqlDbType.Int, _LotType.ID);
                db().AddInParameter(mCommande, "@DateComposition", SqlDbType.DateTime, _DateComposition);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);

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
                        _Numero = (string)db().Parameters(mCommande, "@Numero");
                        _Statut = "NA";

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
                throw new Exception(ex.Message + "\r\n" + "CompositionUsinage:fnUpdate");

            }
            return Result;
        }

        public bool fnApprove()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                mCommande = db().CreateStoredProcCommand("V2_CompositionUsinage_Approve");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@Approbateur", SqlDbType.VarChar, _Approbateur);;

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
                throw new Exception(ex.Message + "\r\n" + "CompositionUsinage:fnApprove");
            }
            return Result;
        }


        public override string ToString()
        {
            throw new NotImplementedException();
        }

        private static void MapFromDataReader(CompositionUsinage mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;
                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass.OrdreProduction = new OrdreProduction();
                    if (!DBNull.Value.Equals(mDataReader["OrdreProductionID"]))
                    {                                               

                        mClass.OrdreProduction.ID = (Guid)mDataReader["OrdreProductionID"];
                        if (!DBNull.Value.Equals(mDataReader["NumeroProduction"])) mClass.OrdreProduction.NumeroProduction = (string)mDataReader["NumeroProduction"];
                        mClass.OrdreProduction.Campagne = new Campagne();
                        if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass.OrdreProduction.Campagne.Designation = (string)mDataReader["CampagneID"];
                        mClass.OrdreProduction.Quart = new AgentProduction();
                        if (!DBNull.Value.Equals(mDataReader["QuartNom"])) mClass.OrdreProduction.Quart.Nom = (string)mDataReader["QuartNom"];
                        mClass.OrdreProduction.ChefQuart = new AgentProduction();
                        if (!DBNull.Value.Equals(mDataReader["ChefQuartNom"])) mClass.OrdreProduction.ChefQuart.Nom = (string)mDataReader["ChefQuartNom"];
                        mClass.OrdreProduction.Melangeur = new AgentProduction();
                        if (!DBNull.Value.Equals(mDataReader["MelangeurNom"])) mClass.OrdreProduction.Melangeur.Nom = (string)mDataReader["MelangeurNom"];
                        if (!DBNull.Value.Equals(mDataReader["DateProduction"])) mClass.OrdreProduction.DateProduction = (DateTime)mDataReader["DateProduction"];
                    }

                    mClass._Laboratoire = new Laboratoire();
                    if (!DBNull.Value.Equals(mDataReader["LaboratoireID"])) mClass._Laboratoire.ID = (int)mDataReader["LaboratoireID"];
                    if (!DBNull.Value.Equals(mDataReader["LaboratoireDesignation"])) mClass._Laboratoire.Designation = (string)mDataReader["LaboratoireDesignation"];

                    if (!DBNull.Value.Equals(mDataReader["OrdreProductionID"]))
                    {                        
                        mClass.LotType = new LotType();
                        if (!DBNull.Value.Equals(mDataReader["LotTypeID"])) mClass.LotType.ID = (int)mDataReader["LotTypeID"];
                        if (!DBNull.Value.Equals(mDataReader["LotTypeDesignation"])) mClass.LotType.Designation = (string)mDataReader["LotTypeDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Numero = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DateComposition"])) mClass._DateComposition = (DateTime)mDataReader["DateComposition"];
                    if (!DBNull.Value.Equals(mDataReader["DateApprobation"])) mClass._DateApprobation = (DateTime)mDataReader["DateApprobation"];
                    if (!DBNull.Value.Equals(mDataReader["Approbateur"])) mClass._Approbateur = (string)mDataReader["Approbateur"];
                    //if (!DBNull.Value.Equals(mDataReader["DateApprobation"])) mClass._dat = (double)mDataReader["DateApprobation"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass.Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n CompositionUsinage:MapFromDataReader");
            }
        }

        #endregion
    }

    public partial class CompositionUsinageViewModel
    {
        public CompositionUsinage _CompositionUsinage { get; set; }

        public int? _DefaultLaboratoire { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
