using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mobiroller.Common;
using Mobiroller.Data;
using Mobiroller.Filters;
using Mobiroller.Services;
using System.Reflection;
using System.Text;

namespace Mobiroller
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddControllers(x =>
            {
                x.Filters.AddService<MobirollerExceptionAttribute>();
            });

            services.AddRequestLocalization(options =>
            {
                options.RequestCultureProviders.Clear();
                options.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
                options.AddSupportedCultures(new[] { "tr-Tr", "it-IT" });
                options.SetDefaultCulture("tr-TR");
            });

            services.AddDbContext<MobirollerContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Mobiroller"));
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<MobirollerContext>()
                    .AddDefaultTokenProviders();

            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddScoped<IIncidentService, IncidentService>();
            services.AddScoped<ILocaleService, LocaleService>();

            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.AddScoped<IMobirollerLocalization, AcceptLanguageHeaderLocalization>();

            services.AddTransient<MobirollerExceptionAttribute>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.Configure<JwtOptions>(Configuration.GetSection("JwtOptions"));

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Mobiroller Case",
                    Contact = new OpenApiContact()
                    {
                        Name = "Ömer  Demir",
                        Email = "mobiroller@gmail.com"
                    },
                    Version = "v1"
                });

                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });

            var key = Encoding.UTF8.GetBytes(Configuration["JwtOptions:Key"]);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRequestLocalization();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "TbShort API");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}