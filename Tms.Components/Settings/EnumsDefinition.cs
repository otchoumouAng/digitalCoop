using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tms.Components.Settings
{
    public class EnumsDefinition
    {
        public const string UPDATE = "Update";
        public const string ADD_NEW = "AddNew";
        public const string CONSULT = "Consult";
        public const string DEFAULT = "Default";
        public const string APPROVE = "Approve";
        public const string EXTEND = "Extend";



        public enum eExecMode
        {
            Default = 0,
            AddNew = 1,
            Consult = 2,
            Update = 3,
            Approve = 4,
            Extend = 5,
        };

        public enum eTypeDeValeurs
        {
            Default = 0,
            Alphabetique = 1,
            Entier = 2,
            Decimal = 3
        };

        public static void SetExecMode(string idWindow, eExecMode execmode)
        {
            Ext.Net.X.Js.Call("setExecMode", idWindow, execmode);
        }

        public static void ResetControls(string idWindow)
        {
            Ext.Net.X.Js.Call("resetControls", idWindow);
        }

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }




        /*
        * retourner le mode d'exécution du formulaire en fonction du mode d'action du formulaire
        * @parameter un objet i.e. un hidden field qui contient l'action à réaliser (modifier, créér, consulter) avec le formulaire
        * @return eExecMode de type eExecMode
        */
        private Tms.Components.Settings.EnumsDefinition.eExecMode GetFormExecMode(object hidAction)
        {
            try
            {
                if (hidAction != null)
                {
                    if (hidAction.ToString().Equals(ADD_NEW))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew;
                    else if (hidAction.ToString().Equals(APPROVE))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Approve;
                    else if (hidAction.ToString().Equals(CONSULT))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Consult;
                    else if (hidAction.ToString().Equals(DEFAULT))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
                    else if (hidAction.ToString().Equals(UPDATE))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Update;
                    else if (hidAction.ToString().Equals(EXTEND))
                        return Tms.Components.Settings.EnumsDefinition.eExecMode.Extend;
                }
                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
            }
            catch (Exception ex)
            {
                throw ex;
                return Tms.Components.Settings.EnumsDefinition.eExecMode.Default;
            }
        }

        /*
         Définir le mode d'exécution de notre formulaire
         * @parameter execMode de type eExecMode
         * @return void
         */
        public object SetFormExecMode(Tms.Components.Settings.EnumsDefinition.eExecMode execMode)
        {
            try
            {
                switch (execMode)
                {
                    case Tms.Components.Settings.EnumsDefinition.eExecMode.AddNew:
                        return ADD_NEW;
                        break;

                    case Tms.Components.Settings.EnumsDefinition.eExecMode.Update:
                        return UPDATE;
                        break;

                    case Tms.Components.Settings.EnumsDefinition.eExecMode.Consult:
                        return CONSULT;
                        break;

                    case Tms.Components.Settings.EnumsDefinition.eExecMode.Approve:
                        return APPROVE;
                        break;
                    case Tms.Components.Settings.EnumsDefinition.eExecMode.Extend:
                        return EXTEND;
                        break;

                    default:
                        return DEFAULT;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


     

}
