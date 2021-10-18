using System.IO;
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

        [TestCase("14/10/2021 11:43:02", 22)]
        [TestCase("01/01/1921 00:00:22", 123)]
        [TestCase("03/04/2000 15:20:39", 32)]
        public void LogDoorLocked_SaveLockedDateAndId_LoggedCorrectDate(string dateTime, int id)
        {
            _dateTime.getDateTime().Returns(dateTime);

            _uut.LogDoorLocked(id);

            FileStream fs = new FileStream("LogFile.txt", FileMode.OpenOrCreate);
            StreamReader s = new StreamReader(fs);

            string expected = $"{dateTime}: Locked with RFID {id}";

            
            Assert.That(s.ReadToEnd().Contains(expected));

            s.Close();
            fs.Close();
            
        }

        [TestCase("14/10/2021 11:43:02", 22)]
        [TestCase("01/01/1921 00:00:22", 123)]
        [TestCase("03/04/2000 15:20:39", 32)]
        public void LogDoorUnlocked_SaveUnlockedDateAndId_LoggedCorrectDate(string dateTime, int id)
        {
            _dateTime.getDateTime().Returns(dateTime);

            _uut.LogDoorUnlocked(id);

            FileStream fs = new FileStream("LogFile.txt", FileMode.OpenOrCreate);
            StreamReader s = new StreamReader(fs);

            string expected = $"{dateTime}: Unlocked with RFID {id}";

            Assert.That(s.ReadToEnd().Contains(expected));

            s.Close();
            fs.Close();
        }
    }
}
