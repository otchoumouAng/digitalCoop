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
    public class Producteur : DataPersist
    {
        #region fields
        private Guid _ID;
        private Int64 _Code;
        private string _CodeRegistre;
        private string _CodeExterne1;
        private string _CodeExterne2;
        private string _CodeExterne3;
        private string _Nom;
        private string _Prenom;
        private string _Village;
        private string _Section;
        private bool _Desactive;
        private string _NumeroDePiece;
        private PieceType _PieceType;
        private int _NombreDeParcelles;
        private double? _SuperficieTotale;
        private bool _Certifie;
        private string _Projet1;
        private string _Projet2;
        private double? _QuotaProjet1;
        private double? _QuotaProjet2;
        private int _NombreEnfants;
        private string _GPSParcelle1_Long;
        private string _GPSParcelle1_Lat;
        private string _GPSParcelle2_Long;
        private string _GPSParcelle2_Lat;
        private string _GPSParcelle3_Long;
        private string _GPSParcelle3_Lat;        
        private string _Commentaire;
        private string _Tel1;
        private string _Tel2;
        private string _MobileMoney1;
        private string _MobileMoney2;
        private string _MobileMoney3;
        private string _CodeNom;
        private string _NomPrenomRegistre;
        private decimal? _RendementMoyen;
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



        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (Desactive)
                    return 0; // Annulée                                                
                else
                    return 2; // 
            }
        }

        public Int64 Code
        {
            get
            {
                return _Code;
            }

            set
            {
                _Code = value;
            }
        }

        public string CodeRegistre
        {
            get
            {
                return _CodeRegistre;
            }

            set
            {
                _CodeRegistre = value;
            }
        }

        public string CodeExterne1
        {
            get
            {
                return _CodeExterne1;
            }

            set
            {
                _CodeExterne1 = value;
            }
        }

        public string CodeExterne2
        {
            get
            {
                return _CodeExterne2;
            }

            set
            {
                _CodeExterne2 = value;
            }
        }

        public string CodeExterne3
        {
            get
            {
                return _CodeExterne3;
            }

            set
            {
                _CodeExterne3 = value;
            }
        }

        public string Nom
        {
            get
            {
                return _Nom;
            }

            set
            {
                _Nom = value;
            }
        }

        public string Prenom
        {
            get
            {
                return _Prenom;
            }

            set
            {
                _Prenom = value;
            }
        }

        public string Village
        {
            get
            {
                return _Village;
            }

            set
            {
                _Village = value;
            }
        }

        public string Section
        {
            get
            {
                return _Section;
            }

            set
            {
                _Section = value;
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

        public string NumeroDePiece
        {
            get
            {
                return _NumeroDePiece;
            }

            set
            {
                _NumeroDePiece = value;
            }
        }

        public PieceType PieceType
        {
            get
            {
                return _PieceType;
            }

            set
            {
                _PieceType = value;
            }
        }

        public int NombreDeParcelles
        {
            get
            {
                return _NombreDeParcelles;
            }

            set
            {
                _NombreDeParcelles = value;
            }
        }

        public double? SuperficieTotale
        {
            get
            {
                return _SuperficieTotale;
            }

            set
            {
                _SuperficieTotale = value;
            }
        }

        public bool Certifie
        {
            get
            {
                return _Certifie;
            }

            set
            {
                _Certifie = value;
            }
        }

        public string Projet1
        {
            get
            {
                return _Projet1;
            }

            set
            {
                _Projet1 = value;
            }
        }

        public string Projet2
        {
            get
            {
                return _Projet2;
            }

            set
            {
                _Projet2 = value;
            }
        }

        public double? QuotaProjet1
        {
            get
            {
                return _QuotaProjet1;
            }

            set
            {
                _QuotaProjet1 = value;
            }
        }

        public double? QuotaProjet2
        {
            get
            {
                return _QuotaProjet2;
            }

            set
            {
                _QuotaProjet2 = value;
            }
        }

        public int NombreEnfants
        {
            get
            {
                return _NombreEnfants;
            }

            set
            {
                _NombreEnfants = value;
            }
        }

        public string GPSParcelle1_Long
        {
            get
            {
                return _GPSParcelle1_Long;
            }

            set
            {
                _GPSParcelle1_Long = value;
            }
        }

        public string GPSParcelle1_Lat
        {
            get
            {
                return _GPSParcelle1_Lat;
            }

            set
            {
                _GPSParcelle1_Lat = value;
            }
        }

        public string GPSParcelle2_Long
        {
            get
            {
                return _GPSParcelle2_Long;
            }

            set
            {
                _GPSParcelle2_Long = value;
            }
        }

        public string GPSParcelle2_Lat
        {
            get
            {
                return _GPSParcelle2_Lat;
            }

            set
            {
                _GPSParcelle2_Lat = value;
            }
        }

        public string GPSParcelle3_Long
        {
            get
            {
                return _GPSParcelle3_Long;
            }

            set
            {
                _GPSParcelle3_Long = value;
            }
        }

        public string GPSParcelle3_Lat
        {
            get
            {
                return _GPSParcelle3_Lat;
            }

            set
            {
                _GPSParcelle3_Lat = value;
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

        public string Tel1
        {
            get
            {
                return _Tel1;
            }

            set
            {
                _Tel1 = value;
            }
        }

        public string Tel2
        {
            get
            {
                return _Tel2;
            }

            set
            {
                _Tel2 = value;
            }
        }

        public string MobileMoney1
        {
            get
            {
                return _MobileMoney1;
            }

            set
            {
                _MobileMoney1 = value;
            }
        }

        public string MobileMoney2
        {
            get
            {
                return _MobileMoney2;
            }

            set
            {
                _MobileMoney2 = value;
            }
        }

        public string MobileMoney3
        {
            get
            {
                return _MobileMoney3;
            }

            set
            {
                _MobileMoney3 = value;
            }
        }

        public string CodeNom
        {
            get
            {
                return _CodeNom;
            }

            set
            {
                _CodeNom = value;
            }
        }

        public string NomPrenomRegistre
        {
            get
            {
                return _NomPrenomRegistre;
            }

            set
            {
                _NomPrenomRegistre = value;
            }
        }

        public decimal? RendementMoyen
        {
            get
            {
                return _RendementMoyen;
            }

            set
            {
                _RendementMoyen = value;
            }
        }

        #endregion

        #region Constructor
        public Producteur()
        {

        }

        public Producteur(Guid myId)
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
            DataCommand mCommande = db().CreateStoredProcCommand("V4_Producteur_DeActivate");
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
                throw new Exception(ex.Message + "\r\n" + "Producteur:fnDeActivate");
            }
            return bolResult;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("V4_Producteur_Get", (Guid)Id);
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
            return fnSelect(-1, null, null, -1);
        }

        public virtual List<DataPersist> fnSelect(Int64 mCode, string nom, string prenom, int statut)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V4_Producteur_Select");

                db().AddInParameter(mCommande, "@Code", SqlDbType.BigInt, mCode);
                db().AddInParameter(mCommande, "@Nom", SqlDbType.VarChar, nom);
                db().AddInParameter(mCommande, "@Prenom", SqlDbType.VarChar, prenom);                
                db().AddInParameter(mCommande, "@Statut", SqlDbType.Int, statut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Producteur mClass = new Producteur();

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

        public virtual List<DataPersist> fnSelectForDelivery(Guid livraisonID)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("V4_Producteur_SelectForDelivery");

                db().AddInParameter(mCommande, "@livraisonID", SqlDbType.UniqueIdentifier, livraisonID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Producteur mClass = new Producteur();

                    MapFromDataReaderLite(mClass, mDataReader);
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
                    mCommande = db().CreateStoredProcCommand("V4_Producteur_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddOutParameter(mCommande, "@Code", SqlDbType.BigInt,0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("V4_Producteur_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@CodeRegistre", SqlDbType.VarChar, _CodeRegistre);
                db().AddInParameter(mCommande, "@CodeExterne1", SqlDbType.VarChar, _CodeExterne1);
                db().AddInParameter(mCommande, "@CodeExterne2", SqlDbType.VarChar, _CodeExterne2);
                if (string.IsNullOrEmpty(_CodeExterne3))
                    db().AddInParameter(mCommande, "@CodeExterne3", SqlDbType.VarChar, DBNull.Value);
                else db().AddInParameter(mCommande, "@CodeExterne3", SqlDbType.VarChar, _CodeExterne3);

                db().AddInParameter(mCommande, "@Nom", SqlDbType.VarChar, _Nom);
                db().AddInParameter(mCommande, "@Prenom", SqlDbType.VarChar, _Prenom);

                if (string.IsNullOrEmpty(_Village))
                    db().AddInParameter(mCommande, "@Village", SqlDbType.VarChar, DBNull.Value);
                else db().AddInParameter(mCommande, "@Village", SqlDbType.VarChar, _Village);

                if (string.IsNullOrEmpty(_Section))
                    db().AddInParameter(mCommande, "@Section", SqlDbType.VarChar, DBNull.Value);
                else db().AddInParameter(mCommande, "@Section", SqlDbType.VarChar, _Section);

                db().AddInParameter(mCommande, "@NumeroDePiece", SqlDbType.VarChar, _NumeroDePiece);
                db().AddInParameter(mCommande, "@TypeDePiece", SqlDbType.Int, _PieceType.ID);
                db().AddInParameter(mCommande, "@NombreDeParcelles", SqlDbType.Int, _NombreDeParcelles);
                db().AddInParameter(mCommande, "@SuperficieTotale", SqlDbType.Float, _SuperficieTotale);
                db().AddInParameter(mCommande, "@Certifie", SqlDbType.Bit, _Certifie);

                if (string.IsNullOrEmpty(_Projet1))
                    db().AddInParameter(mCommande, "@Projet1", SqlDbType.VarChar, DBNull.Value);
                else db().AddInParameter(mCommande, "@Projet1", SqlDbType.VarChar, _Projet1);

                if (string.IsNullOrEmpty(_Projet2))
                    db().AddInParameter(mCommande, "@Projet2", SqlDbType.VarChar, DBNull.Value);
                else db().AddInParameter(mCommande, "@Projet2", SqlDbType.VarChar, _Projet2);

                db().AddInParameter(mCommande, "@QuotaProjet1", SqlDbType.Float, _QuotaProjet1);
                db().AddInParameter(mCommande, "@QuotaProjet2", SqlDbType.Float, _QuotaProjet2);
                db().AddInParameter(mCommande, "@NombreEnfants", SqlDbType.Int, _NombreEnfants);

                if (string.IsNullOrEmpty(_GPSParcelle1_Long))
                    db().AddInParameter(mCommande, "@GPSParcelle1_Long", SqlDbType.VarChar, DBNull.Value);
                else db().AddInParameter(mCommande, "@GPSParcelle1_Long", SqlDbType.VarChar, _GPSParcelle1_Long);

                if (string.IsNullOrEmpty(_GPSParcelle1_Lat))
                    db().AddInParameter(mCommande, "@GPSParcelle1_Lat", SqlDbType.VarChar, DBNull.Value);
                else db().AddInParameter(mCommande, "@GPSParcelle1_Lat", SqlDbType.VarChar, _GPSParcelle1_Lat);

                if (string.IsNullOrEmpty(_GPSParcelle2_Long))
                    db().AddInParameter(mCommande, "@GPSParcelle2_Long", SqlDbType.VarChar, DBNull.Value);
                else db().AddInParameter(mCommande, "@GPSParcelle2_Long", SqlDbType.VarChar, _GPSParcelle2_Long);

                if (string.IsNullOrEmpty(_GPSParcelle2_Lat))
                    db().AddInParameter(mCommande, "@GPSParcelle2_Lat", SqlDbType.VarChar, DBNull.Value);
                else db().AddInParameter(mCommande, "@GPSParcelle2_Lat", SqlDbType.VarChar, _GPSParcelle2_Lat);

                if (string.IsNullOrEmpty(_GPSParcelle3_Long))
                    db().AddInParameter(mCommande, "@GPSParcelle3_Long", SqlDbType.VarChar, DBNull.Value);
                else db().AddInParameter(mCommande, "@GPSParcelle3_Long", SqlDbType.VarChar, _GPSParcelle3_Long);

                if (string.IsNullOrEmpty(_GPSParcelle3_Lat))
                    db().AddInParameter(mCommande, "@GPSParcelle3_Lat", SqlDbType.VarChar, DBNull.Value);
                else db().AddInParameter(mCommande, "@GPSParcelle3_Lat", SqlDbType.VarChar, _GPSParcelle3_Lat);

                db().AddInParameter(mCommande, "@Commentaire", SqlDbType.VarChar, _Commentaire);
                db().AddInParameter(mCommande, "@tel1", SqlDbType.VarChar, _Tel1);
                db().AddInParameter(mCommande, "@tel2", SqlDbType.VarChar, _Tel2);
                db().AddInParameter(mCommande, "@MobileMoney1", SqlDbType.VarChar, _MobileMoney1);
                db().AddInParameter(mCommande, "@MobileMoney2", SqlDbType.VarChar, _MobileMoney2);
                db().AddInParameter(mCommande, "@MobileMoney3", SqlDbType.VarChar, _MobileMoney3);                
                db().AddInParameter(mCommande, "@RendementMoyen", SqlDbType.Decimal, _RendementMoyen);

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
                            Code = (Int64)db().Parameters(mCommande, "@Code");
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
                throw new Exception(ex.Message + "\r\n" + "Producteur:fnUpdate");

            }
            return Result;
        }

        private static void MapFromDataReader(Producteur mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass.ID = (Guid)mDataReader["ID"];                                                                          
                    if (!DBNull.Value.Equals(mDataReader["Code"])) mClass._Code = (Int64)mDataReader["Code"];
                    if (!DBNull.Value.Equals(mDataReader["CodeRegistre"])) mClass._CodeRegistre = (string)mDataReader["CodeRegistre"];
                    if (!DBNull.Value.Equals(mDataReader["CodeExterne1"])) mClass._CodeExterne1 = (string)mDataReader["CodeExterne1"];
                    if (!DBNull.Value.Equals(mDataReader["CodeExterne2"])) mClass._CodeExterne2 = (string)mDataReader["CodeExterne2"];
                    if (!DBNull.Value.Equals(mDataReader["CodeExterne3"])) mClass._CodeExterne3 = (string)mDataReader["CodeExterne3"];
                    if (!DBNull.Value.Equals(mDataReader["Nom"])) mClass._Nom = (string)mDataReader["Nom"];
                    if (!DBNull.Value.Equals(mDataReader["Prenom"])) mClass._Prenom = (string)mDataReader["Prenom"];
                    if (!DBNull.Value.Equals(mDataReader["Village"])) mClass._Village = (string)mDataReader["Village"];
                    if (!DBNull.Value.Equals(mDataReader["Section"])) mClass._Section = (string)mDataReader["Section"];
                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroDePiece"])) mClass._NumeroDePiece = (string)mDataReader["NumeroDePiece"];
                    if (!DBNull.Value.Equals(mDataReader["TypeDePiece"]))
                    {
                        mClass._PieceType = new PieceType();
                        mClass._PieceType.ID = (int)mDataReader["TypeDePiece"];
                        mClass._PieceType.Designation = (string)mDataReader["LibellePieceType"];
                    }

                    if (!DBNull.Value.Equals(mDataReader["NombreDeParcelles"])) mClass._NombreDeParcelles = (int)mDataReader["NombreDeParcelles"];
                    if (!DBNull.Value.Equals(mDataReader["SuperficieTotale"])) mClass._SuperficieTotale = (double)mDataReader["SuperficieTotale"];
                    if (!DBNull.Value.Equals(mDataReader["Certifie"])) mClass._Certifie = (bool)mDataReader["Certifie"];
                    if (!DBNull.Value.Equals(mDataReader["Projet1"])) mClass._Projet1 = (string)mDataReader["Projet1"];
                    if (!DBNull.Value.Equals(mDataReader["Projet2"])) mClass._Projet2 = (string)mDataReader["Projet2"];
                    if (!DBNull.Value.Equals(mDataReader["QuotaProjet1"])) mClass._QuotaProjet1 = (double)mDataReader["QuotaProjet1"];
                    if (!DBNull.Value.Equals(mDataReader["QuotaProjet2"])) mClass._QuotaProjet2 = (double)mDataReader["QuotaProjet2"];
                    if (!DBNull.Value.Equals(mDataReader["NombreEnfants"])) mClass._NombreEnfants = (int)mDataReader["NombreEnfants"];
                    if (!DBNull.Value.Equals(mDataReader["GPSParcelle1_Long"])) mClass._GPSParcelle1_Long = (string)mDataReader["GPSParcelle1_Long"];
                    if (!DBNull.Value.Equals(mDataReader["GPSParcelle1_Lat"])) mClass._GPSParcelle1_Lat = (string)mDataReader["GPSParcelle1_Lat"];
                    if (!DBNull.Value.Equals(mDataReader["GPSParcelle2_Long"])) mClass._GPSParcelle2_Long = (string)mDataReader["GPSParcelle2_Long"];
                    if (!DBNull.Value.Equals(mDataReader["GPSParcelle2_Lat"])) mClass._GPSParcelle2_Lat = (string)mDataReader["GPSParcelle2_Lat"];
                    if (!DBNull.Value.Equals(mDataReader["GPSParcelle3_Long"])) mClass._GPSParcelle3_Long = (string)mDataReader["GPSParcelle3_Long"];
                    if (!DBNull.Value.Equals(mDataReader["GPSParcelle3_Lat"])) mClass._GPSParcelle3_Lat = (string)mDataReader["GPSParcelle3_Lat"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Commentaire = (string)mDataReader["Commentaire"];
                    if (!DBNull.Value.Equals(mDataReader["Tel1"])) mClass._Tel1 = (string)mDataReader["Tel1"];
                    if (!DBNull.Value.Equals(mDataReader["Tel2"])) mClass._Tel2 = (string)mDataReader["Tel2"];
                    if (!DBNull.Value.Equals(mDataReader["MobileMoney1"])) mClass._MobileMoney1 = (string)mDataReader["MobileMoney1"];
                    if (!DBNull.Value.Equals(mDataReader["MobileMoney2"])) mClass._MobileMoney2 = (string)mDataReader["MobileMoney2"];
                    if (!DBNull.Value.Equals(mDataReader["MobileMoney3"])) mClass._MobileMoney3 = (string)mDataReader["MobileMoney3"];
                    
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["DateCreation"])) mClass._DateCreation = (DateTime)mDataReader["DateCreation"];
                    if (!DBNull.Value.Equals(mDataReader["DateModification"])) mClass._DateModification = (DateTime)mDataReader["DateModification"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];
                    if (!DBNull.Value.Equals(mDataReader["RendementMoyen"])) mClass._RendementMoyen = (decimal)mDataReader["RendementMoyen"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n Producteur:MapFromDataReader");
            }
        }

        private static void MapFromDataReaderLite(Producteur mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass.ID = (Guid)mDataReader["ID"];
                    if (!DBNull.Value.Equals(mDataReader["Code"])) mClass._Code = (Int64)mDataReader["Code"];                    
                    if (!DBNull.Value.Equals(mDataReader["Nom"])) mClass._Nom = (string)mDataReader["Nom"];
                    if (!DBNull.Value.Equals(mDataReader["Prenom"])) mClass._Prenom = (string)mDataReader["Prenom"];
                    if (!DBNull.Value.Equals(mDataReader["CodeNom"])) mClass._CodeNom = (string)mDataReader["CodeNom"];
                    if (!DBNull.Value.Equals(mDataReader["CodeRegistre"])) mClass._CodeRegistre = (string)mDataReader["CodeRegistre"];
                    if (!DBNull.Value.Equals(mDataReader["CodeExterne1"])) mClass._CodeExterne1 = (string)mDataReader["CodeExterne1"];
                    if (!DBNull.Value.Equals(mDataReader["CodeExterne2"])) mClass._CodeExterne2 = (string)mDataReader["CodeExterne2"];
                    if (!DBNull.Value.Equals(mDataReader["CodeExterne3"])) mClass._CodeExterne3 = (string)mDataReader["CodeExterne3"];
                    if (!DBNull.Value.Equals(mDataReader["NomPrenomRegistre"])) mClass._NomPrenomRegistre = (string)mDataReader["NomPrenomRegistre"];                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n Producteur:MapFromDataReader");
            }
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public partial class ProducteurViewModel
    {
        public Producteur _Producteur { get; set; }        

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }


}
