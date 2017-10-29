using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Telkom.Pirsa.VPA.Setting
{
    public class IOManager
    {
        /// <summary>
        /// Creates a new configuration file on specified path. If the file exists opens the file instead.
        /// </summary>
        /// <param name="path">Target path string the file should be create</param>
        /// <returns>Stream writer to target file</returns>
        public static BinaryWriter CreateFile(string path)
        {
            try
            {
                return new BinaryWriter(new FileStream(path, FileMode.OpenOrCreate));
            }
            catch (Exception ex)
            {
                throw ExceptionHandling.GenerateException("Failed to create new file!", ex);
            }
        }

        /// <summary>
        /// Save and overwrite the content of target file with sepcific string content 
        /// </summary>
        /// <param name="path">Target file location. Creates new file if not exist</param>
        /// <param name="data">Content to write in target file</param>
        /// <returns>Boolean indicate save status of the file. True if success</returns>
        public static bool Save(string path, string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data))
                    throw new ArgumentException("Data to save is required!");
                using (BinaryWriter writer = CreateFile(path))
                {
                    var encrypted = Encryption.Encrypt(data);
                    writer.Write(encrypted);
                    writer.Flush();
                    writer.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ExceptionHandling.GenerateException("Failed to save data to file!", ex);
            }
        }

        /// <summary>
        /// Load target file contents as string
        /// </summary>
        /// <param name="path">Target file path</param>
        /// <returns>Contents of the file</returns>
        public static string Load(string path)
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(new FileStream(path, FileMode.Open)))
                {
                    var encrypted = reader.ReadString();
                    return Encryption.Decrypt(encrypted);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHandling.GenerateException("Failed to load data from file!", ex);
            }
        }
    }
}
