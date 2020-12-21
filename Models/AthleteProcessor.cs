using System.Threading.Tasks;
using System.Net.Http;
using System;

namespace FitBananas.Models
{
    public class AthleteProcessor
    {
        public static async Task<Athlete> LoadAthleteInformation()
        {
            string url = "https://www.strava.com/api/v3/athlete?access_token=7deb301eecc10dcce1b2227652ab3dd246e0e0c3";

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
    }
}