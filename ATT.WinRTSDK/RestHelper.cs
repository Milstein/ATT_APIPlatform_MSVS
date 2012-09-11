namespace ATT.MetroSDK
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class RestHelper
    {
        public string PostRequest(Uri serviceAddress, HttpContent content)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = serviceAddress;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
                Task<HttpResponseMessage> response = client.PostAsync(serviceAddress, content);
                var streamTask = (StreamContent)response.Result.Content;
                var text = new StreamReader(streamTask.ReadAsStreamAsync().Result);
                return text.ReadToEnd();
            }
        }

        public string GetRequest(Uri serviceAddress, string authorization)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = serviceAddress;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(string.Format("Bearer {0}", authorization));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                Task<HttpResponseMessage> response = client.GetAsync(serviceAddress);
                return response.Result.Content.ToString();
            }
        }
    }
}
