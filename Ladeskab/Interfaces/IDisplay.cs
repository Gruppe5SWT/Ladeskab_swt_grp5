using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Interfaces
{
    interface IDisplay
    {
        void ShowConnectPhoneRequest();
        void ShowDisconnectPhoneRequestion();
        void ShowConnectionError();
        void ShowLoadRFIDRequest();
        void ShowRFIDError();
    }
}
