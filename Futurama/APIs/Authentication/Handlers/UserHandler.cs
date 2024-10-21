using Microsoft.AspNetCore.Mvc;
using ProblemDetailsApiDemo.Futurama.Shared.Models.Source.Entities;
using static ProblemDetailsApiDemo.Futurama.Shared.DataPaths;
using static ProblemDetailsApiDemo.Futurama.Shared.ProblemDetails.ProblemBundler;
using static System.Net.HttpStatusCode;

#pragma warning disable CA2254

namespace ProblemDetailsApiDemo.Futurama.Apis.Authentication.Handlers;

internal class UserHandler
{
    private readonly ILogger _logger;

    private IList<Character>? Users { get; set; }

    private Dictionary<long, Character>? UsersById { get; set; }

    private Dictionary<string, Character>? UsersByName { get; set; }

    private ProblemDetails? LoadProblem { get; set; }


    public UserHandler(ILogger logger)
    {
        _logger = logger;

        LoadUsers();
    }

    internal IResult GetUsers()
    {
        _logger.LogWarning("UserHandler - Authentication API - Get All Users:");
        _logger.LogDebug($"\t CharactersPath = {CharactersPath}");

        var loadResult = LoadUsers();

        return loadResult.GetType() != Results.Ok().GetType()
            ? loadResult
            : Results.Ok(Users);
    }

    internal IResult GetUsersById(int id)
    {
        _logger.LogWarning("UserHandler - Authentication API - Get Users By Id:");
        _logger.LogDebug($"\t CharactersPath = {CharactersPath}");

        var loadResult = LoadUsers();
        if (loadResult.GetType() != Results.Ok().GetType())
        {
            return loadResult;
        }

        if (!UsersById!.ContainsKey(id))
        {
            var userIdNotFoundProblem =
                BundleProblemDetails(NotFound,
                    "Unrecognized User Id",
                    $"The User Id '{id}' was not found");

            return Results.Problem(userIdNotFoundProblem);
        }

        UsersById.TryGetValue(id, out var user);

        return Results.Ok(user);
    }

    internal IResult GetUsersByName(string fullName)
    {
        _logger.LogWarning("UserHandler - Authentication API - Get Users By Name:");
        _logger.LogDebug($"\t CharactersPath = {CharactersPath}");

        var loadResult = LoadUsers();
        if (loadResult.GetType() != Results.Ok().GetType())
        {
            return loadResult;
        }

        if (UsersByName!.TryGetValue(fullName, out var user))
            return Results.Ok(user);

        var userNameNotFoundProblem =
            BundleProblemDetails(NotFound,
                "Unrecognized User Name",
                $"The User Name '{fullName}' Was Not Found");

        return Results.Problem(userNameNotFoundProblem);
    }

    // HACK Implement better way to load data in memory inline of API
    // TODO Combine Characters and Cast as Users when add reference ids to each
    private IResult LoadUsers()
    {
        _logger.LogWarning("UserHandler - Authentication API - Load Users:");
        _logger.LogDebug($"\t CharactersPath = {CharactersPath}");

        if (Users != null && UsersByName != null)
            return Results.Ok();

        if (LoadProblem is not null)
            return Results.Problem(LoadProblem);

        _logger.LogWarning(
            $"UserHandler - Authentication API: CharactersPath = '{CharactersPath}'");

        if (!Path.Exists(CharactersPath))
        {
            var devMsg = "Unable to find/access local file path '{CharactersPath}'";

            _logger.LogError($"ERROR: {devMsg}");

            LoadProblem = BundleProblemDetails(
                InternalServerError,
                detail: "Unable to Access User File",
                developerMessages: [devMsg]);

            return Results.Problem(LoadProblem);
        }

        try
        {
            var charactersJson = File.ReadAllText(CharactersPath);
            var characters = Character.FromJson(charactersJson);

            Users = characters;
            UsersById = Users?.ToDictionary(user => user.Id);
            UsersByName =
                Users?.ToDictionary(user => user.Name.FullName);

            return Results.Ok();
        }
        catch (Exception exception)
        {
            var devMsg = "Unable to load local file path '{CharactersPath}'";
            _logger.LogError($"ERROR: {devMsg}");
            _logger.LogError($"EXCEPTION: '{exception.Message}'");

            LoadProblem = BundleProblemDetails(
                InternalServerError,
                detail: "Unable to Load User File",
                developerMessages: [devMsg]);

            return Results.Problem(LoadProblem);
        }
    }
}
