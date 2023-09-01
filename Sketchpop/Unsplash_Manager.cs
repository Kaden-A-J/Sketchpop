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
    /// <summary>
    /// This class manages the calls and methods for getting images using the Unsplash API calls.
    /// </summary>
    public class Unsplash_Manager
    {
        private readonly string _access_key = "uSAp84GR3QQdBCCXAENt-gHQKs4-8DhvxhMCTWVTOZ4"; // access key provided by Unsplash API
        private int _max_page_count = 5; // number of pages to look for images

        public Unsplash_Manager()
        {
        }

        /// <summary>
        /// Retrieves the number of images requested by the user. Searches through 
        /// number of pages specified by the _max_page_count variable. Returns the 
        /// collected images in a List.
        /// </summary>
        /// <param name="query">search string entered by the user</param>
        /// <param name="num_pics">number of requested images chosen by the user</param>
        /// <returns></returns>
        public List<UnsplashImage> Get_Images(string query, int num_pics)
        {
            // Generate the HTTP request to Unsplash 
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", _access_key);

            List<UnsplashImage> images = new List<UnsplashImage>();

            // loop through pages until the number of images requested has been reached
            for (int p = 1; p <= _max_page_count; p++)
            {
                string url = $"https://api.unsplash.com/search/photos?query={query}&per_page={num_pics}&page={p}";

                try
                {
                    // Send request/get the repsonse 
                    HttpResponseMessage http_response = client.GetAsync(url).Result;
                    string response = http_response.Content.ReadAsStringAsync().Result;

                    // Parse the JSON data from the response 
                    JObject jo = JsonConvert.DeserializeObject<JObject>(response);
                    JArray json_images = (JArray)jo["results"];

                    // construct the Unsplash_Image object
                    foreach (JObject result in json_images)
                    {
                        string id = query + "-" + result["id"].ToString();

                        string description = result["description"].ToString();
                        string author_name = result["user"]["name"].ToString();
                        string author_profile = result["user"]["links"]["html"].ToString();
                        string image_url = result["urls"]["regular"].ToString();
                        string unsplash_url = result["links"]["html"].ToString();

                        UnsplashImage image = new UnsplashImage(id, description, author_name, author_profile, image_url, unsplash_url);

                        images.Add(image);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                if (images.Count == num_pics) { break; } // exit loop if number of images has been reached
            }
            return images;
        }
    }
}
