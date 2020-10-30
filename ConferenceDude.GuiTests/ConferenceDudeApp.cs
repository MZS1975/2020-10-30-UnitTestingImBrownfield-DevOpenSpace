using System;
using System.IO;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace ConferenceDude.GuiTests
{
    public class ConferenceDudeApp
    {
        readonly bool _wait;
        const string DriverUrl = "http://127.0.0.1:4723";
        const string AppPath = @"c:\work\github\2020-10-30-UnitTestingImBrownfield-DevOpenSpace\ConferenceDude.Client\bin\Debug\netcoreapp3.1";
        readonly string AppId = Path.Combine(AppPath, "ConferenceDude.Client.exe"); 

        WindowsDriver<WindowsElement> session;

        public ConferenceDudeApp(bool wait = true)
        {
            _wait = wait;
        }

        public WindowsDriver<WindowsElement> Session
        {
            get
            {
                // Launch application if it is not yet launched
                if (session == null)
                {
                    var options = new AppiumOptions();
                    options.AddAdditionalCapability("app", AppId);
                    options.AddAdditionalCapability("deviceName", "WindowsPC");
                    options.AddAdditionalCapability("appWorkingDir", AppPath);
                    session = new WindowsDriver<WindowsElement>(new Uri(DriverUrl), options);
                    Assert.IsNotNull(session);

                    // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times
                    session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);
                }

                if (_wait)
                {
                    var ewh = new EventWaitHandle(false, EventResetMode.AutoReset, "DUDECLIENT_PROCESS_READY");
                    if (!ewh.WaitOne(3000))
                    {
                        TearDown();
                        ConferenceDudeServer.TearDown();
                        throw new InvalidDataException("Could not start Conference Dude client");
                    }
                }

                return session;
            }
        }

        public void TearDown()
        {
            // Close the application and delete the session
            if (session != null)
            {
                session.Quit();
                session = null;
            }
        }
    }
}