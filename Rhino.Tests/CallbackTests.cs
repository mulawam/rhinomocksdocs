using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;

namespace Rhino.Tests
{
    [TestFixture]
    public class CallbackTests
    {
        [Test]
        public void CallbackCallOnExpectation()
        {
            //Arrange
            var mocks = new MockRepository();
            IProjectRepository repository = mocks.StrictMock<IProjectRepository>();
            IProjectView view = mocks.StrictMock<IProjectView>();
            Expect.Call(view.Ask(null, null)).Callback(new Delegates.Function<bool,object, object> (onAskCallback)).Return(null);
            mocks.ReplayAll();
            //Act
            ProjectPresenter presenter = new ProjectPresenter(view);
            presenter.Ask(null, null);
            //Assert
            mocks.VerifyAll();

        }

        private bool onAskCallback(object arg1, object arg2)
        {
            return true;
        }
    }
}