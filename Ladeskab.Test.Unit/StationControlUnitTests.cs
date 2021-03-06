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
            _chargeControl = Substitute.For<IChargeControl>();
            _display = Substitute.For<IDisplay>();
            _door = Substitute.For<IDoor>();
            _rfid = Substitute.For<IRFID>();
            _logFile = Substitute.For<ILogFile>();

            _uut = new StationControl(_door, _chargeControl, _display, _rfid, _logFile);
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

            _display.Received(1).ShowMessage(Arg.Is<string>(s => s.Contains("System Area: Wrong RFID tag")));
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

            _display.Received(1).ShowMessage(Arg.Is<string>(s => s.Contains("Take your phone and close")));
            
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

        [Test]
        public void HandleRFIDDetectedEvent_LadeskabAvailableChargeControlConnectedRFIDDetectedEvent_LockDoor()
        {

            _uut.State = StationControl.LadeskabState.Available;
            _chargeControl.Connected.Returns(true);


            _rfid.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs {RFID = 1337});

            _door.Received(1).LockDoor();
            
        }
        [Test]
        public void HandleRFIDDetectedEvent_LadeskabAvailableChargeControlConnectedRFIDDetectedEvent_ChargeControlStartCharge()
        {

            _uut.State = StationControl.LadeskabState.Available;
            _chargeControl.Connected.Returns(true);
            

            _rfid.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs {RFID = 1337});

            _chargeControl.Received(1).StartCharge();
            
        }
        [Test]
        public void HandleRFIDDetectedEvent_LadeskabAvailableChargeControlConnectedRFIDDetectedEvent_oldIDsetToNewID()
        {

            _uut.State = StationControl.LadeskabState.Available;
            _chargeControl.Connected.Returns(true);

            int newRFID = 1337;
            _rfid.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs {RFID = newRFID});

            Assert.That(_uut.OldId.Equals(newRFID));
            
        }
        [Test]
        public void HandleRFIDDetectedEvent_LadeskabAvailableChargeControlConnectedRFIDDetectedEvent_LogDoorLocked()
        {

            _uut.State = StationControl.LadeskabState.Available;
            _chargeControl.Connected.Returns(true);

            int newRFID = 1337;
            _rfid.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs {RFID = newRFID});

            _logFile.Received(1).LogDoorLocked(newRFID);
            
        }

        [Test]
        public void HandleRFIDDetectedEvent_LadeskabAvailableChargeControlConnectedRFIDDetectedEvent_ShowMessageLocked()
        {

            _uut.State = StationControl.LadeskabState.Available;
            _chargeControl.Connected.Returns(true);

            int newRFID = 1337;
            _rfid.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs {RFID = newRFID});

            _display.Received(1).ShowMessage(Arg.Is<string>(s => s.Contains("System Area: The locker has been locked")));
            
        }
        [Test]
        public void HandleRFIDDetectedEvent_LadeskabAvailableChargeControlConnectedRFIDDetectedEvent_LadeskabStateIsLocked()
        {

            _uut.State = StationControl.LadeskabState.Available;
            _chargeControl.Connected.Returns(true);

            int newRFID = 1337;
            _rfid.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs {RFID = newRFID});

            Assert.That(_uut.State.Equals(StationControl.LadeskabState.Locked));

        }

        [Test]
        public void HandleRFIDDetectedEvent_LadeskabAvailableChargeControlNotConnectedRFIDDetectedEvent_DisplayShowMessagePhoneNotConnected()
        {

            _uut.State = StationControl.LadeskabState.Available;
            _chargeControl.Connected.Returns(false);

            int newRFID = 1337;
            _rfid.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs { RFID = newRFID });

            _display.Received(1).ShowMessage(Arg.Is<string>(s => s.Contains("System Area: Your phone is not connected properly")));


        }

        [Test]
        public void HandleRFIDDetectedEvent_LadeskabLockedRFIDDetectedEventWithMatchingID_LadeskabStateIsAvailable()
        {

            _uut.State = StationControl.LadeskabState.Locked;
            int newRFID = 1337;
            _uut.OldId = newRFID;

            _rfid.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs { RFID = newRFID });

            Assert.That(_uut.State.Equals(StationControl.LadeskabState.Available));
        }

        [Test]
        public void HandleDoorStateChangedEvent_DoorStateChangedEventDoorOpen_DisplayShowConnectPhoneRequest()
        {

            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs {Open = true});

            _display.Received(1).ShowConnectPhoneRequest();
            
        }

        [Test]
        public void HandleDoorStateChangedEvent_DoorStateChangedEventDoorClosed_DisplayShowLoadRFIDRequest()
        {

            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs {Open = false});

            _display.Received(1).ShowLoadRFIDRequest();
            
        }

        [Test]
        public void HandleRFIDDetectedEvent_LadeskabDoorOpenRFIDDetectedEvent_DisplayDidNotShowAnything()
        {
            _uut.State = StationControl.LadeskabState.DoorOpen;
            int newRFID = 1337;

            _rfid.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs { RFID = newRFID });

            _display.DidNotReceive().ShowMessage(Arg.Any<string>());
        }
    }
}