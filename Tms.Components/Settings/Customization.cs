using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tms.Components.Settings
{
    public class Customization
    {

        #region "Fields"
        string _id;
        string _label;
        int _width;
        string _align;
        string _type;
        string _format;
        string _dictionnaire;
        string _instanceOf;
        string _xType;



        #endregion

        #region "Properties"
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }


        public string Label
        {
            get { return _label; }
            set { _label = value; }
        }


        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }


        public string Align
        {
            get { return _align; }
            set { _align = value; }
        }


        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }


        public string Format
        {
            get { return _format; }
            set { _format = value; }
        }


        public string Dictionnaire
        {
            get { return _dictionnaire; }
            set { _dictionnaire = value; }
        }



        public string InstanceOf
        {
            get { return _instanceOf; }
            set { _instanceOf = value; }
        }

        public string XType
        {
            get { return _xType; }
            set { _xType = value; }
        }
        #endregion
    }
}
