using Ladeskab.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.IO;

namespace Ladeskab.Test.Unit
{
    public class DisplayUnitTests
    {
        Display _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new Display();
        }

        [Test]
        public void ShowConnectionError_ConnectionError_CorrectConsoleOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                _uut.ShowConnectionError();
                string expected = "Charging Area: Connection Error!";

                Assert.AreEqual(expected, sw.ToString().Trim());
            }

        }

        [Test]
        public void ShowConnectPhoneRequest_ConnectPhoneRequest_CorrectConsoleOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                _uut.ShowConnectPhoneRequest();
                string expected = "System Area: Please connect phone...";

                Assert.AreEqual(expected, sw.ToString().Trim());
            }

        }

        [Test]
        public void ShowDisconnectPhoneRequest_DisconnectPhoneRequest_CorrectConsoleOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                _uut.ShowDisconnectPhoneRequest();
                string expected = "System Area: Please disconnect phone...";

                Assert.AreEqual(expected, sw.ToString().Trim());
            }

        }

        [Test]
        public void ShowLoadRFIDRequest_LoadRFIDRequest_CorrectConsoleOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                _uut.ShowLoadRFIDRequest();
                string expected = "System Area: Please load RFID...";

                Assert.AreEqual(expected, sw.ToString().Trim());
            }

        }

        [Test]
        public void ShowRFIDError_RFIDError_CorrectConsoleOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                _uut.ShowRFIDError();
                string expected = "System Area: RFID Error!";

                Assert.AreEqual(expected, sw.ToString().Trim());
            }

        }

        [Test]
        public void ShowPhoneCharging_PhoneCharging_CorrectConsoleOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                _uut.ShowPhoneCharging();
                string expected = "Charging Area: Phone is charging...";

                Assert.AreEqual(expected, sw.ToString().Trim());
            }

        }

        [Test]
        public void ShowPhoneDoneCharging_PhoneDoneCharging_CorrectConsoleOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                _uut.ShowPhoneDoneCharging();
                string expected = "Charging Area: Phone is fully charged...";

                Assert.AreEqual(expected, sw.ToString().Trim());
            }

        }

        [Test]
        public void ShowChargingError_ChargingError_CorrectConsoleOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                _uut.ShowChargingError();
                string expected = "Charging Area: Charging Error!";

                Assert.AreEqual(expected, sw.ToString().Trim());
            }

        }

        [Test]
        public void ShowMessage_MessageWithNumbersAndLetters_CorrectConsoleOutput()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                string message = "Oh no, error 2234 occured!";
                string expected = message;
                _uut.ShowMessage(message);

                Assert.AreEqual(expected, sw.ToString().Trim());
            }
        }
    }
}
