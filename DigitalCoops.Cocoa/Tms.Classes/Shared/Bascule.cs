using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Components.Data;

namespace Tms.Classes.Shared
{
    public class Bascule : DataPersist
    {
        #region "Fields"

        private int _ID;
        private Site _Site;
        private string _Designation;
        private string _Ordinateur;
        private Protocole _Protocole;
        private int _ComVitesse;
        private short _ComParite;
        private short _ComBitsDonnees;
        private short _ComBitsStop;
        private string _ComPort;
        private int _ComTimeOut;
        private bool _Desative;

        private SerialPort myComObj;

        #endregion

        public Bascule()
        {
        }

        public Bascule(int myId)
        {
            this.fnGet(myId);
        }

        public Bascule(string mComputerName)
        {
            myComObj = new SerialPort();
            if (this.fnGetByMachineName(mComputerName))
            {
                myComObj.BaudRate = _ComVitesse;
                myComObj.Parity = (Parity)_ComParite;
                myComObj.DataBits = _ComBitsDonnees;
                myComObj.StopBits = (StopBits)_ComBitsStop;
                myComObj.PortName = _ComPort;
                myComObj.ReadTimeout = _ComTimeOut;
                //_IsScalingDevice = true;
            }
            //else
            //{
            //    _IsScalingDevice = false;
            //}
        }

        #region "Properties"
        [ModelField(IDProperty = true)]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Site Site
        {
            get { return _Site; }
            set { _Site = value; }
        }

        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }

        public string Ordinateur
        {
            get { return _Ordinateur; }
            set { _Ordinateur = value; }
        }

        public Protocole Protocole
        {
            get { return _Protocole; }
            set { _Protocole = value; }
        }

        public string ProtocoleAsString
        {
            get { return _Protocole != null ? _Protocole.Designation : string.Empty; }
        }

        public int ComVitesse
        {
            get { return _ComVitesse; }
            set { _ComVitesse = value; }
        }

        public short ComParite
        {
            get { return _ComParite; }
            set { _ComParite = value; }
        }

        public short ComBitsDonnees
        {
            get { return _ComBitsDonnees; }
            set { _ComBitsDonnees = value; }
        }

        public short ComBitsStop
        {
            get { return _ComBitsStop; }
            set { _ComBitsStop = value; }
        }

        public string ComPort
        {
            get { return _ComPort; }
            set { _ComPort = value; }
        }

        public int ComTimeOut
        {
            get { return _ComTimeOut; }
            set { _ComTimeOut = value; }
        }

        public bool Desactive
        {
            get { return _Desative; }
            set { _Desative = value; }
        }

        public string AsString
        {
            get { return Designation.ToString(); }
        }

        //DF=defaut, RF=Refoulee, AN=Annulee
        [Column(Text = "")]
        public int mIcon
        {
            get
            {
                if (_Desative)
                    return 0; // Annulée                
                else
                    return 2; // Normal                                   
            }
        }
        #endregion

