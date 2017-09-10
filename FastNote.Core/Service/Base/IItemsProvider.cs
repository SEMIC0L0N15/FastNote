using System.Collections.Generic;

namespace FastNote.Core
{
    public interface IItemsProvider<T>
    {
        IEnumerable<T> GetItems();
        IEnumerable<T> GetItems(object parameter);
    }
}
