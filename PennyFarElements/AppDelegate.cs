using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using MonoTouch.Dialog;


namespace PennyFarElements
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		CustomDialogViewController cDVC;
		CustomRootElement root;
		UINavigationController nav;
		
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			root = new CustomRootElement("Penny Far Elements Sample");
			Section section = new Section("Counter Element", "Has a ValueChanged event") {
				new ResponsiveCounterElement("Responsive Counter", "10.0")
			};
			root.Add(section);
			cDVC = new CustomDialogViewController(root);
			nav = new UINavigationController(cDVC);
			
			
			window.RootViewController = nav;
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}

}

