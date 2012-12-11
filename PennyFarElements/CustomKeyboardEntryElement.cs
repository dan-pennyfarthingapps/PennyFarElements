//
// 	CustomKeyboardEntryElement.cs
// 	Author: Daniel Wiseman
//	PennyFarElements: http://penfarapps.com/Sx1v5n
//
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using System.Threading;

namespace PennyFarElements
{
	// Creates an entryelement with toolbar over the keyboard. It loops through the various elements as defined in code.
	public class CustomKeyboardEntryElement : EntryElement
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PennyFarElements.CustomKeyboardEntryElement"/> class.
		/// </summary>
		/// <param name='caption'>
		/// Caption.
		/// </param>
		/// <param name='placeholder'>
		/// Placeholder.
		/// </param>
		/// <param name='value'>
		/// Value.
		/// </param>
		public CustomKeyboardEntryElement (string caption, string placeholder, string value) : base(caption, placeholder, value)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PennyFarElements.CustomKeyboardEntryElement"/> class.
		/// </summary>
		/// <param name='caption'>
		/// Caption.
		/// </param>
		/// <param name='placeholder'>
		/// Placeholder.
		/// </param>
		/// <param name='value'>
		/// Value.
		/// </param>
		/// <param name='password'>
		/// If set to <c>true</c> password.
		/// </param>
		public CustomKeyboardEntryElement (string caption, string placeholder, string value, bool password) : base(caption, placeholder, value, password)
		{
		}

		/// <summary>
		/// Hides the keyboard.
		/// </summary>
		private void HideKeyboard ()
		{
			this.ResignFirstResponder(true);
		}
		
		/// <Docs>
		/// Bounds for the entry to create
		/// </Docs>
		/// <returns>
		/// 
		/// </returns>
		/// <summary>
		/// Creates the text field.
		/// </summary>
		/// <param name='frame'>
		/// Frame.
		/// </param>
		protected override UITextField CreateTextField (RectangleF frame)
		{
			UITextField uITextField = base.CreateTextField(frame);
			
			string closeButtonText = "Done";
			
			UIToolbar toolbar = new UIToolbar ();
			uITextField.KeyboardType = this.KeyboardType;
			toolbar.BarStyle = UIBarStyle.Black;
			toolbar.Translucent = true;
			toolbar.SizeToFit ();
			UIBarButtonItem nextButton = new UIBarButtonItem ("Next", UIBarButtonItemStyle.Bordered,
			                                                  (s, e) => {
				
				nextelement.BecomeFirstResponder(true);
				
				
			});
			if(nextelement == null)
				nextButton.Enabled = false;
			
			UIBarButtonItem prevButton = new UIBarButtonItem ("Prev", UIBarButtonItemStyle.Bordered,
			                                                  (s, e) => {
				
				prevelement.BecomeFirstResponder(true);
				
				
			});
			if(prevelement == null)
				prevButton.Enabled = false;
			
			UIBarButtonItem doneButton = new UIBarButtonItem (closeButtonText, UIBarButtonItemStyle.Done,
			                                                  (s, e) => {
				uITextField.ResignFirstResponder ();
			});
			toolbar.SetItems (new UIBarButtonItem[]{prevButton, nextButton, new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace), doneButton}, true);
			
			uITextField.InputAccessoryView = toolbar;
			
			return uITextField;
		}

		/// <summary>
		/// Sets the next element.
		/// </summary>
		/// <value>
		/// The next element.
		/// </value>
		public EntryElement NextElement {
			set { 
				nextelement = value;
			}
		}
		private EntryElement nextelement;

		/// <summary>
		/// Sets the previous element.
		/// </summary>
		/// <value>
		/// The previous element.
		/// </value>
		public EntryElement PrevElement {
			set { 
				prevelement = value;
			}
		}
		private EntryElement prevelement;



	}
}

