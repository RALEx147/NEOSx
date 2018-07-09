using System;
using AppKit;
using CoreGraphics;
using Foundation;
using System.Collections;
using System.Collections.Generic;

namespace NEOSxGUI
{
    public class PortTableDataSource : NSTableViewDataSource
    {
        public List<PortTable> ports= new List<PortTable>();

        public PortTableDataSource()
        {
        }

        public override nint GetRowCount(NSTableView tableView)
        {
            return ports.Count;
        }

    }
}
