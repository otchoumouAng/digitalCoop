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
    public class FicheLotCertifieLivraison : DataPersist
    {
        #region fields
        private Guid _ID;
        private FicheLotCertifie _FicheLotCertifie;        
        private BonDeLivraison _BonDeLivraison;
        private DateTime _DateFiche;
        private int _NombreSacsProduction;
        private int _NombreSacsLivraison;
        private int _mIcon;
        private bool _IsNewInList;
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

        public FicheLotCertifie FicheLotCertifie
        {
            get
            {
                return _FicheLotCertifie;
            }

            set
            {
                _FicheLotCertifie = value;
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

        public string NumeroFicheLot
        {
            get { return _FicheLotCertifie != null ? _FicheLotCertifie.Numero : string.Empty; }            
        }

        public string NoteNumero
        {
            get { return (_BonDeLivraison != null) ? _BonDeLivraison.Numero : string.Empty; }

        }
        
        public string LivraisonID
        {
            get { return (_BonDeLivraison != null && _BonDeLivraison.Livraison != null) ? _BonDeLivraison.Livraison.Numero : string.Empty; }

        }

        public string DateLivraisonAstring
        {
            get { return (_BonDeLivraison != null && _BonDeLivraison.Livraison != null) ? _BonDeLivraison.Livraison.DateLivraison.ToShortDateString() : string.Empty; }

        }
        
        public string LibelleCertification
        {
            get { return (_BonDeLivraison != null && _BonDeLivraison.Livraison != null && _BonDeLivraison.Livraison.Certification != null) ? _BonDeLivraison.Livraison.Certification.Designation : string.Empty; }

        }
        public string Immatriculation
        {
            get { return _BonDeLivraison != null && _BonDeLivraison.Livraison != null ? _BonDeLivraison.Livraison.Immatriculation : string.Empty; }

        }

        public string FournisseurNom
        {
            get { return _BonDeLivraison != null && _BonDeLivraison.Livraison != null && _BonDeLivraison.Livraison.Fournisseur != null ? _BonDeLivraison.Livraison.Fournisseur.Nom : string.Empty; }

        }
        public string PoidsNetAccepteAsString
        {
            get { return _BonDeLivraison != null && _BonDeLivraison.PoidsNetAccepte != 0 ? String.Format("{0:#,#}", _BonDeLivraison.PoidsNetAccepte).TrimStart() : string.Empty; }
        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_isnew)
                    return 0; // BulletCross                
                else
                    return 2; //                     
            }
            set { _mIcon = value; }
        }

        public DateTime DateFiche
        {
            get
            {
                return _DateFiche;
            }

            set
            {
                _DateFiche = value;
            }
        }

        public bool IsNewInList
        {
            get
            {
                return _IsNewInList;
            }

            set
            {
                _IsNewInList = value;
            }
        }

        public int NombreSacsProduction
        {
            get
            {
                return _NombreSacsProduction;
            }

            set
            {
                _NombreSacsProduction = value;
            }
        }

        public int NombreSacsLivraison
        {
            get
            {
                return _NombreSacsLivraison;
            }

            set
            {
                _NombreSacsLivraison = value;
            }
        }

        #endregion

        #region constructor
        public FicheLotCertifieLivraison()
        {

        }

        public FicheLotCertifieLivraison(Guid myId)
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
                mDataReader = db().ExecuteReader("V2_FicheLotCertifieLivraison_Get", (Guid)Id);
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
            return fnSelect(Guid.Empty);
        }

        public List<DataPersist> fnSelect(Guid FicheLotCertifieID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_FicheLotCertifieLivraison_SelectByFiche");
                db().AddInParameter(mCommande, "@FicheID", SqlDbType.UniqueIdentifier, FicheLotCertifieID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FicheLotCertifieLivraison mClass = new FicheLotCertifieLivraison();
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

        public List<DataPersist> fnSelectAvailable(Guid FicheLotCertifieID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_FicheLotCertifieLivraison_Select");
                db().AddInParameter(mCommande, "@FicheID", SqlDbType.UniqueIdentifier, FicheLotCertifieID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FicheLotCertifieLivraison mClass = new FicheLotCertifieLivraison();
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


        public List<DataPersist> fnSelectAvailableForWeighing(int FournisseurID, DateTime? StartDate, DateTime? EndDate, int certificationID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_BonDeLivraison_SelectForPeseeProduction");
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@certificationID", SqlDbType.Int, certificationID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FicheLotCertifieLivraison mClass = new FicheLotCertifieLivraison();
                    MapFromDataReaderLiv(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectAvailableForInvoice");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectAvailableForCertif(int FournisseurID, DateTime? StartDate, DateTime? EndDate, int certificationID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_BonDeLivraison_SelectForCertification");
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@certificationID", SqlDbType.Int, certificationID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    FicheLotCertifieLivraison mClass = new FicheLotCertifieLivraison();
                    MapFromDataReaderLiv(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectAvailableForInvoice");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }


        //public List<DataPersist> fnSelectAvailableForWeighing(Guid FicheID)
        //{
        //    List<DataPersist> mList = new List<DataPersist>();
        //    IDataReader mDataReader = null;

        //    try
        //    {
        //        DataCommand mCommande = db().CreateStoredProcCommand("V2_FicheLotCertifieLivraison_Select");
        //        db().AddInParameter(mCommande, "@FicheID", SqlDbType.UniqueIdentifier, FicheID);
        //        mDataReader = db().ExecuteReader(mCommande);

        //        while (mDataReader.Read())
        //        {
        //            FicheLotCertifieLivraison mClass = new FicheLotCertifieLivraison();
        //            MapFromDataReaderLiv(mClass, mDataReader);
        //            mList.Add(mClass);
        //        }
        //        return mList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectAvailableForWeighing");
        //    }
        //    finally
        //    {
        //        if (mDataReader != null) mDataReader.Close();
        //    }
        //}        


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
                    mCommande = db().CreateStoredProcCommand("V2_FicheLotCertifieLivraison_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_FicheLotCertifieLivraison_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@FicheLotCertifieID", SqlDbType.UniqueIdentifier, _FicheLotCertifie.ID);
                db().AddInParameter(mCommande, "@BonDeLivraisonID", SqlDbType.UniqueIdentifier, _BonDeLivraison.ID);
                db().AddInParameter(mCommande, "@DateFiche", SqlDbType.DateTime, DateTime.Now);
                db().AddInParameter(mCommande, "@NombreSacsProduction", SqlDbType.Int, _NombreSacsProduction);

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
                }
            }
            catch (Exception ex)
            {
                Result = false;
                throw new Exception(ex.Message + "\r\n" + "FicheLotCertifie:fnUpdate");

            }
            return Result;
        }

        public bool fnRemove()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_FicheLotCertifieLivraison_Remove");
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
                throw new Exception(ex.Message + "\r\n" + "FicheLotCertifieLivraison:fnRemove");
            }
            return Result;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        private static void MapFromDataReader(FicheLotCertifieLivraison mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;                    
                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass._FicheLotCertifie = new FicheLotCertifie();
                    if (!DBNull.Value.Equals(mDataReader["FicheLotCertifieID"])) mClass._FicheLotCertifie.ID = (Guid)mDataReader["FicheLotCertifieID"];
                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._FicheLotCertifie.Numero = (string)mDataReader["Numero"];

                    if (!DBNull.Value.Equals(mDataReader["BonDeLivraisonID"]))
                    {
                        Livraison mLivraison = new Livraison();
                        mClass.BonDeLivraison = new BonDeLivraison();
                        mClass.BonDeLivraison.Livraison = new Livraison();
                        mClass.BonDeLivraison.Livraison.Certification = new Certification();                                                                        

                        mClass.BonDeLivraison.ID = (Guid)mDataReader["BonDeLivraisonID"];

                        if (!DBNull.Value.Equals(mDataReader["NumeroBL"])) mClass.BonDeLivraison.Numero = (string)mDataReader["NumeroBL"];
                        if (!DBNull.Value.Equals(mDataReader["NbreSacsLivraison"])) mClass.BonDeLivraison.NbreSacs = (int)mDataReader["NbreSacsLivraison"];
                        if (!DBNull.Value.Equals(mDataReader["NbreSacsLivraison"])) mClass._NombreSacsLivraison = (int)mDataReader["NbreSacsLivraison"];
                        if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass.BonDeLivraison.PoidsNetAccepte = (decimal)mDataReader["PoidsNet"];

                        if (!DBNull.Value.Equals(mDataReader["NumeroLivraison"])) mLivraison.Numero = (string)mDataReader["NumeroLivraison"];
                        if (!DBNull.Value.Equals(mDataReader["DateLivraison"])) mLivraison.DateLivraison = (DateTime)mDataReader["DateLivraison"];
                        if (!DBNull.Value.Equals(mDataReader["LivraisonImmatriculation"])) mLivraison.Immatriculation = (string)mDataReader["LivraisonImmatriculation"];

                        mLivraison.Fournisseur = new Fournisseur();
                        if (!DBNull.Value.Equals(mDataReader["FournisseurID"])) mLivraison.Fournisseur.ID = (int)mDataReader["FournisseurID"];
                        if (!DBNull.Value.Equals(mDataReader["FournisseurNom"])) mLivraison.Fournisseur.Nom = (string)mDataReader["FournisseurNom"];

                        mLivraison.Campagne = new Campagne();
                        mLivraison.Certification = new Certification();
                        mLivraison.Exportateur = new Exportateur();
                        mLivraison.SacType = new SacType();

                        //if (!DBNull.Value.Equals(mDataReader["CertificationID"])) mLivraison.Certification.ID = (int)mDataReader["CertificationID"];
                        //if (!DBNull.Value.Equals(mDataReader["SacTypeID"])) mLivraison.SacType.ID = (int)mDataReader["SacTypeID"];
                        //if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mLivraison.Campagne.Designation = (string)mDataReader["CampagneID"];
                        //if (!DBNull.Value.Equals(mDataReader["ExportateurID"])) mLivraison.Exportateur.ID = (int)mDataReader["ExportateurID"];
                        //if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass.BonDeLivraison.PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                        //if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass.BonDeLivraison.TareSacs = (decimal)mDataReader["TareSacs"];
                        //if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass.BonDeLivraison.TarePalettes = (decimal)mDataReader["TarePalettes"];
                        //if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass.BonDeLivraison.PoidsNetAccepte = (decimal)mDataReader["PoidsNet"];
                        //if (!DBNull.Value.Equals(mDataReader["RefactionBrisures"])) mClass.BonDeLivraison.RefactionBrisures = (decimal)mDataReader["RefactionBrisures"];
                        //if (!DBNull.Value.Equals(mDataReader["RefactionHumidite"])) mClass.BonDeLivraison.RefactionHumidite = (decimal)mDataReader["RefactionHumidite"];
                        //if (!DBNull.Value.Equals(mDataReader["RefactionMatieresEtg"])) mClass.BonDeLivraison.RefactionMatieresEtg = (decimal)mDataReader["RefactionMatieresEtg"];

                        mClass.BonDeLivraison.Livraison = mLivraison;                                                                                            
                    }

                    if (!DBNull.Value.Equals(mDataReader["DateFiche"])) mClass._DateFiche = (DateTime)mDataReader["DateFiche"];
                    if (!DBNull.Value.Equals(mDataReader["NombreSacsFicheProduction"])) mClass._NombreSacsProduction = (int)mDataReader["NombreSacsFicheProduction"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];                    

                    mClass._isnew = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n FicheLotCertifieLivraison:MapFromDataReader");
            }
        }


        private static void MapFromDataReaderLiv(FicheLotCertifieLivraison mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;
                    if (!DBNull.Value.Equals(mDataReader["BonDeLivraisonID"])) mClass._ID = (Guid)mDataReader["BonDeLivraisonID"];                    

                    if (!DBNull.Value.Equals(mDataReader["BonDeLivraisonID"]))
                    {
                        Livraison mLivraison = new Livraison();
                        mClass.BonDeLivraison = new BonDeLivraison();
                        mLivraison.Campagne = new Campagne();
                        mLivraison.Certification = new Certification();
                        mLivraison.Exportateur = new Exportateur();
                        mLivraison.SacType = new SacType();

                        mClass.BonDeLivraison.ID = (Guid)mDataReader["BonDeLivraisonID"];
                        if (!DBNull.Value.Equals(mDataReader["NumeroBL"])) mClass.BonDeLivraison.Numero = (string)mDataReader["NumeroBL"];
                        if (!DBNull.Value.Equals(mDataReader["NbreSacs"])) mClass.BonDeLivraison.NbreSacs = (int)mDataReader["NbreSacs"];
                        if (!DBNull.Value.Equals(mDataReader["NbreSacs"])) mClass._NombreSacsLivraison = (int)mDataReader["NbreSacs"];
                        if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass.BonDeLivraison.PoidsNetAccepte = (decimal)mDataReader["PoidsNet"];

                        if (!DBNull.Value.Equals(mDataReader["NumeroLivraison"])) mLivraison.Numero = (string)mDataReader["NumeroLivraison"];
                        if (!DBNull.Value.Equals(mDataReader["DateLivraison"])) mLivraison.DateLivraison = (DateTime)mDataReader["DateLivraison"];
                        if (!DBNull.Value.Equals(mDataReader["LivraisonImmatriculation"])) mLivraison.Immatriculation = (string)mDataReader["LivraisonImmatriculation"];

                        mLivraison.Fournisseur = new Fournisseur();
                        if (!DBNull.Value.Equals(mDataReader["FournisseurID"])) mLivraison.Fournisseur.ID = (int)mDataReader["FournisseurID"];
                        if (!DBNull.Value.Equals(mDataReader["FournisseurNom"])) mLivraison.Fournisseur.Nom = (string)mDataReader["FournisseurNom"];

                        if (!DBNull.Value.Equals(mDataReader["CertificationID"])) mLivraison.Certification.ID = (int)mDataReader["CertificationID"];
                        if (!DBNull.Value.Equals(mDataReader["SacTypeID"])) mLivraison.SacType.ID = (int)mDataReader["SacTypeID"];
                        if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mLivraison.Campagne.Designation = (string)mDataReader["CampagneID"];
                        if (!DBNull.Value.Equals(mDataReader["ExportateurID"])) mLivraison.Exportateur.ID = (int)mDataReader["ExportateurID"];
                        if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass.BonDeLivraison.PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                        if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass.BonDeLivraison.TareSacs = (decimal)mDataReader["TareSacs"];
                        if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass.BonDeLivraison.TarePalettes = (decimal)mDataReader["TarePalettes"];
                        if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass.BonDeLivraison.PoidsNetAccepte = (decimal)mDataReader["PoidsNet"];
                        if (!DBNull.Value.Equals(mDataReader["RefactionBrisures"])) mClass.BonDeLivraison.RefactionBrisures = (decimal)mDataReader["RefactionBrisures"];
                        if (!DBNull.Value.Equals(mDataReader["RefactionHumidite"])) mClass.BonDeLivraison.RefactionHumidite = (decimal)mDataReader["RefactionHumidite"];
                        if (!DBNull.Value.Equals(mDataReader["RefactionMatieresEtg"])) mClass.BonDeLivraison.RefactionMatieresEtg = (decimal)mDataReader["RefactionMatieresEtg"];

                        mClass.BonDeLivraison.Livraison = mLivraison;
                    }                    

                    mClass._isnew = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n FicheLotCertifieLivraison:MapFromDataReaderLiv");
            }
        }


        #endregion
    }

    public partial class FicheLotCertifieLivraisonViewModel
    {
        public FicheLotCertifieLivraison _FicheLotCertifieLivraison { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
