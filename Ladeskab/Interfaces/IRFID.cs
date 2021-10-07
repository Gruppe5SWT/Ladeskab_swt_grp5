using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Interfaces
{
    public class RFIDDetectedEventArgs : EventArgs
    {
        public bool RFID { get; set; }
    }
    public interface IRFID
    {
        event EventHandler<RFIDDetectedEventArgs> RFIDDetectedEvent;
    }
}
