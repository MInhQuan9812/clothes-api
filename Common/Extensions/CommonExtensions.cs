using AutoMapper;
using clothes.api.Common.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using clothes.api.Common.Middlewares;
using clothes.api.Common.Seedworks;
using Microsoft.AspNetCore.Hosting;

namespace clothes.api.Common.Extensions
{
    public static class CommonExtensions
    {
        public static IServiceCollection AddWebApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services
                 .AddControllers()
                 .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddCors();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            services
                 .AddTransient<IHttpContextAccessor, HttpContextAccessor>()
                 .AddDefaultOpenApi(configuration)
                 .AddDefaultAuthentication(configuration)
                 //.AddLogger(configuration)
                 .AddJwtExtension(configuration);
                 //.AddRedisConfiguration(configuration);
            return services;
        }

        public static IServiceCollection AddAutoMapperConfig<TProfile>(this IServiceCollection services) where TProfile : Profile
        {
            services.AddAutoMapper(typeof(TProfile).Assembly);
            return services;
        }

        public static IServiceCollection AddDbContext<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
        {
            services.AddDbContext<T>(x => x.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

            services.AddScoped<DbContext, T>();
            services.AddTransient(typeof(IEfRepository<,>), typeof(EfRepository<,>));
            //services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
        public static IServiceCollection AddDefaultOpenApi(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                                {
                                    {
                                        new OpenApiSecurityScheme
                                        {
                                            Reference = new OpenApiReference
                                            {
                                                Type = ReferenceType.SecurityScheme,
                                                Id = "Bearer"
                                            }
                                        },
                                        new string[] { }
                                    }
                                });
                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });
            return services;
        }


        public static IServiceCollection AddDefaultAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            services
              .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = configuration["JWT:ValidIssuser"],
                    ValidIssuer = configuration["JWT:ValidIssuser"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });
            return services;
        }

        public static IServiceCollection AddJwtExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("JWT");
            if (!section.Exists())
                return services;

            services.Configure<JwtSetting>(section);
            services.AddSingleton<IJwtExtension, JwtExtension>();
            return services;
        }

        public static IApplicationBuilder AddCommonApplicationBuilder(this WebApplication app)
        {
            if (!app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(x => x
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.UseMiddleware<ErrorHandlingMiddleWare>();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
            return app;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(serverOptions =>
                {
                    // Listen on all network interfaces and use HTTPS
                    serverOptions.ListenAnyIP(7240, listenOptions =>
                    {
                        listenOptions.UseHttps("path/to/your/certificate.pfx", "your_certificate_password");
                    });
                });
                webBuilder.UseStartup<Program>();
            });
    }
}
