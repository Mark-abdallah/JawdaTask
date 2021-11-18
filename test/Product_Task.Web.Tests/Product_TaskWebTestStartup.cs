using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Product_Task
{
    public class Product_TaskWebTestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<Product_TaskWebTestModule>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.InitializeApplication();
        }
    }
}