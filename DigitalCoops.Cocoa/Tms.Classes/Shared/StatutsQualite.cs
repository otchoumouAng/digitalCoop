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
using Tms.Classes.Shared.Sales;

namespace Tms.Classes.Shared
{
    public class StatutsQualite : DataPersist
    {
        #region fields
        private Guid _ID;
        private OrdreFabrication _OrdreFabrication;

        private QAStatus _QAStatus;
        private Guid _PaletteID;

        private string _PaletteIDAsString;

        private int _Annee;
        private int _Semaine;
        private string _NumeroDeFabrication;
        private string _ArticleDesignation;
        private string _ArticleCodeInterne;
        private string _ProduitFiniDesignation;
        private string _TypeProduitDesignation;
        private DateTime _DateDeDefinition;


        private string _QAStatuts;

        private string _Statut;
        private bool _Desactive;
        private bool _IsApproved;
        #endregion

        #region Properties
        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }


        public int Annee
        {
            get { return _Annee; }
            set { _Annee = value; }
        }

        public int Semaine
        {
            get { return _Semaine; }
            set { _Semaine = value; }
        }

        public QAStatus QAStatus
        {
            get { return _QAStatus; }
            set { _QAStatus = value; }
        }

        public Guid PaletteID
        {
            get { return _PaletteID; }
            set { _PaletteID = value; }
        }

        public string PaletteIDAsString
        {
            get { return _PaletteIDAsString; }
            set { _PaletteIDAsString = value; }

        }


        public OrdreFabrication OrdreFabrication
        {
            get { return _OrdreFabrication; }
            set { _OrdreFabrication = value; }
        }

        public string NumeroDeFabrication
        {
            get { return _NumeroDeFabrication; }
            set { _NumeroDeFabrication = value; }
        }

        public string ArticleDesignation
        {
            get { return _ArticleDesignation; }
            set { _ArticleDesignation = value; }
        }

        public string ArticleCodeInterne
        {
            get { return _ArticleCodeInterne; }
            set { _ArticleCodeInterne = value; }
        }

        public string ProduitFiniDesignation
        {
            get { return _ProduitFiniDesignation; }
            set { _ProduitFiniDesignation = value; }
        }

        public string TypeProduitDesignation
        {
            get { return _TypeProduitDesignation; }
            set { _TypeProduitDesignation = value; }
        }

        public DateTime DateDeDefinition
        {
            get { return _DateDeDefinition; }
            set { _DateDeDefinition = value; }
        }

        public string QAStatuts
        {
            get { return _QAStatuts; }
            set { _QAStatuts = value; }
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

        [Column(Text = "IsApproved")]
        public bool IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }

        public int mIcon
        {
            get
            {
                if (_Desactive || _Statut == "CA")
                    return 0; // BulletCross
                else if (_IsApproved || _Statut == "AP")
                    return 1; // Tick                
                else
                    return 2; //                     
            }
        }

        #endregion

        #region Constructor
        public StatutsQualite()
        {

        }

        public StatutsQualite(Guid myId)
        {
            this.fnGet(myId);
        }
        #endregion

