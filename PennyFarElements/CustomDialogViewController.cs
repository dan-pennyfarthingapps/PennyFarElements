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
			/** Buttons
			if (pushing) {
				
				UISegmentedControl backControl = new UISegmentedControl (RectangleF.FromLTRB (0, 20, 50, 20));
				//backControl.TintColor = buttonColor;
				backControl.ControlStyle = UISegmentedControlStyle.Plain;
				backControl.InsertSegment (UIImage.FromBundle ("icons/backButton"), 0, false);
				backControl.Momentary = true;
				
				UIBarButtonItem backButton = new UIBarButtonItem (backControl);
				//backButton.Title = "Back";
				//backButton.Style = UIBarButtonItemStyle.Bordered;
				//backButton.TintColor = buttonColor;
				backControl.ValueChanged += (object sender, EventArgs e) => {
					this.NavigationController.PopViewControllerAnimated (true);
				}; 
				this.NavigationItem.LeftBarButtonItem = backButton;
			}
			**/
			
			
		}
	}

}

