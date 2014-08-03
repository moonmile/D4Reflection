// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace D4Reflection.iPhone
{
	[Register ("D4ReflectioniPhoneViewController")]
	partial class D4ReflectioniPhoneViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btn0 { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btn1 { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btn2 { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView image1 { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel text1 { get; set; }

		[Action ("btn0_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btn0_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (btn0 != null) {
				btn0.Dispose ();
				btn0 = null;
			}
			if (btn1 != null) {
				btn1.Dispose ();
				btn1 = null;
			}
			if (btn2 != null) {
				btn2.Dispose ();
				btn2 = null;
			}
			if (image1 != null) {
				image1.Dispose ();
				image1 = null;
			}
			if (text1 != null) {
				text1.Dispose ();
				text1 = null;
			}
		}
	}
}
