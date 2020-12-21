using System.Threading.Tasks;
using System.Net.Http;
using System;

namespace FitBananas.Models
{
    public class AthleteStatsProcessor
    {
        //takes in athleteId from current athlete object
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
    }
}