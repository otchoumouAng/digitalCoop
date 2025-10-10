using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data.OleDb;


namespace Tms.Components.Data
{
   
        public class DataSource : IDisposable
        {

            private string _ConnectionString;
            private string _OleDbConnectionString;

            private SqlConnection _PersistConnection;
            public DataSource()
            {
                //Construction de la chaine de connection à partir de app.config
                //Appel de l'objet DataConfig

                DataConfig mConfig = new DataConfig();
                string mServer = mConfig.Server.Trim();
                string mDataBase = mConfig.DataBase.Trim();
                string mUserName = mConfig.UserName.Trim();
                string mPassword = mConfig.Password.Trim();

                if ((mServer == string.Empty) | (mDataBase == string.Empty))
                {
                    throw new Exception("Erreur à l'initialisation de DataSource : Valeur incorrecte pour le nom du serveur ou de la base de données");
                }
                mConfig = null;
                //_ConnectionString = "Data Source=" + mServer + "; Initial Catalog=" + mDataBase + "; Integrated Security=SSPI;";
                //Server = myServerName\myInstanceName; Database = myDataBase; User Id = myUsername; Password = myPassword;
                _ConnectionString = "Server=" + mServer + "; Database=" + mDataBase + "; User Id="+ mUserName+"; Password="+mPassword;

                _OleDbConnectionString = "Provider=SQLOLEDB; Data Source=" + mServer + "; Initial Catalog=" + mDataBase + "; User Id=" + mUserName + "; Password=" + mPassword; 
            }

            public DataSource(string mServer, string mDataBase, string mUserName, string mPassword)
            {
                //Construction de la chaine de connection à partir des paramètres reçus

                if ((mServer == string.Empty) | (mDataBase == string.Empty))
                {
                    throw new Exception("Erreur à l'initialisation de DataSource : Valeur incorrecte pour le nom du serveur ou de la base de données");
                }
                _ConnectionString = "Server=" + mServer + "; Database=" + mDataBase + "; User Id=" + mUserName + "; Password=" + mPassword;
                 _OleDbConnectionString = "Provider=SQLOLEDB; Data Source=" + mServer + "; Initial Catalog=" + mDataBase + "; User Id=" + mUserName + "; Password=" + mPassword;

            // _ConnectionString = "Data Source=" + mServer + "; Initial Catalog=" + mDataBase + "; Integrated Security=SSPI;";
            //_OleDbConnectionString = "Provider=SQLOLEDB; Data Source=" + mServer + "; Initial Catalog=" + mDataBase + "; Integrated Security=SSPI;";
        }

        public System.String ConnectionString
            {
                //Retourne la chaine de connection
                get { return _ConnectionString; }
            }

            public DataCommand CreateStoredProcCommand(string storedProcName)
            {
                //Retourne un objet DataCommande 
                //Utilisé pour exécuter une procédure stockée
                DataCommand mDataCommand = new DataCommand();
                mDataCommand.CommandType = CommandType.StoredProcedure;
                mDataCommand.CommandText = storedProcName;
                //mDataCommand.CommandTimeout = 0
                return mDataCommand;
            }

            public void AddInParameter(DataCommand mDataCommand, string ParameterName, SqlDbType DbType, int Size, object Value)
            {
                //Ajoute un paramètre d'entrée à l'objet DataCommand
                mDataCommand.AddInParameter(ParameterName, DbType, Size, Value);
            }

            public void AddInParameter(DataCommand mDataCommand, string ParameterName, SqlDbType DbType, object Value)
            {
                //Ajoute un paramètre d'entrée à l'objet DataCommand
                mDataCommand.AddInParameter(ParameterName, DbType, Value);
            }

            public void AddParameter(DataCommand mDataCommand, string ParameterName, SqlDbType DbType, int Size, object Value, ParameterDirection Direction)
            {
                //Ajoute un paramètre d'entrée/sortie à l'objet DataCommand
                mDataCommand.AddParameter(ParameterName, DbType, Size, Value, Direction);
            }

            public void AddOutParameter(DataCommand mDataCommand, string ParameterName, SqlDbType DbType, int Size)
            {
                //Ajoute un paramètre de sortie à l'objet DataCommand
                mDataCommand.AddOutParameter(ParameterName, DbType, Size);
            }

