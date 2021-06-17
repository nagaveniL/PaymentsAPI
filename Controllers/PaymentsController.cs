using System;
using System.Data;
using System.Net.Http;
using System.Web.Http;
using PaymentsAPI.Model;
using System.Web.Http.Filters;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Diagnostics;
using System.Threading;

namespace PaymentsAPI.Controllers
{
    public class UserDetails
    {
        public string PhoneNo { get; set; }
        public string access { get; set; }
    }
    public class TokenDetails
    {
        public string token { get; set; }
        public string username { get; set; }
    }
    //[Log]
    [RoutePrefix("api/payments")]
    public class PaymentsController : ApiController
    {
        Data obj = new Data(); 
        [Route("LoginUser/")]
        [HttpPost]
        public object LoginUser(UserDetails details)        
        {
            string strSQL = "call payments.sp_getdetails('{1}', '{2}')";
            DataTable dt = new DataTable();
            strSQL = strSQL
                     //.Replace("{0}", id)
                     .Replace("{1}", details.PhoneNo)
                     .Replace("{2}", details.access);
            dt = obj.ExecuteQuery(strSQL);
            if (dt.Rows.Count > 1 || dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return new
                {
                    Status = "Success",
                    Message = TokenManager.GenerateToken(dt.Rows[0][1].ToString()),
                    Username = dt.Rows[0][1].ToString()
                    //dt.Rows[0][2].ToString()
                };
            }
        }

        [Route("Validate/")]
        [HttpPost]
        public object Validate(TokenDetails details)
        {
            int UserId = '2';
            if (UserId == 0) return new 
            {
                Status = "Invalid",
                Message = "Invalid User."
            };
            string tokenUsername = TokenManager.ValidateToken(details.token);
            if (details.username.Equals(tokenUsername))
            {
                return new 
                {
                    Status = "Success",
                    Message = "OK",
                };
            }
            return new 
            {
                Status = "Invalid",
                Message = "Invalid Token."
            };
        }
        public class LogAttribute : Attribute, IActionFilter
        {
            public LogAttribute()
            {

            }

            public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
            {
                Trace.WriteLine(string.Format("Action Method {0} executing at {1}", actionContext.ActionDescriptor.ActionName, DateTime.Now.ToShortDateString()), "Web API Logs");

                var result = continuation();

                result.Wait();

                Trace.WriteLine(string.Format("Action Method {0} executed at {1}", actionContext.ActionDescriptor.ActionName, DateTime.Now.ToShortDateString()), "Web API Logs");

                return result;
            }

            public bool AllowMultiple
            {
                get { return true; }
            }
        }
    }
}

        /*
        public string Get(string id, string emailPhone,string displayName, string access)
        // public string Get(string id)        
        {
            string strSQL = "call payments.sp_Adddetails('{0}', '{1}', '{2}', '{3}')";
            DataTable dt = new DataTable();
            strSQL = strSQL
                     .Replace("{0}", id)
                     .Replace("{1}", emailPhone)
                     .Replace("{2}", displayName)
                     .Replace("{3}", access);
            dt = obj.ExecuteQuery(strSQL);
            if (dt.Rows.Count > 1 || dt.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0][2].ToString();
            }
        }
        */
    

