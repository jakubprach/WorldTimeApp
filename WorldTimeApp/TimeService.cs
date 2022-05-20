using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace WorldTimeApp
{
    public class TimeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public TimeService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }



        public async Task<string> GetTime()
        {
            var httpClient = _httpClientFactory.CreateClient("WorldTime");

            var endpoint = _config.GetValue<string>(
                "Endpoint"
                );


            var response = await httpClient.GetAsync(endpoint);

            try
            {
                response.EnsureSuccessStatusCode();
            }

            catch (Exception ex)
            {
                return "error";
            }

            


            var content = await response.Content.ReadAsStringAsync();


            JObject data = JObject.Parse(content);


            string offset = (string)data["utc_offset"];
            int offsetInt = int.Parse(offset.Substring(1, 2));

            DateTime currentTime = DateTime.UtcNow.AddHours(offsetInt);

            return currentTime.ToString("HH:mm:ss");


        }
    }
}
