using Microsoft.AspNetCore.Mvc;
using System;

namespace Application.Common.Helpers;

public static class ProblemDetailsHelper
{
    public static ProblemDetails CreateProblemDetails(int statusCode, string message)
    {
        return new ProblemDetails
        {
            Status = statusCode,
            Title = GetDefaultTitle(statusCode),
            Detail = message,
            Instance = $"urn:problem:{Guid.NewGuid()}",
            Type = $"https://httpstatuses.com/{statusCode}"
        };
    }

    private static string GetDefaultTitle(int statusCode)
    {
        return statusCode switch
        {
            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not Found",
            500 => "Internal Server Error",
            _ => "Error"
        };
    }
} 