using System;
using System.Text;
using System.Net;
using System.Collections.Generic;
using Models;
using System.Configuration;
using Newtonsoft.Json;
using Services;

namespace Data
{
    class GetStatusService : IGetStatusService
    {
        private readonly ILogger _logger;

        public GetStatusService(ILogger logger)
        {
            _logger = logger;
        }

        // Calls the web api to get the status of a given account if found. Sets the status in the account list.
        public void CheckStatuses(ref AcccountsModel accountsList)
        {
            string apiUrl = ConfigurationManager.AppSettings["WebApiUrl"];
            if (apiUrl != null && apiUrl != string.Empty)
            {
                var client = new WebClient();
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;

                try
                {
                    for (var i = 0; i < accountsList.Accounts.Count; i++)
                    {
                        var query = apiUrl + "accounts/" + accountsList.Accounts[i].AccountID.ToString();
                        string json = client.DownloadString(query);
                        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                        accountsList.Accounts[i].Status = dictionary["status"];
                        accountsList.Accounts[i].StatusSetOn = dictionary["created_on"];
                    }
                }
                catch(Exception e)
                {
                    _logger.Error(e.Message);
                }

            }
        }
    }
}
