using System.Web.Http;
using System.Web.Http.Cors;

namespace PaymentsAPI
{
    [RoutePrefix("api/payments")]
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.Routes.MapHttpRoute(
                name: "LoginUser",
                routeTemplate: "api/LoginUser/",
                defaults: new
                {
                    PhoneNo = RouteParameter.Optional,
                    access = RouteParameter.Optional
                }
                );

                 config.Routes.MapHttpRoute(
                name: "Validate",
                routeTemplate: "api/Validate/",
                defaults: new
                {
                    token = RouteParameter.Optional,
                    username = RouteParameter.Optional
                }
            );
        }
    }
}
