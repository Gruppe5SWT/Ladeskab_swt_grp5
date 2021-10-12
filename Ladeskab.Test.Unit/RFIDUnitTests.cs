using Ladeskab.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Test.Unit
{
    public class RFIDUnitTests
    {
        private RFID _uut;
        private RFIDDetectedEventArgs _receivedEventArgs;

        [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;
            _uut = new RFID();

            _uut.RFIDDetectedEvent += (o, args) =>
            {
                _receivedEventArgs = args;
            };
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(1)]
        [TestCase(123)]
        [TestCase(-123)]
        public void SetRFID_RFIDDetected_EventFired(int ID)
        {
            _uut.SetRFID(ID);
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(1)]
        [TestCase(123)]
        [TestCase(-123)]
        public void SetRFID_RFIDDetected_CorrectEventArgs(int ID)
        {
            _uut.SetRFID(ID);
            Assert.That(_receivedEventArgs.RFID, Is.EqualTo(ID));
        }
    }
}