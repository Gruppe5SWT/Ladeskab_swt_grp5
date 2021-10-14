using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladeskab.Interfaces;

namespace Ladeskab
{
    class LogFile : ILogFile
    {
        public void LogDoorLocked()
        {
            FileStream fs = new FileStream("LogFile.txt", FileMode.OpenOrCreate);
            StreamWriter s = new StreamWriter(fs);
            s.WriteLine(DateTime.Now + ": Door Locked");
            s.Close();
            fs.Close();
   
        }

        public void LogDoorUnlocked()
        {
            FileStream fs = new FileStream("LogFile.txt", FileMode.OpenOrCreate);
            StreamWriter s = new StreamWriter(fs);
            s.WriteLine(DateTime.Now + ": Door Unlocked");
            s.Close();
            fs.Close();

        }
    }
}
