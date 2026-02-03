using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Invaders.Shared.Models;
using System.Text;

namespace Invaders.Client.ViewModels
{
    public class FileViewModel
    {
        public string Name => Model.Name;
        public bool IsFolder => Model.IsFolder;

        public ObservableCollection<FileViewModel> Children { get; set; } = new();

        public FileEntry Model { get; }

        public FileViewModel(FileEntry entry)
        {
            Model = entry;

            if (entry.Children != null)
                foreach (var child in entry.Children)
                    Children.Add(new FileViewModel(child));
        }
    }
}
