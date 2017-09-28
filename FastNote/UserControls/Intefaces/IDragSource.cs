using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FastNote
{
    public interface IDragSource : IItemsKeeper, IHasBackground
    {
        FrameworkElement GetDraggableTile();
    }
}
