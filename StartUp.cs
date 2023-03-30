using Microsoft.AspNetCore.Authentication.Google;
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


            services.AddDbContext<ApplicationDbContext>(options
             => options.UseNpgsql(Configuration.GetConnectionString("defaultConnection")));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            //services.AddDbContext<ApplicationDbContext>(options
            //=> options.UseNpgsql(Configuration.GetConnectionString("LocalConnection")));

            services.AddControllers();

            services.AddAutoMapper(typeof(StartUp));

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


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            }).AddGoogle(options =>
            {
                options.ClientId = "402562450789-0aau8bfeu40ef95tg4c1c8trnrjoblo5.apps.googleusercontent.com";
                options.ClientSecret = "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";
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
