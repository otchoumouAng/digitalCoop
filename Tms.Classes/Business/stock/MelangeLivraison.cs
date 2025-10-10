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
    //[Proxy(Read = "~/MelangeLivraison/Select")]
    //[JsonReader(RootProperty = "data")]
    public class MelangeLivraison : DataPersist
    {
        #region "Fields"

        private Guid _ID;        
        private Livraison _Livraison;        
        private string _Numero;
        private DateTime _DateMelangeLivraison;       
        private double _Humidite;
        private double _MatieresEtrangeres;        
        private int _NbreSacs;
        private int _BeanCount;                        
        private int? _CertificationID;
        private bool _isNewValue;
        private double _Sievings;        

        private double _Defectueuse;
        private double _Mouldy;
        private double _Slaty;

        private double? _Ffa;
        private double? _WeevilPc;

        // ** Pour Usinage
        private int? _MelangeID;
        private Melange _Melange;
        private string _MelangeAsString;

        #endregion

        #region "Properties"
        [ModelField(IDProperty = true, SortType = Ext.Net.SortTypeMethod.None, SortDir = Ext.Net.SortDirection.ASC)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        
        public int? CertificationID
        {
            get { return _CertificationID; }
            set { _CertificationID = value; }
        }

        public Livraison Livraison
        {
            get { return _Livraison; }
            set { _Livraison = value; }
        }
        
        public DateTime? DateLivraison
        {
            get { return (_Livraison != null && _Livraison.DateLivraison != null) ? _Livraison.DateLivraison : (DateTime?)null; }

        }

        public string MelangeAsString
        {
            get { return _Melange != null ? _Melange.Designation : string.Empty; }
            set { _MelangeAsString = value; }
        }

        public string DateLivraisonLongAstring
        {
            get { return (_Livraison != null && _Livraison.DateLivraison != null) ? _Livraison.DateLivraison.ToString("d") : string.Empty; }

        }

        public string DateLivraisonAstring
        {
            get { return (_Livraison != null && _Livraison.DateLivraison != null) ? _Livraison.DateLivraison.ToShortDateString() : string.Empty; }

        }

        public string LibelleTypeDeLivraison
        {
            get { return (_Livraison != null && _Livraison.LivraisonType != null)  ? _Livraison.LivraisonType.Designation :  string.Empty; }
            
        }
        public string LivraisonID
        {
            get { return (_Livraison != null) ? _Livraison.Numero : string.Empty; }

        }
        public string LibelleTypeDeSac
        {
            get { return (_Livraison != null && _Livraison.SacType != null) ? _Livraison.SacType.Designation : string.Empty; }

        }

        public string LibelleCertification
        {
            get { return (_Livraison != null && _Livraison.Certification != null ) ? _Livraison.Certification.Designation : string.Empty; }

        }
        public string Immatriculation
        {
            get { return _Livraison != null ? _Livraison.Immatriculation : string.Empty; }

        }

        public string FournisseurNom
        {
            get { return (_Livraison != null  && _Livraison.Fournisseur != null ) ? _Livraison.Fournisseur.Nom + " - " + _Livraison.Fournisseur.ID : string.Empty; }

        }

        public string NomTransporteur
        {
            get { return (_Livraison != null && _Livraison.Transporteur != null) ? _Livraison.Transporteur.Nom : string.Empty; }

        }

        public string LivraisonNumeroExterne
        {
            get { return (_Livraison != null ) ? _Livraison.NumeroExterne : string.Empty; }

        }
        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
                
        public DateTime DateMelangeLivraison
        {
            get { return _DateMelangeLivraison; }
            set { _DateMelangeLivraison = value; }
        }

        public string DateMelangeLivraisonLongAsSrtring
        {
            get { return _DateMelangeLivraison != null ? _DateMelangeLivraison.ToString("d") : string.Empty; }            
        }

        public int NbreSacs
        {
            get { return _NbreSacs; }
            set { _NbreSacs = value;}
        }

        public string NbreSacsAsString
        {
            get { return _NbreSacs != 0 ? String.Format("{0:#,#}", _NbreSacs).TrimStart() : string.Empty; }
        }

        public int BeanCount
        {
            get { return _BeanCount; }
            set { _BeanCount = value; }
        }

        public double Humidite
        {
            get { return _Humidite; }
            set { _Humidite = value; }
        }

        public double MatieresEtrangeres
        {
            get { return _MatieresEtrangeres; }
            set { _MatieresEtrangeres = value; }
        }                      

        [Column(Ignore = true)]
        public string AsString
        {
            get { return _Numero; }
        }                

        public double Sievings
        {
            get { return _Sievings; }
            set { _Sievings = value; }
        }

        public double Defectueuse
        {
            get
            {
                return _Defectueuse;
            }

            set
            {
                _Defectueuse = value;
            }
        }

        public double Moisie
        {
            get
            {
                return _Mouldy;
            }

            set
            {
                _Mouldy = value;
            }
        }

        public double Slaty
        {
            get
            {
                return _Slaty;
            }

            set
            {
                _Slaty = value;
            }
        }

        public string LivraisonPoidsDeclare
        {
            get { return _Livraison != null ? String.Format("{0:#,#}", _Livraison.PoidsDeclare).TrimStart() : string.Empty; }
        }

        public double? Ffa
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

        public double? WeevilPc
        {
            get
            {
                return _WeevilPc;
            }

            set
            {
                _WeevilPc = value;
            }
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
        #endregion

        #region Constructor
        public MelangeLivraison()
        {

        }

        public MelangeLivraison(Guid myID)
        {
            this.fnGet(myID);
        }
        #endregion

        #region Methods
        public override bool fnGet(object Id)
        {
            throw new NotImplementedException();
        }        

        public override List<DataPersist> fnSelect()
        {
            return fnSelect("", -1, -1, null, null, "-1");
        }

        public List<DataPersist> fnSelect(string Campagne, int FournisseurID, int TypeID, DateTime? StartDate, DateTime? EndDate, string statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("MelangeLivraison_Select");
                db().AddInParameter(mCommande, "@Campagne", SqlDbType.VarChar, 9, Campagne);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, FournisseurID);
                db().AddInParameter(mCommande, "@DeliveryTypeID", SqlDbType.Int, TypeID);
                db().AddInParameter(mCommande, "@DateDebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                db().AddInParameter(mCommande, "@status", SqlDbType.VarChar, 2, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    MelangeLivraison mClass = new MelangeLivraison();
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
        public List<DataPersist> fnSelectAvailableForProduction(int CertificationID, int LivraisonTypeID, DateTime? StartDate, DateTime? EndDate)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("BonDeLivraion_SelectForProduction");
                db().AddInParameter(mCommande, "@LivraisonTypeID", SqlDbType.Int, LivraisonTypeID);
                db().AddInParameter(mCommande, "@CertificationId", SqlDbType.Int, CertificationID);
                db().AddInParameter(mCommande, "@datedebut", SqlDbType.DateTime, StartDate);
                db().AddInParameter(mCommande, "@Datefin", SqlDbType.DateTime, EndDate);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    MelangeLivraison mClass = new MelangeLivraison();
                    MapFromDataReader(mClass, mDataReader);
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

        public override bool fnUpdate()
        {
            throw new NotImplementedException();
        }

        public override bool fnActivate()
        {
            return false;
        }

        public override bool fnDeActivate()
        {
            return false;
        }

        public bool fnCancel()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _Numero;
        }

        private static void MapFromDataReader(MelangeLivraison mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;
                    mClass._ID = Guid.NewGuid();
                    //if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["LivraisonGuid"]))
                    {
                        Livraison mLivraison = new Livraison();

                        mLivraison.ID = (Guid)mDataReader["LivraisonGuid"];
                        if (!DBNull.Value.Equals(mDataReader["LivraisonID"])) mLivraison.Numero = (string)mDataReader["LivraisonID"];
                        if (!DBNull.Value.Equals(mDataReader["DateLivraison"])) mLivraison.DateLivraison = (DateTime)mDataReader["DateLivraison"];
                        if (!DBNull.Value.Equals(mDataReader["Immatriculation"])) mLivraison.Immatriculation = (string)mDataReader["Immatriculation"];

                        Campagne mCampagne = new Campagne();
                        if (!DBNull.Value.Equals(mDataReader["Campagne"])) mCampagne.Designation = (string)mDataReader["Campagne"];
                        mLivraison.Campagne = mCampagne;

                        LivraisonType mLivraisonType = new LivraisonType();
                        if (!DBNull.Value.Equals(mDataReader["TypeLivraisonID"]))
                        {
                            mLivraisonType.ID = (int)mDataReader["TypeLivraisonID"];
                            mLivraisonType.Designation = (string)mDataReader["TypeLivraisonNom"];
                            mLivraison.LivraisonType = mLivraisonType;
                        }                        

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

                        mClass.Livraison = mLivraison;
                    }                    
                                       
                    if (!DBNull.Value.Equals(mDataReader["NbreSacs"])) mClass._NbreSacs = (int)mDataReader["NbreSacs"];
                    if (!DBNull.Value.Equals(mDataReader["Grainage"])) mClass._BeanCount = (int)mDataReader["Grainage"];

                    if (!DBNull.Value.Equals(mDataReader["Humidite"])) mClass._Humidite = (double)mDataReader["Humidite"];
                    if (!DBNull.Value.Equals(mDataReader["MatiereEtg"])) mClass._MatieresEtrangeres = (double)mDataReader["MatiereEtg"];
                    if (!DBNull.Value.Equals(mDataReader["Mitee"])) mClass._WeevilPc = (double)mDataReader["Mitee"];
                    if (!DBNull.Value.Equals(mDataReader["Moisie"])) mClass._Mouldy = (double)mDataReader["Moisie"];
                    if (!DBNull.Value.Equals(mDataReader["Ardoisee"])) mClass._Slaty = (double)mDataReader["Ardoisee"];
                    if (!DBNull.Value.Equals(mDataReader["Dechets"])) mClass._Sievings = (double)mDataReader["Dechets"];
                    if (!DBNull.Value.Equals(mDataReader["FFA"])) mClass._Ffa = (double)mDataReader["FFA"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nlbc_MelangeLivraison:MapFromDataReaderForProduction");
            }
        }

        #endregion
    }

    public partial class MelangeLivraisonViewModel
    {
        public MelangeLivraison _MelangeLivraison { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }

        public string CertificationID
        {
            get
            {
                if (_ExecMode != Components.Settings.EnumsDefinition.eExecMode.AddNew)
                    return (this._MelangeLivraison != null && this._MelangeLivraison.Livraison != null && this._MelangeLivraison.Livraison.Certification != null) ? _MelangeLivraison.Livraison.Certification.ID.ToString() : String.Empty;
                else
                    return String.Empty;
            }
        }
    }
}
