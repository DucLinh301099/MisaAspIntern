using MisaAsp.Attribute;
using System.ComponentModel.DataAnnotations;
using MisaAsp.Attribute.ModelAttribute;
public class ForgotPasswordRequestVM
{
    [RequiredVI]
    [EmailAddress]
    [StringLengthVI(50)]
    public string Email { get; set; }
}
