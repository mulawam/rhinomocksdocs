using System;
using NUnit.Framework;
using Rhino.Mocks;
using RMC = Rhino.Mocks.Constraints;

namespace Rhino.Tests
{
    [TestFixture, Ignore("Just typing to remember")]
    public class InterfaceOptionsTests
    {
        [Test]
        public void Method()
        {
            //Arrange
            var mocks = new MockRepository();
            IProjectView view = mocks.StrictMock<IProjectView>();
            Expect.Call(view.Ask(null, null)).Return(null);
            Expect.Call(view.Ask(null, null)).Throw(new Exception());
            Expect.Call(view.Ask(null, null)).Return(null).IgnoreArguments();
            Expect.Call(view.Ask(null, null)).Return(null).Repeat.Twice();
            Expect.Call(view.Ask(null, null))
                .Return(null)
                .Constraints(RMC.Text.Contains("Some"), RMC.Text.Contains("abc"));
            Expect.Call(view.Ask(null, null)).Return(null).Callback(new Func<object, object, bool>(VerifyArguments));
            Expect.Call(view.Name).PropertyBehavior();
            Expect.Call(view.Ask(null, null)).Do(new Func<object,object, bool>((aa, bb) => true)).IgnoreArguments();
            mocks.ReplayAll();
            //Act

            //Assert
            mocks.VerifyAll();

        }

        private bool VerifyArguments(object a, object b)
        {
            return true;
        }
    }
}