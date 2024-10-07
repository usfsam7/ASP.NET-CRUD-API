using System.ComponentModel.DataAnnotations;

namespace crud.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required] public string Username { get; set; } = string.Empty;

        public DateTime CreatedTimestamp { get; set; }

    }
}
