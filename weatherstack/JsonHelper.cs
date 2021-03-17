using Newtonsoft.Json.Linq;
using System;

namespace weatherstack
{
    interface IJsonHelper
    {
        T GetResultByNode<T>(JObject apiResult, string rootNode);
        T GetResultByNode<T>(JObject apiResult, string rootNode, string node);
        T GetResultByNode<T>(JObject apiResult, string rootNode, string node, string indexNode, int arrayIndex = 0);
    }
    public class JsonHelper : IJsonHelper
    {
        /// <summary>
        /// This function is available to get node value from API result.
        /// </summary>
        /// <param name="apiResult">Weather api result</param>
        /// <param name="rootNode">Parent node name</param>     
        /// <returns>generic type</returns>
        public T GetResultByNode<T>(JObject apiResult, string rootNode)
        {
            string nodeValue = (string)apiResult[rootNode];
            return (T)Convert.ChangeType(nodeValue, typeof(T));
        }

        /// <summary>
        /// This function is available to get node value from API result.
        /// </summary>
        /// <param name="apiResult">Weather api result</param>
        /// <param name="rootNode">Parent node name</param>
        /// <param name="node">Child node name</param>
        /// <returns>generic type</returns>
        public T GetResultByNode<T>(JObject apiResult, string rootNode, string node)
        {
            string nodeValue = (string)apiResult[rootNode][node];
            return (T)Convert.ChangeType(nodeValue, typeof(T));
        }

        /// <summary>
        /// This function is available to get node value from API result.
        /// </summary>
        /// <param name="apiResult">Weather api result</param>
        /// <param name="rootNode">Parent node name</param>
        /// <param name="node">Child node name</param>
        /// <param name="indexNode">Child array node name</param>
        /// <param name="arrayIndex">Child array node index</param>
        /// <returns>generic type</returns>
        public T GetResultByNode<T>(JObject apiResult, string rootNode, string node, string indexNode, int arrayIndex = 0)
        {
            string nodeValue = string.Empty;
            // if required inner node to execute
            if (apiResult[rootNode][node] is JArray)
                nodeValue = (string)apiResult[rootNode][node][arrayIndex][indexNode];
            else
                nodeValue = (string)apiResult[rootNode][node];
            return (T)Convert.ChangeType(nodeValue, typeof(T));
        }
    }
}
