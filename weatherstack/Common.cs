using Newtonsoft.Json.Linq;
using System;

namespace weatherstack
{
    interface ICommon
    {
        bool CheckForHumidity(JObject apiResult);
        bool CheckForUVIndex(JObject apiResult);
        bool CheckForWindSpeed(JObject apiResult, bool isRainyDay);
        void DisplayResult(string apiResult);
    }
    public class Common : ICommon
    {
        private readonly JsonHelper jsonHelper;
        public Common()
        {
            jsonHelper = new JsonHelper();
        }

        /// <summary>
        /// This function is available for Humidity Check.
        /// </summary>
        /// <param name="apiResult">Weather api result</param>
        /// <returns>bool</returns>
        public bool CheckForHumidity(JObject apiResult)
        {
            int humidityLevel;
            string nodeValue = jsonHelper.GetResultByNode<string>(apiResult, "current", "humidity");

            bool isRainyDay = Int32.TryParse(nodeValue, out humidityLevel) && humidityLevel >= 100;
            if (isRainyDay)
            {
                Console.WriteLine("No if it’s raining and humidity level " + humidityLevel);
            }
            else
            {
                Console.WriteLine("Yes if it’s not raining and humidity level " + humidityLevel);
            }
            return isRainyDay;
        }

        /// <summary>
        /// This function is available for UV index Check.
        /// </summary>
        /// <param name="apiResult">Weather api result</param>
        /// <returns>bool</returns>
        public bool CheckForUVIndex(JObject apiResult)
        {
            int uvIndex;
            string nodeValue = jsonHelper.GetResultByNode<string>(apiResult, "current", "uv_index");
            bool isSunScreenRequired = Int32.TryParse(nodeValue, out uvIndex) && uvIndex > 3;
            if (isSunScreenRequired)
            {
                Console.WriteLine("Yes, sunscreen is required and uvIndex is" + uvIndex);
            }
            else
            {
                Console.WriteLine("No, sunscreen is not required and uvIndex is " + uvIndex);
            }
            return isSunScreenRequired;
        }

        /// <summary>
        /// This function is available for Wind Speed Check.
        /// </summary>
        /// <param name="apiResult">Weather api result</param>
        /// <returns>bool</returns>
        public bool CheckForWindSpeed(JObject apiResult, bool isRainyDay)
        {
            int windSpeed;
            string nodeValue = jsonHelper.GetResultByNode<string>(apiResult, "current", "wind_speed");
            bool canFlyKite = Int32.TryParse(nodeValue, out windSpeed) && windSpeed > 15;

            if (!isRainyDay && canFlyKite)
            {
                Console.WriteLine("Yes, You can fly kite and wind Speed is " + windSpeed);
            }
            else
            {
                Console.WriteLine("No, You cannot fly kite and wind Speed is " + windSpeed);
            }
            return !isRainyDay && canFlyKite;
        }

        /// <summary>
        /// This function is available display api result.
        /// </summary>
        /// <param name="apiResult">Weather api result</param>
        /// <returns>void</returns>
        public void DisplayResult(string apiResult)
        {
            Console.WriteLine(apiResult);
        }
    }
}
