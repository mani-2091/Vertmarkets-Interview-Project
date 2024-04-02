using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vert_Interview_Project.Model.Request;
using Vert_Interview_Project.Model.Response;

namespace Vert_Interview_Project.Service
{
    public interface IVertMagazineService
    {
        string token { get; set; }
        //Task<GetTokenResponseModel> GetToken();
        Task<GetCategoryResponseModel> GetCategory();
        Task<GetMagazineResponseModel> GetMagazine(string category);
        Task<GetSubscriberResponseModel> GetSubscriber();
        Task<PostAnswerResponseModel> PostAnswer(PostAnswerRequestModel req);
    }
}
