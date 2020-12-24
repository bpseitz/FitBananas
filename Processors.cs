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
        public static async Task<Athlete> LoadAthleteInformation()
        {
            string url = $"https://www.strava.com/api/v3/athlete?access_token={AccessToken.current}";
            Console.WriteLine("Load Athlete info running");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Athlete result = await response.Content.ReadAsAsync<Athlete>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
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
            var listValues = new List<KeyValuePair<string,object>>();
            // listValues.Add({"client_id", ClientInfo.myClientId});

            // AuthorizationJson content = new AuthorizationJson(code);
            // string json = JsonConvert.SerializeObject(content);
            // var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            // Console.WriteLine(stringContent);
            // using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url, stringContent))
            var content = new FormUrlEncodedContent(postValues);
            Console.WriteLine("Authorization running");
            Console.WriteLine(content);
            using (var response = await client.PostAsync(url, content))
            {
                Console.WriteLine("Status Code: " + response.StatusCode);
                foreach(var header in response.Headers)
                {
                    foreach(var item in header.Value)
                    {
                        Console.WriteLine(header.Value);
                    }
                }
                AuthorizationModel result = await response.Content.ReadAsAsync<AuthorizationModel>();
                return result;
                // if (response.IsSuccessStatusCode)
                // {


                //     return result;
                // }
                // else
                // {
                //     throw new Exception(response.ReasonPhrase);
                // }
            }
        }
    }
}