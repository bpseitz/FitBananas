using System.Threading.Tasks;
using System.Net.Http;
using System;
using FitBananas.Models;

namespace FitBananas
{
    public class Processor
    {
        public static async Task<Athlete> LoadAthleteInformation()
        {
            string url = $"https://www.strava.com/api/v3/athlete?access_token={AccessToken.current}";

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
        public static async Task<AthleteStats> LoadAthleteStatsInfo(int athleteId)
        {
            string url = $"https://www.strava.com/api/v3/athletes/{athleteId}/stats?access_token={AccessToken.current}";

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
            var stringContent = new StringContent($"?client_id={ClientInfo.myClientId}&client_secret={ClientInfo.myClientSecret}&code=AUTHORIZATIONCODE&grant_type={code}");
            using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync(url,stringContent))
            {
                if (response.IsSuccessStatusCode)
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
    }
}