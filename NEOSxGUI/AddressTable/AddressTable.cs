using System;
namespace NEOSxGUI
{
    public class AddressTable
    {
        public string Address { get; set; } = "";
        public bool extended { get; set; } = false;
        public bool top { get; set; } = false;
        public AddressTable()
        {

        }

        public AddressTable(string Address)
        {
            this.Address = Address;
        }
    }
}
