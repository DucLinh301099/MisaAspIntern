using System.ComponentModel.DataAnnotations;

public class ForgotPasswordRequestVM
{
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }
}
