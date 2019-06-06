using System.ComponentModel.DataAnnotations;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels
{
    public class DbAdmin : CoreDbEntity
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Disabled { get; set; }
    }
}
