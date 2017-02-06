namespace SuperZapatos.Responses
{
    using Models;
    using Newtonsoft.Json;

    /// <summary>
    /// Class to map the response when there is only one article.
    /// </summary>
    public class ArticleResponse
    {
        [JsonProperty("Article")]
        public ArticleModel Article { get; set; }

        [JsonProperty("Sucess")]
        public bool Sucess { get; set; }

        [JsonProperty("TotalElements")]
        public int TotalElements { get; set; }
    }
}