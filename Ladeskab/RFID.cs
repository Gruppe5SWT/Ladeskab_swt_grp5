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
        private bool _oldRFID;
        public event EventHandler<RFIDDetectedEventArgs> RFIDDetectedEvent;
        public void SetRFID(bool newRFID)
            {
            if (newRFID != _oldRFID)
                {
                OnRFIDDetected(new RFIDDetectedEventArgs { RFID = newRFID });
                }
            }

        protected virtual void OnRFIDDetected(RFIDDetectedEventArgs e)
        {
            RFIDDetectedEvent?.Invoke(this, e);
        }
    }
}
