

using clothes.api.Common.Extensions;
using clothes.api.Instrafructure.Context;
using clothes.api.Instrafructure.Extensions.AutoMapper;
using clothes.api.Repositories;

namespace clothes.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services
               .AddWebApiConfiguration(builder.Configuration)
               .AddDbContext<ClothesContext>(builder.Configuration)
               .AddAutoMapperConfig<AutoMapperProfile>();

            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(x => x
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.AddCommonApplicationBuilder();
            app.Run();
        }
    }
}