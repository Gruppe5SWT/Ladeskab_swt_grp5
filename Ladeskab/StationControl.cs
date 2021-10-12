using Ladeskab.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class StationControl : IStationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IChargeControl _chargeControl;
        private int _oldId;
        private IDoor _door;
        private IDisplay _display;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        public StationControl(IDoor door, IChargeControl chargeControl, IDisplay display)
        {
            _chargeControl = chargeControl;
            _door = door;
            _display = display;
            _door.DoorStateChangedEvent += HandleDoorStateChangedEvent;
        }
        private void HandleDoorStateChangedEvent(object o, DoorStateChangedEventArgs e)
        {
            if (e.Open)
            {
                DoorOpened();
            }
            else
                DoorClosed();
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_chargeControl.Connected)
                    {
                        _door.LockDoor();
                        _chargeControl.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    CheckID(_oldId, id);

                    break;
            }



        }

        public void DoorOpened()
        {
            _display.ShowConnectPhoneRequest();
        }

        public void DoorClosed()
        {
            _display.ShowLoadRFIDRequest();
        }

        public void CheckID(int OldId, int Id)
        {
            if (Id == _oldId)
            {
                _chargeControl.StopCharge();
                _door.UnlockDoor();
                using (var writer = File.AppendText(logFile))
                {
                    writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", Id);
                }

                Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                _state = LadeskabState.Available;
            }
            else
            {
                Console.WriteLine("Forkert RFID tag");
            }
        }

    }
}
