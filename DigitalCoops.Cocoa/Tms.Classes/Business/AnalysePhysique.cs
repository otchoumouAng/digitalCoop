using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Components.Data;

namespace Tms.Classes.Business
{
    public class AnalysePhysique : DataPersist
    {
        #region "Fields"

        private Guid _ID;
        private Livraison _Livraison;
        private AnalyseCode _AnalyseCode;
        private Laboratoire _Laboratoire;
        private DateTime _DateAnalyse;
        private DateTime _DateApprobation;
        private string _FicheNumero;
        private int _NombreFeves;
        private int _Grainage;
        private int _MoisieNbre;
        private int _PlateNbre;
        private int _WeevilNbre;
        private int _GermeeNbre;
        private int _ArdoiseeNbre;
        private int _VioletteNbre;
        private double _NombreFevesPc;
        private double _Moisie;
        private double _Plate;
        private double _Weevil;
        private double _Germee;
        private double _Ardoisee;
        private double _Violette;
        private double _Defectueuse;
        private double _Fermentation;
        private double _Tamis;
        private double _Fragment;
        private double _Brisure;
        private double _Humidite;
        private double _MatiereEtrangere;
        private double? _Ffa;
        private ClassificationFeves _ClassificationFeves;
        private Analyseur _Analyseur;
        private string _Approbateur; 
        private Analyseur _Verificateur;
        private string _Commentaire;
        private bool _Confirmation;
        private string _Statut;
        private bool _Desactive;
        private Site _Sites;
        private bool _IsPending;
        private double? _BeansCluster;
        #endregion

        #region "Properties"

        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Livraison Livraison
        {
            get { return _Livraison; }
            set { _Livraison = value; }
        }

        public AnalyseCode AnalyseCode
        {
            get { return _AnalyseCode; }
            set { _AnalyseCode = value; }
        }

        
        public Laboratoire Laboratoire
        {
            get { return _Laboratoire; }
            set { _Laboratoire = value; }
        }

        
        public DateTime DateAnalyse
        {
            get { return _DateAnalyse; }
            set { _DateAnalyse = value; }
        }

        public DateTime DateApprobation
        {
            get { return _DateApprobation; }
            set { _DateApprobation = value; }
        }
        

        public string FicheNumero
        {
            get { return _FicheNumero; }
            set { _FicheNumero = value; }
        }

        

        public int NombreFeves
        {
            get { return _NombreFeves; }
            set { _NombreFeves = value; }
        }

        
        public int Grainage
        {
            get { return _Grainage; }
            set { _Grainage = value; }
        }

        
        public int MoisieNbre
        {
            get { return _MoisieNbre; }
            set { _MoisieNbre = value; }
        }

        
        public int PlateNbre
        {
            get { return _PlateNbre; }
            set { _PlateNbre = value; }
        }

        
        public int WeevilNbre
        {
            get { return _WeevilNbre; }
            set { _WeevilNbre = value; }
        }

        
        public int GermeeNbre
        {
            get { return _GermeeNbre; }
            set { _GermeeNbre = value; }
        }

       
        public int ArdoiseeNbre
        {
            get { return _ArdoiseeNbre; }
            set { _ArdoiseeNbre = value; }
        }

        public int VioletteNbre
        {
            get { return _VioletteNbre; }
            set { _VioletteNbre = value; }
        }

        
        public double Moisie
        {
            get { return _Moisie; }
            set { _Moisie = value; }
        }

        
        public double Plate
        {
            get { return _Plate; }
            set { _Plate = value; }
        }

        
        public double Weevil
        {
            get { return _Weevil; }
            set { _Weevil = value; }
        }

        public double Germee
        {
            get { return _Germee; }
            set { _Germee = value; }
        }

        
        public double Ardoisee
        {
            get { return _Ardoisee; }
            set { _Ardoisee = value; }
        }

        
        public double Violette
        {
            get { return _Violette; }
            set { _Violette = value; }
        }

        
        public double Defectueuse
        {
            get { return _Defectueuse; }
            set { _Defectueuse = value; }
        }

        public double Tamis
        {
            get { return _Tamis; }
            set { _Tamis = value; }
        }

        
        public double Fragment
        {
            get { return _Fragment; }
            set { _Fragment = value; }
        }

