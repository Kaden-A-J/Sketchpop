using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sketchpop
{
    public class Unsplash_Manager
    {
        private readonly string _access_key = "uSAp84GR3QQdBCCXAENt-gHQKs4-8DhvxhMCTWVTOZ4";
        private string _user_query;
        private int _max_page_count = 5;
        private int _per_page = 5;

        private HashSet<string> _unique_ids = new HashSet<string>();        

        public Unsplash_Manager() { 
        }

        public List<UnsplashImage> Get_Images(string query)
        {
            // Generate the HTTP request to Unsplash 
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", _access_key);

            List<UnsplashImage> images = new List<UnsplashImage>();
            for (int p = 1; p <= _max_page_count; p++) {
                string url = $"https://api.unsplash.com/search/photos?query={query}&per_page={_per_page}&page={p}";

                try { 
                    // Send request/get the repsonse 
                    HttpResponseMessage http_response = client.GetAsync(url).Result;
                    string response = http_response.Content.ReadAsStringAsync().Result;

                    // Parse the JSON data from the response 
                    JObject jo = JsonConvert.DeserializeObject<JObject>(response);
                    JArray json_images = (JArray)jo["results"];

                    foreach (JObject result in json_images) { 
                        string id = query + "-" + result["id"].ToString();

                        if (!_unique_ids.Contains(id)) {
                            string description = result["description"].ToString();
                            string author_name = result["user"]["name"].ToString();
                            string author_profile = result["user"]["links"]["html"].ToString();
                            string image_url = result["urls"]["regular"].ToString();
                            string unsplash_url = result["links"]["html"].ToString();

                            UnsplashImage image = new UnsplashImage(id, description, author_name, author_profile, image_url,unsplash_url);

                            images.Add(image);
                            _unique_ids.Add(id);
                            _per_page--;
                        }
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    break;
                }
                if (images.Count == 5) { break; }
            }
            return images; 
        }
    }
}
