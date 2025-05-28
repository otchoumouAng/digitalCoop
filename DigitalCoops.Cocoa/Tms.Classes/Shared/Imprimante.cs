using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class Imprimante
    {
        string _Icon;
        string _NomImprimante;
        bool _IsSelected;



        public string Icon
        {
            get { return _Icon; }
            set { _Icon = value; }
        }

        
        public string NomImprimante
        {
            get { return _NomImprimante; }
            set { _NomImprimante = value; }
        }


        
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { _IsSelected = value; }
        }


        public int mIcon
        {
            get
            {
                return 6;
            }
        }
    }

