using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;

namespace Tms.Classes.Business
{
    public class Refoulement
    {
        #region "Fields"

        private Guid _ID;
        private Livraison _Livraison;
        private Bascule _Bascule;
        private int _Numero;
        private DateTime _DateP1;
        private decimal _PoidsP1;
        private DateTime _DateP2;
        private decimal _PoidsP2;
        private decimal _PoidsBrut;
        private bool _Desative;

        #endregion

        #region "Properties"

        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Livraison Livraison
        {
            get { return _Livraison; }
            set { _Livraison = value; }
        }


        public Bascule Bascule
        {
            get { return _Bascule; }
            set { _Bascule = value; }
        }


        public int Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }


        public DateTime DateP1
        {
            get { return _DateP1; }
            set { _DateP1 = value; }
        }


        public decimal PoidsP1
        {
            get { return _PoidsP1; }
            set { _PoidsP1 = value; }
        }

        public DateTime DateP2
        {
            get { return _DateP2; }
            set { _DateP2 = value; }
        }


        public decimal PoidsP2
        {
            get { return _PoidsP2; }
            set { _PoidsP2 = value; }
        }


        public decimal PoidsBrut
        {
            get { return _PoidsBrut; }
            set { _PoidsBrut = value; }
        }

        public bool Desactive
        {
            get { return _Desative; }
            set { _Desative = value; }
        }

        #endregion
    }
}
