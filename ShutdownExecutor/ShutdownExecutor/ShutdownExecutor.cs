using System;
using System.Diagnostics;

namespace ShutdownExecutor
{
    class ShutdownExecutor
    {
        static void Main(string[] args)
        {
            //https://stackoverflow.com/questions/1859248/how-to-change-time-in-datetime
            TimeSpan ts = new TimeSpan(19, 0, 0);
            DateTime adjustedTime = DateTime.Now.Date + ts;
            double secondsToShutdown = (adjustedTime - DateTime.Now).TotalSeconds;
            if (DateTime.Now > adjustedTime)
            {
                StartShutdownApplication("/f /s /t 0");
            }
            else
            {
                StartShutdownApplication("/f /s /t " + Convert.ToInt32(secondsToShutdown));
            }
            Environment.Exit(1);
        }

        static void StartShutdownApplication(string args)
        {
            var p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "CMD.exe";
            p.StartInfo.Arguments = @"/C C:\Windows\System32\shutdown.exe " + args;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
        }
    }
}
