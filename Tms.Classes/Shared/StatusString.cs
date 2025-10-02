using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ext.Net.MVC;

namespace Tms.Classes.Shared
{
    [Proxy(Read = "~/StatusString/LoadForFinancing")]
    [JsonReader(RootProperty = "data")]
    public class StatusString
    {
        #region "Fields"
        private string _ID;
        private string _Description;
        #endregion

        #region "Properties"
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        #endregion

        #region "Members"

        public StatusString()
        {
            //
        }

        public StatusString(string mIdStatut)
        {
            _ID = mIdStatut;
        }

        public override string ToString()
        {
            return _Description;
        }

        #endregion

      
    }
}
