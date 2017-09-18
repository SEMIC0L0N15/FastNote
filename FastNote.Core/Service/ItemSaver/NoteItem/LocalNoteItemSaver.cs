using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace FastNote.Core
{
    public class LocalNoteItemSaver : INoteItemSaver
    {
        public void SaveItems(List<NoteItem> items, string filename)
        {
            using (FileStream stream = File.Open("notes/" + filename + ".xml", FileMode.Create, FileAccess.Write))
            {
                var serializer = new XmlSerializer(typeof(List<NoteItem>));
                serializer.Serialize(stream, items);
            }
        }
    }
}
