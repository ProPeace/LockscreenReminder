﻿using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LockscreenReminder.Controls
{
	public class WrapLayout : Layout<View>
    {
		#region Propiétés bindables
		readonly Dictionary<Size, LayoutData> layoutDataCache = new Dictionary<Size, LayoutData>();

		public static readonly BindableProperty ColumnSpacingProperty = BindableProperty.Create(
			"ColumnSpacing",
			typeof(double),
			typeof(WrapLayout),
			5.0,
			propertyChanged: (bindable, oldvalue, newvalue) =>
			{
				((WrapLayout)bindable).InvalidateLayout();
			});

		public static readonly BindableProperty RowSpacingProperty = BindableProperty.Create(
			"RowSpacing",
			typeof(double),
			typeof(WrapLayout),
			5.0,
			propertyChanged: (bindable, oldvalue, newvalue) =>
			{
				((WrapLayout)bindable).InvalidateLayout();
			});

		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
				"ItemsSource",
				typeof(IList),
				typeof(WrapLayout),
				propertyChanging: (bindableObject, oldValue, newValue) => {
					((WrapLayout)bindableObject).ItemsSourceChanging();
				},
				propertyChanged: (bindableObject, oldValue, newValue) => {
					((WrapLayout)bindableObject).ItemsSourceChanged();
				}
			);
		#endregion

		#region Attributs
		public double ColumnSpacing
		{
			set { SetValue(ColumnSpacingProperty, value); }
			get { return (double)GetValue(ColumnSpacingProperty); }
		}

		public double RowSpacing
		{
			set { SetValue(RowSpacingProperty, value); }
			get { return (double)GetValue(RowSpacingProperty); }
		}

		public IList ItemsSource
		{
			get
			{
				return (IList)GetValue(ItemsSourceProperty);
			}
			set
			{
				SetValue(ItemsSourceProperty, value);
			}
		}

		public DataTemplate ItemTemplate
		{
			get;
			set;
		}
		#endregion


		void ItemsSourceChanging()
		{
			if (ItemsSource == null) return;
			//_selectedIndex = ItemsSource.IndexOf (SelectedItem);
		}

		void ItemsSourceChanged()
		{
			Children.Clear();
			if (ItemsSource != null)
			{
				foreach (var item in ItemsSource)
				{
					var view = (View)ItemTemplate.CreateContent();
					var bindableObject = view as BindableObject;
					if (bindableObject != null)
						bindableObject.BindingContext = item;
					Children.Add(view);
				}
			}
		}

		LayoutData GetLayoutData(double width, double height)
		{
			Size size = new Size(width, height);

			if (layoutDataCache.ContainsKey(size))
			{
				return layoutDataCache[size];
			}

			int visibleChildCount = 0;
			Size maxChildSize = new Size();
			int rows = 0;
			int columns = 0;
			LayoutData layoutData = new LayoutData();

			foreach (View child in Children)
			{
				if (!child.IsVisible)
					continue;

				visibleChildCount++;
				SizeRequest childSizeRequest = child.Measure(Double.PositiveInfinity, Double.PositiveInfinity);
				maxChildSize.Width = Math.Max(maxChildSize.Width, childSizeRequest.Request.Width);
				maxChildSize.Height = Math.Max(maxChildSize.Height, childSizeRequest.Request.Height);
			}

			if (visibleChildCount != 0)
			{
				if (Double.IsPositiveInfinity(width))
				{
					columns = visibleChildCount;
					rows = 1;
				}
				else
				{
					columns = (int)((width + ColumnSpacing) / (maxChildSize.Width + ColumnSpacing));
					columns = Math.Max(1, columns);
					rows = (visibleChildCount + columns - 1) / columns;
				}

				Size cellSize = new Size();

				if (Double.IsPositiveInfinity(width))
				{
					cellSize.Width = maxChildSize.Width;
				}
				else
				{
					cellSize.Width = (width - ColumnSpacing * (columns - 1)) / columns;
				}

				if (Double.IsPositiveInfinity(height))
				{
					cellSize.Height = maxChildSize.Height;
				}
				else
				{
					cellSize.Height = (height - RowSpacing * (rows - 1)) / rows;
				}

				layoutData = new LayoutData(visibleChildCount, cellSize, rows, columns);
			}

			layoutDataCache.Add(size, layoutData);
			return layoutData;
		}

		#region Méthodes surchargées
		protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
		{
			LayoutData layoutData = GetLayoutData(widthConstraint, heightConstraint);

			if (layoutData.VisibleChildCount == 0)
			{
				return new SizeRequest();
			}

			Size totalSize = new Size(layoutData.CellSize.Width * layoutData.Columns + ColumnSpacing * (layoutData.Columns - 1),
									  layoutData.CellSize.Height * layoutData.Rows + RowSpacing * (layoutData.Rows - 1));
			return new SizeRequest(totalSize);
		}

		protected override void LayoutChildren(double x, double y, double width, double height)
		{
			LayoutData layoutData = GetLayoutData(width, height);

			if (layoutData.VisibleChildCount == 0)
			{
				return;
			}

			double xChild = x;
			double yChild = y;
			int row = 0;
			int column = 0;

			foreach (View child in Children)
			{
				if (!child.IsVisible)
				{
					continue;
				}

				LayoutChildIntoBoundingRegion(child, new Rectangle(new Point(xChild, yChild), layoutData.CellSize));

				if (++column == layoutData.Columns)
				{
					column = 0;
					row++;
					xChild = x;
					yChild += RowSpacing + layoutData.CellSize.Height;
				}
				else
				{
					xChild += ColumnSpacing + layoutData.CellSize.Width;
				}
			}
		}

		protected override void InvalidateLayout()
		{
			base.InvalidateLayout();
			layoutDataCache.Clear();
		}

		protected override void OnChildMeasureInvalidated()
		{
			base.OnChildMeasureInvalidated();
			layoutDataCache.Clear();
		}
		#endregion
    }
}
