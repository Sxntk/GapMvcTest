namespace SuperZapatos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using System.Net.Http;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using HttpHelper;
    using Responses;
    public class StoreModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public static IEnumerable<StoreModel> GetAllAStores()
        {
            List<StoreModel> lista = new List<StoreModel>();
            HttpHelperSuperZapatos helper = new HttpHelperSuperZapatos();
            HttpClient client = helper.GetRequest();

            HttpResponseMessage response = client.GetAsync("services/stores").Result;
            Exception ex = ErrorModel.HandleError(response);
            if (ex != null)
            {
                throw ex;
            }

            var jsonArticles = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            if (jsonArticles["TotalElements"] != null && (int)jsonArticles.GetValue("TotalElements") == 1)
            {
                StoreResponse storeResponse = JsonConvert.DeserializeObject<StoreResponse>(jsonArticles.ToString());
                return new List<StoreModel>() { storeResponse.Store };
            }

            StoresResponse responseStores = JsonConvert.DeserializeObject<StoresResponse>(jsonArticles.ToString());
            return responseStores.Stores;
        }

        public static StoreModel CreateStore(StoreModel store)
        {
            HttpHelperSuperZapatos helper = new HttpHelperSuperZapatos();
            HttpClient client = helper.GetRequest();

            string api = string.Format("services/stores/create/{0}/{1}",
                store.Name, store.Address);
            HttpResponseMessage response = client.GetAsync(api).Result;

            Exception ex = ErrorModel.HandleError(response);
            if (ex != null)
            {
                throw ex;
            }

            var jsonArticles = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            StoreResponse storeResponse = JsonConvert.DeserializeObject<StoreResponse>(jsonArticles.ToString());
            return storeResponse.Store;
        }

        /// <summary>
        /// Retrieve all the stores and ask if the storeName is in the stores.
        /// </summary>
        /// <param name="storeName">The store name.</param>
        /// <returns>Guid of the store if it is valid to else, otherwise Guid.Empty.</returns>
        public static Guid IsValidStore(string storeName)
        {
            IEnumerable<StoreModel> stores = GetAllAStores();
            StoreModel store = stores.FirstOrDefault(x => x.Name == storeName);
            if (store != null)
            {
                return store.Id;
            }

            return Guid.Empty;
        }
    }
}