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
	public class CustomRootElement : RootElement {
		
		private UIColor buttonColor;
		
		public CustomRootElement (string caption) : base (caption)
		{

		}
		public CustomRootElement (string caption, RadioGroup rgroup) : base (caption, rgroup)
		{

		}
		
		
		protected override MonoTouch.UIKit.UIViewController MakeViewController()
		{
			CustomDialogViewController result = new CustomDialogViewController(this, true);

			// set the background here
			UIImage background = UIImage.FromBundle ("background/escheresque");
			result.TableView.BackgroundView = null;
			result.TableView.BackgroundColor = UIColor.FromPatternImage (background);
			
			
			UISegmentedControl backControl = new UISegmentedControl(RectangleF.FromLTRB(0,20,50,20));
			backControl.TintColor = buttonColor;
			backControl.ControlStyle = UISegmentedControlStyle.Plain;
			backControl.InsertSegment(UIImage.FromBundle("icons/backButton"), 0, false);
			backControl.Momentary = true;
			
			UIBarButtonItem backButton = new UIBarButtonItem(backControl);

			backControl.ValueChanged += (object sender, EventArgs e) => {
				result.NavigationController.PopViewControllerAnimated(true);
			}; 
			result.NavigationItem.LeftBarButtonItem = backButton;
			return result;
		}
		
	}
}

