using System;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;

namespace Rhino.Tests
{
    [TestFixture]
    public class EventsTests
    {
        [Test]
        public void Method()
        {
            //Arrange
            var mocks = new MockRepository();
            IWithEvents events = mocks.Stub<IWithEvents>();
            With.Mocks(mocks).Expecting(delegate()
            {
               events.Blah += new EventHandler(events_Blah); 
            })
            .Verify(delegate()
            {
                MethodUnderTest(events);
            });
            //Act

            //Assert

        }

        [Test]
        public void VerifyingThatEventWasFired()
        {
            //Arrange
            var mocks = new MockRepository();
            IEventSubscriber eventSubscriber = mocks.StrictMock<IEventSubscriber>();
            IWithEvents events = new WithEvents();
            events.Blah += eventSubscriber.OnBlah;
            eventSubscriber.OnBlah(events, EventArgs.Empty);
            mocks.ReplayAll();
            //Act
            events.RaiseEvent();
            //Assert
            mocks.VerifyAll();

        }

        private void MethodUnderTest(IWithEvents events)
        {
            events.Blah += new EventHandler(events_Blah);
        }

        private void events_Blah(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public interface IEventSubscriber
    {
        void OnBlah(object source, EventArgs args);
    }

    public interface IWithEvents
    {
        event EventHandler Blah;
        void RaiseEvent();
    }

    public class WithEvents : IWithEvents
    {
        public event EventHandler Blah;
        public void RaiseEvent()
        {
            if (Blah != null)
            {
                Blah(this, EventArgs.Empty);
            }
        }
    }
}