# Shutdown Scheduler
A command-line application that uses Windows' shutdown.exe to schedule a shutdown on a specific time. Useful for controlling computer uptime without using Task Scheduler.
# COMMAND LINE PARAMETERS
-help | Display this help dialog.
-abort | Abort/cancel a scheduled shutdown.
-hour | The specific hour when you want to trigger the shutdown. Only works with military time. (i.e 19:00 or 7:00 PM)
-minute | The specific minute when you want to trigger the shutdown. Only works with military time. (i.e 19:30 or 7:30 PM)
-second | The specific second when you want to trigger the shutdown. Only works with military time. (i.e 19:30:10 or 7:30:10 PM)
-delay | This option allows you to toggle a delay that adds more time before the scheduled shutdown. The delay can be modified with the options below.
-delayafterschedule | This option toggles whether the delay time should be added to the scheduled time, or if the delay should happen if the application is launched after the scheduled shutdown time. The former is default. Requires -delay.
-delayedhour | How many hours you want to delay the shutdown. Requires -delay.
-delayedminute | How many minutes you want to delay the shutdown. Requires -delay.
-delayedsecond | How many seconds you want to delay the shutdown. Requires -delay.
# EXAMPLES

## Shut down the PC at 7:00 PM/19:00
ShutdownScheduler.exe -hour 19

## The same as the above, but it delays it by an hour and 30 minutes.
ShutdownScheduler.exe -hour 19 -delay -delayedhour 1 -delayedminute 30

## The same as the above, but it will begin the delay if the application is booted up after the scheduled shutdown, rather than adding the time to the schedule time.
ShutdownScheduler.exe -hour 19 -delay -delayafterschedule -delayedhour 1 -delayedminute 30

## Abort a scheduled shutdown.
ShutdownScheduler.exe -abort
