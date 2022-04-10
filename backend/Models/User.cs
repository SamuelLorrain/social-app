using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class User
{
    [Key]
    public long Id { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Role { get; set; }
}
