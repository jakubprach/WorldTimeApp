using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace WorldTimeApp
{
    public class TimezoneList
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        // Constructor
        public TimezoneList(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }



        public async Task<List<string>> GetTime()
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
