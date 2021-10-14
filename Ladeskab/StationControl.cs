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
        public enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
         LadeskabState _state;
        public LadeskabState State { get; private set; }
        private IChargeControl _chargeControl;
        private int _oldId;
        private IDoor _door;
        private IDisplay _display;
        private IRFID _RFID;
        private ILogFile _ILogFile;

        public StationControl(IDoor door, IChargeControl chargeControl, IDisplay display, IRFID rfid, ILogFile logFile)
        {
            _chargeControl = chargeControl;
            _door = door;
            _display = display;
            _RFID = rfid;
            _ILogFile = logFile;
            _door.DoorStateChangedEvent += HandleDoorStateChangedEvent;
            _RFID.RFIDDetectedEvent += RfidDetected;
            
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
        private void RfidDetected(object o, RFIDDetectedEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_chargeControl.Connected)
                    {
                        _door.LockDoor();
                        _chargeControl.StartCharge();
                        _oldId = e.RFID;
                        
                        _ILogFile.LogDoorLocked(e.RFID);
                        _display.ShowMessage("Charging Area: Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");

                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.ShowMessage("Charging Area: Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    CheckID(_oldId, e.RFID);

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
                
                _ILogFile.LogDoorUnlocked(Id);
                
                _display.ShowMessage("Charging Area: Tag din telefon ud af skabet og luk døren");
                
                _state = LadeskabState.Available;
            }
            else
            {
                _display.ShowMessage("Charging Area: Forkert RFID tag");
            }
        }

    }
}
