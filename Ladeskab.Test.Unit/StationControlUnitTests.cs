using Ladeskab.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Test.Unit
{
    public class StationControlUnitTests
    {
        private StationControl _uut;
        
        IDoor _door;
        IChargeControl _chargeControl;
        IDisplay _display;

        [SetUp]
        public void Setup()
        {
            //_receivedEventArgs = null;
            _chargeControl = Substitute.For<IChargeControl>();
            _display = Substitute.For<IDisplay>();
            _door = Substitute.For<IDoor>();

            _uut = new StationControl(_door, _chargeControl, _display);

            

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



        
    }
}