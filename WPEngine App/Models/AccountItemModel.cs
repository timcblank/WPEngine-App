using System.Collections.Generic;

namespace Models
{
    public class AccountItemModel
    {
        public int AccountID { get; set; }
        public string AccountName { get; set; }
        public string FirstName { get; set; }
        public string CreatedOn { get; set; }
        public string Status { get; set; }
        public string StatusSetOn { get; set; }
    }

    public class AcccountsModel
    {
        public IList<AccountItemModel> Accounts;
    }
}