        public double Brisure
        {
            get { return _Brisure; }
            set { _Brisure = value; }
        }
        
        public double Humidite
        {
            get { return _Humidite; }
            set { _Humidite = value; }
        }

        public double MatiereEtrangere
        {
            get { return _MatiereEtrangere; }
            set { _MatiereEtrangere = value; }
        }


        public ClassificationFeves ClassificationFeves
        {
            get { return _ClassificationFeves; }
            set { _ClassificationFeves = value; }
        }

        public string NomClassificationFeves
        {
            get { return _ClassificationFeves != null ? _ClassificationFeves.Designation : string.Empty; }            
        }

        public Analyseur Analyseur
        {
            get { return _Analyseur; }
            set { _Analyseur = value; }
        }

        public string Approbateur
        {
            get { return _Approbateur; }
            set { _Approbateur = value; }
        }

        public Analyseur Verificateur
        {
            get { return _Verificateur; }
            set { _Verificateur = value; }
        }

        public string Commentaire
        {
            get { return _Commentaire; }
            set { _Commentaire = value; }
        }

        public bool Confirmation
        {
            get { return _Confirmation; }
            set { _Confirmation = value; }
        }

        public string Statut
        {
            get { return _Statut; }
            set { _Statut = value; }
        }

        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
        }

        public string CodeAnalyseDate
        {
            get { return _AnalyseCode == null ? string.Empty : _AnalyseCode.DateCode.ToString(); }

        }

        public string CodeAnalyse
        {
            get { return _AnalyseCode == null ? string.Empty : _AnalyseCode.Code; }

        }

