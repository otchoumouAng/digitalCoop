using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ext.Net;
using System.Web.UI;

[assembly: WebResource("CwaWebComponents/cwaMenuItem/cwaMenuItem.css", "text/css")]

namespace CwaWebComponents.webComponents
{
    public class cwaMenuItem : Ext.Net.MenuItem
    {
        protected override List<ResourceItem> Resources
        {
            get
            {

                //const string src = "WamsWebEditionPro/_Learning/Synthese/WebComponents/cwaGridPanel.";
                List<ResourceItem> baseList = base.Resources;
                baseList.Capacity += 1;
                baseList.Add(new ClientStyleItem(typeof(cwaMenuItem), "CwaWebComponents/cwaMenuItem/cwaMenuItem.css", "CwaWebComponents/cwaMenuItem/cwaMenuItem.css"));
                return baseList;
            }
        }

        public cwaMenuItem()
        {
            //ajouter par Angoua  pour changer la couleur de l'arrière plan du menu 
            //la couleur choisie est identique à celle du panneau des fonctions.
            // @ le fichier Styles/cwaMenuItem.css             
            this.Cls = "x-cwaMenu-body";
        }
    }
}
