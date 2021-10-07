using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Interfaces
{
    interface IChargeControl
    {
        bool Connected();
        void StartCharge();
        void StopCharge();
    }
}
