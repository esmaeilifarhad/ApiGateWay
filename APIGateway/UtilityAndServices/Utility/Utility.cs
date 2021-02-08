using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UtilityAndServices.Utility
{
   public class Utility
    {
        #region RSA
        public static Dictionary<string, string> RSAGenerator()
        {
            var dictionary = new Dictionary<string, string>();
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                string pubKey = rsa.ToXmlString(false);
                string prvKey = rsa.ToXmlString(true);
                dictionary.Add("PublicKey", pubKey);
                dictionary.Add("PrivateKey", prvKey);
            }
            return dictionary;
        }

        public static string RSAEncryption(string strText, string pubKey)
        {
            var byteData = Encoding.UTF8.GetBytes(strText);
            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    rsa.FromXmlString(pubKey.ToString());
                    var encryptedData = rsa.Encrypt(byteData, true);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        public static string RSADecryption(string strText, string prvKey)
        {
            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    var base64Encrypted = strText;
                    // server decrypting data with private key                    
                    rsa.FromXmlString(prvKey);
                    var resultBytes = Convert.FromBase64String(base64Encrypted);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        public static void CreateLog(string message)
        {
            var path = HttpContext.Current.Server.MapPath("~/Log");
            Directory.CreateDirectory(path);

            path = path + "\\Log.txt";
            if (!File.Exists(path))
            {

                FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
                StreamWriter str = new StreamWriter(fs);
                str.BaseStream.Seek(0, SeekOrigin.End);
                str.WriteLine("start.........................");
                str.WriteLine(DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToLongDateString());
                str.WriteLine(message);
                str.WriteLine("End.........................");
                string addtext = "this line is added" + Environment.NewLine;
                str.Flush();
                str.Close();
                fs.Close();

            }
            else if (File.Exists(path))
            {
                TextWriter str = new StreamWriter(path, true);
                str.WriteLine("start.........................");
                str.WriteLine(DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToLongDateString());
                str.WriteLine(message);
                str.WriteLine("End.........................");
                str.Close();
            }
        }
        #endregion

    }
}
