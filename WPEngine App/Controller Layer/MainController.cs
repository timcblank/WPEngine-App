using Services;

namespace Controllers
{
    // Main Generic Controller. Can be used to call other controllers if need be. In this example it calls the File Processor Service
    class MainController : IMainController
    {
        private readonly ILogger _logger;
        private readonly IHelpMenuService _help;
        private readonly IProcessFileService _processor;

        public MainController(ILogger logger, IHelpMenuService help, IProcessFileService processor)
        {
            _logger = logger;
            _help = help;
            _processor = processor;
        }

        public void ProcessArguments(string[] args)
        {
            if (args.Length != 2)
            {
                _help.Run();
            }
            else
            {
                _processor.ProcessFile(args[0], args[1]);
            }
        }
    }
}
