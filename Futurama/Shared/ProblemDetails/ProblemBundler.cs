using Microsoft.AspNetCore.Mvc;
using System.Net;
using static ProblemDetailsApiDemo.Futurama.Shared.ProblemDetails.HttpSemanticsAndContent;
using MvcProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace ProblemDetailsApiDemo.Futurama.Shared.ProblemDetails;

public static class ProblemBundler
{
    #region Fields

    private const string DevelopMessagesKey = "developerMessages";

    private const string UserMessagesKey = "userMessages";

    #endregion

    #region Internal Methods

    public static MvcProblemDetails
        BundleProblemDetails(
            HttpStatusCode statusCode,
            string? title = null,
            string? detail = null,
            string? requestUri = null,
            // Values are string arrays to accommodate Errors property in VPD
            string[]? developerMessages = null,
            string[]? userMessages = null
        )
    {
        var statusCodeInt = (int)statusCode;
        string? type = null;

        if (StatusCodeDetails.TryGetValue(statusCodeInt, out var value))
        {
            title ??= value.Title;
            type = value.Type;
        }
        else
        {
            title = statusCode.ToString();
        }

        var problemDetails = new MvcProblemDetails
        {
            Status = statusCodeInt,
            Title = title,
            Detail = detail,
            Type = type,
            Instance = requestUri
        };

        var extensions = problemDetails.Extensions;
        if (developerMessages != null)
            extensions.Add(DevelopMessagesKey, developerMessages);
        if (userMessages != null)
            extensions.Add(UserMessagesKey, userMessages);

        return problemDetails;
    }

    // TODO Need to pass in errors in addition to developer + user messages?
    public static ValidationProblemDetails
        BundleValidationProblemDetails(
            HttpStatusCode statusCode,
            string? title = null,
            string? detail = null,
            string? requestUri = null,
            string[]? developerMessages = null,
            string[]? userMessages = null
        )
    {
        var problemDetails =
            BundleProblemDetails(statusCode, title, detail,
                requestUri, developerMessages, userMessages);

        var errors = new Dictionary<string, string[]>();
        foreach (var (key, value) in problemDetails.Extensions)
        {
            // TODO Cast other types if convert PD from other sources
            if (value is string[] valueStrings)
            {
                errors.Add(key, valueStrings);
            }
        }

        // HACK VPD should have constructor passing in PD and Errors
        var validationProblemDetails = new ValidationProblemDetails(errors)
        {
            Status = problemDetails.Status,
            Title = problemDetails.Title,
            Detail = problemDetails.Detail,
            Type = problemDetails.Type,
            Instance = problemDetails.Instance
            // Skip PD.Extensions - Errors dictionary is better for messages
        };

        return validationProblemDetails;
    }

    #endregion
}
