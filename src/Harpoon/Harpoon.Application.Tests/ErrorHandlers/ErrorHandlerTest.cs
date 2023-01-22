using System.Web;
using NUnit.Framework;
using System;

namespace Harpoon.Application.ErrorHandlers
{
    [TestFixture]
    public class ErrorHandlerTest
    {
        private IErrorHandler errorHandler;

        [SetUp]
        public void SetUp()
        {
            errorHandler = new ErrorHandler();
        }
        
        [Test]
        public void TestDoNothingIfNullException()
        {
            var context = new FakeErrorContext(null);
            errorHandler.Handle(context);

            Assert.IsFalse(context.IsClearError);
            Assert.IsFalse(context.IsRedirected());
        }

        [Test]
        public void TestResourceNotFoundError()
        {
            Exception exception = new HttpException(404, "Page not found");
            AssertResourceNotFound(exception);

            exception = new ActionParameterNullException("param");
            AssertResourceNotFound(exception);

            exception = new ContentNotFoundException("article", 10);
            AssertResourceNotFound(exception);
        }

        private void AssertResourceNotFound(Exception exception)
        {
            const string someUrl = "some-page";
            var context = new FakeErrorContext(exception)
            {
                RequestedUrl = someUrl
            };

            errorHandler.Handle(context);
            
            Assert.IsTrue(context.IsClearError);
            Assert.IsTrue(context.IsRedirected());
            Assert.IsFalse(context.IsErrorSent);
            context.AssertRouteParam("url", someUrl);
            context.AssertRouteController("Error");
            context.AssertRouteAction("NotFound");
        }

        [Test]
        public void TestUnhandledError()
        {
            AssertUnhandledError(new Exception("Unhandled"));
        }
        
        [Test]
        public void TestUnhandledHttpError()
        {
            AssertUnhandledError(new HttpException(500, "Http unhandled"));
        }
        
        private void AssertUnhandledError(Exception exception)
        {
            var context = new FakeErrorContext(exception);
            
            errorHandler.Handle(context);

            Assert.IsTrue(context.IsClearError);
            Assert.IsTrue(context.IsRedirected());
            Assert.IsTrue(context.IsErrorSent);
            context.AssertRouteController("Error");
            context.AssertRouteAction("Internal");
        }

    }
}