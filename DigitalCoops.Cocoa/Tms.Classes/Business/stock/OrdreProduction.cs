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
    public class OrdreProduction : DataPersist
    {
        #region Fields
        private Guid _ID;
        private Campagne _Campagne;
        private Usine _Usine;
        private Exportateur _Exportateur;
        private Certification _Certification;
        private PoleProvenance _PoleProvenance;
        private AgentProduction _Quart;
        private AgentProduction _ChefQuart;
        private AgentProduction _Melangeur;
        private string _NumeroProduction;
        private bool _EstReusine;
        private bool _Desactive;
        private string _Approbateur;
        private DateTime? _DateApprobation;
        private DateTime _DateProduction;
        private DateTime? _DateDebutProduction;
        private DateTime? _DateFinProduction;
        private string _ClotureUtilisateur;
        private DateTime? _DateCloture;
        private string _Statut;
        private string _Commentaire;
        private int _NombreSacs;
        private decimal _PoidsNet;
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

        public string CampagneAsString
        {
            get
            {
                return _Campagne != null ? _Campagne.Designation : string.Empty;
            }


        }

        public Exportateur Exportateur
        {
            get
            {
                return _Exportateur;
            }

            set
            {
                _Exportateur = value;
            }
        }

        public string ExportateurAsString
        {
            get
            {
                return _Exportateur != null ? _Exportateur.Nom : string.Empty;
            }
        }

        public Certification Certification
        {
            get
            {
                return _Certification;
            }

            set
            {
                _Certification = value;
            }
        }

        public string CertificationAsString
        {
            get
            {
                return _Certification != null ? _Certification.Designation : string.Empty;
            }
        }

        public PoleProvenance PoleProvenance
        {
            get
            {
                return _PoleProvenance;
            }

            set
            {
                _PoleProvenance = value;
            }
        }

        public string PoleProvenanceAsString
        {
            get
            {
                return _PoleProvenance != null ? _PoleProvenance.Designation : string.Empty;
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

        public string QuartAsString
        {
            get
            {
                return _Quart != null ? _Quart.Nom : string.Empty;
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

        public string ChefQuartAsString
        {
            get
            {
                return _ChefQuart != null ? _ChefQuart.Nom : string.Empty;
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

        public string MelangeurAsString
        {
            get
            {
                return _Melangeur != null ? _Melangeur.Nom : string.Empty;
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

        public bool EstReusine
        {
            get
            {
                return _EstReusine;
            }

            set
            {
                _EstReusine = value;
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

        public DateTime? DateCloture
        {
            get
            {
                return _DateCloture;
            }

            set
            {
                _DateCloture = value;
            }
        }

        public string DateClotureAsString
        {
            get
            {
                return _DateCloture != null ? _DateCloture.Value.ToShortDateString() : string.Empty;
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

        [Column(Text = "")]
        public string mIcon
        {
            get
            {
                if (_Desactive || _Statut == "CA")
                    return "0"; // BulletCross
                else if (_Statut == "AP")
                    return "1"; // Tick
                else if (_Statut == "NA")
                    return "2";
                else if (_Statut == "CL")
                    return "3";
                else
                    return "2";
            }
        }

        public DateTime? DateApprobation
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

        public string ClotureUtilisateur
        {
            get
            {
                return _ClotureUtilisateur;
            }

            set
            {
                _ClotureUtilisateur = value;
            }
        }

        public DateTime DateProduction
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
                return _DateProduction != null ? _DateProduction.ToShortDateString() : string.Empty;
            }
        }

        public Usine Usine
        {
            get
            {
                return _Usine;
            }

            set
            {
                _Usine = value;
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
            get { return _PoidsNet != 0 ? String.Format("{0:#,#}", _PoidsNet).TrimStart() : string.Empty; }
        }

        public bool IsClosed
        {
            get
            {
                return _Statut == "CL";
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
            DataCommand mCommande = db().CreateStoredProcCommand("V2_OrdreProduction_DeActivate");
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
                mDataReader = db().ExecuteReader("V2_OrdreProduction_GetByNumber", (string)Numero);
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
                mDataReader = db().ExecuteReader("V2_OrdreProduction_Get", (Guid)Id);
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
            return fnSelect( -1, -1, -1, null, null, "-1",-1);
        }

        public List<DataPersist> fnSelect(int quartID, int MelangeurID, int CertificationID, DateTime? StartDate, DateTime? EndDate,string statut, int otherStatut = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_OrdreProduction_Select");
                db().AddInParameter(mCommande, "@quartID", SqlDbType.Int, quartID);
                db().AddInParameter(mCommande, "@melangeurID", SqlDbType.Int, MelangeurID);
                db().AddInParameter(mCommande, "@certificationID", SqlDbType.Int, CertificationID);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@datefin", SqlDbType.DateTime, EndDate);
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
                DataCommand mCommande = db().CreateStoredProcCommand("V2_OrdreProduction_SelectOpenned");
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
                    mCommande = db().CreateStoredProcCommand("V2_OrdreProduction_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Numero", SqlDbType.Char,9);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_OrdreProduction_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@campagneID", SqlDbType.Char,9, _Campagne.Designation);
                db().AddInParameter(mCommande, "@ExportateurID", SqlDbType.Int, _Exportateur.ID);
                //db().AddInParameter(mCommande, "@ProvenanceFevesID", SqlDbType.Int, _ProvenanceFeves.ID);

                //if (_ProvenanceFeves != null)
                //    db().AddInParameter(mCommande, "@ProvenanceFevesID", SqlDbType.Int, _ProvenanceFeves.ID);
                //else
                //    db().AddInParameter(mCommande, "@ProvenanceFevesID", SqlDbType.Int, DBNull.Value);

                if (_PoleProvenance != null)
                    db().AddInParameter(mCommande, "@PoleID", SqlDbType.Int, _PoleProvenance.ID);
                else
                    db().AddInParameter(mCommande, "@PoleID", SqlDbType.Int, DBNull.Value);

                if (_Quart != null)
                    db().AddInParameter(mCommande, "@QuartID", SqlDbType.Int, _Quart.ID);
                else
                    db().AddInParameter(mCommande, "@QuartID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@ChefQuartID", SqlDbType.Int, _ChefQuart.ID);
                db().AddInParameter(mCommande, "@MelangeurID", SqlDbType.Int, _Melangeur.ID);

                if (Certification != null)
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, _Certification.ID);
                else
                    db().AddInParameter(mCommande, "@CertificationID", SqlDbType.Int, DBNull.Value);

                db().AddInParameter(mCommande, "@DateProduction", SqlDbType.DateTime, _DateProduction);
                db().AddInParameter(mCommande, "@EstReusine", SqlDbType.Bit, _EstReusine);
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
                mCommande = db().CreateStoredProcCommand("V2_OrdreProduction_Close");
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

                    mClass._Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass._Campagne.Designation = (string)mDataReader["CampagneID"];

                    if (!DBNull.Value.Equals(mDataReader["ExportateurID"]))
                    {
                        mClass._Exportateur = new Exportateur();
                        mClass._Exportateur.ID = (int)mDataReader["ExportateurID"];
                        mClass._Exportateur.Nom = (string)mDataReader["ExportateurNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["CertificationID"]))
                    {
                        mClass._Certification = new Certification();
                        mClass._Certification.ID = (int)mDataReader["CertificationID"];
                        mClass._Certification.Designation = (string)mDataReader["CertificationDesignation"];
                    }

                    //if (!DBNull.Value.Equals(mDataReader["ProvenanceFevesID"]))
                    //{
                    //    mClass._ProvenanceFeves = new ProvenanceFeves();
                    //    mClass._ProvenanceFeves.ID = (int)mDataReader["ProvenanceFevesID"];
                    //    mClass._ProvenanceFeves.Designation = (string)mDataReader["ProvenanceFevesDesignation"];
                    //}

                    if (!DBNull.Value.Equals(mDataReader["PoleProvenanceID"]))
                    {
                        mClass._PoleProvenance = new PoleProvenance();
                        mClass._PoleProvenance.ID = (int)mDataReader["PoleProvenanceID"];
                        mClass._PoleProvenance.Designation = (string)mDataReader["PoleProvenanceDesignation"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["QuartID"]))
                    {
                        mClass._Quart = new AgentProduction();
                        mClass._Quart.ID = (int)mDataReader["QuartID"];
                        mClass._Quart.Nom = (string)mDataReader["QuartNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ChefQuartID"]))
                    {
                        mClass._ChefQuart = new AgentProduction();
                        mClass._ChefQuart.ID = (int)mDataReader["ChefQuartID"];
                        mClass._ChefQuart.Nom = (string)mDataReader["ChefQuartNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["MelangeurID"]))
                    {
                        mClass._Melangeur = new AgentProduction();
                        mClass._Melangeur.ID = (int)mDataReader["MelangeurID"];
                        mClass._Melangeur.Nom = (string)mDataReader["MelangeurNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["DateProduction"])) mClass._DateProduction = (DateTime)mDataReader["DateProduction"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroProduction"])) mClass._NumeroProduction = (string)mDataReader["NumeroProduction"];
                    if (!DBNull.Value.Equals(mDataReader["EstReusine"])) mClass._EstReusine = (bool)mDataReader["EstReusine"];

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];

                    if (!DBNull.Value.Equals(mDataReader["DateApprobation"])) mClass._DateApprobation = (DateTime)mDataReader["DateApprobation"];
                    if (!DBNull.Value.Equals(mDataReader["Approbateur"])) mClass._Approbateur = (string)mDataReader["Approbateur"];
                    if (!DBNull.Value.Equals(mDataReader["DateFin"])) mClass._DateFinProduction = (DateTime)mDataReader["DateFin"];
                    if (!DBNull.Value.Equals(mDataReader["ClotureUtilisateur"])) mClass._ClotureUtilisateur = (string)mDataReader["ClotureUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["DateCloture"])) mClass._DateCloture = (DateTime)mDataReader["DateCloture"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];

                    if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass._NombreSacs = (int)mDataReader["NombreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNet = (decimal)mDataReader["PoidsNet"];

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

                    mClass._Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass._Campagne.Designation = (string)mDataReader["CampagneID"];
                    if (!DBNull.Value.Equals(mDataReader["DateProduction"])) mClass._DateProduction = (DateTime)mDataReader["DateProduction"];


                    if (!DBNull.Value.Equals(mDataReader["QuartID"]))
                    {
                        mClass._Quart = new AgentProduction();
                        mClass._Quart.ID = (int)mDataReader["QuartID"];
                        mClass._Quart.Nom = (string)mDataReader["QuartNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["ChefQuartID"]))
                    {
                        mClass._ChefQuart = new AgentProduction();
                        mClass._ChefQuart.ID = (int)mDataReader["ChefQuartID"];
                        mClass._ChefQuart.Nom = (string)mDataReader["ChefQuartNom"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["MelangeurID"]))
                    {
                        mClass._Melangeur = new AgentProduction();
                        mClass._Melangeur.ID = (int)mDataReader["MelangeurID"];
                        mClass._Melangeur.Nom = (string)mDataReader["MelangeurNom"];
                    }


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
