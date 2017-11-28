/*********************************
 * 作者：xzf
 * 日期：2017-06-18 00:13:02
 * 操作：创建
 * ********************************/
using SteponTech.Utils.DHSH.Model.Config;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SteponTech.Utils.DHSH.Utils
{
    /// <summary>
    /// 通用AES加密解密工具类
    /// </summary>
    public class AesHelper
    {
       
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="data">需要加密数据</param>
        /// <returns>加密后的数据</returns>
        public virtual string Encrypt(string data, AESConfig config)
        {
            if (string.IsNullOrEmpty(data)) return null;
            var size = 16;
            var rm = Aes.Create();
            rm.Key = config.Key;
            rm.Mode = CipherMode.CBC;
            rm.IV = config.Iv;
            rm.Padding = PaddingMode.None;
            var ctf = rm.CreateEncryptor();
            byte[] dataArray = Encoding.UTF8.GetBytes(data);
            if (dataArray.Length % size != 0)
            {
                var temp = new byte[dataArray.Length + (size - (dataArray.Length % size))];
                dataArray.CopyTo(temp, 0);
                dataArray = temp;
            }
            byte[] result = ctf.TransformFinalBlock(dataArray, 0, dataArray.Length);
            var arr = Convert.ToBase64String(result);
            return arr;
        }
        /*
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="data">需要加密数据</param>
        /// <param name="config">加密配置</param>
        /// <returns>加密后的数据</returns>
        public string Encrypt(string data, AESConfig config)
        {
            var encryptKey = config.Key;
            var aesAlg1 = Aes.Create();
           // aesAlg1.
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.None;
                using (var encryptor = aesAlg.CreateEncryptor(encryptKey, config.Iv))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor,
                            CryptoStreamMode.Write))

                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            if (data.Length % 16 != 0) {
                                data = data + Encoding.UTF8.GetString(new byte[16- data.Length % 16]);
                            }
                            swEncrypt.Write(data);
                        }
                        var iv = aesAlg.IV;
                        var decryptedContent = msEncrypt.ToArray();
                        var result = new byte[iv.Length + decryptedContent.Length];
                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result,
                            iv.Length, decryptedContent.Length);
                        return Convert.ToBase64String(result);
                    }
                }
            }
        }*/

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="data">需要解密数据</param>
        /// <returns>解密后的数据</returns>
        public virtual string Decrypt(string data, AESConfig config)
        {
            byte[] toEncryptArray = Convert.FromBase64String(data);

            var rm = Aes.Create();
            rm.Key = config.Key;
            rm.Mode = CipherMode.CBC;
            rm.IV = config.Iv;
            rm.Padding = PaddingMode.None;
          
            var cTransform = rm.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }
        /*
        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="data">需要解密数据</param>
        /// <param name="config">解密配置</param>
        /// <returns>解密后的数据</returns>
        public string Decrypt(string data, AESConfig config)
        {
            var fullCipher = Convert.FromBase64String(data);
            var iv = config.Iv;
            var cipher = new byte[16];
            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var decryptKey = config.Key;
            var aesAlg = Aes.Create();
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.None;
            var decryptor = aesAlg.CreateDecryptor(decryptKey, iv);
            string result;
            var msDecrypt = new MemoryStream(cipher);
            var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            var srDecrypt = new StreamReader(csDecrypt);
            result = srDecrypt.ReadToEnd();
            return result;
        }*/
    }
}
