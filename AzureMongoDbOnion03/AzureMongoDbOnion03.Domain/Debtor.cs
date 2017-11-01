namespace AzureMongoDbOnion03.Domain
{
    public class Debtor
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
