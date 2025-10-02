using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Classes.Shared;
using Tms.Components.Data;

namespace Tms.Classes.Business
{
    public class Facture_Prelevement : DataPersist
    {
        #region "Fields"
        private Guid _DeductionID;
        private string _Numero;   
        private decimal _MontantPreleve;
        private int _TypeID;
        private string _Libelle;
        private int _ElementTypeID;

        #endregion

        #region "Properties"  

        public Guid DeductionID
        {
            get { return _DeductionID; }
            set { _DeductionID = value; }
        }

        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }
        
        public decimal MontantPreleve
        {
            get { return _MontantPreleve; }
            set { _MontantPreleve = value; } 
        }

        public int TypeID
        {
            get { return _TypeID; }
            set { _TypeID = value; }

        }

        public string Libelle
        {
            get { return _Libelle; }
            set { _Libelle = value; }
        }

        public int ElementTypeID
        {
            get { return _ElementTypeID; }
            set { _ElementTypeID = value; }

        }


        #endregion

        #region Constructor
        public Facture_Prelevement()
        {

        }
       
        #endregion

        #region Methods
        public override bool fnGet(object Id)
        {
            return false;
        }
        
        public override List<DataPersist> fnSelect()
        {
            return null;
        }
        
        public override bool fnUpdate()
        {
            return false;
        }

        public override bool fnActivate()
        {
            return false;
        }

        public override bool fnDeActivate()
        {
            return false;
        }
        
        #endregion

        #region "Private Members"

        public override string ToString()
        {
            return _Numero;
        }        

        #endregion
    }

    
}
