using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vert_Interview_Project.Model.Response
{
    public class PostAnswerResponseModel
    {
        [JsonProperty("data")]
        public DataModel Data { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }


        public class DataModel
        {
            [JsonProperty("totalTime")]
            public string TotalTime { get; set; }

            [JsonProperty("answerCorrect")]
            public bool AnswerCorrect { get; set; }

            [JsonProperty("shouldBe")]
            public List<string> ShouldBe { get; set; }
        }
    }
}
