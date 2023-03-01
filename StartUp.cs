using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace api_app
{
    public class StartUp
    {

        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<FormOptions>(options =>
            {
                options.KeyLengthLimit = int.MaxValue;
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<ApplicationDbContext>(options
             => options.UseNpgsql(Configuration.GetConnectionString("defaultConnection")));

            services.AddControllers();

            //services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()       // AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }


    }
}
