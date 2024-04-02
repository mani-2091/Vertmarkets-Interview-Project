using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vert_Interview_Project.Model.Response
{
    public class GetSubscriberResponseModel
    {
        [JsonProperty("data")]
        public List<DataModel> Data { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public class DataModel
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("firstName")]
            public string FirstName { get; set; }

            [JsonProperty("lastName")]
            public string LastName { get; set; }

            [JsonProperty("magazineIds")]
            public List<int> MagazineIds { get; set; }
        }
    }
}
