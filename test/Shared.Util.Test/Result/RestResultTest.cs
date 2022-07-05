using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;
using Shared.Util.Extension;
using System;
using System.Linq;

namespace Shared.Util.Test.Result
{
    [TestFixture]
    public class RestResultTest
    {
        [Test]
        public void CreateRestResultOK()
        {
            var restResult = Util.Result.RestResult.Ok();

            Assert.IsNotNull(restResult);
            Assert.IsTrue(restResult.Success);
        }

        [Test]
        public void CreateRestResultOKWihtCorrelationId()
        {
            var correlationId = Guid.NewGuid().ToString();
            var restResult = Util.Result.RestResult.Ok(correlationId);

            Assert.IsNotNull(restResult);
            Assert.IsTrue(restResult.Success);
            Assert.AreEqual(correlationId, restResult.CorrelationId);
        }

        [Test]
        public void CreateRestResultFail()
        {
            string error = "Error 1.";
            var restResult = Util.Result.RestResult.Fail(error);

            Assert.IsNotNull(restResult);
            Assert.IsFalse(restResult.Success);
            Assert.NotNull(restResult.Errors);
            Assert.Greater(restResult.Errors.Count, 0);
            Assert.AreEqual(error, restResult.Errors.FirstOrDefault());
        }

        [Test]
        public void CreateRestResultFailWihtCorrelationId()
        {
            var correlationId = Guid.NewGuid().ToString();
            string error = "Error 1.";
            var restResult = Util.Result.RestResult.Fail(error, correlationId);

            Assert.IsNotNull(restResult);
            Assert.IsFalse(restResult.Success);
            Assert.AreEqual(correlationId, restResult.CorrelationId);
            Assert.Greater(restResult.Errors.Count, 0);
            Assert.AreEqual(error, restResult.Errors.FirstOrDefault());
        }

        [Test]
        public void CreateRestResultExceptionWithCorrelationId()
        {
            var correlationId = Guid.NewGuid().ToString();
            var exceptionMessage = "Exception";
            var restResult = Util.Result.RestResult.Create(new Exception(exceptionMessage), correlationId);

            Assert.IsNotNull(restResult);
            Assert.IsFalse(restResult.Success);
            Assert.Greater(restResult.Errors.Count, 0);
            Assert.AreEqual(exceptionMessage, restResult.Errors.FirstOrDefault());
            Assert.AreEqual(correlationId, restResult.CorrelationId);
        }

        [Test]
        public void CreateRestResultInternalServerError()
        {
            var exceptionMessage = "Exception";
            var restResult = Util.Result.RestResult.CreateInternalServerError(exceptionMessage);

            Assert.IsNotNull(restResult);
            Assert.IsFalse(restResult.Success);
            Assert.Greater(restResult.Errors.Count, 0);
            Assert.AreEqual(exceptionMessage, restResult.Errors.FirstOrDefault());
        }

        [Test]
        public void CreateRestResultInternalServerErrorWithCorrelationId()
        {
            var correlationId = Guid.NewGuid().ToString();
            var exceptionMessage = "Exception";
            var restResult = Util.Result.RestResult.CreateInternalServerError(exceptionMessage, correlationId);

            Assert.IsNotNull(restResult);
            Assert.IsFalse(restResult.Success);
            Assert.Greater(restResult.Errors.Count, 0);
            Assert.AreEqual(exceptionMessage, restResult.Errors.FirstOrDefault());
            Assert.AreEqual(correlationId, restResult.CorrelationId);
        }

        [Test]
        public void CreateRestResultExceptionWihtCorrelationId()
        {
            var correlationId = Guid.NewGuid().ToString();
            var exceptionMessage = "Exception";
            var restResult = Util.Result.RestResult.Create(new Exception(exceptionMessage), correlationId);

            Assert.IsNotNull(restResult);
            Assert.IsFalse(restResult.Success);
            Assert.Greater(restResult.Errors.Count, 0);
            Assert.AreEqual(exceptionMessage, restResult.Errors.FirstOrDefault());
            Assert.AreEqual(correlationId, restResult.CorrelationId);
        }

        [Test]
        public void CreateRestResultModelStateDictionary()
        {
            ModelStateDictionary keyValuePairs = new ModelStateDictionary();
            var restResult = Util.Result.RestResult.Create(keyValuePairs);

            Assert.IsNotNull(restResult);
            Assert.IsTrue(restResult.Success);
            Assert.Less(restResult.Errors.Count, 1);
        }

        [Test]
        public void CreateRestResultModelStateDictionaryWihtCorrelationId()
        {
            var correlationId = Guid.NewGuid().ToString();
            ModelStateDictionary keyValuePairs = new ModelStateDictionary();
            var restResult = Util.Result.RestResult.Create(keyValuePairs, correlationId);

            Assert.IsNotNull(restResult);
            Assert.IsTrue(restResult.Success);
            Assert.Less(restResult.Errors.Count, 1);
            Assert.AreEqual(correlationId, restResult.CorrelationId);
        }

