using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Azure;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ServeAPI.Paypal
{
	public class PaypalService
	{
        private readonly HttpClient _httpClient;
        private string token = "";
        private string clientKey = "AUVNDss4XX4asKue6ZJvdBG0K1Vd3Fo4qs_60kjlBq3BwSqUKPgFkrgTzfn68fYGBse-b-FP4AWYrgqj";
        private string Secret = "ECWaVuHX31vT-cR8MTje_tyIqVPOWqgcT9sPFiJWLuef89-q4rm2i3IBbO6ftDZLt6Cwjxg_weXS7iau";



        public PaypalService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://api-m.sandbox.paypal.com/");

            // using Microsoft.Net.Http.Headers;
            // The GitHub API requires two headers.
            
            //_httpClient.DefaultRequestHeaders.Add(
            //    HeaderNames.ContentType, "application/x-www-form-urlencoded");

            var textBytes = System.Text.Encoding.UTF8.GetBytes(clientKey + ":" + Secret);
            string code = System.Convert.ToBase64String(textBytes);
            _httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Authorization, "Basic "+ code);
        }

         

        public async Task<PaypalOrder?> CheckOrder(string orderid) {
            if (token == "") {
                var body = new StringContent(
                "grant_type=client_credentials",
                Encoding.UTF8,
                Application.Json);
                HttpResponseMessage responseMessage = await _httpClient.PostAsync("/v1/oauth2/token", body);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string responseBody = await responseMessage.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        IncludeFields = true,
                    };
                    var paypalToken = JsonSerializer.Deserialize<PaypalToken>(responseBody, options)!;
                    token = paypalToken.access_token;
                    _httpClient.DefaultRequestHeaders.Remove(HeaderNames.Authorization);
                    _httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + token);
                }
            }
            return await _httpClient.GetFromJsonAsync<PaypalOrder>("/v2/checkout/orders/" + orderid);
            //if (token == "") {
            //var paypalToken = await _httpClient.PostAsync("/v1/oauth2/token",null);
            //paypalToken.Content
            //token = paypalToken!.access_token;
            //_httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + token);
            //}

            //return await _httpClient.GetFromJsonAsync<PaypalOrder>("/v2/checkout/orders/"+orderid);
        }
    }
}

