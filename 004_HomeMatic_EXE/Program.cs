using System;
using System.Text;

using System.Net;
using System.Net.Sockets;
using SocketConnection;

using CookComputing.XmlRpc;
using HomeMatic;

using System.Collections.Generic;
using DescriptionProccessing;


namespace HomeMatic_EXE
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            byte[] serverIpAddress = { 172, 17, 16, 3 };
            byte[] clientIpAddress = { 172, 17, 16, 10 };

            ServerInformation serverInfo = new ServerInformation(serverIpAddress, 80);
            //ServerInformation serverInfo = new ServerInformation(serverIpAddress);

            Client client = new Client(
                                clientIpAddress,
                                serverInfo.GetConstPort,
                                serverInfo);

            Console.WriteLine("before establishing connection");

            
            client.EstablishConnection();
            Console.WriteLine("after establishing connection");

            Console.WriteLine(client.ToString());
            client.SendRequest("Hello");
            Console.WriteLine("client.send");
            string response = client.ReceiveResponse();
            Console.WriteLine("true3");
            client.ShutdownAndCloseConnection();
            

            Console.WriteLine("before PROXY creation");
            IHomeMaticProxy proxy = XmlRpcProxyGen.Create<IHomeMaticProxy>();
            Console.WriteLine("after PROXY creation");

            Console.WriteLine("read devices");
            DeviceDescription[] devices = proxy.ListDevices();

            Console.WriteLine("Ping(): " + proxy.Ping("http://172.16.17.3:2001"));
*/
            //HomeMaticHandler handler = new HomeMaticHandler("http://172.16.17.3:2001");

            //DeviceDescription[] deviceDescriptions = handler.GetDeviceDescriptions();

            //Console.WriteLine("output devices");
            /*
            foreach (DeviceDescription singleDevice in devices)
            {
                Console.WriteLine(singleDevice.ToString());
            }
            
            Console.WriteLine(devices[0].Address);
            Console.WriteLine(devices[0].IsDevice());
            Console.WriteLine(devices[0].Type);

            Console.WriteLine(devices[1].Address);
            Console.WriteLine(devices[1].IsChannel());
            Console.WriteLine(devices[1].ParentType);

            Console.WriteLine(devices[2].Address);
            Console.WriteLine(devices[2].IsChannel());
            Console.WriteLine(devices[2].ParentType);
            

            DeviceDescriptionProcessor dp = new DeviceDescriptionProcessor();
            //dp.Convert(deviceDescriptions);

            List<PhysicalDevice> list = new List<PhysicalDevice>();
            list = dp.ListOfPhysicalDevices;

            Console.WriteLine(list[0]);
            */
            /*foreach (PhysicalDevice device in list)
            {
                Console.WriteLine(device.ToString);
            }*/

            HomeMaticHandler BidCos_Handler = new HomeMaticHandler();
            HomeMaticHandler HmIP_Handler = new HomeMaticHandler();


            BidCos_Handler.SetUrl("http://172.16.17.28");
            BidCos_Handler.SetPort(2001);
            BidCos_Handler.Start();

            HmIP_Handler.SetUrl("http://172.16.17.28");
            HmIP_Handler.SetPort(2010);
            HmIP_Handler.Start();

            string[] BidCos_Handler_addresses = BidCos_Handler.GetAnyDistinctAddress();
            int i = 0;
            foreach (string address in BidCos_Handler_addresses)
            {
                ++i;
                Console.WriteLine(i + ": " + address);
                Console.WriteLine("----------------------");
            }

            string[] HmIP_Handler_addresses = HmIP_Handler.GetAnyDistinctAddress();
            i = 0;
            foreach (string address in HmIP_Handler_addresses)
            {
                ++i;
                Console.WriteLine(i + ": " + address);
                Console.WriteLine("----------------------");
            }
            Actuator actuator = new Actuator(BidCos_Handler.getProxy());
            actuator.SetAddress("NEQ0184037");
            actuator.SetChannel("3");
            actuator.SetDataPoint("RAMP_TIME");
            var  power = actuator.GetValue();
            Console.WriteLine("power: " + power);
            Console.WriteLine("power: " + power);

        }
    }
}

