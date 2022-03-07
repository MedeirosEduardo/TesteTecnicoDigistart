using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Net;
using TesteTecnicoDigiStart.API.Controllers;
using TesteTecnicoDigiStart.Interface;

namespace TesteTecnicoDigiStart.API
{
    public class RequestHelper : AppBaseController, IRequestHelper
    {
        public RequestHelper(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        public string PostT(string url, string body, string token)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "bearer " + token);
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            try
            {
                var response = client.Execute(request);

                var responseFromServer = response.Content.ToString();

                return responseFromServer;
            }
            catch
            {
                return "There was one error on the request.";
            }
        }

        public string GetT(string url, string token)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", token);

            try
            {
                var response = client.Execute(request);

                var responseFromServer = response.Content.ToString();

                return responseFromServer;
            }
            catch
            {
                return "There was one error on the request.";
            }
        }
    }
}
