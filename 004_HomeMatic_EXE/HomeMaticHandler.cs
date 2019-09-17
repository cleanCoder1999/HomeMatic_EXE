
using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;

namespace HomeMatic
{
    public class HomeMaticHandler
    {
        /*
        private IHomeMaticProxy homeMaticProxy;
        private DeviceDescription[] deviceDescriptions;

        public HomeMaticHandler(string url)
        {
            ConnectTo(url);
        }
        private void ConnectTo(string url)
        {
            GenerateHomeMaticProxy();
            homeMaticProxy.Url = url;
        }
        private void GenerateHomeMaticProxy()
        {
            homeMaticProxy = XmlRpcProxyGen.Create<IHomeMaticProxy>();
        }

        public DeviceDescription[] GetDeviceDescriptions()
        {
            ReadDeviceDescriptionsFromHomeMatic();
            return deviceDescriptions;
        }
        private void ReadDeviceDescriptionsFromHomeMatic()
        {
            deviceDescriptions = homeMaticProxy.ListDevices();
        }
        */
        private string url;
        private int port;
        private IHomeMaticProxy homeMaticProxy;

        /*
        public IHomeMaticProxy HomeMaticProxy
        {
            get;
        }*/
        public IHomeMaticProxy getProxy()
        {
            return homeMaticProxy;
        }

        public void SetUrl(string url)
        {
            this.url = url;
        }
        public void SetPort(int port)
        {
            this.port = port;
        }

        public void Start()
        {
            try {
                homeMaticProxy = XmlRpcProxyGen.Create<IHomeMaticProxy>();
                homeMaticProxy.Url = url + ":" + port;
            } catch (Exception) {
                /**/
            }
        }
        public bool IsAlive()
        {
            bool IsAlive = false;
            try {
                IsAlive = homeMaticProxy.Ping(url);
            } catch (Exception) { }

            return IsAlive;
        }
        
        public string[] GetAnyDistinctAddress()
        {
            string[] allAddresses = GetArrayOfAddresses();
            List<string> listOfdistinctAddresses = new List<string>();

            foreach(string address in allAddresses)
            {
                if(IsAddress(address))
                    listOfdistinctAddresses.Add(address);
            }

            return Convert(listOfdistinctAddresses); 
        }
        private string[] GetArrayOfAddresses()
        {
            DeviceDescription[] deviceDescriptions = ReadFromHomeMatic();
            string[] allAddresses = GatherAllAdressesFrom(deviceDescriptions);

            return allAddresses;
        }
        private DeviceDescription[] ReadFromHomeMatic()
        {
            try {
                return homeMaticProxy.ListDevices();
            } catch (Exception) {
                return null;
            }
            
        }
        private string[] GatherAllAdressesFrom(
                            DeviceDescription[] deviceDescriptions)
        {
            string[] addresses = new string[deviceDescriptions.Length];

            for(int i = 0; i < deviceDescriptions.Length; ++i)
                addresses[i] = deviceDescriptions[i].Address;

            return addresses;
        }
        private bool IsAddress(
                            string supposedAddress)
        {
            return !supposedAddress.Contains(":");
        }
        private string[] Convert(
                            List<string> list)
        {
            string[] arrayOfdistinctAddresses = new string[list.Count];

            for (int i = 0; i < list.Count; ++i)
                arrayOfdistinctAddresses[i] = list[i];

            return arrayOfdistinctAddresses;
        }

    }
}