            public SqlDataReader ExecuteReader(string StoredProcName)
            {
                System.Diagnostics.Debug.Write("SP:" + StoredProcName + " " + DateTime.Now.ToString() + Environment.NewLine);
                //Exécute une procédure stockée dont le nom est passé en paramètre
                //Retourne un objet SqlDataReader 

                SqlConnection mConnection = OpenDataSource();

                if (mConnection == null)
                    throw new Exception("Erreur à l'ouverture de la connection à la base de données");

                SqlCommand mCommand = new SqlCommand(StoredProcName, mConnection);
                mCommand.CommandType = CommandType.StoredProcedure;

                try
                {
                    SqlDataReader mDataReader = mCommand.ExecuteReader(CommandBehavior.CloseConnection);

                    return mDataReader;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + Environment.NewLine+ "DataSource:ExecuteReader");
                    mConnection.Close();
                    return null;
                }
            }

            public OleDbDataReader ExecuteReader(string StoredProcName, params object[] ParameterValues)
            {
                System.Diagnostics.Debug.Write("SP:" + StoredProcName + " " + DateTime.Now.ToString() +Environment.NewLine);
                //Exécute une procédure stockée dont le nom 
                //+ les valeurs des paramètres sont passés en paramètre
                //Retourne un objet OledbDataReader

                OleDbConnection mConnection = OpenOleDbDataSource();

                if (mConnection == null)
                    throw new Exception("Erreur à l'ouverture de la connection à la base de données");

                OleDbCommand mCommand = new OleDbCommand(StoredProcName, mConnection);
                mCommand.CommandType = CommandType.StoredProcedure;
                //mCommand.CommandTimeout = 0

                if (ParameterValues.Length > 0)
                {

                    for (int i = 0; i <= Information.UBound(ParameterValues, 1); i++)
                    {
                        OleDbParameter mParameter = new OleDbParameter();
                        mParameter.Direction = ParameterDirection.Input;
                        mParameter.Value = ParameterValues[i];

                        mCommand.Parameters.Add(mParameter);
                    }
                }

                try
                {
                    OleDbDataReader mDataReader = mCommand.ExecuteReader(CommandBehavior.CloseConnection);

                    return mDataReader;
                }
                catch (Exception ex)
                {
                    mConnection.Close();
                    throw new Exception(ex.Message + Environment.NewLine+ "DataSource:ExecuteReader");
                    return null;
                }
            }

            public OleDbDataReader ExecuteReader(string StoredProcName, ref IDbConnection GeneratedConnection, params object[] ParameterValues)
            {
                System.Diagnostics.Debug.Write("SP:" + StoredProcName + " " + DateTime.Now.ToString() +Environment.NewLine);
                //Exécute une procédure stockée dont le nom 
                //+ les valeurs des paramètres sont passés en paramètre
                //Retourne un objet OledbDataReader

                OleDbConnection mConnection = OpenOleDbDataSource();

                GeneratedConnection = mConnection;

                if (mConnection == null)
                    throw new Exception("Erreur à l'ouverture de la connection à la base de données");

                OleDbCommand mCommand = new OleDbCommand(StoredProcName, mConnection);
                mCommand.CommandType = CommandType.StoredProcedure;

                if (ParameterValues.Length > 0)
                {

                    for (int i = 0; i <= Information.UBound(ParameterValues, 1); i++)
                    {
                        OleDbParameter mParameter = new OleDbParameter();
                        mParameter.Direction = ParameterDirection.Input;
                        mParameter.Value = ParameterValues[i];

                        mCommand.Parameters.Add(mParameter);
                    }
                }

                try
                {
                    OleDbDataReader mDataReader = mCommand.ExecuteReader(CommandBehavior.CloseConnection);

                    return mDataReader;

                }
                catch (Exception ex)
                {
                    mConnection.Close();
                    throw new Exception(ex.Message + Environment.NewLine+ "DataSource:ExecuteReader");
                    return null;
                }
            }

            public SqlDataReader ExecuteReader(DataCommand mDataCommand)
            {
                System.Diagnostics.Debug.Write("SP:" + mDataCommand.Command.CommandText + " " + DateTime.Now.ToString() +Environment.NewLine);
                //Exécute une procédure stockée à partir d'un objet DataCommand
                //Retourne un objet SqlDataReader

                SqlConnection mConnection = OpenDataSource();

                if (mConnection == null)
                    throw new Exception("Erreur à l'ouverture de la connection à la base de données");

                if (mDataCommand == null)
                {
                    throw new Exception("Objet DataCommand non valide");
                }

                SqlCommand mCommand = mDataCommand.Command;
                mCommand.Connection = mConnection;
                //mCommand.CommandTimeout = 0

                try
                {
                    SqlDataReader mDataReader = mCommand.ExecuteReader(CommandBehavior.CloseConnection);

                    return mDataReader;

                }
                catch (Exception ex)
                {
                    mConnection.Close();
                    throw new Exception(ex.Message + Environment.NewLine+ "DataSource:ExecuteReader");
                    return null;
                }
            }


