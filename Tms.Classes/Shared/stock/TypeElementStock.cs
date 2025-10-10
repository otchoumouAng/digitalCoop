using Ext.Net.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Components.Data;

namespace Tms.Classes.Shared.stock
{
    public class TypeElementStock : DataPersist
    {
        #region "Fields"

        private int _ID;
        private string _Designation;
        private bool _Desactive;
        private string _Mouvement;
        #endregion

        #region "Properties"
        [ModelField(IDProperty = true)]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }

        public bool Desactive
        {
            get { return _Desactive; }
            set { _Desactive = value; }
        }

        [Column(Text = "AsString")]
        public string AsString
        {
            get { return _Designation; }
        }

        public int mIcon
        {
            get
            {
                if (_Desactive)
                    return 0; // BulletCross                
                else
                    return 2; //                     
            }
        }

        public string Mouvement
        {
            get
            {
                return _Mouvement;
            }

            set
            {
                _Mouvement = value;
            }
        }
        #endregion

        #region Constructor
        public TypeElementStock()
        {

        }

        public TypeElementStock(int myId)
        {
            this.fnGet(myId);
        }

        #endregion

        #region Methods
        public override bool fnActivate()
        {
            throw new NotImplementedException();
        }

        public override bool fnDeActivate()
        {
            throw new NotImplementedException();
        }

        public override bool fnGet(object Id)
        {
            throw new NotImplementedException();
        }

        public override List<DataPersist> fnSelect()
        {
            throw new NotImplementedException();
        }

        public override bool fnUpdate()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public partial class TypeElementStockViewModel
    {
        public TypeElementStock _TypeElementStock { get; set; }
        public Tms.Components.Settings.EnumsDefinition.eExecMode _ExecMode { get; set; }
    }
}
