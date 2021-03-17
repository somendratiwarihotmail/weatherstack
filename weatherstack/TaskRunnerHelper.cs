using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace weatherstack
{
    public class TaskRunnerHelper
    {
        private readonly HttpClient client;//= new HttpClient();     
        private readonly Common common;
        private readonly JsonHelper jsonHelper;
        private readonly NetworkHelper networkHelper;
        
        public TaskRunnerHelper()
        {
            client = new HttpClient();
            common = new Common();
            jsonHelper = new JsonHelper();
            networkHelper = new NetworkHelper();
        }

        public async Task RunAsync()
        {
            var uriPath = "http://api.weatherstack.com/current?access_key=610acf4c1d203448cd6f671955c5e8aa&query={0}";
            client.BaseAddress = new Uri(uriPath);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                //We required to gether zipcode from user
                //Also zipcode always be a numeric value
                string zipcode;
                int zipcodeNumeric;
                bool inValidZipcode = false;
                do
                {
                    Console.WriteLine("Please enter a valid zipcode");
                    zipcode = Console.ReadLine();
                    inValidZipcode = !string.IsNullOrEmpty(zipcode) && Int32.TryParse(zipcode, out zipcodeNumeric);
                } while (!inValidZipcode);

                uriPath = string.Format(uriPath, zipcode);
                JObject apiResult = await networkHelper.GetWeatherRecord(client, uriPath);

                common.DisplayResult(Convert.ToString(apiResult));

                string nodeValue = jsonHelper.GetResultByNode<string>(apiResult, "success");
                bool isRecordAvailable = string.IsNullOrEmpty(nodeValue);

                if (apiResult != null)
                {
                    if (!isRecordAvailable)
                    {
                        Console.WriteLine("Unable to fetch record for user zipcode");
                    }
                    else
                    {
                        Console.WriteLine("Should I go outside?");
                        bool isRainyDay = common.CheckForHumidity(apiResult);

                        Console.WriteLine("Should I wear sunscreen?");
                        bool isSunScreenRequired = common.CheckForUVIndex(apiResult);

                        Console.WriteLine("Can I fly my kite?");
                        bool canFlyKite = common.CheckForWindSpeed(apiResult, isRainyDay);
                    }
                }
                else
                {
                    Console.WriteLine("Unable to connect to Weather API server");
                }
            }
            catch (Exception e)
            {
                //We cannot share the error message due to security
                //Console.WriteLine(e.Message);
                Console.WriteLine("Sorry. Iinternal server error occurred.");
            }
        }
    }
}
