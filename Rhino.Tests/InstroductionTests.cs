using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;

namespace Rhino.Tests
{
    [TestFixture]
    public class InstroductionTests
    {
        [Test]
        public void Project_view()
        {
            //Arrange
            var mocks = new MockRepository();
            IProjectView projectView = mocks.StrictMock<IProjectView>();
            Expect.Call(projectView.Title).Return("Project Title");
            mocks.ReplayAll();
            var sut = new ProjectPresenter(projectView);
            //Act
            sut.GetProjectTitle();
            //Assert
            mocks.VerifyAll();

        }

        [Test, Ignore]
        public void SaveProjectAs_CanBeCanceled()
        {
            //Arrange
            var mocks = new MockRepository();
            IProjectView projectView = mocks.StrictMock<IProjectView>();
            Project prj = new Project("Example Project");
            IProjectPresenter presenter = new ProjectPresenter(projectView, prj);
            Expect.Call(projectView.Title).Return(prj.Name);
            //Expect.Call(projectView.Ask(question, asnwer))
            mocks.ReplayAll();
            //Act
            Assert.IsFalse(presenter.SaveProjectAs());
            //Assert
            mocks.VerifyAll();

        }

        [Test]
        [ExpectedException(typeof(ExpectationViolationException))]
        public void MockObjectsThrowsForUnexpectedCall()
        {
            //Arrange
            var mocks = new MockRepository();
            IDemo demo = mocks.StrictMock<IDemo>();
            mocks.ReplayAll();
            //Act
            demo.VoidNoArgs();
            //Assert
            mocks.VerifyAll();

        }

        [Test]
        public void DynamicMockAcceptUnexpectedCall()
        {
            //Arrange
            var mocks = new MockRepository();
            IDemo demo = mocks.DynamicMock<IDemo>();
            mocks.ReplayAll();
            //Act
            demo.VoidNoArgs();
            //Assert
            mocks.VerifyAll();

        }

    }

    public interface IDemo
    {
        void VoidNoArgs();
        int Property1 { get; set; }
    }


    public interface IProjectPresenter
    {
        bool SaveProjectAs();
    }

    public class Project
    {
        public Project(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

    public class ProjectPresenter : IProjectPresenter
    {
        private readonly IProjectRepository _repository;
        private readonly IProjectView _projectView;
        private readonly Project _prj;

        public ProjectPresenter(IProjectView projectView)
        {
            _projectView = projectView;
        }

        public ProjectPresenter(IProjectView projectView, Project prj)
        {
            _projectView = projectView;
            _prj = prj;
        }

        public ProjectPresenter(IProjectView projectView, IProjectRepository repository, Project prj)
            : this(projectView, prj)
        {
            _repository = repository;
        }

        public string GetProjectTitle()
        {
            return _projectView.Title;
        }

        public bool SaveProjectAs()
        {
            var projectName = _projectView.Title;
            Project project = _repository.GetProjectByName(projectName);
            if (project == null)
            {
                _projectView.Title = projectName;
                _projectView.HasChanges = false;
                _repository.SaveProject(_prj);
            }
            return false;
        }

        public void Ask(object o, object o1)
        {
            _projectView.Ask(o, o1);
        }
    }

    public interface IProjectView
    {
        string Title { get; set; }
        bool HasChanges  { get; set; }
        string Ask(object o, object o1);
        string Name { get; set; }
    }
}