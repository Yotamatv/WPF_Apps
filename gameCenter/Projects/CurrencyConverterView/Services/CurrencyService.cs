using gameCenter.Projects.CurrencyConverter.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;


namespace gameCenter.Projects.CurrencyConverter.Services
{
    class CurrencyService
    {
        private const string BaseApiEndPoint = "http://api.exchangeratesapi.io/v1/latest";
        private HttpClient Http_Client = new HttpClient();

        //Tasks are asynchronous functions that return a value, 
        //the syntax is async Task<var type>
        public async Task<Dictionary<string, double>> GetExchangeRateAsync()
        {
            var config = new ConfigurationBuilder().AddUserSecrets<CurrencyService>().Build();
            string requestUrl = $"{BaseApiEndPoint}?access_key={config["apiKey"]}"; // <- Replace with your own exchangeratesapi key!
            string response = await Http_Client.GetStringAsync(requestUrl); //Http_Client =object we made on line 18
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Models.ExchangeResponse exchangeData = JsonSerializer.Deserialize<Models.ExchangeResponse>(response, options);
            //in our exchange response object, properties are all properly capitalized. in the response, they're all lower.
            //this makes our request match our response.
            if (exchangeData == null || exchangeData.Rates == null)
                throw new InvalidOperationException("Failed to fetch exchange rates");
            return exchangeData.Rates;
        }
    }
}
