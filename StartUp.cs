
using api_app.Filter;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddAuthorization(opciones =>
            {
                opciones.AddPolicy("EsAdmin", politica => politica.RequireClaim("esAdmin"));
            });

            services.AddDbContext<ApplicationDbContext>(options
             => options.UseNpgsql(Configuration.GetConnectionString("defaultConnection")));


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;

            }).AddGoogle(options =>
            {
                options.ClientId = "402562450789-0aau8bfeu40ef95tg4c1c8trnrjoblo5.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Secret"])),
                    ClockSkew = TimeSpan.Zero
                });



            services.AddMvc(options =>
            {
                options.Filters.Add<AuthorizationFilter>();
            });

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            //services.AddDbContext<ApplicationDbContext>(options
            //=> options.UseNpgsql(Configuration.GetConnectionString("LocalConnection")));

            services.AddControllers();

            services.AddAutoMapper(typeof(StartUp));


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


            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }


    }
}
