using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Helpers;

namespace Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            // Adding the authentication with JWT tokens
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                // The recommended way is to store your secrets in some file that is not accessible by anyone.
                // For more information, look up secrets.json in .Net Core projects.
                string secret = "your_secret_here";
                var key = Encoding.ASCII.GetBytes(secret);
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Here you can add authorization options, the ideas are below
            // EmployeeOnly is authorization filter, so that some endpoints are accessible by Employees only.
            // By adding [Authorization(Policy = "EmployeeOnly")] above the endpoints you are protecting the certain endpoints.
            // Also you can create your custom policies like "HasEmail" that is contained in Helpers/Middleware.cs class
            services.AddAuthorization(options =>
            {
                //options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));
                //options.AddPolicy("HasEmail", policy => policy.Requirements.Add(new HasEmail()));
            });

            // Connection to MongoDB, provide database connection string and databaseName
            MongoDriver.MongoDriverInstance.ConnectToDB("connectionString", "databaseName");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            // Setting up CORS, Don't reccoment to leave AllowAnyOrigin, rather put the .WithOrigins("applicationurl")
            // Example for allowing CORS just for the localhost application: builder.WithOrigins("http://localhost:4200")
            app.UseCors(builder => builder.AllowAnyOrigin()
                                          .AllowAnyMethod()
                                          .WithHeaders("authorization", "accept", "content-type"));
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
