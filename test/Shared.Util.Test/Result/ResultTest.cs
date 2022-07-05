using NUnit.Framework;
using System;

namespace Shared.Util.Test.Result
{
    [TestFixture]
    public class ResultTest
    {
        [Test]
        public void CreateResultOK()
        {
            var result = Shared.Util.Result.Result.Ok();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void CreateResultOKCorrelationId()
        {
            var correlationId = Guid.NewGuid().ToString();
            var result = Shared.Util.Result.Result.Ok(correlationId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.CorrelationId);
            Assert.IsTrue(correlationId == result.CorrelationId);
        }

        [Test]
        public void CreateResultFail()
        {
            var result = Util.Result.Result.Fail("Error");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsFailure);
            Assert.IsNotEmpty(result.Error);
        }

        [Test]
        public void CreateResultFailWithCorrelationId()
        {
            var correlationId = Guid.NewGuid().ToString();
            var result = Util.Result.Result.Fail("Error", correlationId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsFailure);
            Assert.IsNotEmpty(result.Error);
        }

        [Test]
        public void CreateResultFailError()
        {
            Assert.Throws<ArgumentNullException>(delegate { Util.Result.Result.Fail(null); });
        }

        [Test]
        public void CreateResultOKObject()
        {
            var obj = new { Id = 1, Name = "Smarkets" };
            var result = Shared.Util.Result.Result.Ok<object>(obj);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Data);
            Assert.True(result.Data.Equals(obj));
        }

        [Test]
        public void CreateResultOKObjectWithCorrelationId()
        {
            var obj = new { Id = 1, Name = "Smarkets" };
            var correlationId = Guid.NewGuid().ToString();
            var result = Shared.Util.Result.Result.Ok<object>(obj, correlationId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Data);
            Assert.True(result.Data.Equals(obj));
        }


