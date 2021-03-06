//
// 	CustomRootElement.cs
// 	Author: Daniel Wiseman
//	PennyFarElements: http://penfarapps.com/Sx1v5n
//
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ElementPack;

namespace PennyFarElements
{
	public class CustomRootElement : RootElement, IEnumerable, IEnumerable<Section> {

		private UIImage backgroundImage;
		
		private UIBarButtonItem backButton;
		private UISegmentedControl backControl;
		private bool customBackButton;

		public CustomRootElement (string caption) : base (caption)
		{
			SetDefaults();
		}
		public CustomRootElement (string caption, RadioGroup rgroup) : base (caption, rgroup)
		{
			SetDefaults();
		}

		private void SetDefaults ()
		{
			backgroundImage = UIImage.FromBundle("images/shl");
			customBackButton = false;
		}

		// Send a string value to the image. Loads via FromBundle for Retina Support
		public string BackgroundImage {
			set { this.backgroundImage = UIImage.FromBundle(value); }
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


		
		protected override MonoTouch.UIKit.UIViewController MakeViewController ()
		{
			CustomDialogViewController result = new CustomDialogViewController (this, true);

			// set the background here
			UIImage background = backgroundImage;
			result.TableView.BackgroundView = null;
			result.TableView.BackgroundColor = UIColor.FromPatternImage (background);

			
			if (customBackButton) {

				backControl.ValueChanged += (object sender, EventArgs e) => {
					if(result.NavigationController != null) 
						result.NavigationController.PopViewControllerAnimated (true);
				}; 

				result.NavigationItem.LeftBarButtonItem = backButton;
			}
			return result;
		}




		// allows subclasses of CustomRootElement to be used in a foreach statement
		IEnumerator IEnumerable.GetEnumerator ()
		{
			for(int i = 0; i < base.Count; i++) {
				yield return base[i];
			}
		}

		IEnumerator<Section> IEnumerable<Section>.GetEnumerator ()
		{
			for(int i = 0; i < base.Count; i++) {
				yield return base[i];
			}
		}
		

	}
}

