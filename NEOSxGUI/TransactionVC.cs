// This file has been autogenerated from a class added in the UI designer.

using System;

using Neo.Network;
using Neo.Core;

using Foundation;
using AppKit;

namespace NEOSxGUI
{
	public partial class TransactionVC : NSView
	{
		public TransactionVC (IntPtr handle) : base (handle)
		{
		}


        partial void button(NSObject sender)
        {
            
            RemoteNode[] nodes = MainClass.LocalNode.GetRemoteNodes();
            var DataSource = new PortTableDataSource();

            for (int i = 0; i < nodes.Length; i++)
            {
                DataSource.ports.Add(new PortTable(nodes[i].RemoteEndpoint.Address.ToString(), nodes[i].RemoteEndpoint.Port.ToString(), (nodes[i].ListenerEndpoint?.Port ?? 0).ToString(), (nodes[i].Version?.StartHeight ?? 0).ToString()));

            }

            table.DataSource = DataSource;
            table.Delegate = new PortTableDelegate(DataSource);

            table.ReloadData();
            height.StringValue = Blockchain.Default.HeaderHeight.ToString();
        }
	}
}
