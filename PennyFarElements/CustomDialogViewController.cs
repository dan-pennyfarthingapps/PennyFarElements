//
// 	CustomDialogViewController.cs
// 	Author: Daniel Wiseman
//	PennyFarElements: http://penfarapps.com/Sx1v5n
//
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;


using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ElementPack;

namespace PennyFarElements
{
	public class CustomDialogViewController : DialogViewController {
		
		
		private UIImage navBarBackgroundImage;
		private bool pushing;
		private UIColor buttonColor;
		private UIColor navBarColor;
		private UIImage backgroundImage;

		private UIBarButtonItem backButton;
		private UISegmentedControl backControl;
		private bool customBackButton;

		public CustomDialogViewController (RootElement root) : base (root)
		{
			pushing = false;
			this.SetDefaults();
		}
		
		public CustomDialogViewController (RootElement root, bool back) : base(root, back)
		{
			pushing = back;
			this.SetDefaults();
		}

		private void SetDefaults ()
		{
			buttonColor = UIColor.FromRGB (223, 159, 53);
			backgroundImage = UIImage.FromBundle("images/shl");
			navBarColor = UIColor.FromRGB (223, 159, 53);
			customBackButton = false;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		// Send a string value to the image. Loads via FromBundle for Retina Support
		public string BackgroundImage {
			set { this.backgroundImage = UIImage.FromBundle(value); }
		}

		// UIColor for the Nav Bar
		public UIColor NavigationBarColor {
			set { this.navBarColor = value; }
		}

		// Sets a custom back button from an image
		// Sample: RectangleF.FromLTRB (0, 20, 50, 20)
		public void SetCustomBackButton (UIImage buttonImage, RectangleF locationAndSize)
		{
			backControl = new UISegmentedControl (locationAndSize);

			backControl.ControlStyle = UISegmentedControlStyle.Plain;
			backControl.InsertSegment (buttonImage, 0, false);
			backControl.Momentary = true;
			
			backButton = new UIBarButtonItem (backControl);
			
			customBackButton = true;
		}
	
		
		public override void LoadView ()
		{
			base.LoadView ();
			// Clear the Table View
			TableView.BackgroundColor = UIColor.Clear;
			TableView.BackgroundView = null;


			this.View.BackgroundColor = UIColor.FromPatternImage (this.backgroundImage);
			

			if (this.NavigationController != null) {
					this.NavigationController.NavigationBar.TintColor = this.navBarColor;
			}

			if (pushing && customBackButton) {
				
				this.backControl.ValueChanged += (object sender, EventArgs e) => {
					this.NavigationController.PopViewControllerAnimated (true);
				}; 

				this.NavigationItem.LeftBarButtonItem = backButton;
			}

			
			
		}
	}

}

