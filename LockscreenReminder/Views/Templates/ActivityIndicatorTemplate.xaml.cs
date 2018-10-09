using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LockscreenReminder.Views.Templates
{
    public partial class ActivityIndicatorTemplate : Grid
    {
		#region Propriétés bindables
		public static readonly BindableProperty ActivityTextProperty =
			BindableProperty.Create(nameof(ActivityText), typeof(string), typeof(ActivityIndicatorTemplate), default(string), BindingMode.TwoWay);

		public static readonly BindableProperty IsActivateProperty =
			BindableProperty.Create(nameof(IsActivate), typeof(bool), typeof(ActivityIndicatorTemplate), false, BindingMode.TwoWay);
		#endregion

		#region Attributs
		/// <summary>
		/// Correspond au texte affiché pendant le chargement
		/// </summary>
		public string ActivityText
		{
			get
			{
				return GetValue(ActivityTextProperty).ToString();
			}
			set
			{
				SetValue(ActivityTextProperty, value);
			}
		}

		/// <summary>
		/// Définit si la vue est activée
		/// </summary>
		public bool IsActivate
		{
			get
			{
				return (bool)GetValue(IsActivateProperty);
			}
			set
			{
				SetValue(IsActivateProperty, value);
			}
		}
		#endregion

		public ActivityIndicatorTemplate()
		{
			InitializeComponent();
		}

		protected override void OnPropertyChanged(string propertyName)
		{
			base.OnPropertyChanged(propertyName);

			if (propertyName == ActivityTextProperty.PropertyName)
			{
				ActivityLabel.Text = ActivityText;
			}
			if (propertyName == IsActivateProperty.PropertyName)
			{
				this.IsVisible = IsActivate;
				Indicator.IsRunning = IsActivate;
				Indicator.IsEnabled = IsActivate;
			}
		}
    }
}
