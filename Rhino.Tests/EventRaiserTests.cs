using System;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;
using Rhino.Mocks.Interfaces;

namespace Rhino.Tests
{
    [TestFixture]
    public class EventRaiserTests
    {
        [Test]
        public void Method()
        {
            //Arrange
            var mocks = new MockRepository();
            IView view = mocks.StrictMock<IView>();
            view.Load += null;
            LastCall.IgnoreArguments();
            IEventRaiser raiseViewEvent = LastCall.GetEventRaiser();
            mocks.ReplayAll();
            //Act
            var presenter = new Presenter(view);
            raiseViewEvent.Raise(presenter, EventArgs.Empty);
            //Assert
            mocks.VerifyAll();
            Assert.That(presenter.OnLoadCalled);

        }
    }

    public interface IView
    {
        event EventHandler Load;
    }

    public class Presenter
    {
        public bool OnLoadCalled = false;

        public Presenter(IView view)
        {
            view.Load += OnLoad;
        }

        private void OnLoad(object sender, EventArgs eventArgs)
        {
            OnLoadCalled = true;
        }
    }
}