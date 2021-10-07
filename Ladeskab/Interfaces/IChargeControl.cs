using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Interfaces
{
    interface IChargeControl
    {
        public bool Connected { get; set; }
        void StartCharge();
        void StopCharge();
    }
}
