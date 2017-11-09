using System.Collections.Generic;
using AzureMongoDbOnion03.Domain;

namespace AzureMongoDbOnion03.ViewModels
{
    public class UsersViewModel
    {
        public IEnumerable<User> Users { get; set; }

        public IEnumerable<Debtor> Debtors { get; set; }

        public User NewUser { get; set; }

        public string SelectedDebtorId { get; set; }
    }
}
