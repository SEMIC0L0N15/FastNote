
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using FastNote.Core;

namespace FastNote
{
    public interface IItemsKeeper
    {
        event Action ItemsChanged;
        void AddItem(object item);
        void DeleteItem(object item);
        IEnumerable<FrameworkElement> GetItems();
    }
}
