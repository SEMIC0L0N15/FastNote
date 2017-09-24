using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;

namespace FastNote.Core
{
    public class NoteGroup : ObservableObject
    {
        #region Public Properties
        public string Name { get; set; }
        private List<NoteItem> Notes { get; set; } = new List<NoteItem>();
        #endregion

        #region Constructor
        public NoteGroup(string name)
        {
            Name = name;
        }

        public NoteGroup() { }
        #endregion

        #region Methods
        public void AddNote(NoteItem noteItem)
        {
            Notes.Add(noteItem);
            SaveNotes();
            RaisePropertyChanged(nameof(Notes));
        }

        public void DeleteNote(NoteItem noteItem)
        {
            Notes.Remove(noteItem);
            SaveNotes();
            RaisePropertyChanged(nameof(Notes));
        }

        public List<NoteItem> GetNotes()
        {
            return Notes;
        }

        public void UpdateNotes()
        {
            Directory.CreateDirectory("notes");

            using (FileStream stream = File.Open("notes/" + Name + ".xml", FileMode.OpenOrCreate,
                FileAccess.Read))
            {
                if (stream.Length == 0)
                    Notes = new List<NoteItem>();
                else
                {
                    var serializer = new XmlSerializer(typeof(List<NoteItem>));
                    Notes = (List<NoteItem>) serializer.Deserialize(stream);
                }
            }
        }

        public void SaveNotes()
        {
            using (FileStream stream = File.Open("notes/" + Name + ".xml", FileMode.Create, FileAccess.Write))
            {
                var serializer = new XmlSerializer(typeof(List<NoteItem>));
                serializer.Serialize(stream, Notes);
            }
        }

        #endregion
    }
}
