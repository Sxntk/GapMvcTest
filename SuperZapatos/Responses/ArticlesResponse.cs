namespace SuperZapatos.Responses
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using Models;

    /// <summary>
    /// Class to map the response when there is more than one article.
    /// </summary>
    public class ArticlesResponse
    {
        [JsonProperty("Articles")]
        public IEnumerable<ArticleModel> Articles { get; set; }

        [JsonProperty("Sucess")]
        public bool Sucess { get; set; }

        [JsonProperty("TotalElements")]
        public int TotalElements { get; set; }
    }
}