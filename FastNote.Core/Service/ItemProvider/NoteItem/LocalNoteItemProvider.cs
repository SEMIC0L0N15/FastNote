using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace FastNote.Core
{
    public class LocalNoteItemProvider : INoteItemProvider
    {
        public IEnumerable<NoteItem> GetItems(NoteGroup noteGroup)
        {
            Directory.CreateDirectory("notes");

            using (FileStream stream = File.Open("notes/" + noteGroup.Name + ".xml", FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (stream.Length == 0)
                    return new List<NoteItem>();

                var serializer = new XmlSerializer(typeof(List<NoteItem>));
                var items = (List<NoteItem>) serializer.Deserialize(stream);
                return items;
            }
        }
    }
}
