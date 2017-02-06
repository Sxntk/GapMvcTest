namespace SuperZapatos.Responses
{
    using Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Class to map the response when there is more than one store.
    /// </summary>
    public class StoresResponse
    {
        [JsonProperty("Stores")]
        public IEnumerable<StoreModel> Stores { get; set; }

        [JsonProperty("Sucess")]
        public bool Sucess { get; set; }

        [JsonProperty("TotalElements")]
        public int TotalElements { get; set; }
    }
}