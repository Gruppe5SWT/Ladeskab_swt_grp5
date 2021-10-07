using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;
using UsbSimulator;

namespace Ladeskab
{
    public class ChargeControl : IChargeControl
    {
        private IUsbCharger _charger;

        public ChargeControl(IUsbCharger charger) 
        {
            _charger = charger;
        }

        public bool Connected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void StartCharge()
        {
            throw new NotImplementedException();
        }

        public void StopCharge()
        {
            throw new NotImplementedException();
        }

        private void HandleCurrentEvent(object sender, CurrentEventArgs e)
        {
            double currentCurrent = e.Current;

            if(currentCurrent == 0)
            {
                Connected = false;
            }
            else if(currentCurrent > 0 && currentCurrent <= 5)
            {
                Connected = true;
                _charger.StopCharge();
            }
            else if(currentCurrent > 5 && currentCurrent <= 500)
            {
                Connected = true;
                _charger.StartCharge();
            }
            else
            {
                Connected = true;
                _charger.StopCharge();
            }
        }

        
    }
}
