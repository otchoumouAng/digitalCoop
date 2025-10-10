using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Security.Cryptography;


namespace Tms.Components.Data
{
    /// <summary>
	/// ***************************** CryptageTexte *********************************
	/// Namespace simple assurant le cryptage et le décryptage de chaînes de 
	/// caractères via la classe Crypto.
	/// Niveau : Débutant (je le suis moi même).
	/// Elle a pour but d'éveiller au cryptage sous Framework .NET.
	/// DELCHER Pierre. 2004. pierre@shinkan.org
	/// 
	/// Fonctions implémentées : Crypter(Textebrut), Decrypter(CypherTexte).
	/// *****************************************************************************
	/// </summary>
	public class DataEncryption
    {
        // Fields
        //' La cryptographie utilisée est symmétrique ; c'est à dire qu'elle ne
        //' nécessite qu'une clef privée, qui est la même pour le cryptage et le
        //' décryptage. C'est pourquoi nous déclarons un tableau de "byte" nommé Clef,
        //' qui contient la clef de 16 octets que l'algorithme de cryptage utilisé
        //' nécessite. Les octets doivent être écrits en héxadécimal, sous la forme 
        //' 0xAB ou A et B représentent des caractères héxadécimaux (de 0 à F).
        //' Vous pouvez donc les modifier, puisqu'il s'agira de votre clef.

        private byte[] Clef;
        //' Egalement nécessaire à l'algorithme, le "vecteur d'initialisation" 
        //' intervient comme opérande de la fonction XOR réalisée par l'algorithme
        //' avec la Clef et les données. Il est aussi unique et privé, vous pouvez le 
        //' modifier.Il est écrit en hexadécimal.
        private byte[] Vect;
        //' Le Framework .NET fournit de nombreux algorithmes de cryptage puissants
        //' dans le namespace System.Security.Cryptography, déclaré dans les directives
        //' "using".
        //' Nous choisirons l'algorithme dit de Rijndael (prononcer "Rain Doll").
        //' Successeur du DES, et adopté comme standard à l'Advanced Encryption
        //' Standard, il supporte des clefs jusqu'à 256 bits (16 octets), mais la
        //' législation de l'AES impose une clef maximum de 128bits (8 octets).
        //' Il a l'avantage d'être rapide et jusqu'à présent très résistant.
        //' Nous créons donc un objet de la classe RijndaelManaged fournie par le
        //' Framework .NET, et nous l'appelons "rj".

        private RijndaelManaged rj;

        internal byte[] Crypter(byte[] TexteBrutByte)
        {
            //' Je vous amène à suivre la procédure avec le schéma fourni (Schema.gif).

            //' Créons un flux de données mémoire "CypherTexteMem", dans lequel nous 
            //' placerons le texte crypté. Elle sera considérée comme mémoire tampon.
            MemoryStream CypherTexteMem = new MemoryStream();
            //' Créons un flux de cryptage "CStream", nécessaire pour crypter.
            //' Nous lui indiquons que le flux à crypter doit être écrit dans
            //' CypherTexteMem, notre mémoire tampon, que la méthode de cryptage est 
            //' l'algorithme rijndael initialisé avec la Clef et le Vecteur définis, 
            //' et que son but est d'écrire des données.
            CryptoStream CStream = new CryptoStream(CypherTexteMem, this.rj.CreateEncryptor(this.Clef, this.Vect), CryptoStreamMode.Write);
            //' Le nécessaire en flux étant préparé, nous allons passer au cryptage
            //' proprement dit.
            //' Ceci étant fait, nous cryptons maintenant le tableau d'octets obtenu
            //' grâce au flux de cryptage, et ce du début (0) jusqu'à la fin (définie
            //' par la taille du tableau d'octets) ; ce qui est normal ...!
            CStream.Write(TexteBrutByte, 0, TexteBrutByte.Length);
            //' Nous fermons maintenant le flux de cryptage, afin que son opération
            //' se termine, que la mémoire soit libérée, et que ce qu'il a écrit en
            //' mémoire tampon soit utilisable.
            CStream.Close();
            //' Nous allons maintenant créer un tableau d'octets qui contiendra les
            //' données qui ont été écrites par le flux de cryptage dans le flux de
            //' mémoire tampon (CypherTextMem).
            byte[] CypherTexteByte = CypherTexteMem.ToArray();
            //' Le flux de mémoire tampon ne nous sert plus, nous pouvons le fermer car 
            //' les données cryptées ont été copiées dans le tableau d''octets
            //' CypherTexteByte.
            CypherTexteMem.Close();
            return CypherTexteByte;
        }
        //Methodes
        //' ************************ CRYPTER(Textebrut)*******************************
        /// <summary>
        /// Fonction de cryptage : elle necessite en argument une chaîne de caractères,
        /// et renvoie une chaîne de caractères cryptée (cipher-text).
        /// </summary>
        /// <param name="TexteBrut"></param>
        /// <returns name="string CypherTexte"></returns>
        internal string Crypter(string TexteBrut)
        {
            //' Nous converstissons d'abord le texte entré en argument (Textebrut)
            //' en tableau d'octets (byte), car les algorithmes de cryptage ne
            //' travaillent directement qu'avec ce type de données.
            byte[] TextebrutByte = new UnicodeEncoding().GetBytes(TexteBrut);
            byte[] CypherTexteByte = Crypter(TextebrutByte);
            //' Nous convertissons maintenant les données cryptées en chaîne de
            //' caractère cryptée, que nous copions dans CypherTexte.
            string CypherTexte = Convert.ToBase64String(CypherTexteByte);
            //New UnicodeEncoding().GetString(CypherTexteByte)
            //' Nous retournons le texte crypté.
            return CypherTexte;
        }

