using System.ComponentModel.DataAnnotations;

namespace AzureMongoDbOnion03.Domain
{
    public class User
    {
        public string Id { get; set; }

        [Required]
        public string ForeignId { get; set; }

        [Required(ErrorMessage = "Enter e-mail")]
        [EmailAddress]
        public string Email { get; set; }

       // [Range(1, 10, ErrorMessage = "Please enter password between 1 and 10")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
