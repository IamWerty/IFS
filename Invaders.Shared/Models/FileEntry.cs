using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Invaders.Shared.Models
{
    public class FileEntry
    {
        public string Name { get; set; }
        public bool IsFolder { get; set; }
        public ObservableCollection<FileEntry> Children { get; set; } = new();
    }
}
