﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MediCore_API.Middleware
{
	internal sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailService, ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
	{
		public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
		{
			logger.LogError(exception, "Unhandled exception");

			httpContext.Response.StatusCode = exception switch
			{
				ApplicationException => StatusCodes.Status400BadRequest,
				_ => StatusCodes.Status500InternalServerError
			};

			return await problemDetailService.TryWriteAsync
			(
				new ProblemDetailsContext
				{
					HttpContext = httpContext,
					Exception = exception,
					ProblemDetails = new ProblemDetails
					{
						Type = exception.GetType().Name,
						Title = "An Error Occured",
						Detail = exception.Message
					}
				}
			);
		}
	}
}