        internal byte[] Decrypter(byte[] CypherTexteByte)
        {
            MemoryStream CypherTexteMem = new MemoryStream(CypherTexteByte);
            //' Nous créons un flux de cryptage initialisé de la même manière que
            //' celui vu plus haut, mais celui ci va devoir lire, non plus écrire.
            CryptoStream CStream = new CryptoStream(CypherTexteMem, this.rj.CreateDecryptor(this.Clef, this.Vect), CryptoStreamMode.Read);
            //' Nous créons une deuxième mémoire tampon destinée cette fois ci au texte
            //' décrypté.
            MemoryStream TextebrutMem = new MemoryStream();
            while (true)
            {
                //' Créé un tableau d'octets dans lequel nous placeront le texte lu
                //' en provenance du flux de cryptage.
                byte[] buf = new byte[100];
                //' Nous lisons le flux de cryptage.
                int BytesLus = CStream.Read(buf, 0, 100);
                //' Arrête la lecture lorsque nous avons atteint la fin du flux.
                if ((0 == BytesLus))
                {
                    break; // TODO: might not be correct. Was : Exit Do
                }
                //' Nous écrivons le texte brut obtenu dans la mémoire tampon.
                TextebrutMem.Write(buf, 0, BytesLus);
            }
            //' Nous fermons le flux de cryptage.
            CStream.Close();
            //' Nous fermons la mémoire tampon du texte crypté.
            CypherTexteMem.Close();
            //' Nous placons les données de la mémoire tampon du texte brut dans un 
            //' tableau d''octets.
            byte[] TextebrutByte = TextebrutMem.ToArray();
            //' Nous fermons la mémoire tampon du texte brut.
            TextebrutMem.Close();
            return TextebrutByte;
        }
        //' ************************ DECRYPTER(Textebrut)*****************************
        /// <summary>
        /// Fonction de décryptage : elle necessite en argument une chaîne de 
        /// caractères cryptés (cipher-text) et renvoie une chaîne de caractères.
        /// </summary>
        /// <param name="CypherTexte"></param>
        /// <returns name="string Textebrut"></returns>
        internal string Decrypter(string CypherTexte)
        {
            //' Le décryptage est l'opération exactement inverse du cryptage, et se code
            //' d'une manière très proche.
            //' Nous créons une mémoire tampon contenant le texte crypté.
            byte[] CypherTexteByte = Convert.FromBase64String(CypherTexte);
            //New UnicodeEncoding().GetBytes(CypherTexte)
            byte[] TextebrutByte = Decrypter(CypherTexteByte);
            //' Nous transformons les données octales en texte.
            string Textebrut = new UnicodeEncoding().GetString(TextebrutByte);
            //' Nous retournons le texte.
            return Textebrut;
        }


        public object CryptageAssymetrique(string Motdepasse)
        {
            UnicodeEncoding uEncode = new UnicodeEncoding();
            string salt = "ibm";
            byte[] bytClearString = uEncode.GetBytes(Motdepasse + salt);
            System.Security.Cryptography.SHA256Managed sha = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha.ComputeHash(bytClearString);
            return Convert.ToBase64String(hash);
        }

        public DataEncryption()
        {
            this.Clef = new byte[] {
                100,
                2,
                3,
                40,
                5,
                96,
                87,
                8,
                9,
                16,
                17,
                18,
                19,
                20,
                21,
                225
            };
            this.Vect = new byte[] {
                250,
                2,
                3,
                84,
                5,
                6,
                17,
                8,
                9,
                16,
                17,
                18,
                19,
                20,
                21,
                (byte)22D
            };
            this.rj = new RijndaelManaged();
        }


        public DataEncryption(byte[] key, byte[] IV)
        {
            this.Clef = key;
            this.Vect = IV;
            this.rj = new RijndaelManaged();
        }

    }

}
