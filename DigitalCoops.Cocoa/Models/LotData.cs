using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalCoops.Cocoa.Models
{
    public class LotData
    {
        [Column("id_lot")]
        public int IdLot { get; set; }

        [Column("numerolot")]
        public string NumeroLot { get; set; }

        [Column("code_exportateur")]
        public int CodeExportateur { get; set; }

        [Column("exportateur")]
        public string Exportateur { get; set; }

        [Column("campagne")]
        public string Campagne { get; set; }

        [Column("code_produit")]
        public int CodeProduit { get; set; }

        [Column("dateusinage")]
        public DateTime DateUsinage { get; set; }

        [Column("nombresacusine")]
        public int NombresAcusine { get; set; }

        [Column("poidsbrut")]
        public double PoidsBrut { get; set; }

        [Column("poidsusine")]
        public double PoidsUsine { get; set; }

        [Column("tarepalette")]
        public double TarePalette { get; set; }

        [Column("taresac")]
        public double TareSac { get; set; }

        // New columns in the stored procedure:
        [Column("datereusinage")]
        public DateTime? DateReusinage { get; set; }

        [Column("nombresacreusine")]
        public double NombresAcreusine { get; set; }

        [Column("poidsbrutreusinage")]
        public double PoidsBrutReusinage { get; set; }

        [Column("poidsreusinage")]
        public double PoidsReusinage { get; set; }

        [Column("tarepalettereusinage")]
        public double TarePaletteReusinage { get; set; }

        [Column("codesite")]
        public int CodeSite { get; set; }

        [Column("nomsite")]
        public string NomSite { get; set; }

        [Column("code_magasin_initial")]
        public int CodeMagasinInitial { get; set; }

        [Column("magasin_initial")]
        public string MagasinInitial { get; set; }

        [Column("nombrepalette")]
        public int NombrePalette { get; set; }

        public string RawJson { get; set; }
        public List<DataItem> Items { get; set; }
        public DateTime ProcessedAt { get; set; }

    }

    public class DataItem
    {
        // Add properties matching your API response structure
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }


    
    public partial class GetDummyLotData
    {
        // Create dummy data for testing.
        List<LotData> dummyData = new List<LotData>
        {
            new LotData
            {
                IdLot = 1,
                NumeroLot = "LOT001",
                CodeExportateur = 101,
                Exportateur = "ExporterA",
                Campagne = "CampagneAlpha",
                CodeProduit = 2020,
                DateUsinage = DateTime.Now,
                NombresAcusine = 50,
                PoidsBrut = 1000.0,
                PoidsUsine = 980.0,
                TarePalette = 20.0,
                DateReusinage = DateTime.Now.AddDays(1),
                NombresAcreusine = 45,
                PoidsBrutReusinage = 990.0,
                PoidsReusinage = 970.0,
                TarePaletteReusinage = 15.0
            },
            new LotData
            {
                IdLot = 2,
                NumeroLot = "LOT002",
                CodeExportateur = 202,
                Exportateur = "ExporterB",
                Campagne = "CampagneBeta",
                CodeProduit = 3030,
                DateUsinage = DateTime.Now.AddDays(-1),
                NombresAcusine = 30,
                PoidsBrut = 800.0,
                PoidsUsine = 780.0,
                TarePalette = 18.0,
                DateReusinage = null,  // No reusinage for this lot
                NombresAcreusine = 0,
                PoidsBrutReusinage = 0,
                PoidsReusinage = 0,
                TarePaletteReusinage = 0
            }
            // You can always add more dummy records as needed.
        };
    }

}