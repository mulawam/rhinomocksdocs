using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;

namespace Rhino.Tests
{
    [TestFixture]
    public class OrderedUnordered
    {
        [Test]
        public void SaveProjectAs_NewNameWithoutConflicts()
        {
            //Arrange
            
            var mocks = new MockRepository();
            IProjectView projectView = mocks.StrictMock<IProjectView>();
            IProjectRepository repository = mocks.StrictMock<IProjectRepository>();
            
            string newProjectName = "Foo";
            Project prj = new Project("Foo");
            IProjectPresenter projectPresenter = new ProjectPresenter(projectView, repository, prj);
            using (mocks.Ordered())
            {
                Expect.Call(projectView.Title).Return(prj.Name);
                Expect.Call(repository.GetProjectByName(newProjectName)).Return(null);
                projectView.Title = newProjectName;
                projectView.HasChanges = false;
                repository.SaveProject(prj);
            }

            mocks.ReplayAll();
            //Act
            projectPresenter.SaveProjectAs();
            //Assert
            mocks.VerifyAll();

        }

        [Test]
        public void MovingFundsUsingTransaction()
        {
            //Arrange
            var mocks = new MockRepository();
            IDatabaseMapper databaseMapper = mocks.StrictMock<IDatabaseMapper>();
            IBankAccount account1 = mocks.StrictMock<IBankAccount>();
            IBankAccount account2 = mocks.StrictMock<IBankAccount>();
            using (mocks.Ordered())
            {
                Expect.Call(databaseMapper.BeginTransaction()).Return(databaseMapper);
                using (mocks.Unordered())
                {
                    account1.Withdraw(1000);
                    account2.Deposit(1000);
                }
                databaseMapper.Dispose();
            }
            mocks.ReplayAll();
            //Act
            Bank bank = new Bank(databaseMapper);
            bank.TransferFunds(account1, account2, 1000);
            //Assert
            mocks.VerifyAll();

        }
    }

    public class Bank
    {
        private readonly IDatabaseMapper _databaseMapper;

        public Bank(IDatabaseMapper databaseMapper)
        {
            _databaseMapper = databaseMapper;
        }

        public void TransferFunds(IBankAccount account1, IBankAccount account2, int amount)
        {
            _databaseMapper.BeginTransaction();
            account1.Withdraw(amount);
            account2.Deposit(amount);
            _databaseMapper.Dispose();
        }
    }

    public interface IBankAccount
    {
        void Withdraw(int i);
        void Deposit(int i);
    }

    public interface IDatabaseMapper
    {
        IDatabaseMapper BeginTransaction();
        void Dispose();
    }

    public interface IProjectRepository
    {
        Project GetProjectByName(string newProjectName);
        void SaveProject(Project project);
    }
}