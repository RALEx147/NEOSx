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
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        AppKit.NSImageView banner { get; set; }

        [Outlet]
        AppKit.NSScrollView bg { get; set; }

        [Outlet]
        AppKit.NSScrollView buttonView { get; set; }

        [Outlet]
        AppKit.NSButton current { get; set; }

        [Outlet]
        AppKit.NSTextField height { get; set; }

        [Outlet]
        AppKit.NSTableColumn heightColum { get; set; }

        [Outlet]
        AppKit.NSTableColumn ipColum { get; set; }

        [Outlet]
        AppKit.NSTableColumn listenColum { get; set; }

        [Outlet]
        AppKit.NSTableColumn portColum { get; set; }

        [Outlet]
        AppKit.NSButton settingsButton { get; set; }

        [Outlet]
        AppKit.NSView settingsController { get; set; }

        [Outlet]
        AppKit.NSView settingsview { get; set; }

        [Outlet]
        AppKit.NSTableView table { get; set; }

        [Outlet]
        AppKit.NSTouchBar touchbar { get; set; }

        [Outlet]
        AppKit.NSButton transactionButton { get; set; }

        [Outlet]
        AppKit.NSView transactionsController { get; set; }

        [Outlet]
        AppKit.NSView transview { get; set; }

        [Outlet]
        AppKit.NSButton walletButton { get; set; }

        [Outlet]
        AppKit.NSView walletController { get; set; }

        [Outlet]
        AppKit.NSView walletview { get; set; }

        [Action ("button:")]
        partial void button (Foundation.NSObject sender);

        [Action ("exit:")]
        partial void exit (Foundation.NSObject sender);

        [Action ("settings:")]
        partial void settings (Foundation.NSObject sender);

        [Action ("transaction:")]
        partial void transaction (Foundation.NSObject sender);

        [Action ("transTouch:")]
        partial void transTouch (Foundation.NSObject sender);

        [Action ("wallet:")]
        partial void wallet (Foundation.NSObject sender);

        [Action ("walletTouch:")]
        partial void walletTouch (Foundation.NSObject sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (banner != null) {
                banner.Dispose ();
                banner = null;
            }

            if (bg != null) {
                bg.Dispose ();
                bg = null;
            }

            if (buttonView != null) {
                buttonView.Dispose ();
                buttonView = null;
            }

            if (current != null) {
                current.Dispose ();
                current = null;
            }

            if (heightColum != null) {
                heightColum.Dispose ();
                heightColum = null;
            }

            if (ipColum != null) {
                ipColum.Dispose ();
                ipColum = null;
            }

            if (listenColum != null) {
                listenColum.Dispose ();
                listenColum = null;
            }

            if (portColum != null) {
                portColum.Dispose ();
                portColum = null;
            }

            if (settingsButton != null) {
                settingsButton.Dispose ();
                settingsButton = null;
            }

            if (settingsController != null) {
                settingsController.Dispose ();
                settingsController = null;
            }

            if (settingsview != null) {
                settingsview.Dispose ();
                settingsview = null;
            }

            if (table != null) {
                table.Dispose ();
                table = null;
            }

            if (transactionButton != null) {
                transactionButton.Dispose ();
                transactionButton = null;
            }

            if (transactionsController != null) {
                transactionsController.Dispose ();
                transactionsController = null;
            }

            if (transview != null) {
                transview.Dispose ();
                transview = null;
            }

            if (walletButton != null) {
                walletButton.Dispose ();
                walletButton = null;
            }

            if (walletController != null) {
                walletController.Dispose ();
                walletController = null;
            }

            if (walletview != null) {
                walletview.Dispose ();
                walletview = null;
            }

            if (touchbar != null) {
                touchbar.Dispose ();
                touchbar = null;
            }

            if (height != null) {
                height.Dispose ();
                height = null;
            }
        }
    }
}
