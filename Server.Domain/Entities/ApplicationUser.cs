using Server.Application.Enum;
using Server.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class ApplicationUser : BaseEntity
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string? Password { get; set; }
    public double? Balance { get; set; }
    public string? RefreshToken { get; set; }
    public UserStatus? Status { get; set; }
    public int RoleCodeId { get; set; }
    public string? Otp {  get; set; }
    public bool IsVerified { get; set; } = false;
    public DateTime? OtpExpiryTime { get; set; }
    public string? VerificationToken { get; set; }
    public string? ResetToken { get; set; }
    public DateTime? ResetTokenExpiry { get; set; }
    public bool? IsStaff { get; set; }
    [ForeignKey("RoleCodeId")]
    public Role RoleCode { get; set; }
    public ICollection<UserService> UserSerive { get; set; }
    public List<Payment> Payment { get; set; }
    public List<Transaction> Transaction { get; set; }
    public virtual ShopData? ShopData { get; set; }
    public string PhoneNumber { get; set; }
}