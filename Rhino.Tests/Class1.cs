using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhino.Tests
{
    public interface IFoo
    {
        int ID { get; }
    }

    public class FooService
    {
        private readonly IFooRepository _mockFooRepository;

        public FooService(IFooRepository mockFooRepository)
        {
            _mockFooRepository = mockFooRepository;
        }

        public void LookupFoo(Foo foo)
        {
            _mockFooRepository.GetFooByName(foo.Name);
        }
    }

    public interface IFooRepository
    {
        void GetFooByName(string fooName);
    }

    public class Foo
    {
        public string Name { get; set; }
    }

    public class LoginController
    {
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository, ISmsSender smsSender)
        {
            _userRepository = userRepository;
        }

        public void ForgotMyPassword(string userName)
        {
            var user = _userRepository.GetUserByName(userName);
        }
    }

    public class User
    {
        public string HashedPassword { get; set; }
    }

    public interface ISmsSender
    {
    }

    public interface IUserRepository
    {
        User GetUserByName(string user);
    }
}
