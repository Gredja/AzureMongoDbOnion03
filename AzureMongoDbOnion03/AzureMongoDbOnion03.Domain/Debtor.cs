
using System.ComponentModel.DataAnnotations;

namespace AzureMongoDbOnion03.Domain
{
    public class Debtor
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }
    }
}
