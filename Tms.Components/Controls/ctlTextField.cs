using Ext.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

[assembly: WebResource("Tms.Components/Controls/cltTextField.css", "text/css")]
[assembly: WebResource("Tms.Components/Controls/cltTextField.js", "text/javascript")]
namespace Tms.Components.Controls
{
    public class ctlTextField : TextField
    {
        private bool _isrequired;

        public bool Isrequired
        {
            get
            {
                return _isrequired;
            }

            set
            {
                _isrequired = value;

                if (_isrequired)
                    this.SetRequired();
                else
                    this.SetNotRequired();
            }
        }


        private void  SetRequired()
        {
            this.AllowBlank = true;
            this.IndicatorCls = "IndicatorRedStar";
            this.IndicatorText = "*";
        }


        private void SetNotRequired()
        {
            this.AllowBlank = false;
            this.IndicatorCls = string.Empty;
            this.IndicatorText = string.Empty;
        }


        public void AlterProperty(string property_name_js, object property_value)
        {
            try
            {
                string fullItemID = "";
                fullItemID = this.ContainerID.Substring(0, this.ContainerID.Length - "_Container".Length);

                Ext.Net.X.Js.Call("alterProperty", fullItemID.Split('.')[1], property_name_js, property_value);
            }
            catch (Exception ex)
            {
                throw new Exception("ctlTextField : AlterProperty : " + ex.Message);
            }
        }


        protected override List<ResourceItem> Resources
        {
            get
            {
                const string scriptPath = "Tms.Components/Controls/cltTextField.";
                const string stylePath = "Tms.Components/Controls/cltTextField.";

                List<ResourceItem> baseList = base.Resources;
                baseList.Capacity += 1;
                baseList.Add(new ClientScriptItem(typeof(ctlTextField), scriptPath + "js", scriptPath + "js"));
                baseList.Add(new ClientStyleItem(typeof(ctlTextField), stylePath + "css", stylePath + "css"));
                return baseList;
            }
        }


        public override string InstanceOf
        {
            get
            {
                return "ctl.ctlTextField";
            }
        }

        public override string XType
        {
            get
            {
                return "ctlTextField";
            }
        }


        public override ConfigOptionsCollection ConfigOptions
        {
            get
            {
                ConfigOptionsCollection list = base.ConfigOptions;

                list.Add("isrequired", "false", this._isrequired);

                return list;
            }
        }


       



    }
}