            public OleDbDataAdapter ExecuteAdapter(string StoredProcName, params object[] ParameterValues)
            {
                System.Diagnostics.Debug.Write("SP:" + StoredProcName + " " + DateTime.Now.ToString() +Environment.NewLine);
                //Exécute une procédure stockée à partir d'un objet DataCommand
                //Retourne un objet OleDbDataAdapter

                OleDbConnection mConnection = OpenOleDbDataSource();

                if (mConnection == null)
                    throw new Exception("Erreur à l'ouverture de la connection à la base de données");

                try
                {
                    OleDbDataAdapter myOleDbDataAdapter = new OleDbDataAdapter(StoredProcName, mConnection);
                    myOleDbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    if (ParameterValues.Length > 0)
                    {

                        for (int i = 0; i <= Information.UBound(ParameterValues, 1); i++)
                        {
                            OleDbParameter mParameter = new OleDbParameter();
                            mParameter.Direction = ParameterDirection.Input;
                            //mParameter.ParameterName = "@ID"
                            mParameter.Value = ParameterValues[i];
                            myOleDbDataAdapter.SelectCommand.Parameters.Add(mParameter);
                        }
                    }

                    return myOleDbDataAdapter;

                }
                catch (Exception ex)
                {
                    mConnection.Close();
                    throw new Exception(ex.Message + Environment.NewLine+ "DataSource:ExecuteReader");
                    return null;
                }
            }

