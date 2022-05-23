using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaAPI.Entities
{
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? Username { get; set; }

        [Column(TypeName = "varchar(255)")]

        public byte[]? PasswordHash { get; set; }

        [Column(TypeName = "varchar(255)")]
        public byte[]? PasswordSalt { get; set; }

        [Column(TypeName = "varchar(255)")]
        [EmailAddress]
        public string? Email { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? Role { get; set; }

        [Column(TypeName = "varchar(1024)")]
        public string? Token { get; set; }

        public bool isConfirmed { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? ConfirmationKey { get; set; }
    }
}
