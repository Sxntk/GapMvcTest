using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperZapatos.HttpHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace SuperZapatos.HttpHelper.Tests
{
    [TestClass()]
    public class HttpHelperSuperZapatosTests
    {
        [TestInitialize]
        public void Start()
        {
            WebConfigurationManager.AppSettings["WebApiEndpoint"] = "https://localhost";
        }

        [TestMethod()]
        public void HttpClientIsCreating_myusername_mypassword_AsUserAndPassTest()
        {
            // Expects
            string username = "my_user";
            string password = "my_password";

            // Arrange
            HttpHelperSuperZapatos helper = new HttpHelperSuperZapatos();

            // Act
            HttpClient client = helper.GetRequest();

            string credentials = Encoding.UTF8.GetString(Convert.FromBase64String(client.DefaultRequestHeaders.Authorization.Parameter));
            string requestUserName = string.Empty;
            string requestPassword = string.Empty;

            int separatorIndex = credentials.IndexOf(':');
            if (separatorIndex >= 0)
            {
                requestUserName = credentials.Substring(0, separatorIndex);
                requestPassword = credentials.Substring(separatorIndex + 1);
            }

            // Assert
            Assert.AreEqual(username, requestUserName);
            Assert.AreEqual(password, requestPassword);
        }

        [TestMethod()]
        public void FormatForTheRequestIsJSON_Test()
        {
            // Expects
            string mediaType = "application/json";

            // Arrange
            HttpHelperSuperZapatos helper = new HttpHelperSuperZapatos();
            
            // Act
            HttpClient client = helper.GetRequest();
            MediaTypeWithQualityHeaderValue media = client.DefaultRequestHeaders.Accept.FirstOrDefault(x => x.MediaType == mediaType);

            // Assert
            Assert.IsNotNull(media);
            Assert.AreEqual(mediaType, media.MediaType);
        }
    }
}