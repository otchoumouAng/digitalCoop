using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLib
{
    [ServiceContract]
    public interface IService
    {

        [OperationContract]
        // permet de télécharger les états crystal reports du serveur web vers le client
        string DownLoadFile(byte[] binaryData);        

        [OperationContract]
        // Liste les imprimantes installées sur le client
        List<string> PrintersInstalled();

        [OperationContract]
        // Liste les bacs installées sur le client
        List<string> TraysInstalled();

        [OperationContract]
        // Obtenir l'imprimante par défaut du client
        string GetDefaultPrinter();

        [OperationContract]
        // Imprimer automatiquement l'état (impression directe vers l'imprimante)       
        string DirectPrintReport(string pathFile, string mListeParametre, string printerName, int nCopies, bool collated, int startPageN, int endPageN, string trayName);

        [OperationContract]
        // Imprimer automatiquement l'état (impression directe vers l'imprimante)       
        string DirectPrintTicket(string ParmReport, string Report);

        [OperationContract]
        // Imprimer automatiquement l'état (impression directe vers l'imprimante)       
        string DirectPrintTicketDevXRpt(string ReportName, string Parameters);

        //[OperationContract]
        //// Imprimer automatiquement l'état (impression directe vers l'imprimante)       
        //void DirectPrintCodeBarLabel(string JsonStringObject, string PrinterName);

        [OperationContract]
        // Obtenir l'imprimante code barre configurée (stockée dans un fichier xml coté client)   
        string GetTicketPrinter();

        [OperationContract]
        // Obtenir l'imprimante code barre configurée (stockée dans un fichier xml coté client)    
        string GetBarCodePrinter();

        [OperationContract]
        // Obtenir le bac de l'imprimante (stockée dans un fichier xml coté client)  
        string GetTrayName();

        [OperationContract]
        // Stocker le nom de l'imprimante  ticket sélectionnée dans un fichier xml coté client
        void SaveTicketPrinter(string mCurrentPrinterName);

        [OperationContract]
        // Stocker le nom de l'imprimante code barre sélectionnée dans un fichier xml coté client
        void SaveBarCodePrinter(string mCurrentPrinterName);

        [OperationContract]
        // Stocker le nom du bac sélectionnée dans un fichier xml coté client
        void SaveTrayName(string mCurrentPrinterName);

        [OperationContract]
        // Obtenir le poids capturé coté client
        int GetWeight();

        [OperationContract]
        // Obtenir le poids capturé coté client
        int GetWeightTest(string values);

        [OperationContract]
        // Obtenir le nom de la machine sur laquelle le service tourne
        string GetMachineName();


    }
}