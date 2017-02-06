namespace SuperZapatos.HttpHelper
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Web.Configuration;

    /// <summary>
    /// Helper used to create the endpoint from the AppConfig and create the request to the web api.
    /// </summary>
    public class HttpHelperSuperZapatos
    {
        private string Endpoint;

        public HttpHelperSuperZapatos()
        {
            this.Endpoint = WebConfigurationManager.AppSettings["WebApiEndpoint"];
        }

        /// <summary>
        /// Get the HttpClient to make the request to the web api.
        /// </summary>
        /// <returns>The client with the endpoint, user, password and request for JSON.</returns>
        public HttpClient GetRequest()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(this.Endpoint);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string request = Convert.ToBase64String(Encoding.UTF8.GetBytes("my_user:my_password"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", request);

            return client;
        }
    }
}