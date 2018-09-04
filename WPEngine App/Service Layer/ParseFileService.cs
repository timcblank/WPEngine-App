using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Models;
using Microsoft.VisualBasic.FileIO;
using System.Reflection;

namespace Services
{
    class ParseFileService : IParseFileService
    {
        private readonly ILogger _logger;

        public ParseFileService(ILogger logger)
        {
            _logger = logger;
        }

        public AcccountsModel ParseFile(string inputFile)
        {
            AcccountsModel list = new AcccountsModel();
            bool possiblePath = inputFile.IndexOfAny(Path.GetInvalidPathChars()) == -1;
 
            if (possiblePath)
            {
                try
                {
                    using (TextFieldParser parser = new TextFieldParser(inputFile))
                    {
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(",");
                        parser.TrimWhiteSpace = true;
                        string[] header;

                        AccountItemModel item = new AccountItemModel();

                        // File should have a header. Header should match up to the Model object properties. Validate the header first. Remove whitespace
                        if (parser.PeekChars(1) != null)
                        {
                            var cleanFields = parser.ReadFields().Select(f => f.Trim(new[] { ' ', '"' }).Replace(" ", string.Empty)).ToArray();
                            header = cleanFields;
                            if (header.Length > 0)
                            {
                                IList<PropertyInfo> props = new List<PropertyInfo>(item.GetType().GetProperties());

                                var propNames = string.Join(",", props.Select(x => x.Name));

                                bool allMatches = true;

                                // make sure all the header items matchs our object property names for mapping
                                foreach(var headerItem in header)
                                {
                                    if (propNames.IndexOf(headerItem) == -1)
                                    {
                                        allMatches = false;
                                    }
                                }

                                // if all matches we'll parse the rest of the CSV file and map the data to the object
                                if (allMatches)
                                {
                                    list.Accounts = new List<AccountItemModel>();
                                    while (parser.PeekChars(1) != null)
                                    {
                                        item = new AccountItemModel();
                                        cleanFields = parser.ReadFields().Select(f => f.Trim(new[] { ' ', '"' })).ToArray();
                                        
                                        for (var i = 0; i < header.Length; i++)
                                        {
                                            item = (AccountItemModel)SetPropertyValue((Object)item, header[i], cleanFields[i]);
                                        }
                                        list.Accounts.Add(item);
                                    }
                                }
                                else
                                {
                                    _logger.Error("An incorrect header value was found in the CSV file.");
                                }
                            }
                            else
                            {
                                _logger.Error("header row missing correct values");
                            }
                        }
                        else
                        {
                            _logger.Error("No rows found in file");
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.Error(e.Message);
                }
            }

            return list;
        }

        private Object SetPropertyValue(Object obj, string propName, string value)
        {
            var prop = obj.GetType().GetProperty(propName);

            switch(Type.GetTypeCode(prop.PropertyType))
            {
                case TypeCode.Boolean:
                case TypeCode.Byte:
                    prop.SetValue(obj, bool.Parse(value));
                    break;
                case TypeCode.DateTime:
                    prop.SetValue(obj, DateTime.Parse(value));
                    break;
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    prop.SetValue(obj, int.Parse(value));
                    break;
                case TypeCode.Decimal:
                case TypeCode.Double:
                    prop.SetValue(obj, float.Parse(value));
                    break;
                default:
                    prop.SetValue(obj, value);
                    break;
            }
            return obj;
        }
    }
}
