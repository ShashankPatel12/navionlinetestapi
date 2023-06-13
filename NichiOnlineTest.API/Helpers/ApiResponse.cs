using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NichiOnlineTest.API.Helpers
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public T Result { get; set; }
        public string ErrorMessage { get; set; }
    }
}
