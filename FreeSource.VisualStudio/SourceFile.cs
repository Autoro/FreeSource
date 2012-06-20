using System;
using System.IO;
using FreeSource.Extensibility;

namespace FreeSource.VisualStudio
{
    public class SourceFile : ISourceFile
    {
        public FileInfo Path
        {
            get;
            private set;
        }

        public SourceFile(string path)
        {
            this.Path = new FileInfo(path);
        }
    }
}
