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

        public void DoorClosed_DoorStateChanged_ShowLoadRFIDRequestCalledOnDisplay()
        {
            _uut.DoorClosed();

            _display.Received(1).ShowLoadRFIDRequest();
        }

        [TestCase(1, 2)]
        [TestCase(0, 1)]
        public void CheckID_IDNotEqualToOldID_ShowWrongRFIDMessage(int OldId, int Id)
        {
            _uut.CheckID(OldId, Id);


            
        }

        
    }
}