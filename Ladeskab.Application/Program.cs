using Ladeskab.Interfaces;
using System;

namespace Ladeskab.Application
{ 
    class Program
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            Door door = new();
            RFID Rfid = new();
            Display display = new();
            ChargeControl chargeControl = new(new UsbChargerSimulator(), display);
            LogFile logFile = new(new DateTimeProvider());
            StationControl stationControl = new(door, chargeControl, display, Rfid, logFile);

            bool finish = false;
            do
            {
                string input;
                Console.WriteLine("Indtast E = exit, O = open, C = close, P = plug in phone, U = unplug phone, R = set RFID: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.OnDoorOpen();
                        break;

                    case 'C':
                        door.OnDoorClose();
                        break;

                    case 'P':
                        chargeControl.Connected = true;
                        break;

                    case 'U':
                        chargeControl.Connected = false;
                        break;

                    case 'R':
                        Console.WriteLine("Indtast RFID id: ");
                        string idString = Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        Rfid.SetRFID(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
