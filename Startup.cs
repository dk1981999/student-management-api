using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using student_management_api.Data;
using student_management_api.Repositories.IRepositories;
using AutoMapper.Extensions.ExpressionMapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using student_management_api.EntityMapper;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.IO;
using student_management_api.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace student_management_api
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
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IStudentRepo, StudentRepo>();

            services.AddScoped<IUserRepo, UserRepo>();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddExpressionMapping();
            },
            typeof(EntityModelMapper));
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("StudentManagementAPISpecification", new OpenApiInfo()
                {
                    Title = "Student Management API",
                    Version = "V1.0",
                    Description = "Student Management Application",
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = 
                    "JWT Authorization header using Bearer scheme. \r\n\r\n " +
                    "Enter 'Bearer' [space] and then your token in the text input below.\r\r\n" +
                    "Example:\"Bearer 12331abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme="Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
                var xmlCommantFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommantsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommantFile);
                options.IncludeXmlComments(xmlCommantsFullPath);
            });

            var appSettingsSection = Configuration.GetSection("AppSettings");

            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(options => options
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/StudentManagementAPISpecification/swagger.json", "Student Management API");
                options.RoutePrefix = "";
            });
        }
    }
}
