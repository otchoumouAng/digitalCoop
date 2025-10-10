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
    public class CompositionUsinageLivraison : DataPersist
    {
        #region fields
        private Guid _ID;
        private CompositionUsinage _CompositionUsinage;
        private Melange _Melange;
        private BonDeLivraison _BonDeLivraison;
        private int _NombreSacs;
        private decimal _PoidsBrut;
        private decimal _PoidsNet;
        private double _Humidite;
        private int _Grainage;
        private double _Moisi;
        private double _Mite;
        private double _Defective;
        private double _Ardoisee;
        private double _Ffa;
        private double _MatiereEtrangere;
        private double _Sievings;
        private double _Fragment;
        private double _Fermentation;
        private ClassificationFeves _ClassificationFeves;
        private double _CRM;
        private string _MelangeAsString;
        private int? _MelangeID;
        private int _mIcon;
        private int _NbreSacsTotalLivraison;

        private decimal _TareSacs;
        private decimal _PoidsLivre;
        private decimal _TarePalettes;
        private decimal _Retention;
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

        public CompositionUsinage CompositionUsinage
        {
            get
            {
                return _CompositionUsinage;
            }

            set
            {
                _CompositionUsinage = value;
            }
        }

        public Melange Melange
        {
            get
            {
                return _Melange;
            }

            set
            {
                _Melange = value;
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

        public double Humidite
        {
            get
            {
                return _Humidite;
            }

            set
            {
                _Humidite = value;
            }
        }

        public int Grainage
        {
            get
            {
                return _Grainage;
            }

            set
            {
                _Grainage = value;
            }
        }

        public double Moisi
        {
            get
            {
                return _Moisi;
            }

            set
            {
                _Moisi = value;
            }
        }

        public double Mite
        {
            get
            {
                return _Mite;
            }

            set
            {
                _Mite = value;
            }
        }

        public double Defective
        {
            get
            {
                return _Defective;
            }

            set
            {
                _Defective = value;
            }
        }

        public double Ardoisee
        {
            get
            {
                return _Ardoisee;
            }

            set
            {
                _Ardoisee = value;
            }
        }

        public double Ffa
        {
            get
            {
                return _Ffa;
            }

            set
            {
                _Ffa = value;
            }
        }

        public double MatiereEtrangere
        {
            get
            {
                return _MatiereEtrangere;
            }

            set
            {
                _MatiereEtrangere = value;
            }
        }

        public double Fragment
        {
            get
            {
                return _Fragment;
            }

            set
            {
                _Fragment = value;
            }
        }

        public double Fermentation
        {
            get
            {
                return _Fermentation;
            }

            set
            {
                _Fermentation = value;
            }
        }

        public ClassificationFeves ClassificationFeves
        {
            get
            {
                return _ClassificationFeves;
            }

            set
            {
                _ClassificationFeves = value;
            }
        }

        public double CRM
        {
            get
            {
                return _CRM;
            }

            set
            {
                _CRM = value;
            }
        }

        public double Sievings
        {
            get
            {
                return _Sievings;
            }

            set
            {
                _Sievings = value;
            }
        }

        public string MelangeAsString
        {
            get { return _Melange != null ? _Melange.Designation : string.Empty; }
            set { _MelangeAsString = value; }
        }

        public string LibelleTypeDeLivraison
        {
            get { return (_BonDeLivraison != null && _BonDeLivraison.Livraison != null && _BonDeLivraison.Livraison.LivraisonType != null) ? _BonDeLivraison.Livraison.LivraisonType.Designation : string.Empty; }

        }
        public string LivraisonID
        {
            get { return (_BonDeLivraison != null && _BonDeLivraison.Livraison != null) ? _BonDeLivraison.Livraison.Numero : string.Empty; }

        }

        public string DateLivraisonAstring
        {
            get { return (_BonDeLivraison != null && _BonDeLivraison.Livraison != null) ? _BonDeLivraison.Livraison.DateLivraison.ToShortDateString() : string.Empty; }

        }
        //public string LibelleTypeDeSac
        //{
        //    get { return (_Livraison != null && _Livraison.SacType != null) ? _Livraison.SacType.Designation : string.Empty; }

        //}

        public string LibelleCertification
        {
            get { return (_BonDeLivraison != null && _BonDeLivraison.Livraison != null && _BonDeLivraison.Livraison.Certification != null) ? _BonDeLivraison.Livraison.Certification.Designation : string.Empty; }

        }
        public string Immatriculation
        {
            get { return _BonDeLivraison != null && _BonDeLivraison.Livraison != null ? _BonDeLivraison.Livraison.Immatriculation : string.Empty; }

        }

        public int? MelangeID
        {
            get
            {
                return _MelangeID;
            }

            set
            {
                _MelangeID = value;
            }
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

        public int NbreSacsTotalLivraison
        {
            get
            {
                return _NbreSacsTotalLivraison;
            }

            set
            {
                _NbreSacsTotalLivraison = value;
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

        public decimal TarePalettes
        {
            get
            {
                return _TarePalettes;
            }

            set
            {
                _TarePalettes = value;
            }
        }

        public decimal Retention
        {
            get
            {
                return _Retention;
            }

            set
            {
                _Retention = value;
            }
        }

        public decimal PoidsLivre
        {
            get
            {
                return _PoidsLivre;
            }

            set
            {
                _PoidsLivre = value;
            }
        }

        public string PoidsLivreAsString
        {
            get
            {
                return _PoidsLivre != 0 ? string.Format("{0:#,#}", _PoidsLivre).TrimStart() : "0";
            }
        }

        #endregion

        #region constructor
        public CompositionUsinageLivraison()
        {

        }

        public CompositionUsinageLivraison(Guid myId)
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
                mDataReader = db().ExecuteReader("V2_CompositionUsinageLivraison_Get", (Guid)Id);
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

        public List<DataPersist> fnSelect(Guid CompositionUsinageID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_CompositionUsinageLivraison_Select");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, CompositionUsinageID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    CompositionUsinageLivraison mClass = new CompositionUsinageLivraison();
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
        public List<DataPersist> fnSelectAvailableForProduction(int CertificationID, int LivraisonTypeID, DateTime? StartDate, DateTime? EndDate, string campagne)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_BonDeLivraison_SelectForProduction");
                db().AddInParameter(mCommande, "@LivraisonTypeID", SqlDbType.Int, LivraisonTypeID);
                db().AddInParameter(mCommande, "@CertificationId", SqlDbType.Int, CertificationID);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, campagne);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    CompositionUsinageLivraison mClass = new CompositionUsinageLivraison();
                    MapFromDataReaderBL(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectAvailableForProduction");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectAvailableDeliveries(Guid productionID, Guid? peseeID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V2_CompositionUsinageLivraison_SelectLivraisons");
                db().AddInParameter(mCommande, "@productionID", SqlDbType.UniqueIdentifier, productionID);
                db().AddInParameter(mCommande, "@peseeID", SqlDbType.UniqueIdentifier, peseeID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    CompositionUsinageLivraison mClass = new CompositionUsinageLivraison();
                    MapFromDataReaderLiv(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectAvailableDeliveries");
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
                    mCommande = db().CreateStoredProcCommand("V2_CompositionUsinageLivraison_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V2_CompositionUsinageLivraison_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CompositionUsinageID", SqlDbType.UniqueIdentifier, _CompositionUsinage.ID);
                db().AddInParameter(mCommande, "@BonDeLivraisonID", SqlDbType.UniqueIdentifier, _BonDeLivraison.ID);
                db().AddInParameter(mCommande, "@MelangeID", SqlDbType.Int, _Melange.ID);
                db().AddInParameter(mCommande, "@NombreSacs", SqlDbType.Int, _NombreSacs);
                db().AddInParameter(mCommande, "@Humidite", SqlDbType.Float, _Humidite);
                db().AddInParameter(mCommande, "@Grainage", SqlDbType.Int, _Grainage);
                db().AddInParameter(mCommande, "@Moisi", SqlDbType.Float, _Moisi);
                db().AddInParameter(mCommande, "@Mite", SqlDbType.Float, _Mite);
                db().AddInParameter(mCommande, "@Ffa", SqlDbType.Float, _Ffa);
                db().AddInParameter(mCommande, "@Slaty", SqlDbType.Float, _Ardoisee);
                db().AddInParameter(mCommande, "@Dechets", SqlDbType.Float, _Sievings);
                db().AddInParameter(mCommande, "@MatiereEtrangere", SqlDbType.Float, _MatiereEtrangere);

                db().AddInParameter(mCommande, "@PoidsBrut", SqlDbType.Float, _PoidsBrut);
                db().AddInParameter(mCommande, "@TareSacs", SqlDbType.Float, _TareSacs);
                db().AddInParameter(mCommande, "@TarePalettes", SqlDbType.Float, _TarePalettes);
                db().AddInParameter(mCommande, "@PoidsLivre", SqlDbType.Float, _PoidsLivre);
                db().AddInParameter(mCommande, "@Retention", SqlDbType.Float, _Retention);
                db().AddInParameter(mCommande, "@PoidsNet", SqlDbType.Float, _PoidsNet);

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
                throw new Exception(ex.Message + "\r\n" + "CompositionUsinage:fnUpdate");

            }
            return Result;
        }

        public bool fnRemove()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("V2_CompositionUsinageLivraison_Remove");
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
                throw new Exception(ex.Message + "\r\n" + "CompositionUsinageLivraison:fnRemove");
            }
            return Result;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        private static void MapFromDataReader(CompositionUsinageLivraison mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;                    
                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    mClass.Melange = new Melange();
                    if (!DBNull.Value.Equals(mDataReader["MelangeID"])) mClass._Melange.ID = (int)mDataReader["MelangeID"];
                    if (!DBNull.Value.Equals(mDataReader["MelangeDesignation"])) mClass.Melange.Designation = (string)mDataReader["MelangeDesignation"];

                    if (!DBNull.Value.Equals(mDataReader["blID"]))
                    {
                        Livraison mLivraison = new Livraison();

                        mClass.BonDeLivraison = new BonDeLivraison();
                        mClass.BonDeLivraison.Livraison = new Livraison();
                        mClass.BonDeLivraison.Livraison.Certification = new Certification();
                        mClass.BonDeLivraison.Livraison.LivraisonType = new LivraisonType();
                        mClass.BonDeLivraison.Livraison.SacType = new SacType();
                        mClass.BonDeLivraison.Livraison.Exportateur = new Exportateur();

                        mClass.BonDeLivraison.ID = (Guid)mDataReader["blID"];

                        //if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass.BonDeLivraison.TareSacsAjustee = (decimal)mDataReader["TareSacs"];
                        //if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass.BonDeLivraison.TarePalettesAjustee = (decimal)mDataReader["TarePalettes"];
                        //if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass.BonDeLivraison.PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                        //if (!DBNull.Value.Equals(mDataReader["RefactionBrisures"])) mClass.BonDeLivraison.RefactionBrisures = (decimal)mDataReader["RefactionBrisures"];
                        //if (!DBNull.Value.Equals(mDataReader["RefactionHumidite"])) mClass.BonDeLivraison.RefactionHumidite = (decimal)mDataReader["RefactionHumidite"];
                        //if (!DBNull.Value.Equals(mDataReader["RefactionMatieresEtg"])) mClass.BonDeLivraison.RefactionMatieresEtg = (decimal)mDataReader["RefactionMatieresEtg"];
                        //if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass.BonDeLivraison.PoidsNetAccepte = (decimal)mDataReader["PoidsNet"];

                        mLivraison.Campagne = new Campagne();
                        if (!DBNull.Value.Equals(mDataReader["LivraisonGuid"])) mLivraison.ID = (Guid)mDataReader["LivraisonGuid"];
                        if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mLivraison.Campagne.Designation = (string)mDataReader["CampagneID"];
                        if (!DBNull.Value.Equals(mDataReader["LivraisonID"])) mLivraison.Numero = (string)mDataReader["LivraisonID"];
                        if (!DBNull.Value.Equals(mDataReader["DateLivraison"])) mLivraison.DateLivraison = (DateTime)mDataReader["DateLivraison"];
                        if (!DBNull.Value.Equals(mDataReader["Immatriculation"])) mLivraison.Immatriculation = (string)mDataReader["Immatriculation"];
                        mClass.BonDeLivraison.Livraison = mLivraison;

                        LivraisonType mLivraisonType = new LivraisonType();
                        if (!DBNull.Value.Equals(mDataReader["TypeLivraisonID"]))
                        {
                            mLivraisonType.ID = (int)mDataReader["TypeLivraisonID"];
                            mLivraisonType.Designation = (string)mDataReader["TypeLivraisonNom"];
                            mLivraison.LivraisonType = mLivraisonType;
                        }
                        mClass.BonDeLivraison.Livraison.LivraisonType = mLivraisonType;
                        
                        Certification mCertification = new Certification();
                        if (!DBNull.Value.Equals(mDataReader["CertificationID"]))
                        {
                            mCertification.ID = (int)mDataReader["CertificationID"];
                            mCertification.Designation = (string)mDataReader["CertificationNom"];
                            mLivraison.Certification = mCertification;
                        }
                        mClass.BonDeLivraison.Livraison.Certification = mCertification;

                        Exportateur exportateur = new Exportateur();

                        if (!DBNull.Value.Equals(mDataReader["ExportateurID"])) exportateur.ID = (int)mDataReader["ExportateurID"];
                        mLivraison.Exportateur = exportateur;
                        mClass.BonDeLivraison.Livraison.Exportateur = exportateur;

                        SacType sacType = new SacType();
                        if (!DBNull.Value.Equals(mDataReader["SacTypeID"])) sacType.ID = (int)mDataReader["SacTypeID"];
                        mLivraison.SacType = sacType;
                        mClass.BonDeLivraison.Livraison.SacType = sacType;
                    }

                    if (!DBNull.Value.Equals(mDataReader["NbreSacs"])) mClass._NombreSacs = (int)mDataReader["NbreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["Grainage"])) mClass._Grainage = (int)mDataReader["Grainage"];

                    if (!DBNull.Value.Equals(mDataReader["Humidite"])) mClass._Humidite = (double)mDataReader["Humidite"];
                    if (!DBNull.Value.Equals(mDataReader["MatiereEtrangere"])) mClass._MatiereEtrangere = (double)mDataReader["MatiereEtrangere"];
                    if (!DBNull.Value.Equals(mDataReader["Mitee"])) mClass._Mite = (double)mDataReader["Mitee"];
                    if (!DBNull.Value.Equals(mDataReader["Moisie"])) mClass._Moisi = (double)mDataReader["Moisie"];
                    if (!DBNull.Value.Equals(mDataReader["Ardoisee"])) mClass._Ardoisee = (double)mDataReader["Ardoisee"];
                    if (!DBNull.Value.Equals(mDataReader["Dechets"])) mClass._Sievings = (double)mDataReader["Dechets"];
                    if (!DBNull.Value.Equals(mDataReader["Ffa"])) mClass._Ffa = (double)mDataReader["Ffa"];

                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass.TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass.TarePalettes = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass.PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["RetentionPoids"])) mClass.Retention = (decimal)mDataReader["RetentionPoids"];                    
                    if (!DBNull.Value.Equals(mDataReader["PoidsLivre"])) mClass.PoidsLivre = (decimal)mDataReader["PoidsLivre"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass.PoidsNet = (decimal)mDataReader["PoidsNet"];

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
                throw new Exception(ex.Message + "\n CompositionUsinageLivraison:MapFromDataReader");
            }
        }


        private static void MapFromDataReaderBL(CompositionUsinageLivraison mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;
                    mClass._ID = Guid.NewGuid();
                    //if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["blID"]))
                    {
                        Livraison mLivraison = new Livraison();
                        
                        mClass.BonDeLivraison = new BonDeLivraison();
                        mClass.BonDeLivraison.Livraison = new Livraison();
                        mClass.BonDeLivraison.Livraison.Certification = new Certification();
                        mClass.BonDeLivraison.Livraison.LivraisonType = new LivraisonType();

                        mClass.BonDeLivraison.ID = (Guid)mDataReader["blID"];
                        if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass.BonDeLivraison.PoidsNetAccepte = (decimal)mDataReader["PoidsNet"];
                        if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass.BonDeLivraison.PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                        if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass.BonDeLivraison.TareSacs = (decimal)mDataReader["TareSacs"];
                        if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass.BonDeLivraison.TarePalettesAjustee = (decimal)mDataReader["TarePalettes"];
                        if (!DBNull.Value.Equals(mDataReader["RefactionBrisures"])) mClass.BonDeLivraison.RefactionBrisures = (decimal)mDataReader["RefactionBrisures"];
                        if (!DBNull.Value.Equals(mDataReader["RefactionHumidite"])) mClass.BonDeLivraison.RefactionHumidite = (decimal)mDataReader["RefactionHumidite"];
                        if (!DBNull.Value.Equals(mDataReader["RefactionMatiereEtg"])) mClass.BonDeLivraison.RefactionMatieresEtg = (decimal)mDataReader["RefactionMatiereEtg"];
                        if (!DBNull.Value.Equals(mDataReader["MatiereEtg"])) mClass.BonDeLivraison.MatieresEtrangeres = (double)mDataReader["MatiereEtg"];

                        if (!DBNull.Value.Equals(mDataReader["LivraisonGuid"])) mLivraison.ID = (Guid)mDataReader["LivraisonGuid"];
                        if (!DBNull.Value.Equals(mDataReader["LivraisonID"])) mLivraison.Numero = (string)mDataReader["LivraisonID"];
                        if (!DBNull.Value.Equals(mDataReader["DateLivraison"])) mLivraison.DateLivraison = (DateTime)mDataReader["DateLivraison"];
                        if (!DBNull.Value.Equals(mDataReader["Immatriculation"])) mLivraison.Immatriculation = (string)mDataReader["Immatriculation"];
                        mClass.BonDeLivraison.Livraison = mLivraison;                       

                        LivraisonType mLivraisonType = new LivraisonType();
                        if (!DBNull.Value.Equals(mDataReader["TypeLivraisonID"]))
                        {
                            mLivraisonType.ID = (int)mDataReader["TypeLivraisonID"];
                            mLivraisonType.Designation = (string)mDataReader["TypeLivraisonNom"];
                            mLivraison.LivraisonType = mLivraisonType;
                        }
                        mClass.BonDeLivraison.Livraison.LivraisonType = mLivraisonType;

                        //SacType mSacType = new SacType();
                        //if (!DBNull.Value.Equals(mDataReader["SacTypeID"]))
                        //{
                        //    mSacType.ID = (int)mDataReader["SacTypeID"];
                        //    mSacType.Designation = (string)mDataReader["SacTypeNom"];
                        //    mLivraison.SacType = mSacType;
                        //}

                        Certification mCertification = new Certification();
                        if (!DBNull.Value.Equals(mDataReader["CertificationID"]))
                        {
                            mCertification.ID = (int)mDataReader["CertificationID"];
                            mCertification.Designation = (string)mDataReader["CertificationNom"];
                            mLivraison.Certification = mCertification;
                        }
                        mClass.BonDeLivraison.Livraison.Certification = mCertification;                        
                    }

                    if (!DBNull.Value.Equals(mDataReader["NbreSacs"])) mClass._NombreSacs = (int)mDataReader["NbreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["Grainage"])) mClass._Grainage = (int)mDataReader["Grainage"];

                    if (!DBNull.Value.Equals(mDataReader["Humidite"])) mClass._Humidite = (double)mDataReader["Humidite"];
                    if (!DBNull.Value.Equals(mDataReader["MatiereEtg"])) mClass._MatiereEtrangere = (double)mDataReader["MatiereEtg"];
                    if (!DBNull.Value.Equals(mDataReader["Mitee"])) mClass._Mite = (double)mDataReader["Mitee"];
                    if (!DBNull.Value.Equals(mDataReader["Moisie"])) mClass._Moisi = (double)mDataReader["Moisie"];
                    if (!DBNull.Value.Equals(mDataReader["Ardoisee"])) mClass._Ardoisee = (double)mDataReader["Ardoisee"];
                    if (!DBNull.Value.Equals(mDataReader["Dechets"])) mClass._Sievings = (double)mDataReader["Dechets"];
                    if (!DBNull.Value.Equals(mDataReader["Ffa"])) mClass._Ffa = (double)mDataReader["Ffa"]; 
                    if (!DBNull.Value.Equals(mDataReader["NbreSacsLivraison"])) mClass._NbreSacsTotalLivraison = (int)mDataReader["NbreSacsLivraison"];
                    if (!DBNull.Value.Equals(mDataReader["TareSacs"])) mClass.TareSacs = (decimal)mDataReader["TareSacs"];
                    if (!DBNull.Value.Equals(mDataReader["TarePalettes"])) mClass.TarePalettes = (decimal)mDataReader["TarePalettes"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass.PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                    if (!DBNull.Value.Equals(mDataReader["RetentionPoids"])) mClass.Retention = (decimal)mDataReader["RetentionPoids"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsLivre"])) mClass.PoidsLivre = (decimal)mDataReader["PoidsLivre"];
                    if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass.PoidsNet = (decimal)mDataReader["PoidsNet"];
                    mClass._isnew = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n CompositionUsinageLivraison:MapFromDataReaderForProduction");
            }
        }

        private static void MapFromDataReaderLiv(CompositionUsinageLivraison mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;
                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass.ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["BonDeLivraisonID"]))
                    {
                        Livraison mLivraison = new Livraison();
                        mClass.BonDeLivraison = new BonDeLivraison();
                        mClass.BonDeLivraison.Livraison = new Livraison();

                        mClass.BonDeLivraison.ID = (Guid)mDataReader["BonDeLivraisonID"];
                        if (!DBNull.Value.Equals(mDataReader["LivraisonNumero"])) mLivraison.Numero = (string)mDataReader["LivraisonNumero"];
                        if (!DBNull.Value.Equals(mDataReader["LivraisonImmatriculation"])) mLivraison.Immatriculation = (string)mDataReader["LivraisonImmatriculation"];
                        if (!DBNull.Value.Equals(mDataReader["NombreSacs"])) mClass._NombreSacs = (int)mDataReader["NombreSacs"];
                        if (!DBNull.Value.Equals(mDataReader["TotalNombreSacs"])) mClass._NbreSacsTotalLivraison = (int)mDataReader["TotalNombreSacs"];
                        if (!DBNull.Value.Equals(mDataReader["PoidsBrut"])) mClass._PoidsBrut = (decimal)mDataReader["PoidsBrut"];
                        if (!DBNull.Value.Equals(mDataReader["PoidsLivre"])) mClass._PoidsLivre = (decimal)mDataReader["PoidsLivre"];
                        if (!DBNull.Value.Equals(mDataReader["PoidsNet"])) mClass._PoidsNet = (decimal)mDataReader["PoidsNet"];
                        if (!DBNull.Value.Equals(mDataReader["TareUnitaireSacs"])) mClass._TareSacs = (decimal)mDataReader["TareUnitaireSacs"];
                        mClass.BonDeLivraison.Livraison = mLivraison;                                                                       
                    }
                    mClass._isnew = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n CompositionUsinageLivraison:MapFromDataReaderLiv");
            }
        }


        #endregion
    }

    public partial class CompositionUsinageLivraisonViewModel
    {
        public CompositionUsinageLivraison _CompositionUsinageLivraison { get; set; }
        public string _Campagne { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
