using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;


namespace Tms.Components.Data
{
    public class DataCommand : IDisposable
    {


        private SqlCommand _Command;
        public DataCommand()
        {
            //Instancie un objet SqlCommand
            _Command = new SqlCommand();
        }

        public int CommandTimeout
        {
            get { return _Command.CommandTimeout; }
            set { _Command.CommandTimeout = value; }
        }
        internal System.Data.CommandType CommandType
        {
            set { _Command.CommandType = value; }
        }

        internal string CommandText
        {
            set { _Command.CommandText = value; }
        }

        internal SqlCommand Command
        {
            //Retourne l'objet SqlCommand
            get { return _Command; }
        }

        internal void AddInParameter(string ParameterName, SqlDbType DbType, int Size, object Value)
        {
            //Ajoute un paramètre d'entrée à l'objet SqlCommand
            SqlParameter mParameter = _Command.Parameters.Add(ParameterName, DbType, Size);
            mParameter.Value = Value;
            mParameter.Direction = ParameterDirection.Input;
        }

        internal void AddInParameter(string ParameterName, SqlDbType DbType, object Value)
        {
            //Ajoute un paramètre d'entrée à l'objet SqlCommand
            SqlParameter mParameter = _Command.Parameters.Add(ParameterName, DbType);
            mParameter.Value = Value;
            mParameter.Direction = ParameterDirection.Input;
        }

        internal void AddParameter(string ParameterName, SqlDbType DbType, int Size, object Value, ParameterDirection Direction)
        {
            //Ajoute un paramètre d'entrée/sortie à l'objet SqlCommand
            SqlParameter mParameter = _Command.Parameters.Add(ParameterName, DbType, Size);
            mParameter.Value = Value;
            mParameter.Direction = Direction;
        }

        internal void AddOutParameter(string ParameterName, SqlDbType DbType, int Size)
        {
            //Ajoute un paramètre de sortie à l'objet SqlCommand
            SqlParameter mParameter = _Command.Parameters.Add(ParameterName, DbType, Size);
            mParameter.Direction = ParameterDirection.Output;
        }

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
                _Command.Dispose();
                _Command = null;
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
