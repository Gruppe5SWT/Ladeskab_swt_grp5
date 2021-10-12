using Ladeskab.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab
{
    public class RFID : IRFID
    {
        public event EventHandler<RFIDDetectedEventArgs> RFIDDetectedEvent;
        public void SetRFID(int newRFID)
        {
            OnRFIDDetected(new RFIDDetectedEventArgs { RFID = newRFID });
        }

        private void OnRFIDDetected(RFIDDetectedEventArgs e)
        {
            RFIDDetectedEvent?.Invoke(this, e);
        }
    }
}
