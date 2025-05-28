using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Tms.Components.Data
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class DataDisplayStyle : System.Attribute
    {
        private string _Text = string.Empty;
        private int _DefaultWidth = 100;
        private bool _Visible = false;
        private string _DisplayFormat = string.Empty;
        private string _EnglishText = string.Empty;

        private bool _ExtendedProperty = false;
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        public int DefaultWidth
        {
            get { return _DefaultWidth; }
            set { _DefaultWidth = value; }
        }

        public bool Visible
        {
            get { return _Visible; }
            set { _Visible = value; }
        }

        public string DisplayFormat
        {
            get { return _DisplayFormat; }
            set { _DisplayFormat = value; }
        }

        public string EnglishText
        {
            get { return _EnglishText; }
            set { _EnglishText = value; }
        }

        public bool ExtendedProperty
        {
            get { return _ExtendedProperty; }
            set { _ExtendedProperty = value; }
        }

    }
}
