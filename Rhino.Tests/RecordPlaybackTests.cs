using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;

namespace Rhino.Tests
{
    [TestFixture]
    public class RecordPlaybackTests
    {
        [Test]
        public void RecordAndPlayback()
        {
            //Arrange
            var mocks = new MockRepository();
            IProjectView view;
            using (mocks.Record())
            {
                view = mocks.StrictMock<IProjectView>();
                Expect.Call(view.Ask(null, null)).Return("string");
            }
            //Act
            dynamic result;
            using (mocks.Playback())
            {
                result = view.Ask(null, null);
            }

            //Assert
            Assert.That(result, Is.EqualTo("string"));

        }
    }
}