using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _004_HomeMatic_EXE
{
    class Client
    {
        static void Blub(string[] args)
        {
            try
            {
                // gets IPHostEntry from IP address of local computer
                // IPHostEntry ... ip address associated with hostname and aliasses
                IPHostEntry IpHostEntry = Dns.GetHostEntry(Dns.GetHostName());
                // read ip address from local address
                IPAddress IpAddress = IpHostEntry.AddressList[1];
                // class IPEndPoint combines port number and ip address
                IPEndPoint localEndPoint = new IPEndPoint(IpAddress, 11111);

                // create socket
                Socket clientSocket = new Socket(IpAddress.AddressFamily
                                                    , SocketType.Stream
                                                    , ProtocolType.Tcp);

                try
                {
                    // connect to server
                    clientSocket.Connect(localEndPoint);

                    Console.WriteLine("client connected to: {0}"
                                        , clientSocket.RemoteEndPoint.ToString());

                    // message to be sent via socket
                    byte[] messageToSend = Encoding.ASCII.GetBytes("First connection<EOF>");
                    // send message
                    int returnValueOfSend = clientSocket.Send(messageToSend);

                    byte[] encodedReceivedMessage = new byte[1024];

                    // read from server
                    int returnValueOfReicv = clientSocket.Receive(encodedReceivedMessage);
                    string decodedReceivedMessage = Encoding.ASCII.GetString(encodedReceivedMessage);

                    Console.WriteLine("server sent: {0}"
                                        , decodedReceivedMessage);

                    // shutdown and close connection
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();

                } // Manage of Socket's Exceptions 
                catch (ArgumentNullException ane)
                {

                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }

                catch (SocketException se)
                {

                    Console.WriteLine("SocketException : {0}", se.ToString());
                }

                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
