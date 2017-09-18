using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastNote.Core
{
    public interface IParametrizedItemsProvider<TReturn, TParameter>
    {
        IEnumerable<TReturn> GetItems(TParameter parameter);
    }
}
