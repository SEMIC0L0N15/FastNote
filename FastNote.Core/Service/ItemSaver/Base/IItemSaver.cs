using System.Collections.Generic;

namespace FastNote.Core
{
    public interface IItemSaver<T>
    {
        void SaveItems(List<T> items, string filename);
    }
}
