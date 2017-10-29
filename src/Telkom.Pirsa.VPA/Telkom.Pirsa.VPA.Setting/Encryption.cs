using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;

namespace Telkom.Pirsa.VPA.Setting
{
    public class Encryption
    {
        /// <summary>
        /// Encrypt specified string to Base64 format encrypted string 
        /// </summary>
        /// <param name="value">Plain text string to encrypt</param>
        /// <returns>The encrypted string</returns>
        public static string Encrypt(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("The specified string to encrypt is required!");


                byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(value);
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionConfiguration.Key, EncryptionConfiguration.InitializationVector);
                byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
                return Convert.ToBase64String(encryptedData);
            }
            catch (Exception ex)
            { 
                throw ExceptionHandling.GenerateException("Failed while executing Encrypt operation", ex);
            }
        }

        /// <summary>
        /// Decrypt specified string to its original plain format (if exist) otherwise throw exception 
        /// </summary>
        /// <param name="encypted">The encrypted string to decrypt</param>
        /// <returns>The plain original form of the string</returns>
        public static string Decrypt(string encypted)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(encypted);
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionConfiguration.Key, EncryptionConfiguration.InitializationVector);
                byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));
                return System.Text.Encoding.Unicode.GetString(decryptedData);
            }
            catch (Exception ex)
            {
                throw ExceptionHandling.GenerateException("Failed while executing Decrypt operation", ex);
            }
        }

        #region Private Helper Methods
        private static byte[] Encrypt(byte[] value,  byte[] key, byte[] iv)
        {
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = null;
            try
            {
                Aes alghoritm = Aes.Create();
                alghoritm.Key = key;
                alghoritm.IV = iv;
                cs = new CryptoStream(ms, alghoritm.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(value, 0, value.Length);
                cs.FlushFinalBlock();
                return ms.ToArray();
            }
            catch(Exception ex)
            {
                throw ExceptionHandling.GenerateException("Failed while executing Encrypt Binary operation", ex);
            }
            finally
            {
                cs.Close();
            }
        }

        private static byte[] Decrypt(byte[] encrypted, byte[] key, byte[] iv)
        {
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = null;
            try
            {
                Aes alghorithm = Aes.Create();
                alghorithm.Key = key;
                alghorithm.IV = iv;
                cs = new CryptoStream(ms, alghorithm.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(encrypted, 0, encrypted.Length);
                cs.FlushFinalBlock();
                return ms.ToArray();
            }
            catch(Exception ex)
            {
                throw ExceptionHandling.GenerateException("Failed while executing Decrypt Binary operation", ex);
            }
            finally
            {
                cs.Close();
            }
        }

        #endregion

        #region Private Class for Encryption Configuration
        private class EncryptionConfiguration
        {
            public static string Key
            {
                get { return "IslamIsMyLife"; }
            }

            public static byte[] InitializationVector
            {
                get 
                {
                    return new byte[] { 0x65, 0x64, 0x76, 0x65, 0x64, 0x03, 0x76, 0x45, 0xF1, 0x61, 0x6e, 0x20, 0x00 }; 
                }
            }
        }
        #endregion
    }
}
