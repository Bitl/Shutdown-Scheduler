#region Usings
using System;
using System.Diagnostics;
#endregion

namespace ShutdownExecutor
{
    #region Shutdown Executor
    class ShutdownExecutor
    {
        #region Main
        static void Main(string[] args)
        {
            //set to 7:00 or 19:00 by default.
            int hour = 19;
            int minute = 0;
            int second = 0;
            if (args.Length > 0)
            {
                CommandLineArguments.Arguments CommandLine = new CommandLineArguments.Arguments(args);

                if (CommandLine["abort"] == null)
                {
                    if (CommandLine["hour"] != null)
                    {
                        hour = Convert.ToInt32(CommandLine["hour"]);
                    }

                    if (CommandLine["minute"] != null)
                    {
                        minute = Convert.ToInt32(CommandLine["minute"]);
                    }

                    if (CommandLine["second"] != null)
                    {
                        second = Convert.ToInt32(CommandLine["second"]);
                    }
                }
                else
                {
                    StartShutdownApplication("/a", true);
                }
            }
            //https://stackoverflow.com/questions/1859248/how-to-change-time-in-datetime
            TimeSpan ts = new TimeSpan(hour, minute, second);
            DateTime adjustedTime = DateTime.Now.Date + ts;
            double secondsToShutdown = (adjustedTime - DateTime.Now).TotalSeconds;
            if (DateTime.Now > adjustedTime)
            {
                StartShutdownApplication("/f /s /t 0", true);
            }
            else
            {
                StartShutdownApplication("/f /s /t " + Convert.ToInt32(secondsToShutdown), true);
            }
        }
        #endregion

        #region Functions
        static void StartShutdownApplication(string args, bool exit)
        {
            var p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "CMD.exe";
            p.StartInfo.Arguments = @"/C C:\Windows\System32\shutdown.exe " + args;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            if (exit)
            {
                Environment.Exit(1);
            }
        }
        #endregion
    }
    #endregion
}
