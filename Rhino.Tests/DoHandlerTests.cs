using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;

namespace Rhino.Tests
{
    [TestFixture]
    public class DoHandlerTests
    {
        [Test]
        public void ReturnFormalName()
        {
            //Arrange
            var mocks = new MockRepository();
            var nameSource = mocks.StrictMock<INameSource>();
            Expect.Call(nameSource.CreateName(null, null)).IgnoreArguments().Do(new NameSourceDelegate(formal));
            mocks.ReplayAll();
            //Act
            Speaker speaker = new Speaker("Samuel", "Mulawa", nameSource);
            string actual = speaker.Introduce();
            //Assert
            mocks.VerifyAll();
            Assert.That(actual, Is.EqualTo("Hi, my name is Mr Samuel Mulawa"));

        }

        private string formal(string firstname, string surname)
        {
            return string.Format("Mr {0} {1}", firstname, surname);
        }

        public delegate string NameSourceDelegate(string firstName, string surname);
    }

    public class Speaker
    {
        private readonly string _firstName;
        private readonly string _surname;
        private readonly INameSource _nameSource;

        public Speaker(string firstName, string surname, INameSource nameSource)
        {
            _firstName = firstName;
            _surname = surname;
            _nameSource = nameSource;
        }

        public string Introduce()
        {
            string name = _nameSource.CreateName(_firstName, _surname);
            return string.Format("Hi, my name is {0}",name);
        }
    }

    public interface INameSource
    {
        string CreateName(string firstName, string surname);
    }
}