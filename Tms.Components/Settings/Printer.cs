using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tms.Components.Data;

namespace Tms.Components.Settings
{
    public class Printer
    {
        string _Icon;
        string _NomImprimante;
        string _IsSelected;



        [DataDisplayStyle(DefaultWidth = 5, Text = "", Visible = true)]
        public string Icon
        {
            get { return _Icon; }
            set { _Icon = value; }
        }

        [DataDisplayStyle(DefaultWidth = 80, Text = "Nom imprimante", Visible = true)]
        public string NomImprimante
        {
            get { return _NomImprimante; }
            set { _NomImprimante = value; }
        }


        [DataDisplayStyle(DefaultWidth = 50, Text = "Configurée ?", Visible = true)]
        public string IsSelected
        {
            get { return _IsSelected; }
            set { _IsSelected = value; }
        }


        public int MergedStatus
        {
            get
            {
                return 6;
            }
        }
    }
}