        #region Methods
        public override bool fnActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("PP_StatutsQualite_Activate");
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
                        _Desactive = false;
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
                throw new Exception(ex.Message + "\r\n" + "StatutsQualite:fnDeActivate");
            }
            return bolResult;
        }

        public override bool fnDeActivate()
        {
            bool bolResult;
            DataCommand mCommande = db().CreateStoredProcCommand("PP_StatutsQualite_DeActivate");
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
                        _Desactive = true;
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
                throw new Exception(ex.Message + "\r\n" + "StatutsQualite:fnDeActivate");
            }
            return bolResult;
        }

        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("PP_StatutsQualite_Get", (Guid)Id);
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
            return fnSelect("-1", "-1", -1, -1, "-1");
        }

        public List<DataPersist> fnSelect(string mAnnee, string mSemaine, int mProduitID, int mProduitTypeID, string mStatut)

        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("PP_StatutsQualite_Select");
                db().AddInParameter(mCommande, "@Annee", SqlDbType.VarChar, mAnnee);
                db().AddInParameter(mCommande, "@Semaine", SqlDbType.VarChar, mSemaine);
                db().AddInParameter(mCommande, "@ProduitID", SqlDbType.Int, mProduitID);
                db().AddInParameter(mCommande, "@ProduitTypeID", SqlDbType.Int, mProduitTypeID);
                db().AddInParameter(mCommande, "@statut", SqlDbType.Char, 2, mStatut);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    StatutsQualite mClass = new StatutsQualite();
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
                    mCommande = db().CreateStoredProcCommand("PP_StatutsQualite_New");
                    db().AddOutParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, 0);
                    db().AddInParameter(mCommande, "@CreationUtilisateur", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("PP_StatutsQualite_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                    db().AddInParameter(mCommande, "@ModificationUtilisateur", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@Statut", SqlDbType.VarChar, _Statut);
                db().AddInParameter(mCommande, "@Desactive", SqlDbType.Bit, _Desactive);
                db().AddInParameter(mCommande, "@IsApproved", SqlDbType.Bit, _IsApproved);


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
                throw new Exception(ex.Message + "\r\n" + "StatutsQualite:fnUpdate");

            }
            return Result;
        }


        public bool fnUpdatePickedPalette()
        {
            bool Result;
            DataCommand mCommande;
            try
            {
              
                mCommande = db().CreateStoredProcCommand("PP_Palette_Modify_QAStatus");
                db().AddInParameter(mCommande, "@ID", SqlDbType.UniqueIdentifier, _ID);
                db().AddInParameter(mCommande, "@ModificationUtilisateur", SqlDbType.VarChar, _UtilisateurModification);

                db().AddInParameter(mCommande, "@PaletteID", SqlDbType.UniqueIdentifier, (Guid)_PaletteID);
                db().AddInParameter(mCommande, "@QAStatusID", SqlDbType.Int, (int)_QAStatus.ID);

                db().AddParameter(mCommande, "ReturnValue", SqlDbType.Int, 0, null, ParameterDirection.ReturnValue);
                db().AddOutParameter(mCommande, "@ErrorMessage", SqlDbType.VarChar, 1000);
                db().ExecuteNonQuery(ref mCommande);
                switch ((int)db().Parameters(mCommande, "ReturnValue"))
                {
                    case 0:
                        //Everything OK
                        base.UpdateAuditFields();
                        Result = true;
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
                throw new Exception(ex.Message + "\r\n" + "StatutsQualite:fnUpdatePickedPalette");
            }
            return Result;
        }



        public override string ToString()
        {
            return _ID.ToString();
        }



        private static void MapFromDataReader(StatutsQualite mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["Annee"])) mClass._Annee = (int)mDataReader["Annee"];
                    if (!DBNull.Value.Equals(mDataReader["Semaine"])) mClass._Semaine = (int)mDataReader["Semaine"];
                    if (!DBNull.Value.Equals(mDataReader["NumeroDeFabrication"])) mClass._NumeroDeFabrication = (string)mDataReader["NumeroDeFabrication"];
                    if (!DBNull.Value.Equals(mDataReader["ArticleDesignation"])) mClass._ArticleDesignation = (string)mDataReader["ArticleDesignation"];
                    if (!DBNull.Value.Equals(mDataReader["ArticleCodeInterne"])) mClass._ArticleCodeInterne = (string)mDataReader["ArticleCodeInterne"];
                    if (!DBNull.Value.Equals(mDataReader["ProduitFiniDesignation"])) mClass._ProduitFiniDesignation = (string)mDataReader["ProduitFiniDesignation"];
                    if (!DBNull.Value.Equals(mDataReader["TypeProduitDesignation"])) mClass._TypeProduitDesignation = (string)mDataReader["TypeProduitDesignation"];
                    if (!DBNull.Value.Equals(mDataReader["QAStatuts"])) mClass._QAStatuts = (string)mDataReader["QAStatuts"];

                    if (!DBNull.Value.Equals(mDataReader["Desactive"])) mClass._Desactive = (bool)mDataReader["Desactive"];
                    if (!DBNull.Value.Equals(mDataReader["Statut"])) mClass._Statut = (string)mDataReader["Statut"];
                    if (!DBNull.Value.Equals(mDataReader["IsApproved"])) mClass._IsApproved = (bool)mDataReader["IsApproved"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass._UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass._DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass._DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass._UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass._RowVersionKey = (object)mDataReader["RowVersionKey"];
                    //if (!DBNull.Value.Equals(mDataReader["EstFictif"])) mClass._EstFictif = (bool)mDataReader["EstFictif"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nStatutsQualite:MapFromDataReader");
            }
        }


        #endregion
    }

    public partial class StatutsQualiteViewModel
    {
        public StatutsQualite _StatutsQualite { get; set; }

        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
