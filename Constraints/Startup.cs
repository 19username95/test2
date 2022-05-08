using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;

namespace Constraints
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            var myRouteHandler = new RouteHandler(Handle);

            var routeBuilder = new RouteBuilder(app, myRouteHandler);

            routeBuilder.MapRoute("default",
                    "{controller}/{action}/{id?}",
                    new { action = "Index" }
            );

            app.UseRouter(routeBuilder.Build());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("default page");
            });
        }

        private async Task Handle(HttpContext context)
        {
            await context.Response.WriteAsync("route is chosen");
        }
    }
}
