using Ladeskab.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Test.Unit
{
    public class DoorUnitTests
    {
        private Door _uut;
        private DoorStateChangedEventArgs _receivedEventArgs;

        [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;
            _uut = new Door();

            _uut.DoorStateChangedEvent += (o, args) =>
            {
                _receivedEventArgs = args;
            };
        }

        [Test]
        public void OnDoorOpen_DoorStateChanged_EventFired()
        {
            _uut.OnDoorOpen();
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void OnDoorClose_DoorStateChanged_EventFired()
        {
            _uut.OnDoorClose();
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void OnDoorOpen_DoorStateChanged_CorrectEventArgs()
        {
            _uut.OnDoorOpen();
            Assert.That(_receivedEventArgs.Open, Is.True);
        }

        [Test]
        public void OnDoorClose_DoorStateChanged_CorrectEventArgs()
        {
            _uut.OnDoorClose();
            Assert.That(_receivedEventArgs.Open, Is.False);
        }
    }
}