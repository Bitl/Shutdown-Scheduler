#region Usings
using System;
using System.Diagnostics;
#endregion

namespace ShutdownScheduler
{
    #region Shutdown Scheduler
    class ShutdownScheduler
    {
        #region Variables
        static int hour = 0;
        static int minute = 0;
        static int second = 0;
        static bool delay = false;
        static bool delayAfterSchedule = false;
        static int delayedHour = 0;
        static int delayedMinute = 0;
        static int delayedSecond = 0;
        #endregion

        #region Main
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                CommandLineArguments.Arguments CommandLine = new CommandLineArguments.Arguments(args);

                if (CommandLine["help"] != null)
                {
                    DisplayHelp();
                }

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

                    if (CommandLine["delay"] != null)
                    {
                        delay = true;

                        if (CommandLine["delayafterschedule"] != null)
                        {
                            delayAfterSchedule = true;
                        }

                        if (CommandLine["delayedhour"] != null)
                        {
                            delayedHour = Convert.ToInt32(CommandLine["delayedhour"]);
                        }

                        if (CommandLine["delayedminute"] != null)
                        {
                            delayedMinute = Convert.ToInt32(CommandLine["delayedminute"]);
                        }

                        if (CommandLine["delayedsecond"] != null)
                        {
                            delayedSecond = Convert.ToInt32(CommandLine["delayedsecond"]);
                        }
                    }
                }
                else
                {
                    StartShutdownApplication("/a", true);
                }
            }
            else
            {
                DisplayHelp();
            }
            //https://stackoverflow.com/questions/1859248/how-to-change-time-in-datetime
            TimeSpan ts = new TimeSpan(hour, minute, second);
            DateTime adjustedTime = DateTime.Now.Date + ts;
            DateTime adjustedDelayTime = (!delayAfterSchedule ? adjustedTime.AddHours(delayedHour).AddMinutes(delayedMinute).AddSeconds(delayedSecond) : adjustedTime);
            double secondsToShutdown = (adjustedDelayTime - DateTime.Now).TotalSeconds;
            if (DateTime.Now > adjustedTime)
            {
                DateTime adjustedDelayTimeNow = DateTime.Now.AddHours(delayedHour).AddMinutes(delayedMinute).AddSeconds(delayedSecond);
                double delaySeconds = (adjustedDelayTimeNow - DateTime.Now).TotalSeconds;
                StartShutdownApplication("/f /s /t " + (delay ? Convert.ToInt32(delaySeconds) : 0), true);
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

        static void DisplayHelp()
        {
            Console.WriteLine("Shutdown Scheduler by Bitl");
            Console.WriteLine("A command-line application that uses Windows' shutdown.exe to schedule a shutdown on a specific time of day.");
            Console.WriteLine("Useful for controlling computer uptime without using Task Scheduler.\n");
            Console.WriteLine("---- COMMAND LINE PARAMETERS ---");
            Console.WriteLine("-help | Display this help dialog.");
            Console.WriteLine("-abort | Abort/cancel a scheduled shutdown.");
            Console.WriteLine("-hour | The specific hour when you want to trigger the shutdown. Only works with military time. (i.e 19:00 or 7:00 PM)");
            Console.WriteLine("-minute | The specific minute when you want to trigger the shutdown. Only works with military time. (i.e 19:30 or 7:30 PM)");
            Console.WriteLine("-second | The specific second when you want to trigger the shutdown. Only works with military time. (i.e 19:30:10 or 7:30:10 PM)");
            Console.WriteLine("-delay | This option allows you to toggle a delay that adds more time before the scheduled shutdown. The delay can be modified with the options below.");
            Console.WriteLine("-delayafterschedule | This option toggles whether the delay time should be added to the scheduled time, or if the delay should happen if the application is launched after the scheduled shutdown time. The former is default. Requires -delay.");
            Console.WriteLine("-delayedhour | How many hours you want to delay the shutdown. Requires -delay.");
            Console.WriteLine("-delayedminute | How many minutes you want to delay the shutdown. Requires -delay.");
            Console.WriteLine("-delayedsecond | How many seconds you want to delay the shutdown. Requires -delay.");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadLine();
            Environment.Exit(1);
        }
        #endregion
    }
    #endregion
}
