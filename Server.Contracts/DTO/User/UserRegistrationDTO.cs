using System.ComponentModel.DataAnnotations;

namespace Server.Contracts.DTO.User;
public class UserRegistrationDTO
{
    [Required]
    public string FullName { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }
    [Required]
    [PasswordValidation]
    public string Password { get; set; }
    public string Introduction { get; set; }
}


