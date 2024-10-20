using EmployeesMyPlugin.Entities.JsonRequest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeesMyPlugin.Infrastructure.ExternalApiQuery
{
    internal class ExternalApiService : IExternalApiQuery
    {
        private readonly HttpClient _httpClient;
        public ExternalApiService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<IEnumerable<User>> GetUsers(params int[] userNumbers)
        {
            var users = new List<User>();
            foreach (var user in userNumbers)
            {
                using (var request = new HttpRequestMessage())
                {
                    var uriBuilder = new UriBuilder(Settings.ApiUrl + "/" + user);
                    request.RequestUri = uriBuilder.Uri;

                    request.Method = HttpMethod.Get;

                    var response = await _httpClient.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("request for remote resource failed");
                    }

                    try
                    {
                        var obj = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<User>(obj) ?? new User();
                        users.Add(result);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return users;
        }
    }
}
