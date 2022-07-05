using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Shared.Util.Common.Constants;
using Shared.Util.Result;
using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Smarkets.Catalog.Api
{
    /// <summary>
    /// Middleware responsible for capturing unhandled errors, logging and generating a default error response.
    /// </summary>
    /// <example>
    /// <note type="note">The ***TStartup*** is used to get product name from assembly.</note>
    /// Using the extension class for middlewares
    /// <code>
    /// internal static class MiddlewaresConfigurations
    /// {
    ///     public static IApplicationBuilder UseMiddlewares<![CDATA[<TStartup>]]>(this IApplicationBuilder app, IHostingEnvironment env)
    ///     {
    ///         app.UseMiddleware<![CDATA[<ErrorHandlerMiddleware<TStartup>>]]>();
    ///         return app;
    ///     }
    /// }
    /// </code>
    /// Using the root startup class.
    /// <code>
    /// public class Startup
    /// {
    ///     public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    ///     {
    ///         app.UseMiddleware<![CDATA[<ErrorHandlerMiddleware<TStartup>>]]>();
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <note type="warning">If you use this middleware, you must use ***Smarkets.Framework.DependencyInjection.Extension.AddHostApiLog*** to inject the required dependencies.</note>
    /// <typeparam name="TStartup">Class that start configurations.</typeparam>
    public class ErrorHandlerMiddleware<TStartup>
        where TStartup : class
    {
        private RequestDelegate NextMiddlewareInPipelineDelegate { get; }
        private IConfiguration Configuration { get; }
        private IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// Default contructor for configure middleware.
        /// </summary>
        /// <param name="nextMiddlewareInPipelineDelegate">A function that can process an HTTP request.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        /// <example>
        /// In the host layer into configuration folder, create a new file that's name ***MiddlewaresConfigurations***.
        /// <para>Create a new method as below.</para>
        /// <code>
        /// public static IApplicationBuilder UseMiddlewares<![CDATA[<TStartup>]]>(this IApplicationBuilder app, IHostingEnvironment env)
        /// {
        ///     app.UseMiddleware<![CDATA[<ErrorHandlerMiddleware<TStartup>>]]>();
        /// 
        ///     return app;
        /// }
        /// </code>
        /// To use *appsettings.json* file.
        /// <code>
        /// {
        ///   "Logs": {
        ///     "Error": {
        ///       "Enable": true,
        ///       "Host": "https://domain-of-api-log.com/",
        ///       "Uri": "v1/log",
        ///       "Tags": [ "Foo", "Error", "HostApi" ],
        ///       "SensitiveProperties": [ "password" ],
        ///       "IgnoreException": true,
        ///       "ViewDetailsOnResponse": false
        ///     }
        /// }
        /// </code>
        /// To use configurations in database.
        /// <code>
        /// key = "Logs:Error:Enable"; value = true;
        /// key = "Logs:Error:Host"; value = "https://domain-of-api-log.com/";
        /// </code>
        /// </example>
        /// <exception cref="ArgumentException">If ***Options*** is not configured, an exception is thrown with message <c>'You must enter log settings in ErrorLogOptions.'</c>.</exception>
        public ErrorHandlerMiddleware(RequestDelegate nextMiddlewareInPipelineDelegate, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            NextMiddlewareInPipelineDelegate = nextMiddlewareInPipelineDelegate;
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Method that invokes the next middleware.
        /// </summary>
        /// <param name="context">The HTTPContext for the request.</param>
        /// <returns>A Task that, on completion, indicates the middleware has executed.</returns>
        public async Task Invoke(HttpContext context)
        {
            try { await NextMiddlewareInPipelineDelegate(context); } catch (Exception ex) { await HandleExceptionAsync(context, ex); }
        }

        /// <summary>
        /// Method responsible for catching the exception and logging.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            if (ex is AggregateException)
                ex = ex.InnerException;

            var correlationId = context.Request.Headers[Headers.CorrelationId].ToString();

            if (string.IsNullOrWhiteSpace(correlationId) || !Guid.TryParse(correlationId, out Guid correlationIdParsed))
            {
                correlationId = Guid.NewGuid().ToString("N");

                if (string.IsNullOrWhiteSpace(correlationId))
                    context.Request.Headers.Add(Headers.CorrelationId, correlationId);
                else
                    context.Request.Headers[Headers.CorrelationId] = correlationId;
            }

            var errorMessage = WebHostEnvironment.IsDevelopment() ? ex.ToString() : Messages.ErrorDefault;

            Serilog.Log.Error("Request => Path: {0}, Method: {1}, Host: {2}, Exception: {3}, CorrelationId: {4}", context.Request.Path, context.Request.Method, context.Request.Host, ex.ToString(), correlationId);

            var result = RestResult.CreateInternalServerError(errorMessage, correlationId);
            var jsonResult = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(jsonResult);
        }
    }
}