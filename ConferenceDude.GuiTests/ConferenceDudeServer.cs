using System.Diagnostics;
using System.IO;
using System.Threading;
using NUnit.Framework;

namespace ConferenceDude.GuiTests
{
    public class ConferenceDudeServer
    {
        const string AppPath = @"c:\work\github\2020-10-30-UnitTestingImBrownfield-DevOpenSpace\ConferenceDude.Server\bin\Debug\netcoreapp3.1";
        static readonly string AppId = Path.Combine(AppPath, @"ConferenceDude.Server.exe");

        static Process _process;

        public static void Setup()
        {
            if (_process == null)
            {
                //var cd = Directory.GetCurrentDirectory().Replace("\\", "/");
                var cd = AppPath.Replace("\\", "/");

                EventWaitHandle ewh = null;
                try
                {
                    ewh = new EventWaitHandle(false, EventResetMode.AutoReset, "DUDESERVER_PROCESS_READY");

                    var startInfo = new ProcessStartInfo
                    {
                        FileName = AppId,
                        WorkingDirectory = cd,
                        UseShellExecute = false,
                    };
                    _process = Process.Start(startInfo);
                    Assert.IsNotNull(_process);
                    if (!ewh.WaitOne(3000))
                    {
                        throw new InvalidDataException("Could not start Conference Dude server");
                    }
                }
                finally
                {
                    ewh?.Close();
                }
            }
        }

        public static void TearDown()
        {
            // Close the application and delete the session
            if (_process != null)
            {
                _process.Kill();
                _process = null;
            }
        }
    }
}