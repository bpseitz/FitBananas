using System;
using System.Net.Http;
using System.Net.Http.Headers;


namespace FitBananas.Models
{
    public class ApiHelper
    {
        public static HttpClient ApiClient { get; set; } = new HttpClient();

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            // ApiClient.BaseAddress = new Uri("http://xkcd.com/");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}