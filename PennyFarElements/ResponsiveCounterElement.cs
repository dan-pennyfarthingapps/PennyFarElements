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
	public class ResponsiveCounterElement : CounterElement
	{

			
			private UIColor backgroundColor;

			public ResponsiveCounterElement (string caption, string counter) : base (caption, counter)
			{
				backgroundColor = UIColor.Black;
			}

			public ResponsiveCounterElement (string caption, string counter, UIImage backgroundImage) : base (caption, counter)
			{
				
				backgroundColor = UIColor.FromPatternImage(backgroundImage);
			}
				
			public event EventHandler<EventArgs> ValueChanged;
			
			static RectangleF PickerFrameWithSize (SizeF size)
			{                                                                                                                                    
				var screenRect = UIScreen.MainScreen.ApplicationFrame;
				float fY = 0, fX = 0;
				
				switch (UIApplication.SharedApplication.StatusBarOrientation){
				case UIInterfaceOrientation.LandscapeLeft:
				case UIInterfaceOrientation.LandscapeRight:
					fX = (screenRect.Height - size.Width) /2;
					fY = (screenRect.Width - size.Height) / 2 -17;
					break;
					
				case UIInterfaceOrientation.Portrait:
				case UIInterfaceOrientation.PortraitUpsideDown:
					fX = (screenRect.Width - size.Width) / 2;
					fY = (screenRect.Height - size.Height) / 2 - 25;
					break;
				}
				
				return new RectangleF (fX, fY, size.Width, size.Height);
			}                                                                                                                                    
			
			class MyViewController : UIViewController {
				CounterElement container;
				
				public MyViewController (CounterElement container)
				{
					this.container = container;
				}
				
				public override void ViewWillDisappear (bool animated)
				{
					base.ViewWillDisappear (animated);
				}
				
				public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
				{
					base.DidRotate (fromInterfaceOrientation);
					container.counterPicker.Frame = PickerFrameWithSize (container.counterPicker.SizeThatFits (SizeF.Empty));
				}
				
				public bool Autorotate { get; set; }
				
				public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
				{
					return Autorotate;
				}
			}
			
			public override void Selected (DialogViewController dvc, UITableView tableView, NSIndexPath path)
			{
				model = new CounterPickerDataModel(model.Counter);
				var vc = new MyViewController (this) {
					Autorotate = dvc.Autorotate
				};
				counterPicker = CreatePicker ();
				counterPicker.Frame = PickerFrameWithSize (counterPicker.SizeThatFits (SizeF.Empty));
				counterPicker.Model = model;
				for (int d = 0; d < model.Items.Count; d++) {
					counterPicker.Select(model.SelectedIndex[d], d, true);
				}
				
				// pass value changed
				model.ValueChanged += delegate {
					if (this.ValueChanged != null) {
						Value = model.FormatValue();
						this.ValueChanged (this, new EventArgs ());
					}
				};
				
				// Add the background color
				vc.View.BackgroundColor = this.backgroundColor;
				vc.View.AddSubview (counterPicker);
				
				/**
				UISegmentedControl backControl = new UISegmentedControl(RectangleF.FromLTRB(0,20,50,20));
				
				backControl.ControlStyle = UISegmentedControlStyle.Plain;
				backControl.InsertSegment(UIImage.FromBundle("icons/backButton"), 0, false);
				backControl.Momentary = true;
				
				UIBarButtonItem backButton = new UIBarButtonItem(backControl);
				
				backControl.ValueChanged += (object sender, EventArgs e) => {
					vc.NavigationController.PopViewControllerAnimated(true);
				}; 
				vc.NavigationItem.LeftBarButtonItem = backButton;
				**/
				
				dvc.ActivateController (vc);
				
				
			}
			
			

		}

}
