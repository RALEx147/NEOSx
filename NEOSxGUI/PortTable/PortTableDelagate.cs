using System;
using AppKit;
using CoreGraphics;
using Foundation;
using System.Collections;
using System.Collections.Generic;

namespace NEOSxGUI
{
    public class PortTableDelegate : NSTableViewDelegate
    {
        
        private const string CellIdentifier = "PortCell";

        private PortTableDataSource DataSource;


        public PortTableDelegate(PortTableDataSource datasource)
        {
            this.DataSource = datasource;
        }
        public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            
            NSTextField view = (NSTextField)tableView.MakeView(CellIdentifier, this);
            if (view == null)
            {
                view = new NSTextField();
                view.Identifier = CellIdentifier;
                view.BackgroundColor = NSColor.Clear;
                view.Bordered = false;
                view.Selectable = false;
                view.Editable = false;
            }

            // Setup view based on the column selected
            switch (tableColumn.Title)
            {
                case "IP":
                    view.StringValue = DataSource.ports[(int)row].IP;
                    break;
                case "Port":
                    view.StringValue = DataSource.ports[(int)row].Port;
                    break;
                case "Listen":
                    view.StringValue = DataSource.ports[(int)row].Listen;
                    break;
                case "Height":
                    view.StringValue = DataSource.ports[(int)row].Height;
                    break;
            }

            return view;
        }
    }
}