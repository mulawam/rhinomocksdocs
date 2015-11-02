using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;

namespace Rhino.Tests
{
    [TestFixture]
    public class GenericsTests
    {
        [Test]
        public void MockAGenericInterface()
        {
            //Arrange
            var mocks = new MockRepository();
            IList<int> list = mocks.DynamicMock <IList<int>>();
            Assert.That(list, Is.Not.Null);
            Expect.Call(list.Count).Return(5);
            mocks.ReplayAll();
            //Act
            int i = list.Count;
            //Assert
            Assert.That(i, Is.EqualTo(5));
            mocks.VerifyAll();

        }
    }
}