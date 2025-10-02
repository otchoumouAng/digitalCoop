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

namespace Tms.Classes.Business.Sites
{
    public class PeseeSite : DataPersist
    {
        #region fields
        private Guid _ID;
        private Campagne _Campagne;
        private Livraison _Livraison;
        private SacType _SacType;
        private Site _Sites;
        private DateTime _DatePesee;
        private Fournisseur _Fournisseur;
        private string _ReferenceObjet;
        private int _NumeroPesee;
        private int _NombreSacs;
        private decimal _PoidsBrut;
        private decimal _TareSacs;
        private decimal _TarePalette;
        private decimal _PoidsNet;
        private string _Statut;
        private string _Commentaire;
        private bool _Desactive;
        private bool _IsManual;
        private bool _IsPending;
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

        public Livraison Livraison
        {
            get
            {
                return _Livraison;
            }

            set
            {
                _Livraison = value;
            }
        }

        public Site Sites
        {
            get
            {
                return _Sites;
            }

            set
            {
                _Sites = value;
            }
        }

        public string LivraisonID
        {
            get { return _Livraison != null && _Livraison.Numero != null ? _Livraison.Numero  : string.Empty; }
        }

        public string Immatriculation
        {
            get { return _Livraison != null ? _Livraison.Immatriculation : string.Empty; }
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

        public string DatePeseeAsString
        {
            get { return _DatePesee != null ? _DatePesee.ToShortDateString() : string.Empty; }
        }        

        public int NumeroPesee
        {
            get
            {
                return _NumeroPesee;
            }

            set
            {
                _NumeroPesee = value;
            }
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

        public string NombreSacsAsString
        {
            get
            {
                return _NombreSacs != 0 ? string.Format("{0:#,#}", _NombreSacs).TrimStart() : "0";
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

        public string TareSacsAsString
        {
            get
            {
                return _TareSacs != 0 ? string.Format("{0:#,#}", _TareSacs).TrimStart() : "0";
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

        public string TarePaletteAsString
        {
            get
            {
                return _TarePalette != 0 ? string.Format("{0:#,#}", _TarePalette).TrimStart() : "0";
            }
        }

        public string PoidsNetEvalAsString
        {
            get
            {
                return _PoidsNet != 0 ? string.Format("{0:#,#}", Math.Round((_PoidsNet * 8) / 100), 0).TrimStart() : "0";
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

        public string PoidsNetAsString
        {
            get
            {
                return _PoidsNet != 0 ? string.Format("{0:#,#}", _PoidsNet).TrimStart() : "0";
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

        public bool EstFinalise
        {
            get
            {
                return( _Statut == "AP");
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

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desactive)
                    return 0; // Annulée
                else if (_Statut == "AP")
                    return 1; // Tick
                else if (_Statut == "NA")
                    return 2; // 
                else if (_Statut == "CA")
                    return 3; //                                 
                else
                    return 2; // 
            }
        }

        public bool IsManual
        {
            get
            {
                return _IsManual;
            }

            set
            {
                _IsManual = value;
            }
        }

        public SacType SacType
        {
            get
            {
                return _SacType;
            }

            set
            {
                _SacType = value;
            }
        }

        public Fournisseur Fournisseur
        {
            get
            {
                return _Fournisseur;
            }

            set
            {
                _Fournisseur = value;
            }
        }

        public string FournisseurAsString
        {
            get
            {
                return _Fournisseur != null ? _Fournisseur.Nom : string.Empty;
            }            
        }

        public string SiteAsString
        {
            get
            {
                return _Sites != null ? _Sites.Nom : string.Empty;
            }
        }

        public string SacTypeAsString
        {
            get
            {
                return _SacType != null ? _SacType.Designation : string.Empty;
            }
        }

        public bool IsPending
        {
            get
            {
                return _IsPending;
            }

            set
            {
                _IsPending = value;
            }
        }
        #endregion


                #region Constructor
        public PeseeSite()
        {

        }

        public PeseeSite(Guid myId)
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
            DataCommand mCommande = db().CreateStoredProcCommand("V3_Pesee_DeActivate");
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
                        Statut = "CA";
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
                throw new Exception(ex.Message + "\r\n" + "PeseeSite:fnDeActivate");
            }
            return bolResult;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V3_Pesee_Get", (Guid)Id);
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
            return fnSelect("-1", null, null, "-1", -1, -1,-1);
        }

        public virtual List<DataPersist> fnSelect(string CampagneID, DateTime? StartDate, DateTime? EndDate, string status, int SiteID, int fournisseurID,int livraisonTypeID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V3_Pesee_Select");

                db().AddInParameter(mCommande, "@CampagneID", SqlDbType.Char, 9, CampagneID);
                db().AddInParameter(mCommande, "@siteID", SqlDbType.Int, SiteID);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, fournisseurID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@DateFin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, status);
                db().AddInParameter(mCommande, "@livraisonTypeID", SqlDbType.Int, livraisonTypeID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PeseeSite mClass = new PeseeSite();

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
                    mCommande = db().CreateStoredProcCommand("V3_Pesee_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.Int, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V3_Pesee_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@LivraisonID", SqlDbType.UniqueIdentifier, _Livraison.ID);
                db().AddInParameter(mCommande, "@SacTypeID", SqlDbType.Int, _SacType.ID);
                db().AddInParameter(mCommande, "@DatePesee", SqlDbType.DateTime, _DatePesee);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, Commentaire);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, PoidsNet);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, Statut);
                db().AddInParameter(mCommande, "@EstManuel", SqlDbType.Bit, IsManual);

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
                        ID = (Guid)db().Parameters(mCommande, "@ID");
                        if (_isnew)
                        {
                            NumeroPesee = (int)db().Parameters(mCommande, "@Numero");
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
                throw new Exception(ex.Message + "\r\n" + "PeseeSite:fnUpdate");

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
                    mCommande = db().CreateStoredProcCommand("V3_Pesee_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.Int, 10);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V3_Pesee_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }
                db().AddInParameter(mCommande, "@LivraisonID", SqlDbType.UniqueIdentifier, _Livraison.ID);
                db().AddInParameter(mCommande, "@SacTypeID", SqlDbType.Int, _SacType.ID);
                db().AddInParameter(mCommande, "@DatePesee", SqlDbType.DateTime, _DatePesee);
                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, Commentaire);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, NombreSacs);
                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Decimal, PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Decimal, TareSacs);
                db().AddInParameter(mCommande, "@TarePalette", SqlDbType.Decimal, TarePalette);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Decimal, PoidsNet);
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Char, 2, Statut);
                db().AddInParameter(mCommande, "@EstManuel", SqlDbType.Bit, IsManual);

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
                        ID = (Guid)db().Parameters(mCommande, "@ID");
                        if (_isnew)
                        {
                            NumeroPesee = (int)db().Parameters(mCommande, "@Numero");
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
                throw new Exception(ex.Message + "\r\n" + "PeseeSite:fnUpdate");

            }
            return Result;
        }


        private static void MapFromDataReader(PeseeSite mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass.ID = (Guid)mDataReader["ID"];

                    mClass.Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass.Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["SiteID"]))
                    {
                        mClass.Sites = new Site();
                        mClass.Sites.ID = (int)mDataReader["SiteID"];
                        mClass.Sites.Nom = (string)mDataReader["SiteNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["SacTypeID"]))
                    {
                        mClass.SacType = new SacType();
                        mClass.SacType.ID = (int)mDataReader["SacTypeID"];
                        mClass.SacType.Designation = (string)mDataReader["SacTypeDesignation"];
                        mClass.SacType.Tare = (decimal)mDataReader["TareUnitaireSacs"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["LivraisonID"]))
                    {
                        mClass.Livraison = new Livraison();
                        mClass.Livraison.ID = (Guid)mDataReader["LivraisonID"];                                                
                        mClass.Livraison.Numero = (string)mDataReader["LivraisonNumero"];                        
                        mClass.Livraison.Immatriculation = (string)mDataReader["LivraisonImmatriculation"];
                        mClass.Livraison.SacsDeclares = (int)mDataReader["SacDeclares"];
                        mClass.Livraison.PoidsDeclare = (decimal)mDataReader["PoidsDeclare"];
                        mClass.Livraison.TareSacs = (decimal)mDataReader["TareUnitaireSacs"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["fournisseurID"]))
                    {
                        mClass.Fournisseur = new Fournisseur();
                        mClass.Fournisseur.ID = (int)mDataReader["fournisseurID"];
                        mClass.Fournisseur.Nom = (string)mDataReader["fournisseurNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass.NumeroPesee = (int)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["DatePesee"])) mClass.DatePesee = (DateTime)mDataReader["DatePesee"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass.Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass.NombreSacs = (int)mDataReader["NombreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass.PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass.TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass.TarePalette = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass.PoidsNet = (decimal)mDataReader["PoidsNet"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass.Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["EstManuel"])) mClass.IsManual = (bool)mDataReader["EstManuel"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass.Statut = (string)mDataReader["Statut"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n PeseeSite:MapFromDataReader");
            }
        }


        public override string ToString()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public partial class PeseeSiteViewModel
    {
        public PeseeSite _PeseeSite { get; set; }

        public string _DefaultCampagne { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }


}
