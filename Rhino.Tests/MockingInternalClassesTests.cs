using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;
using Rhino.Tests.Internals;

namespace Rhino.Tests
{
    [TestFixture]
    public class MockingInternalClassesTests
    {
        [Test]
        public void Method()
        {
            //Arrange
            var mocks = new MockRepository();
            var cls = mocks.StrictMock<Class1>();
            Expect.Call(cls.TestMethod()).Return("Asia");
            mocks.ReplayAll();
            //Act
            string name = cls.TestMethod();
            //Assert
            mocks.VerifyAll();
            Assert.AreEqual("Asia", name);
        }
    }
}