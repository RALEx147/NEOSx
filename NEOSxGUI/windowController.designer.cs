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
    [Register ("windowController")]
    partial class windowController
    {
        [Outlet]
        AppKit.NSTouchBar touchbar { get; set; }

        [Action ("toWallet:")]
        partial void toWallet (Foundation.NSObject sender);

        [Action ("transTouch:")]
        partial void transTouch (Foundation.NSObject sender);

        [Action ("walletTouch:")]
        partial void walletTouch (Foundation.NSObject sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (touchbar != null) {
                touchbar.Dispose ();
                touchbar = null;
            }
        }
    }
}
