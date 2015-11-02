using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;

namespace Rhino.Tests
{
    [TestFixture]
    public class PartialMock
    {
        [Test]
        public void UsingPartialMock()
        {
            //Arrange
            var mocks = new MockRepository();
            ProcessorBase proc = mocks.PartialMock<ProcessorBase>();
            Expect.Call(proc.Add(1)).Return(1);
            Expect.Call(proc.Add(1)).Return(2);
            mocks.ReplayAll();
            //Act
            proc.Inc();
            Assert.That(proc.Register, Is.EqualTo(1));
            proc.Inc();
            Assert.That(proc.Register, Is.EqualTo(2));
            //Assert
            mocks.VerifyAll();

        }
    }

    public abstract class ProcessorBase
    {
        public int Register;

        public virtual int Inc()
        {
            Register = Add(1);
            return Register;
        }

        public abstract int Add(int i);
    }
}