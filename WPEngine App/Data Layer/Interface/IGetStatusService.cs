using Models;

namespace Data
{
    interface IGetStatusService
    {
        void CheckStatuses(ref AcccountsModel accountsList);
    }
}
