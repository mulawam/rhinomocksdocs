using System.Collections;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;

namespace Rhino.Tests
{
    [TestFixture]
    public class MockingClassesTests
    {
        [Test]
        public void StubTheClass()
        {
            //Arrange
            var mocks = new MockRepository();
            ArrayList list = mocks.Stub<ArrayList>();
            mocks.ReplayAll();
            //Act

            //Assert
            mocks.VerifyAll();
            Assert.AreEqual(0, list.Capacity);

        }

        [Test]
        public void MockClassProperty()
        {
            //Arrange
            var mocks = new MockRepository();
            ArrayList list = mocks.StrictMock<ArrayList>();
            Expect.Call(list.Capacity).Return(10);
            mocks.ReplayAll();
            //Act
            int capacity = list.Capacity;
            //Assert
            mocks.VerifyAll();
            Assert.AreEqual(10, capacity);
            
        }

        [Test]
        public void ParametersPassed()
        {
            //Arrange
            var mocks = new MockRepository();
            var list = mocks.PartialMock<ArrayList>(500);
            
            mocks.ReplayAll();
            //Act
            Assert.AreEqual(500, list.Capacity);
            //Assert
            mocks.VerifyAll();

        }
    }
}