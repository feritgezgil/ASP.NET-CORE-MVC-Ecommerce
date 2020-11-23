using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UI.Models
{
    public class SecurityModel
    {
        private static readonly string crptyKey = "ilkerturan";

        public SecurityModel()
        {
                
        }

        public static string Encrypt(string data)
        {
            string encryptedData = "";

            byte[] eData = UTF8Encoding.UTF8.GetBytes(data);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(crptyKey));
                using(TripleDESCryptoServiceProvider tripleDES=new TripleDESCryptoServiceProvider()
                {
                    Key = key,
                    Mode =CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                })
                {
                    ICryptoTransform transform = tripleDES.CreateEncryptor();
                    byte[] result = transform.TransformFinalBlock(eData, 0, eData.Length);
                    encryptedData = Convert.ToBase64String(result,0,result.Length);
                }
            }

            return encryptedData;
        }

        public static string Decrypt(string data)
        {
            string decryptedData = "";

            byte[] dData = Convert.FromBase64String(data);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(crptyKey));
                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider()
                {
                    Key = key,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                })
                {
                    ICryptoTransform transform = tripleDES.CreateDecryptor();
                    byte[] result = transform.TransformFinalBlock(dData, 0, dData.Length);
                    decryptedData = UTF8Encoding.UTF8.GetString(result);
                }
            }

            return decryptedData;
        }
    }
}
