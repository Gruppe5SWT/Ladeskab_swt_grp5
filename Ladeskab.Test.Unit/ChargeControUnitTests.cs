using Ladeskab.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Test.Unit_
{
    public class ChargeControlUnitTests
    {
        IUsbCharger _usbCharger;
        IDisplay _display;
   
        [SetUp]
        public void Setup()
        {
            _usbCharger = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();

        }

        [Test]
        public void Test1()
        {

        }
    }
}