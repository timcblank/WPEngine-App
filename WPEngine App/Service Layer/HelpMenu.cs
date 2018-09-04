using System;

namespace Services
{
    class HelpMenuService : IHelpMenuService
    {
        private readonly ILogger _logger;

        public HelpMenuService(ILogger logger)
        {
            _logger = logger;
        }

        // This can be expaneded for other help options in the future.
        public void Run()
        {
            _logger.Log("Two commmand line arguments are required.");
            _logger.Log("1) The name of the CSV input account file to process");
            _logger.Log("2) The name of the output CSV file to be created after processing");
            Console.ReadLine();
        }
    }
}
