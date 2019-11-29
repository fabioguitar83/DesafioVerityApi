using DesafioVerity.Domain.Common;
using DesafioVerity.Domain.Interfaces.Entity;
using DesafioVerity.Handler.Account;
using DesafioVerity.Repository.Entity;
using DesafioVerity.Repository.Infrastructure;
using DesafioVerity.Repository.Initialize;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DesafioVerity.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //REPOSITORY INJECTION
            services.AddScoped<IExtractRepository, ExtractRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountHolderRepository, AccountHolderRepository>();

            //DATABASE INJECTION
            var connection = Configuration["ConnectionStrings:DefaultConnection"];

            services.AddDbContext<Context>(options =>
                options.UseMySQL(connection)
            );

            //MEDIATR
            services.AddMediatR(typeof(Startup), typeof(AccountHandler));

            //SWAGGER
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Desafio Verity",
                        Version = "v1",
                        Description = "",
                        Contact = new Contact
                        {
                            Name = "Fabio Guedes dos Santos",
                            Url = "https://github.com/fabioguitar83"
                        }
                    });

                string caminhoAplicacao =
                    PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao =
                    PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc =
                    Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                c.IncludeXmlComments(caminhoXmlDoc);

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer",new ApiKeyScheme
                {
                    In = "header",
                    Description = "Por favor, insira no campo abaixo a palavra 'Bearer', seguida por espaço e o token JWT gerado no login",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(security);
            });

            var jwtConfigurationSection = Configuration.GetSection("JwtConfiguration");
            services.Configure<JwtConfigurations>(jwtConfigurationSection);

            var jwtConfiguration = jwtConfigurationSection.Get<JwtConfigurations>();
            var key = Encoding.ASCII.GetBytes(jwtConfiguration.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtConfiguration.Issuer,
                    ValidAudience = jwtConfiguration.Audience,
                };
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Context contexto)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseMvc();

            //ENABLING MIDDLEWARE FOR USE BY SWAGGER 
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Desafio Verity");
            });


            InicializaBD.Initialize(contexto);
        }
    }
}