namespace SocketConnection
{
    /// <summary>
    /// an object of class ServerInformation contains an ip address and a port number
    /// </summary>
    public class ServerInformation
    {
        private IPAddress ipAddress;
        private int port;
        private const int constPort = 80;

        public ServerInformation(
                            byte[] ipAddress,
                            int port)
        {
            this.ipAddress = new IPAddress(ipAddress);
            this.port = port;
        }

        public ServerInformation(byte[] ipAddress)
        {
            this.ipAddress = new IPAddress(ipAddress);
        }

        public IPAddress GetIpAddress
        {
            /*
            get
            {
                return ipAddress;
            }
            */
            get;
        }

        public int GetConstPort
        {
            /*
            get
            {
                return constPort;
            }
            */
            get;
        }

        public int GetPort
        {
            /*
            get
            {
                return port;
            }
            */
            get;
        }

    }

    /// <summary>
    /// an object of class Client creates/maintains/uses a socket connection via TCP
    /// </summary>
    public class Client
    {
        private IPAddress ipAddress;
        private int port;
        private ServerInformation serverInfo;
        private Socket client;

        public Client(
                    byte[] ipAddress,
                    int port,
                    ServerInformation serverInfo
                    )
        {
            this.ipAddress = new IPAddress(ipAddress);
            this.port = port;
            this.serverInfo = serverInfo;
        }


        public void EstablishConnection()
        {
            CreateClientSocket();

            try
            {
                client.Connect(serverInfo.GetIpAddress, serverInfo.GetPort);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
            catch (SocketException)
            {
                throw;
            }
            catch (ObjectDisposedException)
            {
                throw;
            }
            catch (NotSupportedException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }
        private void CreateClientSocket()
        {
            this.client = new Socket(
                                ipAddress.AddressFamily,
                                SocketType.Stream,
                                ProtocolType.Tcp);
        }

        /*
        public bool sendAndCheckRequest(string request)
        {
            byte[] encodedRequest;
            int quotaQuantityOfSentBytes = 0;
            int actualQuantityOfSentBytes = 1;

            try {
                encodedRequest = Encoding.ASCII.GetBytes(request);

                quotaQuantityOfSentBytes = encodedRequest.Length;
                actualQuantityOfSentBytes = client.Send(encodedRequest);
            } catch (Exception) {
                throw;
            }

            return hasSameQuantity(
                                quotaQuantityOfSentBytes,
                                actualQuantityOfSentBytes);
        }
        private bool hasSameQuantity(
                                int quotaQuantity,
                                int actualQuantity)
        {
            return quotaQuantity == actualQuantity;
        }
        */
        public void SendRequest(string request)
        {
            try
            {
                byte[] encodedRequest = Encoding.ASCII.GetBytes(request);
                client.Send(encodedRequest);

            } catch (ArgumentNullException)
            {
                throw;
            }
            catch (SocketException)
            {
                throw;
            }
            catch (ObjectDisposedException)
            {
                throw;
            }
        }

        public string ReceiveResponse()
        {
            byte[] encodedResponse = new byte[1024];
            string response;

            client.Receive(encodedResponse);
            response = Encoding.ASCII.GetString(encodedResponse);

            return response;
        }
        
        public void ShutdownAndCloseConnection()
        {
            try
            {
                client.Shutdown(SocketShutdown.Both);
            } catch (SocketException)
            {
                throw;
            }
            catch (ObjectDisposedException)
            {
                throw;
            }
            client.Close();
        }

        override public string ToString()
        { 
            try
            {
                return "IP: " + ipAddress.ToString() + "\nPort: " + port + "\nclient.Connected: " + client.Connected;
            } catch (SocketException)
            {
                throw;
            }
        }
    }

}

