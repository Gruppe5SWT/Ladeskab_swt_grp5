using Ladeskab.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Test.Unit
{
    public class LogFileUnitTests
    {
        LogFile _uut;
        IDateTime _dateTime;

        [SetUp]
        public void SetUp()
        {
            _dateTime = Substitute.For<IDateTime>();
            _uut = new LogFile(_dateTime);
        }

        [Test]
        public void LogDoorLocked_()
        {

        }
    }
}
