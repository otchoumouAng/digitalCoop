using Ext.Net.MVC;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Tms2017.MVC.Controllers
{
    public class DeplacementPaletteTypeController : Controller
    {
        /// <summary>
        /// Fournit la liste des types de déplacement (Manuel ou Auto) pour la ComboBox.
        /// </summary>
        public ActionResult LoadTypeDeplacementPaletteAll()
        {
            // Création d'une liste statique des types de déplacement
            var typesDeplacement = new List<object>
            {
                // La valeur "-1" pour "{Tous}" est gérée côté client dans le ComboBox
                new { ID = "manuel", Designation = "Déplacement Manuel" },
                new { ID = "auto", Designation = "Déplacement Automatique (QR Code)" }
            };

            // Ajout de l'option "Tous" en première position pour le filtre
            var listePourLeStore = new List<object>
            {
                new { ID = "-1", Designation = "{Tous}" }
            };
            listePourLeStore.AddRange(typesDeplacement);


            return this.Store(listePourLeStore);
        }
    }
}