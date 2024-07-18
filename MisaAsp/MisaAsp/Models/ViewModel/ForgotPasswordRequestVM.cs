using MisaAsp.Attribute;
using System.ComponentModel.DataAnnotations;

public class ForgotPasswordRequestVM
{
    [RequiredVI]
    [EmailAddress]
    [StringLengthVI(100)]
    public string Email { get; set; }
}
