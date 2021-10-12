using Ladeskab.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Test.Unit
{
    public class ChargeControlUnitTests
    {
        ChargeControl _uut;
        IUsbCharger _usbCharger;
        IDisplay _display;
   
        [SetUp]
        public void Setup()
        {
            _usbCharger = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();

            _uut = new ChargeControl(_usbCharger, _display);
        }

        [Test]
        public void Test_CurrentEvent_NoConnection()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 0 });
            Assert.That(_uut.Connected, Is.False);
            _display.Received(1).ShowConnectionError();
        }
    }
}