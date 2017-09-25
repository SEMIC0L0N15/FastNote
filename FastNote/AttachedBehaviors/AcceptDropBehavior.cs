using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using FastNote.Core;

namespace FastNote
{
	public abstract class AcceptDropBehavior<TSelf> : BaseAttachedBehavior<TSelf, FrameworkElement>
		where TSelf: new()
	{
		private bool isMouseOver;

		protected override void OnAttached()
		{
			AssociatedObject.MouseEnter += OnMouseEnter;
			AssociatedObject.MouseLeave += OnMouseLeave;
			AssociatedObject.MouseLeftButtonUp += OnMouseLeftButtonUp;
		}

		protected override void OnDetaching()
		{
			AssociatedObject.MouseEnter -= OnMouseEnter;
			AssociatedObject.MouseLeave -= OnMouseLeave;
			AssociatedObject.MouseLeftButtonUp -= OnMouseLeftButtonUp;
		}

		private void OnMouseEnter(object sender, MouseEventArgs e)
		{
			isMouseOver = true;
		}

		private void OnMouseLeave(object sender, MouseEventArgs e)
		{
			isMouseOver = false;
		}

		private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (isMouseOver && ViewModelLocator.ApplicationViewModel.IsDragActive)
			{
				OnDrop(sender, e);
			}
		}

		protected abstract void OnDrop(object sender, MouseButtonEventArgs mouseButtonEventArgs);
	}
}
