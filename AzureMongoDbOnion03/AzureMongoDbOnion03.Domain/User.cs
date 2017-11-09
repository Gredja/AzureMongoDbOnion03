using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AzureMongoDbOnion03.Domain
{
    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "Enter e-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [PasswordPropertyText(true)]
        [Range(1, 10, ErrorMessage = "Please enter password between 1 and 10")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
