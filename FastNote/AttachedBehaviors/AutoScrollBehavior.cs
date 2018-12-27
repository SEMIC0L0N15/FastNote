using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FastNote
{
    public class AutoScrollBehaviorProperty
        : AttachedBehaviorProperty<AutoScrollBehaviorProperty, ScrollViewer>
    {
        protected override AttachedBehavior<ScrollViewer> CreateAttachedBehavior(DependencyObject d)
        {
            return new AutoScrollBehavior((ScrollViewer) d);
        }
    }

    public class AutoScrollBehavior : AttachedBehavior<ScrollViewer>
    {
        private bool autoScroll = true;

        public AutoScrollBehavior(ScrollViewer associatedObject)
            : base(associatedObject)
        {
        }

        public override void OnAttached()
        {
            AssociatedObject.ScrollChanged += ScrollViewer_OnScrollChanged;
            AssociatedObject.PreviewMouseWheel += ScrollViewer_OnPreviewMouseWheel;
        }

        public override void OnDetaching()
        {
            AssociatedObject.ScrollChanged -= ScrollViewer_OnScrollChanged;
            AssociatedObject.PreviewMouseWheel -= ScrollViewer_OnPreviewMouseWheel;
        }

        private void ScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;

            if (e.ExtentHeightChange == 0)
            {
                if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
                    autoScroll = true;
                else
                    autoScroll = false;
            }

            if (autoScroll && e.ExtentHeightChange != 0)
                scrollViewer.ScrollToVerticalOffset(scrollViewer.ExtentHeight);
        }

        private void ScrollViewer_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
            e.Handled = true;
        } 
    }
}
