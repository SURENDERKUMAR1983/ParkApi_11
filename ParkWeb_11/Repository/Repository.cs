using Newtonsoft.Json;
using ParkWeb_11.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParkWeb_11.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    { private readonly IHttpClientFactory _IHttpClientFactory;
        public Repository(IHttpClientFactory httpClientFactory)
        {
            _IHttpClientFactory = httpClientFactory;
        }
        public async Task<bool> CreateAsync(string Url, T ObjToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Url);
            if( ObjToCreate != null)
            {
                request.Content = new StringContent(JsonConvert.
                    SerializeObject(ObjToCreate), Encoding. UTF8, "application/Json");
            }
            var client = _IHttpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteAsync(string Url, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, Url + id.ToString());
            var Client = _IHttpClientFactory.CreateClient();
            HttpResponseMessage response = await Client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            return true;
            else
            return false;
        }       
                
        public async Task<T> GetAsync(string Url, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Url + id.ToString());
            var Client = _IHttpClientFactory.CreateClient();
            HttpResponseMessage response = await Client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            return null;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string Url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,Url);
            var Client = _IHttpClientFactory.CreateClient();
            HttpResponseMessage response = await Client.SendAsync(request);
           
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<T>> (jsonString);
                }
            return null;
        }

        public async Task<bool> UpdateAsync(string Url, T ObjToUpdate )
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Url);
            if (ObjToUpdate != null)
            {
                request.Content = new StringContent(JsonConvert.
                    SerializeObject(ObjToUpdate), Encoding.UTF8, "application/Json");
            }
            var client = _IHttpClientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return true;
            else
            return false;
        }        
    }
}
