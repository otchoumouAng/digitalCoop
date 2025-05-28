using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Components.Settings
{
    public class EnumReportDefinition
    {
        public const string TICKETPESEECAISSE = "TicketPeseeCaisse";
        public const string TICKETPESEENORMAL = "TicketPeseeNormal";
        public const string FACTURE = "Facture";
        public const string BONDELIVRAISON = "BonDeLivraison";
        public const string PAYEMENT = "Payement";
        public const string PAYEMENTTRANSPORT = "PayementTransport";

        public enum reportNum
        {
            TicketPeseeCaisse = 0,
            TicketPeseeNormal = 1,
            Facture = 2,
            BONDELIVRAISON = 3,
            Payement = 4,
            PayementTransport = 5,
        };
    }
}
