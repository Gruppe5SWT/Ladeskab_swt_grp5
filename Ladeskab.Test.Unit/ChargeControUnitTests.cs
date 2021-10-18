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
        public void StartCharge_ChargeControlStartCharge_CallUSBStartCharge()
        {
            _uut.StartCharge();
            _usbCharger.Received(1).StartCharge();
        }


        [Test]
        public void StopCharge_ChargeControlStopCharge_CallUSBStopCharge()
        {
            _uut.StopCharge();
            _usbCharger.Received(1).StopCharge();
        }


        [Test]
        public void HandleCurrentValueEvent_EventArgIsZero_DisplayExpected()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 0 });
            Assert.That(_uut.Connected, Is.False);
            _display.Received(1).ShowConnectionError();
        }

        [TestCase(1)]
        [TestCase(4)]
        [TestCase(5)]
        public void HandleCurrentValueEvent_EventSignalsFullyCharged_DisplayExpected(int newCurrent)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = newCurrent });
            Assert.That(_uut.Connected, Is.True);
            _display.Received(1).ShowPhoneDoneCharging();
        }

        [TestCase(6)]
        [TestCase(7)]
        [TestCase(250)]
        [TestCase(499)]
        [TestCase(500)]
        public void HandleCurrentValueEvent_EventSignalsCharging_DisplayExpected(int newCurrent)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = newCurrent });
            Assert.That(_uut.Connected, Is.True);
            _display.Received(1).ShowPhoneCharging();
        }

        [Test]
        public void HandleCurrentValueEvent_EventCurrentTooHigh_PhoneChargingError()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 501 });
            Assert.That(_uut.Connected, Is.True);
            _display.Received(1).ShowChargingError();
            _usbCharger.Received(1).StopCharge();
        }





    }
}