        #region Methods
        public bool fnGetByMachineName(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("PontBascule_GetByMachineName", (string)Id);
                if (mDataReader.Read())
                {
                    MapFromDataReader(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetByMachineName");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public bool fnGetByUserName(object userName)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("PontBascule_GetByUserName", (string)userName);
                if (mDataReader.Read())
                {
                    MapFromDataReader(this, mDataReader);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().Name + ":fnGetByUserName");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public List<Bascule> fnSelectBySite(int mSiteId)
        {
            List<Bascule> mList = new List<Bascule>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("PontBascule_SelectBySite");
                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, mSiteId);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Bascule mClass = new Bascule();
                    MapFromDataReader(mClass, mDataReader);
                    mList.Add(mClass);
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n" + this.GetType().FullName + ":fnSelectBySite");
            }
            finally
            {
                if (mDataReader != null) mDataReader.Close();
            }
        }

        public override List<DataPersist> fnSelect()
        {
            return fnSelect(0);
        }

        public List<DataPersist> fnSelect(short mStatus)
        {
            List<DataPersist> mList = new List<DataPersist>();
            IDataReader mDataReader = null;

            try
            {
                DataCommand mCommande = db().CreateStoredProcCommand("PontBascule_Select");
                db().AddInParameter(mCommande, "@Status", SqlDbType.SmallInt, mStatus);
                mDataReader = db().ExecuteReader(mCommande);

                while (mDataReader.Read())
                {
                    Bascule mClass = new Bascule();
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

        public int fnRead()
        {
            if (_Designation == string.Empty)
            {
                return -5;
            }

            switch (_Protocole.ID)
            {
                case 1:
                    //Eric 
                    return ReadUsingEric();

                case 2:
                    //Metler 1
                    return ReadUsingMetler1();

                case 3:
                    //Metler 2
                    return ReadUsingMetler2();
                case 4:
                    //Precia Molen i200
                    return ReadUsingMaitreA();
                case 5:
                    //Precia Molen i200
                    return ReadUsingEsclave();
                case 6:
                    //Precia Molen i200
                    return ReadUsinBilanciai();
                case 7:
                    return ReadUsingMaitre_HKF();
                default:
                    return -6;
            }

        }

        //Eric Protocol used by Arpege Master K
        private int ReadUsingEric()
        {
            try
            {
                char[] mBuffer = new char[9];
                string mData = string.Empty;
                int mWeight = -10;

                //Open port 
                               
                myComObj.Open();
                //Send request to indicator 
                myComObj.Write("P0");

                //wait 100 ms
                System.Threading.Thread.Sleep(500);

                //Reads eight caracters from com  
                int mLength = myComObj.Read(mBuffer, 0, 8);

                //Close port 
                myComObj.Close();

                //Data lenght > 8 ? 
                if (mLength >= 8)
                {
                    //Get data from one-dimentional array 
                    for (int i = 0; i <= mLength - 1; i++)
                    {
                        mData = mData + mBuffer[i];
                    }

                    //Weight is stable ? 
                    if (mData.Substring(1, 1) == "I")
                    {
                        mData = mData.Substring(2, 5);

                    }
                    else
                        throw new Exception("Poids Instable");
                }


                mWeight = int.Parse(mData);

                //if (mWeight < 0)
                //    throw new Exception("Le poids capturé doit être supérieur à 0");

                return mWeight;

            }
            catch (Exception ex)
            {
                myComObj.Close();
                throw new Exception("Erreur lors de la capture du poids : " + ex.Message + (Char)13 + (Char)10 + "Reessayer SVP");
                return -20;
            }
        }

        //Metler Toledo IND protocol (Reading Offset = 5)
        private int ReadUsingMetler1()
        {
            string mData = string.Empty;
            char mChar;
            string sWeight;
            int mWeight;
            int mComparedWeight;

            try
            {
                //Open port 
                myComObj.Open();

                //Start Reading
                mChar = (char)myComObj.ReadChar();
                mData = mData + mChar.ToString();

                //Read at least 150 characters
                while (mData.Length < 150)
                {
                    mChar = (char)myComObj.ReadChar();
                    mData = mData + mChar.ToString();
                }

                //Close port
                myComObj.Close();

                //display data
                //MessageBox.Show(mData);

                //Get Index of carriage return
                char mCret = (char)13;
                int mIndex = mData.IndexOf(mCret);

                //Get data after Cr
                mData = mData.Substring(mIndex + 1);

                //Get Weight
                sWeight = mData.Substring(5, 5);

                //Get first weight
                mWeight = int.Parse(sWeight);

                //Wait 500 ms
                System.Threading.Thread.Sleep(300);

                //Get second time index of Cr
                mIndex = mData.IndexOf(mCret);

                //Get data after Cr
                mData = mData.Substring(mIndex + 1);

                //Get Weight
                sWeight = mData.Substring(5, 5);

                //Get first weight
                mComparedWeight = int.Parse(sWeight);

                if (mWeight != mComparedWeight)
                {
                    throw new Exception("Poids Instable");
                }

                if (mWeight < 0)
                    throw new Exception("Le poids capturé doit être supérieur à 0");

                return mWeight;

            }
            catch (Exception ex)
            {
                myComObj.Close();
                throw new Exception("Erreur lors de la capture du poids : " + ex.Message + (Char)13 + (Char)10 + "Reessayer SVP");
                return -30;
            }
        }

        //Metler Toledo IND protocol (Reading Offset = 6)
        private int ReadUsingMetler2()
        {
            string mData = string.Empty;
            char mChar;
            string sWeight;
            int mWeight;
            int mComparedWeight;

            try
            {
                //Open port 
                myComObj.Open();

                //Start Reading
                mChar = (char)myComObj.ReadChar();
                mData = mData + mChar.ToString();

                //Read at least 150 characters
                while (mData.Length < 150)
                {
                    mChar = (char)myComObj.ReadChar();
                    mData = mData + mChar.ToString();
                }

                //Close port
                myComObj.Close();

                //display data
                //MessageBox.Show(mData);

                //Get Index of carriage return
                char mCret = (char)13;
                int mIndex = mData.IndexOf(mCret);

                //Get data after Cr
                mData = mData.Substring(mIndex + 1);

                //Get Weight
                sWeight = mData.Substring(6, 5);

                //Get first weight
                mWeight = int.Parse(sWeight);

                //Wait 500 ms
                System.Threading.Thread.Sleep(300);

                //Get second time index of Cr
                mIndex = mData.IndexOf(mCret);

                //Get data after Cr
                mData = mData.Substring(mIndex + 1);

                //Get Weight
                sWeight = mData.Substring(6, 5);

                //Get first weight
                mComparedWeight = int.Parse(sWeight);

                if (mWeight != mComparedWeight)
                {
                    throw new Exception("Poids Instable");
                }

                if (mWeight < 0)
                    throw new Exception("Le poids capturé doit être supérieur à 0");

                return mWeight;

            }
            catch (Exception ex)
            {
                myComObj.Close();
                throw new Exception("Erreur lors de la capture du poids : " + ex.Message + (Char)13 + (Char)10 + "Reessayer SVP");
                return -40;
            }
        }

        private int ReadUsingMaitreA()
        {
            string mData = string.Empty;
            string mData2 = string.Empty;
            char mChar;
            string sWeight;
            int mWeight;
            int mComparedWeight;

            try
            {
                //Open port 
                myComObj.Open();

                //Start Reading
                mChar = (char)myComObj.ReadChar();
                mData = mData + mChar.ToString();


                //Read at least 150 characters
                while (mData.Length < 200)
                {
                    mChar = (char)myComObj.ReadChar();
                    mData = mData + mChar.ToString();
                }

                //Close port
                myComObj.Close();

                //display data
                //MessageBox.Show(mData);

                //Get Index of carriage return
                char mCret = (char)01;
                int mIndex = mData.IndexOf(mCret);

                //Get data after Cr
                mData = mData.Substring(mIndex + 1);

                //Get Weight
                sWeight = mData.Substring(10, 6).Replace(" ","");

                //Get first weight
                mWeight = int.Parse(sWeight);

                //Wait 500 ms
                System.Threading.Thread.Sleep(300);

                //Get second time index of Cr
                mIndex = mData.IndexOf(mCret);

                //Get data after Cr
                mData = mData.Substring(mIndex + 1);

                //Get Weight
                sWeight = mData.Substring(10, 6).Replace(" ", "");

                //Get first weight
                mComparedWeight = int.Parse(sWeight);

                if (mWeight != mComparedWeight)
                {
                    throw new Exception("Poids Instable");
                }

                if (mWeight < 0)
                    throw new Exception("Le poids capturé doit être supérieur à 0");

                return mWeight;

            }
            catch (Exception ex)
            {
                myComObj.Close();
                throw new Exception("Erreur lors de la capture du poids : " + ex.Message + (Char)13 + (Char)10 + "Reessayer SVP");
                return -30;
            }
        }

        private int ReadUsingEsclave()
        {
            try
            {
                char[] mBuffer = new char[49];
                byte[] bytesToSend = { 0x01, 0x0D, 0x0A }; // SOH CR LF
                string mData = string.Empty;
                int mWeight = -10;
                
                //Open port 
                myComObj.Open();
                
                //Send request to indicator 
                myComObj.Write(bytesToSend, 0, bytesToSend.Length);

                //wait 100 ms
                System.Threading.Thread.Sleep(500);

                //Reads eight caracters from com  
                int mLength = myComObj.Read(mBuffer, 0, 49);

                //Close port 
                myComObj.Close();

                //Data lenght > 49 ? 
                if (mLength >= 49)
                {
                    //Get data from one-dimentional array 
                    for (int i = 0; i <= mLength - 1; i++)
                    {
                        mData = mData + mBuffer[i];
                    }
                    mData = mData.Substring(11, 6);
                    //Weight is stable ? 
                    //if (mData.Substring(1, 1) == "I")
                    //{
                    //    mData = mData.Substring(2, 5);

                    //}
                    //else
                    //    throw new Exception("Poids Instable");
                }


                mWeight = int.Parse(mData);

                //if (mWeight < 0)
                //    throw new Exception("Le poids capturé doit être supérieur à 0");

                return mWeight;

            }
            catch (Exception ex)
            {
                myComObj.Close();
                throw new Exception("Erreur lors de la capture du poids : " + ex.Message + (Char)13 + (Char)10 + "Reessayer SVP");
                return -20;
            }
        }

        private int ReadUsinBilanciai()
        {

            try
            {
                char[] mBuffer = new char[14];

                string mData = string.Empty;

                int mWeight = -1;
                byte[] bytesToSend = { 0x58, 0x42, 0x0D }; // X B CR
                
                //Open port 
                myComObj.Open();
                              
                //Send request to indicator 
                myComObj.Write(bytesToSend, 0, bytesToSend.Length);

                //Reads 14 caracters from com port 
                System.Threading.Thread.Sleep(500);
                int mLength = myComObj.Read(mBuffer, 0, 14);

                //Close the port 
                myComObj.Close();

                //Data lenght > 8 ? 
                if (mLength >= 14)
                {
                    //Get data from one-dimentional array 
                    for (int i = 0; i <= mLength - 1; i++)
                    {
                        mData = mData + mBuffer[i];
                    }

                    //Get Weight                 
                    mData = mData.Substring(0, 9);                    
                }
                //int wgt = 0;
                bool res = false;
                res = int.TryParse(mData, out mWeight);                                
                return mWeight;

            }

            catch (Exception ex)
            {
                myComObj.Close();
                //throw new Exception("Erreur lors de la capture du poids : " + ex.Message + (Char)13 + (Char)10 + "Reessayer SVP");
                return -1;
            }
        }

        private int ReadUsingEsclave_HKF()
        {
            try
            {
                char[] mBuffer = new char[10];
                byte[] bytesToSend = { 0x01, 0x0D, 0x0A }; // SOH CR LF
                string mData = string.Empty;
                int mWeight = -10;

                //Open port 
                myComObj.Open();

                //Send request to indicator 
                myComObj.Write(bytesToSend, 0, bytesToSend.Length);

                //wait 100 ms
                System.Threading.Thread.Sleep(500);

                //Reads eight caracters from com  
                int mLength = myComObj.Read(mBuffer, 0, 49);

                //Close port 
                myComObj.Close();

                //Data lenght > 49 ? 
                if (mLength >= 49)
                {
                    //Get data from one-dimentional array 
                    for (int i = 0; i <= mLength - 1; i++)
                    {
                        mData = mData + mBuffer[i];
                    }
                    mData = mData.Substring(11, 6);
                    //Weight is stable ? 
                    //if (mData.Substring(1, 1) == "I")
                    //{
                    //    mData = mData.Substring(2, 5);

                    //}
                    //else
                    //    throw new Exception("Poids Instable");
                }


                mWeight = int.Parse(mData);

                //if (mWeight < 0)
                //    throw new Exception("Le poids capturé doit être supérieur à 0");

                return mWeight;

            }
            catch (Exception ex)
            {
                myComObj.Close();
                throw new Exception("Erreur lors de la capture du poids : " + ex.Message + (Char)13 + (Char)10 + "Reessayer SVP");
                return -20;
            }
        }

        private int ReadUsingMaitre_HKF()
        {
            string mData = string.Empty;
            string mData2 = string.Empty;
            char mChar;
            string sWeight;
            int mWeight;
            int mComparedWeight;

            try
            {
                //Open port 
                myComObj.Open();

                //Start Reading
                mChar = (char)myComObj.ReadChar();
                mData = mData + mChar.ToString();

                //Read at least 150 characters
                while (mData.Length < 200)
                {
                    mChar = (char)myComObj.ReadChar();
                    mData = mData + mChar.ToString();
                }

                //Close port
                myComObj.Close();

                //display data
                //MessageBox.Show(mData);

                //Get Index of carriage return
                char mCret = (char)01;
                int mIndex = mData.IndexOf(mCret);

                //Get data after Cr
                mData = mData.Substring(mIndex + 1);

                //Get Weight
                sWeight = mData.Substring(3, 6).Replace(" ", "");

                //Get first weight
                mWeight = int.Parse(sWeight);

                //Wait 500 ms
                System.Threading.Thread.Sleep(300);

                //Get second time index of Cr
                mIndex = mData.IndexOf(mCret);

                //Get data after Cr
                mData = mData.Substring(mIndex + 1);

                //Get Weight
                sWeight = mData.Substring(3, 6).Replace(" ", "");

                //Get first weight
                mComparedWeight = int.Parse(sWeight);

                if (mWeight != mComparedWeight)
                {
                    throw new Exception("Poids Instable");
                }

                if (mWeight < 0)
                    throw new Exception("Le poids capturé doit être supérieur à 0");

                return mWeight;

            }
            catch (Exception ex)
            {
                myComObj.Close();
                throw new Exception("Erreur lors de la capture du poids : " + ex.Message + (Char)13 + (Char)10 + "Reessayer SVP");
                return -30;
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

                    mCommande = db().CreateStoredProcCommand("PontBascule_New");

                    db().AddOutParameter(mCommande, "@ID", SqlDbType.Int, 0);
                    db().AddInParameter(mCommande, "@CreationUser", SqlDbType.VarChar, _UtilisateurCreation);
                }
                else
                {
                    mCommande = db().CreateStoredProcCommand("PontBascule_Modify");
                    db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
                    db().AddInParameter(mCommande, "@ModificationUser", SqlDbType.VarChar, _UtilisateurModification);
                }

                db().AddInParameter(mCommande, "@SiteID", SqlDbType.Int, _Site.ID);
                db().AddInParameter(mCommande, "@Designation", SqlDbType.VarChar, _Designation);
                db().AddInParameter(mCommande, "@NomOrdinateur", SqlDbType.VarChar, _Ordinateur);
                db().AddInParameter(mCommande, "@ProtocoleID", SqlDbType.Int, _Protocole.ID);
                db().AddInParameter(mCommande, "@ComVitesse", SqlDbType.Int, _ComVitesse);
                db().AddInParameter(mCommande, "@ComParite", SqlDbType.Int, _ComParite);
                db().AddInParameter(mCommande, "@ComBitsDonnees", SqlDbType.Int, _ComBitsDonnees);
                db().AddInParameter(mCommande, "@ComBitsStop", SqlDbType.Int, _ComBitsStop);
                db().AddInParameter(mCommande, "@ComPort", SqlDbType.VarChar, _ComPort);
                db().AddInParameter(mCommande, "@ComTimeOut", SqlDbType.Int, _ComTimeOut);

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
                        _ID = (int)db().Parameters(mCommande, "@ID");

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
                throw new Exception(ex.Message + "\r\n" + "Certification:fnUpdate");

            }
            return Result;
        }


        public override bool fnActivate()
        {

            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("PontBascule_Activate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
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
                throw new Exception(ex.Message + "\r\n" + "PontBascule:fnActivate");
            }
            return Result;

        }


        public override bool fnDeActivate()
        {
            bool Result;
            DataCommand mCommande = db().CreateStoredProcCommand("PontBascule_DeActivate");
            db().AddInParameter(mCommande, "@ID", SqlDbType.Int, _ID);
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
                throw new Exception(ex.Message + "\r\n" + "PontBascule:fnDeActivate");
            }
            return Result;
        }


        public override bool fnGet(object Id)
        {
            IDataReader mDataReader = null;
            try
            {
                mDataReader = db().ExecuteReader("PontBascule_Get", (int)Id);
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
        public override string ToString()
        {
            throw new NotImplementedException();
        }

        #endregion

        private static void MapFromDataReader(Bascule mClass, IDataReader mDataReader)
        {
            try
            {
                if (mDataReader != null)
                {
                    mClass.IsNew = false;

                    if (!DBNull.Value.Equals(mDataReader["ID"])) mClass._ID = (int)mDataReader["ID"];

                    mClass._Site = new Site();
                    mClass._Protocole = new Protocole();

                    if (!DBNull.Value.Equals(mDataReader["SiteID"])) mClass._Site.ID = (int)mDataReader["SiteID"];
                    if (!DBNull.Value.Equals(mDataReader["SiteName"])) mClass._Site.Nom = (string)mDataReader["SiteName"];

                    if (!DBNull.Value.Equals(mDataReader["Name"])) mClass._Designation = (string)mDataReader["Name"];
                    if (!DBNull.Value.Equals(mDataReader["Computer"])) mClass._Ordinateur = (string)mDataReader["Computer"];

                    if (!DBNull.Value.Equals(mDataReader["ProtocolID"])) mClass._Protocole.ID = (int)mDataReader["ProtocolID"];
                    if (!DBNull.Value.Equals(mDataReader["ProtocolName"])) mClass._Protocole.Designation = (string)mDataReader["ProtocolName"];

                    if (!DBNull.Value.Equals(mDataReader["PontBasculeSpeed"])) mClass._ComVitesse = (int)mDataReader["PontBasculeSpeed"];
                    if (!DBNull.Value.Equals(mDataReader["PontBasculeParity"])) mClass._ComParite = (short)mDataReader["PontBasculeParity"];
                    if (!DBNull.Value.Equals(mDataReader["PontBasculeDataBits"])) mClass._ComBitsDonnees = (short)mDataReader["PontBasculeDataBits"];
                    if (!DBNull.Value.Equals(mDataReader["PontBasculeStopBits"])) mClass._ComBitsStop = (short)mDataReader["PontBasculeStopBits"];
                    if (!DBNull.Value.Equals(mDataReader["PontBasculeComPort"])) mClass._ComPort = (string)mDataReader["PontBasculeComPort"];
                    if (!DBNull.Value.Equals(mDataReader["PontBasculeTimeOut"])) mClass._ComTimeOut = (int)mDataReader["PontBasculeTimeOut"];
                    if (!DBNull.Value.Equals(mDataReader["PontBasculeDesactive"])) mClass._Desative = (bool)mDataReader["PontBasculeDesactive"];

                    if (!DBNull.Value.Equals(mDataReader["CreationUtilisateur"])) mClass.UtilisateurCreation = (string)mDataReader["CreationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["CreationDate"])) mClass.DateCreation = (DateTime)mDataReader["CreationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationDate"])) mClass.DateModification = (DateTime)mDataReader["ModificationDate"];
                    if (!DBNull.Value.Equals(mDataReader["ModificationUtilisateur"])) mClass.UtilisateurModification = (string)mDataReader["ModificationUtilisateur"];
                    if (!DBNull.Value.Equals(mDataReader["RowVersionKey"])) mClass.RowVersionKey = (object)mDataReader["RowVersionKey"];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\n Bascule : MapFromDataReader");
            }
        }


    }

    public partial class BasculeViewModel
    {
        public Bascule _Bascule { get; set; }
        public Parametres _Parametres { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
