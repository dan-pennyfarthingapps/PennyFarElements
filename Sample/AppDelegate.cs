using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using MonoTouch.Dialog;
using PennyFarElements;

namespace Sample
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


			Section customentry = new Section("Loop through the entries");
			CustomKeyboardEntryElement firstElement = new CustomKeyboardEntryElement("entry 1", "", "");
			CustomKeyboardEntryElement secondElement = new CustomKeyboardEntryElement("entry 2", "", "");
			CustomKeyboardEntryElement thirdElement = new CustomKeyboardEntryElement("entry 3", "", "");

			// Now set up the element order so it can go through them correctly
			firstElement.NextElement = secondElement;
			secondElement.PrevElement = firstElement;
			secondElement.NextElement = thirdElement;
			thirdElement.PrevElement = secondElement;

			customentry.Add(firstElement);
			customentry.Add(secondElement);
			customentry.Add(thirdElement);

			root.Add(customentry);

			Section subSection = new Section("Section with sub roots");

			CustomRootElement subRoot = new CustomRootElement("Multi Options");
			Section stuffSect = new Section("Showing Stuff");
			StringElement stuff1 = new StringElement("Stuff");
			StringElement stuff2 = new StringElement("More stuff");

			TimeWithSecondsPickerElement tepe = new TimeWithSecondsPickerElement("new pick", new TimeSpan(4,3,1));


			stuffSect.Add(stuff1);
			stuffSect.Add(stuff2);

			stuffSect.Add(tepe);

			subRoot.BackgroundImage= "images/norwegian_rose";

			subRoot.Add(stuffSect);

			subSection.Add(subRoot);

			root.Add(subSection);

			cDVC = new CustomDialogViewController(root);

			// Add custom background
			cDVC.BackgroundImage = "images/norwegian_rose";

			nav = new UINavigationController(cDVC);
			
			
			window.RootViewController = nav;
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}

}

