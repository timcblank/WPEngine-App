using Models;

namespace Services
{
    interface IParseFileService
    {
        AcccountsModel ParseFile(string inputFile);
    }
}
