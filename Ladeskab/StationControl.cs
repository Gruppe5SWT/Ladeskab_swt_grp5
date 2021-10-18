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
        public LadeskabState State { get; set; }
        public int OldId { get; set; }  

        private IChargeControl _chargeControl;
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
            _RFID.RFIDDetectedEvent += HandleRFIDDetectedEvent;
            
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
        private void HandleRFIDDetectedEvent(object o, RFIDDetectedEventArgs e)
        {
            switch (State)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_chargeControl.Connected)
                    {
                        _door.LockDoor();
                        _chargeControl.StartCharge();
                        OldId = e.RFID;
                        
                        _ILogFile.LogDoorLocked(e.RFID);
                        _display.ShowMessage("System Area: The locker has been locked and your phone is being charged. Use your RFID to unlock.");

                        State = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.ShowMessage("Charging Area: Your phone is not connected properly. Try again.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    CheckID(OldId, e.RFID);

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
            if (Id == OldId)
            {
                _chargeControl.StopCharge();

                _door.UnlockDoor();
                
                _ILogFile.LogDoorUnlocked(Id);
                
                _display.ShowMessage("System Area: Take your phone and close the door.");
                
                State = LadeskabState.Available;
            }
            else
            {
                _display.ShowMessage("System Area: Wrong RFID tag");
            }
        }

    }
}
