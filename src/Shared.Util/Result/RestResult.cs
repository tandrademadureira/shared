using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Shared.Util.Result
{
    /// <summary>
    /// RestResult class containing a generic instance of a desired class. Contains an inheritance from RestResult
    /// </summary>
    /// <typeparam name="T">Generic parameter. Class Reference</typeparam>
    public class RestResult<T> : RestResult
    {
        /// <summary>
        /// Property Data => Generic parameter. Class Reference
        /// </summary>
        public T Data { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="data">Generic parameter. Instance of class reference</param>
        public RestResult(T data)
        {
            Data = data;
        }
    }

    /// <summary>
    /// Class responsible for formatting PageResult<T/> responses to http requests
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResult<T> : RestResult
    {
        /// <summary>
        /// Property Page => An enumerable of a data generic object
        /// </summary>
        public IEnumerable<T> Page { get; }

        /// <summary>
        /// Property Count => Indicates the total number of records we will have in paging the object
        /// </summary>
        public long Count { get; }

        /// <summary>
        /// Property PageCount => Indicates how many pages we will have in the object's pagination
        /// </summary>
        public int PageCount { get; }

        /// <summary>
        /// Property PageSize => Indicates the maximum number of pages we will have in the object's pagination
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Property CurrentPage => Indicates what the current page of the object's pagination will be
        /// </summary>
        public int CurrentPage { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="data">Information of the desired pagination to assemble the PageResult</param>
        public PageResult(PagedList<T> data)
        {
            Page = data.Items;
            Count = data.TotalCount;
            PageCount = data.TotalPages;
            PageSize = data.ItemsPerPage;
            CurrentPage = data.CurrentPage;
        }
    }

    /// <summary>
    /// Class responsible for formatting RestResult responses to http requests
    /// </summary>
    /// <example>
    /// Example method HttpGet in any controller 
    /// <para>Create a new method HttpGet.</para>
    /// <code>
    ///[HttpGet]
    ///public async Task<IActionResult/> Get([FromQuery] GetQuery.GetContract request)
    /// {
    ///    var result = await _mediator.Send(request);
    ///    return RestResult.CreateHttpResponse(result);
    /// }
    /// </code>
    /// </example> 
    public class RestResult
    {
        /// <summary>
        /// Property CorrelationId => Contains the information of a single guid per request, making it possible to track it
        /// </summary>
        public string CorrelationId { get; protected set; }

        /// <summary>
        /// Property Success => Contains information as to whether the request was successful or not.
        /// </summary>
        public bool Success { get; protected set; }

        /// <summary>
        /// Property Errors => Contains a list of errors if the request was not successful.
        /// </summary>
        public IList<string> Errors { get; protected set; }

        /// <summary>
        /// Property StatusCode => Contains the status code information of the request        
        /// </summary>
        [JsonIgnore]
        protected int StatusCode { get; set; }

        /// <summary>
        /// Protected contructor
        /// </summary>
        protected RestResult()
        {
        }

        /// <summary>
        /// Build a standard result for a successful request. StatusCode = 200
        /// </summary>
        /// <returns>Object RestResult indicating success in the request</returns>
        public static RestResult Ok()
        {
            return new RestResult
            {
                Success = true,
                Errors = new List<string>(),
                StatusCode = 200
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="correlationId"></param>
        /// <returns></returns>
        public static RestResult Ok(string correlationId)
        {
            return new RestResult
            {
                Success = true,
                Errors = new List<string>(),
                StatusCode = 200,
                CorrelationId = correlationId
            };
        }

        /// <summary>
        /// Creates a standard result for a fail request. StatusCode = 400
        /// </summary>
        /// <param name="error">Error that caused the failure</param>
        /// <returns>Object RestResult indicating fail in the request</returns>
        public static RestResult Fail(string error)
        {
            return new RestResult
            {
                Success = false,
                Errors = new List<string> { error },
                StatusCode = 400
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static RestResult Fail(string error, string correlationId)
        {
            return new RestResult
            {
                Success = false,
                Errors = new List<string> { error },
                StatusCode = 400,
                CorrelationId = correlationId
            };
        }

        /// <summary>
        /// Creates a standard result for a create exception request. StatusCode = 500
        /// </summary>
        /// <param name="ex">A Exception</param>
        /// <returns>Object RestResult indicating exception in the request</returns>
        public static RestResult CreateInternalServerError(string message)
        {
            return new RestResult
            {
                Success = false,
                Errors = new List<string> { message },
                StatusCode = 500
            };
        }

        /// <summary>
        /// Creates a standard result for a create exception request. StatusCode = 500
        /// </summary>
        /// <param name="ex">A Exception</param>
        /// <returns>Object RestResult indicating exception in the request</returns>
        public static RestResult CreateInternalServerError(string message, string correlationId)
        {
            return new RestResult
            {
                Success = false,
                Errors = new List<string> { message },
                StatusCode = 500,
                CorrelationId = correlationId
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static RestResult Create(Exception ex, string correlationId)
        {
            return new RestResult
            {
                Success = false,
                Errors = new List<string> { ex.Message },
                StatusCode = 500,
                CorrelationId = correlationId
            };
        }

        /// <summary>
        ///  Creates a default result for requests, indicating success or not being processed. StatusCode = 200 (success) or 422 (unprocessable)
        /// </summary>
        /// <param name="dict">ModelStateDictionary to provide error list information.</param>
        /// <returns>Object RestResult indicating success(200) or unprocessable(422) in the request</returns>
        public static RestResult Create(ModelStateDictionary dict)
        {
            return new RestResult
            {
                Success = dict.IsValid,
                Errors = dict.IsValid
                ? new List<string>()
                : dict
                .Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage)
                .ToList(),
                StatusCode = dict.IsValid ? 200 : 422
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static RestResult Create(ModelStateDictionary dict, string correlationId)
        {
            return new RestResult
            {
                Success = dict.IsValid,
                Errors = dict.IsValid
                ? new List<string>()
                : dict
                .Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage)
                .ToList(),
                StatusCode = dict.IsValid ? 200 : 422,
                CorrelationId = correlationId
            };
        }

        /// <summary>
        /// Creates a paged default response for the request. StatusCode = 200
        /// </summary>
        /// <typeparam name="T">Generic parameter. Class Reference</typeparam>
        /// <param name="result">Information of the desired pagination to assemble the RestResult</param>
        /// <returns>Object RestResult paged indicating success in the request</returns>
        public static RestResult Create<T>(PagedList<T> result)
        {
            return new PageResult<T>(result)
            {
                Success = true,
                Errors = new List<string>(),
                StatusCode = 200
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static RestResult Create<T>(PagedList<T> result, string correlationId)
        {
            return new PageResult<T>(result)
            {
                Success = true,
                Errors = new List<string>(),
                StatusCode = 200,
                CorrelationId = correlationId
            };
        }

        /// <summary>
        /// Create a default result for a successful request containing the generic object. StatusCode = 200
        /// </summary>
        /// <typeparam name="T">Generic parameter. Class Reference</typeparam>
        /// <param name="result">Information of the desired object to assemble the RestResult</param>
        /// <returns>Object RestResult indicating success in the request</returns>
        public static RestResult Create<T>(T result)
        {
            return new RestResult<T>(result)
            {
                Success = true,
                Errors = new List<string>(),
                StatusCode = 200
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static RestResult Create<T>(T result, string correlationId)
        {
            return new RestResult<T>(result)
            {
                Success = true,
                Errors = new List<string>(),
                StatusCode = 200,
                CorrelationId = correlationId
            };
        }

        /// <summary>
        /// Create a default result for a successful(200) or unprocessable(422) request containing the generic object. StatusCode = 200 (success) or 422 (unprocessable)
        /// </summary>
        /// <typeparam name="T">Generic parameter. Class Reference</typeparam>
        /// <param name="result">Information of the desired object to assemble the RestResult</param>
        /// <returns>Object RestResult indicating success(200) or unprocessable(422) in the request</returns>
        public static RestResult Create<T>(Result<T> result)
        {
            return new RestResult<T>(result.IsSuccess ? result.Data : default(T))
            {
                CorrelationId = result.CorrelationId,
                Success = result.IsSuccess,
                Errors = result.IsSuccess
                    ? new List<string>()
                    : result.Error
                        .Split(';')
                        .ToList(),
                StatusCode = result.IsSuccess ? 200 : 400
            };
        }

        /// <summary>
        /// Create a default result for a successful(200) or unprocessable(422) request containing generic object information. StatusCode = 200 (success) or 422 (unprocessable)
        /// </summary>
        /// <param name="result">Generic parameter. Generic object</param>
        /// <returns>Request containing generic object information.</returns>
        public static RestResult Create(Result result)
        {
            return new RestResult
            {
                CorrelationId = result.CorrelationId,
                Success = result.IsSuccess,
                Errors = result.IsSuccess
                    ? new List<string>()
                    : result.Error
                        .Split(';')
                        .ToList(),
                StatusCode = result.IsSuccess ? 200 : 400
            };
        }

        /// <summary>
        ///  Exposes the Create method call to format a response to the object ModelStateDictionary converting it to a IActionResult
        /// </summary>
        /// <param name="dict">Object ModelStateDictionary</param>
        /// <returns>RestResult formatted in an http Response</returns>
        public static IActionResult CreateHttpResponse(ModelStateDictionary dict)
        {
            return Create(dict).ToHttp();
        }

        /// <summary>
        /// Exposes the Create method call to format a response to the exception converting it to a IActionResult
        /// </summary>
        /// <param name="ex">A exception</param>
        /// <returns>RestResult formatted in an http Response</returns>
        public static IActionResult CreateHttpResponse(Exception ex)
        {
            return Create(ex).ToHttp();
        }

        /// <summary>
        /// Exposes the Create method call to format a response to the generic object converting it to a IActionResult
        /// </summary>
        /// <typeparam name="T">Generic parameter. Class Reference</typeparam>
        /// <param name="data">Information of the desired object to assemble the RestResult</param>
        /// <returns>RestResult formatted in an http Response</returns>
        public static IActionResult CreateHttpResponse<T>(T data)
        {
            return Create(data).ToHttp();
        }

        /// <summary>
        /// Exposes the Create method call to format a response to the paged generic object converting it to a IActionResult
        /// </summary>
        /// <typeparam name="T">Generic parameter. Class Reference</typeparam>
        /// <param name="data">Paged generic object to assemble the RestResult</param>
        /// <returns>RestResult formatted in an http Response</returns>
        public static IActionResult CreateHttpResponse<T>(PagedList<T> data)
        {
            return Create(data).ToHttp();
        }

        /// <summary>
        /// Exposes the Create method call to format a response to the RestResult informing to assemble the RestResult and converting it to a IActionResult
        /// </summary>
        /// <typeparam name="T">Generic parameter. Class Reference</typeparam>
        /// <param name="data">Instance of RestResult</param>
        /// <returns>RestResult formatted in an http Response</returns>
        public static IActionResult CreateHttpResponse<T>(Result<T> data)
        {
            return Create(data).ToHttp();
        }

        /// <summary>
        /// Exposes the Create method call to format a response to the RestResult instantiating the structure Result and converting it to a IActionResult
        /// </summary>
        /// <param name="data">Instance of RestResult</param>
        /// <returns>RestResult formatted in an http Response</returns>
        public static IActionResult CreateHttpResponse(Result data)
        {
            return Create(data).ToHttp();
        }

        /// <summary>
        /// Exposes the Ok method call to format a success response to the RestResult converting it to a IActionResult
        /// </summary>
        /// <returns>RestResult formatted in an http Response</returns>
        public static IActionResult CreateOkHttpResponse()
        {
            return Ok().ToHttp();
        }

        /// <summary>
        /// Exposes the Fail method call to format a fail response to the RestResult converting it to a IActionResult
        /// </summary>
        /// <param name="error"></param>
        /// <returns>RestResult formatted in an http Response</returns>
        public static IActionResult CreateFailHttpResponse(string error)
        {
            return Fail(error).ToHttp();
        }

        private IActionResult ToHttp()
        {
            return new ObjectResult(this)
            {
                StatusCode = StatusCode
            };
        }
    }
}
