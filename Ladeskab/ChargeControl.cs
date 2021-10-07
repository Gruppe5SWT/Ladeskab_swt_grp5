using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;
using UsbSimulator;

namespace Ladeskab
{
    class ChargeControl : IChargeControl
    {
        public bool Connected { get; set; }

        public void StartCharge()
        {
            Console.WriteLine("Started charging");
        }

        public void StopCharge()
        {
            Console.WriteLine("Stopped charging");
        }

        private void HandleCurrentEvent(object sender, CurrentEventArgs e)
        {

        }

        
    }
}
