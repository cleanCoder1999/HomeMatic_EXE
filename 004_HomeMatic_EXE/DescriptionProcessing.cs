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
        override public string ToString()
        {
            return "Name:_" + address
                    + "\nType: " + type
                    + "\nChannels: " + channels.ToString();
        }

    }

    class Channel
    {
        private string id;
        private string direction;
        private List<DataPoint> ListOfDataPoints;

        Channel()
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
        }
        private void createListOfDeviceDescriptions(DeviceDescription[] fieldOfDeviceDescriptions)
        {
            foreach(DeviceDescription deviceDescription in fieldOfDeviceDescriptions)
            {
                listOfDeviceDescriptions.Add(deviceDescription);
            }
        }
        private void createPhysicalDeviceOutOf(DeviceDescription deviceDescription)
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
            // assign shrunk list
            this.listOfDeviceDescriptions = listOfDeviceDescriptions;             
        }
        */
        private void addChannelsToPhysicalDevice()
        {
            Channel channel = new Channel();

            foreach (DeviceDescription deviceDescription in listOfDeviceDescriptions)
            {
                if (deviceDescription.IsChannel())
                {
                    channel.Id = deviceDescription.Address;
                    channel.Direction = deviceDescription.GetCategory();
                    physicalDevice.AddChannel(channel);
                    // remove after usage
                    listOfDeviceDescriptions.Remove(deviceDescription);
                }
                else
                    break;
            }
        }
        private void addPhysicalDeviceToList()
        {
            listOfPhysicalDevices.Add(physicalDevice);
        }

        public List<PhysicalDevice> ListOfPhysicalDevices
        {
            get;
        }

    }
}