        [Test]
        public void CreateResultFailObject()
        {
            var result = Util.Result.Result.Fail<object>("Error");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsFailure);
            Assert.IsNotEmpty(result.Error);
        }

        [Test]
        public void CreateResultFailObjectWithCorrelationId()
        {
            var correlationId = Guid.NewGuid().ToString();
            var result = Util.Result.Result.Fail<object>("Error", correlationId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsFailure);
            Assert.IsNotEmpty(result.Error);
        }

        [Test]
        public void CreateResultFailObjectError()
        {
            Assert.Throws<ArgumentNullException>(delegate { Util.Result.Result.Fail<object>(null); });
        }

        [Test]
        public void CreateResultFailObjectErrorWithCorrelationId()
        {

            var correlationId = Guid.NewGuid().ToString();
            Assert.Throws<ArgumentNullException>(delegate { Util.Result.Result.Fail<object>(null, correlationId); });
        }

        [Test]
        public void CreateResultOkObjectWithErrorClass()
        {
            var obj = new { Id = 1, Name = "Smarkets" };
            var result = Shared.Util.Result.Result.Ok<object, object>(obj);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.True(result.Data.Equals(obj));
            Assert.Throws<InvalidOperationException>(delegate { Assert.IsNull(result.Error); });
        }

        [Test]
        public void CreateResultOkObjectWithErrorClassWithCorrelationId()
        {
            var correlationId = Guid.NewGuid().ToString();
            var obj = new { Id = 1, Name = "Smarkets" };
            var result = Shared.Util.Result.Result.Ok<object, object>(obj, correlationId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.True(result.Data.Equals(obj));
            Assert.Throws<InvalidOperationException>(delegate { Assert.IsNull(result.Error); });
            Assert.IsNotNull(result.CorrelationId);
            Assert.AreEqual(result.CorrelationId, correlationId);
        }

        [Test]
        public void CreateResultErrorObjectWithErrorClass()
        {
            var obj = new { Id = 1, Name = "Smarkets" };
            var result = Shared.Util.Result.Result.Fail<object, object>(obj);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsFailure);
            Assert.True(result.Error.Equals(obj));
            Assert.Throws<InvalidOperationException>(delegate { Assert.IsNull(result.Data); });
        }

        [Test]
        public void CreateResultErrorObjectWithErrorClassWithCorrelationId()
        {
            var obj = new { Id = 1, Name = "Smarkets" };
            var correlationId = Guid.NewGuid().ToString();
            var result = Shared.Util.Result.Result.Fail<object, object>(obj, correlationId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsFailure);
            Assert.True(result.Error.Equals(obj));
            Assert.Throws<InvalidOperationException>(delegate { Assert.IsNull(result.Data); });
        }

        [Test]
        public void CreateResultOKSetCorrelationId()
        {
            var correlationId = Guid.NewGuid().ToString();
            var result = Shared.Util.Result.Result.Ok();
            result.SetCorrelationId(correlationId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.CorrelationId);
            Assert.AreEqual(result.CorrelationId, correlationId);
        }

        [Test]
        public void CreateResultFailSetCorrelationId()
        {
            var correlationId = Guid.NewGuid().ToString();
            var result = Util.Result.Result.Fail("Error");
            result.SetCorrelationId(correlationId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsFailure);
            Assert.IsNotEmpty(result.Error);
            Assert.IsNotNull(result.CorrelationId);
            Assert.AreEqual(result.CorrelationId, correlationId);
        }

        [Test]
        public void CreateResultOKObjectSetCorrelationId()
        {
            var correlationId = Guid.NewGuid().ToString();
            var obj = new { Id = 1, Name = "Smarkets" };
            var result = Shared.Util.Result.Result.Ok<object>(obj);
            result.SetCorrelationId(correlationId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Data);
            Assert.True(result.Data.Equals(obj));
            Assert.IsNotNull(result.CorrelationId);
            Assert.AreEqual(result.CorrelationId, correlationId);
        }

        [Test]
        public void CreateResultFailObjectSetCorrelationId()
        {
            var correlationId = Guid.NewGuid().ToString();
            var result = Util.Result.Result.Fail<object>("Error");
            result.SetCorrelationId(correlationId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsFailure);
            Assert.IsNotEmpty(result.Error);
            Assert.IsNotNull(result.CorrelationId);
            Assert.AreEqual(result.CorrelationId, correlationId);
        }

        [Test]
        public void CreateCombineResultOk()
        {
            var correlationId = Guid.NewGuid().ToString();
            var result1 = Util.Result.Result.Ok();
            var result2 = Util.Result.Result.Ok();
            var result = Util.Result.Result.Combine(correlationId, result1, result2);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.CorrelationId);
            Assert.AreEqual(result.CorrelationId, correlationId);
        }

        [Test]
        public void CreateCombineResultOkWithObject()
        {
            var obj = new { Id = 1, Name = "Smarkets" };
            var obj2 = new { Id = 1, Name = "Smarkets" };
            var correlationId = Guid.NewGuid().ToString();
            var result1 = Util.Result.Result.Ok<object>(obj);
            var result2 = Util.Result.Result.Ok<object>(obj2);
            var result = Util.Result.Result.Combine<object>(correlationId, result1, result2);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.CorrelationId);
            Assert.AreEqual(result.CorrelationId, correlationId);
        }

        [Test]
        public void CreateCombineResultFail()
        {
            string error1 = "Error 1.";
            string error2 = "Error 2.";
            var correlationId = Guid.NewGuid().ToString();
            var result1 = Util.Result.Result.Fail(error1);
            var result2 = Util.Result.Result.Fail(error2);
            var result = Util.Result.Result.Combine(correlationId, result1, result2);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsFailure);
            Assert.IsNotNull(result.CorrelationId);
            Assert.AreEqual(result.CorrelationId, correlationId);
            Assert.IsTrue(result.Error.Contains(error1));
            Assert.IsTrue(result.Error.Contains(error2));
        }

        [Test]
        public void CreateCombineResultFailWithObject()
        {
            string error1 = "Error 1.";
            string error2 = "Error 2.";
            var correlationId = Guid.NewGuid().ToString();
            var result1 = Util.Result.Result.Fail<object>(error1);
            var result2 = Util.Result.Result.Fail<object>(error2);
            var result = Util.Result.Result.Combine<object>(correlationId, result1, result2);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsFailure);
            Assert.IsNotNull(result.CorrelationId);
            Assert.AreEqual(result.CorrelationId, correlationId);
            Assert.IsTrue(result.Error.Contains(error1));
            Assert.IsTrue(result.Error.Contains(error2));
        }

        [Test]
        public void CreateCombineResultFailWithObjectWithSeparator()
        {
            string error1 = "Error 1.";
            string error2 = "Error 2.";
            string separator = ";";
            var correlationId = Guid.NewGuid().ToString();
            var result1 = Util.Result.Result.Fail<object>(error1);
            var result2 = Util.Result.Result.Fail<object>(error2);
            var result = Util.Result.Result.Combine<object>(separator, correlationId, result1, result2);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsFailure);
            Assert.IsNotNull(result.CorrelationId);
            Assert.AreEqual(result.CorrelationId, correlationId);
            Assert.IsTrue(result.Error.Contains(error1));
            Assert.IsTrue(result.Error.Contains(error2));
            Assert.IsTrue(result.Error.Contains(separator));
        }

        [Test]
        public void CreateCombineResultFailWithResultOkWithObjectOneErrorMessage()
        {
            string error1 = "Error 1.";
            var obj = new { Id = 1, Name = "Smarkets" };
            var correlationId = Guid.NewGuid().ToString();
            var result1 = Util.Result.Result.Fail<object>(error1);
            var result2 = Util.Result.Result.Ok<object>(obj);
            var result = Util.Result.Result.Combine<object>(correlationId, result1, result2);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsFailure);
            Assert.IsNotNull(result.CorrelationId);
            Assert.AreEqual(result.CorrelationId, correlationId);
            Assert.IsTrue(result.Error.Contains(error1));
        }

        [Test]
        public void CreateCombineResultFailWithResultOkWithObjectTwoErrorMessages()
        {
            string error1 = "Error 1.";
            string error2 = "Error 2.";
            var obj = new { Id = 1, Name = "Smarkets" };
            var correlationId = Guid.NewGuid().ToString();
            var result1 = Util.Result.Result.Fail<object>(error1);
            var result2 = Util.Result.Result.Ok<object>(obj);
            var result3 = Util.Result.Result.Fail<object>(error2);
            var result = Util.Result.Result.Combine<object>(correlationId, result1, result2, result3);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsFailure);
            Assert.IsNotNull(result.CorrelationId);
            Assert.AreEqual(result.CorrelationId, correlationId);
            Assert.IsTrue(result.Error.Contains(error1));
            Assert.IsTrue(result.Error.Contains(error2));
        }

        [Test]
        public void CreateCombineResultFailWithResultOkWithObjectTwoErrorMessagesWithSeparator()
        {
            string error1 = "Error 1.";
            string error2 = "Error 2.";
            string separator = ";";
            var obj = new { Id = 1, Name = "Smarkets" };
            var correlationId = Guid.NewGuid().ToString();
            var result1 = Util.Result.Result.Fail<object>(error1);
            var result2 = Util.Result.Result.Ok<object>(obj);
            var result3 = Util.Result.Result.Fail<object>(error2);
            var result = Util.Result.Result.Combine<object>(separator, correlationId, result1, result2, result3);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsFailure);
            Assert.IsNotNull(result.CorrelationId);
            Assert.AreEqual(result.CorrelationId, correlationId);
            Assert.IsTrue(result.Error.Contains(error1));
            Assert.IsTrue(result.Error.Contains(error2));
            Assert.IsTrue(result.Error.Contains(separator));
        }


        [Test]
        public void CreateFirstFailureOrSuccessResultFailWithResultOkOneErrorMessage()
        {
            string error1 = "Error 1.";
            var obj = new { Id = 1, Name = "Smarkets" };
            var correlationId = Guid.NewGuid().ToString();
            var result1 = Util.Result.Result.Fail<object>(error1);
            var result2 = Util.Result.Result.Ok<object>(obj);
            var result = Util.Result.Result.FirstFailureOrSuccess(correlationId, result1, result2);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsFailure);
            Assert.IsNotNull(result.CorrelationId);
            Assert.AreEqual(result.CorrelationId, correlationId);
            Assert.AreEqual(error1, result.Error);
        }

        [Test]
        public void CreateFirstFailureOrSuccessResultFailWithResultOk()
        {
            var correlationId = Guid.NewGuid().ToString();
            var result1 = Util.Result.Result.Ok();
            var result2 = Util.Result.Result.Ok();
            var result = Util.Result.Result.FirstFailureOrSuccess(correlationId, result1, result2);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.CorrelationId);
            Assert.AreEqual(result.CorrelationId, correlationId);
        }

    }
}
