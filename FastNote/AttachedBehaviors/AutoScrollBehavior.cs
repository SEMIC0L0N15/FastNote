using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FastNote
{
    #region Property
    public class AutoScrollBehaviorProperty
        : AttachedBehaviorProperty<AutoScrollBehaviorProperty, ScrollViewer>
    {
        protected override AttachedBehavior<ScrollViewer> CreateAttachedBehavior(DependencyObject d)
        {
            return new AutoScrollBehavior((ScrollViewer) d);
        }
    }
    #endregion

    public class AutoScrollBehavior : AttachedBehavior<ScrollViewer>
    {
        #region Private Members
        private bool autoScroll = true;
        #endregion

        #region Contructor
        public AutoScrollBehavior(ScrollViewer associatedObject)
            : base(associatedObject)
        {
        }
        #endregion

        #region Attach / Detach
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
        #endregion

        #region Event Handlers
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
        #endregion
    }
}
