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
            Console.WriteLine("Connection Error!");
        }

        public void ShowConnectPhoneRequest()
        {
            Console.WriteLine("Please connect phone...");
        }

        public void ShowDisconnectPhoneRequestion()
        {
            Console.WriteLine("Please disconnect phone...");
        }

        public void ShowLoadRFIDRequest()
        {
            Console.WriteLine("Please load RFID...");
        }

        public void ShowRFIDError()
        {
            Console.WriteLine("RFID Error!");
        }
    }
}
