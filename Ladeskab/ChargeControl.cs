using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;


namespace Ladeskab
{
    public class ChargeControl : IChargeControl
    {
        private IUsbCharger _charger;
        private IDisplay _display;

        public ChargeControl(IUsbCharger charger, IDisplay display) 
        {
            _charger = charger;
            _display = display;
            _charger.CurrentValueEvent += HandleCurrentValueEvent;
        }

        public bool Connected { get; set; }

        public void StartCharge()
        {
            _charger.StartCharge();
        }

        public void StopCharge()
        {
            _charger.StopCharge();
        }

        private void HandleCurrentValueEvent(object sender, CurrentEventArgs e)
        {
            double currentCurrent = e.Current;

            if(currentCurrent == 0)
            {
                Connected = false;
                _display.ShowConnectionError();
            }
            else if(currentCurrent > 0 && currentCurrent <= 5)
            {
                Connected = true;
                _display.ShowPhoneDoneCharging();

            }
            else if(currentCurrent > 5 && currentCurrent <= 500)
            {
                Connected = true;
                _display.ShowPhoneCharging();
            }
            else
            {
                Connected = true;
                _charger.StopCharge();
                _display.ShowChargingError();
            }
        }

        
    }
}
