namespace SuperZapatos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Net.Http;

    using HttpHelper;
    using Responses;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ArticleModel
    {
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("StoreId")]
        public Guid StoreId { get; set; }

        [JsonProperty("StoreName")]
        [Display(Name = "Store Name")]
        [Required(ErrorMessage = "Store is required")]
        public string StoreName { get; set; }

        [JsonProperty("Name")]
        [Display(Name = "Product name")]
        [Required]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Price")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        [Required]
        public decimal Price { get; set; }

        [JsonProperty("TotalInShelf")]
        [Display(Name = "Total in shelf"), DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true, NullDisplayText = "0")]
        public decimal? TotalInShelf { get; set; }

        [JsonProperty("TotalInVault")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true, NullDisplayText = "0")]
        [Display(Name = "Total in vault")]
        public decimal? TotalInVault { get; set; }

        public static IEnumerable<ArticleModel> GetAllArticles()
        {
            // Create the http request
            HttpHelperSuperZapatos helper = new HttpHelperSuperZapatos();
            HttpClient client = helper.GetRequest();

            // Gets all the articles
            HttpResponseMessage response = client.GetAsync("services/articles").Result;

            // If there are no errors continue
            Exception ex = ErrorModel.HandleError(response);
            if (ex != null)
            {
                throw ex;
            }

            // Deserialize the object
            var jsonArticles = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            // If there is only one element we just map to a collection with one article
            if (jsonArticles["TotalElements"] != null && (int)jsonArticles.GetValue("TotalElements") == 1)
            {
                ArticleResponse articleResponse = JsonConvert.DeserializeObject<ArticleResponse>(jsonArticles.ToString());
                return new List<ArticleModel>() { articleResponse.Article };
            }

            // If there is more than one element we map to a collection of articles
            ArticlesResponse articlesResponse = JsonConvert.DeserializeObject<ArticlesResponse>(jsonArticles.ToString());
            return articlesResponse.Articles;
        }

        public static IEnumerable<ArticleModel> GetArticleByStore(Guid id)
        {
            // Create the http request
            HttpHelperSuperZapatos helper = new HttpHelperSuperZapatos();
            HttpClient client = helper.GetRequest();

            // Gets all the articles by store id
            string api = string.Format("services/articles/stores/{0}", id);
            HttpResponseMessage response = client.GetAsync(api).Result;

            // If there are no errors continue
            Exception ex = ErrorModel.HandleError(response);
            if (ex != null)
            {
                throw ex;
            }

            // Deserialize the object
            var jsonArticles = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            // If there is only one element, just map to a collection with one article
            if (jsonArticles["TotalElements"] != null && (int)jsonArticles.GetValue("TotalElements") == 1)
            {
                ArticleResponse articleResponse = JsonConvert.DeserializeObject<ArticleResponse>(jsonArticles.ToString());
                return new List<ArticleModel>() { articleResponse.Article };
            }

            // If there is more than one element, maps to a collection of articles
            ArticlesResponse articlesResponse = JsonConvert.DeserializeObject<ArticlesResponse>(jsonArticles.ToString());
            return articlesResponse.Articles;
        }

        public static ArticleModel CreateArticle(ArticleModel article)
        {
            // Create the http request
            HttpHelperSuperZapatos helper = new HttpHelperSuperZapatos();
            HttpClient client = helper.GetRequest();

            // Sends to create the article with all the values
            // In case we use special characters like '/' we encode in base64 or another converter
            // This scenario is not supported in this test
            string api = string.Format("services/articles/create/{0}/{1}/{2}/{3}/{4}/{5}",
                article.Name, article.Description, article.Price, article.TotalInShelf, article.TotalInVault, article.StoreId);

            // Deserialize the object, should be the object we sent to create
            HttpResponseMessage response = client.GetAsync(api).Result;

            // If there are no errors continue
            Exception ex = ErrorModel.HandleError(response);
            if (ex != null)
            {
                throw ex;
            }

            // Deserialize the object
            var jsonArticles = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            // Expects one article
            ArticleResponse articleResponse = JsonConvert.DeserializeObject<ArticleResponse>(jsonArticles.ToString());

            // Store name is not in the article database, it maps from the sent store name
            articleResponse.Article.StoreName = article.StoreName;
            return articleResponse.Article;
        }
    }
}