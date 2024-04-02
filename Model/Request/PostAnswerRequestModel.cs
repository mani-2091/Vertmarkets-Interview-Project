using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vert_Interview_Project.Model.Request
{
    public class PostAnswerRequestModel
    {
        [JsonProperty("subscribers")]
        public List<string> Subscribers { get; set; }
    }
}
