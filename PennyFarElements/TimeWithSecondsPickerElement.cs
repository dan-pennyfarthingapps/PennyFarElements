// 
//	TimeWithSecondsPickerElement
// 	Author: Daniel Wiseman
//	PennyFarElements: http://penfarapps.com/Sx1v5n
//
// Based HEAVILY on: CounterElement.cs: 
// 	Author:
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
		private UIColor backgroundColor;


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

				this._time = time;

				this._times = new List<List<int>>();
				this._selectedIndex = new List<int>();

				this.SetTimes();

			}

			public TimeSpan Time { 
				get { return this._time; }
			}

			public List<List<int>> Times {
				get { return this._times; }
				set { this._times = value; }
			}

			public override int GetRowsInComponent (UIPickerView picker, int component)
			{
				return this._times[component].Count;
			
			}

			public override string GetTitle (UIPickerView picker, int row, int component)
			{
				return this._times[component][row].ToString();
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

				this._time = new TimeSpan(this._selectedIndex[0], this._selectedIndex[1], this._selectedIndex[2]);

			}

			public List<int> SelectedIndex {
				get { return _selectedIndex; }
			}

			public string FormatValue () 
			{

				return this._time.ToString();
			}


			public override UIView GetView(UIPickerView picker, int row, int component, UIView view) {
				var label = new UILabel {
					Text = this._times[component][row].ToString(),
					TextAlignment = UITextAlignment.Center,
				};
				if (row == SelectedIndex[component]) {
					label.TextColor = UIColor.Magenta;
					switch (component) {
					case 0:
						label.Text += " hours";
						break;
					case 1: 
						label.Text += " min";
						break;
					case 2:
						label.Text += " sec";
						break;

					}

				} 

				return label;
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

			private void SetTimes ()
			{
				this._selectedIndex.Add(this._time.Hours);
				this._selectedIndex.Add(this._time.Minutes);
				this._selectedIndex.Add(this._time.Seconds);

				this._times.Add(this._hours);
				this._times.Add(this._minutes);
				this._times.Add(this._seconds);

			} 
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PennyFarElements.TimeWithSecondsPickerElement"/> class.
		/// </summary>
		/// <param name='caption'>
		/// Caption.
		/// </param>
		/// <param name='time'>
		/// Time.
		/// </param>
		public TimeWithSecondsPickerElement(string caption, TimeSpan time) : base (caption) {
			model = new TimePickerDataModel(time);
			backgroundColor = UIColor.Black;
		}

		/// <summary>
		/// Occurs when value changed.
		/// </summary>
		public event EventHandler<EventArgs> ValueChanged;
		
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

		public TimeSpan TimeValue {
			get { return model.Time; }
		}

		public UIColor BackgroundColor {
			get { return backgroundColor; }
			set { backgroundColor = value; }
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
			model = new TimePickerDataModel(model.Time);
			var vc = new MyViewController (this) {
				Autorotate = dvc.Autorotate
			};
			counterPicker = CreatePicker ();
			counterPicker.Frame = PickerFrameWithSize (counterPicker.SizeThatFits (SizeF.Empty));
			counterPicker.Model = model;
			for (int d = 0; d < model.Times.Count; d++) {
				counterPicker.Select(model.SelectedIndex[d], d, true);
			}

			// pass value changed
			model.ValueChanged += delegate {
				if (this.ValueChanged != null) {
					Value = model.FormatValue ();
					this.ValueChanged (this, new EventArgs ());
				}
			};

			vc.View.BackgroundColor = backgroundColor;
			vc.View.AddSubview (counterPicker);
			dvc.ActivateController (vc);
		}
	}
}	