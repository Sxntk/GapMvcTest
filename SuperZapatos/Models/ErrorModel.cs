namespace SuperZapatos.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Exceptions;
    using System.Net.Http;
    using System;

    /// <summary>
    /// Class to map the response in case there are errors.
    /// </summary>
    public class ErrorModel
    {
        [JsonProperty("Sucess")]
        public bool Sucess { get; set; }

        [JsonProperty("ErrorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Handles all possible errors from the Api.
        /// </summary>
        /// <param name="response">The response from the Web Api.</param>
        /// <returns>Null if there no exceptions, otherwise the type of the exception raised
        /// Exception type for all un handle exceptions.</returns>
        public static Exception HandleError(HttpResponseMessage response)
        {
            // Deserialize the object into an erro object
            var jsonError = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            // If the error is a known error we handle
            ErrorModel error = JsonConvert.DeserializeObject<ErrorModel>(jsonError.ToString());
            switch (error.ErrorCode)
            {
                case "400":
                    return new BadRequestException(error.ErrorMessage);

                case "401":
                    return new UnauthorizedException(error.ErrorMessage);

                case "404":
                    return new RecordNotFoundException(error.ErrorMessage);

                case "500":
                    return new Exception(error.ErrorMessage);

                default:
                    break;
            }

            // If the error is unknown
            if (jsonError["Sucess"] == null || !(bool)jsonError["Sucess"])
            {
                return new Exception(jsonError.ToString());
            }

            // If the is no error, we return null
            return null;
        }
    }
}