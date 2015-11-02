using System.Runtime.Remoting.Proxies;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using Rhino.Mocks.Exceptions;
using RM = Rhino.Mocks.Constraints;

namespace Rhino.Tests
{
    [TestFixture]
    public class ConstraintsTests
    {
        [Test]
        public void ConstraintsOnMethodPassedArguments()
        {
            //Arrange
            var mocks = new MockRepository();
            var view = mocks.StrictMock<IProjectView>();
            Expect.Call(view.Ask(null, null))
                .IgnoreArguments()
                .Constraints(RM.Is.Anything(), RM.Is.TypeOf(typeof(string)))
                .Return(null);
            mocks.ReplayAll();
            //Act
            view.Ask(null, "1");
            //Assert
            mocks.VerifyAll();

        }

        [Test]
        public void ConfiguringConstraintsOnPassedArgumentObjects()
        {
            //Arrange
            var mocks = new MockRepository();
            var logger = mocks.StrictMock<IValidationMessageLogger>();
            using (mocks.Record())
            {
                logger.LogMessage(null);
                LastCall.Constraints(new PropertyConstraint("Text", RM.Text.Contains("ABC")) &&
                RM.Property.Value("MessageKind", ValidationMessageKind.Error)
                    ).Repeat.Once();
            } //implicitly calls mocks.ReplayAll();
            //Act
            var obj = new MessageABC{Text = "ABC 1", MessageKind = ValidationMessageKind.Error};
            logger.LogMessage(obj);
            //Assert
            mocks.VerifyAll();

        }
    }

    public class MessageABC
    {
        public string Text { get; set; }
        public ValidationMessageKind MessageKind { get; set; }
    }

    public enum ValidationMessageKind   
    {
         Error, Warning
    }

    public interface IValidationMessageLogger
    {
        void LogMessage(object o);
    }
}