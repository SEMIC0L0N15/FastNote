﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastNote.Core;

namespace FastNote
{
    class DesignNoteItemProvider : INoteItemProvider
    {
        public IEnumerable<NoteItem> GetItems(NoteGroup noteGroup)
        {
            var items = new ObservableCollection<NoteItem>();

            switch (noteGroup.Name)
            {
                case "Gry do zagrania":
                    items = new ObservableCollection<NoteItem>
                    {
                        new NoteItem("Lords of The Fallen"),
                        new NoteItem("DeusEx: Rozłam Ludzkości"),
                        new NoteItem("Sid Meier's Civilization VI"),
                        new NoteItem("Dragon Age: Inkwizycja"),
                        new NoteItem("Mass Effect: Andromeda"),
                    };
                    break;

                case "Filmy do obejrzenia":
                    items = new ObservableCollection<NoteItem>
                    {
                        new NoteItem("Gladiator"),
                        new NoteItem("Interstellar"),
                        new NoteItem("Incepcja"),
                    };
                    break;


                case "Kod":
                    items = new ObservableCollection<NoteItem>
                    {
                        new NoteItem(
                            "private void OnWindowResized()\r\n{\r\n            RaisePropertyChanged(nameof(Borderless));\r\n            RaisePropertyChanged(nameof(Border));\r\n            RaisePropertyChanged(nameof(BorderThickness));\r\n            RaisePropertyChanged(nameof(OuterMargin));\r\n            RaisePropertyChanged(nameof(OuterMarginThickness));\r\n            RaisePropertyChanged(nameof(ResizeBorder));\r\n            RaisePropertyChanged(nameof(ResizeBorderThickness));\r\n            RaisePropertyChanged(nameof(IsActive));\r\n}"),
                        new NoteItem(
                            "private void CreateCommands()\r\n{\r\n            MinimizeCommand = new RelayCommand(Minimize);\r\n            MaximizeCommand = new RelayCommand(Maximize);\r\n            CloseCommand = new RelayCommand(CloseWindow);\r\n            SystemMenuCommand = new RelayCommand(ShowSystemMenu);\r\n}"),
                        new NoteItem(
                            "private void Minimize()\r\n{\r\n            if (mWindow.WindowState != WindowState.Maximized)\r\n                mWindow.WindowStyle = WindowStyle.SingleBorderWindow;\r\n            mWindow.WindowState = WindowState.Minimized;\r\n}")
                    };
                    break;
            }

            return items;
        }
    }
}
