using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Common
{
    public static class HttpRequestHelper
    {
        public static string SendPost(string urlPath, string baseUrl, string jsonBody)
        {
            try
            {
                var options = new RestClientOptions(urlPath)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest(baseUrl, Method.Post);
                request.AddHeader("Content-Type", "application/json");

                request.AddStringBody(jsonBody, DataFormat.Json);
                RestResponse response = client.Execute(request);
                return response.Content;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public static async Task<string> HttpClientSend(string baseUrl, string api_url,string jsonData)
        {
            try
            {
                //"api/UploadImage/SaveImage_Data"
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //GET Method
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(api_url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return string.Empty;
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