            public int ExecuteNonQuery(string StoredProcName)
            {
                System.Diagnostics.Debug.Write("SP:" + StoredProcName + " " + DateTime.Now.ToString() +Environment.NewLine);
                //Exécute une procédure stockée à partir du nom du PS en paramètre
                //Ramène le nombre de ligne affecté par l'exécution de la PS

                SqlConnection mConnection = OpenDataSource();

                if (mConnection == null)
                    throw new Exception("Erreur à l'ouverture de la connection à la base de données");

                SqlCommand mCommand = new SqlCommand(StoredProcName, mConnection);
                mCommand.CommandType = CommandType.StoredProcedure;

                try
                {
                    int mRowsAffected = mCommand.ExecuteNonQuery();

                    mCommand.Dispose();

                    mConnection.Close();
                    mConnection.Dispose();
                    mConnection = null;

                    return mRowsAffected;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + Environment.NewLine+ "DataSource:ExecuteNonQuery");
                    return 0;
                }
            }

            public int ExecuteNonQuery(string StoredProcName, params object[] ParameterValues)
            {
                System.Diagnostics.Debug.Write("SP:" + StoredProcName + " " + DateTime.Now.ToString() +Environment.NewLine);
                //Exécute une procédure stockée à partir du nom du PS 
                //+ une liste des paramètres passée en paramètre
                //Ramène le nombre de ligne affecté par l'exécution de la PS

                OleDbConnection mConnection = OpenOleDbDataSource();

                if (mConnection == null)
                    throw new Exception("Erreur à l'ouverture de la connection à la base de données");

                OleDbCommand mCommand = new OleDbCommand(StoredProcName, mConnection);
                mCommand.CommandType = CommandType.StoredProcedure;

                if (ParameterValues.Length > 0)
                {

                    for (int i = 0; i <= Information.UBound(ParameterValues, 1); i++)
                    {
                        OleDbParameter mParameter = new OleDbParameter();
                        mParameter.Direction = ParameterDirection.Input;
                        mParameter.Value = ParameterValues[i];

                        mCommand.Parameters.Add(mParameter);
                    }
                }

                try
                {
                    int mRowsAffected = mCommand.ExecuteNonQuery();

                    mCommand.Dispose();

                    mConnection.Close();
                    mConnection.Dispose();
                    mConnection = null;

                    return mRowsAffected;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + Environment.NewLine+ "DataSource:ExecuteNonQuery");
                    return 0;
                }
            }

            public int ExecuteNonQuery(ref DataCommand mDataCommand)
            {
                System.Diagnostics.Debug.Write("SP:" + mDataCommand.Command.CommandText + " " + DateTime.Now.ToString() +Environment.NewLine);
                //Exécute une procédure stockée à partir d'un objet DataCommand passé en paramètre
                //Ramène le nombre de ligne affecté par l'exécution de la PS

                SqlConnection mConnection = OpenDataSource();

                if (mConnection == null)
                    throw new Exception("Erreur à l'ouverture de la connection à la base de données");

                if (mDataCommand == null)
                {
                    throw new Exception("Objet DataCommand non valide");
                }

                SqlCommand mCommand = mDataCommand.Command;
                mCommand.Connection = mConnection;

                try
                {
                    int mRowsAffected = mCommand.ExecuteNonQuery();

                    mCommand.Dispose();

                    mConnection.Close();
                    mConnection.Dispose();
                    mConnection = null;

                    return mRowsAffected;
                }
                catch (Exception ex)
                {
                    mCommand.Dispose();

                    mConnection.Close();
                    throw new Exception(ex.Message + Environment.NewLine+ "DataSource:ExecuteNonQuery");
                    //Return 0
                }
            }

            public int ExecuteNonQuery(ref DataCommand mDataCommand, DataTransaction mDataTransaction)
            {
                System.Diagnostics.Debug.Write("SP:" + mDataCommand.Command.CommandText + DateTime.Now.ToString() +Environment.NewLine);
                //Exécute une procédure stockée à partir d'un objet DataDataCommand passé en paramètre
                //Et l'objet DataTransaction qui crée une transaction explicite
                //Ramène le nombre de ligne affecté par l'exécution de la PS

                if (mDataCommand == null)
                {
                    throw new Exception("Objet DataCommand non valide");
                }

                SqlCommand mCommand = mDataCommand.Command;
                mCommand.Connection = _PersistConnection;
                mCommand.Transaction = mDataTransaction.Transaction;

                try
                {
                    int mRowsAffected = mCommand.ExecuteNonQuery();

                    return mRowsAffected;

                }
                catch (Exception ex)
                {
                    mDataTransaction.RollBack();
                    _PersistConnection.Close();
                    _PersistConnection.Dispose();
                    throw new Exception(ex.Message + Environment.NewLine+ "DataSource:ExecuteNonQuery");
                    return 0;
                }
            }

            public object ExecuteScalar(string StoredProcName)
            {
                System.Diagnostics.Debug.Write("SP:" + StoredProcName + " " + DateTime.Now.ToString() +Environment.NewLine);
                //Exécute une procédure stockée à partir du nom du PS en paramètre
                //Ramène une valeur scalaire

                SqlConnection mConnection = OpenDataSource();

                if (mConnection == null)
                    throw new Exception("Erreur à l'ouverture de la connection à la base de données");

                SqlCommand mCommand = new SqlCommand(StoredProcName, mConnection);
                mCommand.CommandType = CommandType.StoredProcedure;

                try
                {
                    object mReturnValue = mCommand.ExecuteScalar();

                    mConnection.Close();
                    mConnection.Dispose();
                    mConnection = null;

                    return mReturnValue;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + Environment.NewLine+ "DataSource:ExecuteScalar");
                    return null;
                }
            }

            public object ExecuteScalar(string StoredProcName, params object[] ParameterValues)
            {
                System.Diagnostics.Debug.Write("SP:" + StoredProcName + " " + DateTime.Now.ToString() +Environment.NewLine);
                //Exécute une procédure stockée à partir du nom du PS en paramètre
                //+ une liste des paramètres passée en paramètre
                //Ramène une valeur scalaire

                OleDbConnection mConnection = OpenOleDbDataSource();

                if (mConnection == null)
                    throw new Exception("Erreur à l'ouverture de la connection à la base de données");

                OleDbCommand mCommand = new OleDbCommand(StoredProcName, mConnection);
                mCommand.CommandType = CommandType.StoredProcedure;

                if (ParameterValues.Length > 0)
                {

                    for (int i = 0; i <= Information.UBound(ParameterValues, 1); i++)
                    {
                        OleDbParameter mParameter = new OleDbParameter();
                        mParameter.Direction = ParameterDirection.Input;
                        mParameter.Value = ParameterValues[i];

                        mCommand.Parameters.Add(mParameter);
                    }
                }

                try
                {
                    object mReturnValue = mCommand.ExecuteScalar();

                    mConnection.Close();
                    mConnection.Dispose();
                    mConnection = null;

                    return mReturnValue;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + Environment.NewLine+ "DataSource:ExecuteScalar");
                    return null;
                }
            }

            public object ExecuteScalar(DataCommand mDataCommand)
            {
                System.Diagnostics.Debug.Write("SP:" + mDataCommand.Command.CommandText + " " + DateTime.Now.ToString() + Environment.NewLine+Environment.NewLine);
                //Exécute une procédure stockée à partir d'un objet DataCommand
                //Ramène une valeur scalaire

                SqlConnection mConnection = OpenDataSource();

                if (mConnection == null)
                {
                    throw new Exception("Erreur à l'ouverture de la connection à la base de données");
                }

                if (mDataCommand == null)
                {
                    throw new Exception("Objet DataCommand non valide");
                }

                SqlCommand mCommand = mDataCommand.Command;
                mCommand.Connection = mConnection;

                try
                {
                    object mReturnValue = mCommand.ExecuteScalar();

                    mConnection.Close();
                    mConnection.Dispose();
                    mConnection = null;
                    return mReturnValue;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + Environment.NewLine+ "DataSource:ExecuteScalar");
                    return null;
                }
            }

            public object Parameters(DataCommand mCommand, string ParameterName)
            {
                //Retourne la valeur d'un paramètre
                //Dans la collection Parameters de l'objet DataCommand

                if (mCommand == null)
                {
                    throw new Exception("Objet DataCommand non valide");
                }

                return mCommand.Command.Parameters[ParameterName].Value;
            }

            public DataTransaction BeginTransaction(IsolationLevel mIsolationLevel = IsolationLevel.ReadCommitted)
            {
                //Ouvre une connection persistante et
                //initialise une transaction et
                //Retourne un objet DataTransaction

                _PersistConnection = OpenDataSource();

                if (_PersistConnection == null)
                    throw new Exception("Erreur à l'ouverture de la connection à la base de données");

                DataTransaction mDataTransaction = new DataTransaction();
                mDataTransaction.Transaction = _PersistConnection.BeginTransaction(mIsolationLevel);
                return mDataTransaction;
            }

            public void CommitTransaction(DataTransaction mDataTransaction)
            {
                //Valide la transaction ouverte sur la 
                //connection _PersistConnection et ferme l'objet

                mDataTransaction.Commit();
                //mDataTransaction.Dispose()
                _PersistConnection.Close();
                _PersistConnection.Dispose();
                _PersistConnection = null;
            }

            public void RollBackTransaction(DataTransaction mDataTransaction)
            {
                try
                {
                    mDataTransaction.RollBack();
                    //mDataTransaction.Dispose()
                    _PersistConnection.Close();
                    _PersistConnection.Dispose();
                    _PersistConnection = null;
                }
                catch (Exception ex)
                {
                    _PersistConnection = null;
                }
            }

            #region "Fonctions privées partagées"

            public SqlConnection OpenDataSource()
            {
                //Ouverture de la connection avec SQL Client natif
                //Retourne un objet SQLConnection

                SqlConnection myConnection = new SqlConnection();

                try
                {
                    myConnection.ConnectionString = _ConnectionString;

                    myConnection.Open();
                    if (myConnection.State == ConnectionState.Open)
                    {
                        return myConnection;
                    }
                    else {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                    return null;
                }
            }

            public OleDbConnection OpenOleDbDataSource()
            {
                //Ouverture de la connection avec oledb
                //Retourne un objet OleDbConnection

                OleDbConnection myConnection = new OleDbConnection();

                try
                {
                    myConnection.ConnectionString = _OleDbConnectionString;
                    myConnection.Open();
                    if (myConnection.State == ConnectionState.Open)
                    {
                        return myConnection;
                    }
                    else {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                    return null;
                }
            }

            #endregion


            // To detect redundant calls
            private bool disposedValue = false;

            // IDisposable
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: free unmanaged resources when explicitly called
                    }

                    // TODO: free shared unmanaged resources
                    _PersistConnection = null;
                    //_Connection.Close()
                    //_Connection.Dispose()
                }
                this.disposedValue = true;
            }

            #region " IDisposable Support "
            // This code added by Visual Basic to correctly implement the disposable pattern.
            public void Dispose()
            {
                // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            #endregion
        
    }
}
