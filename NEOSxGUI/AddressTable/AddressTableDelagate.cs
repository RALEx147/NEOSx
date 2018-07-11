using System;
using AppKit;
using CoreGraphics;
using Foundation;
using System.Collections;
using System.Collections.Generic;

namespace NEOSxGUI
{
    public class AddressTableDelegate : NSTableViewDelegate
    {

        private const string CellIdentifier = "address";
        private AddressTableDataSource DataSource;
        private WalletVC wvc;
        public AddressTableDelegate(WalletVC wvc, AddressTableDataSource datasource)
        {
            this.DataSource = datasource;
            this.wvc = wvc;
        }

        public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            NSTableCellView view = (NSTableCellView)tableView.MakeView(CellIdentifier, this);
            if (view == null)
            {
                view = new NSTableCellView();

                var click = new
                    NSButton(new CGRect(0, -10, 400, 42));
                click.SetButtonType(NSButtonType.MomentaryPushIn);
                click.BezelStyle = NSBezelStyle.RegularSquare;
                click.AlphaValue = 0;
                click.Title = "";
                click.Tag = row;

                click.Activated += (sender, e) =>
                {
                    if (DataSource.addresses[(int)row].extended && (int)row != 0)
                    {
                        DataSource.addresses[(int)row].top = true;
                        wvc.update();
                        var btw = sender as NSButton;

                    };
                };




                view.TextField = new NSTextField(new CGRect(0,0,400,35));
                view.Identifier = CellIdentifier;
                view.TextField.Font = NSFont.BoldSystemFontOfSize(28);
                view.TextField.BackgroundColor = NSColor.Clear;
                view.TextField.Bordered = false;
                view.TextField.Selectable = false;
                view.TextField.Editable = false;
                view.AddSubview(view.TextField);
                view.AddSubview(click);
            }

            // Setup view based on the column selected
            switch (tableColumn.Title)
            {
                case "address":
                    view.TextField.StringValue = DataSource.addresses[(int)row].Address;
                    foreach (NSView subview in view.Subviews)
                    {
                        var btw = subview as NSButton;
                        if (btw != null)
                        {
                            btw.Tag = row;
                        }
                    }

                    break;
            }

            return view;
        }
    }
}