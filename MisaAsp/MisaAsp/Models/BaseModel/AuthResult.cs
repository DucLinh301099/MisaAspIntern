namespace MisaAsp.Models.BaseModel
{
    public class AuthResult
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public int UserId { get; internal set; }
    }

}
