using System;

using System.Collections.Generic;
using XmlRpcConnection;


namespace DescriptionProccessing
{
    class PhysicalDevice
    {
        private string name;
        // Type des Geräts
        private string type;
        private List<Channel> channels;

        public PhysicalDevice()
        {
            channels = new List<Channel>();
        }

        public string Name
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
            return "Name:_" + name
                    + "\nType: " + type
                    + "\nChannels: " + channels.ToString();
        }

    }

    class Channel
    {
        private string name;
        private string direction;

        public string Name
        {
            get;
            set;
        }
        public string Direction
        {
            get;
            set;
        }
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

            physicalDevice.Name = deviceDescription.Address;
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
