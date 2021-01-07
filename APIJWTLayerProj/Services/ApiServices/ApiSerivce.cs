
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;

namespace APIJWTLayerProj.Services.ApiServices
{
    public class ApiSerivce
    {
        public string call(string token, string url, object param, RestSharp.Method method)
        {
            try
            {
                /*"http://localhost:56272/api/User/login"*/
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(method);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("Content-Type", "application/json");
                if (param != null)
                {
                    string jsonString = JsonSerializer.Serialize(param);
                    request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
                }
                IRestResponse response = client.Execute(request);
                //  var objResult = JsonSerializer.Deserialize<T>(response.Content);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);

                return response.Content;


                //var client = new RestClient("http://localhost:56272/api/Student/Get");
                //client.Timeout = -1;
                //var request = new RestRequest(Method.GET);
                //request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjpbIkFkbWluIiwiVXNlciJdLCJuYmYiOjE2MDgzOTgwNzMsImV4cCI6MTYwODQwMTY3MywiaWF0IjoxNjA4Mzk4MDczfQ.-6eedUuoWKcezyZRq7y2F-LeFHm2WsdHGAMxrQOlWjE");
                //IRestResponse response = client.Execute(request);
                //Console.WriteLine(response.Content);
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }

        }
    }
}

