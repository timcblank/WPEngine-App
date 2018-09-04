using Data;
using Models;

namespace Services
{
    class ProcessFileService : IProcessFileService
    {
        private readonly ILogger _logger;
        private readonly IParseFileService _parser;
        private readonly ICreateFileService _output;
        private readonly IGetStatusService _dataService;

        private AcccountsModel accountList;

        public ProcessFileService(
            ILogger logger,
            IParseFileService parser,
            ICreateFileService output,
            IGetStatusService dataService)
        {
            _logger = logger;
            _parser = parser;
            _output = output;
            _dataService = dataService;
        }

        public void ProcessFile(string inputFile, string outputFile)
        {
            accountList = _parser.ParseFile(inputFile);
            if (accountList == null || accountList.Accounts == null)
            {
                _logger.Error("An error occurred parsing the accounts from the file");
            }
            else
            {
                _dataService.CheckStatuses(ref accountList);
                _output.CreateFile(accountList, outputFile);
            }
        }
    }
}
