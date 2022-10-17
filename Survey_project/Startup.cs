using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Survey_project.Entities;
using Survey_project.Repository;
using Survey_project.Services;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Survey_project
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
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("103.50.212.140"));
            });
            services.AddHttpClient();
            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ContractResolver = new DefaultContractResolver();
                o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                o.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                o.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ssZ";
            });
            services.AddDbContext<SurveyDbContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), ServerVersion.Parse("8.0.28-mysql"))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Survey_project",
                    new OpenApiInfo
                    {
                        Title = "Survey_project",
                        Version = "1.1",
                        Description = "Survey_project using ASP.NET CORE 6",
                        Contact = new OpenApiContact
                        {
                            Name = "Dileep N V",
                            Email = "dileepnv123@gmail.com",
                        },
                    });
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.",
                });

                ////important check the class presence
                //c.OperationFilter<AuthOperationFilter>();
            });




            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(options =>
                {
                    //options.Authority = "Authority URL";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration.GetValue<string>("JwtOptions:Issuer"),
                        ValidAudience = Configuration.GetValue<string>("JwtOptions:Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtOptions:SecurityKey")))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) /*&&
                                (path.StartsWithSegments("https://Survey.com/"))*/)
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });


            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                      .WithOrigins(
                                   "http://localhost:4200",
                                   "https://localhost:4200",
                                   "http://development2.promena.in"
                                   )
                      .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowCredentials();
            }));

            services.AddHttpContextAccessor();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddTransient<IAccountServices, AccountServices>();
            services.AddTransient<IEmailService, EmailServices>();
            services.AddTransient<ISurveyService, SurveyService>();
            services.AddTransient<IUserServices, UserService>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHost hostBuilder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            //Sends mail to the developer if there is an internal server error along with api name.
            else
            {
                var _exceptionMail = ActivatorUtilities.CreateInstance<EmailServices>(hostBuilder.Services);

                app.UseExceptionHandler(options =>
                {
                    options.Run(
                        async context =>
                        {
                            var ex = context.Features.Get<IExceptionHandlerFeature>();
                            if (ex != null)
                            {
                                _exceptionMail.SendExceptionMail(ex, context);
                            }
                        });
                });
            }

            app.UseStatusCodePages("text/plain", "Status code page, status code: {0}");

            app.UseHttpsRedirection();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseWebSockets();

            app.UseHsts();

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("Survey_project/swagger.json", "Survey_project"));



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.HasValue && context.Request.Path.Value != "/")
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.SendFileAsync(
                        env.ContentRootFileProvider.GetFileInfo("wwwroot/index.html")
                    );
                    return;
                }
                await next();
            });

        }
    }
}
