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
	[Register ("TransactionVC")]
	partial class TransactionVC
	{
		[Outlet]
		AppKit.NSTextField height { get; set; }

		[Outlet]
		AppKit.NSTableView table { get; set; }

		[Action ("button:")]
		partial void button (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (height != null) {
				height.Dispose ();
				height = null;
			}

			if (table != null) {
				table.Dispose ();
				table = null;
			}
		}
	}
}
