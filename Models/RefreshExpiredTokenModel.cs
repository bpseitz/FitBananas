namespace FitBananas.Models
{
    public class RefreshExpiredTokenModel
    {
        public int Expires_At { get; set; }
        public int Expires_In { get; set; }
        public string Refresh_Token { get; set; }
        public string Access_Token { get; set; }
    }
}