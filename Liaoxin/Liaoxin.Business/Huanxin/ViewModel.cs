using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liaoxin.Business
{
    public class TokenResponse

    {
        public string application { get; set; }
        public string access_token { get; set; }

        public int expires_in { get; set; }
    }

    public class ErrorResponse
    {
        public string error_description { get; set; }
    } 
}
