using AppKit;
using Foundation;
using Neo.Network;
using Neo.Core;
using System.IO;
using System.ComponentModel;


namespace NEOSxGUI
{
    [Register("AppDelegate")]
    public partial class AppDelegate : NSApplicationDelegate
    {
        public AppDelegate()
        {
        }
        public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender)
        {
            return true;
        }


        public override void DidFinishLaunching(NSNotification notification)
        {
            NSApplication.SharedApplication.SetAutomaticCustomizeTouchBarMenuItemEnabled(true); // Enable custom touchbar.

            NSApplication.SharedApplication.SetTouchBar(NSApplication.SharedApplication.MainWindow.ContentViewController.TouchBar);
        }        

        public override void WillTerminate(NSNotification notification)
        {
            const string PeerStatePath = "peers.dat";
            using (FileStream fs = new FileStream(PeerStatePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                LocalNode.SaveState(fs);
            }
            MainClass mc = new MainClass();
            Blockchain.PersistCompleted -= mc.Blockchain_PersistCompleted;
            mc.ChangeWallet(null);
        }


    }

        
        

}
