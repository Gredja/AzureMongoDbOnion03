using System.Collections.Generic;
using AzureMongoDbOnion03.Domain;

namespace AzureMongoDbOnion03.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Debtor> Debtors { get; set; }
        public IEnumerable<Credit> Credits { get; set; }
        public IEnumerable<MoneyPlusDebtorName> CreditPlusDebtorNames { get; set; }
        public Credit NewCredit { get; set; }
        public string SelectedCurrency { get; set; } = "All";
    }
}
