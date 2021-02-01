

namespace FitBananas.Models
{
    public class AuthorizationModel
    {
        public int Expires_At { get; set; }
        public int Expires_In { get; set; }
        public string Refresh_Token { get; set; }
        public string Access_Token { get; set; }
        public Athlete Athlete { get; set; }
    }
}