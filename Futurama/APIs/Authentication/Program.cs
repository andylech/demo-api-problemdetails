using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using ProblemDetailsApiDemo.Futurama.Apis.Authentication.Handlers;
using ProblemDetailsApiDemo.Futurama.Apis.Authentication.Models;
using ProblemDetailsApiDemo.Futurama.Shared.Models.Requests.Authentication;
using ProblemDetailsApiDemo.Futurama.Shared.Models.Source.Entities;
using static Microsoft.AspNetCore.Http.StatusCodes;

#pragma warning disable CA2254

#region Site Setup

var applicationOptions = new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.GetName().Name,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = Environments.Development
};
var builder = WebApplication.CreateBuilder(applicationOptions);

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddDebug();

// Pull app settings
builder.Services.AddOptions();
builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));

// Add controllers
builder.Services.AddControllers();

// Add OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
    setup.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Mock Authentication API",
        Version = "v1"
    }));

var app = builder.Build();

var logger = app.Logger;
app.Logger.LogDebug($"Application Name: \t{builder.Environment.ApplicationName}");
logger.LogDebug($"Environment Name: \t{app.Environment.EnvironmentName}");
logger.LogDebug($"ContentRoot Path: \t{app.Environment.ContentRootPath}");
logger.LogDebug($"WebRoot Path: \t{app.Environment.WebRootPath}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

#endregion

#region Mock Authentication API

var userHandler = new UserHandler(logger);

app.MapGet("api/users",
        () =>
        {
            var usersResult = userHandler.GetUsers();

            return usersResult;
        })
    .Produces(Status200OK, responseType: typeof(IEnumerable<Character>))
    .Produces(Status404NotFound,
        contentType: "application/problem+json")
    .Produces(Status500InternalServerError,
        contentType: "application/problem+json")
    .WithTags("Users");

app.MapGet("api/users/{userId}",
    ([FromRoute] int userId) =>
    {
        var usersResult = userHandler.GetUsersById(userId);

        return usersResult;
    })
    .Produces(Status200OK, responseType: typeof(Character))
    .Produces(Status404NotFound,
        contentType: "application/problem+json")
    .Produces(Status500InternalServerError,
        contentType: "application/problem+json")
    .WithTags("Users");

app.MapPost("api/users/search",
    (NameSearchParameters? bodyParams) =>
    {
        var usersResult = bodyParams is null
            ? userHandler.GetUsers()
            : userHandler.GetUsersByName(bodyParams.FullName);

        return usersResult;
    })
    .Produces(Status200OK, responseType: typeof(Character))
    .Produces(Status404NotFound,
        contentType: "application/problem+json")
    .Produces(Status500InternalServerError,
        contentType: "application/problem+json")
    .WithTags("Users");

#endregion

app.Run();
