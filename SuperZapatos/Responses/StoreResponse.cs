namespace SuperZapatos.Responses
{
    using Newtonsoft.Json;

    using Models;

    /// <summary>
    /// Class to map the response when there is one Store.
    /// </summary>
    public class StoreResponse
    {
        [JsonProperty("Store")]
        public StoreModel Store { get; set; }

        [JsonProperty("Sucess")]
        public bool Sucess { get; set; }

        [JsonProperty("TotalElements")]
        public int TotalElements { get; set; }
    }
}
