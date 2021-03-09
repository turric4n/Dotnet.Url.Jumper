using Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.SQLDatamodels
{
    [Table("Admins")]
    public class Admins : Core
    {
        public Admins() : base()
        {
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Disabled { get; set; }
    }
}
