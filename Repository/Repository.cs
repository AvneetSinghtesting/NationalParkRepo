
using Microsoft.Extensions.Http;
using NationalParkFrontEnd.Repository.IRepository;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace NationalPark_Front.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public Repository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> CreateAsync(string url, T obj)
        {
            var request= new HttpRequestMessage(HttpMethod.Post, url);
            if(obj!=null)
            {
                request.Content=new StringContent(JsonConvert.SerializeObject(obj),Encoding.UTF8,"application/json");
            }
            var client= _httpClientFactory.CreateClient();
            HttpResponseMessage response= await client.SendAsync(request);
            if(response.StatusCode==System.Net.HttpStatusCode.OK)
                return true;
            return false;
        }

        public async Task<bool> DeleteAsync(string url, int Id)
        {
            var request= new HttpRequestMessage(HttpMethod.Delete, url+Id.ToString());
            var client=_httpClientFactory.CreateClient();
            HttpResponseMessage response=await client.SendAsync(request);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;
            return false;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string url)
        {
            var request= new HttpRequestMessage(HttpMethod.Get, url+ "GetNationalParks");
            var client= _httpClientFactory.CreateClient();
            HttpResponseMessage response= await client.SendAsync(request);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString=await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }
            return null;
        }

        public async Task<T> GetAsync(string url, int Id)
        {
            var request= new HttpRequestMessage(HttpMethod.Get, url+Id.ToString());
            var client=_httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK) 
            {
                var jsonString= await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            return null;
        }

        public async Task<bool> UpdateAsync(string url, T obj)
        {
            var request=new HttpRequestMessage(HttpMethod.Patch, url);
            if(obj!=null)
            {
                request.Content=new StringContent(JsonConvert.SerializeObject(obj),Encoding.UTF8,"application/json");
            }
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;
            return false;

        }
    }
}
