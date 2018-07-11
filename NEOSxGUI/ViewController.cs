using System;
using AppKit;
using Foundation;
using Neo.Network;
using System.Threading;
using System.Timers;
using Neo.Implementations.Blockchains;

using AppKit;
using System;
using Neo.Core;
using Neo.Implementations.Blockchains.LevelDB;
using Neo.Network;
using Properties;
using Neo.Wallets;
using System.IO;
using Neo;
using System.Threading.Tasks;
using Neo.Cryptography;
using Neo.IO;
using Neo.VM;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using Properties;

namespace NEOSxGUI
{

    public partial class ViewController : NSViewController
    {
        
        public override NSTouchBar TouchBar => touchbar;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }
        static System.Timers.Timer timer = new System.Timers.Timer();
        public override void ViewWillAppear()
        {
            base.ViewWillAppear();
            walletview.RemoveFromSuperview();
            transview.RemoveFromSuperview();

            addShadowBanner();
            addShadowbg();


            timer = new System.Timers.Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            BeginInvokeOnMainThread(() =>
            {
                var wh = "0";
                if (MainClass.CurrentWallet != null){
                    wh = MainClass.CurrentWallet.WalletHeight.ToString();
                }
                var bh = Blockchain.Default.Height.ToString();
                var hh = Blockchain.Default.HeaderHeight.ToString();

                height.StringValue = wh + " / " + bh + " / " + hh;

            });


        }

        public void addShadowBanner(){
            var shadow = new NSShadow();
            shadow.ShadowOffset = new CoreGraphics.CGSize(width: 0, height: 25);
            shadow.ShadowBlurRadius = 20;
            shadow.ShadowColor = NSColor.Black;

            banner.Superview.WantsLayer = true;
            banner.Shadow = shadow;
        }
        public void addShadowbg()
        {
            var shadow = new NSShadow();
            shadow.ShadowOffset = new CoreGraphics.CGSize(width: 50, height: 0);
            shadow.ShadowBlurRadius = 22;
            shadow.ShadowColor = NSColor.Black;

            bg.Superview.WantsLayer = true;
            bg.Shadow = shadow;
        }
            

        public ViewController(IntPtr handle) : base(handle)
        {
            
        }
        public override void LoadView()
        {
            base.LoadView();
        }
        public override void ViewWillDisappear()
        {
            
            base.ViewWillDisappear();
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
            }
        }



        partial void transTouch(NSObject sender)
        {
            toTransView();
        }

        partial void walletTouch(NSObject sender)
        {
            toWalletView();

        }



        partial void exit(NSObject sender)
        {
            NSApplication.SharedApplication.Terminate(sender);
        }

        partial void settings(NSObject sender){toSettingsView();}
        private void toSettingsView(){
            current.AlphaValue = 0;
            View.AddSubview(transview);
            settingsview.RemoveFromSuperview();
            walletview.RemoveFromSuperview();
            settingsview.AlphaValue = 1;
            current.SetFrameOrigin(new CoreGraphics.CGPoint(x: 29, y: 58));
            current.AlphaValue = 1;
        }

        partial void transaction(NSObject sender){toTransView();}
        private void toTransView(){
            View.AddSubview(walletview);
            transview.RemoveFromSuperview();
            settingsview.RemoveFromSuperview();
            current.AlphaValue = 0;
            current.SetFrameOrigin(new CoreGraphics.CGPoint(x: 29, y: 541));
            current.AlphaValue = 1;
        }

        partial void wallet(NSObject sender){toWalletView();}
        private void toWalletView(){
            transview.RemoveFromSuperview();
            walletview.RemoveFromSuperview();
            View.AddSubview(settingsview);
            current.AlphaValue = 0;
            current.SetFrameOrigin(new CoreGraphics.CGPoint(x: 29, y: 600));
            current.AlphaValue = 1;
        }







        //partial void button2(NSObject sender)
        //{

        //    //Console.WriteLine(Blockchain.Default.GetValidators());
        //    //Console.WriteLine(Blockchain.Default.GetType());
        //    //Console.WriteLine(LocalNode.GetRemoteNodes());


        //    //foreach (var item in account)
        //    //{
        //    //    Console.WriteLine($"address: {item.Address}");
        //    //}


        //    //Console.WriteLine($"address: {account.Address}");

        //    //Console.WriteLine($" pubkey: {account.GetKey().PublicKey.EncodePoint(true).ToHexString()}");

        //}





            //fucking animations are so fucking hard on mac os compared to ios
        //shitty fucking xamarin.mac framework

        //NSAnimationContext.RunAnimation((NSAnimationContext obj) =>
        //{
        //    obj.Duration = 1;
        //    current.Animator = 0;

        //}, null);
        //var aa = new CABasicAnimation();
        //aa.BeginTime = 1.0;
        //aa.To = new NSNumber(1.0);
        //aa.Duration = 0.5;
        //aa.KeyPath = "opacity";
        //current.Layer.AddAnimation(aa, "fadeIn");

    }
}
