namespace AzureMongoDbOnion03.Helpers
{
   public class Role
    {
        public string Name { get; set; }
        public Role(Roles name)
        {
            Name = name.ToString();
        }
    }
}
