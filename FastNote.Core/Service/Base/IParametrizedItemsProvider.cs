using System.Collections.Generic;

namespace FastNote.Core
{
    public interface IParametrizedItemsProvider<ReturnType, ParameterType>
    {
        IEnumerable<ReturnType> GetItems(ParameterType parameter);
    }
}
