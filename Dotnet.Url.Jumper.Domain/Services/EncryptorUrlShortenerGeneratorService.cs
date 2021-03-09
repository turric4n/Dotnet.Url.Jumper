using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Dotnet.Url.Jumper.Domain.Services
{
    public class EncryptorUrlShortenerGeneratorService : IUrlShortenerGeneratorService
    {
        public int Decode(string str)
        {
            byte[] inputByteArray = new byte[str.Length + 1];
            byte[] rgbIV = { 0x21, 0x43, 0x56, 0x87, 0x10, 0xfd, 0xea, 0x1c };
            byte[] key = { };

            try
            {
                var temp = str;
                temp = temp.Replace('-', '+'); // 62nd char of encoding
                temp = temp.Replace('_', '/'); // 63rd char of encoding
                switch (temp.Length % 4) // Pad with trailing '='s
                {
                    case 0:
                        break; // No pad chars in this case
                    case 2:
                        temp += "==";
                        break; // Two pad chars
                    case 3:
                        temp += "=";
                        break; // One pad char
                    default:
                        throw new Exception("Illegal base64 url string!");
                }

                key = System.Text.Encoding.UTF8.GetBytes("A0D1nX0Q");
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    inputByteArray = Convert.FromBase64String(temp);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, rgbIV), CryptoStreamMode.Write))
                        {
                            cs.Write(inputByteArray, 0, inputByteArray.Length);
                            cs.FlushFinalBlock();
                            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                            int result = 0;
                            int.TryParse(encoding.GetString(ms.ToArray()), out result);
                            return result;
                        }
                    }                      
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string Encode(int num)
        {
            byte[] inputByteArray = Encoding.UTF8.GetBytes(num.ToString());
            byte[] rgbIV = { 0x21, 0x43, 0x56, 0x87, 0x10, 0xfd, 0xea, 0x1c };
            byte[] key = { };
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes("A0D1nX0Q");
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, rgbIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        var temp = Convert.ToBase64String(ms.ToArray());
                        temp = temp.Split('=')[0]; // Remove any trailing '='s
                        temp = temp.Replace('+', '-'); // 62nd char of encoding
                        temp = temp.Replace('/', '_'); // 63rd char of encoding
                        return temp;
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
