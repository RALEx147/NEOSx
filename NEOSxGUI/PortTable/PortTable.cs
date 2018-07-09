using System;
namespace NEOSxGUI
{
    public class PortTable
    {
        public string IP { get; set; } = "";
        public string Port { get; set; } = "";
        public string Listen { get; set; } = "";
        public string Height { get; set; } = "";
  
        public PortTable()
        {

        }

        public PortTable(string ip, string port, string listen, string height)
        {
            this.IP = ip;
            this.Port = port;
            this.Listen = listen;
            this.Height = height;
        }
    }
}
