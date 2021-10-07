using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Interfaces
{
    public interface IDoor
    {
        event EventHandler<DoorStateChangedEventArgs> DoorStateChangedEvent;
        void LockDoor();
        void UnlockDoor();
    }
}
