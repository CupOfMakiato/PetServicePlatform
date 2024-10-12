using Server.Application.Enum;
using Server.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class ApplicationUser : BaseEntity
{
    public string FullName { get; set; }
    public string Email { get; set; }
    //public string? StripeAccountId { get; set; }
    public string? Password { get; set; }
    public double? Balance { get; set; }
    public string? RefreshToken { get; set; }
    public UserStatus? Status { get; set; }
    public int RoleCodeId { get; set; }
    public string? Otp {  get; set; }
    public bool IsVerified { get; set; } = false;
    public DateTime? OtpExpiryTime { get; set; }
    public string? VerificationToken { get; set; }
    public bool? IsStaff { get; set; }
    //public string? Type { get; set; }
    [ForeignKey("RoleCodeId")]
    public Role RoleCode { get; set; }
    //public ICollection<Message> SentMessages { get; set; }
    //public ICollection<Message> ReceivedMessages { get; set; }
    public ICollection<UserService> UserSerive { get; set; }
    public ICollection<Service> ServiceCreated { get; set; }
    //public virtual ICollection<Notification> Notifications { get; set; }
    public List<Payment> Payment { get; set; }
    public List<Transaction> Transaction { get; set; }
    public virtual ShopData? ShopData { get; set; }
    public string AvatarUrl { get; set; }
    public string? AvatarId { get; set; }
    public string? Introduction { get; set; }
}