using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;

namespace Rhino.Tests
{
    [TestFixture]
    public class FluentSyntaxTests
    {
        [Test]
        public void FluentSyntax()
        {
            //Arrange
            var mocks = new MockRepository();
            IProjectView view = mocks.StrictMock<IProjectView>();

            With.Mocks(mocks)
                .Expecting(()=> Expect.Call(view.Ask(null,null)).Return("string"))
                .Verify(()=>view.Ask(null,null));


        }

        [Test]
        public void VeryCompact()
        {
            With.Mocks(delegate()
            {
                var presenter = Mocker.Current.StrictMock<IProjectPresenter>();
                Expect.Call(presenter.SaveProjectAs()).Return(false);
                Mocker.Current.ReplayAll();
                Assert.IsFalse(presenter.SaveProjectAs());
            });

        }
    }
}