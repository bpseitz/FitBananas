using System.Threading.Tasks;
using System.Net.Http;
using System;
using FitBananas.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;

namespace FitBananas
{
    public class Processor
    {
        private static readonly HttpClient client = new HttpClient();
        
        public static async Task<AthleteStats> LoadAthleteStatsInfo(int athleteId, string accessToken)
        {
            string url = $"https://www.strava.com/api/v3/athletes/{athleteId}/stats?access_token={accessToken}";
            Console.WriteLine("Load Athlete Stats running");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    AthleteStats result = await response.Content.ReadAsAsync<AthleteStats>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public static async Task<AuthorizationModel> Authorization(string code)
        {
            string url = $"https://www.strava.com/oauth/token";
            var postValues = new Dictionary<string, string>{
                {"client_id",ClientInfo.myClientId},
                {"client_secret", ClientInfo.myClientSecret},
                {"code", code},
                {"grant_type", "authorization_code"}
            };
            
            var content = new FormUrlEncodedContent(postValues);
            Console.WriteLine("Authorization running");
            Console.WriteLine(content);
            using (var response = await client.PostAsync(url, content))
            {
                Console.WriteLine("Status Code: " + response.StatusCode);
                // build status code to meet our needs
                if (response.StatusCode.ToString() == "OK")
                {
                    AuthorizationModel result = await response.Content.ReadAsAsync<AuthorizationModel>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        
        public static async Task<AuthorizationModel> RefreshExpiredToken(string refreshToken)
        {
            string url = $"https://www.strava.com/oauth/token";
            var postValues = new Dictionary<string, string>{
                {"client_id",ClientInfo.myClientId},
                {"client_secret", ClientInfo.myClientSecret},
                {"grant_type", "refresh_token"},
                {"refresh_token", refreshToken}
            };
            
            var content = new FormUrlEncodedContent(postValues);
            Console.WriteLine("Refreshing Expired Token");
            Console.WriteLine(content);
            using (var response = await client.PostAsync(url, content))
            {
                Console.WriteLine("Status Code: " + response.StatusCode);
                // build status code to meet our needs
                if (response.StatusCode.ToString() == "OK")
                {
                    AuthorizationModel result = await response
                        .Content
                        .ReadAsAsync<AuthorizationModel>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async void Deauthorization(string accessToken)
        {
            string url = $"https://www.strava.com/oauth/deauthorize";
            var postValues = new Dictionary<string, string>{
                {"access_token", accessToken}
            };
            
            var content = new FormUrlEncodedContent(postValues);
            Console.WriteLine("Purge");
            Console.WriteLine(content);
            using (var response = await client.PostAsync(url, content))
            {
                Console.WriteLine("Status Code: " + response.StatusCode);
                // build status code to meet our needs
                if (response.StatusCode.ToString() == "OK")
                {
                    Console.WriteLine("Application access revoked on Strava");
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}