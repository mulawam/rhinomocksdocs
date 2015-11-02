using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;

namespace Rhino.Tests
{
    [TestFixture]
    public class SetupResultTests
    {
        [Test]
        public void MakeSureMethodIsNeverCalled()
        {
            //Arrange
            var mocks = new MockRepository();
            var view = mocks.StrictMock<IProjectView>();
            SetupResult.For(view.Name).Return("Asia");
            Expect.Call(view.Ask(null, null)).Repeat.Never();
            Expect.Call(view.Ask(null, null)).Return(string.Empty).Repeat.Times(0, int.MaxValue);
            mocks.ReplayAll();
            //Act

            //Assert
            mocks.VerifyAll();

        } 
    }
}