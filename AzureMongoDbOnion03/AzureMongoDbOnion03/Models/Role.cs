namespace AzureMongoDbOnion03.Models
{
   public class Role
    {
        public Roles Name { get; set; }
        public Role(Roles name)
        {
            Name = name;
        }
    }
}
