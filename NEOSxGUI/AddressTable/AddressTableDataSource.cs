using System;
using AppKit;
using CoreGraphics;
using Foundation;
using System.Collections;
using System.Collections.Generic;

namespace NEOSxGUI
{
    public class AddressTableDataSource : NSTableViewDataSource
    {
        public List<AddressTable> addresses= new List<AddressTable>();

        public AddressTableDataSource()
        {
        }

        public override nint GetRowCount(NSTableView tableView)
        {
            return addresses.Count;
        }


    
       
    
    }
}
