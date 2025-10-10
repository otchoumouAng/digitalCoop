using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Ext.Net;

[assembly: WebResource("Tms.Components/Settings/Menu.css", "text/css")]

namespace Tms.Components.Settings
{
    public class Menu : Ext.Net.Menu
    {
        protected override List<ResourceItem> Resources
        {
            get
            {

                List<ResourceItem> baseList = base.Resources;
                baseList.Capacity += 1;
                baseList.Add(new ClientStyleItem(typeof(Menu), "Tms.Components/Settings/Menu.css", "Tms.Components/Settings/Menu.css"));
                return baseList;
            }
        }

        public Menu()
        {
            //ajouter par Angoua  pour changer la couleur de l'arrière plan du menu 
            //la couleur choisie est identique à celle du panneau des fonctions.
            // @ le fichier Styles/Menu.css             
            this.BodyCls = "x-Menu-body";
        }
    }
}
