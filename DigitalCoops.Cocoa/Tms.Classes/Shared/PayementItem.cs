using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Business;
using Tms.Components.Data;

namespace Tms.Classes.Shared
{    

    public class PayementItem : DataPersist
    {
        #region Fields
        private Guid _ID;
        private string _PayementType;
        private string _Description;
        private decimal _Montant;
        private decimal _Solde;
        private string _Reference;
        private DateTime _DateItem;
        private Fournisseur _Fournisseur;
        private decimal _OldMontant;
        private decimal _NewMontant;

        #endregion

        #region Properties

        [ModelField(IDProperty = true)]
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string PayementType
        {
            get { return _PayementType; }
            set { _PayementType = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }


        public decimal Montant
        {
            get { return _Montant; }
            set { _Montant = value; }
        }

        public string MontantAsString
        {
            get { return _Montant != 0 ? String.Format("{0:# ### ### ### ###}", _Montant).TrimStart() : string.Empty; }            
        }

        public string OldMontantAsString
        {
            get { return _Montant != 0 ? String.Format("{0:# ### ### ### ###}", _Montant).TrimStart() : string.Empty; }
        }

        public string SoldeAsString
        {
            get { return _Solde != 0 ? String.Format("{0:#,#}", _Solde).TrimStart() : string.Empty; }
        }

        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
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

        public DateTime DateItem
        {
            get
            {
                return _DateItem;
            }

            set
            {
                _DateItem = value;
            }
        }

        public string DateItemAsString
        {
            get
            {
                return _DateItem != null ? _DateItem.ToString() : string.Empty;
            }            
        }

        public decimal Solde
        {
            get
            {
                return _Solde;
            }

            set
            {
                _Solde = value;
            }
        }

        public decimal OldMontant
        {
            get
            {
                return _OldMontant;
            }

            set
            {
                _OldMontant = value;
            }
        }

        public decimal NewMontant
        {
            get
            {
                return _NewMontant;
            }

            set
            {
                _NewMontant = value;
            }
        }

        #endregion

        #region "Constructor"

        public PayementItem()
        {

        }

        public PayementItem(Guid myId)
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
            throw new NotImplementedException();
        }

        public override List<DataPersist> fnSelect()
        {
            return fnSelect("", -1);
        }

        public List<DataPersist> fnSelect(string campagneID, int fournisseurID)
        {            
            throw new NotImplementedException();
        }

        public List<DataPersist> fnSelectDeliveryToPay(int fournisseurID, int SiteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Payement_SelectDeliveryToPay");
                //db().AddInParameter(mCommande, "@campagneID", SqlDbType.VarChar, campagneID);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, fournisseurID);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, SiteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PayementItem mClass = new PayementItem();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectDeliveryToPay");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectFinancingToPay(int fournisseurID = -1, int SiteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Payement_SelectFinancingToPay");
                //db().AddInParameter(mCommande, "@campagneID", SqlDbType.VarChar, campagneID);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, fournisseurID);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, SiteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PayementItem mClass = new PayementItem();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectDeliveryToPay");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectSavingToPay(int fournisseurID, int SiteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Payement_SelectSavingToPay");
                //db().AddInParameter(mCommande, "@campagneID", SqlDbType.VarChar, campagneID);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, fournisseurID);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, SiteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PayementItem mClass = new PayementItem();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectDeliveryToPay");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectBonusToPay(int fournisseurID, int SiteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Payement_SelectBonusToPay");
                //db().AddInParameter(mCommande, "@campagneID", SqlDbType.VarChar, campagneID);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, fournisseurID);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, SiteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PayementItem mClass = new PayementItem();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectBonusToPay");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<DataPersist> fnSelectCommissionToPay(int fournisseurID, int SiteID = -1)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("Payement_SelectCommissionToPay");
                //db().AddInParameter(mCommande, "@campagneID", SqlDbType.VarChar, campagneID);
                db().AddInParameter(mCommande, "@fournisseurID", SqlDbType.Int, fournisseurID);
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, SiteID);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    PayementItem mClass = new PayementItem();

                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectBonusToPay");
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

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region "Private Members"
        

        private static void MapFromDataReader(PayementItem mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (Guid)mDataReader["ID"];

                    if (!DBNull.Value.Equals(mDataReader["Numero"])) mClass._Reference = (string)mDataReader["Numero"];
                    if (!DBNull.Value.Equals(mDataReader["ItemType"])) mClass._PayementType = (string)mDataReader["ItemType"];
                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._Montant = (decimal)mDataReader["Montant"];
                    if (!DBNull.Value.Equals(mDataReader["Montant"])) mClass._OldMontant = (decimal)mDataReader["Montant"];

                    if (!DBNull.Value.Equals(mDataReader["Solde"])) mClass._Solde = (decimal)mDataReader["Solde"];
                    if (!DBNull.Value.Equals(mDataReader["Solde"])) mClass._NewMontant = (decimal)mDataReader["Solde"];

                    if (!DBNull.Value.Equals(mDataReader["DateFacture"])) mClass._DateItem = (DateTime)mDataReader["DateFacture"];
                    if (!DBNull.Value.Equals(mDataReader["Commentaire"])) mClass._Description = (string)mDataReader["Commentaire"];

                    mClass.Fournisseur = new Fournisseur();
                    if (!DBNull.Value.Equals(mDataReader["fournisseurID"])) mClass._Fournisseur.ID = (int)mDataReader["fournisseurID"];
                    if (!DBNull.Value.Equals(mDataReader["fournisseurNom"])) mClass._Fournisseur.Nom = (string)mDataReader["fournisseurNom"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nPayementItem:MapFromDataReader");
            }
        }
        #endregion

    }
}
