using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;
using TechTalk.SpecFlow;

namespace ConferenceDude.GuiTests
{
    [Binding]
    public class ProgrammStartenSteps
    {
        ConferenceDudeApp _dudeClientApp;
        WindowsDriver<WindowsElement> _dudeClientSession;

        [Given(@"Der Server wurde gestartet")]
        public void AngenommenDerServerWurdeGestartet()
        {
            System.Diagnostics.Debug.WriteLine("Server gestartet");
        }
        
        [When(@"der Clientgestartet wurde")]
        public void WennDerClientgestartetWurde()
        {
            System.Diagnostics.Debug.WriteLine("Client gestartet");
        }

        [Then(@"ist die Oberfläche zu sehen")]
        public void DannIstDieOberflacheZuSehen()
        {
            System.Diagnostics.Debug.WriteLine("zu sehen?");
            var view = _dudeClientSession.FindElementByAccessibilityId("MainWindow");
            Assert.AreEqual("Sessions", view.Text);
        }

        [BeforeScenario("StarteClientUndServer")]
        public void SetupServerAndClient()
        {
            System.Diagnostics.Debug.WriteLine("Starte server und client");
            ConferenceDudeServer.Setup();

            _dudeClientApp = new ConferenceDudeApp();
            _dudeClientSession = _dudeClientApp.Session;
        }

        [AfterScenario("StarteClientUndServer")]
        public void TearDownServerAndClient()
        {
            System.Diagnostics.Debug.WriteLine("Beende server und client");
            _dudeClientApp.TearDown();
            ConferenceDudeServer.TearDown();
        }

    }
}
