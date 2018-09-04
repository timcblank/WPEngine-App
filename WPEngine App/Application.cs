using System;
using Controllers;
using Services;

namespace WPEngine_App
{
    class Application
    {
        protected readonly IMainController _mainController;
        private readonly ILogger _logger;
        private string[] _args;

        public Application(IMainController mainController, ILogger logger, string[] args)
        {
            _mainController = mainController;
            _logger = logger;
            _args = args;
        }

        public void Run()
        {
            _mainController.ProcessArguments(_args);
            _logger.Log("Please press enter to end the application");
            Console.ReadLine();
        }
    }
}
