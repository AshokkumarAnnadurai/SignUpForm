using System.ComponentModel.DataAnnotations;

namespace AngularApi.Models
{
    public class users
    {
        [Key]
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? username { get; set; }
        public string? Password { get; set; }

    }
}
