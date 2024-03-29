﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Models;
using System.Reflection;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Services
{
    class CreateFileService : ICreateFileService
    {
        private readonly ILogger _logger;

        public CreateFileService(ILogger logger)
        {
            _logger = logger;
        }

        public void CreateFile(AcccountsModel accountList, string outputFile)
        {
            try
            {
                if (accountList.Accounts.Count > 0)
                {
                    // get the list of property names from the AccountItemModel object. These will be the header row in the new CSV file
                    IList<PropertyInfo> props = new List<PropertyInfo>(accountList.Accounts[0].GetType().GetProperties());
                    var propNames = string.Join(",", props.Select(x => Regex.Replace(x.Name, "([A-Z])([A-Z])([a-z])|([a-z])([A-Z])", "$1$4 $2$3$5").Trim()));

                    var path = File.Create(outputFile);

                    using (var writer = new StreamWriter(path))
                    {
                        writer.WriteLine(propNames);

                        // loop through the account list for each item
                        // we just need the values so it is simple enough to serialize the object to json
                        // then deserialize to a dictionary to get the values in a string array to write out
                        foreach (var item in accountList.Accounts)
                        {
                            var serialized = JsonConvert.SerializeObject(item);
                            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(serialized);

                            var outputLine = string.Join(",", dictionary.Values);
                            writer.WriteLine(outputLine);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
        }
    }
}
