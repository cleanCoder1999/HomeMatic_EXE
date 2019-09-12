using System;
using System.Text;

using System.Net;
using System.Net.Sockets;


namespace HomeMatic_EXE
{
    class Program
    {
        static void Main(string[] args)
        {
           
        }
    }
}

namespace SocketConnection
{
    class ServerInformation
    {
        private IPAddress ipAddress;
        private int port;

        public ServerInformation(
                            byte[] ipAddress,
                            int port)
        {
            this.ipAddress = new IPAddress(ipAddress);
            this.port = port;
        }

        public IPAddress getIpAddress()
        {
            return ipAddress;
        }

        public int getPort()
        {
            return port;
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

            client.Connect(serverInfo.getIpAddress(),
                            serverInfo.getPort());
        }
        private void CreateClientSocket()
        {
            this.client = new Socket(
                                ipAddress.AddressFamily,
                                SocketType.Stream,
                                ProtocolType.Tcp);
        }

        public bool sendRequest(string requestString)
        {
            byte[] encodedRequest = Encoding.ASCII.GetBytes(requestString);
            int quantityOfSentBytes = client.Send(encodedRequest);

            return hasSameQuantityOfElements(
                                encodedRequest.Length,
                                quantityOfSentBytes);
        }
        private bool hasSameQuantityOfElements(int sentBytes,
                                int answeredBytes)
        {
            return sentBytes == answeredBytes;
        }

        /* ... */
    }
}
