using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WeActiveWebservice.Models.Local;

namespace WeActiveWebservice {


    public class Startup {

		public IConfigurationRoot theConfiguration { get; }

        public Startup(IHostingEnvironment env) {

            /*var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();*/

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

			Console.WriteLine($"option1 = {Configuration["option1"]}");
			Console.WriteLine($"option2 = {Configuration["option2"]}");

            Console.WriteLine($"option1 = {Configuration["ConnectionStrings:DefaultConnection"]}");


        }

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            
            services.AddMvc();

            services.AddOptions();

            services.Configure<ConnectionStrings>(Configuration);


            //services.AddSingleton<IDbconnection,new ConnectionStrings("Server=127.0.0.1;Database=test;Uid=root;Pwd=;")>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
