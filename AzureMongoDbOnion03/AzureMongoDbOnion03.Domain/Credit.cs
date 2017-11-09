using System.ComponentModel.DataAnnotations;

namespace AzureMongoDbOnion03.Domain
{
   public class Credit
    {
        public string Id { get; set; }

        public string ForeignId { get; set; }

        public string Currency { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Amount { get; set; }

        public bool Active { get; set; } = true;

        public string Comment { get; set; }

        public override string ToString()
        {
            return Amount.ToString();
        }
    }
}
