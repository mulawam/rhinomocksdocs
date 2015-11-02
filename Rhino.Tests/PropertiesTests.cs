using System.Collections;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;

namespace Rhino.Tests
{
    [TestFixture]
    public class PropertiesTests
    {
        [Test]
        public void CountPropertySetupResult()
        {
            //Arrange
            var mocks = new MockRepository();
            IList list = mocks.DynamicMock<IList>();
            SetupResult.For(list.Count).Return(4);
            mocks.ReplayAll();
            //Act
            int count = list.Count;
            //Assert
            mocks.VerifyAll();
            Assert.That(count, Is.EqualTo(4));
        }

        [Test]
        public void MakePropertyBehaveAsProperty()
        {
            //Arrange
            var mocks = new MockRepository();
            IDemo prep = mocks.StrictMock<IDemo>();
            Expect.Call(prep.Property1).PropertyBehavior();
            mocks.ReplayAll();
            //Act
            prep.Property1 = 1;
            prep.Property1 = 2;
            //Assert
            mocks.VerifyAll();

        }
    }
}