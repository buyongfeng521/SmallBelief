using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SecurityHelper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="sIn"></param>
        /// <returns></returns>
        public static string GetMD5(string sIn)
        {
            //salt
            sIn += "@SB";

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytValue, bytHash;
            bytValue = System.Text.Encoding.UTF8.GetBytes(sIn);
            bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            string sTemp = "";
            for (int i = 0; i < bytHash.Length; i++)
            {
                sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
            }
            return sTemp.ToLower();
        }



        //加密IV向量
        private static byte[] IV = { 0x14, 0xFC, 0x29, 0xC8, 0x9A, 0xCB, 0xCA, 0xE7 };
        //public static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private static string Key = "souxihuan365";
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String Encrypt(String str)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(Key.Substring(0, 8));
            byte[] bIV = IV;
            byte[] bStr = Encoding.UTF8.GetBytes(str);
            try
            {
                DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, desc.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write);
                cStream.Write(bStr, 0, bStr.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// DESC解密
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="DecryptStr"></param>
        /// <returns></returns>
        public static String Decrypt(String DecryptStr)
        {
            try
            {
                byte[] bKey = Encoding.UTF8.GetBytes(Key.Substring(0, 8));
                byte[] bIV = IV;
                byte[] bStr = Convert.FromBase64String(DecryptStr);
                DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, desc.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write);
                cStream.Write(bStr, 0, bStr.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return string.Empty;
            }
        }



    }
}
