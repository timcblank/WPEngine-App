using Models;

namespace Services
{
    interface ICreateFileService
    {
        void CreateFile(AcccountsModel accountList, string outputFile);
    }
}
