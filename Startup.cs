using AutoMapper;
using Azure;
using Azure.AI.FormRecognizer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RSMessageProcessor;
using RSMessageProcessor.RabbitMQ.Interface;
using ScannedAPI.Automapper;
using ScannedAPI.Repositories;
using ScannedAPI.Repositories.Context;
using ScannedAPI.Repositories.Contexts.Interfaces;
using ScannedAPI.Repositories.Interfaces;
using ScannedAPI.Services;
using ScannedAPI.Services.Handlers;
using ScannedAPI.Services.Interfaces;
using ScannedAPI.SignalR;
using System;
using System.Threading.Tasks;
using ScannedAPI.Extensions;

namespace ScannedAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private static readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


        // Creates a Cosmos DB database and a container with the specified partition key. 
        private static async Task<CosmosDbContext> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
        {
            var databaseName = configurationSection.GetSection("DatabaseName").Value;
            var containerName = configurationSection.GetSection("ContainerName").Value;
            var uri = configurationSection.GetSection("Uri").Value;
            var key = configurationSection.GetSection("Key").Value;
            var client = new CosmosClient(uri, key, new CosmosClientOptions() { ConnectionMode = ConnectionMode.Gateway });
            var cosmosDbService = new CosmosDbContext(client, databaseName, containerName);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName, 400);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/partitionKey");

            return cosmosDbService;
        }

        private FormRecognizerService InitializeFormRecognizer()
        {
            var credential = new AzureKeyCredential(Configuration.GetValue<string>("FormRecognizerApiKey"));
            var client = new FormRecognizerClient(new Uri(Configuration.GetValue<string>("FormRecognizerEndpoint")), credential);
            return new FormRecognizerService(client);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy(MyAllowSpecificOrigins, policy =>
                {
                    policy.WithOrigins("http://localhost:3000", "http://localhost:3001", "https://scannly.azurewebsites.net")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ScannedAPI", Version = "v1" });
            });
             
            // auto mapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            var rabbitConfig = Configuration.GetSection("Rabbit");

            services.AddRabbitConsumer(rabbitConfig);
            services.AddHostedService<UploadImageService>();
            services.AddSignalR();
            services.AddJWTTokenServices(Configuration);

            // singleton
            services.AddSingleton(mapper);
            services.AddSingleton<IFormRecognizerService>(InitializeFormRecognizer());
            services.AddSingleton<ICosmosDbContext>(InitializeCosmosClientInstanceAsync(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());

            // scoped
            services.AddScoped<IRabbitHandler<string>, UploadImageHandler>();

            // transient
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUsersRepository, UsersRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScannedAPI v1"));
            }

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("/hub/upload");
            });
        }
    }
}
