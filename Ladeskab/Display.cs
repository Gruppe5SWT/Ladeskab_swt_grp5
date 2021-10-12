using Ladeskab.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class Display : IDisplay
    {
        public void ShowConnectionError()
        {
            Console.WriteLine("Charging Area: Connection Error!");
        }

        public void ShowConnectPhoneRequest()
        {
            Console.WriteLine("System Area: Please connect phone...");
        }

        public void ShowDisconnectPhoneRequestion()
        {
            Console.WriteLine("System Area: Please disconnect phone...");
        }

        public void ShowLoadRFIDRequest()
        {
            Console.WriteLine("System Area: Please load RFID...");
        }

        public void ShowRFIDError()
        {
            Console.WriteLine("System Area: RFID Error!");
        }

        public void ShowPhoneCharging()
        {
            Console.WriteLine("Charging Area: Phone is charging...");
        }

        public void ShowPhoneDoneCharging()
        {
            Console.WriteLine("Charging Area: Phone is fully charged...");
        }

        public void ShowChargingError()
        {
            Console.WriteLine("Charging Area: Charging Error!")
        }
    }
}
