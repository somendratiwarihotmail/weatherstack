using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace weatherstack
{
    interface INetworkHelper
    {
        Task<JObject> GetWeatherRecord(HttpClient client, string path);
    }
    public class NetworkHelper: INetworkHelper
    {
        /// <summary>
        /// This function is available for Weather API.
        /// </summary>
        /// <param name="client">A http client</param>
        /// <param name="path">Uri for Weather API</param>
        /// <returns>JObject result</returns>
        public async Task<JObject> GetWeatherRecord(HttpClient client, string path)
        {
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(json);
                return data;
            }
            return null;
        }
    }
}
