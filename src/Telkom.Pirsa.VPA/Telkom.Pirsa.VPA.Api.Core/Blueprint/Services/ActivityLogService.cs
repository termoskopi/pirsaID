using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telkom.Pirsa.VPA.Api.Data.BusinessLogic;
using Telkom.Pirsa.VPA.Api.Data.BusinessModel;
using Telkom.Pirsa.VPA.Api.Data.Core;

namespace Telkom.Pirsa.VPA.Api.Core.Blueprint.Services
{
    public class ActivityLogService
    {
        private readonly DataAccessManager _manager;
        private readonly IRepository _historyRepository;

        public ActivityLogService()
        {
            throw new Exception("This default constructor has been disbaled. All Service instance only generated on ApplicationServiceBuilder");
        }

        public ActivityLogService(DataAccessManager manager)
        {
            _manager = manager;
            _historyRepository = new HistoryRepository(_manager.ConnectionManager);
        }

        public JArray LoadHistory(int limit = int.MaxValue)
        {
            try
            {
                var results = _historyRepository.Get();
                var fetchLimit = Math.Min(limit, results.Count);
                JArray resultArray = new JArray();
                foreach (var item in results.Take(fetchLimit))
                {
                    resultArray.Add(JObject.Parse(item.Json));
                }

                return resultArray;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool LogActivity(string activity, string source)
        {
            try
            {
                History model = new History();
                model.ActionDate = DateTime.Now;
                model.Activity = activity;
                model.Source = source;

                return _historyRepository.Create(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LogRequestActivity(string username, string endpoint)
        {
            try
            {
              LogSystemActivity(string.Format("{0} accessed endpoint {1}", username, endpoint));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LogSystemActivity(string activity)
        {
          try
          {
            string filename = string.Format("ServerLog-{0:yyyyMMdd}.txt", DateTime.Now);
            if (!File.Exists(filename))
            {
              using (var writer = File.CreateText(filename))
              {
                writer.WriteLine("Server Log File created on {0:dd MMMM yyyy}", DateTime.Now);
                writer.Flush();
                writer.Close();
              }
            }

            using (var stream = File.Open(filename, FileMode.Append))
            {
              using (var writer = new StreamWriter(stream))
              {
                writer.WriteLine("[{0:HH:mm:ss}] {1}", DateTime.Now, activity);
                writer.Flush();
                stream.Flush();
                writer.Close();
                stream.Close();
              }
            }

          }
          catch (Exception ex)
          {
            throw ex;
          }
        }
    }
}
