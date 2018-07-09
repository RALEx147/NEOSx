using System;
using AppKit;
using Foundation;


namespace NEOSxGUI
{

    public partial class ViewController : NSViewController
    {



        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void ViewWillAppear()
        {
            base.ViewWillAppear();
            ////View.Window.BackgroundColor = NSColor.Red;
            //var shadowPath = UIBezierPath(rect: view.bounds)

            //view.layer.masksToBounds = false
            //view.layer.shadowColor = UIColor.black.cgColor
            //view.layer.shadowOffset = CGSize(width: 0.0, height: 5.0)
            //view.layer.shadowOpacity = 0.5
            //view.layer.shadowPath = shadowPath.cgPath

            //settingsview.RemoveFromSuperview();
            walletview.RemoveFromSuperview();
            transview.RemoveFromSuperview();

            addShadowBanner();
            addShadowbg();

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







        partial void exit(NSObject sender)
        {
            NSApplication.SharedApplication.Terminate(sender);
        }

        partial void settings(NSObject sender)
        {
            
            current.AlphaValue = 0;

            View.AddSubview(transview);
            settingsview.RemoveFromSuperview();
            walletview.RemoveFromSuperview();

            settingsview.AlphaValue = 1;
            current.SetFrameOrigin(new CoreGraphics.CGPoint(x: 29, y: 58));

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
            current.AlphaValue = 1;

        }

        partial void transaction(NSObject sender)
        {
            
            View.AddSubview(walletview);
            transview.RemoveFromSuperview();
            settingsview.RemoveFromSuperview();

            current.AlphaValue = 0;
            current.SetFrameOrigin(new CoreGraphics.CGPoint(x: 29, y: 541));
            current.AlphaValue = 1;
        }
        partial void wallet(NSObject sender)
        {

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

            //NEP6Wallet w = new NEP6Wallet("test.json");
        //    //w.Unlock("1");
        //    //var account = w.GetAccounts();
        //    //foreach (var item in account)
        //    //{
        //    //    Console.WriteLine($"address: {item.Address}");
        //    //}


        //    //Console.WriteLine($"address: {account.Address}");

        //    //Console.WriteLine($" pubkey: {account.GetKey().PublicKey.EncodePoint(true).ToHexString()}");

        //}






    }
}
