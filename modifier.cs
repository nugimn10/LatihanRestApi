using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LathianRestApi
{
    public class modifier
    {
        
        public static HttpClient client = new HttpClient();
        public static string db = "http://localhost:3000/StuffToDo";

        public static async Task<string> Get(string link)
        {
            return await client.GetStringAsync(link);
        }

        public static async void Delete(string link)
        {
            await client.DeleteAsync(link);
        }

        public static async 
        Task
        Post(string link, HttpContent data)
        {
            await client.PostAsync(link, data);
        }

        public static async void Put(string link, HttpContent data)
        {
            await client.PutAsync(link, data);
        }

        public static async void Patch(string link, HttpContent data)
        {
            await client.PatchAsync(link, data);
        }
    }
}