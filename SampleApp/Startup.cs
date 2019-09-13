using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SampleApp.Configs;
using SampleApp.Models;
using SampleApp.Reponsitory;
using SampleApp.Security;
using SampleApp.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace SampleApp
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
            services.AddDbContext<SampleContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SampleConnection")));

            services.AddMvc();

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
           
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(option =>
            {
                var key = Encoding.ASCII.GetBytes(appSettings.Secret);
                var signingKey = new SymmetricSecurityKey(key);
                //option.TokenValidationParameters = new TokenValidationParameters()
                //{
                //    ValidateAudience = false,
                //    ValidateIssuer = false,
                //    ValidateIssuerSigningKey = true,
                //    //ValidIssuer = "Sample",
                //    //ValidAudience = "Sample",
                //    //IssuerSigningKey = signingConfigurations.Key,
                //    //ClockSkew = TimeSpan.Zero
                //    IssuerSigningKey = new SymmetricSecurityKey(key)
                //};
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSwaggerDocumentation();

            var mappingConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new MappingProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<ITokenHandler, TokenHandler>();

            services.AddScoped<IUserReponsitory, UserReponsitory>();
            services.AddScoped<IRefreshTokenReponsitory, RefreshTokenReponsitory>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseMvc();
            app.UseSwaggerDocumentation();
            app.UseAuthentication();
        }
    }

    public class AppSettings
    {
        public string Secret { get; set; }
    }
}
