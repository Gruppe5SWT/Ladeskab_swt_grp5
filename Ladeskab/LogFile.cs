using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    public class LogFile : ILogFile
    {
        IDateTime _dateTime;

        public LogFile(IDateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public void LogDoorLocked(int id)
        {
            FileStream fs = new FileStream("LogFile.txt", FileMode.OpenOrCreate);
            StreamWriter s = new StreamWriter(fs);

            
            s.WriteLine(_dateTime.getDateTime() + $": Locked with RFID {id}");
            
            s.Close();
            fs.Close();
   
        }

        public void LogDoorUnlocked(int id)
        {
            FileStream fs = new FileStream("LogFile.txt", FileMode.OpenOrCreate);
            StreamWriter s = new StreamWriter(fs);
            s.WriteLine(_dateTime.getDateTime() + $": Unlocked with RFID {id}");
            s.Close();
            fs.Close();

        }
    }
}
