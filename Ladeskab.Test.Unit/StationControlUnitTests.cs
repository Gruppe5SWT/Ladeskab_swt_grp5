using Ladeskab.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System.IO;

namespace Ladeskab.Test.Unit
{
    public class StationControlUnitTests
    {
        private StationControl _uut;
        
        IDoor _door;
        IChargeControl _chargeControl;
        IDisplay _display;
        IRFID _rfid;
        ILogFile _logFile;

        [SetUp]
        public void Setup()
        {
            //_receivedEventArgs = null;
            _chargeControl = Substitute.For<IChargeControl>();
            _display = Substitute.For<IDisplay>();
            _door = Substitute.For<IDoor>();
            _rfid = Substitute.For<IRFID>();
            _logFile = Substitute.For<ILogFile>();

            _uut = new StationControl(_door, _chargeControl, _display, _rfid, _logFile);

            

            //_uut.DoorStateChangedEvent += (o, args) =>
            //{
            //    _receivedEventArgs = args;
            //};
        }

        [Test]
        public void DoorOpened_DoorStateChanged_ShowConnectCalledOnDisplay()
        {
            _uut.DoorOpened();

            _display.Received(1).ShowConnectPhoneRequest();
        }

        [Test]
        public void DoorClosed_DoorStateChanged_ShowLoadRFIDRequestCalledOnDisplay()
        {
            _uut.DoorClosed();

            _display.Received(1).ShowLoadRFIDRequest();
        }

        [TestCase(1, 100)]
        [TestCase(1, 2)]
        [TestCase(0, 1)]
        [TestCase(1, -1)]
        [TestCase(-10, 10)]
        [TestCase(-10, -100)]
        public void CheckID_IDNotEqualToOldID_ShowWrongRFIDMessage(int OldId, int Id)
        {
            _uut.CheckID(OldId, Id);

            _display.Received(1).ShowMessage(Arg.Is<string>(s => s.Contains("Forkert RFID tag")));
        }

        [TestCase(-1, -1)]
        [TestCase(199999, 199999)]
        [TestCase(1, 1)]
        [TestCase(0, 0)]
        [TestCase(-10, -10)]
        [TestCase(-100, -100)]
        public void CheckID_IDIsEqualToOldID_ChargeControlStopCharge(int OldId, int Id)
        {
            _uut.CheckID(OldId, Id);

            _chargeControl.Received(1).StopCharge();
        }

        [TestCase(199999, 199999)]
        [TestCase(1, 1)]
        [TestCase(0, 0)]
        [TestCase(-1, -1)]
        [TestCase(-10, -10)]
        [TestCase(-100, -100)]
        public void CheckID_IDIsEqualToOldID_DoorUnlockDoor(int OldId, int Id)
        {
            _uut.CheckID(OldId, Id);

            _door.Received(1).UnlockDoor();
        }

        [TestCase(199999, 199999)]
        [TestCase(1, 1)]
        [TestCase(0, 0)]
        [TestCase(-1, -1)]
        [TestCase(-10, -10)]
        [TestCase(-100, -100)]
        public void CheckID_IDIsEqualToOldID_LogDoorUnlocked(int OldId, int Id)
        {
            _uut.CheckID(OldId, Id);

            _logFile.Received(1).LogDoorUnlocked(Id);
        }
        
        [TestCase(199999, 199999)]
        [TestCase(1, 1)]
        [TestCase(0, 0)]
        [TestCase(-1, -1)]
        [TestCase(-10, -10)]
        [TestCase(-100, -100)]
        public void CheckID_IDIsEqualToOldID_DisplayShowMessage(int OldId, int Id)
        {
            _uut.CheckID(OldId, Id);            

            _display.Received(1).ShowMessage(Arg.Is<string>(s => s.Contains("Tag din telefon ud af skabet og luk døren")));
            
        }

        [TestCase(199999, 199999)]
        [TestCase(1, 1)]
        [TestCase(0, 0)]
        [TestCase(-1, -1)]
        [TestCase(-10, -10)]
        [TestCase(-100, -100)]
        public void CheckID_IDIsEqualToOldID_LadeskabeStateIsAvailable(int OldId, int Id)
        {
            _uut.CheckID(OldId, Id);
            
            

            Assert.That(_uut.State, Is.EqualTo(StationControl.LadeskabState.Available));
            
        }

        
    }
}