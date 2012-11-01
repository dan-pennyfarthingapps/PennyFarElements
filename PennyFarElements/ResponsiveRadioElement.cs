//
// 	ResponsiveRadioElement.cs
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

namespace PennyFarElements
{
	public class ResponsiveRadioElement : RadioElement {
			
		// Simple RadioElement with an OnSelected Event
		public ResponsiveRadioElement (string s) : base (s) {}
			
		public override void Selected (DialogViewController dvc, UITableView tableView, NSIndexPath path)
		{
			base.Selected (dvc, tableView, path);
			var selected = OnSelected;
			if (selected != null)
				selected (this, EventArgs.Empty);
		}
		
		public event EventHandler<EventArgs> OnSelected;
	}

}


