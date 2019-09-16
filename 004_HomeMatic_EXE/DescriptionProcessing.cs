using System;

using System.Collections.Generic;
using XmlRpcConnection;


namespace DescriptionProccessing
{
    class PhysicalDevice
    {
        private string address;
        // Type des Geräts
        private string type;
        private List<Channel> channels;

        public PhysicalDevice()
        {
            channels = new List<Channel>();
        }

        public string Address
        {
            get;
            set;
        }
        public string Type
        {
            get;
            set;
        }

        public void AddChannel(Channel channel)
        {
            channels.Add(channel);
        }
        public string ToString()
        {
            return "Address: " + address
                    + "\nType: " + type
                    + "\nChannels: " + channels.ToString();
        }

    }

    class Channel
    {
        private string id;
        private string direction;
        private List<DataPoint> ListOfDataPoints;

        public Channel()
        {
            ListOfDataPoints = new List<DataPoint>();
        }

        public string Id
        {
            get;
            set;
        }
        public string Direction
        {
            get;
            set;
        }
        public List<DataPoint> DataPoints
        {
            get;
            set;
        }

        public void addDataPoint(DataPoint dataPoint)
        {
            ListOfDataPoints.Add(dataPoint);
        }

    }

    class DataPoint
    {
        private string name;
        private int id;
        private string value;
        private int valueType;
        private string valueUnit;
        private string timestamp;

        /*
         * 
         * 
         */

    }

    class DeviceProcessor
    {
        private PhysicalDevice physicalDevice;
        private List<PhysicalDevice> listOfPhysicalDevices;
        private List<DeviceDescription> listOfDeviceDescriptions;

        /*
        DeviceProcessor(DeviceDescription[] deviceDescriptions)
        {
            this.deviceDescriptions = deviceDescriptions;
        }
        */
        /*
    public void addPhysicalDevices()
    {
        PhysicalDevice physicalDevice;

        foreach(DeviceDescription deviceDescription in fieldOfDeviceDescriptions)
        {
            if (deviceDescription.IsDevice())
            {
                //physicalDevice = createPhysicalDeviceOutOf(deviceDescription);
                //listOfPhysicalDevices.Add(physicalDevice);
            }
        }
    }
    */

        public DeviceProcessor()
        {
            listOfDeviceDescriptions = new List<DeviceDescription>();
            physicalDevice = new PhysicalDevice();
            listOfPhysicalDevices = new List<PhysicalDevice>();
        }

        /*
        public void convert(DeviceDescription[] fieldOfDeviceDescriptions)
        {
            createListOfDeviceDescriptions(fieldOfDeviceDescriptions);
            Console.WriteLine("created list");

            foreach (DeviceDescription deviceDescription in listOfDeviceDescriptions)
            {
                createPhysicalDeviceOutOf(deviceDescription);
                Console.WriteLine("created physical device");

                listOfDeviceDescriptions.Remove(deviceDescription);
                Console.WriteLine("removed element");

                addChannelsToPhysicalDevice();
                Console.WriteLine("added channels");

                addPhysicalDeviceToList();
            }
        }*/
        public void Convert(DeviceDescription[] fieldOfDeviceDescriptions)
        {
            createListOfDeviceDescriptions(fieldOfDeviceDescriptions);
            Console.WriteLine("created list");
            int i = 0;

            while (i < listOfDeviceDescriptions.Count)
            {
                ++i;

                CreatePhysicalDeviceOutOf(listOfDeviceDescriptions[0]);
                //Console.WriteLine(i + ": created physical device");
                Console.WriteLine(i + ": physical device: " + physicalDevice.Address);
                //Console.WriteLine(i + ": physical device: " + physicalDevice.Type);

                listOfDeviceDescriptions.RemoveAt(0);
                //Console.WriteLine(i + ": removed element");

                AddChannelsToPhysicalDevice();
                //Console.WriteLine(i + ": added channels");
                Console.WriteLine(i + ": physical device: " + physicalDevice.Address + "\n" + physicalDevice.ToString());

                AddPhysicalDeviceToList();
                Console.WriteLine(i + "list member: " + listOfPhysicalDevices[0].Address);
            }

            Console.WriteLine("ListOfPhysicalDevices has " + listOfPhysicalDevices.Count + " members");

        }
        private void createListOfDeviceDescriptions(DeviceDescription[] fieldOfDeviceDescriptions)
        {
            foreach(DeviceDescription deviceDescription in fieldOfDeviceDescriptions)
            {
                listOfDeviceDescriptions.Add(deviceDescription);
            }
        }
        private void CreatePhysicalDeviceOutOf(DeviceDescription deviceDescription)
        {
            PhysicalDevice physicalDevice = new PhysicalDevice();

            physicalDevice.Address = deviceDescription.Address;
            physicalDevice.Type = deviceDescription.Type;

            this.physicalDevice = physicalDevice;
        }
        /*
        private void addChannelsFrom(List<DeviceDescription> listOfDeviceDescriptions)
        {
            Channel channel = new Channel();

            foreach(DeviceDescription deviceDescription in listOfDeviceDescriptions)
            {
                if (deviceDescription.IsChannel())
                {
                    channel.Name = deviceDescription.Address;
                    channel.Direction = deviceDescription.GetCategory();
                    physicalDevice.AddChannel(channel);
                    // remove after usage
                    listOfDeviceDescriptions.Remove(deviceDescription);
                }
                else
                    break;
            }      
        }
        */
        private void AddChannelsToPhysicalDevice()
        {
            Channel channel = new Channel();
            int i = 0;

            while (i < listOfDeviceDescriptions.Count)
            {
                ++i;

                if (listOfDeviceDescriptions[0].IsChannel())
                {
                    channel.Id = listOfDeviceDescriptions[0].Address;
                    channel.Direction = listOfDeviceDescriptions[0].GetCategory();
                    physicalDevice.AddChannel(channel);
                    // remove after usage
                    listOfDeviceDescriptions.RemoveAt(0);
                }
                else
                    break;
            }
        }
        private void AddPhysicalDeviceToList()
        {
            listOfPhysicalDevices.Add(physicalDevice);
        }

        public List<PhysicalDevice> ListOfPhysicalDevices
        {
            get;
        }

    }
}
