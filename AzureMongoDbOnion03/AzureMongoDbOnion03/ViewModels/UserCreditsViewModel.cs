using System.Collections.Generic;
using AzureMongoDbOnion03.Domain;

namespace AzureMongoDbOnion03.ViewModels
{
    public class UserCreditsViewModel
    {
       public IEnumerable<Credit> Credits { get; set; }
    }
}
