using System;
using QRCoder;
using System.Drawing;
using System.IO;

public class QrCodeGenerator
{
    public string GenerateQrCodeAsBase64(string textToEncode, int width = 300, int height = 300, int pixelsPerModule = 55)
    {
        // 1. Générer le QR Code
        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
        using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(textToEncode, QRCodeGenerator.ECCLevel.Q))
        using (QRCode qrCode = new QRCode(qrCodeData))
        {
            // 2. Créer l'image bitmap avec la taille spécifiée
            Bitmap qrCodeImage = qrCode.GetGraphic(pixelsPerModule, Color.Black, Color.White, false);

            // 3. Redimensionner à la taille demandée
            Bitmap resizedImage = new Bitmap(qrCodeImage, new Size(width, height));

            // 4. Convertir en Base64
            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);

                // Optionnel: Ajouter le préfixe pour les données URI
                return base64String;
            }
        }
    }
}

