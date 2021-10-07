using Ladeskab.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class DoorStateChangedEventArgs : EventArgs
    {
        public bool Open { get; set; }
    }

    public class Door : IDoor
    {
        public event EventHandler<DoorStateChangedEventArgs> DoorStateChangedEvent;

        private void OnDoorStateChanged(bool currentDoorState)
        {
            DoorStateChangedEvent?.Invoke(this, new DoorStateChangedEventArgs() { Open = currentDoorState });
        }

        public void OnDoorOpen()
        {
            OnDoorStateChanged(true);
        }

        public void OnDoorClose()
        {
            OnDoorStateChanged(false);
        }

        public void LockDoor()
        {
            Console.WriteLine("Door is locked");
        }

        public void UnlockDoor()
        {
            Console.WriteLine("Door is unlocked");
        }
    }
}
