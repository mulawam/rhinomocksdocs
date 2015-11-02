using System;
using NUnit.Framework;
using Rhino.Mocks;

namespace Rhino.Tests
{
    [TestFixture]
    public class UserTest
    {
        private const string UserName = "ayende";

        [Test]
        public void When_user_forgot_password_should_save_user()
        {
            var stubUserRepository = MockRepository.GenerateStub<IUserRepository>();
            var stubSmsSender = MockRepository.GenerateStub<ISmsSender>();
            var theUser = new User {HashedPassword = "This is not hashed password"};

            stubUserRepository.Stub(x => x.GetUserByName(UserName)).Return(theUser);

            var controllerUnderTest = new LoginController(stubUserRepository, stubSmsSender);
            controllerUnderTest.ForgotMyPassword(UserName);
            
            stubUserRepository.AssertWasCalled(x=>x.GetUserByName(UserName));
        }

        [Test]
        public void How_to_Assert_That_a_method_calls_an_expected_method_with_value()
        {
            //Arrange
            var foo = new Foo {Name = "rhino-mocks"};
            var mockFooRepository = MockRepository.GenerateStub<IFooRepository>();
            var fooService = new FooService(mockFooRepository);
            //Act
            fooService.LookupFoo(foo);
            //Assert
            mockFooRepository.AssertWasCalled(r=>r.GetFooByName(Arg<String>.Matches(y=>y.Equals(foo.Name))));
        }

        [Test]
        public void How_to_Stub_out_your_own_value_of_ReadOnlyProperty()
        {
            //Arrange
            var foo = MockRepository.GenerateStub<IFoo>();
            foo.Stub(x => x.ID).Return(123);
            //Act
            var id = foo.ID;
            //Assert

            Assert.That(id, Is.EqualTo(123));
        }


    }
}