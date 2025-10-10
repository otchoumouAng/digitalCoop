using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Tms.Components;
using System.Data.SqlClient;
using Tms.Components.Data;

namespace Tms.Classes.Security
{
    public class Security
    {

        //Check if current user has permissions to access function 
        public bool HasFunctionAccess(string FunctionID)
        {
            bool bolReturn = false;
            DataSource _db = new DataSource();
            DataCommand _cmd = _db.CreateStoredProcCommand("se_SecurityGetFunctionAccessStatus");
            DataConfig mConfig = new DataConfig();

            try
            {
                if (!string.IsNullOrEmpty(FunctionID))
                {
                    _db.AddInParameter(_cmd, "@FunctionID", SqlDbType.UniqueIdentifier, new Guid(FunctionID));
                    _db.AddInParameter(_cmd, "@UserName", SqlDbType.VarChar, 50, Environment.UserDomainName + '\\' + Environment.UserName);
                    _db.AddInParameter(_cmd, "@BaseDeDonnees", SqlDbType.VarChar, 50, mConfig.DataBase);
                    _db.AddInParameter(_cmd, "@Serveur", SqlDbType.VarChar, 50, mConfig.Server);
                    bolReturn = (((int)_db.ExecuteScalar(_cmd)) == 1);
                }
            }
            catch (Exception ex)
            {
                //Throw New Exception(ex.Message & vbCrLf & "Security:HasFunctionAccess")
                throw new Exception(ex.Message);
            }
            return bolReturn;
        }

        /// <summary>
        /// Vérifie l'utilisateur qui se connecte avec son nom utilisateur et mot de passe possède les permissions necessaires
        /// </summary>
        /// <param name="FunctionID">ID de la fonction</param>
        /// <param name="UserName">Nom de l'utilisateur</param>
        /// <returns>Vrai s'il a les permissions</returns>
        /// <remarks></remarks>
        public bool HasFunctionAccess(string FunctionID, string UserName)
        {
            bool bolReturn = false;
            DataSource _db = new DataSource();
            DataCommand _cmd = _db.CreateStoredProcCommand("se_SecurityGetFunctionAccessStatus");
            DataConfig mConfig = new DataConfig();
            try
            {
                if (!string.IsNullOrEmpty(FunctionID))
                {
                    if ((string.IsNullOrEmpty(UserName)))
                    {
                        UserName = Environment.UserDomainName + '\\' + Environment.UserName;
                    }
                    _db.AddInParameter(_cmd, "@FunctionID", SqlDbType.UniqueIdentifier, new Guid(FunctionID));
                    _db.AddInParameter(_cmd, "@UserName", SqlDbType.VarChar, 50, UserName);
                    _db.AddInParameter(_cmd, "@BaseDeDonnees", SqlDbType.VarChar, 50, mConfig.DataBase);
                    _db.AddInParameter(_cmd, "@Serveur", SqlDbType.VarChar, 50, mConfig.Server);
                    bolReturn = (((int)_db.ExecuteScalar(_cmd)) == 1);
                }
            }
            catch (Exception ex)
            {
                //Throw New Exception(ex.Message & vbCrLf & "Security:HasFunctionAccess")
                throw new Exception(ex.Message);
            }
            return bolReturn;
        }


        public bool HasOfflineAccess(string HorsReseauUserName, string Password)
        {
            bool bolReturn = false;
            DataSource _db = new DataSource();
            DataCommand _cmd = _db.CreateStoredProcCommand("se_UtilisateurHorsReseauVerify");
            DataConfig mConfig = new DataConfig();

            try
            {
                _db.AddInParameter(_cmd, "@UserName", SqlDbType.VarChar, 50, HorsReseauUserName);
                _db.AddInParameter(_cmd, "@Password", SqlDbType.VarChar, 400, Password);
                bolReturn = ((int)(_db.ExecuteScalar(_cmd)) == 1);
            }
            catch (Exception ex)
            {
                //Throw New Exception(ex.Message & vbCrLf & "Security:HasFunctionAccess")
                throw new Exception(ex.Message);
            }
            return bolReturn;
        }

        public bool HasFunctionAccess(string FunctionID, Guid[] _TabOfRights)
        {
            if (_TabOfRights == null || _TabOfRights.Length == 0)
            {
                return HasFunctionAccess(FunctionID);
            }
            return !string.IsNullOrEmpty(FunctionID) && Array.IndexOf(_TabOfRights, new Guid(FunctionID)) >= 0;
        }



        //Check if user has access to database using Windows authentication
        public bool HasApplicationAccess(string ApplicationID)
        {
            bool bolReturn = false;
            DataSource _db = new DataSource();
            DataCommand _cmd = _db.CreateStoredProcCommand("se_SecurityGetApplicationAccessStatus");
            DataConfig mConfig = new DataConfig();

            _db.AddInParameter(_cmd, "@ApplicationID", SqlDbType.UniqueIdentifier, new Guid(ApplicationID));
            _db.AddInParameter(_cmd, "@UserName", SqlDbType.VarChar, 50, Environment.UserDomainName + '\\' + Environment.UserName);
            _db.AddInParameter(_cmd, "@BaseDeDonnees", SqlDbType.VarChar, 50, mConfig.DataBase);
            _db.AddInParameter(_cmd, "@Serveur", SqlDbType.VarChar, 50, mConfig.Server);

            try
            {
                bolReturn = (((int)_db.ExecuteScalar(_cmd)) == 1);
            }
            catch (Exception ex)
            {
                //Throw New Exception(ex.Message & vbCrLf & "Security:HasApplicationAccess")
                throw new Exception(ex.Message);
            }
            return bolReturn;
        }

        //Check if user has access to database using Windows authentication
        public bool HasApplicationAccess(string ApplicationID, string HorsReseauUserName)
        {
            bool bolReturn = false;
            DataSource _db = new DataSource();
            DataCommand _cmd = _db.CreateStoredProcCommand("se_SecurityGetApplicationAccessStatus");
            DataConfig mConfig = new DataConfig();

            _db.AddInParameter(_cmd, "@ApplicationID", SqlDbType.UniqueIdentifier, new Guid(ApplicationID));
            _db.AddInParameter(_cmd, "@UserName", SqlDbType.VarChar, 50, HorsReseauUserName);
            _db.AddInParameter(_cmd, "@BaseDeDonnees", SqlDbType.VarChar, 50, mConfig.DataBase);
            _db.AddInParameter(_cmd, "@Serveur", SqlDbType.VarChar, 50, mConfig.Server);

            try
            {
                bolReturn = (((int)_db.ExecuteScalar(_cmd)) == 1);
            }
            catch (Exception ex)
            {
                //Throw New Exception(ex.Message & vbCrLf & "Security:HasApplicationAccess")
                throw new Exception(ex.Message);
            }
            return bolReturn;
        }
      }
    }
