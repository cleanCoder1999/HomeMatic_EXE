using System;
using System.Text;

using System.Net;
using System.Net.Sockets;
using SocketConnection;

using CookComputing.XmlRpc;
using XmlRpcConnection;

using System.Collections.Generic;



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
            */

            Console.WriteLine("before PROXY creation");
            IHomeMaticProxy proxy = XmlRpcProxyGen.Create<IHomeMaticProxy>();
            Console.WriteLine("after PROXY creation");

            

            Console.WriteLine("read devices");
            DeviceDescription[] devices = proxy.ListDevices();

            Console.WriteLine("output devices");
            foreach (DeviceDescription singleDevice in devices)
            {
                Console.WriteLine(singleDevice.ToString());
            }

        }
    }
}

namespace XmlRpcConnection
{

    /*
     * 
     * SHOULD NOT BE ACCESSIBLE FROM MAIN
     * --> leave out public
     * 
     */
    /// <summary>
    /// proxy interface for HomeMatic
    /// </summary>
    [XmlRpcUrl("http://192.168.0.106:2001/")]
    public interface IHomeMaticProxy : IXmlRpcProxy
    {
        /// <summary>
        /// lists any available logical devices
        /// </summary>
        /// <returns>available logical devices</returns>
        // method called listDevices will be executed via XML-RPC communication
        [XmlRpcMethod("listDevices")]
        DeviceDescription[] ListDevices();

        /// <summary>
        /// returns the status of a logical device
        /// </summary>
        /// <param name="address"></param>
        /// <param name="valueKey"></param>
        /// <returns>status of logical decice</returns>
        // method called getValue will be executed via XML-RPC communication
        [XmlRpcMethod("getValue")]
        object GetValue(string address, string valueKey);

        /// <summary>
        /// sets the status of a logical device
        /// </summary>
        /// <param name="address"></param>
        /// <param name="valueKey"></param>
        /// <param name="value"></param>
        // method called setValue will be executed via XML-RPC communication
        [XmlRpcMethod("setValue")]
        void SetValue(string address, string valueKey, object value);
    }



    /*
     * 
     * SHOULD NOT BE ACCESSIBLE FROM MAIN
     * --> leave out public
     * 
     */
    /// <summary>
    /// class DeviceDescription is a pre defined struct by HomeMatic for communicating via XML-RPC
    /// </summary>
    public class DeviceDescription
    {
        /// <summary>
        /// stes/gets type of logical devices (type = Kurzbezeichnung)
        /// </summary>
        [XmlRpcMember("TYPE")]
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// sets/gets address of a logical device
        /// </summary>
        [XmlRpcMember("ADDRESS")]
        public string Address
        {
            get;
            set;
        }

        /// <summary>
        /// sets/gets address (Seriennummer) of a parental device
        /// Ist bei physikalischen Geräten "".
        /// </summary>
        [XmlRpcMember("PARENT")]
        public string Parent
        {
            get;
            set;
        }

        /// <summary>
        /// sets/gets address (Seriennummer) of children of a logical device
        /// children of a logical device --> pyhsical device
        /// </summary>
        [XmlRpcMissingMapping(MappingAction.Ignore), XmlRpcMember("CHILDREN")]
        public string[] Children
        {
            get;
            set;
        }

        /// <summary>
        /// sets/gets parental device (just for physical devices)
        /// </summary>
        [XmlRpcMissingMapping(MappingAction.Ignore), XmlRpcMember("PARENT_TYPE")]
        public string ParentType
        {
            get;
            set;
        }

        /// <summary>
        /// sets/gets version of firmware (just for physical devices)
        /// </summary>
        [XmlRpcMissingMapping(MappingAction.Ignore), XmlRpcMember("FIRMWARE")]
        public string Firmware
        {
            get;
            set;
        }

        /// <summary>
        /// direction (send / receive / not connectable)
        /// --> just for physical devices
        /// </summary>
        [XmlRpcMissingMapping(MappingAction.Ignore), XmlRpcMember("DIRECTION")]
        public int Direction
        {
            get;
            set;
        }

        /// <summary>
        /// checks if logical device is a physical device
        /// </summary>
        /// <returns>
        /// true if it is a logical device
        /// </returns>
        public bool IsDevice()
        {
            return string.IsNullOrEmpty(this.Parent);
        }

        /// <summary>
        /// checks if logical device is a channel
        /// </summary>
        /// <returns>
        /// true if it is a channel
        /// </returns>
        public bool IsChannel()
        {
            return !this.IsDevice();
        }

        /// <summary>
        /// shows the address of a logical device as string
        /// </summary>
        /// <returns>
        /// address as string
        /// </returns>
        public override string ToString()
        {
            return this.Address;
        }

        /// <summary>
        /// describes the direction as string
        /// </summary>
        /// <returns>
        /// described direction
        /// </returns>
        public string GetCategory()
        {
            switch (this.Direction)
            {
                case 0:
                    return "nicht verknüpfbar";
                case 1:
                    return "Sender (Sensor)";
                case 2:
                    return "Empfänger (Aktor)";
                default:
                    return string.Empty;
            }
        }
    }

    public class HomeMatic
    {
        /*
         * 
         * FRIEND CLASS OF IHomeMaticProxy & DeviceDescription?
         * 
         * 
         */
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

