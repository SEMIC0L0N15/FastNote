using System.Collections.Generic;

namespace FastNote.Core
{
    public interface IItemProvider<T>
    {
        IEnumerable<T> GetItems();
    }
}
