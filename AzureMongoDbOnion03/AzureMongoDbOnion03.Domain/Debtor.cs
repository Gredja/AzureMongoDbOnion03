
using System.ComponentModel.DataAnnotations;

namespace AzureMongoDbOnion03.Domain
{
    public class Debtor
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
