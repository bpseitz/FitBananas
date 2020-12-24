namespace FitBananas
{
    public class AuthorizationJson
    {
        public string client_id { get; set; } = ClientInfo.myClientId;
        public string client_secret { get; set; } = ClientInfo.myClientSecret;
        public string code { get; set; }
        public string grant_type { get; set; }= "authorization_code";
        public AuthorizationJson(string _code)
        {
            code = _code;
        }
    }
}