        public string DateAnalyseAsString
        {
            get { return _DateAnalyse.ToString(); }

        }

        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desactive)
                    return 0; // BulletCross
                else if (_Confirmation && _Statut == "AC")
                    return 1;
                else if ((_Confirmation) && (_Statut == "RE"))
                    return 4;
                else if (_Statut == "RE")
                    return 3;
                else
                    return 2; // Tick                
            }            
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

        public double NombreFevesPc
        {
            get
            {
                return _NombreFevesPc;
            }

            set
            {
                _NombreFevesPc = value;
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

        public string SiteAsString
        {
            get { return _Sites != null ? _Sites.Nom : string.Empty; }
        }

        public string FournisseurAsString
        {
            get { return _Livraison != null && _Livraison.Fournisseur != null ? _Livraison.Fournisseur.Nom : string.Empty; }
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

        public double? BeansCluster
        {
            get
            {
                return _BeansCluster;
            }

            set
            {
                _BeansCluster = value;
            }
        }
        #endregion

        #region "Constructor"

        public AnalysePhysique()
        {

        }

        public AnalysePhysique(Guid myId)
        {
            this.fnGet(myId);
        }


        #endregion


        #region Methods
        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("AnalysePhysique_Get", (Guid)Id);
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
            return fnSelect("{Tous}", -1, -1, -1, null, null, -1);
        }

        public List<DataPersist> fnSelect(string cropyear, int SiteID, int LivraisonTypeID, int Fournisseur, DateTime? startdate, DateTime? enddate, int statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("AnalysePhysique_Select");
                db().AddInParameter(mCommande, "@cropyear", SqlDbType.Char, cropyear);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, SiteID);
                db().AddInParameter(mCommande, "@LivraisonTypeID", SqlDbType.Int, LivraisonTypeID);
                db().AddInParameter(mCommande, "@Fournisseur", SqlDbType.Int, Fournisseur);
                db().AddInParameter(mCommande, "@begindate", SqlDbType.DateTime, startdate);
                db().AddInParameter(mCommande, "@enddate", SqlDbType.DateTime, enddate);
                db().AddInParameter(mCommande, "@status", SqlDbType.SmallInt, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    AnalysePhysique mClass = new AnalysePhysique();

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

        public List<DataPersist> fnSelect_Interne(string cropyear, int SiteID, int LivraisonTypeID, int Fournisseur, DateTime? startdate, DateTime? enddate, int statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("AnalysePhysique_SelectInterne");
                db().AddInParameter(mCommande, "@cropyear", SqlDbType.Char, cropyear);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, SiteID);
                db().AddInParameter(mCommande, "@LivraisonTypeID", SqlDbType.Int, LivraisonTypeID);
                db().AddInParameter(mCommande, "@Fournisseur", SqlDbType.Int, Fournisseur);
                db().AddInParameter(mCommande, "@begindate", SqlDbType.DateTime, startdate);
                db().AddInParameter(mCommande, "@enddate", SqlDbType.DateTime, enddate);
                db().AddInParameter(mCommande, "@status", SqlDbType.SmallInt, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    AnalysePhysique mClass = new AnalysePhysique();

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


        public List<DataPersist> fnSelectForSite(string cropyear, int SiteID, int LivraisonTypeID, int Fournisseur, DateTime? startdate, DateTime? enddate, int statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("AnalysePhysique_Select");
                db().AddInParameter(mCommande, "@cropyear", SqlDbType.Char, cropyear);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, SiteID);
                db().AddInParameter(mCommande, "@LivraisonTypeID", SqlDbType.Int, LivraisonTypeID);
                db().AddInParameter(mCommande, "@Fournisseur", SqlDbType.Int, Fournisseur);
                db().AddInParameter(mCommande, "@begindate", SqlDbType.DateTime, startdate);
                db().AddInParameter(mCommande, "@enddate", SqlDbType.DateTime, enddate);
                db().AddInParameter(mCommande, "@status", SqlDbType.SmallInt, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    AnalysePhysique mClass = new AnalysePhysique();

                    MapFromDataReaderSite(mClass, mDataReader);
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


        public List<DataPersist> fnSelectByDelivery(Guid LivraisonID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("AnalysePhysique_SelectByLivraison");
                db().AddInParameter(mCommande, "@LivraisonID", SqlDbType.UniqueIdentifier, LivraisonID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    AnalysePhysique mClass = new AnalysePhysique();

                    MapFromDataReaderLight(mClass, mDataReader);
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

                    mCommande = db().CreateStoredProcCommand("AnalysePhysique_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);                    
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("AnalysePhysique_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                if(_AnalyseCode != null) db().AddInParameter(mCommande, "@CodeId", SqlDbType.UniqueIdentifier, _AnalyseCode.ID);
                else db().AddInParameter(mCommande, "@CodeId", SqlDbType.UniqueIdentifier, DBNull.Value);
                //db().AddInParameter(mCommande, "@CodeId", SqlDbType.UniqueIdentifier, _AnalyseCode.ID);
                db().AddInParameter(mCommande, "@LaboratoireId", SqlDbType.Int, _Laboratoire.ID);
                db().AddInParameter(mCommande, "@DateAnalyse", SqlDbType.DateTime, _DateAnalyse);
                db().AddInParameter(mCommande, "@NumeroFiche", SqlDbType.VarChar, _FicheNumero);
                db().AddInParameter(mCommande, "@NombreFeves", SqlDbType.Int, _NombreFeves);
                db().AddInParameter(mCommande, "@BeanCount", SqlDbType.Int, _Grainage);
                db().AddInParameter(mCommande, "@BeanCountPc", SqlDbType.Decimal, NombreFevesPc);
                db().AddInParameter(mCommande, "@Mouldy", SqlDbType.Int, _MoisieNbre);
                db().AddInParameter(mCommande, "@Flat", SqlDbType.Int, _PlateNbre);
                db().AddInParameter(mCommande, "@Weevil", SqlDbType.Int, _WeevilNbre);
                db().AddInParameter(mCommande, "@Germinated", SqlDbType.Int, _GermeeNbre);
                db().AddInParameter(mCommande, "@Slaty", SqlDbType.Int, _ArdoiseeNbre);
                db().AddInParameter(mCommande, "@Violet", SqlDbType.Int, _VioletteNbre);
                db().AddInParameter(mCommande, "@MouldyPc", SqlDbType.Decimal, _Moisie);
                db().AddInParameter(mCommande, "@FlatPc", SqlDbType.Decimal, _Plate);
                db().AddInParameter(mCommande, "@WeevilPc", SqlDbType.Decimal, _Weevil);
                db().AddInParameter(mCommande, "@GerminatedPc", SqlDbType.Decimal, _Germee);
                db().AddInParameter(mCommande, "@SlatyPc", SqlDbType.Decimal, _Ardoisee);
                db().AddInParameter(mCommande, "@VioletPc", SqlDbType.Decimal, _Violette);
                db().AddInParameter(mCommande, "@DefectivePC", SqlDbType.Decimal, _Defectueuse);
                db().AddInParameter(mCommande, "@UnfermentedPc", SqlDbType.Decimal, _Fermentation);
                db().AddInParameter(mCommande, "@SievingPc", SqlDbType.Decimal, _Tamis);
                db().AddInParameter(mCommande, "@FragmentPc", SqlDbType.Decimal, _Fragment);
                db().AddInParameter(mCommande, "@BrokenBeanPc", SqlDbType.Decimal, _Brisure);
                db().AddInParameter(mCommande, "@Moisture", SqlDbType.Decimal, _Humidite);
                db().AddInParameter(mCommande, "@ForeignMatter", SqlDbType.Decimal, _MatiereEtrangere);
                db().AddInParameter(mCommande, "@Ffa", SqlDbType.Decimal, _Ffa);
                db().AddInParameter(mCommande, "@Crabot", SqlDbType.Decimal, _BeansCluster);
                db().AddInParameter(mCommande, "@ClassificationFeve", SqlDbType.Int, _ClassificationFeves.ID);                

                if (_Verificateur != null) db().AddInParameter(mCommande, "@Analyseur", SqlDbType.Int, _Analyseur.ID);
                else db().AddInParameter(mCommande, "@Analyseur", SqlDbType.Int, DBNull.Value);

                if (_Verificateur != null) db().AddInParameter(mCommande, "@Verificateur", SqlDbType.Int, _Verificateur.ID);
                else db().AddInParameter(mCommande, "@Verificateur", SqlDbType.Int, DBNull.Value);

                if (_Livraison != null) db().AddInParameter(mCommande, "@LivraisonID", SqlDbType.UniqueIdentifier, _Livraison.ID);
                else db().AddInParameter(mCommande, "@LivraisonID", SqlDbType.UniqueIdentifier, DBNull.Value);

                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, _Sites.ID);

                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);                
                db().AddInParameter(mCommande, "@ResultatAnalyse", SqlDbType.Char, _Statut);

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
                throw new Exception(ex.Message + "\r\n" + "AnalysePhysique:fnUpdate");

            }
            return Result;
        }


        public override bool fnActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("AnalysePhysique_Activate");
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
                        Result = true;
                        Desactive = false;
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
                throw new Exception(ex.Message + "\r\n" + "AnalysePhysique:fnActivate");
            }
            return Result;
        }

        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("AnalysePhysique_DeActivate");
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
                        Result = true;
                        Desactive = true;
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
                throw new Exception(ex.Message + "\r\n" + "AnalysePhysique:fnDeActivate");
            }
            return Result;
        }

        public bool fnApprove()
        {

            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("AnalysePhysique_Approve");
            db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
            db().AddInParameter(mCommande, "@statut", SqlDbType.VarChar, _Statut);
            db().AddInParameter(mCommande, "@CodeAnalyse", SqlDbType.UniqueIdentifier, _AnalyseCode.ID);
            db().AddInParameter(mCommande, "@ApproveUser", SqlDbType.VarChar, _Approbateur);
            db().AddInParameter(mCommande, "@ApproveDate", SqlDbType.DateTime, _DateApprobation);            
            db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);
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
                throw new Exception(ex.Message + "\r\n" + "AbalysePhysique:fnApprove");
            }
            return Result;

        }

        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _FicheNumero;
        }

        private static void MapFromDataReader(AnalysePhysique mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    
                    if (!DBNull.Value.Equals(mDataReader["DateAnalyse"])) mClass._DateAnalyse = (DateTime)mDataReader["DateAnalyse"];                    
                    if (!DBNull.Value.Equals(mDataReader["NumeroFiche"])) mClass._FicheNumero = (string)mDataReader["NumeroFiche"];                    
                    if (!DBNull.Value.Equals(mDataReader["NombreFeves"])) mClass._NombreFeves = (int)mDataReader["NombreFeves"];
                    if (!DBNull.Value.Equals(mDataReader["BeanCount"])) mClass._Grainage = (int)mDataReader["BeanCount"];
                    if (!DBNull.Value.Equals(mDataReader["BeanCountPc"])) mClass._NombreFevesPc = (double)mDataReader["BeanCountPc"];
                    if (!DBNull.Value.Equals(mDataReader["Moudly"])) mClass._MoisieNbre = (int)mDataReader["Moudly"];
                    if (!DBNull.Value.Equals(mDataReader["Flat"])) mClass._PlateNbre = (int)mDataReader["Flat"];
                    if (!DBNull.Value.Equals(mDataReader["Weevil"])) mClass._WeevilNbre = (int)mDataReader["Weevil"];
                    if (!DBNull.Value.Equals(mDataReader["Germinated"])) mClass._GermeeNbre = (int)mDataReader["Germinated"];
                    if (!DBNull.Value.Equals(mDataReader["Slaty"])) mClass._ArdoiseeNbre = (int)mDataReader["Slaty"];
                    if (!DBNull.Value.Equals(mDataReader["Violet"])) mClass._VioletteNbre = (int)mDataReader["Violet"];
                    if (!DBNull.Value.Equals(mDataReader["MoudlyPc"])) mClass._Moisie = (double)mDataReader["MoudlyPc"];
                    if (!DBNull.Value.Equals(mDataReader["FlatPc"])) mClass._Plate = (double)mDataReader["FlatPc"];
                    if (!DBNull.Value.Equals(mDataReader["WeevilPc"])) mClass._Weevil = (double)mDataReader["WeevilPc"];
                    if (!DBNull.Value.Equals(mDataReader["GerminatedPc"])) mClass._Germee = (double)mDataReader["GerminatedPc"];
                    if (!DBNull.Value.Equals(mDataReader["SlatyPc"])) mClass._Ardoisee = (double)mDataReader["SlatyPc"];
                    if (!DBNull.Value.Equals(mDataReader["VioletPc"])) mClass._Violette = (double)mDataReader["VioletPc"];
                    if (!DBNull.Value.Equals(mDataReader["DefectivesPc"])) mClass._Defectueuse = (double)mDataReader["DefectivesPc"];
                    if (!DBNull.Value.Equals(mDataReader["SievingPc"])) mClass._Tamis = (double)mDataReader["SievingPc"];
                    if (!DBNull.Value.Equals(mDataReader["FragmentPc"])) mClass._Fragment = (double)mDataReader["FragmentPc"];
                    if (!DBNull.Value.Equals(mDataReader["BrokenBeanPc"])) mClass._Brisure = (double)mDataReader["BrokenBeanPc"];
                    if (!DBNull.Value.Equals(mDataReader["Moisture"])) mClass._Humidite = (double)mDataReader["Moisture"];                    
                    if (!DBNull.Value.Equals(mDataReader["ConfirmationResultat"])) mClass._Confirmation = (bool)mDataReader["ConfirmationResultat"];
                    if (!DBNull.Value.Equals(mDataReader["ResultatAnalyse"])) mClass._Statut = (string)mDataReader["ResultatAnalyse"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["FFA"])) mClass._Ffa = (double)mDataReader["FFA"];
                    if (!DBNull.Value.Equals(mDataReader["Crabot"])) mClass._BeansCluster = (double)mDataReader["Crabot"];
                    if (!DBNull.Value.Equals(mDataReader["UnfermentedPc"])) mClass._Fermentation = (double)mDataReader["UnfermentedPc"];

                    mClass._AnalyseCode = new AnalyseCode();
                    if (!DBNull.Value.Equals(mDataReader["CodeAnalyseID"]))
                    {
                        if (!DBNull.Value.Equals(mDataReader["CodeAnalyseID"])) mClass._AnalyseCode.ID = (Guid)mDataReader["CodeAnalyseID"];
                        if (!DBNull.Value.Equals(mDataReader["CodeAnalyse"])) mClass._AnalyseCode.Code = (string)mDataReader["CodeAnalyse"];
                        if (!DBNull.Value.Equals(mDataReader["CodeAnalyseDate"])) mClass._AnalyseCode.DateCode = (DateTime)mDataReader["CodeAnalyseDate"];
                    }
                    mClass._Laboratoire = new Laboratoire();
                    if (!DBNull.Value.Equals(mDataReader["LaboratoireID"])) mClass._Laboratoire.ID = (int)mDataReader["LaboratoireID"];
                    if (!DBNull.Value.Equals(mDataReader["DesignationLaboratoire"])) mClass._Laboratoire.Designation = (string)mDataReader["DesignationLaboratoire"];

                    mClass._ClassificationFeves = new ClassificationFeves();
                    if (!DBNull.Value.Equals(mDataReader["ClassificationFevesID"])) mClass._ClassificationFeves.ID = (int)mDataReader["ClassificationFevesID"];
                    if (!DBNull.Value.Equals(mDataReader["ClassificationDesignation"])) mClass._ClassificationFeves.Designation = (string)mDataReader["ClassificationDesignation"];

                    mClass._Analyseur = new Analyseur();
                    if (!DBNull.Value.Equals(mDataReader["AnalyseurID"])) mClass._Analyseur.ID = (int)mDataReader["AnalyseurID"];                    
                    if (!DBNull.Value.Equals(mDataReader["AnalyseurNom"])) mClass._Analyseur.Nom = (string)mDataReader["AnalyseurNom"];                    
                    if (!DBNull.Value.Equals(mDataReader["AnalyseurPeutApprouver"])) mClass._Analyseur.PeutApprouver = (bool)mDataReader["AnalyseurPeutApprouver"];
                    
                    if (!DBNull.Value.Equals(mDataReader["Approbateur"])) mClass._Approbateur = (string)mDataReader["Approbateur"];
                    if (!DBNull.Value.Equals(mDataReader["DateApprobation"])) mClass._DateApprobation = (DateTime)mDataReader["DateApprobation"];
                    if (!DBNull.Value.Equals(mDataReader["MatiereEtrangere"])) mClass._MatiereEtrangere = (double)mDataReader["MatiereEtrangere"];

                    mClass._Verificateur = new Analyseur();
                    if (!DBNull.Value.Equals(mDataReader["VerificateurID"])) mClass._Verificateur.ID = (int)mDataReader["VerificateurID"];
                    if (!DBNull.Value.Equals(mDataReader["VerificateurNom"])) mClass._Verificateur.Nom = (string)mDataReader["VerificateurNom"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                    mClass._Sites = new Site();
                    if (!DBNull.Value.Equals(mDataReader["SiteID"])) mClass._Sites.ID = (int)mDataReader["SiteID"];
                    if (!DBNull.Value.Equals(mDataReader["SiteNom"])) mClass._Sites.Nom = (string)mDataReader["SiteNom"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nAnalysePhysique:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderLight(AnalysePhysique mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["AnalyseCodeID"]))
                    {
                        AnalyseCode mAnalyseCode = new AnalyseCode();
                        mAnalyseCode.ID = (Guid)mDataReader["AnalyseCodeID"];
                        mAnalyseCode.Code = (string)mDataReader["Code"];
                        mClass._AnalyseCode = mAnalyseCode;
                    }

                    if (!DBNull.Value.Equals(mDataReader["BeanCount"])) mClass._NombreFeves = (int)mDataReader["BeanCount"];
                    if (!DBNull.Value.Equals(mDataReader["Moisture"])) mClass._Humidite = (double)mDataReader["Moisture"];
                    if (!DBNull.Value.Equals(mDataReader["Mouldy"])) mClass._Moisie = (double)mDataReader["Mouldy"];
                    if (!DBNull.Value.Equals(mDataReader["BrokenBean"])) mClass._Brisure = (double)mDataReader["BrokenBean"];
                    if (!DBNull.Value.Equals(mDataReader["Sieving"])) mClass._Tamis = (double)mDataReader["Sieving"];
                    if (!DBNull.Value.Equals(mDataReader["ForeignMatter"])) mClass._MatiereEtrangere = (double)mDataReader["ForeignMatter"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nAnalysePhysique:MapFromDataReaderLight");
            }
        }

        private static void MapFromDataReaderSite(AnalysePhysique mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["DateAnalyse"])) mClass._DateAnalyse = (DateTime)mDataReader["DateAnalyse"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroFiche"])) mClass._FicheNumero = (string)mDataReader["NumeroFiche"];
                    if (!DBNull.Value.Equals(mDataReader["NombreFeves"])) mClass._NombreFeves = (int)mDataReader["NombreFeves"];
                    if (!DBNull.Value.Equals(mDataReader["BeanCount"])) mClass._Grainage = (int)mDataReader["BeanCount"];
                    if (!DBNull.Value.Equals(mDataReader["BeanCountPc"])) mClass._NombreFevesPc = (double)mDataReader["BeanCountPc"];
                    if (!DBNull.Value.Equals(mDataReader["Moudly"])) mClass._MoisieNbre = (int)mDataReader["Moudly"];
                    if (!DBNull.Value.Equals(mDataReader["Flat"])) mClass._PlateNbre = (int)mDataReader["Flat"];
                    if (!DBNull.Value.Equals(mDataReader["Weevil"])) mClass._WeevilNbre = (int)mDataReader["Weevil"];
                    if (!DBNull.Value.Equals(mDataReader["Germinated"])) mClass._GermeeNbre = (int)mDataReader["Germinated"];
                    if (!DBNull.Value.Equals(mDataReader["Slaty"])) mClass._ArdoiseeNbre = (int)mDataReader["Slaty"];
                    if (!DBNull.Value.Equals(mDataReader["Violet"])) mClass._VioletteNbre = (int)mDataReader["Violet"];
                    if (!DBNull.Value.Equals(mDataReader["MoudlyPc"])) mClass._Moisie = (double)mDataReader["MoudlyPc"];
                    if (!DBNull.Value.Equals(mDataReader["FlatPc"])) mClass._Plate = (double)mDataReader["FlatPc"];
                    if (!DBNull.Value.Equals(mDataReader["WeevilPc"])) mClass._Weevil = (double)mDataReader["WeevilPc"];
                    if (!DBNull.Value.Equals(mDataReader["GerminatedPc"])) mClass._Germee = (double)mDataReader["GerminatedPc"];
                    if (!DBNull.Value.Equals(mDataReader["SlatyPc"])) mClass._Ardoisee = (double)mDataReader["SlatyPc"];
                    if (!DBNull.Value.Equals(mDataReader["VioletPc"])) mClass._Violette = (double)mDataReader["VioletPc"];
                    if (!DBNull.Value.Equals(mDataReader["DefectivesPc"])) mClass._Defectueuse = (double)mDataReader["DefectivesPc"];
                    if (!DBNull.Value.Equals(mDataReader["SievingPc"])) mClass._Tamis = (double)mDataReader["SievingPc"];
                    if (!DBNull.Value.Equals(mDataReader["FragmentPc"])) mClass._Fragment = (double)mDataReader["FragmentPc"];
                    if (!DBNull.Value.Equals(mDataReader["BrokenBeanPc"])) mClass._Brisure = (double)mDataReader["BrokenBeanPc"];
                    if (!DBNull.Value.Equals(mDataReader["Moisture"])) mClass._Humidite = (double)mDataReader["Moisture"];
                    if (!DBNull.Value.Equals(mDataReader["ConfirmationResultat"])) mClass._Confirmation = (bool)mDataReader["ConfirmationResultat"];
                    if (!DBNull.Value.Equals(mDataReader["ResultatAnalyse"])) mClass._Statut = (string)mDataReader["ResultatAnalyse"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["FFA"])) mClass._Ffa = (double)mDataReader["FFA"];
                    if (!DBNull.Value.Equals(mDataReader["UnfermentedPc"])) mClass._Fermentation = (double)mDataReader["UnfermentedPc"];

                    mClass._AnalyseCode = new AnalyseCode();
                    if (!DBNull.Value.Equals(mDataReader["CodeAnalyseID"]))
                    {
                        if (!DBNull.Value.Equals(mDataReader["CodeAnalyseID"])) mClass._AnalyseCode.ID = (Guid)mDataReader["CodeAnalyseID"];
                        if (!DBNull.Value.Equals(mDataReader["CodeAnalyse"])) mClass._AnalyseCode.Code = (string)mDataReader["CodeAnalyse"];
                        if (!DBNull.Value.Equals(mDataReader["CodeAnalyseDate"])) mClass._AnalyseCode.DateCode = (DateTime)mDataReader["CodeAnalyseDate"];
                    }

                    mClass._Livraison = new Livraison();
                    if (!DBNull.Value.Equals(mDataReader["LivraisonID"])) mClass._Livraison.ID = (Guid)mDataReader["LivraisonID"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonNumero"])) mClass._Livraison.Numero = (string)mDataReader["LivraisonNumero"];

                    mClass._Sites = new Site();
                    if (!DBNull.Value.Equals(mDataReader["SiteID"])) mClass._Sites.ID = (int)mDataReader["SiteID"];
                    if (!DBNull.Value.Equals(mDataReader["SiteNom"])) mClass._Sites.Nom = (string)mDataReader["SiteNom"];

                    mClass._Livraison.Fournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["FournisseurID"])) mClass._Livraison.Fournisseur.ID = (int)mDataReader["FournisseurID"];
                    if (!DBNull.Value.Equals(mDataReader["FournisseurNom"])) mClass._Livraison.Fournisseur.Nom = (string)mDataReader["FournisseurNom"];

                    mClass._Livraison.Campagne = new Campagne();
                    if (!DBNull.Value.Equals(mDataReader["CampagneID"])) mClass._Livraison.Campagne.Designation = (string)mDataReader["CampagneID"];

                    mClass._Livraison.LivraisonType = new LivraisonType();
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeID"])) mClass._Livraison.LivraisonType.ID = (int)mDataReader["LivraisonTypeID"];
                    if (!DBNull.Value.Equals(mDataReader["LivraisonTypeDesignation"])) mClass._Livraison.LivraisonType.Designation = (string)mDataReader["LivraisonTypeDesignation"];

                    mClass._Laboratoire = new Laboratoire();
                    if (!DBNull.Value.Equals(mDataReader["LaboratoireID"])) mClass._Laboratoire.ID = (int)mDataReader["LaboratoireID"];
                    if (!DBNull.Value.Equals(mDataReader["DesignationLaboratoire"])) mClass._Laboratoire.Designation = (string)mDataReader["DesignationLaboratoire"];

                    mClass._ClassificationFeves = new ClassificationFeves();
                    if (!DBNull.Value.Equals(mDataReader["ClassificationFevesID"])) mClass._ClassificationFeves.ID = (int)mDataReader["ClassificationFevesID"];
                    if (!DBNull.Value.Equals(mDataReader["ClassificationDesignation"])) mClass._ClassificationFeves.Designation = (string)mDataReader["ClassificationDesignation"];

                    mClass._Analyseur = new Analyseur();
                    if (!DBNull.Value.Equals(mDataReader["AnalyseurID"])) mClass._Analyseur.ID = (int)mDataReader["AnalyseurID"];
                    if (!DBNull.Value.Equals(mDataReader["AnalyseurNom"])) mClass._Analyseur.Nom = (string)mDataReader["AnalyseurNom"];
                    if (!DBNull.Value.Equals(mDataReader["AnalyseurPeutApprouver"])) mClass._Analyseur.PeutApprouver = (bool)mDataReader["AnalyseurPeutApprouver"];

                    if (!DBNull.Value.Equals(mDataReader["Approbateur"])) mClass._Approbateur = (string)mDataReader["Approbateur"];
                    if (!DBNull.Value.Equals(mDataReader["DateApprobation"])) mClass._DateApprobation = (DateTime)mDataReader["DateApprobation"];
                    if (!DBNull.Value.Equals(mDataReader["MatiereEtrangere"])) mClass._MatiereEtrangere = (double)mDataReader["MatiereEtrangere"];

                    mClass._Verificateur = new Analyseur();
                    if (!DBNull.Value.Equals(mDataReader["VerificateurID"])) mClass._Verificateur.ID = (int)mDataReader["VerificateurID"];
                    if (!DBNull.Value.Equals(mDataReader["VerificateurNom"])) mClass._Verificateur.Nom = (string)mDataReader["VerificateurNom"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nAnalysePhysiqueAgence:MapFromDataReader");
            }
        }

        #endregion

    }

    public partial class AnalysePhysiqueViewModel
    {
        public AnalysePhysique _AnalysePhysique { get; set; }

        public Parametres _Parametres { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
