using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastNote.Core
{
    public interface IItemProvider<TReturn>
    {
        IEnumerable<TReturn> GetItems();
    }
}
