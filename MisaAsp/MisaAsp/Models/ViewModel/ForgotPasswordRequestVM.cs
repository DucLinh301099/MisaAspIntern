using System.ComponentModel.DataAnnotations;

public class ForgotPasswordRequestVM
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
