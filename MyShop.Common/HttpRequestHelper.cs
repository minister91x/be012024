using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Common
{
    public static class HttpRequestHelper
    {
        public static string SendPost(string urlPath,string baseUrl, string jsonBody)
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
                RestResponse response =  client.Execute(request);
                return response.Content;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
