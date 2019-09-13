using System;
using System.Text;

using System.Net;
using System.Net.Sockets;
using SocketConnection;



namespace HomeMatic_EXE
{
    class Program
    {
        static void Main(string[] args)
        {
            
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
        }
    }
}

namespace SocketConnection
{
    class ServerInformation
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
            get
            {
                return ipAddress;
            }
        }

        public int GetConstPort
        {
            get
            {
                return constPort;
            }
        }

        public int GetPort
        {
            get
            {
                return port;
            }
        }

    }

    class Client
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
            } catch (Exception)
            {
                throw;
            }
            client.Close();
        }

        override public string ToString()
        { 
            return "IP: " + ipAddress.ToString() + "\nPort: " + port + "\nclient.Connected: " + client.Connected;
        }
    }

    class HTTP_RequestCreator
    {
        private string completeHttpRequest;


        private const string requestMethod = "GET";
        private string uri;
        private string httpVersion;

        /*struct requestLine
        {
            private string eleme;
        }*/       

        private string CreateRequestLine()
        {

            return "";
        }

    }

}
