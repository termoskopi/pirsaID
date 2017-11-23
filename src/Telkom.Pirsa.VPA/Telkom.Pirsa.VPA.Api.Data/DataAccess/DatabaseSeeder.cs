using Research.Web.Nancy.Application.Data.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Research.Web.Nancy.Application.Data.DataAccess
{
    public class DatabaseSeeder
    {
        private IConnectionManager _connection;
        private string _sql;

        public DatabaseSeeder(IConnectionManager connection)
        {
            _connection = connection;
        }

        public void ReadScript()
        { 
            try
            {
              string path = ConfigurationManager.AppSettings["ScriptLocation"] + Path.DirectorySeparatorChar + "Seeder.sql";
              
              if (!File.Exists(path))
                throw new Exception("The specified file not found!");

              string batch = File.ReadAllText(path);
              string[] commands = batch.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

              foreach (string cmd in commands)
              {
                _sql += string.Format("{0};#", cmd.Trim());
              }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public bool SeedDatabase()
        {
          try
          {
            string[] commands = _sql.Split(new [] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            if (!_connection.IsOpen)
            {
              _connection.Connect();
            }
            _connection.Execute(commands);

            _connection.Disconnect();

            return true;
          }
          catch (Exception ex)
          {
            _connection.Disconnect();
            throw ex;
          }
        }

        public string Sql
        {
          get { return _sql; }
        }
    }
}
