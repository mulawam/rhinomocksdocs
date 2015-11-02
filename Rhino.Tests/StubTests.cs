using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;

namespace Rhino.Tests
{
    [TestFixture]
    public class StubTests
    {
        [Test]
        public void CreatingAnimalStubWithDynamicMock()
        {
            //Arrange
            var mocks = new MockRepository();
            IAnimal animal = mocks.DynamicMock<IAnimal>();
            Expect.Call(animal.Eyes).PropertyBehavior();
            mocks.ReplayAll();
            //Act
            animal.Eyes = 5;
            animal.Eyes = 2 + animal.Eyes;
            //Assert
            Assert.That(animal.Eyes, Is.EqualTo(7));
            mocks.VerifyAll();

        }

        [Test]
        public void CreatingStub_Stub()
        {
            //Arrange
            var mocks = new MockRepository();
            IAnimal animal = mocks.Stub<IAnimal>();
            mocks.ReplayAll();
            //Act
            animal.Name = "Snoopy";
            //Assert
            Assert.That(animal.Name, Is.EqualTo("Snoopy"));
            mocks.VerifyAll();

        }

        public interface IAnimal
        {
            int Legs { get; set; }

            int Eyes { get; set; }

            string Name { get; set; }
        }
    }
}