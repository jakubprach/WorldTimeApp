using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace WorldTimeApp
{
    public class TimezoneList
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TimezoneList(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }



        public async Task<List<string>> GetTimezones()
        {
            var httpClient = _httpClientFactory.CreateClient("WorldTime");

            var response = await httpClient.GetAsync("");


            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            string result =  content.Substring(1, content.Length - 2);

            List<string> data = result.Split(new char[] { ',' }).ToList();


            return data;


        }
    }
}
