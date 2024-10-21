using Microsoft.AspNetCore.Http;
using System.Net;
using static ProblemDetailsApiDemo.Futurama.Shared.ProblemDetails.ProblemBundler;
using static System.Int32;

namespace ProblemDetailsApiDemo.Futurama.Shared.Testing
{
    internal static class DevTesting
    {
        // For testing of response to codes which are hard to set up or re-create
        internal static IResult? TryNumberAsStatusCode(object value)
        {
            int? statusCode = value switch
            {
                int num => num,
                string str when TryParse(str, out var strInt) => strInt,
                _ => null
            };

            if (statusCode is null
                || !Enum.IsDefined(typeof(HttpStatusCode), statusCode))
                return null;

            var problemDetails =
                BundleProblemDetails((HttpStatusCode)statusCode);

            return Results.Problem(problemDetails);
        }
    }
}
