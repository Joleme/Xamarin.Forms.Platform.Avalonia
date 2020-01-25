﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Forms.Platform.Avalonia.Controls
{
	public class FormsTimePicker : TextBox
	{
		#region Properties
		public static readonly StyledProperty<TimeSpan?> TimeProperty = AvaloniaProperty.Register<FormsTimePicker, TimeSpan?>(nameof(Time));
		public TimeSpan? Time
		{
			get { return (TimeSpan?)GetValue(TimeProperty); }
			set { SetValue(TimeProperty, value); }
		}

		public static readonly StyledProperty<string> TimeFormatProperty = AvaloniaProperty.Register<FormsTimePicker, string>(nameof(TimeFormat));
		public String TimeFormat
		{
			get { return (String)GetValue(TimeFormatProperty); }
			set { SetValue(TimeFormatProperty, value); }
		}
		#endregion

		#region Events
		public delegate void TimeChangedEventHandler(object sender, TimeChangedEventArgs e);
		public event TimeChangedEventHandler TimeChanged;
		#endregion

		public FormsTimePicker()
		{

		}

		//public override void OnApplyTemplate()
		//{
		//	base.OnApplyTemplate();
		//	SetText();
		//}

		private void SetText()
		{
			if (Time == null)
				Text = null;
			else
			{
				var dateTime = new DateTime(Time.Value.Ticks);
				
				String text = dateTime.ToString(String.IsNullOrWhiteSpace(TimeFormat) ? @"hh\:mm" : TimeFormat.ToLower());
				if (text.CompareTo(Text) != 0)
					Text = text;
			}
		}

		private void SetTime()
		{
			DateTime dateTime = DateTime.MinValue;
			String timeFormat = String.IsNullOrWhiteSpace(TimeFormat) ? @"hh\:mm" : TimeFormat.ToLower();

			if (DateTime.TryParseExact(Text, timeFormat, null, System.Globalization.DateTimeStyles.None, out dateTime))
			{
				if ((Time == null) || (Time != null && Time.Value.CompareTo(dateTime.TimeOfDay) != 0))
				{
					if (dateTime.TimeOfDay < TimeSpan.FromHours(24) && dateTime.TimeOfDay > TimeSpan.Zero)
						Time = dateTime.TimeOfDay;
					else
						SetText();
				}
			}
			else
				SetText();
		}

		#region Overrides
		protected override void OnLostFocus(RoutedEventArgs e)
		{
			SetTime();
			base.OnLostFocus(e);
		}

		//protected override void OnGotFocus(RoutedEventArgs e)
		//{
		//	base.OnGotFocus(e);
		//}
		#endregion

		#region Property Changes
		//private static void OnTimePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		//{
		//	FormsTimePicker element = d as FormsTimePicker;
		//	if (element == null)
		//		return;

		//	element.OnTimeChanged(e.OldValue as TimeSpan?, e.NewValue as TimeSpan?);
		//}

		private void OnTimeChanged(TimeSpan? oldValue, TimeSpan? newValue)
		{
			SetText();

			if (TimeChanged != null)
				TimeChanged(this, new TimeChangedEventArgs(oldValue, newValue));
		}

		//private static void OnTimeFormatPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		//{
		//	FormsTimePicker element = d as FormsTimePicker;
		//	if (element == null)
		//		return;

		//	element.OnTimeFormatChanged();
		//}

		private void OnTimeFormatChanged()
		{
			SetText();
		}
		#endregion
	}

	public class TimeChangedEventArgs : EventArgs
	{
		private TimeSpan? _oldTime;
		private TimeSpan? _newTime;

		public TimeSpan? OldTime
		{
			get { return _oldTime; }
		}

		public TimeSpan? NewTime
		{
			get { return _newTime; }
		}

		public TimeChangedEventArgs(TimeSpan? oldTime, TimeSpan? newTime)
		{
			_oldTime = oldTime;
			_newTime = newTime;
		}
	}
}