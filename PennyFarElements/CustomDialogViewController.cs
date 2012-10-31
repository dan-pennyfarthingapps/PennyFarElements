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
		
		public CustomDialogViewController (RootElement root) : base (root)
		{
			
			buttonColor = new UIColor(223.0f, 159.0f, 53.0f, 1.0f);
			pushing = false;
		}
		
		public CustomDialogViewController (RootElement root, bool back) : base(root, back)
		{
			pushing = back;
			buttonColor = new UIColor(223.0f, 159.0f, 53.0f, 1.0f);
		}
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			
			
			
		}
		
		public override void LoadView ()
		{
			base.LoadView ();
			TableView.BackgroundColor = UIColor.Clear;
			TableView.BackgroundView = null;
			//NavigationItem.BackBarButtonItem.TintColor = buttonColor;
			UIImage background = UIImage.FromBundle ("background/escheresque");
			this.View.BackgroundColor = UIColor.FromPatternImage (background);
			
			// Bar
			if (this.NavigationController != null) {
				if (UIDevice.CurrentDevice.CheckSystemVersion (5, 0)) {
					this.navBarBackgroundImage = UIImage.FromBundle ("background/navBar");
					//this.NavigationController.NavigationItem.Title = "";
					this.NavigationController.NavigationBar.SetBackgroundImage (this.navBarBackgroundImage, UIBarMetrics.Default);
					this.NavigationController.NavigationBar.TintColor = buttonColor;
				} else {
					this.NavigationController.NavigationBar.TintColor = buttonColor;
				}
				
				
				
			}
			
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
			
			
			
		}
	}

}

