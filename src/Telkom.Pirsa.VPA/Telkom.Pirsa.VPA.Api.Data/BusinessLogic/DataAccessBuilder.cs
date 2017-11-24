using Telkom.Pirsa.VPA.Api.Data.Core;
using Telkom.Pirsa.VPA.Api.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telkom.Pirsa.VPA.Api.Data.BusinessLogic
{
    public class DataAccessBuilder
    {
        private DatabaseSeeder _seeder;
        private IConnectionManager _seedConnection;
        private IConnectionManager _appConnection;
        private bool _forceSeed;

        public DataAccessBuilder(bool forceSeed = false)
        {
            _forceSeed = forceSeed;
        }

        public void Build()
        {
            _seedConnection = new SQLiteConnectionManager(true);
            _appConnection = new SQLiteConnectionManager();
            _seeder = new DatabaseSeeder(_seedConnection);
        }

        public void Destroy()
        {
            _seedConnection.Disconnect();
            _appConnection.Disconnect();
        }

        #region Components
        public DatabaseSeeder Seeder
        {
            get { return _seeder; }
        }

        public IConnectionManager Connection
        {
            get { return _appConnection; }
        }

        public bool IsRequireSeeding
        {
            get
            {
                if (_forceSeed)
                    return true;

                return !System.IO.File.Exists(ConfigurationManager.AppSettings["DataSource"]);
            }
        }
        #endregion
    }
}
