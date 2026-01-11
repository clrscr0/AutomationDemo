using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumDotNetAutomation.Framework.Utils
{
    public class NetworkMonitoring
    {
        private readonly NetworkManager manager;
        public List<NetworkRequestSentEventArgs> Requests;
        public List<NetworkResponseReceivedEventArgs> Responses;

        public NetworkMonitoring(IWebDriver driver)
        {
            manager = new NetworkManager(driver);
            Requests = new List<NetworkRequestSentEventArgs>();
            Responses = new List<NetworkResponseReceivedEventArgs>();
        }

        public void Start()
        {
             manager.NetworkResponseReceived += ResponseReceivedHandler;
             manager.NetworkRequestSent += ManagerOnNetworkRequestSent;
             var monitor = manager.StartMonitoring();
             monitor.Wait();
        }

        private void ManagerOnNetworkRequestSent(object sender, NetworkRequestSentEventArgs e)
        {
            Requests.Add(e);
        }

        private void ResponseReceivedHandler(object sender, NetworkResponseReceivedEventArgs e)
        {
            Responses.Add(e);
        }

        public void Stop()
        {
            manager.StopMonitoring();
        }

        public void Clear()
        {
            Requests.Clear();
            Responses.Clear();
        }
    }
}
