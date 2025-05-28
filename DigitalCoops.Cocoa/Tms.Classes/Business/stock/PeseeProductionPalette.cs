using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Components.Data;

namespace Tms.Classes.Business.stock
{
    public class PeseeProductionPalette : DataPersist
    {
        #region Fields
        private Guid _ID;
        private PeseeProduction _PeseeProduction;
        private BonDeLivraison _BonDeLivraison;
        private DateTime _DatePesee;
        private int _NombreSacs;
        private string _NumeroPalette;
        private string _NumeroLivraison;
        private string _LivraisonImmatriculation;
        private decimal _PoidsBrut;

        private decimal _TareSacs;
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

        public PeseeProduction PeseeProduction
        {
            get
            {
                return _PeseeProduction;
            }

            set
            {
                _PeseeProduction = value;
            }
        }

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

        public string DatePeseeLongAsString
        {
            get { return (_DatePesee != null) ? _DatePesee.ToString("G") : string.Empty; }

        }

        public int NombreSacs
        {
            get
            {
                return _NombreSacs;
            }

            set
            {
                _NombreSacs = value;
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

        public string PoidsBrutAsString
        {
            get
            {
                return _PoidsBrut != 0 ? string.Format("{0:#,#}", _PoidsBrut).TrimStart() : "0";
            }
        }

        public string NumeroPalette
        {
            get
            {
                return _NumeroPalette;
            }

            set
            {
                _NumeroPalette = value;
            }
        }

        public string NumeroLivraison
        {
            get
            {
                return _NumeroLivraison;
            }

            set
            {
                _NumeroLivraison = value;
            }
        }

        public string LivraisonImmatriculation
        {
            get
            {
                return _LivraisonImmatriculation;
            }

            set
            {
                _LivraisonImmatriculation = value;
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

        public BonDeLivraison BonDeLivraison
        {
            get
            {
                return _BonDeLivraison;
            }

            set
            {
                _BonDeLivraison = value;
            }
        }
        #endregion
        #region Constructor
        public PeseeProductionPalette()
        {

        }

        public PeseeProductionPalette(Guid myId)
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
            throw new NotImplementedException();
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V2_PeseeProductionPalette_Get", (Guid)Id);
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
            return fnSelect((Guid?)null);
        }

        public virtual List<DataPersist> fnSelect(Guid? PeseeID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_PeseeProductionPalette_SelectByPesee");

                db().AddInParameter(mCommande, "@PeseeID", SqlDbType.UniqueIdentifier, PeseeID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PeseeProductionPalette mClass = new PeseeProductionPalette();

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

        public bool fnUpdate(DataTransaction mTran)
        {
            bool Result;
            DataCommand mCommande;
            try
            {
                if (this._isnew)
                {
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProductionPalette_New");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_PeseeProductionPalette_New");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@PeseeProductionID", SqlDbType.UniqueIdentifier, _PeseeProduction.ID);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, _PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, _TareSacs);
                db().AddInParameter(mCommande, "@DatePesee", SqlDbType.DateTime, _DatePesee);
                db().AddInParameter(mCommande, "@NumeroPalette", SqlDbType.VarChar, _NumeroPalette);
                db().AddInParameter(mCommande, "@NumeroLivraison", SqlDbType.VarChar, _NumeroLivraison);
                db().AddInParameter(mCommande, "@Immatriculation", SqlDbType.VarChar, _LivraisonImmatriculation);

                if (_BonDeLivraison != null)
                    db().AddInParameter(mCommande, "@BonDeLivraisonID", SqlDbType.UniqueIdentifier, _BonDeLivraison.ID);
                else
                    db().AddInParameter(mCommande, "@BonDeLivraisonID", SqlDbType.UniqueIdentifier, DBNull.Value);

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
                        //_ID = (Guid)db().Parameters(mCommande, "@ID");
                        _isnew = false;
                        //_Statut = (string)db().Parameters(mCommande, "@statut");

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
                throw new Exception(ex.Message + "\r\n" + "PeseeProductionPalette:fnUpdate");

            }
            return Result;
        }

        public bool fnRemove()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_PeseeProductionPalette_Remove");
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
                throw new Exception(ex.Message + "\r\n" + "PeseeProductionPalette:fnRemove");
            }
            return Result;
        }


        public override string ToString()
        {
            throw new NotImplementedException();
        }

        private static void MapFromDataReader(PeseeProductionPalette mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass.ID = (Guid)mDataReader["ID"];

                    mClass._PeseeProduction = new PeseeProduction();
                    if (!DBNull.Value.Equals(mDataReader["PeseeProductionID"])) mClass.PeseeProduction.ID = (Guid)mDataReader["PeseeProductionID"];

                    mClass._BonDeLivraison = new BonDeLivraison();
                    if (!DBNull.Value.Equals(mDataReader["BonDeLivraisonID"])) mClass._BonDeLivraison.ID = (Guid)mDataReader["BonDeLivraisonID"];

                    if (!DBNull.Value.Equals(mDataReader["DatePesee"])) mClass._DatePesee = (DateTime)mDataReader["DatePesee"];
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass._NombreSacs = (int)mDataReader["NombreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass._TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];

                    if (!DBNull.Value.Equals(mDataReader["NumeroPalette"])) mClass._NumeroPalette = (string)mDataReader["NumeroPalette"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroLivraison"])) mClass._NumeroLivraison = (string)mDataReader["NumeroLivraison"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonImmatriculation"])) mClass._LivraisonImmatriculation = (string)mDataReader["LivraisonImmatriculation"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n PeseeProductionPalette:MapFromDataReader");
            }
        }

        #endregion
    }

    public partial class PeseeProductionPaletteViewModel
    {
        public PeseeProductionPalette _PeseeProductionPalette { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
