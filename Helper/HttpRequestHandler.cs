using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Vert_Interview_Project.Helper
{
    public static class HttpRequestHandler
    {
        public static async Task<T> MakeRequestAsync<T>(string url, string method, Dictionary<string, string>? header, string body, string username, string password, string? authType = null, Dictionary<string, string> queryParams = null, string grantType = null)
        {
            try
            {
                using HttpClient _client = new HttpClient();

                var httpMethod = GetHttpMethod(method);

                // Set the request URI and method
                var request = new HttpRequestMessage
                {
                    Method = httpMethod
                };

                // Add query string parameters (if applicable)
                if (queryParams != null && queryParams.Count > 0)
                {
                    var uriBuilder = new UriBuilder(url);
                    var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                    foreach (var kvp in queryParams)
                    {
                        query[kvp.Key] = kvp.Value;
                    }
                    uriBuilder.Query = query.ToString();
                    request.RequestUri = uriBuilder.Uri;
                }
                else
                {
                    request.RequestUri = new Uri(url);
                }

                if (header != null)
                {
                    // Set the request headers
                    foreach (var kvp in header)
                    {
                        if (kvp.Key.ToLower() != "content-length" && kvp.Key.ToLower() != "host" && kvp.Key.ToLower() != "content-type")
                            request.Headers.Add(kvp.Key, kvp.Value.ToString());
                    }
                }

                // Set the request body (if applicable)
                if (httpMethod != HttpMethod.Get && !string.IsNullOrEmpty(body))
                {
                    request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                }

                if (!string.IsNullOrEmpty(grantType))
                {
                    var values = new[] { new KeyValuePair<string, string>("grant_type", grantType) };
                    var content = new FormUrlEncodedContent(values);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                    request.Content = content;
                }
                // Set the authorization header
                if (authType == "Bearer")
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", password);
                }
                else if (authType == "Basic")
                {
                    string authValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                    request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authValue);
                }
                else if (authType == "APIKey")
                {
                    request.Headers.Add(username, password);
                }

                // Send the request and get the response
                var response = await _client.SendAsync(request);

                // Read the response body as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the response body into an object of type T
                T result = JsonConvert.DeserializeObject<T>(responseBody);

                // Return the deserialized object
                return result;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as desired
                Console.WriteLine(ex.Message);
                return default;
            }
        }

        private static HttpMethod GetHttpMethod(string method)
        {
            // Convert the method string to an HttpMethod enum value
            HttpMethod httpMethod;
            switch (method.ToLowerInvariant())
            {
                case "get":
                    httpMethod = HttpMethod.Get;
                    break;
                case "post":
                    httpMethod = HttpMethod.Post;
                    break;
                case "patch":
                    httpMethod = new HttpMethod("PATCH");
                    break;
                case "delete":
                    httpMethod = HttpMethod.Delete;
                    break;
                default:
                    throw new ArgumentException("Invalid HTTP method.", nameof(method));
            }

            return httpMethod;
        }

    }
}
