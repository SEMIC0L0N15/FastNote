using System.Collections.Generic;
using System.Collections.ObjectModel;
using FastNote.Core;

namespace FastNote
{
    public interface INoteItemProvider : IParametrizedItemsProvider<NoteItem, NoteGroup>
    {
    }
}
