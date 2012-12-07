// 
// CounterElement.cs: // Author:
//   Guido Van Hoecke
//
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System;
using MonoTouch.Dialog;
using System.Drawing;
using System.Collections.Generic;

namespace PennyFarElements {
	public class TimeWithSecondsPickerElement : StringElement {
		public UIPickerView counterPicker;
		protected TimePickerDataModel model;

		protected class TimePickerDataModel : UIPickerViewModel
		{
			public event EventHandler<EventArgs> ValueChanged;


			private TimeSpan _time;

			private List<List<int>> _times;

			private List<int> _hours;
			private List<int> _minutes;
			private List<int> _seconds;
			private List<int> _selectedIndex;

			public TimePickerDataModel(TimeSpan time) {
				this.FillTimes();

				this._times = new List<List<int>>();
				this._selectedIndex = new List<int>();

			}

			public TimeSpan Time { 
				get { return this._time; }
			}

			public override int GetRowsInComponent (UIPickerView picker, int component)
			{
				return this._times[component].Count;
			
			}

			public override string GetTitle (UIPickerView picker, int row, int component)
			{
				// TODO: Implement - see: http://go-mono.com/docs/index.aspx?link=T%3aMonoTouch.Foundation.ModelAttribute
			}

			public override int GetComponentCount (UIPickerView picker)
			{
				return this._times.Count;
			}

			public override void Selected (UIPickerView picker, int row, int component)
			{
				this._selectedIndex[component] = row;
				if (this.ValueChanged != null) {
					this.ValueChanged (this, new EventArgs ());
				}
			}

			public List<int> SelectedIndex {
				get { return _selectedIndex; }
			}


			private void FillTimes ()
			{
				this._hours = new List<int> ();
				this._minutes = new List<int> ();
				this._seconds = new List<int> ();

				for (int i = 0; i< 25; i++) {
					this._hours.Add(i);

				}

				for (int i = 0; i< 60; i++) {
					this._minutes.Add(i);
					
				}

				for (int i = 0; i< 60; i++) {
					this._seconds.Add(i);
					
				}


			}

		} 
		
		public TimeWithSecondsPickerElement(string caption, TimeSpan time) : base (caption) {
			model = new TimePickerDataModel(time);
		}
		
		public override UITableViewCell GetCell (UITableView tv)
		{
			Value = model.FormatValue();
			var cell = base.GetCell(tv);
			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			return cell;
		}
		
		protected override void Dispose (bool disposing)
		{ 
			base.Dispose(disposing);
			if (disposing) {
				if (model != null) {
					model.Dispose();
					model = null;
				}
				if (counterPicker != null) {
					counterPicker.Dispose();
					counterPicker = null;
				}
			}
		}
		
		public virtual UIPickerView CreatePicker ()
		{
			var picker = new UIPickerView (RectangleF.Empty){
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth,
				ShowSelectionIndicator = true,
				
			};
			return picker;
		}
		
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
			TimeWithSecondsPickerElement container;
			
			public MyViewController (TimeWithSecondsPickerElement container)
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
			vc.View.BackgroundColor = UIColor.Black;
			vc.View.AddSubview (counterPicker);
			dvc.ActivateController (vc);
		}
	}
}	