using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    class DateTimeProvider : IDateTime
    {
        public string getDateTime()
        {
            return DateTime.Now.ToString();
        }

    }
}
