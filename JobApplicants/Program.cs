using JobApplicants.Repositories.InMemory;
using JobApplicants.Repositories.MongoDB.JobApplicantsApi;
using JobApplicants.Repositories.MongoDB.JobPostsApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//Jwt authentication
builder.Services.AddAuthentication().AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Admin:Token")!))
    };
});


//convert strings to enums
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters
            .Add(new JsonStringEnumConverter()));

//Register dependency and inject into controller for in memory
//builder.Services.AddSingleton<IInMemItemsRepository, InMemItemsRepository>();

//-----Dependency injection for MongoDB-----------

// ---- Job Post Api -----

//Get JobPostStoreDataBaseSettings inside of appSettings and map to JobPostStoreDataBaseSettings class
builder.Services.Configure<JobPostStoreDataBaseSettings>(
                builder.Configuration.GetSection(nameof(JobPostStoreDataBaseSettings)));

//Everytime IJobPostStoreDataBaseSettings is required, provide an instance of JobPostStoreDataBaseSettings (global => singleton = single instance for lifetime of app)
builder.Services.AddSingleton<IJobPostStoreDataBaseSettings>(sp =>
    sp.GetRequiredService<IOptions<JobPostStoreDataBaseSettings>>().Value);

//Inject into IMongoClient the instance of mongoClient with connection string
builder.Services.AddSingleton<IMongoClient>(s =>
        new MongoClient(builder.Configuration.GetValue<string>("JobPostStoreDataBaseSettings:ConnectionString")));

//Inject into IJobPostService the instance of JobPostService (single instance for duration of scoped request)
builder.Services.AddScoped<IJobPostService, JobPostService>();



// ---- Applicant Api -----
builder.Services.Configure<JobApplicantStoreDataBaseSettings>(
                builder.Configuration.GetSection(nameof(JobApplicantStoreDataBaseSettings)));

builder.Services.AddSingleton<IJobApplicantStoreDataBaseSettings>(sp =>
    sp.GetRequiredService<IOptions<JobApplicantStoreDataBaseSettings>>().Value);

builder.Services.AddScoped<IJobApplicantService, JobApplicantService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

    app.Run();
