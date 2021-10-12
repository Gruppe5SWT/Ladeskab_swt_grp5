using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Interfaces
{
    public interface IStationControl
    {
        void DoorOpened();
        void DoorClosed();
        void CheckID(int OldId, int Id);
    }
}
