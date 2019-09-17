using CookComputing.XmlRpc;

namespace HomeMatic
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
    //[XmlRpcUrl("http://172.16.17.3:2001/")]
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

        /// <summary>
        /// sends a request to the HomeMatic device
        /// </summary>
        /// <param name="callerId"></param>
        /// <returns>true --> HomeMatic ON
        ///          false --> HomeMatic OFF</returns>
        [XmlRpcMethod("ping")]
        bool Ping(string callerId);

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
}
