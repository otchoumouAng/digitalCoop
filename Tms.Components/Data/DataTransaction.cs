using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;



namespace Tms.Components.Data
{
    public class DataTransaction
    {


        private SqlTransaction _Transaction;
        internal SqlTransaction Transaction
        {
            //Retourne l'objet SqlCommand
            get { return _Transaction; }

            set { _Transaction = value; }
        }

        internal void Commit()
        {
            _Transaction.Commit();
        }

        internal void RollBack()
        {
            _Transaction.Rollback();
        }

        internal void Dispose()
        {
            _Transaction.Dispose();
        }

    }

}
