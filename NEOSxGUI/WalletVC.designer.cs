// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace NEOSxGUI
{
	[Register ("WalletVC")]
	partial class WalletVC
	{
		[Outlet]
		AppKit.NSTableCellView cell { get; set; }

		[Outlet]
		AppKit.NSButton openWallet { get; set; }

		[Outlet]
		AppKit.NSScrollView scroll { get; set; }

		[Outlet]
		AppKit.NSButton select { get; set; }

		[Outlet]
		AppKit.NSButton show { get; set; }

		[Outlet]
		AppKit.NSTableView table { get; set; }

		[Action ("button:")]
		partial void button (Foundation.NSObject sender);

		[Action ("walletOpen:")]
		partial void walletOpen (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (cell != null) {
				cell.Dispose ();
				cell = null;
			}

			if (scroll != null) {
				scroll.Dispose ();
				scroll = null;
			}

			if (select != null) {
				select.Dispose ();
				select = null;
			}

			if (show != null) {
				show.Dispose ();
				show = null;
			}

			if (table != null) {
				table.Dispose ();
				table = null;
			}

			if (openWallet != null) {
				openWallet.Dispose ();
				openWallet = null;
			}
		}
	}
}
