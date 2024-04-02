using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vert_Interview_Project.Constant
{
    public static class URLConstant
    {
        public const string GetToken = "/api/token";
        public const string GetCategory = "/api/categories/{token}";
        public const string GetMagazine = "/api/magazines/{token}/{category}";
        public const string GetSubscriber = "/api/subscribers/{token}";
        public const string PostAnswer = "/api/answer/{token}";
    }
}
