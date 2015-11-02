using System;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;

namespace Rhino.Tests
{
    [TestFixture]
    public class MockingDelegates
    {
        [Test]
        public void GenericDelagate()
        {
            //Arrange
            var mocks = new MockRepository();
            Action<int> action = mocks.StrictMock<Action<int>>();
            for (int i = 0; i < 10; i++)
            {
                action(i);
            }
            mocks.ReplayAll();
            //Act
            ForEachFromZeroToNine(action);
            //Assert
            mocks.VerifyAll();

        }

        [Test]
        public void MockingDelegate()
        {
            //Arrange
            var mocks = new MockRepository();
            var stubMapper = mocks.Stub<Func<int, int>>();
            var expectedResult = 1234;
            stubMapper.Stub(x => x(10)).Return(expectedResult);
            var someClass = new SomeClass(stubMapper);
            mocks.ReplayAll();
            //Act
            var result = someClass.DoSomething(10);
            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
            mocks.VerifyAll();

        }

        private void ForEachFromZeroToNine(Action<int> action)
        {
            for (int i = 0; i < 10; i++)
            {
                action(i);
            }
        }
    }

    public class SomeClass
    {
        private readonly Func<int, int> _stubMapper;

        public SomeClass(Func<int, int> stubMapper)
        {
            _stubMapper = stubMapper;
        }

        public int DoSomething(int value)
        {
            return _stubMapper(value);
        }
    }
}