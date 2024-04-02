using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vert_Interview_Project.Constant;
using Vert_Interview_Project.Helper;
using Vert_Interview_Project.Model.Request;
using Vert_Interview_Project.Model.Response;

namespace Vert_Interview_Project.Service
{
    public class VertMagazineService : IVertMagazineService
    {
        public string token { get; set; }
        public static GetCategoryResponseModel categoryList { get; set; }
        public static Dictionary<string, GetMagazineResponseModel> categoryBasedMagazine { get; set; }

        public static string baseUrl;
        public VertMagazineService(string _baseUrl)
        {

            baseUrl = _baseUrl;
            var result = GetToken()?.Result;
            token = result?.Token ?? "";
            categoryBasedMagazine = new Dictionary<string, GetMagazineResponseModel>();
        }

        public async Task<GetCategoryResponseModel> GetCategory()
        {
            if (categoryList != null)
            {
                return categoryList;
            }
            var res = await HttpRequestHandler.MakeRequestAsync<GetCategoryResponseModel>(baseUrl + URLConstant.GetCategory.Replace("{token}", token), "GET", null, null, null, null, null);
            categoryList = res;
            return res;
        }

        public async Task<GetMagazineResponseModel> GetMagazine(string category)
        {
            var data = categoryBasedMagazine?.FirstOrDefault(x => x.Key == category);
            if (data != null && data?.Key != null)
            {
                return data?.Value;
            }
            var res = await HttpRequestHandler.MakeRequestAsync<GetMagazineResponseModel>(baseUrl + URLConstant.GetMagazine.Replace("{token}", token).Replace("{category}", category), "GET", null, null, null, null, null);
            categoryBasedMagazine.Add(category, res);
            return res;
        }

        public async Task<GetSubscriberResponseModel> GetSubscriber()
        {
            var res = await HttpRequestHandler.MakeRequestAsync<GetSubscriberResponseModel>(baseUrl + URLConstant.GetSubscriber.Replace("{token}", token), "GET", null, null, null, null, null);
            return res;
        }

        public async Task<GetTokenResponseModel> GetToken()
        {
            var res = await HttpRequestHandler.MakeRequestAsync<GetTokenResponseModel>(baseUrl + URLConstant.GetToken, "GET", null, null, null, null, null);
            return res;
        }

        public async Task<PostAnswerResponseModel> PostAnswer(PostAnswerRequestModel req)
        {
            PostAnswerResponseModel? output = null;

            var res = await HttpRequestHandler.MakeRequestAsync<PostAnswerResponseModel>(baseUrl + URLConstant.PostAnswer.Replace("{token}", token), "POST", null, JsonConvert.SerializeObject(req), null, null, null);
            return res;
        }
    }
}