        [Test]
        public void CreateRestResultModelStateDictionaryError()
        {
            string key = "key";
            string message = "message";
            ModelStateDictionary keyValuePairs = new ModelStateDictionary();
            keyValuePairs.AddModelError(key, message);
            var restResult = Util.Result.RestResult.Create(keyValuePairs);

            Assert.IsNotNull(restResult);
            Assert.IsFalse(restResult.Success);
            Assert.Greater(restResult.Errors.Count, 0);
            Assert.AreEqual(message, restResult.Errors.FirstOrDefault());
        }

        [Test]
        public void CreateRestResultModelStateDictionaryErrorWihtCorrelationId()
        {
            var correlationId = Guid.NewGuid().ToString();
            string key = "key";
            string message = "message";
            ModelStateDictionary keyValuePairs = new ModelStateDictionary();
            keyValuePairs.AddModelError(key, message);
            var restResult = Util.Result.RestResult.Create(keyValuePairs, correlationId);

            Assert.IsNotNull(restResult);
            Assert.IsFalse(restResult.Success);
            Assert.Greater(restResult.Errors.Count, 0);
            Assert.AreEqual(message, restResult.Errors.FirstOrDefault());
            Assert.AreEqual(correlationId, restResult.CorrelationId);
        }

        [Test]
        public void CreateRestResultPagedList()
        {
            PagedList<Object> pagedList = new PagedList<Object>(FizzWare.NBuilder.Builder<Object>.CreateListOfSize(10).Build().ToList(), 0, 10, 100, true);
            var restResult = Util.Result.RestResult.Create<Object>(pagedList);

            Assert.IsNotNull(restResult);
            Assert.IsTrue(restResult.Success);
            Assert.AreEqual(restResult.Errors.Count, 0);
        }

        [Test]
        public void CreateRestResultPagedListWihtCorrelationId()
        {
            var correlationId = Guid.NewGuid().ToString();
            PagedList<Object> pagedList = new PagedList<Object>(FizzWare.NBuilder.Builder<Object>.CreateListOfSize(10).Build().ToList(), 0, 10, 100, true);
            var restResult = Util.Result.RestResult.Create<Object>(pagedList, correlationId);

            Assert.IsNotNull(restResult);
            Assert.IsTrue(restResult.Success);
            Assert.AreEqual(restResult.Errors.Count, 0);
            Assert.AreEqual(correlationId, restResult.CorrelationId);
        }

        [Test]
        public void CreateRestResultObject()
        {
            var obj = new { Id = 1, Name = "Smarkets" };
            var restResult = Util.Result.RestResult.Create(new Util.Result.RestResult<Object>(obj));

            Assert.IsNotNull(restResult);
            Assert.IsTrue(restResult.Success);
            Assert.AreEqual(restResult.Errors.Count, 0);
            Assert.IsNotNull(((Shared.Util.Result.RestResult<Shared.Util.Result.RestResult<object>>)restResult).Data.Data);
            Assert.AreEqual(obj, ((Shared.Util.Result.RestResult<Shared.Util.Result.RestResult<object>>)restResult).Data.Data);
        }

        [Test]
        public void CreateRestResultObjectWihtCorrelationId()
        {
            var correlationId = Guid.NewGuid().ToString();
            var obj = new { Id = 1, Name = "Smarkets" };
            var restResult = Util.Result.RestResult.Create(new Util.Result.RestResult<Object>(obj), correlationId);

            Assert.IsNotNull(restResult);
            Assert.IsTrue(restResult.Success);
            Assert.AreEqual(restResult.Errors.Count, 0);
            Assert.AreEqual(correlationId, restResult.CorrelationId);
            Assert.IsNotNull(((Shared.Util.Result.RestResult<Shared.Util.Result.RestResult<object>>)restResult).Data.Data);
            Assert.AreEqual(obj, ((Shared.Util.Result.RestResult<Shared.Util.Result.RestResult<object>>)restResult).Data.Data);
        }

        [Test]
        public void CreateRestResultWithResultObject()
        {
            var correlationId = Guid.NewGuid().ToString();
            var obj = new { Id = 1, Name = "Smarkets" };
            var result = Util.Result.Result.Ok<object>(obj, correlationId);
            var restResult = Util.Result.RestResult.Create(result);

            Assert.IsNotNull(restResult);
            Assert.IsTrue(restResult.Success);
            Assert.AreEqual(restResult.Errors.Count, 0);
            Assert.AreEqual(correlationId, restResult.CorrelationId);
            Assert.IsNotNull(((Shared.Util.Result.RestResult<object>)restResult).Data);
            Assert.AreEqual(obj, ((Shared.Util.Result.RestResult<object>)restResult).Data);
        }

        [Test]
        public void CreateRestResultFailWithResultObject()
        {
            var correlationId = Guid.NewGuid().ToString();
            string error = "Error 1.";
            var result = Util.Result.Result.Fail<object>(error, correlationId);
            var restResult = Util.Result.RestResult.Create(result);

            Assert.IsNotNull(restResult);
            Assert.IsFalse(restResult.Success);
            Assert.AreEqual(restResult.Errors.Count, 1);
            Assert.AreEqual(error, restResult.Errors.First());
            Assert.AreEqual(correlationId, restResult.CorrelationId);
        }
    }
}
