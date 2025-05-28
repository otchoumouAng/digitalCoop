using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Components.Settings
{
    public class MenuAccess
    {
        private bool _CanAdd;
        private bool _CanEdit;
        private bool _CanCancel;
        private bool _CanEnable;
        private bool _CanDisable;
        private bool _CanExtend;
        private bool _CanPrint;

        public bool CanAdd
        {
            get
            {
                return _CanAdd;
            }

            set
            {
                _CanAdd = value;
            }
        }

        public bool CanEdit
        {
            get
            {
                return _CanEdit;
            }

            set
            {
                _CanEdit = value;
            }
        }

        public bool CanCancel
        {
            get
            {
                return _CanCancel;
            }

            set
            {
                _CanCancel = value;
            }
        }

        public bool CanEnable
        {
            get
            {
                return _CanEnable;
            }

            set
            {
                _CanEnable = value;
            }
        }

        public bool CanDisable
        {
            get
            {
                return _CanDisable;
            }

            set
            {
                _CanDisable = value;
            }
        }

        public bool CanExtend
        {
            get
            {
                return _CanExtend;
            }

            set
            {
                _CanExtend = value;
            }
        }

        public bool CanPrint
        {
            get
            {
                return _CanPrint;
            }

            set
            {
                _CanPrint = value;
            }
        }
    }
}
