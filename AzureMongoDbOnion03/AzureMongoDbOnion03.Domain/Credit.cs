using System;

namespace AzureMongoDbOnion03.Domain
{
   public class Credit
    {
        public string Id { get; set; }

        public string ForeignId { get; set; }

        public string Currency { get; set; }

        public int Amount { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool Active { get; set; } = true;

        public string Comment { get; set; }

        public override string ToString()
        {
            return Amount.ToString();
        }
    }
}
