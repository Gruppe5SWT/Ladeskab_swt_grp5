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
        public void HandleCurrentValueEvent_NoConnection()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 0 });
            Assert.That(_uut.Connected, Is.False);
            _display.Received(1).ShowConnectionError();
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public void HandleCurrentValueEvent_FullyCharged(int newCurrent)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = newCurrent });
            Assert.That(_uut.Connected, Is.True);
            _display.Received(1).ShowPhoneDoneCharging();
        }

        [TestCase(6)]
        [TestCase(250)]
        [TestCase(500)]
        public void HandleCurrentValueEvent_PhoneCharging(int newCurrent)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = newCurrent });
            Assert.That(_uut.Connected, Is.True);
            _display.Received(1).ShowPhoneCharging();
        }

        [Test]
        public void HandleCurrentValueEvent_PhoneChargingError()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 501 });
            Assert.That(_uut.Connected, Is.True);
            _display.Received(1).ShowChargingError();
            _usbCharger.Received(1).StopCharge();
        }





    }
}